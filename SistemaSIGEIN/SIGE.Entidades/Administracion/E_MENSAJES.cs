using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_MENSAJES
    {
        public string CL_CLAVE_RETORNO { get; set; }
        public string NB_MENSAJE_RETORNO { get; set; }
        public string NB_VALOR_RETRONO { get; set; }
        public byte[] CL_BYTES_RETORNO { get; set; }
    }
}
