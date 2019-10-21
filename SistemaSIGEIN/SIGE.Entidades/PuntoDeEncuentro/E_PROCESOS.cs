using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.PuntoDeEncuentro
{
    [Serializable]
    public class E_PROCESOS
    {
        public Guid ID_USUARIO_FUNCION { set; get; }
        public string CL_USUARIO_PROCESO { set; get; }
        public bool FG_COMUNICADOS { set; get; }
        public bool FG_TRAMITES { set; get; }
        public bool FG_COMPROMISOS { set; get; }
        public bool FG_NOMINA { set; get; }


    }
}
