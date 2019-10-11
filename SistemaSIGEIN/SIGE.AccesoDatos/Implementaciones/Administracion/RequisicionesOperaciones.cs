using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using System.Data.Objects;
using System.Xml.Linq;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.IntegracionDePersonal;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal
{
    public class RequisicionOperaciones
    {

        public List<SPE_OBTIENE_K_REQUISICION_Result> ObtenerRequisicion(int? pIdRequisicion = null, String pNoRequisicion = null, DateTime? pFeSolicitud = null, DateTime? pFeRequisicion = null, int? pIdPuesto = null, String pClEstado = null, String pClCausa = null, String pDsCausa = null, int? pIdNotificacion = null, int? pIdSolicitante = null, int? pIdAutoriza = null, int? pIdVistoBueno = null, int? pIdEmpresa = null, Guid? flRequisicion = null, Guid? flNotificacion = null, int? pIdCandidato = null,int? pIdRol=null)
        {
            using (SistemaSigeinEntities context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_K_REQUISICION(pIdRequisicion, pNoRequisicion, pFeSolicitud, pFeRequisicion, pIdPuesto, pClEstado, pClCausa, pDsCausa, pIdNotificacion, pIdSolicitante, pIdAutoriza, pIdVistoBueno, pIdEmpresa, flRequisicion, flNotificacion, pIdCandidato,pIdRol).ToList();
            }
        }



        public XElement InsertarActualizarRequisicion(string pTipoTransaccion, E_REQUISICION pRequisicion, string pClUsuario, string pNbPrograma)
        {
            using (SistemaSigeinEntities context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));

                context.SPE_INSERTA_ACTUALIZA_K_REQUISICION(pout_clave_retorno,
                    pRequisicion.ID_REQUISICION, pRequisicion.NO_REQUISICION,
                    pRequisicion.FE_SOLICITUD, pRequisicion.FE_REQUERIMIENTO,
                    pRequisicion.ID_PUESTO, pRequisicion.CL_ESTATUS_REQUISICION,
                    pRequisicion.CL_CAUSA, pRequisicion.DS_CAUSA, pRequisicion.DS_TIEMPO_CAUSA,
                    pRequisicion.ID_SOLICITANTE,
                    pRequisicion.NB_EMPLEADO_SOLICITANTE,
                    pRequisicion.NB_PUESTO_SOLICITANTE,
                    pRequisicion.NB_CORREO_SOLICITANTE,
                    pRequisicion.ID_AUTORIZA,
                    pRequisicion.NB_EMPLEADO_AUTORIZA,
                    pRequisicion.NB_PUESTO_AUTORIZA,
                    pRequisicion.NB_CORREO_AUTORIZA,
                    pRequisicion.ID_EMPLEADO_AUTORIZA_PUESTO,
                    pRequisicion.NB_EMPLEADO_AUTORIZA_PUESTO,
                    pRequisicion.NB_EMPLEADO_AUTORIZA_PUESTO_PUESTO,
                    pRequisicion.NB_CORREO_AUTORIZA_PUESTO,
                    pRequisicion.ID_EMPRESA, pRequisicion.CL_TOKEN,
                    pRequisicion.FL_REQUISICION,
                    pRequisicion.ID_EMPLEADO_SUPLENTE, pRequisicion.NB_EMPLEADO_SUPLENTE,
                    pRequisicion.MN_SUELDO, pRequisicion.MN_SUELDO_TABULADOR,
                    pRequisicion.MN_SUELDO_SUGERIDO, pRequisicion.MAX_SUELDO_SUGERIDO,
                    pRequisicion.CL_TOKEN_PUESTO,
                    pRequisicion.FL_NOTIFICACION,
                    pRequisicion.DS_COMENTARIOS,
                    pClUsuario, pNbPrograma, pTipoTransaccion);
                return XElement.Parse(pout_clave_retorno.Value.ToString());

            }
        }

        public XElement Elimina_K_REQUISICION(int? ID_REQUISICION = null, string usuario = null, string programa = null)
        {
            using (SistemaSigeinEntities context = new SistemaSigeinEntities())
            {

                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                pout_clave_retorno.Value = "";


                context.SPE_ELIMINA_K_REQUISICION(pout_clave_retorno, ID_REQUISICION, usuario, programa);
                //regresamos el valor de retorno de sql				

                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }



        public XElement ActualizaEstatusRequisicion(int? ID_REQUISICION = null, string usuario = null, string programa = null)
        {
            using (SistemaSigeinEntities context = new SistemaSigeinEntities())
            {

                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                pout_clave_retorno.Value = "";


                context.SPE_ACTUALIZA_ESTATUS_REQUISICION(pout_clave_retorno, ID_REQUISICION, usuario, programa);
                //regresamos el valor de retorno de sql				

                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_K_AUTORIZA_REQUISICION_Result> ObtenerAutorizarRequisicion(int? ID_REQUISICION = null, string NO_REQUISICION = null, DateTime? FE_SOLICITUD = null, DateTime? FE_REQUERIMIENTO = null, int? ID_PUESTO = null, string CL_ESTADO = null, string CL_CAUSA = null, string DS_CAUSA = null, int? ID_NOTIFICACION = null, int? ID_SOLICITANTE = null, int? ID_AUTORIZA = null, int? ID_VISTO_BUENO = null, int? ID_EMPRESA = null, string NB_EMPRESA = null, string NB_PUESTO = null, string SOLICITANTE = null, Guid? FL_REQUISICION = null, string CL_TOKEN = null)
        {
            using (SistemaSigeinEntities context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_K_AUTORIZA_REQUISICION(FL_REQUISICION, ID_REQUISICION).ToList();
            }
        }

        public XElement ActualizaEstatusRequisicion(bool pFgActualizaRequisicion, bool pFgActualizaPuesto, int pIdRequisicion, int pIdPuesto, string pDsComentariosPuesto, string pDsComentariosRequisicion, string pEstatusPuesto, string pEstatusRequisicion, string pNbPrograma, string pClUsuario)
        {
            using (SistemaSigeinEntities context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ACTUALIZA_ESTADO_REQUISICION(pout_clave_retorno, pFgActualizaRequisicion, pFgActualizaPuesto, pIdRequisicion, pIdPuesto, pDsComentariosPuesto, pDsComentariosRequisicion, pEstatusPuesto, pEstatusRequisicion, pNbPrograma, pClUsuario);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement InsertaCandidatoRequisicion(int pIdCandidato, int pIdEmpleado, int pIdRequisicion, string pClUsuario, string pNbPrograma)
        {
            using (IntegracionDePersonalEntities context = new IntegracionDePersonalEntities())
            {
                ObjectParameter pOutRespuesta = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_CANDIDATO_REQUISICION(pOutRespuesta, pIdCandidato, pIdEmpleado, pIdRequisicion, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutRespuesta.Value.ToString());
            }
        }

        public XElement EliminaCandidatoRequisicion(int pIdRequisicion, int pIdCandidato, string pClUsuario, string pNbPrograma)
        {
            using (IntegracionDePersonalEntities context = new IntegracionDePersonalEntities())
            {
                ObjectParameter pOutRespuesta = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_CANDIDATO_REQUISICION(pOutRespuesta, pIdRequisicion, pIdCandidato, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutRespuesta.Value.ToString());
            }
        }

        public string ObtenerNumeroRequisicion()
        {
            using (SistemaSigeinEntities context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_K_NUMERO_REQUISICION().FirstOrDefault();
            }
        }

        public List<E_CANDIDATO_IDONEO> BuscarCandidatoRequisicion(int pIdRequisicion, string pXmlParametrosBusqueda)
        {
            using (SistemaSigeinEntities context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_BUSQUEDA_CANDIDATO_REQUISICION(pIdRequisicion, pXmlParametrosBusqueda).Select(a => new E_CANDIDATO_IDONEO
                {
                    ID_CANDIDATO = a.ID_CANDIDATO,
                    ID_SOLICITUD = a.ID_SOLICITUD,
                    CL_SOLICITUD = a.CL_SOLICITUD,
                    NB_CANDIDATO = a.NB_CANDIDATO,
                    CL_SOLICITUD_ESTATUS = a.CL_SOLICITUD_ESTATUS,
                    PR_COMPATIBILIDAD_PERFIL = a.PR_COMPATIBILIDAD_PERFIL,
                    PR_COMPATIBILIDAD_COMPETENCIAS = a.PR_COMPATIBILIDAD_COMPETENCIAS,
                    CL_TOKEN = a.CL_TOKEN,
                    ID_BATERIA = a.ID_BATERIA,
                    CL_PAIS = a.CL_PAIS,
                    CL_ESTADO = a.CL_ESTADO,
                    CL_MUNICIPIO = a.CL_MUNICIPIO,
                    CL_COLONIA = a.CL_COLONIA,
                    FE_NACIMIENTO = a.FE_NACIMIENTO,
                    DS_LUGAR_NACIMIENTO = a.DS_LUGAR_NACIMIENTO,
                    CL_NACIONALIDAD = a.CL_NACIONALIDAD,
                    CL_DISPONIBILIDAD_VIAJE = a.CL_DISPONIBILIDAD_VIAJE,
                    DS_DISPONIBILIDAD = a.DS_DISPONIBILIDAD,
                    ID_REQUISICION = a.ID_REQUISICION,
                    ID_PROCESO_SELECCION = a.ID_PROCESO_SELECCION,
                    CL_ESTADO_PROCESO = a.CL_ESTADO_PROCESO,
                    FG_OTRO_PROCESO_SELECCION = a.FG_OTRO_PROCESO_SELECCION,
                    ID_EMPLEADO = a.ID_EMPLEADO,
                    CL_EMPLEADO = a.CL_EMPLEADO,
                    NB_PUESTO = a.NB_PUESTO,
                    CL_ORIGEN = a.CL_ORIGEN
                }).ToList();
            }
        }

        public List<SPE_OBTIENE_COMPARACION_CANDIDATO_PUESTO_REQUISICION_Result> ObtenerComparacionCandidatoPuesto(int pIdRequisicion, int pIdCandidato, string pXmlParametrosBusqueda)
        {
            using (SistemaSigeinEntities context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_COMPARACION_CANDIDATO_PUESTO_REQUISICION(pIdRequisicion, pIdCandidato, pXmlParametrosBusqueda).ToList();
            }
        }

        public List<SPE_OBTIENE_CANDIDATOS_POR_REQUISICION_Result> ObtenerCandidatosPorRequisicion(int? pIdRequisicion = null, int? pIdProcesoSeleccion = null, int? pIdCandidato = null, int? pIdSolicitud = null)
        {
            using (SistemaSigeinEntities context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_CANDIDATOS_POR_REQUISICION(pIdRequisicion, pIdProcesoSeleccion, pIdCandidato, pIdSolicitud).ToList();
            }
        }
    }
}