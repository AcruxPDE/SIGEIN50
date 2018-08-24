using SIGE.Entidades.Externas;
using SIGE.Entidades.Administracion;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.Comunes;
using SIGE.Negocio.Utilerias;

namespace SIGE.WebApp.ModulosApoyo
{
    public partial class MenuMA : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ContextoApp.RP.LicenciaReportes.MsgActivo == "1")
            {
                if (ContextoUsuario.oUsuario != null)
                {
                    List<E_FUNCION> lstMenuGeneral = ContextoUsuario.oUsuario.oFunciones.Where(w => w.CL_TIPO_FUNCION.Equals(E_TIPO_FUNCION.MENUGRAL.ToString())).ToList();
                    List<E_FUNCION> lstMenuModulo = ContextoUsuario.oUsuario.oFunciones.Where(w => w.CL_TIPO_FUNCION.Equals(E_TIPO_FUNCION.MENUWEB.ToString())).ToList();

                    string vClModulo = "REPORTES_PERSONALIZADOS";
                    string vModulo = Request.QueryString["m"];
                    if (vModulo != null)
                        vClModulo = vModulo;

                    List<E_MENU> lstMenu = Utileria.CrearMenuLista(lstMenuModulo, "REPORTES_PERSONALIZADOS", true);
                    lstMenu.AddRange(Utileria.CrearMenuLista(lstMenuGeneral, vClModulo));
                    divMenu.Controls.Add(Utileria.CrearMenu(lstMenu, Request.Browser.IsMobileDevice));
                }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(RadWindowManager1, ContextoApp.RP.LicenciaReportes.MsgActivo, E_TIPO_RESPUESTA_DB.WARNING);
                Response.Redirect(ContextoUsuario.nbHost + "/Logon.aspx");
            }
        }
    }
}