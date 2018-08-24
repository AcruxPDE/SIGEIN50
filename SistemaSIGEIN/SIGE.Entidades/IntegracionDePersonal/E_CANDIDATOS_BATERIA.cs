using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.IntegracionDePersonal
{
    [Serializable]
    public class E_CANDIDATOS_BATERIA
    {
        public Guid vIdGeneraBaterias { get; set; }
        public List<int> vListaCandidatos { get; set; }

        public E_CANDIDATOS_BATERIA()
        {
            vListaCandidatos = new List<int>();
        }
    }
}
