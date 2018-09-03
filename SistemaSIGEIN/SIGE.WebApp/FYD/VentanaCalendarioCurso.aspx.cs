using SIGE.Entidades.Externas;
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
    public partial class VentanaCalendarioCurso : System.Web.UI.Page
    {
        
        #region Variables

        private int vIdEvento
        {
            get { return (int)ViewState["vs_vcc_id_evento"]; }
            set { ViewState["vs_vcc_id_evento"] = value; }
        }

        #endregion

        #region Funciones

        private void cargarResources()
        {
            rsCusro.ResourceTypes.Clear();

            EventoCapacitacionNegocio neg = new EventoCapacitacionNegocio();

            List<E_EVENTO> evento = neg.ObtieneEventos(ID_EVENTO: vIdEvento);
            //List<E_EVENTO_CALENDARIO> calendario = neg.obtieneEventoCalendario(ID_EVENTO: vIdEvento);
            
            //rsCusro.ResourceTypes.Add(rt);
            rsCusro.DataSource = evento;
            rsCusro.SelectedDate = evento.FirstOrDefault().FE_INICIO;
            rsCusro.Rebind();
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["IdEvento"] != null)
                {
                    vIdEvento = int.Parse(Request.Params["Idevento"].ToString());
                    cargarResources();
                }
            }
        }
    }
}