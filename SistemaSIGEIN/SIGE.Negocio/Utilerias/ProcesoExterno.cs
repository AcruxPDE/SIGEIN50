using SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo;
using SIGE.AccesoDatos.Implementaciones.EvaluacionOrganizacional;
using SIGE.Entidades;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Entidades.EvaluacionOrganizacional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Negocio.IntegracionDePersonal;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.EvaluacionOrganizacional;

namespace SIGE.Negocio.Utilerias
{
    [Serializable]
    public class ProcesoExterno
    {
        public string NombreProceso { get; set; }
        public string NumeroRequisicion { get; set; }
        public string UsuarioProceso { get; set; }
        private string ContraseñaProceso { get; set; }
        public string ContraseñaUsuario { get; set; }
        public int IdProceso { get; set; }
        public int IdPeriodo { get; set; }
        public string FlRequisicion { get; set; }
        public string Url { get; set; }
        public Guid FolioProceso { get; set; }
        public DateTime FechaInicialProceso { get; set; }
        public DateTime FechaFinalProceso { get; set; }
        public bool fgCuestionarioContestado { get; set; }
        public string clEstatusProceso { get; set; }

        public string MensajeError { get; set; }

        /// <summary>
        /// Envio de correo para el proceso externo. En el mensaje del correo debe de venir ya la contraseña y la url para el proceso externo.
        /// </summary>
        /// <param name="Email">Correo electrónico al que se va a enviar.</param>
        /// <param name="Nombre">Nombre de la persona propietaria del correo.</param>
        /// <param name="Titulo">Titulo del correo electrónico</param>
        /// <param name="Mensaje">Mensaje principal del correo.</param>
        /// <returns>Regresa un boleano en caso de que el correo haya sido enviado o no.</returns>
        public bool EnvioCorreo(string Email, string Nombre, string Titulo, string Mensaje)
        {
            bool resultado;

            Mail mail = new Mail(ContextoApp.mailConfiguration);
            try
            {
                mail.addToAddress(Email, Nombre);
                mail.Send(Titulo, Mensaje);
                resultado = true;
            }
            catch (Exception)
            {
                resultado = false;
            }

            return resultado;
        }

