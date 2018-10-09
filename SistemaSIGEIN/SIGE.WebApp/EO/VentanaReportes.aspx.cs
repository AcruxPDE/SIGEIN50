using SIGE.Entidades.MetodologiaCompensacion;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Negocio.EvaluacionOrganizacional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using SIGE.Entidades.Externas;
using SIGE.WebApp.Comunes;
using System.Drawing;
using System.Data;
using Newtonsoft.Json;
using System.Xml.Linq;
using WebApp.Comunes;
using System.Xml;

namespace SIGE.WebApp.EO
{
    public partial class VentanaReportes : System.Web.UI.Page
    {
        #region Variables

        public int vIdPeriodo
        {
            get { return (int)ViewState["vs_vIdPeriodo"]; }
            set { ViewState["vs_vIdPeriodo"] = value; }
        }

        private string vNbFirstRadEditorTagName = "p";

        public List<E_SELECCIONADOS> vFiltros
        {
            get { return (List<E_SELECCIONADOS>)ViewState["vs_vFiltros"]; }
            set { ViewState["vs_vFiltros"] = value; }
        }

        public int vIdGenero
        {
            get { return (int)ViewState["vs_vIdGenero"]; }
            set { ViewState["vs_vIdGenero"] = value; }
        }

        public decimal? vPorcentajePeriodo
        {
            get { return (decimal?)ViewState["vs_vPorcentajePeriodo"]; }
            set { ViewState["vs_vPorcentajePeriodo"] = value; }
        }

        public string vXmlFiltrosPre
        {
            get { return (string)ViewState["vs_vXmlFiltros"]; }
            set { ViewState["vs_vXmlFiltros"] = value; }
        }

        public int? vIdRol;

        #endregion

        #region Funciones

        private static Boolean ValidarRamaXml(XElement parentEl, string elementsName)
        {
            var foundEl = parentEl.Element(elementsName);
            if (foundEl != null)
            {
                return true;
            }

            return false;
        }

        protected string ObtieneDepartamentos(string pXmlDepartamentos)
        {
            string vDepartamentos = "";
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(pXmlDepartamentos);
            XmlNodeList departamentos = xml.GetElementsByTagName("ITEMS");

            XmlNodeList lista =
            ((XmlElement)departamentos[0]).GetElementsByTagName("ITEM");

            foreach (XmlElement nodo in lista)
            {

                vDepartamentos = vDepartamentos + nodo.GetAttribute("NB_DEPARTAMENTO") + ".\n";

            }


            return vDepartamentos;
        }

        protected string ObtieneAdicionales(string pXmlAdicionales)
        {
            string vAdicionales = "";
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(pXmlAdicionales);
            XmlNodeList departamentos = xml.GetElementsByTagName("ITEMS");

            XmlNodeList lista =
            ((XmlElement)departamentos[0]).GetElementsByTagName("ITEM");

            foreach (XmlElement nodo in lista)
            {

                vAdicionales = vAdicionales + nodo.GetAttribute("NB_CAMPO") + ".\n";

            }


            return vAdicionales;
        }

