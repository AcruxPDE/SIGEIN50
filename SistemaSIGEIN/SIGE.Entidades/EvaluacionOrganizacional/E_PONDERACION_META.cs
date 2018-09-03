using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    public class E_PONDERACION_META
    {
        public int ID_EMPLEADO { get; set; }
        public int ID_EVALUADO { get; set; }
        public string CL_EMPLEADO { get; set; }
        public string NB_EMPLEADO { get; set; }
        public string NB_PUESTO { get; set; }
        public string NB_AREA{ get; set; }
        public decimal PR_PONDERACION { get; set; }

    }
}
