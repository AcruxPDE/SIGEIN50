using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Negocio.Administracion;
using SIGE.WebApp.Comunes;
using System.Web.Services;
using Telerik.Web.UI;
using SIGE.Negocio.Utilerias;

namespace SIGE.WebApp.IDP
{
    public partial class VentanaInteresesPersonales : System.Web.UI.Page
    {
        #region Propiedades
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private List<E_PREGUNTA> vPregunta
        {
            get { return (List<E_PREGUNTA>)ViewState["vsPregunta"]; }
            set { ViewState["vsPregunta"] = value; }
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

        public Guid vClTokenExterno
        {
            get { return (Guid)ViewState["vsClTokenExterno"]; }
            set { ViewState["vsClTokenExterno"] = value; }
        }

        public int vTiempoPrueba
        {
            get { return (int)ViewState["vsTiempoPrueba"]; }
            set { ViewState["vsTiempoPrueba"] = value; }
        }

        public string vEstatusPrueba
        {
            get { return (string)ViewState["vsvEstatusPrueba"]; }
            set { ViewState["vsvEstatusPrueba"] = value;  }
        }

        public string vTipoRevision
        {
            get { return (string)ViewState["vsTipoRevision"]; }
            set { ViewState["vsTipoRevision"] = value; }
        }

        public bool MostrarCronometro
        {
            get { return (bool)ViewState["vsMostrarCronometroIP"]; }
            set { ViewState["vsMostrarCronometroIP"] = value; }
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

                    MostrarCronometro = ContextoApp.IDP.ConfiguracionPsicometria.FgMostrarCronometro;

                    PruebasNegocio nKprueba = new PruebasNegocio();
                    vIdPrueba = int.Parse(Request.QueryString["ID"]);
                    vClTokenExterno = new Guid(Request.QueryString["T"]);
                    if (Request.QueryString["vIdBateria"] != null)
                    vIdBateria = int.Parse(Request.QueryString["vIdBateria"]);

                    //Si el modo de revision esta activado
                    if (vTipoRevision == "REV")
                    {
                        cronometro.Visible = false;
                        vTiempoPrueba = 0;
                        btnTerminar.Enabled = false;
                        //obtener respuestas
                        var respuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClTokenExterno);
                        var vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClTokenExterno).FirstOrDefault();
                        if (vPrueba.NB_TIPO_PRUEBA == "MANUAL")
                            AsignarValoresManual(respuestas);
                        else
                            asignarValores(respuestas);
                        btnImpresionPrueba.Visible = true;

                    }
                    else if (vTipoRevision == "EDIT")
                    {
                        cronometro.Visible = false;
                        vTiempoPrueba = 0;
                        //btnEliminar.Visible = true;// Se agrega para la nueva forma de navegación 06/06/2018
                        btnTerminar.Visible = false;
                        btnCorregir.Visible = true;
                        btnImpresionPrueba.Visible = true; // Se agrega para imprimir en la nueva navegación IDP 06/06/2018
                        //obtener respuestas
                        var respuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClTokenExterno);
                        var vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClTokenExterno).FirstOrDefault();
                        if (vPrueba.NB_TIPO_PRUEBA == "MANUAL")
                        {
                            AsignarValoresManual(respuestas);
                            btnCorregir.Enabled = false;
                        }
                        else
                            asignarValores(respuestas);
                    }
                    else
                    {

                    E_RESULTADO vObjetoPrueba = nKprueba.INICIAR_K_PRUEBA(pIdPrueba: vIdPrueba, pFeInicio: DateTime.Now, pClTokenExterno: vClTokenExterno, usuario: vClUsuario, programa: vNbPrograma);
                    if (vObjetoPrueba != null)
                    {
                        //         //Si el modo de revision esta activado
                        //if (vTipoRevision == "REV")
                        //{
                        //    cronometro.Visible = false;
                        //    vTiempoPrueba = 0;
                        //    btnTerminar.Enabled = false;
                        //    //obtener respuestas
                        //    var respuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClTokenExterno);
                        //    var vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClTokenExterno).FirstOrDefault();
                        //    if (vPrueba.NB_TIPO_PRUEBA == "MANUAL")
                        //        AsignarValoresManual(respuestas);
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
                        //    var respuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClTokenExterno);
                        //    var vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClTokenExterno).FirstOrDefault();
                        //    if (vPrueba.NB_TIPO_PRUEBA == "MANUAL")
                        //        AsignarValoresManual(respuestas);
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
                        }
                    }
                }
                vPregunta = new List<E_PREGUNTA>();
            }
        }

        [WebMethod]
        public static void IniciaPrueba(int pIdPrueba, Guid pClToken)
        {
            PruebasNegocio negocio = new PruebasNegocio();
            E_RESULTADO vResultado = negocio.INICIAR_K_PRUEBA(pIdPrueba: pIdPrueba, pFeInicio: DateTime.Now, pClTokenExterno: pClToken, usuario: ContextoUsuario.oUsuario.CL_USUARIO, programa: ContextoUsuario.nbPrograma.ToString());
        }

        public void obtenerPreguntas(int pIdPrueba, Guid pClTokenExterno) 
        {
            PreguntaNegocio nPregunta = new PreguntaNegocio();
            vPregunta = new List<E_PREGUNTA>();
            var vListaPreguntas = nPregunta.Obtener_K_PREGUNTA(ID_PRUEBA: vIdPrueba, CL_TOKEN_EXTERNO: vClTokenExterno);
            foreach (var pregunta in vListaPreguntas)
            {
                E_PREGUNTA vObjetoPregunta = new E_PREGUNTA();
                vObjetoPregunta.ID_PRUEBA = pregunta.ID_PRUEBA;
                vObjetoPregunta.ID_CUESTIONARIO_PREGUNTA = pregunta.ID_CUESTIONARIO_PREGUNTA;
                vObjetoPregunta.ID_CUESTIONARIO = pregunta.ID_CUESTIONARIO;
                vObjetoPregunta.ID_PREGUNTA = pregunta.ID_PREGUNTA;
                vObjetoPregunta.CL_PREGUNTA = pregunta.CL_PREGUNTA;
                vObjetoPregunta.NB_PREGUNTA = pregunta.NB_PREGUNTA;
                vObjetoPregunta.DS_PREGUNTA = pregunta.DS_PREGUNTA;
                vObjetoPregunta.CL_TIPO_PREGUNTA = pregunta.CL_TIPO_PREGUNTA;
                vObjetoPregunta.NO_VALOR = pregunta.NO_VALOR;
                vObjetoPregunta.NO_VALOR_RESPUESTA = pregunta.NO_VALOR_RESPUESTA;
                vObjetoPregunta.NB_RESPUESTA = pregunta.NB_RESPUESTA;
                vObjetoPregunta.FG_REQUERIDO = pregunta.FG_REQUERIDO;
                vObjetoPregunta.FG_ACTIVO = pregunta.FG_ACTIVO;
                vObjetoPregunta.ID_COMPETENCIA = pregunta.ID_COMPETENCIA;
                vObjetoPregunta.ID_BITACORA = pregunta.ID_BITACORA;
                vObjetoPregunta.DS_FILTRO = pregunta.DS_FILTRO;
                vPregunta.Add(vObjetoPregunta);
            }
        }

        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            PruebasNegocio nKprueba = new PruebasNegocio();
            SPE_OBTIENE_K_PRUEBA_Result vPruebaTerminada = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClTokenExterno, pIdPrueba: vIdPrueba).FirstOrDefault();
            vPruebaTerminada.FE_TERMINO = DateTime.Now;
            vPruebaTerminada.CL_ESTADO = E_ESTADO_PRUEBA.TERMINADA.ToString();
            vPruebaTerminada.NB_TIPO_PRUEBA = "APLICACION";
            E_RESULTADO vResultado = nKprueba.InsertaActualiza_K_PRUEBA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            GuardarPrueba();
            else if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.WARNING)
            {
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                if (vMensaje == "NO")
                    UtilMensajes.MensajeResultadoDB(rnMensaje, "Usted ha tratado de capturar doble una prueba; los datos no fueron guardados.", E_TIPO_RESPUESTA_DB.SUCCESSFUL, 400, 150, "CloseTest");
            }
        }

        public void GuardarPrueba() {

            obtenerPreguntas(vIdPrueba, vClTokenExterno);

            actualizaLista("INTERES-A0001", "INTERES_RES_A1", txtAsk1ans1.Text);
            actualizaLista("INTERES-A0002", "INTERES_RES_A2", txtAsk1ans2.Text);
            actualizaLista("INTERES-A0003", "INTERES_RES_A3", txtAsk1ans3.Text);
            actualizaLista("INTERES-A0004", "INTERES_RES_A4", txtAsk1ans4.Text);
            actualizaLista("INTERES-A0005", "INTERES_RES_A5", txtAsk1ans5.Text);
            actualizaLista("INTERES-A0006", "INTERES_RES_A6", txtAsk1ans6.Text);

            actualizaLista("INTERES-B0001", "INTERES_RES_B1", txtAsk2ans1.Text);
            actualizaLista("INTERES-B0002", "INTERES_RES_B2", txtAsk2ans2.Text);
            actualizaLista("INTERES-B0003", "INTERES_RES_B3", txtAsk2ans3.Text);
            actualizaLista("INTERES-B0004", "INTERES_RES_B4", txtAsk2ans4.Text);
            actualizaLista("INTERES-B0005", "INTERES_RES_B5", txtAsk2ans5.Text);
            actualizaLista("INTERES-B0006", "INTERES_RES_B6", txtAsk2ans6.Text);

            actualizaLista("INTERES-C0001", "INTERES_RES_C1", txtAsk3ans1.Text);
            actualizaLista("INTERES-C0002", "INTERES_RES_C2", txtAsk3ans2.Text);
            actualizaLista("INTERES-C0003", "INTERES_RES_C3", txtAsk3ans3.Text);
            actualizaLista("INTERES-C0004", "INTERES_RES_C4", txtAsk3ans4.Text);
            actualizaLista("INTERES-C0005", "INTERES_RES_C5", txtAsk3ans5.Text);
            actualizaLista("INTERES-C0006", "INTERES_RES_C6", txtAsk3ans6.Text);

            actualizaLista("INTERES-D0001", "INTERES_RES_D1", txtAsk4ans1.Text);
            actualizaLista("INTERES-D0002", "INTERES_RES_D2", txtAsk4ans2.Text);
            actualizaLista("INTERES-D0003", "INTERES_RES_D3", txtAsk4ans3.Text);
            actualizaLista("INTERES-D0004", "INTERES_RES_D4", txtAsk4ans4.Text);
            actualizaLista("INTERES-D0005", "INTERES_RES_D5", txtAsk4ans5.Text);
            actualizaLista("INTERES-D0006", "INTERES_RES_D6", txtAsk4ans6.Text);

            actualizaLista("INTERES-E0001", "INTERES_RES_E1", txtAsk5ans1.Text);
            actualizaLista("INTERES-E0002", "INTERES_RES_E2", txtAsk5ans2.Text);
            actualizaLista("INTERES-E0003", "INTERES_RES_E3", txtAsk5ans3.Text);
            actualizaLista("INTERES-E0004", "INTERES_RES_E4", txtAsk5ans4.Text);
            actualizaLista("INTERES-E0005", "INTERES_RES_E5", txtAsk5ans5.Text);
            actualizaLista("INTERES-E0006", "INTERES_RES_E6", txtAsk5ans6.Text);

            actualizaLista("INTERES-F0001", "INTERES_RES_F1", txtAsk6ans1.Text);
            actualizaLista("INTERES-F0002", "INTERES_RES_F2", txtAsk6ans2.Text);
            actualizaLista("INTERES-F0003", "INTERES_RES_F3", txtAsk6ans3.Text);
            actualizaLista("INTERES-F0004", "INTERES_RES_F4", txtAsk6ans4.Text);
            actualizaLista("INTERES-F0005", "INTERES_RES_F5", txtAsk6ans5.Text);
            actualizaLista("INTERES-F0006", "INTERES_RES_F6", txtAsk6ans6.Text);

            actualizaLista("INTERES-G0001", "INTERES_RES_G1", txtAsk7ans1.Text);
            actualizaLista("INTERES-G0002", "INTERES_RES_G2", txtAsk7ans2.Text);
            actualizaLista("INTERES-G0003", "INTERES_RES_G3", txtAsk7ans3.Text);
            actualizaLista("INTERES-G0004", "INTERES_RES_G4", txtAsk7ans4.Text);
            actualizaLista("INTERES-G0005", "INTERES_RES_G5", txtAsk7ans5.Text);
            actualizaLista("INTERES-G0006", "INTERES_RES_G6", txtAsk7ans6.Text);

            actualizaLista("INTERES-H0001", "INTERES_RES_H1", txtAsk8ans1.Text);
            actualizaLista("INTERES-H0002", "INTERES_RES_H2", txtAsk8ans2.Text);
            actualizaLista("INTERES-H0003", "INTERES_RES_H3", txtAsk8ans3.Text);
            actualizaLista("INTERES-H0004", "INTERES_RES_H4", txtAsk8ans4.Text);
            actualizaLista("INTERES-H0005", "INTERES_RES_H5", txtAsk8ans5.Text);
            actualizaLista("INTERES-H0006", "INTERES_RES_H6", txtAsk8ans6.Text);

            actualizaLista("INTERES-I0001", "INTERES_RES_I1", txtAsk9ans1.Text);
            actualizaLista("INTERES-I0002", "INTERES_RES_I2", txtAsk9ans2.Text);
            actualizaLista("INTERES-I0003", "INTERES_RES_I3", txtAsk9ans3.Text);
            actualizaLista("INTERES-I0004", "INTERES_RES_I4", txtAsk9ans4.Text);
            actualizaLista("INTERES-I0005", "INTERES_RES_I5", txtAsk9ans5.Text);
            actualizaLista("INTERES-I0006", "INTERES_RES_I6", txtAsk9ans6.Text);

            actualizaLista("INTERES-J0001", "INTERES_RES_J1", txtAsk10ans1.Text);
            actualizaLista("INTERES-J0002", "INTERES_RES_J2", txtAsk10ans2.Text);
            actualizaLista("INTERES-J0003", "INTERES_RES_J3", txtAsk10ans3.Text);
            actualizaLista("INTERES-J0004", "INTERES_RES_J4", txtAsk10ans4.Text);
            actualizaLista("INTERES-J0005", "INTERES_RES_J5", txtAsk10ans5.Text);
            actualizaLista("INTERES-J0006", "INTERES_RES_J6", txtAsk10ans6.Text);

            var vXelements = vPregunta.Select(x =>
                                                 new XElement("RESPUESTA",
                                                 new XAttribute("ID_PREGUNTA", x.ID_PREGUNTA),
                                                 new XAttribute("NB_PREGUNTA", x.NB_PREGUNTA),
                                                 new XAttribute("NB_RESPUESTA", x.NB_RESPUESTA),
                                                 new XAttribute("CL_PREGUNTA", x.CL_PREGUNTA),
                                                 new XAttribute("NO_VALOR_RESPUESTA", x.NO_VALOR_RESPUESTA),
                                                 new XAttribute("ID_CUESTIONARIO_PREGUNTA", x.ID_CUESTIONARIO_PREGUNTA)
                                      ));

            XElement RESPUESTAS = new XElement("RESPUESTAS", vXelements);

            PruebasNegocio nKprueba = new PruebasNegocio();
            var vResultPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClTokenExterno).FirstOrDefault();

            if(vResultPrueba != null){
            CuestionarioPreguntaNegocio negocioCuestionario = new CuestionarioPreguntaNegocio();
            E_RESULTADO vResultado = negocioCuestionario.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vResultPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(),pNbPrueba: "INTERES", usuario: vClUsuario, programa: vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "CloseTest");
            }

        }

        public void EditarPrueba()
        {

            obtenerPreguntas(vIdPrueba, vClTokenExterno);

            actualizaLista("INTERES-A0001", "INTERES_RES_A1", txtAsk1ans1.Text);
            actualizaLista("INTERES-A0002", "INTERES_RES_A2", txtAsk1ans2.Text);
            actualizaLista("INTERES-A0003", "INTERES_RES_A3", txtAsk1ans3.Text);
            actualizaLista("INTERES-A0004", "INTERES_RES_A4", txtAsk1ans4.Text);
            actualizaLista("INTERES-A0005", "INTERES_RES_A5", txtAsk1ans5.Text);
            actualizaLista("INTERES-A0006", "INTERES_RES_A6", txtAsk1ans6.Text);

            actualizaLista("INTERES-B0001", "INTERES_RES_B1", txtAsk2ans1.Text);
            actualizaLista("INTERES-B0002", "INTERES_RES_B2", txtAsk2ans2.Text);
            actualizaLista("INTERES-B0003", "INTERES_RES_B3", txtAsk2ans3.Text);
            actualizaLista("INTERES-B0004", "INTERES_RES_B4", txtAsk2ans4.Text);
            actualizaLista("INTERES-B0005", "INTERES_RES_B5", txtAsk2ans5.Text);
            actualizaLista("INTERES-B0006", "INTERES_RES_B6", txtAsk2ans6.Text);

            actualizaLista("INTERES-C0001", "INTERES_RES_C1", txtAsk3ans1.Text);
            actualizaLista("INTERES-C0002", "INTERES_RES_C2", txtAsk3ans2.Text);
            actualizaLista("INTERES-C0003", "INTERES_RES_C3", txtAsk3ans3.Text);
            actualizaLista("INTERES-C0004", "INTERES_RES_C4", txtAsk3ans4.Text);
            actualizaLista("INTERES-C0005", "INTERES_RES_C5", txtAsk3ans5.Text);
            actualizaLista("INTERES-C0006", "INTERES_RES_C6", txtAsk3ans6.Text);

            actualizaLista("INTERES-D0001", "INTERES_RES_D1", txtAsk4ans1.Text);
            actualizaLista("INTERES-D0002", "INTERES_RES_D2", txtAsk4ans2.Text);
            actualizaLista("INTERES-D0003", "INTERES_RES_D3", txtAsk4ans3.Text);
            actualizaLista("INTERES-D0004", "INTERES_RES_D4", txtAsk4ans4.Text);
            actualizaLista("INTERES-D0005", "INTERES_RES_D5", txtAsk4ans5.Text);
            actualizaLista("INTERES-D0006", "INTERES_RES_D6", txtAsk4ans6.Text);

            actualizaLista("INTERES-E0001", "INTERES_RES_E1", txtAsk5ans1.Text);
            actualizaLista("INTERES-E0002", "INTERES_RES_E2", txtAsk5ans2.Text);
            actualizaLista("INTERES-E0003", "INTERES_RES_E3", txtAsk5ans3.Text);
            actualizaLista("INTERES-E0004", "INTERES_RES_E4", txtAsk5ans4.Text);
            actualizaLista("INTERES-E0005", "INTERES_RES_E5", txtAsk5ans5.Text);
            actualizaLista("INTERES-E0006", "INTERES_RES_E6", txtAsk5ans6.Text);

            actualizaLista("INTERES-F0001", "INTERES_RES_F1", txtAsk6ans1.Text);
            actualizaLista("INTERES-F0002", "INTERES_RES_F2", txtAsk6ans2.Text);
            actualizaLista("INTERES-F0003", "INTERES_RES_F3", txtAsk6ans3.Text);
            actualizaLista("INTERES-F0004", "INTERES_RES_F4", txtAsk6ans4.Text);
            actualizaLista("INTERES-F0005", "INTERES_RES_F5", txtAsk6ans5.Text);
            actualizaLista("INTERES-F0006", "INTERES_RES_F6", txtAsk6ans6.Text);

            actualizaLista("INTERES-G0001", "INTERES_RES_G1", txtAsk7ans1.Text);
            actualizaLista("INTERES-G0002", "INTERES_RES_G2", txtAsk7ans2.Text);
            actualizaLista("INTERES-G0003", "INTERES_RES_G3", txtAsk7ans3.Text);
            actualizaLista("INTERES-G0004", "INTERES_RES_G4", txtAsk7ans4.Text);
            actualizaLista("INTERES-G0005", "INTERES_RES_G5", txtAsk7ans5.Text);
            actualizaLista("INTERES-G0006", "INTERES_RES_G6", txtAsk7ans6.Text);

            actualizaLista("INTERES-H0001", "INTERES_RES_H1", txtAsk8ans1.Text);
            actualizaLista("INTERES-H0002", "INTERES_RES_H2", txtAsk8ans2.Text);
            actualizaLista("INTERES-H0003", "INTERES_RES_H3", txtAsk8ans3.Text);
            actualizaLista("INTERES-H0004", "INTERES_RES_H4", txtAsk8ans4.Text);
            actualizaLista("INTERES-H0005", "INTERES_RES_H5", txtAsk8ans5.Text);
            actualizaLista("INTERES-H0006", "INTERES_RES_H6", txtAsk8ans6.Text);

            actualizaLista("INTERES-I0001", "INTERES_RES_I1", txtAsk9ans1.Text);
            actualizaLista("INTERES-I0002", "INTERES_RES_I2", txtAsk9ans2.Text);
            actualizaLista("INTERES-I0003", "INTERES_RES_I3", txtAsk9ans3.Text);
            actualizaLista("INTERES-I0004", "INTERES_RES_I4", txtAsk9ans4.Text);
            actualizaLista("INTERES-I0005", "INTERES_RES_I5", txtAsk9ans5.Text);
            actualizaLista("INTERES-I0006", "INTERES_RES_I6", txtAsk9ans6.Text);

            actualizaLista("INTERES-J0001", "INTERES_RES_J1", txtAsk10ans1.Text);
            actualizaLista("INTERES-J0002", "INTERES_RES_J2", txtAsk10ans2.Text);
            actualizaLista("INTERES-J0003", "INTERES_RES_J3", txtAsk10ans3.Text);
            actualizaLista("INTERES-J0004", "INTERES_RES_J4", txtAsk10ans4.Text);
            actualizaLista("INTERES-J0005", "INTERES_RES_J5", txtAsk10ans5.Text);
            actualizaLista("INTERES-J0006", "INTERES_RES_J6", txtAsk10ans6.Text);

            var vXelements = vPregunta.Select(x =>
                                                 new XElement("RESPUESTA",
                                                 new XAttribute("ID_PREGUNTA", x.ID_PREGUNTA),
                                                 new XAttribute("NB_PREGUNTA", x.NB_PREGUNTA),
                                                 new XAttribute("NB_RESPUESTA", x.NB_RESPUESTA),
                                                 new XAttribute("CL_PREGUNTA", x.CL_PREGUNTA),
                                                 new XAttribute("NO_VALOR_RESPUESTA", x.NO_VALOR_RESPUESTA),
                                                 new XAttribute("ID_CUESTIONARIO_PREGUNTA", x.ID_CUESTIONARIO_PREGUNTA)
                                      ));

            XElement RESPUESTAS = new XElement("RESPUESTAS", vXelements);

            PruebasNegocio nKprueba = new PruebasNegocio();
            var vResultPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClTokenExterno).FirstOrDefault();

            if (vResultPrueba != null)
            {
                CuestionarioPreguntaNegocio negocioCuestionario = new CuestionarioPreguntaNegocio();
                E_RESULTADO vResultado = negocioCuestionario.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vResultPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: "INTERES", usuario: vClUsuario, programa: vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
            }

        }

        public void actualizaLista(string pClPreguntaTipo1,string pClPreguntaTipo2, string valorNuevo)
        {
            var vPregunt = vPregunta.Where(x => x.CL_PREGUNTA.Equals(pClPreguntaTipo1)).FirstOrDefault();
            if (vPregunt != null)
            {
                decimal vNoValor;
                vPregunt.CL_PREGUNTA = pClPreguntaTipo2;
                vPregunt.NB_RESPUESTA = valorNuevo;
                vPregunt.NO_VALOR_RESPUESTA = (vNoValor = (valorNuevo != "") ? decimal.Parse(valorNuevo) : 0);
            }
        }

        public void asignarValores(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> respuestas)
        {
            foreach (SPE_OBTIENE_RESULTADO_PRUEBA_Result resp in respuestas)
            {
                switch (resp.CL_PREGUNTA)
                {
                    case "INTERES-A0001": txtAsk1ans1.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-A0002": txtAsk1ans2.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-A0003": txtAsk1ans3.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-A0004": txtAsk1ans4.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-A0005": txtAsk1ans5.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-A0006": txtAsk1ans6.Text = resp.NB_RESPUESTA; break;

                    case "INTERES-B0001": txtAsk2ans1.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-B0002": txtAsk2ans2.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-B0003": txtAsk2ans3.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-B0004": txtAsk2ans4.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-B0005": txtAsk2ans5.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-B0006": txtAsk2ans6.Text = resp.NB_RESPUESTA; break;

                    case "INTERES-C0001": txtAsk3ans1.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-C0002": txtAsk3ans2.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-C0003": txtAsk3ans3.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-C0004": txtAsk3ans4.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-C0005": txtAsk3ans5.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-C0006": txtAsk3ans6.Text = resp.NB_RESPUESTA; break;

                    case "INTERES-D0001": txtAsk4ans1.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-D0002": txtAsk4ans2.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-D0003": txtAsk4ans3.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-D0004": txtAsk4ans4.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-D0005": txtAsk4ans5.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-D0006": txtAsk4ans6.Text = resp.NB_RESPUESTA; break;

                    case "INTERES-E0001": txtAsk5ans1.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-E0002": txtAsk5ans2.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-E0003": txtAsk5ans3.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-E0004": txtAsk5ans4.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-E0005": txtAsk5ans5.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-E0006": txtAsk5ans6.Text = resp.NB_RESPUESTA; break;

                    case "INTERES-F0001": txtAsk6ans1.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-F0002": txtAsk6ans2.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-F0003": txtAsk6ans3.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-F0004": txtAsk6ans4.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-F0005": txtAsk6ans5.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-F0006": txtAsk6ans6.Text = resp.NB_RESPUESTA; break;

                    case "INTERES-G0001": txtAsk7ans1.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-G0002": txtAsk7ans2.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-G0003": txtAsk7ans3.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-G0004": txtAsk7ans4.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-G0005": txtAsk7ans5.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-G0006": txtAsk7ans6.Text = resp.NB_RESPUESTA; break;

                    case "INTERES-H0001": txtAsk8ans1.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-H0002": txtAsk8ans2.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-H0003": txtAsk8ans3.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-H0004": txtAsk8ans4.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-H0005": txtAsk8ans5.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-H0006": txtAsk8ans6.Text = resp.NB_RESPUESTA; break;

                    case "INTERES-I0001": txtAsk9ans1.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-I0002": txtAsk9ans2.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-I0003": txtAsk9ans3.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-I0004": txtAsk9ans4.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-I0005": txtAsk9ans5.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-I0006": txtAsk9ans6.Text = resp.NB_RESPUESTA; break;
                    
                    case "INTERES-J0001": txtAsk10ans1.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-J0002": txtAsk10ans2.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-J0003": txtAsk10ans3.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-J0004": txtAsk10ans4.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-J0005": txtAsk10ans5.Text = resp.NB_RESPUESTA; break;
                    case "INTERES-J0006": txtAsk10ans6.Text = resp.NB_RESPUESTA; break;
                }
            }
        }

        public void AsignarValoresManual(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> respuestas)
        {
            foreach (SPE_OBTIENE_RESULTADO_PRUEBA_Result resp in respuestas)
            {
                switch (resp.CL_PREGUNTA)
                {
                    case "INTERES_RES_A1": txtAsk1ans1.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_A2": txtAsk1ans2.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_A3": txtAsk1ans3.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_A4": txtAsk1ans4.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_A5": txtAsk1ans5.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_A6": txtAsk1ans6.Text = resp.NB_RESULTADO; break;

                    case "INTERES_RES_B1": txtAsk2ans1.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_B2": txtAsk2ans2.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_B3": txtAsk2ans3.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_B4": txtAsk2ans4.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_B5": txtAsk2ans5.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_B6": txtAsk2ans6.Text = resp.NB_RESULTADO; break;

                    case "INTERES_RES_C1": txtAsk3ans1.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_C2": txtAsk3ans2.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_C3": txtAsk3ans3.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_C4": txtAsk3ans4.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_C5": txtAsk3ans5.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_C6": txtAsk3ans6.Text = resp.NB_RESULTADO; break;

                    case "INTERES_RES_D1": txtAsk4ans1.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_D2": txtAsk4ans2.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_D3": txtAsk4ans3.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_D4": txtAsk4ans4.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_D5": txtAsk4ans5.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_D6": txtAsk4ans6.Text = resp.NB_RESULTADO; break;

                    case "INTERES_RES_E1": txtAsk5ans1.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_E2": txtAsk5ans2.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_E3": txtAsk5ans3.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_E4": txtAsk5ans4.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_E5": txtAsk5ans5.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_E6": txtAsk5ans6.Text = resp.NB_RESULTADO; break;

                    case "INTERES_RES_F1": txtAsk6ans1.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_F2": txtAsk6ans2.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_F3": txtAsk6ans3.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_F4": txtAsk6ans4.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_F5": txtAsk6ans5.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_F6": txtAsk6ans6.Text = resp.NB_RESULTADO; break;

                    case "INTERES_RES_G1": txtAsk7ans1.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_G2": txtAsk7ans2.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_G3": txtAsk7ans3.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_G4": txtAsk7ans4.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_G5": txtAsk7ans5.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_G6": txtAsk7ans6.Text = resp.NB_RESULTADO; break;

                    case "INTERES_RES_H1": txtAsk8ans1.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_H2": txtAsk8ans2.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_H3": txtAsk8ans3.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_H4": txtAsk8ans4.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_H5": txtAsk8ans5.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_H6": txtAsk8ans6.Text = resp.NB_RESULTADO; break;

                    case "INTERES_RES_I1": txtAsk9ans1.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_I2": txtAsk9ans2.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_I3": txtAsk9ans3.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_I4": txtAsk9ans4.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_I5": txtAsk9ans5.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_I6": txtAsk9ans6.Text = resp.NB_RESULTADO; break;

                    case "INTERES_RES_J1": txtAsk10ans1.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_J2": txtAsk10ans2.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_J3": txtAsk10ans3.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_J4": txtAsk10ans4.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_J5": txtAsk10ans5.Text = resp.NB_RESULTADO; break;
                    case "INTERES_RES_J6": txtAsk10ans6.Text = resp.NB_RESULTADO; break;
                }
            }
        }

        protected void btnCorregir_Click(object sender, EventArgs e)
        {
            PruebasNegocio nKprueba = new PruebasNegocio();
            SPE_OBTIENE_K_PRUEBA_Result vPruebaTerminada = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClTokenExterno, pIdPrueba: vIdPrueba).FirstOrDefault();
            E_RESULTADO vResultado = nKprueba.CorrigePrueba(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                EditarPrueba();
            else 
            {
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
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
    }
}