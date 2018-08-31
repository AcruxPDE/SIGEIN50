using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.FYD
{
    public partial class VentanaNuevoEvento : System.Web.UI.Page
    {
        #region variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private E_TIPO_OPERACION_DB vClOperacion
        {
            get { return (E_TIPO_OPERACION_DB)ViewState["vs_vClOperacion"]; }
            set { ViewState["vs_vClOperacion"] = value; }
        }

        private int? vIdEvento
        {
            get { return (int?)ViewState["vs_vIdEvento"]; }
            set { ViewState["vs_vIdEvento"] = value; }
        }

        #endregion

        #region Metodos

        protected string quitarCararcteresNoAlfanumericos(string newPassword)
        {
            String vPassword = "";
            Random rnd = new Random();
            vPassword = Regex.Replace(newPassword, @"[^a-zA-Z0-9]", m => rnd.Next(1, 10) + "");
            return vPassword;
        }

        protected void CargarEvento()
        {
            EventoCapacitacionNegocio neg = new EventoCapacitacionNegocio();

            E_EVENTO evento = new E_EVENTO();
            evento = neg.ObtieneEventos(vIdEvento).FirstOrDefault();

            txtClEvento.Text = evento.CL_EVENTO;
            txtDsEvento.Text = evento.DS_EVENTO;

            dtpInicial.SelectedDate = evento.FE_INICIO;
            dtpFinal.SelectedDate = evento.FE_TERMINO;

            cmbTipo.SelectedValue = evento.CL_TIPO_CURSO;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                if (Request.Params["EventoId"] != null)
                {
                    vIdEvento = int.Parse(Request.Params["EventoId"].ToString());
                    CargarEvento();
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtClEvento.Text != "" && txtDsEvento.Text != "")
            {
                if (dtpInicial.SelectedDate.HasValue && dtpFinal.SelectedDate.HasValue)
                {

                    E_EVENTO evento = new E_EVENTO();
                    EventoCapacitacionNegocio neg = new EventoCapacitacionNegocio();

                    if (vIdEvento != null)
                        vClOperacion = E_TIPO_OPERACION_DB.A;
                    else
                        vClOperacion = E_TIPO_OPERACION_DB.I;

                    if (vIdEvento != null)
                    evento.ID_EVENTO = (int)vIdEvento;

                    evento.CL_EVENTO = txtClEvento.Text;
                    evento.NB_EVENTO = txtDsEvento.Text;
                    evento.DS_EVENTO = txtDsEvento.Text;
                    evento.FE_INICIO = dtpInicial.SelectedDate.Value;
                    evento.FE_TERMINO = dtpFinal.SelectedDate.Value;
                    evento.CL_TIPO_CURSO = cmbTipo.SelectedValue;
                    evento.CL_ESTADO = "CALENDARIZADO";
                    evento.MN_COSTO_DIRECTO = 0;
                    evento.MN_COSTO_INDIRECTO = 0;
                    evento.FG_INCLUIR_EN_PLANTILLA = false;
                    evento.CL_TOKEN = quitarCararcteresNoAlfanumericos(Membership.GeneratePassword(8, 0));
                    evento.FL_EVENTO = Guid.NewGuid();

                    E_RESULTADO msj = neg.InsertaActualizaEvento(vClOperacion.ToString(), evento, vClUsuario, vNbPrograma);
                    string vMensaje = msj.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    if (vClOperacion == E_TIPO_OPERACION_DB.A)
                        UtilMensajes.MensajeResultadoDB(rwmEvento, vMensaje, msj.CL_TIPO_ERROR, pCallBackFunction: "ReturnDataToParentEdit");
                    else
                        UtilMensajes.MensajeResultadoDB(rwmEvento, vMensaje, msj.CL_TIPO_ERROR, pCallBackFunction: "ReturnDataToParent");
                }
                else
                    UtilMensajes.MensajeResultadoDB(rwmEvento, "Los campos evento, descripción, fecha de inicio y fecha de término son obligatorios.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, pCallBackFunction: "");
            }
            else
                UtilMensajes.MensajeResultadoDB(rwmEvento, "Los campos evento, descripción, fecha de inicio y fecha de término son obligatorios.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, pCallBackFunction: "");

        }
    }
}