        protected void MostrarPromedioIndice()
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            List<E_GRAFICAS> vGraficaDimension = nClima.ObtieneGraficaDimension(pID_PERIODO: vIdPeriodo).Select(s => new E_GRAFICAS { PORCENTAJE = s.PR_DIMENSION }).ToList();
            int vCantidadDimensiones = vGraficaDimension.Count;
            if (vCantidadDimensiones > 0)
            {
                decimal? vSumaPorcentaje = vGraficaDimension.Sum(item => item.PORCENTAJE);
                decimal? vPromedioPeriodo = ((vSumaPorcentaje * 100) / (vCantidadDimensiones * 100));
                string vColor = ColoresPoncentajes(vPromedioPeriodo);
                dvColorProm.Style.Add("background-color", vColor);
                if (vColor == "green")
                    lbTotal.InnerText = "Resultado global satisfactorio";
                if (vColor == "yellow")
                    lbTotal.InnerText = "Resultado global medianamente satisfactorio";
                if (vColor == "red")
                    lbTotal.InnerText = "Resultado global poco satisfactorio";
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;

            if (!IsPostBack)
            {
                if (Request.Params["PeriodoID"] != null)
                {
                    vIdPeriodo = int.Parse(Request.Params["PeriodoID"]);
                    ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
                    var vPeriodoClima = nClima.ObtienePeriodosClima(pIdPerido: vIdPeriodo).FirstOrDefault();
                    txtClPeriodo.InnerText = vPeriodoClima.CL_PERIODO;
                    txtDsPeriodo.InnerText = vPeriodoClima.DS_PERIODO;
                    txtEstatus.InnerText = vPeriodoClima.CL_ESTADO_PERIODO;
                    if (vPeriodoClima.CL_TIPO_CONFIGURACION == "PARAMETROS")
                        txtTipo.InnerText = "Sin asignación de evaluadores";
                    else
                        txtTipo.InnerText = "Evaluadores asignados";

                    if (vPeriodoClima.DS_NOTAS != null)
                    {
                        if (vPeriodoClima.DS_NOTAS.Contains("DS_NOTA"))
                        {
                            txtNotas.InnerHtml = Utileria.MostrarNotas(vPeriodoClima.DS_NOTAS);
                        }
                        else
                        {
                            XElement vRequerimientos = XElement.Parse(vPeriodoClima.DS_NOTAS);
                            if (vRequerimientos != null)
                            {
                                vRequerimientos.Name = vNbFirstRadEditorTagName;
                                txtNotas.InnerHtml = vRequerimientos.ToString();
                            }
                        }
                    }
                    if (vPeriodoClima.CL_ORIGEN_CUESTIONARIO == "PREDEFINIDO")
                        lbCuestionario.InnerText = "Predefinido de SIGEIN";
                    if (vPeriodoClima.CL_ORIGEN_CUESTIONARIO == "COPIA")
                        lbCuestionario.InnerText = "Copia de otro periodo";
                    if (vPeriodoClima.CL_ORIGEN_CUESTIONARIO == "VACIO")
                        lbCuestionario.InnerText = "Creado en blanco";


                    MostrarPromedioIndice();

                    if (Request.Params["ClDestino"] != null)
                    {
                        string vClDestino = Request.Params["ClDestino"].ToString();

                        if (vClDestino == "INDICE")
                        {
                            tbReportes.Tabs[1].Selected = true;
                            mpgReportes.PageViews[1].Selected = true;
                            divContexto.Style.Add("display", "none");
                            divIndice.Style.Add("display", "block");
                            divDistribucion.Style.Add("display", "none");
                            divPreguntas.Style.Add("display", "none");
                            divGlobal.Style.Add("display", "none");
                        }
                        else if (vClDestino == "DISTRIBUCION")
                        {
                            tbReportes.Tabs[2].Selected = true;
                            mpgReportes.PageViews[2].Selected = true;
                            divContexto.Style.Add("display", "none");
                            divIndice.Style.Add("display", "none");
                            divDistribucion.Style.Add("display", "block");
                            divPreguntas.Style.Add("display", "none");
                            divGlobal.Style.Add("display", "none");
                        }
                        else if (vClDestino == "PREGUNTAS")
                        {
                            tbReportes.Tabs[3].Selected = true;
                            mpgReportes.PageViews[3].Selected = true;
                            divContexto.Style.Add("display", "none");
                            divIndice.Style.Add("display", "none");
                            divDistribucion.Style.Add("display", "none");
                            divPreguntas.Style.Add("display", "block");
                            divGlobal.Style.Add("display", "none");
                        }
                        else if (vClDestino == "GENERAL")
                        {
                            tbReportes.Tabs[4].Selected = true;
                            mpgReportes.PageViews[4].Selected = true;
                            divContexto.Style.Add("display", "none");
                            divIndice.Style.Add("display", "none");
                            divDistribucion.Style.Add("display", "none");
                            divPreguntas.Style.Add("display", "none");
                            divGlobal.Style.Add("display", "block");
                        }
                    }
                    //int countFiltros = nClima.ObtenerFiltrosEvaluadores(vIdPeriodo).Count;
                    //if (countFiltros > 0)
                    //{
                    //    var vFiltros = nClima.ObtenerParametrosFiltros(vIdPeriodo).FirstOrDefault();
                    //    if (vFiltros != null)
                    //    {
                    //        // LbFiltros.Visible = true;
                    //        if (vFiltros.EDAD_INICIO != null)
                    //        {
                    //            lbedad.Visible = true;
                    //            txtEdad.Visible = true;
                    //            txtEdad.Attributes.Add("class", "ctrlTableDataBorderContext");
                    //            txtEdad.InnerText = vFiltros.EDAD_INICIO + " a " + vFiltros.EDAD_FINAL + " años";
                    //        }
                    //        if (vFiltros.ANTIGUEDAD_INICIO != null)
                    //        {
                    //            lbAntiguedad.Visible = true;
                    //            txtAntiguedad.Visible = true;
                    //            txtAntiguedad.Attributes.Add("class", "ctrlTableDataBorderContext");
                    //            txtAntiguedad.InnerText = vFiltros.ANTIGUEDAD_INICIO + " a " + vFiltros.ANTIGUEDAD_FINAL + " años";
                    //        }
                    //        if (vFiltros.CL_GENERO != null)
                    //        {
                    //            lbGenero.Visible = true;
                    //            txtGenero.Visible = true;
                    //            txtGenero.Attributes.Add("class", "ctrlTableDataBorderContext");
                    //            if (vFiltros.CL_GENERO == "M")
                    //                txtGenero.InnerText = "Masculino";
                    //            else
                    //                txtGenero.InnerText = "Femenino";
                    //        }

                    //        if (vFiltros.XML_DEPARTAMENTOS != null)
                    //        {
                    //            lbDepartamento.Visible = true;
                    //            rlDepartamento.Visible = true;
                    //            rlDepartamento.Attributes.Add("class", "ctrlTableDataBorderContext");
                    //            rlDepartamento.Text = ObtieneDepartamentos(vFiltros.XML_DEPARTAMENTOS);
                    //        }

                    //        if (vFiltros.XML_CAMPOS_ADICIONALES != null)
                    //        {
                    //            lbAdscripciones.Visible = true;
                    //            rlAdicionales.Visible = true;
                    //            rlAdicionales.Attributes.Add("class", "ctrlTableDataBorderContext");
                    //            rlAdicionales.Text = ObtieneAdicionales(vFiltros.XML_CAMPOS_ADICIONALES);
                    //        }

                    //    }
                    //}
                }

                if (cmbIndiceSatisfaccion.SelectedValue != null)
                {
                    MostrarGraficaIndice(int.Parse(cmbIndiceSatisfaccion.SelectedValue), null);
                }

                if (cmbMostradoPor.SelectedValue != null)
                {
                    CargarDatosCombo(int.Parse(cmbMostradoPor.SelectedValue));
                }

                if (cmbMostradoPor.SelectedValue != null)
                {
                    MostrarGraficaDistribucion(int.Parse(cmbMostradoPor.SelectedValue), cmbTemaGraficar.SelectedValue, null);

                }
            }
            GraficaDistribucionGlobal();
        }

