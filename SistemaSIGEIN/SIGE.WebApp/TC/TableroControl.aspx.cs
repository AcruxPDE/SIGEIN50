using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.Negocio.TableroControl;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.TC
{
    public partial class TableroControl : System.Web.UI.Page
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

        #endregion

        #region Funciones

        private void EstatusBotonesPeriodos(bool pFgEstatus)
        {
            btnConfigurar.Enabled = pFgEstatus;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            Page.Title = "Tablero de control";
            if (!IsPostBack)
            {

            }
        }

        protected void rlvPeriodos_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
        {
            TableroControlNegocio nTableroControl = new TableroControlNegocio();
            rlvPeriodos.DataSource = nTableroControl.ObtenerPeriodoTableroControl();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            TableroControlNegocio nTablero = new TableroControlNegocio();
            foreach (RadListViewDataItem item in rlvPeriodos.SelectedItems)
            {
                int vIdPeriodo = int.Parse(item.GetDataKeyValue("ID_PERIODO").ToString());
                E_RESULTADO vResulatdo = nTablero.EliminaPeriodoTableroControl(vIdPeriodo);
                string vMensaje = vResulatdo.MENSAJE.Where(x => x.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResulatdo.CL_TIPO_ERROR, 400, 150, "");
                txtClPeriodo.Text = "";
                txtDsPeriodo.Text = "";
                txtClEstatus.Text = "";
                //txtTipo.Text = "";
                txtUsuarioMod.Text = "";
                txtFechaMod.Text = "";
            }
        }

        protected void btnCopiar_Click(object sender, EventArgs e)
        {
            TableroControlNegocio nTablero = new TableroControlNegocio();
            foreach (RadListViewDataItem item in rlvPeriodos.SelectedItems)
            {
                int? vIdPeriodo = int.Parse(item.GetDataKeyValue("ID_PERIODO").ToString());
                E_RESULTADO vResulatdo = nTablero.CopiaConsultatablero(vIdPeriodo, vClUsuario, vNbPrograma);
                string vMensaje = vResulatdo.MENSAJE.Where(x => x.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResulatdo.CL_TIPO_ERROR, 400, 150, "");
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            PeriodoNegocio neg = new PeriodoNegocio();


            var vMensaje = neg.ActualizaEstatusPeriodo(vIdPeriodo.Value, "Cerrado", vClUsuario, vNbPrograma);
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje.MENSAJE[0].DS_MENSAJE.ToString(), vMensaje.CL_TIPO_ERROR, 400, 150, "");
            rlvPeriodos.Rebind();
          //  EstatusBotonesPeriodos(false);
        }

        protected void rlvPeriodos_ItemCommand(object sender, RadListViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                RadListViewDataItem item = e.ListViewItem as RadListViewDataItem;
                rlvPeriodos.SelectedItems.Clear();

                int vIdPeriodoLista = 0;
                if (int.TryParse(item.GetDataKeyValue("ID_PERIODO").ToString(), out vIdPeriodoLista))
                    vIdPeriodo = vIdPeriodoLista;

                TableroControlNegocio nTableroControl = new TableroControlNegocio();
                SPE_OBTIENE_PERIODO_TABLERO_CONTROL_Result vPeriodo = nTableroControl.ObtenerPeriodoTableroControl(pIdPeriodo:vIdPeriodo).FirstOrDefault();

                txtClPeriodo.Text = vPeriodo.CL_PERIODO;
                txtDsPeriodo.Text = vPeriodo.DS_PERIODO;
                txtClEstatus.Text = vPeriodo.CL_ESTADO_PERIODO;
                //txtTipo.Text = vPeriodo.CL_TIPO_PUESTO;
                txtUsuarioMod.Text = vPeriodo.CL_USUARIO_APP_MODIFICA;
                txtFechaMod.Text = String.Format("{0:dd/MM/yyyy}", vPeriodo.FE_MODIFICA);
            }
        }
    }
}