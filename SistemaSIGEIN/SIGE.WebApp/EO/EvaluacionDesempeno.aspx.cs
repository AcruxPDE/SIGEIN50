using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Negocio.EvaluacionOrganizacional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using SIGE.Entidades.Externas;
using SIGE.Negocio.FormacionDesarrollo;

using SIGE.WebApp.Comunes;
using SIGE.Entidades;
using Newtonsoft.Json;
using System.Xml.Linq;
using SIGE.Negocio.Utilerias;

namespace SIGE.WebApp.EO
{
    public partial class DesempenoInicio : System.Web.UI.Page
    {
        #region Variables

        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private string vClUsuario;
        private string vNbPrograma;

        public int? vIdPeriodo
        {
            get { return (int?)ViewState["vs_vIdPeriodo"]; }
            set { ViewState["vs_vIdPeriodo"] = value; }
        }

        public bool vConfigurar
        {
            get { return (bool)ViewState["vs_vConfigurar"]; }
            set { ViewState["vs_vConfigurar"] = value; }
        }

        public bool vCerrar
        {
            get { return (bool)ViewState["vs_vCerrar"]; }
            set { ViewState["vs_vCerrar"] = value; }
        }

        public bool vEnviarSolicitudes
        {
            get { return (bool)ViewState["vs_vEnviarSolicitudes"]; }
            set { ViewState["vs_vEnviarSolicitudes"] = value; }
        }

        public bool vCapturaEvaluaciones
        {
            get { return (bool)ViewState["vs_vCapturaEvaluaciones"]; }
            set { ViewState["vs_vCapturaEvaluaciones"] = value; }
        }

        public bool vReplicar
        {
            get { return (bool)ViewState["vs_vReplicar"]; }
            set { ViewState["vs_vReplicar"] = value; }
        }

        public bool vEditar
        {
            get { return (bool)ViewState["vs_vEditar"]; }
            set { ViewState["vs_vEditar"] = value; }
        }

        public bool vEliminar
        {
            get { return (bool)ViewState["vs_vEliminar"]; }
            set { ViewState["vs_vEliminar"] = value; }
        }


        #endregion

        #region Funciones

        private void EstatusBotonesPeriodos(bool pFgEstatus, bool? pFgConfiguracion)
        {
            bool vFgConfigurado = false;
            if (pFgConfiguracion == true)
                vFgConfigurado = true;

            btnConfiguracion.Enabled = pFgEstatus && vConfigurar;
            btnCerrar.Enabled = pFgEstatus && vCerrar;
            btnEnviarSolicitudes.Enabled = pFgEstatus && vEnviarSolicitudes && vFgConfigurado;
            btnCapturaEvaluaciones.Enabled = pFgEstatus && vCapturaEvaluaciones && vFgConfigurado;
            btnControlAvance.Enabled = vFgConfigurado;
            btnReplicar.Enabled = vReplicar;
        }

