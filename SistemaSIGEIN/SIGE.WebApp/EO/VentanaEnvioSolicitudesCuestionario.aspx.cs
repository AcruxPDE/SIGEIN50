using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.EvaluacionOrganizacional;
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
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;
using WebApp.Comunes;

namespace SIGE.WebApp.EO
{
    public partial class VentanaEnvioSolicitudesCuestionario : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int vIdPeriodo
        {
            get { return (int)ViewState["vs_vIdPeriodo"]; }
            set { ViewState["vs_vIdPeriodo"] = value; }
        }


        public string vDsMensaje
        {
            get { return (string)ViewState["vs_ves_ds_mensaje"]; }
            set { ViewState["vs_ves_ds_mensaje"] = value; }
        }

        public string vDsMensajeE
        {
            get { return (string)ViewState["vs_ves_ds_mensaje_e"]; }
            set { ViewState["vs_ves_ds_mensaje_e"] = value; }
        }

        public string vDsMensajeME
        {
            get { return (string)ViewState["vs_ves_ds_mensaje_me"]; }
            set { ViewState["vs_ves_ds_mensaje_me"] = value; }
        }

        public string vDsMensajeEv
        {
            get { return (string)ViewState["vs_ves_ds_mensaje_ev"]; }
            set { ViewState["vs_ves_ds_mensaje_ev"] = value; }
        }

        public string vDsMensajeMEv
        {
            get { return (string)ViewState["vs_ves_ds_mensaje_mev"]; }
            set { ViewState["vs_ves_ds_mensaje_mev"] = value; }
        }

        public string vMensaje;
        #endregion

        #region Funciones


