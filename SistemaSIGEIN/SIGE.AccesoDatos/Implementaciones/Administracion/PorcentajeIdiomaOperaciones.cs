using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using System.Data.Objects;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{
   
    public class PorcentajeIdiomaOperaciones
    {

		private SistemaSigeinEntities context;		

		#region OBTIENE DATOS  VW_OBTIENE_PORCENTAJES_IDIOMAS
		public List<SPE_OBTIENE_VW_OBTIENE_PORCENTAJES_IDIOMAS_Result> Obtener_VW_OBTIENE_PORCENTAJES_IDIOMAS(String NB_PORCENTAJE = null,decimal? CL_PORCENTAJE = null)
		{
			using (context = new SistemaSigeinEntities ())
			{
				var q = from V_VW_OBTIENE_PORCENTAJES_IDIOMAS in context.SPE_OBTIENE_VW_OBTIENE_PORCENTAJES_IDIOMAS(NB_PORCENTAJE,CL_PORCENTAJE)
				select V_VW_OBTIENE_PORCENTAJES_IDIOMAS;
				return q.ToList();
			}
		}
		#endregion

	}
}
