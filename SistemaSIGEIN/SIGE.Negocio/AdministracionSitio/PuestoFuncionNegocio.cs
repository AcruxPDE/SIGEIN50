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
    public class PuestoFuncionNegocio
    {

        //#region OBTIENE DATOS  C_PUESTO_FUNCION
        //public List<SPE_OBTIENE_C_PUESTO_FUNCION_Result> Obtener_C_PUESTO_FUNCION(int? ID_PUESTO_FUNCION = null, String CL_PUESTO_FUNCION = null, String NB_PUESTO_FUNCION = null, String DS_PUESTO_FUNCION = null, int? ID_PUESTO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        //{
        //    PuestoFuncionOperaciones operaciones = new PuestoFuncionOperaciones();
        //    return operaciones.Obtener_C_PUESTO_FUNCION(ID_PUESTO_FUNCION, CL_PUESTO_FUNCION, NB_PUESTO_FUNCION, DS_PUESTO_FUNCION, ID_PUESTO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
        //}
        //#endregion

        //#region INSERTA ACTUALIZA DATOS  C_PUESTO_FUNCION
        //public int InsertaActualiza_C_PUESTO_FUNCION(string tipo_transaccion, SPE_OBTIENE_C_PUESTO_FUNCION_Result V_C_PUESTO_FUNCION, string usuario, string programa)
        //{
        //    PuestoFuncionOperaciones operaciones = new PuestoFuncionOperaciones();
        //    return operaciones.InsertaActualiza_C_PUESTO_FUNCION(tipo_transaccion, V_C_PUESTO_FUNCION, usuario, programa);
        //}
        //#endregion

        //#region ELIMINA DATOS  C_PUESTO_FUNCION
        //public int Elimina_C_PUESTO_FUNCION(int? ID_PUESTO_FUNCION = null, string usuario = null, string programa = null)
        //{
        //    PuestoFuncionOperaciones operaciones = new PuestoFuncionOperaciones();
        //    return operaciones.Elimina_C_PUESTO_FUNCION(ID_PUESTO_FUNCION, usuario, programa);
        //}
        //#endregion
    }
}
