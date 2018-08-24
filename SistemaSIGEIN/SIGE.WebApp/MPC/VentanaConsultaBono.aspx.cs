using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Entidades.Externas;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.MPC
{
    public partial class VentanaConsultaBono : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private XElement SELECCIONPERIODOS { get; set; }

        private DataTable vBonos
        {
            get { return (DataTable)ViewState["vs_vBonos"]; }
            set { ViewState["vs_vBonos"] = value; }
        }

        public List<E_OBTIENE_EVALUADOS_DESEMPENO> vLstEvaluadosReporte
        {
            get { return (List<E_OBTIENE_EVALUADOS_DESEMPENO>)ViewState["vs_vLstEvaluadosReporte"]; }
            set { ViewState["vs_vLstEvaluadosReporte"] = value; }
        }

        public List<E_OBTIENE_EVALUADOS_DESEMPENO> vLstBonosEvaluados
        {
            get { return (List<E_OBTIENE_EVALUADOS_DESEMPENO>)ViewState["vs_vLstBonosEvaluados"]; }
            set { ViewState["vs_vLstBonosEvaluados"] = value; }
        }

        public List<E_PERIODO_DESEMPENO> oLstPeriodos
        {
            get { return (List<E_PERIODO_DESEMPENO>)ViewState["vs_lista_periodos"]; }
            set { ViewState["vs_lista_periodos"] = value; }
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
            vLstBonosEvaluados = new List<E_OBTIENE_EVALUADOS_DESEMPENO>();
            vLstEvaluadosReporte = new List<E_OBTIENE_EVALUADOS_DESEMPENO>();
            decimal? vTopeBono;
            decimal? vCumplimientoEvaluado;
            decimal? vMnBonoTotal;
            int vPeriodos;

            vLstEvaluadosReporte = oNegocio.ObtenerEvaluadosDesempeno(SELECCIONPERIODOS.ToString());

            DataTable vDtPivot = new DataTable();

            vDtPivot.Columns.Add("CL_EMPLEADO", typeof(string));
            vDtPivot.Columns.Add("NB_EMPLEADO_COMPLETO", typeof(string));
            vDtPivot.Columns.Add("NB_PUESTO", typeof(string));
            vDtPivot.Columns.Add("NB_DEPARTAMENTO", typeof(string));
            vDtPivot.Columns.Add("MN_SUELDO", typeof(string));
            vDtPivot.Columns.Add("MN_TOPE_BONO", typeof(string));

            foreach (var item in oLstPeriodos)
            {
                vDtPivot.Columns.Add(item.CL_TIPO_PERIODO, typeof(string));
            }

            vDtPivot.Columns.Add("PR_CUMPLIMIENTO_EVALUADO", typeof(string));
            vDtPivot.Columns.Add("MN_BONO_TOTAL", typeof(string));


            foreach (var item in vLstEvaluadosReporte)
            {

                vLstBonosEvaluados = oNegocio.ObtenerBonosDesempeno(item.ID_EMPLEADO, SELECCIONPERIODOS.ToString());
                vCumplimientoEvaluado = 0;
                vMnBonoTotal = 0;
                vTopeBono = 0;
                vPeriodos = 0;



                DataRow vDr = vDtPivot.NewRow();
                vDr["CL_EMPLEADO"] = item.CL_EMPLEADO;
                vDr["NB_EMPLEADO_COMPLETO"] = item.NB_EMPLEADO;
                vDr["NB_PUESTO"] = item.NB_PUESTO;
                vDr["NB_DEPARTAMENTO"] = item.NB_DEPARTAMENTO;
                vDr["MN_SUELDO"] = "$" + item.MN_SUELDO.ToString();

                foreach (var vPeriodo in oLstPeriodos)
                {

                    var vResultado = vLstBonosEvaluados.Where(t => t.ID_PERIODO == vPeriodo.ID_PERIODO).FirstOrDefault();
                    if (vResultado != null)
                    {
                        if (vResultado.PR_CUMPLIMIENTO_EVALUADO != null)
                        {
                            if (vResultado.MN_BONO_TOTAL != null)
                                vDr[vPeriodo.CL_TIPO_PERIODO.ToString()] = vResultado.PR_CUMPLIMIENTO_EVALUADO.ToString() + "%  " + "<br>" + "  $" + vResultado.MN_BONO_TOTAL.ToString();
                            else
                                vDr[vPeriodo.CL_TIPO_PERIODO.ToString()] = vResultado.PR_CUMPLIMIENTO_EVALUADO.ToString() + "%";
                        }
                        else if (vResultado.MN_BONO_TOTAL != null)
                        {
                            vDr[vPeriodo.CL_TIPO_PERIODO.ToString()] = "\n" + "$" + vResultado.MN_BONO_TOTAL.ToString();
                        }

                        vCumplimientoEvaluado = vCumplimientoEvaluado + vResultado.PR_CUMPLIMIENTO_EVALUADO;
                        vMnBonoTotal = vMnBonoTotal + vResultado.MN_BONO_TOTAL;
                        vPeriodos = vPeriodos + 1;
                        if (vResultado.MN_BONO != 0)
                        {
                            vTopeBono = vTopeBono + vResultado.MN_BONO;
                        }
                        else if (vResultado.PR_BONO != 0)
                        {
                            decimal vDiasPeriodo = (decimal)((vResultado.FE_TERMINO - vResultado.FE_INICIO).TotalDays);
                            decimal vSueldoDia = ((decimal)item.MN_SUELDO / ((decimal)30.4));
                            vTopeBono = vTopeBono + ((vDiasPeriodo + 1) * vSueldoDia * (vResultado.PR_BONO / 100));
                        }
                    }


                }

                vDr["MN_TOPE_BONO"] = "$" + String.Format("{0:0.00}", vTopeBono);
                if (vCumplimientoEvaluado != null)
                    vDr["PR_CUMPLIMIENTO_EVALUADO"] = String.Format("{0:0.00}", ((vCumplimientoEvaluado * 100) / (vPeriodos * 100))) + "%";
                if (vMnBonoTotal != null)
                    vDr["MN_BONO_TOTAL"] = "$" + vMnBonoTotal.ToString();

                vDtPivot.Rows.Add(vDr);
            }
            return vDtPivot;
        }

        public void generarReporte()
        {
            vBonos = ObtieneEvaluadosReporte();
            foreach (var item in vBonos.Columns)
            {
                GeneraColumna(item.ToString());
            }
        }

        protected void GeneraColumna(string pColumna)
        {
            switch (pColumna)
            {
                case "CL_EMPLEADO":
                    ConfigurarColumna(pColumna, 100, "No. de Empleado", true, false, false, true);
                    break;
                case "NB_EMPLEADO_COMPLETO":
                    ConfigurarColumna(pColumna, 200, "Nombre completo", true, false, false, true);
                    break;
                case "NB_PUESTO":
                    ConfigurarColumna(pColumna, 200, "Puesto", true, false, false, true);
                    break;
                case "NB_DEPARTAMENTO":
                    ConfigurarColumna(pColumna, 200, "Área/Departamento", true, false, false, true);
                    break;
                case "MN_SUELDO":
                    ConfigurarColumna(pColumna, 90, "Sueldo mensual", true, false, false, false);
                    break;
                case "MN_TOPE_BONO":
                    ConfigurarColumna(pColumna, 80, "Bono maximo", true, false, false, false);
                    break;
                case "PR_CUMPLIMIENTO_EVALUADO":
                    ConfigurarColumna(pColumna, 90, "Desempeño promedio", true, false, false, false);
                    break;
                case "MN_BONO_TOTAL":
                    ConfigurarColumna(pColumna, 80, "Total bono", true, false, false, false);
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

            rgBonosComparativos.MasterTableView.Columns.Add(pColumn);
        }

        private void ConfiguraPeriodosColumna(string pColumna, int pWidth, string pEncabezado, bool pVisible, bool pGenerarEncabezado, bool pFiltrarColumna, bool pCentrar)
        {
            GridBoundColumn ColumnPeriodo = new GridBoundColumn();
            ColumnPeriodo.HeaderText = "<div style=\"height: 30px;font-size: 10pt;\">" + pColumna + "</div>";
            ColumnPeriodo.DataField = pColumna;
            ColumnPeriodo.HeaderStyle.Width = pWidth;
            ColumnPeriodo.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            ColumnPeriodo.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
            ColumnPeriodo.HeaderStyle.Font.Bold = true;
            ColumnPeriodo.ColumnGroupName = "Desempeno";


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

            rgBonosComparativos.MasterTableView.Columns.Add(ColumnPeriodo);
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                oLstPeriodos = new List<E_PERIODO_DESEMPENO>();

                if (ContextoBono.oLstPeriodosBonos != null)
                {
                    foreach (E_SELECCION_PERIODOS_DESEMPENO item in ContextoBono.oLstPeriodosBonos)
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

                    SELECCIONPERIODOS = new XElement("SELECCION", vXelements
                  );

                }

                generarReporte();
            }

            if (SELECCIONPERIODOS == null)
            {
                var vXelements = oLstPeriodos.Select(x =>
                                                         new XElement("PERIODOS",
                                                             new XAttribute("ID_PERIODO", x.ID_PERIODO)
                                                             ));

                SELECCIONPERIODOS = new XElement("SELECCION", vXelements
              );
            }

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

        }

        protected void rgBonosComparativos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgBonosComparativos.DataSource = vBonos;
        }

        protected void rgContexto_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgContexto.DataSource = oLstPeriodos;
        }
    }
}