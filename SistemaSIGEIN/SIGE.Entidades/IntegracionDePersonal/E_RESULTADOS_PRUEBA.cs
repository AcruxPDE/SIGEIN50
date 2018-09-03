using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.IntegracionDePersonal
{
    [Serializable]
   public class E_RESULTADOS_PRUEBA
    {
        public int ID_PRUEBA { get; set; }
        public Nullable<int> ID_PREGUNTA { get; set; }
        public int ID_VARIABLE { get; set; }
        public string CL_PREGUNTA { get; set; }
        public int CL_TIPO_VARIABLE { get; set; }
        public string NB_PREGUNTA { get; set; }
        public string NB_RESPUESTA { get; set; }
        public Nullable<decimal> NO_VALOR_RESPUESTA { get; set; }
    }
}
