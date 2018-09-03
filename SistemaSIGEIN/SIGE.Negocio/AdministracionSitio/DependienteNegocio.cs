using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;

namespace SIGE.Negocio.Administracion  // reemplazar por la carpeta correspondiente
{
    public class DependienteNegocio
    {

        //#region OBTIENE DATOS  C_DEPENDIENTE_ECONOMICO
        //public List<SPE_OBTIENE_C_DEPENDIENTE_ECONOMICO_Result> Obtener_C_DEPENDIENTE_ECONOMICO(int? ID_DEPENDIENTE_ECONOMICO = null, String NB_DEPENDIENTE_ECONOMICO = null, String CL_PARENTEZCO = null, String CL_GENERO = null, DateTime? FE_NACIMIENTO = null, int? ID_BITACORA = null, bool? CL_OCUPACION = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        //{
        //    DependienteOperaciones operaciones = new DependienteOperaciones();
        //    return operaciones.Obtener_C_DEPENDIENTE_ECONOMICO(ID_DEPENDIENTE_ECONOMICO, NB_DEPENDIENTE_ECONOMICO, CL_PARENTEZCO, CL_GENERO, FE_NACIMIENTO, ID_BITACORA, CL_OCUPACION, FG_ACTIVO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
        //}
        //#endregion

        //#region INSERTA ACTUALIZA DATOS  C_DEPENDIENTE_ECONOMICO
        //public int InsertaActualiza_C_DEPENDIENTE_ECONOMICO(string tipo_transaccion, SPE_OBTIENE_C_DEPENDIENTE_ECONOMICO_Result V_C_DEPENDIENTE_ECONOMICO, string usuario, string programa)
        //{
        //    DependienteOperaciones operaciones = new DependienteOperaciones();
        //    return operaciones.InsertaActualiza_C_DEPENDIENTE_ECONOMICO(tipo_transaccion, V_C_DEPENDIENTE_ECONOMICO, usuario, programa);
        //}
        //#endregion

        //#region ELIMINA DATOS  C_DEPENDIENTE_ECONOMICO
        //public int Elimina_C_DEPENDIENTE_ECONOMICO(int? ID_DEPENDIENTE_ECONOMICO = null, string usuario = null, string programa = null)
        //{
        //    DependienteOperaciones operaciones = new DependienteOperaciones();
        //    return operaciones.Elimina_C_DEPENDIENTE_ECONOMICO(ID_DEPENDIENTE_ECONOMICO, usuario, programa);
        //}
        //#endregion
    }
}
