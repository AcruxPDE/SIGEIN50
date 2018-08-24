using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_ES_DATOS_ECONOMICOS
    {
        public int ID_DATO_PROPIEDAD { get; set; }
        public int ID_ESTUDIO_SOCIOECONOMICO { get; set; }
        public string CL_TIPO_PROPIEDAD { get; set; }
        public string CL_TIPO_ZONA { get; set; }
        public string CL_FORMA_ADQUISICION { get; set; }
        public string DS_FORMA_ADQUISICION { get; set; }
        public string XML_INGRESOS { get; set; }
        public string XML_EGRESOS { get; set; }
    }
}