        /// <summary>
        /// Obtener la información del proceso que se realizara
        /// </summary>
        /// <param name="Proceso">Clave del proceso.</param>
        /// <param name="Folio">Folio correspondiente al proceso. Con este folio se obtienen los datos a mostrar.</param>
        /// <returns>Regresa un booleano en caso de que no exista información dependiendo del folio y el proceso.</returns>
        public bool ObtenerInformacionProceso(string Proceso)
        {
            bool resultado;


            switch (Proceso)
            {
                case "EVALUACION":

                    EventoCapacitacionOperaciones op = new EventoCapacitacionOperaciones();

                    E_EVENTO evento = op.ObtenerEventos(FL_EVENTO: FolioProceso).FirstOrDefault();

                    if (evento != null)
                    {
                        IdProceso = evento.ID_EVENTO;
                        NombreProceso = evento.NB_EVENTO;
                        UsuarioProceso = evento.NB_EVALUADOR;
                        ContraseñaProceso = evento.CL_TOKEN;
                        FechaInicialProceso = evento.FE_EVALUACION.Value;
                        FechaFinalProceso = evento.FE_EVALUACION.Value;
                       // FechaFinalProceso = evento.FE_TERMINO;
                        MensajeError = "";
                        resultado = true;
                    }
                    else
                    {
                        MensajeError = "El evento no existe.";
                        resultado = false;
                    }
                    break;

                case "CUESTIONARIOS":
                    PeriodoOperaciones oPeriodo = new PeriodoOperaciones();

                    SPE_OBTIENE_FYD_PERIODO_EVALUADOR_Result oPeriodoEvaluador = oPeriodo.ObtenerPeriodoEvaluador(pFlEvaluador: FolioProceso);

                    if (oPeriodoEvaluador != null)
                    {
                        IdProceso = oPeriodoEvaluador.ID_EVALUADOR;
                        NombreProceso = oPeriodoEvaluador.NB_PERIODO;
                        UsuarioProceso = oPeriodoEvaluador.NB_EVALUADOR;
                        ContraseñaProceso = oPeriodoEvaluador.CL_TOKEN;
                        fgCuestionarioContestado = oPeriodoEvaluador.FG_CONTESTADO;
                        clEstatusProceso = oPeriodoEvaluador.CL_ESTADO_PERIODO;
                        MensajeError = "";
                        resultado = true;
                    }
                    else
                    {
                        MensajeError = "El periodo no existe.";
                        resultado = false;
                    }

                    break;

                case "CLIMALABORAL":

                    ClimaLaboralOperaciones oClima = new ClimaLaboralOperaciones();
                    SPE_OBTIENE_EO_PERIODO_EVALUADOR_Result oClimaEvaluador = oClima.ObtenerPeriodoEvaluador(pFlEvaluador: FolioProceso);

                    if (oClimaEvaluador != null)
                    {
                        IdProceso = oClimaEvaluador.ID_EVALUADOR;
                        IdPeriodo = oClimaEvaluador.ID_PERIODO;
                        NombreProceso = oClimaEvaluador.NB_PERIODO;
                        UsuarioProceso = oClimaEvaluador.NB_EVALUADOR;
                        ContraseñaProceso = oClimaEvaluador.CL_TOKEN;
                        fgCuestionarioContestado = oClimaEvaluador.FG_CONTESTADO;
                        MensajeError = "";
                        resultado = true;
                    }
                    else
                    {
                        MensajeError = "El periodo no existe.";
                        resultado = false;
                    }
                    break;

                case "ENTREVISTA_SELECCION":

                    ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();
                    var vProcesoSeleccion = nProcesoSeleccion.ObtieneEntrevistaProcesoSeleccion(pFlEntrevista: FolioProceso).FirstOrDefault();

                    if (vProcesoSeleccion != null)
                    {
                        IdProceso = vProcesoSeleccion.ID_ENTREVISTA;
                        IdPeriodo = vProcesoSeleccion.ID_PROCESO_SELECCION;
                        UsuarioProceso = vProcesoSeleccion.NB_ENTREVISTADOR;
                        ContraseñaProceso = vProcesoSeleccion.CL_TOKEN;

                        resultado = true;
                    }
                    else
                    {
                        MensajeError = "La entrevista no existe.";
                        resultado = false;
                    }

                    break;
                case "NOTIFICACIONRRHH":

                    RequisicionNegocio nNotificacion = new RequisicionNegocio();
                    var vNotificacion = nNotificacion.ObtieneRequisicion(flNotificacion: FolioProceso).FirstOrDefault();

                    if (vNotificacion != null)
                    {
                        IdProceso = (int)vNotificacion.ID_PUESTO;
                        IdPeriodo = (int)vNotificacion.ID_REQUISICION;
                        NombreProceso = vNotificacion.NB_PUESTO;
                        ContraseñaProceso = vNotificacion.CL_TOKEN_PUESTO;
                        FlRequisicion = vNotificacion.NO_REQUISICION;
                        clEstatusProceso = vNotificacion.CL_ESTATUS_PUESTO;
                        resultado = true;
                    }
                    else
                    {
                        MensajeError = "El puesto no existe.";
                        resultado = false;
                    }

                    break;

                case "AUTORIZAREQUISICION":

                    RequisicionNegocio nARequisicion = new RequisicionNegocio();
                    var vAutorizaRequisicion = nARequisicion.ObtenerAutorizarRequisicion(FL_REQUISICION: FolioProceso).FirstOrDefault();

                    if (vAutorizaRequisicion != null)
                    {
                        IdProceso = vAutorizaRequisicion.ID_REQUISICION;
                        FlRequisicion = vAutorizaRequisicion.NO_REQUISICION;
                        IdPeriodo = Convert.ToInt32(vAutorizaRequisicion.ID_REQUISICION);
                        NombreProceso = vAutorizaRequisicion.NB_PUESTO;
                        ContraseñaProceso = vAutorizaRequisicion.CL_TOKEN_REQUISICION;
                        clEstatusProceso = vAutorizaRequisicion.CL_ESTATUS_REQUISICION;

                        resultado = true;
                    }
                    else
                    {
                        MensajeError = "La notificación no existe.";
                        resultado = false;
                    }
                    break;

                case "AUTORIZAREQPUESTO":

                    RequisicionNegocio nAReqPuesto = new RequisicionNegocio();
                    var vRequisicion = nAReqPuesto.ObtenerAutorizarRequisicion(FL_REQUISICION: FolioProceso).FirstOrDefault();

                    if (vRequisicion != null)
                    {
                        IdProceso = vRequisicion.ID_REQUISICION;
                        FlRequisicion = vRequisicion.NO_REQUISICION;
                        IdPeriodo = Convert.ToInt32(vRequisicion.ID_REQUISICION);
                        NombreProceso = vRequisicion.NB_PUESTO;
                        ContraseñaProceso = vRequisicion.CL_TOKEN_REQUISICION;
                        clEstatusProceso = vRequisicion.CL_ESTATUS_REQUISICION.Equals("AUTORIZADO") & vRequisicion.CL_ESTATUS_PUESTO.Equals("AUTORIZADO") ? "AUTORIZADO" : "FALTA";

                        resultado = true;
                    }
                    else
                    {
                        MensajeError = "La requisición no existe";
                        resultado = false;
                    }
                    break;

                case "DESEMPENO":

                    PeriodoDesempenoNegocio oDesempeno = new PeriodoDesempenoNegocio();
                    SPE_OBTIENE_EO_PERIODO_EVALUADOR_DESEMPENO_Result oDesempenoEvaluador = oDesempeno.ObtenerPeriodoEvaluadorDesempeno(pFL_EVALUADOR: FolioProceso);

                    if (oDesempenoEvaluador != null)
                    {
                        IdProceso = oDesempenoEvaluador.ID_EVALUADOR;
                        IdPeriodo = oDesempenoEvaluador.ID_PERIODO;
                        NombreProceso = oDesempenoEvaluador.NB_PERIODO;
                        UsuarioProceso = oDesempenoEvaluador.NB_EVALUADOR;
                        ContraseñaProceso = oDesempenoEvaluador.CL_TOKEN;
                        clEstatusProceso = oDesempenoEvaluador.CL_ESTATUS_CAPTURA;
                        MensajeError = "";
                        resultado = true;
                    }
                    else
                    {
                        MensajeError = "El periodo no existe.";
                        resultado = false;
                    }
                    break;
                case "CUESTIONARIO":

                    PeriodoDesempenoNegocio oDesempenos = new PeriodoDesempenoNegocio();
                    SPE_OBTIENE_EO_PERIODO_EVALUADOR_DESEMPENO_Result oDesempenoEvaluadores = oDesempenos.ObtenerPeriodoEvaluadorDesempeno(pFL_EVALUADOR: FolioProceso);

                    if (oDesempenoEvaluadores != null)
                    {
                        IdProceso = oDesempenoEvaluadores.ID_EVALUADOR;
                        IdPeriodo = oDesempenoEvaluadores.ID_PERIODO;
                        NombreProceso = oDesempenoEvaluadores.NB_PERIODO;
                        UsuarioProceso = oDesempenoEvaluadores.NB_EVALUADOR;
                        ContraseñaProceso = oDesempenoEvaluadores.CL_TOKEN;
                        MensajeError = "";
                        resultado = true;
                    }
                    else
                    {
                        MensajeError = "El periodo no existe.";
                        resultado = false;
                    }
                    break;

                default:
                    resultado = false;
                    MensajeError = "No se encontró el proceso especificado";
                    break;
            }


            return resultado;
        }

