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
    public class EmpleadoRelacionadoNegocio
    {

        //#region OBTIENE DATOS  C_EMPLEADO_RELACIONADO
        //public List<SPE_OBTIENE_C_EMPLEADO_RELACIONADO_Result> Obtener_C_EMPLEADO_RELACIONADO(int?  ID_EMPLEADO = null,int?  ID_EMPLEADO_RELACIONADO = null,String CL_TIPO_RELACION = null,String DS_EMPLEADO_RELACIONADO = null,DateTime?  FE_CREACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_APP_CREA = null,String CL_USUARIO_APP_MODIFICA = null,String NB_PROGRAMA_CREA = null,String NB_PROGRAMA_MODIFICA = null)
        //{
        //    EmpleadoRelacionadoOperaciones operaciones = new EmpleadoRelacionadoOperaciones();
        //    return operaciones.Obtener_C_EMPLEADO_RELACIONADO(ID_EMPLEADO,ID_EMPLEADO_RELACIONADO,CL_TIPO_RELACION,DS_EMPLEADO_RELACIONADO,FE_CREACION,FE_MODIFICACION,CL_USUARIO_APP_CREA,CL_USUARIO_APP_MODIFICA,NB_PROGRAMA_CREA,NB_PROGRAMA_MODIFICA);
        //}
        //#endregion

        //#region INSERTA ACTUALIZA DATOS  C_EMPLEADO_RELACIONADO
        //public int InsertaActualiza_C_EMPLEADO_RELACIONADO(string tipo_transaccion, SPE_OBTIENE_C_EMPLEADO_RELACIONADO_Result V_C_EMPLEADO_RELACIONADO, string usuario, string programa)
        //{
        //    EmpleadoRelacionadoOperaciones operaciones = new EmpleadoRelacionadoOperaciones();
        //    return operaciones.InsertaActualiza_C_EMPLEADO_RELACIONADO(tipo_transaccion,V_C_EMPLEADO_RELACIONADO, usuario,programa);
        //}	
        //#endregion

        //#region ELIMINA DATOS  C_EMPLEADO_RELACIONADO
        //public int Elimina_C_EMPLEADO_RELACIONADO(SPE_OBTIENE_C_EMPLEADO_RELACIONADO_Result V_C_EMPLEADO_RELACIONADO, string usuario = null, string programa = null)
        //{
        //    EmpleadoRelacionadoOperaciones operaciones = new EmpleadoRelacionadoOperaciones();
        //    return operaciones.Elimina_C_EMPLEADO_RELACIONADO(V_C_EMPLEADO_RELACIONADO, usuario, programa);
        //}
        //#endregion
    }
}
