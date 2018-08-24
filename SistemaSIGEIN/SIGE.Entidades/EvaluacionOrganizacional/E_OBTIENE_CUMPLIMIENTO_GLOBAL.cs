using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    [Serializable]
    public class E_OBTIENE_CUMPLIMIENTO_GLOBAL
    {
        public int ID_PERIODO { get; set; }
        public string CL_PUESTO_PERIODO { get; set; }
        public string NB_PUESTO_PERIODO { get; set; }
        public string CL_PUESTO_ACTUAL { get; set; }
        public string NB_PUESTO_ACTUAL { get; set; }
        public string NB_EVALUADO { get; set; }
        public Nullable<decimal> PR_EVALUADO { get; set; }
        public Nullable<decimal> PR_CUMPLIMIENTO_EVALUADO { get; set; }
        public Nullable<decimal> C_GENERAL { get; set; }
        public int ID_EVALUADO { get; set; }
        public int ID_BONO_EVALUADO { get; set; }
        public Nullable<int> ID_EMPLEADO { get; set; }
        public string CL_EMPLEADO { get; set; }
        public Nullable<decimal> CUMPLIDO { get; set; }
    }
}
