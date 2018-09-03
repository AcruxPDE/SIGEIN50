using WebApp.Comunes;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.IntegracionDePersonal;
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
    public partial class CatalogoSolicitudes : System.Web.UI.Page
    {
        private string vClUsuario = "jdiaz";
        private string vNbPrograma = "Departamentos";
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int vID_SOLICITUD
        {
            get { return (int)ViewState["vsID_SOLICITUD"]; }
            set { ViewState["vsID_SOLICITUD"] = value; }
        }

        private List<SPE_OBTIENE_K_SOLICITUD_Result> Solicitudes;

        protected void Page_Load(object sender, EventArgs e)
        {
            Solicitudes = new List<SPE_OBTIENE_K_SOLICITUD_Result>();
            if (!IsPostBack)
            {

            }
            SolicitudNegocio nSolicitud = new SolicitudNegocio();
            Solicitudes = nSolicitud.ObtieneSolicitudes();
        }

        #region grdSolicitudes_NeedDataSource
        protected void grdSolicitudes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdSolicitudes.DataSource = Solicitudes;

        }

        #endregion

        #region grdSolicitudes_ItemDataBound
        protected void grdSolicitudes_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
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
            SolicitudNegocio nSolicitud = new SolicitudNegocio();
            foreach (GridDataItem item in grdSolicitudes.SelectedItems)
            {
                vID_SOLICITUD = (int.Parse(item.GetDataKeyValue("ID_SOLICITUD").ToString()));
                var vSolicitud= nSolicitud.ObtieneSolicitudes(ID_SOLICITUD: vID_SOLICITUD).FirstOrDefault();
                E_RESULTADO vResultado = nSolicitud.Elimina_K_SOLICITUD(ID_SOLICITUD: vID_SOLICITUD, programa: "CatalogoSolicitudes.aspx", usuario: "felipazo");
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindow");
            }
        }


        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administracion/NuevaSolicitud.aspx");
        }


        protected void btnEditar_Click(object sender, EventArgs e)
        {
           
            foreach (GridDataItem item in grdSolicitudes.SelectedItems)
            {
                vID_SOLICITUD = (int.Parse(item.GetDataKeyValue("ID_SOLICITUD").ToString()));
                Response.Redirect("~/Administracion/NuevaSolicitud.aspx?&ID=" + vID_SOLICITUD);
            }
        }


        
    }
}