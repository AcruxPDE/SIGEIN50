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
    public partial class VentanaTIVA : System.Web.UI.Page
    {
        #region Propiedades
        public int vTiempoPrueba
        {
            get { return (int)ViewState["vsAptitudMental1seconds"]; }
            set { ViewState["vsAptitudMental1seconds"] = value; }
        }

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

        public string vTipoRevision
        {
            get { return (string)ViewState["vsTipoRevision"]; }
            set { ViewState["vsTipoRevision"] = value; }
        }

        public bool MostrarCronometro
        {
            get { return (bool)ViewState["vsMostrarCronometroTIVA"]; }
            set { ViewState["vsMostrarCronometroTIVA"] = value; }
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
                if (Request.QueryString["ID"] != null)
                {
                    if (Request.QueryString["MOD"] != null)
                    {
                        vTipoRevision = Request.QueryString["MOD"];
                    }
                    MostrarCronometro = ContextoApp.IDP.ConfiguracionPsicometria.FgMostrarCronometro;

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

                    var vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken).FirstOrDefault();

                    //Si el modo de revision esta activado
                    if (vTipoRevision == "REV")
                    {
                        btnImpresionPrueba.Visible = true;
                        vTiempoPrueba = 0;
                        btnTerminar.Enabled = false;
                        cronometro.Visible = false;
                        //obtener respuestas
                        var respuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);

                        if (vPrueba.NB_TIPO_PRUEBA == "MANUAL")
                            AsignarValoresCapturaManual(respuestas);
                        else
                            asignarValores(respuestas);
                    }
                    else if (vTipoRevision == "EDIT")
                    {
                        //btnEliminar.Visible = true;// Se agrega para la nueva forma de navegación 06/06/2018
                        btnImpresionPrueba.Visible = true; // Se agrega para imprimir en la nueva navegación IDP 06/06/2018
                        DateTime vTiem = DateTime.Now;
                        vTiempoPrueba = 0;
                        cronometro.Visible = false;
                        btnTerminar.Visible = false;
                        btnCorregir.Visible = true;
                        //obtener respuestas
                        var respuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);

                        if (vPrueba.NB_TIPO_PRUEBA == "MANUAL")
                        {
                            AsignarValoresCapturaManual(respuestas);
                            btnCorregir.Enabled = false;
                        }
                        else
                            asignarValores(respuestas);
                    }
                    else
                    {

                    E_RESULTADO vObjetoPrueba = nKprueba.INICIAR_K_PRUEBA(pIdPrueba: vIdPrueba, pFeInicio: DateTime.Now, pClTokenExterno: vClToken, usuario: vClUsuario, programa: vNbPrograma);
                    if (vObjetoPrueba != null)
                    {
                        //    //Si el modo de revision esta activado
                        //if (vTipoRevision == "REV")
                        //{
                        //    btnImpresionPrueba.Visible = true;
                        //    vTiempoPrueba = 0;
                        //    btnTerminar.Enabled = false;
                        //    cronometro.Visible = false;
                        //    //obtener respuestas
                        //    var respuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);

                        //    if (vPrueba.NB_TIPO_PRUEBA == "MANUAL")
                        //        AsignarValoresCapturaManual(respuestas);
                        //    else
                        //        asignarValores(respuestas);
                        //}
                        //    if (vTipoRevision == "EDIT")
                        //    {
                        //        DateTime vTiem = DateTime.Now;
                        //        vTiempoPrueba = 0;
                        //        cronometro.Visible = false;
                        //        btnTerminar.Visible = false;
                        //        btnCorregir.Visible = true;
                        //        //obtener respuestas
                        //        var respuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);

                        //        if (vPrueba.NB_TIPO_PRUEBA == "MANUAL")
                        //            AsignarValoresCapturaManual(respuestas);
                        //        else
                        //            asignarValores(respuestas);
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

                String vTIVA_A_0001 = BackSelectedCheckBox(btnPreg1Resp1, btnPreg1Resp2, btnPreg1Resp3, btnPreg1Resp4);
                AgregarRespuesta("TIVA-A-0001", vTIVA_A_0001);

                String vTIVA_A_0002 = BackSelectedCheckBox(btnPreg2Resp1, btnPreg2Resp2, btnPreg2Resp3, btnPreg2Resp4);
                AgregarRespuesta("TIVA-A-0002", vTIVA_A_0002);

                String vTIVA_A_0003 = BackSelectedCheckBox(btnPreg3Resp1, btnPreg3Resp2, btnPreg3Resp3, btnPreg3Resp4);
                AgregarRespuesta("TIVA-A-0003", vTIVA_A_0003);

                String vTIVA_A_0004 = BackSelectedCheckBox(btnPreg4Resp1, btnPreg4Resp2, btnPreg4Resp3, btnPreg4Resp4);
                AgregarRespuesta("TIVA-A-0004", vTIVA_A_0004);

                String vTIVA_A_0005 = BackSelectedCheckBox(btnPreg5Resp1, btnPreg5Resp2, btnPreg5Resp3, btnPreg5Resp4);
                AgregarRespuesta("TIVA-A-0005", vTIVA_A_0005);

                String vTIVA_A_0006 = BackSelectedCheckBox(btnPreg6Resp1, btnPreg6Resp2, btnPreg6Resp3, btnPreg6Resp4);
                AgregarRespuesta("TIVA-A-0006", vTIVA_A_0006);

                String vTIVA_A_0007 = BackSelectedCheckBox(btnPreg7Resp1, btnPreg7Resp2, btnPreg7Resp3, btnPreg7Resp4);
                AgregarRespuesta("TIVA-A-0007", vTIVA_A_0007);

                String vTIVA_A_0008 = BackSelectedCheckBox(btnPreg8Resp1, btnPreg8Resp2, btnPreg8Resp3, btnPreg8Resp4);
                AgregarRespuesta("TIVA-A-0008", vTIVA_A_0008);

                String vTIVA_A_0009 = BackSelectedCheckBox(btnPreg9Resp1, btnPreg9Resp2, btnPreg9Resp3, btnPreg9Resp4);
                AgregarRespuesta("TIVA-A-0009", vTIVA_A_0009);

                String vTIVA_A_0010 = BackSelectedCheckBox(btnPreg10Resp1, btnPreg10Resp2, btnPreg10Resp3, btnPreg10Resp4);
                AgregarRespuesta("TIVA-A-0010", vTIVA_A_0010);

                String vTIVA_A_0011 = BackSelectedCheckBox(btnPreg11Resp1, btnPreg11Resp2, btnPreg11Resp3, btnPreg11Resp4);
                AgregarRespuesta("TIVA-A-0011", vTIVA_A_0011);

                String vTIVA_A_0012 = BackSelectedCheckBox(btnPreg12Resp1, btnPreg12Resp2, btnPreg12Resp3, btnPreg12Resp4);
                AgregarRespuesta("TIVA-A-0012", vTIVA_A_0012);

                String vTIVA_A_0013 = BackSelectedCheckBox(btnPreg13Resp1, btnPreg13Resp2, btnPreg13Resp3, btnPreg13Resp4);
                AgregarRespuesta("TIVA-A-0013", vTIVA_A_0013);

                String vTIVA_A_0014 = BackSelectedCheckBox(btnPreg14Resp1, btnPreg14Resp2, btnPreg14Resp3, btnPreg14Resp4);
                AgregarRespuesta("TIVA-A-0014", vTIVA_A_0014);

                String vTIVA_A_0015 = BackSelectedCheckBox(btnPreg15Resp1, btnPreg15Resp2, btnPreg15Resp3, btnPreg15Resp4);
                AgregarRespuesta("TIVA-A-0015", vTIVA_A_0015);

                String vTIVA_A_0016 = BackSelectedCheckBox(btnPreg16Resp1, btnPreg16Resp2, btnPreg16Resp3, btnPreg16Resp4);
                AgregarRespuesta("TIVA-A-0016", vTIVA_A_0016);

                String vTIVA_A_0017 = BackSelectedCheckBox(btnPreg17Resp1, btnPreg17Resp2, btnPreg17Resp3, btnPreg17Resp4);
                AgregarRespuesta("TIVA-A-0017", vTIVA_A_0017);

                String vTIVA_A_0018 = BackSelectedCheckBox(btnPreg18Resp1, btnPreg18Resp2, btnPreg18Resp3, btnPreg18Resp4);
                AgregarRespuesta("TIVA-A-0018", vTIVA_A_0018);

                String vTIVA_A_0019 = BackSelectedCheckBox(btnPreg19Resp1, btnPreg19Resp2, btnPreg19Resp3, btnPreg19Resp4);
                AgregarRespuesta("TIVA-A-0019", vTIVA_A_0019);

                String vTIVA_A_0020 = BackSelectedCheckBox(btnPreg20Resp1, btnPreg20Resp2, btnPreg20Resp3, btnPreg20Resp4);
                AgregarRespuesta("TIVA-A-0020", vTIVA_A_0020);

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
                    E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vObjetoPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: "TIVA", usuario: vClUsuario, programa: vNbPrograma);
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

                String vTIVA_A_0001 = BackSelectedCheckBox(btnPreg1Resp1, btnPreg1Resp2, btnPreg1Resp3, btnPreg1Resp4);
                AgregarRespuesta("TIVA-A-0001", vTIVA_A_0001);

                String vTIVA_A_0002 = BackSelectedCheckBox(btnPreg2Resp1, btnPreg2Resp2, btnPreg2Resp3, btnPreg2Resp4);
                AgregarRespuesta("TIVA-A-0002", vTIVA_A_0002);

                String vTIVA_A_0003 = BackSelectedCheckBox(btnPreg3Resp1, btnPreg3Resp2, btnPreg3Resp3, btnPreg3Resp4);
                AgregarRespuesta("TIVA-A-0003", vTIVA_A_0003);

                String vTIVA_A_0004 = BackSelectedCheckBox(btnPreg4Resp1, btnPreg4Resp2, btnPreg4Resp3, btnPreg4Resp4);
                AgregarRespuesta("TIVA-A-0004", vTIVA_A_0004);

                String vTIVA_A_0005 = BackSelectedCheckBox(btnPreg5Resp1, btnPreg5Resp2, btnPreg5Resp3, btnPreg5Resp4);
                AgregarRespuesta("TIVA-A-0005", vTIVA_A_0005);

                String vTIVA_A_0006 = BackSelectedCheckBox(btnPreg6Resp1, btnPreg6Resp2, btnPreg6Resp3, btnPreg6Resp4);
                AgregarRespuesta("TIVA-A-0006", vTIVA_A_0006);

                String vTIVA_A_0007 = BackSelectedCheckBox(btnPreg7Resp1, btnPreg7Resp2, btnPreg7Resp3, btnPreg7Resp4);
                AgregarRespuesta("TIVA-A-0007", vTIVA_A_0007);

                String vTIVA_A_0008 = BackSelectedCheckBox(btnPreg8Resp1, btnPreg8Resp2, btnPreg8Resp3, btnPreg8Resp4);
                AgregarRespuesta("TIVA-A-0008", vTIVA_A_0008);

                String vTIVA_A_0009 = BackSelectedCheckBox(btnPreg9Resp1, btnPreg9Resp2, btnPreg9Resp3, btnPreg9Resp4);
                AgregarRespuesta("TIVA-A-0009", vTIVA_A_0009);

                String vTIVA_A_0010 = BackSelectedCheckBox(btnPreg10Resp1, btnPreg10Resp2, btnPreg10Resp3, btnPreg10Resp4);
                AgregarRespuesta("TIVA-A-0010", vTIVA_A_0010);

                String vTIVA_A_0011 = BackSelectedCheckBox(btnPreg11Resp1, btnPreg11Resp2, btnPreg11Resp3, btnPreg11Resp4);
                AgregarRespuesta("TIVA-A-0011", vTIVA_A_0011);

                String vTIVA_A_0012 = BackSelectedCheckBox(btnPreg12Resp1, btnPreg12Resp2, btnPreg12Resp3, btnPreg12Resp4);
                AgregarRespuesta("TIVA-A-0012", vTIVA_A_0012);

                String vTIVA_A_0013 = BackSelectedCheckBox(btnPreg13Resp1, btnPreg13Resp2, btnPreg13Resp3, btnPreg13Resp4);
                AgregarRespuesta("TIVA-A-0013", vTIVA_A_0013);

                String vTIVA_A_0014 = BackSelectedCheckBox(btnPreg14Resp1, btnPreg14Resp2, btnPreg14Resp3, btnPreg14Resp4);
                AgregarRespuesta("TIVA-A-0014", vTIVA_A_0014);

                String vTIVA_A_0015 = BackSelectedCheckBox(btnPreg15Resp1, btnPreg15Resp2, btnPreg15Resp3, btnPreg15Resp4);
                AgregarRespuesta("TIVA-A-0015", vTIVA_A_0015);

                String vTIVA_A_0016 = BackSelectedCheckBox(btnPreg16Resp1, btnPreg16Resp2, btnPreg16Resp3, btnPreg16Resp4);
                AgregarRespuesta("TIVA-A-0016", vTIVA_A_0016);

                String vTIVA_A_0017 = BackSelectedCheckBox(btnPreg17Resp1, btnPreg17Resp2, btnPreg17Resp3, btnPreg17Resp4);
                AgregarRespuesta("TIVA-A-0017", vTIVA_A_0017);

                String vTIVA_A_0018 = BackSelectedCheckBox(btnPreg18Resp1, btnPreg18Resp2, btnPreg18Resp3, btnPreg18Resp4);
                AgregarRespuesta("TIVA-A-0018", vTIVA_A_0018);

                String vTIVA_A_0019 = BackSelectedCheckBox(btnPreg19Resp1, btnPreg19Resp2, btnPreg19Resp3, btnPreg19Resp4);
                AgregarRespuesta("TIVA-A-0019", vTIVA_A_0019);

                String vTIVA_A_0020 = BackSelectedCheckBox(btnPreg20Resp1, btnPreg20Resp2, btnPreg20Resp3, btnPreg20Resp4);
                AgregarRespuesta("TIVA-A-0020", vTIVA_A_0020);

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
                    E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vObjetoPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: "TIVA", usuario: vClUsuario, programa: vNbPrograma);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
                }
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

        public void AgregarRespuesta(string pclPregunta, string pnbRespuesta)
        {
            var vPregunta = vRespuestas.Where(x => x.CL_PREGUNTA.Equals(pclPregunta)).FirstOrDefault();
            if (vPregunta != null)
            {
                decimal vNoValor;
                vPregunta.NB_RESPUESTA = pnbRespuesta;
                vPregunta.NO_VALOR_RESPUESTA = (vNoValor = (pnbRespuesta != "-") ? 1 : 0);
            }
        }

        public void asignarValores(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> respuestas)
        {
            foreach (SPE_OBTIENE_RESULTADO_PRUEBA_Result resp in respuestas)
            {
                switch (resp.CL_PREGUNTA)
                {
                    case "TIVA-A-0001": if(resp.NB_RESPUESTA != null){SeleccionarBotonRespuesta(btnPreg1Resp1, btnPreg1Resp2, btnPreg1Resp3, btnPreg1Resp4, resp.NB_RESPUESTA);} break;
                    case "TIVA-A-0002": if(resp.NB_RESPUESTA != null){SeleccionarBotonRespuesta(btnPreg2Resp1, btnPreg2Resp2, btnPreg2Resp3, btnPreg2Resp4, resp.NB_RESPUESTA);} break;
                    case "TIVA-A-0003": if(resp.NB_RESPUESTA != null){SeleccionarBotonRespuesta(btnPreg3Resp1, btnPreg3Resp2, btnPreg3Resp3, btnPreg3Resp4, resp.NB_RESPUESTA);} break;
                    case "TIVA-A-0004": if(resp.NB_RESPUESTA != null){SeleccionarBotonRespuesta(btnPreg4Resp1, btnPreg4Resp2, btnPreg4Resp3, btnPreg4Resp4, resp.NB_RESPUESTA);} break;
                    case "TIVA-A-0005": if(resp.NB_RESPUESTA != null){SeleccionarBotonRespuesta(btnPreg5Resp1, btnPreg5Resp2, btnPreg5Resp3, btnPreg5Resp4, resp.NB_RESPUESTA);} break;
                    case "TIVA-A-0006": if(resp.NB_RESPUESTA != null){SeleccionarBotonRespuesta(btnPreg6Resp1, btnPreg6Resp2, btnPreg6Resp3, btnPreg6Resp4, resp.NB_RESPUESTA);} break;
                    case "TIVA-A-0007": if(resp.NB_RESPUESTA != null){SeleccionarBotonRespuesta(btnPreg7Resp1, btnPreg7Resp2, btnPreg7Resp3, btnPreg7Resp4, resp.NB_RESPUESTA);} break;
                    case "TIVA-A-0008": if(resp.NB_RESPUESTA != null){SeleccionarBotonRespuesta(btnPreg8Resp1, btnPreg8Resp2, btnPreg8Resp3, btnPreg8Resp4, resp.NB_RESPUESTA);} break;
                    case "TIVA-A-0009": if(resp.NB_RESPUESTA != null){SeleccionarBotonRespuesta(btnPreg9Resp1, btnPreg9Resp2, btnPreg9Resp3, btnPreg9Resp4, resp.NB_RESPUESTA);} break;
                    case "TIVA-A-0010": if(resp.NB_RESPUESTA != null){SeleccionarBotonRespuesta(btnPreg10Resp1, btnPreg10Resp2, btnPreg10Resp3, btnPreg10Resp4, resp.NB_RESPUESTA);} break;
                    case "TIVA-A-0011": if(resp.NB_RESPUESTA != null){SeleccionarBotonRespuesta(btnPreg11Resp1, btnPreg11Resp2, btnPreg11Resp3, btnPreg11Resp4, resp.NB_RESPUESTA);} break;
                    case "TIVA-A-0012": if(resp.NB_RESPUESTA != null){SeleccionarBotonRespuesta(btnPreg12Resp1, btnPreg12Resp2, btnPreg12Resp3, btnPreg12Resp4, resp.NB_RESPUESTA);} break;
                    case "TIVA-A-0013": if(resp.NB_RESPUESTA != null){SeleccionarBotonRespuesta(btnPreg13Resp1, btnPreg13Resp2, btnPreg13Resp3, btnPreg13Resp4, resp.NB_RESPUESTA);} break;
                    case "TIVA-A-0014": if(resp.NB_RESPUESTA != null){SeleccionarBotonRespuesta(btnPreg14Resp1, btnPreg14Resp2, btnPreg14Resp3, btnPreg14Resp4, resp.NB_RESPUESTA);} break;
                    case "TIVA-A-0015": if(resp.NB_RESPUESTA != null){SeleccionarBotonRespuesta(btnPreg15Resp1, btnPreg15Resp2, btnPreg15Resp3, btnPreg15Resp4, resp.NB_RESPUESTA);} break;
                    case "TIVA-A-0016": if(resp.NB_RESPUESTA != null){SeleccionarBotonRespuesta(btnPreg16Resp1, btnPreg16Resp2, btnPreg16Resp3, btnPreg16Resp4, resp.NB_RESPUESTA);} break;
                    case "TIVA-A-0017": if(resp.NB_RESPUESTA != null){SeleccionarBotonRespuesta(btnPreg17Resp1, btnPreg17Resp2, btnPreg17Resp3, btnPreg17Resp4, resp.NB_RESPUESTA);} break;
                    case "TIVA-A-0018": if(resp.NB_RESPUESTA != null){SeleccionarBotonRespuesta(btnPreg18Resp1, btnPreg18Resp2, btnPreg18Resp3, btnPreg18Resp4, resp.NB_RESPUESTA);} break;
                    case "TIVA-A-0019": if(resp.NB_RESPUESTA != null){SeleccionarBotonRespuesta(btnPreg19Resp1, btnPreg19Resp2, btnPreg19Resp3, btnPreg19Resp4, resp.NB_RESPUESTA);} break;
                    case "TIVA-A-0020": if(resp.NB_RESPUESTA != null){SeleccionarBotonRespuesta(btnPreg20Resp1, btnPreg20Resp2, btnPreg20Resp3, btnPreg20Resp4, resp.NB_RESPUESTA);} break;

                    //case "TIVA_RES_1": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg1Resp1, btnPreg1Resp2, btnPreg1Resp3, btnPreg1Resp4, resp.NB_RESULTADO.ToString()); } break;
                    //case "TIVA_RES_2": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg2Resp1, btnPreg2Resp2, btnPreg2Resp3, btnPreg2Resp4, resp.NB_RESULTADO.ToString()); } break;
                    //case "TIVA_RES_3": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg3Resp1, btnPreg3Resp2, btnPreg3Resp3, btnPreg3Resp4, resp.NB_RESULTADO.ToString()); } break;
                    //case "TIVA_RES_4": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg4Resp1, btnPreg4Resp2, btnPreg4Resp3, btnPreg4Resp4, resp.NB_RESULTADO.ToString()); } break;
                    //case "TIVA_RES_5": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg5Resp1, btnPreg5Resp2, btnPreg5Resp3, btnPreg5Resp4, resp.NB_RESULTADO.ToString()); } break;
                    //case "TIVA_RES_6": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg6Resp1, btnPreg6Resp2, btnPreg6Resp3, btnPreg6Resp4, resp.NB_RESULTADO.ToString()); } break;
                    //case "TIVA_RES_7": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg7Resp1, btnPreg7Resp2, btnPreg7Resp3, btnPreg7Resp4, resp.NB_RESULTADO.ToString()); } break;
                    //case "TIVA_RES_8": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg8Resp1, btnPreg8Resp2, btnPreg8Resp3, btnPreg8Resp4, resp.NB_RESULTADO.ToString()); } break;
                    //case "TIVA_RES_9": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg9Resp1, btnPreg9Resp2, btnPreg9Resp3, btnPreg9Resp4, resp.NB_RESULTADO.ToString()); } break;
                    //case "TIVA_RES_10": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg10Resp1, btnPreg10Resp2, btnPreg10Resp3, btnPreg10Resp4, resp.NB_RESULTADO.ToString()); } break;
                    //case "TIVA_RES_11": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg11Resp1, btnPreg11Resp2, btnPreg11Resp3, btnPreg11Resp4, resp.NB_RESULTADO.ToString()); } break;
                    //case "TIVA_RES_12": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg12Resp1, btnPreg12Resp2, btnPreg12Resp3, btnPreg12Resp4, resp.NB_RESULTADO.ToString()); } break;
                    //case "TIVA_RES_13": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg13Resp1, btnPreg13Resp2, btnPreg13Resp3, btnPreg13Resp4, resp.NB_RESULTADO.ToString()); } break;
                    //case "TIVA_RES_14": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg14Resp1, btnPreg14Resp2, btnPreg14Resp3, btnPreg14Resp4, resp.NB_RESULTADO.ToString()); } break;
                    //case "TIVA_RES_15": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg15Resp1, btnPreg15Resp2, btnPreg15Resp3, btnPreg15Resp4, resp.NB_RESULTADO.ToString()); } break;
                    //case "TIVA_RES_16": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg16Resp1, btnPreg16Resp2, btnPreg16Resp3, btnPreg16Resp4, resp.NB_RESULTADO.ToString()); } break;
                    //case "TIVA_RES_17": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg17Resp1, btnPreg17Resp2, btnPreg17Resp3, btnPreg17Resp4, resp.NB_RESULTADO.ToString()); } break;
                    //case "TIVA_RES_18": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg18Resp1, btnPreg18Resp2, btnPreg18Resp3, btnPreg18Resp4, resp.NB_RESULTADO.ToString()); } break;
                    //case "TIVA_RES_19": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg19Resp1, btnPreg19Resp2, btnPreg19Resp3, btnPreg19Resp4, resp.NB_RESULTADO.ToString()); } break;
                    //case "TIVA_RES_20": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg20Resp1, btnPreg20Resp2, btnPreg20Resp3, btnPreg20Resp4, resp.NB_RESULTADO.ToString()); } break;
                }
            }
        }

        public void SeleccionarBotonRespuesta(RadButton a,RadButton b,RadButton c,RadButton d,string pAnswer) 
        {
            if (pAnswer != null)
            {
                switch (pAnswer.ToUpper().Substring(0,1))
                {
                    case "A": a.Checked = true; break;
                    case "B": b.Checked = true; break;
                    case "C": c.Checked = true; break;
                    case "D": d.Checked = true; break;
                    case "1": a.Checked = true; break;
                    case "2": b.Checked = true; break;
                    case "3": c.Checked = true; break;
                    case "4": d.Checked = true; break;
                    default: break;
                }
            }
        }

        public void AsignarValoresCapturaManual(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> respuestas) 
        {

            foreach (SPE_OBTIENE_RESULTADO_PRUEBA_Result resp in respuestas)
            {
                switch (resp.CL_PREGUNTA)
                {
                    //case "TIVA-A-0001": if (resp.NB_RESPUESTA != null) { SeleccionarBotonRespuesta(btnPreg1Resp1, btnPreg1Resp2, btnPreg1Resp3, btnPreg1Resp4, resp.NB_RESPUESTA); } break;
                    //case "TIVA-A-0002": if (resp.NB_RESPUESTA != null) { SeleccionarBotonRespuesta(btnPreg2Resp1, btnPreg2Resp2, btnPreg2Resp3, btnPreg2Resp4, resp.NB_RESPUESTA); } break;
                    //case "TIVA-A-0003": if (resp.NB_RESPUESTA != null) { SeleccionarBotonRespuesta(btnPreg3Resp1, btnPreg3Resp2, btnPreg3Resp3, btnPreg3Resp4, resp.NB_RESPUESTA); } break;
                    //case "TIVA-A-0004": if (resp.NB_RESPUESTA != null) { SeleccionarBotonRespuesta(btnPreg4Resp1, btnPreg4Resp2, btnPreg4Resp3, btnPreg4Resp4, resp.NB_RESPUESTA); } break;
                    //case "TIVA-A-0005": if (resp.NB_RESPUESTA != null) { SeleccionarBotonRespuesta(btnPreg5Resp1, btnPreg5Resp2, btnPreg5Resp3, btnPreg5Resp4, resp.NB_RESPUESTA); } break;
                    //case "TIVA-A-0006": if (resp.NB_RESPUESTA != null) { SeleccionarBotonRespuesta(btnPreg6Resp1, btnPreg6Resp2, btnPreg6Resp3, btnPreg6Resp4, resp.NB_RESPUESTA); } break;
                    //case "TIVA-A-0007": if (resp.NB_RESPUESTA != null) { SeleccionarBotonRespuesta(btnPreg7Resp1, btnPreg7Resp2, btnPreg7Resp3, btnPreg7Resp4, resp.NB_RESPUESTA); } break;
                    //case "TIVA-A-0008": if (resp.NB_RESPUESTA != null) { SeleccionarBotonRespuesta(btnPreg8Resp1, btnPreg8Resp2, btnPreg8Resp3, btnPreg8Resp4, resp.NB_RESPUESTA); } break;
                    //case "TIVA-A-0009": if (resp.NB_RESPUESTA != null) { SeleccionarBotonRespuesta(btnPreg9Resp1, btnPreg9Resp2, btnPreg9Resp3, btnPreg9Resp4, resp.NB_RESPUESTA); } break;
                    //case "TIVA-A-0010": if (resp.NB_RESPUESTA != null) { SeleccionarBotonRespuesta(btnPreg10Resp1, btnPreg10Resp2, btnPreg10Resp3, btnPreg10Resp4, resp.NB_RESPUESTA); } break;
                    //case "TIVA-A-0011": if (resp.NB_RESPUESTA != null) { SeleccionarBotonRespuesta(btnPreg11Resp1, btnPreg11Resp2, btnPreg11Resp3, btnPreg11Resp4, resp.NB_RESPUESTA); } break;
                    //case "TIVA-A-0012": if (resp.NB_RESPUESTA != null) { SeleccionarBotonRespuesta(btnPreg12Resp1, btnPreg12Resp2, btnPreg12Resp3, btnPreg12Resp4, resp.NB_RESPUESTA); } break;
                    //case "TIVA-A-0013": if (resp.NB_RESPUESTA != null) { SeleccionarBotonRespuesta(btnPreg13Resp1, btnPreg13Resp2, btnPreg13Resp3, btnPreg13Resp4, resp.NB_RESPUESTA); } break;
                    //case "TIVA-A-0014": if (resp.NB_RESPUESTA != null) { SeleccionarBotonRespuesta(btnPreg14Resp1, btnPreg14Resp2, btnPreg14Resp3, btnPreg14Resp4, resp.NB_RESPUESTA); } break;
                    //case "TIVA-A-0015": if (resp.NB_RESPUESTA != null) { SeleccionarBotonRespuesta(btnPreg15Resp1, btnPreg15Resp2, btnPreg15Resp3, btnPreg15Resp4, resp.NB_RESPUESTA); } break;
                    //case "TIVA-A-0016": if (resp.NB_RESPUESTA != null) { SeleccionarBotonRespuesta(btnPreg16Resp1, btnPreg16Resp2, btnPreg16Resp3, btnPreg16Resp4, resp.NB_RESPUESTA); } break;
                    //case "TIVA-A-0017": if (resp.NB_RESPUESTA != null) { SeleccionarBotonRespuesta(btnPreg17Resp1, btnPreg17Resp2, btnPreg17Resp3, btnPreg17Resp4, resp.NB_RESPUESTA); } break;
                    //case "TIVA-A-0018": if (resp.NB_RESPUESTA != null) { SeleccionarBotonRespuesta(btnPreg18Resp1, btnPreg18Resp2, btnPreg18Resp3, btnPreg18Resp4, resp.NB_RESPUESTA); } break;
                    //case "TIVA-A-0019": if (resp.NB_RESPUESTA != null) { SeleccionarBotonRespuesta(btnPreg19Resp1, btnPreg19Resp2, btnPreg19Resp3, btnPreg19Resp4, resp.NB_RESPUESTA); } break;
                    //case "TIVA-A-0020": if (resp.NB_RESPUESTA != null) { SeleccionarBotonRespuesta(btnPreg20Resp1, btnPreg20Resp2, btnPreg20Resp3, btnPreg20Resp4, resp.NB_RESPUESTA); } break;

                    case "TIVA_RES_1": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg1Resp1, btnPreg1Resp2, btnPreg1Resp3, btnPreg1Resp4, resp.NB_RESULTADO.ToString()); } break;
                    case "TIVA_RES_2": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg2Resp1, btnPreg2Resp2, btnPreg2Resp3, btnPreg2Resp4, resp.NB_RESULTADO.ToString()); } break;
                    case "TIVA_RES_3": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg3Resp1, btnPreg3Resp2, btnPreg3Resp3, btnPreg3Resp4, resp.NB_RESULTADO.ToString()); } break;
                    case "TIVA_RES_4": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg4Resp1, btnPreg4Resp2, btnPreg4Resp3, btnPreg4Resp4, resp.NB_RESULTADO.ToString()); } break;
                    case "TIVA_RES_5": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg5Resp1, btnPreg5Resp2, btnPreg5Resp3, btnPreg5Resp4, resp.NB_RESULTADO.ToString()); } break;
                    case "TIVA_RES_6": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg6Resp1, btnPreg6Resp2, btnPreg6Resp3, btnPreg6Resp4, resp.NB_RESULTADO.ToString()); } break;
                    case "TIVA_RES_7": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg7Resp1, btnPreg7Resp2, btnPreg7Resp3, btnPreg7Resp4, resp.NB_RESULTADO.ToString()); } break;
                    case "TIVA_RES_8": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg8Resp1, btnPreg8Resp2, btnPreg8Resp3, btnPreg8Resp4, resp.NB_RESULTADO.ToString()); } break;
                    case "TIVA_RES_9": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg9Resp1, btnPreg9Resp2, btnPreg9Resp3, btnPreg9Resp4, resp.NB_RESULTADO.ToString()); } break;
                    case "TIVA_RES_10": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg10Resp1, btnPreg10Resp2, btnPreg10Resp3, btnPreg10Resp4, resp.NB_RESULTADO.ToString()); } break;
                    case "TIVA_RES_11": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg11Resp1, btnPreg11Resp2, btnPreg11Resp3, btnPreg11Resp4, resp.NB_RESULTADO.ToString()); } break;
                    case "TIVA_RES_12": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg12Resp1, btnPreg12Resp2, btnPreg12Resp3, btnPreg12Resp4, resp.NB_RESULTADO.ToString()); } break;
                    case "TIVA_RES_13": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg13Resp1, btnPreg13Resp2, btnPreg13Resp3, btnPreg13Resp4, resp.NB_RESULTADO.ToString()); } break;
                    case "TIVA_RES_14": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg14Resp1, btnPreg14Resp2, btnPreg14Resp3, btnPreg14Resp4, resp.NB_RESULTADO.ToString()); } break;
                    case "TIVA_RES_15": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg15Resp1, btnPreg15Resp2, btnPreg15Resp3, btnPreg15Resp4, resp.NB_RESULTADO.ToString()); } break;
                    case "TIVA_RES_16": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg16Resp1, btnPreg16Resp2, btnPreg16Resp3, btnPreg16Resp4, resp.NB_RESULTADO.ToString()); } break;
                    case "TIVA_RES_17": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg17Resp1, btnPreg17Resp2, btnPreg17Resp3, btnPreg17Resp4, resp.NB_RESULTADO.ToString()); } break;
                    case "TIVA_RES_18": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg18Resp1, btnPreg18Resp2, btnPreg18Resp3, btnPreg18Resp4, resp.NB_RESULTADO.ToString()); } break;
                    case "TIVA_RES_19": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg19Resp1, btnPreg19Resp2, btnPreg19Resp3, btnPreg19Resp4, resp.NB_RESULTADO.ToString()); } break;
                    case "TIVA_RES_20": if (resp.NB_RESULTADO != null) { SeleccionarBotonRespuesta(btnPreg20Resp1, btnPreg20Resp2, btnPreg20Resp3, btnPreg20Resp4, resp.NB_RESULTADO.ToString()); } break;
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