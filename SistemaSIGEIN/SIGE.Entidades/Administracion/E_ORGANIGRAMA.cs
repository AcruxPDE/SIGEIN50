using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    public class E_ORGANIGRAMA
    {
        public List<E_ORGANIGRAMA_GRUPO> lstGrupo { get; set; }
        public List<E_ORGANIGRAMA_NODO> lstNodoDescendencia { get; set; }
        public List<E_ORGANIGRAMA_NODO> lstNodoAscendencia { get; set; }
    }

    public class E_ORGANIGRAMA_NODO
    {
        public int idNodo { get; set; }
        public int? idNodoSuperior { get; set; }
        public string clNodo { get; set; }
        public string nbNodo { get; set; }
        public string clTipoNodo { get; set; }
        public string clTipoGenealogia { get; set; }
        public int noNivel { get; set; }
        public int noNivelPuesto { get; set; }
        public string cssNodo { get; set; }
    }

    public class E_ORGANIGRAMA_GRUPO
    {
        public int? idNodo { get; set; }
        public int? idItem { get; set; }
        public string clItem { get; set; }
        public string nbItem { get; set; }
        public string cssItem { get; set; }
    }
}
