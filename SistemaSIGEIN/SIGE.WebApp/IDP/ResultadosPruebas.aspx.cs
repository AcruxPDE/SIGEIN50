using SIGE.Entidades;
using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Drawing;
using System.Xml.Linq;
using SIGE.Negocio.Utilerias;
using SIGE.Entidades.Externas;


namespace SIGE.WebApp.IDP
{
    public partial class ResultadosPruebas : System.Web.UI.Page
    {

        #region Variables 

        List<E_RESULTADOS_BATERIA> vResultados
        {
            get { return (List<E_RESULTADOS_BATERIA>)ViewState["vResultados"]; }
            set { ViewState["vResultados"] = value; }
        }
        public int vIdBateria
        {
            get { return (int)ViewState["vsIdBateria"]; }
            set { ViewState["vsIdBateria"] = value; }
        }
        public Guid vClToken
        {
            get { return (Guid)ViewState["vsClToken"]; }
            set { ViewState["vsClToken"] = value; }
        }
        private String vPosicionInicialTab
        {
            get { return (String)ViewState["vPosicionInicialTab"]; }
            set { ViewState["vPosicionInicialTab"] = value; }
        }
        public Boolean vHabilitaLaboralI
        {
            get { return (Boolean)ViewState["vHabilitaLaboralI"]; }
            set { ViewState["vHabilitaLaboralI"] = value; }
        }
        public String grafica 
        {
            get { return (String)ViewState["vGrafica"]; }
            set { ViewState["vGrafica"] = value; }
        }
        public String graficaTIVA
        {
            get { return (String)ViewState["vGraficaTIVA"]; }
            set { ViewState["vGraficaTIVA"] = value; }
        }
        decimal? ORTOGRAFIA1_TOTAL;
        decimal? ORTOGRAFIA1_ACIERTOS;
        decimal? ORTOGRAFIA2_TOTAL;
        decimal? ORTOGRAFIA2_ACIERTOS;
        decimal? ORTOGRAFIA3_TOTAL;
        decimal? ORTOGRAFIA3_ACIERTOS;
        double PORCENTAJE_TOTAL;
        string NIVEL_ORT;
        ColumnSeries serieOrtografias;


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            grafica = "";

