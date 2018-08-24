using Newtonsoft.Json;
using SIGE.Entidades;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.EO
{
    public partial class VentanaReporteCumplimientoPersonal : System.Web.UI.Page
    {

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        public int vIdPeriodo
        {
            get { return (int)ViewState["vsIdPeriodo"]; }
            set { ViewState["vsIdPeriodo"] = value; }
        }
        public int vIdEvaluado
        {
            get { return (int)ViewState["vsIdEvaluado"]; }
            set { ViewState["vsIdEvaluado"] = value; }
        }

        public List<E_SELECCION_PERIODOS_DESEMPENO> oLstPeriodos
        {
            get { return (List<E_SELECCION_PERIODOS_DESEMPENO>)ViewState["vs_lista_periodos"]; }
            set { ViewState["vs_lista_periodos"] = value; }
        }

        #endregion

        #region Funciones

        private void AgregarPeriodos(string pPeriodos)
        {
            List<E_SELECCION_PERIODOS_DESEMPENO> vLstPeriodos = JsonConvert.DeserializeObject<List<E_SELECCION_PERIODOS_DESEMPENO>>(pPeriodos);
            string vOrigen;

            foreach (E_SELECCION_PERIODOS_DESEMPENO item in vLstPeriodos)
            {
                if (item.clOrigen == "COPIA")
                    vOrigen = item.clOrigen + " " + item.clTipoCopia;
                else
                    vOrigen = item.clOrigen;

                if (oLstPeriodos.Where(t => t.idPeriodo == item.idPeriodo).Count() == 0)
                {
                    oLstPeriodos.Add(new E_SELECCION_PERIODOS_DESEMPENO
                    {
                        idPeriodo = item.idPeriodo,
                        clPeriodo = item.clPeriodo,
                        nbPeriodo = item.nbPeriodo,
                        dsPeriodo = item.dsPeriodo,
                        clOrigen = vOrigen.ToLower()
                    });

                    ContextoPeriodos.oLstPeriodosPersonal.Add(new E_SELECCION_PERIODOS_DESEMPENO
                    {
                        idPeriodo = item.idPeriodo,
                        idEvaluado = item.idEvaluado,
                        nbPeriodo = item.nbPeriodo
                    });

                }
            }

            rgComparativos.Rebind();
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["idPeriodo"] != null)
                {
                    vIdPeriodo = int.Parse(Request.QueryString["idPeriodo"]);
                }
                else
                {
                    vIdPeriodo = 0;
                }
                if (Request.Params["idEvaluado"] != null)
                {
                    vIdEvaluado = int.Parse(Request.QueryString["idEvaluado"]);
                }
                else
                {
                    vIdPeriodo = 0;
                }

                PeriodoDesempenoNegocio periodo = new PeriodoDesempenoNegocio();
                var oPeriodo = periodo.ObtienePeriodoDesempeno(pIdPeriodo: vIdPeriodo);

                if (oPeriodo != null)
                {
                    txtClPeriodo.InnerText = oPeriodo.CL_PERIODO;
                    txtNbPeriodo.InnerText = oPeriodo.DS_PERIODO;
                   // txtPeriodos.InnerText = oPeriodo.DS_PERIODO;
                    txtFechas.InnerText = oPeriodo.FE_INICIO.ToString("d") + " a " + oPeriodo.FE_TERMINO.Value.ToShortDateString();
                }
                var oEvaluado = periodo.ObtieneEvaluados(pIdPeriodo: vIdPeriodo, pIdEvaluado: vIdEvaluado, pClUsuario: vClUsuario, pNbPrograma: vNbPrograma).FirstOrDefault();
                if (oEvaluado != null)
                {
                    txtNoEmpleado.InnerText = oEvaluado.CL_EMPLEADO;
                    txtNbEmpleado.InnerText =  oEvaluado.NB_EMPLEADO_COMPLETO;
                    txtClPuesto.InnerText = oEvaluado.CL_PUESTO;
                    txtPuesto.InnerText = oEvaluado.NB_PUESTO;
                }

                //GRÁFICA
                List<E_META> vGraficaTemas = periodo.ObtieneMetasEvaluados(pIdPeriodo: vIdPeriodo, idEvaluado: vIdEvaluado).Select(s => new E_META { NO_META = s.NO_META.ToString(), DS_META = s.DS_META, PR_CUMPLIMIENTO = s.PR_CUMPLIMIENTO_META, COLOR_NIVEL = s.COLOR_NIVEL }).ToList();
                rhcCumplimientoPersonal.PlotArea.Series.Clear();
                ColumnSeries vSerie = new ColumnSeries();

                foreach (var item in vGraficaTemas)
                {
                    Color vColor = System.Drawing.ColorTranslator.FromHtml(item.COLOR_NIVEL);
                    vSerie.SeriesItems.Add(new CategorySeriesItem(item.PR_CUMPLIMIENTO, vColor));
                    vSerie.LabelsAppearance.DataFormatString = "{0:N2}%";
                    rhcCumplimientoPersonal.PlotArea.XAxis.Items.Add(item.NO_META);
                    rhcCumplimientoPersonal.PlotArea.XAxis.LabelsAppearance.DataFormatString = item.NO_META;
                    rhcCumplimientoPersonal.PlotArea.YAxis.LabelsAppearance.DataFormatString = "{0:N2}%";

                }

                ContextoPeriodos.oLstPeriodosPersonal = new List<E_SELECCION_PERIODOS_DESEMPENO>();
                ContextoPeriodos.oLstPeriodosPersonal.Add(new E_SELECCION_PERIODOS_DESEMPENO
                {
                    idPeriodo = vIdPeriodo,
                    idEvaluado = vIdEvaluado,
                    nbPeriodo = oPeriodo.NB_PERIODO
                });

                oLstPeriodos = new List<E_SELECCION_PERIODOS_DESEMPENO>();
                string vOrigenPeriodo;
                if (oPeriodo.CL_ORIGEN_CUESTIONARIO == "COPIA")
                    vOrigenPeriodo = oPeriodo.CL_ORIGEN_CUESTIONARIO + " " + oPeriodo.CL_TIPO_COPIA;
                else
                    vOrigenPeriodo = oPeriodo.CL_ORIGEN_CUESTIONARIO;

                oLstPeriodos.Add(new E_SELECCION_PERIODOS_DESEMPENO
                {
                    idPeriodo = vIdPeriodo,
                    clPeriodo = oPeriodo.CL_PERIODO,
                    nbPeriodo = oPeriodo.NB_PERIODO,
                    dsPeriodo = oPeriodo.DS_PERIODO,
                    clOrigen = vOrigenPeriodo.ToLower()
                });

                rhcCumplimientoPersonal.PlotArea.Series.Add(vSerie);
            }

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
        }

        protected void grdCumplimiento_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            List<SPE_OBTIENE_EO_METAS_EVALUADOS_Result> vCumplimiento = new List<SPE_OBTIENE_EO_METAS_EVALUADOS_Result>();
            PeriodoDesempenoNegocio neg = new PeriodoDesempenoNegocio();
            vCumplimiento = neg.ObtieneMetasEvaluados(idEvaluadoMeta: null, pIdPeriodo: vIdPeriodo, idEvaluado: vIdEvaluado, no_Meta: null, cl_nivel: null, FgEvaluar: true);
            grdCumplimiento.DataSource = vCumplimiento;
        }

        protected void grdResultados_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            List<SPE_OBTIENE_EO_METAS_EVALUADOS_Result> vResultados = new List<SPE_OBTIENE_EO_METAS_EVALUADOS_Result>();
            PeriodoDesempenoNegocio neg = new PeriodoDesempenoNegocio();
            vResultados = neg.ObtieneMetasEvaluados(idEvaluadoMeta: null, pIdPeriodo: vIdPeriodo, idEvaluado: vIdEvaluado, no_Meta: null, cl_nivel: null);
            grdResultados.DataSource = vResultados;
        }

        protected void rgComparativos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgComparativos.DataSource = oLstPeriodos;
        }

        protected void ramReportes_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            E_SELECTOR vSeleccion = new E_SELECTOR();
            string pParameter = e.Argument;

            if (pParameter != null)
                vSeleccion = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);

            if (vSeleccion.clTipo == "PERIODODESEMPENO")
                AgregarPeriodos(vSeleccion.oSeleccion.ToString());
        }

        protected void rgComparativos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgComparativos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgComparativos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgComparativos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgComparativos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgComparativos.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void rgComparativos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                GridDataItem item = e.Item as GridDataItem;
                int? vIdPeriodo = int.Parse(item.GetDataKeyValue("idPeriodo").ToString());
                oLstPeriodos.RemoveAll(w => w.idPeriodo == vIdPeriodo);
                ContextoPeriodos.oLstPeriodosPersonal.RemoveAll(w => w.idPeriodo == vIdPeriodo);
                rgComparativos.Rebind();
                
            }
        }
    }


}