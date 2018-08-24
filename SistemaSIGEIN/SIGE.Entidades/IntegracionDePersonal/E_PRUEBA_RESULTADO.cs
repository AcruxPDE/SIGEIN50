using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.IntegracionDePersonal
{
    [Serializable]
    public class E_PRUEBA_RESULTADO
    {
        public int ID_VARIABLE { get; set; }
        public string CL_VARIABLE { get; set; }
        public int NO_VALOR { get; set; }
        public int NO_VALOR_RES { get; set; }
    }
}
