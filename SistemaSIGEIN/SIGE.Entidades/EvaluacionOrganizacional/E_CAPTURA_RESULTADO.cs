using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    public class E_CAPTURA_RESULTADO
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
        public int? ID_EMPRESA { get; set; }
        public string CL_EMPRESA { get; set; }
        public string NB_EMPRESA { get; set; }
        public string CL_ESTADO { get; set; }
        public string NB_ESTADO { get; set; }
    }
}
