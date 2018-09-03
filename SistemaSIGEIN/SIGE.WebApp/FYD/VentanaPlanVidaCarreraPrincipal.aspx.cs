using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Web.UI.HtmlControls;

namespace SIGE.WebApp.FYD
{
    public partial class VentanaPlanVidaCarreraPrincipal : System.Web.UI.Page
    {
        #region propiedades

        private string vClUsuario;

        private string vNbPrograma;

        public int vIdEmpleado
        {
            get { return (int)ViewState["vs_pvc_id_empleado"]; }
            set { ViewState["vs_pvc_id_empleado"] = value; }
        }

        private int vIdPuesto
        {
            get { return (int)ViewState["vs_pvc_id_puesto"]; }
            set { ViewState["vs_pvc_id_puesto"] = value; }
        }

        #endregion

        #region Funciones

        private void cargarListas()
        {
            List<SPE_OBTIENE_PLAN_VIDA_CARRERA_Result> rutaNatural = new List<SPE_OBTIENE_PLAN_VIDA_CARRERA_Result>();
            List<SPE_OBTIENE_PLAN_VIDA_CARRERA_Result> rutaAlternativa = new List<SPE_OBTIENE_PLAN_VIDA_CARRERA_Result>();
            List<SPE_OBTIENE_PLAN_VIDA_CARRERA_Result> rutaHorizontal = new List<SPE_OBTIENE_PLAN_VIDA_CARRERA_Result>();
            List<SPE_OBTIENE_PLAN_VIDA_CARRERA_Result> rutas = new List<SPE_OBTIENE_PLAN_VIDA_CARRERA_Result>();

            PlanVidaCarreraNegocio neg = new PlanVidaCarreraNegocio();

            rutas = neg.obtieneDatosPlanVidaCarrera(vIdPuesto, null);

            rutaNatural = rutas.Where(n => n.CL_TIPO_GENEALOGIA == "NATURAL").ToList();
            generarOrganigrama(rocNatural, rutaNatural, true, "NATURAL");

            rutaAlternativa = rutas.Where(n => n.CL_TIPO_GENEALOGIA == "RUTA ALTERNATIVA").ToList();
            generarOrganigrama(rocAlternativa, rutaAlternativa, false, "ALTER");

            rutaHorizontal = rutas.Where(n => n.CL_TIPO_GENEALOGIA == "RUTA LATERAL").ToList();
            generarOrganigrama(rocHorizontal, rutaHorizontal, false, "HOR");
        }

        private void generarOrganigrama(RadOrgChart control, List<SPE_OBTIENE_PLAN_VIDA_CARRERA_Result> source, bool ordenar, string tipo)
        {
            bool mostrarcheck;
            string css = "";
            int i = 0;


            if (ordenar)
            {
                source = source.OrderByDescending(t => t.NO_NIVEL).ToList();
            }

            if (source[i].ID_PUESTO == vIdPuesto)
            {
                mostrarcheck = false;

                switch (tipo)
                {
                    case "NATURAL":
                        css = "cssNaturalActual";
                        break;
                    case "ALTER":
                        css = "cssAlternativaActual";
                        break;
                    case "HOR":
                        css = "cssHorizontalActual";
                        break;
                }
            }
            else
            {
                mostrarcheck = true;

                switch (tipo)
                {
                    case "NATURAL":
                        css = "cssNatural";
                        break;
                    case "ALTER":
                        css = "cssAlternativa";
                        break;
                    case "HOR":
                        css = "cssHorizontal";
                        break;
                }
            }

            var node = new OrgChartNode();
            node.DataItem = source[i];

            var groupItem = new OrgChartGroupItem() { Template = new SelectionTemplate(source[i].ID_PUESTO, source[i].NB_PUESTO, mostrarcheck, css) };
            groupItem.CssClass = css;

            if (source.Count > 1)
            {
                generarOrganigrama(source, node, i + 1, tipo);
            }

            node.GroupItems.Add(groupItem);
            control.Nodes.Add(node);
        }

        private void generarOrganigrama(List<SPE_OBTIENE_PLAN_VIDA_CARRERA_Result> source, OrgChartNode nodo, int i, string tipo)
        {
            bool mostrarcheck;
            string css = "";

            if (source[i].ID_PUESTO == vIdPuesto)
            {
                mostrarcheck = false;
                switch (tipo)
                {
                    case "NATURAL":
                        css = "cssNaturalActual";
                        break;
                    case "ALTER":
                        css = "cssAlternativaActual";
                        break;
                    case "HOR":
                        css = "cssHorizontalActual";
                        break;
                }
            }
            else
            {
                mostrarcheck = true;
                switch (tipo)
                {
                    case "NATURAL":
                        css = "cssNatural";
                        break;
                    case "ALTER":
                        css = "cssAlternativa";
                        break;
                    case "HOR":
                        css = "cssHorizontal";
                        break;
                }
            }

            var node = new OrgChartNode();
            node.DataItem = source[i];

            var groupItem = new OrgChartGroupItem() { Template = new SelectionTemplate(source[i].ID_PUESTO, source[i].NB_PUESTO, mostrarcheck, css) };
            groupItem.CssClass = css;

            nodo.Nodes.Add(node);
            node.GroupItems.Add(groupItem);

            if (i < source.Count - 1)
            {
                generarOrganigrama(source, node, i + 1, tipo);
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                vIdEmpleado = 0;

                if (Request.Params["idEmpleado"] != null)
                {
                    vIdEmpleado = int.Parse(Request.Params["idEmpleado"].ToString());
                    SPE_OBTIENE_M_EMPLEADO_Result emp = new SPE_OBTIENE_M_EMPLEADO_Result();

                    EmpleadoNegocio neg = new EmpleadoNegocio();

                    emp = neg.ObtenerEmpleado(ID_EMPLEADO: vIdEmpleado).FirstOrDefault();

                    txtClaveEvaluado.InnerText = emp.CL_EMPLEADO;
                    txtClavePuesto.InnerText = emp.CL_PUESTO;
                    txtNombreEvaluado.InnerText = emp.NB_EMPLEADO_COMPLETO;
                    txtNombrePuesto.InnerText = emp.NB_PUESTO;
                    vIdPuesto = emp.ID_PUESTO;

                    cargarListas();
                }
            }
        }

    }

    class SelectionTemplate : ITemplate
    {

        private int _id;
        private string _nombre;
        private bool _mostrarCheck;
        private string _css;

        public SelectionTemplate(int id, string nombre, bool mostrarCheck, string css)
        {
            _id = id;
            _nombre = nombre;
            _mostrarCheck = mostrarCheck;
            _css = css;
        }

        public void InstantiateIn(Control container)
        {
            HtmlGenericControl div = new HtmlGenericControl();
            Label label1 = new Label();

            div.Style.Add("float", "right");
            label1.Text = _nombre;
            container.Controls.Add(label1);

            if (_mostrarCheck)
            {
                HtmlInputCheckBox chk = new HtmlInputCheckBox();
                chk.Attributes.Add("onclick", "abrir(" + _id.ToString() + ",this)");
                div.Controls.Add(chk);
                container.Controls.Add(div);
            }
        }
    }

}