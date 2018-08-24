using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.IntegracionDePersonal
{
    [Serializable]
    public class E_PREGUNTA
    {
        public int ID_PRUEBA { get; set; }
        public int ID_CUESTIONARIO_PREGUNTA { get; set; }
        public int ID_CUESTIONARIO { get; set; }
        public int ID_PREGUNTA { get; set; }
        public string CL_PREGUNTA { get; set; }
        public string NB_PREGUNTA { get; set; }
        public string DS_PREGUNTA { get; set; }
        public string CL_TIPO_PREGUNTA { get; set; }
        public decimal NO_VALOR { get; set; }
        public Nullable<decimal> NO_VALOR_RESPUESTA { get; set; }
        public string NB_RESPUESTA { get; set; }
        public bool FG_REQUERIDO { get; set; }
        public bool FG_ACTIVO { get; set; }
        public Nullable<int> ID_COMPETENCIA { get; set; }
        public Nullable<int> ID_BITACORA { get; set; }
        public string DS_FILTRO { get; set; }
        public int ID_VARIABLE { get; set; }

    }
}
