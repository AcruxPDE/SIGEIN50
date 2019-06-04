using SIGE.Entidades.EvaluacionOrganizacional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using Telerik.Web.UI;
using SIGE.Negocio.FormacionDesarrollo;
using WebApp.Comunes;
using System.Xml;
using SIGE.Entidades.Administracion;

namespace SIGE.WebApp.EO
{
    public partial class ConfiguracionClima : System.Web.UI.Page
    {

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdRol;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private string vNbFirstRadEditorTagName = "p";

        public string vXmlFiltrosSel
        {
            get { return (string)ViewState["vs_vXmlFiltros"]; }
            set { ViewState["vs_vXmlFiltros"] = value; }
        }

        public bool? vFgFiltroSeleccionado
        {
            get { return (bool?)ViewState["vs_vFgFiltroSeleccionado"]; }
            set { ViewState["vs_vFgFiltroSeleccionado"] = value; }
        }

        public int vIdPeriodo
        {
            get { return (int)ViewState["vs_vIdPeriodo"]; }
            set { ViewState["vs_vIdPeriodo"] = value; }
        }

        public string vClTipoPeriodo
        {
            get { return (string)ViewState["vs_vClTipoPeriodo"]; }
            set { ViewState["vs_vClTipoPeriodo"] = value; }
        }

        private List<E_EVALUADORES_CLIMA> vlstEvaluavores
        {
            get { return (List<E_EVALUADORES_CLIMA>)ViewState["vs_vlstEvaluavores"]; }
            set { ViewState["vs_vlstEvaluavores"] = value; }
        }

        private List<E_PREGUNTAS_PERIODO_CLIMA> vlstPreguntasPeriodo
        {
            get { return (List<E_PREGUNTAS_PERIODO_CLIMA>)ViewState["vs_vlstPreguntasPeriodo"]; }
            set { ViewState["vs_vlstPreguntasPeriodo"] = value; }
        }

        private List<E_DEPARTAMENTOS> vLstDepartamentos
        {
            get { return (List<E_DEPARTAMENTOS>)ViewState["vs_vLstDepartamentos"]; }
            set { ViewState["vs_vLstDepartamentos"] = value; }
        }

        private List<E_GENERO> vLstGeneros
        {
            get { return (List<E_GENERO>)ViewState["vs_vLstGeneros"]; }
            set { ViewState["vs_vLstGeneros"] = value; }
        }

        private List<E_ADICIONALES_SELECCIONADOS> vLstAdicionales
        {
            get { return (List<E_ADICIONALES_SELECCIONADOS>)ViewState["vs_vLstAdicionales"]; }
            set { ViewState["vs_vLstAdicionales"] = value; }
        }

        RadListBoxItem vNoSeleccionado = new RadListBoxItem("No seleccionado", String.Empty);

        #endregion

        #region Funciones

