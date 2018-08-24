using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
     [Serializable]
    public class E_BAJA_EMPLEADO
    {
      public int ID_EMPLEADO {set; get;}
      public string CL_EMPLEADO {set; get;}
      public string NB_EMPLEADO {set; get;}
      public int ID_CAUSA_ROTACION {set; get;}
      public string DS_COMENTARIOS {set; get;}
      public DateTime? FE_BAJA_EFECTIVA { set; get;}
      public int ID_PUESTO { get; set; } 
    }
}
