using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.Administracion;
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
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int vIdPeriodo
        {
            get { return (int)ViewState["vs_vppa_id_periodo"]; }
            set { ViewState["vs_vppa_id_periodo"] = value; }
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
            List<SPE_OBTIENE_FYD_PREGUNTAS_ADICIONALES_PERIODO_Result> vLstPreguntas = nPeriodos.ObtienePreguntasAdicionales(vIdPeriodo);

            foreach ( var item in vLstPreguntas)
            {
                XElement vXmlPregunta = XElement.Parse(item.XML_PREGUNTA);
                string vIdPregunta = (vXmlPregunta.Attribute("ID_CAMPO").Value.ToString());
                if (vIdPregunta == txtClave.Text)
                {
                    return false;
                }
            }

            return true;
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
            }

        }

        protected void cmbComboboxCatalogo_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            CargaItemsCatalogoValor();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            XElement vXmlCampo = new XElement("CAMPO");

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

                E_RESULTADO vResultado = nPeriodo.InsertaPreguntasAdicionales(vIdPeriodo, txtNombre.Text, vXmlCampo, vClCuestionarioObjetivo, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "generateDataForParent");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Ya existe un registro con esta clave.", E_TIPO_RESPUESTA_DB.ERROR , pCallBackFunction:"");
            }
        }
    }
}