using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades.IntegracionDePersonal;
using System.Data;
using SIGE.Entidades.Externas;
using OfficeOpenXml;
using System.IO;

namespace SIGE.Negocio.Administracion
{
    public class ConsultaPersonalNegocio
    {
        public List<SPE_OBTIENE_CONSULTA_PERSONAL_RESUMEN_Result> obtieneConsultaPersonalResumen(int ID_BATERIA, bool vFgConsultaparcial)
        {
            ConsultaPersonalOperaciones op = new ConsultaPersonalOperaciones();
            return op.obtieneConsultaPersonalResumen(ID_BATERIA, vFgConsultaparcial);
        }

        public UDTT_ARCHIVO obtieneConsultaPersonalResumenDT(int pIdBateria, bool vFgConsultaParcial, string pNbCandidato, string pClFolio)
        {

            ConsultaPersonalOperaciones op = new ConsultaPersonalOperaciones();
            UDTT_ARCHIVO oConsultaPersonal = new UDTT_ARCHIVO();
            Stream newStream = new MemoryStream();
            Utilerias.Utilerias aux = new Utilerias.Utilerias();

            int vFila = 9;
            string vClCelda = "";
            string vNbCategoria = "";
            decimal? vPromedioBaremos = 0;

            //Obtenemos los datos y los pasamos a un DataTable
            List<SPE_OBTIENE_CONSULTA_PERSONAL_RESUMEN_Result> oLista = op.obtieneConsultaPersonalResumen(pIdBateria, vFgConsultaParcial);

            vPromedioBaremos = oLista.Average(t => t.NO_BAREMO_PORCENTAJE);

            //Creamos el archivo de excel
            using (ExcelPackage pck = new ExcelPackage(newStream))
            {
                var ws = pck.Workbook.Worksheets.Add("ConsultaPersonalResumida");

                ws.Column(1).Width = 50;
                ws.Column(2).Width = 50;
                ws.Column(4).Width = 50;

                ws.Cells["A3"].Value = "Consulta personal resumida";
                ws.Cells["A3"].Style.Font.Size = 18;
                ws.Cells["A3"].Style.Font.Color.SetColor(System.Drawing.Color.IndianRed);

                ws.Cells["A5"].Value = "Solicitud: " + pClFolio + "     Aplicante: " + pNbCandidato;
                ws.Cells["A5"].Style.Font.Bold = true;
                asignarEstiloCelda(ws.Cells["A5:B5"], "PowderBlue", false);

                ws.Cells["A7"].Value = "Competencia";
                asignarEstiloCelda(ws.Cells["A7"], "PowderBlue");

                ws.Cells["B7"].Value = "Descripción";
                asignarEstiloCelda(ws.Cells["B7"], "PowderBlue");

                ws.Cells["C7"].Value = "%";
                asignarEstiloCelda(ws.Cells["C7"], "PowderBlue");

                foreach (SPE_OBTIENE_CONSULTA_PERSONAL_RESUMEN_Result item in oLista)
                {
                    //aquí verificamos la primer entrada del ciclo
                    if (string.IsNullOrEmpty(vNbCategoria))
                    {
                        vClCelda = "A" + vFila.ToString() + ":D" + vFila.ToString();
                        ws.Cells[vClCelda].Merge = true;

                        vClCelda = "A" + vFila.ToString();
                        ws.Cells[vClCelda].Value = item.CL_CLASIFICACION;
                        vNbCategoria = item.CL_CLASIFICACION;

                        vClCelda = "A" + vFila.ToString() + ":D" + vFila.ToString();
                        asignarEstiloCelda(ws.Cells[vClCelda], item.CL_COLOR);
                        vFila++;

                    }


                    //Ahora, verificamos que la clasificación sea diferente, esto para el cambio de clasificación
                    if (!vNbCategoria.Equals(item.CL_CLASIFICACION))
                    {
                        vClCelda = "A" + vFila.ToString() + ":D" + vFila.ToString();
                        ws.Cells[vClCelda].Merge = true;

                        vClCelda = "A" + vFila.ToString();
                        ws.Cells[vClCelda].Value = item.CL_CLASIFICACION;
                        vNbCategoria = item.CL_CLASIFICACION;

                        vClCelda = "A" + vFila.ToString() + ":D" + vFila.ToString();
                        asignarEstiloCelda(ws.Cells[vClCelda], item.CL_COLOR);
                        vFila++;
                    }

                    ws.Row(vFila).Style.WrapText = true;

                    //Si no entró a ninguna de las anteriores, ponemos los datos de la competencía
                    vClCelda = "A" + vFila.ToString();
                    ws.Cells[vClCelda].Value = item.NB_COMPETENCIA;
                    asignarEstiloCelda(ws.Cells[vClCelda], item.CL_COLOR);

                    vClCelda = "B" + vFila.ToString();
                    ws.Cells[vClCelda].Value = item.DS_COMPETENCIA;
                    asignarEstiloCelda(ws.Cells[vClCelda], item.CL_COLOR);

                    vClCelda = "C" + vFila.ToString();
                    ws.Cells[vClCelda].Value = item.NO_BAREMO_PORCENTAJE.Value.ToString("N2") + "%";
                    asignarEstiloCelda(ws.Cells[vClCelda], alineacion: OfficeOpenXml.Style.ExcelHorizontalAlignment.Right);

                    vClCelda = "D" + vFila.ToString();
                    ws.Cells[vClCelda].Value = item.DS_NIVEL_COMPETENCIA_PERSONA;
                    asignarEstiloCelda(ws.Cells[vClCelda]);

                    vFila++;
                }

                vFila++;

                vClCelda = "A" + vFila.ToString();
                ws.Cells[vClCelda].Value = "Total de compatibilidad de competencias: " + vPromedioBaremos.Value.ToString("N2") + "%";
                ws.Cells[vClCelda].Style.Font.Bold = true;

                pck.Save();
                newStream = pck.Stream;
            }

            oConsultaPersonal.NB_ARCHIVO = "ConsultaPersonalResumida.xlsx";
            oConsultaPersonal.FI_ARCHIVO = ((MemoryStream)newStream).ToArray();

            return oConsultaPersonal;

        }

