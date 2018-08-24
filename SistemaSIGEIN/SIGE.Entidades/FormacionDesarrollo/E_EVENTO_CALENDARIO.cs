using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_EVENTO_CALENDARIO
    {
        public int ID_EVENTO_CALENDARIO { get; set; }
        public int ID_EVENTO { get; set; }
        public System.DateTime FE_INICIAL { get; set; }
        public System.DateTime FE_FINAL { get; set; }
        public byte NO_HORAS { get; set; }
    }
}
