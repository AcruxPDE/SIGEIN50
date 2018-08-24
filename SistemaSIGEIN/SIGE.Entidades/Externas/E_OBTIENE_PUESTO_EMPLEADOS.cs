using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{[Serializable]
  public  class E_OBTIENE_PUESTO_EMPLEADOS
    {

    public string ID_EMPLEADO { set; get; }
    public string ID_PUESTO  { set; get; }
    public string CL_PUESTO { set; get; }
    public string NB_PUESTO { set; get; }
    public bool FG_ACTIVO { set; get; }
    public bool NB_CORREO { set; get; }
    public string  CL_USUARIO { set; get; }

    }
}
