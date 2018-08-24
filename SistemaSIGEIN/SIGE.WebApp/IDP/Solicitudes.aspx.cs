using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Telerik.Web.UI;

namespace SIGE.WebApp.IDP
{
    public partial class Solicitudes : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int vIdSolicitud
        {
            get { return (int)ViewState["vs_vIdSolicitud"]; }
            set { ViewState["vs_vIdSolicitud"] = value; }
        }

        private bool vAgregarSolicitud;
        private bool vModificarSolicitud;
        private bool vEliminarSolicitud;
        private bool vContratarEmpleado;
        private bool vResultadoPruebas;
        private bool vProcesoSeleccion;
        private bool vConsultasPersonales;

        private void SeguridadProcesos()
        {
            btnGuardar.Enabled = vAgregarSolicitud = ContextoUsuario.oUsuario.TienePermiso("A.A.A");
            btnEditar.Enabled = vModificarSolicitud = ContextoUsuario.oUsuario.TienePermiso("A.A.B");
            btnEliminar.Enabled = vEliminarSolicitud = ContextoUsuario.oUsuario.TienePermiso("A.A.C");
            btnProceso.Enabled = vContratarEmpleado = ContextoUsuario.oUsuario.TienePermiso("A.A.D");
            btnPruebas.Enabled = vResultadoPruebas = ContextoUsuario.oUsuario.TienePermiso("A.A.E");
            btnProcesoSeleccion.Enabled = vProcesoSeleccion = ContextoUsuario.oUsuario.TienePermiso("A.A.F");
            btnConsultas.Enabled = vConsultasPersonales = ContextoUsuario.oUsuario.TienePermiso("A.A.G");
        }

        protected void Page_Init(object sender, System.EventArgs e)
        {
            SeguridadProcesos();
            DefineGrid();
        }

        private void DefineGrid()
        {
            SolicitudNegocio nSolicitud = new SolicitudNegocio();
            List<SPE_OBTIENE_SOLICITUDES_Result> vSolicitudes;
            vSolicitudes = nSolicitud.ObtieneCatalogoSolicitudes();
            CamposAdicionales cad = new CamposAdicionales();
            grdSolicitudes.DataSource = vSolicitudes;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

           

            if (!Page.IsPostBack)
            {
                grdSolicitudes.AllowMultiRowSelection = false;
                if (!String.IsNullOrEmpty(Request.QueryString["mulSel"]))
                {
                    grdSolicitudes.AllowMultiRowSelection = (Request.QueryString["mulSel"] == "1");
                }
            }

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
        }
  
        protected void grdSolicitudes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            SolicitudNegocio nSolicitud = new SolicitudNegocio();
            grdSolicitudes.DataSource = nSolicitud.ObtieneSolicitudes();
        }

        protected void btnEliminar_click(object sender, EventArgs e)
        {
            SolicitudNegocio nSolicitud = new SolicitudNegocio();
            foreach (GridDataItem item in grdSolicitudes.SelectedItems)
            {
                vIdSolicitud = (int.Parse(item.GetDataKeyValue("ID_SOLICITUD").ToString()));
                var vSolicitud = nSolicitud.ObtieneSolicitudes(ID_SOLICITUD: vIdSolicitud).FirstOrDefault();
                E_RESULTADO vResultado = nSolicitud.Elimina_K_SOLICITUD(vIdSolicitud, vNbPrograma, vClUsuario);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindow");
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/IDP/Solicitud/Solicitud.aspx");
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in grdSolicitudes.SelectedItems)
            {
                vIdSolicitud = (int.Parse(item.GetDataKeyValue("ID_SOLICITUD").ToString()));
                Response.Redirect("~/IDP/Solicitud.aspx?&ID=" + vIdSolicitud);
            }
        }

        protected void ftrGrdSolicitudes_PreRender(object sender, EventArgs e)
        {
            var menu = ftGrdSolicitudes.FindControl("rfContextMenu") as RadContextMenu;
            menu.DefaultGroupSettings.Height = Unit.Pixel(300);
            menu.EnableAutoScroll = false;
        }

        protected void grdSolicitudes_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdSolicitudes.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdSolicitudes.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdSolicitudes.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdSolicitudes.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdSolicitudes.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}