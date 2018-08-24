using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{ [Serializable]
   public class E_EMPRESAS
    {
        public int ID_EMPRESA { get; set; }
        public string CL_EMPRESA { get; set; }
        public string NB_EMPRESA { get; set; }
        public string NB_RAZON_SOCIAL { get; set; }
        public string DS_FILTRO { get; set; }
    }
}
