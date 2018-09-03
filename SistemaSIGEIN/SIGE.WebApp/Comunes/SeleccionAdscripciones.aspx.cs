using SIGE.Entidades;
using SIGE.Negocio.AdministracionSitio;
using SIGE.Negocio.EvaluacionOrganizacional;
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
    public partial class SeleccionAdscripciones : System.Web.UI.Page
    {

        #region Variables

        public string vClCatalogo
        {
            get { return (string)ViewState["vs_vClCatalogo"]; }
            set { ViewState["vs_vClCatalogo"] = value; }
        }

        public string vClLista
        {
            get { return (string)ViewState["vs_vClLista"]; }
            set { ViewState["vs_vClLista"] = value; }
        }

        private string vclTipoSeleccion
        {
            get { return (string)ViewState["vs_vclTipoSeleccion"]; }
            set { ViewState["vs_vclTipoSeleccion"] = value; }
        }

        #endregion Variables

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                vClCatalogo = Request.QueryString["CatalogoCl"];
                if (String.IsNullOrEmpty(vClCatalogo))
                    vClCatalogo = "ADSCRIPCION";

                string vTipoSeleccion = Request.QueryString["MultiSeleccion"];
                if (!String.IsNullOrEmpty(vTipoSeleccion) && vTipoSeleccion == "0")
                {
                    grdAdscripcion.AllowMultiRowSelection = false;
                    btnAgregar.Visible = false;
                }

                string  pclTipoSeleccion = Request.QueryString["CL_REFERENCIA"];
                if (!String.IsNullOrEmpty(pclTipoSeleccion))
                    vclTipoSeleccion = pclTipoSeleccion;

                string vTipoLista = Request.QueryString["ClLista"];
                if (!String.IsNullOrEmpty(vTipoLista))
                    vClLista = vTipoLista;
            }
        }

        protected void grdAdscripcion_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            List<SPE_OBTIENE_ADSCRIPCIONES_Result> ListaAdscripcion = new List<SPE_OBTIENE_ADSCRIPCIONES_Result>();
            RotacionPersonalNegocio negocio = new RotacionPersonalNegocio();
            ListaAdscripcion = negocio.ObtieneCatalogoAdscripciones();
            if(!String.IsNullOrEmpty(vclTipoSeleccion))
                grdAdscripcion.DataSource = ListaAdscripcion.Where(w=> w.CL_TABLA_REFERENCIA == vclTipoSeleccion).ToList();
            else
               grdAdscripcion.DataSource = ListaAdscripcion;
        }

        protected void grdAdscripcion_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdAdscripcion.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdAdscripcion.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdAdscripcion.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdAdscripcion.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdAdscripcion.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}