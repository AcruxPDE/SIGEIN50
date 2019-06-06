using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    [Serializable]
    public class E_BAJAS_PERIODO_EDD
    {
        public int? ID_PERIODO { get; set; }
        public string CL_PERIODO { get; set; }
        public int? ID_EMPLEADO { get; set; }
        public string NB_EVALUADOR { get; set; }
    }
}
