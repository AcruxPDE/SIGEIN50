using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_OBTENE_ROL
    {
        public int ID_ROL { get; set; }
        public string CL_ROL { get; set; }
        public string NB_ROL { get; set; }
        public bool FG_ACTIVO { get; set; }
        public bool FG_SUELDO_VISIBLE { get; set; }
        public Nullable<int> ID_PLANTILLA { get; set; }
        public string XML_AUTORIZACION { get; set; }
        public string XML_GRUPOS { get; set; }
    }
}
