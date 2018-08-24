using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_PROGRAMA_EMPLEADO
    {
        public int ID_PROGRAMA_EMPLEADO { get; set; }
        public int ID_PROGRAMA { get; set; }
        public int ID_EMPLEADO { get; set; }
        public int? ID_AUXILIAR { get; set; }
        public string NB_EMPLEADO { get; set; }
        public string CL_EMPLEADO { get; set; }
        public string NB_PUESTO { get; set; }
        public string CL_PUESTO { get; set; }
        public string NB_DEPARTAMENTO { get; set; }
        public string CL_PROGRAMA { get; set; }
        public string NB_PROGRAMA { get; set; }
    }
}
