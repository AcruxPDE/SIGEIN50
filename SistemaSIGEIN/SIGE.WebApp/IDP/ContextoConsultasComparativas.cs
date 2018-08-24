using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Comunes;
using SIGE.Entidades.IntegracionDePersonal;


namespace SIGE.WebApp.IDP
{
    public class ContextoConsultasComparativas
    {

        public static List<E_PUESTO_VS_CANDIDATOS> oPuestoVsCandidatos
        {
            get { return Utileria.GetSessionValue<List<E_PUESTO_VS_CANDIDATOS>>("__oPuestoVsCandidatos__"); }
            set { Utileria.SetSessionValue<List<E_PUESTO_VS_CANDIDATOS>>("__oPuestoVsCandidatos__", value); }
        }

        public static List<E_CANDIDATO_VS_PUESTOS> oCandidatoVsPuestos
        {
            get { return Utileria.GetSessionValue<List<E_CANDIDATO_VS_PUESTOS>>("__oCandidatoVsPuestos__"); }
            set { Utileria.SetSessionValue<List<E_CANDIDATO_VS_PUESTOS>>("__oCandidatoVsPuestos__", value); }
        }
    }
}