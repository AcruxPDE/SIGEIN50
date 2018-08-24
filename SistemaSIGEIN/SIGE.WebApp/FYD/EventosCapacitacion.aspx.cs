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

        private int vIdEventoSeleccionado {
            get { return (int)ViewState["vs_ec_id_evento_seleccionado"]; }
            set { ViewState["vs_ec_id_evento_seleccionado"] = value; }
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

            UDTT_ARCHIVO excel = neg.ListaAsistencia(vIdEventoSeleccionado);

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

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                vIdEventoSeleccionado = 0;
                //ObtenerListaEventos();
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
                var evento = ListaEventos.Where(t => t.ID_EVENTO == vIdEventoSeleccionado).FirstOrDefault();

                if (evento != null)
                {
                    txtNbEvento.Text = evento.CL_EVENTO;
                    txtDescripcion.Text = evento.DS_EVENTO;
                    txtEstado.Text = evento.NB_ESTADO;
                    txtCurso.Text = evento.NB_CURSO;
                    txtUsuarioMod.Text = evento.CL_USUARIO_APP_MODIFICA;
                    txtFechaMod.Text = String.Format("{0:dd/MM/yyyy}", evento.FE_MODIFICA);
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

                vIdEventoSeleccionado = 0;
                txtCurso.Text = "";
                txtDescripcion.Text = "";
                txtEstado.Text = "";
                txtNbEvento.Text = "";
                rlvEventos.Rebind();
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
    }
}