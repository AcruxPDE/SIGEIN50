using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    [Serializable]
    public class E_BAJAS_PENDIENTES
    {
        public int ID_BAJA_EMPLEADO { get; set; }
        public int ID_EMPLEADO { get; set; }
        public string CL_EMPLEADO { get; set; }
        public string NB_EMPLEADO { get; set; }
        public int ID_PUESTO { get; set; }
        public string CL_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
        public Nullable<int> ID_CAUSA_ROTACION { get; set; }
        public string DS_COMENTARIOS { get; set; }
        public System.DateTime FE_BAJA_EFECTIVA { get; set; }
        public System.DateTime FE_CREACION { get; set; }
        public string CL_USUARIO_APP_CREA { get; set; }
        public Nullable<int> ID_PLAZA { get; set; }
    }
}
