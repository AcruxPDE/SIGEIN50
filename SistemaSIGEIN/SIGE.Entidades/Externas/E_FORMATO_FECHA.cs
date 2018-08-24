using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_FORMATO_FECHA
    {

        public int ID_FECHA { get; set; }
        public string NB_FECHA { get; set; }
        public string CL_FECHA { get; set; }
        public string FORMATO_FECHA { set; get; }
    }
}
