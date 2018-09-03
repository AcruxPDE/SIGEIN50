using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_AVANCE_PROGRAMA_CAPACITACION
    {
        public int ID_PROGRAMA_COMPETENCIA { get; set; }
        public string NB_CATEGORIA { get; set; }
        public string NB_CLASIFICACION { get; set; }
        public string CL_COLOR { get; set; }
        public int ID_COMPETENCIA { get; set; }
        public string NB_COMPETENCIA { get; set; }
        public string CL_EMPLEADO { get; set; }
        public string CL_PUESTO { get; set; }
        public int ID_PROGRAMA_EMPLEADO { get; set; }
        public string NB_EMPLEADO { get; set; }
        public int? ID_EMPLEADO { get; set; }
        public string NB_PUESTO { get; set; }
        public Nullable<int> ID_PROGRAMA_EMPLEADO_COMPETENCIA { get; set; }
        public Nullable<int> ID_EVENTO { get; set; }
        public Nullable<System.DateTime> FE_INICIO { get; set; }
        public Nullable<System.DateTime> FE_TERMINO { get; set; }
        public Nullable<int> ID_EVENTO_PARTICIPANTE { get; set; }
        public Nullable<decimal> PR_CUMPLIMIENTO { get; set; }
        public string NB_EVENTOS_RELACIONADOS { get; set; }
        public int NO_COLOR_AVANCE { get; set; }
        public string CL_COLOR_AVANCE { get; set; }
        public string CL_COLOR_ASISTENCIA { get; set; }
    }
}
