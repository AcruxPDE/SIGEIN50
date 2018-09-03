using SIGE.Entidades.Externas;
using SIGE.Negocio.SecretariaTrabajoPrevisionSocial;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.STPS
{
    public partial class OcupacionesNacionales : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int vID_AREA
        {
            get { return (int)ViewState["vsID_AREA"]; }
            set { ViewState["vsID_AREA"] = value; }
        }

        private int vID_SUBAREA
        {
            get { return (int)ViewState["vsID_SUBAREA"]; }
            set { ViewState["vsID_SUBAREA"] = value; }
        }

        private int vID_MODULO
        {
            get { return (int)ViewState["vsID_MODULO"]; }
            set { ViewState["vsID_MODULO"] = value; }
        }

        private int vID_OCUPACION
        {
            get { return (int)ViewState["vsID_OCUPACION"]; }
            set { ViewState["vsID_OCUPACION"] = value; }
        }

        OcupacionesNegocio negocio= new OcupacionesNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {

            }
        }

        protected void grdAreas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdAreas.DataSource = negocio.Obtener_AREA_OCUPACION();
        }

        protected void grdAreas_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
            }

            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdAreas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdAreas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdAreas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdAreas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdAreas.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void grdAreas_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void btnEliminarArea_Click(object sender, EventArgs e)
        {
            var valida_eliminacion = false;
            foreach (GridDataItem item in grdAreas.SelectedItems)
            {
                valida_eliminacion = true;
                vID_AREA = (int.Parse(item.GetDataKeyValue("ID_AREA").ToString()));
                string CL_AREA = item.GetDataKeyValue("CL_AREA").ToString();
                E_RESULTADO vResultado = negocio.Elimina_C_AREA(vID_AREA, CL_AREA, vNbPrograma, vClUsuario);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindow");

            }
        }

        protected void grdSubareas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdSubareas.DataSource = negocio.Obtener_SUBAREA_OCUPACION();
        }

        protected void grdSubareas_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
            }

            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdSubareas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdSubareas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdSubareas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdSubareas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdSubareas.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void grdSubareas_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "RowClick")
            {

            }
        }

        protected void btnEliminarSubarea_Click(object sender, EventArgs e)
        {
            var valida_eliminacion = false;
            foreach (GridDataItem item in grdSubareas.SelectedItems)
            {
                valida_eliminacion = true;
                vID_SUBAREA = (int.Parse(item.GetDataKeyValue("ID_SUBAREA").ToString()));
                string CL_SUBAREA = item.GetDataKeyValue("CL_SUBAREA").ToString();
                E_RESULTADO vResultado = negocio.Elimina_C_SUBAREA(vID_SUBAREA, CL_SUBAREA, vNbPrograma, vClUsuario);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindowSubarea");

            }
        }

        protected void grdModulos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdModulos.DataSource = negocio.Obtener_MODULO_OCUPACION();
        }

        protected void grdModulos_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
            }

            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdModulos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdModulos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdModulos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdModulos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdModulos.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void grdModulos_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "RowClick")
            {

            }
        }

        protected void btnEliminarModulo_Click(object sender, EventArgs e)
        {
            var valida_eliminacion = false;
            foreach (GridDataItem item in grdModulos.SelectedItems)
            {
                valida_eliminacion = true;
                vID_MODULO = (int.Parse(item.GetDataKeyValue("ID_MODULO").ToString()));
                string CL_MODULO = item.GetDataKeyValue("CL_MODULO").ToString();
                E_RESULTADO vResultado = negocio.Elimina_C_MODULO(vID_MODULO, CL_MODULO, vNbPrograma, vClUsuario);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindowModulo");

            }
        }

        protected void grdOcupaciones_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdOcupaciones.DataSource = negocio.Obtener_OCUPACIONES();
        }

        protected void grdOcupaciones_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
            }

            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdOcupaciones.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdOcupaciones.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdOcupaciones.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdOcupaciones.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdOcupaciones.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void grdOcupaciones_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "RowClick")
            {

            }
        }

        protected void btnEliminarOcupacion_Click(object sender, EventArgs e)
        {
            var valida_eliminacion = false;
            foreach (GridDataItem item in grdOcupaciones.SelectedItems)
            {
                valida_eliminacion = true;
                vID_OCUPACION = (int.Parse(item.GetDataKeyValue("ID_OCUPACION").ToString()));
                E_RESULTADO vResultado = negocio.Elimina_C_OCUPACION(vID_OCUPACION, vNbPrograma, vClUsuario);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindowOcupacion");

            }
        }
    }
}