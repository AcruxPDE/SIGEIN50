using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SIGE.Entidades.Externas;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Xml.XPath;
using WebApp.Comunes;

namespace SIGE.WebApp.FYD
{
    public partial class PeriodoEvaluacion : System.Web.UI.Page
    {

        #region Variables
        
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private string vNbFirstRadEditorTagName = "p";

        private int vIdPeriodo
        {
            get { return (int)ViewState["vs_vIdPeriodo"]; }
            set { ViewState["vs_vIdPeriodo"] = value; }
        }

        private bool vPeriodoPVC
        {
            get { return (bool)ViewState["vs_vPeriodoPVC"]; }
            set { ViewState["vs_vPeriodoPVC"] = value; }
        }

        private bool vPeriodoPS
        {
            get { return (bool)ViewState["vs_vPeriodoPS"]; }
            set { ViewState["vs_vPeriodoPS"] = value; }
        }


        private int vIdEmpleadoPVC
        {
            get { return (int)ViewState["vs_vIdEmpleadoPVC"]; }
            set { ViewState["vs_vIdEmpleadoPVC"] = value; }
        }

        private int vIdPuestoPS
        {
            get { return (int)ViewState["vs_vIdPuestoPS"]; }
            set { ViewState["vs_vIdPuestoPS"] = value; }
        }

        private string vIdsPuestosPVC
        {
            get { return ViewState["vs_vIdsPuestosPVC"].ToString(); }
            set { ViewState["vs_vIdsPuestosPVC"] = value; }
        }

        private string vIdsEmpleadosPS
        {
            get { return ViewState["vs_vIdsEmpleadosPS"].ToString(); }
            set { ViewState["vs_vIdsEmpleadosPS"] = value; }
        }


        private string vTipoTarea
        {
            get { return (string)ViewState["vs_vTipoTarea"];}
            set { ViewState["vs_vTipoTarea"] = value; }
        }

        #endregion

        #region Funciones


        public bool obtenerValorSeleccionado(Telerik.Web.UI.RadButton boton)
        {
            return (boton.SelectedToggleStateIndex == 0 ? false : true);
        }

