using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_CENTRO_OPTVO
    {
        public string ID_CENTRO_OPTVO { get; set; }
        public string NB_CENTRO_OPTVO { get; set; }
        public string CL_CENTRO_OPTVO { get; set; }
        public bool FG_SELECCIONADO { get; set; }
    }
}
