using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using System.Data.Objects;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{
   
    public class MesesOperaciones
    {

		private SistemaSigeinEntities context;		

		#region OBTIENE DATOS  VW_OBTIENE_MESES
		public List<SPE_OBTIENE_VW_OBTIENE_MESES_Result> Obtener_VW_OBTIENE_MESES(int?  NUMERO = null,String MES = null)
		{
			using (context = new SistemaSigeinEntities ())
			{
				var q = from V_VW_OBTIENE_MESES in context.SPE_OBTIENE_VW_OBTIENE_MESES(NUMERO,MES)
				select V_VW_OBTIENE_MESES;
				return q.ToList();
			}
		}
		#endregion

        #region
        public List<SP_OBTIENE_ANIOS_Result> Obtener_OBTIENE_ANIOS()
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_VW_OBTIENE_MESES in context.SP_OBTIENE_ANIOS()
                        select V_VW_OBTIENE_MESES;
                return q.ToList();
            }
        }
        #endregion


    }
}
