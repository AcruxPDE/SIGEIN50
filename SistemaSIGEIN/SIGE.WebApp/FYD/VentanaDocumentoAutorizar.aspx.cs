using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using Stimulsoft.Base.Json;
using Telerik.Web.UI;

namespace SIGE.WebApp.FYD
{
    public partial class DocumetoAutorizar : System.Web.UI.Page
    {
        #region Variables

        E_DOCUMENTO_AUTORIZAR vDocumento
        {
            get { return (E_DOCUMENTO_AUTORIZAR)ViewState["vs_vDocumento"]; }
            set { ViewState["vs_vDocumento"] = value; }
        }

        E_NOTA pNota
        {
            get { return (E_NOTA)ViewState["vs_pNota"]; }
            set { ViewState["vs_pNota"] = value; }
        }

        E_PROGRAMA vPrograma
        {
            get { return (E_PROGRAMA)ViewState["vs_vPrograma"]; }
            set { ViewState["vs_vPrograma"] = value; }
        }

        E_PERIODO_EVALUACION vPeriodo
        {
            get { return (E_PERIODO_EVALUACION)ViewState["vs_vPeriodo"]; }
            set { ViewState["vs_vPeriodo"] = value; }
        }

        List<E_DOCUMENTO_AUTORIZA_EMPLEADO> vDocumentosAutorizar
        {
            get { return (List<E_DOCUMENTO_AUTORIZA_EMPLEADO>)ViewState["vs_vDocumentosAutorizar"]; }
            set { ViewState["vs_vDocumentosAutorizar"] = value; }
        }

        int? vIdDocumento
        {
            get { return (int?)ViewState["vs_vID_DOCUMENTO"]; }
            set { ViewState["vs_vID_DOCUMENTO"] = value; }
        }

        int? vIdPrograma
        {
            get { return (int?)ViewState["vs_vID_PROGRAMA"]; }
            set { ViewState["vs_vID_PROGRAMA"] = value; }
        }

        private string ptipo
        {
            get { return (string)ViewState["vstipo"]; }
            set { ViewState["vstipo"] = value; }
        }

        private int? vIdPeriodo
        {
            get { return (int?)ViewState["vs_vda_id_periodo"]; }
            set { ViewState["vs_vda_id_periodo"] = value; }
        }

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        #endregion

        #region Funciones

        public E_DOCUMENTO_AUTORIZAR parseDocumentoAutorizar(SPE_OBTIENE_C_DOCUMENTO_AUTORIZACION_Result pDocumento)
        {
            return new E_DOCUMENTO_AUTORIZAR
             {
                 ID_DOCUMENTO_AUTORIZACION = pDocumento.ID_DOCUMENTO_AUTORIZACION,
                 CL_DOCUMENTO = pDocumento.CL_DOCUMENTO,
                 NB_DOCUMENTO = pDocumento.NB_DOCUMENTO,
                 CL_TIPO_DOCUMENTO = pDocumento.CL_TIPO_DOCUMENTO,
                 DS_NOTAS = pDocumento.DS_NOTAS,
                 VERSION = pDocumento.VERSION,
                 FE_ELABORACION = pDocumento.FE_ELABORACION,
                 FE_REVISION = pDocumento.FE_REVISION,
                 NB_EMPLEADO_ELABORA = pDocumento.NB_EMPLEADO_ELABORA
             };
        }

