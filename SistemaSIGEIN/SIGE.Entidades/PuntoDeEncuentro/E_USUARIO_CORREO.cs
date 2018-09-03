using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.PuntoDeEncuentro
{  [Serializable ]
    public  class E_USUARIO_CORREO
    {
        public string CL_USUARIO { set; get; }
        public DateTime  FE_ENVIO { set; get; }
        public bool  FG_ENVIADO { set; get; }
    }
}
