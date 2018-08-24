﻿using SIGE.Entidades;
using SIGE.Entidades.EvaluacionOrganizacional;
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
    public partial class VentanaEnvioSolicitudes : System.Web.UI.Page
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

        public string validarDsNotas(string vdsNotas)
        {
            E_NOTAS pNota = null;
            if (vdsNotas != null)
            {
                XElement vNotas = XElement.Parse(vdsNotas.ToString());
                if (ValidarRamaXml(vNotas, "NOTA"))
                {
                    pNota = vNotas.Elements("NOTA").Select(el => new E_NOTAS
                    {
                        DS_NOTA = UtilXML.ValorAtributo<string>(el.Attribute("DS_NOTA")),
                        FE_NOTA = (DateTime?)UtilXML.ValorAtributo(el.Attribute("FE_NOTA"), E_TIPO_DATO.DATETIME),
                    }).FirstOrDefault();
                }

                if (pNota.DS_NOTA != null)
                {
                    return pNota.DS_NOTA.ToString();
                }
                else return "";
            }
            else
            {
                return "";
            }
        }

        public Boolean ValidarRamaXml(XElement parentEl, string elementsName)
        {
            var foundEl = parentEl.Element(elementsName);

            if (foundEl != null)
            {
                return true;
            }
            return false;
        }

        private void CargarDatosContexto()
        {
            PeriodoDesempenoNegocio periodo = new PeriodoDesempenoNegocio();
            var oPeriodo = periodo.ObtienePeriodoDesempenoContexto(vIdPeriodo, null);
            if (oPeriodo != null)
            {
                txtClPeriodo.InnerText = oPeriodo.CL_PERIODO;
               // txtNbPeriodo.InnerText = oPeriodo.NB_PERIODO;
                txtPeriodos.InnerText = oPeriodo.DS_PERIODO;
                txtFechas.InnerText = oPeriodo.FE_INICIO.ToString("d") + " a " + oPeriodo.FE_TERMINO.Value.ToShortDateString();
                txtTipoMetas.InnerText = oPeriodo.CL_TIPO_PERIODO;
                txtTipoCapturista.InnerText = Utileria.LetrasCapitales(oPeriodo.CL_TIPO_CAPTURISTA);
                txtTipoBono.InnerText = oPeriodo.CL_TIPO_BONO;
                txtTipoPeriodo.InnerText = oPeriodo.CL_ORIGEN_CUESTIONARIO;

                if (oPeriodo.DS_NOTAS != null)
                {
                    XElement vNotas = XElement.Parse(oPeriodo.DS_NOTAS);
                    if (vNotas != null)
                    {
                        txtNotas.InnerHtml = validarDsNotas(vNotas.ToString());
                    }
                }
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
            string vUrl = ContextoUsuario.nbHost + "/Logon.aspx?ClProceso=DESEMPENO";
            GridItemCollection oListaEvaluadores = new GridItemCollection();
            XElement vXmlEvaluados = new XElement("EVALUADORES");

            if (string.IsNullOrEmpty(vDsMensaje))
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "El mensaje para el correo electrónico de las solicitudes no está definido. Por favor, configura el mensaje para poder enviar los correos. Los correos no fueron enviados.", E_TIPO_RESPUESTA_DB.WARNING, pAlto: 200);
                return;
            }

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
                            bool vEstatusCorreo = pe.EnvioCorreo(vClCorreo, vNbEvaluador, "Solicitud para calificar metas", vMensaje);

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
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "No hay personas para notificar el problema ocurrido, revisa la configuración del periodo", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
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
                bool vEstatusCorreo = pe.EnvioCorreo(vClCorreo, vNbEvaluador, "Periodo de desempeño " + vDatosPeriodo.NB_PERIODO, vMensaje);
                if(vEstatusCorreo)
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, "Ha ocurrido un problema en el periodo. Se ha enviado un correo a la persona que recibe las notificaciones", E_TIPO_RESPUESTA_DB.WARNING,400,200, pCallBackFunction: "");
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
                var vDatosPeriodo = nPeriodo.ObtienePeriodosDesempeno(vIdPeriodo).FirstOrDefault();

                if (vDatosPeriodo != null)
                {
                    CargarDatosContexto();
                }

                //vDsMensaje = ContextoApp.EO.MensajeCorreoEvaluador.dsMensaje;
                //vDsMensajeE = ContextoApp.EO.MensajeDesempenioEvaluador.dsMensaje;
                //vDsMensajeME = ContextoApp.EO.MensajeDesempenioMEvaluador.dsMensaje;
                //vDsMensajeEv = ContextoApp.EO.MensajeDesempenioEvaluado.dsMensaje;
                //vDsMensajeMEv = ContextoApp.EO.MensajeDesempenioMEvaluado.dsMensaje;
                vDsMensaje = ContextoApp.EO.Configuracion.MensajeCapturaResultados.dsMensaje;
                vDsMensajeE = ContextoApp.EO.Configuracion.MensajeImportantes.dsMensaje;
                vDsMensajeME = ContextoApp.EO.Configuracion.MensajeBajaNotificador.dsMensaje;
                vDsMensajeEv = ContextoApp.EO.Configuracion.MensajeImportantes.dsMensaje;
                vDsMensajeMEv = ContextoApp.EO.Configuracion.MensajeBajaNotificador.dsMensaje;
                lMensaje.InnerHtml = vDsMensaje;

                if (string.IsNullOrEmpty(vDsMensaje))
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, "El mensaje para el correo electrónico de las solicitudes no está definido. Por favor, configura el mensaje para poder enviar los correos.", E_TIPO_RESPUESTA_DB.WARNING, pAlto: 200);
                }
            }
        }

        protected void rgCorreos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            List<SPE_OBTIENE_EO_EVALUADORES_TOKEN_Result> vEvaluadores = new List<SPE_OBTIENE_EO_EVALUADORES_TOKEN_Result>();
            PeriodoDesempenoNegocio eoNegocio = new PeriodoDesempenoNegocio();
            vEvaluadores = eoNegocio.ObtenerEvaluadoresPeriodo(vIdPeriodo);
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

        protected void rgCorreos_ItemDataBound(object sender, GridItemEventArgs e)
         {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                int vIdEvaluador = int.Parse(item.GetDataKeyValue("ID_EVALUADOR").ToString());
                //Guid vFlEvaluador = Guid.Parse(item.GetDataKeyValue("FL_EVALUADOR").ToString());
                PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                var vEvaluacionEvaluado = nPeriodo.ObtenerPeriodoEvaluadorDesempeno(vIdEvaluador);
                if (vEvaluacionEvaluado.CL_ESTATUS_CAPTURA == "TERMINADO")
                {
                    item.Enabled = false;
                    item.SelectableMode = GridItemSelectableMode.None;
                    btnEnviarTodos.Enabled = false;
                    lbMensaje.Visible = true;
                }
            }
        }
    }
}