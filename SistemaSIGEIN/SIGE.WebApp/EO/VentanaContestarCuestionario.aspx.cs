using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;
using WebApp.Comunes;
using System.Xml;

namespace SIGE.WebApp.EO
{
    public partial class VentanaContestarCuestionario : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private string vNbFirstRadEditorTagName = "p";
        private int? vIdRol;

        public int vIdPeriodo
        {
            get { return (int)ViewState["vs_vIdPeriodo"]; }
            set { ViewState["vs_vIdPeriodo"] = value; }
        }

        #endregion

        #region Funciones

        private static Boolean ValidarRamaXml(XElement parentEl, string elementsName)
        {
            var foundEl = parentEl.Element(elementsName);
            if (foundEl != null)
            {
                return true;
            }

            return false;
        }

        protected string ObtieneDepartamentos(string pXmlDepartamentos)
        {
            string vDepartamentos = "";
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(pXmlDepartamentos);
            XmlNodeList departamentos = xml.GetElementsByTagName("ITEMS");

            XmlNodeList lista =
            ((XmlElement)departamentos[0]).GetElementsByTagName("ITEM");

            foreach (XmlElement nodo in lista)
            {

                vDepartamentos = vDepartamentos + nodo.GetAttribute("NB_DEPARTAMENTO") + ".\n";

            }


            return vDepartamentos;
        }

        protected string ObtieneAdicionales(string pXmlAdicionales)
        {
            string vAdicionales = "";
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(pXmlAdicionales);
            XmlNodeList departamentos = xml.GetElementsByTagName("ITEMS");

            XmlNodeList lista =
            ((XmlElement)departamentos[0]).GetElementsByTagName("ITEM");

            foreach (XmlElement nodo in lista)
            {

                vAdicionales = vAdicionales + nodo.GetAttribute("NB_CAMPO") + ".\n";

            }


            return vAdicionales;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;

            if (!IsPostBack)
            {
                if (Request.Params["PeriodoID"] != null)
                {
                    vIdPeriodo = int.Parse(Request.Params["PeriodoID"]);
                    ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
                    var vPeriodoClima = nClima.ObtienePeriodosClima(pIdPerido: vIdPeriodo).FirstOrDefault();
                    txtClPeriodo.InnerText = vPeriodoClima.CL_PERIODO;
                    txtDsPeriodo.InnerText = vPeriodoClima.DS_PERIODO;
                    txtEstatus.InnerText = vPeriodoClima.CL_ESTADO_PERIODO;
                    if (vPeriodoClima.CL_TIPO_CONFIGURACION == "PARAMETROS")
                        txtTipo.InnerText = "Sin asignación de evaluadores";
                    else
                        txtTipo.InnerText = "Evaluadores asignados";

                    if (vPeriodoClima.DS_NOTAS != null)
                    {
                        if (vPeriodoClima.DS_NOTAS.Contains("DS_NOTA"))
                        {
                            txtNotas.InnerHtml = Utileria.MostrarNotas(vPeriodoClima.DS_NOTAS);
                        }
                        else
                        {
                            XElement vRequerimientos = XElement.Parse(vPeriodoClima.DS_NOTAS);
                            if (vRequerimientos != null)
                            {
                                vRequerimientos.Name = vNbFirstRadEditorTagName;
                                txtNotas.InnerHtml = vRequerimientos.ToString();
                            }
                        }
                    }
                    if (vPeriodoClima.CL_ORIGEN_CUESTIONARIO == "PREDEFINIDO")
                        lbCuestionario.InnerText = "Predefinido de SIGEIN";
                    if (vPeriodoClima.CL_ORIGEN_CUESTIONARIO == "COPIA")
                        lbCuestionario.InnerText = "Copia de otro período";
                    if (vPeriodoClima.CL_ORIGEN_CUESTIONARIO == "VACIO")
                        lbCuestionario.InnerText = "Creado en blanco";
                    
                    //int countFiltros = nClima.ObtenerFiltrosEvaluadores(vIdPeriodo).Count;

                    if (vPeriodoClima.CL_TIPO_CONFIGURACION == "PARAMETROS")
                    {
                        btnContestarCuestionarios.Visible = false;
                        btnContestarConfidencial.Visible = true;
                        RadSlidingZone3.Visible = true;
                    }
                    //    var vFiltros = nClima.ObtenerParametrosFiltros(vIdPeriodo).FirstOrDefault();
                    //    if (vFiltros != null)
                    //    {
                    //        // LbFiltros.Visible = true;
                    //        if (vFiltros.EDAD_INICIO != null)
                    //        {
                    //            lbedad.Visible = true;
                    //            txtEdad.Visible = true;
                    //            txtEdad.Attributes.Add("class", "ctrlTableDataBorderContext");
                    //            txtEdad.InnerText = vFiltros.EDAD_INICIO + " a " + vFiltros.EDAD_FINAL + " años";
                    //        }
                    //        if (vFiltros.ANTIGUEDAD_INICIO != null)
                    //        {
                    //            lbAntiguedad.Visible = true;
                    //            txtAntiguedad.Visible = true;
                    //            txtAntiguedad.Attributes.Add("class", "ctrlTableDataBorderContext");
                    //            txtAntiguedad.InnerText = vFiltros.ANTIGUEDAD_INICIO + " a " + vFiltros.ANTIGUEDAD_FINAL + " años";
                    //        }
                    //        if (vFiltros.CL_GENERO != null)
                    //        {
                    //            lbGenero.Visible = true;
                    //            txtGenero.Visible = true;
                    //            txtGenero.Attributes.Add("class", "ctrlTableDataBorderContext");
                    //            if (vFiltros.CL_GENERO == "M")
                    //                txtGenero.InnerText = "Masculino";
                    //            else
                    //                txtGenero.InnerText = "Femenino";
                    //        }

                    //        if (vFiltros.XML_DEPARTAMENTOS != null)
                    //        {
                    //            lbDepartamento.Visible = true;
                    //            rlDepartamento.Visible = true;
                    //            rlDepartamento.Attributes.Add("class", "ctrlTableDataBorderContext");
                    //            rlDepartamento.Text = ObtieneDepartamentos(vFiltros.XML_DEPARTAMENTOS);
                    //        }

                    //        if (vFiltros.XML_CAMPOS_ADICIONALES != null)
                    //        {
                    //            lbAdscripciones.Visible = true;
                    //            rlAdicionales.Visible = true;
                    //            rlAdicionales.Attributes.Add("class", "ctrlTableDataBorderContext");
                    //            rlAdicionales.Text = ObtieneAdicionales(vFiltros.XML_CAMPOS_ADICIONALES);
                    //        }

                    //    }
                    //}
                }
            }
        }

