using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.AdministracionSitio;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace SIGE.WebApp.FYD
{
    public partial class VentanaPeriodoPreguntasAdicionales : System.Web.UI.Page
    {
        #region Variables

        string vClUsuario;
        string vNbPrograma;
        //private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        public string vClIdioma = ContextoApp.clCultureIdioma;

        public int vIdPeriodo
        {
            get { return (int)ViewState["vs_vppa_id_periodo"]; }
            set { ViewState["vs_vppa_id_periodo"] = value; }
        }

        public int? vIdPregunta
        {
            get { return (int?)ViewState["vs_vIdPregunta"]; }
            set { ViewState["vs_vIdPregunta"] = value; }
        }

        #endregion

        #region Funciones

        protected void CargarDatos()
        {
            CampoFormularioNegocio nCampoFormulario = new CampoFormularioNegocio();
            cmbTipoControl.DataSource = nCampoFormulario.ObtieneTiposCampoFormulario().Where(w => (!w.FG_SISTEMA ?? false));
            cmbTipoControl.DataTextField = "NB_TIPO_CAMPO";
            cmbTipoControl.DataValueField = "CL_TIPO_CAMPO";
            cmbTipoControl.DataBind();

            CatalogoListaNegocio nCatalogoLista = new CatalogoListaNegocio();
            cmbComboboxCatalogo.DataSource = nCatalogoLista.ObtieneCatalogoLista().OrderBy(o => o.NB_CATALOGO_LISTA);
            cmbComboboxCatalogo.DataTextField = "NB_CATALOGO_LISTA";
            cmbComboboxCatalogo.DataValueField = "ID_CATALOGO_LISTA";
            cmbComboboxCatalogo.DataBind();

            CargaItemsCatalogoValor();

            switch (cmbTipoControl.SelectedValue)
            {
                case "TEXTBOX":
                    rpvTextbox.Selected = true;
                    break;
                case "COMBOBOX":
                    //cmbComboboxCatalogo.SelectedValue = UtilXML.ValorAtributo<string>(vXmlCampoFormulario.Attribute("ID_CATALOGO"));
                    rpvCombobox.Selected = true;
                    break;
                case "MASKBOX":
                    rpvMaskbox.Selected = true;
                    break;
                case "NUMERICBOX":
                    rpvNumericbox.Selected = true;
                    break;
                case "DATEPICKER":
                case "DATEAGE":
                    rpvDate.Selected = true;
                    break;
                case "CHECKBOX":
                    rpvCheckbox.Selected = true;
                    break;
                default:
                    rpvVacio.Selected = true;
                    break;
            }
        }

        protected void CargaItemsCatalogoValor()
        {
            CatalogoValorNegocio nCatalogoValor = new CatalogoValorNegocio();
            List<SPE_OBTIENE_C_CATALOGO_VALOR_Result> vCatalogoValor = nCatalogoValor.Obtener_C_CATALOGO_VALOR();

            cmbComboboxDefault.DataSource = vCatalogoValor.Where(w => w.ID_CATALOGO_LISTA.Equals(int.Parse(cmbComboboxCatalogo.SelectedValue)));
            cmbComboboxDefault.DataTextField = "NB_CATALOGO_VALOR";
            cmbComboboxDefault.DataValueField = "CL_CATALOGO_VALOR";
            cmbComboboxDefault.DataBind();
        }

        protected bool ValidarPregunta()
        {
            SIGE.Negocio.FormacionDesarrollo.PeriodoNegocio nPeriodos = new SIGE.Negocio.FormacionDesarrollo.PeriodoNegocio();
            List<SPE_OBTIENE_FYD_PREGUNTAS_ADICIONALES_PERIODO_Result> vLstPreguntas = nPeriodos.ObtienePreguntasAdicionales(pIdPeriodo: vIdPeriodo);

            foreach (var item in vLstPreguntas)
            {
                XElement vXmlPregunta = XElement.Parse(item.XML_PREGUNTA);
                string vClPregunta = (vXmlPregunta.Attribute("ID_CAMPO").Value.ToString());

                if (vIdPregunta == null)
                {
                    if (vClPregunta == txtClave.Text)
                    {
                        return false;
                    }
                }
                else
                {
                    if (vClPregunta == txtClave.Text && item.ID_PREGUNTA_ADICIONAL != vIdPregunta)
                    {
                        return false;
                    }
                }

            }

            return true;
        }

        protected void MostrarDiv(string pClTipoCampo, XElement vXmlPregunta)
        {
            switch (pClTipoCampo)
            {
                case "TEXTBOX":
                    txtTextboxLongitud.Text = vXmlPregunta.Attribute("NO_LONGITUD").Value.ToString();
                    txtTextboxDefault.Text = vXmlPregunta.Attribute("NO_VALOR_DEFECTO").Value.ToString();
                    rmpAdicionales.PageViews[1].Selected = true;
                    break;
                case "DATEPICKER":
                    if (vXmlPregunta.Attribute("NO_VALOR_DEFECTO") != null)
                        txtDateDefault.SelectedDate = DateTime.Parse(vXmlPregunta.Attribute("NO_VALOR_DEFECTO").Value.ToString());
                    rmpAdicionales.PageViews[2].Selected = true;
                    break;
                case "DATEAGE":
                    if (vXmlPregunta.Attribute("NO_VALOR_DEFECTO") != null)
                        txtDateDefault.SelectedDate = DateTime.Parse(vXmlPregunta.Attribute("NO_VALOR_DEFECTO").Value.ToString());
                    rmpAdicionales.PageViews[2].Selected = true;
                    break;
                case "COMBOBOX":
                    cmbComboboxCatalogo.SelectedValue = vXmlPregunta.Attribute("ID_CATALOGO").Value.ToString();
                    CargaItemsCatalogoValor();
                    cmbComboboxDefault.SelectedValue = vXmlPregunta.Attribute("NO_VALOR_DEFECTO").Value.ToString();
                    rmpAdicionales.PageViews[3].Selected = true;
                    break;
                case "MASKBOX":
                    txtMaskboxMascara.Text = vXmlPregunta.Attribute("NB_MASCARA").Value.ToString();
                    rmpAdicionales.PageViews[4].Selected = true;
                    break;
                case "NUMERICBOX":
                    txtNumericboxDecimales.Text = vXmlPregunta.Attribute("NO_DECIMALES").Value.ToString();
                    txtNumericboxEnteros.Text = vXmlPregunta.Attribute("NO_ENTEROS").Value.ToString();
                    txtNumericboxDefault.Text = vXmlPregunta.Attribute("NO_VALOR_DEFECTO").Value.ToString();
                    rmpAdicionales.PageViews[5].Selected = true;
                    break;
                case "CHECKBOX":
                    chkCheckboxDefault.Checked = vXmlPregunta.Attribute("NO_VALOR_DEFECTO").Value.ToString() == "1" ? true : false;
                    rmpAdicionales.PageViews[6].Selected = true;
                    break;
                default:

                    break;
            }
        }

        protected void CargarDatosPregunta()
        {
            SIGE.Negocio.FormacionDesarrollo.PeriodoNegocio nPeriodos = new SIGE.Negocio.FormacionDesarrollo.PeriodoNegocio();
            SPE_OBTIENE_FYD_PREGUNTAS_ADICIONALES_PERIODO_Result vLstPreguntas = nPeriodos.ObtienePreguntasAdicionales(vIdPeriodo, vIdPregunta).FirstOrDefault();
            if (vLstPreguntas != null)
            {

                if (vLstPreguntas.XML_PREGUNTA != null)
                {
                    XElement vXmlPregunta = XElement.Parse(vLstPreguntas.XML_PREGUNTA);
                    txtClave.Text = vXmlPregunta.Attribute("ID_CAMPO").Value.ToString();
                    txtNombre.Text = vLstPreguntas.NB_PREGUNTA;
                    txtTooltip.Text = vXmlPregunta.Attribute("NB_TOOLTIP").Value.ToString();
                    cmbTipoControl.SelectedValue = vXmlPregunta.Attribute("CL_TIPO").Value.ToString();



                    MostrarDiv(vXmlPregunta.Attribute("CL_TIPO").Value.ToString(), vXmlPregunta);

                    if (vLstPreguntas.CL_CUESTIONARIO_OBJETIVO == "OTROS")
                        btnCuestionarioOtros.Checked = true;
                    else if (vLstPreguntas.CL_CUESTIONARIO_OBJETIVO == "AUTOEVALUACION")
                        btnCuestionarioAutoevaluacion.Checked = true;
                    else
                        btnCuestionarioAmbos.Checked = true;


                }
            }
        }

        //Metodo de traducción
        protected void TraducirTextos()
        {
            //Asignar texto variables vista
            TraduccionIdiomaTextoNegocio oNegocio = new TraduccionIdiomaTextoNegocio();
            List<SPE_OBTIENE_TRADUCCION_TEXTO_Result> vLstTextosTraduccion = oNegocio.ObtieneTraduccion(pCL_MODULO: "FYD", pCL_PROCESO: "EC_PREGUNTASADICIONALES", pCL_IDIOMA: "PORT");
            if (vLstTextosTraduccion.Count > 0)
            {

                //Asignar texto label
                lblClave.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblClave").FirstOrDefault().DS_TEXTO;
                lblNombre.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblNombre").FirstOrDefault().DS_TEXTO;
                lblTooltip.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblTooltip").FirstOrDefault().DS_TEXTO;
                lblTipoControl.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblTipoControl").FirstOrDefault().DS_TEXTO;
                lblTextboxLongitud.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblTextboxLongitud").FirstOrDefault().DS_TEXTO;
                lblTextboxDefault.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblTextboxDefault").FirstOrDefault().DS_TEXTO;
                lblDateDefault.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblDateDefault").FirstOrDefault().DS_TEXTO;
                lblComboboxCatalogo.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblComboboxCatalogo").FirstOrDefault().DS_TEXTO;
                lblComboboxDefault.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblComboboxDefault").FirstOrDefault().DS_TEXTO;
                lblMaskboxMascara.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblMaskboxMascara").FirstOrDefault().DS_TEXTO;
                lblNumericboxEnteros.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblNumericboxEnteros").FirstOrDefault().DS_TEXTO;
                lblNumericboxDecimales.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblNumericboxDecimales").FirstOrDefault().DS_TEXTO;
                lblNumericboxDefault.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblNumericboxDefault").FirstOrDefault().DS_TEXTO;
                lblCheckboxDefault.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblCheckboxDefault").FirstOrDefault().DS_TEXTO;
                lblGrupoCuestionario.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblGrupoCuestionario").FirstOrDefault().DS_TEXTO;
               
                //Asignar texto RadButton
                btnCuestionarioAutoevaluacion.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnCuestionarioAutoevaluacion").FirstOrDefault().DS_TEXTO;
                btnCuestionarioOtros.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnCuestionarioOtros").FirstOrDefault().DS_TEXTO;
                btnCuestionarioAmbos.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnCuestionarioAmbos").FirstOrDefault().DS_TEXTO;
                btnGuardar.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnGuardar").FirstOrDefault().DS_TEXTO;
                btnGuardar.ToolTip = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnGuardar_tooltip").FirstOrDefault().DS_TEXTO;
                

                rspAyudaCuestionario.Title = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vrspAyudaCuestionario").FirstOrDefault().DS_TEXTO;
                pTextoAyuda.InnerHtml = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vpTextoAyuda").FirstOrDefault().DS_TEXTO;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;


            if (!Page.IsPostBack)
            {
                if (Request.Params["IdPeriodo"] != null)
                {
                    vIdPeriodo = int.Parse(Request.Params["IdPeriodo"].ToString());
                    CargarDatos();
                }

                if (Request.Params["IdPregunta"] != null)
                {
                    vIdPregunta = int.Parse(Request.Params["IdPregunta"].ToString());
                    CargarDatosPregunta();
                }

                //Se verifica si el idioma y si es necesaio traducir
                if (vClIdioma != E_IDIOMA_ENUM.ES.ToString())
                    TraducirTextos();
            }

        }

        protected void cmbComboboxCatalogo_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            CargaItemsCatalogoValor();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            XElement vXmlCampo = new XElement("CAMPO");

            if (txtClave.Text != "" && txtNombre.Text != "")
            {

                string vClTipoCampo = cmbTipoControl.SelectedValue;

                vXmlCampo.SetAttributeValue("CL_TIPO", vClTipoCampo);
                vXmlCampo.SetAttributeValue("ID_CAMPO", txtClave.Text);
                vXmlCampo.SetAttributeValue("NB_CAMPO", txtNombre.Text);
                vXmlCampo.SetAttributeValue("NB_TOOLTIP", txtTooltip.Text);

                if (ValidarPregunta())
                {

                    switch (vClTipoCampo)
                    {
                        case "TEXTBOX":
                            vXmlCampo.SetAttributeValue("NO_LONGITUD", txtTextboxLongitud.Text);
                            vXmlCampo.SetAttributeValue("NO_ANCHO", int.Parse(txtTextboxLongitud.Text) * 2);
                            vXmlCampo.SetAttributeValue("NO_VALOR_DEFECTO", txtTextboxDefault.Text);

                            if (int.Parse(txtTextboxLongitud.Text) > 200)
                            {
                                vXmlCampo.SetAttributeValue("NO_ANCHO", txtTextboxLongitud.Text);
                                vXmlCampo.SetAttributeValue("FG_MULTILINEA", "1");
                            }
                            else
                            {
                                vXmlCampo.SetAttributeValue("NO_LARGO", 30);
                                vXmlCampo.SetAttributeValue("NO_ANCHO", int.Parse(txtTextboxLongitud.Text) * 3);
                                vXmlCampo.SetAttributeValue("FG_MULTILINEA", "0");
                            }

                            break;
                        case "DATEPICKER":
                        case "DATEAGE":
                            if (txtDateDefault.SelectedDate != null)
                                vXmlCampo.SetAttributeValue("NO_VALOR_DEFECTO", ((DateTime)txtDateDefault.SelectedDate).ToString("dd/MM/yyyy"));
                            break;
                        case "COMBOBOX":
                            vXmlCampo.SetAttributeValue("ID_CATALOGO", cmbComboboxCatalogo.SelectedValue);
                            vXmlCampo.SetAttributeValue("NO_VALOR_DEFECTO", cmbComboboxDefault.SelectedValue);
                            break;
                        case "MASKBOX":
                            vXmlCampo.SetAttributeValue("NO_ANCHO", 100);
                            vXmlCampo.SetAttributeValue("NB_MASCARA", txtMaskboxMascara.Text);
                            break;
                        case "NUMERICBOX":
                            vXmlCampo.SetAttributeValue("NO_DECIMALES", txtNumericboxDecimales.Text);
                            vXmlCampo.SetAttributeValue("NO_ENTEROS", txtNumericboxEnteros.Text);
                            vXmlCampo.SetAttributeValue("NO_ANCHO", String.Format("{0:N0},{1:N0}", txtNumericboxEnteros.Value + txtNumericboxDecimales.Value, txtNumericboxDecimales.Value));
                            vXmlCampo.SetAttributeValue("NO_VALOR_DEFECTO", (decimal)txtNumericboxDefault.Value);
                            break;
                        case "CHECKBOX":
                            vXmlCampo.SetAttributeValue("NO_VALOR_DEFECTO", chkCheckboxDefault.Checked ? "1" : "0");
                            break;
                    }

                    //XElement vXmlPreguntasAdicionales = new XElement("PREGUNTAS_ADICIONALES");
                    //vXmlPreguntasAdicionales.Add(lstCampoAdicional.Items.Where(w => !w.Value.Equals(String.Empty)).Select(s => new XElement("PREGUNTA", new XAttribute("ID_CAMPO", s.Value))));

                    SIGE.Negocio.FormacionDesarrollo.PeriodoNegocio nPeriodo = new SIGE.Negocio.FormacionDesarrollo.PeriodoNegocio();

                    E_CL_CUESTIONARIO_OBJETIVO vClCuestionarioObjetivo = E_CL_CUESTIONARIO_OBJETIVO.AMBOS;

                    if (btnCuestionarioAutoevaluacion.Checked)
                        vClCuestionarioObjetivo = E_CL_CUESTIONARIO_OBJETIVO.AUTOEVALUACION;

                    if (btnCuestionarioOtros.Checked)
                        vClCuestionarioObjetivo = E_CL_CUESTIONARIO_OBJETIVO.OTROS;

                    string vClTipoTransaccion = "I";
                    if (vIdPregunta != null)
                        vClTipoTransaccion = "A";

                    E_RESULTADO vResultado = nPeriodo.InsertaPreguntasAdicionales(vIdPeriodo, vIdPregunta, txtNombre.Text, vXmlCampo, vClCuestionarioObjetivo.ToString(), vClUsuario, vNbPrograma, vClTipoTransaccion);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "generateDataForParent");
                }
                else
                {
                    if (vClIdioma != E_IDIOMA_ENUM.ES.ToString())
                    {
                        TraduccionIdiomaTextoNegocio oNegocio = new TraduccionIdiomaTextoNegocio();
                        SPE_OBTIENE_TRADUCCION_TEXTO_Result vLstTextosTraduccion = oNegocio.ObtieneTraduccion(pCL_TEXTO: "vCB_ValidaClaveNombre", pCL_MODULO: "FYD", pCL_PROCESO: "EC_PREGUNTASADICIONALES", pCL_IDIOMA: "PORT").FirstOrDefault();
                        if (vLstTextosTraduccion != null)
                            UtilMensajes.MensajeResultadoDB(rwmAlertas, vLstTextosTraduccion.DS_TEXTO, E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "");
                    }
                    else
                        UtilMensajes.MensajeResultadoDB(rwmAlertas, "Ya existe un registro con esta clave.", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "");
                }
            }

            else
            {
                if (vClIdioma != E_IDIOMA_ENUM.ES.ToString())
                {
                    TraduccionIdiomaTextoNegocio oNegocio = new TraduccionIdiomaTextoNegocio();
                    SPE_OBTIENE_TRADUCCION_TEXTO_Result vLstTextosTraduccion = oNegocio.ObtieneTraduccion(pCL_TEXTO: "vCB_ValidaPregunta", pCL_MODULO: "FYD", pCL_PROCESO: "EC_PREGUNTASADICIONALES", pCL_IDIOMA: "PORT").FirstOrDefault();
                    if (vLstTextosTraduccion != null)
                        UtilMensajes.MensajeResultadoDB(rwmAlertas, vLstTextosTraduccion.DS_TEXTO, E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "");
                }
                else
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, "La clave y el título del campo son requeridos.", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "");
            }
        }
    }
}