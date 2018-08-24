using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.SecretariaTrabajoPrevisionSocial
{
    [Serializable]
    public class E_SUBAREA_OCUPACION
    {
        public int? ID_SUBAREA { get; set; }
        public string CL_SUBAREA { get; set; }
        public string NB_SUBAREA { get; set; }
        public string CL_AREA { get; set; }
        public bool FG_ACTIVO { get; set; }
    }
}
