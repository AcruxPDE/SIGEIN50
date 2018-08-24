using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{   [Serializable]
    public class E_CANDIDATO_AREAS_INTERES
    {
        public int ID_CANDIDATO_AREA_INTERES { get; set; }
        public int ID_CANDIDATO { get; set; }
        public int ID_AREA_INTERES { get; set; }
        public string DS_FILTRO { get; set; }
    //C_AREA_INTERES
        public string CL_AREA_INTERES { get; set; }
        public string NB_AREA_INTERES { get; set; }
    }
}
