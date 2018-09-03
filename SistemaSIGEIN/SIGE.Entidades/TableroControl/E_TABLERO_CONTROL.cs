using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.TableroControl
{
    [Serializable]
    public class E_TABLERO_CONTROL
    {
        public int? ID_PERIODO { get; set; }
        public string CL_PERIODO { get; set; }
        public string NB_PERIODO { get; set; }
        public string DS_PERIODO { get; set; }
        public string DS_NOTAS { get; set; }
        public bool FG_EVALUACION_IDP { get; set; }
        public bool FG_EVALUACION_FYD { get; set; }
        public bool FG_EVALUACION_DESEMPENO { get; set; }
        public bool FG_EVALUACION_CLIMA { get; set; }
        public bool FG_SITUACION_SALARIAL { get; set; }
    }
}
