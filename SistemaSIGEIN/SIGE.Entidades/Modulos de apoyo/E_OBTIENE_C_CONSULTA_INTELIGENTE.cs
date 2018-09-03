using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Modulos_de_apoyo
{
    [Serializable]
    public class E_OBTIENE_C_CONSULTA_INTELIGENTE
    {
        public int ID_CUBO { get; set; }
        public string NB_ARCHIVO { get; set; }
        public string NB_CATALOGO { get; set; }
        public int ID_ARCHIVO { get; set; }
        public Nullable<System.Guid> ID_ITEM { get; set; }
        public byte[] FI_ARCHIVO { get; set; }
    }
}
