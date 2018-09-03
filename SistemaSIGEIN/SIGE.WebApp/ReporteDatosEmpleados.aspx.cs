using SIGE.Entidades;
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

namespace SIGE.WebApp.Administracion
{
      
    public partial class ReporteDatosEmpleados : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private void ExportarExcel()
        {
            grdEmpleados.ExportSettings.OpenInNewWindow = true;
            foreach (GridColumn col in grdEmpleados.MasterTableView.RenderColumns)
            {
                col.Display = true;
            }
            grdEmpleados.Rebind();
            grdEmpleados.MasterTableView.ExportToExcel();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
        }

        
        protected void grdEmpleados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            SolicitudNegocio empleados = new SolicitudNegocio();
            List<SPE_OBTIENE_DATOS_EMPLEADOS_Result> listaEmpleados = new List<SPE_OBTIENE_DATOS_EMPLEADOS_Result>();
            listaEmpleados = empleados.ObtieneDatosEmpleados();
            grdEmpleados.DataSource = listaEmpleados;
        
        }

        protected void grdEmpleados_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "ExportToExcel":
                    ExportarExcel();
                    break;
                default:
                    break;
            }
        }
    }
}