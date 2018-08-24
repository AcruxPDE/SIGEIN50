using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Comunes;
using SIGE.Entidades.Administracion;

namespace SIGE.WebApp.Administracion
{
    public class ContextoConsultaGlobal 
    {
        public static List<E_PUESTOS_CONSULTA_GLOBAL> oPuestosConfiguracion
        {
            get { return Utileria.GetSessionValue<List<E_PUESTOS_CONSULTA_GLOBAL>>("__oPuestoConsultaGlobal__"); }
            set { Utileria.SetSessionValue<List<E_PUESTOS_CONSULTA_GLOBAL>>("__oPuestoConsultaGlobal__", value); }
        }
    }
}