using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_DEPENDIENTE_ECONOMICO
    {
        public string CL_GENERO { get; set; }
        public bool CL_OCUPACION { get; set; }
        public string CL_PARENTEZCO { get; set; }
        public Nullable<System.DateTime> FE_NACIMIENTO { get; set; }
        public bool FG_ACTIVO { get; set; }
        public int? ID_BITACORA { get; set; }
        public int? ID_DEPENDIENTE_ECONOMICO { get; set; }
        public string NB_DEPENDIENTE_ECONOMICO { get; set; } 
    }
}
