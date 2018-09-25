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
    public partial class SelectorRequisiciones : System.Web.UI.Page
    {
        #region Variables

        public int? vIdCandidato
        {
            get { return (int?)ViewState["vs_vIdCandidato"]; }
            set { ViewState["vs_vIdCandidato"] = value; }
        }

        public string vClCatalogo
        {
            get { return (string)ViewState["vs_vClCatalogo"]; }
            set { ViewState["vs_vClCatalogo"] = value; }
        }

        private string vClTipoFiltro
        {
            get { return (string)ViewState["vs_vClTipoFiltro"]; }
            set { ViewState["vs_vClTipoFiltro"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                vClCatalogo = Request.QueryString["CatalogoCl"];
                if (String.IsNullOrEmpty(vClCatalogo))
                    vClCatalogo = "REQUISICION";

                vClTipoFiltro = Request.QueryString["CL_FILTRO_REQ"];

                if (Request.Params["CandidatoId"] != null)
                {
                    vIdCandidato = int.Parse(Request.Params["CandidatoId"].ToString());
                }
                else
                {
                    vIdCandidato = null;
                }

            }
        }

        protected void grdRequisiciones_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RequisicionNegocio nRequisicion = new RequisicionNegocio();
            grdRequisiciones.DataSource = nRequisicion.ObtieneRequisicion(pClEstado:vClTipoFiltro, pIdEmpresa: ContextoUsuario.oUsuario.ID_EMPRESA);
        }

        protected void grdRequisiciones_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdRequisiciones.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdRequisiciones.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdRequisiciones.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdRequisiciones.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdRequisiciones.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}