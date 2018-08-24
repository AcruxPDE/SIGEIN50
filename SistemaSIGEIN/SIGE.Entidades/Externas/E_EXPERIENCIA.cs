using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_EXPERIENCIA
    {
        public int ID_AREA_INTERES { get; set; }
        public string NB_AREA_INTERES { get; set; }
        public decimal NO_TIEMPO { get; set; }
        public string CL_TIPO_EXPERIENCIA { get; set; }
    }
}
