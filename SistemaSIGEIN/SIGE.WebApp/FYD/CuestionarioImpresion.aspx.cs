using SIGE.Entidades.Externas;
using System;
using System.Collections.Generic;
using System.Web.UI;
using SIGE.Entidades;
using SIGE.Negocio.FormacionDesarrollo;
using System.Xml.Linq;
using Telerik.Web.UI;

using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using SIGE.WebApp.Comunes;
using WebApp.Comunes;
using SIGE.Negocio.Utilerias;
using System.Xml;

namespace SIGE.WebApp.FYD
{
    public partial class CuestionarioImpresion : System.Web.UI.Page
    {
        #region Variables

        public string cssModulo = String.Empty;
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int pIdEvaluador
        {
            get { return (int)ViewState["vs_pIdEvaluador"]; }
            set { ViewState["vs_pIdEvaluador"] = value; }
        }

        public int pIdEvaluadoEvaluador
        {
            get { return (int)ViewState["vs_pIdEvaluadoEvaluador"]; }
            set { ViewState["vs_pIdEvaluadoEvaluador"] = value; }
        }

        private int vIdEvaluado
        {
            get { return (int)ViewState["vs_id_evaluado"]; }
            set { ViewState["vs_id_evaluado"] = value; }
        }

        private int vIdPeriodo
        {
            get { return (int)ViewState["vs_id_periodo"]; }
            set { ViewState["vs_id_periodo"] = value; }
        }

        private List<E_PREGUNTAS> pListPeguntas
        {
            get { return (List<E_PREGUNTAS>)ViewState["vs_pListPeguntas"]; }
            set { ViewState["vs_pListPeguntas"] = value; }
        }

        private int vIdEvaluadoEvaluador
        {
            get { return (int)Session["ss_eval_id_evaluado_evaluador"]; }
            set { Session["ss_eval_id_evaluado_evaluador"] = value; }
        }

        private string vXmlCamposAdicionales
        {
            get { return (string)Session["ss_eval_xml_campos_adicionales"]; }
            set { Session["ss_eval_xml_campos_adicionales"] = value; }
        }

        private string vXmlCamposAdicionalesCatalogos
        {
            get { return (string)Session["ss_eval_xml_campos_adicionales_catalogos"]; }
            set { Session["ss_eval_xml_campos_adicionales_catalogos"] = value; }
        }

        private List<E_CLASIFICACION> pListClasificacion
        {
            get { return (List<E_CLASIFICACION>)ViewState["vs_pListClasificacion"]; }
            set { ViewState["vs_pListClasificacion"] = value; }
        }

        PeriodoNegocio negocio = new PeriodoNegocio();

        private int pOrden
        {
            get { return (int)ViewState["vs_pOrden"]; }
            set { ViewState["vs_pOrden"] = value; }
        }

        private string pClas
        {
            get { return (string)ViewState["vs_pClas"]; }
            set { ViewState["vs_pClas"] = value; }
        }

        private string vClColor
        {
            get { return (string)ViewState["vs_v_cl_color"]; }
            set { ViewState["vs_v_cl_color"] = value; }
        }

        private bool vTieneCuestionarios
        {
            get { return (bool)ViewState["vs_v_tieneCuestionarios"]; }
            set { ViewState["vs_v_tieneCuestionarios"] = value; }
        }

        private bool? vValidaCuestionario
        {
            get { return (bool?)ViewState["vs_vValidaCuestionario"]; }
            set { ViewState["vs_vValidaCuestionario"] = value; }
        }

        #endregion

        #region Funciones

        protected void CrearFormulario(XElement pXmlPlantilla)
        {
            foreach (XElement vXmlControl in pXmlPlantilla.Elements("CAMPO"))
            {
                int vDefaultWidth = 200;
                int vDefaultLabelWidth = 150;

                vXmlControl.Add(new XAttribute("FG_HABILITADO", "true"));


                HtmlGenericControl vControlHTML;
                ControlDinamico vControl = new ControlDinamico(vXmlControl, true, vDefaultWidth, vDefaultLabelWidth);

                if (vControl.CtrlControl != null)
                {
                    vControlHTML = new HtmlGenericControl("div");
                    vControlHTML.Attributes.Add("class", "ctrlBasico");

                    if (vControl.CtrlLabel != null)
                    {
                        ((HtmlGenericControl)vControl.CtrlLabel).Style.Add("display", "inline-block");
                        ((HtmlGenericControl)vControl.CtrlLabel).Style.Add("padding-right", "10px");
                        ((HtmlGenericControl)vControl.CtrlLabel).Style.Add("text-align", "right");
                        ((HtmlGenericControl)vControl.CtrlLabel).Style.Add("width", "200px");

                        vControlHTML.Controls.Add(vControl.CtrlLabel);
                    }

                    vControlHTML.Controls.Add(vControl.CtrlControl);
                    divCamposExtras.Controls.Add(new LiteralControl("<div style='clear:both;'></div>"));
                    divCamposExtras.Controls.Add(vControlHTML);
                }
            }
        }

