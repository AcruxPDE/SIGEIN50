using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_PLAZA_GRUPO
    {
        public int ID_PLAZA { get; set; }
        public int ID_EMPLEADO { get; set; }
        public int ID_PUESTO {get; set;}
        public string CL_PLAZA { get; set; }
        public string CL_PUESTO { get; set; }
        public string CL_EMPLEADO { get; set; }
        public string NB_PLAZA { get; set; }
        public string NB_PUESTO { get; set; }
        public string NB_EMPLEADO { get; set; }
    }
}
