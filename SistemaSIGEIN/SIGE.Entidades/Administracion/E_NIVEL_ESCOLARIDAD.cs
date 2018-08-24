using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_NIVEL_ESCOLARIDAD
    {
        public string CL_NIVEL_ESCOLARIDAD { get; set; }
        public string DS_NIVEL_ESCOLARIDAD { get; set; }
        public string CL_TIPO_ESCOLARIDAD { get; set; }
        public bool FG_ACTIVO { get; set; }
        public int? ID_NIVEL_ESCOLARIDAD { get; set; } 
    }
}