        /// <summary>
        /// Valida la contraseña que ingreso el usuario para construir la Url a la que navegará el usuario segu el proceso.
        /// </summary>
        /// <param name="Proceso">Clave del proceso.</param>
        /// <param name="ContraseñaUsuario">Contraseña ingresada por el usuario.</param>
        /// <returns>Regresa un boleano en caso de que la contraseña sea o no valida.</returns>
        public bool EjecutarProceso(string Proceso)
        {
            bool resultado;

            switch (Proceso)
            {
                case "EVALUACION":

                    if (ValidarDatosEvaluacion())
                    {
                        NavegacionEvaluacionEvento();
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                    }

                    break;
                case "CUESTIONARIOS":

                    if (ValidarDatosPeriodoEvaluacion())
                    {
                        NavegacionPeriodoEvaluacion();
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                    }


                    break;

                case "CLIMALABORAL":

                    if (ValidarDatosClimaLaboral())
                    {
                        NavegacionPeriodoClimaLaboral();
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                    }
                    break;
               
                case "ENTREVISTA_SELECCION":

                    if (ValidarDatosPeriodoEvaluacion())
                    {
                        NavegacionComentariosEntrevista();
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                    }

                    break;
                case "NOTIFICACIONRRHH":

                    if (ValidarDatosNotificacion())
                    {
                        NavegacionNotificacion(Proceso);
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                    }

                    break;

                case "AUTORIZAREQUISICION":

                    if (ValidarDatosRequisicion())
                    {
                        NavegacionAutorizaRequisicion(Proceso);
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                    }

                    break;

                case "AUTORIZAREQPUESTO":

                    if (ValidarDatosRequisicionPuesto())
                    {
                        NavegacionAutorizaRequisicionPuesto(Proceso);
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                    }

                    break;
                    
                case "DESEMPENO":

                    if (ValidarDatosDesempeno())
                    {
                        NavegacionPeriodoDesempeno();
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                    }
                    break;

                case "CUESTIONARIO":

                    if (ValidarDatosDesempeno())
                    {
                        NavegacionCuestionario();
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                    }
                    break;

                default:
                    MensajeError = "No se encontro el proceso especificado";
                    resultado = false;
                    break;


            }

            return resultado;
        }

