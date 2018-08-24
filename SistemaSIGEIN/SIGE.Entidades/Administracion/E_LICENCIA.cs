using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    public class E_LICENCIA
    {
        public Guid? ID_LICENCIAMIENTO { get; set; }
        public string CL_CLIENTE { get; set; }
        public string CL_SISTEMA { get; set; }
        public string CL_EMPRESA { get; set; }
        public string CL_MODULO { get; set; }
        public string NO_RELEASE { get; set; }
        public string CL_LICENCIA { get; set; }
        public string FE_INICIO { get; set; }
        public string FE_FIN { get; set; }
        public string NO_VOLUMEN { get; set; }
    }
}
