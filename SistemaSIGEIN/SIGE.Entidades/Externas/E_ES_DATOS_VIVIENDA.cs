using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_ES_DATOS_VIVIENDA
    {
        public int ID_DATO_VIVIENDA { get; set; }
        public int ID_ESTUDIO_SOCIOECONOMICO { get; set; }
        public string CL_TIPO_VIVIENDA { get; set; }
        public string CL_TIPO_POSESION { get; set; }
        public string CL_TIPO_CONSTRUCCION { get; set; }
        public string DS_TIPO_CONSTRUCCION { get; set; }
        public Nullable<byte> NO_HABITACIONES { get; set; }
        public Nullable<byte> NO_BANIOS { get; set; }
        public Nullable<byte> NO_PATIOS { get; set; }
        public Nullable<byte> NO_HABITANTES { get; set; }
        public string XML_SERVICIOS { get; set; }
        public string XML_BIENES_MUEBLES { get; set; }
    }
}
