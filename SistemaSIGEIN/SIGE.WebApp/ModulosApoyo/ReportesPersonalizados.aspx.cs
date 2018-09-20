using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stimulsoft.Report;
using SIGE.WebApp.Comunes;
using SIGE.Negocio.Utilerias;

namespace SIGE.WebApp.ModulosApoyo
{
    public partial class ReportesPersonalizados : System.Web.UI.Page
    {

        #region Variables

        public string vNbUsuario
        {
            get { return (string)ViewState["vs_vNbUsuario"]; }
            set { ViewState["vs_vNbUsuario"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vNbUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            if (ContextoApp.InfoEmpresa.FiLogotipo.FiArchivo != null)
                rbiLogoOrganizacion1.DataValue = ContextoApp.InfoEmpresa.FiLogotipo.FiArchivo;
            else
                dvLogo.Visible = false;
        }
    }
}