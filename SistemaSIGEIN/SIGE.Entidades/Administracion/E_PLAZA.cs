using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public partial class E_PLAZA 
    {
        public int ID_PLAZA { get; set; }
        public int ID_PLAZA_PDE { get; set; }
        public Nullable<int> ID_PLAZA_SUPERIOR { get; set; }
        public string CL_PLAZA { get; set; }
        public string NB_PLAZA { get; set; }
        public Nullable<int> ID_EMPRESA { get; set; }
        public string XML_GRUPOS { get; set; }
        public string XML_PLAZAS_INTERRELACIONADAS { get; set; }
        public Nullable<int> ID_DEPARTAMENTO { get; set; }
        public string NB_DEPARTAMENTO { get; set; }
        public Nullable<int> ID_PUESTO { get; set; }
        public string CL_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
        public Nullable<int> ID_EMPLEADO { get; set; }
        public string CL_EMPLEADO { get; set; }
        public string NB_EMPLEADO { get; set; }
        public string NB_APELLIDO_PATERNO { get; set; }
        public string NB_APELLIDO_MATERNO { get; set; }
        public string NB_EMPLEADO_COMPLETO { get; set; }
        public string CL_PLAZA_JEFE { get; set; }
        public string NB_PLAZA_JEFE { get; set; }
        public string CL_PUESTO_JEFE { get; set; }
        public string NB_PUESTO_JEFE { get; set; }
        public string CL_EMPLEADO_JEFE { get; set; }
        public string NB_EMPLEADO_JEFE { get; set; }
        public string NB_APELLIDO_PATERNO_JEFE { get; set; }
        public string NB_APELLIDO_MATERNO_JEFE { get; set; }
        public string NB_EMPLEADO_JEFE_COMPLETO { get; set; }
        public bool FG_ACTIVO { get; set; }
        public string CL_ACTIVO { get; set; }
        public Nullable<System.DateTime> FE_MODIFICACION { get; set; }
        public string CL_USUARIO_MODIFICA { get; set; }

       
    }
}
