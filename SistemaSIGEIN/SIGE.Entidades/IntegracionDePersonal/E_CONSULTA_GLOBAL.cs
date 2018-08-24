using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.IntegracionDePersonal
{
    [Serializable]
    public class E_CONSULTA_GLOBAL
    {
        public int ID_CONSULTA_GLOBAL { get; set; }
        public int ID_CANDIDATO { get; set; }
        public int ID_PUESTO { get; set; }
        public decimal PR_COMPATIBILIDAD { get; set; }
        public string DS_COMENTARIOS { get; set; }
        public int NO_FACTOR { get; set; }
        public string NB_FACTOR { get; set; }
        public decimal PR_FACTOR { get; set; }
        public int ID_CONSULTA_GLOBAL_CALIFICACION { get; set; }
        public decimal PR_CALIFICACION { get; set; }
        public decimal PR_VALOR { get; set; }
        public bool FG_ASOCIADO_INGLES { get; set; }
    }
}
