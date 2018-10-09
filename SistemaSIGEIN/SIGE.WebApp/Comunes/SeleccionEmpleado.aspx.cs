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
using SIGE.Negocio.Utilerias;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.AdministracionSitio;

namespace SIGE.WebApp.Comunes
{
    public partial class SeleccionEmpleado : System.Web.UI.Page
    {
        #region Variables
        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private int? vIdRol;
        public string vClIdioma = ContextoApp.clCultureIdioma;

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

        public string vClModulo
        {
            get { return (string)ViewState["vs_vClModulo"]; }
            set { ViewState["vs_vClModulo"] = value; }
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

        private int? vIdPrograma
        {
            get { return (int?)ViewState["vs_vIdPrograma"]; }
            set { ViewState["vs_vIdPrograma"] = value; }
        }

        public string vaddSelection_label
        {
            get { return (string)ViewState["vs_vaddSelection_label"]; }
            set { ViewState["vs_vaddSelection_label"] = value; }
        }

        public string vaddSelection_alert
        {
            get { return (string)ViewState["vs_vaddSelection_alert"]; }
            set { ViewState["vs_vaddSelection_alert"] = value; }
        }

        public XElement vXmlTipoSeleccion
        {
            get { return XElement.Parse((string)(ViewState["vs_vXmlTipoSeleccion"] ?? new XElement("SELECCION").ToString())); }
            set { ViewState["vs_vXmlTipoSeleccion"] = value.ToString(); }
        }
        #endregion

        #region Funciones
        private void DefineGrid()
        {
            bool? vFgActivo = true; ;
            if (Request.QueryString["CL_ORIGEN"] != null)
                if (Request.QueryString["CL_ORIGEN"].ToString() == "REQUISICION")
                {
                    vFgActivo = null;
                    vIdRol = null;
                }
            
            vClTipoSeleccion = Request.QueryString["vClTipoSeleccion"];
            if (string.IsNullOrEmpty(vClTipoSeleccion))
                vClTipoSeleccion = "TODAS";

            XElement vXmlSeleccion = vTipoDeSeleccion(vClTipoSeleccion);
            EmpleadoNegocio nEmpleado = new EmpleadoNegocio();
            List<SPE_OBTIENE_EMPLEADOS_Result> eEmpleados;
            eEmpleados = nEmpleado.ObtenerEmpleados(pXmlSeleccion: vXmlSeleccion, pClUsuario: vClUsuario, pFgActivo: vFgActivo, pID_EMPRESA: vIdEmpresa, pID_ROL: vIdRol); // Se manda el ID ROL como parametro
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
                case "FYD_PROGRAMA":
                    vIdPrograma = int.Parse(Request.QueryString["IdPrograma"]);
                    XElement vXmlIdPrograma = new XElement("TIPO", new XAttribute("ID_PROGRAMA", vIdPrograma));
                    vXmlSeleccion.Element("FILTRO").Add(vXmlIdPrograma);
                    break;
            }
            return vXmlSeleccion;
        }

        //Método de traduccion
        private void TraducirTextos()
        {
             //Asignar texto variables vista
            TraduccionIdiomaTextoNegocio oNegocio = new TraduccionIdiomaTextoNegocio();
            List<SPE_OBTIENE_TRADUCCION_TEXTO_Result> vLstTextosTraduccion = oNegocio.ObtieneTraduccion(pCL_MODULO: "ADM", pCL_PROCESO: "CO_SELECTOREMPLEADO", pCL_IDIOMA: "PORT");
            if (vLstTextosTraduccion.Count > 0)
            {

                //Asignar texto variables javascript
                vaddSelection_label = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vaddSelection_label").FirstOrDefault().DS_TEXTO;
                vaddSelection_alert = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vaddSelection_alert").FirstOrDefault().DS_TEXTO;

                //Asignar texto a botones
                btnAgregar.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnAgregar").FirstOrDefault().DS_TEXTO;
                btnSeleccion.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnSeleccion").FirstOrDefault().DS_TEXTO;
                btnCancelar.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnCancelar").FirstOrDefault().DS_TEXTO;

                //Asignar texto a RadSlidingPane
                slpAdvSearch.Title = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vslpAdvSearch").FirstOrDefault().DS_TEXTO;
            }

        }


        #endregion

        protected void Page_Init(object sender, System.EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;

            if (Request.QueryString["CLFILTRO"] != null)
            { //Este parametro se usa para cuando no se requiere filtrar por el ID_ROL
                if (Request.QueryString["CLFILTRO"] == "NINGUNO")
                {
                    vIdRol = null;
                }
                else
                {
                    vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL; //Se obtiene el id rol del usuario
                }
            }
            else
                vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL; //Se obtiene el id rol del usuario

            DefineGrid();
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
                if (Request.QueryString["vClTipoSeleccion"] != null)
                    vClModulo = Request.QueryString["vClTipoSeleccion"].ToString();
                //    vClCatalogo = Request.QueryString["Catalogo"];
                //if (Request.QueryString["Catalogo"] == "SUPLENTE")
                //    vClCatalogo = "SUPLENTE";

                if (vClIdioma != E_IDIOMA_ENUM.ES.ToString())
                    TraducirTextos();
            }

