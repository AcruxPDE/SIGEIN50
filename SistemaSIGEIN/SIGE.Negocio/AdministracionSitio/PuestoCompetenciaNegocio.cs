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
    public class PuestoCompetenciaNegocio
    {

        //#region OBTIENE DATOS  C_PUESTO_COMPETENCIA
        //public List<SPE_OBTIENE_C_PUESTO_COMPETENCIA_Result> Obtener_C_PUESTO_COMPETENCIA(int? ID_PUESTO_COMPETENCIA = null, int? ID_PUESTO = null, int? ID_COMPETENCIA = null, Decimal? ID_NIVEL_DESEADO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        //{
        //    PuestoCompetenciaOperaciones operaciones = new PuestoCompetenciaOperaciones();
        //    return operaciones.Obtener_C_PUESTO_COMPETENCIA(ID_PUESTO_COMPETENCIA, ID_PUESTO, ID_COMPETENCIA, ID_NIVEL_DESEADO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
        //}
        //#endregion

        //#region INSERTA ACTUALIZA DATOS  C_PUESTO_COMPETENCIA
        //public int InsertaActualiza_C_PUESTO_COMPETENCIA(string tipo_transaccion, SPE_OBTIENE_C_PUESTO_COMPETENCIA_Result V_C_PUESTO_COMPETENCIA, string usuario, string programa)
        //{
        //    PuestoCompetenciaOperaciones operaciones = new PuestoCompetenciaOperaciones();
        //    return operaciones.InsertaActualiza_C_PUESTO_COMPETENCIA(tipo_transaccion, V_C_PUESTO_COMPETENCIA, usuario, programa);
        //}
        //#endregion

        //#region ELIMINA DATOS  C_PUESTO_COMPETENCIA
        //public int Elimina_C_PUESTO_COMPETENCIA(int? ID_PUESTO_COMPETENCIA = null, string usuario = null, string programa = null)
        //{
        //    PuestoCompetenciaOperaciones operaciones = new PuestoCompetenciaOperaciones();
        //    return operaciones.Elimina_C_PUESTO_COMPETENCIA(ID_PUESTO_COMPETENCIA, usuario, programa);
        //}
        //#endregion
    }
}
