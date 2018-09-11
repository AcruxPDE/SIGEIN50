using SIGE.Entidades;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Entidades.Externas;
using SIGE.Negocio.EvaluacionOrganizacional;
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
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.EO
{
    public partial class VentanaComparativaIndividual : System.Web.UI.Page
    {

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        public int vWidthDiv;
        public int vRow;

        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private XElement SELECCIONPERIODOS { get; set; }

        private DataTable vResultados
        {
            get { return (DataTable)ViewState["vs_vTablaMetas"]; }
            set { ViewState["vs_vTablaMetas"] = value; }
        }

        private List<E_SELECCION_PERIODOS_DESEMPENO> vListaPeriodos
        {
            get { return (List<E_SELECCION_PERIODOS_DESEMPENO>)ViewState["vs_vListaPeriodos"]; }
            set { ViewState["vs_vListaPeriodos"] = value; }
        }

        private List<E_PERIODOS_COMPARAR> vPeriodosComparar
        {
            get { return (List<E_PERIODOS_COMPARAR>)ViewState["vs_vPeriodosComparar"]; }
            set { ViewState["vs_vPeriodosComparar"] = value; }
        }

        private List<E_METAS_COMPARACION_DESEMPENO> vMetasPeriodo
        {
            get { return (List<E_METAS_COMPARACION_DESEMPENO>)ViewState["vs_vMetasPeriodo"]; }
            set { ViewState["vs_vMetasPeriodo"] = value; }
        }

        private List<E_METAS_PERIODOS> vMetas
        {
            get { return (List<E_METAS_PERIODOS>)ViewState["vs_vMetas"]; }
            set { ViewState["vs_vMetas"] = value; }
        }

        private List<E_METAS_EVALUADO_PERIODO> vListaMetasEvalPeriodo
        {
            get { return (List<E_METAS_EVALUADO_PERIODO>)ViewState["vs_vListaMetasEvalPeriodo"]; }
            set { ViewState["vs_vListaMetasEvalPeriodo"] = value; }
        }

        private decimal? vResultado
        {
            get { return (decimal?)ViewState["vs_vResultado"]; }
            set { ViewState["vs_vResultado"] = value; }
        }

        private int? vIdEvaluado
        {
            get { return (int?)ViewState["vs_vIdEvaluado"]; }
            set { ViewState["vs_vIdEvaluado"] = value; }
        }

        private int? vIdEmpleado
        {
            get { return(int?)ViewState["vs_vIdEmpleado"];}
            set { ViewState["vs_vIdEmpleado"]=value;}
        }

        #endregion

        #region Funciones

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
                if (colname == "Evidencias")
                {
                    vControlGrid = new HtmlGenericControl("div");
                    vControlGrid.ID = "DivGridEvidencias";
                    container.Controls.Add(vControlGrid);
                }
                else if (colname == "Color")
                {
                    vControlGrid = new HtmlGenericControl("div");
                    vControlGrid.ID = "DivGridColor";
                    vControlGrid.Style.Add("height", "30px");
                    vControlGrid.Style.Add("width", "60px");
                    vControlGrid.Style.Add("border-radius", "5px");
                    container.Controls.Add(vControlGrid);
                }
            }
        }

        public DataTable ObtieneMetasComparativaGrafica(string vXmlPeriodos, ref List<E_PERIODOS_COMPARAR> vListaPeriodos)
        {
            PeriodoDesempenoNegocio oPeriodo = new PeriodoDesempenoNegocio();
            List<E_PERIODOS_COMPARAR> vListaPeriodosGrafica = new List<E_PERIODOS_COMPARAR>();

            vListaPeriodosGrafica = oPeriodo.ObtenerDesempenoComparacion(vXmlPeriodos).ToList();

            DataTable vDtPivot = new DataTable();

            vDtPivot.Columns.Add("NO_META_EVALUA", typeof(string));
            vDtPivot.Columns.Add("DS_META", typeof(string));

            List<E_METAS_PERIODO_COMPARACION> vListaMetasGrafica = new List<E_METAS_PERIODO_COMPARACION>();
            List<E_METAS_PERIODO_COMPARACION> vListaMetasGraficaFinal = new List<E_METAS_PERIODO_COMPARACION>();
            
            foreach (var item in vListaPeriodosGrafica)
            {
                //item.NB_PERIODO_ENCABEZADO = item.NB_PERIODO;

                //if (vDtPivot.Columns.Contains(item.NB_PERIODO_ENCABEZADO))
                //{
                //    item.NB_PERIODO_ENCABEZADO = item.NB_PERIODO_ENCABEZADO + "1";
                //}

                vDtPivot.Columns.Add(item.NB_PERIODO_ENCABEZADO, typeof(string));
                vListaMetasGrafica = oPeriodo.ObtieneMetasComparacion(idEvaluadoMeta: null, pIdPeriodo: item.ID_PERIODO, idEvaluado: vIdEvaluado).ToList();

                foreach (E_METAS_PERIODO_COMPARACION items in vListaMetasGrafica)
                {
                    vListaMetasGraficaFinal.Add(new E_METAS_PERIODO_COMPARACION
                            {
                                ID_EVALUADO_META = items.ID_EVALUADO_META,
                                ID_PERIODO = items.ID_PERIODO,
                                ID_EVALUADO = items.ID_EVALUADO,
                                NO_META = items.NO_META,
                                DS_FUNCION = items.DS_FUNCION,
                                NB_INDICADOR = items.NB_INDICADOR,
                                DS_META = items.DS_META,
                                CL_TIPO_META = items.CL_TIPO_META,
                                FG_VALIDA_CUMPLIMIENTO = items.FG_VALIDA_CUMPLIMIENTO,
                                FG_EVALUAR = items.FG_EVALUAR,
                                NB_CUMPLIMIENTO_ACTUAL = items.NB_CUMPLIMIENTO_ACTUAL,
                                NB_CUMPLIMIENTO_MINIMO = items.NB_CUMPLIMIENTO_MINIMO,
                                NB_CUMPLIMIENTO_SATISFACTORIO = items.NB_CUMPLIMIENTO_SATISFACTORIO,
                                NB_CUMPLIMIENTO_SOBRESALIENTE = items.NB_CUMPLIMIENTO_SOBRESALIENTE,
                                PR_META = items.PR_META,
                                NB_RESULTADO = items.NB_RESULTADO,
                                PR_RESULTADO = items.PR_RESULTADO,
                                CL_NIVEL = items.CL_NIVEL,
                                PR_CUMPLIMIENTO_META = items.PR_CUMPLIMIENTO_META,
                                FG_EVIDENCIA = items.FG_EVIDENCIA,
                                PR_EVALUADO = items.PR_EVALUADO,
                                NIVEL_ALZANZADO = items.NIVEL_ALZANZADO,
                                COLOR_NIVEL = items.COLOR_NIVEL
                            }
                            );
                }
            }

            List<E_META> vMetasGrafica = oPeriodo.ObtenerMetasGrafica(SELECCIONPERIODOS.ToString(), vIdEmpleado).Select(s => new E_META { NO_META = s.NO_META.ToString(), DS_META = s.DS_META, PR_CUMPLIMIENTO = s.PR_CUMPLIMIENTO_META, COLOR_NIVEL = s.COLOR_NIVEL, NB_PERIODO = s.NB_PERIODO }).ToList();
            vMetas = new List<E_METAS_PERIODOS>();
            vRow = 1;

            foreach (E_META item in vMetasGrafica.OrderBy(o => o.DS_META))
            {
                bool exists = vMetas.Exists(element => element.DS_META == item.DS_META);
                if (!exists)
                {
                    E_METAS_PERIODOS f = new E_METAS_PERIODOS
                    {
                        NO_META_EVALUA = vRow,
                        NO_META = item.NO_META,
                        DS_META = item.DS_META,
                        NB_PERIODO = item.NB_PERIODO,
                        PR_CUMPLIMIENTO = item.PR_CUMPLIMIENTO,
                        COLOR_NIVEL = item.COLOR_NIVEL
                    };
                    vMetas.Add(f);
                    vRow++;
                }
            }


            foreach (var vPues in vMetas.OrderBy(o => o.DS_META))
            {
                DataRow vDr = vDtPivot.NewRow();
                vDr["NO_META_EVALUA"] = vPues.NO_META_EVALUA;
                vDr["DS_META"] = vPues.DS_META;

                foreach (var vCom in vListaPeriodosGrafica.OrderBy(o => o.ID_PERIODO))
                {
                    
                    var vResultado = vListaMetasGraficaFinal.OrderBy(o => o.ID_PERIODO).Where(t => t.ID_PERIODO == vCom.ID_PERIODO && t.DS_META == vPues.DS_META).FirstOrDefault();
                    if (vResultado != null)
                    {
                        if (vResultado.PR_CUMPLIMIENTO_META != null)
                            vDr[vCom.NB_PERIODO_ENCABEZADO.ToString()] = vResultado.PR_CUMPLIMIENTO_META + "%";
                        else
                            vDr[vCom.NB_PERIODO_ENCABEZADO.ToString()] = "0.00%";
                    }
                    else
                    {
                        vDr[vCom.NB_PERIODO_ENCABEZADO.ToString()] = "N/A";
                    }
                }
                vDtPivot.Rows.Add(vDr);
            }
            return vDtPivot;
        }

        void vGrid_ItemDataBound(object sender, GridItemEventArgs e)
        {
            int vEvaluadoMeta, vPeriodoMeta;
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                HtmlGenericControl DivGridColor = (HtmlGenericControl)item.FindControl("DivGridColor");
                vEvaluadoMeta = int.Parse(item.GetDataKeyValue("ID_EVALUADO_META").ToString());
                vPeriodoMeta = int.Parse(item.GetDataKeyValue("ID_PERIODO").ToString());
                PeriodoDesempenoNegocio nDesempeno = new PeriodoDesempenoNegocio();
                var vMetasEvaluado = nDesempeno.ObtieneMetasComparacion(idEvaluadoMeta: vEvaluadoMeta, idEvaluado: vIdEvaluado, pIdPeriodo: vPeriodoMeta).FirstOrDefault();
                if (vMetasEvaluado != null)
                {
                    DivGridColor.Style.Add("background", vMetasEvaluado.COLOR_NIVEL);
                    DivGridColor.Attributes.Add("title", vMetasEvaluado.NIVEL_ALZANZADO);
                    HtmlGenericControl vControlGridEvidencias = (HtmlGenericControl)item.FindControl("DivGridEvidencias");
                    System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                    if (vMetasEvaluado.FG_EVIDENCIA == true)
                        img.Attributes["src"] = "../Assets/images/Aceptar.png";
                    else
                        img.Attributes["src"] = "../Assets/images/Cancelar.png";
                    vControlGridEvidencias.Controls.Add(img);
                }
                else
                {
                   // DivGridColor.Style.Add("background", "#F2F2F2");
                    DivGridColor.InnerText = "N/A";
                    DivGridColor.Attributes.Add("title", "N/A");
                    HtmlGenericControl vControlGridEvidencias = (HtmlGenericControl)item.FindControl("DivGridEvidencias");
                    System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                        img.Attributes["src"] = "../Assets/images/Cancelar.png";
                    vControlGridEvidencias.Controls.Add(img);
                }

            }
        }

        void vGridResultados_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
            }
        }

        public string GeneraColor(int pNumPeriodo)
        {
            string vColor = "";
            switch(pNumPeriodo)
            {
                case 1:
                    vColor = "#fffc96";
                    break;
                case 2:
                    vColor = "#ffcc96";
                    break;
                case 3:
                    vColor = "#c0f6d9";
                    break;
                case 4:
                    vColor = "#ece0f4";
                    break;
                case 5:
                    vColor = "#d79d9d";
                    break;
                case 6:
                    vColor = "#a9f7ff";
                    break;
                case 7:
                    vColor = "#e0162b";
                    break;
                case 8:
                    vColor = "#d0e1f9";
                    break;
                case 9:
                    vColor = "#136310";
                    break;
                case 10:
                    vColor = "#db7b2c";
                    break;
                case 11:
                    vColor = "#6497b1";
                    break;
                case 12:
                    vColor = "#efcbff";
                    break;
                case 13:
                    vColor = "#d48e8e";
                    break;
                case 14:
                    vColor = "#caeabb";
                    break;
                case 15:
                    vColor = "#ccb8d0";
                    break;
                case 16:
                    vColor = "#cbe2ee";
                    break;
                case 17:
                    vColor = "#f65555";
                    break;
                case 18:
                    vColor = "#fdfaaf";
                    break;
                case 19:
                    vColor = "#3fb5e5";
                    break;
                case 20:
                    vColor = "#aacaf6";
                    break;
                case 21:
                    vColor = "#cc8f66";
                    break;
                case 22:
                    vColor = "#ffede1";
                    break;
                case 23:
                    vColor = "#ffe05d";
                    break;
                case 24:
                    vColor = "#5d7cff";
                    break;
                case 25:
                    vColor = "#ffe05d";
                    break;
                case 26:
                    vColor = "#fbead5";
                    break;
                case 27:
                    vColor = "#ecba7d";
                    break;
                case 28:
                    vColor = "#b2d088";
                    break;
                case 29:
                    vColor = "#467240";
                    break;
                case 30:
                    vColor = "#7a7565";
                    break;
                case 31:
                    vColor = "#4285f4";
                    break;
                case 32:
                    vColor = "#34a853";
                    break;
                case 33:
                    vColor = "#fbbc05";
                    break;
                case 34:
                    vColor = "#ea4335";
                    break;
                case 35:
                    vColor = "#78c31e";
                    break;
                case 36:
                    vColor = "#388fac";
                    break;
                case 37:
                    vColor = "#f69d49";
                    break;
                case 38:
                    vColor = "#33ab81";
                    break;
                case 39:
                    vColor = "#554ac2";
                    break;
                case 40:
                    vColor = "#58d6c2";
                    break;
                case 41:
                    vColor = "#b84069";
                    break;
                case 42:
                    vColor = "#7aadf0";
                    break;
                case 43:
                    vColor = "#ddb7e5";
                    break;
                case 44:
                    vColor = "#ecba45";
                    break;
                case 45:
                    vColor = "#e0fcff";
                    break;
                case 46:
                    vColor = "#7b89b8";
                    break;
                case 47:
                    vColor = "#92e0ec";
                    break;
                case 48:
                    vColor = "#e3c28f";
                    break;
                case 49:
                    vColor = "#ece4b9";
                    break;
                case 50:
                    vColor = "#3b784d";
                    break;
                default:
                    vColor = "#d9e2e5";
                    break;
            }


            return vColor;
        }

        public string GeneraFooter(int pIdPeriodo)
        {
            string vFooter = "";

            PeriodoDesempenoNegocio nDesempenoGlobal = new PeriodoDesempenoNegocio();
            List<E_OBTIENE_CUMPLIMIENTO_GLOBAL> lDesempenoGlobal = new List<E_OBTIENE_CUMPLIMIENTO_GLOBAL>();
            lDesempenoGlobal = nDesempenoGlobal.ObtieneCumplimientoGlobal(pIdPeriodo);

            vFooter = lDesempenoGlobal.Where(w => w.ID_EMPLEADO == vIdEmpleado).FirstOrDefault().PR_CUMPLIMIENTO_EVALUADO.ToString();

            return vFooter + "%";
        }

        public void PintarResultadosGrafica()
        {
            PeriodoDesempenoNegocio nDesempeno = new PeriodoDesempenoNegocio();
            GridBoundColumn vColumn = new GridBoundColumn();
            vColumn.HeaderText = "No. Meta";
            vColumn.DataField = "NO_META_EVALUA";
            vColumn.HeaderStyle.Width = 60;
            vColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            vColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            vColumn.HeaderStyle.Font.Bold = true;
            vGridResultados.Columns.Add(vColumn);
            GridBoundColumn vColumnPr = new GridBoundColumn();
            vColumnPr.HeaderText = "Meta";
            vColumnPr.DataField = "DS_META";
            vColumnPr.HeaderStyle.Width = 200;
            vColumnPr.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            vColumnPr.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
            vColumnPr.HeaderStyle.Font.Bold = true;
            vColumnPr.FooterText = "Total:";
            vColumnPr.FooterStyle.Font.Bold = true;
            vColumnPr.FooterStyle.HorizontalAlign = HorizontalAlign.Right;
            vGridResultados.Columns.Add(vColumnPr);

            foreach (var items in vPeriodosComparar)
            {
                GridBoundColumn ColumnPeriodo = new GridBoundColumn();
                ColumnPeriodo.HeaderText = items.NB_PERIODO + "&nbsp;&nbsp;<span style=\"border: 1px solid gray; border-radius: 5px; background:" + GeneraColor((int)items.NUM_PERIODO) +";\">&nbsp;&nbsp;</span>";
                ColumnPeriodo.DataField = items.NB_PERIODO_ENCABEZADO;
                ColumnPeriodo.HeaderStyle.Width = 200;
                ColumnPeriodo.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                ColumnPeriodo.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                ColumnPeriodo.HeaderStyle.Font.Bold = true;
                ColumnPeriodo.FooterStyle.Font.Bold = true;
                ColumnPeriodo.FooterStyle.HorizontalAlign = HorizontalAlign.Center;
                ColumnPeriodo.FooterText = GeneraFooter(items.ID_PERIODO);
                vGridResultados.Columns.Add(ColumnPeriodo);
            }
            List<E_PERIODOS_COMPARAR> vListaPeriodos = new List<E_PERIODOS_COMPARAR>();
            vResultados = ObtieneMetasComparativaGrafica(SELECCIONPERIODOS.ToString(), ref vListaPeriodos);
        }

        public void PintarReporte()
        {
            vPeriodosComparar = new List<E_PERIODOS_COMPARAR>();
            vMetasPeriodo = new List<E_METAS_COMPARACION_DESEMPENO>();
            PeriodoDesempenoNegocio nDesempeno = new PeriodoDesempenoNegocio();
            vPeriodosComparar = nDesempeno.ObtenerDesempenoComparacion(SELECCIONPERIODOS.ToString());
            int vCuentaPeriodos = nDesempeno.ObtenerDesempenoComparacion(SELECCIONPERIODOS.ToString()).Count;
            vWidthDiv = vCuentaPeriodos * 580;

            if (vCuentaPeriodos > 2)
                btnCancelar.Visible = false;

            dvReporte.Style.Add("width", vWidthDiv.ToString() + "px");
            foreach (E_PERIODOS_COMPARAR item in vPeriodosComparar)
            {
                vResultado = 0;
                HtmlGenericControl vControlGrid = new HtmlGenericControl("div");
                vControlGrid.Attributes.Add("class", "ctrlBasico");
                string vRows = "<table class='ctrlTableForm' style='max-width: 460' >" +
                                 "<tr><td class='ctrlTableDataContext'><b>Puesto: </b></td><td colspan='2' class='ctrlTableDataBorderContext'>" + item.NB_PUESTO + "</td></tr>" +
                                "<tr><td class='ctrlTableDataContext'><b>Período: </b></td><td colspan='2' class='ctrlTableDataBorderContext'>" + item.NB_PERIODO + "</td></tr>" +
                                "<tr><td class='ctrlTableDataContext'><b>Fechas: </b></td><td colspan='2' class='ctrlTableDataBorderContext'>" + item.FE_INICIO.ToString("dd-MM-yyyy") + " a " + item.FE_TERMINO.ToString("dd-MM-yyyy") + "</td></tr></table>";
                vControlGrid.InnerHtml = vRows;
               // vMetasPeriodo = nDesempeno.ObtieneMetasComparacion(idEvaluadoMeta: null, pIdPeriodo: item.ID_PERIODO, idEvaluado: vIdEvaluado);
                vMetasPeriodo = nDesempeno.ObtieneMetasPeriodoComparar(SELECCIONPERIODOS.ToString(), idEvaluado: vIdEvaluado, pIdPeriodo: item.ID_PERIODO);
                int vHeight = nDesempeno.ObtieneMetasPeriodoComparar(SELECCIONPERIODOS.ToString(), idEvaluado: vIdEvaluado, pIdPeriodo: item.ID_PERIODO).Count;
                foreach (E_METAS_COMPARACION_DESEMPENO elemen in vMetasPeriodo)
                {
                    vResultado = vResultado + elemen.PR_CUMPLIMIENTO_META;
                }

                if (vHeight == 1)
                    vHeight = 2;

                RadGrid vGrid = new RadGrid()
                {
                    ID = "rgEvaluado" + item.CL_PERIODO + item.ID_PERIODO.ToString(),
                    Width = 560,
                    CssClass = "cssGrid",
                    AutoGenerateColumns = false,
                };
                GridColumnGroup columnGroup = new GridColumnGroup();
                vGrid.MasterTableView.ColumnGroups.Add(columnGroup);
                vGrid.ShowFooter = true;
                vGrid.ClientSettings.Scrolling.UseStaticHeaders = true;
                vGrid.FooterStyle.Font.Bold = true;
                columnGroup.HeaderText = "METAS EVALUADAS";
                columnGroup.Name = "metas";
                columnGroup.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                columnGroup.HeaderStyle.Font.Bold = true;
                columnGroup.HeaderStyle.BackColor = Color.FromArgb(162, 8, 0);
                columnGroup.HeaderStyle.ForeColor = Color.White;
                vGrid.MasterTableView.DataKeyNames = new string[] { "ID_EVALUADO_META", "ID_PERIODO" };
                vGrid.ItemDataBound += new GridItemEventHandler(vGrid_ItemDataBound);
                DataTable dataTable = new DataTable();
                GridBoundColumn vColumn = new GridBoundColumn();
                vColumn.HeaderText = "Meta";
                vColumn.DataField = "DS_META";
                vColumn.HeaderStyle.Width = 150;
                vColumn.ColumnGroupName = "metas";
                vColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                vColumn.HeaderStyle.Font.Bold = true;
                vGrid.Columns.Add(vColumn);
                GridBoundColumn vColumnPr = new GridBoundColumn();
                vColumnPr.HeaderText = "Ponderación";
                vColumnPr.DataField = "PR_EVALUADO";
                vColumnPr.HeaderStyle.Width = 100;
                vColumnPr.ColumnGroupName = "metas";
                vColumnPr.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                vColumnPr.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                vColumnPr.HeaderStyle.Font.Bold = true;
                //vColumnPr.DataFormatString = "{0:N2}%";
                vGrid.Columns.Add(vColumnPr);
                GridTemplateColumn templateColumn = new GridTemplateColumn();
                templateColumn.HeaderText = "Nivel alcanzado";
                templateColumn.ColumnGroupName = "metas";
                templateColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                templateColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                templateColumn.HeaderStyle.Font.Bold = true;
                templateColumn.UniqueName = item.ToString();
                templateColumn.HeaderStyle.Width = 80;
                templateColumn.ItemTemplate = new MyTemplate("Color");
                vGrid.Columns.Add(templateColumn);
                GridBoundColumn vColumnCum = new GridBoundColumn();
                vColumnCum.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                vColumnCum.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                vColumnCum.HeaderText = "Cumplimiento";
                vColumnCum.ColumnGroupName = "metas";
                vColumnCum.DataField = "PR_CUMPLIMIENTO_META_STR";
                //vColumnCum.Aggregate = GridAggregateFunction.Sum;
                //vColumnCum.FooterAggregateFormatString = "Total: {0:N2}%";
                //vColumnCum.DataFormatString = "{0:N2}%";
                vColumnCum.FooterText = vResultado.ToString() + "%";

                if (vResultado < 1)
                    vColumnCum.FooterStyle.BackColor = Color.Gray;
                if (vResultado > 0 && vResultado < 60)
                {
                    vColumnCum.FooterStyle.BackColor = Color.Red;
                    vColumnCum.FooterStyle.ForeColor = Color.White;
                }
                if (vResultado > 59 && vResultado < 76)
                    vColumnCum.FooterStyle.BackColor = Color.Yellow;
                if (vResultado > 75)
                {
                    vColumnCum.FooterStyle.BackColor = Color.Green;
                    vColumnCum.FooterStyle.ForeColor = Color.White;
                }
                vColumnCum.HeaderStyle.Width = 130;
                vColumnCum.HeaderStyle.Font.Bold = true;
                vColumnCum.FooterStyle.HorizontalAlign = HorizontalAlign.Center;
                vGrid.Columns.Add(vColumnCum);
                GridTemplateColumn templateColumnArchivo = new GridTemplateColumn();
                templateColumnArchivo.HeaderText = "Evidencia";
                templateColumnArchivo.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                templateColumnArchivo.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                templateColumnArchivo.HeaderStyle.Width = 90;
                templateColumnArchivo.ColumnGroupName = "metas";
                templateColumnArchivo.HeaderStyle.Font.Bold = true;
                templateColumnArchivo.ItemTemplate = new MyTemplate("Evidencias");
                vGrid.Columns.Add(templateColumnArchivo);
                vGrid.DataSource = vMetasPeriodo;
                vGrid.ClientSettings.Selecting.AllowRowSelect = false;
                vGrid.ClientSettings.Scrolling.AllowScroll = true;
                HtmlGenericControl vContenedorControlGrid = new HtmlGenericControl("div");
                vContenedorControlGrid.Attributes.Add("class", "ctrlBasico");
                vContenedorControlGrid.Controls.Add(vGrid);
                vControlGrid.Controls.Add(vContenedorControlGrid);
                dvReporte.Controls.Add(vControlGrid);
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PeriodoDesempenoNegocio nDesempeno = new PeriodoDesempenoNegocio();
                vListaPeriodos = new List<E_SELECCION_PERIODOS_DESEMPENO>();

                if (Request.Params["ID_EVALUADO"] != null)
                {
                    vIdEvaluado = int.Parse(Request.Params["ID_EVALUADO"].ToString());
                }

                if (Request.Params["ID_EMPLEADO"] != null)
                {
                    vIdEmpleado = int.Parse(Request.Params["ID_EMPLEADO"].ToString());
                }

                    if (Request.Params["CL_ORIGEN"] != null)
                    {
                        if (Request.Params["CL_ORIGEN"] == "GLOBAL")
                        {

                            if (ContextoPeriodos.oLstPeriodos != null)
                            {
                                int i = 1;
                                foreach (E_SELECCION_PERIODOS_DESEMPENO item in ContextoPeriodos.oLstPeriodos.OrderBy(s => s.idPeriodo))
                                {

                                    vListaPeriodos.Add(new E_SELECCION_PERIODOS_DESEMPENO
                                    {
                                        numPeriodo = i,
                                        idPeriodo = item.idPeriodo,
                                        idEvaluado = vIdEvaluado,
                                        nbPeriodo = item.nbPeriodo
                                    });
                                    i++;
                                }
                            }

                        }
                    }
                    else
                    {
                        if (ContextoPeriodos.oLstPeriodosPersonal != null)
                        {
                            int i = 1;
                            foreach (E_SELECCION_PERIODOS_DESEMPENO item in ContextoPeriodos.oLstPeriodosPersonal.OrderBy(s => s.idPeriodo))
                            {
                                vListaPeriodos.Add(new E_SELECCION_PERIODOS_DESEMPENO
                                {
                                    numPeriodo = i,
                                    idPeriodo = item.idPeriodo,
                                    idEvaluado = item.idEvaluado,
                                    nbPeriodo = item.nbPeriodo
                                }
                                    );
                                i++;
                            }
                        }
                    }

                    var vXelements = vListaPeriodos.Select(x => new XElement("PERIODOS",
                                                                 new XAttribute("ID_PERIODO", x.idPeriodo),
                                                                 new XAttribute("ID_EVALUADO", x.idEvaluado)));
                    SELECCIONPERIODOS = new XElement("SELECCIONADOS", vXelements);

                    //GRÁFICA
                    List<E_META> vMetasGrafica = nDesempeno.ObtenerMetasGrafica(SELECCIONPERIODOS.ToString(), vIdEmpleado).Select(s => new E_META { NO_META = s.NO_META.ToString(), DS_META = s.DS_META, PR_CUMPLIMIENTO = s.PR_CUMPLIMIENTO_META, COLOR_NIVEL = s.COLOR_NIVEL, NB_PERIODO = s.NB_PERIODO }).ToList();
                    vMetas = new List<E_METAS_PERIODOS>();
                    rhcCumplimientoPersonal.PlotArea.Series.Clear();
                    rhcCumplimientoPersonal.PlotArea.YAxis.LabelsAppearance.DataFormatString = "{0:N2}%";
                    vRow = 1;

                    foreach (E_META item in vMetasGrafica.OrderBy(o => o.DS_META))
                    {
                        bool exists = vMetas.Exists(element => element.DS_META == item.DS_META);
                        if (!exists)
                        {
                            E_METAS_PERIODOS f = new E_METAS_PERIODOS
                            {
                                NO_META_EVALUA = vRow,
                                NO_META = item.NO_META,
                                DS_META = item.DS_META,
                                NB_PERIODO = item.NB_PERIODO,
                                PR_CUMPLIMIENTO = item.PR_CUMPLIMIENTO,
                                COLOR_NIVEL = item.COLOR_NIVEL
                            };
                            vMetas.Add(f);
                            vRow++;
                        }
                    }

                    foreach (var Metas in vMetas.OrderBy(o => o.DS_META))
                    {
                        rhcCumplimientoPersonal.PlotArea.XAxis.Items.Add("Meta " + Metas.NO_META_EVALUA);
                    }

                    List<E_SELECCION_PERIODOS_DESEMPENO> vListaPeriodosGraf = new List<E_SELECCION_PERIODOS_DESEMPENO>();
                    vListaPeriodosGraf = vListaPeriodos.OrderBy(o => o.idPeriodo).ToList();

                    foreach (var items in vListaPeriodosGraf.OrderBy(o => o.idPeriodo).ToList())
                    {
                        List<E_META> vGraficaTemas = nDesempeno.ObtieneMetasEvaluados(pIdPeriodo: items.idPeriodo, idEvaluado: items.idEvaluado, FgEvaluar: true, idEmpleado: vIdEmpleado).Select(s => new E_META { NO_META = s.NO_META.ToString(), DS_META = s.DS_META, PR_CUMPLIMIENTO = s.PR_CUMPLIMIENTO_META, COLOR_NIVEL = s.COLOR_NIVEL }).ToList();
                        ColumnSeries vSerie = new ColumnSeries();
                        // vSerie.Name = (items.nbPeriodo);
                        vSerie.TooltipsAppearance.ClientTemplate = items.nbPeriodo;
                   

                        vListaMetasEvalPeriodo = new List<E_METAS_EVALUADO_PERIODO>();

                        foreach (var item in vGraficaTemas.OrderBy(o => o.ID_PERIODO).ToList())
                        {
                            foreach (var Metas in vMetas)
                            {
                                if (item.DS_META == Metas.DS_META)
                                {
                                    bool exists = vListaMetasEvalPeriodo.Exists(element => element.DS_META == Metas.DS_META);
                                    bool value = vListaMetasEvalPeriodo.Exists(element => element.DS_META == Metas.DS_META && element.PR_CUMPLIMIENTO != 0);
                                    if (!exists)
                                    {
                                        vListaMetasEvalPeriodo.Add(new E_METAS_EVALUADO_PERIODO
                                            {
                                                DS_META = Metas.DS_META,
                                                PR_CUMPLIMIENTO = item.PR_CUMPLIMIENTO
                                            });
                                    }
                                    else if (exists && !value)
                                    {
                                        var itemToRemove = vListaMetasEvalPeriodo.Single(r => r.DS_META == Metas.DS_META);
                                        vListaMetasEvalPeriodo.Remove(itemToRemove);
                                        vListaMetasEvalPeriodo.Add(new E_METAS_EVALUADO_PERIODO
                                        {
                                            DS_META = Metas.DS_META,
                                            PR_CUMPLIMIENTO = item.PR_CUMPLIMIENTO
                                        });
                                    }
                                }
                                else
                                {
                                    bool exists = vListaMetasEvalPeriodo.Exists(element => element.DS_META == Metas.DS_META);
                                    bool value = vListaMetasEvalPeriodo.Exists(element => element.DS_META == Metas.DS_META && element.PR_CUMPLIMIENTO != 0);
                                    if (!exists)
                                    {
                                        vListaMetasEvalPeriodo.Add(new E_METAS_EVALUADO_PERIODO
                                           {
                                               DS_META = Metas.DS_META,
                                               PR_CUMPLIMIENTO = 0
                                           });
                                    }
                                }
                            }
                        }


                        foreach (var item in vListaMetasEvalPeriodo.OrderBy(o => o.DS_META))
                        {
                            vSerie.SeriesItems.Add(new CategorySeriesItem(item.PR_CUMPLIMIENTO, System.Drawing.ColorTranslator.FromHtml(GeneraColor(items.numPeriodo))));
                            vSerie.LabelsAppearance.Visible = false;
                            //  vSerie.TooltipsAppearance.ClientTemplate = item.PR_CUMPLIMIENTO.ToString();
                        }

                        rhcCumplimientoPersonal.PlotArea.Series.Add(vSerie);
                    }

                var vEmpleado = nDesempeno.ObtieneEvaluados(pIdPeriodo: null, pIdEvaluado: vIdEvaluado, pIdEvaluador: null).FirstOrDefault();
                    vIdEmpleado = vEmpleado.ID_EMPLEADO;
                    txtClEmpleado.InnerText = vEmpleado.CL_EMPLEADO;
                    txtNbEmpleado.InnerText = vEmpleado.NB_EMPLEADO_COMPLETO;

                    PintarReporte();
                    PintarResultadosGrafica();
            }

            if (SELECCIONPERIODOS == null)
            {
                var vXelements = vListaPeriodos.Select(x => new XElement("PERIODOS",
                                                                 new XAttribute("ID_PERIODO", x.idPeriodo),
                                                                 new XAttribute("ID_EVALUADO", x.idEvaluado)));
                SELECCIONPERIODOS = new XElement("SELECCIONADOS", vXelements);
            }

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

        }

        protected void vGridResultados_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            vGridResultados.DataSource = vResultados;
        }
    }
}