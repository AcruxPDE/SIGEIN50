using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_SECUENCIA
    {
        public string CL_PREFIJO { get; set; }
        public string CL_SECUENCIA { get; set; }
        public string CL_SUFIJO { get; set; }
        public int? NO_DIGITOS { get; set; }
        public int? NO_ULTIMO_VALOR { get; set; }
        public int? NO_VALOR_MAXIMO { get; set; } 
    }
}
