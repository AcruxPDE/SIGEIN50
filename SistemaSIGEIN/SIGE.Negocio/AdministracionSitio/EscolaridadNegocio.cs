using System;
using System.Collections.Generic;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal; // reemplazar por la carpeta correspondiente
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;

namespace SIGE.Negocio.Administracion  // reemplazar por la carpeta correspondiente
{
    public class EscolaridadNegocio
    {

        public List<SPE_OBTIENE_C_ESCOLARIDAD_Result> Obtener_C_ESCOLARIDAD(int? ID_ESCOLARIDAD = null, String CL_ESCOLARIDAD = null, String NB_ESCOLARIDAD = null, String DS_ESCOLARIDAD = null, String CL_NIVEL_ESCOLARIDAD = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            EscolaridadOperaciones operaciones = new EscolaridadOperaciones();
            return operaciones.Obtener_C_ESCOLARIDAD(ID_ESCOLARIDAD, CL_ESCOLARIDAD, NB_ESCOLARIDAD, DS_ESCOLARIDAD, CL_NIVEL_ESCOLARIDAD, FG_ACTIVO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
        }

        public List<SPE_OBTIENE_ESCOLARIDADES_Result> Obtener_Escolaridades(int? ID_ESCOLARIDAD = null, String NB_ESCOLARIDAD = null, String DS_ESCOLARIDAD = null, String CL_NIVEL_ESCOLARIDAD = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null, int? ID_NIVEL_ESCOLARIDAD = null, String NB_NIVEL_ESCOLARIDAD = null)
        {
            EscolaridadOperaciones operaciones = new EscolaridadOperaciones();
            return operaciones.Obtener_Escolaridades(ID_ESCOLARIDAD, NB_ESCOLARIDAD, DS_ESCOLARIDAD, CL_NIVEL_ESCOLARIDAD, FG_ACTIVO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA, ID_NIVEL_ESCOLARIDAD, NB_NIVEL_ESCOLARIDAD);
        }

        public E_RESULTADO InsertaActualiza_C_ESCOLARIDAD(string tipo_transaccion, E_ESCOLARIDAD V_C_ESCOLARIDAD, string usuario, string programa)
        {
            EscolaridadOperaciones operaciones = new EscolaridadOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualiza_C_ESCOLARIDAD(tipo_transaccion, V_C_ESCOLARIDAD, usuario, programa));
        }

        public E_RESULTADO Elimina_C_ESCOLARIDAD(int? ID_ESCOLARIDAD = null, string usuario = null, string programa = null)
        {
            EscolaridadOperaciones operaciones = new EscolaridadOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_C_ESCOLARIDAD(ID_ESCOLARIDAD, usuario, programa));
        }

    }
}