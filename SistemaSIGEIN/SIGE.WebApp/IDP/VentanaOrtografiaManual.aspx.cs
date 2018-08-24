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
    public partial class VentanaOrtografiaManual : System.Web.UI.Page
    {

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

        private int? vIdBateria
        {
            get { return (int)ViewState["vsIdBateria"]; }
            set { ViewState["vsIdBateria"] = value; }
        }

        private Guid vClToken
        {
            get { return (Guid)ViewState["vsClToken"]; }
            set { ViewState["vsClToken"] = value; }
        }

        private List<E_DATOS_ORTOGRAFIAS> vPruebaResultado
        {
            get { return (List<E_DATOS_ORTOGRAFIAS>)ViewState["vsPruebaResultado"]; }
            set { ViewState["vsPruebaResultado"] = value; }
        }

        private List<E_PRUEBA> vBateriaPruebas
        {
            get { return (List<E_PRUEBA>)ViewState["vsBateriaPruebas"]; }
            set { ViewState["vsBateriaPruebas"] = value; }
        }
        //
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
                    if (Request.Params["CLAVE"] != null)
                    {
                      string  vClavePruebaReq = Request.Params["CLAVE"].ToString();

                      switch (vClavePruebaReq)
                        {
                          case "ORTOGRAFIA-1":
                                txtOrtografia1.Visible = true; lblOrtografia1.Visible = true;
                                break;
                          case "ORTOGRAFIA-2":
                                txtOrtografia2.Visible = true; lblOrtografia2.Visible = true;
                                break;
                          case "ORTOGRAFIA-3":
                                txtOrtografia3.Visible = true; lblOrtografia3.Visible = true;
                                break;
                        }

                    }
                    var vObjetoPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba).FirstOrDefault();
                    if (vObjetoPrueba != null && vObjetoPrueba.NB_TIPO_PRUEBA == "APLICACION") 
                    {
                        vIdBateria = vObjetoPrueba.ID_BATERIA;
                        var vBateria = nKprueba.Obtener_K_PRUEBA(pIdBateria: vIdBateria, pFgAsignada: true);
                        vBateriaPruebas = ParseListBaterias(vBateria);
                        HabilitarOrtografiasAplicacion(vBateriaPruebas, nKprueba);
                    }
                        else if (vObjetoPrueba != null)
                        {
                       vIdBateria = vObjetoPrueba.ID_BATERIA;
                       var vBateria = nKprueba.Obtener_K_PRUEBA(pIdBateria: vIdBateria, pFgAsignada: true);
                       vBateriaPruebas = ParseListBaterias(vBateria);
                       HabilitarOrtografias(vBateriaPruebas, nKprueba);
                    }

                }
                vPruebaResultado = new List<E_DATOS_ORTOGRAFIAS>();
            }
        }



        public void HabilitarOrtografias(List<E_PRUEBA> list, PruebasNegocio negocio)
        {
            foreach (var item in list)
            {
                switch (item.CL_PRUEBA)
                {
                    case "ORTOGRAFIA-1": //txtOrtografia1.Visible = true; lblOrtografia1.Visible = true;
                        ObtenerResultadosCapturaManual(negocio, item.CL_TOKEN_EXTERNO, item.ID_PRUEBA, "ORTOGRAFIA-1");
                        break;
                    case "ORTOGRAFIA-2": //txtOrtografia2.Visible = true; lblOrtografia2.Visible = true;
                        ObtenerResultadosCapturaManual(negocio, item.CL_TOKEN_EXTERNO, item.ID_PRUEBA, "ORTOGRAFIA-2");
                        break;
                    case "ORTOGRAFIA-3":// txtOrtografia3.Visible = true; lblOrtografia3.Visible = true;
                        ObtenerResultadosCapturaManual(negocio, item.CL_TOKEN_EXTERNO, item.ID_PRUEBA, "ORTOGRAFIA-3");
                        break;
                    default: break;
                }
            }
        }


        public void HabilitarOrtografiasAplicacion(List<E_PRUEBA> list, PruebasNegocio negocio)
        {
            foreach (var item in list)
            {
                switch (item.CL_PRUEBA)
                {
                    case "ORTOGRAFIA-1": //txtOrtografia1.Visible = true; lblOrtografia1.Visible = true;
                        ObtenerResultadosAplicacion(negocio, item.CL_TOKEN_EXTERNO, item.ID_PRUEBA, "ORTOGRAFIA-1");
                        break;
                    case "ORTOGRAFIA-2":// txtOrtografia2.Visible = true; lblOrtografia2.Visible = true;
                        ObtenerResultadosAplicacion(negocio, item.CL_TOKEN_EXTERNO, item.ID_PRUEBA, "ORTOGRAFIA-2");
                        break;
                    case "ORTOGRAFIA-3":// txtOrtografia3.Visible = true; lblOrtografia3.Visible = true;
                        ObtenerResultadosAplicacion(negocio, item.CL_TOKEN_EXTERNO, item.ID_PRUEBA, "ORTOGRAFIA-3");
                        break;
                    default: break;
                }
            }
        }

        public void ObtenerResultadosCapturaManual(PruebasNegocio nPruebas,Guid? pToken , int pIdPrueba,string pOrtografia)
        {
            var prueba = nPruebas.Obtener_RESULTADO_PRUEBA(pClTokenExterno: (Guid)pToken, pIdPrueba: pIdPrueba).ToList();
            if (prueba != null)
            {
                switch(pOrtografia)
                {
                    case "ORTOGRAFIA-1":
                     var vPruebaRespuesta = prueba.Where(w => w.CL_PREGUNTA.Equals("ORTOGRAFIA1-RES-01")).FirstOrDefault();
                     if (vPruebaRespuesta != null && vPruebaRespuesta.NB_RESULTADO != null)
                     {
                         var vPruebaRespuestaR = vPruebaRespuesta.NB_RESULTADO.ToString();
                         txtOrtografia1.Text = vPruebaRespuestaR != null ? vPruebaRespuestaR : "";
                     }
                   
                 break;
                    case "ORTOGRAFIA-2":
                        var vPruebaRespuesta2 = prueba.Where(w => w.CL_PREGUNTA.Equals("ORTOGRAFIA2-RES-01")).FirstOrDefault();
                        if (vPruebaRespuesta2 != null && vPruebaRespuesta2.NB_RESULTADO != null)
                        {
                            var vPruebaRespuesta2R = vPruebaRespuesta2.NB_RESULTADO.ToString();
                            txtOrtografia2.Text = vPruebaRespuesta2R != null ? vPruebaRespuesta2R : "";
                        }
                       
                        break;
                    case "ORTOGRAFIA-3":
                        var vPruebaRespuesta3 = prueba.Where(w => w.CL_PREGUNTA.Equals("ORTOGRAFIA3-RES-01")).FirstOrDefault();
                        if (vPruebaRespuesta3 != null && vPruebaRespuesta3.NB_RESULTADO != null)
                        {
                            var vPruebaRespuesta3R = vPruebaRespuesta3.NB_RESULTADO.ToString();
                            txtOrtografia3.Text = vPruebaRespuesta3R != null ? vPruebaRespuesta3R : "";
                        }
                        break;
                }
            }
        }

        public void ObtenerResultadosAplicacion(PruebasNegocio nPruebas, Guid? pToken, int pIdPrueba, string pOrtografia)
        {
            var prueba = nPruebas.Obtener_RESULTADO_PRUEBA(pClTokenExterno: (Guid)pToken, pIdPrueba: pIdPrueba).ToList();
            if (prueba != null)
            {
                switch (pOrtografia)
                {
                    case "ORTOGRAFIA-1":
                        var vPruebaRespuesta = prueba.Where(w => w.CL_PREGUNTA.Equals("ORTOGRAFIA1-RES-01")).FirstOrDefault();
                        if (vPruebaRespuesta != null && vPruebaRespuesta.NO_VALOR_RESPUESTA != null)
                        {
                            var vPruebaRespuestaR = vPruebaRespuesta.NO_VALOR_RESPUESTA.ToString();
                            txtOrtografia1.Text = vPruebaRespuestaR != null ? vPruebaRespuestaR : "";
                        }

                        break;
                    case "ORTOGRAFIA-2":
                        var vPruebaRespuesta2 = prueba.Where(w => w.CL_PREGUNTA.Equals("ORTOGRAFIA2-RES-01")).FirstOrDefault();
                        if (vPruebaRespuesta2 != null && vPruebaRespuesta2.NO_VALOR_RESPUESTA != null)
                        {
                            var vPruebaRespuesta2R = vPruebaRespuesta2.NO_VALOR_RESPUESTA.ToString();
                            txtOrtografia2.Text = vPruebaRespuesta2R != null ? vPruebaRespuesta2R : "";
                        }

                        break;
                    case "ORTOGRAFIA-3":
                        var vPruebaRespuesta3 = prueba.Where(w => w.CL_PREGUNTA.Equals("ORTOGRAFIA3-RES-01")).FirstOrDefault();
                        if (vPruebaRespuesta3 != null && vPruebaRespuesta3.NO_VALOR_RESPUESTA != null)
                        {
                            var vPruebaRespuesta3R = vPruebaRespuesta3.NO_VALOR_RESPUESTA.ToString();
                            txtOrtografia3.Text = vPruebaRespuesta3R != null ? vPruebaRespuesta3R : "";
                        }
                        break;
                }
            }
        }


        public List<E_PRUEBA> ParseListBaterias(List<SPE_OBTIENE_K_PRUEBA_Result> list) 
        {
            List<E_PRUEBA> vBateriaPruebas = new List<E_PRUEBA>();
            foreach (var item in list)
	        {
                vBateriaPruebas.Add(new
                    E_PRUEBA
                    {
                        ID_PRUEBA = item.ID_PRUEBA,
                        ID_PRUEBA_PLANTILLA = item.ID_PRUEBA_PLANTILLA,
                        CL_PRUEBA = item.CL_PRUEBA,
                        NB_PRUEBA = item.NB_PRUEBA,
                        CL_TOKEN_EXTERNO = item.CL_TOKEN_EXTERNO,
                        CL_ESTADO = item.CL_ESTADO
                         });
	        }
            return vBateriaPruebas;
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
            SaveTest();
        }

        public void SaveTest()
        {
         var vOrtografiaI   = (vBateriaPruebas.Where(w => w.CL_PRUEBA.Equals("ORTOGRAFIA-1")).FirstOrDefault());
         var vOrtografiaII  = (vBateriaPruebas.Where(w => w.CL_PRUEBA.Equals("ORTOGRAFIA-2")).FirstOrDefault());
         var vOrtografiaIII = (vBateriaPruebas.Where(w => w.CL_PRUEBA.Equals("ORTOGRAFIA-3")).FirstOrDefault());

           if(vOrtografiaI != null && txtOrtografia1.Visible == true)
           {
               AsignarValorRespuestas("ORTOGRAFIA1-RES-01", (!txtOrtografia1.Text.Equals("")) ? int.Parse(txtOrtografia1.Text) : 0, vOrtografiaI.ID_PRUEBA);
           
           }
           if (vOrtografiaII != null && txtOrtografia2.Visible == true)
           {
               AsignarValorRespuestas("ORTOGRAFIA2-RES-01", (!txtOrtografia2.Text.Equals("")) ? int.Parse(txtOrtografia2.Text) : 0, vOrtografiaII.ID_PRUEBA);
           }

           if (vOrtografiaIII != null && txtOrtografia3.Visible == true)
           {
               AsignarValorRespuestas("ORTOGRAFIA3-RES-01", (!txtOrtografia3.Text.Equals("")) ? int.Parse(txtOrtografia3.Text) : 0, vOrtografiaIII.ID_PRUEBA);
           }

            var vXelements = vPruebaResultado.Select(x =>
                                                 new XElement("RESULTADO",
                                                 new XAttribute("ID_PRUEBA", x.ID_PRUEBA),
                                                 new XAttribute("CL_VARIABLE", x.CL_VARIABLE),
                                                 new XAttribute("NO_VALOR", x.NO_VALOR),
                                                 new XAttribute("NB_RESULTADO",x.NB_RESULTADO)
                                      ));
            XElement RESPUESTAS =
            new XElement("RESULTADOS", vXelements
            );

            ResultadoNegocio negRes = new ResultadoNegocio();
            PruebasNegocio nKprueba = new PruebasNegocio();
            var vObjetoPrueba = nKprueba.Obtener_K_PRUEBA(pClTokenExterno: vClToken, pIdPrueba: vIdPrueba).FirstOrDefault();

            if (vObjetoPrueba != null)
            {
                E_RESULTADO vResultado = negRes.insertaResultadosOrtografias(RESPUESTAS.ToString(), null, vIdPrueba,"", vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                //UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "CloseTest");
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
            }
        }

        public void AsignarValorRespuestas(string pClVariable, int pnbRespuesta,int pIdPrueba)
        {
            E_DATOS_ORTOGRAFIAS vResultado = new E_DATOS_ORTOGRAFIAS();
            vResultado.NO_VALOR = pnbRespuesta;
            vResultado.CL_VARIABLE = pClVariable;
            vResultado.ID_PRUEBA = pIdPrueba;
            vResultado.NB_RESULTADO = pnbRespuesta;
            vPruebaResultado.Add(vResultado);
        }

        [Serializable]
        public class E_DATOS_ORTOGRAFIAS
        {
            public int ID_PRUEBA { get; set; }
            public string CL_VARIABLE { get; set; }
            public int NO_VALOR { get; set; }
            public int NB_RESULTADO { get; set; }
        }
    }
}