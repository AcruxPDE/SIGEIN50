using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

using SIGE.Entidades;
using SIGE.Negocio.Administracion;
using SIGE.Entidades.Externas;
using SIGE.WebApp.Comunes;

namespace SIGE.WebApp.Administracion
{
    public partial class Localizacion : System.Web.UI.Page
    {
        private List<SPE_OBTIENE_C_ESTADO_Result> listaEstados;
        private List<SPE_OBTIENE_C_MUNICIPIO_Result> listaMunicipios;

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                traerEstados();
            }
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

        }

        public void traerEstados()
        {
            EstadoNegocio negocioEstado = new EstadoNegocio();
            listaEstados = negocioEstado.ObtieneEstados();
            cmbEstados.DataSource = listaEstados;
            cmbEstados.DataTextField = "NB_ESTADO";
            cmbEstados.DataValueField = "CL_ESTADO";
            cmbEstados.DataBind();
        }

        public void traerMunicipios(string pCL_ESTADO)
        {
            cmbMunicipio.Text = string.Empty;
            MunicipioNegocio negocioMunicipio = new MunicipioNegocio();
            listaMunicipios = negocioMunicipio.ObtieneMunicipios(pClEstado: pCL_ESTADO);
            cmbMunicipio.DataSource = listaMunicipios;
            cmbMunicipio.DataTextField = "NB_MUNICIPIO";
            cmbMunicipio.DataValueField = "CL_MUNICIPIO";
            cmbMunicipio.DataBind();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            ColoniaNegocio negocioColonia = new ColoniaNegocio();
            GridCatalogoLista.DataSource = negocioColonia.ObtieneColonias(pClEstado: cmbEstados.SelectedValue, pClMunicipio: cmbMunicipio.SelectedValue);
            GridCatalogoLista.Rebind();
        }

        protected void GridCatalogoLista_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ColoniaNegocio negocioColonia = new ColoniaNegocio();
            GridCatalogoLista.DataSource = negocioColonia.ObtieneColonias(pClEstado: cmbEstados.SelectedValue, pClMunicipio: cmbMunicipio.SelectedValue);
        }

        protected void GridCatalogoLista_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", GridCatalogoLista.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", GridCatalogoLista.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", GridCatalogoLista.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", GridCatalogoLista.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", GridCatalogoLista.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void cmbEstados_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            GridCatalogoLista.DataSource = new string[] { };
            GridCatalogoLista.Rebind();
            traerMunicipios(e.Value);
        }

        protected void RadBtnEliminar_Click(object sender, EventArgs e)
        {
            ColoniaNegocio nColonia = new ColoniaNegocio();

            foreach (GridDataItem item in GridCatalogoLista.SelectedItems)
            {
                E_RESULTADO vResultado = nColonia.Elimina_C_COLONIA(int.Parse(item.GetDataKeyValue("ID_COLONIA").ToString()),vClUsuario,vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindow");
            }

        }

        protected void cmbColonia_Load(object sender, EventArgs e)
        {
            RadComboBox cmbColonia = sender as RadComboBox;
            ColoniaNegocio negocioColonia = new ColoniaNegocio();
            cmbColonia.DataSource = negocioColonia.ObtieneColonias(pClEstado: cmbEstados.SelectedValue, pClMunicipio: cmbMunicipio.SelectedValue);
            cmbColonia.DataTextField = "NB_COLONIA";
            cmbColonia.DataValueField = "ID_COLONIA";
            cmbColonia.DataBind();

        }

        protected void cmbColonia_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox cmbColonia = sender as RadComboBox;
            ColoniaNegocio negocioColonia = new ColoniaNegocio();
            GridCatalogoLista.DataSource = negocioColonia.ObtieneColonias(pClEstado: cmbEstados.SelectedValue, pClMunicipio: cmbMunicipio.SelectedValue, pNbColonia: cmbColonia.Text.Trim());
            GridCatalogoLista.DataBind();
            
        }

        protected void btnCancelarFiltro_Click(object sender, EventArgs e)
        {
            Button btnCancelar = sender as Button;
            ColoniaNegocio negocioColonia = new ColoniaNegocio();
            GridCatalogoLista.DataSource = negocioColonia.ObtieneColonias(pClEstado: cmbEstados.SelectedValue, pClMunicipio: cmbMunicipio.SelectedValue);
            GridCatalogoLista.DataBind();

        }

    }
}