using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_CANDIDATO_SOLICITUD
    {
        public int ID_SOLICITUD { get; set; }
        public string CL_SOLICITUD { get; set; }
        public string C_CANDIDATO_NB_EMPLEADO_COMPLETO { get; set; }
        public string C_CANDIDATO_CL_CORREO_ELECTRONICO { get; set; }
    }
}
