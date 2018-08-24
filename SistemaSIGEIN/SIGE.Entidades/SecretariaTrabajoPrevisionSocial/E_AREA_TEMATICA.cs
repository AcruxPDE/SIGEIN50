using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.SecretariaTrabajoPrevisionSocial
{
    [Serializable]
    public class E_AREA_TEMATICA
    {
        public int? ID_AREA_TEMATICA { get; set; }
        public string CL_AREA_TEMATICA { get; set; }
        public string NB_AREA_TEMATICA { get; set; }
        public bool FG_ACTIVO { get; set; }
    }
}
