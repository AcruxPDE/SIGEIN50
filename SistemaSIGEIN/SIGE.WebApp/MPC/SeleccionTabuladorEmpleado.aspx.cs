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
using System.Xml.Linq;
using SIGE.Entidades.MetodologiaCompensacion;
using SIGE.Negocio.MetodologiaCompensacion;


namespace SIGE.WebApp.Comunes
{
    public partial class SeleccionTabuladorEmpleado : System.Web.UI.Page
    {

        private int vIdTabulador
        {
            get { return (int)ViewState["vs_vIdTabulador"]; }
            set { ViewState["vs_vIdTabulador"] = value; }
        }

        private string vIdTabuladores
        {
            get { return (string)ViewState["vs_vIdTabuladores"]; }
            set { ViewState["vs_vIdTabuladores"] = value; }
        }

        private string vClTipoSeleccion
        {
            get { return (string)ViewState["vs_vClTipoSeleccion"]; }
            set { ViewState["vs_vClTipoSeleccion"] = value; }
        }

        public string vClCatalogo
        {
            get { return (string)ViewState["vs_vClCatalogo"]; }
            set { ViewState["vs_vClCatalogo"] = value; }
        }

        public XElement vXmlTabuladorEmpleado
        {
            get { return XElement.Parse((string)(ViewState["vs_vXmlTabuladorEmpleado"])); }
            set { ViewState["vs_vXmlTabuladorEmpleado"] = value.ToString(); }
        }

        private int? vIdRol;


        #region Funciones

        private void DefineGrid()
        {

            vClTipoSeleccion = Request.QueryString["vClTipoSeleccion"];
            if (string.IsNullOrEmpty(vClTipoSeleccion))
                vClTipoSeleccion = "TODAS";

            vXmlTabuladorEmpleado = vTipoDeSeleccion(vClTipoSeleccion);
            TabuladoresNegocio nTabuladores = new TabuladoresNegocio();
            var vTabulador = nTabuladores.ObtenieneEmpleadosTabulador(XML_SELECCIONADOS: vXmlTabuladorEmpleado, ID_EMPRESA: ContextoUsuario.oUsuario.ID_EMPRESA, pIdRol: vIdRol);
            grdTabuladorEmpleado.DataSource = vTabulador;

            List<SPE_OBTIENE_EMPLEADOS_TABULADOR_Result> eEmpleados;

            eEmpleados = nTabuladores.ObtenieneEmpleadosTabulador(XML_SELECCIONADOS: vXmlTabuladorEmpleado, ID_EMPRESA: ContextoUsuario.oUsuario.ID_EMPRESA, pIdRol: vIdRol);

            CamposAdicionales cad = new CamposAdicionales();
            DataTable tEmpleados = cad.camposAdicionales(eEmpleados, "M_EMPLEADO_XML_CAMPOS_ADICIONALES", grdTabuladorEmpleado, "M_EMPLEADO");

            grdTabuladorEmpleado.DataSource = tEmpleados;
        }

        public XElement vTipoDeSeleccion(string pTipoSeleccion)
        {
            XElement vXmlSeleccion = new XElement("SELECCION", new XElement("FILTRO", new XAttribute("CL_TIPO", vClTipoSeleccion)));
            switch (pTipoSeleccion)
            {
                case "TODOS":
                    break;
                case "TABULADOR":
                    vIdTabulador = int.Parse(Request.QueryString["IdTabulador"]);
                    XElement vXmlClTipoPuesto = new XElement("TIPO", new XAttribute("ID_TABULADOR", vIdTabulador));
                    vXmlSeleccion.Element("FILTRO").Add(vXmlClTipoPuesto);
                    break;
                case "CONSULTAS":
                    vIdTabulador = int.Parse(Request.QueryString["IdTabulador"]);
                    XElement vXmlClPuesto = new XElement("TIPO", new XAttribute("ID_TABULADOR", vIdTabulador));
                    vXmlSeleccion.Element("FILTRO").Add(vXmlClPuesto);
                    break;
                case "TABULADORES":
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

        #endregion

        protected void Page_Init(object sender, System.EventArgs e)
        {
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;

            DefineGrid();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                vClTipoSeleccion = Request.QueryString["vClTipoSeleccion"];
                if (string.IsNullOrEmpty(vClTipoSeleccion))
                    vClTipoSeleccion = "TODOS";

                vClCatalogo = Request.QueryString["CatalogoCl"];
                if (String.IsNullOrEmpty(vClCatalogo))
                    vClCatalogo = "TABULADOR_EMPLEADO";
            }
        }

        protected void grdTabuladorEmpleado_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //vXmlTabuladorEmpleado = vTipoDeSeleccion(vClTipoSeleccion);
            //TabuladoresNegocio nTabuladores = new TabuladoresNegocio();
            //var vTabulador = nTabuladores.ObtenieneEmpleadosTabulador(XML_SELECCIONADOS: vXmlTabuladorEmpleado, ID_EMPRESA: ContextoUsuario.oUsuario.ID_EMPRESA);
            //grdTabuladorEmpleado.DataSource = vTabulador;
        }

        protected void grdTabuladorEmpleado_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdTabuladorEmpleado.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdTabuladorEmpleado.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdTabuladorEmpleado.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdTabuladorEmpleado.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdTabuladorEmpleado.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            } 
        }
    }
}