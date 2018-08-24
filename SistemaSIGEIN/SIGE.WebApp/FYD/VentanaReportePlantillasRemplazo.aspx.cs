using SIGE.Negocio.FormacionDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.FYD
{
    public partial class VentanaReportePlantillasRemplazo : System.Web.UI.Page
    {

        #region Variables

        private int vIdPuesto
        {
            get { return (int)ViewState["vs_vrpr_id_puesto"]; }
            set { ViewState["vs_vrpr_id_puesto"] = value; }
        }

        #endregion

        #region Funciones

        private void CargarDatos()
        {
            ConsultasFYDNegocio neg = new ConsultasFYDNegocio();

            var oPuesto = neg.ObtienePuestos(ID_PUESTO: vIdPuesto).FirstOrDefault();

            if (oPuesto != null)
            {
                txtPuesto.Text = oPuesto.CL_PUESTO +" "+ oPuesto.NB_PUESTO;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["IdPuesto"] != null)
                {
                    vIdPuesto = int.Parse(Request.Params["IdPuesto"].ToString());
                    CargarDatos();
                }
            }
        }

        protected void rpgReporte_NeedDataSource(object sender, Telerik.Web.UI.PivotGridNeedDataSourceEventArgs e)
        {
            ConsultasFYDNegocio neg = new ConsultasFYDNegocio();
            rpgReporte.DataSource = neg.ObtenerReportePlantillasReemplazo(vIdPuesto);
        }

        protected void rpgReporte_CellDataBound(object sender, Telerik.Web.UI.PivotGridCellDataBoundEventArgs e)
        {
            int vNoValorCompetencia;

            if (e.Cell is PivotGridRowHeaderCell)
            {

                if (e.Cell.Controls.Count > 1)
                {
                    (e.Cell.Controls[0] as Button).Visible = false;
                }
            }

            if (e.Cell is PivotGridDataCell)
            {
                PivotGridDataCell celda = (PivotGridDataCell)e.Cell;

                e.Cell.HorizontalAlign = HorizontalAlign.Center;

                if (celda.IsGrandTotalCell)
                {
                    vNoValorCompetencia = Convert.ToInt32((decimal)e.Cell.DataItem);
                    
                    if (vNoValorCompetencia < 70) { e.Cell.ForeColor = System.Drawing.Color.Red; }
                    else if (vNoValorCompetencia >= 70 & vNoValorCompetencia < 85) { e.Cell.ForeColor = System.Drawing.Color.Gold; }
                    else if (vNoValorCompetencia >= 85) { e.Cell.ForeColor = System.Drawing.Color.Green; }
                }


            }
        }
    }
}