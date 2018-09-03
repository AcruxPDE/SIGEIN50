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
    public partial class VentanaListaPorRevisarInformacion : System.Web.UI.Page
    {
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        public string vNbPrograma;
        public string vClUsuario;
        public string id_empleado;
        private int vIdComunicado
        {
            get { return (int)ViewState["vs_vObtieneId"]; }
            set { ViewState["vs_vObtieneId"] = value; }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vIdComunicado = int.Parse(Request.Params["IdComunicado"]);
            id_empleado = ContextoUsuario.oUsuario.CL_USUARIO;

            if (Request.Params["TipoComunicado"] != null)
            {
                var vTipoComunicado = Request.Params["TipoComunicado"];
                if (vTipoComunicado == "D") 
                {
                    grdSeleccionEmpleado.MasterTableView.Columns.FindByUniqueName("NB_EMPLEADO").Visible = false;
                    grdSeleccionEmpleado.MasterTableView.Columns.FindByUniqueName("ID_EMPLEADO").Visible = false;
                }
            }

        }

        protected void grdSeleccionEmpleado_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            List<SPE_OBTIENE_EMPLEADOS_COMUNICADO_INFORMACION_Result> ListaEmpleados = new List<SPE_OBTIENE_EMPLEADOS_COMUNICADO_INFORMACION_Result>();
            VisorComunicadoNegocio negocio = new VisorComunicadoNegocio();
            ListaEmpleados = negocio.ObtenerEmpleadosInformacion(vIdComunicado, id_empleado );
            grdSeleccionEmpleado.DataSource = ListaEmpleados;
        }
    }
}