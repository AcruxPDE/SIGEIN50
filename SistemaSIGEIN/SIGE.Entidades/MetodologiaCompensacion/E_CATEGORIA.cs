using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.MetodologiaCompensacion
{
     [Serializable]
    public class E_CATEGORIA
    {
        public int ID_TABULADOR_NIVEL { set; get; }
        public int? ID_TABULADOR_EMPLEADO { set; get; }
        public int NO_NIVEL { set; get; }
        public int? NO_CATEGORIA { set; get; }
        public decimal? MN_MINIMO { set; get; }
        public decimal? MN_PRIMER_CUARTIL { set; get; }
        public decimal? MN_MEDIO { set; get; }
        public decimal? MN_SEGUNDO_CUARTIL { set; get; }
        public decimal? MN_MAXIMO { set; get; }
        public decimal? CANTIDAD { set; get; }
    }
}
