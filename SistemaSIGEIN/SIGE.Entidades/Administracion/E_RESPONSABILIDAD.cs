using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_RESPONSABILIDAD
    {
        public long RENGLON { get; set; }
        public int ID_EMPLEADO {get; set;}
        public string CL_EMPLEADO { get; set; }
        public string NB_EMPLEADO_COMPLETO { get; set; }
        public DateTime FE_INGRESO { get; set; }
        public string FE_ANTIGUEDAD { get; set; }
        public string CL_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
        public string NB_OBJETIVO_PUESTO { get; set; }
    }
}
