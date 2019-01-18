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
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.IDP
{
    public partial class VentanaPersonalidadLaboral2 : System.Web.UI.Page
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
            get { return (bool)ViewState["vsMostrarCronometroPL2"]; }
            set { ViewState["vsMostrarCronometroPL2"] = value; }
        }

        public int vIdBateria
        {
            get { return (int)ViewState["vsIdBateria"]; }
            set { ViewState["vsIdBateria"] = value; }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = (ContextoUsuario.oUsuario != null ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO");
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {

                if (Request.QueryString["MOD"] != null)
                {
                    vTipoRevision = Request.QueryString["MOD"];
                }
                if (Request.QueryString["ID"] != null)
                {
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
                    else
                        if (vTipoRevision == "EDIT")
                        {
                            //btnEliminar.Visible = true;// Se agrega para la nueva forma de navegación 06/06/2018
                            cronometro.Visible = false;
                            vTiempoPrueba = 0;
                            btnTerminar.Visible = false;
                            btnCorregir.Visible = true;
                            btnImpresionPrueba.Visible = true; // Se agrega para imprimir en la nueva navegación IDP 06/06/2018
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
                        //else
                        //    if (vTipoRevision == "EDIT")
                        //    {
                        //        cronometro.Visible = false;
                        //        vTiempoPrueba = 0;
                        //        btnTerminar.Visible = false;
                        //        btnCorregir.Visible = true;
                        //        //obtener respuestas
                        //        var respuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);
                        //        asignarValores(respuestas);
                        //    }
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

            String LABORAL2_A_0001 = radTxtPreg1Resp1.Text;
            GuardarResultado("LABORAL2-A-0001", LABORAL2_A_0001);
            String LABORAL2_A_0002 = radTxtPreg1Resp2.Text;
            GuardarResultado("LABORAL2-A-0002", LABORAL2_A_0002);
            String LABORAL2_A_0003 = radTxtPreg1Resp3.Text;
            GuardarResultado("LABORAL2-A-0003", LABORAL2_A_0003);
            String LABORAL2_A_0004 = radTxtPreg1Resp4.Text;
            GuardarResultado("LABORAL2-A-0004", LABORAL2_A_0004);

            String LABORAL2_A_0005 = radTxtPreg2Resp1.Text;
            GuardarResultado("LABORAL2-A-0005", LABORAL2_A_0005);
            String LABORAL2_A_0006 = radTxtPreg2Resp2.Text;
            GuardarResultado("LABORAL2-A-0006", LABORAL2_A_0006);
            String LABORAL2_A_0007 = radTxtPreg2Resp3.Text;
            GuardarResultado("LABORAL2-A-0007", LABORAL2_A_0007);
            String LABORAL2_A_0008 = radTxtPreg2Resp4.Text;
            GuardarResultado("LABORAL2-A-0008", LABORAL2_A_0008);

            String LABORAL2_A_0009 = radTxtPreg3Resp1.Text;
            GuardarResultado("LABORAL2-A-0009", LABORAL2_A_0009);
            String LABORAL2_A_0010 = radTxtPreg3Resp2.Text;
            GuardarResultado("LABORAL2-A-0010", LABORAL2_A_0010);
            String LABORAL2_A_0011 = radTxtPreg3Resp3.Text;
            GuardarResultado("LABORAL2-A-0011", LABORAL2_A_0011);
            String LABORAL2_A_0012 = radTxtPreg3Resp4.Text;
            GuardarResultado("LABORAL2-A-0012", LABORAL2_A_0012);


            String LABORAL2_A_0013 = radTxtPreg4Resp1.Text;
            GuardarResultado("LABORAL2-A-0013", LABORAL2_A_0013);
            String LABORAL2_A_0014 = radTxtPreg4Resp2.Text;
            GuardarResultado("LABORAL2-A-0014", LABORAL2_A_0014);
            String LABORAL2_A_0015 = radTxtPreg4Resp3.Text;
            GuardarResultado("LABORAL2-A-0015", LABORAL2_A_0015);
            String LABORAL2_A_0016 = radTxtPreg4Resp4.Text;
            GuardarResultado("LABORAL2-A-0016", LABORAL2_A_0016);

            String LABORAL2_A_0017 = radTxtPreg5Resp1.Text;
            GuardarResultado("LABORAL2-A-0017", LABORAL2_A_0017);
            String LABORAL2_A_0018 = radTxtPreg5Resp2.Text;
            GuardarResultado("LABORAL2-A-0018", LABORAL2_A_0018);
            String LABORAL2_A_0019 = radTxtPreg5Resp3.Text;
            GuardarResultado("LABORAL2-A-0019", LABORAL2_A_0019);
            String LABORAL2_A_0020 = radTxtPreg5Resp4.Text;
            GuardarResultado("LABORAL2-A-0020", LABORAL2_A_0020);

            String LABORAL2_A_0021 = radTxtPreg6Resp1.Text;
            GuardarResultado("LABORAL2-A-0021", LABORAL2_A_0021);
            String LABORAL2_A_0022 = radTxtPreg6Resp2.Text;
            GuardarResultado("LABORAL2-A-0022", LABORAL2_A_0022);
            String LABORAL2_A_0023 = radTxtPreg6Resp3.Text;
            GuardarResultado("LABORAL2-A-0023", LABORAL2_A_0023);
            String LABORAL2_A_0024 = radTxtPreg6Resp4.Text;
            GuardarResultado("LABORAL2-A-0024", LABORAL2_A_0024);


            //////////////////////
            String LABORAL2_A_0025 = radTxtPreg7Resp1.Text;
            GuardarResultado("LABORAL2-A-0025", LABORAL2_A_0025);
            String LABORAL2_A_0026 = radTxtPreg7Resp2.Text;
            GuardarResultado("LABORAL2-A-0026", LABORAL2_A_0026);
            String LABORAL2_A_0027 = radTxtPreg7Resp3.Text;
            GuardarResultado("LABORAL2-A-0027", LABORAL2_A_0027);
            String LABORAL2_A_0028 = radTxtPreg7Resp4.Text;
            GuardarResultado("LABORAL2-A-0028", LABORAL2_A_0028);

            String LABORAL2_A_0029 = radTxtPreg8Resp1.Text;
            GuardarResultado("LABORAL2-A-0029", LABORAL2_A_0029);
            String LABORAL2_A_0030 = radTxtPreg8Resp2.Text;
            GuardarResultado("LABORAL2-A-0030", LABORAL2_A_0030);
            String LABORAL2_A_0031 = radTxtPreg8Resp3.Text;
            GuardarResultado("LABORAL2-A-0031", LABORAL2_A_0031);
            String LABORAL2_A_0032 = radTxtPreg8Resp4.Text;
            GuardarResultado("LABORAL2-A-0032", LABORAL2_A_0032);

            String LABORAL2_A_0033 = radTxtPreg9Resp1.Text;
            GuardarResultado("LABORAL2-A-0033", LABORAL2_A_0033);
            String LABORAL2_A_0034 = radTxtPreg9Resp2.Text;
            GuardarResultado("LABORAL2-A-0034", LABORAL2_A_0034);
            String LABORAL2_A_0035 = radTxtPreg9Resp3.Text;
            GuardarResultado("LABORAL2-A-0035", LABORAL2_A_0035);
            String LABORAL2_A_0036 = radTxtPreg9Resp4.Text;
            GuardarResultado("LABORAL2-A-0036", LABORAL2_A_0036);

            String LABORAL2_A_0037 = radTxtPreg10Resp1.Text;
            GuardarResultado("LABORAL2-A-0037", LABORAL2_A_0037);
            String LABORAL2_A_0038 = radTxtPreg10Resp2.Text;
            GuardarResultado("LABORAL2-A-0038", LABORAL2_A_0038);
            String LABORAL2_A_0039 = radTxtPreg10Resp3.Text;
            GuardarResultado("LABORAL2-A-0039", LABORAL2_A_0039);
            String LABORAL2_A_0040 = radTxtPreg10Resp4.Text;
            GuardarResultado("LABORAL2-A-0040", LABORAL2_A_0040);

            String LABORAL2_A_0041 = radTxtPreg11Resp1.Text;
            GuardarResultado("LABORAL2-A-0041", LABORAL2_A_0041);
            String LABORAL2_A_0042 = radTxtPreg11Resp2.Text;
            GuardarResultado("LABORAL2-A-0042", LABORAL2_A_0042);
            String LABORAL2_A_0043 = radTxtPreg11Resp3.Text;
            GuardarResultado("LABORAL2-A-0043", LABORAL2_A_0043);
            String LABORAL2_A_0044 = radTxtPreg11Resp4.Text;
            GuardarResultado("LABORAL2-A-0044", LABORAL2_A_0044);

            String LABORAL2_A_0045 = radTxtPreg12Resp1.Text;
            GuardarResultado("LABORAL2-A-0045", LABORAL2_A_0045);
            String LABORAL2_A_0046 = radTxtPreg12Resp2.Text;
            GuardarResultado("LABORAL2-A-0046", LABORAL2_A_0046);
            String LABORAL2_A_0047 = radTxtPreg12Resp3.Text;
            GuardarResultado("LABORAL2-A-0047", LABORAL2_A_0047);
            String LABORAL2_A_0048 = radTxtPreg12Resp4.Text;
            GuardarResultado("LABORAL2-A-0048", LABORAL2_A_0048);

            //////////////////////////////////////////////////////

            String LABORAL2_A_0049 = radTxtPreg13Resp1.Text;
            GuardarResultado("LABORAL2-A-0049", LABORAL2_A_0049);
            String LABORAL2_A_0050 = radTxtPreg13Resp2.Text;
            GuardarResultado("LABORAL2-A-0050", LABORAL2_A_0050);
            String LABORAL2_A_0051 = radTxtPreg13Resp3.Text;
            GuardarResultado("LABORAL2-A-0051", LABORAL2_A_0051);
            String LABORAL2_A_0052 = radTxtPreg13Resp4.Text;
            GuardarResultado("LABORAL2-A-0052", LABORAL2_A_0052);

            String LABORAL2_A_0053 = radTxtPreg14Resp1.Text;
            GuardarResultado("LABORAL2-A-0053", LABORAL2_A_0053);
            String LABORAL2_A_0054 = radTxtPreg14Resp2.Text;
            GuardarResultado("LABORAL2-A-0054", LABORAL2_A_0054);
            String LABORAL2_A_0055 = radTxtPreg14Resp3.Text;
            GuardarResultado("LABORAL2-A-0055", LABORAL2_A_0055);
            String LABORAL2_A_0056 = radTxtPreg14Resp4.Text;
            GuardarResultado("LABORAL2-A-0056", LABORAL2_A_0056);

            String LABORAL2_A_0057 = radTxtPreg15Resp1.Text;
            GuardarResultado("LABORAL2-A-0057", LABORAL2_A_0057);
            String LABORAL2_A_0058 = radTxtPreg15Resp2.Text;
            GuardarResultado("LABORAL2-A-0058", LABORAL2_A_0058);
            String LABORAL2_A_0059 = radTxtPreg15Resp3.Text;
            GuardarResultado("LABORAL2-A-0059", LABORAL2_A_0059);
            String LABORAL2_A_0060 = radTxtPreg15Resp4.Text;
            GuardarResultado("LABORAL2-A-0060", LABORAL2_A_0060);

            String LABORAL2_A_0061 = radTxtPreg16Resp1.Text;
            GuardarResultado("LABORAL2-A-0061", LABORAL2_A_0061);
            String LABORAL2_A_0062 = radTxtPreg16Resp2.Text;
            GuardarResultado("LABORAL2-A-0062", LABORAL2_A_0062);
            String LABORAL2_A_0063 = radTxtPreg16Resp3.Text;
            GuardarResultado("LABORAL2-A-0063", LABORAL2_A_0063);
            String LABORAL2_A_0064 = radTxtPreg16Resp4.Text;
            GuardarResultado("LABORAL2-A-0064", LABORAL2_A_0064);

            String LABORAL2_A_0065 = radTxtPreg17Resp1.Text;
            GuardarResultado("LABORAL2-A-0065", LABORAL2_A_0065);
            String LABORAL2_A_0066 = radTxtPreg17Resp2.Text;
            GuardarResultado("LABORAL2-A-0066", LABORAL2_A_0066);
            String LABORAL2_A_0067 = radTxtPreg17Resp3.Text;
            GuardarResultado("LABORAL2-A-0067", LABORAL2_A_0067);
            String LABORAL2_A_0068 = radTxtPreg17Resp4.Text;
            GuardarResultado("LABORAL2-A-0068", LABORAL2_A_0068);

            String LABORAL2_A_0069 = radTxtPreg18Resp1.Text;
            GuardarResultado("LABORAL2-A-0069", LABORAL2_A_0069);
            String LABORAL2_A_0070 = radTxtPreg18Resp2.Text;
            GuardarResultado("LABORAL2-A-0070", LABORAL2_A_0070);
            String LABORAL2_A_0071 = radTxtPreg18Resp3.Text;
            GuardarResultado("LABORAL2-A-0071", LABORAL2_A_0071);
            String LABORAL2_A_0072 = radTxtPreg18Resp4.Text;
            GuardarResultado("LABORAL2-A-0072", LABORAL2_A_0072);

            ///////////////////////////////////

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
                E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vObjetoPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(),pNbPrueba: "LABORAL2", usuario: vClUsuario, programa: vNbPrograma);
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

            String LABORAL2_A_0001 = radTxtPreg1Resp1.Text;
            GuardarResultado("LABORAL2-A-0001", LABORAL2_A_0001);
            String LABORAL2_A_0002 = radTxtPreg1Resp2.Text;
            GuardarResultado("LABORAL2-A-0002", LABORAL2_A_0002);
            String LABORAL2_A_0003 = radTxtPreg1Resp3.Text;
            GuardarResultado("LABORAL2-A-0003", LABORAL2_A_0003);
            String LABORAL2_A_0004 = radTxtPreg1Resp4.Text;
            GuardarResultado("LABORAL2-A-0004", LABORAL2_A_0004);

            String LABORAL2_A_0005 = radTxtPreg2Resp1.Text;
            GuardarResultado("LABORAL2-A-0005", LABORAL2_A_0005);
            String LABORAL2_A_0006 = radTxtPreg2Resp2.Text;
            GuardarResultado("LABORAL2-A-0006", LABORAL2_A_0006);
            String LABORAL2_A_0007 = radTxtPreg2Resp3.Text;
            GuardarResultado("LABORAL2-A-0007", LABORAL2_A_0007);
            String LABORAL2_A_0008 = radTxtPreg2Resp4.Text;
            GuardarResultado("LABORAL2-A-0008", LABORAL2_A_0008);

            String LABORAL2_A_0009 = radTxtPreg3Resp1.Text;
            GuardarResultado("LABORAL2-A-0009", LABORAL2_A_0009);
            String LABORAL2_A_0010 = radTxtPreg3Resp2.Text;
            GuardarResultado("LABORAL2-A-0010", LABORAL2_A_0010);
            String LABORAL2_A_0011 = radTxtPreg3Resp3.Text;
            GuardarResultado("LABORAL2-A-0011", LABORAL2_A_0011);
            String LABORAL2_A_0012 = radTxtPreg3Resp4.Text;
            GuardarResultado("LABORAL2-A-0012", LABORAL2_A_0012);


            String LABORAL2_A_0013 = radTxtPreg4Resp1.Text;
            GuardarResultado("LABORAL2-A-0013", LABORAL2_A_0013);
            String LABORAL2_A_0014 = radTxtPreg4Resp2.Text;
            GuardarResultado("LABORAL2-A-0014", LABORAL2_A_0014);
            String LABORAL2_A_0015 = radTxtPreg4Resp3.Text;
            GuardarResultado("LABORAL2-A-0015", LABORAL2_A_0015);
            String LABORAL2_A_0016 = radTxtPreg4Resp4.Text;
            GuardarResultado("LABORAL2-A-0016", LABORAL2_A_0016);

            String LABORAL2_A_0017 = radTxtPreg5Resp1.Text;
            GuardarResultado("LABORAL2-A-0017", LABORAL2_A_0017);
            String LABORAL2_A_0018 = radTxtPreg5Resp2.Text;
            GuardarResultado("LABORAL2-A-0018", LABORAL2_A_0018);
            String LABORAL2_A_0019 = radTxtPreg5Resp3.Text;
            GuardarResultado("LABORAL2-A-0019", LABORAL2_A_0019);
            String LABORAL2_A_0020 = radTxtPreg5Resp4.Text;
            GuardarResultado("LABORAL2-A-0020", LABORAL2_A_0020);

            String LABORAL2_A_0021 = radTxtPreg6Resp1.Text;
            GuardarResultado("LABORAL2-A-0021", LABORAL2_A_0021);
            String LABORAL2_A_0022 = radTxtPreg6Resp2.Text;
            GuardarResultado("LABORAL2-A-0022", LABORAL2_A_0022);
            String LABORAL2_A_0023 = radTxtPreg6Resp3.Text;
            GuardarResultado("LABORAL2-A-0023", LABORAL2_A_0023);
            String LABORAL2_A_0024 = radTxtPreg6Resp4.Text;
            GuardarResultado("LABORAL2-A-0024", LABORAL2_A_0024);


            //////////////////////
            String LABORAL2_A_0025 = radTxtPreg7Resp1.Text;
            GuardarResultado("LABORAL2-A-0025", LABORAL2_A_0025);
            String LABORAL2_A_0026 = radTxtPreg7Resp2.Text;
            GuardarResultado("LABORAL2-A-0026", LABORAL2_A_0026);
            String LABORAL2_A_0027 = radTxtPreg7Resp3.Text;
            GuardarResultado("LABORAL2-A-0027", LABORAL2_A_0027);
            String LABORAL2_A_0028 = radTxtPreg7Resp4.Text;
            GuardarResultado("LABORAL2-A-0028", LABORAL2_A_0028);

            String LABORAL2_A_0029 = radTxtPreg8Resp1.Text;
            GuardarResultado("LABORAL2-A-0029", LABORAL2_A_0029);
            String LABORAL2_A_0030 = radTxtPreg8Resp2.Text;
            GuardarResultado("LABORAL2-A-0030", LABORAL2_A_0030);
            String LABORAL2_A_0031 = radTxtPreg8Resp3.Text;
            GuardarResultado("LABORAL2-A-0031", LABORAL2_A_0031);
            String LABORAL2_A_0032 = radTxtPreg8Resp4.Text;
            GuardarResultado("LABORAL2-A-0032", LABORAL2_A_0032);

            String LABORAL2_A_0033 = radTxtPreg9Resp1.Text;
            GuardarResultado("LABORAL2-A-0033", LABORAL2_A_0033);
            String LABORAL2_A_0034 = radTxtPreg9Resp2.Text;
            GuardarResultado("LABORAL2-A-0034", LABORAL2_A_0034);
            String LABORAL2_A_0035 = radTxtPreg9Resp3.Text;
            GuardarResultado("LABORAL2-A-0035", LABORAL2_A_0035);
            String LABORAL2_A_0036 = radTxtPreg9Resp4.Text;
            GuardarResultado("LABORAL2-A-0036", LABORAL2_A_0036);

            String LABORAL2_A_0037 = radTxtPreg10Resp1.Text;
            GuardarResultado("LABORAL2-A-0037", LABORAL2_A_0037);
            String LABORAL2_A_0038 = radTxtPreg10Resp2.Text;
            GuardarResultado("LABORAL2-A-0038", LABORAL2_A_0038);
            String LABORAL2_A_0039 = radTxtPreg10Resp3.Text;
            GuardarResultado("LABORAL2-A-0039", LABORAL2_A_0039);
            String LABORAL2_A_0040 = radTxtPreg10Resp4.Text;
            GuardarResultado("LABORAL2-A-0040", LABORAL2_A_0040);

            String LABORAL2_A_0041 = radTxtPreg11Resp1.Text;
            GuardarResultado("LABORAL2-A-0041", LABORAL2_A_0041);
            String LABORAL2_A_0042 = radTxtPreg11Resp2.Text;
            GuardarResultado("LABORAL2-A-0042", LABORAL2_A_0042);
            String LABORAL2_A_0043 = radTxtPreg11Resp3.Text;
            GuardarResultado("LABORAL2-A-0043", LABORAL2_A_0043);
            String LABORAL2_A_0044 = radTxtPreg11Resp4.Text;
            GuardarResultado("LABORAL2-A-0044", LABORAL2_A_0044);

            String LABORAL2_A_0045 = radTxtPreg12Resp1.Text;
            GuardarResultado("LABORAL2-A-0045", LABORAL2_A_0045);
            String LABORAL2_A_0046 = radTxtPreg12Resp2.Text;
            GuardarResultado("LABORAL2-A-0046", LABORAL2_A_0046);
            String LABORAL2_A_0047 = radTxtPreg12Resp3.Text;
            GuardarResultado("LABORAL2-A-0047", LABORAL2_A_0047);
            String LABORAL2_A_0048 = radTxtPreg12Resp4.Text;
            GuardarResultado("LABORAL2-A-0048", LABORAL2_A_0048);

            //////////////////////////////////////////////////////

            String LABORAL2_A_0049 = radTxtPreg13Resp1.Text;
            GuardarResultado("LABORAL2-A-0049", LABORAL2_A_0049);
            String LABORAL2_A_0050 = radTxtPreg13Resp2.Text;
            GuardarResultado("LABORAL2-A-0050", LABORAL2_A_0050);
            String LABORAL2_A_0051 = radTxtPreg13Resp3.Text;
            GuardarResultado("LABORAL2-A-0051", LABORAL2_A_0051);
            String LABORAL2_A_0052 = radTxtPreg13Resp4.Text;
            GuardarResultado("LABORAL2-A-0052", LABORAL2_A_0052);

            String LABORAL2_A_0053 = radTxtPreg14Resp1.Text;
            GuardarResultado("LABORAL2-A-0053", LABORAL2_A_0053);
            String LABORAL2_A_0054 = radTxtPreg14Resp2.Text;
            GuardarResultado("LABORAL2-A-0054", LABORAL2_A_0054);
            String LABORAL2_A_0055 = radTxtPreg14Resp3.Text;
            GuardarResultado("LABORAL2-A-0055", LABORAL2_A_0055);
            String LABORAL2_A_0056 = radTxtPreg14Resp4.Text;
            GuardarResultado("LABORAL2-A-0056", LABORAL2_A_0056);

            String LABORAL2_A_0057 = radTxtPreg15Resp1.Text;
            GuardarResultado("LABORAL2-A-0057", LABORAL2_A_0057);
            String LABORAL2_A_0058 = radTxtPreg15Resp2.Text;
            GuardarResultado("LABORAL2-A-0058", LABORAL2_A_0058);
            String LABORAL2_A_0059 = radTxtPreg15Resp3.Text;
            GuardarResultado("LABORAL2-A-0059", LABORAL2_A_0059);
            String LABORAL2_A_0060 = radTxtPreg15Resp4.Text;
            GuardarResultado("LABORAL2-A-0060", LABORAL2_A_0060);

            String LABORAL2_A_0061 = radTxtPreg16Resp1.Text;
            GuardarResultado("LABORAL2-A-0061", LABORAL2_A_0061);
            String LABORAL2_A_0062 = radTxtPreg16Resp2.Text;
            GuardarResultado("LABORAL2-A-0062", LABORAL2_A_0062);
            String LABORAL2_A_0063 = radTxtPreg16Resp3.Text;
            GuardarResultado("LABORAL2-A-0063", LABORAL2_A_0063);
            String LABORAL2_A_0064 = radTxtPreg16Resp4.Text;
            GuardarResultado("LABORAL2-A-0064", LABORAL2_A_0064);

            String LABORAL2_A_0065 = radTxtPreg17Resp1.Text;
            GuardarResultado("LABORAL2-A-0065", LABORAL2_A_0065);
            String LABORAL2_A_0066 = radTxtPreg17Resp2.Text;
            GuardarResultado("LABORAL2-A-0066", LABORAL2_A_0066);
            String LABORAL2_A_0067 = radTxtPreg17Resp3.Text;
            GuardarResultado("LABORAL2-A-0067", LABORAL2_A_0067);
            String LABORAL2_A_0068 = radTxtPreg17Resp4.Text;
            GuardarResultado("LABORAL2-A-0068", LABORAL2_A_0068);

            String LABORAL2_A_0069 = radTxtPreg18Resp1.Text;
            GuardarResultado("LABORAL2-A-0069", LABORAL2_A_0069);
            String LABORAL2_A_0070 = radTxtPreg18Resp2.Text;
            GuardarResultado("LABORAL2-A-0070", LABORAL2_A_0070);
            String LABORAL2_A_0071 = radTxtPreg18Resp3.Text;
            GuardarResultado("LABORAL2-A-0071", LABORAL2_A_0071);
            String LABORAL2_A_0072 = radTxtPreg18Resp4.Text;
            GuardarResultado("LABORAL2-A-0072", LABORAL2_A_0072);

            ///////////////////////////////////

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
                E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vObjetoPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: "LABORAL2", usuario: vClUsuario, programa: vNbPrograma);
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
        public void GuardarResultado(string pclPregunta, string pnbRespuesta)
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
                    case "LABORAL2-A-0001": radTxtPreg1Resp1.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0002": radTxtPreg1Resp2.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0003": radTxtPreg1Resp3.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0004": radTxtPreg1Resp4.Text = resp.NB_RESPUESTA; break;

                    case "LABORAL2-A-0005": radTxtPreg2Resp1.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0006": radTxtPreg2Resp2.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0007": radTxtPreg2Resp3.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0008": radTxtPreg2Resp4.Text = resp.NB_RESPUESTA; break;

                    case "LABORAL2-A-0009": radTxtPreg3Resp1.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0010": radTxtPreg3Resp2.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0011": radTxtPreg3Resp3.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0012": radTxtPreg3Resp4.Text = resp.NB_RESPUESTA; break;
                    
                    case "LABORAL2-A-0013": radTxtPreg4Resp1.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0014": radTxtPreg4Resp2.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0015": radTxtPreg4Resp3.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0016": radTxtPreg4Resp4.Text = resp.NB_RESPUESTA; break;
                    
                    case "LABORAL2-A-0017": radTxtPreg5Resp1.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0018": radTxtPreg5Resp2.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0019": radTxtPreg5Resp3.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0020": radTxtPreg5Resp4.Text = resp.NB_RESPUESTA; break;
                    
                    case "LABORAL2-A-0021": radTxtPreg6Resp1.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0022": radTxtPreg6Resp2.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0023": radTxtPreg6Resp3.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0024": radTxtPreg6Resp4.Text = resp.NB_RESPUESTA; break;
                    
                    case "LABORAL2-A-0025": radTxtPreg7Resp1.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0026": radTxtPreg7Resp2.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0027": radTxtPreg7Resp3.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0028": radTxtPreg7Resp4.Text = resp.NB_RESPUESTA; break;
                    
                    case "LABORAL2-A-0029": radTxtPreg8Resp1.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0030": radTxtPreg8Resp2.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0031": radTxtPreg8Resp3.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0032": radTxtPreg8Resp4.Text = resp.NB_RESPUESTA; break;
                    
                    case "LABORAL2-A-0033": radTxtPreg9Resp1.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0034": radTxtPreg9Resp2.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0035": radTxtPreg9Resp3.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0036": radTxtPreg9Resp4.Text = resp.NB_RESPUESTA; break;
                    
                    case "LABORAL2-A-0037": radTxtPreg10Resp1.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0038": radTxtPreg10Resp2.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0039": radTxtPreg10Resp3.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0040": radTxtPreg10Resp4.Text = resp.NB_RESPUESTA; break;
                    
                    case "LABORAL2-A-0041": radTxtPreg11Resp1.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0042": radTxtPreg11Resp2.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0043": radTxtPreg11Resp3.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0044": radTxtPreg11Resp4.Text = resp.NB_RESPUESTA; break;
                    
                    case "LABORAL2-A-0045": radTxtPreg12Resp1.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0046": radTxtPreg12Resp2.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0047": radTxtPreg12Resp3.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0048": radTxtPreg12Resp4.Text = resp.NB_RESPUESTA; break;
                    
                    case "LABORAL2-A-0049": radTxtPreg13Resp1.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0050": radTxtPreg13Resp2.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0051": radTxtPreg13Resp3.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0052": radTxtPreg13Resp4.Text = resp.NB_RESPUESTA; break;
                    
                    case "LABORAL2-A-0053": radTxtPreg14Resp1.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0054": radTxtPreg14Resp2.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0055": radTxtPreg14Resp3.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0056": radTxtPreg14Resp4.Text = resp.NB_RESPUESTA; break;
                    
                    case "LABORAL2-A-0057": radTxtPreg15Resp1.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0058": radTxtPreg15Resp2.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0059": radTxtPreg15Resp3.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0060": radTxtPreg15Resp4.Text = resp.NB_RESPUESTA; break;
                    
                    case "LABORAL2-A-0061": radTxtPreg16Resp1.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0062": radTxtPreg16Resp2.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0063": radTxtPreg16Resp3.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0064": radTxtPreg16Resp4.Text = resp.NB_RESPUESTA; break;
                    
                    case "LABORAL2-A-0065": radTxtPreg17Resp1.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0066": radTxtPreg17Resp2.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0067": radTxtPreg17Resp3.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0068": radTxtPreg17Resp4.Text = resp.NB_RESPUESTA; break;
                    
                    case "LABORAL2-A-0069": radTxtPreg18Resp1.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0070": radTxtPreg18Resp2.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0071": radTxtPreg18Resp3.Text = resp.NB_RESPUESTA; break;
                    case "LABORAL2-A-0072": radTxtPreg18Resp4.Text = resp.NB_RESPUESTA; break;
                   }
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