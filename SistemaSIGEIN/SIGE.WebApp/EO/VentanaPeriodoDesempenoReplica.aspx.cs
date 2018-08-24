using SIGE.Entidades.Externas;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.EO
{
    public partial class VentanaPeriodoDesempenoReplica : System.Web.UI.Page
    {

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();

        public int vIdPeriodo
        {
            get { return (int)ViewState["vs_ps_id_periodo"]; }
            set { ViewState["vs_ps_id_periodo"] = value; }
        }

        private string vClTipo
        {
            get { return (string)ViewState["vs_ps_cl_tipo"]; }
            set { ViewState["vs_ps_cl_tipo"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                if (!Request.Params["PeriodoId"].ToString().Equals("null"))
                {
                    vIdPeriodo = int.Parse(Request.Params["PeriodoId"].ToString());
                    var vPeriodoDesempeno = nPeriodo.ObtienePeriodosDesempeno(pIdPeriodo: vIdPeriodo).FirstOrDefault();
                    txtPeriodo.InnerText = vPeriodoDesempeno.CL_PERIODO;
                    txtDescripcion.InnerText = vPeriodoDesempeno.DS_PERIODO;
                    txtInicio.InnerText = vPeriodoDesempeno.FE_INICIO.ToString();
                    txtFin.InnerText = vPeriodoDesempeno.FE_TERMINO.ToString().Substring(0,10);
                }
                else
                    vIdPeriodo = 0;
            }
        }

        protected void btnReplicarPeriodo_Click(object sender, EventArgs e)
        {
            if(feInicio.SelectedDate==null || feFin.SelectedDate== null){
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "La fecha de inicio y término son obligatorias", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                return;
            }
            E_RESULTADO vResultado = nPeriodo.InsertaPeriodoDesempenoReplica(vIdPeriodo, feInicio.SelectedDate,feFin.SelectedDate, vClUsuario, vNbPrograma,"");
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR,pCallBackFunction:"");
            grdPeriodosReplica.Rebind();
            feInicio.SelectedDate = null;
            feFin.SelectedDate = null;
        }

        protected void grdPeriodosReplica_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdPeriodosReplica.DataSource = nPeriodo.ObtienePeriodosReplicados(vIdPeriodo);
        }

        protected void grdPeriodosReplica_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                int vIdPeriodo = int.Parse(item.GetDataKeyValue("ID_PERIODO").ToString());
                PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();

                if (vIdPeriodo > 0)
                {
                    E_RESULTADO vResultado = nPeriodo.EliminaPeriodoDesempeno(vIdPeriodo);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");

                    grdPeriodosReplica.Rebind();
                }
            }
        }
    }
}