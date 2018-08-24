using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
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
    public partial class InventarioDePersonal : System.Web.UI.Page
    {
        #region Variables

        private int? vID_EMPLEADO
        {
            get { return (int?)ViewState["vsID_EMPLEADO"]; }
            set { ViewState["vsID_EMPLEADO"] = value; }
        }

        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private bool vAgregar;
        private bool vEditar;
        private bool vEliminar;
        private bool vDarBaja;

        #endregion

        #region Funcion

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

        private void SeguridadProcesos()
        {
            btnAgregar.Enabled = vAgregar = ContextoUsuario.oUsuario.TienePermiso("B.A");
            btnEditar.Enabled = vEditar = ContextoUsuario.oUsuario.TienePermiso("B.B");
            btnEliminar.Enabled = vEliminar = ContextoUsuario.oUsuario.TienePermiso("B.C");
            btnDarDeBaja.Enabled = vDarBaja = ContextoUsuario.oUsuario.TienePermiso("B.D");
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
            SeguridadProcesos();

           
        }

        protected void grdEmpleados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            EmpleadoNegocio nEmpleados = new EmpleadoNegocio();
            List<SPE_OBTIENE_EMPLEADOS_Result> CatalogoListaNegocioEmp = new List<SPE_OBTIENE_EMPLEADOS_Result>();
            CatalogoListaNegocioEmp = nEmpleados.ObtenerEmpleados(pID_EMPRESA: vIdEmpresa);
            grdEmpleados.DataSource = CatalogoListaNegocioEmp;

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            EmpleadoNegocio nEmpleados = new EmpleadoNegocio();
            var valida_eliminacion = false;
            foreach (GridDataItem item in grdEmpleados.SelectedItems)
            {
                valida_eliminacion = true;
                vID_EMPLEADO = (int.Parse(item.GetDataKeyValue("M_EMPLEADO_ID_EMPLEADO").ToString()));
                string CVE_EMPLEADO = item.GetDataKeyValue("M_EMPLEADO_CL_EMPLEADO").ToString();
                E_RESULTADO vResultado = nEmpleados.Elimina_M_EMPLEADO(vID_EMPLEADO, CVE_EMPLEADO, vNbPrograma, vClUsuario);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindow");

            }
        }

        protected void grdEmpleados_ItemCommand(object sender, GridCommandEventArgs e)
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

        protected void btnCancelarBaja_Click(object sender, EventArgs e)
        {
            EmpleadoNegocio nEmpleados = new EmpleadoNegocio();
            GridDataItem itemId = (GridDataItem)grdEmpleados.SelectedItems[0];
            int vIdEmpleado = (int.Parse(itemId.GetDataKeyValue("M_EMPLEADO_ID_EMPLEADO").ToString()));

            List<SPE_OBTIENE_EMPLEADOS_Result> CatalogoListaNegocioEmp = new List<SPE_OBTIENE_EMPLEADOS_Result>();
            CatalogoListaNegocioEmp = nEmpleados.ObtenerEmpleados(pID_EMPRESA: vIdEmpresa);
            if (nEmpleados.ObtenerEmpleados(pID_EMPRESA: vIdEmpresa, pFgActivo: true).Count() + 1 > ContextoApp.InfoEmpresa.Volumen)
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Se ha alcanzado el máximo número de empleados para la licencia y no es posible agregar más.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                return;
            }
            E_RESULTADO vResultado = nEmpleados.CancelaBajaEmpleado(vIdEmpleado, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, null);
            grdEmpleados.Rebind();
        }

        protected void grdEmpleados_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdEmpleados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdEmpleados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdEmpleados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdEmpleados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdEmpleados.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}