using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.PuntoDeEncuentro
{
    [Serializable]
    public partial class E_USUARIO_PDE
    {
        public int ID_EMPLEADO { get; set; }

        public string CL_EMPLEADO { get; set; }

        public string NB_EMPLEADO { get; set; }

        public string NB_PUESTO { get; set; }

        public string NB_DEPARTAMENTO { get; set; }
        
        public string CL_CORREO_ELECTRONICO { get; set; }
        
        public string NB_ESTATUS { get; set; }
        
        public int MN_COUNT { get; set; }

        public string NB_CONTRASEÑA { get; set; }
    }
}
