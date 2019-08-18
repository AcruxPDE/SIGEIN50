using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.WebApp.Comunes;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Entidades.IntegracionDePersonal;
using System.Xml.Linq;
using SIGE.Entidades;
using SIGE.Negocio.Utilerias;
namespace SIGE.WebApp.IDP
{
    public partial class VentanaRedaccion : System.Web.UI.Page
    {
        #region Propiedades
        private string vClUsuario;
        private string vNbPrograma;
        /*private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        */
        public int vTiempoRedaccion
        {
            get { return (int)ViewState["vsTiempoRedaccion"]; }
            set { ViewState["vsTiempoRedaccion"] = value; }
        }
        private int vIdPrueba
        {
            get { return (int)ViewState["vsIdPrueba"]; }
            set { ViewState["vsIdPrueba"] = value; }
        }

        private Guid vClToken
        {
            get { return (Guid)ViewState["vsClToken"]; }
            set { ViewState["vsClToken"] = value; }
        }

        private DateTime? vNow
        {
            get { return (DateTime?)ViewState["vsNow"]; }
            set { ViewState["vsNow"] = value; }
        }

        public List<E_PREGUNTA> vPreguntas
        {
            get { return (List<E_PREGUNTA>)ViewState["vPreguntas"]; }
            set { ViewState["vsPreguntas"] = value; }
        }
        public string vTipoRevision
        {
            get { return (string)ViewState["vsTipoRevision"]; }
            set { ViewState["vsTipoRevision"] = value; }
        }

        public bool MostrarCronometro
        {
            get { return (bool)ViewState["vsMostrarCronometroRED"]; }
            set { ViewState["vsMostrarCronometroRED"] = value; }
        }

        public int vIdBateria
        {
            get { return (int)ViewState["vsIdBateria"]; }
            set { ViewState["vsIdBateria"] = value; }
        }

