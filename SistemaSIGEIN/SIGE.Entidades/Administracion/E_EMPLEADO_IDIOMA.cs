using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    public class E_EMPLEADO_IDIOMA
    {
        public int? ID_EMPLEADO { get; set; }
        public int? ID_EMPLEADO_IDIOMA { get; set; }
        public int? ID_IDIOMA { get; set; }
        public decimal? PR_CONVERSACIONAL { get; set; }
        public decimal? PR_ESCRITURA { get; set; }
        public decimal? PR_LECTURA { get; set; } 
    }
}
