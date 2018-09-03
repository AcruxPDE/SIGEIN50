using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_CURSO_INSTRUCTOR
    {
        public Guid ID_ITEM { get; set; }
        public int ID_INSTRUCTOR_CURSO { get; set; }
        public int ID_INSTRUCTOR { get; set; }
        public string CL_INSTRUCTOR { get; set; }
        public string NB_INSTRUCTOR { get; set; }

        public E_CURSO_INSTRUCTOR()
        {
            ID_ITEM = Guid.NewGuid();
        }
    }
}
