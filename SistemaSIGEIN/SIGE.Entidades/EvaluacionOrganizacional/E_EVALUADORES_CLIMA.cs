using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    [Serializable]
   public  class E_EVALUADORES_CLIMA
    {
        public int? ID_EVALUADOR { set; get; }
        public int? ID_PERIODO {set; get;}
	    public int? ID_PUESTO {set; get;}
		public string NB_PUESTO {set; get;}
		public int? ID_EMPLEADO {set; get;}
		public string NB_EVALUADOR {set; get;}
		public string CL_CORREO_ELECTRONICO {set; get;}
		public string CL_TIPO_EVALUADOR {set; get;}
        public string CL_EMPLEADO { set; get; }
        public string CL_TOKEN { set; get; }
        public string NB_DEPARTAMENTO { set; get; }
        public string NB_CONTESTADO { set; get; }
        public bool FG_CONTESTADO { set; get; }
    }
}
