using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.WebApp.Comunes;
using Telerik.Web.UI;
using WebApp.Comunes;
using SIGE.Negocio.Utilerias;

namespace SIGE.WebApp.PDE
{
    public partial class MenuPDE : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 if (ContextoApp.PDE.LicenciaPuntoEncuentro.MsgActivo == "1")
                {
                if (ContextoUsuario.oUsuario != null)
                {

                    List<E_FUNCION> lstMenuGeneral = ContextoUsuario.oUsuario.oFunciones.Where(w => w.CL_TIPO_FUNCION.Equals(E_TIPO_FUNCION.MENUGRAL.ToString())).ToList();
                    List<E_FUNCION> lstMenuModulo = ContextoUsuario.oUsuario.oFunciones.Where(w => w.CL_TIPO_FUNCION.Equals(E_TIPO_FUNCION.MENUADICIONAL.ToString())).ToList();

                    string vClModulo = "ENCUENTRO";
                    string vModulo = Request.QueryString["m"];
                    if (vModulo != null)
                        vClModulo = vModulo;

                    List<E_MENU> lstMenu = Utileria.CrearMenuLista(lstMenuModulo, "ENCUENTRO", true);
                    lstMenu.AddRange(Utileria.CrearMenuLista(lstMenuGeneral, vClModulo));
                    divMenu.Controls.Add(Utileria.CrearMenu(lstMenu, Request.Browser.IsMobileDevice));
                }
                }
                 else
                 {
                     UtilMensajes.MensajeResultadoDB(rwmMensaje, ContextoApp.PDE.LicenciaPuntoEncuentro.MsgActivo, E_TIPO_RESPUESTA_DB.WARNING);
                     var myUrl = ResolveUrl("~/Logon.aspx");
                     Response.Redirect(ContextoUsuario.nbHost + myUrl);
                 }
            }
        }
    }
}