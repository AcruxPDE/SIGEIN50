using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.TableroControl
{
    [Serializable]
    public class E_PERIODOS_EVALUADOS
    {
        public Nullable<long> NUMERO_PERIODO { get; set; }
        public int ID_PERIODO { get; set; }
        public string CL_PERIODO { get; set; }
        public int ID_PERIODO_REFERENCIA { get; set; }
        public string CL_TIPO_PERIODO_REFERENCIA { get; set; }
        public string NB_TIPO_PERIODO_REFERENCIA { get; set; }
        public string DS_PERIODO { get; set; }
        public Nullable<System.DateTime> FE_PERIODO { get; set; }
        public string CL_TABULADOR { get; set; }
        public Nullable<System.DateTime> FE_TABULADOR { get; set; }
        public Nullable<bool> FG_EVALUACION_IDP { get; set; }
        public Nullable<bool> FG_EVALUACION_FYD { get; set; }
        public Nullable<bool> FG_EVALUCION_ED { get; set; }
    }
}
