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
    public partial class CatalogoRequisiciones : System.Web.UI.Page
    {

        private string vClUsuario = "Admin";
        private string vNbPrograma = "VentanaCatalogoRequisiciones.aspx";
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;


        private int vID_RQUISICION
        {
            get { return (int)ViewState["vsID_REQUISICION"]; }
            set { ViewState["vsID_REQUISICION"] = value; }
        }

        private List<SPE_OBTIENE_K_REQUISICION_Result> Requisiciones;



        protected void Page_Load(object sender, EventArgs e)
        {
            Requisiciones = new List<SPE_OBTIENE_K_REQUISICION_Result>();

            if (!IsPostBack)
            {

            }

            RequisicionNegocio negocio = new RequisicionNegocio();
            Requisiciones = negocio.Obtener_K_REQUISICION();

        }

        #region grdRequisicion_NeedDataSource
        protected void grdRequisicion_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdRequisicion.DataSource = Requisiciones;

        }

        #endregion

        #region grdRequisicion_ItemDataBound
        protected void grdRequisicion_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
            RequisicionNegocio negocio = new RequisicionNegocio();
            foreach (GridDataItem item in grdRequisicion.SelectedItems)
            {
                vID_RQUISICION = (int.Parse(item.GetDataKeyValue("ID_REQUISICION").ToString()));
                var vObtenerKrequisicion = negocio.Obtener_K_REQUISICION(ID_REQUISICION: vID_RQUISICION).FirstOrDefault();
                E_RESULTADO vResultado = negocio.Elimina_K_REQUISICION(ID_REQUISICION: vID_RQUISICION, programa: "CatalogoRequisiciones.aspx", usuario: "felipe");
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindow");
            }
        }
    }
}