        private XElement EditorContentToXml(string pNbNodoRaiz, string pDsContenido, string pNbTag)
        {
            return XElement.Parse(EncapsularRadEditorContent(XElement.Parse(String.Format("<{1}>{0}</{1}>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(pDsContenido)), pNbNodoRaiz)), pNbNodoRaiz));
        }

        private string EncapsularRadEditorContent(XElement nodo, string nbNodo)
        {
            if (nodo.Elements().Count() == 1)
                return EncapsularRadEditorContent((XElement)nodo.FirstNode, nbNodo);
            else
            {
                nodo.Name = nbNodo;
                return nodo.ToString();
            }
        }

        private void SeguridadProcesos()
        {
            btnGuardar.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.A.A.B");
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            string vClEstadoPeriodo = "ABIERTO";
            if (!IsPostBack)
            {
                bool vFgHabilitarEdicion = true;
                bool vFgAutoevaluacion = false;
                bool vFgSupervisor = false;
                bool vFgSubordinados = false;
                bool vFgInterrelacionados = false;
                bool vFgOtrosEvaluadores = false;
                if (Request.QueryString["PeriodoId"] != null)
                {
                    vIdPeriodo = int.Parse(Request.QueryString["PeriodoId"]);

                    PeriodoNegocio nPeriodo = new PeriodoNegocio();
                    var vPeriodo = nPeriodo.ObtienePeriodosEvaluacion(pIdPeriodo: vIdPeriodo).FirstOrDefault();
                    txtNbPeriodo.Text = vPeriodo.NB_PERIODO;
                    txtDsPeriodo.Text = vPeriodo.DS_PERIODO;
                    vClEstadoPeriodo = vPeriodo.CL_ESTADO_PERIODO;

                    vFgHabilitarEdicion = !(bool)vPeriodo.FG_TIENE_EVALUADORES;
                    vFgAutoevaluacion = vPeriodo.FG_AUTOEVALUACION;
                    vFgSupervisor = vPeriodo.FG_SUPERVISOR;
                    vFgSubordinados = vPeriodo.FG_SUBORDINADOS;
                    vFgInterrelacionados = vPeriodo.FG_INTERRELACIONADOS;
                    vFgOtrosEvaluadores = vPeriodo.FG_OTROS_EVALUADORES;

                    if (vPeriodo.DS_NOTAS != null)
                    {
                        if (vPeriodo.DS_NOTAS.Contains("DS_NOTA"))
                        {
                            txtDsNotas.Content = Utileria.MostrarNotas(vPeriodo.DS_NOTAS);
                        }
                        else
                        {
                            XElement vRequerimientos = XElement.Parse(vPeriodo.DS_NOTAS);
                            if (vRequerimientos != null)
                            {
                                vRequerimientos.Name = vNbFirstRadEditorTagName;
                                txtDsNotas.Content = vRequerimientos.ToString();
                            }
                        }
                    }

                        SeguridadProcesos();
                }
                else
                {
                    vIdPeriodo = 0;
                }

                if (Request.Params["TipoTarea"] != null)
                {
                    vTipoTarea = Request.Params["TipoTarea"].ToString();
                    if (vTipoTarea == "COPIA")
                        vClEstadoPeriodo = "ABIERTO";
                }

                btnAutoevaluacionTrue.Enabled = vFgHabilitarEdicion;
                btnAutoevaluacionFalse.Enabled = vFgHabilitarEdicion;
                btnSupervisorTrue.Enabled = vFgHabilitarEdicion;
                btnSupervisorFalse.Enabled = vFgHabilitarEdicion;
                btnSubordinadosTrue.Enabled = vFgHabilitarEdicion;
                btnSubordinadosFalse.Enabled = vFgHabilitarEdicion;
                btnInterrelacionadosTrue.Enabled = vFgHabilitarEdicion;
                btnInterrelacionadosFalse.Enabled = vFgHabilitarEdicion;
                btnOtrosTrue.Enabled = vFgHabilitarEdicion;
                btnOtrosFalse.Enabled = vFgHabilitarEdicion;

                btnAutoevaluacionTrue.Checked = vFgAutoevaluacion;
                btnAutoevaluacionFalse.Checked = !vFgAutoevaluacion;
                btnSupervisorTrue.Checked = vFgSupervisor;
                btnSupervisorFalse.Checked = !vFgSupervisor;
                btnSubordinadosTrue.Checked = vFgSubordinados;
                btnSubordinadosFalse.Checked = !vFgSubordinados;
                btnInterrelacionadosTrue.Checked = vFgInterrelacionados;
                btnInterrelacionadosFalse.Checked = !vFgInterrelacionados;
                btnOtrosTrue.Checked = vFgOtrosEvaluadores;
                btnOtrosFalse.Checked = !vFgOtrosEvaluadores;
                vPeriodoPS = false;
                vPeriodoPVC = false;
                if (Request.QueryString["evaluadoPVC"] != null)
                {
                    //Si el periodo se va a crear desde plan de vida y carrera
                    vPeriodoPVC = true;
                    txtDsPeriodo.Text = Request.QueryString["evaluadoPVC"];
                    txtDsPeriodo.Enabled = false;
                    vIdsPuestosPVC = Request.QueryString["idsPuestosPVC"];
                    vIdEmpleadoPVC = int.Parse(Request.QueryString["idEvaluadoPVC"]);
                }

                if (Request.QueryString["evaluadoPs"] != null)
                {
                    //Si el periodo se va a crear desde plan de sucesión
                    vPeriodoPS = true;
                    txtDsPeriodo.Text = Request.QueryString["evaluadoPS"];
                    txtDsPeriodo.Enabled = false;
                    vIdsEmpleadosPS = Request.QueryString["idsEmpleadosPS"];
                    vIdPuestoPS = int.Parse(Request.QueryString["idPuestoPS"]);
                }

            }
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            txtClEstado.Text = vClEstadoPeriodo;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            PeriodoNegocio per = new PeriodoNegocio();
            bool vFgAutoEvaluacion = btnAutoevaluacionTrue.Checked;
            bool vFgSupervisor = btnSupervisorTrue.Checked;
            bool vFgSubordinados = btnSubordinadosTrue.Checked;
            bool vFgInterrelacionados = btnInterrelacionadosTrue.Checked;
            bool vFgOtros = btnOtrosTrue.Checked;

            if (vFgAutoEvaluacion == false && vFgSupervisor == false && vFgSubordinados == false && vFgInterrelacionados == false && vFgOtros == false)
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Se debe seleccionar por lo menos un nivel para crear los cuestionarios", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, "");
                return;
            }

            if (vTipoTarea != "COPIA")
            {
                //string vDsNotas = txtDsNotas.Content.Replace("&lt;","");
                XElement nodoPrincipal = Utileria.GuardarNotas(txtDsNotas.Content, "XML_NOTAS"); //new XElement("XML_NOTAS", EditorContentToXml("DS_NOTAS", vDsNotas, vNbFirstRadEditorTagName));
                string vAccion = (vIdPeriodo != 0 ? "A" : "I");
                var resultado = per.InsertaActualizaPeriodoEvaluacionCompetencias(vIdPeriodo, txtNbPeriodo.Text, txtNbPeriodo.Text, txtDsPeriodo.Text, true, "", txtClEstado.Text, "FD_EVALUACION", nodoPrincipal.ToString(), null, null, null, vFgAutoEvaluacion, vFgSupervisor, vFgSubordinados, vFgInterrelacionados, vFgOtros, vPeriodoPVC, vClUsuario, vNbPrograma, vAccion);
                //resultado obtener el idCreado
                var idCreado = 0;
                var esNumero = int.TryParse(resultado.MENSAJE.Where(x => x.CL_IDIOMA == "ID_PERIODO").FirstOrDefault().DS_MENSAJE, out idCreado);
                vIdPeriodo = idCreado;
                //SI ES CREADO DESDE PVC CREAR CONFIGURACION
                if (vPeriodoPVC)
                    GenerarConfiguracionPVC();

                if (vPeriodoPS)
                    GenerarConfiguracionPS();


                //UtilMensajes.MensajeResultadoDB(rwmAlertas, resultado.MENSAJE[0].DS_MENSAJE.ToString(), resultado.CL_TIPO_ERROR, 400, 150, "sendDataToParent(" + vIdPeriodo + ")");
                if(vAccion == "I")
                UtilMensajes.MensajeResultadoDB(rwmAlertas, resultado.MENSAJE[0].DS_MENSAJE.ToString(), resultado.CL_TIPO_ERROR, 400, 150, "closeWindow");
                else
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, resultado.MENSAJE[0].DS_MENSAJE.ToString(), resultado.CL_TIPO_ERROR, 400, 150, "closeWindowEdit");
            }
            else
            {
                XElement nodoPrincipal = Utileria.GuardarNotas(txtDsNotas.Content, "XML_NOTAS"); //new XElement("XML_NOTAS", EditorContentToXml("DS_NOTAS", vDsNotas, vNbFirstRadEditorTagName));
                string vAccion = "I";
                var resultado = per.InsertarCopiaPeriodoEvaluacionCompetencias(vIdPeriodo, txtNbPeriodo.Text, txtNbPeriodo.Text, txtDsPeriodo.Text, true, "", txtClEstado.Text, "FD_EVALUACION", nodoPrincipal.ToString(), null, null, null, vFgAutoEvaluacion, vFgSupervisor, vFgSubordinados, vFgInterrelacionados, vFgOtros, vPeriodoPVC, vClUsuario, vNbPrograma, vAccion);
                //resultado obtener el idCreado
                var idCreado = 0;
                var esNumero = int.TryParse(resultado.MENSAJE.Where(x => x.CL_IDIOMA == "ID_PERIODO").FirstOrDefault().DS_MENSAJE, out idCreado);
                vIdPeriodo = idCreado;
                //UtilMensajes.MensajeResultadoDB(rwmAlertas, resultado.MENSAJE[0].DS_MENSAJE.ToString(), resultado.CL_TIPO_ERROR, 400, 150, "sendDataToParent(" + vIdPeriodo + ")");
                UtilMensajes.MensajeResultadoDB(rwmAlertas, resultado.MENSAJE[0].DS_MENSAJE.ToString(), resultado.CL_TIPO_ERROR, 400, 150, "closeWindow");
            }
        }

        public void GenerarConfiguracionPVC()
        {
            GuardarConfiguracionTipo();
            AgregarEvaluadoPVC();
            AgregarPuestosParaEvaluar();
        }

        public void GenerarConfiguracionPS()
        {
            GuardarConfiguracionTipo();
            AgregarEvaluadosPS();
            AgregarPuestoParaEvaluar();
        }

        public void GuardarConfiguracionTipo()
        {
            XElement vXmlConfiguracion = new XElement("CONFIGURACION");
            XElement vXmlTipoEvaluacion = new XElement("TIPO_EVALUACION");
             vXmlTipoEvaluacion.Add(new XAttribute("CL_TIPO_EVALUACION", "PUESTO_CARRERA"),
                        new XAttribute("FG_PUESTO_ACTUAL",  "0"),
                        new XAttribute("FG_OTROS_PUESTOS", "1" ),
                        new XAttribute("FG_RUTA_VERTICAL", "0"),
                        new XAttribute("FG_RUTA_VERTICAL_ALTERNATIVA", "0"),
                        new XAttribute("FG_RUTA_HORIZONTAL", "0")
                        );
             vXmlConfiguracion.Add(vXmlTipoEvaluacion);
             PeriodoNegocio nPeriodo = new PeriodoNegocio();
             E_RESULTADO vResultado = nPeriodo.ActualizaConfiguracionPeriodo(vIdPeriodo, vXmlConfiguracion, vClUsuario, vNbPrograma);
        }

        protected void AgregarEvaluadoPVC()
        {
            XElement pXmlElementos = (new XElement("EMPLEADOS", new XElement("EMPLEADO", new XAttribute("ID_EMPLEADO", vIdEmpleadoPVC))));

            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            E_RESULTADO vResultado = nPeriodo.InsertaEvaluados(vIdPeriodo, pXmlElementos, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            //if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
            //{
            //}            
        }

        protected void AgregarPuestosParaEvaluar()
        {
            Char delimiter = ',';
            var vPuestos = vIdsPuestosPVC.Split(delimiter);
            XElement vXmlEmpleados = new XElement("EMPLEADOS", new XElement("EMPLEADO", new XAttribute("ID_EMPLEADO", vIdEmpleadoPVC)));
            XElement vXmlPuestos = new XElement("PUESTOS", vPuestos.Distinct().Select(s => new XElement("PUESTO", new XAttribute("ID_PUESTO", s.ToString()))));

            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            E_RESULTADO vResultado = nPeriodo.InsertaActualizaOtrosPuestosEvaluados(vIdPeriodo, vXmlEmpleados, vXmlPuestos, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            
        }

        protected void AgregarEvaluadosPS()
        {
            Char delimiter = ',';
            var vEmpleados = vIdsEmpleadosPS.Split(delimiter);
            XElement pXmlElementos = new XElement("EMPLEADOS", vEmpleados.Distinct().Select(s => new XElement("EMPLEADO", new XAttribute("ID_EMPLEADO", s.ToString()))));

            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            E_RESULTADO vResultado = nPeriodo.InsertaEvaluados(vIdPeriodo, pXmlElementos, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

        }

        protected void AgregarPuestoParaEvaluar()
        {
            Char delimiter = ',';
            var vEmpleados = vIdsEmpleadosPS.Split(delimiter);
            XElement vXmlEmpleados = new XElement("EMPLEADOS", vEmpleados.Distinct().Select(s => new XElement("EMPLEADO", new XAttribute("ID_EMPLEADO", s.ToString()))));
            XElement vXmlPuestos = new XElement("PUESTOS", new XElement("PUESTO", new XAttribute("ID_PUESTO", vIdPuestoPS)));
           
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            E_RESULTADO vResultado = nPeriodo.InsertaActualizaOtrosPuestosEvaluados(vIdPeriodo, vXmlEmpleados, vXmlPuestos, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

        }

    }
}