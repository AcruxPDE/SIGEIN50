using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Negocio.IntegracionDePersonal;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.IDP
{
    public partial class ReporteCandidatoVsPuesto : System.Web.UI.Page
    {

        #region Variables

        public Guid vIdCandidatoVsPuesto
        {
            get { return (Guid)ViewState["vs_vIdCandidatoVsPuesto"]; }
            set { ViewState["vs_vIdCandidatoVsPuesto"] = value; }
        }

        public int vIdCandidato
        {
            get { return (int)ViewState["vs_vIdCandidato"]; }
            set { ViewState["vs_vIdCandidato"] = value; }
        }

        public int? vIdPuestoCandidato
        {
            get { return (int?)ViewState["vs_vIdPuestoCandidato"]; }
            set { ViewState["vs_vIdPuestoCandidato"] = value; }
        }

        public string vs_NB_CANDIDATO
        {
            get { return (string)ViewState["vs_NB_CANDIDATO"]; }
            set { ViewState["vs_NB_CANDIDATO"] = value; }
        }

        public bool vFgConsultaParcial
        {
            get { return (bool)ViewState["vs_vFgConsultaParcial"]; }
            set { ViewState["vs_vFgConsultaParcial"] = value; }
        }

        public bool vFgEvaluacionesCero
        {
            get { return (bool)ViewState["vs_vFgEvaluacionesCero"]; }
            set { ViewState["vs_vFgEvaluacionesCero"] = value; }
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

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = (ContextoUsuario.oUsuario != null ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO");
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                vIdCandidatoVsPuesto = Guid.Empty;
                vFgConsultaParcial = ContextoApp.IDP.ConfiguracionIntegracion.FgConsultasParciales;
                vFgEvaluacionesCero = ContextoApp.IDP.ConfiguracionIntegracion.FgIgnorarCompetencias;
                if (Request.Params["vIdCandidatoVsPuestos"] != null)
                {
                    vIdCandidatoVsPuesto = Guid.Parse(Request.Params["vIdCandidatoVsPuestos"].ToString());
                }
                else
                    vIdCandidatoVsPuesto = Guid.NewGuid();

                if (ContextoConsultasComparativas.oCandidatoVsPuestos == null)
                {
                    ContextoConsultasComparativas.oCandidatoVsPuestos = new List<E_CANDIDATO_VS_PUESTOS>();
                }

                if (Request.Params["IdCandidato"] != null)
                {
                    vIdCandidato = int.Parse(Request.Params["IdCandidato"]);
                }
                if (Request.Params["pIdPuestoTablero"] != null)
                {
                    vIdPuestoCandidato = int.Parse(Request.Params["pIdPuestoTablero"]);
                }

                CargarCandidatoPuestos();
                divTabla.Controls.Add(CrearTabla(vCandidatoPuestos));
            }

        }

        protected void CargarCandidatoPuestos()
        {
            vListaPuestos = new List<int>();

            if (vIdPuestoCandidato == null)
            {
                if (vIdCandidatoVsPuesto != Guid.Empty)
                {
                    E_CANDIDATO_VS_PUESTOS oCandidatoPuestos = ContextoConsultasComparativas.oCandidatoVsPuestos.Where(w => w.vIdCandidatoVsPuestos == vIdCandidatoVsPuesto).FirstOrDefault();
                    vListaPuestos = oCandidatoPuestos.vListaPuestos;
                }
            }
            else
            {
                vListaPuestos = new List<int>() { (int)vIdPuestoCandidato };
            }

            var vXelements = vListaPuestos.Select(x =>
                                           new XElement("PUESTO",
                                           new XAttribute("ID_PUESTO", x)
                                ));
            XElement SELECCIONADOS =
            new XElement("PUESTOS", vXelements
                );

            ConsultasComparativasNegocio nComparativas = new ConsultasComparativasNegocio();
            vCandidatoPuestos = nComparativas.ObtieneCandidatoPuestos(pID_CANDIDATO: vIdCandidato, pXML_PUESTOS: SELECCIONADOS.ToString(), vFgConsultaParcial: vFgConsultaParcial, vFgCalificacionCero: vFgEvaluacionesCero).Select(s => new E_GRAFICA_PUESTO_CANDIDATOS
            {
                CL_COMPETENCIA = s.CL_COMPETENCIA,
                NB_COMPETENCIA = s.NB_COMPETENCIA,
                NO_VALOR_NIVEL = s.NO_VALOR_NIVEL,
                NB_CANDIDATO = s.NB_CANDIDATO,
                NO_VALOR_CANDIDATO = s.NO_VALOR_CANDIDATO,
                PR_CANDIDATO_PUESTO = s.PR_CANDIDATO_PUESTO,
                DS_COMPETENCIA = s.DS_COMPETENCIA,
                ID_BATERIA = s.ID_BATERIA,
                NB_PUESTO = s.NB_PUESTO,
                CL_PUESTO = s.CL_PUESTO,
                CL_COLOR = s.CL_COLOR,
                ID_PUESTO = s.ID_PUESTO,
                PR_TRUNCADO = CalculaPorcentaje(s.PR_CANDIDATO_PUESTO),
                CL_SOLICITUD = s.CL_SOLICITUD
            }).OrderBy(s => s.CL_COMPETENCIA).ToList();

            if (vCandidatoPuestos.Count > 0)
            {
                vs_NB_CANDIDATO = vCandidatoPuestos.FirstOrDefault().NB_CANDIDATO;
            }

            GraficaCandidatoPuestos(vCandidatoPuestos);

            List<E_PROMEDIO> vlstPromedios = new List<E_PROMEDIO>();
            foreach (var item in vListaPuestos)
            {
                List<E_PROMEDIO> vlist = new List<E_PROMEDIO>();
                foreach (var i in vCandidatoPuestos)
                {
                    if (item == i.ID_PUESTO && i.NO_VALOR_NIVEL != 0)
                    {
                        vlist.Add(new E_PROMEDIO { NB_PUESTO = i.NB_PUESTO, PORCENTAJE = i.PR_TRUNCADO, PORCENTAJE_NO_TRUNCADO = i.PR_CANDIDATO_PUESTO });
                    }
                }
                vlstPromedios.Add(new E_PROMEDIO
                {
                    NB_PUESTO = vlist.Select(s => s.NB_PUESTO).FirstOrDefault(),
                    PROMEDIO = vlist.Average(s => s.PORCENTAJE),
                    FG_SUPERA_130 = vlist.Average(s => s.PORCENTAJE_NO_TRUNCADO) >= 130 ? true : false,
                    PROMEDIO_TXT = vlist.Average(s => s.PORCENTAJE_NO_TRUNCADO) >= 130 ? Decimal.Round(vlist.Average(s => s.PORCENTAJE) ?? 1, 2).ToString() + "(*)" : Decimal.Round(vlist.Average(s => s.PORCENTAJE) ?? 1, 2).ToString()
                                          ,
                    PORCENTAJE_NO_TRUNCADO = vlist.Average(s => s.PORCENTAJE_NO_TRUNCADO)
                });
            }
            rgdPromedios.DataSource = vlstPromedios.Where(w => w.NB_PUESTO != null);
            rgdPromedios.DataBind();
            rgdPromedios.Rebind();

            for (int i = 0; i < vlstPromedios.Count; i++)
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

        protected void GraficaCandidatoPuestos(List<E_GRAFICA_PUESTO_CANDIDATOS> plstCandidatoPuestos)
        {
            List<ColumnSeries> lstPuestos = new List<ColumnSeries>();
            string vPuestosComp = "";

            bool continua = false;
            rhcCandidatoPuestos.PlotArea.Series.Clear();

            foreach (var item in vListaPuestos)
            {
                ColumnSeries vPuestos = new ColumnSeries();
                if (plstCandidatoPuestos.Where(w => w.ID_PUESTO == item).FirstOrDefault() != null)
                    if (plstCandidatoPuestos.Where(w => w.ID_PUESTO == item).FirstOrDefault().CL_PUESTO != null)
                        vPuestosComp = vPuestosComp + "- (" + plstCandidatoPuestos.Where(w => w.ID_PUESTO == item).FirstOrDefault().CL_PUESTO + ")" + plstCandidatoPuestos.Where(w => w.ID_PUESTO == item).FirstOrDefault().NB_PUESTO + "<br>";
                    else
                        vPuestosComp = vPuestosComp + "- ()" + plstCandidatoPuestos.Where(w => w.ID_PUESTO == item).FirstOrDefault().NB_PUESTO + "<br>";

                foreach (var i in plstCandidatoPuestos)
                {
                    if (item == i.ID_PUESTO)
                    {
                        vPuestos.SeriesItems.Add(new CategorySeriesItem(i.NO_VALOR_NIVEL));
                        vPuestos.LabelsAppearance.Visible = false;
                        vPuestos.Name = "(" + i.CL_PUESTO + ")" + i.NB_PUESTO;
                        continua = true;
                    }
                }
                if (continua)
                {
                    vPuestos.SeriesItems.Add(new CategorySeriesItem(0));
                    lstPuestos.Add(vPuestos);
                    continua = false;
                }
            }
            lbPuestos.InnerHtml = vPuestosComp;

            var vPuestoCompetencia = plstCandidatoPuestos.Select(s => new { s.NO_VALOR_CANDIDATO, s.NB_CANDIDATO, s.NB_COMPETENCIA, s.CL_SOLICITUD }).Distinct().ToList();
            ColumnSeries vCandidato = new ColumnSeries();
            lbCandidatosCom.InnerText = "- (" + vPuestoCompetencia.FirstOrDefault().CL_SOLICITUD + ") " + vPuestoCompetencia.FirstOrDefault().NB_CANDIDATO;

            foreach (var item in vPuestoCompetencia)
            {
                vCandidato.SeriesItems.Add(new CategorySeriesItem(item.NO_VALOR_CANDIDATO));
                vCandidato.LabelsAppearance.Visible = false;
                vCandidato.Name = "(" + item.CL_SOLICITUD + ")" + item.NB_CANDIDATO;
                rhcCandidatoPuestos.PlotArea.XAxis.Items.Add(item.NB_COMPETENCIA);
                rhcCandidatoPuestos.PlotArea.XAxis.LabelsAppearance.RotationAngle = 270;
                rhcCandidatoPuestos.PlotArea.YAxis.MaxValue = 6;
            }
            rhcCandidatoPuestos.PlotArea.Series.Add(vCandidato);

            foreach (var it in lstPuestos)
            {
                rhcCandidatoPuestos.PlotArea.Series.Add(it);
            }
        }

        //protected void pgDetalleCompetencia_NeedDataSource(object sender, Telerik.Web.UI.PivotGridNeedDataSourceEventArgs e)
        //{
        //    pgDetalleCompetencia.DataSource = vCandidatoPuestos;
        //}

        //protected void pgDetalleCompetencia_CellDataBound(object sender, Telerik.Web.UI.PivotGridCellDataBoundEventArgs e)
        //{
        //    if (e.Cell is PivotGridRowHeaderCell)
        //    {
        //        if (e.Cell.Controls.Count > 1)
        //        {
        //            (e.Cell.Controls[0] as Button).Visible = false;
        //        }
        //    }

        //}
        //protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        //{
        //    String json = e.Argument;
        //    String[] stringArray = json.Split("_".ToCharArray());
        //    switch (stringArray[1])
        //    {
        //        case "SVC":

        //            JSON_VALUES jsonValues = JsonConvert.DeserializeObject<JSON_VALUES>(stringArray[0]);
        //            try
        //            {
        //                Stream newStream = null;
        //                using (ExcelPackage excelPackage = new ExcelPackage(newStream ?? new MemoryStream()))
        //                {
        //                    excelPackage.Workbook.Properties.Author = vClUsuario;
        //                    excelPackage.Workbook.Properties.Title = "Consultas Personales " + vs_NB_CANDIDATO + " vs puestos";
        //                    excelPackage.Workbook.Properties.Comments = "SIGEIN 5.0";

        //                    excelPackage.Workbook.Worksheets.Add("Reporte");
        //                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];

        //                    setGraphic(worksheet, vCandidatoPuestos);
        //                    pivotData(worksheet);

        //                    string[] propertyNames = { "NB_PUESTO", "PROMEDIO" };
        //                    MemberInfo[] membersToInclude = typeof(E_PROMEDIO)
        //                   .GetProperties(BindingFlags.Instance | BindingFlags.Public)
        //                   .Where(p => propertyNames.Contains(p.Name))
        //                   .ToArray();

        //                    worksheet.Cells[1, 1].LoadFromCollection(getPromedios(), true, OfficeOpenXml.Table.TableStyles.Light1, BindingFlags.Instance | BindingFlags.Public, membersToInclude);
        //                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
        //                    excelPackage.Save();
        //                    newStream = excelPackage.Stream;
        //                }
        //                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //                Response.AddHeader("content-disposition", "attachment; filename=Consulta " + vs_NB_CANDIDATO + " vs. N Puestos.xlsx");
        //                Response.BinaryWrite(((MemoryStream)newStream).ToArray());
        //                Response.End();
        //            }
        //            catch (Exception)
        //            {
        //                UtilMensajes.MensajeDB(rnMensaje, "Ocurrio un error al procesar el Excel, Intente de nuevo.", Entidades.Externas.E_TIPO_RESPUESTA_DB.ERROR);
        //            }
        //            break;

        //        default: break;
        //    }

        //}

        //public void setGraphic(ExcelWorksheet oSheet, List<E_GRAFICA_PUESTO_CANDIDATOS> plstCandidatoPuestos)
        //{
        //    List<E_GRAPHIC_PUESTOS> puestos = new List<E_GRAPHIC_PUESTOS>();
        //    List<E_GRAPHIC_PUESTO_COMPETENCIA> competencias = new List<E_GRAPHIC_PUESTO_COMPETENCIA>();
        //    List<E_GRAPHIC_PUESTO_DATOS> datosPorPuestos = new List<E_GRAPHIC_PUESTO_DATOS>();

        //    string nbCandidato = string.Empty;

        //    var pivot = plstCandidatoPuestos.GroupBy(l => new { l.NB_PUESTO })
        //     .Select(cl => new E_GRAPHIC_PUESTO_DATOS
        //     {
        //         Puesto = cl.First().NB_PUESTO,
        //         Datos = new List<E_GRAPHIC_PUESTOS>()
        //     }).OrderBy(o => o.Puesto).ToList();


        //    foreach (var i in plstCandidatoPuestos)
        //    {
        //        if (pivot.Where(w => w.Puesto == i.NB_PUESTO).FirstOrDefault() != null)
        //        {
        //            pivot.Where(w => w.Puesto == i.NB_PUESTO).FirstOrDefault().Datos.Add(new E_GRAPHIC_PUESTOS { Puesto = i.NB_PUESTO, Competencia = i.NB_COMPETENCIA, valor = i.NO_VALOR_NIVEL });
        //        }
        //    }


        //    var vPuestoCompetencia = plstCandidatoPuestos.Select(s => new { s.NO_VALOR_CANDIDATO, s.NB_CANDIDATO, s.NB_COMPETENCIA }).Distinct().ToList();
        //    foreach (var item in vPuestoCompetencia)
        //    {
        //        nbCandidato = item.NB_CANDIDATO;
        //        competencias.Add(new E_GRAPHIC_PUESTO_COMPETENCIA { Competencia = item.NB_COMPETENCIA, valor = item.NO_VALOR_CANDIDATO });
        //    }

        //    string[] propertyCompetencias = { "Competencia", "valor" };
        //    MemberInfo[] membersToIncludeCompetencias = typeof(E_GRAPHIC_PUESTO_COMPETENCIA)
        //   .GetProperties(BindingFlags.Instance | BindingFlags.Public)
        //   .Where(p => propertyCompetencias.Contains(p.Name))
        //   .ToArray();
        //    oSheet.Cells[200, 4].LoadFromCollection(competencias, true, OfficeOpenXml.Table.TableStyles.Light1, BindingFlags.Instance | BindingFlags.Public, membersToIncludeCompetencias);


        //    var chart = oSheet.Drawings.AddChart("Consultas personales", eChartType.ColumnClustered);
        //    chart.SetPosition(600, 25);
        //    chart.SetSize(940, 20 * 14);


        //    //*************************************************************************
        //    int countInicio = 201;
        //    int countFin = 0;
        //    int rowColumnsNamesPosition = 200;
        //    foreach (var item in pivot)
        //    {
        //        countFin = countInicio + (item.Datos.Count - 1);
        //        var serie1 = chart.Series.Add(oSheet.Cells["C" + countInicio.ToString() + ":C" + countFin.ToString()], oSheet.Cells["B" + countInicio.ToString() + ":B" + countFin.ToString()]);
        //        serie1.Header = item.Puesto;

        //        string[] propertyNames = { "Puesto", "Competencia", "valor" };
        //        MemberInfo[] membersToInclude = typeof(E_GRAPHIC_PUESTOS)
        //       .GetProperties(BindingFlags.Instance | BindingFlags.Public)
        //       .Where(p => propertyNames.Contains(p.Name))
        //       .ToArray();
        //        oSheet.Cells[rowColumnsNamesPosition, 1].LoadFromCollection(item.Datos, true, OfficeOpenXml.Table.TableStyles.Light1, BindingFlags.Instance | BindingFlags.Public, membersToInclude);
        //        rowColumnsNamesPosition = countInicio + (item.Datos.Count - 1) + 1;
        //        countInicio = countInicio + (item.Datos.Count - 1) + 2;

        //    }
        //    //*************************************************************************

        //    var serie2 = chart.Series.Add(oSheet.Cells["E201:E" + (201 + competencias.Count - 1).ToString()], oSheet.Cells["D201:D" + (201 + competencias.Count - 1).ToString()]);
        //    chart.YAxis.MaxValue = 10;
        //    chart.YAxis.MinValue = 0;
        //    chart.Title.Text = "Reporte candidato vs Puestos";
        //    chart.Title.Font.Color = System.Drawing.ColorTranslator.FromHtml("#4F81BD");
        //    chart.Border.Fill.Style = eFillStyle.NoFill;
        //    chart.Title.Font.Size = 9;
        //    chart.YAxis.Font.Size = 8;
        //    chart.XAxis.Font.Size = 8;
        //    chart.Legend.Font.Size = 8;
        //    chart.Legend.Font.Color = System.Drawing.ColorTranslator.FromHtml("#4F81BD");
        //    chart.YAxis.Font.Color = System.Drawing.ColorTranslator.FromHtml("#4F81BD");
        //    chart.XAxis.Font.Color = System.Drawing.ColorTranslator.FromHtml("#4F81BD");
        //    chart.Legend.Position = OfficeOpenXml.Drawing.Chart.eLegendPosition.Bottom;
        //    chart.Style = OfficeOpenXml.Drawing.Chart.eChartStyle.Style10;
        //    serie2.Header = nbCandidato;
        //}



        //public List<E_PROMEDIO> getPromedios()
        //{

        //    vListaPuestos = new List<int>();

        //    if (vIdPuestoCandidato == null)
        //    {

        //        if (vIdCandidatoVsPuesto != Guid.Empty)
        //        {
        //            E_CANDIDATO_VS_PUESTOS oCandidatoPuestos = ContextoConsultasComparativas.oCandidatoVsPuestos.Where(w => w.vIdCandidatoVsPuestos == vIdCandidatoVsPuesto).FirstOrDefault();
        //            vListaPuestos = oCandidatoPuestos.vListaPuestos;
        //        }
        //    }
        //    else
        //    {
        //        vListaPuestos = new List<int>() { (int)vIdPuestoCandidato };
        //    }

        //    var vXelements = vListaPuestos.Select(x =>
        //                                   new XElement("PUESTO",
        //                                   new XAttribute("ID_PUESTO", x)
        //                        ));
        //    XElement SELECCIONADOS =
        //    new XElement("PUESTOS", vXelements
        //        );

        //    ConsultasComparativasNegocio nComparativas = new ConsultasComparativasNegocio();
        //    vCandidatoPuestos = nComparativas.ObtieneCandidatoPuestos(pID_CANDIDATO: vIdCandidato, pXML_PUESTOS: SELECCIONADOS.ToString(), vFgConsultaParcial: vFgConsultaParcial, vFgCalificacionCero: vFgEvaluacionesCero).Select(s => new E_GRAFICA_PUESTO_CANDIDATOS
        //    {
        //        CL_COMPETENCIA = s.CL_COMPETENCIA,
        //        NB_COMPETENCIA = s.NB_COMPETENCIA,
        //        NO_VALOR_NIVEL = s.NO_VALOR_NIVEL,
        //        NB_CANDIDATO = s.NB_CANDIDATO,
        //        NO_VALOR_CANDIDATO = s.NO_VALOR_CANDIDATO,
        //        PR_CANDIDATO_PUESTO = s.PR_CANDIDATO_PUESTO,
        //        DS_COMPETENCIA = s.DS_COMPETENCIA,
        //        ID_BATERIA = s.ID_BATERIA,
        //        NB_PUESTO = s.NB_PUESTO,
        //        CL_PUESTO = s.CL_PUESTO,
        //        CL_COLOR = s.CL_COLOR,
        //        ID_PUESTO = s.ID_PUESTO,
        //        PR_TRUNCADO = CalculaPorcentaje(s.PR_CANDIDATO_PUESTO)
        //    }).OrderBy(s => s.CL_COMPETENCIA).ToList();


        //    List<E_PROMEDIO> vlstPromedios = new List<E_PROMEDIO>();
        //    foreach (var item in vListaPuestos)
        //    {
        //        List<E_PROMEDIO> vlist = new List<E_PROMEDIO>();
        //        foreach (var i in vCandidatoPuestos)
        //        {
        //            if (item == i.ID_PUESTO && i.NO_VALOR_NIVEL != 0)
        //            {
        //                vlist.Add(new E_PROMEDIO { NB_PUESTO = i.NB_PUESTO, PORCENTAJE = i.PR_TRUNCADO });
        //            }
        //        }
        //        vlstPromedios.Add(new E_PROMEDIO { NB_PUESTO = vlist.Select(s => s.NB_PUESTO).FirstOrDefault(), PROMEDIO = vlist.Average(s => s.PORCENTAJE) });
        //    }

        //    return vlstPromedios;
        //}
        //public void pivotData(ExcelWorksheet oSheet)
        //{
        //    var query = vCandidatoPuestos.GroupBy(t => new { t.CL_COMPETENCIA, t.NB_COMPETENCIA, t.DS_COMPETENCIA, t.CL_COLOR }).Select(g => new
        //    {
        //        Clave = g.Key.CL_COMPETENCIA,
        //        Nombre = g.Key.NB_COMPETENCIA,
        //        Descripcion = g.Key.DS_COMPETENCIA,
        //        color = g.Key.CL_COLOR,
        //        Details = g.GroupBy(t => t.NB_PUESTO).Select(s => new
        //        {
        //            Puesto = s.Key,
        //            Avg = s.Average(c => c.PR_TRUNCADO)
        //        }).ToList()
        //    });


        //    string mensaje = string.Empty;
        //    int start = 2;

        //    oSheet.Cells[start - 1, 5].Value = "Competencia";
        //    oSheet.Cells[start - 1, 5].Style.Font.Bold = true;
        //    oSheet.Cells[start - 1, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //    oSheet.Cells[start - 1, 5].Style.Fill.BackgroundColor.SetColor(Color.White);

        //    oSheet.Cells[start - 1, 6].Value = "Descripción";
        //    oSheet.Cells[start - 1, 6].Style.Font.Bold = true;
        //    oSheet.Cells[start - 1, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //    oSheet.Cells[start - 1, 6].Style.Fill.BackgroundColor.SetColor(Color.White);
        //    int columnsNames = 6;
        //    if (query.FirstOrDefault() != null)
        //    {
        //        for (int i = 0; i < query.FirstOrDefault().Details.Count; i++)
        //        {
        //            oSheet.Cells[start - 1, (columnsNames + 1)].Value = query.FirstOrDefault().Details[i].Puesto;
        //            oSheet.Cells[start - 1, (columnsNames + 1)].Style.Font.Bold = true;
        //            oSheet.Cells[start - 1, (columnsNames + 1)].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            oSheet.Cells[start - 1, (columnsNames + 1)].Style.Fill.BackgroundColor.SetColor(Color.White);
        //            columnsNames++;
        //        }
        //    }

        //    int contador = 1;
        //    foreach (var i in query)
        //    {
        //        oSheet.Cells[start, 4].Value = string.Empty;
        //        oSheet.Cells[start, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //        oSheet.Cells[start, 4].Style.Fill.BackgroundColor.SetColor(getColor(i.color));

        //        oSheet.Cells[start, 5].Value = i.Nombre;
        //        oSheet.Cells[start, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //        oSheet.Cells[start, 5].Style.Fill.BackgroundColor.SetColor((contador) % 2 == 0
        //           ? Color.White
        //           : Color.LightGray);

        //        oSheet.Cells[start, 6].Value = i.Descripcion;
        //        oSheet.Cells[start, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //        oSheet.Cells[start, 6].Style.Fill.BackgroundColor.SetColor((contador) % 2 == 0
        //           ? Color.White
        //           : Color.LightGray);

        //        int contadorColums = 6;
        //        for (int j = 0; j < i.Details.Count; j++)
        //        {
        //            oSheet.Cells[start, contadorColums + (j + 1)].Value = i.Details.ElementAt(j).Avg;
        //            oSheet.Cells[start, contadorColums + (j + 1)].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            oSheet.Cells[start, contadorColums + (j + 1)].Style.Fill.BackgroundColor.SetColor((contador) % 2 == 0
        //               ? Color.White
        //               : Color.LightGray);
        //        }
        //        contador++;
        //        start++;
        //    }
        //}


        //public Color getColor(string pClColor)
        //{
        //    //MediumOrchid
        //    //Yellow
        //    //SkyBlue
        //    //LawnGreen
        //    //OrangeRed
        //    //Orange

        //    switch (pClColor)
        //    {
        //        case "MediumOrchid": return Color.MediumOrchid;
        //        case "Yellow": return Color.Yellow;
        //        case "LawnGreen": return Color.LawnGreen;
        //        case "OrangeRed": return Color.OrangeRed;
        //        case "Orange": return Color.Orange;
        //        case "SkyBlue": return Color.SkyBlue;
        //        default: return Color.LightGray;
        //    }
        //}

        public HtmlGenericControl CrearTabla(List<E_GRAFICA_PUESTO_CANDIDATOS> pValoresTabla)
        {
            var vPuestoCompetencia = pValoresTabla.Select(s => new { s.NO_VALOR_CANDIDATO, s.NB_CANDIDATO, s.NB_COMPETENCIA, s.CL_COMPETENCIA, s.DS_COMPETENCIA, s.CL_SOLICITUD }).Distinct().ToList();

            HtmlGenericControl vCtrlTabla = new HtmlGenericControl("table");
            vCtrlTabla.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 8pt;");
            HtmlGenericControl vCtrlColumn = new HtmlGenericControl("tr");
            vCtrlColumn.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 8pt;");

            HtmlGenericControl vCtrlTh2 = new HtmlGenericControl("th");
            vCtrlTh2.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 8pt;");
            vCtrlTh2.InnerText = String.Format("{0}", "Competencia");
            vCtrlColumn.Controls.Add(vCtrlTh2);

            HtmlGenericControl vCtrlTh3 = new HtmlGenericControl("th");
            vCtrlTh3.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 8pt;");
            vCtrlTh3.InnerText = String.Format("{0}", "Descripción");
            vCtrlColumn.Controls.Add(vCtrlTh3);

            foreach (var item in vListaPuestos)
            {
                string vNbPuesto = "";
                if (pValoresTabla.Where(w => w.ID_PUESTO == item).FirstOrDefault() != null)
                {
                    vNbPuesto = pValoresTabla.Where(w => w.ID_PUESTO == item).FirstOrDefault().NB_PUESTO.ToString();
                    HtmlGenericControl vCtrlThPuesto = new HtmlGenericControl("th");
                    vCtrlThPuesto.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 8pt;");
                    HtmlGenericControl vCtrlDiv = new HtmlGenericControl("div");
                    vCtrlDiv.Attributes.Add("style", "writing-mode:tb-rl; height: 250px;");
                    vCtrlDiv.InnerText = String.Format("{0}", vNbPuesto);
                    vCtrlThPuesto.Controls.Add(vCtrlDiv);
                    vCtrlColumn.Controls.Add(vCtrlThPuesto);
                }
            }
            vCtrlTabla.Controls.Add(vCtrlColumn);

            foreach (var item in vPuestoCompetencia)
            {
                HtmlGenericControl vCtrlRow = new HtmlGenericControl("tr");

                HtmlGenericControl vCtrlNbCompetencia = new HtmlGenericControl("td");
                vCtrlNbCompetencia.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 8pt;");
                vCtrlNbCompetencia.InnerText = String.Format("{0}", item.NB_COMPETENCIA);
                vCtrlRow.Controls.Add(vCtrlNbCompetencia);


                HtmlGenericControl vCtrlDsCompetencia = new HtmlGenericControl("td");
                vCtrlDsCompetencia.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 8pt;");
                vCtrlDsCompetencia.InnerText = String.Format("{0}", item.DS_COMPETENCIA);
                vCtrlRow.Controls.Add(vCtrlDsCompetencia);

                foreach (var vValor in vListaPuestos)
                {
                    var vCumplimientoCandidato = pValoresTabla.Where(w => w.ID_PUESTO == vValor && w.CL_COMPETENCIA == item.CL_COMPETENCIA).FirstOrDefault();
                    if (vCumplimientoCandidato != null)
                    {
                        HtmlGenericControl vCtrlPrCandidato = new HtmlGenericControl("td");
                        vCtrlPrCandidato.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 8pt;");
                        vCtrlPrCandidato.InnerText = String.Format("{0}%", vCumplimientoCandidato.PR_CANDIDATO_PUESTO);
                        vCtrlRow.Controls.Add(vCtrlPrCandidato);
                    }
                }
                vCtrlTabla.Controls.Add(vCtrlRow);
            }
            return vCtrlTabla;
        }
    }
}