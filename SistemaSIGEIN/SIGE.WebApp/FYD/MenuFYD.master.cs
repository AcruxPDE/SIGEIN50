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

namespace SIGE.WebApp.FYD
{
    public partial class MenuFYD : MasterPage
    {
        public string cssPiePagina = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ContextoUsuario.oUsuario != null)
            {
                if (ContextoApp.FYD.LicenciaFormacion.MsgActivo == "1")
                {
                    List<E_FUNCION> lstMenuGeneral = ContextoUsuario.oUsuario.oFunciones.Where(w => w.CL_TIPO_FUNCION.Equals(E_TIPO_FUNCION.MENUGRAL.ToString())).ToList();
                    List<E_FUNCION> lstMenuModulo = ContextoUsuario.oUsuario.oFunciones.Where(w => w.CL_TIPO_FUNCION.Equals(E_TIPO_FUNCION.MENUWEB.ToString())).ToList();

                    string vClModulo = "FORMACION";
                    string vModulo = Request.QueryString["m"];
                    if (vModulo != null)
                        vClModulo = vModulo;

                    switch (vClModulo)
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

                    List<E_MENU> lstMenu = Utileria.CrearMenuLista(lstMenuModulo, "FORMACION", true);
                    lstMenu.AddRange(Utileria.CrearMenuLista(lstMenuGeneral, vClModulo));
                    divMenu.Controls.Add(Utileria.CrearMenu(lstMenu, Request.Browser.IsMobileDevice));
                    lblEmpresa.InnerText = ContextoApp.InfoEmpresa.NbEmpresa;
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
}