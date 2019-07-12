using Newtonsoft.Json;
using SIGE.Entidades.Externas;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using SIGE.Entidades;
using Telerik.Web.UI;
using WebApp.Comunes;
using SIGE.Negocio.Utilerias;

namespace SIGE.WebApp.EO
{
    public partial class VentanaConfigurarDesempeno : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private XElement SeleccionMetasEvaluado { get; set; }
        private XElement RESULTADOS { get; set; }
        private int? vIdRol;
        decimal sum = 0;
        decimal vSumaEvaluadas = 0;

        private List<E_METAS_EVALUADO> vListaMetasEvaluado
        {
            get { return (List<E_METAS_EVALUADO>)ViewState["vs_vListaMetasEvaluado"]; }
            set { ViewState["vs_vListaMetasEvaluado"] = value; }
        }

        private List<E_META> vListaMetas
        {
            get { return (List<E_META>)ViewState["vs_vListaMetas"]; }
            set { ViewState["vs_vListaMetas"] = value; }
        }

        private int vIdPeriodoDesempeno
        {
            get { return (int)ViewState["vsIdPeriodoDesempeno"]; }
            set { ViewState["vsIdPeriodoDesempeno"] = value; }
        }

        private int vIdEmpleado
        {
            get { return (int)ViewState["vsvIdEmpleado"]; }
            set { ViewState["vsvIdEmpleado"] = value; }
        }

        public int vIdPeriodo
        {
            get { return (int)ViewState["vsIdPeriodo"]; }
            set { ViewState["vsIdPeriodo"] = value; }
        }

        private int? vEstatusDisenoMetas
        {
            get { return (int?)ViewState["vEstatusDisenoMetas"]; }
            set { ViewState["vEstatusDisenoMetas"] = value; }
        }

        public string vClOrigenPeriodo
        {
            get { return (string)ViewState["vs_vClOrigenPeriodo"]; }
            set { ViewState["vs_vClOrigenPeriodo"] = value; }
        }

        public int? vNoReplica
        {
            get { return (int?)ViewState["vs_vNoReplica"]; }
            set { ViewState["vs_vNoReplica"] = value; }
        }

        public string vClTipoMetas
        {
            get { return (string)ViewState["vs_vClTipoMetas"]; }
            set { ViewState["vs_vClTipoMetas"] = value; }
        }

        private bool vFgCordinador
        {
            get { return (bool)ViewState["vs_vFgCordinador"]; }
            set { ViewState["vs_vFgCordinador"] = value; }
        }

        public bool vFgBajas
        {
            get { return (bool)ViewState["vs_vFgBajas"]; }
            set { ViewState["vs_vFgBajas"] = value; }
        }

        #endregion

        #region Funciones

        public string validarDsNotas(string vdsNotas)
        {
            E_NOTAS pNota = null;
            if (vdsNotas != null)
            {
                XElement vNotas = XElement.Parse(vdsNotas.ToString());
                if (ValidarRamaXml(vNotas, "NOTA"))
                {
                    pNota = vNotas.Elements("NOTA").Select(el => new E_NOTAS
                    {
                        DS_NOTA = UtilXML.ValorAtributo<string>(el.Attribute("DS_NOTA")),
                        FE_NOTA = (DateTime?)UtilXML.ValorAtributo(el.Attribute("FE_NOTA"), E_TIPO_DATO.DATETIME),
                    }).FirstOrDefault();
                }

                if (pNota != null)
                {
                    if (pNota.DS_NOTA != null)
                    {
                        return pNota.DS_NOTA.ToString();
                    }
                    else return "";
                }
                else
                    return "";
            }
            else
            {
                return "";
            }
        }

        public Boolean ValidarRamaXml(XElement parentEl, string elementsName)
        {
            var foundEl = parentEl.Element(elementsName);

            if (foundEl != null)
            {
                return true;
            }
            return false;
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

        protected void AgregarEvaluadores(string pEvaluadores)
        {
            List<E_SELECTOR_EMPLEADO> vEvaluadores = JsonConvert.DeserializeObject<List<E_SELECTOR_EMPLEADO>>(pEvaluadores);
            if (vEvaluadores.Count > 0)
                AgregarEvaluadores(new XElement("EVALUADORES", vEvaluadores.Select(s => new XElement("EVALUADOR", new XAttribute("ID_EVALUADOR", s.idEmpleado)))));
        }

        protected void AgregarEvaluadores(XElement pXmlElementos)
        {
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            XElement vXmlEvaluados = new XElement("EVALUADOS");
            foreach (GridDataItem item in grdEvaluados.SelectedItems)
                vXmlEvaluados.Add(new XElement("EVALUADO", new XAttribute("ID_EVALUADO", item.GetDataKeyValue("ID_EMPLEADO").ToString())));

            E_RESULTADO vResultado = nPeriodo.InsertaEvaluadoresOtro(vIdPeriodo, vXmlEvaluados, pXmlElementos, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vMensaje == "Proceso exitoso")
            {
                grdEvaluados.Rebind();
                grdDisenoMetas.Rebind();
                GenerarContrasena(vResultado);
               // grdContrasenaEvaluadores.Rebind();
            }
            //UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

            //if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
            //{
            //    grdEvaluados.Rebind();
            //    grdContrasenaEvaluadores.Rebind();

            //}
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
            }
        }

