using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_SELECCION_PLAZA
    {
        public int idPlaza { get; set; }
        public int idPazaSuperior { get; set; }
        public string clPlaza { get; set; }
        public string clPlazaSuperior { get; set; }
        public string nbPlaza { get; set; }
        public string nbEmpleado { get; set; }
        public string nbEmpleadoSuperior { get; set; }
        public string nbPuesto { get; set; }
        public string nbPuestoSuperior { get; set; }
    }
}