        protected void cmbIndiceSatisfaccion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //MostrarGraficaIndice(int.Parse(cmbIndiceSatisfaccion.SelectedValue),null);
            FiltrosIndice();

            rgdGraficasIndice.Rebind();
        }

        protected void MostrarGraficaIndice(int pGraficaMostrar, XElement pFiltros)
        {

            switch (pGraficaMostrar)
            {
                case 1: GraficaDimension(pFiltros);
                    break;
                case 2: GraficaTema(pFiltros);
                    break;
                case 3: GraficaPregunta(pFiltros);
                    break;
            }
        }

        protected void GraficasIndiceSatisfaccion(List<E_GRAFICAS> plstPorcentajes)
        {
            rhtColumnChart.PlotArea.Series.Clear();
            //rhtColumnChart.PlotArea.XAxis.Items.Clear();
            ColumnSeries vCsIndiceSatisfaccion = new ColumnSeries();

            foreach (var item in plstPorcentajes)
            {
                vCsIndiceSatisfaccion = new ColumnSeries();
                string vColorColumna = ColoresPoncentajes(item.PORCENTAJE);
                Color vColor = System.Drawing.ColorTranslator.FromHtml(vColorColumna);
                vCsIndiceSatisfaccion.SeriesItems.Add(item.PORCENTAJE, vColor);
                rhtColumnChart.PlotArea.YAxis.MaxValue = 100;
                rhtColumnChart.PlotArea.YAxis.MinValue = 0;
                rhtColumnChart.PlotArea.YAxis.Step = 10;
                vCsIndiceSatisfaccion.LabelsAppearance.Visible = false;
                vCsIndiceSatisfaccion.LabelsAppearance.DataFormatString = "{0:N1}";
                vCsIndiceSatisfaccion.LabelsAppearance.RotationAngle = 270;
                vCsIndiceSatisfaccion.TooltipsAppearance.ClientTemplate = item.NOMBRE;
                vCsIndiceSatisfaccion.TooltipsAppearance.Color = Color.Black;
                //rhtColumnChart.PlotArea.XAxis.Items.Add(item.NO_NOMBRE.ToString());
                rhtColumnChart.PlotArea.Series.Add(vCsIndiceSatisfaccion);
            }

            
            rgdGraficasIndice.DataSource = plstPorcentajes;
            rgdGraficasIndice.DataBind();
        }

