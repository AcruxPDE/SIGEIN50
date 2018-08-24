using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.PuntoDeEncuentro;
using SIGE.Negocio.PuntoDeEncuentro;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.PDE
{
    public partial class VentanaRespuestaANotificacion : System.Web.UI.Page
    {
        public string vIdEmpleado;
        public string vNbPrograma;
        public string vClUsuario;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        List<E_CONFIGURACION_NOTIFICACION> vConfiguracionesNotificacion
        {
            get { return (List<E_CONFIGURACION_NOTIFICACION>)ViewState["vs_vConfiguracionesNotificacion"]; }
            set { ViewState["vs_vConfiguracionesNotificacion"] = value; }
        }

        List<E_DESCRIPCION_NOTIFICACION> vNotificacion
        {
            get { return (List<E_DESCRIPCION_NOTIFICACION>)ViewState["vs_vConfiguracionNotificacion"]; }
            set { ViewState["vs_vConfiguracionNotificacion"] = value; }
        }

        public string vInstruccion
        {
            set { ViewState["vs_vip_Instruccion"] = value; }
            get { return (string)ViewState["vs_vip_Instruccion"]; }
        }
        public string vTipoArchivo
        {
            set { ViewState["vs_vip_TipoArchivo"] = value; }
            get { return (string)ViewState["vs_vip_TipoArchivo"]; }
        }

        public int vIdNotificacion
        {
            set { ViewState["vs_IdNotificacion"] = value; }
            get { return (int)ViewState["vs_IdNotificacion"]; }
        }
        public int vIdArchivo
        {
            set { ViewState["vs_IdArchivo"] = value; }
            get { return (int)ViewState["vs_IdArchivo"]; }
        }

        E_DESCRIPCION_NOTIFICACION vNotificacionAdministrar
        {
            get { return (E_DESCRIPCION_NOTIFICACION)ViewState["vs_vConfiguracionNotificacionAdministrar"]; }
            set { ViewState["vs_vConfiguracionNotificacionAdministrar"] = value; }
        }
        public string vBotonesVisibles
        {
            set { ViewState["vs_vrn_BotonesVisibles"] = value; }
            get { return (string)ViewState["vs_vrn_BotonesVisibles"]; }
        }
        public Byte[] archivo
        {
            set { ViewState["vs_vrn_Archivo"] = value; }
            get { return (Byte[])ViewState["vs_vrn_Archivo"]; }

        }
        public string vStatusNotificacion
        {
            set { ViewState["vs_vrn_Status"] = value; }
            get { return (string)ViewState["vs_vrn_Status"]; }
        }
        public string vComentario
        {
            set { ViewState["vs_vrn_Comentario"] = value; }
            get { return (string)ViewState["vs_vrn_Comentario"]; }
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
                    if (vNotificacionAdministrar != null)
                    {

                        //pIns.InnerHtml = vNotificacionAdministrar.DS_INSTRUCCION.ToString();
                        pIns.Text = vNotificacionAdministrar.DS_INSTRUCCION.ToString();
                    }
                }


                if (Request.Params["pIdNotificacion"] != null)
                {
                    vIdNotificacion = int.Parse(Request.Params["pIdNotificacion"]);
                    vBotonesVisibles = (Request.Params["pBotonesVisbles"]);
                    List<SPE_OBTIENE_NOTIFICACIONES_PENDIENTES_Result> ListaNotificacionesPendientes = new List<SPE_OBTIENE_NOTIFICACIONES_PENDIENTES_Result>();
                    MenuNegocio nego = new MenuNegocio();
                    vIdArchivo =0;
                    ListaNotificacionesPendientes = nego.ObtenerNotificacionesPendientes(ContextoUsuario.oUsuario.CL_USUARIO, vIdNotificacion, null);
                    foreach (SPE_OBTIENE_NOTIFICACIONES_PENDIENTES_Result item in ListaNotificacionesPendientes)
                    {
                        txtAsunto.Text = item.NB_ASUNTO;
                        lblNotificacion.InnerHtml = item.DS_NOTIFICACION;
                        archivo = item.FI_ARCHIVO;
                        vTipoArchivo = item.NB_ARCHIVO;
                        vStatusNotificacion = item.CL_ESTADO;
                        vComentario = item.DS_COMENTARIO;
                        if (item.ID_ARCHIVO_PDE != null)
                        {
                            vIdArchivo = (int)item.ID_ARCHIVO_PDE;
                        }
                        else { vIdArchivo = (int)0; }
                    }

                    if (archivo == null)
                    {
                        ArchivoAdjunto.Visible = false;
                    }
                    else
                    {
                        ArchivoAdjunto.Visible = true;
                    }

                    if (vBotonesVisibles == "true")
                    {
                        btnAutorizar.Visible = true;
                        btnRechazar.Visible = true;
                        rcbleido.Visible = true;
                        if (vStatusNotificacion == "En revisión")
                        {
                            rcbleido.Checked = true;
                        }
                    }
                    else
                    {
                        if (vStatusNotificacion == "Autorizada")
                        {
                            btnRerponder.Visible = false;
                            lblFinComentario.Visible = true;
                            lblFinComentario.Text = "Petición autorizada: " + vComentario;
                        }
                        else if (vStatusNotificacion == "Rechazada")
                        {
                            btnRerponder.Visible = false;
                            lblFinComentario.Visible = true;
                            lblFinComentario.Text = "Peticion rechazada: " + vComentario;
                        }

                        btnAutorizar.Visible = false;
                        btnRechazar.Visible = false;
                        rcbleido.Visible = false;
                    }

                    rlvComentarios.Rebind();


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
            if (ValidarRamaXml(tablas, "ADMINISTRAR"))
            {
                vNotificacionAdministrar = tablas.Element("ADMINISTRAR").Elements("INSTRUCCION").Select(el => new E_DESCRIPCION_NOTIFICACION
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


        public static System.Drawing.Image CreateImage(byte[] imageData)
        {
            System.Drawing.Image image;
            using (MemoryStream inStream = new MemoryStream())
            {
                inStream.Write(imageData, 0, imageData.Length);

                image = Bitmap.FromStream(inStream);
            }

            return image;
        }

        protected void rcbleido_CheckedChanged(object sender, EventArgs e)
        {
            if (rcbleido.Checked == true)
            {
                vStatusNotificacion = "En revisión";
                NotificacionNegocio nConfiguracion = new NotificacionNegocio();
                E_RESULTADO vResultado = nConfiguracion.INSERTA_ACTUALIZA_NOTIFICACION_EMPLEADO(vIdNotificacion, null, null,
                vIdEmpleado, vStatusNotificacion, vClUsuario, vNbPrograma, "Se ha leído la notificación", null, null, "A");
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, E_TIPO_RESPUESTA_DB.SUCCESSFUL);
            }

        }

        protected void rlvComentarios_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
        {
            MenuNegocio nConfiguracion = new MenuNegocio();
            rlvComentarios.DataSource = nConfiguracion.ObtenerComentariosNotificaciones(null, vIdNotificacion);
    
        }


        }

    
}