        private void asignarEstiloCelda(ExcelRange celda, string color = null, bool border = true, OfficeOpenXml.Style.ExcelHorizontalAlignment alineacion = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left)
        {

            if (border)
            {
                celda.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            }

            if (!string.IsNullOrEmpty(color))
            {
                celda.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                celda.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromName(color));
            }

            celda.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
            celda.Style.HorizontalAlignment = alineacion;
        }

        public DataTable obtieneConsultaPersonalDetallada(int ID_BATERIA, ref List<E_CONSULTA_DETALLE> vLstDetallada)
        {
            ConsultaPersonalOperaciones op = new ConsultaPersonalOperaciones();
            List<SPE_OBTIENE_CONSULTA_PERSONAL_DETALLADA_Result> vListaDetallada = new List<SPE_OBTIENE_CONSULTA_PERSONAL_DETALLADA_Result>();
            List<SPE_OBTIENE_FACTORES_CONSULTA_Result> vListaFactores = new List<SPE_OBTIENE_FACTORES_CONSULTA_Result>();
            List<SPE_OBTIENE_COMPETENCIAS_CONSULTA_Result> vListaCompetencias = new List<SPE_OBTIENE_COMPETENCIAS_CONSULTA_Result>();
            List<SPE_OBTIENE_FACTORES_COMPETENCIAS_Result> vListaFactoresCompetencias = new List<SPE_OBTIENE_FACTORES_COMPETENCIAS_Result>();
            
            //Se agregan los dos lineas siguientes para obtener las variables baremos sin relacionar con competencias
            PruebasNegocio pruebas = new PruebasNegocio();
            var vBaremos = pruebas.obtenerVariableBaremos(ID_BATERIA);
            //-------------------------------------------------------------------------------------------------------

            DataTable vDtPivot = new DataTable();
            string vClaseColor = "";
            string vImagen = "";
            vListaFactores = op.obtieneFactoresConsulta();
            vListaCompetencias = op.obtieneCompetenciasConsulta();
            vListaFactoresCompetencias = op.obtieneFactoresCompetencias();

            vListaDetallada = op.obtieneConsultaPersonalDetallada(ID_BATERIA);
            vLstDetallada = vListaDetallada.Select(s => new E_CONSULTA_DETALLE
             {
                 CL_CLASIFICACION = s.CL_CLASIFICACION,
                 CL_COLOR = s.CL_COLOR,
                 CL_FACTOR = s.CL_FACTOR,
                 CL_TIPO_COMPETENCIA = s.CL_TIPO_COMPETENCIA,
                 CL_VARIABLE = s.CL_VARIABLE,
                 DS_COMPETENCIA = s.DS_COMPETENCIA,
                 DS_FACTOR = s.DS_FACTOR,
                 ID_FACTOR = s.ID_FACTOR,
                 ID_COMPETENCIA = s.ID_COMPETENCIA,
                 ID_VARIABLE = s.ID_VARIABLE,
                 NO_VALOR = s.NO_VALOR,
                 NB_FACTOR = s.NB_FACTOR,
                 NB_ABREVIATURA = s.NB_ABREVIATURA,
                 NB_COMPETENCIA = s.NB_COMPETENCIA,
                 NB_PRUEBA = s.NB_PRUEBA,
                 NB_CLASIFICACION_COMPETENCIA = s.NB_CLASIFICACION_COMPETENCIA
             }).ToList();

            vDtPivot.Columns.Add("ID_COMPETENCIA", typeof(int));
            vDtPivot.Columns.Add("CL_COLOR", typeof(string));
            //vDtPivot.Columns.Add("NB_CLASIFICACION_COMPETENCIA", typeof(string));
            vDtPivot.Columns.Add("NB_COMPETENCIA", typeof(string));

            //var vLstFactores = (from a in vListaDetallada select new {  a.ID_FACTOR }).Distinct().OrderBy(t => t.ID_FACTOR);
            //var vLstCompetencias = (from a in vListaDetallada
            //                        select new
            //                        {
            //                            a.ID_COMPETENCIA,
            //                            a.CL_COLOR,
            //                          //  a.CL_CLASIFICACION,
            //                            a.NB_COMPETENCIA,
            //                            a.DS_COMPETENCIA,
            //                            a.NB_CLASIFICACION_COMPETENCIA,
            //                        }).Distinct().OrderBy(t => t.NB_CLASIFICACION_COMPETENCIA);
            //                       // }).Distinct().OrderBy(t => t.CL_CLASIFICACION);

            foreach (var item in vListaFactores)
            {
                vDtPivot.Columns.Add(item.ID_FACTOR.ToString() + "E");
            }

            foreach (var vCom in vListaCompetencias)
            {
                DataRow vDr = vDtPivot.NewRow();

                vDr["ID_COMPETENCIA"] = vCom.ID_COMPETENCIA;
                vClaseColor = string.Format(vCom.CL_COLOR);
                vDr["CL_COLOR"] = "<div style=\"height: 80%; width: 20px;border-radius: 5px;  background:" + vClaseColor + " ;\" ><br><br></div>";
                //vDr["NB_CLASIFICACION_COMPETENCIA"] = vCom.NB_CLASIFICACION_COMPETENCIA;
                vDr["NB_COMPETENCIA"] = vCom.NB_COMPETENCIA;

                foreach (var vFac in vListaFactores)
                {
                    var vResultado = vListaDetallada.Where(t => t.ID_COMPETENCIA == vCom.ID_COMPETENCIA & t.ID_FACTOR == vFac.ID_FACTOR).FirstOrDefault();
                    if (vResultado != null)
                    {
                        if (vFac.DS_FACTOR == "TIVA")
                        {
                            decimal vTvTotal = Math.Round(vListaDetallada.Where(w => w.CL_VARIABLE == "TV-TOTAL").Select(s => s.NO_VALOR).FirstOrDefault(), 0);
                            decimal vPersonal = Math.Round(vListaDetallada.Where(w => w.CL_VARIABLE == "TV-PERSONAL").Select(s => s.NO_VALOR).FirstOrDefault(), 0);
                            decimal vReglamento = Math.Round(vListaDetallada.Where(w => w.CL_VARIABLE == "TV-LEYES Y REGLAMENTOS").Select(s => s.NO_VALOR).FirstOrDefault(), 0);
                            decimal vEtica = Math.Round(vListaDetallada.Where(w => w.CL_VARIABLE == "TV-INTEGRIDAD Y ÉTICA LABORAL").Select(s => s.NO_VALOR).FirstOrDefault(), 0);
                            decimal vCivica = Math.Round(vListaDetallada.Where(w => w.CL_VARIABLE == "TV-CÍVICA").Select(s => s.NO_VALOR).FirstOrDefault(), 0);
                            decimal vResBaremos = 0;
                            decimal vErrores = 0;
                            //foreach (var item in vListaDetallada)
                            foreach (var item in vBaremos) // Se cambia la lista por vBaremos 21/06/2018
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

                            vErrores = 4 - vResBaremos;
                            if (vErrores >= 2)
                            {
                                vDr[vFac.ID_FACTOR.ToString() + "E"] = "<span><img title='" + vFac.NB_FACTOR + "(" + vFac.NB_PRUEBA + ")' src='../Assets/images/Baremos" + "Invalido" + ".png' /></span>";
                            }
                            else
                            {
                                if (vResultado != null)
                                {
                                    int vNum = (int)vResultado.NO_VALOR;
                                    switch (vNum)
                                    {
                                        case 1:
                                            vImagen = "Rojo";
                                            break;

                                        case 2:
                                            vImagen = "Amarillo";
                                            break;

                                        case 3:
                                            vImagen = "Verde";
                                            break;
                                        default:
                                            vImagen = "Gris";
                                            break;
                                    }

                                    vDr[vFac.ID_FACTOR.ToString() + "E"] = "<span><img title='" + vFac.NB_FACTOR + "(" + vFac.NB_PRUEBA + ")' src='../Assets/images/Baremos" + vImagen + ".png' /></span>";
                                }
                            }
                        }
                        else
                        {
                            int vNum = (int)vResultado.NO_VALOR;
                            switch (vNum)
                            {
                                case 1:
                                    vImagen = "Rojo";
                                    break;

                                case 2:
                                    vImagen = "Amarillo";
                                    break;

                                case 3:
                                    vImagen = "Verde";
                                    break;
                                default:
                                    vImagen = "Gris";
                                    break;
                            }

                            vDr[vFac.ID_FACTOR.ToString() + "E"] = "<span><img title='" + vFac.NB_FACTOR + "(" + vFac.NB_PRUEBA + ")' src='../Assets/images/Baremos" + vImagen + ".png' /></span>";
                        }
                    }
                    else
                    {
                        var vBuscarResultado = vListaFactoresCompetencias.Where(t => t.ID_COMPETENCIA == vCom.ID_COMPETENCIA & t.ID_FACTOR == vFac.ID_FACTOR).FirstOrDefault();
                        if (vBuscarResultado != null)
                        {
                            vImagen = "Gris";
                            vDr[vFac.ID_FACTOR.ToString() + "E"] = "<span><img title='" + vFac.NB_FACTOR + "(" + vFac.NB_PRUEBA + ")'  src='../Assets/images/Baremos" + vImagen + ".png' /></span>";
                        }
                    }
                }

                vDtPivot.Rows.Add(vDr);
            }
            return vDtPivot;
        }

