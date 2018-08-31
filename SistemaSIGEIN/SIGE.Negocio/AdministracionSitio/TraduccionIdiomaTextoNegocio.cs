using SIGE.AccesoDatos.Implementaciones.Administracion;
using SIGE.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Negocio.AdministracionSitio
{
    public class TraduccionIdiomaTextoNegocio
    {
        public List<SPE_OBTIENE_TRADUCCION_TEXTO_Result> ObtieneTraduccion(string pCL_TEXTO = null, string pCL_MODULO = null, string pCL_PROCESO = null, string pCL_IDIOMA = null)
        {
            TraduccionIdiomaTextoOperaciones operaciones = new TraduccionIdiomaTextoOperaciones();
            return operaciones.ObtieneTraduccion(pCL_TEXTO, pCL_MODULO, pCL_PROCESO, pCL_IDIOMA);
        }
    }
}
