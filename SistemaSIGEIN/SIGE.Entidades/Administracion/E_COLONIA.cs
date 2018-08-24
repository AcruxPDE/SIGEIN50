using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_COLONIA
    {
        public string CL_COLONIA { get; set; }
        public string CL_ESTADO { get; set; }
        public string CL_MUNICIPIO { get; set; }
        public string CL_PAIS { get; set; }
        public string CL_TIPO_ASENTAMIENTO { get; set; }
        public int? ID_COLONIA { get; set; }
        public string NB_COLONIA { get; set; } 
    }
}
