using SIGE.Entidades;
using SIGE.Negocio.FormacionDesarrollo;
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
    public partial class VentanaEvaluacionCompetenciasPS : System.Web.UI.Page
    {
        #region Variables

        private int vIdEmpleadoSuceder
        {
            get { return (int)ViewState["vs_vIdEmpleadoSuceder"]; }
            set { ViewState["vs_vIdEmpleadoSuceder"] = value; }
        }

        private int vIdEmpleadoSucesor
        {
            get { return (int)ViewState["vs_vIdEmpleadoSucesor"]; }
            set { ViewState["vs_vIdEmpleadoSucesor"] = value; }
        }

        private int vIdPuestoSuceder
        {
            get { return (int)ViewState["vs_vIdPuestoSuceder"]; }
            set { ViewState["vs_vIdPuestoSuceder"] = value; }
        }

        #endregion

        #region Funciones

        private void ConfigurarGrid(string pTotalCompatibilidad)
        {
            PlanSucesionNegocio neg = new PlanSucesionNegocio();

            List<SPE_OBTIENE_EMPLEADOS_Result> emp = new List<SPE_OBTIENE_EMPLEADOS_Result>();
            XElement xml_empleado = new XElement("SELECCION", new XElement("FILTRO", new XAttribute("CL_TIPO", "EMPLEADO"), new XElement("EMP", new XAttribute("ID_EMPLEADO", vIdEmpleadoSucesor))));

            emp = neg.ObtieneEmpleados(xml_empleado).ToList();

            grdGeneralIndividual.Columns[3].HeaderText = "Porcentaje de compatibilidad <br>" + "<a style='color:#025F95' title='" + emp.FirstOrDefault().M_PUESTO_NB_PUESTO + "' href='javascript:OpenPuesto(" + emp.FirstOrDefault().M_PUESTO_ID_PUESTO + ")'>" + emp.FirstOrDefault().M_PUESTO_CL_PUESTO + "</a>";
            grdGeneralIndividual.Columns[3].FooterText = pTotalCompatibilidad + "%";
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PlanSucesionNegocio neg = new PlanSucesionNegocio();

                if (Request.Params["idEmpleadoSucesor"] != null)
                {
                    vIdEmpleadoSucesor = int.Parse(Request.Params["idEmpleadoSucesor"].ToString());

                    List<SPE_OBTIENE_EMPLEADOS_Result> emp = new List<SPE_OBTIENE_EMPLEADOS_Result>();
                    XElement xml_empleado = new XElement("SELECCION", new XElement("FILTRO", new XAttribute("CL_TIPO", "EMPLEADO"), new XElement("EMP", new XAttribute("ID_EMPLEADO", vIdEmpleadoSucesor))));

                    emp = neg.ObtieneEmpleados(xml_empleado).ToList();

                    rgSucesor.DataSource = emp;
                }

                if (Request.Params["IdEmpleadoSuceder"] != null)
                {
                    vIdEmpleadoSuceder = int.Parse(Request.Params["IdEmpleadoSuceder"].ToString());

                    List<SPE_OBTIENE_EMPLEADOS_Result> emp = new List<SPE_OBTIENE_EMPLEADOS_Result>();
                    XElement xml_empleado = new XElement("SELECCION", new XElement("FILTRO", new XAttribute("CL_TIPO", "EMPLEADO"), new XElement("EMP", new XAttribute("ID_EMPLEADO", vIdEmpleadoSuceder))));

                    emp = neg.ObtieneEmpleados(xml_empleado).ToList();

                    rgSuceder.DataSource = emp;
                }

                if (Request.Params["PuestoSuceder"] != null)
                {
                    vIdPuestoSuceder = int.Parse(Request.Params["PuestoSuceder"].ToString());
                }
            }
        }

        protected void grdCompetencias_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            List<KeyValuePair<string, string>> lista = new List<KeyValuePair<string, string>>();

            lista.Add(new KeyValuePair<string, string>("Competencias Genéricas", "Esta dimensión evalúa comportamientos elementales asociados a desempeños comunes a diversas ocupaciones y que están asociados a conocimientos y habilidades de índole formativa."));
            lista.Add(new KeyValuePair<string, string>("Competencias Específicas", "Esta dimensión evalúa los comportamientos asociados a conocimientos y habilidades vinculados con la función productiva que la persona desarrolla en su puesto."));
            lista.Add(new KeyValuePair<string, string>("Competencias Institucionales", "Esta dimensión evalúa los comportamientos y actitudes que reflejan y van estrechamente relacionados con los valores empresariales de la organización."));

            grdCompetencias.DataSource = lista;
        }

        protected void grdGeneralIndividual_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PlanSucesionNegocio neg = new PlanSucesionNegocio();
            List<SPE_OBTIENE_EVALUACION_COMPETENCIAS_PLAN_SUCESION_Result> vLstCompetencias = new List<SPE_OBTIENE_EVALUACION_COMPETENCIAS_PLAN_SUCESION_Result>();
            vLstCompetencias = neg.ObtieneEvalCompetenciasSucesion(vIdEmpleadoSuceder, vIdEmpleadoSucesor, vIdPuestoSuceder, null).ToList();
            grdGeneralIndividual.DataSource = vLstCompetencias;

            if (vLstCompetencias.Count > 0)
                ConfigurarGrid(vLstCompetencias.FirstOrDefault().PR_COMPATIBILIDAD_TOTAL.ToString());
        }

        protected void grdGeneralIndividual_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem gridItem = e.Item as GridDataItem;

                string vDsEventoPeriodo = gridItem.GetDataKeyValue("DS_EVENTO").ToString();
                string vClEvento = gridItem.GetDataKeyValue("CL_EVENTO").ToString();
                gridItem["FECHA"].ToolTip = vClEvento + "/" + vDsEventoPeriodo;
            }
        }

        protected void rgSuceder_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem gridItem = e.Item as GridDataItem;

                string vEmpleado = gridItem.GetDataKeyValue("M_EMPLEADO_CL_EMPLEADO").ToString() + ": " + gridItem.GetDataKeyValue("M_EMPLEADO_NB_EMPLEADO_COMPLETO").ToString();
                string vPuesto = gridItem.GetDataKeyValue("M_PUESTO_CL_PUESTO").ToString() + ": " + gridItem.GetDataKeyValue("M_PUESTO_NB_PUESTO").ToString();
                int vIdEmpleado = int.Parse(gridItem.GetDataKeyValue("M_EMPLEADO_ID_EMPLEADO").ToString());
                int vIdPuesto = int.Parse(gridItem.GetDataKeyValue("M_PUESTO_ID_PUESTO").ToString());
                gridItem["M_EMPLEADO_NB_EMPLEADO_COMPLETO"].Text = "<a href='javascript:OpenInventario(" + vIdEmpleado + ")'>" + vEmpleado + "</a>";
                gridItem["M_PUESTO_NB_PUESTO"].Text = "<a href='javascript:OpenPuesto(" + vIdPuesto + ")'>" + vPuesto + "</a>";
            }
        }

        protected void rgSucesor_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem gridItem = e.Item as GridDataItem;

                string vEmpleado = gridItem.GetDataKeyValue("M_EMPLEADO_CL_EMPLEADO").ToString() + ": " + gridItem.GetDataKeyValue("M_EMPLEADO_NB_EMPLEADO_COMPLETO").ToString();
                string vPuesto = gridItem.GetDataKeyValue("M_PUESTO_CL_PUESTO").ToString() + ": " + gridItem.GetDataKeyValue("M_PUESTO_NB_PUESTO").ToString();
                int vIdEmpleado = int.Parse(gridItem.GetDataKeyValue("M_EMPLEADO_ID_EMPLEADO").ToString());
                int vIdPuesto = int.Parse(gridItem.GetDataKeyValue("M_PUESTO_ID_PUESTO").ToString());
                gridItem["M_EMPLEADO_NB_EMPLEADO_COMPLETO"].Text = "<a href='javascript:OpenInventario(" + vIdEmpleado + ")'>" + vEmpleado + "</a>";
                gridItem["M_PUESTO_NB_PUESTO"].Text = "<a href='javascript:OpenPuesto(" + vIdPuesto + ")'>" + vPuesto + "</a>";
            }
        }
    }
}