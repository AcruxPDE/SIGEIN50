using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    [Serializable]
    public class E_PREGUNTAS_CUESTIONARIO_CLIMA
    {

       public int ID_CUESTIONARIO_PREGUNTA {set;get;}
	   public int ID_CUESTIONARIO {set; get;}
	   public string NB_PREGUNTA {set; get;}
	   public int? ID_EVALUADOR {set; get;}
       public bool FG_CONTESTADO { set; get;}
       public int NO_SECUENCIA { set; get; }
       public decimal? NO_VALOR_RESPUESTA { set; get; }
        public bool FG_VALOR1 { get; set; }
        public bool FG_VALOR2 { get; set; }
        public bool FG_VALOR3 { get; set; }
        public bool FG_VALOR4 { get; set; }

    }
}


