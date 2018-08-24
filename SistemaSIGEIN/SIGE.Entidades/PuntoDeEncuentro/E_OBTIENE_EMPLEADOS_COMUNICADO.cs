using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.PuntoDeEncuentro
{
    [Serializable]
   public class E_OBTIENE_EMPLEADOS_COMUNICADO
    {
        public string  ID_EMPLEADO { get; set; }
        public string NB_EMPLEADO { get; set; }
        public string NB_PUESTO { get; set; }
        public string CL_USUARIO { get; set; }
        public string M_CL_USUARIO { get; set; }

        
    }
}
