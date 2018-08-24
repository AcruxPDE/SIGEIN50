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
    public partial class VentanaAptitudMentalIManual : System.Web.UI.Page
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

            if (txtapartado1_4.Text != "")
                if (txtapartado1_4.Text != "0")
                    lblapartado1_2.InnerText = (Decimal.Round((decimal)100 / 16 * (Convert.ToDecimal(txtapartado1_4.Text)), 2)).ToString();
                else
                    lblapartado1_2.InnerText = "0.00";

            if (txtapartado2_4.Text != "")
                if (txtapartado2_4.Text != "0") 
                lblapartado2_2.InnerText = (Decimal.Round((decimal)100 / 22 * (Convert.ToDecimal(txtapartado2_4.Text)), 2)).ToString();
                else
                    lblapartado2_2.InnerText = "0.00";

            if (txtapartado3_4.Text != "")
                if (txtapartado3_4.Text != "0")
                lblapartado3_2.InnerText = (Decimal.Round((decimal)100 / 30 * (Convert.ToDecimal(txtapartado3_4.Text)), 2)).ToString();
                else
                    lblapartado3_2.InnerText = "0.00";

            if (txtapartado4_4.Text != "")
                if (txtapartado4_4.Text != "0")
                lblapartado4_2.InnerText = (Decimal.Round((decimal)100 / 18 * (Convert.ToDecimal(txtapartado4_4.Text)), 2)).ToString();
                else
                    lblapartado4_2.InnerText = "0.00";

            if (txtapartado5_4.Text != "")
                if (txtapartado5_4.Text != "0")
                lblapartado5_2.InnerText = (Decimal.Round((decimal)100 / 24 * (Convert.ToDecimal(txtapartado5_4.Text)), 2)).ToString();
                else
                    lblapartado5_2.InnerText = "0.00";

            if (txtapartado6_4.Text != "")
                if (txtapartado6_4.Text != "0") 
                lblapartado6_2.InnerText = (Decimal.Round((decimal)100 / 20 * (Convert.ToDecimal(txtapartado6_4.Text)), 2)).ToString() + ".00";
                else
                    lblapartado6_2.InnerText = "0.00";

            if (txtapartado7_4.Text != "")
                if (txtapartado7_4.Text != "0") 
                lblapartado7_2.InnerText = (Decimal.Round((decimal)100 / 20 * (Convert.ToDecimal(txtapartado7_4.Text)), 2)).ToString() + ".00";
                else
                    lblapartado7_2.InnerText = "0.00";

            if (txtapartado8_4.Text != "")
                if (txtapartado8_4.Text != "0") 
                lblapartado8_2.InnerText = (Decimal.Round((decimal)100 / 17 * (Convert.ToDecimal(txtapartado8_4.Text)), 2)).ToString();
                else
                    lblapartado8_2.InnerText = "0.00";

            if (txtapartado9_4.Text != "")
                if (txtapartado9_4.Text != "0")
                lblapartado9_2.InnerText = (Decimal.Round((decimal)100 / 18 * (Convert.ToDecimal(txtapartado9_4.Text)), 2)).ToString();
                else
                    lblapartado9_2.InnerText = "0.00";

            if (txtapartado10_4.Text != "")
                if (txtapartado10_4.Text != "0")
                lblapartado10_2.InnerText = (Decimal.Round((decimal)100 / 22 * (Convert.ToDecimal(txtapartado10_4.Text)), 2)).ToString();
                else
                    lblapartado10_2.InnerText = "0.00";

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
                            var vPrueba = nPruebas.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();
                            if (prueba != null)
                            {
                                if (vPrueba.CL_ESTADO == "TERMINADA")
                                {
                                    if (vPrueba.NB_TIPO_PRUEBA == "APLICACION")
                                        AsignarRespuestasAplicacion(prueba);
                                    else
                                        AsignarRespuestasTextBox(prueba);
                                }
                            }
                        }
                    //    else if (vObjetoPrueba.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                    //    {
                    //        vTiempoPrueba = int.Parse(vObjetoPrueba.MENSAJE.Where(r => r.CL_IDIOMA.Equals("ES")).FirstOrDefault().DS_MENSAJE.ToString());
                    //    }
                    //}
                }
                vRespuestas = new List<E_PRUEBA_RESULTADO>();
                
           // }
        }


        public void AsignarRespuestasTextBox(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> pRespuestas) 
        {
            if (pRespuestas.Count > 0) 
            {
               
                //aciertos
                txtapartado1_4.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0001")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0001")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0001")).FirstOrDefault().NB_RESULTADO.ToString() : "0";
                txtapartado2_4.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0002")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0002")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0002")).FirstOrDefault().NB_RESULTADO.ToString() : "0";
                txtapartado3_4.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0003")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0003")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0003")).FirstOrDefault().NB_RESULTADO.ToString() : "0";
                txtapartado4_4.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0004")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0004")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0004")).FirstOrDefault().NB_RESULTADO.ToString() : "0";
                txtapartado5_4.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0005")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0005")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0005")).FirstOrDefault().NB_RESULTADO.ToString() : "0";
                txtapartado6_4.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0006")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0006")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0006")).FirstOrDefault().NB_RESULTADO.ToString() : "0";
                txtapartado7_4.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0007")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0007")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0007")).FirstOrDefault().NB_RESULTADO.ToString() : "0";
                txtapartado8_4.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0008")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0008")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0008")).FirstOrDefault().NB_RESULTADO.ToString() : "0";
                txtapartado9_4.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0009")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0009")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0009")).FirstOrDefault().NB_RESULTADO.ToString() : "0";
                txtapartado10_4.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0010")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0010")).FirstOrDefault().NB_RESULTADO != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0010")).FirstOrDefault().NB_RESULTADO.ToString() : "0.";

                //porcentajes
                //lblapartado1_2.InnerText = (Decimal.Round((decimal)100 / 16 * (Convert.ToDecimal(txtapartado1_4.Text)), 2)).ToString();
                //lblapartado2_2.InnerText = (Decimal.Round((decimal)100 / 22 * (Convert.ToDecimal(txtapartado2_4.Text)), 2)).ToString();
                //lblapartado3_2.InnerText = (Decimal.Round((decimal)100 / 30 * (Convert.ToDecimal(txtapartado3_4.Text)), 2)).ToString();
                //lblapartado4_2.InnerText = (Decimal.Round((decimal)100 / 18 * (Convert.ToDecimal(txtapartado4_4.Text)), 2)).ToString();
                //lblapartado5_2.InnerText = (Decimal.Round((decimal)100 / 24 * (Convert.ToDecimal(txtapartado5_4.Text)), 2)).ToString();
                //lblapartado6_2.InnerText = (Decimal.Round((decimal)100 / 20 * (Convert.ToDecimal(txtapartado6_4.Text)), 2)).ToString() + ".00"; 
                //lblapartado7_2.InnerText = (Decimal.Round((decimal)100 / 20 * (Convert.ToDecimal(txtapartado7_4.Text)), 2)).ToString() + ".00"; 
                //lblapartado8_2.InnerText = (Decimal.Round((decimal)100 / 17 * (Convert.ToDecimal(txtapartado8_4.Text)), 2)).ToString();
                //lblapartado9_2.InnerText = (Decimal.Round((decimal)100 / 18 * (Convert.ToDecimal(txtapartado9_4.Text)), 2)).ToString();
                //lblapartado10_2.InnerText = (Decimal.Round((decimal)100 / 22 * (Convert.ToDecimal(txtapartado10_4.Text)), 2)).ToString();

                if (txtapartado1_4.Text != "")
                    if (txtapartado1_4.Text != "0")
                        lblapartado1_2.InnerText = (Decimal.Round((decimal)100 / 16 * (Convert.ToDecimal(txtapartado1_4.Text)), 2)).ToString();
                    else
                        lblapartado1_2.InnerText = "0.00";

                if (txtapartado2_4.Text != "")
                    if (txtapartado2_4.Text != "0")
                        lblapartado2_2.InnerText = (Decimal.Round((decimal)100 / 22 * (Convert.ToDecimal(txtapartado2_4.Text)), 2)).ToString();
                    else
                        lblapartado2_2.InnerText = "0.00";

                if (txtapartado3_4.Text != "")
                    if (txtapartado3_4.Text != "0")
                        lblapartado3_2.InnerText = (Decimal.Round((decimal)100 / 30 * (Convert.ToDecimal(txtapartado3_4.Text)), 2)).ToString();
                    else
                        lblapartado3_2.InnerText = "0.00";

                if (txtapartado4_4.Text != "")
                    if (txtapartado4_4.Text != "0")
                        lblapartado4_2.InnerText = (Decimal.Round((decimal)100 / 18 * (Convert.ToDecimal(txtapartado4_4.Text)), 2)).ToString();
                    else
                        lblapartado4_2.InnerText = "0.00";

                if (txtapartado5_4.Text != "")
                    if (txtapartado5_4.Text != "0")
                        lblapartado5_2.InnerText = (Decimal.Round((decimal)100 / 24 * (Convert.ToDecimal(txtapartado5_4.Text)), 2)).ToString();
                    else
                        lblapartado5_2.InnerText = "0.00";

                if (txtapartado6_4.Text != "")
                    if (txtapartado6_4.Text != "0")
                        lblapartado6_2.InnerText = (Decimal.Round((decimal)100 / 20 * (Convert.ToDecimal(txtapartado6_4.Text)), 2)).ToString() + ".00";
                    else
                        lblapartado6_2.InnerText = "0.00";

                if (txtapartado7_4.Text != "")
                    if (txtapartado7_4.Text != "0")
                        lblapartado7_2.InnerText = (Decimal.Round((decimal)100 / 20 * (Convert.ToDecimal(txtapartado7_4.Text)), 2)).ToString() + ".00";
                    else
                        lblapartado7_2.InnerText = "0.00";

                if (txtapartado8_4.Text != "")
                    if (txtapartado8_4.Text != "0")
                        lblapartado8_2.InnerText = (Decimal.Round((decimal)100 / 17 * (Convert.ToDecimal(txtapartado8_4.Text)), 2)).ToString();
                    else
                        lblapartado8_2.InnerText = "0.00";

                if (txtapartado9_4.Text != "")
                    if (txtapartado9_4.Text != "0")
                        lblapartado9_2.InnerText = (Decimal.Round((decimal)100 / 18 * (Convert.ToDecimal(txtapartado9_4.Text)), 2)).ToString();
                    else
                        lblapartado9_2.InnerText = "0.00";

                if (txtapartado10_4.Text != "")
                    if (txtapartado10_4.Text != "0")
                        lblapartado10_2.InnerText = (Decimal.Round((decimal)100 / 22 * (Convert.ToDecimal(txtapartado10_4.Text)), 2)).ToString();
                    else
                        lblapartado10_2.InnerText = "0.00";

                //if (pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0001")).FirstOrDefault().NB_RESULTADO == null)
                //    btnTerminar.Enabled = false;
            }
        }

        public void AsignarRespuestasAplicacion(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> pRespuestas)
        {
            if (pRespuestas.Count > 0)
            {
                txtapartado1_4.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0001")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0001")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0001")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "0";
                txtapartado2_4.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0002")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0002")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0002")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "0";
                txtapartado3_4.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0003")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0003")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0003")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "0";
                txtapartado4_4.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0004")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0004")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0004")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "0";
                txtapartado5_4.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0005")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0005")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0005")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "0";
                txtapartado6_4.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0006")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0006")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0006")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "0";
                txtapartado7_4.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0007")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0007")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0007")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "0";
                txtapartado8_4.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0008")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0008")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0008")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "0";
                txtapartado9_4.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0009")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0009")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0009")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "0";
                txtapartado10_4.Text = (pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0010")).FirstOrDefault() != null && pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0010")).FirstOrDefault().NO_VALOR_RESPUESTA != null) ? pRespuestas.Where(w => w.CL_PREGUNTA.ToString().Equals("APTITUD1-RES-0010")).FirstOrDefault().NO_VALOR_RESPUESTA.ToString() : "0";

                //porcentajes
                //lblapartado1_2.InnerText = (Decimal.Round((decimal)100 / 16 * (Convert.ToDecimal(txtapartado1_4.Text)), 2)).ToString();
                //lblapartado2_2.InnerText = (Decimal.Round((decimal)100 / 22 * (Convert.ToDecimal(txtapartado2_4.Text)), 2)).ToString();
                //lblapartado3_2.InnerText = (Decimal.Round((decimal)100 / 30 * (Convert.ToDecimal(txtapartado3_4.Text)), 2)).ToString();
                //lblapartado4_2.InnerText = (Decimal.Round((decimal)100 / 18 * (Convert.ToDecimal(txtapartado4_4.Text)), 2)).ToString();
                //lblapartado5_2.InnerText = (Decimal.Round((decimal)100 / 24 * (Convert.ToDecimal(txtapartado5_4.Text)), 2)).ToString();
                //lblapartado6_2.InnerText = (Decimal.Round((decimal)100 / 20 * (Convert.ToDecimal(txtapartado6_4.Text)), 2)).ToString() + ".00";
                //lblapartado7_2.InnerText = (Decimal.Round((decimal)100 / 20 * (Convert.ToDecimal(txtapartado7_4.Text)), 2)).ToString() + ".00";
                //lblapartado8_2.InnerText = (Decimal.Round((decimal)100 / 17 * (Convert.ToDecimal(txtapartado8_4.Text)), 2)).ToString();
                //lblapartado9_2.InnerText = (Decimal.Round((decimal)100 / 18 * (Convert.ToDecimal(txtapartado9_4.Text)), 2)).ToString();
                //lblapartado10_2.InnerText = (Decimal.Round((decimal)100 / 22 * (Convert.ToDecimal(txtapartado10_4.Text)), 2)).ToString();

                if (txtapartado1_4.Text != "")
                    if (txtapartado1_4.Text != "0")
                        lblapartado1_2.InnerText = (Decimal.Round((decimal)100 / 16 * (Convert.ToDecimal(txtapartado1_4.Text)), 2)).ToString();
                    else
                        lblapartado1_2.InnerText = "0.00";

                if (txtapartado2_4.Text != "")
                    if (txtapartado2_4.Text != "0")
                        lblapartado2_2.InnerText = (Decimal.Round((decimal)100 / 22 * (Convert.ToDecimal(txtapartado2_4.Text)), 2)).ToString();
                    else
                        lblapartado2_2.InnerText = "0.00";

                if (txtapartado3_4.Text != "")
                    if (txtapartado3_4.Text != "0")
                        lblapartado3_2.InnerText = (Decimal.Round((decimal)100 / 30 * (Convert.ToDecimal(txtapartado3_4.Text)), 2)).ToString();
                    else
                        lblapartado3_2.InnerText = "0.00";

                if (txtapartado4_4.Text != "")
                    if (txtapartado4_4.Text != "0")
                        lblapartado4_2.InnerText = (Decimal.Round((decimal)100 / 18 * (Convert.ToDecimal(txtapartado4_4.Text)), 2)).ToString();
                    else
                        lblapartado4_2.InnerText = "0.00";

                if (txtapartado5_4.Text != "")
                    if (txtapartado5_4.Text != "0")
                        lblapartado5_2.InnerText = (Decimal.Round((decimal)100 / 24 * (Convert.ToDecimal(txtapartado5_4.Text)), 2)).ToString();
                    else
                        lblapartado5_2.InnerText = "0.00";

                if (txtapartado6_4.Text != "")
                    if (txtapartado6_4.Text != "0")
                        lblapartado6_2.InnerText = (Decimal.Round((decimal)100 / 20 * (Convert.ToDecimal(txtapartado6_4.Text)), 2)).ToString() + ".00";
                    else
                        lblapartado6_2.InnerText = "0.00";

                if (txtapartado7_4.Text != "")
                    if (txtapartado7_4.Text != "0")
                        lblapartado7_2.InnerText = (Decimal.Round((decimal)100 / 20 * (Convert.ToDecimal(txtapartado7_4.Text)), 2)).ToString() + ".00";
                    else
                        lblapartado7_2.InnerText = "0.00";

                if (txtapartado8_4.Text != "")
                    if (txtapartado8_4.Text != "0")
                        lblapartado8_2.InnerText = (Decimal.Round((decimal)100 / 17 * (Convert.ToDecimal(txtapartado8_4.Text)), 2)).ToString();
                    else
                        lblapartado8_2.InnerText = "0.00";

                if (txtapartado9_4.Text != "")
                    if (txtapartado9_4.Text != "0")
                        lblapartado9_2.InnerText = (Decimal.Round((decimal)100 / 18 * (Convert.ToDecimal(txtapartado9_4.Text)), 2)).ToString();
                    else
                        lblapartado9_2.InnerText = "0.00";

                if (txtapartado10_4.Text != "")
                    if (txtapartado10_4.Text != "0")
                        lblapartado10_2.InnerText = (Decimal.Round((decimal)100 / 22 * (Convert.ToDecimal(txtapartado10_4.Text)), 2)).ToString();
                    else
                        lblapartado10_2.InnerText = "0.00";
            }
        }

        public void GuardarResultado()
        {

            //String APTITUD1_RES_0001 = txtapartado1_4.Text;
            //BackResultObject(97, int.Parse(APTITUD1_RES_0001));

            int APTITUD1_RES_0001 = (!txtapartado1_4.Text.Equals("")) ? int.Parse(txtapartado1_4.Text) : 0;
            BackResultObject("APTITUD1-RES-0001", APTITUD1_RES_0001);

            //String APTITUD1_RES_0002 = txtapartado2_4.Text;
            //BackResultObject(98, int.Parse(APTITUD1_RES_0002));

            int APTITUD1_RES_0002 = (!txtapartado2_4.Text.Equals("")) ? int.Parse(txtapartado2_4.Text) : 0;
            BackResultObject("APTITUD1-RES-0002", APTITUD1_RES_0002);

            //String APTITUD1_RES_0003 = txtapartado3_4.Text;
            //BackResultObject(99, int.Parse(APTITUD1_RES_0003));
            int APTITUD1_RES_0003 = (!txtapartado3_4.Text.Equals("")) ?int.Parse(txtapartado3_4.Text) : 0;
            BackResultObject("APTITUD1-RES-0003", APTITUD1_RES_0003);

            //String APTITUD1_RES_0004 = txtapartado4_4.Text;
            //BackResultObject(100, int.Parse(APTITUD1_RES_0004));
            int APTITUD1_RES_0004 = (!txtapartado4_4.Text.Equals("")) ?int.Parse(txtapartado4_4.Text) : 0;
            BackResultObject("APTITUD1-RES-0004", APTITUD1_RES_0004);

            //String APTITUD1_RES_0005 = txtapartado5_4.Text;
            //BackResultObject(101, int.Parse(APTITUD1_RES_0005));

            int APTITUD1_RES_0005 = (!txtapartado1_4.Text.Equals("")) ?int.Parse(txtapartado5_4.Text) : 0;
            BackResultObject("APTITUD1-RES-0005", APTITUD1_RES_0005);

            //String APTITUD1_RES_0006 = txtapartado6_4.Text;
            //BackResultObject(102, int.Parse(APTITUD1_RES_0006));
            int APTITUD1_RES_0006 = (!txtapartado6_4.Text.Equals("")) ?int.Parse(txtapartado6_4.Text) : 0;
            BackResultObject("APTITUD1-RES-0006", APTITUD1_RES_0006);

            //String APTITUD1_RES_0007 = txtapartado7_4.Text;
            //BackResultObject(103, int.Parse(APTITUD1_RES_0007));
            int APTITUD1_RES_0007 = (!txtapartado7_4.Text.Equals("")) ?int.Parse(txtapartado7_4.Text) : 0;
            BackResultObject("APTITUD1-RES-0007", APTITUD1_RES_0007);

            //String APTITUD1_RES_0008 = txtapartado8_4.Text;
            //BackResultObject(104, int.Parse(APTITUD1_RES_0008));
            int APTITUD1_RES_0008 = (!txtapartado8_4.Text.Equals("")) ?int.Parse(txtapartado8_4.Text) : 0;
            BackResultObject("APTITUD1-RES-0008", APTITUD1_RES_0008);

            //String APTITUD1_RES_0009 = txtapartado9_4.Text;
            //BackResultObject(105, int.Parse(APTITUD1_RES_0009));

            int APTITUD1_RES_0009 = (!txtapartado9_4.Text.Equals("")) ?int.Parse(txtapartado9_4.Text) : 0;
            BackResultObject("APTITUD1-RES-0009", APTITUD1_RES_0009);

            //String APTITUD1_RES_0010 = txtapartado10_4.Text;
            //BackResultObject(106, int.Parse(APTITUD1_RES_0010));
            int APTITUD1_RES_0010 = (!txtapartado10_4.Text.Equals("")) ? int.Parse(txtapartado10_4.Text) : 0;
            BackResultObject("APTITUD1-RES-0010", APTITUD1_RES_0010);

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
                //E_RESULTADO vResultado = nCustionarioPregunta.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pIdEvaluado: vObjetoPrueba.ID_CANDIDATO, pIdEvaluador: null, pIdCuestionarioPregunta: 0, pIdCuestionario: 0, XML_CUESTIONARIO: RESPUESTAS.ToString(), usuario: vClUsuario, programa: ContextoUsuario.nbPrograma.ToString());
                //string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                //UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "CloseTest");

                E_RESULTADO vResultado = nResultado.insertaResultadosAptitud1(RESPUESTAS.ToString(), null, vIdPrueba, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "CloseTest");
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
            GuardarResultado();
        }
         
        public void BackResultObject(string pClVariable, int pNoValor)
        {
            E_PRUEBA_RESULTADO vResultado = new E_PRUEBA_RESULTADO();
            vResultado.CL_VARIABLE = pClVariable;
            vResultado.NO_VALOR = pNoValor;
            vResultado.NO_VALOR_RES = pNoValor;
            vRespuestas.Add(vResultado);
        }
     }
  }
