using SIGE.Entidades;
using SIGE.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.Comunes.SeleccionLocalizacion
{
    public partial class SeleccionCP : System.Web.UI.Page
    {

        #region Variables

        public string vClCodigoPostal
        {
            set { ViewState["vs_vClCodigoPostal"] = value; }
            get { return (string)ViewState["vs_vClCodigoPostal"]; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }

        }

        protected void grdCodigoPostal_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ColoniaNegocio cpNegocio = new ColoniaNegocio();
            List<SPE_OBTIENE_C_CODIGO_POSTAL_Result> vListaCP = new List<SPE_OBTIENE_C_CODIGO_POSTAL_Result>();
            if (vClCodigoPostal != null && vClCodigoPostal != "")
            {
                vListaCP = cpNegocio.ObtieneCodigoPostal(vClCodigoPostal);
            }
            grdCodigoPostal.DataSource = vListaCP;
        }

        protected void btnBuscarCp_Click(object sender, EventArgs e)
        {
            vClCodigoPostal = txtCP.Text;
            grdCodigoPostal.Rebind();
        }

        protected void grdCodigoPostal_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdCodigoPostal.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdCodigoPostal.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdCodigoPostal.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdCodigoPostal.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdCodigoPostal.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}