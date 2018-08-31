using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.MetodologiaCompensacion
{
    [Serializable]
    public class E_REPORTE_TABULADOR_SUELDOS
    {
        public List<int> vLstEmpleadosTabulador { get; set; }
        public int ID_TABULADOR { get; set; }

        public E_REPORTE_TABULADOR_SUELDOS()
        {
            vLstEmpleadosTabulador = new List<int>();
        }
    }
}
