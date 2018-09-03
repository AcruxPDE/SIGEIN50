using SIGE.Entidades.Externas;
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
    public partial class Configuracion : System.Web.UI.Page
    {
        #region Variables

        private string usuario;
        private string programa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            usuario = ContextoUsuario.oUsuario.CL_USUARIO;
            programa = ContextoUsuario.nbPrograma;
            if (!IsPostBack)
            {
                if (ContextoApp.FYD.ClVistaPrograma.ClVistaPrograma == "MATRIZ")
                    btnMatriz.Checked = true;
                else
                    btnMacros.Checked = true;
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (btnMatriz.Checked == true)
                ContextoApp.FYD.ClVistaPrograma.ClVistaPrograma = "MATRIZ";
            else
                ContextoApp.FYD.ClVistaPrograma.ClVistaPrograma = "MACROS";

            E_RESULTADO vResultado = ContextoApp.SaveConfiguration(usuario, programa);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

        }
    }
}