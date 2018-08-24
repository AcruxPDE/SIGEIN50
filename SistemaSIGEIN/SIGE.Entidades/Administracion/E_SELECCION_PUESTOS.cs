using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_SELECCION_PUESTOS
    {
        public int idPuesto { get; set; }
        public string clPuesto { get; set; }
        public string nbPuesto { get; set; }
    }
}