        private void CargarDatosDetalle(int? pIdPeriodo)
        {
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            SPE_OBTIENE_EO_PERIODOS_DESEMPENO_Result vPeriodo = nPeriodo.ObtienePeriodosDesempeno(pIdPeriodo: pIdPeriodo).FirstOrDefault();
            if (vPeriodo != null)
            {
                txtClPeriodo.Text = vPeriodo.CL_PERIODO;
                txtDsPeriodo.Text = vPeriodo.DS_PERIODO;
                txtClEstatus.Text = vPeriodo.CL_ESTADO_PERIODO;

                if(vPeriodo.CL_TIPO_METAS == "DESCRIPTIVO")
                txtTipo.Text = "A partir del descriptivo";
                else if(vPeriodo.CL_TIPO_METAS == "CERO")
                    txtTipo.Text = "A partir de cero";


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
                PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                var vFgConfigurado = nPeriodo.VerificaConfiguracion(int.Parse(vIdPeriodoSeleccionado)).FirstOrDefault();
                EstatusBotonesPeriodos(true, vFgConfigurado.FG_ESTATUS);

                CargarDatosDetalle(int.Parse(vIdPeriodoSeleccionado));
            }
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

        protected void SeguridadProcesos()
        {
            rdAgregar.Enabled = ContextoUsuario.oUsuario.TienePermiso("M.A.A.A");
            vEditar = ContextoUsuario.oUsuario.TienePermiso("M.A.A.B");
            vEliminar = ContextoUsuario.oUsuario.TienePermiso("M.A.A.C");
            btnConfiguracion.Enabled = vConfigurar = ContextoUsuario.oUsuario.TienePermiso("M.A.A.D");
            btnCerrar.Enabled  = vCerrar= ContextoUsuario.oUsuario.TienePermiso("M.A.A.E");
            btnReabrir.Enabled = ContextoUsuario.oUsuario.TienePermiso("M.A.A.F");
            btnCopiar.Enabled = ContextoUsuario.oUsuario.TienePermiso("M.A.A.G");
            btnReplicar.Enabled = vReplicar = ContextoUsuario.oUsuario.TienePermiso("M.A.A.H");
            btnEnviarSolicitudes.Enabled = vEnviarSolicitudes = ContextoUsuario.oUsuario.TienePermiso("M.A.A.I");
            btnCapturaEvaluaciones.Enabled = vCapturaEvaluaciones = ContextoUsuario.oUsuario.TienePermiso("M.A.A.J");
            btnControlAvance.Enabled = ContextoUsuario.oUsuario.TienePermiso("M.A.A.K");
            btnCumplimientoPersonal.Enabled = ContextoUsuario.oUsuario.TienePermiso("M.A.A.L");
            btnCumplimientoGlobal.Enabled = ContextoUsuario.oUsuario.TienePermiso("M.A.A.M");
            btnBono.Enabled = ContextoUsuario.oUsuario.TienePermiso("M.A.A.N");
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
                SeguridadProcesos();
        }

        protected void rlvPeriodos_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
        {
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            rlvPeriodos.DataSource = nPeriodo.ObtienePeriodosDesempeno();
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

                CargarDatosDetalle(vIdPeriodo);

                PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                var vFgConfigurado = nPeriodo.VerificaConfiguracion(vIdPeriodo).FirstOrDefault();

                //PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                //SPE_OBTIENE_EO_PERIODOS_DESEMPENO_Result  vPeriodo = nPeriodo.ObtienePeriodosDesempeno(pIdPeriodo: vIdPeriodo).FirstOrDefault();

                //txtClPeriodo.Text = vPeriodo.CL_PERIODO;
                //txtDsPeriodo.Text = vPeriodo.DS_PERIODO;
                //txtClEstatus.Text = vPeriodo.CL_ESTADO_PERIODO;
                //txtTipo.Text = vPeriodo.CL_ORIGEN_CUESTIONARIO;
                //txtUsuarioMod.Text = vPeriodo.CL_USUARIO_APP_MODIFICA;
                //txtFechaMod.Text = String.Format("{0:dd/MM/yyyy}", vPeriodo.FE_MODIFICA);


                //if (vPeriodo.DS_NOTAS != null)
                //{
                //    XElement vNotas = XElement.Parse(vPeriodo.DS_NOTAS);
                  
                //    if (vNotas != null)
                //    {
                //       string vNotasTexto = validarDsNotas(vNotas.ToString());
                //       txtNotas.InnerHtml = vNotasTexto;
                //    }
                //}

                //if (e.CommandName == "Select")
                //{
                    vIdPeriodo = int.Parse(item.GetDataKeyValue("ID_PERIODO").ToString());
                    string vClEstado = (item.GetDataKeyValue("CL_ESTADO_PERIODO").ToString());
                    EstatusBotonesPeriodos((vClEstado.ToUpper() == "CERRADO") ? false : true, vFgConfigurado.FG_ESTATUS);

                //}
            }

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();

            foreach (RadListViewDataItem item in rlvPeriodos.SelectedItems)
            {
                E_RESULTADO vResultado = nPeriodo.EliminaPeriodoDesempeno(int.Parse(item.GetDataKeyValue("ID_PERIODO").ToString()));
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
                rlvPeriodos.Rebind();

                if (rlvPeriodos.SelectedItems.Count > 0)
                {
                    string vIdPeriodoSeleccionado = rlvPeriodos.SelectedItems[0].GetDataKeyValue("ID_PERIODO").ToString();
                    string vFgEstado = rlvPeriodos.SelectedItems[0].GetDataKeyValue("CL_ESTADO_PERIODO").ToString();
                    if (vIdPeriodoSeleccionado != null)
                    {
                        PeriodoDesempenoNegocio nConfiguracion = new PeriodoDesempenoNegocio();
                        var vFgConfigurado = nPeriodo.VerificaConfiguracion(int.Parse(vIdPeriodoSeleccionado)).FirstOrDefault();
                        EstatusBotonesPeriodos((vFgEstado.ToUpper() == "CERRADO") ? false : true, vFgConfigurado.FG_ESTATUS);

                        CargarDatosDetalle(int.Parse(vIdPeriodoSeleccionado));
                    }
                }
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            var vMensaje = nPeriodo.ActualizaEstatusPeriodo((int)vIdPeriodo, "Cerrado", vClUsuario, vNbPrograma);
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje.MENSAJE[0].DS_MENSAJE.ToString(), vMensaje.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "onCloseWindow");

            if (vMensaje.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                PeriodoDesempenoNegocio nDesempeno = new PeriodoDesempenoNegocio();
                var vFgConfigurado = nDesempeno.VerificaConfiguracion(vIdPeriodo).FirstOrDefault();
                EstatusBotonesPeriodos(false, vFgConfigurado.FG_ESTATUS);
            }

        }

