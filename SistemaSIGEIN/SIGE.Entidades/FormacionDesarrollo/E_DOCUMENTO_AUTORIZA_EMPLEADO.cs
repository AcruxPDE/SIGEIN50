using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_DOCUMENTO_AUTORIZA_EMPLEADO
    {
        public int ID_AUTORIZACION { get; set; }
        public System.Guid FL_AUTORIZACION { get; set; }
        public string CL_TOKEN { get; set; }
        public int ID_EMPLEADO { get; set; }
        public string NB_EMPLEADO { get; set; }
        public int ID_DOCUMENTO { get; set; }
        public string CL_ESTADO { get; set; }
        public string DS_OBSERVACIONES { get; set; }
        public Nullable<System.DateTime> FE_AUTORIZACION { get; set; }
        public string CL_EMPLEADO { get; set; }
        public string NB_EMPLEADO_PUESTO { get; set; }
        public int ID_PUESTO { get; set; }
        public string CL_CORREO_ELECTRONICO { get; set; }
        public string CL_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
        public string CL_DOCUMENTO { get; set; }
        public string NB_DOCUMENTO { get; set; }
        public string CL_TIPO_DOCUMENTO { get; set; }
        public string DS_NOTAS { get; set; }
        public string VERSION { get; set; }
        public Nullable<System.DateTime> FE_ELABORACION { get; set; }
        public Nullable<System.DateTime> FE_REVISION { get; set; }
        public string NB_EMPLEADO_ELABORA { get; set; }
        public int ID_PROGRAMA { get; set; }
        public string CL_PROGRAMA { get; set; }
        public string NB_PROGRAMA { get; set; }
    }
}
