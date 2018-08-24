using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_TEMA
    {
        public Guid ID_ITEM { get; set; }
        public int ID_TEMA { get; set; }
        public string CL_TEMA { get; set; }
        public string NB_TEMA { get; set; }
        public string NO_DURACION { get; set; }
        public string DS_DESCRIPCION { get; set; }
        public string LSTCOMPETENCIA { get; set; }
        public string LSTMATERIAL { get; set; }

        public List<E_TEMA_COMPETENCIA> LS_COMPETENCIAS { get; set; }
        public List<E_MATERIAL> LS_MATERIALES { get; set; }

        public E_TEMA()
        {
            ID_ITEM = Guid.NewGuid();
            LS_COMPETENCIAS = new List<E_TEMA_COMPETENCIA>();
            LS_MATERIALES = new List<E_MATERIAL>();
        }
    }
}
