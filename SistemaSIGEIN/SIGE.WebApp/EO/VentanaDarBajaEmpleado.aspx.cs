using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using SIGE.Entidades.Externas;
using SIGE.WebApp.Comunes;
using System.Xml.Linq;
using SIGE.Negocio.Utilerias;
using WebApp.Comunes;

namespace SIGE.WebApp.EO
{
    public partial class VentanaDarBajaEmpleado : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private string vNbFirstRadEditorTagName = "p";

        public int vIdEmpleado
        {
            get { return (int)ViewState["vs_vIdEmpleado"]; }
            set { ViewState["vs_vIdEmpleado"] = value; }
        }

        private bool vFgProgramarFecha
        {
            get { return (bool)ViewState["vs_vFgProgramarFecha"];}
            set { ViewState["vs_vFgProgramarFecha"] = value; }
        }

        public int? vIdCausaBaja
        {
            get { return (int?)ViewState["vs_vIdCausaBaja"]; }
            set { ViewState["vs_vIdCausaBaja"] = value; }
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    EmpleadoNegocio nEmpleado = new EmpleadoNegocio();
                    vIdEmpleado = int.Parse(Request.QueryString["ID"]);
                    var vEmpleado = nEmpleado.ObtenerEmpleado(ID_EMPLEADO: vIdEmpleado).FirstOrDefault();
                    txtClEmpleado.InnerText = vEmpleado.CL_EMPLEADO;
                    txtClPuesto.InnerText = vEmpleado.CL_PUESTO;
                    txtNombre.InnerText = vEmpleado.NB_EMPLEADO_COMPLETO;
                    txtPuesto.InnerText = vEmpleado.NB_PUESTO;
                    DateTime vFecha = DateTime.Now;
                    rdpFechaBaja.SelectedDate = vFecha;
                    rdpFechaBaja.MaxDate = vFecha;
                    lbFechaBaja.InnerText = "Fecha de baja:";
                    //btnProgramarTrue.Checked = false;
                    //btnProgramarFalse.Checked = true;
                    vFgProgramarFecha = false;
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            E_BAJA_EMPLEADO vBaja = new E_BAJA_EMPLEADO();

            vBaja.CL_EMPLEADO = txtClEmpleado.InnerText;
            vBaja.NB_EMPLEADO = txtNombre.InnerText;
            vBaja.FE_BAJA_EFECTIVA = rdpFechaBaja.SelectedDate;
            vBaja.ID_EMPLEADO = vIdEmpleado;
            XElement nodoPrincipal = new XElement("XML_COMENTARIOS", EditorContentToXml("COMENTARIOS", reComentarios.Content.Replace("&lt;",""), vNbFirstRadEditorTagName));
            vBaja.DS_COMENTARIOS = nodoPrincipal.ToString();

            foreach (RadListBoxItem item in lstCausaBaja.Items)
            {
                vIdCausaBaja = int.Parse(item.Value);
                vBaja.ID_CAUSA_ROTACION = (int)vIdCausaBaja;
            }
            //if (vIdCausaBaja != null)
            //{
                if (vFgProgramarFecha)
                {
                    if (rdpFechaBaja.SelectedDate != null)
                    {
                        RotacionPersonalNegocio nBaja = new RotacionPersonalNegocio();
                        E_RESULTADO vResultado = nBaja.InsertaBajaEmpleado(pBaja: vBaja, pCL_USUARIO: vClUsuario, pNB_PROGRAMA: vNbPrograma, pTIPO_TRANSACCION: E_TIPO_OPERACION_DB.I.ToString());
                        string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                        UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
                    }
                }
            else
            {
                RotacionPersonalNegocio nBaja = new RotacionPersonalNegocio();
                E_RESULTADO vResultado = nBaja.InsertaBajaManualEmpleado(pBaja: vBaja, pCL_USUARIO: vClUsuario, pNB_PROGRAMA: vNbPrograma, pTIPO_TRANSACCION: E_TIPO_OPERACION_DB.I.ToString());
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
                if (vResultado.CL_TIPO_ERROR.ToString() == "SUCCESSFUL")
                {
                    PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                    List<E_BAJAS_PERIODO_EDD> lstBajasEmpleados = new List<E_BAJAS_PERIODO_EDD>();
                    lstBajasEmpleados = nPeriodo.ObtieneBajasEDD(vBaja.ID_EMPLEADO).ToList();
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

            //}
            //else
            //{
            //    UtilMensajes.MensajeResultadoDB(rwmMensaje,"Seleccione la causa de la baja.",E_TIPO_RESPUESTA_DB.WARNING,pCallBackFunction:"");
            //}
        }

        //protected void btnProgramarTrue_Click(object sender, EventArgs e)
        //{
        //    lbFechaBaja.InnerText = "Fecha efectiva de la baja:";
        //    rdpFechaBaja.Enabled = true;
        //    rdpFechaBaja.MaxDate = DateTime.Today.AddYears(100);
        //    rdpFechaBaja.MinDate = DateTime.Today.AddDays(1);
        //    vFgProgramarFecha = true;

        //}

        //protected void btnProgramarFalse_Click(object sender, EventArgs e)
        //{
        //    lbFechaBaja.InnerText = "Programar fecha efectiva de baja:";
        //    rdpFechaBaja.MinDate = DateTime.Today.AddYears(-100);
        //    rdpFechaBaja.MaxDate = DateTime.Now;
        //    vFgProgramarFecha = false;
        //}

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