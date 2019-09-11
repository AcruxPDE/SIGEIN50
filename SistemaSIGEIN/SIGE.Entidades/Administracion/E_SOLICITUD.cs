using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public partial class E_SOLICITUD
    {
        public int ID_SOLICITUD { get; set; }
        public string NB_CANDIDATO { get; set; }
        public string NB_APELLIDO_PATERNO { get; set; }
        public string NB_APELLIDO_MATERNO { get; set; }
    }
}
