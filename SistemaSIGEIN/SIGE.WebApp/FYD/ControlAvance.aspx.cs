using SIGE.Entidades.Externas;
using SIGE.Negocio.FormacionDesarrollo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.WebApp.Comunes;
using Telerik.Web.UI;
using System.Xml.Linq;
using WebApp.Comunes;

namespace SIGE.WebApp.FYD
{
    public partial class ControlAvance : System.Web.UI.Page
    {
        #region Variables

        private int? vIdRol;
        private string vNbFirstRadEditorTagName = "p";

        public int vIdPeriodo {
            get { return (int)ViewState["vs_ca_id_periodo"]; }
            set { ViewState["vs_ca_id_periodo"] = value; }
        }

        #endregion

        #region Funciones

        private void cargarPeriodo()
        {
            ControlAvanceNegocio neg = new ControlAvanceNegocio();
            var periodo = neg.ObtenerPeriodoEvaluacion(vIdPeriodo);

            //txtNoPeriodo.InnerText = periodo.CL_PERIODO;
            //txtNbPeriodo.InnerText = periodo.DS_PERIODO;
            btnEnvioCuestionarios.Enabled = (periodo.CL_ESTADO_PERIODO.ToUpper() == "CERRADO") ? false : true;

<<<<<<< HEAD
            txtClPeriodo.InnerText = periodo.CL_PERIODO;
            txtDsPeriodo.InnerText = periodo.NB_PERIODO;
=======
            txtClPeriodo.InnerText = periodo.NB_PERIODO;
            txtDsPeriodo.InnerText = periodo.DS_PERIODO;
>>>>>>> DEV
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

        private void cargarDatosGrafica()
        {
            ControlAvanceNegocio neg = new ControlAvanceNegocio();
            var datos = neg.obtenerDatosControlAvance(vIdPeriodo, vIdRol);

            txtPorEvaluar.Text = datos.NO_PERSONAS_POR_EVALUAR.Value.ToString();
            txtEvaluadas.Text = datos.NO_PERSONAS_EVALUADAS.Value.ToString();
            txtTotalEvaluados.Text = datos.NO_TOTAL_EVALUADOS.Value.ToString();

            txtRespondidos.Text = datos.NO_CUESTINARIOS_RESPONDIDOS.Value.ToString();
            txtPorResponder.Text = datos.NO_CUESTIONARIOS_POR_RESPONDER.Value.ToString();
            txtTotalCuestionarios.Text = datos.NO_CUESTIONARIOS_TOTALES.Value.ToString();

            Telerik.Web.UI.PieSeries psEvaluados = new Telerik.Web.UI.PieSeries();
            psEvaluados.LabelsAppearance.Visible = false;
            psEvaluados.SeriesItems.Add(new Telerik.Web.UI.PieSeriesItem { Y = datos.NO_PERSONAS_EVALUADAS, BackgroundColor = ColorTranslator.FromHtml("#ED7D31"), Name = "Personas evaluadas" });
            psEvaluados.SeriesItems.Add(new Telerik.Web.UI.PieSeriesItem { Y = datos.NO_PERSONAS_POR_EVALUAR, BackgroundColor = ColorTranslator.FromHtml("#5B9BD5"), Name = "Personas por evaluar" });
            hcEvaluado.PlotArea.Series.Add(psEvaluados);


            Telerik.Web.UI.PieSeries psCuestionarios = new Telerik.Web.UI.PieSeries();
            psCuestionarios.LabelsAppearance.Visible = false;
            psCuestionarios.SeriesItems.Add(new Telerik.Web.UI.PieSeriesItem { Y = datos.NO_CUESTIONARIOS_POR_RESPONDER, BackgroundColor = ColorTranslator.FromHtml("#ED7D31"), Name = "Cuestionarios por responder" });
            psCuestionarios.SeriesItems.Add(new Telerik.Web.UI.PieSeriesItem { Y = datos.NO_CUESTINARIOS_RESPONDIDOS, BackgroundColor = ColorTranslator.FromHtml("#5B9BD5"), Name = "Cuestionarios respondidos" });
            hcCuestionarios.PlotArea.Series.Add(psCuestionarios);
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;

            if (!Page.IsPostBack)
            {
                vIdPeriodo = 0;

                if (Request.Params["idPeriodo"] != null)
                {
                    vIdPeriodo = int.Parse(Request.Params["idPeriodo"]);
                    cargarPeriodo();
                    cargarDatosGrafica();
                }

                btnVistaPrevia.Attributes.Add("style","display:none");
            }

        }

        protected void GridPorEvaluado_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {            
            ControlAvanceNegocio neg = new ControlAvanceNegocio();
            GridPorEvaluado.DataSource = neg.obtieneEmpleadosEvaluados(vIdPeriodo, ContextoUsuario.oUsuario.ID_EMPRESA, vIdRol);
        }

        protected void GridPorEvaluador_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {            
            ControlAvanceNegocio neg = new ControlAvanceNegocio();
            GridPorEvaluador.DataSource = neg.obtieneEmpleadosEvaluadores(vIdPeriodo, ContextoUsuario.oUsuario.ID_EMPRESA, vIdRol);
        }

        protected void GridPorEvaluado_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", GridPorEvaluado.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", GridPorEvaluado.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", GridPorEvaluado.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", GridPorEvaluado.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", GridPorEvaluado.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void GridPorEvaluador_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", GridPorEvaluador.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", GridPorEvaluador.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", GridPorEvaluador.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", GridPorEvaluador.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", GridPorEvaluador.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}