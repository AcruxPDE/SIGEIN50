using SIGE.Entidades;
using SIGE.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Telerik.Web.UI;
using SIGE.Entidades.MetodologiaCompensacion;
using System.Xml.Linq;


namespace SIGE.WebApp.Comunes
{
    public partial class SeleccionEmpleadoPde : System.Web.UI.Page
    {

        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;

        private string vClUser
        {
            get { return (ViewState["vs_mulSel"] != null) ? ViewState["vs_mulSel"].ToString() : String.Empty; }
            set { ViewState["vs_mulSel"] = value; }
        }

        public string vClCatalogo
        {
            get { return (string)ViewState["vs_vClCatalogo"]; }
            set { ViewState["vs_vClCatalogo"] = value; }
        }

        public string vClTipoSeleccion
        {
            get { return (string)ViewState["vs_vClTipoSeleccion"]; }
            set { ViewState["vs_vClTipoSeleccion"] = value; }
        }

        public string vClTipoPuesto
        {
            get { return (string)ViewState["vs_vClTipoPuesto"]; }
            set { ViewState["vs_vClTipoPuesto"] = value; }
        }

        private string vIdTabuladores
        {
            get { return (string)ViewState["vs_vIdTabuladores"]; }
            set { ViewState["vs_vIdTabuladores"] = value; }
        }

        public XElement vXmlTipoSeleccion
        {
            get { return XElement.Parse((string)(ViewState["vs_vXmlTipoSeleccion"] ?? new XElement("SELECCION").ToString())); }
            set { ViewState["vs_vXmlTipoSeleccion"] = value.ToString(); }
        }

        protected void Page_Init(object sender, System.EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
            DefineGrid();
        }

        private void DefineGrid()
        {

            vClTipoSeleccion = Request.QueryString["vClTipoSeleccion"];
            if (string.IsNullOrEmpty(vClTipoSeleccion))
                vClTipoSeleccion = "TODAS";

            XElement vXmlSeleccion = vTipoDeSeleccion(vClTipoSeleccion);
            EmpleadoNegocio nEmpleado = new EmpleadoNegocio();
            List<SPE_OBTIENE_EMPLEADOS_SELECTOR_Result> eEmpleados;
            eEmpleados = nEmpleado.ObtenerEmpleadosSelector(pXmlSeleccion: vXmlSeleccion, pClUsuario: vClUsuario, pFgActivo: true, pID_EMPRESA: ContextoUsuario.oUsuario.ID_EMPRESA);
            CamposAdicionales cad = new CamposAdicionales();
            DataTable tEmpleados = cad.camposAdicionales(eEmpleados, "M_EMPLEADO_XML_CAMPOS_ADICIONALES", grdEmpleados, "M_EMPLEADO");
            grdEmpleados.DataSource = tEmpleados;
        }

        public XElement vTipoDeSeleccion(string pTipoSeleccion)
        {
            XElement vXmlSeleccion = new XElement("SELECCION", new XElement("FILTRO", new XAttribute("CL_TIPO", vClTipoSeleccion)));
            switch (pTipoSeleccion)
            {
                case "TODAS":
                    break;
                case "MC_PUESTO":
                    vClTipoPuesto = Request.QueryString["vClTipoPuesto"];
                    XElement vXmlClTipoPuesto = new XElement("TIPO", new XAttribute("CL_TIPO_PUESTO", vClTipoPuesto));
                    vXmlSeleccion.Element("FILTRO").Add(vXmlClTipoPuesto);
                    break;
                case "MC_TABULADORES":
                    vIdTabuladores = Request.QueryString["IdTabuladores"];
                    string[] split = vIdTabuladores.Split(new Char[] { ',' });
                    List<E_SELECCIONADOS> vSeleccionados = new List<E_SELECCIONADOS>();

                    foreach (string item in split)
                    {
                        vSeleccionados.Add(new E_SELECCIONADOS { ID_SELECCIONADO = int.Parse(item) });
                    }

                    var vXmlIdTabuladores = vSeleccionados.Select(s => new XElement("TIPO", new XAttribute("ID_TABULADOR", s.ID_SELECCIONADO)));
                    vXmlSeleccion.Element("FILTRO").Add(vXmlIdTabuladores);
                    break;
            }
            return vXmlSeleccion;
        }

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {
                grdEmpleados.AllowMultiRowSelection = true;
                if (!String.IsNullOrEmpty(Request.QueryString["mulSel"]))
                {
                    grdEmpleados.AllowMultiRowSelection = (Request.QueryString["mulSel"] == "1");
                    btnAgregar.Visible = (Request.QueryString["mulSel"] == "1");
                }

                vClCatalogo = Request.QueryString["CatalogoCl"];
                if (String.IsNullOrEmpty(vClCatalogo))
                    vClCatalogo = "EMPLEADO";
            }
        }

        protected void ftrGrdEmpleados_PreRender(object sender, EventArgs e)
        {
            var menu = ftrGrdEmpleados.FindControl("rfContextMenu") as RadContextMenu;
            menu.DefaultGroupSettings.Height = Unit.Pixel(500);
            menu.EnableAutoScroll = true;
        }
    }
}