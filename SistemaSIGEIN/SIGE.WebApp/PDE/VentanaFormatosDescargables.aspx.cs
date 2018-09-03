using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.Comunes;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.PuntoDeEncuentro;
using SIGE.WebApp.Comunes;
using System.Xml.Linq;
using Telerik.Web.UI;
using SIGE.Entidades.PuntoDeEncuentro;
using System.IO;
using SIGE.Negocio.Utilerias;
using SIGE.Negocio.Administracion;

namespace SIGE.WebApp.PDE
{
    public partial class VentanaFormatosDescargables : System.Web.UI.Page
    {
        List<E_FORMATO_TRAMITE> vFormato
        {
            get { return (List<E_FORMATO_TRAMITE>)ViewState["vs_vsEditarFormato"]; }
            set { ViewState["vs_vsEditarFormato"] = value; }
        }

        public int vIdFormato
        {
            set { ViewState["vs_vrn_Formato"] = value; }
            get { return (int)ViewState["vs_vrn_Formato"]; }
        }

        public DateTime FechaHoraActual
        {
            set { ViewState["vs_vrn_FechaHora"] = value; }
            get { return (DateTime)ViewState["vs_vrn_FechaHora"]; }
        }

        public string nbFormato
        {
            set { ViewState["vs_vrn_FechaHora"] = value; }
            get { return (string)ViewState["vs_vrn_FechaHora"]; }
        }
        public Byte[] info;
        public string Nombre;
        string Titulo;
        public int idFormato;
        public string correoElectronico;
        public string vClUsuario;
        string vXmlDocumentos;
        string vIdEmpleado;
        public List<E_FORMATO_FECHA> ListaFechasCampo;
        protected void Page_Load(object sender, EventArgs e)
        {
            Nombre = ContextoUsuario.oUsuario.NB_USUARIO;
            vIdEmpleado = ContextoUsuario.oUsuario.ID_EMPLEADO_PDE;
            correoElectronico = ContextoUsuario.oUsuario.NB_CORREO_ELECTRONICO;
            FechaHoraActual = DateTime.Now;
            string fecha_corta = string.Format("{0:d}", FechaHoraActual); // d: fecha corta  "12/02/2013"
            string fecha_larga = string.Format("{0:D}", FechaHoraActual); // D: fecha larga  "martes, 12 de febrero de 2013"
            string mes_dia = string.Format("{0:M}", FechaHoraActual); // m,M: Mes y día  "12 febrero"
            string mes_ano = string.Format("{0:Y}", FechaHoraActual); // y,Y: Mes y año  "febrero de 2013"
            string hora_corta = string.Format("{0:t}", FechaHoraActual); // t: hora corta "22:48"
            string hora_larga = string.Format("{0:T}", FechaHoraActual); // T: hora larga "22:48:02"
            string dia_fecha_hora_seg = string.Format("{0:f}", FechaHoraActual); // f:D+t "martes, 12 de febrero de 2013 22:48"
            string dia_fecha_hora = string.Format("{0:F}", FechaHoraActual); // F:D+T "martes, 12 de febrero de 2013 22:48:02"
            string fecha_diagonal_hora = string.Format("{0:g}", FechaHoraActual); // g:d+t "12/02/2013 22:48"
            string fecha_diagonal_hora_seg = string.Format("{0:G}", FechaHoraActual); // G:d+T "12/02/2013 22:48:02" 
            string dia_mes_año_parentesis = string.Format("{0:dddd (MMMM yyyy)}", FechaHoraActual);  //"martes (febrero 2013)"
            string dia_fecha_parentesis = string.Format("{0:ddd (MMM yy)}", FechaHoraActual);      //"mar (feb 13)"
            string fecha_diagonal = string.Format("{0:dd/MM/yy}", FechaHoraActual);          //"12/02/13"
            ListaFechasCampo = new List<E_FORMATO_FECHA>();
            ListaFechasCampo.Add(new E_FORMATO_FECHA { ID_FECHA = 1, NB_FECHA = "Fecha corta (12/02/2013)", CL_FECHA = "fecha_corta", FORMATO_FECHA = fecha_corta });
            ListaFechasCampo.Add(new E_FORMATO_FECHA { ID_FECHA = 2, NB_FECHA = "Fecha larga (martes, 12 de febrero de 2013)", CL_FECHA = "fecha_larga", FORMATO_FECHA = fecha_larga });
            ListaFechasCampo.Add(new E_FORMATO_FECHA { ID_FECHA = 3, NB_FECHA = "Día mes (12 febrero)", CL_FECHA = "mes_dia", FORMATO_FECHA = mes_dia });
            ListaFechasCampo.Add(new E_FORMATO_FECHA { ID_FECHA = 4, NB_FECHA = "Mes año (febrero de 2013)", CL_FECHA = "mes_ano", FORMATO_FECHA = mes_ano });
            ListaFechasCampo.Add(new E_FORMATO_FECHA { ID_FECHA = 5, NB_FECHA = "Dia Fecha Hora (martes, 12 de febrero de 2013 22:48)", CL_FECHA = "dia_fecha_hora", FORMATO_FECHA = dia_fecha_hora });
            ListaFechasCampo.Add(new E_FORMATO_FECHA { ID_FECHA = 6, NB_FECHA = "Dia Fecha Hora Segundos (martes, 12 de febrero de 2013 22:48:02)", CL_FECHA = "fecha_diagonal_hora_seg", FORMATO_FECHA = fecha_diagonal_hora_seg });
            ListaFechasCampo.Add(new E_FORMATO_FECHA { ID_FECHA = 7, NB_FECHA = "Fecha diagonal hora (12/02/2013 22:48)", CL_FECHA = "fecha_diagonal_hora", FORMATO_FECHA = fecha_diagonal_hora });
            ListaFechasCampo.Add(new E_FORMATO_FECHA { ID_FECHA = 8, NB_FECHA = "Fecha diagonal hora completa (12/02/2013 22:48:02)", CL_FECHA = "dia_mes_año_parentesis", FORMATO_FECHA = dia_mes_año_parentesis });
            ListaFechasCampo.Add(new E_FORMATO_FECHA { ID_FECHA = 9, NB_FECHA = "Fecha paréntesis (martes (febrero 2013))", CL_FECHA = "dia_fecha_parentesis", FORMATO_FECHA = dia_fecha_parentesis });
            ListaFechasCampo.Add(new E_FORMATO_FECHA { ID_FECHA = 10, NB_FECHA = "Dia Mes corto año (feb 13)", CL_FECHA = "fecha_diagonal", FORMATO_FECHA = fecha_diagonal });
            ListaFechasCampo.Add(new E_FORMATO_FECHA { ID_FECHA = 11, NB_FECHA = "Fecha completa diagonal (12/02/13)", CL_FECHA = "fecha_diagonal", FORMATO_FECHA = fecha_diagonal });
            ListaFechasCampo.Add(new E_FORMATO_FECHA { ID_FECHA = 12, NB_FECHA = "Hora corta (22:48)", CL_FECHA = "hora_corta", FORMATO_FECHA = hora_corta });
            ListaFechasCampo.Add(new E_FORMATO_FECHA { ID_FECHA = 13, NB_FECHA = "Hora larga (22:48:02)", CL_FECHA = "hora_larga", FORMATO_FECHA = hora_larga });
            if (!IsPostBack)
            {

                List<E_FORMATO_FECHA> ListaFechas = new List<E_FORMATO_FECHA>();

                if (Request.Params["Indice"] != null)
                {
                    vIdFormato = int.Parse(Request.Params["Indice"]);
                    rtsFormatosDescargables.SelectedIndex = vIdFormato;
                    mpFormatosTramites.SelectedIndex = vIdFormato;
                }

            }


        }
        private string ObtenerDatoEmpleado(string pClCampo)
        {
            string vValorCampo = "";

            if (!pClCampo.Equals("CL_GENERO"))
            {
                XElement vXmlSolicitud = XElement.Parse(vXmlDocumentos);
                XElement vXmlElemento = vXmlSolicitud.Elements("VALOR").Where(t => t.Attribute("ID_CAMPO").Value == pClCampo).FirstOrDefault();
                if (vXmlElemento != null)
                {
                    vValorCampo = vXmlElemento.Value;
                }
            }

            return vValorCampo;
        }

