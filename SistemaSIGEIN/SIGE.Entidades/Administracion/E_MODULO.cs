using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_MODULO
    {
        public System.Guid? ID_MODULO { get; set; }
        public string CL_MODULO { get; set; }
        public string NB_MODULO { get; set; }
        public string DS_MODULO { get; set; }
        public System.Guid? ID_SISTEMA { get; set; }
    }
}
