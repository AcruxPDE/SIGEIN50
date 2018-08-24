using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.Comunes;

namespace SIGE.WebApp.IDP.Solicitud
{
    public partial class VentanaSolicitudAvisoPrivacidad : System.Web.UI.Page
    {
        public string cssModulo = String.Empty;
     
        public string vClUsuario;
        string vNbPrograma;
        
           protected void Page_Load(object sender, EventArgs e)
        {
            string vClModulo = "INTEGRACION";
            string vModulo = Request.QueryString["m"];
            if (vModulo != null)
                vClModulo = vModulo;

            cssModulo = Utileria.ObtenerCssModulo(vClModulo);

            if (!Page.IsPostBack)
            {
                lbAvisoPrivacidad.InnerHtml = ContextoApp.IDP.MensajePrivacidad.dsMensaje;
            }

            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = (ContextoUsuario.oUsuario != null) ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO SOLICITUD";
        }
    }
}