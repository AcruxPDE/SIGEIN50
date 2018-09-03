using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.IntegracionDePersonal
{
    [Serializable]
    public class E_PUESTO_VS_CANDIDATOS
    {
        public Guid vIdPuestoVsCandidatos { get; set; }
        public List<int> vListaCandidatos { get; set; }

        public E_PUESTO_VS_CANDIDATOS()
        {
            vListaCandidatos = new List<int>();
        }
    }
}
