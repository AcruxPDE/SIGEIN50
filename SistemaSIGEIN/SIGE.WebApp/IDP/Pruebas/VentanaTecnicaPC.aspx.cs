using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Negocio.Administracion;
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

namespace SIGE.WebApp.IDP
{
    public partial class VentanaTecnicaPC : System.Web.UI.Page
    {

        #region Variables
        
        
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

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

        public int vIdPrueba
        {
            get { return (int)ViewState["vsIdEvaluado"]; }
            set { ViewState["vsIdEvaluado"] = value; }
        }

        public int vTiempoPrueba
        {
            get { return (int)ViewState["vsPLaboral2seconds"]; }
            set { ViewState["vsPLaboral2seconds"] = value; }
        }

        public string vEstatusPrueba;

        private List<E_PREGUNTA> vRespuestas
        {
            get { return (List<E_PREGUNTA>)ViewState["vsRespuestas"]; }
            set { ViewState["vsRespuestas"] = value; }
        }

        public Guid vClToken
        {
            get { return (Guid)ViewState["vsClToken"]; }
            set { ViewState["vsClToken"] = value; }
        }
        public string vTipoRevision
        {
            get { return (string)ViewState["vsTipoRevision"]; }
            set { ViewState["vsTipoRevision"] = value; }
        }

        public bool MostrarCronometro
        {
            get { return (bool)ViewState["vsMostrarCronometroTPC"]; }
            set { ViewState["vsMostrarCronometroTPC"] = value; }
        }

