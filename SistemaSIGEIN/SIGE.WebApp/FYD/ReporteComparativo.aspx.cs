using SIGE.Entidades;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.FYD
{
    public partial class ReporteComparativo : System.Web.UI.Page
    {

        #region Variables

        public int vIdPeriodo
        {
            get { return (int)ViewState["vs_rc_id_periodo"]; }
            set { ViewState["vs_rc_id_periodo"] = value; }
        }

        public int vIdEmpleado
        {
            get { return (int)ViewState["vs_vIdEmpleado"]; }
            set { ViewState["vs_vIdEmpleado"] = value; }
        }

        private int? vIdRol;

        public bool vFgFoto
        {
            get { return (bool)ViewState["vs_rc_fg_foto"]; }
            set { ViewState["vs_rc_fg_foto"] = value; }
        }

        public Guid vIdReporteComparativo
        {
            get { return (Guid)ViewState["vs_rc_id_reporte_comparativo"]; }
            set { ViewState["vs_rc_id_reporte_comparativo"] = value; }
        }

        private string vXmlPeriodos
        {
            get { return (string)ViewState["vs_rc_xml_periodos"]; }
            set { ViewState["vs_rc_xml_periodos"] = value; }
        }

        #endregion

        #region Metodos

        //private HtmlGenericControl GeneraHtml(int? pIdEmpleado)
        //{

        //    HtmlGenericControl vCtrlTablaContenedora = new HtmlGenericControl("table");
        //    if (pIdEmpleado != null && pIdEmpleado > 0)
        //    {
        //        string vDivsCeldasPo = "<table class=\"tablaColor\"> " +
        //         "<tr><td class=\"puesto\"> {0}</td> </tr>" +
        //         "<tr> " +
        //         "<td class=\"porcentaje\"> " +
        //         "<div class=\"divPorcentaje\">{1}</div> " +
        //         "</td> " +
        //         "<td class=\"color\"> " +
        //         "<div class=\"{2}\">&nbsp;</div> " +
        //         "</td> </tr>" +
        //         "</table>";

        //        List<SPE_OBTIENE_FYD_REPORTE_GLOBAL_Result> vLstPrEvaluados = new List<SPE_OBTIENE_FYD_REPORTE_GLOBAL_Result>();
        //        ConsultaGeneralNegocio negGen = new ConsultaGeneralNegocio();

        //        SPE_OBTIENE_FYD_PERIODO_EVALUACION_Result oPeriodo = negGen.ObtenerPeriodoEvaluacion(vIdPeriodo);
        //        vLstPrEvaluados = negGen.ObtenerDatosReporteGlobal(vIdPeriodo, null, false).ToList().Where(w => w.ID_EMPLEADO == pIdEmpleado).ToList();
        //        HtmlGenericControl vCtrlRowContenedor = new HtmlGenericControl("tr");
        //        HtmlGenericControl vCtrlColumnaContenedora = new HtmlGenericControl("td");


        //        if (vLstPrEvaluados.Count > 0)
        //        {

        //            HtmlGenericControl vCtrlTablaPeriodoOriginal = new HtmlGenericControl("table");
        //            HtmlGenericControl vCtrlRow = new HtmlGenericControl("tr");

        //            HtmlGenericControl vCtrlEncabezado = new HtmlGenericControl("th");
        //            vCtrlEncabezado.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 11pt; text-align:center; background-color:#F5F5F5; width:100px; border-radius:2px");
        //            vCtrlEncabezado.InnerText = oPeriodo.CL_PERIODO;
        //            vCtrlEncabezado.Attributes.Add("colspan", vLstPrEvaluados.Count.ToString());
        //            vCtrlRow.Controls.Add(vCtrlEncabezado);

        //            vCtrlTablaPeriodoOriginal.Controls.Add(vCtrlRow);

        //            HtmlGenericControl vCtrlRowDatos = new HtmlGenericControl("tr");
        //            foreach (var itemResult in vLstPrEvaluados)
        //            {
        //                HtmlGenericControl vCtrlColumnaResultado = new HtmlGenericControl("td");
        //                vCtrlColumnaResultado.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 11pt;  width:100px; border-radius:2px");
        //                HtmlGenericControl vCtrlDiv = new HtmlGenericControl("div");
        //                vCtrlDiv.Attributes.Add("style", "padding: 10px");
        //                vCtrlDiv.Attributes.Add("title", itemResult.NB_PUESTO);
        //                vCtrlDiv.InnerHtml = String.Format(vDivsCeldasPo, String.Format("<a href=\"javascript:OpenDescriptivo({0})\">{1}</a>", itemResult.ID_PUESTO_PERIODO, itemResult.CL_PUESTO), itemResult.PR_CUMPLIMIENTO + "%", GenerarColor(itemResult.CL_COLOR_CUMPLIMIENTO));
        //                vCtrlColumnaResultado.Controls.Add(vCtrlDiv);
        //                vCtrlRowDatos.Controls.Add(vCtrlColumnaResultado);
        //            }

        //            vCtrlTablaPeriodoOriginal.Controls.Add(vCtrlRowDatos);

        //            vCtrlColumnaContenedora.Controls.Add(vCtrlTablaPeriodoOriginal);
        //            vCtrlRowContenedor.Controls.Add(vCtrlColumnaContenedora);

        //        }
        //            List<int> lista = ContextoReportes.oReporteComparativo.Where(t => t.vIdReporteComparativo == vIdReporteComparativo).FirstOrDefault().vListaPeriodos;

        //            foreach (int item in lista)
        //            {
        //                HtmlGenericControl vCtrlColumnaContenedoraCompara = new HtmlGenericControl("td");
        //                HtmlGenericControl vCtrlTablaCompara = new HtmlGenericControl("table");
        //                SPE_OBTIENE_FYD_PERIODO_EVALUACION_Result oPeriodoCompara = negGen.ObtenerPeriodoEvaluacion(item);


        //                vLstPrEvaluados = negGen.ObtenerDatosReporteGlobal(item, null, false).ToList().Where(w => w.ID_EMPLEADO == pIdEmpleado).ToList();
        //                if (vLstPrEvaluados.Count > 0)
        //                {


        //                    HtmlGenericControl vCtrlRowCompara = new HtmlGenericControl("tr");

        //                    HtmlGenericControl vCtrlEncabezadoCompara = new HtmlGenericControl("th");
        //                    vCtrlEncabezadoCompara.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 11pt; text-align:center; background-color:#F5F5F5; width:100px; border-radius:2px");
        //                    vCtrlEncabezadoCompara.InnerText = oPeriodoCompara.CL_PERIODO;
        //                    vCtrlEncabezadoCompara.Attributes.Add("colspan", vLstPrEvaluados.Count.ToString());
        //                    vCtrlRowCompara.Controls.Add(vCtrlEncabezadoCompara);

        //                    vCtrlTablaCompara.Controls.Add(vCtrlRowCompara);

        //                    HtmlGenericControl vCtrlRowDatosCompara = new HtmlGenericControl("tr");
        //                    foreach (var itemResult in vLstPrEvaluados)
        //                    {
        //                        HtmlGenericControl vCtrlColumnaResultadoC = new HtmlGenericControl("td");
        //                        vCtrlColumnaResultadoC.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 11pt;  width:100px; border-radius:2px");
        //                        HtmlGenericControl vCtrlDiv = new HtmlGenericControl("div");
        //                        vCtrlDiv.Attributes.Add("style", "padding: 10px");
        //                        vCtrlDiv.Attributes.Add("title", itemResult.NB_PUESTO);
        //                        vCtrlDiv.InnerHtml = String.Format(vDivsCeldasPo, String.Format("<a href=\"javascript:OpenDescriptivo({0})\">{1}</a>", itemResult.ID_PUESTO_PERIODO, itemResult.CL_PUESTO), itemResult.PR_CUMPLIMIENTO + "%", GenerarColor(itemResult.CL_COLOR_CUMPLIMIENTO));
        //                        vCtrlColumnaResultadoC.Controls.Add(vCtrlDiv);
        //                        vCtrlRowDatosCompara.Controls.Add(vCtrlColumnaResultadoC);
        //                    }

        //                    vCtrlTablaCompara.Controls.Add(vCtrlRowDatosCompara);

        //                    vCtrlColumnaContenedoraCompara.Controls.Add(vCtrlTablaCompara);
        //                    vCtrlRowContenedor.Controls.Add(vCtrlColumnaContenedoraCompara);
        //                }


        //            }

        //            vCtrlTablaContenedora.Controls.Add(vCtrlRowContenedor);

        //            return vCtrlTablaContenedora;

        //    }

        //    return vCtrlTablaContenedora;
        //}

        public string GenerarColor(string pColorCumplimiento)
        {
            string vClaseDivs = "";
            switch (pColorCumplimiento)
            {
                case "Green":
                    vClaseDivs = "divVerde";
                    break;
                case "Gold":
                    vClaseDivs = "divAmarillo";
                    break;
                case "Red":
                    vClaseDivs = "divRojo";
                    break;
            }

            return vClaseDivs;
        }

        public void GeneraContexto(List<int> pListaPeriodos)
        {
            string vTiposEvaluacion = "";
            HtmlGenericControl vCtrlTabla = new HtmlGenericControl("table");
            vCtrlTabla.Attributes.Add("class", "ctrlTableForm ctrlTableContext");

            HtmlGenericControl vCtrlColumn = new HtmlGenericControl("tr");


            HtmlGenericControl vCtrlTh = new HtmlGenericControl("th");

            vCtrlTh.InnerText = String.Format("{0}", "Periodo");
            vCtrlColumn.Controls.Add(vCtrlTh);

            HtmlGenericControl vCtrlTh2 = new HtmlGenericControl("th");

            vCtrlTh2.InnerText = String.Format("{0}", "Descripción");
            vCtrlColumn.Controls.Add(vCtrlTh2);

            HtmlGenericControl vCtrlTh3 = new HtmlGenericControl("th");

            vCtrlTh3.InnerText = String.Format("{0}", "Tipo de evaluación");
            vCtrlColumn.Controls.Add(vCtrlTh3);

            vCtrlTabla.Controls.Add(vCtrlColumn);

            ConsultaGeneralNegocio neg = new ConsultaGeneralNegocio();
            ConsultaIndividualNegocio nConsulta = new ConsultaIndividualNegocio();

            bool exists = pListaPeriodos.Exists(element => element == vIdPeriodo);
            if (!exists)
            {
                var oPeriodoOriginal = neg.ObtenerPeriodoEvaluacion(vIdPeriodo);

                HtmlGenericControl vCtrlColumnO = new HtmlGenericControl("tr");
                HtmlGenericControl vCtrlColumnaClPeriodoO = new HtmlGenericControl("td");

                vCtrlColumnaClPeriodoO.InnerText = String.Format("{0}", oPeriodoOriginal.CL_PERIODO);
                vCtrlColumnO.Controls.Add(vCtrlColumnaClPeriodoO);

                HtmlGenericControl vCtrlColumnaNbPeriodoO = new HtmlGenericControl("td");

                vCtrlColumnaNbPeriodoO.InnerHtml = String.Format("{0}", oPeriodoOriginal.DS_PERIODO);
                vCtrlColumnO.Controls.Add(vCtrlColumnaNbPeriodoO);

                if (oPeriodoOriginal.FG_AUTOEVALUACION)
                {
                    vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Autoevaluación" : String.Join(", ", vTiposEvaluacion, "Autoevaluacion");
                }

                if (oPeriodoOriginal.FG_SUPERVISOR)
                {
                    vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Superior" : String.Join(", ", vTiposEvaluacion, "Superior");
                }

                if (oPeriodoOriginal.FG_SUBORDINADOS)
                {
                    vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Subordinado" : String.Join(", ", vTiposEvaluacion, "Subordinado");
                }

                if (oPeriodoOriginal.FG_INTERRELACIONADOS)
                {
                    vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Interrelacionado" : String.Join(", ", vTiposEvaluacion, "Interrelacionado");
                }

                if (oPeriodoOriginal.FG_OTROS_EVALUADORES)
                {
                    vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Otros" : String.Join(", ", vTiposEvaluacion, "Otros");
                }

                HtmlGenericControl vCtrlColumnaTipoPeriodoO = new HtmlGenericControl("td");
                vCtrlColumnaTipoPeriodoO.InnerText = String.Format("{0}", vTiposEvaluacion);
                vCtrlColumnO.Controls.Add(vCtrlColumnaTipoPeriodoO);

                vCtrlTabla.Controls.Add(vCtrlColumnO);
            }

            foreach (int item in pListaPeriodos)
            {
                HtmlGenericControl vCtrlRow = new HtmlGenericControl("tr");
                var oPeriodo = neg.ObtenerPeriodoEvaluacion(item);
                vTiposEvaluacion = "";
                if (oPeriodo != null)
                {

                    HtmlGenericControl vCtrlColumnaClPeriodo = new HtmlGenericControl("td");
                    vCtrlColumnaClPeriodo.InnerText = String.Format("{0}", oPeriodo.CL_PERIODO);
                    vCtrlRow.Controls.Add(vCtrlColumnaClPeriodo);

                    HtmlGenericControl vCtrlColumnaNbPeriodo = new HtmlGenericControl("td");
                    vCtrlColumnaNbPeriodo.InnerHtml = String.Format("{0}", oPeriodo.DS_PERIODO);
                    vCtrlRow.Controls.Add(vCtrlColumnaNbPeriodo);

                    if (oPeriodo.FG_AUTOEVALUACION)
                    {
                        vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Autoevaluación" : String.Join(", ", vTiposEvaluacion, "Autoevaluacion");
                    }

                    if (oPeriodo.FG_SUPERVISOR)
                    {
                        vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Superior" : String.Join(", ", vTiposEvaluacion, "Superior");
                    }

                    if (oPeriodo.FG_SUBORDINADOS)
                    {
                        vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Subordinado" : String.Join(", ", vTiposEvaluacion, "Subordinado");
                    }

                    if (oPeriodo.FG_INTERRELACIONADOS)
                    {
                        vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Interrelacionado" : String.Join(", ", vTiposEvaluacion, "Interrelacionado");
                    }

                    if (oPeriodo.FG_OTROS_EVALUADORES)
                    {
                        vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Otros" : String.Join(", ", vTiposEvaluacion, "Otros");
                    }

                    HtmlGenericControl vCtrlColumnaTipoPeriodo = new HtmlGenericControl("td");
                    vCtrlColumnaTipoPeriodo.InnerText = String.Format("{0}", vTiposEvaluacion);
                    vCtrlRow.Controls.Add(vCtrlColumnaTipoPeriodo);


                    vCtrlTabla.Controls.Add(vCtrlRow);
                    dvContexto.Controls.Add(vCtrlTabla);

                    if (oPeriodo.CL_ESTADO_PERIODO == "ABIERTO")
                    {
                        lblAdvertencia.Visible = true;
                    }
                }
            }
        }

        private void CargarPeriodo()
        {
            //ConsultaGeneralNegocio neg = new ConsultaGeneralNegocio();
            //SPE_OBTIENE_FYD_PERIODO_EVALUACION_Result oPeriodo = neg.ObtenerPeriodoEvaluacion(vIdPeriodo);
            //string vTiposEvaluacion = "";

            //if (oPeriodo != null)
            //{
            //  txtNombrePeriodo.InnerText = oPeriodo.DS_PERIODO;
            //   txtClavePeriodo.InnerText = oPeriodo.CL_PERIODO;

            //if (oPeriodo.FG_AUTOEVALUACION)
            //{
            //    vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Autoevaluación" : String.Join(", ", vTiposEvaluacion, "Autoevaluacion");
            //}

            //if (oPeriodo.FG_SUPERVISOR)
            //{
            //    vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Superior" : String.Join(", ", vTiposEvaluacion, "Superior");
            //}

            //if (oPeriodo.FG_SUBORDINADOS)
            //{
            //    vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Subordinado" : String.Join(", ", vTiposEvaluacion, "Subordinado");
            //}

            //if (oPeriodo.FG_INTERRELACIONADOS)
            //{
            //    vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Interrelacionado" : String.Join(", ", vTiposEvaluacion, "Interrelacionado");
            //}

            //if (oPeriodo.FG_OTROS_EVALUADORES)
            //{
            //    vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Otros" : String.Join(", ", vTiposEvaluacion, "Otros");
            //}

            // txtTiposEvaluacion.InnerText = vTiposEvaluacion;

            //if (oPeriodo.CL_ESTADO_PERIODO.ToUpper() == "ABIERTO")
            //{
            //    lblAdvertencia.Visible = true;
            //}
            //else
            //{
            //    lblAdvertencia.Visible = false;
            //}
            // }
        }

        private void ConfigurarGrid()
        {
            rdComparativo.Columns[0].Visible = vFgFoto;
        }

        private void GenerarXmlPeriodos()
        {
            XElement periodos = new XElement("PERIODOS");
            List<int> lista = ContextoReportes.oReporteComparativo.Where(t => t.vIdReporteComparativo == vIdReporteComparativo).FirstOrDefault().vListaPeriodos;


            periodos.Add(lista.Select(t => new XElement("PERIODO", new XAttribute("ID_PERIODO", t.ToString()))));
            vXmlPeriodos = periodos.ToString();

            GeneraContexto(lista);

        }

        private void CargarDatos()
        {
            if (ContextoReportes.oReporteComparativo != null)
            {
                vIdPeriodo = ContextoReportes.oReporteComparativo.Where(t => t.vIdReporteComparativo == vIdReporteComparativo).FirstOrDefault().vIdPeriodo;



                // CargarPeriodo();
                GenerarXmlPeriodos();
            }
        }

        public HtmlGenericControl GeneraTablaHtml(int pPeriodo, int pIdEmpleado)
        {
            HtmlGenericControl vCtrlTabla = new HtmlGenericControl("table");
            if (pIdEmpleado != null && pIdEmpleado > 0)
            {
                string vDivsCeldasPo = "<table class=\"tablaColor\"> " +
                 "<tr><td class=\"puesto\"> {0}</td> </tr>" +
                 "<tr> " +
                 "<td class=\"porcentaje\"> " +
                 "<div class=\"divPorcentaje\">{1}</div> " +
                 "</td> " +
                 "<td class=\"color\"> " +
                 "<div class=\"{2}\">&nbsp;</div> " +
                 "</td> </tr>" +
                 "</table>";

                ConsultaGeneralNegocio negGen = new ConsultaGeneralNegocio();

                HtmlGenericControl vCtrlRow = new HtmlGenericControl("tr");

                List<SPE_OBTIENE_FYD_REPORTE_GLOBAL_Result> vLstEvaluadosReporte = new List<SPE_OBTIENE_FYD_REPORTE_GLOBAL_Result>();
                vLstEvaluadosReporte = negGen.ObtenerDatosReporteGlobal(pPeriodo, null, false).Where(w => w.ID_EMPLEADO == pIdEmpleado).ToList();

                foreach (var itemResult in vLstEvaluadosReporte)
                {
                    HtmlGenericControl vCtrlColumnaResultado = new HtmlGenericControl("td");
                    vCtrlColumnaResultado.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 11pt; width:100px; border-radius:2px");
                    HtmlGenericControl vCtrlDiv = new HtmlGenericControl("div");
                    vCtrlDiv.Attributes.Add("style", "padding: 10px");
                    vCtrlDiv.Attributes.Add("title", itemResult.NB_PUESTO);
                    vCtrlDiv.InnerHtml = String.Format(vDivsCeldasPo, String.Format("<a href=\"javascript:OpenDescriptivo({0})\">{1}</a>", itemResult.ID_PUESTO_PERIODO, itemResult.CL_PUESTO), itemResult.PR_CUMPLIMIENTO + "%", GenerarColor(itemResult.CL_COLOR_CUMPLIMIENTO));
                    vCtrlColumnaResultado.Controls.Add(vCtrlDiv);
                    vCtrlRow.Controls.Add(vCtrlColumnaResultado);
                }

                vCtrlTabla.Controls.Add(vCtrlRow);

                return vCtrlTabla;
            }

            return vCtrlTabla;
        }

        public DataTable ConvertToDataTable<T>(IList<T> data, List<int> periodos)
        {
            ConsultaGeneralNegocio negGen = new ConsultaGeneralNegocio();
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (int itemPeriodo in periodos)
                table.Columns.Add(itemPeriodo.ToString());

            foreach (T item in data)
            {
                int vIdEmpelado = 0;
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    if (prop.Name.Equals("ID_EMPLEADO"))
                        vIdEmpelado = int.Parse(prop.GetValue(item).ToString());

                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }
            return table;
        }

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
                vControlGrid = new HtmlGenericControl("div");
                vControlGrid.ID = colname;
                container.Controls.Add(vControlGrid);
            }
        }

        protected DataTable CrearDataTable<T>(IList<T> pLista, RadGrid pCtrlGrid)
        {
            List<int> lista = ContextoReportes.oReporteComparativo.Where(t => t.vIdReporteComparativo == vIdReporteComparativo).FirstOrDefault().vListaPeriodos;
            List<int> listPeriodos = new List<int>();
            listPeriodos = lista;
            bool exists = listPeriodos.Exists(element => element == vIdPeriodo);
            if (!exists)
                listPeriodos.Add(vIdPeriodo);

            DataTable vColumnas = ConvertToDataTable(pLista, listPeriodos);
            ConsultaGeneralNegocio negGen = new ConsultaGeneralNegocio();
            int? vMaxPuestos = 1;

            foreach (int item in listPeriodos)
            {
                vMaxPuestos = negGen.ObtenerDatosReporteGlobal(item, null, false).FirstOrDefault().NUM_PERIODOS;
                SPE_OBTIENE_FYD_PERIODO_EVALUACION_Result oPeriodos = negGen.ObtenerPeriodoEvaluacion(item);
                GridTemplateColumn vBoundColumn = new GridTemplateColumn();
                vBoundColumn.DataField = oPeriodos.ID_PERIODO.ToString();
                vBoundColumn.UniqueName = oPeriodos.ID_PERIODO.ToString();
                vBoundColumn.HeaderText = oPeriodos.CL_PERIODO;
                vBoundColumn.HeaderStyle.Width = (Unit)(vMaxPuestos * 110);
                vBoundColumn.ColumnGroupName = "gcCalificacion";
                vBoundColumn.ItemTemplate = new MyTemplate(oPeriodos.ID_PERIODO.ToString());
                vBoundColumn.FooterStyle.HorizontalAlign = HorizontalAlign.Center;
                vBoundColumn.FooterStyle.Font.Bold = true;
                vBoundColumn.AllowFiltering = false;
                vBoundColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                rdComparativo.MasterTableView.Columns.Add(vBoundColumn);
            }

            return vColumnas;
        }

        #endregion

        protected void Page_Init(object sender, System.EventArgs e)
        {
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;
            if (Request.Params["FgFoto"] != null)
            {
                vFgFoto = bool.Parse(Request.Params["FgFoto"].ToString());
                ConfigurarGrid();
            }

            if (Request.Params["IdReporteComparativo"] != null)
            {
                vIdReporteComparativo = Guid.Parse(Request.Params["IdReporteComparativo"].ToString());
                CargarDatos();
                ConsultaGeneralNegocio neg = new ConsultaGeneralNegocio();
                List<SPE_OBTIENE_FYD_EVALUADOS_COMPARATIVO_Result> vLstEvaluadores = neg.ObtenerEvaluadosComparativo(vIdPeriodo, vXmlPeriodos, vFgFoto, vIdRol);
                rdComparativo.DataSource = CrearDataTable(vLstEvaluadores, rdComparativo);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //if (Request.Params["IdReporteComparativo"] != null)
                //{
                //    vIdReporteComparativo = Guid.Parse(Request.Params["IdReporteComparativo"].ToString());
                //    CargarDatos();
                //}

                //if (Request.Params["IdPeriodo"] != null)
                //{
                //    vIdPeriodo = int.Parse(Request.Params["IdPeriodo"].ToString());
                //    CargarPeriodo();
                //}

                //if (Request.Params["FgFoto"] != null)
                //{
                //    vFgFoto = bool.Parse(Request.Params["FgFoto"].ToString());
                //    ConfigurarGrid();
                //}

                //GenerarXmlPeriodos();
            }
        }

        protected void rdComparativo_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            // ConsultaGeneralNegocio neg = new ConsultaGeneralNegocio();
            //// rdComparativo.DataSource = neg.ObtenerDatosReporteComparativo(vIdPeriodo, vFgFoto);
            // List<SPE_OBTIENE_FYD_EVALUADOS_COMPARATIVO_Result> vLstEvaluadores = neg.ObtenerEvaluadosComparativo(vIdPeriodo, vXmlPeriodos, vFgFoto);
            // rdComparativo.DataSource = CrearDataTable(vLstEvaluadores, rdComparativo);
        }

        //protected void rdComparativo_DetailTableDataBind(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        //{
        //    GridDataItem vDataItem = (GridDataItem)e.DetailTableView.ParentItem;

        //    switch (e.DetailTableView.Name)
        //    {
        //        case "gtvComparativo":
        //            int vIdPuestoEvaluadoPeriodo = int.Parse(vDataItem.GetDataKeyValue("ID_PUESTO").ToString());
        //            int vIdEmpleado;
        //            decimal vPrCumplimiento = decimal.Parse(vDataItem.GetDataKeyValue("PR_CUMPLIMIENTO").ToString());
        //            ConsultaGeneralNegocio neg = new ConsultaGeneralNegocio();

        //            vIdEmpleado = int.Parse(vDataItem.GetDataKeyValue("ID_EMPLEADO").ToString());                     

        //            //List<E_REPORTE_COMPARATIVO_DETALLE> oListaDetalleExterna = vListaDetalle.Select(t => new E_REPORTE_COMPARATIVO_DETALLE{ DS_PERIODO = t.DS_PERIODO, ID_PERIODO = t.ID_PERIODO, NB_PUESTO = t.NB_PUESTO, PR_CUMPLIMIENTO = t.PR_CUMPLIMIENTO, PR_CUMPLIMIENTO_COMPARACION = vPrCumplimiento}).ToList();
        //            e.DetailTableView.DataSource = neg.ObtenerDetalleReporteComparativo(vIdPeriodo, vIdPuestoEvaluadoPeriodo, vXmlPeriodos, vIdEmpleado, vPrCumplimiento);

        //            break;
        //        default:
        //            break;
        //    }
        //}

        protected void rdComparativo_ItemDataBound(object sender, GridItemEventArgs e)
        {
            List<int> lista = ContextoReportes.oReporteComparativo.Where(t => t.vIdReporteComparativo == vIdReporteComparativo).FirstOrDefault().vListaPeriodos;
            List<int> listPeriodos = new List<int>();
            listPeriodos = lista;
            List<SPE_OBTIENE_FYD_REPORTE_GLOBAL_Result> vLstEvaluadosReporte = new List<SPE_OBTIENE_FYD_REPORTE_GLOBAL_Result>();
            ConsultaGeneralNegocio negGen = new ConsultaGeneralNegocio();

            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                int vIdEmpleado = int.Parse(item.GetDataKeyValue("ID_EMPLEADO").ToString());
                foreach (int oPeriodo in listPeriodos)
                {
                    HtmlGenericControl vCtrlDiv = (HtmlGenericControl)item.FindControl(oPeriodo.ToString());
                    if (vCtrlDiv != null)
                    {
                        vCtrlDiv.Controls.Add(GeneraTablaHtml(oPeriodo, vIdEmpleado));
                    }
                }
            }

            if (e.Item is GridFooterItem)
            {
                GridFooterItem footer = (GridFooterItem)e.Item;
                foreach (int oPeriodo in listPeriodos)
                {
                    var vPeriodoo = negGen.ObtenerPeriodoEvaluacion(vIdPeriodo);
                    vLstEvaluadosReporte = negGen.ObtenerDatosReporteGlobal(oPeriodo, null, false).ToList();
                    if (vLstEvaluadosReporte != null && vLstEvaluadosReporte.Count > 0)
                        footer[oPeriodo.ToString()].Text = String.Format("{0:0.00}%", vLstEvaluadosReporte.Average(a => a.PR_CUMPLIMIENTO));
                    else
                        footer[oPeriodo.ToString()].Text = "0.00%";
                }

            }
        }
    }
}