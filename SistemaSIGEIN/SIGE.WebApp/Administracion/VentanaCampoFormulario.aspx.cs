using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaCampoFormulario : System.Web.UI.Page
    {
        public int? vIdCampoAdicional
        {
            get { return (int?)ViewState["vs_vIdCampoAdicional"]; }
            set { ViewState["vs_vIdCampoAdicional"] = value; }
        }

        public string vClTipoPlantilla
        {
            get { return (string)ViewState["vs_vClTipoPlantilla"]; }
            set { ViewState["vs_vClTipoPlantilla"] = value; }
        }

        public string vClAccion
        {
            get { return (string)ViewState["vs_vClAccion"]; }
            set { ViewState["vs_vClAccion"] = value; }
        }

        public XElement vXmlCampoFormulario
        {
            get { return XElement.Parse((string)(ViewState["vs_vXmlCampoFormulario"] ?? new XElement("CAMPO").ToString())); }
            set { ViewState["vs_vXmlCampoFormulario"] = value.ToString(); }
        }

        public bool vFgSistema
        {
            get { return (bool)(ViewState["vs_vFgSistema"] ?? false); }
            set { ViewState["vs_vFgSistema"] = value; }
        }

        public string vClTipoTransaccion
        {
            get { return (string)ViewState["vs_vClTipoTransaccion"]; }
            set { ViewState["vs_vClTipoTransaccion"] = value; }
        }

        string vClUsuario;
        string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                vClAccion = (string)Request.QueryString["AccionCl"];
                vClTipoPlantilla = (string)Request.QueryString["PlantillaTipoCl"];

                int idCampo = 0;
                if (int.TryParse((string)Request.QueryString["CampoId"], out idCampo))
                    vIdCampoAdicional = idCampo;

                CargarDatos();
                List<E_TIPO_FORMULARIO> ListaTipoFormulario = new List<E_TIPO_FORMULARIO>();

                ListaTipoFormulario.Add(new E_TIPO_FORMULARIO { ID_FORMULARIO = 1, NB_FORMULARIO = "Solicitud", CL_FORMULARIO = "SOLICITUD" });
                ListaTipoFormulario.Add(new E_TIPO_FORMULARIO { ID_FORMULARIO = 2, NB_FORMULARIO = "Inventario", CL_FORMULARIO = "INVENTARIO" });
                ListaTipoFormulario.Add(new E_TIPO_FORMULARIO { ID_FORMULARIO = 3, NB_FORMULARIO = "Descriptivo", CL_FORMULARIO = "DESCRIPTIVO" });
                //ListaTipoFormulario.Add(new E_TIPO_FORMULARIO { ID_FORMULARIO = 4, NB_FORMULARIO = "Cuestionario", CL_FORMULARIO = "CUESTIONARIO" });
                ListaTipoFormulario.Add(new E_TIPO_FORMULARIO { ID_FORMULARIO = 5, NB_FORMULARIO = "Inventario PDE", CL_FORMULARIO = "INVENTARIO_PDE" });
                ListaTipoFormulario.Add(new E_TIPO_FORMULARIO { ID_FORMULARIO = 6, NB_FORMULARIO = "Formatos y trámites PDE", CL_FORMULARIO = "FORMATO_TRAMITE_PDE" });
                ListaTipoFormulario.Add(new E_TIPO_FORMULARIO { ID_FORMULARIO = 7, NB_FORMULARIO = "Instructor", CL_FORMULARIO = "INSTRUCTOR" });
                ListaTipoFormulario.Add(new E_TIPO_FORMULARIO { ID_FORMULARIO = 8, NB_FORMULARIO = "Curso", CL_FORMULARIO = "CURSO" });
                ListaTipoFormulario.Add(new E_TIPO_FORMULARIO { ID_FORMULARIO = 9, NB_FORMULARIO = "Evento", CL_FORMULARIO = "EVENTO" });

                cmbTipoFormulario.DataSource = ListaTipoFormulario;
                cmbTipoFormulario.DataValueField = "CL_FORMULARIO";
                cmbTipoFormulario.DataTextField = "NB_FORMULARIO";
                cmbTipoFormulario.DataBind();
            }

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
        }

        protected void CargarDatos()
        {
            CampoFormularioNegocio nCampoFormulario = new CampoFormularioNegocio();
            cmbTipoControl.DataSource = nCampoFormulario.ObtieneTiposCampoFormulario().Where(w => (!w.FG_SISTEMA ?? false) || !vClAccion.Equals("add"));
            cmbTipoControl.DataTextField = "NB_TIPO_CAMPO";
            cmbTipoControl.DataValueField = "CL_TIPO_CAMPO";
            cmbTipoControl.DataBind();

            CatalogoListaNegocio nCatalogoLista = new CatalogoListaNegocio();
            cmbComboboxCatalogo.DataSource = nCatalogoLista.ObtieneCatalogoLista().OrderBy(o => o.NB_CATALOGO_LISTA);
            cmbComboboxCatalogo.DataTextField = "NB_CATALOGO_LISTA";
            cmbComboboxCatalogo.DataValueField = "ID_CATALOGO_LISTA";
            cmbComboboxCatalogo.DataBind();
         
            vClTipoTransaccion = "I";
            if (!vClAccion.Equals("add"))
                AsignarValores(vClAccion.Equals("copy"));

            switch (cmbTipoControl.SelectedValue)
            {
                case "TEXTBOX":
                    rpvTextbox.Selected = true;
                    break;
                case "COMBOBOX":
                    cmbComboboxCatalogo.SelectedValue = UtilXML.ValorAtributo<string>(vXmlCampoFormulario.Attribute("ID_CATALOGO"));
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

            CargaItemsCatalogoValor();

        }

        protected void AsignarValores(bool pFgCopy)
        {
            if (!pFgCopy)
                vClTipoTransaccion = "A";

            cmbTipoControl.Enabled = false;

            txtClave.Enabled = pFgCopy;
            cmbTipoFormulario.Enabled = pFgCopy;

            SPE_OBTIENE_CAMPO_FORMULARIO_Result vCampoFormulario = new SPE_OBTIENE_CAMPO_FORMULARIO_Result();
            CampoFormularioNegocio nCampoFormulario = new CampoFormularioNegocio();
            vCampoFormulario = nCampoFormulario.ObtieneCamposFormularios(pIdCampoFormulario: vIdCampoAdicional).FirstOrDefault();
            vXmlCampoFormulario = XElement.Parse(vCampoFormulario.XML_CAMPO_FORMULARIO);
            vFgSistema = vCampoFormulario.FG_SISTEMA && !pFgCopy;

            txtClave.Text = vCampoFormulario.CL_CAMPO_FORMULARIO;
            txtNombre.Text = vCampoFormulario.NB_CAMPO_FORMULARIO;
            txtTooltip.Text = vCampoFormulario.NB_TOOLTIP;
            chkActivo.Checked = vCampoFormulario.FG_ACTIVO;

            cmbTipoFormulario.SelectedValue = vClTipoPlantilla;

            cmbTipoControl.SelectedValue = vCampoFormulario.CL_TIPO_CAMPO;

            switch (vCampoFormulario.CL_TIPO_CAMPO)
            {
                case "TEXTBOX":
                    txtTextboxLongitud.Text = UtilXML.ValorAtributo<string>(vXmlCampoFormulario.Attribute("NO_LONGITUD"));
                    txtTextboxDefault.Text = UtilXML.ValorAtributo<string>(vXmlCampoFormulario.Attribute("NO_VALOR_DEFECTO"));
                    break;
                case "COMBOBOX":
                    string vValor = UtilXML.ValorAtributo<string>(vXmlCampoFormulario.Attribute("NO_VALOR_DEFECTO"));
                    cmbComboboxDefault.SelectedValue = vValor;
                    break;
                case "DATEAGE":
                case "DATEPICKER":
                    string NbValor = UtilXML.ValorAtributo<string>(vXmlCampoFormulario.Attribute("NO_VALOR_DEFECTO"));
                    if (String.IsNullOrWhiteSpace(NbValor))
                        NbValor = DateTime.Now.ToString("dd/MM/yyyy");
                    string[] vFechaEdad = NbValor.Split('/');
                    int vDiaEdad = int.Parse(vFechaEdad[0]);
                    int vMesEdad = int.Parse(vFechaEdad[1]);
                    int vAnioEdad = int.Parse(vFechaEdad[2]);

                    txtDateDefault.SelectedDate = new DateTime(vAnioEdad, vMesEdad, vDiaEdad);
                    break;
                case "MASKBOX":
                    txtMaskboxMascara.Text = UtilXML.ValorAtributo<string>(vXmlCampoFormulario.Attribute("NB_MASCARA"));
                    break;
                case "NUMERICBOX":
                    int vNoDecimales = 0;
                    if (int.TryParse(UtilXML.ValorAtributo<string>(vXmlCampoFormulario.Attribute("NO_DECIMALES")), out vNoDecimales))
                        txtNumericboxDecimales.Value = vNoDecimales;

                    int vNoEnteros = 0;
                    if (int.TryParse(UtilXML.ValorAtributo<string>(vXmlCampoFormulario.Attribute("NO_ENTEROS")), out vNoEnteros))
                        txtNumericboxEnteros.Value = vNoEnteros;

                    double vNoDefault = 0;
                    if (double.TryParse(UtilXML.ValorAtributo<string>(vXmlCampoFormulario.Attribute("NO_VALOR_DEFECTO")), out vNoDefault))
                        txtNumericboxDefault.Value = vNoDefault;

                    break;
                case "CHECKBOX":
                    chkCheckboxDefault.Checked = UtilXML.ValorAtributo<bool>(vXmlCampoFormulario.Attribute("NO_VALOR_DEFECTO"));
                    break;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            SPE_OBTIENE_CAMPO_FORMULARIO_Result vCampoFormulario = new SPE_OBTIENE_CAMPO_FORMULARIO_Result();
            vCampoFormulario.ID_CAMPO_FORMULARIO = vIdCampoAdicional ?? -1;
            vCampoFormulario.CL_CAMPO_FORMULARIO = txtClave.Text;
            vCampoFormulario.NB_CAMPO_FORMULARIO = txtNombre.Text;
            vCampoFormulario.NB_TOOLTIP = txtTooltip.Text;
            vCampoFormulario.CL_FORMULARIO = cmbTipoFormulario.SelectedValue;
            vCampoFormulario.FG_ACTIVO = chkActivo.Checked;
            vCampoFormulario.FG_SISTEMA = vFgSistema;

            XElement vXmlCampo = vXmlCampoFormulario;

            string vClTipoCampo = cmbTipoControl.SelectedValue;
            vCampoFormulario.CL_TIPO_CAMPO = vClTipoCampo;

            vXmlCampo.SetAttributeValue("CL_TIPO", vClTipoCampo);
            vXmlCampo.SetAttributeValue("ID_CAMPO", vCampoFormulario.CL_CAMPO_FORMULARIO);
            vXmlCampo.SetAttributeValue("NB_CAMPO", vCampoFormulario.NB_CAMPO_FORMULARIO);
            vXmlCampo.SetAttributeValue("NB_TOOLTIP", vCampoFormulario.NB_TOOLTIP);

            switch (vClTipoCampo)
            {
                case "TEXTBOX":
                    vXmlCampo.SetAttributeValue("NO_LONGITUD", txtTextboxLongitud.Text);
                    vXmlCampo.SetAttributeValue("CL_DIMENSION", txtTextboxLongitud.Text);
                    vXmlCampo.SetAttributeValue("NO_VALOR_DEFECTO", txtTextboxDefault.Text);
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
                    vXmlCampo.SetAttributeValue("CL_DIMENSION", 100);
                    vXmlCampo.SetAttributeValue("NB_MASCARA", txtMaskboxMascara.Text);
                    break;
                case "NUMERICBOX":
                    vXmlCampo.SetAttributeValue("NO_DECIMALES", txtNumericboxDecimales.Text);
                    vXmlCampo.SetAttributeValue("NO_ENTEROS", txtNumericboxEnteros.Text);
                    vXmlCampo.SetAttributeValue("CL_DIMENSION", String.Format("{0:N0},{1:N0}", txtNumericboxEnteros.Value + txtNumericboxDecimales.Value, txtNumericboxDecimales.Value));
                    vXmlCampo.SetAttributeValue("NO_VALOR_DEFECTO", txtNumericboxDefault.Text ==""? "":((decimal)txtNumericboxDefault.Value).ToString());
                    break;
                case "CHECKBOX":
                    vXmlCampo.SetAttributeValue("NO_VALOR_DEFECTO", chkCheckboxDefault.Checked ? "1" : "0");
                    break;
            }

            vCampoFormulario.XML_CAMPO_FORMULARIO = vXmlCampo.ToString();

            CampoFormularioNegocio nCampoFormulario = new CampoFormularioNegocio();
            E_RESULTADO vResultado = nCampoFormulario.InsertaActualizaCampoFormulario(vClTipoTransaccion, vCampoFormulario, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR);
        }

        protected void cmbComboboxCatalogo_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            CargaItemsCatalogoValor();
        }

        protected void CargaItemsCatalogoValor()
        {
            CatalogoValorNegocio nCatalogoValor = new CatalogoValorNegocio();
            List<SPE_OBTIENE_C_CATALOGO_VALOR_Result> vCatalogoValor = nCatalogoValor.Obtener_C_CATALOGO_VALOR();

            cmbComboboxDefault.DataSource = vCatalogoValor.Where(w => w.ID_CATALOGO_LISTA.Equals(int.Parse(cmbComboboxCatalogo.SelectedValue)));
            cmbComboboxDefault.DataSource = vCatalogoValor;
            cmbComboboxDefault.DataValueField = "CL_CATALOGO_VALOR";
            cmbComboboxDefault.DataBind();
        }
    }
}