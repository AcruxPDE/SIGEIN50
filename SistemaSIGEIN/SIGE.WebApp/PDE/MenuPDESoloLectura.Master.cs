using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.PDE
{
    public partial class MenuPDESoloLectura : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             if (ContextoApp.PDE.LicenciaPuntoEncuentro.MsgActivo == "1")
                {

                }
             else
             {
                 UtilMensajes.MensajeResultadoDB(rwmMensaje, ContextoApp.MPC.LicenciaMetodologia.MsgActivo, E_TIPO_RESPUESTA_DB.WARNING);
                 Response.Redirect(ContextoUsuario.nbHost + "/Logon.aspx");
             }


        }
    }
}