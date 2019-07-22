using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_EMPLEADO_NOMINA_DO
    {
        public int ID_EMPLEADO_NOMINA_DO { get; set; }
        public int ID_EMPLEADO { get; set; }
        public string ID_PLANTILLA { get; set; }
        public string CL_EMPLEADO { get; set; }
        public string NB_EMPLEADO { get; set; }
        public string NB_APELLIDO_PATERNO { get; set; }
        public string NB_APELLIDO_MATERNO { get; set; } 
        public bool FG_NOMINA { get; set; }
        public bool FG_DO { get; set; }
        public bool FG_NOMINA_DO { get; set; }
        public int ID_PUESTO { get; set; }
        public int ID_PLAZA { get; set; }        
        public Nullable<bool> FG_SUELDO_NOMINA_DO { get; set; }
        public Nullable<System.DateTime> FE_BAJA_NOMINA { get; set; }
        public string DS_CAUSA_BAJA_NOMINA { get; set; }
        public Nullable<System.DateTime> FE_CREACION { get; set; }
        public Nullable<System.DateTime> FE_MODIFICACION { get; set; }
        public string CL_USUARIO_APP_CREA { get; set; }
        public string CL_USUARIO_APP_MODIFICA { get; set; }
        public string NB_PROGRAMA_CREA { get; set; }
        public string NB_PROGRAMA_MODIFICA { get; set; }
        public string NB_PUESTO { get; set; }
        public string NB_PLAZA { get; set; }

    }
}
