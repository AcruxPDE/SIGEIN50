using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.MetodologiaCompensacion
{
    [Serializable]
    public class E_NIVEL
    {
        public int ID_PUESTO { get; set; }
        public int NO_NIVEL { get; set; }
        public decimal NO_VALOR { get; set; }
    }
}
