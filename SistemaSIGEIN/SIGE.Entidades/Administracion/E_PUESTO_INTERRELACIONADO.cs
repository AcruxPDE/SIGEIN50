using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public partial class E_PUESTO_INTERRELACIONADO
    {
        public int ID_PUESTO { get; set; }
        public int NO_PLAZAS { get; set; }
        public string NB_PUESTO { get; set; }
        public string CL_PUESTO { get; set; }
    }
}
