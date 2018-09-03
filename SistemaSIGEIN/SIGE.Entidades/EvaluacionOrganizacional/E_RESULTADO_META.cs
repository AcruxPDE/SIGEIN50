using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    [Serializable]
    public class E_RESULTADO_META
    {
        public int ID_EVALUADO_META { get; set; }
        public int CL_META { get; set; }
        public decimal  PONDERACION { get; set; }
        public string MINIMO { get; set; }
        public string SATISFACTORIO { get; set; }
        public string SOBRESALIENTE { get; set; }
        public decimal CUMPLIMIENTO { get; set; }
        public int RANGOVALOR { get; set; }
        public string RESULTADO { get; set; }

    }
}