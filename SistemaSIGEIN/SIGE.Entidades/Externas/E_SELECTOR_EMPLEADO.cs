using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
     [Serializable]
    public class E_SELECTOR_EMPLEADO
    {
        public int? idEmpleado { get; set; }
        public string idEmpleado_pde { get; set; }
        public string clPuesto { get; set; }
        public string clEmpleado { get; set; }
        public string nbCorreoElectronico { get; set; }
        public string idPuesto { get; set; }
        public string nbEmpleado { get; set; }
        public string nbPuesto { get; set; }
        public string clTipoCatalogo { get; set; }
    }
}
