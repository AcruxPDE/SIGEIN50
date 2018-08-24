using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaCatalogoIdiomas : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario ;
        private string vNbPrograma ;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int vIdIdioma
        {
            set { ViewState["vIdIdioma"] = value; }
            get { return (int)ViewState["vIdIdioma"]; }
        }

        #endregion

        #region Funciones

        private void cargarIdioma()
        {
            SPE_OBTIENE_C_IDIOMA_Result idioma = new SPE_OBTIENE_C_IDIOMA_Result();
            IdiomaNegocio neg = new IdiomaNegocio();

            idioma = neg.Obtener_C_IDIOMA(vIdIdioma).FirstOrDefault();

            if (idioma != null)
            {
                txtClIdioma.Text = idioma.CL_IDIOMA;
                txtNbIdioma.Text = idioma.NB_IDIOMA;
                chkActivo.Checked = idioma.FG_ACTIVO;
            }

        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                //Agregar aqui el parametro para la edicion
                vIdIdioma = 0;

                if (Request.Params["ID"] != null)
                {
                    vIdIdioma = int.Parse(Request.Params["ID"]);
                    cargarIdioma();
                }

            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            SPE_OBTIENE_C_IDIOMA_Result idioma = new SPE_OBTIENE_C_IDIOMA_Result();

            string tipo_transaccion = E_TIPO_OPERACION_DB.I.ToString();

            if (vIdIdioma != 0)
            {
                tipo_transaccion = E_TIPO_OPERACION_DB.A.ToString();
                idioma.ID_IDIOMA = vIdIdioma;
            }

            idioma.CL_IDIOMA = txtClIdioma.Text;
            idioma.NB_IDIOMA = txtNbIdioma.Text;
            idioma.FG_ACTIVO = chkActivo.Checked;

            IdiomaNegocio neg = new IdiomaNegocio();
            E_RESULTADO vResultado = neg.InsertaActualiza_C_IDIOMA(tipo_transaccion, idioma,vClUsuario, vNbPrograma);
             string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            // UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);


            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
          //  Page.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "closeWindow(0);", true);
        }

    }
}