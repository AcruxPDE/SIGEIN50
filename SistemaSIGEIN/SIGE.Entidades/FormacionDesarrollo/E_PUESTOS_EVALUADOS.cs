using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_PUESTOS_EVALUADOS
    {
        public int ID_PUESTO { get; set; }
        public int ID_PUESTO_PERIODO { get; set; }
        public string CL_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
    }
}
