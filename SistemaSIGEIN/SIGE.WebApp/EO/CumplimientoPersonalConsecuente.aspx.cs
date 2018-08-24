using SIGE.Entidades;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Entidades.Externas;
using SIGE.Negocio.EvaluacionOrganizacional;
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
    public partial class CumplimientoPersonalConsecuente : System.Web.UI.Page
    {

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int? vIdPOriginal
        {
            get { return (int)ViewState["vsIdPOriginal"]; }
            set { ViewState["vsIdPOriginal"] = value; }
        }

        public int? vIdPConsecuente
        {
            get { return (int)ViewState["vsIdPConsecuente"]; }
            set { ViewState["vsIdPConsecuente"] = value; }
        }

        public int? vIdEvaOriginal
        {
            get { return (int)ViewState["vsIdEvaOriginal"]; }
            set { ViewState["vsIdEvaOriginal"] = value; }
        }

        public int? vIdEvaConsecuente
        {
            get { return (int)ViewState["vsIdEvaConsecuente"]; }
            set { ViewState["vsIdEvaConsecuente"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["idPeriodoOriginal"] != null)
                {
                    vIdPOriginal = int.Parse(Request.Params["idPeriodoOriginal"]);
                    vIdPConsecuente = int.Parse(Request.Params["idPeriodoConsecuente"]);
                    vIdEvaOriginal = int.Parse(Request.Params["IdEvalOriginal"]);
                    vIdEvaConsecuente = int.Parse(Request.Params["IdEvalConsecuente"]);
                    PeriodoDesempenoNegocio periodo = new PeriodoDesempenoNegocio();
                    var oPeriodo = periodo.ObtienePeriodoDesempeno(pIdPeriodo: (int)vIdPOriginal);
                    if (oPeriodo != null)
                    {
                        txtClPeriodoOriginal.InnerText = oPeriodo.CL_PERIODO;
                        txtNbPeriodo.InnerText = oPeriodo.DS_PERIODO;
                        txtPeriodosOriginal.InnerText = oPeriodo.DS_PERIODO;
                        txtFechas.InnerText = oPeriodo.FE_INICIO.ToString() + " a " + oPeriodo.FE_TERMINO.Value.ToShortDateString();
                    }
                    var oEvaluado = periodo.ObtieneEvaluados(pIdPeriodo: vIdPOriginal, pIdEvaluado: vIdEvaOriginal).FirstOrDefault();
                    if (oEvaluado != null)
                    {
                        txtClEmpleado.InnerText = oEvaluado.ID_EVALUADO + " " + oEvaluado.NB_EMPLEADO_COMPLETO;
                        txtPuestoOriginal.InnerText = oEvaluado.NB_PUESTO;
                    }
                    oPeriodo = periodo.ObtienePeriodoDesempeno(pIdPeriodo: (int)vIdPConsecuente);
                    if (oPeriodo != null)
                    {
                        txtClPeriodoConsecuente.InnerText = oPeriodo.CL_PERIODO;
                        txtNbConsecuente.InnerText = oPeriodo.DS_PERIODO;
                        txtPeriodosConsecuente.InnerText = oPeriodo.DS_PERIODO;
                        txtFechasConsecuentes.InnerText = oPeriodo.FE_INICIO.ToString() + " a " + oPeriodo.FE_TERMINO.Value.ToShortDateString();
                    }
                    oEvaluado = periodo.ObtieneEvaluados(pIdPeriodo: vIdPOriginal, pIdEvaluado: vIdEvaOriginal).FirstOrDefault();
                    if (oEvaluado != null)
                    {
                        txtPuestoConsecuente.InnerText = oEvaluado.NB_PUESTO;
                    }
                    //GRÁFICA
                    PeriodoDesempenoNegocio NMetas = new PeriodoDesempenoNegocio();
                    List<SPE_OBTIENE_EO_METAS_EVALUADOS_CONSECUENTES_Result> LCumpPersonal = new List<SPE_OBTIENE_EO_METAS_EVALUADOS_CONSECUENTES_Result>();
                    LCumpPersonal = NMetas.ObtieneMetasConsecuentes(vIdPOriginal, vIdPConsecuente, vIdEvaOriginal, vIdEvaConsecuente);
                    rhcCumplimientoPersonal.PlotArea.Series.Clear();
                    ColumnSeries vSerie = new ColumnSeries();

                    foreach (var item in LCumpPersonal)
                    {
                        Color vColor = System.Drawing.ColorTranslator.FromHtml(item.COLOR_NIVEL_ORIGINAL);
                        vSerie.SeriesItems.Add(new CategorySeriesItem(item.PR_CUMPLIMIENTO_META_ORIGINAL, vColor));
                        vSerie.SeriesItems.Add(new CategorySeriesItem(item.PR_CUMPLIMIENTO_META_CONSECUENTE, vColor));
                        vSerie.LabelsAppearance.DataFormatString = "{0:N2}%";
                        rhcCumplimientoPersonal.PlotArea.XAxis.Items.Add(item.NO_META_ORIGINAL.ToString());
                        rhcCumplimientoPersonal.PlotArea.XAxis.LabelsAppearance.DataFormatString = item.NO_META_ORIGINAL.ToString();
                        rhcCumplimientoPersonal.PlotArea.XAxis.Items.Add(item.NO_META_CONSECUENTE.ToString());
                        rhcCumplimientoPersonal.PlotArea.XAxis.LabelsAppearance.DataFormatString = item.NO_META_CONSECUENTE.ToString();
                        rhcCumplimientoPersonal.PlotArea.YAxis.LabelsAppearance.DataFormatString = "{0:N2}%";
                    }
                    rhcCumplimientoPersonal.PlotArea.Series.Add(vSerie);
                }
            }
        }

        protected void grdCumplimiento_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PeriodoDesempenoNegocio NMetas = new PeriodoDesempenoNegocio();
            List<SPE_OBTIENE_EO_METAS_EVALUADOS_CONSECUENTES_Result> LCumpPersonal = new List<SPE_OBTIENE_EO_METAS_EVALUADOS_CONSECUENTES_Result>();
            LCumpPersonal = NMetas.ObtieneMetasConsecuentes(vIdPOriginal, vIdPConsecuente, vIdEvaOriginal, vIdEvaConsecuente);
            grdCumplimiento.DataSource = LCumpPersonal;
        }

        protected void grdResultados_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            PeriodoDesempenoNegocio NMetas = new PeriodoDesempenoNegocio();
            List<SPE_OBTIENE_EO_METAS_EVALUADOS_CONSECUENTES_Result> LCumpPersonal = new List<SPE_OBTIENE_EO_METAS_EVALUADOS_CONSECUENTES_Result>();
            LCumpPersonal = NMetas.ObtieneMetasConsecuentes(vIdPOriginal, vIdPConsecuente, vIdEvaOriginal, vIdEvaConsecuente);
            grdResultados.DataSource = LCumpPersonal;
        }
    }
}