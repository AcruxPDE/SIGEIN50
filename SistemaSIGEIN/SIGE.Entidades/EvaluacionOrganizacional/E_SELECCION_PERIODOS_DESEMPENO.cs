using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    [Serializable]
   public class E_SELECCION_PERIODOS_DESEMPENO
    {
        public int numPeriodo { get; set; }
        public int? idPeriodo { get; set; }
        public string clPeriodo { get; set; }
        public string nbPeriodo { get; set; }
        public string dsPeriodo { get; set; }
        public string dsNotas { get; set; }
        public string feInicio { get; set; }
        public string feTermino { get; set; }
        public int? idEvaluado { get; set; }
        public string clOrigen { get; set; }
        public string clTipoCopia { get; set; }
    }
}