        protected void AgregarEvaluados(XElement pXmlElementos)
        {
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            E_RESULTADO vResultado = nPeriodo.InsertaEvaluados(vIdPeriodo, pXmlElementos, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                grdEvaluados.Rebind();
                grdDisenoMetas.Rebind();
                rgBono.Rebind();
               // grdContrasenaEvaluadores.Rebind();

              if(vFgCordinador)
                GenerarContrasena(vResultado);
              else
                  grdContrasenaEvaluadores.Rebind();

            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
            }
        }

        protected void GenerarContrasena(E_RESULTADO vResultadoAgregar)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();

            E_RESULTADO vResultado = nPeriodo.InsertarActualizarTokenEvaluadores(vIdPeriodo, null, vClUsuario, vNbPrograma, pIdRol: vIdRol);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            string vMensajeAgregar = vResultadoAgregar.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vMensaje.Equals("Proceso exitoso"))
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensajeAgregar, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
            else
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                grdContrasenaEvaluadores.Rebind();
        }

        private void CargarDatosContexto()
        {
            PeriodoDesempenoNegocio periodo = new PeriodoDesempenoNegocio();
            var oPeriodo = periodo.ObtienePeriodoDesempenoContexto(vIdPeriodo, null);
            if (oPeriodo != null)
            {
                txtClPeriodo.InnerText = oPeriodo.CL_PERIODO;
                //txtNbPeriodo.InnerText = oPeriodo.NB_PERIODO;
                txtPeriodos.InnerText = oPeriodo.DS_PERIODO;
                txtFechas.InnerText = oPeriodo.FE_INICIO.ToString("d") + " a " + oPeriodo.FE_TERMINO.Value.ToShortDateString();
                txtTipoMetas.InnerText = oPeriodo.CL_TIPO_PERIODO;
                txtTipoCapturista.InnerText = Utileria.LetrasCapitales(oPeriodo.CL_TIPO_CAPTURISTA);

                if (oPeriodo.FG_BONO == true && oPeriodo.FG_MONTO == true)
                    txtTipoBono.InnerText = oPeriodo.CL_TIPO_BONO + " (monto)";
                else if (oPeriodo.FG_BONO == true && oPeriodo.FG_PORCENTUAL == true)
                    txtTipoBono.InnerText = oPeriodo.CL_TIPO_BONO + " (porcentual)";
                else
                    txtTipoBono.InnerText = oPeriodo.CL_TIPO_BONO;

                txtTipoPeriodo.InnerText = oPeriodo.CL_ORIGEN_CUESTIONARIO;

                if (oPeriodo.DS_NOTAS != null)
                {
                    XElement vNotas = XElement.Parse(oPeriodo.DS_NOTAS);
                    if (vNotas != null)
                    {
                        txtNotas.InnerHtml = validarDsNotas(vNotas.ToString());
                    }
                }
            }
        }

        private void CargarDatos()
        {
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();

            E_RESULTADO vResultado = nPeriodo.InsertaPeriodoDesempenoReplica(pIdPeriodo: vIdPeriodo, pCL_USUARIO: vClUsuario, pNB_PROGRAMA: vNbPrograma, pTipoTransaccion: "V");
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vMensaje == "No hay empleados dados de baja")
                vFgBajas = false;
            else
                vFgBajas = true;


            var vPeriodoDesempeno = nPeriodo.ObtienePeriodosDesempeno(pIdPeriodo: vIdPeriodo).FirstOrDefault();

            vClOrigenPeriodo = vPeriodoDesempeno.CL_ORIGEN_CUESTIONARIO;
            vClTipoMetas = vPeriodoDesempeno.CL_TIPO_METAS;
            vNoReplica = vPeriodoDesempeno.NO_REPLICA;

            if (vPeriodoDesempeno.CL_TIPO_CAPTURISTA == "OTRO")
                btnAgregarEvaluador.Visible = true;
            if (vPeriodoDesempeno.CL_TIPO_CAPTURISTA == "COORDINADOR_EVAL")           
                vFgCordinador = false;
            else
                vFgCordinador = true;

               
            
            txtRangoPeriodo.Text = vPeriodoDesempeno.FE_INICIO.ToString("d") + " a " + vPeriodoDesempeno.FE_TERMINO.Value.ToShortDateString();
            vIdPeriodoDesempeno = vPeriodoDesempeno.ID_PERIODO_DESEMPENO;
            CargarDatosContexto();

            if (vPeriodoDesempeno.CL_TIPO_METAS == "DESCRIPTIVO")
            {
                btnAgregarMeta.Enabled = true;
                btnModificarMetas.Enabled = true;

                btnAgregarMeta.Text = "Agregar";
                btnActivarMetas.Text = "Activar";
                btnDesactivarMetas.Text = "Desactivar";
                btnModificarMetas.Text = "Editar";
                btnCopiarMetas.Text = "Copiar";
                btnGuardarMetas.Text= "Guardar";

                grdDisenoMetas.MasterTableView.DetailTables[0].Columns[0].Visible = true;
            }
            else if (vPeriodoDesempeno.CL_TIPO_METAS == "CERO")
            {
                btnAgregarMeta.Enabled = true;
                btnModificarMetas.Enabled = true;
                grdDisenoMetas.MasterTableView.DetailTables[0].Columns.FindByUniqueName("NB_INDICADOR").Visible = false;
            }
            else
            {
                btnAgregarMeta.Enabled = false;
                btnModificarMetas.Enabled = false;
            }


            if (vPeriodoDesempeno.FG_BONO == true)
            {
                rbSi.Checked = true;
                rbIndividualDependiente.Enabled = true;
                rbIndividualIndependiente.Enabled = true;
                rbGrupal.Enabled = true;

                btnCalcularTodos.Enabled = true;
                btnCalcularSeleccion.Enabled = true;

                if (vPeriodoDesempeno.PR_BONO != 0 & vPeriodoDesempeno.MN_BONO == 0)
                {
                    rbPorcentaje.Checked = true;
                    rbMonto.Checked = false;
                    txtMontoBono.Text = vPeriodoDesempeno.PR_BONO.ToString();
                }
                else
                {
                    rbPorcentaje.Checked = false;
                    rbMonto.Checked = true;
                    txtMontoBono.Text = vPeriodoDesempeno.MN_BONO.ToString();
                }
                rgBono.Enabled = true;
            }
            else
            {
                if(vPeriodoDesempeno.FG_BONO == false)
                rbNo.Checked = true;

                rbIndividualDependiente.Enabled = false;
                rbIndividualIndependiente.Enabled = false;
                rbGrupal.Enabled = false;

                rbMonto.Enabled = false;
                rbMonto.Checked = false;

                btnCalcularTodos.Enabled = false;
                btnCalcularSeleccion.Enabled = false;

                rbPorcentaje.Checked = false;
                rbPorcentaje.Enabled = false;

                txtMontoBono.Text = "0";
                txtMontoBono.Enabled = false;
                rgBono.Enabled = false;
            }

            if (vPeriodoDesempeno.CL_TIPO_BONO == "DEPENDIENTE")
                rbIndividualDependiente.Checked = true;
            if (vPeriodoDesempeno.CL_TIPO_BONO == "INDEPENDIENTE")
                rbIndividualIndependiente.Checked = true;
            if (vPeriodoDesempeno.CL_TIPO_BONO == "GRUPAL")
                rbGrupal.Checked = true;

        }

        private void GuardarDatos(bool pCerrar)
        {
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            decimal vNoMonto=0;
            decimal vNoPorcentaje=0;
            string vTipoBono = "N/A";

   
            if (rbIndividualDependiente.Checked)
                vTipoBono = "DEPENDIENTE";
            if (rbIndividualIndependiente.Checked)
                vTipoBono = "INDEPENDIENTE";
            if (rbGrupal.Checked)
                vTipoBono = "GRUPAL";

            if(rbSi.Checked == true && (rbPorcentaje.Checked == false && rbMonto.Checked == false))
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Indique el monto de bono.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return;
            }

            if (rbSi.Checked == true && vTipoBono == "N/A")
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Indique el tipo de bono.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return;
            }

           


            if (txtMontoBono.Text != null && txtMontoBono.Text != "")
            {
                if (rbPorcentaje.Checked)
                {
                    vNoPorcentaje = Convert.ToDecimal(txtMontoBono.Text);
                    vNoMonto = 0;
                }
                else
                {
                    vNoPorcentaje = 0;
                    vNoMonto = Convert.ToDecimal(txtMontoBono.Text);
                }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Indique la cantidad de monto de bono para el evaluado", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return;
            }

            var vResultado = nPeriodo.ActualizaConfiguracionDesempeno(vIdPeriodoDesempeno, rbSi.Checked ? 1 : 0, vNoPorcentaje, vNoMonto, vTipoBono, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (pCerrar)
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
            }
        }

        protected void SeguridadProcesos(bool? pFgConfiguracionCompleta)
        {
            bool vFgConfiguracionCompleta = false;
            if (pFgConfiguracionCompleta == true)
                vFgConfiguracionCompleta = true;

            btnEnvioSolicitudes.Enabled = ContextoUsuario.oUsuario.TienePermiso("M.A.A.I") && vFgCordinador && vFgConfiguracionCompleta;
            btnReasignarContrasena.Enabled = vFgCordinador;
            lbMensaje.Visible = !vFgCordinador;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;
            if (!IsPostBack)
            {
                if (Request.Params["PeriodoId"] != null)
                {
                    vIdPeriodo = int.Parse(Request.QueryString["PeriodoId"]);
                    CargarDatos();
                }
                else
                {
                    vIdPeriodo = 0;
                }
            }

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
        }

        protected void grdSeleccionEvaluados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            grdEvaluados.DataSource = nPeriodo.ObtieneEvaluados(pIdPeriodo: vIdPeriodo, pIdRol: vIdRol);
        }

        protected void ramConfiguracionPeriodo_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
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

            if (vSeleccion.clTipo == "EVALUADOR")
                AgregarEvaluadores(vSeleccion.oSeleccion.ToString());

            if (vSeleccion.clTipo == "VERIFICACONFIGURACION")
            {
                PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                var vFgConfigurado = nPeriodo.VerificaConfiguracion(vIdPeriodo).FirstOrDefault();
                if (vFgConfigurado != null)
                    SeguridadProcesos(vFgConfigurado.FG_ESTATUS);
            }
        }

        protected void rgBono_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            rgBono.DataSource = nPeriodo.ObtieneBonoEvaluados(vIdPeriodo, vIdRol);
        }

        protected void btnCalcularTodos_Click(object sender, EventArgs e)
        {
            if (!rbMonto.Checked & !rbPorcentaje.Checked)
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Indique un tipo de monto de bono para el evaluado", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
            }
            else
            {
                if (txtMontoBono.Text != "" && txtMontoBono.Text != null)
                {

                    decimal vPrBono = decimal.Parse(txtMontoBono.Text);
                    string vCltipoBono = rbMonto.Checked ? "MONTO" : "PORCENTAJE";

                    PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();

                    if (vCltipoBono == "PORCENTAJE" & vPrBono > 100)
                    {
                        vPrBono = 100;
                        txtMontoBono.Text = "100";
                    }

                    E_RESULTADO vResultado = nPeriodo.ActualizaEvaluadoTopeBono(vIdPeriodo, vPrBono, vCltipoBono, null, vNbPrograma, vClUsuario);
                    string vmensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                    {
                        GuardarDatos(false);
                        rgBono.Rebind();
                    }
                    else
                    {
                        UtilMensajes.MensajeResultadoDB(rwmMensaje, vmensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
                    }
                }
                else
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, "Indique la cantidad de monto de bono para el evaluado", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                }
            }
        }

        protected void btnCalcularSeleccion_Click(object sender, EventArgs e)
        {
            if (!rbMonto.Checked & !rbPorcentaje.Checked)
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Indique un tipo de monto de bono para el evaluado", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
            }
            else
            {
                 if (txtMontoBono.Text != "" && txtMontoBono.Text != null)
                {
                PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                XElement vXmlempleados = new XElement("EMPLEADOS");
                decimal vPrMonto = 0;
                string vCltipoBono = rbMonto.Checked ? "MONTO" : "PORCENTAJE";

                foreach (GridDataItem item in rgBono.Items)
                {

                    if (((item.FindControl("txtMontoBono") as RadTextBox).Text) != "" && ((item.FindControl("txtMontoBono") as RadTextBox).Text != null))
                    vPrMonto = decimal.Parse((item.FindControl("txtMontoBono") as RadTextBox).Text);

                    if (vCltipoBono == "PORCENTAJE" & vPrMonto > 100)
                    {
                        vPrMonto = 100;
                        (item.FindControl("txtMontoBono") as RadTextBox).Text = "100";
                    }

                    vXmlempleados.Add(new XElement("EMPLEADO", new XAttribute("ID_BONO_EVALUADO", item.GetDataKeyValue("ID_BONO_EVALUADO").ToString()), new XAttribute("PR_BONO", vPrMonto)));

                    
                }
                E_RESULTADO vResultado = nPeriodo.ActualizaEvaluadoTopeBono(vIdPeriodo, 0, vCltipoBono, vXmlempleados.ToString(), vNbPrograma, vClUsuario);
                string vmensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                {
                    GuardarDatos(false);
                    rgBono.Rebind();
                }
                else
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vmensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
                }
                }
                 else
                 {
                     UtilMensajes.MensajeResultadoDB(rwmMensaje, "Indique la cantidad de monto de bono para el evaluado", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                 }
            }

        }

        protected void rbNo_Click(object sender, EventArgs e)
        {
            rbPorcentaje.Checked = false;
            rbPorcentaje.Enabled = false;
            rbMonto.Enabled = false;
            rbMonto.Checked = false;
            txtMontoBono.Text = "0";
            txtMontoBono.Enabled = false;

            rbIndividualDependiente.Checked = false;
            rbIndividualIndependiente.Checked = false;
            rbGrupal.Checked = false;

            btnCalcularSeleccion.Enabled = false;
            btnCalcularTodos.Enabled = false;

            rbIndividualDependiente.Enabled = false;
            rbIndividualIndependiente.Enabled = false;
            rbGrupal.Enabled = false;

            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();

            E_RESULTADO vResultado = nPeriodo.ActualizaEvaluadoTopeBono(vIdPeriodo, 0, "MONTO", null, vNbPrograma, vClUsuario);
            string vmensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vmensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                rgBono.Rebind();


            rgBono.Enabled = false;
        }

        protected void rbSi_Click(object sender, EventArgs e)
        {
            rbPorcentaje.Enabled = true;
            rbMonto.Enabled = true;
            txtMontoBono.Text = "0";
            txtMontoBono.Enabled = true;

            btnCalcularSeleccion.Enabled = true;
            btnCalcularTodos.Enabled = true;

            rbIndividualDependiente.Enabled = true;
            rbIndividualIndependiente.Enabled = true;
            rbGrupal.Enabled = true;

            rgBono.Enabled = true;
        }

        protected void btnGuardarCerrar_Click(object sender, EventArgs e)
        {
            GuardarDatos(true);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarDatos(false);
        }

        protected void grdDisenoMetas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            grdDisenoMetas.DataSource = nPeriodo.ObtieneEvaluados(pIdPeriodo: vIdPeriodo, pIdRol: vIdRol);
        }

        protected void grdDisenoMetas_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem vDataItem = (GridDataItem)e.DetailTableView.ParentItem;
            vListaMetas = new List<E_META>();
            if (e.DetailTableView.Name == "gtvMetas")
            {
                PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();

                vIdEmpleado = int.Parse(vDataItem.GetDataKeyValue("ID_EVALUADO").ToString());
                vSumaEvaluadas = 0;
                vListaMetas = nPeriodo.ObtieneMetas(vIdPeriodo, vIdEmpleado);

                e.DetailTableView.DataSource = vListaMetas;

                foreach (E_META Item in vListaMetas)
                {
                    int vIdEval_Metas = int.Parse(Item.ID_EVALUADO.ToString());
                    bool vfgEvalua = bool.Parse(Item.FG_EVALUAR.ToString());
                    if (vIdEval_Metas == vIdEmpleado)
                    {
                        if (vfgEvalua == true)
                        {
                            vSumaEvaluadas += decimal.Parse(Item.PR_META.ToString());
                        }
                    }
                }
            }
        }

        protected void grdEvaluados_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                XElement vXmlEvaluados = new XElement("EVALUADORES");

                GridDataItem item = e.Item as GridDataItem;
                vXmlEvaluados.Add(new XElement("EVALUADOR", new XAttribute("ID_EVALUADO", item.GetDataKeyValue("ID_EVALUADO").ToString()),
                                                            new XAttribute("ID_EVALUADOR", item.GetDataKeyValue("ID_EVALUADOR").ToString())));

                E_RESULTADO vResultado = nPeriodo.EliminaEvaluadorEvaluado(vIdPeriodo, vXmlEvaluados, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "recargarEvaluados()");

            }

            if (e.CommandName == RadGrid.ExpandCollapseCommandName)
            {
                foreach (GridItem item in e.Item.OwnerTableView.Items)
                {
                    if (item.Expanded && item != e.Item)
                    {
                        item.Expanded = false;
                    }
                }
            }
        }

        protected void grdDisenoMetas_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridFooterItem & e.Item.OwnerTableView.Name.Equals("gtvMetas"))
            {
                GridFooterItem footer = (GridFooterItem)e.Item;
                footer["PR_META"].Text = vSumaEvaluadas.ToString();
            }
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdDisenoMetas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdDisenoMetas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdDisenoMetas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdDisenoMetas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdDisenoMetas.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void btnReasignarPonderacion_Click(object sender, EventArgs e)
        {
            if(grdEvaluados.Items.Count <=0)
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Debe seleccionar por lo menos un evaluado", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return;
            }

            List<E_PONDERACION_META> ponderacionMeta = new List<E_PONDERACION_META>();
            foreach (GridDataItem item in grdEvaluados.MasterTableView.Items)
            {
                int id_empleado = Convert.ToInt32(item.GetDataKeyValue("ID_EMPLEADO").ToString());
                int id_evaluado = Convert.ToInt32(item.GetDataKeyValue("ID_EVALUADO").ToString());
                string cl_empleado = (item.GetDataKeyValue("CL_EMPLEADO").ToString());
                string nb_empleado = (item.GetDataKeyValue("NB_EMPLEADO_COMPLETO").ToString());
                string nb_puesto = (item.GetDataKeyValue("NB_PUESTO").ToString());
                string nb_area = (item.GetDataKeyValue("NB_DEPARTAMENTO").ToString());
                RadNumericTextBox txtPonderacion = (RadNumericTextBox)item.FindControl("txtPonderacion");
                decimal ponderacion = txtPonderacion.Text != "" ? Convert.ToDecimal(txtPonderacion.Text) : 0;

                E_PONDERACION_META ponderacionM = new
                E_PONDERACION_META
                {
                    ID_EMPLEADO = id_empleado,
                    ID_EVALUADO = id_evaluado,
                    CL_EMPLEADO = cl_empleado,
                    NB_EMPLEADO = nb_empleado,
                    NB_PUESTO = nb_puesto,
                    NB_AREA = nb_area,
                    PR_PONDERACION = ponderacion
                };

                ponderacionMeta.Add(ponderacionM);
                var vXelements = ponderacionMeta.Select(x =>
                                      new XElement("EVALUADO",
                                      new XAttribute("ID_EMPLEADO", x.ID_EMPLEADO),
                                      new XAttribute("ID_EVALUADO", x.ID_EVALUADO),
                                      new XAttribute("CL_EMPLEADO", x.CL_EMPLEADO),
                                      new XAttribute("NB_EMPLEADO_COMPLETO", x.NB_EMPLEADO),
                                      new XAttribute("NB_PUESTO", x.NB_PUESTO),
                                      new XAttribute("NB_DEPARTAMENTO", x.NB_AREA),
                                      new XAttribute("PR_EVALUADO", x.PR_PONDERACION)
                          ));
                RESULTADOS =
                new XElement("EVALUADOS", vXelements
                );
            }

            PeriodoDesempenoNegocio neg = new PeriodoDesempenoNegocio();
            E_RESULTADO vResultado = neg.ActualizaPonderacionEvaluados(vIdPeriodo, RESULTADOS.ToString(), "REASIGNAR", vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
            grdEvaluados.Rebind();
        }

        protected void btnGuardarEvaluado_Click(object sender, EventArgs e)
        {
            if(grdEvaluados.Items.Count == 0)
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Debe seleccionar por lo menos un evaluado.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                return;
            }
            
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();

            List<E_PONDERACION_META> ponderacionMeta = new List<E_PONDERACION_META>();
            foreach (GridDataItem item in grdEvaluados.MasterTableView.Items)
            {
                int id_empleado = Convert.ToInt32(item.GetDataKeyValue("ID_EMPLEADO").ToString());
                int id_evaluado = Convert.ToInt32(item.GetDataKeyValue("ID_EVALUADO").ToString());
                string cl_empleado = (item.GetDataKeyValue("CL_EMPLEADO").ToString());
                string nb_empleado = (item.GetDataKeyValue("NB_EMPLEADO_COMPLETO").ToString());
                string nb_puesto = (item.GetDataKeyValue("NB_PUESTO").ToString());
                string nb_area = (item.GetDataKeyValue("NB_DEPARTAMENTO").ToString());
                RadNumericTextBox txtPonderacion = (RadNumericTextBox)item.FindControl("txtPonderacion");
                decimal ponderacion = txtPonderacion.Text != "" ? Convert.ToDecimal(txtPonderacion.Text) : 0;

                E_PONDERACION_META ponderacionM = new
                E_PONDERACION_META
                {
                    ID_EMPLEADO = id_empleado,
                    ID_EVALUADO = id_evaluado,
                    CL_EMPLEADO = cl_empleado,
                    NB_EMPLEADO = nb_empleado,
                    NB_PUESTO = nb_puesto,
                    NB_AREA = nb_area,
                    PR_PONDERACION = ponderacion
                };

                ponderacionMeta.Add(ponderacionM);
                var vXelements = ponderacionMeta.Select(x =>
                                        new XElement("EVALUADO",
                                        new XAttribute("ID_EMPLEADO", x.ID_EMPLEADO),
                                        new XAttribute("ID_EVALUADO", x.ID_EVALUADO),
                                        new XAttribute("CL_EMPLEADO", x.CL_EMPLEADO),
                                        new XAttribute("NB_EMPLEADO_COMPLETO", x.NB_EMPLEADO),
                                        new XAttribute("NB_PUESTO", x.NB_PUESTO),
                                        new XAttribute("NB_DEPARTAMENTO", x.NB_AREA),
                                        new XAttribute("PR_EVALUADO", x.PR_PONDERACION)
                            ));
                RESULTADOS =
                new XElement("EVALUADOS", vXelements
                );
            }

            decimal sumaPonderacion = 0;
            foreach (E_PONDERACION_META item in ponderacionMeta)
            {
                sumaPonderacion = sumaPonderacion + Convert.ToDecimal(item.PR_PONDERACION);
            }

            decimal totalPonderacion = sumaPonderacion;
            if (totalPonderacion > 100 || totalPonderacion < 100)
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "La suma de la ponderación debe ser igual a 100", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                return;
            }

            var evaluadosEvaluador = nPeriodo.ObtieneEvaluados(pIdPeriodo: vIdPeriodo);
            foreach (var evaluador in evaluadosEvaluador)
            {
                int? ev = evaluador.NO_EVALUADOR;
                string nb = evaluador.NB_EMPLEADO_COMPLETO;
                if (ev == 0)
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, "El evaluado " + nb + " no tiene evaluador", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                    return;
                }
            }

            PeriodoDesempenoNegocio neg = new PeriodoDesempenoNegocio();
            E_RESULTADO vResultado = neg.ActualizaPonderacionEvaluados(vIdPeriodo, RESULTADOS.ToString(), "ACTUALIZA", vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
            grdEvaluados.Rebind();
            
                



        }

        protected void grdDisenoMetas_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                GridDataItem item = e.Item as GridDataItem;
                int idEvaluado = Convert.ToInt32(item.GetDataKeyValue("ID_EVALUADO").ToString());
                int idMeta = Convert.ToInt32(item.GetDataKeyValue("ID_EVALUADO_META").ToString());
                E_RESULTADO vResultado = nPeriodo.EliminaMetaEvaluado(vIdPeriodo, idMeta, idEvaluado, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "recargarMetas()");
            }

        }

        protected void btnGuardarMetas_Click(object sender, EventArgs e)
        {
            PeriodoDesempenoNegocio negocio = new PeriodoDesempenoNegocio();
            List<SPE_PONDERACION_METAS_DESEMPENO_Result> listaMetas = negocio.ObtienePonderacionMetas(vIdPeriodo);

            foreach (var ponderacion in listaMetas)
            {
                grdDisenoMetas.Rebind();
                if (ponderacion.PR_META > 100)
                {
                    foreach (GridDataItem item in grdDisenoMetas.MasterTableView.Items)
                    {
                        if (item.Selected == true)
                        {
                            item.Selected = false;
                        }
                        var vIdEmpleado = int.Parse(item.GetDataKeyValue("ID_EVALUADO").ToString());
                        if (vIdEmpleado == ponderacion.ID_EVALUADO)
                        {
                            item.Selected = true;
                            item.Focus();
                        }
                    }
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, "La ponderación de las metas de un evaluado rebasa el 100%", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                    return;
                }
                if (ponderacion.PR_META < 100)
                {
                    foreach (GridDataItem item in grdDisenoMetas.MasterTableView.Items)
                    {
                        var vIdEmpleado = int.Parse(item.GetDataKeyValue("ID_EVALUADO").ToString());
                        if (vIdEmpleado == ponderacion.ID_EVALUADO)
                        {
                            item.Selected = true;
                            item.Focus();
                        }
                    }
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, "La ponderación de las metas de un evaluado es menor que el 100%", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                    return;
                }
            }
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            E_RESULTADO vResultado = nPeriodo.EliminaMetaInactivas(vIdPeriodo);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "recargarMetas()");

            //UtilMensajes.MensajeResultadoDB(rwmMensaje, "Proceso exitoso", E_TIPO_RESPUESTA_DB.SUCCESSFUL, pCallBackFunction: null);
        }

        protected void grdContrasenaEvaluadores_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            grdContrasenaEvaluadores.DataSource = nPeriodo.ObtieneTokenEvaluadores(pIdPeriodo: vIdPeriodo , pIdRol: vIdRol);
        }

        protected void btnReasignarTodasContrasenas_Click(object sender, EventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();

            E_RESULTADO vResultado = nPeriodo.InsertarActualizarTokenEvaluadores(vIdPeriodo, null, vClUsuario, vNbPrograma, pIdRol: vIdRol);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                grdContrasenaEvaluadores.Rebind();
        }

        protected void btnReasignarContrasena_Click(object sender, EventArgs e)
        {

            string vMensaje = "";
            if (grdContrasenaEvaluadores.SelectedItems.Count > 0)
            {
                foreach (GridDataItem item in grdContrasenaEvaluadores.SelectedItems)
                {
                    PeriodoNegocio nPeriodo = new PeriodoNegocio();

                    E_RESULTADO vResultado = nPeriodo.InsertarActualizarTokenEvaluadores(vIdPeriodo, int.Parse(item.GetDataKeyValue("ID_EVALUADOR").ToString()), vClUsuario, vNbPrograma, pIdRol: vIdRol);
                    vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    if (!vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                    {
                        //grdContrasenaEvaluadores.Rebind();
                        UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
                        return;
                    }
                }

                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, E_TIPO_RESPUESTA_DB.SUCCESSFUL, pCallBackFunction: null);
                grdContrasenaEvaluadores.Rebind();
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Selecciona por lo menos un evaluador para reasignar contraseña.", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: null);
            }

        }

        protected void grdEvaluados_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem vDataItem = (GridDataItem)e.DetailTableView.ParentItem;

            if (e.DetailTableView.Name == "gtvEvaluadores")
            {
                int vIdEmpleado;
                PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                vIdEmpleado = int.Parse(vDataItem.GetDataKeyValue("ID_EVALUADO").ToString());
                e.DetailTableView.DataSource = nPeriodo.ObtieneEvaluadoresPorEvaluado(vIdPeriodo, vIdEmpleado);
            }
        }

        protected void btnEliminarEvaluado_Click(object sender, EventArgs e)
        {
            XElement vXmlEvaluados = new XElement("EMPLEADOS");
            foreach (GridDataItem item in grdEvaluados.SelectedItems)
                vXmlEvaluados.Add(new XElement("EMPLEADO", new XAttribute("ID_EMPLEADO", item.GetDataKeyValue("ID_EMPLEADO").ToString())));

            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            E_RESULTADO vResultado = nPeriodo.EliminaEvaluados(vIdPeriodo, vXmlEvaluados, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
            {
                grdEvaluados.Rebind();
                grdDisenoMetas.Rebind();
                grdContrasenaEvaluadores.Rebind();
                rgBono.Rebind();
            }
        }

        protected void btnActivarMetas_Click(object sender, EventArgs e)
        {
            vListaMetasEvaluado = new List<E_METAS_EVALUADO>();
            foreach (GridDataItem item in grdDisenoMetas.SelectedItems)
            {
                if (item.OwnerTableView.Name == "gtvMetas")
                {
                    int vIdEvaluadoMeta = (int.Parse(item.GetDataKeyValue("ID_EVALUADO_META").ToString()));

                    vListaMetasEvaluado.Add(new E_METAS_EVALUADO
                    {
                        ID_EVALUADO_META = vIdEvaluadoMeta
                    });
                }
            }
            var vXelements = vListaMetasEvaluado.Select(x =>
                                 new XElement("METAS",
                                  new XAttribute("ID_METAS_EVALUADO", x.ID_EVALUADO_META),
                                  new XAttribute("FG_ESTATUS", "1")
                       )
                      );

            SeleccionMetasEvaluado =
            new XElement("SELECCION", vXelements
            );

            if (vListaMetasEvaluado.Count > 0)
            {
                PeriodoDesempenoNegocio Negocio = new PeriodoDesempenoNegocio();
                E_RESULTADO vResultado = Negocio.ActualizarEvaluadoMetas(SeleccionMetasEvaluado.ToString(), vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "recargarMetas()");
            }
            else
            {
                string vTstoIndicadorMeta = " una meta.";
                if (vClTipoMetas == "DESCRIPTIVO")
                    vTstoIndicadorMeta = " un indicador.";

                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Seleccione" + vTstoIndicadorMeta, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            }
        }

        protected void btnDesactivarMetas_Click(object sender, EventArgs e)
        {
            vListaMetasEvaluado = new List<E_METAS_EVALUADO>();
            foreach (GridDataItem item in grdDisenoMetas.SelectedItems)
            {
                if (item.OwnerTableView.Name == "gtvMetas")
                {
                    int vIdEvaluadoMeta = (int.Parse(item.GetDataKeyValue("ID_EVALUADO_META").ToString()));

                    vListaMetasEvaluado.Add(new E_METAS_EVALUADO
                    {
                        ID_EVALUADO_META = vIdEvaluadoMeta
                    });
                }
            }
            var vXelements = vListaMetasEvaluado.Select(x =>
                                 new XElement("METAS",
                                  new XAttribute("ID_METAS_EVALUADO", x.ID_EVALUADO_META),
                                  new XAttribute("FG_ESTATUS", "0")
                       )
                      );

            SeleccionMetasEvaluado =
            new XElement("SELECCION", vXelements
            );

            if (vListaMetasEvaluado.Count > 0)
            {
                PeriodoDesempenoNegocio Negocio = new PeriodoDesempenoNegocio();
                E_RESULTADO vResultado = Negocio.ActualizarEvaluadoMetas(SeleccionMetasEvaluado.ToString(), vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "recargarMetas()");
            }
            else
            {
                string vTstoIndicadorMeta = " una meta.";
                if (vClTipoMetas == "DESCRIPTIVO")
                    vTstoIndicadorMeta = " un indicador.";

                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Seleccione" + vTstoIndicadorMeta, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            }

        }

        protected void btnCopiarMetas_Click(object sender, EventArgs e)
        {
            vListaMetas = new List<E_META>();
            vListaMetasEvaluado = new List<E_METAS_EVALUADO>();
            foreach (GridDataItem item in grdDisenoMetas.SelectedItems)
            {
                if (item.OwnerTableView.Name == "gtvMetas")
                {
                    int vIdEvaluadoMeta = (int.Parse(item.GetDataKeyValue("ID_EVALUADO_META").ToString()));
                    vListaMetasEvaluado.Add(new E_METAS_EVALUADO
                        {
                            ID_EVALUADO_META = vIdEvaluadoMeta
                        }
                            );
                }
                else
                {
                    int vIdEvaluado = (int.Parse(item.GetDataKeyValue("ID_EVALUADO").ToString()));
                    vListaMetas.Add(new E_META
                    {
                        ID_EVALUADO = vIdEvaluado
                    });
                }
            }

            var xElements1 = vListaMetasEvaluado.Select(x => new XElement("EVALUADOS_METAS",
                                                    new XAttribute("ID_EVALUADO_META", x.ID_EVALUADO_META)));

            var xElements2 = vListaMetas.Select(x => new XElement("EVALUADO",
                                                    new XAttribute("ID_EVALUADO", x.ID_EVALUADO)));

            SeleccionMetasEvaluado =
             new XElement("EVALUADOS", new XElement("METAS_EVALUADOS", xElements1), new XElement("EVALUADO", xElements2));

            if (vListaMetasEvaluado.Count > 0 && vListaMetas.Count > 0)
            {
                PeriodoDesempenoNegocio Negocio = new PeriodoDesempenoNegocio();
                E_RESULTADO vResultado = Negocio.InsertaCopiaMetas(SeleccionMetasEvaluado.ToString(), vIdPeriodo, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "recargarMetas()");
            }
            else
            {
                string vTstoIndicadorMeta = " una meta.";
                if(vClTipoMetas=="DESCRIPTIVO")
                    vTstoIndicadorMeta = " un indicador.";

                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Seleccione por lo menos un evaluado y" + vTstoIndicadorMeta, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            }
        }

        protected void txtPonderacion_TextChanged(object sender, EventArgs e)
        {
        //    decimal vTotal = 0;
        //    foreach (GridDataItem item in grdEvaluados.Items)
        //    {
        //        RadNumericTextBox radNumericTxtBox = (RadNumericTextBox)item["PR_EVALUADO"].FindControl("txtPonderacion");
        //        if (radNumericTxtBox != null)
        //        {
        //            if (radNumericTxtBox.Text != "")
        //                vTotal = vTotal + decimal.Parse(radNumericTxtBox.Text);
        //        }
        //    }    
        }

        protected void rgBono_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgBono.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgBono.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgBono.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgBono.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgBono.MasterTableView.ClientID);
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

        protected void grdEvaluados_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem && e.Item.OwnerTableView.Name != "gtvEvaluadores")
            {
                GridDataItem item = (GridDataItem)e.Item;
          
                string vClEstadoEmpleado = item.GetDataKeyValue("CL_ESTADO_EMPLEADO").ToString();
                int vIdEvaluado = int.Parse(item.GetDataKeyValue("ID_EVALUADO").ToString());
                if (vClEstadoEmpleado != null && vClEstadoEmpleado != "Alta")
                {
                    //item["CL_ESTADO_EMPLEADO"].Text = "<a href='javascript:OpenRemplazaBaja(" + vIdEvaluado + "," + vIdPeriodo + ")'>" + vClEstadoEmpleado + "</a>";
                    item["CL_ESTADO_EMPLEADO"].Text = vClEstadoEmpleado;
                }

            }
        }
    }
}

