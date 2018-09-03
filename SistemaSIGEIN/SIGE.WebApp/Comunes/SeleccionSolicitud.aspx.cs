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
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.Comunes
{
    public partial class SeleccionSolicitud : System.Web.UI.Page
    {


        #region Variables

        public string vClTipoSeleccion
        {
            get { return (string)ViewState["vs_vClTipoSeleccion"]; }
            set { ViewState["vs_vClTipoSeleccion"] = value; }
        }


        #endregion


        #region Funciones


        private void DefineGrid()
        {
            //EmpleadoNegocio nEmpleado = new EmpleadoNegocio();
            SolicitudNegocio nSolicitud = new SolicitudNegocio();
            List<SPE_OBTIENE_SOLICITUDES_Result> vSolicitudes;
            vSolicitudes = nSolicitud.ObtieneCatalogoSolicitudes(TipoSeleccion());
            CamposAdicionales cad = new CamposAdicionales();
            DataTable tSolicitudes = cad.camposAdicionales(vSolicitudes, "M_EMPLEADO_XML_CAMPOS_ADICIONALES", grdSolicitudes, "M_EMPLEADO");
            grdSolicitudes.DataSource = tSolicitudes;
        }

        public string TipoSeleccion()
        {
            return (new XElement("SELECCION", new XElement("FILTRO", new XAttribute("CL_TIPO", vClTipoSeleccion)))).ToString();
        }

        #endregion

        protected void Page_Init(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["vClTipoSeleccion"] != null)
                {
                    vClTipoSeleccion = Request.Params["vClTipoSeleccion"].ToString();
                }
                else
                {
                    vClTipoSeleccion = "TODAS";
                }
            }

            DefineGrid();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                grdSolicitudes.AllowMultiRowSelection = false;
                if (!String.IsNullOrEmpty(Request.QueryString["mulSel"]))
                {
                    grdSolicitudes.AllowMultiRowSelection = (Request.QueryString["mulSel"] == "1");
                    btnAgregar.Visible = (Request.QueryString["mulSel"] == "1");
                }
            }
        }

        protected void ftrGrdSolicitudes_PreRender(object sender, EventArgs e)
        {
            var menu = ftrGrdSolicitudes.FindControl("rfContextMenu") as RadContextMenu;
            menu.DefaultGroupSettings.Height = Unit.Pixel(500);
            menu.EnableAutoScroll = true;
        }

        protected void grdSolicitudes_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdSolicitudes.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdSolicitudes.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdSolicitudes.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdSolicitudes.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdSolicitudes.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

    }
}