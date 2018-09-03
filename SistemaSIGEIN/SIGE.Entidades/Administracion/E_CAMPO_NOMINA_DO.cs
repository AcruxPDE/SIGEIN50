using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_CAMPO_NOMINA_DO
    {
        public int ID_CAMPO { get; set; }
        public string CL_CAMPO { get; set; }
        public bool FG_EDITABLE_NOMINA { get; set; }
        public bool FG_EDITABLE_DO { get; set; }
        public Nullable<System.DateTime> FE_MODIFICACION { get; set; }
        public string CL_ULTIMO_USUARIO_MODIFICA { get; set; }
    }
}
