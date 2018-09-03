using SIGE.Entidades.Externas;
using SIGE.Negocio.EvaluacionOrganizacional;
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

namespace SIGE.WebApp.EO
{
    public partial class VentanaSolicitudesReplicas : System.Web.UI.Page
    {

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdRol;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int vIdPeriodo
        {
            get { return (int)ViewState["vs_vIdPeriodo"]; }
            set { ViewState["vs_vIdPeriodo"] = value; }
        }

        public int? vIdPeriodoNoEnviado
        {
            get { return (int?)ViewState["vs_vIdPeriodoNoEnviado"]; }
            set { ViewState["vs_vIdPeriodoNoEnviado"] = value; }
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

        public string vDsMensajeBajaReplica
        {
            get { return (string)ViewState["vs_vDsMensajeBajaReplica"]; }
            set { ViewState["vs_vDsMensajeBajaReplica"] = value; }
        }

        public string vMensaje;

        public bool vFgMasiva
        {
            get { return (bool)ViewState["vs_vFgMasiva"]; }
            set { ViewState["vs_vFgMasiva"] = value; }
        }

        #endregion

        #region Funciones

        private void InsertaEstatusEnvio(bool pFgEnviado, int pIdPeriodo)
        {
            PeriodoDesempenoNegocio pNegocio = new PeriodoDesempenoNegocio();
            E_RESULTADO vResultado = pNegocio.InsertaEstatusEnvioSolicitudes(pIdPeriodo, pFgEnviado, vClUsuario, vNbPrograma);
            vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            if (vResultado.CL_TIPO_ERROR != E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
            }
        }

        private void EnviarCorreo(bool pFgEnviarTodos)
        {

            ProcesoExterno pe = new ProcesoExterno();
            int vNoCorreosEnviados = 0;
            int vNoTotalCorreos = 0;
            int vIdEvaluador;
            string vClCorreo;
            string vNbEvaluador;
            string myUrl = ResolveUrl("~/Logon.aspx?ClProceso=DESEMPENO");
            string vUrl = ContextoUsuario.nbHost + myUrl;
            GridItemCollection oListaEvaluadores = new GridItemCollection();
            XElement vXmlEvaluados = new XElement("EVALUADORES");
            vIdPeriodoNoEnviado = null;


            if (pFgEnviarTodos)
                oListaEvaluadores = rgCorreos.Items;
            else
                oListaEvaluadores = rgCorreos.SelectedItems;

            //vNoTotalCorreos = oListaEvaluadores.Count;

            foreach (GridDataItem item in oListaEvaluadores)
            {
                int vIdPeriodoMasteTable;
               // bool vFgNoenviado;
                string vMensaje = vDsMensaje;
                vClCorreo = (item.FindControl("txtCorreo") as RadTextBox).Text;
                vNbEvaluador = item["NB_EVALUADOR"].Text;
                vIdEvaluador = int.Parse(item.GetDataKeyValue("ID_EVALUADOR").ToString());
                vIdPeriodoMasteTable = int.Parse(item.GetDataKeyValue("ID_PERIODO").ToString());

                if (vFgMasiva)
                {
                    PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                    var vPeriododDesempeno = nPeriodo.ObtienePeriodosDesempeno(pIdPeriodo: int.Parse(item.GetDataKeyValue("ID_PERIODO").ToString())).FirstOrDefault();
                    var resultado = nPeriodo.InsertaActualiza_PERIODO(vPeriododDesempeno.ID_PERIODO_DESEMPENO, vPeriododDesempeno.CL_PERIODO, vPeriododDesempeno.NB_PERIODO, vPeriododDesempeno.DS_PERIODO, vPeriododDesempeno.CL_ESTADO_PERIODO, vPeriododDesempeno.DS_NOTAS.ToString(), vPeriododDesempeno.FE_INICIO, (DateTime)vPeriododDesempeno.FE_TERMINO, vPeriododDesempeno.CL_TIPO_CAPTURISTA, vPeriododDesempeno.CL_TIPO_METAS, vClUsuario, vNbPrograma, "A", btnCapturaMasivaYes.Checked);
                }

                DateTime vFechaEnvio = Convert.ToDateTime(item.GetDataKeyValue("FE_ENVIO_SOLICITUD").ToString());
                if (vFechaEnvio.Date == DateTime.Now.Date)
                {
                    vNoTotalCorreos = vNoTotalCorreos + 1;
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
                                bool vEstatusCorreo = pe.EnvioCorreo(vClCorreo, vNbEvaluador, "Solicitud para calificar metas", vMensaje);



                                if (vEstatusCorreo)
                                {
                                    vXmlEvaluados.Add(new XElement("EVALUADOR", new XAttribute("ID_EVALUADOR", vIdEvaluador), new XAttribute("CL_CORREO_ELECTRONICO", vClCorreo)));
                                    vNoCorreosEnviados++;
                                    (item.FindControl("txtCorreo") as RadTextBox).EnabledStyle.BackColor = System.Drawing.Color.White;
                                    (item.FindControl("txtCorreo") as RadTextBox).HoveredStyle.BackColor = System.Drawing.Color.White;
                                    if (vIdPeriodoNoEnviado != vIdPeriodoMasteTable)
                                    {
                                        InsertaEstatusEnvio(true, vIdPeriodoMasteTable);
                                    }
                                }
                                else
                                {
                                    (item.FindControl("txtCorreo") as RadTextBox).EnabledStyle.BackColor = System.Drawing.Color.Gold;
                                    vIdPeriodoNoEnviado = vIdPeriodoMasteTable;
                                    InsertaEstatusEnvio(false, vIdPeriodoMasteTable);
                                }
                            }

                            else
                            {
                                (item.FindControl("txtCorreo") as RadTextBox).EnabledStyle.BackColor = System.Drawing.Color.Gold;
                                vIdPeriodoNoEnviado = vIdPeriodoMasteTable;
                                InsertaEstatusEnvio(false, vIdPeriodoMasteTable);
                            }

                        }
                        else
                        {
                            (item.FindControl("txtCorreo") as RadTextBox).EnabledStyle.BackColor = System.Drawing.Color.Gold;
                            vIdPeriodoNoEnviado = vIdPeriodoMasteTable;
                            InsertaEstatusEnvio(false, vIdPeriodoMasteTable);
                        }
                    }
                    else
                    {
                        (item.FindControl("txtCorreo") as RadTextBox).EnabledStyle.BackColor = System.Drawing.Color.Gold;
                        vIdPeriodoNoEnviado = vIdPeriodoMasteTable;
                        InsertaEstatusEnvio(false, vIdPeriodoMasteTable);
                    }
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
                if (vEstatusCorreo)
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, "Ha ocurrido un problema en el período. Se ha enviado un correo a la persona que recibe las notificaciones", E_TIPO_RESPUESTA_DB.WARNING, 400, 200, pCallBackFunction: "");
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;

