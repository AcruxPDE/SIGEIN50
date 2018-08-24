using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    public class E_PRUEBA_CANDIDATO
    {
        public int ID_PRUEBA_PLANTILLA { get; set; }
        public string CL_PRUEBA { get; set; }
        public string CL_ESTADO { get; set; }
        public int? ID_PUESTO { get; set; }
        public int? NIVEL_COMPETENCIA_PUESTO { get; set; }
        public bool? FG_ASIGNADA { get; set; }
    }   
}
