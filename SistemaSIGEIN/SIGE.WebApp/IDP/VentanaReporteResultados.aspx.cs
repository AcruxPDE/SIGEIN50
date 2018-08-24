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

namespace SIGE.WebApp.IDP
{
    public partial class VentanaReporteResultados : System.Web.UI.Page
    {
        #region Variables

        decimal? ORTOGRAFIA1_REP_A = 0;
        decimal? ORTOGRAFIA2_REP_A = 0;
        decimal? ORTOGRAFIA3_REP_A = 0;
        decimal ORTOGRAFIAS_TOTAL = 0;
        string OT_REGLASORTOGRAFICAS;
        bool vFactorOrtografia = false;

        private int? vIdBateria
        {
            get { return (int?)ViewState["vs_vIdBateria"]; }
            set { ViewState["vs_vIdBateria"] = value; }
        }

        private List<E_REPORTE_RESULTADO_BAREMOS> vListaBaremos
        {
            get { return (List<E_REPORTE_RESULTADO_BAREMOS>)ViewState["vs_vListaBaremos"]; }
            set { ViewState["vs_vListaBaremos"] = value; }
        }

        private List<E_REPORTE_RESULTADO_BAREMOS> vListaDetailTable
        {
            get { return (List<E_REPORTE_RESULTADO_BAREMOS>)ViewState["vs_vListaDetailTable"]; }
            set { ViewState["vs_vListaDetailTable"] = value; }
        }

        private List<E_RESULTADOS_PRUEBAS_REPORTE> vListaResultadoPruebas
        {
            get { return (List<E_RESULTADOS_PRUEBAS_REPORTE>)ViewState["vs_vListaResultadoPruebas"]; }
            set { ViewState["vs_vListaResultadoPruebas"] = value; }
        }

