using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public partial class E_REGISTRO_PATRONAL
    {
        public System.Guid ID_REGISTRO_PATRONAL { get; set; }
        public System.String CL_CLIENTE { get; set; }
        public System.Guid ID_RAZON_SOCIAL { get; set; }
        public string NB_RAZON_SOCIAL { get; set; }
        public string CL_REGISTRO_PATRONAL { get; set; }
        public string NO_REGISTRO_PATRONAL { get; set; }
        public bool FG_ACTIVO { get; set; }
        public string ACTIVO { get; set; }
        public string NB_DESCRIPCION { get; set; }
        public string CL_DELEGACION { get; set; }
    }
}
