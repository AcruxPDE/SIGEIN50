﻿using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.IDP
{
    public partial class Default : System.Web.UI.Page
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
            rbiLogoOrganizacion1.DataValue = ContextoApp.InfoEmpresa.FiLogotipo.FiArchivo;
        }
    }
}