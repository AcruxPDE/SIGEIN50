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

    public class EvaluadoPeriodoOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  K_EVALUADO_PERIODO
        public List<SPE_OBTIENE_K_EVALUADO_PERIODO_Result> Obtener_K_EVALUADO_PERIODO(int? ID_EVALAUDOR_PERIODO = null, int? ID_PERIODO = null, int? ID_EMPLEADO = null, int? ID_PUESTO = null, int? FG_PUESTO_ACTUAL = null, int? NO_CONSUMO_SUP = null, Decimal? MN_CUOTA_BASE = null, Decimal? MN_CUOTA_CONSUMO = null, Decimal? MN_CUOTA_ADICIONAL = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_K_EVALUADO_PERIODO in context.SPE_OBTIENE_K_EVALUADO_PERIODO(ID_EVALAUDOR_PERIODO, ID_PERIODO, ID_EMPLEADO, ID_PUESTO, FG_PUESTO_ACTUAL, NO_CONSUMO_SUP, MN_CUOTA_BASE, MN_CUOTA_CONSUMO, MN_CUOTA_ADICIONAL)
                        select V_K_EVALUADO_PERIODO;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  K_EVALUADO_PERIODO
        public int InsertaActualiza_K_EVALUADO_PERIODO(string tipo_transaccion, SPE_OBTIENE_K_EVALUADO_PERIODO_Result V_K_EVALUADO_PERIODO, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_K_EVALUADO_PERIODO(pout_clave_retorno, V_K_EVALUADO_PERIODO.ID_EVALUADOR_PERIODO, V_K_EVALUADO_PERIODO.ID_PERIODO, V_K_EVALUADO_PERIODO.ID_EMPLEADO, V_K_EVALUADO_PERIODO.ID_PUESTO, V_K_EVALUADO_PERIODO.FG_PUESTO_ACTUAL, V_K_EVALUADO_PERIODO.NO_CONSUMO_SUP, V_K_EVALUADO_PERIODO.MN_CUOTA_BASE, V_K_EVALUADO_PERIODO.MN_CUOTA_CONSUMO, V_K_EVALUADO_PERIODO.MN_CUOTA_ADICIONAL, usuario, usuario, programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
                return Convert.ToInt32(pout_clave_retorno.Value); ;
            }
        }
        #endregion

        #region ELIMINA DATOS  K_EVALUADO_PERIODO
        public int Elimina_K_EVALUADO_PERIODO(int? ID_EVALAUDOR_PERIODO = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_K_EVALUADO_PERIODO(pout_clave_retorno, ID_EVALAUDOR_PERIODO, usuario, programa);
                //regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
        #endregion

    }
}
