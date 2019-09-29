using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.AdministracionSitio;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;
using WebApp.Comunes;

namespace SIGE.WebApp.Administracion
{
    public partial class Empleado : System.Web.UI.Page
    {

        #region Variables
        private List<E_RAZON_SOCIAL> listRazonSocial = new List<E_RAZON_SOCIAL>();
        private List<E_REGISTRO_PATRONAL> listRegistrosPatronales = new List<E_REGISTRO_PATRONAL>();
        private List<E_TIPO_TRABAJO_SUA> listTipoTrabajoSUA = new List<E_TIPO_TRABAJO_SUA>();
        private List<E_TIPO_JORNADA_SUA> listTipoJornadaSUA = new List<E_TIPO_JORNADA_SUA>();
        private List<E_TIPO_CONTRATO_SAT> listTipoContratoSAT = new List<E_TIPO_CONTRATO_SAT>();
        private List<E_TIPO_JORNADA_SAT> listTipoJornadaSAT = new List<E_TIPO_JORNADA_SAT>();
        private List<E_REGIMEN_SAT> listRegimenSAT = new List<E_REGIMEN_SAT>();
        private List<E_TIPO_SALARIO_SUA> listTipoSalarioSUA = new List<E_TIPO_SALARIO_SUA>();
        private List<E_RIESGO_PUESTO> listRiesgoPuesto = new List<E_RIESGO_PUESTO>();
        private List<E_HORARIO_SEMANA> listHorarioSemana = new List<E_HORARIO_SEMANA>();
        private List<E_PAQUETE_PRESTACIONES> listPaquetePrestaciones = new List<E_PAQUETE_PRESTACIONES>();
        private List<E_FORMATO_DISPERSION> listFormatoDispersion = new List<E_FORMATO_DISPERSION>();
        private List<E_TIPO_NOMINA> listTipoNomina = new List<E_TIPO_NOMINA>();
        private List<E_FORMA_PAGO> listFormaPago = new List<E_FORMA_PAGO>();
        private List<E_BANCO> listBancoNomina = new List<E_BANCO>();

        Plantilla vPlantilla;
        XElement vPlantillaNomina = new XElement("NOMINA");
        string vXmlPlantilla;
        int? vIdEmpleado;
        int? vIdEmpleadoNominaDo;
        string vClCliente;
        string vClUsuario;
        string vNbPrograma;
        string vXmlDocumentos;
        Guid? vIdItemFoto;
        string vClRutaArchivosTemporales;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private bool vGuardar;
        private int? vIdEmpresa;
        public string xmlPlantillaNO { get; set; }

        public int? vIdSolicitud
        {
            get { return (int?)ViewState["vs_vIdSolicitud"]; }
            set { ViewState["vs_vIdSolicitud"] = value; }
        }

        public string vXmlEmpleadoPlantilla
        {
            get { return (string)ViewState["vs_vXmlEmpleadoPlantilla"]; }
            set { ViewState["vs_vXmlEmpleadoPlantilla"] = value; }
        }

        public string vTipoTransaccion
        {
            get { return (string)ViewState["vs_vTipoTransaccion"]; }
            set { ViewState["vs_vTipoTransaccion"] = value; }
        }

        int? vIdEmpleadoVS
        {
            get { return (int?)ViewState["vs_vIdEmpleadoVS"]; }
            set { ViewState["vs_vIdEmpleadoVS"] = value; }
        }

        List<E_DOCUMENTO> vLstDocumentos
        {
            get { return (List<E_DOCUMENTO>)ViewState["vs_LstDocumentos"]; }
            set { ViewState["vs_LstDocumentos"] = value; }
        }

        string vFiLogotipo
        {
            get { return (string)ViewState["vs_vFiLogotipo"]; }
            set { ViewState["vs_vFiLogotipo"] = value; }
        }

        string vNbLogotipo
        {
            get { return (string)ViewState["vs_vNbLogotipo"]; }
            set { ViewState["vs_vNbLogotipo"] = value; }
        }

        Guid? vIdItemFotografia
        {
            get { return (Guid?)ViewState["vs_vIdItemFotografia"]; }
            set { ViewState["vs_vIdItemFotografia"] = value; }
        }



        public int? vIdCandidato
        {
            get { return (int?)ViewState["vs_vIdCandidato"]; }
            set { ViewState["vs_vIdCandidato"] = value; }
        }

        public string vUrlNomina
        {
            get { return (string)ViewState["vs_vUrlNomina"]; }
            set { ViewState["vs_vUrlNomina"] = value; }
        }

        public List<KeyValuePair<string, string>> vDatosModificar
        {
            get { return (List<KeyValuePair<string, string>>)ViewState["vs_vDatosModificar"]; }
            set { ViewState["vs_vDatosModificar"] = value; }
        }

        public List<E_GENERICA> vControlDependienteSeleccion
        {
            get { return (List<E_GENERICA>)ViewState["vs_emp_control_dependiente_seleccion"]; }
            set { ViewState["vs_emp_control_dependiente_seleccion"] = value; }
        }

        public E_REPORTE_EMPLEADO vDatosReporteModular
        {
            get { return (E_REPORTE_EMPLEADO)ViewState["vs_reporte_modular_empleado"]; }
            set { ViewState["vs_reporte_modular_empleado"] = value; }
        }

        public string vClEstadoEmpleado
        {
            get { return (string)ViewState["vs_vClEstadoEmpleado"]; }
            set { ViewState["vs_vClEstadoEmpleado"] = value; }
        }

        #endregion

        #region Funciones

        public string ObtenerDatosDeReportes(string CL_COLUMNA)
        {
            string vDato = "";

            if (vDatosReporteModular != null)
            {
                switch (CL_COLUMNA)
                {
                    case "ID_BATERIA":
                        vDato = vDatosReporteModular.DatosIdp.ID_BATERIA.ToString();
                        break;

                    case "CL_TOKEN":
                        vDato = vDatosReporteModular.DatosIdp.CL_TOKEN.ToString();
                        break;

                    case "ID_EVALUADO_CLIMA":
                        vDato = vDatosReporteModular.DatosEo.ID_EVALUADO_CLIMA.ToString();
                        break;

                    case "ID_PERIODO_CLIMA":
                        vDato = vDatosReporteModular.DatosEo.ID_PERIODO_CLIMA.ToString();
                        break;
                }
            }

            return vDato;

        }

        protected void CargarDocumentos()
        {
            XElement x = XElement.Parse(vXmlDocumentos).Elements("VALOR").FirstOrDefault(f => UtilXML.ValorAtributo<string>(f.Attribute("ID_CAMPO")) == "LS_DOCUMENTOS");

            if (vLstDocumentos == null)
                vLstDocumentos = new List<E_DOCUMENTO>();

            foreach (XElement item in (x.Element("ITEMS") ?? new XElement("ITEMS")).Elements("ITEM"))
                vLstDocumentos.Add(new E_DOCUMENTO()
                {
                    ID_ITEM = new Guid(UtilXML.ValorAtributo<string>(item.Attribute("ID_ITEM"))),
                    NB_DOCUMENTO = UtilXML.ValorAtributo<string>(item.Attribute("NB_DOCUMENTO")),
                    ID_DOCUMENTO = UtilXML.ValorAtributo<int>(item.Attribute("ID_DOCUMENTO")),
                    ID_ARCHIVO = UtilXML.ValorAtributo<int>(item.Attribute("ID_ARCHIVO")),
                    CL_TIPO_DOCUMENTO = UtilXML.ValorAtributo<string>(item.Attribute("CL_TIPO_DOCUMENTO"))
                });
        }

