using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    [Serializable]
    public class E_GRAFICAS
    {
        public int NO_NOMBRE { set; get; }
        public string NOMBRE { set; get; }
        public decimal? PORCENTAJE { set; get; }
        public string COLOR_PORCENTAJE { set; get; }
        public int? NO_RESPUESTA { set; get; }
        public int? NO_CANTIDAD { set; get; }

    }
}
