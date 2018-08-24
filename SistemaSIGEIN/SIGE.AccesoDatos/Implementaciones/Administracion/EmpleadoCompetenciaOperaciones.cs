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

    public class EmpleadoCompetenciaOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  K_EMPLEADO_COMPETENCIA
        public List<SPE_OBTIENE_K_EMPLEADO_COMPETENCIA_Result> Obtener_K_EMPLEADO_COMPETENCIA(int? ID_EMPLEADO_COMPETENCIA = null, int? ID_EMPLEADO = null, int? ID_COMPETENCIA = null, Decimal? NO_CALIFICACION = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_K_EMPLEADO_COMPETENCIA in context.SPE_OBTIENE_K_EMPLEADO_COMPETENCIA(ID_EMPLEADO_COMPETENCIA, ID_EMPLEADO, ID_COMPETENCIA, NO_CALIFICACION)
                        select V_K_EMPLEADO_COMPETENCIA;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  K_EMPLEADO_COMPETENCIA
        public int InsertaActualiza_K_EMPLEADO_COMPETENCIA(string tipo_transaccion, SPE_OBTIENE_K_EMPLEADO_COMPETENCIA_Result V_K_EMPLEADO_COMPETENCIA, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_K_EMPLEADO_COMPETENCIA(pout_clave_retorno, V_K_EMPLEADO_COMPETENCIA.ID_EMPLEADO_COMPETENCIA, V_K_EMPLEADO_COMPETENCIA.ID_EMPLEADO, V_K_EMPLEADO_COMPETENCIA.ID_COMPETENCIA, V_K_EMPLEADO_COMPETENCIA.NO_CALIFICACION, usuario, usuario, programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
                return Convert.ToInt32(pout_clave_retorno.Value); ;
            }
        }
        #endregion

        #region ELIMINA DATOS  K_EMPLEADO_COMPETENCIA
        public int Elimina_K_EMPLEADO_COMPETENCIA(int? ID_EMPLEADO_COMPETENCIA = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_K_EMPLEADO_COMPETENCIA(pout_clave_retorno, ID_EMPLEADO_COMPETENCIA, usuario, programa);
                //regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
        #endregion

    }
}
