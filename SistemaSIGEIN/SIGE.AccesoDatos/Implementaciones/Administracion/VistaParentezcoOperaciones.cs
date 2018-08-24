using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using System.Data.Objects;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{
   
    public class VistaParentezcoOperaciones
    {

		private SistemaSigeinEntities context;		

		#region OBTIENE DATOS  VW_PARENTEZCO
		public List<SPE_OBTIENE_VW_PARENTEZCO_Result> Obtener_VW_PARENTEZCO(String NB_PARENTEZCO = null)
		{
			using (context = new SistemaSigeinEntities ())
			{
				var q = from V_VW_PARENTEZCO in context.SPE_OBTIENE_VW_PARENTEZCO(NB_PARENTEZCO)
				select V_VW_PARENTEZCO;
				return q.ToList();
			}
		}
		#endregion

		#region INSERTA ACTUALIZA DATOS  VW_PARENTEZCO
		public int InsertaActualiza_VW_PARENTEZCO(string tipo_transaccion, SPE_OBTIENE_VW_PARENTEZCO_Result V_VW_PARENTEZCO,string usuario, string programa)
		{
			using (context = new SistemaSigeinEntities ())
			{
				//Declaramos el objeto de valor de retorno
				ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
				context.SPE_INSERTA_ACTUALIZA_VW_PARENTEZCO(pout_clave_retorno,  V_VW_PARENTEZCO.NB_PARENTEZCO, tipo_transaccion);
				//regresamos el valor de retorno de sql
                return Convert.ToInt32(pout_clave_retorno.Value); ; 
			}
		}
		#endregion

		#region ELIMINA DATOS  VW_PARENTEZCO
		public int Elimina_VW_PARENTEZCO(SPE_OBTIENE_VW_PARENTEZCO_Result PARENTEZCO, string usuario  = null, string programa  = null)
		{
			using (context = new SistemaSigeinEntities ())
			{
				//Declaramos el objeto de valor de retorno
				ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
				context.SPE_ELIMINA_VW_PARENTEZCO(pout_clave_retorno, PARENTEZCO.NB_PARENTEZCO, usuario, programa);
				//regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
			}
		}
		#endregion

	}
}