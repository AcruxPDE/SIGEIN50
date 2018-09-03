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
using Telerik.Web.UI;
using WebApp.Comunes;

namespace SIGE.WebApp
{
    public partial class Menu : MasterPage
    {
        public string cssPiePagina = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            List<E_FUNCION> lstFunciones = ContextoUsuario.oUsuario.oFunciones.Where(w => w.CL_TIPO_FUNCION.Equals(E_TIPO_FUNCION.MENUGRAL.ToString())).ToList();

            string vClModulo = "GENERAL";
           
            string vModulo = Request.QueryString["m"];
            if (vModulo != null)
                vClModulo = vModulo;

            switch(vClModulo)
            {
                case "INTEGRACION":
                    cssPiePagina = "PiedePaginaIdp";
                    break;
                case "FORMACION":
                    cssPiePagina = "PiedePaginaFd";
                    break;
                case "DESEMPENO":
                    cssPiePagina = "PiedePaginaEo";
                    break;
                case "CLIMA":
                    cssPiePagina = "PiedePaginaEo";
                    break;
                case "ROTACION":
                    cssPiePagina = "PiedePaginaEo";
                    break;
                case "COMPENSACION":
                    cssPiePagina = "PiedePaginaMpc";
                    break;
                case "TC":
                    cssPiePagina = "PiedePaginaTc";
                    break;
                default:
                    cssPiePagina = "PiedePaginaAdm";
                    break;
            }

            List<E_MENU> lstMenu = Utileria.CrearMenuLista(lstFunciones, vClModulo);
            divMenu.Controls.Add(Utileria.CrearMenu(lstMenu, Request.Browser.IsMobileDevice));
            lblEmpresa.InnerText = ContextoApp.InfoEmpresa.NbEmpresa;
        }
    }
}