using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.FYD
{
    public partial class ReporteIndividual : System.Web.UI.Page
    {

        #region Variables

        private int vIdPeriodo
        {
            get { return (int)ViewState["vs_ri_id_periodo"]; }
            set { ViewState["vs_ri_id_periodo"] = value; }
        }

        private int? vIdEvaluado
        {
            get { return (int?)ViewState["vs_ri_id_evaluado"]; }
            set { ViewState["vs_ri_id_evaluado"] = value; }
        }

        private Guid vIdReporteIndividual
        {
            get { return (Guid)ViewState["vs_ri_id_reporte_individual"]; }
            set { ViewState["vs_ri_id_reporte_individual"] = value; }
        }

        private Guid vIdReporteComparativo
        {
            get { return (Guid)ViewState["vs_ri_id_reporte_comparativo"]; }
            set { ViewState["vs_ri_id_reporte_comparativo"] = value; }
        }

        public int? vIdEmpleado
        {
            get { return (int?)ViewState["vs_ri_id_empleado"]; }
            set { ViewState["vs_ri_id_empleado"] = value; }
        }

        public int? vIdPuesto
        {
            get { return (int?)ViewState["vs_vIdPuesto"]; }
            set { ViewState["vs_vIdPuesto"] = value; }
        }

        public int? vNoPuestos
        {
            get { return (int?)ViewState["vs_vNoPuestos"]; }
            set { ViewState["vs_vNoPuestos"] = value; }
        }

        private string vXmlPeriodos
        {
            get { return (string)ViewState["vs_ri_xml_periodos"]; }
            set { ViewState["vs_ri_xml_periodos"] = value; }

        }

        private List<int> vListaPeriodos
        {
            get { return (List<int>)ViewState["vs_vListaPeriodos"]; }
            set { ViewState["vs_vListaPeriodos"] = value; }
        }

        private List<string> oListaPuestos
        {
            get { return (List<string>)ViewState["vs_vps_lista_puestos"]; }
            set { ViewState["vs_vps_lista_puestos"] = value; }
        }

        private List<E_REPORTE_GENERAL_INDIVIDUAL> oReporteGeneralIndividual
        {
            get { return (List<E_REPORTE_GENERAL_INDIVIDUAL>)ViewState["vs_ri_lista_reporte_individual"]; }
            set { ViewState["vs_ri_lista_reporte_individual"] = value; }
        }

        private List<E_REPORTE_COMPARATIVO_INDIVIDUAL> vLstCompetenciasEvaluacion
        {
            get { return (List<E_REPORTE_COMPARATIVO_INDIVIDUAL>)ViewState["vs_vLstCompetenciasEvaluacion"]; }
            set { ViewState["vs_vLstCompetenciasEvaluacion"] = value; }
        }

        //private List<E_REPORTE_COMPARATIVO_INDIVIDUAL> vLstCReporte
        //{
        //    get { return (List<E_REPORTE_COMPARATIVO_INDIVIDUAL>)ViewState["vs_vLstCReporte"]; }
        //    set { ViewState["vs_vLstCReporte"] = value; }
        //}

        private List<E_PUESTOS_PERIODOS> lstPuestosPeriodos
        {
            get { return (List<E_PUESTOS_PERIODOS>)ViewState["vs_lstPuestosPeriodos"]; }
            set { ViewState["vs_lstPuestosPeriodos"] = value; }
        }

        private List<E_REPORTE_360> oReporte360
        {
            get { return (List<E_REPORTE_360>)ViewState["vs_ri_lista_reporte_360"]; }
            set { ViewState["vs_ri_lista_reporte_360"] = value; }
        }

        private List<E_PERIODO> vLstPeriodosComparativo
        {
            get { return (List<E_PERIODO>)ViewState["vs_vLstPeriodosComparativo"]; }
            set { ViewState["vs_vLstPeriodosComparativo"] = value; }
        }

        private List<SPE_OBTIENE_FYD_PUESTOS_EVALUADO_Result> vPuestosEvaluados { get; set; }

        public int? vNoPeriodos { get; set; }

        private List<E_REPORTE_GENERAL_INDIVIDUAL> vLstIndividual
        {
            get { return (List<E_REPORTE_GENERAL_INDIVIDUAL>)ViewState["vs_vLstIndividual"]; }
            set { ViewState["vs_vLstIndividual"] = value; }
        }

        private List<E_RESPUESTAS_PREGUNTAS_ADICIONALES> vListaRespuestasPreguntas
        {
            get { return (List<E_RESPUESTAS_PREGUNTAS_ADICIONALES>)ViewState["vs_vListaRespuestas"]; }
            set { ViewState["vs_vListaRespuestas"] = value; }
        }


        #endregion

        #region Funciones

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
                if (pNota != null)
                {
                    if (pNota.DS_NOTA != null)
                    {
                        return pNota.DS_NOTA.ToString();
                    }
                    else return "";
                }
                else
                    return "";
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

        private void CargarDatos()
        {
            ConsultaIndividualNegocio neg = new ConsultaIndividualNegocio();
            vListaPeriodos = new List<int>();
            SPE_OBTIENE_FYD_EVALUADOS_CONFIGURACION_Result oEvaluado;

            vNoPeriodos = 0;

            if (vIdReporteIndividual != Guid.Empty)
            {
                E_REPORTE_INDIVIDUAL oReporteIndividual = ContextoReportes.oReporteIndividual.Where(t => t.vIdReporteIndividual == vIdReporteIndividual).FirstOrDefault();
                vIdPeriodo = oReporteIndividual.vIdPeriodo;
                vListaPeriodos = oReporteIndividual.vListaPeriodos;
                vNoPeriodos = vListaPeriodos.Count;
            }
            else if (vIdReporteComparativo != Guid.Empty)
            {
                E_REPORTE_COMPARATIVO oReporteComparativo = ContextoReportes.oReporteComparativo.Where(t => t.vIdReporteComparativo == vIdReporteComparativo).FirstOrDefault();
                vIdPeriodo = oReporteComparativo.vIdPeriodo;
                vListaPeriodos = oReporteComparativo.vListaPeriodos;
                vNoPeriodos = vListaPeriodos.Count;
            }

            if (vNoPeriodos > 0)
                lblAdvertencia.InnerText = "* Alguno de los períodos que aparecen aún no ha sido cerrados por lo que alguna de las calificaciones podrían ser parciales";
            else
            {
                lblAdvertencia.InnerText = "* El periodo aún no ha sido cerrado por lo que las calificaciones podrían ser parciales";
                rtsReportes.Tabs[4].Visible = false;
            }

            foreach (int item in vListaPeriodos)
            {
                var oPeriodoComp = neg.ObtenerPeriodoEvaluacion(item);
                if (oPeriodoComp != null)
                {
                    if (oPeriodoComp.CL_ESTADO_PERIODO == "ABIERTO")
                    {
                        lblAdvertencia.Visible = true;
                    }
                }
            }

            var oPeriodo = neg.ObtenerPeriodoEvaluacion(vIdPeriodo);

            if (oPeriodo != null)
            {
                txtNoPeriodo.InnerText = oPeriodo.CL_PERIODO;
                txtNbPeriodo.InnerText = oPeriodo.DS_PERIODO;

                if (oPeriodo.FG_PUESTO_ACTUAL)
                    ulTipoEvaluacion.Controls.Add(new HtmlGenericControl("li") { InnerText = "Puesto actual" });

                if (oPeriodo.FG_OTROS_PUESTOS)
                    ulTipoEvaluacion.Controls.Add(new HtmlGenericControl("li") { InnerText = "Otros puestos" });

                if (oPeriodo.FG_RUTA_VERTICAL)
                    ulTipoEvaluacion.Controls.Add(new HtmlGenericControl("li") { InnerText = "Ruta vertical" });

                if (oPeriodo.FG_RUTA_VERTICAL_ALTERNATIVA)
                    ulTipoEvaluacion.Controls.Add(new HtmlGenericControl("li") { InnerText = "Ruta vertical alternativa" });

                if (oPeriodo.FG_RUTA_HORIZONTAL)
                    ulTipoEvaluacion.Controls.Add(new HtmlGenericControl("li") { InnerText = "Ruta horizontal alternativa" });

                if (oPeriodo.FG_GENERICAS)
                    ulTipoEvaluacion.Controls.Add(new HtmlGenericControl("li") { InnerText = "Competencias genéricas" });

                if (oPeriodo.FG_ESPECIFICAS)
                    ulTipoEvaluacion.Controls.Add(new HtmlGenericControl("li") { InnerText = "Competencias específicas" });

                if (oPeriodo.FG_INSTITUCIONALES)
                    ulTipoEvaluacion.Controls.Add(new HtmlGenericControl("li") { InnerText = "Competencias institucionales" });

                if (oPeriodo.CL_TIPO_EVALUACION.Equals(E_CL_TIPO_EVALUACION.COMPETENCIAS_OTRAS.ToString()))
                    ulTipoEvaluacion.Controls.Add(new HtmlGenericControl("li") { InnerText = "Otras competencias" });

                if (oPeriodo.DS_NOTAS != null)
                {
                    XElement vNotas = XElement.Parse(oPeriodo.DS_NOTAS);
                    if (vNotas != null)
                    {
                        txtDsNotas.InnerHtml = validarDsNotas(oPeriodo.DS_NOTAS);
                    }
                }

                if (oPeriodo.CL_ESTADO_PERIODO == "ABIERTO")
                    lblAdvertencia.Visible = true;
            }

            //if (Request.Params["ClTipoReporte"] != null)
            //{
            //    if (Request.Params["ClTipoReporte"].ToString() == "COMPARATIVO")
            //    {
               
            //    }
            //}


            if (vIdEvaluado != null)
            {
                oEvaluado = neg.ObtenerEvaluados(vIdPeriodo).Where(t => t.ID_EVALUADO == vIdEvaluado).FirstOrDefault();
            }
            else if (vIdEmpleado != null)
            {
                oEvaluado = neg.ObtenerEvaluados(vIdPeriodo).Where(t => t.ID_EMPLEADO == vIdEmpleado).FirstOrDefault();
            }
            else
            {
                oEvaluado = null;
            }

            if (oEvaluado != null)
            {
                txtClEvaluado.InnerText = oEvaluado.CL_EMPLEADO;
                txtClPuesto.InnerText = oEvaluado.CL_PUESTO;
                txtNbEvaluado.InnerText = oEvaluado.NB_EMPLEADO_COMPLETO;
                txtNbPuesto.InnerText = oEvaluado.NB_PUESTO;
                vIdEmpleado = oEvaluado.ID_EMPLEADO;
                vIdEvaluado = oEvaluado.ID_EVALUADO;
                vIdPuesto = oEvaluado.ID_PUESTO;

                PeriodoNegocio nPeriodo = new PeriodoNegocio();

                vPuestosEvaluados = nPeriodo.ObtienePuestosEvaluado(oEvaluado.ID_EVALUADO);
                grdPuestosEvaluados.DataSource = vPuestosEvaluados;
                grdPuestosEvaluados.DataBind();

                vNoPuestos = vPuestosEvaluados.Count + 2;
                if (vPuestosEvaluados.Count > 4)
                    vNoPuestos = 4;

            }

            ObtenerXmlPeriodos(vListaPeriodos);
        }

        private void ConfigurarGraficas()
        {
            GenerarSeriesReporte360();
            GenerarSeriesReportePuestos();
        }

        private void ObtenerXmlPeriodos(List<int> pListaPeriodos)
        {
            XElement periodos = new XElement("PERIODOS");

            if (pListaPeriodos.Count > 0)
            {
                if ((Request.Params["ClTipoReporte"].ToString() == "COMPARATIVO"))
                {
                    periodos.Add(pListaPeriodos.Select(t => new XElement("PERIODO", new XAttribute("ID_PERIODO", t.ToString()))));
                    rtsReportes.SelectedIndex = 4;
                    rpvReporteComparativo.Selected = true;
                }
                else
                {
                    periodos.Add(new XElement("PERIODO", new XAttribute("ID_PERIODO", vIdPeriodo)));
                    periodos.Add(pListaPeriodos.Select(t => new XElement("PERIODO", new XAttribute("ID_PERIODO", t.ToString()))));
                }
            }

            vXmlPeriodos = periodos.ToString();
        }

        private string obtenerPromedioReporteGeneralIndividual(string vClPuesto)
        {
            //int i = 0, j = 0;
            decimal vPromedio = 0;

            vPromedio = oReporteGeneralIndividual.Where(t => t.CL_PUESTO == vClPuesto).Average(t => t.NO_TOTAL_TIPO_EVALUACION).Value;
            //i = ComparacionCompetencias.Where(t => t.NO_ACIERTO == 1 & t.CL_EMPLEADO == vClEmpleado).Count();
            //j = ComparacionCompetencias.Where(t => t.CL_EMPLEADO == vClEmpleado).Count();
            //promedio = (Convert.ToDecimal(i) / Convert.ToDecimal(j)) * 100;

            return string.Format("{0:N2}", vPromedio);
        }

        private void GenerarSeriesReporte360()
        {
            GenerarItemsX(rhcPenslamiento);

            List<string> oListaTipoEvaluacionPerfiles = new List<string>();

            oListaTipoEvaluacionPerfiles = (from c in oReporte360
                                            group c by new { c.CL_PUESTO, c.NO_ORDEN } into grp
                                            orderby grp.Key.NO_ORDEN ascending
                                            select grp.Key.CL_PUESTO).ToList();

            foreach (string item in oListaTipoEvaluacionPerfiles)
            {
                rhcPenslamiento.PlotArea.Series.Add(ConfigurarSeries360(item));

            }
        }

        private void GenerarItemsX(RadHtmlChart rhcGrafica)
        {
            List<string> oListaCompetencias = new List<string>();

            oListaCompetencias = (from c in oReporte360
                                  group c by new { c.NO_ORDEN_CONSECUTIVO } into grp
                                  orderby grp.Key.NO_ORDEN_CONSECUTIVO ascending
                                  select grp.Key.NO_ORDEN_CONSECUTIVO.ToString()).ToList();

            foreach (string item in oListaCompetencias)
            {
                rhcGrafica.PlotArea.XAxis.Items.Add(item);
            }
        }

        private RadarLineSeries ConfigurarSeries360(string pClPuesto)
        {
            List<E_REPORTE_360> oListaCompetencias = (oReporte360.Where(t => t.CL_PUESTO == pClPuesto).ToList()).OrderBy(t => t.ID_COMPETENCIA).ToList();
            RadarLineSeries vLinea = new RadarLineSeries();

            vLinea.Name = pClPuesto;
            //vLinea.le
            vLinea.LabelsAppearance.Visible = false;
            //vLinea.TooltipsAppearance.ClientTemplate = "Nombre";

            List<string> oListaCompetenciasCompleta = new List<string>();

            oListaCompetenciasCompleta = (from c in oReporte360
                                          group c by new { c.NO_COMPETENCIA } into grp
                                          orderby grp.Key.NO_COMPETENCIA ascending
                                          select grp.Key.NO_COMPETENCIA.ToString()).ToList();


            foreach (string clCompetencia in oListaCompetenciasCompleta)
            {
                E_REPORTE_360 item = oListaCompetencias.Where(w => w.NO_COMPETENCIA.ToString() == clCompetencia).FirstOrDefault();
                if (item != null)
                {
                    vLinea.SeriesItems.Add(new CategorySeriesItem(item.NO_VALOR_COMPETENCIA));
                }
                else
                {
                    vLinea.SeriesItems.Add(new CategorySeriesItem(0));
                }
            }
            //foreach (E_REPORTE_360 item in oListaCompetencias)
            //{
            //    CategorySeriesItem vPunto = new CategorySeriesItem(item.NO_VALOR_COMPETENCIA);
            //    vLinea.SeriesItems.Add(vPunto);
            //}

            return vLinea;
        }

        private void GenerarSeriesReportePuestos()
        {
            GenerarItemsX(rhcPuestos);


            List<string> oListaTipoEvaluacionPerfiles = new List<string>();

            oListaTipoEvaluacionPerfiles = (from c in oReporte360
                                            group c by new { c.CL_PUESTO, c.NO_ORDEN } into grp
                                            orderby grp.Key.NO_ORDEN ascending
                                            select grp.Key.CL_PUESTO).ToList();

            foreach (string item in oListaTipoEvaluacionPerfiles)
            {
                rhcPuestos.PlotArea.Series.Add(ConfigurarSeriesPuestos(item));
            }
        }

        private LineSeries ConfigurarSeriesPuestos(string pClPuesto)
        {
            List<E_REPORTE_360> oListaCompetencias = (oReporte360.Where(t => t.CL_PUESTO == pClPuesto).ToList()).OrderBy(t => t.ID_COMPETENCIA).ToList();
            LineSeries vLinea = new LineSeries();

            vLinea.Name = pClPuesto;
            vLinea.LabelsAppearance.Visible = false;


            List<string> oListaCompetenciasCompleta = new List<string>();

            oListaCompetenciasCompleta = (from c in oReporte360
                                          group c by new { c.NO_COMPETENCIA } into grp
                                          orderby grp.Key.NO_COMPETENCIA ascending
                                          select grp.Key.NO_COMPETENCIA.ToString()).ToList();


            foreach (string clCompetencia in oListaCompetenciasCompleta)
            {
                E_REPORTE_360 item = oListaCompetencias.Where(w => w.NO_COMPETENCIA.ToString() == clCompetencia).FirstOrDefault();
                if (item != null)
                    vLinea.SeriesItems.Add(new CategorySeriesItem(item.NO_VALOR_COMPETENCIA));
                else
                    vLinea.SeriesItems.Add(new CategorySeriesItem(0));
            }

            //foreach (E_REPORTE_360 item in oListaCompetencias)
            //{
            //    CategorySeriesItem vPunto = new CategorySeriesItem(item.NO_VALOR_COMPETENCIA);
            //    vLinea.SeriesItems.Add(vPunto);
            //}

            return vLinea;
        }

        //private HtmlGenericControl GeneraTablaHtml(int? pIdEmpleado, int? pIdPeriodo, int? pIdCompetencia)
        //{
        //    string vControl = "";
        //    string vDivsCeldasPo = "<table class=\"tablaColor\"> " +
        //"<tr><td class=\"puesto\"> {0}</td> </tr>" +
        //"<tr> " +
        //"<td class=\"porcentaje\"> " +
        //"<div class=\"divPorcentaje\">{1}</div> " +
        //"</td> " +
        //"<td class=\"color\"> " +
        //"<div class=\"{2}\">&nbsp;</div> " +
        //"</td> </tr>" +
        //"</table>";

        //    HtmlGenericControl vCtrlTabla = new HtmlGenericControl("table");

        //    HtmlGenericControl vCtrlRow = new HtmlGenericControl("tr");

        //    ConsultaIndividualNegocio negGen = new ConsultaIndividualNegocio();
        //    List<SPE_OBTIENE_FYD_REPORTE_GENERAL_INDIVIDUAL_Result> vListaIndividual = new List<SPE_OBTIENE_FYD_REPORTE_GENERAL_INDIVIDUAL_Result>();
        //    var vResultado = negGen.ObtenerCumplimientoGeneralIndividual(pIdPeriodo, pIdEmpleado, pIdCompetencia).FirstOrDefault();

        //    if (vResultado != null)
        //    {
        //        vListaIndividual = negGen.ObtenerReporteIndividualGeneral((int)pIdPeriodo, vResultado.ID_EVALUADO);

        //        if (vListaIndividual.Where(w => w.ID_COMPETENCIA == pIdCompetencia) != null)
        //        {
        //            foreach (var itemResult in vListaIndividual.Where(w => w.ID_COMPETENCIA == pIdCompetencia))
        //            {
        //                HtmlGenericControl vCtrlColumnaResultado = new HtmlGenericControl("td");
        //                vCtrlColumnaResultado.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 11pt; width:100px; border-radius:2px");
        //                HtmlGenericControl vCtrlDiv = new HtmlGenericControl("div");
        //                vCtrlDiv.Attributes.Add("style", "padding: 10px");
        //                vCtrlDiv.Attributes.Add("title", itemResult.NB_PUESTO);
        //                vCtrlDiv.InnerHtml = String.Format(vDivsCeldasPo, String.Format("<a href=\"javascript:OpenDescriptivo({0})\">{1}</a>", itemResult.ID_PUESTO, itemResult.CL_PUESTO), itemResult.NO_TOTAL_TIPO_EVALUACION + "%", GenerarColor(itemResult.CL_COLOR_COMPATIBILIDAD));
        //                vCtrlColumnaResultado.Controls.Add(vCtrlDiv);
        //                vCtrlRow.Controls.Add(vCtrlColumnaResultado);
        //            }
        //        }
        //        else
        //        {
        //            HtmlGenericControl vCtrlColumnaResultado = new HtmlGenericControl("td");
        //            vCtrlColumnaResultado.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 11pt; width:100px; border-radius:2px");
        //            HtmlGenericControl vCtrlDiv = new HtmlGenericControl("div");
        //            vCtrlDiv.Attributes.Add("style", "padding: 10px");
        //            vCtrlDiv.InnerHtml = String.Format(vDivsCeldasPo,"NA", "divNa");
        //            vCtrlColumnaResultado.Controls.Add(vCtrlDiv);
        //            vCtrlRow.Controls.Add(vCtrlColumnaResultado);
        //        }
        //    }
        //    else
        //    {
        //        HtmlGenericControl vCtrlColumnaResultado = new HtmlGenericControl("td");
        //        vCtrlColumnaResultado.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 11pt; width:100px; border-radius:2px");
        //        HtmlGenericControl vCtrlDiv = new HtmlGenericControl("div");
        //        vCtrlDiv.Attributes.Add("style", "padding: 10px");
        //        vCtrlDiv.InnerHtml = String.Format(vDivsCeldasPo, "NA", "divNa");
        //        vCtrlColumnaResultado.Controls.Add(vCtrlDiv);
        //        vCtrlRow.Controls.Add(vCtrlColumnaResultado);
        //    }

        //    vCtrlTabla.Controls.Add(vCtrlRow);

        //   // var vResultado = neg.ObtenerCumplimientoGeneralIndividual(pIdPeriodo, pIdEmpleado, pIdCompetencia).FirstOrDefault();

        //   // if (vResultado != null)
        //   // {

        //   //     switch (vResultado.CL_COLOR_COMPATIBILIDAD)
        //   //     {
        //   //         case "Green":
        //   //             vClaseDivs = "divVerde";
        //   //             break;

        //   //         case "Gold":
        //   //             vClaseDivs = "divAmarillo";
        //   //             break;

        //   //         case "Red":
        //   //             vClaseDivs = "divRojo";
        //   //             break;
        //   //     }




        //   //     vNbPorcentaje = string.Format("{0:N2}", vResultado.NO_TOTAL_TIPO_EVALUACION) + "%";
        //   //     vControl = String.Format(vDivsCeldasPo, vNbPorcentaje, vClaseDivs);
        //   // }
        //   // else{

        //   //vControl = String.Format(vDivsCeldasPo, "NA", "divNa");
        //   // }

        //    return vCtrlTabla;
        //}

        public string GenerarColor(string pColorCumplimiento)
        {
            string vClaseDivs = "";
            switch (pColorCumplimiento)
            {
                case "Green":
                    vClaseDivs = "divVerde";
                    break;
                case "Gold":
                    vClaseDivs = "divAmarillo";
                    break;
                case "Red":
                    vClaseDivs = "divRojo";
                    break;
            }

            return vClaseDivs;
        }

        #endregion

        private class MyTemplate : ITemplate
        {
            private string colname;
            protected HtmlGenericControl vControlGrid;

            public MyTemplate(string cName)
            {
                colname = cName;
            }

            public void InstantiateIn(System.Web.UI.Control container)
            {
                vControlGrid = new HtmlGenericControl("div");
                vControlGrid.ID = colname;
                container.Controls.Add(vControlGrid);
            }
        }

        public DataTable ConvertToDataTable<T>(IList<T> data, List<SPE_OBTIENE_FYD_PUESTOS_PERIODO_Result> puestosperiodos)
        {
            ConsultaGeneralNegocio negGen = new ConsultaGeneralNegocio();
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (var itemPuesto in puestosperiodos)
                table.Columns.Add(itemPuesto.ID_PERIODO.ToString() + itemPuesto.ID_PUESTO.ToString());

            foreach (T item in data)
            {
                int vIdCompetencia = 0;
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    if (prop.Name.Equals("ID_COMPETENCIA"))
                        vIdCompetencia = int.Parse(prop.GetValue(item).ToString());

                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }
            return table;
        }

        protected DataTable CrearDataTable<T>(IList<T> pLista, RadGrid pCtrlGrid, List<SPE_OBTIENE_FYD_PUESTOS_PERIODO_Result> vLstPuestosPeriodos, List<SPE_OBTIENE_FYD_REPORTE_COMPARATIVO_INDIVIDUAL_Result> vLstResultados)
        {
            //ConsultaIndividualNegocio neg = new ConsultaIndividualNegocio();
            //List<SPE_OBTIENE_FYD_REPORTE_COMPARATIVO_INDIVIDUAL_Result> vLstReporteComparativo = neg.ObtenerDatosReporteComparativoIndividual(vIdEmpleado.Value, vXmlPeriodos);

            if (vLstPeriodosComparativo == null)
                vLstPeriodosComparativo = new List<E_PERIODO>();

            foreach (SPE_OBTIENE_FYD_REPORTE_COMPARATIVO_INDIVIDUAL_Result item in vLstResultados)
            {
                if (!vLstPeriodosComparativo.Any(a => a.ID_PERIODO.Equals(item.ID_PERIODO)))
                    vLstPeriodosComparativo.Add(new E_PERIODO() { ID_PERIODO = item.ID_PERIODO, CL_PERIODO = item.CL_PERIODO, NB_PERIODO = item.NB_PERIODO, DS_PERIODO = item.DS_PERIODO });
            }

            DataTable vColumnas = ConvertToDataTable(pLista, vLstPuestosPeriodos);

            foreach (var vPeriodo in vLstPeriodosComparativo)
            {
                GridColumnGroup vColumnGroup = new GridColumnGroup();
                vColumnGroup.Name = vPeriodo.ID_PERIODO.ToString();
                vColumnGroup.HeaderText = vPeriodo.CL_PERIODO;
                vColumnGroup.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                rgComparativo.MasterTableView.ColumnGroups.Add(vColumnGroup);
            }

            foreach (var item in vLstPuestosPeriodos)
            {
                if (vLstPeriodosComparativo.Any(a => a.ID_PERIODO.Equals(item.ID_PERIODO)))
                {
                    GridTemplateColumn vBoundColumn = new GridTemplateColumn();
                    vBoundColumn.DataField = item.ID_PERIODO.ToString() + item.ID_PUESTO.ToString();
                    vBoundColumn.UniqueName = item.ID_PERIODO.ToString() + item.ID_PUESTO.ToString();
                    vBoundColumn.HeaderText = item.CL_PUESTO;
                    vBoundColumn.ItemTemplate = new MyTemplate(item.ID_PERIODO.ToString() + item.ID_PUESTO.ToString());
                    vBoundColumn.FooterStyle.HorizontalAlign = HorizontalAlign.Center;
                    vBoundColumn.FooterStyle.Font.Bold = true;
                    vBoundColumn.AllowFiltering = false;
                    vBoundColumn.ColumnGroupName = item.ID_PERIODO.ToString();
                    vBoundColumn.HeaderStyle.Width = 80;
                    vBoundColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    rgComparativo.MasterTableView.Columns.Add(vBoundColumn);
                }
            }

            return vColumnas;
        }

        public HtmlGenericControl GeneraTablaHtml(int pIdCompetencia, int pIdPeriodo, int pIdPuesto)
        {
            HtmlGenericControl vCtrlTabla = new HtmlGenericControl("table");
            if (pIdCompetencia > 0 && pIdCompetencia > 0)
            {
                string vDivsCeldasPo = "<table class=\"tablaColor\"> " +
                 "<tr> " +
                 "<td class=\"porcentaje\"> " +
                 "<div class=\"divPorcentaje\">{0}</div> " +
                 "</td> " +
                 "<td class=\"color\"> " +
                 "<div class=\"{1}\">&nbsp;</div> " +
                 "</td> </tr>" +
                 "</table>";

                HtmlGenericControl vCtrlRow = new HtmlGenericControl("tr");
                ConsultaIndividualNegocio neg = new ConsultaIndividualNegocio();
                //List<SPE_OBTIENE_FYD_REPORTE_GLOBAL_Result> vLstEvaluadosReporte = new List<SPE_OBTIENE_FYD_REPORTE_GLOBAL_Result>();
                //ConsultaGeneralNegocio negGen = new ConsultaGeneralNegocio();
                //vLstEvaluadosReporte = negGen.ObtenerDatosReporteGlobal(pIdPeriodo, null, false).ToList();
                //    int vIdEvaluado =0;

                //    if (vLstEvaluadosReporte != null)
                //        vIdEvaluado = vLstEvaluadosReporte.Where(w => w.ID_EMPLEADO == vIdEmpleado).FirstOrDefault().ID_EVALUADO;
                List<SPE_OBTIENE_FYD_REPORTE_GENERAL_INDIVIDUAL_Result> vListaIndividual = new List<SPE_OBTIENE_FYD_REPORTE_GENERAL_INDIVIDUAL_Result>();
                vListaIndividual = neg.ObtenerReporteIndividualGeneral(pIdPeriodo, null, vIdEmpleado.Value);
                //vLstIndividual = vListaIndividual.Select(s => new E_REPORTE_GENERAL_INDIVIDUAL
                //{
                //    CL_COLOR = s.CL_COLOR,
                //    CL_COLOR_COMPATIBILIDAD = s.CL_COLOR_COMPATIBILIDAD,
                //    CL_EVALUADO = s.CL_EVALUADO,
                //    CL_PUESTO = s.CL_PUESTO,
                //    CL_TIPO_COMPETENCIA = s.CL_TIPO_COMPETENCIA,
                //    DS_COMPETENCIA = s.DS_COMPETENCIA,
                //    ID_COMPETENCIA = s.ID_COMPETENCIA,
                //    ID_EMPLEADO = s.ID_EMPLEADO,
                //    ID_EVALUADO = s.ID_EVALUADO,
                //    ID_PERIODO = s.ID_PERIODO,
                //    ID_PUESTO = s.ID_PUESTO,
                //    NB_COMPETENCIA = s.NB_COMPETENCIA,
                //    NB_EVALUADO = s.NB_EVALUADO,
                //    NB_PUESTO = s.NB_PUESTO,
                //    NO_TOTAL_TIPO_EVALUACION = s.NO_TOTAL_TIPO_EVALUACION,
                //    NO_ORDEN = s.NO_ORDEN
                //}).ToList();
                //if (vListaIndividual.Where(w => w.ID_PERIODO == pIdPeriodo && pIdCompetencia == w.ID_COMPETENCIA && w.ID_PUESTO == pIdPuesto).Count() > 0)
                //{
                var itemResult = vListaIndividual.Where(w => w.ID_PERIODO == pIdPeriodo && pIdCompetencia == w.ID_COMPETENCIA && w.ID_PUESTO == pIdPuesto).FirstOrDefault();
                if (itemResult != null)
                {
                    HtmlGenericControl vCtrlColumnaResultado = new HtmlGenericControl("td");
                    vCtrlColumnaResultado.Attributes.Add("style", "width:100px;");
                    HtmlGenericControl vCtrlDiv = new HtmlGenericControl("div");
                    vCtrlDiv.InnerHtml = String.Format(vDivsCeldasPo, itemResult.NO_TOTAL_TIPO_EVALUACION + "%", GenerarColor(itemResult.CL_COLOR_COMPATIBILIDAD));
                    vCtrlColumnaResultado.Controls.Add(vCtrlDiv);
                    vCtrlRow.Controls.Add(vCtrlColumnaResultado);
                }
                else
                {
                    HtmlGenericControl vCtrlColumnaResultado = new HtmlGenericControl("td");
                    vCtrlColumnaResultado.Attributes.Add("style", "width:100px;");
                    HtmlGenericControl vCtrlDiv = new HtmlGenericControl("div");
                    vCtrlDiv.InnerHtml = String.Format(vDivsCeldasPo, "NA", "divNa");
                    vCtrlColumnaResultado.Controls.Add(vCtrlDiv);
                    vCtrlRow.Controls.Add(vCtrlColumnaResultado);
                }
                vCtrlTabla.Controls.Add(vCtrlRow);

                return vCtrlTabla;
            }

            return vCtrlTabla;
        }

        protected void Page_Init(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                oListaPuestos = new List<string>();
                vIdReporteComparativo = Guid.Empty;
                vIdReporteIndividual = Guid.Empty;

                if (Request.Params["IdReporteIndividual"] != null)
                {
                    vIdReporteIndividual = Guid.Parse(Request.Params["IdReporteIndividual"].ToString());
                }

                if (Request.Params["IdReporteIndividual"] != null)
                {
                    vIdReporteIndividual = Guid.Parse(Request.Params["IdReporteIndividual"].ToString());
                }
                else if (Request.Params["IdReporteComparativo"] != null)
                {
                    vIdReporteComparativo = Guid.Parse(Request.Params["IdReporteComparativo"].ToString());
                }
                else
                {
                    vIdReporteIndividual = Guid.NewGuid();

                    if (ContextoReportes.oReporteIndividual == null)
                    {
                        ContextoReportes.oReporteIndividual = new List<E_REPORTE_INDIVIDUAL>();
                    }

                    if (Request.Params["IdPeriodo"] != null)
                    {
                        vIdPeriodo = int.Parse(Request.Params["IdPeriodo"].ToString());
                    }

                    ContextoReportes.oReporteIndividual.Add(new E_REPORTE_INDIVIDUAL { vIdReporteIndividual = vIdReporteIndividual, vIdPeriodo = vIdPeriodo });
                    //rtsReportes.Tabs[3].Visible = false;
                }

                if (Request.Params["IdEvaluado"] != null)
                {
                    vIdEvaluado = int.Parse(Request.Params["IdEvaluado"].ToString());
                }

                if (Request.Params["IdEmpleado"] != null)
                {
                    vIdEmpleado = int.Parse(Request.Params["IdEmpleado"].ToString());
                }

                CargarDatos();


               
                if (vListaPeriodos.Count > 0)
                {
                    ConsultaIndividualNegocio neg = new ConsultaIndividualNegocio();
                    List<SPE_OBTIENE_FYD_REPORTE_COMPARATIVO_INDIVIDUAL_Result> vLstReporteComparativo = neg.ObtenerDatosReporteComparativoIndividual(vIdEmpleado.Value, vXmlPeriodos);
                    List<SPE_OBTIENE_FYD_PUESTOS_PERIODO_Result> vLstPuestosPeriodo = neg.ObtenerPuestosPeriodo(vIdEmpleado.Value, vXmlPeriodos);
                    lstPuestosPeriodos = new List<E_PUESTOS_PERIODOS>();

                    foreach (var item in vLstPuestosPeriodo)
                    {
                        lstPuestosPeriodos.Add(new E_PUESTOS_PERIODOS()
                            {
                                ID_PERIODO = item.ID_PERIODO,
                                ID_PUESTO = item.ID_PUESTO,
                                CL_PUESTO = item.CL_PUESTO,
                                NB_PUESTO = item.NB_PUESTO
                            }
                            );
                    }


                    vLstCompetenciasEvaluacion = new List<E_REPORTE_COMPARATIVO_INDIVIDUAL>();
                    // vLstCReporte = new List<E_REPORTE_COMPARATIVO_INDIVIDUAL>();

                    foreach (var item in vLstReporteComparativo)
                    {
                        bool exists = vLstCompetenciasEvaluacion.Exists(element => element.ID_COMPETENCIA == item.ID_COMPETENCIA);
                        if (!exists)
                        {
                            vLstCompetenciasEvaluacion.Add(new E_REPORTE_COMPARATIVO_INDIVIDUAL()
                            {
                                CL_COLOR = item.CL_COLOR,
                                ID_COMPETENCIA = item.ID_COMPETENCIA,
                                NB_COMPETENCIA = item.NB_COMPETENCIA,
                                NO_LINEA = item.NO_LINEA
                            });
                        }

                        //vLstCReporte.Add(
                        //     new E_REPORTE_COMPARATIVO_INDIVIDUAL()
                        //     {
                        //         ID_PERIODO = item.ID_PERIODO,
                        //         CL_PERIODO = item.CL_PERIODO,
                        //         NB_PERIODO = item.NB_PERIODO,
                        //         DS_PERIODO = item.DS_PERIODO,
                        //         ID_EVALUADO = item.ID_EVALUADO,
                        //         CL_COLOR = item.CL_COLOR,
                        //         ID_COMPETENCIA = item.ID_COMPETENCIA,
                        //         NB_COMPETENCIA = item.NB_COMPETENCIA,
                        //         PR_COMPATIBILIDAD = item.PR_COMPATIBILIDAD,
                        //         NO_LINEA = item.NO_LINEA
                        //     }
                        //    );
                    }

                    rgComparativo.DataSource = CrearDataTable(vLstCompetenciasEvaluacion, rgComparativo, vLstPuestosPeriodo, vLstReporteComparativo);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
            //    oListaPuestos = new List<string>();
            //    vIdReporteComparativo = Guid.Empty;
            //    vIdReporteIndividual = Guid.Empty;

            //    if (Request.Params["IdReporteIndividual"] != null)
            //    {
            //        vIdReporteIndividual = Guid.Parse(Request.Params["IdReporteIndividual"].ToString());
            //    }
            //    else if (Request.Params["IdReporteComparativo"] != null)
            //    {
            //        vIdReporteComparativo = Guid.Parse(Request.Params["IdReporteComparativo"].ToString());
            //    }
            //    else
            //    {
            //        vIdReporteIndividual = Guid.NewGuid();

            //        if (ContextoReportes.oReporteIndividual == null)
            //        {
            //            ContextoReportes.oReporteIndividual = new List<E_REPORTE_INDIVIDUAL>();
            //        }

            //        if (Request.Params["IdPeriodo"] != null)
            //        {
            //            vIdPeriodo = int.Parse(Request.Params["IdPeriodo"].ToString());
            //        }

            //        ContextoReportes.oReporteIndividual.Add(new E_REPORTE_INDIVIDUAL { vIdReporteIndividual = vIdReporteIndividual, vIdPeriodo = vIdPeriodo });
            //        rtsReportes.Tabs[3].Visible = false;
            //    }


            //    //if (Request.Params["IdPeriodo"] != null)
            //    //{
            //    //    vIdPeriodo = int.Parse(Request.Params["IdPeriodo"].ToString());
            //    //}

            //    if (Request.Params["IdEvaluado"] != null)
            //    {
            //        vIdEvaluado = int.Parse(Request.Params["IdEvaluado"].ToString());
            //    }

            //    if (Request.Params["IdEmpleado"] != null)
            //    {
            //        vIdEmpleado = int.Parse(Request.Params["IdEmpleado"].ToString());
            //    }

            // CargarDatos();

            CuestionarioNegocio nCuestionario = new CuestionarioNegocio();
            int vPreguntasAdicionales = nCuestionario.ObtienePreguntasAdicionales(null, vIdPeriodo, null).Count;
            if (vPreguntasAdicionales < 1)
            {
                rmpReportes.PageViews[5].Visible = false;
                rtsReportes.Tabs[5].Visible = false;
            }

            List<SPE_OBTIENE_FYD_RESPUESTAS_ADICIONALES_Result> vListaRespuestas = new List<SPE_OBTIENE_FYD_RESPUESTAS_ADICIONALES_Result>();
            vListaRespuestasPreguntas = new List<E_RESPUESTAS_PREGUNTAS_ADICIONALES>();
            vListaRespuestas = nCuestionario.ObtieneResultadosAdicionales(vIdEvaluado, null, null).ToList();
            foreach (var item in vListaRespuestas)
            {
                if (item.XML_PREGUNTAS_ADICIONALES != null)
                {
                    XElement vXmlSolicitud = XElement.Parse(item.XML_PREGUNTAS_ADICIONALES);
                    foreach (XElement vXmlRespuestas in vXmlSolicitud.Elements("CAMPO"))
                    {
                        E_RESPUESTAS_PREGUNTAS_ADICIONALES f = new E_RESPUESTAS_PREGUNTAS_ADICIONALES()
                        {
                            ID_CAMPO = UtilXML.ValorAtributo<string>(vXmlRespuestas.Attribute("ID_CAMPO")),
                            NB_CAMPO = UtilXML.ValorAtributo<string>(vXmlRespuestas.Attribute("NB_CAMPO")),
                            NB_VALOR = (UtilXML.ValorAtributo<string>(vXmlRespuestas.Attribute("CL_TIPO")) == "CHECKBOX") ? (UtilXML.ValorAtributo<string>(vXmlRespuestas.Attribute("NB_VALOR")) == "0") ? "No" : "Si" : (UtilXML.ValorAtributo<string>(vXmlRespuestas.Attribute("NB_VALOR")))
                        };
                        vListaRespuestasPreguntas.Add(f);
                    }

                }
            }

            // }
        }

        protected void rgIndividualGeneral_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            //rgIndividualGeneral.DataSource = neg.ObtenerDatosReporteGeneralIndividual(vIdPeriodo, vIdEvaluado.Value);
        }

        protected void rgReporte360_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            //
            //
            //rgReporte360.DataSource = vDataSource;
            //ConfigurarGraficas(vDataSource);
        }

        //protected void rpgComparativo_NeedDataSource(object sender, Telerik.Web.UI.PivotGridNeedDataSourceEventArgs e)
        //{
        //    ConsultaIndividualNegocio neg = new ConsultaIndividualNegocio();
        //    List<SPE_OBTIENE_FYD_REPORTE_COMPARATIVO_INDIVIDUAL_Result> vLstReporteComparativo = neg.ObtenerDatosReporteComparativoIndividual(vIdEmpleado.Value, vXmlPeriodos);


        //    if (vLstPeriodosComparativo == null)
        //        vLstPeriodosComparativo = new List<E_PERIODO>();

        //    if (vLstReporteComparativo.Count() != 0)
        //    {

        //        foreach (SPE_OBTIENE_FYD_REPORTE_COMPARATIVO_INDIVIDUAL_Result item in vLstReporteComparativo)
        //        {
        //            if (!vLstPeriodosComparativo.Any(a => a.ID_PERIODO.Equals(item.ID_PERIODO)))
        //                vLstPeriodosComparativo.Add(new E_PERIODO() { ID_PERIODO = item.ID_PERIODO, CL_PERIODO = item.CL_PERIODO, NB_PERIODO = item.NB_PERIODO, DS_PERIODO = item.DS_PERIODO });
        //        }

        //        rpgComparativo.DataSource = vLstReporteComparativo;
        //    }
        //    else {
        //        rtsReportes.Tabs[4].Visible = false;
        //    }
        //}

        //protected void rpgComparativo_CellDataBound(object sender, PivotGridCellDataBoundEventArgs e)
        //{
        //    if (e.Cell is PivotGridRowHeaderCell)
        //    {
        //        if (e.Cell.Controls.Count > 1)
        //        {
        //            (e.Cell.Controls[0] as Button).Visible = false;
        //        }
        //    }

        //    if (e.Cell is PivotGridColumnHeaderCell)
        //    {
        //        E_PERIODO vPeriodoComparativo = vLstPeriodosComparativo.Where(w => w.CL_PERIODO == e.Cell.DataItem.ToString()).FirstOrDefault();

        //        if (vPeriodoComparativo == null)
        //            vPeriodoComparativo = new E_PERIODO() { NB_PERIODO = e.Cell.DataItem.ToString(), CL_PERIODO = e.Cell.DataItem.ToString() };

        //        e.Cell.ToolTip = vPeriodoComparativo.NB_PERIODO;
        //        e.Cell.Text = vPeriodoComparativo.CL_PERIODO;


        //    }

        //    if (e.Cell is PivotGridDataCell)
        //    {
        //        PivotGridDataCell celda = (PivotGridDataCell)e.Cell;
        //        int vNoValorCompetencia;


        //        if (!celda.IsGrandTotalCell & !celda.IsTotalCell)
        //        {
        //            //System.Web.UI.HtmlControls.HtmlGenericControl divColor = celda.FindControl("divColorComparacionC") as System.Web.UI.HtmlControls.HtmlGenericControl;
        //            //System.Web.UI.HtmlControls.HtmlGenericControl divPromedio = celda.FindControl("divPromedioC") as System.Web.UI.HtmlControls.HtmlGenericControl;
        //            //System.Web.UI.HtmlControls.HtmlGenericControl divNa = celda.FindControl("divNaC") as System.Web.UI.HtmlControls.HtmlGenericControl;

        //            //if (e.Cell.DataItem == null)
        //            //{
        //            //    divNa.Style.Add("display", "block");
        //            //    divPromedio.Style.Add("display", "none");
        //            //    divColor.Style.Add("background-color", "gray");
        //            //    e.Cell.DataItem = "N/A";
        //            //}
        //            //else
        //            //{
        //            //    vNoValorCompetencia = Convert.ToInt32((decimal)e.Cell.DataItem);

        //            //    divNa.Style.Add("display", "none");
        //            //    divPromedio.Style.Add("display", "block");

        //            //    if (vNoValorCompetencia > 90)
        //            //    {
        //            //        divColor.Style.Add("background-color", "green");
        //            //    }
        //            //    else if (vNoValorCompetencia >= 70 & vNoValorCompetencia <= 89)
        //            //    {
        //            //        divColor.Style.Add("background-color", "gold");
        //            //    }
        //            //    else
        //            //    {
        //            //        divColor.Style.Add("background-color", "red");
        //            //    }
        //            //}

        //            System.Web.UI.HtmlControls.HtmlGenericControl divResultado = celda.FindControl("dvResultado") as System.Web.UI.HtmlControls.HtmlGenericControl;
        //            if (divResultado != null)
        //            {
        //                PivotGridDataCell item = e.Cell as PivotGridDataCell;
        //                int? vIdPeriodoItem = 0;
        //                int? vIdCompetencia = 0;
        //                int? vIdEmpleadoItem = vIdEmpleado;

        //                if (vPeriodoComparativo == null)

        //                if (item.DataItem != null)
        //                {
        //                    vIdPeriodoItem = int.Parse(item.DataItem.ToString());
        //                }
        //                if (item.ParentRowIndexes[0] != null)
        //                {
        //                    vIdCompetencia = int.Parse(item.ParentRowIndexes[0].ToString());
        //                }


        //                divResultado.Controls.Add(GeneraTablaHtml(vIdEmpleadoItem, vIdPeriodoItem, vIdCompetencia));
        //            }
        //        }


        //        if (celda.IsGrandTotalCell)
        //        {
        //            ConsultaGeneralNegocio neg  = new ConsultaGeneralNegocio();
        //            e.Cell.HorizontalAlign = HorizontalAlign.Right;

        //            if (celda.ParentColumnIndexes[0].ToString() != null)
        //            {
        //                var vPeriodoComparativo = vLstPeriodosComparativo.Where(w => w.CL_PERIODO == celda.ParentColumnIndexes[0].ToString()).FirstOrDefault();
        //                if (vPeriodoComparativo != null)
        //                {
        //                    var vPorcentaje = neg.ObtenerDatosReporteGlobal(vPeriodoComparativo.ID_PERIODO, null, false).FirstOrDefault();
        //                    if (vPorcentaje != null)
        //                        e.Cell.Text = vPorcentaje.PR_CUMPLIMIENTO.ToString() + "%";
        //                }
        //                else
        //                 e.Cell.Text = "0%";
        //            }
        //            else
        //            e.Cell.Text = "0%";
        //        }
        //    }
        //}

        //protected void pgIndividualGeneral_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        //{
        //    ConsultaIndividualNegocio neg = new ConsultaIndividualNegocio();
        //    oReporteGeneralIndividual = neg.ObtenerDatosReporteGeneralIndividual(vIdPeriodo, vIdEvaluado.Value);
        //    pgIndividualGeneral.DataSource = oReporteGeneralIndividual;
        //}

        protected void pgIndividualGeneral_CellDataBound(object sender, PivotGridCellDataBoundEventArgs e)
        {
            if (e.Cell is PivotGridRowHeaderCell)
            {
                if (e.Cell.Controls.Count > 1)
                {
                    (e.Cell.Controls[0] as Button).Visible = false;
                }
            }

            if (e.Cell is PivotGridColumnHeaderCell)
            {
                SPE_OBTIENE_FYD_PUESTOS_EVALUADO_Result vPuestoEvaluado = vPuestosEvaluados.Where(w => w.ID_PUESTO.ToString().Equals(e.Cell.DataItem.ToString())).FirstOrDefault();

                if (vPuestoEvaluado == null)
                    vPuestoEvaluado = new SPE_OBTIENE_FYD_PUESTOS_EVALUADO_Result();

                e.Cell.ToolTip = vPuestoEvaluado.NB_PUESTO;
                e.Cell.Text = String.Format("<a href='#' onclick='OpenDescriptivo({1})'>{0}</a>", vPuestoEvaluado.CL_PUESTO, vPuestoEvaluado.ID_PUESTO);
                oListaPuestos.Add(vPuestoEvaluado.CL_PUESTO);
            }



            if (e.Cell is PivotGridDataCell)
            {

                PivotGridDataCell celda = (PivotGridDataCell)e.Cell;
                int vNoValorCompetencia;

                if (celda.IsGrandTotalCell)
                {
                    celda.Text = "<div style=\"text-align: right;\">" + obtenerPromedioReporteGeneralIndividual(oListaPuestos[celda.ColumnIndex]).ToString() + "</div>";
                }
                else
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl divColor = celda.FindControl("divColorComparacion") as System.Web.UI.HtmlControls.HtmlGenericControl;
                    System.Web.UI.HtmlControls.HtmlGenericControl divPromedio = celda.FindControl("divPromedio") as System.Web.UI.HtmlControls.HtmlGenericControl;
                    System.Web.UI.HtmlControls.HtmlGenericControl divNa = celda.FindControl("divNa") as System.Web.UI.HtmlControls.HtmlGenericControl;

                    if (e.Cell.DataItem == null)
                    {
                        divNa.Style.Add("display", "block");
                        divPromedio.Style.Add("display", "none");
                        divColor.Style.Add("background-color", "gray");
                        e.Cell.DataItem = "N/A";
                    }
                    else
                    {
                        vNoValorCompetencia = 0;
                        decimal noValor = 0;
                        if (decimal.TryParse(e.Cell.DataItem.ToString(), out noValor))
                            vNoValorCompetencia = Convert.ToInt32((decimal)e.Cell.DataItem);


                        divNa.Style.Add("display", "none");
                        divPromedio.Style.Add("display", "block");

                        if (vNoValorCompetencia > 90)
                        {
                            divColor.Style.Add("background-color", "green");
                        }
                        else if (vNoValorCompetencia >= 70 & vNoValorCompetencia <= 89)
                        {
                            divColor.Style.Add("background-color", "gold");
                        }
                        else
                        {
                            divColor.Style.Add("background-color", "red");
                        }

                    }
                }
            }
        }

        protected void pgReporte360_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            ConsultaIndividualNegocio neg = new ConsultaIndividualNegocio();
            if (vIdPeriodo != 0 && vIdEvaluado != null)
            {
                oReporte360 = neg.ObtenerDatosReporte360(vIdPeriodo, vIdEvaluado.Value).OrderBy(w => w.NO_ORDEN).ToList();
                pgReporte360.DataSource = oReporte360;
                //GenerarSeriesReporte360();
                ConfigurarGraficas();
            }
        }

        protected void pgReporte360_CellDataBound(object sender, PivotGridCellDataBoundEventArgs e)
        {
            if (e.Cell is PivotGridRowHeaderCell)
            {
                if (e.Cell.Controls.Count > 1)
                {
                    (e.Cell.Controls[0] as Button).Visible = false;
                }
            }

            if (e.Cell is PivotGridColumnHeaderCell)
            {
                SPE_OBTIENE_FYD_PUESTOS_EVALUADO_Result vPuestoEvaluado = new SPE_OBTIENE_FYD_PUESTOS_EVALUADO_Result(); ;
                if (vPuestosEvaluados != null)
                    vPuestoEvaluado = vPuestosEvaluados.Where(w => w.CL_PUESTO.Equals(e.Cell.DataItem.ToString())).FirstOrDefault();

                if (vPuestoEvaluado != null)
                {
                    e.Cell.ToolTip = vPuestoEvaluado.NB_PUESTO;
                    e.Cell.Text = String.Format("<a href='#' onclick='OpenDescriptivo({1})'>{0}</a>", vPuestoEvaluado.CL_PUESTO, vPuestoEvaluado.ID_PUESTO);
                }
            }

            if (e.Cell is PivotGridDataCell)
            {
                PivotGridDataCell celda = (PivotGridDataCell)e.Cell;

                if (e.Cell.DataItem == null)
                {
                    e.Cell.Text = "<div style=\"text-align: right;\">0.00</div>";
                }
            }
        }

        protected void pgReportePuestos_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            if (vIdPeriodo != 0 && vIdEvaluado != null)
            {
                ConsultaIndividualNegocio neg = new ConsultaIndividualNegocio();
                oReporte360 = neg.ObtenerDatosReporte360(vIdPeriodo, vIdEvaluado.Value).OrderBy(w => w.NO_ORDEN).ToList();
                pgReportePuestos.DataSource = oReporte360;
            }
        }

        protected void pgReportePuestos_CellDataBound(object sender, PivotGridCellDataBoundEventArgs e)
        {
            if (e.Cell is PivotGridRowHeaderCell)
            {
                if (e.Cell.Controls.Count > 1)
                {
                    (e.Cell.Controls[0] as Button).Visible = false;
                }
            }

            if (e.Cell is PivotGridColumnHeaderCell)
            {
                SPE_OBTIENE_FYD_PUESTOS_EVALUADO_Result vPuestoEvaluado = new SPE_OBTIENE_FYD_PUESTOS_EVALUADO_Result();
                if (vPuestosEvaluados != null)
                    vPuestoEvaluado = vPuestosEvaluados.Where(w => w.CL_PUESTO.Equals(e.Cell.DataItem.ToString())).FirstOrDefault();

                if (vPuestoEvaluado != null)
                {
                    e.Cell.ToolTip = vPuestoEvaluado.NB_PUESTO;
                    e.Cell.Text = String.Format("<a href='#' onclick='OpenDescriptivo({1})'>{0}</a>", vPuestoEvaluado.CL_PUESTO, vPuestoEvaluado.ID_PUESTO);
                }
            }

            if (e.Cell is PivotGridDataCell)
            {
                PivotGridDataCell celda = (PivotGridDataCell)e.Cell;

                if (e.Cell.DataItem == null)
                {
                    e.Cell.Text = "<div style=\"text-align: right;\">0.00</div>";
                }
            }
        }

        protected void grdGeneralIndividual_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem gridItem = e.Item as GridDataItem;

                int vIdCompetencia = int.Parse(gridItem.GetDataKeyValue("ID_COMPETENCIA").ToString());
                string vDsCompetencia = vLstIndividual.Where(t => t.ID_COMPETENCIA == vIdCompetencia).FirstOrDefault().DS_COMPETENCIA;

                gridItem["NB_COMPETENCIA"].ToolTip = vDsCompetencia;
            }

        }

        protected void grdGeneralIndividual_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            List<E_REPORTE_GENERAL_INDIVIDUAL> vListaTemporal = new List<E_REPORTE_GENERAL_INDIVIDUAL>();
            ConsultaIndividualNegocio neg = new ConsultaIndividualNegocio();
            //grdCapacitacion.Columns.Clear();
            //if (vFgCargarGrid)
            //{
            grdGeneralIndividual.DataSource = neg.ObtenerDatosReporteGeneralIndividual(vIdPeriodo, vIdEvaluado, ref vListaTemporal);
            vLstIndividual = vListaTemporal;
            //}
        }

        protected void grdGeneralIndividual_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
        {
            switch (e.Column.UniqueName)
            {
                case "ID_COMPETENCIA":
                    ConfigurarColumna(e.Column, 10, "No Competencia", false, false, true, false, false);
                    break;
                case "CL_COLOR":
                    ConfigurarColumna(e.Column, 30, "", true, false, false, false, false);
                    break;
                case "NB_COMPETENCIA":
                    ConfigurarColumna(e.Column, 150, "Competencia", true, false, true, false, false);
                    break;
                case "DS_COMPETENCIA":
                    ConfigurarColumna(e.Column, 300, "Descripción", true, false, true, true, false);
                    break;
                case "ExpandColumn":
                    break;
                default:
                    ConfigurarColumna(e.Column, 120, "", true, true, false, true, true);
                    break;
            }
        }

        private void ConfigurarColumna(GridColumn pColumna, int pWidth, string pEncabezado, bool pVisible, bool pGenerarEncabezado, bool pFiltrarColumna, bool pFooter, bool pToolTip)
        {
            if (pGenerarEncabezado)
            {
                pEncabezado = GeneraEncabezado(pColumna);
            }

            if (pToolTip)
            {
                pColumna.HeaderTooltip = GenerarTooltip(pColumna);
            }

            if (pFooter)
            {
                pColumna.FooterText = ObtieneFooter(pColumna);
                pColumna.FooterStyle.Font.Bold = true;
                pColumna.FooterStyle.HorizontalAlign = HorizontalAlign.Right;
            }

            pColumna.HeaderStyle.Width = Unit.Pixel(pWidth);
            pColumna.HeaderText = pEncabezado;
            pColumna.Visible = pVisible;
            pColumna.HeaderStyle.Font.Bold = true;

            if (pFiltrarColumna & pVisible)
            {
                if (pWidth <= 60)
                {
                    (pColumna as GridBoundColumn).FilterControlWidth = Unit.Pixel(pWidth);
                }
                else
                {
                    (pColumna as GridBoundColumn).FilterControlWidth = Unit.Pixel(pWidth - 60);
                }
            }
            else
            {
                (pColumna as GridBoundColumn).AllowFiltering = false;
            }
        }

        private string GenerarTooltip(GridColumn pColumna)
        {
            string vToolTip = "";
            int vResultado;
            string vEmpleado = pColumna.UniqueName.ToString().Substring(0, pColumna.UniqueName.ToString().IndexOf('E'));

            if (pColumna.UniqueName == "DS_COMPETENCIA")
                return "Total:";

            if (int.TryParse(vEmpleado, out vResultado))
            {
                var vDatosEmpleado = vLstIndividual.Where(t => t.ID_PUESTO == vResultado).FirstOrDefault();

                if (vDatosEmpleado != null)
                {
                    vToolTip = vDatosEmpleado.NB_PUESTO;
                }

            }

            return vToolTip;
        }

        private string ObtieneFooter(GridColumn pColumna)
        {
            string vTolCumplimiento = "";
            int vResultado;
            string vEmpleado = pColumna.UniqueName.ToString().Substring(0, pColumna.UniqueName.ToString().IndexOf('E'));

            if (pColumna.UniqueName == "DS_COMPETENCIA")
                return "Total:";

            if (int.TryParse(vEmpleado, out vResultado))
            {
                var vDatosEmpleado = vLstIndividual.Where(t => t.ID_PUESTO == vResultado).FirstOrDefault();

                if (vDatosEmpleado != null)
                {
                    ConsultaIndividualNegocio neg = new ConsultaIndividualNegocio();
                    var vTotalCumplimiento = neg.ObtenerCumplimientoPuestoPeriodo(vIdPeriodo, vIdEvaluado, vDatosEmpleado.ID_PUESTO, null).FirstOrDefault();
                    vTolCumplimiento = vTotalCumplimiento.PR_CUMPLIMIENTO.ToString() + "%";
                }
                else
                    vTolCumplimiento = "0%";

            }

            return vTolCumplimiento;
        }

        private string GeneraEncabezado(GridColumn pColumna)
        {
            int vResultado;
            string vEncabezado = "";
            string vEmpleado = pColumna.UniqueName.ToString().Substring(0, pColumna.UniqueName.ToString().IndexOf('E'));

            if (int.TryParse(vEmpleado, out vResultado))
            {
                var vDatosEmpleado = vLstIndividual.Where(t => t.ID_PUESTO == vResultado).FirstOrDefault();

                if (vDatosEmpleado != null)
                {
                    //vEncabezado = "<div style=\"text-align:center;\"> " + vDatosEmpleado.CL_PUESTO+ "</div>";
                    vEncabezado = String.Format("<a href='#' onclick='OpenDescriptivo({1})'>{0}</a>", vDatosEmpleado.CL_PUESTO, vDatosEmpleado.ID_PUESTO);
                }
            }

            return vEncabezado;
        }

        protected void rgResultadosPreguntas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            CuestionarioNegocio nPeriodo = new CuestionarioNegocio();
            List<SPE_OBTIENE_FYD_PREGUNTAS_ADICIONALES_Result> vListaPreguntas = new List<SPE_OBTIENE_FYD_PREGUNTAS_ADICIONALES_Result>();
            vListaPreguntas = nPeriodo.ObtienePreguntasAdicionales(null, vIdPeriodo, null).ToList();
            rgResultadosPreguntas.DataSource = vListaPreguntas;
        }

        protected void rgResultadosPreguntas_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {

            GridDataItem datItem = (GridDataItem)e.DetailTableView.ParentItem;
            string vClPregunta = datItem.GetDataKeyValue("ID_CAMPO_PREGUNTA").ToString();
            e.DetailTableView.DataSource = vListaRespuestasPreguntas.Where(x => x.ID_CAMPO == vClPregunta).ToList();
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

        protected void rgComparativo_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //ConsultaIndividualNegocio neg = new ConsultaIndividualNegocio();
            //List<SPE_OBTIENE_FYD_REPORTE_COMPARATIVO_INDIVIDUAL_Result> vLstReporteComparativo = neg.ObtenerDatosReporteComparativoIndividual(vIdEmpleado.Value, vXmlPeriodos);
            //vLstCompetenciasEvaluacion = new List<E_REPORTE_COMPARATIVO_INDIVIDUAL>();

            //foreach (var item in vLstReporteComparativo)
            //{
            //    bool exists = vLstCompetenciasEvaluacion.Exists(element => element.ID_COMPETENCIA == item.ID_COMPETENCIA);
            //    if (!exists)
            //    {
            //        vLstCompetenciasEvaluacion.Add(new E_REPORTE_COMPARATIVO_INDIVIDUAL()
            //        {
            //            CL_COLOR = item.CL_COLOR,
            //            ID_COMPETENCIA = item.ID_COMPETENCIA,
            //            NB_COMPETENCIA = item.NB_COMPETENCIA,
            //            NO_LINEA = item.NO_LINEA
            //        });
            //    }
            //}

            //rgComparativo.DataSource = vLstCompetenciasEvaluacion;
        }

        protected void rgComparativo_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                int vIdCompetencia = int.Parse(item.GetDataKeyValue("ID_COMPETENCIA").ToString());

                foreach (var vPeriodo in lstPuestosPeriodos)
                {
                    HtmlGenericControl vCtrlDiv = (HtmlGenericControl)item.FindControl(vPeriodo.ID_PERIODO.ToString() + vPeriodo.ID_PUESTO.ToString());
                    if (vCtrlDiv != null)
                    {
                        vCtrlDiv.Controls.Add(GeneraTablaHtml(vIdCompetencia, vPeriodo.ID_PERIODO, vPeriodo.ID_PUESTO));
                    }
                }
            }

            if (e.Item is GridFooterItem)
            {
                GridFooterItem footer = (GridFooterItem)e.Item;
                footer["NB_COMPETENCIA"].Text = "Total:";

                foreach (var vPeriodo in lstPuestosPeriodos)
                {
                    if (vLstPeriodosComparativo.Any(a => a.ID_PERIODO.Equals(vPeriodo.ID_PERIODO)))
                    {
                        ConsultaIndividualNegocio neg = new ConsultaIndividualNegocio();
                        var vTotalCumplimiento = neg.ObtenerCumplimientoPuestoPeriodo(vPeriodo.ID_PERIODO, null, vPeriodo.ID_PUESTO, vIdEmpleado.Value).FirstOrDefault();
                        if (vTotalCumplimiento != null)
                            footer[vPeriodo.ID_PERIODO.ToString() + vPeriodo.ID_PUESTO.ToString()].Text = vTotalCumplimiento.PR_CUMPLIMIENTO.ToString() + "%";
                        else
                            footer[vPeriodo.ID_PERIODO.ToString() + vPeriodo.ID_PUESTO.ToString()].Text = "0%";
                    }
                }


            }
        }

    }
}