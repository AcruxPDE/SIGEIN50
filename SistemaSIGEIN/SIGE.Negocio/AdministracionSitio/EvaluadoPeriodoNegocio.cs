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
    public class EvaluadoPeriodoNegocio
    {

        //#region OBTIENE DATOS  K_EVALUADO_PERIODO
        //public List<SPE_OBTIENE_K_EVALUADO_PERIODO_Result> Obtener_K_EVALUADO_PERIODO(int? ID_EVALAUDOR_PERIODO = null, int? ID_PERIODO = null, int? ID_EMPLEADO = null, int? ID_PUESTO = null, int? FG_PUESTO_ACTUAL = null, int? NO_CONSUMO_SUP = null, Decimal? MN_CUOTA_BASE = null, Decimal? MN_CUOTA_CONSUMO = null, Decimal? MN_CUOTA_ADICIONAL = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        //{
        //    EvaluadoPeriodoOperaciones operaciones = new EvaluadoPeriodoOperaciones();
        //    return operaciones.Obtener_K_EVALUADO_PERIODO(ID_EVALAUDOR_PERIODO, ID_PERIODO, ID_EMPLEADO, ID_PUESTO, FG_PUESTO_ACTUAL, NO_CONSUMO_SUP, MN_CUOTA_BASE, MN_CUOTA_CONSUMO, MN_CUOTA_ADICIONAL, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
        //}
        //#endregion

        //#region INSERTA ACTUALIZA DATOS  K_EVALUADO_PERIODO
        //public int InsertaActualiza_K_EVALUADO_PERIODO(string tipo_transaccion, SPE_OBTIENE_K_EVALUADO_PERIODO_Result V_K_EVALUADO_PERIODO, string usuario, string programa)
        //{
        //    EvaluadoPeriodoOperaciones operaciones = new EvaluadoPeriodoOperaciones();
        //    return operaciones.InsertaActualiza_K_EVALUADO_PERIODO(tipo_transaccion, V_K_EVALUADO_PERIODO, usuario, programa);
        //}
        //#endregion

        //#region ELIMINA DATOS  K_EVALUADO_PERIODO
        //public int Elimina_K_EVALUADO_PERIODO(int? ID_EVALAUDOR_PERIODO = null, string usuario = null, string programa = null)
        //{
        //    EvaluadoPeriodoOperaciones operaciones = new EvaluadoPeriodoOperaciones();
        //    return operaciones.Elimina_K_EVALUADO_PERIODO(ID_EVALAUDOR_PERIODO, usuario, programa);
        //}
        //#endregion
    }
}