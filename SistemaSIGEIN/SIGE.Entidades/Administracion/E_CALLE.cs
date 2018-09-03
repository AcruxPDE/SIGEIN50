using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public partial class E_CALLE
    {
        public int ID_CALLE { get; set; }
        public string CL_PAIS { get; set; }
        public string CL_ESTADO { get; set; }
        public string CL_MUNICIPIO { get; set; }
        public string CL_COLONIA { get; set; }
        public string CL_CALLE { get; set; }
        public string NB_CALLE { get; set; }
        
    }
}
