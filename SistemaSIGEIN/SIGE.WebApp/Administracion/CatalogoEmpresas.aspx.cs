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

namespace SIGE.WebApp.Administracion
{
    public partial class CatalogoEmpresas : System.Web.UI.Page
    {

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private int vID_EMPRESA
        {
            get { return (int)ViewState["vsID_EMPRESA"]; }
            set { ViewState["vsID_EMPRESA"] = value; }
        }
        private int? vIdEmpresa;

        private List<SPE_OBTIENE_C_EMPRESA_Result> Empresas;



        protected void Page_Load(object sender, EventArgs e)
        {
            Empresas = new List<SPE_OBTIENE_C_EMPRESA_Result>();
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;

            if (!IsPostBack)
            {

            }

            EmpresaNegocio negocio = new EmpresaNegocio();
            Empresas = negocio.Obtener_C_EMPRESA();

        }

        #region grdCatEmpresas_NeedDataSource
        protected void grdCatEmpresas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdCatEmpresas.DataSource = Empresas;

        }

        #endregion

        #region grdCatEmpresas_ItemDataBound
        protected void grdCatEmpresas_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdCatEmpresas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdCatEmpresas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdCatEmpresas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdCatEmpresas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdCatEmpresas.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }


        }
        #endregion

        #region OnItemCommand
        protected void OnItemCommand(object sender, GridCommandEventArgs e)
        {


            if (e.CommandName == "RowClick")
            {

            }
        }
        #endregion


        protected void btnEliminar_click(object sender, EventArgs e)
        {

            EmpresaNegocio negocio = new EmpresaNegocio();
            var valida_eliminacion = false;
            foreach (GridDataItem item in grdCatEmpresas.SelectedItems)
            {
                valida_eliminacion = true;

                vID_EMPRESA = (int.Parse(item.GetDataKeyValue("ID_EMPRESA").ToString()));

                E_RESULTADO vResultado = negocio.Elimina_C_EMPRESA(ID_EMPRESA: vID_EMPRESA, programa: vNbPrograma, usuario: vClUsuario);

                //E_RESULTADO vResultado  = negocio.Elimina_M_PUESTO(ID_PUESTO: vID_PUESTO, programa: "CatalogoDescriptivoPuestos.aspx", usuario: "felipe");

                //E_RESULTADO vResultado = negocio.Elimina_C_COMPETENCIA(lista.ID_COMPETENCIA, usuario, programa);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindow");

            }

        }
    }
}