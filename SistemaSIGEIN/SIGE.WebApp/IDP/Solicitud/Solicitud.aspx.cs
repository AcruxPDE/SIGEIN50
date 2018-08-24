using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;
using WebApp.Comunes;

namespace SIGE.WebApp.IDP.Solicitud
{
    public partial class Solicitud : System.Web.UI.Page
    {

        #region Variables

        List<E_DOCUMENTO> vLstDocumentos
        {
            get { return (List<E_DOCUMENTO>)ViewState["vs_LstDocumentos"]; }
            set { ViewState["vs_LstDocumentos"] = value; }
        }

        public string vXmlSolicitudPlantilla
        {
            get { return (string)ViewState["vs_vXmlSolicitudPlantilla"]; }
            set { ViewState["vs_vXmlSolicitudPlantilla"] = value; }
        }

        public int? vIdSolicitudVS
        {
            get { return (int?)ViewState["vs_vIdSolicitud"]; }
            set { ViewState["vs_vIdSolicitud"] = value; }
        }

        int? vIdPlantillaFormulario
        {
            get { return (int?)ViewState["vs_vFlPlantillaFormulario"]; }
            set { ViewState["vs_vFlPlantillaFormulario"] = value; }
        }

        public bool? vFgAceptaTermino
        {
            get { return (bool?)ViewState["vs_vFgAceptaTermino"]; }
            set { ViewState["vs_vFgAceptaTermino"] = value; }
        }

