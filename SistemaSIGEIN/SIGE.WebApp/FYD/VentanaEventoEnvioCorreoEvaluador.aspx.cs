using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.FYD
{
    public partial class VentanaEventoEnvioCorreoEvaluador : System.Web.UI.Page
    {
        #region Variables

        private int vIdEvento
        {
            get { return (int)ViewState["vs_veee_id_evento"]; }
            set { ViewState["vs_veee_id_evento"] = value; }
        }

        private E_EVENTO oEvento
        {
            get { return (E_EVENTO)ViewState["vs_veee_evento"]; }
            set { ViewState["vs_veee_evento"] = value; }
        }

        private string vMensajeCorreo
        {
            get { return (string)ViewState["vs_veee_mensaje_correo"]; }
            set { ViewState["vs_veee_mensaje_correo"] = value; }
        }
        
        #endregion

        #region Funciones

        private void EnviarCorreo()
        {           
            ProcesoExterno pe = new ProcesoExterno();
            string myUrl = ResolveUrl("~/Logon.aspx?ClProceso=EVALUACION&FlProceso=");
            string vUrl = ContextoUsuario.nbHost + myUrl + oEvento.FL_EVENTO;
            vMensajeCorreo = vMensajeCorreo.Replace("[URL]", vUrl);
            vMensajeCorreo = vMensajeCorreo.Replace("[contraseña]", oEvento.CL_TOKEN);
            
            bool vEstatusCorreo = pe.EnvioCorreo(txtCorreo.Text, oEvento.NB_EVALUADOR, "Evaluación de evento", vMensajeCorreo);

            if (vEstatusCorreo)
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Correo enviado con éxito.", E_TIPO_RESPUESTA_DB.SUCCESSFUL);
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Error al enviar el correo.", E_TIPO_RESPUESTA_DB.ERROR);
            }            
        }

        private void cargarEvento()
        {
            EventoCapacitacionNegocio neg = new EventoCapacitacionNegocio();
            oEvento = neg.ObtieneEventos(ID_EVENTO: vIdEvento).FirstOrDefault();

            if (oEvento != null)
            {
                txtEvento.InnerText = oEvento.CL_EVENTO + " - " + oEvento.NB_EVENTO;
                txtCurso.InnerText = oEvento.NB_CURSO;
                txtEvaluador.InnerText = oEvento.CL_EVALUADOR + " - " + oEvento.NB_EVALUADOR;
                txtCorreo.Text = oEvento.CL_CORREO_EVALUADOR;

                vMensajeCorreo = ContextoApp.FYD.MensajeCorreoEvaluadores.dsMensaje;

                vMensajeCorreo = vMensajeCorreo.Replace("[Evaluador]", oEvento.NB_EVALUADOR);
                vMensajeCorreo = vMensajeCorreo.Replace("[NB_EVENTO]", oEvento.NB_EVENTO);

                if (oEvento.FE_EVALUACION.HasValue)
                {
                    vMensajeCorreo = vMensajeCorreo.Replace("[FE_EVALUACION]", oEvento.FE_EVALUACION.Value.ToString("dd/MM/yyyy"));
                }

                lMensaje.InnerHtml = vMensajeCorreo;

                if (DateTime.Now.Date > oEvento.FE_EVALUACION)
                {
                    btnAceptar.Enabled = false;
                    lblCaducidad.Visible = true;
                }
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["IdEvento"] != null)
                {
                    vIdEvento = int.Parse(Request.Params["IdEvento"]);
                    cargarEvento();
                }
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            EnviarCorreo();
        }
    }
}