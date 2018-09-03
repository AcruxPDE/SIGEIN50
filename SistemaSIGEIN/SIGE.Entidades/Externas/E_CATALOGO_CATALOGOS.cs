using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_CATALOGO_CATALOGOS
    {
        public int? ID_CATALOGO_VALOR { get; set; }
        public string CL_CATALOGO_VALOR { get; set; }
        public string NB_CATALOGO_VALOR { get; set; }
        public string DS_CATALOGO_VALOR { get; set; }
        public int? ID_CATALOGO_LISTA { get; set; }
        public string NB_CATALOGO_LISTA { get; set; }
        public bool FG_SELECCIONADO { get; set; }

    }
}
