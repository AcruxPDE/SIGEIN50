using Newtonsoft.Json;
using SIGE.Entidades.Externas;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using SIGE.Entidades;
using Telerik.Web.UI;
using WebApp.Comunes;
using SIGE.Negocio.Utilerias;


namespace SIGE.WebApp.EO
{
    public partial class VentanaNuevoPeriodoDesempeno : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private string vNbFirstRadEditorTagName = "p";
        private string vEstadoPeriodo;
        private DateTime? vFechaInicio;
        private DateTime? vFechaTermino;
        private int vIdPeriodoDesempeno;
        public string vCapturistaResultado;

        #region Variables

        private int vIdPeriodo
        {
            get { return (int)ViewState["vsIdPeriodo"]; }
            set { ViewState["vsIdPeriodo"] = value; }
        }

        private int? vIdPeriodoCopia
        {
            get { return (int?)ViewState["vs_vIdPeriodoCopia"]; }
            set { ViewState["vs_vIdPeriodoCopia"] = value; }
        }

        private string vTipoMetas
        {
            get { return (string)ViewState["vs_vTipoMetas"]; }
            set { ViewState["vs_vTipoMetas"] = value; }
        }

        #endregion

        protected void SeguridadProcesos()
        {
            btnAceptar.Enabled = ContextoUsuario.oUsuario.TienePermiso("M.A.A.B");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime fechaActual = DateTime.Today;
                FeInicio.SelectedDate = fechaActual;
                FeTerminacion.SelectedDate = fechaActual.AddMonths(3);
                btnConsecuente.Checked = true;
                if (Request.QueryString["PeriodoId"] != null)
                {
                    if (Request.QueryString["Tipo"] == "COPIA")
                    {

                        vIdPeriodoCopia = int.Parse((Request.QueryString["PeriodoId"]));
                        divCopiaPeriodo.Visible = true;
                    }
                    else
                    {
                        vIdPeriodoCopia = 0;
                        SeguridadProcesos();
                    }
             
                    vIdPeriodo = int.Parse(Request.QueryString["PeriodoId"]);
                    PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                    var vPeriododDesempeno = nPeriodo.ObtienePeriodosDesempeno(pIdPeriodo: vIdPeriodo).FirstOrDefault();

                    if (Request.QueryString["Tipo"] != "COPIA")
                    {
                        txtDsPeriodo.Text = vPeriododDesempeno.CL_PERIODO;
                        txtDsDescripcion.Text = vPeriododDesempeno.DS_PERIODO;
                        //txtDsNotas.Content = vPeriododDesempeno.DS_NOTAS;
                        vEstadoPeriodo = vPeriododDesempeno.CL_ESTADO_PERIODO;
                        if (vPeriododDesempeno.CL_ESTADO_PERIODO == "CERRADO")
                            btnAceptar.Enabled = false;

                        if (vPeriododDesempeno.DS_NOTAS != null)
                        {
                            if (vPeriododDesempeno.DS_NOTAS.Contains("DS_NOTA"))
                            {
                                txtDsNotas.Content = Utileria.MostrarNotas(vPeriododDesempeno.DS_NOTAS);
                            }
                            else
                            {
                                XElement vRequerimientos = XElement.Parse(vPeriododDesempeno.DS_NOTAS);
                                if (vRequerimientos != null)
                                {
                                    vRequerimientos.Name = vNbFirstRadEditorTagName;
                                    txtDsNotas.Content = vRequerimientos.ToString();
                                }
                            }
                        }
                    }


                    FeInicio.SelectedDate = vPeriododDesempeno.FE_INICIO;
                    FeTerminacion.SelectedDate = vPeriododDesempeno.FE_TERMINO;
                    if (vPeriododDesempeno.CL_TIPO_METAS == "CERO")
                    {
                        rbMetasCero.Checked = true;
                        rbMetasCero.Enabled = false;
                        rbMetasDescriptivo.Enabled = false;
                    }
                    else if (vPeriododDesempeno.CL_TIPO_METAS == "DESCRIPTIVO")
                    {
                        rbMetasDescriptivo.Checked = true;
                        rbMetasCero.Enabled = false;
                        rbMetasDescriptivo.Enabled = false;
                    }

                    if (vPeriododDesempeno.CL_TIPO_CAPTURISTA == "COORDINADOR_EVAL")
                        rbCoordinadorEvaluacion.Checked = true;
                    if (vPeriododDesempeno.CL_TIPO_CAPTURISTA == "OCUPANTE_PUESTO")
                        rbOcupantePuesto.Checked = true;
                    if (vPeriododDesempeno.CL_TIPO_CAPTURISTA == "JEFE_INMEDIATO")
                        rbJefeInmediato.Checked = true;
                    if (vPeriododDesempeno.CL_TIPO_CAPTURISTA == "OTRO")
                        rbOtro.Checked = true;

                    if (vPeriododDesempeno.CL_TIPO_COPIA != null && Request.QueryString["Tipo"] != "COPIA")
                    {
                        if (vPeriododDesempeno.CL_TIPO_COPIA == "INDEPENDIENTE")
                        {
                            btnIndependiente.Checked = true;
                            divCopiaPeriodo.Visible = true;
                            btnIndependiente.Enabled = false;
                            btnConsecuente.Enabled = false;
                        }
                        else 
                        {
                            btnConsecuente.Checked = true;
                            divCopiaPeriodo.Visible = true;
                            btnIndependiente.Enabled = false;
                            btnConsecuente.Enabled = false;
                        }
                    }

                    if (vPeriododDesempeno.NO_REPLICA > 0)
                        txtDsPeriodo.Enabled = false;

                    if(vPeriododDesempeno.CL_ORIGEN_CUESTIONARIO=="REPLICA")
                        txtDsPeriodo.Enabled = false;

                    if (Request.QueryString["Tipo"] == "COPIA")
                        txtDsPeriodo.Enabled = true;

                    //var listaEvaluados = nPeriodo.ObtieneEvaluados(vIdPeriodo);
                    //if (listaEvaluados.Count != 0)
                    //{
                    //    rbCoordinadorEvaluacion.Enabled = false;
                    //    rbOcupantePuesto.Enabled = false;
                    //    rbJefeInmediato.Enabled = false;
                    //    rbOtro.Enabled = false;
                    //}
                }
                else
                {
                    vIdPeriodo = 0;
                    rbMetasDescriptivo.Checked = true;
                }
            }
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            if (vEstadoPeriodo == null)
            vEstadoPeriodo = "Abierto";
            vFechaInicio = FeInicio.SelectedDate;
            vFechaTermino = FeTerminacion.SelectedDate;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            string vNotas = "";

