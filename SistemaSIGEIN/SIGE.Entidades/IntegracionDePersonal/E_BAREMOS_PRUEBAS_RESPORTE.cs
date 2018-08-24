using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.IntegracionDePersonal
{
    [Serializable]
    public class E_BAREMOS_PRUEBAS_RESPORTE
    {
        public int ID_FACTOR { get; set; }
        public string CL_FACTOR { get; set; }
        public string NB_FACTOR { get; set; }
        public string DS_FACTOR { get; set; }
        public Nullable<int> ID_VARIABLE { get; set; }
        public string CL_VARIABLE { get; set; }
        public string NB_PRUEBA { get; set; }
        public int CL_TIPO_VARIABLE { get; set; }
        public int CL_TIPO_RESULTADO { get; set; }
        public decimal NO_VALOR { get; set; }
        public int ID_CUESTIONARIO { get; set; }
        public int ID_BATERIA { get; set; }
    }
}
