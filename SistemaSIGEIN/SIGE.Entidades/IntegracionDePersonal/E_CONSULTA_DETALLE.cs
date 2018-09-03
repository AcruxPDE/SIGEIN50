using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.IntegracionDePersonal
{
     [Serializable]
    public class E_CONSULTA_DETALLE
    {
        public decimal NO_VALOR { get; set; }
        public int ID_VARIABLE { get; set; }
        public string CL_VARIABLE { get; set; }
        public string CL_FACTOR { get; set; }
        public string NB_FACTOR { get; set; }
        public string DS_FACTOR { get; set; }
        public int ID_FACTOR { get; set; }
        public int ID_COMPETENCIA { get; set; }
        public string NB_COMPETENCIA { get; set; }
        public string DS_COMPETENCIA { get; set; }
        public string CL_TIPO_COMPETENCIA { get; set; }
        public string CL_CLASIFICACION { get; set; }
        public string CL_COLOR { get; set; }
        public string NB_CLASIFICACION_COMPETENCIA { get; set; }
        public string NB_PRUEBA { get; set; }
        public string NB_ABREVIATURA { get; set; }
        public string ICONO { get; set; }
    }
}
