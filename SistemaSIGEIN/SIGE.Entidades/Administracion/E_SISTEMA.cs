using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_SISTEMA
    {
        public System.Guid? ID_SISTEMA { get; set; }
        public string CL_SISTEMA { get; set; }
        public string NB_SISTEMA { get; set; }
        public string CL_TIPO { get; set; }
        public string DS_SISTEMA { get; set; }
    }
}
