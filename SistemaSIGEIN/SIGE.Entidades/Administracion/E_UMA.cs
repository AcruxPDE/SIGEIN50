using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_UMA
    {
        public System.Guid ID_UMA { get; set; }
        public System.DateTime FE_INICIAL { get; set; }
        public System.DateTime FE_FINAL { get; set; }
        public decimal MN_UMA { get; set; }
    }
}
