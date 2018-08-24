using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.IDP
{
    public partial class Solicitudes : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int vIdSolicitud
        {
            get { return (int)ViewState["vs_vIdSolicitud"]; }
            set { ViewState["vs_vIdSolicitud"] = value; }
        }

        private bool vAgregarSolicitud;
        private bool vModificarSolicitud;
        private bool vEliminarSolicitud;
        //private bool vContratarEmpleado;
        //private bool vResultadoPruebas;
        //private bool vProcesoSeleccion;
        //private bool vConsultasPersonales;

        private void SeguridadProcesos()
        {
            btnGuardar.Enabled = vAgregarSolicitud = ContextoUsuario.oUsuario.TienePermiso("A.A.A");
            btnEditar.Enabled = vModificarSolicitud = ContextoUsuario.oUsuario.TienePermiso("A.A.B");
            btnEliminar.Enabled = vEliminarSolicitud = ContextoUsuario.oUsuario.TienePermiso("A.A.C");
            //btnProceso.Enabled = vContratarEmpleado = ContextoUsuario.oUsuario.TienePermiso("A.A.D");
            //btnPruebas.Enabled = vResultadoPruebas = ContextoUsuario.oUsuario.TienePermiso("A.A.E");
            //btnProcesoSeleccion.Enabled = vProcesoSeleccion = ContextoUsuario.oUsuario.TienePermiso("A.A.F");
            //btnConsultas.Enabled = vConsultasPersonales = ContextoUsuario.oUsuario.TienePermiso("A.A.G");
        }

        protected void Page_Init(object sender, System.EventArgs e)
        {
            SeguridadProcesos();
            DefineGrid();
        }

        private void DefineGrid()
        {
            SolicitudNegocio nSolicitud = new SolicitudNegocio();
            List<SPE_OBTIENE_SOLICITUDES_Result> vSolicitudes;
            vSolicitudes = nSolicitud.ObtieneCatalogoSolicitudes();
            CamposAdicionales cad = new CamposAdicionales();
            grdSolicitudes.DataSource = vSolicitudes;
        }

        private string EnvioCorreoSolicitudes(List<E_CANDIDATO_SOLICITUD> listaCandidatos)
        {
            var respuesta = "0";
            foreach (E_CANDIDATO_SOLICITUD item in listaCandidatos)
            {
                var EnviaMail = false;
                Mail mail = new Mail(ContextoApp.mailConfiguration);
                if (!String.IsNullOrEmpty(item.C_CANDIDATO_CL_CORREO_ELECTRONICO) && item.C_CANDIDATO_CL_CORREO_ELECTRONICO != "&nbsp;" && ContextoApp.IDP.MensajeActualizacionPeriodica.fgVisible)
                {
                    //TIENE CONFIGURADO QUE SE LE ENVIA CORREO AL SOLICITANTE
                    mail.addToAddress(item.C_CANDIDATO_CL_CORREO_ELECTRONICO, null);
                    EnviaMail = true;
                }
                //AGREGAR CORREOS DE RR.HH
                if (ContextoApp.IDP.MensajeActualizacionPeriodica.dsNotificacion.fgVisible)
                {
                    var direccionesRRHH = ContextoApp.IDP.MensajeActualizacionPeriodica.lstCorreos;
                    foreach (var dirrrhh in direccionesRRHH)
                    {
                        mail.addToAddress(dirrrhh.Address, dirrrhh.DisplayName);
                        EnviaMail = true;
                    }
                }
                if (EnviaMail)
                {
                    //  respuesta = mail.Send("Candidatos", String.Format("Estimado(a) {0},<br/><br/>{1}<br/><br/>Saludos cordiales.", item.C_CANDIDATO_NB_EMPLEADO_COMPLETO, ContextoApp.IDP.MensajeActualizacionPeriodica.dsNotificacion.dsMensaje));
                    respuesta = mail.Send(ContextoApp.IDP.MensajeAsutoCorreo.dsMensaje, String.Format("Estimado(a) {0},<br/><br/>{1}<br/><br/>Saludos cordiales.", item.C_CANDIDATO_NB_EMPLEADO_COMPLETO, ContextoApp.IDP.MensajeCuerpoCorreo.dsMensaje));
                    if (respuesta != "0")
                    {
                        return respuesta;
                    }
                }
            }
            return respuesta;
        }

        public E_RESULTADO eliminarSolicitud(List<E_CANDIDATO_SOLICITUD> listaCandidatos)
        {
            XElement xmlElements = new XElement("CANDIDATOS", listaCandidatos.Select(i => new XElement("CANDIDATO", new XAttribute("ID_SOLICITUD", i.ID_SOLICITUD),
                                                                                          new XAttribute("C_CANDIDATO_NB_EMPLEADO_COMPLETO", i.C_CANDIDATO_NB_EMPLEADO_COMPLETO),
                                                                                          new XAttribute("C_CANDIDATO_CL_CORREO_ELECTRONICO", i.C_CANDIDATO_CL_CORREO_ELECTRONICO))));

            SolicitudNegocio nSolicitud = new SolicitudNegocio();
            return nSolicitud.Elimina_K_SOLICITUDES(xmlElements, vClUsuario, vNbPrograma);

        }

        private string EnvioCorreoSolicitudesConContrasena(List<E_CANDIDATO_SOLICITUD> listaCandidatos)
        {
            var respuesta = "0";
            foreach (E_CANDIDATO_SOLICITUD item in listaCandidatos)
            {
                var EnviaMail = false;
                Mail mail = new Mail(ContextoApp.mailConfiguration);
                if (!String.IsNullOrEmpty(item.C_CANDIDATO_CL_CORREO_ELECTRONICO) && item.C_CANDIDATO_CL_CORREO_ELECTRONICO != "&nbsp;")
                {
                    mail.addToAddress(item.C_CANDIDATO_CL_CORREO_ELECTRONICO, null);
                    EnviaMail = true;
                }
                //AGREGAR CORREOS DE RR.HH
                if (ContextoApp.IDP.MensajeActualizacionPeriodica.dsNotificacion.fgVisible)
                {
                    var direccionesRRHH = ContextoApp.IDP.MensajeActualizacionPeriodica.lstCorreos;
                    foreach (var dirrrhh in direccionesRRHH)
                    {
                        mail.addToAddress(dirrrhh.Address, dirrrhh.DisplayName);
                        EnviaMail = true;
                    }
                }
                if (EnviaMail)
                {
                    //OBTENER CONTRASEÑA Y GENERAR TOKEN
                    SolicitudNegocio nSolicitud = new SolicitudNegocio();
                    var vTokenCartera = Guid.NewGuid();
                    var vPassCartera = nSolicitud.GenerarContrasenaCartera();
                    var vActualizacion = nSolicitud.ActualizaDatosSolicitudCorreo(item.ID_SOLICITUD, vTokenCartera, vPassCartera, vClUsuario, vNbPrograma);
                    string myUrl = ResolveUrl("~/Logon.aspx?ClProceso=ACTUALIZACIONCARTERA&FlProceso=");
                    string vUrl = ContextoUsuario.nbHost + myUrl + vTokenCartera;
                    var vMsgUrl = String.Format("Acceso: <a href={0}>aquí</a><br/><br/>Contraseña: {1}", vUrl, vPassCartera);

                    //   respuesta = mail.Send("Candidatos", String.Format("Estimado(a) {0},<br/><br/>{1}<br/><br/>{2}<br/><br/>Saludos cordiales.", item.C_CANDIDATO_NB_EMPLEADO_COMPLETO, ContextoApp.IDP.MensajeActualizacionPeriodica.dsNotificacion.dsMensaje, vMsgUrl));
                    respuesta = mail.Send(ContextoApp.IDP.MensajeAsutoCorreo.dsMensaje, String.Format("Estimado(a) {0},<br/><br/>{1}<br/><br/>{2}<br/><br/>Saludos cordiales.", item.C_CANDIDATO_NB_EMPLEADO_COMPLETO, ContextoApp.IDP.MensajeCuerpoCorreo.dsMensaje, vMsgUrl));
                    if (respuesta != "0")
                    {
                        return respuesta;
                    }
                }
            }
            return respuesta;
        }

        private void Borrar()
        {
            SolicitudNegocio nSolicitud = new SolicitudNegocio();
            List<E_CANDIDATO_SOLICITUD> listaCandidatos = new List<E_CANDIDATO_SOLICITUD>();

            foreach (GridDataItem item in grdSolicitudes.SelectedItems)
            {
                E_CANDIDATO_SOLICITUD candidatos = new E_CANDIDATO_SOLICITUD();
                candidatos.ID_SOLICITUD = int.Parse(item.GetDataKeyValue("ID_SOLICITUD").ToString());
                candidatos.C_CANDIDATO_NB_EMPLEADO_COMPLETO = item["C_CANDIDATO_NB_EMPLEADO_COMPLETO"].Text.ToString();
                candidatos.C_CANDIDATO_CL_CORREO_ELECTRONICO = item["C_CANDIDATO_CL_CORREO_ELECTRONICO"].Text.ToString();
                listaCandidatos.Add(candidatos);       
            }


            var vResultado = eliminarSolicitud(listaCandidatos);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindow");

        }

        private void EnviarBorrar()
        {
            SolicitudNegocio nSolicitud = new SolicitudNegocio();
            List<E_CANDIDATO_SOLICITUD> listaCandidatos = new List<E_CANDIDATO_SOLICITUD>();

            foreach (GridDataItem item in grdSolicitudes.SelectedItems)
            {
                E_CANDIDATO_SOLICITUD candidatos = new E_CANDIDATO_SOLICITUD();
                candidatos.ID_SOLICITUD = int.Parse(item.GetDataKeyValue("ID_SOLICITUD").ToString());
                candidatos.C_CANDIDATO_NB_EMPLEADO_COMPLETO = item["C_CANDIDATO_NB_EMPLEADO_COMPLETO"].Text.ToString();
                candidatos.C_CANDIDATO_CL_CORREO_ELECTRONICO = item["C_CANDIDATO_CL_CORREO_ELECTRONICO"].Text.ToString();
                listaCandidatos.Add(candidatos);
            }


            var respuesta = EnvioCorreoSolicitudes(listaCandidatos);
            if (respuesta == "0")
            {
                var vResultado = eliminarSolicitud(listaCandidatos);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindow");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Error al enviar correo: " + respuesta + ". No se eliminaron las solicitudes.", E_TIPO_RESPUESTA_DB.ERROR, 400, 250, "onCloseWindow");
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
       
            if (!Page.IsPostBack)
            {
                grdSolicitudes.AllowMultiRowSelection = true;
                if (!String.IsNullOrEmpty(Request.QueryString["mulSel"]))
                {
                    grdSolicitudes.AllowMultiRowSelection = (Request.QueryString["mulSel"] == "1");
                }
            }

            if (Convert.ToString(Request.Form["__EVENTARGUMENT"]) == "BorrarEnviar")
                EnviarBorrar();
            else if (Convert.ToString(Request.Form["__EVENTARGUMENT"]) == "Borrar")
                Borrar();

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
        }
  
        protected void grdSolicitudes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            SolicitudNegocio nSolicitud = new SolicitudNegocio();
            grdSolicitudes.DataSource = nSolicitud.ObtieneSolicitudes();
        }

        protected void btnEliminar_click(object sender, EventArgs e)
        {
            //SolicitudNegocio nSolicitud = new SolicitudNegocio();
            //foreach (GridDataItem item in grdSolicitudes.SelectedItems)
            //{
            //    vIdSolicitud = (int.Parse(item.GetDataKeyValue("ID_SOLICITUD").ToString()));
            //    var vSolicitud = nSolicitud.ObtieneSolicitudes(ID_SOLICITUD: vIdSolicitud).FirstOrDefault();
            //    E_RESULTADO vResultado = nSolicitud.Elimina_K_SOLICITUD(vIdSolicitud, vNbPrograma, vClUsuario);
            //    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            //    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindow");
            //}

            rnTemplate.RadAlert("", 420, 180,"Confirmar","");
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/IDP/Solicitud/Solicitud.aspx");
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in grdSolicitudes.SelectedItems)
            {
                vIdSolicitud = (int.Parse(item.GetDataKeyValue("ID_SOLICITUD").ToString()));
                Response.Redirect("~/IDP/Solicitud.aspx?&ID=" + vIdSolicitud);
            }
        }

        protected void ftrGrdSolicitudes_PreRender(object sender, EventArgs e)
        {
            var menu = ftGrdSolicitudes.FindControl("rfContextMenu") as RadContextMenu;
            menu.DefaultGroupSettings.Height = Unit.Pixel(300);
            menu.EnableAutoScroll = false;
        }

        protected void grdSolicitudes_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdSolicitudes.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdSolicitudes.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdSolicitudes.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdSolicitudes.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdSolicitudes.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void btnActualizarCartera_Click(object sender, EventArgs e)
        {
            List<E_CANDIDATO_SOLICITUD> listaCandidatos = new List<E_CANDIDATO_SOLICITUD>();

            foreach (GridDataItem item in grdSolicitudes.SelectedItems)
            {
                E_CANDIDATO_SOLICITUD candidatos = new E_CANDIDATO_SOLICITUD();
                candidatos.ID_SOLICITUD = int.Parse(item.GetDataKeyValue("ID_SOLICITUD").ToString());
                candidatos.C_CANDIDATO_NB_EMPLEADO_COMPLETO = item["C_CANDIDATO_NB_EMPLEADO_COMPLETO"].Text.ToString();
                candidatos.C_CANDIDATO_CL_CORREO_ELECTRONICO = item["C_CANDIDATO_CL_CORREO_ELECTRONICO"].Text.ToString();
                listaCandidatos.Add(candidatos);
            }

            var respuesta = EnvioCorreoSolicitudesConContrasena(listaCandidatos);
            if (respuesta == "0")
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Proceso exitoso.", E_TIPO_RESPUESTA_DB.SUCCESSFUL, 400, 150, "onCloseWindow");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Error al enviar correo: " + respuesta + ". ", E_TIPO_RESPUESTA_DB.ERROR, 400, 250, "onCloseWindow");
            }
        }
    }
}