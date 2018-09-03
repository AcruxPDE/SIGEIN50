using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
     [Serializable]
    public class E_INSTRUCCIONES
    {
         public string TEMA { set; get; }
         public string LIDERAZGO { set; get; }
         public int ORDEN { set; get; }
    }
}
