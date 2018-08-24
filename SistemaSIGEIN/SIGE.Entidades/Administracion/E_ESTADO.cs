using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_ESTADO
    {
        public string CL_ESTADO { get; set; }
        public string CL_PAIS { get; set; }
        public int? ID_ESTADO { get; set; }
        public string NB_ESTADO { get; set; } 
    }
}
