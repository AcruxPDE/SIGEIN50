using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_REPORTE_360
    {
        public int ID_PERIODO { get; set; }
        public int ID_PUESTO { get; set; }
        public string CL_PUESTO { get; set; }
        public long? NO_ORDEN_CONSECUTIVO { get; set; }
        public long NO_COMPETENCIA { get; set; }
        public int ID_COMPETENCIA { get; set; }
        public string NB_COMPETENCIA { get; set; }
        public decimal NO_VALOR_COMPETENCIA { get; set; }
        public string CL_COLOR { get; set; }
        public int NO_ORDEN { get; set; }
    }
}
