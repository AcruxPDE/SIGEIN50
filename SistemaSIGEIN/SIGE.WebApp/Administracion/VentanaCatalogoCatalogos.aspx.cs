using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Negocio.Administracion;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using Telerik.Web.UI;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using SIGE.Entidades.Externas;
using SIGE.WebApp.Comunes;
using System.Net;

namespace SIGE.WebApp.Administracion
{
    public partial class PopupmodalCatalogoGenerico : System.Web.UI.Page
    {

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        public int vIdCatalogoLista
        {
            set { ViewState["vs_vIdCatalogoLista"] = value; }
            get { return (int)ViewState["vs_vIdCatalogoLista"]; }
        }

        //public string usuario = "";
        //public string programa = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                ObtenerTipoCatalogo();

                if (Request.Params["ID"] != null )
                {
                    vIdCatalogoLista = int.Parse(Request.Params["ID"]);

                    if (vIdCatalogoLista != 0) {
                        SPE_OBTIENE_C_CATALOGO_LISTA_Result catalogo = new SPE_OBTIENE_C_CATALOGO_LISTA_Result();

                        catalogo = getLista(Request.Params["ID"]);

                        txtNbCatalogo.Text = catalogo.NB_CATALOGO_LISTA;
                        txtDsCatalogo.Text = catalogo.DS_CATALOGO_LISTA;
                        cmbIdCatalogo.SelectedValue = catalogo.ID_CATALOGO_TIPO.ToString();
                    }
                }
            }
        }

        protected void ObtenerTipoCatalogo() { 
          CatalogoTipoNegocio negocio = new CatalogoTipoNegocio();

          cmbIdCatalogo.DataSource = negocio.Obtener_S_CATALOGO_TIPO();
          cmbIdCatalogo.DataTextField = "NB_CATALOGO_TIPO";
          cmbIdCatalogo.DataValueField = "ID_CATALOGO_TIPO";
          cmbIdCatalogo.DataBind();
        }

        protected void btnGuardarCatalogo_Click(object sender, EventArgs e)
        {
            SPE_OBTIENE_C_CATALOGO_LISTA_Result catalogo = new SPE_OBTIENE_C_CATALOGO_LISTA_Result();
            

            string tipoTransaccion = "I";

            if (vIdCatalogoLista != null && vIdCatalogoLista != 0)
            {
                tipoTransaccion = "A";
                catalogo = getLista(vIdCatalogoLista.ToString());
            }

            catalogo.NB_CATALOGO_LISTA = txtNbCatalogo.Text;
            catalogo.DS_CATALOGO_LISTA = txtDsCatalogo.Text;
            catalogo.ID_CATALOGO_TIPO = int.Parse(cmbIdCatalogo.SelectedValue);

            CatalogoListaNegocio operaciones = new CatalogoListaNegocio();
            E_RESULTADO vResultado = operaciones.InsertaActualiza_C_CATALOGO_LISTA(tipoTransaccion, catalogo,vClUsuario, vNbPrograma);

               string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
               // UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);


                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
            
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
                        ,NB_CATALOGO_LISTA = o.NB_CATALOGO_LISTA
                        ,DS_CATALOGO_LISTA = o.DS_CATALOGO_LISTA
                        ,ID_CATALOGO_TIPO = o.ID_CATALOGO_TIPO
                        ,NB_CATALOGO_TIPO = o.NB_CATALOGO_TIPO
                        ,FG_SISTEMA = o.FG_SISTEMA
                    };

            return q.FirstOrDefault();
        }

    }
}