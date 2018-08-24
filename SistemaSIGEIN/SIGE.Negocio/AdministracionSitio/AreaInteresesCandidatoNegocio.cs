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
    public class AreaInteresCandidatoNegocio
    {

        //#region OBTIENE DATOS  K_AREA_INTERES
        //public List<SPE_OBTIENE_K_AREA_INTERES_Result> Obtener_K_AREA_INTERES(int? ID_CANDIDATO_AREA_INTERES = null, int? ID_CANDIDATO = null, int? ID_AREA_INTERES = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        //{
        //    AreaInteresCandidatoOperaciones operaciones = new AreaInteresCandidatoOperaciones();
        //    return operaciones.Obtener_K_AREA_INTERES(ID_CANDIDATO_AREA_INTERES, ID_CANDIDATO, ID_AREA_INTERES, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
        //}
        //#endregion

        //#region INSERTA ACTUALIZA DATOS  K_AREA_INTERES
        //public int InsertaActualiza_K_AREA_INTERES(string tipo_transaccion, SPE_OBTIENE_K_AREA_INTERES_Result V_K_AREA_INTERES, string usuario, string programa)
        //{
        //    AreaInteresCandidatoOperaciones operaciones = new AreaInteresCandidatoOperaciones();
        //    return operaciones.InsertaActualiza_K_AREA_INTERES(tipo_transaccion, V_K_AREA_INTERES, usuario, programa);
        //}
        //#endregion

        //#region ELIMINA DATOS  K_AREA_INTERES
        //public int Elimina_K_AREA_INTERES(int? ID_CANDIDATO_AREA_INTERES = null, string usuario = null, string programa = null)
        //{
        //    AreaInteresCandidatoOperaciones operaciones = new AreaInteresCandidatoOperaciones();
        //    return operaciones.Elimina_K_AREA_INTERES(ID_CANDIDATO_AREA_INTERES, usuario, programa);
        //}
        //#endregion
    }
}