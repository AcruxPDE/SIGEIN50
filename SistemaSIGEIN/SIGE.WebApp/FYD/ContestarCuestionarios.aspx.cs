
using System;
using System.Collections.Generic;
using System.Web.UI;
using SIGE.Entidades;
using SIGE.Negocio.FormacionDesarrollo;
using Telerik.Web.UI;
using SIGE.WebApp.Comunes;
using WebApp.Comunes;
using System.Xml.Linq;

namespace SIGE.WebApp.FYD
{
    public partial class ContestarCuestionarios : System.Web.UI.Page
    {
        private string vNbFirstRadEditorTagName = "p";

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

        private int? vIdRol;

        protected void Page_Load(object sender, EventArgs e)
        {
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;

            if (!Page.IsPostBack)
            {
                if (Request.Params["PeriodoId"] != null)
                {


                    pIdPeriodo = int.Parse((Request.QueryString["PeriodoId"]));
                    ControlAvanceNegocio neg = new ControlAvanceNegocio();
                    var periodo = neg.ObtenerPeriodoEvaluacion(pIdPeriodo);

                    //txtNoPeriodo.InnerText = periodo.CL_PERIODO;
                    //txtNbPeriodo.InnerText = periodo.DS_PERIODO;
                    vEstadoPeriodo = Request.QueryString["EstadoPeriodo"].ToString();
                    txtClPeriodo.InnerText = periodo.CL_PERIODO;
                    txtDsPeriodo.InnerText = periodo.NB_PERIODO;
                    txtEstatus.InnerText = periodo.CL_ESTADO_PERIODO;
                    string vTiposEvaluacion = "";

                    if (periodo.FG_AUTOEVALUACION)
                    {
                        vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Autoevaluación" : String.Join(", ", vTiposEvaluacion, "Autoevaluacion");
                    }

                    if (periodo.FG_SUPERVISOR)
                    {
                        vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Superior" : String.Join(", ", vTiposEvaluacion, "Superior");
                    }

                    if (periodo.FG_SUBORDINADOS)
                    {
                        vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Subordinado" : String.Join(", ", vTiposEvaluacion, "Subordinado");
                    }

                    if (periodo.FG_INTERRELACIONADOS)
                    {
                        vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Interrelacionado" : String.Join(", ", vTiposEvaluacion, "Interrelacionado");
                    }

                    if (periodo.FG_OTROS_EVALUADORES)
                    {
                        vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Otros" : String.Join(", ", vTiposEvaluacion, "Otros");
                    }

                    txtTipoEvaluacion.InnerText = vTiposEvaluacion;

                    if (periodo.DS_NOTAS != null)
                    {
                        if (periodo.DS_NOTAS.Contains("DS_NOTA"))
                        {
                            txtNotas.InnerHtml = Utileria.MostrarNotas(periodo.DS_NOTAS);
                        }
                        else
                        {
                            XElement vNotas = XElement.Parse(periodo.DS_NOTAS);
                            if (vNotas != null)
                            {
                                vNotas.Name = vNbFirstRadEditorTagName;
                                txtNotas.InnerHtml = vNotas.ToString();
                            }
                        }
                    }
                }
            }
        }

        protected void grdCuestionarios_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            CuestionarioNegocio negocioEval = new CuestionarioNegocio();
            grdCuestionarios.DataSource = negocioEval.ObtieneEvaluadores(pIdPeriodo: pIdPeriodo, pID_EMPRESA: ContextoUsuario.oUsuario.ID_EMPRESA, pID_ROL: vIdRol);
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