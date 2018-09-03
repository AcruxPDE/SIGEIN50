using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.SecretariaTrabajoPrevisionSocial
{
    [Serializable]
    public class E_OCUPACION_PUESTO
    {
        public Guid ID_ITEM { get; set; }
        public int ID_OCUPACION_PUESTO { get; set; }
        public int ID_PUESTO { get; set; }
        public int ID_OCUPACION { get; set; }
        public string CL_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
        public string CL_OCUPACION { get; set; }
        public string NB_OCUPACION { get; set; }
        public string CL_MODULO { get; set; }
        public string NB_MODULO { get; set; }
        public string CL_SUBAREA { get; set; }
        public string NB_SUBAREA { get; set; }
        public string CL_AREA { get; set; }
        public string NB_AREA { get; set; }
             
        public E_OCUPACION_PUESTO()
        {
            ID_ITEM = Guid.NewGuid();
        }
    }
}
