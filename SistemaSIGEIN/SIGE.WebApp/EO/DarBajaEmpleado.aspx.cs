using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Negocio.Administracion;
using Telerik.Web.UI;
using SIGE.WebApp.Comunes;

namespace SIGE.WebApp.EO
{
    public partial class DarBajaEmpleado : System.Web.UI.Page
    {
        private int? vIdEmpresa;
        private int? vIdRol;

        protected void Page_Load(object sender, EventArgs e)
        {
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;
        }

        protected void rgEmpleadosBaja_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            EmpleadoNegocio nEmpleado = new EmpleadoNegocio();
            var vEmpleado = nEmpleado.ObtenerEmpleados(pFgActivo: true, pID_EMPRESA: vIdEmpresa, pID_ROL: vIdRol);
            rgEmpleadosBaja.DataSource = vEmpleado;
        }

        protected void rgEmpleadosBaja_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgEmpleadosBaja.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgEmpleadosBaja.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgEmpleadosBaja.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgEmpleadosBaja.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgEmpleadosBaja.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}