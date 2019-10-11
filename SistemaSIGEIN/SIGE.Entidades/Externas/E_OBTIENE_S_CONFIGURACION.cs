using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_OBTIENE_S_CONFIGURACION
    {
        public string CL_CLIENTE { get; set; }
        public string CL_CONFIGURACION { get; set; }
        public string NB_CONFIGURACION { get; set; }
        public string NO_CONFIGURACION { get; set; }
        public string DS_CONFIGURACION { get; set; }
        public string TIPO_CONTROL { get; set; }
    }
}