        public void FiltrosIndice()
        {
            List<E_SELECCIONADOS> vDepartamentos = new List<E_SELECCIONADOS>();
            List<E_ADICIONALES_SELECCIONADOS> vAdicionales = new List<E_ADICIONALES_SELECCIONADOS>();
            List<E_GENERO> vLstGenero = new List<E_GENERO>();
            XElement vXlmFiltros = new XElement("FILTROS");
            XElement vXlmDepartamentos = new XElement("DEPARTAMENTOS");
            XElement vXlmGeneros = new XElement("GENEROS");
            XElement vXlmEdad = new XElement("EDAD");
            XElement vXlmAntiguedad = new XElement("ANTIGUEDAD");
            XElement vXlmCamposAdicional = new XElement("CAMPOS_ADICIONALES");
            vFgFiltroSeleccionado = false;

            //if (chkarea.Checked == true)
            //{
            foreach (RadListBoxItem item in rlbDepartamento.Items)
            {
                int vIdDepartamento = int.Parse(item.Value);
                vDepartamentos.Add(new E_SELECCIONADOS { ID_SELECCIONADO = vIdDepartamento });
                vFgFiltroSeleccionado = true;
            }
            var vXelements = vDepartamentos.Select(x => new XElement("DEPARTAMENTO", new XAttribute("ID_DEPARTAMENTO", x.ID_SELECCIONADO)));
            vXlmDepartamentos = new XElement("DEPARTAMENTOS", vXelements);
            vXlmFiltros.Add(vXlmDepartamentos);
            // }

            //if (chkadicionales.Checked == true)
            //{
            foreach (RadListBoxItem item in rlbAdicionales.Items)
            {
                string vClAdicional = item.Value;
                vAdicionales.Add(new E_ADICIONALES_SELECCIONADOS { CL_CAMPO = vClAdicional });
                vFgFiltroSeleccionado = true;
            }
            var vXelementsAdicionales = vAdicionales.Select(x => new XElement("ADICIONAL", new XAttribute("CL_CAMPO", x.CL_CAMPO)));
            vXlmCamposAdicional = new XElement("ADICIONALES", vXelementsAdicionales);
            vXlmFiltros.Add(vXlmCamposAdicional);
            //}

            //if (chkgenero.Checked == true)
            //{
            foreach (RadListBoxItem item in rlbGenero.Items)
            {
                string vClGenero = item.Value;
                vLstGenero.Add(new E_GENERO { CL_GENERO = vClGenero });
                vFgFiltroSeleccionado = true;
            }
            var vXmlLstGeneros = vLstGenero.Select(x => new XElement("GENERO", new XAttribute("NB_GENERO", x.CL_GENERO)));
            vXlmGeneros = new XElement("GENEROS", vXmlLstGeneros);
            vXlmFiltros.Add(vXlmGeneros);
            // }

            if (rbEdad.Checked == true)
            {

                vXlmEdad = new XElement("EDAD", new XAttribute("EDAD_INICIAL", rntEdadInicial.Text), new XAttribute("EDAD_FINAL", rntEdadFinal.Text));
                vXlmFiltros.Add(vXlmEdad);
                vFgFiltroSeleccionado = true;
            }

            if (rbAntiguedad.Checked == true)
            {

                vXlmAntiguedad = new XElement("ANTIGUEDAD", new XAttribute("ANTIGUEDAD_INICIAL", rntAntiguedadInicial.Text), new XAttribute("ANTIGUEDAD_FINAL", rntAntiguedadFinal.Text));
                vXlmFiltros.Add(vXlmAntiguedad);
                vFgFiltroSeleccionado = true;
            }
            if (vFgFiltroSeleccionado != false)
                vXmlFiltrosSel = vXlmFiltros.ToString();
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
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            E_RESULTADO vResultado = nClima.InsertaActualizaEvaluadorClima(vIdPeriodo, pXmlElementos.ToString(), vClUsuario, vNbPrograma, E_TIPO_OPERACION_DB.I.ToString());
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindowE");
            GenerarContraseñas();
            grdEmpleadosContrasenias.Rebind();
        }

        protected bool ValidaEvaluados()
        {
            bool vFgEvaluadores = true;
            if (vClTipoPeriodo == "EVALUADORES")
                if (grdEmpleadosSeleccionados.Items.Count < 1)
                    vFgEvaluadores = false;

            return vFgEvaluadores;
        }

        protected void CargarDatosEvaluador()
        {

            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            vlstEvaluavores = nClima.ObtieneEvaluadoresClima(pID_PERIODO: vIdPeriodo, pID_ROL: vIdRol).Select(s => new E_EVALUADORES_CLIMA
            {
                NB_EVALUADOR = s.NB_EVALUADOR,
                NB_PUESTO = s.NB_PUESTO,
                CL_TIPO_EVALUADOR = s.CL_TIPO_EVALUADOR,
                CL_EMPLEADO = s.CL_EMPLEADO,
                ID_EMPLEADO = s.ID_EMPLEADO,
                CL_CORREO_ELECTRONICO = s.CL_CORREO_ELECTRONICO,
                ID_PUESTO = s.ID_PUESTO,
                ID_EVALUADOR = s.ID_EVALUADOR,
                CL_TOKEN = s.CL_TOKEN,
                NB_DEPARTAMENTO = s.NB_DEPARTAMENTO
            }).ToList();

        }

