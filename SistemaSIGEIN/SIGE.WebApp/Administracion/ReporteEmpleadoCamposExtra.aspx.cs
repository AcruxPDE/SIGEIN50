using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.Administracion
{
    public partial class ReporteEmpleadoCamposExtra : System.Web.UI.Page
    {

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private int? vIdRol;

        #endregion

        #region Funciones

        private void ExportarExcel()
        {
            grdCamposExtra.ExportSettings.OpenInNewWindow = true;
            foreach (GridColumn col in grdCamposExtra.MasterTableView.RenderColumns)
            {
                col.Display = true;
            }
            grdCamposExtra.Rebind();
            grdCamposExtra.MasterTableView.ExportToExcel();
        }

        private void DefineGrid()
        {
            XElement vXmlSeleccion = vTipoDeSeleccion("TODAS");
            EmpleadoNegocio nEmpleado = new EmpleadoNegocio();
            List<SPE_OBTIENE_EMPLEADOS_CAMPOS_EXTRA_Result> eEmpleados;
            eEmpleados = nEmpleado.ObtenerEmpleadosCamposExtra(pXmlSeleccion: vXmlSeleccion, pClUsuario: vClUsuario, pFgActivo: true, pID_EMPRESA: ContextoUsuario.oUsuario.ID_EMPRESA, pIdRol: vIdRol);
            CamposAdicionales cad = new CamposAdicionales();
            DataTable tEmpleados = cad.camposAdicionales(eEmpleados, "M_EMPLEADO_XML_CAMPOS_ADICIONALES", grdCamposExtra, "M_EMPLEADO");
            grdCamposExtra.DataSource = tEmpleados;
        }

        public XElement vTipoDeSeleccion(string pTipoSeleccion)
        {
            XElement vXmlSeleccion = new XElement("SELECCION", new XElement("FILTRO", new XAttribute("CL_TIPO", "TODAS")));
            switch (pTipoSeleccion)
            {
                case "TODAS":
                    break;
            }
            return vXmlSeleccion;
        }

        #endregion

        protected void Page_Init(object sender, System.EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;
            DefineGrid();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void grdCamposExtra_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "ExportToExcel":
                    ExportarExcel();
                    break;
                default:
                    break;
            }
        }

        protected void grdCamposExtra_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdCamposExtra.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdCamposExtra.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdCamposExtra.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdCamposExtra.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdCamposExtra.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}