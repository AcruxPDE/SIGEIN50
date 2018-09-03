using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.IntegracionDePersonal
{
    [Serializable]
    public class E_RESULTADOS_PRUEBAS_REPORTE
    {
        public int ID_BATERIA { get; set; }
        public string CL_PRUEBA { get; set; }
        public string NB_PRUEBA { get; set; }
        public decimal NO_VALOR { get; set; }
        public Nullable<int> ID_CUESTIONARIO { get; set; }
        public string CL_VARIABLE { get; set; }
    }
}
