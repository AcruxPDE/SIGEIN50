using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    [Serializable]
    public class E_METAS_EVALUADO_PERIODO
    {
        public int NUM_PERIODO { get; set; }
        public string DS_META { get; set; }
        public Nullable<decimal> PR_CUMPLIMIENTO { get; set; }
    }
}
