using SIGE.Entidades;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Entidades.Externas;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.EO
{
    public partial class VentanaComparativaGlobal : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private XElement SELECCIONPERIODOS { get; set; }

        private DataTable vCumplimientoGlobal
        {
            get { return (DataTable)ViewState["vs_vBonos"]; }
            set { ViewState["vs_vBonos"] = value; }
        }

        public List<E_PERIODO_DESEMPENO> oLstPeriodos
        {
            get { return (List<E_PERIODO_DESEMPENO>)ViewState["vs_lista_periodos"]; }
            set { ViewState["vs_lista_periodos"] = value; }
        }

        public List<E_OBTIENE_CUMPLIMIENTO_GLOBAL> vListaCumplimientoGlobal
        {
            get { return (List<E_OBTIENE_CUMPLIMIENTO_GLOBAL>)ViewState["vs_vListaCumplimientoGlobal"]; }
            set { ViewState["vs_vListaCumplimientoGlobal"] = value; }
        }

        public List<E_OBTIENE_CUMPLIMIENTO_GLOBAL> vListaEvaluados
        {
            get { return (List<E_OBTIENE_CUMPLIMIENTO_GLOBAL>)ViewState["vs_vListaEvaluados"]; }
            set { ViewState["vs_vListaEvaluados"] = value; }
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
                if (pNota.DS_NOTA != null)
                {
                    return pNota.DS_NOTA.ToString();
                }
                else return "";
            }
            else
            {
                return "";
            }
        }

        public string GeneraColor(int pNumPeriodo)
        {
             string vColor = "";
             switch (pNumPeriodo)
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

        public Boolean ValidarRamaXml(XElement parentEl, string elementsName)
        {
            var foundEl = parentEl.Element(elementsName);

            if (foundEl != null)
            {
                return true;
            }
            return false;
        }

        public DataTable ObtieneEvaluadosReporte()
        {
            PeriodoDesempenoNegocio oNegocio = new PeriodoDesempenoNegocio();
            vListaCumplimientoGlobal = new List<E_OBTIENE_CUMPLIMIENTO_GLOBAL>();
            vListaEvaluados = new List<E_OBTIENE_CUMPLIMIENTO_GLOBAL>();
            string vNbPuesto;
            DataTable vDtPivot = new DataTable();

            vDtPivot.Columns.Add("NB_PUESTO", typeof(string));
            vDtPivot.Columns.Add("NB_EMPLEADO", typeof(string));

            foreach (var item in oLstPeriodos)
            {
                vDtPivot.Columns.Add(item.CL_TIPO_PERIODO, typeof(string));

                vListaCumplimientoGlobal = oNegocio.ObtieneCumplimientoGlobal(item.ID_PERIODO);

                foreach (E_OBTIENE_CUMPLIMIENTO_GLOBAL items in vListaCumplimientoGlobal)
                {
                    bool exists = vListaEvaluados.Exists(element => element.ID_EMPLEADO == items.ID_EMPLEADO);
                    if (!exists)
                    {
                        E_OBTIENE_CUMPLIMIENTO_GLOBAL f = new E_OBTIENE_CUMPLIMIENTO_GLOBAL
                        {
                            ID_PERIODO = items.ID_PERIODO,
                            NB_PUESTO_ACTUAL = items.NB_PUESTO_ACTUAL,
                            NB_EVALUADO = items.NB_EVALUADO,
                            ID_EVALUADO = items.ID_EVALUADO,
                            ID_EMPLEADO = items.ID_EMPLEADO,
                            CL_EMPLEADO = items.CL_EMPLEADO
                        };
                        vListaEvaluados.Add(f);
                    }
                }
            }

            foreach (var item in vListaEvaluados)
            {
                vNbPuesto = "";
                DataRow vDr = vDtPivot.NewRow();
                vDr["NB_EMPLEADO"] = "<p title='Clave: " + item.CL_EMPLEADO + "'><a href='javascript:OpenIndividualComparativo("+item.ID_EVALUADO+","+item.ID_EMPLEADO+")'>" + item.NB_EVALUADO + "</a></p>";
                int i = 1;
                foreach (var vPeriodo in oLstPeriodos.OrderByDescending(v => v.ID_PERIODO))
                {
                    vListaCumplimientoGlobal = oNegocio.ObtieneCumplimientoGlobal(vPeriodo.ID_PERIODO);
                    
                    var vResultado = vListaCumplimientoGlobal.Where(t => t.ID_PERIODO == vPeriodo.ID_PERIODO && t.ID_EMPLEADO == item.ID_EMPLEADO).FirstOrDefault();
                    if (vResultado != null)
                    {
                        vNbPuesto = "<p title='Clave: "+vResultado.CL_PUESTO_ACTUAL+"'>"+vResultado.NB_PUESTO_ACTUAL+"</p>";
                        //vDr[vPeriodo.CL_TIPO_PERIODO] = String.Format(vDivsCeldasPo, "<p title=\"" + vResultado.C_GENERAL.ToString() + "\">" + vResultado.C_GENERAL.ToString() + "%</p>", GeneraColor(vResultado.PR_CUMPLIMIENTO_EVALUADO), "<p title=\"" + vResultado.NB_PUESTO_PERIODO + "\">" + vResultado.CL_PUESTO_PERIODO) + "</p>";
                        vDr[vPeriodo.CL_TIPO_PERIODO] = "<p title='" + vResultado.NB_PUESTO_PERIODO + "'>" + vResultado.C_GENERAL + "%&nbsp;&nbsp;<span style=\"border: 1px solid gray; border-radius: 5px; background:" + GeneraColor(i) + ";\">&nbsp;&nbsp;&nbsp;</span>&nbsp" + vResultado.CL_PUESTO_PERIODO + "</p>";

                    }
                    i++;
                }
                vDr["NB_PUESTO"] = vNbPuesto;

                vDtPivot.Rows.Add(vDr);
            }
            return vDtPivot;
        }

        protected void GeneraColumna(string pColumna)
        {
            switch (pColumna)
            {
                case "NB_EMPLEADO":
                    ConfigurarColumna(pColumna, 150, "Nombre completo", true, false, false, true);
                    break;
                case "NB_PUESTO":
                    ConfigurarColumna(pColumna, 150, "Puesto actual", true, false, false, true);
                    break;
                default:
                    ConfiguraPeriodosColumna(pColumna, 100, "", true, false, false, false);
                    break;
            }
        }

        private void ConfigurarColumna(string pColumna, int pWidth, string pEncabezado, bool pVisible, bool pGenerarEncabezado, bool pFiltrarColumna, bool pHorizontalLeft)
        {
            GridBoundColumn pColumn = new GridBoundColumn();
            pColumn.DataField = pColumna;
            pColumn.UniqueName = pColumna;
            pColumn.HeaderStyle.Width = Unit.Pixel(pWidth);
            pColumn.HeaderStyle.Font.Bold = true;

            pColumn.HeaderText = pEncabezado;
            pColumn.Visible = pVisible;

            if (pHorizontalLeft)
            {
                pColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                pColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
            }
            else
            {
                pColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                pColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
            }

            if (pFiltrarColumna & pVisible)
            {
                if (pWidth <= 60)
                {
                    (pColumn as GridBoundColumn).FilterControlWidth = Unit.Pixel(pWidth);
                }
                else
                {
                    (pColumn as GridBoundColumn).FilterControlWidth = Unit.Pixel(pWidth - 60);
                }
            }
            else
            {
                (pColumn as GridBoundColumn).AllowFiltering = false;
            }

            rgGlobalComparativos.MasterTableView.Columns.Add(pColumn);
        }

        private void ConfiguraPeriodosColumna(string pColumna, int pWidth, string pEncabezado, bool pVisible, bool pGenerarEncabezado, bool pFiltrarColumna, bool pCentrar)
        {
            GridBoundColumn ColumnPeriodo = new GridBoundColumn();
           
            ColumnPeriodo.DataField = pColumna;
            ColumnPeriodo.HeaderStyle.Width = pWidth;
            ColumnPeriodo.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            ColumnPeriodo.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            ColumnPeriodo.HeaderStyle.VerticalAlign = VerticalAlign.Middle;
            ColumnPeriodo.HeaderStyle.Font.Bold = true;
            ColumnPeriodo.ColumnGroupName = "Periodos";
            ColumnPeriodo.FooterStyle.Font.Bold = true;
            ColumnPeriodo.FooterStyle.HorizontalAlign = HorizontalAlign.Right;


            PeriodoDesempenoNegocio nDesempeno = new PeriodoDesempenoNegocio();
            List<SPE_OBTIENE_EO_CUMPLIMIENTO_GLOBAL_GRAFICA_Result> vGraficaTotal = nDesempeno.ObtenerCumplimientoGlobalGrafica(SELECCIONPERIODOS.ToString());
            var vPeriodoResultado = vGraficaTotal.Where(s => s.CL_PERIODO == pColumna).FirstOrDefault();
            ColumnPeriodo.FooterText = "Total: "+vPeriodoResultado.CUMPLIDO.ToString()+"%";
            ColumnPeriodo.HeaderText = "<div style=\"height: 30px;font-size: 10pt;\"><a href='javascript:OpenGlobal(" + vPeriodoResultado.ID_PERIODO + ")'>" + pColumna + "</a>&nbsp;&nbsp;<span style=\"border: 1px solid gray; border-radius: 5px; background:" + GeneraColor((int)vPeriodoResultado.NUM_PERIODO) + ";\">&nbsp;&nbsp;</span></div>";

            if (pFiltrarColumna & pVisible)
            {
                if (pWidth <= 60)
                {
                    (ColumnPeriodo as GridBoundColumn).FilterControlWidth = Unit.Pixel(pWidth);
                }
                else
                {
                    (ColumnPeriodo as GridBoundColumn).FilterControlWidth = Unit.Pixel(pWidth - 60);
                }
            }
            else
            {
                (ColumnPeriodo as GridBoundColumn).AllowFiltering = false;
            }

            rgGlobalComparativos.MasterTableView.Columns.Add(ColumnPeriodo);
        }

        public void GenerarReporte()
        {
            vCumplimientoGlobal = ObtieneEvaluadosReporte();
            foreach (var item in vCumplimientoGlobal.Columns)
            {
                GeneraColumna(item.ToString());
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                oLstPeriodos = new List<E_PERIODO_DESEMPENO>();

                if (ContextoPeriodos.oLstPeriodos != null)
                {
                    foreach (E_SELECCION_PERIODOS_DESEMPENO item in ContextoPeriodos.oLstPeriodos)
                    {

                        oLstPeriodos.Add(new E_PERIODO_DESEMPENO
                        {
                            ID_PERIODO = item.idPeriodo,
                            CL_TIPO_PERIODO = item.clPeriodo,
                            DS_PERIODO = item.dsPeriodo,
                            XML_DS_NOTAS = validarDsNotas(item.dsNotas),
                            FE_INICIO_PERIODO = DateTime.Parse(item.feInicio),
                            FE_TERMINO_PERIODO = DateTime.Parse(item.feTermino)

                        });
                    }

                    var vXelements = oLstPeriodos.Select(x =>
                                                         new XElement("PERIODOS",
                                                             new XAttribute("ID_PERIODO", x.ID_PERIODO)
                                                             ));

                    SELECCIONPERIODOS = new XElement("SELECCIONADOS", vXelements
                  );

                }

                GenerarReporte();

                //GRÁFICA
                PeriodoDesempenoNegocio nDesempeno = new PeriodoDesempenoNegocio();
                List<SPE_OBTIENE_EO_CUMPLIMIENTO_GLOBAL_GRAFICA_Result> vGraficaTotal = nDesempeno.ObtenerCumplimientoGlobalGrafica(SELECCIONPERIODOS.ToString());
                rhcGraficaGlobal.PlotArea.Series.Clear();
                ColumnSeries vSerie = new ColumnSeries();
                Color vColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2");

                foreach (var item in vGraficaTotal.OrderByDescending(o => o.ID_PERIODO))
                {
                    if (item.CUMPLIDO > 0 && item.CUMPLIDO < 60)
                        vColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
                    if (item.CUMPLIDO > 59 && item.CUMPLIDO < 76)
                        vColor = System.Drawing.ColorTranslator.FromHtml("#FFFF00");
                    //if (item.CUMPLIDO > 74 && item.CUMPLIDO < 91)
                    //    vColor = System.Drawing.ColorTranslator.FromHtml("#0070C0"); ;
                    if (item.CUMPLIDO > 75 && item.CUMPLIDO < 101)
                        vColor = System.Drawing.ColorTranslator.FromHtml("#00B050");
                    vSerie.SeriesItems.Add(new CategorySeriesItem(item.CUMPLIDO, vColor));
                    vSerie.LabelsAppearance.DataFormatString = "{0:N2}%";
                    rhcGraficaGlobal.PlotArea.XAxis.Items.Add(item.CL_PERIODO);
                    rhcGraficaGlobal.PlotArea.XAxis.LabelsAppearance.DataFormatString = item.CL_PERIODO;
                    rhcGraficaGlobal.PlotArea.YAxis.LabelsAppearance.DataFormatString = "{0:N2}%";
                    
                }

                rhcGraficaGlobal.PlotArea.Series.Add(vSerie);
            }

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
        }

        protected void rgGlobalComparativos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgGlobalComparativos.DataSource = vCumplimientoGlobal;
        }

        protected void rgContexto_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgContexto.DataSource = oLstPeriodos;
        }
    }
}