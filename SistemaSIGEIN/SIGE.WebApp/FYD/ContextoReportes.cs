using SIGE.Entidades.FormacionDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Comunes;

namespace SIGE.WebApp.FYD
{
    public class ContextoReportes
    {
        public static List<E_REPORTE_GLOBAL> oReporteGlobal
        {
            get { return Utileria.GetSessionValue<List<E_REPORTE_GLOBAL>>("__oReporteGlobal__"); }
            set { Utileria.SetSessionValue<List<E_REPORTE_GLOBAL>>("__oReporteGlobal__", value); }
        }

        public static List<E_REPORTE_COMPARATIVO> oReporteComparativo
        {
            get { return Utileria.GetSessionValue <List<E_REPORTE_COMPARATIVO >> ("__oReporteComparativo__"); }
            set { Utileria.SetSessionValue<List<E_REPORTE_COMPARATIVO>>("__oReporteComparativo__", value); }
        }

        public static List<E_REPORTE_INDIVIDUAL> oReporteIndividual {
            get { return Utileria.GetSessionValue<List<E_REPORTE_INDIVIDUAL>>("__oReporteIndividual__"); }
            set { Utileria.SetSessionValue<List<E_REPORTE_INDIVIDUAL>>("__oReporteIndividual__", value); }
        }

        public static List<E_REPORTE_FYD> oReporteFyd {
            get { return Utileria.GetSessionValue<List<E_REPORTE_FYD>>("__oReporteFyd__"); }
            set { Utileria.SetSessionValue<List<E_REPORTE_FYD>>("__oReporteFyd__", value); }
        }

        public static List<E_REPORTE_MAXIMO_MINIMO> oReporteMaximoMinimo {
            get { return Utileria.GetSessionValue<List<E_REPORTE_MAXIMO_MINIMO>>("__oReporteMaximoMinimo__"); }
            set { Utileria.SetSessionValue<List<E_REPORTE_MAXIMO_MINIMO>>("__oReporteMaximoMinimo__", value); }
        }
    }
}