using SIGE.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.Comunes.SeleccionLocalizacion
{
    public partial class SeleccionMunicipio : System.Web.UI.Page
    {
        string vClPais {
            get { return (string)ViewState["vs_vClPais"]; }
            set { ViewState["vs_vClPais"] = value; }
        }

        string vClEstado
        {
            get { return (string)ViewState["vs_vClEstado"]; }
            set { ViewState["vs_vClEstado"] = value ; }
        }

        int? vIdEstado
        {
            get { return (int?)ViewState["vs_vIdEstado"]; }
            set { ViewState["vs_vIdEstado"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                vClPais = (String.IsNullOrEmpty(Request.QueryString["ClPais"])) ? "México" : Request.QueryString["ClPais"];
                vClEstado = (String.IsNullOrEmpty(Request.QueryString["ClEstado"])) ? null : Request.QueryString["ClEstado"];
                vIdEstado = (String.IsNullOrEmpty(Request.QueryString["IdEstado"])) ? (int?)null : int.Parse(Request.QueryString["IdEstado"]);
            }
        }

        protected void grdMunicipios_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            MunicipioNegocio nMunicipio = new MunicipioNegocio();
            grdMunicipios.DataSource = nMunicipio.ObtieneMunicipios(pClPais: vClPais, pClEstado: vClEstado);
        }
    }
}