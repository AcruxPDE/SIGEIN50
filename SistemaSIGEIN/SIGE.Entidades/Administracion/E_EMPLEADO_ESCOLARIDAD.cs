using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public partial class E_EMPLEADO_ESCOLARIDAD
    {

        public int? ID_EMPLEADO_ESCOLARIDAD { get; set; }
        public int? ID_EMPLEADO { get; set; }
        public int? ID_CANDIDATO { get; set; }
        public int? ID_ESCOLARIDAD { get; set; }
        public int? CL_INSTITUCION { get; set; }
        public string NB_INSTITUCION { get; set; }
        public string FE_PERIODO_FIN { get; set; }
        public string FE_PERIODO_INICIO { get; set; }
        public string CL_ESTADO_ESCOLARIDAD { get; set; }

        //C_ESCOLARIDADES
        public string NB_ESCOLARIDAD { get; set; }
        public string DS_ESCOLARIDAD { get; set; }
        public int? ID_NIVEL_ESCOLARIDAD { get; set; }
           
    }
}
