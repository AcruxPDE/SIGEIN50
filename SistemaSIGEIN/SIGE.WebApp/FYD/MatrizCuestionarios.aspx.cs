using SIGE.Entidades;
using SIGE.Entidades.Administracion;
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
    public partial class MatrizCuestionarios : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int? vIdPeriodo
        {
            get { return (int?)ViewState["vs_vIdPeriodo"]; }
            set { ViewState["vs_vIdPeriodo"] = value; }
        }

        public int? vIdEvaluado
        {
            get { return (int?)ViewState["vs_vIdEvaluado"]; }
            set { ViewState["vs_vIdEvaluado"] = value; }
        }

        public List<E_EVALUADOR> vLstEvaluadores
        {
            get { return (List<E_EVALUADOR>)ViewState["vs_vLstEvaluadores"]; }
            set { ViewState["vs_vLstEvaluadores"] = value; }
        }

        #endregion

        #region Funciones

        protected void ObtenerEvaluadores()
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            vLstEvaluadores = nPeriodo.ObtieneCuestionariosEvaluado(vIdEvaluado ?? 0).Select(s => new E_EVALUADOR()
            {
                CL_ROL_EVALUADOR = s.CL_ROL_EVALUADOR,
                ID_CUESTIONARIO = s.ID_CUESTIONARIO,
                CL_EVALUADOR = s.CL_EVALUADOR,
                NB_EVALUADOR = s.NB_EVALUADOR,
                CL_PUESTO = s.CL_PUESTO,
                NB_PUESTO = s.NB_PUESTO,
                NB_ROL_EVALUADOR = s.NB_ROL_EVALUADOR,
                ID_EVALUADO_EVALUADOR = s.ID_EVALUADO_EVALUADOR
            }).ToList();
        }

        public string ObtenerToolTipEvaluador(string pClEmpleado, string pClPuesto)
        {
            string vToolTip = String.Empty;
            if (!String.IsNullOrWhiteSpace(pClEmpleado))
                vToolTip += String.Format("Clave empleado: {0}", pClEmpleado);

            if (!String.IsNullOrWhiteSpace(pClPuesto))
                vToolTip += String.Format("{1}Clave puesto: {0}", pClPuesto, !String.IsNullOrWhiteSpace(vToolTip) ? ", " : String.Empty);
            return vToolTip;
        }

        protected void EliminarEvaluador(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is GridEditableItem)
            {
                GridEditableItem item = (GridEditableItem)e.Item;

                int vIdCuestionario = int.Parse(item.GetDataKeyValue("ID_CUESTIONARIO").ToString());

                PeriodoNegocio nPeriodo = new PeriodoNegocio();
                E_RESULTADO vResultado = nPeriodo.EliminaCuestionario(vIdCuestionario);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

                if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                {
                    ObtenerEvaluadores();
                    ((RadGrid)sender).Rebind();
                }
            }
        }

        private void ObtenerEvaluado()
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();

            SPE_OBTIENE_FYD_EVALUADO_Result vEvaluado = nPeriodo.ObtieneEvaluado(pIdEvaluado: vIdEvaluado);

            if (vEvaluado != null)
            {
                txtEvaluado.Text = vEvaluado.NB_EVALUADO;
            }

        }

        private void ObtenerPeriodo()
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();

            SPE_OBTIENE_FYD_PERIODO_EVALUACION_Result oPeriodo = nPeriodo.ObtienePeriodoEvaluacion(vIdPeriodo.Value);

            if (oPeriodo != null)
            {
                btnAutoevaluacion.Enabled = oPeriodo.FG_AUTOEVALUACION;
                btnSuperior.Enabled = oPeriodo.FG_SUPERVISOR;
                btnSubordinado.Enabled = oPeriodo.FG_SUBORDINADOS;
                btnInterrelacionado.Enabled = oPeriodo.FG_INTERRELACIONADOS;
                btnOrtos.Enabled = oPeriodo.FG_OTROS_EVALUADORES;
            }

        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                int vIdPeriodoQS = 0;

                if (int.TryParse(Request.QueryString["PeriodoId"], out vIdPeriodoQS))
                    vIdPeriodo = vIdPeriodoQS;

                int vIdEvaluadoQS = 0;

                if (int.TryParse(Request.QueryString["EvaluadoId"], out vIdEvaluadoQS))
                    vIdEvaluado = vIdEvaluadoQS;

                if (vIdPeriodo != 0 && vIdEvaluado != 0)
                {
                    ObtenerEvaluadores();
                    ObtenerEvaluado();
                    ObtenerPeriodo();
                }
            }
        }

        protected void grdAutoevaluador_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdAutoevaluador.DataSource = vLstEvaluadores.Where(w => w.CL_ROL_EVALUADOR.Equals(E_CL_TIPO_EVALUADOR.AUTOEVALUACION.ToString()));
        }

        protected void grdSupervisor_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdSupervisor.DataSource = vLstEvaluadores.Where(w => w.CL_ROL_EVALUADOR.Equals(E_CL_TIPO_EVALUADOR.SUPERIOR.ToString()));
        }

        protected void grdSubordinado_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdSubordinado.DataSource = vLstEvaluadores.Where(w => w.CL_ROL_EVALUADOR.Equals(E_CL_TIPO_EVALUADOR.SUBORDINADO.ToString()));
        }

        protected void grdInterrelacionado_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdInterrelacionado.DataSource = vLstEvaluadores.Where(w => w.CL_ROL_EVALUADOR.Equals(E_CL_TIPO_EVALUADOR.INTERRELACIONADO.ToString()));
        }

        protected void grdOtrosEvaluadores_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdOtrosEvaluadores.DataSource = vLstEvaluadores.Where(w => w.CL_ROL_EVALUADOR.Equals(E_CL_TIPO_EVALUADOR.OTRO.ToString()));
        }

        protected void ramMatriz_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            ObtenerEvaluadores();
            grdAutoevaluador.Rebind();
            grdInterrelacionado.Rebind();
            grdSubordinado.Rebind();
            grdSupervisor.Rebind();
            grdOtrosEvaluadores.Rebind();
        }

        protected void grdAutoevaluador_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            EliminarEvaluador(sender, e);
        }

        protected void grdSupervisor_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            EliminarEvaluador(sender, e);
        }

        protected void grdSubordinado_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            EliminarEvaluador(sender, e);
        }

        protected void grdInterrelacionado_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            EliminarEvaluador(sender, e);
        }

        protected void grdOtrosEvaluadores_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            EliminarEvaluador(sender, e);
        }

        protected void btnAutoevaluacion_Click(object sender, EventArgs e)
        {

                PeriodoNegocio nPeriodo = new PeriodoNegocio();
                E_RESULTADO vResultado = nPeriodo.InsertaCuestionarioAutoevaluacion(vIdPeriodo.Value, vIdEvaluado.Value, true, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

                if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                {
                    ObtenerEvaluadores();
                    grdAutoevaluador.Rebind();
                }
        }

    }
}