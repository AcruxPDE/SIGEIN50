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
   public class CuestionariosNegocio
    {

        #region obtiene datos  c_cuestionario
        public List<SPE_OBTIENE_C_CUESTIONARIO_Result> Obtener_C_CUESTIONARIO(int? pIdCuestionario = null, string pClCuestionario = null, string pNbCuestionario = null)
        {
            CuestionariosOperaciones operaciones = new CuestionariosOperaciones();
            return operaciones.Obtiene_C_CUESTIONARIO(pIdCuestionario, pClCuestionario, pNbCuestionario); //fe_creacion, fe_modificacion, cl_usuario_app_crea, cl_usuario_app_modifica, nb_programa_crea, nb_programa_modifica);
        }
        #endregion

        #region inserta actualiza datos  c_cuestionario
        public E_RESULTADO InsertaActualiza_C_CUESTIONARIO(string tipo_transaccion, SPE_OBTIENE_C_CUESTIONARIO_Result v_c_cuestionario, string usuario, string programa)
        {
            CuestionariosOperaciones operaciones = new CuestionariosOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualiza_C_CUESTIONARIO(tipo_transaccion, v_c_cuestionario, usuario, programa));
        }
        #endregion

        #region elimina datos  c_cuestionario
        public E_RESULTADO Elimina_C_CUESTIONARIO(int? pIdCuestionario = null, string usuario = null, string programa = null)
        {
            CuestionariosOperaciones operaciones = new CuestionariosOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_C_CUESTIONARIO(pIdCuestionario, usuario, programa));
        }
        #endregion


        #region obtiene datos  K_PREGUNTAS
        public List<SPE_OBTIENE_K_PREGUNTA_Result> Obtener_K_PREGUNTA
         (int? pIdPregunta = null, string pClPregunta = null, string pNbPregunta = null, string pDsPregunta = null, string pClTipoPregunta = null, decimal? pNoValor = null, bool? pFgRequerido = null, bool? pFgActivo = null, int? pIdCompetencia = null, int? pIdBitacora = null, int? pIdPrueba = null, int? pIdCuestionario = null, Guid? pClTokenExterno = null)
        {
            CuestionariosOperaciones operaciones = new CuestionariosOperaciones();
            return operaciones.Obtiene_K_PREGUNTA(pIdPregunta, pClPregunta, pNbPregunta, pDsPregunta, pClTipoPregunta, pNoValor, pFgRequerido, pFgActivo, pIdCompetencia, pIdBitacora, pIdPrueba, pIdCuestionario, pClTokenExterno); //fe_creacion, fe_modificacion, cl_usuario_app_crea, cl_usuario_app_modifica, nb_programa_crea, nb_programa_modifica);
        }
        #endregion
    }
}
