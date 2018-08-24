using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    public class E_META_RESULTADOS
    {

        public int ID_EVALUADO_FUNCION { get; set; }
        public string NB_FUNCION { get; set; }
        public int ID_EVALUADO { get; set; }
        public int ID_EVALUADO_META { get; set; }
        public string NB_INDICADOR { get; set; }
        public string NO_META { get; set; }
        public string DS_META { get; set; }
        public string DS_FUNCION { get; set; }
        public string CL_TIPO_META { get; set; }
        public bool FG_EVALUAR { get; set; }
        public bool FG_VALIDA_CUMPLIMIENTO { get; set; }
        public string NB_CUMPLIMIENTO_ACTUAL { get; set; }
        public string NB_CUMPLIMIENTO_MINIMO { get; set; }
        public string NB_CUMPLIMIENTO_SATISFACTORIO { get; set; }
        public string NB_CUMPLIMIENTO_SOBRESALIENTE { get; set; }
        public string PR_META { get; set; }
        public Nullable<decimal> PR_RESULTADO { get; set; }
        public string PR_CUMPLIMIENTO { get; set; }
        public string COLOR_NIVEL { get; set; }
        public string NIVEL_ALZANZADO { get; set; }
        public Nullable<decimal> PR_CUMPLIMIENTO_META { get; set; }
         public string PR_EVALUADO { get; set; }
        
    }
}
