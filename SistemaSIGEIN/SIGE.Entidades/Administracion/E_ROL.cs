using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    public class E_ROL
    {
        public string CL_ROL { get; set; }
        public Nullable<System.DateTime> FE_INACTIVO { get; set; }
        public bool FG_ACTIVO { get; set; }
        public int? ID_ROL { get; set; }
        public string NB_ROL { get; set; }
        public string XML_AUTORIZACION { get; set; }
        public List<E_FUNCION> LST_FUNCIONES { get; set; }
    }
}
