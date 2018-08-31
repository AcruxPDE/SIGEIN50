
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using System;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Net.Mail;
using SIGE.Entidades.Administracion;
//Licencia
using System.Linq;
using SIGE.Negocio.AdministracionSitio;

namespace SIGE.Negocio.Utilerias
{
    public class ContextoApp
    {

        public static MailConf mailConfiguration { get; set; }

        public static Empresa InfoEmpresa { get; set; }
        public static int IdCatalogoParentescos { get; set; }
        public static int IdCatalogoNacionalidades { get; set; }
        public static int IdCatalogoEstadosCivil { get; set; }
        public static int IdCatalogoGeneros { get; set; }
        public static int IdCatalogoCausaVacantes { get; set; }
        public static int IdCatalogoTiposTelefono { get; set; }
        public static int IdCatalogoOcupaciones { get; set; }
        public static int IdCatalogoRedesSociales { get; set; }
        public static int IdCatalogoCausaRequisicion { get; set; }

        public static bool FgControlDocumentos
        {
            get { return ContextoApp.ctrlDocumentos.fgHabilitado; }
            set { ContextoApp.ctrlDocumentos.fgHabilitado = value; }
        }

        //Licencia
        public static LicenciaSistema Licencia { get; set; }

        public static ControlDocumentos ctrlDocumentos { get; set; }

        public static string ClRutaArchivosTemporales { get; set; }

        public static IntegracionDePersonal IDP { get; set; }

        public static FormacionYDesarrollo FYD { get; set; }

        public static EvaluacionOrganizacional EO { get; set; }

        public static Metodologia MPC { get; set; }

        public static ReportesPersonalizados RP { get; set; }

        public static Consultas CI { get; set; }

        public static PuntodeEncuentro PDE { get; set; }

        public static AccesoNomina ANOM { get; set; }

        //Lista Campos Nomina - DO
        public static List<E_CAMPO_NOMINA_DO> vLstCamposNominaDO { get; set; }

        //Idioma del sistema

        public static string clCultureIdioma { get; set; }

        static ContextoApp()
        {
            ConfiguracionOperaciones oConfiguracion = new ConfiguracionOperaciones();

            SPE_OBTIENE_S_CONFIGURACION_Result vConfiguracion = new SPE_OBTIENE_S_CONFIGURACION_Result();

            vConfiguracion = oConfiguracion.ObtenerConfiguracion();

            XElement vXmlConfiguracion = XElement.Parse(vConfiguracion.XML_CONFIGURACION);
            XElement vXmlInfoEmpresa = vXmlConfiguracion.Element("EMPRESA");

            CamposNominaNegocio nNegocio = new CamposNominaNegocio();
            vLstCamposNominaDO = nNegocio.ObtenerConfiguracionCampo();

            Licencia = new LicenciaSistema();
            InfoEmpresa = new Empresa();
            if (vXmlInfoEmpresa != null)
            {
                InfoEmpresa.NbEmpresa = UtilXML.ValorAtributo<string>(vXmlInfoEmpresa.Attribute("NB_EMPRESA"));
                InfoEmpresa.FiLogotipo.FiArchivo = vConfiguracion.FI_LOGOTIPO;
                XElement vXmlLogotipo = vXmlInfoEmpresa.Element("LOGOTIPO");
                if (vXmlLogotipo != null)
                {
                    InfoEmpresa.FiLogotipo.NbArchivo = UtilXML.ValorAtributo<string>(vXmlLogotipo.Attribute("NB_ARCHIVO"));
                    InfoEmpresa.FiLogotipo.IdArchivo = UtilXML.ValorAtributo<int>(vXmlLogotipo.Attribute("ID_ARCHIVO"));
                }
            }

            XElement vXmlConfiguracionEO = vXmlConfiguracion.Element("EO");
            XElement vXmlConfiguracionIDP = vXmlConfiguracion.Element("IDP");
            XElement vXmlConfiguracionFYD = vXmlConfiguracion.Element("FYD");
            XElement vXmlCatalogos = vXmlConfiguracion.Element("CATALOGOS");
            XElement vXmlMailServer = vXmlConfiguracion.Element("MAIL").Element("SERVER");

            mailConfiguration = new MailConf()
            {
                SenderName = UtilXML.ValorAtributo<string>(vXmlMailServer.Attribute("NB_SENDER")),
                SenderMail = UtilXML.ValorAtributo<string>(vXmlMailServer.Attribute("NB_SENDER_MAIL")),
                User = UtilXML.ValorAtributo<string>(vXmlMailServer.Attribute("NB_USER")),
                Password = UtilXML.ValorAtributo<string>(vXmlMailServer.Attribute("NB_PASSWORD")),
                Server = UtilXML.ValorAtributo<string>(vXmlMailServer.Attribute("NB_HOST")),
                Port = UtilXML.ValorAtributo<int>(vXmlMailServer.Attribute("NO_PORT")),
                UseSSL = UtilXML.ValorAtributo<bool>(vXmlMailServer.Attribute("FG_USE_SSL")),
                IsHtmlMail = UtilXML.ValorAtributo<bool>(vXmlMailServer.Attribute("FG_HTML")),
                UseAuthentication = UtilXML.ValorAtributo<bool>(vXmlMailServer.Attribute("FG_AUTENTICA")),

            };

            IdCatalogoGeneros = UtilXML.ValorAtributo<int>(vXmlCatalogos.Element("GENERO").Attribute("ID_CATALOGO"));
            IdCatalogoCausaVacantes = UtilXML.ValorAtributo<int>(vXmlCatalogos.Element("CAUSA_VACANTES").Attribute("ID_CATALOGO"));
            IdCatalogoEstadosCivil = UtilXML.ValorAtributo<int>(vXmlCatalogos.Element("EDOCIVIL").Attribute("ID_CATALOGO"));
            IdCatalogoParentescos = UtilXML.ValorAtributo<int>(vXmlCatalogos.Element("PARENTESCO").Attribute("ID_CATALOGO"));
            IdCatalogoNacionalidades = UtilXML.ValorAtributo<int>(vXmlCatalogos.Element("NACIONALIDAD").Attribute("ID_CATALOGO"));
            IdCatalogoTiposTelefono = UtilXML.ValorAtributo<int>(vXmlCatalogos.Element("TELEFONO_TIPOS").Attribute("ID_CATALOGO"));
            IdCatalogoOcupaciones = UtilXML.ValorAtributo<int>(vXmlCatalogos.Element("OCUPACION").Attribute("ID_CATALOGO"));
            IdCatalogoRedesSociales = UtilXML.ValorAtributo<int>(vXmlCatalogos.Element("RED_SOCIAL").Attribute("ID_CATALOGO"));
            IdCatalogoCausaRequisicion = UtilXML.ValorAtributo<int>(vXmlCatalogos.Element("CAUSA_REQUISICION").Attribute("ID_CATALOGO"));

            ctrlDocumentos = new ControlDocumentos()
            {
                fgHabilitado = UtilXML.ValorAtributo<bool>(vXmlConfiguracion.Element("CONTROL_DOCUMENTOS").Attribute("FG_HABILITADO"))
            };

            ClRutaArchivosTemporales = @"~/App_Data";

            //Idioma del sistema 
          //  clCultureIdioma = UtilXML.ValorAtributo<string>(vXmlConfiguracion.Element("IDIOMA").Attribute("CULTURE"));
            clCultureIdioma = "ES";


            IDP = new IntegracionDePersonal();
            foreach (XElement vXmlMensaje in vXmlConfiguracionIDP.Element("MENSAJES").Elements("MENSAJE"))
                if ((UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("CL_MENSAJE")) == "AVISO_PRIVACIDAD"))
                {
                    IDP.MensajePrivacidad.fgVisible = UtilXML.ValorAtributo<bool>(vXmlMensaje.Attribute("FG_VISIBLE"));
                    IDP.MensajePrivacidad.dsMensaje = UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("DS_MENSAJE"));
                    break;
                }

