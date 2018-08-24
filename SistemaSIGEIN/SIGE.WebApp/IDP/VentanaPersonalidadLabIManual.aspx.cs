using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Negocio.Administracion;
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
    public partial class CapManPersonalidadLabI : System.Web.UI.Page
    {
        private static E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private List<E_PRUEBA_RESULTADO> vRespuestas
        {
            get { return (List<E_PRUEBA_RESULTADO>)ViewState["vsPreguntasCustionario"]; }
            set { ViewState["vsPreguntasCustionario"] = value; }
        }

        private List<E_PREGUNTA> vRespuestasAplicacion
        {
            get { return (List<E_PREGUNTA>)ViewState["vsRespuestasAplicacion"]; }
            set { ViewState["vsRespuestasAplicacion"] = value; }
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
                        var vPrueba = nPruebas.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken).FirstOrDefault();
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
            }
            
        }

        public void CargarRespuestas(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> pResultados)
        {
            if (pResultados.Count > 0)
            {
                radTxtPreg1Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-A01")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-A01")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-A01")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg1Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-A02")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-A02")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-A02")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg2Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-B01")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-B01")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-B01")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg2Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-B02")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-B02")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-B02")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg3Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-C01")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-C01")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-C01")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg3Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-C02")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-C02")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-C02")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg4Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-D01")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-D01")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-D01")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg4Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-D02")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-D02")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-D02")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg5Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-E01")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-E01")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-E01")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg5Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-E02")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-E02")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-E02")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg6Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-F01")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-F01")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-F01")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg6Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-F02")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-F02")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-F02")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg7Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-G01")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-G01")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-G01")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg7Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-G02")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-G02")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-G02")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg8Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-H01")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-H01")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-H01")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg8Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-H02")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-H02")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-H02")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg9Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-I01")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-I01")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-I01")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg9Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-I02")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-I02")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-I02")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg10Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-J01")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-J01")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-J01")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg10Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-J02")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-J02")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-J02")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg11Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-K01")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-K01")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-K01")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg11Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-K02")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-K02")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-K02")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg12Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-L01")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-L01")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-L01")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg12Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-L02")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-L02")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-L02")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg13Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-M01")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-M01")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-M01")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg13Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-M02")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-M02")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-M02")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString()) : "";

                radTxtPreg14Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-N01")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-N01")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-N01")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg14Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-N02")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-N02")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-N02")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg15Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-O01")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-O01")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-O01")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg15Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-O02")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-O02")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-O02")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg16Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-P01")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-P01")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-P01")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg16Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-P02")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-P02")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-P02")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg17Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-Q01")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-Q01")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-Q01")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg17Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-Q02")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-Q02")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-Q02")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg18Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-R01")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-R01")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-R01")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg18Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-R02")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-R02")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-R02")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg19Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-S01")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-S01")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-S01")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg19Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-S02")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-S02")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-S02")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg20Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-T01")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-T01")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-T01")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg20Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-T02")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-T02")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-T02")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg21Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-U01")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-U01")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-U01")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg21Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-U02")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-U02")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-U02")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg22Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-V01")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-V01")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-V01")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg22Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-V02")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-V02")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-V02")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg23Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-W01")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-W01")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-W01")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg23Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-W02")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-W02")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-W02")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                radTxtPreg24Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-X01")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-X01")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-X01")).FirstOrDefault().NB_RESULTADO.ToString()) : "";
                radTxtPreg24Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-X02")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-X02")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-X02")).FirstOrDefault().NB_RESULTADO.ToString()) : "";

                //if (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-RES-A01")).FirstOrDefault().NB_RESULTADO == null)
                //    btnTerminar.Enabled = false;
            
            }
        }

        public void CargarRespuestasAplicacion(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> pResultados)
        {
            if (pResultados.Count > 0)
            {
                radTxtPreg1Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0001")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0001")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0001")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg1Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0001")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0001")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0001")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg2Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0002")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0002")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0002")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg2Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0002")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0002")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0002")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg3Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0003")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0003")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0003")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg3Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0003")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0003")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0003")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg4Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0004")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0004")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0004")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg4Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0004")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0004")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0004")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg5Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0005")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0005")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0005")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg5Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0005")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0005")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0005")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg6Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0006")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0006")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0006")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg6Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0006")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0006")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0006")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg7Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0007")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0007")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0007")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg7Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0007")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0007")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0007")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg8Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0008")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0008")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0008")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg8Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0008")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0008")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0008")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg9Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0009")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0009")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0009")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg9Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0009")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0009")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0009")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg10Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0010")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0010")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0010")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg10Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0010")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0010")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0010")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg11Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0011")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0011")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0011")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg11Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0011")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0011")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0011")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg12Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0012")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0012")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0012")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg12Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0012")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0012")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0012")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg13Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0013")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0013")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0013")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg13Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0013")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0013")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0013")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString()) : "";

                radTxtPreg14Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0014")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0014")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0014")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg14Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0014")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0014")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0014")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg15Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0015")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0015")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0015")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg15Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0015")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0015")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0015")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg16Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0016")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0016")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0016")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg16Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0016")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0016")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0016")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg17Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0017")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0017")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0017")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg17Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0017")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0017")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0017")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg18Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0018")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0018")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0018")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg18Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0018")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0018")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0018")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg19Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0019")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0019")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0019")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg19Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0019")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0019")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0019")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg20Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0020")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0020")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0020")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg20Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0020")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0020")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0020")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg21Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0021")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0021")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0021")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg21Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0021")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0021")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0021")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg22Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0022")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0022")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0022")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg22Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0022")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0022")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0022")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg23Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0023")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0023")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0023")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg23Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0023")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0023")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0023")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";

                radTxtPreg24Resp1.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0024")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0024")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-A-0024")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";
                radTxtPreg24Resp2.Text = (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0024")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0024")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("LABORAL1-B-0024")).FirstOrDefault().NB_RESPUESTA.ToString()) : "";


            }
        }

        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            PruebasNegocio nKprueba = new PruebasNegocio();
            SPE_OBTIENE_K_PRUEBA_Result vPruebaTerminada = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();
            if (vPruebaTerminada.NB_TIPO_PRUEBA == "APLICACION")
            {
                vPruebaTerminada.FE_TERMINO = DateTime.Now;
                vPruebaTerminada.FE_INICIO = DateTime.Now;
                vPruebaTerminada.CL_ESTADO = E_ESTADO_PRUEBA.TERMINADA.ToString();
                E_RESULTADO vResultado = nKprueba.InsertaActualiza_K_PRUEBA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
                SaveTestAplicacion();
            }
            else
            {
                vPruebaTerminada.FE_TERMINO = DateTime.Now;
                vPruebaTerminada.CL_ESTADO = E_ESTADO_PRUEBA.TERMINADA.ToString();
                vPruebaTerminada.NB_TIPO_PRUEBA = "MANUAL";
                E_RESULTADO vResultado = nKprueba.InsertaActualiza_K_PRUEBA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
                SaveTest();
            }
        }

        public void SaveTest()
        {
            vRespuestas = new List<E_PRUEBA_RESULTADO>();
            AsignarValorRespuestas("LABORAL1-RES-A01", (!radTxtPreg1Resp1.Text.Equals("")) ? int.Parse(radTxtPreg1Resp1.Text) : 0);
            AsignarValorRespuestas("LABORAL1-RES-A02", (!radTxtPreg1Resp2.Text.Equals("")) ? int.Parse(radTxtPreg1Resp2.Text) : 0);

            AsignarValorRespuestas("LABORAL1-RES-B01", (!radTxtPreg2Resp1.Text.Equals("")) ? int.Parse(radTxtPreg2Resp1.Text) : 0);
            AsignarValorRespuestas("LABORAL1-RES-B02", (!radTxtPreg2Resp2.Text.Equals("")) ? int.Parse(radTxtPreg2Resp2.Text) : 0);

            AsignarValorRespuestas("LABORAL1-RES-C01", (!radTxtPreg3Resp1.Text.Equals("")) ? int.Parse(radTxtPreg3Resp1.Text) : 0);
            AsignarValorRespuestas("LABORAL1-RES-C02", (!radTxtPreg3Resp2.Text.Equals("")) ? int.Parse(radTxtPreg3Resp2.Text) : 0);

            AsignarValorRespuestas("LABORAL1-RES-D01", (!radTxtPreg4Resp1.Text.Equals("")) ? int.Parse(radTxtPreg4Resp1.Text) : 0);
            AsignarValorRespuestas("LABORAL1-RES-D02", (!radTxtPreg4Resp2.Text.Equals("")) ? int.Parse(radTxtPreg4Resp2.Text) : 0);

            AsignarValorRespuestas("LABORAL1-RES-E01", (!radTxtPreg5Resp1.Text.Equals("")) ? int.Parse(radTxtPreg5Resp1.Text) : 0);
            AsignarValorRespuestas("LABORAL1-RES-E02", (!radTxtPreg5Resp2.Text.Equals("")) ? int.Parse(radTxtPreg5Resp2.Text) : 0);

            AsignarValorRespuestas("LABORAL1-RES-F01", (!radTxtPreg6Resp1.Text.Equals("")) ? int.Parse(radTxtPreg6Resp1.Text) : 0);
            AsignarValorRespuestas("LABORAL1-RES-F02", (!radTxtPreg6Resp2.Text.Equals("")) ? int.Parse(radTxtPreg6Resp2.Text) : 0);

            AsignarValorRespuestas("LABORAL1-RES-G01", (!radTxtPreg7Resp1.Text.Equals("")) ? int.Parse(radTxtPreg7Resp1.Text) : 0);
            AsignarValorRespuestas("LABORAL1-RES-G02", (!radTxtPreg7Resp2.Text.Equals("")) ? int.Parse(radTxtPreg7Resp2.Text) : 0);

            AsignarValorRespuestas("LABORAL1-RES-H01", (!radTxtPreg8Resp1.Text.Equals("")) ? int.Parse(radTxtPreg8Resp1.Text) : 0);
            AsignarValorRespuestas("LABORAL1-RES-H02", (!radTxtPreg8Resp2.Text.Equals("")) ? int.Parse(radTxtPreg8Resp2.Text) : 0);

            AsignarValorRespuestas("LABORAL1-RES-I01", (!radTxtPreg9Resp1.Text.Equals("")) ? int.Parse(radTxtPreg9Resp1.Text) : 0);
            AsignarValorRespuestas("LABORAL1-RES-I02", (!radTxtPreg9Resp2.Text.Equals("")) ? int.Parse(radTxtPreg9Resp2.Text) : 0);

            AsignarValorRespuestas("LABORAL1-RES-J01", (!radTxtPreg10Resp1.Text.Equals("")) ? int.Parse(radTxtPreg10Resp1.Text) : 0);
            AsignarValorRespuestas("LABORAL1-RES-J02", (!radTxtPreg10Resp2.Text.Equals("")) ? int.Parse(radTxtPreg10Resp2.Text) : 0);

            AsignarValorRespuestas("LABORAL1-RES-K01", (!radTxtPreg11Resp1.Text.Equals("")) ? int.Parse(radTxtPreg11Resp1.Text) : 0);
            AsignarValorRespuestas("LABORAL1-RES-K02", (!radTxtPreg11Resp2.Text.Equals("")) ? int.Parse(radTxtPreg11Resp2.Text) : 0);

            AsignarValorRespuestas("LABORAL1-RES-L01", (!radTxtPreg12Resp1.Text.Equals("")) ? int.Parse(radTxtPreg12Resp1.Text) : 0);
            AsignarValorRespuestas("LABORAL1-RES-L02", (!radTxtPreg12Resp2.Text.Equals("")) ? int.Parse(radTxtPreg12Resp2.Text) : 0);

            AsignarValorRespuestas("LABORAL1-RES-M01", (!radTxtPreg13Resp1.Text.Equals("")) ? int.Parse(radTxtPreg13Resp1.Text) : 0);
            AsignarValorRespuestas("LABORAL1-RES-M02", (!radTxtPreg13Resp2.Text.Equals("")) ? int.Parse(radTxtPreg13Resp2.Text) : 0);

            AsignarValorRespuestas("LABORAL1-RES-N01", (!radTxtPreg14Resp1.Text.Equals("")) ? int.Parse(radTxtPreg14Resp1.Text) : 0);
            AsignarValorRespuestas("LABORAL1-RES-N02", (!radTxtPreg14Resp2.Text.Equals("")) ? int.Parse(radTxtPreg14Resp2.Text) : 0);

            AsignarValorRespuestas("LABORAL1-RES-O01", (!radTxtPreg15Resp1.Text.Equals("")) ? int.Parse(radTxtPreg15Resp1.Text) : 0);
            AsignarValorRespuestas("LABORAL1-RES-O02", (!radTxtPreg15Resp2.Text.Equals("")) ? int.Parse(radTxtPreg15Resp2.Text) : 0);

            AsignarValorRespuestas("LABORAL1-RES-P01", (!radTxtPreg16Resp1.Text.Equals("")) ? int.Parse(radTxtPreg16Resp1.Text) : 0);
            AsignarValorRespuestas("LABORAL1-RES-P02", (!radTxtPreg16Resp2.Text.Equals("")) ? int.Parse(radTxtPreg16Resp2.Text) : 0);

            AsignarValorRespuestas("LABORAL1-RES-Q01", (!radTxtPreg17Resp1.Text.Equals("")) ? int.Parse(radTxtPreg17Resp1.Text) : 0);
            AsignarValorRespuestas("LABORAL1-RES-Q02", (!radTxtPreg17Resp2.Text.Equals("")) ? int.Parse(radTxtPreg17Resp2.Text) : 0);

            AsignarValorRespuestas("LABORAL1-RES-R01", (!radTxtPreg18Resp1.Text.Equals("")) ? int.Parse(radTxtPreg18Resp1.Text) : 0);
            AsignarValorRespuestas("LABORAL1-RES-R02", (!radTxtPreg18Resp2.Text.Equals("")) ? int.Parse(radTxtPreg18Resp2.Text) : 0);

            AsignarValorRespuestas("LABORAL1-RES-S01", (!radTxtPreg19Resp1.Text.Equals("")) ? int.Parse(radTxtPreg19Resp1.Text) : 0);
            AsignarValorRespuestas("LABORAL1-RES-S02", (!radTxtPreg19Resp2.Text.Equals("")) ? int.Parse(radTxtPreg19Resp2.Text) : 0);

            AsignarValorRespuestas("LABORAL1-RES-T01", (!radTxtPreg20Resp1.Text.Equals("")) ? int.Parse(radTxtPreg20Resp1.Text) : 0);
            AsignarValorRespuestas("LABORAL1-RES-T02", (!radTxtPreg20Resp2.Text.Equals("")) ? int.Parse(radTxtPreg20Resp2.Text) : 0);

            AsignarValorRespuestas("LABORAL1-RES-U01", (!radTxtPreg21Resp1.Text.Equals("")) ? int.Parse(radTxtPreg21Resp1.Text) : 0);
            AsignarValorRespuestas("LABORAL1-RES-U02", (!radTxtPreg21Resp2.Text.Equals("")) ? int.Parse(radTxtPreg21Resp2.Text) : 0);

            AsignarValorRespuestas("LABORAL1-RES-V01", (!radTxtPreg22Resp1.Text.Equals("")) ? int.Parse(radTxtPreg22Resp1.Text) : 0);
            AsignarValorRespuestas("LABORAL1-RES-V02", (!radTxtPreg22Resp2.Text.Equals("")) ? int.Parse(radTxtPreg22Resp2.Text) : 0);

            AsignarValorRespuestas("LABORAL1-RES-W01", (!radTxtPreg23Resp1.Text.Equals("")) ? int.Parse(radTxtPreg23Resp1.Text) : 0);
            AsignarValorRespuestas("LABORAL1-RES-W02", (!radTxtPreg23Resp2.Text.Equals("")) ? int.Parse(radTxtPreg23Resp2.Text) : 0);

            AsignarValorRespuestas("LABORAL1-RES-X01", (!radTxtPreg24Resp1.Text.Equals("")) ? int.Parse(radTxtPreg24Resp1.Text) : 0);
            AsignarValorRespuestas("LABORAL1-RES-X02", (!radTxtPreg24Resp2.Text.Equals("")) ? int.Parse(radTxtPreg24Resp2.Text) : 0);


            var vXelements = vRespuestas.Select(x =>
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
                E_RESULTADO vResultado = negRes.insertaResultadosLaboral1(RESPUESTAS.ToString(), null, vIdPrueba, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "CloseTest");
            }
        }

        public void SaveTestAplicacion()
        {
            CuestionariosNegocio nPreguntas = new CuestionariosNegocio();
            vRespuestasAplicacion = new List<E_PREGUNTA>();
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
                vRespuestasAplicacion.Add(vObjetoPregunta);
            }

            BackQuestionObject("LABORAL1-A-0001", "LABORAL1-RES-A01", (!radTxtPreg1Resp1.Text.Equals("")) ? int.Parse(radTxtPreg1Resp1.Text) : 0);
            BackQuestionObject("LABORAL1-B-0001", "LABORAL1-RES-A02", (!radTxtPreg1Resp2.Text.Equals("")) ? int.Parse(radTxtPreg1Resp2.Text) : 0);
            //
            BackQuestionObject("LABORAL1-A-0002", "LABORAL1-RES-B01", (!radTxtPreg2Resp1.Text.Equals("")) ? int.Parse(radTxtPreg2Resp1.Text) : 0);
            BackQuestionObject("LABORAL1-B-0002", "LABORAL1-RES-B02", (!radTxtPreg2Resp2.Text.Equals("")) ? int.Parse(radTxtPreg2Resp2.Text) : 0);
            //
            BackQuestionObject("LABORAL1-A-0003", "LABORAL1-RES-C01", (!radTxtPreg3Resp1.Text.Equals("")) ? int.Parse(radTxtPreg3Resp1.Text) : 0);
            BackQuestionObject("LABORAL1-B-0003", "LABORAL1-RES-C02", (!radTxtPreg3Resp2.Text.Equals("")) ? int.Parse(radTxtPreg3Resp2.Text) : 0);
            //
            BackQuestionObject("LABORAL1-A-0004", "LABORAL1-RES-D01", (!radTxtPreg4Resp1.Text.Equals("")) ? int.Parse(radTxtPreg4Resp1.Text) : 0);
            BackQuestionObject("LABORAL1-B-0004", "LABORAL1-RES-D02", (!radTxtPreg4Resp2.Text.Equals("")) ? int.Parse(radTxtPreg4Resp2.Text) : 0);
            //
            BackQuestionObject("LABORAL1-A-0005", "LABORAL1-RES-E01", (!radTxtPreg5Resp1.Text.Equals("")) ? int.Parse(radTxtPreg5Resp1.Text) : 0);
            BackQuestionObject("LABORAL1-B-0005", "LABORAL1-RES-E02", (!radTxtPreg5Resp2.Text.Equals("")) ? int.Parse(radTxtPreg5Resp2.Text) : 0);
            //
            BackQuestionObject("LABORAL1-A-0006", "LABORAL1-RES-F01", (!radTxtPreg6Resp1.Text.Equals("")) ? int.Parse(radTxtPreg6Resp1.Text) : 0);
            BackQuestionObject("LABORAL1-B-0006", "LABORAL1-RES-F02", (!radTxtPreg6Resp2.Text.Equals("")) ? int.Parse(radTxtPreg6Resp2.Text) : 0);
            //
            BackQuestionObject("LABORAL1-A-0007", "LABORAL1-RES-G01", (!radTxtPreg7Resp1.Text.Equals("")) ? int.Parse(radTxtPreg7Resp1.Text) : 0);
            BackQuestionObject("LABORAL1-B-0007", "LABORAL1-RES-G02", (!radTxtPreg7Resp2.Text.Equals("")) ? int.Parse(radTxtPreg7Resp2.Text) : 0);
            //
            BackQuestionObject("LABORAL1-A-0008", "LABORAL1-RES-H01", (!radTxtPreg8Resp1.Text.Equals("")) ? int.Parse(radTxtPreg8Resp1.Text) : 0);
            BackQuestionObject("LABORAL1-B-0008", "LABORAL1-RES-H02", (!radTxtPreg8Resp2.Text.Equals("")) ? int.Parse(radTxtPreg8Resp2.Text) : 0);
            //
            BackQuestionObject("LABORAL1-A-0009", "LABORAL1-RES-I01", (!radTxtPreg9Resp1.Text.Equals("")) ? int.Parse(radTxtPreg9Resp1.Text) : 0);
            BackQuestionObject("LABORAL1-B-0009", "LABORAL1-RES-I02", (!radTxtPreg9Resp2.Text.Equals("")) ? int.Parse(radTxtPreg9Resp2.Text) : 0);
            //
            BackQuestionObject("LABORAL1-A-0010", "LABORAL1-RES-J01", (!radTxtPreg10Resp1.Text.Equals("")) ? int.Parse(radTxtPreg10Resp1.Text) : 0);
            BackQuestionObject("LABORAL1-B-0010", "LABORAL1-RES-J02", (!radTxtPreg10Resp2.Text.Equals("")) ? int.Parse(radTxtPreg10Resp2.Text) : 0);
            //
            BackQuestionObject("LABORAL1-A-0011", "LABORAL1-RES-K01", (!radTxtPreg11Resp1.Text.Equals("")) ? int.Parse(radTxtPreg11Resp1.Text) : 0);
            BackQuestionObject("LABORAL1-B-0011", "LABORAL1-RES-K02", (!radTxtPreg11Resp2.Text.Equals("")) ? int.Parse(radTxtPreg11Resp2.Text) : 0);
            //
            BackQuestionObject("LABORAL1-A-0012", "LABORAL1-RES-L01", (!radTxtPreg12Resp1.Text.Equals("")) ? int.Parse(radTxtPreg12Resp1.Text) : 0);
            BackQuestionObject("LABORAL1-B-0012", "LABORAL1-RES-L02", (!radTxtPreg12Resp2.Text.Equals("")) ? int.Parse(radTxtPreg12Resp2.Text) : 0);
            //
            BackQuestionObject("LABORAL1-A-0013", "LABORAL1-RES-M01", (!radTxtPreg13Resp1.Text.Equals("")) ? int.Parse(radTxtPreg13Resp1.Text) : 0);
            BackQuestionObject("LABORAL1-B-0013", "LABORAL1-RES-M02", (!radTxtPreg13Resp2.Text.Equals("")) ? int.Parse(radTxtPreg13Resp2.Text) : 0);
            //
            BackQuestionObject("LABORAL1-A-0014", "LABORAL1-RES-N01", (!radTxtPreg14Resp1.Text.Equals("")) ? int.Parse(radTxtPreg14Resp1.Text) : 0);
            BackQuestionObject("LABORAL1-B-0014", "LABORAL1-RES-N02", (!radTxtPreg14Resp2.Text.Equals("")) ? int.Parse(radTxtPreg14Resp2.Text) : 0);
            //
            BackQuestionObject("LABORAL1-A-0015", "LABORAL1-RES-O01", (!radTxtPreg15Resp1.Text.Equals("")) ? int.Parse(radTxtPreg15Resp1.Text) : 0);
            BackQuestionObject("LABORAL1-B-0015", "LABORAL1-RES-O02", (!radTxtPreg15Resp2.Text.Equals("")) ? int.Parse(radTxtPreg15Resp2.Text) : 0);
            //
            BackQuestionObject("LABORAL1-A-0016", "LABORAL1-RES-P01", (!radTxtPreg16Resp1.Text.Equals("")) ? int.Parse(radTxtPreg16Resp1.Text) : 0);
            BackQuestionObject("LABORAL1-B-0016", "LABORAL1-RES-P02", (!radTxtPreg16Resp2.Text.Equals("")) ? int.Parse(radTxtPreg16Resp2.Text) : 0);
            //
            BackQuestionObject("LABORAL1-A-0017", "LABORAL1-RES-Q01", (!radTxtPreg17Resp1.Text.Equals("")) ? int.Parse(radTxtPreg17Resp1.Text) : 0);
            BackQuestionObject("LABORAL1-B-0017", "LABORAL1-RES-Q02", (!radTxtPreg17Resp2.Text.Equals("")) ? int.Parse(radTxtPreg17Resp2.Text) : 0);
            //
            BackQuestionObject("LABORAL1-A-0018", "LABORAL1-RES-R01", (!radTxtPreg18Resp1.Text.Equals("")) ? int.Parse(radTxtPreg18Resp1.Text) : 0);
            BackQuestionObject("LABORAL1-B-0018", "LABORAL1-RES-R02", (!radTxtPreg18Resp2.Text.Equals("")) ? int.Parse(radTxtPreg18Resp2.Text) : 0);
            //
            BackQuestionObject("LABORAL1-A-0019", "LABORAL1-RES-S01", (!radTxtPreg19Resp1.Text.Equals("")) ? int.Parse(radTxtPreg19Resp1.Text) : 0);
            BackQuestionObject("LABORAL1-B-0019", "LABORAL1-RES-S02", (!radTxtPreg19Resp2.Text.Equals("")) ? int.Parse(radTxtPreg19Resp2.Text) : 0);
            //
            BackQuestionObject("LABORAL1-A-0020", "LABORAL1-RES-T01", (!radTxtPreg20Resp1.Text.Equals("")) ? int.Parse(radTxtPreg20Resp1.Text) : 0);
            BackQuestionObject("LABORAL1-B-0020", "LABORAL1-RES-T02", (!radTxtPreg20Resp2.Text.Equals("")) ? int.Parse(radTxtPreg20Resp2.Text) : 0);
            //
            BackQuestionObject("LABORAL1-A-0021", "LABORAL1-RES-U01", (!radTxtPreg21Resp1.Text.Equals("")) ? int.Parse(radTxtPreg21Resp1.Text) : 0);
            BackQuestionObject("LABORAL1-B-0021", "LABORAL1-RES-U02", (!radTxtPreg21Resp2.Text.Equals("")) ? int.Parse(radTxtPreg21Resp2.Text) : 0);
            //
            BackQuestionObject("LABORAL1-A-0022", "LABORAL1-RES-V01", (!radTxtPreg22Resp1.Text.Equals("")) ? int.Parse(radTxtPreg22Resp1.Text) : 0);
            BackQuestionObject("LABORAL1-B-0022", "LABORAL1-RES-V02", (!radTxtPreg22Resp2.Text.Equals("")) ? int.Parse(radTxtPreg22Resp2.Text) : 0);
            //
            BackQuestionObject("LABORAL1-A-0023", "LABORAL1-RES-W01", (!radTxtPreg23Resp1.Text.Equals("")) ? int.Parse(radTxtPreg23Resp1.Text) : 0);
            BackQuestionObject("LABORAL1-B-0023", "LABORAL1-RES-W02", (!radTxtPreg23Resp2.Text.Equals("")) ? int.Parse(radTxtPreg23Resp2.Text) : 0);
            //
            BackQuestionObject("LABORAL1-A-0024", "LABORAL1-RES-X01", (!radTxtPreg24Resp1.Text.Equals("")) ? int.Parse(radTxtPreg24Resp1.Text) : 0);
            BackQuestionObject("LABORAL1-B-0024", "LABORAL1-RES-X02", (!radTxtPreg24Resp2.Text.Equals("")) ? int.Parse(radTxtPreg24Resp2.Text) : 0);
            //
            var vXelements = vRespuestasAplicacion.Select(x =>
                                                 new XElement("RESPUESTA",
                                                 new XAttribute("ID_CUESTIONARIO_PREGUNTA", x.ID_CUESTIONARIO_PREGUNTA),
                                                 new XAttribute("ID_PREGUNTA", x.ID_PREGUNTA),
                                                 new XAttribute("NB_PREGUNTA", x.NB_PREGUNTA),
                                                 new XAttribute("CL_PREGUNTA", x.CL_PREGUNTA),
                                                 new XAttribute("NB_RESPUESTA", x.NB_RESPUESTA),
                                                 new XAttribute("NO_VALOR_RESPUESTA", x.NO_VALOR_RESPUESTA)
                                      ));
            XElement RESPUESTAS =
            new XElement("RESPUESTAS", vXelements
            );

            CuestionarioPreguntaNegocio nCustionarioPregunta = new CuestionarioPreguntaNegocio();
            PruebasNegocio nKprueba = new PruebasNegocio();
            var vObjetoPrueba = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();

            E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vObjetoPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), pNbPrueba: "PERSONALIDAD_LABORAL", usuario: vClUsuario, programa: vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "CloseTest");
        }

        public void AsignarValorRespuestas(string pClVariable, int pnbRespuesta)
        {
            E_PRUEBA_RESULTADO vResultado = new E_PRUEBA_RESULTADO();
            vResultado.NO_VALOR = pnbRespuesta;
            vResultado.CL_VARIABLE = pClVariable;
            vResultado.NO_VALOR_RES=pnbRespuesta;
            vRespuestas.Add(vResultado);
        }

        public void BackQuestionObject(string pclPreguntaTipo1, String pclPreguntaTipo2, int pnbRespuesta)
        {
            var vPregunta = vRespuestasAplicacion.Where(x => x.CL_PREGUNTA.Equals(pclPreguntaTipo1)).FirstOrDefault();
            if (vPregunta != null)
            {
                vPregunta.CL_PREGUNTA = pclPreguntaTipo2;
                vPregunta.NB_RESPUESTA = pnbRespuesta.ToString();
                vPregunta.NO_VALOR_RESPUESTA = pnbRespuesta;
            }
        }

    }
}
