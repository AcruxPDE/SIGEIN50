using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_BANCO
    {
        public string CL_BANCO { get; set; }
        public string NB_BANCO { get; set; }
        public bool FG_ACTIVO { get; set; }
        public string DS_ACTIVO { get; set; }
        public string NB_RAZON_SOCIAL { get; set; }
    }
}
