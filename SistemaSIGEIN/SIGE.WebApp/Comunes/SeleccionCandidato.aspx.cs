using SIGE.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.Comunes
{
    public partial class SeleccionCandidato : System.Web.UI.Page
    {

        #region Variables

        public string vClTipoSeleccion
        {
            get { return (string)ViewState["vs_vClTipoSeleccion"]; }
            set { ViewState["vs_vClTipoSeleccion"] = value; }
        }

        public string vClCatalogo
        {
            get { return (string)ViewState["vs_vClCatalogo"]; }
            set { ViewState["vs_vClCatalogo"] = value; }
        }

        #endregion

        #region Funciones

        public XElement TipoSeleccion()
        {
            return new XElement("SELECCION", new XElement("FILTRO", new XAttribute("CL_TIPO", vClTipoSeleccion)));
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                vClCatalogo = Request.QueryString["CatalogoCl"];
                if (String.IsNullOrEmpty(vClCatalogo))
                    vClCatalogo = "CANDIDATO";

                if (!String.IsNullOrEmpty(Request.QueryString["mulSel"]))
                {
                    grdCandidatos.AllowMultiRowSelection = (Request.QueryString["mulSel"] == "1");
                    btnAgregar.Visible = (Request.QueryString["mulSel"] == "1");
                }
                else
                {
                    grdCandidatos.AllowMultiRowSelection = false;
                    btnAgregar.Visible = false;
                }

                if (Request.Params["vClTipoSeleccion"] != null)
                {
                    vClTipoSeleccion = Request.Params["vClTipoSeleccion"].ToString();
                }
                else
                {
                    vClTipoSeleccion = "TODAS";
                }
            }
        }

        protected void grdCandidatos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            XElement vXmlFiltro = TipoSeleccion();

            CandidatoNegocio nCandidato = new CandidatoNegocio();
            var Candidatos = nCandidato.ObtieneCandidato(pXmlSeleccion: vXmlFiltro.ToString());
            grdCandidatos.DataSource = Candidatos;
        }

        protected void grdCandidatos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdCandidatos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdCandidatos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdCandidatos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdCandidatos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdCandidatos.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

    }
}