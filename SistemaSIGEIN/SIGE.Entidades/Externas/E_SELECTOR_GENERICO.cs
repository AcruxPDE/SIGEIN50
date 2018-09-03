using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_SELECTOR_GENERICO
    {
        public int ID_ENTIDAD { get; set; }
        public string CL_ENTIDAD { get; set; }
        public string NB_ENTIDAD { get; set; }
    }
}
