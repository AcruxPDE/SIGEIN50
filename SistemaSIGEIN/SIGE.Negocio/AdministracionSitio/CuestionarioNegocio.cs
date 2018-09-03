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
    public class CuestionarioNegocio
    {

        //#region OBTIENE DATOS  K_CUESTIONARIO
        //public List<SPE_OBTIENE_CUESTIONARIO_Result> ObtieneCuestionario(int? pIdCuestionario = null, int? pIdEvaluado = null, int? pIdEvaluadoEvaluador = null, int? pIdEvaluador = null)
        //{
        //    CuestionarioOperaciones operaciones = new CuestionarioOperaciones();
        //    return operaciones.ObtenerCuestionario(pIdCuestionario, pIdEvaluado, pIdEvaluadoEvaluador, pIdEvaluador);
        //}
        //#endregion

        //#region INSERTA ACTUALIZA DATOS  K_CUESTIONARIO
        //public int InsertaActualiza_K_CUESTIONARIO(string tipo_transaccion, SPE_OBTIENE_CUESTIONARIO_Result V_K_CUESTIONARIO, string usuario, string programa)
        //{
        //    CuestionarioOperaciones operaciones = new CuestionarioOperaciones();
        //    return operaciones.InsertaActualiza_K_CUESTIONARIO(tipo_transaccion, V_K_CUESTIONARIO, usuario, programa);
        //}
        //#endregion

        //#region ELIMINA DATOS  K_CUESTIONARIO
        //public int Elimina_K_CUESTIONARIO(int? ID_CUESTIONARIO = null, string usuario = null, string programa = null)
        //{
        //    CuestionarioOperaciones operaciones = new CuestionarioOperaciones();
        //    return operaciones.Elimina_K_CUESTIONARIO(ID_CUESTIONARIO, usuario, programa);
        //}
        //#endregion
    }
}