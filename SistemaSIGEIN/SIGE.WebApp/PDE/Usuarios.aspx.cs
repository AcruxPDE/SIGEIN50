using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.PDE
{
    public partial class Usuarios : System.Web.UI.Page
    { private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
        }

        protected void grdUsuarios_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            UsuarioNegocio nUsuarios = new UsuarioNegocio();
            grdUsuarios.DataSource = nUsuarios.ObtieneUsuarios(null);
        }
        protected void grdContrasenas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            UsuarioNegocio nUsuarios = new UsuarioNegocio();
            grdContrasenas.DataSource = nUsuarios.ObtieneUsuarios(null);
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            UsuarioNegocio nUsuario = new UsuarioNegocio();

            foreach (GridDataItem item in grdUsuarios.SelectedItems)
            {
                E_RESULTADO vResultado = nUsuario.EliminaUsuario(item.GetDataKeyValue("CL_USUARIO").ToString(), vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindow");
            }
        }
    }
    }

