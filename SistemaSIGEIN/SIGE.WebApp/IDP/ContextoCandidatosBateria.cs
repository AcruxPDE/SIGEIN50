using SIGE.Entidades.Externas;
using SIGE.Entidades.IntegracionDePersonal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Comunes;

namespace SIGE.WebApp.IDP
{
    public class ContextoCandidatosBateria
    {
        public static List<E_CANDIDATOS_BATERIA> oCandidatosBateria
        {
            get { return Utileria.GetSessionValue<List<E_CANDIDATOS_BATERIA>>("__oSeleccionCandidatosBateria__"); }
            set { Utileria.SetSessionValue<List<E_CANDIDATOS_BATERIA>>("__oSeleccionCandidatosBateria__", value); }
        }
    }
}