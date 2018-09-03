using SIGE.Entidades.IntegracionDePersonal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Comunes;

namespace SIGE.WebApp.IDP
{
    public class ContextoBusquedaCandidato
    {
        public static List<E_PARAMETROS_BUSQUEDA_CANDIDATO> oPuestoVsCandidatos
        {
            get { return Utileria.GetSessionValue<List<E_PARAMETROS_BUSQUEDA_CANDIDATO>>("__oBusquedaCandidatos__"); }
            set { Utileria.SetSessionValue<List<E_PARAMETROS_BUSQUEDA_CANDIDATO>>("__oBusquedaCandidatos__", value); }
        }
    }
}