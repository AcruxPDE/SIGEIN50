using SIGE.Entidades.FormacionDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Comunes;

namespace SIGE.WebApp.FYD
{
    public class ContextoCurso
    {
        public static List<E_CURSO> oCursos
        {
            get
            {
                return Utileria.GetSessionValue<List<E_CURSO>>("__oCursos__");
            }
            set
            {
                Utileria.SetSessionValue<List<E_CURSO>>("__oCursos__", value);
            }
        }
    }
}