using OfficeOpenXml;
using SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.Negocio.FormacionDesarrollo
{
    public class NecesidadesCapacitacionNegocio
    {
        public List<SPE_OBTIENE_NECESIDADES_CAPACITACION_Result> obtenerNecesidadesCapacitacion(int? pIdPeriodo, int? pIdDepartamento, string pDsPrioridades)
        {
            NecesidadesCapacitacionOperaciones oNecesidades = new NecesidadesCapacitacionOperaciones();
            return oNecesidades.obtenerNecesidadesCapacitacion(pIdPeriodo, pIdDepartamento, pDsPrioridades);
        }

        public E_RESULTADO InsertaProgramaDesdeDNC(string xmldatosDnc, string CL_USUARIO, string NB_PROGRAMA)
        {
            NecesidadesCapacitacionOperaciones ope = new NecesidadesCapacitacionOperaciones();
            return UtilRespuesta.EnvioRespuesta(ope.InsertaProgramaDesdeDNC(xmldatosDnc, CL_USUARIO, NB_PROGRAMA));
        }

        public E_RESULTADO ActualizaProgramaDesdeDNC(int ID_PROGRAMA, string XML_DATOS_DNC, string CL_USUARIO, string NB_PROGRAMA)
        {
            NecesidadesCapacitacionOperaciones op = new NecesidadesCapacitacionOperaciones();
            return UtilRespuesta.EnvioRespuesta(op.ActualizaProgramaDesdeDNC(ID_PROGRAMA, XML_DATOS_DNC, CL_USUARIO, NB_PROGRAMA));
        }

        public E_RESULTADO InsertaActualizaProgramaDesdeDNC(int? ID_PROGRAMA, string XML_DATOS_DNC, string CL_USUARIO, string NB_PROGRAMA)
        {
            NecesidadesCapacitacionOperaciones op = new NecesidadesCapacitacionOperaciones();
            return UtilRespuesta.EnvioRespuesta(op.InsertaActualizaProgramaDesdeDNC(ID_PROGRAMA, XML_DATOS_DNC, CL_USUARIO, NB_PROGRAMA));
        }

        private Boolean ValidarRamaXml(XElement parentEl, string elementsName)
        {
            var foundEl = parentEl.Element(elementsName);
            if (foundEl != null)
            {
                return true;
            }

            return false;
        }

        public UDTT_ARCHIVO ExportarDatosExcel(int? ID_PERIODO, int? ID_DEPARTAMENTO, string NB_DEPARTAMENTO, string DS_PRIORIDADES)
        {
            NecesidadesCapacitacionOperaciones op = new NecesidadesCapacitacionOperaciones();
            PeriodoOperaciones neg = new PeriodoOperaciones();

            List<SPE_OBTIENE_NECESIDADES_CAPACITACION_Result> lista = new List<SPE_OBTIENE_NECESIDADES_CAPACITACION_Result>();
            SPE_OBTIENE_FYD_PERIODO_EVALUACION_Result periodo = new SPE_OBTIENE_FYD_PERIODO_EVALUACION_Result();
            UDTT_ARCHIVO excelNecesidadCapacitacion = new UDTT_ARCHIVO();

            int i = 11;
            int col = 0;
            int row = 0;

            bool FinalListaEmpleados = false;
            bool CompetenciaEncontrada = false;

            string vDsNotas = "";
            string vNbFirstRadEditorTagName = "p";

            periodo = neg.ObtenerPeriodoEvaluacion(ID_PERIODO.Value);
            lista = op.obtenerNecesidadesCapacitacion(ID_PERIODO, ID_DEPARTAMENTO, DS_PRIORIDADES);
            Stream newStream = new MemoryStream();

            using (ExcelPackage pck = new ExcelPackage(newStream))
            {
                var ws = pck.Workbook.Worksheets.Add("Reporte");

                ws.View.ShowGridLines = false;

                //ponemos los encabezados con los datos del periodo
                ws.Cells["A1"].Value = "Periodo:";
                ws.Cells["B1"].Value = periodo.NB_PERIODO;
                ws.Cells["A2"].Value = "Notas:";

                if (periodo.DS_NOTAS != null)
                {
                    XElement vNota = XElement.Parse(periodo.DS_NOTAS);
                    if (vNota != null)
                    {
                        if (vNota.Element("DS_NOTAS") != null)
                        {
                            vDsNotas = vNota.Element("DS_NOTAS").Value;
                        }
                    }
                }

                ws.Cells["B2"].Value = vDsNotas;
                ws.Cells["A3"].Value = "Tipo de evaluacion:";
                ws.Cells["A4"].Value = "Departamento:";
                ws.Cells["B4"].Value = NB_DEPARTAMENTO;

                //Leyendas de colores del archivo de excel
                ws.Cells["D1:F1"].Merge = true;
                ws.Cells["D1"].Value = "Prioridades de Capacitación";
                ws.Cells["D2"].Value = "Alta:";
                ws.Cells["D3"].Value = "Intermedia:";
                ws.Cells["D4"].Value = "No necesaria:";
                ws.Cells["D5"].Value = "No Aplica:";

                ws.Cells["F2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Cells["F3"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Cells["F4"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Cells["F5"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

                ws.Cells["F2"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                ws.Cells["F3"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Gold);
                ws.Cells["F4"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                ws.Cells["F5"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

                //formato de encabezados
                ws.Cells["A7:A10"].Merge = true;
                ws.Cells["B7:B10"].Merge = true;
                ws.Cells["C7:C10"].Merge = true;

                ws.Cells["A7"].Value = "CATEGORIA";
                ws.Cells["B7"].Value = "CLASIFICACION";
                ws.Cells["C7"].Value = "COMPETENCIA";

                ws.Cells["A7"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["A7"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["A7"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Cells["A7"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Lavender);

                ws.Cells["B7"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["B7"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["B7"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Cells["B7"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Lavender);

                ws.Cells["C7"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["C7"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["C7"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Cells["C7"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Lavender);

                //Obtenemos las distintas competencias que se generan en la DNC.
                var competencias = (from a in lista select new { a.NB_CLASIFICACION_COMPETENCIA, a.CL_TIPO_COMPETENCIA, a.NB_COMPETENCIA, a.CL_COLOR }).Distinct();

                foreach (var item in competencias)
                {
                    ws.Cells["A" + i.ToString()].Value = item.CL_TIPO_COMPETENCIA;
                    ws.Cells["B" + i.ToString()].Value = item.NB_CLASIFICACION_COMPETENCIA;
                    ws.Cells["C" + i.ToString()].Value = item.NB_COMPETENCIA;

                    ws.Cells["A" + i.ToString() + ":C" + i.ToString()].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.FromName(item.CL_COLOR));

                    i++;
                }

                ws.Column(1).AutoFit();
                ws.Column(2).AutoFit();
                ws.Column(3).AutoFit();

                //obtenemos todos los empleados
                var empleados = (from a in lista select new { a.CL_DEPARTAMENTO, a.NB_DEPARTAMENTO, a.CL_EVALUADO, a.NB_EVALUADO }).Distinct();
                col = 4;
                foreach (var item in empleados)
                {
                    ws.Cells[7, col].Value = item.CL_DEPARTAMENTO;
                    ws.Cells[8, col].Value = item.NB_DEPARTAMENTO;
                    ws.Cells[9, col].Value = item.CL_EVALUADO;
                    ws.Cells[10, col].Value = item.NB_EVALUADO;

                    col++;
                }

                col = 4;
                row = 11;

                string empleado = "";

                while (!FinalListaEmpleados)
                {
                    var valor = ws.Cells[10, col].Value;

                    if (valor != null)
                    {
                        empleado = valor.ToString();

                        var listaEmpleado = (from a in lista where a.NB_EVALUADO == empleado select new { a.NB_COMPETENCIA, a.PR_RESULTADO, a.NB_PRIORIDAD });

                        foreach (var item in listaEmpleado)
                        {
                            while (!CompetenciaEncontrada)
                            {
                                if (ws.Cells[row, 3].Value.ToString() == item.NB_COMPETENCIA)
                                {
                                    ws.Cells[row, col].Value = item.PR_RESULTADO;
                                    ws.Cells[row, col].Style.Numberformat.Format = "0.00";
                                    ws.Cells[row, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

                                    switch (item.NB_PRIORIDAD)
                                    {
                                        case "Alta":
                                            ws.Cells[row, col].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                                            break;

                                        case "Intermedia":
                                            ws.Cells[row, col].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Gold);
                                            break;

                                        case "No Necesaria":
                                            ws.Cells[row, col].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                else
                                {
                                    if (ws.Cells[row, col].Value == null)
                                    {
                                        ws.Cells[row, col].Value = "N/A";
                                        ws.Cells[row, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                        ws.Cells[row, col].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                                    }
                                    else
                                    {
                                        if (ws.Cells[row, col].Value.ToString() == "N/A")
                                        {
                                            ws.Cells[row, col].Value = "N/A";
                                            ws.Cells[row, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                            ws.Cells[row, col].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                                        }
                                    }

                                }

                                if (ws.Cells[row + 1, 3].Value == null)
                                {
                                    CompetenciaEncontrada = true;
                                }

                                row++;
                            }

                            row = 11;
                            CompetenciaEncontrada = false;
                        }

                        ws.Column(col).AutoFit();
                        col++;
                    }
                    else
                    {
                        FinalListaEmpleados = true;
                    }
                }


                pck.Save();
                newStream = pck.Stream;
            }

            excelNecesidadCapacitacion.NB_ARCHIVO = "ReporteDNC.xlsx";
            excelNecesidadCapacitacion.FI_ARCHIVO = ((MemoryStream)newStream).ToArray();

            return excelNecesidadCapacitacion;
        }

        public DataTable ObtieneNecesidadesCapacitacionPivot(int? pIdPeriodo, int? pIdDepartamento, string pDsPrioridades, ref List<E_NECESIDADES_CAPACITACION> vLstDnc)
        {

            NecesidadesCapacitacionOperaciones oNecesidades = new NecesidadesCapacitacionOperaciones();
            List<SPE_OBTIENE_NECESIDADES_CAPACITACION_Result> vListaDnc = new List<SPE_OBTIENE_NECESIDADES_CAPACITACION_Result>();
            //string vDivsCeldasChk = "<div class=\"divCheckbox\"> <input type=\"checkbox\" runat=\"server\" class=\"{4}\" id=\"{2}\" value=\"{2}\" {3}> </div>  <div class=\"divPorcentaje\">{0:N2}</div><div class=\"{1}\">&nbsp;</div>";
            string vNbPorcentaje = "";
            string vDivsCeldasChk = "<table class=\"tabladnc\"> " +
                "<tr> " +
                "<td class=\"porcentaje\"> " +
                "<div class=\"divPorcentaje\">{0}</div> " +
                "</td> " +
                "<td class=\"color\"> " +
                "<div class=\"{1}\">&nbsp;</div> " +
                "</td> " +
                "<td class=\"check\"> " +
                "<div class=\"divCheckbox\"> <input type=\"checkbox\" runat=\"server\" class=\"{4}\" id=\"{2}\" value=\"{2}\" {3}> </div> " +
                "</td> </tr> </table>";
            //string vDivsCeldasNa = "<div class=\"divPorcentaje\">{0:N2}</div><div class=\"{1}\">&nbsp;</div>";

            string vDivCeldaCompetencia = "<div style=\" width:85%;float:left;\">{0}</div><divstyle=\"float:right; width:15%;\"> <input type=\"checkbox\" class=\"competencia\" runat=\"server\" id=\"{1}\" value=\"{1}\" onchange=\"SeleccionaCompetencia(this);\"> </div>";

            string vClaseDivs = "";
            string vClaveChk = "";
            string vClaseInput = "";
            string vFgInput = "";
            //<telerik:RadCheckBox runat="server" ID="chkComEmp" Text="" Checked="true" AutoPostBack="false"></telerik:RadCheckBox>
            //
            DataTable vDtPivot = new DataTable();

            vListaDnc = oNecesidades.obtenerNecesidadesCapacitacion(pIdPeriodo, pIdDepartamento, pDsPrioridades);

            vLstDnc = vListaDnc.Select(t => new E_NECESIDADES_CAPACITACION
            {
                ID_PERIODO = t.ID_PERIODO,
                CL_TIPO_COMPETENCIA = t.CL_TIPO_COMPETENCIA,
                NB_TIPO_COMPETENCIA = t.NB_TIPO_COMPETENCIA,
                CL_CLASIFICACION = t.CL_CLASIFICACION,
                NB_CLASIFICACION_COMPETENCIA = t.NB_CLASIFICACION_COMPETENCIA,
                DS_COMPETENCIA = t.DS_COMPETENCIA,
                CL_COLOR = t.CL_COLOR,
                ID_COMPETENCIA = t.ID_COMPETENCIA,
                NB_COMPETENCIA = t.NB_COMPETENCIA,
                ID_EMPLEADO = t.ID_EMPLEADO,
                CL_EVALUADO = t.CL_EVALUADO,
                NB_EVALUADO = t.NB_EVALUADO,
                ID_PUESTO = t.ID_PUESTO,
                CL_PUESTO = t.CL_PUESTO,
                NB_PUESTO = t.NB_PUESTO,
                ID_DEPARTAMENTO = t.ID_DEPARTAMENTO,
                CL_DEPARTAMENTO = t.CL_DEPARTAMENTO,
                NB_DEPARTAMENTO = t.NB_DEPARTAMENTO,
                PR_RESULTADO = t.PR_RESULTADO,
                NB_PRIORIDAD = t.NB_PRIORIDAD
            }).ToList();

            //Creamos las primeras columnas del pivot

            vDtPivot.Columns.Add("ID_COMPETENCIA", typeof(int));
            vDtPivot.Columns.Add("NB_TIPO_COMPETENCIA", typeof(string));
            vDtPivot.Columns.Add("NB_CLASIFICACION_COMPETENCIA", typeof(string));
            vDtPivot.Columns.Add("NB_COMPETENCIA", typeof(string));

            //Obtenemos la lista de empleados y generamos las columnas
            var vLstEmpleados = (from a in vListaDnc select new { a.ID_EMPLEADO }).Distinct().OrderBy(t => t.ID_EMPLEADO);
            var vLstCompetencias = (from a in vListaDnc select new { a.ID_COMPETENCIA, a.NB_TIPO_COMPETENCIA, a.NB_CLASIFICACION_COMPETENCIA, a.CL_TIPO_COMPETENCIA, a.NB_COMPETENCIA, a.CL_COLOR }).Distinct().OrderBy(t => t.ID_COMPETENCIA);

            foreach (var item in vLstEmpleados)
            {
                vDtPivot.Columns.Add(item.ID_EMPLEADO.ToString() + "E");
            }

            foreach (var vCom in vLstCompetencias)
            {
                DataRow vDr = vDtPivot.NewRow();

                vDr["ID_COMPETENCIA"] = vCom.ID_COMPETENCIA;
                vDr["NB_TIPO_COMPETENCIA"] = vCom.NB_TIPO_COMPETENCIA;
                vDr["NB_CLASIFICACION_COMPETENCIA"] = vCom.NB_CLASIFICACION_COMPETENCIA;
                vDr["NB_COMPETENCIA"] = string.Format(vDivCeldaCompetencia, vCom.NB_COMPETENCIA, vCom.ID_COMPETENCIA.ToString());

                foreach (var vEmp in vLstEmpleados)
                {
                    var vResultado = vListaDnc.Where(t => t.ID_COMPETENCIA == vCom.ID_COMPETENCIA & t.ID_EMPLEADO == vEmp.ID_EMPLEADO).FirstOrDefault();
                    vClaveChk = "C" + vCom.ID_COMPETENCIA.ToString() + "E" + vEmp.ID_EMPLEADO.ToString();

                    if (vResultado != null)
                    {

                        switch (vResultado.NB_PRIORIDAD)
                        {
                            case "Alta":
                                vClaseDivs = "divNecesario";
                                vClaseInput = "Datos";
                                vFgInput = "checked";
                                break;

                            case "Intermedia":
                                vClaseDivs = "divIntermedio";
                                vClaseInput = "Datos";
                                vFgInput = "checked";
                                break;

                            case "No Necesaria":
                                vClaseDivs = "divBajo";
                                vClaseInput = "NoNecesaria";
                                vFgInput = "";
                                break;
                        }


                        vNbPorcentaje = string.Format("{0:N2}", vResultado.PR_RESULTADO) + "%";
                        vDr[vEmp.ID_EMPLEADO.ToString()+"E"] = String.Format(vDivsCeldasChk, vNbPorcentaje, vClaseDivs, vClaveChk, vFgInput, vClaseInput);
                    }
                    else
                    {
                        vClaseInput = "NA";
                        vDr[vEmp.ID_EMPLEADO.ToString()+"E"] = String.Format(vDivsCeldasChk, "N/A", "divNa", vClaveChk, "", vClaseInput);
                    }
                }

                vDtPivot.Rows.Add(vDr);
            }

            return vDtPivot;
        }

    }
}
