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

        public bool vControlAvance
        {
            get { return (bool)ViewState["vs_vControlAvance"]; }
            set { ViewState["vs_vControlAvance"] = value; }
        }

        public bool vEliminar
        {
            get { return (bool)ViewState["vs_vEliminar"]; }
            set { ViewState["vs_vEliminar"] = value; }
        }

        public bool vVistaPrevia
        {
            get { return (bool)ViewState["vs_vVistaPrevia"]; }
            set { ViewState["vs_vVistaPrevia"] = value; }
        }

        private bool vFgCuestionariosCreados
        {
            get { return (bool)ViewState["vs_vFgCuestionariosCreados"]; }
            set { ViewState["vs_vFgCuestionariosCreados"] = value; }
        }

        private bool cl_tipo_configuracion = true;

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
                ID_PERIODO_ORIGEN = s.ID_PERIODO_ORIGEN,
                CL_TIPO_CONFIGURACION = s.CL_TIPO_CONFIGURACION
            }).ToList();


        }

        private void EstatusBotonesPeriodos(bool pFgEstatus, bool pFgCuestionariosCreados)
        {
            btnConfigurar.Enabled = pFgEstatus && vConfigurar;
            btnCerrar.Enabled = pFgEstatus && vCerrar;


            btnEnviarSolicitudes.Enabled = pFgEstatus && vEnviarSolicitudes && pFgCuestionariosCreados && cl_tipo_configuracion;
            btnContestar.Enabled = pFgEstatus && vContestar && pFgCuestionariosCreados;
            btnControlAvance.Enabled = vControlAvance && pFgCuestionariosCreados;
            btnVistaPreviaCuestionario.Enabled = vVistaPrevia && pFgCuestionariosCreados;
        }

        private void ordenarListView(string ordenamiento)
        {
            var campo = cmbOrdenamiento.SelectedValue;
            rlvPeriodos.Items[0].FireCommandEvent(RadListView.SortCommandName, campo + ordenamiento);
        }

        private void CargarDatosDetalle(int? pIdPeriodo)
        {
            ClimaLaboralNegocio nPeriodo = new ClimaLaboralNegocio();
            SPE_OBTIENE_EO_PERIODOS_CLIMA_Result vPeriodo = nPeriodo.ObtienePeriodosClima(pIdPerido: pIdPeriodo).FirstOrDefault();

            if (vPeriodo != null)
            {
                txtClPeriodo.Text = vPeriodo.CL_PERIODO;
                txtDsPeriodo.Text = vPeriodo.DS_PERIODO;
                txtClEstatus.Text = vPeriodo.CL_ESTADO_PERIODO;
                string tipoPeriodo = vPeriodo.CL_TIPO_CONFIGURACION;

                //VERIFICAR SI EL PERIODO ES CON EVALUADORES O SIN EVALUADORES
                if (tipoPeriodo == "PARAMETROS")
                    cl_tipo_configuracion = false;
                else
                    cl_tipo_configuracion = true;
                                    
                if (vPeriodo.CL_ORIGEN_CUESTIONARIO == "PREDEFINIDO")
                    txtTipo.Text = "Predefinido de SIGEIN";    
                else if (vPeriodo.CL_ORIGEN_CUESTIONARIO == "COPIA")
                    txtTipo.Text = "Copia de otro período";
                else if (vPeriodo.CL_ORIGEN_CUESTIONARIO == "VACIO")
                    txtTipo.Text = "Creado en blanco";
   
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
                vFgCuestionariosCreados = bool.Parse(rlvPeriodos.Items[0].GetDataKeyValue("FG_CUESTIONARIOS_CREADOS").ToString());
                string vClEstadoPeriodo = rlvPeriodos.Items[0].GetDataKeyValue("CL_ESTADO_PERIODO").ToString();
                if (vIdPeriodoSeleccionado != null)
                {
                    CargarDatosDetalle(int.Parse(vIdPeriodoSeleccionado));

                    EstatusBotonesPeriodos((vClEstadoPeriodo.ToUpper() == "CERRADO") ? false : true, vFgCuestionariosCreados);
                }
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
            btnVistaPreviaCuestionario.Enabled = vVistaPrevia = ContextoUsuario.oUsuario.TienePermiso("L.A.A.H");
            btnEnviarSolicitudes.Enabled = vEnviarSolicitudes = ContextoUsuario.oUsuario.TienePermiso("L.A.A.I");
            btnContestar.Enabled = vContestar = ContextoUsuario.oUsuario.TienePermiso("L.A.A.J");
            btnControlAvance.Enabled = vControlAvance = ContextoUsuario.oUsuario.TienePermiso("L.A.A.K");
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

                vFgCuestionariosCreados = bool.Parse(item.GetDataKeyValue("FG_CUESTIONARIOS_CREADOS").ToString());

                CargarDatosDetalle(vIdPeriodo);

                //ClimaLaboralNegocio nPeriodo = new ClimaLaboralNegocio();
                //SPE_OBTIENE_EO_PERIODOS_CLIMA_Result vPeriodo = nPeriodo.ObtienePeriodosClima(pIdPerido: vIdPeriodo).FirstOrDefault();


                //txtClPeriodo.Text = vPeriodo.CL_PERIODO;
                //txtDsPeriodo.Text = vPeriodo.DS_PERIODO;
                //txtClEstatus.Text = vPeriodo.CL_ESTADO_PERIODO;
                //txtTipo.Text = vPeriodo.CL_ORIGEN_CUESTIONARIO;
                //txtUsuarioMod.Text = vPeriodo.CL_USUARIO_APP_MODIFICA;
                //txtFechaMod.Text = String.Format("{0:dd/MM/yyyy}", vPeriodo.FE_MODIFICA);

                //if (vPeriodo.DS_NOTAS != null)
                //{
                //    if (vPeriodo.DS_NOTAS.Contains("DS_NOTA"))
                //    {
                //        txtNotas.InnerHtml = Utileria.MostrarNotas(vPeriodo.DS_NOTAS);
                //    }
                //    else
                //    {
                //        XElement vRequerimientos = XElement.Parse(vPeriodo.DS_NOTAS);
                //        if (vRequerimientos != null)
                //        {
                //            vRequerimientos.Name = vNbFirstRadEditorTagName;
                //            txtNotas.InnerHtml = vRequerimientos.ToString();
                //        }
                //    }
                //}

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
                EstatusBotonesPeriodos((vClEstado.ToUpper() == "CERRADO") ? false : true, vFgCuestionariosCreados);

            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ClimaLaboralNegocio nPeriodo = new ClimaLaboralNegocio();

            foreach (RadListViewDataItem item in rlvPeriodos.SelectedItems)
            {
                E_RESULTADO vResultado = nPeriodo.EliminaPeriodoClimaLaboral(int.Parse(item.GetDataKeyValue("ID_PERIODO").ToString()));
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                //txtClPeriodo.Text = "";
                //txtDsPeriodo.Text = "";
                //txtClEstatus.Text = "";
                //txtTipo.Text = "";
                //txtUsuarioMod.Text = "";
                //txtFechaMod.Text = "";
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "");
                rlvPeriodos.Rebind();

                if (rlvPeriodos.SelectedItems.Count > 0)
                {
                    string vIdPeriodoSeleccionado = rlvPeriodos.SelectedItems[0].GetDataKeyValue("ID_PERIODO").ToString();
                    vFgCuestionariosCreados = bool.Parse(rlvPeriodos.SelectedItems[0].GetDataKeyValue("FG_CUESTIONARIOS_CREADOS").ToString());
                    string vClEstadoPeriodo = rlvPeriodos.SelectedItems[0].GetDataKeyValue("CL_ESTADO_PERIODO").ToString();
                    if (vIdPeriodoSeleccionado != null)
                    {
                        CargarDatosDetalle(int.Parse(vIdPeriodoSeleccionado));

                        EstatusBotonesPeriodos((vClEstadoPeriodo.ToUpper() == "CERRADO") ? false : true, vFgCuestionariosCreados);
                    }
                }
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            var vMensaje = nPeriodo.ActualizaEstatusPeriodo((int)vIdPeriodo, "Cerrado", vClUsuario, vNbPrograma);
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje.MENSAJE[0].DS_MENSAJE.ToString(), vMensaje.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "onCloseWindow");
            //rlvPeriodos.Rebind();
            EstatusBotonesPeriodos(false, vFgCuestionariosCreados);
            //txtDsEstado.Text = "CERRADO";

        }

        protected void btnReactivar_Click(object sender, EventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            var vMensaje = nPeriodo.ActualizaEstatusPeriodo((int)vIdPeriodo, "Abierto", vClUsuario, vNbPrograma);
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje.MENSAJE[0].DS_MENSAJE.ToString(), vMensaje.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "onCloseWindow");
            //  rlvPeriodos.Rebind();
            EstatusBotonesPeriodos(true, vFgCuestionariosCreados);
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
            else if (vSeleccion.clTipo == "CONFIGURACION")
            {
                rlvPeriodos.Rebind();
                if (rlvPeriodos.SelectedItems.Count > 0)
                {
                    string vIdPeriodoSeleccionado = rlvPeriodos.SelectedItems[0].GetDataKeyValue("ID_PERIODO").ToString();
                    vFgCuestionariosCreados = bool.Parse(rlvPeriodos.SelectedItems[0].GetDataKeyValue("FG_CUESTIONARIOS_CREADOS").ToString());
                    string vClEstadoPeriodo = rlvPeriodos.SelectedItems[0].GetDataKeyValue("CL_ESTADO_PERIODO").ToString();
                    if (vIdPeriodoSeleccionado != null)
                    {
                        CargarDatosDetalle(int.Parse(vIdPeriodoSeleccionado));

                        EstatusBotonesPeriodos((vClEstadoPeriodo.ToUpper() == "CERRADO") ? false : true, vFgCuestionariosCreados);
                    }
                }
            }
            else if(vSeleccion.clTipo == "ACTUALIZAR")
            {
                rlvPeriodos.Rebind();
                if (rlvPeriodos.SelectedItems.Count > 0)
                {
                    string vIdPeriodoSeleccionado = rlvPeriodos.SelectedItems[0].GetDataKeyValue("ID_PERIODO").ToString();
                    vFgCuestionariosCreados = bool.Parse(rlvPeriodos.SelectedItems[0].GetDataKeyValue("FG_CUESTIONARIOS_CREADOS").ToString());
                    string vClEstadoPeriodo = rlvPeriodos.SelectedItems[0].GetDataKeyValue("CL_ESTADO_PERIODO").ToString();
                    if (vIdPeriodoSeleccionado != null)
                    {
                        CargarDatosDetalle(int.Parse(vIdPeriodoSeleccionado));

                        EstatusBotonesPeriodos((vClEstadoPeriodo.ToUpper() == "CERRADO") ? false : true, vFgCuestionariosCreados);
                    }
                }
            }
        }

    }
}