using SIGE.Entidades;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;
using SIGE.Negocio.Utilerias;
using WebApp.Comunes;

namespace SIGE.WebApp.EO
{
    public partial class CapturarBajaPendiente : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private string vNbFirstRadEditorTagName = "p";

        private int? vIdBajaEmpleado
        {
            get { return (int?)ViewState["vs_vIdEvaluadoPeriodo"]; }
            set { ViewState["vs_vIdEvaluadoPeriodo"] = value; }
        }

        private int? vIdEmpleado
        {
            get { return (int?)ViewState["vs_vIdEmpleado"]; }
            set { ViewState["vs_vIdEmpleado"] = value; }
        }

        public int? vIdCausaBaja
        {
            get { return (int?)ViewState["vs_vIdCausaBaja"]; }
            set { ViewState["vs_vIdCausaBaja"] = value; }
        }

        public int? vIdCatalogoBaja
        {
            get { return (int?)ViewState["vs_vIdCatalogoBaja"]; }
            set { ViewState["vs_vIdCatalogoBaja"] = value; }
        }

        public string vMensaje;
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

        #endregion

        #region Funciones

        private XElement EditorContentToXml(string pNbNodoRaiz, string pDsContenido, string pNbTag)
        {
            return XElement.Parse(EncapsularRadEditorContent(XElement.Parse(String.Format("<{1}>{0}</{1}>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(pDsContenido)), pNbNodoRaiz)), pNbNodoRaiz));
        }

        private string EncapsularRadEditorContent(XElement nodo, string nbNodo)
        {
            if (nodo.Elements().Count() == 1)
                return EncapsularRadEditorContent((XElement)nodo.FirstNode, nbNodo);
            else
            {
                nodo.Name = nbNodo;
                return nodo.ToString();
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["pIdEmpleadoBaja"] != null)
                {
                    vIdBajaEmpleado = int.Parse(Request.Params["pIdEmpleadoBaja"].ToString());
                    vIdEmpleado = int.Parse(Request.Params["pIdEmpleado"].ToString());
                    RotacionPersonalNegocio nRotacion = new RotacionPersonalNegocio();
                    var vEmpleadoBaja = nRotacion.ObtieneBajasPendientes(vIdBajaEmpleado).FirstOrDefault();
                    txtNbEmpleado.InnerText = vEmpleadoBaja.CL_EMPLEADO + " - " + vEmpleadoBaja.NB_EMPLEADO;
                    txtNbPuesto.InnerText = vEmpleadoBaja.CL_PUESTO + " - " + vEmpleadoBaja.NB_PUESTO;
                    rdpFechaBaja.SelectedDate = vEmpleadoBaja.FE_BAJA_EFECTIVA;
                    XElement xmlComentarios = XElement.Parse(vEmpleadoBaja.DS_COMENTARIOS);
                    reComentarios.Content = xmlComentarios.Value;
                    CatalogoListaNegocio negocio = new CatalogoListaNegocio();
                    SPE_OBTIENE_C_CATALOGO_LISTA_Result vCatalogo = new SPE_OBTIENE_C_CATALOGO_LISTA_Result();
                    vCatalogo =negocio.ObtieneCatalogoLista().Where(w=>w.NB_CATALOGO_LISTA== "Causas de baja").FirstOrDefault();
                    vIdCatalogoBaja = vCatalogo.ID_CATALOGO_LISTA;
                    vDsMensaje = ContextoApp.EO.Configuracion.MensajeCapturaResultados.dsMensaje;
                    vDsMensajeE = ContextoApp.EO.Configuracion.MensajeImportantes.dsMensaje;
                    vDsMensajeME = ContextoApp.EO.Configuracion.MensajeBajaNotificador.dsMensaje;
                    vDsMensajeEv = ContextoApp.EO.Configuracion.MensajeImportantes.dsMensaje;
                    vDsMensajeMEv = ContextoApp.EO.Configuracion.MensajeBajaNotificador.dsMensaje;

                }
            }

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            
            DateTime? vFeBaja = rdpFechaBaja.SelectedDate;
            XElement nodoPrincipal = new XElement("XML_COMENTARIOS", EditorContentToXml("COMENTARIOS", reComentarios.Content.Replace("&lt;", ""), vNbFirstRadEditorTagName));
            string vDesComentarios = nodoPrincipal.ToString();

            foreach (RadListBoxItem item in lstCausaBaja.Items)
            {
                vIdCausaBaja = int.Parse(item.Value);
            }
            if (vIdCausaBaja != null)
            {
                if (rdpFechaBaja.SelectedDate != null)
                {
                    RotacionPersonalNegocio nBaja = new RotacionPersonalNegocio();
                    E_RESULTADO vResultado = nBaja.ActualizaBajaPendiente(vIdBajaEmpleado, vIdEmpleado, vIdCausaBaja, vDesComentarios, vFeBaja, vClUsuario, vNbPrograma, pTIPO_TRANSACCION: E_TIPO_OPERACION_DB.I.ToString());
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
                    if (vResultado.CL_TIPO_ERROR.ToString() == "SUCCESSFUL")
                    {
                        PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                        List<E_BAJAS_PERIODO_EDD> lstBajasEmpleados = new List<E_BAJAS_PERIODO_EDD>();
                        lstBajasEmpleados = nPeriodo.ObtieneBajasEDD(vIdEmpleado).ToList();
                        if (lstBajasEmpleados.Count() > 0)
                        {
                            foreach (E_BAJAS_PERIODO_EDD PeridoEDD in lstBajasEmpleados)
                            {
                                var validarProceso = nPeriodo.ValidaPeriodoDesempeno(PeridoEDD.ID_PERIODO).FirstOrDefault();
                                EnviarCorreos(validarProceso.VALIDACION, validarProceso.CL_CORREO_ELECTRONICO, validarProceso.NB_EMPLEADO_COMPLETO, PeridoEDD.ID_PERIODO);

                            }

                        }

                    }
                }
                else
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, "Seleccione la fecha de baja.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Seleccione la causa de la baja.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            }

        }

        private void EnviarCorreos(string validacion, string correo, string evaluador, int? IdPeriodo)
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
            var vDatosPeriodo = nPeriodo.ObtienePeriodosDesempeno(IdPeriodo).FirstOrDefault();

            vClCorreo = correo;
            vNbEvaluador = evaluador;

            if (Utileria.ComprobarFormatoEmail(vClCorreo) && (vClCorreo != null || vClCorreo == ""))
            {
                vMensaje = vMensaje.Replace("[NB_PERSONA]", vNbEvaluador);
                vMensaje = vMensaje.Replace("[CL_PERIODO]", vDatosPeriodo.NB_PERIODO);

                //Envío de correo
                bool vEstatusCorreo = pe.EnvioCorreo(vClCorreo, vNbEvaluador, "Período de desempeño " + vDatosPeriodo.NB_PERIODO, vMensaje);

            }
        }

    }
}