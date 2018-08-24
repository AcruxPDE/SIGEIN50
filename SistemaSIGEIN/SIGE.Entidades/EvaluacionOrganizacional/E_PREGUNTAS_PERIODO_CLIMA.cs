using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
     [Serializable]
    public class E_PREGUNTAS_PERIODO_CLIMA
    {
        public int ID_PREGUNTA { set; get; }
        public int? ID_PREGUNTA_VERIFICACION { set; get; }
		public string NB_DIMENSION {set; get;}
		public string NB_TEMA {set; get;}
		public int NO_SECUENCIA {set; get;}
		public string NB_PREGUNTA {set; get;}
		public string NB_PREGUNTA_VERIFICACION {set; get;}
		public int? NO_SECUENCIA_VERIFICACION {set; get;}
        public bool FG_HABILITA_VERIFICACION { set; get; }
        public string CL_TIPO { set; get; }
        public Guid ID_RELACION_PREGUNTA { get; set; }
       
    }
}