        private List<E_BAREMOS_PRUEBAS_RESPORTE> vListaResultadosBaremos
        {
            get { return (List<E_BAREMOS_PRUEBAS_RESPORTE>)ViewState["vs_vListaResultadosBaremos"]; }
            set { ViewState["vs_vListaResultadosBaremos"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["pIdBateria"] != null)
                {
                    vIdBateria = int.Parse(Request.Params["pIdBateria"].ToString());
                }

                ResultadosPruebasNegocio nResultadosPruebas = new ResultadosPruebasNegocio();
                vListaResultadoPruebas = nResultadosPruebas.ObtieneResultadosReporte(vIdBateria);
                vListaResultadosBaremos = nResultadosPruebas.ObtieneResultadosBaremosReporte(vIdBateria);
                vListaBaremos = new List<E_REPORTE_RESULTADO_BAREMOS>();


                vListaBaremos = new List<E_REPORTE_RESULTADO_BAREMOS>();

                #region Personalidad Laboral 1

                //if (vListaResultadoPruebas.Where(item => item.CL_PRUEBA.Equals("LABORAL-1")).ToList().Count > 0)
                //{
                // Personalidad laboral I
                //Empuje
                string LABORAL1_REP_DT = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("LABORAL1-REP-DT")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-DT")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string L1_EMPUJE = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("L1-EMPUJE")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("L1-EMPUJE")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Empuje", NO_VALOR_RESPUESTA = (LABORAL1_REP_DT == null ? "NC" : LABORAL1_REP_DT), NO_VALOR_BAREMOS = (L1_EMPUJE == null ? "NC" : L1_EMPUJE), DS_FACTOR = "Personalidad laboral 1" });
                //Influencia
                string LABORAL1_REP_IT = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("LABORAL1-REP-IT")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-IT")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string L1_INFLUENCIA = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("L1-INFLUENCIA")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("L1-INFLUENCIA")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Influencia", NO_VALOR_RESPUESTA = (LABORAL1_REP_IT == null ? "NC" : LABORAL1_REP_IT), NO_VALOR_BAREMOS = (L1_INFLUENCIA == null ? "NC" : L1_INFLUENCIA), DS_FACTOR = "Personalidad laboral 1" });
                //Constancia
                string LABORAL1_REP_ST = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("LABORAL1-REP-ST")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-ST")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string L1_CONSTANCIA = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("L1-CONSTANCIA")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("L1-CONSTANCIA")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Constancia", NO_VALOR_RESPUESTA = (LABORAL1_REP_ST == null ? "NC" : LABORAL1_REP_ST), NO_VALOR_BAREMOS = (L1_CONSTANCIA == null ? "NC" : L1_CONSTANCIA), DS_FACTOR = "Personalidad laboral 1" });
                //Cumplimiento
                string LABORAL1_REP_CT = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("LABORAL1-REP-CT")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-CT")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string L1_CUMPLIMIENTO = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("L1-CUMPLIMIENTO")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("L1-CUMPLIMIENTO")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Cumplimiento", NO_VALOR_RESPUESTA = (LABORAL1_REP_CT == null ? "NC" : LABORAL1_REP_CT), NO_VALOR_BAREMOS = (L1_CUMPLIMIENTO == null ? "NC" : L1_CUMPLIMIENTO), DS_FACTOR = "Personalidad laboral 1" });
                //    }

                #endregion

                #region Personalidad laboral 2

                //if (vListaResultadoPruebas.Where(item => item.CL_PRUEBA.Equals("LABORAL-2")).ToList().Count > 0)
                //{
                // Personalidad laboral 2
                //Da y apoya
                string LABORAL2_REP_DAAPF = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("LABORAL2-REP-DAAPF")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("LABORAL2-REP-DAAPF")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string L2_DAYAPOYA = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("L2-DA Y APOYA")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("L2-DA Y APOYA")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Da y apoya", NO_VALOR_RESPUESTA = (LABORAL2_REP_DAAPF == null ? "NC" : LABORAL2_REP_DAAPF), NO_VALOR_BAREMOS = (L2_DAYAPOYA == null ? "NC" : L2_DAYAPOYA), DS_FACTOR = "Personalidad laboral 2" });

                //Toma y controla
                string LABORAL2_REP_TMCTF = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("LABORAL2-REP-TMCTF")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("LABORAL2-REP-TMCTF")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string L2_TOMAYCONTROLA = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("L2-TOMA Y CONTROLA")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("L2-TOMA Y CONTROLA")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Toma y controla", NO_VALOR_RESPUESTA = (LABORAL2_REP_TMCTF == null ? "NC" : LABORAL2_REP_TMCTF), NO_VALOR_BAREMOS = (L2_TOMAYCONTROLA == null ? "NC" : L2_TOMAYCONTROLA), DS_FACTOR = "Personalidad laboral 2" });

                //Mantiene y conserva
                string LABORAL2_REP_MTCSF = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("LABORAL2-REP-MTCSF")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("LABORAL2-REP-MTCSF")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string L2_MANTIENEYCONSERVA = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("L2-MANTIENE Y CONSERVA")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("L2-MANTIENE Y CONSERVA")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Mantiene y conserva", NO_VALOR_RESPUESTA = (LABORAL2_REP_MTCSF == null ? "NC" : LABORAL2_REP_MTCSF), NO_VALOR_BAREMOS = (L2_MANTIENEYCONSERVA == null ? "NC" : L2_MANTIENEYCONSERVA), DS_FACTOR = "Personalidad laboral 2" });

                //Adapta y negocia
                string LABORAL2_REP_ADNGF = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("LABORAL2-REP-ADNGF")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("LABORAL2-REP-ADNGF")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string L2_ADAPTAYNEGOCIA = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("L2-ADAPTA Y NEGOCIA")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("L2-ADAPTA Y NEGOCIA")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Adapta y negocia", NO_VALOR_RESPUESTA = (LABORAL2_REP_ADNGF == null ? "NC" : LABORAL2_REP_ADNGF), NO_VALOR_BAREMOS = (L2_ADAPTAYNEGOCIA == null ? "NC" : L2_ADAPTAYNEGOCIA), DS_FACTOR = "Personalidad laboral 2" });

                // }

                #endregion

                #region Estilo de pensamiento

                //if (vListaResultadoPruebas.Where(item => item.CL_PRUEBA.Equals("PENSAMIENTO")).ToList().Count > 0)
                //{
                // Estilo de pensamiento
                //Análisis
                string PENSAMIENTO_REP_A = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("PENSAMIENTO_REP_A")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("PENSAMIENTO_REP_A")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string PN_ANALISIS = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("PN-ANÁLISIS")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("PN-ANÁLISIS")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Análisis", NO_VALOR_RESPUESTA = (PENSAMIENTO_REP_A == null ? "NC" : PENSAMIENTO_REP_A), NO_VALOR_BAREMOS = (PN_ANALISIS == null ? "NC" : PN_ANALISIS), DS_FACTOR = "Estilo de pensamiento" });
                //Visión
                string PENSAMIENTO_REP_L = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("PENSAMIENTO_REP_V")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("PENSAMIENTO_REP_V")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string PN_VISION = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("PN-VISIÓN")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("PN-VISIÓN")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Visión", NO_VALOR_RESPUESTA = (PENSAMIENTO_REP_L == null ? "NC" : PENSAMIENTO_REP_L), NO_VALOR_BAREMOS = (PN_VISION == null ? "NC" : PN_VISION), DS_FACTOR = "Estilo de pensamiento" });
                //Intuición
                string PENSAMIENTO_REP_I = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("PENSAMIENTO_REP_I")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("PENSAMIENTO_REP_I")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string PN_INTUICION = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("PN-INTUICIÓN")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("PN-INTUICIÓN")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Intuición", NO_VALOR_RESPUESTA = (PENSAMIENTO_REP_I == null ? "NC" : PENSAMIENTO_REP_I), NO_VALOR_BAREMOS = (PN_INTUICION == null ? "NC" : PN_INTUICION), DS_FACTOR = "Estilo de pensamiento" });
                //Lógica
                string PENSAMIENTO_REP_V = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("PENSAMIENTO_REP_L")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("PENSAMIENTO_REP_L")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string PN_LOGICA = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("PN-LÓGICA")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("PN-LÓGICA")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Lógica", NO_VALOR_RESPUESTA = (PENSAMIENTO_REP_V == null ? "NC" : PENSAMIENTO_REP_V), NO_VALOR_BAREMOS = (PN_LOGICA == null ? "NC" : PN_LOGICA), DS_FACTOR = "Estilo de pensamiento" });
                //  }

                #endregion

                #region Intereses personales

                //if (vListaResultadoPruebas.Where(item => item.CL_PRUEBA.Equals("INTERES")).ToList().Count > 0)
                //{
                // Intereses personales
                //Teórico
                string INTERES_REP_T = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("INTERES_REP_T")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("INTERES_REP_T")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string IN_TEORICO = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("IN-TEÓRICO")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("IN-TEÓRICO")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Teórico", NO_VALOR_RESPUESTA = (INTERES_REP_T == null ? "NC" : INTERES_REP_T), NO_VALOR_BAREMOS = (IN_TEORICO == null ? "NC" : IN_TEORICO), DS_FACTOR = "Intereses Personales" });
                //Económico
                string INTERES_REP_E = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("INTERES_REP_E")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("INTERES_REP_E")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string IN_ECONOMICO = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("IN-ECONÓMICO")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("IN-ECONÓMICO")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Económico", NO_VALOR_RESPUESTA = (INTERES_REP_E == null ? "NC" : INTERES_REP_E), NO_VALOR_BAREMOS = (IN_ECONOMICO == null ? "NC" : IN_ECONOMICO), DS_FACTOR = "Intereses Personales" });
                //Artístico Estético
                string INTERES_REP_A = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("INTERES_REP_A")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("INTERES_REP_A")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string IN_ARTISTICOESTETICO = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("IN-ARTÍSTICO ESTÉTICO")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("IN-ARTÍSTICO ESTÉTICO")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Artístico Estético", NO_VALOR_RESPUESTA = (INTERES_REP_A == null ? "NC" : INTERES_REP_A), NO_VALOR_BAREMOS = (IN_ARTISTICOESTETICO == null ? "NC" : IN_ARTISTICOESTETICO), DS_FACTOR = "Intereses Personales" });
                //Social
                string INTERES_REP_S = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("INTERES_REP_S")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("INTERES_REP_S")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string IN_SOCIAL = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("IN-SOCIAL")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("IN-SOCIAL")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Social", NO_VALOR_RESPUESTA = (INTERES_REP_S == null ? "NC" : INTERES_REP_S), NO_VALOR_BAREMOS = (IN_SOCIAL == null ? "NC" : IN_SOCIAL), DS_FACTOR = "Intereses Personales" });
                //Político
                string INTERES_REP_P = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("INTERES_REP_P")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("INTERES_REP_P")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string IN_POLITICO = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("IN-POLÍTICO")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("IN-POLÍTICO")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Político", NO_VALOR_RESPUESTA = (INTERES_REP_P == null ? "NC" : INTERES_REP_P), NO_VALOR_BAREMOS = (IN_POLITICO == null ? "NC" : IN_POLITICO), DS_FACTOR = "Intereses Personales" });
                //Regulatorio
                string INTERES_REP_R = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("INTERES_REP_R")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("INTERES_REP_R")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string IN_REGULATORIO = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("IN-REGULATORIO")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("IN-REGULATORIO")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Regulatorio", NO_VALOR_RESPUESTA = (INTERES_REP_R == null ? "NC" : INTERES_REP_R), NO_VALOR_BAREMOS = (IN_REGULATORIO == null ? "NC" : IN_REGULATORIO), DS_FACTOR = "Intereses Personales" });
                // }

                #endregion

                #region Actitud Mental

                if (vListaResultadosBaremos.Where(item => item.NB_PRUEBA.Equals("APTITUD-1")).ToList().Count > 0)
                {
                    // Actitud Mental
                    //Cultura General
                    string APTITUD1_REP_0001 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0001")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0001")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                    string AT_CULTURAGENERAL = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AT-CULTURA GENERAL")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AT-CULTURA GENERAL")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Cultura general", NO_VALOR_RESPUESTA = (APTITUD1_REP_0001 == null ? "NC" : APTITUD1_REP_0001), NO_VALOR_BAREMOS = (AT_CULTURAGENERAL == null ? "NC" : AT_CULTURAGENERAL), DS_FACTOR = "Aptitud mental" });
                    //Capacidad de juicio
                    string APTITUD1_REP_0002 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0002")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0002")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                    string AT_CAPACIDADDEJUICIO = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE JUICIO")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE JUICIO")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Capacidad de juicio", NO_VALOR_RESPUESTA = (APTITUD1_REP_0002 == null ? "NC" : APTITUD1_REP_0002), NO_VALOR_BAREMOS = (AT_CAPACIDADDEJUICIO == null ? "NC" : AT_CAPACIDADDEJUICIO), DS_FACTOR = "Aptitud mental" });
                    //Capacidad de análisis y síntesis
                    string APTITUD1_REP_0003 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0003")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0003")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                    string AT_CAPACIDADDEANALISISYSINTESIS = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE ANÁLISIS Y SÍNTESIS")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE ANÁLISIS Y SÍNTESIS")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Capacidad de análisis y síntesis", NO_VALOR_RESPUESTA = (APTITUD1_REP_0003 == null ? "NC" : APTITUD1_REP_0003), NO_VALOR_BAREMOS = (AT_CAPACIDADDEANALISISYSINTESIS == null ? "NC" : AT_CAPACIDADDEANALISISYSINTESIS), DS_FACTOR = "Aptitud mental" });
                    //Capacidad de abstracción
                    string APTITUD1_REP_0004 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0004")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0004")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                    string AT_CAPACIDADDEABSTRACCION = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE ABSTRACCIÓN")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE ABSTRACCIÓN")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Capacidad de abstracción", NO_VALOR_RESPUESTA = (APTITUD1_REP_0004 == null ? "NC" : APTITUD1_REP_0004), NO_VALOR_BAREMOS = (AT_CAPACIDADDEABSTRACCION == null ? "NC" : AT_CAPACIDADDEABSTRACCION), DS_FACTOR = "Aptitud mental" });
                    //Capacidad de razonamiento
                    string APTITUD1_REP_0005 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0005")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0005")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                    string AT_CAPACIDADDERAZONAMIENTO = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE RAZONAMIENTO")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE RAZONAMIENTO")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Capacidad de razonamiento", NO_VALOR_RESPUESTA = (APTITUD1_REP_0005 == null ? "NC" : APTITUD1_REP_0005), NO_VALOR_BAREMOS = (AT_CAPACIDADDERAZONAMIENTO == null ? "NC" : AT_CAPACIDADDERAZONAMIENTO), DS_FACTOR = "Aptitud mental" });
                    //Sentido común
                    string APTITUD1_REP_0006 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0006")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0006")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                    string AT_SENTIDOCOMUN = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AT-SENTIDO COMÚN")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AT-SENTIDO COMÚN")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Sentido común", NO_VALOR_RESPUESTA = (APTITUD1_REP_0006 == null ? "NC" : APTITUD1_REP_0006), NO_VALOR_BAREMOS = (AT_SENTIDOCOMUN == null ? "NC" : AT_SENTIDOCOMUN), DS_FACTOR = "Aptitud mental" });
                    //Pensamiento organizado
                    string APTITUD1_REP_0007 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0007")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0007")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                    string AT_PENSAMIENTO_ORGANIZADO = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AT-PENSAMIENTO ORGANIZADO")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AT-PENSAMIENTO ORGANIZADO")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Pensamiento organizado", NO_VALOR_RESPUESTA = (APTITUD1_REP_0007 == null ? "NC" : APTITUD1_REP_0007), NO_VALOR_BAREMOS = (AT_PENSAMIENTO_ORGANIZADO == null ? "NC" : AT_PENSAMIENTO_ORGANIZADO), DS_FACTOR = "Aptitud mental" });
                    //Capacidad de planeación
                    string APTITUD1_REP_0008 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0008")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0008")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                    string AT_CAPACIDADDEPLANEACION = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE PLANEACIÓN")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE PLANEACIÓN")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Capacidad de planeación", NO_VALOR_RESPUESTA = (APTITUD1_REP_0008 == null ? "NC" : APTITUD1_REP_0008), NO_VALOR_BAREMOS = (AT_CAPACIDADDEPLANEACION == null ? "NC" : AT_CAPACIDADDEPLANEACION), DS_FACTOR = "Aptitud mental" });
                    //Capacidad de discriminación
                    string APTITUD1_REP_0009 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0009")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0009")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                    string AT_CAPACIDADDEDISCRIMINACION = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE DISCRIMINACIÓN")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE DISCRIMINACIÓN")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Capacidad de discriminación", NO_VALOR_RESPUESTA = (APTITUD1_REP_0009 == null ? "NC" : APTITUD1_REP_0009), NO_VALOR_BAREMOS = (AT_CAPACIDADDEDISCRIMINACION == null ? "NC" : AT_CAPACIDADDEDISCRIMINACION), DS_FACTOR = "Aptitud mental" });
                    //Capacidad de deducción
                    string APTITUD1_REP_0010 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0010")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0010")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                    string AT_CAPACIDADDEDEDUCCION = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE DEDUCCIÓN")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE DEDUCCIÓN")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Capacidad de deducción", NO_VALOR_RESPUESTA = (APTITUD1_REP_0010 == null ? "NC" : APTITUD1_REP_0010), NO_VALOR_BAREMOS = (AT_CAPACIDADDEDEDUCCION == null ? "NC" : AT_CAPACIDADDEDEDUCCION), DS_FACTOR = "Aptitud mental" });
                    //Inteligencia
                    string APTITUD1_REP_TOTAL = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("APTITUD1_REP_TOTAL")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_TOTAL")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                    string AT_INTELIGENCIA = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AT-APRENDIZAJE")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AT-APRENDIZAJE")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Aprendizaje", NO_VALOR_RESPUESTA = (APTITUD1_REP_TOTAL == null ? "NC" : APTITUD1_REP_TOTAL), NO_VALOR_BAREMOS = (AT_INTELIGENCIA == null ? "NC" : AT_INTELIGENCIA), DS_FACTOR = "Aptitud mental" });
                    //Aprendizaje
                    string APTITUD1_REP_CI = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("APTITUD1_REP_CI")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_CI")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                    string AT_APRENDIZAJE = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AT-INTELIGENCIA")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AT-INTELIGENCIA")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Inteligencia", NO_VALOR_RESPUESTA = (APTITUD1_REP_CI == null ? "NC" : APTITUD1_REP_CI), NO_VALOR_BAREMOS = (AT_APRENDIZAJE == null ? "NC" : AT_APRENDIZAJE), DS_FACTOR = "Aptitud mental" });
                }

                else if (vListaResultadosBaremos.Where(item => item.NB_PRUEBA.Equals("APTITUD-2")).ToList().Count > 0)
                {
                    // Actitud Mental
                    //Cultura General
                    string APTITUD1_REP_0001 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("APTITUD2_REP_CONOCIMIENTO")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_CONOCIMIENTO")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                    string AT_CULTURAGENERAL = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AT-CULTURA GENERAL")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AT-CULTURA GENERAL")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Cultura general", NO_VALOR_RESPUESTA = (APTITUD1_REP_0001 == null ? "NC" : APTITUD1_REP_0001), NO_VALOR_BAREMOS = (AT_CULTURAGENERAL == null ? "NC" : AT_CULTURAGENERAL), DS_FACTOR = "Aptitud mental" });
                    //Capacidad de juicio
                    string APTITUD1_REP_0002 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("APTITUD2_REP_COMPRENSION")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_COMPRENSION")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                    string AT_CAPACIDADDEJUICIO = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE JUICIO")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE JUICIO")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Capacidad de juicio", NO_VALOR_RESPUESTA = (APTITUD1_REP_0002 == null ? "NC" : APTITUD1_REP_0002), NO_VALOR_BAREMOS = (AT_CAPACIDADDEJUICIO == null ? "NC" : AT_CAPACIDADDEJUICIO), DS_FACTOR = "Aptitud mental" });
                    //Capacidad de análisis y síntesis
                    string APTITUD1_REP_0003 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("APTITUD2_REP_SIGNIFICADO")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_SIGNIFICADO")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                    string AT_CAPACIDADDEANALISISYSINTESIS = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE ANÁLISIS Y SÍNTESIS")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE ANÁLISIS Y SÍNTESIS")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Capacidad de análisis y síntesis", NO_VALOR_RESPUESTA = (APTITUD1_REP_0003 == null ? "NC" : APTITUD1_REP_0003), NO_VALOR_BAREMOS = (AT_CAPACIDADDEANALISISYSINTESIS == null ? "NC" : AT_CAPACIDADDEANALISISYSINTESIS), DS_FACTOR = "Aptitud mental" });
                    //Capacidad de abstracción
                    string APTITUD1_REP_0004 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("APTITUD2_REP_LOGICA")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_LOGICA")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                    string AT_CAPACIDADDEABSTRACCION = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE ABSTRACCIÓN")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE ABSTRACCIÓN")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Capacidad de abstracción", NO_VALOR_RESPUESTA = (APTITUD1_REP_0004 == null ? "NC" : APTITUD1_REP_0004), NO_VALOR_BAREMOS = (AT_CAPACIDADDEABSTRACCION == null ? "NC" : AT_CAPACIDADDEABSTRACCION), DS_FACTOR = "Aptitud mental" });
                    //Capacidad de razonamiento
                    string APTITUD1_REP_0005 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("APTITUD2_REP_ARITMETICA")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_ARITMETICA")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                    string AT_CAPACIDADDERAZONAMIENTO = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE RAZONAMIENTO")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE RAZONAMIENTO")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Capacidad de razonamiento", NO_VALOR_RESPUESTA = (APTITUD1_REP_0005 == null ? "NC" : APTITUD1_REP_0005), NO_VALOR_BAREMOS = (AT_CAPACIDADDERAZONAMIENTO == null ? "NC" : AT_CAPACIDADDERAZONAMIENTO), DS_FACTOR = "Aptitud mental" });
                    //Sentido común
                    string APTITUD1_REP_0006 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("APTITUD2_REP_JUICIO")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_JUICIO")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                    string AT_SENTIDOCOMUN = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AT-SENTIDO COMÚN")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AT-SENTIDO COMÚN")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Sentido común", NO_VALOR_RESPUESTA = (APTITUD1_REP_0006 == null ? "NC" : APTITUD1_REP_0006), NO_VALOR_BAREMOS = (AT_SENTIDOCOMUN == null ? "NC" : AT_SENTIDOCOMUN), DS_FACTOR = "Aptitud mental" });
                    //Pensamiento organizado
                    string APTITUD1_REP_0007 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("APTITUD2_REP_ANALOGIAS")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_ANALOGIAS")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                    string AT_PENSAMIENTO_ORGANIZADO = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AT-PENSAMIENTO ORGANIZADO")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AT-PENSAMIENTO ORGANIZADO")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Pensamiento organizado", NO_VALOR_RESPUESTA = (APTITUD1_REP_0007 == null ? "NC" : APTITUD1_REP_0007), NO_VALOR_BAREMOS = (AT_PENSAMIENTO_ORGANIZADO == null ? "NC" : AT_PENSAMIENTO_ORGANIZADO), DS_FACTOR = "Aptitud mental" });
                    //Capacidad de planeación
                    string APTITUD1_REP_0008 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("APTITUD2_REP_ORDENAMIENTO")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_ORDENAMIENTO")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                    string AT_CAPACIDADDEPLANEACION = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE PLANEACIÓN")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE PLANEACIÓN")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Capacidad de planeación", NO_VALOR_RESPUESTA = (APTITUD1_REP_0008 == null ? "NC" : APTITUD1_REP_0008), NO_VALOR_BAREMOS = (AT_CAPACIDADDEPLANEACION == null ? "NC" : AT_CAPACIDADDEPLANEACION), DS_FACTOR = "Aptitud mental" });
                    //Capacidad de discriminación
                    string APTITUD1_REP_0009 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("APTITUD2_REP_CLASIFICACION")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_CLASIFICACION")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                    string AT_CAPACIDADDEDISCRIMINACION = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE DISCRIMINACIÓN")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE DISCRIMINACIÓN")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Capacidad de discriminación", NO_VALOR_RESPUESTA = (APTITUD1_REP_0009 == null ? "NC" : APTITUD1_REP_0009), NO_VALOR_BAREMOS = (AT_CAPACIDADDEDISCRIMINACION == null ? "NC" : AT_CAPACIDADDEDISCRIMINACION), DS_FACTOR = "Aptitud mental" });
                    //Capacidad de deducción
                    string APTITUD1_REP_0010 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("APTITUD2_REP_SERIACION")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_SERIACION")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                    string AT_CAPACIDADDEDEDUCCION = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE DEDUCCIÓN")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AT-CAPACIDAD DE DEDUCCIÓN")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Capacidad de deducción", NO_VALOR_RESPUESTA = (APTITUD1_REP_0010 == null ? "NC" : APTITUD1_REP_0010), NO_VALOR_BAREMOS = (AT_CAPACIDADDEDEDUCCION == null ? "NC" : AT_CAPACIDADDEDEDUCCION), DS_FACTOR = "Aptitud mental" });
                    //Inteligencia
                    string APTITUD1_REP_TOTAL = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("APTITUD2_REP_CI")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_CI")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                    string AT_INTELIGENCIA = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AT-INTELIGENCIA")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AT-INTELIGENCIA")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Inteligencia", NO_VALOR_RESPUESTA = (APTITUD1_REP_TOTAL == null ? "NC" : APTITUD1_REP_TOTAL), NO_VALOR_BAREMOS = (AT_INTELIGENCIA == null ? "NC" : AT_INTELIGENCIA), DS_FACTOR = "Aptitud mental" });
                    //Aprendizaje
                    string APTITUD1_REP_CI = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("APTITUD2_REP_ACIERTOS")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_ACIERTOS")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                    string AT_APRENDIZAJE = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AT-APRENDIZAJE")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AT-APRENDIZAJE")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Aprendizaje", NO_VALOR_RESPUESTA = (APTITUD1_REP_CI == null ? "NC" : APTITUD1_REP_CI), NO_VALOR_BAREMOS = (AT_APRENDIZAJE == null ? "NC" : AT_APRENDIZAJE), DS_FACTOR = "Aptitud mental" });
                }
                else
                {
                    // Actitud Mental
                    //Cultura General
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Cultura general", NO_VALOR_RESPUESTA = "NC", NO_VALOR_BAREMOS = "NC", DS_FACTOR = "Aptitud mental" });
                    //Capacidad de juicio
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Capacidad de juicio", NO_VALOR_RESPUESTA = "NC", NO_VALOR_BAREMOS = "NC", DS_FACTOR = "Aptitud mental" });
                    //Capacidad de análisis y síntesis
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Capacidad de análisis y síntesis", NO_VALOR_RESPUESTA = "NC", NO_VALOR_BAREMOS = "NC", DS_FACTOR = "Aptitud mental" });
                    //Capacidad de abstracción
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Capacidad de abstracción", NO_VALOR_RESPUESTA = "NC", NO_VALOR_BAREMOS = "NC", DS_FACTOR = "Aptitud mental" });
                    //Capacidad de razonamiento
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Capacidad de razonamiento", NO_VALOR_RESPUESTA = "NC", NO_VALOR_BAREMOS = "NC", DS_FACTOR = "Aptitud mental" });
                    //Sentido común
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Sentido común", NO_VALOR_RESPUESTA = "NC", NO_VALOR_BAREMOS = "NC", DS_FACTOR = "Aptitud mental" });
                    //Pensamiento organizado
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Pensamiento organizado", NO_VALOR_RESPUESTA = "NC", NO_VALOR_BAREMOS = "NC", DS_FACTOR = "Aptitud mental" });
                    //Capacidad de planeación
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Capacidad de planeación", NO_VALOR_RESPUESTA = "NC", NO_VALOR_BAREMOS = "NC", DS_FACTOR = "Aptitud mental" });
                    //Capacidad de discriminación
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Capacidad de discriminación", NO_VALOR_RESPUESTA = "NC", NO_VALOR_BAREMOS = "NC", DS_FACTOR = "Aptitud mental" });
                    //Capacidad de deducción
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Capacidad de deducción", NO_VALOR_RESPUESTA = "NC", NO_VALOR_BAREMOS = "NC", DS_FACTOR = "Aptitud mental" });
                    //Inteligencia
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Inteligencia", NO_VALOR_RESPUESTA = "NC", NO_VALOR_BAREMOS = "NC", DS_FACTOR = "Aptitud mental" });
                    //Aprendizaje
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Aprendizaje", NO_VALOR_RESPUESTA = "NC", NO_VALOR_BAREMOS = "NC", DS_FACTOR = "Aptitud mental" });

                }

                #endregion

                #region Comunicacion

                // Comunicación
                //if (vListaResultadoPruebas.Where(item => item.CL_PRUEBA.Equals("REDACCION")).ToList().Count > 0)
                //{
                //Redacción
                string REDACCION_RES_0001 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("REDACCION_RES_0001")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("REDACCION_RES_0001")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string REDACCION = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("REDACCION")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("REDACCION")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Redacción", NO_VALOR_RESPUESTA = (REDACCION_RES_0001 == null ? "NC" : REDACCION_RES_0001), NO_VALOR_BAREMOS = (REDACCION == null ? "NC" : REDACCION), DS_FACTOR = "Comunicación" });

                //}

                if (vListaResultadoPruebas.Where(item => item.CL_PRUEBA.Equals("ORTOGRAFIA-1")).ToList().Count > 0)
                {
                    // Ortografía
                    vFactorOrtografia = true;
                    ORTOGRAFIA1_REP_A = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("ORTOGRAFIA1-REP-#A")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("ORTOGRAFIA1-REP-#A")).FirstOrDefault().NO_VALOR : 0;
                    OT_REGLASORTOGRAFICAS = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("OT-REGLAS ORTOGRÁFICAS")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("OT-REGLAS ORTOGRÁFICAS")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                }
                if (vListaResultadoPruebas.Where(item => item.CL_PRUEBA.Equals("ORTOGRAFIA-2")).ToList().Count > 0)
                {
                    vFactorOrtografia = true;
                    ORTOGRAFIA2_REP_A = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("ORTOGRAFIA2-REP-#A")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("ORTOGRAFIA2-REP-#A")).FirstOrDefault().NO_VALOR : 0;
                    OT_REGLASORTOGRAFICAS = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("OT-REGLAS ORTOGRÁFICAS")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("OT-REGLAS ORTOGRÁFICAS")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                }

                if (vListaResultadoPruebas.Where(item => item.CL_PRUEBA.Equals("ORTOGRAFIA-3")).ToList().Count > 0)
                {
                    vFactorOrtografia = true;
                    ORTOGRAFIA3_REP_A = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("ORTOGRAFIA3-REP-#A")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("ORTOGRAFIA3-REP-#A")).FirstOrDefault().NO_VALOR : 0;
                    OT_REGLASORTOGRAFICAS = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("OT-REGLAS ORTOGRÁFICAS")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("OT-REGLAS ORTOGRÁFICAS")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                }

                if (vFactorOrtografia == true)
                {
                    ORTOGRAFIAS_TOTAL = (decimal)ORTOGRAFIA1_REP_A + (decimal)ORTOGRAFIA2_REP_A + (decimal)ORTOGRAFIA3_REP_A;
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Ortografía", NO_VALOR_RESPUESTA = ORTOGRAFIAS_TOTAL.ToString("0.00"), NO_VALOR_BAREMOS = OT_REGLASORTOGRAFICAS.ToString(), DS_FACTOR = "Comunicación" });
                }
                else if (vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("OT-REGLAS ORTOGRÁFICAS")))
                {
                    OT_REGLASORTOGRAFICAS = vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("OT-REGLAS ORTOGRÁFICAS")).FirstOrDefault().NO_VALOR.ToString("0");
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Ortografía", NO_VALOR_RESPUESTA = "NC", NO_VALOR_BAREMOS = OT_REGLASORTOGRAFICAS, DS_FACTOR = "Comunicación" });
                }
                else
                {
                    vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Ortografía", NO_VALOR_RESPUESTA = "NC", NO_VALOR_BAREMOS = "NC", DS_FACTOR = "Comunicación" });
                }

                //if (vListaResultadoPruebas.Where(item => item.CL_PRUEBA.Equals("ENTREVISTA")).ToList().Count > 0)
                //{
                //Comunicación verbal
                string REDACCION_RES_0002 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("ENTREVISTA_RES_0001")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("ENTREVISTA_RES_0001")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string ENT_COMUNICACIONVERBAL = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("ENT-COMUNICACIÓN VERBAL")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("ENT-COMUNICACIÓN VERBAL")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Comunicación verbal", NO_VALOR_RESPUESTA = (REDACCION_RES_0002 == null ? "NC" : REDACCION_RES_0002), NO_VALOR_BAREMOS = (ENT_COMUNICACIONVERBAL == null ? "NC" : ENT_COMUNICACIONVERBAL), DS_FACTOR = "Comunicación" });
                //Comunicación no verbaL
                string REDACCION_RES_0003 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("ENTREVISTA_RES_0002")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("ENTREVISTA_RES_0002")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string ENT_COMUNICACIONNOVERBAL = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("ENT-COMUNICACIÓN NO VERBAL")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("ENT-COMUNICACIÓN NO VERBAL")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Comunicación no verbal", NO_VALOR_RESPUESTA = (REDACCION_RES_0003 == null ? "NC" : REDACCION_RES_0003), NO_VALOR_BAREMOS = (ENT_COMUNICACIONNOVERBAL == null ? "NC" : ENT_COMUNICACIONNOVERBAL), DS_FACTOR = "Comunicación" });

                //  }

                #endregion

                #region Adapatacion al medio (colores)

                //if (vListaResultadoPruebas.Where(item => item.CL_PRUEBA.Equals("ADAPTACION")).ToList().Count > 0)
                //{
                // Adaptación al medio (colores)
                //Productividad
                string ADAPTACION_REP_A0 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A2")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A2")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string AP_PRODUCTIVIDAD = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AP-PRODUCTIVIDAD")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AP-PRODUCTIVIDAD")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Productividad", NO_VALOR_RESPUESTA = (ADAPTACION_REP_A0 == null ? "NC" : ADAPTACION_REP_A0), NO_VALOR_BAREMOS = (AP_PRODUCTIVIDAD == null ? "NC" : AP_PRODUCTIVIDAD), DS_FACTOR = "Colores" });
                //Empuje
                string ADAPTACION_REP_A1 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A3")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A3")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string AP_EMPUJE = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AP-EMPUJE")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AP-EMPUJE")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Empuje", NO_VALOR_RESPUESTA = (ADAPTACION_REP_A1 == null ? "NC" : ADAPTACION_REP_A1), NO_VALOR_BAREMOS = (AP_EMPUJE == null ? "NC" : AP_EMPUJE), DS_FACTOR = "Colores" });
                //Sociabilidad
                string ADAPTACION_REP_A2 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A4")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A4")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string AP_SOCIABILIDAD = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AP-SOCIABILIDAD")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AP-SOCIABILIDAD")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Sociabilidad", NO_VALOR_RESPUESTA = (ADAPTACION_REP_A2 == null ? "NC" : ADAPTACION_REP_A2), NO_VALOR_BAREMOS = (AP_SOCIABILIDAD == null ? "NC" : AP_SOCIABILIDAD), DS_FACTOR = "Colores" });
                //Creatividad
                string ADAPTACION_REP_A3 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A5")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A5")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string AP_CREATIVIDAD = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AP-CREATIVIDAD")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AP-CREATIVIDAD")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Creatividad", NO_VALOR_RESPUESTA = (ADAPTACION_REP_A3 == null ? "NC" : ADAPTACION_REP_A3), NO_VALOR_BAREMOS = (AP_CREATIVIDAD == null ? "NC" : AP_CREATIVIDAD), DS_FACTOR = "Colores" });
                //Paciencia
                string ADAPTACION_REP_A4 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A1")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A1")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string AP_PACIENCIA = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AP-PACIENCIA")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AP-PACIENCIA")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Paciencia", NO_VALOR_RESPUESTA = (ADAPTACION_REP_A4 == null ? "NC" : ADAPTACION_REP_A4), NO_VALOR_BAREMOS = (AP_PACIENCIA == null ? "NC" : AP_PACIENCIA), DS_FACTOR = "Colores" });
                //Energia
                string ADAPTACION_REP_A5 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A6")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A6")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string AP_ENERGIA = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AP-ENERGÍA")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AP-ENERGÍA")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Energía", NO_VALOR_RESPUESTA = (ADAPTACION_REP_A5 == null ? "NC" : ADAPTACION_REP_A5), NO_VALOR_BAREMOS = (AP_ENERGIA == null ? "NC" : AP_ENERGIA), DS_FACTOR = "Colores" });
                //Participación
                string ADAPTACION_REP_A6 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A0")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A0")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string AP_PARTICIPACION = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AP-PARTICIPACIÓN")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AP-PARTICIPACIÓN")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Participación", NO_VALOR_RESPUESTA = (ADAPTACION_REP_A6 == null ? "NC" : ADAPTACION_REP_A6), NO_VALOR_BAREMOS = (AP_PARTICIPACION == null ? "NC" : AP_PARTICIPACION), DS_FACTOR = "Colores" });
                //Autoestima y seguridad
                string ADAPTACION_REP_A7 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A7")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A7")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string AP_AUTOESTIMAYSEGURIDAD = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("AP-AUTOESTIMA Y SEGURIDAD")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("AP-AUTOESTIMA Y SEGURIDAD")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Autoestima y seguridad", NO_VALOR_RESPUESTA = (ADAPTACION_REP_A7 == null ? "NC" : ADAPTACION_REP_A7), NO_VALOR_BAREMOS = (AP_AUTOESTIMAYSEGURIDAD == null ? "NC" : AP_AUTOESTIMAYSEGURIDAD), DS_FACTOR = "Colores" });

                //    }

                #endregion

                #region Tecnica PC

                //if (vListaResultadoPruebas.Where(item => item.CL_PRUEBA.Equals("TECNICAPC")).ToList().Count > 0)
                //{
                // Tecnica PC
                //Software
                string TECNICAPC_REP_S = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_S")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_S")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string TP_SOFTWARE = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("TP-SOFTWARE")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("TP-SOFTWARE")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Software", NO_VALOR_RESPUESTA = (TECNICAPC_REP_S == null ? "NC" : TECNICAPC_REP_S), NO_VALOR_BAREMOS = (TP_SOFTWARE == null ? "NC" : TP_SOFTWARE), DS_FACTOR = "Técnico" });
                //Internet
                string TECNICAPC_REP_I = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_I")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_I")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string TP_INTERNET = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("TP-INTERNET")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("TP-INTERNET")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Internet", NO_VALOR_RESPUESTA = (TECNICAPC_REP_I == null ? "NC" : TECNICAPC_REP_I), NO_VALOR_BAREMOS = (TP_INTERNET == null ? "NC" : TP_INTERNET), DS_FACTOR = "Técnico" });
                //Hardware
                string TECNICAPC_REP_H = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_H")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_H")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string TP_HARDWARE = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("TP-HARDWARE")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("TP-HARDWARE")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Hardware", NO_VALOR_RESPUESTA = (TECNICAPC_REP_H == null ? "NC" : TECNICAPC_REP_H), NO_VALOR_BAREMOS = (TP_HARDWARE == null ? "NC" : TP_HARDWARE), DS_FACTOR = "Técnico" });
                //Comunicaciones
                string TECNICAPC_REP_C = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_C")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_C")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string TP_COMUNICACIONES = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("TP-COMUNICACIONES")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("TP-COMUNICACIONES")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Comunicaciones", NO_VALOR_RESPUESTA = (TECNICAPC_REP_C == null ? "NC" : TECNICAPC_REP_C), NO_VALOR_BAREMOS = (TP_COMUNICACIONES == null ? "NC" : TP_COMUNICACIONES), DS_FACTOR = "Técnico" });
                //   }

                #endregion

                #region TIVA

                //if (vListaResultadoPruebas.Where(item => item.CL_PRUEBA.Equals("TIVA")).ToList().Count > 0)
                //{
                // TIVA
                //Personal
                string TIVA_REP_INDICE_IP = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("TIVA-REP_INDICE_IP")) ? Math.Round((decimal)(vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("TIVA-REP_INDICE_IP")).FirstOrDefault().NO_VALOR), 2).ToString("N2") : null;
                string TV_PERSONAL = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("TV-PERSONAL")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("TV-PERSONAL")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Personal", NO_VALOR_RESPUESTA = (TIVA_REP_INDICE_IP == null ? "NC" : TIVA_REP_INDICE_IP.ToString()), NO_VALOR_BAREMOS = (TV_PERSONAL == null ? "NC" : TV_PERSONAL.ToString()), DS_FACTOR = "TIVA" });
                //Leyes y reglamentos
                string TIVA_REP_INDICE_ALR = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("TIVA-REP_INDICE_ALR")) ? Math.Round((decimal)(vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("TIVA-REP_INDICE_ALR")).FirstOrDefault().NO_VALOR), 2).ToString("N2") : null;
                string TV_LEYESYREGLAMENTOS = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("TV-LEYES Y REGLAMENTOS")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("TV-LEYES Y REGLAMENTOS")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Leyes y reglamentos", NO_VALOR_RESPUESTA = (TIVA_REP_INDICE_ALR == null ? "NC" : TIVA_REP_INDICE_ALR.ToString()), NO_VALOR_BAREMOS = (TV_LEYESYREGLAMENTOS == null ? "NC" : TV_LEYESYREGLAMENTOS.ToString()), DS_FACTOR = "TIVA" });
                //Integridad y ética laboral
                string TIVA_REP_INDICE_IEL = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("TIVA-REP_INDICE_IEL")) ? Math.Round((decimal)(vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("TIVA-REP_INDICE_IEL")).FirstOrDefault().NO_VALOR), 2).ToString("N2") : null;
                string TV_INTEGRIDADYETICALABORAL = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("TV-INTEGRIDAD Y ÉTICA LABORAL")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("TV-INTEGRIDAD Y ÉTICA LABORAL")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Integridad y ética laboral", NO_VALOR_RESPUESTA = (TIVA_REP_INDICE_IEL == null ? "NC" : TIVA_REP_INDICE_IEL.ToString()), NO_VALOR_BAREMOS = (TV_INTEGRIDADYETICALABORAL == null ? "NC" : TV_INTEGRIDADYETICALABORAL.ToString()), DS_FACTOR = "TIVA" });
                //Cívica
                string TIVA_REP_INDICE_IC = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("TIVA-REP_INDICE_IC")) ? Math.Round((decimal)(vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("TIVA-REP_INDICE_IC")).FirstOrDefault().NO_VALOR), 2).ToString("N2") : null;
                string TV_CIVICA = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("TV-CÍVICA")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("TV-CÍVICA")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Cívica", NO_VALOR_RESPUESTA = (TIVA_REP_INDICE_IC == null ? "NC" : TIVA_REP_INDICE_IC.ToString()), NO_VALOR_BAREMOS = (TV_CIVICA == null ? "NC" : TV_CIVICA.ToString()), DS_FACTOR = "TIVA" });
                //Total
                string TIVA_REP_INDICE_GI = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("TIVA-REP_INDICE_GI")) ? Math.Round((decimal)(vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("TIVA-REP_INDICE_GI")).FirstOrDefault().NO_VALOR), 2).ToString("N2") : null;
                string TV_TOTAL = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("TV-TOTAL")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("TV-TOTAL")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Total", NO_VALOR_RESPUESTA = (TIVA_REP_INDICE_GI == null ? "NC" : TIVA_REP_INDICE_GI.ToString()), NO_VALOR_BAREMOS = (TV_TOTAL == null ? "NC" : TV_TOTAL.ToString()), DS_FACTOR = "TIVA" });
                //   }

                #endregion

                #region Ingles

                //if (vListaResultadoPruebas.Where(item => item.CL_PRUEBA.Equals("INGLES")).ToList().Count > 0)
                //{
                //Ingles
                string INGLES_TOTAL = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("INGLES_TOTAL")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("INGLES_TOTAL")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Inglés", NO_VALOR_RESPUESTA = (INGLES_TOTAL == null ? "NA" : INGLES_TOTAL), NO_VALOR_BAREMOS = "NA", DS_FACTOR = "Inglés" });
                //  }

                #endregion


                #region Entrevista


                string ENTREVISTA_RES_0003 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("ENTREVISTA_RES_0003")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("ENTREVISTA_RES_0003")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string SEGURIDAD = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("ENT-SEGURIDAD")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("ENT-SEGURIDAD")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Seguridad en si mismo", NO_VALOR_RESPUESTA = (ENTREVISTA_RES_0003 == null ? "NC" : ENTREVISTA_RES_0003), NO_VALOR_BAREMOS = (SEGURIDAD == null ? "NC" : SEGURIDAD), DS_FACTOR = "Entrevista" });

                string ENTREVISTA_RES_0004 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("ENTREVISTA_RES_0004")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("ENTREVISTA_RES_0004")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string ENFOQUE = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("ENT-ENFOQUE")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("ENT-ENFOQUE")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Enfoque a resultados", NO_VALOR_RESPUESTA = (ENTREVISTA_RES_0004 == null ? "NC" : ENTREVISTA_RES_0004), NO_VALOR_BAREMOS = (ENFOQUE == null ? "NC" : ENFOQUE), DS_FACTOR = "Entrevista" });

                string ENTREVISTA_RES_0005 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("ENTREVISTA_RES_0005")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("ENTREVISTA_RES_0005")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string CONFLICTO = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("ENT-CONFLICTO")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("ENT-CONFLICTO")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Manejo del conflicto", NO_VALOR_RESPUESTA = (ENTREVISTA_RES_0005 == null ? "NC" : ENTREVISTA_RES_0005), NO_VALOR_BAREMOS = (CONFLICTO == null ? "NC" : CONFLICTO), DS_FACTOR = "Entrevista" });

                string ENTREVISTA_RES_0006 = vListaResultadoPruebas.Exists(x => x.CL_VARIABLE.Equals("ENTREVISTA_RES_0006")) ? vListaResultadoPruebas.Where(x => x.CL_VARIABLE.Equals("ENTREVISTA_RES_0006")).FirstOrDefault().NO_VALOR.ToString("0.00") : null;
                string CARISMA = vListaResultadosBaremos.Exists(x => x.CL_VARIABLE.Equals("ENT-CARISMA")) ? vListaResultadosBaremos.Where(x => x.CL_VARIABLE.Equals("ENT-CARISMA")).FirstOrDefault().NO_VALOR.ToString("0") : null;
                vListaBaremos.Add(new E_REPORTE_RESULTADO_BAREMOS { NB_FACTOR = "Carisma", NO_VALOR_RESPUESTA = (ENTREVISTA_RES_0006 == null ? "NC" : ENTREVISTA_RES_0006), NO_VALOR_BAREMOS = (CARISMA == null ? "NC" : CARISMA), DS_FACTOR = "Entrevista" });

                #endregion


                PruebasNegocio nSolicitud = new PruebasNegocio();
                SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result candidato = new SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result();
                candidato = nSolicitud.ObtenienePruebasResultadosCandidatos(vIdBateria);
                txtNbCandidato.InnerText = candidato.NB_CANDIDATO;
                txtFolio.InnerText = candidato.CL_SOLICITUD;
            }
        }

        protected void grdResultadosPruebas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ResultadosPruebasNegocio nResultadosPruebas = new ResultadosPruebasNegocio();
            //  List<E_FACTORES_PRUEBAS> vlstFactoresPruebas = new List<E_FACTORES_PRUEBAS>();
            List<SPE_OBTIENE_FACTORES_PRUEBAS_Result> vLstFactores = new List<SPE_OBTIENE_FACTORES_PRUEBAS_Result>();
            vLstFactores = nResultadosPruebas.ObtieneFactoresPruebas(vIdBateria, null);
            //if (vListaResultadoPruebas.Where(item => item.CL_PRUEBA.Equals("INGLES")).ToList().Count > 0)
            //{
            //    vLstFactores.Add(new SPE_OBTIENE_FACTORES_PRUEBAS_Result { DS_FACTOR = "Inglés", ID_VARIABLE = 0 });
            //}
            grdResultadosPruebas.DataSource = vLstFactores;

        }

        protected void grdResultadosPruebas_DetailTableDataBind(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            vListaDetailTable = new List<E_REPORTE_RESULTADO_BAREMOS>();
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            string vDsFactor = dataItem.GetDataKeyValue("DS_FACTOR").ToString();
            if (vDsFactor == "Técnica PC")
                vDsFactor = "Técnico";
            if (vDsFactor == "Adaptación al medio")
                vDsFactor = "Colores";

            foreach (E_REPORTE_RESULTADO_BAREMOS item in vListaBaremos)
            {
                if (item.DS_FACTOR == vDsFactor)
                {
                    vListaDetailTable.Add(new E_REPORTE_RESULTADO_BAREMOS
                        {
                            NB_FACTOR = item.NB_FACTOR,
                            NO_VALOR_RESPUESTA = item.NO_VALOR_RESPUESTA,
                            NO_VALOR_BAREMOS = item.NO_VALOR_BAREMOS
                        });
                }
            }
            e.DetailTableView.DataSource = vListaDetailTable;
        }

        protected void grdResultadosPruebas_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.ExpandCollapseCommandName)
            {
                foreach (GridItem item in e.Item.OwnerTableView.Items)
                {
                    if (item.Expanded && item != e.Item)
                    {
                        item.Expanded = false;
                    }
                }
            }
        }
    }
}