using SIGE.Entidades.FormacionDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Comunes;

namespace SIGE.WebApp.FYD
{
    public class ContextoInstructor
    {
        public static List<E_INSTRUCTOR> oInstructores
        {
            get
            {
                return Utileria.GetSessionValue<List<E_INSTRUCTOR>>("__oInstructores__");
            }
            set
            {
                Utileria.SetSessionValue<List<E_INSTRUCTOR>>("__oInstructores__", value);
            }
        }
    }
}