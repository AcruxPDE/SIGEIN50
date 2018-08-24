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
    public partial class VentanaTIVAManual : System.Web.UI.Page
    {
        #region Propiedades

        public int vTiempoPrueba
        {
            get { return (int)ViewState["vsAptitudMental1seconds"]; }
            set { ViewState["vsAptitudMental1seconds"] = value; }
        }

        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private List<E_PRUEBA_RESULTADO> vPruebaResultado
        {
            get { return (List<E_PRUEBA_RESULTADO>)ViewState["vsPruebaResultado"]; }
            set { ViewState["vsPruebaResultado"] = value; }
        }

        private List<E_RESULTADOS_TIVA> vRespuestas
        {
            get { return (List<E_RESULTADOS_TIVA>)ViewState["vsvRespuestas"]; }
            set { ViewState["vsvRespuestas"] = value; }
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

        private int vIdPrueba
        {
            get { return (int)ViewState["vsIdEvaluado"]; }
            set { ViewState["vsIdEvaluado"] = value; }
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

                        var prueba = nKprueba.Obtener_RESULTADO_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).ToList();
                        var vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken).FirstOrDefault();
                        if (prueba != null)
                        {
                            vRespuestas = new List<E_RESULTADOS_TIVA>();
                          //  CargarRespuestas();
                            if (vPrueba.NB_TIPO_PRUEBA == "MANUAL")
                                AsignarRespuestasTextBox(prueba);
                            else
                                AsignarRespuestasAplicacion(prueba);
                        }
                    //}
                    //else if (vObjetoPrueba.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                    //{
                    //    vTiempoPrueba = int.Parse(vObjetoPrueba.MENSAJE.Where(r => r.CL_IDIOMA.Equals("ES")).FirstOrDefault().DS_MENSAJE.ToString());
                    //}
                     
                }
                vRespuestas = new List<E_RESULTADOS_TIVA>();
                CargarRespuestas();
                vPruebaResultado = new List<E_PRUEBA_RESULTADO>();
            }
        }

        public void AsignarRespuestasTextBox(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> pResultados)
        {
            if (pResultados.Count > 0)
            {
                txtResp1.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_1")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_1")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_1")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtResp2.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_2")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_2")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_2")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtResp3.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_3")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_3")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_3")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtResp4.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_4")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_4")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_4")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtResp5.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_5")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_5")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_5")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtResp6.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_6")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_6")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_6")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtResp7.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_7")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_7")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_7")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtResp8.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_8")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_8")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_8")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtResp9.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_9")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_9")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_9")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtResp10.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_10")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_10")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_10")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtResp11.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_11")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_11")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_11")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtResp12.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_12")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_12")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_12")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtResp13.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_13")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_13")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_13")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtResp14.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_14")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_14")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_14")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtResp15.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_15")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_15")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_15")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtResp16.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_16")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_16")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_16")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtResp17.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_17")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_17")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_17")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtResp18.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_18")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_18")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_18")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtResp19.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_19")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_19")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_19")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtResp20.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_20")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_20")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_20")).FirstOrDefault().NB_RESULTADO.ToString()) : "");

                //if (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA_RES_1")).FirstOrDefault().NB_RESULTADO == null)
                //    btnTerminar.Enabled = false;
            }
        }

        public void AsignarRespuestasAplicacion(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> pResultados)
        {
            if (pResultados.Count > 0)
            {
                txtResp1.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0001")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0001")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0001")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtResp2.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0002")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0002")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0002")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtResp3.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0003")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0003")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0003")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtResp4.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0004")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0004")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0004")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtResp5.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0005")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0005")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0005")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtResp6.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0006")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0006")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0006")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtResp7.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0007")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0007")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0007")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtResp8.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0008")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0008")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0008")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtResp9.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0009")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0009")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0009")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtResp10.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0010")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0010")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0010")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtResp11.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0011")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0011")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0011")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtResp12.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0012")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0012")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0012")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtResp13.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0013")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0013")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0013")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtResp14.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0014")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0014")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0014")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtResp15.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0015")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0015")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0015")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtResp16.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0016")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0016")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0016")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtResp17.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0017")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0017")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0017")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtResp18.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0018")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0018")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0018")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtResp19.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0019")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0019")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0019")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtResp20.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0020")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0020")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("TIVA-A-0020")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");

            }
        }

        public string CambiarNumeroPorLetra(string valor) 
        {
            if (!valor.Equals(""))
            {
                string letra = "";
                switch (valor.ToLower())
                {
                    case "1": letra = "a"; break;
                    case "2": letra = "b"; break;
                    case "3": letra = "c"; break;
                    case "4": letra = "d"; break;
                    case "0": letra = ""; break;
                    case "a": letra = "a"; break;
                    case "b": letra = "b"; break;
                    case "c": letra = "c"; break;
                    case "d": letra = "d"; break;
                }
                return letra.ToLower();
            }
            else 
            {
                return string.Empty;
            }
        }




        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            PruebasNegocio nKprueba = new PruebasNegocio();
            SPE_OBTIENE_K_PRUEBA_Result vPruebaTerminada = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();
            vPruebaTerminada.FE_TERMINO = DateTime.Now;
            vPruebaTerminada.FE_INICIO = DateTime.Now;
            vPruebaTerminada.CL_ESTADO = E_ESTADO_PRUEBA.TERMINADA.ToString();
            //if (vPruebaTerminada.NB_TIPO_PRUEBA == "APLICACION")
            //{
            //    vPruebaTerminada.NB_TIPO_PRUEBA = "APLICACION";
            //    E_RESULTADO vResultado = nKprueba.InsertaActualiza_K_PRUEBA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
            //    SaveTest();
            //}
            //else
            //{
                vPruebaTerminada.NB_TIPO_PRUEBA = "MANUAL";
                E_RESULTADO vResultado = nKprueba.InsertaActualiza_K_PRUEBA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
                SaveTest();
           // }
        }

        public void SaveTest()
        {

                String vTIVA_A_0001 = txtResp1.Text;
                BackQuestionObject("TIVA_RES_1", vTIVA_A_0001);
                BackQuestionObject("TIVA-A-0001", vTIVA_A_0001);

                String vTIVA_A_0002 = txtResp2.Text;
                BackQuestionObject("TIVA_RES_2", vTIVA_A_0002);
                BackQuestionObject("TIVA-A-0002", vTIVA_A_0002);


                String vTIVA_A_0003 = txtResp3.Text;
                BackQuestionObject("TIVA_RES_3", vTIVA_A_0003);
                BackQuestionObject("TIVA-A-0003", vTIVA_A_0003);


                String vTIVA_A_0004 = txtResp4.Text;
                BackQuestionObject("TIVA_RES_4", vTIVA_A_0004);
                BackQuestionObject("TIVA-A-0004", vTIVA_A_0004);


                String vTIVA_A_0005 = txtResp5.Text;
                BackQuestionObject("TIVA_RES_5", vTIVA_A_0005);
                BackQuestionObject("TIVA-A-0005", vTIVA_A_0005);


                String vTIVA_A_0006 = txtResp6.Text;
                BackQuestionObject("TIVA_RES_6", vTIVA_A_0006);
                BackQuestionObject("TIVA-A-0006", vTIVA_A_0006);


                String vTIVA_A_0007 = txtResp7.Text;
                BackQuestionObject("TIVA_RES_7", vTIVA_A_0007);
                BackQuestionObject("TIVA-A-0007", vTIVA_A_0007);


                String vTIVA_A_0008 = txtResp8.Text;
                BackQuestionObject("TIVA_RES_8", vTIVA_A_0008);
                BackQuestionObject("TIVA-A-0008", vTIVA_A_0008);


                String vTIVA_A_0009 = txtResp9.Text;
                BackQuestionObject("TIVA_RES_9", vTIVA_A_0009);
                BackQuestionObject("TIVA-A-0009", vTIVA_A_0009);


                String vTIVA_A_0010 = txtResp10.Text;
                BackQuestionObject("TIVA_RES_10", vTIVA_A_0010);
                BackQuestionObject("TIVA-A-0010", vTIVA_A_0010);


                String vTIVA_A_0011 = txtResp11.Text;
                BackQuestionObject("TIVA_RES_11", vTIVA_A_0011);
                BackQuestionObject("TIVA-A-0011", vTIVA_A_0011);


                String vTIVA_A_0012 = txtResp12.Text;
                BackQuestionObject("TIVA_RES_12", vTIVA_A_0012);
                BackQuestionObject("TIVA-A-0012", vTIVA_A_0012);


                String vTIVA_A_0013 = txtResp13.Text;
                BackQuestionObject("TIVA_RES_13", vTIVA_A_0013);
                BackQuestionObject("TIVA-A-0013", vTIVA_A_0013);


                String vTIVA_A_0014 = txtResp14.Text;
                BackQuestionObject("TIVA_RES_14", vTIVA_A_0014);
                BackQuestionObject("TIVA-A-0014", vTIVA_A_0014);


                String vTIVA_A_0015 = txtResp15.Text;
                BackQuestionObject("TIVA_RES_15", vTIVA_A_0015);
                BackQuestionObject("TIVA-A-0015", vTIVA_A_0015);


                String vTIVA_A_0016 = txtResp16.Text;
                BackQuestionObject("TIVA_RES_16", vTIVA_A_0016);
                BackQuestionObject("TIVA-A-0016", vTIVA_A_0016);


                String vTIVA_A_0017 = txtResp17.Text;
                BackQuestionObject("TIVA_RES_17", vTIVA_A_0017);
                BackQuestionObject("TIVA-A-0017", vTIVA_A_0017);


                String vTIVA_A_0018 = txtResp18.Text;
                BackQuestionObject("TIVA_RES_18", vTIVA_A_0018);
                BackQuestionObject("TIVA-A-0018", vTIVA_A_0018);


                String vTIVA_A_0019 = txtResp19.Text;
                BackQuestionObject("TIVA_RES_19", vTIVA_A_0019);
                BackQuestionObject("TIVA-A-0019", vTIVA_A_0019);


                String vTIVA_A_0020 = txtResp20.Text;
                BackQuestionObject("TIVA_RES_20", vTIVA_A_0020);
                BackQuestionObject("TIVA-A-0020", vTIVA_A_0020);


                var vXelements = vPruebaResultado.Select(x =>
                                                   new XElement("RESULTADO",
                                                   new XAttribute("CL_VARIABLE", x.CL_VARIABLE),
                                                   new XAttribute("NO_VALOR", x.NO_VALOR),
                                                   new XAttribute("NO_VALOR_RES",x.NO_VALOR_RES)
                                        ));
                XElement RESPUESTAS =
                new XElement("RESULTADOS", vXelements
                );

                ResultadoNegocio nResNeg = new ResultadoNegocio();
                PruebasNegocio nKprueba = new PruebasNegocio();
                var vObjetoPrueba = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();

                if (vObjetoPrueba != null)
                {
                    E_RESULTADO vResultado = nResNeg.insertaResultadosTiva(RESPUESTAS.ToString(), null, vIdPrueba, vClUsuario, vNbPrograma);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "CloseTest");
                }
        }
    
        public void BackQuestionObject(string pClVariable, string pRespuesta)
        {
            E_PRUEBA_RESULTADO vResultado = new E_PRUEBA_RESULTADO();
            int vNoValor = 0;
            int vNoValorFinal = 0;

            switch (pRespuesta)
            {
                case "a":
                    if (pClVariable.Contains("RES"))
                    {
                        vNoValorFinal = vRespuestas.Where(w => w.CL_VARIABLE.Equals(pClVariable)).First().NO_VALOR1;
                        vNoValor = 1;
                    }
                    else 
                    {
                        vNoValor = 1;
                    }
                    break;
                case "b":
                    if (pClVariable.Contains("RES"))
                    {
                        vNoValorFinal = vRespuestas.Where(w => w.CL_VARIABLE.Equals(pClVariable)).First().NO_VALOR2;
                        vNoValor = 2;
                    }
                    else
                    {
                        vNoValor = 2;
                    }
                    break;
                case "c":
                    if (pClVariable.Contains("RES"))
                    {
                        vNoValorFinal = vRespuestas.Where(w => w.CL_VARIABLE.Equals(pClVariable)).First().NO_VALOR3;
                        vNoValor = 3;
                    }
                    else 
                    {
                        vNoValor = 3;
                    }
                        break;
                case "d":
                        if (pClVariable.Contains("RES"))
                        {
                            vNoValorFinal = vRespuestas.Where(w => w.CL_VARIABLE.Equals(pClVariable)).First().NO_VALOR4;
                            vNoValor = 4;
                        }
                        else 
                        {
                            vNoValor = 4;
                        }
                           break;
                default:
                    vNoValor = 0;
                    vNoValorFinal = 0;
                    break;
            }

            if (pClVariable.Contains("RES"))
            {
                vResultado.NO_VALOR = vNoValorFinal;
                vResultado.CL_VARIABLE = pClVariable;
                vResultado.NO_VALOR_RES = vNoValor;
            }
            else 
            {
                vResultado.NO_VALOR = vNoValor;
                vResultado.CL_VARIABLE = pClVariable;
            }
            vPruebaResultado.Add(vResultado);
        }

        public void CargarRespuestas()
        {
            vRespuestas.Add(new E_RESULTADOS_TIVA { CL_VARIABLE = "TIVA_RES_1", NO_VALOR1 = 2, NO_VALOR2 = 3, NO_VALOR3 = 2, NO_VALOR4 = 1 });
            vRespuestas.Add(new E_RESULTADOS_TIVA { CL_VARIABLE = "TIVA_RES_2", NO_VALOR1 = 3, NO_VALOR2 = 3, NO_VALOR3 = 1, NO_VALOR4 = 2 });
            vRespuestas.Add(new E_RESULTADOS_TIVA { CL_VARIABLE = "TIVA_RES_3", NO_VALOR1 = 2, NO_VALOR2 = 3, NO_VALOR3 = 1, NO_VALOR4 = 3 });
            vRespuestas.Add(new E_RESULTADOS_TIVA { CL_VARIABLE = "TIVA_RES_4", NO_VALOR1 = 3, NO_VALOR2 = 2, NO_VALOR3 = 3, NO_VALOR4 = 1 });
            vRespuestas.Add(new E_RESULTADOS_TIVA { CL_VARIABLE = "TIVA_RES_5", NO_VALOR1 = 1, NO_VALOR2 = 2, NO_VALOR3 = 3, NO_VALOR4 = 3 });
            vRespuestas.Add(new E_RESULTADOS_TIVA { CL_VARIABLE = "TIVA_RES_6", NO_VALOR1 = 3, NO_VALOR2 = 1, NO_VALOR3 = 2, NO_VALOR4 = 1 });
            vRespuestas.Add(new E_RESULTADOS_TIVA { CL_VARIABLE = "TIVA_RES_7", NO_VALOR1 = 3, NO_VALOR2 = 2, NO_VALOR3 = 1, NO_VALOR4 = 2 });
            vRespuestas.Add(new E_RESULTADOS_TIVA { CL_VARIABLE = "TIVA_RES_8", NO_VALOR1 = 1, NO_VALOR2 = 1, NO_VALOR3 = 3, NO_VALOR4 = 2 });
            vRespuestas.Add(new E_RESULTADOS_TIVA { CL_VARIABLE = "TIVA_RES_9", NO_VALOR1 = 1, NO_VALOR2 = 3, NO_VALOR3 = 2, NO_VALOR4 = 1 });
            vRespuestas.Add(new E_RESULTADOS_TIVA { CL_VARIABLE = "TIVA_RES_10", NO_VALOR1 = 1, NO_VALOR2 = 3, NO_VALOR3 = 1, NO_VALOR4 = 2 });
            vRespuestas.Add(new E_RESULTADOS_TIVA { CL_VARIABLE = "TIVA_RES_11", NO_VALOR1 = 3, NO_VALOR2 = 1, NO_VALOR3 = 2, NO_VALOR4 = 1 });
            vRespuestas.Add(new E_RESULTADOS_TIVA { CL_VARIABLE = "TIVA_RES_12", NO_VALOR1 = 3, NO_VALOR2 = 2, NO_VALOR3 = 1, NO_VALOR4 = 2 });
            vRespuestas.Add(new E_RESULTADOS_TIVA { CL_VARIABLE = "TIVA_RES_13", NO_VALOR1 = 3, NO_VALOR2 = 2, NO_VALOR3 = 3, NO_VALOR4 = 1 });
            vRespuestas.Add(new E_RESULTADOS_TIVA { CL_VARIABLE = "TIVA_RES_14", NO_VALOR1 = 3, NO_VALOR2 = 2, NO_VALOR3 = 3, NO_VALOR4 = 1 });
            vRespuestas.Add(new E_RESULTADOS_TIVA { CL_VARIABLE = "TIVA_RES_15", NO_VALOR1 = 2, NO_VALOR2 = 1, NO_VALOR3 = 3, NO_VALOR4 = 1 });
            vRespuestas.Add(new E_RESULTADOS_TIVA { CL_VARIABLE = "TIVA_RES_16", NO_VALOR1 = 1, NO_VALOR2 = 2, NO_VALOR3 = 2, NO_VALOR4 = 3 });
            vRespuestas.Add(new E_RESULTADOS_TIVA { CL_VARIABLE = "TIVA_RES_17", NO_VALOR1 = 2, NO_VALOR2 = 3, NO_VALOR3 = 1, NO_VALOR4 = 2 });
            vRespuestas.Add(new E_RESULTADOS_TIVA { CL_VARIABLE = "TIVA_RES_18", NO_VALOR1 = 1, NO_VALOR2 = 3, NO_VALOR3 = 1, NO_VALOR4 = 2 });
            vRespuestas.Add(new E_RESULTADOS_TIVA { CL_VARIABLE = "TIVA_RES_19", NO_VALOR1 = 3, NO_VALOR2 = 1, NO_VALOR3 = 2, NO_VALOR4 = 2 });
            vRespuestas.Add(new E_RESULTADOS_TIVA { CL_VARIABLE = "TIVA_RES_20", NO_VALOR1 = 3, NO_VALOR2 = 2, NO_VALOR3 = 3, NO_VALOR4 = 1 });
        }

        //'TIVA_RES_1' AS CL_VARIABLE_RESULTADO, 2 AS NO_VALOR1, 3 AS NO_VALOR2, 2 AS NO_VALOR3, 1 AS NO_VALOR4 UNION ALL
        //'TIVA_RES_2' AS CL_VARIABLE_RESULTADO, 3 AS NO_VALOR1, 3 AS NO_VALOR2, 1 AS NO_VALOR3, 2 AS NO_VALOR4 UNION ALL
        //'TIVA_RES_3' AS CL_VARIABLE_RESULTADO, 2 AS NO_VALOR1, 3 AS NO_VALOR2, 1 AS NO_VALOR3, 3 AS NO_VALOR4 UNION ALL
        //'TIVA_RES_4' AS CL_VARIABLE_RESULTADO, 3 AS NO_VALOR1, 2 AS NO_VALOR2, 3 AS NO_VALOR3, 1 AS NO_VALOR4 UNION ALL
        //'TIVA_RES_5' AS CL_VARIABLE_RESULTADO, 1 AS NO_VALOR1, 2 AS NO_VALOR2, 3 AS NO_VALOR3, 3 AS NO_VALOR4 UNION ALL
        //'TIVA_RES_6' AS CL_VARIABLE_RESULTADO, 3 AS NO_VALOR1, 1 AS NO_VALOR2, 2 AS NO_VALOR3, 1 AS NO_VALOR4 UNION ALL
        //'TIVA_RES_7' AS CL_VARIABLE_RESULTADO, 3 AS NO_VALOR1, 2 AS NO_VALOR2, 1 AS NO_VALOR3, 2 AS NO_VALOR4 UNION ALL
        //'TIVA_RES_8' AS CL_VARIABLE_RESULTADO, 1 AS NO_VALOR1, 1 AS NO_VALOR2, 3 AS NO_VALOR3, 2 AS NO_VALOR4 UNION ALL
        //'TIVA_RES_9' AS CL_VARIABLE_RESULTADO, 1 AS NO_VALOR1, 3 AS NO_VALOR2, 2 AS NO_VALOR3, 1 AS NO_VALOR4 UNION ALL
        //'TIVA_RES_10' AS CL_VARIABLE_RESULTADO, 1 AS NO_VALOR1, 3 AS NO_VALOR2, 1 AS NO_VALOR3, 2 AS NO_VALOR4 UNION ALL
        //'TIVA_RES_11' AS CL_VARIABLE_RESULTADO, 3 AS NO_VALOR1, 1 AS NO_VALOR2, 2 AS NO_VALOR3, 1 AS NO_VALOR4 UNION ALL
        //'TIVA_RES_12' AS CL_VARIABLE_RESULTADO, 3 AS NO_VALOR1, 2 AS NO_VALOR2, 1 AS NO_VALOR3, 2 AS NO_VALOR4 UNION ALL
        //'TIVA_RES_13' AS CL_VARIABLE_RESULTADO, 3 AS NO_VALOR1, 2 AS NO_VALOR2, 3 AS NO_VALOR3, 1 AS NO_VALOR4 UNION ALL
        //'TIVA_RES_14' AS CL_VARIABLE_RESULTADO, 3 AS NO_VALOR1, 2 AS NO_VALOR2, 3 AS NO_VALOR3, 1 AS NO_VALOR4 UNION ALL
        //'TIVA_RES_15' AS CL_VARIABLE_RESULTADO, 2 AS NO_VALOR1, 1 AS NO_VALOR2, 3 AS NO_VALOR3, 1 AS NO_VALOR4 UNION ALL
        //'TIVA_RES_16' AS CL_VARIABLE_RESULTADO, 1 AS NO_VALOR1, 2 AS NO_VALOR2, 2 AS NO_VALOR3, 3 AS NO_VALOR4 UNION ALL
        //'TIVA_RES_17' AS CL_VARIABLE_RESULTADO, 2 AS NO_VALOR1, 3 AS NO_VALOR2, 1 AS NO_VALOR3, 2 AS NO_VALOR4 UNION ALL
        //'TIVA_RES_18' AS CL_VARIABLE_RESULTADO, 1 AS NO_VALOR1, 3 AS NO_VALOR2, 1 AS NO_VALOR3, 2 AS NO_VALOR4 UNION ALL
        //'TIVA_RES_19' AS CL_VARIABLE_RESULTADO, 3 AS NO_VALOR1, 1 AS NO_VALOR2, 2 AS NO_VALOR3, 2 AS NO_VALOR4 UNION ALL
        //'TIVA_RES_20' AS CL_VARIABLE_RESULTADO, 3 AS NO_VALOR1, 2 AS NO_VALOR2, 3 AS NO_VALOR3, 1 AS NO_VALOR4 

        [Serializable]
        public class E_RESULTADOS_TIVA 
        {
            public string CL_VARIABLE { get; set; }
            public int NO_VALOR1 { get; set; }
            public int NO_VALOR2 { get; set; }
            public int NO_VALOR3 { get; set; }
            public int NO_VALOR4 { get; set; }
        }
    }
}