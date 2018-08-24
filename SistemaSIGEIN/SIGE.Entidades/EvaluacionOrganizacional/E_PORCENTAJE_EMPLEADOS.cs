using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
     [Serializable]
    public class E_PORCENTAJE_EMPLEADOS
    {
        public string NB_CANTIDAD { set; get; }
        public string PR_CANTIDAD { set; get; }
    }

     public class E_PORCENTAJES_EMPLEADOS
     {
         public string NB_CANTIDAD { set; get; }
         public decimal? PR_CANTIDAD { set; get; }
     }
}
