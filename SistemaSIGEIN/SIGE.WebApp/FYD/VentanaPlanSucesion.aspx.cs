using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using Stimulsoft.Base.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.FYD
{
    public partial class VentanaPlanSucesion : System.Web.UI.Page
    {
        #region Propiedades

        public int vIdEmpleado
        {
            get { return (int)ViewState["vs_vps_id_empleado"]; }
            set { ViewState["vs_vps_id_empleado"] = value; }
        }

        public int vIdPuesto
        {
            get { return (int)ViewState["vs_vps_id_puesto"]; }
            set { ViewState["vs_vps_id_puesto"] = value; }
        }

        public string vXmlSucesores
        {
            get { return (string)ViewState["vs_vps_xml_sucesores"]; }
            set { ViewState["vs_vps_xml_sucesores"] = value; }
        }

        public string vXmlEmpleados
        {
            get { return (string)ViewState["vs_vXmlEmpleados"]; }
            set { ViewState["vs_vXmlEmpleados"] = value; }
        }

        private string vXmlSucesoresSeleccion
        {
            get { return (string)ViewState["vs_vps_xml_seleccion_sucesores"]; }
            set { ViewState["vs_vps_xml_seleccion_sucesores"] = value; }
        }

        private List<string> oListaEmpleados
        {
            get { return (List<string>)ViewState["vs_vps_columnas"]; }
            set { ViewState["vs_vps_columnas"] = value; }
        }

        public List<E_PLAN_SUCESION> listaDatosSucesores
        {
            get { return (List<E_PLAN_SUCESION>)ViewState["vs_listaDatosSucesores"]; }
            set { ViewState["vs_listaDatosSucesores"] = value; }
        }

        private List<E_COMPARACION_COMPETENCIA> ComparacionCompetencias
        {
            get { return (List<E_COMPARACION_COMPETENCIA>)ViewState["vs_vps_competencias"]; }
            set { ViewState["vs_vps_competencias"] = value; }
        }

        public int vNoEmpleados
        {
            get { return (int)ViewState["vs_vps_no_empleados"]; }
            set { ViewState["vs_vps_no_empleados"] = value; }
        }

        #endregion

        #region Funciones

        private string obtenerPromedio(string vClEmpleado)
        {
            int i = 0, j = 0;
            decimal? promedio = 0;

            i = ComparacionCompetencias.Where(t => t.CL_EMPLEADO == vClEmpleado && t.ID_COMPETENCIA != null && t.PR_NO_COMPATIBILIDAD != -1).Count();
            j = ComparacionCompetencias.Where(t => t.CL_EMPLEADO == vClEmpleado && t.ID_COMPETENCIA != null).Count();

            //promedio = (Convert.ToDecimal(i) / Convert.ToDecimal(j)) * 100; 

            promedio = ComparacionCompetencias.Where(w => w.CL_EMPLEADO == vClEmpleado && w.ID_COMPETENCIA != null).Average(av => av.PR_NO_COMPATIBILIDAD);

            if (i == j)
                return string.Format("{0:N2}%", promedio);
            else
                return "S/C";
        }

        private void CargarDatos()
        {
            PlanSucesionNegocio neg = new PlanSucesionNegocio();
            ComparacionCompetencias = neg.obtieneComparacionCompetenciasPlanSucesion(vXmlSucesores, vIdPuesto);

            vNoEmpleados = (from a in ComparacionCompetencias select new { a.ID_EMPLEADO }).Distinct().Count();

            pgCompetencias.DataSource = ComparacionCompetencias;
            pgCompetencias.DataBind();
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                oListaEmpleados = new List<string>();
                PlanSucesionNegocio neg = new PlanSucesionNegocio();

                if (Request.Params["idEmpleado"] != null)
                {
                    vIdEmpleado = int.Parse(Request.Params["idEmpleado"]);


                    SPE_OBTIENE_EMPLEADOS_Result emp = new SPE_OBTIENE_EMPLEADOS_Result();
                    XElement xml_empleado = new XElement("SELECCION", new XElement("FILTRO", new XAttribute("CL_TIPO", "EMPLEADO"), new XElement("EMP", new XAttribute("ID_EMPLEADO", vIdEmpleado))));

                    emp = neg.ObtieneEmpleados(xml_empleado).FirstOrDefault();

                    vIdPuesto = emp.M_PUESTO_ID_PUESTO.Value;
                    txtEmpleado.InnerText = emp.M_EMPLEADO_CL_EMPLEADO + ": "+ emp.M_EMPLEADO_NB_EMPLEADO_COMPLETO;
                    txtPuesto.InnerText = emp.M_PUESTO_CL_PUESTO + ": " + emp.M_PUESTO_NB_PUESTO;

                }

                if (Request.Params["sucesores"] != null)
                {
                    listaDatosSucesores = new List<E_PLAN_SUCESION>();
                    listaDatosSucesores = JsonConvert.DeserializeObject<List<E_PLAN_SUCESION>>(Request.Params["sucesores"].ToString());

                    XElement xml_sucesores = new XElement("EMPLEADOS");
                    XElement xml_seleccion = new XElement("SELECCION");
                    XElement xml_filtro = new XElement("FILTRO", new XAttribute("CL_TIPO", "EMPLEADO"));

                    foreach (E_PLAN_SUCESION item in listaDatosSucesores)
                    {
                        vXmlEmpleados = vXmlEmpleados + item.ID_EMPLEADO +",";
                        xml_filtro.Add(new XElement("EMP", new XAttribute("ID_EMPLEADO", item.ID_EMPLEADO)));
                        xml_sucesores.Add(new XElement("EMP", new XAttribute("ID_EMPLEADO", item.ID_EMPLEADO), new XAttribute("ID_PERIODO", item.ID_PERIODO)));
                    }

                    xml_seleccion.Add(xml_filtro);

                    vXmlSucesores = xml_sucesores.ToString();
                    vXmlSucesoresSeleccion = xml_seleccion.ToString();

                }

                CargarDatos();
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

        protected void pgCompetencias_NeedDataSource(object sender, Telerik.Web.UI.PivotGridNeedDataSourceEventArgs e)
        {
           
        }

        protected void pgCompetencias_CellDataBound(object sender, Telerik.Web.UI.PivotGridCellDataBoundEventArgs e)
        {
            int vNoValorCompetencia;

            if (e.Cell is PivotGridColumnHeaderCell)
            {

                E_COMPARACION_COMPETENCIA vEmpleado = ComparacionCompetencias.Where(t => t.CL_EMPLEADO.Equals(e.Cell.DataItem.ToString())).FirstOrDefault();

                if (vEmpleado != null)
                {
                    e.Cell.ToolTip = vEmpleado.NB_EMPLEADO;
                    e.Cell.Text = String.Format("<a href='#' onclick='OpenInventario({1})'>{0}</a>", vEmpleado.CL_EMPLEADO, vEmpleado.ID_EMPLEADO);

                    oListaEmpleados.Add(e.Cell.DataItem.ToString());
                }
            }

            if (e.Cell is PivotGridRowHeaderCell)
            {
                if (e.Cell.Controls.Count > 1)
                {
                    (e.Cell.Controls[0] as Button).Visible = false;

   
                }
            }
            else if (e.Cell is PivotGridDataCell)
            {

                PivotGridDataCell celda = (PivotGridDataCell)e.Cell;

                if (celda.IsGrandTotalCell)
                {
                    celda.Text = "<div style=\"text-align: center;\">" + obtenerPromedio(oListaEmpleados[celda.ColumnIndex]).ToString() + "</div>";
                }
                else
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl divColor = celda.FindControl("divColorComparacion") as System.Web.UI.HtmlControls.HtmlGenericControl;
                    System.Web.UI.HtmlControls.HtmlGenericControl divPromedio = celda.FindControl("divPromedio") as System.Web.UI.HtmlControls.HtmlGenericControl;
                    System.Web.UI.HtmlControls.HtmlGenericControl divNa = celda.FindControl("divNa") as System.Web.UI.HtmlControls.HtmlGenericControl;
                    System.Web.UI.HtmlControls.HtmlGenericControl divNc = celda.FindControl("divNc") as System.Web.UI.HtmlControls.HtmlGenericControl;

                    if (e.Cell.DataItem != null)
                    {
                        if ((decimal)e.Cell.DataItem == Convert.ToDecimal(-1))
                        {
                            //e.Cell.Text = "<div style=\"text-align: center;\">N/C</div>";
                            //e.Cell.CssClass = "PotencialNC";
                            divNa.Style.Add("display", "none");
                            divNc.Style.Add("display", "block");
                            divPromedio.Style.Add("display", "none");
                            divColor.Style.Add("background-color", "gray");
                        }
                        else if (celda.ParentRowIndexes[2].ToString() == "Total de elementos en común:")
                        {
                            divNa.Style.Add("display", "none");
                            divNc.Style.Add("display", "none");
                            divPromedio.Style.Add("display", "block");
                            divPromedio.Style.Add(" font-weight", "bold");
                            divColor.Style.Add("background-color", "white");
                        }
                        else
                        {
                            vNoValorCompetencia = Convert.ToInt32((decimal)e.Cell.DataItem);

                            if (vNoValorCompetencia == 0)
                            {
                                //e.Cell.CssClass = "PotencialPuestoBajo";
                                this.rtmInfoEmpleados.TargetControls.Add(celda.ClientID, oListaEmpleados[celda.ColumnIndex], true);
                                divNa.Style.Add("display", "none");
                                divNc.Style.Add("display", "none");
                                divPromedio.Style.Add("display", "none");
                                divColor.Style.Add("background-color", "red");
                            }
                            else if (vNoValorCompetencia == 1)
                            {
                                //e.Cell.CssClass = "PotencialPuestoAlto";
                                divNa.Style.Add("display", "none");
                                divNc.Style.Add("display", "none");
                                divPromedio.Style.Add("display", "none");
                                divColor.Style.Add("background-color", "green");
                                this.rtmInfoEmpleados.TargetControls.Add(celda.ClientID, oListaEmpleados[celda.ColumnIndex], true);
                            }
                            else if (vNoValorCompetencia < 70)
                            {
                                //e.Cell.CssClass = "PotencialBajo"; 
                                divNa.Style.Add("display", "none");
                                divNc.Style.Add("display", "none");
                                divPromedio.Style.Add("display", "block");
                                divColor.Style.Add("background-color", "red");
                            }
                            else if (vNoValorCompetencia >= 70 & vNoValorCompetencia <= 90)
                            {
                                //e.Cell.CssClass = "PotencialIntermedio"; 
                                divNa.Style.Add("display", "none");
                                divNc.Style.Add("display", "none");
                                divPromedio.Style.Add("display", "block");
                                divColor.Style.Add("background-color", "gold");
                            }
                            else if (vNoValorCompetencia > 90)
                            {
                                //e.Cell.CssClass = "PotencialAlto"; 
                                divNa.Style.Add("display", "none");
                                divNc.Style.Add("display", "none");
                                divPromedio.Style.Add("display", "block");
                                divColor.Style.Add("background-color", "green");
                            }
                        }
                    }
                    else
                    {
                        divNa.Style.Add("display", "none");
                        divNc.Style.Add("display", "block");
                        divPromedio.Style.Add("display", "none");
                        divColor.Style.Add("background-color", "gray");
                    }
                }
            }
        }

        protected void rgSucesores_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            PlanSucesionNegocio neg = new PlanSucesionNegocio();
            rgSucesores.DataSource = neg.ObtieneEmpleados(XElement.Parse(vXmlSucesoresSeleccion));
        }

        protected void RadToolTipManager1_AjaxUpdate(object sender, ToolTipUpdateEventArgs e)
        {
            this.UpdateToolTip(e.Value, e.UpdatePanel);
        }

        private void UpdateToolTip(string elementID, UpdatePanel panel)
        {
            Control ctrl = Page.LoadControl("ToolTipEmpleado.ascx");
            ctrl.ID = "UcDatosEmpleado1";
            panel.ContentTemplateContainer.Controls.Add(ctrl);
            ToolTipEmpleado details = (ToolTipEmpleado)ctrl;

            var datos = ComparacionCompetencias.Where(t => t.CL_TIPO_REGISTRO == "P" & t.CL_EMPLEADO == elementID).ToList();

            details.ListaDatos = datos;
        }

        protected void rgSucesores_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridDataItem)
            //{
            //    GridDataItem item = e.Item as GridDataItem;

            //    item["M_EMPLEADO_NB_EMPLEADO_COMPLETO"].ToolTip = "Nombre";
            //    item["M_PUESTO_NB_PUESTO"].ToolTip = "Puesto";

            //}
        }
    }
}