using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    [Serializable]
    public class E_INDICADORES_METAS
    {
        public int ID_EVALUADO_FUNCION { get; set; }
        public string NB_FUNCION { get; set; }
        public int ID_EVALUADO { get; set; }
        public int ID_PERIODO { get; set; }
        public string NB_PERIODO { get; set; }
        public int ID_EVALUADO_META { get; set; }
        public string NB_INDICADOR { get; set; }
        public string NO_META { get; set; }
        public string DS_META { get; set; }
        public string DS_FUNCION { get; set; }
        public string CL_TIPO_META { get; set; }
    }
}
