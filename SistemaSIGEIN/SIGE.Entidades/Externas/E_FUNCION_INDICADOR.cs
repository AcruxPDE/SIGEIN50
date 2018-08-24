using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_FUNCION_INDICADOR
    {
        public Guid ID_ITEM { get; set; }
        public int ID_FUNCION_INDICADOR { get; set; }
        public int ID_INDICADOR_DESEMPENO { get; set; }
        public string NB_INDICADOR_DESEMPENO { get; set; }
        public int ID_FUNCION_COMPETENCIA { get; set; }
        public int ID_FUNCION_GENERICA { get; set; }
        public Guid ID_PARENT_ITEM { get; set; }

        public E_FUNCION_INDICADOR()
        {
            this.ID_ITEM = Guid.NewGuid();
        }
    }
}
