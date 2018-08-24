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

    public partial class VentanaPersonalidadLaboral1 : System.Web.UI.Page
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
            get { return (bool)ViewState["vsMostrarCronometroPL1"]; }
            set { ViewState["vsMostrarCronometroPL1"] = value; }
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
                        //divCandidato.Visible = true;
                       
                        vTipoRevision = Request.QueryString["MOD"];
                    }
                    //else
                    //{
                    //    divCandidato.Visible = false;
                    //}
                    PruebasNegocio nKprueba = new PruebasNegocio();
                    vIdPrueba = int.Parse(Request.QueryString["ID"]);
                    vClToken = new Guid(Request.QueryString["T"]);

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
                        btnCorregir.Visible = true;
                        var respuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);
                        asignarValores(respuestas);
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
                        //    btnTerminar.Visible= false;
                        //    btnCorregir.Visible = true;
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

            int vLABORAL1_A_0001M = BackSelectedCheckBox(btnPersuasivo1, btnGentil1, btnHumilde1, btnOriginal1);
            int vLABORAL1_B_0001L = BackSelectedCheckBox(btnPersuasivo2, btnGentil2, btnHumilde2, btnOriginal2);
            BackQuestionObject("LABORAL1-A-0001", "LABORAL1-RES-A01", vLABORAL1_A_0001M);
            BackQuestionObject("LABORAL1-B-0001", "LABORAL1-RES-A02", vLABORAL1_B_0001L);
            //
            int vLABORAL1_A_0002M = BackSelectedCheckBox(btnAgresivo, btnAlmafiesta, btnComodino, btnTemeroso);
            int vLABORAL1_B_0002L = BackSelectedCheckBox(btnAgresivo2, btnAlmafiesta2, btnComodino2, btnTemeroso2);
            BackQuestionObject("LABORAL1-A-0002", "LABORAL1-RES-B01", vLABORAL1_A_0002M);
            BackQuestionObject("LABORAL1-B-0002", "LABORAL1-RES-B02", vLABORAL1_B_0002L);
            //
            int vLABORAL1_A_0003M = BackSelectedCheckBox(btnAgradable, btnTemerosoDios, btnTenaz, btnAtractivo);
            int vLABORAL1_B_0003L = BackSelectedCheckBox(btnAgradable2, btnTemerosoDios2, btnTenaz2, btnAtractivo2);
            BackQuestionObject("LABORAL1-A-0003", "LABORAL1-RES-C01", vLABORAL1_A_0003M);
            BackQuestionObject("LABORAL1-B-0003", "LABORAL1-RES-C02", vLABORAL1_B_0003L);
            //
            int vLABORAL1_A_0004M = BackSelectedCheckBox(btnCauteloso, btnDeterminado, btnConvincente, btnBonachon);
            int vLABORAL1_B_0004L = BackSelectedCheckBox (btnCauteloso2, btnDeterminado2, btnConvincente2, btnBonachon2);
            BackQuestionObject("LABORAL1-A-0004", "LABORAL1-RES-D01", vLABORAL1_A_0004M);
            BackQuestionObject("LABORAL1-B-0004", "LABORAL1-RES-D02", vLABORAL1_B_0004L);
            //
            int vLABORAL1_A_0005M = BackSelectedCheckBox(btnDocil, btnAtrevido, btnleal, btnEncantador);
            int vLABORAL1_B_0005L = BackSelectedCheckBox(btnDocil2, btnAtrevido2, btnleal2, btnEncantador2);
            BackQuestionObject("LABORAL1-A-0005", "LABORAL1-RES-E01", vLABORAL1_A_0005M);
            BackQuestionObject("LABORAL1-B-0005", "LABORAL1-RES-E02", vLABORAL1_B_0005L);
            //
            int vLABORAL1_A_0006M = BackSelectedCheckBox(btnDispuesto, btnDeseoso, btnConsecuente, btnEntusiasta);
            int vLABORAL1_B_0006L = BackSelectedCheckBox(btnDispuesto2, btnDeseoso2, btnConsecuente2, btnEntusiasta2);
            BackQuestionObject("LABORAL1-A-0006", "LABORAL1-RES-F01", vLABORAL1_A_0006M);
            BackQuestionObject("LABORAL1-B-0006", "LABORAL1-RES-F02", vLABORAL1_B_0006L);
            //
            int vLABORAL1_A_0007M = BackSelectedCheckBox(btnFuerza, btnMenteAbierta, btnComplaciente, btnAnimoso);
            int vLABORAL1_B_0007L = BackSelectedCheckBox(btnFuerza2, btnMenteAbierta2, btnComplaciente2, btnAnimoso2);
            BackQuestionObject("LABORAL1-A-0007", "LABORAL1-RES-G01", vLABORAL1_A_0007M);
            BackQuestionObject("LABORAL1-B-0007", "LABORAL1-RES-G02", vLABORAL1_B_0007L);
            //
            int vLABORAL1_A_0008M = BackSelectedCheckBox(btnConfiado, btnSimpatizador, btnTolerante, btnAfirmativo);
            int vLABORAL1_B_0008L = BackSelectedCheckBox(btnConfiado2, btnSimpatizador2, btnTolerante2, btnAfirmativo2);
            BackQuestionObject("LABORAL1-A-0008", "LABORAL1-RES-H01", vLABORAL1_A_0008M);
            BackQuestionObject("LABORAL1-B-0008", "LABORAL1-RES-H02", vLABORAL1_B_0008L);
            //
            int vLABORAL1_A_0009M = BackSelectedCheckBox(btnEcuanime, btnPreciso, btnNervioso, btnJovial);
            int vLABORAL1_B_0009L = BackSelectedCheckBox(btnEcuanime2, btnPreciso2, btnNervioso2, btnJovial2);
            BackQuestionObject("LABORAL1-A-0009", "LABORAL1-RES-I01", vLABORAL1_A_0009M);
            BackQuestionObject("LABORAL1-B-0009", "LABORAL1-RES-I02", vLABORAL1_B_0009L);
            //
            int vLABORAL1_A_0010M = BackSelectedCheckBox(btnDisciplinado, btnGeneroso, btnAnimado, btnPersistente);
            int vLABORAL1_B_0010L = BackSelectedCheckBox(btnDisciplinado2, btnGeneroso2, btnAnimado2, btnPersistente2);
            BackQuestionObject("LABORAL1-A-0010", "LABORAL1-RES-J01", vLABORAL1_A_0010M);
            BackQuestionObject("LABORAL1-B-0010", "LABORAL1-RES-J02", vLABORAL1_B_0010L);
            //
            int vLABORAL1_A_0011M = BackSelectedCheckBox(btnCompetitivo, btnAlegre, btnConsiderado, btnArmonioso);
            int vLABORAL1_B_0011L = BackSelectedCheckBox(btnCompetitivo2, btnAlegre2, btnConsiderado2, btnArmonioso2);
            BackQuestionObject("LABORAL1-A-0011", "LABORAL1-RES-K01", vLABORAL1_A_0011M);
            BackQuestionObject("LABORAL1-B-0011", "LABORAL1-RES-K02", vLABORAL1_B_0011L);
            //
            int vLABORAL1_A_0012M = BackSelectedCheckBox(btnAdmirable, btnBondadoso, btnResignado, btnCaracter);
            int vLABORAL1_B_0012L = BackSelectedCheckBox(btnAdmirable2, btnBondadoso2, btnResignado2, btnCaracter2);
            BackQuestionObject("LABORAL1-A-0012", "LABORAL1-RES-L01", vLABORAL1_A_0012M);
            BackQuestionObject("LABORAL1-B-0012", "LABORAL1-RES-L02", vLABORAL1_B_0012L);
            //
            int vLABORAL1_A_0013M = BackSelectedCheckBox(btnObediente, btnQuisquilloso, btnInconquistable, btnJugueton);
            int vLABORAL1_B_0013L = BackSelectedCheckBox(btnObediente2, btnQuisquilloso2, btnInconquistable2, btnJugueton2);
            BackQuestionObject("LABORAL1-A-0013", "LABORAL1-RES-M01", vLABORAL1_A_0013M);
            BackQuestionObject("LABORAL1-B-0013", "LABORAL1-RES-M02", vLABORAL1_B_0013L);
            //
            int vLABORAL1_A_0014M = BackSelectedCheckBox(btnRespetuoso, btnEmprendedor, btnOptimista, btnServicial);
            int vLABORAL1_B_0014L = BackSelectedCheckBox(btnRespetuoso2, btnEmprendedor2, btnOptimista2, btnServicial2);
            BackQuestionObject("LABORAL1-A-0014", "LABORAL1-RES-N01", vLABORAL1_A_0014M);
            BackQuestionObject("LABORAL1-B-0014", "LABORAL1-RES-N02", vLABORAL1_B_0014L);
            //
            int vLABORAL1_A_0015M = BackSelectedCheckBox(btnValiente, btnInspirador, btnSumiso, btnTimido);
            int vLABORAL1_B_0015L = BackSelectedCheckBox(btnValiente2, btnInspirador2, btnSumiso2, btnTimido2);
            BackQuestionObject("LABORAL1-A-0015", "LABORAL1-RES-O01", vLABORAL1_A_0015M);
            BackQuestionObject("LABORAL1-B-0015", "LABORAL1-RES-O02", vLABORAL1_B_0015L);
            //
            int vLABORAL1_A_0016M = BackSelectedCheckBox(btnAdaptable, btnDisputador, btnIndiferente, btnSangreliviana);
            int vLABORAL1_B_0016L = BackSelectedCheckBox(btnAdaptable2, btnDisputador2, btnIndiferente2, btnSangreliviana2);
            BackQuestionObject("LABORAL1-A-0016", "LABORAL1-RES-P01", vLABORAL1_A_0016M);
            BackQuestionObject("LABORAL1-B-0016", "LABORAL1-RES-P02", vLABORAL1_B_0016L);
            //
            int vLABORAL1_A_0017M = BackSelectedCheckBox(btnAmiguero, btnPaciente, btnConfianza, btnMesurado);
            int vLABORAL1_B_0017L = BackSelectedCheckBox(btnAmiguero2, btnPaciente2, btnConfianza2, btnMesurado2);
            BackQuestionObject("LABORAL1-A-0017", "LABORAL1-RES-Q01", vLABORAL1_A_0017M);
            BackQuestionObject("LABORAL1-B-0017", "LABORAL1-RES-Q02", vLABORAL1_B_0017L);
            //
            int vLABORAL1_A_0018M = BackSelectedCheckBox(btnConforme, btnConfiable, btnPacific, btnPositivo);
            int vLABORAL1_B_0018L = BackSelectedCheckBox(btnConforme2, btnConfiable2, btnPacific2, btnPositivo2);
            BackQuestionObject("LABORAL1-A-0018", "LABORAL1-RES-R01", vLABORAL1_A_0018M);
            BackQuestionObject("LABORAL1-B-0018", "LABORAL1-RES-R02", vLABORAL1_B_0018L);
            //
            int vLABORAL1_A_0019M = BackSelectedCheckBox(btnAventurero, btnPerceptivo, btnCordial, btnModerado);
            int vLABORAL1_B_0019L = BackSelectedCheckBox(btnAventurero2, btnPerceptivo2, btnCordial2, btnModerado2);
            BackQuestionObject("LABORAL1-A-0019", "LABORAL1-RES-S01", vLABORAL1_A_0019M);
            BackQuestionObject("LABORAL1-B-0019", "LABORAL1-RES-S02", vLABORAL1_B_0019L);
            //
            int vLABORAL1_A_0020M = BackSelectedCheckBox(btnIndulgente, btnEsteta, btnVigoroso, btnSociable);
            int vLABORAL1_B_0020L = BackSelectedCheckBox(btnIndulgente2, btnEsteta2, btnVigoroso2, btnSociable2);
            BackQuestionObject("LABORAL1-A-0020", "LABORAL1-RES-T01", vLABORAL1_A_0020M);
            BackQuestionObject("LABORAL1-B-0020", "LABORAL1-RES-T02", vLABORAL1_B_0020L);
            //
            int vLABORAL1_A_0021M = BackSelectedCheckBox(btnParlanchin, btnControlado, btnConvencional, btnDecisivo);
            int vLABORAL1_B_0021L = BackSelectedCheckBox(btnParlanchin2, btnControlado2, btnConvencional2, btnDecisivo2);
            BackQuestionObject("LABORAL1-A-0021", "LABORAL1-RES-U01", vLABORAL1_A_0021M);
            BackQuestionObject("LABORAL1-B-0021", "LABORAL1-RES-U02", vLABORAL1_B_0021L);
            //
            int vLABORAL1_A_0022M = BackSelectedCheckBox(btnCohibido, btnExacto, btnFranco, btnBuencompaniero);
            int vLABORAL1_B_0022L = BackSelectedCheckBox(btnCohibido2, btnExacto2, btnFranco2, btnBuencompaniero2);
            BackQuestionObject("LABORAL1-A-0022", "LABORAL1-RES-V01", vLABORAL1_A_0022M);
            BackQuestionObject("LABORAL1-B-0022", "LABORAL1-RES-V02", vLABORAL1_B_0022L);
            //
            int vLABORAL1_A_0023M = BackSelectedCheckBox(btnDiplomatico, btnAudaz, btnRefinado, btnSatisfecho);
            int vLABORAL1_B_0023L = BackSelectedCheckBox(btnDiplomatico2, btnAudaz2, btnRefinado2, btnSatisfecho2);
            BackQuestionObject("LABORAL1-A-0023", "LABORAL1-RES-W01", vLABORAL1_A_0023M);
            BackQuestionObject("LABORAL1-B-0023", "LABORAL1-RES-W02", vLABORAL1_B_0023L);
            //
            int vLABORAL1_A_0024M = BackSelectedCheckBox(btnInquieto, btnPopular, btnBuenvecino, btnDevoto);
            int vLABORAL1_B_0024L = BackSelectedCheckBox(btnInquieto2, btnPopular2, btnBuenvecino2, btnDevoto2);
            BackQuestionObject("LABORAL1-A-0024", "LABORAL1-RES-X01", vLABORAL1_A_0024M);
            BackQuestionObject("LABORAL1-B-0024", "LABORAL1-RES-X02", vLABORAL1_B_0024L);
            //
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

            E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vObjetoPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: "PERSONALIDAD_LABORAL", usuario: vClUsuario, programa: vNbPrograma);
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

            int vLABORAL1_A_0001M = BackSelectedCheckBox(btnPersuasivo1, btnGentil1, btnHumilde1, btnOriginal1);
            int vLABORAL1_B_0001L = BackSelectedCheckBox(btnPersuasivo2, btnGentil2, btnHumilde2, btnOriginal2);
            BackQuestionObject("LABORAL1-A-0001", "LABORAL1-RES-A01", vLABORAL1_A_0001M);
            BackQuestionObject("LABORAL1-B-0001", "LABORAL1-RES-A02", vLABORAL1_B_0001L);
            //
            int vLABORAL1_A_0002M = BackSelectedCheckBox(btnAgresivo, btnAlmafiesta, btnComodino, btnTemeroso);
            int vLABORAL1_B_0002L = BackSelectedCheckBox(btnAgresivo2, btnAlmafiesta2, btnComodino2, btnTemeroso2);
            BackQuestionObject("LABORAL1-A-0002", "LABORAL1-RES-B01", vLABORAL1_A_0002M);
            BackQuestionObject("LABORAL1-B-0002", "LABORAL1-RES-B02", vLABORAL1_B_0002L);
            //
            int vLABORAL1_A_0003M = BackSelectedCheckBox(btnAgradable, btnTemerosoDios, btnTenaz, btnAtractivo);
            int vLABORAL1_B_0003L = BackSelectedCheckBox(btnAgradable2, btnTemerosoDios2, btnTenaz2, btnAtractivo2);
            BackQuestionObject("LABORAL1-A-0003", "LABORAL1-RES-C01", vLABORAL1_A_0003M);
            BackQuestionObject("LABORAL1-B-0003", "LABORAL1-RES-C02", vLABORAL1_B_0003L);
            //
            int vLABORAL1_A_0004M = BackSelectedCheckBox(btnCauteloso, btnDeterminado, btnConvincente, btnBonachon);
            int vLABORAL1_B_0004L = BackSelectedCheckBox(btnCauteloso2, btnDeterminado2, btnConvincente2, btnBonachon2);
            BackQuestionObject("LABORAL1-A-0004", "LABORAL1-RES-D01", vLABORAL1_A_0004M);
            BackQuestionObject("LABORAL1-B-0004", "LABORAL1-RES-D02", vLABORAL1_B_0004L);
            //
            int vLABORAL1_A_0005M = BackSelectedCheckBox(btnDocil, btnAtrevido, btnleal, btnEncantador);
            int vLABORAL1_B_0005L = BackSelectedCheckBox(btnDocil2, btnAtrevido2, btnleal2, btnEncantador2);
            BackQuestionObject("LABORAL1-A-0005", "LABORAL1-RES-E01", vLABORAL1_A_0005M);
            BackQuestionObject("LABORAL1-B-0005", "LABORAL1-RES-E02", vLABORAL1_B_0005L);
            //
            int vLABORAL1_A_0006M = BackSelectedCheckBox(btnDispuesto, btnDeseoso, btnConsecuente, btnEntusiasta);
            int vLABORAL1_B_0006L = BackSelectedCheckBox(btnDispuesto2, btnDeseoso2, btnConsecuente2, btnEntusiasta2);
            BackQuestionObject("LABORAL1-A-0006", "LABORAL1-RES-F01", vLABORAL1_A_0006M);
            BackQuestionObject("LABORAL1-B-0006", "LABORAL1-RES-F02", vLABORAL1_B_0006L);
            //
            int vLABORAL1_A_0007M = BackSelectedCheckBox(btnFuerza, btnMenteAbierta, btnComplaciente, btnAnimoso);
            int vLABORAL1_B_0007L = BackSelectedCheckBox(btnFuerza2, btnMenteAbierta2, btnComplaciente2, btnAnimoso2);
            BackQuestionObject("LABORAL1-A-0007", "LABORAL1-RES-G01", vLABORAL1_A_0007M);
            BackQuestionObject("LABORAL1-B-0007", "LABORAL1-RES-G02", vLABORAL1_B_0007L);
            //
            int vLABORAL1_A_0008M = BackSelectedCheckBox(btnConfiado, btnSimpatizador, btnTolerante, btnAfirmativo);
            int vLABORAL1_B_0008L = BackSelectedCheckBox(btnConfiado2, btnSimpatizador2, btnTolerante2, btnAfirmativo2);
            BackQuestionObject("LABORAL1-A-0008", "LABORAL1-RES-H01", vLABORAL1_A_0008M);
            BackQuestionObject("LABORAL1-B-0008", "LABORAL1-RES-H02", vLABORAL1_B_0008L);
            //
            int vLABORAL1_A_0009M = BackSelectedCheckBox(btnEcuanime, btnPreciso, btnNervioso, btnJovial);
            int vLABORAL1_B_0009L = BackSelectedCheckBox(btnEcuanime2, btnPreciso2, btnNervioso2, btnJovial2);
            BackQuestionObject("LABORAL1-A-0009", "LABORAL1-RES-I01", vLABORAL1_A_0009M);
            BackQuestionObject("LABORAL1-B-0009", "LABORAL1-RES-I02", vLABORAL1_B_0009L);
            //
            int vLABORAL1_A_0010M = BackSelectedCheckBox(btnDisciplinado, btnGeneroso, btnAnimado, btnPersistente);
            int vLABORAL1_B_0010L = BackSelectedCheckBox(btnDisciplinado2, btnGeneroso2, btnAnimado2, btnPersistente2);
            BackQuestionObject("LABORAL1-A-0010", "LABORAL1-RES-J01", vLABORAL1_A_0010M);
            BackQuestionObject("LABORAL1-B-0010", "LABORAL1-RES-J02", vLABORAL1_B_0010L);
            //
            int vLABORAL1_A_0011M = BackSelectedCheckBox(btnCompetitivo, btnAlegre, btnConsiderado, btnArmonioso);
            int vLABORAL1_B_0011L = BackSelectedCheckBox(btnCompetitivo2, btnAlegre2, btnConsiderado2, btnArmonioso2);
            BackQuestionObject("LABORAL1-A-0011", "LABORAL1-RES-K01", vLABORAL1_A_0011M);
            BackQuestionObject("LABORAL1-B-0011", "LABORAL1-RES-K02", vLABORAL1_B_0011L);
            //
            int vLABORAL1_A_0012M = BackSelectedCheckBox(btnAdmirable, btnBondadoso, btnResignado, btnCaracter);
            int vLABORAL1_B_0012L = BackSelectedCheckBox(btnAdmirable2, btnBondadoso2, btnResignado2, btnCaracter2);
            BackQuestionObject("LABORAL1-A-0012", "LABORAL1-RES-L01", vLABORAL1_A_0012M);
            BackQuestionObject("LABORAL1-B-0012", "LABORAL1-RES-L02", vLABORAL1_B_0012L);
            //
            int vLABORAL1_A_0013M = BackSelectedCheckBox(btnObediente, btnQuisquilloso, btnInconquistable, btnJugueton);
            int vLABORAL1_B_0013L = BackSelectedCheckBox(btnObediente2, btnQuisquilloso2, btnInconquistable2, btnJugueton2);
            BackQuestionObject("LABORAL1-A-0013", "LABORAL1-RES-M01", vLABORAL1_A_0013M);
            BackQuestionObject("LABORAL1-B-0013", "LABORAL1-RES-M02", vLABORAL1_B_0013L);
            //
            int vLABORAL1_A_0014M = BackSelectedCheckBox(btnRespetuoso, btnEmprendedor, btnOptimista, btnServicial);
            int vLABORAL1_B_0014L = BackSelectedCheckBox(btnRespetuoso2, btnEmprendedor2, btnOptimista2, btnServicial2);
            BackQuestionObject("LABORAL1-A-0014", "LABORAL1-RES-N01", vLABORAL1_A_0014M);
            BackQuestionObject("LABORAL1-B-0014", "LABORAL1-RES-N02", vLABORAL1_B_0014L);
            //
            int vLABORAL1_A_0015M = BackSelectedCheckBox(btnValiente, btnInspirador, btnSumiso, btnTimido);
            int vLABORAL1_B_0015L = BackSelectedCheckBox(btnValiente2, btnInspirador2, btnSumiso2, btnTimido2);
            BackQuestionObject("LABORAL1-A-0015", "LABORAL1-RES-O01", vLABORAL1_A_0015M);
            BackQuestionObject("LABORAL1-B-0015", "LABORAL1-RES-O02", vLABORAL1_B_0015L);
            //
            int vLABORAL1_A_0016M = BackSelectedCheckBox(btnAdaptable, btnDisputador, btnIndiferente, btnSangreliviana);
            int vLABORAL1_B_0016L = BackSelectedCheckBox(btnAdaptable2, btnDisputador2, btnIndiferente2, btnSangreliviana2);
            BackQuestionObject("LABORAL1-A-0016", "LABORAL1-RES-P01", vLABORAL1_A_0016M);
            BackQuestionObject("LABORAL1-B-0016", "LABORAL1-RES-P02", vLABORAL1_B_0016L);
            //
            int vLABORAL1_A_0017M = BackSelectedCheckBox(btnAmiguero, btnPaciente, btnConfianza, btnMesurado);
            int vLABORAL1_B_0017L = BackSelectedCheckBox(btnAmiguero2, btnPaciente2, btnConfianza2, btnMesurado2);
            BackQuestionObject("LABORAL1-A-0017", "LABORAL1-RES-Q01", vLABORAL1_A_0017M);
            BackQuestionObject("LABORAL1-B-0017", "LABORAL1-RES-Q02", vLABORAL1_B_0017L);
            //
            int vLABORAL1_A_0018M = BackSelectedCheckBox(btnConforme, btnConfiable, btnPacific, btnPositivo);
            int vLABORAL1_B_0018L = BackSelectedCheckBox(btnConforme2, btnConfiable2, btnPacific2, btnPositivo2);
            BackQuestionObject("LABORAL1-A-0018", "LABORAL1-RES-R01", vLABORAL1_A_0018M);
            BackQuestionObject("LABORAL1-B-0018", "LABORAL1-RES-R02", vLABORAL1_B_0018L);
            //
            int vLABORAL1_A_0019M = BackSelectedCheckBox(btnAventurero, btnPerceptivo, btnCordial, btnModerado);
            int vLABORAL1_B_0019L = BackSelectedCheckBox(btnAventurero2, btnPerceptivo2, btnCordial2, btnModerado2);
            BackQuestionObject("LABORAL1-A-0019", "LABORAL1-RES-S01", vLABORAL1_A_0019M);
            BackQuestionObject("LABORAL1-B-0019", "LABORAL1-RES-S02", vLABORAL1_B_0019L);
            //
            int vLABORAL1_A_0020M = BackSelectedCheckBox(btnIndulgente, btnEsteta, btnVigoroso, btnSociable);
            int vLABORAL1_B_0020L = BackSelectedCheckBox(btnIndulgente2, btnEsteta2, btnVigoroso2, btnSociable2);
            BackQuestionObject("LABORAL1-A-0020", "LABORAL1-RES-T01", vLABORAL1_A_0020M);
            BackQuestionObject("LABORAL1-B-0020", "LABORAL1-RES-T02", vLABORAL1_B_0020L);
            //
            int vLABORAL1_A_0021M = BackSelectedCheckBox(btnParlanchin, btnControlado, btnConvencional, btnDecisivo);
            int vLABORAL1_B_0021L = BackSelectedCheckBox(btnParlanchin2, btnControlado2, btnConvencional2, btnDecisivo2);
            BackQuestionObject("LABORAL1-A-0021", "LABORAL1-RES-U01", vLABORAL1_A_0021M);
            BackQuestionObject("LABORAL1-B-0021", "LABORAL1-RES-U02", vLABORAL1_B_0021L);
            //
            int vLABORAL1_A_0022M = BackSelectedCheckBox(btnCohibido, btnExacto, btnFranco, btnBuencompaniero);
            int vLABORAL1_B_0022L = BackSelectedCheckBox(btnCohibido2, btnExacto2, btnFranco2, btnBuencompaniero2);
            BackQuestionObject("LABORAL1-A-0022", "LABORAL1-RES-V01", vLABORAL1_A_0022M);
            BackQuestionObject("LABORAL1-B-0022", "LABORAL1-RES-V02", vLABORAL1_B_0022L);
            //
            int vLABORAL1_A_0023M = BackSelectedCheckBox(btnDiplomatico, btnAudaz, btnRefinado, btnSatisfecho);
            int vLABORAL1_B_0023L = BackSelectedCheckBox(btnDiplomatico2, btnAudaz2, btnRefinado2, btnSatisfecho2);
            BackQuestionObject("LABORAL1-A-0023", "LABORAL1-RES-W01", vLABORAL1_A_0023M);
            BackQuestionObject("LABORAL1-B-0023", "LABORAL1-RES-W02", vLABORAL1_B_0023L);
            //
            int vLABORAL1_A_0024M = BackSelectedCheckBox(btnInquieto, btnPopular, btnBuenvecino, btnDevoto);
            int vLABORAL1_B_0024L = BackSelectedCheckBox(btnInquieto2, btnPopular2, btnBuenvecino2, btnDevoto2);
            BackQuestionObject("LABORAL1-A-0024", "LABORAL1-RES-X01", vLABORAL1_A_0024M);
            BackQuestionObject("LABORAL1-B-0024", "LABORAL1-RES-X02", vLABORAL1_B_0024L);
            //
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

            E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vObjetoPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: "PERSONALIDAD_LABORAL", usuario: vClUsuario, programa: vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
        }

        public int BackSelectedCheckBox(RadButton a, RadButton b, RadButton c, RadButton d)
        {
            int resultado = 0;
            if (a.Checked)
            { resultado = 1; }
            else if (b.Checked)
            { resultado = 2; }
            else if (c.Checked)
            { resultado = 3; }
            else if (d.Checked)
            { resultado = 4; }

            return resultado;
        }

        public void BackQuestionObject(string pclPreguntaTipo1, String pclPreguntaTipo2, int pnbRespuesta)
        {
            var vPregunta = vRespuestas.Where(x => x.CL_PREGUNTA.Equals(pclPreguntaTipo1)).FirstOrDefault();
            if (vPregunta != null)
            {
                vPregunta.CL_PREGUNTA = pclPreguntaTipo2;
                vPregunta.NB_RESPUESTA = pnbRespuesta.ToString();
                vPregunta.NO_VALOR_RESPUESTA = pnbRespuesta;
            }
        }

        public void asignarValores(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> respuestas)
        {
            foreach (SPE_OBTIENE_RESULTADO_PRUEBA_Result resp in respuestas)
            {
                switch (resp.CL_PREGUNTA)
                {
                    case "LABORAL1-A-0001": SeleccionarBotonRespuesta(btnPersuasivo1, btnGentil1, btnHumilde1, btnOriginal1, resp.NB_RESPUESTA); break;
                    case "LABORAL1-B-0001": SeleccionarBotonRespuesta(btnPersuasivo2, btnGentil2, btnHumilde2, btnOriginal2, resp.NB_RESPUESTA); break;

                    case "LABORAL1-A-0002": SeleccionarBotonRespuesta(btnAgresivo, btnAlmafiesta, btnComodino, btnTemeroso, resp.NB_RESPUESTA); break;
                    case "LABORAL1-B-0002": SeleccionarBotonRespuesta(btnAgresivo2, btnAlmafiesta2, btnComodino2, btnTemeroso2, resp.NB_RESPUESTA); break;

                    case "LABORAL1-A-0003": SeleccionarBotonRespuesta(btnAgradable, btnTemerosoDios, btnTenaz, btnAtractivo, resp.NB_RESPUESTA); break;
                    case "LABORAL1-B-0003": SeleccionarBotonRespuesta(btnAgradable2, btnTemerosoDios2, btnTenaz2, btnAtractivo2, resp.NB_RESPUESTA); break;

                    case "LABORAL1-A-0004": SeleccionarBotonRespuesta(btnCauteloso, btnDeterminado, btnConvincente, btnBonachon, resp.NB_RESPUESTA); break;
                    case "LABORAL1-B-0004": SeleccionarBotonRespuesta(btnCauteloso2, btnDeterminado2, btnConvincente2, btnBonachon2, resp.NB_RESPUESTA); break;

                    case "LABORAL1-A-0005": SeleccionarBotonRespuesta(btnDocil, btnAtrevido, btnleal, btnEncantador, resp.NB_RESPUESTA); break;
                    case "LABORAL1-B-0005": SeleccionarBotonRespuesta(btnDocil2, btnAtrevido2, btnleal2, btnEncantador2, resp.NB_RESPUESTA); break;

                    case "LABORAL1-A-0006": SeleccionarBotonRespuesta(btnDispuesto, btnDeseoso, btnConsecuente, btnEntusiasta, resp.NB_RESPUESTA); break;
                    case "LABORAL1-B-0006": SeleccionarBotonRespuesta(btnDispuesto2, btnDeseoso2, btnConsecuente2, btnEntusiasta2, resp.NB_RESPUESTA); break;

                    case "LABORAL1-A-0007": SeleccionarBotonRespuesta(btnFuerza, btnMenteAbierta, btnComplaciente, btnAnimoso, resp.NB_RESPUESTA); break;
                    case "LABORAL1-B-0007": SeleccionarBotonRespuesta(btnFuerza2, btnMenteAbierta2, btnComplaciente2, btnAnimoso2, resp.NB_RESPUESTA); break;

                    case "LABORAL1-A-0008": SeleccionarBotonRespuesta(btnConfiado, btnSimpatizador, btnTolerante, btnAfirmativo, resp.NB_RESPUESTA); break;
                    case "LABORAL1-B-0008": SeleccionarBotonRespuesta(btnConfiado2, btnSimpatizador2, btnTolerante2, btnAfirmativo2, resp.NB_RESPUESTA); break;

                    case "LABORAL1-A-0009": SeleccionarBotonRespuesta(btnEcuanime, btnPreciso, btnNervioso, btnJovial, resp.NB_RESPUESTA); break;
                    case "LABORAL1-B-0009": SeleccionarBotonRespuesta(btnEcuanime2, btnPreciso2, btnNervioso2, btnJovial2, resp.NB_RESPUESTA); break;

                    case "LABORAL1-A-0010": SeleccionarBotonRespuesta(btnDisciplinado, btnGeneroso, btnAnimado, btnPersistente, resp.NB_RESPUESTA); break;
                    case "LABORAL1-B-0010": SeleccionarBotonRespuesta(btnDisciplinado2, btnGeneroso2, btnAnimado2, btnPersistente2, resp.NB_RESPUESTA); break;

                    //
                    case "LABORAL1-A-0011": SeleccionarBotonRespuesta(btnCompetitivo, btnAlegre, btnConsiderado, btnArmonioso, resp.NB_RESPUESTA); break;
                    case "LABORAL1-B-0011": SeleccionarBotonRespuesta(btnCompetitivo2, btnAlegre2, btnConsiderado2, btnArmonioso2, resp.NB_RESPUESTA); break;

                    case "LABORAL1-A-0012": SeleccionarBotonRespuesta(btnAdmirable, btnBondadoso, btnResignado, btnCaracter, resp.NB_RESPUESTA); break;
                    case "LABORAL1-B-0012": SeleccionarBotonRespuesta(btnAdmirable2, btnBondadoso2, btnResignado2, btnCaracter2, resp.NB_RESPUESTA); break;

                    case "LABORAL1-A-0013": SeleccionarBotonRespuesta(btnObediente, btnQuisquilloso, btnInconquistable, btnJugueton, resp.NB_RESPUESTA); break;
                    case "LABORAL1-B-0013": SeleccionarBotonRespuesta(btnObediente2, btnQuisquilloso2, btnInconquistable2, btnJugueton2, resp.NB_RESPUESTA); break;

                    case "LABORAL1-A-0014": SeleccionarBotonRespuesta(btnRespetuoso, btnEmprendedor, btnOptimista, btnServicial, resp.NB_RESPUESTA); break;
                    case "LABORAL1-B-0014": SeleccionarBotonRespuesta(btnRespetuoso2, btnEmprendedor2, btnOptimista2, btnServicial2, resp.NB_RESPUESTA); break;

                    case "LABORAL1-A-0015": SeleccionarBotonRespuesta(btnValiente, btnInspirador, btnSumiso, btnTimido, resp.NB_RESPUESTA); break;
                    case "LABORAL1-B-0015": SeleccionarBotonRespuesta(btnValiente2, btnInspirador2, btnSumiso2, btnTimido2, resp.NB_RESPUESTA); break;

                    case "LABORAL1-A-0016": SeleccionarBotonRespuesta(btnAdaptable, btnDisputador, btnIndiferente, btnSangreliviana, resp.NB_RESPUESTA); break;
                    case "LABORAL1-B-0016": SeleccionarBotonRespuesta(btnAdaptable2, btnDisputador2, btnIndiferente2, btnSangreliviana2, resp.NB_RESPUESTA); break;

                    case "LABORAL1-A-0017": SeleccionarBotonRespuesta(btnAmiguero, btnPaciente, btnConfianza, btnMesurado, resp.NB_RESPUESTA); break;
                    case "LABORAL1-B-0017": SeleccionarBotonRespuesta(btnAmiguero2, btnPaciente2, btnConfianza2, btnMesurado2, resp.NB_RESPUESTA); break;

                    case "LABORAL1-A-0018": SeleccionarBotonRespuesta(btnConforme, btnConfiable, btnPacific, btnPositivo, resp.NB_RESPUESTA); break;
                    case "LABORAL1-B-0018": SeleccionarBotonRespuesta(btnConforme2, btnConfiable2, btnPacific2, btnPositivo2, resp.NB_RESPUESTA); break;

                    case "LABORAL1-A-0019": SeleccionarBotonRespuesta(btnAventurero, btnPerceptivo, btnCordial, btnModerado, resp.NB_RESPUESTA); break;
                    case "LABORAL1-B-0019": SeleccionarBotonRespuesta(btnAventurero2, btnPerceptivo2, btnCordial2, btnModerado2, resp.NB_RESPUESTA); break;

                    case "LABORAL1-A-0020": SeleccionarBotonRespuesta(btnIndulgente, btnEsteta, btnVigoroso, btnSociable, resp.NB_RESPUESTA); break;
                    case "LABORAL1-B-0020": SeleccionarBotonRespuesta(btnIndulgente2, btnEsteta2, btnVigoroso2, btnSociable2, resp.NB_RESPUESTA); break;

                    case "LABORAL1-A-0021": SeleccionarBotonRespuesta(btnParlanchin, btnControlado, btnConvencional, btnDecisivo, resp.NB_RESPUESTA); break;
                    case "LABORAL1-B-0021": SeleccionarBotonRespuesta(btnParlanchin2, btnControlado2, btnConvencional2, btnDecisivo2, resp.NB_RESPUESTA); break;

                    case "LABORAL1-A-0022": SeleccionarBotonRespuesta(btnCohibido, btnExacto, btnFranco, btnBuencompaniero, resp.NB_RESPUESTA); break;
                    case "LABORAL1-B-0022": SeleccionarBotonRespuesta(btnCohibido2, btnExacto2, btnFranco2, btnBuencompaniero2, resp.NB_RESPUESTA); break;

                    case "LABORAL1-A-0023": SeleccionarBotonRespuesta(btnDiplomatico, btnAudaz, btnRefinado, btnSatisfecho, resp.NB_RESPUESTA); break;
                    case "LABORAL1-B-0023": SeleccionarBotonRespuesta(btnDiplomatico2, btnAudaz2, btnRefinado2, btnSatisfecho2, resp.NB_RESPUESTA); break;

                    case "LABORAL1-A-0024": SeleccionarBotonRespuesta(btnInquieto, btnPopular, btnBuenvecino, btnDevoto, resp.NB_RESPUESTA); break;
                    case "LABORAL1-B-0024": SeleccionarBotonRespuesta(btnInquieto2, btnPopular2, btnBuenvecino2, btnDevoto2, resp.NB_RESPUESTA); break;

                    case "LABORAL1-RES-A01": SeleccionarBotonRespuesta(btnPersuasivo1, btnGentil1, btnHumilde1, btnOriginal1, resp.NB_RESULTADO); break;
                    case "LABORAL1-RES-A02": SeleccionarBotonRespuesta(btnPersuasivo2, btnGentil2, btnHumilde2, btnOriginal2, resp.NB_RESULTADO); break;

                    case "LABORAL1-RES-B01": SeleccionarBotonRespuesta(btnAgresivo, btnAlmafiesta, btnComodino, btnTemeroso, resp.NB_RESULTADO); break;
                    case "LABORAL1-RES-B02": SeleccionarBotonRespuesta(btnAgresivo2, btnAlmafiesta2, btnComodino2, btnTemeroso2, resp.NB_RESULTADO); break;

                    case "LABORAL1-RES-C01": SeleccionarBotonRespuesta(btnAgradable, btnTemerosoDios, btnTenaz, btnAtractivo, resp.NB_RESULTADO); break;
                    case "LABORAL1-RES-C02": SeleccionarBotonRespuesta(btnAgradable2, btnTemerosoDios2, btnTenaz2, btnAtractivo2, resp.NB_RESULTADO); break;

                    case "LABORAL1-RES-D01": SeleccionarBotonRespuesta(btnCauteloso, btnDeterminado, btnConvincente, btnBonachon, resp.NB_RESULTADO); break;
                    case "LABORAL1-RES-D02": SeleccionarBotonRespuesta(btnCauteloso2, btnDeterminado2, btnConvincente2, btnBonachon2, resp.NB_RESULTADO); break;

                    case "LABORAL1-RES-E01": SeleccionarBotonRespuesta(btnDocil, btnAtrevido, btnleal, btnEncantador, resp.NB_RESULTADO); break;
                    case "LABORAL1-RES-E02": SeleccionarBotonRespuesta(btnDocil2, btnAtrevido2, btnleal2, btnEncantador2, resp.NB_RESULTADO); break;

                    case "LABORAL1-RES-F01": SeleccionarBotonRespuesta(btnDispuesto, btnDeseoso, btnConsecuente, btnEntusiasta, resp.NB_RESULTADO); break;
                    case "LABORAL1-RES-F02": SeleccionarBotonRespuesta(btnDispuesto2, btnDeseoso2, btnConsecuente2, btnEntusiasta2, resp.NB_RESULTADO); break;

                    case "LABORAL1-RES-G01": SeleccionarBotonRespuesta(btnFuerza, btnMenteAbierta, btnComplaciente, btnAnimoso, resp.NB_RESULTADO); break;
                    case "LABORAL1-RES-G02": SeleccionarBotonRespuesta(btnFuerza2, btnMenteAbierta2, btnComplaciente2, btnAnimoso2, resp.NB_RESULTADO); break;

                    case "LABORAL1-RES-H01": SeleccionarBotonRespuesta(btnConfiado, btnSimpatizador, btnTolerante, btnAfirmativo, resp.NB_RESULTADO); break;
                    case "LABORAL1-RES-H02": SeleccionarBotonRespuesta(btnConfiado2, btnSimpatizador2, btnTolerante2, btnAfirmativo2, resp.NB_RESULTADO); break;

                    case "LABORAL1-RES-I01": SeleccionarBotonRespuesta(btnEcuanime, btnPreciso, btnNervioso, btnJovial, resp.NB_RESULTADO); break;
                    case "LABORAL1-RES-I02": SeleccionarBotonRespuesta(btnEcuanime2, btnPreciso2, btnNervioso2, btnJovial2, resp.NB_RESULTADO); break;

                    case "LABORAL1-RES-J01": SeleccionarBotonRespuesta(btnDisciplinado, btnGeneroso, btnAnimado, btnPersistente, resp.NB_RESULTADO); break;
                    case "LABORAL1-RES-J02": SeleccionarBotonRespuesta(btnDisciplinado2, btnGeneroso2, btnAnimado2, btnPersistente2, resp.NB_RESULTADO); break;

                    //
                    case "LABORAL1-RES-K01": SeleccionarBotonRespuesta(btnCompetitivo, btnAlegre, btnConsiderado, btnArmonioso, resp.NB_RESULTADO); break;
                    case "LABORAL1-RES-K02": SeleccionarBotonRespuesta(btnCompetitivo2, btnAlegre2, btnConsiderado2, btnArmonioso2, resp.NB_RESULTADO); break;

                    case "LABORAL1-RES-L01": SeleccionarBotonRespuesta(btnAdmirable, btnBondadoso, btnResignado, btnCaracter, resp.NB_RESULTADO); break;
                    case "LABORAL1-RES-L02": SeleccionarBotonRespuesta(btnAdmirable2, btnBondadoso2, btnResignado2, btnCaracter2, resp.NB_RESULTADO); break;

                    case "LABORAL1-RES-M01": SeleccionarBotonRespuesta(btnObediente, btnQuisquilloso, btnInconquistable, btnJugueton, resp.NB_RESULTADO); break;
                    case "LABORAL1-RES-M02": SeleccionarBotonRespuesta(btnObediente2, btnQuisquilloso2, btnInconquistable2, btnJugueton2, resp.NB_RESULTADO); break;

                    case "LABORAL1-RES-N01": SeleccionarBotonRespuesta(btnRespetuoso, btnEmprendedor, btnOptimista, btnServicial, resp.NB_RESULTADO); break;
                    case "LABORAL1-RES-N02": SeleccionarBotonRespuesta(btnRespetuoso2, btnEmprendedor2, btnOptimista2, btnServicial2, resp.NB_RESULTADO); break;

                    case "LABORAL1-RES-O01": SeleccionarBotonRespuesta(btnValiente, btnInspirador, btnSumiso, btnTimido, resp.NB_RESULTADO); break;
                    case "LABORAL1-RES-O02": SeleccionarBotonRespuesta(btnValiente2, btnInspirador2, btnSumiso2, btnTimido2, resp.NB_RESULTADO); break;

                    case "LABORAL1-RES-P01": SeleccionarBotonRespuesta(btnAdaptable, btnDisputador, btnIndiferente, btnSangreliviana, resp.NB_RESULTADO); break;
                    case "LABORAL1-RES-P02": SeleccionarBotonRespuesta(btnAdaptable2, btnDisputador2, btnIndiferente2, btnSangreliviana2, resp.NB_RESULTADO); break;

                    case "LABORAL1-RES-Q01": SeleccionarBotonRespuesta(btnAmiguero, btnPaciente, btnConfianza, btnMesurado, resp.NB_RESULTADO); break;
                    case "LABORAL1-RES-Q02": SeleccionarBotonRespuesta(btnAmiguero2, btnPaciente2, btnConfianza2, btnMesurado2, resp.NB_RESULTADO); break;

                    case "LABORAL1-RES-R01": SeleccionarBotonRespuesta(btnConforme, btnConfiable, btnPacific, btnPositivo, resp.NB_RESULTADO); break;
                    case "LABORAL1-RES-R02": SeleccionarBotonRespuesta(btnConforme2, btnConfiable2, btnPacific2, btnPositivo2, resp.NB_RESULTADO); break;

                    case "LABORAL1-RES-S01": SeleccionarBotonRespuesta(btnAventurero, btnPerceptivo, btnCordial, btnModerado, resp.NB_RESULTADO); break;
                    case "LABORAL1-RES-S02": SeleccionarBotonRespuesta(btnAventurero2, btnPerceptivo2, btnCordial2, btnModerado2, resp.NB_RESULTADO); break;

                    case "LABORAL1-RES-T01": SeleccionarBotonRespuesta(btnIndulgente, btnEsteta, btnVigoroso, btnSociable, resp.NB_RESULTADO); break;
                    case "LABORAL1-RES-T02": SeleccionarBotonRespuesta(btnIndulgente2, btnEsteta2, btnVigoroso2, btnSociable2, resp.NB_RESULTADO); break;

                    case "LABORAL1-RES-U01": SeleccionarBotonRespuesta(btnParlanchin, btnControlado, btnConvencional, btnDecisivo, resp.NB_RESULTADO); break;
                    case "LABORAL1-RES-U02": SeleccionarBotonRespuesta(btnParlanchin2, btnControlado2, btnConvencional2, btnDecisivo2, resp.NB_RESULTADO); break;

                    case "LABORAL1-RES-V01": SeleccionarBotonRespuesta(btnCohibido, btnExacto, btnFranco, btnBuencompaniero, resp.NB_RESULTADO); break;
                    case "LABORAL1-RES-V02": SeleccionarBotonRespuesta(btnCohibido2, btnExacto2, btnFranco2, btnBuencompaniero2, resp.NB_RESULTADO); break;

                    case "LABORAL1-RES-W01": SeleccionarBotonRespuesta(btnDiplomatico, btnAudaz, btnRefinado, btnSatisfecho, resp.NB_RESULTADO); break;
                    case "LABORAL1-RES-W02": SeleccionarBotonRespuesta(btnDiplomatico2, btnAudaz2, btnRefinado2, btnSatisfecho2, resp.NB_RESULTADO); break;

                    case "LABORAL1-RES-X01": SeleccionarBotonRespuesta(btnInquieto, btnPopular, btnBuenvecino, btnDevoto, resp.NB_RESULTADO); break;
                    case "LABORAL1-RES-X02": SeleccionarBotonRespuesta(btnInquieto2, btnPopular2, btnBuenvecino2, btnDevoto2, resp.NB_RESULTADO); break;
                }
            }
        }

        public void SeleccionarBotonRespuesta(RadButton a, RadButton b, RadButton c, RadButton d, string pAnswer)
        {
            switch (pAnswer)
            {
                case "1": a.Checked = true; break;
                case "2": b.Checked = true; break;
                case "3": c.Checked = true; break;
                case "4": d.Checked = true; break;
                default: break;
            }
        }

        protected void btnCorregir_Click(object sender, EventArgs e)
        {
            PruebasNegocio nKprueba = new PruebasNegocio();
            SPE_OBTIENE_K_PRUEBA_Result vPruebaTerminada = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();
            E_RESULTADO vResultado = nKprueba.CorrigePrueba(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            EditTest();
        }
    }
}
