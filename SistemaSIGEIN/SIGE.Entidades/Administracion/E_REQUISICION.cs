using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_REQUISICION
    {
        public int ID_REQUISICION { get; set; }
        public string NO_REQUISICION { get; set; }
        public System.DateTime? FE_SOLICITUD { get; set; }
        public int ID_PUESTO { get; set; }
        public string CL_ESTATUS_REQUISICION { get; set; }
        public string CL_CAUSA { get; set; }
        public string DS_CAUSA { get; set; }
        public string DS_TIEMPO_CAUSA { get; set; }
        //public Nullable<int> ID_NOTIFICACION { get; set; }
        public Nullable<int> ID_SOLICITANTE { get; set; }
        public string NB_EMPLEADO_SOLICITANTE { get; set; }
        public string NB_PUESTO_SOLICITANTE { get; set; }
        public string NB_CORREO_SOLICITANTE { get; set; }
        public Nullable<int> ID_AUTORIZA { get; set; }
        public string NB_EMPLEADO_AUTORIZA { get; set; }
        public string NB_PUESTO_AUTORIZA { get; set; }
        public string NB_CORREO_AUTORIZA { get; set; }
        public Nullable<int> ID_EMPLEADO_AUTORIZA_PUESTO { get; set; }
        public string NB_EMPLEADO_AUTORIZA_PUESTO { get; set; }
        public string NB_EMPLEADO_AUTORIZA_PUESTO_PUESTO { get; set; }
        public string NB_CORREO_AUTORIZA_PUESTO { get; set; }
        //public Nullable<int> ID_VISTO_BUENO { get; set; }
        public Nullable<int> ID_EMPRESA { get; set; }
        public string CL_EMPRESA { get; set; }
        public string NB_EMPRESA { get; set; }
        //public string NB_RAZON_SOCIAL { get; set; }
        public Nullable<System.DateTime> FE_REQUERIMIENTO { get; set; }
        public string CL_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
        //public string DS_FILTRO { get; set; }
        public string CL_TOKEN { get; set; }
        public System.Guid FL_REQUISICION { get; set; }
        public int ID_EMPLEADO_SUPLENTE { get; set; }
        public string NB_EMPLEADO_SUPLENTE { get; set; }
        public decimal MN_SUELDO { get; set; }
        public decimal MN_SUELDO_TABULADOR { get; set; }
        public decimal MN_SUELDO_SUGERIDO { get; set; }
        public decimal MAX_SUELDO_SUGERIDO { get; set; }
        public string CL_TOKEN_PUESTO  { get; set; }
        public Nullable<System.Guid> FL_NOTIFICACION { get; set; }
        public string DS_COMENTARIOS { get; set; }
        public string DS_OBSERVACIONES_REQUISICION { get; set; }
        public string DS_OBSERVACIONES_PUESTO { get; set; }

    }
}
