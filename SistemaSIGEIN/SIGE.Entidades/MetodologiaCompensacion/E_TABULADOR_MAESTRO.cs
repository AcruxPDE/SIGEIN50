using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.MetodologiaCompensacion
{
     [Serializable]
    public class E_TABULADOR_MAESTRO
    {
         public int? ID_TABULADOR_MAESTRO { set; get; }
         public Guid ID_ITEM { get; set; }
         public int? ID_TABULADOR_NIVEL { set; get; }
         public string NB_TABULADOR_NIVEL { set; get; }
         public int? NO_CATEGORIA { set; get; }
         public string NB_CATEGORIA { set; get; }
         public decimal? PR_PROGRESION { set; get; }
         public decimal? MN_MINIMO { set; get; }
         public decimal? MN_PRIMER_CUARTIL { set; get; }
         public decimal? MN_MEDIO { set; get; }
         public decimal? MN_SEGUNDO_CUARTIL { set; get; }
         public decimal? MN_MAXIMO { set; get; }
         public decimal? MN_SIGUIENTE { set; get; }
         public int? NO_NIVEL { set; get; }
         public string CL_TABULADOR { set; get; }



         public E_TABULADOR_MAESTRO()
         {
             ID_ITEM = Guid.NewGuid();
         }
    }

}
