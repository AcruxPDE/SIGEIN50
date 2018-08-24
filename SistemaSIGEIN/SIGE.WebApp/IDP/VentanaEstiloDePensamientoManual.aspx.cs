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
    public partial class VentanaEstiloDePensamientoManual : System.Web.UI.Page
    {
        private static E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private List<E_PRUEBA_RESULTADO> vResultados
        {
            get { return (List<E_PRUEBA_RESULTADO>)ViewState["vsResultadoPrueba"]; }
            set { ViewState["vsResultadoPrueba"] = value; }
        }

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

                vResultados = new List<E_PRUEBA_RESULTADO>();
                
            }            
        }

        public void CargarRespuestas(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> pResultados)
        {
            if (pResultados.Count > 0)
            {

                //SECCION 1
                radTxtPreg1Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A1")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A1")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A1")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg1Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A2")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A2")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A2")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg1Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A3")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A3")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A3")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg1Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A4")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A4")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A4")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg2Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A5")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A5")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A5")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg2Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A6")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A6")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A6")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg2Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A7")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A7")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A7")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg2Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A8")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A8")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A8")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg3Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A9")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A9")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A9")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg3Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A10")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A10")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A10")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg3Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A11")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A11")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A11")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg3Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A12")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A12")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A12")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg4Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A13")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A13")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A13")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg4Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A14")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A14")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A14")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg4Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A15")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A15")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A15")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg4Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A16")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A16")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A16")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg5Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A17")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A17")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A17")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg5Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A18")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A18")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A18")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg5Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A19")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A19")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A19")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg5Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A20")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A20")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A20")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                
                //SECCION 2
                radTxtPreg6Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B1")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B1")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B1")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg6Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B2")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B2")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B2")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg6Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B3")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B3")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B3")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg6Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B4")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B4")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B4")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg7Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B5")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B5")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B5")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg7Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B6")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B6")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B6")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg7Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B7")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B7")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B7")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg7Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B8")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B8")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B8")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg8Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B9")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B9")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B9")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg8Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B10")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B10")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B10")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg8Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B11")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B11")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B11")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg8Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B12")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B12")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B12")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg9Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B13")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B13")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B13")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg9Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B14")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B14")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B14")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg9Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B15")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B15")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B15")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg9Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B16")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B16")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B16")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg10Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B17")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B17")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B17")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg10Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B18")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B18")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B18")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg10Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B19")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B19")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B19")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg10Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B20")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B20")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B20")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                //SECCION 3
                radTxtPreg11Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C1")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C1")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C1")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg11Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C2")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C2")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C2")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg12Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C3")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C3")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C3")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg12Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C4")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C4")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C4")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg13Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C5")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C5")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C5")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg13Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C6")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C6")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C6")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg14Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C7")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C7")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C7")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg14Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C8")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C8")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C8")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg15Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C9")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C9")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C9")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg15Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C10")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C10")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C10")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg16Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C11")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C11")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C11")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg16Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C12")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C12")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C12")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg17Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C13")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C13")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C13")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg17Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C14")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C14")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C14")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg18Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C15")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C15")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C15")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg18Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C16")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C16")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C16")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg19Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C17")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C17")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C17")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg19Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C18")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C18")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C18")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg20Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C19")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C19")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C19")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg20Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C20")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C20")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C20")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                //if (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A1")).FirstOrDefault().NB_RESULTADO == null)
                //    btnTerminar.Enabled = false;
            }
        }

        public void CargarRespuestasAplicacion(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> pResultados)
        {
            if (pResultados.Count > 0)
            {

                //SECCION 1
                radTxtPreg1Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0001")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0001")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0001")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg1Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0002")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0002")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0002")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg1Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0003")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0003")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0003")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg1Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0004")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0004")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0004")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg2Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0005")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0005")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0005")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg2Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0006")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0006")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0006")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg2Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0007")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0007")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0007")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg2Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0008")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0008")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0008")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg3Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0009")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0009")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0009")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg3Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0010")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0010")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0010")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg3Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0011")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0011")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0011")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg3Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0012")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0012")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0012")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg4Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0013")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0013")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0013")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg4Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0014")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0014")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0014")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg4Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0015")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0015")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0015")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg4Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0016")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0016")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0016")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg5Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0017")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0017")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0017")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg5Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0018")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0018")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0018")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg5Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0019")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0019")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0019")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg5Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0020")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0020")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-A-0020")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                //SECCION 2
                radTxtPreg6Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0001")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0001")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0001")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg6Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0002")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0002")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0002")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg6Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0003")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0003")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0003")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg6Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0004")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0004")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0004")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg7Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0005")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0005")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0005")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg7Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0006")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0006")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0006")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg7Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0007")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0007")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0007")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg7Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0008")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0008")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0008")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg8Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0009")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0009")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0009")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg8Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0010")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0010")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0010")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg8Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0011")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0011")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0011")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg8Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0012")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0012")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0012")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg9Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0013")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0013")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0013")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg9Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0014")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0014")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0014")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg9Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0015")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0015")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0015")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg9Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0016")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0016")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0016")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg10Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0017")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0017")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0017")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg10Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0018")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0018")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0018")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg10Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0019")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0019")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0019")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg10Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0020")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0020")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-B-0020")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                //SECCION 3
                radTxtPreg11Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0001")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0001")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0001")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg11Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0002")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0002")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0002")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg12Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0003")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0003")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0003")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg12Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0004")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0004")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0004")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg13Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0005")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0005")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0005")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg13Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0006")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0006")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0006")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg14Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0007")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0007")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0007")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg14Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0008")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0008")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0008")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg15Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0009")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0009")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0009")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg15Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0010")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0010")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0010")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg16Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0011")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0011")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0011")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg16Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0012")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0012")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0012")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg17Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0013")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0013")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0013")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg17Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0014")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0014")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0014")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg18Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0015")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0015")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0015")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg18Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0016")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0016")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0016")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg19Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0017")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0017")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0017")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg19Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0018")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0018")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0018")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg20Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0019")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0019")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0019")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg20Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0020")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0020")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("PENSAMIENTO-C-0020")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
            }
        }


        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            if (validarCamposVacios())
            {


                PruebasNegocio nKprueba = new PruebasNegocio();
                SPE_OBTIENE_K_PRUEBA_Result vPruebaTerminada = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();
                vPruebaTerminada.FE_TERMINO = DateTime.Now;
                vPruebaTerminada.FE_INICIO = DateTime.Now;
                vPruebaTerminada.CL_ESTADO = E_ESTADO_PRUEBA.TERMINADA.ToString();
                if (vPruebaTerminada.NB_TIPO_PRUEBA == "APLICACION")
                {
                    vPruebaTerminada.NB_TIPO_PRUEBA = "APLICACION";
                    E_RESULTADO vResultado = nKprueba.InsertaActualiza_K_PRUEBA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
                    SaveTestAplicacion();
                }
                else
                {
                    vPruebaTerminada.NB_TIPO_PRUEBA = "MANUAL";
                    E_RESULTADO vResultado = nKprueba.InsertaActualiza_K_PRUEBA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
                    SaveTest();
                }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "No ha ingresado todos los valores.", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, "");
            }
        }

        public void SaveTest()
        {          

            String vPENSAMIENTO_A_0001 = radTxtPreg1Resp1.Text;
            BackQuestionObject("PENSAMIENTO_RES_A1", vPENSAMIENTO_A_0001);

            String vPENSAMIENTO_A_0002 = radTxtPreg1Resp2.Text; ;
            BackQuestionObject("PENSAMIENTO_RES_A2", vPENSAMIENTO_A_0002);

            String vPENSAMIENTO_A_0003 = radTxtPreg1Resp3.Text; ;
            BackQuestionObject("PENSAMIENTO_RES_A3", vPENSAMIENTO_A_0003);

            String vPENSAMIENTO_A_0004 = radTxtPreg1Resp4.Text; ;
            BackQuestionObject("PENSAMIENTO_RES_A4", vPENSAMIENTO_A_0004);

            String vPENSAMIENTO_A_0005 = radTxtPreg2Resp1.Text; ;
            BackQuestionObject("PENSAMIENTO_RES_A5", vPENSAMIENTO_A_0005);

            String vPENSAMIENTO_A_0006 = radTxtPreg2Resp2.Text; ;
            BackQuestionObject("PENSAMIENTO_RES_A6", vPENSAMIENTO_A_0006);

            String vPENSAMIENTO_A_0007 = radTxtPreg2Resp3.Text; ;
            BackQuestionObject("PENSAMIENTO_RES_A7", vPENSAMIENTO_A_0007);

            String vPENSAMIENTO_A_0008 = radTxtPreg2Resp4.Text;
            BackQuestionObject("PENSAMIENTO_RES_A8", vPENSAMIENTO_A_0008);

            String vPENSAMIENTO_A_0009 = radTxtPreg3Resp1.Text;
            BackQuestionObject("PENSAMIENTO_RES_A9", vPENSAMIENTO_A_0009);

            String vPENSAMIENTO_A_0010 = radTxtPreg3Resp2.Text;
            BackQuestionObject("PENSAMIENTO_RES_A10", vPENSAMIENTO_A_0010);

            String vPENSAMIENTO_A_0011 = radTxtPreg3Resp3.Text;
            BackQuestionObject("PENSAMIENTO_RES_A11", vPENSAMIENTO_A_0011);

            String vPENSAMIENTO_A_0012 = radTxtPreg3Resp4.Text;
            BackQuestionObject("PENSAMIENTO_RES_A12", vPENSAMIENTO_A_0012);

            String vPENSAMIENTO_A_0013 = radTxtPreg4Resp1.Text;
            BackQuestionObject("PENSAMIENTO_RES_A13", vPENSAMIENTO_A_0013);

            String vPENSAMIENTO_A_0014 = radTxtPreg4Resp2.Text;
            BackQuestionObject("PENSAMIENTO_RES_A14", vPENSAMIENTO_A_0014);

            String vPENSAMIENTO_A_0015 = radTxtPreg4Resp3.Text;
            BackQuestionObject("PENSAMIENTO_RES_A15", vPENSAMIENTO_A_0015);

            String vPENSAMIENTO_A_0016 = radTxtPreg4Resp4.Text;
            BackQuestionObject("PENSAMIENTO_RES_A16", vPENSAMIENTO_A_0016);

            String vPENSAMIENTO_A_0017 = radTxtPreg5Resp1.Text;
            BackQuestionObject("PENSAMIENTO_RES_A17", vPENSAMIENTO_A_0017);

            String vPENSAMIENTO_A_0018 = radTxtPreg5Resp2.Text;
            BackQuestionObject("PENSAMIENTO_RES_A18", vPENSAMIENTO_A_0018);

            String vPENSAMIENTO_A_0019 = radTxtPreg5Resp3.Text;
            BackQuestionObject("PENSAMIENTO_RES_A19", vPENSAMIENTO_A_0019);

            String vPENSAMIENTO_A_0020 = radTxtPreg5Resp4.Text;
            BackQuestionObject("PENSAMIENTO_RES_A20", vPENSAMIENTO_A_0020);

            ///////////////////////////SEGMENTO 2//////////////////////////////////

            String vPENSAMIENTO_B_0001 = radTxtPreg6Resp1.Text;
            BackQuestionObject("PENSAMIENTO_RES_B1", vPENSAMIENTO_B_0001);

            String vPENSAMIENTO_B_0002 = radTxtPreg6Resp2.Text;
            BackQuestionObject("PENSAMIENTO_RES_B2", vPENSAMIENTO_B_0002);

            String vPENSAMIENTO_B_0003 = radTxtPreg6Resp3.Text;
            BackQuestionObject("PENSAMIENTO_RES_B3", vPENSAMIENTO_B_0003);

            String vPENSAMIENTO_B_0004 = radTxtPreg6Resp4.Text;
            BackQuestionObject("PENSAMIENTO_RES_B4", vPENSAMIENTO_B_0004);

            String vPENSAMIENTO_B_0005 = radTxtPreg7Resp1.Text;
            BackQuestionObject("PENSAMIENTO_RES_B5", vPENSAMIENTO_B_0005);

            String vPENSAMIENTO_B_0006 = radTxtPreg7Resp2.Text;
            BackQuestionObject("PENSAMIENTO_RES_B6", vPENSAMIENTO_B_0006);

            String vPENSAMIENTO_B_0007 = radTxtPreg7Resp3.Text;
            BackQuestionObject("PENSAMIENTO_RES_B7", vPENSAMIENTO_B_0007);

            String vPENSAMIENTO_B_0008 = radTxtPreg7Resp4.Text;
            BackQuestionObject("PENSAMIENTO_RES_B8", vPENSAMIENTO_B_0008);

            String vPENSAMIENTO_B_0009 = radTxtPreg8Resp1.Text;
            BackQuestionObject("PENSAMIENTO_RES_B9", vPENSAMIENTO_B_0009);

            String vPENSAMIENTO_B_0010 = radTxtPreg8Resp2.Text;
            BackQuestionObject("PENSAMIENTO_RES_B10", vPENSAMIENTO_B_0010);

            String vPENSAMIENTO_B_0011 = radTxtPreg8Resp3.Text;
            BackQuestionObject("PENSAMIENTO_RES_B11", vPENSAMIENTO_B_0011);

            String vPENSAMIENTO_B_0012 = radTxtPreg8Resp4.Text;
            BackQuestionObject("PENSAMIENTO_RES_B12", vPENSAMIENTO_B_0012);

            String vPENSAMIENTO_B_0013 = radTxtPreg9Resp1.Text;
            BackQuestionObject("PENSAMIENTO_RES_B13", vPENSAMIENTO_B_0013);

            String vPENSAMIENTO_B_0014 = radTxtPreg9Resp2.Text;
            BackQuestionObject("PENSAMIENTO_RES_B14", vPENSAMIENTO_B_0014);

            String vPENSAMIENTO_B_0015 = radTxtPreg9Resp3.Text;
            BackQuestionObject("PENSAMIENTO_RES_B15", vPENSAMIENTO_B_0015);

            String vPENSAMIENTO_B_0016 = radTxtPreg9Resp4.Text;
            BackQuestionObject("PENSAMIENTO_RES_B16", vPENSAMIENTO_B_0016);

            String vPENSAMIENTO_B_0017 = radTxtPreg10Resp1.Text;
            BackQuestionObject("PENSAMIENTO_RES_B17", vPENSAMIENTO_B_0017);

            String vPENSAMIENTO_B_0018 = radTxtPreg10Resp2.Text;
            BackQuestionObject("PENSAMIENTO_RES_B18", vPENSAMIENTO_B_0018);

            String vPENSAMIENTO_B_0019 = radTxtPreg10Resp3.Text;
            BackQuestionObject("PENSAMIENTO_RES_B19", vPENSAMIENTO_B_0019);

            String vPENSAMIENTO_B_0020 = radTxtPreg10Resp4.Text;
            BackQuestionObject("PENSAMIENTO_RES_B20", vPENSAMIENTO_B_0020);

            //////////////////////////////SEGMENTO 3////////////////////////////////

            String vPENSAMIENTO_C_0001 = radTxtPreg11Resp1.Text;
            BackQuestionObject("PENSAMIENTO_RES_C1", vPENSAMIENTO_C_0001);

            String vPENSAMIENTO_C_0002 = radTxtPreg11Resp2.Text;
            BackQuestionObject("PENSAMIENTO_RES_C2", vPENSAMIENTO_C_0002);

            String vPENSAMIENTO_C_0003 = radTxtPreg12Resp1.Text;
            BackQuestionObject("PENSAMIENTO_RES_C3", vPENSAMIENTO_C_0003);

            String vPENSAMIENTO_C_0004 = radTxtPreg12Resp2.Text;
            BackQuestionObject("PENSAMIENTO_RES_C4", vPENSAMIENTO_C_0004);

            String vPENSAMIENTO_C_0005 = radTxtPreg13Resp1.Text;
            BackQuestionObject("PENSAMIENTO_RES_C5", vPENSAMIENTO_C_0005);

            String vPENSAMIENTO_C_0006 = radTxtPreg13Resp2.Text;
            BackQuestionObject("PENSAMIENTO_RES_C6", vPENSAMIENTO_C_0006);

            String vPENSAMIENTO_C_0007 = radTxtPreg14Resp1.Text;
            BackQuestionObject("PENSAMIENTO_RES_C7", vPENSAMIENTO_C_0007);

            String vPENSAMIENTO_C_0008 = radTxtPreg14Resp2.Text;
            BackQuestionObject("PENSAMIENTO_RES_C8", vPENSAMIENTO_C_0008);

            String vPENSAMIENTO_C_0009 = radTxtPreg15Resp1.Text;
            BackQuestionObject("PENSAMIENTO_RES_C9", vPENSAMIENTO_C_0009);

            String vPENSAMIENTO_C_0010 = radTxtPreg15Resp2.Text;
            BackQuestionObject("PENSAMIENTO_RES_C10", vPENSAMIENTO_C_0010);

            String vPENSAMIENTO_C_0011 = radTxtPreg16Resp1.Text;
            BackQuestionObject("PENSAMIENTO_RES_C11", vPENSAMIENTO_C_0011);

            String vPENSAMIENTO_C_0012 = radTxtPreg16Resp2.Text;
            BackQuestionObject("PENSAMIENTO_RES_C12", vPENSAMIENTO_C_0012);

            String vPENSAMIENTO_C_0013 = radTxtPreg17Resp1.Text;
            BackQuestionObject("PENSAMIENTO_RES_C13", vPENSAMIENTO_C_0013);

            String vPENSAMIENTO_C_0014 = radTxtPreg17Resp2.Text;
            BackQuestionObject("PENSAMIENTO_RES_C14", vPENSAMIENTO_C_0014);

            String vPENSAMIENTO_C_0015 = radTxtPreg18Resp1.Text;
            BackQuestionObject("PENSAMIENTO_RES_C15", vPENSAMIENTO_C_0015);

            String vPENSAMIENTO_C_0016 = radTxtPreg18Resp2.Text;
            BackQuestionObject("PENSAMIENTO_RES_C16", vPENSAMIENTO_C_0016);

            String vPENSAMIENTO_C_0017 = radTxtPreg19Resp1.Text;
            BackQuestionObject("PENSAMIENTO_RES_C17", vPENSAMIENTO_C_0017);

            String vPENSAMIENTO_C_0018 = radTxtPreg19Resp2.Text;
            BackQuestionObject("PENSAMIENTO_RES_C18", vPENSAMIENTO_C_0018);

            String vPENSAMIENTO_C_0019 = radTxtPreg20Resp1.Text;
            BackQuestionObject("PENSAMIENTO_RES_C19", vPENSAMIENTO_C_0019);

            String vPENSAMIENTO_C_0020 = radTxtPreg20Resp2.Text;
            BackQuestionObject("PENSAMIENTO_RES_C20", vPENSAMIENTO_C_0020);

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
                E_RESULTADO vResultado = negRes.insertaResultadosPensamiento(RESPUESTAS.ToString(), null, vIdPrueba, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "CloseTest");
            }
        }

        public void SaveTestAplicacion()
        {
            CuestionariosNegocio nPreguntas = new CuestionariosNegocio();
            vRespuestas = new List<E_PREGUNTA>();
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

            String vPENSAMIENTO_A_0001 = radTxtPreg1Resp1.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-A-0001", vPENSAMIENTO_A_0001);

            String vPENSAMIENTO_A_0002 = radTxtPreg1Resp2.Text; ;
            BackQuestionObjectAplicacion("PENSAMIENTO-A-0002", vPENSAMIENTO_A_0002);

            String vPENSAMIENTO_A_0003 = radTxtPreg1Resp3.Text; ;
            BackQuestionObjectAplicacion("PENSAMIENTO-A-0003", vPENSAMIENTO_A_0003);

            String vPENSAMIENTO_A_0004 = radTxtPreg1Resp4.Text; ;
            BackQuestionObjectAplicacion("PENSAMIENTO-A-0004", vPENSAMIENTO_A_0004);

            String vPENSAMIENTO_A_0005 = radTxtPreg2Resp1.Text; ;
            BackQuestionObjectAplicacion("PENSAMIENTO-A-0005", vPENSAMIENTO_A_0005);

            String vPENSAMIENTO_A_0006 = radTxtPreg2Resp2.Text; ;
            BackQuestionObjectAplicacion("PENSAMIENTO-A-0006", vPENSAMIENTO_A_0006);

            String vPENSAMIENTO_A_0007 = radTxtPreg2Resp3.Text; ;
            BackQuestionObjectAplicacion("PENSAMIENTO-A-0007", vPENSAMIENTO_A_0007);

            String vPENSAMIENTO_A_0008 = radTxtPreg2Resp4.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-A-0008", vPENSAMIENTO_A_0008);

            String vPENSAMIENTO_A_0009 = radTxtPreg3Resp1.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-A-0009", vPENSAMIENTO_A_0009);

            String vPENSAMIENTO_A_0010 = radTxtPreg3Resp2.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-A-0010", vPENSAMIENTO_A_0010);

            String vPENSAMIENTO_A_0011 = radTxtPreg3Resp3.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-A-0011", vPENSAMIENTO_A_0011);

            String vPENSAMIENTO_A_0012 = radTxtPreg3Resp4.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-A-0012", vPENSAMIENTO_A_0012);

            String vPENSAMIENTO_A_0013 = radTxtPreg4Resp1.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-A-0013", vPENSAMIENTO_A_0013);

            String vPENSAMIENTO_A_0014 = radTxtPreg4Resp2.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-A-0014", vPENSAMIENTO_A_0014);

            String vPENSAMIENTO_A_0015 = radTxtPreg4Resp3.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-A-0015", vPENSAMIENTO_A_0015);

            String vPENSAMIENTO_A_0016 = radTxtPreg4Resp4.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-A-0016", vPENSAMIENTO_A_0016);

            String vPENSAMIENTO_A_0017 = radTxtPreg5Resp1.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-A-0017", vPENSAMIENTO_A_0017);

            String vPENSAMIENTO_A_0018 = radTxtPreg5Resp2.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-A-0018", vPENSAMIENTO_A_0018);

            String vPENSAMIENTO_A_0019 = radTxtPreg5Resp3.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-A-0019", vPENSAMIENTO_A_0019);

            String vPENSAMIENTO_A_0020 = radTxtPreg5Resp4.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-A-0020", vPENSAMIENTO_A_0020);

            ///////////////////////////SEGMENTO 2//////////////////////////////////

            String vPENSAMIENTO_B_0001 = radTxtPreg6Resp1.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-B-0001", vPENSAMIENTO_B_0001);

            String vPENSAMIENTO_B_0002 = radTxtPreg6Resp2.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-B-0002", vPENSAMIENTO_B_0002);

            String vPENSAMIENTO_B_0003 = radTxtPreg6Resp3.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-B-0003", vPENSAMIENTO_B_0003);

            String vPENSAMIENTO_B_0004 = radTxtPreg6Resp4.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-B-0004", vPENSAMIENTO_B_0004);

            String vPENSAMIENTO_B_0005 = radTxtPreg7Resp1.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-B-0005", vPENSAMIENTO_B_0005);

            String vPENSAMIENTO_B_0006 = radTxtPreg7Resp2.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-B-0006", vPENSAMIENTO_B_0006);

            String vPENSAMIENTO_B_0007 = radTxtPreg7Resp3.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-B-0007", vPENSAMIENTO_B_0007);

            String vPENSAMIENTO_B_0008 = radTxtPreg7Resp4.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-B-0008", vPENSAMIENTO_B_0008);

            String vPENSAMIENTO_B_0009 = radTxtPreg8Resp1.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-B-0009", vPENSAMIENTO_B_0009);

            String vPENSAMIENTO_B_0010 = radTxtPreg8Resp2.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-B-0010", vPENSAMIENTO_B_0010);

            String vPENSAMIENTO_B_0011 = radTxtPreg8Resp3.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-B-0011", vPENSAMIENTO_B_0011);

            String vPENSAMIENTO_B_0012 = radTxtPreg8Resp4.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-B-0012", vPENSAMIENTO_B_0012);

            String vPENSAMIENTO_B_0013 = radTxtPreg9Resp1.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-B-0013", vPENSAMIENTO_B_0013);

            String vPENSAMIENTO_B_0014 = radTxtPreg9Resp2.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-B-0014", vPENSAMIENTO_B_0014);

            String vPENSAMIENTO_B_0015 = radTxtPreg9Resp3.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-B-0015", vPENSAMIENTO_B_0015);

            String vPENSAMIENTO_B_0016 = radTxtPreg9Resp4.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-B-0016", vPENSAMIENTO_B_0016);

            String vPENSAMIENTO_B_0017 = radTxtPreg10Resp1.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-B-0017", vPENSAMIENTO_B_0017);

            String vPENSAMIENTO_B_0018 = radTxtPreg10Resp2.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-B-0018", vPENSAMIENTO_B_0018);

            String vPENSAMIENTO_B_0019 = radTxtPreg10Resp3.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-B-0019", vPENSAMIENTO_B_0019);

            String vPENSAMIENTO_B_0020 = radTxtPreg10Resp4.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-B-0020", vPENSAMIENTO_B_0020);

            //////////////////////////////SEGMENTO 3////////////////////////////////

            String vPENSAMIENTO_C_0001 = radTxtPreg11Resp1.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-C-0001", vPENSAMIENTO_C_0001);

            String vPENSAMIENTO_C_0002 = radTxtPreg11Resp2.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-C-0002", vPENSAMIENTO_C_0002);

            String vPENSAMIENTO_C_0003 = radTxtPreg12Resp1.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-C-0003", vPENSAMIENTO_C_0003);

            String vPENSAMIENTO_C_0004 = radTxtPreg12Resp2.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-C-0004", vPENSAMIENTO_C_0004);

            String vPENSAMIENTO_C_0005 = radTxtPreg13Resp1.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-C-0005", vPENSAMIENTO_C_0005);

            String vPENSAMIENTO_C_0006 = radTxtPreg13Resp2.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-C-0006", vPENSAMIENTO_C_0006);

            String vPENSAMIENTO_C_0007 = radTxtPreg14Resp1.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-C-0007", vPENSAMIENTO_C_0007);

            String vPENSAMIENTO_C_0008 = radTxtPreg14Resp2.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-C-0008", vPENSAMIENTO_C_0008);

            String vPENSAMIENTO_C_0009 = radTxtPreg15Resp1.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-C-0009", vPENSAMIENTO_C_0009);

            String vPENSAMIENTO_C_0010 = radTxtPreg15Resp2.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-C-0010", vPENSAMIENTO_C_0010);

            String vPENSAMIENTO_C_0011 = radTxtPreg16Resp1.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-C-0011", vPENSAMIENTO_C_0011);

            String vPENSAMIENTO_C_0012 = radTxtPreg16Resp2.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-C-0012", vPENSAMIENTO_C_0012);

            String vPENSAMIENTO_C_0013 = radTxtPreg17Resp1.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-C-0013", vPENSAMIENTO_C_0013);

            String vPENSAMIENTO_C_0014 = radTxtPreg17Resp2.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-C-0014", vPENSAMIENTO_C_0014);

            String vPENSAMIENTO_C_0015 = radTxtPreg18Resp1.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-C-0015", vPENSAMIENTO_C_0015);

            String vPENSAMIENTO_C_0016 = radTxtPreg18Resp2.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-C-0016", vPENSAMIENTO_C_0016);

            String vPENSAMIENTO_C_0017 = radTxtPreg19Resp1.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-C-0017", vPENSAMIENTO_C_0017);

            String vPENSAMIENTO_C_0018 = radTxtPreg19Resp2.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-C-0018", vPENSAMIENTO_C_0018);

            String vPENSAMIENTO_C_0019 = radTxtPreg20Resp1.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-C-0019", vPENSAMIENTO_C_0019);

            String vPENSAMIENTO_C_0020 = radTxtPreg20Resp2.Text;
            BackQuestionObjectAplicacion("PENSAMIENTO-C-0020", vPENSAMIENTO_C_0020);

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

            E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vObjetoPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: "PENSAMIENTO", usuario: vClUsuario, programa: vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "CloseTest");

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

        public void BackQuestionObjectAplicacion(string pclPregunta, string pnbRespuesta)
        {
            var vPregunta = vRespuestas.Where(x => x.CL_PREGUNTA.Equals(pclPregunta)).FirstOrDefault();
            if (vPregunta != null)
            {
                decimal vNoValor;
                vPregunta.NB_RESPUESTA = pnbRespuesta;
                vPregunta.NO_VALOR_RESPUESTA = (vNoValor = (pnbRespuesta != "") ? decimal.Parse(pnbRespuesta) : 0);
            }
        }

        private bool validarCamposVacios()
        {
            bool continua = false;
            if( !String.IsNullOrEmpty(radTxtPreg1Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg1Resp2.Text) && !String.IsNullOrEmpty(radTxtPreg1Resp3.Text) && !String.IsNullOrEmpty(radTxtPreg1Resp4.Text) && 
                !String.IsNullOrEmpty(radTxtPreg2Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg2Resp2.Text) && !String.IsNullOrEmpty(radTxtPreg2Resp3.Text) && !String.IsNullOrEmpty(radTxtPreg2Resp4.Text) && 
                !String.IsNullOrEmpty(radTxtPreg3Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg3Resp2.Text) && !String.IsNullOrEmpty(radTxtPreg3Resp3.Text) && !String.IsNullOrEmpty(radTxtPreg3Resp4.Text) && 
                !String.IsNullOrEmpty(radTxtPreg4Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg4Resp2.Text) && !String.IsNullOrEmpty(radTxtPreg4Resp3.Text) && !String.IsNullOrEmpty(radTxtPreg4Resp4.Text) &&
                !String.IsNullOrEmpty(radTxtPreg5Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg5Resp2.Text) && !String.IsNullOrEmpty(radTxtPreg5Resp3.Text) && !String.IsNullOrEmpty(radTxtPreg5Resp4.Text) &&
                !String.IsNullOrEmpty(radTxtPreg6Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg6Resp2.Text) && !String.IsNullOrEmpty(radTxtPreg6Resp3.Text) && !String.IsNullOrEmpty(radTxtPreg6Resp4.Text) &&
                !String.IsNullOrEmpty(radTxtPreg7Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg7Resp2.Text) && !String.IsNullOrEmpty(radTxtPreg7Resp3.Text) && !String.IsNullOrEmpty(radTxtPreg7Resp4.Text) &&
                !String.IsNullOrEmpty(radTxtPreg8Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg8Resp2.Text) && !String.IsNullOrEmpty(radTxtPreg8Resp3.Text) && !String.IsNullOrEmpty(radTxtPreg8Resp4.Text) &&
                !String.IsNullOrEmpty(radTxtPreg9Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg9Resp2.Text) && !String.IsNullOrEmpty(radTxtPreg9Resp3.Text) && !String.IsNullOrEmpty(radTxtPreg9Resp4.Text) &&
                !String.IsNullOrEmpty(radTxtPreg10Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg10Resp2.Text) && !String.IsNullOrEmpty(radTxtPreg10Resp3.Text) && !String.IsNullOrEmpty(radTxtPreg10Resp4.Text) &&
                !String.IsNullOrEmpty(radTxtPreg11Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg11Resp2.Text) && !String.IsNullOrEmpty(radTxtPreg12Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg12Resp2.Text) &&
                !String.IsNullOrEmpty(radTxtPreg13Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg13Resp2.Text) && !String.IsNullOrEmpty(radTxtPreg14Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg14Resp2.Text) &&
                !String.IsNullOrEmpty(radTxtPreg15Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg15Resp2.Text) && !String.IsNullOrEmpty(radTxtPreg16Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg16Resp2.Text) &&
                !String.IsNullOrEmpty(radTxtPreg17Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg17Resp2.Text) && !String.IsNullOrEmpty(radTxtPreg18Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg18Resp2.Text) &&
                !String.IsNullOrEmpty(radTxtPreg19Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg19Resp2.Text) && !String.IsNullOrEmpty(radTxtPreg20Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg20Resp2.Text)
                )
            {
                continua = true;
            }
            else
            {
                continua = false;
            }

            return continua;
        }
    }
}