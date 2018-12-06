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
    public partial class VentanaAptitudMental2 : System.Web.UI.Page
    {

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

        public int? vTiempoPrueba
        {
            get { return (int?)ViewState["vsPLaboral2seconds"]; }
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
            get { return (bool)ViewState["vsMostrarCronometroAM2"]; }
            set { ViewState["vsMostrarCronometroAM2"] = value; }
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
                    if (Request.QueryString["vIdBateria"] != null)
                    {
					    vIdBateria = int.Parse(Request.QueryString["vIdBateria"]);
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
                        btnImpresionPrueba.Visible = true;
                        //obtener respuestas
                        var respuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);
                        var vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken).FirstOrDefault();
                        if (vPrueba.NB_TIPO_PRUEBA == "MANUAL")
                        {
                            asignarValoresManual(respuestas);
                        }
                        else
                        {
                            asignarValores(respuestas);
                        }
                    }
                    else if (vTipoRevision == "EDIT")
                    {
                        cronometro.Visible = false;
                        vTiempoPrueba = 0;
                        btnTerminar.Visible = false;
                        btnCorregir.Visible = true;
                        //btnEliminar.Visible = true;// Se agrega para la nueva forma de navegación 06/06/2018
                        btnImpresionPrueba.Visible = true; //Se agrega para permitir imprimir en la nueva navegación 06/06/2018
                        //obtener respuestas
                        var respuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);
                        var vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken).FirstOrDefault();
                        if (vPrueba.NB_TIPO_PRUEBA == "MANUAL")
                        {
                            asignarValoresManual(respuestas);
                            btnCorregir.Enabled = false;
                        }
                        else
                        {
                            asignarValores(respuestas);
                        }
                    }
                    else{
                    E_RESULTADO vObjetoPrueba = nKprueba.INICIAR_K_PRUEBA(pIdPrueba: vIdPrueba, pFeInicio: DateTime.Now, pClTokenExterno: vClToken, usuario: vClUsuario, programa: vNbPrograma);

                    if (vObjetoPrueba != null)
                    {
                        //Si el modo de revision esta activado
                        //if (vTipoRevision == "REV")
                        //{
                        //    cronometro.Visible = false;
                        //    vTiempoPrueba = 0;
                        //    btnTerminar.Enabled = false;
                        //    btnImpresionPrueba.Visible = true;
                        //    //obtener respuestas
                        //    var respuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);
                        //    var vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken).FirstOrDefault();
                        //    if (vPrueba.NB_TIPO_PRUEBA == "MANUAL")
                        //    {
                        //        asignarValoresManual(respuestas);
                        //    }
                        //    else
                        //    {
                        //    asignarValores(respuestas);
                        //    }
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
                        //    {
                        //        asignarValoresManual(respuestas);
                        //    }
                        //    else
                        //    {
                        //        asignarValores(respuestas);
                        //    }
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
            else
            {
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

                int APTITUD2_A_0001 = BackSelectedCheckBox(btnPregunta1Resp1, btnPregunta1Resp2, btnPregunta1Resp3, btnPregunta1Resp4, btnPregunta1Resp5);
                BackQuestionObject("APTITUD2-A-0001", APTITUD2_A_0001);

                int APTITUD2_A_0002 = BackSelectedCheckBox(btnPregunta2Resp1, btnPregunta2Resp2, btnPregunta2Resp3, btnPregunta2Resp4, btnPregunta2Resp5);
                BackQuestionObject("APTITUD2-A-0002", APTITUD2_A_0002);

                int APTITUD2_A_0003 = BackSelectedCheckBox(btnPregunta3Resp1, btnPregunta3Resp2, btnPregunta3Resp3, btnPregunta3Resp4, btnPregunta3Resp5);
                BackQuestionObject("APTITUD2-A-0003", APTITUD2_A_0003);

                int APTITUD2_A_0004 = BackSelectedCheckBox(btnPregunta4Resp1, btnPregunta4Resp2, btnPregunta4Resp3, btnPregunta4Resp4, btnPregunta4Resp5);
                BackQuestionObject("APTITUD2-A-0004", APTITUD2_A_0004);

                int APTITUD2_A_0005 = BackSelectedCheckBox(btnPregunta5Resp1, btnPregunta5Resp2, btnPregunta5Resp3, btnPregunta5Resp4, btnPregunta5Resp5);
                BackQuestionObject("APTITUD2-A-0005", APTITUD2_A_0005);

                int APTITUD2_A_0006 = BackSelectedCheckBox(btnPregunta6Resp1, btnPregunta6Resp2, btnPregunta6Resp3, btnPregunta6Resp4, btnPregunta6Resp5);
                BackQuestionObject("APTITUD2-A-0006", APTITUD2_A_0006);

                int APTITUD2_A_0007 = BackSelectedCheckBox(btnPregunta7Resp1, btnPregunta7Resp2, btnPregunta7Resp3, btnPregunta7Resp4, btnPregunta7Resp5);
                BackQuestionObject("APTITUD2-A-0007", APTITUD2_A_0007);

                int APTITUD2_A_0008 = BackSelectedCheckBox(btnPregunta8Resp1, btnPregunta8Resp2, btnPregunta8Resp3, btnPregunta8Resp4, btnPregunta8Resp5);
                BackQuestionObject("APTITUD2-A-0008", APTITUD2_A_0008);

                int APTITUD2_A_0009 = BackSelectedCheckBox(btnPregunta9Resp1, btnPregunta9Resp2, btnPregunta9Resp3, btnPregunta9Resp4, btnPregunta9Resp5);
                BackQuestionObject("APTITUD2-A-0009", APTITUD2_A_0009);

                int APTITUD2_A_0010 = BackSelectedCheckBox(btnPregunta10Resp1, btnPregunta10Resp2, btnPregunta10Resp3, btnPregunta10Resp4, btnPregunta10Resp5);
                BackQuestionObject("APTITUD2-A-0010", APTITUD2_A_0010);

                int APTITUD2_A_0011 = BackSelectedCheckBox(btnPregunta11Resp1, btnPregunta12Resp2, btnPregunta11Resp3, btnPregunta11Resp4, btnPregunta11Resp5);
                BackQuestionObject("APTITUD2-A-0011", APTITUD2_A_0011);

                int APTITUD2_A_0012 = BackSelectedCheckBox(btnPregunta12Resp1, btnPregunta12Resp2, btnPregunta12Resp3, btnPregunta12Resp4, btnPregunta12Resp5);
                BackQuestionObject("APTITUD2-A-0012", APTITUD2_A_0012);

                int APTITUD2_A_0013 = BackSelectedCheckBox(btnPregunta13Resp1, btnPregunta13Resp2, btnPregunta13Resp3, btnPregunta13Resp4, btnPregunta13Resp5);
                BackQuestionObject("APTITUD2-A-0013", APTITUD2_A_0013);

                int APTITUD2_A_0014 = BackSelectedCheckBox(btnPregunta14Resp1, btnPregunta14Resp2, btnPregunta14Resp3, btnPregunta14Resp4, btnPregunta14Resp5);
                BackQuestionObject("APTITUD2-A-0014", APTITUD2_A_0014);

                int APTITUD2_A_0015 = BackSelectedCheckBox(btnPregunta15Resp1, btnPregunta15Resp2, btnPregunta15Resp3, btnPregunta15Resp4, btnPregunta15Resp5);
                BackQuestionObject("APTITUD2-A-0015", APTITUD2_A_0015);

                int APTITUD2_A_0016 = BackSelectedCheckBox(btnPregunta16Resp1, btnPregunta16Resp2, btnPregunta16Resp3, btnPregunta16Resp4, btnPregunta16Resp5);
                BackQuestionObject("APTITUD2-A-0016", APTITUD2_A_0016);

                int APTITUD2_A_0017 = BackSelectedCheckBox(btnPregunta17Resp1, btnPregunta17Resp2, btnPregunta17Resp3, btnPregunta17Resp4, btnPregunta17Resp5);
                BackQuestionObject("APTITUD2-A-0017", APTITUD2_A_0017);

                int APTITUD2_A_0018 = BackSelectedCheckBox(btnPregunta18Resp1, btnPregunta18Resp2, btnPregunta18Resp3, btnPregunta18Resp4, btnPregunta18Resp5);
                BackQuestionObject("APTITUD2-A-0018", APTITUD2_A_0018);

                int APTITUD2_A_0019 = BackSelectedCheckBox(btnPregunta19Resp1, btnPregunta19Resp2, btnPregunta19Resp3, btnPregunta19Resp4, btnPregunta19Resp5);
                BackQuestionObject("APTITUD2-A-0019", APTITUD2_A_0019);

                int APTITUD2_A_0020 = BackSelectedCheckBox(btnPregunta20Resp1, btnPregunta20Resp2, btnPregunta20Resp3, btnPregunta20Resp4, btnPregunta20Resp5);
                BackQuestionObject("APTITUD2-A-0020", APTITUD2_A_0020);

                int APTITUD2_A_0021 = BackSelectedCheckBox(btnPregunta21Resp1, btnPregunta21Resp2, btnPregunta21Resp3, btnPregunta21Resp4, btnPregunta21Resp5);
                BackQuestionObject("APTITUD2-A-0021", APTITUD2_A_0021);

                int APTITUD2_A_0022 = BackSelectedCheckBox(btnPregunta22Resp1, btnPregunta22Resp2, btnPregunta22Resp3, btnPregunta22Resp4, btnPregunta22Resp5);
                BackQuestionObject("APTITUD2-A-0022", APTITUD2_A_0022);

                int APTITUD2_A_0023 = BackSelectedCheckBox(btnPregunta23Resp1, btnPregunta23Resp2, btnPregunta23Resp3, btnPregunta23Resp4, btnPregunta23Resp5);
                BackQuestionObject("APTITUD2-A-0023", APTITUD2_A_0023);

                int APTITUD2_A_0024 = BackSelectedCheckBox(btnPregunta24Resp1, btnPregunta24Resp2, btnPregunta24Resp3, btnPregunta24Resp4, btnPregunta24Resp5);
                BackQuestionObject("APTITUD2-A-0024", APTITUD2_A_0024);

                int APTITUD2_A_0025 = BackSelectedCheckBox(btnPregunta25Resp1, btnPregunta25Resp2, btnPregunta25Resp3, btnPregunta25Resp4, btnPregunta25Resp5);
                BackQuestionObject("APTITUD2-A-0025", APTITUD2_A_0025);

                int APTITUD2_A_0026 = BackSelectedCheckBox(btnPregunta26Resp1, btnPregunta26Resp2, btnPregunta26Resp3, btnPregunta26Resp4, btnPregunta26Resp5);
                BackQuestionObject("APTITUD2-A-0026", APTITUD2_A_0026);

                int APTITUD2_A_0027 = BackSelectedCheckBox(btnPregunta27Resp1, btnPregunta27Resp2, btnPregunta27Resp3, btnPregunta27Resp4, btnPregunta27Resp5);
                BackQuestionObject("APTITUD2-A-0027", APTITUD2_A_0027);

                int APTITUD2_A_0028 = BackSelectedCheckBox(btnPregunta28Resp1, btnPregunta28Resp2, btnPregunta28Resp3, btnPregunta28Resp4, btnPregunta28Resp5);
                BackQuestionObject("APTITUD2-A-0028", APTITUD2_A_0028);

                int APTITUD2_A_0029 = BackSelectedCheckBox(btnPregunta29Resp1, btnPregunta29Resp2, btnPregunta29Resp3, btnPregunta29Resp4, btnPregunta29Resp5);
                BackQuestionObject("APTITUD2-A-0029", APTITUD2_A_0029);

                int APTITUD2_A_0030 = BackSelectedCheckBox(btnPregunta30Resp1, btnPregunta30Resp2, btnPregunta30Resp3, btnPregunta30Resp4, btnPregunta30Resp5);
                BackQuestionObject("APTITUD2-A-0030", APTITUD2_A_0030);

                int APTITUD2_A_0031 = BackSelectedCheckBox(btnPregunta31Resp1, btnPregunta31Resp2, btnPregunta31Resp3, btnPregunta31Resp4, btnPregunta31Resp5);
                BackQuestionObject("APTITUD2-A-0031", APTITUD2_A_0031);

                int APTITUD2_A_0032 = BackSelectedCheckBox(btnPregunta32Resp1, btnPregunta32Resp2, btnPregunta32Resp3, btnPregunta32Resp4, btnPregunta32Resp5);
                BackQuestionObject("APTITUD2-A-0032", APTITUD2_A_0032);

                int APTITUD2_A_0033 = BackSelectedCheckBox(btnPregunta33Resp1, btnPregunta33Resp2, btnPregunta33Resp3, btnPregunta33Resp4, btnPregunta33Resp5);
                BackQuestionObject("APTITUD2-A-0033", APTITUD2_A_0033);

                int APTITUD2_A_0034 = BackSelectedCheckBox(btnPregunta34Resp1, btnPregunta34Resp2, btnPregunta34Resp3, btnPregunta34Resp4, btnPregunta34Resp5);
                BackQuestionObject("APTITUD2-A-0034", APTITUD2_A_0034);

                int APTITUD2_A_0035 = BackSelectedCheckBox(btnPregunta35Resp1, btnPregunta35Resp2, btnPregunta35Resp3, btnPregunta35Resp4, btnPregunta35Resp5);
                BackQuestionObject("APTITUD2-A-0035", APTITUD2_A_0035);

                int APTITUD2_A_0036 = BackSelectedCheckBox(btnPregunta36Resp1, btnPregunta36Resp2, btnPregunta36Resp3, btnPregunta36Resp4, btnPregunta36Resp5);
                BackQuestionObject("APTITUD2-A-0036", APTITUD2_A_0036);

                int APTITUD2_A_0037 = BackSelectedCheckBox(btnPregunta37Resp1, btnPregunta37Resp2, btnPregunta37Resp3, btnPregunta37Resp4, btnPregunta37Resp5);
                BackQuestionObject("APTITUD2-A-0037", APTITUD2_A_0037);

                int APTITUD2_A_0038 = BackSelectedCheckBox(btnPregunta38Resp1, btnPregunta38Resp2, btnPregunta38Resp3, btnPregunta38Resp4, btnPregunta38Resp5);
                BackQuestionObject("APTITUD2-A-0038", APTITUD2_A_0038);

                int APTITUD2_A_0039 = BackSelectedCheckBox(btnPregunta39Resp1, btnPregunta39Resp2, btnPregunta39Resp3, btnPregunta39Resp4, btnPregunta39Resp5);
                BackQuestionObject("APTITUD2-A-0039", APTITUD2_A_0039);

                int APTITUD2_A_0040 = BackSelectedCheckBox(btnPregunta40Resp1, btnPregunta40Resp2, btnPregunta40Resp3, btnPregunta40Resp4, btnPregunta40Resp5);
                BackQuestionObject("APTITUD2-A-0040", APTITUD2_A_0040);

                int APTITUD2_A_0041 = BackSelectedCheckBox(btnPregunta41Resp1, btnPregunta41Resp2, btnPregunta41Resp3, btnPregunta41Resp4, btnPregunta41Resp5);
                BackQuestionObject("APTITUD2-A-0041", APTITUD2_A_0041);

                int APTITUD2_A_0042 = BackSelectedCheckBox(btnPregunta42Resp1, btnPregunta42Resp2, btnPregunta42Resp3, btnPregunta42Resp4, btnPregunta42Resp5);
                BackQuestionObject("APTITUD2-A-0042", APTITUD2_A_0042);

                int APTITUD2_A_0043 = BackSelectedCheckBox(btnPregunta43Resp1, btnPregunta43Resp2, btnPregunta43Resp3, btnPregunta43Resp4, btnPregunta43Resp4);
                BackQuestionObject("APTITUD2-A-0043", APTITUD2_A_0043);

                int APTITUD2_A_0044 = BackSelectedCheckBox(btnPregunta44Resp1, btnPregunta44Resp2, btnPregunta44Resp3, btnPregunta44Resp4, btnPregunta44Resp5);
                BackQuestionObject("APTITUD2-A-0044", APTITUD2_A_0044);

                int APTITUD2_A_0045 = BackSelectedCheckBox(btnPregunta45Resp1, btnPregunta45Resp2, btnPregunta45Resp3, btnPregunta45Resp4, btnPregunta45Resp5);
                BackQuestionObject("APTITUD2-A-0045", APTITUD2_A_0045);

                int APTITUD2_A_0046 = BackSelectedCheckBox(btnPregunta46Resp1, btnPregunta46Resp2, btnPregunta46Resp3, btnPregunta46Resp4, btnPregunta46Resp5);
                BackQuestionObject("APTITUD2-A-0046", APTITUD2_A_0046);

                int APTITUD2_A_0047 = BackSelectedCheckBox(btnPregunta47Resp1, btnPregunta47Resp2, btnPregunta47Resp3, btnPregunta47Resp4, btnPregunta47Resp5);
                BackQuestionObject("APTITUD2-A-0047", APTITUD2_A_0047);

                int APTITUD2_A_0048 = BackSelectedCheckBox(btnPregunta48Resp1, btnPregunta48Resp2, btnPregunta48Resp3, btnPregunta48Resp4, btnPregunta48Resp5);
                BackQuestionObject("APTITUD2-A-0048", APTITUD2_A_0048);

                int APTITUD2_A_0049 = BackSelectedCheckBox(btnPregunta49Resp1, btnPregunta49Resp2, btnPregunta49Resp3, btnPregunta49Resp4, btnPregunta49Resp5);
                BackQuestionObject("APTITUD2-A-0049", APTITUD2_A_0049);

                int APTITUD2_A_0050 = BackSelectedCheckBox(btnPregunta50Resp1, btnPregunta50Resp2, btnPregunta50Resp3, btnPregunta50Resp4, btnPregunta50Resp5);
                BackQuestionObject("APTITUD2-A-0050", APTITUD2_A_0050);

                int APTITUD2_A_0051 = BackSelectedCheckBox(btnPregunta51Resp1, btnPregunta51Resp2, btnPregunta51Resp3, btnPregunta51Resp4, btnPregunta51Resp5);
                BackQuestionObject("APTITUD2-A-0051", APTITUD2_A_0051);

                int APTITUD2_A_0052 = BackSelectedCheckBox(btnPregunta52Resp1, btnPregunta52Resp2, btnPregunta52Resp3, btnPregunta52Resp4, btnPregunta52Resp5);
                BackQuestionObject("APTITUD2-A-0052", APTITUD2_A_0052);

                int APTITUD2_A_0053 = BackSelectedCheckBox(btnPregunta53Resp1, btnPregunta53Resp2, btnPregunta53Resp3, btnPregunta53Resp4, btnPregunta53Resp5);
                BackQuestionObject("APTITUD2-A-0053", APTITUD2_A_0053);

                int APTITUD2_A_0054 = BackSelectedCheckBox(btnPregunta54Resp1, btnPregunta54Resp2, btnPregunta54Resp3, btnPregunta54Resp4, btnPregunta54Resp5);
                BackQuestionObject("APTITUD2-A-0054", APTITUD2_A_0054);

                int APTITUD2_A_0055 = BackSelectedCheckBox(btnPregunta55Resp1, btnPregunta55Resp2, btnPregunta55Resp3, btnPregunta55Resp4, btnPregunta55Resp5);
                BackQuestionObject("APTITUD2-A-0055", APTITUD2_A_0055);

                int APTITUD2_A_0056 = BackSelectedCheckBox(btnPregunta56Resp1, btnPregunta56Resp2, btnPregunta56Resp3, btnPregunta56Resp4, btnPregunta56Resp5);
                BackQuestionObject("APTITUD2-A-0056", APTITUD2_A_0056);

                int APTITUD2_A_0057 = BackSelectedCheckBox(btnPregunta57Resp1, btnPregunta57Resp2, btnPregunta57Resp3, btnPregunta57Resp4, btnPregunta57Resp5);
                BackQuestionObject("APTITUD2-A-0057", APTITUD2_A_0057);

                int APTITUD2_A_0058 = BackSelectedCheckBox(btnPregunta58Resp1, btnPregunta58Resp2, btnPregunta58Resp3, btnPregunta58Resp4, btnPregunta58Resp5);
                BackQuestionObject("APTITUD2-A-0058", APTITUD2_A_0058);

                int APTITUD2_A_0059 = BackSelectedCheckBox(btnPregunta59Resp1, btnPregunta59Resp2, btnPregunta59Resp3, btnPregunta59Resp4, btnPregunta59Resp5);
                BackQuestionObject("APTITUD2-A-0059", APTITUD2_A_0059);

                int APTITUD2_A_0060 = BackSelectedCheckBox(btnPregunta60Resp1, btnPregunta60Resp2, btnPregunta60Resp3, btnPregunta60Resp4, btnPregunta60Resp5);
                BackQuestionObject("APTITUD2-A-0060", APTITUD2_A_0060);

                int APTITUD2_A_0061 = BackSelectedCheckBox(btnPregunta61Resp1, btnPregunta61Resp2, btnPregunta61Resp3, btnPregunta61Resp4, btnPregunta61Resp5);
                BackQuestionObject("APTITUD2-A-0061", APTITUD2_A_0061);

                int APTITUD2_A_0062 = BackSelectedCheckBox(btnPregunta62Resp1, btnPregunta62Resp2, btnPregunta62Resp3, btnPregunta62Resp4, btnPregunta62Resp5);
                BackQuestionObject("APTITUD2-A-0062", APTITUD2_A_0062);

                int APTITUD2_A_0063 = BackSelectedCheckBox(btnPregunta63Resp1, btnPregunta63Resp2, btnPregunta63Resp3, btnPregunta63Resp4, btnPregunta63Resp5);
                BackQuestionObject("APTITUD2-A-0063", APTITUD2_A_0063);

                int APTITUD2_A_0064 = BackSelectedCheckBox(btnPregunta64Resp1, btnPregunta64Resp2, btnPregunta64Resp3, btnPregunta64Resp4, btnPregunta64Resp5);
                BackQuestionObject("APTITUD2-A-0064", APTITUD2_A_0064);

                int APTITUD2_A_0065 = BackSelectedCheckBox(btnPregunta65Resp1, btnPregunta65Resp2, btnPregunta65Resp3, btnPregunta65Resp4, btnPregunta65Resp5);
                BackQuestionObject("APTITUD2-A-0065", APTITUD2_A_0065);

                int APTITUD2_A_0066 = BackSelectedCheckBox(btnPregunta66Resp1, btnPregunta66Resp2, btnPregunta66Resp3, btnPregunta66Resp4, btnPregunta66Resp5);
                BackQuestionObject("APTITUD2-A-0066", APTITUD2_A_0066);

                int APTITUD2_A_0067 = BackSelectedCheckBox(btnPregunta67Resp1, btnPregunta67Resp2, btnPregunta67Resp3, btnPregunta67Resp4, btnPregunta67Resp5);
                BackQuestionObject("APTITUD2-A-0067", APTITUD2_A_0067);

                int APTITUD2_A_0068 = BackSelectedCheckBox(btnPregunta68Resp1, btnPregunta68Resp2, btnPregunta68Resp3, btnPregunta68Resp4, btnPregunta68Resp5);
                BackQuestionObject("APTITUD2-A-0068", APTITUD2_A_0068);

                int APTITUD2_A_0069 = BackSelectedCheckBox(btnPregunta69Resp1, btnPregunta69Resp2, btnPregunta69Resp3, btnPregunta69Resp4, btnPregunta69Resp5);
                BackQuestionObject("APTITUD2-A-0069", APTITUD2_A_0069);

                int APTITUD2_A_0070 = BackSelectedCheckBox(btnPregunta70Resp1, btnPregunta70Resp2, btnPregunta70Resp3, btnPregunta70Resp4, btnPregunta70Resp5);
                BackQuestionObject("APTITUD2-A-0070", APTITUD2_A_0070);

                int APTITUD2_A_0071 = BackSelectedCheckBox(btnPregunta71Resp1, btnPregunta71Resp2, btnPregunta71Resp3, btnPregunta71Resp4, btnPregunta71Resp5);
                BackQuestionObject("APTITUD2-A-0071", APTITUD2_A_0071);

                int APTITUD2_A_0072 = BackSelectedCheckBox(btnPregunta72Resp1, btnPregunta72Resp2, btnPregunta72Resp3, btnPregunta72Resp4, btnPregunta72Resp5);
                BackQuestionObject("APTITUD2-A-0072", APTITUD2_A_0072);

                int APTITUD2_A_0073 = BackSelectedCheckBox(btnPregunta73Resp1, btnPregunta73Resp2, btnPregunta73Resp3, btnPregunta73Resp4, btnPregunta73Resp5);
                BackQuestionObject("APTITUD2-A-0073", APTITUD2_A_0073);

                int APTITUD2_A_0074 = BackSelectedCheckBox(btnPregunta74Resp1, btnPregunta74Resp2, btnPregunta74Resp3, btnPregunta74Resp4, btnPregunta74Resp5);
                BackQuestionObject("APTITUD2-A-0074", APTITUD2_A_0074);

                int APTITUD2_A_0075 = BackSelectedCheckBox(btnPregunta75Resp1, btnPregunta75Resp2, btnPregunta75Resp3, btnPregunta75Resp4, btnPregunta75Resp5);
                BackQuestionObject("APTITUD2-A-0075", APTITUD2_A_0075);

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
                var vObjetoPrueba = nKprueba.Obtener_K_PRUEBA(pClTokenExterno:vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();

                if (vObjetoPrueba != null)
                {
                    E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vObjetoPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: "APTITUD_MENTAL2", usuario: vClUsuario, programa: vNbPrograma);
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

                int APTITUD2_A_0001 = BackSelectedCheckBox(btnPregunta1Resp1, btnPregunta1Resp2, btnPregunta1Resp3, btnPregunta1Resp4, btnPregunta1Resp5);
                BackQuestionObject("APTITUD2-A-0001", APTITUD2_A_0001);

                int APTITUD2_A_0002 = BackSelectedCheckBox(btnPregunta2Resp1, btnPregunta2Resp2, btnPregunta2Resp3, btnPregunta2Resp4, btnPregunta2Resp5);
                BackQuestionObject("APTITUD2-A-0002", APTITUD2_A_0002);

                int APTITUD2_A_0003 = BackSelectedCheckBox(btnPregunta3Resp1, btnPregunta3Resp2, btnPregunta3Resp3, btnPregunta3Resp4, btnPregunta3Resp5);
                BackQuestionObject("APTITUD2-A-0003", APTITUD2_A_0003);

                int APTITUD2_A_0004 = BackSelectedCheckBox(btnPregunta4Resp1, btnPregunta4Resp2, btnPregunta4Resp3, btnPregunta4Resp4, btnPregunta4Resp5);
                BackQuestionObject("APTITUD2-A-0004", APTITUD2_A_0004);

                int APTITUD2_A_0005 = BackSelectedCheckBox(btnPregunta5Resp1, btnPregunta5Resp2, btnPregunta5Resp3, btnPregunta5Resp4, btnPregunta5Resp5);
                BackQuestionObject("APTITUD2-A-0005", APTITUD2_A_0005);

                int APTITUD2_A_0006 = BackSelectedCheckBox(btnPregunta6Resp1, btnPregunta6Resp2, btnPregunta6Resp3, btnPregunta6Resp4, btnPregunta6Resp5);
                BackQuestionObject("APTITUD2-A-0006", APTITUD2_A_0006);

                int APTITUD2_A_0007 = BackSelectedCheckBox(btnPregunta7Resp1, btnPregunta7Resp2, btnPregunta7Resp3, btnPregunta7Resp4, btnPregunta7Resp5);
                BackQuestionObject("APTITUD2-A-0007", APTITUD2_A_0007);

                int APTITUD2_A_0008 = BackSelectedCheckBox(btnPregunta8Resp1, btnPregunta8Resp2, btnPregunta8Resp3, btnPregunta8Resp4, btnPregunta8Resp5);
                BackQuestionObject("APTITUD2-A-0008", APTITUD2_A_0008);

                int APTITUD2_A_0009 = BackSelectedCheckBox(btnPregunta9Resp1, btnPregunta9Resp2, btnPregunta9Resp3, btnPregunta9Resp4, btnPregunta9Resp5);
                BackQuestionObject("APTITUD2-A-0009", APTITUD2_A_0009);

                int APTITUD2_A_0010 = BackSelectedCheckBox(btnPregunta10Resp1, btnPregunta10Resp2, btnPregunta10Resp3, btnPregunta10Resp4, btnPregunta10Resp5);
                BackQuestionObject("APTITUD2-A-0010", APTITUD2_A_0010);

                int APTITUD2_A_0011 = BackSelectedCheckBox(btnPregunta11Resp1, btnPregunta12Resp2, btnPregunta11Resp3, btnPregunta11Resp4, btnPregunta11Resp5);
                BackQuestionObject("APTITUD2-A-0011", APTITUD2_A_0011);

                int APTITUD2_A_0012 = BackSelectedCheckBox(btnPregunta12Resp1, btnPregunta12Resp2, btnPregunta12Resp3, btnPregunta12Resp4, btnPregunta12Resp5);
                BackQuestionObject("APTITUD2-A-0012", APTITUD2_A_0012);

                int APTITUD2_A_0013 = BackSelectedCheckBox(btnPregunta13Resp1, btnPregunta13Resp2, btnPregunta13Resp3, btnPregunta13Resp4, btnPregunta13Resp5);
                BackQuestionObject("APTITUD2-A-0013", APTITUD2_A_0013);

                int APTITUD2_A_0014 = BackSelectedCheckBox(btnPregunta14Resp1, btnPregunta14Resp2, btnPregunta14Resp3, btnPregunta14Resp4, btnPregunta14Resp5);
                BackQuestionObject("APTITUD2-A-0014", APTITUD2_A_0014);

                int APTITUD2_A_0015 = BackSelectedCheckBox(btnPregunta15Resp1, btnPregunta15Resp2, btnPregunta15Resp3, btnPregunta15Resp4, btnPregunta15Resp5);
                BackQuestionObject("APTITUD2-A-0015", APTITUD2_A_0015);

                int APTITUD2_A_0016 = BackSelectedCheckBox(btnPregunta16Resp1, btnPregunta16Resp2, btnPregunta16Resp3, btnPregunta16Resp4, btnPregunta16Resp5);
                BackQuestionObject("APTITUD2-A-0016", APTITUD2_A_0016);

                int APTITUD2_A_0017 = BackSelectedCheckBox(btnPregunta17Resp1, btnPregunta17Resp2, btnPregunta17Resp3, btnPregunta17Resp4, btnPregunta17Resp5);
                BackQuestionObject("APTITUD2-A-0017", APTITUD2_A_0017);

                int APTITUD2_A_0018 = BackSelectedCheckBox(btnPregunta18Resp1, btnPregunta18Resp2, btnPregunta18Resp3, btnPregunta18Resp4, btnPregunta18Resp5);
                BackQuestionObject("APTITUD2-A-0018", APTITUD2_A_0018);

                int APTITUD2_A_0019 = BackSelectedCheckBox(btnPregunta19Resp1, btnPregunta19Resp2, btnPregunta19Resp3, btnPregunta19Resp4, btnPregunta19Resp5);
                BackQuestionObject("APTITUD2-A-0019", APTITUD2_A_0019);

                int APTITUD2_A_0020 = BackSelectedCheckBox(btnPregunta20Resp1, btnPregunta20Resp2, btnPregunta20Resp3, btnPregunta20Resp4, btnPregunta20Resp5);
                BackQuestionObject("APTITUD2-A-0020", APTITUD2_A_0020);

                int APTITUD2_A_0021 = BackSelectedCheckBox(btnPregunta21Resp1, btnPregunta21Resp2, btnPregunta21Resp3, btnPregunta21Resp4, btnPregunta21Resp5);
                BackQuestionObject("APTITUD2-A-0021", APTITUD2_A_0021);

                int APTITUD2_A_0022 = BackSelectedCheckBox(btnPregunta22Resp1, btnPregunta22Resp2, btnPregunta22Resp3, btnPregunta22Resp4, btnPregunta22Resp5);
                BackQuestionObject("APTITUD2-A-0022", APTITUD2_A_0022);

                int APTITUD2_A_0023 = BackSelectedCheckBox(btnPregunta23Resp1, btnPregunta23Resp2, btnPregunta23Resp3, btnPregunta23Resp4, btnPregunta23Resp5);
                BackQuestionObject("APTITUD2-A-0023", APTITUD2_A_0023);

                int APTITUD2_A_0024 = BackSelectedCheckBox(btnPregunta24Resp1, btnPregunta24Resp2, btnPregunta24Resp3, btnPregunta24Resp4, btnPregunta24Resp5);
                BackQuestionObject("APTITUD2-A-0024", APTITUD2_A_0024);

                int APTITUD2_A_0025 = BackSelectedCheckBox(btnPregunta25Resp1, btnPregunta25Resp2, btnPregunta25Resp3, btnPregunta25Resp4, btnPregunta25Resp5);
                BackQuestionObject("APTITUD2-A-0025", APTITUD2_A_0025);

                int APTITUD2_A_0026 = BackSelectedCheckBox(btnPregunta26Resp1, btnPregunta26Resp2, btnPregunta26Resp3, btnPregunta26Resp4, btnPregunta26Resp5);
                BackQuestionObject("APTITUD2-A-0026", APTITUD2_A_0026);

                int APTITUD2_A_0027 = BackSelectedCheckBox(btnPregunta27Resp1, btnPregunta27Resp2, btnPregunta27Resp3, btnPregunta27Resp4, btnPregunta27Resp5);
                BackQuestionObject("APTITUD2-A-0027", APTITUD2_A_0027);

                int APTITUD2_A_0028 = BackSelectedCheckBox(btnPregunta28Resp1, btnPregunta28Resp2, btnPregunta28Resp3, btnPregunta28Resp4, btnPregunta28Resp5);
                BackQuestionObject("APTITUD2-A-0028", APTITUD2_A_0028);

                int APTITUD2_A_0029 = BackSelectedCheckBox(btnPregunta29Resp1, btnPregunta29Resp2, btnPregunta29Resp3, btnPregunta29Resp4, btnPregunta29Resp5);
                BackQuestionObject("APTITUD2-A-0029", APTITUD2_A_0029);

                int APTITUD2_A_0030 = BackSelectedCheckBox(btnPregunta30Resp1, btnPregunta30Resp2, btnPregunta30Resp3, btnPregunta30Resp4, btnPregunta30Resp5);
                BackQuestionObject("APTITUD2-A-0030", APTITUD2_A_0030);

                int APTITUD2_A_0031 = BackSelectedCheckBox(btnPregunta31Resp1, btnPregunta31Resp2, btnPregunta31Resp3, btnPregunta31Resp4, btnPregunta31Resp5);
                BackQuestionObject("APTITUD2-A-0031", APTITUD2_A_0031);

                int APTITUD2_A_0032 = BackSelectedCheckBox(btnPregunta32Resp1, btnPregunta32Resp2, btnPregunta32Resp3, btnPregunta32Resp4, btnPregunta32Resp5);
                BackQuestionObject("APTITUD2-A-0032", APTITUD2_A_0032);

                int APTITUD2_A_0033 = BackSelectedCheckBox(btnPregunta33Resp1, btnPregunta33Resp2, btnPregunta33Resp3, btnPregunta33Resp4, btnPregunta33Resp5);
                BackQuestionObject("APTITUD2-A-0033", APTITUD2_A_0033);

                int APTITUD2_A_0034 = BackSelectedCheckBox(btnPregunta34Resp1, btnPregunta34Resp2, btnPregunta34Resp3, btnPregunta34Resp4, btnPregunta34Resp5);
                BackQuestionObject("APTITUD2-A-0034", APTITUD2_A_0034);

                int APTITUD2_A_0035 = BackSelectedCheckBox(btnPregunta35Resp1, btnPregunta35Resp2, btnPregunta35Resp3, btnPregunta35Resp4, btnPregunta35Resp5);
                BackQuestionObject("APTITUD2-A-0035", APTITUD2_A_0035);

                int APTITUD2_A_0036 = BackSelectedCheckBox(btnPregunta36Resp1, btnPregunta36Resp2, btnPregunta36Resp3, btnPregunta36Resp4, btnPregunta36Resp5);
                BackQuestionObject("APTITUD2-A-0036", APTITUD2_A_0036);

                int APTITUD2_A_0037 = BackSelectedCheckBox(btnPregunta37Resp1, btnPregunta37Resp2, btnPregunta37Resp3, btnPregunta37Resp4, btnPregunta37Resp5);
                BackQuestionObject("APTITUD2-A-0037", APTITUD2_A_0037);

                int APTITUD2_A_0038 = BackSelectedCheckBox(btnPregunta38Resp1, btnPregunta38Resp2, btnPregunta38Resp3, btnPregunta38Resp4, btnPregunta38Resp5);
                BackQuestionObject("APTITUD2-A-0038", APTITUD2_A_0038);

                int APTITUD2_A_0039 = BackSelectedCheckBox(btnPregunta39Resp1, btnPregunta39Resp2, btnPregunta39Resp3, btnPregunta39Resp4, btnPregunta39Resp5);
                BackQuestionObject("APTITUD2-A-0039", APTITUD2_A_0039);

                int APTITUD2_A_0040 = BackSelectedCheckBox(btnPregunta40Resp1, btnPregunta40Resp2, btnPregunta40Resp3, btnPregunta40Resp4, btnPregunta40Resp5);
                BackQuestionObject("APTITUD2-A-0040", APTITUD2_A_0040);

                int APTITUD2_A_0041 = BackSelectedCheckBox(btnPregunta41Resp1, btnPregunta41Resp2, btnPregunta41Resp3, btnPregunta41Resp4, btnPregunta41Resp5);
                BackQuestionObject("APTITUD2-A-0041", APTITUD2_A_0041);

                int APTITUD2_A_0042 = BackSelectedCheckBox(btnPregunta42Resp1, btnPregunta42Resp2, btnPregunta42Resp3, btnPregunta42Resp4, btnPregunta42Resp5);
                BackQuestionObject("APTITUD2-A-0042", APTITUD2_A_0042);

                int APTITUD2_A_0043 = BackSelectedCheckBox(btnPregunta43Resp1, btnPregunta43Resp2, btnPregunta43Resp3, btnPregunta43Resp4, btnPregunta43Resp4);
                BackQuestionObject("APTITUD2-A-0043", APTITUD2_A_0043);

                int APTITUD2_A_0044 = BackSelectedCheckBox(btnPregunta44Resp1, btnPregunta44Resp2, btnPregunta44Resp3, btnPregunta44Resp4, btnPregunta44Resp5);
                BackQuestionObject("APTITUD2-A-0044", APTITUD2_A_0044);

                int APTITUD2_A_0045 = BackSelectedCheckBox(btnPregunta45Resp1, btnPregunta45Resp2, btnPregunta45Resp3, btnPregunta45Resp4, btnPregunta45Resp5);
                BackQuestionObject("APTITUD2-A-0045", APTITUD2_A_0045);

                int APTITUD2_A_0046 = BackSelectedCheckBox(btnPregunta46Resp1, btnPregunta46Resp2, btnPregunta46Resp3, btnPregunta46Resp4, btnPregunta46Resp5);
                BackQuestionObject("APTITUD2-A-0046", APTITUD2_A_0046);

                int APTITUD2_A_0047 = BackSelectedCheckBox(btnPregunta47Resp1, btnPregunta47Resp2, btnPregunta47Resp3, btnPregunta47Resp4, btnPregunta47Resp5);
                BackQuestionObject("APTITUD2-A-0047", APTITUD2_A_0047);

                int APTITUD2_A_0048 = BackSelectedCheckBox(btnPregunta48Resp1, btnPregunta48Resp2, btnPregunta48Resp3, btnPregunta48Resp4, btnPregunta48Resp5);
                BackQuestionObject("APTITUD2-A-0048", APTITUD2_A_0048);

                int APTITUD2_A_0049 = BackSelectedCheckBox(btnPregunta49Resp1, btnPregunta49Resp2, btnPregunta49Resp3, btnPregunta49Resp4, btnPregunta49Resp5);
                BackQuestionObject("APTITUD2-A-0049", APTITUD2_A_0049);

                int APTITUD2_A_0050 = BackSelectedCheckBox(btnPregunta50Resp1, btnPregunta50Resp2, btnPregunta50Resp3, btnPregunta50Resp4, btnPregunta50Resp5);
                BackQuestionObject("APTITUD2-A-0050", APTITUD2_A_0050);

                int APTITUD2_A_0051 = BackSelectedCheckBox(btnPregunta51Resp1, btnPregunta51Resp2, btnPregunta51Resp3, btnPregunta51Resp4, btnPregunta51Resp5);
                BackQuestionObject("APTITUD2-A-0051", APTITUD2_A_0051);

                int APTITUD2_A_0052 = BackSelectedCheckBox(btnPregunta52Resp1, btnPregunta52Resp2, btnPregunta52Resp3, btnPregunta52Resp4, btnPregunta52Resp5);
                BackQuestionObject("APTITUD2-A-0052", APTITUD2_A_0052);

                int APTITUD2_A_0053 = BackSelectedCheckBox(btnPregunta53Resp1, btnPregunta53Resp2, btnPregunta53Resp3, btnPregunta53Resp4, btnPregunta53Resp5);
                BackQuestionObject("APTITUD2-A-0053", APTITUD2_A_0053);

                int APTITUD2_A_0054 = BackSelectedCheckBox(btnPregunta54Resp1, btnPregunta54Resp2, btnPregunta54Resp3, btnPregunta54Resp4, btnPregunta54Resp5);
                BackQuestionObject("APTITUD2-A-0054", APTITUD2_A_0054);

                int APTITUD2_A_0055 = BackSelectedCheckBox(btnPregunta55Resp1, btnPregunta55Resp2, btnPregunta55Resp3, btnPregunta55Resp4, btnPregunta55Resp5);
                BackQuestionObject("APTITUD2-A-0055", APTITUD2_A_0055);

                int APTITUD2_A_0056 = BackSelectedCheckBox(btnPregunta56Resp1, btnPregunta56Resp2, btnPregunta56Resp3, btnPregunta56Resp4, btnPregunta56Resp5);
                BackQuestionObject("APTITUD2-A-0056", APTITUD2_A_0056);

                int APTITUD2_A_0057 = BackSelectedCheckBox(btnPregunta57Resp1, btnPregunta57Resp2, btnPregunta57Resp3, btnPregunta57Resp4, btnPregunta57Resp5);
                BackQuestionObject("APTITUD2-A-0057", APTITUD2_A_0057);

                int APTITUD2_A_0058 = BackSelectedCheckBox(btnPregunta58Resp1, btnPregunta58Resp2, btnPregunta58Resp3, btnPregunta58Resp4, btnPregunta58Resp5);
                BackQuestionObject("APTITUD2-A-0058", APTITUD2_A_0058);

                int APTITUD2_A_0059 = BackSelectedCheckBox(btnPregunta59Resp1, btnPregunta59Resp2, btnPregunta59Resp3, btnPregunta59Resp4, btnPregunta59Resp5);
                BackQuestionObject("APTITUD2-A-0059", APTITUD2_A_0059);

                int APTITUD2_A_0060 = BackSelectedCheckBox(btnPregunta60Resp1, btnPregunta60Resp2, btnPregunta60Resp3, btnPregunta60Resp4, btnPregunta60Resp5);
                BackQuestionObject("APTITUD2-A-0060", APTITUD2_A_0060);

                int APTITUD2_A_0061 = BackSelectedCheckBox(btnPregunta61Resp1, btnPregunta61Resp2, btnPregunta61Resp3, btnPregunta61Resp4, btnPregunta61Resp5);
                BackQuestionObject("APTITUD2-A-0061", APTITUD2_A_0061);

                int APTITUD2_A_0062 = BackSelectedCheckBox(btnPregunta62Resp1, btnPregunta62Resp2, btnPregunta62Resp3, btnPregunta62Resp4, btnPregunta62Resp5);
                BackQuestionObject("APTITUD2-A-0062", APTITUD2_A_0062);

                int APTITUD2_A_0063 = BackSelectedCheckBox(btnPregunta63Resp1, btnPregunta63Resp2, btnPregunta63Resp3, btnPregunta63Resp4, btnPregunta63Resp5);
                BackQuestionObject("APTITUD2-A-0063", APTITUD2_A_0063);

                int APTITUD2_A_0064 = BackSelectedCheckBox(btnPregunta64Resp1, btnPregunta64Resp2, btnPregunta64Resp3, btnPregunta64Resp4, btnPregunta64Resp5);
                BackQuestionObject("APTITUD2-A-0064", APTITUD2_A_0064);

                int APTITUD2_A_0065 = BackSelectedCheckBox(btnPregunta65Resp1, btnPregunta65Resp2, btnPregunta65Resp3, btnPregunta65Resp4, btnPregunta65Resp5);
                BackQuestionObject("APTITUD2-A-0065", APTITUD2_A_0065);

                int APTITUD2_A_0066 = BackSelectedCheckBox(btnPregunta66Resp1, btnPregunta66Resp2, btnPregunta66Resp3, btnPregunta66Resp4, btnPregunta66Resp5);
                BackQuestionObject("APTITUD2-A-0066", APTITUD2_A_0066);

                int APTITUD2_A_0067 = BackSelectedCheckBox(btnPregunta67Resp1, btnPregunta67Resp2, btnPregunta67Resp3, btnPregunta67Resp4, btnPregunta67Resp5);
                BackQuestionObject("APTITUD2-A-0067", APTITUD2_A_0067);

                int APTITUD2_A_0068 = BackSelectedCheckBox(btnPregunta68Resp1, btnPregunta68Resp2, btnPregunta68Resp3, btnPregunta68Resp4, btnPregunta68Resp5);
                BackQuestionObject("APTITUD2-A-0068", APTITUD2_A_0068);

                int APTITUD2_A_0069 = BackSelectedCheckBox(btnPregunta69Resp1, btnPregunta69Resp2, btnPregunta69Resp3, btnPregunta69Resp4, btnPregunta69Resp5);
                BackQuestionObject("APTITUD2-A-0069", APTITUD2_A_0069);

                int APTITUD2_A_0070 = BackSelectedCheckBox(btnPregunta70Resp1, btnPregunta70Resp2, btnPregunta70Resp3, btnPregunta70Resp4, btnPregunta70Resp5);
                BackQuestionObject("APTITUD2-A-0070", APTITUD2_A_0070);

                int APTITUD2_A_0071 = BackSelectedCheckBox(btnPregunta71Resp1, btnPregunta71Resp2, btnPregunta71Resp3, btnPregunta71Resp4, btnPregunta71Resp5);
                BackQuestionObject("APTITUD2-A-0071", APTITUD2_A_0071);

                int APTITUD2_A_0072 = BackSelectedCheckBox(btnPregunta72Resp1, btnPregunta72Resp2, btnPregunta72Resp3, btnPregunta72Resp4, btnPregunta72Resp5);
                BackQuestionObject("APTITUD2-A-0072", APTITUD2_A_0072);

                int APTITUD2_A_0073 = BackSelectedCheckBox(btnPregunta73Resp1, btnPregunta73Resp2, btnPregunta73Resp3, btnPregunta73Resp4, btnPregunta73Resp5);
                BackQuestionObject("APTITUD2-A-0073", APTITUD2_A_0073);

                int APTITUD2_A_0074 = BackSelectedCheckBox(btnPregunta74Resp1, btnPregunta74Resp2, btnPregunta74Resp3, btnPregunta74Resp4, btnPregunta74Resp5);
                BackQuestionObject("APTITUD2-A-0074", APTITUD2_A_0074);

                int APTITUD2_A_0075 = BackSelectedCheckBox(btnPregunta75Resp1, btnPregunta75Resp2, btnPregunta75Resp3, btnPregunta75Resp4, btnPregunta75Resp5);
                BackQuestionObject("APTITUD2-A-0075", APTITUD2_A_0075);

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
                    E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vObjetoPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: "APTITUD_MENTAL2", usuario: vClUsuario, programa: vNbPrograma);
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
            SaveTest();
        }

        public void BackQuestionObject(string pclPregunta, int pnbRespuesta)
        {
            var vPregunta = vRespuestas.Where(x => x.CL_PREGUNTA.Equals(pclPregunta)).FirstOrDefault();
            if (vPregunta != null)
            {
                String vNbRespuesta="-";
                switch (pnbRespuesta)
                {
                    case 1:
                        vNbRespuesta = "a";
                        break;
                    case 2:
                        vNbRespuesta = "b";
                        break;
                    case 3:
                        vNbRespuesta = "c";
                        break;
                    case 4:
                        vNbRespuesta = "d";
                        break;
                    case 5:
                        vNbRespuesta = "e";
                        break;
                }
                vPregunta.NB_RESPUESTA = vNbRespuesta;
                //vPregunta.NO_VALOR_RESPUESTA = (vNoValor = (pnbRespuesta != "-") ? 1 : 0);
                vPregunta.NO_VALOR_RESPUESTA = pnbRespuesta;
            }
        }

        public int BackSelectedCheckBox(RadButton a, RadButton b, RadButton c, RadButton d, RadButton e)
        {
            int resultado = 0;
            if (a.Checked)
            { resultado = 1; }
            else if (b.Checked)
            { resultado = 2; }
            if (c.Checked)
            { resultado = 3; }
            else if (d.Checked)
            { resultado = 4; }
            else if (e.Checked)
            { resultado = 5; }
            
            return resultado;
        }

        public void asignarValoresManual(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> respuestas)
        {
            foreach (SPE_OBTIENE_RESULTADO_PRUEBA_Result resp in respuestas)
            {
                switch (resp.CL_PREGUNTA)
                {
                    case "APTITUD2-RES-0001": SeleccionarBotonRespuesta(btnPregunta1Resp1, btnPregunta1Resp2, btnPregunta1Resp3, btnPregunta1Resp4, btnPregunta1Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0002": SeleccionarBotonRespuesta(btnPregunta2Resp1, btnPregunta2Resp2, btnPregunta2Resp3, btnPregunta2Resp4, btnPregunta2Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0003": SeleccionarBotonRespuesta(btnPregunta3Resp1, btnPregunta3Resp2, btnPregunta3Resp3, btnPregunta3Resp4, btnPregunta3Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0004": SeleccionarBotonRespuesta(btnPregunta4Resp1, btnPregunta4Resp2, btnPregunta4Resp3, btnPregunta4Resp4, btnPregunta4Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0005": SeleccionarBotonRespuesta(btnPregunta5Resp1, btnPregunta5Resp2, btnPregunta5Resp3, btnPregunta5Resp4, btnPregunta5Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0006": SeleccionarBotonRespuesta(btnPregunta6Resp1, btnPregunta6Resp2, btnPregunta6Resp3, btnPregunta6Resp4, btnPregunta6Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0007": SeleccionarBotonRespuesta(btnPregunta7Resp1, btnPregunta7Resp2, btnPregunta7Resp3, btnPregunta7Resp4, btnPregunta7Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0008": SeleccionarBotonRespuesta(btnPregunta8Resp1, btnPregunta8Resp2, btnPregunta8Resp3, btnPregunta8Resp4, btnPregunta8Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0009": SeleccionarBotonRespuesta(btnPregunta9Resp1, btnPregunta9Resp2, btnPregunta9Resp3, btnPregunta9Resp4, btnPregunta9Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0010": SeleccionarBotonRespuesta(btnPregunta10Resp1, btnPregunta10Resp2, btnPregunta10Resp3, btnPregunta10Resp4, btnPregunta10Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0011": SeleccionarBotonRespuesta(btnPregunta11Resp1, btnPregunta11Resp2, btnPregunta11Resp3, btnPregunta11Resp4, btnPregunta11Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0012": SeleccionarBotonRespuesta(btnPregunta12Resp1, btnPregunta12Resp2, btnPregunta12Resp3, btnPregunta12Resp4, btnPregunta12Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0013": SeleccionarBotonRespuesta(btnPregunta13Resp1, btnPregunta13Resp2, btnPregunta13Resp3, btnPregunta13Resp4, btnPregunta13Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0014": SeleccionarBotonRespuesta(btnPregunta14Resp1, btnPregunta14Resp2, btnPregunta14Resp3, btnPregunta14Resp4, btnPregunta14Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0015": SeleccionarBotonRespuesta(btnPregunta15Resp1, btnPregunta15Resp2, btnPregunta15Resp3, btnPregunta15Resp4, btnPregunta15Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0016": SeleccionarBotonRespuesta(btnPregunta16Resp1, btnPregunta16Resp2, btnPregunta16Resp3, btnPregunta16Resp4, btnPregunta16Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0017": SeleccionarBotonRespuesta(btnPregunta17Resp1, btnPregunta17Resp2, btnPregunta17Resp3, btnPregunta17Resp4, btnPregunta17Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0018": SeleccionarBotonRespuesta(btnPregunta18Resp1, btnPregunta18Resp2, btnPregunta18Resp3, btnPregunta18Resp4, btnPregunta18Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0019": SeleccionarBotonRespuesta(btnPregunta19Resp1, btnPregunta19Resp2, btnPregunta19Resp3, btnPregunta19Resp4, btnPregunta19Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0020": SeleccionarBotonRespuesta(btnPregunta20Resp1, btnPregunta20Resp2, btnPregunta20Resp3, btnPregunta20Resp4, btnPregunta20Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0021": SeleccionarBotonRespuesta(btnPregunta21Resp1, btnPregunta21Resp2, btnPregunta21Resp3, btnPregunta21Resp4, btnPregunta21Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0022": SeleccionarBotonRespuesta(btnPregunta22Resp1, btnPregunta22Resp2, btnPregunta22Resp3, btnPregunta22Resp4, btnPregunta22Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0023": SeleccionarBotonRespuesta(btnPregunta23Resp1, btnPregunta23Resp2, btnPregunta23Resp3, btnPregunta23Resp4, btnPregunta23Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0024": SeleccionarBotonRespuesta(btnPregunta24Resp1, btnPregunta24Resp2, btnPregunta24Resp3, btnPregunta24Resp4, btnPregunta24Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0025": SeleccionarBotonRespuesta(btnPregunta25Resp1, btnPregunta25Resp2, btnPregunta25Resp3, btnPregunta25Resp4, btnPregunta25Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0026": SeleccionarBotonRespuesta(btnPregunta26Resp1, btnPregunta26Resp2, btnPregunta26Resp3, btnPregunta26Resp4, btnPregunta26Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0027": SeleccionarBotonRespuesta(btnPregunta27Resp1, btnPregunta27Resp2, btnPregunta27Resp3, btnPregunta27Resp4, btnPregunta27Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0028": SeleccionarBotonRespuesta(btnPregunta28Resp1, btnPregunta28Resp2, btnPregunta28Resp3, btnPregunta28Resp4, btnPregunta28Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0029": SeleccionarBotonRespuesta(btnPregunta29Resp1, btnPregunta29Resp2, btnPregunta29Resp3, btnPregunta29Resp4, btnPregunta29Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0030": SeleccionarBotonRespuesta(btnPregunta30Resp1, btnPregunta30Resp2, btnPregunta30Resp3, btnPregunta30Resp4, btnPregunta30Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0031": SeleccionarBotonRespuesta(btnPregunta31Resp1, btnPregunta31Resp2, btnPregunta31Resp3, btnPregunta31Resp4, btnPregunta31Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0032": SeleccionarBotonRespuesta(btnPregunta32Resp1, btnPregunta32Resp2, btnPregunta32Resp3, btnPregunta32Resp4, btnPregunta32Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0033": SeleccionarBotonRespuesta(btnPregunta33Resp1, btnPregunta33Resp2, btnPregunta33Resp3, btnPregunta33Resp4, btnPregunta33Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0034": SeleccionarBotonRespuesta(btnPregunta34Resp1, btnPregunta34Resp2, btnPregunta34Resp3, btnPregunta34Resp4, btnPregunta34Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0035": SeleccionarBotonRespuesta(btnPregunta35Resp1, btnPregunta35Resp2, btnPregunta35Resp3, btnPregunta35Resp4, btnPregunta35Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0036": SeleccionarBotonRespuesta(btnPregunta36Resp1, btnPregunta36Resp2, btnPregunta36Resp3, btnPregunta36Resp4, btnPregunta36Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0037": SeleccionarBotonRespuesta(btnPregunta37Resp1, btnPregunta37Resp2, btnPregunta37Resp3, btnPregunta37Resp4, btnPregunta37Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0038": SeleccionarBotonRespuesta(btnPregunta38Resp1, btnPregunta38Resp2, btnPregunta38Resp3, btnPregunta38Resp4, btnPregunta38Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0039": SeleccionarBotonRespuesta(btnPregunta39Resp1, btnPregunta39Resp2, btnPregunta39Resp3, btnPregunta39Resp4, btnPregunta39Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0040": SeleccionarBotonRespuesta(btnPregunta40Resp1, btnPregunta40Resp2, btnPregunta40Resp3, btnPregunta40Resp4, btnPregunta40Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0041": SeleccionarBotonRespuesta(btnPregunta41Resp1, btnPregunta41Resp2, btnPregunta41Resp3, btnPregunta41Resp4, btnPregunta41Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0042": SeleccionarBotonRespuesta(btnPregunta42Resp1, btnPregunta42Resp2, btnPregunta42Resp3, btnPregunta42Resp4, btnPregunta42Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0043": SeleccionarBotonRespuesta(btnPregunta43Resp1, btnPregunta43Resp2, btnPregunta43Resp3, btnPregunta43Resp4, btnPregunta43Resp4, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0044": SeleccionarBotonRespuesta(btnPregunta44Resp1, btnPregunta44Resp2, btnPregunta44Resp3, btnPregunta44Resp4, btnPregunta44Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0045": SeleccionarBotonRespuesta(btnPregunta45Resp1, btnPregunta45Resp2, btnPregunta45Resp3, btnPregunta45Resp4, btnPregunta45Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0046": SeleccionarBotonRespuesta(btnPregunta46Resp1, btnPregunta46Resp2, btnPregunta46Resp3, btnPregunta46Resp4, btnPregunta46Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0047": SeleccionarBotonRespuesta(btnPregunta47Resp1, btnPregunta47Resp2, btnPregunta47Resp3, btnPregunta47Resp4, btnPregunta47Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0048": SeleccionarBotonRespuesta(btnPregunta48Resp1, btnPregunta48Resp2, btnPregunta48Resp3, btnPregunta48Resp4, btnPregunta48Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0049": SeleccionarBotonRespuesta(btnPregunta49Resp1, btnPregunta49Resp2, btnPregunta49Resp3, btnPregunta49Resp4, btnPregunta49Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0050": SeleccionarBotonRespuesta(btnPregunta50Resp1, btnPregunta50Resp2, btnPregunta50Resp3, btnPregunta50Resp4, btnPregunta50Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0051": SeleccionarBotonRespuesta(btnPregunta51Resp1, btnPregunta51Resp2, btnPregunta51Resp3, btnPregunta51Resp4, btnPregunta51Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0052": SeleccionarBotonRespuesta(btnPregunta52Resp1, btnPregunta52Resp2, btnPregunta52Resp3, btnPregunta52Resp4, btnPregunta52Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0053": SeleccionarBotonRespuesta(btnPregunta53Resp1, btnPregunta53Resp2, btnPregunta53Resp3, btnPregunta53Resp4, btnPregunta53Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0054": SeleccionarBotonRespuesta(btnPregunta54Resp1, btnPregunta54Resp2, btnPregunta54Resp3, btnPregunta54Resp4, btnPregunta54Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0055": SeleccionarBotonRespuesta(btnPregunta55Resp1, btnPregunta55Resp2, btnPregunta55Resp3, btnPregunta55Resp4, btnPregunta55Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0056": SeleccionarBotonRespuesta(btnPregunta56Resp1, btnPregunta56Resp2, btnPregunta56Resp3, btnPregunta56Resp4, btnPregunta56Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0057": SeleccionarBotonRespuesta(btnPregunta57Resp1, btnPregunta57Resp2, btnPregunta57Resp3, btnPregunta57Resp4, btnPregunta57Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0058": SeleccionarBotonRespuesta(btnPregunta58Resp1, btnPregunta58Resp2, btnPregunta58Resp3, btnPregunta58Resp4, btnPregunta58Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0059": SeleccionarBotonRespuesta(btnPregunta59Resp1, btnPregunta59Resp2, btnPregunta59Resp3, btnPregunta59Resp4, btnPregunta59Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0060": SeleccionarBotonRespuesta(btnPregunta60Resp1, btnPregunta60Resp2, btnPregunta60Resp3, btnPregunta60Resp4, btnPregunta60Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0061": SeleccionarBotonRespuesta(btnPregunta61Resp1, btnPregunta61Resp2, btnPregunta61Resp3, btnPregunta61Resp4, btnPregunta61Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0062": SeleccionarBotonRespuesta(btnPregunta62Resp1, btnPregunta62Resp2, btnPregunta62Resp3, btnPregunta62Resp4, btnPregunta62Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0063": SeleccionarBotonRespuesta(btnPregunta63Resp1, btnPregunta63Resp2, btnPregunta63Resp3, btnPregunta63Resp4, btnPregunta63Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0064": SeleccionarBotonRespuesta(btnPregunta64Resp1, btnPregunta64Resp2, btnPregunta64Resp3, btnPregunta64Resp4, btnPregunta64Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0065": SeleccionarBotonRespuesta(btnPregunta65Resp1, btnPregunta65Resp2, btnPregunta65Resp3, btnPregunta65Resp4, btnPregunta65Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0066": SeleccionarBotonRespuesta(btnPregunta66Resp1, btnPregunta66Resp2, btnPregunta66Resp3, btnPregunta66Resp4, btnPregunta66Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0067": SeleccionarBotonRespuesta(btnPregunta67Resp1, btnPregunta67Resp2, btnPregunta67Resp3, btnPregunta67Resp4, btnPregunta67Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0068": SeleccionarBotonRespuesta(btnPregunta68Resp1, btnPregunta68Resp2, btnPregunta68Resp3, btnPregunta68Resp4, btnPregunta68Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0069": SeleccionarBotonRespuesta(btnPregunta69Resp1, btnPregunta69Resp2, btnPregunta69Resp3, btnPregunta69Resp4, btnPregunta69Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0070": SeleccionarBotonRespuesta(btnPregunta70Resp1, btnPregunta70Resp2, btnPregunta70Resp3, btnPregunta70Resp4, btnPregunta70Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0071": SeleccionarBotonRespuesta(btnPregunta71Resp1, btnPregunta71Resp2, btnPregunta71Resp3, btnPregunta71Resp4, btnPregunta71Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0072": SeleccionarBotonRespuesta(btnPregunta72Resp1, btnPregunta72Resp2, btnPregunta72Resp3, btnPregunta72Resp4, btnPregunta72Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0073": SeleccionarBotonRespuesta(btnPregunta73Resp1, btnPregunta73Resp2, btnPregunta73Resp3, btnPregunta73Resp4, btnPregunta73Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0074": SeleccionarBotonRespuesta(btnPregunta74Resp1, btnPregunta74Resp2, btnPregunta74Resp3, btnPregunta74Resp4, btnPregunta74Resp5, resp.NB_RESULTADO); break;
                    case "APTITUD2-RES-0075": SeleccionarBotonRespuesta(btnPregunta75Resp1, btnPregunta75Resp2, btnPregunta75Resp3, btnPregunta75Resp4, btnPregunta75Resp5, resp.NB_RESULTADO); break;
                }
            }

        }

        public void asignarValores(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> respuestas)
        {
            foreach (SPE_OBTIENE_RESULTADO_PRUEBA_Result resp in respuestas)
            {
                switch (resp.CL_PREGUNTA)
                {
                    case "APTITUD2-A-0001": SeleccionarBotonRespuesta(btnPregunta1Resp1, btnPregunta1Resp2, btnPregunta1Resp3, btnPregunta1Resp4, btnPregunta1Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0002": SeleccionarBotonRespuesta(btnPregunta2Resp1, btnPregunta2Resp2, btnPregunta2Resp3, btnPregunta2Resp4, btnPregunta2Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0003": SeleccionarBotonRespuesta(btnPregunta3Resp1, btnPregunta3Resp2, btnPregunta3Resp3, btnPregunta3Resp4, btnPregunta3Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0004": SeleccionarBotonRespuesta(btnPregunta4Resp1, btnPregunta4Resp2, btnPregunta4Resp3, btnPregunta4Resp4, btnPregunta4Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0005": SeleccionarBotonRespuesta(btnPregunta5Resp1, btnPregunta5Resp2, btnPregunta5Resp3, btnPregunta5Resp4, btnPregunta5Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0006": SeleccionarBotonRespuesta(btnPregunta6Resp1, btnPregunta6Resp2, btnPregunta6Resp3, btnPregunta6Resp4, btnPregunta6Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0007": SeleccionarBotonRespuesta(btnPregunta7Resp1, btnPregunta7Resp2, btnPregunta7Resp3, btnPregunta7Resp4, btnPregunta7Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0008": SeleccionarBotonRespuesta(btnPregunta8Resp1, btnPregunta8Resp2, btnPregunta8Resp3, btnPregunta8Resp4, btnPregunta8Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0009": SeleccionarBotonRespuesta(btnPregunta9Resp1, btnPregunta9Resp2, btnPregunta9Resp3, btnPregunta9Resp4, btnPregunta9Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0010": SeleccionarBotonRespuesta(btnPregunta10Resp1, btnPregunta10Resp2, btnPregunta10Resp3, btnPregunta10Resp4, btnPregunta10Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0011": SeleccionarBotonRespuesta(btnPregunta11Resp1, btnPregunta11Resp2, btnPregunta11Resp3, btnPregunta11Resp4, btnPregunta11Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0012": SeleccionarBotonRespuesta(btnPregunta12Resp1, btnPregunta12Resp2, btnPregunta12Resp3, btnPregunta12Resp4, btnPregunta12Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0013": SeleccionarBotonRespuesta(btnPregunta13Resp1, btnPregunta13Resp2, btnPregunta13Resp3, btnPregunta13Resp4, btnPregunta13Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0014": SeleccionarBotonRespuesta(btnPregunta14Resp1, btnPregunta14Resp2, btnPregunta14Resp3, btnPregunta14Resp4, btnPregunta14Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0015": SeleccionarBotonRespuesta(btnPregunta15Resp1, btnPregunta15Resp2, btnPregunta15Resp3, btnPregunta15Resp4, btnPregunta15Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0016": SeleccionarBotonRespuesta(btnPregunta16Resp1, btnPregunta16Resp2, btnPregunta16Resp3, btnPregunta16Resp4, btnPregunta16Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0017": SeleccionarBotonRespuesta(btnPregunta17Resp1, btnPregunta17Resp2, btnPregunta17Resp3, btnPregunta17Resp4, btnPregunta17Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0018": SeleccionarBotonRespuesta(btnPregunta18Resp1, btnPregunta18Resp2, btnPregunta18Resp3, btnPregunta18Resp4, btnPregunta18Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0019": SeleccionarBotonRespuesta(btnPregunta19Resp1, btnPregunta19Resp2, btnPregunta19Resp3, btnPregunta19Resp4, btnPregunta19Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0020": SeleccionarBotonRespuesta(btnPregunta20Resp1, btnPregunta20Resp2, btnPregunta20Resp3, btnPregunta20Resp4, btnPregunta20Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0021": SeleccionarBotonRespuesta(btnPregunta21Resp1, btnPregunta21Resp2, btnPregunta21Resp3, btnPregunta21Resp4, btnPregunta21Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0022": SeleccionarBotonRespuesta(btnPregunta22Resp1, btnPregunta22Resp2, btnPregunta22Resp3, btnPregunta22Resp4, btnPregunta22Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0023": SeleccionarBotonRespuesta(btnPregunta23Resp1, btnPregunta23Resp2, btnPregunta23Resp3, btnPregunta23Resp4, btnPregunta23Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0024": SeleccionarBotonRespuesta(btnPregunta24Resp1, btnPregunta24Resp2, btnPregunta24Resp3, btnPregunta24Resp4, btnPregunta24Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0025": SeleccionarBotonRespuesta(btnPregunta25Resp1, btnPregunta25Resp2, btnPregunta25Resp3, btnPregunta25Resp4, btnPregunta25Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0026": SeleccionarBotonRespuesta(btnPregunta26Resp1, btnPregunta26Resp2, btnPregunta26Resp3, btnPregunta26Resp4, btnPregunta26Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0027": SeleccionarBotonRespuesta(btnPregunta27Resp1, btnPregunta27Resp2, btnPregunta27Resp3, btnPregunta27Resp4, btnPregunta27Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0028": SeleccionarBotonRespuesta(btnPregunta28Resp1, btnPregunta28Resp2, btnPregunta28Resp3, btnPregunta28Resp4, btnPregunta28Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0029": SeleccionarBotonRespuesta(btnPregunta29Resp1, btnPregunta29Resp2, btnPregunta29Resp3, btnPregunta29Resp4, btnPregunta29Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0030": SeleccionarBotonRespuesta(btnPregunta30Resp1, btnPregunta30Resp2, btnPregunta30Resp3, btnPregunta30Resp4, btnPregunta30Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0031": SeleccionarBotonRespuesta(btnPregunta31Resp1, btnPregunta31Resp2, btnPregunta31Resp3, btnPregunta31Resp4, btnPregunta31Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0032": SeleccionarBotonRespuesta(btnPregunta32Resp1, btnPregunta32Resp2, btnPregunta32Resp3, btnPregunta32Resp4, btnPregunta32Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0033": SeleccionarBotonRespuesta(btnPregunta33Resp1, btnPregunta33Resp2, btnPregunta33Resp3, btnPregunta33Resp4, btnPregunta33Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0034": SeleccionarBotonRespuesta(btnPregunta34Resp1, btnPregunta34Resp2, btnPregunta34Resp3, btnPregunta34Resp4, btnPregunta34Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0035": SeleccionarBotonRespuesta(btnPregunta35Resp1, btnPregunta35Resp2, btnPregunta35Resp3, btnPregunta35Resp4, btnPregunta35Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0036": SeleccionarBotonRespuesta(btnPregunta36Resp1, btnPregunta36Resp2, btnPregunta36Resp3, btnPregunta36Resp4, btnPregunta36Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0037": SeleccionarBotonRespuesta(btnPregunta37Resp1, btnPregunta37Resp2, btnPregunta37Resp3, btnPregunta37Resp4, btnPregunta37Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0038": SeleccionarBotonRespuesta(btnPregunta38Resp1, btnPregunta38Resp2, btnPregunta38Resp3, btnPregunta38Resp4, btnPregunta38Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0039": SeleccionarBotonRespuesta(btnPregunta39Resp1, btnPregunta39Resp2, btnPregunta39Resp3, btnPregunta39Resp4, btnPregunta39Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0040": SeleccionarBotonRespuesta(btnPregunta40Resp1, btnPregunta40Resp2, btnPregunta40Resp3, btnPregunta40Resp4, btnPregunta40Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0041": SeleccionarBotonRespuesta(btnPregunta41Resp1, btnPregunta41Resp2, btnPregunta41Resp3, btnPregunta41Resp4, btnPregunta41Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0042": SeleccionarBotonRespuesta(btnPregunta42Resp1, btnPregunta42Resp2, btnPregunta42Resp3, btnPregunta42Resp4, btnPregunta42Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0043": SeleccionarBotonRespuesta(btnPregunta43Resp1, btnPregunta43Resp2, btnPregunta43Resp3, btnPregunta43Resp4, btnPregunta43Resp4, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0044": SeleccionarBotonRespuesta(btnPregunta44Resp1, btnPregunta44Resp2, btnPregunta44Resp3, btnPregunta44Resp4, btnPregunta44Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0045": SeleccionarBotonRespuesta(btnPregunta45Resp1, btnPregunta45Resp2, btnPregunta45Resp3, btnPregunta45Resp4, btnPregunta45Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0046": SeleccionarBotonRespuesta(btnPregunta46Resp1, btnPregunta46Resp2, btnPregunta46Resp3, btnPregunta46Resp4, btnPregunta46Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0047": SeleccionarBotonRespuesta(btnPregunta47Resp1, btnPregunta47Resp2, btnPregunta47Resp3, btnPregunta47Resp4, btnPregunta47Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0048": SeleccionarBotonRespuesta(btnPregunta48Resp1, btnPregunta48Resp2, btnPregunta48Resp3, btnPregunta48Resp4, btnPregunta48Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0049": SeleccionarBotonRespuesta(btnPregunta49Resp1, btnPregunta49Resp2, btnPregunta49Resp3, btnPregunta49Resp4, btnPregunta49Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0050": SeleccionarBotonRespuesta(btnPregunta50Resp1, btnPregunta50Resp2, btnPregunta50Resp3, btnPregunta50Resp4, btnPregunta50Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0051": SeleccionarBotonRespuesta(btnPregunta51Resp1, btnPregunta51Resp2, btnPregunta51Resp3, btnPregunta51Resp4, btnPregunta51Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0052": SeleccionarBotonRespuesta(btnPregunta52Resp1, btnPregunta52Resp2, btnPregunta52Resp3, btnPregunta52Resp4, btnPregunta52Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0053": SeleccionarBotonRespuesta(btnPregunta53Resp1, btnPregunta53Resp2, btnPregunta53Resp3, btnPregunta53Resp4, btnPregunta53Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0054": SeleccionarBotonRespuesta(btnPregunta54Resp1, btnPregunta54Resp2, btnPregunta54Resp3, btnPregunta54Resp4, btnPregunta54Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0055": SeleccionarBotonRespuesta(btnPregunta55Resp1, btnPregunta55Resp2, btnPregunta55Resp3, btnPregunta55Resp4, btnPregunta55Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0056": SeleccionarBotonRespuesta(btnPregunta56Resp1, btnPregunta56Resp2, btnPregunta56Resp3, btnPregunta56Resp4, btnPregunta56Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0057": SeleccionarBotonRespuesta(btnPregunta57Resp1, btnPregunta57Resp2, btnPregunta57Resp3, btnPregunta57Resp4, btnPregunta57Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0058": SeleccionarBotonRespuesta(btnPregunta58Resp1, btnPregunta58Resp2, btnPregunta58Resp3, btnPregunta58Resp4, btnPregunta58Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0059": SeleccionarBotonRespuesta(btnPregunta59Resp1, btnPregunta59Resp2, btnPregunta59Resp3, btnPregunta59Resp4, btnPregunta59Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0060": SeleccionarBotonRespuesta(btnPregunta60Resp1, btnPregunta60Resp2, btnPregunta60Resp3, btnPregunta60Resp4, btnPregunta60Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0061": SeleccionarBotonRespuesta(btnPregunta61Resp1, btnPregunta61Resp2, btnPregunta61Resp3, btnPregunta61Resp4, btnPregunta61Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0062": SeleccionarBotonRespuesta(btnPregunta62Resp1, btnPregunta62Resp2, btnPregunta62Resp3, btnPregunta62Resp4, btnPregunta62Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0063": SeleccionarBotonRespuesta(btnPregunta63Resp1, btnPregunta63Resp2, btnPregunta63Resp3, btnPregunta63Resp4, btnPregunta63Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0064": SeleccionarBotonRespuesta(btnPregunta64Resp1, btnPregunta64Resp2, btnPregunta64Resp3, btnPregunta64Resp4, btnPregunta64Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0065": SeleccionarBotonRespuesta(btnPregunta65Resp1, btnPregunta65Resp2, btnPregunta65Resp3, btnPregunta65Resp4, btnPregunta65Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0066": SeleccionarBotonRespuesta(btnPregunta66Resp1, btnPregunta66Resp2, btnPregunta66Resp3, btnPregunta66Resp4, btnPregunta66Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0067": SeleccionarBotonRespuesta(btnPregunta67Resp1, btnPregunta67Resp2, btnPregunta67Resp3, btnPregunta67Resp4, btnPregunta67Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0068": SeleccionarBotonRespuesta(btnPregunta68Resp1, btnPregunta68Resp2, btnPregunta68Resp3, btnPregunta68Resp4, btnPregunta68Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0069": SeleccionarBotonRespuesta(btnPregunta69Resp1, btnPregunta69Resp2, btnPregunta69Resp3, btnPregunta69Resp4, btnPregunta69Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0070": SeleccionarBotonRespuesta(btnPregunta70Resp1, btnPregunta70Resp2, btnPregunta70Resp3, btnPregunta70Resp4, btnPregunta70Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0071": SeleccionarBotonRespuesta(btnPregunta71Resp1, btnPregunta71Resp2, btnPregunta71Resp3, btnPregunta71Resp4, btnPregunta71Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0072": SeleccionarBotonRespuesta(btnPregunta72Resp1, btnPregunta72Resp2, btnPregunta72Resp3, btnPregunta72Resp4, btnPregunta72Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0073": SeleccionarBotonRespuesta(btnPregunta73Resp1, btnPregunta73Resp2, btnPregunta73Resp3, btnPregunta73Resp4, btnPregunta73Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0074": SeleccionarBotonRespuesta(btnPregunta74Resp1, btnPregunta74Resp2, btnPregunta74Resp3, btnPregunta74Resp4, btnPregunta74Resp5, resp.NB_RESPUESTA); break;
                    case "APTITUD2-A-0075": SeleccionarBotonRespuesta(btnPregunta75Resp1, btnPregunta75Resp2, btnPregunta75Resp3, btnPregunta75Resp4, btnPregunta75Resp5, resp.NB_RESPUESTA); break;
                }
            }
        }

        public void SeleccionarBotonRespuesta(RadButton a, RadButton b, RadButton c, RadButton d, RadButton e, string pAnswer)
        {
            switch (pAnswer) 
            {
                case "a": a.Checked = true; break;
                case "b": b.Checked = true; break;
                case "c": c.Checked = true; break;
                case "d": d.Checked = true; break;
                case "e": e.Checked = true; break;
                case "1": a.Checked = true; break;
                case "2": b.Checked = true; break;
                case "3": c.Checked = true; break;
                case "4": d.Checked = true; break;
                case "5": e.Checked = true; break;
                default: break;
            }
        }

        protected void btnCorregir_Click(object sender, EventArgs e)
        {
            PruebasNegocio nKprueba = new PruebasNegocio();
            SPE_OBTIENE_K_PRUEBA_Result vPruebaTerminada = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();
            E_RESULTADO vResultado = nKprueba.CorrigePrueba(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            if (vResultado.CL_TIPO_ERROR != E_TIPO_RESPUESTA_DB.SUCCESSFUL )
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
    }
}