using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_TIPO_NOMINA
    {
        public System.Guid ID_TIPO_NOMINA { get; set; }
        public string CL_CLIENTE { get; set; }
        public string CL_TIPO_NOMINA { get; set; }
        public string NB_PERIODICIDAD { get; set; }
        public string CL_PERIODICIDAD { get; set; }
        public string DS_TIPO_NOMINA { get; set; }
        public bool FG_ACTIVO { get; set; }
        public string ACTIVO { get; set; }
    }
}