        protected void btnReactivar_Click(object sender, EventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            var vMensaje = nPeriodo.ActualizaEstatusPeriodo((int)vIdPeriodo, "Abierto", vClUsuario, vNbPrograma);
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje.MENSAJE[0].DS_MENSAJE.ToString(), vMensaje.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "onCloseWindow");

            if (vMensaje.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                PeriodoDesempenoNegocio nDesempeno = new PeriodoDesempenoNegocio();
                var vFgConfigurado = nDesempeno.VerificaConfiguracion(vIdPeriodo).FirstOrDefault();
                EstatusBotonesPeriodos(true, vFgConfigurado.FG_ESTATUS);
            }
        }

        protected void btnReplicar_Click(object sender, EventArgs e)
        {
            if (rlvPeriodos.SelectedItems.Count == 0)
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Selecciona un período", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, "");
            }
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            foreach (RadListViewDataItem item in rlvPeriodos.SelectedItems)
            {
                if (item.GetDataKeyValue("CL_ORIGEN_CUESTIONARIO") != null)
                {
                    string origenP = item.GetDataKeyValue("CL_ORIGEN_CUESTIONARIO").ToString();
                    if (origenP == "REPLICA")
                    {
                        UtilMensajes.MensajeResultadoDB(rwmMensaje, "No se puede replicar un período replicado de otro período", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, "");
                        return;
                    }
                }
                int vIdPeriodo = int.Parse(item.GetDataKeyValue("ID_PERIODO").ToString());
                var vFgConfigurado = nPeriodo.VerificaConfiguracion(vIdPeriodo).FirstOrDefault();
                if (vFgConfigurado.FG_ESTATUS == false)
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, "No se puede replicar un período con metas sin configurar.", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, "");
                    return;
                }
                    int idPeriodo = int.Parse(item.GetDataKeyValue("ID_PERIODO").ToString());
                    E_RESULTADO vResultado = nPeriodo.InsertaPeriodoDesempenoReplica(pIdPeriodo: idPeriodo, pCL_USUARIO: vClUsuario, pNB_PROGRAMA: vNbPrograma, pTipoTransaccion: "V");
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    if (vMensaje == "No hay empleados dados de baja")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "OpenReplicaPeriodoWindow(" + idPeriodo + ");", true);
                    }
                    else
                    {
                        UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
                    }
            }
        }

        protected void btnCopiar_Click(object sender, EventArgs e)
        {
            if (rlvPeriodos.SelectedItems.Count == 0)
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Selecciona un período", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, "");
            }
            foreach (RadListViewDataItem item in rlvPeriodos.SelectedItems)
            {
                PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                int vIdPeriodo = int.Parse(item.GetDataKeyValue("ID_PERIODO").ToString());
                var vFgConfigurado = nPeriodo.VerificaConfiguracion(vIdPeriodo).FirstOrDefault();
                if (vFgConfigurado.FG_ESTATUS == false)
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, "No se puede copiar un período con metas sin configurar.", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, "");
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "OpenCopiaPeriodoWindow(" + vIdPeriodo + ");", true);
                }
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
                PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                var vFgConfigurado = nPeriodo.VerificaConfiguracion(vIdPeriodo).FirstOrDefault();
                EstatusBotonesPeriodos( true, vFgConfigurado.FG_ESTATUS);
            }
            else
            {
                rlvPeriodos.Rebind();
                if (rlvPeriodos.SelectedItems.Count > 0)
                {
                    string vIdPeriodoSeleccionado = rlvPeriodos.SelectedItems[0].GetDataKeyValue("ID_PERIODO").ToString();
                    string vFgEstado = rlvPeriodos.SelectedItems[0].GetDataKeyValue("CL_ESTADO_PERIODO").ToString();
                    if (vIdPeriodoSeleccionado != null)
                    {
                        PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                        var vFgConfigurado = nPeriodo.VerificaConfiguracion(int.Parse(vIdPeriodoSeleccionado)).FirstOrDefault();
                        EstatusBotonesPeriodos((vFgEstado.ToUpper() == "CERRADO") ? false : true, vFgConfigurado.FG_ESTATUS);

                        CargarDatosDetalle(int.Parse(vIdPeriodoSeleccionado));
                    }
                }
            }
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
            if (rbAscendente.Checked)
            {
                ordenarListView(" DESC");
            }
        }

        private void ordenarListView(string ordenamiento)
        {
            var campo = cmbOrdenamiento.SelectedValue;
            rlvPeriodos.Items[0].FireCommandEvent(RadListView.SortCommandName, campo + ordenamiento);
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
    }
}