            if (!IsPostBack)
            {
                if (Request.Params["PeriodoId"] != null)
                {
                    vIdPeriodo = int.Parse(Request.Params["PeriodoId"].ToString());

                }

                PeriodoDesempenoNegocio periodo = new PeriodoDesempenoNegocio();
                var oPeriodo = periodo.ObtienePeriodoDesempenoContexto(vIdPeriodo, null);
                if (oPeriodo != null)
                {
                    if (oPeriodo.CL_TIPO_CAPTURISTA == "Coordinador de evaluación")
                    {
                        btnEnviar.Enabled = false;
                        btnEnviarTodos.Enabled = false;
                        lbMensaje2.Visible = true;
                    }
                }

                var vEvaluadores = periodo.ObtenerEvaluadoresPeriodo(vIdPeriodo, vIdRol).FirstOrDefault();

                if (vEvaluadores != null)
                {
                    vFgMasiva = vEvaluadores.FG_CAPTURA_MASIVA;
                    if (vEvaluadores.FG_CAPTURA_MASIVA)
                    {
                        dvCapturaMasiva.Visible = true;
                        btnCapturaMasivaFalse.Checked = true;
                        btnEnviar.Visible = false;
                        UtilMensajes.MensajeResultadoDB(rwmMensaje, "Este período tiene metas idénticas para todos los participantes, si se trata de una meta compartida es posible que realices la captura de todo el grupo, el resultado que captures se aplicara a cada uno de los participantes. Selecciona la opción de captura masiva antes de enviar los correos.", E_TIPO_RESPUESTA_DB.SUCCESSFUL, pAlto: 250, pCallBackFunction: "");
                    }
                }
            }

            //vDsMensaje = ContextoApp.EO.MensajePeriodoDesempenio.dsMensaje;
            //vDsMensajeE = ContextoApp.EO.MensajeDesempenioEvaluador.dsMensaje;
            //vDsMensajeME = ContextoApp.EO.MensajeDesempenioMEvaluador.dsMensaje;
            //vDsMensajeEv = ContextoApp.EO.MensajeDesempenioEvaluado.dsMensaje;
            //vDsMensajeMEv = ContextoApp.EO.MensajeDesempenioMEvaluado.dsMensaje;
            vDsMensaje = ContextoApp.EO.Configuracion.MensajeCapturaResultados.dsMensaje;
            vDsMensajeE = ContextoApp.EO.Configuracion.MensajeBajaReplica.dsMensaje;
            vDsMensajeME = ContextoApp.EO.Configuracion.MensajeBajaNotificador.dsMensaje;
            vDsMensajeEv = ContextoApp.EO.Configuracion.MensajeBajaReplica.dsMensaje;
            vDsMensajeMEv = ContextoApp.EO.Configuracion.MensajeBajaNotificador.dsMensaje;
            lMensaje.InnerHtml = vDsMensaje;
            lMensaje.InnerHtml = vDsMensaje;
        }

        protected void rgCorreos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PeriodoDesempenoNegocio pNegocio = new PeriodoDesempenoNegocio();
            rgCorreos.DataSource = pNegocio.ObtenerEvaluadoresReplicas(vIdPeriodo);
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