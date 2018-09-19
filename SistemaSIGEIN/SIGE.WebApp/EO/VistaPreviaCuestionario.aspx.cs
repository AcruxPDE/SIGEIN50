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
using SIGE.WebApp.EO.Cuestionarios;

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

        private List<E_GENERO> vLstGeneros
        {
            get { return (List<E_GENERO>)ViewState["vs_vLstGeneros"]; }
            set { ViewState["vs_vLstGeneros"] = value; }
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


        protected string ObtieneGeneros(string pXmlGenros)
        {
            string vGeneros = "";
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(pXmlGenros);
            XmlNodeList generos = xml.GetElementsByTagName("ITEMS");
            vLstGeneros = new List<E_GENERO>();

            XmlNodeList lista =
            ((XmlElement)generos[0]).GetElementsByTagName("ITEM");

            foreach (XmlElement nodo in lista)
            {

                vGeneros = vGeneros + nodo.GetAttribute("NB_GENERO") + ".\n";
                E_GENERO f = new E_GENERO
                {
                    CL_GENERO = nodo.GetAttribute("CL_GENERO"),
                    NB_GENERO = nodo.GetAttribute("NB_GENERO")
                };
                vLstGeneros.Add(f);
            }


            return vGeneros;
        }

        protected HtmlGenericControl GenerarCuestionario()
        {
            HtmlGenericControl vTabla = new HtmlGenericControl("table");
            vTabla.Attributes.Add("style", "border-collapse: collapse;");


            HtmlGenericControl vCtrlColumn = new HtmlGenericControl("thead");
            vCtrlColumn.Attributes.Add("style", "background: #E6E6E6;");

            HtmlGenericControl vCtrlTh1 = new HtmlGenericControl("td");
            vCtrlTh1.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:50px;");
            vCtrlTh1.Attributes.Add("align", "center");
            vCtrlTh1.InnerText = String.Format("{0}", "#");
            vCtrlColumn.Controls.Add(vCtrlTh1);

            HtmlGenericControl vCtrlTh2 = new HtmlGenericControl("td");
            vCtrlTh2.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:300px;");
            vCtrlTh2.InnerText = String.Format("{0}", "Pregunta");
            vCtrlColumn.Controls.Add(vCtrlTh2);

            HtmlGenericControl vCtrlTh3 = new HtmlGenericControl("td");
            vCtrlTh3.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:110px;");
            vCtrlTh3.Attributes.Add("align", "center");
            vCtrlTh3.InnerText = String.Format("{0}", "Totalmente de acuerdo");
            vCtrlColumn.Controls.Add(vCtrlTh3);

            HtmlGenericControl vCtrlTh4 = new HtmlGenericControl("td");
            vCtrlTh4.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:110px;");
            vCtrlTh4.Attributes.Add("align", "center");
            vCtrlTh4.InnerText = String.Format("{0}", "Casi siempre de acuerdo");
            vCtrlColumn.Controls.Add(vCtrlTh4);

            HtmlGenericControl vCtrlTh5 = new HtmlGenericControl("td");
            vCtrlTh5.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:110px;");
            vCtrlTh5.Attributes.Add("align", "center");
            vCtrlTh5.InnerText = String.Format("{0}", "Casi siempre en desacuerdo");
            vCtrlColumn.Controls.Add(vCtrlTh5);

            HtmlGenericControl vCtrlTh6 = new HtmlGenericControl("td");
            vCtrlTh6.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:110px;");
            vCtrlTh6.Attributes.Add("align", "center");
            vCtrlTh6.InnerText = String.Format("{0}", "Totalmente en desacuerdo");
            vCtrlColumn.Controls.Add(vCtrlTh6);

            vTabla.Controls.Add(vCtrlColumn);

            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            List<E_PREGUNTAS_PERIODO_CLIMA> lstCuestionario = nClima.ObtienePreguntasPeriodo(pID_PERIODO: vIdPeriodo).Select(s => new E_PREGUNTAS_PERIODO_CLIMA
            {
                NB_PREGUNTA = s.NB_PREGUNTA,
                NO_SECUENCIA = s.NO_SECUENCIA,
            }).ToList();

            HtmlGenericControl vCtrlTbody = new HtmlGenericControl("tbody");

            foreach (var item in lstCuestionario)
            {
                HtmlGenericControl vCtrlRow = new HtmlGenericControl("tr");
                vCtrlRow.Attributes.Add("style", "page-break-inside:avoid; page-break-after:auto;");

                HtmlGenericControl vCtrlSecuencia = new HtmlGenericControl("td");
                vCtrlSecuencia.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
                vCtrlSecuencia.Attributes.Add("align", "center");
                vCtrlSecuencia.InnerText = String.Format("{0}", item.NO_SECUENCIA);
                vCtrlRow.Controls.Add(vCtrlSecuencia);

                HtmlGenericControl vCtrlNbPregunta = new HtmlGenericControl("td");
                vCtrlNbPregunta.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
                vCtrlNbPregunta.InnerText = String.Format("{0}", item.NB_PREGUNTA);
                vCtrlRow.Controls.Add(vCtrlNbPregunta);

                HtmlGenericControl vCtrlButton1 = new HtmlGenericControl("td");
                vCtrlButton1.Attributes.Add("style", "border: 1px solid #000000;");
                vCtrlButton1.Attributes.Add("align", "center");
                HtmlInputRadioButton radioButton = new HtmlInputRadioButton();
                vCtrlButton1.Controls.Add(radioButton);
                vCtrlRow.Controls.Add(vCtrlButton1);

                HtmlGenericControl vCtrlButton2 = new HtmlGenericControl("td");
                vCtrlButton2.Attributes.Add("style", "border: 1px solid #000000;");
                vCtrlButton2.Attributes.Add("align", "center");
                HtmlInputRadioButton radioButton2 = new HtmlInputRadioButton();
                vCtrlButton2.Controls.Add(radioButton2);
                vCtrlRow.Controls.Add(vCtrlButton2);

                HtmlGenericControl vCtrlButton3 = new HtmlGenericControl("td");
                vCtrlButton3.Attributes.Add("style", "border: 1px solid #000000;");
                vCtrlButton3.Attributes.Add("align", "center");
                HtmlInputRadioButton radioButton3 = new HtmlInputRadioButton();
                vCtrlButton3.Controls.Add(radioButton3);
                vCtrlRow.Controls.Add(vCtrlButton3);

                HtmlGenericControl vCtrlButton4 = new HtmlGenericControl("td");
                vCtrlButton4.Attributes.Add("style", "border: 1px solid #000000;");
                vCtrlButton4.Attributes.Add("align", "center");
                HtmlInputRadioButton radioButton4 = new HtmlInputRadioButton();
                vCtrlButton4.Controls.Add(radioButton4);
                vCtrlRow.Controls.Add(vCtrlButton4);

                vCtrlTbody.Controls.Add(vCtrlRow);
            }

            vTabla.Controls.Add(vCtrlTbody);

            return vTabla;
        }

        protected HtmlGenericControl GenerarPreguntasAbiertas()
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            List<SPE_OBTIENE_EO_PREGUNTAS_ABIERTAS_PERIODO_Result> vLstPreguntas = nClima.ObtenerPreguntasAbiertas(vIdPeriodo, null).ToList();

            HtmlGenericControl vTabla = new HtmlGenericControl("table");
            if (vLstPreguntas.Count > 0)
            {
                vTabla.Attributes.Add("style", "border-collapse: collapse;");

                HtmlGenericControl vCtrlColumn = new HtmlGenericControl("thead");
                vCtrlColumn.Attributes.Add("style", "background: #E6E6E6;");

                HtmlGenericControl vCtrlTh1 = new HtmlGenericControl("td");
                vCtrlTh1.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:348px;");
                vCtrlTh1.Attributes.Add("align", "justify");
                vCtrlTh1.InnerText = String.Format("{0}", "Pregunta abierta");
                vCtrlColumn.Controls.Add(vCtrlTh1);

                HtmlGenericControl vCtrlTh2 = new HtmlGenericControl("td");
                vCtrlTh2.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:442px;");
                vCtrlTh2.InnerText = String.Format("{0}", "Respuesta");
                vCtrlColumn.Controls.Add(vCtrlTh2);

                vTabla.Controls.Add(vCtrlColumn);

                HtmlGenericControl vCtrlTbody = new HtmlGenericControl("tbody");

                foreach (var item in vLstPreguntas)
                {
                    HtmlGenericControl vCtrlRow = new HtmlGenericControl("tr");
                    vCtrlRow.Attributes.Add("style", "page-break-inside:avoid; page-break-after:auto;");

                    HtmlGenericControl vCtrlSecuencia = new HtmlGenericControl("td");
                    vCtrlRow.Attributes.Add("height","80px;");
                    vCtrlSecuencia.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
                    vCtrlSecuencia.Attributes.Add("align", "justify");
                    vCtrlSecuencia.InnerText = String.Format("{0}", item.NB_PREGUNTA);
                    vCtrlRow.Controls.Add(vCtrlSecuencia);

                    HtmlGenericControl vCtrlNbPregunta = new HtmlGenericControl("td");
                    vCtrlNbPregunta.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
                    vCtrlRow.Controls.Add(vCtrlNbPregunta);

                    vCtrlTbody.Controls.Add(vCtrlRow);
                }

                vTabla.Controls.Add(vCtrlTbody);
            }
            return vTabla;
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

                                    ObtieneGeneros(vFiltros.CL_GENERO);
                                    foreach (E_GENERO item in vLstGeneros)
                                    {
                                        HtmlGenericControl vDiv = new HtmlGenericControl("div");
                                        vDiv.Attributes.Add("class", "ctrlBasico");
                                        var checkbox = new CheckBox();
                                        checkbox.Text = item.NB_GENERO;
                                        vDiv.Controls.Add(checkbox);
                                        dvGeneros.Controls.Add(vDiv);
                                    }
                                    //CheckBox vCheckM = new CheckBox();
                                    //vCheckM.Text = "Masculino";
                                    //CheckBox vCheckF = new CheckBox();
                                    //vCheckF.Text = "Femenino";
                                    //if (vFiltros.CL_GENERO == "Femenino")
                                    //dvGeneros.Controls.Add(vCheckF);
                                    //if (vFiltros.CL_GENERO == "Masculino")
                                    //dvGeneros.Controls.Add(vCheckM);
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
                                    cell.Attributes.Add("style", "width:150px;");
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
                    else
                    {
                        dvMostrar.Visible = false;
                    }


                    dvCuestionario.Controls.Add(GenerarCuestionario());
                    dvPreguntasAbiertas.Controls.Add(GenerarPreguntasAbiertas());
                }
            }
        }

        //protected void rgCuestionario_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        //{
        //    ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
        //    int vCantPreguntas = nClima.ObtienePreguntasPeriodo(pID_PERIODO: vIdPeriodo).Count();
        //    rgCuestionario.Height = (vCantPreguntas + 1) * 72;

        //    List<E_PREGUNTAS_PERIODO_CLIMA> lstCuestionario = nClima.ObtienePreguntasPeriodo(pID_PERIODO: vIdPeriodo).Select(s => new E_PREGUNTAS_PERIODO_CLIMA
        //    {
        //        NB_PREGUNTA = s.NB_PREGUNTA,
        //        NO_SECUENCIA = s.NO_SECUENCIA,
        //    }).ToList();
        //    rgCuestionario.DataSource = lstCuestionario;
        //}

        //protected void rgPreguntasAbiertas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        //{
        //    ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
        //    int vCount = nClima.ObtenerPreguntasAbiertas(vIdPeriodo, null).Count;
        //    rgPreguntasAbiertas.Height = (vCount + 1) * 72;


        //    if (vCount > 0)
        //        rgPreguntasAbiertas.DataSource = nClima.ObtenerPreguntasAbiertas(vIdPeriodo, null).ToList();
        //    else
        //        rgPreguntasAbiertas.Visible = false;
        //}
    }

    [Serializable]
    public class E_DEPARTAMENTOS
    {
        public string ID_DEPARTAMENTO { get; set; }
        public string NB_DEPARTAMENTO { get; set; }
    }
}