        public void DeserializarDocumentoAutorizar(XElement pXmlTablas)
        {
            if (ValidarRamaXml(pXmlTablas, "PROGRAMAS"))
            {
                vPrograma = pXmlTablas.Element("PROGRAMAS").Elements("PROGRAMA").Select(el => new E_PROGRAMA
                {
                    ID_PROGRAMA = UtilXML.ValorAtributo<int>(el.Attribute("ID_PROGRAMA")),
                    ID_PERIODO = UtilXML.ValorAtributo<int>(el.Attribute("ID_PERIODO")),
                    CL_PROGRAMA = el.Attribute("CL_PROGRAMA").Value,
                    NB_PROGRAMA = el.Attribute("NB_PROGRAMA").Value,
                    CL_ESTADO = el.Attribute("CL_ESTADO").Value,
                    CL_TIPO_PROGRAMA = el.Attribute("CL_TIPO_PROGRAMA").Value,
                    VERSION = (el.Attribute("CL_VERSION") != null) ? el.Attribute("CL_VERSION").Value : "",
                    DS_NOTAS = (el.Attribute("DS_NOTAS") != null) ? el.Attribute("DS_NOTAS").Value : "",
                    ID_DOCUMENTO_AUTORIZACION = (el.Attribute("ID_DOCUMENTO_AUTORIZACION") != null) ? UtilXML.ValorAtributo<int>(el.Attribute("ID_DOCUMENTO_AUTORIZACION")) : 0,
                    CL_DOCUMENTO = "",
                    NO_COMPETENCIAS = 0,
                    NO_PARTICIPANTES = 0,
                    FE_CREACION = DateTime.Now
                }).FirstOrDefault();
            }

            if (ValidarRamaXml(pXmlTablas, "PERIODOS"))
            {

                vPeriodo = pXmlTablas.Element("PERIODOS").Elements("PERIODO").Select(t => new E_PERIODO_EVALUACION
                {
                    ID_PERIODO = UtilXML.ValorAtributo<int>(t.Attribute("ID_PERIODO")),
                    CL_PERIODO = t.Attribute("CL_PERIODO").Value,
                    NB_PERIODO = t.Attribute("NB_PERIODO").Value,
                    CL_ESTADO = t.Attribute("CL_ESTADO_PERIODO").Value,
                    ID_DOCUMENTO_AUTORIZACION = UtilXML.ValorAtributo<int>(t.Attribute("ID_DOCUMENTO_AUTORIZACION"))
                }).FirstOrDefault();

            }


            if (ValidarRamaXml(pXmlTablas, "DOCUMENTO"))
            {
                vDocumento = pXmlTablas.Element("DOCUMENTO").Elements("DOCUMENTO_AUTORIZAR").Select(el => new E_DOCUMENTO_AUTORIZAR
                {
                    ID_DOCUMENTO_AUTORIZACION = (int)UtilXML.ValorAtributo(el.Attribute("ID_DOCUMENTO_AUTORIZACION"), E_TIPO_DATO.INT),
                    CL_DOCUMENTO = el.Attribute("CL_DOCUMENTO").Value,
                    NB_DOCUMENTO = el.Attribute("NB_DOCUMENTO").Value,
                    CL_TIPO_DOCUMENTO = el.Attribute("CL_TIPO_DOCUMENTO").Value,
                    DS_NOTAS = (el.Attribute("DS_NOTAS") != null) ? el.Attribute("DS_NOTAS").Value : "",
                    VERSION = el.Attribute("VERSION").Value,
                    FE_ELABORACION = (DateTime?)UtilXML.ValorAtributo(el.Attribute("FE_ELABORACION"), E_TIPO_DATO.DATETIME),
                    FE_REVISION = (DateTime?)UtilXML.ValorAtributo(el.Attribute("FE_REVISION"), E_TIPO_DATO.DATETIME),
                    NB_EMPLEADO_ELABORA = el.Attribute("NB_EMPLEADO_ELABORA").Value
                }).FirstOrDefault();

            }


            if (ValidarRamaXml(pXmlTablas, "DOCUMENTO_EMPLEADO"))
            {
                vDocumentosAutorizar = pXmlTablas.Element("DOCUMENTO_EMPLEADO").Elements("DOCUMENTO_EMPLEADO").Select(el => new E_DOCUMENTO_AUTORIZA_EMPLEADO
                {
                    ID_AUTORIZACION = (int)UtilXML.ValorAtributo(el.Attribute("ID_AUTORIZACION"), E_TIPO_DATO.INT),
                    FL_AUTORIZACION = Guid.Parse(el.Attribute("FL_AUTORIZACION").Value),
                    CL_TOKEN = (el.Attribute("CL_TOKEN") != null) ? el.Attribute("CL_TOKEN").Value : "",
                    ID_EMPLEADO = (int)UtilXML.ValorAtributo(el.Attribute("ID_EMPLEADO"), E_TIPO_DATO.INT),
                    NB_EMPLEADO = el.Attribute("NB_EMPLEADO").Value,
                    ID_DOCUMENTO = (int)UtilXML.ValorAtributo(el.Attribute("ID_DOCUMENTO"), E_TIPO_DATO.INT),
                    CL_ESTADO = el.Attribute("CL_ESTADO").Value,
                    DS_OBSERVACIONES = (el.Attribute("DS_NOTAS") != null) ? validarDsNotas(el.Attribute("DS_NOTAS").Value) : "",
                    FE_AUTORIZACION = (DateTime?)UtilXML.ValorAtributo(el.Attribute("FE_AUTORIZACION"), E_TIPO_DATO.DATETIME),
                    CL_EMPLEADO = el.Attribute("CL_EMPLEADO").Value,
                    NB_EMPLEADO_PUESTO = el.Attribute("NB_EMPLEADO_COMPLETO").Value,
                    ID_PUESTO = (int)UtilXML.ValorAtributo(el.Attribute("ID_PUESTO"), E_TIPO_DATO.INT),
                    CL_CORREO_ELECTRONICO = el.Attribute("CL_CORREO_ELECTRONICO").Value,
                    CL_PUESTO = el.Attribute("CL_PUESTO").Value,
                    NB_PUESTO = el.Attribute("NB_PUESTO").Value,
                    CL_DOCUMENTO = el.Attribute("CL_DOCUMENTO").Value,
                    NB_DOCUMENTO = el.Attribute("NB_DOCUMENTO").Value,
                    CL_TIPO_DOCUMENTO = el.Attribute("CL_TIPO_DOCUMENTO").Value,
                    DS_NOTAS = "",
                    VERSION = el.Attribute("VERSION").Value,
                    FE_ELABORACION = (DateTime?)UtilXML.ValorAtributo(el.Attribute("FE_ELABORACION"), E_TIPO_DATO.DATETIME),
                    FE_REVISION = (DateTime?)UtilXML.ValorAtributo(el.Attribute("FE_REVISION"), E_TIPO_DATO.INT),
                    NB_EMPLEADO_ELABORA = el.Attribute("NB_EMPLEADO_ELABORA").Value
                }).ToList();

                grdDocumentosAutorizar.DataSource = vDocumentosAutorizar;
            }
            else
            {
                vDocumentosAutorizar = new List<E_DOCUMENTO_AUTORIZA_EMPLEADO>();
                grdDocumentosAutorizar.DataSource = vDocumentosAutorizar;
            }
        }

