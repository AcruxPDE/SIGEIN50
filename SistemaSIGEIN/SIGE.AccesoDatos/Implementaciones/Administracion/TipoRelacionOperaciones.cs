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

    public class TipoRelacionOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  S_TIPO_RELACION_PUESTO
        public List<SPE_OBTIENE_S_TIPO_RELACION_PUESTO_Result> Obtener_S_TIPO_RELACION_PUESTO(String CL_TIPO_RELACION = null, String NB_TIPO_RELACION = null, String DS_TIPO_RELACION = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_S_TIPO_RELACION_PUESTO in context.SPE_OBTIENE_S_TIPO_RELACION_PUESTO(CL_TIPO_RELACION, NB_TIPO_RELACION, DS_TIPO_RELACION)
                        select V_S_TIPO_RELACION_PUESTO;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  S_TIPO_RELACION_PUESTO
        public int InsertaActualiza_S_TIPO_RELACION_PUESTO(string tipo_transaccion, SPE_OBTIENE_S_TIPO_RELACION_PUESTO_Result V_S_TIPO_RELACION_PUESTO, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_S_TIPO_RELACION_PUESTO(pout_clave_retorno, V_S_TIPO_RELACION_PUESTO.CL_TIPO_RELACION, V_S_TIPO_RELACION_PUESTO.NB_TIPO_RELACION, V_S_TIPO_RELACION_PUESTO.DS_TIPO_RELACION, tipo_transaccion, usuario, programa);
                //regresamos el valor de retorno de sql
                return Convert.ToInt32(pout_clave_retorno.Value); ;
            }
        }
        #endregion

        #region ELIMINA DATOS  S_TIPO_RELACION_PUESTO
        public int Elimina_S_TIPO_RELACION_PUESTO(String CL_TIPO_RELACION = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_S_TIPO_RELACION_PUESTO(pout_clave_retorno, CL_TIPO_RELACION, usuario, programa);
                //regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
        #endregion

    }
}
