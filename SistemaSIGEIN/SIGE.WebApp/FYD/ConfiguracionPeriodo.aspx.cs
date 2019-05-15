using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
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
using Telerik.Web.UI;
using WebApp.Comunes;

namespace SIGE.WebApp.FYD
{
    public partial class ConfiguracionPeriodo : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdRol;
        //private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private string vNbFirstRadEditorTagName = "p";
        //Variable para determinar el idioma
        public string vClIdioma = ContextoApp.clCultureIdioma;
        public bool vFgInterrelacionado
        {
            get { return (bool)ViewState["vs_vFgInterrelacionado"]; }
            set { ViewState["vs_vFgInterrelacionado"] = value; }
        }

       // public string vClCultureIdioma;


        //Variables con los texto de configuración idioma

        public string vOpenCompetenciasSelectionWindow
        {
            get { return (string)ViewState["vs_vOpenCompetenciasSelectionWindow"]; }
            set { ViewState["vs_vOpenCompetenciasSelectionWindow"] = value; }
        }

        public string vOpenEmpleadosSelectionWindow
        {
            get { return (string)ViewState["vs_vOpenEmpleadosSelectionWindow"]; }
            set { ViewState["vs_vOpenEmpleadosSelectionWindow"] = value; }
        }

        public string vOpenPuestoSelectionWindow
        {
            get { return (string)ViewState["vs_vOpenPuestoSelectionWindow"]; }
            set { ViewState["vs_vOpenPuestoSelectionWindow"] = value; }
        }

        public string vOpenAreaSelectionWindow
        {
            get { return (string)ViewState["vs_vOpenPuestoSelectionWindow"]; }
            set { ViewState["vs_vOpenPuestoSelectionWindow"] = value; }

        }

        public string vOpenOtrosEvaluadoresManualWindow
        {
            get { return (string)ViewState["vs_vOpenOtrosEvaluadoresManualWindow"]; }
            set { ViewState["vs_vOpenOtrosEvaluadoresManualWindow"] = value; }
        }

        public string vOpenOtrosEvaluadoresSelectionWindow
        {
            get { return (string)ViewState["vs_vOpenOtrosEvaluadoresSelectionWindow"]; }
            set { ViewState["vs_vOpenOtrosEvaluadoresSelectionWindow"] = value; }
        }

        public string vOpenCamposInterrelacionadosSelectionWindow
        {
            get { return (string)ViewState["vs_vOpenCamposInterrelacionadosSelectionWindow"]; }
            set { ViewState["vs_vOpenCamposInterrelacionadosSelectionWindow"] = value; }
        }

        public string vOpenCamposAdicionalesSelectionWindow
        {
            get { return (string)ViewState["vs_vOpenCamposAdicionalesSelectionWindow"]; }
            set { ViewState["vs_vOpenCamposAdicionalesSelectionWindow"] = value; }
        }

        public string vOpenAgregarCuestionarioSelectionWindow
        {
            get { return (string)ViewState["vs_vOpenAgregarCuestionarioSelectionWindow"]; }
            set { ViewState["vs_vOpenAgregarCuestionarioSelectionWindow"] = value; }
        }

        public string vOpenAgregarPreguntasAdicionalesWindow
        {
            get { return (string)ViewState["vs_vOpenAgregarPreguntasAdicionalesWindow"]; }
            set { ViewState["vs_vOpenAgregarPreguntasAdicionalesWindow"] = value; }
        }

        public string vOpenEditarPreguntasAdicionalesWindow
        {
            get { return (string)ViewState["vs_vOpenEditarPreguntasAdicionalesWindow"]; }
            set { ViewState["vs_vOpenEditarPreguntasAdicionalesWindow"] = value; }
        }

        public string vOpenEditarPreguntasAdicionalesWindow_alert
        {
            get { return (string)ViewState["vs_vOpenEditarPreguntasAdicionalesWindow_alert"]; }
            set { ViewState["vs_vOpenEditarPreguntasAdicionalesWindow_alert"] = value; }
        }

        public string vConfirmarEliminarPregunta
        {
            get { return (string)ViewState["vs_vConfirmarEliminarPregunta"]; }
            set { ViewState["vs_vConfirmarEliminarPregunta"] = value; }
        }

        public string vConfirmarEliminarPregunta_window
        {
            get { return (string)ViewState["vs_vConfirmarEliminarPregunta_window"]; }
            set { ViewState["vs_vConfirmarEliminarPregunta_window"] = value; }
        }

        public string vOpenEnvioCuestionariosWindow
        {
            get { return (string)ViewState["vs_vOpenEnvioCuestionariosWindow"]; }
            set { ViewState["vs_vOpenEnvioCuestionariosWindow"] = value; }
        }

        public string vOpenOtrosPuestosSelectionWindow
        {
            get { return (string)ViewState["vs_vOpenOtrosPuestosSelectionWindow"]; }
            set { ViewState["vs_vOpenOtrosPuestosSelectionWindow"] = value; }
        }

        public string vOpenOtrosPuestosSelectionWindow_alert
        {
            get { return (string)ViewState["vs_vOpenOtrosPuestosSelectionWindow_alert"]; }
            set { ViewState["vs_vOpenOtrosPuestosSelectionWindow_alert"] = value; }

        }
        public string vOpenMatrizEvaluadoresWindow
        {
            get { return (string)ViewState["vs_vOpenMatrizEvaluadoresWindow"]; }
            set { ViewState["vs_vOpenMatrizEvaluadoresWindow"] = value; }
        }

        public string vOpenMatrizEvaluadoresWindow_alert
        {
            get { return (string)ViewState["vs_vOpenMatrizEvaluadoresWindow_alert"]; }
            set { ViewState["vs_vOpenMatrizEvaluadoresWindow_alert"] = value; }
        }

        public string vOpenPlaneacionMatrizWindow
        {
            get { return (string)ViewState["vs_vOpenPlaneacionMatrizWindow"]; }
            set { ViewState["vs_vOpenPlaneacionMatrizWindow"] = value; }
        }

        public string vconfirmarEliminarEvaluados_1a
        {
            get { return (string)ViewState["vs_vconfirmarEliminarEvaluados_1a"]; }
            set { ViewState["vs_vconfirmarEliminarEvaluados_1a"] = value; }
        }

        public string vconfirmarEliminarEvaluados_1b
        {
            get { return (string)ViewState["vs_vconfirmarEliminarEvaluados_1b"]; }
            set { ViewState["vs_vconfirmarEliminarEvaluados_1b"] = value; }
        }

        public string vconfirmarEliminarEvaluados_2a
        {
            get { return (string)ViewState["vs_vconfirmarEliminarEvaluados_2a"]; }
            set { ViewState["vs_vconfirmarEliminarEvaluados_2a"] = value; }
        }

        public string vconfirmarEliminarEvaluados_2b
        {
            get { return (string)ViewState["vs_vconfirmarEliminarEvaluados_2b"]; }
            set { ViewState["vs_vconfirmarEliminarEvaluados_2b"] = value; }
        }

        public string vconfirmarEliminarEvaluados_alert
        {
            get { return (string)ViewState["vs_vconfirmarEliminarEvaluados_alert"]; }
            set { ViewState["vs_vconfirmarEliminarEvaluados_alert"] = value; }
        }

        public string vDeleteListItems
        {
            get { return (string)ViewState["vs_vDeleteListItems"]; }
            set { ViewState["vs_vDeleteListItems"] = value; }
        }

        public string vChangeControlState
        {
            get { return (string)ViewState["vs_vChangeControlState"]; }
            set { ViewState["vs_vChangeControlState"] = value; }
        }

        public string vOpenWindowAutorizarDocumento
        {
            get { return (string)ViewState["vs_vOpenWindowAutorizarDocumento"]; }
            set { ViewState["vs_vOpenWindowAutorizarDocumento"] = value; }

        }

        //*************************************************

        private int vIdPeriodoV
        {
            get { return (int)ViewState["vs_vIderiodo"]; }
            set { ViewState["vs_vIderiodo"] = value; }
        }

        public string vFgContestados
        {
            get { return (string)ViewState["vs_vFgContestados"]; }
            set { ViewState["vs_vFgContestados"] = value; }
        }

        public int vFgPuestosComparacion
        {
            get { return (int)ViewState["vs_cp_fg_puestos_comparacion"]; }
            set { ViewState["vs_cp_fg_puestos_comparacion"] = value; }
        }

        public E_PERIODO_EVALUACION vPeriodo
        {
            get { return (E_PERIODO_EVALUACION)ViewState["vs_vPeriodo"]; }
            set { ViewState["vs_vPeriodo"] = value; }
        }

        //decimal vPrAutoevaluacion;
        //decimal vPrSuperior;
        //decimal vPrSubordinado;
        //decimal vPrInterrelacionado;
        //decimal vPrOtros;
        //decimal vPrTotal;
        //decimal vPrGenericas;
        //decimal vPrEspecificas;
        //decimal vPrInstitucionales;
        //decimal vPrTotalCompetencias;

        public int ValidarPuestosComparacion()
        {
            int vResultado = 0;

            if (grdEvaluados.MasterTableView.Items.Count == 0)
            {
                vResultado = 0;
            }
            else
            {
                foreach (GridDataItem item in grdEvaluados.Items)
                {

                    if (item.OwnerTableView.Name == "Parent" & !item.HasChildItems)
                    {
                        vResultado++;
                    }
                }
            }

            vFgPuestosComparacion = vResultado;
            return vResultado;
        }

        protected void GenerarContrasena()
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();

