using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_TELEFONO
    {
        public Guid ID_ITEM { get; set; }
        public string NB_TELEFONO { get; set; }
        public string CL_TIPO { get; set; }
        public string NB_TIPO { get; set; }

        public E_TELEFONO()
        {
            ID_ITEM = Guid.NewGuid();
        }
    }
}
