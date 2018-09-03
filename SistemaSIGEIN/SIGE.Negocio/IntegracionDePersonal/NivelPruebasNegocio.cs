using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Negocio.Administracion
{
    public class NivelPruebasNegocio
    {
  
        #region obtiene datos  c_prueba_nivel
        public List<SPE_OBTIENE_C_PRUEBA_NIVEL_Result> Obtener_C_PRUEBA_NIVEL(int? pIdPruebaNivel = null, string pClPruebaNivel = null, string pNbPruebaNivel = null)
        {
            NivelPruebasOperaciones operaciones = new NivelPruebasOperaciones();
            return operaciones.Obtener_C_PRUEBA_NIVEL(pIdPruebaNivel, pClPruebaNivel, pNbPruebaNivel); //fe_creacion, fe_modificacion, cl_usuario_app_crea, cl_usuario_app_modifica, nb_programa_crea, nb_programa_modifica);
        }
        #endregion

        #region inserta actualiza datos  c_prueba_nivel
        public E_RESULTADO InsertaActualiza_C_PRUEBA(string tipo_transaccion, SPE_OBTIENE_C_PRUEBA_NIVEL_Result v_c_prueba_nivel, string usuario, string programa)
        {
            NivelPruebasOperaciones operaciones = new NivelPruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualiza_C_PRUEBA_NIVEL(tipo_transaccion, v_c_prueba_nivel, usuario, programa));
        }
        #endregion

        #region elimina datos  c_prueba_nivel
        public E_RESULTADO Elimina_C_PRUEBA_NIVEL(int? pIdPruebaNivel = null,String pclPruebaNivel=null, string usuario = null, string programa = null)
        {
                NivelPruebasOperaciones operaciones = new NivelPruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_C_PRUEBA_NIVEL(pIdPruebaNivel,pclPruebaNivel, usuario, programa));
        }
        #endregion

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        #region obtiene datos  K_prueba_nivel
        public List<SPE_OBTIENE_K_PRUEBA_NIVEL_Result> Obtener_K_PRUEBA_NIVEL(int? pIdPruebaNivel = null, int? pIdPrueba = null)
        {
            NivelPruebasOperaciones operaciones = new NivelPruebasOperaciones();
            return operaciones.Obtener_K_PRUEBA_NIVEL(pIdPruebaNivel, pIdPrueba); //fe_creacion, fe_modificacion, cl_usuario_app_crea, cl_usuario_app_modifica, nb_programa_crea, nb_programa_modifica);
        }
        #endregion

        #region inserta actualiza datos  K_prueba_nivel
        public E_RESULTADO InsertaActualiza_K_PRUEBA(string tipo_transaccion, SPE_OBTIENE_K_PRUEBA_NIVEL_Result v_k_prueba_nivel, string usuario, string programa)
        {
            NivelPruebasOperaciones operaciones = new NivelPruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualiza_K_PRUEBA_NIVEL(tipo_transaccion, v_k_prueba_nivel, usuario, programa));
        }
        #endregion

        #region elimina datos  K_prueba_nivel
        public E_RESULTADO Elimina_K_PRUEBA_NIVEL(int? pIdPruebaNivel = null, int? pIdPrueba = null, string usuario = null, string programa = null)
        {
            NivelPruebasOperaciones operaciones = new NivelPruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_K_PRUEBA_NIVEL(pIdPruebaNivel, pIdPrueba, usuario, programa));
        }
        #endregion
    }
}
