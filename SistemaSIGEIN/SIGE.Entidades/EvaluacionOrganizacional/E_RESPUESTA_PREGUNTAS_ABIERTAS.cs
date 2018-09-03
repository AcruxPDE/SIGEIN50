using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    [Serializable]
    public class E_RESPUESTA_PREGUNTAS_ABIERTAS
    {
        public int? ID_PREGUNTA_RESPUESTA { get; set; }
        public string NB_RESPUESTA { get; set; }
    }
}
