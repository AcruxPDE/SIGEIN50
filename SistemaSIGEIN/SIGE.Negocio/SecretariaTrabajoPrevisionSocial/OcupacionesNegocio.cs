using SIGE.AccesoDatos.Implementaciones.SecretariaTrabajoPrevisionSocial;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.SecretariaTrabajoPrevisionSocial;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Negocio.SecretariaTrabajoPrevisionSocial
{
    public class OcupacionesNegocio
    {
        #region  DATOS  DE ÁREAS
        public List<SPE_OBTIENE_AREA_OCUPACION_Result> Obtener_AREA_OCUPACION(int? PIN_ID_AREA = null, String PIN_CL_AREA = null, String PIN_NB_AREA = null)
        {
            OcupacionesOperaciones operaciones = new OcupacionesOperaciones();
            return operaciones.Obtener_SPE_OBTIENE_AREA_OCUPACION(PIN_ID_AREA, PIN_CL_AREA, PIN_NB_AREA);
        }

        public E_RESULTADO InsertaActualiza_C_AREA(string tipo_transaccion, E_AREA_OCUPACION V_C_AREA, string usuario, string programa)
        {
            OcupacionesOperaciones operaciones = new OcupacionesOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualiza_C_AREA(tipo_transaccion, V_C_AREA, usuario, programa));
        }

        public E_RESULTADO Elimina_C_AREA(int? ID_AREA = null, string CL_AREA = null, string usuario = null, string programa = null)
        {
            OcupacionesOperaciones operaciones = new OcupacionesOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_C_AREA(ID_AREA, CL_AREA, usuario, programa));
        }
        #endregion

        #region  DATOS  DE SUB-ÁREAS
        public List<SPE_OBTIENE_SUBAREA_OCUPACION_Result> Obtener_SUBAREA_OCUPACION(int? PIN_ID_AREA = null, String PIN_CL_AREA = null, String PIN_NB_AREA = null,
                                                                                    int? PIN_ID_SUBAREA = null, String PIN_CL_SUBAREA = null, String PIN_NB_SUBAREA = null)
        {
            OcupacionesOperaciones operaciones = new OcupacionesOperaciones();
            return operaciones.Obtener_SPE_OBTIENE_SUBAREA_OCUPACION(PIN_ID_AREA, PIN_CL_AREA, PIN_NB_AREA, PIN_ID_SUBAREA, PIN_CL_SUBAREA, PIN_NB_SUBAREA);
        }

        public E_RESULTADO InsertaActualiza_C_SUBAREA(string tipo_transaccion, E_SUBAREA_OCUPACION V_C_SUBAREA, string usuario, string programa)
        {
            OcupacionesOperaciones operaciones = new OcupacionesOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualiza_C_SUBAREA(tipo_transaccion, V_C_SUBAREA, usuario, programa));
        }

        public E_RESULTADO Elimina_C_SUBAREA(int? ID_SUBAREA = null, string CL_SUBAREA = null, string usuario = null, string programa = null)
        {
            OcupacionesOperaciones operaciones = new OcupacionesOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_C_SUBAREA(ID_SUBAREA, CL_SUBAREA, usuario, programa));
        }

        #endregion

        #region  DATOS  DE MÓDULOS
        public List<SPE_OBTIENE_MODULO_OCUPACION_Result> Obtener_MODULO_OCUPACION(int? PIN_ID_AREA = null, String PIN_CL_AREA = null, String PIN_NB_AREA = null,
                                                                                  int? PIN_ID_SUBAREA = null, String PIN_CL_SUBAREA = null, String PIN_NB_SUBAREA = null,
                                                                                  int? PIN_ID_MODULO = null, String PIN_CL_MODULO = null, String PIN_NB_MODULO = null)
        {
            OcupacionesOperaciones operaciones = new OcupacionesOperaciones();
            return operaciones.Obtener_SPE_OBTIENE_MODULO_OCUPACION(PIN_ID_AREA, PIN_CL_AREA, PIN_NB_AREA, PIN_ID_SUBAREA, PIN_CL_SUBAREA, PIN_NB_SUBAREA,PIN_ID_MODULO,PIN_CL_MODULO,PIN_NB_MODULO);
        }

        public E_RESULTADO InsertaActualiza_C_MODULO(string tipo_transaccion, E_MODULO_OCUPACION V_C_MODULO, string usuario, string programa)
        {
            OcupacionesOperaciones operaciones = new OcupacionesOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualiza_C_MODULO(tipo_transaccion, V_C_MODULO, usuario, programa));
        }

        public E_RESULTADO Elimina_C_MODULO(int? ID_MODULO = null, string CL_MODULO = null, string usuario = null, string programa = null)
        {
            OcupacionesOperaciones operaciones = new OcupacionesOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_C_MODULO(ID_MODULO, CL_MODULO, usuario, programa));
        }
        #endregion

        #region  DATOS  DE OCUPACIONES
        public List<SPE_OBTIENE_OCUPACIONES_Result> Obtener_OCUPACIONES(int? PIN_ID_AREA = null, String PIN_CL_AREA = null, String PIN_NB_AREA = null,
                                                                                  int? PIN_ID_SUBAREA = null, String PIN_CL_SUBAREA = null, String PIN_NB_SUBAREA = null,
                                                                                  int? PIN_ID_MODULO = null, String PIN_CL_MODULO = null, String PIN_NB_MODULO = null,
                                                                                  int? PIN_ID_OCUPACION = null, String PIN_CL_OCUPACION = null, String PIN_NB_OCUPACION = null)
        {
            OcupacionesOperaciones operaciones = new OcupacionesOperaciones();
            return operaciones.Obtener_SPE_OBTIENE_OCUPACIONES(PIN_ID_AREA, PIN_CL_AREA, PIN_NB_AREA, PIN_ID_SUBAREA, PIN_CL_SUBAREA, PIN_NB_SUBAREA, PIN_ID_MODULO, PIN_CL_MODULO, PIN_NB_MODULO,PIN_ID_OCUPACION,PIN_CL_OCUPACION,PIN_NB_OCUPACION);
        }

        public E_RESULTADO InsertaActualiza_C_OCUPACION(string tipo_transaccion, E_OCUPACION V_C_OCUPACION, string usuario, string programa)
        {
            OcupacionesOperaciones operaciones = new OcupacionesOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualiza_C_OCUPACION(tipo_transaccion, V_C_OCUPACION, usuario, programa));
        }

        public E_RESULTADO Elimina_C_OCUPACION(int? ID_OCUPACION = null, string usuario = null, string programa = null)
        {
            OcupacionesOperaciones operaciones = new OcupacionesOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_C_OCUPACION(ID_OCUPACION, usuario, programa));
        }
        #endregion

        #region ELIMINA DATOS K_PUESTO_OCUPACION
        public E_RESULTADO Elimina_K_PUESTO_OCUPACION(int? ID_OCUPACION = null, int? ID_PUESTO = null, string usuario = null, string programa = null)
        {
            OcupacionesOperaciones operaciones = new OcupacionesOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_K_PUESTO_OCUPACION(ID_OCUPACION, ID_PUESTO, usuario, programa));
        }
        #endregion
    }
}
