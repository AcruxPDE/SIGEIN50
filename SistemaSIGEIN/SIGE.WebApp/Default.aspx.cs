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
using System.Xml.Linq;
using Telerik.Web.UI;
using WebApp.Comunes;

namespace SIGE.WebApp
{
    public partial class Default : System.Web.UI.Page
    {
        #region Variables Licencia

        public string vFgModuloIDP
        {
            get { return (string)ViewState["vs_vFgModuloIDP"]; }
            set { ViewState["vs_vFgModuloIDP"] = value; }
        }

        public string vFgModuloFYD
        {
            get { return (string)ViewState["vs_vFgModuloFYD"]; }
            set { ViewState["vs_vFgModuloFYD"] = value; }
        }

        public string vFgModuloCL
        {
            get { return (string)ViewState["vs_vFgModuloCL"]; }
            set { ViewState["vs_vFgModuloCL"] = value; }
        }

        public string vFgModuloED
        {
            get { return (string)ViewState["vs_vFgModuloED"]; }
            set { ViewState["vs_vFgModuloED"] = value; }
        }


        public string vFgModuloRDP
        {
            get { return (string)ViewState["vs_vFgModuloRDP"]; }
            set { ViewState["vs_vFgModuloRDP"] = value; }
        }


        public string vFgModuloMPC
        {
            get { return (string)ViewState["vs_vFgModuloMPC"]; }
            set { ViewState["vs_vFgModuloMPC"] = value; }
        }

        public string vFgModuloRP
        {
            get { return (string)ViewState["vs_vFgModuloRP"]; }
            set { ViewState["vs_vFgModuloRP"] = value; }
        }

        public string vFgModuloCI
        {
            get { return (string)ViewState["vs_vFgModuloCI"]; }
            set { ViewState["vs_vFgModuloCI"] = value; }
        }

        public string vFgModuloPDE
        {
            get { return (string)ViewState["vs_vFgModuloPDE"]; }
            set { ViewState["vs_vFgModuloPDE"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ContextoUsuario.oUsuario != null)
            {
                List<E_FUNCION> lstFunciones = ContextoUsuario.oUsuario.oFunciones.Where(w => w.CL_TIPO_FUNCION.Equals(E_TIPO_FUNCION.MENUGRAL.ToString())).ToList();
                List<E_MENU> lstMenu = Utileria.CrearMenuLista(lstFunciones, "GENERAL", pFgLimpiarEstilo: true);
                divMenu.Controls.Add(Utileria.CrearMenu(lstMenu, Request.Browser.IsMobileDevice));


                //Licencia
                vFgModuloIDP = ContextoApp.IDP.LicenciaIntegracion.MsgActivo;
                vFgModuloFYD = ContextoApp.FYD.LicenciaFormacion.MsgActivo;
                vFgModuloCL = ContextoApp.EO.LicenciaCL.MsgActivo;
                vFgModuloED = ContextoApp.EO.LicenciaED.MsgActivo;
                vFgModuloRDP = ContextoApp.EO.LicenciaRDP.MsgActivo;
                vFgModuloMPC = ContextoApp.MPC.LicenciaMetodologia.MsgActivo;
                vFgModuloPDE = ContextoApp.PDE.LicenciaPuntoEncuentro.MsgActivo;
                vFgModuloCI = ContextoApp.CI.LicenciaConsultasInteligentes.MsgActivo;
                vFgModuloRP = ContextoApp.RP.LicenciaReportes.MsgActivo;

                lblEmpresa.InnerText = ContextoApp.InfoEmpresa.NbEmpresa;
            }
        }
    }
}