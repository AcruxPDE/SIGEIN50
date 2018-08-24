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

        #endregion

        #region Funciones

        private void EstatusBotonesPeriodos(bool pFgEstatus)
        {
            btnConfiguracion.Enabled = pFgEstatus;
            btnCerrar.Enabled = pFgEstatus;
            btnEnviarSolicitudes.Enabled = pFgEstatus;
            btnCapturaEvaluaciones.Enabled = pFgEstatus;
            btnReplicar.Enabled = pFgEstatus;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
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

                PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                SPE_OBTIENE_EO_PERIODOS_DESEMPENO_Result  vPeriodo = nPeriodo.ObtienePeriodosDesempeno(pIdPeriodo: vIdPeriodo).FirstOrDefault();

                txtClPeriodo.Text = vPeriodo.CL_PERIODO;
                txtDsPeriodo.Text = vPeriodo.DS_PERIODO;
                txtClEstatus.Text = vPeriodo.CL_ESTADO_PERIODO;
                txtTipo.Text = vPeriodo.CL_ORIGEN_CUESTIONARIO;
                txtUsuarioMod.Text = vPeriodo.CL_USUARIO_APP_MODIFICA;
                txtFechaMod.Text = String.Format("{0:dd/MM/yyyy}", vPeriodo.FE_MODIFICA);
                //if (e.CommandName == "Select")
                //{
                    vIdPeriodo = int.Parse(item.GetDataKeyValue("ID_PERIODO").ToString());
                    string vClEstado = (item.GetDataKeyValue("CL_ESTADO_PERIODO").ToString());
                    EstatusBotonesPeriodos((vClEstado.ToUpper() == "CERRADO") ? false : true);

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
                txtClPeriodo.Text = "";
                txtDsPeriodo.Text = "";
                txtClEstatus.Text = "";
                txtTipo.Text = "";
                txtUsuarioMod.Text = "";
                txtFechaMod.Text = "";
                vIdPeriodo = null;
                rlvPeriodos.Rebind();
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            var vMensaje = nPeriodo.ActualizaEstatusPeriodo((int)vIdPeriodo, "Cerrado", vClUsuario, vNbPrograma);
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje.MENSAJE[0].DS_MENSAJE.ToString(), vMensaje.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "onCloseWindow");
            EstatusBotonesPeriodos(false);

        }

        protected void btnReactivar_Click(object sender, EventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            var vMensaje = nPeriodo.ActualizaEstatusPeriodo((int)vIdPeriodo, "Abierto", vClUsuario, vNbPrograma);
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje.MENSAJE[0].DS_MENSAJE.ToString(), vMensaje.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "onCloseWindow");
            EstatusBotonesPeriodos(true);
        }

        protected void btnReplicar_Click(object sender, EventArgs e)
        {
            if (rlvPeriodos.SelectedItems.Count == 0)
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Selecciona un periodo", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, "");
            }
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            foreach (RadListViewDataItem item in rlvPeriodos.SelectedItems)
            {
                if (item.GetDataKeyValue("CL_ORIGEN_CUESTIONARIO") != null)
                {
                    string origenP = item.GetDataKeyValue("CL_ORIGEN_CUESTIONARIO").ToString();
                    if (origenP == "REPLICA")
                    {
                        UtilMensajes.MensajeResultadoDB(rwmMensaje, "No se puede replicar un periodo replicado de otro periodo", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, "");
                        return;
                    }
                }
                int vIdPeriodo = int.Parse(item.GetDataKeyValue("ID_PERIODO").ToString());
                var vFgConfigurado = nPeriodo.VerificaConfiguracion(vIdPeriodo).FirstOrDefault();
                if (vFgConfigurado.FG_ESTATUS == false)
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, "No se puede replicar un periodo con metas sin configurar.", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, "");
                    return;
                }
                    int idPeriodo = int.Parse(item.GetDataKeyValue("ID_PERIODO").ToString());
                    E_RESULTADO vResultado = nPeriodo.InsertaPeriodoDesempenoReplica(pIdPeriodo: idPeriodo, pCL_USUARIO: vClUsuario, pNB_PROGRAMA: vNbPrograma, pTipoTransaccion: "V");
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    if (vMensaje == "No hay empleados dados de baja")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "script", "OpenReplicaPeriodoWindow(" + idPeriodo + ");", true);
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
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Selecciona un periodo", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, "");
            }
            foreach (RadListViewDataItem item in rlvPeriodos.SelectedItems)
            {
                PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                int vIdPeriodo = int.Parse(item.GetDataKeyValue("ID_PERIODO").ToString());
                var vFgConfigurado = nPeriodo.VerificaConfiguracion(vIdPeriodo).FirstOrDefault();
                if (vFgConfigurado.FG_ESTATUS == false)
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, "No se puede copiar un periodo con metas sin configurar.", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, "");
                    return;
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "script", "OpenCopiaPeriodoWindow(" + vIdPeriodo + ");", true);
                }
            }

        }
    }
}