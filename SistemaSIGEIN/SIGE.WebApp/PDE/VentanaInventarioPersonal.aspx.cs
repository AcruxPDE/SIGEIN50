using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.PDE
{
    public partial class VentanaInventarioPersonal : System.Web.UI.Page
    {

        public string vXmlEmpleadoPlantilla
        {
            get { return (string)ViewState["vs_vXmlEmpleadoPlantilla"]; }
            set { ViewState["vs_vXmlEmpleadoPlantilla"] = value; }
        }

        string  vIdEmpleadoVS
        {
            get { return (string )ViewState["vs_vIdEmpleadoVS"]; }
            set { ViewState["vs_vIdEmpleadoVS"] = value; }
        }

        public int vIdCambioVS
        {
            get { return (int)ViewState["vs_vIdCambioVS"]; }
            set { ViewState["vs_vIdCambioVS"] = value; }
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
        string vCambio
        {
            get { return (string)ViewState["vs_vCambio"]; }
            set { ViewState["vs_vCambio"] = value; }
        }
        public string vCambioVS
        {
            get { return (string)ViewState["vs_vCambio"]; }
            set { ViewState["vs_vCambio"] = value; }
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

        public string vsTipoConsulta
        {
            get { return (string)ViewState["vs_vsTipoConsulta"]; }
            set { ViewState["vs_vsTipoConsulta"] = value; }
        }
        public int vsIdCambio
        {
            get { return (int)ViewState["vs_vsIdCambio"]; }
            set { ViewState["vs_vsIdCambio"] = value; }
        }
        public string  vIdEmpleado
        {
            get { return (string)ViewState["vs_vsIdEmpleado"]; }
            set { ViewState["vs_vsIdEmpleado"] = value; }
        }
  
        Plantilla vPlantilla;
        string vXmlPlantilla;
        string vClUsuario;
        string vNbPrograma;
        string vXmlDocumentos;
        Guid? vIdItemFoto;
        string vTipoConsulta = String.Empty;
        string vClRutaArchivosTemporales;

        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        public string vBotonesVisibles
        {
            set { ViewState["vs_vrn_BotonesVisibles"] = value; }
            get { return (string)ViewState["vs_vrn_BotonesVisibles"]; }
        }
         public string  url
        {
            get { return (string)ViewState["vs_vurl"]; }
            set { ViewState["vs_vurl"] = value; }
        }
       
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!Page.IsPostBack)
            {
                vIdEmpleadoVS = vIdEmpleado;
                vIdCambioVS = vsIdCambio;
                vCambioVS = vCambio;
                vXmlEmpleadoPlantilla = vXmlPlantilla;
                vIdItemFotografia = vIdItemFoto;
                CargarDocumentos();
                vPlantilla.CargarGrid();

            }

            //HttpCookie myCookie = new HttpCookie("BotonesVisibles");
            //myCookie = Request.Cookies["BotonesVisibles"];
            //string valor = myCookie.Value;
            //if (valor == "falso")
            //{
            //    btnActualizarFotoEmpleado.Enabled = false;
            //}
            vPlantilla.xmlPlantilla = vXmlEmpleadoPlantilla;
            vClRutaArchivosTemporales = Server.MapPath(ContextoApp.ClRutaArchivosTemporales);
            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            vTipoConsulta = Request.QueryString["Tipo"];
            vBotonesVisibles = (Request.Params["pBotonesVisbles"]);

            vsTipoConsulta = vTipoConsulta;
            if (!Page.IsPostBack)
            {

                if (Request.Params["pIdEmpleado"] != null)
                {
                    vIdEmpleado = Request.Params["pIdEmpleado"];
                    vsIdCambio = int.Parse(Request.Params["IdCambio"]);
                    vCambio = Request.Params["Cambio"];
                }
                else
                {
                    vIdEmpleado = (ContextoUsuario.oUsuario.ID_EMPLEADO_PDE != "0") ? ContextoUsuario.oUsuario.ID_EMPLEADO_PDE : "-1";
                    vsIdCambio = 0;
                    vCambio = "false";
                }

                if (vsTipoConsulta == "e")
                {
                    btnGuardar.Visible = true;
                }
            }


            //Cuando el parametro "vTipoConsulta" sea vacio o =v todos los campos van con el FG_EDITABLE = 0
            //quitar el boton guardar

            if (vsTipoConsulta != "e")
            {
                btnGuardar.Visible = false;
                btnEliminarFotoEmpleado.Visible = false;
                btnActualizarFotoEmpleado.Visible = false;
                btnEliminarFotoEmpleado.Visible = false;
                rauFotoEmpleado.Visible = false;
                btnAgregarDocumento.Visible = false;
                btnEliminarFotoEmpleado.Visible = false;
            }
            
            EmpleadoNegocio nEmpleado = new EmpleadoNegocio();
      
            {
                SPE_OBTIENE_EMPLEADO_PLANTILLA_PDE_Result vSolicitud = nEmpleado.ObtenerPlantillaPDE(null, vIdEmpleado, "INVENTARIO_PDE");
                vXmlPlantilla = vSolicitud.XML_SOLICITUD_PLANTILLA;
                vXmlDocumentos = vSolicitud.XML_VALORES;
                vIdItemFoto = vSolicitud.ID_ITEM_FOTOGRAFIA;

                if (vSolicitud.FI_FOTOGRAFIA != null)
                {
                    rbiFotoEmpleado.DataValue = vSolicitud.FI_FOTOGRAFIA;
                    btnEliminarFotoEmpleado.Visible = true;
                }


                vXmlPlantilla = vSolicitud.XML_SOLICITUD_PLANTILLA;
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

                vPlantilla = new Plantilla()
                {
                    ctrlPlantilla = new Contenedor() { NbContenedor = "PlantillaEmpleado", CtrlContenedor = mpgPlantilla },
                    lstContenedores = ObtenerContenedores(),
                    xmlPlantilla = vXmlPlantilla
                };

                vPlantilla.CrearFormulario(!Page.IsPostBack);

            }
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

        protected void btnGuardar_Click(object sender, EventArgs e)
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

                vXmlEmpleadoPlantilla = vXmlRespuesta.Element("PLANTILLA").ToString();
                EmpleadoNegocio nEmpleado = new EmpleadoNegocio();
                E_RESULTADO vResultado = nEmpleado.InsertaActualizaEmpleadoPDE(vXmlRespuesta.Element("PLANTILLA"), vIdEmpleadoVS, vLstArchivos, vLstDocumentos, vClUsuario, vNbPrograma, vXmlRespuesta);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR);
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

        protected void grdDocumentos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdDocumentos.DataSource = vLstDocumentos;//.Where(w => !w.CL_TIPO_DOCUMENTO.Equals("FOTOGRAFIA"));
        }

        protected void btnAddDocumento_Click(object sender, EventArgs e)
        {
            AddDocumento(cmbTipoDocumento.SelectedValue, rauDocumento);
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
                        bitmapImage.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        vFiLogotipo = Convert.ToBase64String(stream.ToArray());
                        vNbLogotipo = rauFotoEmpleado.UploadedFiles[0].GetName();
                        rbiFotoEmpleado.DataValue = stream.ToArray();
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


    }
}