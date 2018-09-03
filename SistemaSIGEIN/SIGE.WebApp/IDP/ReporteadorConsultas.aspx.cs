using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Negocio.Administracion;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.IDP
{
    public partial class ReporteadorConsultas : System.Web.UI.Page
    {

        #region Variables

        private int vIdBateria
        {
            get { return (int)ViewState["vs_vIdBateria"]; }
            set { ViewState["vs_vIdBateria"] = value; }
        }

        private string vClConsulta
        {
            get { return (string)ViewState["vs_vClConsulta"]; }
            set { ViewState["vs_vClConsulta"] = value; }
        }

        public bool vFgConsultaparcial
        {
            get { return (bool)ViewState["vs_vFgConsultaparcial"]; }
            set { ViewState["vs_vFgConsultaparcial"] = value; }
        }

        private List<E_CONSULTA_PERSONAL> vListaPersonal
        {
            get { return (List<E_CONSULTA_PERSONAL>)ViewState["vs_vListaPersonal"]; }
            set { ViewState["vs_vListaPersonal"] = value; }
        }

        #endregion

        #region Metodos

        protected decimal? CalculaPorcentaje(decimal? pPorcentaje)
        {
            decimal? vPorcentaje = 0;
            if (pPorcentaje > 100)
                vPorcentaje = 100;
            else vPorcentaje = pPorcentaje;
            return vPorcentaje;
        }

        public void CargarPersonalResumida()
        {
            StiReport report = new StiReport();
            string pathValue = string.Empty;
            vListaPersonal = new List<E_CONSULTA_PERSONAL>();

            report.Load(Server.MapPath("~/Assets/reports/IDP/ReporteConsultaResumida.mrt"));
            report.Dictionary.Databases.Clear();

            pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
            report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));

            ConsultaPersonalNegocio neg = new ConsultaPersonalNegocio();
            vListaPersonal = neg.obtieneConsultaPersonalResumen(vIdBateria, vFgConsultaparcial).Select(s => new E_CONSULTA_PERSONAL
            {
                CL_CLASIFICACION = s.CL_CLASIFICACION,
                CL_COLOR = s.CL_COLOR,
                DS_COMPETENCIA = s.DS_COMPETENCIA,
                DS_NIVEL_COMPETENCIA_PERSONA = s.DS_NIVEL_COMPETENCIA_PERSONA,
                ID_COMPETENCIA = s.ID_COMPETENCIA,
                NB_COMPETENCIA = s.NB_COMPETENCIA,
                NO_BAREMO_FACTOR = s.NO_BAREMO_FACTOR,
                NO_BAREMO_PORCENTAJE = CalculaPorcentaje(s.NO_BAREMO_PORCENTAJE),
                NO_BAREMO_PROMEDIO = s.NO_BAREMO_PROMEDIO,
            }).OrderBy(s => s.CL_COMPETENCIA).ToList();

            decimal vPromedio = (decimal)vListaPersonal.Average(s => s.NO_BAREMO_PORCENTAJE);

            PruebasNegocio pNegocio = new PruebasNegocio();
            var ConsultaPersonal = pNegocio.ObtenienePruebasResultadosCandidatos(vIdBateria);

            report.Dictionary.Variables["NB_CANDIDATO"].Value = ConsultaPersonal.NB_CANDIDATO;
            report.Dictionary.Variables["CL_SOLICITUD"].Value = ConsultaPersonal.CL_SOLICITUD;
            report.Dictionary.Variables["ID_BATERIA"].Value = vIdBateria.ToString();
            if (vFgConsultaparcial == true)
            report.Dictionary.Variables["FG_COMPETENCIAS"].Value = "true";
            else
            report.Dictionary.Variables["FG_COMPETENCIAS"].Value = "false";
            report.Dictionary.Variables["PR_PROMEDIO"].Value = vPromedio.ToString("0.00");

            report.Compile();
            swvReporte.Report = report;
        }

        public void CargarPersonalDetallada()
        {
            StiReport report = new StiReport();
            string pathValue = string.Empty;
            vListaPersonal = new List<E_CONSULTA_PERSONAL>();

            report.Load(Server.MapPath("~/Assets/reports/IDP/ReporteConsultaPersonalDetallada.mrt"));
            report.Dictionary.Databases.Clear();

            pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
            report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));

            PruebasNegocio pNegocio = new PruebasNegocio();
            var ConsultaPersonal = pNegocio.ObtenienePruebasResultadosCandidatos(vIdBateria);

            report.Dictionary.Variables["NB_CANDIDATO"].Value = ConsultaPersonal.NB_CANDIDATO;
            report.Dictionary.Variables["CL_SOLICITUD"].Value = ConsultaPersonal.CL_SOLICITUD;
            report.Dictionary.Variables["ID_BATERIA"].Value = vIdBateria.ToString();

            report.Compile();
            swvReporte.Report = report;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["IdBateria"] != null & Request.Params["ClConsulta"] != null)
                {
                    vIdBateria = int.Parse(Request.Params["IdBateria"].ToString());
                    vClConsulta = Request.Params["ClConsulta"].ToString();

                    if (Request.Params["FgParcial"] != null)
                    {
                        if (Request.Params["FgParcial"].ToString() == "True")
                            vFgConsultaparcial = true;
                        else
                            vFgConsultaparcial = false;
                    }

                    switch (vClConsulta)
                    {
                        case "PERSONAL-RESUMIDA":
                            CargarPersonalResumida();
                            break;
                        case "PERSONAL-DETALLADA":
                            CargarPersonalDetallada();
                            break;
                        default:
                            break;
                    }
                }
            }

        }
    }
}