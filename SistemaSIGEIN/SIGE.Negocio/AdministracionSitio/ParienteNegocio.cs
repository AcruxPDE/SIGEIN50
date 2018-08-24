using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades.Administracion;


namespace SIGE.Negocio.Administracion  // reemplazar por la carpeta correspondiente
{
    public class ParienteNegocio
    {

        //#region OBTIENE DATOS  C_PARIENTE
        //public List<SPE_OBTIENE_C_PARIENTE_Result> Obtener_C_PARIENTE(int? ID_PARIENTE = null, String NB_PARIENTE = null, String CL_PARENTEZCO = null, String CL_GENERO = null, DateTime? FE_NACIMIENTO = null, int? ID_EMPLEADO = null, int? ID_CANDIDATO = null, int? ID_BITACORA = null, String CL_OCUPACION = null, bool? FG_DEPENDIENTE = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        //{
        //    ParienteOperaciones operaciones = new ParienteOperaciones();
        //    return operaciones.Obtener_C_PARIENTE(ID_PARIENTE, NB_PARIENTE, CL_PARENTEZCO, CL_GENERO, FE_NACIMIENTO, ID_EMPLEADO, ID_CANDIDATO, ID_BITACORA, CL_OCUPACION, FG_DEPENDIENTE, FG_ACTIVO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
        //}
        //#endregion

        //#region INSERTA ACTUALIZA DATOS  C_PARIENTE
        //public int InsertaActualiza_C_PARIENTE(string tipo_transaccion, SPE_OBTIENE_C_PARIENTE_Result V_C_PARIENTE, string usuario, string programa)
        //{
        //    ParienteOperaciones operaciones = new ParienteOperaciones();
        //    return operaciones.InsertaActualiza_C_PARIENTE(tipo_transaccion, V_C_PARIENTE, usuario, programa);
        //}
        //#endregion

        //#region ELIMINA DATOS  C_PARIENTE
        //public int Elimina_C_PARIENTE(int? ID_PARIENTE = null, string usuario = null, string programa = null)
        //{
        //    ParienteOperaciones operaciones = new ParienteOperaciones();
        //    return operaciones.Elimina_C_PARIENTE(ID_PARIENTE, usuario, programa);
        //}
        //#endregion
    }
}