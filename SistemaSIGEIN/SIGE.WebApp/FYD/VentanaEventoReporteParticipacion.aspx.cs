using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.FYD
{
    public partial class VentanaEventoReporteParticipacion : System.Web.UI.Page
    {

        #region Variables

        private int vIdEvento
        {
            get { return (int)ViewState["vs_verp_id_evento"]; }
            set { ViewState["vs_verp_id_evento"] = value; }
        }

        private int? vIdRol;

        private List<E_EVENTO_PARTICIPANTE_COMPETENCIA> vLstCompetencias {
            get { return (List<E_EVENTO_PARTICIPANTE_COMPETENCIA>)ViewState["vs_verp_lst_competencias"]; }
            set { ViewState["vs_verp_lst_competencias"] = value; }
        }

        #endregion

        #region Funciones

        private void CargarDatos()
        {
            EventoCapacitacionNegocio nEventoCapacitacion = new EventoCapacitacionNegocio();
            E_EVENTO oEvento = nEventoCapacitacion.ObtieneEventos(ID_EVENTO: vIdEvento).FirstOrDefault();

            if (oEvento != null)
            {
                txtCurso.InnerText = oEvento.NB_CURSO;
                txtEvento.InnerText = oEvento.NB_EVENTO;
                vLstCompetencias = nEventoCapacitacion.ObtieneReporteResultadosEventoDetalle(vIdEvento);
            }

        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;
            if (!Page.IsPostBack)
            {
                if (Request.Params["IdEvento"] != null)
                {
                    vIdEvento = int.Parse(Request.Params["IdEvento"].ToString());
                    CargarDatos();
                }    
            }
        }

        protected void rgReporte_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            EventoCapacitacionNegocio nEventoCapacitacion = new EventoCapacitacionNegocio();
            rgReporte.DataSource = nEventoCapacitacion.ObtieneReporteResultadosEvento(vIdEvento, vIdRol);
        }

        protected void rgReporte_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                RadGrid grd = (item["LST_COMPETENCIAS"].FindControl("grdCompetencias") as RadGrid);
                grd.DataSource = vLstCompetencias.Where(w => w.ID_PARTICIPANTE == int.Parse(item.GetDataKeyValue("ID_PARTICIPANTE").ToString()));
                grd.DataBind();
            }
        }
    }
}