            if (txtDsPeriodo.Text == "" || txtDsDescripcion.Text == "")
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "El nombre y la descripción son obligatorios", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, null);
                return;
            }
            if (vFechaInicio.HasValue == false || vFechaTermino.HasValue == false)
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "La fecha de inicio y término es obligatorio", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, null);
                return;
            }

            if (rbCoordinadorEvaluacion.Checked == false && rbOcupantePuesto.Checked == false && rbJefeInmediato.Checked == false && rbOtro.Checked == false)
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Debes elegir un capturista de resultados", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, null);
                return;
            }

            if (rbMetasCero.Checked == true)
            {
                vTipoMetas = "CERO";
            }
            else if (rbMetasDescriptivo.Checked)
            {
                vTipoMetas = "DESCRIPTIVO";
            }

            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            if (rbCoordinadorEvaluacion.Checked)
                vCapturistaResultado = "COORDINADOR_EVAL";
            if (rbOcupantePuesto.Checked)
                vCapturistaResultado = "OCUPANTE_PUESTO";
            if (rbJefeInmediato.Checked)
                vCapturistaResultado = "JEFE_INMEDIATO";
            if (rbOtro.Checked)
                vCapturistaResultado = "OTRO";

            //if (vCapturistaResultado == "COORDINADOR_EVAL")
            //{
            //    ConfiguracionNegocio nConfiguracion = new ConfiguracionNegocio();
            //    var capturista = nConfiguracion.ObteneConfiguracionEvaluacionOrganizacional("CAPTURISTA").FirstOrDefault();
            //    if (capturista == null)
            //    {
            //        UtilMensajes.MensajeResultadoDB(rwmMensaje, "No hay coordinador registrado en la configuración", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, null);
            //        return;
            //    }
            //}

            //XElement nodoPrincipal = new XElement("XML_NOTAS", EditorContentToXml("DS_NOTAS", txtDsNotas.Content.Replace("&lt;", ""), vNbFirstRadEditorTagName));
            XElement nodoPrincipal = Utileria.GuardarNotas(txtDsNotas.Content, "XML_NOTAS");
            
            string vAccion = (vIdPeriodo != 0 ? "A" : "I");

            if (Request.QueryString["TIPO"] == "COPIA" && vIdPeriodoCopia != 0)
            {
                E_PERIODO_DESEMPENO vPeriodo = new E_PERIODO_DESEMPENO();

                vPeriodo.ID_PERIODO = (int)vIdPeriodoCopia;
                vPeriodo.CL_TIPO_PERIODO = "C_"+txtDsPeriodo.Text;
                vPeriodo.NB_PERIODO = txtDsPeriodo.Text;
                vPeriodo.DS_PERIODO = txtDsDescripcion.Text;
                vPeriodo.CL_ESTADO = vEstadoPeriodo;
                vPeriodo.XML_DS_NOTAS = Utileria.GuardarNotas(txtDsNotas.Content, "XML_NOTAS").ToString();
                vPeriodo.FE_INICIO_PERIODO = vFechaInicio.Value;
                vPeriodo.FE_TERMINO_PERIODO = vFechaTermino.Value;
                vPeriodo.CL_TIPO_CAPTURISTA = vCapturistaResultado;
                vPeriodo.CL_TIPO_COPIA = (btnConsecuente.Checked == true) ? "CONSECUENTE" : "INDEPENDIENTE";

                E_RESULTADO vResultado = nPeriodo.InsertaPeriodoDesempenoCopia(pPeriodo: vPeriodo, pCL_USUARIO: vClUsuario, pNB_PROGRAMA: vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "closeWindow");
            }
            else
            {

                if (vAccion == "I")
                {
                    var resultado = nPeriodo.InsertaActualiza_PERIODO(null, txtDsPeriodo.Text, txtDsPeriodo.Text, txtDsDescripcion.Text, vEstadoPeriodo, nodoPrincipal.ToString(), vFechaInicio.Value, vFechaTermino.Value, vCapturistaResultado, vTipoMetas, vClUsuario, vNbPrograma, vAccion, null);
                    //UtilMensajes.MensajeResultadoDB(rwmMensaje, resultado.MENSAJE[0].DS_MENSAJE.ToString(), resultado.CL_TIPO_ERROR, 400, 150, "sendDataToParent(" + vIdPeriodo + ")"); Verificar si se usa este sendDataToParent? se cambio por closeWindow
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, resultado.MENSAJE[0].DS_MENSAJE.ToString(), resultado.CL_TIPO_ERROR, 400, 150, "closeWindow");
                }
                else
                {
                    var vPeriododDesempeno = nPeriodo.ObtienePeriodosDesempeno(pIdPeriodo: vIdPeriodo).FirstOrDefault();
                    vIdPeriodoDesempeno = vPeriododDesempeno.ID_PERIODO_DESEMPENO;
                    var resultado = nPeriodo.InsertaActualiza_PERIODO(vIdPeriodoDesempeno, txtDsPeriodo.Text, txtDsPeriodo.Text, txtDsDescripcion.Text, vEstadoPeriodo, nodoPrincipal.ToString(), vFechaInicio.Value, vFechaTermino.Value, vCapturistaResultado, vTipoMetas, vClUsuario, vNbPrograma, vAccion, null);
                    //UtilMensajes.MensajeResultadoDB(rwmMensaje, resultado.MENSAJE[0].DS_MENSAJE.ToString(), resultado.CL_TIPO_ERROR, 400, 150, "sendDataToParent(" + vIdPeriodo + ")");// Se cambia sendatatoparent, en realidad se usaba esto??
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, resultado.MENSAJE[0].DS_MENSAJE.ToString(), resultado.CL_TIPO_ERROR, 400, 150, "cerrarVentanaActualiza");
                }
            }
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
    }
}