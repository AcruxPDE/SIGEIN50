using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.WebApp.Comunes;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Entidades.IntegracionDePersonal;
using System.Xml.Linq;
using SIGE.Entidades;
using SIGE.Negocio.Utilerias;

namespace SIGE.WebApp.IDP
{
    public partial class PruebaOrtografia1 : System.Web.UI.Page
    {
        #region Propiedades

        private string vClUsuario;
        private string vNbPrograma;
        /*private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        */
        public int vOrtografia1Seconds
        {
            get { return (int)ViewState["vsOrtografia1Seconds"]; }
            set { ViewState["vsOrtografia1Seconds"] = value; }
        }
        public int vIdPrueba
        {
            get { return (int)ViewState["vsIdPrueba"]; }
            set { ViewState["vsIdPrueba"] = value; }
        }

        public Guid vClToken
        {
            get { return (Guid)ViewState["vsClToken"]; }
            set { ViewState["vsClToken"] = value; }
        }

        private DateTime? vNow
        {
            get { return (DateTime?)ViewState["vsNow"]; }
            set { ViewState["vsNow"] = value; }
        }

        public string vTipoRevision
        {
            get { return (string)ViewState["vsTipoRevision"]; }
            set { ViewState["vsTipoRevision"] = value; }
        }

        List<E_PREGUNTA> vRespuestasPOrtografia1 
          {
            get { return (List<E_PREGUNTA>)ViewState["vsRespuestasPOrtografia1"]; }
            set { ViewState["vsRespuestasPOrtografia1"] = value; }
        }

        public bool MostrarCronometro
        {
            get { return (bool)ViewState["vsMostrarCronometroO1"]; }
            set { ViewState["vsMostrarCronometroO1"] = value; }
        }

        public string VerificaValor(string pValor)
        {
            string vValorCampo ="";
            if (pValor == "")
                vValorCampo = "_";
            else
                vValorCampo = pValor;

            return vValorCampo;
        }