        protected List<Contenedor> ObtenerContenedores()
        {
            List<Contenedor> vLstContenedores = new List<Contenedor>();

            vLstContenedores.Add(new Contenedor() { NbContenedor = "PERSONAL", CtrlContenedor = pvwPersonal });
            vLstContenedores.Add(new Contenedor() { NbContenedor = "FAMILIAR", CtrlContenedor = pvwFamiliar });
            vLstContenedores.Add(new Contenedor() { NbContenedor = "ACADEMICA", CtrlContenedor = pvwAcademica });
            vLstContenedores.Add(new Contenedor() { NbContenedor = "LABORAL", CtrlContenedor = pvwLaboral });
            vLstContenedores.Add(new Contenedor() { NbContenedor = "COMPETENCIAS", CtrlContenedor = pvwCompetencias });
            vLstContenedores.Add(new Contenedor() { NbContenedor = "ADICIONAL", CtrlContenedor = pvwAdicional });

            return vLstContenedores;
        }

        protected void AddDocumento(string pClTipoDocumento, RadAsyncUpload pFiDocumentos)
        {
            foreach (UploadedFile f in pFiDocumentos.UploadedFiles)
            {
                E_DOCUMENTO vDocumento = new E_DOCUMENTO()
                {
                    ID_ITEM = Guid.NewGuid(),
                    CL_TIPO_DOCUMENTO = pClTipoDocumento,
                    NB_DOCUMENTO = f.FileName,
                    FE_CREATED_DATE = DateTime.Now
                };

                vLstDocumentos.Add(vDocumento);

                vIdItemFotografia = vDocumento.ID_ITEM;
                f.InputStream.Close();
                f.SaveAs(String.Format(@"{0}\{1}", vClRutaArchivosTemporales, vDocumento.GetDocumentFileName()), true);
            }

            if (vLstDocumentos == null)
                vLstDocumentos = new List<E_DOCUMENTO>();
        }

        protected void EliminarDocumento(string pIdItemDocumento)
        {
            E_DOCUMENTO d = vLstDocumentos.FirstOrDefault(f => f.ID_ITEM.ToString().Equals(pIdItemDocumento));

            if (d != null)
            {
                string vClRutaArchivo = Path.Combine(vClRutaArchivosTemporales, d.GetDocumentFileName());
                if (File.Exists(vClRutaArchivo))
                    File.Delete(vClRutaArchivo);
            }

            vLstDocumentos.Remove(d);
            grdDocumentos.Rebind();
        }

        public string ObtenerClientId(Control pCtrlContenedor, string pNbControl)
        {
            string vIdClientControl = String.Empty;
            Control vControl = pCtrlContenedor.FindControl(pNbControl);
            if (vControl != null)
                vIdClientControl = vControl.ClientID;
            return vIdClientControl;
        }

        public Control ObtenerControl(Control pCtrlContenedor, string pNbControl)
        {
            string vIdClientControl = String.Empty;
            Control vControl = pCtrlContenedor.FindControl(pNbControl);
            return vControl;
        }

        private void SeguridadProcesos()
        {
            btnGuardar.Enabled = vGuardar = ContextoUsuario.oUsuario.TienePermiso("B.F");
            btnGuardarSalir.Enabled = vGuardar = ContextoUsuario.oUsuario.TienePermiso("B.F");
        }

        private void CargarReporteModular()
        {
            EmpleadoNegocio nEmpleado = new EmpleadoNegocio();

            vDatosReporteModular = nEmpleado.ReporteEmpleadoPorModulo(vIdEmpleado.Value);

            if (vDatosReporteModular != null)
            {
                if (vDatosReporteModular.DatosIdp != null)
                {
                    txtFolioSolicitud.Text = vDatosReporteModular.DatosIdp.CL_SOLICITUD;
                    txtBateriaPruebas.Text = vDatosReporteModular.DatosIdp.FL_BATERIA;
                    vIdSolicitud = vDatosReporteModular.DatosIdp.ID_SOLICITUD;
                    vIdCandidato = vDatosReporteModular.DatosIdp.ID_CANDIDATO;
                    if (vIdSolicitud != 0)
                    {
                        btnSolicitud.Enabled = true;
                        btnProceso.Enabled = true;
                    }
                }
            }

        }

        private bool ValidarInformacion()
        {
            int vIdEmpleadoQS = -1;
            int vIdEmpleadoID = -1;

            if (int.TryParse(Request.QueryString["EmpleadoNoDoID"], out vIdEmpleadoQS))
                vIdEmpleado = vIdEmpleadoQS;

            if (int.TryParse(Request.QueryString["EmpleadoId"], out vIdEmpleadoID))
                vIdEmpleado = vIdEmpleadoID;

            CamposNominaNegocio oNegocio = new CamposNominaNegocio();
            E_EMPLEADO_NOMINA_DO vEmpleado = oNegocio.ObtienePersonalNominaDo(pID_EMPLEADO: vIdEmpleado).FirstOrDefault();

            if(vEmpleado.FG_NOMINA == true)
            {
                if (cmbRazonSocial.Text == string.Empty) { PrintMensaje("Razon social"); return false; }
                if (cmbRegistroPatronal.Text == string.Empty) { PrintMensaje("Registro patronal"); return false; }
                if (cmbPaquetePrestacionesNO.Text == string.Empty) { PrintMensaje("Paquete de prestaciones"); return false; }
                if (txtFeReingreso.SelectedDate == null) { PrintMensaje("Fecha de ingreso"); return false; }
                if (txtFeAntiguedad.SelectedDate == null) { PrintMensaje("Fecha de antigüedad"); return false; }
                if (txtSueldoDiario.Value.ToString() == string.Empty) { PrintMensaje("Sueldo diario"); return false; }
                if (txtSueldoMensual.Value.ToString() == string.Empty) { PrintMensaje("Sueldo mensual"); return false; }

                vPlantillaNomina =  GuardarFormularioNomina();
            }
            
            return true;
        }

        private void PrintMensaje(string vMensaje)
        {
            UtilMensajes.MensajeResultadoDB(rwmAlertas, "El campo " + vMensaje + " es requerido.", E_TIPO_RESPUESTA_DB.WARNING);
            //UtilMensajes.MensajeDB(rwmAlertas, , E_TIPO_RESPUESTA_DB.ERROR);
        }

        private void Guardar(bool pFgCerrarVentana)
        {
            XElement vXmlRespuesta = vPlantilla.GuardarFormulario();
            if (UtilXML.ValorAtributo<bool>(vXmlRespuesta.Attribute("FG_VALIDO")))
            {
                List<UDTT_ARCHIVO> vLstArchivos = new List<UDTT_ARCHIVO>();
                foreach (E_DOCUMENTO d in vLstDocumentos)
                {
                    string vFilePath = Server.MapPath(Path.Combine(ContextoApp.ClRutaArchivosTemporales, d.GetDocumentFileName()));
                    if (File.Exists(vFilePath))
                    {
                        vLstArchivos.Add(new UDTT_ARCHIVO()
                        {
                            ID_ITEM = d.ID_ITEM,
                            ID_ARCHIVO = d.ID_ARCHIVO,
                            NB_ARCHIVO = d.NB_DOCUMENTO,
                            FI_ARCHIVO = File.ReadAllBytes(vFilePath)
                        });
                    }
                }
                if (vIdEmpleadoVS != null && vIdEmpleadoVS != 0)
                {
                    vTipoTransaccion = "A";
                }
                else
                {
                    vTipoTransaccion = "I";
                }


                if (vTipoTransaccion == "I")
                {
                    LicenciaNegocio oNegocio = new LicenciaNegocio();
                    var vEmpleados = oNegocio.ObtenerLicenciaVolumen(pFG_ACTIVO: true).FirstOrDefault();
                    if (vEmpleados != null)
                    {
                        if (vEmpleados.NO_TOTAL_ALTA >= ContextoApp.InfoEmpresa.Volumen)
                        {
                            UtilMensajes.MensajeResultadoDB(rwmAlertas, "Se ha alcanzado el máximo número de empleados para la licencia y no es posible agregar más.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                            return;
                        }
                    }
                }

                vXmlEmpleadoPlantilla = vXmlRespuesta.Element("PLANTILLA").ToString();
                EmpleadoNegocio nEmpleado = new EmpleadoNegocio();
                E_RESULTADO vResultado = nEmpleado.InsertaActualizaEmpleado(vXmlRespuesta.Element("PLANTILLA"), vPlantillaNomina, vIdEmpleadoVS,  vLstArchivos, vLstDocumentos, vClUsuario, vNbPrograma, vTipoTransaccion);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                //resultado obtener el idEmpleado
                if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                {
                    if (pFgCerrarVentana)
                    {
                        UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "OnCloseUpdate");
                    }
                    else
                    {
                        UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
                        grdCompensacion.Rebind();
                        //Response.Redirect(Request.RawUrl);
                    }

                }
                else
                {
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
                }
            }
            else
            {
                string vMensajes = String.Empty;
                int vNoMensajes = 0;
                foreach (XElement vXmlMensaje in vXmlRespuesta.Element("MENSAJES").Elements("MENSAJE"))
                {
                    vMensajes += vXmlMensaje.Value;
                    vNoMensajes++;
                }
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensajes, E_TIPO_RESPUESTA_DB.WARNING, pAlto: (120 + (vNoMensajes * 16)));
            }
        }

