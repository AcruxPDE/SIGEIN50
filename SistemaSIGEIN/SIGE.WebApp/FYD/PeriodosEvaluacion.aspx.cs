using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.WebApp.Comunes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.FYD
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

        #region Funciones

        private void EstatusBotonesPeriodos(bool pFgEstatus)
        {
            btnConfigurar.Enabled = pFgEstatus;
            btnCerrar.Enabled = pFgEstatus;
            btnEnviarSolicitudes.Enabled = pFgEstatus;
            btnContestarCuestionarios.Enabled = pFgEstatus;
        }

        private string ObtieneTiposEvaluacion(SPE_OBTIENE_FYD_PERIODOS_EVALUACION_Result pPeriodo)
        {
            string vTiposEvaluacion = "";

            if (pPeriodo.FG_AUTOEVALUACION)
            {
                vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Autoevaluación" : String.Join(", ", vTiposEvaluacion, "Autoevaluacion");
            }

            if (pPeriodo.FG_SUPERVISOR)
            {
                vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Superior" : String.Join(", ", vTiposEvaluacion, "Superior");
            }

            if (pPeriodo.FG_SUBORDINADOS)
            {
                vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Subordinado" : String.Join(", ", vTiposEvaluacion, "Subordinado");
            }

            if (pPeriodo.FG_INTERRELACIONADOS)
            {
                vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Interrelacionado" : String.Join(", ", vTiposEvaluacion, "Interrelacionado");
            }

            if (pPeriodo.FG_OTROS_EVALUADORES)
            {
                vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Otros" : String.Join(", ", vTiposEvaluacion, "Otros");
            }

            return vTiposEvaluacion;

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

        #endregion

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
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            rlvPeriodos.DataSource = nPeriodo.ObtienePeriodosEvaluacion() ;
        }

        protected void rlvPeriodos_ItemCommand(object sender, RadListViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                RadListViewDataItem item = e.ListViewItem as RadListViewDataItem;
                //rlvPeriodos.SelectedItems.Clear();
                //item.Selected = true;

                int vIdPeriodoLista = 0;
                if (int.TryParse(item.GetDataKeyValue("ID_PERIODO").ToString(), out vIdPeriodoLista))
                    vIdPeriodo = vIdPeriodoLista;


                PeriodoNegocio nPeriodo = new PeriodoNegocio();
                SPE_OBTIENE_FYD_PERIODOS_EVALUACION_Result vPeriodo = nPeriodo.ObtienePeriodosEvaluacion(pIdPeriodo: vIdPeriodo).FirstOrDefault();

                txtClPeriodo.Text = vPeriodo.CL_PERIODO;
                txtDsPeriodo.Text = vPeriodo.DS_PERIODO;
                txtClEstatus.Text = vPeriodo.CL_ESTADO_PERIODO;
                txtTipoEval.Text = ObtieneTiposEvaluacion(vPeriodo);
                txtUsuarioMod.Text = vPeriodo.CL_USUARIO_APP_MODIFICA;
                txtFechaMod.Text = String.Format("{0:dd/MM/yyyy}", vPeriodo.FE_MODIFICA);

                //rlvPeriodos.SelectedItemTemplate = null;

                //DESACTIVAR BOTONES
                //if (e.CommandName == RadListView.SelectCommandName)
                //{
                    //no borrar esta parte de codigo, revisar como poder aplicar estas validaíón a los botones
                    //(item.FindControl("btnModificar") as RadButton).Enabled = ContextoUsuario.oUsuario.TienePermiso("J.A.A.B");
                    //(item.FindControl("btnEliminar") as RadButton).Enabled = ContextoUsuario.oUsuario.TienePermiso("J.A.A.C");

                //    string vClEstado = (item.GetDataKeyValue("CL_ESTADO_PERIODO").ToString());
                //    EstatusBotonesPeriodos((vClEstado.ToUpper() == "CERRADO") ? false : true);
                //}
            }
        }

        private void ordenarListView(string ordenamiento)
        {
            var campo = cmbOrdenamiento.SelectedValue;
            rlvPeriodos.Items[0].FireCommandEvent(RadListView.SortCommandName, campo + ordenamiento);
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            PeriodoNegocio neg = new PeriodoNegocio();

            var vMensaje = neg.ActualizaEstatusPeriodo(vIdPeriodo.Value, "Cerrado", vClUsuario, vNbPrograma);
            UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje.MENSAJE[0].DS_MENSAJE.ToString(), vMensaje.CL_TIPO_ERROR, 400, 150,"");
            rlvPeriodos.Rebind();
            EstatusBotonesPeriodos(false);
        }

        protected void btnReactivar_Click(object sender, EventArgs e)
        {
            PeriodoNegocio neg = new PeriodoNegocio();

            var vMensaje = neg.ActualizaEstatusPeriodo(vIdPeriodo.Value, "Abierto", vClUsuario, vNbPrograma);
            UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje.MENSAJE[0].DS_MENSAJE.ToString(), vMensaje.CL_TIPO_ERROR, 400, 150, "");
            rlvPeriodos.Rebind();
            EstatusBotonesPeriodos(true);
        }

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

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();

            //if (vIdPeriodo != null)
            //{
            foreach (RadListViewDataItem item in rlvPeriodos.SelectedItems)
            {
                //var vMensaje = nPeriodo.EliminaPeriodoEvaluación(vIdPeriodo.Value);
                var vMensaje = nPeriodo.EliminaPeriodoEvaluación(int.Parse(item.GetDataKeyValue("ID_PERIODO").ToString()));
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje.MENSAJE[0].DS_MENSAJE.ToString(), vMensaje.CL_TIPO_ERROR, 400, 150, "");
                txtClPeriodo.Text = "";
                txtDsPeriodo.Text = "";
                txtClEstatus.Text = "";
                txtTipoEval.Text = "";
                txtUsuarioMod.Text = "";
                txtFechaMod.Text = "";
                vIdPeriodo = null;                
                rlvPeriodos.Rebind();
                //rlvPeriodos.SelectedValues.Clear();
                //rlvPeriodos.SelectedItems.Clear();
            }
        }

        protected void ramOrganigrama_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }

        protected void btnCopiar_Click(object sender, EventArgs e)
        {
            if (rlvPeriodos.SelectedItems.Count == 0)
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Seleccione un periodo", E_TIPO_RESPUESTA_DB.WARNING, 300, 150, pCallBackFunction: "");
            }
            else
            {
                PeriodoNegocio nPeriodo = new PeriodoNegocio();
                var vVerifica = nPeriodo.VerificaConfiguracion(vIdPeriodo, null, null);
                if (vVerifica.FG_ESTATUS == true)
                {
                    ClientScript.RegisterStartupScript(GetType(), "script", "AbrirCopiarPeriodo("+ vIdPeriodo+");", true);
                }
                else
                {
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, "No se puede copiar un periodo sin configurar.", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, pCallBackFunction:"");
                    return;
                }
            }
        }

        protected void rlvPeriodos_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            if (e.Item is RadListViewDataItem)
            {
                RadListViewDataItem item = e.Item as RadListViewDataItem;
                int vIdPeriodoItem = int.Parse(item.GetDataKeyValue("ID_PERIODO").ToString());
                if (vIdPeriodoItem == vIdPeriodo)
                {
                    item.Selected = true;
                    string vClEstado = (item.GetDataKeyValue("CL_ESTADO_PERIODO").ToString());
                    EstatusBotonesPeriodos((vClEstado.ToUpper() == "CERRADO") ? false : true);
                }
           }
        }

    }
}
