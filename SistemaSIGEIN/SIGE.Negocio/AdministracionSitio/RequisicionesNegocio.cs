using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal; 
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;
using System.Xml.Linq;
using SIGE.Entidades.IntegracionDePersonal;


namespace SIGE.Negocio.Administracion
{
    public class RequisicionNegocio
    {

        public List<SPE_OBTIENE_K_REQUISICION_Result> ObtieneRequisicion(int? pIdRequisicion = null, String pNoRequisicion = null, DateTime? pFeSolicitud = null, DateTime? pFeRequisicion = null, int? pIdPuesto = null, String pClEstado = null, String pClCausa = null, String pDsCausa = null, int? pIdNotificacion = null, int? pIdSolicitante = null, int? pIdAutoriza = null, int? pIdVistoBueno = null, int? pIdEmpresa = null, Guid? flRequisicion=null, Guid? flNotificacion = null, int? pIdCandidato = null,int? pIdRol=null)
		{
            return new RequisicionOperaciones().ObtenerRequisicion(pIdRequisicion, pNoRequisicion, pFeSolicitud, pFeRequisicion, pIdPuesto, pClEstado, pClCausa, pDsCausa, pIdNotificacion, pIdSolicitante, pIdAutoriza, pIdVistoBueno, pIdEmpresa,flRequisicion, flNotificacion, pIdCandidato,pIdRol);
		}     

        public List<SPE_OBTIENE_K_AUTORIZA_REQUISICION_Result> ObtenerAutorizarRequisicion(int? ID_REQUISICION = null, string NO_REQUISICION = null, DateTime? FE_SOLICITUD = null, DateTime? FE_REQUERIMIENTO = null, int? ID_PUESTO = null, string CL_ESTADO = null, string CL_CAUSA = null, string DS_CAUSA = null, int? ID_NOTIFICACION = null, int? ID_SOLICITANTE = null, int? ID_AUTORIZA = null, int? ID_VISTO_BUENO = null, int? ID_EMPRESA = null, string NB_EMPRESA = null, string NB_PUESTO = null, string SOLICITANTE = null, Guid? FL_REQUISICION = null, string CL_TOKEN = null)
        {
            return new RequisicionOperaciones().ObtenerAutorizarRequisicion(ID_REQUISICION, NO_REQUISICION, FE_SOLICITUD, FE_REQUERIMIENTO, ID_PUESTO, CL_ESTADO, CL_CAUSA, DS_CAUSA, ID_NOTIFICACION, ID_SOLICITANTE, ID_AUTORIZA, ID_VISTO_BUENO, ID_EMPRESA, NB_EMPRESA, NB_PUESTO, SOLICITANTE, FL_REQUISICION, CL_TOKEN);
        }

        public E_RESULTADO InsertaActualizaRequisicion(string pTipoTransaccion, E_REQUISICION pRequisicion, string pClUsuario, string pNbPrograma)
		{
            return UtilRespuesta.EnvioRespuesta(new RequisicionOperaciones().InsertarActualizarRequisicion(pTipoTransaccion, pRequisicion, pClUsuario, pNbPrograma));
		}

        public E_RESULTADO ActualizaEstatusRequisicion(bool pFgActualizaRequisicion, bool pFgActualizaPuesto, int pIdRequisicion, int pIdPuesto, string pDsComentariosPuesto, string pDsComentariosRequisicion, string pEstatusPuesto, string pEstatusRequisicion, string pNbPrograma, string pClUsuario)
        {
            return UtilRespuesta.EnvioRespuesta(new RequisicionOperaciones().ActualizaEstatusRequisicion(pFgActualizaRequisicion, pFgActualizaPuesto, pIdRequisicion, pIdPuesto, pDsComentariosPuesto, pDsComentariosRequisicion, pEstatusPuesto, pEstatusRequisicion,pNbPrograma, pClUsuario));
        }

        public E_RESULTADO Elimina_K_REQUISICION(int? ID_REQUISICION = null, string usuario = null, string programa = null)
		{
			return UtilRespuesta.EnvioRespuesta(new RequisicionOperaciones().Elimina_K_REQUISICION(ID_REQUISICION, usuario, programa));
		}

        public E_RESULTADO ActualizaEstatusRequisicion(int? ID_REQUISICION = null, string usuario = null, string programa = null)
		{
            return UtilRespuesta.EnvioRespuesta(new RequisicionOperaciones().ActualizaEstatusRequisicion(ID_REQUISICION, usuario, programa));
		}

        public List<SPE_OBTIENE_CANDIDATOS_POR_REQUISICION_Result> ObtenerCandidatosPorRequisicion(int? pIdRequisicion = null, int? pIdProcesoSeleccion = null, int? pIdCandidato = null, int? pIdSolicitud = null)
        {
            return new RequisicionOperaciones().ObtenerCandidatosPorRequisicion(pIdRequisicion, pIdProcesoSeleccion, pIdCandidato, pIdSolicitud);
        }

        public string ObtieneNumeroRequisicion()
        {
            return new RequisicionOperaciones().ObtenerNumeroRequisicion();
        }

        public List<E_CANDIDATO_IDONEO> BuscaCandidatoRequisicion(int pIdRequisicion, string pXmlParametrosBusqueda)
        {
            return new RequisicionOperaciones().BuscarCandidatoRequisicion(pIdRequisicion, pXmlParametrosBusqueda);
        }

        public List<SPE_OBTIENE_COMPARACION_CANDIDATO_PUESTO_REQUISICION_Result> ObtenerComparacionCandidatoPuesto(int pIdRequisicion, int pIdCandidato, string pXmlParametrosBusqueda)
        {
            return new RequisicionOperaciones().ObtenerComparacionCandidatoPuesto(pIdRequisicion, pIdCandidato, pXmlParametrosBusqueda);
        }

        public E_RESULTADO InsertarCandidatoRequisicion(int pIdCandidato, int pIdEmpleado, int pIdRequisicion, string pClUsuario, string pNbPrograma)
        {
            return UtilRespuesta.EnvioRespuesta(new RequisicionOperaciones().InsertaCandidatoRequisicion(pIdCandidato, pIdEmpleado, pIdRequisicion, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO EliminarCandidatoRequisicion(int pIdRequisicion, int pIdCandidato, string pClUsuario, string pNbPrograma)
        {
            return UtilRespuesta.EnvioRespuesta(new RequisicionOperaciones().EliminaCandidatoRequisicion(pIdRequisicion, pIdCandidato, pClUsuario, pNbPrograma));
        }

    }
}