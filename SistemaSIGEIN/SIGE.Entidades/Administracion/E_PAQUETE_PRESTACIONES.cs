using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_PAQUETE_PRESTACIONES
    {
        public System.Guid ID_PAQUETE_PRESTACIONES { get; set; }
        public string CL_CLIENTE { get; set; }
        public string CL_PAQUETE { get; set; }
        public string DS_PAQUETE { get; set; }
        public bool FG_ACTIVO { get; set; }
        public string NB_ACTIVO { get; set; }
        public string XML_PARAMETROS { get; set; }
    }
}
