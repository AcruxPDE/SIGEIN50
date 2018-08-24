using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.PuntoDeEncuentro;
using SIGE.Negocio.PuntoDeEncuentro;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;

namespace SIGE.WebApp.PDE
{
    public partial class VentanaEditarNotificacionesRRHH : System.Web.UI.Page
    {
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
        E_DESCRIPCION_NOTIFICACION vNotificacionAdministrar
        {
            get { return (E_DESCRIPCION_NOTIFICACION)ViewState["vs_vConfiguracionNotificacionAdministrar"]; }
            set { ViewState["vs_vConfiguracionNotificacionAdministrar"] = value; }
        }
        E_DESCRIPCION_NOTIFICACION vNotificacionComunicados
        {
            get { return (E_DESCRIPCION_NOTIFICACION)ViewState["vs_vNotificacionComunicados"]; }
            set { ViewState["vs_vNotificacionComunicados"] = value; }
        }
        E_DESCRIPCION_NOTIFICACION vComunicado
        {
            get { return (E_DESCRIPCION_NOTIFICACION)ViewState["vs_vComunicado"]; }
            set { ViewState["vs_vComunicado"] = value; }
        }

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                ConfiguracionNotificacionNegocio nConfiguracionNotificacion = new ConfiguracionNotificacionNegocio();
                var notificacion = nConfiguracionNotificacion.ObtenerNotificaciones();
                if (notificacion.Count> 0)
                {
                    parseNotificarConfiguracion(notificacion);
                    if (vConfiguracionesNotificacion != null)
                    {
                        XElement notificaciones = XElement.Parse(vConfiguracionesNotificacion.FirstOrDefault().XML_INSTRUCCION);
                        DeserializarDocumentoAutorizar(notificaciones);
                        if (vNotificacionRegistrar != null)
                        {
                            txtDsNotas.Content = vNotificacionRegistrar.DS_INSTRUCCION.ToString();
                        }

                        if (vNotificacionAdministrar != null)
                        {
                            txtDsNotas2.Content = vNotificacionAdministrar.DS_INSTRUCCION.ToString();
                        }
                        if (vComunicado != null)
                        {
                            txtDsNotas3.Content = vComunicado.DS_INSTRUCCION.ToString();
                        }
                    }
                }
            }
        }

        public void parseNotificarConfiguracion(List <SPE_OBTIENE_CONFIGURACION_NOTIFICACION_Result> lista) 
        {
            vConfiguracionesNotificacion = new List<E_CONFIGURACION_NOTIFICACION>();
            foreach (SPE_OBTIENE_CONFIGURACION_NOTIFICACION_Result item in lista)
            {
                vConfiguracionesNotificacion.Add(    new E_CONFIGURACION_NOTIFICACION {ID_CONFIGURACION_NOTIFICACION = item.ID_CONFIGURACION_NOTIFICACION
                                                  ,XML_INSTRUCCION = item.XML_INSTRUCCION});
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


        public void DeserializarDocumentoAutorizar(XElement tablas)
        {
            if (ValidarRamaXml(tablas, "REGISTRAR"))
            {
                vNotificacionRegistrar = tablas.Element("REGISTRAR").Elements("INSTRUCCION").Select(el => new E_DESCRIPCION_NOTIFICACION
                {
                    DS_INSTRUCCION = el.Attribute("DS_INSTRUCCION").Value
                }).FirstOrDefault();
            }

            if (ValidarRamaXml(tablas, "ADMINISTRAR"))
            {
                vNotificacionAdministrar = tablas.Element("ADMINISTRAR").Elements("INSTRUCCION").Select(el => new E_DESCRIPCION_NOTIFICACION
                {
                    DS_INSTRUCCION = el.Attribute("DS_INSTRUCCION").Value
                }).FirstOrDefault();
            }

            if (ValidarRamaXml(tablas, "COMUNICADOS"))
            {
                vComunicado = tablas.Element("COMUNICADOS").Elements("INSTRUCCION").Select(el => new E_DESCRIPCION_NOTIFICACION
                {
                    DS_INSTRUCCION = el.Attribute("DS_INSTRUCCION").Value
                }).FirstOrDefault();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            String vTipoTransaccion = "";
            if (vNotificacionRegistrar != null && vNotificacionAdministrar != null)
            {
                vTipoTransaccion = E_TIPO_OPERACION_DB.A.ToString();
            }
            else 
            {
                vTipoTransaccion = E_TIPO_OPERACION_DB.I.ToString();
            }
                    var vXelementNotificacionRegistrar =
                      new XElement("INSTRUCCION",
                      new XAttribute("DS_INSTRUCCION", txtDsNotas.Content)
                        );

                    var vXelementNotificacionAdministrar =
                      new XElement("INSTRUCCION",
                      new XAttribute("DS_INSTRUCCION", txtDsNotas2.Content)
                        );

                    var vXelementComunicados =
                             new XElement("INSTRUCCION",
                             new XAttribute("DS_INSTRUCCION", txtDsNotas3.Content)
                               );

                XElement vXelementCompleto =
                    new XElement("CONFIGURACIONES",
                    new XElement("REGISTRAR", vXelementNotificacionRegistrar),
                    new XElement("ADMINISTRAR", vXelementNotificacionAdministrar),
                    new XElement("COMUNICADOS", vXelementComunicados));

                if (vConfiguracionesNotificacion ==null) 
                {
                    vConfiguracionesNotificacion= new List<E_CONFIGURACION_NOTIFICACION>();
                    vConfiguracionesNotificacion.Add(new E_CONFIGURACION_NOTIFICACION{ID_CONFIGURACION_NOTIFICACION=1,XML_INSTRUCCION= vXelementCompleto.ToString()});
                }

            if(vConfiguracionesNotificacion.Count>0){
                    vConfiguracionesNotificacion.FirstOrDefault().XML_INSTRUCCION = vXelementCompleto.ToString();
                    ConfiguracionNotificacionNegocio nConfiguracion = new ConfiguracionNotificacionNegocio();
                    E_RESULTADO vResultado = nConfiguracion.INSERTA_ACTUALIZA_CONFIGURACION_NOTIFICACION(vTipoTransaccion, vConfiguracionesNotificacion.FirstOrDefault(), vClUsuario, vNbPrograma);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    UtilMensajes.MensajeResultadoDB(rwmMensaje , vMensaje, vResultado.CL_TIPO_ERROR, 400, 150);
                }
        }
    }
}