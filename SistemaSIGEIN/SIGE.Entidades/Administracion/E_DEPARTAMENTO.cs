using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_DEPARTAMENTO
    {
        public string CL_DEPARTAMENTO { get; set; }
        public Nullable<System.DateTime> FE_INACTIVO { get; set; }
        public bool FG_ACTIVO { get; set; }
        public int? ID_DEPARTAMENTO { get; set; }
        public string NB_DEPARTAMENTO { get; set; }
        public string CL_TIPO_DEPARTAMENTO { get; set; }
        public int? ID_DEPARTAMENTO_PADRE { get; set; }
        public string NB_DEPARTAMENTO_PADRE { get; set; }
        public string XML_CAMPOS_ADICIONALES { get; set; }
    }
}
