using OfficeOpenXml;
using SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.Negocio.FormacionDesarrollo
{
    public class EventoCapacitacionNegocio
    {
        public List<E_EVENTO> ObtieneEventos(int? ID_EVENTO = null, int? ID_PROGRAMA = null, int? ID_CURSO = null, int? ID_INSTRUCTOR = null, int? ID_EMPLEADO_EVALUADOR = null,
                                                                string CL_EVENTO = null, string NB_EVENTO = null, string DS_EVENTO = null, DateTime? FE_INICIO = null, DateTime? FE_TERMINO = null, string NB_CURSO = null,
                                                                string NB_INSTRUCTOR = null, string CL_TIPO_CURSO = null, string CL_ESTADO = null, DateTime? FE_EVALUACION = null, string DS_LUGAR = null, decimal? MN_COSTO_DIRECTO = null, decimal? MN_COSTO_INDIRECTO = null, Guid? FL_EVENTO = null, string CL_TOKEN = null)
        {

            EventoCapacitacionOperaciones op = new EventoCapacitacionOperaciones();
            return op.ObtenerEventos(ID_EVENTO, ID_PROGRAMA, ID_CURSO, ID_INSTRUCTOR, ID_EMPLEADO_EVALUADOR, CL_EVENTO, NB_EVENTO, DS_EVENTO, FE_INICIO, FE_TERMINO, NB_CURSO, NB_INSTRUCTOR, CL_TIPO_CURSO, CL_ESTADO, FE_EVALUACION, DS_LUGAR, MN_COSTO_DIRECTO, MN_COSTO_INDIRECTO, FL_EVENTO, CL_TOKEN);
        }

        public List<SPE_OBTIENE_EMPLEADOS_Result> ObtenerEmpleados(XElement pXmlSeleccion = null)
        {
            EventoCapacitacionOperaciones oEmpleados = new EventoCapacitacionOperaciones();
            return oEmpleados.ObtenerEmpleados(pXmlSeleccion);
        }

        public E_RESULTADO InsertaActualizaEvento(string tipo_transaccion, E_EVENTO evento, string usuario, string programa)
        {
            EventoCapacitacionOperaciones oEvento = new EventoCapacitacionOperaciones();
            return UtilRespuesta.EnvioRespuesta(oEvento.InsertaActualizaEvento(tipo_transaccion, evento, usuario, programa));
        }

        public E_RESULTADO EliminaEvento(int pIdEvento)
        {
            EventoCapacitacionOperaciones oEventoCapacitacion = new EventoCapacitacionOperaciones();
            return UtilRespuesta.EnvioRespuesta(oEventoCapacitacion.EliminaEvento(pIdEvento));
        }

        public E_RESULTADO EliminaEmpleadoPrograma(int pIdPrograma, int pIdEmpleado)
        {
            EventoCapacitacionOperaciones oEventoCapacitacion = new EventoCapacitacionOperaciones();
            return UtilRespuesta.EnvioRespuesta(oEventoCapacitacion.EliminaEmpleadoPrograma(pIdPrograma, pIdEmpleado));
        }

        public List<E_EVENTO_PARTICIPANTE> ObtieneParticipanteEvento(int? ID_EVENTO_PARTICIPANTE = null, int? ID_EVENTO = null, int? ID_EMPLEADO = null, string CL_PARTICIPANTE = null, string NB_PARTICIPANTE = null, string NB_PUESTO = null, string NB_DEPARTAMENTO = null, int? NO_TIEMPO = null, decimal? PR_CUMPLIMIENTO = null, int? pID_ROL = null)
        {
            EventoCapacitacionOperaciones op = new EventoCapacitacionOperaciones();
            return op.ObtenerParticipanteEvento(ID_EVENTO_PARTICIPANTE, ID_EVENTO, ID_EMPLEADO, CL_PARTICIPANTE, NB_PARTICIPANTE, NB_PUESTO, NB_DEPARTAMENTO, NO_TIEMPO, PR_CUMPLIMIENTO, pID_ROL);
        }

        public List<E_EVENTO_CALENDARIO> ObtieneEventoCalendario(int? ID_EVENTO_CALENDARIO = null, int? ID_EVENTO = null, DateTime? FE_INICIAL = null, DateTime? FE_FINAL = null, int? NO_HORAS = null)
        {
            EventoCapacitacionOperaciones oEvento = new EventoCapacitacionOperaciones();
            return oEvento.ObtenerEventoCalendario(ID_EVENTO_CALENDARIO, ID_EVENTO, FE_INICIAL, FE_FINAL, NO_HORAS);
        }

        public E_RESULTADO ActualizaEventoCalendario(string XML_PARTICIPANTES, string CL_USUARIO, string NB_PROGRAMA)
        {
            EventoCapacitacionOperaciones op = new EventoCapacitacionOperaciones();
            return UtilRespuesta.EnvioRespuesta(op.ActualizarEventoCalendario(XML_PARTICIPANTES, CL_USUARIO, NB_PROGRAMA));
        }

        public List<SPE_OBTIENE_EVENTO_PARTICIPANTE_COMPETENCIA_Result> ObtieneEventoParticipanteCompetencia(int? ID_EVENTO_PARTICIPANTE_COMPETENCIA = null, int? ID_EVENTO = null, int? ID_PARTICIPANTE = null, int? ID_COMPETENCIA = null, byte? NO_EVALUACION = null, string NB_COMPETENCIA = null, int? ID_EMPRESA = null, int? pID_ROL = null)
        {
            EventoCapacitacionOperaciones oEvento = new EventoCapacitacionOperaciones();
            return oEvento.ObtenerEventoParticipanteCompetencia(ID_EVENTO_PARTICIPANTE_COMPETENCIA, ID_EVENTO, ID_PARTICIPANTE, ID_COMPETENCIA, NO_EVALUACION, NB_COMPETENCIA, ID_EMPRESA, pID_ROL);
        }

        public E_RESULTADO ActualizaEvaluacionCompetencias(string pXmlEvaluacion, string pClUsuario, string pNbPrograma)
        {
            EventoCapacitacionOperaciones oEvento = new EventoCapacitacionOperaciones();
            return UtilRespuesta.EnvioRespuesta(oEvento.ActualizarEvaluacionCompetencias(pXmlEvaluacion, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO EliminaEventoParticipante(int pIdEventoParticipante)
        {
            EventoCapacitacionOperaciones oEvento = new EventoCapacitacionOperaciones();
            return UtilRespuesta.EnvioRespuesta(oEvento.EliminarEventoParticipante(pIdEventoParticipante));
        }

        public E_RESULTADO EliminaEventoCalendario(int pIdEventoCalendario)
        {
            EventoCapacitacionOperaciones op = new EventoCapacitacionOperaciones();
            return UtilRespuesta.EnvioRespuesta(op.EliminarEventoCalendario(pIdEventoCalendario));
        }

        public UDTT_ARCHIVO ListaAsistencia(int pIdEvento, int? pIdRol)
        {
            EventoCapacitacionOperaciones op = new EventoCapacitacionOperaciones();
            UDTT_ARCHIVO excelListaAsistencia = new UDTT_ARCHIVO();
            SPE_OBTIENE_EVENTO_LISTA_ASISTENCIA_Result oDatos = op.ObtenerDatosListaAsistencia(pIdEvento, pIdRol);

            Stream newStream = new MemoryStream();

            int vRow;
            int vCol;

            if (oDatos.XML_EVENTO_PARTICIPANTE != null)
            {
                using (ExcelPackage pck = new ExcelPackage(newStream))
                {
                    var ws = pck.Workbook.Worksheets.Add("ListaAsistencia");
                    ws.View.ShowGridLines = false;

                    //Agregamos los datos generales del evento
                    XElement xmlEvento = XElement.Parse(oDatos.XML_EVENTO);

                    ws.Cells["A3"].Value = "Clave del evento:";
                    ws.Cells["B3"].Value = UtilXML.ValorAtributo<string>(xmlEvento.Attribute("CL_EVENTO"));
                    ws.Cells["A4"].Value = "Descripción:";
                    ws.Cells["B4"].Value = UtilXML.ValorAtributo<string>(xmlEvento.Attribute("NB_EVENTO"));
                    ws.Cells["A5"].Value = "Nombre del curso:";
                    ws.Cells["B5"].Value = UtilXML.ValorAtributo<string>(xmlEvento.Attribute("NB_CURSO"));
                    ws.Cells["A6"].Value = "Nombre del instructor:";
                    ws.Cells["B6"].Value = UtilXML.ValorAtributo<string>(xmlEvento.Attribute("NB_INSTRUCTOR"));


                    //agregamos los datos de los participantes del evento

                    if (oDatos.XML_EVENTO_PARTICIPANTE != null)
                    {

                        XElement xmlParticipantes = XElement.Parse(oDatos.XML_EVENTO_PARTICIPANTE);

                        vRow = 13;
                        vCol = 1;

                        foreach (XElement item in xmlParticipantes.Elements("PARTICIPANTE"))
                        {
                            ws.Cells[vRow, vCol].Value = UtilXML.ValorAtributo<string>(item.Attribute("CL_PARTICIPANTE"));
                            vCol++;
                            ws.Cells[vRow, vCol].Value = UtilXML.ValorAtributo<string>(item.Attribute("NB_PARTICIPANTE"));
                            vCol++;
                            ws.Cells[vRow, vCol].Value = UtilXML.ValorAtributo<string>(item.Attribute("NB_PUESTO"));
                            //vCol++;
                            //ws.Cells[vRow, vCol].Value = UtilXML.ValorAtributo<string>(item.Attribute("NB_DEPARTAMENTO"));

                            vCol = 1;
                            vRow++;
                        }

                        ws.Column(1).BestFit = true;
                        ws.Column(1).AutoFit();
                        ws.Column(2).BestFit = true;
                        ws.Column(2).AutoFit();
                        ws.Column(3).BestFit = true;
                        ws.Column(3).AutoFit();
                        ws.Column(4).BestFit = true;
                        ws.Column(4).AutoFit();
                        ws.Column(5).BestFit = true;

                         XElement xmlCalendario = null ;
                        //Agregamos los datos del calendario del evento
                        if(oDatos.XML_EVENTO_CALENDARIO != null)
                        xmlCalendario = XElement.Parse(oDatos.XML_EVENTO_CALENDARIO);

                        vRow = 11;
                        vCol = 4;

                        //OfficeOpenXml.Style.ExcelStyle oEstilo = new OfficeOpenXml.Style.ExcelStyle();
                        if (xmlCalendario != null)
                        {
                            foreach (XElement item in xmlCalendario.Elements("FECHA"))
                            {
                                DateTime vFeInicial, vFeFinal;

                                vFeInicial = UtilXML.ValorAtributo<DateTime>(item.Attribute("FE_INICIAL"));
                                vFeFinal = UtilXML.ValorAtributo<DateTime>(item.Attribute("FE_FINAL"));

                                ws.Cells[vRow, vCol].Value = vFeInicial.ToString("dd/MM/yyyy");
                                asignarEstiloCelda(ws.Cells[vRow, vCol]);
                                vRow++;
                                ws.Cells[vRow, vCol].Value = "De " + vFeInicial.ToString("hh:mm") + " a " + vFeFinal.ToString("hh:mm");
                                asignarEstiloCelda(ws.Cells[vRow, vCol]);


                                ws.Column(vCol).BestFit = true;
                                ws.Column(vCol).AutoFit();

                                vRow = 11;
                                vCol++;
                            }
                        }

                            //Agregamos los encabezados de la tabla
                            asignarEstiloCelda(ws.Cells["A11:A12"]);
                            ws.Cells["A11:A12"].Merge = true;
                            ws.Cells["A11"].Value = "Clave Empleado";

                            asignarEstiloCelda(ws.Cells["B11:B12"]);
                            ws.Cells["B11:B12"].Merge = true;
                            ws.Cells["B11"].Value = "Nombre de participante";

                            asignarEstiloCelda(ws.Cells["C11:C12"]);
                            ws.Cells["C11:C12"].Merge = true;
                            ws.Cells["C11"].Value = "Puesto";

                            //asignarEstiloCelda(ws.Cells["D11:D12"]);
                            //ws.Cells["D11:D12"].Merge = true;
                            //ws.Cells["D11"].Value = "Departamento";

                            vCol--;
                            if (xmlCalendario != null)
                            {
                                asignarEstiloCelda(ws.Cells[10, 4, 10, vCol]);
                                ws.Cells[10, 4, 10, vCol].Merge = true;
                                ws.Cells[10, 4].Value = "Fechas/Firmas";

                                vRow = 13;
                                vCol = 4;

                                int noTotalFechas = xmlCalendario.Elements("FECHA").Count() - 1;
                                int noTotalParticipantes = xmlParticipantes.Elements("PARTICIPANTE").Count() - 1;
                                noTotalParticipantes = vRow + noTotalParticipantes;

                                for (vRow = 13; vRow <= noTotalParticipantes; vRow++)
                                {
                                    ws.Cells[vRow, vCol, vRow, (vCol + noTotalFechas)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                    ws.Cells[vRow, vCol, vRow, (vCol + noTotalFechas)].Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);

                                    ws.Cells[vRow, vCol, vRow, (vCol + noTotalFechas)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                    ws.Cells[vRow, vCol, vRow, (vCol + noTotalFechas)].Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);

                                    ws.Cells[vRow, vCol, vRow, (vCol + noTotalFechas)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                    ws.Cells[vRow, vCol, vRow, (vCol + noTotalFechas)].Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                                }
                            }
      

                        pck.Save();
                        newStream = pck.Stream;
                    }
                }
            }
            excelListaAsistencia.NB_ARCHIVO = "ListaAsistencia.xlsx";
            excelListaAsistencia.FI_ARCHIVO = ((MemoryStream)newStream).ToArray();

            return excelListaAsistencia;
        }

        private void asignarEstiloCelda(ExcelRange celda)
        {
            celda.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            celda.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            celda.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            celda.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            celda.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Lavender);
        }

        public List<SPE_OBTIENE_FYD_REPORTE_RESULTADOS_EVENTO_Result> ObtieneReporteResultadosEvento(int pIdEvento, int? vIdRol)
        {
            EventoCapacitacionOperaciones oEventoCapacitacion = new EventoCapacitacionOperaciones();
            return oEventoCapacitacion.ObtenerReporteResultadosEvento(pIdEvento, vIdRol);
        }

        public List<E_EVENTO_PARTICIPANTE_COMPETENCIA> ObtieneReporteResultadosEventoDetalle(int pIdEvento)
        {
            EventoCapacitacionOperaciones oEvento = new EventoCapacitacionOperaciones();
            return oEvento.ObtenerReporteResultadosEventoDetalle(pIdEvento);
        }

        public string ObtieneCampoAdicionalXml(String CL_TABLA_REFERENCIA = null)
        {
            EventoCapacitacionOperaciones oEvento = new EventoCapacitacionOperaciones();
            return oEvento.ObtenerCamposAdicionalesXml(CL_TABLA_REFERENCIA);
        }
    }
}
