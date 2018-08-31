using Newtonsoft.Json;
using SIGE.Entidades;
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
    public partial class EventosCapacitacion : System.Web.UI.Page
    {
        #region Propiedades
        
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private List<E_EVENTO> ListaEventos {
            get { return (List<E_EVENTO>)ViewState["vs_ec_lista_eventos"]; }
            set { ViewState["vs_ec_lista_eventos"] = value; }
        }

        private int? vIdRol;

        private int vIdEventoSeleccionado 
        {
            get { return (int)ViewState["vs_ec_id_evento_seleccionado"]; }
            set { ViewState["vs_ec_id_evento_seleccionado"] = value; }
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

        private void ObtenerListaEventos()
        {
       
        }

        private void ordenarListView(string ordenamiento)
        { 
            var campo = cmbOrdenamiento.SelectedValue;

            rlvEventos.Items[0].FireCommandEvent(RadListView.SortCommandName, campo + ordenamiento);
        }

        private void GenerarExcel()
        {
            EventoCapacitacionNegocio neg = new EventoCapacitacionNegocio();

            UDTT_ARCHIVO excel = neg.ListaAsistencia(vIdEventoSeleccionado, vIdRol);

            if (excel.FI_ARCHIVO.Length != 0)
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + excel.NB_ARCHIVO);
                Response.BinaryWrite(excel.FI_ARCHIVO);
                Response.Flush();
                Response.End();
            }
            else {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "No hay participantes en este evento", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, null);
            }
        }

        private void SeguridadProcesos()
        {
            btnAgregarEvento.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.A.B.A.A");
            vEditar = ContextoUsuario.oUsuario.TienePermiso("K.A.B.A.B");
            vEliminar = ContextoUsuario.oUsuario.TienePermiso("K.A.B.A.C");
            btnCopiar.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.A.B.A.D");
            btnEnvioCorreo.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.A.B.A.E");
            btnEnvioCorreoEvaluador.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.A.B.A.F");
            btnCapturaAsistencia.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.A.B.A.G");
            btnEvaluacionResultados.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.A.B.A.H");
            btnCalendario.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.A.B.A.I");
            btnListaAsistencia.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.A.B.A.J");
            btnReporteResultados.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.A.B.A.K");
        }

        private void CargarDatosDetalle(int? pIdPeriodo)
        {
            var evento = ListaEventos.Where(t => t.ID_EVENTO == pIdPeriodo).FirstOrDefault();

            if (evento != null)
            {
                txtNbEvento.Text = evento.CL_EVENTO;
                txtDescripcion.Text = evento.DS_EVENTO;

                
                if(evento.CL_TIPO_CURSO == "INDIVIDUAL")
                    txtTipo.Text = "Individual";
                else
                    txtTipo.Text = "Grupal";

                rtbInicio.Text = String.Format("{0:dd/MM/yyyy}", evento.FE_INICIO);
                rtbTermino.Text = String.Format("{0:dd/MM/yyyy}", evento.FE_TERMINO);

                txtCurso.Text = evento.NB_CURSO;
                txtUsuarioMod.Text = evento.CL_USUARIO_APP_MODIFICA;
                txtFechaMod.Text = String.Format("{0:dd/MM/yyyy}", evento.FE_MODIFICA);
                rlvEventos.Rebind();
            }                
        }


        private void seleccionarPeriodo()
        {
            rlvEventos.SelectedItems.Clear();
            rlvEventos.SelectedIndexes.Clear();
            rlvEventos.CurrentPageIndex = 0;
            if (rlvEventos.Items.Count > 0)
            {
                rlvEventos.Items[0].Selected = true;
            }
            rlvEventos.Rebind();

            string vIdPeriodoSeleccionado = rlvEventos.Items[0].GetDataKeyValue("ID_EVENTO").ToString();
            if (vIdPeriodoSeleccionado != null)
            {
                CargarDatosDetalle(int.Parse(vIdPeriodoSeleccionado));
            }
        }


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;

            if (!Page.IsPostBack)
            {
                vIdEventoSeleccionado = 0;
                //ObtenerListaEventos();
                SeguridadProcesos();
            }
        }

        protected void rlvEventos_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
        {
            EventoCapacitacionNegocio neg = new EventoCapacitacionNegocio();
            ListaEventos = neg.ObtieneEventos();
            rlvEventos.DataSource = ListaEventos;
        }

        protected void rlvEventos_ItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            if (e.CommandName == "Select") {
                
                RadListViewDataItem item = e.ListViewItem as RadListViewDataItem;
                vIdEventoSeleccionado = int.Parse(item.GetDataKeyValue("ID_EVENTO").ToString());
                CargarDatosDetalle(vIdEventoSeleccionado);
                //var evento = ListaEventos.Where(t => t.ID_EVENTO == vIdEventoSeleccionado).FirstOrDefault();

                //if (evento != null)
                //{
                //    txtNbEvento.Text = evento.CL_EVENTO;
                //    txtDescripcion.Text = evento.DS_EVENTO;
                //    txtEstado.Text = evento.NB_ESTADO;
                //    txtCurso.Text = evento.NB_CURSO;
                //    txtUsuarioMod.Text = evento.CL_USUARIO_APP_MODIFICA;
                //    txtFechaMod.Text = String.Format("{0:dd/MM/yyyy}", evento.FE_MODIFICA);
                //}                
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
            if (rbDescendente.Checked)
            {
                ordenarListView(" DESC");
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            EventoCapacitacionNegocio negocio = new EventoCapacitacionNegocio();

            if (vIdEventoSeleccionado > 0)
            {
                E_RESULTADO vResultado = negocio.EliminaEvento(vIdEventoSeleccionado);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, null);

                //vIdEventoSeleccionado = 0;
                //txtCurso.Text = "";
                //txtDescripcion.Text = "";
                //txtEstado.Text = "";
                //txtNbEvento.Text = "";
                rlvEventos.Rebind();
                string vIdPeriodoSeleccionado = rlvEventos.SelectedItems[0].GetDataKeyValue("ID_EVENTO").ToString();
                if (vIdPeriodoSeleccionado != null)
                {
                    CargarDatosDetalle(int.Parse(vIdPeriodoSeleccionado));
                }


            }                        
        }

        protected void btnListaAsistencia_Click(object sender, EventArgs e)
        {
            GenerarExcel();
        }

        protected void rfFiltros_ApplyExpressions(object sender, RadFilterApplyExpressionsEventArgs e)
        {
            RadFilterListViewQueryProvider provider = new RadFilterListViewQueryProvider(new List<RadFilterGroupOperation>() { RadFilterGroupOperation.And, RadFilterGroupOperation.Or });
            provider.ProcessGroup(e.ExpressionRoot);
            rlvEventos.FilterExpressions.Add(provider.ListViewExpressions[0]);
            rlvEventos.Rebind();

        }

        protected void ramEventos_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            E_SELECTOR vSeleccion = new E_SELECTOR();
            string pParameter = e.Argument;

            if (pParameter != null)
                vSeleccion = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);

            if (vSeleccion.clTipo == "ACTUALIZARLISTA")
            {
                seleccionarPeriodo();
            }
            else if (vSeleccion.clTipo == "ACTUALIZAR")
            {
                rlvEventos.Rebind();
                string vIdPeriodoSeleccionado = rlvEventos.SelectedItems[0].GetDataKeyValue("ID_EVENTO").ToString();
                if (vIdPeriodoSeleccionado != null)
                {
                    CargarDatosDetalle(int.Parse(vIdPeriodoSeleccionado));
                }
            }
        }
    }
}