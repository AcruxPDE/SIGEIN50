using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Negocio.Administracion;
using Telerik.Web.UI;
using SIGE.Entidades.Administracion;
using SIGE.Entidades;
using System.Xml.Linq;
using SIGE.Entidades.Externas;
using SIGE.WebApp.Comunes;

namespace SIGE.WebApp.Administracion
{
    public partial class CatalogoCatalogos : System.Web.UI.Page
    {

        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        public int vIdCatalogoLista
        {
            set { ViewState["vs_vIdCatalogoLista"] = value; }
            get { return (int)ViewState["vs_vIdCatalogoLista"]; }
        }

        private string usuario = "";
        private string programa = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            usuario = ContextoUsuario.oUsuario.CL_USUARIO;
            programa = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
        }

        protected void GridCatalogoLista_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            CatalogoListaNegocio negocio = new CatalogoListaNegocio();
            grvCatalogoLista.DataSource = negocio.ObtieneCatalogoLista();
        } 

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in grvCatalogoLista.SelectedItems)
            {
                if (item != null)
                {
                    SPE_OBTIENE_C_CATALOGO_LISTA_Result lista = new SPE_OBTIENE_C_CATALOGO_LISTA_Result();

                    lista = getLista(item.GetDataKeyValue("ID_CATALOGO_LISTA").ToString());

                    CatalogoListaNegocio negocio = new CatalogoListaNegocio();

                    E_RESULTADO vResultado = negocio.Elimina_C_CATALOGO_LISTA(lista.ID_CATALOGO_LISTA, usuario, programa);

                  
                    //   = nRol.InsertaActualizaRoles(vClOperacion, vRol, vFunciones, vClUsuario, vNbPrograma);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindow");


                }
            }
        }

        protected void btnElemento_Click(object sender, EventArgs e)
        {
            if (grvCatalogoLista.SelectedItems.Count > 0)
            {
                foreach (GridDataItem item in grvCatalogoLista.SelectedItems)
                {
                    if (item != null)
                    {
                        SPE_OBTIENE_C_CATALOGO_LISTA_Result lista = new SPE_OBTIENE_C_CATALOGO_LISTA_Result();

                        lista = getLista(item.GetDataKeyValue("ID_CATALOGO_LISTA").ToString());

                        vIdCatalogoLista = lista.ID_CATALOGO_LISTA;
                        Server.Transfer("ValoresGenericos.aspx", true);
                    }
                }
            }
            else {
                RadWindowManager1.RadAlert("Seleccione un registro.", 350, 148, "Incidencias recurrentes", null);
            }
            
        }

        protected SPE_OBTIENE_C_CATALOGO_LISTA_Result getLista(string catalogoLista)
        {
            CatalogoListaNegocio negocio = new CatalogoListaNegocio();


            List<SPE_OBTIENE_C_CATALOGO_LISTA_Result> lista = negocio.ObtieneCatalogoLista();

            var q = from o in lista
                    where o.ID_CATALOGO_LISTA == int.Parse(catalogoLista)
                    select new SPE_OBTIENE_C_CATALOGO_LISTA_Result
                    {
                          ID_CATALOGO_LISTA = o.ID_CATALOGO_LISTA
                        , NB_CATALOGO_LISTA = o.NB_CATALOGO_LISTA 
                        , DS_CATALOGO_LISTA = o.DS_CATALOGO_LISTA 
                        , ID_CATALOGO_TIPO = o.ID_CATALOGO_TIPO 
                        , NB_CATALOGO_TIPO = o.NB_CATALOGO_TIPO 
                        , FG_SISTEMA = o.FG_SISTEMA 
                    };

            return q.FirstOrDefault();
        }

        protected void grvCatalogoLista_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grvCatalogoLista.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grvCatalogoLista.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grvCatalogoLista.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grvCatalogoLista.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grvCatalogoLista.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}