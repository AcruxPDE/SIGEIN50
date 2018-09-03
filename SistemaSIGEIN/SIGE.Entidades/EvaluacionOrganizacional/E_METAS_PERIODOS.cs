using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    [Serializable]
    public class E_METAS_PERIODOS
    {
      public int NO_META_EVALUA { get; set; }
      public string NO_META { get; set; }
      public string DS_META { get; set; }
      public string NB_PERIODO { get; set; }
      public Nullable<decimal> PR_CUMPLIMIENTO { get; set; }
      public string COLOR_NIVEL { get; set; }
    }
}
