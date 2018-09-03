using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using System.Data.Objects;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{
   
    public class PaisOperaciones
    {

		private SistemaSigeinEntities context;		

		#region OBTIENE DATOS  VW_PAIS
		public List<SPE_OBTIENE_VW_PAIS_Result> Obtener_VW_PAIS(String CL_PAIS = null,String NB_PAIS = null)
		{
			using (context = new SistemaSigeinEntities ())
			{
				var q = from V_VW_PAIS in context.SPE_OBTIENE_VW_PAIS(CL_PAIS,NB_PAIS)
				select V_VW_PAIS;
				return q.ToList();
			}
        }
        #endregion

    }
}