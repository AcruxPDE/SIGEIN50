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
    public
partial class VentanaCopiarFormatoTramite : System.Web.UI.Page
    {

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        public int vIdFormato_Tramite
        {
            set { ViewState["vs_vrn_Notificacion"] = value; }
            get { return (int)ViewState["vs_vrn_Notificacion"]; }
        }

        public string vTipoDocumento
        {
            set { ViewState["vs_vrn_TipoDocumento"] = value; }
            get { return (string)ViewState["vs_vrn_TipoDocumento"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                if (Request.Params["Indice"] != null)
                {
                    vIdFormato_Tramite = int.Parse(Request.Params["Indice"]);
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombreDocumento = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            if (nombreDocumento != String.Empty && descripcion != String.Empty)
            {
                FormatosYTramitesNegocio nConfiguracion = new FormatosYTramitesNegocio();
                E_RESULTADO vResultado = nConfiguracion.INSERTA_COPIA_FORMATOS_Y_TRAMITES(vIdFormato_Tramite, nombreDocumento, descripcion, true, vClUsuario, vNbPrograma, "I");

                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, E_TIPO_RESPUESTA_DB.SUCCESSFUL );

            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Escribe el nombre y la descripción del " + vTipoDocumento + " a copiar", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            }
        }


    }
}