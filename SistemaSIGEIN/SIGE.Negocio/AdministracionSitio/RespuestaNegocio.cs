using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal; // reemplazar por la carpeta correspondiente
using SIGE.Entidades.Administracion;

namespace SIGE.Negocio.Administracion  // reemplazar por la carpeta correspondiente
{
    public class RespuestaNegocio
    {

        //#region OBTIENE DATOS  C_RESPUESTA
        //public List<SPE_OBTIENE_C_RESPUESTA_Result> Obtener_C_RESPUESTA(int? ID_RESPUESTA = null, String CL_RESPUESTA = null, String NB_RESPUESTA = null, String DS_RESPUESTA = null, Decimal? NO_VALOR = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        //{
        //    RespuestaOperaciones operaciones = new RespuestaOperaciones();
        //    return operaciones.Obtener_C_RESPUESTA(ID_RESPUESTA, CL_RESPUESTA, NB_RESPUESTA, DS_RESPUESTA, NO_VALOR, FG_ACTIVO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
        //}
        //#endregion

        //#region INSERTA ACTUALIZA DATOS  C_RESPUESTA
        //public int InsertaActualiza_C_RESPUESTA(string tipo_transaccion, SPE_OBTIENE_C_RESPUESTA_Result V_C_RESPUESTA, string usuario, string programa)
        //{
        //    RespuestaOperaciones operaciones = new RespuestaOperaciones();
        //    return operaciones.InsertaActualiza_C_RESPUESTA(tipo_transaccion, V_C_RESPUESTA, usuario, programa);
        //}
        //#endregion

        //#region ELIMINA DATOS  C_RESPUESTA
        //public int Elimina_C_RESPUESTA(int? ID_RESPUESTA = null, string usuario = null, string programa = null)
        //{
        //    RespuestaOperaciones operaciones = new RespuestaOperaciones();
        //    return operaciones.Elimina_C_RESPUESTA(ID_RESPUESTA, usuario, programa);
        //}
        //#endregion
    }
}