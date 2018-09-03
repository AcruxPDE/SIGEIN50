using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{  [Serializable]
    public class E_AREA_INTERES
    {
        public int ID_AREA_INTERES { get; set; }
        public string CL_AREA_INTERES { get; set; }
        public string NB_AREA_INTERES { get; set; }
        public Nullable<bool> FG_ACTIVO { get; set; }
        public string NB_ACTIVO { get; set; }
        public string DS_FILTRO { get; set; }
    }
}
