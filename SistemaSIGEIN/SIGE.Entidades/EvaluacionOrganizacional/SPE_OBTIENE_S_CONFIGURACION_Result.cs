using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    [Serializable]
    public partial class SPE_OBTIENE_S_CONFIGURACION
    {
        public string XML_CONFIGURACION { get; set; }
        public byte[] FI_LOGOTIPO { get; set; }
        public string CL_LICENCIAMIENTO { get; set; }
        public string OBJ_ADICIONAL { get; set; }
        public string CL_PASS_WS { get; set; }
        public string FE_CREACION { get; set; }
        public string CL_CLIENTE { get; set; }
        public string CL_EMPRESA { get; set; }
    }
}
