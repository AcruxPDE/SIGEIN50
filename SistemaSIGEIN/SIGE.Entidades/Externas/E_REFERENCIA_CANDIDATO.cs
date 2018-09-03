using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_REFERENCIA_CANDIDATO
    {
        public int ID_EXPERIENCIA_LABORAL { get; set; }
        public string NB_REFERENCIA { get; set; }
        public string NB_PUESTO_REFERENCIA { get; set; }
        public bool FG_INFORMACION_CONFIRMADA { get; set; }
        public string DS_COMENTARIOS { get; set; }
    }
}
