using SIGE.Entidades.MetodologiaCompensacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Comunes;

namespace SIGE.WebApp.MPC
{
    public class ContextoTabuladores
    {
        public static List<E_REPORTE_TABULADOR_SUELDOS> oLstEmpleadoTabulador
        {
            get
            {
                return Utileria.GetSessionValue<List<E_REPORTE_TABULADOR_SUELDOS>>("__oLstTabuladorEmpleado__");
            }
            set
            {
                Utileria.SetSessionValue<List<E_REPORTE_TABULADOR_SUELDOS>>("__oLstTabuladorEmpleado__", value);
            }
        }


        public static List<E_REPORTE_TABULADOR_SUELDOS> oLstEmpleadoAnalisis
        {
            get
            {
                return Utileria.GetSessionValue<List<E_REPORTE_TABULADOR_SUELDOS>>("__oLstEmpleadoAnalisis__");
            }
            set
            {
                Utileria.SetSessionValue<List<E_REPORTE_TABULADOR_SUELDOS>>("__oLstEmpleadoAnalisis__", value);
            }
        }

        public static List<E_REPORTE_TABULADOR_SUELDOS> oLstTabuladorAnalisis
        {
            get
            {
                return Utileria.GetSessionValue<List<E_REPORTE_TABULADOR_SUELDOS>>("__oLstTabuladorAnalisis__");
            }
            set
            {
                Utileria.SetSessionValue<List<E_REPORTE_TABULADOR_SUELDOS>>("__oLstTabuladorAnalisis__", value);
            }
        }

        public static List<E_REPORTE_TABULADOR_SUELDOS> oLstEmpleadosDesviaciones
        {
            get
            {
                return Utileria.GetSessionValue<List<E_REPORTE_TABULADOR_SUELDOS>>("__oLstEmpleadosDesviaciones__");
            }
            set
            {
                Utileria.SetSessionValue<List<E_REPORTE_TABULADOR_SUELDOS>>("__oLstEmpleadosDesviaciones__", value);
            }
        }
    }
}