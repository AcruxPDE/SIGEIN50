using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.MetodologiaCompensacion
{
     [Serializable]
   public class E_CUARTIL_COMPARACION
    {
         public int?  NO_NIVEL { set; get; }
         public decimal? MN_CATEGORIA { set; get; }
         public string NB_CATEGORIA { set; get; }
         public int? NO_CATEGORIA { set; get; }
         public Guid? ID_ITEM { set; get; }
    }
}
