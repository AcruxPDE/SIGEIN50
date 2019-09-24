using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.AdministracionSitio;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaInventarioPersonalNomina : System.Web.UI.Page
    {
        #region Variables

        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        public string vClUsuario;
        string vNbPrograma;

        protected int? vIdEmpleado
        {
            get { return (int?)ViewState["vs_vIdEmpleado"]; }
            set { ViewState["vs_vIdEmpleado"] = value; }
        }

        protected int? vIdSolicitud
        {
            get { return (int?)ViewState["vs_vIdSolicitud"]; }
            set { ViewState["vs_vIdSolicitud"] = value; }
        }

        protected string vClEstadoEmpleado
        {
            get { return (string)ViewState["vs_vClEstadoEmpleado"]; }
            set { ViewState["vs_vClEstadoEmpleado"] = value; }
        }

        #endregion

        #region Metodos

        protected void CargarDatos(int pAccion)
        {
            CamposNominaNegocio oNegocio = new CamposNominaNegocio();
            E_EMPLEADO_NOMINA_DO vEmpleado = oNegocio.ObtienePersonalNominaDo(pID_EMPLEADO: vIdEmpleado).FirstOrDefault();

            txtClEmpleado.Text = vEmpleado.CL_EMPLEADO;
            txtNombre.Text = vEmpleado.NB_EMPLEADO;
            txtPaterno.Text = vEmpleado.NB_APELLIDO_PATERNO;
            txtMaterno.Text = vEmpleado.NB_APELLIDO_MATERNO;

            btnDOTrue.Checked = (bool)vEmpleado.FG_DO;
            btnDOFalse.Checked = !(bool)vEmpleado.FG_DO;
            btnNOTrue.Checked = (bool)vEmpleado.FG_NOMINA;
            btnNOFalse.Checked = !(bool)vEmpleado.FG_NOMINA;

            if ((bool)vEmpleado.FG_SUELDO_NOMINA_DO)
            {
                btnSueldoTrue.Checked = true;
                btnSueldoFalse.Checked = false;
            }
            else
            {
                btnSueldoTrue.Checked = false;
                btnSueldoFalse.Checked = true;
            }


            if (btnNOTrue.Checked && btnDOTrue.Checked == false)
            {
                if (vEmpleado.NB_PUESTO != null)
                {
                    lstPuestoNomina.Items.Clear();
                    lstPuestoNomina.Items.Add(new RadListBoxItem(vEmpleado.NB_PUESTO, vEmpleado.ID_PUESTO.ToString()));
                    lstPuestoNomina.SelectedValue = vEmpleado.ID_PUESTO.ToString();
                    lstPuestoNomina.Enabled = false;
                }
            }
            else if (btnNOTrue.Checked == false && btnDOTrue.Checked)
            {
                if (vEmpleado.ID_PLAZA != null)
                {
                    lstPuesto.Items.Clear();
                    lstPuesto.Items.Add(new RadListBoxItem(vEmpleado.NB_PLAZA, vEmpleado.ID_PLAZA.ToString()));
                    lstPuesto.SelectedValue = vEmpleado.ID_PLAZA.ToString();
                }
            }
            else
            {
                if (vEmpleado.ID_PLAZA != null && pAccion != 1)
                {
                    lstPuesto.Items.Clear();
                    lstPuesto.Items.Add(new RadListBoxItem(vEmpleado.NB_PLAZA, vEmpleado.ID_PLAZA.ToString()));
                    lstPuesto.SelectedValue = vEmpleado.ID_PLAZA.ToString();
                }
            }
        }

        protected void CargaDatosSolicitud()
        {
            CamposNominaNegocio oNegocio = new CamposNominaNegocio();
            E_SOLICITUD vSolicitud = oNegocio.ObtieneCandidatoSolicitud(pID_SOLICITUD: vIdSolicitud).FirstOrDefault();

            txtNombre.Text = vSolicitud.NB_CANDIDATO;
            txtPaterno.Text = vSolicitud.NB_APELLIDO_PATERNO;
            txtMaterno.Text = vSolicitud.NB_APELLIDO_MATERNO;
        }

        protected void VerificarLicencias()
        {
            if (ContextoApp.ANOM.LicenciaAccesoModulo.MsgActivo != "1")
            {

            }
        }

        protected void verifiarBotones()
        {

            if (btnDOTrue.Checked)
            {
                rapPuestoDO.Visible = true;
                rapPuestoNO.Visible = false;
            }

            else if (btnNOTrue.Checked)
            {
                rapPuestoDO.Visible = false;
                rapPuestoNO.Visible = true;
            }
            else
            {
                rapPuestoDO.Visible = true;
                rapPuestoNO.Visible = false;
            }

            if (btnNOTrue.Checked && btnDOTrue.Checked)
            {
                rapSueldoNominaDO.Visible = true;
            }
            else
            {
                rapSueldoNominaDO.Visible = false;
                if (!IsPostBack)
                    btnSueldoTrue.Checked = false;
                btnSueldoFalse.Checked = true;
            }
        }

        //protected void limpiaCampos()
        //{
        //    lstPuesto.Items[0].Text = "No seleccionado";
        //    lstPuestoNomina.Items[0].Text = "No seleccionado";
        //}

        protected bool verificarCampos()
        {
            if (txtClEmpleado.Text == "")
                return false;
            else if (txtNombre.Text == "")
                return false;
            else if (txtPaterno.Text == "")
                return false;
            else
                return true;
        }

        protected bool verificaDisponibilidad()
        {
            if (btnNOTrue.Checked == false)
                if (btnDOTrue.Checked == false)
                    return false;
                else
                    return true;

            return true;
        }

        protected bool verificaPuesto()
        {
            if (btnDOTrue.Checked)
            {
                if (lstPuesto.Items[0].Text == "No seleccionado")
                    return false;
            }
            else if (btnNOTrue.Checked)
            {
                if (lstPuestoNomina.Items[0].Text == "No seleccionado")
                    return false;
            }

            return true;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int vIdEmpleadoRq = 0;
                int vIdSolicitudRq = 0;
                int vAccionRq = 0;
                int vAccion = 0;

                if (int.TryParse(Request.Params["pIdEmpleado"], out vIdEmpleadoRq))
                    vIdEmpleado = vIdEmpleadoRq;

                if (int.TryParse(Request.Params["EmpleadoID"], out vIdEmpleadoRq))
                    vIdEmpleado = vIdEmpleadoRq;

                if (int.TryParse(Request.Params["SolicitudId"], out vIdSolicitudRq))
                    vIdSolicitud = vIdSolicitudRq;

                if (int.TryParse(Request.Params["Accion"], out vAccionRq))
                    vAccion = vAccionRq;

                if (vIdEmpleado != null)
                {
                    CargarDatos(vAccion);

                    if (vAccion != 1)
                        btnMasDatos.Enabled = true;
                    else
                        txtAccion.Text = "REINGRESO";

                    txtClEmpleado.Enabled = false;
                    txtNombre.Enabled = false;
                    txtPaterno.Enabled = false;
                    txtMaterno.Enabled = false;
                }
                else if (vIdSolicitud != null)
                {
                    CargaDatosSolicitud();

                    txtNombre.Enabled = false;
                    txtPaterno.Enabled = false;
                    txtMaterno.Enabled = false;
                    txtAccion.Text = "SOLICITUD";
                    txtAccion.Visible = false;
                    lblRequisicion.Visible = true;
                    rlbRequicion.Visible = true;
                    btnSeleccionaRequisicion.Visible = true;
                    BtnEliminaRequicion.Visible = true;
                }

                VerificarLicencias();
            }

            verifiarBotones();
            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO.ToString();
        }

        protected void btnDOTrue_Click(object sender, EventArgs e)
        {
            verifiarBotones();
        }

        protected void btnDOFalse_Click(object sender, EventArgs e)
        {
            verifiarBotones();
        }

        protected void ramInventarioPersonalNomina_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            verifiarBotones();
        }

        protected void btnNOTrue_Click(object sender, EventArgs e)
        {
            verifiarBotones();
        }

        protected void btnNOFalse_Click(object sender, EventArgs e)
        {
            verifiarBotones();
        }

        protected void GuardarDatos(bool closeWindows)
        {
            if (verificarCampos())
            {
                if (verificaDisponibilidad())
                {
                    if (verificaPuesto())
                    {
                        CamposNominaNegocio cNegocio = new CamposNominaNegocio();
                        string vClTipoTransaccion = vIdEmpleado != null ? "U" : "I";
                        E_EMPLEADO_NOMINA_DO vEmpleadoNominaDo = new E_EMPLEADO_NOMINA_DO();

                        if (vClTipoTransaccion == "I")
                        {
                            LicenciaNegocio oNegocio = new LicenciaNegocio();
                            var vEmpleados = oNegocio.ObtenerLicenciaVolumen(pFG_ACTIVO: true).FirstOrDefault();
                            if (vEmpleados != null)
                            {
                                if (vEmpleados.NO_EMPLEADOS_DO >= ContextoApp.InfoEmpresa.Volumen)
                                {
                                    UtilMensajes.MensajeResultadoDB(rwMensaje, "Se ha alcanzado el máximo número de empleados para la licencia y no es posible agregar más.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                                    return;
                                }
                            }
                        }

                        vEmpleadoNominaDo.CL_EMPLEADO = txtClEmpleado.Text;
                        vEmpleadoNominaDo.NB_EMPLEADO = txtNombre.Text;
                        vEmpleadoNominaDo.NB_APELLIDO_PATERNO = txtPaterno.Text;
                        vEmpleadoNominaDo.NB_APELLIDO_MATERNO = txtMaterno.Text;
                        vEmpleadoNominaDo.FG_DO = btnDOTrue.Checked ? true : false;
                        vEmpleadoNominaDo.FG_NOMINA = btnNOTrue.Checked ? true : false;

                        if (btnNOTrue.Checked && btnDOTrue.Checked == false)
                        {
                            vEmpleadoNominaDo.FG_NOMINA_DO = false;
                            vEmpleadoNominaDo.ID_PUESTO = int.Parse(lstPuestoNomina.SelectedValue);
                        }
                        else if (btnNOTrue.Checked && btnDOTrue.Checked)
                        {
                            vEmpleadoNominaDo.ID_PLAZA = int.Parse(lstPuesto.SelectedValue);
                            vEmpleadoNominaDo.FG_NOMINA_DO = true;
                        }
                        else
                        {
                            vEmpleadoNominaDo.ID_PLAZA = int.Parse(lstPuesto.SelectedValue);
                            vEmpleadoNominaDo.FG_NOMINA_DO = false;
                        }

                        vEmpleadoNominaDo.FG_SUELDO_NOMINA_DO = btnSueldoTrue.Checked ? true : false;

                        if (txtAccion.Text == "SOLICITUD")
                        {
                            XElement vXmlDatos = new XElement("CANDIDATO");

                            vXmlDatos.Add(new XAttribute("ID_SOLICITUD", vIdSolicitud));
                            vXmlDatos.Add(new XAttribute("ID_EMPLEADO", vIdEmpleado ?? 0));
                            vXmlDatos.Add(new XAttribute("CL_EMPLEADO", vEmpleadoNominaDo.CL_EMPLEADO));
                            vXmlDatos.Add(new XAttribute("NB_EMPLEADO", vEmpleadoNominaDo.NB_EMPLEADO));
                            vXmlDatos.Add(new XAttribute("NB_APELLIDO_PATERNO", vEmpleadoNominaDo.NB_APELLIDO_PATERNO));
                            vXmlDatos.Add(new XAttribute("NB_APELLIDO_MATERNO", vEmpleadoNominaDo.NB_APELLIDO_MATERNO));
                            vXmlDatos.Add(new XAttribute("FG_NOMINA", vEmpleadoNominaDo.FG_NOMINA));
                            vXmlDatos.Add(new XAttribute("FG_DO", vEmpleadoNominaDo.FG_DO));
                            vXmlDatos.Add(new XAttribute("FG_NOMINA_DO", vEmpleadoNominaDo.FG_NOMINA_DO));
                            vXmlDatos.Add(new XAttribute("FG_SUELDO_NOMINA_DO", vEmpleadoNominaDo.FG_SUELDO_NOMINA_DO));
                            vXmlDatos.Add(new XAttribute("CL_TIPO_TRANSACCION", vClTipoTransaccion));
                            vXmlDatos.Add(new XAttribute("ID_REQUISICION", rlbRequicion.SelectedValue));

                            E_RESULTADO vResultado = cNegocio.InsertaActualizaEmpleadoCandidato(vXmlDatos.ToString(), vClUsuario, vNbPrograma);
                            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                            if (vClTipoTransaccion == "I" && vResultado.CL_TIPO_ERROR.ToString() != "ERROR")
                                if (vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals("ID_EMPLEADO")).FirstOrDefault().DS_MENSAJE.ToString() != "0")
                                {
                                    vIdEmpleado = int.Parse(vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals("ID_EMPLEADO")).FirstOrDefault().DS_MENSAJE.ToString());
                                    txtAccion.Text = "CONTRATADO";
                                    btnMasDatos.Enabled = true;
                                    txtClEmpleado.Enabled = false;
                                }

                            if (closeWindows)
                                UtilMensajes.MensajeResultadoDB(rwMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "CloseWindowConfig");
                            else
                                UtilMensajes.MensajeResultadoDB(rwMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "UpdateId(" + vIdEmpleado.ToString() + ")");                            
                        }

                        else if (txtAccion.Text == "REINGRESO")
                        {
                            string vAlta = DateTime.Now.ToString("MM/dd/yyyy");

                            XElement vXmlDatos = new XElement("EMPLEADO");

                            vXmlDatos.Add(new XAttribute("ID_EMPLEADO", vIdEmpleado));
                            vXmlDatos.Add(new XAttribute("FE_ALTA", vAlta));
                            vXmlDatos.Add(new XAttribute("FG_NOMINA", vEmpleadoNominaDo.FG_NOMINA));
                            vXmlDatos.Add(new XAttribute("FG_DO", vEmpleadoNominaDo.FG_DO));
                            vXmlDatos.Add(new XAttribute("FG_NOMINA_DO", vEmpleadoNominaDo.FG_SUELDO_NOMINA_DO));
                            vXmlDatos.Add(new XAttribute("FG_SUELDO_NOMINA_DO", vEmpleadoNominaDo.FG_SUELDO_NOMINA_DO));

                            if (vEmpleadoNominaDo.ID_PUESTO != null)
                                vXmlDatos.Add(new XAttribute("ID_PUESTO", int.Parse(lstPuestoNomina.SelectedValue)));

                            if (vEmpleadoNominaDo.ID_PLAZA != null)
                                vXmlDatos.Add(new XAttribute("ID_PLAZA", int.Parse(lstPuesto.SelectedValue)));

                            E_RESULTADO vResultado = cNegocio.InsertaActualizaEmpleadoReingreso(vXmlDatos.ToString(), vClUsuario, vNbPrograma);
                            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                            if (vResultado.CL_TIPO_ERROR.ToString() != "ERROR")
                            {
                                txtAccion.Text = "CONTRATADO";
                                btnMasDatos.Enabled = true;
                                txtClEmpleado.Enabled = false;
                            }

                            if (closeWindows)
                                UtilMensajes.MensajeResultadoDB(rwMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "CloseWindowConfig");
                            else
                                UtilMensajes.MensajeResultadoDB(rwMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "UpdateId(" + vIdEmpleado.ToString() + ")");

                        }

                        else
                        {
                            E_RESULTADO vResultado = cNegocio.InsertaActualizaEmpleado(pIdEmpleado: vIdEmpleado, pEmpleado: vEmpleadoNominaDo, pClUsuario: vClUsuario, pNbPrograma: vNbPrograma, pClTipoTransaccion: vClTipoTransaccion);
                            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                            if (vClTipoTransaccion == "I" && vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals("ID_EMPLEADO")).FirstOrDefault().DS_MENSAJE.ToString() != "0")
                                vIdEmpleado = int.Parse(vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals("ID_EMPLEADO")).FirstOrDefault().DS_MENSAJE.ToString());

                            if (closeWindows)
                                UtilMensajes.MensajeResultadoDB(rwMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "CloseWindowConfig");
                            else
                            {
                                UtilMensajes.MensajeResultadoDB(rwMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "UpdateId(" + vIdEmpleado.ToString() + ")");
                                btnMasDatos.Enabled = true;
                            }
                        }

                    }
                    else
                    {
                        if (btnDOTrue.Checked)
                            UtilMensajes.MensajeResultadoDB(rwMensaje, "Debe seleccionar la plaza del empleado", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, pCallBackFunction: "");
                        else if (btnNOTrue.Checked)
                            UtilMensajes.MensajeResultadoDB(rwMensaje, "Debe seleccionar el puesto del empleado", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, pCallBackFunction: "");
                    }
                }
                else
                    UtilMensajes.MensajeResultadoDB(rwMensaje, "Debe seleccionar la disponiblidad del empleado", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, pCallBackFunction: "");
            }
            else
                UtilMensajes.MensajeResultadoDB(rwMensaje, "Los campos no. de empleado, nombre y apellido paterno son requeridos", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, pCallBackFunction: "");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarDatos(false);
            
        }

        protected void btnGuardarCerrar_Click(object sender, EventArgs e)
        {
            GuardarDatos(true);
        }

        protected void lstPuestoNomina_CallingDataMethods(object sender, CallingDataMethodsEventArgs e)
        {

        }

        protected void btnMasDatos_Click(object sender, EventArgs e)
        {
            if(txtAccion.Text != "SOLICITUD")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirInventario", "abrirInventario(" + vIdEmpleado.ToString() + ")", true); 
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirInventarioContratado", "abrirInventarioContratado(" + vIdEmpleado.ToString() + ")", true);
            
        }
    }
}