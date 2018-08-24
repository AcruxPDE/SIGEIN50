using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public partial class E_CATALOGO_LISTA
    {
        public int ID_CATALOGO_LISTA { get; set; }
        public string NB_CATALOGO_LISTA { get; set; }
        public string DS_CATALOGO_LISTA { get; set; }
        public int ID_CATALOGO_TIPO { get; set; }
        public string NB_CATALOGO_TIPO { get; set; }
        public Nullable<bool> FG_SISTEMA { get; set; }
    }
}
