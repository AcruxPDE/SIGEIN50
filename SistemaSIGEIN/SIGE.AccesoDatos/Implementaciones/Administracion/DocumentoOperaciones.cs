using System;
using System.Collections.Generic;
using System.Linq;
using SIGE.Entidades;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal
{

    public class DocumentoOperaciones
    {

        private SistemaSigeinEntities context;

    
        public List<SPE_OBTIENE_C_DOCUMENTO_Result> ObtenerDocumentos(int? pIdDocumento = null, string pNbDocumento = null, string pClDocumento = null, int? pIdCandidato = null, int? pIdEmpleado = null, string pClRuta = null, string pClTipoRuta = null, DateTime? pFeRecepcion = null, int? pIdInstitucion = null, int? pIdBitacora = null, bool? pFgActivo = null, int? pIdArchivo = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_C_DOCUMENTO(pIdDocumento, pNbDocumento, pClDocumento, pIdCandidato, pIdEmpleado, pClRuta, pClTipoRuta, pFeRecepcion, pIdInstitucion, pIdBitacora, pFgActivo, pIdArchivo).ToList();
            }
        }
         
        //public int InsertaActualiza_C_DOCUMENTO(string tipo_transaccion, SPE_OBTIENE_C_DOCUMENTO_Result V_C_DOCUMENTO, byte[] archivo, string ruta, string usuario, string programa)
        //{
        //    using (context = new SistemaSigeinEntities())
        //    {
        //        //Declaramos el objeto de valor de retorno
        //        ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
        //        context.SPE_INSERTA_ACTUALIZA_C_DOCUMENTO(pout_clave_retorno, V_C_DOCUMENTO.ID_DOCUMENTO, V_C_DOCUMENTO.NB_DOCUMENTO, V_C_DOCUMENTO.CL_DOCUMENTO, V_C_DOCUMENTO.ID_CANDIDATO, V_C_DOCUMENTO.ID_EMPLEADO, V_C_DOCUMENTO.CL_RUTA, V_C_DOCUMENTO.CL_TIPO_RUTA, V_C_DOCUMENTO.FE_RECEPCION, V_C_DOCUMENTO.ID_INSTITUCION, V_C_DOCUMENTO.ID_BITACORA, V_C_DOCUMENTO.FG_ACTIVO, usuario, usuario, programa, programa, tipo_transaccion, archivo, ruta);
        //        //regresamos el valor de retorno de sql
        //        return Convert.ToInt32(pout_clave_retorno.Value); ;
        //    }
        //}
         
        //public int Elimina_C_DOCUMENTO(int? ID_DOCUMENTO = null, string usuario = null, string programa = null)
        //{
        //    using (context = new SistemaSigeinEntities())
        //    {
        //        //Declaramos el objeto de valor de retorno
        //        ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
        //        context.SPE_ELIMINA_C_DOCUMENTO(pout_clave_retorno, ID_DOCUMENTO, usuario, programa);
        //        //regresamos el valor de retorno de sql				
        //        return Convert.ToInt32(pout_clave_retorno.Value);
        //    }
        //}
    
    }
}