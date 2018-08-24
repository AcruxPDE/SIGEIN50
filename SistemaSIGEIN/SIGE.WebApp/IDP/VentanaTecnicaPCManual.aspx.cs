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
    public partial class VentanaTecnicaPCManual : System.Web.UI.Page
    {
        #region Variables
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public string vEstatusPrueba;

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

        private int vIdPrueba
        {
            get { return (int)ViewState["vsIdEvaluado"]; }
            set { ViewState["vsIdEvaluado"] = value; }
        }

        private List<E_PRUEBA_RESULTADO> vPruebaResultado
        {
            get { return (List<E_PRUEBA_RESULTADO>)ViewState["vsPruebaResultado"]; }
            set { ViewState["vsPruebaResultado"] = value; }
        }

        private Guid vClToken
        {
            get { return (Guid)ViewState["vsClToken"]; }
            set { ViewState["vsClToken"] = value; }
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
                            CargarRespuestasAplicacion(prueba);
                            else
                            CargarRespuestas(prueba);
                        }
                    //}
                    //else if (vObjetoPrueba.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                    //{
                    //}
                }

                vPruebaResultado = new List<E_PRUEBA_RESULTADO>();
            }
        }

        public void CargarRespuestas(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> pResultados)
        {
            if (pResultados.Count > 0)
            {
                txtrespuesta1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("TECNICAPC_RES_C")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TECNICAPC_RES_C")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TECNICAPC_RES_C")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                txtrespuesta2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("TECNICAPC_RES_S")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TECNICAPC_RES_S")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TECNICAPC_RES_S")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                txtrespuesta3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("TECNICAPC_RES_I")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TECNICAPC_RES_I")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TECNICAPC_RES_I")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                txtrespuesta4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("TECNICAPC_RES_H")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TECNICAPC_RES_H")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TECNICAPC_RES_H")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                //if (pResultados.Where(w => w.CL_PREGUNTA.Equals("TECNICAPC_RES_C")).FirstOrDefault().NB_RESULTADO == null)
                //    btnTerminar.Enabled = false;
            }
        }

        public void CargarRespuestasAplicacion(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> pResultados)
        {
            if (pResultados.Count > 0)
            {
                decimal? TECNICAPC_REP_C = pResultados.Where(x => x.CL_PREGUNTA.Equals("TECNICAPC_RES_C")).FirstOrDefault().NO_VALOR_RESPUESTA;
                decimal? TECNICAPC_REP_S = pResultados.Where(x => x.CL_PREGUNTA.Equals("TECNICAPC_RES_S")).FirstOrDefault().NO_VALOR_RESPUESTA;
                decimal? TECNICAPC_REP_I = pResultados.Where(x => x.CL_PREGUNTA.Equals("TECNICAPC_RES_I")).FirstOrDefault().NO_VALOR_RESPUESTA;
                decimal? TECNICAPC_REP_H = pResultados.Where(x => x.CL_PREGUNTA.Equals("TECNICAPC_RES_H")).FirstOrDefault().NO_VALOR_RESPUESTA;
                txtrespuesta1.Text = (Math.Round((decimal)TECNICAPC_REP_C, 0)).ToString();
                txtrespuesta2.Text = (Math.Round((decimal)TECNICAPC_REP_S, 0)).ToString();
                txtrespuesta3.Text = (Math.Round((decimal)TECNICAPC_REP_I, 0)).ToString();
                txtrespuesta4.Text = (Math.Round((decimal)TECNICAPC_REP_H, 0)).ToString();
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

            BackQuestionObject("TECNICAPC_RES_C", txtrespuesta1.Text);
            BackQuestionObject("TECNICAPC_RES_S", txtrespuesta2.Text);
            BackQuestionObject("TECNICAPC_RES_I", txtrespuesta3.Text);
            BackQuestionObject("TECNICAPC_RES_H", txtrespuesta4.Text);

            var vXelements = vPruebaResultado.Select(x =>
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
                E_RESULTADO vResultado = negRes.insertaResultadosTecnicaPc(RESPUESTAS.ToString(), null, vIdPrueba, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                //UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "CloseTest");
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
            }

        }

        public void BackQuestionObject(string pClVariable, string valorNuevo)
        {
            E_PRUEBA_RESULTADO vResultado = new E_PRUEBA_RESULTADO();

            if (valorNuevo != "")
            {
                vResultado.NO_VALOR = int.Parse(valorNuevo);
                vResultado.NO_VALOR_RES = int.Parse(valorNuevo);
               
            }
            else {
                vResultado.NO_VALOR = 0;
                vResultado.NO_VALOR_RES = 0;
            }
            vResultado.CL_VARIABLE = pClVariable;
            vPruebaResultado.Add(vResultado);
        }

    }

}
