using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.IntegracionDePersonal
{
        [Serializable]
        public class E_CANDIDATO_VS_PUESTOS
        {
            public Guid vIdCandidatoVsPuestos { get; set; }
            public List<int> vListaPuestos { get; set; }

            public E_CANDIDATO_VS_PUESTOS()
            {
                vListaPuestos = new List<int>();
            }
        }
}
