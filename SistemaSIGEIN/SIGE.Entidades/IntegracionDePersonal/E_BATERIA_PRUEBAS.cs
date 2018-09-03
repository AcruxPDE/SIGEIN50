using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.IntegracionDePersonal
{
    [Serializable]
   public class E_BATERIA_PRUEBAS
    {
        public int ID_BATERIA { get; set; }
        public string FL_BATERIA { get; set; }
        public string NB_CANDIDATO { get; set; }
        public string CL_CORREO_ELECTRONICO { get; set; }
        public int ID_CANDIDATO { get; set; }
        public Nullable<System.Guid> CL_TOKEN { get; set; }
    }
}
