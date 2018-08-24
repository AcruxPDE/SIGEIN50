using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_COMPETENCIA_NIVEL
    {
        public int ID_COMPETENCIA { get; set; }
        public string CL_COMPETENCIA { get; set; }
        public string NB_COMPETENCIA { get; set; }
        public string DS_COMPETENCIA { get; set; }
        public string CL_TIPO_COMPETENCIA { get; set; }
        public string CL_CLASIFICACION { get; set; }
        public bool FG_ACTIVO { get; set; }
        public string XML_CAMPOS_ADICIONALES { get; set; }
        public string DS_TIPO_COMPETENCIA { get; set; }
        public string NB_TIPO_COMPETENCIA { get; set; }
        public string NB_CLASIFICACION_COMPETENCIA { get; set; }
        public string DS_CLASIFICACION_COMPETENCIA { get; set; }
        public string DS_NIVEL_COMPETENCIA_PUESTO_N0 { get; set; }
        public string DS_NIVEL_COMPETENCIA_PERSONA_N0 { get; set; }
        public string DS_NIVEL_COMPETENCIA_PUESTO_N1 { get; set; }
        public string DS_NIVEL_COMPETENCIA_PERSONA_N1 { get; set; }
        public string DS_NIVEL_COMPETENCIA_PUESTO_N2 { get; set; }
        public string DS_NIVEL_COMPETENCIA_PERSONA_N2 { get; set; }
        public string DS_NIVEL_COMPETENCIA_PUESTO_N3 { get; set; }
        public string DS_NIVEL_COMPETENCIA_PERSONA_N3 { get; set; }
        public string DS_NIVEL_COMPETENCIA_PUESTO_N4 { get; set; }
        public string DS_NIVEL_COMPETENCIA_PERSONA_N4 { get; set; }
        public string DS_NIVEL_COMPETENCIA_PUESTO_N5 { get; set; }
        public string DS_NIVEL_COMPETENCIA_PERSONA_N5 { get; set; }
        public string DS_FILTRO { get; set; }
    }
}
