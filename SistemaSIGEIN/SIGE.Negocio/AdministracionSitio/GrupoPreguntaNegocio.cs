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
    public class GrupoPreguntaNegocio
    {

        //#region OBTIENE DATOS  C_GRUPO_PREGUNTA
        //public List<SPE_OBTIENE_C_GRUPO_PREGUNTA_Result> Obtener_C_GRUPO_PREGUNTA(int?  ID_GRUPO_PREGUNTA = null,String CL_GRUPO_PREGUNTA = null,String NB_GRUPO_PREGUNTA = null,int?  ID_PREGUNTA = null,DateTime?  FE_CREACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_APP_CREA = null,String CL_USUARIO_APP_MODIFICA = null,String NB_PROGRAMA_CREA = null,String NB_PROGRAMA_MODIFICA = null)
        //{
        //    GrupoPreguntaOperaciones operaciones = new GrupoPreguntaOperaciones();
        //    return operaciones.Obtener_C_GRUPO_PREGUNTA(ID_GRUPO_PREGUNTA,CL_GRUPO_PREGUNTA,NB_GRUPO_PREGUNTA,ID_PREGUNTA,FE_CREACION,FE_MODIFICACION,CL_USUARIO_APP_CREA,CL_USUARIO_APP_MODIFICA,NB_PROGRAMA_CREA,NB_PROGRAMA_MODIFICA);
        //}
        //#endregion

        //#region INSERTA ACTUALIZA DATOS  C_GRUPO_PREGUNTA
        //public int InsertaActualiza_C_GRUPO_PREGUNTA(string tipo_transaccion, SPE_OBTIENE_C_GRUPO_PREGUNTA_Result V_C_GRUPO_PREGUNTA, string usuario, string programa)
        //{
        //    GrupoPreguntaOperaciones operaciones = new GrupoPreguntaOperaciones();
        //    return operaciones.InsertaActualiza_C_GRUPO_PREGUNTA(tipo_transaccion,V_C_GRUPO_PREGUNTA, usuario,programa);
        //}	
        //#endregion

        //#region ELIMINA DATOS  C_GRUPO_PREGUNTA
        //public int Elimina_C_GRUPO_PREGUNTA(SPE_OBTIENE_C_GRUPO_PREGUNTA_Result V_C_GRUPO_PREGUNTA, string usuario = null, string programa = null)
        //{
        //    GrupoPreguntaOperaciones operaciones = new GrupoPreguntaOperaciones();
        //    return operaciones.Elimina_C_GRUPO_PREGUNTA(V_C_GRUPO_PREGUNTA, usuario, programa);
        //}
        //#endregion
    }
}
