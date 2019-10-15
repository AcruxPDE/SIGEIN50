using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.PuntoDeEncuentro
{
    [Serializable]
    public class E_OBTIENE_MIS_COMPROMISOS
    {
        public Guid ID_COMPROMISO { get; set; }
		public string CL_COMPROMISO { get; set; }
		public string NB_COMPROMISO { get; set; }
        public string SOLICITADO_POR { get; set; }
        public DateTime FE_ENTREGA { get; set; }
        public string NB_ESTATUS_COMPROMISO { get; set; }
        public string NB_PRIORIDAD { get; set; }
		
    }
}
