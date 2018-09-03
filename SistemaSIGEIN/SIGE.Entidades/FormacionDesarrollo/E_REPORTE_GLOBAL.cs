using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_REPORTE_GLOBAL
    {
        public Guid vIdReporteGlobal { get; set; }
        public int vIdPeriodo { get; set; }
        public List<int> vListaEmpleados { get; set; }


        public E_REPORTE_GLOBAL()
        {
            vListaEmpleados = new List<int>();
            vIdPeriodo = 0;
        }
    }

   
}