        public int vIdBateria
        {
            get { return (int)ViewState["vsIdBateria"]; }
            set { ViewState["vsIdBateria"] = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = (ContextoUsuario.oUsuario != null ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO");

            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    if (Request.QueryString["MOD"] != null)
                    {
                        vTipoRevision = Request.QueryString["MOD"];
                    }
                    PruebasNegocio nKprueba = new PruebasNegocio();
                    vIdPrueba = int.Parse(Request.QueryString["ID"]);
                    vClToken = new Guid(Request.QueryString["T"]);
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

                    MostrarCronometro = ContextoApp.IDP.ConfiguracionPsicometria.FgMostrarCronometro;

                    //Si el modo de revision esta activado
                    if (vTipoRevision == "REV")
                    {
                        cronometro.Visible = false;
                        vTiempoPrueba = 0;
                        btnTerminar.Enabled = false;
                        btnImpresionPrueba.Visible = true;
                        //obtener respuestas
                        var respuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);
                        asignarValores(respuestas);
                    }
                    else if (vTipoRevision == "EDIT")
                    {
                        //btnEliminar.Visible = true;// Se agrega para la nueva forma de navegación 06/06/2018
                        btnImpresionPrueba.Visible = true; // Se agrega para imprimir en la nueva navegación IDP 06/06/2018
                        cronometro.Visible = false;
                        vTiempoPrueba = 0;
                        btnTerminar.Visible = false;
                        btnCorregir.Visible = true;
                        //obtener respuestas
                        var respuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);
                        asignarValores(respuestas);
                         var vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken).FirstOrDefault();
                         if (vPrueba.NB_TIPO_PRUEBA == "MANUAL")
                             btnCorregir.Enabled = false;
                    }
                    else
                    {

                    E_RESULTADO vObjetoPrueba = nKprueba.INICIAR_K_PRUEBA(pIdPrueba: vIdPrueba, pFeInicio: DateTime.Now, pClTokenExterno: vClToken, usuario: vClUsuario, programa: vNbPrograma);
                    if (vObjetoPrueba != null)
                    {
                        //     //Si el modo de revision esta activado
                        //if (vTipoRevision == "REV")
                        //{
                        //    cronometro.Visible = false; 
                        //    vTiempoPrueba = 0;
                        //    btnTerminar.Enabled = false;
                        //    btnImpresionPrueba.Visible = true;
                        //    //obtener respuestas
                        //    var respuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);
                        //    asignarValores(respuestas);
                        //}
                        //else if (vTipoRevision == "EDIT")
                        //{
                        //    cronometro.Visible = false;
                        //    vTiempoPrueba = 0;
                        //    btnTerminar.Visible = false;
                        //    btnCorregir.Visible = true;
                        //    //obtener respuestas
                        //    var respuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);
                        //    asignarValores(respuestas);
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
                        }
                    }
                }
                vRespuestas = new List<E_PREGUNTA>();
            }           
        }

        public void SaveTest()
        {
            CuestionariosNegocio nPreguntas = new CuestionariosNegocio();
            var preguntas = nPreguntas.Obtener_K_PREGUNTA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken);
            if (preguntas.Count > 0)
            {
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

                
                BackQuestionObject("TECNICAPC-A-0001", btnPregunta1Resp1SI, btnPregunta1Resp1NO);
                BackQuestionObject("TECNICAPC-A-0002", btnPregunta1Resp2SI, btnPregunta1Resp2NO);
                BackQuestionObject("TECNICAPC-A-0003", btnPregunta1Resp3SI, btnPregunta1Resp3NO);
                BackQuestionObject("TECNICAPC-A-0004", btnPregunta1Resp4SI, btnPregunta1Resp4NO);
                
                BackQuestionObject("TECNICAPC-A-0005", btnPregunta2Resp1SI, btnPregunta2Resp1NO);
                BackQuestionObject("TECNICAPC-A-0006", btnPregunta2Resp2SI, btnPregunta2Resp2NO);
                BackQuestionObject("TECNICAPC-A-0007", btnPregunta2Resp3SI, btnPregunta2Resp3NO);
                BackQuestionObject("TECNICAPC-A-0008", btnPregunta2Resp4SI, btnPregunta2Resp4NO);
                
                BackQuestionObject("TECNICAPC-A-0009", btnPregunta3Resp1SI, btnPregunta3Resp1NO);
                BackQuestionObject("TECNICAPC-A-0010", btnPregunta3Resp2SI, btnPregunta3Resp2NO);
                BackQuestionObject("TECNICAPC-A-0011", btnPregunta3Resp3SI, btnPregunta3Resp3NO);                
                BackQuestionObject("TECNICAPC-A-0012", btnPregunta3Resp4SI, btnPregunta3Resp4NO);

                BackQuestionObject("TECNICAPC-A-0013", btnPregunta4Resp1SI, btnPregunta4Resp1NO);
                BackQuestionObject("TECNICAPC-A-0014", btnPregunta4Resp2SI, btnPregunta4Resp1NO);
                BackQuestionObject("TECNICAPC-A-0015", btnPregunta4Resp3SI, btnPregunta4Resp3NO);
                BackQuestionObject("TECNICAPC-A-0016", btnPregunta4Resp4SI, btnPregunta4Resp4NO);
                
                BackQuestionObject("TECNICAPC-A-0017", btnPregunta5Resp1SI, btnPregunta5Resp1NO);
                BackQuestionObject("TECNICAPC-A-0018", btnPregunta5Resp2SI, btnPregunta5Resp2NO);
                BackQuestionObject("TECNICAPC-A-0019", btnPregunta5Resp3SI, btnPregunta5Resp3NO);
                BackQuestionObject("TECNICAPC-A-0020", btnPregunta5Resp4SI, btnPregunta5Resp4NO);

                //Categoria 1
                BackQuestionObject("TECNICAPC-A-0021", btnPregunta6Resp1SI, btnPregunta6Resp1NO);
                BackQuestionObject("TECNICAPC-A-0022", btnPregunta6Resp2SI, btnPregunta6Resp2NO);
                BackQuestionObject("TECNICAPC-A-0023", btnPregunta6Resp3SI, btnPregunta6Resp3NO);
                BackQuestionObject("TECNICAPC-A-0024", btnPregunta6Resp4SI, btnPregunta6Resp4NO);
                
                BackQuestionObject("TECNICAPC-A-0025", btnPregunta7Resp1SI, btnPregunta7Resp1NO);
                BackQuestionObject("TECNICAPC-A-0026", btnPregunta7Resp2SI, btnPregunta7Resp2NO);
                BackQuestionObject("TECNICAPC-A-0027", btnPregunta7Resp3SI, btnPregunta7Resp3NO);
                BackQuestionObject("TECNICAPC-A-0028", btnPregunta7Resp4SI, btnPregunta7Resp4NO);
                
                BackQuestionObject("TECNICAPC-A-0029", btnPregunta8Resp1SI, btnPregunta8Resp1NO);
                BackQuestionObject("TECNICAPC-A-0030", btnPregunta8Resp2SI, btnPregunta8Resp2NO);
                BackQuestionObject("TECNICAPC-A-0031", btnPregunta8Resp3SI, btnPregunta8Resp3NO);
                BackQuestionObject("TECNICAPC-A-0032", btnPregunta8Resp4SI, btnPregunta8Resp4NO);
               
                BackQuestionObject("TECNICAPC-A-0033", btnPregunta9Resp1SI, btnPregunta9Resp1NO);                
                BackQuestionObject("TECNICAPC-A-0034", btnPregunta9Resp2SI, btnPregunta9Resp2NO);
                BackQuestionObject("TECNICAPC-A-0035", btnPregunta9Resp3SI, btnPregunta9Resp3NO);               
                BackQuestionObject("TECNICAPC-A-0036", btnPregunta9Resp4SI, btnPregunta9Resp4NO);

                BackQuestionObject("TECNICAPC-A-0037", btnPregunta10Resp1SI, btnPregunta10Resp1NO);
                BackQuestionObject("TECNICAPC-A-0038", btnPregunta10Resp2SI, btnPregunta10Resp2NO);                
                BackQuestionObject("TECNICAPC-A-0039", btnPregunta10Resp3SI, btnPregunta10Resp3NO);                
                BackQuestionObject("TECNICAPC-A-0040", btnPregunta10Resp4SI, btnPregunta10Resp4NO);
                
                BackQuestionObject("TECNICAPC-A-0041", btnPregunta11Resp1SI, btnPregunta11Resp1NO);                
                BackQuestionObject("TECNICAPC-A-0042", btnPregunta11Resp2SI, btnPregunta11Resp2NO);
                BackQuestionObject("TECNICAPC-A-0043", btnPregunta11Resp3SI, btnPregunta11Resp3NO);                
                BackQuestionObject("TECNICAPC-A-0044", btnPregunta11Resp4SI, btnPregunta11Resp4NO);


                // Categoria 2
                BackQuestionObject("TECNICAPC-A-0045", btnPregunta12Resp1SI, btnPregunta12Resp1NO);
                BackQuestionObject("TECNICAPC-A-0046", btnPregunta12Resp2SI, btnPregunta12Resp2NO);
                BackQuestionObject("TECNICAPC-A-0047", btnPregunta12Resp3SI, btnPregunta12Resp3NO);                
                BackQuestionObject("TECNICAPC-A-0048", btnPregunta12Resp4SI, btnPregunta12Resp4NO);

                BackQuestionObject("TECNICAPC-A-0049", btnPregunta13Resp1SI, btnPregunta13Resp1NO);
                BackQuestionObject("TECNICAPC-A-0050", btnPregunta13Resp2SI, btnPregunta13Resp2NO);                
                BackQuestionObject("TECNICAPC-A-0051", btnPregunta13Resp3SI, btnPregunta13Resp3NO);
                BackQuestionObject("TECNICAPC-A-0052", btnPregunta13Resp4SI, btnPregunta13Resp4NO);

                BackQuestionObject("TECNICAPC-A-0053", btnPregunta14Resp1SI, btnPregunta14Resp1NO);
                BackQuestionObject("TECNICAPC-A-0054", btnPregunta14Resp2SI, btnPregunta14Resp2NO);
                BackQuestionObject("TECNICAPC-A-0055", btnPregunta14Resp3SI, btnPregunta14Resp3NO);
                BackQuestionObject("TECNICAPC-A-0056", btnPregunta14Resp4SI, btnPregunta14Resp4NO);

                BackQuestionObject("TECNICAPC-A-0057", btnPregunta15Resp1SI, btnPregunta15Resp1NO);
                BackQuestionObject("TECNICAPC-A-0058", btnPregunta15Resp2SI, btnPregunta15Resp2NO);
                BackQuestionObject("TECNICAPC-A-0059", btnPregunta15Resp3SI, btnPregunta15Resp3NO);
                BackQuestionObject("TECNICAPC-A-0060", btnPregunta15Resp4SI, btnPregunta15Resp4NO);

                BackQuestionObject("TECNICAPC-A-0061", btnPregunta16Resp1SI, btnPregunta16Resp1NO);
                BackQuestionObject("TECNICAPC-A-0062", btnPregunta16Resp2SI, btnPregunta16Resp2NO);
                BackQuestionObject("TECNICAPC-A-0063", btnPregunta16Resp3SI, btnPregunta16Resp3NO);
                BackQuestionObject("TECNICAPC-A-0064", btnPregunta16Resp4SI, btnPregunta16Resp4NO);

                BackQuestionObject("TECNICAPC-A-0065", btnPregunta17Resp1SI, btnPregunta17Resp1NO);
                BackQuestionObject("TECNICAPC-A-0066", btnPregunta17Resp2SI, btnPregunta17Resp2NO);
                BackQuestionObject("TECNICAPC-A-0067", btnPregunta17Resp3SI, btnPregunta17Resp3NO);
                BackQuestionObject("TECNICAPC-A-0068", btnPregunta17Resp4SI, btnPregunta17Resp4NO);
                
                BackQuestionObject("TECNICAPC-A-0069", btnPregunta18Resp1SI, btnPregunta18Resp1NO);                
                BackQuestionObject("TECNICAPC-A-0070", btnPregunta18Resp2SI, btnPregunta18Resp2NO);
                BackQuestionObject("TECNICAPC-A-0071", btnPregunta18Resp3SI, btnPregunta18Resp3NO);
                BackQuestionObject("TECNICAPC-A-0072", btnPregunta18Resp4SI, btnPregunta18Resp4NO);

                BackQuestionObject("TECNICAPC-A-0073", btnPregunta19Resp1SI, btnPregunta19Resp1NO);
                BackQuestionObject("TECNICAPC-A-0074", btnPregunta19Resp2SI, btnPregunta19Resp2NO);
                BackQuestionObject("TECNICAPC-A-0075", btnPregunta19Resp3SI, btnPregunta19Resp3NO);                
                BackQuestionObject("TECNICAPC-A-0076", btnPregunta19Resp4SI, btnPregunta19Resp4NO);

                BackQuestionObject("TECNICAPC-A-0077", btnPregunta20Resp1SI, btnPregunta20Resp1NO);
                BackQuestionObject("TECNICAPC-A-0078", btnPregunta20Resp2SI, btnPregunta20Resp2NO);                
                BackQuestionObject("TECNICAPC-A-0079", btnPregunta20Resp3SI, btnPregunta20Resp3NO);                
                BackQuestionObject("TECNICAPC-A-0080", btnPregunta20Resp4SI, btnPregunta21Resp4NO);

                BackQuestionObject("TECNICAPC-A-0081", btnPregunta21Resp1SI, btnPregunta21Resp1NO);
                BackQuestionObject("TECNICAPC-A-0082", btnPregunta21Resp2SI, btnPregunta21Resp2NO);
                BackQuestionObject("TECNICAPC-A-0083", btnPregunta21Resp3SI, btnPregunta21Resp3NO);
                BackQuestionObject("TECNICAPC-A-0084", btnPregunta21Resp4SI, btnPregunta21Resp4NO);

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

                if (vObjetoPrueba != null)
                {
                    E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vObjetoPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: "TECNICAPC", usuario: vClUsuario, programa: vNbPrograma);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "CloseTest");
                }
            }
        }

        public void EditTest()
        {
            CuestionariosNegocio nPreguntas = new CuestionariosNegocio();
            var preguntas = nPreguntas.Obtener_K_PREGUNTA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken);
            if (preguntas.Count > 0)
            {
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


                BackQuestionObject("TECNICAPC-A-0001", btnPregunta1Resp1SI, btnPregunta1Resp1NO);
                BackQuestionObject("TECNICAPC-A-0002", btnPregunta1Resp2SI, btnPregunta1Resp2NO);
                BackQuestionObject("TECNICAPC-A-0003", btnPregunta1Resp3SI, btnPregunta1Resp3NO);
                BackQuestionObject("TECNICAPC-A-0004", btnPregunta1Resp4SI, btnPregunta1Resp4NO);

                BackQuestionObject("TECNICAPC-A-0005", btnPregunta2Resp1SI, btnPregunta2Resp1NO);
                BackQuestionObject("TECNICAPC-A-0006", btnPregunta2Resp2SI, btnPregunta2Resp2NO);
                BackQuestionObject("TECNICAPC-A-0007", btnPregunta2Resp3SI, btnPregunta2Resp3NO);
                BackQuestionObject("TECNICAPC-A-0008", btnPregunta2Resp4SI, btnPregunta2Resp4NO);

                BackQuestionObject("TECNICAPC-A-0009", btnPregunta3Resp1SI, btnPregunta3Resp1NO);
                BackQuestionObject("TECNICAPC-A-0010", btnPregunta3Resp2SI, btnPregunta3Resp2NO);
                BackQuestionObject("TECNICAPC-A-0011", btnPregunta3Resp3SI, btnPregunta3Resp3NO);
                BackQuestionObject("TECNICAPC-A-0012", btnPregunta3Resp4SI, btnPregunta3Resp4NO);

                BackQuestionObject("TECNICAPC-A-0013", btnPregunta4Resp1SI, btnPregunta4Resp1NO);
                BackQuestionObject("TECNICAPC-A-0014", btnPregunta4Resp2SI, btnPregunta4Resp1NO);
                BackQuestionObject("TECNICAPC-A-0015", btnPregunta4Resp3SI, btnPregunta4Resp3NO);
                BackQuestionObject("TECNICAPC-A-0016", btnPregunta4Resp4SI, btnPregunta4Resp4NO);

                BackQuestionObject("TECNICAPC-A-0017", btnPregunta5Resp1SI, btnPregunta5Resp1NO);
                BackQuestionObject("TECNICAPC-A-0018", btnPregunta5Resp2SI, btnPregunta5Resp2NO);
                BackQuestionObject("TECNICAPC-A-0019", btnPregunta5Resp3SI, btnPregunta5Resp3NO);
                BackQuestionObject("TECNICAPC-A-0020", btnPregunta5Resp4SI, btnPregunta5Resp4NO);

                //Categoria 1
                BackQuestionObject("TECNICAPC-A-0021", btnPregunta6Resp1SI, btnPregunta6Resp1NO);
                BackQuestionObject("TECNICAPC-A-0022", btnPregunta6Resp2SI, btnPregunta6Resp2NO);
                BackQuestionObject("TECNICAPC-A-0023", btnPregunta6Resp3SI, btnPregunta6Resp3NO);
                BackQuestionObject("TECNICAPC-A-0024", btnPregunta6Resp4SI, btnPregunta6Resp4NO);

                BackQuestionObject("TECNICAPC-A-0025", btnPregunta7Resp1SI, btnPregunta7Resp1NO);
                BackQuestionObject("TECNICAPC-A-0026", btnPregunta7Resp2SI, btnPregunta7Resp2NO);
                BackQuestionObject("TECNICAPC-A-0027", btnPregunta7Resp3SI, btnPregunta7Resp3NO);
                BackQuestionObject("TECNICAPC-A-0028", btnPregunta7Resp4SI, btnPregunta7Resp4NO);

                BackQuestionObject("TECNICAPC-A-0029", btnPregunta8Resp1SI, btnPregunta8Resp1NO);
                BackQuestionObject("TECNICAPC-A-0030", btnPregunta8Resp2SI, btnPregunta8Resp2NO);
                BackQuestionObject("TECNICAPC-A-0031", btnPregunta8Resp3SI, btnPregunta8Resp3NO);
                BackQuestionObject("TECNICAPC-A-0032", btnPregunta8Resp4SI, btnPregunta8Resp4NO);

                BackQuestionObject("TECNICAPC-A-0033", btnPregunta9Resp1SI, btnPregunta9Resp1NO);
                BackQuestionObject("TECNICAPC-A-0034", btnPregunta9Resp2SI, btnPregunta9Resp2NO);
                BackQuestionObject("TECNICAPC-A-0035", btnPregunta9Resp3SI, btnPregunta9Resp3NO);
                BackQuestionObject("TECNICAPC-A-0036", btnPregunta9Resp4SI, btnPregunta9Resp4NO);

                BackQuestionObject("TECNICAPC-A-0037", btnPregunta10Resp1SI, btnPregunta10Resp1NO);
                BackQuestionObject("TECNICAPC-A-0038", btnPregunta10Resp2SI, btnPregunta10Resp2NO);
                BackQuestionObject("TECNICAPC-A-0039", btnPregunta10Resp3SI, btnPregunta10Resp3NO);
                BackQuestionObject("TECNICAPC-A-0040", btnPregunta10Resp4SI, btnPregunta10Resp4NO);

                BackQuestionObject("TECNICAPC-A-0041", btnPregunta11Resp1SI, btnPregunta11Resp1NO);
                BackQuestionObject("TECNICAPC-A-0042", btnPregunta11Resp2SI, btnPregunta11Resp2NO);
                BackQuestionObject("TECNICAPC-A-0043", btnPregunta11Resp3SI, btnPregunta11Resp3NO);
                BackQuestionObject("TECNICAPC-A-0044", btnPregunta11Resp4SI, btnPregunta11Resp4NO);


                // Categoria 2
                BackQuestionObject("TECNICAPC-A-0045", btnPregunta12Resp1SI, btnPregunta12Resp1NO);
                BackQuestionObject("TECNICAPC-A-0046", btnPregunta12Resp2SI, btnPregunta12Resp2NO);
                BackQuestionObject("TECNICAPC-A-0047", btnPregunta12Resp3SI, btnPregunta12Resp3NO);
                BackQuestionObject("TECNICAPC-A-0048", btnPregunta12Resp4SI, btnPregunta12Resp4NO);

                BackQuestionObject("TECNICAPC-A-0049", btnPregunta13Resp1SI, btnPregunta13Resp1NO);
                BackQuestionObject("TECNICAPC-A-0050", btnPregunta13Resp2SI, btnPregunta13Resp2NO);
                BackQuestionObject("TECNICAPC-A-0051", btnPregunta13Resp3SI, btnPregunta13Resp3NO);
                BackQuestionObject("TECNICAPC-A-0052", btnPregunta13Resp4SI, btnPregunta13Resp4NO);

                BackQuestionObject("TECNICAPC-A-0053", btnPregunta14Resp1SI, btnPregunta14Resp1NO);
                BackQuestionObject("TECNICAPC-A-0054", btnPregunta14Resp2SI, btnPregunta14Resp2NO);
                BackQuestionObject("TECNICAPC-A-0055", btnPregunta14Resp3SI, btnPregunta14Resp3NO);
                BackQuestionObject("TECNICAPC-A-0056", btnPregunta14Resp4SI, btnPregunta14Resp4NO);

                BackQuestionObject("TECNICAPC-A-0057", btnPregunta15Resp1SI, btnPregunta15Resp1NO);
                BackQuestionObject("TECNICAPC-A-0058", btnPregunta15Resp2SI, btnPregunta15Resp2NO);
                BackQuestionObject("TECNICAPC-A-0059", btnPregunta15Resp3SI, btnPregunta15Resp3NO);
                BackQuestionObject("TECNICAPC-A-0060", btnPregunta15Resp4SI, btnPregunta15Resp4NO);

                BackQuestionObject("TECNICAPC-A-0061", btnPregunta16Resp1SI, btnPregunta16Resp1NO);
                BackQuestionObject("TECNICAPC-A-0062", btnPregunta16Resp2SI, btnPregunta16Resp2NO);
                BackQuestionObject("TECNICAPC-A-0063", btnPregunta16Resp3SI, btnPregunta16Resp3NO);
                BackQuestionObject("TECNICAPC-A-0064", btnPregunta16Resp4SI, btnPregunta16Resp4NO);

                BackQuestionObject("TECNICAPC-A-0065", btnPregunta17Resp1SI, btnPregunta17Resp1NO);
                BackQuestionObject("TECNICAPC-A-0066", btnPregunta17Resp2SI, btnPregunta17Resp2NO);
                BackQuestionObject("TECNICAPC-A-0067", btnPregunta17Resp3SI, btnPregunta17Resp3NO);
                BackQuestionObject("TECNICAPC-A-0068", btnPregunta17Resp4SI, btnPregunta17Resp4NO);

                BackQuestionObject("TECNICAPC-A-0069", btnPregunta18Resp1SI, btnPregunta18Resp1NO);
                BackQuestionObject("TECNICAPC-A-0070", btnPregunta18Resp2SI, btnPregunta18Resp2NO);
                BackQuestionObject("TECNICAPC-A-0071", btnPregunta18Resp3SI, btnPregunta18Resp3NO);
                BackQuestionObject("TECNICAPC-A-0072", btnPregunta18Resp4SI, btnPregunta18Resp4NO);

                BackQuestionObject("TECNICAPC-A-0073", btnPregunta19Resp1SI, btnPregunta19Resp1NO);
                BackQuestionObject("TECNICAPC-A-0074", btnPregunta19Resp2SI, btnPregunta19Resp2NO);
                BackQuestionObject("TECNICAPC-A-0075", btnPregunta19Resp3SI, btnPregunta19Resp3NO);
                BackQuestionObject("TECNICAPC-A-0076", btnPregunta19Resp4SI, btnPregunta19Resp4NO);

                BackQuestionObject("TECNICAPC-A-0077", btnPregunta20Resp1SI, btnPregunta20Resp1NO);
                BackQuestionObject("TECNICAPC-A-0078", btnPregunta20Resp2SI, btnPregunta20Resp2NO);
                BackQuestionObject("TECNICAPC-A-0079", btnPregunta20Resp3SI, btnPregunta20Resp3NO);
                BackQuestionObject("TECNICAPC-A-0080", btnPregunta20Resp4SI, btnPregunta21Resp4NO);

                BackQuestionObject("TECNICAPC-A-0081", btnPregunta21Resp1SI, btnPregunta21Resp1NO);
                BackQuestionObject("TECNICAPC-A-0082", btnPregunta21Resp2SI, btnPregunta21Resp2NO);
                BackQuestionObject("TECNICAPC-A-0083", btnPregunta21Resp3SI, btnPregunta21Resp3NO);
                BackQuestionObject("TECNICAPC-A-0084", btnPregunta21Resp4SI, btnPregunta21Resp4NO);

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

                if (vObjetoPrueba != null)
                {
                    E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vObjetoPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: "TECNICAPC", usuario: vClUsuario, programa: vNbPrograma);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
                }
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
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.WARNING && vMensaje == "NO")
            {
                    UtilMensajes.MensajeResultadoDB(rnMensaje, "Usted ha tratado de capturar doble una prueba; los datos no fueron guardados.", E_TIPO_RESPUESTA_DB.SUCCESSFUL, 400, 150, "CloseTest");
            }
            else
            {
                SaveTest();
            }
        }

        public void BackQuestionObject(string pclPregunta, RadButton si, RadButton no)
        {
            var vPregunta = vRespuestas.Where(x => x.CL_PREGUNTA.Equals(pclPregunta)).FirstOrDefault();
            int pRespuesta=0;

            if (vPregunta != null)
            {
                if (si.Checked)
                {
                    pRespuesta = int.Parse(si.Value);   
                }
                else if (no.Checked)
                {
                    pRespuesta = int.Parse(no.Value);
                }
                else {
                    pRespuesta = 2;
                }
                
                decimal vNoValor;

                if (pRespuesta == 2)
                    vPregunta.NB_RESPUESTA = "";
                else
                    vPregunta.NB_RESPUESTA = pRespuesta.ToString();

                if ((pRespuesta.ToString() == "0") || (pRespuesta.ToString() == "2"))
                    vNoValor = 0;
                else
                    vNoValor = 1;

                vPregunta.NO_VALOR_RESPUESTA = vNoValor;
            }
        }

        public void asignarValores(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> respuestas)
        {
            foreach (SPE_OBTIENE_RESULTADO_PRUEBA_Result resp in respuestas)
            {
                switch (resp.CL_PREGUNTA)
                {
                    case "TECNICAPC-A-0001": SeleccionarBotonRespuesta(btnPregunta1Resp1SI, btnPregunta1Resp1NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0002": SeleccionarBotonRespuesta(btnPregunta1Resp2SI, btnPregunta1Resp2NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0003": SeleccionarBotonRespuesta(btnPregunta1Resp3SI, btnPregunta1Resp3NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0004": SeleccionarBotonRespuesta(btnPregunta1Resp4SI, btnPregunta1Resp4NO, resp.NB_RESPUESTA); break;
                  
                    case "TECNICAPC-A-0005": SeleccionarBotonRespuesta(btnPregunta2Resp1SI, btnPregunta2Resp1NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0006": SeleccionarBotonRespuesta(btnPregunta2Resp2SI, btnPregunta2Resp2NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0007": SeleccionarBotonRespuesta(btnPregunta2Resp3SI, btnPregunta2Resp3NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0008": SeleccionarBotonRespuesta(btnPregunta2Resp4SI, btnPregunta2Resp4NO, resp.NB_RESPUESTA); break;
                   
                    case "TECNICAPC-A-0009": SeleccionarBotonRespuesta(btnPregunta3Resp1SI, btnPregunta3Resp1NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0010": SeleccionarBotonRespuesta(btnPregunta3Resp2SI, btnPregunta3Resp2NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0011": SeleccionarBotonRespuesta(btnPregunta3Resp3SI, btnPregunta3Resp3NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0012": SeleccionarBotonRespuesta(btnPregunta3Resp4SI, btnPregunta3Resp4NO, resp.NB_RESPUESTA); break;
                    
                    case "TECNICAPC-A-0013": SeleccionarBotonRespuesta(btnPregunta4Resp1SI, btnPregunta4Resp1NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0014": SeleccionarBotonRespuesta(btnPregunta4Resp2SI, btnPregunta4Resp2NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0015": SeleccionarBotonRespuesta(btnPregunta4Resp3SI, btnPregunta4Resp3NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0016": SeleccionarBotonRespuesta(btnPregunta4Resp4SI, btnPregunta4Resp4NO, resp.NB_RESPUESTA); break;
                    
                    case "TECNICAPC-A-0017": SeleccionarBotonRespuesta(btnPregunta5Resp1SI, btnPregunta5Resp1NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0018": SeleccionarBotonRespuesta(btnPregunta5Resp2SI, btnPregunta5Resp2NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0019": SeleccionarBotonRespuesta(btnPregunta5Resp3SI, btnPregunta5Resp3NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0020": SeleccionarBotonRespuesta(btnPregunta5Resp4SI, btnPregunta5Resp4NO, resp.NB_RESPUESTA); break;
                    
                    case "TECNICAPC-A-0021": SeleccionarBotonRespuesta(btnPregunta6Resp1SI, btnPregunta6Resp1NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0022": SeleccionarBotonRespuesta(btnPregunta6Resp2SI, btnPregunta6Resp2NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0023": SeleccionarBotonRespuesta(btnPregunta6Resp3SI, btnPregunta6Resp3NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0024": SeleccionarBotonRespuesta(btnPregunta6Resp4SI, btnPregunta6Resp4NO, resp.NB_RESPUESTA); break;
                    
                    case "TECNICAPC-A-0025": SeleccionarBotonRespuesta(btnPregunta7Resp1SI, btnPregunta7Resp1NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0026": SeleccionarBotonRespuesta(btnPregunta7Resp2SI, btnPregunta7Resp2NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0027": SeleccionarBotonRespuesta(btnPregunta7Resp3SI, btnPregunta7Resp3NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0028": SeleccionarBotonRespuesta(btnPregunta7Resp4SI, btnPregunta7Resp4NO, resp.NB_RESPUESTA); break;
                    
                    case "TECNICAPC-A-0029": SeleccionarBotonRespuesta(btnPregunta8Resp1SI, btnPregunta8Resp1NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0030": SeleccionarBotonRespuesta(btnPregunta8Resp2SI, btnPregunta8Resp2NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0031": SeleccionarBotonRespuesta(btnPregunta8Resp3SI, btnPregunta8Resp3NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0032": SeleccionarBotonRespuesta(btnPregunta8Resp4SI, btnPregunta8Resp4NO, resp.NB_RESPUESTA); break;
                    
                    case "TECNICAPC-A-0033": SeleccionarBotonRespuesta(btnPregunta9Resp1SI, btnPregunta9Resp1NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0034": SeleccionarBotonRespuesta(btnPregunta9Resp2SI, btnPregunta9Resp2NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0035": SeleccionarBotonRespuesta(btnPregunta9Resp3SI, btnPregunta9Resp3NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0036": SeleccionarBotonRespuesta(btnPregunta9Resp4SI, btnPregunta9Resp4NO, resp.NB_RESPUESTA); break;
                    
                    case "TECNICAPC-A-0037": SeleccionarBotonRespuesta(btnPregunta10Resp1SI, btnPregunta10Resp1NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0038": SeleccionarBotonRespuesta(btnPregunta10Resp2SI, btnPregunta10Resp2NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0039": SeleccionarBotonRespuesta(btnPregunta10Resp3SI, btnPregunta10Resp3NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0040": SeleccionarBotonRespuesta(btnPregunta10Resp4SI, btnPregunta10Resp4NO, resp.NB_RESPUESTA); break;
                    
                    case "TECNICAPC-A-0041": SeleccionarBotonRespuesta(btnPregunta11Resp1SI, btnPregunta11Resp1NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0042": SeleccionarBotonRespuesta(btnPregunta11Resp2SI, btnPregunta11Resp2NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0043": SeleccionarBotonRespuesta(btnPregunta11Resp3SI, btnPregunta11Resp3NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0044": SeleccionarBotonRespuesta(btnPregunta11Resp4SI, btnPregunta11Resp4NO, resp.NB_RESPUESTA); break;
                    
                    case "TECNICAPC-A-0045": SeleccionarBotonRespuesta(btnPregunta12Resp1SI, btnPregunta12Resp1NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0046": SeleccionarBotonRespuesta(btnPregunta12Resp2SI, btnPregunta12Resp2NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0047": SeleccionarBotonRespuesta(btnPregunta12Resp3SI, btnPregunta12Resp3NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0048": SeleccionarBotonRespuesta(btnPregunta12Resp4SI, btnPregunta12Resp4NO, resp.NB_RESPUESTA); break;
                    
                    case "TECNICAPC-A-0049": SeleccionarBotonRespuesta(btnPregunta13Resp1SI, btnPregunta13Resp1NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0050": SeleccionarBotonRespuesta(btnPregunta13Resp2SI, btnPregunta13Resp2NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0051": SeleccionarBotonRespuesta(btnPregunta13Resp3SI, btnPregunta13Resp3NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0052": SeleccionarBotonRespuesta(btnPregunta13Resp4SI, btnPregunta13Resp4NO, resp.NB_RESPUESTA); break;
                    
                    case "TECNICAPC-A-0053": SeleccionarBotonRespuesta(btnPregunta14Resp1SI, btnPregunta14Resp1NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0054": SeleccionarBotonRespuesta(btnPregunta14Resp2SI, btnPregunta14Resp2NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0055": SeleccionarBotonRespuesta(btnPregunta14Resp3SI, btnPregunta14Resp3NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0056": SeleccionarBotonRespuesta(btnPregunta14Resp4SI, btnPregunta14Resp4NO, resp.NB_RESPUESTA); break;
                    
                    case "TECNICAPC-A-0057": SeleccionarBotonRespuesta(btnPregunta15Resp1SI, btnPregunta15Resp1NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0058": SeleccionarBotonRespuesta(btnPregunta15Resp2SI, btnPregunta15Resp2NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0059": SeleccionarBotonRespuesta(btnPregunta15Resp3SI, btnPregunta15Resp3NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0060": SeleccionarBotonRespuesta(btnPregunta15Resp4SI, btnPregunta15Resp4NO, resp.NB_RESPUESTA); break;
                   
                    case "TECNICAPC-A-0061": SeleccionarBotonRespuesta(btnPregunta16Resp1SI, btnPregunta16Resp1NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0062": SeleccionarBotonRespuesta(btnPregunta16Resp2SI, btnPregunta16Resp2NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0063": SeleccionarBotonRespuesta(btnPregunta16Resp3SI, btnPregunta16Resp3NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0064": SeleccionarBotonRespuesta(btnPregunta16Resp4SI, btnPregunta16Resp4NO, resp.NB_RESPUESTA); break;
                    
                    case "TECNICAPC-A-0065": SeleccionarBotonRespuesta(btnPregunta17Resp1SI, btnPregunta17Resp1NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0066": SeleccionarBotonRespuesta(btnPregunta17Resp2SI, btnPregunta17Resp2NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0067": SeleccionarBotonRespuesta(btnPregunta17Resp3SI, btnPregunta17Resp3NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0068": SeleccionarBotonRespuesta(btnPregunta17Resp4SI, btnPregunta17Resp4NO, resp.NB_RESPUESTA); break;
                    
                    case "TECNICAPC-A-0069": SeleccionarBotonRespuesta(btnPregunta18Resp1SI, btnPregunta18Resp1NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0070": SeleccionarBotonRespuesta(btnPregunta18Resp2SI, btnPregunta18Resp2NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0071": SeleccionarBotonRespuesta(btnPregunta18Resp3SI, btnPregunta18Resp3NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0072": SeleccionarBotonRespuesta(btnPregunta18Resp4SI, btnPregunta18Resp4NO, resp.NB_RESPUESTA); break;
                    
                    case "TECNICAPC-A-0073": SeleccionarBotonRespuesta(btnPregunta19Resp1SI, btnPregunta19Resp1NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0074": SeleccionarBotonRespuesta(btnPregunta19Resp2SI, btnPregunta19Resp2NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0075": SeleccionarBotonRespuesta(btnPregunta19Resp3SI, btnPregunta19Resp3NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0076": SeleccionarBotonRespuesta(btnPregunta19Resp4SI, btnPregunta19Resp4NO, resp.NB_RESPUESTA); break;
                    
                    case "TECNICAPC-A-0077": SeleccionarBotonRespuesta(btnPregunta20Resp1SI, btnPregunta20Resp1NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0078": SeleccionarBotonRespuesta(btnPregunta20Resp2SI, btnPregunta20Resp2NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0079": SeleccionarBotonRespuesta(btnPregunta20Resp3SI, btnPregunta20Resp3NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0080": SeleccionarBotonRespuesta(btnPregunta20Resp4SI, btnPregunta20Resp4NO, resp.NB_RESPUESTA); break;
                    
                    case "TECNICAPC-A-0081": SeleccionarBotonRespuesta(btnPregunta21Resp1SI, btnPregunta21Resp1NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0082": SeleccionarBotonRespuesta(btnPregunta21Resp2SI, btnPregunta21Resp2NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0083": SeleccionarBotonRespuesta(btnPregunta21Resp3SI, btnPregunta21Resp3NO, resp.NB_RESPUESTA); break;
                    case "TECNICAPC-A-0084": SeleccionarBotonRespuesta(btnPregunta21Resp4SI, btnPregunta21Resp4NO, resp.NB_RESPUESTA); break;
                  }
            }
        }

        public void SeleccionarBotonRespuesta(RadButton a, RadButton b, string pAnswer)
        {
            if (a.Value.Equals(pAnswer))
            {
                a.Checked = true;
            }
            else if (b.Value.Equals(pAnswer))
            {
                b.Checked = true;
            }
            else 
            { 
            //DEJAR SIN RESPONDER
            }
        }

        protected void btnCorregir_Click(object sender, EventArgs e)
        {
            PruebasNegocio nKprueba = new PruebasNegocio();
            SPE_OBTIENE_K_PRUEBA_Result vPruebaTerminada = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();
            E_RESULTADO vResultado = nKprueba.CorrigePrueba(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            if (vResultado.CL_TIPO_ERROR != E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
            }
            else
            {
                EditTest();
            }
        }

        protected void btnEliminarBateria_Click(object sender, EventArgs e)
        {
            PruebasNegocio nPruebas = new PruebasNegocio();
            var vResultado = nPruebas.EliminaRespuestasBaterias(vIdBateria, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
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

                }
                else
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
            }

        }
    }
}