using SIGE.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.Comunes.SeleccionLocalizacion
{
    public partial class SeleccionEstado : System.Web.UI.Page
    {
        string vClPais
        {
            get { return ViewState["vs_vClPais"].ToString(); }
            set { ViewState["vs_vClPais"] = value; }
        }


        public string vClCatalogo
        {
            get { return (string)ViewState["vs_vClCatalogo"]; }
            set { ViewState["vs_vClCatalogo"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                vClPais = (String.IsNullOrEmpty(Request.QueryString["ClPais"])) ? "México" : Request.QueryString["ClPais"];

                vClCatalogo = Request.QueryString["CatalogoCl"];
                if (String.IsNullOrEmpty(vClCatalogo))
                    vClCatalogo = "ESTADO";
            }
        }

        protected void grdEstados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            EstadoNegocio nEstado = new EstadoNegocio();
            grdEstados.DataSource = nEstado.ObtieneEstados(CL_PAIS: vClPais);
        }
    }
}