        private static Boolean ValidarRamaXml(XElement parentEl, string elementsName)
        {
            var foundEl = parentEl.Element(elementsName);
            if (foundEl != null)
            {
                return true;
            }

            return false;
        }

        protected string ObtieneGeneros(string pXmlGenros)
        {
            string vGeneros = "";
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(pXmlGenros);
            XmlNodeList generos = xml.GetElementsByTagName("ITEMS");
            vLstGeneros = new List<E_GENERO>();

            XmlNodeList lista =
            ((XmlElement)generos[0]).GetElementsByTagName("ITEM");

            foreach (XmlElement nodo in lista)
            {

                vGeneros = vGeneros + nodo.GetAttribute("NB_GENERO") + ".\n";
                E_GENERO f = new E_GENERO
                {
                    CL_GENERO = nodo.GetAttribute("CL_GENERO"),
                    NB_GENERO = nodo.GetAttribute("NB_GENERO")
                };
                vLstGeneros.Add(f);
            }


            return vGeneros;
        }

        protected string ObtieneDepartamentos(string pXmlDepartamentos)
        {
            string vDepartamentos = "";
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(pXmlDepartamentos);
            XmlNodeList departamentos = xml.GetElementsByTagName("ITEMS");
            vLstDepartamentos = new List<E_DEPARTAMENTOS>();

            XmlNodeList lista =
            ((XmlElement)departamentos[0]).GetElementsByTagName("ITEM");

            foreach (XmlElement nodo in lista)
            {

                vDepartamentos = vDepartamentos + nodo.GetAttribute("NB_DEPARTAMENTO") + ".\n";
                E_DEPARTAMENTOS f = new E_DEPARTAMENTOS
                {
                    ID_DEPARTAMENTO = nodo.GetAttribute("ID_DEPARTAMENTO"),
                    NB_DEPARTAMENTO = nodo.GetAttribute("NB_DEPARTAMENTO")
                };
                vLstDepartamentos.Add(f);
            }


            return vDepartamentos;
        }

        protected string ObtieneAdicionales(string pXmlAdicionales)
        {
            string vAdicionales = "";
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(pXmlAdicionales);
            XmlNodeList departamentos = xml.GetElementsByTagName("ITEMS");
            vLstAdicionales = new List<E_ADICIONALES_SELECCIONADOS>();

            XmlNodeList lista =
            ((XmlElement)departamentos[0]).GetElementsByTagName("ITEM");

            foreach (XmlElement nodo in lista)
            {

                vAdicionales = vAdicionales + nodo.GetAttribute("NB_CAMPO") + ".\n";

                E_ADICIONALES_SELECCIONADOS f = new E_ADICIONALES_SELECCIONADOS
                {
                    CL_CAMPO = nodo.GetAttribute("CL_CAMPO"),
                    NB_CAMPO = nodo.GetAttribute("NB_CAMPO")
                };
                vLstAdicionales.Add(f);

            }


            return vAdicionales;
        }

