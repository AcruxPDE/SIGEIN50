using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_RESULTADO_MEDICO
    {
        public int ID_RESULTADO_MEDICO { get; set; }
        public Nullable<int> ID_CANDIDATO { get; set; }
        public Nullable<int> ID_EMPLEADO { get; set; }
        public int ID_PROCESO_SELECCION { get; set; }
        public int NO_EDAD { get; set; }
        public string NO_TALLA { get; set; }
        public Nullable<decimal> NO_PESO { get; set; }
        public Nullable<decimal> NO_INDICE_MASA_CORPORAL { get; set; }
        public Nullable<int> NO_PULSO { get; set; }
        public string NO_PRESION_ARTERIAL { get; set; }
        public Nullable<int> NO_EMBARAZOS { get; set; }
        public Nullable<int> NO_HIJOS { get; set; }
        public string XML_ENFERMEDADES { get; set; }
        public string XML_MEDICAMENTOS { get; set; }
        public string XML_ALERGIAS { get; set; }
        public string XML_ANTECEDENTES { get; set; }
        public string XML_INTERVENCIONES_QUIRURJICAS { get; set; }
        public string DS_OBSERVACIONES { get; set; }
        public Nullable<bool> FG_ADECUADO { get; set; }
    }
}
