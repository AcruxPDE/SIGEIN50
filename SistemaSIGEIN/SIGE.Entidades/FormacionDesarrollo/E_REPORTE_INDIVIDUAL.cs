using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_REPORTE_INDIVIDUAL
    {
        public Guid vIdReporteIndividual { get; set; }
        public List<int> vListaPeriodos { get; set; }
        public int vIdPeriodo { get; set; }

        public E_REPORTE_INDIVIDUAL()
        {
            vListaPeriodos = new List<int>();
        }
    }
}
