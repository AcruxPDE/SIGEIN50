using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_ENTREVISTA
    {
        public int ID_ENTREVISTA { get; set; }
        public int ID_PROCESO_SELECCION { get; set; }
        public int ID_ENTREVISTA_TIPO { get; set; }
        public System.DateTime FE_ENTREVISTA { get; set; }
        public int ID_ENTREVISTADOR { get; set; }
        public string NB_ENTREVISTADOR { get; set; }
        public string NB_PUESTO_ENTREVISTADOR { get; set; }
        public string CL_CORREO_ENTREVISTADOR { get; set; }
        public string DS_OBSERVACIONES { get; set; }
        public string NB_ENTREVISTA_TIPO { get; set; }
        public string CL_TOKEN { get; set; }
        public Nullable<System.Guid> FL_ENTREVISTA { get; set; }
    }
}
