using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Negocio.Administracion;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace SIGE.WebApp.IDP
{
    public partial class VentanaInglesManual : System.Web.UI.Page
    {
        #region "Variables"

        private static E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private List<E_PRUEBA_RESULTADO> vResultados
        {
            get { return (List<E_PRUEBA_RESULTADO>)ViewState["vsResultadoPrueba"]; }
            set { ViewState["vsResultadoPrueba"] = value; }
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

        public string vEstatusPrueba
        {
            get { return (string)ViewState["vsvEstatusPrueba"]; }
            set { ViewState["vsvEstatusPrueba"] = value; }
        }

        #endregion
        
        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = (ContextoUsuario.oUsuario != null ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO");
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                vResultados = new List<E_PRUEBA_RESULTADO>();
                if (Request.QueryString["ID"] != null)
                {
                    PruebasNegocio nKprueba = new PruebasNegocio();
                    vIdPrueba = int.Parse(Request.QueryString["ID"]);
                    vClToken = new Guid(Request.QueryString["T"]);
                    //E_RESULTADO vObjetoPrueba = nKprueba.INICIAR_K_PRUEBA(pIdPrueba: vIdPrueba, pFeInicio: DateTime.Now, pClTokenExterno: vClToken, usuario: vClUsuario, programa: vNbPrograma);
                    //if (vObjetoPrueba.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.ERROR)
                    //{
                        PruebasNegocio nPruebas = new PruebasNegocio();
                        var prueba = nPruebas.Obtener_RESULTADO_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).ToList();
                        var vPrueba = nPruebas.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();
                        if (prueba != null)
                        {
                            if (vPrueba.NB_TIPO_PRUEBA == "APLICACION")
                                CargarResultadosAplicacion(prueba);
                            else
                                CargarRespuestasIngles(prueba);
                        }
                    //}
                    //else if (vObjetoPrueba.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                    //{
                    //}
                }
            }
        }

        public void CargarRespuestasIngles(List  <SPE_OBTIENE_RESULTADO_PRUEBA_Result> pResultados) 
        {
            if (pResultados.Count > 0) 
            {
                txtRespuesta1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INGLES_RES_S1")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INGLES_RES_S1")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INGLES_RES_S1")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                txtRespuesta2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INGLES_RES_S2")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INGLES_RES_S2")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INGLES_RES_S2")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                txtRespuesta3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INGLES_RES_S3")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INGLES_RES_S3")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INGLES_RES_S3")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                txtRespuesta4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INGLES_RES_S4")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INGLES_RES_S4")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INGLES_RES_S4")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                //if (pResultados.Where(w => w.CL_PREGUNTA.Equals("INGLES_RES_S1")).FirstOrDefault().NB_RESULTADO == null)
                //    btnTerminar.Enabled = false;
            
            }
        }

        public void CargarResultadosAplicacion(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> pResultados)
        {
            if (pResultados.Count > 0)
            {
                txtRespuesta1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INGLES_RES_S1")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INGLES_RES_S1")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INGLES_RES_S1")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString()) : "";
                txtRespuesta2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INGLES_RES_S2")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INGLES_RES_S2")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INGLES_RES_S2")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString()) : "";
                txtRespuesta3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INGLES_RES_S3")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INGLES_RES_S3")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INGLES_RES_S3")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString()) : "";
                txtRespuesta4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INGLES_RES_S4")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INGLES_RES_S4")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INGLES_RES_S4")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString()) : "";
            }
        }

        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            PruebasNegocio nKprueba = new PruebasNegocio();
            SPE_OBTIENE_K_PRUEBA_Result vPruebaTerminada = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();
            vPruebaTerminada.FE_TERMINO = DateTime.Now;
            vPruebaTerminada.FE_INICIO = DateTime.Now;
            vPruebaTerminada.CL_ESTADO = E_ESTADO_PRUEBA.TERMINADA.ToString();
            vPruebaTerminada.NB_TIPO_PRUEBA = "MANUAL";
            E_RESULTADO vResultado = nKprueba.InsertaActualiza_K_PRUEBA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
            SaveTest();
        }

        public void SaveTest()
        {
            BackQuestionObject("INGLES_RES_S1", txtRespuesta1.Text);
            BackQuestionObject("INGLES_RES_S2", txtRespuesta2.Text);
            BackQuestionObject("INGLES_RES_S3", txtRespuesta3.Text);
            BackQuestionObject("INGLES_RES_S4", txtRespuesta4.Text);

            var vXelements = vResultados.Select(x =>
                                                 new XElement("RESULTADO",
                                                 new XAttribute("CL_VARIABLE", x.CL_VARIABLE),
                                                 new XAttribute("NO_VALOR", x.NO_VALOR),
                                                 new XAttribute("NO_VALOR_RES", x.NO_VALOR_RES)
                                      ));
            XElement RESPUESTAS =
            new XElement("RESULTADOS", vXelements
            );

            ResultadoNegocio negRes = new ResultadoNegocio();
            PruebasNegocio nKprueba = new PruebasNegocio();
            var vObjetoPrueba = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();

            if (vObjetoPrueba != null)
            {
                E_RESULTADO vResultado = negRes.insertaResultadosIngles(RESPUESTAS.ToString(), null, vIdPrueba,"", vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "CloseTest");
            }
        }

        public void BackQuestionObject(string pClVariable, string pnbRespuesta)
        {
            E_PRUEBA_RESULTADO vResultado = new E_PRUEBA_RESULTADO();

            if (pnbRespuesta != "")
            {
                vResultado.NO_VALOR = int.Parse(pnbRespuesta);
                vResultado.NO_VALOR_RES = int.Parse(pnbRespuesta);
            }
            else {
                vResultado.NO_VALOR = 0;
                vResultado.NO_VALOR_RES = 0;
            }
            vResultado.CL_VARIABLE = pClVariable;
            vResultados.Add(vResultado);

        }
    }
}