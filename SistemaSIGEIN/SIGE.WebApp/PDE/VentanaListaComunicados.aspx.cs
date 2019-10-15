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
using Telerik.Web.UI;

namespace SIGE.WebApp.PDE
{
    public partial class VentanaListaComunicados : System.Web.UI.Page
    {

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        ListaComunicadosNegocio negocio = new ListaComunicadosNegocio();

        private int vIdComunicado
        {
            get { return (int)ViewState["vs_vIdComunicado"]; }
            set { ViewState["vs_vIdComunicado"] = value; }
        }

        private List<E_OBTIENE_ADM_COMUNICADO> vAdmcomunicados
        {
            get { return (List<E_OBTIENE_ADM_COMUNICADO>)ViewState["vs_vAdmcomunicados"]; }
            set { ViewState["vs_vAdmcomunicados"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
                //vNbPrograma = ContextoUsuario.nbPrograma;
            }

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

        }

        protected void grdAdmcomunicados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            vAdmcomunicados = negocio.ObtenerComunicados();
            grdAdmcomunicados.DataSource = vAdmcomunicados;
        }

        protected void rbEliminar_Click(object sender, EventArgs e)
        {
            GridDataItem item = (GridDataItem)grdAdmcomunicados.SelectedItems[0];
            int datakey = int.Parse(item.GetDataKeyValue("ID_COMUNICADO").ToString());
            E_RESULTADO vResultado = negocio.EliminaComunicado(datakey, vNbPrograma, vClUsuario);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, null);
        }

        protected void rbEliminar_Click1(object sender, EventArgs e)
        {
          
            
        }

    }
}