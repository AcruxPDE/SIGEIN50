using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    [Serializable]
    public class E_BAJA_IMPORTANTE_EO
    {
        public int ID_CONFIGURACION_NOTIFICADO { get; set; }
        public int ID_EMPLEADO { get; set; }
        public string CL_EMPLEADO { get; set; }
        public string NB_EMPLEADO_COMPLETO { get; set; }
        public string NB_PUESTO { get; set; }
        public string NB_DEPARTAMENTO { get; set; }
        public string NB_EMPRESA { get; set; }
        public string CL_ESTATUS { get; set; }
        public string CL_CORREO_ELECTRONICO { get; set; }
        public string CL_TIPO_NOTIFICACION { get; set; }
      

    }
}