        protected void CargarCamposExtra()
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            int countFiltros = nClima.ObtenerFiltrosEvaluadores(vIdPeriodo).Count;
            if (countFiltros > 0)
            {
                var vFiltros = nClima.ObtenerParametrosFiltros(vIdPeriodo).FirstOrDefault();
                if (vFiltros != null)
                {
                    if (vFiltros.EDAD_INICIO != null)
                    {
                        //lbedad.Visible = true;
                        //txtEdad.Visible = true;
                        //txtEdad.Attributes.Add("class", "ctrlTableDataBorderContext");
                        //txtEdad.InnerText = vFiltros.EDAD_INICIO + " a " + vFiltros.EDAD_FINAL + " años";
                        rbEdad.Checked = true;
                        rntEdadInicial.Value = vFiltros.EDAD_INICIO;
                        rntAntiguedadFinal.Value = vFiltros.EDAD_FINAL;
                    }
                    if (vFiltros.ANTIGUEDAD_INICIO != null)
                    {
                        //lbAntiguedad.Visible = true;
                        //txtAntiguedad.Visible = true;
                        //txtAntiguedad.Attributes.Add("class", "ctrlTableDataBorderContext");
                        //txtAntiguedad.InnerText = vFiltros.ANTIGUEDAD_INICIO + " a " + vFiltros.ANTIGUEDAD_FINAL + " años";
                        rbAntiguedad.Checked = true;
                        rntAntiguedadInicial.Value = vFiltros.ANTIGUEDAD_INICIO;
                        rntAntiguedadFinal.Value = vFiltros.ANTIGUEDAD_FINAL;
                    }
                    if (vFiltros.CL_GENERO != null)
                    {
                        //lbGenero.Visible = true;
                        //txtGenero.Visible = true;
                        //txtGenero.Attributes.Add("class", "ctrlTableDataBorderContext");
                        //List<E_GENERO> vLstGenero = new List<E_GENERO>();

                        //if (vFiltros.CL_GENERO == "Masculino")
                        //{
                        // //   txtGenero.InnerText = "Masculino";
                        //    E_GENERO g = new E_GENERO
                        //    {
                        //        CL_GENERO = "M",
                        //        NB_GENERO = "Masculino"
                        //    };
                        //    vLstGenero.Add(g);


                        //}
                        //else if (vFiltros.CL_GENERO == "Masculino")
                        //{
                        //   // txtGenero.InnerText = "Femenino";
                        //    E_GENERO g = new E_GENERO
                        //    {
                        //        CL_GENERO = "F",
                        //        NB_GENERO = "Femenino"
                        //    };
                        //    vLstGenero.Add(g);
                        //}
                        ObtieneGeneros(vFiltros.CL_GENERO);
                        rlbGenero.DataSource = vLstGeneros;
                        rlbGenero.DataTextField = "NB_GENERO";
                        rlbGenero.DataValueField = "CL_GENERO";
                        rlbGenero.DataBind();

                        //  chkgenero.Checked = true;
                    }

                    if (vFiltros.XML_DEPARTAMENTOS != null)
                    {
                        //lbDepartamento.Visible = true;
                        //rlDepartamento.Visible = true;
                        //rlDepartamento.Attributes.Add("class", "ctrlTableDataBorderContext");
                        //rlDepartamento.Text = ObtieneDepartamentos(vFiltros.XML_DEPARTAMENTOS);
                        ObtieneDepartamentos(vFiltros.XML_DEPARTAMENTOS);
                        //  chkarea.Checked = true;
                        rlbDepartamento.DataSource = vLstDepartamentos;
                        rlbDepartamento.DataTextField = "NB_DEPARTAMENTO";
                        rlbDepartamento.DataValueField = "ID_DEPARTAMENTO";
                        rlbDepartamento.DataBind();
                    }

                    if (vFiltros.XML_CAMPOS_ADICIONALES != null)
                    {
                        //lbAdscripciones.Visible = true;
                        //rlAdicionales.Visible = true;
                        //rlAdicionales.Attributes.Add("class", "ctrlTableDataBorderContext");
                        //rlAdicionales.Text = ObtieneAdicionales(vFiltros.XML_CAMPOS_ADICIONALES);
                        ObtieneAdicionales(vFiltros.XML_CAMPOS_ADICIONALES);
                        // chkadicionales.Checked = true;
                        rlbAdicionales.DataSource = vLstAdicionales;
                        rlbAdicionales.DataTextField = "NB_CAMPO";
                        rlbAdicionales.DataValueField = "CL_CAMPO";
                        rlbAdicionales.DataBind();
                    }

                }
            }
        }

