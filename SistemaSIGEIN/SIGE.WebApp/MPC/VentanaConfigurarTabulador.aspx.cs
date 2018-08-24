using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Entidades.Externas;
using SIGE.Entidades.MetodologiaCompensacion;
using SIGE.Negocio.MetodologiaCompensacion;
using SIGE.WebApp.Comunes;
using SIGE.Entidades;
using System.IO;
using System.Xml.Linq;
using SIGE.Negocio.Utilerias;
using SIGE.Negocio.Administracion;
using Newtonsoft.Json;
using SIGE.Entidades.Administracion;
using Telerik.Web.UI;


namespace SIGE.WebApp.MPC
{
    public partial class VentanaConfigurarTabulador : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdRol;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        String vTipoTransaccion = "";
        public string vClickedItemEventName = "ClickedItemEvent";

        public int vIdTabulador
        {
            get { return (int)ViewState["vsIdTabulador"]; }
            set { ViewState["vsIdTabulador"] = value; }
        }

        private List<E_VARIACION> vVariacion
        {
            get { return (List<E_VARIACION>)ViewState["vs_vVariacion"]; }
            set { ViewState["vs_vVariacion"] = value; }
        }

        private List<E_SELECCIONADOS> vSeleccionCompetencias
        {
            get { return (List<E_SELECCIONADOS>)ViewState["vs_vSeleccionCompetencias"]; }
            set { ViewState["vs_vSeleccionCompetencias"] = value; }
        }

        private List<E_SELECCIONADOS> vSeleccionEmpleados
        {
            get { return (List<E_SELECCIONADOS>)ViewState["vs_vSeleccionEmpleados"]; }
            set { ViewState["vs_vSeleccionEmpleados"] = value; }
        }

        private List<E_SELECCIONADOS> vSeleccionPuestos
        {
            get { return (List<E_SELECCIONADOS>)ViewState["vs_vSeleccionPuestos"]; }
            set { ViewState["vs_vSeleccionPuestos"] = value; }
        }

        private List<E_SELECCIONADOS> vSeleccionAreas
        {
            get { return (List<E_SELECCIONADOS>)ViewState["vs_vSeleccionAreas"]; }
            set { ViewState["vs_vSeleccionAreas"] = value; }
        }

        private List<E_CUARTILES> vCuartilesTabulador
        {
            get { return (List<E_CUARTILES>)ViewState["vsCuartilesTabulador"]; }
            set { ViewState["vsCuartilesTabulador"] = value; }
        }

        public string vClTipoPuesto
        {
            get { return (string)ViewState["vs_vClTipoPuesto"]; }
            set { ViewState["vs_vClTipoPuesto"] = value; }
        }

        #endregion

        #region Funciones

        private void SeguridadProcesos()
        {
            btnAgregarCompetencia.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.D.A");
            btnEliminarCompetencias.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.D.A");
            btnEliminarEmpleados.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.D.A");
            btnGuardarConfiguracion.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.D.A");
            btnSeleccionarAreas.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.D.A");
            btnSeleccionarCompetencia.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.D.A");
            btnSeleccionarEmpleados.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.D.A");
            btnSeleccionarFactores.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.D.A");
            btnSeleccionarPuestos.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.D.A");
            btnSelecionarCompetenciaEspecifica.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.D.A");
            btnGuardar.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.D.A");
        }

        protected void DespacharEventos(string pCatalogo, string pSeleccionados)
        {
            if (pCatalogo == "EMPLEADO")
                CargarDatosEmpleado(pSeleccionados);
            if (pCatalogo == "COMPETENCIA")
                CargarDatosCompetencia(pSeleccionados);
            if (pCatalogo == "FACTOR")
                CargarDatosFactor(pSeleccionados);
            if (pCatalogo == "PUESTO")
                CargarDatosPuesto(pSeleccionados);
            if (pCatalogo == "DEPARTAMENTO")
                CargarDatosArea(pSeleccionados);
        }

