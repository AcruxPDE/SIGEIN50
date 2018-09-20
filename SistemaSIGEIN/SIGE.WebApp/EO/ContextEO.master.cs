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

namespace SIGE.WebApp.EO
{
    public partial class ContextEO : System.Web.UI.MasterPage
    {
        public string cssModulo = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

            string vClModulo = "EVALUACION";
            string vModulo = Request.QueryString["m"];
            if (vModulo != null)
                vClModulo = vModulo;

            cssModulo = Utileria.ObtenerCssModulo(vClModulo);

            //if (ContextoApp.EO.LicenciaOrganizacion.MsgActivo == "1")
            //    {
            //    }
            //else
            //{
            //    UtilMensajes.MensajeResultadoDB(RadWindowManager1, ContextoApp.EO.LicenciaOrganizacion.MsgActivo, E_TIPO_RESPUESTA_DB.WARNING);
            //    Response.Redirect(ContextoUsuario.nbHost + "/Logon.aspx");
            //}
           
        }
    }
}