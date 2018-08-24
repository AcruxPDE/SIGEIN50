using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace SIGE.WebApp.IDP
{
    public partial class VentanaContratarCandidato : System.Web.UI.Page
    {

        #region Variables
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int vIdSolicitud
        {
            get { return (int)ViewState["vs_vcc_id_solicitud"]; }
            set { ViewState["vs_vcc_id_solicitud"] = value; }
        }

        public int vIdCandidato {
            get { return (int)ViewState["vs_vcc_id_candidato"]; }
            set { ViewState["vs_vcc_id_candidato"] = value; }
        }

        #endregion

        #region Funciones

        private void CargarDatos()
        {
            SolicitudNegocio nSoilcitud = new SolicitudNegocio();

            var oSolicitud = nSoilcitud.ObtieneSolicitudes(ID_SOLICITUD: vIdSolicitud).FirstOrDefault();

            if (oSolicitud != null)
            {
                txtCandidato.InnerText = oSolicitud.NB_CANDIDATO_COMPLETO;
                txtClaveSolicitud.InnerText = oSolicitud.CL_SOLICITUD;
                txtFechaSolicitud.InnerText = oSolicitud.FE_SOLICITUD.Value.ToShortDateString();
                vIdCandidato = oSolicitud.ID_CANDIDATO.Value;
            }
            //EmpresaNegocio nEmpresa = new EmpresaNegocio();
            //var vLstEmpresas = nEmpresa.Obtener_C_EMPRESA();
            //cmbEmpresa.DataSource = vLstEmpresas;
            //cmbEmpresa.DataTextField = "NB_RAZON_SOCIAL";
            //cmbEmpresa.DataValueField = "ID_EMPRESA";
            //cmbEmpresa.DataBind();
            //cmbEmpresa.EmptyMessage = "Seleccione...";
        }

        private void GuardarDatos()
        {
            if (ValidarDatos())
            {
                string vIdPuesto;
                string vIdPlazaJefe;
                XElement vXmlDatos = new XElement("CANDIDATO");

                vIdPuesto = rlbPuesto.Items[0].Value;
                vIdPlazaJefe = rlbJefe.Items[0].Value;

                vXmlDatos.Add(new XAttribute("ID_SOLICITUD", vIdSolicitud));
                vXmlDatos.Add(new XAttribute("CL_EMPLEADO", txtClave.Text));
                vXmlDatos.Add(new XAttribute("ID_PUESTO", vIdPuesto));
                vXmlDatos.Add(new XAttribute("MN_SUELDO", txtSueldo.Text));
                vXmlDatos.Add(new XAttribute("ID_PUESTO_JEFE", vIdPlazaJefe));

                SolicitudNegocio nSolicitud = new SolicitudNegocio();

                EmpleadoNegocio nEmpleado = new EmpleadoNegocio();

                if (nEmpleado.ObtenerEmpleados(pID_EMPRESA: null, pFgActivo: true).Count() + 1 > ContextoApp.InfoEmpresa.Volumen)
                {
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, "Se ha alcanzado el máximo número de empleados para la licencia y no es posible agregar más.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                    return;
                }

                E_RESULTADO vResultado = nSolicitud.InsertaCandidatoContratado(vXmlDatos.ToString(), vClUsuario, vNbPrograma);
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vResultado.MENSAJE[0].DS_MENSAJE.ToString(), vResultado.CL_TIPO_ERROR, 400, 150, "closeWindow");
            }
        }

        private bool ValidarDatos()
        {
            if (String.IsNullOrEmpty(txtClave.Text))
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Indica la clave para el empleado.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                return false;
            }

            if (rlbPuesto.Items[0].Value == "0")
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Indica el puesto que ocupará", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                return false;
            }

            if (rlbJefe.Items[0].Value == "0")
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Indica el jefe inmediato.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                return false;
            }

            if (string.IsNullOrEmpty(txtSueldo.Text.Trim()))
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Indica el sueldo que percibirá", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                return false;
            }

            //string vEmpresa = cmbEmpresa.SelectedItem.Text;

            //if (vEmpresa == null)
            //{
            //    UtilMensajes.MensajeResultadoDB(rwmAlertas, "Indica la empresa a la que pertenece", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
            //    return false;
            //}

            return true;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                if (Request.Params["SolicitudId"] != null)
                {
                    vIdSolicitud = int.Parse(Request.Params["SolicitudId"].ToString());
                    CargarDatos();
                }
            }
        }

        protected void btnGurdar_Click(object sender, EventArgs e)
        {
            GuardarDatos();
        }
    }
}