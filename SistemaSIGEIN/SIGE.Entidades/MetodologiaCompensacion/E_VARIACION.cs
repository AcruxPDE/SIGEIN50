using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.MetodologiaCompensacion
{
     [Serializable]
    public class E_VARIACION
    {
         public int RANGO_INFERIOR { get; set; }
         public int RANGO_SUPERIOR { get; set; }
         public string COLOR { get; set; }
         
    }
}
