using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Negocio.Administracion;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.IDP.Pruebas
{
    public partial class VentanaObtenerFolioSolicitud : System.Web.UI.Page
    {
        protected List<E_SELECCIONADOS> vLstCandidatos
        {
            get { return (List<E_SELECCIONADOS>)ViewState["vs_vLstCandidatos"]; }
            set { ViewState["vs_vLstCandidatos"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                vLstCandidatos = new List<E_SELECCIONADOS>();
            }

        }

        protected void grdCandidatos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdCandidatos.DataSource = vLstCandidatos;
        }

        protected void grdCandidatos_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdCandidatos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdCandidatos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdCandidatos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdCandidatos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdCandidatos.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != "" && txtApellido.Text != "")
            {
                PruebasNegocio pNegocio = new PruebasNegocio();
                vLstCandidatos = pNegocio.ObtenerCandidatoFolio(txtNombre.Text, txtApellido.Text, null).Select(s => new E_SELECCIONADOS
                    {
                        ID_SELECCION = s.ID_SOLICITUD,
                        CL_SELECCION = s.CL_SOLICITUD,
                        NB_SELECCION = s.NB_COMPLETO,
                        DS_SELECCION = s.NB_APELLIDO_PATERNO
                    }).ToList();

                grdCandidatos.Rebind();
            }
            else
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Ingresa tu nombre y apellido paterno para realizar la búsqueda", Entidades.Externas.E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
        }
    }
}