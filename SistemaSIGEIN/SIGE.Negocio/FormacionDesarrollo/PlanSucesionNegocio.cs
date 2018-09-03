using SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.Negocio.FormacionDesarrollo
{
    public class PlanSucesionNegocio
    {
        public List<SPE_OBTIENE_PLAN_SUCESION_Result> obtienePlanSucesion(int ID_EMPLEADO, int? ID_EMPRESA, string XML_PRIORIDADES, string XML_EMPLEADOS = null)
        {
            PlanSucesionOperaciones op = new PlanSucesionOperaciones();
            return op.obtienePlanSucesion(ID_EMPLEADO, ID_EMPRESA, XML_PRIORIDADES, XML_EMPLEADOS);
        }
      
        public List<SPE_OBTIENE_EMPLEADOS_Result> ObtieneEmpleados(XElement pXmlSeleccion = null, bool pFgFoto = false)
        {
            EmpleadoOperaciones oEmpleados = new EmpleadoOperaciones();
            return oEmpleados.ObtenerEmpleados(pXmlSeleccion, pFgFoto: pFgFoto);
        }

        public List<SPE_OBTIENE_EVALUACION_COMPETENCIAS_PLAN_SUCESION_Result> ObtieneEvalCompetenciasSucesion(int? vIdEmpleadoSuceder = null, int? vIdEmpleadoSucesor = null, int? vIdPuestoSuceder = null, int? vIdPuestoSucesor = null)
        {
            EmpleadoOperaciones oEmpleados = new EmpleadoOperaciones();
            return oEmpleados.ObtieneEvalCompetenciasSucesion(vIdEmpleadoSuceder, vIdEmpleadoSucesor, vIdPuestoSuceder, vIdPuestoSucesor);
        }

        public DataTable obtenerComparacionPuestos(string XML_PUESTOS = null, int? ID_EMPLEADO = null)
        {
            PlanVidaCarreraOperaciones op = new PlanVidaCarreraOperaciones();
            List<SPE_OBTIENE_COMPARACION_PUESTOS_PLAN_VIDA_CARRERA_Result> lista = new List<SPE_OBTIENE_COMPARACION_PUESTOS_PLAN_VIDA_CARRERA_Result>();

            lista = op.obtenerComparacionPuestos(XML_PUESTOS, ID_EMPLEADO);

            Utilerias.Utilerias aux = new Utilerias.Utilerias();

            return aux.ConvertToDataTable<SPE_OBTIENE_COMPARACION_PUESTOS_PLAN_VIDA_CARRERA_Result>(lista, true);

        }

        public List<E_COMPARACION_COMPETENCIA> obtieneComparacionCompetenciasPlanSucesion(string XML_EMPLEADOS, int? ID_PUESTO)
        {
            PlanSucesionOperaciones op = new PlanSucesionOperaciones();
            return op.obtieneComparacionCompetenciasPlanSucesion(XML_EMPLEADOS, ID_PUESTO);
        }

        public List<SPE_OBTIENE_COMPARACION_PUESTOS_PLAN_SUCESION_Result> obtieneComparacionPuestosPlanSucesion(int ID_PUESTO, string XML_EMPLEADOS)
        {
            PlanSucesionOperaciones op = new PlanSucesionOperaciones();
            return op.obtieneComparacionPuestosPlanSucesion(ID_PUESTO, XML_EMPLEADOS);
        }
    }
}
