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
    public partial class VentanaPersonalidadLaboral2Manual : System.Web.UI.Page
    {
        #region Variables
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

        private int vIdPrueba
        {
            get { return (int)ViewState["vsIdEvaluado"]; }
            set { ViewState["vsIdEvaluado"] = value; }
        }     

        public string vEstatusPrueba;

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
                    //if (vObjetoPrueba != null)
                    //{
                    //    if (vObjetoPrueba.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.ERROR)
                    //    {
                            PruebasNegocio nPruebas = new PruebasNegocio();
                            var prueba = nPruebas.Obtener_RESULTADO_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).ToList();
                            var vPrueba = nPruebas.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();
                            if (prueba != null)
                            {
                                if (vPrueba.NB_TIPO_PRUEBA == "APLICACION")
                                  CargarRespuestasAplicacion(prueba);
                                else
                                    CargarRespuestasCapturaManual(prueba);
                            }
                    //    }
                    //}
                }
                vPruebaResultado = new List<E_PRUEBA_RESULTADO>();
            }
        }

        public void CargarRespuestasCapturaManual(List  <SPE_OBTIENE_RESULTADO_PRUEBA_Result> pRespuestas) 
        {
            if (pRespuestas.Count > 0)
            {
                txtRespA1.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0001")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0001")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0001")).FirstOrDefault().NB_RESULTADO.ToString() : "";
                txtRespB1.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0002")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0002")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0002")).FirstOrDefault().NB_RESULTADO.ToString() : "";
                txtRespC1.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0003")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0003")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0003")).FirstOrDefault().NB_RESULTADO.ToString() : "";
                txtRespD1.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0004")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0004")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0004")).FirstOrDefault().NB_RESULTADO.ToString() : "";
                txtRespE1.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0005")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0005")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0005")).FirstOrDefault().NB_RESULTADO.ToString() : "";
                txtRespF1.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0006")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0006")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0006")).FirstOrDefault().NB_RESULTADO.ToString() : "";
                txtRespG1.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0007")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0007")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0007")).FirstOrDefault().NB_RESULTADO.ToString() : "";
                txtRespH1.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0008")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0008")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0008")).FirstOrDefault().NB_RESULTADO.ToString() : "";
                txtRespI1.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0009")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0009")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0009")).FirstOrDefault().NB_RESULTADO.ToString() : "";
                txtRespJ1.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0010")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0010")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0010")).FirstOrDefault().NB_RESULTADO.ToString() : "";
                txtRespK1.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0011")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0011")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0011")).FirstOrDefault().NB_RESULTADO.ToString() : "";
                txtRespL1.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0012")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0012")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0012")).FirstOrDefault().NB_RESULTADO.ToString() : "";
                txtRespA2.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0013")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0013")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0013")).FirstOrDefault().NB_RESULTADO.ToString() : "";
                txtRespB2.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0014")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0014")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0014")).FirstOrDefault().NB_RESULTADO.ToString() : "";
                txtRespC2.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0015")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0015")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0015")).FirstOrDefault().NB_RESULTADO.ToString() : "";
                txtRespD2.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0016")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0016")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0016")).FirstOrDefault().NB_RESULTADO.ToString() : "";
                txtRespE2.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0017")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0017")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0017")).FirstOrDefault().NB_RESULTADO.ToString() : "";
                txtRespF2.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0018")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0018")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0018")).FirstOrDefault().NB_RESULTADO.ToString() : "";
                txtRespG2.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0019")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0019")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0019")).FirstOrDefault().NB_RESULTADO.ToString() : "";
                txtRespH2.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0020")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0020")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0020")).FirstOrDefault().NB_RESULTADO.ToString() : "";
                txtRespI2.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0021")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0021")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0021")).FirstOrDefault().NB_RESULTADO.ToString() : "";
                txtRespJ2.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0022")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0022")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0022")).FirstOrDefault().NB_RESULTADO.ToString() : "";
                txtRespK2.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0023")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0023")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0023")).FirstOrDefault().NB_RESULTADO.ToString() : "";
                txtRespL2.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0024")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0024")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0024")).FirstOrDefault().NB_RESULTADO.ToString() : "";

                //if (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0001")).FirstOrDefault().NB_RESULTADO == null)
                //    btnTerminar.Enabled = false;
            }
        }

        public void CargarRespuestasAplicacion(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> pRespuestas)
        {

            if (pRespuestas.Count > 0)
            {
                txtRespA1.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0001")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0001")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0001")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "";
                txtRespB1.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0002")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0002")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0002")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "";
                txtRespC1.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0003")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0003")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0003")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "";
                txtRespD1.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0004")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0004")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0004")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "";
                txtRespE1.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0005")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0005")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0005")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "";
                txtRespF1.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0006")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0006")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0006")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "";
                txtRespG1.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0007")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0007")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0007")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "";
                txtRespH1.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0008")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0008")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0008")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "";
                txtRespI1.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0009")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0009")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0009")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "";
                txtRespJ1.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0010")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0010")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0010")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "";
                txtRespK1.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0011")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0011")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0011")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "";
                txtRespL1.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0012")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0012")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0012")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "";
                txtRespA2.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0013")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0013")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0013")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "";
                txtRespB2.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0014")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0014")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0014")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "";
                txtRespC2.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0015")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0015")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0015")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "";
                txtRespD2.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0016")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0016")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0016")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "";
                txtRespE2.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0017")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0017")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0017")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "";
                txtRespF2.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0018")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0018")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0018")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "";
                txtRespG2.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0019")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0019")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0019")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "";
                txtRespH2.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0020")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0020")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0020")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "";
                txtRespI2.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0021")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0021")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0021")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "";
                txtRespJ2.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0022")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0022")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0022")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "";
                txtRespK2.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0023")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0023")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0023")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "";
                txtRespL2.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0024")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0024")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.Equals("LABORAL2-RES-0024")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "";

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
                vPruebaTerminada.NB_TIPO_PRUEBA = "MANUAL";
                E_RESULTADO vResultado = nKprueba.InsertaActualiza_K_PRUEBA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);

                SaveTest();
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "No ha ingresado todos los valores.", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, "");
            }
        }

        public void SaveTest()
        {
            AsignarValorRespuestas("LABORAL2-RES-0001", (!txtRespA1.Text.Equals("")) ? int.Parse(txtRespA1.Text) : 0);
            AsignarValorRespuestas("LABORAL2-RES-0002", (!txtRespB1.Text.Equals("")) ? int.Parse(txtRespB1.Text) : 0);
            AsignarValorRespuestas("LABORAL2-RES-0003", (!txtRespC1.Text.Equals("")) ? int.Parse(txtRespC1.Text) : 0);
            AsignarValorRespuestas("LABORAL2-RES-0004", (!txtRespD1.Text.Equals("")) ? int.Parse(txtRespD1.Text) : 0);
            AsignarValorRespuestas("LABORAL2-RES-0005", (!txtRespE1.Text.Equals("")) ? int.Parse(txtRespE1.Text) : 0);
            AsignarValorRespuestas("LABORAL2-RES-0006", (!txtRespF1.Text.Equals("")) ? int.Parse(txtRespF1.Text) : 0);
            AsignarValorRespuestas("LABORAL2-RES-0007", (!txtRespG1.Text.Equals("")) ? int.Parse(txtRespG1.Text) : 0);
            AsignarValorRespuestas("LABORAL2-RES-0008", (!txtRespH1.Text.Equals("")) ? int.Parse(txtRespH1.Text) : 0);
            AsignarValorRespuestas("LABORAL2-RES-0009", (!txtRespI1.Text.Equals("")) ? int.Parse(txtRespI1.Text) : 0);
            AsignarValorRespuestas("LABORAL2-RES-0010", (!txtRespJ1.Text.Equals("")) ? int.Parse(txtRespJ1.Text) : 0);
            AsignarValorRespuestas("LABORAL2-RES-0011", (!txtRespK1.Text.Equals("")) ? int.Parse(txtRespK1.Text) : 0);
            AsignarValorRespuestas("LABORAL2-RES-0012", (!txtRespL1.Text.Equals("")) ? int.Parse(txtRespL1.Text) : 0);

            AsignarValorRespuestas("LABORAL2-RES-0013", (!txtRespA2.Text.Equals("")) ? int.Parse(txtRespA2.Text) : 0);
            AsignarValorRespuestas("LABORAL2-RES-0014", (!txtRespB2.Text.Equals("")) ? int.Parse(txtRespB2.Text) : 0);
            AsignarValorRespuestas("LABORAL2-RES-0015", (!txtRespC2.Text.Equals("")) ? int.Parse(txtRespC2.Text) : 0);
            AsignarValorRespuestas("LABORAL2-RES-0016", (!txtRespD2.Text.Equals("")) ? int.Parse(txtRespD2.Text) : 0);
            AsignarValorRespuestas("LABORAL2-RES-0017", (!txtRespE2.Text.Equals("")) ? int.Parse(txtRespE2.Text) : 0);
            AsignarValorRespuestas("LABORAL2-RES-0018", (!txtRespF2.Text.Equals("")) ? int.Parse(txtRespF2.Text) : 0);
            AsignarValorRespuestas("LABORAL2-RES-0019", (!txtRespG2.Text.Equals("")) ? int.Parse(txtRespG2.Text) : 0);
            AsignarValorRespuestas("LABORAL2-RES-0020", (!txtRespH2.Text.Equals("")) ? int.Parse(txtRespH2.Text) : 0);
            AsignarValorRespuestas("LABORAL2-RES-0021", (!txtRespI2.Text.Equals("")) ? int.Parse(txtRespI2.Text) : 0);
            AsignarValorRespuestas("LABORAL2-RES-0022", (!txtRespJ2.Text.Equals("")) ? int.Parse(txtRespJ2.Text) : 0);
            AsignarValorRespuestas("LABORAL2-RES-0023", (!txtRespK2.Text.Equals("")) ? int.Parse(txtRespK2.Text) : 0);
            AsignarValorRespuestas("LABORAL2-RES-0024", (!txtRespL2.Text.Equals("")) ? int.Parse(txtRespL2.Text) : 0);


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
                E_RESULTADO vResultado = negRes.insertaResultadosLaboral2(RESPUESTAS.ToString(), null, vIdPrueba, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "CloseTest");
            }
        }

        public void AsignarValorRespuestas(string pClVariable, int pnbRespuesta)
        {
            E_PRUEBA_RESULTADO vResultado = new E_PRUEBA_RESULTADO();

            vResultado.NO_VALOR = pnbRespuesta;
            vResultado.CL_VARIABLE = pClVariable;
            vResultado.NO_VALOR_RES = pnbRespuesta;
            vPruebaResultado.Add(vResultado);
        }

        private bool validarCamposVacios()
        {
            bool continua = false;

            if (!String.IsNullOrEmpty(txtRespA1.Text) && !String.IsNullOrEmpty(txtRespB1.Text) && !String.IsNullOrEmpty(txtRespC1.Text) && !String.IsNullOrEmpty(txtRespD1.Text) && !String.IsNullOrEmpty(txtRespE1.Text) && !String.IsNullOrEmpty(txtRespF1.Text) &&
                !String.IsNullOrEmpty(txtRespG1.Text) && !String.IsNullOrEmpty(txtRespH1.Text) && !String.IsNullOrEmpty(txtRespI1.Text) && !String.IsNullOrEmpty(txtRespJ1.Text) && !String.IsNullOrEmpty(txtRespK1.Text) && !String.IsNullOrEmpty(txtRespL1.Text) &&
                !String.IsNullOrEmpty(txtRespA2.Text) && !String.IsNullOrEmpty(txtRespB2.Text) && !String.IsNullOrEmpty(txtRespC2.Text) && !String.IsNullOrEmpty(txtRespD2.Text) && !String.IsNullOrEmpty(txtRespE2.Text) && !String.IsNullOrEmpty(txtRespF2.Text) &&
                !String.IsNullOrEmpty(txtRespG2.Text) && !String.IsNullOrEmpty(txtRespH2.Text) && !String.IsNullOrEmpty(txtRespI2.Text) && !String.IsNullOrEmpty(txtRespJ2.Text) && !String.IsNullOrEmpty(txtRespK2.Text) && !String.IsNullOrEmpty(txtRespL2.Text)
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