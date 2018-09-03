using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.IntegracionDePersonal
{
    [Serializable]
   public class E_SOLICITUD
    {
        public int ID_SOLICITUD { get; set; }
        public Nullable<int> ID_CANDIDATO { get; set; }
        public Nullable<int> ID_EMPLEADO { get; set; }
        public int ID_DESCRIPTIVO { get; set; }
        public Nullable<int> ID_REQUISICION { get; set; }
        public string CL_SOLICITUD { get; set; }
        public string CL_ACCESO_EVALUACION { get; set; }
        public Nullable<int> ID_PLANTILLA_SOLICITUD { get; set; }
        public string DS_COMPETENCIAS_ADICIONALES { get; set; }
        public string CL_SOLICITUD_ESTATUS { get; set; }
        public Nullable<System.DateTime> FE_SOLICITUD { get; set; }

      

    }
}
