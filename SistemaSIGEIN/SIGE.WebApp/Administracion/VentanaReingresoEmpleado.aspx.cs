using SIGE.Entidades;
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

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaReingresoEmpleado : System.Web.UI.Page
    {

        #region Variables
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int vIdEmpleado
        {
            get { return (int)ViewState["vs_vre_id_empleado"]; }
            set { ViewState["vs_vre_id_empleado"] = value; }
        }

        #endregion

        #region Funciones

        private void CargarDatos()
        {
            EmpleadoNegocio nEmpleado = new EmpleadoNegocio();

            var oEmpleado = nEmpleado.ObtenerEmpleado(ID_EMPLEADO: vIdEmpleado).FirstOrDefault();

            if (oEmpleado != null)
            {
                txtEmpleado.InnerText = oEmpleado.NB_EMPLEADO_COMPLETO;
                txtClaveEmpleado.InnerText = oEmpleado.CL_EMPLEADO;
                rdpFechaIngreso.SelectedDate = DateTime.Now;
            }
        }

        private void GuardarDatos()
        {
            if (ValidarDatos())
            {
                string vIdPuesto;
                string vIdPlazaJefe;
                string vFeAlta;
                XElement vXmlDatos = new XElement("EMPLEADO");

                vIdPuesto = rlbPuesto.Items[0].Value;
                vIdPlazaJefe = rlbJefe.Items[0].Value;

                if (rdpFechaIngreso.SelectedDate == null)
                {
                    vFeAlta = DateTime.Now.ToString("MM/dd/yyyy");
                }
                else
                {
                    vFeAlta = rdpFechaIngreso.SelectedDate.Value.ToString("MM/dd/yyyy");
                }

                vXmlDatos.Add(new XAttribute("ID_EMPLEADO", vIdEmpleado));
                vXmlDatos.Add(new XAttribute("ID_PUESTO", vIdPuesto));
                vXmlDatos.Add(new XAttribute("MN_SUELDO", txtSueldo.Text));
                vXmlDatos.Add(new XAttribute("ID_PUESTO_JEFE", vIdPlazaJefe));
                vXmlDatos.Add(new XAttribute("FE_ALTA", vFeAlta));

                EmpleadoNegocio nEmpleado = new EmpleadoNegocio();

                    LicenciaNegocio oNegocio = new LicenciaNegocio();
                    var vEmpleados = oNegocio.ObtenerLicenciaVolumen(pFG_ACTIVO: true).FirstOrDefault();
                    if (vEmpleados.NO_TOTAL_ALTA >= ContextoApp.InfoEmpresa.Volumen)
                    {
                        UtilMensajes.MensajeResultadoDB(rwmAlertas, "Se ha alcanzado el máximo número de empleados para la licencia y no es posible agregar más.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                        return;
                    }

                E_RESULTADO vResultado = nEmpleado.ActualizaReingresoEmpleado(vXmlDatos.ToString(), vClUsuario, vNbPrograma);
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vResultado.MENSAJE[0].DS_MENSAJE.ToString(), vResultado.CL_TIPO_ERROR, 400, 150, "closeWindow");
            }
        }

        private bool ValidarDatos()
        {

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
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Indica el sueldo que percibirá.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                return false;
            }

            return true;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                if (Request.Params["EmpleadoId"] != null)
                {
                    vIdEmpleado = int.Parse(Request.Params["EmpleadoId"].ToString());
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