        protected void CargarDatosCompetencia(String pIdsSeleccionados)
        {
            List<int> vSeleccionados = null;
            if (pIdsSeleccionados != null & pIdsSeleccionados != "")
            {
                vSeleccionados = JsonConvert.DeserializeObject<List<int>>(pIdsSeleccionados);
            }

            E_EMPLEADOS_SELECCIONADOS vEmpleados = new E_EMPLEADOS_SELECCIONADOS();
            vEmpleados.ID_TABULADOR = vIdTabulador;

            vSeleccionCompetencias = new List<E_SELECCIONADOS>();
            foreach (int item in vSeleccionados)
            {
                vSeleccionCompetencias.Add(new E_SELECCIONADOS { ID_SELECCIONADO = item });
            }
            var vXelements = vSeleccionCompetencias.Select(x =>
                                           new XElement("COMPETENCIA",
                                           new XAttribute("ID_COMPETENCIA", x.ID_SELECCIONADO)
                                ));
            XElement SELECCIONADOS =
            new XElement("COMPETENCIAS", vXelements
                );
            vEmpleados.XML_ID_SELECCIONADOS = SELECCIONADOS.ToString();
            string vNiveles = null;
            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            E_RESULTADO vResultado = nTabulador.InsertaActualizaTabuladorFactor(pClTipoOperacion: E_TIPO_OPERACION_DB.I.ToString(), vEmpleados: vEmpleados, pNiveles: vNiveles, usuario: vClUsuario, programa: vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindowC");
        }

        protected void CargarDatosFactor(String pIdsSeleccionados)
        {
            List<int> vSeleccionados = null;
            if (pIdsSeleccionados != null & pIdsSeleccionados != "")
            {
                vSeleccionados = JsonConvert.DeserializeObject<List<int>>(pIdsSeleccionados);
            }

            E_EMPLEADOS_SELECCIONADOS vEmpleados = new E_EMPLEADOS_SELECCIONADOS();
            vEmpleados.ID_TABULADOR = vIdTabulador;

            vSeleccionCompetencias = new List<E_SELECCIONADOS>();
            foreach (int item in vSeleccionados)
            {
                vSeleccionCompetencias.Add(new E_SELECCIONADOS { ID_SELECCIONADO = item });
            }
            var vXelements = vSeleccionCompetencias.Select(x =>
                                           new XElement("FACTOR",
                                           new XAttribute("ID_FACTOR_EVALUACION", x.ID_SELECCIONADO)
                                ));
            XElement SELECCIONADOS =
            new XElement("FACTORES", vXelements
                );
            vEmpleados.XML_ID_SELECCIONADOS = SELECCIONADOS.ToString();
            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            E_RESULTADO vResultado = nTabulador.InsertarFactorTabulador(pClTipoOperacion: E_TIPO_OPERACION_DB.I.ToString(), vEmpleados: vEmpleados, usuario: vClUsuario, programa: vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindowC");
        }

        protected void CargarDatosEmpleado(string pIdsSeleccionados)
        {
            List<int> vSeleccionados = null;
            if (pIdsSeleccionados != null & pIdsSeleccionados != "")
            {
                vSeleccionados = JsonConvert.DeserializeObject<List<int>>(pIdsSeleccionados);
            }
            vSeleccionEmpleados = new List<E_SELECCIONADOS>();
            foreach (int item in vSeleccionados)
            {
                vSeleccionEmpleados.Add(new E_SELECCIONADOS { ID_SELECCIONADO = item });
            }
            var vXelements = vSeleccionEmpleados.Select(x =>
                                           new XElement("EMPLEADO",
                                           new XAttribute("ID_EMPLEADO", x.ID_SELECCIONADO)
                                ));
            XElement SELECCIONADOS =
            new XElement("EMPLEADOS", vXelements
                );

            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            E_RESULTADO vResultado = nTabulador.InsertaActualizaTabuladorEmpleado(pClTipoOperacion: E_TIPO_OPERACION_DB.I.ToString(), ID_TABULADOR: vIdTabulador, XML_SELECCIONADOS: SELECCIONADOS.ToString(), usuario: vClUsuario, programa: vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "onCloseWindowE");

        }

        protected void CargarDatosPuesto(string pIdsSeleccionados)
        {
            List<int> vSeleccionados = null;
            if (pIdsSeleccionados != null & pIdsSeleccionados != "")
            {
                vSeleccionados = JsonConvert.DeserializeObject<List<int>>(pIdsSeleccionados);
            }
            vSeleccionPuestos = new List<E_SELECCIONADOS>();
            foreach (int item in vSeleccionados)
            {
                vSeleccionPuestos.Add(new E_SELECCIONADOS { ID_SELECCIONADO = item });
            }
            var vXelements = vSeleccionPuestos.Select(x =>
                                           new XElement("PUESTO",
                                           new XAttribute("ID_PUESTO", x.ID_SELECCIONADO)
                                ));
            XElement SELECCIONADOS =
            new XElement("PUESTOS", vXelements
                );

            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            E_RESULTADO vResultado = nTabulador.InsertaActualizaTabuladorEmpleado(pClTipoOperacion: E_TIPO_OPERACION_DB.I.ToString(), ID_TABULADOR: vIdTabulador, XML_SELECCIONADOS: SELECCIONADOS.ToString(), usuario: vClUsuario, programa: vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "onCloseWindowE");
        }

        protected void CargarDatosArea(string pIdsSeleccionados)
        {
            List<int> vSeleccionados = null;
            if (pIdsSeleccionados != null & pIdsSeleccionados != "")
            {
                vSeleccionados = JsonConvert.DeserializeObject<List<int>>(pIdsSeleccionados);
            }
            vSeleccionAreas = new List<E_SELECCIONADOS>();
            foreach (int item in vSeleccionados)
            {
                vSeleccionAreas.Add(new E_SELECCIONADOS { ID_SELECCIONADO = item });
            }
            var vXelements = vSeleccionAreas.Select(x =>
                                           new XElement("AREA",
                                           new XAttribute("CL_TIPO_PUESTO", vClTipoPuesto),
                                           new XAttribute("ID_AREA", x.ID_SELECCIONADO)
                                ));
            XElement SELECCIONADOS =
            new XElement("AREAS", vXelements
                );

            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            E_RESULTADO vResultado = nTabulador.InsertaActualizaTabuladorEmpleado(pClTipoOperacion: E_TIPO_OPERACION_DB.I.ToString(), ID_TABULADOR: vIdTabulador, XML_SELECCIONADOS: SELECCIONADOS.ToString(), usuario: vClUsuario, programa: vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "onCloseWindowE");
        }

        public int vNivelesTabulador
        {
            get { return (int)ViewState["vs_vNivelesTabulador"]; }
            set { ViewState["vs_vNivelesTabulador"] = value; }
        }

        private List<E_NIVELES> vTabuladorNivel
        {
            get { return (List<E_NIVELES>)ViewState["vs_vTabuladorNivel"]; }
            set { ViewState["vs_vTabuladorNivel"] = value; }
        }

        public string vCL_GUARDAR
        {
            get { return (string)ViewState["vs_vCL_GUARDAR"]; }
            set { ViewState["vs_vCL_GUARDAR"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;

            if (!IsPostBack)
            {
                TabuladoresNegocio nTabuladores = new TabuladoresNegocio();
                SPE_OBTIENE_TABULADORES_Result vCuartiles = nTabuladores.ObtenerTabuladores().FirstOrDefault();
                XElement vXlmCuartiles = XElement.Parse(vCuartiles.XML_VW_CUARTILES);
                vCuartilesTabulador = vXlmCuartiles.Elements("ITEM").Select(x => new E_CUARTILES
                {
                    ID_CUARTIL = UtilXML.ValorAtributo<int>(x.Attribute("NB_VALOR")),
                    NB_CUARTIL = UtilXML.ValorAtributo<string>(x.Attribute("NB_TEXTO")),
                }).ToList();

                cmbCuartilInflacional.DataSource = vCuartilesTabulador;
                cmbCuartilInflacional.DataTextField = "NB_CUARTIL";
                cmbCuartilInflacional.DataValueField = "ID_CUARTIL".ToString();
                cmbCuartilInflacional.DataBind();

                cmbCuartilIncremento.DataSource = vCuartilesTabulador;
                cmbCuartilIncremento.DataTextField = "NB_CUARTIL";
                cmbCuartilIncremento.DataValueField = "ID_CUARTIL".ToString();
                cmbCuartilIncremento.DataBind();
                cmbNivelMercado.DataSource = vCuartilesTabulador;
                cmbNivelMercado.DataTextField = "NB_CUARTIL";
                cmbNivelMercado.DataValueField = "ID_CUARTIL".ToString();
                cmbNivelMercado.DataBind();

                if (Request.QueryString["ID"] != null)
                {
                    vIdTabulador = int.Parse((Request.QueryString["ID"]));
                    TabuladoresNegocio nTabulador = new TabuladoresNegocio();
                    var vTabulador = nTabulador.ObtenerTabuladores(ID_TABULADOR: vIdTabulador).FirstOrDefault();
                    XElement vXlmCuartil = XElement.Parse(vTabulador.XML_CUARTILES);
                    vClTipoPuesto = vTabulador.CL_TIPO_PUESTO;
                    SeguridadProcesos();

                    int TabNivel = 0;
                    int vCount = nTabulador.ObtieneTabuladorNivel(ID_TABULADOR: vIdTabulador).Count;
                    if (vCount > 0)
                    {
                        TabNivel = nTabulador.ObtieneTabuladorNivel(ID_TABULADOR: vIdTabulador).OrderByDescending(w => w.NO_NIVEL).FirstOrDefault().NO_NIVEL;
                    }
                    if (vTabulador.FG_RECALCULAR_NIVELES == true && TabNivel > 0 && TabNivel != vTabulador.NO_NIVELES)
                    {
                        lblAdvertencia.Visible = true;
                        lblAdvertencia.InnerText = "*No fue posible generar el número de niveles solicitados, el número real es " + TabNivel.ToString();
                    }
                    txtClTabulador.InnerText = vTabulador.CL_TABULADOR;
                    txtDescripción.InnerText = vTabulador.DS_TABULADOR;
                    txtNbTabulador.InnerText = vTabulador.NB_TABULADOR;
                    txtVigencia.InnerText = vTabulador.FE_VIGENCIA.ToString("dd/MM/yyyy");
                    txtFecha.InnerText = vTabulador.FE_CREACION.ToString("dd/MM/yyyy");
                    txtPuestos.InnerText = vTabulador.CL_TIPO_PUESTO;
                    vNivelesTabulador = vTabulador.NO_NIVELES;
                    if (vTabulador.CL_ESTADO == "CERRADO")
                    {
                        btnAgregarCompetencia.Enabled = false;
                        btnEliminarCompetencias.Enabled = false;
                        btnEliminarEmpleados.Enabled = false;
                        btnGuardarConfiguracion.Enabled = false;
                        btnSeleccionarAreas.Enabled = false;
                        btnSeleccionarCompetencia.Enabled = false;
                        btnSeleccionarEmpleados.Enabled = false;
                        btnSeleccionarFactores.Enabled = false;
                        btnSeleccionarPuestos.Enabled = false;
                        btnSelecionarCompetenciaEspecifica.Enabled = false;

                    }
                    //txtClTabulador.Text = vTabulador.CL_TABULADOR;
                    //txtNbTabulador.Text = vTabulador.DS_TABULADOR;
                    //rdpCreacion.SelectedDate = vTabulador.FE_CREACION;
                    //rdpVigencia.SelectedDate = vTabulador.FE_VIGENCIA;
                    rntNivelesACrear.Text = vTabulador.NO_NIVELES.ToString();
                    rntNumeroCategorias.Text = vTabulador.NO_CATEGORIAS.ToString();
                    rntProgresion.Text = vTabulador.PR_PROGRESION.ToString();
                    rntPorcentaje.Text = vTabulador.PR_INFLACION.ToString();


                    foreach (XElement vXmlCuartilSeleccionado in vXlmCuartil.Elements("ITEM"))
                        if ((UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("FG_SELECCIONADO_INFLACIONAL")).Equals(1)))
                        {
                            cmbCuartilInflacional.Text = UtilXML.ValorAtributo<string>(vXmlCuartilSeleccionado.Attribute("NB_CUARTIL"));
                            cmbCuartilInflacional.SelectedValue = UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("ID_CUERTIL")).ToString();
                        }
                    foreach (XElement vXmlCuartilSeleccionado in vXlmCuartil.Elements("ITEM"))
                        if ((UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("FG_SELECCIONADO_INCREMENTO")).Equals(1)))
                        {
                            cmbCuartilIncremento.Text = UtilXML.ValorAtributo<string>(vXmlCuartilSeleccionado.Attribute("NB_CUARTIL"));
                            cmbCuartilIncremento.SelectedValue = UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("ID_CUERTIL")).ToString();

                        }
                    foreach (XElement vXmlCuartilSeleccionado in vXlmCuartil.Elements("ITEM"))
                        if ((UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("FG_SELECCIONADO_MERCADO")).Equals(1)))
                        {
                            cmbNivelMercado.Text = UtilXML.ValorAtributo<string>(vXmlCuartilSeleccionado.Attribute("NB_CUARTIL"));
                            cmbNivelMercado.SelectedValue = UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("ID_CUERTIL")).ToString();
                        }
                    //cmbSueldo.Text = vTabulador.CL_SUELDO_COMPARACION;

                    if (vTabulador.XML_VARIACION != null)
                    {
                        XElement vXlmVariacion = XElement.Parse(vTabulador.XML_VARIACION);

                        foreach (XElement vXmlVaria in vXlmVariacion.Elements("Rango"))
                            if ((UtilXML.ValorAtributo<string>(vXmlVaria.Attribute("COLOR")).Equals("green")))
                            {
                                rntVariacionVerde.Text = UtilXML.ValorAtributo<string>(vXmlVaria.Attribute("RANGO_SUPERIOR"));
                            }

                        foreach (XElement vXmlVaria in vXlmVariacion.Elements("Rango"))
                            if ((UtilXML.ValorAtributo<string>(vXmlVaria.Attribute("COLOR")).Equals("yellow")))
                            {
                                rntVariacionAmarillo.Text = UtilXML.ValorAtributo<string>(vXmlVaria.Attribute("RANGO_SUPERIOR"));
                            }
                    }
                }

