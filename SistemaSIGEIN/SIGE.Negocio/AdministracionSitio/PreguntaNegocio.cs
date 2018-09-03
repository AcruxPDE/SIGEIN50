using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades.Administracion;

namespace SIGE.Negocio.Administracion
{
    public class PreguntaNegocio
    {

        //#region OBTIENE DATOS  C_PREGUNTA
        //public List<SPE_OBTIENE_C_PREGUNTA_Result> Obtener_C_PREGUNTA(int? ID_PREGUNTA = null, String CL_PREGUNTA = null, String NB_PREGUNTA = null, String DS_PREGUNTA = null, String CL_TIPO_PREGUNTA = null, Decimal? NO_VALOR = null, bool? FG_REQUERIDO = null, bool? FG_ACTIVO = null, int? ID_COMPETENCIA = null, int? ID_BITACORA = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        //{
        //    PreguntaOperaciones operaciones = new PreguntaOperaciones();
        //    return operaciones.Obtener_C_PREGUNTA(ID_PREGUNTA, CL_PREGUNTA, NB_PREGUNTA, DS_PREGUNTA, CL_TIPO_PREGUNTA, NO_VALOR, FG_REQUERIDO, FG_ACTIVO, ID_COMPETENCIA, ID_BITACORA, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
        //}
        //#endregion

        //#region INSERTA ACTUALIZA DATOS  C_PREGUNTA
        //public int InsertaActualiza_C_PREGUNTA(string tipo_transaccion, SPE_OBTIENE_C_PREGUNTA_Result V_C_PREGUNTA, string usuario, string programa)
        //{
        //    PreguntaOperaciones operaciones = new PreguntaOperaciones();
        //    return operaciones.InsertaActualiza_C_PREGUNTA(tipo_transaccion, V_C_PREGUNTA, usuario, programa);
        //}
        //#endregion

        //#region ELIMINA DATOS  C_PREGUNTA
        //public int Elimina_C_PREGUNTA(int? ID_PREGUNTA = null, string usuario = null, string programa = null)
        //{
        //    PreguntaOperaciones operaciones = new PreguntaOperaciones();
        //    return operaciones.Elimina_C_PREGUNTA(ID_PREGUNTA, usuario, programa);
        //}
        //#endregion

        public List<SPE_OBTIENE_K_PREGUNTA_Result> Obtener_K_PREGUNTA(int? ID_PREGUNTA = null, String CL_PREGUNTA = null, String NB_PREGUNTA = null, String DS_PREGUNTA = null, String CL_TIPO_PREGUNTA = null, Decimal? NO_VALOR = null, bool? FG_REQUERIDO = null, bool? FG_ACTIVO = null, int? ID_COMPETENCIA = null, int? ID_BITACORA = null, int? ID_PRUEBA = null, int? ID_CUESTIONARIO = null, Guid? CL_TOKEN_EXTERNO = null)
        {
            PreguntaOperaciones operaciones = new PreguntaOperaciones();
            return operaciones.Obtener_K_PREGUNTA(ID_PREGUNTA, CL_PREGUNTA, NB_PREGUNTA, DS_PREGUNTA, CL_TIPO_PREGUNTA, NO_VALOR, FG_REQUERIDO, FG_ACTIVO, ID_COMPETENCIA, ID_BITACORA, ID_PRUEBA, ID_CUESTIONARIO, CL_TOKEN_EXTERNO);
        }
        
    }
}
