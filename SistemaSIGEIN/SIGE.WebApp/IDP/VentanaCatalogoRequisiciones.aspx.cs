using WebApp.Comunes;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.WebApp.Comunes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;
using SIGE.Negocio.Utilerias;
using System.Net;
using SIGE.Negocio.IntegracionDePersonal;
using System.Text;
using System.Web.Security;
using System.Net.Mail;

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaCatalogoRequisiciones : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private string vNbUsuario;


        StringBuilder builder = new StringBuilder();
        string Email { set; get; }
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private string vClEstadoMail
        {
            get { return (string)ViewState["vsClEstadoMail"]; }
            set { ViewState["vsClEstadoMail"] = value; }
        }

        public int? vIdRequisicion
        {
            get { return (int?)ViewState["vs_vcr_id_requisicion"]; }
            set { ViewState["vs_vcr_id_requisicion"] = value; }
        }

        public int? vIdPuesto
        {
            get { return (int?)ViewState["vsID_PUESTO"]; }
            set { ViewState["vsID_PUESTO"] = value; }
        }

        private string pClEstatusRequisicion
        {
            get { return (string)ViewState["vsESTADO"]; }
            set { ViewState["vsESTADO"] = value; }
        }

        private string pEstadoPuesto
        {
            get { return (string)ViewState["vsEstadoPuesto"]; }
            set { ViewState["vsEstadoPuesto"] = value; }
        }

        private DateTime? pFechaPuesto
        {
            get { return (DateTime?)ViewState["pFechaPuesto"]; }
            set { ViewState["pFechaPuesto"] = value; }
        }

        public int? vIdNotificacion
        {
            get { return (int?)ViewState["vIdNotificacion"]; }
            set { ViewState["vIdNotificacion"] = value; }
        }

        private string pTipoTransaccion
        {
            get { return (string)ViewState["vstipo"]; }
            set { ViewState["vstipo"] = value; }
        }

        private string clTokenRequisicion
        {
            get { return (string)ViewState["clToken"]; }
            set { ViewState["clToken"] = value; }
        }

        private string clTokenPuesto
        {
            get { return (string)ViewState["clTokenPuesto"]; }
            set { ViewState["clTokenPuesto"] = value; }
        }

        private Guid flRequisicion
        {
            get { return (Guid)ViewState["flRequisicion"]; }
            set { ViewState["flRequisicion"] = value; }
        }

        private Guid flNotificacion
        {
            get { return (Guid)ViewState["flNotificacion"]; }
            set { ViewState["flNotificacion"] = value; }
        }

        private int vIdAutoriza
        {
            get { return (int)ViewState["vIdAutoriza"]; }
            set { ViewState["vIdAutoriza"] = value; }
        }

        //private SPE_OBTIENE_SUELDO_PROMEDIO_PUESTO_Result vSueldo
        //{
        //    get { return (SPE_OBTIENE_SUELDO_PROMEDIO_PUESTO_Result)ViewState["vsVsueldo"]; }
        //    set { ViewState["vsVsueldo"] = value; }
        //}

        //private bool vFgEdicionDescriptivo
        //{
        //    get { return (bool)ViewState["vs_vcr_fg_edicion_descriptivo"]; }
        //    set { ViewState["vs_vcr_fg_edicion_descriptivo"] = value; }
        //}

        #endregion

        #region Funciones

        public void EnvioCorreo(string Email, string Mensaje, string Asunto)
        {
            Mail mail = new Mail(ContextoApp.mailConfiguration);
            mail.addToAddress(Email, "");
            RadProgressContext progress = RadProgressContext.Current;
            mail.Send(Asunto, String.Format("{0}", Mensaje));
        }

        public void SeguridadProcesos()
        {
            btnGuardarCatalogo.Enabled = ContextoUsuario.oUsuario.TienePermiso("J.A.A.F");
        }

        public void CargarDatos()
        {
            RequisicionNegocio nRequisicion = new RequisicionNegocio();
            var vRequisicion = nRequisicion.ObtieneRequisicion(pIdRequisicion: vIdRequisicion).FirstOrDefault();

            txtNo_requisicion.Text = vRequisicion.NO_REQUISICION;
            Fe_solicitud.SelectedDate = vRequisicion.FE_SOLICITUD;
            Fe_Requerimiento.SelectedDate = vRequisicion.FE_REQUERIMIENTO;

            rdpAutorizacion.SelectedDate = vRequisicion.FE_AUTORIZA_REQUISICION;

            EstatusControles(vRequisicion.CL_CAUSA);
            txtTiempoCausa.Text = vRequisicion.DS_TIEMPO_CAUSA;

            cmbCausas.SelectedValue = vRequisicion.CL_CAUSA;
            txtDescripcionCausa.Text = vRequisicion.DS_CAUSA;
            pClEstatusRequisicion = vRequisicion.CL_ESTATUS_REQUISICION;
            pEstadoPuesto = vRequisicion.CL_ESTATUS_PUESTO;


            if (vRequisicion.FL_NOTIFICACION != null)
            {
                flNotificacion = (Guid)vRequisicion.FL_NOTIFICACION;
                clTokenPuesto = vRequisicion.CL_TOKEN_PUESTO;
            }


            rlbPuesto.Items.Clear();
            rlbPuesto.Items.Add(new RadListBoxItem(vRequisicion.NB_PUESTO, vRequisicion.ID_PUESTO.ToString()));
            pFechaPuesto = vRequisicion.FE_MODIFICACION;

            if (vRequisicion.CL_CAUSA == "NUEVO PUESTO" & vRequisicion.CL_ESTATUS_PUESTO == "EN REVISION" || vRequisicion.CL_CAUSA == "NUEVO PUESTO" & vRequisicion.CL_ESTATUS_PUESTO == "RECHAZADO")
            {
                radBtnBuscarPuesto.Enabled = false;
                btnEliminarPuestoObjetivo.Enabled = false;
                btnVistaPrevia.Enabled = false;
                btnNuevoPuesto.Visible = true;
                divAutorizaPuestoReq.Style.Add("display", "block");

                if (vRequisicion.NB_EMPLEADO_AUTORIZA_PUESTO != null)
                {
                    //rlbPuestoReq.Items.Clear();
                    //rlbPuestoReq.Items.Add(new RadListBoxItem(vRequisicion.NB_EMPLEADO_AUTORIZA_PUESTO, vRequisicion.ID_EMPLEADO_AUTORIZA_PUESTO.Value.ToString()));
                    txtPuestoReq.Text = vRequisicion.NB_EMPLEADO_AUTORIZA_PUESTO;
                    txtPuestoAutorizaPuesto.Text = vRequisicion.NB_EMPLEADO_AUTORIZA_PUESTO_PUESTO;
                    txtPuestoAutorizaCorreo.Text = vRequisicion.NB_CORREO_AUTORIZA_PUESTO;
                }

            }
            else
            {
                radBtnBuscarPuesto.Enabled = true;
                btnEliminarPuestoObjetivo.Enabled = true;
                btnVistaPrevia.Enabled = true;
                btnNuevoPuesto.Visible = false;
                divAutorizaPuestoReq.Style.Add("display", "none");
            }

            rlbSolicitante.Items.Clear();
            rlbSolicitante.Items.Add(new RadListBoxItem(vRequisicion.NB_EMPLEADO_SOLICITANTE, vRequisicion.ID_SOLICITANTE.ToString()));
            txtPuestoSolicitante.Text = vRequisicion.NB_PUESTO_SOLICITANTE;
            txtCorreoSolicitante.Text = vRequisicion.NB_CORREO_SOLICITANTE;

            pTipoTransaccion = E_TIPO_OPERACION_DB.A.ToString();

            txtSueldo.Text = vRequisicion.MN_SUELDO.ToString();
            txtSueldoSugerido.Text = vRequisicion.MN_SUELDO_TABULADOR.ToString();
            txtSueldoMin.Text = vRequisicion.MN_SUELDO_SUGERIDO.ToString();
            txtSueldoMax.Text = vRequisicion.MAX_SUELDO_SUGERIDO.ToString();

            Fe_Requerimiento.SelectedDate = vRequisicion.FE_REQUERIMIENTO;
            Fe_solicitud.SelectedDate = vRequisicion.FE_SOLICITUD;

            if (vRequisicion.ID_AUTORIZA != null)
            {
                lstAutoriza.Items.Clear();
                lstAutoriza.Items.Add(new RadListBoxItem(vRequisicion.NB_EMPLEADO_AUTORIZA, vRequisicion.ID_AUTORIZA.ToString()));
                txtPuestoAutoriza.Text = vRequisicion.NB_PUESTO_AUTORIZA;
                txtCorreoAutorizaReq.Text = vRequisicion.NB_CORREO_AUTORIZA;

                if (vRequisicion.FL_REQUISICION.HasValue)
                {
                    flRequisicion = vRequisicion.FL_REQUISICION.Value;
                    clTokenRequisicion = vRequisicion.CL_TOKEN;
                }
            }

            if (vRequisicion.ID_EMPLEADO_SUPLENTE != null)
            {
                rlbSuplente.Items.Clear();
                rlbSuplente.Items.Add(new RadListBoxItem(vRequisicion.NB_EMPLEADO_SUPLENTE, vRequisicion.ID_EMPLEADO_SUPLENTE.ToString()));
            }

            txtObservaciones.Content = vRequisicion.DS_COMENTARIOS;
            vIdPuesto = (int)vRequisicion.ID_PUESTO;

            SeguridadProcesos();

            if (vRequisicion.CL_ESTATUS_REQUISICION != "CREADA" && vRequisicion.CL_ESTATUS_REQUISICION != "RECHAZADO")
            {
                cmbCausas.Enabled = false;
                btnGuardarCatalogo.Enabled = false;
                btnNuevoPuesto.Enabled = false;
            }

            if (((vRequisicion.FL_REQUISICION != null || vRequisicion.FL_REQUISICION != Guid.Empty) & vRequisicion.CL_ESTATUS_REQUISICION != "AUTORIZADO" & vRequisicion.ID_AUTORIZA != null) || (vRequisicion.FL_NOTIFICACION != null & vRequisicion.CL_ESTATUS_PUESTO != "AUTORIZADO"))
            {
                btnReenviarAutorizaciones.Visible = true;
            }
            else
            {
                btnReenviarAutorizaciones.Visible = false;
            }

        }

        public void EstatusControles(string pClEstatus)
        {
            if (pClEstatus.Equals("OTRA"))
            {
                lblDescripcionCausa.Style.Add("display", "block");
                lblDescripcionCausa.InnerText = "Causa de otra:";
                txtDescripcionCausa.Visible = true;
                radBtnBuscarPuesto.ToolTip = "";


                lblTiempoCausa.Style.Add("display", "none");
                divUltimoSueldo.Style.Add("display", "block");
                dvSueldoSugerido.Style.Add("display", "block");
                txtTiempoCausa.Visible = false;

                btnNuevoPuesto.Visible = false;
                radBtnBuscarPuesto.Enabled = true;
                btnVistaPrevia.Enabled = true;
                btnEliminarPuestoObjetivo.Enabled = true;

                //lblEmpleadoSuplir.Visible = false;
                //rlbSuplente.Visible = false;
                //btnBuscarSuplente.Visible = false;
                //btnEliminaSuplente.Visible = false;
                divEmpleadoSuplir.Style.Add("display", "none");

                lblPuesto.InnerText = "Puesto a cubrir";
                divAutorizaPuestoReq.Style.Add("display", "none");

            }

            if (pClEstatus.Equals("NUEVO PUESTO"))
            {
                btnNuevoPuesto.Visible = true;
                lblPuesto.InnerText = "Si ya has creado el nuevo puesto, selecciónalo";

                lblTiempoCausa.Style.Add("display", "none");
                txtTiempoCausa.Visible = false;
                radBtnBuscarPuesto.ToolTip = "Aquí puedes seleccionar el nuevo puesto si es que ya lo creaste. Recuerda que el puesto que selecciones no aparecerá en el catálogo de descriptivos de puestos hasta que sea aprobado.";

                if (vIdRequisicion != null)
                {
                    btnNuevoPuesto.Text = "Editar nuevo puesto";
                }
                else
                {
                    btnNuevoPuesto.Text = "Crear nuevo puesto";
                }

                divAutorizaPuestoReq.Style.Add("display", "none");
                lblDescripcionCausa.Style.Add("display", "none");
                txtDescripcionCausa.Visible = false;
                radBtnBuscarPuesto.Enabled = true;

                //lblEmpleadoSuplir.Visible = false;
                //rlbSuplente.Visible = false;
                //btnBuscarSuplente.Visible = false;
                //btnEliminaSuplente.Visible = false;

                divEmpleadoSuplir.Style.Add("display", "none");
                divUltimoSueldo.Style.Add("display", "block");
                dvSueldoSugerido.Style.Add("display", "block");
                radBtnBuscarPuesto.Enabled = true;
                btnEliminarPuestoObjetivo.Enabled = true;
            }

            if (pClEstatus.Equals("TEMPORAL"))
            {
                btnNuevoPuesto.Visible = false;
                radBtnBuscarPuesto.ToolTip = "";
                radBtnBuscarPuesto.Enabled = true;

                divUltimoSueldo.Style.Add("display", "block");
                dvSueldoSugerido.Style.Add("display", "block");

                lblDescripcionCausa.Style.Add("display", "block");
                txtDescripcionCausa.Visible = true;
                lblDescripcionCausa.InnerText = "Causa de temporal:";

                lblTiempoCausa.Style.Add("display", "block");
                txtTiempoCausa.Visible = true;

                //lblEmpleadoSuplir.Visible = false;
                //rlbSuplente.Visible = false;
                //btnBuscarSuplente.Visible = false;
                //btnEliminaSuplente.Visible = false;

                divEmpleadoSuplir.Style.Add("display", "none");

                radBtnBuscarPuesto.Enabled = true;
                btnEliminarPuestoObjetivo.Enabled = true;

                lblPuesto.InnerText = "Puesto a cubrir";
                divAutorizaPuestoReq.Style.Add("display", "none");
            }

            if (pClEstatus.Equals("SUPLENCIA") || pClEstatus.Equals("VACANTE"))
            {
                //lblEmpleadoSuplir.Visible = true;
                //rlbSuplente.Visible = true;
                //btnBuscarSuplente.Visible = true;
                //btnEliminaSuplente.Visible = true;
                divUltimoSueldo.Style.Add("display", "block");
                divEmpleadoSuplir.Style.Add("display", "block");
                radBtnBuscarPuesto.ToolTip = "";


                radBtnBuscarPuesto.Enabled = false;
                btnEliminarPuestoObjetivo.Enabled = false;
                btnNuevoPuesto.Visible = false;
                lblPuesto.InnerText = "Puesto a cubrir";

                if (pClEstatus.Equals("VACANTE"))
                {
                    lblDescripcionCausa.Style.Add("display", "none");
                    txtDescripcionCausa.Visible = false;

                    lblEmpleadoSuplir.InnerText = "Empleado que deja posición vacante";
                    lblUltimoSueldo.InnerText = "Ultimo Sueldo";
                    divUltimoSueldo.Style.Add("display", "block");
                    dvSueldoSugerido.Style.Add("display", "none");
                }
                else
                {
                    lblDescripcionCausa.Style.Add("display", "block");
                    lblDescripcionCausa.InnerText = "Causa de suplencia:";
                    txtDescripcionCausa.Visible = true;
                    dvSueldoSugerido.Style.Add("display", "none");

                    lblEmpleadoSuplir.InnerText = "Empleado a suplir";
                    lblTiempoCausa.Style.Add("display", "block");
                    txtTiempoCausa.Visible = true;
                }

                divAutorizaPuestoReq.Style.Add("display", "none");

            }

            //if (pClEstatus.Equals("NUEVO PUESTO"))
            //{
            //    radBtnBuscarPuesto.Enabled = false;
            //}

        }

        public void SolicitarAutorizacionRequisicion(E_REQUISICION vRequisicion)
        {

            //Mail mail = new Mail(ContextoApp.mailConfiguration);
            EmpleadoNegocio nEmpleado = new EmpleadoNegocio();
            string vNbPuesto;
            string vNbSolicitante;

            vIdAutoriza = vRequisicion.ID_AUTORIZA.Value;
            vNbPuesto = vRequisicion.NB_PUESTO;
            vNbSolicitante = vRequisicion.NB_EMPLEADO_SOLICITANTE;

            //var vUsuarioInfo = nEmpleado.Obtener_M_EMPLEADO(ID_EMPLEADO: vIdAutoriza).FirstOrDefault();
            //if (vUsuarioInfo != null)
            //{
            string Asunto = "Autorización de requisición";

            if (string.IsNullOrEmpty(txtCorreoAutorizaReq.Text))
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "El empleado que autoriza la requisición no tiene un correo electrónico. Actualiza el inventario de personal antes de envíar la solicitud de autorización", E_TIPO_RESPUESTA_DB.WARNING);
                return;
            }

            if (pClEstatusRequisicion == "AUTORIZADO")
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "La requisición ya está autorizada.", E_TIPO_RESPUESTA_DB.WARNING);
                return;
            }

            try
            {
                string myUrl = ResolveUrl("~/Logon.aspx?FlProceso=");
                string vUrl = ContextoUsuario.nbHost + myUrl + flRequisicion.ToString() + "&ClProceso=" + "AUTORIZAREQUISICION";
                string vMensajeCorreo = ContextoApp.IDP.NotificacionRrhh.dsAutorizadorRequisicion.dsMensaje;
                vMensajeCorreo = vMensajeCorreo.Replace("[NB_NOTIFICAR]", vRequisicion.NB_EMPLEADO_AUTORIZA);
                vMensajeCorreo = vMensajeCorreo.Replace("[NB_CREA_REQUISICION]", vNbSolicitante);
                vMensajeCorreo = vMensajeCorreo.Replace("[NB_PUESTO]", vNbPuesto);
                vMensajeCorreo = vMensajeCorreo.Replace("[URL]", vUrl);
                vMensajeCorreo = vMensajeCorreo.Replace("[CONTRASENA]", clTokenRequisicion);
                builder.Append(txtCorreoAutorizaReq.Text + ";");
                EnvioCorreo(builder.ToString(), vMensajeCorreo, Asunto);

                pClEstatusRequisicion = "POR AUTORIZAR";
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Solicitud de autorización enviada. Guarda la requisición.", E_TIPO_RESPUESTA_DB.SUCCESSFUL, pCallBackFunction: "");
            }
            catch (Exception)
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Envío no procesado", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "");
            }
            //}
        }

        public void SolicitaAutorizacionPuesto(E_REQUISICION vRequisicion)
        {
            // MailAddress lstCorreosRequisiciones = ContextoApp.IDP.NotificacionRrhh.lstCorreosRequisiciones;

            if (!string.IsNullOrEmpty(vRequisicion.NB_CORREO_AUTORIZA_PUESTO))
            {
                //foreach (var item in lstCorreosRequisiciones)
                // {
                var correo = "";
                var autoriza = "";
                correo = vRequisicion.NB_CORREO_AUTORIZA_PUESTO;
                autoriza = vRequisicion.NB_EMPLEADO_AUTORIZA_PUESTO;
                string Asunto = "Autorización para puesto de requisición";

                try
                {
                    string vMensajeCorreo = "";
                    string myUrl = ResolveUrl("~/Logon.aspx?FlProceso=");
                    string vUrl = ContextoUsuario.nbHost + myUrl + flNotificacion.ToString() + "&ClProceso=" + "NOTIFICACIONRRHH";
                    if (pFechaPuesto == null)
                    {
                        vMensajeCorreo = ContextoApp.IDP.NotificacionRrhh.dsCreadorPuesto.dsMensaje;
                    }
                    else
                    {
                        vMensajeCorreo = ContextoApp.IDP.NotificacionRrhh.dsReenvioPuesto.dsMensaje;
                    }
                    vMensajeCorreo = vMensajeCorreo.Replace("[NB_NOTIFICAR]", autoriza);
                    vMensajeCorreo = vMensajeCorreo.Replace("[NB_CREA_REQUISICION]", vRequisicion.NB_EMPLEADO_SOLICITANTE);
                    vMensajeCorreo = vMensajeCorreo.Replace("[NB_PUESTO]", vRequisicion.NB_PUESTO);
                    vMensajeCorreo = vMensajeCorreo.Replace("[URL]", vUrl);
                    vMensajeCorreo = vMensajeCorreo.Replace("[CONTRASENA]", clTokenPuesto);
                    EnvioCorreo(correo, vMensajeCorreo, Asunto);

                    //UtilMensajes.MensajeResultadoDB(rnMensaje, "Solicitud de autorización enviada", E_TIPO_RESPUESTA_DB.SUCCESSFUL);

                }
                catch (Exception)
                {
                    UtilMensajes.MensajeResultadoDB(rnMensaje, "Envío no procesado", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "onCloseWindow");
                }

                // }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "No hay correo asignado para enviar la notificación", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            }
        }

        public void SolicitarAutorizacionRequisicionPuesto(E_REQUISICION vRequisicion)
        {
            //MailAddress lstCorreosRequisiciones = ContextoApp.IDP.NotificacionRrhh.lstCorreosRequisiciones;

            if (!string.IsNullOrEmpty(vRequisicion.NB_CORREO_AUTORIZA_PUESTO))
            {
                //foreach (var item in lstCorreosRequisiciones)
                // {
                var correo = "";
                var autoriza = "";
                correo = vRequisicion.NB_CORREO_AUTORIZA_PUESTO;
                autoriza = vRequisicion.NB_EMPLEADO_AUTORIZA_PUESTO;
                string Asunto = "Autorización de puesto nuevo y requisicion";

                try
                {
                    string vMensajeCorreo = "";
                    string myUrl = ResolveUrl("~/Logon.aspx?FlProceso=");
                    string vUrl = ContextoUsuario.nbHost + myUrl + flRequisicion + "&ClProceso=AUTORIZAREQPUESTO";

                    if (vIdRequisicion == null)
                    {
                        vMensajeCorreo = ContextoApp.IDP.NotificacionRrhh.dsNotificarReqPuesto.dsMensaje;
                    }
                    else
                    {
                        vMensajeCorreo = ContextoApp.IDP.NotificacionRrhh.dsReenvioReqPuesto.dsMensaje;
                    }

                    vMensajeCorreo = vMensajeCorreo.Replace("[NB_NOTIFICAR]", autoriza);
                    vMensajeCorreo = vMensajeCorreo.Replace("[NB_CREA_REQUISICION]", vRequisicion.NB_EMPLEADO_SOLICITANTE);
                    vMensajeCorreo = vMensajeCorreo.Replace("[NB_PUESTO]", vRequisicion.NB_PUESTO);
                    vMensajeCorreo = vMensajeCorreo.Replace("[URL]", vUrl);
                    vMensajeCorreo = vMensajeCorreo.Replace("[CONTRASENA]", vRequisicion.CL_TOKEN);
                    EnvioCorreo(correo, vMensajeCorreo, Asunto);
                    pClEstatusRequisicion = "POR AUTORIZAR";
                    UtilMensajes.MensajeResultadoDB(rnMensaje, "Solicitud de autorización enviada. Guarda la requisición.", E_TIPO_RESPUESTA_DB.SUCCESSFUL, pCallBackFunction: "");

                }
                catch (Exception)
                {
                    UtilMensajes.MensajeResultadoDB(rnMensaje, "Envío no procesado", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "onCloseWindow");
                }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "No hay correo asignado para enviar la notificación", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            }
        }

        public void ValidarEnvioCorreos(E_REQUISICION vRequisicion)
        {
            PuestoNegocio nPuesto = new PuestoNegocio();
            var vPuesto = nPuesto.ObtienePuestosRequisicion(pIdPuesto: vRequisicion.ID_PUESTO).FirstOrDefault();

            bool vFgRequireAutorizacionRequisicion = false;
            bool vFgRequiereAutorizacionPuesto = false;
            bool vFgAutorizaReqPuesto = false;

            if (vPuesto != null)
            {
                vFgRequiereAutorizacionPuesto = (vPuesto.CL_ESTATUS != "AUTORIZADO" & vRequisicion.CL_CAUSA == "NUEVO PUESTO");
            }
            vFgRequireAutorizacionRequisicion = (vRequisicion.ID_AUTORIZA != null);

            if (!string.IsNullOrEmpty(vRequisicion.NB_CORREO_AUTORIZA) & !string.IsNullOrEmpty(vRequisicion.NB_CORREO_AUTORIZA_PUESTO))
            {
                vFgAutorizaReqPuesto = ((vRequisicion.NB_CORREO_AUTORIZA.Equals(vRequisicion.NB_CORREO_AUTORIZA_PUESTO)) & vFgRequiereAutorizacionPuesto & vFgRequireAutorizacionRequisicion);
            }
            else
            {
                vFgAutorizaReqPuesto = false;
            }

            if (vFgAutorizaReqPuesto)
            {
                SolicitarAutorizacionRequisicionPuesto(vRequisicion);
            }
            else
            {
                if (vFgRequiereAutorizacionPuesto)
                {
                    SolicitaAutorizacionPuesto(vRequisicion);
                }

                if (vFgRequireAutorizacionRequisicion)
                {
                    SolicitarAutorizacionRequisicion(vRequisicion);
                }
            }

        }

        private bool ValidarDatosFormulario(E_REQUISICION vRequisicion)
        {
            if (vRequisicion.FE_SOLICITUD == null)
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Ingresa la fecha de la requisición", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return false;
            }

            if (vRequisicion.FE_REQUERIMIENTO == null)
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Ingresa la fecha en el que se requiere", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return false;
            }

            if ((vRequisicion.CL_CAUSA == "SUPLENCIA" || vRequisicion.CL_CAUSA == "VACANTE") && (rlbSuplente.Items[0].Value == "" || rlbSuplente.Items[0].Value == null || rlbSuplente.Items[0].Value == "0"))
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Selecciona el empleado a suplir", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return false;
            }

            if (vRequisicion.CL_CAUSA != "NUEVO PUESTO" && (rlbPuesto.Items[0].Value == "" || rlbPuesto.Items[0].Value == null || rlbPuesto.Items[0].Value == "0"))
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Selecciona el puesto a cubrir", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return false;
            }

            if (cmbCausas.SelectedValue == "")
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Seleccione una causa", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return false;
            }

            if (vRequisicion.CL_CAUSA == "OTRA" && txtDescripcionCausa.Text == "")
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Especifique la causa de otra", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return false;
            }

            if (vRequisicion.ID_SOLICITANTE == null)
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Especifique la persona que solicita la requisición.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return false;
            }

            if (vRequisicion.ID_SOLICITANTE != null & string.IsNullOrEmpty(vRequisicion.NB_CORREO_SOLICITANTE))
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Especifique el correo de la persona que solicita la requisición.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return false;
            }

            if (vRequisicion.ID_AUTORIZA != null & string.IsNullOrEmpty(vRequisicion.NB_CORREO_AUTORIZA))
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Especifique el correo de la persona que autoriza la requisición.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return false;
            }

            DescriptivoNegocio nDescriptivo = new DescriptivoNegocio();

            E_DESCRIPTIVO vDescriptivo = nDescriptivo.ObtieneDescriptivoRequisicion(vRequisicion.ID_PUESTO);

            if (vDescriptivo != null)
            {

                if (vDescriptivo.ESTATUS.ToUpper().Equals("EN REVISION") & string.IsNullOrEmpty(txtPuestoReq.Text) & string.IsNullOrEmpty(txtPuestoAutorizaPuesto.Text) & string.IsNullOrEmpty(txtPuestoAutorizaCorreo.Text))
                {
                    UtilMensajes.MensajeResultadoDB(rnMensaje, "Seleccione o escriba los datos de la persona que autoriza el puesto creado desde la requisición.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                    return false;
                }

                if (vDescriptivo.ESTATUS.ToUpper().Equals("EN REVISION") & string.IsNullOrEmpty(txtPuestoAutorizaCorreo.Text))
                {
                    UtilMensajes.MensajeResultadoDB(rnMensaje, "Especifique el correo de la persona que autoriza el puesto creado desde la requisición.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                    return false;
                }
            }

            return true;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            CatalogoListaNegocio nlista = new CatalogoListaNegocio();
            RequisicionNegocio nRequisicion = new RequisicionNegocio();

            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
            vNbUsuario = ContextoUsuario.oUsuario.NB_USUARIO;

            if (!IsPostBack)
            {
                vIdAutoriza = 0;
                flRequisicion = Guid.Empty;
                clTokenRequisicion = "";
                pClEstatusRequisicion = "CREADA";
                hfRevisoDescriptivo.Value = "0";

                lblDescripcionCausa.Style.Add("display", "none");
                txtDescripcionCausa.Visible = false;

                lblTiempoCausa.Style.Add("display", "none");
                txtTiempoCausa.Visible = false;

                divAutorizaPuestoReq.Style.Add("display", "none");
                divEmpleadoSuplir.Style.Add("display", "none");

                vIdRequisicion = null;

                Fe_solicitud.SelectedDate = DateTime.Now;
                Fe_Requerimiento.SelectedDate = DateTime.Now;
                radBtnBuscarPuesto.Enabled = false;

                var vCatalogoVacantes = nlista.ObtieneCatalogoLista(pIdCatalogoLista: ContextoApp.IdCatalogoCausaRequisicion).FirstOrDefault();
                if (vCatalogoVacantes != null)
                {
                    CatalogoValorNegocio nvalor = new CatalogoValorNegocio();
                    var vCausas = nvalor.Obtener_C_CATALOGO_VALOR(ID_CATALOGO_LISTA: vCatalogoVacantes.ID_CATALOGO_LISTA);

                    if (vCausas != null)
                    {
                        cmbCausas.DataSource = vCausas;
                        cmbCausas.DataTextField = "NB_CATALOGO_VALOR";
                        cmbCausas.DataValueField = "CL_CATALOGO_VALOR";
                        cmbCausas.DataBind();
                    }
                }

                if (Request.Params["RequisicionId"] != null)
                {
                    vIdRequisicion = int.Parse(Request.Params["RequisicionId"]);
                    rdpAutorizacion.Visible = true;
                    lbAutoriza.Visible = true;
                    CargarDatos();
                }
                else
                {
                    txtNo_requisicion.Text = nRequisicion.ObtieneNumeroRequisicion();
                    vIdPuesto = null;
                    vIdRequisicion = null;
                    pEstadoPuesto = null;
                    pFechaPuesto = null;
                }
            }
        }

        protected void btnSave_click(object sender, EventArgs e)
        {
            RequisicionNegocio nRequisicion = new RequisicionNegocio();
            E_REQUISICION vRequisicion = new E_REQUISICION();

            vRequisicion.ID_EMPRESA = ((vIdEmpresa != null) ? (vIdEmpresa) : null);
            vRequisicion.NO_REQUISICION = txtNo_requisicion.Text;
            vRequisicion.FE_SOLICITUD = Fe_solicitud.SelectedDate;
            vRequisicion.FE_REQUERIMIENTO = Fe_Requerimiento.SelectedDate;

            vRequisicion.CL_CAUSA = cmbCausas.SelectedValue;
            vRequisicion.DS_CAUSA = txtDescripcionCausa.Text;
            vRequisicion.DS_TIEMPO_CAUSA = txtTiempoCausa.Text;
            vRequisicion.MN_SUELDO = ((txtSueldo.Text != "") ? (Convert.ToDecimal(txtSueldo.Text)) : 0);
            vRequisicion.MN_SUELDO_TABULADOR = ((txtSueldoSugerido.Text != "") ? (Convert.ToDecimal(txtSueldoSugerido.Text)) : 0);
            vRequisicion.MN_SUELDO_SUGERIDO = ((txtSueldoMin.Text != "") ? (Convert.ToDecimal(txtSueldoMin.Text)) : 0);
            vRequisicion.MAX_SUELDO_SUGERIDO = ((txtSueldoMax.Text != "") ? (Convert.ToDecimal(txtSueldoMax.Text)) : 0);
            vRequisicion.DS_COMENTARIOS = txtObservaciones.Content;
            vRequisicion.CL_ESTATUS_REQUISICION = pClEstatusRequisicion;

            /*Se encarga de dar el ID_AUTORIZA y el ID_SOLICITANTE*/
            if (lstAutoriza.Items[0].Text != "No Seleccionado")
            {
                vRequisicion.ID_AUTORIZA = Convert.ToInt32(lstAutoriza.Items[0].Value);
                vRequisicion.NB_EMPLEADO_AUTORIZA = lstAutoriza.Items[0].Text;
                vRequisicion.NB_PUESTO_AUTORIZA = txtPuestoAutoriza.Text;
                vRequisicion.NB_CORREO_AUTORIZA = txtCorreoAutorizaReq.Text;
                vRequisicion.CL_ESTATUS_REQUISICION = "POR AUTORIZAR";

                if (flRequisicion == Guid.Empty)
                {
                    flRequisicion = Guid.NewGuid();
                    clTokenRequisicion = Membership.GeneratePassword(12, 1);
                }

                vRequisicion.FL_REQUISICION = flRequisicion;
                vRequisicion.CL_TOKEN = clTokenRequisicion;
            }

            if (rlbSuplente.Items[0].Text != "No Seleccionado")
            {
                vRequisicion.ID_EMPLEADO_SUPLENTE = Convert.ToInt32(rlbSuplente.Items[0].Value);
                vRequisicion.NB_EMPLEADO_SUPLENTE = rlbSuplente.Items[0].Text;
            }

            if (rlbPuesto.Items[0].Text != "No Seleccionado")
            {
                vRequisicion.ID_PUESTO = Convert.ToInt32(rlbPuesto.Items[0].Value);
                vRequisicion.NB_PUESTO = rlbPuesto.Items[0].Text;
            }

            //if (rlbPuestoReq.Items[0].Text != "No Seleccionado")
            if (!string.IsNullOrEmpty(txtPuestoReq.Text))
            {
                // vRequisicion.ID_EMPLEADO_AUTORIZA_PUESTO = Convert.ToInt32(.Items[0].Value);
                vRequisicion.NB_EMPLEADO_AUTORIZA_PUESTO = txtPuestoReq.Text;
                vRequisicion.NB_EMPLEADO_AUTORIZA_PUESTO_PUESTO = txtPuestoAutorizaPuesto.Text;
                vRequisicion.NB_CORREO_AUTORIZA_PUESTO = txtPuestoAutorizaCorreo.Text;
            }

            if (rlbSolicitante.Items[0].Value != "")
            {
                vRequisicion.ID_SOLICITANTE = int.Parse(rlbSolicitante.Items[0].Value);
                vRequisicion.NB_EMPLEADO_SOLICITANTE = rlbSolicitante.Items[0].Text;
                vRequisicion.NB_PUESTO_SOLICITANTE = txtPuestoSolicitante.Text;
                vRequisicion.NB_CORREO_SOLICITANTE = txtCorreoSolicitante.Text;
            }

            if (vIdRequisicion != null)
            {
                //vIdRequisicion = int.Parse(Request.Params["RequisicionId"]);
                pTipoTransaccion = E_TIPO_OPERACION_DB.A.ToString();
                vRequisicion.ID_REQUISICION = (int)vIdRequisicion;

                if (vRequisicion.CL_CAUSA == "NUEVO PUESTO")
                {
                    vRequisicion.FL_NOTIFICACION = flNotificacion;
                    vRequisicion.CL_TOKEN_PUESTO = clTokenPuesto;
                }
            }
            else
            {
                vRequisicion.ID_REQUISICION = 0;
                pTipoTransaccion = E_TIPO_OPERACION_DB.I.ToString();

                if (vRequisicion.CL_CAUSA == "NUEVO PUESTO")
                {
                    vRequisicion.FL_NOTIFICACION = Guid.NewGuid();
                    flNotificacion = (Guid)vRequisicion.FL_NOTIFICACION;
                    vRequisicion.CL_TOKEN_PUESTO = Membership.GeneratePassword(12, 1);
                    clTokenPuesto = vRequisicion.CL_TOKEN_PUESTO;
                }
            }

            if (vRequisicion != null)
            {

                if (ValidarDatosFormulario(vRequisicion))
                {

                    E_RESULTADO vResultado = nRequisicion.InsertaActualizaRequisicion(pTipoTransaccion: pTipoTransaccion, pNbPrograma: vNbPrograma, pClUsuario: vClUsuario, pRequisicion: vRequisicion);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;


                    if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                    {
                        if (pTipoTransaccion == "I" || pClEstatusRequisicion == "ABIERTA" && vRequisicion.CL_ESTATUS_REQUISICION == "POR AUTORIZAR")
                        {
                            ValidarEnvioCorreos(vRequisicion);
                        }

                        UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
                    }
                    else
                    {
                        UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
                    }
                }
            }
        }

        protected void cmbCausas_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rlbSuplente.Items[0].Text = "No Seleccionado";
            rlbSuplente.Items[0].Value = "0";

            rlbPuesto.Items[0].Text = "No Seleccionado";
            rlbPuesto.Items[0].Value = "0";

            string pvalue = e.Value;

            EstatusControles(pvalue);
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            string pParameter = e.Argument;

            if (pParameter != null)
            {
                vIdPuesto = int.Parse(pParameter);
            }
        }

        protected void btnCancelarCatalogo_Click(object sender, EventArgs e)
        {
            var idPuesto = 0;
            if (rlbPuesto.Items[0].Text != "No Seleccionado" && cmbCausas.SelectedValue == "NUEVO PUESTO" && vIdRequisicion == null)
            {
                idPuesto = Convert.ToInt32(rlbPuesto.Items[0].Value);

                RequisicionNotificaNegocio RequisicionNegocio = new RequisicionNotificaNegocio();
                E_RESULTADO vResultado = RequisicionNegocio.EliminaNotificacion(idPuesto);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
            }

            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Proceso Exitoso", E_TIPO_RESPUESTA_DB.SUCCESSFUL);
            }
        }

        protected void btnEnvioSolicitudAutorizacion_Click(object sender, EventArgs e)
        {
            //SolicitarAutorizacion();
        }

        protected void btnReenviarAutorizaciones_Click(object sender, EventArgs e)
        {
            E_REQUISICION vRequisicion = new E_REQUISICION();

            vRequisicion.ID_EMPRESA = ((vIdEmpresa != null) ? (vIdEmpresa) : null);
            vRequisicion.NO_REQUISICION = txtNo_requisicion.Text;
            vRequisicion.FE_SOLICITUD = Fe_solicitud.SelectedDate;
            vRequisicion.FE_REQUERIMIENTO = Fe_Requerimiento.SelectedDate;
            vRequisicion.CL_CAUSA = cmbCausas.SelectedValue;
            vRequisicion.DS_CAUSA = txtDescripcionCausa.Text;
            vRequisicion.DS_TIEMPO_CAUSA = txtTiempoCausa.Text;
            vRequisicion.MN_SUELDO = ((txtSueldo.Text != "") ? (Convert.ToDecimal(txtSueldo.Text)) : 0);
            vRequisicion.MN_SUELDO_TABULADOR = ((txtSueldoSugerido.Text != "") ? (Convert.ToDecimal(txtSueldoSugerido.Text)) : 0);
            vRequisicion.MN_SUELDO_SUGERIDO = ((txtSueldoMin.Text != "") ? (Convert.ToDecimal(txtSueldoMin.Text)) : 0);
            vRequisicion.MAX_SUELDO_SUGERIDO = ((txtSueldoMax.Text != "") ? (Convert.ToDecimal(txtSueldoMax.Text)) : 0);
            vRequisicion.DS_COMENTARIOS = txtObservaciones.Content;
            vRequisicion.CL_ESTATUS_REQUISICION = pClEstatusRequisicion;

            /*Se encarga de dar el ID_AUTORIZA y el ID_SOLICITANTE*/
            if (lstAutoriza.Items[0].Text != "No Seleccionado")
            {
                vRequisicion.ID_AUTORIZA = Convert.ToInt32(lstAutoriza.Items[0].Value);
                vRequisicion.NB_EMPLEADO_AUTORIZA = lstAutoriza.Items[0].Text;
                vRequisicion.NB_PUESTO_AUTORIZA = txtPuestoAutoriza.Text;
                vRequisicion.NB_CORREO_AUTORIZA = txtCorreoAutorizaReq.Text;
                vRequisicion.FL_REQUISICION = flRequisicion;
                vRequisicion.CL_TOKEN = clTokenRequisicion;
            }

            if (!string.IsNullOrEmpty(txtCorreoSolicitante.Text))
            {
                vRequisicion.NB_EMPLEADO_AUTORIZA_PUESTO = txtPuestoReq.Text;
                vRequisicion.NB_EMPLEADO_AUTORIZA_PUESTO_PUESTO = txtPuestoAutorizaPuesto.Text;
                vRequisicion.NB_CORREO_AUTORIZA_PUESTO = txtPuestoAutorizaCorreo.Text;
            }


            if (rlbSuplente.Items[0].Text != "No Seleccionado")
            {
                vRequisicion.ID_EMPLEADO_SUPLENTE = Convert.ToInt32(rlbSuplente.Items[0].Value);
                vRequisicion.NB_EMPLEADO_SUPLENTE = rlbSuplente.Items[0].Text;
            }

            if (rlbPuesto.Items[0].Text != "No Seleccionado")
            {
                vRequisicion.ID_PUESTO = Convert.ToInt32(rlbPuesto.Items[0].Value);
                vRequisicion.NB_PUESTO = rlbPuesto.Items[0].Text;
            }

            if (rlbSolicitante.Items[0].Value != "")
            {
                vRequisicion.ID_SOLICITANTE = int.Parse(rlbSolicitante.Items[0].Value);
                vRequisicion.NB_EMPLEADO_SOLICITANTE = rlbSolicitante.Items[0].Text;
                vRequisicion.NB_PUESTO_SOLICITANTE = txtPuestoSolicitante.Text;
            }

            if (vIdRequisicion != null)
            {
                vRequisicion.ID_REQUISICION = (int)vIdRequisicion;

                if (vRequisicion.CL_CAUSA == "NUEVO PUESTO")
                {
                    vRequisicion.FL_NOTIFICACION = flNotificacion;
                    vRequisicion.CL_TOKEN_PUESTO = clTokenPuesto;
                }
            }

            if (vRequisicion != null)
            {
                ValidarEnvioCorreos(vRequisicion);
            }
        }
    }
}