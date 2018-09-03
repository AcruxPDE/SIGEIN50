using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Entidades.MetodologiaCompensacion;
using SIGE.Negocio.MetodologiaCompensacion;
using SIGE.Entidades;
using Telerik.Web.UI;
using SIGE.Entidades.Externas;
using SIGE.WebApp.Comunes;
using Newtonsoft.Json;

namespace SIGE.WebApp.MPC
{
    public partial class Tabuladores : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        TabuladoresNegocio nTabulador = new TabuladoresNegocio();

        E_TABULADOR vTabulador
        {
            get { return (E_TABULADOR)ViewState["vs_vTabulador"]; }
            set { ViewState["vs_vTabulador"] = value; }
        }

        public int? vIdConsulta
        {
            get { return (int?)ViewState["vs_vIdConsulta"]; }
            set { ViewState["vs_vIdConsulta"] = value; }
        }

        public bool vFgEditar
        {
            get { return (bool)ViewState["vs_vFgEditar"]; }
            set { ViewState["vs_vFgEditar"] = value; }
        }

        public bool vFgEliminar
        {
            get { return (bool)ViewState["vs_vFgEliminar"]; }
            set { ViewState["vs_vFgEliminar"] = value; }
        }

        public bool vCambiarEstado
        {
            get { return (bool)ViewState["vs_vCambiarEstado"]; }
            set { ViewState["vs_vCambiarEstado"] = value; }
        }

        public bool vConfigurar
        {
            get { return (bool)ViewState["vs_vConfigurar"]; }
            set { ViewState["vs_vConfigurar"] = value; }
        }

        public bool vValuar
        {
            get { return (bool)ViewState["vs_vValuar"]; }
            set { ViewState["vs_vValuar"] = value; }
        }

        public bool vIncrementos
        {
            get { return (bool)ViewState["vs_vIncrementos"]; }
            set { ViewState["vs_vIncrementos"] = value; }
        }

        public bool vCapturar
        {
            get { return (bool)ViewState["vs_vCapturar"]; }
            set { ViewState["vs_vCapturar"] = value; }
        }

        public bool vCrear
        {
            get { return (bool)ViewState["vs_vCrear"]; }
            set { ViewState["vs_vCrear"] = value; }
        }

        public bool vFgTabuladorConfigurado
        {
            get { return (bool)ViewState["vs_vFgTabuladorConfigurado"]; }
            set { ViewState["vs_vFgTabuladorConfigurado"] = value; }
        }

        private void CargarDatosDetalle(int? pIdPeriodo)
        {
            SPE_OBTIENE_TABULADORES_Result vPeriodo = nTabulador.ObtenerTabuladores(ID_TABULADOR: pIdPeriodo).FirstOrDefault();

            if (vPeriodo != null)
            {
                txtClPeriodo.Text = vPeriodo.CL_TABULADOR;
                txtDsPeriodo.Text = vPeriodo.NB_TABULADOR;
                txtClEstatus.Text = vPeriodo.CL_ESTADO;
                txtTipo.Text = vPeriodo.CL_TIPO_PUESTO;
                txtUsuarioMod.Text = vPeriodo.CL_USUARIO_APP_MODIFICA;
                txtFechaMod.Text = String.Format("{0:dd/MM/yyyy}", vPeriodo.FE_ULTIMA_MODIFICACION);
                txtNotas.Text = vPeriodo.DS_TABULADOR;

                rlvConsultas.Rebind();
            }
        }

        private void seleccionarPeriodo()
        {
            rlvConsultas.SelectedItems.Clear();
            rlvConsultas.SelectedIndexes.Clear();
            rlvConsultas.CurrentPageIndex = 0;
            if (rlvConsultas.Items.Count > 0)
            {
                rlvConsultas.Items[0].Selected = true;
            }

            rlvConsultas.Rebind();

            string vIdPeriodoSeleccionado = rlvConsultas.Items[0].GetDataKeyValue("ID_TABULADOR").ToString();
            if (vIdPeriodoSeleccionado != null)
            {
                CargarDatosDetalle(int.Parse(vIdPeriodoSeleccionado));

                var vVerificaConfiguracion = nTabulador.VerificarTabulador(int.Parse(vIdPeriodoSeleccionado)).FirstOrDefault();
                string vClEstado = (rlvConsultas.Items[0].GetDataKeyValue("CL_ESTADO").ToString());
                EstatusBotonesPeriodos((vClEstado.ToUpper() == "CERRADO") ? false : true, (bool)vVerificaConfiguracion.FG_CONFIGURACION);

            }
        }

