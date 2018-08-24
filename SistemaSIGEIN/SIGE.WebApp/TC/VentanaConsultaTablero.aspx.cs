using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.TableroControl;
using SIGE.Negocio.TableroControl;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.TC
{
    public partial class VentanaConsultaTablero : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private XElement vXmlComentarios { get; set; }
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        List<SPE_OBTIENE_RESULTADOS_FYD_TABLERO_Result> vLstResultFyD;
        List<SPE_OBTIENE_RESULTADOS_ED_TABLERO_Result> vLstResultEd;
        List<SPE_OBTIENE_RESULTADOS_TABULADORES_TABLERO_Result> vLstTabuladores;
        List<SPE_OBTIENE_COMPATIBILIDAD_PUESTO_TABLERO_Result> vLstCompatibilidad;
        List<SPE_OBTIENE_RESULTADOS_CLIMA_LABORAL_TABLERO_Result> vLstClimaLaboral;

        private int? vIdTableroControl
        {
            get { return (int?)ViewState["vs_vIdTableroControl"]; }
            set { ViewState["vs_vIdTableroControl"] = value; }
        }

        public int? vIdEmpleado
        {
            get { return (int?)ViewState["vs_vIdEmpleado"]; }
            set { ViewState["vs_vIdEmpleado"] = value; }
        }

        private int? vIdDesempeno
        {
            get { return (int?)ViewState["vs_vIdDesempeno"]; }
            set { ViewState["vs_vIdDesempeno"] = value; }
        }

        public int? vIdPuesto
        {
            get { return (int?)ViewState["vs_vIdPuesto"]; }
            set { ViewState["vs_vIdPuesto"] = value; }
        }

        private int? vIdCompetencias
        {
            get { return (int?)ViewState["vs_vIdCompetencias"]; }
            set { ViewState["vs_vIdCompetencias"] = value; }
        }

        private DataTable vTableroControl
        {
            get { return (DataTable)ViewState["vs_vTableroControl"]; }
            set { ViewState["vs_vTableroControl"] = value; }
        }

        private List<E_PERIODOS_EVALUADOS> vLstColumnas
        {
            get { return (List<E_PERIODOS_EVALUADOS>)ViewState["vs_vLstColumnas"]; }
            set { ViewState["vs_vLstColumnas"] = value; }
        }

        private List<E_COMENTARIOS_EVALUADOS> vLstComentariosEvaluados
        {
            get { return (List<E_COMENTARIOS_EVALUADOS>)ViewState["vs_vLstComentariosEvaluados"]; }
            set { ViewState["vs_vLstComentariosEvaluados"] = value; }
        }

        private List<E_EVALUADOS_TABLERO_CONTROL> vLstEvaluados
        {
            get { return (List<E_EVALUADOS_TABLERO_CONTROL>)ViewState["vs_vLstEvaluados"]; }
            set { ViewState["vs_vLstEvaluados"] = value; }
        }

        private bool? vFgComentarios
        {
            get { return (bool?)ViewState["vs_vFgComentarios"]; }
            set { ViewState["vs_vFgComentarios"] = value; }
        }

        #endregion

        #region Funciones

        private string ObtenerAdicionales(string pAdicionales)
        {
            string vAdicionales = "";
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(pAdicionales);
            XmlNodeList xmlAdicionales = xml.GetElementsByTagName("CAMPOS");

            XmlNodeList lista =
            ((XmlElement)xmlAdicionales[0]).GetElementsByTagName("CAMPO");

            foreach (XmlElement nodo in lista)
            {
                vAdicionales = vAdicionales + nodo.GetAttribute("NB_TEXTO") + ", ";
            }


            return vAdicionales;
        }

        private string ObtenerIconoDiferencia(decimal? pPrDiferencia, decimal? pMnSueldo)
        {
            string vImagen = null;
            if (pPrDiferencia < 0 & pMnSueldo != 0)
                vImagen = "Down";
            else if (pPrDiferencia > 0 & pMnSueldo != 0)
                vImagen = "Up";
            else if (pPrDiferencia == 0 & pMnSueldo == 0)
                vImagen = "Delete";
            else vImagen = "Equal";

            return vImagen;
        }

        private string ObtenerColorPromedio(decimal? pPromedio)
        {
            string vDivsCeldaPromedio = "divNa";

            if (pPromedio < 70)
                vDivsCeldaPromedio = "divRojo";
            if (pPromedio > 69 && pPromedio < 90)
                vDivsCeldaPromedio = "divAmarillo";
            if (pPromedio > 89)
                vDivsCeldaPromedio = "divVerde";

            return vDivsCeldaPromedio;
        }

        private string ObtenerComentarios()
        {
            foreach (GridDataItem item in grdTableroControl.MasterTableView.Items)
            {
                int vIdEvaluado = int.Parse(item.GetDataKeyValue("ID_EVALUADO").ToString());
                RadTextBox txtResultadoPorcentual = (RadTextBox)item.FindControl("txtComentarios");
                string vComentario = txtResultadoPorcentual.Text;

                if (vComentario != "")
                {

                    vLstComentariosEvaluados.Add(new E_COMENTARIOS_EVALUADOS
                    {
                        ID_EVALUADO = vIdEvaluado,
                        DS_COMENTARIO = vComentario
                    });

                    var vXelements = vLstComentariosEvaluados.Select(x =>
                              new XElement("COMENTARIO",
                              new XAttribute("ID_EVALUADO", x.ID_EVALUADO),
                              new XAttribute("DS_COMENTARIO", x.DS_COMENTARIO)));

                    vXmlComentarios = (new XElement("COMENTARIOS", vXelements));
                }
            }

            if (vXmlComentarios == null)
                vXmlComentarios = (new XElement("COMENTARIOS"));

            return vXmlComentarios.ToString();
        }

        private DataTable ObtieneTableroControl()
        {
            TableroControlNegocio nTablero = new TableroControlNegocio();
            vLstColumnas = nTablero.ObtenerPeriodosEvaluadosTableroControl(vIdTableroControl, vIdEmpleado).ToList();
            vLstEvaluados = nTablero.ObtenerEvaluadosTableroControl(vIdTableroControl, vIdEmpleado).ToList();
            vLstCompatibilidad = nTablero.ObtenerCompatibilidadPuestoTablero(vIdTableroControl, vIdEmpleado, vIdPuesto).ToList();
            vLstResultFyD = nTablero.ObtenerResultadosFyDTableroControl(vIdTableroControl, vIdEmpleado, vIdPuesto).ToList();
            vLstResultEd = nTablero.ObtenerResultadosEDTableroControl(vIdTableroControl, vIdEmpleado).ToList();
            vLstClimaLaboral = nTablero.ObtenerClimaLaboralTablero(vIdTableroControl, vIdEmpleado).ToList();
            vLstTabuladores = nTablero.ObtenerTabuladoresTableroControl(vIdTableroControl, vIdEmpleado).ToList();
            var vPeriodoTablero = nTablero.ObtenerPeriodoTableroControl(vIdTableroControl, null, vIdEmpleado, vIdPuesto).FirstOrDefault();

            string vDivsCeldaPromedio = "";
            string vAdicionales = "";
            string vDivsCeldasPo = "<table class=\"tablaColor\"> " +
             "<tr> " +
             "<td class=\"porcentaje\"> " +
             "<div class=\"divPorcentaje\">{0}</div> " +
             "</td> " +
             "<td class=\"color\"> " +
             "<div class=\"{1}\">&nbsp;</div> " +
             "</td> </tr> </table>";


            DataTable vDtPivot = new DataTable();

            vDtPivot.Columns.Add("FI_FOTOGRAFIA", typeof(string));
            vDtPivot.Columns.Add("NB_EMPLEADO_PUESTO", typeof(string));
            vDtPivot.Columns.Add("ID_EVALUADO", typeof(string));

            //  if (vFgComentarios == true)
            vDtPivot.Columns.Add("DS_COMENTARIOS", typeof(string));

            if (vPeriodoTablero.FG_EVALUACION_IDP == true)
                vDtPivot.Columns.Add("COMPATIVILIDAD_VS_PUESTO", typeof(string));

            foreach (var item in vLstColumnas)
            {
                if (item.CL_TIPO_PERIODO_REFERENCIA == "FD_EVALUACION")
                    vDtPivot.Columns.Add(item.ID_PERIODO_REFERENCIA.ToString(), typeof(string));
            }

            foreach (var item in vLstColumnas)
            {
                switch (item.CL_TIPO_PERIODO_REFERENCIA)
                {
                    case "EO_DESEMPENO":
                        vDtPivot.Columns.Add(item.ID_PERIODO_REFERENCIA.ToString(), typeof(string));
                        break;
                    case "EO_CLIMA":
                        vDtPivot.Columns.Add(item.ID_PERIODO_REFERENCIA.ToString(), typeof(string));
                        break;
                    case "TABULADOR":
                        vDtPivot.Columns.Add("T" + item.ID_PERIODO_REFERENCIA.ToString(), typeof(string));
                        break;
                }
            }

            if (vPeriodoTablero.FG_EVALUACION_FYD == true)
                vDtPivot.Columns.Add("TENDENCIA_FYD", typeof(string));
            if (vPeriodoTablero.FG_EVALUACION_DESEMPEÑO == true)
                vDtPivot.Columns.Add("TENDENCIA_ED", typeof(string));

            vDtPivot.Columns.Add("PROMEDIO", typeof(string));

            if (vPeriodoTablero.FG_EVALUACION_DESEMPEÑO == true)
                vDtPivot.Columns.Add("BONO", typeof(string));

            foreach (var item in vLstEvaluados)
            {
                decimal? vPromedio = 0;
                decimal? vPorcentajeIDP = 0;
                decimal? vPorcentajeFYD = 0;
                int vNumPeriodoFyD = 0;
                decimal? vPorcentajeED = 0;
                int vNumPeriodoED = 0;
                decimal? vPorcentajeCL = 0;
                int vNumPeriodoCL = 0;
                double vPromedioTotal = 0.0;

                DataRow vDr = vDtPivot.NewRow();

                vDr["DS_COMENTARIOS"] = item.DS_COMENTARIO;

                var vResultado = vLstEvaluados.Where(t => t.ID_EMPLEADO == item.ID_EMPLEADO && t.FI_FOTOGRAFIA != null).FirstOrDefault();
                if (vResultado != null)
                    vDr["FI_FOTOGRAFIA"] = "<div class=\"image_resize\"><img id=\"profileImage\" src=\"data:image/jpg;base64, " + Convert.ToBase64String(vResultado.FI_FOTOGRAFIA) + "\"></div>";
                else
                    vDr["FI_FOTOGRAFIA"] = "<div class=\"image_resize\"><img id=\"profileImage\"></div>";

                if (item.XML_CAMPOS_ADICIONALES != null)
                    vAdicionales = ObtenerAdicionales(item.XML_CAMPOS_ADICIONALES.ToString());

                vDr["NB_EMPLEADO_PUESTO"] = "<p title=\"Clave: " + item.CL_EMPLEADO + ", " + item.CL_DEPARTAMENTO + " " + item.NB_DEPARTAMENTO + ", " + vAdicionales + "\"><a href=\"javascript:OpenInventario(" + item.ID_EMPLEADO + ")\">" + item.NB_EMPLEADO + "</a></p>" + "<p title=\"" + item.CL_PUESTO + "\"><a href=\"javascript:OpenDescriptivo(" + item.ID_PUESTO_PERIODO + ")\">" + item.NB_PUESTO + "</a></p>";
                vDr["ID_EVALUADO"] = item.ID_EVALUADO.ToString();

                if (vPeriodoTablero.FG_EVALUACION_IDP == true)
                {
                    var vCompatibilidad = vLstCompatibilidad.Where(t => t.ID_EMPLEADO == item.ID_EMPLEADO && t.ID_PUESTO == item.ID_PUESTO_PERIODO).FirstOrDefault();
                    if (vCompatibilidad != null)
                    {
                        if (vCompatibilidad.PROMEDIO != null)
                        {
                            vPorcentajeIDP = vPorcentajeIDP + vCompatibilidad.PROMEDIO;
                            vDr["COMPATIVILIDAD_VS_PUESTO"] = String.Format(vDivsCeldasPo, "<p title=\"" + item.CL_PUESTO + ", " + item.NB_PUESTO + "\"><a href=\"javascript:openComparativaPuesto(" + vCompatibilidad.ID_CANDIDATO + ", " + vCompatibilidad.ID_PUESTO + ")\">" + vCompatibilidad.PROMEDIO.ToString() + "%</a></p>", vCompatibilidad.CL_COLOR_COMPATIBILIDAD);
                        }
                        else
                        {
                            vDr["COMPATIVILIDAD_VS_PUESTO"] = String.Format(vDivsCeldasPo, "<p title=\"" + item.CL_PUESTO + ", " + item.NB_PUESTO + "\">N/A</p>", "divNa");
                        }
                    }

                    else
                    {
                        vDr["COMPATIVILIDAD_VS_PUESTO"] = String.Format(vDivsCeldasPo, "<p title=\"" + item.CL_PUESTO + ", " + item.NB_PUESTO + "\">N/A</p>", "divNa");
                    }

                    vPromedio = vPromedio + ((vPorcentajeIDP) * (vPeriodoTablero.PR_IDP / 100));
                }

                foreach (var vColumn in vLstColumnas)
                {
                    if (vColumn.CL_TIPO_PERIODO_REFERENCIA == "FD_EVALUACION")
                    {
                        var vResult = vLstResultFyD.Where(t => t.ID_EMPLEADO == item.ID_EMPLEADO && t.ID_PERIODO_REFERENCIA == vColumn.ID_PERIODO_REFERENCIA).FirstOrDefault();
                        if (vResult != null)
                        {
                            vPorcentajeFYD = vPorcentajeFYD + vResult.PR_CUMPLIMIENTO;
                            vNumPeriodoFyD = vNumPeriodoFyD + 1;
                            vDr[vColumn.ID_PERIODO_REFERENCIA.ToString()] = String.Format(vDivsCeldasPo, "<p title=\"" + item.CL_PUESTO + ", " + item.NB_PUESTO + "\"><a href=\"javascript:OpenReporteIndividual(" + vResult.ID_EVALUADO + ", " + vResult.ID_PERIODO_REFERENCIA + ")\">" + vResult.PR_CUMPLIMIENTO.ToString() + "%</a></p>", vResult.CL_COLOR_CUMPLIMIENTO);
                        }
                        else
                        {
                            vDr[vColumn.ID_PERIODO_REFERENCIA.ToString()] = String.Format(vDivsCeldasPo, "<p title=\"" + item.CL_PUESTO + ", " + item.NB_PUESTO + "\">N/A<p>", "divNa");
                        }
                    }

                    if (vColumn.CL_TIPO_PERIODO_REFERENCIA == "EO_DESEMPENO")
                    {
                        var vResult = vLstResultEd.Where(t => t.ID_EMPLEADO == item.ID_EMPLEADO && t.ID_PERIODO_REFERENCIA == vColumn.ID_PERIODO_REFERENCIA).FirstOrDefault();
                        if (vResult != null)
                        {
                            vPorcentajeED = vPorcentajeED + vResult.PR_CUMPLIMIENTO_EVALUADO;
                            vNumPeriodoED = vNumPeriodoED + 1;
                            vDr[vColumn.ID_PERIODO_REFERENCIA.ToString()] = String.Format(vDivsCeldasPo, "<p title=\"" + item.CL_PUESTO + ", " + item.NB_PUESTO + "\"><a href=\"javascript:OpenReporteCumplimientoPersonal(" + vResult.ID_EVALUADO + ", " + vResult.ID_PERIODO_REFERENCIA + ")\">" + vResult.PR_CUMPLIMIENTO_EVALUADO.ToString() + "%</a></p>", vResult.CL_COLOR_CUMPLIMIENTO);
                        }
                        else
                        {
                            vDr[vColumn.ID_PERIODO_REFERENCIA.ToString()] = String.Format(vDivsCeldasPo, "<p title=\"" + item.CL_PUESTO + ", " + item.NB_PUESTO + "\">N/A</p>", "divNa");
                        }
                    }

                    if (vColumn.CL_TIPO_PERIODO_REFERENCIA == "TABULADOR")
                    {
                        var vResult = vLstTabuladores.Where(t => t.ID_EMPLEADO == item.ID_EMPLEADO && t.ID_PUESTO == item.ID_PUESTO_PERIODO).FirstOrDefault();
                        if (vResult != null)
                        {
                            string vIcono = ObtenerIconoDiferencia(vResult.DIFERENCIA, vResult.MN_SUELDO_ORIGINAL);
                            vDr["T" + vColumn.ID_PERIODO_REFERENCIA.ToString()] = "<p title=\"" + vResult.MN_SUELDO_ORIGINAL.ToString() + ", " + item.CL_PUESTO + ", " + item.NB_PUESTO + "\">" + (Math.Abs((decimal)vResult.DIFERENCIA)).ToString() + "% &nbsp;<img src='/Assets/images/Icons/25/Arrow" + vIcono + ".png' /></p>";
                        }
                        else
                        {
                            vDr["T" + vColumn.ID_PERIODO_REFERENCIA.ToString()] = "<p title=\"" + item.CL_PUESTO + ", " + item.NB_PUESTO + "\">N/A</p>";
                        }
                    }

                    if (vColumn.CL_TIPO_PERIODO_REFERENCIA == "EO_CLIMA")
                    {
                        var vResult = vLstClimaLaboral.Where(t => t.ID_EMPLEADO == item.ID_EMPLEADO && t.ID_PERIODO == vColumn.ID_PERIODO_REFERENCIA).FirstOrDefault();
                        if (vResult != null)
                        {
                            if (vResult.PROMEDIO_RESULTADO != null)
                            {
                                vPorcentajeCL = vPorcentajeCL + vResult.PROMEDIO_RESULTADO;
                                vNumPeriodoCL = vNumPeriodoCL + 1;
                                vDr[vColumn.ID_PERIODO_REFERENCIA.ToString()] = String.Format(vDivsCeldasPo, "<p title=\"" + item.CL_PUESTO + ", " + item.NB_PUESTO + "\"><a href=\"javascript:openCuestionarioClima(" + vResult.ID_EVALUADOR + ", " + vResult.ID_PERIODO + ")\">" + vResult.PROMEDIO_RESULTADO.ToString() + "%</a></p>", vResult.COLOR_DIMENSION);
                            }
                            else
                            {
                                vPorcentajeCL = vPorcentajeCL + vResult.PROMEDIO_RESULTADO;
                                vNumPeriodoCL = vNumPeriodoCL + 1;
                                vDr[vColumn.ID_PERIODO_REFERENCIA.ToString()] = String.Format(vDivsCeldasPo, "<p title=\"" + item.CL_PUESTO + ", " + item.NB_PUESTO + "\"><a href=\"javascript:openCuestionarioClima(" + vResult.ID_EVALUADOR + ", " + vResult.ID_PERIODO + ")\">0%</a></p>", vResult.COLOR_DIMENSION);
                            }
                        }
                        else
                        {
                            vDr[vColumn.ID_PERIODO_REFERENCIA.ToString()] = String.Format(vDivsCeldasPo, "<p title=\"" + item.CL_PUESTO + ", " + item.NB_PUESTO + "\">N/A</p>", "divNa");
                        }
                    }

                }

                if (vPeriodoTablero.FG_EVALUACION_FYD == true)
                {
                    var vResult = vLstResultFyD.Where(t => t.ID_EMPLEADO == item.ID_EMPLEADO && t.ID_PUESTO == item.ID_PUESTO_PERIODO).FirstOrDefault();
                    if (vResult != null)
                        vDr["TENDENCIA_FYD"] = String.Format(vDivsCeldasPo, vResult.FYD_TENDENCIA, vResult.CL_COLOR_TENDENCIA);
                    else
                        vDr["TENDENCIA_FYD"] = String.Format(vDivsCeldasPo, "N/A", "divNa");
                }

                if (vPeriodoTablero.FG_EVALUACION_DESEMPEÑO == true)
                {
                    var vResult = vLstResultEd.Where(t => t.ID_EMPLEADO == item.ID_EMPLEADO && t.ID_PUESTO_PERIODO == item.ID_PUESTO_PERIODO).FirstOrDefault();
                    if (vResult != null)
                        vDr["TENDENCIA_ED"] = String.Format(vDivsCeldasPo, vResult.ED_TENDENCIA, vResult.CL_COLOR_TENDENCIA);
                    else
                        vDr["TENDENCIA_ED"] = String.Format(vDivsCeldasPo, "N/A", "divNa");
                }

                if (vPeriodoTablero.FG_EVALUACION_DESEMPEÑO == true)
                {
                    var vBono = vLstResultEd.Where(t => t.ID_EMPLEADO == item.ID_EMPLEADO && t.ID_PUESTO_PERIODO == item.ID_PUESTO_PERIODO).FirstOrDefault();
                    if (vBono != null)
                        vDr["BONO"] = "$" + vBono.MN_BONO.ToString();
                    else
                        vDr["BONO"] = "N/A";
                }

                if (vNumPeriodoFyD > 0)
                    vPromedio = vPromedio + ((vPorcentajeFYD / vNumPeriodoFyD) * (vPeriodoTablero.PR_FYD / 100));
                if (vNumPeriodoED > 0)
                    vPromedio = vPromedio + ((vPorcentajeED / vNumPeriodoED) * (vPeriodoTablero.PR_DESEMPENO / 100));
                if (vNumPeriodoCL > 0)
                    vPromedio = vPromedio + ((vPorcentajeCL / vNumPeriodoCL) * (vPeriodoTablero.PR_CLIMA_LABORAL / 100));

                vDivsCeldaPromedio = ObtenerColorPromedio(vPromedio);
                if (vPromedio != null)
                    vPromedioTotal = (double)vPromedio;
                vDr["PROMEDIO"] = String.Format(vDivsCeldasPo, Math.Round(vPromedioTotal, 2).ToString() + "%", vDivsCeldaPromedio);

                vDtPivot.Rows.Add(vDr);
            }

            return vDtPivot;
        }

        private void ConfigurarColumna(GridColumn pColumn, int pWidth, string pEncabezado, bool pVisible, bool pGenerarEncabezado, bool pCrearToolTip)
        {
            pColumn.HeaderStyle.Width = Unit.Pixel(pWidth);
            pColumn.HeaderStyle.Font.Bold = true;
            pColumn.HeaderText = pEncabezado;
            pColumn.Visible = pVisible;
            pColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            pColumn.HeaderStyle.VerticalAlign = VerticalAlign.Middle;
            pColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            pColumn.HeaderStyle.BackColor = Color.DarkGray;
            pColumn.HeaderStyle.ForeColor = Color.White;
            if (pColumn.UniqueName == "TENDENCIA_FYD" || pColumn.UniqueName == "TENDENCIA_ED")
                pColumn.ColumnGroupName = "Tendencia";
            if (pColumn.UniqueName == "COMPATIVILIDAD_VS_PUESTO")
            {
                pColumn.HeaderStyle.ForeColor = Color.White;
                pColumn.HeaderStyle.BackColor = Color.Green;
                pColumn.ColumnGroupName = "Pruebas";
            }
            if (pColumn.UniqueName == "TENDENCIA_FYD")
            {
                pColumn.HeaderStyle.ForeColor = Color.White;
                pColumn.HeaderStyle.BackColor = Color.DarkOrange;
            }
            if (pColumn.UniqueName == "TENDENCIA_ED")
            {
                pColumn.HeaderStyle.ForeColor = Color.White;
                pColumn.HeaderStyle.BackColor = Color.DarkRed;
            }

        }

        protected void ConfigurarColumnaPeriodos(GridColumn pColumn, int pWidth, string pEncabezado, bool pVisible, bool pGenerarEncabezado, bool pFiltrarColumna, bool pCentrar, bool pCrearTooltip, bool HiperColumn)
        {
            pColumn.HeaderStyle.Width = Unit.Pixel(pWidth);
            pColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            pColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            pColumn.HeaderStyle.VerticalAlign = VerticalAlign.Middle;
            pColumn.HeaderStyle.Font.Bold = true;
            pColumn.HeaderTooltip = GenerarTooltip(pColumn);
            pColumn.HeaderText = GenerarEncabezado(pColumn);
            pColumn.ColumnGroupName = GenerarGroupName(pColumn);
            pColumn.HeaderStyle.ForeColor = Color.White;
            pColumn.HeaderStyle.BackColor = ColorEncabezado(pColumn.UniqueName.ToString());
            pColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        }

        private Color ColorEncabezado(string pNbColumna)
        {
            Color vColor = Color.Gray;
            string vIdColumna = "";
            var vUniqueName = pNbColumna.Split(new string[] { "T" }, StringSplitOptions.None);
            if (vUniqueName.Length == 1)
                vIdColumna = vUniqueName[0].ToString();
            else
                vIdColumna = vUniqueName[1].ToString();

            var vTipoPeriodo = vLstColumnas.Where(x => x.ID_PERIODO_REFERENCIA.ToString() == vIdColumna).FirstOrDefault();
            if (vTipoPeriodo != null)
            {
                switch (vTipoPeriodo.CL_TIPO_PERIODO_REFERENCIA)
                {
                    case "FD_EVALUACION":
                        vColor = Color.DarkOrange;
                        break;
                    case "EO_DESEMPENO":
                        vColor = Color.DarkRed;
                        break;
                    case "EO_CLIMA":
                        vColor = Color.DarkRed;
                        break;
                    case "TABULADOR":
                        vColor = Color.FromArgb(0, 102, 255);
                        break;
                }
            }


            return vColor;
        }

        private string GenerarTooltip(GridColumn pColumna)
        {
            string vToolTip = "";
            string vIdColumna = "";
            var vUniqueName = pColumna.UniqueName.ToString().Split(new string[] { "T" }, StringSplitOptions.None);
            if (vUniqueName.Length == 1)
                vIdColumna = vUniqueName[0].ToString();
            else
                vIdColumna = vUniqueName[1].ToString();
            var vResultado = vLstColumnas.Where(t => t.ID_PERIODO_REFERENCIA.ToString() == vIdColumna).FirstOrDefault();
            if (vResultado != null)
            {
                vToolTip = vResultado.DS_PERIODO + ", " + vResultado.FE_PERIODO.ToString();
                if (vResultado.CL_TIPO_PERIODO_REFERENCIA == "TABULADOR")
                    vToolTip = vResultado.CL_TABULADOR + ", " + vResultado.FE_TABULADOR.ToString();
            }

            return vToolTip;
        }

        private string GenerarEncabezado(GridColumn pColumna)
        {
            string vEncabezado = "";
            string vIdColumna = "";
            var vUniqueName = pColumna.UniqueName.ToString().Split(new string[] { "T" }, StringSplitOptions.None);
            if (vUniqueName.Length == 1)
                vIdColumna = vUniqueName[0].ToString();
            else
                vIdColumna = vUniqueName[1].ToString();

            var vTipoPeriodo = vLstColumnas.Where(x => x.ID_PERIODO_REFERENCIA.ToString() == vIdColumna).FirstOrDefault();
            if (vTipoPeriodo != null)
            {
                switch (vTipoPeriodo.CL_TIPO_PERIODO_REFERENCIA)
                {
                    case "FD_EVALUACION":
                        vEncabezado = "<a style=\"color:white\" href=\"javascript:OpenPeriodoFyD(" + vTipoPeriodo.ID_PERIODO_REFERENCIA + ")\">" + vTipoPeriodo.CL_PERIODO.ToString() + "</a>";
                        break;
                    case "EO_DESEMPENO":
                        vEncabezado = "<a style=\"color:white\" href=\"javascript:OpenPeriodoED(" + vTipoPeriodo.ID_PERIODO_REFERENCIA + ")\">" + vTipoPeriodo.CL_PERIODO.ToString() + "</a>";
                        break;
                    case "EO_CLIMA":
                        vEncabezado = "<a style=\"color:white\" href=\"javascript:OpenPeriodoCL(" + vTipoPeriodo.ID_PERIODO_REFERENCIA + ")\">" + vTipoPeriodo.CL_PERIODO.ToString() + "</a>";
                        break;
                    case "TABULADOR":
                        vEncabezado = "<a style=\"color:white\" href=\"javascript:OpenTabulador(" + vTipoPeriodo.ID_PERIODO_REFERENCIA + ")\">" + vTipoPeriodo.CL_TABULADOR.ToString() + "</a>";
                        break;
                }
            }
            return vEncabezado;
        }

        private string GenerarGroupName(GridColumn pColumn)
        {
            string vGrupo = "";
            string vIdColumna = "";
            var vUniqueName = pColumn.UniqueName.ToString().Split(new string[] { "T" }, StringSplitOptions.None);
            if (vUniqueName.Length == 1)
                vIdColumna = vUniqueName[0].ToString();
            else
                vIdColumna = vUniqueName[1].ToString();

            var vTipoPeriodo = vLstColumnas.Where(x => x.ID_PERIODO_REFERENCIA.ToString() == vIdColumna).FirstOrDefault();
            if (vTipoPeriodo != null)
            {
                switch (vTipoPeriodo.CL_TIPO_PERIODO_REFERENCIA)
                {
                    case "FD_EVALUACION":
                        vGrupo = "Formacion";
                        break;
                    case "EO_DESEMPENO":
                        vGrupo = "Desempeno";
                        break;
                    case "EO_CLIMA":
                        vGrupo = "Clima";
                        break;
                    case "TABULADOR":
                        vGrupo = "Tabulador";
                        break;
                }
            }
            return vGrupo;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                if (Request.Params["pIdTablero"] != null)
                {
                    vFgComentarios = false;
                    vIdTableroControl = int.Parse(Request.Params["pIdTablero"].ToString());

                    vLstColumnas = new List<E_PERIODOS_EVALUADOS>();
                    vLstEvaluados = new List<E_EVALUADOS_TABLERO_CONTROL>();
                    vLstResultFyD = new List<SPE_OBTIENE_RESULTADOS_FYD_TABLERO_Result>();
                    vLstResultEd = new List<SPE_OBTIENE_RESULTADOS_ED_TABLERO_Result>();
                    vLstTabuladores = new List<SPE_OBTIENE_RESULTADOS_TABULADORES_TABLERO_Result>();
                    vLstCompatibilidad = new List<SPE_OBTIENE_COMPATIBILIDAD_PUESTO_TABLERO_Result>();
                    vLstClimaLaboral = new List<SPE_OBTIENE_RESULTADOS_CLIMA_LABORAL_TABLERO_Result>();
                    vLstComentariosEvaluados = new List<E_COMENTARIOS_EVALUADOS>();

                    TableroControlNegocio nTablero = new TableroControlNegocio();
                    var vPeriodoTablero = nTablero.ObtenerPeriodoTableroControl(vIdTableroControl, null).FirstOrDefault();
                    btnGuardar.Enabled = vPeriodoTablero.FG_GENERADO == true ? false : true;
                    btnGuardarPrograma.Enabled = vPeriodoTablero.FG_GENERADO == true ? false : true;
                    btnRecalcular.Enabled = vPeriodoTablero.FG_GENERADO == true ? false : true;
                    btnGuardarConsulta.Visible = false;

                    if (vPeriodoTablero.FG_EVALUACION_IDP == true)
                        txtIdp.Value = (double?)vPeriodoTablero.PR_IDP;
                    else
                        txtIdp.Enabled = false;

                    if (vPeriodoTablero.FG_EVALUACION_FYD == true)
                        txtFyd.Value = (double?)vPeriodoTablero.PR_FYD;
                    else
                        txtFyd.Enabled = false;

                    if (vPeriodoTablero.FG_EVALUACION_DESEMPEÑO == true)
                        txtDesempeno.Value = (double?)vPeriodoTablero.PR_DESEMPENO;
                    else
                        txtDesempeno.Enabled = false;

                    if (vPeriodoTablero.FG_EVALUACION_CLIMA_LABORAL == true)
                        txtClima.Value = (double?)vPeriodoTablero.PR_CLIMA_LABORAL;
                    else
                        txtClima.Enabled = false;

                    //grdTableroControl.MasterTableView.ColumnGroups[0].HeaderText = "EVALUACIÓN DE PRUEBAS";
                    //grdTableroControl.MasterTableView.ColumnGroups[1].HeaderText = "EVALUACIÓN DE COMPETENCIAS </br> Compatibilidad con el puesto";
                    //grdTableroControl.MasterTableView.ColumnGroups[1].HeaderStyle.Height = 200;
                    //grdTableroControl.MasterTableView.ColumnGroups[2].HeaderText = "EVALUACIÓN DE DESEMPEÑO </br> Porcentaje de cumplimiento";
                    //grdTableroControl.MasterTableView.ColumnGroups[3].HeaderText = "CLIMA LABORAL </br> Resultado";
                    //grdTableroControl.MasterTableView.ColumnGroups[4].HeaderText = "EVALUACIÓN DEL SUELDO </br> Situación salarial";
                 //   grdTableroControl.MasterTableView.Columns[0].Visible = false;


                }

                if (Request.Params["idEmpleado"] != null)
                {
                    vFgComentarios = false;
                    vIdEmpleado = int.Parse(Request.Params["idEmpleado"].ToString());

                    if (Request.Params["idDesempeno"] != null)
                    {
                        vIdDesempeno = int.Parse(Request.Params["idDesempeno"].ToString());
                    }
                    if (Request.Params["idCompetencia"] != null)
                    {
                        vIdCompetencias = int.Parse(Request.Params["idCompetencia"].ToString());
                    }
                    if (Request.Params["idPuesto"] != null)
                    {
                        vIdPuesto = int.Parse(Request.Params["idPuesto"].ToString());
                    }

                    TableroControlNegocio nTablero = new TableroControlNegocio();
                    var vPeriodoTablero = nTablero.ObtenerPeriodoTableroControl(null, null, vIdEmpleado, vIdPuesto).FirstOrDefault();
                    btnGuardar.Visible = false;

                    if (vPeriodoTablero.FG_EVALUACION_IDP == true)
                        txtIdp.Value = (double?)vPeriodoTablero.PR_IDP;
                    else
                        txtIdp.Enabled = false;

                    if (vPeriodoTablero.FG_EVALUACION_FYD == true)
                        txtFyd.Value = (double?)vPeriodoTablero.PR_FYD;
                    else
                        txtFyd.Enabled = false;

                    if (vPeriodoTablero.FG_EVALUACION_DESEMPEÑO == true)
                        txtDesempeno.Value = (double?)vPeriodoTablero.PR_DESEMPENO;
                    else
                        txtDesempeno.Enabled = false;

                    if (vPeriodoTablero.FG_EVALUACION_CLIMA_LABORAL == true)
                        txtClima.Value = (double?)vPeriodoTablero.PR_CLIMA_LABORAL;
                    else
                        txtClima.Enabled = false;


                //    grdTableroControl.MasterTableView.Columns[0].Visible = false;
                }
            }

        }

        protected void grdTableroControl_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            vTableroControl = ObtieneTableroControl();
            grdTableroControl.DataSource = vTableroControl;

        }

        protected void grdTableroControl_ColumnCreated(object sender, Telerik.Web.UI.GridColumnCreatedEventArgs e)
        {
            switch (e.Column.UniqueName)
            {
                case "FI_FOTOGRAFIA":
                    ConfigurarColumna(e.Column, 150, "Fotografía", true, false, false);
                    break;
                case "NB_EMPLEADO_PUESTO":
                    ConfigurarColumna(e.Column, 300, "Nombre/Puesto", true, false, false);
                    break;
                case "ID_EVALUADO":
                    ConfigurarColumna(e.Column, 10, "Id Empleado", false, false, false);
                    break;
                case "DS_COMENTARIOS":
                    ConfigurarColumna(e.Column, 200, "Comentarios", (bool)vFgComentarios, false, false);
                    break;
                case "COMPATIVILIDAD_VS_PUESTO":
                    ConfigurarColumna(e.Column, 150, "Compatibilidad vs Puesto", true, false, false);
                    break;
                case "TENDENCIA_FYD":
                    ConfigurarColumna(e.Column, 150, "Evaluación de competencias", true, false, false);
                    break;
                case "TENDENCIA_ED":
                    ConfigurarColumna(e.Column, 150, "Evaluación de desempeño", true, false, false);
                    break;
                case "PROMEDIO":
                    ConfigurarColumna(e.Column, 150, "Promedio", true, false, false);
                    break;
                case "BONO":
                    ConfigurarColumna(e.Column, 150, "Bono", true, false, false);
                    break;
                default:
                    ConfigurarColumnaPeriodos(e.Column, 150, "", true, true, false, true, true, true);
                    break;
                case "ExpandColumn":
                    break;
            }
        }

        protected void btnGuardarPrograma_Click(object sender, EventArgs e)
        {
            decimal? vPonderacionIdp = (decimal?)txtIdp.Value == null ? 0 : (decimal?)txtIdp.Value;
            decimal? vPonderacionFyd = (decimal?)txtFyd.Value == null ? 0 : (decimal?)txtFyd.Value;
            decimal? vPonderacionDesempeno = (decimal?)txtDesempeno.Value == null ? 0 : (decimal?)txtDesempeno.Value;
            decimal? vPonderacionClima = (decimal?)txtClima.Value == null ? 0 : (decimal?)txtClima.Value;
            decimal? vTotalponderacion = vPonderacionIdp + vPonderacionFyd + vPonderacionDesempeno + vPonderacionClima;

            if (vTotalponderacion < 101 && vTotalponderacion > 99)
            {
                TableroControlNegocio nTablero = new TableroControlNegocio();
                E_RESULTADO vResultado = nTablero.ActualizaPonderaciones(vIdTableroControl, vPonderacionIdp, vPonderacionFyd, vPonderacionDesempeno, vPonderacionClima, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
                //if (rcbComentarios.Checked == false)
                //    grdTableroControl.MasterTableView.Columns[0].Visible = false;

            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "El total de las ponderaciones no puede ser mayor al 100%.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");

            }

        }

        protected void btnRecalcular_Click(object sender, EventArgs e)
        {
            grdTableroControl.Rebind();
            //if (rcbComentarios.Checked == false)
            //    grdTableroControl.MasterTableView.Columns[0].Visible = false;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string vComentarios = ObtenerComentarios();

            if (vIdTableroControl != null)
            {
                TableroControlNegocio nTablero = new TableroControlNegocio();
                E_RESULTADO vResultado = nTablero.ActualizaEstatusTablero(vIdTableroControl, vComentarios, vClUsuario, vNbPrograma);
                if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                    btnGuardar.Enabled = false;
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
            }
            
        }

        //protected void rcbComentarios_CheckedChanged(object sender, EventArgs e)
        //{
        //    //if (rcbComentarios.Checked == false)
        //    //{
        //    //    grdTableroControl.MasterTableView.Columns[0].Visible = false;
        //    //    //grdTableroControl.MasterTableView.Columns[0].HeaderStyle.BackColor = Color.DarkGray;
        //    //    //grdTableroControl.MasterTableView.Columns[0].HeaderStyle.ForeColor = Color.White;
        //    //}

        //    //if (rcbComentarios.Checked == true)
        //    //    grdTableroControl.Rebind();
        //}

        protected void Unnamed_PreRender(object sender, EventArgs e)
        {
            GridColumn c = grdTableroControl.MasterTableView.GetColumnSafe("DS_COMENTARIO");
            if (c != null)
            {
                int total = grdTableroControl.MasterTableView.RenderColumns.Count() - 2;
                for (int i = total; i >= 2; i--)
                {
                    grdTableroControl.MasterTableView.RenderColumns[i + 1].OrderIndex = i - 1;
                }
                c.OrderIndex = total + 1;
            }
            grdTableroControl.MasterTableView.Rebind(); 
        }


    }
}