        private XElement GuardarFormularioNomina()
        {
            XElement vXmlPlantillaNO = new XElement("NOMINA");

            XAttribute CL_CAMPO = new XAttribute("CL_CAMPO", "RAZON_SOCIAL");
            XAttribute NB_CAMPO = new XAttribute("NB_CAMPO", cmbRazonSocial.SelectedValue.ToString());
            XElement NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);

            CL_CAMPO = new XAttribute("CL_CAMPO", "REGISTRO_PATRONAL");
            NB_CAMPO = new XAttribute("NB_CAMPO", cmbRegistroPatronal.SelectedValue.ToString());
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);

            CL_CAMPO = new XAttribute("CL_CAMPO", "TIPO_TRABAJO_SUA");
            NB_CAMPO = new XAttribute("NB_CAMPO", cmbTipoTrabajoSUA.SelectedValue.ToString());
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);

            CL_CAMPO = new XAttribute("CL_CAMPO", "TIPO_JORNADA_SUA");
            NB_CAMPO = new XAttribute("NB_CAMPO", cmbTipoJornadaSUA.SelectedValue.ToString());
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);

            CL_CAMPO = new XAttribute("CL_CAMPO", "TIPO_CONTRATO_SAT");
            NB_CAMPO = new XAttribute("NB_CAMPO", cmbTipoContratoSAT.SelectedValue.ToString());
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);

            CL_CAMPO = new XAttribute("CL_CAMPO", "TIPO_JORNADA_SAT");
            NB_CAMPO = new XAttribute("NB_CAMPO", cmbTipoJornadaSAT.SelectedValue.ToString());
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);

            CL_CAMPO = new XAttribute("CL_CAMPO", "REGIMEN_CONTRATACION");
            NB_CAMPO = new XAttribute("NB_CAMPO", cmbRegimenContratacion.SelectedValue.ToString());
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);

            CL_CAMPO = new XAttribute("CL_CAMPO", "UBICACION");
            NB_CAMPO = new XAttribute("NB_CAMPO", txtUbicacionNO.Text);
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);

            CL_CAMPO = new XAttribute("CL_CAMPO", "TIPO_SALARIO");
            NB_CAMPO = new XAttribute("NB_CAMPO", cmbTipoSalario.SelectedValue.ToString());
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);

            CL_CAMPO = new XAttribute("CL_CAMPO", "UMF");
            NB_CAMPO = new XAttribute("NB_CAMPO", txtUMFNO.Text);
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);

            CL_CAMPO = new XAttribute("CL_CAMPO", "RIESGO_PUESTO");
            NB_CAMPO = new XAttribute("NB_CAMPO", cmbRiesgoPuesto.SelectedValue.ToString());
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);
            
            //CL_CAMPO = new XAttribute("CL_CAMPO", "HORARIO");
            //NB_CAMPO = new XAttribute("NB_CAMPO", cmbHorarioNO.SelectedValue.ToString());
            //NOMINA = new XElement("CAMPO_NOMINA");
            //NOMINA.Add(CL_CAMPO);
            //NOMINA.Add(NB_CAMPO);
            //vXmlPlantillaNO.Add(NOMINA);
            
            CL_CAMPO = new XAttribute("CL_CAMPO", "PAQUETE_PRESTACIONES");
            NB_CAMPO = new XAttribute("NB_CAMPO", cmbPaquetePrestacionesNO.SelectedValue.ToString());
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);
            
            CL_CAMPO = new XAttribute("CL_CAMPO", "FORMATO_DISPERSION");
            NB_CAMPO = new XAttribute("NB_CAMPO", cmbFormatoDispersionNO.SelectedValue.ToString());
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);
            
            CL_CAMPO = new XAttribute("CL_CAMPO", "FORMATO_VALES_GASOLINA");
            NB_CAMPO = new XAttribute("NB_CAMPO", cmbFormatoValesGasolinaNO.SelectedValue.ToString());
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);

            CL_CAMPO = new XAttribute("CL_CAMPO", "FORMATO_VALES_DESPENSA");
            NB_CAMPO = new XAttribute("NB_CAMPO", cmbFormatoValesDespensaNO.SelectedValue.ToString());
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);
            
            CL_CAMPO = new XAttribute("CL_CAMPO", "TIPO_NOMINA");
            NB_CAMPO = new XAttribute("NB_CAMPO", cmbTipoNomina.SelectedValue.ToString());
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);
            
            CL_CAMPO = new XAttribute("CL_CAMPO", "FORMA_PAGO");
            NB_CAMPO = new XAttribute("NB_CAMPO", cmbFormaPago.SelectedValue.ToString());
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);
            
            CL_CAMPO = new XAttribute("CL_CAMPO", "BANCO");
            NB_CAMPO = new XAttribute("NB_CAMPO", cmbBanco.SelectedValue.ToString());
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);
            
            CL_CAMPO = new XAttribute("CL_CAMPO", "COTIZA_IMSS");
            NB_CAMPO = new XAttribute("NB_CAMPO", btnCotizaIMSSTrue.Checked);
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);
            
            CL_CAMPO = new XAttribute("CL_CAMPO", "FECHA_REINGRESO");
            NB_CAMPO = new XAttribute("NB_CAMPO", txtFeReingreso.SelectedDate);
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);
            
            CL_CAMPO = new XAttribute("CL_CAMPO", "FECHA_ANTIGUEDAD");
            NB_CAMPO = new XAttribute("NB_CAMPO", txtFeAntiguedad.SelectedDate);
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);
            
            if(txtFePlanta.SelectedDate != null)
            {
                CL_CAMPO = new XAttribute("CL_CAMPO", "FECHA_PLANTA");
                NB_CAMPO = new XAttribute("NB_CAMPO", txtFePlanta.SelectedDate);
                NOMINA = new XElement("CAMPO_NOMINA");
                NOMINA.Add(CL_CAMPO);
                NOMINA.Add(NB_CAMPO);
                vXmlPlantillaNO.Add(NOMINA);
            }
            
            
            CL_CAMPO = new XAttribute("CL_CAMPO", "SUELDO_DIARIO");
            NB_CAMPO = new XAttribute("NB_CAMPO", double.Parse(txtSueldoDiario.Value.ToString()));
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);
            
            CL_CAMPO = new XAttribute("CL_CAMPO", "SUELDO_MENSUAL");
            NB_CAMPO = new XAttribute("NB_CAMPO", double.Parse(txtSueldoMensual.Value.ToString()));
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);
            
            CL_CAMPO = new XAttribute("CL_CAMPO", "FACTOR_SALARIO_BASE_COTIZACION");
            NB_CAMPO = new XAttribute("NB_CAMPO", double.Parse(txtFactorBaseCotizacion.Value.ToString()));
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);
            
            CL_CAMPO = new XAttribute("CL_CAMPO", "SALARIO_BASE_COTIZACION_FIJO");
            NB_CAMPO = new XAttribute("NB_CAMPO", double.Parse(txtBaseCotizacionFijo.Value.ToString()));
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);
            
            CL_CAMPO = new XAttribute("CL_CAMPO", "SALARIO_BASE_COTIZACION_DETERMINADO");
            NB_CAMPO = new XAttribute("NB_CAMPO", double.Parse(txtBaseCotizacionDeterminado.Value.ToString()));
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);
            
            CL_CAMPO = new XAttribute("CL_CAMPO", "SALARIO_BASE_COTIZACION_MAXIMO");
            NB_CAMPO = new XAttribute("NB_CAMPO", double.Parse(txtBaseCotizacionMaximo.Value.ToString()));
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);
            
            CL_CAMPO = new XAttribute("CL_CAMPO", "SALARIO_BASE_COTIZACION");
            NB_CAMPO = new XAttribute("NB_CAMPO", double.Parse(txtSalarioBaseCotizacion.Value.ToString()));
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);

            CL_CAMPO = new XAttribute("CL_CAMPO", "CUENTA_PAGO");
            NB_CAMPO = new XAttribute("NB_CAMPO", txtCuentaPago.Text);
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);
            
            CL_CAMPO = new XAttribute("CL_CAMPO", "CLAVE_PAGO");
            NB_CAMPO = new XAttribute("NB_CAMPO", txtClavePago.Text);
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);
            
            CL_CAMPO = new XAttribute("CL_CAMPO", "CUENTA_VALES_DESPENSA");
            NB_CAMPO = new XAttribute("NB_CAMPO", txtCuentaValesDespensa.Text);
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);

            CL_CAMPO = new XAttribute("CL_CAMPO", "FILLER01");
            NB_CAMPO = new XAttribute("NB_CAMPO", txtFILLER01.Text);
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);
            
            CL_CAMPO = new XAttribute("CL_CAMPO", "FILLER02");
            NB_CAMPO = new XAttribute("NB_CAMPO", txtFILLER02.Text);
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);
            
            CL_CAMPO = new XAttribute("CL_CAMPO", "FILLER03");
            NB_CAMPO = new XAttribute("NB_CAMPO", txtFILLER03.Text);
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);
            
            CL_CAMPO = new XAttribute("CL_CAMPO", "FILLER04");
            NB_CAMPO = new XAttribute("NB_CAMPO", txtFILLER04.Text);
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);
            
            CL_CAMPO = new XAttribute("CL_CAMPO", "FILLER05");
            NB_CAMPO = new XAttribute("NB_CAMPO", txtFILLER05.Text);
            NOMINA = new XElement("CAMPO_NOMINA");
            NOMINA.Add(CL_CAMPO);
            NOMINA.Add(NB_CAMPO);
            vXmlPlantillaNO.Add(NOMINA);

            return vXmlPlantillaNO;
        }

        private void AsignarAjax()
        {
            foreach (XElement vXmlContenedor in XElement.Parse(vXmlPlantilla).Element("CONTENEDORES").Elements("CONTENEDOR"))
            {
                string vNbContenedor = UtilXML.ValorAtributo<string>(vXmlContenedor.Attribute("ID_CONTENEDOR"));
                Contenedor vContenedor = ObtenerContenedores().Where(t => t.NbContenedor.Equals(vNbContenedor)).FirstOrDefault();

                if (vContenedor != null)
                {
                    Control vPageView = vContenedor.CtrlContenedor;
                    if (vPageView != null)
                    {
                        foreach (XElement vXmlControl in vXmlContenedor.Elements("CAMPO"))
                        {

                            string vIdCampo = UtilXML.ValorAtributo<string>(vXmlControl.Attribute("ID_CAMPO"));
                            Control vControl = ObtenerControl(vPageView, vIdCampo);

                            switch (UtilXML.ValorAtributo<string>(vXmlControl.Attribute("CL_TIPO")))
                            {
                                case "GRID":

                                    Control vBotonEliminar = vPageView.FindControl(String.Format("btnDel{0}", vIdCampo));
                                    Control vBotonAgregar = vPageView.FindControl(String.Format("btnAdd{0}", vIdCampo));
                                    Control vBotonCancelar = vPageView.FindControl(string.Format("btnCancelar{0}", vIdCampo));
                                    Control vBotonEditar = vPageView.FindControl(string.Format("btnEdit{0}", vIdCampo));
                                    //Control vBotonAceptar = vPageView.FindControl(string.Format("btnGuardar{0}", UtilXML.ValorAtributo<string>(vXmlControl.Attribute("ID_CAMPO"))));

                                    ramInventario.AjaxSettings.AddAjaxSetting(btnGuardar, vControl);
                                    ramInventario.AjaxSettings.AddAjaxSetting(vBotonEliminar, vControl);
                                    ramInventario.AjaxSettings.AddAjaxSetting(vBotonAgregar, vControl);

                                    ramInventario.AjaxSettings.AddAjaxSetting(vBotonEditar, vBotonAgregar);
                                    ramInventario.AjaxSettings.AddAjaxSetting(vBotonEditar, vBotonCancelar);

                                    ramInventario.AjaxSettings.AddAjaxSetting(vBotonAgregar, vBotonAgregar);
                                    ramInventario.AjaxSettings.AddAjaxSetting(vBotonAgregar, vBotonCancelar);

                                    ramInventario.AjaxSettings.AddAjaxSetting(vBotonCancelar, vBotonAgregar);
                                    ramInventario.AjaxSettings.AddAjaxSetting(vBotonCancelar, vBotonCancelar);

                                    foreach (XElement vCtrlFormulario in vXmlControl.Element("FORMULARIO").Elements("CAMPO"))
                                    {
                                        string vIdCampoFormulario = vCtrlFormulario.Attribute("ID_CAMPO").Value;
                                        string vClTipoControl = UtilXML.ValorAtributo<string>(vCtrlFormulario.Attribute("CL_TIPO"));
                                        Control vControlFormulario;
                                        /*Control vControlFormulario2;*/

                                        /*
                                        ramInventario.AjaxSettings.AddAjaxSetting(vBotonAgregar, vControlFormulario, ralpInventario, UpdatePanelRenderMode.Inline);
                                        ramInventario.AjaxSettings.AddAjaxSetting(vBotonCancelar, vControlFormulario, ralpInventario, UpdatePanelRenderMode.Inline);
                                        ramInventario.AjaxSettings.AddAjaxSetting(vBotonEditar, vControlFormulario, ralpInventario, UpdatePanelRenderMode.Inline);
                                        */

                                        if (vClTipoControl.Equals("COMBOBOX"))
                                        {
                                            vControlFormulario = ObtenerControl(vPageView, vIdCampoFormulario);
                                            ramInventario.AjaxSettings.AddAjaxSetting(vBotonAgregar, vControlFormulario, ralpInventario, UpdatePanelRenderMode.Inline);
                                            ramInventario.AjaxSettings.AddAjaxSetting(vBotonCancelar, vControlFormulario, ralpInventario, UpdatePanelRenderMode.Inline);
                                            ramInventario.AjaxSettings.AddAjaxSetting(vBotonEditar, vControlFormulario, ralpInventario, UpdatePanelRenderMode.Inline);

                                            XElement vXmlCtrlFormularioDependientes = vCtrlFormulario.Element("DEPENDENCIAS");
                                            string g = vCtrlFormulario.Attribute("ID_CAMPO").Value;

                                            if (vXmlCtrlFormularioDependientes != null)
                                            {
                                                foreach (XElement vXmlControlDependiente in vXmlCtrlFormularioDependientes.Elements("CAMPO_DEPENDIENTE"))
                                                {

                                                    Control vControlDependiente = ObtenerControl(vPageView, UtilXML.ValorAtributo<string>(vXmlControlDependiente.Attribute("ID_CAMPO_DEPENDIENTE")));
                                                    ramInventario.AjaxSettings.AddAjaxSetting(vControlFormulario, vControlDependiente);
                                                }
                                            }
                                        }
                                        else if (vClTipoControl.Equals("DATEAGE"))
                                        {
                                            vControlFormulario = ObtenerControl(vPageView, vIdCampoFormulario);
                                            ramInventario.AjaxSettings.AddAjaxSetting(vBotonAgregar, vControlFormulario, ralpInventario, UpdatePanelRenderMode.Inline);
                                            ramInventario.AjaxSettings.AddAjaxSetting(vBotonCancelar, vControlFormulario, ralpInventario, UpdatePanelRenderMode.Inline);
                                            ramInventario.AjaxSettings.AddAjaxSetting(vBotonEditar, vControlFormulario, ralpInventario, UpdatePanelRenderMode.Inline);

                                            Control txtFormularioEdad = ObtenerControl(vPageView, String.Format("txt{0}", vIdCampoFormulario));
                                            ramInventario.AjaxSettings.AddAjaxSetting(vControlFormulario, txtFormularioEdad, ralpInventario, UpdatePanelRenderMode.Inline);
                                            ramInventario.AjaxSettings.AddAjaxSetting(vBotonCancelar, txtFormularioEdad, ralpInventario, UpdatePanelRenderMode.Inline);
                                            ramInventario.AjaxSettings.AddAjaxSetting(vBotonAgregar, txtFormularioEdad, ralpInventario, UpdatePanelRenderMode.Inline);
                                            ramInventario.AjaxSettings.AddAjaxSetting(vBotonEditar, txtFormularioEdad, ralpInventario, UpdatePanelRenderMode.Inline);
                                        }
                                        else if (vClTipoControl.Equals("CHECKBOX"))
                                        {
                                            vControlFormulario = ObtenerControl(vPageView, vIdCampoFormulario);
                                            //vControlFormulario2 = ObtenerControl(vPageView, vIdCampoFormulario + "False");

                                            ramInventario.AjaxSettings.AddAjaxSetting(vBotonAgregar, vControlFormulario, ralpInventario, UpdatePanelRenderMode.Inline);
                                            ramInventario.AjaxSettings.AddAjaxSetting(vBotonCancelar, vControlFormulario, ralpInventario, UpdatePanelRenderMode.Inline);
                                            ramInventario.AjaxSettings.AddAjaxSetting(vBotonEditar, vControlFormulario, ralpInventario, UpdatePanelRenderMode.Inline);

                                            /*
                                            ramInventario.AjaxSettings.AddAjaxSetting(vBotonAgregar, vControlFormulario2, ralpInventario, UpdatePanelRenderMode.Inline);
                                            ramInventario.AjaxSettings.AddAjaxSetting(vBotonCancelar, vControlFormulario2, ralpInventario, UpdatePanelRenderMode.Inline);
                                            ramInventario.AjaxSettings.AddAjaxSetting(vBotonEditar, vControlFormulario2, ralpInventario, UpdatePanelRenderMode.Inline);
                                            */
                                        }

                                        else
                                        {

                                            vControlFormulario = ObtenerControl(vPageView, vIdCampoFormulario);
                                            ramInventario.AjaxSettings.AddAjaxSetting(vBotonAgregar, vControlFormulario, ralpInventario, UpdatePanelRenderMode.Inline);
                                            ramInventario.AjaxSettings.AddAjaxSetting(vBotonCancelar, vControlFormulario, ralpInventario, UpdatePanelRenderMode.Inline);
                                            ramInventario.AjaxSettings.AddAjaxSetting(vBotonEditar, vControlFormulario, ralpInventario, UpdatePanelRenderMode.Inline);

                                        }
                                    }

                                    break;

                                case "DATEAGE":
                                    Control txtEdad = vPageView.FindControl(String.Format("txt{0}", vIdCampo));
                                    ramInventario.AjaxSettings.AddAjaxSetting(vControl, txtEdad, ralpInventario, UpdatePanelRenderMode.Inline);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        protected void CargarDatos()
        {
            CamposNominaNegocio oNegocio = new CamposNominaNegocio();

            //Get the razon social combo
            listRazonSocial = oNegocio.ObtieneRazonSocial(vClCliente, true);
            cmbRazonSocial.DataSource = listRazonSocial;
            cmbRazonSocial.DataValueField = "ID_RAZON_SOCIAL";
            cmbRazonSocial.DataTextField = "CL_RAZON_SOCIAL";
            cmbRazonSocial.DataBind();

            //Get the tipo de trabajo SUA combo
            listTipoTrabajoSUA = oNegocio.ObtieneTipoTrabajoSUA(null, null);
            cmbTipoTrabajoSUA.DataSource = listTipoTrabajoSUA;
            cmbTipoTrabajoSUA.DataValueField = "CL_TIPO_TRAB_SUA";
            cmbTipoTrabajoSUA.DataTextField = "DS_TIPO_TRAB_SUA";
            cmbTipoTrabajoSUA.DataBind();

            //Get the tipo de jornada SUA combo
            listTipoJornadaSUA = oNegocio.ObtieneTipoJornadaSUA(null, null);
            cmbTipoJornadaSUA.DataSource = listTipoJornadaSUA;
            cmbTipoJornadaSUA.DataValueField = "CL_JORNADA_SUA";
            cmbTipoJornadaSUA.DataTextField = "DS_JORNADA_SUA";
            cmbTipoJornadaSUA.DataBind();

            //Get the tipo de contrato SAT combo
            listTipoContratoSAT = oNegocio.ObtieneTipoContratoSAT(null, null);
            cmbTipoContratoSAT.DataSource = listTipoContratoSAT;
            cmbTipoContratoSAT.DataValueField = "CL_TIPO_CONTRATO";
            cmbTipoContratoSAT.DataTextField = "DS_TIPO_CONTRATO";
            cmbTipoContratoSAT.DataBind();

            //Get the tipo de jornada SAT combo
            listTipoJornadaSAT = oNegocio.ObtieneTipoJornadaSAT(null, null);
            cmbTipoJornadaSAT.DataSource = listTipoJornadaSAT;
            cmbTipoJornadaSAT.DataValueField = "CL_TIPO_JORNADA";
            cmbTipoJornadaSAT.DataTextField = "DS_TIPO_JORNADA";
            cmbTipoJornadaSAT.DataBind();

            //Get the tipo de regimen SAT combo
            listRegimenSAT = oNegocio.ObtieneRegimenSAT();
            cmbRegimenContratacion.DataSource = listRegimenSAT;
            cmbRegimenContratacion.DataValueField = "CL_REGIMEN";
            cmbRegimenContratacion.DataTextField = "NB_REGIMEN";
            cmbRegimenContratacion.DataBind();

            //Get the tipo salario SUA combo
            listTipoSalarioSUA = oNegocio.ObtieneTipoSalarioSUA();
            cmbTipoSalario.DataSource = listTipoSalarioSUA;
            cmbTipoSalario.DataValueField = "CL_TIPO_SALARIO_SUA";
            cmbTipoSalario.DataTextField = "DS_TIPO_SALARIO_SUA";
            cmbTipoSalario.DataBind();

            //Get the riesgo puesto combo
            listRiesgoPuesto = oNegocio.ObtieneRiesgoPuesto();
            cmbRiesgoPuesto.DataSource = listRiesgoPuesto;
            cmbRiesgoPuesto.DataValueField = "CL_RIESGO_PUESTO";
            cmbRiesgoPuesto.DataTextField = "NB_RIESGO_PUESTO";
            cmbRiesgoPuesto.DataBind();

            //Get the horario semana combo
            //listHorarioSemana = oNegocio.ObtieneHorarioSemana();
            //cmbHorarioNO.DataSource = listHorarioSemana;
            //cmbHorarioNO.DataValueField = "CL_HORARIO_SEMANA";
            //cmbHorarioNO.DataTextField = "NB_HORARIO_SEMANA";
            //cmbHorarioNO.DataBind();

            //Get the paquete de prestaciones combo
            listPaquetePrestaciones = oNegocio.ObtienePaquetePrestaciones();
            cmbPaquetePrestacionesNO.DataSource = listPaquetePrestaciones;
            cmbPaquetePrestacionesNO.DataValueField = "ID_PAQUETE_PRESTACIONES";
            cmbPaquetePrestacionesNO.DataTextField = "DS_PAQUETE";
            cmbPaquetePrestacionesNO.DataBind();

            //Get the formato dispersion
            E_FORMATO_DISPERSION formatoDispersion = new E_FORMATO_DISPERSION();
            listFormatoDispersion = oNegocio.ObtieneFormatoDispersion(formatoDispersion);

            //Get the formato dispersion combo
            cmbFormatoDispersionNO.DataSource = listFormatoDispersion.Where(x => x.CL_TIPO_FORMATO.Equals("D"));
            cmbFormatoDispersionNO.DataValueField = "CL_FORMATO";
            cmbFormatoDispersionNO.DataTextField = "NB_FORMATO";
            cmbFormatoDispersionNO.DataBind();

            //Get the formato vales gasolina combo
            cmbFormatoValesGasolinaNO.DataSource = listFormatoDispersion.Where(x => x.CL_TIPO_FORMATO.Equals("V"));
            cmbFormatoValesGasolinaNO.DataValueField = "CL_FORMATO";
            cmbFormatoValesGasolinaNO.DataTextField = "NB_FORMATO";
            cmbFormatoValesGasolinaNO.DataBind();

            //Get the formato vales despensa combo
            cmbFormatoValesDespensaNO.DataSource = listFormatoDispersion.Where(x => x.CL_TIPO_FORMATO.Equals("V"));
            cmbFormatoValesDespensaNO.DataValueField = "CL_FORMATO";
            cmbFormatoValesDespensaNO.DataTextField = "NB_FORMATO";
            cmbFormatoValesDespensaNO.DataBind();

            E_TIPO_NOMINA tipoNomina = new E_TIPO_NOMINA();
            tipoNomina.ID_TIPO_NOMINA = null;
            tipoNomina.FG_ACTIVO = true;

            listTipoNomina = oNegocio.ObtieneTipoNomina(tipoNomina);
            cmbTipoNomina.DataSource = listTipoNomina;
            cmbTipoNomina.DataValueField = "ID_TIPO_NOMINA";
            cmbTipoNomina.DataTextField = "DS_TIPO_NOMINA";
            cmbTipoNomina.DataBind();

            E_FORMA_PAGO formaPago = new E_FORMA_PAGO();
            formaPago.FG_ACTIVO = true;

            listFormaPago = oNegocio.ObtieneFormaPago(formaPago);
            cmbFormaPago.DataSource = listFormaPago;
            cmbFormaPago.DataValueField = "CL_FORMA_PAGO";
            cmbFormaPago.DataTextField = "NB_FORMA_PAGO";
            cmbFormaPago.DataBind();

            E_BANCO banco = new E_BANCO();
            banco.FG_ACTIVO = true;

            listBancoNomina = oNegocio.ObtieneBancosNomina(banco);
            cmbBanco.DataSource = listBancoNomina;
            cmbBanco.DataValueField = "CL_BANCO";
            cmbBanco.DataTextField = "NB_BANCO";
            cmbBanco.DataBind();

            //OBTENER UMA
            DateTime FE_HOY = System.DateTime.Now;
            var vUma = oNegocio.ObtenerUMA().Where(x => x.FE_INICIAL <= FE_HOY && x.FE_FINAL >= FE_HOY).FirstOrDefault();
            salarioMinDF.Value = (vUma != null ? vUma.MN_UMA.ToString() : "0");

            if (salarioMinDF.Value == "0")
            {
                //Mensaje de que no se encontro una UMA vigente
                //Utileria.DisplayMessageRadNotification(-1, "No existe una UMA vigente para este día, favor de agregar el registro correspondiente.", rnMensaje);
            }

        }

        protected void CargarDatosNomina(int? vIdEmpleadoNominaDo)
        {
            CamposNominaNegocio oNegocio = new CamposNominaNegocio();
            List<E_EMPLEADO_NOMINA> vEmpleadoNO = new List<E_EMPLEADO_NOMINA>();

            vEmpleadoNO = oNegocio.ObtieneDatosPersonaNO(pID_EMPLEADO: vIdEmpleadoNominaDo);

            foreach (E_EMPLEADO_NOMINA item in vEmpleadoNO)
            {
                cmbRazonSocial.SelectedValue = item.ID_RAZON_SOCIAL.ToString();
                
                cmbRegistroPatronal.DataSource = null;
                listRegistrosPatronales = oNegocio.ObtieneRegistroPatronal(Guid.Parse(cmbRazonSocial.SelectedValue), null, true);
                cmbRegistroPatronal.DataSource = listRegistrosPatronales;
                cmbRegistroPatronal.DataTextField = "CL_REGISTRO_PATRONAL";
                cmbRegistroPatronal.DataValueField = "ID_REGISTRO_PATRONAL";
                cmbRegistroPatronal.DataBind();

                cmbRegistroPatronal.SelectedValue = item.ID_REGISTRO_PATRONAL.ToString();
                cmbTipoTrabajoSUA.SelectedValue = item.CL_TIPO_TRAB_SUA;
                cmbTipoJornadaSUA.SelectedValue = item.CL_JORNADA_SUA;
                cmbTipoContratoSAT.SelectedValue = item.CL_TIPO_CONTRATO_SAT;
                cmbTipoJornadaSAT.SelectedValue = item.CL_TIPO_JORNADA_SAT;
                cmbRegimenContratacion.SelectedValue = item.CL_REGIMEN_CONTRATACION;
                txtUbicacionNO.Text = item.CL_UBICACION_SUA;
                cmbTipoSalario.SelectedValue = item.CL_TIPO_SALARIO_SUA.ToString();
                txtUMFNO.Text = item.NO_UMF;
                cmbRiesgoPuesto.SelectedValue = item.CL_RIESGO_PUESTO;
                //cmbHorarioNO.SelectedValue = item.CL_HORARIO;
                cmbPaquetePrestacionesNO.SelectedValue = item.ID_PAQUETE_PRESTACIONES.ToString();
                cmbFormatoDispersionNO.SelectedValue = item.CL_FORMATO_DISPERSION;
                cmbFormatoValesGasolinaNO.SelectedValue = item.CL_FORMATO_VALES_G;
                cmbFormatoValesDespensaNO.SelectedValue = item.CL_FORMATO_VALES_D;
                cmbTipoNomina.SelectedValue = item.ID_TIPO_NOMINA.ToString();
                cmbFormaPago.SelectedValue = item.CL_FORMA_PAGO;
                cmbBanco.SelectedValue = item.CL_BANCO_SAT;
                txtCuentaPago.Text = item.NO_CUENTA_PAGO;
                txtClavePago.Text = item.NO_CLABE_PAGO;
                txtCuentaValesDespensa.Text = item.NO_CUENTA_DESPENSA;
                txtFILLER01.Text = item.FILLER01;
                txtFILLER02.Text = item.FILLER02;
                txtFILLER03.Text = item.FILLER03;
                txtFILLER04.Text = item.FILLER04;
                txtFILLER05.Text = item.FILLER05;

                if (item.MN_SNOMINAL.ToString() != "0.0")
                    txtSueldoDiario.Value = double.Parse(item.MN_SNOMINAL.ToString());

                if (item.MN_SNOMINAL_MENSUAL.ToString() != "0.0")
                    txtSueldoMensual.Value = double.Parse(item.MN_SNOMINAL_MENSUAL.ToString());

                if (item.NO_FACTOR_SBC.ToString() != "0.0")
                    txtFactorBaseCotizacion.Value = double.Parse(item.NO_FACTOR_SBC.ToString());

                if (item.MN_SBC_FIJO.ToString() != "0.0")
                    txtBaseCotizacionFijo.Value = double.Parse(item.MN_SBC_FIJO.ToString());

                if (item.MN_SBC_DETERMINADO.ToString() != "0.0")
                    txtBaseCotizacionDeterminado.Value = double.Parse(item.MN_SBC_DETERMINADO.ToString());

                if (item.MN_SBC_MAXIMO.ToString() != "0.0")
                    txtBaseCotizacionMaximo.Value = double.Parse(item.MN_SBC_MAXIMO.ToString());

                if (item.MN_SBC.ToString() != "0.0")
                    txtSalarioBaseCotizacion.Value = double.Parse(item.MN_SBC.ToString());

                if (item.FE_REINGRESO != null)
                    txtFeReingreso.SelectedDate = DateTime.Parse(item.FE_REINGRESO);

                if (item.FE_ANTIGUEDAD != null)
                    txtFeAntiguedad.SelectedDate = DateTime.Parse(item.FE_ANTIGUEDAD);

                if (item.FE_PLANTA != null)
                    txtFePlanta.SelectedDate = DateTime.Parse(item.FE_PLANTA);

                if (item.FG_COTIZA_IMSS == true)
                {
                    btnCotizaIMSSTrue.Checked = true;
                    btnCotizaIMSSFalse.Checked = false;
                }
                else
                {
                    btnCotizaIMSSTrue.Checked = false;
                    btnCotizaIMSSFalse.Checked = true;
                }
            }

        }

        private void CalcularSBC()
        {
            bool bandera = true;
            string mensaje = "Debes completar los siguientes campos: ";

            if (cmbPaquetePrestacionesNO.SelectedValue == "")
            {
                mensaje = mensaje + "paquete de prestaciones,";
                bandera = false;
            }

            if (txtFeAntiguedad.SelectedDate == null)
            {
                mensaje = mensaje + "fecha de antigüedad,";
                bandera = false;
            }

            if (txtFeReingreso.SelectedDate == null)
            {
                mensaje = mensaje + "fecha de ingreso,";
                bandera = false;
            }

            if (txtSueldoDiario.Value == null)
            {
                mensaje = mensaje + "sueldo diario,";
                bandera = false;
            }

            if (bandera)
            {
                decimal vSBCFijo;
                decimal vSueldoMensual;
                decimal vSueldoMaximo;
                decimal vSalarioMinimo;
                decimal vSalarioBase;

                TimeSpan dif;
                double no_antiguedad = 1;
                int antiguedad = 0;

                E_ANTIGUEDAD vFactor = new E_ANTIGUEDAD();
                CamposNominaNegocio oNegocio = new CamposNominaNegocio();

                dif = DateTime.Today - txtFeAntiguedad.SelectedDate.Value;
                no_antiguedad = dif.TotalDays / double.Parse("365");

                antiguedad = Convert.ToInt32(no_antiguedad);
                if (antiguedad == 0) antiguedad = 1;

                vFactor = oNegocio.ObtenerTablaAntiguedad(CL_CLIENTE: vClCliente, ID_PAQUETE_PRESTACIONES: Guid.Parse(cmbPaquetePrestacionesNO.SelectedValue.ToString()), NO_ANTIGUEDAD: short.Parse(antiguedad.ToString())).FirstOrDefault();

                if (vFactor != null)
                {

                    decimal vSueldoDiario = decimal.Parse(txtSueldoDiario.Value.ToString());
                    decimal vFactorSBC = vFactor.NO_FACTOR_SBC;

                    List<E_CONFIGURACION> vConfiguracion = new List<E_CONFIGURACION>();
                    E_CONFIGURACION sConfiguracion = new E_CONFIGURACION();

                    sConfiguracion.CL_CLIENTE = vClCliente;
                    sConfiguracion.CL_CONFIGURACION = "NO_DIAS_CALCULO";

                    vConfiguracion = oNegocio.ObtenerConfiguracion(sConfiguracion);


                    vSalarioMinimo = decimal.Parse(salarioMinDF.Value);

                    if (vConfiguracion != null && vConfiguracion.Count > 0)
                    {
                        vSueldoMensual = (vSueldoDiario * (vConfiguracion == null ? 30 : Decimal.Parse(vConfiguracion.FirstOrDefault().NO_CONFIGURACION.ToString())));
                    }
                    else
                    {
                        vSueldoMensual = (vSueldoDiario * 30);
                    }

                    vSBCFijo = (vSueldoDiario * vFactorSBC);
                    vSueldoMaximo = vSalarioMinimo * 25;

                    if (vSueldoMaximo < vSBCFijo)
                    {
                        vSalarioBase = vSueldoMaximo;
                    }
                    else
                    {
                        vSalarioBase = vSBCFijo;
                    }

                    txtBaseCotizacionFijo.Value = double.Parse(vSBCFijo.ToString());
                    txtSueldoMensual.Value = double.Parse(vSueldoMensual.ToString());
                    txtBaseCotizacionDeterminado.Value = double.Parse(vSBCFijo.ToString());
                    txtBaseCotizacionMaximo.Value = double.Parse(vSueldoMaximo.ToString());
                    txtSalarioBaseCotizacion.Value = double.Parse(vSalarioBase.ToString());
                    txtFactorBaseCotizacion.Value = double.Parse(vFactorSBC.ToString());
                    txtBaseCotizacionVariable.Value = double.Parse("0.00");

                    if (vSalarioBase == 0)
                    {
                        UtilMensajes.MensajeResultadoDB(rwmAlertas, "El salario base de cotizacion es cero.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                    }
                }
            }
            else
                UtilMensajes.MensajeResultadoDB(rwmAlertas, mensaje, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");


        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
            vClCliente = ContextoUsuario.clCliente;

            if (!Page.IsPostBack)
            {
                
                vIdEmpleadoVS = vIdEmpleado;
                vXmlEmpleadoPlantilla = vXmlPlantilla;
                vIdItemFotografia = vIdItemFoto;
                CargarDocumentos();


                if (vIdEmpleado != null)
                {
                    pvwReportes.Visible = true;
                    CargarReporteModular();
                    vPlantilla.CargarGrid();
                }
                else
                {
                    vDatosReporteModular = null;
                }
            }

            AsignarAjax();
            vPlantilla.xmlPlantilla = vXmlEmpleadoPlantilla;
            vClRutaArchivosTemporales = Server.MapPath(ContextoApp.ClRutaArchivosTemporales);

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int vIdEmpleadoQS = -1;
                if (int.TryParse(Request.QueryString["EmpleadoId"], out vIdEmpleadoQS))
                    vIdEmpleado = vIdEmpleadoQS;
                else if (int.TryParse(Request.QueryString["EmpleadoNoDoId"], out vIdEmpleadoQS))
                    vIdEmpleado = vIdEmpleadoQS;

                if (Request.QueryString["Ventana"] != null)
                {
                    if (Request.QueryString["Ventana"].ToString() == "CONSULTA")
                    {
                        btnGuardar.Enabled = false;
                        btnGuardarSalir.Enabled = false;
                        btnGuardar.Visible = false;
                        btnGuardarSalir.Visible = false;
                        btnCancelar.Enabled = false;
                        btnCancelar.Visible = false;
                        btnCerrar.Visible = false;
                    }
                }
                    

                if (vIdEmpleadoQS != null)
                {
                    CamposNominaNegocio oNegocio = new CamposNominaNegocio();
                    E_EMPLEADO_NOMINA_DO vEmpleado = oNegocio.ObtienePersonalNominaDo(pID_EMPLEADO: vIdEmpleadoQS).FirstOrDefault();
                    if (vEmpleado != null)
                    {
                        if (vEmpleado.FG_NOMINA == true && ContextoApp.ANOM.LicenciaAccesoModulo.MsgActivo == "1")
                        {
                            CargarDatos();
                            tabSolicitud.Tabs[8].Visible = true;

                            if(vEmpleado.FG_COMPLETO == "SI")
                                CargarDatosNomina(vIdEmpleadoQS);
                        }
                        else
                        {
                            tabSolicitud.Tabs[8].Visible = false;
                        }

                        vIdEmpleado = vEmpleado.ID_EMPLEADO;                        
                    }
                }

                if (Request.QueryString["pFgHabilitaBotones"] == "False")
                {
                    btnGuardar.Visible = false;
                    btnGuardarSalir.Visible = false;
                    btnCancelar.Visible = false;
                }
            }

            if (vDatosModificar == null)
            {
                vDatosModificar = new List<KeyValuePair<string, string>>();
            }

            if (vControlDependienteSeleccion == null)
            {
                vControlDependienteSeleccion = new List<E_GENERICA>();
            }

            EmpleadoNegocio nEmpleado = new EmpleadoNegocio();
            // Se agrega ContextoUsuario.oUsuario.ID_PLANTILLA como parametro al spe que obtiene la plantilla.
            SPE_OBTIENE_EMPLEADO_PLANTILLA_Result vSolicitud = nEmpleado.ObtenerPlantilla(ContextoUsuario.oUsuario.ID_PLANTILLA, vIdEmpleado, ContextoUsuario.oUsuario.ID_EMPRESA);
            vXmlPlantilla = vSolicitud.XML_SOLICITUD_PLANTILLA;
            vXmlDocumentos = vSolicitud.XML_VALORES;
            vIdItemFoto = vSolicitud.ID_ITEM_FOTOGRAFIA;

            if (vSolicitud.CL_ESTADO_EMPLEADO != null)
            {
                if (vSolicitud.CL_ESTADO_EMPLEADO != "ALTA")
                {
                    btnGuardar.Enabled = false;
                    btnGuardarSalir.Enabled = false;
                }
                else
                {
                    SeguridadProcesos();
                }
            }

            if (vSolicitud.FI_FOTOGRAFIA != null)
            {
                rbiFotoEmpleado.DataValue = vSolicitud.FI_FOTOGRAFIA;
                btnEliminarFotoEmpleado.Visible = true;
            }
            else
            {
                btnEliminarFotoEmpleado.Visible = false;
            }

            vPlantilla = new Plantilla()
            {
                ctrlPlantilla = new Contenedor() { NbContenedor = "PlantillaEmpleado", CtrlContenedor = mpgPlantilla },
                lstContenedores = ObtenerContenedores(),
                xmlPlantilla = vXmlPlantilla
            };

            vPlantilla.CrearFormulario(!Page.IsPostBack);

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if(ValidarInformacion())
                Guardar(false);            
        }

        protected void btnGuardarSalir_Click(object sender, EventArgs e)
        {
            if (ValidarInformacion())
                Guardar(true);
        }

        protected void grdDocumentos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdDocumentos.DataSource = vLstDocumentos;//.Where(w => !w.CL_TIPO_DOCUMENTO.Equals("FOTOGRAFIA"));
        }

        protected void btnAddDocumento_Click(object sender, EventArgs e)
        {
            AddDocumento(cmbTipoDocumento.SelectedValue, rauDocumento);
            grdDocumentos.Rebind();
        }

        protected void btnDelDocumentos_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem i in grdDocumentos.SelectedItems)
                EliminarDocumento(i.GetDataKeyValue("ID_ITEM").ToString());
        }

        protected void rauFotoEmpleado_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            EliminarDocumento(vIdItemFotografia.ToString());
            using (Stream fileStream = rauFotoEmpleado.UploadedFiles[0].InputStream)
            {
                using (System.Drawing.Image bitmapImage = UtilImage.ResizeImage(System.Drawing.Image.FromStream(fileStream), 200, 200))
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        vNbLogotipo = rauFotoEmpleado.UploadedFiles[0].GetName();
                        if ((vNbLogotipo.Length != 0) && (vNbLogotipo.Length >= 50))
                        {
                            UtilMensajes.MensajeResultadoDB(rwmAlertas, "El nombre de la imagen no puede ser tan largo", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, null);
                            vNbLogotipo = null;
                            return;
                        }
                        else
                        {
                            bitmapImage.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                            vFiLogotipo = Convert.ToBase64String(stream.ToArray());
                            rbiFotoEmpleado.DataValue = stream.ToArray();
                        }
                    }
                }
            }
            btnEliminarFotoEmpleado.Visible = true;
            AddDocumento("FOTOGRAFIA", rauFotoEmpleado);
            grdDocumentos.Rebind();
        }

        protected void btnEliminarFotoEmpleado_Click(object sender, EventArgs e)
        {
            vFiLogotipo = null;
            rbiFotoEmpleado.DataValue = null;
            rbiFotoEmpleado.ImageUrl = "~/Assets/images/LoginUsuario.png";
            rbiFotoEmpleado.Width = 128;
            rbiFotoEmpleado.Height = 128;

            EliminarDocumento(vIdItemFotografia.ToString());
            btnEliminarFotoEmpleado.Visible = false;
        }

        protected void grdProgramas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (vIdEmpleadoVS != null)
            {
                grdProgramas.DataSource = vDatosReporteModular.DatosFyd.vLstProgramas;
            }
        }

        protected void grdEventos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (vIdEmpleado != null)
            {
                grdEventos.DataSource = vDatosReporteModular.DatosFyd.vLstEventos;
            }
        }

        protected void grdDesempeno_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (vIdEmpleado != null)
            {
                grdDesempeno.DataSource = vDatosReporteModular.DatosEo.vLstDesempeno;
            }
        }

        protected void grdRotacion_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (vIdEmpleado != null)
            {
                grdRotacion.DataSource = vDatosReporteModular.DatosEo.vLstRotacion;
            }
        }

        protected void grdCompensacion_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (vIdEmpleado != null)
            {
                grdCompensacion.DataSource = vDatosReporteModular.DatosMc.vLstBitacoraSueldos;
            }
        }

        

        protected void grdProgramas_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;

                int? vIdPrograma = int.Parse(item.GetDataKeyValue("ID_PROGRAMA").ToString());

                if (vIdPrograma != null)
                {
                    EventoCapacitacionNegocio neg = new EventoCapacitacionNegocio();
                    E_RESULTADO vResultado = neg.EliminaEmpleadoPrograma((int)vIdPrograma, (int)vIdEmpleadoVS);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    E_PROGRAMAS vItemPrograma = vDatosReporteModular.DatosFyd.vLstProgramas.Where(w => w.ID_PROGRAMA == vIdPrograma).FirstOrDefault();
                    vDatosReporteModular.DatosFyd.vLstProgramas.Remove(vItemPrograma);
                    grdProgramas.Rebind();
                }
            }

        }

        protected void cmbRazonSocial_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmbRazonSocial.Text != String.Empty)
            {
                CamposNominaNegocio oNegocio = new CamposNominaNegocio();

                cmbRegistroPatronal.DataSource = null;
                listRegistrosPatronales = oNegocio.ObtieneRegistroPatronal(Guid.Parse(cmbRazonSocial.SelectedValue), null, true);
                cmbRegistroPatronal.DataSource = listRegistrosPatronales;
                cmbRegistroPatronal.DataTextField = "CL_REGISTRO_PATRONAL";
                cmbRegistroPatronal.DataValueField = "ID_REGISTRO_PATRONAL";
                cmbRegistroPatronal.DataBind();
            }
        }

        protected void btnCalcularSueldo_Click(object sender, EventArgs e)
        {
            CalcularSBC();
        }
    }
}