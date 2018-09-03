using SIGE.Entidades;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.FYD
{
    public partial class ReporteGlobalImpresion : System.Web.UI.Page
    {
        #region Variables

        private int? vIdEmpresa;

        public int vIdPeriodo
        {
            get { return (int)ViewState["vs_rg_id_periodo"]; }
            set { ViewState["vs_rg_id_periodo"] = value; }
        }

        public bool vFgFoto
        {
            get { return (bool)ViewState["vs_rg_fg_foto"]; }
            set { ViewState["vs_rg_fg_foto"] = value; }
        }

        public bool vFgGrid
        {
            get { return (bool)ViewState["vs_rg_fg_grid"]; }
            set { ViewState["vs_rg_fg_grid"] = value; }
        }

        public Guid vIdReporteGlobal
        {
            get { return (Guid)ViewState["vs_rg_id_reporte_global"]; }
            set { ViewState["vs_rg_id_reporte_global"] = value; }
        }

        private string vXmlEmpleados
        {
            get { return (string)ViewState["vs_rg_xml_empleados"]; }
            set { ViewState["vs_rg_xml_empleados"] = value; }
        }

        private DataTable vReporteGlobal
        {
            get { return (DataTable)ViewState["vs_vReporteGlobal"]; }
            set { ViewState["vs_vReporteGlobal"] = value; }
        }

        private List<E_PUESTOS_EVALUADOS> vLstPuestosEvaluadosGlobal
        {
            get { return (List<E_PUESTOS_EVALUADOS>)ViewState["vs_vLstPuestosEvaluados"]; }
            set { ViewState["vs_vLstPuestosEvaluados"] = value; }
        }

        private List<E_EVALUADOS_REPORTE_GLOBAL> vLstEvaluadosGlobal
        {
            get { return (List<E_EVALUADOS_REPORTE_GLOBAL>)ViewState["vs_vLstEvaluadosGlobal"]; }
            set { ViewState["vs_vLstEvaluadosGlobal"] = value; }
        }

        #endregion

        #region Funciones

        private void CargarDatos()
        {
            if (ContextoReportes.oReporteGlobal != null)
            {
                vIdPeriodo = ContextoReportes.oReporteGlobal.Where(t => t.vIdReporteGlobal == vIdReporteGlobal).FirstOrDefault().vIdPeriodo;

                CargarPeriodo();
                CrearXmlEmpleados();
            }
        }

        private string ObtieneColorPromedio(decimal pPromedio)
        {
            string vDivColor = "";
            if (pPromedio > 90)
                vDivColor = "green";
            else if (pPromedio >= 70)
                vDivColor = "yellow";
            else
                vDivColor = "red";

            return vDivColor;
        }

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
                if (pNota != null)
                {
                    if (pNota.DS_NOTA != null)
                    {
                        return pNota.DS_NOTA.ToString();
                    }
                    else return "";
                }
                else
                    return "";
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

        private void CargarPeriodo()
        {
            ConsultaGeneralNegocio neg = new ConsultaGeneralNegocio();
            SPE_OBTIENE_FYD_PERIODO_EVALUACION_Result oPeriodo = neg.ObtenerPeriodoEvaluacion(vIdPeriodo);
            string vTiposEvaluacion = "";

            if (oPeriodo != null)
            {
                // txtPeriodo.InnerText = oPeriodo.NB_PERIODO;
                txtClave.InnerText = oPeriodo.CL_PERIODO;
                txtDescripcion.InnerText = oPeriodo.DS_PERIODO;


                if (oPeriodo.DS_NOTAS != null)
                {
                    XElement vNotas = XElement.Parse(oPeriodo.DS_NOTAS);
                    if (vNotas != null)
                    {
                        txtDsNotas.InnerHtml = validarDsNotas(oPeriodo.DS_NOTAS);
                    }
                }


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

                txtTiposEvaluacion.InnerText = vTiposEvaluacion;

                //if (oPeriodo.CL_ESTADO_PERIODO.ToUpper() == "ABIERTO")
                //{
                //    lblAdvertencia.Visible = true;
                //}
                //else
                //{
                //    lblAdvertencia.Visible = false;
                //}

            }
        }

        private void CrearXmlEmpleados()
        {
            XElement oXmlEmpleados = new XElement("EMPLEADOS");
            List<int> oListaEmpleados = ContextoReportes.oReporteGlobal.Where(t => t.vIdReporteGlobal == vIdReporteGlobal).FirstOrDefault().vListaEmpleados;

            if (oListaEmpleados.Count > 0)
            {
                oXmlEmpleados.Add(oListaEmpleados.Select(t => new XElement("EMPLEADO", new XAttribute("ID_EMPLEADO", t.ToString()))));
                vXmlEmpleados = oXmlEmpleados.ToString();
            }
            else
            {
                vXmlEmpleados = null;
            }
        }

        private HtmlGenericControl GenerarReporteGlobal()
        {
            decimal vPrGlobal = 1;
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

            List<SPE_OBTIENE_FYD_EVALUADOS_CONFIGURACION_Result> vLstEvaluados = new List<SPE_OBTIENE_FYD_EVALUADOS_CONFIGURACION_Result>();
            List<SPE_OBTIENE_FYD_REPORTE_GLOBAL_Result> vLstEvaluadosFoto = new List<SPE_OBTIENE_FYD_REPORTE_GLOBAL_Result>();
            ConsultaIndividualNegocio neg = new ConsultaIndividualNegocio();
            ConsultaGeneralNegocio negGen = new ConsultaGeneralNegocio();
            vLstEvaluados = neg.ObtenerEvaluados(vIdPeriodo, vIdEmpresa);
            vLstEvaluadosFoto = negGen.ObtenerDatosReporteGlobal(vIdPeriodo, vXmlEmpleados, vFgFoto).ToList();
            int? vMaxPuestos = 1;
            int vTotalPromedios = 1;
            if (vLstEvaluados.Count > 0)
            {
                vMaxPuestos = negGen.ObtenerDatosReporteGlobal(vIdPeriodo, vXmlEmpleados, false).FirstOrDefault().NUM_PERIODOS;
                vTotalPromedios = negGen.ObtenerDatosReporteGlobal(vIdPeriodo, vXmlEmpleados, false).Count;
            }


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

            HtmlGenericControl vCtrlTh4 = new HtmlGenericControl("th");
            vCtrlTh4.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; text-align:center; background-color:#F5F5F5; height: 50px;  border-top-right-radius:4px");
            vCtrlTh4.Attributes.Add("colspan", vMaxPuestos.ToString());
            vCtrlTh4.InnerText = String.Format("{0}", "Calificación");
            vCtrlColumn.Controls.Add(vCtrlTh4);

            vCtrlTabla.Controls.Add(vCtrlColumn);

            foreach (var item in vLstEvaluados)
            {
                HtmlGenericControl vCtrlRow = new HtmlGenericControl("tr");

                if (vFgFoto == true)
                {
                    HtmlGenericControl vCtrlColumnaFoto = new HtmlGenericControl("td");
                    vCtrlColumnaFoto.Attributes.Add("style", "border: 1px solid gray; border-radius:2px");
                    HtmlGenericControl vCtrlDiv = new HtmlGenericControl("div");
                    vCtrlDiv.Attributes.Add("style", "padding: 2px 2px 2px 2px;");

                    var vResultado = vLstEvaluadosFoto.Where(t => t.ID_EMPLEADO == item.ID_EMPLEADO && t.FI_FOTOGRAFIA != null).FirstOrDefault();
                    if (vResultado != null)
                    {
                        vCtrlDiv.InnerHtml = String.Format("{0}", "<img id=\"profileImage\" height=\"110\" width=\"80\" src=\"data:image/jpg;base64, " + Convert.ToBase64String(vResultado.FI_FOTOGRAFIA) + "\">");
                    }
                    else
                    {
                        vCtrlDiv.InnerHtml = String.Format("{0}", "<img id=\"profileImage\" height=\"110\" width=\"80\" border=\"5\">");
                    }
                    vCtrlColumnaFoto.Controls.Add(vCtrlDiv);
                    vCtrlRow.Controls.Add(vCtrlColumnaFoto);
                }

                HtmlGenericControl vCtrlColumnaClEval = new HtmlGenericControl("td");
                vCtrlColumnaClEval.Attributes.Add("title", "Empleado: " + item.NB_EMPLEADO_COMPLETO);
                vCtrlColumnaClEval.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; text-align:center; padding: 3px; border-radius:2px");
                vCtrlColumnaClEval.InnerText = String.Format("{0}", item.CL_EMPLEADO);
                vCtrlRow.Controls.Add(vCtrlColumnaClEval);

                HtmlGenericControl vCtrlColumnaNbEval = new HtmlGenericControl("td");
                vCtrlColumnaNbEval.Attributes.Add("title", "Clave del empleado: " + item.CL_EMPLEADO);
                vCtrlColumnaNbEval.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; padding: 3px; border-radius:2px");
                vCtrlColumnaNbEval.InnerHtml = String.Format("{0}",  item.NB_EMPLEADO_COMPLETO );
                vCtrlRow.Controls.Add(vCtrlColumnaNbEval);

                HtmlGenericControl vCtrlColumnaPuestoEval = new HtmlGenericControl("td");
                vCtrlColumnaPuestoEval.Attributes.Add("title", "Clave del puesto: " + item.CL_PUESTO);
                vCtrlColumnaPuestoEval.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; padding: 3px; border-radius:2px");
                vCtrlColumnaPuestoEval.InnerText = String.Format("{0}", item.NB_PUESTO);
                vCtrlRow.Controls.Add(vCtrlColumnaPuestoEval);

                List<SPE_OBTIENE_FYD_REPORTE_GLOBAL_Result> vLstEvaluadosReporte = new List<SPE_OBTIENE_FYD_REPORTE_GLOBAL_Result>();
                vLstEvaluadosReporte = negGen.ObtenerDatosReporteGlobal(vIdPeriodo, vXmlEmpleados, false).Where(w => w.ID_EMPLEADO == item.ID_EMPLEADO).ToList();

                foreach (var itemResult in vLstEvaluadosReporte)
                {
                    HtmlGenericControl vCtrlColumnaResultado = new HtmlGenericControl("td");
                    vCtrlColumnaResultado.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; width:100px; border-radius:2px");
                    HtmlGenericControl vCtrlDiv = new HtmlGenericControl("div");
                    vCtrlDiv.Attributes.Add("style", "padding: 10px");
                    vCtrlDiv.Attributes.Add("title", itemResult.NB_PUESTO);
                    vCtrlDiv.InnerHtml = String.Format(vDivsCeldasPo,  itemResult.CL_PUESTO, itemResult.PR_CUMPLIMIENTO + "%", GenerarColor(itemResult.CL_COLOR_CUMPLIMIENTO));
                    vCtrlColumnaResultado.Controls.Add(vCtrlDiv);
                    vCtrlRow.Controls.Add(vCtrlColumnaResultado);
                    vPrGlobal = vPrGlobal + itemResult.PR_CUMPLIMIENTO;
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

            HtmlGenericControl vCtrlFooterPr = new HtmlGenericControl("td");
            vCtrlFooterPr.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; font-weight:bold; text-align: center; border-top: 1.1px solid gray; background-color:" + ObtieneColorPromedio((vPrGlobal / vTotalPromedios)) + "; height: 30px;  border-bottom-right-radius:4px");
            vCtrlFooterPr.Attributes.Add("colspan", vMaxPuestos.ToString());
            if (vLstEvaluados.Count > 0)
                vCtrlFooterPr.InnerText = String.Format("{0:0.00}%", vPrGlobal / vTotalPromedios);
            else
                vCtrlFooterPr.InnerText = String.Format("{0}%", 0);

            vCtrlRowFooter.Controls.Add(vCtrlFooterPr);

            vCtrlTabla.Controls.Add(vCtrlRowFooter);

            return vCtrlTabla;
        }

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

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                if (Request.Params["IdReporteGlobal"] != null)
                {
                    vIdReporteGlobal = Guid.Parse(Request.Params["IdReporteGlobal"].ToString());
                    CargarDatos();
                }

                if (Request.Params["Fgfoto"] != null)
                {
                    vFgFoto = bool.Parse(Request.Params["FgFoto"].ToString());
                }

                if (Request.Params["FgGrid"] != null)
                {
                    vFgGrid = bool.Parse(Request.Params["FgGrid"].ToString());
                }

               // ConfigurarGrid();
                  dvReporteGeneral.Controls.Add(GenerarReporteGlobal());
            }
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
        }
    }
}