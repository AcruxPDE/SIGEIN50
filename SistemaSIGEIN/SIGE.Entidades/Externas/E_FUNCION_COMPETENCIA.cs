using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_FUNCION_COMPETENCIA
    {
        public Guid ID_ITEM { get; set; }
        public int ID_FUNCION_COMPETENCIA { get; set; }
        public int ID_COMPETENCIA { get; set; }
        public string NB_COMPETENCIA { get; set; }
        public string DS_COMPETENCIA { get; set; }
        public string NB_NIVEL { get; set; }
        public int NO_NIVEL { get; set; }        
        public int ID_FUNCION_GENERICA { get; set; }
        public string DS_INDICADORES { get; set; }
        public Guid ID_PARENT_ITEM { get; set; }

        public E_FUNCION_COMPETENCIA()
        {
            this.ID_ITEM = Guid.NewGuid();
        }
    }
}
