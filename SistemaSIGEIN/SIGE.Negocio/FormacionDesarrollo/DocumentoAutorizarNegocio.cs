using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;

namespace SIGE.Negocio.FormacionDesarrollo
{
    public class DocumentoAutorizarNegocio
    {

        #region Obtiene datos K_PROGRAMA
        public List<SPE_OBTIENE_C_DOCUMENTO_AUTORIZACION_Result> ObtieneDocumentoAutorizacion(int? pIdDocumentoAutorizacion = null, string pClDocumento = null, string pNbDocumento = null, string pClTipoDocumento = null, string pVersion = null, DateTime? pFechaElaboracion = null, DateTime? pFechaRevision = null, int? nbEmpleadoElabora = null)
        {
            DocumentoAutorizarOperaciones oPrograma = new DocumentoAutorizarOperaciones();
            return oPrograma.ObtenerDocumentoAutorizacion(pIdDocumentoAutorizacion, pClDocumento, pNbDocumento, pClTipoDocumento, pVersion, pFechaElaboracion, pFechaRevision, nbEmpleadoElabora);
        }
        #endregion


        #region inserta actualiza datos  K_PROGRAMA
        public E_RESULTADO InsertaActualizaDocumentoAutorizacion(int? pIdDocumento = null, int? pIdPeriodo = null, int? pIdPrograma = null, string pXmlDatos = null, string pClUsuario = null, string pNbPrograma = null)
        {
            DocumentoAutorizarOperaciones oDocumento = new DocumentoAutorizarOperaciones();
            return UtilRespuesta.EnvioRespuesta(oDocumento.InsertarActualizarDocumentoAutorizacion(pIdDocumento, pIdPeriodo, pIdPrograma, pXmlDatos, pClUsuario, pNbPrograma));
        }
        #endregion


        #region Obtiene datos K_PROGRAMA
        public XElement ObtieneTablasDocumentoAutorizacion(int? pIdPrograma = null, int? pIdPeriodo = null)
        {
            DocumentoAutorizarOperaciones oPrograma = new DocumentoAutorizarOperaciones();
            return oPrograma.ObtenerTablasDocumentoAutorizacion(pIdPrograma, pIdPeriodo);
        }
        #endregion


        #region Obtiene datos SPE_OBTIENE_K_AUTORIZACION_DCTO_EMPLEADO
        public List<SPE_OBTIENE_C_AUTORIZACION_DCTO_EMPLEADO_Result> ObtieneEmpleadoDocumentoAutorizacion(int? pIdAutorizacion = null, Guid? pFlAutorizacion = null, int? pIdEmpleado = null, string pNbEmpleado = null, int? pIdDocumento  = null, string pClEstado = null, DateTime? pFechaAutorizacion = null)
        {
            DocumentoAutorizarOperaciones oPrograma = new DocumentoAutorizarOperaciones();
            return oPrograma.ObtenerEmpleadoDocumentoAutorizacion(pIdAutorizacion, pFlAutorizacion, pIdEmpleado, pNbEmpleado, pIdDocumento, pClEstado, pFechaAutorizacion);
        }
        #endregion


        #region Actualiza estatus del documento a autorizado o "No autorizado"
        public E_RESULTADO ActualizaEstatusDocumentoAutorizacion(Guid? pFlAutorizacion, string pClEstado, string pDsNotas, DateTime? pFeAutorizacion, string pClUsuarioModifica, string pProgramaModifica)
        {
            DocumentoAutorizarOperaciones oPrograma = new DocumentoAutorizarOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPrograma.ActualizarEstatusDocumentoAutorizacion(pFlAutorizacion, pClEstado, pDsNotas, pFeAutorizacion, pClUsuarioModifica, pProgramaModifica));
        }
        #endregion

        #region Actualiza estatus del documento al enviar e-mail

        public E_RESULTADO ActualizaEstadoAutorizaDoc(int? pIdAutorizacion = null, string pNbUsuario = null, string pNbPrograma = null)
        {
            DocumentoAutorizarOperaciones oPrograma = new DocumentoAutorizarOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPrograma.ActualizaEstadoAutorizaDoc(pIdAutorizacion, pNbUsuario, pNbPrograma));
        }

        #endregion

    }
}
