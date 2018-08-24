using SIGE.Entidades;
using SIGE.Entidades.Externas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo
{
    public class PlanVidaCarreraOperaciones
    {

        SistemaSigeinEntities contexto;

        public List<SPE_OBTIENE_PLAN_VIDA_CARRERA_Result> obtenerDatosPlanVidaCarrera(int ID_PUESTO, int? ID_EMPRESA)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_PLAN_VIDA_CARRERA(ID_PUESTO, ID_EMPRESA).ToList();
            }
        }

        public List<E_COMPARACION_COMPETENCIA> obtenerComparacionCompetenciasPlanVidaCarrera(int ID_EMPLEADO, int ID_PERIODO, string XML_PUESTOS)
        {
            using (contexto = new SistemaSigeinEntities())
            {

                List<E_COMPARACION_COMPETENCIA> source = new List<E_COMPARACION_COMPETENCIA>();

                var lista = contexto.SPE_OBTIENE_COMPARACION_COMPETENCIAS_PLAN_VIDA_CARRERA(ID_EMPLEADO, ID_PERIODO, XML_PUESTOS).ToList();

                source = (from a in lista
                          select new E_COMPARACION_COMPETENCIA
                          {
                              ID_PUESTO = a.ID_PUESTO,
                              CL_PUESTO = a.CL_PUESTO,
                              NB_PUESTO = a.NB_PUESTO,
                              ID_COMPETENCIA = a.ID_COMPETENCIA,
                              NB_COMPETENCIA = a.NB_COMPETENCIA,
                              DS_COMPETENCIA = a.DS_COMPETENCIA,
                              CL_COLOR_COMPATIBILIDAD = a.CL_COLOR_COMPATIBILIDAD,
                              NO_ORDEN = a.NO_ORDEN,
                              //NO_VALOR_NIVEL = a.NO_VALOR_NIVEL,
                              //NO_RESULTADO_PROMEDIO = a.NO_RESULTADO_PROMEDIO,
                              
                              PR_COMPATIBILIDAD = a.PR_COMPATIBILIDAD.ToString(),
                              PR_NO_COMPATIBILIDAD = a.PR_COMPATIBILIDAD,
                              CL_COLOR = a.CL_COLOR
                          }).ToList();

                return source;
            }
        }

        public List<SPE_OBTIENE_COMPARACION_PUESTOS_PLAN_VIDA_CARRERA_Result> obtenerComparacionPuestos(string XML_PUESTOS = null, int? ID_EMPLEADO = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_COMPARACION_PUESTOS_PLAN_VIDA_CARRERA(XML_PUESTOS, ID_EMPLEADO).ToList();
            }
        }

        public List<SPE_OBTIENE_M_PUESTO_Result> ObtenerPuestos(int? pIdPuesto = null, bool? pFgActivo = null, DateTime? pFeInactivo = null, string pClPuesto = null, string pNbPuesto = null, int? pIdDepartamento = null, string pXmlCamposAdicionales = null, int? pIdBitacora = null, byte? pNoEdadMinima = null, byte? pNoEdadMaxima = null, string pClGenero = null, string pClEstadoCivil = null, string pXmlRequerimientos = null, string pXmlObservaciones = null, string pXmlResponsabilidades = null, string pXmlAutoridad = null, string pXmlCursosAdicionales = null, string pXmlMentor = null, string pClTipoPuesto = null, Guid? pIdCentroAdministrativo = null, Guid? pIdCentroOperativo = null, int? pIdPaquetePrestaciones = null, string pNbDepartamento = null, string pClDepartamento = null, string xml_puestos = null, string XML_PUESTOS_SELECCIONADOS = null, int? pIdEmpresa= null, int? pID_ROL = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_M_PUESTO(pIdPuesto, pFgActivo, pFeInactivo, pClPuesto, pNbPuesto, pIdDepartamento, pXmlCamposAdicionales, pIdBitacora, pNoEdadMinima, pNoEdadMaxima, pClGenero, pClEstadoCivil, pXmlRequerimientos, pXmlObservaciones, pXmlResponsabilidades, pXmlAutoridad, pXmlCursosAdicionales, pXmlMentor, pClTipoPuesto, pIdCentroAdministrativo, pIdCentroOperativo, pIdPaquetePrestaciones, xml_puestos, pNbDepartamento, pClDepartamento, XML_PUESTOS_SELECCIONADOS, pIdEmpresa, pID_ROL).ToList();
            }
        }
    }
}
