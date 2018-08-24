using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using SIGE.Entidades.Externas;
using SIGE.WebApp.Comunes;
using System.Xml.Linq;

namespace SIGE.WebApp.EO
{
    public partial class HistorialBaja : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void grdHistorialBaja_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RotacionPersonalNegocio nRotacion = new RotacionPersonalNegocio();
            var vRotacion = nRotacion.ObtienePeriodosClima().Select(s => new E_HISTORIAL_BAJA
            {
                CL_EMPLEADO = s.CL_EMPLEADO,
                CL_ESTADO = s.CL_ACTIVO,
                DS_COMENTARIO = s.DS_COMENTARIOS,
                FECHA_BAJA = s.FE_BAJA_EFECTIVA,
                FECHA_REINGRESO = s.FE_REINGRESO,
                NB_EMPLEADO = s.NB_EMPLEADO,
                NB_CAUSA_BAJA = s.NB_CAUSA_BAJA,
            });
            grdHistorialBaja.DataSource = vRotacion;
        }

        protected void grdHistorialBaja_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdHistorialBaja.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdHistorialBaja.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdHistorialBaja.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdHistorialBaja.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdHistorialBaja.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}