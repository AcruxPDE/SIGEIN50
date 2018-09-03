using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stimulsoft.Report;

namespace SIGE.WebApp.IDP
{
    public partial class PruebaReporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StiReport report = new StiReport();
            
            report.Load(Server.MapPath("~/Assets/reports/IDP/Prueba.mrt"));
            StiWebViewer1.Report = report;
           
        }
    }
}