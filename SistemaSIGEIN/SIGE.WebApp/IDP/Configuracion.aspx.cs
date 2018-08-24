using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Entidades.Administracion;
using SIGE.WebApp.Comunes;
using Telerik.Web.UI;
using System.Xml.Linq;
using SIGE.Negocio.Utilerias;
using System.Net.Mail;


namespace SIGE.WebApp.IDP
{
    public partial class Configuracion : System.Web.UI.Page
    {

        #region Variables

        private string usuario;
        private string programa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        String vTipoTransaccion = "";

        public string vIdAjaxPanel;

        private List<E_PRUEBA_SECCION_TIEMPO> vSeccionPruebas
        {
            get { return (List<E_PRUEBA_SECCION_TIEMPO>)ViewState["vsSeccionPruebas"]; }
            set { ViewState["vsSeccionPruebas"] = value; }
        }

        private int vIdSeccionPrueba
        {
            get { return (int)ViewState["vsIdSeccionPrueba"]; }
            set { ViewState["vsIdSeccionPrueba"] = value; }
        }
        private int vCmbSeleccionado
        {
            get { return (int)ViewState["vsvCmbSeleccionado"]; }
            set { ViewState["vsvCmbSeleccionado"] = value; }
        }

        private List<E_CORREO_ELECTRONICO> vCorreosNombres
        {
            get { return (List<E_CORREO_ELECTRONICO>)ViewState["vsCorreosNombres"]; }
            set { ViewState["vsCorreosNombres"] = value; }
        }

        //private List<E_CORREO_ELECTRONICO> vCorreosNombresRrhh
        //{
        //    get { return (List<E_CORREO_ELECTRONICO>)ViewState["vCorreosNombresRrhh"]; }
        //    set { ViewState["vCorreosNombresRrhh"] = value; }
        //}

        PruebasNegocio nPrueba = new PruebasNegocio();

        #endregion

        #region Funciones

        protected void CargarDatosMensajes()
        {
            btVerAviso.Checked = ContextoApp.IDP.MensajePrivacidad.fgVisible;
            txbPrivacidad.Content = ContextoApp.IDP.MensajePrivacidad.dsMensaje;
            txbSolicitudBienvenida.Content = ContextoApp.IDP.MensajeBienvenidaEmpleo.dsMensaje;
            // txbNotificaciones.Text = ContextoApp.IDP.MensajeRequisicionesEmpleo.dsMensaje;
            btVerPiePagina.Checked = ContextoApp.IDP.MensajePiePagina.fgVisible;
            txbPiePagina.Content = ContextoApp.IDP.MensajePiePagina.dsMensaje;
            reMensajeCorreo.Content = ContextoApp.IDP.MensajeCorreoSolicitud.dsMensaje;
            txbAsunto.Content = ContextoApp.IDP.MensajeAsutoCorreo.dsMensaje;
            txbCuerpo.Content = ContextoApp.IDP.MensajeCuerpoCorreo.dsMensaje;
            txbPruebaBienvenida.Content = ContextoApp.IDP.MensajeBienvenidaPrueba.dsMensaje;
            txbPruebaAgradecimiento.Content = ContextoApp.IDP.MensajeDespedidaPrueba.dsMensaje;
        }

        protected void CargarDatosCartera()
        {
            txbNotificacion.Content = ContextoApp.IDP.MensajeActualizacionPeriodica.dsNotificacion.dsMensaje;
            btNotificar.Checked = ContextoApp.IDP.MensajeActualizacionPeriodica.dsNotificacion.fgVisible;
            btNotificarCandidatos.Checked = ContextoApp.IDP.MensajeActualizacionPeriodica.fgVisible;
            txPeriodicidad.Text = ContextoApp.IDP.MensajeActualizacionPeriodica.noPeriodicidad.ToString();
            cmbTipoPeriodicidad.Text = ContextoApp.IDP.MensajeActualizacionPeriodica.dsTipoPeriodicidad;
            btnProcesoActivo.Checked = ContextoApp.IDP.MensajeActualizacionPeriodica.fgEstatus;
            cmbTipoPeriodicidad.DataBind();
            vCorreosNombres = ContextoApp.IDP.MensajeActualizacionPeriodica.lstCorreos.Select(s => new E_CORREO_ELECTRONICO { NB_DISPLAY = s.DisplayName, NB_MAIL = s.Address }).ToList();

        }

        //protected void CargarNotificacionRequisicion()
        //{
        //    if (ContextoApp.IDP.NotificacionRrhh.lstCorreosRequisiciones != null)
        //    {
        //        rtbNombreRrhh.Text = ContextoApp.IDP.NotificacionRrhh.lstCorreosRequisiciones.DisplayName;
        //        rtbCorreoRrhh.Text = ContextoApp.IDP.NotificacionRrhh.lstCorreosRequisiciones.Address;
        //        hfIdAutorizaPuestoRequisicion.Value = ContextoApp.IDP.NotificacionRrhh.idEmpleadoAutorizaRequisicion.ToString();
        //    }
        //    //vCorreosNombresRrhh = ContextoApp.IDP.NotificacionRrhh.lstCorreosRequisiciones.Select(s => new E_CORREO_ELECTRONICO { NB_DISPLAY = s.DisplayName, NB_MAIL = s.Address }).ToList();
        //}

