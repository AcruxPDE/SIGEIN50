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
    public class EmpleadoCompetenciaNegocio
    {

        //#region OBTIENE DATOS  K_EMPLEADO_COMPETENCIA
        //public List<SPE_OBTIENE_K_EMPLEADO_COMPETENCIA_Result> Obtener_K_EMPLEADO_COMPETENCIA(int? ID_EMPLEADO_COMPETENCIA = null, int? ID_EMPLEADO = null, int? ID_COMPETENCIA = null, Decimal? NO_CALIFICACION = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        //{
        //    EmpleadoCompetenciaOperaciones operaciones = new EmpleadoCompetenciaOperaciones();
        //    return operaciones.Obtener_K_EMPLEADO_COMPETENCIA(ID_EMPLEADO_COMPETENCIA, ID_EMPLEADO, ID_COMPETENCIA, NO_CALIFICACION, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
        //}
        //#endregion

        //#region INSERTA ACTUALIZA DATOS  K_EMPLEADO_COMPETENCIA
        //public int InsertaActualiza_K_EMPLEADO_COMPETENCIA(string tipo_transaccion, SPE_OBTIENE_K_EMPLEADO_COMPETENCIA_Result V_K_EMPLEADO_COMPETENCIA, string usuario, string programa)
        //{
        //    EmpleadoCompetenciaOperaciones operaciones = new EmpleadoCompetenciaOperaciones();
        //    return operaciones.InsertaActualiza_K_EMPLEADO_COMPETENCIA(tipo_transaccion, V_K_EMPLEADO_COMPETENCIA, usuario, programa);
        //}
        //#endregion

        //#region ELIMINA DATOS  K_EMPLEADO_COMPETENCIA
        //public int Elimina_K_EMPLEADO_COMPETENCIA(int? ID_EMPLEADO_COMPETENCIA = null, string usuario = null, string programa = null)
        //{
        //    EmpleadoCompetenciaOperaciones operaciones = new EmpleadoCompetenciaOperaciones();
        //    return operaciones.Elimina_K_EMPLEADO_COMPETENCIA(ID_EMPLEADO_COMPETENCIA, usuario, programa);
        //}
        //#endregion
    }
}