        protected void GenerarContraseñas()
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            E_RESULTADO vResultado = nClima.InsertarActualizarTokenEvaluadoresClima(vIdPeriodo, null, vClUsuario, vNbPrograma);
            //string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            //UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                grdEmpleadosContrasenias.Rebind();
        }

        private void SeguridadProcesos(bool pFgCuestionariosCreados)
        {
            btnEnvioCuestionarios.Enabled = ContextoUsuario.oUsuario.TienePermiso("L.A.A.I") && pFgCuestionariosCreados;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["PeriodoId"] != null)
                {
                    vIdPeriodo = int.Parse((Request.QueryString["PeriodoId"]));
                    ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
                    var vClima = nClima.ObtienePeriodosClima(pIdPerido: vIdPeriodo).FirstOrDefault();
                    txtClPeriodo.InnerText = vClima.CL_PERIODO;
                    txtDsPeriodo.InnerText = vClima.DS_PERIODO;
                    txtEstatus.InnerText = vClima.CL_ESTADO_PERIODO;
                    reInstrucciones.Content = vClima.DS_INSTRUCCIONES;
                    lMensaje.Content = vClima.DS_MENSAJE_CORREO;
                    vClTipoPeriodo = vClima.CL_TIPO_CONFIGURACION;

                    if (vClima.CL_TIPO_CONFIGURACION == "PARAMETROS")
                        txtTipo.InnerText = "Sin asignación de evaluadores";
                    else
                        txtTipo.InnerText = "Evaluadores asignados";

                    if (vClima.DS_NOTAS != null)
                    {
                        if (vClima.DS_NOTAS.Contains("DS_NOTA"))
                        {
                            txtNotas.InnerHtml = Utileria.MostrarNotas(vClima.DS_NOTAS);
                        }
                        else
                        {
                            XElement vRequerimientos = XElement.Parse(vClima.DS_NOTAS);
                            if (vRequerimientos != null)
                            {
                                vRequerimientos.Name = vNbFirstRadEditorTagName;
                                txtNotas.InnerHtml = vRequerimientos.ToString();
                            }
                        }
                    }
                    if (vClima.CL_ORIGEN_CUESTIONARIO == "PREDEFINIDO")
                    {
                        btnValidez.Enabled = false;
                        lbCuestionario.InnerText = "Predefinido de SIGEIN";
                    }
                    if (vClima.CL_ORIGEN_CUESTIONARIO == "COPIA")
                    {
                        btnValidez.Enabled = true;
                        lbCuestionario.InnerText = "Copia de otro período";
                    }
                    if (vClima.CL_ORIGEN_CUESTIONARIO == "VACIO")
                    {
                        btnValidez.Enabled = true;
                        lbCuestionario.InnerText = "Creado en blanco";
                    }


                    if (vClima.FG_CUESTIONARIOS_CREADOS.Value)
                    {
                        btnEditar.Enabled = false;
                        btnEliminar.Enabled = false;
                        btnAgregarPregunta.Enabled = false;
                        divMensajeCuestionarios.Visible = true;
                        btnValidez.Enabled = false;
                        btnCrearCuestionarios.Enabled = false;

                        btnAgregarAbierta.Enabled = false;
                        btnEditarAbierta.Enabled = false;
                        btnEliminarAbierta.Enabled = false;
                        dvPreguntasAbiertas.Visible = true;

                        btnSeleccionar.Enabled = false;
                        btnSeleccionarPuesto.Enabled = false;
                        btmSleccionarArea.Enabled = false;
                        btnEliminarEvaluador.Enabled = false;
                        btnAplicar.Enabled = false;
                        btnGuardarPreguntasAbiertas.Enabled = false;
                        btnGuardarCuestionario.Enabled = false;
                        btnGuardarCerrar.Visible = false;
                        btnCerrar.Visible = true;
                    }
                    else
                    {
                        divMensajeCuestionarios.Visible = false;
                    }

                    //var vPreguntasAbiertas = nClima.ObtenerPreguntasAbiertas(vIdPeriodo).FirstOrDefault();
                    //if (vPreguntasAbiertas != null && vPreguntasAbiertas.FG_ESTATUS == "ASIGNADA")
                    //{
                    //    btnAgregarAbierta.Enabled = false;
                    //    btnEditarAbierta.Enabled = false;
                    //    btnEliminarAbierta.Enabled = false;
                    //    dvPreguntasAbiertas.Visible = true;
                    //}

                    CargarCamposExtra();


                    //lMensaje.Content = ContextoApp.EO.MensajeCorreoEvaluador.dsMensaje;

                    if (vClima.CL_TIPO_CONFIGURACION == "EVALUADORES")
                    {
                        rtsConfiguracionClima.Tabs[2].Visible = false;
                    }
                    else
                    {
                        rtsConfiguracionClima.Tabs[1].Visible = false;
                        rtsConfiguracionClima.Tabs[6].Visible = false;
                        //btnGuardar.Enabled = false;
                    }


                    SeguridadProcesos((bool)vClima.FG_CUESTIONARIOS_CREADOS);

                }
            }
        }

        protected void ramConfiguracionPeriodoClima_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
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

        }

        protected void grdEmpleadosSeleccionados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            // ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            //int vCount = nClima.ObtenerFiltrosEvaluadores(vIdPeriodo).Count;
            //if (vCount < 1)
            //{
            CargarDatosEvaluador();
            grdEmpleadosSeleccionados.DataSource = vlstEvaluavores;
            //}
            //else
            //{
            //    vlstEvaluavores = new List<E_EVALUADORES_CLIMA>();
            //    grdEmpleadosSeleccionados.DataSource = vlstEvaluavores;
            //}


        }

        protected void btnCrearCuestionarios_Click(object sender, EventArgs e)
        {
            if (ValidaEvaluados())
            {
                ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
                E_RESULTADO vResultado = nClima.InsertaActualizaCuestionariosPeriodo(vIdPeriodo, vClUsuario, vNbPrograma);
                if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                {
                    E_RESULTADO vResultadoPreguntas = nClima.InsertaCuestionarioPreguntasAbiertas(vIdPeriodo, vClUsuario, vNbPrograma);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");

                    if (vResultadoPreguntas.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                    {
                        btnEditar.Enabled = false;
                        btnEliminar.Enabled = false;
                        btnAgregarPregunta.Enabled = false;
                        btnAgregarAbierta.Enabled = false;
                        btnEliminarAbierta.Enabled = false;
                        btnEditarAbierta.Enabled = false;
                        btnValidez.Enabled = false;
                        btnCrearCuestionarios.Enabled = false;

                        btnSeleccionar.Enabled = false;
                        btnSeleccionarPuesto.Enabled = false;
                        btmSleccionarArea.Enabled = false;
                        btnEliminarEvaluador.Enabled = false;
                        btnAplicar.Enabled = false;
                        btnGuardarPreguntasAbiertas.Enabled = false;
                        btnGuardarCuestionario.Enabled = false;
                        btnGuardarCerrar.Visible = false;
                        btnCerrar.Visible = true;

                        btnSeleccionar.Enabled = false;
                        btnSeleccionarPuesto.Enabled = false;
                        btnEliminarEvaluador.Enabled = false;
                        btmSleccionarArea.Enabled = false;

                        SeguridadProcesos(true);
                    }
                }
                else
                {
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
                }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Debes seleccionar evaluadores para crear los cuestionarios.", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "");
            }

        }

        protected void grdPreguntasCuestionario_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            vlstPreguntasPeriodo = nClima.ObtienePreguntasPeriodo(pID_PERIODO: vIdPeriodo).Select(s => new E_PREGUNTAS_PERIODO_CLIMA
            {
                ID_PREGUNTA = s.ID_PREGUNTA,
                NB_DIMENSION = s.NB_DIMENSION,
                NB_TEMA = s.NB_TEMA,
                NO_SECUENCIA = s.NO_SECUENCIA,
                NB_PREGUNTA = s.NB_PREGUNTA
            }).ToList();
            grdPreguntasCuestionario.DataSource = vlstPreguntasPeriodo;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            XElement vXmlPreguntas = new XElement("PREGUNTAS");
            foreach (GridDataItem item in grdPreguntasCuestionario.SelectedItems)
                vXmlPreguntas.Add(new XElement("PREGUNTA",
                                  new XAttribute("ID_PREGUNTA", item.GetDataKeyValue("ID_PREGUNTA").ToString()),
                                  new XAttribute("ID_PERIODO", vIdPeriodo)));


            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            E_RESULTADO vResultado = nClima.EliminaPreguntasPeriodo(vClUsuario, vNbPrograma, vXmlPreguntas.ToString());
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindowP");
        }

        protected void btnEliminarEvaluador_Click(object sender, EventArgs e)
        {
            XElement vXmlEvaluadores = new XElement("EVALUADORES");
            foreach (GridDataItem item in grdEmpleadosSeleccionados.SelectedItems)
                vXmlEvaluadores.Add(new XElement("EVALUADOR",
                                  new XAttribute("ID_EVALUADOR", item.GetDataKeyValue("ID_EVALUADOR").ToString()),
                                  new XAttribute("ID_PERIODO", vIdPeriodo)));

            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            E_RESULTADO vResultado = nClima.EliminaEvaluadoresPeriodo(vXmlEvaluadores.ToString(), vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindowE");

            grdEmpleadosContrasenias.Rebind();

        }

        protected void btnReasignarTodasContrasenas_Click(object sender, EventArgs e)
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            E_RESULTADO vResultado = nClima.InsertarActualizarTokenEvaluadoresClima(vIdPeriodo, null, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                grdEmpleadosContrasenias.Rebind();

        }

        protected void btnReasignarContrasena_Click(object sender, EventArgs e)
        {
            string vMensaje = "";
            if (grdEmpleadosContrasenias.SelectedItems.Count == 0)
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Selecciona un evaluador", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "onCloseWindowC");
                grdEmpleadosContrasenias.Rebind();
            }
            else
            {
                foreach (GridDataItem item in grdEmpleadosContrasenias.SelectedItems)
                {
                    ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
                    E_RESULTADO vResultado = nClima.InsertarActualizarTokenEvaluadoresClima(vIdPeriodo, int.Parse(item.GetDataKeyValue("ID_EVALUADOR").ToString()), vClUsuario, vNbPrograma);
                    vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    // UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindowC");
                    if (!vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                    {
                        UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindowC");
                        return;
                    }

                }

                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, E_TIPO_RESPUESTA_DB.SUCCESSFUL, pCallBackFunction: "onCloseWindowC");
                grdEmpleadosContrasenias.Rebind();
            }
        }

        protected void grdEmpleadosContrasenias_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            CargarDatosEvaluador();
            grdEmpleadosContrasenias.DataSource = vlstEvaluavores;
        }

        protected void rgPreguntas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            rgPreguntas.DataSource = nClima.ObtenerPreguntasAbiertas(vIdPeriodo, null).ToList();

        }

        protected void btnEliminarAbierta_Click(object sender, EventArgs e)
        {
            XElement vXmlPreguntas = new XElement("PREGUNTAS");
            foreach (GridDataItem item in rgPreguntas.SelectedItems)
                vXmlPreguntas.Add(new XElement("PREGUNTA",
                                  new XAttribute("ID_PREGUNTA", item.GetDataKeyValue("ID_PREGUNTA").ToString())));


            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            E_RESULTADO vResultado = nClima.EliminarPreguntasAbiertas(vIdPeriodo, vXmlPreguntas.ToString(), vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindowPA");
        }

        //protected void btnGuardarAbierta_Click(object sender, EventArgs e)
        //{
        //    ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
        //    E_RESULTADO vResultado = nClima.InsertaCuestionarioPreguntasAbiertas(vIdPeriodo, vClUsuario, vNbPrograma);
        //    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
        //    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");

        //    if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
        //    {
        //        btnAgregarAbierta.Enabled = false;
        //        btnEliminarAbierta.Enabled = false;
        //        btnEditarAbierta.Enabled = false;
        //    }
        //}


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            FiltrosIndice();
            if (vXmlFiltrosSel != null)
            {
                ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
                E_RESULTADO vResultado = nClima.InsertaFiltroClima(vIdPeriodo, vXmlFiltrosSel, vClUsuario, vNbPrograma);
                //string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                //UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
                GuardarDatos(false);
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Aplique por lo menos un filtro para guardar la configuración.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            }            
        }

        protected void GuardarDatos(bool cerrar)
        {
            //ContextoApp.EO.MensajeCorreoEvaluador.dsMensaje = lMensaje.Content;
            ClimaLaboralNegocio nPeriodo = new ClimaLaboralNegocio();
            E_PERIODO_CLIMA vDsPeriodo = nPeriodo.ObtienePeriodosClima(pIdPerido: vIdPeriodo).Select(s => new E_PERIODO_CLIMA
            {
                ID_PERIODO = s.ID_PERIODO,
                CL_PERIODO = s.CL_PERIODO,
                NB_PERIODO = s.NB_PERIODO,
                DS_PERIODO = s.DS_PERIODO,
                FE_INICIO = s.FE_INICIO,
                CL_ESTADO_PERIODO = s.CL_ESTADO_PERIODO,
                DS_NOTAS = s.DS_NOTAS,
                ID_PERIODO_CLIMA = s.ID_PERIODO_CLIMA,
                CL_TIPO_CONFIGURACION = s.CL_TIPO_CONFIGURACION,
                CL_ORIGEN_CUESTIONARIO = s.CL_ORIGEN_CUESTIONARIO,
                ID_PERIODO_ORIGEN = s.ID_PERIODO_ORIGEN
            }
                ).FirstOrDefault();

            vDsPeriodo.DS_MENSAJE_ENVIO = lMensaje.Content;

            string vInstrucciones = reInstrucciones.Content;
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            E_RESULTADO vResultadoInstrucciones = nClima.InsertaInstruccionesCuestionario(vInstrucciones, vIdPeriodo, vClUsuario, vNbPrograma);
            string vMensajeInstrucciones = vResultadoInstrucciones.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            if (vResultadoInstrucciones.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                E_RESULTADO vResultado = nPeriodo.InsertaActualizaPeriodoClima(pPeriodo: vDsPeriodo, pCL_USUARIO: vClUsuario, pNB_PROGRAMA: vNbPrograma, pTIPO_TRANSACCION: "A");
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                if (cerrar)
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "CloseWindowConfig");
                else
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensajeInstrucciones, vResultadoInstrucciones.CL_TIPO_ERROR, pCallBackFunction: null);

            }
        }

        //protected void btnInstrucciones_Click(object sender, EventArgs e)
        //{
        //    string vInstrucciones = reInstrucciones.Content;
        //    ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
        //    E_RESULTADO vResultado = nClima.InsertaInstruccionesCuestionario(vInstrucciones, vIdPeriodo, vClUsuario, vNbPrograma);
        //    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
        //    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
        //}

        #region Clases

        [Serializable]
        public class E_DEPARTAMENTOS
        {
            public string ID_DEPARTAMENTO { get; set; }
            public string NB_DEPARTAMENTO { get; set; }
        }

        [Serializable]
        public class E_GENERO
        {
            public string CL_GENERO { get; set; }
            public string NB_GENERO { get; set; }
        }

        #endregion

        protected void grdEmpleadosSeleccionados_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdEmpleadosSeleccionados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdEmpleadosSeleccionados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdEmpleadosSeleccionados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdEmpleadosSeleccionados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdEmpleadosSeleccionados.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void grdPreguntasCuestionario_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdPreguntasCuestionario.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdPreguntasCuestionario.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdPreguntasCuestionario.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdPreguntasCuestionario.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdPreguntasCuestionario.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }

        }

        protected void rgPreguntas_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgPreguntas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgPreguntas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgPreguntas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgPreguntas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgPreguntas.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void grdEmpleadosContrasenias_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdEmpleadosContrasenias.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdEmpleadosContrasenias.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdEmpleadosContrasenias.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdEmpleadosContrasenias.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdEmpleadosContrasenias.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void btnValidez_Click(object sender, EventArgs e)
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            E_RESULTADO vResultado = nClima.ActualizaValidezCuestionario(vIdPeriodo);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
        }

        protected void btnGuardarCerrar_Click(object sender, EventArgs e)
        {
            GuardarDatos(true);
        }

    }
}