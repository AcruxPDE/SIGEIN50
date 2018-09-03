using Newtonsoft.Json;
using SIGE.Entidades;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
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

        private bool vAgregarPeriodo
        {
            get { return (bool)ViewState["vs_vAgregarPeriodo"]; }
            set { ViewState["vs_vAgregarPeriodo"] = value; }
        }

        private int vCuestionarios
        {
            get { return (int)ViewState["vs_vCuestionarios"]; }
            set { ViewState["vs_vCuestionarios"] = value; }
        }

        public bool vEditarPeriodo
        {
            get { return (bool)ViewState["vs_vEditarPeriodo"]; }
            set { ViewState["vs_vEditarPeriodo"] = value; }
        }

        public bool vEliminarPeriodo
        {
            get { return (bool)ViewState["vs_vEliminarPeriodo"]; }
            set { ViewState["vs_vEliminarPeriodo"] = value; }
        }

        private bool vConfigurarPeriodo
        {
            get { return (bool)ViewState["vs_vConfigurarPeriodo"]; }
            set {ViewState["vs_vConfigurarPeriodo"] = value; }
        }

        private bool vCerrarPeriodo
        {
            get { return (bool)ViewState["vs_vCerrarPeriodo"]; }
            set { ViewState["vs_vCerrarPeriodo"] = value; }
        }

        private bool vReabrirperiodo
        {
            get { return (bool)ViewState["vs_vReabrirperiodo"]; }
            set { ViewState["vs_vReabrirperiodo"] = value; }
        }

        private bool vCopiarPeriodo
        {
            get { return (bool)ViewState["vs_vCopiarPeriodo"]; }
            set { ViewState["vs_vCopiarPeriodo"] = value; }
        }

        private bool vEnviarSolicitudes
        {
            get { return (bool)ViewState["vs_vEnviarSolicitudes"]; }
            set { ViewState["vs_vEnviarSolicitudes"] = value; }
        }

        private bool vContestar
        {
            get { return (bool)ViewState["vs_vContestar"]; }
            set { ViewState["vs_vContestar"] = value; }
        }

        private bool vControlAvance
        {
            get { return (bool)ViewState["vs_vControlAvance"]; }
            set { ViewState["vs_vControlAvance"] = value; }
        }

        private bool vConsultaIndividual
        {
            get { return (bool)ViewState["vs_vConsultaIndividual"]; }
            set { ViewState["vs_vConsultaIndividual"] = value; }
        }

        private bool vConsultaGeneral
        {
            get { return (bool)ViewState["vs_vConsultaGeneral"]; }
            set { ViewState["vs_vConsultaGeneral"] = value; }
        }

        private bool vNecesidadCap
        {
            get { return (bool)ViewState["vs_vNecesidadCap"]; }
            set { ViewState["vs_vNecesidadCap"] = value; }
        }


        #endregion

        #region Funciones

        public void SeguridadProcesos()
        {
        btnAgregar.Enabled = vAgregarPeriodo = ContextoUsuario.oUsuario.TienePermiso("K.A.A.A");
        vEditarPeriodo= ContextoUsuario.oUsuario.TienePermiso("K.A.A.B");
        vEliminarPeriodo= ContextoUsuario.oUsuario.TienePermiso("K.A.A.C");
        btnConfigurar.Enabled = vConfigurarPeriodo = ContextoUsuario.oUsuario.TienePermiso("K.A.A.D");
        btnCerrar.Enabled = vCerrarPeriodo = ContextoUsuario.oUsuario.TienePermiso("K.A.A.E");
        btnReactivar.Enabled = vReabrirperiodo = ContextoUsuario.oUsuario.TienePermiso("K.A.A.F");
        btnCopiar.Enabled = vCopiarPeriodo = ContextoUsuario.oUsuario.TienePermiso("K.A.A.G");
        btnEnviarSolicitudes.Enabled = vEnviarSolicitudes = ContextoUsuario.oUsuario.TienePermiso("K.A.A.H");
        btnContestarCuestionarios.Enabled = vContestar = ContextoUsuario.oUsuario.TienePermiso("K.A.A.I");
        btnControlAvance.Enabled = vControlAvance = ContextoUsuario.oUsuario.TienePermiso("K.A.A.J");
        btnIndividuales.Enabled = vConsultaIndividual = ContextoUsuario.oUsuario.TienePermiso("K.A.A.K");
        btnGenerales.Enabled = vConsultaGeneral = ContextoUsuario.oUsuario.TienePermiso("K.A.A.L");
        btnNecesidadesCapacitacion.Enabled = vNecesidadCap = ContextoUsuario.oUsuario.TienePermiso("K.A.A.M");
        }

        public string validarDsNotas(string vdsNotas)
        {
            E_NOTAS pNota = null;
            if (vdsNotas != null)
            {
                XElement vNotas = XElement.Parse(vdsNotas.ToString());
                if (ValidarRamaXml(vNotas, "NOTA"))
                {
                    pNota = vNotas.Elements("NOTA").Select(el => new E_NOTAS
                    {
                        DS_NOTA = UtilXML.ValorAtributo<string>(el.Attribute("DS_NOTA")),
                        FE_NOTA = (DateTime?)UtilXML.ValorAtributo(el.Attribute("FE_NOTA"), E_TIPO_DATO.DATETIME),
                    }).FirstOrDefault();
                }

                if (pNota != null)
                {
                    if (pNota.DS_NOTA != null)
                    {
                        return pNota.DS_NOTA.ToString();
                    }
                    else return "";
                }
                else
                    return "";
            }
            else
            {
                return "";
            }
        }

        public Boolean ValidarRamaXml(XElement parentEl, string elementsName)
        {
            var foundEl = parentEl.Element(elementsName);

            if (foundEl != null)
            {
                return true;
            }
            return false;
        }

        private void EstatusBotonesPeriodos(bool pFgEstatus, bool pFgConfigurado)
        {
            btnConfigurar.Enabled = pFgEstatus && vConfigurarPeriodo;
            btnCerrar.Enabled = pFgEstatus && vCerrarPeriodo;
            btnEnviarSolicitudes.Enabled = pFgEstatus && vEnviarSolicitudes && pFgConfigurado;
            btnContestarCuestionarios.Enabled = pFgEstatus && vContestar && pFgConfigurado;
            btnControlAvance.Enabled = vControlAvance && pFgConfigurado;
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

        private void CargarDatosDetalle(int? pIdPeriodo)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            SPE_OBTIENE_FYD_PERIODOS_EVALUACION_Result vPeriodo = nPeriodo.ObtienePeriodosEvaluacion(pIdPeriodo: pIdPeriodo).FirstOrDefault();

            if (vPeriodo != null)
            {
                txtClPeriodo.Text = vPeriodo.CL_PERIODO;
                txtDsPeriodo.Text = vPeriodo.DS_PERIODO;
                txtClEstatus.Text = vPeriodo.CL_ESTADO_PERIODO;
                txtTipoEval.Text = ObtieneTiposEvaluacion(vPeriodo);
                txtUsuarioMod.Text = vPeriodo.CL_USUARIO_APP_MODIFICA;
                txtFechaMod.Text = String.Format("{0:dd/MM/yyyy}", vPeriodo.FE_MODIFICA);

                if (vPeriodo.DS_NOTAS != null)
                {
                    XElement vNotas = XElement.Parse(vPeriodo.DS_NOTAS);

                    if (vNotas != null)
                    {
                        string vNotasTexto = validarDsNotas(vNotas.ToString());
                        txtNotas.InnerHtml = vNotasTexto;
                    }
                }

                rlvPeriodos.Rebind();
            }

        }

        private void seleccionarPeriodo()
        {
            rlvPeriodos.SelectedItems.Clear();
            rlvPeriodos.SelectedIndexes.Clear();
            rlvPeriodos.CurrentPageIndex = 0;
            if (rlvPeriodos.Items.Count > 0)
            {
                rlvPeriodos.Items[0].Selected = true;
            }
            rlvPeriodos.Rebind();

            string vIdPeriodoSeleccionado = rlvPeriodos.Items[0].GetDataKeyValue("ID_PERIODO").ToString();
            if (vIdPeriodoSeleccionado != null)
            {
                CargarDatosDetalle(int.Parse(vIdPeriodoSeleccionado));

                PeriodoNegocio nPeriodo = new PeriodoNegocio();
                vCuestionarios = nPeriodo.ObtieneEvaluadosCuestionarios(int.Parse(vIdPeriodoSeleccionado), ContextoUsuario.oUsuario.ID_EMPRESA, null).Count;
                string vClEstado = (rlvPeriodos.Items[0].GetDataKeyValue("CL_ESTADO_PERIODO").ToString());
                EstatusBotonesPeriodos((vClEstado.ToUpper() == "CERRADO") ? false : true, vCuestionarios > 0 ? true : false);

            }
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
                SeguridadProcesos();
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

                CargarDatosDetalle(vIdPeriodo);

                PeriodoNegocio nPeriodo = new PeriodoNegocio();

                vCuestionarios = nPeriodo.ObtieneEvaluadosCuestionarios((int)vIdPeriodo, ContextoUsuario.oUsuario.ID_EMPRESA, null).Count;
                //PeriodoNegocio nPeriodo = new PeriodoNegocio();
                //SPE_OBTIENE_FYD_PERIODOS_EVALUACION_Result vPeriodo = nPeriodo.ObtienePeriodosEvaluacion(pIdPeriodo: vIdPeriodo).FirstOrDefault();

                //txtClPeriodo.Text = vPeriodo.CL_PERIODO;
                //txtDsPeriodo.Text = vPeriodo.DS_PERIODO;
                //txtClEstatus.Text = vPeriodo.CL_ESTADO_PERIODO;
                //txtTipoEval.Text = ObtieneTiposEvaluacion(vPeriodo);
                //txtUsuarioMod.Text = vPeriodo.CL_USUARIO_APP_MODIFICA;
                //txtFechaMod.Text = String.Format("{0:dd/MM/yyyy}", vPeriodo.FE_MODIFICA);

                //if (vPeriodo.DS_NOTAS != null)
                //{
                //    XElement vNotas = XElement.Parse(vPeriodo.DS_NOTAS);

                //    if (vNotas != null)
                //    {
                //        string vNotasTexto = validarDsNotas(vNotas.ToString());
                //        txtNotas.InnerHtml = vNotasTexto;
                //    }
                //}

                //rlvPeriodos.SelectedItemTemplate = null;

                //DESACTIVAR BOTONES
                //if (e.CommandName == RadListView.SelectCommandName)
                //{
                    //no borrar esta parte de codigo, revisar como poder aplicar estas validaíón a los botones
                    //(item.FindControl("btnModificar") as RadButton).Enabled = ContextoUsuario.oUsuario.TienePermiso("J.A.A.B");
                    //(item.FindControl("btnEliminar") as RadButton).Enabled = ContextoUsuario.oUsuario.TienePermiso("J.A.A.C");

                string vClEstado = (item.GetDataKeyValue("CL_ESTADO_PERIODO").ToString());
                EstatusBotonesPeriodos((vClEstado.ToUpper() == "CERRADO") ? false : true, vCuestionarios > 0? true:false);
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
            EstatusBotonesPeriodos(false, vCuestionarios > 0 ? true : false);
        }

        protected void btnReactivar_Click(object sender, EventArgs e)
        {
            PeriodoNegocio neg = new PeriodoNegocio();

            var vMensaje = neg.ActualizaEstatusPeriodo(vIdPeriodo.Value, "Abierto", vClUsuario, vNbPrograma);
            UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje.MENSAJE[0].DS_MENSAJE.ToString(), vMensaje.CL_TIPO_ERROR, 400, 150, "");
            rlvPeriodos.Rebind();
            EstatusBotonesPeriodos(true, vCuestionarios > 0 ? true : false);
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
                //txtClPeriodo.Text = "";
                //txtDsPeriodo.Text = "";
                //txtClEstatus.Text = "";
                //txtTipoEval.Text = "";
                //txtUsuarioMod.Text = "";
                //txtFechaMod.Text = "";
                //txtNotas.InnerHtml = "";
                //vIdPeriodo = null;                
                rlvPeriodos.Rebind();
                if (rlvPeriodos.SelectedItems.Count > 0)
                {
                    string vIdPeriodoSeleccionado = rlvPeriodos.SelectedItems[0].GetDataKeyValue("ID_PERIODO").ToString();
                    if (vIdPeriodoSeleccionado != null)
                    {
                        CargarDatosDetalle(int.Parse(vIdPeriodoSeleccionado));

                        vCuestionarios = nPeriodo.ObtieneEvaluadosCuestionarios(int.Parse(vIdPeriodoSeleccionado), ContextoUsuario.oUsuario.ID_EMPRESA, null).Count;
                        string vClEstado = (rlvPeriodos.SelectedItems[0].GetDataKeyValue("CL_ESTADO_PERIODO").ToString());
                        EstatusBotonesPeriodos((vClEstado.ToUpper() == "CERRADO") ? false : true, vCuestionarios > 0 ? true : false);
                    }
                }
                //rlvPeriodos.SelectedValues.Clear();
                //rlvPeriodos.SelectedItems.Clear();
            }
        }

        protected void ramOrganigrama_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            E_SELECTOR vSeleccion = new E_SELECTOR();
            string pParameter = e.Argument;

            if (pParameter != null)
                vSeleccion = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);

            if (vSeleccion.clTipo == "ACTUALIZARLISTA")
            {
                seleccionarPeriodo();
            }
            else if (vSeleccion.clTipo == "CONFIGURACION")
            {
                rlvPeriodos.Rebind();
                if (rlvPeriodos.SelectedItems.Count > 0)
                {
                    string vIdPeriodoSeleccionado = rlvPeriodos.SelectedItems[0].GetDataKeyValue("ID_PERIODO").ToString();          
                    if (vIdPeriodoSeleccionado != null)
                    {
                        CargarDatosDetalle(int.Parse(vIdPeriodoSeleccionado));

                        PeriodoNegocio nPeriodo = new PeriodoNegocio();
                        vCuestionarios = nPeriodo.ObtieneEvaluadosCuestionarios(int.Parse(vIdPeriodoSeleccionado), ContextoUsuario.oUsuario.ID_EMPRESA, null).Count;
                        string vClEstado = (rlvPeriodos.SelectedItems[0].GetDataKeyValue("CL_ESTADO_PERIODO").ToString());
                        EstatusBotonesPeriodos((vClEstado.ToUpper() == "CERRADO") ? false : true, vCuestionarios > 0 ? true : false);
                    }
                }
            }
            else if (vSeleccion.clTipo == "ACTUALIZAR")
            {
                rlvPeriodos.Rebind();

                if (rlvPeriodos.SelectedItems.Count > 0)
                {
                    string vIdPeriodoSeleccionado = rlvPeriodos.SelectedItems[0].GetDataKeyValue("ID_PERIODO").ToString();
                    if (vIdPeriodoSeleccionado != null)
                    {
                        CargarDatosDetalle(int.Parse(vIdPeriodoSeleccionado));

                        PeriodoNegocio nPeriodo = new PeriodoNegocio();
                        vCuestionarios = nPeriodo.ObtieneEvaluadosCuestionarios(int.Parse(vIdPeriodoSeleccionado), ContextoUsuario.oUsuario.ID_EMPRESA, null).Count;
                        string vClEstado = (rlvPeriodos.SelectedItems[0].GetDataKeyValue("CL_ESTADO_PERIODO").ToString());
                        EstatusBotonesPeriodos((vClEstado.ToUpper() == "CERRADO") ? false : true, vCuestionarios > 0 ? true : false);
                    }
                }
            }
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

        //protected void rlvPeriodos_ItemDataBound(object sender, RadListViewItemEventArgs e)
        //{
        //    if (e.Item is RadListViewDataItem)
        //    {
        //        RadListViewDataItem item = e.Item as RadListViewDataItem;
        //        int vIdPeriodoItem = int.Parse(item.GetDataKeyValue("ID_PERIODO").ToString());
        //        if (vIdPeriodoItem == vIdPeriodo)
        //        {
        //            item.Selected = true;
        //            string vClEstado = (item.GetDataKeyValue("CL_ESTADO_PERIODO").ToString());
        //            EstatusBotonesPeriodos((vClEstado.ToUpper() == "CERRADO") ? false : true);
        //        }
        //   }
        //}

    }
}
