using SIGE.Entidades;
using SIGE.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Telerik.Web.UI;
using System.Xml.Linq;

namespace SIGE.WebApp.Comunes
{
    public partial class SeleccionPuesto : System.Web.UI.Page
    {
        public string vClCatalogo
        {
            get { return (string)ViewState["vs_vClCatalogo"]; }
            set { ViewState["vs_vClCatalogo"] = value; }
        }

        public string vClTipoSeleccion
        {
            get { return (string)ViewState["vs_vClTipoSeleccion"]; }
            set { ViewState["vs_vClTipoSeleccion"] = value; }
        }

        public string vClTipoPuesto
        {
            get { return (string)ViewState["vs_vClTipoPuesto"]; }
            set { ViewState["vs_vClTipoPuesto"] = value; }
        }

        private int? vIdEmpresa;

        private int? vIdRol;

        public XElement vXmlTipoSeleccion
        {
            get { return XElement.Parse((string)(ViewState["vs_vXmlTipoSeleccion"])); }
            set { ViewState["vs_vXmlTipoSeleccion"] = value.ToString(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;
            if (!Page.IsPostBack)
            {

                grdPuesto.AllowMultiRowSelection = true;
                if (!String.IsNullOrEmpty(Request.QueryString["mulSel"]))
                {
                    grdPuesto.AllowMultiRowSelection = (Request.QueryString["mulSel"] == "1");
                    btnAgregar.Visible = (Request.QueryString["mulSel"] == "1");
                }
                else
                {
                    grdPuesto.AllowMultiRowSelection = false;
                    btnAgregar.Visible = false;
                }

                vClCatalogo = Request.QueryString["CatalogoCl"];
                if (String.IsNullOrEmpty(vClCatalogo))
                    vClCatalogo = "PUESTO";

                vClTipoPuesto = Request.QueryString["vClTipoPuesto"];
                if (String.IsNullOrEmpty(vClTipoPuesto))
                    vClTipoPuesto = "TODOS";

                vClTipoSeleccion = Request.QueryString["vClTipoSeleccion"];
                if (string.IsNullOrEmpty(vClTipoSeleccion))
                    vClTipoSeleccion = "TODOS";

                vXmlTipoSeleccion = new XElement("SELECCION",
                                                    new XElement("FILTRO",
                                                    new XAttribute("CL_TIPO", vClTipoSeleccion),
                                                    new XAttribute("CL_TIPO_PUESTO", vClTipoPuesto)));
            }
        }

        protected void grdPuesto_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PuestoNegocio nPuesto = new PuestoNegocio();
            var vPuesto = nPuesto.ObtienePuestos(XML_PUESTOS_SELECCIONADOS: vXmlTipoSeleccion, ID_EMPRESA: vIdEmpresa, ID_ROL: vIdRol);
            grdPuesto.DataSource = vPuesto;
        }

        protected void grdPuesto_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdPuesto.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdPuesto.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdPuesto.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdPuesto.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdPuesto.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}