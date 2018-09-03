using SIGE.Negocio.AdministracionSitio;
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
    public partial class SeleccionPlaza : System.Web.UI.Page
    {
        private int? vIdEmpresa;
        private int? vIdRol;

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

        public int vIdPlaza
        {
            get { return (int)ViewState["vs_vIdPlaza"]; }
            set { ViewState["vs_vIdPlaza"] = value; }
        }

        public int? vIdRequisicion
        {
            get { return (int?)ViewState["vs_vIdRequisicion"]; }
            set { ViewState["vs_vIdRequisicion"] = value; }
        }

        public XElement vXmlTipoSeleccion
        {
            get { return XElement.Parse((string)(ViewState["vs_vXmlTipoSeleccion"] ?? new XElement("SELECCION").ToString())); }
            set { ViewState["vs_vXmlTipoSeleccion"] = value.ToString(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;

            if (!Page.IsPostBack)
            {


                grdPlazas.AllowMultiRowSelection = true;
                if (!String.IsNullOrEmpty(Request.QueryString["mulSel"]))
                {
                    grdPlazas.AllowMultiRowSelection = (Request.QueryString["mulSel"] == "1");
                    btnAgregar.Visible = (Request.QueryString["mulSel"] == "1");
                }

                vClCatalogo = Request.QueryString["CatalogoCl"];
                if (String.IsNullOrEmpty(vClCatalogo))
                    vClCatalogo = "PLAZA";

                vClTipoSeleccion = Request.QueryString["TipoSeleccionCl"];
                if (String.IsNullOrEmpty(vClTipoSeleccion))
                    vClTipoSeleccion = "TODAS";

                if (Request.Params["RequisicionId"] != null)
                {
                    vIdRequisicion = int.Parse(Request.Params["RequisicionId"].ToString());

                }
                else
                {
                    vIdRequisicion = null;
                }

                int vsIdPlaza = 0;
                int.TryParse(Request.QueryString["PlazaId"], out vsIdPlaza);

                vIdPlaza = vsIdPlaza;

                XElement vXmlSeleccion = new XElement("SELECCION", new XAttribute("CL_TIPO", vClTipoSeleccion));
                switch (vClTipoSeleccion)
                {
                    case "TODAS":
                    case "ACTIVAS":
                    case "VACANTES":
                        break;
                    case "JEFE":
                        XAttribute vXmlIdPlazaSubordinado = new XAttribute("ID_PLAZA_SUBORDINADO", vIdPlaza);
                        vXmlSeleccion.Add(vXmlIdPlazaSubordinado);
                        break;
                }

                vXmlTipoSeleccion = vXmlSeleccion;

                if (vIdRequisicion != null)
                {
                    XAttribute vXmlIdRequisicion = new XAttribute("ID_REQUISICION", vIdRequisicion);
                    vXmlTipoSeleccion.Add(vXmlIdRequisicion);
                }

            }
        }

        protected void grdPlazas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PlazaNegocio nPlaza = new PlazaNegocio();
            grdPlazas.DataSource = nPlaza.ObtienePlazas(pXmlSeleccion: vXmlTipoSeleccion, pID_EMPRESA: vIdEmpresa, pID_ROL: vIdRol);
        }

        protected void grdPlazas_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdPlazas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdPlazas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdPlazas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdPlazas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdPlazas.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}