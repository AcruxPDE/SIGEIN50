using SIGE.Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Comunes;

namespace SIGE.WebApp.Comunes
{
    public class ContextoUsuario
    {
        public static E_USUARIO oUsuario
        {
            get
            {
                return Utileria.GetSessionValue<E_USUARIO>("__oUsuario__");
            }
            set
            {
                Utileria.SetSessionValue<E_USUARIO>("__oUsuario__", value);
            }
        }

        public static string nbHost
        {
            get {
                return string.Format("{0}://{1}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority);
            }
        }

        public static string nbPrograma
        {
            get
            {
                return HttpContext.Current.Request.Url.AbsolutePath;
            }
        }

        public static int idBateriaPruebas
        {
            get
            {
                return Utileria.GetSessionValue<int>("__idBateriaPrueba__");
            }
            set
            {
                Utileria.SetSessionValue<int>("__idBateriaPrueba__", value);
            }
        }

        public static Guid clTokenPruebas
        {
            get
            {
                return Utileria.GetSessionValue<Guid>("__clTokenPrueba__");
            }
            set
            {
                Utileria.SetSessionValue<Guid>("__clTokenPrueba__", value);
            }
        }

        public static string clEstadoPruebas
        {
            get
            {
                return Utileria.GetSessionValue<string>("__clEstadoPruebas__");
            }
            set
            {
                Utileria.SetSessionValue<string>("__clEstadoPruebas__", value);
            }
        }
    }
}