using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_DOCUMENTO_AUTORIZAR
    {
        public int ID_DOCUMENTO_AUTORIZACION { get; set; }
        public string CL_DOCUMENTO { get; set; }
        public string NB_DOCUMENTO { get; set; }
        public string CL_TIPO_DOCUMENTO { get; set; }
        public string DS_NOTAS { get; set; }
        public string VERSION { get; set; }
        public Nullable<System.DateTime> FE_ELABORACION { get; set; }
        public Nullable<System.DateTime> FE_REVISION { get; set; }
        public string NB_EMPLEADO_ELABORA { get; set; }
    }
}
