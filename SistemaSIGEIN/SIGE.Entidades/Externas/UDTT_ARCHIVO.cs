using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    public class UDTT_ARCHIVO
    {
        public Guid ID_ITEM { get; set; }
        public int? ID_ARCHIVO { get; set; }
        public string NB_ARCHIVO { get; set; }
        public byte[] FI_ARCHIVO { get; set; }
        public string  CL_PROCEDENCIA { get; set; }

    }
}
