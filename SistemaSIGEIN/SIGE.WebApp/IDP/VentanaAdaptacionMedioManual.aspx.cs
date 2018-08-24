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

namespace SIGE.WebApp.IDP.Pruebas
{
    public partial class VentanaAdaptacionMedio : System.Web.UI.Page
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
                }
                vPruebaResultado = new List<E_PRUEBA_RESULTADO>();
               
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
            SaveTest();
        }

        public void SaveTest()
        {
             string[] split = HiddenField1.Value.Split(new Char[] {','});
            String SpaceToDelete = "";
            split = split.Where(val => val != SpaceToDelete).ToArray();

            AsignarValorRespuestas("ADAPTACION_RES_0001", int.Parse(split[0]));
            AsignarValorRespuestas("ADAPTACION_RES_0002", int.Parse(split[1]));
            AsignarValorRespuestas("ADAPTACION_RES_0003", int.Parse(split[2]));
            AsignarValorRespuestas("ADAPTACION_RES_0004", int.Parse(split[3]));
            AsignarValorRespuestas("ADAPTACION_RES_0005", int.Parse(split[4]));
            AsignarValorRespuestas("ADAPTACION_RES_0006", int.Parse(split[5]));
            AsignarValorRespuestas("ADAPTACION_RES_0007", int.Parse(split[6]));
            AsignarValorRespuestas("ADAPTACION_RES_0008", int.Parse(split[7]));
            AsignarValorRespuestas("ADAPTACION_RES_0009", int.Parse(split[8]));
            AsignarValorRespuestas("ADAPTACION_RES_0010", int.Parse(split[9]));
            AsignarValorRespuestas("ADAPTACION_RES_0011", int.Parse(split[10]));
            AsignarValorRespuestas("ADAPTACION_RES_0012", int.Parse(split[11]));
            AsignarValorRespuestas("ADAPTACION_RES_0013", int.Parse(split[12]));
            AsignarValorRespuestas("ADAPTACION_RES_0014", int.Parse(split[13]));
            AsignarValorRespuestas("ADAPTACION_RES_0015", int.Parse(split[14]));
            AsignarValorRespuestas("ADAPTACION_RES_0016", int.Parse(split[15]));


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
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "CloseTest");
            }
        }

        public void AsignarValorRespuestas(string pClVariable, int pnbRespuesta)
        {
            E_PRUEBA_RESULTADO vResultado = new E_PRUEBA_RESULTADO();
            vResultado.NO_VALOR = pnbRespuesta;
            vResultado.CL_VARIABLE = pClVariable;
            vPruebaResultado.Add(vResultado);
        }
    }
}