using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class SPE_OBTIENE_COMPETENCIAS_CONSULTA
    {
        public int ID_COMPETENCIA { get; set; }
        public string CL_COMPETENCIA { get; set; }
        public string NB_COMPETENCIA { get; set; }
        public string DS_COMPETENCIA { get; set; }
        public string CL_TIPO_COMPETENCIA { get; set; }
        public string CL_CLASIFICACION { get; set; }
        public string CL_COLOR { get; set; }
        public string NB_CLASIFICACION_COMPETENCIA { get; set; }

    }
}
