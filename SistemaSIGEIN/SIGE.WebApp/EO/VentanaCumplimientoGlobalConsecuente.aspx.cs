using SIGE.Entidades;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Entidades.Externas;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.EO
{
    public partial class VentanaCumplimientoGlobalConsecuente : System.Web.UI.Page
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

        public int vIdPeriodoConsecuente
        {
            get { return (int)ViewState["vsIdPeriodoConsecuente"]; }
            set { ViewState["vsIdPeriodoConsecuente"] = value; }
        }

        #endregion

        #region Funciones

        protected void GraficaDesempenoGlobal()
        {
            PeriodoDesempenoNegocio periodo = new PeriodoDesempenoNegocio();
            var vPrDesempenoGlobal = periodo.ObtienePeriodoConsecutivo(vIdPeriodoConsecuente).FirstOrDefault();
            rhcGraficaGlobal.PlotArea.Series.Clear();
            ColumnSeries vSerie = new ColumnSeries();
            if (vPrDesempenoGlobal.CUMPLIDO_ORIGINAL != null && vPrDesempenoGlobal.CUMPLIDO_CONSECUENTE != null)
            {
                vSerie.SeriesItems.Add(new CategorySeriesItem(vPrDesempenoGlobal.CUMPLIDO_ORIGINAL, System.Drawing.Color.Red));
                vSerie.SeriesItems.Add(new CategorySeriesItem(vPrDesempenoGlobal.CUMPLIDO_CONSECUENTE, System.Drawing.Color.Green));
            }
            vSerie.LabelsAppearance.DataFormatString = "{0:N0}%";
            rhcGraficaGlobal.PlotArea.XAxis.Items.Add(vPrDesempenoGlobal.NB_PERIODO_ORIGINAL);
            rhcGraficaGlobal.PlotArea.XAxis.Items.Add(vPrDesempenoGlobal.NB_PERIODO_CONSECUENTE);
            rhcGraficaGlobal.PlotArea.YAxis.LabelsAppearance.DataFormatString = "{0:N0}%";
            rhcGraficaGlobal.PlotArea.Series.Add(vSerie);
        }

        public string validarDsNotas(string vdsNotas)
        {
            E_NOTAS pNota = null;
            if (vdsNotas != null)
            {
                XElement vNotas = XElement.Parse(vdsNotas.ToString());
                if (ValidarRamaXml(vNotas, "NOTA"))
                {
                    pNota = vNotas.Elements("NOTA").Select(el => new E_NOTAS
                    {
                        DS_NOTA = UtilXML.ValorAtributo<string>(el.Attribute("DS_NOTA")),
                        FE_NOTA = (DateTime?)UtilXML.ValorAtributo(el.Attribute("FE_NOTA"), E_TIPO_DATO.DATETIME),
                    }).FirstOrDefault();
                }
                if (pNota.DS_NOTA != null)
                {
                    return pNota.DS_NOTA.ToString();
                }
                else return "";
            }
            else
            {
                return "";
            }
        }

        public Boolean ValidarRamaXml(XElement parentEl, string elementsName)
        {
            var foundEl = parentEl.Element(elementsName);
            if (foundEl != null)
            {
                return true;
            }
            return false;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["PeriodoId"] != null)
                {
                    vIdPeriodoConsecuente = int.Parse(Request.Params["PeriodoId"]);
                    PeriodoDesempenoNegocio periodo = new PeriodoDesempenoNegocio();
                    var oPeriodo = periodo.ObtienePeriodoConsecutivo(vIdPeriodoConsecuente).FirstOrDefault();
                    if (oPeriodo != null)
                    {
                        vIdPeriodo = (int)oPeriodo.ID_PERIODO_ORIGINAL;
                        txtClConsecuente.InnerText = oPeriodo.NB_PERIODO_CONSECUENTE;
                        txtClPeriodo.InnerText = oPeriodo.NB_PERIODO_ORIGINAL;
                    }

                    var oPeriodoDatos = periodo.ObtienePeriodoDesempenoContexto(vIdPeriodo, null);
                    if (oPeriodoDatos != null)
                    {
                        txtFechaOriginal.InnerText = oPeriodoDatos.FE_INICIO.ToString("d") + " a " + oPeriodoDatos.FE_TERMINO.Value.ToShortDateString();
                        txtTipoPeriodo.InnerText = oPeriodoDatos.CL_TIPO_PERIODO;
                        txtTipoBono.InnerText = oPeriodoDatos.CL_TIPO_BONO;
                        txtCapturaMetas.InnerText = oPeriodoDatos.CL_TIPO_CAPTURISTA;
                        if (oPeriodoDatos.DS_NOTAS != null)
                        {
                            XElement vNotas = XElement.Parse(oPeriodoDatos.DS_NOTAS);
                            if (vNotas != null)
                            {
                                txtNotasOriginal.InnerHtml = validarDsNotas(vNotas.ToString());
                            }
                        }
                    }

                    var oPeriodoConsecuente = periodo.ObtienePeriodoDesempenoContexto(vIdPeriodoConsecuente, null);
                    if (oPeriodoConsecuente != null)
                    {
                        txtFechaConsecuente.InnerText = oPeriodoConsecuente.FE_INICIO.ToString("d") + " a " + oPeriodoConsecuente.FE_TERMINO.Value.ToShortDateString();
                        txtTipoPeriodoConsecuente.InnerText = oPeriodoConsecuente.CL_TIPO_PERIODO;
                        txtTipoBonoConsecuente.InnerText = oPeriodoConsecuente.CL_TIPO_BONO;
                        txtCapturaMetasConsecuentes.InnerText = oPeriodoConsecuente.CL_TIPO_CAPTURISTA;
                        if (oPeriodoConsecuente.DS_NOTAS != null)
                        {
                            XElement vNotas = XElement.Parse(oPeriodoConsecuente.DS_NOTAS);
                            if (vNotas != null)
                            {
                                txtNotasConsecuente.InnerHtml = validarDsNotas(vNotas.ToString());
                            }
                        }
                    }
                }
                GraficaDesempenoGlobal();
            }
        }

        protected void rgEvaluados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PeriodoDesempenoNegocio periodo = new PeriodoDesempenoNegocio();
            List<SPE_OBTIENE_EO_PERIODOS_CONSECUTIVOS_Result> ListaPeriodos = new List<SPE_OBTIENE_EO_PERIODOS_CONSECUTIVOS_Result>();
            ListaPeriodos = periodo.ObtienePeriodoConsecutivo(vIdPeriodoConsecuente);
            rgEvaluados.DataSource = ListaPeriodos;
        }
    }
}