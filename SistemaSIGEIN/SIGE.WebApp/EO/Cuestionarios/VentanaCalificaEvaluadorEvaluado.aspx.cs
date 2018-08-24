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
using WebApp.Comunes;

namespace SIGE.WebApp.EO
{
    public partial class VentanaCalificaEvaluadorEvaluado : System.Web.UI.Page
    {
        private string vClUsuario;
        public string cssModulo = String.Empty;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;


        public int vIdPeriodo
        {
            get { return (int)ViewState["vsIdPeriodo"]; }
            set { ViewState["vsIdPeriodo"] = value; }
        }


        public Guid clToken
        {
            get { return (Guid)ViewState["vsClToken"]; }
            set { ViewState["vsClToken"] = value; }
        }


        public int vIdEvaluador
        {
            get { return (int)ViewState["vsIdEvaluador"]; }
            set { ViewState["vsIdEvaluador"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = (ContextoUsuario.oUsuario != null) ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO";
            vNbPrograma = ContextoUsuario.nbPrograma;

            string vClModulo = "EVALUACION";
            string vModulo = Request.QueryString["m"];
            if (vModulo != null)
                vClModulo = vModulo;

            cssModulo = Utileria.ObtenerCssModulo(vClModulo);

            if (!IsPostBack)
            {
                if (Request.Params["ID_PERIODO"] != null)
                {
                    vIdPeriodo = int.Parse(Request.QueryString["ID_PERIODO"]);
                }
                else
                {
                    vIdPeriodo = 0;
                }
                PeriodoDesempenoNegocio periodo = new PeriodoDesempenoNegocio();
                var oPeriodo = periodo.ObtienePeriodoDesempeno(pIdPeriodo: vIdPeriodo);

                if (oPeriodo != null)
                {
                    txtNoPeriodo.InnerText = oPeriodo.CL_PERIODO;
                    txtNbPeriodo.InnerText = oPeriodo.DS_PERIODO;
                }

                if (Request.Params["IdEvaluador"] != null && Request.Params["TOKEN"] != null)
                {
                    vIdEvaluador= int.Parse(Request.QueryString["IdEvaluador"]);
                    clToken = Guid.Parse(Request.QueryString["TOKEN"]);
                    var evaluador = periodo.ObtenerPeriodoEvaluadorDesempeno(vIdEvaluador,clToken);

                    lblEvaluador.InnerText = evaluador.NB_EVALUADOR;
                }
            }
        }

        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Logon.aspx");
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {

        }

        protected void grdEvaluados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            grdEvaluados.DataSource = nPeriodo.ObtieneEvaluados(pIdPeriodo: vIdPeriodo,pIdEvaluador:vIdEvaluador);
        }

    }
}