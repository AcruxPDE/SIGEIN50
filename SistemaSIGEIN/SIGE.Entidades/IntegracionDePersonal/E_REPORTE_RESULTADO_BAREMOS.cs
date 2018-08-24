using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.IntegracionDePersonal
{
   [Serializable]
   public class E_REPORTE_RESULTADO_BAREMOS
    {
       public string NB_FACTOR { get; set; }
        public string DS_FACTOR { get; set; }
        public string NO_VALOR_RESPUESTA { get; set; }
        public string NO_VALOR_BAREMOS { get; set; }
    }
}
