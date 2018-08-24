using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    [Serializable]
    public class E_PERIODOS_COMPARAR
    {
        public Nullable<long> NUM_PERIODO { get; set; }
        public int ID_PERIODO { get; set; }
        public string CL_PERIODO { get; set; }
        public string NB_PERIODO { get; set; }
        public string NB_PERIODO_ENCABEZADO { get; set; }
        public string DS_PERIODO { get; set; }
        public System.DateTime FE_INICIO { get; set; }
        public System.DateTime FE_TERMINO { get; set; }
        public string CL_ESTADO_PERIODO { get; set; }
        public string DS_NOTAS { get; set; }
        public string CL_TIPO_PERIODO { get; set; }
        public int ID_PERIODO_DESEMPENO { get; set; }
        public bool FG_BONO { get; set; }
        public decimal PR_BONO { get; set; }
        public decimal MN_BONO { get; set; }
        public string CL_TIPO_BONO { get; set; }
        public string CL_TIPO_CAPTURISTA { get; set; }
        public string CL_TIPO_METAS { get; set; }
        public string CL_ORIGEN_CUESTIONARIO { get; set; }
        public Nullable<int> ID_PERIODO_REPLICA { get; set; }
        public string CL_TIPO_COPIA { get; set; }
        public Nullable<int> NO_REPLICA { get; set; }
        public string NB_PUESTO { get; set; }
    }
}
