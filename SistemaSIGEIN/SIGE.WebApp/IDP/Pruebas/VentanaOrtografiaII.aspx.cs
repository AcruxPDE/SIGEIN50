using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.IDP
{
    public partial class VentanaOrtografia2 : System.Web.UI.Page
    {
        #region Propiedades
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
            get { return (int)ViewState["vsOrtografia2seconds"]; }
            set { ViewState["vsOrtografia2seconds"] = value; }
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
            get { return (bool)ViewState["vsMostrarCronometroO2"]; }
            set { ViewState["vsMostrarCronometroO2"] = value; }
        }

        public int comboResp;
        #endregion
        public int vIdBateria
        {
            get { return (int)ViewState["vsIdBateria"]; }
            set { ViewState["vsIdBateria"] = value; }
        }

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
                        cronometro.Visible = false;
                        vTiempoPrueba = 0;
                        btnTerminar.Visible = false;
                        //btnEliminar.Visible = true;// Se agrega para la nueva forma de navegación 06/06/2018
                        btnImpresionPrueba.Visible = true; // Se agrega para imprimir en la nueva navegación IDP 06/06/2018
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
                        ////Si el modo de revision esta activado
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


            String ORTOGRAFIA2_A_0001 = cmbPregunta1.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0001", ORTOGRAFIA2_A_0001, txtPregunta1, "1");

            String ORTOGRAFIA2_A_0002 = cmbPregunta2.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0002", ORTOGRAFIA2_A_0002, txtPregunta2, "exhibición");

            String ORTOGRAFIA2_A_0003 = cmbPregunta3.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0003", ORTOGRAFIA2_A_0003, txtPregunta3, "manantial");

            String ORTOGRAFIA2_A_0004 = cmbPregunta4.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0004", ORTOGRAFIA2_A_0004, txtPregunta4, "1");

            String ORTOGRAFIA2_A_0005 = cmbPregunta5.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0005", ORTOGRAFIA2_A_0005, txtPregunta5, "embotellar");

            String ORTOGRAFIA2_A_0006 = cmbPregunta6.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0006", ORTOGRAFIA2_A_0006, txtPregunta6, "1");

            String ORTOGRAFIA2_A_0007 = cmbPregunta7.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0007", ORTOGRAFIA2_A_0007, txtPregunta7, "atlético");

            String ORTOGRAFIA2_A_0008 = cmbPregunta8.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0008", ORTOGRAFIA2_A_0008, txtPregunta8, "1");

            String ORTOGRAFIA2_A_0009 = cmbPregunta9.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0009", ORTOGRAFIA2_A_0009, txtPregunta9, "hermético");

            String ORTOGRAFIA2_A_0010 = cmbPregunta10.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0010", ORTOGRAFIA2_A_0010, txtPregunta10, "exhaustivo");

            String ORTOGRAFIA2_A_0011 = cmbPregunta11.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0011", ORTOGRAFIA2_A_0011, txtPregunta11, "excelente");

            String ORTOGRAFIA2_A_0012 = cmbPregunta12.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0012", ORTOGRAFIA2_A_0012, txtPregunta12, "1");

            String ORTOGRAFIA2_A_0013 = cmbPregunta13.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0013", ORTOGRAFIA2_A_0013, txtPregunta13, "hallar");

            String ORTOGRAFIA2_A_0014 = cmbPregunta14.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0014", ORTOGRAFIA2_A_0014, txtPregunta14, "innecesario");

            String ORTOGRAFIA2_A_0015 = cmbPregunta15.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0015", ORTOGRAFIA2_A_0015, txtPregunta15, "1");

            String ORTOGRAFIA2_A_0016 = cmbPregunta16.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0016", ORTOGRAFIA2_A_0016, txtPregunta16, "aeropuerto");

            String ORTOGRAFIA2_A_0017 = cmbPregunta17.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0017", ORTOGRAFIA2_A_0017, txtPregunta17, "1");

            String ORTOGRAFIA2_A_0018 = cmbPregunta18.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0018", ORTOGRAFIA2_A_0018, txtPregunta18, "1");

            String ORTOGRAFIA2_A_0019 = cmbPregunta19.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0019", ORTOGRAFIA2_A_0019, txtPregunta19, "defectuoso");

            String ORTOGRAFIA2_A_0020 = cmbPregunta20.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0020", ORTOGRAFIA2_A_0020, txtPregunta20, "1");

            String ORTOGRAFIA2_A_0021 = cmbPregunta21.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0021", ORTOGRAFIA2_A_0021, txtPregunta21, "simultáneo");

            String ORTOGRAFIA2_A_0022 = cmbPregunta22.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0022", ORTOGRAFIA2_A_0022, txtPregunta22, "hinchar");

            String ORTOGRAFIA2_A_0023 = cmbPregunta23.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0023", ORTOGRAFIA2_A_0023, txtPregunta23, "empapar");

            String ORTOGRAFIA2_A_0024 = cmbPregunta24.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0024", ORTOGRAFIA2_A_0024, txtPregunta24, "1");

            String ORTOGRAFIA2_A_0025 = cmbPregunta25.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0025", ORTOGRAFIA2_A_0025, txtPregunta25, "1");

            var vXelements = vRespuestas.Select(x =>
                                                 new XElement("RESPUESTA",
                                                 new XAttribute("ID_CUESTIONARIO_PREGUNTA", x.ID_CUESTIONARIO_PREGUNTA),
                                                 new XAttribute("ID_PREGUNTA", x.ID_PREGUNTA),
                                                 new XAttribute("NB_PREGUNTA", x.NB_PREGUNTA),
                                                 new XAttribute("CL_PREGUNTA", x.CL_PREGUNTA),
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
                E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vObjetoPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: "ORTOGRAFIA-2", usuario: vClUsuario, programa: vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "CloseTest");
            }
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


            String ORTOGRAFIA2_A_0001 = cmbPregunta1.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0001", ORTOGRAFIA2_A_0001, txtPregunta1, "1");

            String ORTOGRAFIA2_A_0002 = cmbPregunta2.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0002", ORTOGRAFIA2_A_0002, txtPregunta2, "exhibición");

            String ORTOGRAFIA2_A_0003 = cmbPregunta3.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0003", ORTOGRAFIA2_A_0003, txtPregunta3, "manantial");

            String ORTOGRAFIA2_A_0004 = cmbPregunta4.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0004", ORTOGRAFIA2_A_0004, txtPregunta4, "1");

            String ORTOGRAFIA2_A_0005 = cmbPregunta5.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0005", ORTOGRAFIA2_A_0005, txtPregunta5, "embotellar");

            String ORTOGRAFIA2_A_0006 = cmbPregunta6.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0006", ORTOGRAFIA2_A_0006, txtPregunta6, "1");

            String ORTOGRAFIA2_A_0007 = cmbPregunta7.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0007", ORTOGRAFIA2_A_0007, txtPregunta7, "atlético");

            String ORTOGRAFIA2_A_0008 = cmbPregunta8.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0008", ORTOGRAFIA2_A_0008, txtPregunta8, "1");

            String ORTOGRAFIA2_A_0009 = cmbPregunta9.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0009", ORTOGRAFIA2_A_0009, txtPregunta9, "hermético");

            String ORTOGRAFIA2_A_0010 = cmbPregunta10.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0010", ORTOGRAFIA2_A_0010, txtPregunta10, "exhaustivo");

            String ORTOGRAFIA2_A_0011 = cmbPregunta11.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0011", ORTOGRAFIA2_A_0011, txtPregunta11, "excelente");

            String ORTOGRAFIA2_A_0012 = cmbPregunta12.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0012", ORTOGRAFIA2_A_0012, txtPregunta12, "1");

            String ORTOGRAFIA2_A_0013 = cmbPregunta13.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0013", ORTOGRAFIA2_A_0013, txtPregunta13, "hallar");

            String ORTOGRAFIA2_A_0014 = cmbPregunta14.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0014", ORTOGRAFIA2_A_0014, txtPregunta14, "innecesario");

            String ORTOGRAFIA2_A_0015 = cmbPregunta15.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0015", ORTOGRAFIA2_A_0015, txtPregunta15, "1");

            String ORTOGRAFIA2_A_0016 = cmbPregunta16.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0016", ORTOGRAFIA2_A_0016, txtPregunta16, "aeropuerto");

            String ORTOGRAFIA2_A_0017 = cmbPregunta17.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0017", ORTOGRAFIA2_A_0017, txtPregunta17, "1");

            String ORTOGRAFIA2_A_0018 = cmbPregunta18.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0018", ORTOGRAFIA2_A_0018, txtPregunta18, "1");

            String ORTOGRAFIA2_A_0019 = cmbPregunta19.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0019", ORTOGRAFIA2_A_0019, txtPregunta19, "defectuoso");

            String ORTOGRAFIA2_A_0020 = cmbPregunta20.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0020", ORTOGRAFIA2_A_0020, txtPregunta20, "1");

            String ORTOGRAFIA2_A_0021 = cmbPregunta21.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0021", ORTOGRAFIA2_A_0021, txtPregunta21, "simultáneo");

            String ORTOGRAFIA2_A_0022 = cmbPregunta22.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0022", ORTOGRAFIA2_A_0022, txtPregunta22, "hinchar");

            String ORTOGRAFIA2_A_0023 = cmbPregunta23.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0023", ORTOGRAFIA2_A_0023, txtPregunta23, "empapar");

            String ORTOGRAFIA2_A_0024 = cmbPregunta24.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0024", ORTOGRAFIA2_A_0024, txtPregunta24, "1");

            String ORTOGRAFIA2_A_0025 = cmbPregunta25.SelectedValue;
            BackQuestionObject("ORTOGRAFIA2-A-0025", ORTOGRAFIA2_A_0025, txtPregunta25, "1");

            var vXelements = vRespuestas.Select(x =>
                                                 new XElement("RESPUESTA",
                                                 new XAttribute("ID_CUESTIONARIO_PREGUNTA", x.ID_CUESTIONARIO_PREGUNTA),
                                                 new XAttribute("ID_PREGUNTA", x.ID_PREGUNTA),
                                                 new XAttribute("NB_PREGUNTA", x.NB_PREGUNTA),
                                                 new XAttribute("CL_PREGUNTA", x.CL_PREGUNTA),
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
                E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vObjetoPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: "ORTOGRAFIA-2", usuario: vClUsuario, programa: vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
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
                SaveTest();
        }

        public void BackQuestionObject(string pclPregunta, string pnbRespuesta, RadTextBox a, string nbRespuestaCorrecta)
        {
            String vRespuesta = "";
            decimal vNoValor = 0;
            var vPregunta = vRespuestas.Where(x => x.CL_PREGUNTA.Equals(pclPregunta)).FirstOrDefault();
            if (vPregunta != null)
            {
                if (pnbRespuesta.Equals("C") && nbRespuestaCorrecta.Equals("1"))
                {
                    vNoValor = 1;
                }
                else if (pnbRespuesta.Equals("I") && a.Text.ToLower().Equals(nbRespuestaCorrecta))
                {
                    vNoValor = 1;
                    vRespuesta = nbRespuestaCorrecta;
                }
                else
                {
                    vNoValor = 0;
                    vRespuesta = a.Text.ToLower();
                }
                List<E_ORTOGRAFIA_II> respuestaValor = new List<E_ORTOGRAFIA_II>();
                respuestaValor.Add(new E_ORTOGRAFIA_II { VALOR = pnbRespuesta, NB_RESPUESTA = a.Text });
                var vXelements = respuestaValor.Select(x =>
                                        new XElement("ANSWER",
                                        new XAttribute("VALOR", x.VALOR),
                                        new XAttribute("NB_RESPUESTA", x.NB_RESPUESTA)
                             ));

                XElement RESPUESTAS =
                new XElement("RESPUESTAS", vXelements
                );
                vPregunta.NB_RESPUESTA = RESPUESTAS.ToString();
                vPregunta.NO_VALOR_RESPUESTA = vNoValor;
            }
        }

        public void asignarValores(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> respuestas)
        {
            foreach (SPE_OBTIENE_RESULTADO_PRUEBA_Result resp in respuestas)
            {
                switch (resp.CL_PREGUNTA)
                {
                    case "ORTOGRAFIA2-A-0001": MostrarRespuestas(cmbPregunta1, txtPregunta1, resp.NB_RESPUESTA); break;
                    case "ORTOGRAFIA2-A-0002": MostrarRespuestas(cmbPregunta2, txtPregunta2, resp.NB_RESPUESTA); break;
                    case "ORTOGRAFIA2-A-0003": MostrarRespuestas(cmbPregunta3, txtPregunta3, resp.NB_RESPUESTA); break;
                    case "ORTOGRAFIA2-A-0004": MostrarRespuestas(cmbPregunta4, txtPregunta4, resp.NB_RESPUESTA); break;
                    case "ORTOGRAFIA2-A-0005": MostrarRespuestas(cmbPregunta5, txtPregunta5, resp.NB_RESPUESTA); break;
                    case "ORTOGRAFIA2-A-0006": MostrarRespuestas(cmbPregunta6, txtPregunta6, resp.NB_RESPUESTA); break;
                    case "ORTOGRAFIA2-A-0007": MostrarRespuestas(cmbPregunta7, txtPregunta7, resp.NB_RESPUESTA); break;
                    case "ORTOGRAFIA2-A-0008": MostrarRespuestas(cmbPregunta8, txtPregunta8, resp.NB_RESPUESTA); break;
                    case "ORTOGRAFIA2-A-0009": MostrarRespuestas(cmbPregunta9, txtPregunta9, resp.NB_RESPUESTA); break;
                    case "ORTOGRAFIA2-A-0010": MostrarRespuestas(cmbPregunta10, txtPregunta10, resp.NB_RESPUESTA); break;
                    case "ORTOGRAFIA2-A-0011": MostrarRespuestas(cmbPregunta11, txtPregunta11, resp.NB_RESPUESTA); break;
                    case "ORTOGRAFIA2-A-0012": MostrarRespuestas(cmbPregunta12, txtPregunta12, resp.NB_RESPUESTA); break;
                    case "ORTOGRAFIA2-A-0013": MostrarRespuestas(cmbPregunta13, txtPregunta13, resp.NB_RESPUESTA); break;
                    case "ORTOGRAFIA2-A-0014": MostrarRespuestas(cmbPregunta14, txtPregunta14, resp.NB_RESPUESTA); break;
                    case "ORTOGRAFIA2-A-0015": MostrarRespuestas(cmbPregunta15, txtPregunta15, resp.NB_RESPUESTA); break;
                    case "ORTOGRAFIA2-A-0016": MostrarRespuestas(cmbPregunta16, txtPregunta16, resp.NB_RESPUESTA); break;
                    case "ORTOGRAFIA2-A-0017": MostrarRespuestas(cmbPregunta17, txtPregunta17, resp.NB_RESPUESTA); break;
                    case "ORTOGRAFIA2-A-0018": MostrarRespuestas(cmbPregunta18, txtPregunta18, resp.NB_RESPUESTA); break;
                    case "ORTOGRAFIA2-A-0019": MostrarRespuestas(cmbPregunta19, txtPregunta19, resp.NB_RESPUESTA); break;
                    case "ORTOGRAFIA2-A-0020": MostrarRespuestas(cmbPregunta20, txtPregunta20, resp.NB_RESPUESTA); break;
                    case "ORTOGRAFIA2-A-0021": MostrarRespuestas(cmbPregunta21, txtPregunta21, resp.NB_RESPUESTA); break;
                    case "ORTOGRAFIA2-A-0022": MostrarRespuestas(cmbPregunta22, txtPregunta22, resp.NB_RESPUESTA); break;
                    case "ORTOGRAFIA2-A-0023": MostrarRespuestas(cmbPregunta23, txtPregunta23, resp.NB_RESPUESTA); break;
                    case "ORTOGRAFIA2-A-0024": MostrarRespuestas(cmbPregunta24, txtPregunta24, resp.NB_RESPUESTA); break;
                    case "ORTOGRAFIA2-A-0025": MostrarRespuestas(cmbPregunta25, txtPregunta25, resp.NB_RESPUESTA); break;
                }
            }
        }

        public void MostrarRespuestas(RadComboBox cmb, RadTextBox txt, string pAnswer)
        {
            if (pAnswer != null)
            {
                XElement pRespuesta = XElement.Parse(pAnswer);
                List<E_ORTOGRAFIA_II> vListaRespuestas = pRespuesta.Elements("ANSWER").Select(el => new E_ORTOGRAFIA_II
                {
                    VALOR = el.Attribute("VALOR").Value,
                    NB_RESPUESTA = el.Attribute("NB_RESPUESTA").Value
                }).ToList();

                if (vListaRespuestas.Count != null)
                {
                    if (vListaRespuestas.FirstOrDefault().VALOR == "C")
                    {
                        comboResp = 0;
                        cmb.SelectedIndex = comboResp;
                    }
                    else if (vListaRespuestas.FirstOrDefault().VALOR == "I")
                    {
                        comboResp = 1;
                        cmb.SelectedIndex = comboResp;
                    }
                    else
                    {
                        cmb.SelectedValue = "";
                    }

                    txt.Text = vListaRespuestas.FirstOrDefault().NB_RESPUESTA;
                }
            }
        }

        [Serializable]
        public class E_ORTOGRAFIA_II
        {
            public string VALOR { get; set; }
            public string NB_RESPUESTA { get; set; }
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
                EditTest();
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