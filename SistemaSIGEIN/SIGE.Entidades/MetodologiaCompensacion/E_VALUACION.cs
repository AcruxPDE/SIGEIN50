using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.MetodologiaCompensacion
{
     [Serializable]
    public class E_VALUACION
    {
        public int ID_PUESTO { get; set; }
        public Nullable<int> ID_TABULADOR_PUESTO { get; set; }
        public Nullable<int> ID_COMPETENCIA { get; set; }
        public string NB_COMPETENCIA { get; set; }
        public string DS_COMPETENCIA { get; set; }
        public string CL_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
        public Nullable<int> ID_TABULADOR_FACTOR { get; set; }
        public decimal NO_VALOR { get; set; }
        public int ID_TABULADOR_VALUACION { get; set; }
        public string NB_TIPO_COMPETENCIA { get; set; }
        public Nullable<decimal> NO_PROMEDIO_VALUACION { get; set; }
        public Nullable<int> NO_NIVEL { get; set; }
        public string CL_TIPO_COMPETENCIA { get; set; }
    }
}
