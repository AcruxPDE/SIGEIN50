using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.PuntoDeEncuentro
{   [Serializable]
    public class E_OBTIENE_MIS_REPORTES
    {
        public Guid ID_COMPROMISO { get; set; }
        public DateTime PERIODO_DEL { get; set; }
        public DateTime PERIODO_AL { get; set; }
        public string EVALUADO { get; set; }
        public decimal CALIFICACION_GLOBAL { get; set; }
        public string CL_COMPROMISO { get; set; }
        public string NB_CALIFICACION { get; set; }
        
    }
}
