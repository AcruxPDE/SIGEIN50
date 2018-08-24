using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.IDP
{
    public partial class ActualizacionCartera : System.Web.UI.Page
    {

        #region Variables
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private bool vEnviar;
        private bool vEliminarTodas;
        private bool vNotificacion;
        #endregion

        #region Funciones

        //private void SeguridadProcesos()
        //{
        //    btnEnviar.Enabled = vEnviar = ContextoUsuario.oUsuario.TienePermiso("A.B.A");
        //    btnEliminarTodas.Enabled = vEliminarTodas = ContextoUsuario.oUsuario.TienePermiso("A.B.B");
        //    btnEnviarNotificacion.Enabled = vNotificacion = ContextoUsuario.oUsuario.TienePermiso("A.B.C");
        //}

        private void DefineGrid()
        {
            //EmpleadoNegocio nEmpleado = new EmpleadoNegocio();
            SolicitudNegocio nSolicitud = new SolicitudNegocio();
            List<SPE_OBTIENE_CARTERA_Result> vSolicitudes;
            vSolicitudes = nSolicitud.Obtener_SOLICITUDES_CARTERA();
            CamposAdicionales cad = new CamposAdicionales();
            DataTable tSolicitudes = cad.camposAdicionales(vSolicitudes, "M_EMPLEADO_XML_CAMPOS_ADICIONALES", grdSolicitudes, "M_EMPLEADO");
            grdSolicitudes.DataSource = tSolicitudes;
        }

        public List<E_CANDIDATO_SOLICITUD> obtenerListaCandidatos(int opcion)
        {
            List<E_CANDIDATO_SOLICITUD> listaCandidatos = new List<E_CANDIDATO_SOLICITUD>();
            switch (opcion)
            {
                case 1://Solo solicitudes seleccionadas en el grid
                    foreach (GridDataItem item in grdSolicitudes.SelectedItems)
                    {

                        E_CANDIDATO_SOLICITUD candidatos = new E_CANDIDATO_SOLICITUD();
                        candidatos.ID_SOLICITUD = int.Parse(item.GetDataKeyValue("ID_SOLICITUD").ToString());
                        //candidatos.ID_SOLICITUD = int.Parse(item["ID_SOLICITUD"].Text.ToString());
                        candidatos.C_CANDIDATO_NB_EMPLEADO_COMPLETO = item["C_CANDIDATO_NB_EMPLEADO_COMPLETO"].Text.ToString();
                        candidatos.C_CANDIDATO_CL_CORREO_ELECTRONICO = item["C_CANDIDATO_CL_CORREO_ELECTRONICO"].Text.ToString();
                        listaCandidatos.Add(candidatos);
                    }
                    break;
                case 2: //Todas
                    SolicitudNegocio nSolicitud = new SolicitudNegocio();
                    var solicitudes = nSolicitud.Obtener_SOLICITUDES_CARTERA();
                    listaCandidatos = (from c in solicitudes
                                       select new E_CANDIDATO_SOLICITUD
                                       {
                                           ID_SOLICITUD = c.ID_SOLICITUD,
                                           C_CANDIDATO_NB_EMPLEADO_COMPLETO = c.C_CANDIDATO_NB_EMPLEADO_COMPLETO,
                                           C_CANDIDATO_CL_CORREO_ELECTRONICO = c.C_CANDIDATO_CL_CORREO_ELECTRONICO,

                                       }).ToList();
                    break;
            }
            return listaCandidatos;
        }

        public E_RESULTADO eliminarSolicitud(List<E_CANDIDATO_SOLICITUD> listaCandidatos)
        {
            XElement xmlElements = new XElement("CANDIDATOS", listaCandidatos.Select(i => new XElement("CANDIDATO", new XAttribute("ID_SOLICITUD", i.ID_SOLICITUD),
                                                                                          new XAttribute("C_CANDIDATO_NB_EMPLEADO_COMPLETO", i.C_CANDIDATO_NB_EMPLEADO_COMPLETO),
                                                                                          new XAttribute("C_CANDIDATO_CL_CORREO_ELECTRONICO", i.C_CANDIDATO_CL_CORREO_ELECTRONICO))));

            SolicitudNegocio nSolicitud = new SolicitudNegocio();
            return nSolicitud.Elimina_K_SOLICITUDES(xmlElements, vClUsuario, vNbPrograma);

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
                    string vUrl = ContextoUsuario.nbHost + "/Logon.aspx?ClProceso=ACTUALIZACIONCARTERA&FlProceso=" + vTokenCartera;
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

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
               
            }
        }

        protected void Page_Init(object sender, System.EventArgs e)
        {
            DefineGrid();
            //SeguridadProcesos();
        }

        protected void grdSolicitudes_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            SolicitudNegocio nSolicitud = new SolicitudNegocio();
            grdSolicitudes.DataSource = nSolicitud.Obtener_SOLICITUDES_CARTERA();
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            var lista = obtenerListaCandidatos(1);
            var respuesta = EnvioCorreoSolicitudes(lista);
            if (respuesta == "0")
            {
                var vResultado = eliminarSolicitud(lista);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindow");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Error al enviar correo: " + respuesta + ". No se eliminaron las solicitudes.", E_TIPO_RESPUESTA_DB.ERROR, 400, 250, "onCloseWindow");
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            var lista = obtenerListaCandidatos(1);
            var respuesta = EnvioCorreoSolicitudes(lista);
            if (respuesta == "0")
            {
                var vResultado = eliminarSolicitud(lista);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindow");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Error al enviar correo: " + respuesta + ". No se eliminaron las solicitudes.", E_TIPO_RESPUESTA_DB.ERROR, 400, 250, "onCloseWindow");
            }
        }

        protected void btnEliminarTodas_Click(object sender, EventArgs e)
        {
            var lista = obtenerListaCandidatos(2);
            var respuesta = EnvioCorreoSolicitudes(lista);
            if (respuesta == "0")
            {
                var vResultado = eliminarSolicitud(lista);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindow");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Error al enviar correo: " + respuesta + ". No se eliminaron las solicitudes.", E_TIPO_RESPUESTA_DB.ERROR, 400, 250, "onCloseWindow");
            }
        }

        protected void btnEnviarNotificacion_Click(object sender, EventArgs e)
        {
            var lista = obtenerListaCandidatos(1);
            var respuesta = EnvioCorreoSolicitudesConContrasena(lista);
            if (respuesta == "0")
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Proceso exitoso.", E_TIPO_RESPUESTA_DB.SUCCESSFUL, 400, 150, "onCloseWindow");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Error al enviar correo: " + respuesta + ". ", E_TIPO_RESPUESTA_DB.ERROR, 400, 250, "onCloseWindow");
            }
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

    }
}