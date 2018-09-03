using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_CURSO_COMPETENCIA
    {
        public Guid ID_ITEM { get; set; }
        public int ID_CURSO_COMPETENCIA { get; set; }
        public int ID_COMPETENCIA { get; set; }
        public string CL_TIPO_COMPETENCIA { get; set; }
        public string NB_COMPETENCIA { get; set; }

        public E_CURSO_COMPETENCIA()
        {
            ID_ITEM = Guid.NewGuid();
        }
    }		
}
