using SIGE.Negocio.TableroControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.Comunes
{
    public partial class SeleccionPeriodoParaTablero : System.Web.UI.Page
    {
        #region Variables

        public int vIdPeriodoTablero
        {
            get { return (int)ViewState["vs_id_periodo_tablero"]; }
            set { ViewState["vs_id_periodo_tablero"] = value; }
        }

        public string vClTipoPeriodo
        {
            get { return (string)ViewState["vs_cl_tipo_periodo"]; }
            set { ViewState["vs_cl_tipo_periodo"] = value; }
        }

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                vIdPeriodoTablero = 0;
                vClTipoPeriodo = "";

                if (Request.Params["IdPeriodoTablero"] != null)
                {
                    vIdPeriodoTablero = int.Parse(Request.Params["IdPeriodoTablero"].ToString());
                }

                if (Request.Params["ClTipoPeriodo"] != null)
                {
                    vClTipoPeriodo = Request.Params["ClTipoPeriodo"].ToString();
                }

            }
        }

        protected void grdPeriodos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            TableroControlNegocio nTableroControl = new TableroControlNegocio();
            grdPeriodos.DataSource = nTableroControl.ObtenerPeriodosParaTablero(vIdPeriodoTablero, vClTipoPeriodo);
        }

        protected void grdPeriodos_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdPeriodos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdPeriodos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdPeriodos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdPeriodos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdPeriodos.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}