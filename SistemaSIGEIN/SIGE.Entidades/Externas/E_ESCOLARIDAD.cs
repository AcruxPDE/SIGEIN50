using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_ESCOLARIDADES
    {
        public int? ID_ESCOLARIDAD { get; set; }
        public string NB_ESCOLARIDAD { get; set; }
        public string DS_ESCOLARIDAD { get; set; }
        public int? CL_NIVEL_ESCOLARIDAD { get; set; }
        public bool FG_ACTIVO { get; set; }
        public string NB_ACTIVO { get; set; }
        public int? ID_NIVEL_ESCOLARIDAD { get; set; }
        public string CL_INSTITUCION { get; set; }
        public string CL_TIPO_ESCOLARIDAD { get; set; }
        public string CL_NB_NIVEL_ESCOLARIDAD { get; set; }
    }
}