        public List<SPE_OBTIENE_C_FACTOR_Result> obtieneFactores(int? ID_FACTOR = null, string CL_FACTOR = null, string NB_FACTOR = null, string DS_FACTOR = null, int? ID_VARIABLE = null, string NB_ABREVIATURA = null)
        {
            ConsultaPersonalOperaciones op = new ConsultaPersonalOperaciones();
            return op.obtieneFactores(ID_FACTOR, CL_FACTOR, NB_FACTOR, DS_FACTOR, ID_VARIABLE, NB_ABREVIATURA);
        }

        public List<SPE_OBTIENE_FACTORES_CONSULTA> obtenerFactoresConsulta()
        {
            ConsultaPersonalOperaciones op = new ConsultaPersonalOperaciones();
            var vFactores = op.obtieneFactoresConsulta().ToList();
            return (from x in vFactores
                    select new SPE_OBTIENE_FACTORES_CONSULTA
                                {
                                    ID_FACTOR = x.ID_FACTOR,
                                    CL_FACTOR = x.CL_FACTOR,
                                    NB_FACTOR = x.NB_FACTOR,
                                    DS_FACTOR = x.DS_FACTOR,
                                    ID_VARIABLE = x.ID_VARIABLE,
                                    NB_ABREVIATURA = x.NB_ABREVIATURA,
                                    CL_VARIABLE = x.CL_VARIABLE,
                                    NB_PRUEBA = x.NB_PRUEBA,
                                    CL_TIPO_VARIABLE = x.CL_TIPO_VARIABLE,
                                }
                   ).ToList();


        }

