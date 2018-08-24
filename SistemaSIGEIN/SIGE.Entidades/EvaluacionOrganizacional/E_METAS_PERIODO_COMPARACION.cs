using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    [Serializable]
    public class E_METAS_PERIODO_COMPARACION
    {
        public int ID_EVALUADO_META { get; set; }
        public int ID_PERIODO { get; set; }
        public int ID_EVALUADO { get; set; }
        public Nullable<int> NO_META { get; set; }
        public string DS_FUNCION { get; set; }
        public string NB_INDICADOR { get; set; }
        public string DS_META { get; set; }
        public string CL_TIPO_META { get; set; }
        public bool FG_VALIDA_CUMPLIMIENTO { get; set; }
        public bool FG_EVALUAR { get; set; }
        public string NB_CUMPLIMIENTO_ACTUAL { get; set; }
        public string NB_CUMPLIMIENTO_MINIMO { get; set; }
        public string NB_CUMPLIMIENTO_SATISFACTORIO { get; set; }
        public string NB_CUMPLIMIENTO_SOBRESALIENTE { get; set; }
        public Nullable<decimal> PR_META { get; set; }
        public string NB_RESULTADO { get; set; }
        public Nullable<decimal> PR_RESULTADO { get; set; }
        public Nullable<int> CL_NIVEL { get; set; }
        public Nullable<decimal> PR_CUMPLIMIENTO_META { get; set; }
        public Nullable<bool> FG_EVIDENCIA { get; set; }
        public Nullable<decimal> PR_EVALUADO { get; set; }
        public string NIVEL_ALZANZADO { get; set; }
        public string COLOR_NIVEL { get; set; }
    }
}