        private void SeguridadProcesos()
        {
            btnAgregar.Enabled = ContextoUsuario.oUsuario.TienePermiso("O.A.A.A");
            vFgEditar = ContextoUsuario.oUsuario.TienePermiso("O.A.A.B");
            vFgEliminar = ContextoUsuario.oUsuario.TienePermiso("O.A.A.C");
            btnConfigurar.Enabled = vConfigurar = ContextoUsuario.oUsuario.TienePermiso("O.A.A.D");
            //btnVerNiveles.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.A.A.E");       
            btnCambiarEstado.Enabled = vCambiarEstado = ContextoUsuario.oUsuario.TienePermiso("O.A.A.E");
            btnReabrir.Enabled = ContextoUsuario.oUsuario.TienePermiso("O.A.A.F");
            btnCopiar.Enabled = ContextoUsuario.oUsuario.TienePermiso("O.A.A.G");
            btnValuar.Enabled = vValuar = ContextoUsuario.oUsuario.TienePermiso("O.A.A.H");
            btnCapturar.Enabled = vCapturar  = ContextoUsuario.oUsuario.TienePermiso("O.A.A.I");
            btnCrear.Enabled = vCrear = ContextoUsuario.oUsuario.TienePermiso("O.A.A.J");
            btnIncrementos.Enabled = vIncrementos = ContextoUsuario.oUsuario.TienePermiso("O.A.A.K");
            btnConsSueldos.Enabled = ContextoUsuario.oUsuario.TienePermiso("O.A.A.L");
            btnGraficaAnalisis.Enabled = ContextoUsuario.oUsuario.TienePermiso("O.A.A.M");
            btnDesviaciones.Enabled = ContextoUsuario.oUsuario.TienePermiso("O.A.A.N");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            if (!IsPostBack)
            {
                SeguridadProcesos();
            }
        }
        
        //protected void grdTabuladores_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        //{
           // grdTabuladores.DataSource = nTabulador.ObtenerTabuladores();
        //}

        private void ordenarListView(string ordenamiento)
        {
            var campo = cmbOrdenamiento.SelectedValue;
            rlvConsultas.Items[0].FireCommandEvent(RadListView.SortCommandName, campo + ordenamiento);
        }

