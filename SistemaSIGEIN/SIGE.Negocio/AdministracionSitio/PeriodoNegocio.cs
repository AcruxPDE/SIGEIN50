using System;
using System.Collections.Generic;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;


namespace SIGE.Negocio.Administracion
{
    public class PeriodoNegocio
    {

        public List<SPE_OBTIENE_C_PERIODO_Result> Obtener_C_PERIODO(int? ID_PERIODO = null, String CL_PERIODO = null, String NB_PERIODO = null, String DS_PERIODO = null, DateTime? FE_INICIO = null, DateTime? FE_TERMINO = null, String CL_ESTADO_PERIODO = null, String XML_CAMPOS_ADICIONALES = null)
        {
            PeriodoOperaciones operaciones = new PeriodoOperaciones();
            return operaciones.Obtener_C_PERIODO(ID_PERIODO, CL_PERIODO, NB_PERIODO, DS_PERIODO, FE_INICIO, FE_TERMINO, CL_ESTADO_PERIODO, XML_CAMPOS_ADICIONALES);
        }

        //#region INSERTA ACTUALIZA DATOS  C_PERIODO
        //public int InsertaActualiza_C_PERIODO(string tipo_transaccion, SPE_OBTIENE_C_PERIODO_Result V_C_PERIODO, string usuario, string programa)
        //{
        //    PeriodoOperaciones operaciones = new PeriodoOperaciones();
        //    return operaciones.InsertaActualiza_C_PERIODO(tipo_transaccion, V_C_PERIODO, usuario, programa);
        //}
        //#endregion

        //#region ELIMINA DATOS  C_PERIODO
        //public int Elimina_C_PERIODO(int? ID_PERIODO = null, string usuario = null, string programa = null)
        //{
        //    PeriodoOperaciones operaciones = new PeriodoOperaciones();
        //    return operaciones.Elimina_C_PERIODO(ID_PERIODO, usuario, programa);
        //}
        //#endregion
    }
}
