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

    public class RespuestaOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  C_RESPUESTA
        public List<SPE_OBTIENE_C_RESPUESTA_Result> Obtener_C_RESPUESTA(int? ID_RESPUESTA = null, String CL_RESPUESTA = null, String NB_RESPUESTA = null, String DS_RESPUESTA = null, Decimal? NO_VALOR = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_C_RESPUESTA in context.SPE_OBTIENE_C_RESPUESTA(ID_RESPUESTA, CL_RESPUESTA, NB_RESPUESTA, DS_RESPUESTA, NO_VALOR, FG_ACTIVO)
                        select V_C_RESPUESTA;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_RESPUESTA
        public int InsertaActualiza_C_RESPUESTA(string tipo_transaccion, SPE_OBTIENE_C_RESPUESTA_Result V_C_RESPUESTA, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_C_RESPUESTA(pout_clave_retorno, V_C_RESPUESTA.ID_RESPUESTA, V_C_RESPUESTA.CL_RESPUESTA, V_C_RESPUESTA.NB_RESPUESTA, V_C_RESPUESTA.DS_RESPUESTA, V_C_RESPUESTA.NO_VALOR, V_C_RESPUESTA.FG_ACTIVO, usuario, usuario, programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
                return Convert.ToInt32(pout_clave_retorno.Value); ;
            }
        }
        #endregion

        #region ELIMINA DATOS  C_RESPUESTA
        public int Elimina_C_RESPUESTA(int? ID_RESPUESTA = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_C_RESPUESTA(pout_clave_retorno, ID_RESPUESTA, usuario, programa);
                //regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
        #endregion

    }
}
