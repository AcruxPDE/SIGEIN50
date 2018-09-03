using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.SecretariaTrabajoPrevisionSocial
{
    [Serializable]
    public class E_CURSO_AREA_TEMATICA
    {
        public Guid ID_ITEM { get; set; }
        public int ID_AREA_TEMATICA_CURSO { get; set; }
        public int ID_AREA_TEMATICA { get; set; }
        public string CL_AREA_TEMATICA { get; set; }
        public string NB_AREA_TEMATICA{ get; set; }

        public E_CURSO_AREA_TEMATICA()
        {
            ID_ITEM = Guid.NewGuid();
        }
    }
}
