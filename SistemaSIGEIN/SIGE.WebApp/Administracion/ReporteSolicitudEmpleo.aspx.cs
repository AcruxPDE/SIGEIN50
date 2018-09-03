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
    public partial class ReporteSolicitudEmpleo : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private void ExportarExcel()
        {
            grdSolicitud.ExportSettings.OpenInNewWindow = true;
            foreach (GridColumn col in grdSolicitud.MasterTableView.RenderColumns)
            {
                col.Display = true;
            }
            grdSolicitud.Rebind();
            grdSolicitud.MasterTableView.ExportToExcel();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
            if (!IsPostBack) 
            {
                grdSolicitud.EnableLinqExpressions = false;
            }
        }

        protected void grdSolicitud_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            SolicitudNegocio solicitudes = new SolicitudNegocio();
            List<SPE_OBTIENE_SOLICITUDES_EMPLEO_Result> listaCandidatos = new List<SPE_OBTIENE_SOLICITUDES_EMPLEO_Result>();
            listaCandidatos = solicitudes.ObtieneSolicitudesEmpleo();
            grdSolicitud.DataSource = listaCandidatos;
            
     
        }

        protected void grdSolicitud_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
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

        protected void grdSolicitud_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdSolicitud.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdSolicitud.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdSolicitud.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdSolicitud.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdSolicitud.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }


    }
}