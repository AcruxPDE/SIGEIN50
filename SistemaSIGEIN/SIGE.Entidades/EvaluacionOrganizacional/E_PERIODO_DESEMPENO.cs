using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
[Serializable]
    public class E_PERIODO_DESEMPENO
    {
        public int? ID_PERIODO { get; set; }
        public string CL_TIPO_PERIODO { get; set; }
        public string NB_PERIODO { get; set; }
        public string DS_PERIODO { get; set; }
        public string CL_ESTADO { get; set; }
        public string XML_DS_NOTAS { get; set; }
        public DateTime FE_INICIO_PERIODO { get; set; }
        public DateTime FE_TERMINO_PERIODO { get; set; }
        public string CL_TIPO_CAPTURISTA {get; set;}
        public string CL_TIPO_COPIA { get; set; }
    }
}
