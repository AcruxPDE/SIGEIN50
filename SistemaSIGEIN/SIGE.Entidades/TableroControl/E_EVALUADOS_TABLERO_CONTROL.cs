using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.TableroControl
{
    [Serializable]
    public class E_EVALUADOS_TABLERO_CONTROL
    {
        public int ID_EVALUADO { get; set; }
        public string CL_EVALUADO { get; set; }
        public Nullable<int> ID_EMPLEADO { get; set; }
        public string CL_EMPLEADO { get; set; }
        public string NB_EMPLEADO { get; set; }
        public Nullable<int> ID_PUESTO_PERIODO { get; set; }
        public string CL_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
        public string CL_DEPARTAMENTO { get; set; }
        public string NB_DEPARTAMENTO { get; set; }
        public string DS_COMENTARIO { get; set; }
        public string XML_CAMPOS_ADICIONALES { get; set; }
        public byte[] FI_FOTOGRAFIA { get; set; }
    }
}
