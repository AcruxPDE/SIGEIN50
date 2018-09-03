using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.AdministracionSitio;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaConfigurarInventario : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

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

        #region Metodos

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
                    case "CL_OPERATIVO":
                        btnCODOTrue.Checked = item.FG_EDITABLE_DO;
                        btnCODOFalse.Checked = !item.FG_EDITABLE_DO;
                        btnCONOTrue.Checked = item.FG_EDITABLE_NOMINA;
                        btnCONOFalse.Checked = !item.FG_EDITABLE_NOMINA;
                        break;
                    case "CL_ADMINISTRATIVO":
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
                vLstConfiguracion.Add(new E_CAMPO_NOMINA_DO { CL_CAMPO = "CL_OPERATIVO", FG_EDITABLE_NOMINA = btnCONOTrue.Checked ? true : false, FG_EDITABLE_DO = btnCODOTrue.Checked ? true : false });
            }
            if (btnCANOTrue.Checked == false && btnCADOTrue.Checked == false)
            {
                vNbCampo = "Centro administrativo";
                return false;
            }
            else
            {
                vLstConfiguracion.Add(new E_CAMPO_NOMINA_DO { CL_CAMPO = "CL_ADMINISTRATIVO", FG_EDITABLE_NOMINA = btnCANOTrue.Checked ? true : false, FG_EDITABLE_DO = btnCADOTrue.Checked ? true : false });
            }

            return true;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EstatusBotones();
            }

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
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
                E_RESULTADO vResultado = nCampos.InsertaActualizaConfiguracionCampos(pXML_CONFIGURACION: vXmlConfiguracion.ToString(), pClUsuario: vClUsuario, pNbPrograma: vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                {
                    ContextoApp.vLstCamposNominaDO = vLstConfiguracion;
                }
                UtilMensajes.MensajeResultadoDB(rwAlerta, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindows");
            }
            else
            {
                UtilMensajes.MensajeDB(rwAlerta, "El campo " + vNbCampo + " debe de ser editable desde Nómina o/y DO.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            }
        }
    }
}