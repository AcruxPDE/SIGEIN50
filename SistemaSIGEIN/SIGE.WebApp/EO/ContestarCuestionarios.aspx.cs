
using System;
using System.Collections.Generic;
using System.Web.UI;
using SIGE.Entidades;
using SIGE.Negocio.EvaluacionOrganizacional;
using Telerik.Web.UI;
using SIGE.WebApp.Comunes;

namespace SIGE.WebApp.EO
{
    public partial class ContestarCuestionarios : System.Web.UI.Page
    {
        private int pIdPeriodo
        {
            get { return (int)ViewState["vs_pIdPeriodo"]; }
            set { ViewState["vs_pIdPeriodo"] = value; }
        }

        public string vEstadoPeriodo
        {
            get { return (string)ViewState["vs_vEstadoPeriodo"]; }
            set { ViewState["vs_vEstadoPeriodo"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["PeriodoId"] != null)
                {
                    pIdPeriodo = int.Parse((Request.QueryString["PeriodoId"]));
                    txtNoPeriodo.InnerText = pIdPeriodo.ToString();
                    txtNbPeriodo.InnerText = Request.QueryString["PeriodoNb"].ToString();
                    vEstadoPeriodo = Request.QueryString["EstadoPeriodo"].ToString();
                }
            }
        }

        protected void grdCuestionarios_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
           PeriodoDesempenoNegocio negocioEval = new PeriodoDesempenoNegocio();
            grdCuestionarios.DataSource = negocioEval.ObtieneEvaluadoresEvaluacionOrganizacional(pIdPeriodo:pIdPeriodo,pID_EMPRESA:ContextoUsuario.oUsuario.ID_EMPRESA);
            RadProgressBar a = new RadProgressBar();
            a.Value = 0;
        }

        protected void btnCuestionarios_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in grdCuestionarios.SelectedItems)
            {
                int vID_EVALUADOR = (int.Parse(item.GetDataKeyValue("ID_EVALUADOR").ToString()));
                Response.Redirect("EvaluacionCompetencia/Cuestionarios.aspx?ID_EVALUADOR=" + vID_EVALUADOR);
            }
        }

        public double toDouble(int? pNoValor)
        {
            if (pNoValor != null)
                return (double)pNoValor;
            else
                return 0;
        }

        protected void ramContestarCuestionarios_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            grdCuestionarios.Rebind();
        }

        protected void grdCuestionarios_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdCuestionarios.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdCuestionarios.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdCuestionarios.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdCuestionarios.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdCuestionarios.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}