                if (Request.QueryString["CL_CONFIGURAR"] != null)
                {
                    rtsConfiguracion.Tabs[3].Enabled = true;
                    rtsConfiguracion.Tabs[3].Selected = true;
                    rtsConfiguracion.Tabs[0].Enabled = false;
                    rtsConfiguracion.Tabs[1].Enabled = false;
                    rtsConfiguracion.Tabs[2].Enabled = false;
                    rmpConfiguracion.SelectedIndex = 3;
                }
                else
                {
                    rtsConfiguracion.SelectedIndex = 0;
                }
            }
            DespacharEventos(Request.Params.Get("__EVENTTARGET"), Request.Params.Get("__EVENTARGUMENT"));
        }

        protected void btnGuardarConfiguracion_Click(object sender, EventArgs e)
        {
            E_TABULADOR vTabulador = new E_TABULADOR();
            vTabulador.ID_TABULADOR = vIdTabulador;
            vTabulador.CL_TABULADOR = txtClTabulador.InnerText;
            vTabulador.DS_TABULADOR = txtDescripción.InnerText;
            //vTabulador.FE_VIGENCIA = txtVigencia.InnerText;
            vTabulador.NO_NIVELES = byte.Parse(rntNivelesACrear.Text);
            vTabulador.NO_CATEGORIAS = byte.Parse(rntNumeroCategorias.Text);
            vTabulador.PR_PROGRESION = decimal.Parse(rntProgresion.Text);
            vTabulador.PR_INFLACION = decimal.Parse(rntPorcentaje.Text);
            vTabulador.ID_CUARTIL_INFLACIONAL = int.Parse(cmbCuartilInflacional.SelectedValue);
            vTabulador.ID_CUARTIL_INCREMENTO = int.Parse(cmbCuartilIncremento.SelectedValue);
            vTabulador.ID_CUARTIL_MERCADO = int.Parse(cmbNivelMercado.SelectedValue ?? "0");

            //vTabulador.CL_SUELDO_COMPARACION = cmbSueldo.Text;

            //vTabulador.FE_CREACION = txtFecha.SelectedDate ?? DateTime.Now;

            if (rntVariacionAmarillo.Text != "" & rntVariacionVerde.Text != "")
            {
                vVariacion = new List<E_VARIACION>();
                vVariacion.Add(new E_VARIACION { RANGO_INFERIOR = 0, RANGO_SUPERIOR = int.Parse(rntVariacionVerde.Text), COLOR = "green" });
                vVariacion.Add(new E_VARIACION { RANGO_INFERIOR = int.Parse(rntVariacionVerde.Text), RANGO_SUPERIOR = int.Parse(rntVariacionAmarillo.Text), COLOR = "yellow" });


                var vXelements = vVariacion.Select(x =>
                                                new XElement("Rango",
                                                       new XAttribute("RANGO_INFERIOR", x.RANGO_INFERIOR),
                                                       new XAttribute("RANGO_SUPERIOR", x.RANGO_SUPERIOR),
                                                       new XAttribute("COLOR", x.COLOR))
                                   );
                XElement vVariacionSemaforo =
                new XElement("Variacion", vXelements
                );

                vTabulador.XML_VARIACION = vVariacionSemaforo.ToString();
            }
            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            E_RESULTADO vResultado = nTabulador.InsertaActualizaTabulador(usuario: vClUsuario, programa: vNbPrograma, pClTipoOperacion: vTipoTransaccion.ToString(), vTabulador: vTabulador);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                rtsConfiguracion.Tabs[4].Enabled = true;
                rpvNiveles.Selected = true;
                rtsConfiguracion.Tabs[4].Selected = true;
                vNivelesTabulador = int.Parse(rntNivelesACrear.Text);
                grdNiveles.Rebind();
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction:"");
            }
            else
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
        }

        protected void grdEmpleadosSeleccionados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            XElement vXmlIdTabulador = new XElement("SELECCION",
                                                    new XElement("FILTRO",
                                                    new XAttribute("CL_TIPO", "TABULADOR"),
                                                    new XElement("TIPO",
                                                    new XAttribute("ID_TABULADOR", vIdTabulador))));
            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            grdEmpleadosSeleccionados.DataSource = nTabulador.ObtenieneEmpleadosTabulador(XML_SELECCIONADOS: vXmlIdTabulador, pIdRol: vIdRol);
        }

        protected void grdCompetenciasSeleccionadas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            grdCompetenciasSeleccionadas.DataSource = nTabulador.ObtenieneCompetenciasTabulador(ID_TABULADOR: vIdTabulador);
        }

        protected void btnEliminarEmpleados_Click(object sender, EventArgs e)
        {
            XElement vXmlEmpleados = new XElement("EMPLEADOS");
            foreach (GridDataItem item in grdEmpleadosSeleccionados.SelectedItems)
                vXmlEmpleados.Add(new XElement("EMPLEADO",
                                  new XAttribute("ID_EMPLEADO", item.GetDataKeyValue("ID_TABULADOR_EMPLEADO").ToString()),
                                  new XAttribute("ID_TABULADOR", vIdTabulador)));



            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            E_RESULTADO vResultado = nTabulador.EliminaTabuladorEmpleado(vClUsuario, vNbPrograma, vXmlEmpleados.ToString());
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindowE");
        }

        protected void btnEliminarCompetencias_Click(object sender, EventArgs e)
        {
            XElement vXmlCompetencias = new XElement("COMPETENCIAS");
            foreach (GridDataItem item in grdCompetenciasSeleccionadas.SelectedItems)
                vXmlCompetencias.Add(new XElement("COMPETENCIA",
                                  new XAttribute("ID_TABULADOR_FACTOR", item.GetDataKeyValue("ID_TABULADOR_FACTOR").ToString())));


            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            E_RESULTADO vResultado = nTabulador.EliminaTabuladorFactor(vClUsuario, vNbPrograma, vXmlCompetencias.ToString());
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindowC");

        }

        protected void grdCompetenciasSeleccionadas_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridDataItem item = (GridDataItem)grdCompetenciasSeleccionadas.SelectedItems[0];
            int cont = grdCompetenciasSeleccionadas.SelectedItems.Count;
            if (cont > 0)
            {
                string dataKey = item.GetDataKeyValue("CL_TIPO_COMPETENCIA").ToString();
                if (dataKey != "FACTOR")
                {
                    btnEditarCompetencias.Visible = false;
                }
                else
                {
                    btnEditarCompetencias.Visible = true;
                }
            }

        }

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

        protected void grdCompetenciasSeleccionadas_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdCompetenciasSeleccionadas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdCompetenciasSeleccionadas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdCompetenciasSeleccionadas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdCompetenciasSeleccionadas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdCompetenciasSeleccionadas.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
        
        protected void grdNiveles_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            var vTabuladorNivel = nTabulador.ObtieneTabuladorNivel(ID_TABULADOR: vIdTabulador);
            grdNiveles.DataSource = vTabuladorNivel;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int vIdTabuladorNivel = 0;
            vTabuladorNivel = new List<E_NIVELES>();
            foreach (GridDataItem item in grdNiveles.MasterTableView.Items)
            { 
                vIdTabuladorNivel = (int.Parse(item.GetDataKeyValue("ID_TABULADOR_NIVEL").ToString()));
                RadTextBox vClNivel = (RadTextBox)item.FindControl("txtClNivel");
                RadTextBox vNbNivel = (RadTextBox)item.FindControl("txtNbNivel");
                RadNumericTextBox vOrden = (RadNumericTextBox)item.FindControl("txnOrden");
                RadNumericTextBox vProgresion = (RadNumericTextBox)item.FindControl("txnProgresion");

                vTabuladorNivel.Add(new E_NIVELES { ID_TABULADOR_NIVEL = vIdTabuladorNivel, CL_TABULADOR_NIVEL = vClNivel.Text, NB_TABULADOR_NIVEL = vNbNivel.Text, NO_ORDEN = int.Parse(vOrden.Text), PR_PROGRESION = decimal.Parse(vProgresion.Text)});
            }

            var dups = vTabuladorNivel.GroupBy(x => x.NO_ORDEN).Where(x => x.Count() > 1).Select(x => x.Key).ToList();

            if (dups.Count == 0)
            {

                TabuladoresNegocio nTabulador = new TabuladoresNegocio();
                var vTabuladorProgresion = nTabulador.ObtieneTabuladorNivel(ID_TABULADOR: vIdTabulador).ToList();

                var vPrTavulador = vTabuladorNivel.Select(s => s.PR_PROGRESION).ToList();
                var vPrProgresion = vTabuladorProgresion.Select(s => s.PR_PROGRESION).ToList();

                if (vPrTavulador.SequenceEqual(vPrProgresion))
                    vCL_GUARDAR = "GUARDAR";
                else vCL_GUARDAR = "PR_GUARDAR";

                var vXelements = vTabuladorNivel.Select(x =>
                                               new XElement("NIVEL",
                                               new XAttribute("ID_TABULADOR_NIVEL", x.ID_TABULADOR_NIVEL),
                                               new XAttribute("CL_TABULADOR_NIVEL", x.CL_TABULADOR_NIVEL),
                                               new XAttribute("NB_TABULADOR_NIVEL", x.NB_TABULADOR_NIVEL),
                                               new XAttribute("NO_ORDEN", x.NO_ORDEN),
                                               new XAttribute("PR_PROGRESION", x.PR_PROGRESION)
                                    ));
                XElement TABULADORNIVEL =
                new XElement("NIVELES", vXelements
                );

                E_RESULTADO vResultado = nTabulador.ActualizaTabuladorNivel(vIdTabulador, TABULADORNIVEL.ToString(), vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "closeWindow");
            }

            else {

                UtilMensajes.MensajeResultadoDB(rwmMensaje, "No se pueden repetir valores en el número de orden.", E_TIPO_RESPUESTA_DB.ERROR);
            }
        }

        protected void grdNiveles_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                RadNumericTextBox txnOrden = (RadNumericTextBox)item.FindControl("txnOrden");
                txnOrden.MaxValue = vNivelesTabulador;
            } 
        }

    }


}

