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
    public partial class VentanaResultadosEntrevista : System.Web.UI.Page
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
        public int vIdBateria
        {
            get { return (int)ViewState["vsIdBateria"]; }
            set { ViewState["vsIdBateria"] = value; }
        }

        protected void HabilitarPestana(string pClFactor, int pCompetenciasFactor)
        {
            switch (pClFactor)
            {
                case "33":
                 tbresultados.Tabs[0].Visible =   pCompetenciasFactor > 0 ?  true : false;
                 rpvcomunicacionverbal.Selected = true;
                break;
                case "34":
                tbresultados.Tabs[1].Visible = pCompetenciasFactor > 0 ? true : false;
                rpvcomunicacionnoverbal.Selected = true;
                break;
                case "52":
                tbresultados.Tabs[2].Visible = pCompetenciasFactor > 0 ? true : false;
                rpvSeguridad.Selected = true;
                break;
                case "53":
                tbresultados.Tabs[3].Visible = pCompetenciasFactor > 0 ? true : false;
                rpvEnfoque.Selected = true;
                break;
                case "54":
                tbresultados.Tabs[4].Visible = pCompetenciasFactor > 0 ? true : false;
                rpvConflicto.Selected = true;
                break;
                case "55":
                tbresultados.Tabs[5].Visible = pCompetenciasFactor > 0 ? true : false;
                rpvCarisma.Selected = true;
                break;
            }
        }

        protected void CargarFactores()
        {
            PruebasNegocio nPruebasNegocio = new PruebasNegocio();
              var vPrueba = nPruebasNegocio.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken).FirstOrDefault();

              if (vPrueba != null)
              {
                  var vlstFactores = nPruebasNegocio.ObtienePruebasFactores(pID_SELECCION: vPrueba.ID_PRUEBA_PLANTILLA, pCL_SELECCION: "FACTORES").ToList();

                  foreach (var item in vlstFactores.OrderByDescending(o => o.ID_SELECCION))
                  {
                      var vLstCompetenciasFactor = nPruebasNegocio.ObtienePruebasFactores(pID_SELECCION: item.ID_SELECCION, pCL_SELECCION: "COMPETENCIAS").ToList();
                      HabilitarPestana(item.ID_SELECCION.ToString(), vLstCompetenciasFactor.Count);
                  }
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
                    vClToken = new Guid(Request.QueryString["T"]);
                    if (Request.QueryString["vIdBateria"] != null)
                    vIdBateria = int.Parse(Request.QueryString["vIdBateria"]);
                     btnTerminar.Text = "Guardar";

                     CargarFactores();

                    if (Request.QueryString["CL"] != null)
                    {
                        if (Request.QueryString["CL"] == "REVISION")
                            btnTerminar.Enabled = false;
                        if (Request.QueryString["CL"] == "EDIT")
                        {
                           // btnEliminar.Visible = true;
                            btnTerminar.Text = "Guardar";
                        }
                           

                    }
                    //E_RESULTADO vObjetoPrueba = nKprueba.INICIAR_K_PRUEBA(pIdPrueba: vIdPrueba, pFeInicio: DateTime.Now, pClTokenExterno: vClToken, usuario: vClUsuario, programa: vNbPrograma);
                    //if (vObjetoPrueba != null)
                    //{
                    //    if (vObjetoPrueba.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.ERROR)
                    //    {
                            PruebasNegocio nPruebas = new PruebasNegocio();
                            var prueba = nPruebas.Obtener_RESULTADO_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).ToList();
                            if (prueba != null)
                            {
                                var vPruebaRespuesta = prueba.Where(w => w.CL_PREGUNTA.Equals("ENTREVISTA_RES_0001")).FirstOrDefault();
                                var vPruebaRespuesta2 = prueba.Where(w => w.CL_PREGUNTA.Equals("ENTREVISTA_RES_0002")).FirstOrDefault();
                                var vPruebaRespuesta3 = prueba.Where(w => w.CL_PREGUNTA.Equals("ENTREVISTA_RES_0003")).FirstOrDefault();
                                var vPruebaRespuesta4 = prueba.Where(w => w.CL_PREGUNTA.Equals("ENTREVISTA_RES_0004")).FirstOrDefault();
                                var vPruebaRespuesta5 = prueba.Where(w => w.CL_PREGUNTA.Equals("ENTREVISTA_RES_0005")).FirstOrDefault();
                                var vPruebaRespuesta6 = prueba.Where(w => w.CL_PREGUNTA.Equals("ENTREVISTA_RES_0006")).FirstOrDefault();
                                
                                if (vPruebaRespuesta != null)
                                {
                                    iniciaRadButtonResultado((int)vPruebaRespuesta.NO_VALOR_RESPUESTA);
                                }

                                if (vPruebaRespuesta2 != null)
                                {
                                    iniciaRadButtonResultado2((int)vPruebaRespuesta2.NO_VALOR_RESPUESTA);
                                }

                                if (vPruebaRespuesta3 != null)
                                {
                                    iniciaRadButtonResultado3((int)vPruebaRespuesta3.NO_VALOR_RESPUESTA);
                                }

                                if (vPruebaRespuesta4 != null)
                                {
                                    iniciaRadButtonResultado4((int)vPruebaRespuesta4.NO_VALOR_RESPUESTA);
                                }

                                if (vPruebaRespuesta5 != null)
                                {
                                    iniciaRadButtonResultado5((int)vPruebaRespuesta5.NO_VALOR_RESPUESTA);
                                }

                                if (vPruebaRespuesta6 != null)
                                {
                                    iniciaRadButtonResultado6((int)vPruebaRespuesta6.NO_VALOR_RESPUESTA);
                                }
                            }
                        //}
                        //else if (vObjetoPrueba.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                        //{
                        //}
                    //}
                }
                vRespuestas = new List<E_PRUEBA_RESULTADO>();
            }
        }

        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            PruebasNegocio nKprueba = new PruebasNegocio();
            SPE_OBTIENE_K_PRUEBA_Result vPruebaTerminada = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();
            vPruebaTerminada.FE_TERMINO = DateTime.Now;
            vPruebaTerminada.CL_ESTADO = E_ESTADO_PRUEBA.TERMINADA.ToString();
            vPruebaTerminada.NB_TIPO_PRUEBA = "MANUAL";
            E_RESULTADO vResultado = nKprueba.InsertaActualiza_K_PRUEBA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
            saveTest();
        }

        public void saveTest()
        {
            if (tbresultados.Tabs[0].Visible)
            {
                int ENTREVISTA_RES_0001 = BackSelectedCheckBox(rbrespuesta1, rbrespuesta2, rbrespuesta3, rbrespuesta4);
                BackQuestionObject("ENTREVISTA_RES_0001", ENTREVISTA_RES_0001);
            }

            if (tbresultados.Tabs[1].Visible)
            {
                int ENTREVISTA_RES_0002 = BackSelectedCheckBox(rbrespuesta5, rbrespuesta6, rbrespuesta7, rbrespuesta8);
                BackQuestionObject("ENTREVISTA_RES_0002", ENTREVISTA_RES_0002);
            }

            if (tbresultados.Tabs[2].Visible)
            {
                int ENTREVISTA_RES_0003 = BackSelectedCheckBox(btnSeguridadInsegura, btnSeguridadAgresiva, btnSeguridadMediaSegura, btnSeguridadSegura);
                BackQuestionObject("ENTREVISTA_RES_0003", ENTREVISTA_RES_0003);
            }

            if (tbresultados.Tabs[3].Visible)
            {
                int ENTREVISTA_RES_0004 = BackSelectedCheckBox(btnEnfoque1, btnEnfoque2, btnEnfoque3, btnEnfoque4);
                BackQuestionObject("ENTREVISTA_RES_0004", ENTREVISTA_RES_0004);
            }

            if (tbresultados.Tabs[4].Visible)
            {
                int ENTREVISTA_RES_0005 = BackSelectedCheckBox(btnConflicto1, btnConflicto2, btnConflicto3, btnConflicto4);
                BackQuestionObject("ENTREVISTA_RES_0005", ENTREVISTA_RES_0005);
            }

            if (tbresultados.Tabs[5].Visible)
            {
                int ENTREVISTA_RES_0006 = BackSelectedCheckBox(btnCarisma1, btnCarisma2, btnCarisma3, btnCarisma4);
                BackQuestionObject("ENTREVISTA_RES_0006", ENTREVISTA_RES_0006);
            }

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
                E_RESULTADO vResultado = nResultado.insertaResultadosEntrevista(RESPUESTAS.ToString(), null, vIdPrueba, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                //UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "CloseTest");
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
            }
        }

        public void BackQuestionObject(string pClVariable, int pRespuesta)
        {
          E_PRUEBA_RESULTADO vRespuesta = new E_PRUEBA_RESULTADO();
                vRespuesta.CL_VARIABLE = pClVariable;
                vRespuesta.NO_VALOR= pRespuesta;
                vRespuesta.NO_VALOR_RES = pRespuesta;
                vRespuestas.Add(vRespuesta);
            
        }

        public int BackSelectedCheckBox(RadButton a,RadButton b, RadButton c, RadButton d)
        {
            int resultado = 0;
            if (a.Checked)
            { resultado = int.Parse(a.Value); }
            else if (b.Checked)
            { resultado = int.Parse(b.Value); }
            else if (c.Checked)
            { resultado = int.Parse(c.Value); }
            else if (d.Checked)
            { resultado = int.Parse(d.Value); }
            return resultado;
        }

        public void iniciaRadButtonResultado(int valor)
        {
            switch (valor)
            {
                case 1: rbrespuesta1.Checked = true; break;
                case 2: rbrespuesta2.Checked = true; break;
                case 3: rbrespuesta3.Checked = true; break;
                case 4: rbrespuesta4.Checked = true; break;
                default: break;
            }
        }

        public void iniciaRadButtonResultado2(int valor)
        {
            switch (valor)
            {
                case 1: rbrespuesta5.Checked = true; break;
                case 2: rbrespuesta6.Checked = true; break;
                case 3: rbrespuesta7.Checked = true; break;
                case 4: rbrespuesta8.Checked = true; break;
                default: break;
            }
        }

        public void iniciaRadButtonResultado3(int valor)
        {
            switch (valor)
            {
                case 1: btnSeguridadInsegura.Checked = true; break;
                case 2: btnSeguridadAgresiva.Checked = true; break;
                case 3: btnSeguridadMediaSegura.Checked = true; break;
                case 4: btnSeguridadSegura.Checked = true; break;
                default: break;
            }
        }

        public void iniciaRadButtonResultado4(int valor)
        {
            switch (valor)
            {
                case 1: btnEnfoque1.Checked = true; break;
                case 2: btnEnfoque2.Checked = true; break;
                case 3: btnEnfoque3.Checked = true; break;
                case 4: btnEnfoque4.Checked = true; break;
                default: break;
            }
        }

        public void iniciaRadButtonResultado5(int valor)
        {
            switch (valor)
            {
                case 1: btnConflicto1.Checked = true; break;
                case 2: btnConflicto2.Checked = true; break;
                case 3: btnConflicto3.Checked = true; break;
                case 4: btnConflicto4.Checked = true; break;
                default: break;
            }
        }

        public void iniciaRadButtonResultado6(int valor)
        {
            switch (valor)
            {
                case 1: btnCarisma1.Checked = true; break;
                case 2: btnCarisma2.Checked = true; break;
                case 3: btnCarisma3.Checked = true; break;
                case 4: btnCarisma4.Checked = true; break;
                default: break;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (vIdPrueba != null)
            {
                PruebasNegocio nPruebas = new PruebasNegocio();
                var vResultado = nPruebas.EliminaRespuestasPrueba(vIdPrueba, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                {
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");

                }
                else
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
            }

        }

        protected void btnEliminarBateria_Click(object sender, EventArgs e)
        {
            PruebasNegocio nPruebas = new PruebasNegocio();
            var vResultado = nPruebas.EliminaRespuestasBaterias(vIdBateria, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
            }
        }
    }
}