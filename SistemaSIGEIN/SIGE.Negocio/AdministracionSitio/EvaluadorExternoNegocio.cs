using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal; // reemplazar por la carpeta correspondiente
//using SIGE.AccesoDatos.Implementaciones.EvaluacionOrganizacional.EvaluadorExterno; // reemplazar por la carpeta correspondiente
//using SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo.EvaluadorExterno; // reemplazar por la carpeta correspondiente
//using SIGE.AccesoDatos.Implementaciones.MetodologiaCompensacion.EvaluadorExterno; // reemplazar por la carpeta correspondiente
//using SIGE.Entidades.Administracion;
//using SIGE.Entidades.EvaluacionOrganizacional;
//using SIGE.Entidades.FormacionDesarrollo;
//using SIGE.Entidades.MetodologiaCompensacion;


namespace SIGE.Negocio.Administracion  // reemplazar por la carpeta correspondiente
{
    public class EvaluadorExternoNegocio
    {

        //#region OBTIENE DATOS  C_EVALUADOR_EXTERNO
        //public List<SPE_OBTIENE_C_EVALUADOR_EXTERNO_Result> Obtener_C_EVALUADOR_EXTERNO(int? ID_EVALUADOR_EXTERNO = null, String CL_EVALUADOR_EXTERNO = null, String NB_EVALUADOR_EXTERNO = null, String DS_EVALUARDO_EXTERNO = null, bool? FG_ACTIVO = null, String XML_CAMPOS_ADICIONALES = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        //{
        //    EvaluadorExternoOperaciones operaciones = new EvaluadorExternoOperaciones();
        //    return operaciones.Obtener_C_EVALUADOR_EXTERNO(ID_EVALUADOR_EXTERNO, CL_EVALUADOR_EXTERNO, NB_EVALUADOR_EXTERNO, DS_EVALUARDO_EXTERNO, FG_ACTIVO, XML_CAMPOS_ADICIONALES, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
        //}
        //#endregion

        //#region INSERTA ACTUALIZA DATOS  C_EVALUADOR_EXTERNO
        //public int InsertaActualiza_C_EVALUADOR_EXTERNO(string tipo_transaccion, SPE_OBTIENE_C_EVALUADOR_EXTERNO_Result V_C_EVALUADOR_EXTERNO, string usuario, string programa)
        //{
        //    EvaluadorExternoOperaciones operaciones = new EvaluadorExternoOperaciones();
        //    return operaciones.InsertaActualiza_C_EVALUADOR_EXTERNO(tipo_transaccion, V_C_EVALUADOR_EXTERNO, usuario, programa);
        //}
        //#endregion

        //#region ELIMINA DATOS  C_EVALUADOR_EXTERNO
        //public int Elimina_C_EVALUADOR_EXTERNO(int? ID_EVALUADOR_EXTERNO = null, string usuario = null, string programa = null)
        //{
        //    EvaluadorExternoOperaciones operaciones = new EvaluadorExternoOperaciones();
        //    return operaciones.Elimina_C_EVALUADOR_EXTERNO(ID_EVALUADOR_EXTERNO, usuario, programa);
        //}
        //#endregion
    }
}
