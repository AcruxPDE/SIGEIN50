using SIGE.Entidades.Externas;
using SIGE.Negocio.AdministracionSitio;
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
    public partial class Plazas : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private int? vIdRol;

        protected void SeguridadProcesos()
        {
            btnAgregar.Enabled = ContextoUsuario.oUsuario.TienePermiso("D.A");
            btnEditar.Enabled = ContextoUsuario.oUsuario.TienePermiso("D.B");
            btnEliminar.Enabled = ContextoUsuario.oUsuario.TienePermiso("D.C");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;

            if (!IsPostBack)
            {
                SeguridadProcesos();
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            PlazaNegocio nPlaza = new PlazaNegocio();

            foreach (GridDataItem item in grdPlazas.SelectedItems)
            {
                E_RESULTADO vResultado = nPlaza.EliminaPlaza(int.Parse(item.GetDataKeyValue("ID_PLAZA").ToString()), vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
                grdPlazas.Rebind();
            }
        }

        protected void grdPlazas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PlazaNegocio nPlaza = new PlazaNegocio();
            grdPlazas.DataSource = nPlaza.ObtienePlazas(pID_EMPRESA: vIdEmpresa, pID_ROL: vIdRol);
        }

        protected void grdPlazas_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdPlazas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdPlazas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdPlazas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdPlazas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdPlazas.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}