using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_PROMEDIO_PLAN_VIDA_CARRERA
    {
        public string NB_PUESTO { get; set; }        
        public decimal NO_ELEMENTOS { get; set; }
        public decimal SUM_ELEMENTOS { get; set; }
        public decimal PR_PUESTO { get; set; }


        public E_PROMEDIO_PLAN_VIDA_CARRERA()
        {
            NO_ELEMENTOS = 0;
            SUM_ELEMENTOS = 0;
            PR_PUESTO = 0;
        }

        public void CalcularPromedio()
        {
            if (NO_ELEMENTOS > 0)
            {
                PR_PUESTO = SUM_ELEMENTOS / NO_ELEMENTOS;
            }
        }

        public void SumarElemento(decimal pElemento)
        {
            SUM_ELEMENTOS = SUM_ELEMENTOS + pElemento;
            NO_ELEMENTOS++;
        }

    }
}
