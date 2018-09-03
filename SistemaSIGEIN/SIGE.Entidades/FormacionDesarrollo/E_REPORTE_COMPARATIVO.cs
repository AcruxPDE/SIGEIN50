using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_REPORTE_COMPARATIVO
    {
        public Guid vIdReporteComparativo { get; set; }
        public List<int> vListaPeriodos { get; set; }
        public int vIdPeriodo { get; set; }

        public E_REPORTE_COMPARATIVO()
        {
            vListaPeriodos = new List<int>();
        }
    }
}
