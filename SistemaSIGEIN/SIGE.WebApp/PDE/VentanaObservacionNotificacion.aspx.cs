using SIGE.Entidades;
using SIGE.Entidades.Externas;
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
    public partial class VentanaObservacionNotificacion : System.Web.UI.Page
    {
        public string  vIdEmpleado;
        public string vNbPrograma;
        public string vClUsuario;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int vIdNotificacion
        {
            set { ViewState["vs_vrn_Notificacion"] = value; }
            get { return (int)ViewState["vs_vrn_Notificacion"]; }
        }
        public string vStatusNotificacion
        {
            set { ViewState["vs_vrn_Status"] = value; }
            get { return (string)ViewState["vs_vrn_Status"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vIdEmpleado = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;

            if (!IsPostBack)
            {
                if (Request.Params["IdNotificacion"] != null)
                {
                    NotificacionNegocio nConfiguracion = new NotificacionNegocio();
                    vIdNotificacion = int.Parse(Request.Params["IdNotificacion"]);
                    vStatusNotificacion = Request.Params["ClAutorizacion"];

                }

            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDescripcion.Text))
            {
                var Comentario = txtDescripcion.Text;
                if (vStatusNotificacion == "true")
                {
                vStatusNotificacion = "Atendida";
                }
                else if (vStatusNotificacion == "false")
                {
                    vStatusNotificacion = "Pendiente";
                }
                else {
                    vStatusNotificacion = vStatusNotificacion;
                }
                NotificacionNegocio nConfiguracion = new NotificacionNegocio();
                E_RESULTADO vResultado = nConfiguracion.INSERTA_ACTUALIZA_NOTIFICACION_EMPLEADO(vIdNotificacion, null, null,
                vIdEmpleado, vStatusNotificacion, vClUsuario, vNbPrograma, Comentario, null, null, "A");
                txtDescripcion.Text = "";
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, E_TIPO_RESPUESTA_DB.SUCCESSFUL, pCallBackFunction: "closeWindow");
            }
            else {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Escriba la respuesta", E_TIPO_RESPUESTA_DB.WARNING);
            }
            
        }
    }
}