        protected void grdEvaluadorCuestionarios_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            List<E_EVALUADORES_CLIMA> vlstEvaluadores = nClima.ObtieneEvaluadoresCuestionario(pID_PERIODO: vIdPeriodo, pIdRol: vIdRol).Select(s => new E_EVALUADORES_CLIMA
            {
                CL_CORREO_ELECTRONICO = s.CL_CORREO_ELECTRONICO,
                CL_EMPLEADO = s.CL_EMPLEADO,
                CL_TIPO_EVALUADOR = s.CL_TIPO_EVALUADOR,
                ID_EMPLEADO = s.ID_EMPLEADO,
                NB_EVALUADOR = s.NB_EVALUADOR,
                NB_PUESTO = s.NB_PUESTO,
                CL_TOKEN = s.CL_TOKEN,
                FG_CONTESTADO = s.FG_CONTESTADO,
                NB_CONTESTADO = s.FG_CONTESTADO == true ? "Si" : "No",
                ID_EVALUADOR = s.ID_EVALUADOR
            }).ToList();
            grdEvaluadorCuestionarios.DataSource = vlstEvaluadores;
        }

        protected void ramCuestinariosClima_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            grdEvaluadorCuestionarios.Rebind();
        }

        protected void grdEvaluadorCuestionarios_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdEvaluadorCuestionarios.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdEvaluadorCuestionarios.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdEvaluadorCuestionarios.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdEvaluadorCuestionarios.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdEvaluadorCuestionarios.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}