            if (vClModulo == "MC_PUESTO")
                grdEmpleados.MasterTableView.Columns.FindByUniqueName("M_EMPLEADO_MN_SUELDO").Display = true;
        }

        protected void ftrGrdEmpleados_PreRender(object sender, EventArgs e)
        {
            var menu = ftrGrdEmpleados.FindControl("rfContextMenu") as RadContextMenu;
            menu.DefaultGroupSettings.Height = Unit.Pixel(500);
            menu.EnableAutoScroll = true;
        }

        protected void grdEmpleados_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdEmpleados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdEmpleados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdEmpleados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdEmpleados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdEmpleados.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void grdEmpleados_PreRender(object sender, EventArgs e)
        {
            if (vClIdioma != E_IDIOMA_ENUM.ES.ToString())
            {
                //Asignar texto variables vista
                TraduccionIdiomaTextoNegocio oNegocio = new TraduccionIdiomaTextoNegocio();
                List<SPE_OBTIENE_TRADUCCION_TEXTO_Result> vLstTextosTraduccion = oNegocio.ObtieneTraduccion(pCL_MODULO: "ADM", pCL_PROCESO: "CO_SELECTOREMPLEADO", pCL_IDIOMA: "PORT");
                if (vLstTextosTraduccion.Count > 0)
                {
                    foreach (GridColumn col in grdEmpleados.MasterTableView.Columns)
                    {
                        switch (col.UniqueName)
                        {
                            case "M_EMPLEADO_CL_EMPLEADO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_CL_EMPLEADO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_NB_EMPLEADO_COMPLETO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_NB_EMPLEADO_COMPLETO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_NB_EMPLEADO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_NB_EMPLEADO").FirstOrDefault().DS_TEXTO;
                                break;

                            case "M_EMPLEADO_NB_APELLIDO_PATERNO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_NB_APELLIDO_PATERNO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_NB_APELLIDO_MATERNO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_NB_APELLIDO_MATERNO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_PUESTO_CL_PUESTO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_PUESTO_CL_PUESTO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_PUESTO_NB_PUESTO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_PUESTO_NB_PUESTO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_DEPARTAMENTO_CL_DEPARTAMENTO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_DEPARTAMENTO_CL_DEPARTAMENTO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_DEPARTAMENTO_NB_DEPARTAMENTO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_DEPARTAMENTO_NB_DEPARTAMENTO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_CL_GENERO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_CL_GENERO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_CL_ESTADO_CIVIL":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_CL_ESTADO_CIVIL").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_NB_CONYUGUE":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_NB_CONYUGUE").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_CL_RFC":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_CL_RFC").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_CL_CURP":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_CL_CURP").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_CL_NSS":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_CL_NSS").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_CL_TIPO_SANGUINEO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_CL_TIPO_SANGUINEO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_CL_NACIONALIDAD":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_CL_NACIONALIDAD").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_NB_PAIS":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_NB_PAIS").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_NB_ESTADO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_NB_ESTADO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_NB_MUNICIPIO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_NB_MUNICIPIO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_NB_COLONIA":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_NB_COLONIA").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_NB_CALLE":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_NB_CALLE").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_NO_EXTERIOR":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_NO_EXTERIOR").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_NO_INTERIOR":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_NO_INTERIOR").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_CL_CODIGO_POSTAL":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_CL_CODIGO_POSTAL").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_NB_EMPRESA":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_NB_EMPRESA").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_CL_CORREO_ELECTRONICO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_CL_CORREO_ELECTRONICO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_FE_NACIMIENTO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_FE_NACIMIENTO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_DS_LUGAR_NACIMIENTO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_DS_LUGAR_NACIMIENTO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_FE_ALTA":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_FE_ALTA").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_FE_BAJA":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_FE_BAJA").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_MN_SUELDO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_MN_SUELDO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_MN_SUELDO_VARIABLE":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_MN_SUELDO_VARIABLE").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_DS_SUELDO_COMPOSICION":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_DS_SUELDO_COMPOSICION").FirstOrDefault().DS_TEXTO;
                                break;
                            case "C_EMPRESA_CL_EMPRESA":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_C_EMPRESA_CL_EMPRESA").FirstOrDefault().DS_TEXTO;
                                break;
                            case "C_EMPRESA_NB_EMPRESA":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_C_EMPRESA_NB_EMPRESA").FirstOrDefault().DS_TEXTO;
                                break;
                            case "C_EMPRESA_NB_RAZON_SOCIAL":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_C_EMPRESA_NB_RAZON_SOCIAL").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_CL_ESTADO_EMPLEADO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_CL_ESTADO_EMPLEADO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_EMPLEADO_FG_ACTIVO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEmpleados_M_EMPLEADO_FG_ACTIVO").FirstOrDefault().DS_TEXTO;
                                break;
                        }
                    }
                    grdEmpleados.Rebind();
                }
            }
        }
    }
}