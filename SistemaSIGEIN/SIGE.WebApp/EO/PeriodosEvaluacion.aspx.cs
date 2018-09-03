using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.WebApp.Comunes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.EO
{
    public partial class PeriodosEvaluacion : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int? vIdPeriodo
        {
            get { return (int?)ViewState["vs_vIdPeriodo"]; }
            set { ViewState["vs_vIdPeriodo"] = value; }
        }

        #region Variables para seguridad de procesos

        private bool vAgregarPeriodo;
        private bool vCerrarPeriodo;
        private bool vConfigurarPeriodo;
        private bool vControlAvancePeriodo;
        private bool vEnvioSolicitudesPeriodo;
        private bool vGeneralesPeriodo;
        private bool vIndividualesPeriodo;
        private bool vDNCPeriodo;
        private bool vReactivarPeriodo;
        private bool vContestarCuestionarioPeriodo;

        #endregion


        private void EstatusBotonesPeriodos(bool pFgEstatus)
        {
            #region Comentadas por oscar
            //HACK Comentadas por Oscar
            //btnConfigurar.Enabled = pFgEstatus;
            //btnCerrar.Enabled = pFgEstatus;
            #endregion
            btnEnviarSolicitudes.Enabled = pFgEstatus;
            btnContestarCuestionarios.Enabled = pFgEstatus;
        }

        //private void SeguridadProcesos()
        //{
        //    btnAgregar.Enabled = vAgregarPeriodo = ContextoUsuario.oUsuario.TienePermiso("J.A.A.A");
        //    btnCerrar.Enabled = vCerrarPeriodo = ContextoUsuario.oUsuario.TienePermiso("J.A.A.E");
        //    btnConfigurar.Enabled = vConfigurarPeriodo = ContextoUsuario.oUsuario.TienePermiso("J.A.A.D");
        //    btnControlAvance.Enabled = vControlAvancePeriodo = ContextoUsuario.oUsuario.TienePermiso("J.A.A.H");
        //    btnEnviarSolicitudes.Enabled = vEnvioSolicitudesPeriodo = ContextoUsuario.oUsuario.TienePermiso("J.A.A.G");
        //    btnGenerales.Enabled = vGeneralesPeriodo = ContextoUsuario.oUsuario.TienePermiso("J.A.A.K");
        //    btnIndividuales.Enabled = vIndividualesPeriodo = ContextoUsuario.oUsuario.TienePermiso("J.A.A.J");
        //    btnNecesidadesCapacitacion.Enabled = vDNCPeriodo = ContextoUsuario.oUsuario.TienePermiso("J.A.A.L");
        //    btnReactivar.Enabled = vReactivarPeriodo = ContextoUsuario.oUsuario.TienePermiso("J.A.A.F");
        //    btnContestarCuestionarios.Enabled = vContestarCuestionarioPeriodo = ContextoUsuario.oUsuario.TienePermiso("J.A.A.I");
        //}

        

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            //SeguridadProcesos();

            if (!Page.IsPostBack)
            {

            }

        }

        protected void rlvPeriodos_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
        {
            //PeriodoNegocio nPeriodo = new PeriodoNegocio();
            //rlvPeriodos.DataSource = nPeriodo.ObtienePeriodosEvaluacion() ;
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            rlvPeriodos.DataSource = nPeriodo.ObtienePeriodosDesempenoCuestionario();
        }

        protected void rlvPeriodos_ItemCommand(object sender, RadListViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                RadListViewDataItem item = e.ListViewItem as RadListViewDataItem;
                rlvPeriodos.SelectedItems.Clear();
                item.Selected = true;

                int vIdPeriodoLista = 0;
                if (int.TryParse(item.GetDataKeyValue("ID_PERIODO").ToString(), out vIdPeriodoLista))
                    vIdPeriodo = vIdPeriodoLista;

                //DESACTIVAR BOTONES
                if (e.CommandName == RadListView.SelectCommandName)
                {
                    //no borrar esta parte de codigo, revisar como poder aplicar estas validaíón a los botones
                    //(item.FindControl("btnModificar") as RadButton).Enabled = ContextoUsuario.oUsuario.TienePermiso("J.A.A.B");
                    //(item.FindControl("btnEliminar") as RadButton).Enabled = ContextoUsuario.oUsuario.TienePermiso("J.A.A.C");

                    string vClEstado = (item.GetDataKeyValue("CL_ESTADO_PERIODO").ToString());
                    EstatusBotonesPeriodos((vClEstado.ToUpper() == "CERRADO") ? false : true);
                }
            }
        }

        private void ordenarListView(string ordenamiento)
        {
            var campo = cmbOrdenamiento.SelectedValue;
            rlvPeriodos.Items[0].FireCommandEvent(RadListView.SortCommandName, campo + ordenamiento);
        }

        #region Comentadas por oscar
        //HACK Comentadas por Oscar
        //protected void btnCerrar_Click(object sender, EventArgs e)
        //{
        //    PeriodoNegocio neg = new PeriodoNegocio();

        //    var vMensaje = neg.ActualizaEstatusPeriodo(vIdPeriodo.Value, "Cerrado", vClUsuario, vNbPrograma);
        //    UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje.MENSAJE[0].DS_MENSAJE.ToString(), vMensaje.CL_TIPO_ERROR, 400, 150,"");
        //    rlvPeriodos.Rebind();
        //    EstatusBotonesPeriodos(false);
        //}
        #endregion


        #region Comentadas por oscar
        //HACK Comentadas por Oscar
        //protected void btnReactivar_Click(object sender, EventArgs e)
        //{
        //    PeriodoNegocio neg = new PeriodoNegocio();

        //    var vMensaje = neg.ActualizaEstatusPeriodo(vIdPeriodo.Value, "Abierto", vClUsuario, vNbPrograma);
        //    UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje.MENSAJE[0].DS_MENSAJE.ToString(), vMensaje.CL_TIPO_ERROR, 400, 150, "");
        //    rlvPeriodos.Rebind();
        //    EstatusBotonesPeriodos(true);
        //}
        #endregion

        protected void rbAscendente_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAscendente.Checked)
            {
                ordenarListView(" ASC");
            }
        }

        protected void rbDescendente_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDescendente.Checked)
            {
                ordenarListView(" DESC");
            }
        }

        protected void rfFiltros_ApplyExpressions(object sender, RadFilterApplyExpressionsEventArgs e)
        {

            RadFilterListViewQueryProvider provider = new RadFilterListViewQueryProvider(new List<RadFilterGroupOperation>() { RadFilterGroupOperation.And, RadFilterGroupOperation.Or });
            provider.ProcessGroup(e.ExpressionRoot);

            if (provider.ListViewExpressions.Count > 0)
            {
                rlvPeriodos.FilterExpressions.Add(provider.ListViewExpressions[0]);
            }
            else
            {
                rlvPeriodos.FilterExpressions.Clear();
            }

            rlvPeriodos.Rebind();
        }

        #region Comentadas por oscar
        //HACK Comentadas por Oscar
        //protected void btnEliminar_Click(object sender, EventArgs e)
        //{
        //    PeriodoNegocio nPeriodo = new PeriodoNegocio();

        //    if (vIdPeriodo != null)
        //    {
        //        var vMensaje = nPeriodo.EliminaPeriodoEvaluación(vIdPeriodo.Value);
        //        UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje.MENSAJE[0].DS_MENSAJE.ToString(), vMensaje.CL_TIPO_ERROR, 400, 150, "");
        //        vIdPeriodo = null;                
        //        rlvPeriodos.Rebind();
        //        rlvPeriodos.SelectedValues.Clear();
        //        rlvPeriodos.SelectedItems.Clear();
        //    }
        //}
        #endregion

        protected void ramOrganigrama_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }

        #region Comentadas por oscar
        //HACK Comentadas por Oscar
        //protected void btnCopiar_Click(object sender, EventArgs e)
        //{
        //    if (rlvPeriodos.SelectedItems.Count == 0)
        //    {
        //        UtilMensajes.MensajeResultadoDB(rwmAlertas, "Seleccione un periodo", E_TIPO_RESPUESTA_DB.WARNING, 300, 150, pCallBackFunction: "");
        //    }
        //    else
        //    {
        //        PeriodoNegocio nPeriodo = new PeriodoNegocio();
        //        var vVerifica = nPeriodo.VerificaConfiguracion(vIdPeriodo, null, null);
        //        if (vVerifica.FG_ESTATUS == true)
        //        {
        //            ClientScript.RegisterStartupScript(GetType(), "script", "AbrirCopiarPeriodo("+ vIdPeriodo+");", true);
        //        }
        //        else
        //        {
        //            UtilMensajes.MensajeResultadoDB(rwmAlertas, "No se puede copiar un periodo sin configurar.", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, pCallBackFunction:"");
        //            return;
        //        }
        //    }
        //}
        #endregion

    }
}