        public void DeserializarDocumentoAutorizar(XElement tablas)
        {
            if (ValidarRamaXml(tablas, "FORMATO_XML"))
            {
                vFormato = tablas.Element("FORMATO_XML").Elements("DOCUMENTO").Select(el => new E_FORMATO_TRAMITE
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

        protected void GridFormatos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            List<SPE_OBTIENE_FORMATOS_Y_TRAMITES_Result> ListaFormatos = new List<SPE_OBTIENE_FORMATOS_Y_TRAMITES_Result>();
            FormatosYTramitesNegocio negocio = new FormatosYTramitesNegocio();
            ListaFormatos = negocio.OBTENER_FORMATOS_Y_TRAMITES(null, "Formato", true);
            rgFormatos.DataSource = ListaFormatos;


        }

        protected void GridTramites_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            List<SPE_OBTIENE_FORMATOS_Y_TRAMITES_Result> ListaTramites = new List<SPE_OBTIENE_FORMATOS_Y_TRAMITES_Result>();
            FormatosYTramitesNegocio negocio = new FormatosYTramitesNegocio();
            ListaTramites = negocio.OBTENER_FORMATOS_Y_TRAMITES(null, "Trámite", true);
            rgTramites.DataSource = ListaTramites;
        }

        protected void reTramite_ExportContent(object sender, EditorExportingArgs e)
        {
            string url = String.Format("~/{0}.pdf", reTramite.ExportSettings.FileName);
            string path = Server.MapPath(url);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (FileStream fs = File.Create(path))
            {
                info = System.Text.Encoding.Default.GetBytes(e.ExportOutput);
            }
            e.Cancel = true;
        }


        public bool EnvioCorreo(string Email, string Nombre, string Titulo)
        {

            bool resultado;
            string tipo = "";
            Mail mail = new Mail(ContextoApp.mailConfiguration);
            try
            {
                //envio correo

                                //
                mail.addToAddress(Email, Nombre);

                RadProgressContext progress = RadProgressContext.Current;
                tipo = "application/pdf";
                mail.addAttachment(info, Titulo, tipo);
                if (String.IsNullOrEmpty(Email))
                {
                    UtilMensajes.MensajeResultadoDB(rnMensaje, "No cuentas con dirección de correo electrónico.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                }
                else
                {
                    string body = ContextoCorreo.CuerpoMensaje;
                    mail.Send("Buen día" + " "  + Titulo , String.Format(body, ContextoCorreo.encabezado, "Estimado(a): ", " " + Titulo, "Anexamos el documento de la solicitud requerida para tu comodidad, no olvides imprimir en hojas recicladas!! Cuidemos el medio ambiente", ContextoCorreo.pie));
                
                   

                }
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Proceso exitoso", E_TIPO_RESPUESTA_DB.SUCCESSFUL, pCallBackFunction: "");
                resultado = true;

            }
            catch (Exception)
            {
                resultado = false;
            }

            return resultado;
        }

        protected void imagenFormato_Click(object sender, ImageClickEventArgs e)
        {
            var cont = rgFormatos.SelectedItems.Count;
            if (cont > 0)
            {
                GridDataItem item = (GridDataItem)rgFormatos.SelectedItems[0];
                vIdFormato = int.Parse(item.GetDataKeyValue("ID_FORMATO_TRAMITE").ToString());
                List<SPE_OBTIENE_FORMATOS_Y_TRAMITES_Result> ListaTramites = new List<SPE_OBTIENE_FORMATOS_Y_TRAMITES_Result>();
                FormatosYTramitesNegocio negocio = new FormatosYTramitesNegocio();
                ListaTramites = negocio.OBTENER_FORMATOS_Y_TRAMITES(vIdFormato, "Formato", true);

                // xml del empleado

                SPE_OBTIENE_EMPLEADO_FORMATO_TRAMITE_Result vSolicitud = negocio.ObtenerPlantilla(null, vIdEmpleado, "I");
                vXmlDocumentos = vSolicitud.XML_VALORES;

                foreach (SPE_OBTIENE_FORMATOS_Y_TRAMITES_Result itemlt in ListaTramites)
                {

                    List<SPE_OBTIENE_CAMPO_FORMULARIO_Result> ListaCampos = new List<SPE_OBTIENE_CAMPO_FORMULARIO_Result>();
                    CampoFormularioNegocio negocioCampo = new CampoFormularioNegocio();
                    ListaCampos = negocioCampo.ObtieneCamposFormularios(null, null, null, null, "FORMATO_TRAMITE_PDE", true, null);

                    XElement formato = XElement.Parse(itemlt.XML_FORMATO_TRAMITE);
                    DeserializarDocumentoAutorizar(formato);
                    if (vFormato.Count > 0)
                    {
                        string a = "";
                        foreach (var campo in ListaCampos)
                        {
                            if (vFormato.FirstOrDefault().XML_FORMATO_TRAMITE.ToString().Contains(campo.CL_CAMPO_FORMULARIO))
                            {
                                a = vFormato.FirstOrDefault().XML_FORMATO_TRAMITE.ToString().Replace("{" + campo.CL_CAMPO_FORMULARIO + "}", ObtenerDatoEmpleado(campo.CL_CAMPO_FORMULARIO)); //, vSolicitud.NB_EVALUADOR//
                                vFormato.FirstOrDefault().XML_FORMATO_TRAMITE = a;
                            }
                        }
                        foreach (var fecha in ListaFechasCampo)
                        {
                            if (vFormato.FirstOrDefault().XML_FORMATO_TRAMITE.ToString().Contains(fecha.CL_FECHA))
                            {
                                a = vFormato.FirstOrDefault().XML_FORMATO_TRAMITE.ToString().Replace("'" + fecha.CL_FECHA + "'", (fecha.FORMATO_FECHA)); //, vSolicitud.NB_EVALUADOR//
                                vFormato.FirstOrDefault().XML_FORMATO_TRAMITE = a;
                            }
                        }
                        reTramite.Content = vFormato.FirstOrDefault().XML_FORMATO_TRAMITE.ToString();
                        nbFormato = itemlt.NB_FORMATO_TRAMITE;
                        reTramite.ExportToPdf();
                    }
                }

                Titulo = nbFormato + " " + Nombre;

                EnvioCorreo(correoElectronico, Nombre, Titulo);
            }
            else { UtilMensajes.MensajeResultadoDB(rnMensaje, "Selecciona un formato", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: ""); }



        }

        protected void imagenTramite_Click(object sender, ImageClickEventArgs e)
        {
            var cont = rgTramites.SelectedItems.Count;
            if (cont > 0)
            {
                GridDataItem item = (GridDataItem)rgTramites.SelectedItems[0];
                vIdFormato = int.Parse(item.GetDataKeyValue("ID_FORMATO_TRAMITE").ToString());
                List<SPE_OBTIENE_FORMATOS_Y_TRAMITES_Result> ListaTramites = new List<SPE_OBTIENE_FORMATOS_Y_TRAMITES_Result>();
                FormatosYTramitesNegocio negocio = new FormatosYTramitesNegocio();
                ListaTramites = negocio.OBTENER_FORMATOS_Y_TRAMITES(vIdFormato, "Trámite", true);
                // xml del empleado

                SPE_OBTIENE_EMPLEADO_FORMATO_TRAMITE_Result vSolicitud = negocio.ObtenerPlantilla(null, vIdEmpleado, "I");
                vXmlDocumentos = vSolicitud.XML_VALORES;

                foreach (SPE_OBTIENE_FORMATOS_Y_TRAMITES_Result itemlt in ListaTramites)
                {

                    List<SPE_OBTIENE_CAMPO_FORMULARIO_Result> ListaCampos = new List<SPE_OBTIENE_CAMPO_FORMULARIO_Result>();
                    CampoFormularioNegocio negocioCampo = new CampoFormularioNegocio();
                    ListaCampos = negocioCampo.ObtieneCamposFormularios(null, null, null, null, "FORMATO_TRAMITE_PDE", true, null);

                    XElement formato = XElement.Parse(itemlt.XML_FORMATO_TRAMITE);
                    DeserializarDocumentoAutorizar(formato);
                    if (vFormato.Count > 0)
                    {
                        string a = "";
                        foreach (var campo in ListaCampos)
                        {
                            if (vFormato.FirstOrDefault().XML_FORMATO_TRAMITE.ToString().Contains(campo.CL_CAMPO_FORMULARIO))
                            {
                                a = vFormato.FirstOrDefault().XML_FORMATO_TRAMITE.ToString().Replace("{" + campo.CL_CAMPO_FORMULARIO + "}", ObtenerDatoEmpleado(campo.CL_CAMPO_FORMULARIO)); //, vSolicitud.NB_EVALUADOR//
                                vFormato.FirstOrDefault().XML_FORMATO_TRAMITE = a;
                            }
                        }
                        foreach (var fecha in ListaFechasCampo)
                        {
                            if (vFormato.FirstOrDefault().XML_FORMATO_TRAMITE.ToString().Contains(fecha.CL_FECHA))
                            {
                                a = vFormato.FirstOrDefault().XML_FORMATO_TRAMITE.ToString().Replace("'" + fecha.CL_FECHA + "'", (fecha.FORMATO_FECHA)); //, vSolicitud.NB_EVALUADOR//
                                vFormato.FirstOrDefault().XML_FORMATO_TRAMITE = a;
                            }
                        }
                        reTramite.Content = vFormato.FirstOrDefault().XML_FORMATO_TRAMITE.ToString();
                        nbFormato = itemlt.NB_FORMATO_TRAMITE;
                        reTramite.ExportToPdf();
                    }
                }
                Titulo = nbFormato + " " + Nombre;
                EnvioCorreo(correoElectronico, Nombre, Titulo);
            }
            else { UtilMensajes.MensajeResultadoDB(rnMensaje, "Selecciona un trámite", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: ""); }
        }

    }
}







