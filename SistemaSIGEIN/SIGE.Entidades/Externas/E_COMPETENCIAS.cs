using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_COMPETENCIAS
    {
        public int? ID_COMPETENCIA { get; set; }
        public string CL_COMPETENCIA { get; set; }
        public string NB_COMPETENCIA { get; set; }        
        public string DS_COMPETENCIA { get; set; }
        public int ID_PUESTO_FUNCION { get; set; }
        public int ID_PUESTO_COMPETENCIA { get; set; }
        public string CL_TIPO_COMPETENCIA { get; set; }
        public string CL_PUESTO_TIPO_COMPETENCIA { get; set; }
        public string NB_TIPO_COMPETENCIA { get; set; }
        public string CL_CLASIFICACION { get; set; }
        public string NB_CLASIFICACION { get; set; }
        public string DS_CLASIFICACION { get; set; }
        public string CL_CLASIFICACION_COLOR { get; set; }
        public int ID_NIVEL_DESEADO { get; set; }
        public int NO_VALOR_NIVEL { get; set; }
        public int ID_NIVEL0 { get; set; }
        public string DS_COMENTARIOS_NIVEL0 { get; set; }
        public int ID_NIVEL1 { get; set; }
        public string DS_COMENTARIOS_NIVEL1 { get; set; }
        public int ID_NIVEL2 { get; set; }
        public string DS_COMENTARIOS_NIVEL2 { get; set; }
        public int ID_NIVEL3 { get; set; }
        public string DS_COMENTARIOS_NIVEL3 { get; set; }
        public int ID_NIVEL4 { get; set; }
        public string DS_COMENTARIOS_NIVEL4 { get; set; }
        public int ID_NIVEL5 { get; set; }
        public string DS_COMENTARIOS_NIVEL5 { get; set; }
    }
}
