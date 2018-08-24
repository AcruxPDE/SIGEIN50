using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_REPORTE_MAXIMO_MINIMO
    {
        public Guid ID_REPORTE { get; set; }
        public int ID_PUESTO_OBJETIVO { get; set; }
        public int NO_DIAS_CURSO { get; set; }
        public int NO_ROTACION_PROMEDIO { get; set; }
        public int NO_PORCENTAJE_NO_APROBADOS { get; set; }
        public int NO_PUNTO_REORDEN { get; set; }

        public int ID_EVENTO { get; set; }
        public Nullable<int> ID_CURSO { get; set; }
        public string NB_EVENTO { get; set; }
        public System.DateTime FE_INICIO { get; set; }
        public System.DateTime FE_TERMINO { get; set; }
        public string NB_CURSO { get; set; }
        public Nullable<int> NO_PERSONAL_ENTRENAMIENTO { get; set; }
        public string DS_COMPETENCIAS { get; set; }
    }
}
