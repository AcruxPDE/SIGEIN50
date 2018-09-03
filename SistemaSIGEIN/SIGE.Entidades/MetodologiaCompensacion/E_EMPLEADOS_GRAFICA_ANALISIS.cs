using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.MetodologiaCompensacion
{
     [Serializable]
    public class E_EMPLEADOS_GRAFICAS
    {
         public int? ID_TABULADOR_EMPLEADO { set; get; }
         public int? ID_EMPLEADO { set; get; }
         public int? ID_TABULADOR { set; get; }
         public string NB_EMPLEADO { set; get; }
         public string NB_PUESTO { set; get; }
         public string NB_TABULADOR { set; get; }
         public decimal? MN_SUELDO_ORIGINAL { set; get; }
         public decimal NB_SUELDO_NUEVO { set; get; }
         public int? NO_NIVEL { set; get; }
         public string CL_TABULADOR { set; get; }
         public decimal? PR_DIFERENCIA { set; get; }
         public string CL_PR_DIFERENCIA { set; get; }


    }
}
