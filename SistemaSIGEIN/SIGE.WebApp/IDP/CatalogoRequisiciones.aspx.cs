using WebApp.Comunes;
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
using System.Xml.Linq;
using Telerik.Web.UI;
using SIGE.Negocio.Utilerias;
using System.Web.Security;
using System.Text;

namespace SIGE.WebApp.Administracion
{
    public partial class CatalogoRequisiciones : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario = string.Empty;
        private string vNbPrograma = string.Empty;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        //StringBuilder builder = new StringBuilder();
        //string Email { set; get; }

        private int vIdRequisicion
        {
            get { return (int)ViewState["vsID_REQUISICION"]; }
            set { ViewState["vsID_REQUISICION"] = value; }
        }

        //private List<SPE_OBTIENE_K_REQUISICION_Result> Requisiciones;

        private string vEstatus
        {
            get { return (string)ViewState["vEstatus"]; }
            set { ViewState["vEstatus"] = value; }
        }

        #endregion

        protected void SeguridadProcesos()
        {
            btnGuardar.Enabled = ContextoUsuario.oUsuario.TienePermiso("J.A.A.A");
            btnEditar.Enabled = ContextoUsuario.oUsuario.TienePermiso("J.A.A.B");
            btnCancelar.Enabled = ContextoUsuario.oUsuario.TienePermiso("J.A.A.C");
            btnEliminar.Enabled = ContextoUsuario.oUsuario.TienePermiso("J.A.A.D");
            btnBuscarCandidato.Enabled = ContextoUsuario.oUsuario.TienePermiso("J.A.A.E");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
                SeguridadProcesos();

        }

        protected void grdRequisicion_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RequisicionNegocio nRequisicion = new RequisicionNegocio();
            grdRequisicion.DataSource = nRequisicion.ObtieneRequisicion(pIdEmpresa: ContextoUsuario.oUsuario.ID_EMPRESA);

        }

        protected void btnEliminar_click(object sender, EventArgs e)
        {
            RequisicionNegocio negocio = new RequisicionNegocio();
            foreach (GridDataItem item in grdRequisicion.SelectedItems)
            {
                vIdRequisicion = (int.Parse(item.GetDataKeyValue("ID_REQUISICION").ToString()));

                E_RESULTADO vResultado = negocio.Elimina_K_REQUISICION(ID_REQUISICION: vIdRequisicion, programa: vNbPrograma, usuario: vClUsuario);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindow");
            }
        }

        protected void grdRequisicion_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            RequisicionNegocio nReq = new RequisicionNegocio();
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;

            int vIdRequisicion = int.Parse(dataItem.GetDataKeyValue("ID_REQUISICION").ToString());

            e.DetailTableView.DataSource = nReq.ObtenerCandidatosPorRequisicion(pIdRequisicion: vIdRequisicion);
        }

        protected void grdRequisicion_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;

                if (e.Item.OwnerTableView.Name == "CandidatosAsociados")
                {
                    GridDataItem dataItem = e.Item as GridDataItem;
                    if (dataItem["CL_ESTATUS_CANDIDATO_REQUISICION"].Text == "Contratado")
                        item.BackColor = System.Drawing.ColorTranslator.FromHtml("#C6DB95");
                }
            }

            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdRequisicion.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdRequisicion.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdRequisicion.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdRequisicion.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdRequisicion.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            RequisicionNegocio negocio = new RequisicionNegocio();
            foreach (GridDataItem item in grdRequisicion.SelectedItems)
            {
                vIdRequisicion = (int.Parse(item.GetDataKeyValue("ID_REQUISICION").ToString()));
                E_RESULTADO vResultado = negocio.ActualizaEstatusRequisicion(ID_REQUISICION: vIdRequisicion, programa: vNbPrograma, usuario: vClUsuario);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindow");
            }
        }

        protected void grdRequisicion_ItemCommand(object sender, GridCommandEventArgs e)
        {
            string vClCommandName = e.CommandName;
            if (vClCommandName == "Delete")
            {
                int vIdRequisicion;
                int vIdCandidato;

                int.TryParse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID_REQUISICION"].ToString(), out vIdRequisicion);
                int.TryParse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID_CANDIDATO"].ToString(), out vIdCandidato);

                E_RESULTADO vResultado = new RequisicionNegocio().EliminarCandidatoRequisicion(vIdRequisicion, vIdCandidato, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindow");
            }
        }

    }
}