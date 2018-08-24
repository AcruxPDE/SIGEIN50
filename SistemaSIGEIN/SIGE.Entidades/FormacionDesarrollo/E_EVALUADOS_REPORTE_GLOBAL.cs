using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_EVALUADOS_REPORTE_GLOBAL
    {
        public int ID_EMPLEADO { get; set; }
        public int ID_EVALUADO { get; set; }
        public string CL_EVALUADO { get; set; }
        public string NB_EVALUADO { get; set; }
        public int ID_PUESTO { get; set; }
        public int ID_PUESTO_PERIODO { get; set; }
        public byte[] FI_FOTOGRAFIA { get; set; }
    }
}
