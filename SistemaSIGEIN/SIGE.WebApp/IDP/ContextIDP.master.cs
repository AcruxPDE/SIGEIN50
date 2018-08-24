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

namespace SIGE.WebApp.IDP
{
    public partial class ContextIDP : System.Web.UI.MasterPage
    {
        public string cssModulo = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

             if (ContextoApp.IDP.LicenciaIntegracion.MsgActivo == "1")
                {
            string vClModulo = "INTEGRACION";
            string vModulo = Request.QueryString["m"];
            if (vModulo != null)
                vClModulo = vModulo;

            cssModulo = Utileria.ObtenerCssModulo(vClModulo);
                }
             else
             {
                 UtilMensajes.MensajeResultadoDB(RadWindowManager1, ContextoApp.IDP.LicenciaIntegracion.MsgActivo, E_TIPO_RESPUESTA_DB.WARNING);
                 Response.Redirect(ContextoUsuario.nbHost + "/Logon.aspx");
                
             }
        }
    }
}