        public string validarDsNotas(string vdsNotas)
        {
            if (!vdsNotas.Equals(""))
            {
                XElement vNotas = XElement.Parse(vdsNotas.ToString());
                if (ValidarRamaXml(vNotas, "NOTA"))
                {
                    pNota = vNotas.Elements("NOTA").Select(el => new E_NOTA
                    {
                        DS_NOTA = el.Attribute("DS_NOTA").Value,
                        FE_NOTA = (DateTime?)UtilXML.ValorAtributo(el.Attribute("FE_NOTA"), E_TIPO_DATO.DATETIME),

                    }).FirstOrDefault();
                }
                return pNota.DS_NOTA.ToString();
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

        public E_RESULTADO GuardarEmpleadosAutorizar()
        {
            DocumentoAutorizarNegocio nDocumentoAutorizar = new DocumentoAutorizarNegocio();
            if (ptipo.Equals("Agregar") && vDocumento == null)
            {
                vDocumento = new E_DOCUMENTO_AUTORIZAR
                {
                    ID_DOCUMENTO_AUTORIZACION = 0,
                    CL_DOCUMENTO = txtClDocumento.Text,
                    NB_DOCUMENTO = "",
                    CL_TIPO_DOCUMENTO = "",
                    DS_NOTAS = "",
                    VERSION = txtVersionDocumento.Text,
                    FE_ELABORACION = rdpFeElaboracion.SelectedDate,
                    FE_REVISION = rdpFechaRevision.SelectedDate,
                    NB_EMPLEADO_ELABORA = txtElaboro.Text
                };
            }

            int contador = 0;
            foreach (GridDataItem item in grdDocumentosAutorizar.MasterTableView.Items)
            {
                RadTextBox vClCorreoElectronico = (RadTextBox)item.FindControl("txtClCorreoElectronico");
                vDocumentosAutorizar.ElementAt(contador).CL_CORREO_ELECTRONICO = vClCorreoElectronico.Text;
                contador++;
            }

            var vXelementDocumento =
              new XElement("DOCUMENTO",
              new XAttribute("ID_DOCUMENTO_AUTORIZACION", vDocumento.ID_DOCUMENTO_AUTORIZACION.ToString()),
              new XAttribute("CL_DOCUMENTO", (ptipo.Equals("Agregar")) ? vDocumento.CL_DOCUMENTO.ToString() : txtClDocumento.Text.ToString()),
              new XAttribute("NB_DOCUMENTO", vDocumento.NB_DOCUMENTO.ToString()),
              new XAttribute("CL_TIPO_DOCUMENTO", vDocumento.CL_TIPO_DOCUMENTO.ToString()),
              new XAttribute("DS_NOTAS", vDocumento.DS_NOTAS.ToString()),
              new XAttribute("VERSION", (ptipo.Equals("Agregar")) ? vDocumento.VERSION.ToString() : txtVersionDocumento.Text),
              new XAttribute("FE_ELABORACION", (ptipo.Equals("Agregar")) ? vDocumento.FE_ELABORACION.ToString() : rdpFeElaboracion.SelectedDate.ToString()),
              new XAttribute("FE_REVISION", (ptipo.Equals("Agregar")) ? vDocumento.FE_REVISION.ToString() : (rdpFechaRevision.SelectedDate != null) ? rdpFechaRevision.SelectedDate.ToString() : ""),
              new XAttribute("NB_EMPLEADO_ELABORA", (ptipo.Equals("Agregar")) ? vDocumento.NB_EMPLEADO_ELABORA.ToString() : txtElaboro.Text.ToString()));

            var vXelementEmpleadosAutorizar = vDocumentosAutorizar.Select(x =>
                                     new XElement("EMPLEADO_AUTORIZAR",
                                     new XAttribute("ID_AUTORIZACION", x.ID_AUTORIZACION.ToString()),
                                     new XAttribute("FL_AUTORIZACION", x.FL_AUTORIZACION.ToString()),
                                     new XAttribute("CL_TOKEN", x.CL_TOKEN.ToString()),
                                     new XAttribute("ID_EMPLEADO", x.ID_EMPLEADO.ToString()),
                                     new XAttribute("NB_EMPLEADO", x.NB_EMPLEADO.ToString()),
                                     new XAttribute("ID_DOCUMENTO", x.ID_DOCUMENTO.ToString()),
                                     new XAttribute("CL_ESTADO", x.CL_ESTADO.ToString()),
                                     new XAttribute("DS_OBSERVACIONES", ConvertirNota(x.DS_OBSERVACIONES.ToString())),
                                     new XAttribute("FE_AUTORIZACION", x.FE_AUTORIZACION.ToString())));

            XElement programaCapacitacion =
                new XElement("CONFIGURACION",
                new XElement("DOCUMENTOS", vXelementDocumento),
                new XElement("EMPLEADOS_AUTORIZAR", vXelementEmpleadosAutorizar));

            E_RESULTADO vResultado = nDocumentoAutorizar.InsertaActualizaDocumentoAutorizacion(vIdDocumento, vIdPeriodo, vIdPrograma, programaCapacitacion.ToString(), vClUsuario, vNbPrograma);
            return vResultado;
        }

        protected void CargarDatosEmpleado(List<int> vSeleccionados)
        {
            EmpleadoNegocio nEmpleado = new EmpleadoNegocio();
            var empleados = nEmpleado.ObtenerEmpleados();
            int contador = 1;
            foreach (int idEmpleado in vSeleccionados)
            {
                var empleado = empleados.Where(x => x.M_EMPLEADO_ID_EMPLEADO == idEmpleado).FirstOrDefault();

                var result = vDocumentosAutorizar.FirstOrDefault(x => x.ID_EMPLEADO == empleado.M_EMPLEADO_ID_EMPLEADO);
                if (result == null)
                {
                    if (empleado != null)
                    {
                        vDocumentosAutorizar.Add(new E_DOCUMENTO_AUTORIZA_EMPLEADO
                        {
                            ID_AUTORIZACION = contador++,
                            FL_AUTORIZACION = Guid.NewGuid(),
                            CL_TOKEN = quitarCararcteresNoAlfanumericos(Membership.GeneratePassword(8, 0)),
                            ID_EMPLEADO = empleado.M_EMPLEADO_ID_EMPLEADO,
                            NB_EMPLEADO = empleado.M_EMPLEADO_NB_EMPLEADO_COMPLETO,
                            ID_DOCUMENTO = 0,
                            CL_ESTADO = "Por autorizar",
                            DS_OBSERVACIONES = "",
                            FE_AUTORIZACION = null,
                            CL_EMPLEADO = empleado.M_EMPLEADO_CL_EMPLEADO,
                            NB_EMPLEADO_PUESTO = empleado.M_PUESTO_NB_PUESTO,
                            ID_PUESTO = 0,
                            CL_CORREO_ELECTRONICO = empleado.M_EMPLEADO_CL_CORREO_ELECTRONICO,
                            CL_PUESTO = empleado.M_PUESTO_CL_PUESTO,
                            NB_PUESTO = empleado.M_PUESTO_NB_PUESTO,
                            CL_DOCUMENTO = "",
                            NB_DOCUMENTO = "",
                            CL_TIPO_DOCUMENTO = "",
                            DS_NOTAS = "",
                            VERSION = "",
                            FE_ELABORACION = DateTime.Now,
                            FE_REVISION = null,
                            NB_EMPLEADO_ELABORA = ContextoUsuario.oUsuario.NB_USUARIO,
                            //ID_PROGRAMA = vPrograma.ID_PROGRAMA,
                            //CL_PROGRAMA = vPrograma.CL_PROGRAMA,
                            //NB_PROGRAMA = vPrograma.NB_PROGRAMA
                        });
                    }
                }
            }
            grdDocumentosAutorizar.DataSource = vDocumentosAutorizar;
            grdDocumentosAutorizar.Rebind();
        }

        public string quitarCararcteresNoAlfanumericos(string newPassword)
        {
            String vPassword = "";
            Random rnd = new Random();
            vPassword = Regex.Replace(newPassword, @"[^a-zA-Z0-9]", m => rnd.Next(1, 10) + "");
            return vPassword;
        }

        private void CargarDatos()
        {
            DocumentoAutorizarNegocio nDocumento = new DocumentoAutorizarNegocio();
            ProgramaNegocio nPrograma = new ProgramaNegocio();

            XElement Documento = nDocumento.ObtieneTablasDocumentoAutorizacion(vIdPrograma, vIdPeriodo);

            if (Documento != null)
            {
                DeserializarDocumentoAutorizar(Documento);
                ptipo = "Editar";

                if (vDocumento != null)
                {
                    if (vPrograma != null)
                    {
                        vIdDocumento = vPrograma.ID_DOCUMENTO_AUTORIZACION;
                        if (vPrograma.CL_ESTADO.Equals("Terminado"))
                        {
                            btnEnviar.Enabled = false;
                            btnEliminar.Enabled = false;
                            btnSeleccionar.Enabled = false;
                            btnAceptar.Enabled = false;
                        }
                    }

                    if (vPeriodo != null)
                    {
                        vIdDocumento = vPeriodo.ID_DOCUMENTO_AUTORIZACION;
                    }

                    txtClDocumento.Text = vDocumento.CL_DOCUMENTO.ToString();
                    txtElaboro.Text = vDocumento.NB_EMPLEADO_ELABORA.ToString();
                    txtVersionDocumento.Text = vDocumento.VERSION.ToString();
                    rdpFechaRevision.SelectedDate = vDocumento.FE_REVISION;
                    rdpFeElaboracion.Enabled = false;
                    rdpFeElaboracion.SelectedDate = vDocumento.FE_ELABORACION;

                  


                }
                else
                {
                    ptipo = "Agregar";
                    txtElaboro.Text = ContextoUsuario.oUsuario.NB_USUARIO.ToString();
                }
            }

        }

        private string ConvertirNota(string pDsNota)
        {
            XElement vXelementNotas = null;

            var vXelementNota =
            new XElement("NOTA",
            new XAttribute("FE_NOTA", DateTime.Now.ToString()),
            new XAttribute("DS_NOTA", pDsNota));

            vXelementNotas = new XElement("NOTAS", vXelementNota);

            return vXelementNotas.ToString();
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                vDocumentosAutorizar = new List<E_DOCUMENTO_AUTORIZA_EMPLEADO>();
                txtElaboro.Text = ContextoUsuario.oUsuario.NB_USUARIO.ToString();

                if (Request.Params["IdPeriodo"] != null)
                {
                    vIdPeriodo = int.Parse(Request.Params["IdPeriodo"].ToString());
                    vIdPrograma = null;
                }

                if (Request.Params["IdPrograma"] != null)
                {
                    vIdPrograma = int.Parse(Request.Params["IdPrograma"].ToString());
                    vIdPeriodo = null;
                }

                if (Request.Params["FGCONTESTADO"] != null)
                {
                    string vFgCuestContestado = Request.Params["FGCONTESTADO"].ToString();
                    if (vFgCuestContestado == "SI")
                    {
                        btnEnviar.Enabled = false;
                        btnSeleccionar.Enabled = false;
                        btnEliminar.Enabled = false;
                        btnAceptar.Enabled = false;
                    }
                }

                CargarDatos();
            }
        }

        protected void ramProgramasCapacitacion_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            String json = e.Argument;
            String[] stringArray = json.Split("_".ToCharArray());
            List<int> listaSeleccionados = JsonConvert.DeserializeObject<List<int>>(stringArray[0]);
            if (stringArray[1] == "EMPLEADO")
            {
                CargarDatosEmpleado(listaSeleccionados);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grdDocumentosAutorizar.SelectedItems.Count; i++)
            {
                GridDataItem item = grdDocumentosAutorizar.SelectedItems[i] as GridDataItem;
                int idAutorizacion = int.Parse(item.GetDataKeyValue("ID_AUTORIZACION").ToString());
                var vCompetenciaEliminada = vDocumentosAutorizar.Where(x => x.ID_AUTORIZACION == idAutorizacion).FirstOrDefault();
                vDocumentosAutorizar.Remove(vCompetenciaEliminada);
            }

            string vMensaje = "Proceso exitoso";
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, E_TIPO_RESPUESTA_DB.SUCCESSFUL, 400, 150, "");
            grdDocumentosAutorizar.DataSource = vDocumentosAutorizar;
            grdDocumentosAutorizar.Rebind();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            E_RESULTADO vResultado = GuardarEmpleadosAutorizar();
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            E_RESULTADO vResultado = GuardarEmpleadosAutorizar();