        Guid? vIdItemFotografia
        {
            get { return (Guid?)ViewState["vs_vIdItemFotografia"]; }
            set { ViewState["vs_vIdItemFotografia"] = value; }
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

        public List<KeyValuePair<string, string>> vDatosModificar
        {
            get { return (List<KeyValuePair<string, string>>)ViewState["vs_vDatosModificar"]; }
            set { ViewState["vs_vDatosModificar"] = value; }
        }

        public List<E_GENERICA> vControlDependienteSeleccion
        {
            get { return (List<E_GENERICA>)ViewState["vs_sol_control_dependiente_seleccion"]; }
            set { ViewState["vs_sol_control_dependiente_seleccion"] = value; }
        }

        public string cssModulo = String.Empty;
        Plantilla vPlantilla;
        string vXmlSolicitud;
        int? vIdSolicitud;
        public string vClUsuario;
        public bool MostrarPrivacidad;
        string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        string vClRutaArchivosTemporales;
        Guid? vIdItemFoto;
        string vXmlDocumentos;
        String vTipoTransaccion = "";
        #endregion

        #region Funciones

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

        public string ObtenerClientId(Control pCtrlContenedor, string pNbControl)
        {
            string vIdClientControl = String.Empty;
            Control vControl = pCtrlContenedor.FindControl(pNbControl);
            if (vControl != null)
                vIdClientControl = vControl.ClientID;
            return vIdClientControl;
        }

        protected void CargarDocumentos()
        {
            XElement x = XElement.Parse(vXmlDocumentos).Elements("VALOR").FirstOrDefault(f => UtilXML.ValorAtributo<string>(f.Attribute("ID_CAMPO")) == "LS_DOCUMENTOS");

            if (vLstDocumentos == null)
                vLstDocumentos = new List<E_DOCUMENTO>();

            if (x != null)
            {
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
        }

        private void Guardar(bool pFgCerrar)
        {
            if (vIdSolicitudVS != null)
            {
                vTipoTransaccion = E_TIPO_OPERACION_DB.A.ToString();
            }

            else
            {
                vTipoTransaccion = E_TIPO_OPERACION_DB.I.ToString();
            }


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

                vXmlSolicitudPlantilla = vXmlRespuesta.Element("PLANTILLA").ToString();
                SolicitudNegocio nSolicitud = new SolicitudNegocio();
                E_RESULTADO vResultado = nSolicitud.InsertaActualizaSolicitud(vXmlRespuesta.Element("PLANTILLA"), vIdSolicitudVS, vLstArchivos, vLstDocumentos, vClUsuario, vNbPrograma, vTipoTransaccion);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                var idSolicitud = 0;
                bool esNumero;

                if (vResultado.MENSAJE.Where(x => x.CL_IDIOMA == "ID_SOLICITUD").FirstOrDefault() != null)
                {
                    esNumero = int.TryParse(vResultado.MENSAJE.Where(x => x.CL_IDIOMA == "ID_SOLICITUD").FirstOrDefault().DS_MENSAJE, out idSolicitud);
                    vIdSolicitudVS = idSolicitud;
                    vTipoTransaccion = E_TIPO_OPERACION_DB.A.ToString();
                }

                if (pFgCerrar)
                {
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR);
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

        public Control ObtenerControl(Control pCtrlContenedor, string pNbControl)
        {
            string vIdClientControl = String.Empty;
            Control vControl = pCtrlContenedor.FindControl(pNbControl);
            return vControl;
        }

        private void AsignarAjax()
        {
            foreach (XElement vXmlContenedor in XElement.Parse(vXmlSolicitud).Element("CONTENEDORES").Elements("CONTENEDOR"))
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
                                        Control vControlFormulario = ObtenerControl(vPageView, vIdCampoFormulario);

                                        ramInventario.AjaxSettings.AddAjaxSetting(vBotonAgregar, vControlFormulario, ralpInventario, UpdatePanelRenderMode.Inline);
                                        ramInventario.AjaxSettings.AddAjaxSetting(vBotonCancelar, vControlFormulario, ralpInventario, UpdatePanelRenderMode.Inline);
                                        ramInventario.AjaxSettings.AddAjaxSetting(vBotonEditar, vControlFormulario, ralpInventario, UpdatePanelRenderMode.Inline);

                                        if (UtilXML.ValorAtributo<string>(vCtrlFormulario.Attribute("CL_TIPO")).Equals("COMBOBOX"))
                                        {

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

                                        if (UtilXML.ValorAtributo<string>(vCtrlFormulario.Attribute("CL_TIPO")).Equals("DATEAGE"))
                                        {
                                            Control txtFormularioEdad = ObtenerControl(vPageView, String.Format("txt{0}", vIdCampoFormulario));
                                            ramInventario.AjaxSettings.AddAjaxSetting(vControlFormulario, txtFormularioEdad, ralpInventario, UpdatePanelRenderMode.Inline);
                                            ramInventario.AjaxSettings.AddAjaxSetting(vBotonCancelar, txtFormularioEdad, ralpInventario, UpdatePanelRenderMode.Inline);
                                            ramInventario.AjaxSettings.AddAjaxSetting(vBotonAgregar, txtFormularioEdad, ralpInventario, UpdatePanelRenderMode.Inline);
                                            ramInventario.AjaxSettings.AddAjaxSetting(vBotonEditar, txtFormularioEdad, ralpInventario, UpdatePanelRenderMode.Inline);
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
            string vClModulo = "INTEGRACION";
            string vModulo = Request.QueryString["m"];
            if (vModulo != null)
                vClModulo = vModulo;
            vFgAceptaTermino = true;
            

            cssModulo = Utileria.ObtenerCssModulo(vClModulo);

            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = (ContextoUsuario.oUsuario != null) ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO SOLICITUD";

            if (vClUsuario != "INVITADO SOLICITUD")
            {
                btnImpresion2.Visible = true;
            }

            if (!Page.IsPostBack)
            {
                vIdSolicitudVS = vIdSolicitud;
                vXmlSolicitudPlantilla = vXmlSolicitud;
                vIdItemFotografia = vIdItemFoto;
                CargarDocumentos();
                vPlantilla.CargarGrid();
                MostrarPrivacidad = ContextoApp.IDP.MensajePrivacidad.fgVisible;
                lbAyuda.InnerHtml = ContextoApp.IDP.MensajeBienvenidaEmpleo.dsMensaje;
                lbAvisoPrivacidad.InnerHtml = ContextoApp.IDP.MensajePiePagina.dsMensaje;
                rspAvisoDePrivacidad.Visible = ContextoApp.IDP.MensajePiePagina.fgVisible;
                if (ContextoApp.IDP.MensajePiePagina.fgVisible)
                {
                   
                }

                //vFgAceptaTermino = false;
              //  vIsPostBack = true;
            }
            AsignarAjax();
            vPlantilla.xmlPlantilla = vXmlSolicitudPlantilla;
            vClRutaArchivosTemporales = Server.MapPath(ContextoApp.ClRutaArchivosTemporales);
            
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                vIdSolicitud = null;
                int vIdSolicitudQS = -1;
                if (int.TryParse(Request.QueryString["SolicitudId"], out vIdSolicitudQS))
                    vIdSolicitud = vIdSolicitudQS;

                if (Request.QueryString["FG_HABILITADO"] != null)
                {
                    if (Request.QueryString["FG_HABILITADO"] == "False")
                    {
                        btnGuardar.Enabled = false;
                        btnGuardarSalir.Enabled = false;
                    }
                }

                if (Request.QueryString["PlantillaId"] != null)
                {
                    vIdPlantillaFormulario = (int.Parse(Request.QueryString["PlantillaId"]));
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

            SolicitudNegocio nSolicitud = new SolicitudNegocio();
            SPE_OBTIENE_SOLICITUD_PLANTILLA_Result vSolicitud = new SPE_OBTIENE_SOLICITUD_PLANTILLA_Result();

            if (vIdPlantillaFormulario != null)
            {
                vSolicitud = nSolicitud.ObtenerSolicitudPlantilla(vIdPlantillaFormulario, null, null);
            }
            else
            {
                vSolicitud = nSolicitud.ObtenerSolicitudPlantilla(null, vIdSolicitud, null);
            }
            vXmlSolicitud = vSolicitud.XML_SOLICITUD_PLANTILLA;

            vPlantilla = new Plantilla()
            {
                ctrlPlantilla = new Contenedor() { NbContenedor = "PlantillaSolicitud", CtrlContenedor = mpgSolicitud },
                lstContenedores = ObtenerContenedores(),
                xmlPlantilla = vXmlSolicitud
            };

            vXmlDocumentos = vSolicitud.XML_VALORES;
            vIdItemFoto = vSolicitud.ID_ITEM_FOTOGRAFIA;

            if (vSolicitud.FI_FOTOGRAFIA != null)
            {
                rbiFotoEmpleado.DataValue = vSolicitud.FI_FOTOGRAFIA;
                btnEliminarFotoEmpleado.Visible = true;
            }
            else
            {
                btnEliminarFotoEmpleado.Visible = false;
            }

            vPlantilla.CrearFormulario(!Page.IsPostBack);
        
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar(false);
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

        protected void btnAgregarDocumento_Click(object sender, EventArgs e)
        {
            AddDocumento(cmbTipoDocumento.SelectedValue, rauDocumento);
            grdDocumentos.Rebind();
        }

        protected void grdDocumentos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdDocumentos.DataSource = vLstDocumentos;
        }

        protected void btnDelDocumentos_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem i in grdDocumentos.SelectedItems)
                EliminarDocumento(i.GetDataKeyValue("ID_ITEM").ToString());
        }

        protected void btnGuardarSalir_Click(object sender, EventArgs e)
        {
            Guardar(true);
        }

        //protected void ramInventario_AjaxRequest(object sender, AjaxRequestEventArgs e)
        //{
        //    vFgAceptaTermino = true;
        //}
    }
}