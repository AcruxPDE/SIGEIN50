using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{  [Serializable]
   public class E_EMPLEADO_COMPETENCIA
    {//EMPLEADO_COMPETENCIA
        public int ID_EMPLEADO_COMPETENCIA { get; set; }
        public int ID_EMPLEADO { get; set; }
        public int ID_COMPETENCIA { get; set; }
        public decimal NO_CALIFICACION { get; set; }
        public string DS_FILTRO { get; set; }

     //C_COMPETENCIA
        public string CL_COMPETENCIA { get; set; }
        public string NB_COMPETENCIA { get; set; }
        public string DS_COMPETENCIA { get; set; }
        public string CL_TIPO_COMPETENCIA { get; set; }
        public string CL_CLASIFICACION { get; set; }
       
    }


}
