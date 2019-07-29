using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_FORMA_PAGO
    {
        public string CL_FORMA_PAGO { get; set; }
        public string NB_FORMA_PAGO { get; set; }
        public bool FG_ACTIVO { get; set; }
        public string DS_ACTIVO { get; set; }

    }
}
