using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_RIESGO_PUESTO
    {
        public string CL_RIESGO_PUESTO { get; set; }
        public string NB_RIESGO_PUESTO { get; set; }
        public bool FG_ACTIVO { get; set; }
        public string DS_ACTIVO { get; set; }
    }
}
