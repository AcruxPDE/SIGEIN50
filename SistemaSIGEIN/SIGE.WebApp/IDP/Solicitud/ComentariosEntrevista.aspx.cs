using SIGE.Entidades.Externas;
using SIGE.Negocio.IntegracionDePersonal;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.IDP.Solicitud
{
    public partial class ComentariosEntrevista : System.Web.UI.Page
    {
        #region Variables
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int vIdProcesoSeleccion
        {
            get { return (int)ViewState["vs_ce_id_proceso_seleccion"]; }
            set { ViewState["vs_ce_id_proceso_seleccion"] = value; }
        }

        public int vIdEntrevista
        {
            get { return (int)ViewState["vs_ce_id_entrevista"]; }
            set { ViewState["vs_ce_id_entrevista"] = value; }
        }
        #endregion

        #region Funciones

        private void CargarDatos()
        {
            ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();

            var vProcesoSeleccion = nProcesoSeleccion.ObtieneProcesoSeleccion(pIdProcesoSeleccion: vIdProcesoSeleccion).FirstOrDefault();

            if (vProcesoSeleccion != null)
            {
                txtCandidato.InnerText = vProcesoSeleccion.NB_CANDIDATO_COMPLETO;
                txtClaveRequisicion.InnerText = vProcesoSeleccion.NO_REQUISICION;
                txtPuestoAplicar.InnerText = vProcesoSeleccion.NB_PUESTO;
            }

            var vEntrevista = nProcesoSeleccion.ObtieneEntrevistaProcesoSeleccion(pIdEntrevista: vIdEntrevista).FirstOrDefault();

            if (vEntrevista != null)
            {
                txtDsNotas.Content = vEntrevista.DS_OBSERVACIONES;
            }

        }

        private void GuardarDatos()
        {
            ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();

            E_RESULTADO vResultado = nProcesoSeleccion.ActualizaComentarioEntrevista(vIdEntrevista, txtDsNotas.Content, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
                Response.Redirect("~/Logout.aspx");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = (ContextoUsuario.oUsuario != null) ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO";
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {

                if (Request.Params["IdEntrevista"] != null)
                {
                    vIdEntrevista = int.Parse(Request.Params["IdEntrevista"].ToString());
                }

                if (Request.Params["IdProcesoSeleccion"] != null)
                {
                    vIdProcesoSeleccion = int.Parse(Request.Params["IdProcesoSeleccion"].ToString());
                }

                CargarDatos();

            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarDatos();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Logout.aspx");
        }
    }
}