        public string AsignaValor(string pValor)
        {

            string vValorCampo = "";
            if (pValor == "_")
                vValorCampo = "";
            else if (pValor == null)
                vValorCampo = "";
            else
                vValorCampo = pValor;

            return vValorCampo;
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
                 vRespuestasPOrtografia1  = new List<E_PREGUNTA>();
                if (Request.QueryString["ID"] != null && Request.QueryString["T"] != null)
                {

                    MostrarCronometro = ContextoApp.IDP.ConfiguracionPsicometria.FgMostrarCronometro;

                    if (Request.QueryString["MOD"] != null)
                    {
                        vTipoRevision =Request.QueryString["MOD"];
                    }
                    PruebasNegocio nKprueba = new PruebasNegocio();
                    if (vNow == null)
                    {
                        vNow = DateTime.Now;
                    }
                    vIdPrueba = int.Parse(Request.QueryString["ID"]);
                    vClToken = Guid.Parse(Request.QueryString["T"].ToString());
                    vIdBateria = int.Parse(Request.QueryString["vIdBateria"]);

                    //Si el modo de revision esta activado
                    if (vTipoRevision == "REV")
                    {

                        cronometro.Visible = false;
                        vOrtografia1Seconds = 0;
                        btnTerminar.Enabled = false;
                        btnImpresionPrueba.Visible = true;
                        //obtener respuestas
                        var respuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);
                        asignarValores(respuestas);
                    }
                    else if (vTipoRevision == "EDIT")
                    {
                        //btnEliminar.Visible = true;// Se agrega para la nueva forma de navegación 06/06/2018
                        cronometro.Visible = false;
                        vOrtografia1Seconds = 0;
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

                    var vObjetoPrueba = nKprueba.INICIAR_K_PRUEBA(pIdPrueba: vIdPrueba, pFeInicio: vNow.Value,pClTokenExterno: vClToken,usuario:vClUsuario,programa:vNbPrograma);
                        //pClTokenExterno: vClToken, pIdPrueba: vIdPrueba, pFeInicio: vNow).FirstOrDefault();

                    if (vObjetoPrueba != null)
                    {
                        ////Si el modo de revision esta activado
                        //if (vTipoRevision == "REV")
                        //{
                        //    cronometro.Visible = false;
                        //    vOrtografia1Seconds = 0;
                        //    btnTerminar.Enabled = false;
                        //    btnImpresionPrueba.Visible = true;
                        //    //obtener respuestas
                        //    var respuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);
                        //    asignarValores(respuestas);
                        //}
                        //else if (vTipoRevision == "EDIT")
                        //{
                        //    cronometro.Visible = false;
                        //    vOrtografia1Seconds = 0;
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
                                //REDIRECCIONAR
                                vOrtografia1Seconds = 0;
                                //UtilMensajes.MensajeResultadoDB(rnMensaje, "La prueba no esta disponible.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "CloseTest");
                                btnTerminar.Enabled = false;
                            }
                            else if (vObjetoPrueba.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                            {
                                vOrtografia1Seconds = int.Parse(vObjetoPrueba.MENSAJE.Where(r => r.CL_IDIOMA.Equals("ES")).FirstOrDefault().DS_MENSAJE.ToString());
                                if (vOrtografia1Seconds <= 0)
                                {
                                    UtilMensajes.MensajeResultadoDB(rnMensaje, "La prueba no esta disponible.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "CloseTest");
                                    btnTerminar.Enabled = false;
                                }
                            }
                        }
                        
                    }
                }
                else 
                {
                    //FALTAN PARAMETROS REDIRECCIONAR AL LOGIN
                    vOrtografia1Seconds = 0;
                    UtilMensajes.MensajeResultadoDB(rnMensaje,"No existe la prueba", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "CloseTest");
                    btnTerminar.Enabled = false;
                }

            }
        }

        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            PruebasNegocio nKprueba = new PruebasNegocio();
            var vObjetoPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pIdPruebaPlantilla: 9, pClTokenExterno: vClToken);

            PreguntaNegocio nPregunta = new PreguntaNegocio();
            var vPregunta = nPregunta.Obtener_K_PREGUNTA(ID_PRUEBA: vIdPrueba, CL_TOKEN_EXTERNO:vClToken );
            if (vPregunta.Count > 0)
            {
                vRespuestasPOrtografia1 = (from c in vPregunta
                 select new E_PREGUNTA
                {
                    ID_CUESTIONARIO_PREGUNTA = c.ID_CUESTIONARIO_PREGUNTA,
                    ID_PREGUNTA = c.ID_PREGUNTA,
                    CL_PREGUNTA = c.CL_PREGUNTA,
                    NB_PREGUNTA = c.NB_PREGUNTA,
                    DS_PREGUNTA = c.DS_PREGUNTA,
                    CL_TIPO_PREGUNTA = c.CL_TIPO_PREGUNTA,
                    NO_VALOR = c.NO_VALOR,
                    FG_REQUERIDO = c.FG_REQUERIDO,
                    FG_ACTIVO = c.FG_ACTIVO,
                    ID_COMPETENCIA = c.ID_COMPETENCIA,
                    ID_BITACORA = c.ID_BITACORA
                }).ToList();
                //MODIFCAR EL OBJETO EN CADA PREGUNTA

                BackQuestionObject(VerificaValor(pregunta1A1.Text) + VerificaValor(pregunta1A2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0001")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta2A1.Text) + VerificaValor(pregunta2A2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0002")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta3A1.Text) + VerificaValor(pregunta3A2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0003")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta4A1.Text) + VerificaValor(pregunta4A2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0004")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta5A1.Text) + VerificaValor(pregunta5A2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0005")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta6A1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0006")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta7A1.Text) + VerificaValor(pregunta7A2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0007")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta8A1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0008")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta9A1.Text) + VerificaValor(pregunta9A2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0009")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta10A1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0010")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta11A1.Text) + VerificaValor(pregunta11A2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0011")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta12A1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0012")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta13A1.Text) + VerificaValor(pregunta13A2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0013")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta14A1.Text) + VerificaValor(pregunta14A2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0014")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta15A1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0015")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta16A1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0016")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta17A1.Text) + VerificaValor(pregunta17A2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0017")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta18A1.Text) , vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0018")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta19A1.Text) + VerificaValor(pregunta19A2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0019")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta20A1.Text) , vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0020")).FirstOrDefault());


                BackQuestionObject(VerificaValor(pregunta1B1.Text) , vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0001")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta2B1.Text) + VerificaValor(pregunta2B2.Text) , vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0002")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta3B1.Text) , vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0003")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta4B1.Text) , vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0004")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta5B1.Text) + VerificaValor(pregunta5B2.Text) , vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0005")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta6B1.Text) + VerificaValor(pregunta6B2.Text) , vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0006")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta7B1.Text) , vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0007")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta8B1.Text) , vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0008")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta9B1.Text) , vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0009")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta10B1.Text) , vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0010")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta11B1.Text) , vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0011")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta12B1.Text) , vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0012")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta13B1.Text) , vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0013")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta14B1.Text) , vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0014")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta15B1.Text) , vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0015")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta16B1.Text) , vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0016")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta17B1.Text) , vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0017")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta18B1.Text) , vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0018")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta19B1.Text) + VerificaValor(pregunta19B2.Text) , vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0019")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta20B1.Text) + VerificaValor(pregunta20B2.Text) + VerificaValor(pregunta20B3.Text) , vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0020")).FirstOrDefault());


              //BackQuestionObject(preguntaA1.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0001")).FirstOrDefault());
              //BackQuestionObject(preguntaA2.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0002")).FirstOrDefault());
              //BackQuestionObject(preguntaA3.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0003")).FirstOrDefault());
              //BackQuestionObject(preguntaA4.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0004")).FirstOrDefault());
              //BackQuestionObject(preguntaA5.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0005")).FirstOrDefault());
              //BackQuestionObject(preguntaA6.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0006")).FirstOrDefault());
              //BackQuestionObject(preguntaA7.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0007")).FirstOrDefault());
              //BackQuestionObject(preguntaA8.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0008")).FirstOrDefault());
              //BackQuestionObject(preguntaA9.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0009")).FirstOrDefault());
              //BackQuestionObject(preguntaA10.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0010")).FirstOrDefault());
              //BackQuestionObject(preguntaA11.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0011")).FirstOrDefault());
              //BackQuestionObject(preguntaA12.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0012")).FirstOrDefault());
              //BackQuestionObject(preguntaA13.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0013")).FirstOrDefault());
              //BackQuestionObject(preguntaA14.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0014")).FirstOrDefault());
              //BackQuestionObject(preguntaA15.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0015")).FirstOrDefault());
              //BackQuestionObject(preguntaA16.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0016")).FirstOrDefault());
              //BackQuestionObject(preguntaA17.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0017")).FirstOrDefault());
              //BackQuestionObject(preguntaA18.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0018")).FirstOrDefault());
              //BackQuestionObject(preguntaA19.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0019")).FirstOrDefault());
              //BackQuestionObject(preguntaA20.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0020")).FirstOrDefault());


              //BackQuestionObject(preguntaB1.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0001")).FirstOrDefault());
              //BackQuestionObject(preguntaB2.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0002")).FirstOrDefault());
              //BackQuestionObject(preguntaB3.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0003")).FirstOrDefault());
              //BackQuestionObject(preguntaB4.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0004")).FirstOrDefault());
              //BackQuestionObject(preguntaB5.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0005")).FirstOrDefault());
              //BackQuestionObject(preguntaB6.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0006")).FirstOrDefault());
              //BackQuestionObject(preguntaB7.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0007")).FirstOrDefault());
              //BackQuestionObject(preguntaB8.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0008")).FirstOrDefault());
              //BackQuestionObject(preguntaB9.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0009")).FirstOrDefault());
              //BackQuestionObject(preguntaB10.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0010")).FirstOrDefault());
              //BackQuestionObject(preguntaB11.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0011")).FirstOrDefault());
              //BackQuestionObject(preguntaB12.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0012")).FirstOrDefault());
              //BackQuestionObject(preguntaB13.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0013")).FirstOrDefault());
              //BackQuestionObject(preguntaB14.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0014")).FirstOrDefault());
              //BackQuestionObject(preguntaB15.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0015")).FirstOrDefault());
              //BackQuestionObject(preguntaB16.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0016")).FirstOrDefault());
              //BackQuestionObject(preguntaB17.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0017")).FirstOrDefault());
              //BackQuestionObject(preguntaB18.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0018")).FirstOrDefault());
              //BackQuestionObject(preguntaB19.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0019")).FirstOrDefault());
              //BackQuestionObject(preguntaB20.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0020")).FirstOrDefault());

                var vXelements = vRespuestasPOrtografia1.Select(x =>
                                                new XElement("RESPUESTA",
                                                new XAttribute("ID_PREGUNTA", x.ID_PREGUNTA),
                                                new XAttribute("ID_CUESTIONARIO_PREGUNTA",x.ID_CUESTIONARIO_PREGUNTA),
                                                new XAttribute("NB_PREGUNTA", x.NB_PREGUNTA),
                                                new XAttribute("CL_PREGUNTA", x.CL_PREGUNTA),
                                                new XAttribute("NB_RESPUESTA", x.NB_RESPUESTA),
                                                new XAttribute("NO_VALOR_RESPUESTA", x.NO_VALOR_RESPUESTA)
                                     ));
                XElement RESPUESTAS =
                new XElement("RESPUESTAS", vXelements
                );

                CuestionarioPreguntaNegocio nCustionarioPregunta = new CuestionarioPreguntaNegocio();
                PruebasNegocio objPrueba = new PruebasNegocio();
                var vKPrueba = objPrueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();

                SPE_OBTIENE_K_PRUEBA_Result vPruebaTerminada = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();
                vPruebaTerminada.FE_TERMINO = DateTime.Now;
                vPruebaTerminada.CL_ESTADO = E_ESTADO_PRUEBA.TERMINADA.ToString();
                vPruebaTerminada.NB_TIPO_PRUEBA = "APLICACION";
                E_RESULTADO vResultados = nKprueba.InsertaActualiza_K_PRUEBA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
                if (vResultados.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                {
                    E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vKPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: "ORTOGRAFIA-1", usuario: vClUsuario, programa: vNbPrograma);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals("ES")).FirstOrDefault().DS_MENSAJE;
                    btnTerminar.Enabled = false;
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "CloseTest");
                }
                else if (vResultados.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.WARNING)
                {
                    string vMensaje = vResultados.MENSAJE.Where(w => w.CL_IDIOMA.Equals("ES")).FirstOrDefault().DS_MENSAJE;
                    if (vMensaje == "NO")
                        UtilMensajes.MensajeResultadoDB(rnMensaje, "Usted ha tratado de capturar doble una prueba; los datos no fueron guardados.", E_TIPO_RESPUESTA_DB.SUCCESSFUL, 400, 150, "CloseTest");
                }
            }
        }

        public void BackQuestionObject(string pnbRespuesta, E_PREGUNTA pEPregunta)
        {
                pEPregunta.NB_RESPUESTA = pnbRespuesta;
                pEPregunta.NO_VALOR_RESPUESTA = (pnbRespuesta.ToUpper() == pEPregunta.NB_PREGUNTA.ToUpper() ? 1 : 0);
        }

        public void asignarValores(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> respuestas)
        {
            foreach (SPE_OBTIENE_RESULTADO_PRUEBA_Result resp in respuestas)
            {
                if (resp.NB_RESPUESTA != null)
                {
                    switch (resp.CL_PREGUNTA)
                    {

                        case "ORTOGRAFIA-1-A0001":
                            pregunta1A1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            pregunta1A2.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[1].ToString());
                            break;
                        case "ORTOGRAFIA-1-A0002":
                            pregunta2A1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            pregunta2A2.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[1].ToString());
                            break;
                        case "ORTOGRAFIA-1-A0003":
                            pregunta3A1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            pregunta3A2.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[1].ToString());
                            break;
                        case "ORTOGRAFIA-1-A0004":
                            pregunta4A1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            pregunta4A2.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[1].ToString());
                            break;
                        case "ORTOGRAFIA-1-A0005":
                            pregunta5A1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            pregunta5A2.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[1].ToString());
                            break;
                        case "ORTOGRAFIA-1-A0006":
                            pregunta6A1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            break;
                        case "ORTOGRAFIA-1-A0007":
                            pregunta7A1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            pregunta7A2.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[1].ToString());
                            break;
                        case "ORTOGRAFIA-1-A0008":
                            pregunta8A1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            break;
                        case "ORTOGRAFIA-1-A0009":
                            pregunta9A1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            pregunta9A2.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[1].ToString());
                            break;
                        case "ORTOGRAFIA-1-A0010":
                            pregunta10A1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            break;
                        case "ORTOGRAFIA-1-A0011":
                            pregunta11A1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            pregunta11A2.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[1].ToString());
                            break;
                        case "ORTOGRAFIA-1-A0012":
                            pregunta12A1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            break;
                        case "ORTOGRAFIA-1-A0013":
                            pregunta13A1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            pregunta13A2.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[1].ToString());
                            break;
                        case "ORTOGRAFIA-1-A0014":
                            pregunta14A1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            pregunta14A2.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[1].ToString());
                            break;
                        case "ORTOGRAFIA-1-A0015":
                            pregunta15A1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            break;
                        case "ORTOGRAFIA-1-A0016":
                            pregunta16A1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            break;
                        case "ORTOGRAFIA-1-A0017":
                            pregunta17A1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            pregunta17A2.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[1].ToString());
                            break;
                        case "ORTOGRAFIA-1-A0018":
                            pregunta18A1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            break;
                        case "ORTOGRAFIA-1-A0019":
                            pregunta19A1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            pregunta19A2.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[1].ToString());
                            break;
                        case "ORTOGRAFIA-1-A0020":
                            pregunta20A1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            break;

                        case "ORTOGRAFIA-1-B0001":
                            pregunta1B1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            break;
                        case "ORTOGRAFIA-1-B0002":
                            pregunta2B1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            pregunta2B2.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[1].ToString());
                            break;
                        case "ORTOGRAFIA-1-B0003":
                            pregunta3B1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            break;
                        case "ORTOGRAFIA-1-B0004":
                            pregunta4B1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            break;
                        case "ORTOGRAFIA-1-B0005":
                            pregunta5B1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            pregunta5B2.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[1].ToString());
                            break;
                        case "ORTOGRAFIA-1-B0006":
                            pregunta6B1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            pregunta6B2.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[1].ToString());
                            break;
                        case "ORTOGRAFIA-1-B0007":
                            pregunta7B1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            break;
                        case "ORTOGRAFIA-1-B0008":
                            pregunta8B1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            break;
                        case "ORTOGRAFIA-1-B0009":
                            pregunta9B1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            break;
                        case "ORTOGRAFIA-1-B0010":
                            pregunta10B1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            break;
                        case "ORTOGRAFIA-1-B0011":
                            pregunta11B1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            break;
                        case "ORTOGRAFIA-1-B0012":
                            pregunta12B1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            break;
                        case "ORTOGRAFIA-1-B0013":
                            pregunta13B1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            break;
                        case "ORTOGRAFIA-1-B0014":
                            pregunta14B1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            break;
                        case "ORTOGRAFIA-1-B0015":
                            pregunta15B1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            break;
                        case "ORTOGRAFIA-1-B0016":
                            pregunta16B1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            break;
                        case "ORTOGRAFIA-1-B0017":
                            pregunta17B1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            break;
                        case "ORTOGRAFIA-1-B0018":
                            pregunta18B1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            break;
                        case "ORTOGRAFIA-1-B0019":
                            pregunta19B1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            pregunta19B2.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[1].ToString());
                            break;
                        case "ORTOGRAFIA-1-B0020":
                            pregunta20B1.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[0].ToString());
                            pregunta20B2.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[1].ToString());
                            pregunta20B3.Text = AsignaValor(resp.NB_RESPUESTA.ToCharArray()[2].ToString());
                            break;
               

                    //case "ORTOGRAFIA-1-A0001": preguntaA1.TextWithLiterals = resp.NB_RESPUESTA;  break;
                    //case "ORTOGRAFIA-1-A0002": preguntaA2.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-A0003": preguntaA3.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-A0004": preguntaA4.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-A0005": preguntaA5.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-A0006": preguntaA6.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-A0007": preguntaA7.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-A0008": preguntaA8.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-A0009": preguntaA9.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-A0010": preguntaA10.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-A0011": preguntaA11.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-A0012": preguntaA12.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-A0013": preguntaA13.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-A0014": preguntaA14.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-A0015": preguntaA15.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-A0016": preguntaA16.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-A0017": preguntaA17.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-A0018": preguntaA18.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-A0019": preguntaA19.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-A0020": preguntaA20.TextWithLiterals = resp.NB_RESPUESTA; break;

                    //case "ORTOGRAFIA-1-B0001": preguntaB1.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-B0002": preguntaB2.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-B0003": preguntaB3.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-B0004": preguntaB4.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-B0005": preguntaB5.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-B0006": preguntaB6.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-B0007": preguntaB7.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-B0008": preguntaB8.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-B0009": preguntaB9.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-B0010": preguntaB10.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-B0011": preguntaB11.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-B0012": preguntaB12.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-B0013": preguntaB13.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-B0014": preguntaB14.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-B0015": preguntaB15.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-B0016": preguntaB16.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-B0017": preguntaB17.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-B0018": preguntaB18.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-B0019": preguntaB19.TextWithLiterals = resp.NB_RESPUESTA; break;
                    //case "ORTOGRAFIA-1-B0020": preguntaB20.TextWithLiterals = resp.NB_RESPUESTA; break;
                    }
                }
               
            }
        }

        protected void btnCorregir_Click(object sender, EventArgs e)
        {
            PruebasNegocio nKprueba = new PruebasNegocio();
            var vObjetoPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pIdPruebaPlantilla: 9, pClTokenExterno: vClToken);

            PreguntaNegocio nPregunta = new PreguntaNegocio();
            var vPregunta = nPregunta.Obtener_K_PREGUNTA(ID_PRUEBA: vIdPrueba, CL_TOKEN_EXTERNO: vClToken);
            if (vPregunta.Count > 0)
            {
                vRespuestasPOrtografia1 = (from c in vPregunta
                                           select new E_PREGUNTA
                                           {
                                               ID_CUESTIONARIO_PREGUNTA = c.ID_CUESTIONARIO_PREGUNTA,
                                               ID_PREGUNTA = c.ID_PREGUNTA,
                                               CL_PREGUNTA = c.CL_PREGUNTA,
                                               NB_PREGUNTA = c.NB_PREGUNTA,
                                               DS_PREGUNTA = c.DS_PREGUNTA,
                                               CL_TIPO_PREGUNTA = c.CL_TIPO_PREGUNTA,
                                               NO_VALOR = c.NO_VALOR,
                                               FG_REQUERIDO = c.FG_REQUERIDO,
                                               FG_ACTIVO = c.FG_ACTIVO,
                                               ID_COMPETENCIA = c.ID_COMPETENCIA,
                                               ID_BITACORA = c.ID_BITACORA
                                           }).ToList();
                //MODIFCAR EL OBJETO EN CADA PREGUNTA


                BackQuestionObject(VerificaValor(pregunta1A1.Text) + VerificaValor(pregunta1A2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0001")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta2A1.Text) + VerificaValor(pregunta2A2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0002")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta3A1.Text) + VerificaValor(pregunta3A2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0003")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta4A1.Text) + VerificaValor(pregunta4A2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0004")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta5A1.Text) + VerificaValor(pregunta5A2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0005")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta6A1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0006")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta7A1.Text) + VerificaValor(pregunta7A2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0007")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta8A1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0008")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta9A1.Text) + VerificaValor(pregunta9A2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0009")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta10A1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0010")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta11A1.Text) + VerificaValor(pregunta11A2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0011")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta12A1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0012")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta13A1.Text) + VerificaValor(pregunta13A2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0013")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta14A1.Text) + VerificaValor(pregunta14A2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0014")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta15A1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0015")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta16A1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0016")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta17A1.Text) + VerificaValor(pregunta17A2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0017")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta18A1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0018")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta19A1.Text) + VerificaValor(pregunta19A2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0019")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta20A1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0020")).FirstOrDefault());


                BackQuestionObject(VerificaValor(pregunta1B1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0001")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta2B1.Text) + VerificaValor(pregunta2B2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0002")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta3B1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0003")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta4B1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0004")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta5B1.Text) + VerificaValor(pregunta5B2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0005")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta6B1.Text) + VerificaValor(pregunta6B2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0006")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta7B1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0007")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta8B1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0008")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta9B1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0009")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta10B1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0010")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta11B1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0011")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta12B1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0012")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta13B1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0013")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta14B1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0014")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta15B1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0015")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta16B1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0016")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta17B1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0017")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta18B1.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0018")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta19B1.Text) + VerificaValor(pregunta19B2.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0019")).FirstOrDefault());
                BackQuestionObject(VerificaValor(pregunta20B1.Text) + VerificaValor(pregunta20B2.Text) + VerificaValor(pregunta20B3.Text), vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0020")).FirstOrDefault());


                //BackQuestionObject(preguntaA1.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0001")).FirstOrDefault());
                //BackQuestionObject(preguntaA2.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0002")).FirstOrDefault());
                //BackQuestionObject(preguntaA3.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0003")).FirstOrDefault());
                //BackQuestionObject(preguntaA4.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0004")).FirstOrDefault());
                //BackQuestionObject(preguntaA5.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0005")).FirstOrDefault());
                //BackQuestionObject(preguntaA6.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0006")).FirstOrDefault());
                //BackQuestionObject(preguntaA7.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0007")).FirstOrDefault());
                //BackQuestionObject(preguntaA8.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0008")).FirstOrDefault());
                //BackQuestionObject(preguntaA9.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0009")).FirstOrDefault());
                //BackQuestionObject(preguntaA10.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0010")).FirstOrDefault());
                //BackQuestionObject(preguntaA11.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0011")).FirstOrDefault());
                //BackQuestionObject(preguntaA12.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0012")).FirstOrDefault());
                //BackQuestionObject(preguntaA13.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0013")).FirstOrDefault());
                //BackQuestionObject(preguntaA14.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0014")).FirstOrDefault());
                //BackQuestionObject(preguntaA15.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0015")).FirstOrDefault());
                //BackQuestionObject(preguntaA16.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0016")).FirstOrDefault());
                //BackQuestionObject(preguntaA17.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0017")).FirstOrDefault());
                //BackQuestionObject(preguntaA18.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0018")).FirstOrDefault());
                //BackQuestionObject(preguntaA19.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0019")).FirstOrDefault());
                //BackQuestionObject(preguntaA20.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-A0020")).FirstOrDefault());


                //BackQuestionObject(preguntaB1.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0001")).FirstOrDefault());
                //BackQuestionObject(preguntaB2.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0002")).FirstOrDefault());
                //BackQuestionObject(preguntaB3.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0003")).FirstOrDefault());
                //BackQuestionObject(preguntaB4.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0004")).FirstOrDefault());
                //BackQuestionObject(preguntaB5.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0005")).FirstOrDefault());
                //BackQuestionObject(preguntaB6.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0006")).FirstOrDefault());
                //BackQuestionObject(preguntaB7.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0007")).FirstOrDefault());
                //BackQuestionObject(preguntaB8.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0008")).FirstOrDefault());
                //BackQuestionObject(preguntaB9.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0009")).FirstOrDefault());
                //BackQuestionObject(preguntaB10.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0010")).FirstOrDefault());
                //BackQuestionObject(preguntaB11.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0011")).FirstOrDefault());
                //BackQuestionObject(preguntaB12.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0012")).FirstOrDefault());
                //BackQuestionObject(preguntaB13.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0013")).FirstOrDefault());
                //BackQuestionObject(preguntaB14.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0014")).FirstOrDefault());
                //BackQuestionObject(preguntaB15.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0015")).FirstOrDefault());
                //BackQuestionObject(preguntaB16.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0016")).FirstOrDefault());
                //BackQuestionObject(preguntaB17.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0017")).FirstOrDefault());
                //BackQuestionObject(preguntaB18.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0018")).FirstOrDefault());
                //BackQuestionObject(preguntaB19.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0019")).FirstOrDefault());
                //BackQuestionObject(preguntaB20.TextWithPromptAndLiterals, vRespuestasPOrtografia1.Where(r => r.CL_PREGUNTA.Equals("ORTOGRAFIA-1-B0020")).FirstOrDefault());

                var vXelements = vRespuestasPOrtografia1.Select(x =>
                                                new XElement("RESPUESTA",
                                                new XAttribute("ID_PREGUNTA", x.ID_PREGUNTA),
                                                new XAttribute("ID_CUESTIONARIO_PREGUNTA", x.ID_CUESTIONARIO_PREGUNTA),
                                                new XAttribute("NB_PREGUNTA", x.NB_PREGUNTA),
                                                new XAttribute("CL_PREGUNTA", x.CL_PREGUNTA),
                                                new XAttribute("NB_RESPUESTA", x.NB_RESPUESTA),
                                                new XAttribute("NO_VALOR_RESPUESTA", x.NO_VALOR_RESPUESTA)
                                     ));
                XElement RESPUESTAS =
                new XElement("RESPUESTAS", vXelements
                );

                CuestionarioPreguntaNegocio nCustionarioPregunta = new CuestionarioPreguntaNegocio();
                PruebasNegocio objPrueba = new PruebasNegocio();
                var vKPrueba = objPrueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();

                SPE_OBTIENE_K_PRUEBA_Result vPruebaTerminada = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();
                E_RESULTADO vResultados = nKprueba.CorrigePrueba(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
                if (vResultados.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                {
                    E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vKPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: "ORTOGRAFIA-1", usuario: vClUsuario, programa: vNbPrograma);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals("ES")).FirstOrDefault().DS_MENSAJE;
                    btnTerminar.Enabled = false;
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
                }
                else 
                {
                    string vMensaje = vResultados.MENSAJE.Where(w => w.CL_IDIOMA.Equals("ES")).FirstOrDefault().DS_MENSAJE;
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultados.CL_TIPO_ERROR, 400, 150, "");
                }
            }
        }

        protected void btnEliminarBateria_Click(object sender, EventArgs e)
        {
            PruebasNegocio nPruebas = new PruebasNegocio();
            var vResultado = nPruebas.EliminaRespuestasBaterias(vIdBateria, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals("ES")).FirstOrDefault().DS_MENSAJE;
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
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals("ES")).FirstOrDefault().DS_MENSAJE;
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