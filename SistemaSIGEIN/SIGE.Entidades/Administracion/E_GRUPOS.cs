using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_GRUPOS
    {
        public int ID_GRUPO { get; set; }
        public string CL_GRUPO { get; set; }
        public string NB_GRUPO { get; set; }
        public bool FG_SISTEMA { get; set; }
    }
}
