using SIGE.Entidades;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.FYD
{
    public partial class VentanaReporteListaMateriales : System.Web.UI.Page
    {
        #region Variables

        private Guid vIdReporte
        {
            get { return (Guid)ViewState["vs_vrlm_id_reporte"]; }
            set { ViewState["vs_vrlm_id_reporte"] = value; }
        }

        #endregion

        #region Funciones

        private string ConvertToHTMLTable(XElement pXmlMateriales)
        {
            string Table = "<Table border=\"1\">";
            string aux = "";
            int i = 1;
            bool alternateColor = false;

            if (pXmlMateriales.Elements("MATERIAL").Count() > 0)
            {

                Table = Table + "<tr style=\"padding: 5px;\">" +
                                    "<th style=\"padding: 5px;\">Concepto</th>" +
                                    "<th style=\"padding: 5px;\">Costo</th>" +
                                "</tr>";

                foreach (XElement item in pXmlMateriales.Elements("MATERIAL"))
                {

                    if (alternateColor)
                    {
                        aux = "<tr style=\"padding: 5px; background-color:#E6E6FA\">";
                    }
                    else
                    {
                        aux = "<tr style=\"padding: 5px;\">";
                    }

                    aux = aux + "<td style=\"padding: 5px;\">" + item.Attribute("NB_MATERIAL").Value + "</td>";
                    aux = aux + "<td style=\"padding: 5px;\">" + item.Attribute("MN_MATERIAL").Value + "</td>";

                    alternateColor = !alternateColor;
                    aux = aux + "</tr>";
                    Table = Table + aux;
                    i++;
                }

                Table = Table + "</Table>";
            }
            else
            {
                Table = "<b>(No hay datos)</b>";
            }

            return Table;
        }

        private DataTable ConvertToDataTable(List<SPE_OBTIENE_FYD_REPORTE_MATERIALES_EVENTO_Result> pListaSource)
        {
            DataTable dtDetalle = new DataTable();

            dtDetalle.Columns.Add(new DataColumn("NB_TEMA", typeof(string)));
            dtDetalle.Columns.Add(new DataColumn("HTML_MATERIAL", typeof(string)));

            foreach (SPE_OBTIENE_FYD_REPORTE_MATERIALES_EVENTO_Result item in pListaSource)
            {
                DataRow drFila = dtDetalle.NewRow();
                drFila["NB_TEMA"] = item.NB_TEMA;
                if (item.XML_MATERIALES != null)
                    drFila["HTML_MATERIAL"] = ConvertToHTMLTable(XElement.Parse(item.XML_MATERIALES));
                dtDetalle.Rows.Add(drFila);
            }

            return dtDetalle;
        }

        private string CrearXmlSeleccion(List<int> pListaSource, string pSingular, string pPlural, string pIdentificador)
        {
            XElement vXmlFiltro = new XElement(pPlural);

            if (pListaSource.Count > 0)
            {
                vXmlFiltro.Add(pListaSource.Select(t => new XElement(pSingular, new XAttribute(pIdentificador, t.ToString()))));
                return vXmlFiltro.ToString();
            }
            else
            {
                return null;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["IdReporte"] != null)
                {
                    vIdReporte = Guid.Parse(Request.Params["IdReporte"].ToString());
                }
            }
        }

        protected void rgCursos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ConsultasFYDNegocio neg = new ConsultasFYDNegocio();

            if (ContextoReportes.oReporteFyd != null)
            {
                E_REPORTE_FYD oReporte = ContextoReportes.oReporteFyd.Where(t => t.ID_REPORTE_FYD == vIdReporte).FirstOrDefault();

                if (oReporte != null)
                {

                    string vXmlCursos = CrearXmlSeleccion(oReporte.LISTA_CURSOS, "CURSO", "CURSOS", "ID_CURSO");
                    string vXmlInstructores = CrearXmlSeleccion(oReporte.LISTA_INSTRUCTORES, "INSTRUCTOR", "INSTRUCTORES", "ID_INSTRUCTOR");
                    string vXmlCompetencias = CrearXmlSeleccion(oReporte.LISTA_COMPETENCIAS, "COMPETENCIA", "COMPETENCIAS", "ID_COMPETENCIA");
                    string vXmlParticipantes = CrearXmlSeleccion(oReporte.LISTA_PARTICIPANTES, "PARTICIPANTE", "PARTICIPANTES", "ID_EMPLEADO");
                    string vXmlEventos = CrearXmlSeleccion(oReporte.LISTA_EVENTOS, "EVENTO", "EVENTOS", "ID_EVENTO");

                    rgCursos.DataSource = neg.ReporteCursosRealizados(oReporte.FE_INICIO, oReporte.FE_FINAL, oReporte.CL_TIPO_CURSO, vXmlCursos, vXmlInstructores, vXmlCompetencias, vXmlParticipantes, vXmlEventos);
                }
            }
        }

        protected void rgCursos_DetailTableDataBind(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            GridDataItem vDataItem = (GridDataItem)e.DetailTableView.ParentItem;

            switch (e.DetailTableView.Name)
            {
                case "gtvMaterial":
                    int vIdEvento;
                    ConsultasFYDNegocio neg = new ConsultasFYDNegocio();

                    vIdEvento = int.Parse(vDataItem.GetDataKeyValue("ID_EVENTO").ToString());
                    e.DetailTableView.DataSource = ConvertToDataTable(neg.ReporteMaterialesPorEvento(vIdEvento));
                    break;
                default:
                    break;
            }
        }
    }
}