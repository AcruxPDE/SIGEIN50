using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_EVALUADOR
    {
        public int ID_EVALUADO_EVALUADOR { get; set; }
        public int ID_CUESTIONARIO { get; set; }
        public string CL_EVALUADOR { get; set; }
        public string NB_EVALUADOR { get; set; }
        public string CL_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
        public string CL_ROL_EVALUADOR { get; set; }
        public string NB_ROL_EVALUADOR { get; set; }
    }
}
