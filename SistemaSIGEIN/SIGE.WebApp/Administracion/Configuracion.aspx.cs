using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.AdministracionSitio;
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

        private string vNbCampo
        {
            get { return (string)ViewState["vs_vNbCampo"]; }
            set { ViewState["vs_vNbCampo"] = value; }
        }

        private List<E_CAMPO_NOMINA_DO> vLstConfiguracion
        {
            get { return (List<E_CAMPO_NOMINA_DO>)ViewState["vs_vLstConfiguracion"]; }
            set { ViewState["vs_vLstConfiguracion"] = value; }
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

            if (ContextoApp.ANOM.LicenciaAccesoModulo.MsgActivo != "1")
            {
                rtsConfiguracion.Tabs[6].Visible = false;
            }
        }

        protected void EstatusBotones()
        {
            foreach (var item in ContextoApp.vLstCamposNominaDO)
            {
                switch (item.CL_CAMPO)
                {
                    case "CL_RFC":
                        btnRFCNOTrue.Checked = item.FG_EDITABLE_NOMINA;
                        btnRFCNOFalse.Checked = !item.FG_EDITABLE_NOMINA;
                        btnRFCDOTrue.Checked = item.FG_EDITABLE_DO;
                        btnRFCDOFalse.Checked = !item.FG_EDITABLE_DO;
                        break;
                    case "CL_CURP":
                        btnCURPDOTrue.Checked = item.FG_EDITABLE_DO;
                        btnCURPDOFalse.Checked = !item.FG_EDITABLE_DO;
                        btnCURPNOTrue.Checked = item.FG_EDITABLE_NOMINA;
                        btnCURPNOFalse.Checked = !item.FG_EDITABLE_NOMINA;
                        break;
                    case "CL_NSS":
                        btnNSSDOTrue.Checked = item.FG_EDITABLE_DO;
                        btnNSSDOFalse.Checked = !item.FG_EDITABLE_DO;
                        btnNSSNOTrue.Checked = item.FG_EDITABLE_NOMINA;
                        btnNSSNOFalse.Checked = !item.FG_EDITABLE_NOMINA;
                        break;
                    case "DS_LUGAR_NACIMIENTO":
                        btnNANOTrue.Checked = item.FG_EDITABLE_NOMINA;
                        btnNANOFalse.Checked = !item.FG_EDITABLE_NOMINA;
                        btnNADOTrue.Checked = item.FG_EDITABLE_DO;
                        btnNADOFalse.Checked = !item.FG_EDITABLE_DO;
                        break;
                    case "NB_ESTADO_NACIMIENTO":
                        btnEstadoNaNoTrue.Checked = item.FG_EDITABLE_NOMINA;
                        btnEstadoNaNoFalse.Checked = !item.FG_EDITABLE_NOMINA;
                        btnEstadoNaDoTrue.Checked = item.FG_EDITABLE_DO;
                        btnEstadoNaDoFalse.Checked = !item.FG_EDITABLE_DO;
                        break;
                    case "NB_NACIONALIDAD":
                        btnNacionalidadDOTrue.Checked = item.FG_EDITABLE_DO;
                        btnNacionalidadDOFlase.Checked = !item.FG_EDITABLE_DO;
                        btnNacionalidadNOTrue.Checked = item.FG_EDITABLE_NOMINA;
                        btnNacionalidadNOFalse.Checked = !item.FG_EDITABLE_NOMINA;
                        break;
                    case "FE_NACIMIENTO":
                        btnFeNacimientoDOTrue.Checked = item.FG_EDITABLE_DO;
                        btnFeNacimientoDOFalse.Checked = !item.FG_EDITABLE_DO;
                        btnFeNacimientoNOTrue.Checked = item.FG_EDITABLE_NOMINA;
                        btnFeNacimientoNOFalse.Checked = !item.FG_EDITABLE_NOMINA;
                        break;
                    case "CL_GENERO":
                        btnGeneroDOTrue.Checked = item.FG_EDITABLE_DO;
                        btnGeneroDoFalse.Checked = !item.FG_EDITABLE_DO;
                        btnGeneroNOTrue.Checked = item.FG_EDITABLE_NOMINA;
                        btnGeneroNOFalse.Checked = !item.FG_EDITABLE_NOMINA;
                        break;
                    case "CL_ESTADO_CIVIL":
                        btnCivilDOTrue.Checked = item.FG_EDITABLE_DO;
                        btnCivilDOFalse.Checked = !item.FG_EDITABLE_DO;
                        btnCivilNOTrue.Checked = item.FG_EDITABLE_NOMINA;
                        btnCivilNOFalse.Checked = !item.FG_EDITABLE_NOMINA;
                        break;
                    case "CL_CODIGO_POSTAL":
                        btnCPDOTrue.Checked = item.FG_EDITABLE_DO;
                        btnCPDOFalse.Checked = !item.FG_EDITABLE_DO;
                        btnCPNOTrue.Checked = item.FG_EDITABLE_NOMINA;
                        btnCPNOFalse.Checked = !item.FG_EDITABLE_NOMINA;
                        break;
                    case "NB_ESTADO":
                        btnEstadoDOTrue.Checked = item.FG_EDITABLE_DO;
                        btnEstadoDOFalse.Checked = !item.FG_EDITABLE_DO;
                        btnEstadoNOTrue.Checked = item.FG_EDITABLE_NOMINA;
                        btnEstadoNOFalse.Checked = !item.FG_EDITABLE_NOMINA;
                        break;
                    case "NB_MUNICIPIO":
                        btnMunicipioDOTrue.Checked = item.FG_EDITABLE_DO;
                        btnMunicipioDOFalse.Checked = !item.FG_EDITABLE_DO;
                        btnMunicipioNOTrue.Checked = item.FG_EDITABLE_NOMINA;
                        btnMunicipioNOFalse.Checked = !item.FG_EDITABLE_NOMINA;
                        break;
                    case "NB_COLONIA":
                        btnColoniaDOTrue.Checked = item.FG_EDITABLE_DO;
                        btnColoniaDOFalse.Checked = !item.FG_EDITABLE_DO;
                        btnColoniaNOTrue.Checked = item.FG_EDITABLE_NOMINA;
                        btnColoniaNOFalse.Checked = !item.FG_EDITABLE_NOMINA;
                        break;
                    case "NB_CALLE":
                        btnCalleDOTrue.Checked = item.FG_EDITABLE_DO;
                        btnCalleDOFalse.Checked = !item.FG_EDITABLE_DO;
                        btnCalleNOTrue.Checked = item.FG_EDITABLE_NOMINA;
                        btnCalleNOFalse.Checked = !item.FG_EDITABLE_NOMINA;
                        break;
                    case "NO_EXTERIOR":
                        btnNoExtDOTrue.Checked = item.FG_EDITABLE_DO;
                        btnNoExtDOFalse.Checked = !item.FG_EDITABLE_DO;
                        btnNoExtNOTrue.Checked = item.FG_EDITABLE_NOMINA;
                        btnNoExtNOFalse.Checked = !item.FG_EDITABLE_NOMINA;
                        break;
                    case "NO_INTERIOR":
                        btnNoInteriorDOTrue.Checked = item.FG_EDITABLE_DO;
                        btnNoInteriorDOFalse.Checked = !item.FG_EDITABLE_DO;
                        btnNoInteriorNOTrue.Checked = item.FG_EDITABLE_NOMINA;
                        btnNoInteriorNOFalse.Checked = !item.FG_EDITABLE_NOMINA;
                        break;
                    case "LS_TELEFONOS":
                        btnTelDOTrue.Checked = item.FG_EDITABLE_DO;
                        btnTelDOFalse.Checked = !item.FG_EDITABLE_DO;
                        btnTelNOTrue.Checked = item.FG_EDITABLE_NOMINA;
                        btnTelNOFalse.Checked = !item.FG_EDITABLE_NOMINA;
                        break;
                    case "CL_CORREO_ELECTRONICO":
                        btnEmailDOTrue.Checked = item.FG_EDITABLE_DO;
                        btnEmailDOFalse.Checked = !item.FG_EDITABLE_DO;
                        btnEmailNOTrue.Checked = item.FG_EDITABLE_NOMINA;
                        btnEmailNOFalse.Checked = !item.FG_EDITABLE_NOMINA;
                        break;
                    case "CL_CENTRO_OPERATIVO":
                        btnCODOTrue.Checked = item.FG_EDITABLE_DO;
                        btnCODOFalse.Checked = !item.FG_EDITABLE_DO;
                        btnCONOTrue.Checked = item.FG_EDITABLE_NOMINA;
                        btnCONOFalse.Checked = !item.FG_EDITABLE_NOMINA;
                        break;
                    case "CL_CENTRO_ADMINISTRATIVO":
                        btnCANOTrue.Checked = item.FG_EDITABLE_NOMINA;
                        btnCANOFalse.Checked = !item.FG_EDITABLE_NOMINA;
                        btnCADOTrue.Checked = item.FG_EDITABLE_DO;
                        btnCADOFalse.Checked = !item.FG_EDITABLE_DO;
                        break;
                }
            }
        }

        protected bool ValidarValorBotones()
        {
            vLstConfiguracion = new List<E_CAMPO_NOMINA_DO>();

            if (btnRFCNOTrue.Checked == false && btnRFCDOTrue.Checked == false)
            {
                vNbCampo = "RFC";
                return false;
            }
            else
            {
                vLstConfiguracion.Add(new E_CAMPO_NOMINA_DO { CL_CAMPO = "CL_RFC", FG_EDITABLE_NOMINA = btnRFCNOTrue.Checked ? true : false, FG_EDITABLE_DO = btnRFCDOTrue.Checked ? true : false });
            }
            if (btnCURPDOTrue.Checked == false && btnCURPNOTrue.Checked == false)
            {
                vNbCampo = "CURP";
                return false;
            }
            else
            {
                vLstConfiguracion.Add(new E_CAMPO_NOMINA_DO { CL_CAMPO = "CL_CURP", FG_EDITABLE_NOMINA = btnCURPNOTrue.Checked ? true : false, FG_EDITABLE_DO = btnCURPDOTrue.Checked ? true : false });
            }
            if (btnNSSDOTrue.Checked == false && btnNSSNOTrue.Checked == false)
            {
                vNbCampo = "No. De seguro social";
                return false;
            }
            else
            {
                vLstConfiguracion.Add(new E_CAMPO_NOMINA_DO { CL_CAMPO = "CL_NSS", FG_EDITABLE_NOMINA = btnNSSNOTrue.Checked ? true : false, FG_EDITABLE_DO = btnNSSDOTrue.Checked ? true : false });
            }
            if (btnNANOTrue.Checked == false && btnNADOTrue.Checked == false)
            {
                vNbCampo = "Lugar de nacimiento";
                return false;
            }
            else
            {
                vLstConfiguracion.Add(new E_CAMPO_NOMINA_DO { CL_CAMPO = "DS_LUGAR_NACIMIENTO", FG_EDITABLE_NOMINA = btnNANOTrue.Checked ? true : false, FG_EDITABLE_DO = btnNADOTrue.Checked ? true : false });
            }
            if (btnEstadoNaDoTrue.Checked == false && btnEstadoNaNoTrue.Checked == false)
            {
                vNbCampo = "Estado de nacimiento";
                return false;
            }
            else
            {
                vLstConfiguracion.Add(new E_CAMPO_NOMINA_DO { CL_CAMPO = "NB_ESTADO_NACIMIENTO", FG_EDITABLE_NOMINA = btnEstadoNaNoTrue.Checked ? true : false, FG_EDITABLE_DO = btnEstadoNaDoTrue.Checked ? true : false });
            }
            if (btnNacionalidadDOTrue.Checked == false && btnNacionalidadNOTrue.Checked == false)
            {
                vNbCampo = "Nacionalidad";
                return false;
            }
            else
            {
                vLstConfiguracion.Add(new E_CAMPO_NOMINA_DO { CL_CAMPO = "NB_NACIONALIDAD", FG_EDITABLE_NOMINA = btnNacionalidadNOTrue.Checked ? true : false, FG_EDITABLE_DO = btnNacionalidadDOTrue.Checked ? true : false });
            }
            if (btnFeNacimientoDOTrue.Checked == false && btnFeNacimientoNOTrue.Checked == false)
            {
                vNbCampo = "Fecha de nacimiento";
                return false;
            }
            else
            {
                vLstConfiguracion.Add(new E_CAMPO_NOMINA_DO { CL_CAMPO = "FE_NACIMIENTO", FG_EDITABLE_NOMINA = btnFeNacimientoNOTrue.Checked ? true : false, FG_EDITABLE_DO = btnFeNacimientoDOTrue.Checked ? true : false });
            }
            if (btnGeneroDOTrue.Checked == false && btnGeneroNOTrue.Checked == false)
            {
                vNbCampo = "Género";
                return false;
            }
            else
            {
                vLstConfiguracion.Add(new E_CAMPO_NOMINA_DO { CL_CAMPO = "CL_GENERO", FG_EDITABLE_NOMINA = btnGeneroNOTrue.Checked ? true : false, FG_EDITABLE_DO = btnGeneroDOTrue.Checked ? true : false });
            }
            if (btnCivilDOTrue.Checked == false && btnCivilNOTrue.Checked == false)
            {
                vNbCampo = "Estado civil";
                return false;
            }
            else
            {
                vLstConfiguracion.Add(new E_CAMPO_NOMINA_DO { CL_CAMPO = "CL_ESTADO_CIVIL", FG_EDITABLE_NOMINA = btnCivilNOTrue.Checked ? true : false, FG_EDITABLE_DO = btnCivilDOTrue.Checked ? true : false });
            }
            if (btnCPDOTrue.Checked == false && btnCPNOTrue.Checked == false)
            {
                vNbCampo = "C.P.";
                return false;
            }
            else
            {
                vLstConfiguracion.Add(new E_CAMPO_NOMINA_DO { CL_CAMPO = "CL_CODIGO_POSTAL", FG_EDITABLE_NOMINA = btnCPNOTrue.Checked ? true : false, FG_EDITABLE_DO = btnCPDOTrue.Checked ? true : false });
            }
            if (btnEstadoDOTrue.Checked = false && btnEstadoNOTrue.Checked == false)
            {
                vNbCampo = "Estado";
                return false;
            }
            else
            {
                vLstConfiguracion.Add(new E_CAMPO_NOMINA_DO { CL_CAMPO = "NB_ESTADO", FG_EDITABLE_NOMINA = btnEstadoNOTrue.Checked ? true : false, FG_EDITABLE_DO = btnEstadoDOFalse.Checked ? false : true });
            }
            if (btnMunicipioDOTrue.Checked == false && btnMunicipioNOTrue.Checked == false)
            {
                vNbCampo = "Municipio";
                return false;
            }
            else
            {
                vLstConfiguracion.Add(new E_CAMPO_NOMINA_DO { CL_CAMPO = "NB_MUNICIPIO", FG_EDITABLE_NOMINA = btnMunicipioNOTrue.Checked ? true : false, FG_EDITABLE_DO = btnMunicipioDOTrue.Checked ? true : false });
            }
            if (btnColoniaDOTrue.Checked == false && btnColoniaNOTrue.Checked == false)
            {
                vNbCampo = "Colonia";
                return false;
            }
            else
            {
                vLstConfiguracion.Add(new E_CAMPO_NOMINA_DO { CL_CAMPO = "NB_COLONIA", FG_EDITABLE_NOMINA = btnColoniaNOTrue.Checked ? true : false, FG_EDITABLE_DO = btnColoniaDOTrue.Checked ? true : false });
            }
            if (btnCalleDOTrue.Checked == false && btnCalleNOTrue.Checked == false)
            {
                vNbCampo = "Calle";
                return false;
            }
            else
            {
                vLstConfiguracion.Add(new E_CAMPO_NOMINA_DO { CL_CAMPO = "NB_CALLE", FG_EDITABLE_NOMINA = btnCalleNOTrue.Checked ? true : false, FG_EDITABLE_DO = btnCalleDOTrue.Checked ? true : false });
            }
            if (btnNoExtDOTrue.Checked == false && btnNoExtNOTrue.Checked == false)
            {
                vNbCampo = "No. Exterior";
                return false;
            }
            else
            {
                vLstConfiguracion.Add(new E_CAMPO_NOMINA_DO { CL_CAMPO = "NO_EXTERIOR", FG_EDITABLE_NOMINA = btnNoExtNOTrue.Checked ? true : false, FG_EDITABLE_DO = btnNoExtDOTrue.Checked ? true : false });
            }
            if (btnNoInteriorDOTrue.Checked == false && btnNoInteriorNOTrue.Checked == false)
            {
                vNbCampo = "No. Interior";
                return false;
            }
            else
            {
                vLstConfiguracion.Add(new E_CAMPO_NOMINA_DO { CL_CAMPO = "NO_INTERIOR", FG_EDITABLE_NOMINA = btnNoInteriorNOTrue.Checked ? true : false, FG_EDITABLE_DO = btnNoInteriorDOTrue.Checked ? true : false });
            }
            if (btnTelDOTrue.Checked == false && btnTelNOTrue.Checked == false)
            {
                vNbCampo = "Teléfono";
                return false;
            }
            else
            {
                vLstConfiguracion.Add(new E_CAMPO_NOMINA_DO { CL_CAMPO = "LS_TELEFONOS", FG_EDITABLE_NOMINA = btnTelNOTrue.Checked ? true : false, FG_EDITABLE_DO = btnTelDOTrue.Checked ? true : false });
            }
            if (btnEmailDOTrue.Checked == false && btnEmailNOTrue.Checked == false)
            {
                vNbCampo = "Correo electrónico";
                return false;
            }
            else
            {
                vLstConfiguracion.Add(new E_CAMPO_NOMINA_DO { CL_CAMPO = "CL_CORREO_ELECTRONICO", FG_EDITABLE_NOMINA = btnEmailNOTrue.Checked ? true : false, FG_EDITABLE_DO = btnEmailDOTrue.Checked ? true : false });
            }
            if (btnCODOTrue.Checked == false && btnCONOTrue.Checked == false)
            {
                vNbCampo = "Centro operativo";
                return false;
            }
            else
            {
                vLstConfiguracion.Add(new E_CAMPO_NOMINA_DO { CL_CAMPO = "CL_CENTRO_OPERATIVO", FG_EDITABLE_NOMINA = btnCONOTrue.Checked ? true : false, FG_EDITABLE_DO = btnCODOTrue.Checked ? true : false });
            }
            if (btnCANOTrue.Checked == false && btnCADOTrue.Checked == false)
            {
                vNbCampo = "Centro administrativo";
                return false;
            }
            else
            {
                vLstConfiguracion.Add(new E_CAMPO_NOMINA_DO { CL_CAMPO = "CL_CENTRO_ADMINISTRATIVO", FG_EDITABLE_NOMINA = btnCANOTrue.Checked ? true : false, FG_EDITABLE_DO = btnCADOTrue.Checked ? true : false });
            }

            return true;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarDatos();
                EstatusBotones();
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

            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                if (ValidarValorBotones())
                {
                    XElement vXmlConfiguracion = null;

                    var vConfiguracion = vLstConfiguracion.Select(x => new XElement("CAMPO",
                                                                       new XAttribute("CL_CAMPO", x.CL_CAMPO),
                                                                       new XAttribute("FG_EDITABLE_NOMINA", x.FG_EDITABLE_NOMINA),
                                                                       new XAttribute("FG_EDITABLE_DO", x.FG_EDITABLE_DO)
                                                                       ));

                    vXmlConfiguracion = new XElement("CAMPOS", vConfiguracion);

                    CamposNominaNegocio nCampos = new CamposNominaNegocio();
                    E_RESULTADO vResultado2 = nCampos.InsertaActualizaConfiguracionCampos(pXML_CONFIGURACION: vXmlConfiguracion.ToString(), pClUsuario: vClUsuario, pNbPrograma: vNbPrograma);
                    string vMensaje2 = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                    {
                        ContextoApp.vLstCamposNominaDO = vLstConfiguracion;
                    }
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, pCallBackFunction: null);
                }
                else
                {
                    UtilMensajes.MensajeDB(rwmAlertas, "El campo " + vNbCampo + " debe de ser editable desde Nómina o/y DO.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                }
            }

            else
            {

                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
            }
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
            ContextoApp.ANOM.LicenciaAccesoModulo.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "NOMINA", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);

            if (lstLicencia.Count > 0)
            {
                ContextoApp.InfoEmpresa.Volumen = int.Parse(lstLicencia.FirstOrDefault().NO_VOLUMEN);

                if (ContextoApp.IDP.LicenciaIntegracion.MsgActivo == "1" || ContextoApp.FYD.LicenciaFormacion.MsgActivo == "1" || ContextoApp.EO.LicenciaED.MsgActivo == "1"
         || ContextoApp.EO.LicenciaCL.MsgActivo == "1" || ContextoApp.EO.LicenciaRDP.MsgActivo == "1"
           || ContextoApp.MPC.LicenciaMetodologia.MsgActivo == "1" || ContextoApp.RP.LicenciaReportes.MsgActivo == "1" || ContextoApp.CI.LicenciaConsultasInteligentes.MsgActivo == "1"
           || ContextoApp.PDE.LicenciaPuntoEncuentro.MsgActivo == "1" || ContextoApp.ANOM.LicenciaAccesoModulo.MsgActivo == "1")
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

        //protected void rgGrupos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        //{
        //    GruposNegocio oNegocio = new GruposNegocio();
        //    rgGrupos.DataSource = oNegocio.ObtieneGrupos();
        //}

        //protected void rgGrupos_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item is GridPagerItem)
        //    {
        //        RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

        //        PageSizeCombo.Items.Clear();
        //        PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
        //        PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgGrupos.MasterTableView.ClientID);
        //        PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
        //        PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgGrupos.MasterTableView.ClientID);
        //        PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
        //        PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgGrupos.MasterTableView.ClientID);
        //        PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
        //        PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgGrupos.MasterTableView.ClientID);
        //        PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
        //        PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgGrupos.MasterTableView.ClientID);
        //        PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
        //    }
        //}

        //protected void btnEliminar_Click(object sender, EventArgs e)
        //{
        //    foreach(GridDataItem item in rgGrupos.SelectedItems)
        //    {
        //        int vIdGrupo = int.Parse(item.GetDataKeyValue("ID_GRUPO").ToString());

        //        GruposNegocio oNeg = new GruposNegocio();
        //        E_RESULTADO vResultado = oNeg.EliminaGrupo(pID_GRUPO: vIdGrupo);
        //        string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
        //        UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
        //    }

        //    rgGrupos.Rebind();
        //}
    }
}