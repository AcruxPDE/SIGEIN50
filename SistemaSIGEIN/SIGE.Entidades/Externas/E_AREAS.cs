using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_AREAS
    {
        public int? ID_DEPARTAMENTO { get; set; }
        public string  CL_DEPARTAMENTO { get; set; }
        public string NB_DEPARTAMENTO { get; set; }
        public bool FG_SELECCIONADO { get; set; }
    }
}
