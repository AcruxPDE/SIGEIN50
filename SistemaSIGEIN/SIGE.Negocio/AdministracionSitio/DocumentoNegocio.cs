using System;
using System.Collections.Generic;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;


namespace SIGE.Negocio.Administracion
{
    public class DocumentoNegocio
    {
        public List<SPE_OBTIENE_C_DOCUMENTO_Result> ObtieneDocumento(int? pIdDocumento = null, string pNbDocumento = null, string pClDocumento = null, int? pIdCandidato = null, int? pIdEmpleado = null, string pClRuta = null, string pClTipoRuta = null, DateTime? pFeRecepcion = null, int? pIdInstitucion = null, int? pIdBitacora = null, bool? pFgActivo = null, int? pIdArchivo = null)
        {
            DocumentoOperaciones operaciones = new DocumentoOperaciones();
            return operaciones.ObtenerDocumentos(pIdDocumento, pNbDocumento, pClDocumento, pIdCandidato, pIdEmpleado, pClRuta, pClTipoRuta, pFeRecepcion, pIdInstitucion, pIdBitacora, pFgActivo, pIdArchivo);
        }

        //#region INSERTA ACTUALIZA DATOS  C_DOCUMENTO
        //public int InsertaActualiza_C_DOCUMENTO(string tipo_transaccion, SPE_OBTIENE_C_DOCUMENTO_Result V_C_DOCUMENTO, byte[] archivo, string ruta, string usuario, string programa)
        //{
        //    DocumentoOperaciones operaciones = new DocumentoOperaciones();
        //    return operaciones.InsertaActualiza_C_DOCUMENTO(tipo_transaccion, V_C_DOCUMENTO, archivo, ruta, usuario, programa);
        //}
        //#endregion

        //#region ELIMINA DATOS  C_DOCUMENTO
        //public int Elimina_C_DOCUMENTO(int? ID_DOCUMENTO = null, string usuario = null, string programa = null)
        //{
        //    DocumentoOperaciones operaciones = new DocumentoOperaciones();
        //    return operaciones.Elimina_C_DOCUMENTO(ID_DOCUMENTO, usuario, programa);
        //}
        //#endregion
    }
}