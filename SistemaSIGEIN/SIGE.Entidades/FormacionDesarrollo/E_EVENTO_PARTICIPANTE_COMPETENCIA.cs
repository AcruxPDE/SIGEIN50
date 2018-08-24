using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_EVENTO_PARTICIPANTE_COMPETENCIA
    {
        public int ID_EVENTO { get; set; }
        public int ID_PARTICIPANTE { get; set; }
        public int NO_FILA { get; set; }
        public int ID_COMPETENCIA { get; set; }
        public string CL_TIPO_COMPETENCIA { get; set; }
        public string CL_COMPETENCIA { get; set; }
        public string NB_COMPETENCIA { get; set; }
        public decimal PR_EVALUACION_COMPETENCIA { get; set; }
    }
}