        private void EstatusBotonesPeriodos(bool pFgEstatus, bool pFgConfigurado)
        {
            //btnConfigurar.Enabled = pFgEstatus;
            btnCambiarEstado.Enabled = pFgEstatus && vCambiarEstado;
            btnConfigurar.Enabled = pFgEstatus && vConfigurar;
            btnValuar.Enabled = pFgEstatus && vValuar && pFgConfigurado;
            btnIncrementos.Enabled = pFgEstatus && vIncrementos && pFgConfigurado;
            btnCapturar.Enabled = pFgEstatus && vCapturar && pFgConfigurado;
            btnCrear.Enabled = pFgEstatus && vCrear && pFgConfigurado;
            
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            foreach (RadListViewDataItem item in rlvConsultas.SelectedItems)
            {
                var vMensaje = nTabulador.EliminaTabulador(int.Parse(item.GetDataKeyValue("ID_TABULADOR").ToString()), vClUsuario, vNbPrograma);
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje.MENSAJE[0].DS_MENSAJE.ToString(), vMensaje.CL_TIPO_ERROR, 400, 150, "");
                //vIdConsulta = null;
                rlvConsultas.Rebind();
                //rlvConsultas.SelectedValues.Clear();
                //rlvConsultas.SelectedItems.Clear();
                if (rlvConsultas.SelectedItems.Count > 0)
                {
                    string vIdPeriodoSeleccionado = rlvConsultas.SelectedItems[0].GetDataKeyValue("ID_TABULADOR").ToString();
                    if (vIdPeriodoSeleccionado != null)
                    {
                        CargarDatosDetalle(int.Parse(vIdPeriodoSeleccionado));

                        var vVerificaConfiguracion = nTabulador.VerificarTabulador(int.Parse(vIdPeriodoSeleccionado)).FirstOrDefault();
                        string vClEstado = (rlvConsultas.SelectedItems[0].GetDataKeyValue("CL_ESTADO").ToString());
                        EstatusBotonesPeriodos((vClEstado.ToUpper() == "CERRADO") ? false : true, (bool)vVerificaConfiguracion.FG_CONFIGURACION);
                    }
                }
                //txtClPeriodo.Text = "";
                //txtDsPeriodo.Text = "";
                //txtClEstatus.Text = "";
                //txtNotas.Text = "";
                //txtTipo.Text = "";
                //txtUsuarioMod.Text = "";
                //txtFechaMod.Text = "";
            }
            //foreach (GridDataItem item in grdTabuladores.SelectedItems)
            //{
            //    E_RESULTADO vResultado = nTabulador.EliminaTabulador(int.Parse(item.GetDataKeyValue("ID_TABULADOR").ToString()), vClUsuario, vNbPrograma);
            //    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            //    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindow");
            //}
        }

        protected void btnCambiarEstado_Click(object sender, EventArgs e)
        {
           TabuladoresNegocio nTabulador = new TabuladoresNegocio();
           // var vpTabulador = nTabulador.ObtenerTabuladores(ID_TABULADOR: vIdConsulta).FirstOrDefault();

            //if (vpTabulador.CL_ESTADO == "ABIERTO")
            //{
            //    vpTabulador.CL_ESTADO = "CERRADO";
            //}
            //else
            //{
            //    vpTabulador.CL_ESTADO = "ABIERTO";
            //}
            //vTabulador = new E_TABULADOR
            //{
            //    CL_TABULADOR = vpTabulador.CL_TABULADOR,
            //    ID_TABULADOR = vpTabulador.ID_TABULADOR,
            //    NB_TABULADOR = vpTabulador.NB_TABULADOR,
            //    DS_TABULADOR = vpTabulador.DS_TABULADOR,
            //    NO_NIVELES = vpTabulador.NO_NIVELES,
            //    NO_CATEGORIAS = vpTabulador.NO_CATEGORIAS,
            //    PR_INFLACION = vpTabulador.PR_INFLACION,
            //    PR_PROGRESION = vpTabulador.PR_PROGRESION,
            //    XML_VARIACION = vpTabulador.XML_VARIACION,
            //    ID_CUARTIL_INCREMENTO = vpTabulador.ID_CUARTIL_INCREMENTO,
            //    ID_CUARTIL_INFLACIONAL = vpTabulador.ID_CUARTIL_INFLACIONAL,
            //    FE_CREACION = vpTabulador.FE_CREACION,
            //    FE_VIGENCIA = vpTabulador.FE_VIGENCIA,
            //    CL_ESTADO = vpTabulador.CL_ESTADO,
            //    CL_SUELDO_COMPARACION = vpTabulador.CL_SUELDO_COMPARACION,
            //    CL_TIPO_PUESTO = vpTabulador.CL_TIPO_PUESTO
            //};

            E_RESULTADO vResultado = nTabulador.ActualizarEstatusTabulador(pID_TABULADOR: vIdConsulta, pCL_ESTATUS_TABULADOR: "CERRADO", usuario: vClUsuario, programa: vNbPrograma, pClTipoOperacion: E_TIPO_OPERACION_DB.A.ToString());
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            rlvConsultas.Rebind();
            EstatusBotonesPeriodos(false, vFgTabuladorConfigurado);
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");

        }

        //protected void grdTabuladores_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //GridDataItem item = (GridDataItem)grdTabuladores.SelectedItems[0];
        //    //int dataKey = int.Parse(item.GetDataKeyValue("ID_TABULADOR").ToString());
        //    ////if (dataKey != 0) {
        //    //    var vTabulador = nTabulador.ObtenerTabuladores(ID_TABULADOR: dataKey).FirstOrDefault();
        //    //    if (vTabulador.FG_MERCADO_SALARIAL.Equals(true))
        //    //        chbMercadoSalarial.Checked = true;
        //    //    else
        //    //        chbMercadoSalarial.Checked = false;
        //    //    if (vTabulador.FG_PLANEACION_INCREMENTOS.Equals(true))
        //    //        chbPlaneacionIncrementos.Checked = true;
        //    //    else
        //    //        chbPlaneacionIncrementos.Checked = false;
        //    //    if (vTabulador.FG_VALUACION_PUESTOS.Equals(true))
        //    //        chbValuacionPuestos.Checked = true;
        //    //    else
        //    //        chbValuacionPuestos.Checked = false;
        //    //    if (vTabulador.FG_TABULADOR_MAESTRO.Equals(true))
        //    //        chbTabuladorMaestro.Checked = true;
        //    //    else
        //    //        chbTabuladorMaestro.Checked = false;
        //    ////}
        //}

        protected void rlvConsultas_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            rlvConsultas.DataSource = nTabulador.ObtenerTabuladores();
        }

        protected void rlvConsultas_ItemCommand(object sender, RadListViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                RadListViewDataItem item = e.ListViewItem as RadListViewDataItem;
               
                rlvConsultas.SelectedItems.Clear();
                item.Selected = true;

                int vIdConsultaLista = 0;
                if (int.TryParse(item.GetDataKeyValue("ID_TABULADOR").ToString(), out vIdConsultaLista))
                    vIdConsulta = vIdConsultaLista;

                CargarDatosDetalle(vIdConsulta);
                //SPE_OBTIENE_TABULADORES_Result vPeriodo =  nTabulador.ObtenerTabuladores(ID_TABULADOR: vIdConsulta).FirstOrDefault();

                //txtClPeriodo.Text = vPeriodo.CL_TABULADOR;
                //txtDsPeriodo.Text = vPeriodo.NB_TABULADOR;
                //txtClEstatus.Text = vPeriodo.CL_ESTADO;
                //txtTipo.Text = vPeriodo.CL_TIPO_PUESTO;
                //txtUsuarioMod.Text = vPeriodo.CL_USUARIO_APP_MODIFICA;
                //txtFechaMod.Text = String.Format("{0:dd/MM/yyyy}", vPeriodo.FE_ULTIMA_MODIFICACION);
                //txtNotas.Text = vPeriodo.DS_TABULADOR;
                var vVerificaConfiguracion = nTabulador.VerificarTabulador(vIdConsulta).FirstOrDefault();
                if (vVerificaConfiguracion != null)
                    vFgTabuladorConfigurado = (bool)vVerificaConfiguracion.FG_CONFIGURACION;


                //DESACTIVAR BOTONES
                if (e.CommandName == RadListView.SelectCommandName)
                {
                //    (item.FindControl("btnModificar") as RadButton).Enabled = ContextoUsuario.oUsuario.TienePermiso("K.A.A.B");
                //    (item.FindControl("btnEliminar") as RadButton).Enabled = ContextoUsuario.oUsuario.TienePermiso("K.A.A.C");
                
                    string vClEstado = (item.GetDataKeyValue("CL_ESTADO").ToString());
                    EstatusBotonesPeriodos((vClEstado.ToUpper() == "CERRADO") ? false : true, vFgTabuladorConfigurado);
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

        protected void rfFiltros_ApplyExpressions(object sender, RadFilterApplyExpressionsEventArgs e)
        {

            RadFilterListViewQueryProvider provider = new RadFilterListViewQueryProvider(new List<RadFilterGroupOperation>() { RadFilterGroupOperation.And, RadFilterGroupOperation.Or });
            provider.ProcessGroup(e.ExpressionRoot);

            if (provider.ListViewExpressions.Count > 0)
            {
                rlvConsultas.FilterExpressions.Add(provider.ListViewExpressions[0]);
            }
            else
            {
                rlvConsultas.FilterExpressions.Clear();
            }

            rlvConsultas.Rebind();
        }

        protected void btnReabrir_Click(object sender, EventArgs e)
        {
            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            //var vpTabulador = nTabulador.ObtenerTabuladores(ID_TABULADOR: vIdConsulta).FirstOrDefault();

            //if (vpTabulador.CL_ESTADO == "ABIERTO")
            //{
            //    vpTabulador.CL_ESTADO = "CERRADO";
            //}
            //else
            //{
            //    vpTabulador.CL_ESTADO = "ABIERTO";
            //}
            //vTabulador = new E_TABULADOR
            //{
            //    CL_TABULADOR = vpTabulador.CL_TABULADOR,
            //    ID_TABULADOR = vpTabulador.ID_TABULADOR,
            //    NB_TABULADOR = vpTabulador.NB_TABULADOR,
            //    DS_TABULADOR = vpTabulador.DS_TABULADOR,
            //    NO_NIVELES = vpTabulador.NO_NIVELES,
            //    NO_CATEGORIAS = vpTabulador.NO_CATEGORIAS,
            //    PR_INFLACION = vpTabulador.PR_INFLACION,
            //    PR_PROGRESION = vpTabulador.PR_PROGRESION,
            //    XML_VARIACION = vpTabulador.XML_VARIACION,
            //    ID_CUARTIL_INCREMENTO = vpTabulador.ID_CUARTIL_INCREMENTO,
            //    ID_CUARTIL_INFLACIONAL = vpTabulador.ID_CUARTIL_INFLACIONAL,
            //    FE_CREACION = vpTabulador.FE_CREACION,
            //    FE_VIGENCIA = vpTabulador.FE_VIGENCIA,
            //    CL_ESTADO = vpTabulador.CL_ESTADO,
            //    CL_SUELDO_COMPARACION = vpTabulador.CL_SUELDO_COMPARACION,
            //    CL_TIPO_PUESTO = vpTabulador.CL_TIPO_PUESTO
            //};

            E_RESULTADO vResultado = nTabulador.ActualizarEstatusTabulador(pID_TABULADOR: vIdConsulta, pCL_ESTATUS_TABULADOR: "ABIERTO", usuario: vClUsuario, programa: vNbPrograma, pClTipoOperacion: E_TIPO_OPERACION_DB.A.ToString());
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            rlvConsultas.Rebind();
            EstatusBotonesPeriodos(true, vFgTabuladorConfigurado);
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
        }

        protected void ramTabulador_AjaxRequest(object sender, AjaxRequestEventArgs e)
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
                rlvConsultas.Rebind();
                string vIdPeriodoSeleccionado = rlvConsultas.SelectedItems[0].GetDataKeyValue("ID_TABULADOR").ToString();
                if (vIdPeriodoSeleccionado != null)
                {
                    CargarDatosDetalle(int.Parse(vIdPeriodoSeleccionado));
                    var vVerificaConfiguracion = nTabulador.VerificarTabulador(int.Parse(vIdPeriodoSeleccionado)).FirstOrDefault();
                    string vClEstado = (rlvConsultas.SelectedItems[0].GetDataKeyValue("CL_ESTADO").ToString());
                    EstatusBotonesPeriodos((vClEstado.ToUpper() == "CERRADO") ? false : true, (bool)vVerificaConfiguracion.FG_CONFIGURACION);
                }
            }
            else if (vSeleccion.clTipo == "ACTUALIZAR")
            {
                rlvConsultas.Rebind();
                string vIdPeriodoSeleccionado = rlvConsultas.SelectedItems[0].GetDataKeyValue("ID_TABULADOR").ToString();
                if (vIdPeriodoSeleccionado != null)
                {
                    CargarDatosDetalle(int.Parse(vIdPeriodoSeleccionado));

                    var vVerificaConfiguracion = nTabulador.VerificarTabulador(int.Parse(vIdPeriodoSeleccionado)).FirstOrDefault();
                    string vClEstado = (rlvConsultas.SelectedItems[0].GetDataKeyValue("CL_ESTADO").ToString());
                    EstatusBotonesPeriodos((vClEstado.ToUpper() == "CERRADO") ? false : true, (bool)vVerificaConfiguracion.FG_CONFIGURACION);
                }
            }
        }

    }
}