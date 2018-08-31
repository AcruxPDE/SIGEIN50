using SIGE.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.Comunes
{
    public partial class SeleccionPruebas : System.Web.UI.Page
    {

        #region Variables

        private int? vIdBateria
        {
            get { return(int?)ViewState["vs_vIdBateria"];}
            set { ViewState["vs_vIdBateria"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["pIdBateria"] != null)
                {
                    vIdBateria = int.Parse(Request.Params["pIdBateria"].ToString());
                }
            }
        }

        protected void grdPruebas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PruebasNegocio pNeg = new PruebasNegocio();
            grdPruebas.DataSource = pNeg.ObtenerPruebasConfiguradas(vIdBateria);
        }

        protected void grdPruebas_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdPruebas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdPruebas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdPruebas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdPruebas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdPruebas.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}