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
    public partial class SeleccionCampoAdicional : System.Web.UI.Page
    {
        public string vClCatalogo
        {
            get { return (string)ViewState["vs_vClCatalogo"]; }
            set { ViewState["vs_vClCatalogo"] = value; }
        }

        public string vClTipoFormulario
        {
            get { return (string)ViewState["vs_vClTipoFormulario"]; }
            set { ViewState["vs_vClTipoFormulario"] = value; }
        }

        public bool? vFgSistema
        {
            get { return (bool?)ViewState["vs_vFgSistema"]; }
            set { ViewState["vs_vFgSistema"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                grdCamposAdicionales.AllowMultiRowSelection = true;
                if (!String.IsNullOrEmpty(Request.QueryString["mulSel"]))
                {
                    grdCamposAdicionales.AllowMultiRowSelection = (Request.QueryString["mulSel"] == "1");
                    btnAgregar.Visible = (Request.QueryString["mulSel"] == "1");
                }

                vClTipoFormulario = Request.QueryString["TipoFormularioCl"];
                if (String.IsNullOrEmpty(vClTipoFormulario) || vClTipoFormulario == "TODOS")
                    vClTipoFormulario = null;

                string vFgSistemaQS = Request.QueryString["SistemaFg"];
                if (!String.IsNullOrEmpty(vFgSistemaQS))
                    vFgSistema = vFgSistemaQS == "1";
                else
                    vFgSistema = null;

                vClCatalogo = Request.QueryString["CatalogoCl"];
                if (String.IsNullOrEmpty(vClCatalogo))
                    vClCatalogo = "CAMPOADICIONAL";
            }
        }

        protected void grdCamposAdicionales_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            CampoFormularioNegocio nCampoAdicional = new CampoFormularioNegocio();
            grdCamposAdicionales.DataSource = nCampoAdicional.ObtieneCamposFormularios(pClFormulario: vClTipoFormulario, pFgSistema: vFgSistema);
        }

        protected void grdCamposAdicionales_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdCamposAdicionales.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdCamposAdicionales.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdCamposAdicionales.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdCamposAdicionales.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdCamposAdicionales.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}