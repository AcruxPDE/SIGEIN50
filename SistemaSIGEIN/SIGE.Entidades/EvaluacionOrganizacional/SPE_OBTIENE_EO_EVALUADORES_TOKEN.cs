using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    [Serializable]
     public class SPE_OBTIENE_EO_EVALUADORES_TOKEN
    {
        public int ID_EVALUADOR { get; set; }
        public string NB_EVALUADOR { get; set; }
        public string NB_PUESTO { get; set; }
        public string CL_TOKEN { get; set; }
        public Nullable<System.Guid> FL_EVALUADOR { get; set; }
        public string CL_CORREO_ELECTRONICO { get; set; }
        public string CL_EVALUADOR { get; set; }
        public bool FG_CAPTURA_MASIVA { get; set; }
        public string CL_ESTADO_EMPLEADO { get; set; }
    }
}
