using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.Entidades.IntegracionDePersonal
{
    [Serializable]
   public class E_RESULTADOS_BATERIA
    {

        //public int? ID_BATERIA { get; set; }
        //public string CL_PRUEBA { get; set; }
        //public Nullable<decimal> NO_VALOR { get; set; }
        //public string CL_VARIABLE { get; set; }
        //public string XML_MENSAJES { get; set; }

        public int? ID_BATERIA { get; set; }
        public Nullable<decimal> NO_VALOR { get; set; }
        public string CL_PRUEBA { get; set; }
        public string CL_VARIABLE { get; set; }
        public string XML_MENSAJES { get; set; }

    }
}
