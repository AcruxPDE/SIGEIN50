using SIGE.Entidades.Externas;
using SIGE.Entidades.PuntoDeEncuentro;
﻿using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.Comunes;
using SIGE.Entidades;
using SIGE.Negocio.PuntoDeEncuentro;
using SIGE.WebApp.Comunes;
using System.Xml.Linq;
using Telerik.Web.UI;
using System.Data;
using System.IO;
using System.Text;



namespace SIGE.WebApp.PDE
{
    public partial class VentanaNotificacionesRRHH : System.Web.UI.Page
    {
        public string vIdEmpleado;
        public string vNbPrograma;
        public string vClUsuario;
        string Email { set; get; }
        bool vCerrarPantalla;
        StringBuilder builder = new StringBuilder();
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private string xmlPuestoEmpleado;

        List<E_CONFIGURACION_NOTIFICACION> vConfiguracionesNotificacion
        {
            get { return (List<E_CONFIGURACION_NOTIFICACION>)ViewState["vs_vConfiguracionesNotificacion"]; }
            set { ViewState["vs_vConfiguracionesNotificacion"] = value; }
        }

        E_DESCRIPCION_NOTIFICACION vNotificacionRegistrar
        {
            get { return (E_DESCRIPCION_NOTIFICACION)ViewState["vs_vConfiguracionNotificacionRegistrar"]; }
            set { ViewState["vs_vConfiguracionNotificacionRegistrar"] = value; }

        }

