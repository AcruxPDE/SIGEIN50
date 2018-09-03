using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_ESCOLARIDAD
    {
        public int ID_ESCOLARIDAD { get; set; }
        public string CL_ESCOLARIDAD { get; set; }
        public string NB_ESCOLARIDAD { get; set; }
        public string DS_ESCOLARIDAD { get; set; }
        public string CL_NIVEL_ESCOLARIDAD { get; set; }
        public Nullable<int> CL_INSTITUCION { get; set; }
        public bool FG_ACTIVO { get; set; }
        public string NB_ACTIVO { get; set; }
        public string DS_FILTRO { get; set; }
    }
}
