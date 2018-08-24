using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Entidades;
using SIGE.Negocio.Administracion;
using Telerik.Web.UI;
using System.Xml.Linq;
using SIGE.Entidades.Externas;
using SIGE.WebApp.Comunes;

namespace SIGE.WebApp.Administracion
{
    public partial class ValoresGenericos : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int vIdCatalogoLista
        {
            set { ViewState["vs_vIdCatalogoLista"] = value; }
            get { return (int)ViewState["vs_vIdCatalogoLista"]; }
        }

        public int vIdCatalogoValor
        {
            set { ViewState["vs_vIdCatalogoValor"] = value; }
            get { return (int)ViewState["vs_vIdCatalogoValor"]; }
        }

        //private string usuario = "";
        //private string programa = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack) {
                ObtenerCatalogos();
            }


            if (PreviousPage != null)
            {
                if (PreviousPage is CatalogoCatalogos)
                {

                    CatalogoCatalogos paginas = (CatalogoCatalogos)PreviousPage;
                    vIdCatalogoLista = paginas.vIdCatalogoLista;
                    cmbIdCatalogo.SelectedValue = vIdCatalogoLista.ToString();
                }

            }
            else{

                if (cmbIdCatalogo.SelectedValue != "")
                {
                    vIdCatalogoLista = int.Parse(cmbIdCatalogo.SelectedValue);
                }
                else {
                    vIdCatalogoLista = 0;
                }
                
            }
        }

        protected void ObtenerCatalogos() 
        {
            CatalogoListaNegocio negocio = new CatalogoListaNegocio();
            cmbIdCatalogo.DataSource = negocio.ObtieneCatalogoLista();
            cmbIdCatalogo.DataTextField = "NB_CATALOGO_LISTA";
            cmbIdCatalogo.DataValueField = "ID_CATALOGO_LISTA";
            cmbIdCatalogo.DataBind();
        }

        protected void GridValoresGenericos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            CatalogoValorNegocio negocio = new CatalogoValorNegocio();
            grvValoresGenericos.DataSource = negocio.Obtener_C_CATALOGO_VALOR(ID_CATALOGO_LISTA: vIdCatalogoLista);
        }

        protected void GridValoresGenericos_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grvValoresGenericos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grvValoresGenericos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grvValoresGenericos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grvValoresGenericos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grvValoresGenericos.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            } 
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in grvValoresGenericos.SelectedItems)
            {
                if (item != null)
                {
                    SPE_OBTIENE_C_CATALOGO_VALOR_Result valor = new SPE_OBTIENE_C_CATALOGO_VALOR_Result();

                    CatalogoValorNegocio negocio = new CatalogoValorNegocio();

                    valor = getValor(item.GetDataKeyValue("ID_CATALOGO_VALOR").ToString());

                    E_RESULTADO vResultado = negocio.Elimina_C_CATALOGO_VALOR(ID_CATALOGO_VALOR: valor.ID_CATALOGO_VALOR, usuario: vClUsuario, programa: vNbPrograma);

                    //E_RESULTADO vResultado = negocio.Elimina_C_CATALOGO_LISTA(lista.ID_CATALOGO_LISTA, usuario, programa);


                    //   = nRol.InsertaActualizaRoles(vClOperacion, vRol, vFunciones, vClUsuario, vNbPrograma);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindow");



                    
                }
            }
        }

        protected void cmbIdCatalogo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            CatalogoValorNegocio negocio = new CatalogoValorNegocio();
            vIdCatalogoLista = int.Parse(cmbIdCatalogo.SelectedValue);
            grvValoresGenericos.DataSource = negocio.Obtener_C_CATALOGO_VALOR(ID_CATALOGO_LISTA: vIdCatalogoLista);
            grvValoresGenericos.DataBind();
        }

        protected SPE_OBTIENE_C_CATALOGO_VALOR_Result getValor(string idValorCatalogo)
        {

            CatalogoValorNegocio negocio = new CatalogoValorNegocio();

            List<SPE_OBTIENE_C_CATALOGO_VALOR_Result> lista = negocio.Obtener_C_CATALOGO_VALOR(ID_CATALOGO_VALOR: int.Parse(idValorCatalogo));

            var q = from o in lista
                    where o.ID_CATALOGO_VALOR == int.Parse(idValorCatalogo)
                    select new SPE_OBTIENE_C_CATALOGO_VALOR_Result
                    {
                        ID_CATALOGO_LISTA = o.ID_CATALOGO_LISTA
                        ,NB_CATALOGO_VALOR = o.NB_CATALOGO_VALOR
                        ,DS_CATALOGO_VALOR = o.DS_CATALOGO_VALOR
                        ,ID_CATALOGO_VALOR = o.ID_CATALOGO_VALOR
                        ,CL_CATALOGO_VALOR = o.CL_CATALOGO_VALOR
                    };

            return q.FirstOrDefault();

        }

    }
}