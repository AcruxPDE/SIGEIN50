using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class SPE_OBTIENE_FACTORES_CONSULTA
    {
        public int ID_FACTOR { get; set; }
        public string CL_FACTOR { get; set; }
        public string NB_FACTOR { get; set; }
        public string DS_FACTOR { get; set; }
        public int ID_VARIABLE { get; set; }
        public string NB_ABREVIATURA { get; set; }
        public string CL_VARIABLE { get; set; }
        public string NB_PRUEBA { get; set; }
        public int CL_TIPO_VARIABLE { get; set; }
    }
}