        private void CargarDatosPsicometria()
        {
            chkMostrarConometro.Checked = ContextoApp.IDP.ConfiguracionPsicometria.FgMostrarCronometro;
        }
        private void CargarDatosIntegracion()
        {
            chkConsultasParciales.Checked = ContextoApp.IDP.ConfiguracionIntegracion.FgConsultasParciales;
            chkIgnorarCompetencias.Checked = ContextoApp.IDP.ConfiguracionIntegracion.FgIgnorarCompetencias;
        }

        protected void GuardarCartera()
        {
            ContextoApp.IDP.MensajeActualizacionPeriodica.dsNotificacion.fgVisible = btNotificar.Checked;
            ContextoApp.IDP.MensajeActualizacionPeriodica.dsNotificacion.dsMensaje = txbNotificacion.Content;
            ContextoApp.IDP.MensajeActualizacionPeriodica.noPeriodicidad = int.Parse(txPeriodicidad.Text);
            ContextoApp.IDP.MensajeActualizacionPeriodica.dsTipoPeriodicidad = cmbTipoPeriodicidad.Text;
            ContextoApp.IDP.MensajeActualizacionPeriodica.fgVisible = btNotificarCandidatos.Checked;
            ContextoApp.IDP.MensajeActualizacionPeriodica.lstCorreos = vCorreosNombres.Select(s => new MailAddress(s.NB_MAIL, s.NB_DISPLAY)).ToList();
            ContextoApp.IDP.MensajeActualizacionPeriodica.fgEstatus = btnProcesoActivo.Checked;
            E_RESULTADO vResultado = ContextoApp.SaveConfiguration(usuario, programa);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
        }

        private void GuardarDatosPsicometria()
        {
            ContextoApp.IDP.ConfiguracionPsicometria.FgMostrarCronometro = chkMostrarConometro.Checked;

            E_RESULTADO vResultado = ContextoApp.SaveConfiguration(usuario, programa);

            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.ERROR)
            {
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
            }
        }

