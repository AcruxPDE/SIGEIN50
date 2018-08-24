using SIGE.Entidades.Externas;
using SIGE.Negocio.MetodologiaCompensacion;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.MPC
{
    public partial class SeleccionTabulador : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int? vIdTabulador
        {
            get { return (int?)ViewState["vs_vIdTabulador"]; }
            set { ViewState["vs_vIdTabulador"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
         if (!IsPostBack)
            {
                if (Request.Params["pIdTabulador"] != null)
                {
                    vIdTabulador = int.Parse(Request.Params["pIdTabulador"].ToString());
                }

                if (Request.Params["pFgMultSeleccion"] == "0")
                    grdTabulador.AllowMultiRowSelection = false;

            }

        }
        protected void grdTabulador_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            if (vIdTabulador != null)
            {
                var vTabuladorFiltrado = nTabulador.ObtenerTabuladoresNivel(vIdTabulador);
                grdTabulador.DataSource = vTabuladorFiltrado;
            }
            else
            {
                var vTabulador = nTabulador.ObtenerTabuladores();
                grdTabulador.DataSource = vTabulador;
            }
        }

        protected void grdTabulador_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdTabulador.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdTabulador.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdTabulador.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdTabulador.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdTabulador.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}