        private void EnviarCorreo(bool pFgEnviarTodos)
        {
            ProcesoExterno pe = new ProcesoExterno();

            int vNoCorreosEnviados = 0;
            int vNoTotalCorreos = 0;
            int vIdEvaluador;
            string vClCorreo;
            string vNbEvaluador;
            var myUrl = ResolveUrl("~/Logon.aspx?ClProceso=CUESTIONARIO");
            string vUrl = ContextoUsuario.nbHost + myUrl;
            GridItemCollection oListaEvaluadores = new GridItemCollection();
            XElement vXmlEvaluados = new XElement("EVALUADORES");

            if (pFgEnviarTodos)
                oListaEvaluadores = rgCorreos.Items;
            else
                oListaEvaluadores = rgCorreos.SelectedItems;

            vNoTotalCorreos = oListaEvaluadores.Count;

            foreach (GridDataItem item in oListaEvaluadores)
            {
                string vMensaje = vDsMensaje;
                vClCorreo = (item.FindControl("txtCorreo") as RadTextBox).Text;
                vNbEvaluador = item["NB_EVALUADOR"].Text;
                vIdEvaluador = int.Parse(item.GetDataKeyValue("ID_EVALUADOR").ToString());

                if (Utileria.ComprobarFormatoEmail(vClCorreo))
                {

                    if (item.GetDataKeyValue("FL_EVALUADOR") != null)
                    {
                        if (item.GetDataKeyValue("CL_TOKEN") != null)
                        {
                            vMensaje = vMensaje.Replace("[NB_EVALUADOR]", vNbEvaluador);
                            vMensaje = vMensaje.Replace("[URL]", vUrl + "&FlProceso=" + item.GetDataKeyValue("FL_EVALUADOR").ToString());
                            vMensaje = vMensaje.Replace("[CONTRASENA]", item.GetDataKeyValue("CL_TOKEN").ToString());

                            //Envío de correo
                            bool vEstatusCorreo = pe.EnvioCorreo(vClCorreo, vNbEvaluador, "Evaluación del desempeño", vMensaje);

                            if (vEstatusCorreo)
                            {
                                vXmlEvaluados.Add(new XElement("EVALUADOR", new XAttribute("ID_EVALUADOR", vIdEvaluador), new XAttribute("CL_CORREO_ELECTRONICO", vClCorreo)));
                                vNoCorreosEnviados++;

                                (item.FindControl("txtCorreo") as RadTextBox).EnabledStyle.BackColor = System.Drawing.Color.White;
                                (item.FindControl("txtCorreo") as RadTextBox).HoveredStyle.BackColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                (item.FindControl("txtCorreo") as RadTextBox).EnabledStyle.BackColor = System.Drawing.Color.Gold;
                            }
                        }

                        else
                        {
                            (item.FindControl("txtCorreo") as RadTextBox).EnabledStyle.BackColor = System.Drawing.Color.Gold;
                        }

                    }
                    else
                    {
                        (item.FindControl("txtCorreo") as RadTextBox).EnabledStyle.BackColor = System.Drawing.Color.Gold;
                    }
                }
                else
                {
                    (item.FindControl("txtCorreo") as RadTextBox).EnabledStyle.BackColor = System.Drawing.Color.Gold;
                }
            }

            if (vNoTotalCorreos == vNoCorreosEnviados)
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Los correos han sido enviados con éxito.", E_TIPO_RESPUESTA_DB.SUCCESSFUL);
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Se enviaron " + vNoCorreosEnviados.ToString() + " correos de " + vNoTotalCorreos.ToString() + " en total.", E_TIPO_RESPUESTA_DB.SUCCESSFUL, pCallBackFunction: "");
            }
        }

        private void EnviarCorreosFallo(string validacion, string correo, string evaluador)
        {
            ProcesoExterno pe = new ProcesoExterno();
            string vClCorreo;
            string vNbEvaluador;

            if (validacion == "NO_HAY_M_IMPORTANTE_EVALUADOR" || validacion == "NO_HAY_M_IMPORTANTE_EVALUADO")
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "No hay personas para notificar el problema ocurrido, revisa la configuración del período", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return;
            }
            else if (validacion == "SI_HAY_IMPORTANTE_EVALUADOR")
            {
                vMensaje = vDsMensajeE;
            }
            else if (validacion == "SI_HAY_M_IMPORTANTE_EVALUADOR")
            {
                vMensaje = vDsMensajeME;
            }
            else if (validacion == "SI_HAY_IMPORTANTE_EVALUADO")
            {
                vMensaje = vDsMensajeEv;
            }
            else if (validacion == "ENVIO_CORREO_M_IMPORTANTE_EVALUADO")
            {
                vMensaje = vDsMensajeMEv;
            }

            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            var vDatosPeriodo = nPeriodo.ObtienePeriodosDesempeno(vIdPeriodo).FirstOrDefault();

            vClCorreo = correo;
            vNbEvaluador = evaluador;

            if (Utileria.ComprobarFormatoEmail(vClCorreo))
            {
                vMensaje = vMensaje.Replace("[NB_PERSONA]", vNbEvaluador);
                vMensaje = vMensaje.Replace("[CL_PERIODO]", vDatosPeriodo.NB_PERIODO);

                //Envío de correo
                bool vEstatusCorreo = pe.EnvioCorreo(vClCorreo, vNbEvaluador, "Período de desempeño " + vDatosPeriodo.NB_PERIODO, vMensaje);
                if(vEstatusCorreo)
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, "Ha ocurrido un problema en el período. Se ha enviado un correo a la persona que recibe las notificaciones", E_TIPO_RESPUESTA_DB.WARNING,400,200, pCallBackFunction: "");
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                if (Request.Params["PeriodoId"] != null)
                {
                    vIdPeriodo = int.Parse(Request.Params["PeriodoId"].ToString());
                }

                PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                var vDatosPeriodo = nPeriodo.ObtienePeriodosDesempenoCuestionario(vIdPeriodo).FirstOrDefault();
                txtNbPeriodo.InnerText = vDatosPeriodo.NB_PERIODO;
                txtTipoPeriodo.InnerText = vDatosPeriodo.CL_TIPO_PERIODO;

            }
            vDsMensaje = ContextoApp.EO.MensajeCuestionario.dsMensaje;
            //vDsMensajeE = ContextoApp.EO.MensajeDesempenioEvaluador.dsMensaje;
            //vDsMensajeME = ContextoApp.EO.MensajeDesempenioMEvaluador.dsMensaje;
            //vDsMensajeEv = ContextoApp.EO.MensajeDesempenioEvaluado.dsMensaje;
            //vDsMensajeMEv = ContextoApp.EO.MensajeDesempenioMEvaluado.dsMensaje;
            //vDsMensaje = ContextoApp.EO.Configuracion.MensajeCapturaResultados.dsMensaje;
            vDsMensajeE = ContextoApp.EO.Configuracion.MensajeImportantes.dsMensaje;
            vDsMensajeME = ContextoApp.EO.Configuracion.MensajeBajaNotificador.dsMensaje;
            vDsMensajeEv = ContextoApp.EO.Configuracion.MensajeImportantes.dsMensaje;
            vDsMensajeMEv = ContextoApp.EO.Configuracion.MensajeBajaNotificador.dsMensaje;
            lMensaje.InnerHtml = "";

        }

        protected void rgCorreos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            List<SPE_OBTIENE_EO_EVALUADORES_TOKEN_Result> vEvaluadores = new List<SPE_OBTIENE_EO_EVALUADORES_TOKEN_Result>();
            PeriodoDesempenoNegocio eoNegocio = new PeriodoDesempenoNegocio();
            vEvaluadores = eoNegocio.ObtenerEvaluadoresPeriodo(vIdPeriodo, null);
            rgCorreos.DataSource = vEvaluadores;
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();

            if (rgCorreos.SelectedItems.Count == 0)
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "No ha seleccionado ningun evaluador.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            }
            else
            {
                var validarProceso = nPeriodo.ValidaPeriodoDesempeno(vIdPeriodo).FirstOrDefault();
                if (validarProceso.VALIDACION == "COMPLETO")
                {
                    EnviarCorreo(false);
                }
                else
                {
                    EnviarCorreosFallo(validarProceso.VALIDACION, validarProceso.CL_CORREO_ELECTRONICO, validarProceso.NB_EMPLEADO_COMPLETO);
                }

            }
        }

        protected void btnEnviarTodos_Click(object sender, EventArgs e)
        {
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            var validarProceso = nPeriodo.ValidaPeriodoDesempeno(vIdPeriodo).FirstOrDefault();
            if (validarProceso.VALIDACION == "COMPLETO")
            {
                EnviarCorreo(true);
            }
            else
            {
                EnviarCorreosFallo(validarProceso.VALIDACION, validarProceso.CL_CORREO_ELECTRONICO, validarProceso.NB_EMPLEADO_COMPLETO);
            }
        }
    }
}