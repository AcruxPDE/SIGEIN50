using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_OBTIENE_PLAN_SUCESION
    {
        public Nullable<int> ID_EMPLEADO { get; set; }
        public byte[] FI_FOTOGRAFIA { get; set; }
        public string CL_EMPLEADO { get; set; }
        public string NB_EMPLEADO { get; set; }
        public int ID_PUESTO { get; set; }
        public string CL_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
        public System.DateTime FE_ALTA { get; set; }
        public string NB_ANTIGUEDAD { get; set; }
        public int ID_PERIODO_COMPETENCIAS { get; set; }
        public string FE_INICIO_COMPETENCIAS { get; set; }
        public string PR_COMPETENCIAS { get; set; }
        public int ID_EVALUADO { get; set; }
        public int ID_PERIODO_DESEMPEÑO { get; set; }
        public string FE_INICIO_DESEMPEÑO { get; set; }
        public string PR_DESEMPEÑO { get; set; }
        public int ID_PROGRAMA { get; set; }
        public string NB_PROGRAMA { get; set; }
        public string CL_TABLERO_CONTROL { get; set; }
        public string CL_TIPO_EMPLEADO { get; set; }
        public string CL_POTENCIAL_SUCESOR { get; set; }
    }
}
