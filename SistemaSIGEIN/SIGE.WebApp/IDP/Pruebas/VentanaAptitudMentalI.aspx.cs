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

namespace SIGE.WebApp.IDP
{

    public partial class VentanaAptitudMental1 : System.Web.UI.Page
    {
        #region Variables

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

        private Guid vClToken
        {
            get { return (Guid)ViewState["vsClToken"]; }
            set { ViewState["vsClToken"] = value; }
        }

        public Guid vClTokenExterno
        {
            get { return (Guid)ViewState["vClTokenExterno"]; }
            set { ViewState["vClTokenExterno"] = value; }
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

        public int? vSeccionEtime
        {
            get { return (int?)ViewState["vSeccionEtime"]; }
            set { ViewState["vSeccionEtime"] = value; }
        }

        public int? vSeccionFtime
        {
            get { return (int?)ViewState["vSeccionFtime"]; }
            set { ViewState["vSeccionFtime"] = value; }
        }

        public int? vSeccionGtime
        {
            get { return (int?)ViewState["vSeccionGtime"]; }
            set { ViewState["vSeccionGtime"] = value; }
        }

        public int? vSeccionHtime
        {
            get { return (int?)ViewState["vSeccionHtime"]; }
            set { ViewState["vSeccionHtime"] = value; }
        }

        public int? vSeccionItime
        {
            get { return (int?)ViewState["vSeccionItime"]; }
            set { ViewState["vSeccionItime"] = value; }
        }

        public int? vSeccionJtime
        {
            get { return (int?)ViewState["vSeccionJtime"]; }
            set { ViewState["vSeccionJtime"] = value; }
        }

        public int vIndexMultiPage
        {
            get { return (int)ViewState["vIndexMultipage"]; }
            set { ViewState["vIndexMultipage"] = value; }
        }

        //public int vRadAlertAltura
        //{
        //    get { return (int)ViewState["vRadAlertAltura"]; }
        //    set { ViewState["vRadAlertAltura"] = value; }
        //}

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
            get { return (bool)ViewState["vsMostrarCronometroAM1"]; }
            set { ViewState["vsMostrarCronometroAM1"] = value; }
        }

        public string vMOD
        {
            get { return (string)ViewState["vsvMod"]; }
            set { ViewState["vsvMod"] = value; }
        }

        public int vIdBateria
        {
            get { return (int)ViewState["vsIdBateria"]; }
            set { ViewState["vsIdBateria"] = value; }
        }
        
        #endregion

        protected void SeleccionaSeccionPrueba()
        {
            PruebasNegocio nKprueba = new PruebasNegocio();

            int position = mpgActitudMentalI.SelectedIndex;
            var KPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken).FirstOrDefault();
            vSeccionesPrueba = new List<E_PRUEBA_TIEMPO>();
            var vSegmentos = nKprueba.Obtener_K_PRUEBA_SECCION(pIdPrueba: KPrueba.ID_PRUEBA);
            vSeccionesPrueba = ParseList(vSegmentos);
            int VPosicionPrueba = IniciaPruebaSeccion(vSeccionesPrueba);

