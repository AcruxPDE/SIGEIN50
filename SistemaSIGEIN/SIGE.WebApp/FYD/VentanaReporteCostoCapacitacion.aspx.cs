using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace SIGE.WebApp.FYD
{
    public partial class VentanaReporteCostoCapacitacion : System.Web.UI.Page
    {
        #region Variables

        private Guid vIdReporte
        {
            get { return (Guid)ViewState["vs_vrcc_id_reporte"]; }
            set { ViewState["vs_vrcc_id_reporte"] = value; }
        }

        #endregion

        #region Funciones

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
    }
}