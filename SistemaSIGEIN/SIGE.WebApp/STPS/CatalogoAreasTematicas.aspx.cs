using SIGE.Entidades;
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
    public partial class CatalogoAreasTematicas : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private int vID_AREA_TEMATICA
        {
            get { return (int)ViewState["vsID_AREA_TEMATICA"]; }
            set { ViewState["vsID_AREA_TEMATICA"] = value; }
        }

        private List<SPE_OBTIENE_C_AREA_TEMATICA_Result> AreasTematicas;
        AreaTematicaNegocio negocio = new AreaTematicaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            AreasTematicas = new List<SPE_OBTIENE_C_AREA_TEMATICA_Result>();
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {

            }
            AreasTematicas = negocio.Obtener_C_AREA_TEMATICA();
        }

        protected void grdAreasTematicas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdAreasTematicas.DataSource = AreasTematicas;
        }

        protected void grdAreasTematicas_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdAreasTematicas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdAreasTematicas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdAreasTematicas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdAreasTematicas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdAreasTematicas.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void grdAreasTematicas_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "RowClick")
            {

            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            var valida_eliminacion = false;
            foreach (GridDataItem item in grdAreasTematicas.SelectedItems)
            {
                valida_eliminacion = true;
                vID_AREA_TEMATICA = (int.Parse(item.GetDataKeyValue("ID_AREA_TEMATICA").ToString()));
                E_RESULTADO vResultado = negocio.Elimina_C_AREA_TEMATICA(vID_AREA_TEMATICA, vNbPrograma, vClUsuario);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindow");

            }
        }
    }
}