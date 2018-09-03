using SIGE.Negocio.FormacionDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.Comunes
{
    public partial class SeleccionPrograma : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void grdPrograma_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ProgramaNegocio neg = new ProgramaNegocio();
            grdPrograma.DataSource = neg.ObtieneProgramasCapacitacion();
        }
    }
}