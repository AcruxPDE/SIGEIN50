using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_AREAS_INTERES
    {
        public int? ID_AREA_INTERES { get; set; }
        public int? CL_AREA_INTERES { get; set; }
        public string NB_AREA_INTERES { get; set; }
        public string NB_ACTIVO { get; set; }
    }
}
