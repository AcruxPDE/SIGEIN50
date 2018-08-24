using SIGE.Entidades;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.FYD
{
    public partial class VentanaPlanSucesionPrincipal : System.Web.UI.Page
    {
        #region Propiedades

        public int vIdEmpleado
        {
            get { return (int)ViewState["vs_ps_id_empleado"]; }
            set { ViewState["vs_ps_id_empleado"] = value; }
        }

        public int vIdPuesto
        {
            get { return (int)ViewState["vs_ps_id_puesto"]; }
            set { ViewState["vs_ps_id_puesto"] = value; }
        }

        public string vXmlEmpleadosAgregados
        {
            get { return (string)ViewState["vs_ps_xml_empleados_agregados"]; }
            set { ViewState["vs_ps_xml_empleados_agregados"] = value; }
        }

        private string vPrioridades
        {
            get { return (string)ViewState["vs_ps_prioridades"]; }
            set { ViewState["vs_ps_prioridades"] = value; }
        }

        private string vXmlPrioridades
        {
            get { return (string)ViewState["vs_ps_xml_prioridades"]; }
            set { ViewState["vs_ps_xml_prioridades"] = value; }
        }

        private List<E_OBTIENE_PLAN_SUCESION> LstSucesores
        {
            get { return (List<E_OBTIENE_PLAN_SUCESION>)ViewState["vs_LstSucesores"]; }
            set { ViewState["vs_LstSucesores"] = value; }
        }

        #endregion

        #region Metodos

        private void CargarEmpleado()
        {
            SPE_OBTIENE_EMPLEADOS_Result vEmpleado = new SPE_OBTIENE_EMPLEADOS_Result();
            PlanSucesionNegocio nPlanSucesion = new PlanSucesionNegocio();

            XElement vXmlEmpleado = new XElement("SELECCION", new XElement("FILTRO", new XAttribute("CL_TIPO", "EMPLEADO"), new XElement("EMP", new XAttribute("ID_EMPLEADO", vIdEmpleado))));

            vEmpleado = nPlanSucesion.ObtieneEmpleados(vXmlEmpleado, true).FirstOrDefault();

            if (vEmpleado != null)
            {
                txtNbEmpleado.InnerText = vEmpleado.M_EMPLEADO_NB_EMPLEADO_COMPLETO;
                txtNbPuesto.InnerText = vEmpleado.M_PUESTO_NB_PUESTO;
                txtNbAntiguedad.InnerText = Antiguedad(vEmpleado.M_EMPLEADO_FE_ALTA);
                vIdPuesto = vEmpleado.M_PUESTO_ID_PUESTO.Value;
                rbiFotoSuceder.DataValue = vEmpleado.FI_FOTOGRAFIA;
            }

        }

        private void CargarPrioridades()
        {
            XElement prioridades = new XElement("PRIORIDADES");
            string[] aux = vPrioridades.Split(',');

            prioridades.Add(new XElement("PRIORIDAD", new XAttribute("ID", 4)));

            foreach (string item in aux)
            {
                prioridades.Add(new XElement("PRIORIDAD", new XAttribute("ID", item)));
            }

            vXmlPrioridades = prioridades.ToString();
        }

        private string Antiguedad(DateTime FechaAlta)
        {
            int años = 0;
            int meses = 0;

            if (DateTime.Now.Year != FechaAlta.Year)
            {
                if (DateTime.Now.Month > FechaAlta.Month & DateTime.Now.Day >= FechaAlta.Day)
                {
                    años = DateTime.Now.Year - FechaAlta.Year;
                }
                else
                {
                    años = (DateTime.Now.Year - FechaAlta.Year) - 1;
                }
            }

            meses = (DateTime.Now.Subtract(FechaAlta).Days / 30) - (años * 12);

            if (años <= 0)
            {
                return meses.ToString() + " Meses";
            }
            else
            {
                return años.ToString() + " Años " + meses.ToString() + " meses";
            }
        }

        private void ProcesarEmpleados(string arrEmpleados)
        {
            XElement prueba;
            string[] aux = arrEmpleados.Split(',');

            if (vXmlEmpleadosAgregados != null)
            {
                prueba = XElement.Parse(vXmlEmpleadosAgregados);

                foreach (string str in aux)
                {
                    var elemento = prueba.Elements().Where(t => t.Attribute("ID").Value == str).FirstOrDefault();

                    if (elemento == null)
                    {
                        prueba.Add(new XElement("EMP", new XAttribute("ID", str)));
                    }
                }
            }
            else
            {
                prueba = new XElement("EMPLEADOS");

                foreach (string item in aux)
                {
                    prueba.Add(new XElement("EMP", new XAttribute("ID", item)));
                }
            }

            vXmlEmpleadosAgregados = prueba.ToString();
        }

        protected string GeneraColor(string pClPotencial)
        {
            string vColor = "black;";

            switch (pClPotencial)
            {
                case "ALTO":
                    vColor = "green";
                    break;
                case "MEDIO":
                    vColor = "gold";
                    break;
                case "BAJO":
                    vColor = "red";
                    break;
                case "OTRO":
                    vColor = "gray";
                    break;
            }

            return vColor;
        }

        private void CargarListaEmpleados()
        {
            PlanSucesionNegocio neg = new PlanSucesionNegocio();
            List<SPE_OBTIENE_PLAN_SUCESION_Result> LstEmpleadosSucesores = new List<SPE_OBTIENE_PLAN_SUCESION_Result>();
            LstSucesores = new List<E_OBTIENE_PLAN_SUCESION>();
            LstEmpleadosSucesores = neg.obtienePlanSucesion(vIdEmpleado, ContextoUsuario.oUsuario.ID_EMPRESA, vXmlPrioridades, vXmlEmpleadosAgregados);

            foreach (var item in LstEmpleadosSucesores)
            {
                E_OBTIENE_PLAN_SUCESION oPs = new E_OBTIENE_PLAN_SUCESION()
                {
                    ID_EMPLEADO = item.ID_EMPLEADO,
                    CL_EMPLEADO = item.CL_EMPLEADO,
                    NB_EMPLEADO = item.NB_EMPLEADO,
                    ID_PUESTO = item.ID_PUESTO,
                    CL_PUESTO = item.CL_PUESTO,
                    NB_PUESTO = item.NB_PUESTO,
                    FE_ALTA = item.FE_ALTA,
                    NB_ANTIGUEDAD = item.NB_ANTIGUEDAD,
                    ID_PERIODO_COMPETENCIAS = item.ID_PERIODO_COMPETENCIAS,
                    FE_INICIO_COMPETENCIAS = item.FE_INICIO_COMPETENCIAS,
                    PR_COMPETENCIAS = item.PR_EVALUACION_COMPETENCIAS,
                    ID_EVALUADO = item.ID_EVALUADO,
                    ID_PERIODO_DESEMPEÑO = item.ID_PERIODO_DESEMPEÑO,
                    FE_INICIO_DESEMPEÑO = item.FE_INICIO_DESEMPEÑO,
                    PR_DESEMPEÑO = item.PR_DESEMPEÑO,
                    CL_TABLERO_CONTROL = item.CL_TABLERO_CONTROL,
                    CL_TIPO_EMPLEADO = item.CL_TIPO_EMPLEADO,
                    CL_POTENCIAL_SUCESOR = item.CL_POTENCIAL_SUCESOR
                };

                LstSucesores.Add(oPs);
            }
        }

        public HtmlGenericControl GeneraLabel(int pIdEmpleado, string pClTd)
        {
            HtmlGenericControl div = new HtmlGenericControl("div");
            var vItem = LstSucesores.Where(w => w.ID_EMPLEADO == pIdEmpleado).FirstOrDefault();

            switch (pClTd)
            {
                case "tdEvalComp":
                    if ((vItem.PR_COMPETENCIAS == "Parcial"))
                        div.InnerHtml = "<label style='padding-left:10px; font-weight:normal;'>Evaluación de competencias:" + "</label>&nbsp<label style='color:red; font-weight:normal;'>" + vItem.PR_COMPETENCIAS + "</label>";
                    else if (vItem.ID_PERIODO_COMPETENCIAS == 0)
                        div.InnerHtml = "<label style='padding-left:10px; font-weight:normal;'>Evaluación de competencias:</label><label style='color:red; font-weight:normal;'>" + "</label>&nbsp<label>" + vItem.PR_COMPETENCIAS + "</label>";
                    else
                        div.InnerHtml = "<label style='padding-left:10px;'><a href='javascript:OpenPlanSucesionWindow(" + vItem.ID_EMPLEADO + ")'><u>Evaluación de competencias</u></a></label>";
                    break;
                case "tdEvalDes":
                    if (vItem.ID_PERIODO_DESEMPEÑO == 0)
                        div.InnerHtml = "<label style='padding-left:10px; font-weight:normal;'>Evaluación de desempeño:&nbsp</label><label style='color:red; font-weight:normal;'>" + vItem.FE_INICIO_DESEMPEÑO + "</label>";
                    else
                        div.InnerHtml = "<label style='padding-left:10px;'><a href='javascript:OpenEvaluacionDesempenoWindow(" + vItem.ID_PERIODO_DESEMPEÑO + "," + vItem.ID_EVALUADO + ")'><u>Evaluación de desempeño:</u></a></label><label style='font-weight:normal;'>&nbsp" + vItem.FE_INICIO_DESEMPEÑO + "</label>&nbsp<label style='font-weight:normal;'>" + vItem.PR_DESEMPEÑO + "</label>";
                    break;
                //case "tdPrograma":
                //    if (vItem.ID_PROGRAMA == -1)
                //        div.InnerHtml = "<label style='padding-left:10px; font-weight:normal;'>Plan de carrera:&nbsp</label><label style='color:red; font-weight:normal;'>" + vItem.NB_PROGRAMA + "</label>";
                //    else
                //        div.InnerHtml = "<label style='padding-left:10px;'> <a href='javascript:OpenAvanceProgramaWindow(" + vItem.ID_PROGRAMA + "," + vItem.ID_EMPLEADO + ")'><u>Plan de carrera:</u></a>&nbsp</label><label style='font-weight:normal;'>" + vItem.NB_PROGRAMA + "</label>";
                //    break;
            }
            return div;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                vIdEmpleado = 0;
                vXmlEmpleadosAgregados = null;

                if (Request.Params["idEmpleado"] != null)
                {
                    vIdEmpleado = int.Parse(Request.Params["idEmpleado"].ToString());
                }

                if (Request.Params["Prioridades"] != null)
                {
                    vPrioridades = Request.Params["Prioridades"].ToString();
                }

                CargarEmpleado();
                CargarPrioridades();
                CargarListaEmpleados();
            }
        }

        protected void rgSucesion_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PlanSucesionNegocio neg = new PlanSucesionNegocio();
            rgSucesion.DataSource = neg.obtienePlanSucesion(vIdEmpleado, ContextoUsuario.oUsuario.ID_EMPRESA, vXmlPrioridades, vXmlEmpleadosAgregados);
        }

        protected void ramAgregarEmpleados_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            string vArrEmpleados = e.Argument;

            ProcesarEmpleados(vArrEmpleados);
            CargarListaEmpleados();
            //vXmlEmpleadosAgregados = vXmlEmpleadosAgregados;

            rgSucesion.Rebind();
        }

        protected void rgSucesion_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                int vIdEmpleado = int.Parse(item.GetDataKeyValue("ID_EMPLEADO").ToString());
                foreach (GridColumn col in rgSucesion.MasterTableView.Columns)
                {
                    string columnname = "";
                    columnname = col.UniqueName;
                    if (columnname == "TC_DATOS_GENERALES")
                    {
                        var vTdCompetencias = item[columnname].FindControl("tdEvalComp");
                        if (vTdCompetencias != null)
                            vTdCompetencias.Controls.Add(GeneraLabel(vIdEmpleado, "tdEvalComp"));
                        var vTdDesempeno = item[columnname].FindControl("tdEvalDes");
                        if (vTdDesempeno != null)
                            vTdDesempeno.Controls.Add(GeneraLabel(vIdEmpleado, "tdEvalDes"));
                        //var vTdPrograma = item[columnname].FindControl("tdPrograma");
                        //if (vTdPrograma != null)
                        //    vTdPrograma.Controls.Add(GeneraLabel(vIdEmpleado, "tdPrograma"));
                    }
                }
            }
        }
    }
}