            if (VPosicionPrueba < 10)
            {
                E_RESULTADO vObjetoPrueba = nKprueba.INICIAR_K_PRUEBA_SECCION(pIdPrueba: vSeccionesPrueba.ElementAt(VPosicionPrueba).ID_PRUEBA_SECCION, pFeInicio: DateTime.Now, usuario: "ANONIMO", programa: vNbPrograma);
                E_RESULTADO vPrueba = nKprueba.INICIAR_K_PRUEBA(pIdPrueba: vIdPrueba, pFeInicio: DateTime.Now, pClTokenExterno: vClToken, usuario: "ANONIMO", programa: vNbPrograma);
                if (vObjetoPrueba != null)
                {
                    if (vObjetoPrueba.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.ERROR)
                    {
                    }
                    else if (vObjetoPrueba.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                    {
                        vTiempoInicio = int.Parse(vObjetoPrueba.MENSAJE.Where(r => r.CL_IDIOMA.Equals("ES")).FirstOrDefault().DS_MENSAJE.ToString());
                    }

                    if (vTiempoInicio > 0) //Se agrega condición para verificar que el tiempo sea mayor a cero, si no es asi pasa a la siguiente sección.13/04/2018
                    {
                        controltime(VPosicionPrueba, vTiempoInicio);
                    }
                    else
                    {
                        //Como en la sección el tiempo se ha agotado, se guarda como terminada para avanzar a la siguiente
                        var vSeccionTermina = new E_PRUEBA_TIEMPO();
                        vSeccionTermina = vSeccionesPrueba.ElementAt(VPosicionPrueba);
                        vSeccionTermina.CL_ESTADO = E_ESTADO_PRUEBA.TERMINADA.ToString();
                        vSeccionTermina.FE_TERMINO = DateTime.Now;  
                        E_RESULTADO vResultadoSeccion = nKprueba.InsertaActualiza_K_PRUEBA_SECCION(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), v_k_prueba: vSeccionTermina, usuario: vClUsuario, programa: vNbPrograma);
                        //En esta parte se vuelve a mandar llamar al método SeleccionaSeccionPrueba para incrementar la sección de la prueba.
                        mpgActitudMentalI.SelectedIndex = VPosicionPrueba + 1;
                        SeleccionaSeccionPrueba();
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = (ContextoUsuario.oUsuario != null ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO");
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null && Request.QueryString["MOD"] == null)
                {
                   
                    vIdPrueba = int.Parse(Request.QueryString["ID"]);
                    vClToken = new Guid(Request.QueryString["T"]);
                    vClTokenExterno = new Guid(Request.QueryString["T"]);
                    if (Request.QueryString["vIdBateria"] != null)
					vIdBateria = int.Parse(Request.QueryString["vIdBateria"]);

                    SeleccionaSeccionPrueba(); //Método creado 13/04/2018 que permite cambiar de seccion si el tiempo esta agotado

                    //int position = mpgActitudMentalI.SelectedIndex;
                    //var KPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken).FirstOrDefault();
                    //vSeccionesPrueba = new List<E_PRUEBA_TIEMPO>();
                    //var vSegmentos = nKprueba.Obtener_K_PRUEBA_SECCION(pIdPrueba: KPrueba.ID_PRUEBA);
                    //vSeccionesPrueba = ParseList(vSegmentos);
                    //int VPosicionPrueba = IniciaPruebaSeccion(vSeccionesPrueba);

                    //if (VPosicionPrueba < 10)
                    //{
                    //    E_RESULTADO vObjetoPrueba = nKprueba.INICIAR_K_PRUEBA_SECCION(pIdPrueba: vSeccionesPrueba.ElementAt(VPosicionPrueba).ID_PRUEBA_SECCION, pFeInicio: DateTime.Now, usuario: "ANONIMO", programa: vNbPrograma);
                    //    E_RESULTADO vPrueba = nKprueba.INICIAR_K_PRUEBA(pIdPrueba: vIdPrueba, pFeInicio: DateTime.Now, pClTokenExterno: vClToken, usuario: "ANONIMO", programa: vNbPrograma);
                    //    if (vObjetoPrueba != null)
                    //    {
                    //        if (vObjetoPrueba.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.ERROR)
                    //        {
                    //        }
                    //        else if (vObjetoPrueba.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                    //        {
                    //            vTiempoInicio = int.Parse(vObjetoPrueba.MENSAJE.Where(r => r.CL_IDIOMA.Equals("ES")).FirstOrDefault().DS_MENSAJE.ToString());
                    //        }
                    //            controltime(VPosicionPrueba, vTiempoInicio);    
                    //       // vRadAlertAltura = HeightRadAlert(VPosicionPrueba);
                    //    }
                    //}
                    ////else
                    ////{
                    ////   // vRadAlertAltura = HeightRadAlert(320);
                    ////}
                }
                else
                {
                    vMOD = Request.QueryString["MOD"];            
                    PintarRespuestasMentalI();
                    mpgActitudMentalI.RenderSelectedPageOnly = true;
                }

                MostrarCronometro = ContextoApp.IDP.ConfiguracionPsicometria.FgMostrarCronometro;
                vRespuestas = new List<E_PREGUNTA>();
            }
        }

        public void PintarRespuestasMentalI()
        {
            PruebasNegocio nKprueba = new PruebasNegocio();
            vIdPrueba = int.Parse(Request.QueryString["ID"]);
            vClToken = new Guid(Request.QueryString["T"]);
            vClTokenExterno = new Guid(Request.QueryString["T"]);
            vTipoRevision = Request.QueryString["MOD"];
           // vRadAlertAltura = HeightRadAlert(0);
            int position = mpgActitudMentalI.SelectedIndex;
            vSeccionesPrueba = new List<E_PRUEBA_TIEMPO>();
            var vSegmentos = nKprueba.Obtener_K_PRUEBA_SECCION(pIdPrueba: vIdPrueba);
            vSeccionesPrueba = ParseList(vSegmentos);
            //Si el modo de revision esta activado
            if (vTipoRevision == "REV")
            {
                cronometro.Visible = false;
                vTiempoInicio = 0;
                btnImpresionPrueba.Visible = true;
                //btnTerminar.Text = "Guardar";
                //btnTerminar.Enabled = false;
                btnSiguiente.Enabled = false;
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
                asignarValores(vResultadosRevision.Where(item => item.CL_PREGUNTA.Contains("APTITUD1-" + TraerLetraSeccion(mpgActitudMentalI.SelectedIndex) + "-")).ToList());
                habilitarResultadosAptitudMentalI(vResultadosRevision);
            }

            if (vTipoRevision == "EDIT")
            {
                cronometro.Visible = false;
                vTiempoInicio = 0;
                btnSiguiente.Text = "Guardar"; // Se agrega para la nueva forma de navegación 06/06/2018
                btnImpresionPrueba.Visible = true; // Se agrega para la nueva forma de navegación 06/06/2018
               // btnEliminar.Visible = true;// Se agrega para la nueva forma de navegación 06/06/2018
              //  btnTerminar.Text = "Guardar";
                //btnTerminar.Visible = false;
                //RadButton1.Visible = false;
                //RadButton2.Visible = true;
                //btnCorregir.Visible = true;
                var vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken).FirstOrDefault();
                if (vPrueba.NB_TIPO_PRUEBA == "MANUAL")
                    btnSiguiente.Enabled = false;

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
                asignarValores(vResultadosRevision.Where(item => item.CL_PREGUNTA.Contains("APTITUD1-" + TraerLetraSeccion(mpgActitudMentalI.SelectedIndex) + "-")).ToList());
                habilitarResultadosAptitudMentalI(vResultadosRevision);
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
            mpgActitudMentalI.SelectedIndex = vIndexMultiPage;
            return vIndexMultiPage;
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

        //protected void btnTerminar_Click(object sender, EventArgs e)
        //{
        //    PruebasNegocio nKprueba = new PruebasNegocio();
        //    var vSeccionTermina = vSeccionesPrueba.ElementAt(mpgActitudMentalI.SelectedIndex);
        //    if (vTipoRevision == "REV")
        //    {

        //    }
        //    else
        //    {
        //        vSeccionTermina.CL_ESTADO = E_ESTADO_PRUEBA.TERMINADA.ToString();
        //        vSeccionTermina.FE_TERMINO = DateTime.Now;

        //    }
        //    E_RESULTADO vResultadoSeccion = nKprueba.InsertaActualiza_K_PRUEBA_SECCION(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), v_k_prueba: vSeccionTermina, usuario: vClUsuario, programa: vNbPrograma);
        //    //    if (vResultadoSeccion.CL_TIPO_ERROR != E_TIPO_RESPUESTA_DB.WARNING)
        //    SaveTest(mpgActitudMentalI.SelectedIndex);

        //    //    else
        //    //    {
        //    //        string vMensaje = vResultadoSeccion.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
        //    //        UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultadoSeccion.CL_TIPO_ERROR, 400, 150, "");
        //    //    }
        //}

        public void SaveTest(int vseccion)
        {
            string vPrueba = "APTITUD1";
            CuestionariosNegocio nPreguntas = new CuestionariosNegocio();
            var preguntas = nPreguntas.Obtener_K_PREGUNTA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken);
            var filtroPreguntas = preguntas.Where(oh => oh.CL_PREGUNTA.StartsWith("APTITUD1-" + TraerLetraSeccion(vseccion))).ToList();

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
                        String vAPTITUD1_A_0001 = BackSelectedCheckBox(APregunta1Resp1, APregunta1Resp2, APregunta1Resp3, APregunta1Resp4);
                        BackQuestionObject("APTITUD1-A-0001", vAPTITUD1_A_0001);

                        String vAPTITUD1_A_0002 = BackSelectedCheckBox(APregunta2Resp1, APregunta2Resp2, APregunta2Resp3, APregunta2Resp4);
                        BackQuestionObject("APTITUD1-A-0002", vAPTITUD1_A_0002);

                        String vAPTITUD1_A_0003 = BackSelectedCheckBox(APregunta3Resp1, APregunta3Resp2, APregunta3Resp3, APregunta3Resp4);
                        BackQuestionObject("APTITUD1-A-0003", vAPTITUD1_A_0003);

                        String vAPTITUD1_A_0004 = BackSelectedCheckBox(APregunta4Resp1, APregunta4Resp2, APregunta4Resp3, APregunta4Resp4);
                        BackQuestionObject("APTITUD1-A-0004", vAPTITUD1_A_0004);

                        String vAPTITUD1_A_0005 = BackSelectedCheckBox(APregunta5Resp1, APregunta5Resp2, APregunta5Resp3, APregunta5Resp4);
                        BackQuestionObject("APTITUD1-A-0005", vAPTITUD1_A_0005);

                        String vAPTITUD1_A_0006 = BackSelectedCheckBox(APregunta6Resp1, APregunta6Resp2, APregunta6Resp3, APregunta6Resp4);
                        BackQuestionObject("APTITUD1-A-0006", vAPTITUD1_A_0006);

                        String vAPTITUD1_A_0007 = BackSelectedCheckBox(APregunta7Resp1, APregunta7Resp2, APregunta7Resp3, APregunta7Resp4);
                        BackQuestionObject("APTITUD1-A-0007", vAPTITUD1_A_0007);

                        String vAPTITUD1_A_0008 = BackSelectedCheckBox(APregunta8Resp1, APregunta8Resp2, APregunta8Resp3, APregunta8Resp4);
                        BackQuestionObject("APTITUD1-A-0008", vAPTITUD1_A_0008);

                        String vAPTITUD1_A_0009 = BackSelectedCheckBox(APregunta9Resp1, APregunta9Resp2, APregunta9Resp3, APregunta9Resp4);
                        BackQuestionObject("APTITUD1-A-0009", vAPTITUD1_A_0009);

                        String vAPTITUD1_A_0010 = BackSelectedCheckBox(APregunta10Resp1, APregunta10Resp2, APregunta10Resp3, APregunta10Resp4);
                        BackQuestionObject("APTITUD1-A-0010", vAPTITUD1_A_0010);

                        String vAPTITUD1_A_0011 = BackSelectedCheckBox(APregunta11Resp1, APregunta11Resp2, APregunta11Resp3, APregunta11Resp4);
                        BackQuestionObject("APTITUD1-A-0011", vAPTITUD1_A_0011);

                        String vAPTITUD1_A_0012 = BackSelectedCheckBox(APregunta12Resp1, APregunta12Resp2, APregunta12Resp3, APregunta12Resp4);
                        BackQuestionObject("APTITUD1-A-0012", vAPTITUD1_A_0012);

                        String vAPTITUD1_A_0013 = BackSelectedCheckBox(APregunta13Resp1, APregunta13Resp2, APregunta13Resp3, APregunta13Resp4);
                        BackQuestionObject("APTITUD1-A-0013", vAPTITUD1_A_0013);

                        String vAPTITUD1_A_0014 = BackSelectedCheckBox(APregunta14Resp1, APregunta14Resp2, APregunta14Resp3, APregunta14Resp4);
                        BackQuestionObject("APTITUD1-A-0014", vAPTITUD1_A_0014);

                        String vAPTITUD1_A_0015 = BackSelectedCheckBox(APregunta15Resp1, APregunta15Resp2, APregunta15Resp3, APregunta15Resp4);
                        BackQuestionObject("APTITUD1-A-0015", vAPTITUD1_A_0015);

                        String vAPTITUD1_A_0016 = BackSelectedCheckBox(APregunta16Resp1, APregunta16Resp2, APregunta16Resp3, APregunta16Resp4);
                        BackQuestionObject("APTITUD1-A-0016", vAPTITUD1_A_0016);

                        break;

                    case 1:
                        //////////////////////////////////////////////////SECCION B/////////////////////////////////////////////////////////////////////

                        String vAPTITUD1_B_0001 = RespuestasSeccionB(BPregunta1Resp1, BPregunta1Resp2, BPregunta1Resp3);
                        BackQuestionObject("APTITUD1-B-0001", vAPTITUD1_B_0001);

                        String vAPTITUD1_B_0002 = RespuestasSeccionB(BPregunta2Resp1, BPregunta2Resp2, BPregunta2Resp3);
                        BackQuestionObject("APTITUD1-B-0002", vAPTITUD1_B_0002);

                        String vAPTITUD1_B_0003 = RespuestasSeccionB(BPregunta3Resp, BPregunta3Resp2, BPregunta3Resp3);
                        BackQuestionObject("APTITUD1-B-0003", vAPTITUD1_B_0003);

                        String vAPTITUD1_B_0004 = RespuestasSeccionB(BPregunta4Resp1, BPregunta4Resp2, BPregunta4Resp3);
                        BackQuestionObject("APTITUD1-B-0004", vAPTITUD1_B_0004);

                        String vAPTITUD1_B_0005 = RespuestasSeccionB(BPregunta5Resp1, BPregunta5Resp2, BPregunta5Resp3);
                        BackQuestionObject("APTITUD1-B-0005", vAPTITUD1_B_0005);

                        String vAPTITUD1_B_0006 = RespuestasSeccionB(BPregunta6Resp1, BPregunta6Resp2, BPregunta6Resp3);
                        BackQuestionObject("APTITUD1-B-0006", vAPTITUD1_B_0006);

                        String vAPTITUD1_B_0007 = RespuestasSeccionB(BPregunta7Resp1, BPregunta7Resp2, BPregunta7Resp3);
                        BackQuestionObject("APTITUD1-B-0007", vAPTITUD1_B_0007);

                        String vAPTITUD1_B_0008 = RespuestasSeccionB(BPregunta8Resp1, BPregunta8Resp2, BPregunta8Resp3);
                        BackQuestionObject("APTITUD1-B-0008", vAPTITUD1_B_0008);

                        String vAPTITUD1_B_0009 = RespuestasSeccionB(BPregunta9Resp1, BPregunta9Resp2, BPregunta9Resp3);
                        BackQuestionObject("APTITUD1-B-0009", vAPTITUD1_B_0009);

                        String vAPTITUD1_B_0010 = RespuestasSeccionB(BPregunta10Resp1, BPregunta10Resp2, BPregunta10Resp3);
                        BackQuestionObject("APTITUD1-B-0010", vAPTITUD1_B_0010);

                        String vAPTITUD1_B_0011 = RespuestasSeccionB(BPregunta11Resp1, BPregunta11Resp2, BPregunta11Resp3);
                        BackQuestionObject("APTITUD1-B-0011", vAPTITUD1_B_0011);

                        break;

                    case 2:
                        //////////////////////////////////////////////////SECCION C/////////////////////////////////////////////////////////////////////
                        String vAPTITUD1_C_0001 = RespuestasSeccionC(CPregunta1Resp1, CPregunta1Resp2);
                        BackQuestionObject("APTITUD1-C-0001", vAPTITUD1_C_0001);

                        String vAPTITUD1_C_0002 = RespuestasSeccionC(CPregunta2Resp1, CPregunta2Resp2);
                        BackQuestionObject("APTITUD1-C-0002", vAPTITUD1_C_0002);

                        String vAPTITUD1_C_0003 = RespuestasSeccionC(CPregunta3Resp1, CPregunta3Resp2);
                        BackQuestionObject("APTITUD1-C-0003", vAPTITUD1_C_0003);

                        String vAPTITUD1_C_0004 = RespuestasSeccionC(CPregunta4Resp1, CPregunta4Resp2);
                        BackQuestionObject("APTITUD1-C-0004", vAPTITUD1_C_0004);

                        String vAPTITUD1_C_0005 = RespuestasSeccionC(CPregunta5Resp1, CPregunta5Resp2);
                        BackQuestionObject("APTITUD1-C-0005", vAPTITUD1_C_0005);

                        String vAPTITUD1_C_0006 = RespuestasSeccionC(CPregunta6Resp1, CPregunta6Resp2);
                        BackQuestionObject("APTITUD1-C-0006", vAPTITUD1_C_0006);

                        String vAPTITUD1_C_0007 = RespuestasSeccionC(CPregunta7Resp1, CPregunta7Resp2);
                        BackQuestionObject("APTITUD1-C-0007", vAPTITUD1_C_0007);

                        String vAPTITUD1_C_0008 = RespuestasSeccionC(CPregunta8Resp1, CPregunta8Resp2);
                        BackQuestionObject("APTITUD1-C-0008", vAPTITUD1_C_0008);

                        String vAPTITUD1_C_0009 = RespuestasSeccionC(CPregunta9Resp1, CPregunta9Resp2);
                        BackQuestionObject("APTITUD1-C-0009", vAPTITUD1_C_0009);

                        String vAPTITUD1_C_0010 = RespuestasSeccionC(CPregunta10Resp1, CPregunta10Resp2);
                        BackQuestionObject("APTITUD1-C-0010", vAPTITUD1_C_0010);

                        String vAPTITUD1_C_0011 = RespuestasSeccionC(CPregunta11Resp1, CPregunta11Resp2);
                        BackQuestionObject("APTITUD1-C-0011", vAPTITUD1_C_0011);

                        String vAPTITUD1_C_0012 = RespuestasSeccionC(CPregunta12Resp1, CPregunta12Resp2);
                        BackQuestionObject("APTITUD1-C-0012", vAPTITUD1_C_0012);

                        String vAPTITUD1_C_0013 = RespuestasSeccionC(CPregunta13Resp1, CPregunta13Resp2);
                        BackQuestionObject("APTITUD1-C-0013", vAPTITUD1_C_0013);

                        String vAPTITUD1_C_0014 = RespuestasSeccionC(CPregunta14Resp1, CPregunta14Resp2);
                        BackQuestionObject("APTITUD1-C-0014", vAPTITUD1_C_0014);

                        String vAPTITUD1_C_0015 = RespuestasSeccionC(CPregunta15Resp1, CPregunta15Resp2);
                        BackQuestionObject("APTITUD1-C-0015", vAPTITUD1_C_0015);

                        String vAPTITUD1_C_0016 = RespuestasSeccionC(CPregunta16Resp1, CPregunta16Resp2);
                        BackQuestionObject("APTITUD1-C-0016", vAPTITUD1_C_0016);

                        String vAPTITUD1_C_0017 = RespuestasSeccionC(CPregunta17Resp1, CPregunta17Resp2);
                        BackQuestionObject("APTITUD1-C-0017", vAPTITUD1_C_0017);

                        String vAPTITUD1_C_0018 = RespuestasSeccionC(CPregunta18Resp1, CPregunta18Resp2);
                        BackQuestionObject("APTITUD1-C-0018", vAPTITUD1_C_0018);

                        String vAPTITUD1_C_0019 = RespuestasSeccionC(CPregunta19Resp1, CPregunta19Resp2);
                        BackQuestionObject("APTITUD1-C-0019", vAPTITUD1_C_0019);

                        String vAPTITUD1_C_0020 = RespuestasSeccionC(CPregunta20Resp1, CPregunta20Resp2);
                        BackQuestionObject("APTITUD1-C-0020", vAPTITUD1_C_0020);

                        String vAPTITUD1_C_0021 = RespuestasSeccionC(CPregunta21Resp1, CPregunta21Resp2);
                        BackQuestionObject("APTITUD1-C-0021", vAPTITUD1_C_0021);

                        String vAPTITUD1_C_0022 = RespuestasSeccionC(CPregunta22Resp1, CPregunta22Resp2);
                        BackQuestionObject("APTITUD1-C-0022", vAPTITUD1_C_0022);

                        String vAPTITUD1_C_0023 = RespuestasSeccionC(CPregunta23Resp1, CPregunta23Resp2);
                        BackQuestionObject("APTITUD1-C-0023", vAPTITUD1_C_0023);

                        String vAPTITUD1_C_0024 = RespuestasSeccionC(CPregunta24Resp1, CPregunta24Resp2);
                        BackQuestionObject("APTITUD1-C-0024", vAPTITUD1_C_0024);

                        String vAPTITUD1_C_0025 = RespuestasSeccionC(CPregunta25Resp1, CPregunta25Resp2);
                        BackQuestionObject("APTITUD1-C-0025", vAPTITUD1_C_0025);

                        String vAPTITUD1_C_0026 = RespuestasSeccionC(CPregunta26Resp1, CPregunta26Resp2);
                        BackQuestionObject("APTITUD1-C-0026", vAPTITUD1_C_0026);

                        String vAPTITUD1_C_0027 = RespuestasSeccionC(CPregunta27Resp1, CPregunta27Resp2);
                        BackQuestionObject("APTITUD1-C-0027", vAPTITUD1_C_0027);

                        String vAPTITUD1_C_0028 = RespuestasSeccionC(CPregunta28Resp1, CPregunta28Resp2);
                        BackQuestionObject("APTITUD1-C-0028", vAPTITUD1_C_0028);

                        String vAPTITUD1_C_0029 = RespuestasSeccionC(CPregunta29Resp1, CPregunta29Resp2);
                        BackQuestionObject("APTITUD1-C-0029", vAPTITUD1_C_0029);

                        String vAPTITUD1_C_0030 = RespuestasSeccionC(CPregunta30Resp1, CPregunta30Resp2);
                        BackQuestionObject("APTITUD1-C-0030", vAPTITUD1_C_0030);

                        break;

                    case 3:

                        //////////////////////////////////////////////////SECCION D/////////////////////////////////////////////////////////////////////

                        String vAPTITUD1_D_0001 = RespuestasSeccionD(DPregunta1Resp1, DPregunta1Resp2, DPregunta1Resp3, DPregunta1Resp4, DPregunta1Resp5);
                        BackQuestionObject("APTITUD1-D-0001", vAPTITUD1_D_0001);

                        String vAPTITUD1_D_0002 = RespuestasSeccionD(DPregunta2Resp1, DPregunta2Resp2, DPregunta2Resp3, DPregunta2Resp4, DPregunta2Resp5);
                        BackQuestionObject("APTITUD1-D-0002", vAPTITUD1_D_0002);

                        String vAPTITUD1_D_0003 = RespuestasSeccionD(DPregunta3Resp1, DPregunta3Resp2, DPregunta3Resp3, DPregunta3Resp4, DPregunta3Resp5);
                        BackQuestionObject("APTITUD1-D-0003", vAPTITUD1_D_0003);

                        String vAPTITUD1_D_0004 = RespuestasSeccionD(DPregunta4Resp1, DPregunta4Resp2, DPregunta4Resp3, DPregunta4Resp4, DPregunta4Resp5);
                        BackQuestionObject("APTITUD1-D-0004", vAPTITUD1_D_0004);

                        String vAPTITUD1_D_0005 = RespuestasSeccionD(DPregunta5Resp1, DPregunta5Resp2, DPregunta5Resp3, DPregunta5Resp4, DPregunta5Resp5);
                        BackQuestionObject("APTITUD1-D-0005", vAPTITUD1_D_0005);

                        String vAPTITUD1_D_0006 = RespuestasSeccionD(DPregunta6Resp1, DPregunta6Resp2, DPregunta6Resp3, DPregunta6Resp4, DPregunta6Resp5);
                        BackQuestionObject("APTITUD1-D-0006", vAPTITUD1_D_0006);

                        String vAPTITUD1_D_0007 = RespuestasSeccionD(DPregunta7Resp1, DPregunta7Resp2, DPregunta7Resp3, DPregunta7Resp4, DPregunta7Resp5);
                        BackQuestionObject("APTITUD1-D-0007", vAPTITUD1_D_0007);

                        String vAPTITUD1_D_0008 = RespuestasSeccionD(DPregunta8Resp1, DPregunta8Resp2, DPregunta8Resp3, DPregunta8Resp4, DPregunta8Resp5);
                        BackQuestionObject("APTITUD1-D-0008", vAPTITUD1_D_0008);

                        String vAPTITUD1_D_0009 = RespuestasSeccionD(DPregunta9Resp1, DPregunta9Resp2, DPregunta9Resp3, DPregunta9Resp4, DPregunta9Resp5);
                        BackQuestionObject("APTITUD1-D-0009", vAPTITUD1_D_0009);

                        String vAPTITUD1_D_0010 = RespuestasSeccionD(DPregunta10Resp1, DPregunta10Resp2, DPregunta10Resp3, DPregunta10Resp4, DPregunta10Resp5);
                        BackQuestionObject("APTITUD1-D-0010", vAPTITUD1_D_0010);

                        String vAPTITUD1_D_0011 = RespuestasSeccionD(DPregunta11Resp1, DPregunta11Resp2, DPregunta11Resp3, DPregunta11Resp4, DPregunta11Resp5);
                        BackQuestionObject("APTITUD1-D-0011", vAPTITUD1_D_0011);

                        String vAPTITUD1_D_0012 = RespuestasSeccionD(DPregunta12Resp1, DPregunta12Resp2, DPregunta12Resp3, DPregunta12Resp4, DPregunta12Resp5);
                        BackQuestionObject("APTITUD1-D-0012", vAPTITUD1_D_0012);

                        String vAPTITUD1_D_0013 = RespuestasSeccionD(DPregunta13Resp1, DPregunta13Resp2, DPregunta13Resp3, DPregunta13Resp4, DPregunta13Resp5);
                        BackQuestionObject("APTITUD1-D-0013", vAPTITUD1_D_0013);

                        String vAPTITUD1_D_0014 = RespuestasSeccionD(DPregunta14Resp1, DPregunta14Resp2, DPregunta14Resp3, DPregunta14Resp4, DPregunta14Resp5);
                        BackQuestionObject("APTITUD1-D-0014", vAPTITUD1_D_0014);

                        String vAPTITUD1_D_0015 = RespuestasSeccionD(DPregunta15Resp1, DPregunta15Resp2, DPregunta15Resp3, DPregunta15Resp4, DPregunta15Resp5);
                        BackQuestionObject("APTITUD1-D-0015", vAPTITUD1_D_0015);

                        String vAPTITUD1_D_0016 = RespuestasSeccionD(DPregunta16Resp1, DPregunta16Resp2, DPregunta16Resp3, DPregunta16Resp4, DPregunta16Resp5);
                        BackQuestionObject("APTITUD1-D-0016", vAPTITUD1_D_0016);

                        String vAPTITUD1_D_0017 = RespuestasSeccionD(DPregunta17Resp1, DPregunta17Resp2, DPregunta17Resp3, DPregunta17Resp4, DPregunta17Resp5);
                        BackQuestionObject("APTITUD1-D-0017", vAPTITUD1_D_0017);

                        String vAPTITUD1_D_0018 = RespuestasSeccionD(DPregunta18Resp1, DPregunta18Resp2, DPregunta18Resp3, DPregunta18Resp4, DPregunta18Resp5);
                        BackQuestionObject("APTITUD1-D-0018", vAPTITUD1_D_0018);

                        break;

                    case 4:

                        //////////////////////////////////////////////////SECCION E/////////////////////////////////////////////////////////////////////
                        String vAPTITUD1_E_0001 = EtxtPreg1Resp1.Value.ToString();
                        BackQuestionObject("APTITUD1-E-0001", vAPTITUD1_E_0001);

                        String vAPTITUD1_E_0002 = EtxtPreg2Resp2.Value.ToString();
                        BackQuestionObject("APTITUD1-E-0002", vAPTITUD1_E_0002);

                        String vAPTITUD1_E_0003 = EtxtPreg3Resp3.Value.ToString();
                        BackQuestionObject("APTITUD1-E-0003", vAPTITUD1_E_0003);

                        String vAPTITUD1_E_0004 = EtxtPreg4Resp4.Value.ToString();
                        BackQuestionObject("APTITUD1-E-0004", vAPTITUD1_E_0004);

                        String vAPTITUD1_E_0005 = EtxtPreg5Resp5.Value.ToString();
                        BackQuestionObject("APTITUD1-E-0005", vAPTITUD1_E_0005);

                        String vAPTITUD1_E_0006 = EtxtPreg6Resp6.Value.ToString();
                        BackQuestionObject("APTITUD1-E-0006", vAPTITUD1_E_0006);

                        String vAPTITUD1_E_0007 = EtxtPreg7Resp7.Value.ToString();
                        BackQuestionObject("APTITUD1-E-0007", vAPTITUD1_E_0007);

                        String vAPTITUD1_E_0008 = EtxtPreg8Resp8.Value.ToString();
                        BackQuestionObject("APTITUD1-E-0008", vAPTITUD1_E_0008);

                        String vAPTITUD1_E_0009 = EtxtPreg9Resp9.Value.ToString();
                        BackQuestionObject("APTITUD1-E-0009", vAPTITUD1_E_0009);

                        String vAPTITUD1_E_0010 = EtxtPreg10Resp10.Value.ToString();
                        BackQuestionObject("APTITUD1-E-0010", vAPTITUD1_E_0010);

                        String vAPTITUD1_E_0011 = EtxtPreg11Resp11.Value.ToString();
                        BackQuestionObject("APTITUD1-E-0011", vAPTITUD1_E_0011);

                        String vAPTITUD1_E_0012 = EtxtPreg12Resp12.Value.ToString();
                        BackQuestionObject("APTITUD1-E-0012", vAPTITUD1_E_0012);

                        break;

                    case 5:

                        //////////////////////////////////////////////////SECCION F/////////////////////////////////////////////////////////////////////

                        String vAPTITUD1_F_0001 = RespuestasSeccionF(FtxtPreg1Resp1, FtxtPreg1Resp2);
                        BackQuestionObject("APTITUD1-F-0001", vAPTITUD1_F_0001);

                        String vAPTITUD1_F_0002 = RespuestasSeccionF(FtxtPreg2Resp1, FtxtPreg2Resp2);
                        BackQuestionObject("APTITUD1-F-0002", vAPTITUD1_F_0002);

                        String vAPTITUD1_F_0003 = RespuestasSeccionF(FtxtPreg3Resp1, FtxtPreg3Resp2);
                        BackQuestionObject("APTITUD1-F-0003", vAPTITUD1_F_0003);

                        String vAPTITUD1_F_0004 = RespuestasSeccionF(FtxtPreg4Resp1, FtxtPreg4Resp2);
                        BackQuestionObject("APTITUD1-F-0004", vAPTITUD1_F_0004);

                        String vAPTITUD1_F_0005 = RespuestasSeccionF(FtxtPreg5Resp1, FtxtPreg5Resp2);
                        BackQuestionObject("APTITUD1-F-0005", vAPTITUD1_F_0005);

                        String vAPTITUD1_F_0006 = RespuestasSeccionF(FtxtPreg6Resp1, FtxtPreg6Resp2);
                        BackQuestionObject("APTITUD1-F-0006", vAPTITUD1_F_0006);

                        String vAPTITUD1_F_0007 = RespuestasSeccionF(FtxtPreg7Resp1, FtxtPreg7Resp2);
                        BackQuestionObject("APTITUD1-F-0007", vAPTITUD1_F_0007);

                        String vAPTITUD1_F_0008 = RespuestasSeccionF(FtxtPreg8Resp1, FtxtPreg8Resp2);
                        BackQuestionObject("APTITUD1-F-0008", vAPTITUD1_F_0008);

                        String vAPTITUD1_F_0009 = RespuestasSeccionF(FtxtPreg9Resp1, FtxtPreg9Resp2);
                        BackQuestionObject("APTITUD1-F-0009", vAPTITUD1_F_0009);

                        String vAPTITUD1_F_0010 = RespuestasSeccionF(FtxtPreg10Resp1, FtxtPreg10Resp2);
                        BackQuestionObject("APTITUD1-F-0010", vAPTITUD1_F_0010);

                        String vAPTITUD1_F_0011 = RespuestasSeccionF(FtxtPreg11Resp1, FtxtPreg11Resp2);
                        BackQuestionObject("APTITUD1-F-0011", vAPTITUD1_F_0011);

                        String vAPTITUD1_F_0012 = RespuestasSeccionF(FtxtPreg12Resp1, FtxtPreg12Resp2);
                        BackQuestionObject("APTITUD1-F-0012", vAPTITUD1_F_0012);

                        String vAPTITUD1_F_0013 = RespuestasSeccionF(FtxtPreg13Resp1, FtxtPreg13Resp2);
                        BackQuestionObject("APTITUD1-F-0013", vAPTITUD1_F_0013);

                        String vAPTITUD1_F_0014 = RespuestasSeccionF(FtxtPreg14Resp1, FtxtPreg14Resp2);
                        BackQuestionObject("APTITUD1-F-0014", vAPTITUD1_F_0014);

                        String vAPTITUD1_F_0015 = RespuestasSeccionF(FtxtPreg15Resp1, FtxtPreg15Resp2);
                        BackQuestionObject("APTITUD1-F-0015", vAPTITUD1_F_0015);

                        String vAPTITUD1_F_0016 = RespuestasSeccionF(FtxtPreg16Resp1, FtxtPreg16Resp2);
                        BackQuestionObject("APTITUD1-F-0016", vAPTITUD1_F_0016);

                        String vAPTITUD1_F_0017 = RespuestasSeccionF(FtxtPreg17Resp1, FtxtPreg17Resp2);
                        BackQuestionObject("APTITUD1-F-0017", vAPTITUD1_F_0017);

                        String vAPTITUD1_F_0018 = RespuestasSeccionF(FtxtPreg18Resp1, FtxtPreg18Resp2);
                        BackQuestionObject("APTITUD1-F-0018", vAPTITUD1_F_0018);

                        String vAPTITUD1_F_0019 = RespuestasSeccionF(FtxtPreg19Resp1, FtxtPreg19Resp2);
                        BackQuestionObject("APTITUD1-F-0019", vAPTITUD1_F_0019);

                        String vAPTITUD1_F_0020 = RespuestasSeccionF(FtxtPreg20Resp1, FtxtPreg20Resp2);
                        BackQuestionObject("APTITUD1-F-0020", vAPTITUD1_F_0020);
                        break;

                    case 6:
                        //////////////////////////////////////////////////SECCION G/////////////////////////////////////////////////////////////////////

                        String vAPTITUD1_G_0001 = RespuestasSeccionG(GbtnPreg1Resp1, GbtnPreg1Resp2, GbtnPreg1Resp3, GbtnPreg1Resp4);
                        BackQuestionObject("APTITUD1-G-0001", vAPTITUD1_G_0001);

                        String vAPTITUD1_G_0002 = RespuestasSeccionG(GbtnPreg2Resp1, GbtnPreg2Resp2, GbtnPreg2Resp3, GbtnPreg2Resp4);
                        BackQuestionObject("APTITUD1-G-0002", vAPTITUD1_G_0002);

                        String vAPTITUD1_G_0003 = RespuestasSeccionG(GbtnPreg3Resp1, GbtnPreg3Resp2, GbtnPreg3Resp3, GbtnPreg3Resp4);
                        BackQuestionObject("APTITUD1-G-0003", vAPTITUD1_G_0003);

                        String vAPTITUD1_G_0004 = RespuestasSeccionG(GbtnPreg4Resp1, GbtnPreg4Resp2, GbtnPreg4Resp3, GbtnPreg4Resp4);
                        BackQuestionObject("APTITUD1-G-0004", vAPTITUD1_G_0004);

                        String vAPTITUD1_G_0005 = RespuestasSeccionG(GbtnPreg5Resp1, GbtnPreg5Resp2, GbtnPreg5Resp3, GbtnPreg5Resp4);
                        BackQuestionObject("APTITUD1-G-0005", vAPTITUD1_G_0005);

                        String vAPTITUD1_G_0006 = RespuestasSeccionG(GbtnPreg6Resp1, GbtnPreg6Resp2, GbtnPreg6Resp3, GbtnPreg6Resp4);
                        BackQuestionObject("APTITUD1-G-0006", vAPTITUD1_G_0006);

                        String vAPTITUD1_G_0007 = RespuestasSeccionG(GbtnPreg7Resp1, GbtnPreg7Resp2, GbtnPreg7Resp3, GbtnPreg7Resp4);
                        BackQuestionObject("APTITUD1-G-0007", vAPTITUD1_G_0007);

                        String vAPTITUD1_G_0008 = RespuestasSeccionG(GbtnPreg8Resp1, GbtnPreg8Resp2, GbtnPreg8Resp3, GbtnPreg8Resp4);
                        BackQuestionObject("APTITUD1-G-0008", vAPTITUD1_G_0008);

                        String vAPTITUD1_G_0009 = RespuestasSeccionG(GbtnPreg9Resp1, GbtnPreg9Resp2, GbtnPreg9Resp3, GbtnPreg9Resp4);
                        BackQuestionObject("APTITUD1-G-0009", vAPTITUD1_G_0009);

                        String vAPTITUD1_G_0010 = RespuestasSeccionG(GbtnPreg10Resp1, GbtnPreg10Resp2, GbtnPreg10Resp3, GbtnPreg10Resp4);
                        BackQuestionObject("APTITUD1-G-0010", vAPTITUD1_G_0010);

                        String vAPTITUD1_G_0011 = RespuestasSeccionG(GbtnPreg11Resp1, GbtnPreg11Resp2, GbtnPreg11Resp3, GbtnPreg11Resp4);
                        BackQuestionObject("APTITUD1-G-0011", vAPTITUD1_G_0011);

                        String vAPTITUD1_G_0012 = RespuestasSeccionG(GbtnPreg12Resp1, GbtnPreg12Resp2, GbtnPreg12Resp3, GbtnPreg12Resp4);
                        BackQuestionObject("APTITUD1-G-0012", vAPTITUD1_G_0012);

                        String vAPTITUD1_G_0013 = RespuestasSeccionG(GbtnPreg13Resp1, GbtnPreg13Resp2, GbtnPreg13Resp3, GbtnPreg13Resp4);
                        BackQuestionObject("APTITUD1-G-0013", vAPTITUD1_G_0013);

                        String vAPTITUD1_G_0014 = RespuestasSeccionG(GbtnPreg14Resp1, GbtnPreg14Resp2, GbtnPreg14Resp3, GbtnPreg14Resp4);
                        BackQuestionObject("APTITUD1-G-0014", vAPTITUD1_G_0014);

                        String vAPTITUD1_G_0015 = RespuestasSeccionG(GbtnPreg15Resp1, GbtnPreg15Resp2, GbtnPreg15Resp3, GbtnPreg15Resp4);
                        BackQuestionObject("APTITUD1-G-0015", vAPTITUD1_G_0015);

                        String vAPTITUD1_G_0016 = RespuestasSeccionG(GbtnPreg16Resp1, GbtnPreg16Resp2, GbtnPreg16Resp3, GbtnPreg16Resp4);
                        BackQuestionObject("APTITUD1-G-0016", vAPTITUD1_G_0016);

                        String vAPTITUD1_G_0017 = RespuestasSeccionG(GbtnPreg17Resp1, GbtnPreg17Resp2, GbtnPreg17Resp3, GbtnPreg17Resp4);
                        BackQuestionObject("APTITUD1-G-0017", vAPTITUD1_G_0017);

                        String vAPTITUD1_G_0018 = RespuestasSeccionG(GbtnPreg18Resp1, GbtnPreg18Resp2, GbtnPreg18Resp3, GbtnPreg18Resp4);
                        BackQuestionObject("APTITUD1-G-0018", vAPTITUD1_G_0018);

                        String vAPTITUD1_G_0019 = RespuestasSeccionG(GbtnPreg19Resp1, GbtnPreg19Resp2, GbtnPreg19Resp3, GbtnPreg19Resp4);
                        BackQuestionObject("APTITUD1-G-0019", vAPTITUD1_G_0019);

                        String vAPTITUD1_G_0020 = RespuestasSeccionG(GbtnPreg20Resp1, GbtnPreg20Resp2, GbtnPreg20Resp3300, GbtnPreg20Resp4);
                        BackQuestionObject("APTITUD1-G-0020", vAPTITUD1_G_0020);

                        break;

                    case 7:

                        //////////////////////////////////////////////////SECCION H/////////////////////////////////////////////////////////////////////

                        String vAPTITUD1_H_0001 = RespuestasSeccionH(HbtnPreg1Resp1, HbtnPreg1Resp2);
                        BackQuestionObject("APTITUD1-H-0001", vAPTITUD1_H_0001);

                        String vAPTITUD1_H_0002 = RespuestasSeccionH(HbtnPreg2Resp1, HbtnPreg2Resp2);
                        BackQuestionObject("APTITUD1-H-0002", vAPTITUD1_H_0002);

                        String vAPTITUD1_H_0003 = RespuestasSeccionH(HbtnPreg3Resp1, HbtnPreg3Resp2);
                        BackQuestionObject("APTITUD1-H-0003", vAPTITUD1_H_0003);

                        String vAPTITUD1_H_0004 = RespuestasSeccionH(HbtnPreg4Resp1, HbtnPreg4Resp2);
                        BackQuestionObject("APTITUD1-H-0004", vAPTITUD1_H_0004);

                        String vAPTITUD1_H_0005 = RespuestasSeccionH(HbtnPreg5Resp1, HbtnPreg5Resp2);
                        BackQuestionObject("APTITUD1-H-0005", vAPTITUD1_H_0005);

                        String vAPTITUD1_H_0006 = RespuestasSeccionH(HbtnPreg6Resp1, HbtnPreg6Resp2);
                        BackQuestionObject("APTITUD1-H-0006", vAPTITUD1_H_0006);

                        String vAPTITUD1_H_0007 = RespuestasSeccionH(HbtnPreg7Resp1, HbtnPreg7Resp2);
                        BackQuestionObject("APTITUD1-H-0007", vAPTITUD1_H_0007);

                        String vAPTITUD1_H_0008 = RespuestasSeccionH(HbtnPreg8Resp1, HbtnPreg8Resp2);
                        BackQuestionObject("APTITUD1-H-0008", vAPTITUD1_H_0008);

                        String vAPTITUD1_H_0009 = RespuestasSeccionH(HbtnPreg9Resp1, HbtnPreg9Resp2);
                        BackQuestionObject("APTITUD1-H-0009", vAPTITUD1_H_0009);

                        String vAPTITUD1_H_0010 = RespuestasSeccionH(HbtnPreg10Resp1, HbtnPreg10Resp2);
                        BackQuestionObject("APTITUD1-H-0010", vAPTITUD1_H_0010);

                        String vAPTITUD1_H_0011 = RespuestasSeccionH(HbtnPreg11Resp1, HbtnPreg11Resp2);
                        BackQuestionObject("APTITUD1-H-0011", vAPTITUD1_H_0011);

                        String vAPTITUD1_H_0012 = RespuestasSeccionH(HbtnPreg12Resp1, HbtnPreg12Resp2);
                        BackQuestionObject("APTITUD1-H-0012", vAPTITUD1_H_0012);

                        String vAPTITUD1_H_0013 = RespuestasSeccionH(HbtnPreg13Resp1, HbtnPreg13Resp2);
                        BackQuestionObject("APTITUD1-H-0013", vAPTITUD1_H_0013);

                        String vAPTITUD1_H_0014 = RespuestasSeccionH(HbtnPreg14Resp1, HbtnPreg14Resp2);
                        BackQuestionObject("APTITUD1-H-0014", vAPTITUD1_H_0014);

                        String vAPTITUD1_H_0015 = RespuestasSeccionH(HbtnPreg15Resp1, HbtnPreg15Resp2);
                        BackQuestionObject("APTITUD1-H-0015", vAPTITUD1_H_0015);

                        String vAPTITUD1_H_0016 = RespuestasSeccionH(HbtnPreg16Resp1, HbtnPreg16Resp2);
                        BackQuestionObject("APTITUD1-H-0016", vAPTITUD1_H_0016);

                        String vAPTITUD1_H_0017 = RespuestasSeccionH(HbtnPreg17Resp1, HbtnPreg17Resp2);
                        BackQuestionObject("APTITUD1-H-0017", vAPTITUD1_H_0017);
                        break;

                    case 8:
                        //////////////////////////////////////////////////SECCION I/////////////////////////////////////////////////////////////////////
                        //IbtnPreg1Resp1

                        String vAPTITUD1_I_0001 = RespuestasSeccionI(IbtnPreg1Resp1, IbtnPreg1Resp2, IbtnPreg1Resp3, IbtnPreg1Resp4, IbtnPreg1Resp5);
                        BackQuestionObject("APTITUD1-I-0001", vAPTITUD1_I_0001);

                        String vAPTITUD1_I_0002 = RespuestasSeccionI(IbtnPreg2Resp1, IbtnPreg2Resp2, IbtnPreg2Resp3, IbtnPreg2Resp4, IbtnPreg2Resp5);
                        BackQuestionObject("APTITUD1-I-0002", vAPTITUD1_I_0002);

                        String vAPTITUD1_I_0003 = RespuestasSeccionI(IbtnPreg3Resp1, IbtnPreg3Resp2, IbtnPreg3Resp3, IbtnPreg3Resp4, IbtnPreg3Resp5);
                        BackQuestionObject("APTITUD1-I-0003", vAPTITUD1_I_0003);

                        String vAPTITUD1_I_0004 = RespuestasSeccionI(IbtnPreg4Resp1, IbtnPreg4Resp2, IbtnPreg4Resp3, IbtnPreg4Resp4, IbtnPreg4Resp5);
                        BackQuestionObject("APTITUD1-I-0004", vAPTITUD1_I_0004);

                        String vAPTITUD1_I_0005 = RespuestasSeccionI(IbtnPreg5Resp1, IbtnPreg5Resp2, IbtnPreg5Resp3, IbtnPreg5Resp4, IbtnPreg5Resp5);
                        BackQuestionObject("APTITUD1-I-0005", vAPTITUD1_I_0005);

                        String vAPTITUD1_I_0006 = RespuestasSeccionI(IbtnPreg6Resp1, IbtnPreg6Resp2, IbtnPreg6Resp3, IbtnPreg6Resp4, IbtnPreg6Resp5);
                        BackQuestionObject("APTITUD1-I-0006", vAPTITUD1_I_0006);

                        String vAPTITUD1_I_0007 = RespuestasSeccionI(IbtnPreg7Resp1, IbtnPreg7Resp2, IbtnPreg7Resp3, IbtnPreg7Resp4, IbtnPreg7Resp5);
                        BackQuestionObject("APTITUD1-I-0007", vAPTITUD1_I_0007);

                        String vAPTITUD1_I_0008 = RespuestasSeccionI(IbtnPreg8Resp1, IbtnPreg8Resp2, IbtnPreg8Resp3, IbtnPreg8Resp4, IbtnPreg8Resp5);
                        BackQuestionObject("APTITUD1-I-0008", vAPTITUD1_I_0008);

                        String vAPTITUD1_I_0009 = RespuestasSeccionI(IbtnPreg9Resp1, IbtnPreg9Resp2, IbtnPreg9Resp3, IbtnPreg9Resp4, IbtnPreg9Resp5);
                        BackQuestionObject("APTITUD1-I-0009", vAPTITUD1_I_0009);


                        String vAPTITUD1_I_0010 = RespuestasSeccionI(IbtnPreg10Resp1, IbtnPreg10Resp2, IbtnPreg10Resp3, IbtnPreg10Resp4, IbtnPreg10Resp5);
                        BackQuestionObject("APTITUD1-I-0010", vAPTITUD1_I_0010);

                        String vAPTITUD1_I_0011 = RespuestasSeccionI(IbtnPreg11Resp1, IbtnPreg11Resp2, IbtnPreg11Resp3, IbtnPreg11Resp4, IbtnPreg11Resp5);
                        BackQuestionObject("APTITUD1-I-0011", vAPTITUD1_I_0011);

                        String vAPTITUD1_I_0012 = RespuestasSeccionI(IbtnPreg12Resp1, IbtnPreg12Resp2, IbtnPreg12Resp3, IbtnPreg12Resp4, IbtnPreg12Resp5);
                        BackQuestionObject("APTITUD1-I-0012", vAPTITUD1_I_0012);

                        String vAPTITUD1_I_0013 = RespuestasSeccionI(IbtnPreg13Resp1, IbtnPreg13Resp2, IbtnPreg13Resp3, IbtnPreg13Resp4, IbtnPreg13Resp5);
                        BackQuestionObject("APTITUD1-I-0013", vAPTITUD1_I_0013);

                        String vAPTITUD1_I_0014 = RespuestasSeccionI(IbtnPreg14Resp1, IbtnPreg14Resp2, IbtnPreg14Resp3, IbtnPreg14Resp4, IbtnPreg14Resp5);
                        BackQuestionObject("APTITUD1-I-0014", vAPTITUD1_I_0014);

                        String vAPTITUD1_I_0015 = RespuestasSeccionI(IbtnPreg15Resp1, IbtnPreg15Resp2, IbtnPreg15Resp3, IbtnPreg15Resp4, IbtnPreg15Resp5);
                        BackQuestionObject("APTITUD1-I-0015", vAPTITUD1_I_0015);

                        String vAPTITUD1_I_0016 = RespuestasSeccionI(IbtnPreg16Resp1, IbtnPreg16Resp2, IbtnPreg16Resp3, IbtnPreg16Resp4, IbtnPreg16Resp5);
                        BackQuestionObject("APTITUD1-I-0016", vAPTITUD1_I_0016);

                        String vAPTITUD1_I_0017 = RespuestasSeccionI(IbtnPreg17Resp1, IbtnPreg17Resp2, IbtnPreg17Resp3, IbtnPreg17Resp4, IbtnPreg17Resp5);
                        BackQuestionObject("APTITUD1-I-0017", vAPTITUD1_I_0017);

                        String vAPTITUD1_I_0018 = RespuestasSeccionI(IbtnPreg18Resp1, IbtnPreg18Resp2, IbtnPreg18Resp3, IbtnPreg18Resp4, IbtnPreg18Resp5);
                        BackQuestionObject("APTITUD1-I-0018", vAPTITUD1_I_0018);

                        break;

                    case 9:


                        //////////////////////////////////////////////////SECCION J/////////////////////////////////////////////////////////////////////

                        String vAPTITUD1_J_0001 = RespuestasSeccionJ(JbtnPreg1Resp1);
                        BackQuestionObject("APTITUD1-J-0001", vAPTITUD1_J_0001);
                        String vAPTITUD1_J_0002 = RespuestasSeccionJ(JbtnPreg1Resp2);
                        BackQuestionObject("APTITUD1-J-0002", vAPTITUD1_J_0002);


                        String vAPTITUD1_J_0003 = RespuestasSeccionJ(JbtnPreg2Resp1);
                        BackQuestionObject("APTITUD1-J-0003", vAPTITUD1_J_0003);
                        String vAPTITUD1_J_0004 = RespuestasSeccionJ(JbtnPreg2Resp2);
                        BackQuestionObject("APTITUD1-J-0004", vAPTITUD1_J_0004);


                        String vAPTITUD1_J_0005 = RespuestasSeccionJ(JbtnPreg3Resp1);
                        BackQuestionObject("APTITUD1-J-0005", vAPTITUD1_J_0005);
                        String vAPTITUD1_J_0006 = RespuestasSeccionJ(JbtnPreg3Resp2);
                        BackQuestionObject("APTITUD1-J-0006", vAPTITUD1_J_0006);


                        String vAPTITUD1_J_0007 = RespuestasSeccionJ(JbtnPreg4Resp1);
                        BackQuestionObject("APTITUD1-J-0007", vAPTITUD1_J_0007);
                        String vAPTITUD1_J_0008 = RespuestasSeccionJ(JbtnPreg4Resp2);
                        BackQuestionObject("APTITUD1-J-0008", vAPTITUD1_J_0008);

                        String vAPTITUD1_J_0009 = RespuestasSeccionJ(JbtnPreg5Resp1);
                        BackQuestionObject("APTITUD1-J-0009", vAPTITUD1_J_0009);
                        String vAPTITUD1_J_0010 = RespuestasSeccionJ(JbtnPreg5Resp2);
                        BackQuestionObject("APTITUD1-J-0010", vAPTITUD1_J_0010);

                        String vAPTITUD1_J_0011 = RespuestasSeccionJ(JbtnPreg6Resp1);
                        BackQuestionObject("APTITUD1-J-0011", vAPTITUD1_J_0011);
                        String vAPTITUD1_J_0012 = RespuestasSeccionJ(JbtnPreg6Resp2);
                        BackQuestionObject("APTITUD1-J-0012", vAPTITUD1_J_0012);

                        String vAPTITUD1_J_0013 = RespuestasSeccionJ(JbtnPreg7Resp1);
                        BackQuestionObject("APTITUD1-J-0013", vAPTITUD1_J_0013);
                        String vAPTITUD1_J_0014 = RespuestasSeccionJ(JbtnPreg7Resp2);
                        BackQuestionObject("APTITUD1-J-0014", vAPTITUD1_J_0014);

                        String vAPTITUD1_J_0015 = RespuestasSeccionJ(JbtnPreg8Resp1);
                        BackQuestionObject("APTITUD1-J-0015", vAPTITUD1_J_0015);
                        String vAPTITUD1_J_0016 = RespuestasSeccionJ(JbtnPreg8Resp2);
                        BackQuestionObject("APTITUD1-J-0016", vAPTITUD1_J_0016);

                        String vAPTITUD1_J_0017 = RespuestasSeccionJ(JbtnPreg9Resp1);
                        BackQuestionObject("APTITUD1-J-0017", vAPTITUD1_J_0017);
                        String vAPTITUD1_J_0018 = RespuestasSeccionJ(JbtnPreg9Resp2);
                        BackQuestionObject("APTITUD1-J-0018", vAPTITUD1_J_0018);

                        String vAPTITUD1_J_0019 = RespuestasSeccionJ(JbtnPreg10Resp1);
                        BackQuestionObject("APTITUD1-J-0019", vAPTITUD1_J_0019);
                        String vAPTITUD1_J_0020 = RespuestasSeccionJ(JbtnPreg10Resp2);
                        BackQuestionObject("APTITUD1-J-0020", vAPTITUD1_J_0020);

                        String vAPTITUD1_J_0021 = RespuestasSeccionJ(JbtnPreg11Resp1);
                        BackQuestionObject("APTITUD1-J-0021", vAPTITUD1_J_0021);
                        String vAPTITUD1_J_0022 = RespuestasSeccionJ(JbtnPreg11Resp2);
                        BackQuestionObject("APTITUD1-J-0022", vAPTITUD1_J_0022);
                        break;
                }

                var vXelements = vRespuestas.Select(x =>
                                                     new XElement("RESPUESTA",
                                                     new XAttribute("ID_CUESTIONARIO_PREGUNTA", x.ID_CUESTIONARIO_PREGUNTA),
                                                     new XAttribute("ID_PREGUNTA", x.ID_PREGUNTA),
                                                     new XAttribute("NB_PREGUNTA", x.NB_PREGUNTA),
                                                     new XAttribute("NB_RESPUESTA", x.NB_RESPUESTA),
                                                     new XAttribute("NO_VALOR_RESPUESTA", x.NO_VALOR_RESPUESTA),
                                                     new XAttribute("CL_VARIABLE", x.CL_PREGUNTA)
                                          ));
                XElement RESPUESTAS =
                new XElement("RESPUESTAS", vXelements
                );

                CuestionarioPreguntaNegocio nCustionarioPregunta = new CuestionarioPreguntaNegocio();
                PruebasNegocio nKprueba = new PruebasNegocio();
                var vObjetoPrueba = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();

                if (vObjetoPrueba != null)
                {
                    vPrueba = vPrueba + (vseccion + 1).ToString();
                   // String CallBackFunction = "";
                    var vSeccionInicia = new E_PRUEBA_TIEMPO();
                    if (vseccion != (vSeccionesPrueba.Count - 1))
                    {
                        //CallBackFunction = "updateTimer('" + (vseccion + 1) + "','click')";
                        vSeccionInicia = vSeccionesPrueba.ElementAt(vseccion + 1);
                        vSeccionInicia.FE_INICIO = DateTime.Now;
                        vSeccionInicia.CL_ESTADO = E_ESTADO_PRUEBA.INICIADA.ToString();
                    }
                    else
                    {
                        SPE_OBTIENE_K_PRUEBA_Result vPruebaTerminada = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();
                        vPruebaTerminada.FE_TERMINO = DateTime.Now;
                        vPruebaTerminada.CL_ESTADO = E_ESTADO_PRUEBA.TERMINADA.ToString();
                        vPruebaTerminada.NB_TIPO_PRUEBA = "APLICACION";

                        E_RESULTADO vResultadoTestEnd = nKprueba.InsertaActualiza_K_PRUEBA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
                        vSeccionInicia = vSeccionesPrueba.ElementAt(vseccion);
                        vSeccionInicia.FE_INICIO = DateTime.Now;
                        vSeccionInicia.CL_ESTADO = E_ESTADO_PRUEBA.INICIADA.ToString();
                       // CallBackFunction = "CloseTest";
                    }

                    if (Request.QueryString["MOD"] != null)
                    {
                        E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vObjetoPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: vPrueba, usuario: vClUsuario, programa: vNbPrograma);
                        string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                        UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
                        PintarRespuestasMentalI();
                    }
                    else
                    {
                        E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vObjetoPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: vPrueba, usuario: vClUsuario, programa: vNbPrograma);
                      //  string vMensaje = instrucciones(vseccion + 1);
                        //int vHeight = HeightRadAlert(vseccion + 1);
                      //  vRadAlertAltura = HeightRadAlert(vseccion + 1);

                        //UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, vHeight, CallBackFunction);
                        E_RESULTADO vResultadoSeccion = nKprueba.InsertaActualiza_K_PRUEBA_SECCION(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), v_k_prueba: vSeccionInicia, usuario: vClUsuario, programa: vNbPrograma);
                        mpgActitudMentalI.SelectedIndex = vseccion + 1;
                        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "MyScript", CallBackFunction, true);

