using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Negocio.EvaluacionOrganizacional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using SIGE.Entidades.Externas;
using SIGE.WebApp.Comunes;
using WebApp.Comunes;
using System.Xml.Linq;
using System.Xml;

namespace SIGE.WebApp.EO
{
    public partial class VentanaControlAvance : System.Web.UI.Page
    {

        #region Variables

        private string vNbFirstRadEditorTagName = "p";

        public int vIdPeriodo
        {
            get { return (int)ViewState["vs_vIdPeriodo"]; }
            set { ViewState["vs_vIdPeriodo"] = value; }
        }

        #endregion

        #region Metodos

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

        protected void PintarGrafica()
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            var vControlAance = nClima.ObtieneControlAvance(pID_PERIODO: vIdPeriodo).FirstOrDefault();
            txtRespondidos.Text = vControlAance.NO_CUESTIONARIOS_RESPONDIDOS.ToString();
            txtPorResponder.Text = vControlAance.NO_CUESTIONARIOS_POR_RESPONDER.ToString();
            txtTotalCuestionarios.Text = vControlAance.NO_CUESTIONARIOS_TOTALES.ToString();

            PieSeries vSerie = new PieSeries();
            vSerie.SeriesItems.Add(vControlAance.NO_CUESTIONARIOS_POR_RESPONDER, System.Drawing.Color.Red, "Cuestionarios por contestar.", false, true);
            vSerie.SeriesItems.Add(vControlAance.NO_CUESTIONARIOS_RESPONDIDOS, System.Drawing.Color.Green, "Cuestionarios contestados.", false, true);
            vSerie.LabelsAppearance.Visible = false;
            hcCuestionarios.PlotArea.Series.Add(vSerie);
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
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
                    {
                        txtTipo.InnerText = "Sin asignación de evaluadores";
                        btnVerCuestionario.Visible = false;
                        btnVerCuestionarioConfidencial.Visible = true;
                    }
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
                        lbCuestionario.InnerText = "Copia de otro periodo";
                    if (vPeriodoClima.CL_ORIGEN_CUESTIONARIO == "VACIO")
                        lbCuestionario.InnerText = "Creado desde cero";

                    //int countFiltros = nClima.ObtenerFiltrosEvaluadores(vIdPeriodo).Count;
                    //if (countFiltros > 0)
                    //{
                    //    var vFiltros = nClima.ObtenerParametrosFiltros(vIdPeriodo).FirstOrDefault();
                    //    if (vFiltros != null)
                    //    {
                    //        //LbFiltros.Visible = true;
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

            PintarGrafica();
        }

        protected void grdEvaluadorCuestionarios_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            List<E_EVALUADORES_CLIMA> vlstEvaluadores = nClima.ObtieneEvaluadoresCuestionario(pID_PERIODO: vIdPeriodo).Select(s => new E_EVALUADORES_CLIMA
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