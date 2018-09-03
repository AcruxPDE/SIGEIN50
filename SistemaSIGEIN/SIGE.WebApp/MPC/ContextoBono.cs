using SIGE.Entidades.EvaluacionOrganizacional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Comunes;

namespace SIGE.WebApp.MPC
{
    public class ContextoBono
    {
        public static List<E_SELECCION_PERIODOS_DESEMPENO> oLstPeriodosBonos
        {
            get
            {
                return Utileria.GetSessionValue<List<E_SELECCION_PERIODOS_DESEMPENO>>("__oLstPeriodosBonos__");
            }
            set
            {
                Utileria.SetSessionValue<List<E_SELECCION_PERIODOS_DESEMPENO>>("__oLstPeriodosBonos__", value);
            }
        }
    }
}