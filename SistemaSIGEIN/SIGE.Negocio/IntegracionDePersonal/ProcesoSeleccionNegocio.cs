using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.Negocio.IntegracionDePersonal
{
    public class ProcesoSeleccionNegocio
    {
        public E_RESULTADO InsertaProcesoSeleccion(int? pIdCandidato = null, int? pIdRequisicion = null, string pClUsuario = null, string pNbPrograma = null)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return UtilRespuesta.EnvioRespuesta(oProcesoSeleccion.InsertarProcesoSeleccion(pIdCandidato, pIdRequisicion, pClUsuario, pNbPrograma));
        }

        public List<SPE_OBTIENE_PROCESO_SELECCION_Result> ObtieneProcesoSeleccion(int? pIdProcesoSeleccion = null, int? pIdCandidato = null, int? pIdRequisicion = null, int? pIdProcesoSeleccionActual = null)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return oProcesoSeleccion.ObtenerProcesoSeleccion(pIdProcesoSeleccion, pIdCandidato, pIdRequisicion, pIdProcesoSeleccionActual);
        }

        public List<SPE_OBTIENE_ENTREVISTA_PROCESO_SELECCION_Result> ObtieneEntrevistaProcesoSeleccion(int? pIdProcesoSeleccion = null, int? pIdEntrevista = null, Guid? pFlEntrevista = null)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return oProcesoSeleccion.ObtenerEntrevistasProcesoSeleccion(pIdProcesoSeleccion, pIdEntrevista, pFlEntrevista);
        }

        public List<SPE_OBTIENE_TIPO_ENTREVISTA_Result> ObtieneTipoEntrevista(int? pIdEntrevistaTipo = null, string pClEntrevistaTipo = null, string pNbEntrevistaTipo = null)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return oProcesoSeleccion.ObtenerTipoEntrevista(pIdEntrevistaTipo, pClEntrevistaTipo, pNbEntrevistaTipo);
        }

        public E_RESULTADO InsertaActualizaEntrevista(string pClTipoOperacion, E_ENTREVISTA pEntrevista, string pClUsuario, string pNbPrograma)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return UtilRespuesta.EnvioRespuesta(oProcesoSeleccion.InsertarActualizarEntrevista(pClTipoOperacion, pEntrevista, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO EliminaEntrevista(int pIdEntrevista)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return UtilRespuesta.EnvioRespuesta(oProcesoSeleccion.EliminarEntrevista(pIdEntrevista));
        }

        public E_RESULTADO ActualizaComentarioEntrevista(int pIdEntrevista, string pDsObservaciones, string pClUsuario, string pNbPrograma)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return UtilRespuesta.EnvioRespuesta(oProcesoSeleccion.ActualizarComentarioEntrevista(pIdEntrevista, pDsObservaciones, pClUsuario, pNbPrograma));
        }

        public List<SPE_OBTIENE_K_EXPERIENCIA_LABORAL_Result> ObtieneExperienciaLaboral(int? pIdExperienciaLaboral = null, int? pIdCandidato = null, int? pIdEmpleado = null, String pNbEmpresa = null, String pDsDomicilio = null, String pNbGiro = null, DateTime? pFeInicio = null, DateTime? pFeFin = null, String pNbPuesto = null, String pNbFuncion = null, String pDsFunciones = null, Decimal? pMnPrimerSueldo = null, Decimal? pMnUltimoSueldo = null, String pClTipoContrato = null, String pClTipoContratoOtro = null, String pNoTelefonoContacto = null, String pClCorreoElectronico = null, String pNbContacto = null, String pNbPuestoContacto = null, bool? pClInformacionConfirmada = null, String pDsComentarios = null)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return oProcesoSeleccion.ObtenerExperienciaLaboral(pIdExperienciaLaboral, pIdCandidato, pIdEmpleado, pNbEmpresa, pDsDomicilio, pNbGiro, pFeInicio, pFeFin, pNbPuesto, pNbFuncion, pDsFunciones, pMnPrimerSueldo, pMnUltimoSueldo, pClTipoContrato, pClTipoContratoOtro, pNoTelefonoContacto, pClCorreoElectronico, pNbContacto, pNbPuestoContacto, pClInformacionConfirmada, pDsComentarios);
        }

        public E_RESULTADO ActualizaReferenciasExperienciaLaboral(string pXmlReferencias, string pClUsuario, string pNbPrograma)
        {
            ProcesoSeleccionOperaciones oProcesoseleccion = new ProcesoSeleccionOperaciones();
            return UtilRespuesta.EnvioRespuesta(oProcesoseleccion.ActualizarReferenciasExperienciaLaboral(pXmlReferencias, pClUsuario, pNbPrograma));
        }

        public List<E_COMPETENCIAS_PROCESO_SELECCION> ObtieneCompetenciasProcesoSeleccion(int pIdCandidato, int? pIdPuesto)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return oProcesoSeleccion.ObtenerCompetenciasProcesoSeleccion(pIdCandidato, pIdPuesto);
        }

        public E_RESULTADO InsertaActualizaDocumentos(int pID_CANDIDATO, List<UDTT_ARCHIVO> pLstArchivoTemporales, List<E_DOCUMENTO> pLstDocumentos, string pClUsuario, string pNbPrograma)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return UtilRespuesta.EnvioRespuesta(oProcesoSeleccion.InsertarActualizarDocumentos(pID_CANDIDATO, pLstArchivoTemporales, pLstDocumentos, pClUsuario, pNbPrograma));
        }

        public SPE_OBTIENE_DOCUMENTO_PROCESO_Result ObtenieneDocumentoProceso(int? pIdCandidato = null)
        {
            ProcesoSeleccionOperaciones oProceso = new ProcesoSeleccionOperaciones();
            SPE_OBTIENE_DOCUMENTO_PROCESO_Result vDocumentoProceso = oProceso.ObtenerDocumentoProceso(pIdCandidato);
            if (vDocumentoProceso.XML_DOCUMENTOS != null)
            {
                XElement vDocumento = XElement.Parse(vDocumentoProceso.XML_DOCUMENTOS);
                vDocumentoProceso.XML_DOCUMENTOS = vDocumento.ToString();
            }
            else {
                vDocumentoProceso.XML_DOCUMENTOS = "";
            }

            return vDocumentoProceso;
        }

        public E_RESULTADO_MEDICO ObtieneResultadoMedico(int? pIdResultadoMedico = null, int? pIdCandidato = null, int? pIdEmpleado = null, int? pIdProcesoSeleccion = null)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return oProcesoSeleccion.ObtenerResultadoMedico(pIdResultadoMedico, pIdCandidato, pIdEmpleado, pIdProcesoSeleccion);
        }

        public E_RESULTADO InsertaActualizaResultadoMedico(E_RESULTADO_MEDICO vResultado, string pNbPrograma, string pClUsuario, string pTipoTransaccion, int? pIdResultadoMedico = null)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return UtilRespuesta.EnvioRespuesta(oProcesoSeleccion.InsertarActualizaResultadoMedico(vResultado, pNbPrograma, pClUsuario, pTipoTransaccion, pIdResultadoMedico));
        }

        public List<SPE_OBTIENE_CONSULTA_APLICACION_PRUEBA_Result> ObtieneAplicacionPrueba(int? pID_CANDIDATO = null)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return oProcesoSeleccion.ObtenerAplicacionPrueba(pID_CANDIDATO);
        }

        public List<SPE_OBTIENE_C_CATALOGO_VALOR_Result> ObtieneCatalogoValor(int? pIdCatalogoValor = null, String pClCatalogoValor = null, String pNbCatalogoValor = null, String pDsCatalogoValor = null, int? pIdCatalogoLista = null){
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return oProcesoSeleccion.ObtenerCatalogoValor(pIdCatalogoValor, pClCatalogoValor, pNbCatalogoValor, pDsCatalogoValor, pIdCatalogoLista);
        }

        public List<SPE_OBTIENE_BITACORA_SOLICITUD_Result> ObtieneBitacoraSolicitud(int? pID_CANDIDATO = null)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return oProcesoSeleccion.ObtenerBitacoraSolicitud(pID_CANDIDATO);

        }

        public E_ESTUDIO_SOCIOECONOMICO ObtieneEstudioSocioeconomico(int? pIdEstudioSocioeconomico = null, int? pIdProcesoSeleccion = null, int? pIdEmpleado = null, int? pIdCandidato = null)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return oProcesoSeleccion.ObtenerEstudioSocioeconomico(pIdEstudioSocioeconomico, pIdProcesoSeleccion, pIdEmpleado, pIdCandidato);
        }

        public E_RESULTADO InsertaActualizaEstudioSocioeconomico(E_ESTUDIO_SOCIOECONOMICO vEstudio, string pClUsuario = null, string pNbPrograma = null, string pTipoTransaccion = null, int? pIdEstudioSocioeconomico = null)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return UtilRespuesta.EnvioRespuesta(oProcesoSeleccion.InsertarActualizarEstudioSocioeconomico(vEstudio, pClUsuario, pNbPrograma, pTipoTransaccion, pIdEstudioSocioeconomico));
        }



        public E_ES_DATOS_LABORALES ObtieneESDatosLaborales(int? pIdDatoLaboral = null, int? pIdEstudioSocioeconomico = null)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return oProcesoSeleccion.ObtenerESDatosLaborales(pIdDatoLaboral, pIdEstudioSocioeconomico);
        }

        public E_RESULTADO InsertaActualizaESDatosLaborales(E_ES_DATOS_LABORALES vDatosLaborales, string pClUsuario, string pNbPrograma, string pTipoTransaccion, int? pIdDatoLaboral = null)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return UtilRespuesta.EnvioRespuesta(oProcesoSeleccion.InsertarActualizarESDatosLaborales(vDatosLaborales, pClUsuario, pNbPrograma, pTipoTransaccion, pIdDatoLaboral));
        }

        public E_ES_DATOS_ECONOMICOS ObtieneESDatosEconomicos(int? pIdDatoPropiedad = null, int? pIdEstudioSocioeconomico = null)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return oProcesoSeleccion.ObtenerESDatosEconomicos(pIdDatoPropiedad, pIdEstudioSocioeconomico);
        }

        public E_RESULTADO InsertaActualizaESDatosEconomicos(E_ES_DATOS_ECONOMICOS pDatoEconomico, string pClUsuario, string pNbPrograma, string pTipoTransaccion, int? pIdDatoPropiedad = null)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return UtilRespuesta.EnvioRespuesta(oProcesoSeleccion.InsertarActualizarESDatosEconomicos(pDatoEconomico, pClUsuario, pNbPrograma, pTipoTransaccion, pIdDatoPropiedad));
        }

        public E_ES_DATOS_VIVIENDA ObtieneESDatosVivienda(int? pIdDatoVivienda = null, int? pIdEstudioSocioeconomico = null)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return oProcesoSeleccion.ObtenerESDatosVivienda(pIdDatoVivienda, pIdEstudioSocioeconomico);
        }

        public E_RESULTADO InsertaActualizaESDatosVivienda(E_ES_DATOS_VIVIENDA pDatosVivienda, string pClUsuario, string pNbPrograma, string pTipoTransaccion, int? pIdDatoVivienda = null)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return UtilRespuesta.EnvioRespuesta(oProcesoSeleccion.InsertarActualizarESDatosVivienda(pDatosVivienda, pClUsuario, pNbPrograma, pTipoTransaccion, pIdDatoVivienda));
        }
        public E_RESULTADO InsertaActualizaCopiaSocioEconomico(int idCandidato, int idProceso, string pClUsuario, string pNbPrograma)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return UtilRespuesta.EnvioRespuesta(oProcesoSeleccion.InsertarActualizaCopiaSocioEconomico(idCandidato, idProceso, pClUsuario, pNbPrograma));
        }
        //dependientes
        public E_RESULTADO EliminaDependientes(int pIdDependiente)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return UtilRespuesta.EnvioRespuesta(oProcesoSeleccion.EliminarDependiente(pIdDependiente));
        }

        public E_RESULTADO InsertaDependientes(int pIdEstudioSocioEconomico, string nbPariente, string clParentesco, string clGenero, DateTime fechaNacimiento, int idBitacora, string ocupacion, bool fgDependiente, bool fgActivo, string pClUsuario, string pNbPrograma)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return UtilRespuesta.EnvioRespuesta(oProcesoSeleccion.InsertarDependientes(pIdEstudioSocioEconomico, nbPariente, clParentesco, clGenero, fechaNacimiento, idBitacora, ocupacion, fgDependiente, fgActivo, pClUsuario, pNbPrograma));
        }

        public List<SPE_OBTIENE_EST_SOC_DEPENDIENTE_Result> ObtieneDependientes(int? pIdEstudioSocioEconomico = null)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return oProcesoSeleccion.ObtenerDependientes(pIdEstudioSocioEconomico);
        }

        public List<SPE_OBTIENE_COMENTARIOS_ENTREVISTAS_Result> ObtieneComentariosEntrevistaProcesoSeleccion(int? pIdCandidato = null, int? pIdProceso = null)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return oProcesoSeleccion.ObtenerCometariosEntrevistasProcesoSeleccion(pIdCandidato, pIdProceso);
        }

        public E_RESULTADO TerminaProcesoSeleccion(int pIdProcesoSeleccion, string pDsObservacionesTermino, string pClUsuario, string pNbPrograma)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return UtilRespuesta.EnvioRespuesta(oProcesoSeleccion.TerminaProcesoSeleccion(pIdProcesoSeleccion, pDsObservacionesTermino, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO CopiaDatosProcesoSeleccion(int pIdProcesoSeleccionOrigen, int pIdProcesoSeleccionDestino, string pXmlConfiguracion, string pClUsuario, string pNbPrograma)
        {
            ProcesoSeleccionOperaciones oProcesoSeleccion = new ProcesoSeleccionOperaciones();
            return UtilRespuesta.EnvioRespuesta(oProcesoSeleccion.CopiarDatosProcesoSeleccion(pIdProcesoSeleccionOrigen, pIdProcesoSeleccionDestino, pXmlConfiguracion, pClUsuario, pNbPrograma));
        }
    }
}
