using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.IDP.Pruebas
{
    public partial class VentanaIngles : System.Web.UI.Page
    {
        #region Propiedades
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private List<E_PREGUNTA> vRespuestas
        {
            get { return (List<E_PREGUNTA>)ViewState["vsRespuestas"]; }
            set { ViewState["vsRespuestas"] = value; }
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

        public int vIdPrueba
        {
            get { return (int)ViewState["vsIdEvaluado"]; }
            set { ViewState["vsIdEvaluado"] = value; }
        }

        public Guid vClToken
        {
            get { return (Guid)ViewState["vsClToken"]; }
            set { ViewState["vsClToken"] = value; }
        }

        public List<E_PRUEBA_TIEMPO> vSeccionesPrueba
        {
            get { return (List<E_PRUEBA_TIEMPO>)ViewState["vSeccionesPrueba"]; }
            set { ViewState["vSeccionesPrueba"] = value; }
        }

        public int? vTiempoInicio
        {
            get { return (int?)ViewState["vTiempoInicio"]; }
            set { ViewState["vTiempoInicio"] = value; }
        }

        public int? vSeccionAtime
        {
            get { return (int?)ViewState["vSeccionAtime"]; }
            set { ViewState["vSeccionAtime"] = value; }
        }

        public int? vSeccionBtime
        {
            get { return (int?)ViewState["vSeccionBtime"]; }
            set { ViewState["vSeccionBtime"] = value; }
        }

        public int? vSeccionCtime
        {
            get { return (int?)ViewState["vSeccionCtime"]; }
            set { ViewState["vSeccionCtime"] = value; }
        }

        public int? vSeccionDtime
        {
            get { return (int?)ViewState["vSeccionDtime"]; }
            set { ViewState["vSeccionDtime"] = value; }
        }

        public int vIndexMultiPage
        {
            get { return (int)ViewState["vIndexMultipage"]; }
            set { ViewState["vIndexMultipage"] = value; }
        }

        public int vRadAlertAltura
        {
            get { return (int)ViewState["vRadAlertAltura"]; }
            set { ViewState["vRadAlertAltura"] = value; }
        }

        public string vTipoRevision
        {
            get { return (string)ViewState["vsTipoRevision"]; }
            set { ViewState["vsTipoRevision"] = value; }
        }

        public List<E_RESULTADOS_PRUEBA> vResultadosRevision
        {
            get { return (List<E_RESULTADOS_PRUEBA>)ViewState["vsResultadosRevision"]; }
            set { ViewState["vsResultadosRevision"] = value; }
        }

        public bool MostrarCronometro
        {
            get { return (bool)ViewState["vsMostrarCronometroING"]; }
            set { ViewState["vsMostrarCronometroING"] = value; }
        }

        public int vIdBateria
        {
            get { return (int)ViewState["vsIdBateria"]; }
            set { ViewState["vsIdBateria"] = value; }
        }
        #endregion

        public void initRespuestasIngles()
        {
               PruebasNegocio nKprueba = new PruebasNegocio();
               MostrarCronometro = true;
               vIdPrueba = int.Parse(Request.QueryString["ID"]);
               vClToken = new Guid(Request.QueryString["T"]);
               vTipoRevision = Request.QueryString["MOD"];
               vRadAlertAltura = HeightRadAlert(0);
               int position = mpgIngles.SelectedIndex;
               vSeccionesPrueba = new List<E_PRUEBA_TIEMPO>();
               var vSegmentos = nKprueba.Obtener_K_PRUEBA_SECCION(pIdPrueba: vIdPrueba);
               vSeccionesPrueba = ParseList(vSegmentos);
               //Si el modo de revision esta activado
               if (vTipoRevision == "REV" || vTipoRevision == "EDIT")
               {
                   cronometro.Visible = false;
                   vTiempoInicio = 0;
                   btnTerminar.Text = "Guardar";
                  // btnEliminar.Visible = true;// Se agrega para la nueva forma de navegación 06/06/2018
                   btnImpresionPrueba.Visible = true; // Se agrega para imprimir en la nueva navegación IDP 06/06/2018
                   if (vTipoRevision == "REV")
                   {
                       btnTerminar.Enabled = false;
                   }

                   var vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken).FirstOrDefault();
                   if (vPrueba.NB_TIPO_PRUEBA == "MANUAL")
                       btnTerminar.Enabled = false;

                   //obtener respuestas
                   var respuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);
                   vResultadosRevision = new List<E_RESULTADOS_PRUEBA>();
                   foreach (var item in respuestas)
                   {
                       vResultadosRevision.Add(new E_RESULTADOS_PRUEBA
                       {
                           ID_PRUEBA = item.ID_PRUEBA,
                           ID_PREGUNTA = item.ID_PREGUNTA,
                           ID_VARIABLE = item.ID_VARIABLE,
                           CL_PREGUNTA = item.CL_PREGUNTA,
                           CL_TIPO_VARIABLE = item.CL_TIPO_VARIABLE,
                           NB_PREGUNTA = item.NB_PREGUNTA,
                           NB_RESPUESTA = item.NB_RESPUESTA,
                           NO_VALOR_RESPUESTA = item.NO_VALOR_RESPUESTA
                       });
                   }
                   asignarValores(vResultadosRevision.Where(item => item.CL_PREGUNTA.Contains("INGLES-" + BackLetterQuestions(mpgIngles.SelectedIndex) + "-")).ToList());
                   habilitarResultadosIngles(vResultadosRevision);
           }
       }

        protected void Page_Load(object sender, EventArgs e)
        {
            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = (ContextoUsuario.oUsuario != null ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO");

            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null && Request.QueryString["MOD"] == null)
                {
                    MostrarCronometro = ContextoApp.IDP.ConfiguracionPsicometria.FgMostrarCronometro;

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

                    int position = mpgIngles.SelectedIndex;
                    vSeccionesPrueba = new List<E_PRUEBA_TIEMPO>();
                    var vSegmentos = nKprueba.Obtener_K_PRUEBA_SECCION(pIdPrueba: vIdPrueba);
                    vSeccionesPrueba = ParseList(vSegmentos);

                    int VPosicionPrueba = IniciaPruebaSeccion(vSeccionesPrueba);
                    E_RESULTADO vObjetoPrueba = nKprueba.INICIAR_K_PRUEBA_SECCION(pIdPrueba: vSeccionesPrueba.ElementAt(VPosicionPrueba).ID_PRUEBA_SECCION, pFeInicio: DateTime.Now, usuario: vClUsuario, programa: vNbPrograma);
                    E_RESULTADO vPrueba = nKprueba.INICIAR_K_PRUEBA(pIdPrueba: vIdPrueba, pFeInicio: DateTime.Now, pClTokenExterno: vClToken, usuario: vClUsuario, programa: vNbPrograma);

                    if (vObjetoPrueba != null)
                    {
                        if (vObjetoPrueba.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.ERROR)
                        {
                        }
                        else if (vObjetoPrueba.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                        {
                            vTiempoInicio = int.Parse(vObjetoPrueba.MENSAJE.Where(r => r.CL_IDIOMA.Equals("ES")).FirstOrDefault().DS_MENSAJE.ToString());
                        }
                        controltime(VPosicionPrueba, vTiempoInicio);
                        vRadAlertAltura = HeightRadAlert(VPosicionPrueba);
                    }
                }
                else 
                {
                    initRespuestasIngles();
                    mpgIngles.RenderSelectedPageOnly = true;
                }
                vRespuestas = new List<E_PREGUNTA>();
               
            }
        }

        public int IniciaPruebaSeccion(List<E_PRUEBA_TIEMPO> vListaSecciones)
        {
            vIndexMultiPage = 0;
            foreach (var item in vListaSecciones)
            {
                if (item.CL_ESTADO.Equals("CREADA") || item.CL_ESTADO.Equals("INICIADA"))
                {
                    break;
                }
                else
                {
                    vIndexMultiPage++;
                }
            }
            mpgIngles.SelectedIndex = vIndexMultiPage;
            return (vIndexMultiPage == 4) ? 3 : vIndexMultiPage;
        }

        public List<E_PRUEBA_TIEMPO> ParseList(List<SPE_OBTIENE_K_PRUEBA_SECCION_Result> list)
        {
            List<E_PRUEBA_TIEMPO> vlista_Secciones = new List<E_PRUEBA_TIEMPO>();
            foreach (SPE_OBTIENE_K_PRUEBA_SECCION_Result item in list)
            {
                vlista_Secciones.Add(new E_PRUEBA_TIEMPO
                {
                    ID_PRUEBA_SECCION = item.ID_PRUEBA_SECCION,
                    ID_PRUEBA = item.ID_PRUEBA,
                    CL_PRUEBA_SECCION = item.CL_PRUEBA_SECCION,
                    NB_PRUEBA_SECCION = item.NB_PRUEBA_SECCION,
                    NO_TIEMPO = (short)item.NO_TIEMPO,
                    CL_ESTADO = item.CL_ESTADO,
                    FE_INICIO = item.FE_INICIO,
                    FE_TERMINO = item.FE_TERMINO
                });
            }
            return vlista_Secciones;
        }

        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            PruebasNegocio nKprueba = new PruebasNegocio();
            var vSeccionTermina = vSeccionesPrueba.ElementAt(mpgIngles.SelectedIndex);
            if (vTipoRevision == "EDIT")
            {
                E_RESULTADO vResultadoSeccion = nKprueba.InsertaActualiza_K_PRUEBA_SECCION(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), v_k_prueba: vSeccionTermina, usuario: vClUsuario, programa: vNbPrograma);
                if (vResultadoSeccion.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                    EditTest(mpgIngles.SelectedIndex);
                else
                {
                    string vMensaje = vResultadoSeccion.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultadoSeccion.CL_TIPO_ERROR, 400, 150, "");
                }
            }
            else
            {
                vSeccionTermina.CL_ESTADO = E_ESTADO_PRUEBA.TERMINADA.ToString();
                vSeccionTermina.FE_TERMINO = DateTime.Now;
                E_RESULTADO vResultadoSeccion = nKprueba.InsertaActualiza_K_PRUEBA_SECCION(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), v_k_prueba: vSeccionTermina, usuario: vClUsuario, programa: vNbPrograma);
                if (vResultadoSeccion.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                    SaveTest(mpgIngles.SelectedIndex);
                else
                {
                    string vMensaje = vResultadoSeccion.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultadoSeccion.CL_TIPO_ERROR, 400, 150, "CloseTest");
                }
            }
        }

        public void SaveTest(int vseccion)
        {
            CuestionariosNegocio nPreguntas = new CuestionariosNegocio();
            var preguntas = nPreguntas.Obtener_K_PREGUNTA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken);
            var filtroPreguntas = preguntas.Where(oh => oh.CL_PREGUNTA.StartsWith("INGLES-" + BackLetterQuestions(vseccion))).ToList();
            vRespuestas = new List<E_PREGUNTA>();
            if (filtroPreguntas.Count > 0)
            {
                foreach (SPE_OBTIENE_K_PREGUNTA_Result pregunta in filtroPreguntas)
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

                switch (vseccion)
                {
                    case 0:
                        String INGLES_A_0001 = BackSelectedCheckBox(APregunta1Resp1, APregunta1Resp2, APregunta1Resp3, APregunta1Resp4);
                        BackQuestionObject("b", "INGLES-A-0001", INGLES_A_0001);

                        String INGLES_A_0002 = BackSelectedCheckBox(APregunta2Resp1, APregunta2Resp2, APregunta2Resp3, APregunta2Resp4);
                        BackQuestionObject("c", "INGLES-A-0002", INGLES_A_0002);

                        String INGLES_A_0003 = BackSelectedCheckBox(APregunta3Resp1, APregunta3Resp2, APregunta3Resp3, APregunta3Resp4);
                        BackQuestionObject("c", "INGLES-A-0003", INGLES_A_0003);

                        String INGLES_A_0004 = BackSelectedCheckBox(APregunta4Resp1, APregunta4Resp2, APregunta4Resp3, APregunta4Resp4);
                        BackQuestionObject("c", "INGLES-A-0004", INGLES_A_0004);

                        String INGLES_A_0005 = BackSelectedCheckBox(APregunta5Resp1, APregunta5Resp2, APregunta5Resp3, APregunta5Resp4);
                        BackQuestionObject("c", "INGLES-A-0005", INGLES_A_0005);

                        String INGLES_A_0006 = BackSelectedCheckBox(APregunta6Resp1, APregunta6Resp2, APregunta6Resp3, APregunta6Resp4);
                        BackQuestionObject("c", "INGLES-A-0006", INGLES_A_0006);

                        String INGLES_A_0007 = BackSelectedCheckBox(APregunta7Resp1, APregunta7Resp2, APregunta7Resp3, APregunta7Resp4);
                        BackQuestionObject("b", "INGLES-A-0007", INGLES_A_0007);

                        String INGLES_A_0008 = BackSelectedCheckBox(APregunta8Resp1, APregunta8Resp2, APregunta8Resp3, APregunta8Resp4);
                        BackQuestionObject("b", "INGLES-A-0008", INGLES_A_0008);

                        String INGLES_A_0009 = BackSelectedCheckBox(APregunta9Resp1, APregunta9Resp2, APregunta9Resp3, APregunta9Resp4);
                        BackQuestionObject("d", "INGLES-A-0009", INGLES_A_0009);

                        String INGLES_A_0010 = BackSelectedCheckBox(APregunta10Resp1, APregunta10Resp2, APregunta10Resp3, APregunta10Resp4);
                        BackQuestionObject("c", "INGLES-A-0010", INGLES_A_0010);

                        String INGLES_A_0011 = BackSelectedCheckBox(APregunta11Resp1, APregunta11Resp2, APregunta11Resp3, APregunta11Resp4);
                        BackQuestionObject("c", "INGLES-A-0011", INGLES_A_0011);

                        String INGLES_A_0012 = BackSelectedCheckBox(APregunta12Resp1, APregunta12Resp2, APregunta12Resp3, APregunta12Resp4);
                        BackQuestionObject("d", "INGLES-A-0012", INGLES_A_0012);

                        String INGLES_A_0013 = BackSelectedCheckBox(APregunta13Resp1, APregunta13Resp2, APregunta13Resp3, APregunta13Resp4);
                        BackQuestionObject("c", "INGLES-A-0013", INGLES_A_0013);

                        String INGLES_A_0014 = BackSelectedCheckBox(APregunta14Resp1, APregunta14Resp2, APregunta14Resp3, APregunta14Resp4);
                        BackQuestionObject("a", "INGLES-A-0014", INGLES_A_0014);

                        String INGLES_A_0015 = BackSelectedCheckBox(APregunta15Resp1, APregunta15Resp2, APregunta15Resp3, APregunta15Resp4);
                        BackQuestionObject("a", "INGLES-A-0015", INGLES_A_0015);

                        String INGLES_A_0016 = BackSelectedCheckBox(APregunta16Resp1, APregunta16Resp2, APregunta16Resp3, APregunta16Resp4);
                        BackQuestionObject("b", "INGLES-A-0016", INGLES_A_0016);

                        String INGLES_A_0017 = BackSelectedCheckBox(APregunta17Resp1, APregunta17Resp2, APregunta17Resp3, APregunta17Resp4);
                        BackQuestionObject("c", "INGLES-A-0017", INGLES_A_0017);

                        String INGLES_A_0018 = BackSelectedCheckBox(APregunta18Resp1, APregunta18Resp2, APregunta18Resp3, APregunta18Resp4);
                        BackQuestionObject("b", "INGLES-A-0018", INGLES_A_0018);

                        String INGLES_A_0019 = BackSelectedCheckBox(APregunta19Resp1, APregunta19Resp2, APregunta19Resp3, APregunta19Resp4);
                        BackQuestionObject("d", "INGLES-A-0019", INGLES_A_0019);

                        String INGLES_A_0020 = BackSelectedCheckBox(APregunta20Resp1, APregunta20Resp2, APregunta20Resp3, APregunta20Resp4);
                        BackQuestionObject("c", "INGLES-A-0020", INGLES_A_0020);

                        String INGLES_A_0021 = BackSelectedCheckBox(APregunta21Resp1, APregunta21Resp2, APregunta21Resp3, APregunta21Resp4);
                        BackQuestionObject("c", "INGLES-A-0021", INGLES_A_0021);

                        String INGLES_A_0022 = BackSelectedCheckBox(APregunta22Resp1, APregunta22Resp2, APregunta22Resp3, APregunta22Resp4);
                        BackQuestionObject("c", "INGLES-A-0022", INGLES_A_0022);

                        String INGLES_A_0023 = BackSelectedCheckBox(APregunta23Resp1, APregunta23Resp2, APregunta23Resp3, APregunta23Resp4);
                        BackQuestionObject("d", "INGLES-A-0023", INGLES_A_0023);

                        String INGLES_A_0024 = BackSelectedCheckBox(APregunta24Resp1, APregunta24Resp2, APregunta24Resp3, APregunta24Resp4);
                        BackQuestionObject("d", "INGLES-A-0024", INGLES_A_0024);

                        String INGLES_A_0025 = BackSelectedCheckBox(APregunta25Resp1, APregunta25Resp2, APregunta25Resp3, APregunta25Resp4);
                        BackQuestionObject("a", "INGLES-A-0025", INGLES_A_0025);

                        String INGLES_A_0026 = BackSelectedCheckBox(APregunta26Resp1, APregunta26Resp2, APregunta26Resp3, APregunta26Resp4);
                        BackQuestionObject("c", "INGLES-A-0026", INGLES_A_0026);

                        String INGLES_A_0027 = BackSelectedCheckBox(APregunta27Resp1, APregunta27Resp2, APregunta27Resp3, APregunta27Resp4);
                        BackQuestionObject("c", "INGLES-A-0027", INGLES_A_0027);

                        String INGLES_A_0028 = BackSelectedCheckBox(APregunta28Resp1, APregunta28Resp2, APregunta28Resp3, APregunta28Resp4);
                        BackQuestionObject("b", "INGLES-A-0028", INGLES_A_0028);

                        String INGLES_A_0029 = BackSelectedCheckBox(APregunta29Resp1, APregunta29Resp2, APregunta29Resp3, APregunta29Resp4);
                        BackQuestionObject("c", "INGLES-A-0029", INGLES_A_0029);

                        String INGLES_A_0030 = BackSelectedCheckBox(APregunta30Resp1, APregunta30Resp2, APregunta30Resp3, APregunta30Resp4);
                        BackQuestionObject("d", "INGLES-A-0030", INGLES_A_0030);
                        break;

                    case 1:
                        //////////////////////////////////////////////////SECCION B/////////////////////////////////////////////////////////////////////

                        String INGLES_B_0001 = BackSelectedCheckBox(BPregunta1Resp1, BPregunta1Resp2, BPregunta1Resp3, BPregunta1Resp4);
                        BackQuestionObject("c", "INGLES-B-0001", INGLES_B_0001);

                        String INGLES_B_0002 = BackSelectedCheckBox(BPregunta2Resp1, BPregunta2Resp2, BPregunta2Resp3, BPregunta2Resp4);
                        BackQuestionObject("d", "INGLES-B-0002", INGLES_B_0002);

                        String INGLES_B_0003 = BackSelectedCheckBox(BPregunta3Resp1, BPregunta3Resp2, BPregunta3Resp3, BPregunta3Resp4);
                        BackQuestionObject("b", "INGLES-B-0003", INGLES_B_0003);

                        String INGLES_B_0004 = BackSelectedCheckBox(BPregunta4Resp1, BPregunta4Resp2, BPregunta4Resp3, BPregunta4Resp4);
                        BackQuestionObject("d", "INGLES-B-0004", INGLES_B_0004);

                        String INGLES_B_0005 = BackSelectedCheckBox(BPregunta5Resp1, BPregunta5Resp2, BPregunta5Resp3, BPregunta5Resp4);
                        BackQuestionObject("c", "INGLES-B-0005", INGLES_B_0005);

                        String INGLES_B_0006 = BackSelectedCheckBox(BPregunta6Resp1, BPregunta6Resp2, BPregunta6Resp3, BPregunta6Resp4);
                        BackQuestionObject("c", "INGLES-B-0006", INGLES_B_0006);

                        String INGLES_B_0007 = BackSelectedCheckBox(BPregunta7Resp1, BPregunta7Resp2, BPregunta7Resp3, BPregunta7Resp4);
                        BackQuestionObject("b", "INGLES-B-0007", INGLES_B_0007);

                        String INGLES_B_0008 = BackSelectedCheckBox(BPregunta8Resp1, BPregunta8Resp2, BPregunta8Resp3, BPregunta8Resp4);
                        BackQuestionObject("c", "INGLES-B-0008", INGLES_B_0008);

                        String INGLES_B_0009 = BackSelectedCheckBox(BPregunta9Resp1, BPregunta9Resp2, BPregunta9Resp3, BPregunta9Resp4);
                        BackQuestionObject("b", "INGLES-B-0009", INGLES_B_0009);

                        String INGLES_B_0010 = BackSelectedCheckBox(BPregunta10Resp1, BPregunta10Resp2, BPregunta10Resp3, BPregunta10Resp4);
                        BackQuestionObject("c", "INGLES-B-0010", INGLES_B_0010);

                        String INGLES_B_0011 = BackSelectedCheckBox(BPregunta11Resp1, BPregunta11Resp2, BPregunta11Resp3, BPregunta11Resp4);
                        BackQuestionObject("c", "INGLES-B-0011", INGLES_B_0011);

                        String INGLES_B_0012 = BackSelectedCheckBox(BPregunta12Resp1, BPregunta12Resp2, BPregunta12Resp3, BPregunta12Resp4);
                        BackQuestionObject("a", "INGLES-B-0012", INGLES_B_0012);

                        String INGLES_B_0013 = BackSelectedCheckBox(BPregunta13Resp1, BPregunta13Resp2, BPregunta13Resp3, BPregunta13Resp4);
                        BackQuestionObject("a", "INGLES-B-0013", INGLES_B_0013);

                        String INGLES_B_0014 = BackSelectedCheckBox(BPregunta14Resp1, BPregunta14Resp2, BPregunta14Resp3, BPregunta14Resp4);
                        BackQuestionObject("d", "INGLES-B-0014", INGLES_B_0014);

                        String INGLES_B_0015 = BackSelectedCheckBox(BPregunta15Resp1, BPregunta15Resp2, BPregunta15Resp3, BPregunta15Resp4);
                        BackQuestionObject("d", "INGLES-B-0015", INGLES_B_0015);

                        String INGLES_B_0016 = BackSelectedCheckBox(BPregunta16Resp1, BPregunta16Resp2, BPregunta16Resp3, BPregunta16Resp4);
                        BackQuestionObject("d", "INGLES-B-0016", INGLES_B_0016);

                        String INGLES_B_0017 = BackSelectedCheckBox(BPregunta17Resp1, BPregunta17Resp2, BPregunta17Resp3, BPregunta17Resp4);
                        BackQuestionObject("c", "INGLES-B-0017", INGLES_B_0017);

                        String INGLES_B_0018 = BackSelectedCheckBox(BPregunta18Resp1, BPregunta18Resp2, BPregunta18Resp3, BPregunta18Resp4);
                        BackQuestionObject("d", "INGLES-B-0018", INGLES_B_0018);

                        String INGLES_B_0019 = BackSelectedCheckBox(BPregunta19Resp1, BPregunta19Resp2, BPregunta19Resp3, BPregunta19Resp4);
                        BackQuestionObject("c", "INGLES-B-0019", INGLES_B_0019);

                        String INGLES_B_0020 = BackSelectedCheckBox(BPregunta20Resp1, BPregunta20Resp2, BPregunta20Resp3, BPregunta20Resp4);
                        BackQuestionObject("b", "INGLES-B-0020", INGLES_B_0020);

                        String INGLES_B_0021 = BackSelectedCheckBox(BPregunta21Resp1, BPregunta21Resp2, BPregunta21Resp3, BPregunta21Resp4);
                        BackQuestionObject("b", "INGLES-B-0021", INGLES_B_0021);

                        String INGLES_B_0022 = BackSelectedCheckBox(BPregunta22Resp1, BPregunta22Resp2, BPregunta22Resp3, BPregunta22Resp4);
                        BackQuestionObject("d", "INGLES-B-0022", INGLES_B_0022);

                        String INGLES_B_0023 = BackSelectedCheckBox(BPregunta23Resp1, BPregunta23Resp2, BPregunta23Resp3, BPregunta23Resp4);
                        BackQuestionObject("b", "INGLES-B-0023", INGLES_B_0023);

                        String INGLES_B_0024 = BackSelectedCheckBox(BPregunta24Resp1, BPregunta24Resp2, BPregunta24Resp3, BPregunta24Resp4);
                        BackQuestionObject("c", "INGLES-B-0024", INGLES_B_0024);

                        String INGLES_B_0025 = BackSelectedCheckBox(BPregunta25Resp1, BPregunta25Resp2, BPregunta25Resp3, BPregunta25Resp4);
                        BackQuestionObject("b", "INGLES-B-0025", INGLES_B_0025);

                        String INGLES_B_0026 = BackSelectedCheckBox(BPregunta26Resp1, BPregunta26Resp2, BPregunta26Resp3, BPregunta26Resp4);
                        BackQuestionObject("a", "INGLES-B-0026", INGLES_B_0026);

                        String INGLES_B_0027 = BackSelectedCheckBox(BPregunta27Resp1, BPregunta27Resp2, BPregunta27Resp3, BPregunta27Resp4);
                        BackQuestionObject("b", "INGLES-B-0027", INGLES_B_0027);

                        String INGLES_B_0028 = BackSelectedCheckBox(BPregunta28Resp1, BPregunta28Resp2, BPregunta28Resp3, BPregunta28Resp4);
                        BackQuestionObject("b", "INGLES-B-0028", INGLES_B_0028);

                        String INGLES_B_0029 = BackSelectedCheckBox(BPregunta29Resp1, BPregunta29Resp2, BPregunta29Resp3, BPregunta29Resp4);
                        BackQuestionObject("b", "INGLES-B-0029", INGLES_B_0029);

                        String INGLES_B_0030 = BackSelectedCheckBox(BPregunta30Resp1, BPregunta30Resp2, BPregunta30Resp3, BPregunta30Resp4);
                        BackQuestionObject("a", "INGLES-B-0030", INGLES_B_0030);
                        break;

                    case 2:
                        //////////////////////////////////////////////////SECCION C/////////////////////////////////////////////////////////////////////
                        String INGLES_C_0001 = BackSelectedCheckBox(CPregunta1Resp1, CPregunta1Resp2, CPregunta1Resp3, CPregunta1Resp4);
                        BackQuestionObject("b", "INGLES-C-0001", INGLES_C_0001);

                        String INGLES_C_0002 = BackSelectedCheckBox(CPregunta2Resp1, CPregunta2Resp2, CPregunta2Resp3, CPregunta2Resp4);
                        BackQuestionObject("b", "INGLES-C-0002", INGLES_C_0002);

                        String INGLES_C_0003 = BackSelectedCheckBox(CPregunta3Resp1, CPregunta3Resp2, CPregunta3Resp3, CPregunta3Resp4);
                        BackQuestionObject("c", "INGLES-C-0003", INGLES_C_0003);

                        String INGLES_C_0004 = BackSelectedCheckBox(CPregunta4Resp1, CPregunta4Resp2, CPregunta4Resp3, CPregunta4Resp4);
                        BackQuestionObject("c", "INGLES-C-0004", INGLES_C_0004);

                        String INGLES_C_0005 = BackSelectedCheckBox(CPregunta5Resp1, CPregunta5Resp2, CPregunta5Resp3, CPregunta5Resp4);
                        BackQuestionObject("b", "INGLES-C-0005", INGLES_C_0005);

                        String INGLES_C_0006 = BackSelectedCheckBox(CPregunta6Resp1, CPregunta6Resp2, CPregunta6Resp3, CPregunta6Resp4);
                        BackQuestionObject("d", "INGLES-C-0006", INGLES_C_0006);

                        String INGLES_C_0007 = BackSelectedCheckBox(CPregunta7Resp1, CPregunta7Resp2, CPregunta7Resp3, CPregunta7Resp4);
                        BackQuestionObject("c", "INGLES-C-0007", INGLES_C_0007);

                        String INGLES_C_0008 = BackSelectedCheckBox(CPregunta8Resp1, CPregunta8Resp2, CPregunta8Resp3, CPregunta8Resp4);
                        BackQuestionObject("a", "INGLES-C-0008", INGLES_C_0008);

                        String INGLES_C_0009 = BackSelectedCheckBox(CPregunta9Resp1, CPregunta9Resp2, CPregunta9Resp3, CPregunta9Resp4);
                        BackQuestionObject("d", "INGLES-C-0009", INGLES_C_0009);

                        String INGLES_C_0010 = BackSelectedCheckBox(CPregunta10Resp1, CPregunta10Resp2, CPregunta10Resp3, CPregunta10Resp4);
                        BackQuestionObject("a", "INGLES-C-0010", INGLES_C_0010);

                        String INGLES_C_0011 = BackSelectedCheckBox(CPregunta11Resp1, CPregunta11Resp2, CPregunta11Resp3, CPregunta11Resp4);
                        BackQuestionObject("c", "INGLES-C-0011", INGLES_C_0011);

                        String INGLES_C_0012 = BackSelectedCheckBox(CPregunta12Resp1, CPregunta12Resp2, CPregunta12Resp3, CPregunta12Resp4);
                        BackQuestionObject("d", "INGLES-C-0012", INGLES_C_0012);

                        String INGLES_C_0013 = BackSelectedCheckBox(CPregunta13Resp1, CPregunta13Resp2, CPregunta13Resp3, CPregunta13Resp4);
                        BackQuestionObject("c", "INGLES-C-0013", INGLES_C_0013);

                        String INGLES_C_0014 = BackSelectedCheckBox(CPregunta14Resp1, CPregunta14Resp2, CPregunta14Resp3, CPregunta14Resp4);
                        BackQuestionObject("c", "INGLES-C-0014", INGLES_C_0014);

                        String INGLES_C_0015 = BackSelectedCheckBox(CPregunta15Resp1, CPregunta15Resp2, CPregunta15Resp3, CPregunta15Resp4);
                        BackQuestionObject("b", "INGLES-C-0015", INGLES_C_0015);

                        String INGLES_C_0016 = BackSelectedCheckBox(CPregunta16Resp1, CPregunta16Resp2, CPregunta16Resp3, CPregunta16Resp4);
                        BackQuestionObject("c", "INGLES-C-0016", INGLES_C_0016);

                        String INGLES_C_0017 = BackSelectedCheckBox(CPregunta17Resp1, CPregunta17Resp2, CPregunta17Resp3, CPregunta17Resp4);
                        BackQuestionObject("c", "INGLES-C-0017", INGLES_C_0017);

                        String INGLES_C_0018 = BackSelectedCheckBox(CPregunta18Resp1, CPregunta18Resp2, CPregunta18Resp3, CPregunta18Resp4);
                        BackQuestionObject("a", "INGLES-C-0018", INGLES_C_0018);

                        String INGLES_C_0019 = BackSelectedCheckBox(CPregunta19Resp1, CPregunta19Resp2, CPregunta19Resp3, CPregunta19Resp4);
                        BackQuestionObject("c", "INGLES-C-0019", INGLES_C_0019);

                        String INGLES_C_0020 = BackSelectedCheckBox(CPregunta20Resp1, CPregunta20Resp2, CPregunta20Resp3, CPregunta20Resp4);
                        BackQuestionObject("b", "INGLES-C-0020", INGLES_C_0020);

                        String INGLES_C_0021 = BackSelectedCheckBox(CPregunta21Resp1, CPregunta21Resp2, CPregunta21Resp3, CPregunta21Resp4);
                        BackQuestionObject("c", "INGLES-C-0021", INGLES_C_0021);

                        String INGLES_C_0022 = BackSelectedCheckBox(CPregunta22Resp1, CPregunta22Resp2, CPregunta22Resp3, CPregunta22Resp4);
                        BackQuestionObject("c", "INGLES-C-0022", INGLES_C_0022);

                        String INGLES_C_0023 = BackSelectedCheckBox(CPregunta23Resp1, CPregunta23Resp2, CPregunta23Resp3, CPregunta23Resp4);
                        BackQuestionObject("d", "INGLES-C-0023", INGLES_C_0023);

                        String INGLES_C_0024 = BackSelectedCheckBox(CPregunta24Resp1, CPregunta24Resp2, CPregunta24Resp3, CPregunta24Resp4);
                        BackQuestionObject("d", "INGLES-C-0024", INGLES_C_0024);

                        String INGLES_C_0025 = BackSelectedCheckBox(CPregunta25Resp1, CPregunta25Resp2, CPregunta25Resp3, CPregunta25Resp4);
                        BackQuestionObject("c", "INGLES-C-0025", INGLES_C_0025);

                        String INGLES_C_0026 = BackSelectedCheckBox(CPregunta26Resp1, CPregunta26Resp2, CPregunta26Resp3, CPregunta26Resp4);
                        BackQuestionObject("d", "INGLES-C-0026", INGLES_C_0026);

                        String INGLES_C_0027 = BackSelectedCheckBox(CPregunta27Resp1, CPregunta27Resp2, CPregunta27Resp3, CPregunta27Resp4);
                        BackQuestionObject("d", "INGLES-C-0027", INGLES_C_0027);

                        String INGLES_C_0028 = BackSelectedCheckBox(CPregunta28Resp1, CPregunta28Resp2, CPregunta28Resp3, CPregunta28Resp4);
                        BackQuestionObject("c", "INGLES-C-0028", INGLES_C_0028);

                        String INGLES_C_0029 = BackSelectedCheckBox(CPregunta29Resp1, CPregunta29Resp2, CPregunta29Resp3, CPregunta29Resp4);
                        BackQuestionObject("b", "INGLES-C-0029", INGLES_C_0029);

                        String INGLES_C_0030 = BackSelectedCheckBox(CPregunta30Resp1, CPregunta30Resp2, CPregunta30Resp3, CPregunta30Resp4);
                        BackQuestionObject("d", "INGLES-C-0030", INGLES_C_0030);

                        break;

                    case 3:

                        //////////////////////////////////////////////////SECCION D/////////////////////////////////////////////////////////////////////
                        String INGLES_D_0001 = BackSelectedCheckBox(DPregunta1Resp1, DPregunta1Resp2, DPregunta1Resp3, DPregunta1Resp4);
                        BackQuestionObject("a", "INGLES-D-0001", INGLES_D_0001);

                        String INGLES_D_0002 = BackSelectedCheckBox(DPregunta2Resp1, DPregunta2Resp2, DPregunta2Resp3, DPregunta2Resp4);
                        BackQuestionObject("d", "INGLES-D-0002", INGLES_D_0002);

                        String INGLES_D_0003 = BackSelectedCheckBox(DPregunta3Resp1, DPregunta3Resp2, DPregunta3Resp3, DPregunta3Resp4);
                        BackQuestionObject("c", "INGLES-D-0003", INGLES_D_0003);

                        String INGLES_D_0004 = BackSelectedCheckBox(DPregunta4Resp1, DPregunta4Resp2, DPregunta4Resp3, DPregunta4Resp4);
                        BackQuestionObject("b", "INGLES-D-0004", INGLES_D_0004);

                        String INGLES_D_0005 = BackSelectedCheckBox(DPregunta5Resp1, DPregunta5Resp2, DPregunta5Resp3, DPregunta5Resp4);
                        BackQuestionObject("b", "INGLES-D-0005", INGLES_D_0005);

                        String INGLES_D_0006 = BackSelectedCheckBox(DPregunta6Resp1, DPregunta6Resp2, DPregunta6Resp3, DPregunta6Resp4);
                        BackQuestionObject("b", "INGLES-D-0006", INGLES_D_0006);

                        String INGLES_D_0007 = BackSelectedCheckBox(DPregunta7Resp1, DPregunta7Resp2, DPregunta7Resp3, DPregunta7Resp4);
                        BackQuestionObject("b", "INGLES-D-0007", INGLES_D_0007);

                        String INGLES_D_0008 = BackSelectedCheckBox(DPregunta8Resp1, DPregunta8Resp2, DPregunta8Resp3, DPregunta8Resp4);
                        BackQuestionObject("a", "INGLES-D-0008", INGLES_D_0008);

                        String INGLES_D_0009 = BackSelectedCheckBox(DPregunta9Resp1, DPregunta9Resp2, DPregunta9Resp3, DPregunta9Resp4);
                        BackQuestionObject("c", "INGLES-D-0009", INGLES_D_0009);

                        String INGLES_D_0010 = BackSelectedCheckBox(DPregunta10Resp1, DPregunta10Resp2, DPregunta10Resp3, DPregunta10Resp4);
                        BackQuestionObject("a", "INGLES-D-0010", INGLES_D_0010);

                        String INGLES_D_0011 = BackSelectedCheckBox(DPregunta11Resp1, DPregunta11Resp2, DPregunta11Resp3, DPregunta11Resp4);
                        BackQuestionObject("a", "INGLES-D-0011", INGLES_D_0011);

                        String INGLES_D_0012 = BackSelectedCheckBox(DPregunta12Resp1, DPregunta12Resp2, DPregunta12Resp3, DPregunta12Resp4);
                        BackQuestionObject("b", "INGLES-D-0012", INGLES_D_0012);

                        String INGLES_D_0013 = BackSelectedCheckBox(DPregunta13Resp1, DPregunta13Resp2, DPregunta13Resp3, DPregunta13Resp4);
                        BackQuestionObject("c", "INGLES-D-0013", INGLES_D_0013);

                        String INGLES_D_0014 = BackSelectedCheckBox(DPregunta14Resp1, DPregunta14Resp2, DPregunta14Resp3, DPregunta14Resp4);
                        BackQuestionObject("a", "INGLES-D-0014", INGLES_D_0014);

                        String INGLES_D_0015 = BackSelectedCheckBox(DPregunta15Resp1, DPregunta15Resp2, DPregunta15Resp3, DPregunta15Resp4);
                        BackQuestionObject("b", "INGLES-D-0015", INGLES_D_0015);

                        String INGLES_D_0016 = BackSelectedCheckBox(DPregunta16Resp1, DPregunta16Resp2, DPregunta16Resp3, DPregunta16Resp4);
                        BackQuestionObject("d", "INGLES-D-0016", INGLES_D_0016);

                        String INGLES_D_0017 = BackSelectedCheckBox(DPregunta17Resp1, DPregunta17Resp2, DPregunta17Resp3, DPregunta17Resp4);
                        BackQuestionObject("b", "INGLES-D-0017", INGLES_D_0017);

                        String INGLES_D_0018 = BackSelectedCheckBox(DPregunta18Resp1, DPregunta18Resp2, DPregunta18Resp3, DPregunta18Resp4);
                        BackQuestionObject("c", "INGLES-D-0018", INGLES_D_0018);

                        String INGLES_D_0019 = BackSelectedCheckBox(DPregunta19Resp1, DPregunta19Resp2, DPregunta19Resp3, DPregunta19Resp4);
                        BackQuestionObject("c", "INGLES-D-0019", INGLES_D_0019);

                        String INGLES_D_0020 = BackSelectedCheckBox(DPregunta20Resp1, DPregunta20Resp2, DPregunta20Resp3, DPregunta20Resp4);
                        BackQuestionObject("c", "INGLES-D-0020", INGLES_D_0020);

                        String INGLES_D_0021 = BackSelectedCheckBox(DPregunta21Resp1, DPregunta21Resp2, DPregunta21Resp3, DPregunta21Resp4);
                        BackQuestionObject("c", "INGLES-D-0021", INGLES_D_0021);

                        String INGLES_D_0022 = BackSelectedCheckBox(DPregunta22Resp1, DPregunta22Resp2, DPregunta22Resp3, DPregunta22Resp4);
                        BackQuestionObject("b", "INGLES-D-0022", INGLES_D_0022);

                        String INGLES_D_0023 = BackSelectedCheckBox(DPregunta23Resp1, DPregunta23Resp2, DPregunta23Resp3, DPregunta23Resp4);
                        BackQuestionObject("d", "INGLES-D-0023", INGLES_D_0023);

                        String INGLES_D_0024 = BackSelectedCheckBox(DPregunta24Resp1, DPregunta24Resp2, DPregunta24Resp3, DPregunta24Resp4);
                        BackQuestionObject("c", "INGLES-D-0024", INGLES_D_0024);

                        String INGLES_D_0025 = BackSelectedCheckBox(DPregunta25Resp1, DPregunta25Resp2, DPregunta25Resp3, DPregunta25Resp4);
                        BackQuestionObject("a", "INGLES-D-0025", INGLES_D_0025);

                        String INGLES_D_0026 = BackSelectedCheckBox(DPregunta26Resp1, DPregunta26Resp2, DPregunta26Resp3, DPregunta26Resp4);
                        BackQuestionObject("b", "INGLES-D-0026", INGLES_D_0026);

                        String INGLES_D_0027 = BackSelectedCheckBox(DPregunta27Resp1, DPregunta27Resp2, DPregunta27Resp3, DPregunta27Resp4);
                        BackQuestionObject("d", "INGLES-D-0027", INGLES_D_0027);

                        String INGLES_D_0028 = BackSelectedCheckBox(DPregunta28Resp1, DPregunta28Resp2, DPregunta28Resp3, DPregunta28Resp4);
                        BackQuestionObject("a", "INGLES-D-0028", INGLES_D_0028);

                        String INGLES_D_0029 = BackSelectedCheckBox(DPregunta29Resp1, DPregunta29Resp2, DPregunta29Resp3, DPregunta29Resp4);
                        BackQuestionObject("c", "INGLES-D-0029", INGLES_D_0029);

                        String INGLES_D_0030 = BackSelectedCheckBox(DPregunta30Resp1, DPregunta30Resp2, DPregunta30Resp3, DPregunta30Resp4);
                        BackQuestionObject("c", "INGLES-D-0030", INGLES_D_0030);
                        break;
                }

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
                List<E_RESULTADOS_PRUEBA> Respuestasseccion = new List<E_RESULTADOS_PRUEBA>();

                var vObjetoPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba).FirstOrDefault();
                if (vObjetoPrueba != null)
                {
                    String CallBackFunction = "";
                    var vSeccionInicia = new E_PRUEBA_TIEMPO();
                    if (vseccion != (vSeccionesPrueba.Count - 1))
                    {
                        var vRespuestasCorrectas = vRespuestas.Where(item => item.NO_VALOR_RESPUESTA == 1).ToList();
                        if (vRespuestasCorrectas.Count >= 24)
                        {
                            CallBackFunction = "updateTimer('" + (vseccion + 1) + "')";
                        }
                        else
                        {
                            CallBackFunction = "updateTimer('" + (-1) + "')";
                        }
                        vSeccionInicia = vSeccionesPrueba.ElementAt(vseccion + 1);
                        vSeccionInicia.FE_INICIO = DateTime.Now;
                        vSeccionInicia.CL_ESTADO = E_ESTADO_PRUEBA.INICIADA.ToString();
                    }
                    else
                    {
                        SPE_OBTIENE_K_PRUEBA_Result vPruebaTerminada = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba).FirstOrDefault();
                        vPruebaTerminada.FE_TERMINO = DateTime.Now;
                        vPruebaTerminada.CL_ESTADO = E_ESTADO_PRUEBA.TERMINADA.ToString();
                        E_RESULTADO vResultadoTestEnd = nKprueba.InsertaActualiza_K_PRUEBA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
                        vSeccionInicia = vSeccionesPrueba.ElementAt(vseccion);
                        vSeccionInicia.FE_INICIO = DateTime.Now;
                        vSeccionInicia.CL_ESTADO = E_ESTADO_PRUEBA.INICIADA.ToString();
                        CallBackFunction = "CloseTest";
                    }
                   
                    if (Request.QueryString["MOD"] != null)
                    {
                        

                        E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vObjetoPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: "INGLES-" + (vseccion + 1), usuario: vClUsuario, programa: vNbPrograma);
                        string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                        UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
                        initRespuestasIngles();
                    }
                    else 
                    {

                        E_RESULTADO vResultadoSeccion = nKprueba.InsertaActualiza_K_PRUEBA_SECCION(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), v_k_prueba: vSeccionInicia, usuario: vClUsuario, programa: vNbPrograma);
                        E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vObjetoPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: "INGLES-" + (vseccion + 1), usuario: vClUsuario, programa: vNbPrograma);
                        string vMensaje = instrucciones(vseccion + 1);
                        int vHeight = HeightRadAlert(vseccion + 1);
                        UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, vHeight, CallBackFunction);
                       }
                }
            }
        }

        public void EditTest(int vseccion)
        {
            CuestionariosNegocio nPreguntas = new CuestionariosNegocio();
            var preguntas = nPreguntas.Obtener_K_PREGUNTA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken);
            var filtroPreguntas = preguntas.Where(oh => oh.CL_PREGUNTA.StartsWith("INGLES-" + BackLetterQuestions(vseccion))).ToList();
            vRespuestas = new List<E_PREGUNTA>();
            if (filtroPreguntas.Count > 0)
            {
                foreach (SPE_OBTIENE_K_PREGUNTA_Result pregunta in filtroPreguntas)
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

                switch (vseccion)
                {
                    case 0:
                        String INGLES_A_0001 = BackSelectedCheckBox(APregunta1Resp1, APregunta1Resp2, APregunta1Resp3, APregunta1Resp4);
                        BackQuestionObject("b", "INGLES-A-0001", INGLES_A_0001);

                        String INGLES_A_0002 = BackSelectedCheckBox(APregunta2Resp1, APregunta2Resp2, APregunta2Resp3, APregunta2Resp4);
                        BackQuestionObject("c", "INGLES-A-0002", INGLES_A_0002);

                        String INGLES_A_0003 = BackSelectedCheckBox(APregunta3Resp1, APregunta3Resp2, APregunta3Resp3, APregunta3Resp4);
                        BackQuestionObject("c", "INGLES-A-0003", INGLES_A_0003);

                        String INGLES_A_0004 = BackSelectedCheckBox(APregunta4Resp1, APregunta4Resp2, APregunta4Resp3, APregunta4Resp4);
                        BackQuestionObject("c", "INGLES-A-0004", INGLES_A_0004);

                        String INGLES_A_0005 = BackSelectedCheckBox(APregunta5Resp1, APregunta5Resp2, APregunta5Resp3, APregunta5Resp4);
                        BackQuestionObject("c", "INGLES-A-0005", INGLES_A_0005);

                        String INGLES_A_0006 = BackSelectedCheckBox(APregunta6Resp1, APregunta6Resp2, APregunta6Resp3, APregunta6Resp4);
                        BackQuestionObject("c", "INGLES-A-0006", INGLES_A_0006);

                        String INGLES_A_0007 = BackSelectedCheckBox(APregunta7Resp1, APregunta7Resp2, APregunta7Resp3, APregunta7Resp4);
                        BackQuestionObject("b", "INGLES-A-0007", INGLES_A_0007);

                        String INGLES_A_0008 = BackSelectedCheckBox(APregunta8Resp1, APregunta8Resp2, APregunta8Resp3, APregunta8Resp4);
                        BackQuestionObject("b", "INGLES-A-0008", INGLES_A_0008);

                        String INGLES_A_0009 = BackSelectedCheckBox(APregunta9Resp1, APregunta9Resp2, APregunta9Resp3, APregunta9Resp4);
                        BackQuestionObject("d", "INGLES-A-0009", INGLES_A_0009);

                        String INGLES_A_0010 = BackSelectedCheckBox(APregunta10Resp1, APregunta10Resp2, APregunta10Resp3, APregunta10Resp4);
                        BackQuestionObject("c", "INGLES-A-0010", INGLES_A_0010);

                        String INGLES_A_0011 = BackSelectedCheckBox(APregunta11Resp1, APregunta11Resp2, APregunta11Resp3, APregunta11Resp4);
                        BackQuestionObject("c", "INGLES-A-0011", INGLES_A_0011);

                        String INGLES_A_0012 = BackSelectedCheckBox(APregunta12Resp1, APregunta12Resp2, APregunta12Resp3, APregunta12Resp4);
                        BackQuestionObject("d", "INGLES-A-0012", INGLES_A_0012);

                        String INGLES_A_0013 = BackSelectedCheckBox(APregunta13Resp1, APregunta13Resp2, APregunta13Resp3, APregunta13Resp4);
                        BackQuestionObject("c", "INGLES-A-0013", INGLES_A_0013);

                        String INGLES_A_0014 = BackSelectedCheckBox(APregunta14Resp1, APregunta14Resp2, APregunta14Resp3, APregunta14Resp4);
                        BackQuestionObject("a", "INGLES-A-0014", INGLES_A_0014);

                        String INGLES_A_0015 = BackSelectedCheckBox(APregunta15Resp1, APregunta15Resp2, APregunta15Resp3, APregunta15Resp4);
                        BackQuestionObject("a", "INGLES-A-0015", INGLES_A_0015);

                        String INGLES_A_0016 = BackSelectedCheckBox(APregunta16Resp1, APregunta16Resp2, APregunta16Resp3, APregunta16Resp4);
                        BackQuestionObject("b", "INGLES-A-0016", INGLES_A_0016);

                        String INGLES_A_0017 = BackSelectedCheckBox(APregunta17Resp1, APregunta17Resp2, APregunta17Resp3, APregunta17Resp4);
                        BackQuestionObject("c", "INGLES-A-0017", INGLES_A_0017);

                        String INGLES_A_0018 = BackSelectedCheckBox(APregunta18Resp1, APregunta18Resp2, APregunta18Resp3, APregunta18Resp4);
                        BackQuestionObject("b", "INGLES-A-0018", INGLES_A_0018);

                        String INGLES_A_0019 = BackSelectedCheckBox(APregunta19Resp1, APregunta19Resp2, APregunta19Resp3, APregunta19Resp4);
                        BackQuestionObject("d", "INGLES-A-0019", INGLES_A_0019);

                        String INGLES_A_0020 = BackSelectedCheckBox(APregunta20Resp1, APregunta20Resp2, APregunta20Resp3, APregunta20Resp4);
                        BackQuestionObject("c", "INGLES-A-0020", INGLES_A_0020);

                        String INGLES_A_0021 = BackSelectedCheckBox(APregunta21Resp1, APregunta21Resp2, APregunta21Resp3, APregunta21Resp4);
                        BackQuestionObject("c", "INGLES-A-0021", INGLES_A_0021);

                        String INGLES_A_0022 = BackSelectedCheckBox(APregunta22Resp1, APregunta22Resp2, APregunta22Resp3, APregunta22Resp4);
                        BackQuestionObject("c", "INGLES-A-0022", INGLES_A_0022);

                        String INGLES_A_0023 = BackSelectedCheckBox(APregunta23Resp1, APregunta23Resp2, APregunta23Resp3, APregunta23Resp4);
                        BackQuestionObject("d", "INGLES-A-0023", INGLES_A_0023);

                        String INGLES_A_0024 = BackSelectedCheckBox(APregunta24Resp1, APregunta24Resp2, APregunta24Resp3, APregunta24Resp4);
                        BackQuestionObject("d", "INGLES-A-0024", INGLES_A_0024);

                        String INGLES_A_0025 = BackSelectedCheckBox(APregunta25Resp1, APregunta25Resp2, APregunta25Resp3, APregunta25Resp4);
                        BackQuestionObject("a", "INGLES-A-0025", INGLES_A_0025);

                        String INGLES_A_0026 = BackSelectedCheckBox(APregunta26Resp1, APregunta26Resp2, APregunta26Resp3, APregunta26Resp4);
                        BackQuestionObject("c", "INGLES-A-0026", INGLES_A_0026);

                        String INGLES_A_0027 = BackSelectedCheckBox(APregunta27Resp1, APregunta27Resp2, APregunta27Resp3, APregunta27Resp4);
                        BackQuestionObject("c", "INGLES-A-0027", INGLES_A_0027);

                        String INGLES_A_0028 = BackSelectedCheckBox(APregunta28Resp1, APregunta28Resp2, APregunta28Resp3, APregunta28Resp4);
                        BackQuestionObject("b", "INGLES-A-0028", INGLES_A_0028);

                        String INGLES_A_0029 = BackSelectedCheckBox(APregunta29Resp1, APregunta29Resp2, APregunta29Resp3, APregunta29Resp4);
                        BackQuestionObject("c", "INGLES-A-0029", INGLES_A_0029);

                        String INGLES_A_0030 = BackSelectedCheckBox(APregunta30Resp1, APregunta30Resp2, APregunta30Resp3, APregunta30Resp4);
                        BackQuestionObject("d", "INGLES-A-0030", INGLES_A_0030);
                        break;

                    case 1:
                        //////////////////////////////////////////////////SECCION B/////////////////////////////////////////////////////////////////////

                        String INGLES_B_0001 = BackSelectedCheckBox(BPregunta1Resp1, BPregunta1Resp2, BPregunta1Resp3, BPregunta1Resp4);
                        BackQuestionObject("c", "INGLES-B-0001", INGLES_B_0001);

                        String INGLES_B_0002 = BackSelectedCheckBox(BPregunta2Resp1, BPregunta2Resp2, BPregunta2Resp3, BPregunta2Resp4);
                        BackQuestionObject("d", "INGLES-B-0002", INGLES_B_0002);

                        String INGLES_B_0003 = BackSelectedCheckBox(BPregunta3Resp1, BPregunta3Resp2, BPregunta3Resp3, BPregunta3Resp4);
                        BackQuestionObject("b", "INGLES-B-0003", INGLES_B_0003);

                        String INGLES_B_0004 = BackSelectedCheckBox(BPregunta4Resp1, BPregunta4Resp2, BPregunta4Resp3, BPregunta4Resp4);
                        BackQuestionObject("d", "INGLES-B-0004", INGLES_B_0004);

                        String INGLES_B_0005 = BackSelectedCheckBox(BPregunta5Resp1, BPregunta5Resp2, BPregunta5Resp3, BPregunta5Resp4);
                        BackQuestionObject("c", "INGLES-B-0005", INGLES_B_0005);

                        String INGLES_B_0006 = BackSelectedCheckBox(BPregunta6Resp1, BPregunta6Resp2, BPregunta6Resp3, BPregunta6Resp4);
                        BackQuestionObject("c", "INGLES-B-0006", INGLES_B_0006);

                        String INGLES_B_0007 = BackSelectedCheckBox(BPregunta7Resp1, BPregunta7Resp2, BPregunta7Resp3, BPregunta7Resp4);
                        BackQuestionObject("b", "INGLES-B-0007", INGLES_B_0007);

                        String INGLES_B_0008 = BackSelectedCheckBox(BPregunta8Resp1, BPregunta8Resp2, BPregunta8Resp3, BPregunta8Resp4);
                        BackQuestionObject("c", "INGLES-B-0008", INGLES_B_0008);

                        String INGLES_B_0009 = BackSelectedCheckBox(BPregunta9Resp1, BPregunta9Resp2, BPregunta9Resp3, BPregunta9Resp4);
                        BackQuestionObject("b", "INGLES-B-0009", INGLES_B_0009);

                        String INGLES_B_0010 = BackSelectedCheckBox(BPregunta10Resp1, BPregunta10Resp2, BPregunta10Resp3, BPregunta10Resp4);
                        BackQuestionObject("c", "INGLES-B-0010", INGLES_B_0010);

                        String INGLES_B_0011 = BackSelectedCheckBox(BPregunta11Resp1, BPregunta11Resp2, BPregunta11Resp3, BPregunta11Resp4);
                        BackQuestionObject("c", "INGLES-B-0011", INGLES_B_0011);

                        String INGLES_B_0012 = BackSelectedCheckBox(BPregunta12Resp1, BPregunta12Resp2, BPregunta12Resp3, BPregunta12Resp4);
                        BackQuestionObject("a", "INGLES-B-0012", INGLES_B_0012);

                        String INGLES_B_0013 = BackSelectedCheckBox(BPregunta13Resp1, BPregunta13Resp2, BPregunta13Resp3, BPregunta13Resp4);
                        BackQuestionObject("a", "INGLES-B-0013", INGLES_B_0013);

                        String INGLES_B_0014 = BackSelectedCheckBox(BPregunta14Resp1, BPregunta14Resp2, BPregunta14Resp3, BPregunta14Resp4);
                        BackQuestionObject("d", "INGLES-B-0014", INGLES_B_0014);

                        String INGLES_B_0015 = BackSelectedCheckBox(BPregunta15Resp1, BPregunta15Resp2, BPregunta15Resp3, BPregunta15Resp4);
                        BackQuestionObject("d", "INGLES-B-0015", INGLES_B_0015);

                        String INGLES_B_0016 = BackSelectedCheckBox(BPregunta16Resp1, BPregunta16Resp2, BPregunta16Resp3, BPregunta16Resp4);
                        BackQuestionObject("d", "INGLES-B-0016", INGLES_B_0016);

                        String INGLES_B_0017 = BackSelectedCheckBox(BPregunta17Resp1, BPregunta17Resp2, BPregunta17Resp3, BPregunta17Resp4);
                        BackQuestionObject("c", "INGLES-B-0017", INGLES_B_0017);

                        String INGLES_B_0018 = BackSelectedCheckBox(BPregunta18Resp1, BPregunta18Resp2, BPregunta18Resp3, BPregunta18Resp4);
                        BackQuestionObject("d", "INGLES-B-0018", INGLES_B_0018);

                        String INGLES_B_0019 = BackSelectedCheckBox(BPregunta19Resp1, BPregunta19Resp2, BPregunta19Resp3, BPregunta19Resp4);
                        BackQuestionObject("c", "INGLES-B-0019", INGLES_B_0019);

                        String INGLES_B_0020 = BackSelectedCheckBox(BPregunta20Resp1, BPregunta20Resp2, BPregunta20Resp3, BPregunta20Resp4);
                        BackQuestionObject("b", "INGLES-B-0020", INGLES_B_0020);

                        String INGLES_B_0021 = BackSelectedCheckBox(BPregunta21Resp1, BPregunta21Resp2, BPregunta21Resp3, BPregunta21Resp4);
                        BackQuestionObject("b", "INGLES-B-0021", INGLES_B_0021);

                        String INGLES_B_0022 = BackSelectedCheckBox(BPregunta22Resp1, BPregunta22Resp2, BPregunta22Resp3, BPregunta22Resp4);
                        BackQuestionObject("d", "INGLES-B-0022", INGLES_B_0022);

                        String INGLES_B_0023 = BackSelectedCheckBox(BPregunta23Resp1, BPregunta23Resp2, BPregunta23Resp3, BPregunta23Resp4);
                        BackQuestionObject("b", "INGLES-B-0023", INGLES_B_0023);

                        String INGLES_B_0024 = BackSelectedCheckBox(BPregunta24Resp1, BPregunta24Resp2, BPregunta24Resp3, BPregunta24Resp4);
                        BackQuestionObject("c", "INGLES-B-0024", INGLES_B_0024);

                        String INGLES_B_0025 = BackSelectedCheckBox(BPregunta25Resp1, BPregunta25Resp2, BPregunta25Resp3, BPregunta25Resp4);
                        BackQuestionObject("b", "INGLES-B-0025", INGLES_B_0025);

                        String INGLES_B_0026 = BackSelectedCheckBox(BPregunta26Resp1, BPregunta26Resp2, BPregunta26Resp3, BPregunta26Resp4);
                        BackQuestionObject("a", "INGLES-B-0026", INGLES_B_0026);

                        String INGLES_B_0027 = BackSelectedCheckBox(BPregunta27Resp1, BPregunta27Resp2, BPregunta27Resp3, BPregunta27Resp4);
                        BackQuestionObject("b", "INGLES-B-0027", INGLES_B_0027);

                        String INGLES_B_0028 = BackSelectedCheckBox(BPregunta28Resp1, BPregunta28Resp2, BPregunta28Resp3, BPregunta28Resp4);
                        BackQuestionObject("b", "INGLES-B-0028", INGLES_B_0028);

                        String INGLES_B_0029 = BackSelectedCheckBox(BPregunta29Resp1, BPregunta29Resp2, BPregunta29Resp3, BPregunta29Resp4);
                        BackQuestionObject("b", "INGLES-B-0029", INGLES_B_0029);

                        String INGLES_B_0030 = BackSelectedCheckBox(BPregunta30Resp1, BPregunta30Resp2, BPregunta30Resp3, BPregunta30Resp4);
                        BackQuestionObject("a", "INGLES-B-0030", INGLES_B_0030);
                        break;

                    case 2:
                        //////////////////////////////////////////////////SECCION C/////////////////////////////////////////////////////////////////////
                        String INGLES_C_0001 = BackSelectedCheckBox(CPregunta1Resp1, CPregunta1Resp2, CPregunta1Resp3, CPregunta1Resp4);
                        BackQuestionObject("b", "INGLES-C-0001", INGLES_C_0001);

                        String INGLES_C_0002 = BackSelectedCheckBox(CPregunta2Resp1, CPregunta2Resp2, CPregunta2Resp3, CPregunta2Resp4);
                        BackQuestionObject("b", "INGLES-C-0002", INGLES_C_0002);

                        String INGLES_C_0003 = BackSelectedCheckBox(CPregunta3Resp1, CPregunta3Resp2, CPregunta3Resp3, CPregunta3Resp4);
                        BackQuestionObject("c", "INGLES-C-0003", INGLES_C_0003);

                        String INGLES_C_0004 = BackSelectedCheckBox(CPregunta4Resp1, CPregunta4Resp2, CPregunta4Resp3, CPregunta4Resp4);
                        BackQuestionObject("c", "INGLES-C-0004", INGLES_C_0004);

                        String INGLES_C_0005 = BackSelectedCheckBox(CPregunta5Resp1, CPregunta5Resp2, CPregunta5Resp3, CPregunta5Resp4);
                        BackQuestionObject("b", "INGLES-C-0005", INGLES_C_0005);

                        String INGLES_C_0006 = BackSelectedCheckBox(CPregunta6Resp1, CPregunta6Resp2, CPregunta6Resp3, CPregunta6Resp4);
                        BackQuestionObject("d", "INGLES-C-0006", INGLES_C_0006);

                        String INGLES_C_0007 = BackSelectedCheckBox(CPregunta7Resp1, CPregunta7Resp2, CPregunta7Resp3, CPregunta7Resp4);
                        BackQuestionObject("c", "INGLES-C-0007", INGLES_C_0007);

                        String INGLES_C_0008 = BackSelectedCheckBox(CPregunta8Resp1, CPregunta8Resp2, CPregunta8Resp3, CPregunta8Resp4);
                        BackQuestionObject("a", "INGLES-C-0008", INGLES_C_0008);

                        String INGLES_C_0009 = BackSelectedCheckBox(CPregunta9Resp1, CPregunta9Resp2, CPregunta9Resp3, CPregunta9Resp4);
                        BackQuestionObject("d", "INGLES-C-0009", INGLES_C_0009);

                        String INGLES_C_0010 = BackSelectedCheckBox(CPregunta10Resp1, CPregunta10Resp2, CPregunta10Resp3, CPregunta10Resp4);
                        BackQuestionObject("a", "INGLES-C-0010", INGLES_C_0010);

                        String INGLES_C_0011 = BackSelectedCheckBox(CPregunta11Resp1, CPregunta11Resp2, CPregunta11Resp3, CPregunta11Resp4);
                        BackQuestionObject("c", "INGLES-C-0011", INGLES_C_0011);

                        String INGLES_C_0012 = BackSelectedCheckBox(CPregunta12Resp1, CPregunta12Resp2, CPregunta12Resp3, CPregunta12Resp4);
                        BackQuestionObject("d", "INGLES-C-0012", INGLES_C_0012);

                        String INGLES_C_0013 = BackSelectedCheckBox(CPregunta13Resp1, CPregunta13Resp2, CPregunta13Resp3, CPregunta13Resp4);
                        BackQuestionObject("c", "INGLES-C-0013", INGLES_C_0013);

                        String INGLES_C_0014 = BackSelectedCheckBox(CPregunta14Resp1, CPregunta14Resp2, CPregunta14Resp3, CPregunta14Resp4);
                        BackQuestionObject("c", "INGLES-C-0014", INGLES_C_0014);

                        String INGLES_C_0015 = BackSelectedCheckBox(CPregunta15Resp1, CPregunta15Resp2, CPregunta15Resp3, CPregunta15Resp4);
                        BackQuestionObject("b", "INGLES-C-0015", INGLES_C_0015);

                        String INGLES_C_0016 = BackSelectedCheckBox(CPregunta16Resp1, CPregunta16Resp2, CPregunta16Resp3, CPregunta16Resp4);
                        BackQuestionObject("c", "INGLES-C-0016", INGLES_C_0016);

                        String INGLES_C_0017 = BackSelectedCheckBox(CPregunta17Resp1, CPregunta17Resp2, CPregunta17Resp3, CPregunta17Resp4);
                        BackQuestionObject("c", "INGLES-C-0017", INGLES_C_0017);

                        String INGLES_C_0018 = BackSelectedCheckBox(CPregunta18Resp1, CPregunta18Resp2, CPregunta18Resp3, CPregunta18Resp4);
                        BackQuestionObject("a", "INGLES-C-0018", INGLES_C_0018);

                        String INGLES_C_0019 = BackSelectedCheckBox(CPregunta19Resp1, CPregunta19Resp2, CPregunta19Resp3, CPregunta19Resp4);
                        BackQuestionObject("c", "INGLES-C-0019", INGLES_C_0019);

                        String INGLES_C_0020 = BackSelectedCheckBox(CPregunta20Resp1, CPregunta20Resp2, CPregunta20Resp3, CPregunta20Resp4);
                        BackQuestionObject("b", "INGLES-C-0020", INGLES_C_0020);

                        String INGLES_C_0021 = BackSelectedCheckBox(CPregunta21Resp1, CPregunta21Resp2, CPregunta21Resp3, CPregunta21Resp4);
                        BackQuestionObject("c", "INGLES-C-0021", INGLES_C_0021);

                        String INGLES_C_0022 = BackSelectedCheckBox(CPregunta22Resp1, CPregunta22Resp2, CPregunta22Resp3, CPregunta22Resp4);
                        BackQuestionObject("c", "INGLES-C-0022", INGLES_C_0022);

                        String INGLES_C_0023 = BackSelectedCheckBox(CPregunta23Resp1, CPregunta23Resp2, CPregunta23Resp3, CPregunta23Resp4);
                        BackQuestionObject("d", "INGLES-C-0023", INGLES_C_0023);

                        String INGLES_C_0024 = BackSelectedCheckBox(CPregunta24Resp1, CPregunta24Resp2, CPregunta24Resp3, CPregunta24Resp4);
                        BackQuestionObject("d", "INGLES-C-0024", INGLES_C_0024);

                        String INGLES_C_0025 = BackSelectedCheckBox(CPregunta25Resp1, CPregunta25Resp2, CPregunta25Resp3, CPregunta25Resp4);
                        BackQuestionObject("c", "INGLES-C-0025", INGLES_C_0025);

                        String INGLES_C_0026 = BackSelectedCheckBox(CPregunta26Resp1, CPregunta26Resp2, CPregunta26Resp3, CPregunta26Resp4);
                        BackQuestionObject("d", "INGLES-C-0026", INGLES_C_0026);

                        String INGLES_C_0027 = BackSelectedCheckBox(CPregunta27Resp1, CPregunta27Resp2, CPregunta27Resp3, CPregunta27Resp4);
                        BackQuestionObject("d", "INGLES-C-0027", INGLES_C_0027);

                        String INGLES_C_0028 = BackSelectedCheckBox(CPregunta28Resp1, CPregunta28Resp2, CPregunta28Resp3, CPregunta28Resp4);
                        BackQuestionObject("c", "INGLES-C-0028", INGLES_C_0028);

                        String INGLES_C_0029 = BackSelectedCheckBox(CPregunta29Resp1, CPregunta29Resp2, CPregunta29Resp3, CPregunta29Resp4);
                        BackQuestionObject("b", "INGLES-C-0029", INGLES_C_0029);

                        String INGLES_C_0030 = BackSelectedCheckBox(CPregunta30Resp1, CPregunta30Resp2, CPregunta30Resp3, CPregunta30Resp4);
                        BackQuestionObject("d", "INGLES-C-0030", INGLES_C_0030);

                        break;

                    case 3:

                        //////////////////////////////////////////////////SECCION D/////////////////////////////////////////////////////////////////////
                        String INGLES_D_0001 = BackSelectedCheckBox(DPregunta1Resp1, DPregunta1Resp2, DPregunta1Resp3, DPregunta1Resp4);
                        BackQuestionObject("a", "INGLES-D-0001", INGLES_D_0001);

                        String INGLES_D_0002 = BackSelectedCheckBox(DPregunta2Resp1, DPregunta2Resp2, DPregunta2Resp3, DPregunta2Resp4);
                        BackQuestionObject("d", "INGLES-D-0002", INGLES_D_0002);

                        String INGLES_D_0003 = BackSelectedCheckBox(DPregunta3Resp1, DPregunta3Resp2, DPregunta3Resp3, DPregunta3Resp4);
                        BackQuestionObject("c", "INGLES-D-0003", INGLES_D_0003);

                        String INGLES_D_0004 = BackSelectedCheckBox(DPregunta4Resp1, DPregunta4Resp2, DPregunta4Resp3, DPregunta4Resp4);
                        BackQuestionObject("b", "INGLES-D-0004", INGLES_D_0004);

                        String INGLES_D_0005 = BackSelectedCheckBox(DPregunta5Resp1, DPregunta5Resp2, DPregunta5Resp3, DPregunta5Resp4);
                        BackQuestionObject("b", "INGLES-D-0005", INGLES_D_0005);

                        String INGLES_D_0006 = BackSelectedCheckBox(DPregunta6Resp1, DPregunta6Resp2, DPregunta6Resp3, DPregunta6Resp4);
                        BackQuestionObject("b", "INGLES-D-0006", INGLES_D_0006);

                        String INGLES_D_0007 = BackSelectedCheckBox(DPregunta7Resp1, DPregunta7Resp2, DPregunta7Resp3, DPregunta7Resp4);
                        BackQuestionObject("b", "INGLES-D-0007", INGLES_D_0007);

                        String INGLES_D_0008 = BackSelectedCheckBox(DPregunta8Resp1, DPregunta8Resp2, DPregunta8Resp3, DPregunta8Resp4);
                        BackQuestionObject("a", "INGLES-D-0008", INGLES_D_0008);

                        String INGLES_D_0009 = BackSelectedCheckBox(DPregunta9Resp1, DPregunta9Resp2, DPregunta9Resp3, DPregunta9Resp4);
                        BackQuestionObject("c", "INGLES-D-0009", INGLES_D_0009);

                        String INGLES_D_0010 = BackSelectedCheckBox(DPregunta10Resp1, DPregunta10Resp2, DPregunta10Resp3, DPregunta10Resp4);
                        BackQuestionObject("a", "INGLES-D-0010", INGLES_D_0010);

                        String INGLES_D_0011 = BackSelectedCheckBox(DPregunta11Resp1, DPregunta11Resp2, DPregunta11Resp3, DPregunta11Resp4);
                        BackQuestionObject("a", "INGLES-D-0011", INGLES_D_0011);

                        String INGLES_D_0012 = BackSelectedCheckBox(DPregunta12Resp1, DPregunta12Resp2, DPregunta12Resp3, DPregunta12Resp4);
                        BackQuestionObject("b", "INGLES-D-0012", INGLES_D_0012);

                        String INGLES_D_0013 = BackSelectedCheckBox(DPregunta13Resp1, DPregunta13Resp2, DPregunta13Resp3, DPregunta13Resp4);
                        BackQuestionObject("c", "INGLES-D-0013", INGLES_D_0013);

                        String INGLES_D_0014 = BackSelectedCheckBox(DPregunta14Resp1, DPregunta14Resp2, DPregunta14Resp3, DPregunta14Resp4);
                        BackQuestionObject("a", "INGLES-D-0014", INGLES_D_0014);

                        String INGLES_D_0015 = BackSelectedCheckBox(DPregunta15Resp1, DPregunta15Resp2, DPregunta15Resp3, DPregunta15Resp4);
                        BackQuestionObject("b", "INGLES-D-0015", INGLES_D_0015);

                        String INGLES_D_0016 = BackSelectedCheckBox(DPregunta16Resp1, DPregunta16Resp2, DPregunta16Resp3, DPregunta16Resp4);
                        BackQuestionObject("d", "INGLES-D-0016", INGLES_D_0016);

                        String INGLES_D_0017 = BackSelectedCheckBox(DPregunta17Resp1, DPregunta17Resp2, DPregunta17Resp3, DPregunta17Resp4);
                        BackQuestionObject("b", "INGLES-D-0017", INGLES_D_0017);

                        String INGLES_D_0018 = BackSelectedCheckBox(DPregunta18Resp1, DPregunta18Resp2, DPregunta18Resp3, DPregunta18Resp4);
                        BackQuestionObject("c", "INGLES-D-0018", INGLES_D_0018);

                        String INGLES_D_0019 = BackSelectedCheckBox(DPregunta19Resp1, DPregunta19Resp2, DPregunta19Resp3, DPregunta19Resp4);
                        BackQuestionObject("c", "INGLES-D-0019", INGLES_D_0019);

                        String INGLES_D_0020 = BackSelectedCheckBox(DPregunta20Resp1, DPregunta20Resp2, DPregunta20Resp3, DPregunta20Resp4);
                        BackQuestionObject("c", "INGLES-D-0020", INGLES_D_0020);

                        String INGLES_D_0021 = BackSelectedCheckBox(DPregunta21Resp1, DPregunta21Resp2, DPregunta21Resp3, DPregunta21Resp4);
                        BackQuestionObject("c", "INGLES-D-0021", INGLES_D_0021);

                        String INGLES_D_0022 = BackSelectedCheckBox(DPregunta22Resp1, DPregunta22Resp2, DPregunta22Resp3, DPregunta22Resp4);
                        BackQuestionObject("b", "INGLES-D-0022", INGLES_D_0022);

                        String INGLES_D_0023 = BackSelectedCheckBox(DPregunta23Resp1, DPregunta23Resp2, DPregunta23Resp3, DPregunta23Resp4);
                        BackQuestionObject("d", "INGLES-D-0023", INGLES_D_0023);

                        String INGLES_D_0024 = BackSelectedCheckBox(DPregunta24Resp1, DPregunta24Resp2, DPregunta24Resp3, DPregunta24Resp4);
                        BackQuestionObject("c", "INGLES-D-0024", INGLES_D_0024);

                        String INGLES_D_0025 = BackSelectedCheckBox(DPregunta25Resp1, DPregunta25Resp2, DPregunta25Resp3, DPregunta25Resp4);
                        BackQuestionObject("a", "INGLES-D-0025", INGLES_D_0025);

                        String INGLES_D_0026 = BackSelectedCheckBox(DPregunta26Resp1, DPregunta26Resp2, DPregunta26Resp3, DPregunta26Resp4);
                        BackQuestionObject("b", "INGLES-D-0026", INGLES_D_0026);

                        String INGLES_D_0027 = BackSelectedCheckBox(DPregunta27Resp1, DPregunta27Resp2, DPregunta27Resp3, DPregunta27Resp4);
                        BackQuestionObject("d", "INGLES-D-0027", INGLES_D_0027);

                        String INGLES_D_0028 = BackSelectedCheckBox(DPregunta28Resp1, DPregunta28Resp2, DPregunta28Resp3, DPregunta28Resp4);
                        BackQuestionObject("a", "INGLES-D-0028", INGLES_D_0028);

                        String INGLES_D_0029 = BackSelectedCheckBox(DPregunta29Resp1, DPregunta29Resp2, DPregunta29Resp3, DPregunta29Resp4);
                        BackQuestionObject("c", "INGLES-D-0029", INGLES_D_0029);

                        String INGLES_D_0030 = BackSelectedCheckBox(DPregunta30Resp1, DPregunta30Resp2, DPregunta30Resp3, DPregunta30Resp4);
                        BackQuestionObject("c", "INGLES-D-0030", INGLES_D_0030);
                        break;
                }

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
                List<E_RESULTADOS_PRUEBA> Respuestasseccion = new List<E_RESULTADOS_PRUEBA>();

                var vObjetoPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba).FirstOrDefault();
                if (vObjetoPrueba != null)
                {
                    String CallBackFunction = "";
                    var vSeccionInicia = new E_PRUEBA_TIEMPO();
                    if (vseccion != (vSeccionesPrueba.Count - 1))
                    {
                        var vRespuestasCorrectas = vRespuestas.Where(item => item.NO_VALOR_RESPUESTA == 1).ToList();
                        //if (vRespuestasCorrectas.Count >= 24)
                        //{
                        //    CallBackFunction = "updateTimer('" + (vseccion + 1) + "')";
                        //}
                        //else
                        //{
                        //    CallBackFunction = "updateTimer('" + (-1) + "')";
                        //}
                        vSeccionInicia = vSeccionesPrueba.ElementAt(vseccion + 1);
                        //vSeccionInicia.FE_INICIO = DateTime.Now;
                        //vSeccionInicia.CL_ESTADO = E_ESTADO_PRUEBA.INICIADA.ToString();
                    }
                    else
                    {
                        SPE_OBTIENE_K_PRUEBA_Result vPruebaTerminada = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba).FirstOrDefault();
                        E_RESULTADO vResultadoTestEnd = nKprueba.CorrigePrueba(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
                        vSeccionInicia = vSeccionesPrueba.ElementAt(vseccion);
                        CallBackFunction = "";
                    }

                    if (Request.QueryString["MOD"] != null)
                    {


                        E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vObjetoPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: "INGLES-" + (vseccion + 1), usuario: vClUsuario, programa: vNbPrograma);
                        string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                        UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
                        initRespuestasIngles();
                    }
                    else
                    {

                        E_RESULTADO vResultadoSeccion = nKprueba.InsertaActualiza_K_PRUEBA_SECCION(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), v_k_prueba: vSeccionInicia, usuario: vClUsuario, programa: vNbPrograma);
                        E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vObjetoPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: "INGLES-" + (vseccion + 1), usuario: vClUsuario, programa: vNbPrograma);
                        string vMensaje = instrucciones(vseccion + 1);
                        int vHeight = HeightRadAlert(vseccion + 1);
                        UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, vHeight, CallBackFunction);
                    }
                }
            }
        }

        public void controltime(int? vPosicionPrueba, int? vTiempoPrueba)
        {
            vSeccionAtime = vSeccionesPrueba.ElementAt(0).NO_TIEMPO * 60;
            vSeccionBtime = vSeccionesPrueba.ElementAt(1).NO_TIEMPO * 60;
            vSeccionCtime = vSeccionesPrueba.ElementAt(2).NO_TIEMPO * 60;
            vSeccionDtime = vSeccionesPrueba.ElementAt(3).NO_TIEMPO * 60;

            switch (vPosicionPrueba)
            {
                case 0:
                    vSeccionAtime = vTiempoPrueba;
                    break;
                case 1:
                    vSeccionBtime = vTiempoPrueba;

                    break;
                case 2:
                    vSeccionCtime = vTiempoPrueba;
                    break;
                case 3:
                    vSeccionDtime = vTiempoPrueba;
                    break;
                default: break;

            }
        }

        public string instrucciones(int seccion)
        {
            string instruccion = "";
            switch (seccion)
            {
                case 1:
                    instruccion = "<p style=\"margin: 10px; text-align: justify;\"> <b>II Directions</b><br />" +
                     "Read carefully the following questions, choose the correct answer, there is only one correct per item. Each question has a limit time to be answered don´t spend a lot of time in select one." +
                     "</p>";
                    break;
                case 2:
                    instruccion = "<p style=\"margin: 10px; text-align: justify; \"><b>III Directions</b><br />" +
                 "Read carefully the following questions, choose the correct answer, there is only one correct per item. Each question has a limit time to be answered don´t spend a lot of time in select one." +
                 "</p>";

                    break;
                case 3:
                    instruccion = "<p style=\"margin: 10px; text-align: justify; \"><b>IV Directions</b><br />" +
                  "Read carefully the following questions, choose the correct answer, there is only one correct per item. Each question has a limit time to be answered don´t spend a lot of time in select one." +
                  "</p>";
                    break;
                default:
                    instruccion = "Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido. <br> Cuando esté listo para pasar a la siguiente prueba, por favor haga clic en el botón 'Siguiente' más abajo <br>Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no te lo permitirá: se generará un error y el intento quedará registrado";
                    break;
            }
            return instruccion;
        }

        public int HeightRadAlert(int seccion)
        {
            int height = 0;
            switch (seccion)
            {
                case 0: height = 250; break;
                case 1: height = 250; break;
                case 2: height = 250; break;
                case 3: height = 250; break;
                default: height = 380;
                    break;
            }
            return height;
        }

        public void BackQuestionObject(string vRespuestaCorrecta, string pclPregunta, string pnbRespuesta)
        {
            var vPregunta = vRespuestas.Where(x => x.CL_PREGUNTA.Equals(pclPregunta)).FirstOrDefault();
            if (vPregunta != null)
            {
                decimal vNoValor;

                if (vRespuestaCorrecta.Equals(pnbRespuesta.ToLower()))
                {
                    vNoValor = 1;
                }
                else
                {
                    vNoValor = 0;
                }
                vPregunta.NB_RESPUESTA = pnbRespuesta;
                vPregunta.NO_VALOR_RESPUESTA = vNoValor;
            }
        }

        public String BackSelectedCheckBox(RadButton a, RadButton b, RadButton c, RadButton d)
        {
            String resultado = "";
            if (a.Checked)
            { resultado = "A"; }
            else if (b.Checked)
            { resultado = "B"; }
            else if (c.Checked)
            { resultado = "C"; }
            else if (d.Checked)
            { resultado = "D"; }
            else
            {
                resultado = "-";
            }
            return resultado;
        }

        public string BackLetterQuestions(int position)
        {
            string letter = "";
            switch (position)
            {
                case 0: letter = "A"; break;
                case 1: letter = "B"; break;
                case 2: letter = "C"; break;
                case 3: letter = "D"; break;
                default: break;
            }
            return letter;
        }

        public void asignarValores(List<E_RESULTADOS_PRUEBA> respuestas)
        {
            if (respuestas != null || respuestas.Count > 0)
            {
                foreach (E_RESULTADOS_PRUEBA resp in respuestas)
                {
                    switch (resp.CL_PREGUNTA)
                    {
                        case "INGLES-A-0001": SeleccionarBotonRespuesta(APregunta1Resp1, APregunta1Resp2, APregunta1Resp3, APregunta1Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0002": SeleccionarBotonRespuesta(APregunta2Resp1, APregunta2Resp2, APregunta2Resp3, APregunta2Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0003": SeleccionarBotonRespuesta(APregunta3Resp1, APregunta3Resp2, APregunta3Resp3, APregunta3Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0004": SeleccionarBotonRespuesta(APregunta4Resp1, APregunta4Resp2, APregunta4Resp3, APregunta4Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0005": SeleccionarBotonRespuesta(APregunta5Resp1, APregunta5Resp2, APregunta5Resp3, APregunta5Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0006": SeleccionarBotonRespuesta(APregunta6Resp1, APregunta6Resp2, APregunta6Resp3, APregunta6Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0007": SeleccionarBotonRespuesta(APregunta7Resp1, APregunta7Resp2, APregunta7Resp3, APregunta7Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0008": SeleccionarBotonRespuesta(APregunta8Resp1, APregunta8Resp2, APregunta8Resp3, APregunta8Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0009": SeleccionarBotonRespuesta(APregunta9Resp1, APregunta9Resp2, APregunta9Resp3, APregunta9Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0010": SeleccionarBotonRespuesta(APregunta10Resp1, APregunta10Resp2, APregunta10Resp3, APregunta10Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0011": SeleccionarBotonRespuesta(APregunta11Resp1, APregunta11Resp2, APregunta11Resp3, APregunta11Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0012": SeleccionarBotonRespuesta(APregunta12Resp1, APregunta12Resp2, APregunta12Resp3, APregunta12Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0013": SeleccionarBotonRespuesta(APregunta13Resp1, APregunta13Resp2, APregunta13Resp3, APregunta13Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0014": SeleccionarBotonRespuesta(APregunta14Resp1, APregunta14Resp2, APregunta14Resp3, APregunta14Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0015": SeleccionarBotonRespuesta(APregunta15Resp1, APregunta15Resp2, APregunta15Resp3, APregunta15Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0016": SeleccionarBotonRespuesta(APregunta16Resp1, APregunta16Resp2, APregunta16Resp3, APregunta16Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0017": SeleccionarBotonRespuesta(APregunta17Resp1, APregunta17Resp2, APregunta17Resp3, APregunta17Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0018": SeleccionarBotonRespuesta(APregunta18Resp1, APregunta18Resp2, APregunta18Resp3, APregunta18Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0019": SeleccionarBotonRespuesta(APregunta19Resp1, APregunta19Resp2, APregunta19Resp3, APregunta19Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0020": SeleccionarBotonRespuesta(APregunta20Resp1, APregunta20Resp2, APregunta20Resp3, APregunta20Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0021": SeleccionarBotonRespuesta(APregunta21Resp1, APregunta21Resp2, APregunta21Resp3, APregunta21Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0022": SeleccionarBotonRespuesta(APregunta22Resp1, APregunta22Resp2, APregunta22Resp3, APregunta22Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0023": SeleccionarBotonRespuesta(APregunta23Resp1, APregunta23Resp2, APregunta23Resp3, APregunta23Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0024": SeleccionarBotonRespuesta(APregunta24Resp1, APregunta24Resp2, APregunta24Resp3, APregunta24Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0025": SeleccionarBotonRespuesta(APregunta25Resp1, APregunta25Resp2, APregunta25Resp3, APregunta25Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0026": SeleccionarBotonRespuesta(APregunta26Resp1, APregunta26Resp2, APregunta26Resp3, APregunta26Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0027": SeleccionarBotonRespuesta(APregunta27Resp1, APregunta27Resp2, APregunta27Resp3, APregunta27Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0028": SeleccionarBotonRespuesta(APregunta28Resp1, APregunta28Resp2, APregunta28Resp3, APregunta28Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0029": SeleccionarBotonRespuesta(APregunta29Resp1, APregunta29Resp2, APregunta29Resp3, APregunta29Resp4, resp.NB_RESPUESTA,RPView1); break;
                        case "INGLES-A-0030": SeleccionarBotonRespuesta(APregunta30Resp1, APregunta30Resp2, APregunta30Resp3, APregunta30Resp4, resp.NB_RESPUESTA,RPView1); break;

                        ///////seccion 2

                        case "INGLES-B-0001": SeleccionarBotonRespuesta(BPregunta1Resp1, BPregunta1Resp2, BPregunta1Resp3, BPregunta1Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0002": SeleccionarBotonRespuesta(BPregunta2Resp1, BPregunta2Resp2, BPregunta2Resp3, BPregunta2Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0003": SeleccionarBotonRespuesta(BPregunta3Resp1, BPregunta3Resp2, BPregunta3Resp3, BPregunta3Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0004": SeleccionarBotonRespuesta(BPregunta4Resp1, BPregunta4Resp2, BPregunta4Resp3, BPregunta4Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0005": SeleccionarBotonRespuesta(BPregunta5Resp1, BPregunta5Resp2, BPregunta5Resp3, BPregunta5Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0006": SeleccionarBotonRespuesta(BPregunta6Resp1, BPregunta6Resp2, BPregunta6Resp3, BPregunta6Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0007": SeleccionarBotonRespuesta(BPregunta7Resp1, BPregunta7Resp2, BPregunta7Resp3, BPregunta7Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0008": SeleccionarBotonRespuesta(BPregunta8Resp1, BPregunta8Resp2, BPregunta8Resp3, BPregunta8Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0009": SeleccionarBotonRespuesta(BPregunta9Resp1, BPregunta9Resp2, BPregunta9Resp3, BPregunta9Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0010": SeleccionarBotonRespuesta(BPregunta10Resp1, BPregunta10Resp2, BPregunta10Resp3, BPregunta10Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0011": SeleccionarBotonRespuesta(BPregunta11Resp1, BPregunta11Resp2, BPregunta11Resp3, BPregunta11Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0012": SeleccionarBotonRespuesta(BPregunta12Resp1, BPregunta12Resp2, BPregunta12Resp3, BPregunta12Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0013": SeleccionarBotonRespuesta(BPregunta13Resp1, BPregunta13Resp2, BPregunta13Resp3, BPregunta13Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0014": SeleccionarBotonRespuesta(BPregunta14Resp1, BPregunta14Resp2, BPregunta14Resp3, BPregunta14Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0015": SeleccionarBotonRespuesta(BPregunta15Resp1, BPregunta15Resp2, BPregunta15Resp3, BPregunta15Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0016": SeleccionarBotonRespuesta(BPregunta16Resp1, BPregunta16Resp2, BPregunta16Resp3, BPregunta16Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0017": SeleccionarBotonRespuesta(BPregunta17Resp1, BPregunta17Resp2, BPregunta17Resp3, BPregunta17Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0018": SeleccionarBotonRespuesta(BPregunta18Resp1, BPregunta18Resp2, BPregunta18Resp3, BPregunta18Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0019": SeleccionarBotonRespuesta(BPregunta19Resp1, BPregunta19Resp2, BPregunta19Resp3, BPregunta19Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0020": SeleccionarBotonRespuesta(BPregunta20Resp1, BPregunta20Resp2, BPregunta20Resp3, BPregunta20Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0021": SeleccionarBotonRespuesta(BPregunta21Resp1, BPregunta21Resp2, BPregunta21Resp3, BPregunta21Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0022": SeleccionarBotonRespuesta(BPregunta22Resp1, BPregunta22Resp2, BPregunta22Resp3, BPregunta22Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0023": SeleccionarBotonRespuesta(BPregunta23Resp1, BPregunta23Resp2, BPregunta23Resp3, BPregunta23Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0024": SeleccionarBotonRespuesta(BPregunta24Resp1, BPregunta24Resp2, BPregunta24Resp3, BPregunta24Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0025": SeleccionarBotonRespuesta(BPregunta25Resp1, BPregunta25Resp2, BPregunta25Resp3, BPregunta25Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0026": SeleccionarBotonRespuesta(BPregunta26Resp1, BPregunta26Resp2, BPregunta26Resp3, BPregunta26Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0027": SeleccionarBotonRespuesta(BPregunta27Resp1, BPregunta27Resp2, BPregunta27Resp3, BPregunta27Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0028": SeleccionarBotonRespuesta(BPregunta28Resp1, BPregunta28Resp2, BPregunta28Resp3, BPregunta28Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0029": SeleccionarBotonRespuesta(BPregunta29Resp1, BPregunta29Resp2, BPregunta29Resp3, BPregunta29Resp4, resp.NB_RESPUESTA,RPView2); break;
                        case "INGLES-B-0030": SeleccionarBotonRespuesta(BPregunta30Resp1, BPregunta30Resp2, BPregunta30Resp3, BPregunta30Resp4, resp.NB_RESPUESTA,RPView2); break;
                        ///////seccion 3


                        case "INGLES-C-0001": SeleccionarBotonRespuesta(CPregunta1Resp1, CPregunta1Resp2, CPregunta1Resp3, CPregunta1Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0002": SeleccionarBotonRespuesta(CPregunta2Resp1, CPregunta2Resp2, CPregunta2Resp3, CPregunta2Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0003": SeleccionarBotonRespuesta(CPregunta3Resp1, CPregunta3Resp2, CPregunta3Resp3, CPregunta3Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0004": SeleccionarBotonRespuesta(CPregunta4Resp1, CPregunta4Resp2, CPregunta4Resp3, CPregunta4Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0005": SeleccionarBotonRespuesta(CPregunta5Resp1, CPregunta5Resp2, CPregunta5Resp3, CPregunta5Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0006": SeleccionarBotonRespuesta(CPregunta6Resp1, CPregunta6Resp2, CPregunta6Resp3, CPregunta6Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0007": SeleccionarBotonRespuesta(CPregunta7Resp1, CPregunta7Resp2, CPregunta7Resp3, CPregunta7Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0008": SeleccionarBotonRespuesta(CPregunta8Resp1, CPregunta8Resp2, CPregunta8Resp3, CPregunta8Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0009": SeleccionarBotonRespuesta(CPregunta9Resp1, CPregunta9Resp2, CPregunta9Resp3, CPregunta9Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0010": SeleccionarBotonRespuesta(CPregunta10Resp1, CPregunta10Resp2, CPregunta10Resp3, CPregunta10Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0011": SeleccionarBotonRespuesta(CPregunta11Resp1, CPregunta11Resp2, CPregunta11Resp3, CPregunta11Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0012": SeleccionarBotonRespuesta(CPregunta12Resp1, CPregunta12Resp2, CPregunta12Resp3, CPregunta12Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0013": SeleccionarBotonRespuesta(CPregunta13Resp1, CPregunta13Resp2, CPregunta13Resp3, CPregunta13Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0014": SeleccionarBotonRespuesta(CPregunta14Resp1, CPregunta14Resp2, CPregunta14Resp3, CPregunta14Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0015": SeleccionarBotonRespuesta(CPregunta15Resp1, CPregunta15Resp2, CPregunta15Resp3, CPregunta15Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0016": SeleccionarBotonRespuesta(CPregunta16Resp1, CPregunta16Resp2, CPregunta16Resp3, CPregunta16Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0017": SeleccionarBotonRespuesta(CPregunta17Resp1, CPregunta17Resp2, CPregunta17Resp3, CPregunta17Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0018": SeleccionarBotonRespuesta(CPregunta18Resp1, CPregunta18Resp2, CPregunta18Resp3, CPregunta18Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0019": SeleccionarBotonRespuesta(CPregunta19Resp1, CPregunta19Resp2, CPregunta19Resp3, CPregunta19Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0020": SeleccionarBotonRespuesta(CPregunta20Resp1, CPregunta20Resp2, CPregunta20Resp3, CPregunta20Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0021": SeleccionarBotonRespuesta(CPregunta21Resp1, CPregunta21Resp2, CPregunta21Resp3, CPregunta21Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0022": SeleccionarBotonRespuesta(CPregunta22Resp1, CPregunta22Resp2, CPregunta22Resp3, CPregunta22Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0023": SeleccionarBotonRespuesta(CPregunta23Resp1, CPregunta23Resp2, CPregunta23Resp3, CPregunta23Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0024": SeleccionarBotonRespuesta(CPregunta24Resp1, CPregunta24Resp2, CPregunta24Resp3, CPregunta24Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0025": SeleccionarBotonRespuesta(CPregunta25Resp1, CPregunta25Resp2, CPregunta25Resp3, CPregunta25Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0026": SeleccionarBotonRespuesta(CPregunta26Resp1, CPregunta26Resp2, CPregunta26Resp3, CPregunta26Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0027": SeleccionarBotonRespuesta(CPregunta27Resp1, CPregunta27Resp2, CPregunta27Resp3, CPregunta27Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0028": SeleccionarBotonRespuesta(CPregunta28Resp1, CPregunta28Resp2, CPregunta28Resp3, CPregunta28Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0029": SeleccionarBotonRespuesta(CPregunta29Resp1, CPregunta29Resp2, CPregunta29Resp3, CPregunta29Resp4, resp.NB_RESPUESTA,RPView3); break;
                        case "INGLES-C-0030": SeleccionarBotonRespuesta(CPregunta30Resp1, CPregunta30Resp2, CPregunta30Resp3, CPregunta30Resp4, resp.NB_RESPUESTA,RPView3); break;
                        ///////seccion


                        case "INGLES-D-0001": SeleccionarBotonRespuesta(DPregunta1Resp1, DPregunta1Resp2, DPregunta1Resp3, DPregunta1Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0002": SeleccionarBotonRespuesta(DPregunta2Resp1, DPregunta2Resp2, DPregunta2Resp3, DPregunta2Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0003": SeleccionarBotonRespuesta(DPregunta3Resp1, DPregunta3Resp2, DPregunta3Resp3, DPregunta3Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0004": SeleccionarBotonRespuesta(DPregunta4Resp1, DPregunta4Resp2, DPregunta4Resp3, DPregunta4Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0005": SeleccionarBotonRespuesta(DPregunta5Resp1, DPregunta5Resp2, DPregunta5Resp3, DPregunta5Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0006": SeleccionarBotonRespuesta(DPregunta6Resp1, DPregunta6Resp2, DPregunta6Resp3, DPregunta6Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0007": SeleccionarBotonRespuesta(DPregunta7Resp1, DPregunta7Resp2, DPregunta7Resp3, DPregunta7Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0008": SeleccionarBotonRespuesta(DPregunta8Resp1, DPregunta8Resp2, DPregunta8Resp3, DPregunta8Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0009": SeleccionarBotonRespuesta(DPregunta9Resp1, DPregunta9Resp2, DPregunta9Resp3, DPregunta9Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0010": SeleccionarBotonRespuesta(DPregunta10Resp1, DPregunta10Resp2, DPregunta10Resp3, DPregunta10Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0011": SeleccionarBotonRespuesta(DPregunta11Resp1, DPregunta11Resp2, DPregunta11Resp3, DPregunta11Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0012": SeleccionarBotonRespuesta(DPregunta12Resp1, DPregunta12Resp2, DPregunta12Resp3, DPregunta12Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0013": SeleccionarBotonRespuesta(DPregunta13Resp1, DPregunta13Resp2, DPregunta13Resp3, DPregunta13Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0014": SeleccionarBotonRespuesta(DPregunta14Resp1, DPregunta14Resp2, DPregunta14Resp3, DPregunta14Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0015": SeleccionarBotonRespuesta(DPregunta15Resp1, DPregunta15Resp2, DPregunta15Resp3, DPregunta15Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0016": SeleccionarBotonRespuesta(DPregunta16Resp1, DPregunta16Resp2, DPregunta16Resp3, DPregunta16Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0017": SeleccionarBotonRespuesta(DPregunta17Resp1, DPregunta17Resp2, DPregunta17Resp3, DPregunta17Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0018": SeleccionarBotonRespuesta(DPregunta18Resp1, DPregunta18Resp2, DPregunta18Resp3, DPregunta18Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0019": SeleccionarBotonRespuesta(DPregunta19Resp1, DPregunta19Resp2, DPregunta19Resp3, DPregunta19Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0020": SeleccionarBotonRespuesta(DPregunta20Resp1, DPregunta20Resp2, DPregunta20Resp3, DPregunta20Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0021": SeleccionarBotonRespuesta(DPregunta21Resp1, DPregunta21Resp2, DPregunta21Resp3, DPregunta21Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0022": SeleccionarBotonRespuesta(DPregunta22Resp1, DPregunta22Resp2, DPregunta22Resp3, DPregunta22Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0023": SeleccionarBotonRespuesta(DPregunta23Resp1, DPregunta23Resp2, DPregunta23Resp3, DPregunta23Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0024": SeleccionarBotonRespuesta(DPregunta24Resp1, DPregunta24Resp2, DPregunta24Resp3, DPregunta24Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0025": SeleccionarBotonRespuesta(DPregunta25Resp1, DPregunta25Resp2, DPregunta25Resp3, DPregunta25Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0026": SeleccionarBotonRespuesta(DPregunta26Resp1, DPregunta26Resp2, DPregunta26Resp3, DPregunta26Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0027": SeleccionarBotonRespuesta(DPregunta27Resp1, DPregunta27Resp2, DPregunta27Resp3, DPregunta27Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0028": SeleccionarBotonRespuesta(DPregunta28Resp1, DPregunta28Resp2, DPregunta28Resp3, DPregunta28Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0029": SeleccionarBotonRespuesta(DPregunta29Resp1, DPregunta29Resp2, DPregunta29Resp3, DPregunta29Resp4, resp.NB_RESPUESTA,RPView4); break;
                        case "INGLES-D-0030": SeleccionarBotonRespuesta(DPregunta30Resp1, DPregunta30Resp2, DPregunta30Resp3, DPregunta30Resp4, resp.NB_RESPUESTA,RPView4); break;
                        ///////seccion
                        default: break;
                    }
                }
            }
        }

        public void SeleccionarBotonRespuesta(RadButton a, RadButton b, RadButton c, RadButton d, string pAnswer,RadPageView v)
        {
            RadButton t ;
            switch (pAnswer)
            {
                case "A": t = v.FindControl(a.ID) as RadButton; t.Checked = true; break;
                case "B": t = v.FindControl(b.ID) as RadButton; t.Checked = true; break;
                case "C": t = v.FindControl(c.ID) as RadButton; t.Checked = true; break;
                case "D": t = v.FindControl(d.ID) as RadButton; t.Checked = true; break;
                default: break;
            }
        }

        protected void tbInglesSecciones_TabClick(object sender, RadTabStripEventArgs e)
        {
            mpgIngles.SelectedIndex = e.Tab.Index;
            asignarValores(vResultadosRevision.Where(item => item.CL_PREGUNTA.Contains("INGLES-" + BackLetterQuestions(mpgIngles.SelectedIndex) + "-")).ToList());
            instrucciones(e.Tab.Index);
        }
        public void habilitarResultadosIngles(List<E_RESULTADOS_PRUEBA> lista)
        {
            if (lista.Count > 91)
            {
                tbInglesSecciones.Tabs.ElementAt(3).Visible = true;
                tbInglesSecciones.Tabs.ElementAt(2).Visible = true;
                tbInglesSecciones.Tabs.ElementAt(1).Visible = true;
                tbInglesSecciones.Tabs.ElementAt(0).Visible = true;
            }
            else if (lista.Count > 61)
            {
                tbInglesSecciones.Tabs.ElementAt(2).Visible = true;
                tbInglesSecciones.Tabs.ElementAt(1).Visible = true;
                tbInglesSecciones.Tabs.ElementAt(0).Visible = true;
            }

            else if (lista.Count > 31)
            {
                tbInglesSecciones.Tabs.ElementAt(1).Visible = true;
                tbInglesSecciones.Tabs.ElementAt(0).Visible = true;
            }
            else
            {
                tbInglesSecciones.Tabs.ElementAt(0).Visible = true;
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