        private XElement generalXmlAdicionales()
        {

            if (vXmlCamposAdicionales != null)
            {
                XElement pXmlAdicionales = XElement.Parse(vXmlCamposAdicionales);

                foreach (XElement vXmlControl in pXmlAdicionales.Elements("CAMPO"))
                {
                    string vClTipoControl = vXmlControl.Attribute("CL_TIPO").Value;
                    string vIdControl = vXmlControl.Attribute("ID_CAMPO").Value;
                    string vNbControl = vXmlControl.Attribute("NB_CAMPO").Value;
                    string vNbValor = String.Empty;
                    string vDsValor = "";

                    bool vFgAsignarValor = true;
                    Control vControl = divCamposExtras.FindControl(vIdControl);

                    switch (vClTipoControl)
                    {
                        case "TEXTBOX":
                            vNbValor = ((RadTextBox)vControl).Text;
                            vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);

                            if (vFgAsignarValor)
                                UtilXML.AsignarValorAtributo(vXmlControl, "NB_VALOR", vNbValor);

                            break;

                        case "MASKBOX":
                            vNbValor = ((RadMaskedTextBox)vControl).Text;
                            vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);

                            if (vFgAsignarValor)
                                UtilXML.AsignarValorAtributo(vXmlControl, "NB_VALOR", vNbValor);

                            break;

                        case "DATEPICKER":
                            DateTime vFecha = ((RadDatePicker)vControl).SelectedDate ?? DateTime.Now;

                            vNbValor = vFecha.ToString("dd/MM/yyyy");
                            vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);

                            if (vFgAsignarValor)
                                UtilXML.AsignarValorAtributo(vXmlControl, "NB_VALOR", vNbValor);

                            break;

                        case "COMBOBOX":
                            vNbValor = ((RadComboBox)vControl).SelectedValue;

                            vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);

                            if (vFgAsignarValor)
                                UtilXML.AsignarValorAtributo(vXmlControl, "NB_VALOR", vNbValor);

                            break;

                        case "LISTBOX":
                            RadListBox vRadListBox = ((RadListBox)vControl);
                            string vClValor = String.Empty;

                            foreach (RadListBoxItem item in vRadListBox.SelectedItems)
                            {
                                vNbValor = item.Value;
                                vDsValor = item.Text;
                            }

                            vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);
                            if (vFgAsignarValor)
                            {
                                UtilXML.AsignarValorAtributo(vXmlControl, "NB_VALOR", vNbValor);
                                UtilXML.AsignarValorAtributo(vXmlControl, "DS_VALOR", vDsValor);
                            }

