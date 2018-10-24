using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.IDP
{
    public partial class VentanaAutorizaRequisicion : System.Web.UI.Page
    {
        #region Variables

        StringBuilder builder = new StringBuilder();
        private int pIdRequisicion
        {
            get { return (int)ViewState["vspIdRequisicion"]; }
            set { ViewState["vspIdRequisicion"] = value; }
        }

        private string pEstatus
        {
            get { return (string)ViewState["vsEstatus"]; }
            set { ViewState["vsEstatus"] = value; }
        }

        private string pCausa
        {
            get { return (string)ViewState["vsCausa"]; }
            set { ViewState["vsCausa"] = value; }
        }

        public int vIdPuesto
        {
            get { return (int)ViewState["vs_var_id_puesto"]; }
            set { ViewState["vs_var_id_puesto"] = value; }
        }

        public string vClProceso
        {
            get { return (string)ViewState["vs_var_cl_proceso"]; }
            set { ViewState["vs_var_cl_proceso"] = value; }
        }

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        #endregion

        #region Metodos

        public void EnvioCorreo(string Email, string Mensaje, string Asunto)
        {
            Mail mail = new Mail(ContextoApp.mailConfiguration);
            mail.addToAddress(Email, "");
            RadProgressContext progress = RadProgressContext.Current;
            mail.Send(Asunto, String.Format("{0}", Mensaje));
        }

        private void CargarDatos()
        {
            RequisicionNegocio nARequisicion = new RequisicionNegocio();
            var vAutorizaRequisicion = nARequisicion.ObtenerAutorizarRequisicion(ID_REQUISICION: pIdRequisicion).FirstOrDefault();

            if (vAutorizaRequisicion != null)
            {
                txtClaveRequisicion.InnerText = vAutorizaRequisicion.NO_REQUISICION;
                //txtPuesto.InnerText = vAutorizaRequisicion.NB_PUESTO;
                vIdPuesto = vAutorizaRequisicion.ID_PUESTO.Value;
                txtPUestoEnlace.InnerText = vAutorizaRequisicion.NB_PUESTO;
                //txtEmpresa.Text = vAutorizaRequisicion.NB_EMPRESA;
                rtbSolicita.InnerText = vAutorizaRequisicion.NB_EMPLEADO_SOLICITANTE;
                rdpFeSolicitud.InnerText = vAutorizaRequisicion.FE_SOLICITUD.ToShortDateString();
                rdpFeRequisicion.InnerText = vAutorizaRequisicion.FE_REQUERIMIENTO.Value.ToShortDateString();
                rtbCausaRequisicion.InnerText = vAutorizaRequisicion.NB_CAUSA_REQUISICION;

                //rtbDsCausa.Text = vAutorizaRequisicion.DS_CAUSA;
                rtbObservacionesPuesto.Text = vAutorizaRequisicion.DS_OBSERVACIONES_PUESTO;
                txtObservacionesRequisicion.Text = vAutorizaRequisicion.DS_OBSERVACIONES_REQUISICION;

                txtSueldoActual.InnerText = string.Format("{0:C2}", vAutorizaRequisicion.MN_SUELDO.ToString());
                txtSueldoSugerido.InnerText = string.Format("{0:C2}", vAutorizaRequisicion.MN_SUELDO_TABULADOR.ToString());
                txtSueldoMinimo.InnerText = string.Format("{0:C2}", vAutorizaRequisicion.MN_SUELDO_SUGERIDO.ToString());
                txtSueldoMaximo.InnerText = string.Format("{0:C2}", vAutorizaRequisicion.MAX_SUELDO_SUGERIDO.ToString());
                pEstatus = vAutorizaRequisicion.CL_ESTATUS_REQUISICION;
                pCausa = vAutorizaRequisicion.CL_CAUSA;

                if (vAutorizaRequisicion.CL_ESTATUS_PUESTO.Equals("AUTORIZADO"))
                {
                    rtbObservacionesPuesto.ReadOnly = true;
                    btnAutorizar.Enabled = false;
                    btnRechazar.Enabled = false;
                    btnAutorizar.Checked = true;
                }

                if (vAutorizaRequisicion.CL_ESTATUS_REQUISICION.Equals("AUTORIZADO"))
                {
                    txtObservacionesRequisicion.ReadOnly = true;
                    btnAutorizarReq.Enabled = false;
                    btnRechazarReq.Enabled = false;
                    btnAutorizarReq.Checked = true;
                }

                if (pCausa == "SUPLENCIA" || pCausa == "VACANTE")
                {
                    //lblSuplir.Visible = true;
                    //txtEmpleadoSuplir.Visible = true;
                  //  rowSuplente.Style.Add("dispay", "block");
                    txtEmpleadoSuplir.InnerText = vAutorizaRequisicion.NB_EMPLEADO_SUPLENTE;
                 

                }
                else
                {
                    rowSuplente.Style.Add("dispay", "none");
                    txtEmpleadoSuplir.InnerText = "NA";
                }
            }


            if (vClProceso.Equals("NOTIFICACIONRRHH"))
            {
                divComentariosPuesto.Style.Add("display", "block");
                divComentariosRequisicion.Style.Add("display", "none");
            }

            if (vClProceso.Equals("AUTORIZAREQUISICION"))
            {
                divComentariosPuesto.Style.Add("display", "none");
                divComentariosRequisicion.Style.Add("display", "block");
            }

            if (vClProceso.Equals("AUTORIZAREQPUESTO"))
            {
                divComentariosPuesto.Style.Add("display", "block");
                divComentariosRequisicion.Style.Add("display", "block");
            }
        }

        private void GuardarDatosPuesto()
        {
            RequisicionNegocio nRequisicion = new RequisicionNegocio();
            EmpleadoNegocio nEmpleado = new EmpleadoNegocio();

            string vClEstatusPuesto = btnAutorizar.Checked ? "AUTORIZADO" : "RECHAZADO";
            string vMensajeCorreo = "";
            string vNbCorreo = "";
            string nbCreaRequisicion = "";
            string vDsComentarios = rtbObservacionesPuesto.Text;
            string Asunto = "Actualización de puesto de requisición";

            var vRequisicion = nRequisicion.ObtieneRequisicion(pIdRequisicion: pIdRequisicion).FirstOrDefault();
          //  var vSolicitante = nEmpleado.ObtenerEmpleado(ID_EMPLEADO: vRequisicion.ID_SOLICITANTE).FirstOrDefault();

           // if (vSolicitante != null)
            if (vRequisicion != null)
            {
                //nbCreaRequisicion = vSolicitante.NB_EMPLEADO_COMPLETO;
                //vNbCorreo = vSolicitante.CL_CORREO_ELECTRONICO;
                nbCreaRequisicion = vRequisicion.NB_EMPLEADO_SOLICITANTE;
                vNbCorreo = vRequisicion.NB_CORREO_SOLICITANTE;
            }

            if (vClEstatusPuesto.Equals("RECHAZADO"))
            {
                vMensajeCorreo = ContextoApp.IDP.NotificacionRrhh.dsRechazarPuesto.dsMensaje;
            }
            else
            {
                vMensajeCorreo = ContextoApp.IDP.NotificacionRrhh.dsAutorizarPuesto.dsMensaje;
            }

            vMensajeCorreo = vMensajeCorreo.Replace("[NB_CREA_REQUISICION]", nbCreaRequisicion);
            vMensajeCorreo = vMensajeCorreo.Replace("[NB_PUESTO]", vRequisicion.NB_PUESTO);
            vMensajeCorreo = vMensajeCorreo.Replace("[DS_COMENTARIOS]", vDsComentarios);
            vMensajeCorreo = vMensajeCorreo.Replace("[CL_REQUISICION]", vRequisicion.NO_REQUISICION);

            E_RESULTADO vResultado = nRequisicion.ActualizaEstatusRequisicion(false, true,pIdRequisicion,vRequisicion.ID_PUESTO.Value, rtbObservacionesPuesto.Text, txtObservacionesRequisicion.Text, vClEstatusPuesto, "", vNbPrograma, vClUsuario);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                builder.Append(vNbCorreo + ";");
                EnvioCorreo(builder.ToString(), vMensajeCorreo, Asunto);
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "OnCloseWindow");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensajeError, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
            }
        }

        private void GuardarDatosRequisicion()
        {
            RequisicionNegocio nRequisicion = new RequisicionNegocio();
            EmpleadoNegocio nEmpleado = new EmpleadoNegocio();

            string vClEstatusRequisicion = btnAutorizarReq.Checked ? "AUTORIZADO" : "RECHAZADO";
            string vMensajeCorreo = "";
            string vNbCorreo = "";
            string nbCreaRequisicion = "";
            string vDsComentarios = txtObservacionesRequisicion.Text;
            string Asunto = "Actualización de requisición";

            var vRequisicion = nRequisicion.ObtieneRequisicion(pIdRequisicion: pIdRequisicion).FirstOrDefault();
           // var vSolicitante = nEmpleado.ObtenerEmpleado(ID_EMPLEADO: vRequisicion.ID_SOLICITANTE).FirstOrDefault();

            //if (vSolicitante != null)
            if (vRequisicion != null)
            {
                nbCreaRequisicion = vRequisicion.NB_EMPLEADO_SOLICITANTE;
                vNbCorreo = vRequisicion.NB_CORREO_SOLICITANTE;
            }

            if (vClEstatusRequisicion.Equals("RECHAZADO"))
            {
                vMensajeCorreo = ContextoApp.IDP.NotificacionRrhh.dsCancelarRequisicion.dsMensaje;
            }
            else
            {
                vMensajeCorreo = ContextoApp.IDP.NotificacionRrhh.dsAutorizarRequisicion.dsMensaje;
            }

            vMensajeCorreo = vMensajeCorreo.Replace("[NB_NOTIFICA]", nbCreaRequisicion);
            vMensajeCorreo = vMensajeCorreo.Replace("[NB_PUESTO]", vRequisicion.NB_PUESTO);
            vMensajeCorreo = vMensajeCorreo.Replace("[DS_MOTIVO]", vDsComentarios);
            vMensajeCorreo = vMensajeCorreo.Replace("[CL_REQUISICION]", vRequisicion.NO_REQUISICION);

            E_RESULTADO vResultado = nRequisicion.ActualizaEstatusRequisicion(true, false, pIdRequisicion, vRequisicion.ID_PUESTO.Value, "", txtObservacionesRequisicion.Text, "", vClEstatusRequisicion,vNbPrograma, vClUsuario);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                builder.Append(vNbCorreo + ";");
                EnvioCorreo(builder.ToString(), vMensajeCorreo, Asunto);
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "OnCloseWindow");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensajeError, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
            }
        }

        private void GuardarDatosPuestoRequisicion()
        {
            RequisicionNegocio nRequisicion = new RequisicionNegocio();
            EmpleadoNegocio nEmpleado = new EmpleadoNegocio();

            string vClEstatusPuesto = btnAutorizar.Checked ? "AUTORIZADO" : "RECHAZADO";
            string vClEstatusRequisicion = btnAutorizarReq.Checked ? "AUTORIZADO" : "RECHAZADO";

            string vMensajeCorreo = "";
            string vNbCorreo = "";
            string nbCreaRequisicion = "";
            string vDsComentariosRequisicion = txtObservacionesRequisicion.Text;
            string vDsComentariosPuesto = rtbObservacionesPuesto.Text;
            string Asunto = "Actualización de requisición y puesto";

            var vRequisicion = nRequisicion.ObtieneRequisicion(pIdRequisicion: pIdRequisicion).FirstOrDefault();
           // var vSolicitante = nEmpleado.ObtenerEmpleado(ID_EMPLEADO: vRequisicion.ID_SOLICITANTE).FirstOrDefault();

           // if (vSolicitante != null)
            if (vRequisicion != null)
            {
                //nbCreaRequisicion = vSolicitante.NB_EMPLEADO_COMPLETO;
                //vNbCorreo = vSolicitante.CL_CORREO_ELECTRONICO;
                nbCreaRequisicion = vRequisicion.NB_EMPLEADO_SOLICITANTE;
                vNbCorreo = vRequisicion.NB_CORREO_SOLICITANTE;
            }

            vMensajeCorreo = ContextoApp.IDP.NotificacionRrhh.dsEstatusReqPuesto.dsMensaje;
            vMensajeCorreo = vMensajeCorreo.Replace("[NB_CREA_REQUISICION]", nbCreaRequisicion);
            vMensajeCorreo = vMensajeCorreo.Replace("[NB_ESTATUS_REQUISICION]", vClEstatusRequisicion);
            vMensajeCorreo = vMensajeCorreo.Replace("[NB_PUESTO]", vRequisicion.NB_PUESTO);
            vMensajeCorreo = vMensajeCorreo.Replace("[NB_ESTATUS_REQUISICION]", vClEstatusRequisicion);
            vMensajeCorreo = vMensajeCorreo.Replace("[DS_COMENTARIOS_REQUISICION]", vDsComentariosRequisicion);
            vMensajeCorreo = vMensajeCorreo.Replace("[NB_ESTATUS_PUESTO]", vClEstatusPuesto);
            vMensajeCorreo = vMensajeCorreo.Replace("[DS_COMENTARIOS_PUESTO]", vDsComentariosPuesto);
            vMensajeCorreo = vMensajeCorreo.Replace("[CL_REQUISICION]", vRequisicion.NO_REQUISICION);

            E_RESULTADO vResultado = nRequisicion.ActualizaEstatusRequisicion(true, true, pIdRequisicion, vRequisicion.ID_PUESTO.Value, rtbObservacionesPuesto.Text, txtObservacionesRequisicion.Text, vClEstatusPuesto,vClEstatusRequisicion,vNbPrograma, vClUsuario);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                builder.Append(vNbCorreo + ";");
                EnvioCorreo(builder.ToString(), vMensajeCorreo, Asunto);
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "OnCloseWindow");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensajeError, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
            }


        }

        private void GuardarDatos()
        {

            if (vClProceso.Equals("NOTIFICACIONRRHH"))
            {
                if (!btnAutorizar.Checked & !btnRechazar.Checked)
                {
                    UtilMensajes.MensajeResultadoDB(rnMensajeError, "Indique el estatus del puesto", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                    return;
                }

                if (btnRechazar.Checked == true & rtbObservacionesPuesto.Text == "")
                {
                    UtilMensajes.MensajeResultadoDB(rnMensajeError, "Indique el motivo del rechazo", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                    return;
                }


                GuardarDatosPuesto();
            }

            if (vClProceso.Equals("AUTORIZAREQUISICION"))
            {
                if (!btnAutorizarReq.Checked & !btnRechazarReq.Checked)
                {
                    UtilMensajes.MensajeResultadoDB(rnMensajeError, "Indique el estatus de la requisición", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                    return;
                }

                if (btnRechazarReq.Checked == true & txtObservacionesRequisicion.Text == "")
                {
                    UtilMensajes.MensajeResultadoDB(rnMensajeError, "Indique el motivo del rechazo", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                    return;
                }

                GuardarDatosRequisicion();
            }

            if (vClProceso.Equals("AUTORIZAREQPUESTO"))
            {

                if (!btnAutorizar.Checked & !btnRechazar.Checked)
                {
                    UtilMensajes.MensajeResultadoDB(rnMensajeError, "Indique el estatus del puesto", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                    return;
                }

                if (!btnAutorizarReq.Checked & !btnRechazarReq.Checked)
                {
                    UtilMensajes.MensajeResultadoDB(rnMensajeError, "Indique el estatus de la requisición", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                    return;
                }

                if (btnRechazarReq.Checked == true & txtObservacionesRequisicion.Text == "")
                {
                    UtilMensajes.MensajeResultadoDB(rnMensajeError, "Indique el motivo del rechazo", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                    return;
                }
                if (btnRechazar.Checked == true & rtbObservacionesPuesto.Text == "")
                {
                    UtilMensajes.MensajeResultadoDB(rnMensajeError, "Indique el motivo del rechazo", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                    return;
                }


                GuardarDatosPuestoRequisicion();

            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = (ContextoUsuario.oUsuario != null) ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO";
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                pEstatus = "";
                pCausa = "";
                pIdRequisicion = Convert.ToInt32(Request.Params["IdRequisicion"]);
                vClProceso = Request.Params["ClProceso"].ToString();
                CargarDatos();

            }
        }

        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(rtbDsCausa.Text))
            //{
            //    UtilMensajes.MensajeResultadoDB(rnMensajeError, "Indique la cuasa del rechazo de la requisición", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            //    return;
            //}

            //if (pEstatus != "AUTORIZADO" && pEstatus != "RECHAZADO")
            //{
            //    RequisicionNegocio Rnegocio = new RequisicionNegocio();
            //    E_RESULTADO vResultado = Rnegocio.ActualizaEstatusRequisicion(pIdRequisicion,rtbDsCausa.Text, vClUsuario, vNbPrograma, "R");
            //    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            //    if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            //    {
            //        UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "OnCloseWindow");
            //    }
            //    else
            //    {
            //        UtilMensajes.MensajeResultadoDB(rnMensajeError, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
            //    }
            //}
            //else
            //{
            //    UtilMensajes.MensajeResultadoDB(rnMensajeError, "Esta requisición ya tiene un estatus " + pEstatus, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "OnCloseWindow");
            //}
        }

        protected void btnAutorizar_Click(object sender, EventArgs e)
        {
            //if (pEstatus != "AUTORIZADO" && pEstatus != "RECHAZADO")
            //{
            //    RequisicionNegocio Rnegocio = new RequisicionNegocio();
            //    E_RESULTADO vResultado = Rnegocio.ActualizaEstatusRequisicion(pIdRequisicion, rtbDsCausa.Text, vClUsuario, vNbPrograma, "A");
            //    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            //    if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            //    {
            //        UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "OnCloseWindow");
            //    }

            //    else
            //    {
            //        UtilMensajes.MensajeResultadoDB(rnMensajeError, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
            //    }
            //}
            //else
            //{
            //    UtilMensajes.MensajeResultadoDB(rnMensajeError, "Esta requisición ya tiene un estatus " + pEstatus, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "OnCloseWindow");
            //}
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarDatos();           
        }
    }
}