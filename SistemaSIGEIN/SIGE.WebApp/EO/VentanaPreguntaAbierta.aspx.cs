using SIGE.Entidades.Externas;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.EO
{
    public partial class VentanaPreguntaAbierta : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int? vIdPeriodo
        {
            get { return(int?)ViewState["vs_vIdPeriodo"];}
            set { ViewState["vs_vIdPeriodo"] = value; }
        }

        public string vTipoTransaccion
        {
            get { return(string)ViewState["vs_vTipoTransaccion"];}
            set { ViewState["vs_vTipoTransaccion"]=value;}
        }

        public int? vIdPregunta
        {
            get { return (int?)ViewState["vs_vIdPregunta"];}
            set { ViewState["vs_vIdPregunta"]=value;}
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["ID_PERIODO"] != null)
                {
                    vIdPeriodo = int.Parse(Request.Params["ID_PERIODO"].ToString());
                }

                if (Request.Params["ID_PREGUNTA"] != null)
                {
                    vIdPregunta = int.Parse(Request.Params["ID_PREGUNTA"].ToString());
                    ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
                    var oPregunta = nClima.ObtenerPreguntasAbiertas(vIdPeriodo, vIdPregunta).FirstOrDefault();
                    txtNbPregunta.Text = oPregunta.NB_PREGUNTA;
                    txtDsPregunta.Text = oPregunta.DS_PREGUNTA;
                    vTipoTransaccion = "A";
                }
                else
                {
                    vTipoTransaccion = "I";
                }

            }
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string vNbPregunta = txtNbPregunta.Text;
            string vDsPregunta = txtDsPregunta.Text;

            if (vNbPregunta != "")
            {
                ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
                E_RESULTADO vResultado = nClima.InsertaActualizaPreguntasAbiertas(vIdPeriodo, vIdPregunta, vNbPregunta, vDsPregunta, vClUsuario, vNbPrograma, vTipoTransaccion);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "CloseWindow");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Introduzca la pregunta.",E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: " ");
            }
        }
    }
}