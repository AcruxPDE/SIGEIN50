using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_COMPARACION_COMPETENCIA
    {
        public int NO_ORDEN { get; set; }
        public int NO_ACIERTO { get; set; }
        public string CL_TIPO_REGISTRO { get; set; }
        public Nullable<int> ID_PUESTO { get; set; }
        public string CL_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
        public Nullable<int> ID_EMPLEADO { get; set; }
        public string CL_EMPLEADO { get; set; }
        public string NB_EMPLEADO { get; set; }
        public Nullable<int> ID_COMPETENCIA { get; set; }
        public string NB_COMPETENCIA { get; set; }
        public string DS_COMPETENCIA { get; set; }
        public Nullable<decimal> NO_VALOR_NIVEL { get; set; }
        public Nullable<decimal> NO_RESULTADO_PROMEDIO { get; set; }
        public string PR_COMPATIBILIDAD { get; set; }
        public Nullable<decimal> PR_NO_COMPATIBILIDAD { get; set; }
        public string CL_COLOR { get; set; }
        public string CL_COLOR_COMPATIBILIDAD { get; set; }
        public string NB_HTML_COMPETENCIA {
            get {                
                return "<div style=\"border: 2px solid " + CL_COLOR + "; height: 100%; width: 100%; padding: 5px 0px 5px 0px ;\">" + NB_COMPETENCIA + "</div>";
            }
        }

        public string DS_HTML_COMPETENCIA {
            get {
                return "<div style=\"border: 2px solid " + CL_COLOR + "; height: 100%; width: 100%; padding: 8px;\">" + DS_COMPETENCIA + "</div>";
            }
        }

    }
}
