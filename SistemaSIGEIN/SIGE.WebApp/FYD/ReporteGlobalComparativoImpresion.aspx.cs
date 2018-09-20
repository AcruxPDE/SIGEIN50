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
    public partial class ReporteGlobalComparativoImpresion : System.Web.UI.Page
    {
        #region Variables

        private int? vIdRol;

        public int vIdPeriodo
        {
            get { return (int)ViewState["vs_rc_id_periodo"]; }
            set { ViewState["vs_rc_id_periodo"] = value; }
        }

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
            vCtrlTabla.Attributes.Add("style", "border: 1px solid gray;");

            HtmlGenericControl vCtrlColumn = new HtmlGenericControl("tr");

            HtmlGenericControl vCtrlTh = new HtmlGenericControl("th");
            vCtrlTh.Attributes.Add("style", "border: 1px solid gray;");
            vCtrlTh.InnerText = String.Format("{0}", "Periodo");
            vCtrlColumn.Controls.Add(vCtrlTh);

            HtmlGenericControl vCtrlTh2 = new HtmlGenericControl("th");
            vCtrlTh2.Attributes.Add("style", "border: 1px solid gray;");
            vCtrlTh2.InnerText = String.Format("{0}", "Descripción");
            vCtrlColumn.Controls.Add(vCtrlTh2);

            HtmlGenericControl vCtrlTh3 = new HtmlGenericControl("th");
            vCtrlTh3.Attributes.Add("style", "border: 1px solid gray;");
            vCtrlTh3.InnerText = String.Format("{0}", "Tipo de evaluación");
            vCtrlColumn.Controls.Add(vCtrlTh3);

            vCtrlTabla.Controls.Add(vCtrlColumn);

            ConsultaGeneralNegocio neg = new ConsultaGeneralNegocio();

            bool exists = pListaPeriodos.Exists(element => element == vIdPeriodo);
            if (!exists)
            {
                var oPeriodoOriginal = neg.ObtenerPeriodoEvaluacion(vIdPeriodo);

                HtmlGenericControl vCtrlColumnO = new HtmlGenericControl("tr");
                HtmlGenericControl vCtrlColumnaClPeriodoO = new HtmlGenericControl("td");
                vCtrlColumnaClPeriodoO.Attributes.Add("style", "border: 1px solid gray;");
                vCtrlColumnaClPeriodoO.InnerText = String.Format("{0}", oPeriodoOriginal.CL_PERIODO);
                vCtrlColumnO.Controls.Add(vCtrlColumnaClPeriodoO);

                HtmlGenericControl vCtrlColumnaNbPeriodoO = new HtmlGenericControl("td");
                vCtrlColumnaNbPeriodoO.Attributes.Add("style", "border: 1px solid gray;");
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
                vCtrlColumnaTipoPeriodoO.Attributes.Add("style", "border: 1px solid gray;");
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
                    vCtrlColumnaClPeriodo.Attributes.Add("style", "border: 1px solid gray;");
                    vCtrlColumnaClPeriodo.InnerText = String.Format("{0}", oPeriodo.CL_PERIODO);
                    vCtrlRow.Controls.Add(vCtrlColumnaClPeriodo);

                    HtmlGenericControl vCtrlColumnaNbPeriodo = new HtmlGenericControl("td");
                    vCtrlColumnaNbPeriodo.Attributes.Add("style", "border: 1px solid gray;");
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
                    vCtrlColumnaTipoPeriodo.Attributes.Add("style", "border: 1px solid gray;");
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
                GenerarXmlPeriodos();
            }
        }

        public HtmlGenericControl GenerarReporteComparativo()
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


            ConsultaGeneralNegocio neg = new ConsultaGeneralNegocio();
            List<SPE_OBTIENE_FYD_EVALUADOS_COMPARATIVO_Result> vLstEvaluadores = neg.ObtenerEvaluadosComparativo(vIdPeriodo, vXmlPeriodos, vFgFoto, vIdRol);
            List<int> lista = ContextoReportes.oReporteComparativo.Where(t => t.vIdReporteComparativo == vIdReporteComparativo).FirstOrDefault().vListaPeriodos;
            bool exists = lista.Exists(element => element == vIdPeriodo);
            int? vMaxPuestosPeriodo = 1;

            if (!exists)
                lista.Add(vIdPeriodo);
            decimal? vPrGlobal = 0;
            int vTotalPromedios = 1;
            decimal? vPromedioPeriodo = 0;


            HtmlGenericControl vCtrlTabla = new HtmlGenericControl("table");

            HtmlGenericControl vCtrlColumn = new HtmlGenericControl("tr");
            vCtrlColumn.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt;");

            if (vFgFoto == true)
            {
                HtmlGenericControl vCtrlThFoto = new HtmlGenericControl("th");
                vCtrlThFoto.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; width:80px; background-color:#F5F5F5; height: 50px; padding: 3px; border-top-left-radius:4px");
                vCtrlThFoto.InnerText = String.Format("{0}", "Fotografía");
                vCtrlColumn.Controls.Add(vCtrlThFoto);
            }

            HtmlGenericControl vCtrlTh = new HtmlGenericControl("th");
            vCtrlTh.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; width:100px; background-color:#F5F5F5; height: 50px; padding: 3px; border-top-left-radius:4px");
            vCtrlTh.InnerText = String.Format("{0}", "No. de empleado");
            vCtrlColumn.Controls.Add(vCtrlTh);

            HtmlGenericControl vCtrlTh2 = new HtmlGenericControl("th");
            vCtrlTh2.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; width:300px; background-color:#F5F5F5; height: 50px; padding: 3px;");
            vCtrlTh2.InnerText = String.Format("{0}", "Nombre completo");
            vCtrlColumn.Controls.Add(vCtrlTh2);

            HtmlGenericControl vCtrlTh3 = new HtmlGenericControl("th");
            vCtrlTh3.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; width:300px; background-color:#F5F5F5; height: 50px; padding: 3px; ");
            vCtrlTh3.InnerText = String.Format("{0}", "Puesto");
            vCtrlColumn.Controls.Add(vCtrlTh3);

            foreach (int item in lista)
            {
                vMaxPuestosPeriodo = neg.ObtenerDatosReporteGlobal(item, null, false).FirstOrDefault().NUM_PERIODOS;
                var oPeriodo = neg.ObtenerPeriodoEvaluacion(item);
                HtmlGenericControl vCtrlTh4 = new HtmlGenericControl("th");
                vCtrlTh4.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; text-align:center; background-color:#F5F5F5; height: 50px;  border-top-right-radius:4px");
                vCtrlTh4.Attributes.Add("colspan", vMaxPuestosPeriodo.ToString());
                vCtrlTh4.InnerText = String.Format("{0}", oPeriodo.CL_PERIODO);
                vCtrlColumn.Controls.Add(vCtrlTh4);
            }

            vCtrlTabla.Controls.Add(vCtrlColumn);

            foreach (var item in vLstEvaluadores)
            {
                HtmlGenericControl vCtrlRow = new HtmlGenericControl("tr");

                if (vFgFoto == true)
                {
                    HtmlGenericControl vCtrlColumnaFoto = new HtmlGenericControl("td");
                    vCtrlColumnaFoto.Attributes.Add("style", "border: 1px solid gray; border-radius:2px");
                    HtmlGenericControl vCtrlDiv = new HtmlGenericControl("div");
                    vCtrlDiv.Attributes.Add("style", "padding: 2px 2px 2px 2px;");

                    var vResultado = vLstEvaluadores.Where(t => t.ID_EMPLEADO == item.ID_EMPLEADO && t.FI_ARCHIVO != null).FirstOrDefault();
                    if (vResultado != null)
                    {
                        vCtrlDiv.InnerHtml = String.Format("{0}", "<img id=\"profileImage\" height=\"110\" width=\"80\" src=\"data:image/jpg;base64, " + Convert.ToBase64String(vResultado.FI_ARCHIVO) + "\">");
                    }
                    else
                    {
                        vCtrlDiv.InnerHtml = String.Format("{0}", "<img id=\"profileImage\" height=\"110\" width=\"80\" border=\"5\">");
                    }
                    vCtrlColumnaFoto.Controls.Add(vCtrlDiv);
                    vCtrlRow.Controls.Add(vCtrlColumnaFoto);
                }

                HtmlGenericControl vCtrlColumnaClEval = new HtmlGenericControl("td");
                vCtrlColumnaClEval.Attributes.Add("title", "Empleado: " + item.NB_EVALUADO);
                vCtrlColumnaClEval.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; text-align:center; padding: 3px; border-radius:2px");
                vCtrlColumnaClEval.InnerText = String.Format("{0}", item.CL_EVALUADO);
                vCtrlRow.Controls.Add(vCtrlColumnaClEval);

                HtmlGenericControl vCtrlColumnaNbEval = new HtmlGenericControl("td");
                vCtrlColumnaNbEval.Attributes.Add("title", "Clave del empleado: " + item.CL_EVALUADO);
                vCtrlColumnaNbEval.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; padding: 3px; border-radius:2px");
                vCtrlColumnaNbEval.InnerHtml = String.Format("{0}", item.NB_EVALUADO);
                vCtrlRow.Controls.Add(vCtrlColumnaNbEval);

                HtmlGenericControl vCtrlColumnaPuestoEval = new HtmlGenericControl("td");
                vCtrlColumnaPuestoEval.Attributes.Add("title", "Clave del puesto: " + item.CL_PUESTO);
                vCtrlColumnaPuestoEval.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; padding: 3px; border-radius:2px");
                vCtrlColumnaPuestoEval.InnerText = String.Format("{0}", item.NB_PUESTO);
                vCtrlRow.Controls.Add(vCtrlColumnaPuestoEval);


                foreach (int idPeriodo in lista)
                {
                    List<SPE_OBTIENE_FYD_REPORTE_GLOBAL_Result> vLstEvaluadosReporte = new List<SPE_OBTIENE_FYD_REPORTE_GLOBAL_Result>();
                    vLstEvaluadosReporte = neg.ObtenerDatosReporteGlobal(idPeriodo, null, false).Where(w => w.ID_EMPLEADO == item.ID_EMPLEADO).ToList();

                    foreach (var itemResult in vLstEvaluadosReporte)
                    {
                        HtmlGenericControl vCtrlColumnaResultado = new HtmlGenericControl("td");
                        vCtrlColumnaResultado.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; width:100px; border-radius:2px");
                        HtmlGenericControl vCtrlDiv = new HtmlGenericControl("div");
                        vCtrlDiv.Attributes.Add("style", "padding: 10px");
                        vCtrlDiv.Attributes.Add("title", itemResult.NB_PUESTO);
                        vCtrlDiv.InnerHtml = String.Format(vDivsCeldasPo, itemResult.CL_PUESTO, itemResult.PR_CUMPLIMIENTO + "%", GenerarColor(itemResult.CL_COLOR_CUMPLIMIENTO));
                        vCtrlColumnaResultado.Controls.Add(vCtrlDiv);
                        vCtrlRow.Controls.Add(vCtrlColumnaResultado);
                    }

                    if (vLstEvaluadosReporte == null || vLstEvaluadosReporte.Count < 1)
                    {
                        vMaxPuestosPeriodo = neg.ObtenerDatosReporteGlobal(idPeriodo, null, false).FirstOrDefault().NUM_PERIODOS;
                        HtmlGenericControl vCtrlColumnaResultado = new HtmlGenericControl("td");
                        vCtrlColumnaResultado.Attributes.Add("colspan", vMaxPuestosPeriodo.ToString());
                        vCtrlColumnaResultado.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; width:100px; border-radius:2px");
                        vCtrlColumnaResultado.InnerHtml = "&nbsp;";
                        vCtrlRow.Controls.Add(vCtrlColumnaResultado);
                    }
                }

                vCtrlTabla.Controls.Add(vCtrlRow);
            }

            HtmlGenericControl vCtrlRowFooter = new HtmlGenericControl("tr");

            HtmlGenericControl vCtrlFooterTol = new HtmlGenericControl("td");
            vCtrlFooterTol.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; font-weight:bold;  text-align: right; background-color:#F5F5F5; height: 30px;  border-bottom-left-radius:4px");

            if (vFgFoto)
                vCtrlFooterTol.Attributes.Add("colspan", "4");
            else
                vCtrlFooterTol.Attributes.Add("colspan", "3");

            vCtrlFooterTol.InnerText = String.Format("{0}", "Total:");
            vCtrlRowFooter.Controls.Add(vCtrlFooterTol);

            foreach (int itemPeriodo in lista)
            {
                vMaxPuestosPeriodo = neg.ObtenerDatosReporteGlobal(itemPeriodo, null, false).FirstOrDefault().NUM_PERIODOS;
                vTotalPromedios = neg.ObtenerDatosReporteGlobal(itemPeriodo, null, false).Count;
                vPrGlobal = neg.ObtenerDatosReporteGlobal(itemPeriodo, null, false).Average(a => a.PR_CUMPLIMIENTO);

                HtmlGenericControl vCtrlFooterPr = new HtmlGenericControl("td");
                vCtrlFooterPr.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; font-weight:bold; text-align: center; border-top: 1.1px solid gray; height: 30px;  border-bottom-right-radius:4px");
                vCtrlFooterPr.Attributes.Add("colspan", vMaxPuestosPeriodo.ToString());
                if (vLstEvaluadores.Count > 0)
                    vCtrlFooterPr.InnerText = String.Format("{0:0.00}%", vPrGlobal);
                else
                    vCtrlFooterPr.InnerText = String.Format("{0}%", 0);

                vCtrlRowFooter.Controls.Add(vCtrlFooterPr);
            }

            vCtrlTabla.Controls.Add(vCtrlRowFooter);

            return vCtrlTabla;
        }

        #endregion

        protected void Page_Init(object sender, System.EventArgs e)
        {
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;
            if (Request.Params["FgFoto"] != null)
            {
                vFgFoto = bool.Parse(Request.Params["FgFoto"].ToString());
            }

            if (Request.Params["IdReporteComparativo"] != null)
            {
                vIdReporteComparativo = Guid.Parse(Request.Params["IdReporteComparativo"].ToString());
                CargarDatos();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                dvReporteGeneral.Controls.Add(GenerarReporteComparativo());
            }
        }

    }
}