using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.ModulosApoyo
{
    public partial class stiConsultaReportes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ruta"] != null)
            {
                string ruta = Request.QueryString["ruta"];
                if (ruta != string.Empty)
                {
                    StiReport reporte = new StiReport();
                    reporte.Load(ruta);
                    reporte.Render(false);
                    StiConsultaReporte.ResetReport();
                    reporte.ReportName = "Prueba Kav";
                    StiConsultaReporte.Report = reporte;
                    
                }
            }
        }
    }
}