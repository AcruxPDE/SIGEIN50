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
using SIGE.WebApp.Comunes;
using SIGE.Negocio.FormacionDesarrollo;
using WebApp.Comunes;
using System.Xml.Linq;
using SIGE.Entidades;
using Newtonsoft.Json;



namespace SIGE.WebApp.EO
{
    public partial class ClimaLaboral : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private string vNbFirstRadEditorTagName = "p";

        public int? vIdPeriodo
        {
            get { return (int?)ViewState["vs_vIdPeriodo"]; }
            set { ViewState["vs_vIdPeriodo"] = value; }
        }

        private List<E_PERIODO_CLIMA> vListaPeriodo
        {
            get { return (List<E_PERIODO_CLIMA>)ViewState["vs_vListaPeriodo"]; }
            set { ViewState["vs_vListaPeriodo"] = value; }
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

        public bool vContestar
        {
            get { return (bool)ViewState["vs_vContestar"]; }
            set { ViewState["vs_vContestar"] = value; }
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

        private void ObtenerLista()
        {
            ClimaLaboralNegocio nPeriodo = new ClimaLaboralNegocio();
            vListaPeriodo = nPeriodo.ObtienePeriodosClima().Select(s => new E_PERIODO_CLIMA
            {
                ID_PERIODO = s.ID_PERIODO,
                NB_PERIODO = s.NB_PERIODO,
                DS_PERIODO = s.DS_PERIODO,
                DS_NOTAS = s.DS_NOTAS,
                CL_PERIODO = s.CL_PERIODO,
                FE_INICIO = s.FE_INICIO,
                CL_ESTADO_PERIODO = s.CL_ESTADO_PERIODO,
                ID_PERIODO_CLIMA = s.ID_PERIODO_CLIMA,
                CL_ORIGEN_CUESTIONARIO = s.CL_ORIGEN_CUESTIONARIO,
                ID_PERIODO_ORIGEN = s.ID_PERIODO_ORIGEN
            }).ToList();


        }

        private void EstatusBotonesPeriodos(bool pFgEstatus)
        {
            btnConfigurar.Enabled = pFgEstatus && vConfigurar;
            btnCerrar.Enabled = pFgEstatus && vCerrar;
            btnEnviarSolicitudes.Enabled = pFgEstatus && vEnviarSolicitudes;
            btnContestar.Enabled = pFgEstatus && vContestar;
        }

        private void ordenarListView(string ordenamiento)
        {
            var campo = cmbOrdenamiento.SelectedValue;
            rlvPeriodos.Items[0].FireCommandEvent(RadListView.SortCommandName, campo + ordenamiento);
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
        }

        private void SeguridadProcesos()
        {
            rdAgregar.Enabled = ContextoUsuario.oUsuario.TienePermiso("L.A.A.A");
            vEditar = ContextoUsuario.oUsuario.TienePermiso("L.A.A.B");
            vEliminar = ContextoUsuario.oUsuario.TienePermiso("L.A.A.C");
            btnConfigurar.Enabled = vConfigurar  = ContextoUsuario.oUsuario.TienePermiso("L.A.A.D");
            btnCerrar.Enabled = vCerrar = ContextoUsuario.oUsuario.TienePermiso("L.A.A.E");
            btnReactivar.Enabled = ContextoUsuario.oUsuario.TienePermiso("L.A.A.F");
            btnCopiar.Enabled = ContextoUsuario.oUsuario.TienePermiso("L.A.A.G");
            btnVistaPreviaCuestionario.Enabled = ContextoUsuario.oUsuario.TienePermiso("L.A.A.H");
            btnEnviarSolicitudes.Enabled = vEnviarSolicitudes = ContextoUsuario.oUsuario.TienePermiso("L.A.A.I");
            btnContestar.Enabled = vContestar = ContextoUsuario.oUsuario.TienePermiso("L.A.A.J");
            btnControlAvance.Enabled = ContextoUsuario.oUsuario.TienePermiso("L.A.A.K");
            btnReportes.Enabled = ContextoUsuario.oUsuario.TienePermiso("L.A.A.L");
            btnRDistribucion.Enabled = ContextoUsuario.oUsuario.TienePermiso("L.A.A.M");
            btnPreguntasAbiertas.Enabled = ContextoUsuario.oUsuario.TienePermiso("L.A.A.N");
            btnGlobal.Enabled = ContextoUsuario.oUsuario.TienePermiso("L.A.A.O");
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            //ObtenerLista();

            if (!IsPostBack)
                SeguridadProcesos();

            Page.Title = "Clima laboral";
        }

        protected void rlvPeriodos_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
        {
            ClimaLaboralNegocio nPeriodo = new ClimaLaboralNegocio();
            rlvPeriodos.DataSource = nPeriodo.ObtienePeriodosClima();
            //rlvPeriodos.DataSource = vListaPeriodo;
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

        protected void rlvPeriodos_ItemCommand(object sender, RadListViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                RadListViewDataItem item = e.ListViewItem as RadListViewDataItem;
                vIdPeriodo = int.Parse(item.GetDataKeyValue("ID_PERIODO").ToString());

                ClimaLaboralNegocio nPeriodo = new ClimaLaboralNegocio();
                SPE_OBTIENE_EO_PERIODOS_CLIMA_Result vPeriodo = nPeriodo.ObtienePeriodosClima(pIdPerido: vIdPeriodo).FirstOrDefault();


                txtClPeriodo.Text = vPeriodo.CL_PERIODO;
                txtDsPeriodo.Text = vPeriodo.DS_PERIODO;
                txtClEstatus.Text = vPeriodo.CL_ESTADO_PERIODO;
                txtTipo.Text = vPeriodo.CL_ORIGEN_CUESTIONARIO;
                txtUsuarioMod.Text = vPeriodo.CL_USUARIO_APP_MODIFICA;
                txtFechaMod.Text = String.Format("{0:dd/MM/yyyy}", vPeriodo.FE_MODIFICA);

                if (vPeriodo.DS_NOTAS != null)
                {
                    if (vPeriodo.DS_NOTAS.Contains("DS_NOTA"))
                    {
                        txtNotas.InnerHtml = Utileria.MostrarNotas(vPeriodo.DS_NOTAS);
                    }
                    else
                    {
                        XElement vRequerimientos = XElement.Parse(vPeriodo.DS_NOTAS);
                        if (vRequerimientos != null)
                        {
                            vRequerimientos.Name = vNbFirstRadEditorTagName;
                            txtNotas.InnerHtml = vRequerimientos.ToString();
                        }
                    }
                }

                //var vPeriodo = vListaPeriodo.Where(w => w.ID_PERIODO == vIdPeriodo).FirstOrDefault();

                //txtDsDescripcion.Text = vPeriodo.DS_PERIODO;
                //txtDsEstado.Text = vPeriodo.CL_ESTADO_PERIODO;
                //// txtDsNotas.Content = vPeriodo.DS_NOTAS;
                //if (vPeriodo.DS_NOTAS != null)
                //{
                //    if (vPeriodo.DS_NOTAS.Contains("DS_NOTA"))
                //    {
                //        txtDsNotas.Content = Utileria.MostrarNotas(vPeriodo.DS_NOTAS);
                //    }
                //    else
                //    {
                //        XElement vRequerimientos = XElement.Parse(vPeriodo.DS_NOTAS);
                //        if (vRequerimientos != null)
                //        {
                //            vRequerimientos.Name = vNbFirstRadEditorTagName;
                //            txtDsNotas.Content = vRequerimientos.ToString();
                //        }
                //    }
                //}

                //DESACTIVAR BOTONES
                string vClEstado = (item.GetDataKeyValue("CL_ESTADO_PERIODO").ToString());
                EstatusBotonesPeriodos((vClEstado.ToUpper() == "CERRADO") ? false : true);

            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ClimaLaboralNegocio nPeriodo = new ClimaLaboralNegocio();

            foreach (RadListViewDataItem item in rlvPeriodos.SelectedItems)
            {
                E_RESULTADO vResultado = nPeriodo.EliminaPeriodoClimaLaboral(int.Parse(item.GetDataKeyValue("ID_PERIODO").ToString()));
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                txtClPeriodo.Text = "";
                txtDsPeriodo.Text = "";
                txtClEstatus.Text = "";
                txtTipo.Text = "";
                txtUsuarioMod.Text = "";
                txtFechaMod.Text = "";
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "onCloseWindow");
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            var vMensaje = nPeriodo.ActualizaEstatusPeriodo((int)vIdPeriodo, "Cerrado", vClUsuario, vNbPrograma);
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje.MENSAJE[0].DS_MENSAJE.ToString(), vMensaje.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "onCloseWindow");
            //rlvPeriodos.Rebind();
            EstatusBotonesPeriodos(false);
            //txtDsEstado.Text = "CERRADO";

        }

        protected void btnReactivar_Click(object sender, EventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            var vMensaje = nPeriodo.ActualizaEstatusPeriodo((int)vIdPeriodo, "Abierto", vClUsuario, vNbPrograma);
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje.MENSAJE[0].DS_MENSAJE.ToString(), vMensaje.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "onCloseWindow");
            //  rlvPeriodos.Rebind();
            EstatusBotonesPeriodos(true);
            //txtDsEstado.Text = "ABIERTO";
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
        }

    }
}