        private void GuardarDatosIntegracion()
        {
            ContextoApp.IDP.ConfiguracionIntegracion.FgConsultasParciales = chkConsultasParciales.Checked;
            ContextoApp.IDP.ConfiguracionIntegracion.FgIgnorarCompetencias = chkIgnorarCompetencias.Checked;
            
            E_RESULTADO vResultado = ContextoApp.SaveConfiguration(usuario, programa);

            //if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.ERROR)
            //{
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
            //}
        }
        private void ObtieneTiempos()
        {
            if (txtAdapatacion.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "ADAPTACION-0001", NO_TIEMPO = int.Parse(txtAdapatacion.Text) });
            if (txtnMinutosMental1.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "APTITUD-1-0001", NO_TIEMPO = int.Parse(txtnMinutosMental1.Text) });
            if (txtnMinutosMental2.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "APTITUD-1-0002", NO_TIEMPO = int.Parse(txtnMinutosMental2.Text) });
            if (txtnMinutosMental3.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "APTITUD-1-0003", NO_TIEMPO = int.Parse(txtnMinutosMental3.Text) });
            if (txtnMinutosMental4.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "APTITUD-1-0004", NO_TIEMPO = int.Parse(txtnMinutosMental4.Text) });
            if (txtnMinutosMental5.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "APTITUD-1-0005", NO_TIEMPO = int.Parse(txtnMinutosMental5.Text) });
            if (txtnMinutosMental6.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "APTITUD-1-0006", NO_TIEMPO = int.Parse(txtnMinutosMental6.Text) });
            if (txtnMinutosMental7.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "APTITUD-1-0007", NO_TIEMPO = int.Parse(txtnMinutosMental7.Text) });
            if (txtnMinutosMental8.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "APTITUD-1-0008", NO_TIEMPO = int.Parse(txtnMinutosMental8.Text) });
            if (txtnMinutosMental9.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "APTITUD-1-0009", NO_TIEMPO = int.Parse(txtnMinutosMental9.Text) });
            if (txtnMinutosMental10.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "APTITUD-1-0010", NO_TIEMPO = int.Parse(txtnMinutosMental10.Text) });
            if (txtnMentalDos.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "APTITUD-2-0001", NO_TIEMPO = int.Parse(txtnMentalDos.Text) });
            if (txtnMinutosIntereses.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "INTERES-0001", NO_TIEMPO = int.Parse(txtnMinutosIntereses.Text) });
            if (txnMinutosLaboral1.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "LABORAL-1-0001", NO_TIEMPO = int.Parse(txnMinutosLaboral1.Text) });
            if (txtnMinutosLaboral2.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "LABORAL-2-0001", NO_TIEMPO = int.Parse(txtnMinutosLaboral2.Text) });
            if (txtOrt1.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "ORTOGRAFIA-1-0001", NO_TIEMPO = int.Parse(txtOrt1.Text) });
            if (txtOrt2.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "ORTOGRAFIA-2-0001", NO_TIEMPO = int.Parse(txtOrt2.Text) });
            if (txtOrt3.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "ORTOGRAFIA-3-0001", NO_TIEMPO = int.Parse(txtOrt3.Text) });
            if (txtnMinutosPensamiento.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "PENSAMIENTO-0001", NO_TIEMPO = int.Parse(txtnMinutosPensamiento.Text) });
            if (txtRedaccion.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "REDACCION-0001", NO_TIEMPO = int.Parse(txtRedaccion.Text) });
            if (txtTecnica.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "TECNICAPC-0001", NO_TIEMPO = int.Parse(txtTecnica.Text) });
            if (txtTiva.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "TIVA-0001", NO_TIEMPO = int.Parse(txtTiva.Text) });
            if (txtIngles1.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "INGLES-0001", NO_TIEMPO = int.Parse(txtIngles1.Text) });
            if (txtIngles2.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "INGLES-0002", NO_TIEMPO = int.Parse(txtIngles2.Text) });
            if (txtIngles3.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "INGLES-0003", NO_TIEMPO = int.Parse(txtIngles3.Text) });
            if (txtIngles4.Text != "")
                vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { CL_PRUEBA_SECCION = "INGLES-0004", NO_TIEMPO = int.Parse(txtIngles4.Text) });
        }


        private void CalculaTotalTiempo()
        {
            int vTotalTiempos = 0;
            vTotalTiempos = vTotalTiempos + int.Parse(txtAdapatacion.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtnMinutosMental1.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtnMinutosMental2.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtnMinutosMental3.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtnMinutosMental4.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtnMinutosMental5.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtnMinutosMental6.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtnMinutosMental7.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtnMinutosMental8.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtnMinutosMental9.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtnMinutosMental10.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtnMentalDos.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtnMinutosIntereses.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txnMinutosLaboral1.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtnMinutosLaboral2.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtOrt1.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtOrt2.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtOrt3.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtnMinutosPensamiento.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtRedaccion.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtTecnica.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtTiva.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtIngles1.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtIngles2.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtIngles3.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtIngles4.Text);

            TimeSpan result = TimeSpan.FromMinutes(vTotalTiempos);
            string fromTimeString = result.ToString("hh':'mm");

            txtAjuste.Value = fromTimeString + " Horas";
        }

        private void CalcularTotalTiempoMental()
        {
            int vTotalTiempos = 0;
            vTotalTiempos = vTotalTiempos + int.Parse(txtnMinutosMental1.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtnMinutosMental2.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtnMinutosMental3.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtnMinutosMental4.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtnMinutosMental5.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtnMinutosMental6.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtnMinutosMental7.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtnMinutosMental8.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtnMinutosMental9.Text);
            vTotalTiempos = vTotalTiempos + int.Parse(txtnMinutosMental10.Text);
            TimeSpan result = TimeSpan.FromMinutes(vTotalTiempos);
            string fromTimeString = result.ToString("hh':'mm");

            txtTotalmental.Value = fromTimeString + " Minutos";
        }

        //public E_RESULTADO enviarEliminarSolicitudes()
        //{
        //    GuardarCartera();
        //    var lista = obtenerListaCandidatos();
        //    var respuestaCorreos = EnvioCorreoSolicitudes(lista);
        //    var respuesta = eliminarSolicitud(lista);
        //    return respuesta;
        //}

        //public List<E_CANDIDATO_SOLICITUD> obtenerListaCandidatos()
        //{
        //    List<E_CANDIDATO_SOLICITUD> listaCandidatos = new List<E_CANDIDATO_SOLICITUD>();
        //    SolicitudNegocio nSolicitud = new SolicitudNegocio();
        //    var solicitudes = nSolicitud.Obtener_SOLICITUDES_CARTERA_A_ELIMINAR();
        //    listaCandidatos = (from c in solicitudes
        //                       select new E_CANDIDATO_SOLICITUD
        //                       {
        //                           ID_SOLICITUD = c.ID_SOLICITUD,
        //                           C_CANDIDATO_NB_EMPLEADO_COMPLETO = c.C_CANDIDATO_NB_EMPLEADO_COMPLETO,
        //                           C_CANDIDATO_CL_CORREO_ELECTRONICO = c.C_CANDIDATO_CL_CORREO_ELECTRONICO,

        //                       }).ToList();
        //    return listaCandidatos;
        //}

        //public E_RESULTADO eliminarSolicitud(List<E_CANDIDATO_SOLICITUD> listaCandidatos)
        //{
        //    XElement xmlElements = new XElement("CANDIDATOS", listaCandidatos.Select(i => new XElement("CANDIDATO", new XAttribute("ID_SOLICITUD", i.ID_SOLICITUD),
        //                                                                                  new XAttribute("C_CANDIDATO_NB_EMPLEADO_COMPLETO", i.C_CANDIDATO_NB_EMPLEADO_COMPLETO),
        //                                                                                  new XAttribute("C_CANDIDATO_CL_CORREO_ELECTRONICO", i.C_CANDIDATO_CL_CORREO_ELECTRONICO))));
        //    SolicitudNegocio nSolicitud = new SolicitudNegocio();
        //    return nSolicitud.Elimina_K_SOLICITUDES(xmlElements, "Proceso automatico", "Proceso automatico");
        //}

        private string EnvioCorreoSolicitudes(List<E_CANDIDATO_SOLICITUD> listaCandidatos)
        {
            var respuesta = "0";
            foreach (E_CANDIDATO_SOLICITUD item in listaCandidatos)
            {
                Mail mail = new Mail(ContextoApp.mailConfiguration);
                bool tieneEmail = false;
                if (!String.IsNullOrEmpty(item.C_CANDIDATO_CL_CORREO_ELECTRONICO) && item.C_CANDIDATO_CL_CORREO_ELECTRONICO != "&nbsp;" && ContextoApp.IDP.MensajeActualizacionPeriodica.fgVisible)
                {

                    mail.addToAddress(item.C_CANDIDATO_CL_CORREO_ELECTRONICO, null);
                    tieneEmail = true;
                }
                //AGREGAR CORREOS DE RR.HH
                if (ContextoApp.IDP.MensajeActualizacionPeriodica.dsNotificacion.fgVisible)
                {
                    var direccionesRRHH = ContextoApp.IDP.MensajeActualizacionPeriodica.lstCorreos;
                    foreach (var dirrrhh in direccionesRRHH)
                    {
                        mail.addToAddress(dirrrhh.Address, dirrrhh.DisplayName);
                        tieneEmail = true;
                    }
                }
                if (tieneEmail)
                {
                    respuesta = mail.Send("Candidatos", String.Format("Estimado(a) {0},<br/><br/>{1}<br/><br/>Saludos cordiales.", item.C_CANDIDATO_NB_EMPLEADO_COMPLETO, ContextoApp.IDP.MensajeActualizacionPeriodica.dsNotificacion.dsMensaje));
                    if (respuesta != "0")
                    {
                        return respuesta;
                    }
                }
            }
            return respuesta;
        }

        //protected void GuardarNotificacionRequisicion()
        //{
        //    if (hfIdAutorizaPuestoRequisicion.Value != "")
        //    {
        //        ContextoApp.IDP.NotificacionRrhh.lstCorreosRequisiciones = new MailAddress(rtbCorreoRrhh.Text, rtbNombreRrhh.Text);
        //        ContextoApp.IDP.NotificacionRrhh.idEmpleadoAutorizaRequisicion = int.Parse(hfIdAutorizaPuestoRequisicion.Value);

        //        E_RESULTADO vResultado = ContextoApp.SaveConfiguration(usuario, programa);
        //        string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
        //        UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);   
        //    }
        //    else
        //    {
        //        UtilMensajes.MensajeResultadoDB(rwmMensaje, "Selecciona una persona para autorizar los puestos creados desde la requisición", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: null);
        //    }
        //}

        //private bool ValidarTiemposPruebas()
        //{

        //    string vMensaje = "";
        //    string vMensajeInicial = "Las siguientes secciones tienen un tiempo menor al mínimo establecido: <br/> <br/>";
        //    string vMensajeFinal = "<br/>Revisa los tiempos antes de guardar.";

        //    string vNombre = "";
        //    int vNoTiempoMinimo = 0;

        //    //foreach (GridDataItem item in grdAjusteTiempo.MasterTableView.Items)
        //    //{
        //    //    vNoTiempoMinimo = (int.Parse(item.GetDataKeyValue("NO_TIEMPO_MINIMO_ESTANDAR").ToString()));
        //    //    vNombre = item.GetDataKeyValue("NB_PRUEBA_SECCION").ToString();
        //    //    RadNumericTextBox vNoTiempoNew = (RadNumericTextBox)item.FindControl("txnMinutos");

        //    //    if (!vNoTiempoNew.Text.Equals(""))
        //    //    {
        //    //        if (int.Parse(vNoTiempoNew.Text) < vNoTiempoMinimo)
        //    //        {
        //    //            vMensaje = vMensaje + " " + vNombre + " <br/>";
        //    //        }
        //    //    }
        //    //}

        //    if (!string.IsNullOrEmpty(vMensaje))
        //    {
        //        vMensaje = vMensajeInicial + vMensaje + vMensajeFinal;
        //        UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, E_TIPO_RESPUESTA_DB.WARNING, 400, 300, null);
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }

        //}

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            usuario = ContextoUsuario.oUsuario.CL_USUARIO;
            programa = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                CargarDatosMensajes();
                CargarDatosCartera();
                //CargarNotificacionRequisicion();
                CargarDatosPsicometria();
                CargarDatosIntegracion();


                vCmbSeleccionado = 0;
                PruebasNegocio nPruebaSeccion = new PruebasNegocio();
                var vPrueba = nPruebaSeccion.Obtener_C_PRUEBA();
                //cmbPrueba.DataSource = vPrueba;
                //cmbPrueba.DataTextField = "NB_PRUEBA";
                //cmbPrueba.DataValueField = "ID_PRUEBA";
                //cmbPrueba.DataBind();
                //vCorreosNombres = new List<E_CORREO_ELECTRONICO>();
                // vCorreosNombresRrhh = new List<E_CORREO_ELECTRONICO>();
                List<SPE_OBTIENE_C_PRUEBA_SECCION_Result> vListaPruebas = new List<SPE_OBTIENE_C_PRUEBA_SECCION_Result>();
                vListaPruebas = nPrueba.Obtener_C_PRUEBA_SECCION();
                int? vTiempoTotal = 0;
                int? vTotalMental = 0;
                foreach (var item in vListaPruebas)
                {
                    if (item.CL_PRUEBA_SECCION != "ENTREVISTA-0001")
                    {
                        vTiempoTotal = vTiempoTotal + item.NO_TIEMPO;
                       
                    }
                    switch (item.CL_PRUEBA_SECCION)
                    {
                        case "ADAPTACION-0001":
                            txtAdapatacion.Text = item.NO_TIEMPO.ToString();
                            break;
                        case "APTITUD-1-0001":
                            txtnMinutosMental1.Text = item.NO_TIEMPO.ToString();
                            vTotalMental = vTotalMental + item.NO_TIEMPO;
                            break;
                        case "APTITUD-1-0002":
                            txtnMinutosMental2.Text = item.NO_TIEMPO.ToString();
                            vTotalMental = vTotalMental + item.NO_TIEMPO;
                            break;
                        case "APTITUD-1-0003":
                            txtnMinutosMental3.Text = item.NO_TIEMPO.ToString();
                            vTotalMental = vTotalMental + item.NO_TIEMPO;
                            break;
                        case "APTITUD-1-0004":
                            txtnMinutosMental4.Text = item.NO_TIEMPO.ToString();
                            vTotalMental = vTotalMental + item.NO_TIEMPO;
                            break;
                        case "APTITUD-1-0005":
                            txtnMinutosMental5.Text = item.NO_TIEMPO.ToString();
                            vTotalMental = vTotalMental + item.NO_TIEMPO;
                            break;
                        case "APTITUD-1-0006":
                            txtnMinutosMental6.Text = item.NO_TIEMPO.ToString();
                            vTotalMental = vTotalMental + item.NO_TIEMPO;
                            break;
                        case "APTITUD-1-0007":
                            txtnMinutosMental7.Text = item.NO_TIEMPO.ToString();
                            vTotalMental = vTotalMental + item.NO_TIEMPO;
                            break;
                        case "APTITUD-1-0008":
                            txtnMinutosMental8.Text = item.NO_TIEMPO.ToString();
                            vTotalMental = vTotalMental + item.NO_TIEMPO;
                            break;
                        case "APTITUD-1-0009":
                            txtnMinutosMental9.Text = item.NO_TIEMPO.ToString();
                            vTotalMental = vTotalMental + item.NO_TIEMPO;
                            break;
                        case "APTITUD-1-0010":
                            txtnMinutosMental10.Text = item.NO_TIEMPO.ToString();
                            vTotalMental = vTotalMental + item.NO_TIEMPO;
                            break;
                        case "APTITUD-2-0001":
                            txtnMentalDos.Text = item.NO_TIEMPO.ToString();
                            break;
                        case "INTERES-0001":
                            txtnMinutosIntereses.Text = item.NO_TIEMPO.ToString();
                            break;
                        case "LABORAL-1-0001":
                            txnMinutosLaboral1.Text = item.NO_TIEMPO.ToString();
                            break;
                        case "LABORAL-2-0001":
                            txtnMinutosLaboral2.Text = item.NO_TIEMPO.ToString();
                            break;
                        case "ORTOGRAFIA-1-0001":
                            txtOrt1.Text = item.NO_TIEMPO.ToString();
                            break;
                        case "ORTOGRAFIA-2-0001":
                            txtOrt2.Text = item.NO_TIEMPO.ToString();
                            break;
                        case "ORTOGRAFIA-3-0001":
                            txtOrt3.Text = item.NO_TIEMPO.ToString();
                            break;
                        case "PENSAMIENTO-0001":
                            txtnMinutosPensamiento.Text = item.NO_TIEMPO.ToString();
                            break;
                        case "REDACCION-0001":
                            txtRedaccion.Text = item.NO_TIEMPO.ToString();
                            break;
                        case "TECNICAPC-0001":
                            txtTecnica.Text = item.NO_TIEMPO.ToString();
                            break;
                        case "TIVA-0001":
                            txtTiva.Text = item.NO_TIEMPO.ToString();
                            break;
                        case "INGLES-0001":
                            txtIngles1.Text = item.NO_TIEMPO.ToString();
                            break;
                        case "INGLES-0002":
                            txtIngles2.Text = item.NO_TIEMPO.ToString();
                            break;
                        case "INGLES-0003":
                            txtIngles3.Text = item.NO_TIEMPO.ToString();
                            break;
                        case "INGLES-0004":
                            txtIngles4.Text = item.NO_TIEMPO.ToString();
                            break;
                    }

                    //E_PRUEBA_SECCION_TIEMPO f = new E_PRUEBA_SECCION_TIEMPO
                    //{
                    //    ID_PRUEBA_SECCIONES = item.ID_PRUEBA_SECCION,
                    //    CL_PRUEBA_SECCION = item.CL_PRUEBA_SECCION,
                    //    NO_TIEMPO = (int)item.NO_TIEMPO
                    //};
                    //vListaTiempoPruebas.Add(f);
                }
                int val = (int)vTiempoTotal;
                TimeSpan result = TimeSpan.FromMinutes(val);
                string fromTimeString = result.ToString("hh':'mm");

                txtAjuste.Value = fromTimeString + " Horas";
                int valMental = (int)vTotalMental;
                TimeSpan resultMental = TimeSpan.FromMinutes(valMental);
                string fromTimeStringMental = resultMental.ToString("hh':'mm");
                txtTotalmental.Value = fromTimeStringMental + " Minutos";
                //  txtTotalOriginal.Value = vTiempoReal.ToString();
            }
            else
            {
                PruebasNegocio nPrueba = new PruebasNegocio();
                //grdAjusteTiempo.DataSource = nPrueba.Obtener_C_PRUEBA_SECCION(pIdPrueba: vCmbSeleccionado);
            }

            string vClDefaultTab = Request.QueryString["tabCl"];

            if (!String.IsNullOrWhiteSpace(vClDefaultTab))
                switch (vClDefaultTab)
                {
                    case "DEPURACION":
                        rtsConfiguracion.SelectedIndex = 2;
                        rmpConfiguracion.SelectedIndex = 2;
                        break;
                    default:
                        break;
                }

            vSeccionPruebas = new List<E_PRUEBA_SECCION_TIEMPO>();
        }

        //protected void grdAjusteTiempo_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        //{
        //    var vPruebas = nPrueba.Obtener_C_PRUEBA_SECCION();
        //    grdAjusteTiempo.DataSource = vPruebas;
        //}

        protected void cmbPrueba_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PruebasNegocio nPrueba = new PruebasNegocio();
            vCmbSeleccionado = int.Parse(e.Value);
            //   grdAjusteTiempo.DataSource = nPrueba.Obtener_C_PRUEBA_SECCION(pIdPrueba: vCmbSeleccionado);
            //  grdAjusteTiempo.DataBind();
        }

        protected void btnGuardarTiempo_Click(object sender, EventArgs e)
        {
            //String vNbPruebaCmb = cmbPrueba.Text;
            //int vIdSeccion = 0;

            //if (!ValidarTiemposPruebas())
            //{
            //    return;
            //}

            //foreach (GridDataItem item in grdAjusteTiempo.MasterTableView.Items)
            //{
            //    vIdSeccion = (int.Parse(item.GetDataKeyValue("ID_PRUEBA_SECCION").ToString()));
            //    RadNumericTextBox vNoTiempoNew = (RadNumericTextBox)item.FindControl("txnMinutos");
            //    if (!vNoTiempoNew.Text.Equals(""))
            //    {
            //        vSeccionPruebas.Add(new E_PRUEBA_SECCION_TIEMPO { ID_PRUEBA_SECCIONES = vIdSeccion, NO_TIEMPO = int.Parse(vNoTiempoNew.Text) });
            //        vTipoTransaccion = E_TIPO_OPERACION_DB.A.ToString();
            //    }
            //}

            ObtieneTiempos();

            var vXelements = vSeccionPruebas.Select(x =>
                                           new XElement("TIEMPO",
                                           new XAttribute("CL_PRUEBA_SECCION", x.CL_PRUEBA_SECCION),
                                           new XAttribute("NO_TIEMPO", x.NO_TIEMPO)));

            XElement CambioNoTiempo = new XElement("TIEMPOS", vXelements);

            PruebasNegocio nPruebas = new PruebasNegocio();

            E_RESULTADO vResultado = nPruebas.ActualizaTiempoPruebaSeccion(CambioNoTiempo.ToString(), null, usuario, programa);

            GuardarDatosPsicometria();

            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, null);
            Response.Redirect(Request.RawUrl);
            //grdAjusteTiempo.Rebind();
        }

        protected void grdCorreos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdCorreos.DataSource = vCorreosNombres;
        }

        protected void btnGuardarMensajes_Click(object sender, EventArgs e)
        {
            ContextoApp.IDP.MensajePrivacidad.fgVisible = btVerAviso.Checked;
            ContextoApp.IDP.MensajePrivacidad.dsMensaje = txbPrivacidad.Content;
            ContextoApp.IDP.MensajeBienvenidaEmpleo.dsMensaje = txbSolicitudBienvenida.Content;
            //  ContextoApp.IDP.MensajeRequisicionesEmpleo.dsMensaje = txbNotificaciones.Text;
            ContextoApp.IDP.MensajePiePagina.fgVisible = btVerPiePagina.Checked;
            ContextoApp.IDP.MensajePiePagina.dsMensaje = txbPiePagina.Content;
            ContextoApp.IDP.MensajeCorreoSolicitud.dsMensaje = reMensajeCorreo.Content;
            ContextoApp.IDP.MensajeAsutoCorreo.dsMensaje = txbAsunto.Content;
            ContextoApp.IDP.MensajeCuerpoCorreo.dsMensaje = txbCuerpo.Content;
            ContextoApp.IDP.MensajeBienvenidaPrueba.dsMensaje = txbPruebaBienvenida.Content;
            ContextoApp.IDP.MensajeDespedidaPrueba.dsMensaje = txbPruebaAgradecimiento.Content;

            E_RESULTADO vResultado = ContextoApp.SaveConfiguration(usuario, programa);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
        }

        protected void btnAgregarCorreo_Click(object sender, EventArgs e)
        {
            if (txbCorreo.Text != "" & txbNombre.Text != "")
            {
                vCorreosNombres.Add(new E_CORREO_ELECTRONICO { NB_MAIL = txbCorreo.Text, NB_DISPLAY = txbNombre.Text });
                grdCorreos.DataSource = vCorreosNombres;
                grdCorreos.Rebind();
                txbCorreo.Text = "";
                txbNombre.Text = "";
            }
        }

        protected void btnEliminarCorreo_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in grdCorreos.SelectedItems)
            {
                string vItCorreo = item.GetDataKeyValue("NB_MAIL").ToString();
                vCorreosNombres.RemoveAll(w => w.NB_MAIL == vItCorreo);
            }
            grdCorreos.DataSource = vCorreosNombres;
            grdCorreos.Rebind();
        }

        protected void btnGuardarCartera_Click(object sender, EventArgs e)
        {
            GuardarCartera();
        }

        public E_RESULTADO enviarEliminarSolicitudes()
        {
            GuardarCartera();
            var lista = obtenerListaCandidatos();
            var respuestaCorreos = EnvioCorreoSolicitudes(lista);
            var respuesta = eliminarSolicitud(lista);
            return respuesta;
        }

        public List<E_CANDIDATO_SOLICITUD> obtenerListaCandidatos()
        {
            List<E_CANDIDATO_SOLICITUD> listaCandidatos = new List<E_CANDIDATO_SOLICITUD>();
            SolicitudNegocio nSolicitud = new SolicitudNegocio();
            var solicitudes = nSolicitud.Obtener_SOLICITUDES_CARTERA_A_ELIMINAR();
            listaCandidatos = (from c in solicitudes
                               select new E_CANDIDATO_SOLICITUD
                               {
                                   ID_SOLICITUD = c.ID_SOLICITUD,
                                   C_CANDIDATO_NB_EMPLEADO_COMPLETO = c.C_CANDIDATO_NB_EMPLEADO_COMPLETO,
                                   C_CANDIDATO_CL_CORREO_ELECTRONICO = c.C_CANDIDATO_CL_CORREO_ELECTRONICO,

                               }).ToList();
            return listaCandidatos;
        }

        public E_RESULTADO eliminarSolicitud(List<E_CANDIDATO_SOLICITUD> listaCandidatos)
        {
            XElement xmlElements = new XElement("CANDIDATOS", listaCandidatos.Select(i => new XElement("CANDIDATO", new XAttribute("ID_SOLICITUD", i.ID_SOLICITUD),
                                                                                          new XAttribute("C_CANDIDATO_NB_EMPLEADO_COMPLETO", i.C_CANDIDATO_NB_EMPLEADO_COMPLETO),
                                                                                          new XAttribute("C_CANDIDATO_CL_CORREO_ELECTRONICO", i.C_CANDIDATO_CL_CORREO_ELECTRONICO))));
            SolicitudNegocio nSolicitud = new SolicitudNegocio();
            return nSolicitud.Elimina_K_SOLICITUDES(xmlElements, "Proceso automatico", "Proceso automatico");
        }

        //private string EnvioCorreoSolicitudes(List<E_CANDIDATO_SOLICITUD> listaCandidatos)
        //{
        //    var respuesta = "0";
        //    foreach (E_CANDIDATO_SOLICITUD item in listaCandidatos)
        //    {
        //        Mail mail = new Mail(ContextoApp.mailConfiguration);
        //        bool tieneEmail = false;
        //        if (!String.IsNullOrEmpty(item.C_CANDIDATO_CL_CORREO_ELECTRONICO) && item.C_CANDIDATO_CL_CORREO_ELECTRONICO != "&nbsp;" && ContextoApp.IDP.MensajeActualizacionPeriodica.fgVisible)
        //        {

        //            mail.addToAddress(item.C_CANDIDATO_CL_CORREO_ELECTRONICO, null);
        //            tieneEmail = true;
        //        }
        //            //AGREGAR CORREOS DE RR.HH
        //            if (ContextoApp.IDP.MensajeActualizacionPeriodica.dsNotificacion.fgVisible)
        //            {
        //                var direccionesRRHH = ContextoApp.IDP.MensajeActualizacionPeriodica.lstCorreos;
        //                foreach (var dirrrhh in direccionesRRHH)
        //                {
        //                    mail.addToAddress(dirrrhh.Address, dirrrhh.DisplayName);
        //                    tieneEmail = true;
        //                }
        //            }
        //            if (tieneEmail)
        //            {
        //                respuesta = mail.Send("Candidatos", String.Format("Estimado(a) {0},<br/><br/>{1}<br/><br/>Saludos cordiales.", item.C_CANDIDATO_NB_EMPLEADO_COMPLETO, ContextoApp.IDP.MensajeActualizacionPeriodica.dsNotificacion.dsMensaje));
        //                if (respuesta != "0")
        //                {
        //                    return respuesta;
        //                }
        //            }            
        //      }
        //    return respuesta;
        //}

        protected void dgvBateria_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            //PruebasNegocio pruebas = new PruebasNegocio();
            //dgvBateria.DataSource = pruebas.ObtieneBateria();
            SolicitudNegocio nSolicitudes = new SolicitudNegocio();
            dgvBateria.DataSource = nSolicitudes.ObtieneCandidatosBaterias();
        }

        protected void dgvBateria_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void btnEjecutarLimpiaCartera_Click(object sender, EventArgs e)
        {
            enviarEliminarSolicitudes();
        }

        protected void rbAgregarRrhh_Click(object sender, EventArgs e)
        {
            //if (rtbCorreoRrhh.Text != "" & rtbNombreRrhh.Text != "")
            //{
            //    {
            //        vCorreosNombresRrhh.Add(new E_CORREO_ELECTRONICO { NB_MAIL = rtbCorreoRrhh.Text, NB_DISPLAY = rtbNombreRrhh.Text });
            //        rgNotificacionRrhh.DataSource = vCorreosNombresRrhh;
            //        rgNotificacionRrhh.Rebind();
            //        rtbCorreoRrhh.Text = "";
            //        rtbNombreRrhh.Text = "";
            //    }
            //}
        }

        protected void rgNotificacionRrhh_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //rgNotificacionRrhh.DataSource = vCorreosNombresRrhh;
        }

        protected void rbEliminarRrhh_Click(object sender, EventArgs e)
        {
            //foreach (GridDataItem item in rgNotificacionRrhh.SelectedItems)
            //{
            //    string vItCorreo = item.GetDataKeyValue("NB_MAIL").ToString();
            //    vCorreosNombresRrhh.RemoveAll(w => w.NB_MAIL == vItCorreo);
            //}
            //rgNotificacionRrhh.DataSource = vCorreosNombresRrhh;
            //rgNotificacionRrhh.Rebind();
        }

        //protected void rbGuardarNotificacionRrhh_Click(object sender, EventArgs e)
        //{
        //    GuardarNotificacionRequisicion();
        //}

        protected void rgBaterias_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            SolicitudNegocio nSolicitudes = new SolicitudNegocio();
            rgBaterias.DataSource = nSolicitudes.ObtieneCandidatosBaterias();
            //PruebasNegocio pruebas = new PruebasNegocio();
            //rgBaterias.DataSource = pruebas.ObtieneBateria();
        }

        protected void btnReinicializar_Click(object sender, EventArgs e)
        {

            PruebasNegocio nPruebas = new PruebasNegocio();

            E_RESULTADO vResultado = nPruebas.ActualizaTiempoPruebaSeccion(null, "R", usuario, programa);

            GuardarDatosPsicometria();

            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, null);
            Response.Redirect(Request.RawUrl);

        }

        protected void txnMinutosLaboral1_TextChanged(object sender, EventArgs e)
        {
            CalculaTotalTiempo();
        }

        protected void txtnMinutosMental1_TextChanged(object sender, EventArgs e)
        {
            CalculaTotalTiempo();
            CalcularTotalTiempoMental();
        }

        protected void btnGuardarIntegracion_Click(object sender, EventArgs e)
        {
            GuardarDatosIntegracion();
        }

    }
}