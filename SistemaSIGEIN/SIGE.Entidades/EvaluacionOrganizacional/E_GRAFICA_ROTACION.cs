using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    [Serializable]
    public class E_GRAFICA_ROTACION
    {
        public DateTime? FECHA { set; get; }
        public int? NO_CANTIDAD { set; get; }
        public string NB_CAUSA { set; get; }
        public decimal? PR_CANTIDAD { set; get; }
        public int? NO_EMPLEADOS_BAJA { set; get; }
        public int? NO_EMPLEADOS_ALTA { set; get; }
        public decimal? PR_TOTAL_BAJA { set; get; }
      
    }
}