                            break;
                        case "NUMERICBOX":
                            vNbValor = ((RadNumericTextBox)vControl).Text;
                            vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);

                            if (vFgAsignarValor)
                                UtilXML.AsignarValorAtributo(vXmlControl, "NB_VALOR", vNbValor);
                            break;
                        case "CHECKBOX":
                            vNbValor = (((RadButton)vControl).Checked) ? "1" : "0";
                            UtilXML.AsignarValorAtributo(vXmlControl, "NB_VALOR", vNbValor);
                            break;
                        case "DATEAGE":
                            RadDatePicker vControlF = new RadDatePicker();
                            vControlF = (RadDatePicker)vControl;
                            string value = vControlF.DateInput.InvalidTextBoxValue;

                            if (value == string.Empty)
                            {
                                vFecha = ((RadDatePicker)vControl).SelectedDate ?? DateTime.Now;

                                if (vControlF.SelectedDate < vControlF.MinDate)
                                {
                                    vFecha = DateTime.Now;
                                }
                                vNbValor = vFecha.ToString("dd/MM/yyyy");
                                vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);
                                if (vFgAsignarValor)
                                    UtilXML.AsignarValorAtributo(vXmlControl, "NB_VALOR", vNbValor);
                            }


                            break;
                    }
                }

                return pXmlAdicionales;
            }
            else
            {
                return null;
            }
        }

        private string GeneraValor(decimal? pValorRespuesta, int pValorControl)
        {
            string vCtrCheck = "";

            if (pValorRespuesta != null)
            {
                if (pValorRespuesta == pValorControl)
                {
                    vCtrCheck = "checked='checked'";
                }
            }

            return vCtrCheck;
        }

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();

            vIdEvaluadoEvaluador = int.Parse(Request.Params["ID_EVALUADO_EVALUADOR"].ToString());

            var oCuestionario = nPeriodo.ObtenerCuestionarioEvaluacion(pIdEvaluadoEvaluador: vIdEvaluadoEvaluador);

            if (oCuestionario != null)
            {
                vTieneCuestionarios = true;
                if (oCuestionario.XML_CATALOGOS != null)
                {
                    vXmlCamposAdicionales = oCuestionario.XML_PREGUNTAS_ADICIONALES;
                    vXmlCamposAdicionalesCatalogos = oCuestionario.XML_PREGUNTAS_CATALOGOS_ADICIONALES;
                    CrearFormulario(XElement.Parse(oCuestionario.XML_PREGUNTAS_CATALOGOS_ADICIONALES));
                }
                else if (oCuestionario.XML_PREGUNTAS_ADICIONALES != null)
                {

                    vXmlCamposAdicionales = oCuestionario.XML_PREGUNTAS_ADICIONALES;
                    vXmlCamposAdicionalesCatalogos = oCuestionario.XML_PREGUNTAS_CATALOGOS_ADICIONALES;

                    CrearFormulario(XElement.Parse(oCuestionario.XML_PREGUNTAS_ADICIONALES));
                }
                else
                {
                    vXmlCamposAdicionales = null;
                    vXmlCamposAdicionalesCatalogos = null;
                }

            }
            else
            {
                vTieneCuestionarios = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = (ContextoUsuario.oUsuario != null) ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO";

            string vClModulo = "FORMACION";
            string vModulo = Request.QueryString["m"];
            if (vModulo != null)
                vClModulo = vModulo;

            cssModulo = Utileria.ObtenerCssModulo(vClModulo);

            if (!Page.IsPostBack)
            {
                if (Request.Params["ID_EVALUADOR"] != null)
                {
                    SPE_OBTIENE_FYD_PERIODO_EVALUADOR_Result periodo = new SPE_OBTIENE_FYD_PERIODO_EVALUADOR_Result();
                    SPE_OBTIENE_FYD_EVALUADO_Result evaluado = new SPE_OBTIENE_FYD_EVALUADO_Result();
                    List<SPE_OBTIENE_FYD_PREGUNTAS_EVALUACION_Result> preguntas = new List<SPE_OBTIENE_FYD_PREGUNTAS_EVALUACION_Result>();

                    pIdEvaluador = int.Parse((Request.QueryString["ID_EVALUADOR"]));
                    pIdEvaluadoEvaluador = int.Parse((Request.QueryString["ID_EVALUADO_EVALUADOR"]));

                    periodo = negocio.ObtienePeriodoEvaluador(pIdEvaluador);
                    evaluado = negocio.ObtieneEvaluado(pIdEvaluadoEvaluador);
                    preguntas = negocio.ObtienePreguntas(pIdEvaluadoEvaluador);

                    pListPeguntas = (from c in preguntas
                                     select new E_PREGUNTAS
                                     {
                                         CL_CLASIFICACION = c.CL_CLASIFICACION,
                                         CL_COLOR = c.CL_COLOR,
                                         NB_CLASIFICACION_COMPETENCIA = c.NB_CLASIFICACION_COMPETENCIA,
                                         DS_CLASIFICACION_COMPETENCIA = c.DS_CLASIFICACION_COMPETENCIA,
                                         NO_ORDEN = c.NO_ORDEN,
                                         ID_CUESTIONARIO_PREGUNTA = c.ID_CUESTIONARIO_PREGUNTA,
                                         NB_PREGUNTA = c.NB_PREGUNTA,
                                         DS_PREGUNTA = c.DS_PREGUNTA,
                                         NB_RESPUESTA = c.NB_RESPUESTA,
                                         NO_VALOR_RESPUESTA = c.NO_VALOR_RESPUESTA,
                                         DS_NIVEL0 = c.DS_NIVEL0,
                                         DS_NIVEL1 = c.DS_NIVEL1,
                                         DS_NIVEL2 = c.DS_NIVEL2,
                                         DS_NIVEL3 = c.DS_NIVEL3,
                                         DS_NIVEL4 = c.DS_NIVEL4,
                                         DS_NIVEL5 = c.DS_NIVEL5
                                     }).ToList();

                    pListClasificacion = (from c in pListPeguntas
                                          group c by new { c.CL_CLASIFICACION, c.CL_COLOR, c.NO_ORDEN } into grp
                                          orderby grp.Key.NO_ORDEN ascending
                                          select new E_CLASIFICACION
                                          {
                                              CL_CLASIFICACION = grp.Key.CL_CLASIFICACION,
                                              CL_COLOR = grp.Key.CL_COLOR,
                                              NO_ORDEN = grp.Key.NO_ORDEN

                                          }).ToList();

                    if (periodo != null)
                    {
                        vIdEvaluado = evaluado.ID_EVALUADO;
                        vIdPeriodo = periodo.ID_PERIODO;

                        txtNoPeriodo.InnerText = periodo.CL_PERIODO;
                        txtNbPeriodo.InnerText = periodo.DS_PERIODO;
                        txtEvaluador.InnerText = periodo.NB_EVALUADOR;

                        txtNombreEvaluado.InnerText = evaluado.NB_EVALUADO;
                        txtPuestoEvaluado.InnerText = evaluado.NB_PUESTO;
                        txtTipo.InnerText = evaluado.CL_ROL_EVALUADOR;
                    }

                }
                dvTabla.Controls.Add(generaHtml());
                divCamposExtras.Controls.Add(generaCamposHtml());
            }
        }

        public HtmlGenericControl generaHtml()
        {
            HtmlGenericControl vCtrlTabla = new HtmlGenericControl("table");

            HtmlGenericControl vCtrlRowColumn = new HtmlGenericControl("tr");
            vCtrlRowColumn.Attributes.Add("style", "border: 1px solid gray;");

            HtmlGenericControl vCtrlTh = new HtmlGenericControl("th");
            vCtrlTh.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; background-color:#F5F5F5; height: 50px; padding: 3px; border-top-left-radius:4px");
            vCtrlTh.InnerText = String.Format("{0}", "Color");
            vCtrlRowColumn.Controls.Add(vCtrlTh);

            HtmlGenericControl vCtrlTh2 = new HtmlGenericControl("th");
            vCtrlTh2.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; background-color:#F5F5F5; padding: 3px; height: 50px;");
            vCtrlTh2.InnerText = String.Format("{0}", "Competencia");
            vCtrlRowColumn.Controls.Add(vCtrlTh2);

            HtmlGenericControl vCtrlTh3 = new HtmlGenericControl("th");
            vCtrlTh3.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; text-align:center; background-color:#F5F5F5; padding: 3px; height: 50px;");
            vCtrlTh3.InnerHtml = String.Format("{0}", "0 <br /> 0%");
            vCtrlRowColumn.Controls.Add(vCtrlTh3);

            HtmlGenericControl vCtrlTh3b = new HtmlGenericControl("th");
            vCtrlTh3b.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; text-align:center; background-color:#F5F5F5; padding: 3px; height: 50px;");
            vCtrlTh3b.InnerHtml = String.Format("{0}", "1 <br /> 20%");
            vCtrlRowColumn.Controls.Add(vCtrlTh3b);

            HtmlGenericControl vCtrlTh3c = new HtmlGenericControl("th");
            vCtrlTh3c.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; text-align:center; background-color:#F5F5F5; height: 50px; padding: 3px; ");
            vCtrlTh3c.InnerHtml = String.Format("{0}", "2 <br /> 40%");
            vCtrlRowColumn.Controls.Add(vCtrlTh3c);

            HtmlGenericControl vCtrlTh3d = new HtmlGenericControl("th");
            vCtrlTh3d.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; text-align:center; background-color:#F5F5F5; height: 50px; padding: 3px; ");
            vCtrlTh3d.InnerHtml = String.Format("{0}", "3 <br /> 60%");
            vCtrlRowColumn.Controls.Add(vCtrlTh3d);

            HtmlGenericControl vCtrlTh3e = new HtmlGenericControl("th");
            vCtrlTh3e.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; text-align:center; background-color:#F5F5F5; height: 50px; padding: 3px; ");
            vCtrlTh3e.InnerHtml = String.Format("{0}", "4 <br /> 80%");
            vCtrlRowColumn.Controls.Add(vCtrlTh3e);

            HtmlGenericControl vCtrlTh3f = new HtmlGenericControl("th");
            vCtrlTh3f.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; text-align:center;  background-color:#F5F5F5; height: 50px; padding: 3px; ");
            vCtrlTh3f.InnerHtml = String.Format("{0}", "5 <br /> 100%");
            vCtrlRowColumn.Controls.Add(vCtrlTh3f);

            HtmlGenericControl vCtrlTh4 = new HtmlGenericControl("th");
            vCtrlTh4.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; background-color:#F5F5F5; height: 50px; padding: 3px; ");
            vCtrlTh4.InnerHtml = String.Format("{0}", "Descripción de la competencia");
            vCtrlRowColumn.Controls.Add(vCtrlTh4);

            vCtrlTabla.Controls.Add(vCtrlRowColumn);

            foreach (var item in pListPeguntas)
            {
                HtmlGenericControl vCtrlRow = new HtmlGenericControl("tr");

                HtmlGenericControl vCtrlColumnaClColor = new HtmlGenericControl("td");
                vCtrlColumnaClColor.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; text-align:center; padding: 3px; border-radius:2px");
                vCtrlColumnaClColor.InnerHtml = "<div style='height: 60px; width: 30px; float: left; background: " + item.CL_COLOR + "; border-radius: 5px;'></div>";
                vCtrlRow.Controls.Add(vCtrlColumnaClColor);

                HtmlGenericControl vCtrlColumnaNbPregunta= new HtmlGenericControl("td");
                vCtrlColumnaNbPregunta.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; padding: 3px; border-radius:2px");
                vCtrlColumnaNbPregunta.InnerHtml = String.Format("{0}", item.NB_PREGUNTA);
                vCtrlRow.Controls.Add(vCtrlColumnaNbPregunta);

                HtmlGenericControl vCtrlCheck0 = new HtmlGenericControl("td");
                vCtrlCheck0.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; padding: 3px; border-radius:2px");
                vCtrlCheck0.InnerHtml = "<input type='radio' id='radioButton' " + GeneraValor(item.NO_VALOR_RESPUESTA, 0) + ">";
                vCtrlRow.Controls.Add(vCtrlCheck0);

                HtmlGenericControl vCtrlCheck1 = new HtmlGenericControl("td");
                vCtrlCheck1.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; padding: 3px; border-radius:2px");
                vCtrlCheck1.InnerHtml = "<input type='radio' id='radioButton' " + GeneraValor(item.NO_VALOR_RESPUESTA, 1) + ">";
                vCtrlRow.Controls.Add(vCtrlCheck1);

                HtmlGenericControl vCtrlCheck2 = new HtmlGenericControl("td");
                vCtrlCheck2.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; padding: 3px; border-radius:2px");
                vCtrlCheck2.InnerHtml = "<input type='radio' id='radioButton' " + GeneraValor(item.NO_VALOR_RESPUESTA, 2) + ">";
                vCtrlRow.Controls.Add(vCtrlCheck2);

                HtmlGenericControl vCtrlCheck3 = new HtmlGenericControl("td");
                vCtrlCheck3.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; padding: 3px; border-radius:2px");
                vCtrlCheck3.InnerHtml = "<input type='radio' id='radioButton' " + GeneraValor(item.NO_VALOR_RESPUESTA, 3) + ">";
                vCtrlRow.Controls.Add(vCtrlCheck3);

                HtmlGenericControl vCtrlCheck4 = new HtmlGenericControl("td");
                vCtrlCheck4.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; padding: 3px; border-radius:2px");
                vCtrlCheck4.InnerHtml = "<input type='radio' id='radioButton' " + GeneraValor(item.NO_VALOR_RESPUESTA, 4) + ">";
                vCtrlRow.Controls.Add(vCtrlCheck4);

                HtmlGenericControl vCtrlCheck5 = new HtmlGenericControl("td");
                vCtrlCheck5.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; padding: 3px; border-radius:2px");
                vCtrlCheck5.InnerHtml = "<input type='radio' id='radioButton' " + GeneraValor(item.NO_VALOR_RESPUESTA, 5) + ">";
                vCtrlRow.Controls.Add(vCtrlCheck5);

                HtmlGenericControl vCtrlDsPregunta = new HtmlGenericControl("td");
                vCtrlDsPregunta.Attributes.Add("style", "border: 1px solid gray; font-family:arial; font-size: 10pt; padding: 3px; border-radius:2px");
                vCtrlDsPregunta.InnerText = String.Format("{0}", item.DS_PREGUNTA);
                vCtrlRow.Controls.Add(vCtrlDsPregunta);


                vCtrlTabla.Controls.Add(vCtrlRow);
            }

            return vCtrlTabla;
        }

        public HtmlGenericControl generaCamposHtml()
        {
            HtmlGenericControl vCtrlDiv = new HtmlGenericControl("div");


            return vCtrlDiv;
          
        }
    }
}