                        //if (mpgActitudMentalI.SelectedIndex == 9)
                        //{
                        //    RadButton1.Text = "Guardar";
                        //}

                    }

                }
            }
        }

        public void EditTest(int vseccion)
        {
            string vPrueba = "APTITUD1";
            CuestionariosNegocio nPreguntas = new CuestionariosNegocio();
            var preguntas = nPreguntas.Obtener_K_PREGUNTA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken);
            var filtroPreguntas = preguntas.Where(oh => oh.CL_PREGUNTA.StartsWith("APTITUD1-" + TraerLetraSeccion(vseccion))).ToList();

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
                        String vAPTITUD1_A_0001 = BackSelectedCheckBox(APregunta1Resp1, APregunta1Resp2, APregunta1Resp3, APregunta1Resp4);
                        BackQuestionObject("APTITUD1-A-0001", vAPTITUD1_A_0001);

                        String vAPTITUD1_A_0002 = BackSelectedCheckBox(APregunta2Resp1, APregunta2Resp2, APregunta2Resp3, APregunta2Resp4);
                        BackQuestionObject("APTITUD1-A-0002", vAPTITUD1_A_0002);

                        String vAPTITUD1_A_0003 = BackSelectedCheckBox(APregunta3Resp1, APregunta3Resp2, APregunta3Resp3, APregunta3Resp4);
                        BackQuestionObject("APTITUD1-A-0003", vAPTITUD1_A_0003);

                        String vAPTITUD1_A_0004 = BackSelectedCheckBox(APregunta4Resp1, APregunta4Resp2, APregunta4Resp3, APregunta4Resp4);
                        BackQuestionObject("APTITUD1-A-0004", vAPTITUD1_A_0004);

                        String vAPTITUD1_A_0005 = BackSelectedCheckBox(APregunta5Resp1, APregunta5Resp2, APregunta5Resp3, APregunta5Resp4);
                        BackQuestionObject("APTITUD1-A-0005", vAPTITUD1_A_0005);

                        String vAPTITUD1_A_0006 = BackSelectedCheckBox(APregunta6Resp1, APregunta6Resp2, APregunta6Resp3, APregunta6Resp4);
                        BackQuestionObject("APTITUD1-A-0006", vAPTITUD1_A_0006);

                        String vAPTITUD1_A_0007 = BackSelectedCheckBox(APregunta7Resp1, APregunta7Resp2, APregunta7Resp3, APregunta7Resp4);
                        BackQuestionObject("APTITUD1-A-0007", vAPTITUD1_A_0007);

                        String vAPTITUD1_A_0008 = BackSelectedCheckBox(APregunta8Resp1, APregunta8Resp2, APregunta8Resp3, APregunta8Resp4);
                        BackQuestionObject("APTITUD1-A-0008", vAPTITUD1_A_0008);

                        String vAPTITUD1_A_0009 = BackSelectedCheckBox(APregunta9Resp1, APregunta9Resp2, APregunta9Resp3, APregunta9Resp4);
                        BackQuestionObject("APTITUD1-A-0009", vAPTITUD1_A_0009);

                        String vAPTITUD1_A_0010 = BackSelectedCheckBox(APregunta10Resp1, APregunta10Resp2, APregunta10Resp3, APregunta10Resp4);
                        BackQuestionObject("APTITUD1-A-0010", vAPTITUD1_A_0010);

                        String vAPTITUD1_A_0011 = BackSelectedCheckBox(APregunta11Resp1, APregunta11Resp2, APregunta11Resp3, APregunta11Resp4);
                        BackQuestionObject("APTITUD1-A-0011", vAPTITUD1_A_0011);

                        String vAPTITUD1_A_0012 = BackSelectedCheckBox(APregunta12Resp1, APregunta12Resp2, APregunta12Resp3, APregunta12Resp4);
                        BackQuestionObject("APTITUD1-A-0012", vAPTITUD1_A_0012);

                        String vAPTITUD1_A_0013 = BackSelectedCheckBox(APregunta13Resp1, APregunta13Resp2, APregunta13Resp3, APregunta13Resp4);
                        BackQuestionObject("APTITUD1-A-0013", vAPTITUD1_A_0013);

                        String vAPTITUD1_A_0014 = BackSelectedCheckBox(APregunta14Resp1, APregunta14Resp2, APregunta14Resp3, APregunta14Resp4);
                        BackQuestionObject("APTITUD1-A-0014", vAPTITUD1_A_0014);

                        String vAPTITUD1_A_0015 = BackSelectedCheckBox(APregunta15Resp1, APregunta15Resp2, APregunta15Resp3, APregunta15Resp4);
                        BackQuestionObject("APTITUD1-A-0015", vAPTITUD1_A_0015);

                        String vAPTITUD1_A_0016 = BackSelectedCheckBox(APregunta16Resp1, APregunta16Resp2, APregunta16Resp3, APregunta16Resp4);
                        BackQuestionObject("APTITUD1-A-0016", vAPTITUD1_A_0016);

                        break;

                    case 1:
                        //////////////////////////////////////////////////SECCION B/////////////////////////////////////////////////////////////////////

                        String vAPTITUD1_B_0001 = RespuestasSeccionB(BPregunta1Resp1, BPregunta1Resp2, BPregunta1Resp3);
                        BackQuestionObject("APTITUD1-B-0001", vAPTITUD1_B_0001);

                        String vAPTITUD1_B_0002 = RespuestasSeccionB(BPregunta2Resp1, BPregunta2Resp2, BPregunta2Resp3);
                        BackQuestionObject("APTITUD1-B-0002", vAPTITUD1_B_0002);

                        String vAPTITUD1_B_0003 = RespuestasSeccionB(BPregunta3Resp, BPregunta3Resp2, BPregunta3Resp3);
                        BackQuestionObject("APTITUD1-B-0003", vAPTITUD1_B_0003);

                        String vAPTITUD1_B_0004 = RespuestasSeccionB(BPregunta4Resp1, BPregunta4Resp2, BPregunta4Resp3);
                        BackQuestionObject("APTITUD1-B-0004", vAPTITUD1_B_0004);

                        String vAPTITUD1_B_0005 = RespuestasSeccionB(BPregunta5Resp1, BPregunta5Resp2, BPregunta5Resp3);
                        BackQuestionObject("APTITUD1-B-0005", vAPTITUD1_B_0005);

                        String vAPTITUD1_B_0006 = RespuestasSeccionB(BPregunta6Resp1, BPregunta6Resp2, BPregunta6Resp3);
                        BackQuestionObject("APTITUD1-B-0006", vAPTITUD1_B_0006);

                        String vAPTITUD1_B_0007 = RespuestasSeccionB(BPregunta7Resp1, BPregunta7Resp2, BPregunta7Resp3);
                        BackQuestionObject("APTITUD1-B-0007", vAPTITUD1_B_0007);

                        String vAPTITUD1_B_0008 = RespuestasSeccionB(BPregunta8Resp1, BPregunta8Resp2, BPregunta8Resp3);
                        BackQuestionObject("APTITUD1-B-0008", vAPTITUD1_B_0008);

                        String vAPTITUD1_B_0009 = RespuestasSeccionB(BPregunta9Resp1, BPregunta9Resp2, BPregunta9Resp3);
                        BackQuestionObject("APTITUD1-B-0009", vAPTITUD1_B_0009);

                        String vAPTITUD1_B_0010 = RespuestasSeccionB(BPregunta10Resp1, BPregunta10Resp2, BPregunta10Resp3);
                        BackQuestionObject("APTITUD1-B-0010", vAPTITUD1_B_0010);

                        String vAPTITUD1_B_0011 = RespuestasSeccionB(BPregunta11Resp1, BPregunta11Resp2, BPregunta11Resp3);
                        BackQuestionObject("APTITUD1-B-0011", vAPTITUD1_B_0011);

                        break;

                    case 2:
                        //////////////////////////////////////////////////SECCION C/////////////////////////////////////////////////////////////////////
                        String vAPTITUD1_C_0001 = RespuestasSeccionC(CPregunta1Resp1, CPregunta1Resp2);
                        BackQuestionObject("APTITUD1-C-0001", vAPTITUD1_C_0001);

                        String vAPTITUD1_C_0002 = RespuestasSeccionC(CPregunta2Resp1, CPregunta2Resp2);
                        BackQuestionObject("APTITUD1-C-0002", vAPTITUD1_C_0002);

                        String vAPTITUD1_C_0003 = RespuestasSeccionC(CPregunta3Resp1, CPregunta3Resp2);
                        BackQuestionObject("APTITUD1-C-0003", vAPTITUD1_C_0003);

                        String vAPTITUD1_C_0004 = RespuestasSeccionC(CPregunta4Resp1, CPregunta4Resp2);
                        BackQuestionObject("APTITUD1-C-0004", vAPTITUD1_C_0004);

                        String vAPTITUD1_C_0005 = RespuestasSeccionC(CPregunta5Resp1, CPregunta5Resp2);
                        BackQuestionObject("APTITUD1-C-0005", vAPTITUD1_C_0005);

                        String vAPTITUD1_C_0006 = RespuestasSeccionC(CPregunta6Resp1, CPregunta6Resp2);
                        BackQuestionObject("APTITUD1-C-0006", vAPTITUD1_C_0006);

                        String vAPTITUD1_C_0007 = RespuestasSeccionC(CPregunta7Resp1, CPregunta7Resp2);
                        BackQuestionObject("APTITUD1-C-0007", vAPTITUD1_C_0007);

                        String vAPTITUD1_C_0008 = RespuestasSeccionC(CPregunta8Resp1, CPregunta8Resp2);
                        BackQuestionObject("APTITUD1-C-0008", vAPTITUD1_C_0008);

                        String vAPTITUD1_C_0009 = RespuestasSeccionC(CPregunta9Resp1, CPregunta9Resp2);
                        BackQuestionObject("APTITUD1-C-0009", vAPTITUD1_C_0009);

                        String vAPTITUD1_C_0010 = RespuestasSeccionC(CPregunta10Resp1, CPregunta10Resp2);
                        BackQuestionObject("APTITUD1-C-0010", vAPTITUD1_C_0010);

                        String vAPTITUD1_C_0011 = RespuestasSeccionC(CPregunta11Resp1, CPregunta11Resp2);
                        BackQuestionObject("APTITUD1-C-0011", vAPTITUD1_C_0011);

                        String vAPTITUD1_C_0012 = RespuestasSeccionC(CPregunta12Resp1, CPregunta12Resp2);
                        BackQuestionObject("APTITUD1-C-0012", vAPTITUD1_C_0012);

                        String vAPTITUD1_C_0013 = RespuestasSeccionC(CPregunta13Resp1, CPregunta13Resp2);
                        BackQuestionObject("APTITUD1-C-0013", vAPTITUD1_C_0013);

                        String vAPTITUD1_C_0014 = RespuestasSeccionC(CPregunta14Resp1, CPregunta14Resp2);
                        BackQuestionObject("APTITUD1-C-0014", vAPTITUD1_C_0014);

                        String vAPTITUD1_C_0015 = RespuestasSeccionC(CPregunta15Resp1, CPregunta15Resp2);
                        BackQuestionObject("APTITUD1-C-0015", vAPTITUD1_C_0015);

                        String vAPTITUD1_C_0016 = RespuestasSeccionC(CPregunta16Resp1, CPregunta16Resp2);
                        BackQuestionObject("APTITUD1-C-0016", vAPTITUD1_C_0016);

                        String vAPTITUD1_C_0017 = RespuestasSeccionC(CPregunta17Resp1, CPregunta17Resp2);
                        BackQuestionObject("APTITUD1-C-0017", vAPTITUD1_C_0017);

                        String vAPTITUD1_C_0018 = RespuestasSeccionC(CPregunta18Resp1, CPregunta18Resp2);
                        BackQuestionObject("APTITUD1-C-0018", vAPTITUD1_C_0018);

                        String vAPTITUD1_C_0019 = RespuestasSeccionC(CPregunta19Resp1, CPregunta19Resp2);
                        BackQuestionObject("APTITUD1-C-0019", vAPTITUD1_C_0019);

                        String vAPTITUD1_C_0020 = RespuestasSeccionC(CPregunta20Resp1, CPregunta20Resp2);
                        BackQuestionObject("APTITUD1-C-0020", vAPTITUD1_C_0020);

                        String vAPTITUD1_C_0021 = RespuestasSeccionC(CPregunta21Resp1, CPregunta21Resp2);
                        BackQuestionObject("APTITUD1-C-0021", vAPTITUD1_C_0021);

                        String vAPTITUD1_C_0022 = RespuestasSeccionC(CPregunta22Resp1, CPregunta22Resp2);
                        BackQuestionObject("APTITUD1-C-0022", vAPTITUD1_C_0022);

                        String vAPTITUD1_C_0023 = RespuestasSeccionC(CPregunta23Resp1, CPregunta23Resp2);
                        BackQuestionObject("APTITUD1-C-0023", vAPTITUD1_C_0023);

                        String vAPTITUD1_C_0024 = RespuestasSeccionC(CPregunta24Resp1, CPregunta24Resp2);
                        BackQuestionObject("APTITUD1-C-0024", vAPTITUD1_C_0024);

                        String vAPTITUD1_C_0025 = RespuestasSeccionC(CPregunta25Resp1, CPregunta25Resp2);
                        BackQuestionObject("APTITUD1-C-0025", vAPTITUD1_C_0025);

                        String vAPTITUD1_C_0026 = RespuestasSeccionC(CPregunta26Resp1, CPregunta26Resp2);
                        BackQuestionObject("APTITUD1-C-0026", vAPTITUD1_C_0026);

                        String vAPTITUD1_C_0027 = RespuestasSeccionC(CPregunta27Resp1, CPregunta27Resp2);
                        BackQuestionObject("APTITUD1-C-0027", vAPTITUD1_C_0027);

                        String vAPTITUD1_C_0028 = RespuestasSeccionC(CPregunta28Resp1, CPregunta28Resp2);
                        BackQuestionObject("APTITUD1-C-0028", vAPTITUD1_C_0028);

                        String vAPTITUD1_C_0029 = RespuestasSeccionC(CPregunta29Resp1, CPregunta29Resp2);
                        BackQuestionObject("APTITUD1-C-0029", vAPTITUD1_C_0029);

                        String vAPTITUD1_C_0030 = RespuestasSeccionC(CPregunta30Resp1, CPregunta30Resp2);
                        BackQuestionObject("APTITUD1-C-0030", vAPTITUD1_C_0030);

                        break;

                    case 3:

                        //////////////////////////////////////////////////SECCION D/////////////////////////////////////////////////////////////////////

                        String vAPTITUD1_D_0001 = RespuestasSeccionD(DPregunta1Resp1, DPregunta1Resp2, DPregunta1Resp3, DPregunta1Resp4, DPregunta1Resp5);
                        BackQuestionObject("APTITUD1-D-0001", vAPTITUD1_D_0001);

                        String vAPTITUD1_D_0002 = RespuestasSeccionD(DPregunta2Resp1, DPregunta2Resp2, DPregunta2Resp3, DPregunta2Resp4, DPregunta2Resp5);
                        BackQuestionObject("APTITUD1-D-0002", vAPTITUD1_D_0002);

                        String vAPTITUD1_D_0003 = RespuestasSeccionD(DPregunta3Resp1, DPregunta3Resp2, DPregunta3Resp3, DPregunta3Resp4, DPregunta3Resp5);
                        BackQuestionObject("APTITUD1-D-0003", vAPTITUD1_D_0003);

                        String vAPTITUD1_D_0004 = RespuestasSeccionD(DPregunta4Resp1, DPregunta4Resp2, DPregunta4Resp3, DPregunta4Resp4, DPregunta4Resp5);
                        BackQuestionObject("APTITUD1-D-0004", vAPTITUD1_D_0004);

                        String vAPTITUD1_D_0005 = RespuestasSeccionD(DPregunta5Resp1, DPregunta5Resp2, DPregunta5Resp3, DPregunta5Resp4, DPregunta5Resp5);
                        BackQuestionObject("APTITUD1-D-0005", vAPTITUD1_D_0005);

                        String vAPTITUD1_D_0006 = RespuestasSeccionD(DPregunta6Resp1, DPregunta6Resp2, DPregunta6Resp3, DPregunta6Resp4, DPregunta6Resp5);
                        BackQuestionObject("APTITUD1-D-0006", vAPTITUD1_D_0006);

                        String vAPTITUD1_D_0007 = RespuestasSeccionD(DPregunta7Resp1, DPregunta7Resp2, DPregunta7Resp3, DPregunta7Resp4, DPregunta7Resp5);
                        BackQuestionObject("APTITUD1-D-0007", vAPTITUD1_D_0007);

                        String vAPTITUD1_D_0008 = RespuestasSeccionD(DPregunta8Resp1, DPregunta8Resp2, DPregunta8Resp3, DPregunta8Resp4, DPregunta8Resp5);
                        BackQuestionObject("APTITUD1-D-0008", vAPTITUD1_D_0008);

                        String vAPTITUD1_D_0009 = RespuestasSeccionD(DPregunta9Resp1, DPregunta9Resp2, DPregunta9Resp3, DPregunta9Resp4, DPregunta9Resp5);
                        BackQuestionObject("APTITUD1-D-0009", vAPTITUD1_D_0009);

                        String vAPTITUD1_D_0010 = RespuestasSeccionD(DPregunta10Resp1, DPregunta10Resp2, DPregunta10Resp3, DPregunta10Resp4, DPregunta10Resp5);
                        BackQuestionObject("APTITUD1-D-0010", vAPTITUD1_D_0010);

                        String vAPTITUD1_D_0011 = RespuestasSeccionD(DPregunta11Resp1, DPregunta11Resp2, DPregunta11Resp3, DPregunta11Resp4, DPregunta11Resp5);
                        BackQuestionObject("APTITUD1-D-0011", vAPTITUD1_D_0011);

                        String vAPTITUD1_D_0012 = RespuestasSeccionD(DPregunta12Resp1, DPregunta12Resp2, DPregunta12Resp3, DPregunta12Resp4, DPregunta12Resp5);
                        BackQuestionObject("APTITUD1-D-0012", vAPTITUD1_D_0012);

                        String vAPTITUD1_D_0013 = RespuestasSeccionD(DPregunta13Resp1, DPregunta13Resp2, DPregunta13Resp3, DPregunta13Resp4, DPregunta13Resp5);
                        BackQuestionObject("APTITUD1-D-0013", vAPTITUD1_D_0013);

                        String vAPTITUD1_D_0014 = RespuestasSeccionD(DPregunta14Resp1, DPregunta14Resp2, DPregunta14Resp3, DPregunta14Resp4, DPregunta14Resp5);
                        BackQuestionObject("APTITUD1-D-0014", vAPTITUD1_D_0014);

                        String vAPTITUD1_D_0015 = RespuestasSeccionD(DPregunta15Resp1, DPregunta15Resp2, DPregunta15Resp3, DPregunta15Resp4, DPregunta15Resp5);
                        BackQuestionObject("APTITUD1-D-0015", vAPTITUD1_D_0015);

                        String vAPTITUD1_D_0016 = RespuestasSeccionD(DPregunta16Resp1, DPregunta16Resp2, DPregunta16Resp3, DPregunta16Resp4, DPregunta16Resp5);
                        BackQuestionObject("APTITUD1-D-0016", vAPTITUD1_D_0016);

                        String vAPTITUD1_D_0017 = RespuestasSeccionD(DPregunta17Resp1, DPregunta17Resp2, DPregunta17Resp3, DPregunta17Resp4, DPregunta17Resp5);
                        BackQuestionObject("APTITUD1-D-0017", vAPTITUD1_D_0017);

                        String vAPTITUD1_D_0018 = RespuestasSeccionD(DPregunta18Resp1, DPregunta18Resp2, DPregunta18Resp3, DPregunta18Resp4, DPregunta18Resp5);
                        BackQuestionObject("APTITUD1-D-0018", vAPTITUD1_D_0018);

                        break;

                    case 4:

                        //////////////////////////////////////////////////SECCION E/////////////////////////////////////////////////////////////////////
                        String vAPTITUD1_E_0001 = EtxtPreg1Resp1.Value.ToString();
                        BackQuestionObject("APTITUD1-E-0001", vAPTITUD1_E_0001);

                        String vAPTITUD1_E_0002 = EtxtPreg2Resp2.Value.ToString();
                        BackQuestionObject("APTITUD1-E-0002", vAPTITUD1_E_0002);

                        String vAPTITUD1_E_0003 = EtxtPreg3Resp3.Value.ToString();
                        BackQuestionObject("APTITUD1-E-0003", vAPTITUD1_E_0003);

                        String vAPTITUD1_E_0004 = EtxtPreg4Resp4.Value.ToString();
                        BackQuestionObject("APTITUD1-E-0004", vAPTITUD1_E_0004);

                        String vAPTITUD1_E_0005 = EtxtPreg5Resp5.Value.ToString();
                        BackQuestionObject("APTITUD1-E-0005", vAPTITUD1_E_0005);

                        String vAPTITUD1_E_0006 = EtxtPreg6Resp6.Value.ToString();
                        BackQuestionObject("APTITUD1-E-0006", vAPTITUD1_E_0006);

                        String vAPTITUD1_E_0007 = EtxtPreg7Resp7.Value.ToString();
                        BackQuestionObject("APTITUD1-E-0007", vAPTITUD1_E_0007);

                        String vAPTITUD1_E_0008 = EtxtPreg8Resp8.Value.ToString();
                        BackQuestionObject("APTITUD1-E-0008", vAPTITUD1_E_0008);

                        String vAPTITUD1_E_0009 = EtxtPreg9Resp9.Value.ToString();
                        BackQuestionObject("APTITUD1-E-0009", vAPTITUD1_E_0009);

                        String vAPTITUD1_E_0010 = EtxtPreg10Resp10.Value.ToString();
                        BackQuestionObject("APTITUD1-E-0010", vAPTITUD1_E_0010);

                        String vAPTITUD1_E_0011 = EtxtPreg11Resp11.Value.ToString();
                        BackQuestionObject("APTITUD1-E-0011", vAPTITUD1_E_0011);

                        String vAPTITUD1_E_0012 = EtxtPreg12Resp12.Value.ToString();
                        BackQuestionObject("APTITUD1-E-0012", vAPTITUD1_E_0012);

                        break;

                    case 5:

                        //////////////////////////////////////////////////SECCION F/////////////////////////////////////////////////////////////////////

                        String vAPTITUD1_F_0001 = RespuestasSeccionF(FtxtPreg1Resp1, FtxtPreg1Resp2);
                        BackQuestionObject("APTITUD1-F-0001", vAPTITUD1_F_0001);

                        String vAPTITUD1_F_0002 = RespuestasSeccionF(FtxtPreg2Resp1, FtxtPreg2Resp2);
                        BackQuestionObject("APTITUD1-F-0002", vAPTITUD1_F_0002);

                        String vAPTITUD1_F_0003 = RespuestasSeccionF(FtxtPreg3Resp1, FtxtPreg3Resp2);
                        BackQuestionObject("APTITUD1-F-0003", vAPTITUD1_F_0003);

                        String vAPTITUD1_F_0004 = RespuestasSeccionF(FtxtPreg4Resp1, FtxtPreg4Resp2);
                        BackQuestionObject("APTITUD1-F-0004", vAPTITUD1_F_0004);

                        String vAPTITUD1_F_0005 = RespuestasSeccionF(FtxtPreg5Resp1, FtxtPreg5Resp2);
                        BackQuestionObject("APTITUD1-F-0005", vAPTITUD1_F_0005);

                        String vAPTITUD1_F_0006 = RespuestasSeccionF(FtxtPreg6Resp1, FtxtPreg6Resp2);
                        BackQuestionObject("APTITUD1-F-0006", vAPTITUD1_F_0006);

                        String vAPTITUD1_F_0007 = RespuestasSeccionF(FtxtPreg7Resp1, FtxtPreg7Resp2);
                        BackQuestionObject("APTITUD1-F-0007", vAPTITUD1_F_0007);

                        String vAPTITUD1_F_0008 = RespuestasSeccionF(FtxtPreg8Resp1, FtxtPreg8Resp2);
                        BackQuestionObject("APTITUD1-F-0008", vAPTITUD1_F_0008);

                        String vAPTITUD1_F_0009 = RespuestasSeccionF(FtxtPreg9Resp1, FtxtPreg9Resp2);
                        BackQuestionObject("APTITUD1-F-0009", vAPTITUD1_F_0009);

                        String vAPTITUD1_F_0010 = RespuestasSeccionF(FtxtPreg10Resp1, FtxtPreg10Resp2);
                        BackQuestionObject("APTITUD1-F-0010", vAPTITUD1_F_0010);

                        String vAPTITUD1_F_0011 = RespuestasSeccionF(FtxtPreg11Resp1, FtxtPreg11Resp2);
                        BackQuestionObject("APTITUD1-F-0011", vAPTITUD1_F_0011);

                        String vAPTITUD1_F_0012 = RespuestasSeccionF(FtxtPreg12Resp1, FtxtPreg12Resp2);
                        BackQuestionObject("APTITUD1-F-0012", vAPTITUD1_F_0012);

                        String vAPTITUD1_F_0013 = RespuestasSeccionF(FtxtPreg13Resp1, FtxtPreg13Resp2);
                        BackQuestionObject("APTITUD1-F-0013", vAPTITUD1_F_0013);

                        String vAPTITUD1_F_0014 = RespuestasSeccionF(FtxtPreg14Resp1, FtxtPreg14Resp2);
                        BackQuestionObject("APTITUD1-F-0014", vAPTITUD1_F_0014);

                        String vAPTITUD1_F_0015 = RespuestasSeccionF(FtxtPreg15Resp1, FtxtPreg15Resp2);
                        BackQuestionObject("APTITUD1-F-0015", vAPTITUD1_F_0015);

                        String vAPTITUD1_F_0016 = RespuestasSeccionF(FtxtPreg16Resp1, FtxtPreg16Resp2);
                        BackQuestionObject("APTITUD1-F-0016", vAPTITUD1_F_0016);

                        String vAPTITUD1_F_0017 = RespuestasSeccionF(FtxtPreg17Resp1, FtxtPreg17Resp2);
                        BackQuestionObject("APTITUD1-F-0017", vAPTITUD1_F_0017);

                        String vAPTITUD1_F_0018 = RespuestasSeccionF(FtxtPreg18Resp1, FtxtPreg18Resp2);
                        BackQuestionObject("APTITUD1-F-0018", vAPTITUD1_F_0018);

                        String vAPTITUD1_F_0019 = RespuestasSeccionF(FtxtPreg19Resp1, FtxtPreg19Resp2);
                        BackQuestionObject("APTITUD1-F-0019", vAPTITUD1_F_0019);

                        String vAPTITUD1_F_0020 = RespuestasSeccionF(FtxtPreg20Resp1, FtxtPreg20Resp2);
                        BackQuestionObject("APTITUD1-F-0020", vAPTITUD1_F_0020);
                        break;

                    case 6:
                        //////////////////////////////////////////////////SECCION G/////////////////////////////////////////////////////////////////////

                        String vAPTITUD1_G_0001 = RespuestasSeccionG(GbtnPreg1Resp1, GbtnPreg1Resp2, GbtnPreg1Resp3, GbtnPreg1Resp4);
                        BackQuestionObject("APTITUD1-G-0001", vAPTITUD1_G_0001);

                        String vAPTITUD1_G_0002 = RespuestasSeccionG(GbtnPreg2Resp1, GbtnPreg2Resp2, GbtnPreg2Resp3, GbtnPreg2Resp4);
                        BackQuestionObject("APTITUD1-G-0002", vAPTITUD1_G_0002);

                        String vAPTITUD1_G_0003 = RespuestasSeccionG(GbtnPreg3Resp1, GbtnPreg3Resp2, GbtnPreg3Resp3, GbtnPreg3Resp4);
                        BackQuestionObject("APTITUD1-G-0003", vAPTITUD1_G_0003);

                        String vAPTITUD1_G_0004 = RespuestasSeccionG(GbtnPreg4Resp1, GbtnPreg4Resp2, GbtnPreg4Resp3, GbtnPreg4Resp4);
                        BackQuestionObject("APTITUD1-G-0004", vAPTITUD1_G_0004);

                        String vAPTITUD1_G_0005 = RespuestasSeccionG(GbtnPreg5Resp1, GbtnPreg5Resp2, GbtnPreg5Resp3, GbtnPreg5Resp4);
                        BackQuestionObject("APTITUD1-G-0005", vAPTITUD1_G_0005);

                        String vAPTITUD1_G_0006 = RespuestasSeccionG(GbtnPreg6Resp1, GbtnPreg6Resp2, GbtnPreg6Resp3, GbtnPreg6Resp4);
                        BackQuestionObject("APTITUD1-G-0006", vAPTITUD1_G_0006);

                        String vAPTITUD1_G_0007 = RespuestasSeccionG(GbtnPreg7Resp1, GbtnPreg7Resp2, GbtnPreg7Resp3, GbtnPreg7Resp4);
                        BackQuestionObject("APTITUD1-G-0007", vAPTITUD1_G_0007);

                        String vAPTITUD1_G_0008 = RespuestasSeccionG(GbtnPreg8Resp1, GbtnPreg8Resp2, GbtnPreg8Resp3, GbtnPreg8Resp4);
                        BackQuestionObject("APTITUD1-G-0008", vAPTITUD1_G_0008);

                        String vAPTITUD1_G_0009 = RespuestasSeccionG(GbtnPreg9Resp1, GbtnPreg9Resp2, GbtnPreg9Resp3, GbtnPreg9Resp4);
                        BackQuestionObject("APTITUD1-G-0009", vAPTITUD1_G_0009);

                        String vAPTITUD1_G_0010 = RespuestasSeccionG(GbtnPreg10Resp1, GbtnPreg10Resp2, GbtnPreg10Resp3, GbtnPreg10Resp4);
                        BackQuestionObject("APTITUD1-G-0010", vAPTITUD1_G_0010);

                        String vAPTITUD1_G_0011 = RespuestasSeccionG(GbtnPreg11Resp1, GbtnPreg11Resp2, GbtnPreg11Resp3, GbtnPreg11Resp4);
                        BackQuestionObject("APTITUD1-G-0011", vAPTITUD1_G_0011);

                        String vAPTITUD1_G_0012 = RespuestasSeccionG(GbtnPreg12Resp1, GbtnPreg12Resp2, GbtnPreg12Resp3, GbtnPreg12Resp4);
                        BackQuestionObject("APTITUD1-G-0012", vAPTITUD1_G_0012);

                        String vAPTITUD1_G_0013 = RespuestasSeccionG(GbtnPreg13Resp1, GbtnPreg13Resp2, GbtnPreg13Resp3, GbtnPreg13Resp4);
                        BackQuestionObject("APTITUD1-G-0013", vAPTITUD1_G_0013);

                        String vAPTITUD1_G_0014 = RespuestasSeccionG(GbtnPreg14Resp1, GbtnPreg14Resp2, GbtnPreg14Resp3, GbtnPreg14Resp4);
                        BackQuestionObject("APTITUD1-G-0014", vAPTITUD1_G_0014);

                        String vAPTITUD1_G_0015 = RespuestasSeccionG(GbtnPreg15Resp1, GbtnPreg15Resp2, GbtnPreg15Resp3, GbtnPreg15Resp4);
                        BackQuestionObject("APTITUD1-G-0015", vAPTITUD1_G_0015);

                        String vAPTITUD1_G_0016 = RespuestasSeccionG(GbtnPreg16Resp1, GbtnPreg16Resp2, GbtnPreg16Resp3, GbtnPreg16Resp4);
                        BackQuestionObject("APTITUD1-G-0016", vAPTITUD1_G_0016);

                        String vAPTITUD1_G_0017 = RespuestasSeccionG(GbtnPreg17Resp1, GbtnPreg17Resp2, GbtnPreg17Resp3, GbtnPreg17Resp4);
                        BackQuestionObject("APTITUD1-G-0017", vAPTITUD1_G_0017);

                        String vAPTITUD1_G_0018 = RespuestasSeccionG(GbtnPreg18Resp1, GbtnPreg18Resp2, GbtnPreg18Resp3, GbtnPreg18Resp4);
                        BackQuestionObject("APTITUD1-G-0018", vAPTITUD1_G_0018);

                        String vAPTITUD1_G_0019 = RespuestasSeccionG(GbtnPreg19Resp1, GbtnPreg19Resp2, GbtnPreg19Resp3, GbtnPreg19Resp4);
                        BackQuestionObject("APTITUD1-G-0019", vAPTITUD1_G_0019);

                        String vAPTITUD1_G_0020 = RespuestasSeccionG(GbtnPreg20Resp1, GbtnPreg20Resp2, GbtnPreg20Resp3300, GbtnPreg20Resp4);
                        BackQuestionObject("APTITUD1-G-0020", vAPTITUD1_G_0020);

                        break;

                    case 7:

                        //////////////////////////////////////////////////SECCION H/////////////////////////////////////////////////////////////////////

                        String vAPTITUD1_H_0001 = RespuestasSeccionH(HbtnPreg1Resp1, HbtnPreg1Resp2);
                        BackQuestionObject("APTITUD1-H-0001", vAPTITUD1_H_0001);

                        String vAPTITUD1_H_0002 = RespuestasSeccionH(HbtnPreg2Resp1, HbtnPreg2Resp2);
                        BackQuestionObject("APTITUD1-H-0002", vAPTITUD1_H_0002);

                        String vAPTITUD1_H_0003 = RespuestasSeccionH(HbtnPreg3Resp1, HbtnPreg3Resp2);
                        BackQuestionObject("APTITUD1-H-0003", vAPTITUD1_H_0003);

                        String vAPTITUD1_H_0004 = RespuestasSeccionH(HbtnPreg4Resp1, HbtnPreg4Resp2);
                        BackQuestionObject("APTITUD1-H-0004", vAPTITUD1_H_0004);

                        String vAPTITUD1_H_0005 = RespuestasSeccionH(HbtnPreg5Resp1, HbtnPreg5Resp2);
                        BackQuestionObject("APTITUD1-H-0005", vAPTITUD1_H_0005);

                        String vAPTITUD1_H_0006 = RespuestasSeccionH(HbtnPreg6Resp1, HbtnPreg6Resp2);
                        BackQuestionObject("APTITUD1-H-0006", vAPTITUD1_H_0006);

                        String vAPTITUD1_H_0007 = RespuestasSeccionH(HbtnPreg7Resp1, HbtnPreg7Resp2);
                        BackQuestionObject("APTITUD1-H-0007", vAPTITUD1_H_0007);

                        String vAPTITUD1_H_0008 = RespuestasSeccionH(HbtnPreg8Resp1, HbtnPreg8Resp2);
                        BackQuestionObject("APTITUD1-H-0008", vAPTITUD1_H_0008);

                        String vAPTITUD1_H_0009 = RespuestasSeccionH(HbtnPreg9Resp1, HbtnPreg9Resp2);
                        BackQuestionObject("APTITUD1-H-0009", vAPTITUD1_H_0009);

                        String vAPTITUD1_H_0010 = RespuestasSeccionH(HbtnPreg10Resp1, HbtnPreg10Resp2);
                        BackQuestionObject("APTITUD1-H-0010", vAPTITUD1_H_0010);

                        String vAPTITUD1_H_0011 = RespuestasSeccionH(HbtnPreg11Resp1, HbtnPreg11Resp2);
                        BackQuestionObject("APTITUD1-H-0011", vAPTITUD1_H_0011);

                        String vAPTITUD1_H_0012 = RespuestasSeccionH(HbtnPreg12Resp1, HbtnPreg12Resp2);
                        BackQuestionObject("APTITUD1-H-0012", vAPTITUD1_H_0012);

                        String vAPTITUD1_H_0013 = RespuestasSeccionH(HbtnPreg13Resp1, HbtnPreg13Resp2);
                        BackQuestionObject("APTITUD1-H-0013", vAPTITUD1_H_0013);

                        String vAPTITUD1_H_0014 = RespuestasSeccionH(HbtnPreg14Resp1, HbtnPreg14Resp2);
                        BackQuestionObject("APTITUD1-H-0014", vAPTITUD1_H_0014);

                        String vAPTITUD1_H_0015 = RespuestasSeccionH(HbtnPreg15Resp1, HbtnPreg15Resp2);
                        BackQuestionObject("APTITUD1-H-0015", vAPTITUD1_H_0015);

                        String vAPTITUD1_H_0016 = RespuestasSeccionH(HbtnPreg16Resp1, HbtnPreg16Resp2);
                        BackQuestionObject("APTITUD1-H-0016", vAPTITUD1_H_0016);

                        String vAPTITUD1_H_0017 = RespuestasSeccionH(HbtnPreg17Resp1, HbtnPreg17Resp2);
                        BackQuestionObject("APTITUD1-H-0017", vAPTITUD1_H_0017);
                        break;

                    case 8:
                        //////////////////////////////////////////////////SECCION I/////////////////////////////////////////////////////////////////////
                        //IbtnPreg1Resp1

                        String vAPTITUD1_I_0001 = RespuestasSeccionI(IbtnPreg1Resp1, IbtnPreg1Resp2, IbtnPreg1Resp3, IbtnPreg1Resp4, IbtnPreg1Resp5);
                        BackQuestionObject("APTITUD1-I-0001", vAPTITUD1_I_0001);

                        String vAPTITUD1_I_0002 = RespuestasSeccionI(IbtnPreg2Resp1, IbtnPreg2Resp2, IbtnPreg2Resp3, IbtnPreg2Resp4, IbtnPreg2Resp5);
                        BackQuestionObject("APTITUD1-I-0002", vAPTITUD1_I_0002);

                        String vAPTITUD1_I_0003 = RespuestasSeccionI(IbtnPreg3Resp1, IbtnPreg3Resp2, IbtnPreg3Resp3, IbtnPreg3Resp4, IbtnPreg3Resp5);
                        BackQuestionObject("APTITUD1-I-0003", vAPTITUD1_I_0003);

                        String vAPTITUD1_I_0004 = RespuestasSeccionI(IbtnPreg4Resp1, IbtnPreg4Resp2, IbtnPreg4Resp3, IbtnPreg4Resp4, IbtnPreg4Resp5);
                        BackQuestionObject("APTITUD1-I-0004", vAPTITUD1_I_0004);

                        String vAPTITUD1_I_0005 = RespuestasSeccionI(IbtnPreg5Resp1, IbtnPreg5Resp2, IbtnPreg5Resp3, IbtnPreg5Resp4, IbtnPreg5Resp5);
                        BackQuestionObject("APTITUD1-I-0005", vAPTITUD1_I_0005);

                        String vAPTITUD1_I_0006 = RespuestasSeccionI(IbtnPreg6Resp1, IbtnPreg6Resp2, IbtnPreg6Resp3, IbtnPreg6Resp4, IbtnPreg6Resp5);
                        BackQuestionObject("APTITUD1-I-0006", vAPTITUD1_I_0006);

                        String vAPTITUD1_I_0007 = RespuestasSeccionI(IbtnPreg7Resp1, IbtnPreg7Resp2, IbtnPreg7Resp3, IbtnPreg7Resp4, IbtnPreg7Resp5);
                        BackQuestionObject("APTITUD1-I-0007", vAPTITUD1_I_0007);

                        String vAPTITUD1_I_0008 = RespuestasSeccionI(IbtnPreg8Resp1, IbtnPreg8Resp2, IbtnPreg8Resp3, IbtnPreg8Resp4, IbtnPreg8Resp5);
                        BackQuestionObject("APTITUD1-I-0008", vAPTITUD1_I_0008);

                        String vAPTITUD1_I_0009 = RespuestasSeccionI(IbtnPreg9Resp1, IbtnPreg9Resp2, IbtnPreg9Resp3, IbtnPreg9Resp4, IbtnPreg9Resp5);
                        BackQuestionObject("APTITUD1-I-0009", vAPTITUD1_I_0009);


                        String vAPTITUD1_I_0010 = RespuestasSeccionI(IbtnPreg10Resp1, IbtnPreg10Resp2, IbtnPreg10Resp3, IbtnPreg10Resp4, IbtnPreg10Resp5);
                        BackQuestionObject("APTITUD1-I-0010", vAPTITUD1_I_0010);

                        String vAPTITUD1_I_0011 = RespuestasSeccionI(IbtnPreg11Resp1, IbtnPreg11Resp2, IbtnPreg11Resp3, IbtnPreg11Resp4, IbtnPreg11Resp5);
                        BackQuestionObject("APTITUD1-I-0011", vAPTITUD1_I_0011);

                        String vAPTITUD1_I_0012 = RespuestasSeccionI(IbtnPreg12Resp1, IbtnPreg12Resp2, IbtnPreg12Resp3, IbtnPreg12Resp4, IbtnPreg12Resp5);
                        BackQuestionObject("APTITUD1-I-0012", vAPTITUD1_I_0012);

                        String vAPTITUD1_I_0013 = RespuestasSeccionI(IbtnPreg13Resp1, IbtnPreg13Resp2, IbtnPreg13Resp3, IbtnPreg13Resp4, IbtnPreg13Resp5);
                        BackQuestionObject("APTITUD1-I-0013", vAPTITUD1_I_0013);

                        String vAPTITUD1_I_0014 = RespuestasSeccionI(IbtnPreg14Resp1, IbtnPreg14Resp2, IbtnPreg14Resp3, IbtnPreg14Resp4, IbtnPreg14Resp5);
                        BackQuestionObject("APTITUD1-I-0014", vAPTITUD1_I_0014);

                        String vAPTITUD1_I_0015 = RespuestasSeccionI(IbtnPreg15Resp1, IbtnPreg15Resp2, IbtnPreg15Resp3, IbtnPreg15Resp4, IbtnPreg15Resp5);
                        BackQuestionObject("APTITUD1-I-0015", vAPTITUD1_I_0015);

                        String vAPTITUD1_I_0016 = RespuestasSeccionI(IbtnPreg16Resp1, IbtnPreg16Resp2, IbtnPreg16Resp3, IbtnPreg16Resp4, IbtnPreg16Resp5);
                        BackQuestionObject("APTITUD1-I-0016", vAPTITUD1_I_0016);

                        String vAPTITUD1_I_0017 = RespuestasSeccionI(IbtnPreg17Resp1, IbtnPreg17Resp2, IbtnPreg17Resp3, IbtnPreg17Resp4, IbtnPreg17Resp5);
                        BackQuestionObject("APTITUD1-I-0017", vAPTITUD1_I_0017);

                        String vAPTITUD1_I_0018 = RespuestasSeccionI(IbtnPreg18Resp1, IbtnPreg18Resp2, IbtnPreg18Resp3, IbtnPreg18Resp4, IbtnPreg18Resp5);
                        BackQuestionObject("APTITUD1-I-0018", vAPTITUD1_I_0018);

                        break;

                    case 9:


                        //////////////////////////////////////////////////SECCION J/////////////////////////////////////////////////////////////////////

                        String vAPTITUD1_J_0001 = RespuestasSeccionJ(JbtnPreg1Resp1);
                        BackQuestionObject("APTITUD1-J-0001", vAPTITUD1_J_0001);
                        String vAPTITUD1_J_0002 = RespuestasSeccionJ(JbtnPreg1Resp2);
                        BackQuestionObject("APTITUD1-J-0002", vAPTITUD1_J_0002);


                        String vAPTITUD1_J_0003 = RespuestasSeccionJ(JbtnPreg2Resp1);
                        BackQuestionObject("APTITUD1-J-0003", vAPTITUD1_J_0003);
                        String vAPTITUD1_J_0004 = RespuestasSeccionJ(JbtnPreg2Resp2);
                        BackQuestionObject("APTITUD1-J-0004", vAPTITUD1_J_0004);


                        String vAPTITUD1_J_0005 = RespuestasSeccionJ(JbtnPreg3Resp1);
                        BackQuestionObject("APTITUD1-J-0005", vAPTITUD1_J_0005);
                        String vAPTITUD1_J_0006 = RespuestasSeccionJ(JbtnPreg3Resp2);
                        BackQuestionObject("APTITUD1-J-0006", vAPTITUD1_J_0006);


                        String vAPTITUD1_J_0007 = RespuestasSeccionJ(JbtnPreg4Resp1);
                        BackQuestionObject("APTITUD1-J-0007", vAPTITUD1_J_0007);
                        String vAPTITUD1_J_0008 = RespuestasSeccionJ(JbtnPreg4Resp2);
                        BackQuestionObject("APTITUD1-J-0008", vAPTITUD1_J_0008);

                        String vAPTITUD1_J_0009 = RespuestasSeccionJ(JbtnPreg5Resp1);
                        BackQuestionObject("APTITUD1-J-0009", vAPTITUD1_J_0009);
                        String vAPTITUD1_J_0010 = RespuestasSeccionJ(JbtnPreg5Resp2);
                        BackQuestionObject("APTITUD1-J-0010", vAPTITUD1_J_0010);

                        String vAPTITUD1_J_0011 = RespuestasSeccionJ(JbtnPreg6Resp1);
                        BackQuestionObject("APTITUD1-J-0011", vAPTITUD1_J_0011);
                        String vAPTITUD1_J_0012 = RespuestasSeccionJ(JbtnPreg6Resp2);
                        BackQuestionObject("APTITUD1-J-0012", vAPTITUD1_J_0012);

                        String vAPTITUD1_J_0013 = RespuestasSeccionJ(JbtnPreg7Resp1);
                        BackQuestionObject("APTITUD1-J-0013", vAPTITUD1_J_0013);
                        String vAPTITUD1_J_0014 = RespuestasSeccionJ(JbtnPreg7Resp2);
                        BackQuestionObject("APTITUD1-J-0014", vAPTITUD1_J_0014);

                        String vAPTITUD1_J_0015 = RespuestasSeccionJ(JbtnPreg8Resp1);
                        BackQuestionObject("APTITUD1-J-0015", vAPTITUD1_J_0015);
                        String vAPTITUD1_J_0016 = RespuestasSeccionJ(JbtnPreg8Resp2);
                        BackQuestionObject("APTITUD1-J-0016", vAPTITUD1_J_0016);

                        String vAPTITUD1_J_0017 = RespuestasSeccionJ(JbtnPreg9Resp1);
                        BackQuestionObject("APTITUD1-J-0017", vAPTITUD1_J_0017);
                        String vAPTITUD1_J_0018 = RespuestasSeccionJ(JbtnPreg9Resp2);
                        BackQuestionObject("APTITUD1-J-0018", vAPTITUD1_J_0018);

                        String vAPTITUD1_J_0019 = RespuestasSeccionJ(JbtnPreg10Resp1);
                        BackQuestionObject("APTITUD1-J-0019", vAPTITUD1_J_0019);
                        String vAPTITUD1_J_0020 = RespuestasSeccionJ(JbtnPreg10Resp2);
                        BackQuestionObject("APTITUD1-J-0020", vAPTITUD1_J_0020);

                        String vAPTITUD1_J_0021 = RespuestasSeccionJ(JbtnPreg11Resp1);
                        BackQuestionObject("APTITUD1-J-0021", vAPTITUD1_J_0021);
                        String vAPTITUD1_J_0022 = RespuestasSeccionJ(JbtnPreg11Resp2);
                        BackQuestionObject("APTITUD1-J-0022", vAPTITUD1_J_0022);
                        break;
                }

                var vXelements = vRespuestas.Select(x =>
                                                     new XElement("RESPUESTA",
                                                     new XAttribute("ID_CUESTIONARIO_PREGUNTA", x.ID_CUESTIONARIO_PREGUNTA),
                                                     new XAttribute("ID_PREGUNTA", x.ID_PREGUNTA),
                                                     new XAttribute("NB_PREGUNTA", x.NB_PREGUNTA),
                                                     new XAttribute("NB_RESPUESTA", x.NB_RESPUESTA),
                                                     new XAttribute("NO_VALOR_RESPUESTA", x.NO_VALOR_RESPUESTA),
                                                     new XAttribute("CL_VARIABLE", x.CL_PREGUNTA)
                                          ));
                XElement RESPUESTAS =
                new XElement("RESPUESTAS", vXelements
                );

                CuestionarioPreguntaNegocio nCustionarioPregunta = new CuestionarioPreguntaNegocio();
                PruebasNegocio nKprueba = new PruebasNegocio();
                var vObjetoPrueba = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();

                if (vObjetoPrueba != null)
                {
                    vPrueba = vPrueba + (vseccion + 1).ToString();
                    String CallBackFunction = "";
                    var vSeccionInicia = new E_PRUEBA_TIEMPO();
                    if (vseccion != (vSeccionesPrueba.Count - 1))
                    {
                        CallBackFunction = "updateTimer('" + (vseccion + 1) + "','click')";
                        vSeccionInicia = vSeccionesPrueba.ElementAt(vseccion + 1);
                        //vSeccionInicia.FE_INICIO = DateTime.Now;
                        //vSeccionInicia.CL_ESTADO = E_ESTADO_PRUEBA.INICIADA.ToString();
                    }
                    else
                    {
                        SPE_OBTIENE_K_PRUEBA_Result vPruebaTerminada = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();
                        //vPruebaTerminada.FE_TERMINO = DateTime.Now;
                        //vPruebaTerminada.CL_ESTADO = E_ESTADO_PRUEBA.TERMINADA.ToString();
                        //vPruebaTerminada.NB_TIPO_PRUEBA = "APLICACION";
                        E_RESULTADO vResultadoTestEnd = nKprueba.CorrigePrueba(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
                        //vSeccionInicia = vSeccionesPrueba.ElementAt(vseccion);
                        //vSeccionInicia.FE_INICIO = DateTime.Now;
                        //vSeccionInicia.CL_ESTADO = E_ESTADO_PRUEBA.INICIADA.ToString();
                        //CallBackFunction = "CloseTest";
                    }

                    if (Request.QueryString["MOD"] != null)
                    {
                        E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vObjetoPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: vPrueba, usuario: vClUsuario, programa: vNbPrograma);
                        string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                        UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
                        PintarRespuestasMentalI();
                    }
                    //else
                    //{
                    //    E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vObjetoPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: vPrueba, usuario: vClUsuario, programa: vNbPrograma);
                    //    string vMensaje = instrucciones(vseccion + 1);
                    //    //int vHeight = HeightRadAlert(vseccion + 1);
                    //    vRadAlertAltura = HeightRadAlert(vseccion + 1);

                    //    //UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, vHeight, CallBackFunction);
                    //    E_RESULTADO vResultadoSeccion = nKprueba.InsertaActualiza_K_PRUEBA_SECCION(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), v_k_prueba: vSeccionInicia, usuario: vClUsuario, programa: vNbPrograma);
                    //    mpgActitudMentalI.SelectedIndex = vseccion + 1;
                    //    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "MyScript", CallBackFunction, true);

                    //    //if (mpgActitudMentalI.SelectedIndex == 9)
                    //    //{
                    //    //    RadButton1.Text = "Guardar";
                    //    //}

                    //}

                }
            }
        }

        public void controltime(int? vPosicionPrueba, int? vTiempoPrueba)
        {
            vSeccionAtime = vSeccionesPrueba.ElementAt(0).NO_TIEMPO * 60;
            vSeccionBtime = vSeccionesPrueba.ElementAt(1).NO_TIEMPO * 60;
            vSeccionCtime = vSeccionesPrueba.ElementAt(2).NO_TIEMPO * 60;
            vSeccionDtime = vSeccionesPrueba.ElementAt(3).NO_TIEMPO * 60;
            vSeccionEtime = vSeccionesPrueba.ElementAt(4).NO_TIEMPO * 60;
            vSeccionFtime = vSeccionesPrueba.ElementAt(5).NO_TIEMPO * 60;
            vSeccionGtime = vSeccionesPrueba.ElementAt(6).NO_TIEMPO * 60;
            vSeccionHtime = vSeccionesPrueba.ElementAt(7).NO_TIEMPO * 60;
            vSeccionItime = vSeccionesPrueba.ElementAt(8).NO_TIEMPO * 60;
            vSeccionJtime = vSeccionesPrueba.ElementAt(9).NO_TIEMPO * 60;

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
                case 4:
                    vSeccionEtime = vTiempoPrueba;
                    break;
                case 5:
                    vSeccionFtime = vTiempoPrueba;
                    break;
                case 6:
                    vSeccionGtime = vTiempoPrueba;
                    break;
                case 7:
                    vSeccionHtime = vTiempoPrueba;
                    break;
                case 8:
                    vSeccionItime = vTiempoPrueba;
                    break;
                case 9:
                    vSeccionJtime = vTiempoPrueba;
                    break;
                default: break;

            }
        }

        //public string instrucciones(int seccion)
        //{
        //    string instruccion = "";
        //    switch (seccion)
        //    {
        //        case 1:
        //            instruccion = "<p style=\"margin: 10px; text-align: justify; \"><b>SERIE II</b><br />" +
        //                    "Instrucciones: Lee cada cuestión y anota la letra correspondiente a la mejor respuesta tal como lo muestra el ejemplo. <br />" +
        //                    "<b>Ejemplo:</b><br />" +
        //                    "¿Por qué compramos relojes? Porque:<br />" +
        //                    "( ) Nos gusta oirlos sonar<br />" +
        //                    "( ) Tiene manecillas<br />" +
        //                    "(*) Nos indican las horas" +
        //                    "</p>";
        //            break;
        //        case 2:
        //            instruccion = "<p style=\"margin: 10px; text-align: justify; \"><b>SERIE III</b><br />" +
        //                    "Instrucciones: Cuando dos palabras signifiquen lo mismo, selecciona la opción &quot;I&quot; de igual. Cuando signifiquen lo opuesto selecciona la letra &quot;O&quot;" +
        //                    "<br />" +
        //                    "<b>Ejemplo:</b><br />" +
        //                    "Tirar-Arrojar <br />" +
        //                    "(*) I ( ) O <br />" +
        //                    "Norte-Sur <br />" +
        //                    "( ) I (*) O </p>";

        //            break;
        //        case 3:
        //            instruccion = " <p style=\"margin: 10px; text-align: justify; \"> <b>SERIE IV</b><br />" +
        //                    "Instrucciones: Anote en la hoja de respuestas las letras correspondientes a las DOS palabras que indican algo que SIEMPRE tiene el sujeto." +
        //                    "ANOTA SOLAMENTE DOS para cada renglón<br />" +
        //                    "<br />" +
        //                    "<b>Ejemplo:</b><br />" +
        //                    "Un hombre tiene siempre:<br />" +
        //                    "[*] Cuerpo [ ] Gorra [ ] Guantes [&#149;] Boca [ ] Dinero" +
        //                    "</p>";
        //            break;
        //        case 4:
        //            instruccion = "<p style=\"margin: 10px; text-align: justify; \"> <b>SERIE V</b><br />" +
        //                         "Instrucciones: Encuentra las respuestas lo más pronto posible y escríbelas en el espacio para captura. Puedes utilizar una hoja en blanco para hacer las operaciones.</p>";
        //            break;
        //        case 5:
        //            instruccion =
        //                    " <p style=\"margin: 10px; text-align: justify; \"><b>SERIE VI</b><br />" +
        //                    "Instrucciones: Señala &quot;Si&quot; o &quot;No&quot; para cada caso.<br />" +
        //                    "<br />" +
        //                    "<b>Ejemplo:</b><br />" +
        //                    "Si - No" +
        //                    "Se hace el carbón de madera? <br />" +
        //                     "(*)( )  <br />" +
        //                    "¿Tienen todos los hombres 1.70 de altura?  <br />" +
        //                    "( )(*)  </p>";
        //            break;
        //        case 6:
        //            instruccion =
        //                    "    <p style=\"margin: 10px; text-align: justify; \"><b>SERIE VII</b><br />" +
        //                    "Instrucciones: Selecciona la letra correspondiente a la palabra que complete correctamente la oración" +
        //                    "<br />" +
        //                    "<br /><b>Ejemplo:</b><br />" +
        //                    "El OIDO es a OIR como el OJO es a:<br />" +
        //                    "( ) Mesa (*) Ver ( ) Mano ( ) Jugar<br />" +
        //                    "El SOMBRERO es a la CABEZA como el ZAPATO es a:<br />" +
        //                    "( ) Brazo ( ) Abrigo (*) Pie ( ) Pierna  </p>";
        //            break;
        //        case 7:
        //            instruccion =
        //                    "<p style=\"margin: 10px; text-align: justify; \">" +
        //                    "<b>SERIE VIII</b><br />" +
        //                    "Instrucciones: Las palabras de cada una de las oraciones siguientes están mezcladas.Ordena cada una de las oraciones. Si el significado de la oración es VERDADERO selecciona la opcion &quot;V&quot;; si el significado de la oración es FALSO, selecciona la opción &quot;F&quot;. <br />" +
        //                    "<br />" +
        //                    "<b>Ejemplo:</b><br />" +
        //                    "V  <br/>" +
        //                    "F" +
        //                    "oir son los para oídos <br />" +
        //                    "(*)" +
        //                    "( )  <br/>" +
        //                    "comer para pólvora la buena es <br/>" +
        //                    "( ) " +
        //                    "(*)  </p>";
        //            break;
        //        case 8:
        //            instruccion =
        //                                     " <p style=\"margin: 10px; text-align: justify; \"><b>SERIE IX</b><br />" +
        //                    "Instrucciones: Selecciona la opción con la palabra que no corresponde con las demás del renglón.<br />" +
        //                    "<br /><b>Ejemplo:</b><br />" +
        //                    "( ) Bala ( ) Cañón ( ) Pistola ( ) Espada (*) Lápiz<br />" +
        //                    "( ) Canadá (*) Sonora ( ) China ( ) India ( ) Francia<br />";
        //            break;
        //        case 9:
        //            instruccion =
        //                    "<p style=\"margin: 10px; text-align: justify; \"><b>SERIE X</b><br />" +
        //                    "Instrucciones: En cada renglón procura encontrar cómo están hechas las series; después, escribe en los espacios en blanco los DOS números que deban seguir en cada serie<br />" +
        //                    "<br /><b>Ejemplo:</b><br />" +
        //                     "5 10 15 20  <br />" +
        //                     "25  <br />" +
        //                     "30  <br />" +
        //                      "20 18 16 14 12 <br />" +
        //                     "10  <br />" +
        //                     "8  </p>";
        //            break;

        //        default:
        //            instruccion = "Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido. <br> Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no te lo permitirá: se generará un error y el intento quedará registrado";
        //            break;
        //    }
        //    return instruccion;
        //}

        //public int HeightRadAlert(int seccion)
        //{
        //    int height = 0;
        //    switch (seccion)
        //    {
        //        case 1:
        //            height = 300;
        //            break;
        //        case 2:
        //            height = 300;
        //            break;
        //        case 3:
        //            height = 350;
        //            break;
        //        case 4:
        //            height = 250;
        //            break;
        //        case 5:
        //            height = 300;
        //            break;
        //        case 6:
        //            height = 330;
        //            break;
        //        case 7:
        //            height = 400;
        //            break;
        //        case 8:
        //            height = 300;
        //            break;
        //        case 9:
        //            height = 360;
        //            break;
        //        default:
        //            height = 320;
        //            break;
        //    }
        //    return height;
        //}

        public String BackSelectedCheckBox(RadButton a, RadButton b, RadButton c, RadButton d)
        {
            String resultado = "";
            if (a.Checked)
            { resultado = "a"; }
            else if (b.Checked)
            { resultado = "b"; }
            else if (c.Checked)
            { resultado = "c"; }
            else if (d.Checked)
            { resultado = "d"; }
            else
            {
                resultado = "-";
            }
            return resultado;
        }

        public String RespuestasSeccionB(RadButton a, RadButton b, RadButton c)
        {
            String resultado = "";
            if (a.Checked)
            { resultado = "a"; }
            else if (b.Checked)
            { resultado = "b"; }
            else if (c.Checked)
            { resultado = "c"; }
            else
            {
                resultado = "-";
            }
            return resultado;
        }

        public String RespuestasSeccionC(RadButton a, RadButton b)
        {
            String resultado = "";
            if (a.Checked)
            { resultado = a.Text; }
            else if (b.Checked)
            { resultado = b.Text; }
            else
            {
                resultado = "-";
            }
            return resultado;
        }

        public String RespuestasSeccionD(RadButton a, RadButton b, RadButton c, RadButton d, RadButton e)
        {
            String resultado = "";
            if (a.Checked)
            { resultado += "a"; }
            if (b.Checked)
            { resultado += "b"; }
            if (c.Checked)
            { resultado += "c"; }
            if (d.Checked)
            { resultado += "d"; }
            if (e.Checked)
            { resultado += "e"; }


            if (resultado.Length == 0)
            {
                resultado = "00";
            }
            else if (resultado.Length != 2)
            {
                resultado = "0" + resultado;
            }
            return resultado;
        }

        public String RespuestasSeccionF(RadButton a, RadButton b)
        {
            String resultado = "";
            if (a.Checked)
            { resultado = "1"; }
            else if (b.Checked)
            { resultado = "0"; }
            else
            {
                resultado = "0";
            }
            return resultado;
        }

        public String RespuestasSeccionG(RadButton a, RadButton b, RadButton c, RadButton d)
        {
            String resultado = "";
            if (a.Checked)
            { resultado = "a"; }
            else if (b.Checked)
            { resultado = "b"; }
            else if (c.Checked)
            { resultado = "c"; }
            else if (d.Checked)
            { resultado = "d"; }
            else
            { resultado = "-"; }
            return resultado;
        }

        public String RespuestasSeccionH(RadButton a, RadButton b)
        {
            String resultado = "";
            if (a.Checked)
            { resultado = a.Text; }
            else if (b.Checked)
            { resultado = b.Text; }
            else
            {
                resultado = "-";
            }
            return resultado;
        }

        public String RespuestasSeccionI(RadButton a, RadButton b, RadButton c, RadButton d, RadButton e)
        {
            String resultado = "";
            if (a.Checked)
            { resultado = "a"; }
            else if (b.Checked)
            { resultado = "b"; }
            else if (c.Checked)
            { resultado = "c"; }
            else if (d.Checked)
            { resultado = "d"; }
            else if (e.Checked)
            { resultado = "e"; }
            else
            { resultado = "-"; }
            return resultado;
        }

        public String RespuestasSeccionJ(RadTextBox a)
        {
            String resultado = "";
            resultado = a.Text;
            return resultado;
        }

        public void BackQuestionObject(string pclPregunta, string pnbRespuesta)
        {
            var vPregunta = vRespuestas.Where(x => x.CL_PREGUNTA.Equals(pclPregunta)).FirstOrDefault();
            if (vPregunta != null)
            {
                decimal vNoValor;
                vPregunta.NB_RESPUESTA = pnbRespuesta;
                vPregunta.NO_VALOR_RESPUESTA = (vNoValor = (pnbRespuesta != "") ? 1 : 0);
            }
        }

        public string TraerLetraSeccion(int position)
        {
            string letter = "";
            switch (position)
            {
                case 0: letter = "A"; break;
                case 1: letter = "B"; break;
                case 2: letter = "C"; break;
                case 3: letter = "D"; break;
                case 4: letter = "E"; break;
                case 5: letter = "F"; break;
                case 6: letter = "G"; break;
                case 7: letter = "H"; break;
                case 8: letter = "I"; break;
                case 9: letter = "J"; break;
                default: break;
            }
            return letter;
        }

        public void asignarValores(List<E_RESULTADOS_PRUEBA> respuestas)
        {
            CuestionarioPreguntaNegocio nCuestionarioPregunta = new CuestionarioPreguntaNegocio();
            var vPruebaSeccion10 = nCuestionarioPregunta.Obtener_K_CUESTIONARIO_PREGUNTA_PRUEBA(ID_PRUEBA: vIdPrueba, CL_TOKEN: vClToken).OrderByDescending(t => t.ID_PREGUNTA).Take(22);
            //var seccion10 = vPruebaSeccion10.OrderByDescending(t => t.ID_PRUEBA).Take(22);


            if (respuestas != null || respuestas.Count > 0)
            {
                foreach (E_RESULTADOS_PRUEBA resp in respuestas)
                {
                    switch (resp.CL_PREGUNTA)
                    {
                        case "APTITUD1-A-0001": PintarRespuestasA(APregunta1Resp1, APregunta1Resp2, APregunta1Resp3, APregunta1Resp4, resp.NB_RESPUESTA, RPView1); break;
                        case "APTITUD1-A-0002": PintarRespuestasA(APregunta2Resp1, APregunta2Resp2, APregunta2Resp3, APregunta2Resp4, resp.NB_RESPUESTA, RPView1); break;
                        case "APTITUD1-A-0003": PintarRespuestasA(APregunta3Resp1, APregunta3Resp2, APregunta3Resp3, APregunta3Resp4, resp.NB_RESPUESTA, RPView1); break;
                        case "APTITUD1-A-0004": PintarRespuestasA(APregunta4Resp1, APregunta4Resp2, APregunta4Resp3, APregunta4Resp4, resp.NB_RESPUESTA, RPView1); break;
                        case "APTITUD1-A-0005": PintarRespuestasA(APregunta5Resp1, APregunta5Resp2, APregunta5Resp3, APregunta5Resp4, resp.NB_RESPUESTA, RPView1); break;
                        case "APTITUD1-A-0006": PintarRespuestasA(APregunta6Resp1, APregunta6Resp2, APregunta6Resp3, APregunta6Resp4, resp.NB_RESPUESTA, RPView1); break;
                        case "APTITUD1-A-0007": PintarRespuestasA(APregunta7Resp1, APregunta7Resp2, APregunta7Resp3, APregunta7Resp4, resp.NB_RESPUESTA, RPView1); break;
                        case "APTITUD1-A-0008": PintarRespuestasA(APregunta8Resp1, APregunta8Resp2, APregunta8Resp3, APregunta8Resp4, resp.NB_RESPUESTA, RPView1); break;
                        case "APTITUD1-A-0009": PintarRespuestasA(APregunta9Resp1, APregunta9Resp2, APregunta9Resp3, APregunta9Resp4, resp.NB_RESPUESTA, RPView1); break;
                        case "APTITUD1-A-0010": PintarRespuestasA(APregunta10Resp1, APregunta10Resp2, APregunta10Resp3, APregunta10Resp4, resp.NB_RESPUESTA, RPView1); break;
                        case "APTITUD1-A-0011": PintarRespuestasA(APregunta11Resp1, APregunta11Resp2, APregunta11Resp3, APregunta11Resp4, resp.NB_RESPUESTA, RPView1); break;
                        case "APTITUD1-A-0012": PintarRespuestasA(APregunta12Resp1, APregunta12Resp2, APregunta12Resp3, APregunta12Resp4, resp.NB_RESPUESTA, RPView1); break;
                        case "APTITUD1-A-0013": PintarRespuestasA(APregunta13Resp1, APregunta13Resp2, APregunta13Resp3, APregunta13Resp4, resp.NB_RESPUESTA, RPView1); break;
                        case "APTITUD1-A-0014": PintarRespuestasA(APregunta14Resp1, APregunta14Resp2, APregunta14Resp3, APregunta14Resp4, resp.NB_RESPUESTA, RPView1); break;
                        case "APTITUD1-A-0015": PintarRespuestasA(APregunta15Resp1, APregunta15Resp2, APregunta15Resp3, APregunta15Resp4, resp.NB_RESPUESTA, RPView1); break;
                        case "APTITUD1-A-0016": PintarRespuestasA(APregunta16Resp1, APregunta16Resp2, APregunta16Resp3, APregunta16Resp4, resp.NB_RESPUESTA, RPView1); break;

                        ///SECCION 2

                        case "APTITUD1-B-0001": PintarRespuestasB(BPregunta1Resp1, BPregunta1Resp2, BPregunta1Resp3, resp.NB_RESPUESTA, RPView2); break;
                        case "APTITUD1-B-0002": PintarRespuestasB(BPregunta2Resp1, BPregunta2Resp2, BPregunta2Resp3, resp.NB_RESPUESTA, RPView2); break;
                        case "APTITUD1-B-0003": PintarRespuestasB(BPregunta3Resp, BPregunta3Resp2, BPregunta3Resp3, resp.NB_RESPUESTA, RPView2); break;
                        case "APTITUD1-B-0004": PintarRespuestasB(BPregunta4Resp1, BPregunta4Resp2, BPregunta4Resp3, resp.NB_RESPUESTA, RPView2); break;
                        case "APTITUD1-B-0005": PintarRespuestasB(BPregunta5Resp1, BPregunta5Resp2, BPregunta5Resp3, resp.NB_RESPUESTA, RPView2); break;
                        case "APTITUD1-B-0006": PintarRespuestasB(BPregunta6Resp1, BPregunta6Resp2, BPregunta6Resp3, resp.NB_RESPUESTA, RPView2); break;
                        case "APTITUD1-B-0007": PintarRespuestasB(BPregunta7Resp1, BPregunta7Resp2, BPregunta7Resp3, resp.NB_RESPUESTA, RPView2); break;
                        case "APTITUD1-B-0008": PintarRespuestasB(BPregunta8Resp1, BPregunta8Resp2, BPregunta8Resp3, resp.NB_RESPUESTA, RPView2); break;
                        case "APTITUD1-B-0009": PintarRespuestasB(BPregunta9Resp1, BPregunta9Resp2, BPregunta9Resp3, resp.NB_RESPUESTA, RPView2); break;
                        case "APTITUD1-B-0010": PintarRespuestasB(BPregunta10Resp1, BPregunta10Resp2, BPregunta10Resp3, resp.NB_RESPUESTA, RPView2); break;
                        case "APTITUD1-B-0011": PintarRespuestasB(BPregunta11Resp1, BPregunta11Resp2, BPregunta11Resp3, resp.NB_RESPUESTA, RPView2); break;

                        ///SECCION 3
                        case "APTITUD1-C-0001": PintarRespuestasC(CPregunta1Resp1, CPregunta1Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0002": PintarRespuestasC(CPregunta2Resp1, CPregunta2Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0003": PintarRespuestasC(CPregunta3Resp1, CPregunta3Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0004": PintarRespuestasC(CPregunta4Resp1, CPregunta4Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0005": PintarRespuestasC(CPregunta5Resp1, CPregunta5Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0006": PintarRespuestasC(CPregunta6Resp1, CPregunta6Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0007": PintarRespuestasC(CPregunta7Resp1, CPregunta7Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0008": PintarRespuestasC(CPregunta8Resp1, CPregunta8Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0009": PintarRespuestasC(CPregunta9Resp1, CPregunta9Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0010": PintarRespuestasC(CPregunta10Resp1, CPregunta10Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0011": PintarRespuestasC(CPregunta11Resp1, CPregunta11Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0012": PintarRespuestasC(CPregunta12Resp1, CPregunta12Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0013": PintarRespuestasC(CPregunta13Resp1, CPregunta13Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0014": PintarRespuestasC(CPregunta14Resp1, CPregunta14Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0015": PintarRespuestasC(CPregunta15Resp1, CPregunta15Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0016": PintarRespuestasC(CPregunta16Resp1, CPregunta16Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0017": PintarRespuestasC(CPregunta17Resp1, CPregunta17Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0018": PintarRespuestasC(CPregunta18Resp1, CPregunta18Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0019": PintarRespuestasC(CPregunta19Resp1, CPregunta19Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0020": PintarRespuestasC(CPregunta20Resp1, CPregunta20Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0021": PintarRespuestasC(CPregunta21Resp1, CPregunta21Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0022": PintarRespuestasC(CPregunta22Resp1, CPregunta22Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0023": PintarRespuestasC(CPregunta23Resp1, CPregunta23Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0024": PintarRespuestasC(CPregunta24Resp1, CPregunta24Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0025": PintarRespuestasC(CPregunta25Resp1, CPregunta25Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0026": PintarRespuestasC(CPregunta26Resp1, CPregunta26Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0027": PintarRespuestasC(CPregunta27Resp1, CPregunta27Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0028": PintarRespuestasC(CPregunta28Resp1, CPregunta28Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0029": PintarRespuestasC(CPregunta29Resp1, CPregunta29Resp2, resp.NB_RESPUESTA, RPView3); break;
                        case "APTITUD1-C-0030": PintarRespuestasC(CPregunta30Resp1, CPregunta30Resp2, resp.NB_RESPUESTA, RPView3); break;

                        ///SECCION 4
                        case "APTITUD1-D-0001": PintarRespuestasD(DPregunta1Resp1, DPregunta1Resp2, DPregunta1Resp3, DPregunta1Resp4, DPregunta1Resp5, resp.NB_RESPUESTA, RPView4); break;
                        case "APTITUD1-D-0002": PintarRespuestasD(DPregunta2Resp1, DPregunta2Resp2, DPregunta2Resp3, DPregunta2Resp4, DPregunta2Resp5, resp.NB_RESPUESTA, RPView4); break;
                        case "APTITUD1-D-0003": PintarRespuestasD(DPregunta3Resp1, DPregunta3Resp2, DPregunta3Resp3, DPregunta3Resp4, DPregunta3Resp5, resp.NB_RESPUESTA, RPView4); break;
                        case "APTITUD1-D-0004": PintarRespuestasD(DPregunta4Resp1, DPregunta4Resp2, DPregunta4Resp3, DPregunta4Resp4, DPregunta4Resp5, resp.NB_RESPUESTA, RPView4); break;
                        case "APTITUD1-D-0005": PintarRespuestasD(DPregunta5Resp1, DPregunta5Resp2, DPregunta5Resp3, DPregunta5Resp4, DPregunta5Resp5, resp.NB_RESPUESTA, RPView4); break;
                        case "APTITUD1-D-0006": PintarRespuestasD(DPregunta6Resp1, DPregunta6Resp2, DPregunta6Resp3, DPregunta6Resp4, DPregunta6Resp5, resp.NB_RESPUESTA, RPView4); break;
                        case "APTITUD1-D-0007": PintarRespuestasD(DPregunta7Resp1, DPregunta7Resp2, DPregunta7Resp3, DPregunta7Resp4, DPregunta7Resp5, resp.NB_RESPUESTA, RPView4); break;
                        case "APTITUD1-D-0008": PintarRespuestasD(DPregunta8Resp1, DPregunta8Resp2, DPregunta8Resp3, DPregunta8Resp4, DPregunta8Resp5, resp.NB_RESPUESTA, RPView4); break;
                        case "APTITUD1-D-0009": PintarRespuestasD(DPregunta9Resp1, DPregunta9Resp2, DPregunta9Resp3, DPregunta9Resp4, DPregunta9Resp5, resp.NB_RESPUESTA, RPView4); break;
                        case "APTITUD1-D-0010": PintarRespuestasD(DPregunta10Resp1, DPregunta10Resp2, DPregunta10Resp3, DPregunta10Resp4, DPregunta10Resp5, resp.NB_RESPUESTA, RPView4); break;
                        case "APTITUD1-D-0011": PintarRespuestasD(DPregunta11Resp1, DPregunta11Resp2, DPregunta11Resp3, DPregunta11Resp4, DPregunta11Resp5, resp.NB_RESPUESTA, RPView4); break;
                        case "APTITUD1-D-0012": PintarRespuestasD(DPregunta12Resp1, DPregunta12Resp2, DPregunta12Resp3, DPregunta12Resp4, DPregunta12Resp5, resp.NB_RESPUESTA, RPView4); break;
                        case "APTITUD1-D-0013": PintarRespuestasD(DPregunta13Resp1, DPregunta13Resp2, DPregunta13Resp3, DPregunta13Resp4, DPregunta13Resp5, resp.NB_RESPUESTA, RPView4); break;
                        case "APTITUD1-D-0014": PintarRespuestasD(DPregunta14Resp1, DPregunta14Resp2, DPregunta14Resp3, DPregunta14Resp4, DPregunta14Resp5, resp.NB_RESPUESTA, RPView4); break;
                        case "APTITUD1-D-0015": PintarRespuestasD(DPregunta15Resp1, DPregunta15Resp2, DPregunta15Resp3, DPregunta15Resp4, DPregunta15Resp5, resp.NB_RESPUESTA, RPView4); break;
                        case "APTITUD1-D-0016": PintarRespuestasD(DPregunta16Resp1, DPregunta16Resp2, DPregunta16Resp3, DPregunta16Resp4, DPregunta16Resp5, resp.NB_RESPUESTA, RPView4); break;
                        case "APTITUD1-D-0017": PintarRespuestasD(DPregunta17Resp1, DPregunta17Resp2, DPregunta17Resp3, DPregunta17Resp4, DPregunta17Resp5, resp.NB_RESPUESTA, RPView4); break;
                        case "APTITUD1-D-0018": PintarRespuestasD(DPregunta18Resp1, DPregunta18Resp2, DPregunta18Resp3, DPregunta18Resp4, DPregunta18Resp5, resp.NB_RESPUESTA, RPView4); break;

                        ///SECCION 5
                        case "APTITUD1-E-0001": EtxtPreg1Resp1.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-E-0002": EtxtPreg2Resp2.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-E-0003": EtxtPreg3Resp3.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-E-0004": EtxtPreg4Resp4.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-E-0005": EtxtPreg5Resp5.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-E-0006": EtxtPreg6Resp6.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-E-0007": EtxtPreg7Resp7.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-E-0008": EtxtPreg8Resp8.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-E-0009": EtxtPreg9Resp9.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-E-0010": EtxtPreg10Resp10.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-E-0011": EtxtPreg11Resp11.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-E-0012": EtxtPreg12Resp12.Text = resp.NB_RESPUESTA; break;

                        //SECCION 6
                        case "APTITUD1-F-0001": PintarRespuestasF(FtxtPreg1Resp1, FtxtPreg1Resp2, resp.NB_RESPUESTA, RPView6); break;
                        case "APTITUD1-F-0002": PintarRespuestasF(FtxtPreg2Resp1, FtxtPreg2Resp2, resp.NB_RESPUESTA, RPView6); break;
                        case "APTITUD1-F-0003": PintarRespuestasF(FtxtPreg3Resp1, FtxtPreg3Resp2, resp.NB_RESPUESTA, RPView6); break;
                        case "APTITUD1-F-0004": PintarRespuestasF(FtxtPreg4Resp1, FtxtPreg4Resp2, resp.NB_RESPUESTA, RPView6); break;
                        case "APTITUD1-F-0005": PintarRespuestasF(FtxtPreg5Resp1, FtxtPreg5Resp2, resp.NB_RESPUESTA, RPView6); break;
                        case "APTITUD1-F-0006": PintarRespuestasF(FtxtPreg6Resp1, FtxtPreg6Resp2, resp.NB_RESPUESTA, RPView6); break;
                        case "APTITUD1-F-0007": PintarRespuestasF(FtxtPreg7Resp1, FtxtPreg7Resp2, resp.NB_RESPUESTA, RPView6); break;
                        case "APTITUD1-F-0008": PintarRespuestasF(FtxtPreg8Resp1, FtxtPreg8Resp2, resp.NB_RESPUESTA, RPView6); break;
                        case "APTITUD1-F-0009": PintarRespuestasF(FtxtPreg9Resp1, FtxtPreg9Resp2, resp.NB_RESPUESTA, RPView6); break;
                        case "APTITUD1-F-0010": PintarRespuestasF(FtxtPreg10Resp1, FtxtPreg10Resp2, resp.NB_RESPUESTA, RPView6); break;
                        case "APTITUD1-F-0011": PintarRespuestasF(FtxtPreg11Resp1, FtxtPreg11Resp2, resp.NB_RESPUESTA, RPView6); break;
                        case "APTITUD1-F-0012": PintarRespuestasF(FtxtPreg12Resp1, FtxtPreg12Resp2, resp.NB_RESPUESTA, RPView6); break;
                        case "APTITUD1-F-0013": PintarRespuestasF(FtxtPreg13Resp1, FtxtPreg13Resp2, resp.NB_RESPUESTA, RPView6); break;
                        case "APTITUD1-F-0014": PintarRespuestasF(FtxtPreg14Resp1, FtxtPreg14Resp2, resp.NB_RESPUESTA, RPView6); break;
                        case "APTITUD1-F-0015": PintarRespuestasF(FtxtPreg15Resp1, FtxtPreg15Resp2, resp.NB_RESPUESTA, RPView6); break;
                        case "APTITUD1-F-0016": PintarRespuestasF(FtxtPreg16Resp1, FtxtPreg16Resp2, resp.NB_RESPUESTA, RPView6); break;
                        case "APTITUD1-F-0017": PintarRespuestasF(FtxtPreg17Resp1, FtxtPreg17Resp2, resp.NB_RESPUESTA, RPView6); break;
                        case "APTITUD1-F-0018": PintarRespuestasF(FtxtPreg18Resp1, FtxtPreg18Resp2, resp.NB_RESPUESTA, RPView6); break;
                        case "APTITUD1-F-0019": PintarRespuestasF(FtxtPreg19Resp1, FtxtPreg19Resp2, resp.NB_RESPUESTA, RPView6); break;
                        case "APTITUD1-F-0020": PintarRespuestasF(FtxtPreg20Resp1, FtxtPreg20Resp2, resp.NB_RESPUESTA, RPView6); break;

                        //SECCION 7
                        case "APTITUD1-G-0001": PintarRespuestasG(GbtnPreg1Resp1, GbtnPreg1Resp2, GbtnPreg1Resp3, GbtnPreg1Resp4, resp.NB_RESPUESTA, RPView7); break;
                        case "APTITUD1-G-0002": PintarRespuestasG(GbtnPreg2Resp1, GbtnPreg2Resp2, GbtnPreg2Resp3, GbtnPreg2Resp4, resp.NB_RESPUESTA, RPView7); break;
                        case "APTITUD1-G-0003": PintarRespuestasG(GbtnPreg3Resp1, GbtnPreg3Resp2, GbtnPreg3Resp3, GbtnPreg3Resp4, resp.NB_RESPUESTA, RPView7); break;
                        case "APTITUD1-G-0004": PintarRespuestasG(GbtnPreg4Resp1, GbtnPreg4Resp2, GbtnPreg4Resp3, GbtnPreg4Resp4, resp.NB_RESPUESTA, RPView7); break;
                        case "APTITUD1-G-0005": PintarRespuestasG(GbtnPreg5Resp1, GbtnPreg5Resp2, GbtnPreg5Resp3, GbtnPreg5Resp4, resp.NB_RESPUESTA, RPView7); break;
                        case "APTITUD1-G-0006": PintarRespuestasG(GbtnPreg6Resp1, GbtnPreg6Resp2, GbtnPreg6Resp3, GbtnPreg6Resp4, resp.NB_RESPUESTA, RPView7); break;
                        case "APTITUD1-G-0007": PintarRespuestasG(GbtnPreg7Resp1, GbtnPreg7Resp2, GbtnPreg7Resp3, GbtnPreg7Resp4, resp.NB_RESPUESTA, RPView7); break;
                        case "APTITUD1-G-0008": PintarRespuestasG(GbtnPreg8Resp1, GbtnPreg8Resp2, GbtnPreg8Resp3, GbtnPreg8Resp4, resp.NB_RESPUESTA, RPView7); break;
                        case "APTITUD1-G-0009": PintarRespuestasG(GbtnPreg9Resp1, GbtnPreg9Resp2, GbtnPreg9Resp3, GbtnPreg9Resp4, resp.NB_RESPUESTA, RPView7); break;
                        case "APTITUD1-G-0010": PintarRespuestasG(GbtnPreg10Resp1, GbtnPreg10Resp2, GbtnPreg10Resp3, GbtnPreg10Resp4, resp.NB_RESPUESTA, RPView7); break;
                        case "APTITUD1-G-0011": PintarRespuestasG(GbtnPreg11Resp1, GbtnPreg11Resp2, GbtnPreg11Resp3, GbtnPreg11Resp4, resp.NB_RESPUESTA, RPView7); break;
                        case "APTITUD1-G-0012": PintarRespuestasG(GbtnPreg12Resp1, GbtnPreg12Resp2, GbtnPreg12Resp3, GbtnPreg12Resp4, resp.NB_RESPUESTA, RPView7); break;
                        case "APTITUD1-G-0013": PintarRespuestasG(GbtnPreg13Resp1, GbtnPreg13Resp2, GbtnPreg13Resp3, GbtnPreg13Resp4, resp.NB_RESPUESTA, RPView7); break;
                        case "APTITUD1-G-0014": PintarRespuestasG(GbtnPreg14Resp1, GbtnPreg14Resp2, GbtnPreg14Resp3, GbtnPreg14Resp4, resp.NB_RESPUESTA, RPView7); break;
                        case "APTITUD1-G-0015": PintarRespuestasG(GbtnPreg15Resp1, GbtnPreg15Resp2, GbtnPreg15Resp3, GbtnPreg15Resp4, resp.NB_RESPUESTA, RPView7); break;
                        case "APTITUD1-G-0016": PintarRespuestasG(GbtnPreg16Resp1, GbtnPreg16Resp2, GbtnPreg16Resp3, GbtnPreg16Resp4, resp.NB_RESPUESTA, RPView7); break;
                        case "APTITUD1-G-0017": PintarRespuestasG(GbtnPreg17Resp1, GbtnPreg17Resp2, GbtnPreg17Resp3, GbtnPreg17Resp4, resp.NB_RESPUESTA, RPView7); break;
                        case "APTITUD1-G-0018": PintarRespuestasG(GbtnPreg18Resp1, GbtnPreg18Resp2, GbtnPreg18Resp3, GbtnPreg18Resp4, resp.NB_RESPUESTA, RPView7); break;
                        case "APTITUD1-G-0019": PintarRespuestasG(GbtnPreg19Resp1, GbtnPreg19Resp2, GbtnPreg19Resp3, GbtnPreg19Resp4, resp.NB_RESPUESTA, RPView7); break;
                        case "APTITUD1-G-0020": PintarRespuestasG(GbtnPreg20Resp1, GbtnPreg20Resp2, GbtnPreg20Resp3300, GbtnPreg20Resp4, resp.NB_RESPUESTA, RPView7); break;

                        //SECCION 8
                        case "APTITUD1-H-0001": PintarRespuestasH(HbtnPreg1Resp1, HbtnPreg1Resp2, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-H-0002": PintarRespuestasH(HbtnPreg2Resp1, HbtnPreg2Resp2, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-H-0003": PintarRespuestasH(HbtnPreg3Resp1, HbtnPreg3Resp2, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-H-0004": PintarRespuestasH(HbtnPreg4Resp1, HbtnPreg4Resp2, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-H-0005": PintarRespuestasH(HbtnPreg5Resp1, HbtnPreg5Resp2, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-H-0006": PintarRespuestasH(HbtnPreg6Resp1, HbtnPreg6Resp2, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-H-0007": PintarRespuestasH(HbtnPreg7Resp1, HbtnPreg7Resp2, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-H-0008": PintarRespuestasH(HbtnPreg8Resp1, HbtnPreg8Resp2, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-H-0009": PintarRespuestasH(HbtnPreg9Resp1, HbtnPreg9Resp2, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-H-0010": PintarRespuestasH(HbtnPreg10Resp1, HbtnPreg10Resp2, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-H-0011": PintarRespuestasH(HbtnPreg11Resp1, HbtnPreg11Resp2, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-H-0012": PintarRespuestasH(HbtnPreg12Resp1, HbtnPreg12Resp2, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-H-0013": PintarRespuestasH(HbtnPreg13Resp1, HbtnPreg13Resp2, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-H-0014": PintarRespuestasH(HbtnPreg14Resp1, HbtnPreg14Resp2, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-H-0015": PintarRespuestasH(HbtnPreg15Resp1, HbtnPreg15Resp2, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-H-0016": PintarRespuestasH(HbtnPreg16Resp1, HbtnPreg16Resp2, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-H-0017": PintarRespuestasH(HbtnPreg17Resp1, HbtnPreg17Resp2, resp.NB_RESPUESTA, RPView8); break;

                        //SECCION 9

                        case "APTITUD1-I-0001": PintarRespuestasI(IbtnPreg1Resp1, IbtnPreg1Resp2, IbtnPreg1Resp3, IbtnPreg1Resp4, IbtnPreg1Resp5, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-I-0002": PintarRespuestasI(IbtnPreg2Resp1, IbtnPreg2Resp2, IbtnPreg2Resp3, IbtnPreg2Resp4, IbtnPreg2Resp5, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-I-0003": PintarRespuestasI(IbtnPreg3Resp1, IbtnPreg3Resp2, IbtnPreg3Resp3, IbtnPreg3Resp4, IbtnPreg3Resp5, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-I-0004": PintarRespuestasI(IbtnPreg4Resp1, IbtnPreg4Resp2, IbtnPreg4Resp3, IbtnPreg4Resp4, IbtnPreg4Resp5, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-I-0005": PintarRespuestasI(IbtnPreg5Resp1, IbtnPreg5Resp2, IbtnPreg5Resp3, IbtnPreg5Resp4, IbtnPreg5Resp5, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-I-0006": PintarRespuestasI(IbtnPreg6Resp1, IbtnPreg6Resp2, IbtnPreg6Resp3, IbtnPreg6Resp4, IbtnPreg6Resp5, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-I-0007": PintarRespuestasI(IbtnPreg7Resp1, IbtnPreg7Resp2, IbtnPreg7Resp3, IbtnPreg7Resp4, IbtnPreg7Resp5, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-I-0008": PintarRespuestasI(IbtnPreg8Resp1, IbtnPreg8Resp2, IbtnPreg8Resp3, IbtnPreg8Resp4, IbtnPreg8Resp5, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-I-0009": PintarRespuestasI(IbtnPreg9Resp1, IbtnPreg9Resp2, IbtnPreg9Resp3, IbtnPreg9Resp4, IbtnPreg9Resp5, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-I-0010": PintarRespuestasI(IbtnPreg10Resp1, IbtnPreg10Resp2, IbtnPreg10Resp3, IbtnPreg10Resp4, IbtnPreg10Resp5, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-I-0011": PintarRespuestasI(IbtnPreg11Resp1, IbtnPreg11Resp2, IbtnPreg11Resp3, IbtnPreg11Resp4, IbtnPreg11Resp5, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-I-0012": PintarRespuestasI(IbtnPreg12Resp1, IbtnPreg12Resp2, IbtnPreg12Resp3, IbtnPreg12Resp4, IbtnPreg12Resp5, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-I-0013": PintarRespuestasI(IbtnPreg13Resp1, IbtnPreg13Resp2, IbtnPreg13Resp3, IbtnPreg13Resp4, IbtnPreg13Resp5, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-I-0014": PintarRespuestasI(IbtnPreg14Resp1, IbtnPreg14Resp2, IbtnPreg14Resp3, IbtnPreg14Resp4, IbtnPreg14Resp5, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-I-0015": PintarRespuestasI(IbtnPreg15Resp1, IbtnPreg15Resp2, IbtnPreg15Resp3, IbtnPreg15Resp4, IbtnPreg15Resp5, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-I-0016": PintarRespuestasI(IbtnPreg16Resp1, IbtnPreg16Resp2, IbtnPreg16Resp3, IbtnPreg16Resp4, IbtnPreg16Resp5, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-I-0017": PintarRespuestasI(IbtnPreg17Resp1, IbtnPreg17Resp2, IbtnPreg17Resp3, IbtnPreg17Resp4, IbtnPreg17Resp5, resp.NB_RESPUESTA, RPView8); break;
                        case "APTITUD1-I-0018": PintarRespuestasI(IbtnPreg18Resp1, IbtnPreg18Resp2, IbtnPreg18Resp3, IbtnPreg18Resp4, IbtnPreg18Resp5, resp.NB_RESPUESTA, RPView8); break;

                        //SECCION 10
                        /*
                        case "APTITUD1-J-0001": JbtnPreg1Resp1.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-J-0002": JbtnPreg1Resp2.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-J-0003": JbtnPreg2Resp1.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-J-0004": JbtnPreg2Resp2.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-J-0005": JbtnPreg3Resp1.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-J-0006": JbtnPreg3Resp2.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-J-0007": JbtnPreg4Resp1.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-J-0008": JbtnPreg4Resp2.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-J-0009": JbtnPreg5Resp1.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-J-0010": JbtnPreg5Resp2.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-J-0011": JbtnPreg6Resp1.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-J-0012": JbtnPreg6Resp2.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-J-0013": JbtnPreg7Resp1.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-J-0014": JbtnPreg7Resp2.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-J-0015": JbtnPreg8Resp1.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-J-0016": JbtnPreg8Resp2.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-J-0017": JbtnPreg9Resp1.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-J-0018": JbtnPreg9Resp2.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-J-0019": JbtnPreg10Resp1.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-J-0020": JbtnPreg10Resp2.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-J-0021": JbtnPreg11Resp1.Text = resp.NB_RESPUESTA; break;
                        case "APTITUD1-J-0022": JbtnPreg11Resp2.Text = resp.NB_RESPUESTA; break;*/
                        default: break;
                    }
                }
            }

            if (vPruebaSeccion10 != null)
            {
                foreach (SPE_OBTIENE_K_CUESTIONARIO_PREGUNTA_PPRUEBA_Result resp in vPruebaSeccion10)
                {
                    switch (resp.NB_PREGUNTA)
                    {
                        case "Pregunta 1": JbtnPreg1Resp1.Text = resp.NB_RESPUESTA; break;
                        case "Pregunta 2": JbtnPreg1Resp2.Text = resp.NB_RESPUESTA; break;
                        case "Pregunta 3": JbtnPreg2Resp1.Text = resp.NB_RESPUESTA; break;
                        case "Pregunta 4": JbtnPreg2Resp2.Text = resp.NB_RESPUESTA; break;
                        case "Pregunta 5": JbtnPreg3Resp1.Text = resp.NB_RESPUESTA; break;
                        case "Pregunta 6": JbtnPreg3Resp2.Text = resp.NB_RESPUESTA; break;
                        case "Pregunta 7": JbtnPreg4Resp1.Text = resp.NB_RESPUESTA; break;
                        case "Pregunta 8": JbtnPreg4Resp2.Text = resp.NB_RESPUESTA; break;
                        case "Pregunta 9": JbtnPreg5Resp1.Text = resp.NB_RESPUESTA; break;
                        case "Pregunta 10": JbtnPreg5Resp2.Text = resp.NB_RESPUESTA; break;
                        case "Pregunta 11": JbtnPreg6Resp1.Text = resp.NB_RESPUESTA; break;
                        case "Pregunta 12": JbtnPreg6Resp2.Text = resp.NB_RESPUESTA; break;
                        case "Pregunta 13": JbtnPreg7Resp1.Text = resp.NB_RESPUESTA; break;
                        case "Pregunta 14": JbtnPreg7Resp2.Text = resp.NB_RESPUESTA; break;
                        case "Pregunta 15": JbtnPreg8Resp1.Text = resp.NB_RESPUESTA; break;
                        case "Pregunta 16": JbtnPreg8Resp2.Text = resp.NB_RESPUESTA; break;
                        case "Pregunta 17": JbtnPreg9Resp1.Text = resp.NB_RESPUESTA; break;
                        case "Pregunta 18": JbtnPreg9Resp2.Text = resp.NB_RESPUESTA; break;
                        case "Pregunta 19": JbtnPreg10Resp1.Text = resp.NB_RESPUESTA; break;
                        case "Pregunta 20": JbtnPreg10Resp2.Text = resp.NB_RESPUESTA; break;
                        case "Pregunta 21": JbtnPreg11Resp1.Text = resp.NB_RESPUESTA; break;
                        case "Pregunta 22": JbtnPreg11Resp2.Text = resp.NB_RESPUESTA; break;
                        default: break;
                    }
                }
            }
        }

        public void PintarRespuestasA(RadButton a, RadButton b, RadButton c, RadButton d, string pAnswer, RadPageView v)
        {
            if (pAnswer != null)
            {
                RadButton t;
                switch (pAnswer.ToUpper())
                {
                    case "A": t = v.FindControl(a.ID) as RadButton; t.Checked = true; break;
                    case "B": t = v.FindControl(b.ID) as RadButton; t.Checked = true; break;
                    case "C": t = v.FindControl(c.ID) as RadButton; t.Checked = true; break;
                    case "D": t = v.FindControl(d.ID) as RadButton; t.Checked = true; break;
                    default: break;
                }
            }
        }

        public void PintarRespuestasB(RadButton a, RadButton b, RadButton c, string pAnswer, RadPageView v)
        {
            if (pAnswer != null)
            {
                RadButton t;
                switch (pAnswer.ToUpper())
                {
                    case "A": t = v.FindControl(a.ID) as RadButton; t.Checked = true; break;
                    case "B": t = v.FindControl(b.ID) as RadButton; t.Checked = true; break;
                    case "C": t = v.FindControl(c.ID) as RadButton; t.Checked = true; break;
                    default: break;
                }
            }
        }

        public void PintarRespuestasC(RadButton a, RadButton b, string pAnswer, RadPageView v)
        {
            RadButton t;
            switch (pAnswer)
            {
                case "I": t = v.FindControl(a.ID) as RadButton; t.Checked = true; break;
                case "O": t = v.FindControl(b.ID) as RadButton; t.Checked = true; break;
                default: break;
            }
        }

        public void PintarRespuestasD(RadButton a, RadButton b, RadButton c, RadButton d, RadButton e, string pAnswer, RadPageView v)
        {
            if (!string.IsNullOrEmpty(pAnswer))
            {
                RadButton t;
                string vrespuesta1 = pAnswer.Substring(0, 1);
                string vrespuesta2 = pAnswer.Replace(vrespuesta1, "");

                List<String> listaRespuestas = new List<string>();

                listaRespuestas.Add(vrespuesta1.Trim());
                listaRespuestas.Add(vrespuesta2.Trim());
                foreach (var item in listaRespuestas)
                {
                    switch (item.ToUpper())
                    {
                        case "A": t = v.FindControl(a.ID) as RadButton; t.Checked = true; break;
                        case "B": t = v.FindControl(b.ID) as RadButton; t.Checked = true; break;
                        case "C": t = v.FindControl(c.ID) as RadButton; t.Checked = true; break;
                        case "D": t = v.FindControl(d.ID) as RadButton; t.Checked = true; break;
                        case "E": t = v.FindControl(e.ID) as RadButton; t.Checked = true; break;
                        default: break;
                    }
                }
            }
        }

        public void PintarRespuestasF(RadButton a, RadButton b, string pAnswer, RadPageView v)
        {
            RadButton t;
            switch (pAnswer)
            {
                case "1": t = v.FindControl(a.ID) as RadButton; t.Checked = true; break;
                case "0": t = v.FindControl(b.ID) as RadButton; t.Checked = true; break;
                default: break;
            }
        }

        public void PintarRespuestasG(RadButton a, RadButton b, RadButton c, RadButton d, string pAnswer, RadPageView v)
        {
            if (!string.IsNullOrEmpty(pAnswer))
            {
                RadButton t;
                switch (pAnswer.ToUpper())
                {
                    case "A": t = v.FindControl(a.ID) as RadButton; t.Checked = true; break;
                    case "B": t = v.FindControl(b.ID) as RadButton; t.Checked = true; break;
                    case "C": t = v.FindControl(c.ID) as RadButton; t.Checked = true; break;
                    case "D": t = v.FindControl(d.ID) as RadButton; t.Checked = true; break;
                    default: break;
                }   
            }
        }

        public void PintarRespuestasH(RadButton a, RadButton b, string pAnswer, RadPageView v)
        {
            RadButton t;
            switch (pAnswer)
            {
                case "V": t = v.FindControl(a.ID) as RadButton; t.Checked = true; break;
                case "F": t = v.FindControl(b.ID) as RadButton; t.Checked = true; break;
                default: break;
            }
        }

        public void PintarRespuestasI(RadButton a, RadButton b, RadButton c, RadButton d, RadButton e, string pAnswer, RadPageView v)
        {
            if (pAnswer != null)
            {
                RadButton t;
                switch (pAnswer.ToUpper())
                {
                    case "A": t = v.FindControl(a.ID) as RadButton; t.Checked = true; break;
                    case "B": t = v.FindControl(b.ID) as RadButton; t.Checked = true; break;
                    case "C": t = v.FindControl(c.ID) as RadButton; t.Checked = true; break;
                    case "D": t = v.FindControl(d.ID) as RadButton; t.Checked = true; break;
                    case "E": t = v.FindControl(e.ID) as RadButton; t.Checked = true; break;
                    default: break;
                }
            }
        }

        protected void tbMentalISecciones_TabClick(object sender, RadTabStripEventArgs e)
        {
            mpgActitudMentalI.SelectedIndex = e.Tab.Index;
            asignarValores(vResultadosRevision.Where(item => item.CL_PREGUNTA.Contains("APTITUD1-" + TraerLetraSeccion(mpgActitudMentalI.SelectedIndex) + "-")).ToList());
            //texto.InnerHtml = instrucciones(e.Tab.Index);
        }

        public void habilitarResultadosAptitudMentalI(List<E_RESULTADOS_PRUEBA> lista)
        {
            if (lista.Count > 0)
            {
                tbActitudMentalI.Tabs.ElementAt(0).Visible = true;
                tbActitudMentalI.Tabs.ElementAt(1).Visible = true;
                tbActitudMentalI.Tabs.ElementAt(2).Visible = true;
                tbActitudMentalI.Tabs.ElementAt(3).Visible = true;
                tbActitudMentalI.Tabs.ElementAt(4).Visible = true;
                tbActitudMentalI.Tabs.ElementAt(5).Visible = true;
                tbActitudMentalI.Tabs.ElementAt(6).Visible = true;
                tbActitudMentalI.Tabs.ElementAt(7).Visible = true;
                tbActitudMentalI.Tabs.ElementAt(8).Visible = true;
                tbActitudMentalI.Tabs.ElementAt(9).Visible = true;
            }
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            PruebasNegocio nKprueba = new PruebasNegocio();
            var vSeccionTermina = vSeccionesPrueba.ElementAt(mpgActitudMentalI.SelectedIndex);
            if (vTipoRevision == "REV")
            {

            }
            else
            {
                vSeccionTermina.CL_ESTADO = E_ESTADO_PRUEBA.TERMINADA.ToString();
                vSeccionTermina.FE_TERMINO = DateTime.Now;
            }
            E_RESULTADO vResultadoSeccion = nKprueba.InsertaActualiza_K_PRUEBA_SECCION(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), v_k_prueba: vSeccionTermina, usuario: vClUsuario, programa: vNbPrograma);
            //    if (vResultadoSeccion.CL_TIPO_ERROR != E_TIPO_RESPUESTA_DB.WARNING)
            if (vTipoRevision == "EDIT")
                EditTest(mpgActitudMentalI.SelectedIndex);
            else
                SaveTest(mpgActitudMentalI.SelectedIndex);
            //    else
            //    {
            //        string vMensaje = vResultadoSeccion.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            //        UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultadoSeccion.CL_TIPO_ERROR, 400, 150, "");
            //    }
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

        protected void btnEliminarBateria_Click(object sender, EventArgs e)
        {
            PruebasNegocio nPruebas = new PruebasNegocio();
            var vResultado = nPruebas.EliminaRespuestasBaterias(vIdBateria, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "s");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
            }
        }

        //protected void btnCorregir_Click(object sender, EventArgs e)
        //{

        //}

    }
}
