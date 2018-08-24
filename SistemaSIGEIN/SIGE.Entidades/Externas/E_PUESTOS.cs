using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_PUESTOS
    {
        public int? ID_PUESTO { get; set; }
        public string CL_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
        public int? ID_DEPARTAMENTO { get; set; }
        public string CL_TIPO_RELACION { get; set; }
    }
}
