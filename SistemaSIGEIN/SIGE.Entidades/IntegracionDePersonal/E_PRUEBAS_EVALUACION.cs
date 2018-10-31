using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.IntegracionDePersonal
{
    [Serializable]
    public class E_PRUEBAS_EVALUACION
    {
        public int ID_PRUEBA { get; set; }
        public string CL_PRUEBA { get; set; }
        public string NB_PRUEBA { get; set; }
        public int ID_CUESTIONARIO { get; set; }
        public short NO_TIEMPO_PRUEBA { get; set; }
    }
}
