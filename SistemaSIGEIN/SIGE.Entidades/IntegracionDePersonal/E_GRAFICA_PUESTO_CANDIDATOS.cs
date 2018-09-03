using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.IntegracionDePersonal
{
     [Serializable]
    public class E_GRAFICA_PUESTO_CANDIDATOS
    {
        public Nullable<int> ID_BATERIA { get; set; }
        public string NB_CANDIDATO { get; set; }
        public string CL_CANDIDATO { get; set; }
        public string NB_PUESTO { get; set; }
        public string CL_PUESTO { get; set; }
        public string CL_COMPETENCIA { get; set; }
        public string NB_COMPETENCIA { get; set; }
        public string DS_COMPETENCIA { get; set; }
        public Nullable<decimal> NO_VALOR_NIVEL { get; set; }
        public Nullable<int> NO_VALOR_CANDIDATO { get; set; }
        public Nullable<decimal> PR_CANDIDATO { get; set; }
        public Nullable<decimal> PR_CANDIDATO_PUESTO { get; set; }
        public int ID_CANDIDATO { get; set; }
        public int ID_PUESTO { get; set; }
        public string CL_COLOR { get; set; }
        public Nullable<decimal> PR_TRUNCADO { get; set; }
        public string CL_SOLICITUD { get; set; }
    }

     [Serializable]
     public class E_PROMEDIO
     {
        public string NB_PUESTO { get; set; }
        public Nullable<decimal> PORCENTAJE { get; set; }
        public Nullable<decimal> PORCENTAJE_NO_TRUNCADO { get; set; }
        public Nullable<decimal> PROMEDIO { get; set; }
        public Nullable<bool> FG_SUPERA_130 { get; set; }
        public string PROMEDIO_TXT { get; set; }
    }
    
}
