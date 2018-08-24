using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]    
    public class E_SELECTOR_CURSO
    {
        public int idCurso { get; set; }
        public string clCurso { get; set; }
        public string nbCurso { get; set; }
        public decimal noDuracion { get; set; }
        public string clTipoCatalogo { get; set; }
    }

    [Serializable]
    public class E_SELECTOR_PROGRAMA
    {
        public int idPrograma { get; set; }
        public string clTipoCatalogo { get; set; }
       
    }
}
