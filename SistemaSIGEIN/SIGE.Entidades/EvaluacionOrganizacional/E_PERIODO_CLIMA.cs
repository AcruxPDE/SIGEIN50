using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
     [Serializable]
    public class E_PERIODO_CLIMA
    {
        public int ID_PERIODO { get; set; }
        public string CL_PERIODO { get; set; }
        public string NB_PERIODO { get; set; }
        public string DS_PERIODO { get; set; }
        public DateTime? FE_INICIO { get; set; }
        public string CL_ESTADO_PERIODO { get; set; }
        public string DS_NOTAS { get; set; }
        public string XML_CAMPOS_ADICIONALES { get; set; }
        public int ID_PERIODO_CLIMA { get; set; }
        public string CL_TIPO_CONFIGURACION { get; set; }
        public string CL_ORIGEN_CUESTIONARIO { get; set; }
        public int? ID_PERIODO_ORIGEN { get; set; }
      
    }
}
