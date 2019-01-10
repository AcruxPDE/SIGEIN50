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

        Plantilla vPlantilla;

        string vXmlPlantilla;

        int? vIdEmpleado;

        int? vIdEmpleadoNominaDo;

        public int? vIdCandidato
        {
            get { return (int?)ViewState["vs_vIdCandidato"]; }
            set { ViewState["vs_vIdCandidato"] = value; }
        }

        public string vUrlNomina
        {
            get { return (string)ViewState["vs_vUrlNomina"];}
            set { ViewState["vs_vUrlNomina"] = value; }
        }

        string vClUsuario;

        string vNbPrograma;

        string vXmlDocumentos;

        Guid? vIdItemFoto;

        string vClRutaArchivosTemporales;

        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private bool vGuardar;

        private int? vIdEmpresa;

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
            get { return (string)ViewState["vs_vClEstadoEmpleado"];}
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
                E_RESULTADO vResultado = nEmpleado.InsertaActualizaEmpleado(vXmlRespuesta.Element("PLANTILLA"), vIdEmpleadoVS, vLstArchivos, vLstDocumentos, vClUsuario, vNbPrograma, vTipoTransaccion);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                //resultado obtener el idEmpleado
                if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                {
                    var idEmpleado = 0;
                    var esNumero = int.TryParse(vResultado.MENSAJE.Where(x => x.CL_IDIOMA == "ID_EMPLEADO").FirstOrDefault().DS_MENSAJE, out idEmpleado);
                    vIdEmpleadoVS = idEmpleado;
                    vIdEmpleado = idEmpleado;
                }

                if (pFgCerrarVentana)
                {
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "OnCloseUpdate");
                }
                else
                {
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
                    Response.Redirect(Request.RawUrl); 
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

                                            Control txtFormularioEdad = ObtenerControl(vPageView,String.Format("txt{0}", vIdCampoFormulario));
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

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
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
            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int vIdEmpleadoQS = -1;
                if (int.TryParse(Request.QueryString["EmpleadoId"], out vIdEmpleadoQS))
                    vIdEmpleado = vIdEmpleadoQS;
                else if (int.TryParse(Request.QueryString["EmpleadoNoDoId"], out vIdEmpleadoQS))
                    vIdEmpleadoNominaDo = vIdEmpleadoQS;

                if (vIdEmpleadoNominaDo != null)
                {
                    CamposNominaNegocio oNegocio = new CamposNominaNegocio();
                    SPE_OBTIENE_EMPLEADOS_NOMINA_DO_Result vEmpleado = oNegocio.ObtieneEmpleadosNominaDo(pID_EMPLEADO_NOMINA_DO: vIdEmpleadoNominaDo).FirstOrDefault();
                    if (vEmpleado != null)
                    {
                        if (vEmpleado.FG_NOMINA == true && ContextoApp.ANOM.LicenciaAccesoModulo.MsgActivo == "1")
                        {
                            //Session["__clUsuario__"] = vClUsuario;
                            tabSolicitud.Tabs[8].Visible = true;
                            ifNomina.Attributes.Add("src", "/NOMINA/InventarioPersonal/PopupInventarioPersonalNuevoEditar.aspx?clOrigen=DO&clUsuario=" + vClUsuario + "&ID=" + vEmpleado.ID_EMPLEADO_NOMINA);
                        }

                        if (vEmpleado.FG_DO == true)
                        {
                            vIdEmpleado = vEmpleado.ID_EMPLEADO_DO;
                        }
                        else
                        {
                            tabSolicitud.Tabs[0].Visible = false;
                            tabSolicitud.Tabs[1].Visible = false;
                            tabSolicitud.Tabs[2].Visible = false;
                            tabSolicitud.Tabs[3].Visible = false;
                            tabSolicitud.Tabs[4].Visible = false;
                            tabSolicitud.Tabs[5].Visible = false;
                            tabSolicitud.Tabs[6].Visible = false;
                            tabSolicitud.Tabs[7].Visible = false;
                            pvwNomina.Selected = true;
                            tabSolicitud.Tabs[8].Selected = true;
                        }
                       
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
            //if (vClEstadoEmpleado == "ALTA")
            //{
            Guardar(false);
            // }
            //else
            //{
            //    UtilMensajes.MensajeResultadoDB(rwmAlertas, "No se puede guardar un empleado en estado de baja.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction:"");
            //}
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

        protected void btnGuardarSalir_Click(object sender, EventArgs e)
        {
            //if (vClEstadoEmpleado == "ALTA")
            //{
                Guardar(true);
            //}
            //else
            //{
            //    UtilMensajes.MensajeResultadoDB(rwmAlertas, "No se puede guardar un empleado en estado de baja.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction:"");
            //}
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
    }
}