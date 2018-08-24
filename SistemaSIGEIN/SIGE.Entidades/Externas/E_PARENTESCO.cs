using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
     public class E_PARENTESCO
    {
        public int ID_PARIENTE { get; set; }
        public string NB_PARIENTE { get; set; }
        public string CL_PARENTEZCO { get; set; }
        public string CL_GENERO { get; set; }
        public Nullable<System.DateTime> FE_NACIMIENTO { get; set; }
        public Nullable<int> ID_EMPLEADO { get; set; }
        public Nullable<int> ID_CANDIDATO { get; set; }
        public Nullable<int> ID_BITACORA { get; set; }
        public string CL_OCUPACION { get; set; }
        public bool FG_DEPENDIENTE { get; set; }
        public bool FG_ACTIVO { get; set; }
        public string DS_FILTRO { get; set; }
    }
}