        public List<SPE_OBTIENE_COMPETENCIAS_CONSULTA> obtenerCompetenciasConsulta()
        {
            ConsultaPersonalOperaciones op = new ConsultaPersonalOperaciones();
            var vCompetencias = op.obtieneCompetenciasConsulta().ToList();

            return (from x in vCompetencias
                    select new SPE_OBTIENE_COMPETENCIAS_CONSULTA
                    {
                        ID_COMPETENCIA = x.ID_COMPETENCIA,
                        CL_COMPETENCIA = x.CL_COMPETENCIA,
                        NB_COMPETENCIA = x.NB_COMPETENCIA,
                        DS_COMPETENCIA = x.DS_COMPETENCIA,
                        CL_TIPO_COMPETENCIA = x.CL_TIPO_COMPETENCIA,
                        CL_CLASIFICACION = x.CL_CLASIFICACION,
                        CL_COLOR = x.CL_COLOR,
                        NB_CLASIFICACION_COMPETENCIA = x.NB_CLASIFICACION_COMPETENCIA
                    }
                    ).ToList();

        }

        public List<SPE_OBTIENE_FACTORES_COMPETENCIAS_Result> obtenerFactoresCompetencias()
        {
            ConsultaPersonalOperaciones op = new ConsultaPersonalOperaciones();
            return op.obtieneFactoresCompetencias();
        }