            E_RESULTADO vResultado = nPeriodo.InsertarActualizarTokenEvaluadores(vPeriodo.ID_PERIODO ?? 0, null, vClUsuario, vNbPrograma, pIdRol: vIdRol);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            //UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                grdContrasenaEvaluadores.Rebind();
        }

        private void SeguridadProcesos(bool vFgCuestionariosCreados)
        {
           btnEnviarCuestionarios.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.A.A.H") && vFgCuestionariosCreados;
        }

        //Metodo de traducción
        protected void TraducirTextos()
        {

            //Asignar texto variables vista
            TraduccionIdiomaTextoNegocio oNegocio = new TraduccionIdiomaTextoNegocio();
            List<SPE_OBTIENE_TRADUCCION_TEXTO_Result> vLstTextosTraduccion = oNegocio.ObtieneTraduccion(pCL_MODULO: "FYD", pCL_PROCESO: "EC_CONFIGURACION", pCL_IDIOMA: "PORT");
            if (vLstTextosTraduccion.Count > 0)
            {

                //Asignar texto variables javascript
                vOpenCompetenciasSelectionWindow = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vrtsConfiguracionPeriodo_0").FirstOrDefault().DS_TEXTO;
                vOpenEmpleadosSelectionWindow = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vOpenEmpleadosSelectionWindow").FirstOrDefault().DS_TEXTO;
                vOpenPuestoSelectionWindow = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vOpenPuestoSelectionWindow").FirstOrDefault().DS_TEXTO;
                vOpenAreaSelectionWindow = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vOpenAreaSelectionWindow").FirstOrDefault().DS_TEXTO;
                vOpenOtrosEvaluadoresManualWindow = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vOpenOtrosEvaluadoresManualWindow").FirstOrDefault().DS_TEXTO;
                vOpenOtrosEvaluadoresSelectionWindow = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vOpenOtrosEvaluadoresSelectionWindow").FirstOrDefault().DS_TEXTO;
                vOpenCamposInterrelacionadosSelectionWindow = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vOpenCamposInterrelacionadosSelectionWindow").FirstOrDefault().DS_TEXTO;
                vOpenCamposAdicionalesSelectionWindow = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vOpenCamposAdicionalesSelectionWindow").FirstOrDefault().DS_TEXTO;
                vOpenAgregarCuestionarioSelectionWindow = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vOpenAgregarCuestionarioSelectionWindow").FirstOrDefault().DS_TEXTO;
                vOpenAgregarPreguntasAdicionalesWindow = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vOpenAgregarPreguntasAdicionalesWindow").FirstOrDefault().DS_TEXTO;
                vOpenEditarPreguntasAdicionalesWindow = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vOpenEditarPreguntasAdicionalesWindow").FirstOrDefault().DS_TEXTO;
                vOpenEditarPreguntasAdicionalesWindow_alert = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vOpenEditarPreguntasAdicionalesWindow_alert").FirstOrDefault().DS_TEXTO;
                vConfirmarEliminarPregunta = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vConfirmarEliminarPregunta").FirstOrDefault().DS_TEXTO;
                vConfirmarEliminarPregunta_window = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vConfirmarEliminarPregunta_window").FirstOrDefault().DS_TEXTO;
                vOpenEnvioCuestionariosWindow = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vOpenEnvioCuestionariosWindow").FirstOrDefault().DS_TEXTO;
                vOpenOtrosPuestosSelectionWindow = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vOpenOtrosPuestosSelectionWindow").FirstOrDefault().DS_TEXTO;
                vOpenOtrosPuestosSelectionWindow_alert = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vOpenOtrosPuestosSelectionWindow_alert").FirstOrDefault().DS_TEXTO;
                vOpenMatrizEvaluadoresWindow = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vOpenMatrizEvaluadoresWindow").FirstOrDefault().DS_TEXTO;
                vOpenMatrizEvaluadoresWindow_alert = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vOpenMatrizEvaluadoresWindow_alert").FirstOrDefault().DS_TEXTO;
                vOpenPlaneacionMatrizWindow = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vOpenPlaneacionMatrizWindow").FirstOrDefault().DS_TEXTO;
                vconfirmarEliminarEvaluados_1a = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vconfirmarEliminarEvaluados_1a").FirstOrDefault().DS_TEXTO;
                vconfirmarEliminarEvaluados_1b = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vconfirmarEliminarEvaluados_1b").FirstOrDefault().DS_TEXTO;
                vconfirmarEliminarEvaluados_2a = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vconfirmarEliminarEvaluados_2a").FirstOrDefault().DS_TEXTO;
                vconfirmarEliminarEvaluados_2b = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vconfirmarEliminarEvaluados_2b").FirstOrDefault().DS_TEXTO;
                vconfirmarEliminarEvaluados_alert = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vconfirmarEliminarEvaluados_alert").FirstOrDefault().DS_TEXTO;
                vDeleteListItems = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vDeleteListItems").FirstOrDefault().DS_TEXTO;
                vChangeControlState = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vChangeControlState").FirstOrDefault().DS_TEXTO;
                vOpenWindowAutorizarDocumento = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vOpenWindowAutorizarDocumento").FirstOrDefault().DS_TEXTO;

                //Asignar texto a RadSlidingZone
                rspAyudaCuestionario.Title = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vrspAyudaCuestionario_title").FirstOrDefault().DS_TEXTO;
                txtAyuda.InnerHtml = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vtxtAyuda").FirstOrDefault().DS_TEXTO;

                //Asignar texto a los RadTab del RadTabStrip
                rtsConfiguracionPeriodo.Tabs[0].Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vrtsConfiguracionPeriodo_0").FirstOrDefault().DS_TEXTO;
                rtsConfiguracionPeriodo.Tabs[1].Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vrtsConfiguracionPeriodo_1").FirstOrDefault().DS_TEXTO;
                rtsConfiguracionPeriodo.Tabs[2].Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vrtsConfiguracionPeriodo_2").FirstOrDefault().DS_TEXTO;
                rtsConfiguracionPeriodo.Tabs[3].Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vrtsConfiguracionPeriodo_3").FirstOrDefault().DS_TEXTO;
                rtsConfiguracionPeriodo.Tabs[4].Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vrtsConfiguracionPeriodo_4").FirstOrDefault().DS_TEXTO;
                rtsConfiguracionPeriodo.Tabs[5].Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vrtsConfiguracionPeriodo_5").FirstOrDefault().DS_TEXTO;
                rtsConfiguracionPeriodo.Tabs[6].Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vrtsConfiguracionPeriodo_6").FirstOrDefault().DS_TEXTO;
                rtsConfiguracionPeriodo.Tabs[7].Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vrtsConfiguracionPeriodo_7").FirstOrDefault().DS_TEXTO;

                //Asignar texto a las label
                lbPeriodo.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlbPeriodo").FirstOrDefault().DS_TEXTO;
                lbNbPeriodo.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlbNbPeriodo").FirstOrDefault().DS_TEXTO;
                lbEstatus.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlbEstatus").FirstOrDefault().DS_TEXTO;
                lbClTipoEval.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlbClTipoEval").FirstOrDefault().DS_TEXTO;
                lblDsNotas.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblDsNotas").FirstOrDefault().DS_TEXTO;
               // lblPorPuesto.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblPorPuesto").FirstOrDefault().DS_TEXTO;
                lblFgPuestoActual.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblFgPuestoActual").FirstOrDefault().DS_TEXTO;
                lblFgOtrosPuestos.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblFgOtrosPuestos").FirstOrDefault().DS_TEXTO;
               // lblPlanVidaCarrera.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblPlanVidaCarrera").FirstOrDefault().DS_TEXTO;
                lblFgRutaVertical.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblFgRutaVertical").FirstOrDefault().DS_TEXTO;
                lblFgRutaVerticalAlternativa.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblFgRutaVerticalAlternativa").FirstOrDefault().DS_TEXTO;
                lblFgRutaHorizontalAlternativa.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblFgRutaHorizontalAlternativa").FirstOrDefault().DS_TEXTO;
                lblEspecificas.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblEspecificas").FirstOrDefault().DS_TEXTO;
                lblFgGenericas.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblFgGenericas").FirstOrDefault().DS_TEXTO;
                lblFgInstitucionales.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblFgInstitucionales").FirstOrDefault().DS_TEXTO;
                lblLstCamposInterrelacionados.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblLstCamposInterrelacionados").FirstOrDefault().DS_TEXTO;
                lblNbMensajeInicial.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblNbMensajeInicial").FirstOrDefault().DS_TEXTO;
                lblDsMensajeInicial.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblDsMensajeInicial").FirstOrDefault().DS_TEXTO;
                lblNbMensajeEval.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblNbMensajeEval").FirstOrDefault().DS_TEXTO;
                lblNoAutoevaluacion.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblNoAutoevaluacion").FirstOrDefault().DS_TEXTO;
                lblNoSuperior.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblNoSuperior").FirstOrDefault().DS_TEXTO;
                lblNoSubordinados.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblNoSubordinados").FirstOrDefault().DS_TEXTO;
                lblNoInterrelacionados.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblNoInterrelacionados").FirstOrDefault().DS_TEXTO;
                lblNoOtros.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblNoOtros").FirstOrDefault().DS_TEXTO;
                lblNoTotal.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblNoTotal").FirstOrDefault().DS_TEXTO;
                lblNbMensajeComp.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblNbMensajeComp").FirstOrDefault().DS_TEXTO;
                lblPrGenericas.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblPrGenericas").FirstOrDefault().DS_TEXTO;
                lblPrEspecificas.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblPrEspecificas").FirstOrDefault().DS_TEXTO;
                lblPrInstitucionales.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblPrInstitucionales").FirstOrDefault().DS_TEXTO;
                lblPrTotalCom.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblPrTotalCom").FirstOrDefault().DS_TEXTO;
                lblNbEvaluadorExterno.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblNbEvaluadorExterno").FirstOrDefault().DS_TEXTO;
                lblPuestoEvaluadorExterno.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblPuestoEvaluadorExterno").FirstOrDefault().DS_TEXTO;
                lblClCorreoElectronico.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblClCorreoElectronico").FirstOrDefault().DS_TEXTO;
                lblFgGenericas.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblFgGenericas").FirstOrDefault().DS_TEXTO;
                lblFgGenericas.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblFgGenericas").FirstOrDefault().DS_TEXTO;
                lblEvalExt.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblEvalExt").FirstOrDefault().DS_TEXTO;
                lblEvalInt.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlblEvalInt").FirstOrDefault().DS_TEXTO;

                //Asignar texto a RadButton
                btnEvaluacionPorPuesto.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnEvaluacionPorPuesto").FirstOrDefault().DS_TEXTO;
                btnEvaluacionPorEstandar.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnEvaluacionPorEstandar").FirstOrDefault().DS_TEXTO;
                btnEvaluacionPorOtras.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnEvaluacionPorOtras").FirstOrDefault().DS_TEXTO;
                btnGuardarConfiguracion.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnGuardarConfiguracion").FirstOrDefault().DS_TEXTO;
                //btnGuardarConfiguracionCerrar.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnGuardarConfiguracionCerrar").FirstOrDefault().DS_TEXTO;
                btnAgregarCampoAdicional.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnAgregarCampoAdicional").FirstOrDefault().DS_TEXTO;
                btnEditar.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnEditar").FirstOrDefault().DS_TEXTO;
                btnEliminar.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnEliminar").FirstOrDefault().DS_TEXTO;
                btnGuardar.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnGuardar").FirstOrDefault().DS_TEXTO;
                //btnGuardarCerrar.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnGuardarCerrar").FirstOrDefault().DS_TEXTO;
                chkFgPonderarEvaluadoresAuto.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vchkFgPonderarEvaluadoresAuto").FirstOrDefault().DS_TEXTO;
                chkFgPonderarEvaluadores.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vchkFgPonderarEvaluadores").FirstOrDefault().DS_TEXTO;
                chkFgPonderarCompetenciasAuto.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vchkFgPonderarCompetenciasAuto").FirstOrDefault().DS_TEXTO;
                chkFgPonderacionCompetencia.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vchkFgPonderacionCompetencia").FirstOrDefault().DS_TEXTO;
                btnGuardarPonderacion.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnGuardarPonderacion").FirstOrDefault().DS_TEXTO;
                //btnGuardarPonderacionCerrar.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnGuardarPonderacionCerrar").FirstOrDefault().DS_TEXTO;
                btnSeleccionPorPersona.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnSeleccionPorPersona").FirstOrDefault().DS_TEXTO;
                btnSeleccionPorPuesto.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnSeleccionPorPuesto").FirstOrDefault().DS_TEXTO;
                btnSeleccionPorArea.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnSeleccionPorArea").FirstOrDefault().DS_TEXTO;
                btnEliminarEvaluado.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnEliminarEvaluado").FirstOrDefault().DS_TEXTO;
                btnAgregarPuestos.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnAgregarPuestos").FirstOrDefault().DS_TEXTO;
                btnAgregarOtroEvaluador.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnAgregarOtroEvaluador").FirstOrDefault().DS_TEXTO;
                btnAgregarOtrosEvaluadoresInventario.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnAgregarOtrosEvaluadoresInventario").FirstOrDefault().DS_TEXTO;
                btnPlaneacionMatriz.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnPlaneacionMatriz").FirstOrDefault().DS_TEXTO;
                btnMostrarMatriz.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnMostrarMatriz").FirstOrDefault().DS_TEXTO;
                btnRegistroAutorizacion.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnRegistroAutorizacion").FirstOrDefault().DS_TEXTO;
                btnAgregarCuestionario.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnAgregarCuestionario").FirstOrDefault().DS_TEXTO;
                btnCrearCuestionarios.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnCrearCuestionarios").FirstOrDefault().DS_TEXTO;
                btnReasignarContrasena.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnReasignarContrasena").FirstOrDefault().DS_TEXTO;
                btnEnviarCuestionarios.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnEnviarCuestionarios").FirstOrDefault().DS_TEXTO;
                //btnCerrarPantalla.Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnCerrarPantalla").FirstOrDefault().DS_TEXTO;

                //Asignar tooltip a RadButton
                chkFgPuestoActual.ToolTip = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vchkFgPuestoActual").FirstOrDefault().DS_TEXTO;             
                chkFgOtrosPuestos.ToolTip = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vchkFgOtrosPuestos").FirstOrDefault().DS_TEXTO;                
                chkFgRutaVertical.ToolTip = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vchkFgRutaVertical").FirstOrDefault().DS_TEXTO;             
                chkFgRutaVerticalAlternativa.ToolTip = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vchkFgRutaVerticalAlternativa").FirstOrDefault().DS_TEXTO;               
                chkFgRutaHorizontalAlternativa.ToolTip = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vchkFgRutaHorizontalAlternativa").FirstOrDefault().DS_TEXTO;                    
                chkFgCompetenciasGenericas.ToolTip = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vchkFgCompetenciasGenericas").FirstOrDefault().DS_TEXTO;               
                chkFgCompetenciasEspecificas.ToolTip = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vchkFgCompetenciasEspecificas").FirstOrDefault().DS_TEXTO;              
                chkFgCompetenciasInstitucionales.ToolTip = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vchkFgCompetenciasInstitucionales").FirstOrDefault().DS_TEXTO;                              
                btnEvaluacionPorOtras.ToolTip = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnEvaluacionPorOtras_tooltip").FirstOrDefault().DS_TEXTO;
                btnRegistroAutorizacion.ToolTip = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vbtnRegistroAutorizacion_tooltip").FirstOrDefault().DS_TEXTO;

                //Asignar texto a legend
                lgInterrelacionado.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlgInterrelacionado").FirstOrDefault().DS_TEXTO;           
                lgPorEval.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlgPorEval").FirstOrDefault().DS_TEXTO;                                               
                lgPorCompetencia.InnerText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vlgPorCompetencia").FirstOrDefault().DS_TEXTO;

                //Asignat titulo a RadSlidingPane     
                rspOtroEvaluadorManual.Title = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vrspOtroEvaluadorManual").FirstOrDefault().DS_TEXTO;                         
                rspOtroEvaluadorInventario.Title = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vrspOtroEvaluadorInventario").FirstOrDefault().DS_TEXTO;
                             
                winSeleccion.Title = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vwinSeleccion").FirstOrDefault().DS_TEXTO;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;
          //  vClCultureIdioma = ContextoApp.clCultureIdioma;

            if (!Page.IsPostBack)
            {
                vFgPuestosComparacion = 0;
                int vIdPeriodoQS = 0;

                int.TryParse(Request.QueryString["PeriodoId"], out vIdPeriodoQS);
                vIdPeriodoV = vIdPeriodoQS;

                ObtenerDatosPeriodo(vIdPeriodoQS);
                CargarDatosConfiguracion();

                if (vPeriodo.FG_CREADO_POR_PVC)
                    aplicarConfiguracionPeriodoPVC();


                //Traducir textos
                if (vClIdioma != E_IDIOMA_ENUM.ES.ToString())
                {
                    TraducirTextos();
                }

            }


        }

        protected void AgregarEvaluadosPorEmpleado(string pEvaluados)
        {
            List<E_SELECTOR_EMPLEADO> vEvaluados = JsonConvert.DeserializeObject<List<E_SELECTOR_EMPLEADO>>(pEvaluados);

            if (vEvaluados.Count > 0)
                AgregarEvaluados(new XElement("EMPLEADOS", vEvaluados.Select(s => new XElement("EMPLEADO", new XAttribute("ID_EMPLEADO", s.idEmpleado)))));
        }

        protected void AgregarEvaluadosPorPuesto(string pPuestos)
        {
            List<E_SELECTOR_PUESTO> vPuestos = JsonConvert.DeserializeObject<List<E_SELECTOR_PUESTO>>(pPuestos);

            if (vPuestos.Count > 0)
                AgregarEvaluados(new XElement("PUESTOS", vPuestos.Select(s => new XElement("PUESTO", new XAttribute("ID_PUESTO", s.idPuesto)))));
        }

        protected void AgregarEvaluadosPorArea(string pAreas)
        {
            List<E_SELECTOR_AREA> vAreas = JsonConvert.DeserializeObject<List<E_SELECTOR_AREA>>(pAreas);

            if (vAreas.Count > 0)
                AgregarEvaluados(new XElement("AREAS", vAreas.Select(s => new XElement("AREA", new XAttribute("ID_AREA", s.idArea)))));
        }

        protected void AgregarEvaluados(XElement pXmlElementos)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            E_RESULTADO vResultado = nPeriodo.InsertaEvaluados(vPeriodo.ID_PERIODO ?? 0, pXmlElementos, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
            {
                grdEvaluados.Rebind();

                if (vPeriodo.FG_OTROS_PUESTOS)
                {
                    //Valida idioma 
                    if (vClIdioma != E_IDIOMA_ENUM.ES.ToString())
                    {
                        TraduccionIdiomaTextoNegocio oNegocio = new TraduccionIdiomaTextoNegocio();
                        SPE_OBTIENE_TRADUCCION_TEXTO_Result vLstTextosTraduccion = oNegocio.ObtieneTraduccion(pCL_TEXTO: "CB_MS_EXITO", pCL_MODULO: "FYD", pCL_PROCESO: "EC_CONFIGURACION", pCL_IDIOMA: "PORT").FirstOrDefault();
                        if (vLstTextosTraduccion != null)
                        {
                            UtilMensajes.MensajeResultadoDB(rwmMensaje, vLstTextosTraduccion.DS_TEXTO, E_TIPO_RESPUESTA_DB.SUCCESSFUL, 400, 200, pCallBackFunction: null);
                        }
                    }
                    else
                    {
                        UtilMensajes.MensajeResultadoDB(rwmMensaje, "Proceso exitoso. <br />Los evaluados no tienen puesto a evaluar por default. Por favor, selecciona los puestos por cada evaluado.", E_TIPO_RESPUESTA_DB.SUCCESSFUL, 400, 200, pCallBackFunction: null);
                    }
                }
                else
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
                }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
            }
        }

        protected void AgregarOtrosEvaluadoresInventario(string pOtroEvaluador)
        {
            List<E_SELECTOR_EMPLEADO> vOtrosEvaluadores = JsonConvert.DeserializeObject<List<E_SELECTOR_EMPLEADO>>(pOtroEvaluador);

            if (vOtrosEvaluadores.Count > 0)
            {
                XElement vXmlOtrosEvaluadores = new XElement("EVALUADORES", vOtrosEvaluadores.Select(s => new XElement("EVALUADOR", new XAttribute("ID_EVALUADOR", s.idEmpleado))));
                PeriodoNegocio nPeriodo = new PeriodoNegocio();
                E_RESULTADO vResultado = nPeriodo.InsertaOtrosEvaluadoresInventario(vPeriodo.ID_PERIODO ?? 0, vXmlOtrosEvaluadores, chkFgOtroEvaluadorTodos.Checked, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

                if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                {
                    grdEvaluadoresExternos.Rebind();
                }
            }
        }

        protected void AgregarPuestosParaEvaluar(string pOtrosPuestos)
        {
            dynamic vData = JObject.Parse(pOtrosPuestos);

            List<E_SELECTOR_EMPLEADO> vEmpleados = new List<E_SELECTOR_EMPLEADO>();
            List<E_SELECTOR_PUESTO> vPuestos = new List<E_SELECTOR_PUESTO>();

            if (vData.lstEmpleados is JArray)
                vEmpleados = vData.lstEmpleados.ToObject<List<E_SELECTOR_EMPLEADO>>();

            if (vData.lstPuestos is JArray)
                vPuestos = vData.lstPuestos.ToObject<List<E_SELECTOR_PUESTO>>();

            XElement vXmlEmpleados = new XElement("EMPLEADOS", vEmpleados.Select(s => new XElement("EMPLEADO", new XAttribute("ID_EMPLEADO", s.idEmpleado))));
            XElement vXmlPuestos = new XElement("PUESTOS", vPuestos.Select(s => new XElement("PUESTO", new XAttribute("ID_PUESTO", s.idPuesto))));

            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            E_RESULTADO vResultado = nPeriodo.InsertaActualizaOtrosPuestosEvaluados(vPeriodo.ID_PERIODO ?? 0, vXmlEmpleados, vXmlPuestos, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                grdEvaluados.Rebind();
        }

        protected void ObtenerDatosPeriodo(int vIdPeriodo)
        {
            PeriodoNegocio oPeriodo = new PeriodoNegocio();
            SPE_OBTIENE_FYD_PERIODO_EVALUACION_Result vPer = oPeriodo.ObtienePeriodoEvaluacion(vIdPeriodo);

            vPeriodo = new E_PERIODO_EVALUACION()
            {
                CL_ESTADO = vPer.CL_ESTADO_PERIODO,
                CL_TIPO_EVALUACION = vPer.CL_TIPO_EVALUACION,
                CL_TIPO_PERIODO = vPer.CL_TIPO_PERIODO,
                FE_PERIODO = vPer.FE_INICIO,
                FG_AUTOEVALUACION = vPer.FG_AUTOEVALUACION,
                FG_COMPETENCIAS_ESPECIFICAS = vPer.FG_ESPECIFICAS,
                FG_COMPETENCIAS_GENERICAS = vPer.FG_GENERICAS,
                FG_COMPETENCIAS_INSTITUCIONALES = vPer.FG_INSTITUCIONALES,
                PR_COMPETENCIAS_ESPECIFICAS = vPer.PR_ESPECIFICAS,
                PR_COMPETENCIAS_GENERICAS = vPer.PR_GENERICAS,
                PR_COMPETENCIAS_INSTITUCIONALES = vPer.PR_INSTITUCIONALES,
                FG_INTERRELACIONADOS = vPer.FG_INTERRELACIONADOS,
                FG_OTROS_EVALUADORES = vPer.FG_OTROS_EVALUADORES,
                FG_OTROS_PUESTOS = vPer.FG_OTROS_PUESTOS,
                FG_PUESTO_ACTUAL = vPer.FG_PUESTO_ACTUAL,
                FG_RUTA_HORIZONTAL = vPer.FG_RUTA_HORIZONTAL,
                FG_RUTA_VERTICAL = vPer.FG_RUTA_VERTICAL,
                FG_RUTA_VERTICAL_ALTERNATIVA = vPer.FG_RUTA_VERTICAL_ALTERNATIVA,
                FG_SUBORDINADOS = vPer.FG_SUBORDINADOS,
                FG_SUPERIOR = vPer.FG_SUPERVISOR,
                ID_PERIODO = vPer.ID_PERIODO,
                NB_PERIODO = vPer.NB_PERIODO,
                DS_PERIODO = vPer.DS_PERIODO,
                DS_NOTAS = vPer.DS_NOTAS,
                PR_AUTOEVALUACION = vPer.PR_AUTOEVALUACION,
                PR_INTERRELACIONADOS = vPer.PR_INTERRELACIONADOS,
                PR_OTROS_EVALUADORES = vPer.PR_OTROS_EVALUADORES,
                PR_SUBORDINADOS = vPer.PR_SUBORDINADOS,
                PR_SUPERIOR = vPer.PR_SUPERVISOR,
                XML_INSTRUCCIONES_DE_LLENADO = vPer.XML_INSTRUCCIONES_LLENADO,
                XML_MENSAJE_INICIAL = vPer.XML_MENSAJE_INICIAL,
                FG_PONDERACION_COMPETENCIAS = vPer.FG_PONDERACION_COMPETENCIAS,
                FG_PONDERACION_EVALUADORES = vPer.FG_PONDERACION_EVALUADORES,
                FG_CREADO_POR_PVC = vPer.FG_CREADO_POR_PVC
            };

            if (vPer.CL_TIPO_EVALUACION.Equals(E_CL_TIPO_EVALUACION.COMPETENCIAS_OTRAS.ToString()) && vPer.XML_OTRAS_COMPETENCIAS != null)
            {
                XElement vLstCompetencias = XElement.Parse(vPer.XML_OTRAS_COMPETENCIAS);
                if (vLstCompetencias != null && vLstCompetencias.Descendants("COMPETENCIA_ESPECIFICA") != null)
                {
                    vPeriodo.LS_OTRAS_COMPETENCIAS = vLstCompetencias.Descendants("COMPETENCIA_ESPECIFICA").Select(s => new E_COMPETENCIAS()
                    {
                        ID_COMPETENCIA = UtilXML.ValorAtributo<int>(s.Attribute("ID_COMPETENCIA")),
                        CL_COMPETENCIA = UtilXML.ValorAtributo<string>(s.Attribute("CL_COMPETENCIA")),
                        NB_COMPETENCIA = UtilXML.ValorAtributo<string>(s.Attribute("NB_COMPETENCIA"))
                    }).ToList();
                }
            }
            else
                vPeriodo.LS_OTRAS_COMPETENCIAS = new List<E_COMPETENCIAS>();

            if (vPer.FG_INTERRELACIONADOS && !String.IsNullOrEmpty(vPer.XML_CAMPOS_ADICIONALES))
            {
                XElement vLstCamposAdicionales = XElement.Parse(vPer.XML_CAMPOS_ADICIONALES);
                vPeriodo.LS_CAMPOS_COMUNES = vLstCamposAdicionales.Element("INTERRELACIONADOS").Elements("CAMPO").Select(s => new E_CAMPO()
                {
                    ID_CAMPO = UtilXML.ValorAtributo<int>(s.Attribute("ID_CAMPO")),
                    NB_CAMPO = UtilXML.ValorAtributo<string>(s.Attribute("NB_CAMPO"))
                }).ToList();
            }
            else
                vPeriodo.LS_CAMPOS_COMUNES = new List<E_CAMPO>();
        }

        protected void CargarDatosConfiguracion()
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            List<SPE_OBTIENE_FYD_CUESTIONARIOS_EVALUADOS_Result> vLstEvaluadosCuestionarios = nPeriodo.ObtieneEvaluadosCuestionarios(vPeriodo.ID_PERIODO ?? 0, ContextoUsuario.oUsuario.ID_EMPRESA);
            bool fgHabilitaPonderacion = !vLstEvaluadosCuestionarios.Any(a => a.NO_CUESTIONARIOS > 0);
            string vTiposEvaluacion = "";

            txtClPeriodo.InnerText = vPeriodo.NB_PERIODO;
            txtDsPeriodo.InnerText = vPeriodo.DS_PERIODO;
            txtEstatus.InnerText = vPeriodo.CL_ESTADO;
            vFgInterrelacionado = vPeriodo.FG_INTERRELACIONADOS;

            if (vClIdioma != E_IDIOMA_ENUM.ES.ToString())
            {

                TraduccionIdiomaTextoNegocio oNegocio = new TraduccionIdiomaTextoNegocio();
                List<SPE_OBTIENE_TRADUCCION_TEXTO_Result> vLstTextosTraduccion = oNegocio.ObtieneTraduccion(pCL_MODULO: "FYD", pCL_PROCESO: "EC_CONFIGURACION", pCL_IDIOMA: "PORT");
                if (vLstTextosTraduccion != null)
                {
                    if (vPeriodo.FG_AUTOEVALUACION)
                    {
                        vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? vLstTextosTraduccion.Where(w => w.CL_TEXTO == "CB_AUTOEVAL").FirstOrDefault().DS_TEXTO : String.Join(", ", vTiposEvaluacion, vLstTextosTraduccion.Where(w => w.CL_TEXTO == "CB_AUTOEVAL").FirstOrDefault().DS_TEXTO);
                    }

                    if (vPeriodo.FG_SUPERIOR)
                    {
                        vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? vLstTextosTraduccion.Where(w => w.CL_TEXTO == "CB_SUPERIOR").FirstOrDefault().DS_TEXTO : String.Join(", ", vTiposEvaluacion, vLstTextosTraduccion.Where(w => w.CL_TEXTO == "CB_SUPERIOR").FirstOrDefault().DS_TEXTO);
                    }

                    if (vPeriodo.FG_SUBORDINADOS)
                    {
                        vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? vLstTextosTraduccion.Where(w => w.CL_TEXTO == "CB_SUBORDINADO").FirstOrDefault().DS_TEXTO : String.Join(", ", vTiposEvaluacion, vLstTextosTraduccion.Where(w => w.CL_TEXTO == "CB_SUBORDINADO").FirstOrDefault().DS_TEXTO);
                    }

                    if (vPeriodo.FG_INTERRELACIONADOS)
                    {
                        vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? vLstTextosTraduccion.Where(w => w.CL_TEXTO == "CB_INTERRELACIONADO").FirstOrDefault().DS_TEXTO : String.Join(", ", vTiposEvaluacion, vLstTextosTraduccion.Where(w => w.CL_TEXTO == "CB_INTERRELACIONADO").FirstOrDefault().DS_TEXTO);
                    }

                    if (vPeriodo.FG_OTROS_EVALUADORES)
                    {
                        vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? vLstTextosTraduccion.Where(w => w.CL_TEXTO == "CB_OTROS").FirstOrDefault().DS_TEXTO : String.Join(", ", vTiposEvaluacion, vLstTextosTraduccion.Where(w => w.CL_TEXTO == "CB_OTROS").FirstOrDefault().DS_TEXTO);
                    }

                    txtTipoEvaluacion.InnerText = vTiposEvaluacion;
                }
            }
            else
            {

                if (vPeriodo.FG_AUTOEVALUACION)
                {
                    vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Autoevaluación" : String.Join(", ", vTiposEvaluacion, "Autoevaluacion");
                }

                if (vPeriodo.FG_SUPERIOR)
                {
                    vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Superior" : String.Join(", ", vTiposEvaluacion, "Superior");
                }

                if (vPeriodo.FG_SUBORDINADOS)
                {
                    vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Subordinado" : String.Join(", ", vTiposEvaluacion, "Subordinado");
                }

                if (vPeriodo.FG_INTERRELACIONADOS)
                {
                    vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Interrelacionado" : String.Join(", ", vTiposEvaluacion, "Interrelacionado");
                }

                if (vPeriodo.FG_OTROS_EVALUADORES)
                {
                    vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Otros" : String.Join(", ", vTiposEvaluacion, "Otros");
                }

                txtTipoEvaluacion.InnerText = vTiposEvaluacion;
            }


            if (vPeriodo.DS_NOTAS != null)
            {
                if (vPeriodo.DS_NOTAS.Contains("DS_NOTA"))
                {
                    txtNotas.InnerHtml = Utileria.MostrarNotas(vPeriodo.DS_NOTAS);
                }
                else
                {
                    XElement vNotas = XElement.Parse(vPeriodo.DS_NOTAS);
                    if (vNotas != null)
                    {
                        vNotas.Name = vNbFirstRadEditorTagName;
                        txtNotas.InnerHtml = vNotas.ToString();
                    }
                }
            }
            // txtIdPeriodo.InnerText = vPeriodo.NB_PERIODO;
            // txtNbPeriodo.InnerText = vPeriodo.DS_PERIODO;


            //chkFgEvaluadorAutoevaluacion.Checked = vPeriodo.FG_AUTOEVALUACION;
            //chkFgEvaluadorSubordinados.Checked = vPeriodo.FG_SUBORDINADOS;
            //chkFgEvaluadorSupervisor.Checked = vPeriodo.FG_SUPERIOR;
            //chkFgEvaluadorInterrelacionados.Checked = vPeriodo.FG_INTERRELACIONADOS;
            //chkFgEvaluadorOtros.Checked = vPeriodo.FG_OTROS_EVALUADORES;
            chkFgPonderacionCompetencia.Checked = vPeriodo.FG_PONDERACION_COMPETENCIAS;
            chkFgPonderacionCompetencia.Enabled = fgHabilitaPonderacion;
            chkFgPonderarEvaluadores.Checked = vPeriodo.FG_PONDERACION_EVALUADORES;
            chkFgPonderarEvaluadores.Enabled = fgHabilitaPonderacion;
            chkFgPonderarEvaluadoresAuto.Enabled = fgHabilitaPonderacion;
            chkFgPonderarCompetenciasAuto.Enabled = fgHabilitaPonderacion;

            if (chkFgPonderarEvaluadores.Checked == true)
            {
                divPonderarEvaluadores.Style.Value = String.Format("display: {0}", "block");
                //vPrAutoevaluacion = vPeriodo.FG_AUTOEVALUACION ? vPeriodo.PR_AUTOEVALUACION : 20;
                //vPrSuperior = vPeriodo.FG_SUPERIOR ? vPeriodo.PR_SUPERIOR : 20;
                //vPrSubordinado = vPeriodo.FG_SUBORDINADOS ? vPeriodo.PR_SUBORDINADOS : 20;
                //vPrInterrelacionado = vPeriodo.FG_INTERRELACIONADOS ? vPeriodo.PR_INTERRELACIONADOS : 20;
                //vPrOtros = vPeriodo.FG_OTROS_EVALUADORES ? vPeriodo.PR_OTROS_EVALUADORES : 20;
                //vPrTotal = vPrAutoevaluacion + vPrSuperior + vPrSubordinado + vPrInterrelacionado + vPrOtros;
            }
            else
                chkFgPonderarEvaluadoresAuto.Checked = true;

            if (chkFgPonderacionCompetencia.Checked == true)
            {
                divPonderarCompetencias.Style.Value = String.Format("display: {0}", "block");
                //vPrGenericas = vPeriodo.PR_COMPETENCIAS_GENERICAS;
                //vPrEspecificas = vPeriodo.PR_COMPETENCIAS_ESPECIFICAS;
                //vPrInstitucionales = vPeriodo.PR_COMPETENCIAS_INSTITUCIONALES;
                //vPrTotalCompetencias = vPrGenericas + vPrEspecificas + vPrInstitucionales;

            }
            else
                chkFgPonderarCompetenciasAuto.Checked = true;

            if (!vPeriodo.FG_OTROS_EVALUADORES)
            {
                rtsConfiguracionPeriodo.Tabs[5].Visible = false;
            }

            divConfiguracionInterrelacionados.Style.Value = String.Format("display: {0}", vPeriodo.FG_INTERRELACIONADOS ? "block" : "none");
            rspFiltroInter.Visible = vPeriodo.FG_INTERRELACIONADOS;

            bool vFgAutoevaluacion = vPeriodo.FG_AUTOEVALUACION;
            bool vFgOtros = vPeriodo.FG_SUBORDINADOS || vPeriodo.FG_SUPERIOR || vPeriodo.FG_INTERRELACIONADOS || vPeriodo.FG_OTROS_EVALUADORES;
            bool vFgAmbos = vFgAutoevaluacion && vFgOtros;

            //btnCuestionarioAmbos.Enabled = vFgAmbos;
            //btnCuestionarioAutoevaluacion.Enabled = vFgAmbos;
            //btnCuestionarioOtros.Enabled = vFgAmbos;

            //btnCuestionarioAmbos.Checked = vFgAmbos;
            //btnCuestionarioAutoevaluacion.Checked = vFgAutoevaluacion && !vFgAmbos;
            //btnCuestionarioOtros.Checked = vFgOtros && !vFgAmbos;

            txtPrAutoevaluacion.Enabled = vPeriodo.FG_AUTOEVALUACION && fgHabilitaPonderacion;
            txtPrSubordinados.Enabled = vPeriodo.FG_SUBORDINADOS && fgHabilitaPonderacion;
            txtPrSuperior.Enabled = vPeriodo.FG_SUPERIOR && fgHabilitaPonderacion;
            txtPrInterrelacionados.Enabled = vPeriodo.FG_INTERRELACIONADOS && fgHabilitaPonderacion;
            txtPrOtros.Enabled = vPeriodo.FG_OTROS_EVALUADORES && fgHabilitaPonderacion;

            decimal vPrAutoevaluacion = vPeriodo.FG_AUTOEVALUACION ? vPeriodo.PR_AUTOEVALUACION : 0;
            decimal vPrSuperior = vPeriodo.FG_SUPERIOR ? vPeriodo.PR_SUPERIOR : 0;
            decimal vPrSubordinado = vPeriodo.FG_SUBORDINADOS ? vPeriodo.PR_SUBORDINADOS : 0;
            decimal vPrInterrelacionado = vPeriodo.FG_INTERRELACIONADOS ? vPeriodo.PR_INTERRELACIONADOS : 0;
            decimal vPrOtros = vPeriodo.FG_OTROS_EVALUADORES ? vPeriodo.PR_OTROS_EVALUADORES : 0;
            decimal vPrTotal = vPrAutoevaluacion + vPrSuperior + vPrSubordinado + vPrInterrelacionado + vPrOtros;

            //if (!fgHabilitaPonderacion)
            //{
            txtPrAutoevaluacion.Value = (double)vPrAutoevaluacion;
            //txtPrAutoevaluacion.Enabled = fgHabilitaPonderacion;
            txtPrSuperior.Value = (double)vPrSuperior;
            //txtPrSuperior.Enabled = fgHabilitaPonderacion;
            txtPrSubordinados.Value = (double)vPrSubordinado;
            //txtPrSubordinados.Enabled = fgHabilitaPonderacion;
            txtPrInterrelacionados.Value = (double)vPrInterrelacionado;
            //txtPrInterrelacionados.Enabled = fgHabilitaPonderacion;
            txtPrOtros.Value = (double)vPrOtros;
            //txtPrOtros.Enabled = fgHabilitaPonderacion;
            txtPrTotal.Value = (double)vPrTotal;
            //}

            decimal vPrGenericas = vPeriodo.PR_COMPETENCIAS_GENERICAS;
            decimal vPrEspecificas = vPeriodo.PR_COMPETENCIAS_ESPECIFICAS;
            decimal vPrInstitucionales = vPeriodo.PR_COMPETENCIAS_INSTITUCIONALES;
            decimal vPrTotalCompetencias = vPrGenericas + vPrEspecificas + vPrInstitucionales;

            txtPrGenericas.Value = (double)vPrGenericas;
            txtPrGenericas.Enabled = fgHabilitaPonderacion;
            txtPrEspecificas.Value = (double)vPrEspecificas;
            txtPrEspecificas.Enabled = fgHabilitaPonderacion;
            txtPrInstitucionales.Value = (double)vPrInstitucionales;
            txtPrInstitucionales.Enabled = fgHabilitaPonderacion;
            txtPrTotalCompetencias.Value = (double)vPrTotalCompetencias;

            txtDsMensajeInicial.Content = vPeriodo.XML_MENSAJE_INICIAL;

            ChangeEnableSeleccionEvaluados(fgHabilitaPonderacion);
            ChangeEnableOtrosEvaluadores(fgHabilitaPonderacion);
            //ChangeEnableCuestionarios();
            ChangeEnableToken();
        }

        protected void ChangeEnableTipoEvaluacion(bool pFgHabilitarTipoEvaluacion)
        {
            bool vFgPermitirEdicion = (vPeriodo.CL_ESTADO != "CERRADO");
            if (vPeriodo.FG_CREADO_POR_PVC)
                vFgPermitirEdicion = false;

            bool vFgEvaluacionPorPuesto = vPeriodo.CL_TIPO_EVALUACION.Equals(E_CL_TIPO_EVALUACION.PUESTO_CARRERA.ToString());
            btnEvaluacionPorPuesto.Checked = vFgEvaluacionPorPuesto;
            btnEvaluacionPorPuesto.Enabled = pFgHabilitarTipoEvaluacion;

            chkFgPuestoActual.Enabled = vFgEvaluacionPorPuesto && pFgHabilitarTipoEvaluacion;
            chkFgOtrosPuestos.Enabled = vFgEvaluacionPorPuesto && pFgHabilitarTipoEvaluacion;

            chkFgPuestoActual.Checked = vPeriodo.FG_PUESTO_ACTUAL && vFgEvaluacionPorPuesto;
            btnAgregarPuestos.Visible = chkFgOtrosPuestos.Checked = vPeriodo.FG_OTROS_PUESTOS && vFgEvaluacionPorPuesto;


            if (vPeriodo.FG_RUTA_VERTICAL || vPeriodo.FG_RUTA_VERTICAL_ALTERNATIVA || vPeriodo.FG_RUTA_HORIZONTAL)
            {
                btnPlanVidaCarrera.Checked = true;

                btnPlanVidaCarrera.Enabled = pFgHabilitarTipoEvaluacion;
                chkFgRutaVertical.Enabled = pFgHabilitarTipoEvaluacion;
                chkFgRutaVerticalAlternativa.Enabled = pFgHabilitarTipoEvaluacion;
                chkFgRutaHorizontalAlternativa.Enabled = pFgHabilitarTipoEvaluacion;

            }
            else
            {
                btnPlanVidaCarrera.Enabled = pFgHabilitarTipoEvaluacion;
                chkFgRutaVertical.Enabled = pFgHabilitarTipoEvaluacion;
                chkFgRutaVerticalAlternativa.Enabled = pFgHabilitarTipoEvaluacion;
                chkFgRutaHorizontalAlternativa.Enabled = pFgHabilitarTipoEvaluacion;
            }

       
            chkFgRutaVertical.Checked = vPeriodo.FG_RUTA_VERTICAL;
            chkFgRutaVerticalAlternativa.Checked = vPeriodo.FG_RUTA_VERTICAL_ALTERNATIVA;
            chkFgRutaHorizontalAlternativa.Checked = vPeriodo.FG_RUTA_HORIZONTAL;





            bool vFgEvaluacionCompetenciasEstandar = vPeriodo.CL_TIPO_EVALUACION.Equals(E_CL_TIPO_EVALUACION.COMPETENCIA_ESTANDAR.ToString());
            btnEvaluacionPorEstandar.Checked = vFgEvaluacionCompetenciasEstandar;
            btnEvaluacionPorEstandar.Enabled = pFgHabilitarTipoEvaluacion;

            chkFgCompetenciasGenericas.Enabled = vFgEvaluacionCompetenciasEstandar && pFgHabilitarTipoEvaluacion;
            chkFgCompetenciasEspecificas.Enabled = vFgEvaluacionCompetenciasEstandar && pFgHabilitarTipoEvaluacion;
            chkFgCompetenciasInstitucionales.Enabled = vFgEvaluacionCompetenciasEstandar && pFgHabilitarTipoEvaluacion;

            chkFgCompetenciasGenericas.Checked = vPeriodo.FG_COMPETENCIAS_GENERICAS && vFgEvaluacionCompetenciasEstandar;
            chkFgCompetenciasEspecificas.Checked = vPeriodo.FG_COMPETENCIAS_ESPECIFICAS && vFgEvaluacionCompetenciasEstandar;
            chkFgCompetenciasInstitucionales.Checked = vPeriodo.FG_COMPETENCIAS_INSTITUCIONALES && vFgEvaluacionCompetenciasEstandar;

            bool vFgEvaluacionOtrasCompetencias = vPeriodo.CL_TIPO_EVALUACION.Equals(E_CL_TIPO_EVALUACION.COMPETENCIAS_OTRAS.ToString());
            btnEvaluacionPorOtras.Checked = vFgEvaluacionOtrasCompetencias;
            btnEvaluacionPorOtras.Enabled = pFgHabilitarTipoEvaluacion;

            btnEspecificarOtrasCompetencias.Enabled = vFgEvaluacionOtrasCompetencias && pFgHabilitarTipoEvaluacion;
            btnEliminarCompentenciaEspecifica.Enabled = vFgEvaluacionOtrasCompetencias && pFgHabilitarTipoEvaluacion;

            lstCamposInterrelacionados.Enabled = vPeriodo.FG_INTERRELACIONADOS && pFgHabilitarTipoEvaluacion;
            btnEspecificarCamposRelacionados.Enabled = vPeriodo.FG_INTERRELACIONADOS && pFgHabilitarTipoEvaluacion;
            btnEliminarCamposRelacionados.Enabled = vPeriodo.FG_INTERRELACIONADOS && pFgHabilitarTipoEvaluacion;

            lstCompetenciasEspecificas.Items.Clear();
            if (vPeriodo.LS_OTRAS_COMPETENCIAS.Count > 0)
                vPeriodo.LS_OTRAS_COMPETENCIAS.OrderBy(o => o.NB_COMPETENCIA).ToList().ForEach(f => lstCompetenciasEspecificas.Items.Add(new RadListBoxItem(f.NB_COMPETENCIA, f.ID_COMPETENCIA.ToString())));
            else
            {
                if (vClIdioma != E_IDIOMA_ENUM.ES.ToString())
                {
                    TraduccionIdiomaTextoNegocio oNegocio = new TraduccionIdiomaTextoNegocio();
                    SPE_OBTIENE_TRADUCCION_TEXTO_Result vLstTextosTraduccion = oNegocio.ObtieneTraduccion(pCL_TEXTO: "vlstCompetenciasEspecificas", pCL_MODULO: "FYD", pCL_PROCESO: "EC_CONFIGURACION", pCL_IDIOMA: "PORT").FirstOrDefault();
                    lstCompetenciasEspecificas.Items.Add(new RadListBoxItem(vLstTextosTraduccion.DS_TEXTO, "0"));
                }
                else
                lstCompetenciasEspecificas.Items.Add(new RadListBoxItem("No seleccionado", "0"));
            }

            lstCamposInterrelacionados.Items.Clear();
            if (vPeriodo.LS_CAMPOS_COMUNES.Count > 0)
                vPeriodo.LS_CAMPOS_COMUNES.OrderBy(o => o.NB_CAMPO).ToList().ForEach(f => lstCamposInterrelacionados.Items.Add(new RadListBoxItem(f.NB_CAMPO, f.ID_CAMPO.ToString())));
            else
            {
                if (vClIdioma != E_IDIOMA_ENUM.ES.ToString())
                {
                    TraduccionIdiomaTextoNegocio oNegocio = new TraduccionIdiomaTextoNegocio();
                    SPE_OBTIENE_TRADUCCION_TEXTO_Result vLstTextosTraduccion = oNegocio.ObtieneTraduccion(pCL_TEXTO: "vlstCamposInterrelacionados", pCL_MODULO: "FYD", pCL_PROCESO: "EC_CONFIGURACION", pCL_IDIOMA: "PORT").FirstOrDefault();
                    lstCamposInterrelacionados.Items.Add(new RadListBoxItem(vLstTextosTraduccion.DS_TEXTO, "0"));
                }
                else
                lstCamposInterrelacionados.Items.Add(new RadListBoxItem("No seleccionado", "0"));
            }

            btnGuardarConfiguracion.Enabled = vFgPermitirEdicion;
            //btnGuardarConfiguracionCerrar.Enabled = vFgPermitirEdicion;

            ChangeEnablePonderacion(vFgEvaluacionPorPuesto);

        }

        protected void ChangeEnableMensajeCampos(bool pFgHabilitarMensajeCampos)
        {
            txtDsMensajeInicial.EditModes = pFgHabilitarMensajeCampos ? EditModes.Design : EditModes.Preview;
            btnAgregarCampoAdicional.Enabled = pFgHabilitarMensajeCampos;
            // grdCamposAdicionales.MasterTableView.GetColumn("DeleteColumn").Display = pFgHabilitarMensajeCampos;
        }

        protected void ChangeEnablePonderacion(bool pFgHabilitarPonderacion)
        {
            bool vFgPermitirEdicion = (vPeriodo.CL_ESTADO != "CERRADO");
            divPonderacionCompetencias.Style.Value = String.Format("display: {0}", pFgHabilitarPonderacion ? "block" : "none");
            btnGuardarPonderacion.Enabled = vFgPermitirEdicion;
            //btnGuardarPonderacionCerrar.Enabled = vFgPermitirEdicion;
        }

        protected void ChangeEnableSeleccionEvaluados(bool fgHabilitaPonderacion)
        {
            bool vFgPermitirEdicion = (vPeriodo.CL_ESTADO != "CERRADO" && fgHabilitaPonderacion != false);
            btnSeleccionPorPersona.Enabled = vFgPermitirEdicion;
            btnSeleccionPorPuesto.Enabled = vFgPermitirEdicion;
            btnSeleccionPorArea.Enabled = vFgPermitirEdicion;
            btnEliminarEvaluado.Enabled = vFgPermitirEdicion;
            btnAgregarPuestos.Enabled = vFgPermitirEdicion;
        }

        protected void ChangeEnableOtrosEvaluadores(bool fgHabilitaPonderacion)
        {
            bool vFgPermitirEdicion = (vPeriodo.CL_ESTADO != "CERRADO" && fgHabilitaPonderacion != false);
            btnAgregarOtroEvaluador.Enabled = vFgPermitirEdicion;
            btnAgregarOtrosEvaluadoresInventario.Enabled = vFgPermitirEdicion;
        }

        protected void ChangeEnableCuestionarios(bool vFgCuestinoariosCreados)
        {

            // = !(grdCuestionarios.Items.Count > 0);
            bool vFgPermitirEdicion = (vPeriodo.CL_ESTADO != "CERRADO");
            //btnAgregarCuestionario.Enabled = vFgPermitirEdicion && vFgCuestinoariosCreados;
            //btnCrearCuestionarios.Enabled = vFgPermitirEdicion && vFgCuestinoariosCreados;
            btnMostrarMatriz.Enabled = vFgPermitirEdicion;
            btnPlaneacionMatriz.Enabled = vFgPermitirEdicion && vFgCuestinoariosCreados;
            //btnRegistroAutorizacion.Enabled = vFgPermitirEdicion;
            SeguridadProcesos(!vFgCuestinoariosCreados);

            PeriodoNegocio oPeriodo = new PeriodoNegocio();
            var vLstValidacion = oPeriodo.ObtenerValidacionAutorizacion(vIdPeriodoV);
            if (vLstValidacion != null)
            {
                if (vLstValidacion.NUM_DOCUMENTOS > 0 && vLstValidacion.NUM_DOCUMENTOS == vLstValidacion.NUM_AUTORIZADOS || vLstValidacion.NUM_CONTESTADOS > 0)
                    btnMostrarMatriz.Enabled = false;

                if (vLstValidacion.NUM_CONTESTADOS > 0 || (vLstValidacion.NUM_DOCUMENTOS > 0 && vLstValidacion.NUM_DOCUMENTOS == vLstValidacion.NUM_AUTORIZADOS))
                    vFgContestados = "SI";
                else
                    vFgContestados = "NO";

            }

            var vContraseñas = oPeriodo.ObtieneTokenEvaluadores(vPeriodo.ID_PERIODO ?? 0, ContextoUsuario.oUsuario.ID_EMPRESA, pIdRol: vIdRol);
            if (vContraseñas.Count > 0)
                if (vContraseñas.FirstOrDefault().CL_TOKEN == null)
               GenerarContrasena();
        }

        protected void ChangeEnableToken()
        {
            bool vFgPermitirEdicion = (vPeriodo.CL_ESTADO != "CERRADO");
            // btnReasignarTodasContrasenas.Enabled = vFgPermitirEdicion;
            btnReasignarContrasena.Enabled = vFgPermitirEdicion;
        }

        protected void grdEvaluados_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            List<SPE_OBTIENE_FYD_EVALUADOS_CONFIGURACION_Result> vLstEvaluados = nPeriodo.ObtieneEvaluados(vPeriodo.ID_PERIODO ?? 0, ContextoUsuario.oUsuario.ID_EMPRESA, vIdRol);
            grdEvaluados.DataSource = vLstEvaluados;

            ChangeEnableTipoEvaluacion(vLstEvaluados.Count.Equals(0));

            //if (vLstEvaluados.Count == 0)
            //{
            //    vFgPuestosComparacion = true;
            //}
            //else
            //{
            //    vFgPuestosComparacion = false;
            //}
            //ValidarPuestosComparacion();
        }

        protected void ramConfiguracionPeriodo_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            E_SELECTOR vSeleccion = new E_SELECTOR();
            string pParameter = e.Argument;

            if (pParameter != null)
                vSeleccion = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);

            if (vSeleccion.clTipo == "EVALUADO")
                AgregarEvaluadosPorEmpleado(vSeleccion.oSeleccion.ToString());

            if (vSeleccion.clTipo == "PUESTO")
                AgregarEvaluadosPorPuesto(vSeleccion.oSeleccion.ToString());

            if (vSeleccion.clTipo == "AREA")
                AgregarEvaluadosPorArea(vSeleccion.oSeleccion.ToString());

            if (vSeleccion.clTipo == "OTROEVALUADOR")
                AgregarOtrosEvaluadoresInventario(vSeleccion.oSeleccion.ToString());

            if (vSeleccion.clTipo == "OTROSPUESTOS")
                AgregarPuestosParaEvaluar(vSeleccion.oSeleccion.ToString());

            if (vSeleccion.clTipo == "REBIND")
                grdCuestionarios.Rebind();

            if (vSeleccion.clTipo == "INSERTARCAMPOINTERRRELACIONADO")
                GuardarConfiguracion(false, false);

        }

        protected void btnEliminarEvaluado_Click(object sender, EventArgs e)
        {
            XElement vXmlEvaluados = new XElement("EMPLEADOS");
            foreach (GridDataItem item in grdEvaluados.SelectedItems)
                vXmlEvaluados.Add(new XElement("EMPLEADO", new XAttribute("ID_EMPLEADO", item.GetDataKeyValue("ID_EMPLEADO").ToString())));

            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            E_RESULTADO vResultado = nPeriodo.EliminaEvaluados(vPeriodo.ID_PERIODO ?? 0, vXmlEvaluados, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
            {
                grdEvaluados.Rebind();
                grdCuestionarios.Rebind();
            }
        }

        protected void grdEvaluadoresExternos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            CuestionarioNegocio nCuestionario = new CuestionarioNegocio();

            List<SPE_OBTIENE_FYD_EVALUADORES_EXTERNOS_Result> evaluadores = new List<SPE_OBTIENE_FYD_EVALUADORES_EXTERNOS_Result>();
            evaluadores = nCuestionario.ObtieneEvaluadoresExternos(vPeriodo.ID_PERIODO ?? 0, "OTRO");
            grdEvaluadoresExternos.DataSource = evaluadores;
        }

        protected void btnAgregarOtroEvaluador_Click(object sender, EventArgs e)
        {
            AgregarOtroEvaluadorExterno(null, null, txtNbEvaluadorExterno.Text, txtNbEvaluadorExternoPuesto.Text, txtClCorreoElectronico.Text, chkFgOtroEvaluadorExternoTodos.Checked, E_TIPO_OPERACION_DB.I);
            txtNbEvaluadorExterno.Text = String.Empty;
            txtNbEvaluadorExternoPuesto.Text = String.Empty;
            txtClCorreoElectronico.Text = String.Empty;
        }

        protected void grdEvaluadoresExternos_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            if ((e.Item is GridEditableItem) && e.Item.IsInEditMode)
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                string vNbEvaluador = ((RadTextBox)item.FindControl("txtNbEvaluadorEditForm")).Text;
                string vNbPuesto = ((RadTextBox)item.FindControl("txtNbPuestoEvaluadorEditForm")).Text;
                string vClCorreoElectronico = ((RadTextBox)item.FindControl("txtClCorreoElectronicoEditForm")).Text;
                bool vFgEvaluaTodos = ((RadButton)item.FindControl("chkFormFgEvaluaTodos")).Checked;
                int vIdEvaluador = int.Parse(item.GetDataKeyValue("ID_EVALUADOR").ToString());

                AgregarOtroEvaluadorExterno(null, vIdEvaluador, vNbEvaluador, vNbPuesto, vClCorreoElectronico, vFgEvaluaTodos, E_TIPO_OPERACION_DB.A);
            }
        }

        protected void AgregarOtroEvaluadorExterno(int? pIdEmpleado, int? pIdEvaluador, string pNbEvaluador, string pNbPuesto, string pClCorreoElectronico, bool pFgEvaluaTodos, E_TIPO_OPERACION_DB pClTipoOperacion)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            E_RESULTADO vResultado = nPeriodo.InsertaActualizaOtrosEvaluadoresExternos(vPeriodo.ID_PERIODO ?? 0, pIdEmpleado, pIdEvaluador, pNbEvaluador, pNbPuesto, pClCorreoElectronico, pFgEvaluaTodos, vClUsuario, vNbPrograma, pClTipoOperacion);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                grdEvaluadoresExternos.Rebind();
        }

        protected void grdEvaluadoresExternos_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item is GridEditableItem)
            {
                GridEditableItem item = (GridEditableItem)e.Item;

                int vIdEvaluador = int.Parse(item.GetDataKeyValue("ID_EVALUADOR").ToString());

                PeriodoNegocio nPeriodo = new PeriodoNegocio();
                E_RESULTADO vResultado = nPeriodo.EliminaEvaluador(vIdEvaluador, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

                if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                {
                    grdEvaluadoresExternos.Rebind();
                    grdCuestionarios.Rebind();
                }
            }
        }

        protected void btnCrearCuestionarios_Click(object sender, EventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            E_RESULTADO vResultado = nPeriodo.InsertaCuestionarios(vPeriodo.ID_PERIODO ?? 0, true, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
            {
                grdCuestionarios.Rebind();
                grdContrasenaEvaluadores.Rebind();
            }
        }

        protected void grdEvaluados_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem vDataItem = (GridDataItem)e.DetailTableView.ParentItem;
            switch (e.DetailTableView.Name)
            {
                case "gtvPuestosEvaluacion":
                    int vIdEvaluado = 0;

                    if (int.TryParse(vDataItem.GetDataKeyValue("ID_EVALUADO").ToString(), out vIdEvaluado))
                    {
                        PeriodoNegocio nPeriodo = new PeriodoNegocio();
                        var vListaPuestos = nPeriodo.ObtienePuestosEvaluado(vIdEvaluado);
                        e.DetailTableView.DataSource = vListaPuestos;

                        //if (vListaPuestos.Count > 0)
                        //{
                        //    vFgPuestosComparacion = true;
                        //}
                        //else
                        //{
                        //    vFgPuestosComparacion = false;
                        //}
                    }

                    break;
            }
        }

        protected void btnGuardarConfiguracion_Click(object sender, EventArgs e)
        {
            GuardarConfiguracion(false, true);

            if (btnEvaluacionPorPuesto.Checked == true)
                divPonderacionCompetencias.Style.Value = String.Format("display:{0}", "block");

            if (btnEvaluacionPorEstandar.Checked == true)
                divPonderacionCompetencias.Style.Value = String.Format("display: {0}", "none");

            if (btnEvaluacionPorOtras.Checked == true)
                divPonderacionCompetencias.Style.Value = String.Format("display: {0}", "none");

            ObtenerDatosPeriodo(vPeriodo.ID_PERIODO ?? 0);
        }

        protected void btnGuardarConfiguracionCerrar_Click(object sender, EventArgs e)
        {
            GuardarConfiguracion(true, true);
        }

        protected void GuardarConfiguracion(bool pFgCerrarVentana, bool mostrarMsg)
        {
            XElement vXmlConfiguracion = new XElement("CONFIGURACION");
            XElement vXmlCamposComunes = null;
            XElement vXmlMensajeInicial = null;
            string vNbNodoMensajeInicial = "MENSAJE_INICIAL";
            string vNbNodoCamposComunes = "CAMPOS_COMUNES";

            E_RESULTADO vValidacion = new E_RESULTADO()
            {
                CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.SUCCESSFUL,
                MENSAJE = new List<E_MENSAJE>()
            };

            decimal vPrAutoevaluacion = (decimal)(txtPrAutoevaluacion.Value ?? 0);
            decimal vPrSuperior = (decimal)(txtPrSuperior.Value ?? 0);
            decimal vPrSubordinado = (decimal)(txtPrSubordinados.Value ?? 0);
            decimal vPrInterrelacionados = (decimal)(txtPrInterrelacionados.Value ?? 0);
            decimal vPrOtrosEvaluadores = (decimal)(txtPrOtros.Value ?? 0);

            decimal vPrTotalEvaluadores = vPrAutoevaluacion + vPrSuperior + vPrSubordinado + vPrInterrelacionados + vPrOtrosEvaluadores;

            decimal vPrCompetenciasGenericas = (decimal)(txtPrGenericas.Value ?? 0);
            decimal vPrCompetenciasEspecificas = (decimal)(txtPrEspecificas.Value ?? 0);
            decimal vPrCompetenciasInstitucionales = (decimal)(txtPrInstitucionales.Value ?? 0);

            decimal vPrTotalCompetencias = vPrCompetenciasGenericas + vPrCompetenciasEspecificas + vPrCompetenciasInstitucionales;

            XElement vXmlTipoEvaluacion = new XElement("TIPO_EVALUACION");

            if (vXmlMensajeInicial == null)
                vXmlMensajeInicial = new XElement(vNbNodoMensajeInicial);
            vXmlMensajeInicial.Add(new XAttribute("DS_MENSAJE", (txtDsMensajeInicial.Content)));

            if (chkFgPonderarEvaluadores.Checked == true)
            {

                if (vPrTotalEvaluadores == 100)
                {
                    vXmlTipoEvaluacion.Add(
                        new XAttribute("PR_AUTOEVALUACION", vPrAutoevaluacion),
                        new XAttribute("PR_SUPERIOR", vPrSuperior),
                        new XAttribute("PR_SUBORDINADOS", vPrSubordinado),
                        new XAttribute("PR_INTERRELACIONADO", vPrInterrelacionados),
                        new XAttribute("PR_OTROS_EVALUADORES", vPrOtrosEvaluadores),
                        new XAttribute("FG_PONDERACION_EVALUADORES", chkFgPonderarEvaluadores.Checked ? "1" : "0")
                        );

                    txtPrTotal.DisabledStyle.CssClass = txtPrTotal.DisabledStyle.CssClass.Replace("errBackground", "");

                }
                else
                {
                    vValidacion.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.ERROR;
                    if (vClIdioma != E_IDIOMA_ENUM.ES.ToString())
                    {
                        TraduccionIdiomaTextoNegocio oNegocio = new TraduccionIdiomaTextoNegocio();
                        SPE_OBTIENE_TRADUCCION_TEXTO_Result vLstTextosTraduccion = oNegocio.ObtieneTraduccion(pCL_TEXTO: "CB_TOTAL_EVAL", pCL_MODULO: "FYD", pCL_PROCESO: "EC_CONFIGURACION", pCL_IDIOMA: "PORT").FirstOrDefault();
                        if (vLstTextosTraduccion != null)
                        {
                            vValidacion.MENSAJE.Add(new E_MENSAJE() { DS_MENSAJE = vLstTextosTraduccion.DS_TEXTO });
                        }

                    }
                    else
                    {
                        vValidacion.MENSAJE.Add(new E_MENSAJE() { DS_MENSAJE = "La suma de las ponderaciones de los evaluadores debe sumar 100%" });
                    }
                    txtPrTotal.DisabledStyle.CssClass = "errBackground";
                }
            }

            if (chkFgPonderacionCompetencia.Checked == true)
            {

                if (vPrTotalCompetencias == 100)
                {
                    vXmlTipoEvaluacion.Add(
                        new XAttribute("PR_GENERICAS", vPrCompetenciasGenericas),
                        new XAttribute("PR_ESPECIFICAS", vPrCompetenciasEspecificas),
                        new XAttribute("PR_INSTITUCIONALES", vPrCompetenciasInstitucionales),
                        new XAttribute("FG_PONDERACION_COMPETENCIAS", chkFgPonderacionCompetencia.Checked ? "1" : "0")
                        );
                    txtPrTotalCompetencias.DisabledStyle.CssClass = txtPrTotalCompetencias.DisabledStyle.CssClass.Replace("errBackground", "");
                }

                else
                {
                    vValidacion.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.ERROR;
                    if (vClIdioma != E_IDIOMA_ENUM.ES.ToString())
                    {
                        TraduccionIdiomaTextoNegocio oNegocio = new TraduccionIdiomaTextoNegocio();
                        SPE_OBTIENE_TRADUCCION_TEXTO_Result vLstTextosTraduccion = oNegocio.ObtieneTraduccion(pCL_TEXTO: "CB_TOTAL_COMPETENCIAS", pCL_MODULO: "FYD", pCL_PROCESO: "EC_CONFIGURACION", pCL_IDIOMA: "PORT").FirstOrDefault();
                        if (vLstTextosTraduccion != null)
                        {
                            vValidacion.MENSAJE.Add(new E_MENSAJE() { DS_MENSAJE = vLstTextosTraduccion.DS_TEXTO });
                        }

                    }
                    else
                    {
                        vValidacion.MENSAJE.Add(new E_MENSAJE() { DS_MENSAJE = "La suma de las ponderaciones de las competencias debe sumar 100%" });
                    }
                    txtPrTotalCompetencias.DisabledStyle.CssClass = "errBackground";
                }
            }

            if (vValidacion.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
            {

                if (btnEvaluacionPorPuesto.Checked)
                {
                    vXmlTipoEvaluacion.Add(new XAttribute("CL_TIPO_EVALUACION", E_CL_TIPO_EVALUACION.PUESTO_CARRERA.ToString()),
                        new XAttribute("FG_PUESTO_ACTUAL", chkFgPuestoActual.Checked ? "1" : "0"),
                        new XAttribute("FG_OTROS_PUESTOS", chkFgOtrosPuestos.Checked ? "1" : "0"),
                        new XAttribute("FG_RUTA_VERTICAL", chkFgRutaVertical.Checked ? "1" : "0"),
                        new XAttribute("FG_RUTA_VERTICAL_ALTERNATIVA", chkFgRutaVerticalAlternativa.Checked ? "1" : "0"),
                        new XAttribute("FG_RUTA_HORIZONTAL", chkFgRutaHorizontalAlternativa.Checked ? "1" : "0")
                        );
                }

                if (btnEvaluacionPorEstandar.Checked)
                {
                    vXmlTipoEvaluacion.Add(new XAttribute("CL_TIPO_EVALUACION", E_CL_TIPO_EVALUACION.COMPETENCIA_ESTANDAR.ToString()),
                        new XAttribute("FG_GENERICAS", chkFgCompetenciasGenericas.Checked ? "1" : "0"),
                        new XAttribute("FG_ESPECIFICAS", chkFgCompetenciasEspecificas.Checked ? "1" : "0"),
                        new XAttribute("FG_INSTITUCIONALES", chkFgCompetenciasInstitucionales.Checked ? "1" : "0")
                        );
                }

                if (btnEvaluacionPorOtras.Checked)
                {
                    vXmlTipoEvaluacion.Add(new XAttribute("CL_TIPO_EVALUACION", E_CL_TIPO_EVALUACION.COMPETENCIAS_OTRAS.ToString()),
                        lstCompetenciasEspecificas.Items.Where(w => !w.Value.Equals(String.Empty)).Select(s => new XElement("COMPETENCIA_ESPECIFICA", new XAttribute("ID_COMPETENCIA", s.Value)))
                        );
                }

                if (vPeriodo.FG_INTERRELACIONADOS)
                {
                    if (vXmlCamposComunes == null)
                        vXmlCamposComunes = new XElement(vNbNodoCamposComunes);
                    vXmlCamposComunes.Add(new XElement("INTERRELACIONADOS", lstCamposInterrelacionados.Items.Where(w => !w.Value.Equals(String.Empty)).Select(s => new XElement("CAMPO", new XAttribute("ID_CAMPO", s.Value), new XAttribute("NB_CAMPO", s.Text)))));
                }

                vXmlConfiguracion.Add(vXmlTipoEvaluacion);

                if (vXmlCamposComunes != null)
                    vXmlConfiguracion.Add(vXmlCamposComunes);

                if (vXmlMensajeInicial != null)
                    vXmlConfiguracion.Add(vXmlMensajeInicial);

                PeriodoNegocio nPeriodo = new PeriodoNegocio();

                E_RESULTADO vResultado = nPeriodo.ActualizaConfiguracionPeriodo(vPeriodo.ID_PERIODO ?? 0, vXmlConfiguracion, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                string vCallBackFunction = pFgCerrarVentana ? "closeWindow" : null;

                if (mostrarMsg)//No muestra mensaje cuando viene de PVC
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: vCallBackFunction);

                if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                    btnAgregarPuestos.Visible = chkFgOtrosPuestos.Checked;
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vValidacion.MENSAJE.FirstOrDefault().DS_MENSAJE, vValidacion.CL_TIPO_ERROR, pCallBackFunction: null);
            }



        }

        protected void grdEvaluados_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            //SI EL PERIODO ES DE PVC NO ELIMINAR ITEM
            if (vPeriodo.FG_CREADO_POR_PVC)
            {
                if (vClIdioma != E_IDIOMA_ENUM.ES.ToString())
                {
                    TraduccionIdiomaTextoNegocio oNegocio = new TraduccionIdiomaTextoNegocio();
                    SPE_OBTIENE_TRADUCCION_TEXTO_Result vLstTextosTraduccion = oNegocio.ObtieneTraduccion(pCL_TEXTO: "CB_ELIMINA_PERIODO", pCL_MODULO: "FYD", pCL_PROCESO: "EC_CONFIGURACION", pCL_IDIOMA: "PORT").FirstOrDefault();
                    if (vLstTextosTraduccion != null)
                    {
                        UtilMensajes.MensajeResultadoDB(rwmMensaje, vLstTextosTraduccion.DS_TEXTO, E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: null);
                    }
                }
                else
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, "No se puede eliminar de un período creado desde plan de vida y carrera.", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: null);


            }
            else
            {
                if (e.Item.OwnerTableView.Name == "gtvPuestosEvaluacion")
                {
                    int vIdPeriodoPuestoEvaluado = 0;
                    if (int.TryParse(((GridDataItem)e.Item).GetDataKeyValue("ID_PUESTO_EVALUADO_PERIODO").ToString(), out vIdPeriodoPuestoEvaluado))
                    {
                        PeriodoNegocio nPeriodo = new PeriodoNegocio();
                        E_RESULTADO vResultado = nPeriodo.EliminaPuestoEvaluado(vIdPeriodoPuestoEvaluado);
                        string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                        UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

                        if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                            e.Item.OwnerTableView.Rebind();
                    }
                }
            }
        }

        protected void grdCuestionarios_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();

            List<SPE_OBTIENE_FYD_CUESTIONARIOS_EVALUADOS_Result> vLstEvaluadosCuestionarios = nPeriodo.ObtieneEvaluadosCuestionarios(vPeriodo.ID_PERIODO ?? 0, ContextoUsuario.oUsuario.ID_EMPRESA, vIdRol);

            grdCuestionarios.DataSource = vLstEvaluadosCuestionarios;
            ChangeEnableMensajeCampos(!vLstEvaluadosCuestionarios.Any(a => a.NO_CUESTIONARIOS > 0));
            ChangeEnableCuestionarios(!(vLstEvaluadosCuestionarios.Count > 0));
        }

        protected void grdCuestionarios_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem vDataItem = (GridDataItem)e.DetailTableView.ParentItem;
            switch (e.DetailTableView.Name)
            {
                case "gtvEvaluadores":
                    int vIdEvaluado = 0;
                    if (int.TryParse(vDataItem.GetDataKeyValue("ID_EVALUADO").ToString(), out vIdEvaluado))
                    {
                        PeriodoNegocio nPeriodo = new PeriodoNegocio();
                        e.DetailTableView.DataSource = nPeriodo.ObtieneCuestionariosEvaluado(vIdEvaluado);
                    }
                    break;
            }
        }

        protected void btnAgregarCampoAdicional_Click(object sender, EventArgs e)
        {
            /*
            XElement vXmlPreguntasAdicionales = new XElement("PREGUNTAS_ADICIONALES");
            vXmlPreguntasAdicionales.Add(lstCampoAdicional.Items.Where(w => !w.Value.Equals(String.Empty)).Select(s => new XElement("PREGUNTA", new XAttribute("ID_CAMPO", s.Value))));

            PeriodoNegocio nPeriodo = new PeriodoNegocio();

            E_CL_CUESTIONARIO_OBJETIVO vClCuestionarioObjetivo = E_CL_CUESTIONARIO_OBJETIVO.AMBOS;

            if (btnCuestionarioAutoevaluacion.Checked)
                vClCuestionarioObjetivo = E_CL_CUESTIONARIO_OBJETIVO.AUTOEVALUACION;

            if (btnCuestionarioOtros.Checked)
                vClCuestionarioObjetivo = E_CL_CUESTIONARIO_OBJETIVO.OTROS;

            E_RESULTADO vResultado = nPeriodo.InsertaPreguntasAdicionales(vPeriodo.ID_PERIODO ?? 0,"HOLA", vXmlPreguntasAdicionales, vClCuestionarioObjetivo, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
            {
                grdCamposAdicionales.Rebind();
                lstCampoAdicional.Items.Clear();
                lstCampoAdicional.Items.Add(new RadListBoxItem("No seleccionado", "0"));
            }
             * */
        }

        protected void grdCamposAdicionales_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            grdCamposAdicionales.DataSource = nPeriodo.ObtienePreguntasAdicionales(pIdPeriodo: vPeriodo.ID_PERIODO ?? 0);
        }

        //protected void grdCamposAdicionales_DeleteCommand(object sender, GridCommandEventArgs e)
        //{
        //    int vIdPregunta = int.Parse(((GridDataItem)e.Item).GetDataKeyValue("ID_PREGUNTA_ADICIONAL").ToString());

        //    PeriodoNegocio nPeriodo = new PeriodoNegocio();
        //    E_RESULTADO vResultado = nPeriodo.EliminaPreguntaAdicional(vIdPregunta);
        //    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

        //    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

        //    if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
        //        grdCamposAdicionales.Rebind();
        //}

        protected void grdCuestionarios_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item.OwnerTableView.Name == "gtvEvaluadores")
            {
                int vIdCuestionario = 0;
                if (int.TryParse(((GridDataItem)e.Item).GetDataKeyValue("ID_CUESTIONARIO").ToString(), out vIdCuestionario))
                {
                    PeriodoNegocio nPeriodo = new PeriodoNegocio();
                    E_RESULTADO vResultado = nPeriodo.EliminaCuestionario(vIdCuestionario);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

                    if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                        e.Item.OwnerTableView.Rebind();
                }
            }
        }

        protected void grdContrasenaEvaluadores_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            grdContrasenaEvaluadores.DataSource = nPeriodo.ObtieneTokenEvaluadores(vPeriodo.ID_PERIODO ?? 0, ContextoUsuario.oUsuario.ID_EMPRESA, pIdRol: vIdRol);
        }

        protected void btnReasignarTodasContrasenas_Click(object sender, EventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();

            E_RESULTADO vResultado = nPeriodo.InsertarActualizarTokenEvaluadores(vPeriodo.ID_PERIODO ?? 0, null, vClUsuario, vNbPrograma, pIdRol: vIdRol);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                grdContrasenaEvaluadores.Rebind();
        }

        protected void btnReasignarContrasena_Click(object sender, EventArgs e)
        {
            string vMensaje = "";
            foreach (GridDataItem item in grdContrasenaEvaluadores.SelectedItems)
            {
                PeriodoNegocio nPeriodo = new PeriodoNegocio();

                E_RESULTADO vResultado = nPeriodo.InsertarActualizarTokenEvaluadores(vPeriodo.ID_PERIODO ?? 0, int.Parse(item.GetDataKeyValue("ID_EVALUADOR").ToString()), vClUsuario, vNbPrograma, pIdRol: vIdRol);
                vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                if (!vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
                    return;
                }

                //if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                //    grdContrasenaEvaluadores.Rebind();
            }
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, E_TIPO_RESPUESTA_DB.SUCCESSFUL, pCallBackFunction: null);
            grdContrasenaEvaluadores.Rebind();
        }

        protected void rtsConfiguracionPeriodo_TabClick(object sender, RadTabStripEventArgs e)
        {
        }

        public void aplicarConfiguracionPeriodoPVC()
        {
            btnSeleccionPorPersona.Enabled = false;
            btnSeleccionPorPuesto.Enabled = false;
            btnSeleccionPorArea.Enabled = false;
            btnEliminarEvaluado.Enabled = false;
            btnAgregarPuestos.Enabled = false;
        }

        protected void grdCamposAdicionales_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdCamposAdicionales.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdCamposAdicionales.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdCamposAdicionales.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdCamposAdicionales.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdCamposAdicionales.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void grdEvaluados_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdEvaluados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdEvaluados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdEvaluados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdEvaluados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdEvaluados.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void grdEvaluadoresExternos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdEvaluadoresExternos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdEvaluadoresExternos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdEvaluadoresExternos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdEvaluadoresExternos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdEvaluadoresExternos.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }


            if(vClIdioma != E_IDIOMA_ENUM.ES.ToString())
            {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                RadButton pEdit= (RadButton)e.Item.FindControl("chkFormFgEvaluaTodos");
                if (pEdit != null)
                {
                    TraduccionIdiomaTextoNegocio oNegocio = new TraduccionIdiomaTextoNegocio();
                    var vEstado1 = oNegocio.ObtieneTraduccion(pCL_TEXTO: "vchkFormFgEvaluaTodos_Si", pCL_MODULO: "FYD", pCL_PROCESO: "EC_CONFIGURACION", pCL_IDIOMA: "PORT").FirstOrDefault();
                    var vEstado2 = oNegocio.ObtieneTraduccion(pCL_TEXTO: "vchkFormFgEvaluaTodos_No", pCL_MODULO: "FYD", pCL_PROCESO: "EC_CONFIGURACION", pCL_IDIOMA: "PORT").FirstOrDefault();
                    pEdit.ToggleStates[0].Text = vEstado1.DS_TEXTO;
                    pEdit.ToggleStates[1].Text = vEstado2.DS_TEXTO;
                }
            }
            }
        }

        protected void grdCuestionarios_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdCuestionarios.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdCuestionarios.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdCuestionarios.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdCuestionarios.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdCuestionarios.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }

       
        }

        protected void grdContrasenaEvaluadores_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdContrasenaEvaluadores.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdContrasenaEvaluadores.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdContrasenaEvaluadores.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdContrasenaEvaluadores.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdContrasenaEvaluadores.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            XElement vXmlPreguntas = new XElement("PREGUNTAS");
            foreach (GridDataItem item in grdCamposAdicionales.SelectedItems)
                vXmlPreguntas.Add(new XElement("PREGUNTA", new XAttribute("ID_PREGUNTA", item.GetDataKeyValue("ID_PREGUNTA_ADICIONAL").ToString())));

            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            E_RESULTADO vResultado = nPeriodo.EliminaPreguntaAdicional(vXmlPreguntas.ToString());
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                grdCamposAdicionales.Rebind();
        }

        protected void grdCamposAdicionales_PreRender(object sender, EventArgs e)
        {

            if (vClIdioma != E_IDIOMA_ENUM.ES.ToString())
            {
                //Asignar texto variables vista
                TraduccionIdiomaTextoNegocio oNegocio = new TraduccionIdiomaTextoNegocio();
                List<SPE_OBTIENE_TRADUCCION_TEXTO_Result> vLstTextosTraduccion = oNegocio.ObtieneTraduccion(pCL_MODULO: "FYD", pCL_PROCESO: "EC_CONFIGURACION", pCL_IDIOMA: "PORT");
                if (vLstTextosTraduccion.Count > 0)
                {
                    foreach (GridColumn col in grdCamposAdicionales.MasterTableView.Columns)
                    {
                        if (col.UniqueName == "NB_PREGUNTA")
                            col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdCamposAdicionales_NB_PREGUNTA").FirstOrDefault().DS_TEXTO;
                        if (col.UniqueName == "NB_CUESTIONARIO_OBJETIVO")
                            col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdCamposAdicionales_NB_CUESTIONARIO_OBJETIVO").FirstOrDefault().DS_TEXTO;
                    }

                    grdCamposAdicionales.Rebind();
                }
            }
        }

        protected void grdEvaluados_PreRender(object sender, EventArgs e)
        {
            if (vClIdioma != E_IDIOMA_ENUM.ES.ToString())
            {
                //Asignar texto variables vista
                TraduccionIdiomaTextoNegocio oNegocio = new TraduccionIdiomaTextoNegocio();
                List<SPE_OBTIENE_TRADUCCION_TEXTO_Result> vLstTextosTraduccion = oNegocio.ObtieneTraduccion(pCL_MODULO: "FYD", pCL_PROCESO: "EC_CONFIGURACION", pCL_IDIOMA: "PORT");
                if (vLstTextosTraduccion.Count > 0)
                {
                    foreach (GridColumn col in grdEvaluados.MasterTableView.Columns)
                    {
                        switch (col.UniqueName)
                        {
                            case "CL_EMPLEADO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_CL_EMPLEADO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "NB_EMPLEADO_COMPLETO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_NB_EMPLEADO_COMPLETO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "NB_EMPLEADO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_NB_EMPLEADO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "NB_APELLIDO_PATERNO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_NB_APELLIDO_PATERNO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "NB_APELLIDO_MATERNO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_NB_APELLIDO_MATERNO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_PUESTO_NB_PUESTO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_M_PUESTO_NB_PUESTO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_DEPARTAMENTO_CL_DEPARTAMENTO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_M_DEPARTAMENTO_CL_DEPARTAMENTO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "M_DEPARTAMENTO_NB_DEPARTAMENTO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_M_DEPARTAMENTO_NB_DEPARTAMENTO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "CL_GENERO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_CL_GENERO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "CL_ESTADO_CIVIL":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_CL_ESTADO_CIVIL").FirstOrDefault().DS_TEXTO;
                                break;
                            case "NB_CONYUGUE":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_NB_CONYUGUE").FirstOrDefault().DS_TEXTO;
                                break;
                            case "CL_RFC":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_CL_RFC").FirstOrDefault().DS_TEXTO;
                                break;
                            case "CL_CURP":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_CL_CURP").FirstOrDefault().DS_TEXTO;
                                break;
                            case "CL_NSS":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_CL_NSS").FirstOrDefault().DS_TEXTO;
                                break;
                            case "CL_TIPO_SANGUINEO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_CL_TIPO_SANGUINEO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "CL_NACIONALIDAD":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_CL_NACIONALIDAD").FirstOrDefault().DS_TEXTO;
                                break;
                            case "NB_PAIS":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_NB_PAIS").FirstOrDefault().DS_TEXTO;
                                break;
                            case "NB_ESTADO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_NB_ESTADO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "NB_MUNICIPIO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_NB_MUNICIPIO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "NB_COLONIA":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_NB_COLONIA").FirstOrDefault().DS_TEXTO;
                                break;
                            case "NB_CALLE":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_NB_CALLE").FirstOrDefault().DS_TEXTO;
                                break;
                            case "NO_EXTERIOR":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_NO_EXTERIOR").FirstOrDefault().DS_TEXTO;
                                break;
                            case "NO_INTERIOR":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_NO_INTERIOR").FirstOrDefault().DS_TEXTO;
                                break;
                            case "CL_CODIGO_POSTAL":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_CL_CODIGO_POSTAL").FirstOrDefault().DS_TEXTO;
                                break;
                            case "CL_CORREO_ELECTRONICO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_CL_CORREO_ELECTRONICO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "FE_NACIMIENTO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_FE_NACIMIENTO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "DS_LUGAR_NACIMIENTO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_DS_LUGAR_NACIMIENTO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "FE_ALTA":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_FE_ALTA").FirstOrDefault().DS_TEXTO;
                                break;
                            case "FE_BAJA":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_FE_BAJA").FirstOrDefault().DS_TEXTO;
                                break;
                            case "MN_SUELDO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_MN_SUELDO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "MN_SUELDO_VARIABLE":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_MN_SUELDO_VARIABLE").FirstOrDefault().DS_TEXTO;
                                break;
                            case "DS_SUELDO_COMPOSICION":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_DS_SUELDO_COMPOSICION").FirstOrDefault().DS_TEXTO;
                                break;
                            case "CL_EMPRESA":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_CL_EMPRESA").FirstOrDefault().DS_TEXTO;
                                break;
                            case "NB_EMPRESA":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_NB_EMPRESA").FirstOrDefault().DS_TEXTO;
                                break;
                            case "NB_RAZON_SOCIAL":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_NB_RAZON_SOCIAL").FirstOrDefault().DS_TEXTO;
                                break;
                            case "CL_ESTADO_EMPLEADO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_CL_ESTADO_EMPLEADO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "FG_ACTIVO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluados_FG_ACTIVO").FirstOrDefault().DS_TEXTO;
                                break;
                        }

                    }
                    grdEvaluados.MasterTableView.DetailTables[0].GetColumn("NB_PUESTO").HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgtvPuestosEvaluacion_NB_PUESTO").FirstOrDefault().DS_TEXTO;
                    (grdEvaluados.MasterTableView.DetailTables[0].GetColumn("DeleteColumn") as GridButtonColumn).ConfirmTextFormatString = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgtvPuestosEvaluacion_DeleteColumn_Confirm").FirstOrDefault().DS_TEXTO;
                    (grdEvaluados.MasterTableView.DetailTables[0].GetColumn("DeleteColumn") as GridButtonColumn).Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgtvPuestosEvaluacion_DeleteColumn").FirstOrDefault().DS_TEXTO;

                    grdEvaluados.Rebind();
                }
            }
        }

        protected void grdEvaluadoresExternos_PreRender(object sender, EventArgs e)
        {

            if (vClIdioma != E_IDIOMA_ENUM.ES.ToString())
            {
                //Asignar texto variables vista
                TraduccionIdiomaTextoNegocio oNegocio = new TraduccionIdiomaTextoNegocio();
                List<SPE_OBTIENE_TRADUCCION_TEXTO_Result> vLstTextosTraduccion = oNegocio.ObtieneTraduccion(pCL_MODULO: "FYD", pCL_PROCESO: "EC_CONFIGURACION", pCL_IDIOMA: "PORT");
                if (vLstTextosTraduccion.Count > 0)
                {
                    foreach (GridColumn col in grdEvaluadoresExternos.MasterTableView.Columns)
                    {
                        switch (col.UniqueName)
                        {
                            case "EditColumn":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluadoresExternos_EditColumn").FirstOrDefault().DS_TEXTO;
                                break;
                            case "CL_TIPO_EVALUADOR":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluadoresExternos_CL_TIPO_EVALUADOR").FirstOrDefault().DS_TEXTO;
                                break;
                            case "NB_EVALUADOR":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluadoresExternos_NB_EVALUADOR").FirstOrDefault().DS_TEXTO;
                                break;
                            case "NB_PUESTO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluadoresExternos_NB_PUESTO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "CL_CORREO_EVALUADOR":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluadoresExternos_CL_CORREO_EVALUADOR").FirstOrDefault().DS_TEXTO;
                                break;
                            case "CL_EVALUA_TODOS":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluadoresExternos_CL_EVALUA_TODOS").FirstOrDefault().DS_TEXTO;
                                break;
                        }
                    }
                    (grdEvaluadoresExternos.Columns[0] as GridEditCommandColumn).EditText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluadoresExternos_EditColumn").FirstOrDefault().DS_TEXTO;
                    (grdEvaluadoresExternos.Columns[0] as GridEditCommandColumn).InsertText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluadoresExternos_Insert").FirstOrDefault().DS_TEXTO;
                    (grdEvaluadoresExternos.Columns[0] as GridEditCommandColumn).UpdateText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluadoresExternos_Update").FirstOrDefault().DS_TEXTO;
                    (grdEvaluadoresExternos.MasterTableView.GetColumn("DeleteColumn") as GridButtonColumn).ConfirmTextFormatString = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluadoresExternos_DeleteColumn_Confirm").FirstOrDefault().DS_TEXTO;
                    (grdEvaluadoresExternos.Columns[6] as GridButtonColumn).Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdEvaluadoresExternos_DeleteColumn").FirstOrDefault().DS_TEXTO;


                    grdEvaluadoresExternos.Rebind();
                }
            }
        }

        protected void grdCuestionarios_PreRender(object sender, EventArgs e)
        {

            if (vClIdioma != E_IDIOMA_ENUM.ES.ToString())
            {
                //Asignar texto variables vista
                TraduccionIdiomaTextoNegocio oNegocio = new TraduccionIdiomaTextoNegocio();
                List<SPE_OBTIENE_TRADUCCION_TEXTO_Result> vLstTextosTraduccion = oNegocio.ObtieneTraduccion(pCL_MODULO: "FYD", pCL_PROCESO: "EC_CONFIGURACION", pCL_IDIOMA: "PORT");
                if (vLstTextosTraduccion.Count > 0)
                {
                    foreach (GridColumn col in grdCuestionarios.MasterTableView.Columns)
                    {
                        switch (col.UniqueName)
                        {
                            case "NB_EVALUADO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdCuestionarios_NB_EVALUADO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "NB_PUESTO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdCuestionarios_NB_PUESTO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "NB_DEPARTAMENTO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdCuestionarios_NB_DEPARTAMENTO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "NO_CUESTIONARIOS":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdCuestionarios_NO_CUESTIONARIOS").FirstOrDefault().DS_TEXTO;
                                break;
                            //detalle
                            case "EVALUADOR":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgtvEvaluadores_EVALUADOR").FirstOrDefault().DS_TEXTO;
                                break;
                            case "EVALUADOR_CL_EVAL":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgtvEvaluadores_EVALUADOR_CL_EVAL").FirstOrDefault().DS_TEXTO;
                                break;
                            case "EVALUADORCL_PUESTO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgtvEvaluadores_EVALUADORCL_PUESTO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "CL_ROL":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgtvEvaluadores_CL_ROL").FirstOrDefault().DS_TEXTO;
                                break;
                        }
                    }

                    grdCuestionarios.MasterTableView.DetailTables[0].GetColumn("NB_EVALUADOR").HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgtvEvaluadores_EVALUADOR").FirstOrDefault().DS_TEXTO;
                    grdCuestionarios.MasterTableView.DetailTables[0].GetColumn("NB_PUESTO").HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgtvEvaluadores_NB_PUESTO").FirstOrDefault().DS_TEXTO;
                    grdCuestionarios.MasterTableView.DetailTables[0].GetColumn("NB_ROL_EVALUADOR").HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgtvEvaluadores_CL_ROL").FirstOrDefault().DS_TEXTO;

                    (grdCuestionarios.MasterTableView.DetailTables[0].GetColumn("DeleteColumn") as GridButtonColumn).ConfirmTextFormatString = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgtvEvaluadores_DeleteColumn_Confirm").FirstOrDefault().DS_TEXTO;
                    (grdCuestionarios.MasterTableView.DetailTables[0].GetColumn("DeleteColumn") as GridButtonColumn).Text = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgtvEvaluadores_DeleteColumn").FirstOrDefault().DS_TEXTO;
                    grdCuestionarios.Rebind();
                }
            }
        }

        protected void grdContrasenaEvaluadores_PreRender(object sender, EventArgs e)
        {
            if (vClIdioma != E_IDIOMA_ENUM.ES.ToString())
            {
                //Asignar texto variables vista
                TraduccionIdiomaTextoNegocio oNegocio = new TraduccionIdiomaTextoNegocio();
                List<SPE_OBTIENE_TRADUCCION_TEXTO_Result> vLstTextosTraduccion = oNegocio.ObtieneTraduccion(pCL_MODULO: "FYD", pCL_PROCESO: "EC_CONFIGURACION", pCL_IDIOMA: "PORT");
                if (vLstTextosTraduccion.Count > 0)
                {
                    foreach (GridColumn col in grdContrasenaEvaluadores.MasterTableView.Columns)
                    {
                        switch (col.UniqueName)
                        {
                            case "NB_EVALUADOR":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdContrasenaEvaluadores_NB_EVALUADOR").FirstOrDefault().DS_TEXTO;
                                break;
                            case "NB_PUESTO":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdContrasenaEvaluadores_NB_PUESTO").FirstOrDefault().DS_TEXTO;
                                break;
                            case "CL_TOKEN":
                                col.HeaderText = vLstTextosTraduccion.Where(w => w.CL_TEXTO == "vgrdContrasenaEvaluadores_CL_TOKEN").FirstOrDefault().DS_TEXTO;
                                break;
                        }
                    }

                    grdContrasenaEvaluadores.Rebind();
                }
            }
        }
    }
}