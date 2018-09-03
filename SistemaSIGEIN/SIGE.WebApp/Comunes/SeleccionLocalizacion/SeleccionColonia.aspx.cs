using SIGE.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.Comunes.SeleccionLocalizacion
{
    public partial class SeleccionColonia : System.Web.UI.Page
    {
        string vClPais
        {
            get { return (string)ViewState["vs_vClPais"]; }
            set { ViewState["vs_vClPais"] = value; }
        }
        string vClEstado
        {
            get { return (string)ViewState["vs_vClEstado"]; }
            set { ViewState["vs_vClEstado"] = value; }
        }

        string vClMunicipio
        {
            get { return (string)ViewState["vs_vClMunicipio"]; }
            set { ViewState["vs_vClMunicipio"] = value; }
        }

        string vNbEstado
        {
            get { return (string)ViewState["vs_vNbEstado"]; }
            set { ViewState["vs_vNbEstado"] = value; }
        }

        string vNbMunicipio
        {
            get { return (string)ViewState["vs_vNbMunicipio"]; }
            set { ViewState["vs_vNbMunicipio"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                vClPais = (String.IsNullOrEmpty(Request.QueryString["ClPais"])) ? "México" : Request.QueryString["ClPais"];
                vClEstado = (String.IsNullOrEmpty(Request.QueryString["ClEstado"])) ? null : Request.QueryString["ClEstado"];
                vNbEstado = (String.IsNullOrEmpty(Request.QueryString["NbEstado"])) ? null : Request.QueryString["NbEstado"];
                vClMunicipio = (String.IsNullOrEmpty(Request.QueryString["ClMunicipio"])) ? null : Request.QueryString["ClMunicipio"];
                vNbMunicipio = (String.IsNullOrEmpty(Request.QueryString["NbMunicipio"])) ? null : Request.QueryString["NbMunicipio"];
            }
        }

        protected void grdColonias_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ColoniaNegocio nColonia = new ColoniaNegocio();
            grdColonias.DataSource = nColonia.ObtieneColonias(pClPais: vClPais, pClEstado: vClEstado,pNbEstado: vNbEstado, pClMunicipio: vClMunicipio, pNbMunicipio: vNbMunicipio);
        }
    }
}