using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades.Administracion;
using System.Xml.Linq;

namespace SIGE.Negocio.Administracion  // reemplazar por la carpeta correspondiente
{
    public class EmpleadosEscolaridadNegocio
    {

        //#region OBTIENE DATOS  C_EMPLEADO_ESCOLARIDAD
        //public List<SPE_OBTIENE_C_EMPLEADO_ESCOLARIDAD_Result> Obtener_C_EMPLEADO_ESCOLARIDAD(int? ID_EMPLEADO_ESCOLARIDAD = null, int? ID_EMPLEADO = null, int? ID_CANDIDATO = null, int? ID_ESCOLARIDAD = null, int? CL_INSTITUCION = null, String NB_INSTITUCION = null, DateTime? FE_PERIODO_INICIO = null, DateTime? FE_PERIODO_FIN = null, String CL_ESTADO_ESCOLARIDAD = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        //{
        //    EmpleadosEscolaridadOperaciones operaciones = new EmpleadosEscolaridadOperaciones();
        //    return operaciones.Obtener_C_EMPLEADO_ESCOLARIDAD(ID_EMPLEADO_ESCOLARIDAD, ID_EMPLEADO, ID_CANDIDATO, ID_ESCOLARIDAD, CL_INSTITUCION, NB_INSTITUCION, FE_PERIODO_INICIO, FE_PERIODO_FIN, CL_ESTADO_ESCOLARIDAD, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
        //}
        //#endregion

        //#region INSERTA ACTUALIZA DATOS  C_EMPLEADO_ESCOLARIDAD
        //public int InsertaActualiza_C_EMPLEADO_ESCOLARIDAD(XElement archivo, string usuario, string programa)
        //{
        //    EmpleadosEscolaridadOperaciones operaciones = new EmpleadosEscolaridadOperaciones();
        //    return operaciones.InsertaActualiza_C_EMPLEADO_ESCOLARIDAD(usuario, programa, archivo);
        //}
        //#endregion

        //#region ELIMINA DATOS  C_EMPLEADO_ESCOLARIDAD
        //public int Elimina_C_EMPLEADO_ESCOLARIDAD(int? ID_EMPLEADO_ESCOLARIDAD = null, string usuario = null, string programa = null)
        //{
        //    EmpleadosEscolaridadOperaciones operaciones = new EmpleadosEscolaridadOperaciones();
        //    return operaciones.Elimina_C_EMPLEADO_ESCOLARIDAD(ID_EMPLEADO_ESCOLARIDAD, usuario, programa);
        //}
        //#endregion
    }
}