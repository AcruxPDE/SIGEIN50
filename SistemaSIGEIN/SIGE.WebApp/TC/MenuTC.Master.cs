using SIGE.Entidades.Administracion;
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

namespace SIGE.WebApp.TC
{
    public partial class MenuTC : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ContextoUsuario.oUsuario != null)
            {
                List<E_FUNCION> lstMenuGeneral = ContextoUsuario.oUsuario.oFunciones.Where(w => w.CL_TIPO_FUNCION.Equals(E_TIPO_FUNCION.MENUGRAL.ToString())).ToList();
                List<E_FUNCION> lstMenuModulo = ContextoUsuario.oUsuario.oFunciones.Where(w => w.CL_TIPO_FUNCION.Equals(E_TIPO_FUNCION.MENUWEB.ToString())).ToList();

                string vClModulo = "TABLERO";
                string vModulo = Request.QueryString["m"];
                if (vModulo != null)
                    vClModulo = vModulo;

                List<E_MENU> lstMenu = Utileria.CrearMenuLista(lstMenuModulo, "TABLERO", true);
                lstMenu.AddRange(Utileria.CrearMenuLista(lstMenuGeneral, vClModulo));
                divMenu.Controls.Add(Utileria.CrearMenu(lstMenu, Request.Browser.IsMobileDevice));
                lblEmpresa.InnerText = ContextoApp.InfoEmpresa.NbEmpresa;
            }
        }
    }
}