using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.IDP
{
    public partial class VentanaEstiloDePensamiento : System.Web.UI.Page
    {

        private static E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private List<E_PREGUNTA> vRespuestas
        {
            get { return (List<E_PREGUNTA>)ViewState["vsPreguntasCustionario"]; }
            set { ViewState["vsPreguntasCustionario"] = value; }
        }
        private string vClUsuario
        {
            get { return (string)ViewState["vsvClUsuario"]; }
            set { ViewState["vsvClUsuario"] = value; }
        }
        private string vNbPrograma
        {
            get { return (string)ViewState["vsvNbPrograma"]; }
            set { ViewState["vsvNbPrograma"] = value; }
        }

        public int vTiempoPrueba
        {
            get { return (int)ViewState["vsTiempoPrueba"]; }
            set { ViewState["vsTiempoPrueba"] = value; }
        }
        public string vEstatusPrueba
        {
            get { return (string)ViewState["vsvEstatusPrueba"]; }
            set { ViewState["vsvEstatusPrueba"] = value; }
        
        }

        public string vTipoRevision
        {
            get { return (string)ViewState["vsTipoRevision"]; }
            set { ViewState["vsTipoRevision"] = value; }
        }

        public bool MostrarCronometro
        {
            get { return (bool)ViewState["vsMostrarCronometroEP"]; }
            set { ViewState["vsMostrarCronometroEP"] = value; }
        }

        public Guid vClTokenExterno
        {
            get { return (Guid)ViewState["vsClTokenExterno"]; }
            set { ViewState["vsClTokenExterno"] = value; }
        }

        public int vIdPrueba
        {
            get { return (int)ViewState["vsIdEvaluado"]; }
            set { ViewState["vsIdEvaluado"] = value; }
        }

        private Guid vClToken
        {
            get { return (Guid)ViewState["vsClToken"]; }
            set { ViewState["vsClToken"] = value; }
        }

        public string vTipoPrueba
        {
            get { return (string)ViewState["vTipoPrueba"]; }
            set { ViewState["vTipoPrueba"] = value; }
        }

        public int vIdBateria
        {
            get { return (int)ViewState["vsIdBateria"]; }
            set { ViewState["vsIdBateria"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = (ContextoUsuario.oUsuario != null ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO");
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    MostrarCronometro = ContextoApp.IDP.ConfiguracionPsicometria.FgMostrarCronometro;

                    if (Request.QueryString["MOD"] != null)
                    {
                        vTipoRevision = Request.QueryString["MOD"];
                    }
                    PruebasNegocio nKprueba = new PruebasNegocio();
                    vIdPrueba = int.Parse(Request.QueryString["ID"]);
                    vClToken = new Guid(Request.QueryString["T"]);
                    vClTokenExterno = new Guid(Request.QueryString["T"]);
                    if (Request.QueryString["vIdBateria"] != null)
                    {
                        vIdBateria = int.Parse(Request.QueryString["vIdBateria"]);
                        btnEliminar.Visible = true;
                        btnEliminarBateria.Visible = true;
                    }
                    else
                    {
                        btnEliminar.Visible = false;
                        btnEliminarBateria.Visible = false;
                    }

                    if (vTipoRevision == "REV")
                    {
                        cronometro.Visible = false;
                        vTiempoPrueba = 0;
                        btnTerminar.Enabled = false;
                        //obtener respuestas
                        var respuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);
                        var vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken).FirstOrDefault();
                        vTipoPrueba = vPrueba.NB_TIPO_PRUEBA;
                        if (vPrueba.NB_TIPO_PRUEBA == "MANUAL")
                            asignarValoresManual(respuestas);
                        else
                            asignarValores(respuestas);
                        btnImpresionPrueba.Visible = true;
                    }
                    else if (vTipoRevision == "EDIT")
                    {
                        cronometro.Visible = false;
                        vTiempoPrueba = 0;
                       // btnEliminar.Visible = true;// Se agrega para la nueva forma de navegación 06/06/2018
                        btnImpresionPrueba.Visible = true; // Se agrega para imprimir en la nueva navegación IDP 06/06/2018
                        btnTerminar.Visible = false;
                        btnCorregir.Visible = true;
                        //obtener respuestas
                        var respuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);
                        var vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken).FirstOrDefault();
                        if (vPrueba.NB_TIPO_PRUEBA == "MANUAL")
                        {
                            asignarValoresManual(respuestas);
                            btnCorregir.Enabled = false;
                        }
                        else
                            asignarValores(respuestas);
                    }
                    else
                    {
                        var lstPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken);
                        if (lstPrueba.Count == 1)
                        {
                            var vPruebaObj = lstPrueba[0];
                            var tiempoTotal = vPruebaObj.NO_TIEMPO * 60;
                            if (vPruebaObj.FE_INICIO.HasValue)
                            {
                                var tiempoTranscurrido = DateTime.Now.Subtract(vPruebaObj.FE_INICIO.Value);
                                vTiempoPrueba = tiempoTotal - (int)tiempoTranscurrido.TotalSeconds;
                            }
                            else
                                vTiempoPrueba = tiempoTotal;
                        }
                        else
                            vTiempoPrueba = 0;
                        /*E_RESULTADO vObjetoPrueba = nKprueba.INICIAR_K_PRUEBA(pIdPrueba: vIdPrueba, pFeInicio: DateTime.Now, pClTokenExterno: vClToken, usuario: vClUsuario, programa: vNbPrograma);
                        if (vObjetoPrueba != null)
                        {
                                 //Si el modo de revision esta activado
                        //if (vTipoRevision == "REV")
                        //{
                        //    cronometro.Visible = false;
                        //    vTiempoPrueba = 0;
                        //    btnTerminar.Enabled = false;
                        //    //obtener respuestas
                        //    var respuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);
                        //    var vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken).FirstOrDefault();
                        //    vTipoPrueba = vPrueba.NB_TIPO_PRUEBA;
                        //    if (vPrueba.NB_TIPO_PRUEBA == "MANUAL")
                        //        asignarValoresManual(respuestas);
                        //    else
                        //        asignarValores(respuestas);
                        //    btnImpresionPrueba.Visible = true;
                        //}
                        //else if (vTipoRevision == "EDIT")
                        //{
                        //    cronometro.Visible = false;
                        //    vTiempoPrueba = 0;
                        //    btnTerminar.Visible = false;
                        //    btnCorregir.Visible = true;
                        //    //obtener respuestas
                        //    var respuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);
                        //    var vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken).FirstOrDefault();
                        //    if (vPrueba.NB_TIPO_PRUEBA == "MANUAL")
                        //        asignarValoresManual(respuestas);
                        //    else
                        //        asignarValores(respuestas);
                        //}
                        //else
                        //{
                            if (vObjetoPrueba.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.ERROR)
                            {
                                vTiempoPrueba = 0;
                            }
                            else if (vObjetoPrueba.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                            {
                                vTiempoPrueba = int.Parse(vObjetoPrueba.MENSAJE.Where(r => r.CL_IDIOMA.Equals("ES")).FirstOrDefault().DS_MENSAJE.ToString());
                            }
                        }*/
                    }
                }
                vRespuestas = new List<E_PREGUNTA>();
               
            }
        }

        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            PruebasNegocio nKprueba = new PruebasNegocio();
            SPE_OBTIENE_K_PRUEBA_Result vPruebaTerminada = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();
            vPruebaTerminada.FE_TERMINO = DateTime.Now;
            vPruebaTerminada.CL_ESTADO = E_ESTADO_PRUEBA.TERMINADA.ToString();
            vPruebaTerminada.NB_TIPO_PRUEBA = "APLICACION";
            E_RESULTADO vResultado = nKprueba.InsertaActualiza_K_PRUEBA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
           if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                SaveTest();
            else if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.WARNING)
            {
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                if (vMensaje == "NO")
                    UtilMensajes.MensajeResultadoDB(rnMensaje, "Usted ha tratado de capturar doble una prueba; los datos no fueron guardados.", E_TIPO_RESPUESTA_DB.SUCCESSFUL, 400, 150, "CloseTest");
            }
        }

        public void SaveTest()
        {
            CuestionariosNegocio nPreguntas = new CuestionariosNegocio();
            var preguntas = nPreguntas.Obtener_K_PREGUNTA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken);
            foreach (SPE_OBTIENE_K_PREGUNTA_Result pregunta in preguntas)
            {
                E_PREGUNTA vObjetoPregunta = new E_PREGUNTA
                {
                    ID_PRUEBA = pregunta.ID_PRUEBA,
                    ID_CUESTIONARIO_PREGUNTA = pregunta.ID_CUESTIONARIO_PREGUNTA,
                    ID_CUESTIONARIO = pregunta.ID_CUESTIONARIO,
                    ID_PREGUNTA = pregunta.ID_PREGUNTA,
                    CL_PREGUNTA = pregunta.CL_PREGUNTA,
                    NB_PREGUNTA = pregunta.NB_PREGUNTA,
                    DS_PREGUNTA = pregunta.DS_PREGUNTA,
                    CL_TIPO_PREGUNTA = pregunta.CL_TIPO_PREGUNTA,
                    NO_VALOR = pregunta.NO_VALOR,
                    NO_VALOR_RESPUESTA = pregunta.NO_VALOR_RESPUESTA,
                    NB_RESPUESTA = pregunta.NB_RESPUESTA,
                    FG_REQUERIDO = pregunta.FG_REQUERIDO,
                    FG_ACTIVO = pregunta.FG_ACTIVO,
                    ID_COMPETENCIA = pregunta.ID_COMPETENCIA,
                    ID_BITACORA = pregunta.ID_BITACORA
                };
                vRespuestas.Add(vObjetoPregunta);
            }

            String vPENSAMIENTO_A_0001 = radTxtPreg1Resp1.Text;
            BackQuestionObject("PENSAMIENTO-A-0001", vPENSAMIENTO_A_0001);

            String vPENSAMIENTO_A_0002 = radTxtPreg1Resp2.Text; ;
            BackQuestionObject("PENSAMIENTO-A-0002", vPENSAMIENTO_A_0002);

            String vPENSAMIENTO_A_0003 = radTxtPreg1Resp3.Text; ;
            BackQuestionObject("PENSAMIENTO-A-0003", vPENSAMIENTO_A_0003);

            String vPENSAMIENTO_A_0004 = radTxtPreg1Resp4.Text; ;
            BackQuestionObject("PENSAMIENTO-A-0004", vPENSAMIENTO_A_0004);

            String vPENSAMIENTO_A_0005 = radTxtPreg2Resp1.Text; ;
            BackQuestionObject("PENSAMIENTO-A-0005", vPENSAMIENTO_A_0005);

            String vPENSAMIENTO_A_0006 = radTxtPreg2Resp2.Text; ;
            BackQuestionObject("PENSAMIENTO-A-0006", vPENSAMIENTO_A_0006);

            String vPENSAMIENTO_A_0007 = radTxtPreg2Resp3.Text; ;
            BackQuestionObject("PENSAMIENTO-A-0007", vPENSAMIENTO_A_0007);

            String vPENSAMIENTO_A_0008 = radTxtPreg2Resp4.Text; 
            BackQuestionObject("PENSAMIENTO-A-0008", vPENSAMIENTO_A_0008);

            String vPENSAMIENTO_A_0009 = radTxtPreg3Resp1.Text;
            BackQuestionObject("PENSAMIENTO-A-0009", vPENSAMIENTO_A_0009);

            String vPENSAMIENTO_A_0010 = radTxtPreg3Resp2.Text;
            BackQuestionObject("PENSAMIENTO-A-0010", vPENSAMIENTO_A_0010);

            String vPENSAMIENTO_A_0011 = radTxtPreg3Resp3.Text;
            BackQuestionObject("PENSAMIENTO-A-0011", vPENSAMIENTO_A_0011);

            String vPENSAMIENTO_A_0012 = radTxtPreg3Resp4.Text;
            BackQuestionObject("PENSAMIENTO-A-0012", vPENSAMIENTO_A_0012);

            String vPENSAMIENTO_A_0013 = radTxtPreg4Resp1.Text;
            BackQuestionObject("PENSAMIENTO-A-0013", vPENSAMIENTO_A_0013);

            String vPENSAMIENTO_A_0014 = radTxtPreg4Resp2.Text;
            BackQuestionObject("PENSAMIENTO-A-0014", vPENSAMIENTO_A_0014);

            String vPENSAMIENTO_A_0015 = radTxtPreg4Resp3.Text;
            BackQuestionObject("PENSAMIENTO-A-0015", vPENSAMIENTO_A_0015);

            String vPENSAMIENTO_A_0016 = radTxtPreg4Resp4.Text;
            BackQuestionObject("PENSAMIENTO-A-0016", vPENSAMIENTO_A_0016);

            String vPENSAMIENTO_A_0017 = radTxtPreg5Resp1.Text;
            BackQuestionObject("PENSAMIENTO-A-0017", vPENSAMIENTO_A_0017);

            String vPENSAMIENTO_A_0018 = radTxtPreg5Resp2.Text;
            BackQuestionObject("PENSAMIENTO-A-0018", vPENSAMIENTO_A_0018);

            String vPENSAMIENTO_A_0019 = radTxtPreg5Resp3.Text;
            BackQuestionObject("PENSAMIENTO-A-0019", vPENSAMIENTO_A_0019);

            String vPENSAMIENTO_A_0020 = radTxtPreg5Resp4.Text;
            BackQuestionObject("PENSAMIENTO-A-0020", vPENSAMIENTO_A_0020);

            ///////////////////////////SEGMENTO 2//////////////////////////////////

            String vPENSAMIENTO_B_0001 = radTxtPreg6Resp1.Text;
            BackQuestionObject("PENSAMIENTO-B-0001", vPENSAMIENTO_B_0001);

            String vPENSAMIENTO_B_0002 = radTxtPreg6Resp2.Text;
            BackQuestionObject("PENSAMIENTO-B-0002", vPENSAMIENTO_B_0002);

            String vPENSAMIENTO_B_0003 = radTxtPreg6Resp3.Text;
            BackQuestionObject("PENSAMIENTO-B-0003", vPENSAMIENTO_B_0003);

            String vPENSAMIENTO_B_0004 = radTxtPreg6Resp4.Text;
            BackQuestionObject("PENSAMIENTO-B-0004", vPENSAMIENTO_B_0004);

            String vPENSAMIENTO_B_0005 = radTxtPreg7Resp1.Text;
            BackQuestionObject("PENSAMIENTO-B-0005", vPENSAMIENTO_B_0005);

            String vPENSAMIENTO_B_0006 = radTxtPreg7Resp2.Text;
            BackQuestionObject("PENSAMIENTO-B-0006", vPENSAMIENTO_B_0006);

            String vPENSAMIENTO_B_0007 = radTxtPreg7Resp3.Text;
            BackQuestionObject("PENSAMIENTO-B-0007", vPENSAMIENTO_B_0007);

            String vPENSAMIENTO_B_0008 = radTxtPreg7Resp4.Text;
            BackQuestionObject("PENSAMIENTO-B-0008", vPENSAMIENTO_B_0008);

            String vPENSAMIENTO_B_0009 = radTxtPreg8Resp1.Text;
            BackQuestionObject("PENSAMIENTO-B-0009", vPENSAMIENTO_B_0009);

            String vPENSAMIENTO_B_0010 = radTxtPreg8Resp2.Text;
            BackQuestionObject("PENSAMIENTO-B-0010", vPENSAMIENTO_B_0010);

            String vPENSAMIENTO_B_0011 = radTxtPreg8Resp3.Text;
            BackQuestionObject("PENSAMIENTO-B-0011", vPENSAMIENTO_B_0011);

            String vPENSAMIENTO_B_0012 = radTxtPreg8Resp4.Text;
            BackQuestionObject("PENSAMIENTO-B-0012", vPENSAMIENTO_B_0012);

            String vPENSAMIENTO_B_0013 = radTxtPreg9Resp1.Text;
            BackQuestionObject("PENSAMIENTO-B-0013", vPENSAMIENTO_B_0013);

            String vPENSAMIENTO_B_0014 = radTxtPreg9Resp2.Text;
            BackQuestionObject("PENSAMIENTO-B-0014", vPENSAMIENTO_B_0014);

            String vPENSAMIENTO_B_0015 = radTxtPreg9Resp3.Text;
            BackQuestionObject("PENSAMIENTO-B-0015", vPENSAMIENTO_B_0015);

            String vPENSAMIENTO_B_0016 = radTxtPreg9Resp4.Text;
            BackQuestionObject("PENSAMIENTO-B-0016", vPENSAMIENTO_B_0016);

            String vPENSAMIENTO_B_0017 = radTxtPreg10Resp1.Text;
            BackQuestionObject("PENSAMIENTO-B-0017", vPENSAMIENTO_B_0017);

            String vPENSAMIENTO_B_0018 = radTxtPreg10Resp2.Text;
            BackQuestionObject("PENSAMIENTO-B-0018", vPENSAMIENTO_B_0018);

            String vPENSAMIENTO_B_0019 = radTxtPreg10Resp3.Text;
            BackQuestionObject("PENSAMIENTO-B-0019", vPENSAMIENTO_B_0019);

            String vPENSAMIENTO_B_0020 = radTxtPreg10Resp4.Text;
            BackQuestionObject("PENSAMIENTO-B-0020", vPENSAMIENTO_B_0020);

            //////////////////////////////SEGMENTO 3////////////////////////////////

            String vPENSAMIENTO_C_0001 = radTxtPreg11Resp1.Text;
            BackQuestionObject("PENSAMIENTO-C-0001", vPENSAMIENTO_C_0001);

            String vPENSAMIENTO_C_0002 = radTxtPreg11Resp2.Text;
            BackQuestionObject("PENSAMIENTO-C-0002", vPENSAMIENTO_C_0002);

            String vPENSAMIENTO_C_0003 = radTxtPreg12Resp1.Text;
            BackQuestionObject("PENSAMIENTO-C-0003", vPENSAMIENTO_C_0003);

            String vPENSAMIENTO_C_0004 = radTxtPreg12Resp2.Text;
            BackQuestionObject("PENSAMIENTO-C-0004", vPENSAMIENTO_C_0004);

            String vPENSAMIENTO_C_0005 = radTxtPreg13Resp1.Text;
            BackQuestionObject("PENSAMIENTO-C-0005", vPENSAMIENTO_C_0005);

            String vPENSAMIENTO_C_0006 = radTxtPreg13Resp2.Text;
            BackQuestionObject("PENSAMIENTO-C-0006", vPENSAMIENTO_C_0006);

            String vPENSAMIENTO_C_0007 = radTxtPreg14Resp1.Text;
            BackQuestionObject("PENSAMIENTO-C-0007", vPENSAMIENTO_C_0007);

            String vPENSAMIENTO_C_0008 = radTxtPreg14Resp2.Text;
            BackQuestionObject("PENSAMIENTO-C-0008", vPENSAMIENTO_C_0008);

            String vPENSAMIENTO_C_0009 = radTxtPreg15Resp1.Text;
            BackQuestionObject("PENSAMIENTO-C-0009", vPENSAMIENTO_C_0009);

            String vPENSAMIENTO_C_0010 = radTxtPreg15Resp2.Text;
            BackQuestionObject("PENSAMIENTO-C-0010", vPENSAMIENTO_C_0010);

            String vPENSAMIENTO_C_0011 = radTxtPreg16Resp1.Text;
            BackQuestionObject("PENSAMIENTO-C-0011", vPENSAMIENTO_C_0011);

            String vPENSAMIENTO_C_0012 = radTxtPreg16Resp2.Text;
            BackQuestionObject("PENSAMIENTO-C-0012", vPENSAMIENTO_C_0012);

            String vPENSAMIENTO_C_0013 = radTxtPreg17Resp1.Text;
            BackQuestionObject("PENSAMIENTO-C-0013", vPENSAMIENTO_C_0013);

            String vPENSAMIENTO_C_0014 = radTxtPreg17Resp2.Text;
            BackQuestionObject("PENSAMIENTO-C-0014", vPENSAMIENTO_C_0014);

            String vPENSAMIENTO_C_0015 = radTxtPreg18Resp1.Text;
            BackQuestionObject("PENSAMIENTO-C-0015", vPENSAMIENTO_C_0015);

            String vPENSAMIENTO_C_0016 = radTxtPreg18Resp2.Text;
            BackQuestionObject("PENSAMIENTO-C-0016", vPENSAMIENTO_C_0016);

            String vPENSAMIENTO_C_0017 = radTxtPreg19Resp1.Text;
            BackQuestionObject("PENSAMIENTO-C-0017", vPENSAMIENTO_C_0017);

            String vPENSAMIENTO_C_0018 = radTxtPreg19Resp2.Text;
            BackQuestionObject("PENSAMIENTO-C-0018", vPENSAMIENTO_C_0018);

            String vPENSAMIENTO_C_0019 = radTxtPreg20Resp1.Text;
            BackQuestionObject("PENSAMIENTO-C-0019", vPENSAMIENTO_C_0019);

            String vPENSAMIENTO_C_0020 = radTxtPreg20Resp2.Text;
            BackQuestionObject("PENSAMIENTO-C-0020", vPENSAMIENTO_C_0020);

            var vXelements = vRespuestas.Select(x =>
                                                 new XElement("RESPUESTA",
                                                 new XAttribute("ID_CUESTIONARIO_PREGUNTA", x.ID_CUESTIONARIO_PREGUNTA),
                                                 new XAttribute("ID_PREGUNTA", x.ID_PREGUNTA),
                                                 new XAttribute("NB_PREGUNTA", x.NB_PREGUNTA),
                                                 new XAttribute("NB_RESPUESTA", x.NB_RESPUESTA),
                                                 new XAttribute("NO_VALOR_RESPUESTA", x.NO_VALOR_RESPUESTA)
                                      ));
            XElement RESPUESTAS =
            new XElement("RESPUESTAS", vXelements
            );

            CuestionarioPreguntaNegocio nCustionarioPregunta = new CuestionarioPreguntaNegocio();
            PruebasNegocio nKprueba = new PruebasNegocio();
            var vObjetoPrueba = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();

            E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vObjetoPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(),pNbPrueba: "PENSAMIENTO", usuario: vClUsuario, programa: vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "CloseTest");

        }

        public void EditTest()
        {
            CuestionariosNegocio nPreguntas = new CuestionariosNegocio();
            var preguntas = nPreguntas.Obtener_K_PREGUNTA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken);
            foreach (SPE_OBTIENE_K_PREGUNTA_Result pregunta in preguntas)
            {
                E_PREGUNTA vObjetoPregunta = new E_PREGUNTA
                {
                    ID_PRUEBA = pregunta.ID_PRUEBA,
                    ID_CUESTIONARIO_PREGUNTA = pregunta.ID_CUESTIONARIO_PREGUNTA,
                    ID_CUESTIONARIO = pregunta.ID_CUESTIONARIO,
                    ID_PREGUNTA = pregunta.ID_PREGUNTA,
                    CL_PREGUNTA = pregunta.CL_PREGUNTA,
                    NB_PREGUNTA = pregunta.NB_PREGUNTA,
                    DS_PREGUNTA = pregunta.DS_PREGUNTA,
                    CL_TIPO_PREGUNTA = pregunta.CL_TIPO_PREGUNTA,
                    NO_VALOR = pregunta.NO_VALOR,
                    NO_VALOR_RESPUESTA = pregunta.NO_VALOR_RESPUESTA,
                    NB_RESPUESTA = pregunta.NB_RESPUESTA,
                    FG_REQUERIDO = pregunta.FG_REQUERIDO,
                    FG_ACTIVO = pregunta.FG_ACTIVO,
                    ID_COMPETENCIA = pregunta.ID_COMPETENCIA,
                    ID_BITACORA = pregunta.ID_BITACORA
                };
                vRespuestas.Add(vObjetoPregunta);
            }

            String vPENSAMIENTO_A_0001 = radTxtPreg1Resp1.Text;
            BackQuestionObject("PENSAMIENTO-A-0001", vPENSAMIENTO_A_0001);

            String vPENSAMIENTO_A_0002 = radTxtPreg1Resp2.Text; ;
            BackQuestionObject("PENSAMIENTO-A-0002", vPENSAMIENTO_A_0002);

            String vPENSAMIENTO_A_0003 = radTxtPreg1Resp3.Text; ;
            BackQuestionObject("PENSAMIENTO-A-0003", vPENSAMIENTO_A_0003);

            String vPENSAMIENTO_A_0004 = radTxtPreg1Resp4.Text; ;
            BackQuestionObject("PENSAMIENTO-A-0004", vPENSAMIENTO_A_0004);

            String vPENSAMIENTO_A_0005 = radTxtPreg2Resp1.Text; ;
            BackQuestionObject("PENSAMIENTO-A-0005", vPENSAMIENTO_A_0005);

            String vPENSAMIENTO_A_0006 = radTxtPreg2Resp2.Text; ;
            BackQuestionObject("PENSAMIENTO-A-0006", vPENSAMIENTO_A_0006);

            String vPENSAMIENTO_A_0007 = radTxtPreg2Resp3.Text; ;
            BackQuestionObject("PENSAMIENTO-A-0007", vPENSAMIENTO_A_0007);

            String vPENSAMIENTO_A_0008 = radTxtPreg2Resp4.Text;
            BackQuestionObject("PENSAMIENTO-A-0008", vPENSAMIENTO_A_0008);

            String vPENSAMIENTO_A_0009 = radTxtPreg3Resp1.Text;
            BackQuestionObject("PENSAMIENTO-A-0009", vPENSAMIENTO_A_0009);

            String vPENSAMIENTO_A_0010 = radTxtPreg3Resp2.Text;
            BackQuestionObject("PENSAMIENTO-A-0010", vPENSAMIENTO_A_0010);

            String vPENSAMIENTO_A_0011 = radTxtPreg3Resp3.Text;
            BackQuestionObject("PENSAMIENTO-A-0011", vPENSAMIENTO_A_0011);

            String vPENSAMIENTO_A_0012 = radTxtPreg3Resp4.Text;
            BackQuestionObject("PENSAMIENTO-A-0012", vPENSAMIENTO_A_0012);

            String vPENSAMIENTO_A_0013 = radTxtPreg4Resp1.Text;
            BackQuestionObject("PENSAMIENTO-A-0013", vPENSAMIENTO_A_0013);

            String vPENSAMIENTO_A_0014 = radTxtPreg4Resp2.Text;
            BackQuestionObject("PENSAMIENTO-A-0014", vPENSAMIENTO_A_0014);

            String vPENSAMIENTO_A_0015 = radTxtPreg4Resp3.Text;
            BackQuestionObject("PENSAMIENTO-A-0015", vPENSAMIENTO_A_0015);

            String vPENSAMIENTO_A_0016 = radTxtPreg4Resp4.Text;
            BackQuestionObject("PENSAMIENTO-A-0016", vPENSAMIENTO_A_0016);

            String vPENSAMIENTO_A_0017 = radTxtPreg5Resp1.Text;
            BackQuestionObject("PENSAMIENTO-A-0017", vPENSAMIENTO_A_0017);

            String vPENSAMIENTO_A_0018 = radTxtPreg5Resp2.Text;
            BackQuestionObject("PENSAMIENTO-A-0018", vPENSAMIENTO_A_0018);

            String vPENSAMIENTO_A_0019 = radTxtPreg5Resp3.Text;
            BackQuestionObject("PENSAMIENTO-A-0019", vPENSAMIENTO_A_0019);

            String vPENSAMIENTO_A_0020 = radTxtPreg5Resp4.Text;
            BackQuestionObject("PENSAMIENTO-A-0020", vPENSAMIENTO_A_0020);

            ///////////////////////////SEGMENTO 2//////////////////////////////////

            String vPENSAMIENTO_B_0001 = radTxtPreg6Resp1.Text;
            BackQuestionObject("PENSAMIENTO-B-0001", vPENSAMIENTO_B_0001);

            String vPENSAMIENTO_B_0002 = radTxtPreg6Resp2.Text;
            BackQuestionObject("PENSAMIENTO-B-0002", vPENSAMIENTO_B_0002);

            String vPENSAMIENTO_B_0003 = radTxtPreg6Resp3.Text;
            BackQuestionObject("PENSAMIENTO-B-0003", vPENSAMIENTO_B_0003);

            String vPENSAMIENTO_B_0004 = radTxtPreg6Resp4.Text;
            BackQuestionObject("PENSAMIENTO-B-0004", vPENSAMIENTO_B_0004);

            String vPENSAMIENTO_B_0005 = radTxtPreg7Resp1.Text;
            BackQuestionObject("PENSAMIENTO-B-0005", vPENSAMIENTO_B_0005);

            String vPENSAMIENTO_B_0006 = radTxtPreg7Resp2.Text;
            BackQuestionObject("PENSAMIENTO-B-0006", vPENSAMIENTO_B_0006);

            String vPENSAMIENTO_B_0007 = radTxtPreg7Resp3.Text;
            BackQuestionObject("PENSAMIENTO-B-0007", vPENSAMIENTO_B_0007);

            String vPENSAMIENTO_B_0008 = radTxtPreg7Resp4.Text;
            BackQuestionObject("PENSAMIENTO-B-0008", vPENSAMIENTO_B_0008);

            String vPENSAMIENTO_B_0009 = radTxtPreg8Resp1.Text;
            BackQuestionObject("PENSAMIENTO-B-0009", vPENSAMIENTO_B_0009);

            String vPENSAMIENTO_B_0010 = radTxtPreg8Resp2.Text;
            BackQuestionObject("PENSAMIENTO-B-0010", vPENSAMIENTO_B_0010);

            String vPENSAMIENTO_B_0011 = radTxtPreg8Resp3.Text;
            BackQuestionObject("PENSAMIENTO-B-0011", vPENSAMIENTO_B_0011);

            String vPENSAMIENTO_B_0012 = radTxtPreg8Resp4.Text;
            BackQuestionObject("PENSAMIENTO-B-0012", vPENSAMIENTO_B_0012);

            String vPENSAMIENTO_B_0013 = radTxtPreg9Resp1.Text;
            BackQuestionObject("PENSAMIENTO-B-0013", vPENSAMIENTO_B_0013);

            String vPENSAMIENTO_B_0014 = radTxtPreg9Resp2.Text;
            BackQuestionObject("PENSAMIENTO-B-0014", vPENSAMIENTO_B_0014);

            String vPENSAMIENTO_B_0015 = radTxtPreg9Resp3.Text;
            BackQuestionObject("PENSAMIENTO-B-0015", vPENSAMIENTO_B_0015);

            String vPENSAMIENTO_B_0016 = radTxtPreg9Resp4.Text;
            BackQuestionObject("PENSAMIENTO-B-0016", vPENSAMIENTO_B_0016);

            String vPENSAMIENTO_B_0017 = radTxtPreg10Resp1.Text;
            BackQuestionObject("PENSAMIENTO-B-0017", vPENSAMIENTO_B_0017);

            String vPENSAMIENTO_B_0018 = radTxtPreg10Resp2.Text;
            BackQuestionObject("PENSAMIENTO-B-0018", vPENSAMIENTO_B_0018);

            String vPENSAMIENTO_B_0019 = radTxtPreg10Resp3.Text;
            BackQuestionObject("PENSAMIENTO-B-0019", vPENSAMIENTO_B_0019);

            String vPENSAMIENTO_B_0020 = radTxtPreg10Resp4.Text;
            BackQuestionObject("PENSAMIENTO-B-0020", vPENSAMIENTO_B_0020);

            //////////////////////////////SEGMENTO 3////////////////////////////////

            String vPENSAMIENTO_C_0001 = radTxtPreg11Resp1.Text;
            BackQuestionObject("PENSAMIENTO-C-0001", vPENSAMIENTO_C_0001);

            String vPENSAMIENTO_C_0002 = radTxtPreg11Resp2.Text;
            BackQuestionObject("PENSAMIENTO-C-0002", vPENSAMIENTO_C_0002);

            String vPENSAMIENTO_C_0003 = radTxtPreg12Resp1.Text;
            BackQuestionObject("PENSAMIENTO-C-0003", vPENSAMIENTO_C_0003);

            String vPENSAMIENTO_C_0004 = radTxtPreg12Resp2.Text;
            BackQuestionObject("PENSAMIENTO-C-0004", vPENSAMIENTO_C_0004);

            String vPENSAMIENTO_C_0005 = radTxtPreg13Resp1.Text;
            BackQuestionObject("PENSAMIENTO-C-0005", vPENSAMIENTO_C_0005);

            String vPENSAMIENTO_C_0006 = radTxtPreg13Resp2.Text;
            BackQuestionObject("PENSAMIENTO-C-0006", vPENSAMIENTO_C_0006);

            String vPENSAMIENTO_C_0007 = radTxtPreg14Resp1.Text;
            BackQuestionObject("PENSAMIENTO-C-0007", vPENSAMIENTO_C_0007);

            String vPENSAMIENTO_C_0008 = radTxtPreg14Resp2.Text;
            BackQuestionObject("PENSAMIENTO-C-0008", vPENSAMIENTO_C_0008);

            String vPENSAMIENTO_C_0009 = radTxtPreg15Resp1.Text;
            BackQuestionObject("PENSAMIENTO-C-0009", vPENSAMIENTO_C_0009);

            String vPENSAMIENTO_C_0010 = radTxtPreg15Resp2.Text;
            BackQuestionObject("PENSAMIENTO-C-0010", vPENSAMIENTO_C_0010);

            String vPENSAMIENTO_C_0011 = radTxtPreg16Resp1.Text;
            BackQuestionObject("PENSAMIENTO-C-0011", vPENSAMIENTO_C_0011);

            String vPENSAMIENTO_C_0012 = radTxtPreg16Resp2.Text;
            BackQuestionObject("PENSAMIENTO-C-0012", vPENSAMIENTO_C_0012);

            String vPENSAMIENTO_C_0013 = radTxtPreg17Resp1.Text;
            BackQuestionObject("PENSAMIENTO-C-0013", vPENSAMIENTO_C_0013);

            String vPENSAMIENTO_C_0014 = radTxtPreg17Resp2.Text;
            BackQuestionObject("PENSAMIENTO-C-0014", vPENSAMIENTO_C_0014);

            String vPENSAMIENTO_C_0015 = radTxtPreg18Resp1.Text;
            BackQuestionObject("PENSAMIENTO-C-0015", vPENSAMIENTO_C_0015);

            String vPENSAMIENTO_C_0016 = radTxtPreg18Resp2.Text;
            BackQuestionObject("PENSAMIENTO-C-0016", vPENSAMIENTO_C_0016);

            String vPENSAMIENTO_C_0017 = radTxtPreg19Resp1.Text;
            BackQuestionObject("PENSAMIENTO-C-0017", vPENSAMIENTO_C_0017);

            String vPENSAMIENTO_C_0018 = radTxtPreg19Resp2.Text;
            BackQuestionObject("PENSAMIENTO-C-0018", vPENSAMIENTO_C_0018);

            String vPENSAMIENTO_C_0019 = radTxtPreg20Resp1.Text;
            BackQuestionObject("PENSAMIENTO-C-0019", vPENSAMIENTO_C_0019);

            String vPENSAMIENTO_C_0020 = radTxtPreg20Resp2.Text;
            BackQuestionObject("PENSAMIENTO-C-0020", vPENSAMIENTO_C_0020);

            var vXelements = vRespuestas.Select(x =>
                                                 new XElement("RESPUESTA",
                                                 new XAttribute("ID_CUESTIONARIO_PREGUNTA", x.ID_CUESTIONARIO_PREGUNTA),
                                                 new XAttribute("ID_PREGUNTA", x.ID_PREGUNTA),
                                                 new XAttribute("NB_PREGUNTA", x.NB_PREGUNTA),
                                                 new XAttribute("NB_RESPUESTA", x.NB_RESPUESTA),
                                                 new XAttribute("NO_VALOR_RESPUESTA", x.NO_VALOR_RESPUESTA)
                                      ));
            XElement RESPUESTAS =
            new XElement("RESPUESTAS", vXelements
            );

            CuestionarioPreguntaNegocio nCustionarioPregunta = new CuestionarioPreguntaNegocio();
            PruebasNegocio nKprueba = new PruebasNegocio();
            var vObjetoPrueba = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();
            E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vObjetoPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: "PENSAMIENTO", usuario: vClUsuario, programa: vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");

        }

        public void BackQuestionObject(string pclPregunta, string pnbRespuesta)
        {
            var vPregunta = vRespuestas.Where(x => x.CL_PREGUNTA.Equals(pclPregunta)).FirstOrDefault();
            if (vPregunta != null)
            {
                decimal vNoValor;
                vPregunta.NB_RESPUESTA = pnbRespuesta;
                vPregunta.NO_VALOR_RESPUESTA = (vNoValor = (pnbRespuesta != "") ? decimal.Parse(pnbRespuesta) : 0);
            }
        }

        public void asignarValores(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> respuestas)
        {
            foreach (SPE_OBTIENE_RESULTADO_PRUEBA_Result resp in respuestas)
            {
                switch (resp.CL_PREGUNTA)
                {
                    ////////
                    case "PENSAMIENTO-A-0001": radTxtPreg1Resp1.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-A-0002": radTxtPreg1Resp2.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-A-0003": radTxtPreg1Resp3.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-A-0004": radTxtPreg1Resp4.Text = resp.NB_RESPUESTA; break;

                    case "PENSAMIENTO-A-0005": radTxtPreg2Resp1.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-A-0006": radTxtPreg2Resp2.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-A-0007": radTxtPreg2Resp3.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-A-0008": radTxtPreg2Resp4.Text = resp.NB_RESPUESTA; break;

                    case "PENSAMIENTO-A-0009": radTxtPreg3Resp1.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-A-0010": radTxtPreg3Resp2.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-A-0011": radTxtPreg3Resp3.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-A-0012": radTxtPreg3Resp4.Text = resp.NB_RESPUESTA; break;

                    case "PENSAMIENTO-A-0013": radTxtPreg4Resp1.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-A-0014": radTxtPreg4Resp2.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-A-0015": radTxtPreg4Resp3.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-A-0016": radTxtPreg4Resp4.Text = resp.NB_RESPUESTA; break;

                    case "PENSAMIENTO-A-0017": radTxtPreg5Resp1.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-A-0018": radTxtPreg5Resp2.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-A-0019": radTxtPreg5Resp3.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-A-0020": radTxtPreg5Resp4.Text = resp.NB_RESPUESTA; break;

                    ////////
                    case "PENSAMIENTO-B-0001": radTxtPreg6Resp1.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-B-0002": radTxtPreg6Resp2.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-B-0003": radTxtPreg6Resp3.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-B-0004": radTxtPreg6Resp4.Text = resp.NB_RESPUESTA; break;

                    case "PENSAMIENTO-B-0005": radTxtPreg7Resp1.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-B-0006": radTxtPreg7Resp2.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-B-0007": radTxtPreg7Resp3.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-B-0008": radTxtPreg7Resp4.Text = resp.NB_RESPUESTA; break;

                    case "PENSAMIENTO-B-0009": radTxtPreg8Resp1.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-B-0010": radTxtPreg8Resp2.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-B-0011": radTxtPreg8Resp3.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-B-0012": radTxtPreg8Resp4.Text = resp.NB_RESPUESTA; break;

                    case "PENSAMIENTO-B-0013": radTxtPreg9Resp1.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-B-0014": radTxtPreg9Resp2.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-B-0015": radTxtPreg9Resp3.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-B-0016": radTxtPreg9Resp4.Text = resp.NB_RESPUESTA; break;

                    case "PENSAMIENTO-B-0017": radTxtPreg10Resp1.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-B-0018": radTxtPreg10Resp2.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-B-0019": radTxtPreg10Resp3.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-B-0020": radTxtPreg10Resp4.Text = resp.NB_RESPUESTA; break;

                    ////////
                    case "PENSAMIENTO-C-0001": radTxtPreg11Resp1.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-C-0002": radTxtPreg11Resp2.Text = resp.NB_RESPUESTA; break;

                    case "PENSAMIENTO-C-0003": radTxtPreg12Resp1.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-C-0004": radTxtPreg12Resp2.Text = resp.NB_RESPUESTA; break;

                    case "PENSAMIENTO-C-0005": radTxtPreg13Resp1.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-C-0006": radTxtPreg13Resp2.Text = resp.NB_RESPUESTA; break;

                    case "PENSAMIENTO-C-0007": radTxtPreg14Resp1.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-C-0008": radTxtPreg14Resp2.Text = resp.NB_RESPUESTA; break;

                    case "PENSAMIENTO-C-0009": radTxtPreg15Resp1.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-C-0010": radTxtPreg15Resp2.Text = resp.NB_RESPUESTA; break;

                    case "PENSAMIENTO-C-0011": radTxtPreg16Resp1.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-C-0012": radTxtPreg16Resp2.Text = resp.NB_RESPUESTA; break;

                    case "PENSAMIENTO-C-0013": radTxtPreg17Resp1.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-C-0014": radTxtPreg17Resp2.Text = resp.NB_RESPUESTA; break;

                    case "PENSAMIENTO-C-0015": radTxtPreg18Resp1.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-C-0016": radTxtPreg18Resp2.Text = resp.NB_RESPUESTA; break;

                    case "PENSAMIENTO-C-0017": radTxtPreg19Resp1.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-C-0018": radTxtPreg19Resp2.Text = resp.NB_RESPUESTA; break;

                    case "PENSAMIENTO-C-0019": radTxtPreg20Resp1.Text = resp.NB_RESPUESTA; break;
                    case "PENSAMIENTO-C-0020": radTxtPreg20Resp2.Text = resp.NB_RESPUESTA; break;
                    ///////

                  
                }
            }
        }

        public void asignarValoresManual(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> respuestas)
        {
            foreach (SPE_OBTIENE_RESULTADO_PRUEBA_Result resp in respuestas)
            {
                switch (resp.CL_PREGUNTA)
                {
                    case "PENSAMIENTO_RES_A1": radTxtPreg1Resp1.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_A2": radTxtPreg1Resp2.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_A3": radTxtPreg1Resp3.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_A4": radTxtPreg1Resp4.Text = resp.NB_RESULTADO; break;

                    case "PENSAMIENTO_RES_A5": radTxtPreg2Resp1.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_A6": radTxtPreg2Resp2.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_A7": radTxtPreg2Resp3.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_A8": radTxtPreg2Resp4.Text = resp.NB_RESULTADO; break;

                    case "PENSAMIENTO_RES_A9": radTxtPreg3Resp1.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_A10": radTxtPreg3Resp2.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_A11": radTxtPreg3Resp3.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_A12": radTxtPreg3Resp4.Text = resp.NB_RESULTADO; break;

                    case "PENSAMIENTO_RES_A13": radTxtPreg4Resp1.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_A14": radTxtPreg4Resp2.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_A15": radTxtPreg4Resp3.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_A16": radTxtPreg4Resp4.Text = resp.NB_RESULTADO; break;

                    case "PENSAMIENTO_RES_A17": radTxtPreg5Resp1.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_A18": radTxtPreg5Resp2.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_A19": radTxtPreg5Resp3.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_A20": radTxtPreg5Resp4.Text = resp.NB_RESULTADO; break;

                    ////////
                    case "PENSAMIENTO_RES_B1": radTxtPreg6Resp1.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_B2": radTxtPreg6Resp2.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_B3": radTxtPreg6Resp3.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_B4": radTxtPreg6Resp4.Text = resp.NB_RESULTADO; break;

                    case "PENSAMIENTO_RES_B5": radTxtPreg7Resp1.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_B6": radTxtPreg7Resp2.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_B7": radTxtPreg7Resp3.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_B8": radTxtPreg7Resp4.Text = resp.NB_RESULTADO; break;

                    case "PENSAMIENTO_RES_B9": radTxtPreg8Resp1.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_B10": radTxtPreg8Resp2.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_B11": radTxtPreg8Resp3.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_B12": radTxtPreg8Resp4.Text = resp.NB_RESULTADO; break;

                    case "PENSAMIENTO_RES_B13": radTxtPreg9Resp1.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_B14": radTxtPreg9Resp2.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_B15": radTxtPreg9Resp3.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_B16": radTxtPreg9Resp4.Text = resp.NB_RESULTADO; break;

                    case "PENSAMIENTO_RES_B17": radTxtPreg10Resp1.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_B18": radTxtPreg10Resp2.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_B19": radTxtPreg10Resp3.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_B20": radTxtPreg10Resp4.Text = resp.NB_RESULTADO; break;

                    ////////
                    case "PENSAMIENTO_RES_C1": radTxtPreg11Resp1.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_C2": radTxtPreg11Resp2.Text = resp.NB_RESULTADO; break;

                    case "PENSAMIENTO_RES_C3": radTxtPreg12Resp1.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_C4": radTxtPreg12Resp2.Text = resp.NB_RESULTADO; break;

                    case "PENSAMIENTO_RES_C5": radTxtPreg13Resp1.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_C6": radTxtPreg13Resp2.Text = resp.NB_RESULTADO; break;

                    case "PENSAMIENTO_RES_C7": radTxtPreg14Resp1.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_C8": radTxtPreg14Resp2.Text = resp.NB_RESULTADO; break;

                    case "PENSAMIENTO_RES_C9": radTxtPreg15Resp1.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_C10": radTxtPreg15Resp2.Text = resp.NB_RESULTADO; break;

                    case "PENSAMIENTO_RES_C11": radTxtPreg16Resp1.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_C12": radTxtPreg16Resp2.Text = resp.NB_RESULTADO; break;

                    case "PENSAMIENTO_RES_C13": radTxtPreg17Resp1.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_C14": radTxtPreg17Resp2.Text = resp.NB_RESULTADO; break;

                    case "PENSAMIENTO_RES_C15": radTxtPreg18Resp1.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_C16": radTxtPreg18Resp2.Text = resp.NB_RESULTADO; break;

                    case "PENSAMIENTO_RES_C17": radTxtPreg19Resp1.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_C18": radTxtPreg19Resp2.Text = resp.NB_RESULTADO; break;

                    case "PENSAMIENTO_RES_C19": radTxtPreg20Resp1.Text = resp.NB_RESULTADO; break;
                    case "PENSAMIENTO_RES_C20": radTxtPreg20Resp2.Text = resp.NB_RESULTADO; break;
                    ///////
                }
            }
        }

        protected void btnCorregir_Click(object sender, EventArgs e)
        {
            PruebasNegocio nKprueba = new PruebasNegocio();
            SPE_OBTIENE_K_PRUEBA_Result vPruebaTerminada = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();
            E_RESULTADO vResultado = nKprueba.CorrigePrueba(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                EditTest();
            else 
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
            }
        }

        protected void btnEliminarBateria_Click(object sender, EventArgs e)
        {
            PruebasNegocio nPruebas = new PruebasNegocio();
            var vResultado = nPruebas.EliminaRespuestasBaterias(vIdBateria, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
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
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                {
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
                    Response.Redirect(Request.RawUrl); 
                }
                else
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
            }

        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            PruebasNegocio nKprueba = new PruebasNegocio();
            E_RESULTADO vObjetoPrueba = nKprueba.INICIAR_K_PRUEBA(pIdPrueba: vIdPrueba, pFeInicio: DateTime.Now, pClTokenExterno: vClToken, usuario: vClUsuario, programa: vNbPrograma);
        }
    }
}