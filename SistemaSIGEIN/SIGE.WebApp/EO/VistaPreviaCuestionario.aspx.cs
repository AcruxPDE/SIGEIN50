using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Negocio.EvaluacionOrganizacional;
using System.Xml;
using SIGE.Entidades;
using System.Web.UI.HtmlControls;

namespace SIGE.WebApp.EO
{
    public partial class VistaPreviaCuestionario : System.Web.UI.Page
    {
        #region Variables

        private int vIdPeriodo
        {
            get { return (int)ViewState["vsIdPeriodo"]; }
            set { ViewState["vsIdPeriodo"] = value; }
        }

        public List<E_CAMPOS_ADICIONALES> vLstCamposAdicionales
        {
            get { return (List<E_CAMPOS_ADICIONALES>)ViewState["vs_vLstCamposAdicionales"]; }
            set { ViewState["vs_vLstCamposAdicionales"] = value; }
        }

        private List<E_DEPARTAMENTOS> vLstDepartamentos
        {
            get { return (List<E_DEPARTAMENTOS>)ViewState["vs_vLstDepartamentos"]; }
            set { ViewState["vs_vLstDepartamentos"] = value; }
        }

        #endregion

        #region Funciones

        protected void ObtieneAdicionales(string pXmlAdicionales)
        {
            int vIdCatalogo;
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(pXmlAdicionales);
            vLstCamposAdicionales = new List<E_CAMPOS_ADICIONALES>();

            XmlNodeList departamentos = xml.GetElementsByTagName("ITEMS");

            XmlNodeList lista =
            ((XmlElement)departamentos[0]).GetElementsByTagName("ITEM");

            foreach (XmlElement nodo in lista)
            {

                vIdCatalogo = int.Parse(nodo.GetAttribute("ID_CATALOGO_LISTA"));

                bool exist = vLstCamposAdicionales.Exists(e => e.ID_CATALOGO_LISTA == vIdCatalogo);
                if (!exist)
                    vLstCamposAdicionales.Add(new E_CAMPOS_ADICIONALES { ID_CATALOGO_LISTA = vIdCatalogo });
            }

        }

