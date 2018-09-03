using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_PROGRAMA_COMPETENCIA
    {
        public int ID_PROGRAMA_COMPETENCIA { get; set; }
        public int ID_PROGRAMA { get; set; }
        public int ID_COMPETENCIA { get; set; }
        public int NO_ORDEN { get; set; }
        public string NB_COMPETENCIA { get; set; }
        public string CL_COMPETENCIA { get; set; }
        public string NB_CLASIFICACION { get; set; }
        public string NB_CATEGORIA { get; set; }
        public string CL_PROGRAMA { get; set; }
        public string NB_PROGRAMA { get; set; }
        public string NB_CLASIFICACION_COMPETENCIA { get; set; }
        public string NB_TIPO_COMPETENCIA { get; set; }
    }
}