        protected void GraficaDimension(XElement pFiltros)
        {
            rgdGraficasIndice.MasterTableView.GetColumn("NOMBRE").HeaderText = "Dimensión"; 
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            List<E_GRAFICAS> vGraficaDimension = nClima.ObtieneGraficaDimension(pID_PERIODO: vIdPeriodo, pXML_FILTROS: pFiltros, pIdRol:vIdRol).Select(s => new E_GRAFICAS { NO_NOMBRE = int.Parse(s.NO_DIMENSION.ToString()), NOMBRE = s.NB_DIMENSION, PORCENTAJE = s.PR_DIMENSION, COLOR_PORCENTAJE = s.COLOR_DIMENSION }).ToList();
            GraficasIndiceSatisfaccion(vGraficaDimension);
        }

        protected void GraficaTema(XElement pFiltros)
        {
            rgdGraficasIndice.MasterTableView.GetColumn("NOMBRE").HeaderText = "Tema"; 
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            List<E_GRAFICAS> vGraficaTema = nClima.ObtieneGraficaTema(pID_PERIODO: vIdPeriodo, pXML_FILTROS: pFiltros, pIdRol: vIdRol).Select(s => new E_GRAFICAS { NO_NOMBRE = int.Parse(s.NO_TEMA.ToString()), NOMBRE = s.NB_TEMA, PORCENTAJE = s.PR_TEMA, COLOR_PORCENTAJE = s.COLOR_TEMA }).ToList();
            GraficasIndiceSatisfaccion(vGraficaTema);
        }

        protected void GraficaPregunta(XElement pFiltros)
        {
            rgdGraficasIndice.MasterTableView.GetColumn("NOMBRE").HeaderText = "Pregunta"; 
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            List<E_GRAFICAS> vGraficaPregunta = nClima.ObtieneGraficaPregunta(pID_PERIODO: vIdPeriodo, pXML_FILTROS: pFiltros, pIdRol: vIdRol).Select(s => new E_GRAFICAS { NO_NOMBRE = int.Parse(s.NO_PREGUNTA.ToString()), NOMBRE = s.NB_PREGUNTA, PORCENTAJE = s.PR_PREGUNTA, COLOR_PORCENTAJE = s.COLOR_PREGUNTA }).ToList();
            GraficasIndiceSatisfaccion(vGraficaPregunta);
        }

        protected string ColoresPoncentajes(decimal? pPorcentaje)
        {
            string vColor = null;
            if (pPorcentaje >= 66)
                vColor = "green";
            if (pPorcentaje >= 35 && pPorcentaje < 66)
                vColor = "yellow";
            if (pPorcentaje <= 34)
                vColor = "red";

            return vColor;
        }

