using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_INSTRUCTOR_CURSO
    {
        public Guid ID_ITEM { get; set; }
        public int ID_INSTRUCTOR_CURSO{ get; set; }
        public int ID_CURSO { get; set; }
        public string CL_CURSO { get; set; }
        public string NB_CURSO { get; set; }

        public E_INSTRUCTOR_CURSO()
        {
            ID_ITEM = Guid.NewGuid();
        }
    }
}
