using SIGE.Entidades.Externas;
using SIGE.Negocio.EvaluacionOrganizacional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.EO.Cuestionarios
{
    public partial class ReporteJerarquico : System.Web.UI.Page
    {
        private int? vIdEvaluador
        {
            get { return (int?)ViewState["vs_vIdEvaluador"]; }
            set { ViewState["vs_vIdEvaluador"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int vIdEvaluadorTemp = 0;
                if (int.TryParse(Request.QueryString["ID_EVALUADOR"], out vIdEvaluadorTemp))
                    vIdEvaluador = vIdEvaluadorTemp;
            }
        }

        protected void grdReporteJerarquico_NeedDataSource(object sender, Telerik.Web.UI.TreeListNeedDataSourceEventArgs e)
        {
            if (vIdEvaluador != null)
            {
                PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
              //  grdReporteJerarquico.DataSource = nPeriodo.ObtieneResultadoJerarquico((int)vIdEvaluador);
            }
        }

        protected void btnExportarExcel_Click(object sender, EventArgs e)
        {
            grdReporteJerarquico.ExportSettings.IgnorePaging = true;
            grdReporteJerarquico.ExportSettings.ExportMode = TreeListExportMode.DefaultContent;
            grdReporteJerarquico.ShowFooter = false;
            grdReporteJerarquico.Rebind();
            grdReporteJerarquico.ExportSettings.Excel.Format = TreeListExcelFormat.Xlsx;
            grdReporteJerarquico.ExportToExcel();
        }
    }
}