        protected string ObtieneDepartamentos(string pXmlDepartamentos)
        {
            string vDepartamentos = "";
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(pXmlDepartamentos);
            XmlNodeList departamentos = xml.GetElementsByTagName("ITEMS");
            vLstDepartamentos = new List<E_DEPARTAMENTOS>();

            XmlNodeList lista =
            ((XmlElement)departamentos[0]).GetElementsByTagName("ITEM");

            foreach (XmlElement nodo in lista)
            {

                vDepartamentos = vDepartamentos + nodo.GetAttribute("NB_DEPARTAMENTO") + ".\n";
                E_DEPARTAMENTOS f = new E_DEPARTAMENTOS
                {
                    ID_DEPARTAMENTO = nodo.GetAttribute("ID_DEPARTAMENTO"),
                    NB_DEPARTAMENTO = nodo.GetAttribute("NB_DEPARTAMENTO")
                };
                vLstDepartamentos.Add(f);
            }


            return vDepartamentos;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["PeriodoId"] != null)
                {
                    vIdPeriodo = int.Parse(Request.QueryString["PeriodoId"]);

                    ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
                    var vClima = nClima.ObtienePeriodosClima(pIdPerido: vIdPeriodo).FirstOrDefault();
                    txtClPeriodo.Value = vClima.CL_PERIODO + " - " + vClima.DS_PERIODO;
                    lbInstrucciones.InnerHtml = vClima.DS_INSTRUCCIONES;
                    int countFiltros = nClima.ObtenerFiltrosEvaluadores(vIdPeriodo).Count;
                    if (countFiltros > 0)
                    {
                        var vFiltros = nClima.ObtenerParametrosFiltros(vIdPeriodo).FirstOrDefault();
                        if (vFiltros != null)
                        {
                            if (vFiltros.EDAD_INICIO != null)
                            {
                                lbedad.Visible = true;
                                txtEdaddes.Visible = true;
                                //txtEdaddes.Value = vFiltros.EDAD_INICIO + " - " + vFiltros.EDAD_FINAL + " años";
                            }
                            if (vFiltros.ANTIGUEDAD_INICIO != null)
                            {
                                lbAntiguedad.Visible = true;
                                txtAntiguedades.Visible = true;
                               // txtAntiguedades.Value = vFiltros.ANTIGUEDAD_INICIO + " - " + vFiltros.ANTIGUEDAD_FINAL + " años";
                            }
                            if (vFiltros.CL_GENERO != null)
                            {
                                lbGenero.Visible = true;
                                CheckBox vCheckM = new CheckBox();
                                vCheckM.Text = "Masculino";
                                CheckBox vCheckF = new CheckBox();
                                vCheckF.Text = "Femenino";
                                dvGeneros.Controls.Add(vCheckF);
                                dvGeneros.Controls.Add(vCheckM);
                            }

                            if (vFiltros.XML_DEPARTAMENTOS != null)
                            {
                                lbDepartamento.Visible = true;
                                ObtieneDepartamentos(vFiltros.XML_DEPARTAMENTOS);
                                foreach (E_DEPARTAMENTOS item in vLstDepartamentos)
                                {
                                    HtmlGenericControl vDiv = new HtmlGenericControl("div");
                                    vDiv.Attributes.Add("class", "ctrlBasico");
                                    var checkbox = new CheckBox();
                                    vDiv.Controls.Add(checkbox);
                                    var label = new Label();
                                    label.Text = item.NB_DEPARTAMENTO;
                                    vDiv.Controls.Add(label);
                                    dvAreas.Controls.Add(vDiv);
                                }
                            }

                            if (vFiltros.XML_CAMPOS_ADICIONALES != null)
                            {

                                RotacionPersonalNegocio negocio = new RotacionPersonalNegocio();
                                ObtieneAdicionales(vFiltros.XML_CAMPOS_ADICIONALES);
                                foreach (E_CAMPOS_ADICIONALES item in vLstCamposAdicionales)
                                {
                                    var ListaAdscripcion = negocio.ObtieneCatalogoAdscripciones(item.ID_CATALOGO_LISTA).FirstOrDefault();
                                    var row = new HtmlTableRow();
                                    var cell = new HtmlTableCell() { InnerHtml = "<label name='adscripcion' name='name' width='100'>" + ListaAdscripcion.NB_CAMPO + ":" + "</ label>" };
                                    cell.Attributes.Add("style", "width:100px;");
                                    row.Cells.Add(cell);
                                    cell = new HtmlTableCell();
                                    cell.Style.Add("Height", "30px");
                                    var ListaAdscripcionValor = negocio.ObtieneCatalogoAdscripciones(item.ID_CATALOGO_LISTA).ToList();
                                    foreach (var itemValor in ListaAdscripcionValor)
                                    {
                                        var checkbox = new CheckBox();
                                        cell.Controls.Add(checkbox);
                                        var label = new Label();
                                        label.Text = itemValor.NB_CATALOGO_VALOR;
                                        cell.Controls.Add(label);
                                    }
                                    row.Cells.Add(cell);
                                    tbAdscripciones.Rows.Add(row);
                                }
                            }

                        }
                    }

                }
            }
        }

        protected void rgCuestionario_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            int vCantPreguntas = nClima.ObtienePreguntasPeriodo(pID_PERIODO: vIdPeriodo).Count();
            rgCuestionario.Height = (vCantPreguntas + 1) * 82;

            List<E_PREGUNTAS_PERIODO_CLIMA> lstCuestionario = nClima.ObtienePreguntasPeriodo(pID_PERIODO: vIdPeriodo).Select(s => new E_PREGUNTAS_PERIODO_CLIMA
            {
                NB_PREGUNTA = s.NB_PREGUNTA,
                NO_SECUENCIA = s.NO_SECUENCIA,
            }).ToList();
            rgCuestionario.DataSource = lstCuestionario;
        }

        protected void rgPreguntasAbiertas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            int vCount = nClima.ObtenerPreguntasAbiertas(vIdPeriodo, null).Count;
            if (vCount > 0)
                rgPreguntasAbiertas.DataSource = nClima.ObtenerPreguntasAbiertas(vIdPeriodo, null).ToList();
            else
                rgPreguntasAbiertas.Visible = false;
        }
    }

    [Serializable]
    public class E_DEPARTAMENTOS
    {
        public string ID_DEPARTAMENTO { get; set; }
        public string NB_DEPARTAMENTO { get; set; }
    }
}