            if(!IsPostBack)
            {
             
                vPosicionInicialTab = "0";
                serieOrtografias = new ColumnSeries();
                vHabilitaLaboralI = false;

                if (Request.QueryString["ID"] != null)
                {
                   ResultadosPruebasNegocio nResultadosPruebas = new ResultadosPruebasNegocio();
                   vIdBateria = int.Parse(Request.QueryString["ID"]);
                   vClToken = new Guid(Request.QueryString["T"]);
               
                   String xmlResultados = nResultadosPruebas.Obtener_ResultadosBaterias(pClTokenBateria:vClToken, pIdBateria:vIdBateria);
                   if (!xmlResultados.Equals(""))
                   {
                       XElement res = XElement.Parse(xmlResultados);
                       vResultados = new List<E_RESULTADOS_BATERIA>();


                       foreach (var element in res.Descendants("PRUEBA"))
                       //.Where(x => (string)x == "jobSteps"))
                       {
                           //XElement.Parse(xmlResultados).
                           //List<E_RESULTADOS_BATERIA> vResultados2 = new List<E_RESULTADOS_BATERIA>();
                           var ListaResultados = element.Elements("VALORES").Select(el => new E_RESULTADOS_BATERIA
                            {
                                //Where(t => t.Attribute("CL_PRUEBA").Value == "ADAPTACION")
                                ID_BATERIA = 1,
                                XML_MENSAJES = "",

                                NO_VALOR = decimal.Parse(el.Attribute("NO_VALOR").Value),
                                CL_VARIABLE = el.Attribute("CL_VARIABLE").Value,
                                CL_PRUEBA = el.Attribute("CL_PRUEBA").Value,
                            }).ToList();
                           vResultados.AddRange(ListaResultados);
                       }

                       HabilitarPruebas(vResultados);
                       /////////////////////////////////////////////////INTERESES PERSONALES//////////////////////////////////////////////////////////////////////
                       #region INTERESES
                       var vResultadosInteres = vResultados.Where(r => r.CL_PRUEBA.Equals("INTERES")).ToList();
                       if (vResultadosInteres.Count > 0)
                       {
                           decimal? INTERES_REP_T = vResultadosInteres.Exists(ex => ex.CL_VARIABLE.Equals("INTERES_REP_T"))? vResultadosInteres.Where(x => x.CL_VARIABLE.Equals("INTERES_REP_T")).FirstOrDefault().NO_VALOR : null;
                           decimal? INTERES_REP_E = vResultadosInteres.Exists(ex => ex.CL_VARIABLE.Equals("INTERES_REP_E"))? vResultadosInteres.Where(x => x.CL_VARIABLE.Equals("INTERES_REP_E")).FirstOrDefault().NO_VALOR : null;
                           decimal? INTERES_REP_A = vResultadosInteres.Exists(ex => ex.CL_VARIABLE.Equals("INTERES_REP_A")) ? vResultadosInteres.Where(x => x.CL_VARIABLE.Equals("INTERES_REP_A")).FirstOrDefault().NO_VALOR : null;
                           decimal? INTERES_REP_S = vResultadosInteres.Exists(ex => ex.CL_VARIABLE.Equals("INTERES_REP_S")) ? vResultadosInteres.Where(x => x.CL_VARIABLE.Equals("INTERES_REP_S")).FirstOrDefault().NO_VALOR : null;
                           decimal? INTERES_REP_P = vResultadosInteres.Exists(ex => ex.CL_VARIABLE.Equals("INTERES_REP_P")) ? vResultadosInteres.Where(x => x.CL_VARIABLE.Equals("INTERES_REP_P")).FirstOrDefault().NO_VALOR : null;
                           decimal? INTERES_REP_R = vResultadosInteres.Exists(ex => ex.CL_VARIABLE.Equals("INTERES_REP_R")) ? vResultadosInteres.Where(x => x.CL_VARIABLE.Equals("INTERES_REP_R")).FirstOrDefault().NO_VALOR : null;
                           decimal? INTERES_REP_TS = vResultadosInteres.Exists(ex => ex.CL_VARIABLE.Equals("INTERES_REP_TS")) ? vResultadosInteres.Where(x => x.CL_VARIABLE.Equals("INTERES_REP_TS")).FirstOrDefault().NO_VALOR : null;

                           List<E_RESULTADOS_GENERICA> vlistResrultados = new List<E_RESULTADOS_GENERICA>();
                           vlistResrultados.Add(new E_RESULTADOS_GENERICA { NB_TITULO = "Teórico", VALOR = IsNull(INTERES_REP_T).ToString() + "%" });
                           vlistResrultados.Add(new E_RESULTADOS_GENERICA { NB_TITULO = "Económico", VALOR = IsNull(INTERES_REP_E).ToString() + "%" });
                           vlistResrultados.Add(new E_RESULTADOS_GENERICA { NB_TITULO = "Artístico", VALOR = IsNull(INTERES_REP_A).ToString() + "%" });
                           vlistResrultados.Add(new E_RESULTADOS_GENERICA { NB_TITULO = "Social", VALOR = IsNull(INTERES_REP_S).ToString() + "%" });
                           vlistResrultados.Add(new E_RESULTADOS_GENERICA { NB_TITULO = "Político", VALOR = IsNull(INTERES_REP_P).ToString() + "%" });
                           vlistResrultados.Add(new E_RESULTADOS_GENERICA { NB_TITULO = "Regulatorio", VALOR = IsNull(INTERES_REP_R).ToString() + "%" });
                           grdResultadosInteres.DataSource = vlistResrultados;

                           LineSeries secondColumnSeries = new LineSeries();
                           secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(IsNull(INTERES_REP_T)));
                           secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(IsNull(INTERES_REP_E)));
                           secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(IsNull(INTERES_REP_A)));
                           secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(IsNull(INTERES_REP_S)));
                           secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(IsNull(INTERES_REP_P)));
                           secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(IsNull(INTERES_REP_R)));
                           secondColumnSeries.LabelsAppearance.DataFormatString = "{0:N2}" + "%";
                           secondColumnSeries.TooltipsAppearance.DataFormatString = "{0:N2}" + "%";
                           LineChart.PlotArea.Series.Add(secondColumnSeries);
                       }
                       #endregion

                       #region MENTAL I
                       /////////////////////////////////////////////////APTITUD MENTAL I//////////////////////////////////////////////////////////////////////
                       var vResultadosAptitudMental1 = vResultados.Where(r => r.CL_PRUEBA.Equals("APTITUD-1")).ToList();
                       if (vResultadosAptitudMental1.Count > 0)
                       {
                            decimal? APTITUD1_REP_0001 = vResultadosAptitudMental1.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD1_REP_0001")) ? vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0001")).FirstOrDefault().NO_VALOR : null;
                            decimal? APTITUD1_REP_0002 = vResultadosAptitudMental1.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD1_REP_0002")) ? vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0002")).FirstOrDefault().NO_VALOR : null;
                            decimal? APTITUD1_REP_0003 = vResultadosAptitudMental1.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD1_REP_0003")) ? vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0003")).FirstOrDefault().NO_VALOR: null;
                            decimal? APTITUD1_REP_0004 = vResultadosAptitudMental1.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD1_REP_0004")) ? vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0004")).FirstOrDefault().NO_VALOR: null;
                            decimal? APTITUD1_REP_0005 = vResultadosAptitudMental1.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD1_REP_0005")) ? vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0005")).FirstOrDefault().NO_VALOR: null;
                            decimal? APTITUD1_REP_0006 = vResultadosAptitudMental1.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD1_REP_0006")) ? vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0006")).FirstOrDefault().NO_VALOR: null;
                            decimal? APTITUD1_REP_0007 = vResultadosAptitudMental1.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD1_REP_0007")) ? vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0007")).FirstOrDefault().NO_VALOR: null;
                            decimal? APTITUD1_REP_0008 = vResultadosAptitudMental1.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD1_REP_0008")) ? vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0008")).FirstOrDefault().NO_VALOR: null;
                            decimal? APTITUD1_REP_0009 = vResultadosAptitudMental1.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD1_REP_0009")) ? vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0009")).FirstOrDefault().NO_VALOR: null;
                            decimal? APTITUD1_REP_0010 = vResultadosAptitudMental1.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD1_REP_0010")) ? vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0010")).FirstOrDefault().NO_VALOR: null;

                            List<decimal?> lstValores = new List<decimal?>();
                            lstValores.Add(IsNull(APTITUD1_REP_0001));
                            lstValores.Add(IsNull(APTITUD1_REP_0002));
                            lstValores.Add(IsNull(APTITUD1_REP_0003));
                            lstValores.Add(IsNull(APTITUD1_REP_0004));
                            lstValores.Add(IsNull(APTITUD1_REP_0005));
                            lstValores.Add(IsNull(APTITUD1_REP_0006));
                            lstValores.Add(IsNull(APTITUD1_REP_0007));
                            lstValores.Add(IsNull(APTITUD1_REP_0008));
                            lstValores.Add(IsNull(APTITUD1_REP_0009));
                            lstValores.Add(IsNull(APTITUD1_REP_0010));
                            decimal? valorMaximo = CalcularValorMaximo(lstValores);


                            decimal? APTITUD1_REP_TOTAL = vResultadosAptitudMental1.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD1_REP_TOTAL")) ? vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_TOTAL")).FirstOrDefault().NO_VALOR : null;
                            decimal? APTITUD1_REP_CI = vResultadosAptitudMental1.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD1_REP_CI"))? vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_CI")).FirstOrDefault().NO_VALOR : null;
                            decimal? APTITUD_REP_DESC_TOTAL = vResultadosAptitudMental1.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD_REP_DESC_TOTAL"))? vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD_REP_DESC_TOTAL")).FirstOrDefault().NO_VALOR : null;
                           decimal? APTITUD_REP_DESC_CI = vResultadosAptitudMental1.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD_REP_DESC_CI"))? vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD_REP_DESC_CI")).FirstOrDefault().NO_VALOR : null;

                            List<E_RESULTADOS_GENERICA> vlstMentalI = new List<E_RESULTADOS_GENERICA>();
                            vlstMentalI.Add(new E_RESULTADOS_GENERICA { NB_TITULO = "Coeficiente intelectual", VALOR = coeficienteIntelectual((int)IsNull(APTITUD_REP_DESC_CI)) });
                            vlstMentalI.Add(new E_RESULTADOS_GENERICA { NB_TITULO = "Capacidad de aprendizaje", VALOR = Aprendizaje((int)IsNull(APTITUD_REP_DESC_TOTAL)) });
                            vlstMentalI.Add(new E_RESULTADOS_GENERICA { NB_TITULO = "Puntuación", VALOR = IsNull((int)IsNull(APTITUD1_REP_TOTAL)).ToString() });
                            vlstMentalI.Add(new E_RESULTADOS_GENERICA { NB_TITULO = "CI", VALOR = IsNull((int)IsNull(APTITUD1_REP_CI)).ToString() });
                            grdAptitudI.DataSource = vlstMentalI;

                            ColumnSeries secondColumnSeries = new ColumnSeries();
                            secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(IsNull(APTITUD1_REP_0001)));
                            secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(IsNull(APTITUD1_REP_0002)));
                            secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(IsNull(APTITUD1_REP_0003)));
                            secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(IsNull(APTITUD1_REP_0004)));
                            secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(IsNull(APTITUD1_REP_0005)));
                            secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(IsNull(APTITUD1_REP_0006)));
                            secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(IsNull(APTITUD1_REP_0007)));
                            secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(IsNull(APTITUD1_REP_0008)));
                            secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(IsNull(APTITUD1_REP_0009)));
                            secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(IsNull(APTITUD1_REP_0010)));
                            secondColumnSeries.LabelsAppearance.DataFormatString = "{0:N2}" + "%";
                            secondColumnSeries.LabelsAppearance.RotationAngle = 0;
          
                            secondColumnSeries.TooltipsAppearance.DataFormatString = "{0:N2}" + "%";
                            ChartAptitudMental1.PlotArea.Series.Add(secondColumnSeries);
                            ChartAptitudMental1.PlotArea.YAxis.MaxValue = valorMaximo + 10;
                        }
                       #endregion

                       #region INGLES

                       //////////////////////////////////////////////////INGLES/////////////////////////////////////////////////////////
                       var vResultadosIngles = vResultados.Where(r => r.CL_PRUEBA.Equals("INGLES")).ToList();
                       if (vResultadosIngles.Count > 0)
                       {
                           decimal? INGLES_REP_TOTAL = vResultadosIngles.Exists(ex => ex.CL_VARIABLE.Equals("INGLES_REP_TOTAL"))?  vResultadosIngles.Where(x => x.CL_VARIABLE.Equals("INGLES_REP_TOTAL")).FirstOrDefault().NO_VALOR : null;
                           decimal? INGLES_NIVEL = vResultadosIngles.Exists(ex => ex.CL_VARIABLE.Equals("INGLES_REP_NIVEL"))? vResultadosIngles.Where(x => x.CL_VARIABLE.Equals("INGLES_REP_NIVEL")).FirstOrDefault().NO_VALOR: null;
                           decimal? INGLES_TOTAL =  vResultadosIngles.Exists(ex => ex.CL_VARIABLE.Equals("INGLES_TOTAL"))? vResultadosIngles.Where(x => x.CL_VARIABLE.Equals("INGLES_TOTAL")).FirstOrDefault().NO_VALOR: null;
                           lblNivel.InnerHtml = NivelIngles((int)(IsNull(INGLES_NIVEL)));
                           lblInformacion.InnerHtml = DescripcionNivelIngles((int)(IsNull(INGLES_NIVEL))); //INGLES_NIVEL
                           List<GRD_ORT_TEC_ING> vlstInglex = new List<GRD_ORT_TEC_ING>();
                           vlstInglex.Add(new GRD_ORT_TEC_ING { NB_TITULO = "Inglés", ACIERTOS = ((int)(IsNull(INGLES_TOTAL))).ToString(), VALORES_MAXIMOS = "120", PORCENTAJE = ((decimal)(IsNull(INGLES_REP_TOTAL))).ToString() + "%" });
                           grdIngles.DataSource = vlstInglex;
                       }
                       #endregion

                       #region ESTILO DE PENSAMIENTO
                       var vResultadosPensamiento = vResultados.Where(r => r.CL_PRUEBA.Equals("PENSAMIENTO")).ToList();
                     
                       if (vResultadosPensamiento.Count > 0)
                       {
                           decimal? PENSAMIENTO_REP_A = vResultadosPensamiento.Exists(ex => ex.CL_VARIABLE.Equals("PENSAMIENTO_REP_A")) ? vResultadosPensamiento.Where(x => x.CL_VARIABLE.Equals("PENSAMIENTO_REP_A")).FirstOrDefault().NO_VALOR : null;
                           decimal? PENSAMIENTO_REP_L = vResultadosPensamiento.Exists(ex => ex.CL_VARIABLE.Equals("PENSAMIENTO_REP_L")) ? vResultadosPensamiento.Where(x => x.CL_VARIABLE.Equals("PENSAMIENTO_REP_L")).FirstOrDefault().NO_VALOR : null;;
                           decimal? PENSAMIENTO_REP_I = vResultadosPensamiento.Exists(ex => ex.CL_VARIABLE.Equals("PENSAMIENTO_REP_I")) ? vResultadosPensamiento.Where(x => x.CL_VARIABLE.Equals("PENSAMIENTO_REP_I")).FirstOrDefault().NO_VALOR : null;;
                           decimal? PENSAMIENTO_REP_V = vResultadosPensamiento.Exists(ex => ex.CL_VARIABLE.Equals("PENSAMIENTO_REP_V")) ? vResultadosPensamiento.Where(x => x.CL_VARIABLE.Equals("PENSAMIENTO_REP_V")).FirstOrDefault().NO_VALOR : null;;

                           List<E_RESULTADOS_GENERICA> vlstResultados = new List<E_RESULTADOS_GENERICA>();
                           vlstResultados.Add(new E_RESULTADOS_GENERICA { NB_TITULO = "Análisis", VALOR = IsNull(PENSAMIENTO_REP_A).ToString() + "%" });
                           vlstResultados.Add(new E_RESULTADOS_GENERICA { NB_TITULO = "Visión", VALOR = IsNull(PENSAMIENTO_REP_V).ToString() + "%" });
                           vlstResultados.Add(new E_RESULTADOS_GENERICA { NB_TITULO = "Intuición", VALOR = IsNull(PENSAMIENTO_REP_I).ToString() + "%" });
                           vlstResultados.Add(new E_RESULTADOS_GENERICA { NB_TITULO = "Lógica", VALOR = IsNull(PENSAMIENTO_REP_L).ToString() + "%" });
                           grdEstiloPensamiento.DataSource = vlstResultados;

                           ////////////////////////////////////////////////////////////////////////////////////////////
                           ClientScript.RegisterStartupScript(GetType(), "script", "DoGraph();", true);
                           grafica = IsNull(PENSAMIENTO_REP_A).ToString() + "," + IsNull(PENSAMIENTO_REP_V).ToString() + "," + IsNull(PENSAMIENTO_REP_I).ToString() + "," + IsNull(PENSAMIENTO_REP_L).ToString();

                          //// RadarLineSeries secondColumnSeries = new RadarLineSeries();
                          // PolarLineSeries secondColumnSeries = new PolarLineSeries();
                          // secondColumnSeries.SeriesItems.Add(0, PENSAMIENTO_REP_V);
                          // secondColumnSeries.SeriesItems.Add(45, null);
                          // secondColumnSeries.SeriesItems.Add(90, PENSAMIENTO_REP_I);
                          // secondColumnSeries.SeriesItems.Add(135, null);
                          // secondColumnSeries.SeriesItems.Add(180, PENSAMIENTO_REP_L);
                          // secondColumnSeries.SeriesItems.Add(225, null);
                          // secondColumnSeries.SeriesItems.Add(270, PENSAMIENTO_REP_A);
                          // secondColumnSeries.SeriesItems.Add(360, PENSAMIENTO_REP_V);
                          // //secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(PENSAMIENTO_REP_V));
                          // //secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(null));
                          // //secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(PENSAMIENTO_REP_I));
                          // //secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(null));
                          // //secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(PENSAMIENTO_REP_L));
                          // //secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(null));
                          // //secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(PENSAMIENTO_REP_A));
                          // //secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(null));
                          // secondColumnSeries.LabelsAppearance.DataFormatString = "{0:N2}" + "%";
                          // secondColumnSeries.TooltipsAppearance.DataFormatString = "{0:N2}" + "%";
                          // secondColumnSeries.MissingValues = Telerik.Web.UI.HtmlChart.MissingValuesBehavior.Interpolate;
                          //// rhcPenslamiento.PlotArea.Series.Add(secondColumnSeries);
                       }
                      
                       #endregion

                       #region TIVA

                       var vResultadosTIVA = vResultados.Where(r => r.CL_PRUEBA.Equals("TIVA")).ToList();
                       if (vResultadosTIVA.Count > 0)
                       {
                           ORTOGRAFIA1_TOTAL = 0;
                           ORTOGRAFIA1_ACIERTOS = 0;
                           ORTOGRAFIA2_TOTAL = 0;
                           ORTOGRAFIA2_ACIERTOS = 0;
                           ORTOGRAFIA3_TOTAL = 0;
                           ORTOGRAFIA3_ACIERTOS = 0;

                           decimal? TIVA_REP_INDICE_IP = vResultadosTIVA.Exists(ex => ex.CL_VARIABLE.Equals("TIVA-REP_INDICE_IP")) ? vResultadosTIVA.Where(x => x.CL_VARIABLE.Equals("TIVA-REP_INDICE_IP")).FirstOrDefault().NO_VALOR : null;
                           decimal? TIVA_REP_INDICE_ALR = vResultadosTIVA.Exists(ex => ex.CL_VARIABLE.Equals("TIVA-REP_INDICE_ALR")) ? vResultadosTIVA.Where(x => x.CL_VARIABLE.Equals("TIVA-REP_INDICE_ALR")).FirstOrDefault().NO_VALOR : null;
                           decimal? TIVA_REP_INDICE_IEL = vResultadosTIVA.Exists(ex => ex.CL_VARIABLE.Equals("TIVA-REP_INDICE_IEL")) ? vResultadosTIVA.Where(x => x.CL_VARIABLE.Equals("TIVA-REP_INDICE_IEL")).FirstOrDefault().NO_VALOR : null;
                           decimal? TIVA_REP_INDICE_IC = vResultadosTIVA.Exists(ex => ex.CL_VARIABLE.Equals("TIVA-REP_INDICE_IC")) ? vResultadosTIVA.Where(x => x.CL_VARIABLE.Equals("TIVA-REP_INDICE_IC")).FirstOrDefault().NO_VALOR : null;
                           decimal? TIVA_REP_INDICE_GI = vResultadosTIVA.Exists(ex => ex.CL_VARIABLE.Equals("TIVA-REP_INDICE_GI")) ? vResultadosTIVA.Where(x => x.CL_VARIABLE.Equals("TIVA-REP_INDICE_GI")).FirstOrDefault().NO_VALOR : null;

                           List<E_RESULTADOS_GENERICA> vlstTIVA = new List<E_RESULTADOS_GENERICA>();
                           vlstTIVA.Add(new E_RESULTADOS_GENERICA { NB_TITULO = "Indice de integridad personal", VALOR = (Math.Round((decimal)(IsNull(TIVA_REP_INDICE_IP)), 2)).ToString("N2") + "%" });
                           vlstTIVA.Add(new E_RESULTADOS_GENERICA { NB_TITULO = "Indice de apego a leyes y reglamentos", VALOR = (Math.Round((decimal)(IsNull(TIVA_REP_INDICE_ALR)), 2)).ToString("N2") + "%" });
                           vlstTIVA.Add(new E_RESULTADOS_GENERICA { NB_TITULO = "Indice de integridad y ética laboral", VALOR = (Math.Round((decimal)(IsNull(TIVA_REP_INDICE_IEL)), 2)).ToString("N2") + "%" });
                           vlstTIVA.Add(new E_RESULTADOS_GENERICA { NB_TITULO = "Indice de integridad cívica", VALOR = (Math.Round((decimal)(IsNull(TIVA_REP_INDICE_IC)), 2)).ToString("N2") + "%" });
                           vlstTIVA.Add(new E_RESULTADOS_GENERICA { NB_TITULO = "Indice global de integridad", VALOR = (Math.Round((decimal)(IsNull(TIVA_REP_INDICE_GI)), 2)).ToString("N2") + "%" });

                           PruebasNegocio pruebas = new PruebasNegocio();
                           var vBaremos = pruebas.obtenerVariableBaremos(vIdBateria);

                           decimal vTvTotal = Math.Round(vBaremos.Where(w => w.CL_VARIABLE == "TV-TOTAL").Select(s => s.NO_VALOR).FirstOrDefault(),0);
                           decimal vPersonal = Math.Round(vBaremos.Where(w => w.CL_VARIABLE == "TV-PERSONAL").Select(s => s.NO_VALOR).FirstOrDefault(), 0);
                           decimal vReglamento = Math.Round(vBaremos.Where(w => w.CL_VARIABLE == "TV-LEYES Y REGLAMENTOS").Select(s => s.NO_VALOR).FirstOrDefault(), 0);
                           decimal vEtica = Math.Round(vBaremos.Where(w => w.CL_VARIABLE == "TV-INTEGRIDAD Y ÉTICA LABORAL").Select(s => s.NO_VALOR).FirstOrDefault(), 0);
                           decimal vCivica = Math.Round(vBaremos.Where(w => w.CL_VARIABLE == "TV-CÍVICA").Select(s => s.NO_VALOR).FirstOrDefault(), 0);
                           decimal vResBaremos = 0;
                           decimal vErrores = 0;
                           foreach (var item in vBaremos)
                           {
                               if (item.CL_VARIABLE == "L1-CONSTANCIA" || item.CL_VARIABLE == "L1-CUMPLIMIENTO" || item.CL_VARIABLE == "L2-MANTIENE Y CONSERVA" || item.CL_VARIABLE == "IN-REGULATORIO")
                               {
                                   if (vTvTotal == 1)
                                       vResBaremos += (item.NO_VALOR == 1 || item.NO_VALOR == 2) ? 1 : 0;
                                   
                                   if (vTvTotal == 2)
                                       vResBaremos += (item.NO_VALOR == 3 || item.NO_VALOR == 2) ? 1 : 0;
                                   
                                   if (vTvTotal == 3)
                                       vResBaremos += (item.NO_VALOR == 3 || item.NO_VALOR == 2) ? 1 : 0;
                               }
                           }

                           //  vErrores =4-vResBaremos;
                           //if (vErrores >= 2)
                          
                           vErrores =4-vResBaremos;
                           if (vErrores >= 2)
                           {
                               divInvalida.Style.Value = String.Format("display: {0}", "block");
                               divIndiceG0.Style.Value = String.Format("display: {0}", "block");

                           }
                           else
                           {
                               grdtiva.DataSource = vlstTIVA;

                               RadarLineSeries secondColumnSeries = new RadarLineSeries();
                               secondColumnSeries.MissingValues = Telerik.Web.UI.HtmlChart.MissingValuesBehavior.Interpolate;
                               secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(IsNull(TIVA_REP_INDICE_IP)));
                               secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(IsNull(TIVA_REP_INDICE_ALR)));
                               secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(IsNull(TIVA_REP_INDICE_IEL)));
                               secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(IsNull(TIVA_REP_INDICE_IC)));

                               graficaTIVA = TIVA_REP_INDICE_IP.Value.ToString() + "," + TIVA_REP_INDICE_ALR.Value.ToString() + "," + TIVA_REP_INDICE_IEL.Value.ToString() + "," + TIVA_REP_INDICE_IC.Value.ToString();
                               secondColumnSeries.LabelsAppearance.Visible = false;
                           
                              // secondColumnSeries.LabelsAppearance.DataFormatString = "{0:N2}" + "%";
                              // secondColumnSeries.LabelsAppearance.Position = Telerik.Web.UI.HtmlChart.LineAndScatterLabelsPosition.Right;
                               secondColumnSeries.TooltipsAppearance.DataFormatString = "{0:N2}" + "%";
                              
                               RadarLineTIVA.PlotArea.Series.Add(secondColumnSeries);

                               if (vPersonal == 3)
                                   divPersonal.Style.Value = String.Format("display: {0}", "block");
                               if (vReglamento == 3)
                                   divReglamentos.Style.Value = String.Format("display: {0}", "block");
                               if (vEtica == 3)
                                   divEtica.Style.Value = String.Format("display: {0}", "block");
                               if (vCivica == 3)
                                   divCivica.Style.Value = String.Format("display: {0}", "block");
                               if (vTvTotal == 1)
                                   divIndiceG1.Style.Value = String.Format("display: {0}", "block");
                               if (vTvTotal == 2)
                                   divIndiceG2.Style.Value = String.Format("display: {0}", "block");
                               if (vTvTotal == 3)
                                   divIndiceG3.Style.Value = String.Format("display: {0}", "block");

                           }
                       }
                       #endregion

                       #region ORTOGRAFIA I,II,III

                       var vResultadosORTOGRAFIAI = vResultados.Exists(ex => ex.CL_PRUEBA.Equals("ORTOGRAFIA-1")) ? vResultados.Where(r => r.CL_PRUEBA.Equals("ORTOGRAFIA-1")).ToList() : null;
                       var vResultadosORTOGRAFIAII = vResultados.Exists(ex => ex.CL_PRUEBA.Equals("ORTOGRAFIA-2")) ? vResultados.Where(r => r.CL_PRUEBA.Equals("ORTOGRAFIA-2")).ToList(): null;
                       var vResultadosORTOGRAFIAIII = vResultados.Exists(ex => ex.CL_PRUEBA.Equals("ORTOGRAFIA-3")) ? vResultados.Where(r => r.CL_PRUEBA.Equals("ORTOGRAFIA-3")).ToList(): null;
                       List<GRD_ORT_TEC_ING> vlstOrtografias = new List<GRD_ORT_TEC_ING>();
                        if (vResultadosORTOGRAFIAI != null)

                            if (vResultadosORTOGRAFIAI.Count > 0)
                       {
                           ORTOGRAFIA1_TOTAL = vResultadosORTOGRAFIAI.Exists(ex => ex.CL_VARIABLE.Equals("ORTOGRAFIA1-REP-TOTAL")) ? vResultadosORTOGRAFIAI.Where(x => x.CL_VARIABLE.Equals("ORTOGRAFIA1-REP-TOTAL")).FirstOrDefault().NO_VALOR : null;
                           ORTOGRAFIA1_ACIERTOS = vResultadosORTOGRAFIAI.Exists(ex => ex.CL_VARIABLE.Equals("ORTOGRAFIA1-REP-#A"))? vResultadosORTOGRAFIAI.Where(x => x.CL_VARIABLE.Equals("ORTOGRAFIA1-REP-#A")).FirstOrDefault().NO_VALOR : null;
                           vlstOrtografias.Add(new GRD_ORT_TEC_ING { NB_TITULO = "Ortografía I", ACIERTOS = (Math.Round((decimal)ORTOGRAFIA1_ACIERTOS, 0)).ToString(), VALORES_MAXIMOS = "40", PORCENTAJE = (ORTOGRAFIA1_TOTAL).ToString() + "%" });
                           AgregarColumasSeries(ORTOGRAFIA1_TOTAL);
                       }
                       else
                       {
                           vlstOrtografias.Add(new GRD_ORT_TEC_ING { NB_TITULO = "Ortografía I", ACIERTOS = "0", VALORES_MAXIMOS = "40", PORCENTAJE = "0" });
                           AgregarColumasSeries(0);
                       }
                        if (vResultadosORTOGRAFIAII != null)

                            if (vResultadosORTOGRAFIAII.Count > 0)
                       {
                           ORTOGRAFIA2_TOTAL = vResultadosORTOGRAFIAII.Exists(ex => ex.CL_VARIABLE.Equals("ORTOGRAFIA2-REP-TOTAL")) ? vResultadosORTOGRAFIAII.Where(x => x.CL_VARIABLE.Equals("ORTOGRAFIA2-REP-TOTAL")).FirstOrDefault().NO_VALOR : null;
                           ORTOGRAFIA2_ACIERTOS = vResultadosORTOGRAFIAII.Exists(ex => ex.CL_VARIABLE.Equals("ORTOGRAFIA2-REP-#A")) ? vResultadosORTOGRAFIAII.Where(x => x.CL_VARIABLE.Equals("ORTOGRAFIA2-REP-#A")).FirstOrDefault().NO_VALOR : null;
                           vlstOrtografias.Add(new GRD_ORT_TEC_ING { NB_TITULO = "Ortografía II", ACIERTOS = (Math.Round((decimal)ORTOGRAFIA2_ACIERTOS, 0)).ToString(), VALORES_MAXIMOS = "25", PORCENTAJE = (ORTOGRAFIA2_TOTAL).ToString() + "%" });
                           AgregarColumasSeries(ORTOGRAFIA2_TOTAL);
                       }
                       else
                       {
                           vlstOrtografias.Add(new GRD_ORT_TEC_ING { NB_TITULO = "Ortografía II", ACIERTOS = "0", VALORES_MAXIMOS = "25", PORCENTAJE = "0" });
                           AgregarColumasSeries(0);
                       }
                        if (vResultadosORTOGRAFIAIII != null)

                            if (vResultadosORTOGRAFIAIII.Count > 0)
                       {
                           ORTOGRAFIA3_TOTAL = vResultadosORTOGRAFIAIII.Exists(ex => ex.CL_VARIABLE.Equals("ORTOGRAFIA3-REP-TOTAL"))?  vResultadosORTOGRAFIAIII.Where(x => x.CL_VARIABLE.Equals("ORTOGRAFIA3-REP-TOTAL")).FirstOrDefault().NO_VALOR : null;
                           ORTOGRAFIA3_ACIERTOS = vResultadosORTOGRAFIAIII.Exists(ex => ex.CL_VARIABLE.Equals("ORTOGRAFIA3-REP-#A")) ? vResultadosORTOGRAFIAIII.Where(x => x.CL_VARIABLE.Equals("ORTOGRAFIA3-REP-#A")).FirstOrDefault().NO_VALOR : null;
                           vlstOrtografias.Add(new GRD_ORT_TEC_ING { NB_TITULO = "Ortografía III", ACIERTOS = (Math.Round((decimal)ORTOGRAFIA3_ACIERTOS, 0)).ToString(), VALORES_MAXIMOS = "18", PORCENTAJE = (ORTOGRAFIA3_TOTAL).ToString() + "%" });
                           AgregarColumasSeries(ORTOGRAFIA3_TOTAL);
                       }
                       else
                       {
                           vlstOrtografias.Add(new GRD_ORT_TEC_ING { NB_TITULO = "Ortografía III", ACIERTOS = "0", VALORES_MAXIMOS = "18", PORCENTAJE = "0" });
                           AgregarColumasSeries(0);
                       }


                       vlstOrtografias.Add(new GRD_ORT_TEC_ING
                       {
                           NB_TITULO = "Total",
                           ACIERTOS =   (Math.Round((decimal)(((ORTOGRAFIA1_ACIERTOS != null) ? ORTOGRAFIA1_ACIERTOS : 0) + ((ORTOGRAFIA2_ACIERTOS != null) ? ORTOGRAFIA2_ACIERTOS : 0) + ((ORTOGRAFIA3_ACIERTOS != null) ? ORTOGRAFIA3_ACIERTOS : 0)), 0)).ToString(),
                           VALORES_MAXIMOS = "83",
                           PORCENTAJE = (Math.Round((decimal)(((ORTOGRAFIA1_ACIERTOS != null) ? ORTOGRAFIA1_ACIERTOS : 0) + ((ORTOGRAFIA2_ACIERTOS != null) ? ORTOGRAFIA2_ACIERTOS : 0) + ((ORTOGRAFIA3_ACIERTOS != null) ? ORTOGRAFIA3_ACIERTOS : 0)) * 100 / 83, 2)).ToString() + "%" 
                         
                       
                       
                       });

                       PORCENTAJE_TOTAL = (Math.Round((double)(((ORTOGRAFIA1_ACIERTOS != null) ? ORTOGRAFIA1_ACIERTOS : 0) + ((ORTOGRAFIA2_ACIERTOS != null) ? ORTOGRAFIA2_ACIERTOS : 0) + ((ORTOGRAFIA3_ACIERTOS != null) ? ORTOGRAFIA3_ACIERTOS : 0)) * 100 / 83, 2));
                       NIVEL_ORT = NivelOrtografias(PORCENTAJE_TOTAL);
                       txtPorcentajeTotal.InnerText = PORCENTAJE_TOTAL.ToString() + "%";
                       txtNivel.InnerText = NIVEL_ORT;

                       grdOrtografias.DataSource = vlstOrtografias;
                       serieOrtografias.LabelsAppearance.DataFormatString = "{0:N2}" + "%";
                       serieOrtografias.TooltipsAppearance.DataFormatString = "{0:N2}" + "%";
                       RadChartOrtografias.PlotArea.Series.Add(serieOrtografias);

                       #endregion

                       #region LABORAL II

                       var vResultadosLaboralII = vResultados.Where(r => r.CL_PRUEBA.Equals("LABORAL-2")).ToList();
                       if (vResultadosLaboralII.Count > 0)
                       {
                           vHabilitaLaboralI = true;
                           decimal? LABORAL2_REP_DAAPF = vResultadosLaboralII.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL2-REP-DAAPF"))? vResultadosLaboralII.Where(x => x.CL_VARIABLE.Equals("LABORAL2-REP-DAAPF")).FirstOrDefault().NO_VALOR : null;
                           decimal? LABORAL2_REP_TMCTF = vResultadosLaboralII.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL2-REP-TMCTF"))? vResultadosLaboralII.Where(x => x.CL_VARIABLE.Equals("LABORAL2-REP-TMCTF")).FirstOrDefault().NO_VALOR: null;
                           decimal? LABORAL2_REP_MTCSF = vResultadosLaboralII.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL2-REP-MTCSF"))? vResultadosLaboralII.Where(x => x.CL_VARIABLE.Equals("LABORAL2-REP-MTCSF")).FirstOrDefault().NO_VALOR: null;
                           decimal? LABORAL2_REP_ADNGF = vResultadosLaboralII.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL2-REP-ADNGF"))? vResultadosLaboralII.Where(x => x.CL_VARIABLE.Equals("LABORAL2-REP-ADNGF")).FirstOrDefault().NO_VALOR: null;
                           decimal? LABORAL2_REP_DAAPD = vResultadosLaboralII.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL2-REP-DAAPD"))? vResultadosLaboralII.Where(x => x.CL_VARIABLE.Equals("LABORAL2-REP-DAAPD")).FirstOrDefault().NO_VALOR: null;
                           decimal? LABORAL2_REP_TMCTD = vResultadosLaboralII.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL2-REP-TMCTD"))? vResultadosLaboralII.Where(x => x.CL_VARIABLE.Equals("LABORAL2-REP-TMCTD")).FirstOrDefault().NO_VALOR: null;
                           decimal? LABORAL2_REP_MTCSD = vResultadosLaboralII.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL2-REP-MTCSD"))? vResultadosLaboralII.Where(x => x.CL_VARIABLE.Equals("LABORAL2-REP-MTCSD")).FirstOrDefault().NO_VALOR: null;
                           decimal? LABORAL2_REP_ADNGD = vResultadosLaboralII.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL2-REP-ADNGD"))? vResultadosLaboralII.Where(x => x.CL_VARIABLE.Equals("LABORAL2-REP-ADNGD")).FirstOrDefault().NO_VALOR: null;

                           if(LABORAL2_REP_DAAPF >= LABORAL2_REP_TMCTF & LABORAL2_REP_DAAPF >= LABORAL2_REP_MTCSF & LABORAL2_REP_DAAPF >= LABORAL2_REP_ADNGF)
                                divDAF.Style.Value = String.Format("display: {0}", "block");

                           if(LABORAL2_REP_TMCTF >= LABORAL2_REP_DAAPF & LABORAL2_REP_TMCTF >= LABORAL2_REP_MTCSF & LABORAL2_REP_TMCTF >= LABORAL2_REP_ADNGF)
                               divTCF.Style.Value = String.Format("display: {0}", "block");

                           if (LABORAL2_REP_MTCSF >= LABORAL2_REP_DAAPF & LABORAL2_REP_MTCSF >= LABORAL2_REP_TMCTF & LABORAL2_REP_MTCSF >= LABORAL2_REP_ADNGF)
                               divMCF.Style.Value = String.Format("display: {0}", "block");

                           if (LABORAL2_REP_ADNGF >= LABORAL2_REP_DAAPF & LABORAL2_REP_ADNGF >= LABORAL2_REP_TMCTF & LABORAL2_REP_ADNGF >= LABORAL2_REP_MTCSF)
                               divANF.Style.Value = String.Format("display: {0}", "block");

                           if (LABORAL2_REP_DAAPD >= LABORAL2_REP_TMCTD & LABORAL2_REP_DAAPD >= LABORAL2_REP_MTCSD & LABORAL2_REP_DAAPD >= LABORAL2_REP_ADNGD)
                               divDAD.Style.Value = String.Format("display: {0}", "block");

                           if (LABORAL2_REP_TMCTD >= LABORAL2_REP_DAAPD & LABORAL2_REP_TMCTD >= LABORAL2_REP_MTCSD & LABORAL2_REP_TMCTD >= LABORAL2_REP_ADNGD)
                               divTCD.Style.Value = String.Format("display: {0}", "block");

                           if (LABORAL2_REP_MTCSD >= LABORAL2_REP_DAAPD & LABORAL2_REP_MTCSD >= LABORAL2_REP_TMCTD & LABORAL2_REP_MTCSD >= LABORAL2_REP_ADNGD)
                               divMCD.Style.Value = String.Format("display: {0}", "block");

                           if (LABORAL2_REP_ADNGD >= LABORAL2_REP_DAAPD & LABORAL2_REP_ADNGD >= LABORAL2_REP_TMCTD & LABORAL2_REP_ADNGD >= LABORAL2_REP_MTCSD)
                               divAND.Style.Value = String.Format("display: {0}", "block");


                           decimal? LABORAL2_REP_TOTALF = vResultadosLaboralII.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL2-REP-TOTALF"))? vResultadosLaboralII.Where(x => x.CL_VARIABLE.Equals("LABORAL2-REP-TOTALF")).FirstOrDefault().NO_VALOR : null;
                           decimal? LABORAL2_REP_AD_TOTALD = vResultadosLaboralII.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL2-REP-AD-TOTALD"))? vResultadosLaboralII.Where(x => x.CL_VARIABLE.Equals("LABORAL2-REP-AD-TOTALD")).FirstOrDefault().NO_VALOR : null;

                           lblF1.InnerHtml = (Math.Round((decimal)(IsNull(LABORAL2_REP_DAAPF)), 0)).ToString();
                           lblF2.InnerHtml = (Math.Round((decimal)(IsNull(LABORAL2_REP_TMCTF)))).ToString();
                           lblF3.InnerHtml = (Math.Round((decimal)(IsNull(LABORAL2_REP_MTCSF)))).ToString();
                           lblF4.InnerHtml = (Math.Round((decimal)(IsNull(LABORAL2_REP_ADNGF)))).ToString();
                           lblTotalF.InnerHtml = (Math.Round((decimal)(IsNull(LABORAL2_REP_TOTALF)))).ToString();

                           lblD1.InnerHtml = (Math.Round((decimal)(IsNull(LABORAL2_REP_DAAPD)), 0)).ToString();
                           lblD2.InnerHtml = (Math.Round((decimal)(IsNull(LABORAL2_REP_TMCTD)))).ToString();
                           lblD3.InnerHtml = (Math.Round((decimal)(IsNull(LABORAL2_REP_MTCSD)))).ToString();
                           lblD4.InnerHtml = (Math.Round((decimal)(IsNull(LABORAL2_REP_ADNGD)))).ToString();
                           lblTotalD.InnerHtml = (Math.Round((decimal)(IsNull(LABORAL2_REP_AD_TOTALD)))).ToString();

                           List<GRD_LABORAL_II> vlstLaboralII = new List<GRD_LABORAL_II>();
                           vlstLaboralII.Add(new GRD_LABORAL_II { DA_AP = (Math.Round((decimal)(IsNull(LABORAL2_REP_DAAPF)), 0)).ToString(), TM_CT = (Math.Round((decimal)(IsNull(LABORAL2_REP_TMCTF)))).ToString(), MT_CS = (Math.Round((decimal)(IsNull(LABORAL2_REP_MTCSF)))).ToString(), AD_NG = (Math.Round((decimal)(IsNull(LABORAL2_REP_ADNGF)))).ToString(), TOTAL = (Math.Round((decimal)(IsNull(LABORAL2_REP_TOTALF)))).ToString() });
                           vlstLaboralII.Add(new GRD_LABORAL_II { DA_AP = (Math.Round((decimal)(IsNull(LABORAL2_REP_DAAPD)), 0)).ToString(), TM_CT = (Math.Round((decimal)(IsNull(LABORAL2_REP_TMCTD)))).ToString(), MT_CS = (Math.Round((decimal)(IsNull(LABORAL2_REP_MTCSD)))).ToString(), AD_NG = (Math.Round((decimal)(IsNull(LABORAL2_REP_ADNGD)))).ToString(), TOTAL = (Math.Round((decimal)(IsNull(LABORAL2_REP_AD_TOTALD)))).ToString() });
                           grdPersonalidadLaboralII.DataSource = vlstLaboralII;

                       }
                       #endregion

                       #region MENTAL II

                       var vResultadosAptitud2 = vResultados.Where(r => r.CL_PRUEBA.Equals("APTITUD-2")).ToList();
                       if (vResultadosAptitud2.Count > 0)
                       {
                           decimal? APTITUD2_REP_CI = vResultadosAptitud2.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD2_REP_CI")) ? vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_CI")).FirstOrDefault().NO_VALOR : null;
                           decimal? APTITUD2_REP_ACIERTOS = vResultadosAptitud2.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD2_REP_ACIERTOS"))? vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_ACIERTOS")).FirstOrDefault().NO_VALOR : null;
                           decimal? APTITUD2_REP_DESC_CI = vResultadosAptitud2.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD2_REP_DESC_CI"))? vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_DESC_CI")).FirstOrDefault().NO_VALOR : null;
                            
                           decimal? APTITUD2_REP_CONOCIMIENTO = vResultadosAptitud2.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD2_REP_CONOCIMIENTO"))? vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_CONOCIMIENTO")).FirstOrDefault().NO_VALOR : null;
                            decimal? APTITUD2_REP_COMPRENSION = vResultadosAptitud2.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD2_REP_COMPRENSION"))? vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_COMPRENSION")).FirstOrDefault().NO_VALOR: null;
                            decimal? APTITUD2_REP_SIGNIFICADO = vResultadosAptitud2.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD2_REP_SIGNIFICADO"))? vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_SIGNIFICADO")).FirstOrDefault().NO_VALOR: null;
                            decimal? APTITUD2_REP_LOGICA = vResultadosAptitud2.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD2_REP_LOGICA"))? vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_LOGICA")).FirstOrDefault().NO_VALOR: null;
                            decimal? APTITUD2_REP_ARITMETICA = vResultadosAptitud2.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD2_REP_ARITMETICA"))? vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_ARITMETICA")).FirstOrDefault().NO_VALOR: null;
                            decimal? APTITUD2_REP_JUICIO = vResultadosAptitud2.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD2_REP_JUICIO"))? vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_JUICIO")).FirstOrDefault().NO_VALOR: null;
                            decimal? APTITUD2_REP_ANALOGIAS = vResultadosAptitud2.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD2_REP_ANALOGIAS"))? vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_ANALOGIAS")).FirstOrDefault().NO_VALOR: null;
                            decimal? APTITUD2_REP_ORDENAMIENTO = vResultadosAptitud2.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD2_REP_ORDENAMIENTO"))? vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_ORDENAMIENTO")).FirstOrDefault().NO_VALOR: null;
                            decimal? APTITUD2_REP_CLASIFICACION = vResultadosAptitud2.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD2_REP_CLASIFICACION"))? vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_CLASIFICACION")).FirstOrDefault().NO_VALOR: null;
                            decimal? APTITUD2_REP_SERIACION = vResultadosAptitud2.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD2_REP_SERIACION"))? vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_SERIACION")).FirstOrDefault().NO_VALOR: null;

                            List<decimal?> lstValores = new List<decimal?>();
                            lstValores.Add(APTITUD2_REP_CONOCIMIENTO );
                            lstValores.Add(APTITUD2_REP_COMPRENSION);
                            lstValores.Add(APTITUD2_REP_SIGNIFICADO);
                            lstValores.Add(APTITUD2_REP_LOGICA);
                            lstValores.Add(APTITUD2_REP_ARITMETICA);
                            lstValores.Add(APTITUD2_REP_JUICIO);
                            lstValores.Add(APTITUD2_REP_ANALOGIAS);
                            lstValores.Add(APTITUD2_REP_ORDENAMIENTO);
                            lstValores.Add(APTITUD2_REP_CLASIFICACION);
                            lstValores.Add(APTITUD2_REP_SERIACION);

                            decimal? valorMaximo = CalcularValorMaximo(lstValores);

                            List<E_RESULTADOS_GENERICA> vlstMentalII = new List<E_RESULTADOS_GENERICA>();
                            vlstMentalII.Add(new E_RESULTADOS_GENERICA { NB_TITULO = "Puntuación directa:", VALOR = IsNull(APTITUD2_REP_ACIERTOS).Value.ToString("N0") });
                            vlstMentalII.Add(new E_RESULTADOS_GENERICA { NB_TITULO = "Coeficiente intelectual (CI):", VALOR = IsNull(APTITUD2_REP_CI).Value.ToString("N0") });
                            vlstMentalII.Add(new E_RESULTADOS_GENERICA { NB_TITULO = "Inteligencia:", VALOR = coeficienteIntelectual(int.Parse(IsNull(APTITUD2_REP_DESC_CI).Value.ToString("N0"))) });
                            grdMentalII.DataSource = vlstMentalII;
                            
                            ColumnSeries csValuesAptitud2 = new ColumnSeries();
                            csValuesAptitud2.SeriesItems.Add(new CategorySeriesItem(IsNull(APTITUD2_REP_CONOCIMIENTO == -100 ? 0 : APTITUD2_REP_CONOCIMIENTO)));
                            csValuesAptitud2.SeriesItems.Add(new CategorySeriesItem(IsNull(APTITUD2_REP_COMPRENSION == -100 ? 0 : APTITUD2_REP_COMPRENSION)));
                            csValuesAptitud2.SeriesItems.Add(new CategorySeriesItem(IsNull(APTITUD2_REP_SIGNIFICADO == -100 ? 0 : APTITUD2_REP_SIGNIFICADO)));
                            csValuesAptitud2.SeriesItems.Add(new CategorySeriesItem(IsNull(APTITUD2_REP_LOGICA == -100 ? 0 : APTITUD2_REP_LOGICA)));
                            csValuesAptitud2.SeriesItems.Add(new CategorySeriesItem(IsNull(APTITUD2_REP_ARITMETICA == -100 ? 0 : APTITUD2_REP_ARITMETICA)));
                            csValuesAptitud2.SeriesItems.Add(new CategorySeriesItem(IsNull(APTITUD2_REP_JUICIO == -100 ? 0 : APTITUD2_REP_JUICIO)));
                            csValuesAptitud2.SeriesItems.Add(new CategorySeriesItem(IsNull(APTITUD2_REP_ANALOGIAS == -100 ? 0 : APTITUD2_REP_ANALOGIAS)));
                            csValuesAptitud2.SeriesItems.Add(new CategorySeriesItem(IsNull(APTITUD2_REP_ORDENAMIENTO == -100 ? 0 : APTITUD2_REP_ORDENAMIENTO)));
                            csValuesAptitud2.SeriesItems.Add(new CategorySeriesItem(IsNull(APTITUD2_REP_CLASIFICACION == -100 ? 0 : APTITUD2_REP_CLASIFICACION)));
                            csValuesAptitud2.SeriesItems.Add(new CategorySeriesItem(IsNull(APTITUD2_REP_SERIACION == -100 ? 0 : APTITUD2_REP_SERIACION)));
                            csValuesAptitud2.LabelsAppearance.DataFormatString = "{0:N2}" + "%";
                            csValuesAptitud2.LabelsAppearance.RotationAngle = 0;
                            csValuesAptitud2.TooltipsAppearance.DataFormatString = "{0:N2}" + "%";
                            rhcAptitud2.PlotArea.Series.Add(csValuesAptitud2);
                            rhcAptitud2.PlotArea.YAxis.MaxValue = valorMaximo + 10 ;
                            
                            
                           decimal? APTITUD2_REP_C1 = vResultadosAptitud2.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD2_REP_C1"))? vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_C1")).FirstOrDefault().NO_VALOR : null;
                            decimal? APTITUD2_REP_C2 = vResultadosAptitud2.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD2_REP_C2"))? vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_C2")).FirstOrDefault().NO_VALOR: null;
                            decimal? APTITUD2_REP_C3 = vResultadosAptitud2.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD2_REP_C3"))? vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_C3")).FirstOrDefault().NO_VALOR: null;
                            decimal? APTITUD2_REP_C4 = vResultadosAptitud2.Exists(ex => ex.CL_VARIABLE.Equals("APTITUD2_REP_C4"))? vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_C4")).FirstOrDefault().NO_VALOR: null;
                            
                            configurarComparacion1(int.Parse(IsNull(APTITUD2_REP_C1).Value.ToString("N0")));
                            configurarComparacion2(int.Parse(IsNull(APTITUD2_REP_C2).Value.ToString("N0")));
                            configurarComparacion3(int.Parse(IsNull(APTITUD2_REP_C3).Value.ToString("N0")));
                            configurarComparacion4(int.Parse(IsNull(APTITUD2_REP_C4).Value.ToString("N0")));
                       }
                       #endregion

                       #region LABORAL I

                       //////////////////////////////////////////////////LABORAL I/////////////////////////////////////////////////////////
                       var vResultadosLaboralI = vResultados.Where(r => r.CL_PRUEBA.Equals("LABORAL-1")).ToList();
                       if (vResultadosLaboralI.Count > 0) //CORRECTO
                       {
                           vHabilitaLaboralI = true;
                           ///COTIDIANO
                           decimal? LABORAL1_REP_COTIDIANO_D = vResultadosLaboralI.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL1-REP-COTIDIANO-D"))? vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-COTIDIANO-D")).FirstOrDefault().NO_VALOR : null;
                           decimal? LABORAL1_REP_COTIDIANO_I = vResultadosLaboralI.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL1-REP-COTIDIANO-I"))? vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-COTIDIANO-I")).FirstOrDefault().NO_VALOR: null;
                           decimal? LABORAL1_REP_COTIDIANO_S = vResultadosLaboralI.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL1-REP-COTIDIANO-S"))? vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-COTIDIANO-S")).FirstOrDefault().NO_VALOR: null;
                           decimal? LABORAL1_REP_COTIDIANO_C = vResultadosLaboralI.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL1-REP-COTIDIANO-C"))? vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-COTIDIANO-C")).FirstOrDefault().NO_VALOR: null;
                           ///MOTIVANTE
                           decimal? LABORAL1_REP_MOTIVANTE_D = vResultadosLaboralI.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL1-REP-MOTIVANTE-D"))? vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-MOTIVANTE-D")).FirstOrDefault().NO_VALOR: null;
                           decimal? LABORAL1_REP_MOTIVANTE_I = vResultadosLaboralI.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL1-REP-MOTIVANTE-I"))? vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-MOTIVANTE-I")).FirstOrDefault().NO_VALOR: null;
                           decimal? LABORAL1_REP_MOTIVANTE_S = vResultadosLaboralI.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL1-REP-MOTIVANTE-S"))? vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-MOTIVANTE-S")).FirstOrDefault().NO_VALOR: null;
                           decimal? LABORAL1_REP_MOTIVANTE_C = vResultadosLaboralI.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL1-REP-MOTIVANTE-C"))? vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-MOTIVANTE-C")).FirstOrDefault().NO_VALOR: null;
                           //PRESION
                           decimal? LABORAL1_REP_PRESION_D = vResultadosLaboralI.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL1-REP-PRESION-D"))? vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-PRESION-D")).FirstOrDefault().NO_VALOR: null;
                           decimal? LABORAL1_REP_PRESION_I = vResultadosLaboralI.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL1-REP-PRESION-I"))? vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-PRESION-I")).FirstOrDefault().NO_VALOR: null;
                           decimal? LABORAL1_REP_PRESION_S = vResultadosLaboralI.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL1-REP-PRESION-S"))? vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-PRESION-S")).FirstOrDefault().NO_VALOR: null;
                           decimal? LABORAL1_REP_PRESION_C = vResultadosLaboralI.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL1-REP-PRESION-C"))?  vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-PRESION-C")).FirstOrDefault().NO_VALOR: null;
                           //TOTAL  MOTIVANTE
                           decimal? LABORAL1_REP_DM = vResultadosLaboralI.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL1-REP-DM"))? vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-DM")).FirstOrDefault().NO_VALOR: null;
                           decimal? LABORAL1_REP_IM = vResultadosLaboralI.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL1-REP-IM"))? vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-IM")).FirstOrDefault().NO_VALOR: null;
                           decimal? LABORAL1_REP_SM = vResultadosLaboralI.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL1-REP-SM"))? vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-SM")).FirstOrDefault().NO_VALOR: null;
                           decimal? LABORAL1_REP_CM =  vResultadosLaboralI.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL1-REP-CM"))? vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-CM")).FirstOrDefault().NO_VALOR: null;
                           //TOTAL PRESIONANTE
                           decimal? LABORAL1_REP_DL = vResultadosLaboralI.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL1-REP-DL"))? vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-DL")).FirstOrDefault().NO_VALOR: null;
                           decimal? LABORAL1_REP_IL = vResultadosLaboralI.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL1-REP-IL"))? vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-IL")).FirstOrDefault().NO_VALOR: null;
                           decimal? LABORAL1_REP_SL = vResultadosLaboralI.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL1-REP-SL"))? vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-SL")).FirstOrDefault().NO_VALOR: null;
                           decimal? LABORAL1_REP_CL = vResultadosLaboralI.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL1-REP-CL"))? vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-CL")).FirstOrDefault().NO_VALOR: null;
                           //TOTAL COTIDIANO
                           decimal? LABORAL1_REP_DT = vResultadosLaboralI.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL1-REP-DT"))? vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-DT")).FirstOrDefault().NO_VALOR: null;
                           decimal? LABORAL1_REP_IT = vResultadosLaboralI.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL1-REP-IT"))? vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-IT")).FirstOrDefault().NO_VALOR: null;
                           decimal? LABORAL1_REP_ST = vResultadosLaboralI.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL1-REP-ST"))? vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-ST")).FirstOrDefault().NO_VALOR: null;
                           decimal? LABORAL1_REP_CT = vResultadosLaboralI.Exists(ex => ex.CL_VARIABLE.Equals("LABORAL1-REP-CT"))? vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-CT")).FirstOrDefault().NO_VALOR: null;

                           decimal vNoValidez = 0;

                           //GRAFICA COTIDIANO
                           LineSeries csCotidianoLaboralI = new LineSeries();
                           csCotidianoLaboralI.SeriesItems.Add(new CategorySeriesItem(IsNull(LABORAL1_REP_COTIDIANO_D)));
                           csCotidianoLaboralI.SeriesItems.Add(new CategorySeriesItem(IsNull(LABORAL1_REP_COTIDIANO_I)));
                           csCotidianoLaboralI.SeriesItems.Add(new CategorySeriesItem(IsNull(LABORAL1_REP_COTIDIANO_S)));
                           csCotidianoLaboralI.SeriesItems.Add(new CategorySeriesItem(IsNull(LABORAL1_REP_COTIDIANO_C)));
                           csCotidianoLaboralI.LabelsAppearance.Visible = false;
                           csCotidianoLaboralI.TooltipsAppearance.DataFormatString = "{0:N2}" + "%";
                           csCotidianoLaboralI.Name = "Sit. Cotidiano";

                           //GRAFICA MOTIVANTE
                           LineSeries csMotivanteLaboralI = new LineSeries();
                           csMotivanteLaboralI.SeriesItems.Add(new CategorySeriesItem(IsNull(LABORAL1_REP_MOTIVANTE_D)));
                           csMotivanteLaboralI.SeriesItems.Add(new CategorySeriesItem(IsNull(LABORAL1_REP_MOTIVANTE_I)));
                           csMotivanteLaboralI.SeriesItems.Add(new CategorySeriesItem(IsNull(LABORAL1_REP_MOTIVANTE_S)));
                           csMotivanteLaboralI.SeriesItems.Add(new CategorySeriesItem(IsNull(LABORAL1_REP_MOTIVANTE_C)));
                           csMotivanteLaboralI.TooltipsAppearance.DataFormatString = "{0:N2}" + "%";
                           csMotivanteLaboralI.LabelsAppearance.Visible = false;
                           csMotivanteLaboralI.Name = "Sit. Motivante";


                           //GRAFICA PRESIONANTE
                           LineSeries csPresionLaboralI = new LineSeries();
                           csPresionLaboralI.SeriesItems.Add(new CategorySeriesItem(IsNull(LABORAL1_REP_PRESION_D)));
                           csPresionLaboralI.SeriesItems.Add(new CategorySeriesItem(IsNull(LABORAL1_REP_PRESION_I)));
                           csPresionLaboralI.SeriesItems.Add(new CategorySeriesItem(IsNull(LABORAL1_REP_PRESION_S)));
                           csPresionLaboralI.SeriesItems.Add(new CategorySeriesItem(IsNull(LABORAL1_REP_PRESION_C)));
                           csPresionLaboralI.LabelsAppearance.Visible = false;
                           csPresionLaboralI.TooltipsAppearance.DataFormatString = "{0:N2}" + "%";
                           csPresionLaboralI.Name = "Sit. Presión";

                           rhcPresionante.PlotArea.Series.Add(csPresionLaboralI);
                           RadHtmlCotidiana.PlotArea.Series.Add(csCotidianoLaboralI);
                           rhcMotivante.PlotArea.Series.Add(csMotivanteLaboralI);

                           List<GRID_LABORAL_I> vListaResLabI = new List<GRID_LABORAL_I>();
                           vListaResLabI.Add(new GRID_LABORAL_I { NB_TITULO = "M", D_VALUE = LABORAL1_REP_DM.ToString(), I_VALUE = LABORAL1_REP_IM.ToString(), S_VALUE = LABORAL1_REP_SM.ToString(), C_VALUE = LABORAL1_REP_CM.ToString() });
                           vListaResLabI.Add(new GRID_LABORAL_I { NB_TITULO = "L", D_VALUE = LABORAL1_REP_DL.ToString(), I_VALUE = LABORAL1_REP_IL.ToString(), S_VALUE = LABORAL1_REP_SL.ToString(), C_VALUE = LABORAL1_REP_CL.ToString() });
                           vListaResLabI.Add(new GRID_LABORAL_I { NB_TITULO = "Total", D_VALUE = LABORAL1_REP_DT.ToString(), I_VALUE = LABORAL1_REP_IT.ToString(), S_VALUE = LABORAL1_REP_ST.ToString(), C_VALUE = LABORAL1_REP_CT.ToString() });
                           grdResultadosLaboralI.DataSource = vListaResLabI;

                           if(LABORAL1_REP_DT.HasValue)
                           {
                               vNoValidez = vNoValidez+ LABORAL1_REP_DT.Value;
                           }

                           if(LABORAL1_REP_IT.HasValue)
                           {
                               vNoValidez = vNoValidez + LABORAL1_REP_IT.Value;
                           }

                           if(LABORAL1_REP_ST.HasValue)
                           {
                               vNoValidez = vNoValidez + LABORAL1_REP_ST.Value;
                           }
                               
                           if(LABORAL1_REP_CT.HasValue)
                           {
                               vNoValidez = vNoValidez + LABORAL1_REP_CT.Value;
                           }

                           lblValidez.InnerText = "Validez: " + vNoValidez.ToString("N0");

                           List<E_PRUEBA_LABORAL_I> vListaMensajes = new List<E_PRUEBA_LABORAL_I>();
                           foreach (var element in res.Descendants("PRUEBA").Where(item => item.Attribute("CL_PRUEBA").Value.Equals("LABORAL-1")))
                           {
                               vListaMensajes = element.Element("MENSAJES").Elements("MENSAJE").Select(el => new E_PRUEBA_LABORAL_I
                               {
                                   CL_MENSAJE = el.Attribute("CL_MENSAJE").Value,
                                   NB_TITULO = el.Attribute("NB_TITULO").Value,
                                   DS_CONCEPTO = el.Attribute("DS_MENSAJE").Value,
                                   TIPO_MENSAJE = el.Attribute("TIPO_MENSAJE").Value,
                                   SECCION = el.Attribute("SECCION").Value,
                                   NO_ORDEN = el.Attribute("NO_ORDEN").Value
                               }).ToList();
                           }

                           if (vListaMensajes.Count > 0)
                           {
                               var vMensajCotidiano = vListaMensajes.Where(item => item.TIPO_MENSAJE.Contains("COTIDIANO")).OrderByDescending(t => int.Parse(t.NO_ORDEN)).ToList();
                               var vMensajMotivante = vListaMensajes.Where(item => item.TIPO_MENSAJE.Contains("MOTIVANTE")).ToList();
                               var vMensajPresion = vListaMensajes.Where(item => item.TIPO_MENSAJE.Contains("PRESION")).ToList();
                               string Cotidiano = "";
                               string Motivante = "";
                               string Presion = "";
                               string vCaractPersonal = "";
                               string vSituacionMotivante = "";
                               string vSituacionPresionante = "";
                               foreach (var item in vMensajCotidiano)
                               {
                                   Cotidiano += "<b>" + item.NB_TITULO + "</b> </br>" + item.DS_CONCEPTO + "</br>";
                                   vCaractPersonal += item.CL_MENSAJE + " ";
                               }

                               var vEncabezados = vMensajMotivante.Where(item => item.SECCION.Contains("HEAD")).ToList();
                               var vListaClaves = vMensajMotivante.Where(item => item.SECCION.Contains("LISTAS")).ToList();

              //                   Motivante = "<b>" + vEncabezados.Where(item => item.SECCION.Equals("HEADA")).First().NB_TITULO + "</b> </br>" + vEncabezados.Where(item => item.SECCION.Equals("HEADA")).First().DS_CONCEPTO + "</br>" + "<b>" + vListaClaves.Where(item => item.SECCION.Equals("LISTASA")).First().NB_TITULO + "</b> </br>" + vListaClaves.Where(item => item.SECCION.Equals("LISTASA")).First().DS_CONCEPTO + "</br>" +
            //"<b>" + vEncabezados.Where(item => item.SECCION.Equals("HEADB")).First().NB_TITULO + "</b> </br>" + vEncabezados.Where(item => item.SECCION.Equals("HEADB")).First().DS_CONCEPTO + "</br>" + "<b>" + vListaClaves.Where(item => item.SECCION.Equals("LISTASB")).First().NB_TITULO + "</b> </br>" + vListaClaves.Where(item => item.SECCION.Equals("LISTASB")).First().DS_CONCEPTO + "</br>";

                               Motivante = "<b>" + vListaClaves.Where(item => item.SECCION.Equals("LISTASA")).First().NB_TITULO + "</b> </br>" + vEncabezados.Where(item => item.SECCION.Equals("HEADA")).First().DS_CONCEPTO + "</br> </br>" + vListaClaves.Where(item => item.SECCION.Equals("LISTASA")).First().DS_CONCEPTO + "</br>" +
                                           "<b>" + vListaClaves.Where(item => item.SECCION.Equals("LISTASB")).First().NB_TITULO + "</b> </br>" + vEncabezados.Where(item => item.SECCION.Equals("HEADB")).First().DS_CONCEPTO + "</br> </br>" + vListaClaves.Where(item => item.SECCION.Equals("LISTASB")).First().DS_CONCEPTO + "</br>";

                               vSituacionMotivante = vEncabezados.Where(item => item.SECCION.Equals("HEADA")).First().CL_MENSAJE;
                               vSituacionPresionante = vMensajPresion.Where(item => item.SECCION.Equals("LISTASA")).First().CL_MENSAJE;

                               foreach (var item in vMensajPresion.Where(w => w.SECCION == "LISTASA"))
                               {
                                   Presion += "<b>" + item.NB_TITULO + "</b> </br>" + item.DS_CONCEPTO + "</br>";
                               }

                               foreach (var item in vMensajPresion.Where(w => w.SECCION == "LISTASB"))
                               {
                                   Presion += "<b>" + item.NB_TITULO + "</b> </br>" + item.DS_CONCEPTO + "</br>";
                               }

                               divCotidiana.InnerHtml = Cotidiano;
                               divMotivante.InnerHtml = Motivante;
                               divPresion.InnerHtml = Presion;
                               lblCaracteristicas.InnerHtml = "<b>" + vCaractPersonal + "</b>";
                               lblMotivante.InnerHtml = "<b>" + vSituacionMotivante + "</b>";
                               lblPresionante.InnerHtml = "<b>" + vSituacionPresionante + "</b>";
                           }
                       }

                       #endregion

                       #region TECNICA PC

                       ////////////////////////////////////////////////TECNICA PC////////////////////////////////////////////////

                       var vResultadosTecnicaPC = vResultados.Where(r => r.CL_PRUEBA.Equals("TECNICAPC")).ToList();
                       if (vResultadosTecnicaPC.Count > 0)
                       {
                           decimal? TECNICAPC_REP_C = vResultadosTecnicaPC.Exists(ex => ex.CL_VARIABLE.Equals("TECNICAPC_REP_C"))? vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_C")).FirstOrDefault().NO_VALOR : null;
                           decimal? TECNICAPC_REP_S = vResultadosTecnicaPC.Exists(ex => ex.CL_VARIABLE.Equals("TECNICAPC_REP_S"))? vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_S")).FirstOrDefault().NO_VALOR: null;
                           decimal? TECNICAPC_REP_I = vResultadosTecnicaPC.Exists(ex => ex.CL_VARIABLE.Equals("TECNICAPC_REP_I"))? vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_I")).FirstOrDefault().NO_VALOR: null;
                           decimal? TECNICAPC_REP_H = vResultadosTecnicaPC.Exists(ex => ex.CL_VARIABLE.Equals("TECNICAPC_REP_H"))? vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_H")).FirstOrDefault().NO_VALOR: null;

                           decimal? TECNICAPC_REP_P_C = vResultadosTecnicaPC.Exists(ex => ex.CL_VARIABLE.Equals("TECNICAPC_REP_P_C"))? vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_P_C")).FirstOrDefault().NO_VALOR: null;
                           decimal? TECNICAPC_REP_P_S = vResultadosTecnicaPC.Exists(ex => ex.CL_VARIABLE.Equals("TECNICAPC_REP_P_S"))? vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_P_S")).FirstOrDefault().NO_VALOR: null;
                           decimal? TECNICAPC_REP_P_I = vResultadosTecnicaPC.Exists(ex => ex.CL_VARIABLE.Equals("TECNICAPC_REP_P_I"))? vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_P_I")).FirstOrDefault().NO_VALOR: null;
                           decimal? TECNICAPC_REP_P_H = vResultadosTecnicaPC.Exists(ex => ex.CL_VARIABLE.Equals("TECNICAPC_REP_P_H"))? vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_P_H")).FirstOrDefault().NO_VALOR: null;

                           decimal? TECNICAPC_REP_NIVEL_C = vResultadosTecnicaPC.Exists(ex => ex.CL_VARIABLE.Equals("TECNICAPC_REP_NIVEL_C"))? vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_NIVEL_C")).FirstOrDefault().NO_VALOR: null;
                           decimal? TECNICAPC_REP_NIVEL_S = vResultadosTecnicaPC.Exists(ex => ex.CL_VARIABLE.Equals("TECNICAPC_REP_NIVEL_S"))? vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_NIVEL_S")).FirstOrDefault().NO_VALOR: null;
                           decimal? TECNICAPC_REP_NIVEL_I = vResultadosTecnicaPC.Exists(ex => ex.CL_VARIABLE.Equals("TECNICAPC_REP_NIVEL_I"))? vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_NIVEL_I")).FirstOrDefault().NO_VALOR: null;
                           decimal? TECNICAPC_REP_NIVEL_H = vResultadosTecnicaPC.Exists(ex => ex.CL_VARIABLE.Equals("TECNICAPC_REP_NIVEL_H"))? vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_NIVEL_H")).FirstOrDefault().NO_VALOR: null;

                           decimal? TECNICAPC_REP_T = vResultadosTecnicaPC.Exists(ex => ex.CL_VARIABLE.Equals("TECNICAPC_REP_T"))? vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_T")).FirstOrDefault().NO_VALOR: null;
                           decimal? TECNICAPC_REP_P_T = vResultadosTecnicaPC.Exists(ex => ex.CL_VARIABLE.Equals("TECNICAPC_REP_P_T"))? vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_P_T")).FirstOrDefault().NO_VALOR: null;


                           ColumnSeries csTecnicaPc = new ColumnSeries();
                           csTecnicaPc.SeriesItems.Add(new CategorySeriesItem(IsNull(TECNICAPC_REP_P_C)));
                           csTecnicaPc.SeriesItems.Add(new CategorySeriesItem(IsNull(TECNICAPC_REP_P_S)));
                           csTecnicaPc.SeriesItems.Add(new CategorySeriesItem(IsNull(TECNICAPC_REP_P_I)));
                           csTecnicaPc.SeriesItems.Add(new CategorySeriesItem(IsNull(TECNICAPC_REP_P_H)));

                           csTecnicaPc.LabelsAppearance.DataFormatString = "{0:N2}" + "%";
                           csTecnicaPc.TooltipsAppearance.DataFormatString = "{0:N2}" + "%";

                           RadHtmlCSIH.PlotArea.Series.Add(csTecnicaPc);

                           List<GRD_ORT_TEC_ING> vlstTecnica = new List<GRD_ORT_TEC_ING>();
                           vlstTecnica.Add(new GRD_ORT_TEC_ING { NB_TITULO = "Comunicación", ACIERTOS = (Math.Round((decimal)IsNull(TECNICAPC_REP_C), 0)).ToString(), VALORES_MAXIMOS = "20", PORCENTAJE = (Math.Round((decimal)IsNull(TECNICAPC_REP_P_C), 2)).ToString() + "%" });
                           vlstTecnica.Add(new GRD_ORT_TEC_ING { NB_TITULO = "Software", ACIERTOS = (Math.Round((decimal)IsNull(TECNICAPC_REP_S), 0)).ToString(), VALORES_MAXIMOS = "24", PORCENTAJE = (Math.Round((decimal)IsNull(TECNICAPC_REP_P_S), 2)).ToString() + "%" });
                           vlstTecnica.Add(new GRD_ORT_TEC_ING { NB_TITULO = "Internet", ACIERTOS = (Math.Round((decimal)IsNull(TECNICAPC_REP_I), 0)).ToString(), VALORES_MAXIMOS = "16", PORCENTAJE = (Math.Round((decimal)IsNull(TECNICAPC_REP_P_I), 2)).ToString() + "%" });
                           vlstTecnica.Add(new GRD_ORT_TEC_ING { NB_TITULO = "Hardware", ACIERTOS = (Math.Round((decimal)IsNull(TECNICAPC_REP_H), 0)).ToString(), VALORES_MAXIMOS = "24", PORCENTAJE = (Math.Round((decimal)IsNull(TECNICAPC_REP_P_H), 2)).ToString() + "%" });
                           vlstTecnica.Add(new GRD_ORT_TEC_ING { NB_TITULO = "Total", ACIERTOS = (Math.Round((decimal)IsNull(TECNICAPC_REP_T), 0)).ToString(), VALORES_MAXIMOS = "84", PORCENTAJE = (Math.Round((decimal)IsNull(TECNICAPC_REP_P_T), 2)).ToString() + "%" });
                           grdTecnicaPC.DataSource = vlstTecnica;

                           List<GRD_TECNICA_PC> vlstMensajesPC = new List<GRD_TECNICA_PC>();
                           vlstMensajesPC.Add(new GRD_TECNICA_PC { NB_TITULO = "Total Comunicaciones", PORCENTAJE = (Math.Round((decimal)TECNICAPC_REP_P_C, 2)).ToString() + '%', MENSAJE = NivelTecnicaPc((int)TECNICAPC_REP_NIVEL_C) });
                           vlstMensajesPC.Add(new GRD_TECNICA_PC { NB_TITULO = "Total Software", PORCENTAJE = (Math.Round((decimal)TECNICAPC_REP_P_S, 2)).ToString() + '%', MENSAJE = NivelTecnicaPc((int)TECNICAPC_REP_NIVEL_S) });
                           vlstMensajesPC.Add(new GRD_TECNICA_PC { NB_TITULO = "Total Internet", PORCENTAJE = (Math.Round((decimal)TECNICAPC_REP_P_I, 2)).ToString() + '%', MENSAJE = NivelTecnicaPc((int)TECNICAPC_REP_NIVEL_I) });
                           vlstMensajesPC.Add(new GRD_TECNICA_PC { NB_TITULO = "Total Hardware", PORCENTAJE = (Math.Round((decimal)TECNICAPC_REP_P_H, 2)).ToString() + '%', MENSAJE = NivelTecnicaPc((int)TECNICAPC_REP_NIVEL_H) });
                           grdTecnicaMensajesRes.DataSource = vlstMensajesPC;
                       }
                       #endregion

                       #region ADAPTACION AL MEDIO

                       var vResultadosAdaptacion = vResultados.Where(r => r.CL_PRUEBA.Equals("ADAPTACION")).ToList();
                       if (vResultadosAdaptacion.Count > 0)
                       {

                           decimal vP2 = vResultadosAdaptacion.Exists(ex => ex.CL_VARIABLE.Equals("ADAPTACION_REP_P2")) ? vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_P2")).FirstOrDefault().NO_VALOR.Value : 0;
                           decimal vP3 = vResultadosAdaptacion.Exists(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_P3")) ? vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_P3")).FirstOrDefault().NO_VALOR.Value : 0;
                           decimal vP4 = vResultadosAdaptacion.Exists(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_P4")) ? vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_P4")).FirstOrDefault().NO_VALOR.Value : 0;
                           decimal vP5 = vResultadosAdaptacion.Exists(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_P5")) ? vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_P5")).FirstOrDefault().NO_VALOR.Value : 0;
                           decimal vP1 = vResultadosAdaptacion.Exists(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_P1")) ? vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_P1")).FirstOrDefault().NO_VALOR.Value : 0;
                           decimal vP6 = vResultadosAdaptacion.Exists(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_P6")) ? vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_P6")).FirstOrDefault().NO_VALOR.Value : 0;
                           decimal vP0 = vResultadosAdaptacion.Exists(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_P0")) ? vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_P0")).FirstOrDefault().NO_VALOR.Value : 0;
                           decimal vP7 = vResultadosAdaptacion.Exists(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_P7")) ? vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_P7")).FirstOrDefault().NO_VALOR.Value : 0;

                           decimal vA2 = vResultadosAdaptacion.Exists(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A2")) ? vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A2")).FirstOrDefault().NO_VALOR.Value : 0;
                           decimal vA3 = vResultadosAdaptacion.Exists(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A3")) ? vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A3")).FirstOrDefault().NO_VALOR.Value : 0;
                           decimal vA4 = vResultadosAdaptacion.Exists(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A4")) ? vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A4")).FirstOrDefault().NO_VALOR.Value : 0;
                           decimal vA5 = vResultadosAdaptacion.Exists(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A5")) ? vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A5")).FirstOrDefault().NO_VALOR.Value : 0;
                           decimal vA1 = vResultadosAdaptacion.Exists(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A1")) ? vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A1")).FirstOrDefault().NO_VALOR.Value : 0;
                           decimal vA6 = vResultadosAdaptacion.Exists(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A6")) ? vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A6")).FirstOrDefault().NO_VALOR.Value : 0;
                           decimal vA0 = vResultadosAdaptacion.Exists(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A0")) ? vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A0")).FirstOrDefault().NO_VALOR.Value : 0;
                           decimal vA7 = vResultadosAdaptacion.Exists(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A7")) ? vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A7")).FirstOrDefault().NO_VALOR.Value : 0;


                           lblP2.InnerHtml = vP2.ToString("N0") + "%";
                           lblP3.InnerHtml = vP3.ToString("N0") + "%";
                           lblP4.InnerHtml = vP4.ToString("N0") + "%";
                           lblP5.InnerHtml = vP5.ToString("N0") + "%";
                           lblP1.InnerHtml = vP1.ToString("N0") + "%";
                           lblP6.InnerHtml = vP6.ToString("N0") + "%";
                           lblP0.InnerHtml = vP0.ToString("N0") + "%";
                           lblP7.InnerHtml = vP7.ToString("N0") + "%";

                           lblA2.InnerHtml = vA2.ToString("N0") + "%";
                           lblA3.InnerHtml = vA3.ToString("N0") + "%";
                           lblA4.InnerHtml = vA4.ToString("N0") + "%";
                           lblA5.InnerHtml = vA5.ToString("N0") + "%";
                           lblA1.InnerHtml = vA1.ToString("N0") + "%";
                           lblA6.InnerHtml = vA6.ToString("N0") + "%";
                           lblA0.InnerHtml = vA0.ToString("N0") + "%";
                           lblA7.InnerHtml = vA7.ToString("N0") + "%";


                           ColumnSeries csPersonalidad = new ColumnSeries();
                           csPersonalidad.SeriesItems.Add(new CategorySeriesItem(vP2, Color.FromArgb(0, 153, 0)));
                           csPersonalidad.SeriesItems.Add(new CategorySeriesItem(vP3, Color.FromArgb(204, 0, 0)));
                           csPersonalidad.SeriesItems.Add(new CategorySeriesItem(vP4, Color.FromArgb(255, 255, 0)));
                           csPersonalidad.SeriesItems.Add(new CategorySeriesItem(vP5, Color.FromArgb(153, 0, 153)));
                           csPersonalidad.SeriesItems.Add(new CategorySeriesItem(vP1, Color.FromArgb(102, 0, 204)));
                           csPersonalidad.SeriesItems.Add(new CategorySeriesItem(vP6, Color.FromArgb(153, 76, 0)));
                           csPersonalidad.SeriesItems.Add(new CategorySeriesItem(vP0, Color.FromArgb(255, 255, 153)));
                           csPersonalidad.SeriesItems.Add(new CategorySeriesItem(vP7, Color.FromArgb(0, 0, 0)));

                           csPersonalidad.LabelsAppearance.DataFormatString = "{0:N0}" + "%";
                           csPersonalidad.TooltipsAppearance.DataFormatString = "{0:N0}" + "%";

                           ColumnSeries csAdaptacion = new ColumnSeries();
                           csAdaptacion.SeriesItems.Add(new CategorySeriesItem(vA2, Color.FromArgb(102, 255, 102)));
                           csAdaptacion.SeriesItems.Add(new CategorySeriesItem(vA3, Color.FromArgb(255, 102, 102)));
                           csAdaptacion.SeriesItems.Add(new CategorySeriesItem(vA4, Color.FromArgb(255, 255, 102)));
                           csAdaptacion.SeriesItems.Add(new CategorySeriesItem(vA5, Color.FromArgb(255, 153, 254)));
                           csAdaptacion.SeriesItems.Add(new CategorySeriesItem(vA1, Color.FromArgb(178, 102, 255)));
                           csAdaptacion.SeriesItems.Add(new CategorySeriesItem(vA6, Color.FromArgb(255, 178, 102)));
                           csAdaptacion.SeriesItems.Add(new CategorySeriesItem(vA0, Color.FromArgb(255, 255, 204)));
                           csAdaptacion.SeriesItems.Add(new CategorySeriesItem(vA7, Color.FromArgb(96, 96, 96)));

                           csAdaptacion.LabelsAppearance.DataFormatString = "{0:N0}" + "%";
                           csAdaptacion.TooltipsAppearance.DataFormatString = "{0:N0}" + "%";

                           //csAdaptacion.Name = "Adaptación al medio";
                           //csPersonalidad.Name = "Personalidad";

                           rhcAdaptacion.PlotArea.Series.Add(csPersonalidad);
                           rhcAdaptacion.PlotArea.Series.Add(csAdaptacion);

                           string vNeg = vResultadosAdaptacion.Exists(ex => ex.CL_VARIABLE.Equals("ADAPTACION_REP_NEG"))? vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_NEG")).FirstOrDefault().NO_VALOR.Value.ToString("N0") : null;
                           string vPos = vResultadosAdaptacion.Exists(ex => ex.CL_VARIABLE.Equals("ADAPTACION_REP_POS"))? vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_POS")).FirstOrDefault().NO_VALOR.Value.ToString("N0"): null;
                           int vIdDesc = vResultadosAdaptacion.Exists(ex => ex.CL_VARIABLE.Equals("ADAPTACION_REP_DESC"))? int.Parse(vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_DESC")).FirstOrDefault().NO_VALOR.Value.ToString("N0")): 0;

                           lblADesc.InnerHtml = vNeg + ", " + vPos + " : " + DescripcionAdaptacion(vIdDesc);
                       }
                       #endregion
                   }
                   else {
                       mpgResultados.SelectedIndex = 13;
                   }


                   PruebasNegocio nSolicitud = new PruebasNegocio();
                   SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result candidato = new SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result();
                   candidato = nSolicitud.ObtenienePruebasResultadosCandidatos(vIdBateria);
                   //txtClSolicitud.InnerText = candidato.CL_SOLICITUD;
                   txtNbCandidato.InnerText = candidato.NB_CANDIDATO;
                   txtFolio.InnerText = candidato.CL_SOLICITUD;
                   
                }
            }
        }

        private decimal CalcularValorMaximo(List<decimal?> lstValores)
        {
            decimal resultado = 0.00m;

            foreach(var valor in lstValores)
            {
                if(valor.Value > resultado)
                {
                    resultado = valor.Value;
                }
            }

            
            return Math.Truncate(resultado);

        }

        public string NivelOrtografias(double x) 
        {
            string vNivelOrtografias ="";

             if (x < 59.4 ) 
            {
                vNivelOrtografias = "No suficiente";
            }

            else if (x > 59.5 && x <69.4)
            {
                vNivelOrtografias = "Malo";
            }

             else if (x > 69.5 && x < 79.4)
            {
                vNivelOrtografias = "Regular";
            }

             else if (x > 79.5 && x < 89.4)
            {
                vNivelOrtografias = "Bien";
            }

             else if (x > 89.5 && x < 99.4)
            {
                vNivelOrtografias = "Muy bien";
            }

             else if (x > 99.5 && x < 100)
            {
                vNivelOrtografias = "Sobresaliente";
            }
            return vNivelOrtografias;
        }

        public void AgregarColumasSeries(decimal? x) 
        {
            serieOrtografias.SeriesItems.Add(new CategorySeriesItem(IsNull(x)));
           
        }

        public string DescripcionNivelIngles(int x) 
        {
            string nivel = "";
            switch (x) 
            {
                case 1: nivel = "<p style=\"text-align:justify;\">La habilidad del evaluado en este nivel es extremadamente limitada o inexistente. Rara vez se comunica en inglés. Prácticamente sólo puede responder de forma no verbal a indicaciones, enunciados o preguntas que estén expresadas en forma simple.</p>"; break;
                case 2: nivel = "<p style=\"text-align:justify;\">En este nivel, el evaluado podrá entender conversaciones muy breves sobre temas sencillos. Se encuentra muy titubeante y demuestra dificultad para comunicarse. Para apoyar su conversación suele repetir y usar, gestos y comunicación no verbal, debido a lo limitado de sus habilidades y su vocabulario. Tal vez sea capaz de leer algún texto básico como anuncios o letreros con indicaciones básicas. Necesita apoyo contextual y visual que le ayude a comprender. Puede escribir notas sencillas utilizando un vocabulario básico y estructuras comunes de lenguaje. Algo característico de este nivel son los errores frecuentes al hablar, escribir, comprender o leer.</p>"; break;
                case 3: nivel = "<p style=\"text-align:justify;\">En este nivel, el evaluado puede entender un discurso estándar en casi cualquier contexto que contenga un poco de repetición y frases reelaboradas. Puede comunicarse oralmente en la mayoría de las situaciones de la vida cotidiana con un mínimo de dificultad. El candidato en este nivel puede comprender el contenido de muchos textos profesionales de forma independiente, pero no todos. Puede leer la literatura popular de su elección e incluso leerla por placer. Las expresiones idiomáticas y la jerga del inglés le causan dificultad y podría llegar a impedir la comunicación. Puede escribir reportes sencillos de varios párrafos, cartas y pasajes creativos, sin embargo, el estilo y el vocabulario pueden resultar limitados. Puede exponer sus ideas de forma organizada, pero aún se encuentran errores y titubeos ocasionales.</p>"; break;
                case 4: nivel = "<p style=\"text-align:justify;\">En este nivel, el evaluado tiene habilidades de lenguaje adecuadas para comunicarse tanto en situaciones de la vida cotidiana como en situaciones de la vida profesional. De forma ocasional, aún ocurren errores estructurales y léxicos. Aún puede tener dificultad con expresiones idiomáticas y palabras que tienen múltiples significados. También puede tener dificultad con estructuras complejas y conceptos académicos abstractos, pero es capaz de comunicarse en inglés en situaciones nuevas o desconocidas. Al escribir con un propósito específico lo hace de forma adecuada y continúa. Las estructuras, el vocabulario y la organización en general de su escritura, deberían de aproximarse a las de un angloparlante que esté en el mismo nivel académico. No obstante, aún es posible que ocurran errores aislados.</p>"; break;
                case 5: nivel = "<p style=\"text-align:justify;\">En este nivel, el evaluado puede entender y hablar el idioma adecuadamente tanto en conversaciones cotidianas como profesionales sin ningún titubeo o limitación. Tanto la pronunciación, como la fluidez son cercanas al nivel de un angloparlante. Es capaz de entender y utilizar apropiadamente expresiones idiomáticas y la jerga del inglés. Es capaz de leer y entender sin limitaciones todo tipo de lectura. En este nivel el candidato puede leer, hablar y escribir en inglés en casi cualquier situación profesional o de la vida cotidiana. Están presentes, si acaso, sólo algunos pequeños errores.</p>"; break;
                default: nivel = ""; break;
            }
            return nivel;
        }

        public string NivelIngles(int x)
        {
            string nivel = "";
            switch (x)
            {
                case 1: nivel = "Nivel Sin Inglés"; break;
                case 2: nivel = "Nivel Principiante"; break;
                case 3: nivel = "Nivel Intermedio Bajo"; break;
                case 4: nivel = "Nivel Intermedio Alto"; break;
                case 5: nivel = "Nivel Avanzado"; break;
                default: nivel = ""; break;
            }
            return nivel;
        }

        public string NivelTecnicaPc(int x)
        {
            string nivel = "";
            switch (x)
            {
                case 1: nivel = "Sobresaliente"; break;
                case 2: nivel = "Muy bien"; break;
                case 3: nivel = "Bien"; break;
                case 4: nivel = "Regular"; break;
                case 5: nivel = "Malo"; break;
                default: nivel = "No suficiente"; break;
            }
            return nivel;
        }

        public string coeficienteIntelectual(int x) 
        {
            string coeficiente = "";

            switch (x) 
            {
                case 1: coeficiente = "Debilidad mental profunda"; break;
                case 2: coeficiente = "Debilidad mental mediana"; break;
                case 3: coeficiente = "Debilidad mental superficial"; break;
                case 4: coeficiente = "Inteligencia limitrofe"; break;
                case 5: coeficiente = "Inteligencia normal"; break;
                case 6: coeficiente = "Inteligencia superior"; break;
                case 7: coeficiente = "Inteligencia sobresaliente"; break;
                default: coeficiente = "Inválido"; break;
            }
            return coeficiente;
        }

        public string Aprendizaje(int x)
        {
            string Aprendizaje = "";

            switch (x)
            {
                case 1: Aprendizaje = "Deficiente"; break;
                case 2: Aprendizaje = "Inferior"; break;
                case 3: Aprendizaje = "Término medio bajo"; break;
                case 4: Aprendizaje = "Término medio"; break;
                case 5: Aprendizaje = "Término medio alto"; break;
                case 6: Aprendizaje = "Superior"; break;
                case 7: Aprendizaje = "Sobresaliente"; break;
                default: Aprendizaje = "Inválido"; break;
            }
            return Aprendizaje;
        }

        public void HabilitarPruebas(List <E_RESULTADOS_BATERIA>Lista) 
        {
            //PENDIENTES
            if (Lista.Count <= 0)
            {
                tbPruebas.Tabs.ElementAt(13).Visible = true;
                actualizaPosicionInicial("13");
            }

            //if (Lista.Where(item => item.CL_PRUEBA.Equals("ENTREVISTA")).ToList().Count > 0)
            //{
            //    tbPruebas.Tabs.ElementAt(12).Visible = true;
            //    actualizaPosicionInicial("12");
            //}

            // TERMINA PENDIENTES

            //if (Lista.Where(item => item.CL_PRUEBA.Equals("INGLES")).ToList().Count > 0)
            //{
                tbPruebas.Tabs.ElementAt(11).Visible = true;
                actualizaPosicionInicial("11");

            //}
            //if (Lista.Where(item => item.CL_PRUEBA.Equals("REDACCION")).ToList().Count > 0)
            //{
            //    tbPruebas.Tabs.ElementAt(10).Visible = true;
            //    actualizaPosicionInicial("10");

            //}

            //if (Lista.Where(item => item.CL_PRUEBA.Equals("TECNICAPC")).ToList().Count > 0)
            //{
                tbPruebas.Tabs.ElementAt(9).Visible = true;
                actualizaPosicionInicial("9");

            //}

            //if (Lista.Where(item => item.CL_PRUEBA.Equals("ORTOGRAFIA-1")).ToList().Count > 0 || Lista.Where(item => item.CL_PRUEBA.Equals("ORTOGRAFIA-2")).ToList().Count > 0 || Lista.Where(item => item.CL_PRUEBA.Equals("ORTOGRAFIA-3")).ToList().Count > 0)
            //{
                tbPruebas.Tabs.ElementAt(8).Visible = true;
                actualizaPosicionInicial("8");
            //}


            //if (Lista.Where(item => item.CL_PRUEBA.Equals("TIVA")).ToList().Count > 0)
            //{
                tbPruebas.Tabs.ElementAt(7).Visible = true;
                actualizaPosicionInicial("7");
           // }

                //if (Lista.Where(item => item.CL_PRUEBA.Equals("ADAPTACION")).ToList().Count > 0)
                //{
                tbPruebas.Tabs.ElementAt(6).Visible = true;
                actualizaPosicionInicial("6");
                // }


            //if (Lista.Where(item => item.CL_PRUEBA.Equals("LABORAL-2")).ToList().Count > 0)
            //{
                tbPruebas.Tabs.ElementAt(5).Visible = true;
                actualizaPosicionInicial("5");

           // }

            //if (Lista.Where(item => item.CL_PRUEBA.Equals("APTITUD-2")).ToList().Count > 0)
            //{
                tbPruebas.Tabs.ElementAt(4).Visible = true;
                actualizaPosicionInicial("4");

            //}

            //if (Lista.Where(item => item.CL_PRUEBA.Equals("APTITUD-1")).ToList().Count > 0)
            //{
                tbPruebas.Tabs.ElementAt(3).Visible = true;
                actualizaPosicionInicial("3");

            //}

            //if (Lista.Where(item => item.CL_PRUEBA.Equals("PENSAMIENTO")).ToList().Count > 0)
            //{
                tbPruebas.Tabs.ElementAt(2).Visible = true;
                actualizaPosicionInicial("2");
            //}


            //if (Lista.Where(item => item.CL_PRUEBA.Equals("INTERES")).ToList().Count > 0)
            //{
                tbPruebas.Tabs.ElementAt(1).Visible = true;
                actualizaPosicionInicial("1");
            //}

            //if (Lista.Where(item => item.CL_PRUEBA.Equals("LABORAL-1")).ToList().Count > 0)
            //{
                tbPruebas.Tabs.ElementAt(0).Visible = true;
                actualizaPosicionInicial("0");
           // }

           

            mpgResultados.SelectedIndex = int.Parse(vPosicionInicialTab);

        }

        public void actualizaPosicionInicial(string x) 
        {
            if (x != "")
            {
                if (!vPosicionInicialTab.Equals(x))
                {
                    if (int.Parse(vPosicionInicialTab) >= int.Parse(x)) 
                    {
                        vPosicionInicialTab = x;
                    }
                    else { vPosicionInicialTab = x; }
                }
                else { vPosicionInicialTab = x; }
            }
        }

        public decimal? IsNull(decimal? valor) 
        {
            if (valor == null) 
            {
                valor = 0;
            }
            return valor;
        }

        protected void btnTerminar_Click(object sender, EventArgs e)
        {
        }

        private void configurarComparacion1(int seccion)
        {
            switch (seccion)
            {
                case 1:                    
                    c11.Style["background-color"] = ColorTranslator.ToHtml(Color.Red);
                    break;

                case 2:
                    c12.Style["background-color"] = ColorTranslator.ToHtml(Color.Red);
                    break;

                case 3:
                    c13.Style["background-color"] = ColorTranslator.ToHtml(Color.Yellow);
                    break;

                case 4:
                    c14.Style["background-color"] = ColorTranslator.ToHtml(Color.Green);
                    break;

                case 5:
                    c15.Style["background-color"] = ColorTranslator.ToHtml(Color.Green);
                    break;

                default:
                    break;
            }
        }

        private void configurarComparacion2(int seccion)
        {
            switch (seccion)
            {
                case 1:
                    c21.Style["background-color"] = ColorTranslator.ToHtml(Color.Red);
                    break;

                case 2:
                    c22.Style["background-color"] = ColorTranslator.ToHtml(Color.Red);
                    break;

                case 3:
                    c23.Style["background-color"] = ColorTranslator.ToHtml(Color.Red);
                    break;

                case 4:
                    c24.Style["background-color"] = ColorTranslator.ToHtml(Color.Red);
                    break;

                case 5:
                    c25.Style["background-color"] = ColorTranslator.ToHtml(Color.Yellow);
                    break;

                case 6:
                    c26.Style["background-color"] = ColorTranslator.ToHtml(Color.Yellow);
                    break;

                case 7:
                    c27.Style["background-color"] = ColorTranslator.ToHtml(Color.Green);
                    break;

                case 8:
                    c28.Style["background-color"] = ColorTranslator.ToHtml(Color.Green);
                    break;

                case 9:
                    c29.Style["background-color"] = ColorTranslator.ToHtml(Color.Green);
                    break;

                case 10:
                    c210.Style["background-color"] = ColorTranslator.ToHtml(Color.Green);
                    break;

                default:
                    break;
            }
        }

        private void configurarComparacion3(int seccion)
        {
            switch (seccion)
            {
                case 1:
                    c31.Style["background-color"] = ColorTranslator.ToHtml(Color.Red);
                    break;

                case 2:
                    c32.Style["background-color"] = ColorTranslator.ToHtml(Color.Red);
                    break;

                case 3:
                    c33.Style["background-color"] = ColorTranslator.ToHtml(Color.Yellow);
                    break;

                case 4:
                    c34.Style["background-color"] = ColorTranslator.ToHtml(Color.Green);
                    break;

                case 5:
                    c35.Style["background-color"] = ColorTranslator.ToHtml(Color.Green);
                    break;

                default:
                    break;
            }
        }

        private void configurarComparacion4(int seccion)
        {
            switch (seccion)
            {
                case 1:
                    c41.Style["background-color"] = ColorTranslator.ToHtml(Color.Red);
                    break;

                case 2:
                    c42.Style["background-color"] = ColorTranslator.ToHtml(Color.Red);
                    break;

                case 3:
                    c43.Style["background-color"] = ColorTranslator.ToHtml(Color.Yellow);
                    break;

                case 4:
                    c44.Style["background-color"] = ColorTranslator.ToHtml(Color.Green);
                    break;

                case 5:
                    c45.Style["background-color"] = ColorTranslator.ToHtml(Color.Green);
                    break;

                default:
                    break;
            }
        }

        private string DescripcionAdaptacion(int valor)
        {
            string desc = "";

            switch (valor)
            {
                case 0:
                    desc = "Inestabilidad";
                    break;
                case 1:
                    desc = "Optimo riguidez";
                    break;
                case 2:
                    desc = "Movilidad ideal";
                    break;
                case 3:
                    desc = "Movilidad esperada";
                    break;                
            }

            return desc;
        }

        [Serializable]
        public class GRID_LABORAL_I 
        {
            public string NB_TITULO { get; set; }
            public string D_VALUE { get; set; }
            public string I_VALUE { get; set; }
            public string S_VALUE { get; set; }
            public string C_VALUE { get; set; }
        }

        [Serializable]
        public class E_RESULTADOS_GENERICA
        {
            public string NB_TITULO { get; set; }
            public string VALOR { get; set; }
        }

        [Serializable]
        public class GRD_LABORAL_II
        {
            public string DA_AP { get; set; }
            public string TM_CT { get; set; }
            public string MT_CS { get; set; }
            public string AD_NG { get; set; }
            public string TOTAL { get; set; }
        }

        [Serializable]
        public class GRD_ORT_TEC_ING
        {
            public string NB_TITULO { get; set; }
            public string ACIERTOS { get; set; }
            public string VALORES_MAXIMOS { get; set; }
            public string PORCENTAJE { get; set; }
        }

        [Serializable]
        public class GRD_TECNICA_PC
        {
            public string NB_TITULO { get; set; }
            public string PORCENTAJE { get; set; }
            public string MENSAJE { get; set; }
        }
    }
}