            foreach (XElement vXmlMensaje in vXmlConfiguracionIDP.Element("MENSAJES").Elements("MENSAJE"))
                if ((UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("CL_MENSAJE")) == "SOLICITUDES_EMPLEO_BIENVENIDA"))
                {
                    IDP.MensajeBienvenidaEmpleo.fgVisible = UtilXML.ValorAtributo<bool>(vXmlMensaje.Attribute("FG_VISIBLE"));
                    IDP.MensajeBienvenidaEmpleo.dsMensaje = UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("DS_MENSAJE"));
                }


            foreach (XElement vXmlMensaje in vXmlConfiguracionIDP.Element("MENSAJES").Elements("MENSAJE"))
                if ((UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("CL_MENSAJE")) == "SOLICITUDES_EMPLEO_REQUISICION"))
                {
                    IDP.MensajeRequisicionesEmpleo.fgVisible = UtilXML.ValorAtributo<bool>(vXmlMensaje.Attribute("FG_VISIBLE"));
                    IDP.MensajeRequisicionesEmpleo.dsMensaje = UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("DS_MENSAJE"));
                }

            foreach (XElement vXmlMensaje in vXmlConfiguracionIDP.Element("MENSAJES").Elements("MENSAJE"))
                if ((UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("CL_MENSAJE")) == "SOLICITUDES_EMPLEO_PIEPAGINA"))
                {
                    IDP.MensajePiePagina.fgVisible = UtilXML.ValorAtributo<bool>(vXmlMensaje.Attribute("FG_VISIBLE"));
                    IDP.MensajePiePagina.dsMensaje = UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("DS_MENSAJE"));
                }

            foreach (XElement vXmlMensaje in vXmlConfiguracionIDP.Element("MENSAJES").Elements("MENSAJE"))
                if ((UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("CL_MENSAJE")) == "CORREO_ELECTRONICO_ASUNTO"))
                {
                    IDP.MensajeAsutoCorreo.fgVisible = UtilXML.ValorAtributo<bool>(vXmlMensaje.Attribute("FG_VISIBLE"));
                    IDP.MensajeAsutoCorreo.dsMensaje = UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("DS_MENSAJE"));
                }

            foreach (XElement vXmlMensaje in vXmlConfiguracionIDP.Element("MENSAJES").Elements("MENSAJE"))
                if ((UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("CL_MENSAJE")) == "CORREO_ELECTRONICO_CUERPO"))
                {
                    IDP.MensajeCuerpoCorreo.fgVisible = UtilXML.ValorAtributo<bool>(vXmlMensaje.Attribute("FG_VISIBLE"));
                    IDP.MensajeCuerpoCorreo.dsMensaje = UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("DS_MENSAJE"));
                }

            foreach (XElement vXmlMensaje in vXmlConfiguracionIDP.Element("MENSAJES").Elements("MENSAJE"))
                if ((UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("CL_MENSAJE")) == "PRUEBAS_BIENVENIDA"))
                {
                    IDP.MensajeBienvenidaPrueba.fgVisible = UtilXML.ValorAtributo<bool>(vXmlMensaje.Attribute("FG_VISIBLE"));
                    IDP.MensajeBienvenidaPrueba.dsMensaje = UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("DS_MENSAJE"));
                }

            foreach (XElement vXmlMensaje in vXmlConfiguracionIDP.Element("MENSAJES").Elements("MENSAJE"))
                if ((UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("CL_MENSAJE")) == "PRUEBAS_DESPEDIDA"))
                {
                    IDP.MensajeDespedidaPrueba.fgVisible = UtilXML.ValorAtributo<bool>(vXmlMensaje.Attribute("FG_VISIBLE"));
                    IDP.MensajeDespedidaPrueba.dsMensaje = UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("DS_MENSAJE"));
                }

            foreach (XElement vXmlMensaje in vXmlConfiguracionIDP.Element("MENSAJES").Elements("MENSAJE"))
                if ((UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("CL_MENSAJE")) == "CORREO_ENTREVISTA"))
                {
                    IDP.MensajeCorreoEntrevista.fgVisible = UtilXML.ValorAtributo<bool>(vXmlMensaje.Attribute("FG_VISIBLE"));
                    IDP.MensajeCorreoEntrevista.dsMensaje = UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("DS_MENSAJE"));
                }


            foreach (XElement vXmlMensaje in vXmlConfiguracionIDP.Element("MENSAJES").Elements("MENSAJE"))
                if ((UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("CL_MENSAJE")) == "SOLICITUDES_ENVIO_CORREO"))
                {
                    IDP.MensajeCorreoSolicitud.fgVisible = UtilXML.ValorAtributo<bool>(vXmlMensaje.Attribute("FG_VISIBLE"));
                    IDP.MensajeCorreoSolicitud.dsMensaje = UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("DS_MENSAJE"));
                }

            IDP.MensajeActualizacionPeriodica.dsNotificacion.fgVisible = UtilXML.ValorAtributo<bool>(vXmlConfiguracionIDP.Element("DEPURACION_CARTERA").Element("NOTIFICACION").Attribute("FG_HABILITADA"));
            IDP.MensajeActualizacionPeriodica.dsNotificacion.dsMensaje = UtilXML.ValorAtributo<string>(vXmlConfiguracionIDP.Element("DEPURACION_CARTERA").Element("NOTIFICACION").Attribute("DS_MENSAJE_NOTIFICACION"));
            IDP.MensajeActualizacionPeriodica.fgVisible = UtilXML.ValorAtributo<bool>(vXmlConfiguracionIDP.Element("DEPURACION_CARTERA").Element("NOTIFICACION").Attribute("FG_HABILITADA_CANDIDATOS"));
            IDP.MensajeActualizacionPeriodica.fgEstatus = UtilXML.ValorAtributo<bool>(vXmlConfiguracionIDP.Element("DEPURACION_CARTERA").Element("NOTIFICACION").Attribute("FG_ESTATUS_PROCESO"));
            IDP.MensajeActualizacionPeriodica.dsTipoPeriodicidad = UtilXML.ValorAtributo<string>(vXmlConfiguracionIDP.Element("DEPURACION_CARTERA").Element("NOTIFICACION").Attribute("TIPO_PERIODICIDAD"));
            IDP.MensajeActualizacionPeriodica.noPeriodicidad = UtilXML.ValorAtributo<int>(vXmlConfiguracionIDP.Element("DEPURACION_CARTERA").Element("NOTIFICACION").Attribute("NO_PERIODICIDAD"));

            foreach (XElement vXmlCorreo in vXmlConfiguracionIDP.Element("DEPURACION_CARTERA").Element("NOTIFICACION").Element("CORREOS").Elements("CORREO"))
            {
                IDP.MensajeActualizacionPeriodica.lstCorreos.Add(new MailAddress(UtilXML.ValorAtributo<string>(vXmlCorreo.Attribute("NB_DIRECCION")), UtilXML.ValorAtributo<string>(vXmlCorreo.Attribute("NB_DESTINATARIO"))));
            }

            foreach (XElement vXmlMensaje in vXmlConfiguracionIDP.Element("NOTIFICACION_RRHH").Elements("MENSAJES").Elements("MENSAJE"))
            {
                switch (UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("CL_MENSAJE")))
                {
                    case "NOTIFICAR_REQUISICION":
                        IDP.NotificacionRrhh.dsNotificarRequisicion.dsMensaje = vXmlMensaje.Value;
                        break;
                    case "AUTORIZAR_REQUISICION":
                        IDP.NotificacionRrhh.dsAutorizarRequisicion.dsMensaje = vXmlMensaje.Value;
                        break;
                    case "CANCELAR_REQUISICION":
                        IDP.NotificacionRrhh.dsCancelarRequisicion.dsMensaje = vXmlMensaje.Value;
                        break;
                    case "AUTORIZADOR_REQUISICION":
                        IDP.NotificacionRrhh.dsAutorizadorRequisicion.dsMensaje = vXmlMensaje.Value;
                        break;
                    case "CREADOR_PUESTO":
                        IDP.NotificacionRrhh.dsCreadorPuesto.dsMensaje = vXmlMensaje.Value;
                        break;
                    case "REENVIO_PUESTO":
                        IDP.NotificacionRrhh.dsReenvioPuesto.dsMensaje = vXmlMensaje.Value;
                        break;
                    case "RECHAZAR_PUESTO":
                        IDP.NotificacionRrhh.dsRechazarPuesto.dsMensaje = vXmlMensaje.Value;
                        break;
                    case "AUTORIZAR_PUESTO":
                        IDP.NotificacionRrhh.dsAutorizarPuesto.dsMensaje = vXmlMensaje.Value;
                        break;

                    case "NOTIFICAR_REQ_PUESTO":
                        IDP.NotificacionRrhh.dsNotificarReqPuesto.dsMensaje = vXmlMensaje.Value;
                        break;
                    case "REENVIO_REQ_PUESTO":
                        IDP.NotificacionRrhh.dsReenvioReqPuesto.dsMensaje = vXmlMensaje.Value;
                        break;
                    case "ESTATUS_REQ_PUESTO":
                        IDP.NotificacionRrhh.dsEstatusReqPuesto.dsMensaje = vXmlMensaje.Value;
                        break;
                }
            }

            foreach (XElement vXmlCorreo in vXmlConfiguracionIDP.Element("NOTIFICACION_RRHH").Element("CORREOS").Elements("CORREO"))
            {
                IDP.NotificacionRrhh.lstCorreosRequisiciones = new MailAddress(UtilXML.ValorAtributo<string>(vXmlCorreo.Attribute("NB_DIRECCION")), UtilXML.ValorAtributo<string>(vXmlCorreo.Attribute("NB_DESTINATARIO")));
                IDP.NotificacionRrhh.idEmpleadoAutorizaRequisicion = UtilXML.ValorAtributo<int>(vXmlCorreo.Attribute("ID_EMPLEADO"));
            }

            IDP.ConfiguracionPsicometria.FgMostrarCronometro = UtilXML.ValorAtributo<bool>(vXmlConfiguracionIDP.Element("CONFIGURACION_PSICOMETRIA").Attribute("FG_MOSTRAR_CRONOMETRO"));

            IDP.ConfiguracionIntegracion.FgConsultasParciales = UtilXML.ValorAtributo<bool>(vXmlConfiguracionIDP.Element("CONFIGURACION_INTEGRACION").Attribute("FG_CONSULTAS_PARCIALES"));
            IDP.ConfiguracionIntegracion.FgIgnorarCompetencias = UtilXML.ValorAtributo<bool>(vXmlConfiguracionIDP.Element("CONFIGURACION_INTEGRACION").Attribute("FG_IGNORAR_COMPETENCIAS"));


            FYD = new FormacionYDesarrollo();

            FYD.ClVistaPrograma.ClVistaPrograma = UtilXML.ValorAtributo<string>(vXmlConfiguracionFYD.Element("CONFIGURACION_PROGRAMA").Attribute("CL_VISTA"));

            foreach (XElement vXmlMensaje in vXmlConfiguracionFYD.Element("MENSAJES").Elements("MENSAJE"))
                if ((UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("CL_MENSAJE")) == "CORREO_PARTICIPANTE"))
                {
                    FYD.MensajeCorreoParticipantes.dsMensaje = vXmlMensaje.Value;
                    break;
                }

            foreach (XElement vXmlMensaje in vXmlConfiguracionFYD.Element("MENSAJES").Elements("MENSAJE"))
                if ((UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("CL_MENSAJE")) == "CORREO_EVALUADOR"))
                {
                    FYD.MensajeCorreoEvaluadores.dsMensaje = vXmlMensaje.Value;
                    break;
                }

            foreach (XElement vXmlMensaje in vXmlConfiguracionFYD.Element("MENSAJES").Elements("MENSAJE"))
                if ((UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("CL_MENSAJE")) == "CORREO_SOLICITUDES"))
                {
                    FYD.MensajeCorreoSolicitudes.dsMensaje = vXmlMensaje.Value;
                    break;
                }

            EO = new EvaluacionOrganizacional();

            foreach (XElement vXmlMensaje in vXmlConfiguracionEO.Element("MENSAJES").Elements("MENSAJE"))
            {
                switch (UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("CL_MENSAJE")))
                {
                    case "CORREO_EVALUADOR":
                        EO.MensajeCorreoEvaluador.dsMensaje = vXmlMensaje.Value;
                        break;
                    case "CUESTIONARIO":
                        EO.MensajeCuestionario.dsMensaje = vXmlMensaje.Value;
                        break;
                }
            }


            EO.Configuracion.NivelMinimoIndividualIndependiente = UtilXML.ValorAtributo<decimal>(vXmlConfiguracionEO.Element("CONFIGURACION").Attribute("NIVEL_MINIMO_INDIVIDUAL_INDEPENDIENTE"));
            EO.Configuracion.BonoMinimoIndividualIndependiente = UtilXML.ValorAtributo<decimal>(vXmlConfiguracionEO.Element("CONFIGURACION").Attribute("BONO_MINIMO_INDIVIDUAL_INDEPENDIENTE"));
            EO.Configuracion.NivelMinimoIndividualDependiente = UtilXML.ValorAtributo<decimal>(vXmlConfiguracionEO.Element("CONFIGURACION").Attribute("NIVEL_MINIMO_INDIVIDUAL_DEPENDIENTE"));
            EO.Configuracion.BonoMinimoIndividualDependiente = UtilXML.ValorAtributo<decimal>(vXmlConfiguracionEO.Element("CONFIGURACION").Attribute("BONO_MINIMO_INDIVIDUAL_DEPENDIENTE"));
            EO.Configuracion.NivelMinimoGrupal = UtilXML.ValorAtributo<decimal>(vXmlConfiguracionEO.Element("CONFIGURACION").Attribute("NIVEL_MINIMO_GRUPAL"));
            EO.Configuracion.BonoMinimoGrupal = UtilXML.ValorAtributo<decimal>(vXmlConfiguracionEO.Element("CONFIGURACION").Attribute("BONO_MINIMO_GRUPAL"));
            EO.Configuracion.SueldoAsignacion = UtilXML.ValorAtributo<string>(vXmlConfiguracionEO.Element("CONFIGURACION").Attribute("SUELDO_ASIGNACION"));
            EO.Configuracion.CampoExtra = UtilXML.ValorAtributo<string>(vXmlConfiguracionEO.Element("CONFIGURACION").Attribute("CAMPO_EXTRA"));
            EO.Configuracion.IdCampoExtraSueldoAsignacion = UtilXML.ValorAtributo<string>(vXmlConfiguracionEO.Element("CONFIGURACION").Attribute("ID_CAMPO_EXTRA_SUELDO_ASIGNACION"));
            EO.Configuracion.NbCampoExtraSueldoAsignacion = UtilXML.ValorAtributo<string>(vXmlConfiguracionEO.Element("CONFIGURACION").Attribute("NB_CAMPO_EXTRA_SUELDO_ASIGNACION"));

            foreach (XElement vXmlMensaje in vXmlConfiguracionEO.Element("CONFIGURACION").Element("MENSAJES").Elements("MENSAJE"))
                if ((UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("CL_MENSAJE")) == "MENSAJE_CAPTURA_RESULTADOS"))
                {
                    EO.Configuracion.MensajeCapturaResultados.dsMensaje = vXmlMensaje.Value;
                }

            foreach (XElement vXmlMensaje in vXmlConfiguracionEO.Element("CONFIGURACION").Element("MENSAJES").Elements("MENSAJE"))
                if ((UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("CL_MENSAJE")) == "MENSAJE_IMPORTANTE"))
                {
                    EO.Configuracion.MensajeImportantes.dsMensaje = vXmlMensaje.Value;
                }

            foreach (XElement vXmlMensaje in vXmlConfiguracionEO.Element("CONFIGURACION").Element("MENSAJES").Elements("MENSAJE"))
                if ((UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("CL_MENSAJE")) == "MENSAJE_BAJA_REPLICA"))
                {
                    EO.Configuracion.MensajeBajaReplica.dsMensaje = vXmlMensaje.Value;
                }

            foreach (XElement vXmlMensaje in vXmlConfiguracionEO.Element("CONFIGURACION").Element("MENSAJES").Elements("MENSAJE"))
                if ((UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("CL_MENSAJE")) == "MENSAJE_BAJA_NOTIFICADOR"))
                {
                    EO.Configuracion.MensajeBajaNotificador.dsMensaje = vXmlMensaje.Value;
                }

            //foreach (XElement vXmlMensaje in vXmlConfiguracionEO.Element("CONFIGURACION").Element("MENSAJES").Elements("MENSAJE"))
            //    if ((UtilXML.ValorAtributo<string>(vXmlMensaje.Attribute("CL_MENSAJE")) == "MENSAJE_BAJA_REPLICA"))
            //    {
            //        EO.Configuracion.MensajeBajaReplica.dsMensaje = vXmlMensaje.Value;
            //    }

            MPC = new Metodologia();

            //Licencia

            RP = new ReportesPersonalizados();

            //Licencia

            CI = new Consultas();

            //Licencia

            PDE = new PuntodeEncuentro();

            //Licencia
            ANOM = new AccesoNomina();

        }

        public static E_RESULTADO SaveConfiguration(string pClUsuario, string pNbPrograma)
        {
            List<XElement> lstCorreos = new List<XElement>();
            foreach (var item in IDP.MensajeActualizacionPeriodica.lstCorreos)
            {
                lstCorreos.Add
                        (new XElement("CORREO",
                                new XAttribute("NB_DESTINATARIO", item.DisplayName),
                                new XAttribute("NB_DIRECCION", item.Address)));
            }

            List<XElement> lstCorreosRequisiciones = new List<XElement>();

            //foreach (var item in IDP.NotificacionRrhh.lstCorreosRequisiciones)
            //{
            //    lstCorreosRequisiciones.Add
            //            (new XElement("CORREO",
            //                   new XAttribute("NB_DESTINATARIO", item.DisplayName),
            //                    new XAttribute("NB_DIRECCION", item.Address)));
            //}

            if (IDP.NotificacionRrhh.lstCorreosRequisiciones != null)
            {
                lstCorreosRequisiciones.Add
                        (new XElement("CORREO",
                               new XAttribute("NB_DESTINATARIO", IDP.NotificacionRrhh.lstCorreosRequisiciones.DisplayName),
                                new XAttribute("NB_DIRECCION", IDP.NotificacionRrhh.lstCorreosRequisiciones.Address),
                                new XAttribute("ID_EMPLEADO", IDP.NotificacionRrhh.idEmpleadoAutorizaRequisicion)));
            }


            XElement vXmlConfiguracion = new XElement("CONFIGURACIONES",
                new XElement("MAIL",
                    new XElement("SERVER",
                        new XAttribute("NB_SENDER", mailConfiguration.SenderName),
                        new XAttribute("NB_SENDER_MAIL", mailConfiguration.SenderMail),
                        new XAttribute("NB_USER", mailConfiguration.User),
                        new XAttribute("NB_PASSWORD", mailConfiguration.Password),
                        new XAttribute("NB_HOST", mailConfiguration.Server),
                        new XAttribute("NO_PORT", mailConfiguration.Port),
                        new XAttribute("FG_USE_SSL", mailConfiguration.UseSSL ? "1" : "0"),
                        new XAttribute("FG_HTML", mailConfiguration.IsHtmlMail ? "1" : "0"),
                        new XAttribute("FG_AUTENTICA", mailConfiguration.UseAuthentication ? "1" : "0"))),
                new XElement("CATALOGOS",
                    new XElement("GENERO", new XAttribute("ID_CATALOGO", IdCatalogoGeneros)),
                    new XElement("EDOCIVIL", new XAttribute("ID_CATALOGO", IdCatalogoEstadosCivil)),
                    new XElement("CAUSA_VACANTES", new XAttribute("ID_CATALOGO", IdCatalogoCausaVacantes)),
                    new XElement("TELEFONO_TIPOS", new XAttribute("ID_CATALOGO", IdCatalogoTiposTelefono)),
                    new XElement("PARENTESCO", new XAttribute("ID_CATALOGO", IdCatalogoParentescos)),
                    new XElement("OCUPACION", new XAttribute("ID_CATALOGO", IdCatalogoOcupaciones)),
                    new XElement("NACIONALIDAD", new XAttribute("ID_CATALOGO", IdCatalogoNacionalidades)),
                    new XElement("RED_SOCIAL", new XAttribute("ID_CATALOGO", IdCatalogoRedesSociales)),
                    new XElement("CAUSA_REQUISICION", new XAttribute("ID_CATALOGO", IdCatalogoCausaRequisicion))),
                new XElement("CONTROL_DOCUMENTOS",
                    new XAttribute("FG_HABILITADO", ctrlDocumentos.fgHabilitado ? "1" : "0")),
                  new XElement("IDP",
                new XElement("MENSAJES",
                    new XElement("MENSAJE",
                        new XAttribute("CL_MENSAJE", "AVISO_PRIVACIDAD"),
                        new XAttribute("FG_VISIBLE", IDP.MensajePrivacidad.fgVisible ? "1" : "0"),
                        new XAttribute("DS_MENSAJE", IDP.MensajePrivacidad.dsMensaje)),
                   new XElement("MENSAJE",
                        new XAttribute("CL_MENSAJE", "SOLICITUDES_EMPLEO_BIENVENIDA"),
                        new XAttribute("FG_VISIBLE", IDP.MensajeBienvenidaEmpleo.fgVisible ? "1" : "0"),
                        new XAttribute("DS_MENSAJE", IDP.MensajeBienvenidaEmpleo.dsMensaje)),
                   new XElement("MENSAJE",
                        new XAttribute("CL_MENSAJE", "SOLICITUDES_EMPLEO_REQUISICION"),
                        new XAttribute("FG_VISIBLE", IDP.MensajeRequisicionesEmpleo.fgVisible ? "1" : "0"),
                        new XAttribute("DS_MENSAJE", IDP.MensajeRequisicionesEmpleo.dsMensaje)),
                   new XElement("MENSAJE",
                        new XAttribute("CL_MENSAJE", "SOLICITUDES_EMPLEO_PIEPAGINA"),
                        new XAttribute("FG_VISIBLE", IDP.MensajePiePagina.fgVisible ? "1" : "0"),
                        new XAttribute("DS_MENSAJE", IDP.MensajePiePagina.dsMensaje)),
                    new XElement("MENSAJE",
                        new XAttribute("CL_MENSAJE", "CORREO_ELECTRONICO_ASUNTO"),
                        new XAttribute("FG_VISIBLE", IDP.MensajeAsutoCorreo.fgVisible ? "1" : "0"),
                        new XAttribute("DS_MENSAJE", IDP.MensajeAsutoCorreo.dsMensaje)),
                     new XElement("MENSAJE",
                        new XAttribute("CL_MENSAJE", "CORREO_ELECTRONICO_CUERPO"),
                        new XAttribute("FG_VISIBLE", IDP.MensajeCuerpoCorreo.fgVisible ? "1" : "0"),
                        new XAttribute("DS_MENSAJE", IDP.MensajeCuerpoCorreo.dsMensaje)),
                    new XElement("MENSAJE",
                        new XAttribute("CL_MENSAJE", "PRUEBAS_BIENVENIDA"),
                        new XAttribute("FG_VISIBLE", IDP.MensajeBienvenidaPrueba.fgVisible ? "1" : "0"),
                        new XAttribute("DS_MENSAJE", IDP.MensajeBienvenidaPrueba.dsMensaje)),
                    new XElement("MENSAJE",
                        new XAttribute("CL_MENSAJE", "PRUEBAS_DESPEDIDA"),
                        new XAttribute("FG_VISIBLE", IDP.MensajeDespedidaPrueba.fgVisible ? "1" : "0"),
                        new XAttribute("DS_MENSAJE", IDP.MensajeDespedidaPrueba.dsMensaje)),
                    new XElement("MENSAJE",
                        new XAttribute("CL_MENSAJE", "CORREO_ENTREVISTA"),
                        new XAttribute("FG_VISIBLE", IDP.MensajeCorreoEntrevista.fgVisible ? "1" : "0"),
                        new XAttribute("DS_MENSAJE", IDP.MensajeCorreoEntrevista.dsMensaje)),
                     new XElement("MENSAJE",
                        new XAttribute("CL_MENSAJE", "SOLICITUDES_ENVIO_CORREO"),
                        new XAttribute("FG_VISIBLE", IDP.MensajeCorreoSolicitud.fgVisible ? "1" : "0"),
                        new XAttribute("DS_MENSAJE", IDP.MensajeCorreoSolicitud.dsMensaje))),
                 new XElement("DEPURACION_CARTERA",
                        new XElement("NOTIFICACION",
                        new XAttribute("CL_NOTIFICACION", "NOTIFICACION_CARTERA"),
                        new XAttribute("FG_HABILITADA", IDP.MensajeActualizacionPeriodica.dsNotificacion.fgVisible ? "1" : "0"),
                        new XAttribute("DS_MENSAJE_NOTIFICACION", IDP.MensajeActualizacionPeriodica.dsNotificacion.dsMensaje),
                        new XAttribute("FG_HABILITADA_CANDIDATOS", IDP.MensajeActualizacionPeriodica.fgVisible ? "1" : "0"),
                        new XAttribute("FG_ESTATUS_PROCESO", IDP.MensajeActualizacionPeriodica.fgEstatus ? "1" : "0"),
                        new XAttribute("TIPO_PERIODICIDAD", IDP.MensajeActualizacionPeriodica.dsTipoPeriodicidad),
                        new XAttribute("NO_PERIODICIDAD", IDP.MensajeActualizacionPeriodica.noPeriodicidad),
                 new XElement("CORREOS", lstCorreos))),
                 new XElement("NOTIFICACION_RRHH",
                 new XElement("CORREOS", lstCorreosRequisiciones),
                 new XElement("MENSAJES",
                         new XElement("MENSAJE", new XAttribute("CL_MENSAJE", "NOTIFICAR_REQUISICION"), IDP.NotificacionRrhh.dsNotificarRequisicion.dsMensaje),
                         new XElement("MENSAJE", new XAttribute("CL_MENSAJE", "AUTORIZAR_REQUISICION"), IDP.NotificacionRrhh.dsAutorizarRequisicion.dsMensaje),
                         new XElement("MENSAJE", new XAttribute("CL_MENSAJE", "CANCELAR_REQUISICION"), IDP.NotificacionRrhh.dsCancelarRequisicion.dsMensaje),
                         new XElement("MENSAJE", new XAttribute("CL_MENSAJE", "AUTORIZADOR_REQUISICION"), IDP.NotificacionRrhh.dsAutorizadorRequisicion.dsMensaje),
                         new XElement("MENSAJE", new XAttribute("CL_MENSAJE", "CREADOR_PUESTO"), IDP.NotificacionRrhh.dsCreadorPuesto.dsMensaje),
                         new XElement("MENSAJE", new XAttribute("CL_MENSAJE", "REENVIO_PUESTO"), IDP.NotificacionRrhh.dsReenvioPuesto.dsMensaje),
                         new XElement("MENSAJE", new XAttribute("CL_MENSAJE", "RECHAZAR_PUESTO"), IDP.NotificacionRrhh.dsRechazarPuesto.dsMensaje),
                         new XElement("MENSAJE", new XAttribute("CL_MENSAJE", "AUTORIZAR_PUESTO"), IDP.NotificacionRrhh.dsAutorizarPuesto.dsMensaje),

                         new XElement("MENSAJE", new XAttribute("CL_MENSAJE", "NOTIFICAR_REQ_PUESTO"), IDP.NotificacionRrhh.dsNotificarReqPuesto.dsMensaje),
                         new XElement("MENSAJE", new XAttribute("CL_MENSAJE", "REENVIO_REQ_PUESTO"), IDP.NotificacionRrhh.dsReenvioReqPuesto.dsMensaje),
                         new XElement("MENSAJE", new XAttribute("CL_MENSAJE", "ESTATUS_REQ_PUESTO"), IDP.NotificacionRrhh.dsEstatusReqPuesto.dsMensaje)
                         )),
                   new XElement("CONFIGURACION_PSICOMETRIA", new XAttribute("FG_MOSTRAR_CRONOMETRO", IDP.ConfiguracionPsicometria.FgMostrarCronometro)),
                   new XElement("CONFIGURACION_INTEGRACION", new XAttribute("FG_CONSULTAS_PARCIALES", IDP.ConfiguracionIntegracion.FgConsultasParciales),
                                                             new XAttribute("FG_IGNORAR_COMPETENCIAS", IDP.ConfiguracionIntegracion.FgIgnorarCompetencias))
                 ),
                 new XElement("FYD",
                    new XElement("MENSAJES",
                        new XElement("MENSAJE", new XAttribute("CL_MENSAJE", "CORREO_PARTICIPANTE"), FYD.MensajeCorreoParticipantes.dsMensaje),
                        new XElement("MENSAJE", new XAttribute("CL_MENSAJE", "CORREO_EVALUADOR"), FYD.MensajeCorreoEvaluadores.dsMensaje),
                        new XElement("MENSAJE", new XAttribute("CL_MENSAJE", "CORREO_SOLICITUDES"), FYD.MensajeCorreoSolicitudes.dsMensaje)),
                        new XElement("CONFIGURACION_PROGRAMA", new XAttribute("CL_VISTA",FYD.ClVistaPrograma.ClVistaPrograma))),
                new XElement("EO",
                    new XElement("MENSAJES",
                        new XElement("MENSAJE", new XAttribute("CL_MENSAJE", "CORREO_EVALUADOR"), EO.MensajeCorreoEvaluador.dsMensaje)),
                //new XElement("MENSAJE", new XAttribute("CL_MENSAJE", "EVALUACION_DESEMPENO"), EO.MensajeEvaluadorDesempenio.dsMensaje),
                //new XElement("MENSAJE", new XAttribute("CL_MENSAJE", "PERIODO_DESEMPENO"), EO.MensajePeriodoDesempenio.dsMensaje),
                //new XElement("MENSAJE", new XAttribute("CL_MENSAJE", "DESEMPENO_EVALUADOR"), EO.MensajeDesempenioEvaluador.dsMensaje),
                //new XElement("MENSAJE", new XAttribute("CL_MENSAJE", "DESEMPENO_EVALUADOR_M"), EO.MensajeDesempenioMEvaluador.dsMensaje),
                //new XElement("MENSAJE", new XAttribute("CL_MENSAJE", "DESEMPENO_EVALUADO"), EO.MensajeDesempenioEvaluado.dsMensaje),
                //new XElement("MENSAJE", new XAttribute("CL_MENSAJE", "DESEMPENO_EVALUADO_M"), EO.MensajeDesempenioMEvaluado.dsMensaje)

                    new XElement("CONFIGURACION",
                        new XAttribute("NIVEL_MINIMO_INDIVIDUAL_INDEPENDIENTE", EO.Configuracion.NivelMinimoIndividualIndependiente),
                        new XAttribute("BONO_MINIMO_INDIVIDUAL_INDEPENDIENTE", EO.Configuracion.BonoMinimoIndividualIndependiente),
                        new XAttribute("NIVEL_MINIMO_INDIVIDUAL_DEPENDIENTE", EO.Configuracion.NivelMinimoIndividualDependiente),
                        new XAttribute("BONO_MINIMO_INDIVIDUAL_DEPENDIENTE", EO.Configuracion.BonoMinimoIndividualDependiente),
                        new XAttribute("NIVEL_MINIMO_GRUPAL", EO.Configuracion.NivelMinimoGrupal),
                        new XAttribute("BONO_MINIMO_GRUPAL", EO.Configuracion.BonoMinimoGrupal),
                        new XAttribute("SUELDO_ASIGNACION", EO.Configuracion.SueldoAsignacion),
                        new XAttribute("CAMPO_EXTRA", EO.Configuracion.CampoExtra),
                        new XAttribute("ID_CAMPO_EXTRA_SUELDO_ASIGNACION", EO.Configuracion.IdCampoExtraSueldoAsignacion),
                        new XAttribute("NB_CAMPO_EXTRA_SUELDO_ASIGNACION", EO.Configuracion.NbCampoExtraSueldoAsignacion),
                        new XElement("MENSAJES",
                            new XElement("MENSAJE", new XAttribute("CL_MENSAJE", "MENSAJE_CAPTURA_RESULTADOS"), EO.Configuracion.MensajeCapturaResultados.dsMensaje),
                            new XElement("MENSAJE", new XAttribute("CL_MENSAJE", "MENSAJE_IMPORTANTE"), EO.Configuracion.MensajeImportantes.dsMensaje),
                            new XElement("MENSAJE", new XAttribute("CL_MENSAJE", "MENSAJE_BAJA_REPLICA"), EO.Configuracion.MensajeBajaReplica.dsMensaje),
                            new XElement("MENSAJE", new XAttribute("CL_MENSAJE", "MENSAJE_BAJA_NOTIFICADOR"), EO.Configuracion.MensajeBajaNotificador.dsMensaje)))
                        )
                );

            XElement vXmlInfoEmpresa = new XElement("EMPRESA", new XAttribute("NB_EMPRESA", InfoEmpresa.NbEmpresa));
            if (InfoEmpresa.FiLogotipo.FiArchivo != null)
                vXmlInfoEmpresa.Add(new XElement("LOGOTIPO",
                        new XAttribute("NB_ARCHIVO", InfoEmpresa.FiLogotipo.NbArchivo),
                        new XAttribute("ID_ARCHIVO", InfoEmpresa.FiLogotipo.IdArchivo ?? 0)));
            vXmlConfiguracion.Add(vXmlInfoEmpresa);

            ConfiguracionOperaciones oConfiguracion = new ConfiguracionOperaciones();
            return new E_RESULTADO(oConfiguracion.InsertaActualizaConfiguracion("A", vXmlConfiguracion, InfoEmpresa.FiLogotipo.FiArchivo, pClUsuario, pNbPrograma));
        }

    }
    public class ControlDocumentos
    {
        public bool fgHabilitado { get; set; }
    }

    public class Empresa
    {
        public string NbEmpresa { get; set; }
        public Archivo FiLogotipo { get; set; }
        //Licencia
        public string MsgSistema { get; set; }
        public int Volumen { get; set; }

        public Empresa()
        {
            FiLogotipo = new Archivo();
        }
    }

    //Licencia
    public class LicenciaSistema
    {
        public string clLicencia { get; set; }
        public string objAdicional { get; set; }
        public string feCreacion { get; set; }
        public string clPassWs { get; set; }
        public string clCliente { get; set; }
        public string clEmpresa { get; set; }
    }

    public class Archivo
    {
        public int? IdArchivo { get; set; }
        private byte[] fiArchivo;
        public byte[] FiArchivo
        {
            get
            {
                return fiArchivo;
            }
            set
            {
                fiArchivo = value;
            }
        }
        public string NbArchivo { get; set; }
    }

    public class Mensaje
    {
        public bool fgVisible { get; set; }
        public string dsMensaje { get; set; }

        public Mensaje()
        {
            fgVisible = true;
        }
    }

    public class DepuracionCartera
    {
        public Mensaje dsNotificacion { get; set; }
        public bool fgVisible { get; set; }
        public int noPeriodicidad { get; set; }
        public string dsTipoPeriodicidad { get; set; }
        public List<MailAddress> lstCorreos { get; set; }
        public bool fgEstatus { get; set; }

        public DepuracionCartera()
        {
            dsNotificacion = new Mensaje();
            lstCorreos = new List<MailAddress>();
            fgVisible = true;
        }
    }

    public class NotificacionRRHH
    {
        public Mensaje dsNotificarRequisicion { get; set; }
        public Mensaje dsAutorizarRequisicion { get; set; }
        public Mensaje dsCancelarRequisicion { get; set; }
        public Mensaje dsAutorizadorRequisicion { get; set; }

        public Mensaje dsCreadorPuesto { get; set; }
        public Mensaje dsReenvioPuesto { get; set; }
        public Mensaje dsRechazarPuesto { get; set; }
        public Mensaje dsAutorizarPuesto { get; set; }

        public Mensaje dsNotificarReqPuesto { get; set; }
        public Mensaje dsReenvioReqPuesto { get; set; }
        public Mensaje dsEstatusReqPuesto { get; set; }

        public int idEmpleadoAutorizaRequisicion { get; set; }
        public MailAddress lstCorreosRequisiciones { get; set; }

        public NotificacionRRHH()
        {
            dsNotificarRequisicion = new Mensaje();
            dsAutorizarRequisicion = new Mensaje();
            dsCancelarRequisicion = new Mensaje();
            dsAutorizadorRequisicion = new Mensaje();

            dsCreadorPuesto = new Mensaje();
            dsReenvioPuesto = new Mensaje();
            dsRechazarPuesto = new Mensaje();
            dsAutorizarPuesto = new Mensaje();

            dsNotificarReqPuesto = new Mensaje();
            dsReenvioReqPuesto = new Mensaje();
            dsEstatusReqPuesto = new Mensaje();

            //lstCorreosRequisiciones = new MailAddress();
        }
    }

    public class ConfiguracionPsicometria
    {
        public bool FgMostrarCronometro { get; set; }

        public ConfiguracionPsicometria()
        {

        }
    }

    public class ConfiguracionIntegracion
    {
        public bool FgConsultasParciales { get; set; }
        public bool FgIgnorarCompetencias { get; set; }

        public ConfiguracionIntegracion()
        {

        }
    }

    public class ConfiguracionPrograma
    {
        public string ClVistaPrograma { get; set; }

        public ConfiguracionPrograma()
        {
    }
    }

    //Licencia
    public class LicenciaModulo
    {
        public string MsgActivo { get; set; }
    }

    public class IntegracionDePersonal
    {
        public Mensaje MensajePrivacidad { get; set; }
        public Mensaje MensajeBienvenidaEmpleo { get; set; }
        public Mensaje MensajeRequisicionesEmpleo { get; set; }
        public Mensaje MensajePiePagina { get; set; }
        public Mensaje MensajeAsutoCorreo { get; set; }
        public Mensaje MensajeCuerpoCorreo { get; set; }
        public Mensaje MensajeBienvenidaPrueba { get; set; }
        public Mensaje MensajeDespedidaPrueba { get; set; }
        public Mensaje MensajeCorreoEntrevista { get; set; }
        public Mensaje MensajeCorreoSolicitud { get; set; }
        public DepuracionCartera MensajeActualizacionPeriodica { get; set; }
        public NotificacionRRHH NotificacionRrhh { get; set; }
        public ConfiguracionPsicometria ConfiguracionPsicometria { get; set; }
        public ConfiguracionIntegracion ConfiguracionIntegracion { get; set; }

        //Licencia
        public LicenciaModulo LicenciaIntegracion { get; set; }


        public IntegracionDePersonal()
        {
            MensajePrivacidad = new Mensaje();
            MensajeBienvenidaEmpleo = new Mensaje();
            MensajeRequisicionesEmpleo = new Mensaje();
            MensajePiePagina = new Mensaje();
            MensajeAsutoCorreo = new Mensaje();
            MensajeCuerpoCorreo = new Mensaje();
            MensajeBienvenidaPrueba = new Mensaje();
            MensajeDespedidaPrueba = new Mensaje();
            MensajeCorreoEntrevista = new Mensaje();
            MensajeCorreoSolicitud = new Mensaje();
            MensajeActualizacionPeriodica = new DepuracionCartera();
            NotificacionRrhh = new NotificacionRRHH();
            ConfiguracionPsicometria = new ConfiguracionPsicometria();
            ConfiguracionIntegracion = new ConfiguracionIntegracion();
            //Licencia
            LicenciaIntegracion = new LicenciaModulo();
        }
    }

    public class FormacionYDesarrollo
    {
        public Mensaje MensajeCorreoParticipantes { get; set; }
        public Mensaje MensajeCorreoEvaluadores { get; set; }
        public Mensaje MensajeCorreoSolicitudes { get; set; }
        public ConfiguracionPrograma ClVistaPrograma { get; set; }
        //Licencia
        public LicenciaModulo LicenciaFormacion { get; set; }

        public FormacionYDesarrollo()
        {
            MensajeCorreoEvaluadores = new Mensaje();
            MensajeCorreoParticipantes = new Mensaje();
            MensajeCorreoSolicitudes = new Mensaje();
            ClVistaPrograma = new ConfiguracionPrograma();
            
            //Licencia
            LicenciaFormacion = new LicenciaModulo();
        }
    }

    public class ConfiguracionEvaluacionOrganizacional
    {
        public Mensaje MensajeCapturaResultados { get; set; }
        public Mensaje MensajeImportantes { get; set; }
        //public Mensaje MensajeBajaEmpleado { get; set; }
        public Mensaje MensajeBajaNotificador { get; set; }
        public Mensaje MensajeBajaReplica { get; set; }

        public decimal NivelMinimoIndividualIndependiente { get; set; }
        public decimal BonoMinimoIndividualIndependiente { get; set; }

        public decimal NivelMinimoIndividualDependiente { get; set; }
        public decimal BonoMinimoIndividualDependiente { get; set; }

        public decimal NivelMinimoGrupal { get; set; }
        public decimal BonoMinimoGrupal { get; set; }

        public string SueldoAsignacion { get; set; }
        public string CampoExtra { get; set; }
        public string IdCampoExtraSueldoAsignacion { get; set; }

        public string NbCampoExtraSueldoAsignacion { get; set; }

        public ConfiguracionEvaluacionOrganizacional()
        {
            //MensajeBajaEmpleado = new Mensaje();
            MensajeBajaNotificador = new Mensaje();
            MensajeBajaReplica = new Mensaje();
            MensajeCapturaResultados = new Mensaje();
            MensajeImportantes = new Mensaje();
            NivelMinimoIndividualIndependiente = 0;
            BonoMinimoIndividualIndependiente = 0;

            NivelMinimoIndividualDependiente = 0;
            BonoMinimoIndividualDependiente = 0;

            NivelMinimoGrupal = 0;
            BonoMinimoGrupal = 0;

            SueldoAsignacion = "";
            CampoExtra = "";
            IdCampoExtraSueldoAsignacion = "";
            NbCampoExtraSueldoAsignacion = "";
        }
    }

    public class EvaluacionOrganizacional
    {
        public Mensaje MensajeCorreoEvaluador { get; set; }
        //public Mensaje MensajeEvaluadorDesempenio { get; set; } //
        //public Mensaje MensajePeriodoDesempenio { get; set; } //
        //public Mensaje MensajeDesempenioEvaluador { get; set; } //
        //public Mensaje MensajeDesempenioMEvaluador { get; set; } //
        //public Mensaje MensajeDesempenioEvaluado { get; set; } //
        //public Mensaje MensajeDesempenioMEvaluado { get; set; } //
        public Mensaje MensajeCuestionario { get; set; } //
        public ConfiguracionEvaluacionOrganizacional Configuracion { get; set; }
        //Licencia
        public LicenciaModulo LicenciaED { get; set; }
        public LicenciaModulo LicenciaRDP { get; set; }
        public LicenciaModulo LicenciaCL { get; set; }

        public EvaluacionOrganizacional()
        {
            MensajeCorreoEvaluador = new Mensaje();
            //MensajeEvaluadorDesempenio = new Mensaje(); //
            //MensajePeriodoDesempenio = new Mensaje();
            //MensajeDesempenioEvaluador = new Mensaje();
            //MensajeDesempenioMEvaluador = new Mensaje();
            //MensajeDesempenioEvaluado = new Mensaje();
            //MensajeDesempenioMEvaluado = new Mensaje();
            MensajeCuestionario = new Mensaje();
            Configuracion = new ConfiguracionEvaluacionOrganizacional();
            LicenciaED = new LicenciaModulo();
            LicenciaRDP = new LicenciaModulo();
            LicenciaCL = new LicenciaModulo();

        }
    }

    public class Metodologia
    {
        //Licencia
        public LicenciaModulo LicenciaMetodologia { get; set; }

        public Metodologia()
        {
            LicenciaMetodologia = new LicenciaModulo();
        }
    }

    public class Consultas
    {
        //Licencia
        public LicenciaModulo LicenciaConsultasInteligentes { get; set; }

        public Consultas()
        {
            LicenciaConsultasInteligentes = new LicenciaModulo();
        }
    }

    public class ReportesPersonalizados
    {
        //Licencia
        public LicenciaModulo LicenciaReportes { get; set; }

        public ReportesPersonalizados()
        {
            LicenciaReportes = new LicenciaModulo();
        }
    }

    public class PuntodeEncuentro
    {
        //Licencia
        public LicenciaModulo LicenciaPuntoEncuentro { get; set; }

        public PuntodeEncuentro()
        {
            LicenciaPuntoEncuentro = new LicenciaModulo();
        }
    }

    public class AccesoNomina
    {

        //Licencia
        public LicenciaModulo LicenciaAccesoModulo { get; set; }

        public AccesoNomina()
        {
            LicenciaAccesoModulo = new LicenciaModulo();
        }
       
    }

}

