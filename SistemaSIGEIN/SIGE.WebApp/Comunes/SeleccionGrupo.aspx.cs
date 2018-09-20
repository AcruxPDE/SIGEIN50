using SIGE.Negocio.AdministracionSitio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.Comunes
{
    public partial class SeleccionGrupo : System.Web.UI.Page
    {
        public int vIdGrupo
        {
            get { return (int)ViewState["vs_vIdGrupo"]; }
            set { ViewState["vs_vIdGrupo"] = value; }
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
                vClCatalogo = Request.QueryString["CatalogoCl"];
                if (String.IsNullOrEmpty(vClCatalogo))
                    vClCatalogo = "GRUPO";

                int vIdPeriodoQS;
                if (int.TryParse(Request.QueryString["GrupoId"], out vIdPeriodoQS))
                    vIdGrupo = vIdPeriodoQS;

                rgGrupos.AllowMultiRowSelection = true;
                if (!String.IsNullOrEmpty(Request.QueryString["mulSel"]))
                {
                    rgGrupos.AllowMultiRowSelection = (Request.QueryString["mulSel"] == "1");
                    btnAgregar.Visible = (Request.QueryString["mulSel"] == "1");
                }
            }
        }

        protected void rgGrupos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GruposNegocio oNegocio = new GruposNegocio();
            rgGrupos.DataSource = oNegocio.ObtieneGrupos();
        }

        protected void rgGrupos_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgGrupos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgGrupos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgGrupos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgGrupos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgGrupos.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}