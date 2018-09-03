using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Entidades.IntegracionDePersonal;
using System.Xml.Linq;
using SIGE.Negocio.Utilerias;
using SIGE.Negocio.IntegracionDePersonal;
using System.Drawing;
using System.Data;
using Telerik.Web.UI;
using SIGE.WebApp.Comunes;
using System.IO;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Reflection;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;

namespace SIGE.WebApp.IDP
{
    public partial class VantanaPuestoVsCandidatos : System.Web.UI.Page
    {

        public Guid vIdPuestoVsCandidatos
        {
            get { return (Guid)ViewState["vs_vIdPersonaVsCandidatos"]; }
            set { ViewState["vs_vIdPersonaVsCandidatos"] = value; }
        }

        public int vIdPuesto
        {
            get { return (int)ViewState["vs_vIdPuesto"]; }
            set { ViewState["vs_vIdPuesto"] = value; }
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

        public List<int> vListaCandidatos
        {
            get { return (List<int>)ViewState["vs_vListaCandidatos"]; }
            set { ViewState["vs_vListaCandidatos"] = value; }
        }

        public List<E_GRAFICA_PUESTO_CANDIDATOS> vPuestoCandidatos
        {
            get { return (List<E_GRAFICA_PUESTO_CANDIDATOS>)ViewState["vs_vPuestoCandidatos"]; }
            set { ViewState["vs_vPuestoCandidatos"] = value; }
        }

        public List<int> vListaPuestos
        {
            get { return (List<int>)ViewState["vs_vListaPuestos"]; }
            set { ViewState["vs_vListaPuestos"] = value; }
        }

        public List<E_GRAFICA_PUESTO_CANDIDATOS> vCandidatoPuestos
        {
            get { return (List<E_GRAFICA_PUESTO_CANDIDATOS>)ViewState["vs_vCandidatoPuestos"]; }
            set { ViewState["vs_vCandidatoPuestos"] = value; }
        }

        private string vClUsuario
        {
            get { return (string)ViewState["vsvClUsuario"]; }
            set { ViewState["vsvClUsuario"] = value; }
        }

        private string vNbPrograma
        {
            get { return (string)ViewState["vsvNbPrograma"]; }
            set { ViewState["vsvNbPrograma"] = value; }
        }

        public string vs_NB_PUESTO
        {
            get { return (string)ViewState["vs_NB_PUESTO"]; }
            set { ViewState["vs_NB_PUESTO"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = (ContextoUsuario.oUsuario != null ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO");
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                vIdPuestoVsCandidatos = Guid.Empty;
                vFgConsultaParcial = ContextoApp.IDP.ConfiguracionIntegracion.FgConsultasParciales;
                vFgCalificacionCero = ContextoApp.IDP.ConfiguracionIntegracion.FgIgnorarCompetencias;
                if (Request.Params["vIdPuestoVsCandidatos"] != null)
                {
                    vIdPuestoVsCandidatos = Guid.Parse(Request.Params["vIdPuestoVsCandidatos"].ToString());
                }
                else
                    vIdPuestoVsCandidatos = Guid.NewGuid();

                if (ContextoConsultasComparativas.oPuestoVsCandidatos == null)
                {
                    ContextoConsultasComparativas.oPuestoVsCandidatos = new List<E_PUESTO_VS_CANDIDATOS>();
                }

                if (Request.Params["IdPuesto"] != null)
                {
                    vIdPuesto = int.Parse(Request.Params["IdPuesto"]);
                }
                CargarPuestoCandidatos();
            }
        }

        protected void CargarPuestoCandidatos()
        {

            vListaCandidatos = new List<int>();

            if (vIdPuestoVsCandidatos != Guid.Empty)
            {
                E_PUESTO_VS_CANDIDATOS oPuestoCandidatos = ContextoConsultasComparativas.oPuestoVsCandidatos.Where(t => t.vIdPuestoVsCandidatos == vIdPuestoVsCandidatos).FirstOrDefault();
                vListaCandidatos = oPuestoCandidatos.vListaCandidatos;
            }

            var vXelements = vListaCandidatos.Select(x =>
                                           new XElement("CANDIDATO",
                                           new XAttribute("ID_CANDIDATO", x)));

            XElement SELECCIONADOS =
            new XElement("CANDIDATOS", vXelements
                );

            ConsultasComparativasNegocio nComparativas = new ConsultasComparativasNegocio();
            vPuestoCandidatos = nComparativas.ObtienePuestoCandidatos(pID_PUESTO: vIdPuesto, pXML_CANDIDATOS: SELECCIONADOS.ToString(), pFgConsultaParcial: vFgConsultaParcial, pFgCalificacionCero: vFgCalificacionCero ).Select(s => new E_GRAFICA_PUESTO_CANDIDATOS
            {
                NB_COMPETENCIA = s.NB_COMPETENCIA,
                NO_VALOR_NIVEL = s.NO_VALOR_NIVEL,
                CL_CANDIDATO = s.CL_CANDIDATO,
                NB_CANDIDATO = s.NB_CANDIDATO,
                NO_VALOR_CANDIDATO = s.NO_VALOR_CANDIDATO,
                ID_CANDIDATO = s.ID_CANDIDATO,
                DS_COMPETENCIA = s.DS_COMPETENCIA,
                ID_BATERIA = s.ID_BATERIA,
                CL_PUESTO = s.CL_PUESTO,
                NB_PUESTO = s.NB_PUESTO,
                PR_CANDIDATO = s.PR_CANDIDATO,
                CL_COLOR = s.CL_COLOR,
                PR_TRUNCADO = CalculaPorcentaje(s.PR_CANDIDATO),
                CL_COMPETENCIA = s.CL_COMPETENCIA,
                CL_SOLICITUD = s.CL_SOLICITUD

            }).OrderBy(s => s.CL_COMPETENCIA).ToList();

            if (vPuestoCandidatos.Count > 0)
            {
                vs_NB_PUESTO = vPuestoCandidatos.FirstOrDefault().NB_PUESTO;
            }

          GraficaPuestoCandidatos(vPuestoCandidatos);

            //txtPromedio.Enabled = false;
            //var promedio = vPuestoCandidatos.Average(s => s.PR_TRUNCADO);
            ////if (Convert.ToDouble(promedio) <= 100 && Convert.ToDouble(promedio) >= 0) 
            //     txtPromedio.Value = Convert.ToDouble(promedio);
            //else if(Convert.ToDouble(promedio) > 100)
            //    txtPromedio.Value = 100;
            //else if (Convert.ToDouble(promedio) < 0)
            //    txtPromedio.Value = 0;

            List<E_PROMEDIO> vlstPromedios = new List<E_PROMEDIO>();
            foreach (var item in vListaCandidatos)
            {
                List<E_PROMEDIO> vlist = new List<E_PROMEDIO>();
                foreach (var i in vPuestoCandidatos)
                {
                    if (item == i.ID_CANDIDATO && i.NO_VALOR_NIVEL != 0)
                    {
                        vlist.Add(new E_PROMEDIO { NB_PUESTO = i.NB_CANDIDATO, PORCENTAJE = i.PR_TRUNCADO, PORCENTAJE_NO_TRUNCADO = i.PR_CANDIDATO });
                    }
                }
                vlstPromedios.Add(new E_PROMEDIO
                {
                    NB_PUESTO = vlist.Select(s => s.NB_PUESTO).FirstOrDefault(),
                                          PROMEDIO = vlist.Average(s => s.PORCENTAJE),
                                          FG_SUPERA_130 = vlist.Average(s => s.PORCENTAJE_NO_TRUNCADO) >= 130 ? true : false,
                                          PROMEDIO_TXT = vlist.Average(s => s.PORCENTAJE_NO_TRUNCADO) >= 130 ? Decimal.Round(vlist.Average(s => s.PORCENTAJE) ?? 1 ,2).ToString() + "(*)" : Decimal.Round(vlist.Average(s => s.PORCENTAJE) ?? 1, 2).ToString()
                });
            }
            rgdPromedios.DataSource = vlstPromedios.Where(w => w.NB_PUESTO != null);
            rgdPromedios.DataBind();
            rgdPromedios.Rebind();

            for( int i = 0; i < vlstPromedios.Count; i++)
            {
                if (vlstPromedios[i].FG_SUPERA_130 == true)
                {
                    divMensajeMayor130.Visible = true;
                    i = vlstPromedios.Count;
                }
            }
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
            List<ColumnSeries> lstCandidatos = new List<ColumnSeries>();

            bool continua = false;
            rhcPuestoCandidatos.PlotArea.Series.Clear();

            foreach (var item in vListaCandidatos)
            {
                ColumnSeries vCandidatos = new ColumnSeries();

                foreach (var i in plstPuestoCandidatos)
                {
                    if (item == i.ID_CANDIDATO)
                    {
                        vCandidatos.SeriesItems.Add(new CategorySeriesItem(i.NO_VALOR_CANDIDATO));
                        vCandidatos.LabelsAppearance.Visible = false;
                        
                        vCandidatos.Name = "(" + i.CL_SOLICITUD + ")" + i.NB_CANDIDATO;
                        continua = true;
                    }
                }
                if (continua)
                {
                    vCandidatos.SeriesItems.Add(new CategorySeriesItem(0));
                    lstCandidatos.Add(vCandidatos);
                    //rhcPuestoCandidatos.PlotArea.Series.Add(vCandidatos);
                    continua = false;
                }
            }

            var vPuestoCompetencia = plstPuestoCandidatos.Select(s => new { s.NO_VALOR_NIVEL, s.NB_PUESTO, s.NB_COMPETENCIA, s.CL_PUESTO }).Distinct().ToList();
            ColumnSeries vPuesto = new ColumnSeries();

            foreach (var item in vPuestoCompetencia)
            {
                vPuesto.SeriesItems.Add(new CategorySeriesItem(item.NO_VALOR_NIVEL));
                vPuesto.LabelsAppearance.Visible = false;
                vPuesto.Name = "(" + item.CL_PUESTO + ") " + item.NB_PUESTO;
                rhcPuestoCandidatos.PlotArea.XAxis.Items.Add(item.NB_COMPETENCIA);
                rhcPuestoCandidatos.PlotArea.XAxis.LabelsAppearance.RotationAngle = 270;
                rhcPuestoCandidatos.PlotArea.YAxis.MaxValue = 5;
            }
            rhcPuestoCandidatos.PlotArea.Series.Add(vPuesto);

            foreach(var it in lstCandidatos)
            {
                rhcPuestoCandidatos.PlotArea.Series.Add(it);
            }
        }

        protected void pgDetalleCompetencia_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            pgDetalleCompetencia.DataSource = vPuestoCandidatos;
        }

        protected void pgDetalleCompetencia_CellDataBound(object sender, PivotGridCellDataBoundEventArgs e)
        {
            if (e.Cell is PivotGridRowHeaderCell)
            {
                if (e.Cell.Controls.Count > 1)
                {
                    (e.Cell.Controls[0] as Button).Visible = false;
                }
            }
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            String json = e.Argument;
            String[] stringArray = json.Split("_".ToCharArray());
            switch (stringArray[1])
            {
                case "SVC":

                    JSON_VALUES jsonValues = JsonConvert.DeserializeObject<JSON_VALUES>(stringArray[0]);
                    try
                    {
                        Stream newStream = null;
                        using (ExcelPackage excelPackage = new ExcelPackage(newStream ?? new MemoryStream()))
                        {
                            excelPackage.Workbook.Properties.Author = ContextoUsuario.oUsuario.CL_USUARIO;
                            excelPackage.Workbook.Properties.Title = "Consultas Personales "+ vs_NB_PUESTO+ " vs candidatos";
                            excelPackage.Workbook.Properties.Comments = "SIGEIN 5.0";

                            excelPackage.Workbook.Worksheets.Add("Reporte");
                            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];
                            
                            setGraphic(worksheet, vPuestoCandidatos);
                            pivotData(worksheet);

                            worksheet.Column(4).Width = 30;

                            string[] propertyNames = { "Persona", "Compatibilidad" };
                            MemberInfo[] membersToInclude = typeof(E_PROMEDIO_CANDIDATO)
                           .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                           .Where(p => propertyNames.Contains(p.Name))
                           .ToArray();

                            worksheet.Cells[1, 1].LoadFromCollection(getPromedios(), true, OfficeOpenXml.Table.TableStyles.Light1, BindingFlags.Instance | BindingFlags.Public, membersToInclude);
                            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                            excelPackage.Save();
                            newStream = excelPackage.Stream;
                        }
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment; filename=Consulta "+ vs_NB_PUESTO+" vs. N candidatos.xlsx");
                        Response.BinaryWrite(((MemoryStream)newStream).ToArray());
                        Response.End();
                    }
                    catch (Exception ex)
                    {
                        UtilMensajes.MensajeDB(rnMensaje, "Ocurrio un error al procesar el Excel, Intente de nuevo.", Entidades.Externas.E_TIPO_RESPUESTA_DB.ERROR);
                    }
                    break;

                default: break;
            }
        }

        public void setGraphic(ExcelWorksheet oSheet, List<E_GRAFICA_PUESTO_CANDIDATOS> plstCandidatoPuestos)
        {
            List<E_GRAPHIC_CANDIDATOS> puestos = new List<E_GRAPHIC_CANDIDATOS>();
            List<E_GRAPHIC_PUESTO_COMPETENCIA> competencias = new List<E_GRAPHIC_PUESTO_COMPETENCIA>();
            List<E_GRAPHIC_PUESTO_DATOS> datosPorPuestos = new List<E_GRAPHIC_PUESTO_DATOS>();

            string nbPuesto = string.Empty;

            var pivot = plstCandidatoPuestos.GroupBy(l => new { l.NB_CANDIDATO })
             .Select(cl => new E_GRAPHIC_PUESTO_DATOS
             {
                 Candidato = cl.First().NB_CANDIDATO,
                 Datos = new List<E_GRAPHIC_CANDIDATOS>()
             }).OrderBy(o => o.Candidato).ToList();


            foreach (var i in plstCandidatoPuestos)
            {
                if (pivot.Where(w => w.Candidato == i.NB_CANDIDATO).FirstOrDefault() != null)
                {
                    pivot.Where(w => w.Candidato == i.NB_CANDIDATO).FirstOrDefault().Datos.Add(new E_GRAPHIC_CANDIDATOS { Candidato = i.NB_CANDIDATO, Competencia = i.NB_COMPETENCIA, valor = i.NO_VALOR_CANDIDATO });
                }
            }


            var vPuestoCompetencia = plstCandidatoPuestos.Select(s => new { s.NO_VALOR_NIVEL, s.NB_COMPETENCIA }).Distinct().ToList();
            if (plstCandidatoPuestos.FirstOrDefault() != null) 
            {
                nbPuesto = plstCandidatoPuestos.FirstOrDefault().NB_PUESTO;
            }
            foreach (var item in vPuestoCompetencia)
            {
                competencias.Add(new E_GRAPHIC_PUESTO_COMPETENCIA { Competencia = item.NB_COMPETENCIA, valor = item.NO_VALOR_NIVEL });
            }

            string[] propertyCompetencias = { "Competencia", "valor" };
            MemberInfo[] membersToIncludeCompetencias = typeof(E_GRAPHIC_PUESTO_COMPETENCIA)
           .GetProperties(BindingFlags.Instance | BindingFlags.Public)
           .Where(p => propertyCompetencias.Contains(p.Name))
           .ToArray();
            oSheet.Cells[200, 4].LoadFromCollection(competencias, true, OfficeOpenXml.Table.TableStyles.Light1, BindingFlags.Instance | BindingFlags.Public, membersToIncludeCompetencias);


            var chart = oSheet.Drawings.AddChart("Consultas personales", eChartType.ColumnClustered);
            chart.SetPosition(600, 25);
            chart.SetSize(940, 20 * 14);



            var serie2 = chart.Series.Add(oSheet.Cells["E201:E" + (201 + competencias.Count - 1).ToString()], oSheet.Cells["D201:D" + (201 + competencias.Count - 1).ToString()]);
            chart.YAxis.MaxValue = 10;
            chart.YAxis.MinValue = 0;
            chart.Title.Text = "Reporte puesto vs candidatos";
            chart.Title.Font.Color = System.Drawing.ColorTranslator.FromHtml("#4F81BD");
            chart.Border.Fill.Style = eFillStyle.NoFill;
            chart.Title.Font.Size = 9;
            chart.YAxis.Font.Size = 8;
            chart.XAxis.Font.Size = 8;
            chart.Legend.Font.Size = 8;
            chart.Legend.Font.Color = System.Drawing.ColorTranslator.FromHtml("#4F81BD");
            chart.YAxis.Font.Color = System.Drawing.ColorTranslator.FromHtml("#4F81BD");
            chart.XAxis.Font.Color = System.Drawing.ColorTranslator.FromHtml("#4F81BD");
            chart.Legend.Position = OfficeOpenXml.Drawing.Chart.eLegendPosition.Bottom;
            chart.Style = OfficeOpenXml.Drawing.Chart.eChartStyle.Style10;
            serie2.Header = nbPuesto;


            //*************************************************************************
            int countInicio = 201;
            int countFin = 0;
            int rowColumnsNamesPosition = 200;
            foreach (var item in pivot)
            {
                countFin = countInicio + (item.Datos.Count - 1);
                var serie1 = chart.Series.Add(oSheet.Cells["C" + countInicio.ToString() + ":C" + countFin.ToString()], oSheet.Cells["B" + countInicio.ToString() + ":B" + countFin.ToString()]);
                serie1.Header = item.Candidato;

                string[] propertyNames = { "Candidato", "Competencia", "valor" };
                MemberInfo[] membersToInclude = typeof(E_GRAPHIC_CANDIDATOS)
               .GetProperties(BindingFlags.Instance | BindingFlags.Public)
               .Where(p => propertyNames.Contains(p.Name))
               .ToArray();
                oSheet.Cells[rowColumnsNamesPosition, 1].LoadFromCollection(item.Datos, true, OfficeOpenXml.Table.TableStyles.Light1, BindingFlags.Instance | BindingFlags.Public, membersToInclude);
                rowColumnsNamesPosition = countInicio + (item.Datos.Count - 1) + 1;
                countInicio = countInicio + (item.Datos.Count - 1) + 2;

            }
            //*************************************************************************

        }

        public List<E_PROMEDIO_CANDIDATO> getPromedios()
        {
            vListaCandidatos = new List<int>();

            if (vIdPuestoVsCandidatos != Guid.Empty)
            {
                E_PUESTO_VS_CANDIDATOS oPuestoCandidatos = ContextoConsultasComparativas.oPuestoVsCandidatos.Where(t => t.vIdPuestoVsCandidatos == vIdPuestoVsCandidatos).FirstOrDefault();
                vListaCandidatos = oPuestoCandidatos.vListaCandidatos;
            }

            var vXelements = vListaCandidatos.Select(x =>
                                           new XElement("CANDIDATO",
                                           new XAttribute("ID_CANDIDATO", x)));

            XElement SELECCIONADOS =
            new XElement("CANDIDATOS", vXelements
                );

            List<E_PROMEDIO> vlstPromedios = new List<E_PROMEDIO>();
            foreach (var item in vListaCandidatos)
            {
                List<E_PROMEDIO> vlist = new List<E_PROMEDIO>();
                foreach (var i in vPuestoCandidatos)
                {
                    if (item == i.ID_CANDIDATO && i.NO_VALOR_NIVEL != 0)
                    {
                        vlist.Add(new E_PROMEDIO { NB_PUESTO = i.NB_CANDIDATO, PORCENTAJE = i.PR_TRUNCADO, PORCENTAJE_NO_TRUNCADO = i.PR_CANDIDATO });
                    }
                }
                vlstPromedios.Add(new E_PROMEDIO
                {
                    NB_PUESTO = vlist.Select(s => s.NB_PUESTO).FirstOrDefault(),
                    PROMEDIO = vlist.Average(s => s.PORCENTAJE),
                    FG_SUPERA_130 = vlist.Average(s => s.PORCENTAJE_NO_TRUNCADO) >= 130 ? true : false,
                    PROMEDIO_TXT = vlist.Average(s => s.PORCENTAJE_NO_TRUNCADO) >= 130 ? Decimal.Round(vlist.Average(s => s.PORCENTAJE) ?? 1, 2).ToString() + "(*)" : Decimal.Round(vlist.Average(s => s.PORCENTAJE) ?? 1, 2).ToString()
                });
            }

            List <E_PROMEDIO_CANDIDATO> listPersonas = new List<E_PROMEDIO_CANDIDATO>();
            listPersonas = vlstPromedios.Select(s => new 
            E_PROMEDIO_CANDIDATO 
            {
            Persona = s.NB_PUESTO,
            Compatibilidad = s.PROMEDIO_TXT
            }).ToList();

            return listPersonas;
        }

        public void pivotData(ExcelWorksheet oSheet)
        {
            var query = vPuestoCandidatos.OrderBy(s=>s.NB_CANDIDATO).GroupBy(t => new { t.CL_COMPETENCIA, t.NB_COMPETENCIA, t.DS_COMPETENCIA,t.CL_COLOR }).Select(g => new
            {
                Clave = g.Key.CL_COMPETENCIA,
                Nombre = g.Key.NB_COMPETENCIA,
                Descripcion = g.Key.DS_COMPETENCIA,
                color = g.Key.CL_COLOR,
                Details = g.GroupBy(t => t.NB_CANDIDATO).Select(s => new
                {
                    Candidato = s.Key,
                    Avg = s.Average(c => c.PR_CANDIDATO)
                })  .ToList()
            });


            string mensaje = string.Empty;
            int start = 2;
          
            oSheet.Cells[start - 1, 5].Value = "Competencia";
            oSheet.Cells[start - 1, 5].Style.Font.Bold = true;
            oSheet.Cells[start - 1, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
            oSheet.Cells[start - 1, 5].Style.Fill.BackgroundColor.SetColor(Color.White);
            oSheet.Cells[start - 1, 6].Value = "Descripción";
            oSheet.Cells[start - 1, 6].Style.Font.Bold = true;
            oSheet.Cells[start - 1, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
            oSheet.Cells[start - 1, 6].Style.Fill.BackgroundColor.SetColor(Color.White);

            int columnsNames = 6;
            if (query.FirstOrDefault() != null)
            {
                for (int i = 0; i < query.FirstOrDefault().Details.Count; i++)
                {
                    oSheet.Cells[start - 1, (columnsNames + 1)].Value = query.FirstOrDefault().Details[i].Candidato;
                    oSheet.Cells[start - 1, (columnsNames + 1)].Style.Font.Bold = true;
                    oSheet.Cells[start - 1, (columnsNames + 1)].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    oSheet.Cells[start - 1, (columnsNames + 1)].Style.Fill.BackgroundColor.SetColor(Color.White);
                    columnsNames++;
                }
            }

            int contador = 1;
            foreach (var i in query)
            {
                oSheet.Cells[start, 4].Value = string.Empty;
                oSheet.Cells[start, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                oSheet.Cells[start, 4].Style.Fill.BackgroundColor.SetColor(getColor(i.color));

                oSheet.Cells[start, 5].Value = i.Nombre;
                oSheet.Cells[start, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                oSheet.Cells[start, 5].Style.Fill.BackgroundColor.SetColor((contador) % 2 == 0
                   ? Color.White
                   : Color.LightGray);
                oSheet.Cells[start, 6].Value = i.Descripcion;
                oSheet.Cells[start, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
                oSheet.Cells[start, 6].Style.Fill.BackgroundColor.SetColor((contador) % 2 == 0
                   ? Color.White
                   : Color.LightGray);
                int contadorColums = 6;
                for (int j = 0; j < i.Details.Count; j++)
                {
                    oSheet.Cells[start, contadorColums + (j + 1)].Value = i.Details.ElementAt(j).Avg;
                    oSheet.Cells[start, contadorColums + (j + 1)].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    oSheet.Cells[start, contadorColums + (j + 1)].Style.Fill.BackgroundColor.SetColor((contador) % 2 == 0
                       ? Color.White
                       : Color.LightGray);
                }
                contador++;
                start++;
            }
        }


        public Color getColor(string pClColor) {
//MediumOrchid
//Yellow
//SkyBlue
//LawnGreen
//OrangeRed
//Orange

            switch(pClColor)
            {
                case "MediumOrchid": return Color.MediumOrchid;
                case "Yellow":return Color.Yellow;
                case "LawnGreen": return Color.LawnGreen;
                case "OrangeRed": return Color.OrangeRed;
                case "Orange": return Color.Orange;
                case "SkyBlue": return Color.SkyBlue;
                default:return Color.LightGray; 
            }
        }

        [Serializable]
        public class E_PROMEDIO_CANDIDATO
        {
            public string Persona { get; set; }
            public string Compatibilidad { get; set; }
        }

        [Serializable]
        class E_GRAPHIC_PUESTO_DATOS
        {
            public string Candidato { get; set; }
            public List<E_GRAPHIC_CANDIDATOS> Datos { get; set; }
        }

        [Serializable]
        class E_GRAPHIC_CANDIDATOS
        {
            public string Candidato { get; set; }
            public string Competencia { get; set; }
            public string Descripcion { get; set; }
            public Nullable<decimal> valor { get; set; }
            public Nullable<decimal> Promedio { get; set; }
        }

        [Serializable]
        class E_GRAPHIC_PUESTO_COMPETENCIA
        {
            public string Competencia { get; set; }
            public Nullable<decimal> valor { get; set; }
        }

         class JSON_VALUES
        {
            public string svcImage { get; set; }
        }

    }
}