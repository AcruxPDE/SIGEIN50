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
    public partial class CentroOperativo : System.Web.UI.Page
    {
        private string usuario;
        private string programa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        protected void Page_Load(object sender, EventArgs e)
        {
            usuario = ContextoUsuario.oUsuario.CL_USUARIO;
            programa = ContextoUsuario.nbPrograma;
        }

        protected void grdCentroOperativo_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            CentroOperativoNegocio nCentroOperativo = new CentroOperativoNegocio();
            grdCentroOperativo.DataSource = nCentroOperativo.Obtener_C_CENTRO_OPTVO(null);
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            CentroOperativoNegocio nCentroOptvo = new CentroOperativoNegocio();
            foreach (GridDataItem item in grdCentroOperativo.SelectedItems)
            {
                E_RESULTADO vResultado = nCentroOptvo.EliminarCentroOptvo(Guid.Parse(item.GetDataKeyValue("ID_CENTRO_OPTVO").ToString()), usuario, programa);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindow");
            }
        }

        protected void grdCentroOperativo_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdCentroOperativo.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdCentroOperativo.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdCentroOperativo.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdCentroOperativo.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdCentroOperativo.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }       
    }
}