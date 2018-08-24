using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_FUNCION_GENERICA
    {
        public Guid ID_ITEM { get; set; }
        public int ID_FUNCION_GENERICA { get; set; }
        public string CL_FUNCION_GENERICA { get; set; }
        public string NB_FUNCION_GENERICA { get; set; }
        public string DS_DETALLE { get; set; }
        public string DS_NOTAS { get; set; }

        public E_FUNCION_GENERICA()
        {
            this.ID_ITEM = Guid.NewGuid();
        }
    }
}
