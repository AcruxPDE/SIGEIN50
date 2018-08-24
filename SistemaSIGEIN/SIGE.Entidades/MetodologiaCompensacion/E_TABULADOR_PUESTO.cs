using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.MetodologiaCompensacion
{
     [Serializable]
   public  class E_TABULADOR_PUESTO
        {
             public int NO_RENGLON { get; set; }
             public int ID_TABULADOR_PUESTO { get; set; }
            public int ID_PUESTO { get; set; }
            public decimal MN_MINIMO { get; set; }
            public decimal MN_MAXIMO { get; set; }
            public string CL_ORIGEN { get; set; }
            public string NB_DEPARTAMENTO { get; set; }
            public string NB_PUESTO { get; set; }
            public int NO_NIVEL { get; set; }
            
        }


}
