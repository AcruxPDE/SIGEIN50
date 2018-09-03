using SIGE.Negocio.EvaluacionOrganizacional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.Comunes
{
    public partial class SeleccionPeriodosDesempeno : System.Web.UI.Page
    {
        private int? vIdEvaluado
        {
            get { return (int?)ViewState["vs_vIdEvaluado"]; }
            set { ViewState["vs_vIdEvaluado"] = value; }
        }

        private int? vIdPeriodo
        {
            get { return (int?)ViewState["vs_vIdPeriodo"]; }
            set { ViewState["vs_vIdPeriodo"] = value; }
        }

        private string vClTipoConsulta
        {
            get { return (string)ViewState["vs_vClTipoConsulta"]; }
            set { ViewState["vs_vClTipoConsulta"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["CL_TIPO"] != null)
                {
                    vClTipoConsulta = Request.Params["CL_TIPO"];

                    if (Request.Params["ID_EVALUADO"] != null)
                    {
                        vIdEvaluado = int.Parse(Request.Params["ID_EVALUADO"]);
                    }
                    if (Request.Params["ID_PERIODO"] != null)
                    {
                        vIdPeriodo = int.Parse(Request.Params["ID_PERIODO"]);
                    }
                }
            }
        }

        protected void grdPeriodosDesempeno_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            grdPeriodosDesempeno.DataSource = nPeriodo.ObtenerPeriodosComparacion(vIdPeriodo, vIdEvaluado, vClTipoConsulta);
        }

        protected void grdPeriodosDesempeno_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdPeriodosDesempeno.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdPeriodosDesempeno.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdPeriodosDesempeno.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdPeriodosDesempeno.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdPeriodosDesempeno.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}