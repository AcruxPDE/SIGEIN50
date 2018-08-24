using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.IntegracionDePersonal
{
      [Serializable]
    public class E_CONSULTA_PERSONAL
    {
        public string CL_CLASIFICACION { get; set; }
        public string CL_COMPETENCIA { get; set; }
        public Nullable<int> ID_COMPETENCIA { get; set; }
        public string NB_COMPETENCIA { get; set; }
        public string DS_COMPETENCIA { get; set; }
        public string CL_COLOR { get; set; }
        public Nullable<decimal> NO_BAREMO_PROMEDIO { get; set; }
        public Nullable<decimal> NO_BAREMO_PORCENTAJE { get; set; }
        public Nullable<int> NO_BAREMO_FACTOR { get; set; }
        public string DS_NIVEL_COMPETENCIA_PERSONA { get; set; }
    }
}
