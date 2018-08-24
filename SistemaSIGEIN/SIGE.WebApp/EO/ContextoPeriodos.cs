using SIGE.Entidades.EvaluacionOrganizacional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Comunes;

namespace SIGE.WebApp.EO
{
    public class ContextoPeriodos
    {
        public static List<E_SELECCION_PERIODOS_DESEMPENO> oLstPeriodos
        {
            get
            {
                return Utileria.GetSessionValue<List<E_SELECCION_PERIODOS_DESEMPENO>>("__oLstPeriodos__");
            }
            set
            {
                Utileria.SetSessionValue<List<E_SELECCION_PERIODOS_DESEMPENO>>("__oLstPeriodos__", value);
            }
        }

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

        public static List<E_SELECCION_PERIODOS_DESEMPENO> oLstPeriodosPersonal
        {
            get
            {
                return Utileria.GetSessionValue<List<E_SELECCION_PERIODOS_DESEMPENO>>("__oLstPeriodosPersonal__");
            }
            set
            {
                Utileria.SetSessionValue<List<E_SELECCION_PERIODOS_DESEMPENO>>("__oLstPeriodosPersonal__", value);
            }
        }
    }
}