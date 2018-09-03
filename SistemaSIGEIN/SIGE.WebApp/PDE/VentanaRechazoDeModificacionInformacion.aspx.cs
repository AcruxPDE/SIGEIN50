using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.PuntoDeEncuentro;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.PDE
{
    public partial class VentanaRechazoDeModificacionInformacion : System.Web.UI.Page
    {
        public string  vIdEmpleado;
        public string vNbPrograma;
        public string vClUsuario;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int vIdCAmbio
        {
            set { ViewState["vs_vrn_Notificacion"] = value; }
            get { return (int)ViewState["vs_vrn_Notificacion"]; }
        }
        public string vStatusCambio
        {
            set { ViewState["vs_vrn_Status"] = value; }
            get { return (string)ViewState["vs_vrn_Status"]; }
        }
        public string vIdDescriptivo
        {
            get { return (string)ViewState["vs_vIdEmpleado"]; }
            set { ViewState["vs_vIdEmpleado"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            vIdEmpleado =ContextoUsuario.oUsuario.ID_EMPLEADO.ToString();
            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;

            if (!IsPostBack)
            {
                if (Request.Params["IdCambio"] != null)
                {
                    vIdCAmbio = int.Parse(Request.Params["IdCambio"]);
                    vStatusCambio = Request.Params["ClAutorizacion"];
                   
                }
                if (Request.Params["IdPuesto"] != null)
                {
                    vIdDescriptivo = Request.Params["IdPuesto"];
                }

            }


        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDescripcion.Text))
            {
                var Comentario = txtDescripcion.Text;

                if (vIdDescriptivo == null)
                {

                    InventarioPersonalNegocio inventario = new InventarioPersonalNegocio();

                    E_RESULTADO vResultado = inventario.ActualizaModificacionEmpleado(vIdCAmbio, false, txtDescripcion.Text, vIdEmpleado, "Rechazada", "A", vClUsuario, vNbPrograma);
                    txtDescripcion.Text = "";
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, E_TIPO_RESPUESTA_DB.SUCCESSFUL);
                }
                else {
                    DescriptivoPuestoNegocio puesto = new DescriptivoPuestoNegocio();

                    E_RESULTADO vResultado = puesto.ActualizaModificacionDescriptivo(vIdCAmbio, false, txtDescripcion.Text, vIdEmpleado, "Rechazada", "A", vClUsuario, vNbPrograma);
                    txtDescripcion.Text = "";
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, E_TIPO_RESPUESTA_DB.SUCCESSFUL);
                }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Escriba el motivo del rechazo", E_TIPO_RESPUESTA_DB.WARNING);
            }
        }
    }
}