        public Guid vClTokenBateria
        {
            get { return (Guid)ViewState["vsClTokenBateria"]; }
            set { ViewState["vsClTokenBateria"] = value; }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = (ContextoUsuario.oUsuario != null ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO");

            if (!IsPostBack)
            {
               
                if (Request.QueryString["ID"] != null && Request.QueryString["T"] != null)
                {
                    if (Request.QueryString["MOD"] != null)
                    {
                        vTipoRevision = Request.QueryString["MOD"];
                    }
                    PruebasNegocio nPrueba = new PruebasNegocio();
                    if (vNow == null)
                    {
                        vNow = DateTime.Now;
                    }
                    vIdPrueba = int.Parse(Request.QueryString["ID"]);
                    vClToken = Guid.Parse(Request.QueryString["T"].ToString());
                    if (Request.QueryString["vIdBateria"] != null)
                    {
                        vIdBateria = int.Parse(Request.QueryString["vIdBateria"]);
                        btnEliminar.Visible = true;
                        btnEliminarBateria.Visible = true;
                    }
                    else
                    {
                        vIdBateria = int.Parse(Request.QueryString["IDB"]);
                        vClTokenBateria = new Guid(Request.QueryString["TB"]);
                        btnEliminar.Visible = false;
                        btnEliminarBateria.Visible = false;
                    }

                    MostrarCronometro = ContextoApp.IDP.ConfiguracionPsicometria.FgMostrarCronometro;

                    //Si el modo de revision esta activado
                    if (vTipoRevision == "REV")
                    {
                        cronometro.Visible = false;
                        vTiempoRedaccion = 0;
                        btnTerminar.Enabled = false;
                        //obtener respuestas
                        var respuestas = nPrueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);
                        asignarValores(respuestas);
                    }
                    else if (vTipoRevision == "EDIT")
                    {
                        //btnEliminar.Visible = true;// Se agrega para la nueva forma de navegación 06/06/2018
                        cronometro.Visible = false;
                        vTiempoRedaccion = 0;
                        btnTerminar.Visible = false;
                        btnCorregir.Visible = true;
                        //obtener respuestas
                        var respuestas = nPrueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);
                        asignarValores(respuestas);
                    }
                    else
                    {
                        var lstPrueba = nPrueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken);
                        if (lstPrueba.Count == 1)
                        {
                            var vPruebaObj = lstPrueba[0];
                            var tiempoTotal = vPruebaObj.NO_TIEMPO * 60;
                            if (vPruebaObj.FE_INICIO.HasValue)
                            {
                                var tiempoTranscurrido = DateTime.Now.Subtract(vPruebaObj.FE_INICIO.Value);
                                vTiempoRedaccion = tiempoTotal - (int)tiempoTranscurrido.TotalSeconds;
                            }
                            else
                                vTiempoRedaccion = tiempoTotal;
                        }
                        else
                            vTiempoRedaccion = 0;
                        /*var vObjetoPrueba = nPrueba.INICIAR_K_PRUEBA(pIdPrueba: vIdPrueba, pFeInicio: vNow.Value, pClTokenExterno: vClToken, usuario: vClUsuario, programa: vNbPrograma);

                        if (vObjetoPrueba != null)
                        {
                            //       //Si el modo de revision esta activado
                            //if (vTipoRevision == "REV")
                            //{
                            //    cronometro.Visible = false;
                            //    vTiempoRedaccion = 0;
                            //    btnTerminar.Enabled = false;
                            //    //obtener respuestas
                            //    var respuestas = nPrueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);
                            //    asignarValores(respuestas);
                            //}
                            //else if (vTipoRevision == "EDIT")
                            //{
                            //    cronometro.Visible = false;
                            //    vTiempoRedaccion = 0;
                            //    btnTerminar.Visible = false;
                            //    btnCorregir.Visible = true;
                            //    //obtener respuestas
                            //    var respuestas = nPrueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);
                            //    asignarValores(respuestas);
                            //}
                            //else
                            //{
                            if (vObjetoPrueba.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.ERROR)
                            {
                                vTiempoRedaccion = 0;
                            }
                            else if (vObjetoPrueba.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                            {
                                vTiempoRedaccion = int.Parse(vObjetoPrueba.MENSAJE.Where(r => r.CL_IDIOMA.Equals("ES")).FirstOrDefault().DS_MENSAJE.ToString());
                                if (vTiempoRedaccion <= 0)
                                {
                                    //UtilMensajes.MensajeResultadoDB(rnMensaje, "La prueba no esta disponible.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "CloseTest");
                                    //btnTerminar.Enabled = false;
                                }
                            }
                        }*/
                    }
                }
                else
                {
                    vTiempoRedaccion = 0;
                    UtilMensajes.MensajeResultadoDB(rnMensaje, "No existe la prueba", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "CloseTest");
                    btnTerminar.Enabled = false;
                }

            }
        }

        public void asignarValores(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> respuestas)
        {
            foreach (SPE_OBTIENE_RESULTADO_PRUEBA_Result resp in respuestas)
            {
                switch (resp.CL_PREGUNTA)
                {
                    case "REDACCION-A-0001": radPreg1Resp1.Content = (resp.NB_RESPUESTA != null ? resp.NB_RESPUESTA.ToString() : ""); break;
                        
                }
            }
        }

        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            radPreg1Resp1.EditModes = radPreg1Resp1.EditModes ^ Telerik.Web.UI.EditModes.Html;
            PruebasNegocio nKprueba = new PruebasNegocio();
            var vObjetoPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba,  pClTokenExterno: vClToken);

            PreguntaNegocio nPregunta = new PreguntaNegocio();
            var vPregunta = nPregunta.Obtener_K_PREGUNTA(ID_PRUEBA: vIdPrueba, CL_TOKEN_EXTERNO: vClToken);
            if (vPregunta.Count > 0)
            {

                if (radPreg1Resp1.Content.ToString().Length > 0)
                {
                    BackQuestionObject(radPreg1Resp1.Content.ToString(), vPregunta.Where(r => r.CL_PREGUNTA.Equals("REDACCION-A-0001")).FirstOrDefault());
                    var vXelements = vPregunta.Select(x =>
                                                    new XElement("RESPUESTA",
                                                    new XAttribute("ID_PREGUNTA", x.ID_PREGUNTA),
                                                    new XAttribute("ID_CUESTIONARIO_PREGUNTA", x.ID_CUESTIONARIO_PREGUNTA),
                                                    new XAttribute("NB_PREGUNTA", x.NB_PREGUNTA),
                                                    new XAttribute("NB_RESPUESTA", x.NB_RESPUESTA),
                                                    new XAttribute("NO_VALOR_RESPUESTA", x.NO_VALOR_RESPUESTA),
                                                    new XAttribute("CL_PREGUNTA", x.CL_PREGUNTA)
                                         ));
                    XElement RESPUESTAS =
                    new XElement("RESPUESTAS", vXelements
                    );

                    CuestionarioPreguntaNegocio nCustionarioPregunta = new CuestionarioPreguntaNegocio();
                    PruebasNegocio objPrueba = new PruebasNegocio();
                    var vPrueba = objPrueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();

                    if (vPrueba != null)
                    {

                        SPE_OBTIENE_K_PRUEBA_Result vPruebaTerminada = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();
                        vPruebaTerminada.FE_TERMINO = DateTime.Now;
                        vPruebaTerminada.CL_ESTADO = E_ESTADO_PRUEBA.TERMINADA.ToString();
                        vPruebaTerminada.NB_TIPO_PRUEBA = "APLICACION";
                        E_RESULTADO vResultados = nKprueba.InsertaActualiza_K_PRUEBA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
                        string vMensaje = vResultados.MENSAJE.Where(w => w.CL_IDIOMA.Equals("ES")).FirstOrDefault().DS_MENSAJE;                        
                        if (vResultados.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.WARNING && vMensaje == "NO")
                        {
                                UtilMensajes.MensajeResultadoDB(rnMensaje, "Usted ha tratado de capturar doble una prueba; los datos no fueron guardados.", E_TIPO_RESPUESTA_DB.SUCCESSFUL, 400, 150, "CloseTest");
                        }
                        else
                        {

                            E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: "REDACCION", usuario: vClUsuario, programa: vNbPrograma);
                            string vMensajes = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals("ES")).FirstOrDefault().DS_MENSAJE;
                            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensajes, vResultado.CL_TIPO_ERROR, 400, 150, "CloseTest");
                        }
                       
                    }
                }
                else 
                {
                    string vMensaje = "Debes de contestar la prueba.";
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, E_TIPO_RESPUESTA_DB.WARNING, 400, 150, "");
                }
            }
        }


        public void BackQuestionObject(string pnbRespuesta, SPE_OBTIENE_K_PREGUNTA_Result pPregunta)
        {
                pPregunta.NB_RESPUESTA = pnbRespuesta;
                pPregunta.NO_VALOR_RESPUESTA = (pnbRespuesta.ToUpper() != "" ? 1 : 0);
        }

        protected void btnCorregir_Click(object sender, EventArgs e)
        {
            radPreg1Resp1.EditModes = radPreg1Resp1.EditModes ^ Telerik.Web.UI.EditModes.Html;
            PruebasNegocio nKprueba = new PruebasNegocio();
            var vObjetoPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken);

            PreguntaNegocio nPregunta = new PreguntaNegocio();
            var vPregunta = nPregunta.Obtener_K_PREGUNTA(ID_PRUEBA: vIdPrueba, CL_TOKEN_EXTERNO: vClToken);
            if (vPregunta.Count > 0)
            {

                if (radPreg1Resp1.Content.ToString().Length > 0)
                {
                    BackQuestionObject(radPreg1Resp1.Content.ToString(), vPregunta.Where(r => r.CL_PREGUNTA.Equals("REDACCION-A-0001")).FirstOrDefault());
                    var vXelements = vPregunta.Select(x =>
                                                    new XElement("RESPUESTA",
                                                    new XAttribute("ID_PREGUNTA", x.ID_PREGUNTA),
                                                    new XAttribute("ID_CUESTIONARIO_PREGUNTA", x.ID_CUESTIONARIO_PREGUNTA),
                                                    new XAttribute("NB_PREGUNTA", x.NB_PREGUNTA),
                                                    new XAttribute("NB_RESPUESTA", x.NB_RESPUESTA),
                                                    new XAttribute("NO_VALOR_RESPUESTA", x.NO_VALOR_RESPUESTA),
                                                    new XAttribute("CL_PREGUNTA", x.CL_PREGUNTA)
                                         ));
                    XElement RESPUESTAS =
                    new XElement("RESPUESTAS", vXelements
                    );

                    CuestionarioPreguntaNegocio nCustionarioPregunta = new CuestionarioPreguntaNegocio();
                    PruebasNegocio objPrueba = new PruebasNegocio();
                    var vPrueba = objPrueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();

                    if (vPrueba != null)
                    {

                        SPE_OBTIENE_K_PRUEBA_Result vPruebaTerminada = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();
                        E_RESULTADO vResultados = nKprueba.CorrigePrueba(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
                        string vMensaje = vResultados.MENSAJE.Where(w => w.CL_IDIOMA.Equals("ES")).FirstOrDefault().DS_MENSAJE;
                        if (vResultados.CL_TIPO_ERROR != E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                        {
                            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultados.CL_TIPO_ERROR, 400, 150, "");
                        }
                        else
                        {

                            E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: "REDACCION", usuario: vClUsuario, programa: vNbPrograma);
                            string vMensajes = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals("ES")).FirstOrDefault().DS_MENSAJE;
                            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensajes, vResultado.CL_TIPO_ERROR, 400, 150, "");
                        }

                    }
                }
                else
                {
                    string vMensaje = "Debes de contestar la prueba.";
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, E_TIPO_RESPUESTA_DB.WARNING, 400, 150, "");
                }
            }
        }

        protected void btnEliminarBateria_Click(object sender, EventArgs e)
        {
            PruebasNegocio nPruebas = new PruebasNegocio();
            var vResultado = nPruebas.EliminaRespuestasBaterias(vIdBateria, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals("ES")).FirstOrDefault().DS_MENSAJE;
            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "Close");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (vIdPrueba != null)
            {
                PruebasNegocio nPruebas = new PruebasNegocio();
                var vResultado = nPruebas.EliminaRespuestasPrueba(vIdPrueba, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals("ES")).FirstOrDefault().DS_MENSAJE;
                if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                {
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
                    Response.Redirect(Request.RawUrl); 
                }
                else
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
            }

        }

        protected void RadAjaxManager1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            PruebasNegocio nKprueba = new PruebasNegocio();
            E_RESULTADO vObjetoPrueba = nKprueba.INICIAR_K_PRUEBA(pIdPrueba: vIdPrueba, pFeInicio: DateTime.Now, pClTokenExterno: vClToken, usuario: vClUsuario, programa: vNbPrograma);
        }
    }
}