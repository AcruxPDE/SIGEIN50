using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.Administracion
{
    public partial class ReportesSueldoEmpleado : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void grdSueldoEmpleados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            EmpleadoNegocio sueldoEmpleado = new EmpleadoNegocio();
            List<SPE_OBTIENE_SUELDO_EMPLEADOS_Result> listaSueldoEmpleados = new List<SPE_OBTIENE_SUELDO_EMPLEADOS_Result>();
            listaSueldoEmpleados = sueldoEmpleado.ObtenerSueldoEmpleados();
            grdSueldoEmpleados.DataSource = listaSueldoEmpleados;
        }

        protected void grdSueldoEmpleados_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
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

        private void ExportarExcel()
        {
            grdSueldoEmpleados.ExportSettings.OpenInNewWindow = true;
            foreach (GridColumn col in grdSueldoEmpleados.MasterTableView.RenderColumns)
            {
                col.Display = true;
            }
            grdSueldoEmpleados.Rebind();
            grdSueldoEmpleados.MasterTableView.ExportToExcel();
        }

        protected void grdSueldoEmpleados_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdSueldoEmpleados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdSueldoEmpleados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdSueldoEmpleados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdSueldoEmpleados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdSueldoEmpleados.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}