        protected void cmbMostradoPor_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            CargarDatosCombo(int.Parse(cmbMostradoPor.SelectedValue));
        }

        protected void CargarDimensiones()
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            List<E_TIPO_GRAFICAR> vLstDimensiones = nClima.ObtieneDimensiones(pID_PERIODO: vIdPeriodo).Select(s => new E_TIPO_GRAFICAR { NB_MOSTRADO = s.NB_DIMENSION }).ToList();
            LlenarCombo(vLstDimensiones);
        }

        protected void CargarTemas()
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            List<E_TIPO_GRAFICAR> vLstTemas = nClima.ObtieneTemas(pID_PERIODO: vIdPeriodo).Select(s => new E_TIPO_GRAFICAR { NB_MOSTRADO = s.NB_TEMA }).ToList();
            LlenarCombo(vLstTemas);
        }

        protected void CargarPreguntas()
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            List<E_TIPO_GRAFICAR> vLstPreguntas = nClima.ObtienePreguntas(pID_PERIODO: vIdPeriodo).Select(s => new E_TIPO_GRAFICAR { ID_MOSTRADO_POR = s.ID_PREGUNTA, NB_MOSTRADO = s.NB_PREGUNTA }).ToList();
            LlenarCombo(vLstPreguntas);
        }

        public void LlenarCombo(List<E_TIPO_GRAFICAR> pLstDatosCombo)
        {
            cmbTemaGraficar.DataSource = pLstDatosCombo;
            cmbTemaGraficar.DataTextField = "NB_MOSTRADO";
            cmbTemaGraficar.DataValueField = "NB_MOSTRADO";
            cmbTemaGraficar.DataBind();
        }

        protected void CargarDatosCombo(int pDatosCombo)
        {
            switch (pDatosCombo)
            {
                case 1: CargarDimensiones();
                    break;
                case 2: CargarTemas();
                    break;
                case 3: CargarPreguntas();
                    break;
            }
        }

        protected string Respuesta(decimal pNoRespuesta)
        {
            string vRespuesta = null;

            if (pNoRespuesta == 0)
                vRespuesta = "Inválidos";
            if (pNoRespuesta == 1)
                vRespuesta = "Totalmente en desacuerdo";
            if (pNoRespuesta == 2)
                vRespuesta = "Casi siempre en desacuerdo";
            if (pNoRespuesta == 3)
                vRespuesta = "Casi siempre de acuerdo";
            if (pNoRespuesta == 4)
                vRespuesta = "Totalmente de acuerdo";
            return vRespuesta;
        }

        protected string ColorRespuesta(int? pNoRespuesta)
        {
            string vColor = null;

            if (pNoRespuesta == 0)
                vColor = "gray";
            if (pNoRespuesta == 1)
                vColor = "red";
            if (pNoRespuesta == 2)
                vColor = "yellow";
            if (pNoRespuesta == 3)
                vColor = "LightBlue";
            if (pNoRespuesta == 4)
                vColor = "green";
            return vColor;
        }

        protected void GraficaDistribucionDimension(string pNbDimension, XElement pFiltros)
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            List<E_GRAFICAS> vGraficaDimension = nClima.ObtieneGraficaDistribucionDimension(pID_PERIODO: vIdPeriodo, pNB_DIMENSION: pNbDimension, pXML_FILTROS: pFiltros, pIdRol:vIdRol).Select(s => new E_GRAFICAS { NO_CANTIDAD = s.NO_CANTIDAD, NOMBRE = Respuesta((decimal)s.NO_RESPUESTA), PORCENTAJE = s.PR_DIMENSION, NO_RESPUESTA = s.NO_RESPUESTA }).ToList();
            GraficasDistribucion(vGraficaDimension);
        }

        protected void GraficaDistribucionTemas(string pNbTema, XElement pFiltros)
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            List<E_GRAFICAS> vGraficaTemas = nClima.ObtieneGraficaDistribucionTema(pID_PERIODO: vIdPeriodo, pNB_TEMA: pNbTema, pXML_FILTROS: pFiltros, pIdRol:vIdRol).Select(s => new E_GRAFICAS { NO_CANTIDAD = s.NO_CANTIDAD, NOMBRE = Respuesta((decimal)s.NO_RESPUESTA), PORCENTAJE = s.PR_TEMA, NO_RESPUESTA = s.NO_RESPUESTA }).ToList();
            GraficasDistribucion(vGraficaTemas);
        }

        protected void GraficaDistribucionPreguntas(string pNbPregunta, XElement pFiltros)
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            List<E_GRAFICAS> vGraficaPregunta = nClima.ObtieneGraficaDistribucionPregunta(pID_PERIODO: vIdPeriodo, pNB_PREGUNTA: pNbPregunta, pXML_FILTROS: pFiltros.ToString(), pIdRol: vIdRol).Select(s => new E_GRAFICAS { NO_CANTIDAD = s.NO_CANTIDAD, NOMBRE = Respuesta((decimal)s.NO_RESPUESTA), PORCENTAJE = s.PR_PREGUNTA, NO_RESPUESTA = s.NO_RESPUESTA }).ToList();
            GraficasDistribucion(vGraficaPregunta);
        }

        protected void GraficasDistribucion(List<E_GRAFICAS> plstPorcentajes)
        {
            rhcGraficaDistribucion.PlotArea.Series.Clear();
            PieSeries vSerie = new PieSeries();

            foreach (var item in plstPorcentajes)
            {
                string vColorColumna = ColorRespuesta(item.NO_RESPUESTA);
                Color vColor = System.Drawing.ColorTranslator.FromHtml(vColorColumna);
                vSerie.SeriesItems.Add(item.PORCENTAJE, vColor, item.NOMBRE);
                //vSerie.LabelsAppearance.DataFormatString = "{0:N2}%";
                vSerie.LabelsAppearance.Visible = false;
                vSerie.TooltipsAppearance.DataFormatString = "{0:N2}%";
            }
            rhcGraficaDistribucion.PlotArea.Series.Add(vSerie);
            rgGraficaDistribucion.DataSource = plstPorcentajes;
            rgGraficaDistribucion.DataBind();
        }

        protected void MostrarGraficaDistribucion(int pGraficaMostrar, string pTemaGraficar, XElement pFiltros)
        {
            switch (pGraficaMostrar)
            {
                case 1: GraficaDistribucionDimension(pTemaGraficar, pFiltros);
                    break;
                case 2: GraficaDistribucionTemas(pTemaGraficar, pFiltros);
                    break;
                case 3: GraficaDistribucionPreguntas(pTemaGraficar, pFiltros);
                    break;
            }
        }

        protected void cmbTemaGraficar_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //MostrarGraficaDistribucion(int.Parse(cmbMostradoPor.SelectedValue), cmbTemaGraficar.Text);
            FiltrosDistribucion();
        }

        protected void GraficaDistribucionGlobal()
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            List<E_GRAFICAS> vGraficaGlobal = nClima.ObtieneGraficaGlobal(pID_PERIODO: vIdPeriodo, pID_ROL: vIdRol).Select(s => new E_GRAFICAS { NO_CANTIDAD = s.NO_CANTIDAD, NOMBRE = Respuesta((decimal)s.NO_RESPUESTA), PORCENTAJE = s.PR_GLOBAL, NO_RESPUESTA = s.NO_RESPUESTA }).ToList();
            GraficaGlobal(vGraficaGlobal);
        }

        protected void GraficaGlobal(List<E_GRAFICAS> plstPorcentajes)
        {
            PieSeries vSerie = new PieSeries();

            foreach (var item in plstPorcentajes)
            {
                string vColorColumna = ColorRespuesta(item.NO_RESPUESTA);
                Color vColor = System.Drawing.ColorTranslator.FromHtml(vColorColumna);
                vSerie.SeriesItems.Add(item.PORCENTAJE, vColor, item.NOMBRE);
                //vSerie.LabelsAppearance.DataFormatString = "{0:N2}%";
                vSerie.LabelsAppearance.Visible = false;
                vSerie.TooltipsAppearance.DataFormatString = "{0:N2}%";
                rhcGraficaGlobal.PlotArea.XAxis.Items.Add(item.NO_NOMBRE.ToString());
                rhcGraficaGlobal.PlotArea.XAxis.LabelsAppearance.DataFormatString = item.NO_NOMBRE.ToString();
            }
            
            
            rhcGraficaGlobal.PlotArea.Series.Add(vSerie);
            rgGraficaGlobal.DataSource = plstPorcentajes;
            rgGraficaGlobal.DataBind();
        }

        protected void grdCodigoColores_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            List<E_CODIGO_COLORES> vCodigoColores = new List<E_CODIGO_COLORES>();
            vCodigoColores.Add(new E_CODIGO_COLORES { COLOR = "green", DESCRIPCION = "Resultados satisfactorios." });
            vCodigoColores.Add(new E_CODIGO_COLORES { COLOR = "yellow", DESCRIPCION = "Resultados medianamente satisfactorios" });
            vCodigoColores.Add(new E_CODIGO_COLORES { COLOR = "red", DESCRIPCION = "Resultados poco satisfactorios" });
            grdCodigoColores.DataSource = vCodigoColores;
        }

        public void FiltrosIndice()
        {
            List<E_SELECCIONADOS> vDepartamentos = new List<E_SELECCIONADOS>();
            List<E_ADICIONALES_SELECCIONADOS> vAdicionales = new List<E_ADICIONALES_SELECCIONADOS>();
            XElement vXlmFiltros = new XElement("FILTROS");
            XElement vXlmDepartamentos = new XElement("DEPARTAMENTOS");
            XElement vXlmGeneros = new XElement("GENEROS");
            XElement vXlmEdad = new XElement("EDAD");
            XElement vXlmAntiguedad = new XElement("ANTIGUEDAD");
            XElement vXlmCamposAdicional = new XElement("CAMPOS_ADICIONALES");

            foreach (RadListBoxItem item in lstDepartamentosIndice.Items)
            {
                int vIdDepartamento = int.Parse(item.Value);
                vDepartamentos.Add(new E_SELECCIONADOS { ID_SELECCIONADO = vIdDepartamento });
            }
            var vXelements = vDepartamentos.Select(x => new XElement("DEPARTAMENTO", new XAttribute("ID_DEPARTAMENTO", x.ID_SELECCIONADO)));
            vXlmDepartamentos = new XElement("DEPARTAMENTOS", vXelements);
            vXlmFiltros.Add(vXlmDepartamentos);

            foreach (RadListBoxItem item in rlbAdicionales.Items)
            {
                string vClAdicional = item.Value;
                vAdicionales.Add(new E_ADICIONALES_SELECCIONADOS { CL_CAMPO = vClAdicional });
            }
            var vXelementsAdicionales = vAdicionales.Select(x => new XElement("ADICIONAL", new XAttribute("CL_CAMPO", x.CL_CAMPO)));
            vXlmCamposAdicional = new XElement("SELECCIONADOS", vXelementsAdicionales);
            vXlmFiltros.Add(vXlmCamposAdicional);

            foreach (RadListBoxItem item in lstGeneroIndice.Items)
            {
                vXlmGeneros = new XElement("GENERO", new XAttribute("NB_GENERO", item.Value));
            }
            vXlmFiltros.Add(vXlmGeneros);

            if (rbEdadIndice.Checked == true)
            {

                vXlmEdad = new XElement("EDAD", new XAttribute("EDAD_INICIAL", rnEdadInicial.Text), new XAttribute("EDAD_FINAL", rnEdadFinal.Text));
                vXlmFiltros.Add(vXlmEdad);
            }
            if (rbAntiguedadIndice.Checked == true)
            {

                vXlmAntiguedad = new XElement("ANTIGUEDAD", new XAttribute("ANTIGUEDAD_INICIAL", rnAntiguedadInicial.Text), new XAttribute("ANTIGUEDAD_FINAL", rnAtiguedadFinal.Text));
                vXlmFiltros.Add(vXlmAntiguedad);
            }
            MostrarGraficaIndice(int.Parse(cmbIndiceSatisfaccion.SelectedValue), vXlmFiltros);
        }

        public void FiltrosDistribucion()
        {
            List<E_SELECCIONADOS> vDepartamentos = new List<E_SELECCIONADOS>();
            List<E_ADICIONALES_SELECCIONADOS> vAdicionalesDist = new List<E_ADICIONALES_SELECCIONADOS>();
            XElement vXlmFiltros = new XElement("FILTROS");
            XElement vXlmDepartamentos = new XElement("DEPARTAMENTOS");
            XElement vXlmGeneros = new XElement("GENEROS");
            XElement vXlmEdad = new XElement("EDAD");
            XElement vXlmAntiguedad = new XElement("ANTIGUEDAD");
            XElement vXlmCamposAdicional = new XElement("CAMPOS_ADICIONALES");

            foreach (RadListBoxItem item in rlbDepartamentoDistribucion.Items)
            {
                int vIdDepartamento = int.Parse(item.Value);
                vDepartamentos.Add(new E_SELECCIONADOS { ID_SELECCIONADO = vIdDepartamento });
            }
            var vXelements = vDepartamentos.Select(x => new XElement("DEPARTAMENTO", new XAttribute("ID_DEPARTAMENTO", x.ID_SELECCIONADO)));
            vXlmDepartamentos = new XElement("DEPARTAMENTOS", vXelements);
            vXlmFiltros.Add(vXlmDepartamentos);

            foreach (RadListBoxItem item in rlbAdicionalesDist.Items)
            {
                string vClAdicional = item.Value;
                vAdicionalesDist.Add(new E_ADICIONALES_SELECCIONADOS { CL_CAMPO = vClAdicional });
            }
            var vXelementsAdicionales = vAdicionalesDist.Select(x => new XElement("ADICIONAL", new XAttribute("CL_CAMPO", x.CL_CAMPO)));
            vXlmCamposAdicional = new XElement("SELECCIONADOS", vXelementsAdicionales);
            vXlmFiltros.Add(vXlmCamposAdicional);

            foreach (RadListBoxItem item in rlbGeneroDistribucion.Items)
            {
                vXlmGeneros = new XElement("GENERO", new XAttribute("NB_GENERO", item.Value));
            }
            vXlmFiltros.Add(vXlmGeneros);

            if (rbEdadDistribucion.Checked == true)
            {
                vXlmEdad = new XElement("EDAD", new XAttribute("EDAD_INICIAL", rntEdadInicialDis.Text), new XAttribute("EDAD_FINAL", rntEdadFinalDis.Text));
                vXlmFiltros.Add(vXlmEdad);
            }
            if (rbAntiguedadDistribucion.Checked == true)
            {
                vXlmAntiguedad = new XElement("ANTIGUEDAD", new XAttribute("ANTIGUEDAD_INICIAL", rntAntiguedadInicialDis.Text), new XAttribute("ANTIGUEDAD_FINAL", rntAntiguedadFinalDis.Text));
                vXlmFiltros.Add(vXlmAntiguedad);
            }
            MostrarGraficaDistribucion(int.Parse(cmbMostradoPor.SelectedValue), cmbTemaGraficar.SelectedValue, vXlmFiltros);
        }

        public void FiltrosPreguntasAbiertas()
        {
            List<E_SELECCIONADOS> vDepartamentos = new List<E_SELECCIONADOS>();
            List<E_ADICIONALES_SELECCIONADOS> vAdicionales = new List<E_ADICIONALES_SELECCIONADOS>();
            XElement vXlmFiltros = new XElement("FILTROS");
            XElement vXlmDepartamentos = new XElement("DEPARTAMENTOS");
            XElement vXlmGeneros = new XElement("GENEROS");
            XElement vXlmEdad = new XElement("EDAD");
            XElement vXlmAntiguedad = new XElement("ANTIGUEDAD");
            XElement vXlmCamposAdicional = new XElement("CAMPOS_ADICIONALES");

            foreach (RadListBoxItem item in rlbDepPreguntas.Items)
            {
                int vIdDepartamento = int.Parse(item.Value);
                vDepartamentos.Add(new E_SELECCIONADOS { ID_SELECCIONADO = vIdDepartamento });
            }
            var vXelements = vDepartamentos.Select(x => new XElement("DEPARTAMENTO", new XAttribute("ID_DEPARTAMENTO", x.ID_SELECCIONADO)));
            vXlmDepartamentos = new XElement("DEPARTAMENTOS", vXelements);
            vXlmFiltros.Add(vXlmDepartamentos);

            foreach (RadListBoxItem item in rlbPreAdicionales.Items)
            {
                string vClAdicional = item.Value;
                vAdicionales.Add(new E_ADICIONALES_SELECCIONADOS { CL_CAMPO = vClAdicional });
            }
            var vXelementsAdicionales = vAdicionales.Select(x => new XElement("ADICIONAL", new XAttribute("CL_CAMPO", x.CL_CAMPO)));
            vXlmCamposAdicional = new XElement("SELECCIONADOS", vXelementsAdicionales);
            vXlmFiltros.Add(vXlmCamposAdicional);

            foreach (RadListBoxItem item in rlbPreGenero.Items)
            {
                vXlmGeneros = new XElement("GENERO", new XAttribute("NB_GENERO", item.Value));
            }
            vXlmFiltros.Add(vXlmGeneros);

            if (chkPreguntasEdad.Checked == true)
            {
                vXlmEdad = new XElement("EDAD", new XAttribute("EDAD_INICIAL", rntbPreguntasMin.Text), new XAttribute("EDAD_FINAL", rntbPreguntasMax.Text));
                vXlmFiltros.Add(vXlmEdad);
            }
            if (chkAntiguedad.Checked == true)
            {
                vXlmAntiguedad = new XElement("ANTIGUEDAD", new XAttribute("ANTIGUEDAD_INICIAL", rntbAntiguedadMin.Text), new XAttribute("ANTIGUEDAD_FINAL", rntbAntiguedadMax.Text));
                vXlmFiltros.Add(vXlmAntiguedad);
            }

            vXmlFiltrosPre = vXlmFiltros.ToString();

        }

        protected void btnGraficaIndice_Click(object sender, EventArgs e)
        {
            FiltrosIndice();
            rtIndice.SelectedIndex = 1;
            rpvGraficaIndice.Selected = true;
        }

        protected void btnGraficaDistribucion_Click(object sender, EventArgs e)
        {
            FiltrosDistribucion();
            rtDistribucion.SelectedIndex = 1;
            rpvGraficaDistribucion.Selected = true;
        }

        protected void rgResultadosPreguntas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            rgResultadosPreguntas.DataSource = nClima.ObtenerPreguntasAbiertas(vIdPeriodo, null).ToList();
        }

        protected void rgResultadosPreguntas_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem datItem = (GridDataItem)e.DetailTableView.ParentItem;
            int vIdPregunta = int.Parse(datItem.GetDataKeyValue("ID_PREGUNTA").ToString());
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            e.DetailTableView.DataSource = nClima.ObtenerRespuestasAbiertas(pIdPeriodo: vIdPeriodo, pIdPregunta: vIdPregunta, pXmlFiltros: vXmlFiltrosPre, pIdRol: vIdRol).ToList();
        }

        protected void rgResultadosPreguntas_ItemCommand(object sender, GridCommandEventArgs e)
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

        protected void btnFiltrarPreguntas_Click(object sender, EventArgs e)
        {
            FiltrosPreguntasAbiertas();
            rgResultadosPreguntas.Rebind();
            rtPreguntasAbiertas.SelectedIndex = 1;
            rpvPreguntas.Selected = true;
        }

    }

    #region Clases

    public class E_ARREGLOS
    {
        public string clTipo { set; get; }
        public List<int> arrDepartamento { set; get; }
        public List<int> arrGenero { set; get; }
    }

    [Serializable]
    public class E_SELECCIONADOS
    {
        public int ID_SELECCIONADO { get; set; }

    }

    #endregion
}
