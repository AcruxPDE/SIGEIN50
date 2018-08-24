using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.FYD
{
    public partial class ConfiguracionPeriodo : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int vIdPeriodoV
        {
            get { return (int)ViewState["vs_vIderiodo"];}
            set { ViewState["vs_vIderiodo"] = value; }
        }

        public string vFgContestados
        {
            get { return (string)ViewState["vs_vFgContestados"]; }
            set { ViewState["vs_vFgContestados"] = value; }
        }

        public int vFgPuestosComparacion {
            get { return (int)ViewState["vs_cp_fg_puestos_comparacion"]; }
            set { ViewState["vs_cp_fg_puestos_comparacion"] = value; }
        }

        public E_PERIODO_EVALUACION vPeriodo
        {
            get { return (E_PERIODO_EVALUACION)ViewState["vs_vPeriodo"]; }
            set { ViewState["vs_vPeriodo"] = value; }
        }

        //decimal vPrAutoevaluacion;
        //decimal vPrSuperior;
        //decimal vPrSubordinado;
        //decimal vPrInterrelacionado;
        //decimal vPrOtros;
        //decimal vPrTotal;
        //decimal vPrGenericas;
        //decimal vPrEspecificas;
        //decimal vPrInstitucionales;
        //decimal vPrTotalCompetencias;

        public int ValidarPuestosComparacion()
        {
            int vResultado = 0;

            if (grdEvaluados.MasterTableView.Items.Count == 0)
            {
                vResultado = 0;
            }
            else
            {
                foreach (GridDataItem item in grdEvaluados.Items)
                {

                    if (item.OwnerTableView.Name == "Parent" & !item.HasChildItems)
                    {
                        vResultado++;
                    }
                }
            }

            vFgPuestosComparacion = vResultado;
            return vResultado;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                vFgPuestosComparacion = 0;
                int vIdPeriodoQS = 0;

                int.TryParse(Request.QueryString["PeriodoId"], out vIdPeriodoQS);
                vIdPeriodoV = vIdPeriodoQS;

                ObtenerDatosPeriodo(vIdPeriodoQS);
                CargarDatosConfiguracion();

                if (vPeriodo.FG_CREADO_POR_PVC)
                    aplicarConfiguracionPeriodoPVC();

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

        protected void AgregarEvaluados(XElement pXmlElementos)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            E_RESULTADO vResultado = nPeriodo.InsertaEvaluados(vPeriodo.ID_PERIODO ?? 0, pXmlElementos, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
            {
                grdEvaluados.Rebind();

                if (vPeriodo.FG_OTROS_PUESTOS)
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, "Proceso exitoso. <br />Los evaluados no tienen puesto a evaluar por default. Por favor, selecciona los puestos por cada evaluado.", E_TIPO_RESPUESTA_DB.SUCCESSFUL, 400, 200, pCallBackFunction: null);
                }
                else
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
                }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
            }
        }

        protected void AgregarOtrosEvaluadoresInventario(string pOtroEvaluador)
        {
            List<E_SELECTOR_EMPLEADO> vOtrosEvaluadores = JsonConvert.DeserializeObject<List<E_SELECTOR_EMPLEADO>>(pOtroEvaluador);

            if (vOtrosEvaluadores.Count > 0)
            {
                XElement vXmlOtrosEvaluadores = new XElement("EVALUADORES", vOtrosEvaluadores.Select(s => new XElement("EVALUADOR", new XAttribute("ID_EVALUADOR", s.idEmpleado))));
                PeriodoNegocio nPeriodo = new PeriodoNegocio();
                E_RESULTADO vResultado = nPeriodo.InsertaOtrosEvaluadoresInventario(vPeriodo.ID_PERIODO ?? 0, vXmlOtrosEvaluadores, chkFgOtroEvaluadorTodos.Checked, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

                if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                    grdEvaluadoresExternos.Rebind();
            }
        }

        protected void AgregarPuestosParaEvaluar(string pOtrosPuestos)
        {
            dynamic vData = JObject.Parse(pOtrosPuestos);

            List<E_SELECTOR_EMPLEADO> vEmpleados = new List<E_SELECTOR_EMPLEADO>();
            List<E_SELECTOR_PUESTO> vPuestos = new List<E_SELECTOR_PUESTO>();

            if (vData.lstEmpleados is JArray)
                vEmpleados = vData.lstEmpleados.ToObject<List<E_SELECTOR_EMPLEADO>>();

            if (vData.lstPuestos is JArray)
                vPuestos = vData.lstPuestos.ToObject<List<E_SELECTOR_PUESTO>>();

            XElement vXmlEmpleados = new XElement("EMPLEADOS", vEmpleados.Select(s => new XElement("EMPLEADO", new XAttribute("ID_EMPLEADO", s.idEmpleado))));
            XElement vXmlPuestos = new XElement("PUESTOS", vPuestos.Select(s => new XElement("PUESTO", new XAttribute("ID_PUESTO", s.idPuesto))));

            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            E_RESULTADO vResultado = nPeriodo.InsertaActualizaOtrosPuestosEvaluados(vPeriodo.ID_PERIODO ?? 0, vXmlEmpleados, vXmlPuestos, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                grdEvaluados.Rebind();
        }

        protected void ObtenerDatosPeriodo(int vIdPeriodo)
        {
            PeriodoNegocio oPeriodo = new PeriodoNegocio();
            SPE_OBTIENE_FYD_PERIODO_EVALUACION_Result vPer = oPeriodo.ObtienePeriodoEvaluacion(vIdPeriodo);

            vPeriodo = new E_PERIODO_EVALUACION()
            {
                CL_ESTADO = vPer.CL_ESTADO_PERIODO,
                CL_TIPO_EVALUACION = vPer.CL_TIPO_EVALUACION,
                CL_TIPO_PERIODO = vPer.CL_TIPO_PERIODO,
                FE_PERIODO = vPer.FE_INICIO,
                FG_AUTOEVALUACION = vPer.FG_AUTOEVALUACION,
                FG_COMPETENCIAS_ESPECIFICAS = vPer.FG_ESPECIFICAS,
                FG_COMPETENCIAS_GENERICAS = vPer.FG_GENERICAS,
                FG_COMPETENCIAS_INSTITUCIONALES = vPer.FG_INSTITUCIONALES,
                PR_COMPETENCIAS_ESPECIFICAS = vPer.PR_ESPECIFICAS,
                PR_COMPETENCIAS_GENERICAS = vPer.PR_GENERICAS,
                PR_COMPETENCIAS_INSTITUCIONALES = vPer.PR_INSTITUCIONALES,
                FG_INTERRELACIONADOS = vPer.FG_INTERRELACIONADOS,
                FG_OTROS_EVALUADORES = vPer.FG_OTROS_EVALUADORES,
                FG_OTROS_PUESTOS = vPer.FG_OTROS_PUESTOS,
                FG_PUESTO_ACTUAL = vPer.FG_PUESTO_ACTUAL,
                FG_RUTA_HORIZONTAL = vPer.FG_RUTA_HORIZONTAL,
                FG_RUTA_VERTICAL = vPer.FG_RUTA_VERTICAL,
                FG_RUTA_VERTICAL_ALTERNATIVA = vPer.FG_RUTA_VERTICAL_ALTERNATIVA,
                FG_SUBORDINADOS = vPer.FG_SUBORDINADOS,
                FG_SUPERIOR = vPer.FG_SUPERVISOR,
                ID_PERIODO = vPer.ID_PERIODO,
                NB_PERIODO = vPer.NB_PERIODO,
                DS_PERIODO = vPer.DS_PERIODO,
                PR_AUTOEVALUACION = vPer.PR_AUTOEVALUACION,
                PR_INTERRELACIONADOS = vPer.PR_INTERRELACIONADOS,
                PR_OTROS_EVALUADORES = vPer.PR_OTROS_EVALUADORES,
                PR_SUBORDINADOS = vPer.PR_SUBORDINADOS,
                PR_SUPERIOR = vPer.PR_SUPERVISOR,
                XML_INSTRUCCIONES_DE_LLENADO = vPer.XML_INSTRUCCIONES_LLENADO,
                XML_MENSAJE_INICIAL = vPer.XML_MENSAJE_INICIAL,
                FG_PONDERACION_COMPETENCIAS = vPer.FG_PONDERACION_COMPETENCIAS,
                FG_PONDERACION_EVALUADORES = vPer.FG_PONDERACION_EVALUADORES,
                FG_CREADO_POR_PVC = vPer.FG_CREADO_POR_PVC
            };

            if (vPer.CL_TIPO_EVALUACION.Equals(E_CL_TIPO_EVALUACION.COMPETENCIAS_OTRAS.ToString()) && vPer.XML_OTRAS_COMPETENCIAS != null)
            {
                XElement vLstCompetencias = XElement.Parse(vPer.XML_OTRAS_COMPETENCIAS);
                if (vLstCompetencias != null && vLstCompetencias.Descendants("COMPETENCIA_ESPECIFICA") != null)
                {
                    vPeriodo.LS_OTRAS_COMPETENCIAS = vLstCompetencias.Descendants("COMPETENCIA_ESPECIFICA").Select(s => new E_COMPETENCIAS()
                    {
                        ID_COMPETENCIA = UtilXML.ValorAtributo<int>(s.Attribute("ID_COMPETENCIA")),
                        CL_COMPETENCIA = UtilXML.ValorAtributo<string>(s.Attribute("CL_COMPETENCIA")),
                        NB_COMPETENCIA = UtilXML.ValorAtributo<string>(s.Attribute("NB_COMPETENCIA"))
                    }).ToList();
                }
            }
            else
                vPeriodo.LS_OTRAS_COMPETENCIAS = new List<E_COMPETENCIAS>();

            if (vPer.FG_INTERRELACIONADOS && !String.IsNullOrEmpty(vPer.XML_CAMPOS_ADICIONALES))
            {
                XElement vLstCamposAdicionales = XElement.Parse(vPer.XML_CAMPOS_ADICIONALES);
                vPeriodo.LS_CAMPOS_COMUNES = vLstCamposAdicionales.Element("INTERRELACIONADOS").Elements("CAMPO").Select(s => new E_CAMPO()
                {
                    ID_CAMPO = UtilXML.ValorAtributo<int>(s.Attribute("ID_CAMPO")),
                    NB_CAMPO = UtilXML.ValorAtributo<string>(s.Attribute("NB_CAMPO"))
                }).ToList();
            }
            else
                vPeriodo.LS_CAMPOS_COMUNES = new List<E_CAMPO>();
        }

        protected void CargarDatosConfiguracion()
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            List<SPE_OBTIENE_FYD_CUESTIONARIOS_EVALUADOS_Result> vLstEvaluadosCuestionarios = nPeriodo.ObtieneEvaluadosCuestionarios(vPeriodo.ID_PERIODO ?? 0, ContextoUsuario.oUsuario.ID_EMPRESA);
            bool fgHabilitaPonderacion= !vLstEvaluadosCuestionarios.Any(a => a.NO_CUESTIONARIOS > 0);

            txtIdPeriodo.InnerText = vPeriodo.NB_PERIODO;
            txtNbPeriodo.InnerText = vPeriodo.DS_PERIODO;


            chkFgEvaluadorAutoevaluacion.Checked = vPeriodo.FG_AUTOEVALUACION;
            chkFgEvaluadorSubordinados.Checked = vPeriodo.FG_SUBORDINADOS;
            chkFgEvaluadorSupervisor.Checked = vPeriodo.FG_SUPERIOR;
            chkFgEvaluadorInterrelacionados.Checked = vPeriodo.FG_INTERRELACIONADOS;
            chkFgEvaluadorOtros.Checked = vPeriodo.FG_OTROS_EVALUADORES;
            chkFgPonderacionCompetencia.Checked = vPeriodo.FG_PONDERACION_COMPETENCIAS;
            chkFgPonderacionCompetencia.Enabled = fgHabilitaPonderacion;
            chkFgPonderarEvaluadores.Checked = vPeriodo.FG_PONDERACION_EVALUADORES;
            chkFgPonderarEvaluadores.Enabled = fgHabilitaPonderacion;

            if (chkFgPonderarEvaluadores.Checked == true)
            {
                divPonderarEvaluadores.Style.Value = String.Format("display: {0}", "block");
                //vPrAutoevaluacion = vPeriodo.FG_AUTOEVALUACION ? vPeriodo.PR_AUTOEVALUACION : 20;
                //vPrSuperior = vPeriodo.FG_SUPERIOR ? vPeriodo.PR_SUPERIOR : 20;
                //vPrSubordinado = vPeriodo.FG_SUBORDINADOS ? vPeriodo.PR_SUBORDINADOS : 20;
                //vPrInterrelacionado = vPeriodo.FG_INTERRELACIONADOS ? vPeriodo.PR_INTERRELACIONADOS : 20;
                //vPrOtros = vPeriodo.FG_OTROS_EVALUADORES ? vPeriodo.PR_OTROS_EVALUADORES : 20;
                //vPrTotal = vPrAutoevaluacion + vPrSuperior + vPrSubordinado + vPrInterrelacionado + vPrOtros;
            }

            if (chkFgPonderacionCompetencia.Checked == true)
            {
                divPonderarCompetencias.Style.Value = String.Format("display: {0}", "block");
                //vPrGenericas = vPeriodo.PR_COMPETENCIAS_GENERICAS;
                //vPrEspecificas = vPeriodo.PR_COMPETENCIAS_ESPECIFICAS;
                //vPrInstitucionales = vPeriodo.PR_COMPETENCIAS_INSTITUCIONALES;
                //vPrTotalCompetencias = vPrGenericas + vPrEspecificas + vPrInstitucionales;

            }

            if (!vPeriodo.FG_OTROS_EVALUADORES)
            {
                rtsConfiguracionPeriodo.Tabs[4].Visible = false;
            }

            divConfiguracionInterrelacionados.Style.Value = String.Format("display: {0}", vPeriodo.FG_INTERRELACIONADOS ? "block" : "none");

            bool vFgAutoevaluacion = vPeriodo.FG_AUTOEVALUACION;
            bool vFgOtros = vPeriodo.FG_SUBORDINADOS || vPeriodo.FG_SUPERIOR || vPeriodo.FG_INTERRELACIONADOS || vPeriodo.FG_OTROS_EVALUADORES;
            bool vFgAmbos = vFgAutoevaluacion && vFgOtros;

            //btnCuestionarioAmbos.Enabled = vFgAmbos;
            //btnCuestionarioAutoevaluacion.Enabled = vFgAmbos;
            //btnCuestionarioOtros.Enabled = vFgAmbos;

            //btnCuestionarioAmbos.Checked = vFgAmbos;
            //btnCuestionarioAutoevaluacion.Checked = vFgAutoevaluacion && !vFgAmbos;
            //btnCuestionarioOtros.Checked = vFgOtros && !vFgAmbos;

            txtPrAutoevaluacion.Enabled = vPeriodo.FG_AUTOEVALUACION;
            txtPrSubordinados.Enabled = vPeriodo.FG_SUBORDINADOS;
            txtPrSuperior.Enabled = vPeriodo.FG_SUPERIOR;
            txtPrInterrelacionados.Enabled = vPeriodo.FG_INTERRELACIONADOS;
            txtPrOtros.Enabled = vPeriodo.FG_OTROS_EVALUADORES;

            decimal vPrAutoevaluacion = vPeriodo.FG_AUTOEVALUACION ? vPeriodo.PR_AUTOEVALUACION : 0;
            decimal vPrSuperior = vPeriodo.FG_SUPERIOR ? vPeriodo.PR_SUPERIOR : 0;
            decimal vPrSubordinado = vPeriodo.FG_SUBORDINADOS ? vPeriodo.PR_SUBORDINADOS : 0;
            decimal vPrInterrelacionado = vPeriodo.FG_INTERRELACIONADOS ? vPeriodo.PR_INTERRELACIONADOS : 0;
            decimal vPrOtros = vPeriodo.FG_OTROS_EVALUADORES ? vPeriodo.PR_OTROS_EVALUADORES : 0;
            decimal vPrTotal = vPrAutoevaluacion + vPrSuperior + vPrSubordinado + vPrInterrelacionado + vPrOtros;

            //if (!fgHabilitaPonderacion)
            //{
                txtPrAutoevaluacion.Value = (double)vPrAutoevaluacion;
                //txtPrAutoevaluacion.Enabled = fgHabilitaPonderacion;
                txtPrSuperior.Value = (double)vPrSuperior;
                //txtPrSuperior.Enabled = fgHabilitaPonderacion;
                txtPrSubordinados.Value = (double)vPrSubordinado;
                //txtPrSubordinados.Enabled = fgHabilitaPonderacion;
                txtPrInterrelacionados.Value = (double)vPrInterrelacionado;
                //txtPrInterrelacionados.Enabled = fgHabilitaPonderacion;
                txtPrOtros.Value = (double)vPrOtros;
                //txtPrOtros.Enabled = fgHabilitaPonderacion;
                txtPrTotal.Value = (double)vPrTotal;
            //}

            decimal vPrGenericas = vPeriodo.PR_COMPETENCIAS_GENERICAS;
            decimal vPrEspecificas = vPeriodo.PR_COMPETENCIAS_ESPECIFICAS;
            decimal vPrInstitucionales = vPeriodo.PR_COMPETENCIAS_INSTITUCIONALES;
            decimal vPrTotalCompetencias = vPrGenericas + vPrEspecificas + vPrInstitucionales;

            txtPrGenericas.Value = (double)vPrGenericas;
            txtPrGenericas.Enabled = fgHabilitaPonderacion;
            txtPrEspecificas.Value = (double)vPrEspecificas;
            txtPrEspecificas.Enabled = fgHabilitaPonderacion;
            txtPrInstitucionales.Value = (double)vPrInstitucionales;
            txtPrInstitucionales.Enabled = fgHabilitaPonderacion;
            txtPrTotalCompetencias.Value = (double)vPrTotalCompetencias;

            txtDsMensajeInicial.Content = vPeriodo.XML_MENSAJE_INICIAL;

            ChangeEnableSeleccionEvaluados(fgHabilitaPonderacion);
            ChangeEnableOtrosEvaluadores(fgHabilitaPonderacion);
            //ChangeEnableCuestionarios();
            ChangeEnableToken();
        }

        protected void ChangeEnableTipoEvaluacion(bool pFgHabilitarTipoEvaluacion)
        {
            bool vFgPermitirEdicion = (vPeriodo.CL_ESTADO != "CERRADO");
            if (vPeriodo.FG_CREADO_POR_PVC)
                vFgPermitirEdicion = false;

            bool vFgEvaluacionPorPuesto = vPeriodo.CL_TIPO_EVALUACION.Equals(E_CL_TIPO_EVALUACION.PUESTO_CARRERA.ToString());
            btnEvaluacionPorPuesto.Checked = vFgEvaluacionPorPuesto;
            btnEvaluacionPorPuesto.Enabled = pFgHabilitarTipoEvaluacion;

            chkFgPuestoActual.Enabled = vFgEvaluacionPorPuesto && pFgHabilitarTipoEvaluacion;
            chkFgOtrosPuestos.Enabled = vFgEvaluacionPorPuesto && pFgHabilitarTipoEvaluacion;
            chkFgRutaVertical.Enabled = vFgEvaluacionPorPuesto && pFgHabilitarTipoEvaluacion;
            chkFgRutaVerticalAlternativa.Enabled = vFgEvaluacionPorPuesto && pFgHabilitarTipoEvaluacion;
            chkFgRutaHorizontalAlternativa.Enabled = vFgEvaluacionPorPuesto && pFgHabilitarTipoEvaluacion;

            chkFgPuestoActual.Checked = vPeriodo.FG_PUESTO_ACTUAL && vFgEvaluacionPorPuesto;
            btnAgregarPuestos.Visible = chkFgOtrosPuestos.Checked = vPeriodo.FG_OTROS_PUESTOS && vFgEvaluacionPorPuesto;
            chkFgRutaVertical.Checked = vPeriodo.FG_RUTA_VERTICAL && vFgEvaluacionPorPuesto;
            chkFgRutaVerticalAlternativa.Checked = vPeriodo.FG_RUTA_VERTICAL_ALTERNATIVA && vFgEvaluacionPorPuesto;
            chkFgRutaHorizontalAlternativa.Checked = vPeriodo.FG_RUTA_HORIZONTAL && vFgEvaluacionPorPuesto;

            bool vFgEvaluacionCompetenciasEstandar = vPeriodo.CL_TIPO_EVALUACION.Equals(E_CL_TIPO_EVALUACION.COMPETENCIA_ESTANDAR.ToString());
            btnEvaluacionPorEstandar.Checked = vFgEvaluacionCompetenciasEstandar;
            btnEvaluacionPorEstandar.Enabled = pFgHabilitarTipoEvaluacion;

            chkFgCompetenciasGenericas.Enabled = vFgEvaluacionCompetenciasEstandar && pFgHabilitarTipoEvaluacion;
            chkFgCompetenciasEspecificas.Enabled = vFgEvaluacionCompetenciasEstandar && pFgHabilitarTipoEvaluacion;
            chkFgCompetenciasInstitucionales.Enabled = vFgEvaluacionCompetenciasEstandar && pFgHabilitarTipoEvaluacion;

            chkFgCompetenciasGenericas.Checked = vPeriodo.FG_COMPETENCIAS_GENERICAS && vFgEvaluacionCompetenciasEstandar;
            chkFgCompetenciasEspecificas.Checked = vPeriodo.FG_COMPETENCIAS_ESPECIFICAS && vFgEvaluacionCompetenciasEstandar;
            chkFgCompetenciasInstitucionales.Checked = vPeriodo.FG_COMPETENCIAS_INSTITUCIONALES && vFgEvaluacionCompetenciasEstandar;

            bool vFgEvaluacionOtrasCompetencias = vPeriodo.CL_TIPO_EVALUACION.Equals(E_CL_TIPO_EVALUACION.COMPETENCIAS_OTRAS.ToString());
            btnEvaluacionPorOtras.Checked = vFgEvaluacionOtrasCompetencias;
            btnEvaluacionPorOtras.Enabled = pFgHabilitarTipoEvaluacion;

            btnEspecificarOtrasCompetencias.Enabled = vFgEvaluacionOtrasCompetencias && pFgHabilitarTipoEvaluacion;
            btnEliminarCompentenciaEspecifica.Enabled = vFgEvaluacionOtrasCompetencias && pFgHabilitarTipoEvaluacion;

            lstCamposInterrelacionados.Enabled = vPeriodo.FG_INTERRELACIONADOS && pFgHabilitarTipoEvaluacion;
            btnEspecificarCamposRelacionados.Enabled = vPeriodo.FG_INTERRELACIONADOS && pFgHabilitarTipoEvaluacion;
            btnEliminarCamposRelacionados.Enabled = vPeriodo.FG_INTERRELACIONADOS && pFgHabilitarTipoEvaluacion;

            lstCompetenciasEspecificas.Items.Clear();
            if (vPeriodo.LS_OTRAS_COMPETENCIAS.Count > 0)
                vPeriodo.LS_OTRAS_COMPETENCIAS.OrderBy(o => o.NB_COMPETENCIA).ToList().ForEach(f => lstCompetenciasEspecificas.Items.Add(new RadListBoxItem(f.NB_COMPETENCIA, f.ID_COMPETENCIA.ToString())));
            else
                lstCompetenciasEspecificas.Items.Add(new RadListBoxItem("No seleccionado", "0"));

            lstCamposInterrelacionados.Items.Clear();
            if (vPeriodo.LS_CAMPOS_COMUNES.Count > 0)
                vPeriodo.LS_CAMPOS_COMUNES.OrderBy(o => o.NB_CAMPO).ToList().ForEach(f => lstCamposInterrelacionados.Items.Add(new RadListBoxItem(f.NB_CAMPO, f.ID_CAMPO.ToString())));
            else
                lstCamposInterrelacionados.Items.Add(new RadListBoxItem("No seleccionado", "0"));

            btnGuardarConfiguracion.Enabled = vFgPermitirEdicion;
            btnGuardarConfiguracionCerrar.Enabled = vFgPermitirEdicion;

            ChangeEnablePonderacion(vFgEvaluacionPorPuesto);

        }

        protected void ChangeEnableMensajeCampos(bool pFgHabilitarMensajeCampos)
        {
            txtDsMensajeInicial.EditModes = pFgHabilitarMensajeCampos ? EditModes.Design : EditModes.Preview;
            btnAgregarCampoAdicional.Enabled = pFgHabilitarMensajeCampos;
            grdCamposAdicionales.MasterTableView.GetColumn("DeleteColumn").Display = pFgHabilitarMensajeCampos;
        }

        protected void ChangeEnablePonderacion(bool pFgHabilitarPonderacion)
        {
            bool vFgPermitirEdicion = (vPeriodo.CL_ESTADO != "CERRADO");
            divPonderacionCompetencias.Style.Value = String.Format("display: {0}", pFgHabilitarPonderacion ? "block" : "none");
            btnGuardarPonderacion.Enabled = vFgPermitirEdicion;
            btnGuardarPonderacionCerrar.Enabled = vFgPermitirEdicion;
        }

        protected void ChangeEnableSeleccionEvaluados(bool fgHabilitaPonderacion)
        {
            bool vFgPermitirEdicion = (vPeriodo.CL_ESTADO != "CERRADO" && fgHabilitaPonderacion != false );
            btnSeleccionPorPersona.Enabled = vFgPermitirEdicion;
            btnSeleccionPorPuesto.Enabled = vFgPermitirEdicion;
            btnSeleccionPorArea.Enabled = vFgPermitirEdicion;
            btnEliminarEvaluado.Enabled = vFgPermitirEdicion;
            btnAgregarPuestos.Enabled = vFgPermitirEdicion;
        }

        protected void ChangeEnableOtrosEvaluadores(bool fgHabilitaPonderacion)
        {
            bool vFgPermitirEdicion = (vPeriodo.CL_ESTADO != "CERRADO" && fgHabilitaPonderacion != false);
            btnAgregarOtroEvaluador.Enabled = vFgPermitirEdicion;
            btnAgregarOtrosEvaluadoresInventario.Enabled = vFgPermitirEdicion;
        }

        protected void ChangeEnableCuestionarios(bool vFgCuestinoariosCreados)
        {

            // = !(grdCuestionarios.Items.Count > 0);
            bool vFgPermitirEdicion = (vPeriodo.CL_ESTADO != "CERRADO");
            //btnAgregarCuestionario.Enabled = vFgPermitirEdicion && vFgCuestinoariosCreados;
            //btnCrearCuestionarios.Enabled = vFgPermitirEdicion && vFgCuestinoariosCreados;
            btnMostrarMatriz.Enabled = vFgPermitirEdicion;
            btnPlaneacionMatriz.Enabled = vFgPermitirEdicion && vFgCuestinoariosCreados;
            //btnRegistroAutorizacion.Enabled = vFgPermitirEdicion;


            PeriodoNegocio oPeriodo = new PeriodoNegocio();
            var vLstValidacion = oPeriodo.ObtenerValidacionAutorizacion(vIdPeriodoV);
            if (vLstValidacion != null)
            {
                if (vLstValidacion.NUM_DOCUMENTOS > 0 && vLstValidacion.NUM_DOCUMENTOS == vLstValidacion.NUM_AUTORIZADOS || vLstValidacion.NUM_CONTESTADOS > 0)
                    btnMostrarMatriz.Enabled = false;

                if (vLstValidacion.NUM_CONTESTADOS > 0 || (vLstValidacion.NUM_DOCUMENTOS > 0 && vLstValidacion.NUM_DOCUMENTOS == vLstValidacion.NUM_AUTORIZADOS))
                    vFgContestados = "SI";
                else
                    vFgContestados = "NO";

            }
        }

        protected void ChangeEnableToken()
        {
            bool vFgPermitirEdicion = (vPeriodo.CL_ESTADO != "CERRADO");
            btnReasignarTodasContrasenas.Enabled = vFgPermitirEdicion;
            btnReasignarContrasena.Enabled = vFgPermitirEdicion;
        }

        protected void grdEvaluados_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            List<SPE_OBTIENE_FYD_EVALUADOS_CONFIGURACION_Result> vLstEvaluados = nPeriodo.ObtieneEvaluados(vPeriodo.ID_PERIODO ?? 0, ContextoUsuario.oUsuario.ID_EMPRESA);
            grdEvaluados.DataSource = vLstEvaluados;

            ChangeEnableTipoEvaluacion(vLstEvaluados.Count.Equals(0));

            //if (vLstEvaluados.Count == 0)
            //{
            //    vFgPuestosComparacion = true;
            //}
            //else
            //{
            //    vFgPuestosComparacion = false;
            //}
            //ValidarPuestosComparacion();
        }

        protected void ramConfiguracionPeriodo_AjaxRequest(object sender, AjaxRequestEventArgs e)
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

            if (vSeleccion.clTipo == "OTROEVALUADOR")
                AgregarOtrosEvaluadoresInventario(vSeleccion.oSeleccion.ToString());

            if (vSeleccion.clTipo == "OTROSPUESTOS")
                AgregarPuestosParaEvaluar(vSeleccion.oSeleccion.ToString());

            if (vSeleccion.clTipo == "REBIND")
                grdCuestionarios.Rebind();

        }

        protected void btnEliminarEvaluado_Click(object sender, EventArgs e)
        {
            XElement vXmlEvaluados = new XElement("EMPLEADOS");
            foreach (GridDataItem item in grdEvaluados.SelectedItems)
                vXmlEvaluados.Add(new XElement("EMPLEADO", new XAttribute("ID_EMPLEADO", item.GetDataKeyValue("ID_EMPLEADO").ToString())));

            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            E_RESULTADO vResultado = nPeriodo.EliminaEvaluados(vPeriodo.ID_PERIODO ?? 0, vXmlEvaluados, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
            {
                grdEvaluados.Rebind();
                grdCuestionarios.Rebind();
            }
        }

        protected void grdEvaluadoresExternos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            CuestionarioNegocio nCuestionario = new CuestionarioNegocio();

            List<SPE_OBTIENE_FYD_EVALUADORES_EXTERNOS_Result> evaluadores = new List<SPE_OBTIENE_FYD_EVALUADORES_EXTERNOS_Result>();
            evaluadores = nCuestionario.ObtieneEvaluadoresExternos(vPeriodo.ID_PERIODO ?? 0, "OTRO");
            grdEvaluadoresExternos.DataSource = evaluadores;
        }

        protected void btnAgregarOtroEvaluador_Click(object sender, EventArgs e)
        {
            AgregarOtroEvaluadorExterno(null, null, txtNbEvaluadorExterno.Text, txtNbEvaluadorExternoPuesto.Text, txtClCorreoElectronico.Text, chkFgOtroEvaluadorExternoTodos.Checked, E_TIPO_OPERACION_DB.I);
            txtNbEvaluadorExterno.Text = String.Empty;
            txtNbEvaluadorExternoPuesto.Text = String.Empty;
            txtClCorreoElectronico.Text = String.Empty;
        }

        protected void grdEvaluadoresExternos_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            if ((e.Item is GridEditableItem) && e.Item.IsInEditMode)
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                string vNbEvaluador = ((RadTextBox)item.FindControl("txtNbEvaluadorEditForm")).Text;
                string vNbPuesto = ((RadTextBox)item.FindControl("txtNbPuestoEvaluadorEditForm")).Text;
                string vClCorreoElectronico = ((RadTextBox)item.FindControl("txtClCorreoElectronicoEditForm")).Text;
                bool vFgEvaluaTodos = ((RadButton)item.FindControl("chkFormFgEvaluaTodos")).Checked;
                int vIdEvaluador = int.Parse(item.GetDataKeyValue("ID_EVALUADOR").ToString());

                AgregarOtroEvaluadorExterno(null, vIdEvaluador, vNbEvaluador, vNbPuesto, vClCorreoElectronico, vFgEvaluaTodos, E_TIPO_OPERACION_DB.A);
            }
        }

        protected void AgregarOtroEvaluadorExterno(int? pIdEmpleado, int? pIdEvaluador, string pNbEvaluador, string pNbPuesto, string pClCorreoElectronico, bool pFgEvaluaTodos, E_TIPO_OPERACION_DB pClTipoOperacion)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            E_RESULTADO vResultado = nPeriodo.InsertaActualizaOtrosEvaluadoresExternos(vPeriodo.ID_PERIODO ?? 0, pIdEmpleado, pIdEvaluador, pNbEvaluador, pNbPuesto, pClCorreoElectronico, pFgEvaluaTodos, vClUsuario, vNbPrograma, pClTipoOperacion);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                grdEvaluadoresExternos.Rebind();
        }

        protected void grdEvaluadoresExternos_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item is GridEditableItem)
            {
                GridEditableItem item = (GridEditableItem)e.Item;

                int vIdEvaluador = int.Parse(item.GetDataKeyValue("ID_EVALUADOR").ToString());

                PeriodoNegocio nPeriodo = new PeriodoNegocio();
                E_RESULTADO vResultado = nPeriodo.EliminaEvaluador(vIdEvaluador, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

                if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                {
                    grdEvaluadoresExternos.Rebind();
                    grdCuestionarios.Rebind();
                }
            }
        }

        protected void btnCrearCuestionarios_Click(object sender, EventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            E_RESULTADO vResultado = nPeriodo.InsertaCuestionarios(vPeriodo.ID_PERIODO ?? 0, true, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
            {
                grdCuestionarios.Rebind();
                grdContrasenaEvaluadores.Rebind();
            }
        }

        protected void grdEvaluados_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem vDataItem = (GridDataItem)e.DetailTableView.ParentItem;
            switch (e.DetailTableView.Name)
            {
                case "gtvPuestosEvaluacion":
                    int vIdEvaluado = 0;

                    if (int.TryParse(vDataItem.GetDataKeyValue("ID_EVALUADO").ToString(), out vIdEvaluado))
                    {
                        PeriodoNegocio nPeriodo = new PeriodoNegocio();
                        var vListaPuestos = nPeriodo.ObtienePuestosEvaluado(vIdEvaluado);
                        e.DetailTableView.DataSource = vListaPuestos;

                        //if (vListaPuestos.Count > 0)
                        //{
                        //    vFgPuestosComparacion = true;
                        //}
                        //else
                        //{
                        //    vFgPuestosComparacion = false;
                        //}
                    }

                    break;
            }
        }

        protected void btnGuardarConfiguracion_Click(object sender, EventArgs e)
        {
            GuardarConfiguracion(false,true);

            if (btnEvaluacionPorPuesto.Checked == true)
                divPonderacionCompetencias.Style.Value = String.Format("display:{0}", "block");

            if (btnEvaluacionPorEstandar.Checked == true)
                divPonderacionCompetencias.Style.Value = String.Format("display: {0}", "none");

            if (btnEvaluacionPorOtras.Checked == true)
                divPonderacionCompetencias.Style.Value = String.Format("display: {0}", "none");

            ObtenerDatosPeriodo(vPeriodo.ID_PERIODO ?? 0);
        }

        protected void btnGuardarConfiguracionCerrar_Click(object sender, EventArgs e)
        {
            GuardarConfiguracion(true,true);
        }

        protected void GuardarConfiguracion(bool pFgCerrarVentana, bool mostrarMsg)
        {
            XElement vXmlConfiguracion = new XElement("CONFIGURACION");
            XElement vXmlCamposComunes = null;
            string vNbNodoCamposComunes = "CAMPOS_COMUNES";

            E_RESULTADO vValidacion = new E_RESULTADO()
            {
                CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.SUCCESSFUL,
                MENSAJE = new List<E_MENSAJE>()
            };

            decimal vPrAutoevaluacion = (decimal)(txtPrAutoevaluacion.Value ?? 0);
            decimal vPrSuperior = (decimal)(txtPrSuperior.Value ?? 0);
            decimal vPrSubordinado = (decimal)(txtPrSubordinados.Value ?? 0);
            decimal vPrInterrelacionados = (decimal)(txtPrInterrelacionados.Value ?? 0);
            decimal vPrOtrosEvaluadores = (decimal)(txtPrOtros.Value ?? 0);

            decimal vPrTotalEvaluadores = vPrAutoevaluacion + vPrSuperior + vPrSubordinado + vPrInterrelacionados + vPrOtrosEvaluadores;

            decimal vPrCompetenciasGenericas = (decimal)(txtPrGenericas.Value ?? 0);
            decimal vPrCompetenciasEspecificas = (decimal)(txtPrEspecificas.Value ?? 0);
            decimal vPrCompetenciasInstitucionales = (decimal)(txtPrInstitucionales.Value ?? 0);

            decimal vPrTotalCompetencias = vPrCompetenciasGenericas + vPrCompetenciasEspecificas + vPrCompetenciasInstitucionales;

            XElement vXmlTipoEvaluacion = new XElement("TIPO_EVALUACION");




            if (chkFgPonderarEvaluadores.Checked == true)
            {

                if (vPrTotalEvaluadores == 100)
                {
                    vXmlTipoEvaluacion.Add(
                        new XAttribute("PR_AUTOEVALUACION", vPrAutoevaluacion),
                        new XAttribute("PR_SUPERIOR", vPrSuperior),
                        new XAttribute("PR_SUBORDINADOS", vPrSubordinado),
                        new XAttribute("PR_INTERRELACIONADO", vPrInterrelacionados),
                        new XAttribute("PR_OTROS_EVALUADORES", vPrOtrosEvaluadores),
                        new XAttribute("FG_PONDERACION_EVALUADORES", chkFgPonderarEvaluadores.Checked ? "1" : "0")
                        );

                    txtPrTotal.DisabledStyle.CssClass = txtPrTotal.DisabledStyle.CssClass.Replace("errBackground", "");

                }
                else
                {
                    vValidacion.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.ERROR;
                    vValidacion.MENSAJE.Add(new E_MENSAJE() { DS_MENSAJE = "La suma de las ponderaciones de los evaluadores debe sumar 100%" });
                    txtPrTotal.DisabledStyle.CssClass = "errBackground";
                }
            }

            if (chkFgPonderacionCompetencia.Checked == true)
            {

                if (vPrTotalCompetencias == 100)
                {
                    vXmlTipoEvaluacion.Add(
                        new XAttribute("PR_GENERICAS", vPrCompetenciasGenericas),
                        new XAttribute("PR_ESPECIFICAS", vPrCompetenciasEspecificas),
                        new XAttribute("PR_INSTITUCIONALES", vPrCompetenciasInstitucionales),
                        new XAttribute("FG_PONDERACION_COMPETENCIAS", chkFgPonderacionCompetencia.Checked ? "1" : "0")
                        );
                    txtPrTotalCompetencias.DisabledStyle.CssClass = txtPrTotalCompetencias.DisabledStyle.CssClass.Replace("errBackground", "");
                }

                else
                {
                    vValidacion.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.ERROR;
                    vValidacion.MENSAJE.Add(new E_MENSAJE() { DS_MENSAJE = "La suma de las ponderaciones de las competencias debe suma 100%" });
                    txtPrTotalCompetencias.DisabledStyle.CssClass = "errBackground";
                }
            }

            if (vValidacion.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
            {

                if (btnEvaluacionPorPuesto.Checked)
                {
                    vXmlTipoEvaluacion.Add(new XAttribute("CL_TIPO_EVALUACION", E_CL_TIPO_EVALUACION.PUESTO_CARRERA.ToString()),
                        new XAttribute("FG_PUESTO_ACTUAL", chkFgPuestoActual.Checked ? "1" : "0"),
                        new XAttribute("FG_OTROS_PUESTOS", chkFgOtrosPuestos.Checked ? "1" : "0"),
                        new XAttribute("FG_RUTA_VERTICAL", chkFgRutaVertical.Checked ? "1" : "0"),
                        new XAttribute("FG_RUTA_VERTICAL_ALTERNATIVA", chkFgRutaVerticalAlternativa.Checked ? "1" : "0"),
                        new XAttribute("FG_RUTA_HORIZONTAL", chkFgRutaHorizontalAlternativa.Checked ? "1" : "0")
                        );
                }

                if (btnEvaluacionPorEstandar.Checked)
                {
                    vXmlTipoEvaluacion.Add(new XAttribute("CL_TIPO_EVALUACION", E_CL_TIPO_EVALUACION.COMPETENCIA_ESTANDAR.ToString()),
                        new XAttribute("FG_GENERICAS", chkFgCompetenciasGenericas.Checked ? "1" : "0"),
                        new XAttribute("FG_ESPECIFICAS", chkFgCompetenciasEspecificas.Checked ? "1" : "0"),
                        new XAttribute("FG_INSTITUCIONALES", chkFgCompetenciasInstitucionales.Checked ? "1" : "0")
                        );
                }

                if (btnEvaluacionPorOtras.Checked)
                {
                    vXmlTipoEvaluacion.Add(new XAttribute("CL_TIPO_EVALUACION", E_CL_TIPO_EVALUACION.COMPETENCIAS_OTRAS.ToString()),
                        lstCompetenciasEspecificas.Items.Where(w => !w.Value.Equals(String.Empty)).Select(s => new XElement("COMPETENCIA_ESPECIFICA", new XAttribute("ID_COMPETENCIA", s.Value)))
                        );
                }

                if (chkFgEvaluadorInterrelacionados.Checked)
                {
                    if (vXmlCamposComunes == null)
                        vXmlCamposComunes = new XElement(vNbNodoCamposComunes);
                    vXmlCamposComunes.Add(new XElement("INTERRELACIONADOS", lstCamposInterrelacionados.Items.Where(w => !w.Value.Equals(String.Empty)).Select(s => new XElement("CAMPO", new XAttribute("ID_CAMPO", s.Value), new XAttribute("NB_CAMPO", s.Text)))));
                }

                vXmlConfiguracion.Add(vXmlTipoEvaluacion);

                if (vXmlCamposComunes != null)
                    vXmlConfiguracion.Add(vXmlCamposComunes);

                PeriodoNegocio nPeriodo = new PeriodoNegocio();

                E_RESULTADO vResultado = nPeriodo.ActualizaConfiguracionPeriodo(vPeriodo.ID_PERIODO ?? 0, vXmlConfiguracion, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                string vCallBackFunction = pFgCerrarVentana ? "closeWindow" : null;

                if (mostrarMsg)//No muestra mensaje cuando viene de PVC
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: vCallBackFunction);

                if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                    btnAgregarPuestos.Visible = chkFgOtrosPuestos.Checked;
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vValidacion.MENSAJE.FirstOrDefault().DS_MENSAJE, vValidacion.CL_TIPO_ERROR, pCallBackFunction: null);
            }



        }

        protected void grdEvaluados_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            //SI EL PERIODO ES DE PVC NO ELIMINAR ITEM
            if (vPeriodo.FG_CREADO_POR_PVC)
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "No se puede eliminar de un período creado desde plan de vida y carrera.", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: null);
            }
            else
            {
                if (e.Item.OwnerTableView.Name == "gtvPuestosEvaluacion")
                {
                    int vIdPeriodoPuestoEvaluado = 0;
                    if (int.TryParse(((GridDataItem)e.Item).GetDataKeyValue("ID_PUESTO_EVALUADO_PERIODO").ToString(), out vIdPeriodoPuestoEvaluado))
                    {
                        PeriodoNegocio nPeriodo = new PeriodoNegocio();
                        E_RESULTADO vResultado = nPeriodo.EliminaPuestoEvaluado(vIdPeriodoPuestoEvaluado);
                        string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                        UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

                        if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                            e.Item.OwnerTableView.Rebind();
                    }
                }
            }
        }

        protected void grdCuestionarios_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();

            List<SPE_OBTIENE_FYD_CUESTIONARIOS_EVALUADOS_Result> vLstEvaluadosCuestionarios = nPeriodo.ObtieneEvaluadosCuestionarios(vPeriodo.ID_PERIODO ?? 0, ContextoUsuario.oUsuario.ID_EMPRESA);

            grdCuestionarios.DataSource = vLstEvaluadosCuestionarios;
            ChangeEnableMensajeCampos(!vLstEvaluadosCuestionarios.Any(a => a.NO_CUESTIONARIOS > 0));
            ChangeEnableCuestionarios(!(vLstEvaluadosCuestionarios.Count > 0));
        }

        protected void grdCuestionarios_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem vDataItem = (GridDataItem)e.DetailTableView.ParentItem;
            switch (e.DetailTableView.Name)
            {
                case "gtvEvaluadores":
                    int vIdEvaluado = 0;
                    if (int.TryParse(vDataItem.GetDataKeyValue("ID_EVALUADO").ToString(), out vIdEvaluado))
                    {
                        PeriodoNegocio nPeriodo = new PeriodoNegocio();
                        e.DetailTableView.DataSource = nPeriodo.ObtieneCuestionariosEvaluado(vIdEvaluado);
                    }
                    break;
            }
        }

        protected void btnAgregarCampoAdicional_Click(object sender, EventArgs e)
        {
            /*
            XElement vXmlPreguntasAdicionales = new XElement("PREGUNTAS_ADICIONALES");
            vXmlPreguntasAdicionales.Add(lstCampoAdicional.Items.Where(w => !w.Value.Equals(String.Empty)).Select(s => new XElement("PREGUNTA", new XAttribute("ID_CAMPO", s.Value))));

            PeriodoNegocio nPeriodo = new PeriodoNegocio();

            E_CL_CUESTIONARIO_OBJETIVO vClCuestionarioObjetivo = E_CL_CUESTIONARIO_OBJETIVO.AMBOS;

            if (btnCuestionarioAutoevaluacion.Checked)
                vClCuestionarioObjetivo = E_CL_CUESTIONARIO_OBJETIVO.AUTOEVALUACION;

            if (btnCuestionarioOtros.Checked)
                vClCuestionarioObjetivo = E_CL_CUESTIONARIO_OBJETIVO.OTROS;

            E_RESULTADO vResultado = nPeriodo.InsertaPreguntasAdicionales(vPeriodo.ID_PERIODO ?? 0,"HOLA", vXmlPreguntasAdicionales, vClCuestionarioObjetivo, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
            {
                grdCamposAdicionales.Rebind();
                lstCampoAdicional.Items.Clear();
                lstCampoAdicional.Items.Add(new RadListBoxItem("No seleccionado", "0"));
            }
             * */
        }

        protected void grdCamposAdicionales_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            grdCamposAdicionales.DataSource = nPeriodo.ObtienePreguntasAdicionales(vPeriodo.ID_PERIODO ?? 0);
        }

        protected void grdCamposAdicionales_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            int vIdPregunta = int.Parse(((GridDataItem)e.Item).GetDataKeyValue("ID_PREGUNTA_ADICIONAL").ToString());

            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            E_RESULTADO vResultado = nPeriodo.EliminaPreguntaAdicional(vIdPregunta);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                grdCamposAdicionales.Rebind();
        }

        protected void grdCuestionarios_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item.OwnerTableView.Name == "gtvEvaluadores")
            {
                int vIdCuestionario = 0;
                if (int.TryParse(((GridDataItem)e.Item).GetDataKeyValue("ID_CUESTIONARIO").ToString(), out vIdCuestionario))
                {
                    PeriodoNegocio nPeriodo = new PeriodoNegocio();
                    E_RESULTADO vResultado = nPeriodo.EliminaCuestionario(vIdCuestionario);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

                    if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                        e.Item.OwnerTableView.Rebind();
                }
            }
        }

        protected void grdContrasenaEvaluadores_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            grdContrasenaEvaluadores.DataSource = nPeriodo.ObtieneTokenEvaluadores(vPeriodo.ID_PERIODO ?? 0, ContextoUsuario.oUsuario.ID_EMPRESA);
        }

        protected void btnReasignarTodasContrasenas_Click(object sender, EventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();

            E_RESULTADO vResultado = nPeriodo.InsertarActualizarTokenEvaluadores(vPeriodo.ID_PERIODO ?? 0, null, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                grdContrasenaEvaluadores.Rebind();
        }

        protected void btnReasignarContrasena_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in grdContrasenaEvaluadores.SelectedItems)
            {
                PeriodoNegocio nPeriodo = new PeriodoNegocio();

                E_RESULTADO vResultado = nPeriodo.InsertarActualizarTokenEvaluadores(vPeriodo.ID_PERIODO ?? 0, int.Parse(item.GetDataKeyValue("ID_EVALUADOR").ToString()), vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

                if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                    grdContrasenaEvaluadores.Rebind();
            }
        }

        protected void rtsConfiguracionPeriodo_TabClick(object sender, RadTabStripEventArgs e)
        {
        }

        public void aplicarConfiguracionPeriodoPVC()
        {
            btnSeleccionPorPersona.Enabled = false;
            btnSeleccionPorPuesto.Enabled = false;
            btnSeleccionPorArea.Enabled = false;
            btnEliminarEvaluado.Enabled = false;
            btnAgregarPuestos.Enabled = false;
        }

        protected void grdCamposAdicionales_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdCamposAdicionales.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdCamposAdicionales.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdCamposAdicionales.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdCamposAdicionales.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdCamposAdicionales.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void grdEvaluados_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdEvaluados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdEvaluados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdEvaluados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdEvaluados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdEvaluados.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void grdEvaluadoresExternos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdEvaluadoresExternos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdEvaluadoresExternos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdEvaluadoresExternos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdEvaluadoresExternos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdEvaluadoresExternos.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void grdCuestionarios_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdCuestionarios.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdCuestionarios.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdCuestionarios.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdCuestionarios.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdCuestionarios.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void grdContrasenaEvaluadores_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdContrasenaEvaluadores.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdContrasenaEvaluadores.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdContrasenaEvaluadores.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdContrasenaEvaluadores.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdContrasenaEvaluadores.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}