using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades.Administracion;
using SIGE.Entidades;
using System.Data.Objects;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{

    public class CuestionarioOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  K_CUESTIONARIO
        public List<SPE_OBTIENE_CUESTIONARIO_Result> ObtenerCuestionario(int? pIdCuestionario = null, int? pIdEvaluado = null, int? pIdEvaluadoEvaluador = null, int? pIdEvaluador = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_CUESTIONARIO(pIdCuestionario, pIdEvaluado, pIdEvaluadoEvaluador, pIdEvaluador).ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  K_CUESTIONARIO
        public int InsertaActualiza_K_CUESTIONARIO(string tipo_transaccion, SPE_OBTIENE_CUESTIONARIO_Result V_K_CUESTIONARIO, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_K_CUESTIONARIO(pout_clave_retorno, V_K_CUESTIONARIO.ID_CUESTIONARIO, V_K_CUESTIONARIO.ID_EVALUADO, V_K_CUESTIONARIO.ID_EVALUADO_EVALUADOR, usuario, usuario, programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
        #endregion

        #region ELIMINA DATOS  K_CUESTIONARIO
        public int Elimina_K_CUESTIONARIO(int? ID_CUESTIONARIO = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_K_CUESTIONARIO(pout_clave_retorno, ID_CUESTIONARIO, usuario, programa);
                //regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
        #endregion

    }
}