        public UDTT_ARCHIVO obtieneConsultaPersonalDetalladaExcel(int pIdBateria, string pNbCandidato, string pClFolio)
        {
            UDTT_ARCHIVO oConsultaPersonal = new UDTT_ARCHIVO();
            Stream newStream = new MemoryStream();
            Utilerias.Utilerias aux = new Utilerias.Utilerias();

            ConsultaPersonalOperaciones op = new ConsultaPersonalOperaciones();
            List<SPE_OBTIENE_CONSULTA_PERSONAL_DETALLADA_Result> vListaDetallada = new List<SPE_OBTIENE_CONSULTA_PERSONAL_DETALLADA_Result>();
            List<SPE_OBTIENE_FACTORES_CONSULTA_Result> vListaFactores = new List<SPE_OBTIENE_FACTORES_CONSULTA_Result>();
            List<SPE_OBTIENE_COMPETENCIAS_CONSULTA_Result> vListaCompetencias = new List<SPE_OBTIENE_COMPETENCIAS_CONSULTA_Result>();
            List<SPE_OBTIENE_FACTORES_COMPETENCIAS_Result> vListaFactoresCompetencias = new List<SPE_OBTIENE_FACTORES_COMPETENCIAS_Result>();

            //int vFila = 2;
            //int vColumna = 0;

            DataTable vDtPivot = new DataTable();
            DataTable vDtFactoresPruebas = new DataTable();

            string vClaseColor = "";
            string vColorEncabezado = "lightskyblue";
            //string vImagen = "";

            string vClasificacion = "";

            vListaFactores = op.obtieneFactoresConsulta();
            vListaCompetencias = op.obtieneCompetenciasConsulta();
            vListaFactoresCompetencias = op.obtieneFactoresCompetencias();
            vDtFactoresPruebas = ObtieneDataTableCompetencias();
            vListaDetallada = op.obtieneConsultaPersonalDetallada(pIdBateria);


            vDtPivot.Columns.Add("CL_COLOR", typeof(string));
            vDtPivot.Columns.Add("NB_COMPETENCIA", typeof(string));

            foreach (var item in vListaFactores)
            {
                vDtPivot.Columns.Add(item.ID_FACTOR.ToString() + "E", typeof(int));
            }

            foreach (var vCom in vListaCompetencias)
            {
                DataRow vDr = vDtPivot.NewRow();

                if (!vClasificacion.Equals(vCom.NB_CLASIFICACION_COMPETENCIA))
                {
                    vClasificacion = vCom.NB_CLASIFICACION_COMPETENCIA;
                    DataRow vDrClasificacion = vDtPivot.NewRow();

                    vDrClasificacion["CL_COLOR"] = vCom.CL_COLOR;
                    vDrClasificacion["NB_COMPETENCIA"] = vCom.NB_CLASIFICACION_COMPETENCIA;
                    vDtPivot.Rows.Add(vDrClasificacion);

                    vDrClasificacion = null;
                }

                vClaseColor = string.Format(vCom.CL_COLOR);
                vDr["NB_COMPETENCIA"] = vCom.NB_COMPETENCIA;

                foreach (var vFac in vListaFactores)
                {
                    var vResultado = vListaDetallada.Where(t => t.ID_COMPETENCIA == vCom.ID_COMPETENCIA & t.ID_FACTOR == vFac.ID_FACTOR).FirstOrDefault();
                    if (vResultado != null)
                    {
                        int vNum = (int)vResultado.NO_VALOR;

                        vDr[vFac.ID_FACTOR.ToString() + "E"] = vNum;
                    }
                    else
                    {
                        var vBuscarResultado = vListaFactoresCompetencias.Where(t => t.ID_COMPETENCIA == vCom.ID_COMPETENCIA & t.ID_FACTOR == vFac.ID_FACTOR).FirstOrDefault();
                        if (vBuscarResultado != null)
                        {
                            vDr[vFac.ID_FACTOR.ToString() + "E"] = "-1";
                        }
                    }
                }

                vDtPivot.Rows.Add(vDr);
            }






            using (ExcelPackage pck = new ExcelPackage(newStream))
            {
                var ws = pck.Workbook.Worksheets.Add("ConsultaPersonalDetallada");


                ws.Cells["B1"].Value = "Consulta personal detallada";
                ws.Cells["B1"].Style.Font.Size = 18;
                ws.Cells["B1"].Style.Font.Color.SetColor(System.Drawing.Color.IndianRed);

                ws.Cells["B3"].Value = "Solicitud: " + pClFolio + "     Aplicante: " + pNbCandidato;
                ws.Cells["B3"].Style.Font.Bold = true;
                asignarEstiloCelda(ws.Cells["B3:C3"], "PowderBlue", false);

                //Datos de pruebas y competencias
                ws.Cells["A7"].LoadFromDataTable(vDtPivot, false);

                //Encabezados
                ws.Cells["C5"].LoadFromDataTable(vDtFactoresPruebas, false);

                ws.Column(2).AutoFit();

                //Ahora, vamos a realizar un loop para hacer el merge de las columnas de las pruebas y ver como queda
                int vNoColumnas = vDtFactoresPruebas.Columns.Count;
                int vNoFilas = vDtPivot.Columns.Count;

                int vColumnaInicial = 3;
                int vColumnaFinal = 3;
                int vFilaPruebas = 5;
                int vFilaFactores = 6;
                int vFilaDatos = 7;

                string vNbPrueba = ws.Cells[vFilaPruebas, vColumnaInicial].Value.ToString();
                bool vFgRealizaMerge = false;


                //Iteramos las filas para encontrar la fila de la clasificación y dibujarla
                for (int i = 1; i <= vNoFilas + 1; i++)
                {

                    if (ws.Cells[vFilaDatos, 1].Value != null)
                    {
                        vClaseColor = ws.Cells[vFilaDatos, 1].Value.ToString();
                        ws.Cells[vFilaDatos, 1, vFilaDatos, (vNoColumnas + 2)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        ws.Cells[vFilaDatos, 1, vFilaDatos, (vNoColumnas + 2)].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromName(vClaseColor));
                        ws.Cells[vFilaDatos, 1, vFilaDatos, (vNoColumnas + 2)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium, System.Drawing.Color.Black);
                        ws.Cells[vFilaDatos, 1, vFilaDatos, (vNoColumnas + 2)].Style.Font.Bold = true;
                    }

                    vFilaDatos++;
                }

                //Iteramos las columnas para hacer el merge de los titulos de las pruebas y cambiar la orientación de los factores de las pruebas, tambien acomodamos el tamaño de las columnas
                for (int i = 1; i <= vNoColumnas + 1; i++)
                {

                    if (ws.Cells[vFilaPruebas, vColumnaFinal].Value == null)
                    {
                        vFgRealizaMerge = true;
                    }
                    else
                    {
                        if (!ws.Cells[vFilaPruebas, vColumnaFinal].Value.ToString().Equals(vNbPrueba))
                        {
                            vFgRealizaMerge = true;
                        }
                    }

                    if (vFgRealizaMerge)
                    {
                        //aplicamos el merge y el centrado al título de la prueba
                        ws.Cells[vFilaPruebas, vColumnaInicial, vFilaPruebas, (vColumnaFinal - 1)].Merge = true;
                        ws.Cells[vFilaPruebas, vColumnaInicial].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                        //Agregamos el color y el borde
                        ws.Cells[vFilaPruebas, vColumnaInicial, vFilaFactores, (vColumnaFinal - 1)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        ws.Cells[vFilaPruebas, vColumnaInicial, vFilaFactores, (vColumnaFinal - 1)].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromName(vColorEncabezado));
                        //ws.Cells[vFilaPruebas, vColumnaInicial, vFilaFactores, (vColumnaFinal - 1)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium, System.Drawing.Color.Black);

                        ws.Cells[vFilaPruebas, vColumnaInicial, vFilaFactores, (vColumnaFinal - 1)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                        ws.Cells[vFilaPruebas, vColumnaInicial, vFilaFactores, (vColumnaFinal - 1)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                        ws.Cells[vFilaPruebas, vColumnaInicial, vFilaFactores, (vColumnaFinal - 1)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                        ws.Cells[vFilaPruebas, vColumnaInicial, vFilaFactores, (vColumnaFinal - 1)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

                        if (vColorEncabezado.Equals("lightskyblue"))
                        {
                            vColorEncabezado = "white";
                        }
                        else
                        {
                            vColorEncabezado = "lightskyblue";
                        }

                        if (ws.Cells[vFilaPruebas, vColumnaFinal].Value != null)
                        {
                            vNbPrueba = ws.Cells[vFilaPruebas, vColumnaFinal].Value.ToString();
                        }
                        else
                        {
                            vNbPrueba = "";
                        }

                        vColumnaInicial = vColumnaFinal;
                        vFgRealizaMerge = false;
                    }


                    ws.Cells[vFilaFactores, vColumnaFinal].Style.TextRotation = 180;
                    ws.Cells[vFilaFactores, vColumnaFinal].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    vColumnaFinal++;
                }

                //Aqui estams metiendo las formulas necesarias para que ponga el color según el valor

                var conditionalFormattingRule01 = ws.ConditionalFormatting.AddEqual(ws.Cells[3, 3, (vNoFilas + 3), (vNoColumnas + 3)]);
                conditionalFormattingRule01.Formula = "3";
                conditionalFormattingRule01.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                conditionalFormattingRule01.Style.Fill.BackgroundColor.Color = System.Drawing.Color.Green;
                conditionalFormattingRule01.Style.Font.Color.Color = System.Drawing.Color.Green;
                conditionalFormattingRule01.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                conditionalFormattingRule01.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                conditionalFormattingRule01.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                conditionalFormattingRule01.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

                var conditionalFormattingRule02 = ws.ConditionalFormatting.AddEqual(ws.Cells[3, 3, (vNoFilas + 3), (vNoColumnas + 3)]);
                conditionalFormattingRule02.Formula = "2";
                conditionalFormattingRule02.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                conditionalFormattingRule02.Style.Fill.BackgroundColor.Color = System.Drawing.Color.Yellow;
                conditionalFormattingRule02.Style.Font.Color.Color = System.Drawing.Color.Yellow;
                conditionalFormattingRule02.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                conditionalFormattingRule02.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                conditionalFormattingRule02.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                conditionalFormattingRule02.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

                var conditionalFormattingRule03 = ws.ConditionalFormatting.AddEqual(ws.Cells[3, 3, (vNoFilas + 3), (vNoColumnas + 3)]);
                conditionalFormattingRule03.Formula = "1";
                conditionalFormattingRule03.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                conditionalFormattingRule03.Style.Fill.BackgroundColor.Color = System.Drawing.Color.Red;
                conditionalFormattingRule03.Style.Font.Color.Color = System.Drawing.Color.Red;
                conditionalFormattingRule03.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                conditionalFormattingRule03.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                conditionalFormattingRule03.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                conditionalFormattingRule03.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

                var conditionalFormattingRule04 = ws.ConditionalFormatting.AddEqual(ws.Cells[3, 3, (vNoFilas + 3), (vNoColumnas + 3)]);
                conditionalFormattingRule04.Formula = "-1";
                conditionalFormattingRule04.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                conditionalFormattingRule04.Style.Fill.BackgroundColor.Color = System.Drawing.Color.Gray;
                conditionalFormattingRule04.Style.Font.Color.Color = System.Drawing.Color.Gray;
                conditionalFormattingRule04.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                conditionalFormattingRule04.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                conditionalFormattingRule04.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                conditionalFormattingRule04.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
  
                ws.Column(1).Hidden = true;

                pck.Save();
                newStream = pck.Stream;
            }

            oConsultaPersonal.NB_ARCHIVO = "ConsultaPersonalDetallada.xlsx";
            oConsultaPersonal.FI_ARCHIVO = ((MemoryStream)newStream).ToArray();

            return oConsultaPersonal;
        }

        public DataTable ObtieneDataTableCompetencias()
        {
            List<SPE_OBTIENE_FACTORES_CONSULTA_Result> vListaFactores = new List<SPE_OBTIENE_FACTORES_CONSULTA_Result>();
            ConsultaPersonalOperaciones op = new ConsultaPersonalOperaciones();

            vListaFactores = op.obtieneFactoresConsulta();

            DataTable vDtFactoresPruebas = new DataTable();


            foreach (var item in vListaFactores)
            {
                vDtFactoresPruebas.Columns.Add(item.ID_FACTOR.ToString() + "E", typeof(string));
            }


            DataRow vDrPruebas = vDtFactoresPruebas.NewRow();

            foreach (var item in vListaFactores)
            {
                vDrPruebas[item.ID_FACTOR.ToString() + "E"] = item.NB_PRUEBA;
            }

            vDtFactoresPruebas.Rows.Add(vDrPruebas);


            DataRow vDrFactores = vDtFactoresPruebas.NewRow();

            foreach (var item in vListaFactores)
            {
                vDrFactores[item.ID_FACTOR.ToString() + "E"] = item.NB_FACTOR;
            }

            vDtFactoresPruebas.Rows.Add(vDrFactores);


            return vDtFactoresPruebas;
        }

    }
}
