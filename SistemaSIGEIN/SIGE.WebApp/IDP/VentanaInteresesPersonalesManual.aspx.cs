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

namespace SIGE.WebApp.IDP
{
    public partial class VentanaInteresesPersonalesManual : System.Web.UI.Page
    {
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private List<E_PRUEBA_RESULTADO> vPruebaResultado
        {
            get { return (List<E_PRUEBA_RESULTADO>)ViewState["vsPruebaResultado"]; }
            set { ViewState["vsPruebaResultado"] = value; }
        }

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
                    vClTokenExterno = new Guid(Request.QueryString["T"]);
                    //E_RESULTADO vObjetoPrueba = nKprueba.INICIAR_K_PRUEBA(pIdPrueba: vIdPrueba, pFeInicio: DateTime.Now, pClTokenExterno: vClTokenExterno, usuario: vClUsuario, programa: vNbPrograma);
                    //if (vObjetoPrueba.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.ERROR)
                    //{
                        PruebasNegocio nPruebas = new PruebasNegocio();
                        var prueba = nPruebas.Obtener_RESULTADO_PRUEBA(pClTokenExterno: vClTokenExterno, pIdPrueba: vIdPrueba).ToList();
                        var vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClTokenExterno).FirstOrDefault();

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

                //GRUPO 1
                radTxtPreg1Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_A1")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_A1")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_A1")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg1Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_A2")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_A2")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_A2")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg1Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_A3")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_A3")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_A3")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg1Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_A4")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_A4")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_A4")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg1Resp5.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_A5")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_A5")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_A5")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg1Resp6.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_A6")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_A6")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_A6")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                //GRUPO 2
                radTxtPreg2Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_B1")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_B1")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_B1")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg2Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_B2")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_B2")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_B2")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg2Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_B3")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_B3")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_B3")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg2Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_B4")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_B4")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_B4")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg2Resp5.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_B5")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_B5")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_B5")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg2Resp6.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_B6")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_B6")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_B6")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                //GRUPO 3
                radTxtPreg3Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_C1")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_C1")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_C1")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg3Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_C2")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_C2")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_C2")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg3Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_C3")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_C3")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_C3")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg3Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_C4")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_C4")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_C4")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg3Resp5.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_C5")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_C5")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_C5")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg3Resp6.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_C6")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_C6")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_C6")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                //GRUPO 4
                radTxtPreg4Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_D1")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_D1")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_D1")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg4Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_D2")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_D2")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_D2")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg4Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_D3")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_D3")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_D3")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg4Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_D4")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_D4")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_D4")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg4Resp5.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_D5")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_D5")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_D5")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg4Resp6.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_D6")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_D6")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_D6")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                //GRUPO 5
                radTxtPreg5Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_E1")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_E1")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_E1")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg5Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_E2")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_E2")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_E2")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg5Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_E3")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_E3")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_E3")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg5Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_E4")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_E4")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_E4")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg5Resp5.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_E5")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_E5")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_E5")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg5Resp6.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_E6")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_E6")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_E6")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                //GRUPO 6
                radTxtPreg6Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_F1")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_F1")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_F1")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg6Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_F2")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_F2")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_F2")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg6Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_F3")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_F3")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_F3")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg6Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_F4")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_F4")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_F4")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg6Resp5.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_F5")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_F5")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_F5")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg6Resp6.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_F6")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_F6")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_F6")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                //GRUPO 7
                radTxtPreg7Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_G1")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_G1")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_G1")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg7Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_G2")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_G2")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_G2")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg7Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_G3")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_G3")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_G3")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg7Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_G4")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_G4")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_G4")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg7Resp5.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_G5")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_G5")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_G5")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg7Resp6.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_G6")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_G6")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_G6")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                //GRUPO 
                radTxtPreg8Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_H1")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_H1")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_H1")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg8Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_H2")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_H2")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_H2")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg8Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_H3")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_H3")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_H3")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg8Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_H4")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_H4")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_H4")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg8Resp5.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_H5")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_H5")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_H5")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg8Resp6.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_H6")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_H6")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_H6")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                //GRUPO 9
                radTxtPreg9Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_I1")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_I1")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_I1")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg9Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_I2")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_I2")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_I2")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg9Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_I3")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_I3")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_I3")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg9Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_I4")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_I4")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_I4")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg9Resp5.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_I5")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_I5")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_I5")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg9Resp6.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_I6")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_I6")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_I6")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                //GRUPO 10
                radTxtPreg10Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_J1")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_J1")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_J1")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg10Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_J2")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_J2")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_J2")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg10Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_J3")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_J3")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_J3")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg10Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_J4")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_J4")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_J4")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg10Resp5.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_J5")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_J5")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_J5")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg10Resp6.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_J6")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_J6")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_J6")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
            
                //if(pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES_RES_A1")).FirstOrDefault() == null)
                //    btnTerminar.Enabled=false;
            }
        }

        public void CargarRespuestasAplicacion(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> pResultados)
        {
            if (pResultados.Count > 0)
            {

                //GRUPO 1
                radTxtPreg1Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-A0001")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-A0001")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-A0001")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg1Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-A0002")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-A0002")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-A0002")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg1Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-A0003")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-A0003")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-A0003")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg1Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-A0004")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-A0004")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-A0004")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg1Resp5.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-A0005")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-A0005")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-A0005")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg1Resp6.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-A0006")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-A0006")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-A0006")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                //GRUPO 2
                radTxtPreg2Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-B0001")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-B0001")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-B0001")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg2Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-B0002")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-B0002")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-B0002")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg2Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-B0003")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-B0003")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-B0003")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg2Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-B0004")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-B0004")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-B0004")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg2Resp5.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-B0005")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-B0005")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-B0005")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg2Resp6.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-B0006")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-B0006")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-B0006")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                //GRUPO 3
                radTxtPreg3Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-C0001")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-C0001")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-C0001")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg3Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-C0002")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-C0002")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-C0002")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg3Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-C0003")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-C0003")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-C0003")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg3Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-C0004")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-C0004")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-C0004")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg3Resp5.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-C0005")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-C0005")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-C0005")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg3Resp6.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-C0006")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-C0006")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-C0006")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                //GRUPO 4
                radTxtPreg4Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-D0001")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-D0001")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-D0001")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg4Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-D0002")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-D0002")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-D0002")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg4Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-D0003")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-D0003")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-D0003")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg4Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-D0004")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-D0004")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-D0004")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg4Resp5.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-D0005")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-D0005")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-D0005")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg4Resp6.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-D0006")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-D0006")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-D0006")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                //GRUPO 5
                radTxtPreg5Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-E0001")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-E0001")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-E0001")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg5Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-E0002")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-E0002")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-E0002")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg5Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-E0003")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-E0003")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-E0003")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg5Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-E0004")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-E0004")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-E0004")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg5Resp5.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-E0005")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-E0005")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-E0005")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg5Resp6.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-E0006")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-E0006")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-E0006")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                //GRUPO 6
                radTxtPreg6Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-F0001")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-F0001")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-F0001")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg6Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-F0002")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-F0002")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-F0002")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg6Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-F0003")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-F0003")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-F0003")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg6Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-F0004")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-F0004")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-F0004")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg6Resp5.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-F0005")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-F0005")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-F0005")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg6Resp6.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-F0006")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-F0006")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-F0006")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                //GRUPO 7
                radTxtPreg7Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-G0001")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-G0001")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-G0001")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg7Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-G0002")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-G0002")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-G0002")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg7Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-G0003")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-G0003")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-G0003")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg7Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-G0004")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-G0004")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-G0004")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg7Resp5.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-G0005")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-G0005")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-G0005")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg7Resp6.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-G0006")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-G0006")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-G0006")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                //GRUPO 
                radTxtPreg8Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-H0001")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-H0001")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-H0001")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg8Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-H0002")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-H0002")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-H0002")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg8Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-H0003")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-H0003")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-H0003")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg8Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-H0004")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-H0004")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-H0004")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg8Resp5.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-H0005")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-H0005")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-H0005")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg8Resp6.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-H0006")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-H0006")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-H0006")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                //GRUPO 9
                radTxtPreg9Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-I0001")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-I0001")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-I0001")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg9Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-I0002")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-I0002")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-I0002")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg9Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-I0003")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-I0003")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-I0003")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg9Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-I0004")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-I0004")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-I0004")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg9Resp5.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-I0005")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-I0005")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-I0005")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg9Resp6.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-I0006")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-I0006")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-I0006")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                //GRUPO 10
                radTxtPreg10Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-J0001")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-J0001")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-J0001")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg10Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-J0002")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-J0002")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-J0002")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg10Resp3.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-J0003")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-J0003")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-J0003")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg10Resp4.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-J0004")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-J0004")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-J0004")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg10Resp5.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-J0005")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-J0005")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-J0005")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg10Resp6.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-J0006")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-J0006")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("INTERES-J0006")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
            }
        }

        public void actualizaLista(string pClVariable, string valorNuevo)
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

        public void actualizaListaAplicacion(string pClPreguntaTipo1, string pClPreguntaTipo2, string valorNuevo)
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

        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            if (validaCamposVacios())
            {
                PruebasNegocio nKprueba = new PruebasNegocio();
                SPE_OBTIENE_K_PRUEBA_Result vPruebaTerminada = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClTokenExterno, pIdPrueba: vIdPrueba).FirstOrDefault();
                vPruebaTerminada.FE_TERMINO = DateTime.Now;
                vPruebaTerminada.FE_INICIO = DateTime.Now;
                vPruebaTerminada.CL_ESTADO = E_ESTADO_PRUEBA.TERMINADA.ToString();
                if (vPruebaTerminada.NB_TIPO_PRUEBA == "APLICACION")
                {
                    vPruebaTerminada.NB_TIPO_PRUEBA = "APLICACION";
                    E_RESULTADO vResultado = nKprueba.InsertaActualiza_K_PRUEBA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
                    GuardarPruebaAplicacion();
                }
                else
                {
                    vPruebaTerminada.NB_TIPO_PRUEBA = "MANUAL";
                    E_RESULTADO vResultado = nKprueba.InsertaActualiza_K_PRUEBA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
                    GuardarPrueba();
                }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "No ha ingresado todos los valores.", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, "");
            }
        }

        public void GuardarPrueba()
        {

            actualizaLista("INTERES_RES_A1", radTxtPreg1Resp1.Text);
            actualizaLista("INTERES_RES_A2", radTxtPreg1Resp2.Text);
            actualizaLista("INTERES_RES_A3", radTxtPreg1Resp3.Text);
            actualizaLista("INTERES_RES_A4", radTxtPreg1Resp4.Text);
            actualizaLista("INTERES_RES_A5", radTxtPreg1Resp5.Text);
            actualizaLista("INTERES_RES_A6", radTxtPreg1Resp6.Text);

            actualizaLista("INTERES_RES_B1", radTxtPreg2Resp1.Text);
            actualizaLista("INTERES_RES_B2", radTxtPreg2Resp2.Text);
            actualizaLista("INTERES_RES_B3", radTxtPreg2Resp3.Text);
            actualizaLista("INTERES_RES_B4", radTxtPreg2Resp4.Text);
            actualizaLista("INTERES_RES_B5", radTxtPreg2Resp5.Text);
            actualizaLista("INTERES_RES_B6", radTxtPreg2Resp6.Text);

            actualizaLista("INTERES_RES_C1", radTxtPreg3Resp1.Text);
            actualizaLista("INTERES_RES_C2", radTxtPreg3Resp2.Text);
            actualizaLista("INTERES_RES_C3", radTxtPreg3Resp3.Text);
            actualizaLista("INTERES_RES_C4", radTxtPreg3Resp4.Text);
            actualizaLista("INTERES_RES_C5", radTxtPreg3Resp5.Text);
            actualizaLista("INTERES_RES_C6", radTxtPreg3Resp6.Text);

            actualizaLista("INTERES_RES_D1", radTxtPreg4Resp1.Text);
            actualizaLista("INTERES_RES_D2", radTxtPreg4Resp2.Text);
            actualizaLista("INTERES_RES_D3", radTxtPreg4Resp3.Text);
            actualizaLista("INTERES_RES_D4", radTxtPreg4Resp4.Text);
            actualizaLista("INTERES_RES_D5", radTxtPreg4Resp5.Text);
            actualizaLista("INTERES_RES_D6", radTxtPreg4Resp6.Text);

            actualizaLista("INTERES_RES_E1", radTxtPreg5Resp1.Text);
            actualizaLista("INTERES_RES_E2", radTxtPreg5Resp2.Text);
            actualizaLista("INTERES_RES_E3", radTxtPreg5Resp3.Text);
            actualizaLista("INTERES_RES_E4", radTxtPreg5Resp4.Text);
            actualizaLista("INTERES_RES_E5", radTxtPreg5Resp5.Text);
            actualizaLista("INTERES_RES_E6", radTxtPreg5Resp6.Text);

            actualizaLista("INTERES_RES_F1", radTxtPreg6Resp1.Text);
            actualizaLista("INTERES_RES_F2", radTxtPreg6Resp2.Text);
            actualizaLista("INTERES_RES_F3", radTxtPreg6Resp3.Text);
            actualizaLista("INTERES_RES_F4", radTxtPreg6Resp4.Text);
            actualizaLista("INTERES_RES_F5", radTxtPreg6Resp5.Text);
            actualizaLista("INTERES_RES_F6", radTxtPreg6Resp6.Text);

            actualizaLista("INTERES_RES_G1", radTxtPreg7Resp1.Text);
            actualizaLista("INTERES_RES_G2", radTxtPreg7Resp2.Text);
            actualizaLista("INTERES_RES_G3", radTxtPreg7Resp3.Text);
            actualizaLista("INTERES_RES_G4", radTxtPreg7Resp4.Text);
            actualizaLista("INTERES_RES_G5", radTxtPreg7Resp5.Text);
            actualizaLista("INTERES_RES_G6", radTxtPreg7Resp6.Text);

            actualizaLista("INTERES_RES_H1", radTxtPreg8Resp1.Text);
            actualizaLista("INTERES_RES_H2", radTxtPreg8Resp2.Text);
            actualizaLista("INTERES_RES_H3", radTxtPreg8Resp3.Text);
            actualizaLista("INTERES_RES_H4", radTxtPreg8Resp4.Text);
            actualizaLista("INTERES_RES_H5", radTxtPreg8Resp5.Text);
            actualizaLista("INTERES_RES_H6", radTxtPreg8Resp6.Text);

            actualizaLista("INTERES_RES_I1", radTxtPreg9Resp1.Text);
            actualizaLista("INTERES_RES_I2", radTxtPreg9Resp2.Text);
            actualizaLista("INTERES_RES_I3", radTxtPreg9Resp3.Text);
            actualizaLista("INTERES_RES_I4", radTxtPreg9Resp4.Text);
            actualizaLista("INTERES_RES_I5", radTxtPreg9Resp5.Text);
            actualizaLista("INTERES_RES_I6", radTxtPreg9Resp6.Text);

            actualizaLista("INTERES_RES_J1", radTxtPreg10Resp1.Text);
            actualizaLista("INTERES_RES_J2", radTxtPreg10Resp2.Text);
            actualizaLista("INTERES_RES_J3", radTxtPreg10Resp3.Text);
            actualizaLista("INTERES_RES_J4", radTxtPreg10Resp4.Text);
            actualizaLista("INTERES_RES_J5", radTxtPreg10Resp5.Text);
            actualizaLista("INTERES_RES_J6", radTxtPreg10Resp6.Text);

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
            var vObjetoPrueba = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClTokenExterno, pIdPrueba: vIdPrueba).FirstOrDefault();

            if (vObjetoPrueba != null)
            {
                E_RESULTADO vResultado = negRes.insertaResultadosInteres(RESPUESTAS.ToString(), null, vIdPrueba, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "CloseTest");
            }

        }

        public void GuardarPruebaAplicacion()
        {
            obtenerPreguntas(vIdPrueba, vClTokenExterno);

            actualizaListaAplicacion("INTERES-A0001", "INTERES_RES_A1", radTxtPreg1Resp1.Text);
            actualizaListaAplicacion("INTERES-A0002", "INTERES_RES_A2", radTxtPreg1Resp2.Text);
            actualizaListaAplicacion("INTERES-A0003", "INTERES_RES_A3", radTxtPreg1Resp3.Text);
            actualizaListaAplicacion("INTERES-A0004", "INTERES_RES_A4", radTxtPreg1Resp4.Text);
            actualizaListaAplicacion("INTERES-A0005", "INTERES_RES_A5", radTxtPreg1Resp5.Text);
            actualizaListaAplicacion("INTERES-A0006", "INTERES_RES_A6", radTxtPreg1Resp6.Text);

            actualizaListaAplicacion("INTERES-B0001", "INTERES_RES_B1", radTxtPreg2Resp1.Text);
            actualizaListaAplicacion("INTERES-B0002", "INTERES_RES_B2", radTxtPreg2Resp2.Text);
            actualizaListaAplicacion("INTERES-B0003", "INTERES_RES_B3", radTxtPreg2Resp3.Text);
            actualizaListaAplicacion("INTERES-B0004", "INTERES_RES_B4", radTxtPreg2Resp4.Text);
            actualizaListaAplicacion("INTERES-B0005", "INTERES_RES_B5", radTxtPreg2Resp5.Text);
            actualizaListaAplicacion("INTERES-B0006", "INTERES_RES_B6", radTxtPreg2Resp6.Text);

            actualizaListaAplicacion("INTERES-C0001", "INTERES_RES_C1", radTxtPreg3Resp1.Text);
            actualizaListaAplicacion("INTERES-C0002", "INTERES_RES_C2", radTxtPreg3Resp2.Text);
            actualizaListaAplicacion("INTERES-C0003", "INTERES_RES_C3", radTxtPreg3Resp3.Text);
            actualizaListaAplicacion("INTERES-C0004", "INTERES_RES_C4", radTxtPreg3Resp4.Text);
            actualizaListaAplicacion("INTERES-C0005", "INTERES_RES_C5", radTxtPreg3Resp5.Text);
            actualizaListaAplicacion("INTERES-C0006", "INTERES_RES_C6", radTxtPreg3Resp6.Text);

            actualizaListaAplicacion("INTERES-D0001", "INTERES_RES_D1", radTxtPreg4Resp1.Text);
            actualizaListaAplicacion("INTERES-D0002", "INTERES_RES_D2", radTxtPreg4Resp2.Text);
            actualizaListaAplicacion("INTERES-D0003", "INTERES_RES_D3", radTxtPreg4Resp3.Text);
            actualizaListaAplicacion("INTERES-D0004", "INTERES_RES_D4", radTxtPreg4Resp4.Text);
            actualizaListaAplicacion("INTERES-D0005", "INTERES_RES_D5", radTxtPreg4Resp5.Text);
            actualizaListaAplicacion("INTERES-D0006", "INTERES_RES_D6", radTxtPreg4Resp6.Text);

            actualizaListaAplicacion("INTERES-E0001", "INTERES_RES_E1", radTxtPreg5Resp1.Text);
            actualizaListaAplicacion("INTERES-E0002", "INTERES_RES_E2", radTxtPreg5Resp2.Text);
            actualizaListaAplicacion("INTERES-E0003", "INTERES_RES_E3", radTxtPreg5Resp3.Text);
            actualizaListaAplicacion("INTERES-E0004", "INTERES_RES_E4", radTxtPreg5Resp4.Text);
            actualizaListaAplicacion("INTERES-E0005", "INTERES_RES_E5", radTxtPreg5Resp5.Text);
            actualizaListaAplicacion("INTERES-E0006", "INTERES_RES_E6", radTxtPreg5Resp6.Text);

            actualizaListaAplicacion("INTERES-F0001", "INTERES_RES_F1", radTxtPreg6Resp1.Text);
            actualizaListaAplicacion("INTERES-F0002", "INTERES_RES_F2", radTxtPreg6Resp2.Text);
            actualizaListaAplicacion("INTERES-F0003", "INTERES_RES_F3", radTxtPreg6Resp3.Text);
            actualizaListaAplicacion("INTERES-F0004", "INTERES_RES_F4", radTxtPreg6Resp4.Text);
            actualizaListaAplicacion("INTERES-F0005", "INTERES_RES_F5", radTxtPreg6Resp5.Text);
            actualizaListaAplicacion("INTERES-F0006", "INTERES_RES_F6", radTxtPreg6Resp6.Text);

            actualizaListaAplicacion("INTERES-G0001", "INTERES_RES_G1", radTxtPreg7Resp1.Text);
            actualizaListaAplicacion("INTERES-G0002", "INTERES_RES_G2", radTxtPreg7Resp2.Text);
            actualizaListaAplicacion("INTERES-G0003", "INTERES_RES_G3", radTxtPreg7Resp3.Text);
            actualizaListaAplicacion("INTERES-G0004", "INTERES_RES_G4", radTxtPreg7Resp4.Text);
            actualizaListaAplicacion("INTERES-G0005", "INTERES_RES_G5", radTxtPreg7Resp5.Text);
            actualizaListaAplicacion("INTERES-G0006", "INTERES_RES_G6", radTxtPreg7Resp6.Text);

            actualizaListaAplicacion("INTERES-H0001", "INTERES_RES_H1", radTxtPreg8Resp1.Text);
            actualizaListaAplicacion("INTERES-H0002", "INTERES_RES_H2", radTxtPreg8Resp2.Text);
            actualizaListaAplicacion("INTERES-H0003", "INTERES_RES_H3", radTxtPreg8Resp3.Text);
            actualizaListaAplicacion("INTERES-H0004", "INTERES_RES_H4", radTxtPreg8Resp4.Text);
            actualizaListaAplicacion("INTERES-H0005", "INTERES_RES_H5", radTxtPreg8Resp5.Text);
            actualizaListaAplicacion("INTERES-H0006", "INTERES_RES_H6", radTxtPreg8Resp6.Text);

            actualizaListaAplicacion("INTERES-I0001", "INTERES_RES_I1", radTxtPreg9Resp1.Text);
            actualizaListaAplicacion("INTERES-I0002", "INTERES_RES_I2", radTxtPreg9Resp2.Text);
            actualizaListaAplicacion("INTERES-I0003", "INTERES_RES_I3", radTxtPreg9Resp3.Text);
            actualizaListaAplicacion("INTERES-I0004", "INTERES_RES_I4", radTxtPreg9Resp4.Text);
            actualizaListaAplicacion("INTERES-I0005", "INTERES_RES_I5", radTxtPreg9Resp5.Text);
            actualizaListaAplicacion("INTERES-I0006", "INTERES_RES_I6", radTxtPreg9Resp6.Text);

            actualizaListaAplicacion("INTERES-J0001", "INTERES_RES_J1", radTxtPreg10Resp1.Text);
            actualizaListaAplicacion("INTERES-J0002", "INTERES_RES_J2", radTxtPreg10Resp2.Text);
            actualizaListaAplicacion("INTERES-J0003", "INTERES_RES_J3", radTxtPreg10Resp3.Text);
            actualizaListaAplicacion("INTERES-J0004", "INTERES_RES_J4", radTxtPreg10Resp4.Text);
            actualizaListaAplicacion("INTERES-J0005", "INTERES_RES_J5", radTxtPreg10Resp5.Text);
            actualizaListaAplicacion("INTERES-J0006", "INTERES_RES_J6", radTxtPreg10Resp6.Text);

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
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "CloseTest");
            }
        }

        private bool validaCamposVacios()
        {
            bool continua = false;

            if(
               !String.IsNullOrEmpty(radTxtPreg1Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg1Resp2.Text) && !String.IsNullOrEmpty(radTxtPreg1Resp3.Text) && !String.IsNullOrEmpty(radTxtPreg1Resp4.Text) && !String.IsNullOrEmpty(radTxtPreg1Resp5.Text) && 
               !String.IsNullOrEmpty(radTxtPreg1Resp6.Text) && !String.IsNullOrEmpty(radTxtPreg2Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg2Resp2.Text) && !String.IsNullOrEmpty(radTxtPreg2Resp3.Text) && !String.IsNullOrEmpty(radTxtPreg2Resp4.Text) &&
               !String.IsNullOrEmpty(radTxtPreg2Resp5.Text) && !String.IsNullOrEmpty(radTxtPreg2Resp6.Text) && !String.IsNullOrEmpty(radTxtPreg3Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg3Resp2.Text) && !String.IsNullOrEmpty(radTxtPreg3Resp3.Text) && 
               !String.IsNullOrEmpty(radTxtPreg3Resp4.Text) && !String.IsNullOrEmpty(radTxtPreg3Resp5.Text) && !String.IsNullOrEmpty(radTxtPreg3Resp6.Text) && !String.IsNullOrEmpty(radTxtPreg4Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg4Resp2.Text) &&
               !String.IsNullOrEmpty(radTxtPreg4Resp3.Text) && !String.IsNullOrEmpty(radTxtPreg4Resp4.Text) && !String.IsNullOrEmpty(radTxtPreg4Resp5.Text) && !String.IsNullOrEmpty(radTxtPreg4Resp6.Text) && !String.IsNullOrEmpty(radTxtPreg5Resp1.Text) &&
               !String.IsNullOrEmpty(radTxtPreg5Resp2.Text) && !String.IsNullOrEmpty(radTxtPreg5Resp3.Text) && !String.IsNullOrEmpty(radTxtPreg5Resp4.Text) && !String.IsNullOrEmpty(radTxtPreg5Resp5.Text) && !String.IsNullOrEmpty(radTxtPreg5Resp6.Text) &&
               !String.IsNullOrEmpty(radTxtPreg6Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg6Resp2.Text) && !String.IsNullOrEmpty(radTxtPreg6Resp3.Text) && !String.IsNullOrEmpty(radTxtPreg6Resp4.Text) && !String.IsNullOrEmpty(radTxtPreg6Resp5.Text) &&
               !String.IsNullOrEmpty(radTxtPreg6Resp6.Text) && !String.IsNullOrEmpty(radTxtPreg7Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg7Resp2.Text) && !String.IsNullOrEmpty(radTxtPreg7Resp3.Text) && !String.IsNullOrEmpty(radTxtPreg7Resp4.Text) &&
               !String.IsNullOrEmpty(radTxtPreg7Resp5.Text) && !String.IsNullOrEmpty(radTxtPreg7Resp6.Text) && !String.IsNullOrEmpty(radTxtPreg8Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg8Resp2.Text) && !String.IsNullOrEmpty(radTxtPreg8Resp3.Text) &&
               !String.IsNullOrEmpty(radTxtPreg8Resp4.Text) && !String.IsNullOrEmpty(radTxtPreg8Resp5.Text) && !String.IsNullOrEmpty(radTxtPreg8Resp6.Text) && !String.IsNullOrEmpty(radTxtPreg9Resp1.Text) && !String.IsNullOrEmpty(radTxtPreg9Resp2.Text) &&
               !String.IsNullOrEmpty(radTxtPreg9Resp3.Text) && !String.IsNullOrEmpty(radTxtPreg9Resp4.Text) && !String.IsNullOrEmpty(radTxtPreg9Resp5.Text) && !String.IsNullOrEmpty(radTxtPreg9Resp6.Text) && !String.IsNullOrEmpty(radTxtPreg10Resp1.Text) &&
               !String.IsNullOrEmpty(radTxtPreg10Resp2.Text) && !String.IsNullOrEmpty(radTxtPreg10Resp3.Text) && !String.IsNullOrEmpty(radTxtPreg10Resp4.Text) && !String.IsNullOrEmpty(radTxtPreg10Resp5.Text) && !String.IsNullOrEmpty(radTxtPreg10Resp6.Text)
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