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
   
    public class RolFuncionOperaciones
    {

		private SistemaSigeinEntities context;		

		#region OBTIENE DATOS  C_ROL_FUNCION
        public List<SPE_OBTIENE_C_ROL_FUNCION_Result> Obtener_C_ROL_FUNCION(int?  ID_ROL = null,int?  ID_FUNCION = null)
        {
            using (context = new SistemaSigeinEntities ())
            {
                var q = from V_C_ROL_FUNCION in context.SPE_OBTIENE_C_ROL_FUNCION(ID_ROL,ID_FUNCION)
                select V_C_ROL_FUNCION;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_ROL_FUNCION
        public int InsertaActualiza_C_ROL_FUNCION(string tipo_transaccion, SPE_OBTIENE_C_ROL_FUNCION_Result V_C_ROL_FUNCION,string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities ())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_C_ROL_FUNCION(pout_clave_retorno,  V_C_ROL_FUNCION.ID_ROL,V_C_ROL_FUNCION.ID_FUNCION,tipo_transaccion, usuario,programa);
                //regresamos el valor de retorno de sql
                return Convert.ToInt32(pout_clave_retorno.Value); ; 
            }
        }
        #endregion

        #region ELIMINA DATOS  C_ROL_FUNCION
        public int Elimina_C_ROL_FUNCION(SPE_OBTIENE_C_ROL_FUNCION_Result V_C_ROL_FUNCION, string usuario  = null, string programa  = null)
        {
            using (context = new SistemaSigeinEntities ())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_C_ROL_FUNCION(pout_clave_retorno,V_C_ROL_FUNCION.ID_ROL,V_C_ROL_FUNCION.ID_FUNCION, usuario, programa);
                //regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
		#endregion

	}
}
