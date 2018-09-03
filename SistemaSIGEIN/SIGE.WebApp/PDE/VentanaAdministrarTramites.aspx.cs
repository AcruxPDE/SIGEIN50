using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.PuntoDeEncuentro;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.PuntoDeEncuentro;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.PDE
{
    public partial class VentanaAdministrarTramites : System.Web.UI.Page
    {

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private List<E_FORMATO_TRAMITE> vTramite
        {
            get { return (List<E_FORMATO_TRAMITE>)ViewState["vs_vTramite"]; }
            set { ViewState["vs_vTramite"] = value; }
        }
        public int vIdTramite
        {
            set { ViewState["vs_vrn_Tramite"] = value; }
            get { return (int)ViewState["vs_vrn_Tramite"]; }
        }
        public DateTime FechaHoraActual
        {
            set { ViewState["vs_vrn_FechaHora"] = value; }
            get { return (DateTime)ViewState["vs_vrn_FechaHora"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            if (!IsPostBack)
            {
                if (Request.Params["IdTramite"] != null)
                {
                    vIdTramite = int.Parse(Request.Params["IdTramite"]);
                    List<SPE_OBTIENE_FORMATOS_Y_TRAMITES_Result> ListaTramites = new List<SPE_OBTIENE_FORMATOS_Y_TRAMITES_Result>();
                    FormatosYTramitesNegocio negocio = new FormatosYTramitesNegocio();
                    ListaTramites = negocio.OBTENER_FORMATOS_Y_TRAMITES(vIdTramite, "Trámite", true);
                    foreach (SPE_OBTIENE_FORMATOS_Y_TRAMITES_Result item in ListaTramites)
                    {
                        txtNombre.Text = item.NB_FORMATO_TRAMITE;
                        txtDescripcion.Text = item.DS_FORMATO_TRAMITE;
                        XElement tramite = XElement.Parse(item.XML_FORMATO_TRAMITE);
                        DeserializarDocumentoAutorizar(tramite);
                        if (vTramite.Count > 0)
                        {
                            reTramite.Content = vTramite.FirstOrDefault().XML_FORMATO_TRAMITE.ToString();
                        }

                    }
                }
                List<SPE_OBTIENE_CAMPO_FORMULARIO_Result> ListaCampos = new List<SPE_OBTIENE_CAMPO_FORMULARIO_Result>();
                CampoFormularioNegocio negocioCampo = new CampoFormularioNegocio();
                ListaCampos = negocioCampo.ObtieneCamposFormularios(null, null, null, null, "FORMATO_TRAMITE_PDE", true, null);
                rlbTramite.DataSource = ListaCampos;
                rlbTramite.DataValueField = "CL_CAMPO_FORMULARIO";
                rlbTramite.DataTextField = "NB_CAMPO_FORMULARIO";
                rlbTramite.DataBind();

                List<E_FORMATO_FECHA> ListaFechas = new List<E_FORMATO_FECHA>();

                ListaFechas.Add(new E_FORMATO_FECHA { ID_FECHA = 1, NB_FECHA = "Fecha corta (12/02/2013)", CL_FECHA = "fecha_corta" });
                ListaFechas.Add(new E_FORMATO_FECHA { ID_FECHA = 2, NB_FECHA = "Fecha larga (martes, 12 de febrero de 2013)", CL_FECHA = "fecha_larga" });
                ListaFechas.Add(new E_FORMATO_FECHA { ID_FECHA = 3, NB_FECHA = "Día mes (12 febrero)", CL_FECHA = "mes_dia" });
                ListaFechas.Add(new E_FORMATO_FECHA { ID_FECHA = 4, NB_FECHA = "Mes año (febrero de 2013)", CL_FECHA = "mes_ano" });
                ListaFechas.Add(new E_FORMATO_FECHA { ID_FECHA = 5, NB_FECHA = "Dia Fecha Hora (martes, 12 de febrero de 2013 22:48)", CL_FECHA = "dia_fecha_hora" });
                ListaFechas.Add(new E_FORMATO_FECHA { ID_FECHA = 6, NB_FECHA = "Dia Fecha Hora Segundos (martes, 12 de febrero de 2013 22:48:02)", CL_FECHA = "fecha_diagonal_hora_seg" });
                ListaFechas.Add(new E_FORMATO_FECHA { ID_FECHA = 7, NB_FECHA = "Fecha diagonal hora (12/02/2013 22:48)", CL_FECHA = "fecha_diagonal_hora" });
                ListaFechas.Add(new E_FORMATO_FECHA { ID_FECHA = 8, NB_FECHA = "Fecha diagonal hora completa (12/02/2013 22:48:02)", CL_FECHA = "dia_mes_año_parentesis" });
                ListaFechas.Add(new E_FORMATO_FECHA { ID_FECHA = 9, NB_FECHA = "Fecha paréntesis (martes (febrero 2013))", CL_FECHA = "dia_fecha_parentesis" });
                ListaFechas.Add(new E_FORMATO_FECHA { ID_FECHA = 10, NB_FECHA = "Dia Mes corto año (feb 13)", CL_FECHA = "fecha_diagonal" });
                ListaFechas.Add(new E_FORMATO_FECHA { ID_FECHA = 11, NB_FECHA = "Fecha completa diagonal (12/02/13)", CL_FECHA = "fecha_diagonal" });
                ListaFechas.Add(new E_FORMATO_FECHA { ID_FECHA = 12, NB_FECHA = "Hora corta (22:48)", CL_FECHA = "hora_corta" });
                ListaFechas.Add(new E_FORMATO_FECHA { ID_FECHA = 13, NB_FECHA = "Hora larga (22:48:02)", CL_FECHA = "hora_larga" });

                rlbFecha.DataSource = ListaFechas;
                rlbFecha.DataValueField = "CL_FECHA";
                rlbFecha.DataTextField = "NB_FECHA";
                rlbFecha.DataBind();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombreDocumento = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            string tipoDocumento = "Trámite";
            string XmlFormato = reTramite.Content;


            if (XmlFormato != String.Empty && nombreDocumento != String.Empty && descripcion != String.Empty)
            {
                var vXelementNotificacion =
                                          new XElement("DOCUMENTO",
                                          new XAttribute("XML_FORMATO_TRAMITE", reTramite.Content)
                                            );

                XElement vXelementCompleto =
                   new XElement("FORMATOS",
                 new XElement("FORMATO_XML", vXelementNotificacion));

                FormatosYTramitesNegocio nConfiguracion = new FormatosYTramitesNegocio();
                E_RESULTADO vResultado = new E_RESULTADO();
                if (Request.Params["IdTramite"] == null)
                {
                    vResultado = nConfiguracion.INSERTA_ACTUALIZA_FORMATOS_Y_TRAMITES(null, nombreDocumento, vXelementCompleto.ToString(), descripcion, tipoDocumento, true, vClUsuario, vNbPrograma, "I");
                }
                else
                {
                    vIdTramite = int.Parse(Request.Params["IdTramite"]);
                    vResultado = nConfiguracion.INSERTA_ACTUALIZA_FORMATOS_Y_TRAMITES(vIdTramite, nombreDocumento, vXelementCompleto.ToString(), descripcion, tipoDocumento, true, vClUsuario, vNbPrograma, "A");
                }

                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, E_TIPO_RESPUESTA_DB.SUCCESSFUL);
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Escribe el nombre y la descripción del trámite", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            }
        }


        public void DeserializarDocumentoAutorizar(XElement tablas)
        {
            if (ValidarRamaXml(tablas, "FORMATO_XML"))
            {
                vTramite = tablas.Element("FORMATO_XML").Elements("DOCUMENTO").Select(el => new E_FORMATO_TRAMITE
                {
                    XML_FORMATO_TRAMITE = el.Attribute("XML_FORMATO_TRAMITE").Value
                }).ToList();
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

    }
}