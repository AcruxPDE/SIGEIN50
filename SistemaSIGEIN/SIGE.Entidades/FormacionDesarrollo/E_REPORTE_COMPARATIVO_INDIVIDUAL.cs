using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
   public class E_REPORTE_COMPARATIVO_INDIVIDUAL
    {
        public int ID_PERIODO { get; set; }
        public string CL_PERIODO { get; set; }
        public string NB_PERIODO { get; set; }
        public string DS_PERIODO { get; set; }
        public int ID_EVALUADO { get; set; }
        public string CL_COLOR { get; set; }
        public int ID_COMPETENCIA { get; set; }
        public string NB_COMPETENCIA { get; set; }
        public Nullable<decimal> PR_COMPATIBILIDAD { get; set; }
        public Nullable<long> NO_LINEA { get; set; }
    }
}
