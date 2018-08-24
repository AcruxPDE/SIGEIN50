using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Negocio.Administracion;
using Telerik.Web.UI;

namespace SIGE.WebApp.Comunes
{
    public partial class SeleccionCatalogos : System.Web.UI.Page
    {
        public int vIdCatalogo
        {
            get { return (int)ViewState["vs_vIdCatalogo"]; }
            set { ViewState["vs_vIdCatalogo"] = value; }
        }

        public string vClCatalogo
        {
            get { return (string)ViewState["vs_vClCatalogo"]; }
            set { ViewState["vs_vClCatalogo"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["ID_CATALOGO"] != null)
                {
                    vIdCatalogo = int.Parse(Request.QueryString["ID_CATALOGO"]);
                }

                if (Request.QueryString["mulSel"] != null)
                {
                    rgCatalogos.AllowMultiRowSelection = (Request.QueryString["mulSel"].ToString() == "1");
                    btnAgregar.Visible = (Request.QueryString["mulSel"].ToString() == "1");
                }


                vClCatalogo = Request.QueryString["CatalogoCl"];
                if (String.IsNullOrEmpty(vClCatalogo))
                    vClCatalogo = "CATALOGO";

            }
        }

        protected void rgCatalogos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            CatalogoValorNegocio nCatalogo = new CatalogoValorNegocio();
            var vCatalogo = nCatalogo.Obtener_C_CATALOGO_VALOR(ID_CATALOGO_LISTA: vIdCatalogo);
            rgCatalogos.DataSource = vCatalogo;

        }

        protected void rgCatalogos_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgCatalogos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgCatalogos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgCatalogos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgCatalogos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgCatalogos.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}