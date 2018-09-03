using SIGE.Entidades;
using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.IntegracionDePersonal;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.IDP
{
    public partial class VentanaAnalisisCompetencias : System.Web.UI.Page
    {
        #region Variables

        private int vIdCandidato
        {
            get { return (int)ViewState["vs_vac_id_candidato"]; }
            set { ViewState["vs_vac_id_candidato"] = value; }
        }



        private int vIdRequisicion
        {
            get { return (int)ViewState["vs_vac_id_requisicion"]; }
            set { ViewState["vs_vac_id_requisicion"] = value; }
        }

        public int vIdPuesto
        {
            get { return (int)ViewState["vs_vac_id_puesto"]; }
            set { ViewState["vs_vac_id_puesto"] = value; }
        }

        private int vIdBateria
        {
            get { return (int)ViewState["vs_vac_id_bateria"]; }
            set { ViewState["vs_vac_id_bateria"] = value; }
        }

        private int vNoElementosCompatibles
        {
            get { return (int)ViewState["vs_vac_no_elementos_compatibles"]; }
            set { ViewState["vs_vac_no_elementos_compatibles"] = value; }
        }

        private int vNoTotalElementos
        {
            get { return (int)ViewState["vs_vac_no_total_elementos"]; }
            set { ViewState["vs_vac_no_total_elementos"] = value; }
        }

        private decimal vPrCompatibilidad
        {
            get { return (decimal)ViewState["vs_vac_pr_compatibilidad"]; }
            set { ViewState["vs_vac_pr_compatibilidad"] = value; }
        }

        public Guid vIdBusquedaCandidato
        {
            get { return (Guid)ViewState["vs_vIdBusquedaCandidato"]; }
            set { ViewState["vs_vIdBusquedaCandidato"] = value; }
        }

        public bool vFgConsultaParcial
        {
            get { return (bool)ViewState["vs_vFgConsultaParcial"]; }
            set { ViewState["vs_vFgConsultaParcial"] = value; }
        }

        public bool vFgCalificacionCero
        {
            get { return (bool)ViewState["vs_vFgCalificacionCero"]; }
            set { ViewState["vs_vFgCalificacionCero"] = value; }
        }

        public List<E_GRAFICA_PUESTO_CANDIDATOS> vPuestoCandidatos
        {
            get { return (List<E_GRAFICA_PUESTO_CANDIDATOS>)ViewState["vs_vPuestoCandidatos"]; }
            set { ViewState["vs_vPuestoCandidatos"] = value; }
        }

        #endregion

        #region Funciones

        private string GenerarXml()
        {
            XElement vParametrosBusqueda = new XElement("PARAMETROS");
            XElement vParametrosPerfil = new XElement("PARAMETROS_PERFIL");

            RequisicionNegocio nRequisicion = new RequisicionNegocio();

            //vParametrosBusqueda.Add(new XAttribute("FE_INICIAL", dtpInicial.SelectedDate.Value.ToString("MM/dd/yyyy")));
            //vParametrosBusqueda.Add(new XAttribute("FE_FINAL", dtpFinal.SelectedDate.Value.ToString("MM/dd/yyyy")));

            bool vFgBusquedaPerfi = ContextoBusquedaCandidato.oPuestoVsCandidatos.Where(t => t.vIdParametroBusqueda == vIdBusquedaCandidato).FirstOrDefault().vFgBusquedaPerfil;
            bool vFgEdad = ContextoBusquedaCandidato.oPuestoVsCandidatos.Where(t => t.vIdParametroBusqueda == vIdBusquedaCandidato).FirstOrDefault().vFgEdad;
            bool vFgGenero = ContextoBusquedaCandidato.oPuestoVsCandidatos.Where(t => t.vIdParametroBusqueda == vIdBusquedaCandidato).FirstOrDefault().vFgGenero;
            bool vFgEdoCivil = ContextoBusquedaCandidato.oPuestoVsCandidatos.Where(t => t.vIdParametroBusqueda == vIdBusquedaCandidato).FirstOrDefault().vFgEdoCivil;
            bool vFgEscolaridad = ContextoBusquedaCandidato.oPuestoVsCandidatos.Where(t => t.vIdParametroBusqueda == vIdBusquedaCandidato).FirstOrDefault().vFgEscolaridad;
            bool vFgComEsp = ContextoBusquedaCandidato.oPuestoVsCandidatos.Where(t => t.vIdParametroBusqueda == vIdBusquedaCandidato).FirstOrDefault().vFgComEsp;
            bool vFgAreaInteres = ContextoBusquedaCandidato.oPuestoVsCandidatos.Where(t => t.vIdParametroBusqueda == vIdBusquedaCandidato).FirstOrDefault().vFgAreaInteres;

            if (vFgBusquedaPerfi)
            {
                vParametrosBusqueda.Add(new XAttribute("FG_PERFIL", "1"));
                vParametrosPerfil.Add(new XAttribute("FG_EDAD", vFgEdad ? "1" : "0"));
                vNoTotalElementos = vNoTotalElementos + (vFgEdad ? 1 : 0);

                vParametrosPerfil.Add(new XAttribute("FG_GENERO", vFgGenero ? "1" : "0"));
                vNoTotalElementos = vNoTotalElementos + (vFgGenero ? 1 : 0);

                vParametrosPerfil.Add(new XAttribute("FG_EDO_CIVIL", vFgEdoCivil ? "1" : "0"));
                vNoTotalElementos = vNoTotalElementos + (vFgEdoCivil ? 1 : 0);

                vParametrosPerfil.Add(new XAttribute("FG_ESCOLARIDAD", vFgEscolaridad ? "1" : "0"));
                vNoTotalElementos = vNoTotalElementos + (vFgEscolaridad ? 1 : 0);

                vParametrosPerfil.Add(new XAttribute("FG_COMP_ESP", vFgComEsp ? "1" : "0"));
                vNoTotalElementos = vNoTotalElementos + (vFgComEsp ? 1 : 0);

                vParametrosPerfil.Add(new XAttribute("FG_AREA_INTERES", vFgAreaInteres ? "1" : "0"));
                vNoTotalElementos = vNoTotalElementos + (vFgAreaInteres ? 1 : 0);

                vParametrosBusqueda.Add(vParametrosPerfil);

            }

            return vParametrosBusqueda.ToString();
        }

        private void CargarDatosPerfil()
        {
            RequisicionNegocio nRequisicion = new RequisicionNegocio();
            var vComparacion = nRequisicion.ObtenerComparacionCandidatoPuesto(vIdRequisicion, vIdCandidato, GenerarXml()).FirstOrDefault();

            if (vComparacion != null)
            {
                //edad
                txtEdadPerfil.InnerText = vComparacion.NO_EDAD_MINIMA.ToString() + " a " + vComparacion.NO_EDAD_MAXIMA.ToString();
                txtEdadCandidato.InnerText = vComparacion.NO_EDAD_CANDIDATO.ToString();

                //Comparación de edad
                if (vComparacion.FG_EDAD.Value == 1)
                {
                    divComparacionEdad.Attributes.Add("class", "CompatibilidadAlta");
                    vNoElementosCompatibles++;
                }
                else if (vComparacion.FG_EDAD.Value == 0)
                {
                    divComparacionEdad.Attributes.Add("class", "CompatibilidadBaja");
                    //vNoElementosCompatibles++;
                }
                else
                {
                    divComparacionEdad.InnerText = "N/A";
                }


                //genero
                txtGeneroPerfil.InnerText = vComparacion.CL_GENERO_PUESTO;
                txtGeneroCandidato.InnerText = vComparacion.CL_GENERO_CANDIDATO;

                ////Comparación de genero
                if (vComparacion.FG_GENERO == 1)
                {
                    divComparacionGenero.Attributes.Add("class", "CompatibilidadAlta");
                    vNoElementosCompatibles++;
                }
                else if (vComparacion.FG_GENERO == 0)
                {
                    divComparacionGenero.Attributes.Add("class", "CompatibilidadBaja");
                    //vNoElementosCompatibles++;
                }
                else
                {
                    //divComparacionGenero.Attributes.Add("class", "CompatibilidadBaja");
                    divComparacionGenero.InnerText = "N/A";
                }

                //estado civil
                txtEdoCivilPerfil.InnerText = vComparacion.CL_ESTADO_CIVIL_PUESTO;
                txtEdoCivilCandidato.InnerText = vComparacion.CL_ESTADO_CIVIL_CANDIDATO;

                if (vComparacion.FG_EDO_CIVIL == 1)
                {
                    divComparacionEdoCivil.Attributes.Add("class", "CompatibilidadAlta");
                    vNoElementosCompatibles++;
                }
                else if (vComparacion.FG_EDO_CIVIL == 0)
                {
                    divComparacionEdoCivil.Attributes.Add("class", "CompatibilidadBaja");
                    //vNoElementosCompatibles++;
                }
                else
                {
                    //divComparacionEdoCivil.Attributes.Add("class", "CompatibilidadBaja");
                    divComparacionEdoCivil.InnerText = "N/A";
                }

                // escolaridades
                txtEscolaridadesPerfil.InnerHtml = vComparacion.DS_ESCOLARIDADES_PUESTO;
                txtEscolaridadesCandidato.InnerHtml = vComparacion.DS_ESCOLARIDADES_CANDIDATO;

                if (vComparacion.FG_ESCOLARIDADES == 1)
                {
                    divComparacionEscolaridades.Attributes.Add("class", "CompatibilidadAlta");
                    vNoElementosCompatibles++;
                }
                else if (vComparacion.FG_ESCOLARIDADES == 0)
                {
                    divComparacionEscolaridades.Attributes.Add("class", "CompatibilidadBaja");
                    //vNoElementosCompatibles++;
                }
                else
                {
                    divComparacionEscolaridades.InnerText = "N/A";
                    //divComparacionPostgrado.Attributes.Add("class", "CompatibilidadBaja");
                }

                //Competencias especificas
                txtComEspPerfil.InnerHtml = vComparacion.DS_COMP_ESP_PUESTO;
                txtCompEspCandidato.InnerHtml = vComparacion.DS_COMP_ESP_CANDIDATO;

                if (vComparacion.FG_COMP_ESP == 1)
                {
                    divComparacionCompEsp.Attributes.Add("class", "CompatibilidadAlta");
                    vNoElementosCompatibles++;
                }
                else if (vComparacion.FG_COMP_ESP == 0)
                {
                    divComparacionCompEsp.Attributes.Add("class", "CompatibilidadBaja");
                    //vNoElementosCompatibles++;
                }
                else
                {
                    //divComparacionProfesional.Attributes.Add("class", "CompatibilidadBaja");
                    divComparacionCompEsp.InnerText = "N/A";
                }


                //areas de interes
                txtAreaInteresPerfil.InnerHtml = vComparacion.DS_AREA_INTERES_PUESTO;
                txtAreaInteresCandidato.InnerHtml = vComparacion.DS_AREA_INTERES_CANDIDATO;

                if (vComparacion.FG_AREA_INTERES == 1)
                {
                    divComparacionAreaInteres.Attributes.Add("class", "CompatibilidadAlta");
                    vNoElementosCompatibles++;
                }
                else if (vComparacion.FG_AREA_INTERES == 0)
                {
                    divComparacionAreaInteres.Attributes.Add("class", "CompatibilidadBaja");
                    //vNoElementosCompatibles++;
                }
                else
                {
                    //divComparacionTecnica.Attributes.Add("class", "CompatibilidadBaja");
                    divComparacionAreaInteres.InnerText = "N/A";
                }

                if (!vNoTotalElementos.Equals(0))
                {
                    vPrCompatibilidad = ((decimal)vNoElementosCompatibles / (decimal)vNoTotalElementos) * 100;    
                }
                else
                {
                    vPrCompatibilidad = 0;
                }
                
                txtNoElementosCompatibles.InnerText = vPrCompatibilidad.ToString("N2") + "%";
            }

        }

        private void CargarDatos()
        {
            CandidatoNegocio nCandidato = new CandidatoNegocio();

            var vCandidato = nCandidato.ObtieneCandidato(pIdCandidato: vIdCandidato).FirstOrDefault();

            if (vCandidato != null)
            {
                //txtCandidato.InnerText = vCandidato.NB_CANDIDATO_COMPLETO;
                txtNombreCandidato.InnerText = vCandidato.NB_CANDIDATO_COMPLETO;
                //txtEdadCandidato.InnerText = vCandidato.NO_EDAD.ToString();
                //txtEdoCivilCandidato.InnerText = vCandidato.CL_ESTADO_CIVIL;
                //txtGeneroCandidato.InnerText = vCandidato.CL_GENERO;
                //txtPostgradoCandidato.InnerHtml = vCandidato.DS_POSTGRADOS;
                //txtProfesionalCandidato.InnerHtml = vCandidato.DS_PROFESIONAL;
                //txtTecnicaCandidato.InnerHtml = vCandidato.DS_TECNICA;
            }

            var vPuestoReq = nCandidato.ObtienePuestoRequisicion(vIdRequisicion);

            if (vPuestoReq != null)
            {
                //txtPuesto.InnerText = vPuestoReq.NB_PUESTO;
                vIdPuesto = vPuestoReq.ID_PUESTO;
                txtNombrePuesto.InnerText = vPuestoReq.NB_PUESTO;
                //txtEdadPerfil.InnerText = vPuestoReq.NO_EDAD_MINIMA.ToString() + " a " + vPuestoReq.NO_EDAD_MAXIMA.ToString();
                //txtEdoCivilPerfil.InnerText = vPuestoReq.CL_ESTADO_CIVIL;

                //txtGeneroPerfil.InnerText = string.IsNullOrEmpty(vPuestoReq.CL_GENERO) ? "Indistinto" : vPuestoReq.CL_GENERO;
                //txtPostgradoPerfil.InnerHtml = vPuestoReq.DS_POSTGRADO;
                //txtProfesionalPerfil.InnerHtml = vPuestoReq.DS_PROFESIONAL;
                //txtTecnicaPerfil.InnerHtml = vPuestoReq.DS_TECNICA;
            }





            //CompararDatos(vCandidato, vPuestoReq);
        }

        private void ConfigurarPagina()
        {

            bool vFgBusquedaPerfi = ContextoBusquedaCandidato.oPuestoVsCandidatos.Where(t => t.vIdParametroBusqueda == vIdBusquedaCandidato).FirstOrDefault().vFgBusquedaPerfil;
            bool vFgBusquedaCompetencias = ContextoBusquedaCandidato.oPuestoVsCandidatos.Where(t => t.vIdParametroBusqueda == vIdBusquedaCandidato).FirstOrDefault().vFgBusquedaCompetencias;

            rtsAnalisis.Tabs[0].Visible = vFgBusquedaPerfi;
            rtsAnalisis.Tabs[1].Visible = vFgBusquedaCompetencias;
            rtsAnalisis.Tabs[2].Visible = vFgBusquedaCompetencias;




            if (vFgBusquedaPerfi)
            {
                CargarDatosPerfil();
            }

            if (vFgBusquedaCompetencias)
            {
                GenerarGrafica();
            }

            if (vFgBusquedaPerfi & !vFgBusquedaCompetencias)
            {
                rmpAnalisis.SelectedIndex = 0;
                rtsAnalisis.Tabs[0].Selected = true;
            }

            if (!vFgBusquedaPerfi & vFgBusquedaCompetencias)
            {
                rmpAnalisis.SelectedIndex = 1;
                rtsAnalisis.Tabs[1].Selected = true;
            }

        }

        private void GenerarGrafica()
        {
            XElement vXelements = new XElement("CANDIDATO", new XAttribute("ID_CANDIDATO", vIdCandidato));

            XElement SELECCIONADOS = new XElement("CANDIDATOS", vXelements);

            ConsultasComparativasNegocio nComparativas = new ConsultasComparativasNegocio();
            vPuestoCandidatos = nComparativas.ObtienePuestoCandidatos(pID_PUESTO: vIdPuesto, pXML_CANDIDATOS: SELECCIONADOS.ToString(), pFgConsultaParcial: vFgConsultaParcial, pFgCalificacionCero:vFgCalificacionCero).Select(s => new E_GRAFICA_PUESTO_CANDIDATOS
            {
                NB_COMPETENCIA = s.NB_COMPETENCIA,
                CL_PUESTO = s.CL_PUESTO,
                CL_SOLICITUD = s.CL_SOLICITUD,
                NO_VALOR_NIVEL = s.NO_VALOR_NIVEL,
                NB_CANDIDATO = s.NB_CANDIDATO,
                NO_VALOR_CANDIDATO = s.NO_VALOR_CANDIDATO,
                ID_CANDIDATO = s.ID_CANDIDATO,
                DS_COMPETENCIA = s.DS_COMPETENCIA,
                ID_BATERIA = s.ID_BATERIA,
                NB_PUESTO = s.NB_PUESTO,
                PR_CANDIDATO = s.PR_CANDIDATO,
                CL_COLOR = s.CL_COLOR,
                PR_TRUNCADO = CalculaPorcentaje(s.PR_CANDIDATO)
            }).ToList();


            GraficaPuestoCandidatos(vPuestoCandidatos);

            List<E_PROMEDIO> vlstPromedios = new List<E_PROMEDIO>();

            List<E_PROMEDIO> vlist = new List<E_PROMEDIO>();
            foreach (var i in vPuestoCandidatos)
            {
                if (i.ID_CANDIDATO == vIdCandidato && i.NO_VALOR_NIVEL != 0)
                {
                    vlist.Add(new E_PROMEDIO { NB_PUESTO = i.NB_CANDIDATO, PORCENTAJE = i.PR_TRUNCADO });
                }
            }

            vlstPromedios.Add(new E_PROMEDIO { 
                  NB_PUESTO = vlist.Select(s => s.NB_PUESTO).FirstOrDefault(),
                                          PROMEDIO = vlist.Average(s => s.PORCENTAJE),
                                          FG_SUPERA_130 = vlist.Average(s => s.PORCENTAJE_NO_TRUNCADO) >= 130 ? true : false,
                                          PROMEDIO_TXT = vlist.Average(s => s.PORCENTAJE_NO_TRUNCADO) >= 130 ? Decimal.Round(vlist.Average(s => s.PORCENTAJE) ?? 1 ,2).ToString() + "(*)" : Decimal.Round(vlist.Average(s => s.PORCENTAJE) ?? 1, 2).ToString()
                //NB_PUESTO = vlist.Select(s => s.NB_PUESTO).FirstOrDefault(), PROMEDIO = vlist.Average(s => s.PORCENTAJE) 
            });

            for (int i = 0; i < vlstPromedios.Count; i++)
            {
                if (vlstPromedios[i].FG_SUPERA_130 == true)
                {
                    divMensajeMayor130.Visible = true;
                    i = vlstPromedios.Count;
                }
            }

            rgdPromedios.DataSource = vlstPromedios;
            rgdPromedios.DataBind();
            rgdPromedios.Rebind();
        }

        protected decimal? CalculaPorcentaje(decimal? pPorcentaje)
        {
            decimal? vPorcentaje = 0;
            if (pPorcentaje > 100)
                vPorcentaje = 100;
            else vPorcentaje = pPorcentaje;
            return vPorcentaje;
        }

        protected void GraficaPuestoCandidatos(List<E_GRAFICA_PUESTO_CANDIDATOS> plstPuestoCandidatos)
        {

            rhcPuestoCandidatos.PlotArea.Series.Clear();


      
            var vPuestoCompetencia = plstPuestoCandidatos.Select(s => new { s.NO_VALOR_NIVEL, s.NB_PUESTO, s.CL_PUESTO, s.NB_COMPETENCIA }).Distinct().ToList();
            ColumnSeries vPuesto = new ColumnSeries();

            foreach (var item in vPuestoCompetencia)
            {
                vPuesto.SeriesItems.Add(new CategorySeriesItem(item.NO_VALOR_NIVEL));
                vPuesto.LabelsAppearance.Visible = false;
                vPuesto.Name = "("+item.CL_PUESTO+") " + item.NB_PUESTO;
                rhcPuestoCandidatos.PlotArea.XAxis.Items.Add(item.NB_COMPETENCIA);
                rhcPuestoCandidatos.PlotArea.XAxis.LabelsAppearance.RotationAngle = 270;
            }
            rhcPuestoCandidatos.PlotArea.Series.Add(vPuesto);


            ColumnSeries vCandidatos = new ColumnSeries();

            foreach (var i in plstPuestoCandidatos)
            {
                if (i.ID_CANDIDATO == vIdCandidato)
                {
                    vCandidatos.SeriesItems.Add(new CategorySeriesItem(i.NO_VALOR_CANDIDATO));
                    vCandidatos.LabelsAppearance.Visible = false;
                    vCandidatos.Name = "("+i.CL_SOLICITUD+") " + i.NB_CANDIDATO;
                }
            }

            rhcPuestoCandidatos.PlotArea.Series.Add(vCandidatos);




        }



        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                vNoElementosCompatibles = 0;
                vNoTotalElementos = 0;

                if (Request.Params["IdCandidato"] != null)
                {
                    vIdCandidato = int.Parse(Request.Params["IdCandidato"].ToString());
                }

                if (Request.Params["IdRequisicion"] != null)
                {
                    vIdRequisicion = int.Parse(Request.Params["IdRequisicion"].ToString());
                }

                if (Request.Params["IdBusqueda"] != null)
                {
                    vIdBusquedaCandidato = Guid.Parse(Request.Params["IdBusqueda"].ToString());
                }

                vFgConsultaParcial = ContextoApp.IDP.ConfiguracionIntegracion.FgConsultasParciales;
                vFgCalificacionCero = ContextoApp.IDP.ConfiguracionIntegracion.FgIgnorarCompetencias;

                //if (Request.Params["IdBateria"] != null)
                //{
                //    vIdBateria = int.Parse(Request.Params["IdBateria"].ToString());
                //}

                vIdBateria = 0;
                CargarDatos();
                ConfigurarPagina();

            }
        }

        protected void rgCompetencias_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            CandidatoNegocio nCandidato = new CandidatoNegocio();
            var vLstCompetencias = nCandidato.ObtieneAnalisisCompetencias(vIdRequisicion, vIdCandidato, vIdBateria);
            rgCompetencias.DataSource = vLstCompetencias;
        }

    }
}