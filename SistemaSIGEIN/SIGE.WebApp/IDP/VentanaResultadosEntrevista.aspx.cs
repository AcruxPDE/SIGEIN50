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
                    if (Request.QueryString["CL"] != null)
                    {
                        if (Request.QueryString["CL"] == "REVISION")
                            btnTerminar.Enabled = false;
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
                                
                                if (vPruebaRespuesta != null)
                                {
                                    iniciaRadButtonResultado((int)vPruebaRespuesta.NO_VALOR_RESPUESTA);
                                }

                                if (vPruebaRespuesta2 != null)
                                {
                                    iniciaRadButtonResultado2((int)vPruebaRespuesta2.NO_VALOR_RESPUESTA);
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
            int ENTREVISTA_RES_0001 = BackSelectedCheckBox(rbrespuesta1, rbrespuesta2, rbrespuesta3, rbrespuesta4);
            BackQuestionObject("ENTREVISTA_RES_0001", ENTREVISTA_RES_0001);

            int ENTREVISTA_RES_0002 = BackSelectedCheckBox(rbrespuesta5, rbrespuesta6, rbrespuesta7, rbrespuesta8);
            BackQuestionObject("ENTREVISTA_RES_0002", ENTREVISTA_RES_0002);

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
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "CloseTest");
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

    }
}