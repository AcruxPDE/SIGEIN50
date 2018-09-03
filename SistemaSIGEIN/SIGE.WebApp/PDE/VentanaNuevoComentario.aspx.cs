using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.PuntoDeEncuentro;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using SIGE.Entidades.PuntoDeEncuentro;

namespace SIGE.WebApp.PDE
{
    public partial class VentanaNuevoComentario : System.Web.UI.Page
    {

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;


        public int vIdComunicado
        {
            set { ViewState["vs_IdComunicado"] = value; }
            get { return (int)ViewState["vs_IdComunicado"]; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                //vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
                //vNbPrograma = ContextoUsuario.nbPrograma;

                    //Agregamos aqui el id del Comunicado seleccionado

                    if (Request.Params["ID_COMUNICADO"] != null)
                    {
                        vIdComunicado = int.Parse(Request.Params["ID_COMUNICADO"]);
                    }
            }
   
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            VisorComunicadoNegocio negocio = new VisorComunicadoNegocio();
            var vComentario = txtComentario.Text;

            if (vComentario != "")
            {

                E_RESULTADO res = negocio.InsertarComentarioComunicado(vIdComunicado, ContextoUsuario.oUsuario.CL_USUARIO, vComentario, ContextoUsuario.oUsuario.CL_USUARIO, ContextoUsuario.nbPrograma);

                string vMensaje = res.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, res.CL_TIPO_ERROR);
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Escribe el comentario", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            }
        }

        protected void btnCancelarAgregar_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "closeWindow(0);", true);
        }   
    }
}