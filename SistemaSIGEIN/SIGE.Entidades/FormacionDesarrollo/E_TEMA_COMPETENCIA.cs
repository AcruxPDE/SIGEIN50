using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_TEMA_COMPETENCIA
    {
        public Guid ID_ITEM { get; set; }
        public int ID_TEMA { get; set; }
        public int ID_TEMA_COMPETENCIA { get; set; }
        public int ID_COMPETENCIA { get; set; }
        public string CL_TIPO_COMPETENCIA { get; set; }
        public string NB_COMPETENCIA { get; set; }

        public E_TEMA_COMPETENCIA()
        {
            ID_ITEM = Guid.NewGuid();
        }
    }
}
