using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.Administracion
{
    public partial class Configuracion : System.Web.UI.Page
    {

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        string vFiLogotipo
        {
            get { return (string)ViewState["vs_vFiLogotipo"]; }
            set { ViewState["vs_vFiLogotipo"] = value; }
        }

        string vNbLogotipo
        {
            get { return (string)ViewState["vs_vNbLogotipo"]; }
            set { ViewState["vs_vNbLogotipo"] = value; }
        }

        #endregion

        #region Funciones

        protected void CargarDatos()
        {
            CatalogoListaNegocio nCatalogoLista = new CatalogoListaNegocio();
            List<SPE_OBTIENE_C_CATALOGO_LISTA_Result> vCatalogoLista = nCatalogoLista.ObtieneCatalogoLista();

            cmbCatalogoGenero.DataSource = vCatalogoLista;
            cmbCatalogoGenero.SelectedValue = ContextoApp.IdCatalogoGeneros.ToString();
            cmbCatalogoGenero.DataBind();

            cmbCatalogoCausaVacante.DataSource = vCatalogoLista;
            cmbCatalogoCausaVacante.SelectedValue = ContextoApp.IdCatalogoCausaVacantes.ToString();
            cmbCatalogoCausaVacante.DataBind();

            cmbCatalogoEstadoCivil.DataSource = vCatalogoLista;
            cmbCatalogoEstadoCivil.SelectedValue = ContextoApp.IdCatalogoEstadosCivil.ToString();
            cmbCatalogoEstadoCivil.DataBind();

            cmbCatalogoTipoTelefono.DataSource = vCatalogoLista;
            cmbCatalogoTipoTelefono.SelectedValue = ContextoApp.IdCatalogoTiposTelefono.ToString();
            cmbCatalogoTipoTelefono.DataBind();

            cmbCatalogoParentesco.DataSource = vCatalogoLista;
            cmbCatalogoParentesco.SelectedValue = ContextoApp.IdCatalogoParentescos.ToString();
            cmbCatalogoParentesco.DataBind();

            cmbCatalogoOcupacion.DataSource = vCatalogoLista;
            cmbCatalogoOcupacion.SelectedValue = ContextoApp.IdCatalogoOcupaciones.ToString();
            cmbCatalogoOcupacion.DataBind();

            cmbCatalogoRedSocial.DataSource = vCatalogoLista;
            cmbCatalogoRedSocial.SelectedValue = ContextoApp.IdCatalogoRedesSociales.ToString();
            cmbCatalogoRedSocial.DataBind();

            cmbCatalogoCausasRequisicion.DataSource = vCatalogoLista;
            cmbCatalogoCausasRequisicion.SelectedValue = ContextoApp.IdCatalogoCausaRequisicion.ToString();
            cmbCatalogoCausasRequisicion.DataBind();

            txtMailServer.Text = ContextoApp.mailConfiguration.Server;
            txtMailServerPort.Text = ContextoApp.mailConfiguration.Port.ToString();
            chkUseSSL.Checked = ContextoApp.mailConfiguration.UseSSL;
            chkAutenticar.Checked = ContextoApp.mailConfiguration.UseAuthentication;
            txtSenderName.Text = ContextoApp.mailConfiguration.SenderName;
            txtSenderMail.Text = ContextoApp.mailConfiguration.SenderMail;
            txtUser.Text = ContextoApp.mailConfiguration.User;

            txtNbOrganizacion.Text = ContextoApp.InfoEmpresa.NbEmpresa;

            if (ContextoApp.InfoEmpresa.FiLogotipo.FiArchivo != null)
            {
                rbiLogoOrganizacion.DataValue = ContextoApp.InfoEmpresa.FiLogotipo.FiArchivo;
                vFiLogotipo = Convert.ToBase64String(ContextoApp.InfoEmpresa.FiLogotipo.FiArchivo);
                vNbLogotipo = ContextoApp.InfoEmpresa.FiLogotipo.NbArchivo;
            }
            else
                rbiLogoOrganizacion.ImageUrl = "~/Assets/images/no-image.png";

            chkControlDocumentos.Checked = ContextoApp.ctrlDocumentos.fgHabilitado;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarDatos();
            }

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ContextoApp.IdCatalogoGeneros = int.Parse(cmbCatalogoGenero.SelectedValue);
            ContextoApp.IdCatalogoCausaVacantes = int.Parse(cmbCatalogoCausaVacante.SelectedValue);
            ContextoApp.IdCatalogoEstadosCivil = int.Parse(cmbCatalogoEstadoCivil.SelectedValue);
            ContextoApp.IdCatalogoTiposTelefono = int.Parse(cmbCatalogoTipoTelefono.SelectedValue);
            ContextoApp.IdCatalogoParentescos = int.Parse(cmbCatalogoParentesco.SelectedValue);
            ContextoApp.IdCatalogoOcupaciones = int.Parse(cmbCatalogoOcupacion.SelectedValue);
            ContextoApp.IdCatalogoRedesSociales = int.Parse(cmbCatalogoRedSocial.SelectedValue);
            ContextoApp.IdCatalogoCausaRequisicion = int.Parse(cmbCatalogoCausasRequisicion.SelectedValue);
            ContextoApp.mailConfiguration.Server = txtMailServer.Text;
            ContextoApp.mailConfiguration.Port = (int)txtMailServerPort.Value;
            ContextoApp.mailConfiguration.UseSSL = chkUseSSL.Checked;
            ContextoApp.mailConfiguration.UseAuthentication = chkAutenticar.Checked; 
            ContextoApp.mailConfiguration.SenderName = txtSenderName.Text;
            ContextoApp.mailConfiguration.SenderMail = txtSenderMail.Text;
            ContextoApp.mailConfiguration.User = txtUser.Text;

            if (chkPasswordChange.Checked)
                ContextoApp.mailConfiguration.Password = txtNbPassword.Text;

            chkPasswordChange.Checked = false;

            ContextoApp.ctrlDocumentos.fgHabilitado = chkControlDocumentos.Checked;

            ContextoApp.InfoEmpresa.NbEmpresa = txtNbOrganizacion.Text;
            ContextoApp.InfoEmpresa.FiLogotipo.FiArchivo = vFiLogotipo != null ? Convert.FromBase64String(vFiLogotipo) : null;
            ContextoApp.InfoEmpresa.FiLogotipo.NbArchivo = vNbLogotipo;

            E_RESULTADO vResultado = ContextoApp.SaveConfiguration(vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
        }

        protected void rauLogoOrganizacion_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            System.Drawing.Image bitmapImage = UtilImage.ResizeImage(System.Drawing.Image.FromStream(rauLogoOrganizacion.UploadedFiles[0].InputStream), 200, 200);
            MemoryStream stream = new MemoryStream();
            bitmapImage.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            vFiLogotipo = Convert.ToBase64String(stream.ToArray());
            vNbLogotipo = rauLogoOrganizacion.UploadedFiles[0].GetName();
            rbiLogoOrganizacion.DataValue = stream.ToArray();
        }

        protected void btnEliminarLogoOrganizacion_Click(object sender, EventArgs e)
        {
            vFiLogotipo = null;
            rbiLogoOrganizacion.DataValue = null;
            rbiLogoOrganizacion.ImageUrl = "~/Assets/images/no-image.png";
        }

        protected void grdPlantillas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PlantillaFormularioNegocio nPlantilla = new PlantillaFormularioNegocio();
            grdPlantillas.DataSource = nPlantilla.ObtienePlantillas();
        }

        protected void btnEliminarPlantilla_Click(object sender, EventArgs e)
        {
            PlantillaFormularioNegocio nPlantilla = new PlantillaFormularioNegocio();

            foreach (GridDataItem item in grdPlantillas.SelectedItems)
            {
                E_RESULTADO vResultado = nPlantilla.EliminaPlantillaFormulario(int.Parse(item.GetDataKeyValue("ID_PLANTILLA_SOLICITUD").ToString()), vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindow");
            }
        }

        protected void btnEstablecerGeneral_Click(object sender, EventArgs e)
        {
            PlantillaFormularioNegocio nPlantilla = new PlantillaFormularioNegocio();

            foreach (GridDataItem item in grdPlantillas.SelectedItems)
            {
                E_RESULTADO vResultado = nPlantilla.EstablecerPlantillaPorDefecto(int.Parse(item.GetDataKeyValue("ID_PLANTILLA_SOLICITUD").ToString()), item.GetDataKeyValue("CL_FORMULARIO").ToString(), vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindow");
            }
        }

        protected void grdCamposAdicionales_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            CampoFormularioNegocio nCampoAdicional = new CampoFormularioNegocio();
            grdCamposFormulario.DataSource = nCampoAdicional.ObtieneCamposFormularios();
        }

        protected void btnEliminarCampo_Click(object sender, EventArgs e)
        {
            CampoFormularioNegocio nCampoFormulario = new CampoFormularioNegocio();

            foreach (GridDataItem item in grdCamposFormulario.SelectedItems)
            {
                if (!bool.Parse(item.GetDataKeyValue("FG_SISTEMA").ToString()))
                {
                    E_RESULTADO vResultado = nCampoFormulario.EliminaCampoFormulario(int.Parse(item.GetDataKeyValue("ID_CAMPO_FORMULARIO").ToString()), vClUsuario, vNbPrograma);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseFieldWindow");
                }
            }
        }

        protected void grdPlantillas_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdPlantillas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdPlantillas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdPlantillas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdPlantillas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdPlantillas.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void grdCamposFormulario_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdCamposFormulario.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdCamposFormulario.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdCamposFormulario.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdCamposFormulario.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdCamposFormulario.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            UtilLicencias nLicencia = new UtilLicencias();
            E_RESULTADO vResultado = nLicencia.generaClaves(ContextoApp.Licencia.clCliente, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.FirstOrDefault().DS_MENSAJE;

            SPE_OBTIENE_S_CONFIGURACION_Result vConfiguracionLicencia = new SPE_OBTIENE_S_CONFIGURACION_Result();
            LicenciaNegocio oConfiguracion = new LicenciaNegocio();

            vConfiguracionLicencia = oConfiguracion.obtieneConfiguracionGeneral();

             if (vConfiguracionLicencia.CL_LICENCIAMIENTO == null)
            {
                nLicencia.generaXmlLicencias(vConfiguracionLicencia.CL_CLIENTE, vConfiguracionLicencia.CL_PASS_WS, "Web Service", "Web Service");
                nLicencia.insertaXmlIdentificacion(vConfiguracionLicencia.CL_CLIENTE, vConfiguracionLicencia.CL_PASS_WS, "Web Service", "Web Service");

                vConfiguracionLicencia = oConfiguracion.obtieneConfiguracionGeneral();
            }

            ContextoApp.Licencia.clCliente = vConfiguracionLicencia.CL_CLIENTE;
            ContextoApp.Licencia.clEmpresa = vConfiguracionLicencia.CL_EMPRESA;
            ContextoApp.Licencia.clLicencia = vConfiguracionLicencia.CL_LICENCIAMIENTO;
            ContextoApp.Licencia.clPassWs = vConfiguracionLicencia.CL_PASS_WS;
            ContextoApp.Licencia.feCreacion = vConfiguracionLicencia.FE_CREACION;
            ContextoApp.Licencia.objAdicional = vConfiguracionLicencia.OBJ_ADICIONAL;

            Crypto desencripta = new Crypto();
            string keyPassword = vConfiguracionLicencia.CL_PASS_WS.Substring(0, 16);
            string cadenaDesencriptadaLic = desencripta.descifrarTextoAES(vConfiguracionLicencia.CL_LICENCIAMIENTO, vConfiguracionLicencia.CL_CLIENTE, vConfiguracionLicencia.FE_CREACION, "SHA1", 22, keyPassword, 256);
            string cadenaDesencriptadaObj = desencripta.descifrarTextoAES(vConfiguracionLicencia.OBJ_ADICIONAL, vConfiguracionLicencia.CL_CLIENTE, vConfiguracionLicencia.FE_CREACION, "SHA1", 22, keyPassword, 256);
            XElement XmlConfiguracionLicencia = XElement.Parse(cadenaDesencriptadaLic);
            XElement XmlConfiguracionCliente = XElement.Parse(cadenaDesencriptadaObj);

            List<E_LICENCIA> lstLicencia = XmlConfiguracionCliente.Descendants("LICENCIA").Select(x => new E_LICENCIA
            {
                CL_CLIENTE = UtilXML.ValorAtributo<string>(x.Attribute("CL_CLIENTE")),
                CL_SISTEMA = UtilXML.ValorAtributo<string>(x.Attribute("CL_SISTEMA")),
                CL_EMPRESA = UtilXML.ValorAtributo<string>(x.Attribute("CL_EMPRESA")),
                CL_MODULO = UtilXML.ValorAtributo<string>(x.Attribute("CL_MODULO")),
                NO_RELEASE = UtilXML.ValorAtributo<string>(x.Attribute("NO_RELEASE")),
                FE_INICIO = UtilXML.ValorAtributo<string>(x.Attribute("FE_INICIO")),
                FE_FIN = UtilXML.ValorAtributo<string>(x.Attribute("FE_FIN")),
                NO_VOLUMEN = UtilXML.ValorAtributo<string>(x.Attribute("NO_VOLUMEN"))
            }).ToList();

            ContextoApp.IDP.LicenciaIntegracion.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "IDP", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);
            ContextoApp.FYD.LicenciaFormacion.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "FYD", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);
            ContextoApp.EO.LicenciaED.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "ED", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);
            ContextoApp.EO.LicenciaCL.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "CL", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);
            ContextoApp.EO.LicenciaRDP.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "RDP", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);
            ContextoApp.MPC.LicenciaMetodologia.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "MPC", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);
            ContextoApp.RP.LicenciaReportes.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "RP", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);
            ContextoApp.CI.LicenciaConsultasInteligentes.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "CI", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);
            ContextoApp.PDE.LicenciaPuntoEncuentro.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "PDE", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);

            if (lstLicencia.Count > 0)
            {
                ContextoApp.InfoEmpresa.Volumen = int.Parse(lstLicencia.FirstOrDefault().NO_VOLUMEN);

                if (ContextoApp.IDP.LicenciaIntegracion.MsgActivo == "1" || ContextoApp.FYD.LicenciaFormacion.MsgActivo == "1" || ContextoApp.EO.LicenciaED.MsgActivo == "1"
         || ContextoApp.EO.LicenciaCL.MsgActivo == "1" || ContextoApp.EO.LicenciaRDP.MsgActivo == "1" 
           || ContextoApp.MPC.LicenciaMetodologia.MsgActivo == "1" || ContextoApp.RP.LicenciaReportes.MsgActivo == "1" || ContextoApp.CI.LicenciaConsultasInteligentes.MsgActivo == "1"
           || ContextoApp.PDE.LicenciaPuntoEncuentro.MsgActivo == "1")
                {
                    ContextoApp.InfoEmpresa.MsgSistema = "1";
                }
                else
                {
                    ContextoApp.InfoEmpresa.MsgSistema = "El cliente actual no cuenta con licencias.";
                }
            }

            UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
        }
    }
}