            if (vResultado.CL_TIPO_ERROR.ToString().Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL.ToString()))
            {
                string vClEstadoMail = "SendMail";
                //int contador = 0;
                string vTitulo;

                if (vPrograma != null)
                {
                    vTitulo = "Solicitud de autorización de un programa de capacitación";
                }
                else
                {
                    vTitulo = "Solicitud de autorización de período de evaluación";
                }


                foreach (GridItem item in grdDocumentosAutorizar.SelectedItems)
                {

                    GridDataItem vDataItem = item as GridDataItem;

                    int idAutorizacion = int.Parse(vDataItem.GetDataKeyValue("ID_AUTORIZACION").ToString());
                    //RadTextBox vClCorreoElectronico = (RadTextBox)vDataItem.FindControl("txtClCorreoElectronico");

                    var vDocumento = vDocumentosAutorizar.Where(x => x.ID_AUTORIZACION == idAutorizacion).FirstOrDefault();
                    //vDocumento.CL_CORREO_ELECTRONICO = vClCorreoElectronico.Text;

                    Mail mail = new Mail(ContextoApp.mailConfiguration);

                    String vCorreoElectronico = (vDataItem.FindControl("txtClCorreoElectronico") as RadTextBox).Text;
                    //String vNbEmpleado = documentoMail.NB_EMPLEADO;
                    //Guid vFlAutorizacion = documentoMail.FL_AUTORIZACION;

                    //string vClToken = documentoMail.CL_TOKEN.ToString();
                    mail.addToAddress(vCorreoElectronico, vDocumento.NB_EMPLEADO);
                    //contador++;
                    string myUrl = ResolveUrl("~/Logon.aspx?AUTORIZA=PROGRAMACAPACITACION&TOKEN=");
                    string vUrl = ContextoUsuario.nbHost + myUrl + vDocumento.FL_AUTORIZACION.ToString();
                    string vResultadoEnvio = "";

                    try
                    {
                        switch (vClEstadoMail)
                        {
                            case "SendMail":
                                vResultadoEnvio = mail.Send(vTitulo, String.Format(" <html>" +
                                        " <head>" +
                                        " <title>Solicitud</title>" +
                                        " <meta charset=\"utf-8\"> " +
                                        " </head>" +
                                        " <body>" +
                                        " <p>Has recibido una solicitud de autorización de documentos. En caso de que decidas no autorizarlo anota las razones de esta decisión. </p>" +
                                        " <br>" +
                                        " <a href=\"{1}\" >Haga click aquí para ver el documento </a>" +
                                        " <br>" +
                                        " <p>Tu contraseña de acceso es <b>{0}</b> . ¡Gracias por tu apoyo! </p>" +
                                        " </body>" +
                                        " </html>",
                                        vDocumento.CL_TOKEN.ToString(),
                                        vUrl
                                       ));
                                break;
                        }
                        if (vResultadoEnvio == "0")
                        {
                            DocumentoAutorizarNegocio nDocumento = new DocumentoAutorizarNegocio();
                            E_RESULTADO vResultadoActualiza = nDocumento.ActualizaEstadoAutorizaDoc(idAutorizacion, vClUsuario, vNbPrograma);
                            if (vResultadoActualiza.CL_TIPO_ERROR.ToString().Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL.ToString()))
                            {
                                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Se mandó el correo exitosamente", E_TIPO_RESPUESTA_DB.SUCCESSFUL, pCallBackFunction:"");
                            }
                            else
                            {
                                UtilMensajes.MensajeResultadoDB(rwmMensaje, vResultadoActualiza.ToString(), E_TIPO_RESPUESTA_DB.ERROR);
                            }
                        }
                        else
                        {
                            UtilMensajes.MensajeResultadoDB(rwmMensaje, vResultadoEnvio, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                        }
                    }
                    catch (Exception)
                    {
                        UtilMensajes.MensajeResultadoDB(rwmMensaje, "Error al procesar el Email", E_TIPO_RESPUESTA_DB.ERROR);
                    }
                }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Error al procesar el Email", E_TIPO_RESPUESTA_DB.ERROR);
            }
        }
    }
}