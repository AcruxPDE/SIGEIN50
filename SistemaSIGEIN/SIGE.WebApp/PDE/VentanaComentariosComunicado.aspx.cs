using SIGE.Entidades.Externas;
using SIGE.Entidades.PuntoDeEncuentro;
using SIGE.Negocio.PuntoDeEncuentro;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.PDE
{
    public partial class VentanaComentariosComunicado : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        VisorComunicadoNegocio negocio = new VisorComunicadoNegocio();



        public int vIdComunicado
        {
            set { ViewState["vs_IdComunicado"] = value; }
            get { return (int)ViewState["vs_IdComunicado"]; }
        }
        public string  vModo
        {
            set { ViewState["vs_vModo"] = value; }
            get { return (string )ViewState["vs_vModo"]; }
        }
        private List<E_OBTIENE_ADM_COMUNICADO> vAdmcomunicados
        {
            get { return (List<E_OBTIENE_ADM_COMUNICADO>)ViewState["vs_vAdmcomunicados"]; }
            set { ViewState["vs_vAdmcomunicados"] = value; }
        }

        private List<E_OBTIENE_COMUNICADO> ListaComunicados
        {
            get { return (List<E_OBTIENE_COMUNICADO>)ViewState["vs_vObtieneComunicado"]; }
            set { ViewState["vs_vObtieneComunicado"] = value; }
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

                if (Request.Params["IdComunicado"] != null)
                {
                    vIdComunicado = int.Parse(Request.Params["IdComunicado"]);
                }
                if (Request.Params["modo"] != null)
                {
                    vModo  = Request.Params["modo"];
                }


                if (vModo == "Privado")
                {
                    btnAgregar.Enabled = false;
                    rlMensajePrivado.Visible = true;
                }
                else {
                    btnAgregar.Enabled = true;
                    rlMensajePrivado.Visible = false;
                }

            }
   
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
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

        protected void rlvComentarios_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
        {
           
            rlvComentarios.DataSource = negocio.ObtenerComentarios_Comunicado(null, vIdComunicado);
        }
    }
}