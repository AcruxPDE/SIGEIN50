using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_CUESTIONARIO
    {
        public int ID_CUESTIONARIO { get; set; }
        public Nullable<int> ID_CUESTIONARIO_PLANTILLA { get; set; }
        public Nullable<int> ID_EVALUADO { get; set; }
        public Nullable<int> ID_EVALUADO_EVALUADOR { get; set; }
        public Nullable<int> ID_EVALUADOR { get; set; }
        public bool FG_CONTESTADO { get; set; }
        public string XML_PREGUNTAS_ADICIONALES { get; set; }
        public string XML_PREGUNTAS_CATALOGOS_ADICIONALES { get; set; }
        public string XML_CATALOGOS { get; set; }
    }
}
