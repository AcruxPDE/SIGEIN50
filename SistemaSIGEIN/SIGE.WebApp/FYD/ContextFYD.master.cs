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

namespace SIGE.WebApp.FYD
{
    public partial class ContextFYD : System.Web.UI.MasterPage
    {
        public string cssModulo = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
             if (ContextoApp.FYD.LicenciaFormacion.MsgActivo == "1")
                {

                string vClModulo = "FORMACION";
                string vModulo = Request.QueryString["m"];
                if (vModulo != null)
                    vClModulo = vModulo;

                cssModulo = Utileria.ObtenerCssModulo(vClModulo);
               
                }

             else
             {
                 UtilMensajes.MensajeResultadoDB(RadWindowManager1, ContextoApp.FYD.LicenciaFormacion.MsgActivo, E_TIPO_RESPUESTA_DB.WARNING);
                 var myUrl = ResolveUrl("~/Logon.aspx");
                 Response.Redirect(ContextoUsuario.nbHost + myUrl);
             }
        }
    }
}