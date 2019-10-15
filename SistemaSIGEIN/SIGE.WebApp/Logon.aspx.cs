using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.Entidades;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace SIGE.WebApp
{
    public partial class Logon : System.Web.UI.Page
    {
        private const int TICKET_VERSION = 1;
        private string vClUsuario = "LOGON";
        private string vNbPrograma = ContextoUsuario.nbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private string ptipo
        {
            get { return (string)ViewState["vstipo"]; }
            set { ViewState["vstipo"] = value; }
        }

        private Guid? pFolioAutorizacion
        {
            get { return (Guid?)ViewState["vsFolioAutorizacion"]; }
            set { ViewState["vsFolioAutorizacion"] = value; }
        }

        private int? vIdPrograma
        {
            get { return (int?)ViewState["vsIdPrograma"]; }
            set { ViewState["vsIdPrograma"] = value; }
        }

        private int? vIdPeriodo
        {
            get { return (int?)ViewState["vsIdPeriodo"]; }
            set { ViewState["vsIdPeriodo"] = value; }
        }

        private string pClaveToken
        {
            get { return (string)ViewState["vsClaveToken"]; }
            set { ViewState["vsClaveToken"] = value; }
        }

        #region Variables procesos externos

        private string vClProceso
        {
            get { return (string)ViewState["vs_logon_cl_proceso"]; }
            set { ViewState["vs_logon_cl_proceso"] = value; }
        }

        private Guid? vFolioProceso
        {
            get { return (Guid?)ViewState["vs_logon_fl_proceso"]; }
            set { ViewState["vs_logon_fl_proceso"] = value; }
        }

        private ProcesoExterno pe
        {
            get { return (ProcesoExterno)ViewState["vs_logon_pe"]; }
            set { ViewState["vs_logon_pe"] = value; }
        }

        #endregion

        #region Metodos para procesos externo

        private void mostrarDiv()
        {
            switch (vClProceso)
            {
                case "EVALUACION":

                    ContentAutorizaDocumentos.Visible = false;
                    ContentLogin.Visible = false;
                    ContentPasswordRecovery.Visible = false;
                    ContentCodigoConfirmacion.Visible = false;
                    ContentEvaluacionResultados.Visible = true;
                    ContentCuestionarios.Visible = false;
                    ContentClimaLaboral.Visible = false;
                    ContentCartera.Visible = false;
                    ContentEntrevista.Visible = false;
                    ContentAutorizaRequisicion.Visible = false;
                    ContentEvaluacionDesempeno.Visible = false;
                    ContentAutorizaPuestoRequisicion.Visible = false;
                    ContentCuestionario.Visible = false;
                    pe.FolioProceso = vFolioProceso.Value;

                    if (pe.ObtenerInformacionProceso(vClProceso))
                    {
                        txtEvento.Text = pe.NombreProceso;
                        txtEvaluador.Text = pe.UsuarioProceso;
                    }
                    else
                    {
                        UtilMensajes.MensajeResultadoDB(RadWindowManager1, pe.MensajeError, E_TIPO_RESPUESTA_DB.WARNING);
                    }
                    txtEvento.ReadOnly = true;
                    txtEvaluador.ReadOnly = true;

                    break;

                case "CUESTIONARIOS":
                    ContentAutorizaDocumentos.Visible = false;
                    ContentLogin.Visible = false;
                    ContentPasswordRecovery.Visible = false;
                    ContentCodigoConfirmacion.Visible = false;
                    ContentEvaluacionResultados.Visible = false;
                    ContentCuestionarios.Visible = true;
                    ContentClimaLaboral.Visible = false;
                    ContentCartera.Visible = false;
                    ContentEntrevista.Visible = false;
                    ContentAutorizaRequisicion.Visible = false;
                    ContentEvaluacionDesempeno.Visible = false;
                    ContentAutorizaPuestoRequisicion.Visible = false;
                    ContentCuestionario.Visible = false;
                    pe.FolioProceso = vFolioProceso.Value;

                    if (pe.ObtenerInformacionProceso(vClProceso))
                    {
                        txtEvaluadorCuestionario.Text = pe.UsuarioProceso;
                        txtPeriodoCapacitacion.Text = pe.NombreProceso;
                    }
                    txtEvaluadorCuestionario.ReadOnly = true;
                    txtPeriodoCapacitacion.ReadOnly = true;

                    break;

                case "CLIMALABORAL":
                    ContentAutorizaDocumentos.Visible = false;
                    ContentLogin.Visible = false;
                    ContentPasswordRecovery.Visible = false;
                    ContentCodigoConfirmacion.Visible = false;
                    ContentEvaluacionResultados.Visible = false;
                    ContentCuestionarios.Visible = false;
                    ContentClimaLaboral.Visible = true;
                    ContentCartera.Visible = false;
                    ContentEntrevista.Visible = false;
                    ContentAutorizaRequisicion.Visible = false;
                    ContentEvaluacionDesempeno.Visible = false;
                    ContentAutorizaPuestoRequisicion.Visible = false;
                    ContentCuestionario.Visible = false;
                    pe.FolioProceso = vFolioProceso.Value;

                    if (pe.ObtenerInformacionProceso(vClProceso))
                    {
                        //txtEvaluadorClima.Text = pe.UsuarioProceso;
                        txtPeriodoClima.Text = pe.NombreProceso;
                    }
                    txtPeriodoClima.ReadOnly = true;

                    break;

                case "ACTUALIZACIONCARTERA":
                    ContentAutorizaDocumentos.Visible = false;
                    ContentLogin.Visible = false;
                    ContentPasswordRecovery.Visible = false;
                    ContentCodigoConfirmacion.Visible = false;
                    ContentEvaluacionResultados.Visible = false;
                    ContentCuestionarios.Visible = false;
                    ContentClimaLaboral.Visible = false;
                    ContentCartera.Visible = true;
                    ContentEntrevista.Visible = false;
                    ContentAutorizaRequisicion.Visible = false;
                    ContentEvaluacionDesempeno.Visible = false;
                    ContentAutorizaPuestoRequisicion.Visible = false;
                    ContentCuestionario.Visible = false;
                    pe.FolioProceso = vFolioProceso.Value;

                    break;

                case "ENTREVISTA_SELECCION":
                    ContentAutorizaDocumentos.Visible = false;
                    ContentLogin.Visible = false;
                    ContentPasswordRecovery.Visible = false;
                    ContentCodigoConfirmacion.Visible = false;
                    ContentEvaluacionResultados.Visible = false;
                    ContentCuestionarios.Visible = false;
                    ContentClimaLaboral.Visible = false;
                    ContentCartera.Visible = false;
                    ContentEntrevista.Visible = true;
                    ContentAutorizaRequisicion.Visible = false;
                    ContentEvaluacionDesempeno.Visible = false;
                    ContentAutorizaPuestoRequisicion.Visible = false;
                    ContentCuestionario.Visible = false;
                    pe.FolioProceso = vFolioProceso.Value;

                    if (pe.ObtenerInformacionProceso(vClProceso))
                    {
                        txtEntrevistador.Text = pe.UsuarioProceso;
                    }
                    txtEntrevistador.ReadOnly = true;

                    break;

                case "NOTIFICACIONRRHH":
                    ContentAutorizaDocumentos.Visible = false;
                    ContentLogin.Visible = false;
                    ContentPasswordRecovery.Visible = false;
                    ContentCodigoConfirmacion.Visible = false;
                    ContentEvaluacionResultados.Visible = false;
                    ContentCuestionarios.Visible = false;
                    ContentClimaLaboral.Visible = false;
                    ContentCartera.Visible = false;
                    ContentEntrevista.Visible = false;
                    ContentRequisiciones.Visible = true;
                    ContentAutorizaRequisicion.Visible = false;
                    ContentEvaluacionDesempeno.Visible = false;
                    ContentAutorizaPuestoRequisicion.Visible = false;
                    ContentCuestionario.Visible = false;
                    pe.FolioProceso = vFolioProceso.Value;

                    if (pe.ObtenerInformacionProceso(vClProceso))
                    {
                        txtNotificacion.Text = pe.FlRequisicion;
                        txtPuesto.Text = pe.NombreProceso;
                    }
                    txtNotificacion.ReadOnly = true;
                    txtPuesto.ReadOnly = true;

                    break;
                case "AUTORIZAREQUISICION":
                    ContentAutorizaDocumentos.Visible = false;
                    ContentLogin.Visible = false;
                    ContentPasswordRecovery.Visible = false;
                    ContentCodigoConfirmacion.Visible = false;
                    ContentEvaluacionResultados.Visible = false;
                    ContentCuestionarios.Visible = false;
                    ContentClimaLaboral.Visible = false;
                    ContentCartera.Visible = false;
                    ContentEntrevista.Visible = false;
                    ContentRequisiciones.Visible = false;
                    ContentAutorizaRequisicion.Visible = true;
                    ContentEvaluacionDesempeno.Visible = false;
                    ContentAutorizaPuestoRequisicion.Visible = false;
                    ContentCuestionario.Visible = false;
                    pe.FolioProceso = vFolioProceso.Value;

                    if (pe.ObtenerInformacionProceso(vClProceso))
                    {
                        rtbAutRequisicion.Text = pe.FlRequisicion;
                        rtbPuesto.Text = pe.NombreProceso;
                    }
                    rtbAutRequisicion.ReadOnly = true;
                    rtbPuesto.ReadOnly = true;

                    break;

                case "AUTORIZAREQPUESTO":
                    ContentAutorizaDocumentos.Visible = false;
                    ContentLogin.Visible = false;
                    ContentPasswordRecovery.Visible = false;
                    ContentCodigoConfirmacion.Visible = false;
                    ContentEvaluacionResultados.Visible = false;
                    ContentCuestionarios.Visible = false;
                    ContentClimaLaboral.Visible = false;
                    ContentCartera.Visible = false;
                    ContentEntrevista.Visible = false;
                    ContentRequisiciones.Visible = false;
                    ContentAutorizaRequisicion.Visible = false;
                    ContentEvaluacionDesempeno.Visible = false;
                    ContentAutorizaPuestoRequisicion.Visible = true;
                    ContentCuestionario.Visible = false;
                    pe.FolioProceso = vFolioProceso.Value;

                    if (pe.ObtenerInformacionProceso(vClProceso))
                    {
                        txtAPRequisicion.Text = pe.FlRequisicion;
                        txtAPPuesto.Text = pe.NombreProceso;
                    }
                    txtAPRequisicion.ReadOnly = true;
                    txtAPPuesto.ReadOnly = true;

                    break;

                case "DESEMPENO":
                    ContentAutorizaDocumentos.Visible = false;
                    ContentLogin.Visible = false;
                    ContentPasswordRecovery.Visible = false;
                    ContentCodigoConfirmacion.Visible = false;
                    ContentEvaluacionResultados.Visible = false;
                    ContentCuestionarios.Visible = false;
                    ContentClimaLaboral.Visible = false;
                    ContentCartera.Visible = false;
                    ContentEntrevista.Visible = false;
                    ContentRequisiciones.Visible = false;
                    ContentAutorizaRequisicion.Visible = false;
                    ContentEvaluacionDesempeno.Visible = true;
                    ContentCuestionario.Visible = false;

                    pe.FolioProceso = vFolioProceso.Value;

                    if (pe.ObtenerInformacionProceso(vClProceso))
                    {
                        txtEvaluadorDesempeno.Text = pe.UsuarioProceso;
                        txtPeriodoDesempeno.Text = pe.NombreProceso;
                    }
                    txtEvaluadorDesempeno.ReadOnly = true;
                    txtPeriodoDesempeno.ReadOnly = true;

                    break;

                case "CUESTIONARIO":
                    ContentAutorizaDocumentos.Visible = false;
                    ContentLogin.Visible = false;
                    ContentPasswordRecovery.Visible = false;
                    ContentCodigoConfirmacion.Visible = false;
                    ContentEvaluacionResultados.Visible = false;
                    ContentCuestionarios.Visible = false;
                    ContentClimaLaboral.Visible = false;
                    ContentCartera.Visible = false;
                    ContentEntrevista.Visible = false;
                    ContentRequisiciones.Visible = false;
                    ContentAutorizaRequisicion.Visible = false;
                    ContentEvaluacionDesempeno.Visible = false;
                    ContentCuestionario.Visible = true;

                    pe.FolioProceso = vFolioProceso.Value;

                    if (pe.ObtenerInformacionProceso(vClProceso))
                    {
                        txtEvaluadorCuestionarioInd.Text = pe.UsuarioProceso;
                        txtPeriodoCuestionarioInd.Text = pe.NombreProceso;
                    }

                    txtEvaluadorCuestionarioInd.ReadOnly = true;
                    txtPeriodoCuestionarioInd.ReadOnly = true;

                    break;

                case "ENVIOSOLICITUDPLANTILLA":
                    if (vFolioProceso != null)
                    {
                        PlantillaFormularioNegocio nPlantilla = new PlantillaFormularioNegocio();
                        var vLstPlanilla = nPlantilla.ObtienePlantillas(pFlPlantillaSolicitud: vFolioProceso).FirstOrDefault();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "OpenWindowPlantilla(" + vLstPlanilla.ID_PLANTILLA_SOLICITUD.ToString() + ")", true);
                    }

                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Metodo Licenciamiento

        public void FgLicenciaActiva()
        {
            //Licencia
            SPE_OBTIENE_S_CONFIGURACION_Result vConfiguracionLicencia = new SPE_OBTIENE_S_CONFIGURACION_Result();
            LicenciaNegocio oConfiguracion = new LicenciaNegocio();
            UtilLicencias nLicencia = new UtilLicencias();

            vConfiguracionLicencia = oConfiguracion.obtieneConfiguracionGeneral();
            if (vConfiguracionLicencia.CL_LICENCIAMIENTO == null)
            {
                nLicencia.generaXmlLicencias(vConfiguracionLicencia.CL_CLIENTE, vConfiguracionLicencia.CL_PASS_WS, "Web Service", "Web Service");
                nLicencia.insertaXmlIdentificacion(vConfiguracionLicencia.CL_CLIENTE, vConfiguracionLicencia.CL_PASS_WS, "Web Service", "Web Service");

                vConfiguracionLicencia = oConfiguracion.obtieneConfiguracionGeneral();
            }

            if (vConfiguracionLicencia.CL_LICENCIAMIENTO != null)
            {
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
                XElement XmlConfiguracionLicencia = null;
                XElement XmlConfiguracionCliente = null;

                if (cadenaDesencriptadaLic != null && cadenaDesencriptadaObj != null)
                {
                    XmlConfiguracionLicencia = XElement.Parse(cadenaDesencriptadaLic);
                    XmlConfiguracionCliente = XElement.Parse(cadenaDesencriptadaObj);

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
                    ContextoApp.EO.LicenciaRDP.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "RDP", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);
                    ContextoApp.EO.LicenciaCL.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "CL", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);

                    ContextoApp.MPC.LicenciaMetodologia.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "MPC", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);
                    ContextoApp.RP.LicenciaReportes.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "RP", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);
                    ContextoApp.CI.LicenciaConsultasInteligentes.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "CI", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);
                    ContextoApp.PDE.LicenciaPuntoEncuentro.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "PDE", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);
                    ContextoApp.ANOM.LicenciaAccesoModulo.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "NOMINA", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);

                    if (lstLicencia.Count > 0)
                    {
                        ContextoApp.InfoEmpresa.Volumen = int.Parse(lstLicencia.FirstOrDefault().NO_VOLUMEN);

                        if (ContextoApp.IDP.LicenciaIntegracion.MsgActivo == "1" || ContextoApp.FYD.LicenciaFormacion.MsgActivo == "1" || ContextoApp.EO.LicenciaCL.MsgActivo == "1" || ContextoApp.EO.LicenciaED.MsgActivo == "1"
                 || ContextoApp.EO.LicenciaRDP.MsgActivo == "1" || ContextoApp.MPC.LicenciaMetodologia.MsgActivo == "1" || ContextoApp.RP.LicenciaReportes.MsgActivo == "1" || ContextoApp.CI.LicenciaConsultasInteligentes.MsgActivo == "1"
                   || ContextoApp.PDE.LicenciaPuntoEncuentro.MsgActivo == "1" || ContextoApp.ANOM.LicenciaAccesoModulo.MsgActivo == "1")
                        {
                            ContextoApp.InfoEmpresa.MsgSistema = "1";
                        }
                        else
                        {
                            ContextoApp.InfoEmpresa.MsgSistema = "El cliente actual no cuenta con licencias.";
                        }

                    }
                }
            }
            else
            {
                ContextoApp.InfoEmpresa.MsgSistema = "El cliente actual no cuenta con licencias.";
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (Request.QueryString["ClProceso"] != null)
                {
                    pe = new ProcesoExterno();
                    //En esta parte tiene que procesar los datos para el proceso externo. Vamos a obtener el proceso, el Guid, en base al proceso es el div que se mostrara.
                    //Se hara una funcion para mostrar los divs

                    vClProceso = Request.QueryString["ClProceso"];
                    vFolioProceso = Guid.Parse(Request.QueryString["FlProceso"]);
                }


                if ((Request.QueryString["AUTORIZA"]) != null)
                {
                    ptipo = (string)(Request.QueryString["AUTORIZA"]);
                    pFolioAutorizacion = Guid.Parse((Request.QueryString["TOKEN"]));
                    DocumentoAutorizarNegocio nDocumento = new DocumentoAutorizarNegocio();
                    var DocumentoAutorizar = nDocumento.ObtieneEmpleadoDocumentoAutorizacion(pFlAutorizacion: pFolioAutorizacion).Where(w => w.ID_PERIODO != null).FirstOrDefault();

                    if (DocumentoAutorizar != null)
                    {
                        if (DocumentoAutorizar.ID_PROGRAMA != null)
                        {
                            txtProgramaCapacitacion.Text = DocumentoAutorizar.NB_PROGRAMA.ToString();
                            vIdPrograma = DocumentoAutorizar.ID_PROGRAMA;
                            txtProgramaCapacitacion.ReadOnly = true;
                            txtAutorizador.ReadOnly = true;
                        }
                        else
                        {
                            txtProgramaCapacitacion.Text = DocumentoAutorizar.NB_PERIODO;
                            vIdPeriodo = DocumentoAutorizar.ID_PERIODO;
                            txtProgramaCapacitacion.ReadOnly = true;
                            txtAutorizador.ReadOnly = true;

                        }

                        //txtAutorizador.Text = DocumentoAutorizar.NB_EMPLEADO_ELABORA.ToString();
                        txtAutorizador.Text = DocumentoAutorizar.NB_EMPLEADO.ToString();
                        pClaveToken = DocumentoAutorizar.CL_TOKEN.ToString();

                    }
                    ContentAutorizaDocumentos.Visible = true;
                    ContentLogin.Visible = false;
                    ContentPasswordRecovery.Visible = false;
                    ContentCodigoConfirmacion.Visible = false;
                    ContentEvaluacionResultados.Visible = false;
                    ContentCuestionarios.Visible = false;
                    ContentClimaLaboral.Visible = false;
                    ContentCartera.Visible = false;
                    ContentEntrevista.Visible = false;
                    ContentRequisiciones.Visible = false;
                    ContentAutorizaRequisicion.Visible = false;
                    ContentEvaluacionDesempeno.Visible = false;
                    ContentAutorizaPuestoRequisicion.Visible = false;
                    ContentCuestionario.Visible = false;
                }
                else
                {
                    ContentPasswordRecovery.Visible = false;
                    ContentCodigoConfirmacion.Visible = false;
                    ContentAutorizaDocumentos.Visible = false;
                    ContentEvaluacionResultados.Visible = false;
                    ContentCuestionarios.Visible = false;
                    ContentClimaLaboral.Visible = false;
                    ContentCartera.Visible = false;
                    ContentEntrevista.Visible = false;
                    ContentRequisiciones.Visible = false;
                    ContentAutorizaRequisicion.Visible = false;
                    ContentEvaluacionDesempeno.Visible = false;
                    ContentAutorizaPuestoRequisicion.Visible = false;
                    ContentCuestionario.Visible = false;
                }

                mostrarDiv();
                FgLicenciaActiva();
            }

            PlantillaFormularioNegocio nPlantilla = new PlantillaFormularioNegocio();
            List<SPE_OBTIENE_C_PLANTILLAS_EXTERNAS_Result> vLstPlantillas = nPlantilla.ObtenerPlantillasExternas("EXTERIOR");
            foreach (var item in vLstPlantillas)
            {
                HtmlGenericControl viSolicitud = new HtmlGenericControl("i");
                viSolicitud.Attributes.Add("class", "fa fa-file-text");
                HtmlGenericControl vControlSolicitud = new HtmlGenericControl("a");
                vControlSolicitud.Attributes.Add("href", "#");
                vControlSolicitud.Attributes.Add("onclick", "return OpenWindowPlantilla(" + item.ID_PLANTILLA_SOLICITUD.ToString() + ");");
                vControlSolicitud.Attributes.Add("style", "margin: 20px;");
                vControlSolicitud.InnerText = item.NB_PLANTILLA_SOLICITUD;
                vControlSolicitud.Style.Add("font-size", "14px");
                vControlSolicitud.Style.Add(" font-family", "sans-serif");
                viSolicitud.Controls.Add(vControlSolicitud);
                dvSolicitudes.Controls.Add(viSolicitud);
            }
            if (vLstPlantillas.Count > 1)
            {
                int vTamanoDiv = vLstPlantillas.Count * 35;
                dvSolicitudes.Style.Add("height", vTamanoDiv.ToString() + "px");
            }

            lblEmpresa.InnerText = ContextoApp.InfoEmpresa.NbEmpresa;
            rbiLogoOrganizacion1.DataValue = ContextoApp.InfoEmpresa.FiLogotipo.FiArchivo;
            rbiLogoOrganizacion2.DataValue = ContextoApp.InfoEmpresa.FiLogotipo.FiArchivo;
            rbiLogoOrganizacion3.DataValue = ContextoApp.InfoEmpresa.FiLogotipo.FiArchivo;

            //Licencia
            if (ContextoApp.InfoEmpresa.MsgSistema != "1")
            {
                btnLogin.Enabled = false;
                btnRecuperarPassword.Enabled = false;
                btnAbrirCuestionario.Enabled = false;
                btnAbrirCuestionarioClima.Enabled = false;
                btnAbrirCuestionarioDesempeno.Enabled = false;
                btnAbrirCuestionarioIndependiente.Enabled = false;
                btnAbrirEntrevista.Enabled = false;
                btnAbrirEvaluacion.Enabled = false;
                btnAbrirRequisicion.Enabled = false;
                btnAutorizarReqPuesto.Enabled = false;
                btnCartera.Enabled = false;
                btnConfirmarCodigo.Enabled = false;
                btnEnviarPorCuenta.Enabled = false;
                btnIntroducirCodigo.Enabled = false;
                btnSiguiente.Enabled = false;
                UtilMensajes.MensajeResultadoDB(RadWindowManager1, ContextoApp.InfoEmpresa.MsgSistema, E_TIPO_RESPUESTA_DB.WARNING);
            }
            // Fin de licencia
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (ContextoApp.InfoEmpresa.MsgSistema == "1")
            {
                UsuarioNegocio nUsuario = new UsuarioNegocio();
                
                E_USUARIO vUsuario = nUsuario.AutenticaUsuario(txtUsuario.Value, txtPassword.Value);
                
                if (vUsuario.FG_ACTIVO)
                {
                    E_USUARIO UsuarioSys = nUsuario.ObtieneUsuarioCambioPassword(txtUsuario.Value);
                    if (vUsuario.oFunciones != null)
                    {
                        //Se agrega la clave del usuario al FormsAuthenticationTicket como user data, este dato se usara en nómina en el global.asax
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(TICKET_VERSION, FormsAuthentication.FormsCookieName, DateTime.Now, DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes), false, vUsuario.CL_USUARIO, FormsAuthentication.FormsCookiePath);//FormsAuthentication.Timeout.TotalMinutes
                        string cookie = FormsAuthentication.Encrypt(ticket);
                        Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, cookie));
                        Session["UniqueUserId"] = Guid.NewGuid();
                        ContextoUsuario.oUsuario = vUsuario;
                        var rol = ContextoUsuario.oUsuario.oRol.NB_ROL;
                        //Determinar si solo tiene la funcion de PDE enviar a PDE                    
                        if (vUsuario.oFunciones.Where(x => x.CL_FUNCION.Substring(0, 1) != "R" && x.CL_FUNCION != "I" && x.CL_FUNCION != "H" && x.CL_FUNCION.Substring(0, 1) != "S").Count() == 0)
                        //x.CL_FUNCION.Substring(0, 1) != "P" && x.CL_FUNCION != "Q" && x.CL_FUNCION != "E" && x.CL_FUNCION.Substring(0,1) != "R").Count() == 0)
                        {
                            if (UsuarioSys.FG_CAMBIAR_PASSWORD != true)
                            {
                                Response.Redirect("~/PDE/CambiarPassword.aspx");
                            }
                            else
                            {
                                Response.Redirect("~/PDE/Default.aspx");
                            }
                        }
                        else
                        {
                            Response.Redirect("Default.aspx");
                        }
                    }
                    else
                    {
                        UtilMensajes.MensajeResultadoDB(RadWindowManager1, "El rol del usuario no tiene permisos.", E_TIPO_RESPUESTA_DB.WARNING);
                    }
                }
                else
                {
                    UtilMensajes.MensajeResultadoDB(RadWindowManager1, "El usuario o la contraseña son incorrectos.", E_TIPO_RESPUESTA_DB.WARNING);
                }
            }
            else
            {
                btnLogin.Enabled = false;
                btnRecuperarPassword.Enabled = false;
                UtilMensajes.MensajeResultadoDB(RadWindowManager1, ContextoApp.InfoEmpresa.MsgSistema, E_TIPO_RESPUESTA_DB.WARNING);
            }
        }

        protected void btnRecuperarPassword_Click(object sender, EventArgs e)
        {
            ContentLogin.Visible = false;
            ContentPasswordRecovery.Visible = true;

            PlantillaFormularioNegocio nPlantilla = new PlantillaFormularioNegocio();
            List<SPE_OBTIENE_C_PLANTILLAS_EXTERNAS_Result> vLstPlantillas = nPlantilla.ObtenerPlantillasExternas("EXTERIOR");
            foreach (var item in vLstPlantillas)
            {
                HtmlGenericControl viSolicitud = new HtmlGenericControl("i");
                viSolicitud.Attributes.Add("class", "fa fa-file-text");
                HtmlGenericControl vControlSolicitud = new HtmlGenericControl("a");
                vControlSolicitud.Attributes.Add("href", "#");
                vControlSolicitud.Attributes.Add("onclick", "return OpenWindowPlantilla(" + item.ID_PLANTILLA_SOLICITUD.ToString() + ");");
                vControlSolicitud.Attributes.Add("style", "margin: 20px;");
                vControlSolicitud.InnerText = item.NB_PLANTILLA_SOLICITUD;
                vControlSolicitud.Style.Add("font-size", "14px");
                vControlSolicitud.Style.Add(" font-family", "sans-serif");
                viSolicitud.Controls.Add(vControlSolicitud);
                dvRecuperaPass.Controls.Add(viSolicitud);
            }
            if (vLstPlantillas.Count > 1)
            {
                int vTamanoDiv = vLstPlantillas.Count * 35;
                dvRecuperaPass.Style.Add("height", vTamanoDiv.ToString() + "px");
            }
        }

        protected void btnEnviarPorCuenta_Click(object sender, EventArgs e)
        {
            SolicitarCambioPassword();
        }

        protected void SolicitarCambioPassword()
        {
            string vToken = Membership.GeneratePassword(12, 1);

            E_USUARIO vUsuario = new E_USUARIO();

            if (rtsRecuperarPassword.SelectedIndex == 0)
                vUsuario.CL_USUARIO = txtRecuperarCuenta.Text;
            else
                vUsuario.NB_CORREO_ELECTRONICO = txtRecuperarCuenta.Text;

            vUsuario.CL_CAMBIAR_PASSWORD = vToken;

            UsuarioNegocio nUsuario = new UsuarioNegocio();

            E_RESULTADO vResultado = nUsuario.CambiaPassword(vUsuario, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(RadWindowManager1, vMensaje, vResultado.CL_TIPO_ERROR, 300, 200, null);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
            {
                ContentLogin.Visible = false;
                ContentPasswordRecovery.Visible = false;
                ContentCodigoConfirmacion.Visible = true;
            }
        }

        protected void btnConfirmarCodigo_Click(object sender, EventArgs e)
        {

            if (!String.IsNullOrWhiteSpace(txtNbPassword.Text) && txtNbPassword.Text.Equals(txtNbPasswordConfirm.Text))
            {
                E_USUARIO vUsuario = new E_USUARIO()
                {
                    CL_CAMBIAR_PASSWORD = txtCodigo.Text,
                    NB_PASSWORD = txtNbPassword.Text
                };

                UsuarioNegocio nUsuario = new UsuarioNegocio();

                E_RESULTADO vResultado = nUsuario.CambiaPassword(vUsuario, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(RadWindowManager1, vMensaje, vResultado.CL_TIPO_ERROR, 300, 180, null);

                ContentLogin.Visible = true;
                ContentPasswordRecovery.Visible = false;
                ContentCodigoConfirmacion.Visible = false;
            }
            else
                UtilMensajes.MensajeResultadoDB(RadWindowManager1, "Las contraseñas no coinciden.", E_TIPO_RESPUESTA_DB.WARNING, 300, 180, null);
        }

        protected void btnIntroducirCodigo_Click(object sender, EventArgs e)
        {
            ContentLogin.Visible = false;
            ContentPasswordRecovery.Visible = false;
            ContentCodigoConfirmacion.Visible = true;
            PlantillaFormularioNegocio nPlantilla = new PlantillaFormularioNegocio();
            List<SPE_OBTIENE_C_PLANTILLAS_EXTERNAS_Result> vLstPlantillas = nPlantilla.ObtenerPlantillasExternas("EXTERIOR");
            foreach (var item in vLstPlantillas)
            {
                HtmlGenericControl viSolicitud = new HtmlGenericControl("i");
                viSolicitud.Attributes.Add("class", "fa fa-file-text");
                HtmlGenericControl vControlSolicitud = new HtmlGenericControl("a");
                vControlSolicitud.Attributes.Add("href", "#");
                vControlSolicitud.Attributes.Add("onclick", "return OpenWindowPlantilla(" + item.ID_PLANTILLA_SOLICITUD.ToString() + ");");
                vControlSolicitud.Attributes.Add("style", "margin: 20px;");
                vControlSolicitud.InnerText = item.NB_PLANTILLA_SOLICITUD;
                vControlSolicitud.Style.Add("font-size", "14px");
                vControlSolicitud.Style.Add(" font-family", "sans-serif");
                viSolicitud.Controls.Add(vControlSolicitud);
                dvConfirmaCodigo.Controls.Add(viSolicitud);
            }
            if (vLstPlantillas.Count > 1)
            {
                int vTamanoDiv = vLstPlantillas.Count * 35;
                dvConfirmaCodigo.Style.Add("height", vTamanoDiv.ToString() + "px");
            }
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (ptipo.Equals("PROGRAMACAPACITACION"))
            {
                if (!pClaveToken.Equals(""))
                {
                    if (vIdPrograma != null)
                    {

                        if (txtClave.Text.ToString().Equals(pClaveToken))
                        {
                            ProgramaNegocio nPrograma = new ProgramaNegocio();
                            var vprogramaCapacitacion = nPrograma.ObtieneProgramasCapacitacion(pIdPrograma: vIdPrograma).FirstOrDefault();
                            if (vprogramaCapacitacion != null)
                            {

                                DocumentoAutorizarNegocio nDocumento = new DocumentoAutorizarNegocio();

                                SPE_OBTIENE_C_AUTORIZACION_DCTO_EMPLEADO_Result vDocumento = new SPE_OBTIENE_C_AUTORIZACION_DCTO_EMPLEADO_Result();

                                vDocumento = nDocumento.ObtieneEmpleadoDocumentoAutorizacion(pFlAutorizacion: pFolioAutorizacion).FirstOrDefault();

                                if (vDocumento != null)
                                {
                                    if (vDocumento.CL_ESTADO.ToUpper().Equals("POR AUTORIZAR"))
                                    {
                                        Response.Redirect("~/FYD/EvaluacionCompetencia/AutorizaProgramaCapacitacion.aspx?ID=" + vIdPrograma + "&TOKEN=" + pFolioAutorizacion);
                                    }
                                    else
                                    {
                                        UtilMensajes.MensajeResultadoDB(RadWindowManager1, "El documento ya fue autorizado o rechazado y no se puede modificar.", E_TIPO_RESPUESTA_DB.ERROR);
                                    }

                                }
                                else
                                {
                                    UtilMensajes.MensajeResultadoDB(RadWindowManager1, "El documento no existe.", E_TIPO_RESPUESTA_DB.ERROR);
                                }
                            }
                        }
                        else
                        {
                            UtilMensajes.MensajeResultadoDB(RadWindowManager1, "Las contraseñas no coinciden.", E_TIPO_RESPUESTA_DB.ERROR);
                        }
                    }

                    if (vIdPeriodo != null)
                    {
                        if (txtClave.Text.ToString().Equals(pClaveToken))
                        {
                            SIGE.Negocio.FormacionDesarrollo.PeriodoNegocio nPrograma = new SIGE.Negocio.FormacionDesarrollo.PeriodoNegocio();
                            DocumentoAutorizarNegocio nDocumento = new DocumentoAutorizarNegocio();
                            var DocumentoAutorizar = nDocumento.ObtieneEmpleadoDocumentoAutorizacion(pFlAutorizacion: pFolioAutorizacion).FirstOrDefault();
                            var vPeriodo = nPrograma.ObtienePeriodoEvaluacion(vIdPeriodo.Value);
                            if (vPeriodo != null)
                            {
                                if (DocumentoAutorizar.CL_ESTADO == "Autorizado")
                                    UtilMensajes.MensajeResultadoDB(RadWindowManager1, "Ya se ha capturado una respuesta a esta autorización.", E_TIPO_RESPUESTA_DB.ERROR);
                                else
                                    Response.Redirect("~/FYD/EvaluacionCompetencia/AutorizarPeriodoEvaluacion.aspx?IdPeriodo=" + vIdPeriodo + "&TOKEN=" + pFolioAutorizacion);
                            }
                        }
                        else
                        {
                            UtilMensajes.MensajeResultadoDB(RadWindowManager1, "Las contraseñas no coinciden.", E_TIPO_RESPUESTA_DB.ERROR);
                        }
                    }
                }
            }
        }

        protected void btnAbrirEvaluacion_Click(object sender, EventArgs e)
        {
            pe.ContraseñaUsuario = txtContraseñaEvaluacion.Text;
            if (pe.EjecutarProceso("EVALUACION"))
            {
                Response.Redirect(pe.Url);
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(RadWindowManager1, pe.MensajeError, E_TIPO_RESPUESTA_DB.ERROR);
            }
        }

        protected void btnAbrirCuestionario_Click(object sender, EventArgs e)
        {
            pe.ContraseñaUsuario = txtContraseñaCuestionario.Text;

            if (pe.EjecutarProceso("CUESTIONARIOS"))
            {
                Response.Redirect(pe.Url);
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(RadWindowManager1, pe.MensajeError, E_TIPO_RESPUESTA_DB.ERROR);
            }

        }

        protected void btnAbrirCuestionarioClima_Click(object sender, EventArgs e)
        {
            pe.ContraseñaUsuario = txtContraseniaClima.Text;

            if (pe.EjecutarProceso("CLIMALABORAL"))
            {
                Response.Redirect(pe.Url);
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(RadWindowManager1, pe.MensajeError, E_TIPO_RESPUESTA_DB.ERROR);
            }

        }

        protected void btnCartera_Click(object sender, EventArgs e)
        {
            //OBTENER CONTRASEÑA DE LA BD PARA COMPARAR
            SolicitudNegocio nSolicitud = new SolicitudNegocio();
            var solicitudes = nSolicitud.Obtener_SOLICITUDES_CARTERA();
            string pass = string.Empty;
            int flSolicitud = 0;
            if (solicitudes != null)
            {
                var solicitud = solicitudes.Where(x => x.CL_ACCESO_CARTERA == pe.FolioProceso).FirstOrDefault();
                if (solicitud != null)
                    pass = solicitud.CL_CONTRASENA_CARTERA;
                flSolicitud = solicitud.ID_SOLICITUD;
            }
            if (pass.Equals(txtContrasenaCartera.Text) && !string.IsNullOrEmpty(pass) && flSolicitud != 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "OpenWindow(" + flSolicitud.ToString() + ")", true);
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(RadWindowManager1, "La contraseña no es correcta. Revise por favor.", E_TIPO_RESPUESTA_DB.ERROR);
            }
        }

        protected void btnAbrirEntrevista_Click(object sender, EventArgs e)
        {
            pe.ContraseñaUsuario = txtContraseñaEntrevista.Text;

            if (pe.EjecutarProceso("ENTREVISTA_SELECCION"))
            {
                Response.Redirect(pe.Url);
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(RadWindowManager1, pe.MensajeError, E_TIPO_RESPUESTA_DB.ERROR);
            }
        }

        protected void btnAbrirRequisicion_Click(object sender, EventArgs e)
        {
            pe.ContraseñaUsuario = txtContraseñaNotificacion.Text;

            if (pe.EjecutarProceso("NOTIFICACIONRRHH"))
            {
                Response.Redirect(pe.Url);
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(RadWindowManager1, pe.MensajeError, E_TIPO_RESPUESTA_DB.ERROR);
            }

        }

        protected void IngresarAutorizar_Click(object sender, EventArgs e)
        {

            pe.ContraseñaUsuario = rtbAutoContrasena.Text;

            if (pe.EjecutarProceso("AUTORIZAREQUISICION"))
            {
                Response.Redirect(pe.Url);
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(RadWindowManager1, pe.MensajeError, E_TIPO_RESPUESTA_DB.ERROR);
            }


        }

        protected void btnAbrirCuestionarioDesempeno_Click(object sender, EventArgs e)
        {

            pe.ContraseñaUsuario = txtContrasenaDesempeno.Text;

            if (pe.EjecutarProceso("DESEMPENO"))
            {
                Response.Redirect(pe.Url);
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(RadWindowManager1, pe.MensajeError, E_TIPO_RESPUESTA_DB.ERROR);
            }

        }

        protected void btnAutorizarReqPuesto_Click(object sender, EventArgs e)
        {
            pe.ContraseñaUsuario = txtAPContraseña.Text;

            if (pe.EjecutarProceso("AUTORIZAREQPUESTO"))
            {
                Response.Redirect(pe.Url);
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(RadWindowManager1, pe.MensajeError, E_TIPO_RESPUESTA_DB.ERROR);
            }
        }

        protected void btnAbrirCuestionarioIndependiente_Click(object sender, EventArgs e)
        {
            pe.ContraseñaUsuario = txtPassCuestionarioInd.Text;

            if (pe.EjecutarProceso("CUESTIONARIO"))
            {
                Response.Redirect(pe.Url);
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(RadWindowManager1, pe.MensajeError, E_TIPO_RESPUESTA_DB.ERROR);
            }
        }
    }
}