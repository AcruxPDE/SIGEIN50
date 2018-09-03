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

    public class SecuenciaOperaciones
    {

        private SistemaSigeinEntities context;

        #region

        public List<SPE_OBTIENE_FOLIO_SECUENCIA_Result> ObtieneFolioSecuencia(String CL_SECUENCIA = null) 
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_C_SECUENCIA in context.SPE_OBTIENE_FOLIO_SECUENCIA(CL_SECUENCIA)
                        select V_C_SECUENCIA;

                return q.ToList();
            }
        }

        #endregion

        #region OBTIENE DATOS  C_SECUENCIA
        public List<SPE_OBTIENE_C_SECUENCIA_Result> Obtener_C_SECUENCIA(String CL_SECUENCIA = null, String CL_PREFIJO = null, int? NO_ULTIMO_VALOR = null, int? NO_VALOR_MAXIMO = null, String CL_SUFIJO = null, byte? NO_DIGITOS = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_C_SECUENCIA in context.SPE_OBTIENE_C_SECUENCIA(CL_SECUENCIA, CL_PREFIJO, NO_ULTIMO_VALOR, NO_VALOR_MAXIMO, CL_SUFIJO, NO_DIGITOS)
                        select V_C_SECUENCIA;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_SECUENCIA
        public int InsertaActualiza_C_SECUENCIA(string tipo_transaccion, SPE_OBTIENE_C_SECUENCIA_Result V_C_SECUENCIA, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_C_SECUENCIA(pout_clave_retorno, V_C_SECUENCIA.CL_SECUENCIA, V_C_SECUENCIA.CL_PREFIJO, V_C_SECUENCIA.NO_ULTIMO_VALOR, V_C_SECUENCIA.NO_VALOR_MAXIMO, V_C_SECUENCIA.CL_SUFIJO, V_C_SECUENCIA.NO_DIGITOS, usuario, usuario, programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
                return Convert.ToInt32(pout_clave_retorno.Value); ;
            }
        }
        #endregion

        #region ELIMINA DATOS  C_SECUENCIA
        public int Elimina_C_SECUENCIA(String CL_SECUENCIA = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_C_SECUENCIA(pout_clave_retorno, CL_SECUENCIA, usuario, programa);
                //regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
        #endregion

    }
}
