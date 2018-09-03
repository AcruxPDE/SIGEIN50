using SIGE.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.AccesoDatos.Implementaciones.Administracion
{
    public class TraduccionIdiomaTextoOperaciones
    {
        private SistemaSigeinEntities context;


        #region ObtenerTraducciones

        public List<SPE_OBTIENE_TRADUCCION_TEXTO_Result> ObtieneTraduccion(string pCL_TEXTO = null, string pCL_MODULO = null, string pCL_PROCESO = null, string pCL_IDIOMA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_TRADUCCION_TEXTO(pCL_TEXTO, pCL_MODULO, pCL_PROCESO, pCL_IDIOMA).ToList();
            }
        }

        #endregion

    }
}
