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
    public partial class VentanaIntegracionMedioManual : System.Web.UI.Page
    {
        #region PROPIEDADES

        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private List<E_PREGUNTA> vRespuestas
        {
            get { return (List<E_PREGUNTA>)ViewState["vsRespuestas"]; }
            set { ViewState["vsRespuestas"] = value; }
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

        public List<E_PRUEBA_TIEMPO> vSeccionesPrueba
        {
            get { return (List<E_PRUEBA_TIEMPO>)ViewState["vSeccionesPrueba"]; }
            set { ViewState["vSeccionesPrueba"] = value; }
        }

        private List<E_PRUEBA_RESULTADO> vPruebaResultado
        {
            get { return (List<E_PRUEBA_RESULTADO>)ViewState["vsPruebaResultado"]; }
            set { ViewState["vsPruebaResultado"] = value; }
        }

        private string clTipoPrueba
        {
            get { return (string)ViewState["vsclTipoPrueba"]; }
            set { ViewState["vsclTipoPrueba"] = value; }
        }

        public int vIdBateria
        {
            get { return (int)ViewState["vsIdBateria"]; }
            set { ViewState["vsIdBateria"] = value; }
        }

        #endregion

        #region PAGE LOAD

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
                    {
                        vIdBateria = int.Parse(Request.QueryString["vIdBateria"]);
                        btnEliminar.Visible = true;
                        btnEliminarBateria.Visible = true;
                    }
                    else
                    {
                        btnEliminar.Visible = false;
                        btnEliminarBateria.Visible = false;
                    }

                    //E_RESULTADO vObjetoPrueba = nKprueba.INICIAR_K_PRUEBA(pIdPrueba: vIdPrueba, pFeInicio: DateTime.Now, pClTokenExterno: vClToken, usuario: vClUsuario, programa: vNbPrograma);
                    //if (vObjetoPrueba.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.ERROR)
                    //{
                        PruebasNegocio nPruebas = new PruebasNegocio();
                        var prueba = nPruebas.Obtener_RESULTADO_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).ToList();
                        var vPrueba = nPruebas.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken).FirstOrDefault();
                        if (prueba != null)
                        {
                            //if (vPrueba.NB_TIPO_PRUEBA == "APLICACION")
                            //    CargarRespuestasAplicacion(prueba);
                            //else
                                CargarRespuestas(prueba);
                        }
                  //  }

                    //if (Request.QueryString["MOD"] != null)
                    //{
                    //    clTipoPrueba = Request.QueryString["MOD"];
                    //    if (clTipoPrueba.Equals("REV"))
                    //    {
                    //      //  btnTerminar.Enabled = false; //Se comenta 06/06/2018
                    //    }
                    //}

                }

                
                vPruebaResultado = new List<E_PRUEBA_RESULTADO>();
            }
        }

        #endregion

        #region EVENTOS

        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtserie_1_1.Text) && !String.IsNullOrEmpty(txtserie_1_2.Text) && !String.IsNullOrEmpty(txtserie_1_3.Text) && !String.IsNullOrEmpty(txtserie_1_4.Text) && !String.IsNullOrEmpty(txtserie_1_5.Text) && !String.IsNullOrEmpty(txtserie_1_6.Text) && !String.IsNullOrEmpty(txtserie_1_7.Text) && !String.IsNullOrEmpty(txtserie_1_8.Text) &&
                !String.IsNullOrEmpty(txtserie_2_1.Text) && !String.IsNullOrEmpty(txtserie_2_2.Text) && !String.IsNullOrEmpty(txtserie_2_3.Text) && !String.IsNullOrEmpty(txtserie_2_4.Text) && !String.IsNullOrEmpty(txtserie_2_5.Text) && !String.IsNullOrEmpty(txtserie_2_6.Text) && !String.IsNullOrEmpty(txtserie_2_7.Text) && !String.IsNullOrEmpty(txtserie_2_8.Text))
            {
                if (clTipoPrueba == "EDIT")
                {
                    PruebasNegocio nKprueba = new PruebasNegocio();
                    SPE_OBTIENE_K_PRUEBA_Result vPruebaTerminada = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();
                    E_RESULTADO vResultado = nKprueba.CorrigePrueba(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), pID_PRUEBA: vIdPrueba, v_k_prueba: vPruebaTerminada, usuario: vClUsuario, programa: vNbPrograma);
                    EditTest();
                }
                else
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
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "¡Ingrese todos los campos por favor!", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, pCallBackFunction: "");
            }
        }
        #endregion

        #region Metodos

        public void SaveTest()
        {
            //string[] split = HiddenField1.Value.Split(new Char[] { ',' });
            //String SpaceToDelete = "";
            //split = split.Where(val => val != SpaceToDelete).ToArray();

            AsignarValorRespuestas("ADAPTACION_RES_0001", int.Parse(txtserie_1_1.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0002", int.Parse(txtserie_1_2.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0003", int.Parse(txtserie_1_3.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0004", int.Parse(txtserie_1_4.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0005", int.Parse(txtserie_1_5.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0006", int.Parse(txtserie_1_6.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0007", int.Parse(txtserie_1_7.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0008", int.Parse(txtserie_1_8.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0009", int.Parse(txtserie_2_1.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0010", int.Parse(txtserie_2_2.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0011", int.Parse(txtserie_2_3.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0012", int.Parse(txtserie_2_4.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0013", int.Parse(txtserie_2_5.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0014", int.Parse(txtserie_2_6.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0015", int.Parse(txtserie_2_7.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0016", int.Parse(txtserie_2_8.Text));


            var vXelements = vPruebaResultado.Select(x =>
                                                 new XElement("RESULTADO",
                                                 new XAttribute("CL_VARIABLE", x.CL_VARIABLE),
                                                 new XAttribute("NO_VALOR", x.NO_VALOR)
                                      ));
            XElement RESPUESTAS =
            new XElement("RESULTADOS", vXelements
            );

            ResultadoNegocio negRes = new ResultadoNegocio();
            PruebasNegocio nKprueba = new PruebasNegocio();
            var vObjetoPrueba = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();

            if (vObjetoPrueba != null)
            {
                E_RESULTADO vResultado = negRes.insertaResultadosAdaptacionMedio(RESPUESTAS.ToString(), null, vIdPrueba, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
            }
          
        }

        public void EditTest()
        {
            //string[] split = HiddenField1.Value.Split(new Char[] { ',' });
            //String SpaceToDelete = "";
            //split = split.Where(val => val != SpaceToDelete).ToArray();

            AsignarValorRespuestas("ADAPTACION_RES_0001", int.Parse(txtserie_1_1.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0002", int.Parse(txtserie_1_2.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0003", int.Parse(txtserie_1_3.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0004", int.Parse(txtserie_1_4.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0005", int.Parse(txtserie_1_5.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0006", int.Parse(txtserie_1_6.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0007", int.Parse(txtserie_1_7.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0008", int.Parse(txtserie_1_8.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0009", int.Parse(txtserie_2_1.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0010", int.Parse(txtserie_2_2.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0011", int.Parse(txtserie_2_3.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0012", int.Parse(txtserie_2_4.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0013", int.Parse(txtserie_2_5.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0014", int.Parse(txtserie_2_6.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0015", int.Parse(txtserie_2_7.Text));
            AsignarValorRespuestas("ADAPTACION_RES_0016", int.Parse(txtserie_2_8.Text));


            var vXelements = vPruebaResultado.Select(x =>
                                                 new XElement("RESULTADO",
                                                 new XAttribute("CL_VARIABLE", x.CL_VARIABLE),
                                                 new XAttribute("NO_VALOR", x.NO_VALOR)
                                      ));
            XElement RESPUESTAS =
            new XElement("RESULTADOS", vXelements
            );

            ResultadoNegocio negRes = new ResultadoNegocio();
            PruebasNegocio nKprueba = new PruebasNegocio();
            var vObjetoPrueba = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();

            if (vObjetoPrueba != null)
            {
                E_RESULTADO vResultado = negRes.insertaResultadosAdaptacionMedio(RESPUESTAS.ToString(), null, vIdPrueba, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
            }

        }

        public void AsignarValorRespuestas(string pClVariable, int pnbRespuesta)
        {
            E_PRUEBA_RESULTADO vResultado = new E_PRUEBA_RESULTADO();
            vResultado.NO_VALOR = pnbRespuesta;
            vResultado.CL_VARIABLE = pClVariable;
            vPruebaResultado.Add(vResultado);
        }

        public void CargarRespuestas(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> pResultados)
        {
            foreach (var item in pResultados)
            {
                switch (item.CL_PREGUNTA)
                {
                    case ("ADAPTACION_RES_0001"):
                        txtserie_1_1.Text = item.NO_VALOR_RESPUESTA.ToString();
                        break;
                    case ("ADAPTACION_RES_0002"):
                        txtserie_1_2.Text = item.NO_VALOR_RESPUESTA.ToString();
                        break;
                    case ("ADAPTACION_RES_0003"):
                        txtserie_1_3.Text = item.NO_VALOR_RESPUESTA.ToString();
                        break;
                    case ("ADAPTACION_RES_0004"):
                        txtserie_1_4.Text = item.NO_VALOR_RESPUESTA.ToString();
                        break;
                    case ("ADAPTACION_RES_0005"):
                        txtserie_1_5.Text = item.NO_VALOR_RESPUESTA.ToString();
                        break;
                    case ("ADAPTACION_RES_0006"):
                        txtserie_1_6.Text = item.NO_VALOR_RESPUESTA.ToString();
                        break;
                    case ("ADAPTACION_RES_0007"):
                        txtserie_1_7.Text = item.NO_VALOR_RESPUESTA.ToString();
                        break;
                    case ("ADAPTACION_RES_0008"):
                        txtserie_1_8.Text = item.NO_VALOR_RESPUESTA.ToString();
                        break;
                    case ("ADAPTACION_RES_0009"):
                        txtserie_2_1.Text = item.NO_VALOR_RESPUESTA.ToString();
                        break;
                    case ("ADAPTACION_RES_0010"):
                        txtserie_2_2.Text = item.NO_VALOR_RESPUESTA.ToString();
                        break;
                    case ("ADAPTACION_RES_0011"):
                        txtserie_2_3.Text = item.NO_VALOR_RESPUESTA.ToString();
                        break;
                    case ("ADAPTACION_RES_0012"):
                        txtserie_2_4.Text = item.NO_VALOR_RESPUESTA.ToString();
                        break;
                    case ("ADAPTACION_RES_0013"):
                        txtserie_2_5.Text = item.NO_VALOR_RESPUESTA.ToString();
                        break;
                    case ("ADAPTACION_RES_0014"):
                        txtserie_2_6.Text = item.NO_VALOR_RESPUESTA.ToString();
                        break;
                    case ("ADAPTACION_RES_0015"):
                        txtserie_2_7.Text = item.NO_VALOR_RESPUESTA.ToString();
                        break;
                    case ("ADAPTACION_RES_0016"):
                        txtserie_2_8.Text = item.NO_VALOR_RESPUESTA.ToString();
                        break;
                }
            }
        }

        #endregion

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
                        //var prueba = nPruebas.Obtener_RESULTADO_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).ToList();
                        //var vPrueba = nPruebas.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba, pClTokenExterno: vClToken).FirstOrDefault();
                        //if (prueba != null)
                        //{
                        // CargarRespuestas(prueba);                     
                        //}

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
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "s");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
            }
        }
    }
}