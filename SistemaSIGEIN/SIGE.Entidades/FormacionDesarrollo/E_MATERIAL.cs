using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_MATERIAL
    {
        public Guid ID_ITEM { get; set; }
        public int ID_TEMA { get; set; }
        public int CL_MATERIAL { get; set; }
        public string NB_MATERIAL { get; set; }
        public string MN_MATERIAL { get; set; }

        public E_MATERIAL()
        {
            ID_ITEM = Guid.NewGuid();
        }
    }
}
