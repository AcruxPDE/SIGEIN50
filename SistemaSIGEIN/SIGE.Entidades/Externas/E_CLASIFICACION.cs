using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public partial class E_CLASIFICACION
    {
        public string CL_CLASIFICACION { get; set; }
        public byte NO_ORDEN { get; set; }
        public string CL_COLOR { get; set; }
    }
}
