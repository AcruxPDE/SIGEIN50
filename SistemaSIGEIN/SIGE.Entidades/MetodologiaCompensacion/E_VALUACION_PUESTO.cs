using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.MetodologiaCompensacion
{
    [Serializable]
    public class E_VALUACION_PUESTO
    {
        public int ID_PUESTO { get; set; }
        public int ID_COMPETENCIA { get; set; }
        public int ID_TABULADOR_PUESTO { get; set; }
        public int NO_NIVEL { get; set; }
        public decimal NO_PROMEDIO_VALUACION { get; set; }
        public int NO_VALOR { get; set; }
        public int ID_TABULADOR_FACTOR { get; set; }

    }
}
