using Newtonsoft.Json;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.TableroControl;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.TC
{
    public partial class VentanaConfiguracionTablero : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int vIdPeriodo
        {
            get { return (int)ViewState["vs_id_periodo"]; }
            set { ViewState["vs_id_periodo"] = value; }
        }

        public bool vFgEvaluacionIdp
        {
            get { return (bool)ViewState["vs_fg_evaluacion_idp"]; }
            set { ViewState["vs_fg_evaluacion_idp"] = value; }
        }

        public bool vFgEvalucionFyd
        {
            get { return (bool)ViewState["vs_fg_evaluacion_fyd"]; }
            set { ViewState["vs_fg_evaluacion_fyd"] = value; }
        }

        public bool vFgEvaluacionDesempeno
        {
            get { return (bool)ViewState["vs_fg_evaluacion_desempeno"]; }
            set { ViewState["vs_fg_evaluacion_desempeno"] = value; }
        }

        public bool vFgEvaluacionClima
        {
            get { return (bool)ViewState["vs_fg_evaluacion_clima"]; }
            set { ViewState["vs_fg_evaluacion_clima"] = value; }
        }

        public bool vFgSituacionSalarial
        {
            get { return (bool)ViewState["vs_fg_situacion_salarial"]; }
            set { ViewState["vs_fg_situacion_salarial"] = value; }
        }

        #endregion

        #region Funciones

        private void CargarDatos()
        {
            TableroControlNegocio nTableroControl = new TableroControlNegocio();

            SPE_OBTIENE_PERIODO_TABLERO_CONTROL_Result vTableroControl = nTableroControl.ObtenerPeriodoTableroControl(pIdPeriodo: vIdPeriodo).FirstOrDefault();

            if (vTableroControl != null)
            {
                txtIdPeriodo.InnerText = vTableroControl.CL_PERIODO;
                txtNbPeriodo.InnerText = vTableroControl.DS_PERIODO;

                vFgEvaluacionIdp = vTableroControl.FG_EVALUACION_IDP == true;
                vFgEvalucionFyd = vTableroControl.FG_EVALUACION_FYD == true;
                vFgEvaluacionDesempeno = vTableroControl.FG_EVALUACION_DESEMPEÑO == true;
                vFgEvaluacionClima = vTableroControl.FG_EVALUACION_CLIMA_LABORAL == true;
                vFgSituacionSalarial = vTableroControl.FG_SITUACION_SALARIAL == true;

                rtsConfiguracionPeriodo.Tabs[1].Visible = vFgEvaluacionIdp;
                rtsConfiguracionPeriodo.Tabs[2].Visible = vFgEvalucionFyd;
                rtsConfiguracionPeriodo.Tabs[3].Visible = vFgEvaluacionDesempeno;
                rtsConfiguracionPeriodo.Tabs[4].Visible = vFgEvaluacionClima;
                rtsConfiguracionPeriodo.Tabs[5].Visible = vFgSituacionSalarial;

               
                if (vTableroControl.CL_ESTADO_PERIODO == "CERRADO" || vTableroControl.CL_ESTADO_PERIODO == "Cerrado")
                {
                    btnAgregarPeriodoFyd.Enabled = false;
                    btnEliminarPeriodoFyd.Enabled = false;
                    btnAgregarPeriodoDesempeno.Enabled = false;
                    btnEliminarPeriodoDesempeno.Enabled = false;
                    btnAgregarPeriodoClima.Enabled = false;
                    btnEliminarPeriodoClima.Enabled = false;
                    btnAgregarTabulador.Enabled = false;
                    btnEliminarTabulador.Enabled = false;
                    btnEliminarEvaluado.Enabled = false;
                    btnSeleccionPorPersona.Enabled = false;
                    btnSeleccionPorPuesto.Enabled = false;
                    btnSeleccionPorArea.Enabled = false;

                }

            }
        }

        protected void AgregarEvaluados(XElement pXmlElementos)
        {
            TableroControlNegocio nTablero = new TableroControlNegocio();
            E_RESULTADO vResultado = nTablero.InsertarEvaluadosTableroControl(vIdPeriodo, pXmlElementos.ToString(), vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
            {
                grdEvaluados.Rebind();

                if (vFgEvaluacionIdp)
                    grdCandidatos.Rebind();

                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
            }
        }

        protected void AgregarEvaluadosPorEmpleado(string pEvaluados)
        {
            List<E_SELECTOR_EMPLEADO> vEvaluados = JsonConvert.DeserializeObject<List<E_SELECTOR_EMPLEADO>>(pEvaluados);

            if (vEvaluados.Count > 0)
                AgregarEvaluados(new XElement("EMPLEADOS", vEvaluados.Select(s => new XElement("EMPLEADO", new XAttribute("ID_EMPLEADO", s.idEmpleado)))));
        }

        protected void AgregarEvaluadosPorPuesto(string pPuestos)
        {
            List<E_SELECTOR_PUESTO> vPuestos = JsonConvert.DeserializeObject<List<E_SELECTOR_PUESTO>>(pPuestos);

            if (vPuestos.Count > 0)
                AgregarEvaluados(new XElement("PUESTOS", vPuestos.Select(s => new XElement("PUESTO", new XAttribute("ID_PUESTO", s.idPuesto)))));
        }

        protected void AgregarEvaluadosPorArea(string pAreas)
        {
            List<E_SELECTOR_AREA> vAreas = JsonConvert.DeserializeObject<List<E_SELECTOR_AREA>>(pAreas);

            if (vAreas.Count > 0)
                AgregarEvaluados(new XElement("AREAS", vAreas.Select(s => new XElement("AREA", new XAttribute("ID_AREA", s.idArea)))));
        }

        protected void AgregarPeriodos(string pPeriodos, string pClTipo)
        {
            List<E_SELECTOR_PERIODO> vPeriodos = JsonConvert.DeserializeObject<List<E_SELECTOR_PERIODO>>(pPeriodos);
            TableroControlNegocio nTablero = new TableroControlNegocio();
            XElement vXmlPeriodos;

            if (vPeriodos.Count > 0)
            {
                vXmlPeriodos = new XElement("PERIODOS", vPeriodos.Select(s => new XElement("PERIODO", new XAttribute("ID_PERIODO", s.idPeriodo), new XAttribute("NB_PERIODO", s.nbPeriodo))));

                E_RESULTADO vResultado = nTablero.InsertarPeriodosReferenciaTableroControl(vIdPeriodo, pClTipo, vXmlPeriodos.ToString(), vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                {
                    if (pClTipo.Equals("FD_EVALUACION"))
                        grdCompetencia.Rebind();

                    if (pClTipo.Equals("EO_DESEMPENO"))
                        grdDesempeno.Rebind();

                    if (pClTipo.Equals("EO_CLIMA"))
                        grdClima.Rebind();

                    if (pClTipo.Equals("TABULADOR"))
                        grdSalarial.Rebind();

                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
                }
                else
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
                }

            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                if (Request.Params["pIdPeriodo"] != null)
                {
                    vIdPeriodo = int.Parse(Request.Params["pIdPeriodo"].ToString());
                    CargarDatos();
                }
            }
        }

        protected void grdEvaluados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            TableroControlNegocio nTableroControl = new TableroControlNegocio();
            grdEvaluados.DataSource = nTableroControl.ObtieneTableroControlEvaluados(pIdPeriodo: vIdPeriodo);
        }

        protected void btnEliminarEvaluado_Click(object sender, EventArgs e)
        {
            XElement vXmlEvaluados = new XElement("EMPLEADOS");
            foreach (GridDataItem item in grdEvaluados.SelectedItems)
                vXmlEvaluados.Add(new XElement("EMPLEADO", new XAttribute("ID_EMPLEADO", item.GetDataKeyValue("ID_EMPLEADO").ToString())));

            TableroControlNegocio nTableroControl = new TableroControlNegocio();
            E_RESULTADO vResultado = nTableroControl.EliminarEvaluadoresTableroControl(vIdPeriodo, vXmlEvaluados.ToString(), vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
            {
                grdEvaluados.Rebind();
            }
        }

        protected void ramConfiguracionTablero_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            E_SELECTOR vSeleccion = new E_SELECTOR();
            string pParameter = e.Argument;

            if (pParameter != null)
                vSeleccion = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);

            if (vSeleccion.clTipo == "EVALUADO")
                AgregarEvaluadosPorEmpleado(vSeleccion.oSeleccion.ToString());

            if (vSeleccion.clTipo == "PUESTO")
                AgregarEvaluadosPorPuesto(vSeleccion.oSeleccion.ToString());

            if (vSeleccion.clTipo == "AREA")
                AgregarEvaluadosPorArea(vSeleccion.oSeleccion.ToString());

            if (vSeleccion.clTipo == "FD_EVALUACION")
                AgregarPeriodos(vSeleccion.oSeleccion.ToString(), vSeleccion.clTipo);

            if (vSeleccion.clTipo == "EO_DESEMPENO")
                AgregarPeriodos(vSeleccion.oSeleccion.ToString(), vSeleccion.clTipo);

            if (vSeleccion.clTipo == "EO_CLIMA")
                AgregarPeriodos(vSeleccion.oSeleccion.ToString(), vSeleccion.clTipo);

            if (vSeleccion.clTipo == "TABULADOR")
                AgregarPeriodos(vSeleccion.oSeleccion.ToString(), vSeleccion.clTipo);

        }

        protected void grdCandidatos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            TableroControlNegocio nTableroControl = new TableroControlNegocio();
            grdCandidatos.DataSource = nTableroControl.ObtenerRelacionEvaluadosCandidatos(vIdPeriodo);
        }

        protected void grdCompetenci_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            TableroControlNegocio nTableroControl = new TableroControlNegocio();
            grdCompetencia.DataSource = nTableroControl.ObtenerPeriodosReferenciaTableroControl(vIdPeriodo, "FD_EVALUACION");
        }

        protected void btnEliminarPeriodoFyd_Click(object sender, EventArgs e)
        {
            XElement vXmlPeriodo = new XElement("TABLERO");
            TableroControlNegocio nTableroControl = new TableroControlNegocio();


            foreach (GridDataItem item in grdCompetencia.SelectedItems)
                vXmlPeriodo.Add(new XElement("PERIODO", new XAttribute("ID_PERIODO_REFERENCIA", item.GetDataKeyValue("ID_PERIODO_REFERENCIA_TABLERO").ToString())));


            E_RESULTADO vResultado = nTableroControl.EliminaPeriodosReferenciaTableroControl(vIdPeriodo, vXmlPeriodo.ToString());
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
            {
                grdCompetencia.Rebind();
            }
        }

        protected void grdDesempeno_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            TableroControlNegocio nTableroControl = new TableroControlNegocio();
            grdDesempeno.DataSource = nTableroControl.ObtenerPeriodosReferenciaTableroControl(vIdPeriodo, "EO_DESEMPENO");
        }

        protected void btnEliminarPeriodoDesempeno_Click(object sender, EventArgs e)
        {
            XElement vXmlPeriodo = new XElement("TABLERO");
            TableroControlNegocio nTableroControl = new TableroControlNegocio();

            foreach (GridDataItem item in grdDesempeno.SelectedItems)
                vXmlPeriodo.Add(new XElement("PERIODO", new XAttribute("ID_PERIODO_REFERENCIA", item.GetDataKeyValue("ID_PERIODO_REFERENCIA_TABLERO").ToString())));


            E_RESULTADO vResultado = nTableroControl.EliminaPeriodosReferenciaTableroControl(vIdPeriodo, vXmlPeriodo.ToString());
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
            {
                grdDesempeno.Rebind();
            }
        }

        protected void grdClima_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            TableroControlNegocio nTableroControl = new TableroControlNegocio();
            grdClima.DataSource = nTableroControl.ObtenerPeriodosReferenciaTableroControl(vIdPeriodo, "EO_CLIMA");
        }

        protected void btnEliminarPeriodoClima_Click(object sender, EventArgs e)
        {
            XElement vXmlPeriodo = new XElement("TABLERO");
            TableroControlNegocio nTableroControl = new TableroControlNegocio();

            foreach (GridDataItem item in grdClima.SelectedItems)
                vXmlPeriodo.Add(new XElement("PERIODO", new XAttribute("ID_PERIODO_REFERENCIA", item.GetDataKeyValue("ID_PERIODO_REFERENCIA_TABLERO").ToString())));


            E_RESULTADO vResultado = nTableroControl.EliminaPeriodosReferenciaTableroControl(vIdPeriodo, vXmlPeriodo.ToString());
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
            {
                grdClima.Rebind();
            }
        }

        protected void grdSalarial_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            TableroControlNegocio nTableroControl = new TableroControlNegocio();
            grdSalarial.DataSource = nTableroControl.ObtenerPeriodosReferenciaTableroControl(vIdPeriodo, "TABULADOR");
        }

        protected void btnEliminarTabulador_Click(object sender, EventArgs e)
        {
            XElement vXmlPeriodo = new XElement("TABLERO");
            TableroControlNegocio nTableroControl = new TableroControlNegocio();

            foreach (GridDataItem item in grdSalarial.SelectedItems)
                vXmlPeriodo.Add(new XElement("PERIODO", new XAttribute("ID_PERIODO_REFERENCIA", item.GetDataKeyValue("ID_PERIODO_REFERENCIA_TABLERO").ToString())));

            E_RESULTADO vResultado = nTableroControl.EliminaPeriodosReferenciaTableroControl(vIdPeriodo, vXmlPeriodo.ToString());
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
            {
                grdSalarial.Rebind();
            }
        }
    }
}