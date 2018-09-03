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

    public class PuestoCompetenciaOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  C_PUESTO_COMPETENCIA
        public List<SPE_OBTIENE_C_PUESTO_COMPETENCIA_Result> Obtener_C_PUESTO_COMPETENCIA(int? ID_PUESTO_COMPETENCIA = null, int? ID_PUESTO = null, int? ID_COMPETENCIA = null, Decimal? ID_NIVEL_DESEADO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_C_PUESTO_COMPETENCIA in context.SPE_OBTIENE_C_PUESTO_COMPETENCIA(ID_PUESTO_COMPETENCIA, ID_PUESTO, ID_COMPETENCIA, ID_NIVEL_DESEADO)
                        select V_C_PUESTO_COMPETENCIA;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_PUESTO_COMPETENCIA
        public int InsertaActualiza_C_PUESTO_COMPETENCIA(string tipo_transaccion, SPE_OBTIENE_C_PUESTO_COMPETENCIA_Result V_C_PUESTO_COMPETENCIA, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_C_PUESTO_COMPETENCIA(pout_clave_retorno, V_C_PUESTO_COMPETENCIA.ID_PUESTO_COMPETENCIA, V_C_PUESTO_COMPETENCIA.ID_PUESTO, V_C_PUESTO_COMPETENCIA.ID_COMPETENCIA, V_C_PUESTO_COMPETENCIA.ID_NIVEL_DESEADO, usuario, usuario, programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
                return Convert.ToInt32(pout_clave_retorno.Value); ;
            }
        }
        #endregion

        #region ELIMINA DATOS  C_PUESTO_COMPETENCIA
        public int Elimina_C_PUESTO_COMPETENCIA(int? ID_PUESTO_COMPETENCIA = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_C_PUESTO_COMPETENCIA(pout_clave_retorno, ID_PUESTO_COMPETENCIA, usuario, programa);
                //regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
        #endregion

    }
}