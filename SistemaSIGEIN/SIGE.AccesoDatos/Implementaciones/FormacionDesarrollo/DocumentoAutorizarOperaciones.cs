using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SIGE.Entidades;

namespace SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo
{
    public class DocumentoAutorizarOperaciones
    {

        SistemaSigeinEntities contexto;

        public List<SPE_OBTIENE_C_DOCUMENTO_AUTORIZACION_Result> ObtenerDocumentoAutorizacion(int? pIdDocumentoAutorizacion = null, string pClDocumento = null, string pNbDocumento = null, string pClTipoDocumento = null, string pVersion = null, DateTime? pFechaElaboracion = null, DateTime? pFechaRevision = null, int? nbEmpleadoElabora = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_C_DOCUMENTO_AUTORIZACION(pIdDocumentoAutorizacion, pClDocumento, pNbDocumento, pClTipoDocumento, pVersion, pFechaElaboracion, pFechaRevision, nbEmpleadoElabora).ToList();
            }
        }

        public List<SPE_OBTIENE_C_AUTORIZACION_DCTO_EMPLEADO_Result> ObtenerEmpleadoDocumentoAutorizacion(int? pIdAutorizacion = null, Guid? pFlAutorizacion = null, int? pIdEmpleado = null, string pNbEmpleado = null, int? pIdDocumento = null, string pClEstado = null, DateTime? pFechaAutorizacion = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_C_AUTORIZACION_DCTO_EMPLEADO(pIdAutorizacion, pFlAutorizacion, pIdEmpleado, pNbEmpleado, pIdDocumento, pClEstado, pFechaAutorizacion).ToList();
            }
        }
     

        public XElement InsertarActualizarDocumentoAutorizacion(int? pIdDocumento = null, int? pIdPeriodo = null, int? pIdPrograma = null, string pXmlDatos = null, string pClUsuario = null, string pNbPrograma = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_ACTUALIZA_DOCUMENTO_AUTORIZACION(pOutClaveRetorno, pIdDocumento, pIdPeriodo, pIdPrograma,  pXmlDatos, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClaveRetorno.Value.ToString());
            }
        }


        public XElement ActualizaEstadoAutorizaDoc(int? pIdAutorizacion = null, string pNbUsuario = null, string pNbPrograma = null)
        {
            using(contexto = new SistemaSigeinEntities())
            {
            ObjectParameter pOutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
            contexto.SPE_ACTUALIZA_ESTADO_DOCUMENTO_AUTORIZACION(pOutClaveRetorno, pIdAutorizacion, pNbUsuario, pNbPrograma);
            return XElement.Parse(pOutClaveRetorno.Value.ToString());
            }
        }

        public XElement ObtenerTablasDocumentoAutorizacion(int? pIdPrograma = null, int? pIdPeriodo = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_OBTIENE_DOCUMENTO_AUTORIZAR(pout_clave_retorno, pIdPrograma, pIdPeriodo);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }


        public XElement ActualizarEstatusDocumentoAutorizacion(Guid? pFlAutorizacion, string pClEstado, string pDsNotas, DateTime? pFeAutorizacion, string pClUsuarioModifica, string pProgramaModifica)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ACTUALIZA_K_AUTORIZACION_DCTO_EMPLEADO(pout_clave_retorno, pFlAutorizacion, pClEstado, pDsNotas, pFeAutorizacion, pClUsuarioModifica, pProgramaModifica);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

    }
}
