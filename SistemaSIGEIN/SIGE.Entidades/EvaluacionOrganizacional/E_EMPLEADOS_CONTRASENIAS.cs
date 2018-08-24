using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    public class E_EMPLEADOS_CONTRASENIAS
    {
        public Guid ID_ITEM { get; set; }
        public int? ID_EMPLEADO { get; set; }
        public string CL_EMPLEADO { get; set; }
        public string NB_EMPLEADO { get; set; }
        public int? ID_PUESTO { get; set; }
        public string CL_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
        public int? ID_DEPARTAMENTO { get; set; }
        public string CL_DEPARTAMENTO { get; set; }
        public string NB_DEPARTAMENTO { get; set; }
        public int? ID_CONTRASENIA { get; set; }
        public string NB_CONTRASENIA { get; set; }
    }
}
