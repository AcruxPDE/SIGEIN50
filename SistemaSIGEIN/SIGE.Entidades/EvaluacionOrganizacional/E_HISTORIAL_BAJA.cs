
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
     [Serializable]
    public class E_HISTORIAL_BAJA
    {
       
        public DateTime FECHA_BAJA { get; set; }
        public int? ID_EMPLEADO { get; set; }
        public string CL_EMPLEADO { get; set; }
        public string NB_EMPLEADO { get; set; }
        public string NB_CAUSA_BAJA{ get; set; }
        public string DS_COMENTARIO { set; get; }
        public string CL_ESTADO { get; set; }
        public DateTime?  FECHA_REINGRESO { get; set; }
    }
}
