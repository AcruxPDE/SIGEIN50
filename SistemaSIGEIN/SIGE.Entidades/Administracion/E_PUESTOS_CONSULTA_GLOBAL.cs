using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_PUESTOS_CONSULTA_GLOBAL
    {
        public Guid vIdParametroConfiguracionConsulta { get; set; }
        public List<E_PUESTOS_CONSULTA> vListaPuestos { get; set; }

             public E_PUESTOS_CONSULTA_GLOBAL()
        {
            vListaPuestos = new List<E_PUESTOS_CONSULTA>();
        }
    }
}
