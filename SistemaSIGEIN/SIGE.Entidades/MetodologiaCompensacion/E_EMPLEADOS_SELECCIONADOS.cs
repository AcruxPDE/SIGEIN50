using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.MetodologiaCompensacion
{
    [Serializable]
    public class E_EMPLEADOS_SELECCIONADOS
    {
        public int ID_TABULADOR { get; set; }
        public string XML_ID_SELECCIONADOS { set; get; }
        public int? ID_TABULADOR_FACTOR { get; set; }
    }
}
