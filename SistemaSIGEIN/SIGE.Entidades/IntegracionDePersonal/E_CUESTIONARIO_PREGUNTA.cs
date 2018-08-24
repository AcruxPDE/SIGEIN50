using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.IntegracionDePersonal
{
    [Serializable]
    public class E_CUESTIONARIO_PREGUNTA
    {

        public int ID_CUESTIONARIO_PREGUNTA { get; set; }
        public int ID_CUESTIONARIO { get; set; }
        public int ID_PREGUNTA { get; set; }
        public string NB_PREGUNTA { get; set; }
        public string NB_RESPUESTA { get; set; }
        public decimal NO_VALOR_RESPUESTA { get; set; }
        public string DS_FILTRO { get; set; }
    }
}
