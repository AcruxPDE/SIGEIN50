using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    [Serializable]
    public class E_REPLICAS_ENVIAR
    {
        public int ID_EVALUADOR { get; set; }
        public string NB_EVALUADOR { get; set; }
        public Nullable<System.Guid> FL_EVALUADOR { get; set; }
        public string CL_TOKEN { get; set; }
        public string CL_CORREO_ELECTRONICO { get; set; }
        public Nullable<System.DateTime> FE_ENVIO_SOLICITUD { get; set; }
        public int ID_PERIODO { get; set; }
    }
}
