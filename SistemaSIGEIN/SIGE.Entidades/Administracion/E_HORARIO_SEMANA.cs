using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_HORARIO_SEMANA
    {
        public System.Guid ID_HORARIO_SEMANA { get; set; }
        public string CL_CLIENTE { get; set; }
        public string CL_HORARIO_SEMANA { get; set; }
        public string NB_HORARIO_SEMANA { get; set; }
        public string CL_HORARIO_DOMINGO { get; set; }
        public string CL_HORARIO_LUNES { get; set; }
        public string CL_HORARIO_MARTES { get; set; }
        public string CL_HORARIO_MIERCOLES { get; set; }
        public string CL_HORARIO_JUEVES { get; set; }
        public string CL_HORARIO_VIERNES { get; set; }
        public string CL_HORARIO_SABADO { get; set; }
        public string DS_DESCRIPCION_GRAL { get; set; }
        public string CL_GRAL { get; set; }
    }
}