        public string vInstruccion
        {
            set { ViewState["vs_vip_Instruccion"] = value; }
            get { return (string)ViewState["vs_vip_Instruccion"]; }
        }
        public string vId_Empleado
        {
            set { ViewState["vs_vId_Empleado"] = value; }
            get { return (string)ViewState["vs_vId_Empleado"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            vIdEmpleado = ContextoUsuario.oUsuario.CL_USUARIO.ToString();
            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;

            if (!IsPostBack)
            {

                ConfiguracionNotificacionNegocio negocio = new ConfiguracionNotificacionNegocio();
                var notificacion = negocio.ObtenerNotificaciones();
                parseNotificarConfiguracion(notificacion);
                if (vConfiguracionesNotificacion != null)
                {
                    XElement notificaciones = XElement.Parse(vConfiguracionesNotificacion.FirstOrDefault().XML_INSTRUCCION);
                    DeserializarDocumentoAutorizar(notificaciones);
                    if (vNotificacionRegistrar != null)
                    {

                        //pIns.InnerHtml = vNotificacionRegistrar.DS_INSTRUCCION.ToString();
                        pIns.Text = vNotificacionRegistrar.DS_INSTRUCCION.ToString();
                    }
                }


            }
        }
        public void parseNotificarConfiguracion(List<SPE_OBTIENE_CONFIGURACION_NOTIFICACION_Result> lista)
        {
            vConfiguracionesNotificacion = new List<E_CONFIGURACION_NOTIFICACION>();
            foreach (SPE_OBTIENE_CONFIGURACION_NOTIFICACION_Result item in lista)
            {
                vConfiguracionesNotificacion.Add(new E_CONFIGURACION_NOTIFICACION
                {
                    ID_CONFIGURACION_NOTIFICACION = item.ID_CONFIGURACION_NOTIFICACION
                    ,
                    XML_INSTRUCCION = item.XML_INSTRUCCION
                });
            }
        }

        public void DeserializarDocumentoAutorizar(XElement tablas)
        {
            if (ValidarRamaXml(tablas, "REGISTRAR"))
            {
                vNotificacionRegistrar = tablas.Element("REGISTRAR").Elements("INSTRUCCION").Select(el => new E_DESCRIPCION_NOTIFICACION
                {
                    DS_INSTRUCCION = el.Attribute("DS_INSTRUCCION").Value
                }).FirstOrDefault();
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


        public bool EnvioCorreo(string Email, string Nombre, string Titulo, string Mensaje, string Msj, string Asunto)
        {

            bool resultado;
            byte[] data = null;
            String name = "";
            string tipo = "";
            Mail mail = new Mail(ContextoApp.mailConfiguration);
            try
            {
                //envio correo
                mail.addToAddress(Email, Nombre);

                RadProgressContext progress = RadProgressContext.Current;
                if (rauArchivo.UploadedFiles.Count > 0)
                {
                    data = new byte[rauArchivo.UploadedFiles[0].ContentLength];
                    rauArchivo.UploadedFiles[0].InputStream.Read(data, 0, int.Parse(rauArchivo.UploadedFiles[0].ContentLength.ToString()));
                    name = rauArchivo.UploadedFiles[0].FileName.ToString();
                    string extension = Path.GetExtension(name).ToLowerInvariant();

                    if (extension.Length > 0 &&
                        mail.MIMETypesDictionary.ContainsKey(extension.Remove(0, 1)))
                    {
                        tipo = mail.MIMETypesDictionary[extension.Remove(0, 1)];
                    }
                    if (tipo == "image/png" || tipo == "image/jpg" || tipo == "image/jpeg" || tipo == "application/pdf")
                    {
                        mail.addAttachment(data, name, tipo);

                    }

                }
                else
                {
                    // UtilMensajes.MensajeResultadoDB(rnMensaje, "No tienes dirección de correo electrónico, no se enviará ", E_TIPO_RESPUESTA_DB.WARNING,pCallBackFunction:"");
                    data = null;
                }
                //Insertar

                NotificacionNegocio nConfiguracion = new NotificacionNegocio();

                ConfiguracionNotificacionNegocio negocio = new ConfiguracionNotificacionNegocio();
                xmlPuestoEmpleado = negocio.ObtienePuestoEmpleadoNotificacion(null, vClUsuario);
                XElement root = XElement.Parse(xmlPuestoEmpleado);
                var id_empleado = "";
                foreach (XElement id in root.Elements("EMPLEADO"))
                {
                    id_empleado = id.Attribute("ID_EMPLEADO").Value;
                    if (id_empleado == "")
                    {
                        vId_Empleado = id_empleado;

                    }
                }
                if (vId_Empleado == null)
                {
                    E_RESULTADO vResultado = nConfiguracion.INSERTA_ACTUALIZA_NOTIFICACION_EMPLEADO(0, Asunto, Msj,
                        vIdEmpleado, "Pendiente", vClUsuario, vNbPrograma, null, name, data, "I");
                    rauArchivo.UploadedFiles.Clear();
                    if (Email == ";")
                    {
                        UtilMensajes.MensajeResultadoDB(rnMensaje, "No hay dirección de correo electrónico para recursos humanos.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                    }
                    else
                    {
                        string body = ContextoCorreo.CuerpoMensaje;
                        mail.Send("Buen día" + " " + Titulo, String.Format(body, ContextoCorreo.encabezado, "Estimado(a): ", " " + Mensaje, " No olvides imprimir en hojas recicladas!! Cuidemos el medio ambiente", ContextoCorreo.pie));


                        //  mail.Send(Titulo, Mensaje);

                    }

                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    if (vCerrarPantalla == false)
                    {
                        UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, E_TIPO_RESPUESTA_DB.SUCCESSFUL);

                    }
                    else
                    {
                        UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, E_TIPO_RESPUESTA_DB.SUCCESSFUL, pCallBackFunction: "");

                    }
                }
                else {
                    UtilMensajes.MensajeResultadoDB(rnMensaje, "Seleccione a quién será enviada la notificación", E_TIPO_RESPUESTA_DB.WARNING , pCallBackFunction: "");

                }
                resultado = true;
            }
            catch (Exception)
            {  //  UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
                resultado = false;
            }

            return resultado;

        }

        protected void bntEnviar_Click(object sender, EventArgs e)
        {
            var correos = "";
            ConfiguracionNotificacionNegocio negocio = new ConfiguracionNotificacionNegocio();
            xmlPuestoEmpleado = negocio.ObtienePuestoEmpleadoNotificacion(null, vClUsuario);
            XElement root = XElement.Parse(xmlPuestoEmpleado);
            foreach (XElement name in root.Elements("EMPLEADO"))
            {
                correos = name.Attribute("NB_CORREO").Value;
                builder.Append(correos.ToString() + ";");
            }

            Email = builder.ToString();
            //if (String.IsNullOrEmpty(Email))
            //{
            //    UtilMensajes.MensajeResultadoDB(rnMensaje, "No cuentas con dirección de correo electrónico.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            //}
            string Nombre = ContextoUsuario.oUsuario.NB_USUARIO;
            string Asunto = txtAsunto.Text;
            string Titulo = txtAsunto.Text + ", " + Nombre;
            string Msj = txtDsNotas.Content;
            string Mensaje = txtDsNotas.Content + " " + "Hay una nueva notificación en su bandeja.";
            if ((Mensaje.Length > 0 && rauArchivo.UploadedFiles.Count > 0 && Asunto.Length > 0) ||
                (Mensaje.Length > 0 && rauArchivo.UploadedFiles.Count == 0 && Asunto.Length > 0) ||
                (Mensaje.Length >= 0 && rauArchivo.UploadedFiles.Count > 0 && Asunto.Length > 0))
            {
                btnEnviar.Enabled = true;
                EnvioCorreo(Email, Nombre, Titulo, Mensaje, Msj, Asunto);
                txtDsNotas.Content = "";
                txtAsunto.Text = "";
                rcbCerrar.Checked = false;
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Escribe tu notificacion", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");

            }


        }

        protected void rcbCerrar_CheckedChanged(object sender, EventArgs e)
        {

            if (rcbCerrar.Checked == true)
            {
                vCerrarPantalla = true;
            }
            else { vCerrarPantalla = false; }
        }


    }
}
