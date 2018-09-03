using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_INSTRUCTOR_COMPETENCIA
    {
        public Guid ID_ITEM { get; set; }
        public int ID_INSTRUCTOR_COMPETENCIA { get; set; }
        public int ID_COMPETENCIA { get; set; }
        public string CL_COMPETENCIA { get; set; }
        public string NB_COMPETENCIA { get; set; }

        public E_INSTRUCTOR_COMPETENCIA()
        {
            ID_ITEM = Guid.NewGuid();
        }
    }
}
