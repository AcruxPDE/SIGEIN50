using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_EVALUADOR_EXTERNO
    {
        public string CL_EVALUADOR_EXTERNO { get; set; }
        public string DS_EVALUARDO_EXTERNO { get; set; }
        public bool FG_ACTIVO { get; set; }
        public int? ID_EVALUADOR_EXTERNO { get; set; }
        public string NB_EVALUADOR_EXTERNO { get; set; }
        public string XML_CAMPOS_ADICIONALES { get; set; } 
    }
}
