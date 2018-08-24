using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.MetodologiaCompensacion
{
    [Serializable]
   public class E_NIVELES
    {
        public int ID_TABULADOR_NIVEL { set; get; }
        public string CL_TABULADOR_NIVEL { set; get; }
		public string NB_TABULADOR_NIVEL { set; get; }
		public int NO_ORDEN { set; get; }
        public decimal PR_PROGRESION { set; get; }
        public int MAX_NIVEL { set; get; }
        public int? NO_NIVEL { set; get; }
        public decimal PR_VERDE { set; get; }
        public decimal PR_AMARILLO_NEG { set; get; }
        public decimal PR_AMARILLO_POS { set; get; }
        public decimal PR_ROJO_NEG { set; get; }
        public decimal PR_ROJO_POS { set; get; }

       
    }
}
