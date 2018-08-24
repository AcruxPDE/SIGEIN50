﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.Comunes;

namespace SIGE.WebApp.PDE
{
    public partial class ContextPDE : System.Web.UI.MasterPage
    {
        public string cssModulo = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string vClModulo = "PUNTODEENCUENTRO";
            string vModulo = Request.QueryString["m"];
            if (vModulo != null)
                vClModulo = vModulo;

            cssModulo = Utileria.ObtenerCssModulo(vClModulo);
        }
    }
}