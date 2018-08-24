using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.FormacionDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace SIGE.WebApp.FYD
{
    public partial class VentanaPlanVidaCarrera : System.Web.UI.Page
    {

        #region Variables

        public int vIdEmpleado
        {
            get { return (int)ViewState["vs_vpvc_id_empleado"]; }
            set { ViewState["vs_vpvc_id_empleado"] = value; }
        }

        public int vIdPuesto
        {
            get { return (int)ViewState["vs_vpvc_id_puesto"]; }
            set { ViewState["vs_vpvc_id_puesto"] = value; }
        }

        private int vIdPeriodo
        {
            get { return (int)ViewState["vs_vpvc_id_periodo"]; }
            set { ViewState["vs_vpvc_id_periodo"] = value; }
        }

        public string PuestosComparacion
        {
            get { return (string)ViewState["vs_vpvc_puesto_comparacion"]; }
            set { ViewState["vs_vpvc_puesto_comparacion"] = value; }
        }

        private List<E_PROMEDIO_PLAN_VIDA_CARRERA> vLstPromediosPuestos
        {
            get { return (List<E_PROMEDIO_PLAN_VIDA_CARRERA>)ViewState["vs_vpvc_promedios_puestos"]; }
            set { ViewState["vs_vpvc_promedios_puestos"] = value; }
        }

        private List<E_COMPARACION_COMPETENCIA> vLstComparacionCompetencias
        {
            get { return (List<E_COMPARACION_COMPETENCIA>)ViewState["vs_vpvc_competencias"]; }
            set { ViewState["vs_vpvc_competencias"] = value; }
        }

        private int vNoValorCompetencia { get; set; }

        #endregion

        #region Funciones

        private string ConvertToHTMLTable(XElement source)
        {
            string Table = "<div style=\"padding-top:0px;\"><Table border=\"1\">";
            string aux = "";
            bool alternateColor = false;

            if (source.Elements("EXP").Count() > 1)
            {
                foreach (XElement item in source.Elements())
                {

                    if (alternateColor)
                    {
                        aux = "<tr style=\"padding: 5px; background-color:#E6E6FA\">";
                    }
                    else
                    {
                        aux = "<tr style=\"padding: 5px;\">";
                    }



                    foreach (XAttribute attr in item.Attributes())
                    {
                        if (attr.Name != "FG_ENCABEZADO")
                        {
                            if (item.Attribute("FG_ENCABEZADO").Value == "1")
                            {
                                aux = aux + "<th style=\"padding: 5px;\">" + attr.Value + "</th>";
                            }
                            else
                            {
                                aux = aux + "<td style=\"padding: 5px;\">" + attr.Value + "</td>";
                            }
                        }
                    }

                    alternateColor = !alternateColor;
                    aux = aux + "</tr>";
                    Table = Table + aux;
                }

                Table = Table + "</Table></div>";
            }
            else
            {
                Table = "";
            }

            return Table;
        }  

        private void ConfigurarColumna(GridColumn pColumna, int pWidth, string pEncabezado, bool pVisible, bool pGenerarEncabezado, bool pFiltrarColumna)
        {
            if (pGenerarEncabezado)
            {
                pEncabezado = GeneraEncabezado(pColumna);
            }

            if (pColumna.UniqueName == "DS_COMPETENCIA")
            {
                pColumna.FooterText = "Total:";
                pColumna.FooterStyle.HorizontalAlign = HorizontalAlign.Right;
                pColumna.FooterStyle.Font.Bold = true;
            }

            pColumna.HeaderStyle.Width = Unit.Pixel(pWidth);
            pColumna.HeaderText = pEncabezado;
            pColumna.Visible = pVisible;
            

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

        private string GeneraEncabezado(GridColumn pColumna)
        {
            int vResultado;
            string vEncabezado = "";
            string vEmpleado = pColumna.UniqueName.ToString().Substring(0, pColumna.UniqueName.ToString().IndexOf('E'));

            if (int.TryParse(vEmpleado, out vResultado))
            {
                var vDatosEmpleado = vLstComparacionCompetencias.Where(t => t.ID_PUESTO == vResultado).FirstOrDefault();

                if (vDatosEmpleado != null)
                {
                    //vEncabezado = "<div style=\"text-align:center;\"> " + vDatosEmpleado.CL_PUESTO+ "</div>";
                    vEncabezado = String.Format("<a href='#' onclick='OpenDescriptivo({1})'>{0}</a>", vDatosEmpleado.CL_PUESTO, vDatosEmpleado.ID_PUESTO);
                }
            }

            return vEncabezado;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                vLstComparacionCompetencias = new List<E_COMPARACION_COMPETENCIA>();
                vLstPromediosPuestos = new List<E_PROMEDIO_PLAN_VIDA_CARRERA>();

                Negocio.Administracion.PeriodoNegocio neg = new Negocio.Administracion.PeriodoNegocio();
                PlanVidaCarreraNegocio negp = new PlanVidaCarreraNegocio();
                EmpleadoNegocio nege = new EmpleadoNegocio();

                if (Request.Params["idPeriodo"] != null)
                {
                    SPE_OBTIENE_C_PERIODO_Result per = new SPE_OBTIENE_C_PERIODO_Result();
                    vIdPeriodo = int.Parse(Request.Params["idPeriodo"]);

                    per = neg.Obtener_C_PERIODO(ID_PERIODO: vIdPeriodo).FirstOrDefault();

                    txtPeriodo.InnerText = per.NB_PERIODO + per.DS_PERIODO;
                    txtTipoEvaluacion.InnerHtml = per.TIPO_EVALUACION;
                }

                if (Request.Params["idEmpleado"] != null)
                {
                    SPE_OBTIENE_M_EMPLEADO_Result emp = new SPE_OBTIENE_M_EMPLEADO_Result();

                    vIdEmpleado = int.Parse(Request.Params["idEmpleado"]);

                    emp = nege.ObtenerEmpleado(ID_EMPLEADO: vIdEmpleado).FirstOrDefault();

                    vIdPuesto = emp.ID_PUESTO;
                    txtClaveEmpleado.InnerText = emp.CL_EMPLEADO;
                    txtClavePuesto.InnerText = emp.CL_PUESTO;
                    txtNombreEmpleado.InnerText = emp.NB_EMPLEADO_COMPLETO;
                    txtNombrePuesto.InnerText = emp.NB_PUESTO;
                }

                if (Request.Params["Puestos"] != null)
                {
                    XElement prueba = new XElement("PUESTOS");

                    string[] aux = Request.Params["Puestos"].ToString().Split(',');

                    foreach (string item in aux)
                    {
                        prueba.Add(new XElement("PUESTO", new XAttribute("ID", item)));
                    }

                    PuestosComparacion = prueba.ToString();

                    List<SPE_OBTIENE_M_PUESTO_Result> listaPuestos = negp.ObtienePuestos(PuestosComparacion);

                    lstPuestos.DataSource = listaPuestos;
                    lstPuestos.DataValueField = "ID_PUESTO";
                    lstPuestos.DataTextField = "NB_PUESTO";
                    lstPuestos.DataBind();

                }
            }
        }

        protected void grdCompetencias_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            List<KeyValuePair<string, string>> lista = new List<KeyValuePair<string, string>>();

            lista.Add(new KeyValuePair<string, string>("Competencias Genéricas", "Esta dimensión evalúa comportamientos elementales asociados a desempeños comunes a diversas ocupaciones y que están asociados a conocimientos y habilidades de índole formativa"));
            lista.Add(new KeyValuePair<string, string>("Competencias Específicas", "Esta dimensión evalúa los comportamientos asociados a conocimientos y habilidades vinculados con la función productiva que la persona desarrolla en su puesto"));
            lista.Add(new KeyValuePair<string, string>("Competencias Institucionales", "Esta dimensión evalúa los comportamientos y actitudes que reflejan y van estrechamente relacionados con los valores empresariales de la organización"));

            grdCompetencias.DataSource = lista;
        }

        protected void grdpuestos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            PlanVidaCarreraNegocio neg = new PlanVidaCarreraNegocio();
            grdpuestos.DataSource = neg.obtenerComparacionPuestos(PuestosComparacion, vIdEmpleado);
        }

        protected void grdpuestos_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
        {
            if (e.Column.UniqueName == "Item")
            {
                e.Column.HeaderStyle.Width = Unit.Pixel(20);
            }
            else
            {
                e.Column.HeaderStyle.Width = Unit.Pixel(150);
            }
        }

        protected void grdpuestos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = e.Item as GridDataItem;

                if (item.ItemIndex == 9)
                {
                    for (int i = 3; i <= item.Cells.Count - 1; i++)
                    {
                        item.Cells[i].Text = ConvertToHTMLTable(XElement.Parse(item.Cells[i].Text));
                    }
                }
            }
        }

        protected void rgCompetencias_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            List<E_COMPARACION_COMPETENCIA> vLstTemporal = new List<E_COMPARACION_COMPETENCIA>();
            List<E_PROMEDIO_PLAN_VIDA_CARRERA> vLstTemporal2 = new List<E_PROMEDIO_PLAN_VIDA_CARRERA>();

            PlanVidaCarreraNegocio neg = new PlanVidaCarreraNegocio();
            rgCompetencias.DataSource = neg.obtieneComparacionCompetenciasPlanVidaCarrera(vIdEmpleado, vIdPeriodo, PuestosComparacion, ref vLstTemporal, ref vLstTemporal2);
            vLstComparacionCompetencias = vLstTemporal;
            vLstPromediosPuestos = vLstTemporal2;
        }

        protected void rgCompetencias_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
        {
            switch (e.Column.UniqueName)
            {
                case "ExpandColumn":
                    break;
                case "ID_COMPETENCIA":
                    ConfigurarColumna(e.Column, 10, "No Competencia", false, false, false);
                    break;
                case "CL_COLOR":
                    ConfigurarColumna(e.Column, 20, "", true, false, false);
                    break;
                case "NB_COMPETENCIA":
                    ConfigurarColumna(e.Column, 150, "Competencia", true, false, true);
                    break;
                case "DS_COMPETENCIA":
                    ConfigurarColumna(e.Column, 300, "Descripción", true, false, true);
                    break;
                default:
                    ConfigurarColumna(e.Column, 50, "", true, true, false);
                    break;
            }
        }

        protected void rgCompetencias_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridFooterItem)
            {
                GridFooterItem vFooterItem = e.Item as GridFooterItem;

                

                foreach (var item in vLstPromediosPuestos)
                {
                    item.CalcularPromedio();
                    vFooterItem[item.NB_PUESTO].Text = string.Format("{0:N2}%", item.PR_PUESTO);
                }
            }
        }
    }
}