using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_NECESIDADES_CAPACITACION
    {
        public int ID_PERIODO { get; set; }
        public string CL_TIPO_COMPETENCIA { get; set; }
        public string NB_TIPO_COMPETENCIA { get; set; }
        public string CL_CLASIFICACION { get; set; }
        public string NB_CLASIFICACION_COMPETENCIA { get; set; }
        public string CL_COLOR { get; set; }
        public int ID_COMPETENCIA { get; set; }
        public string NB_COMPETENCIA { get; set; }
        public string DS_COMPETENCIA { get; set; }
        public Nullable<int> ID_EMPLEADO { get; set; }
        public string CL_EVALUADO { get; set; }
        public string NB_EVALUADO { get; set; }
        public int ID_PUESTO { get; set; }
        public string CL_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
        public Nullable<int> ID_DEPARTAMENTO { get; set; }
        public string CL_DEPARTAMENTO { get; set; }
        public string NB_DEPARTAMENTO { get; set; }
        public Nullable<decimal> PR_RESULTADO { get; set; }
        public string NB_PRIORIDAD { get; set; }
    }
}
