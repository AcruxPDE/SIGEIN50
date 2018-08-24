using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.IntegracionDePersonal
{
    [Serializable]
   public class E_PRUEBA
    {
        public int ID_PRUEBA { get; set; }
        public int ID_PRUEBA_PLANTILLA { get; set; }
        public string CL_PRUEBA { get; set; }
        public string NB_PRUEBA { get; set; }
        public Nullable<int> ID_CANDIDATO { get; set; }
        public string CL_ESTADO { get; set; }
        public Nullable<System.Guid> CL_TOKEN_EXTERNO { get; set; }
        public string CL_CORREO_ELECTRONICO { get; set; }
        public Nullable<int> ID_BATERIA { get; set; }
    }
}
