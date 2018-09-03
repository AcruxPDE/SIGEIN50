using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.IntegracionDePersonal
{
    [Serializable]
    public class E_SELECCIONADOS
    {
        public int? ID_SELECCION { get; set; }
        public string CL_SELECCION { get; set; }
        public string  NB_SELECCION { get; set; }
        public string  DS_SELECCION { get; set; }
    }
}
