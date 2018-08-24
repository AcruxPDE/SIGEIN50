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
using Telerik.Web.UI;

namespace SIGE.WebApp.IDP
{
    public partial class VentanaAptitudMentalIIManual : System.Web.UI.Page
    {

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


        public int vTiempoPrueba
        {
            get { return (int)ViewState["vsPLaboral2seconds"]; }
            set { ViewState["vsPLaboral2seconds"] = value; }
        }

        public string vEstatusPrueba;

        private List<E_PRUEBA_RESULTADO> vRespuestas
        {
            get { return (List<E_PRUEBA_RESULTADO>)ViewState["vsRespuestas"]; }
            set { ViewState["vsRespuestas"] = value; }
        }

        private Guid vClToken
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
                    //if (vObjetoPrueba != null)
                    //{
                        //if (vObjetoPrueba.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.ERROR)
                        //{
                            PruebasNegocio nPruebas = new PruebasNegocio();
                            var prueba = nPruebas.Obtener_RESULTADO_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).ToList();
                            var vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken).FirstOrDefault();
                            if (prueba != null)
                            {
                                if (vPrueba.FE_INICIO != null && vPrueba.FE_TERMINO != null)
                                {
                                    TimeSpan vDiferenciaTiempo = (DateTime)vPrueba.FE_TERMINO - (DateTime)vPrueba.FE_INICIO;
                                    double totalMinutes = vDiferenciaTiempo.TotalMinutes;
                                    txtnMinutosLaboral2.Text = totalMinutes.ToString();
                                }
                                if (vPrueba.NB_TIPO_PRUEBA == "MANUAL")
                                    AsignarRespuestasTextBox(prueba);
                                else
                                 AsignarRespuestasTextBoxAplicacion(prueba);
                                
                            //}
                        //}
                        //else if (vObjetoPrueba.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                        //{
                        //    vTiempoPrueba = int.Parse(vObjetoPrueba.MENSAJE.Where(r => r.CL_IDIOMA.Equals("ES")).FirstOrDefault().DS_MENSAJE.ToString());
                        //}
                    }
                }
                vRespuestas = new List<E_PRUEBA_RESULTADO>();
            }
        }

        public void AsignarRespuestasTextBox(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> pResultados) 
        {
            if (pResultados.Count > 0)
            {
                txtPregunta1.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0001")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0001")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0001")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta2.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0002")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0002")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0002")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta3.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0003")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0003")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0003")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta4.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0004")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0004")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0004")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta5.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0005")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0005")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0005")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta6.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0006")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0006")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0006")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta7.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0007")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0007")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0007")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta8.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0008")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0008")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0008")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta9.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0009")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0009")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0009")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta10.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0010")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0010")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0010")).FirstOrDefault().NB_RESULTADO.ToString()) : "");

                txtPregunta11.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0011")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0011")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0011")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta12.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0012")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0012")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0012")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta13.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0013")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0013")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0013")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta14.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0014")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0014")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0014")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta15.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0015")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0015")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0015")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta16.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0016")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0016")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0016")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta17.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0017")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0017")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0017")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta18.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0018")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0018")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0018")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta19.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0019")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0019")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0019")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta20.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0020")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0020")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0020")).FirstOrDefault().NB_RESULTADO.ToString()) : "");

                txtPregunta21.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0021")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0021")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0021")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta22.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0022")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0022")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0022")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta23.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0023")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0023")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0023")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta24.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0024")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0024")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0024")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta25.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0025")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0025")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0025")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta26.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0026")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0026")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0026")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta27.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0027")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0027")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0027")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta28.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0028")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0028")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0028")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta29.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0029")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0029")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0029")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta30.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0030")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0030")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0030")).FirstOrDefault().NB_RESULTADO.ToString()) : "");

                txtPregunta31.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0031")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0031")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0031")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta32.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0032")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0032")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0032")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta33.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0033")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0033")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0033")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta34.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0034")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0034")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0034")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta35.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0035")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0035")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0035")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta36.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0036")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0036")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0036")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta37.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0037")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0037")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0037")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta38.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0038")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0038")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0038")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta39.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0039")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0039")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0039")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta40.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0040")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0040")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0040")).FirstOrDefault().NB_RESULTADO.ToString()) : "");

                txtPregunta41.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0041")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0041")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0041")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta42.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0042")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0042")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0042")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta43.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0043")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0043")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0043")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta44.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0044")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0044")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0044")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta45.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0045")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0045")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0045")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta46.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0046")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0046")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0046")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta47.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0047")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0047")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0047")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta48.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0048")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0048")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0048")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta49.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0049")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0049")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0049")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta50.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0050")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0050")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0050")).FirstOrDefault().NB_RESULTADO.ToString()) : "");

                txtPregunta51.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0051")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0051")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0051")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta52.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0052")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0052")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0052")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta53.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0053")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0053")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0053")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta54.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0054")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0054")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0054")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta55.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0055")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0055")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0055")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta56.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0056")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0056")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0056")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta57.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0057")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0057")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0057")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta58.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0058")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0058")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0058")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta59.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0059")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0059")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0059")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta60.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0060")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0060")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0060")).FirstOrDefault().NB_RESULTADO.ToString()) : "");

                txtPregunta61.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0061")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0061")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0061")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta62.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0062")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0062")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0062")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta63.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0063")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0063")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0063")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta64.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0064")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0064")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0064")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta65.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0065")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0065")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0065")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta66.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0066")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0066")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0066")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta67.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0067")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0067")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0067")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta68.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0068")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0068")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0068")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta69.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0069")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0069")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0069")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta70.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0070")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0070")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0070")).FirstOrDefault().NB_RESULTADO.ToString()) : "");

                txtPregunta71.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0071")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0071")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0071")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta72.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0072")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0072")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0072")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta73.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0073")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0073")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0073")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta74.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0074")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0074")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0074")).FirstOrDefault().NB_RESULTADO.ToString()) : "");
                txtPregunta75.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0075")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0075")).FirstOrDefault().NB_RESULTADO != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0075")).FirstOrDefault().NB_RESULTADO.ToString()) : "");

                //if (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-RES-0001")).FirstOrDefault().NB_RESULTADO == null)
                //    btnTerminar.Enabled = false;
            }

        }

        public void AsignarRespuestasTextBoxAplicacion(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> pResultados)
        {
            if (pResultados.Count > 0)
            {
                txtPregunta1.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0001")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0001")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0001")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta2.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0002")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0002")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0002")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta3.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0003")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0003")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0003")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta4.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0004")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0004")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0004")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta5.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0005")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0005")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0005")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta6.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0006")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0006")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0006")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta7.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0007")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0007")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0007")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta8.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0008")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0008")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0008")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta9.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0009")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0009")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0009")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta10.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0010")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0010")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0010")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");

                txtPregunta11.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0011")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0011")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0011")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta12.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0012")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0012")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0012")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta13.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0013")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0013")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0013")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta14.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0014")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0014")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0014")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta15.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0015")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0015")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0015")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta16.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0016")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0016")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0016")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta17.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0017")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0017")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0017")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta18.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0018")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0018")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0018")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta19.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0019")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0019")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0019")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta20.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0020")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0020")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0020")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");

                txtPregunta21.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0021")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0021")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0021")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta22.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0022")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0022")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0022")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta23.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0023")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0023")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0023")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta24.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0024")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0024")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0024")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta25.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0025")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0025")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0025")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta26.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0026")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0026")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0026")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta27.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0027")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0027")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0027")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta28.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0028")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0028")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0028")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta29.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0029")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0029")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0029")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta30.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0030")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0030")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0030")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");

                txtPregunta31.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0031")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0031")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0031")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta32.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0032")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0032")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0032")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta33.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0033")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0033")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0033")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta34.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0034")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0034")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0034")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta35.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0035")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0035")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0035")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta36.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0036")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0036")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0036")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta37.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0037")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0037")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0037")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta38.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0038")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0038")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0038")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta39.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0039")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0039")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0039")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta40.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0040")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0040")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0040")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");

                txtPregunta41.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0041")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0041")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0041")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta42.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0042")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0042")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0042")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta43.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0043")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0043")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0043")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta44.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0044")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0044")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0044")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta45.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0045")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0045")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0045")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta46.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0046")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0046")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0046")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta47.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0047")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0047")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0047")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta48.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0048")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0048")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0048")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta49.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0049")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0049")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0049")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta50.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0050")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0050")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0050")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");

                txtPregunta51.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0051")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0051")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0051")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta52.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0052")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0052")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0052")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta53.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0053")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0053")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0053")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta54.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0054")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0054")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0054")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta55.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0055")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0055")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0055")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta56.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0056")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0056")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0056")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta57.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0057")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0057")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0057")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta58.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0058")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0058")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0058")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta59.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0059")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0059")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0059")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta60.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0060")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0060")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0060")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");

                txtPregunta61.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0061")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0061")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0061")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta62.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0062")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0062")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0062")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta63.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0063")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0063")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0063")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta64.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0064")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0064")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0064")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta65.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0065")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0065")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0065")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta66.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0066")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0066")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0066")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta67.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0067")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0067")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0067")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta68.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0068")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0068")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0068")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta69.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0069")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0069")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0069")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta70.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0070")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0070")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0070")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");

                txtPregunta71.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0071")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0071")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0071")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta72.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0072")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0072")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0072")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta73.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0073")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0073")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0073")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta74.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0074")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0074")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0074")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
                txtPregunta75.Text = CambiarNumeroPorLetra((pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0075")).FirstOrDefault() != null && pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0075")).FirstOrDefault().NB_RESPUESTA != null) ? (pResultados.Where(w => w.CL_PREGUNTA.Equals("APTITUD2-A-0075")).FirstOrDefault().NB_RESPUESTA.ToString()) : "");
            }
        }

        public string CambiarNumeroPorLetra(string valor)
        {
            if (valor != "")
            {
                string letra = "";
                switch (valor.ToLower())
                {
                    case "1": letra = "a"; break;
                    case "2": letra = "b"; break;
                    case "3": letra = "c"; break;
                    case "4": letra = "d"; break;
                    case "5": letra = "e"; break;
                    case "0": letra = ""; break;
                    case "a": letra = "a"; break;
                    case "b": letra = "b"; break;
                    case "c": letra = "c"; break;
                    case "d": letra = "d"; break;
                    case "e": letra = "e"; break;

                }
                return letra.ToLower();
            }
            else
            {
                return string.Empty;
            }
        }

        public void SaveTest()
        {

            String APTITUD2_RES_0001 = txtPregunta1.Text;
            BackQuestionObject("APTITUD2-RES-0001", APTITUD2_RES_0001);

            String APTITUD2_RES_0002 = txtPregunta2.Text;
            BackQuestionObject("APTITUD2-RES-0002", APTITUD2_RES_0002);

            String APTITUD2_RES_0003 = txtPregunta3.Text;
            BackQuestionObject("APTITUD2-RES-0003", APTITUD2_RES_0003);

            String APTITUD2_RES_0004 = txtPregunta4.Text;
            BackQuestionObject("APTITUD2-RES-0004", APTITUD2_RES_0004);

            String APTITUD2_RES_0005 = txtPregunta5.Text;
            BackQuestionObject("APTITUD2-RES-0005", APTITUD2_RES_0005);

            String APTITUD2_RES_0006 = txtPregunta6.Text;
            BackQuestionObject("APTITUD2-RES-0006", APTITUD2_RES_0006);

            String APTITUD2_RES_0007 = txtPregunta7.Text;
            BackQuestionObject("APTITUD2-RES-0007", APTITUD2_RES_0007);

            String APTITUD2_RES_0008 = txtPregunta8.Text;
            BackQuestionObject("APTITUD2-RES-0008", APTITUD2_RES_0008);

            String APTITUD2_RES_0009 = txtPregunta9.Text;
            BackQuestionObject("APTITUD2-RES-0009", APTITUD2_RES_0009);

            String APTITUD2_RES_0010 = txtPregunta10.Text;
            BackQuestionObject("APTITUD2-RES-0010", APTITUD2_RES_0010);

            String APTITUD2_RES_0011 = txtPregunta11.Text;
            BackQuestionObject("APTITUD2-RES-0011", APTITUD2_RES_0011);

            String APTITUD2_RES_0012 = txtPregunta12.Text;
            BackQuestionObject("APTITUD2-RES-0012", APTITUD2_RES_0012);

            String APTITUD2_RES_0013 = txtPregunta13.Text;
            BackQuestionObject("APTITUD2-RES-0013", APTITUD2_RES_0013);

            String APTITUD2_RES_0014 = txtPregunta14.Text;
            BackQuestionObject("APTITUD2-RES-0014", APTITUD2_RES_0014);

            String APTITUD2_RES_0015 = txtPregunta15.Text;
            BackQuestionObject("APTITUD2-RES-0015", APTITUD2_RES_0015);

            String APTITUD2_RES_0016 = txtPregunta16.Text;
            BackQuestionObject("APTITUD2-RES-0016", APTITUD2_RES_0016);

            String APTITUD2_RES_0017 = txtPregunta17.Text;
            BackQuestionObject("APTITUD2-RES-0017", APTITUD2_RES_0017);

            String APTITUD2_RES_0018 = txtPregunta18.Text;
            BackQuestionObject("APTITUD2-RES-0018", APTITUD2_RES_0018);

            String APTITUD2_RES_0019 = txtPregunta19.Text;
            BackQuestionObject("APTITUD2-RES-0019", APTITUD2_RES_0019);

            String APTITUD2_RES_0020 = txtPregunta20.Text;
            BackQuestionObject("APTITUD2-RES-0020", APTITUD2_RES_0020);

            String APTITUD2_RES_0021 = txtPregunta21.Text;
            BackQuestionObject("APTITUD2-RES-0021", APTITUD2_RES_0021);

            String APTITUD2_RES_0022 = txtPregunta22.Text;
            BackQuestionObject("APTITUD2-RES-0022", APTITUD2_RES_0022);

            String APTITUD2_RES_0023 = txtPregunta23.Text;
            BackQuestionObject("APTITUD2-RES-0023", APTITUD2_RES_0023);

            String APTITUD2_RES_0024 = txtPregunta24.Text;
            BackQuestionObject("APTITUD2-RES-0024", APTITUD2_RES_0024);

            String APTITUD2_RES_0025 = txtPregunta25.Text;
            BackQuestionObject("APTITUD2-RES-0025", APTITUD2_RES_0025);

            String APTITUD2_RES_0026 = txtPregunta26.Text;
            BackQuestionObject("APTITUD2-RES-0026", APTITUD2_RES_0026);

            String APTITUD2_RES_0027 = txtPregunta27.Text;
            BackQuestionObject("APTITUD2-RES-0027", APTITUD2_RES_0027);

            String APTITUD2_RES_0028 = txtPregunta28.Text;
            BackQuestionObject("APTITUD2-RES-0028", APTITUD2_RES_0028);

            String APTITUD2_RES_0029 = txtPregunta29.Text;
            BackQuestionObject("APTITUD2-RES-0029", APTITUD2_RES_0029);

            String APTITUD2_RES_0030 = txtPregunta30.Text;
            BackQuestionObject("APTITUD2-RES-0030", APTITUD2_RES_0030);

            String APTITUD2_RES_0031 = txtPregunta31.Text;
            BackQuestionObject("APTITUD2-RES-0031", APTITUD2_RES_0031);

            String APTITUD2_RES_0032 = txtPregunta32.Text;
            BackQuestionObject("APTITUD2-RES-0032", APTITUD2_RES_0032);

            String APTITUD2_RES_0033 = txtPregunta33.Text;
            BackQuestionObject("APTITUD2-RES-0033", APTITUD2_RES_0033);

            String APTITUD2_RES_0034 = txtPregunta34.Text;
            BackQuestionObject("APTITUD2-RES-0034", APTITUD2_RES_0034);

            String APTITUD2_RES_0035 = txtPregunta35.Text;
            BackQuestionObject("APTITUD2-RES-0035", APTITUD2_RES_0035);

            String APTITUD2_RES_0036 = txtPregunta36.Text;
            BackQuestionObject("APTITUD2-RES-0036", APTITUD2_RES_0036);

            String APTITUD2_RES_0037 = txtPregunta37.Text;
            BackQuestionObject("APTITUD2-RES-0037", APTITUD2_RES_0037);

            String APTITUD2_RES_0038 = txtPregunta38.Text;
            BackQuestionObject("APTITUD2-RES-0038", APTITUD2_RES_0038);

            String APTITUD2_RES_0039 = txtPregunta39.Text;
            BackQuestionObject("APTITUD2-RES-0039", APTITUD2_RES_0039);

            String APTITUD2_RES_0040 = txtPregunta40.Text;
            BackQuestionObject("APTITUD2-RES-0040", APTITUD2_RES_0040);

            String APTITUD2_RES_0041 = txtPregunta41.Text;
            BackQuestionObject("APTITUD2-RES-0041", APTITUD2_RES_0041);

            String APTITUD2_RES_0042 = txtPregunta42.Text;
            BackQuestionObject("APTITUD2-RES-0042", APTITUD2_RES_0042);

            String APTITUD2_RES_0043 = txtPregunta43.Text;
            BackQuestionObject("APTITUD2-RES-0043", APTITUD2_RES_0043);

            String APTITUD2_RES_0044 = txtPregunta44.Text;
            BackQuestionObject("APTITUD2-RES-0044", APTITUD2_RES_0044);

            String APTITUD2_RES_0045 = txtPregunta45.Text;
            BackQuestionObject("APTITUD2-RES-0045", APTITUD2_RES_0045);

            String APTITUD2_RES_0046 = txtPregunta46.Text;
            BackQuestionObject("APTITUD2-RES-0046", APTITUD2_RES_0046);

            String APTITUD2_RES_0047 = txtPregunta47.Text;
            BackQuestionObject("APTITUD2-RES-0047", APTITUD2_RES_0047);

            String APTITUD2_RES_0048 = txtPregunta48.Text;
            BackQuestionObject("APTITUD2-RES-0048", APTITUD2_RES_0048);

            String APTITUD2_RES_0049 = txtPregunta49.Text;
            BackQuestionObject("APTITUD2-RES-0049", APTITUD2_RES_0049);

            String APTITUD2_RES_0050 = txtPregunta50.Text;
            BackQuestionObject("APTITUD2-RES-0050", APTITUD2_RES_0050);

            String APTITUD2_RES_0051 = txtPregunta51.Text;
            BackQuestionObject("APTITUD2-RES-0051", APTITUD2_RES_0051);

            String APTITUD2_RES_0052 = txtPregunta52.Text;
            BackQuestionObject("APTITUD2-RES-0052", APTITUD2_RES_0052);

            String APTITUD2_RES_0053 = txtPregunta53.Text;
            BackQuestionObject("APTITUD2-RES-0053", APTITUD2_RES_0053);

            String APTITUD2_RES_0054 = txtPregunta54.Text;
            BackQuestionObject("APTITUD2-RES-0054", APTITUD2_RES_0054);

            String APTITUD2_RES_0055 = txtPregunta55.Text;
            BackQuestionObject("APTITUD2-RES-0055", APTITUD2_RES_0055);

            String APTITUD2_RES_0056 = txtPregunta56.Text;
            BackQuestionObject("APTITUD2-RES-0056", APTITUD2_RES_0056);

            String APTITUD2_RES_0057 = txtPregunta57.Text;
            BackQuestionObject("APTITUD2-RES-0057", APTITUD2_RES_0057);

            String APTITUD2_RES_0058 = txtPregunta58.Text;
            BackQuestionObject("APTITUD2-RES-0058", APTITUD2_RES_0058);

            String APTITUD2_RES_0059 = txtPregunta59.Text;
            BackQuestionObject("APTITUD2-RES-0059", APTITUD2_RES_0059);

            String APTITUD2_RES_0060 = txtPregunta60.Text;
            BackQuestionObject("APTITUD2-RES-0060", APTITUD2_RES_0060);

            String APTITUD2_RES_0061 = txtPregunta61.Text;
            BackQuestionObject("APTITUD2-RES-0061", APTITUD2_RES_0061);

            String APTITUD2_RES_0062 = txtPregunta62.Text;
            BackQuestionObject("APTITUD2-RES-0062", APTITUD2_RES_0062);

            String APTITUD2_RES_0063 = txtPregunta63.Text;
            BackQuestionObject("APTITUD2-RES-0063", APTITUD2_RES_0063);

            String APTITUD2_RES_0064 = txtPregunta64.Text;
            BackQuestionObject("APTITUD2-RES-0064", APTITUD2_RES_0064);

            String APTITUD2_RES_0065 = txtPregunta65.Text;
            BackQuestionObject("APTITUD2-RES-0065", APTITUD2_RES_0065);

            String APTITUD2_RES_0066 = txtPregunta66.Text;
            BackQuestionObject("APTITUD2-RES-0066", APTITUD2_RES_0066);

            String APTITUD2_RES_0067 = txtPregunta67.Text;
            BackQuestionObject("APTITUD2-RES-0067", APTITUD2_RES_0067);

            String APTITUD2_RES_0068 = txtPregunta68.Text;
            BackQuestionObject("APTITUD2-RES-0068", APTITUD2_RES_0068);

            String APTITUD2_RES_0069 = txtPregunta69.Text;
            BackQuestionObject("APTITUD2-RES-0069", APTITUD2_RES_0069);

            String APTITUD2_RES_0070 = txtPregunta70.Text;
            BackQuestionObject("APTITUD2-RES-0070", APTITUD2_RES_0070);

            String APTITUD2_RES_0071 = txtPregunta71.Text;
            BackQuestionObject("APTITUD2-RES-0071", APTITUD2_RES_0071);

            String APTITUD2_RES_0072 = txtPregunta72.Text;
            BackQuestionObject("APTITUD2-RES-0072", APTITUD2_RES_0072);

            String APTITUD2_RES_0073 = txtPregunta73.Text;
            BackQuestionObject("APTITUD2-RES-0073", APTITUD2_RES_0073);

            String APTITUD2_RES_0074 = txtPregunta74.Text;
            BackQuestionObject("APTITUD2-RES-0074", APTITUD2_RES_0074);

            String APTITUD2_RES_0075 = txtPregunta75.Text;
            BackQuestionObject("APTITUD2-RES-0075", APTITUD2_RES_0075);

            var vXelements = vRespuestas.Select(x =>
                                                 new XElement("RESULTADO",
                                                 new XAttribute("CL_VARIABLE", x.CL_VARIABLE),
                                                 new XAttribute("NO_VALOR", x.NO_VALOR),
                                                 new XAttribute("NO_VALOR_RES", x.NO_VALOR_RES)
                                      ));
            XElement RESPUESTAS =
            new XElement("RESULTADOS", vXelements
            );

                ResultadoNegocio nResultado = new ResultadoNegocio();
                PruebasNegocio nKprueba = new PruebasNegocio();
                var vObjetoPrueba = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();

                if (vObjetoPrueba != null)
                {
                   
                    E_RESULTADO vResultado = nResultado.insertaResultadosAptitud2(RESPUESTAS.ToString(), null, vIdPrueba, vClUsuario, vNbPrograma);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "CloseTest");
                }
        }

        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            PruebasNegocio nKprueba = new PruebasNegocio();
            SPE_OBTIENE_K_PRUEBA_Result vPruebaTerminada = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();
            vPruebaTerminada.FE_INICIO = DateTime.Now;
            decimal vMinutos = (decimal)(txtnMinutosLaboral2.Value);
            vPruebaTerminada.FE_TERMINO = DateTime.Now.AddSeconds((int)(vMinutos*60));
            vPruebaTerminada.CL_ESTADO = E_ESTADO_PRUEBA.TERMINADA.ToString();
            vPruebaTerminada.NB_TIPO_PRUEBA = "MANUAL";
            E_RESULTADO vResultado = nKprueba.InsertaActualiza_K_PRUEBA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
            SaveTest();
        }

        public void BackQuestionObject(string pClVariable, string pnbRespuesta)
        {
            E_PRUEBA_RESULTADO vResultado = new E_PRUEBA_RESULTADO();
                int vNoValor=0;
            switch (pnbRespuesta.ToLower())
            {
                case "a":
                    vNoValor = 1;
                    break;
                case "b":
                    vNoValor = 2;
                    break;
                case "c":
                    vNoValor = 3;
                    break;
                case "d":
                    vNoValor = 4;
                    break;
                case "e":
                    vNoValor = 5;
                    break;

                default:
                    vNoValor = 0;
                    break;

            }

                vResultado.CL_VARIABLE = pClVariable;
                vResultado.NO_VALOR = vNoValor;
                vResultado.NO_VALOR_RES = vNoValor;
                vRespuestas.Add(vResultado);
        }

     

    }
}