        private bool ValidarDatosPeriodoEvaluacion()
        {
            if (ContraseñaProceso != null)
            {
                if (!ContraseñaProceso.Equals(ContraseñaUsuario))
                {
                    MensajeError = "La contraseña no es correcta. Revise por favor.";
                    return false;
                }
                if (fgCuestionarioContestado == true)
                {
                    MensajeError = "El cuestionario ya ha sido contestado.";
                    return false;
                }

                if (clEstatusProceso == "CERRADO")
                {
                    MensajeError = "El periodo de evaluación ya no está activo.";
                    return false;
                }
                
            }
            else
            {
                MensajeError = "El proceso no existe.";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validar los datos del evento.
        /// </summary>
        /// <returns></returns>
        private bool ValidarDatosEvaluacion()
        {

            if (!ContraseñaProceso.Equals(ContraseñaUsuario))
            {
                MensajeError = "La contraseña no es correcta. Revise por favor.";
                return false;
            }

            if (DateTime.Now.Date < FechaInicialProceso)
            {
                MensajeError = "El periodo de evaluación comienza hasta el dia " + FechaInicialProceso.ToString("dd/MM/yyyy");
                return false;
            }

            if (DateTime.Now.Date > FechaFinalProceso)
            {
                MensajeError = "El periodo de evaluación concluyó el " + FechaFinalProceso.ToString("dd/MM/yyyy");
                return false;
            }

            return true;
        }

        private bool ValidarDatosClimaLaboral()
        {

            if (!ContraseñaProceso.Equals(ContraseñaUsuario))
            {
                MensajeError = "La contraseña no es correcta. Revise por favor.";
                return false;
            }
            if (fgCuestionarioContestado == true)
            {
                MensajeError = "El cuestionario ya ha sido contestado.";
                return false;
            }

            return true;
        }

        private bool ValidarDatosEntrevista()
        {
            if (!ContraseñaProceso.Equals(ContraseñaUsuario))
            {
                MensajeError = "La contraseña no es correcta. Revisa por favor.";
                return false;
            }

            return true;
        }

        private bool ValidarDatosNotificacion()
        {
            if (!ContraseñaProceso.Equals(ContraseñaUsuario))
            {
                MensajeError = "La contraseña no es correcta. Revisa por favor.";
                return false;
            }

            if (clEstatusProceso == "AUTORIZADO")
            {
                MensajeError = "El puesto ya fue autorizado";
                return false;
            }

            return true;
        }

        private bool ValidarDatosRequisicion()
        {
            if (!ContraseñaProceso.Equals(ContraseñaUsuario))
            {
                MensajeError = "La contraseña no es correcta. Revisa por favor.";
                return false;
            }

            if (clEstatusProceso == "AUTORIZADO")
            {
                MensajeError = "La requisición ya esta autorizada.";
                return false;
            }

            //if (clEstatusProceso == "RECHAZADO")
            //{
            //    MensajeError = "La requisición fue rechazada";
            //    return false;
            //}

            return true;
        }

        private bool ValidarDatosRequisicionPuesto()
        {
            if (!ContraseñaProceso.Equals(ContraseñaUsuario))
            {
                MensajeError = "La contraseña no es correcta. Revisa por favor.";
                return false;
            }

            if (clEstatusProceso == "AUTORIZADO")
            {
                MensajeError = "La requisición ya esta autorizada.";
                return false;
            }

            //if (clEstatusProceso == "RECHAZADO")
            //{
            //    MensajeError = "La requisición fue rechazada";
            //    return false;
            //}

            return true;
        }

        private bool ValidarDatosDesempeno()
        {
            if (ContraseñaProceso != (ContraseñaUsuario))
            {
                MensajeError = "La contraseña no es correcta. Revisa por favor.";
                return false;
            }

            if (clEstatusProceso.Equals("TERMINADO"))
            {
                    MensajeError = "El proceso de captura ha concluido. Muchas gracias por tu ayuda.";
                return false;
            }
            return true;
        }

        /// <summary>
        /// Construye la URL para la evaluacion del evento.
        /// </summary>
        /// 
        private void NavegacionEvaluacionEvento()
        {
            Url = "FYD/EvaluacionCompetencia/VentanaEventoEvaluacionResultados.aspx?IdEvento=" + IdProceso + "&TOKEN=" + FolioProceso.ToString();
        }

        private void NavegacionPeriodoEvaluacion()
        {
            Url = "FYD/EvaluacionCompetencia/Cuestionarios.aspx?ID_EVALUADOR=" + IdProceso + "&TOKEN=" + FolioProceso.ToString();
        }

        private void  NavegacionPeriodoClimaLaboral()
        {
            Url = "EO/Cuestionarios/CuestionarioClimaLaboralExterno.aspx?ID_EVALUADOR=" + IdProceso + "&TOKEN=" + FolioProceso.ToString() + "&ID_PERIODO=" + IdPeriodo;
        }

        private void NavegacionComentariosEntrevista()
        {
            Url = "IDP/Solicitud/ComentariosEntrevista.aspx?IdProcesoSeleccion=" + IdPeriodo + "&IdEntrevista=" + IdProceso;
        }

        private void NavegacionNotificacion(string Proceso)
        {
            Url = "IDP/Requisicion/VentanaAutorizaRequisicion.aspx?IdRequisicion=" + IdPeriodo + "&ClProceso=" + Proceso;
        }

        private void NavegacionAutorizaRequisicion(string Proceso)
        {
            Url = "IDP/Requisicion/VentanaAutorizaRequisicion.aspx?IdRequisicion=" + IdProceso + "&ClProceso=" + Proceso;
        }

        private void NavegacionAutorizaRequisicionPuesto(string Proceso)
        {
            Url = "IDP/Requisicion/VentanaAutorizaRequisicion.aspx?IdRequisicion=" + IdProceso + "&ClProceso=" + Proceso;
        }

        private void NavegacionPeriodoDesempeno()
        {
            Url = "EO/Cuestionarios/VentanaCalificaEvaluadorEvaluado.aspx?IdEvaluador=" + IdProceso + "&TOKEN=" + FolioProceso.ToString() + "&ID_PERIODO=" + IdPeriodo;
        }

        private void NavegacionCuestionario()
        {
            Url = "EO/Cuestionarios/Cuestionarios.aspx?ID_EVALUADOR=" + IdProceso + "&TOKEN=" + FolioProceso.ToString() + "&ID_PERIODO=" + IdPeriodo;
        }
    }
}
