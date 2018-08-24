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

namespace SIGE.WebApp.PDE
{
    public partial class VentanaComunicadosLeidos : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        ListaComunicadosNegocio negocio = new ListaComunicadosNegocio();
        private int vIdComunicado
        {
            get { return (int)ViewState["vs_vIdComunicado"]; }
            set { ViewState["vs_vIdComunicado"] = value; }
        }
   

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                //vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
                //vNbPrograma = ContextoUsuario.nbPrograma;
                if (Request.Params["IdComunicado"] != null)
                {
                    vIdComunicado = int.Parse(Request.Params["IdComunicado"]);
                }
            }

        }

        protected void grdAdmcomunicados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            List<SPE_OBTIENE_K_COMUNICADO_LEIDO_Result> Admcomunicados = new List<SPE_OBTIENE_K_COMUNICADO_LEIDO_Result>();
            Admcomunicados = negocio.ObtenerEmpleadosComunicadosLeidos(vIdComunicado);
            grdAdmcomunicados.DataSource = Admcomunicados;

        }
    }
}