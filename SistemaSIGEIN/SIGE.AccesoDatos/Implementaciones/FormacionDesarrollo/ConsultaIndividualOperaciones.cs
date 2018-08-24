using SIGE.Entidades;
using SIGE.Entidades.FormacionDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo
{
    public class ConsultaIndividualOperaciones
    {
        SistemaSigeinEntities contexto;

        public List<SPE_OBTIENE_FYD_EVALUADOS_CONFIGURACION_Result> ObtenerEvaluados(int pIdPeriodo,int? pID_EMPRESA = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_EVALUADOS_CONFIGURACION(pIdPeriodo,pID_EMPRESA).ToList();
            }
        }

        public SPE_OBTIENE_FYD_PERIODO_EVALUACION_Result ObtenerPeriodoEvaluacion(int pIdPeriodo)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_PERIODO_EVALUACION(pIdPeriodo).FirstOrDefault();
            }
        }

        public List<E_PERIODO_EVALUACION> ObtenerPeriodosEvaluacion(int? pIdPeriodo = null, int? pIdEmpleado = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                var ListaSource = contexto.SPE_OBTIENE_FYD_PERIODOS_EVALUACION(pIdPeriodo, pIdEmpleado).ToList();

                var ListaReturn = (from a in ListaSource
                                   select new E_PERIODO_EVALUACION
                                   {
                                       ID_PERIODO = a.ID_PERIODO,
                                       NB_PERIODO = a.NB_PERIODO
                                   }).ToList();

                return ListaReturn;
            }
        }

        //public List<E_REPORTE_GENERAL_INDIVIDUAL> ObtenerDatosReporteGeneralIndividual(int ID_PERIODO, int ID_EVALUADO)
        //{
        //    using (contexto = new SistemaSigeinEntities())
        //    {
        //        var oListaSource = contexto.SPE_OBTIENE_FYD_REPORTE_GENERAL_INDIVIDUAL(ID_PERIODO, ID_EVALUADO).ToList();

        //        List<E_REPORTE_GENERAL_INDIVIDUAL> oListaRegreso = oListaSource.Select(t => new E_REPORTE_GENERAL_INDIVIDUAL
        //        {
        //            CL_COLOR = t.CL_COLOR,
        //            CL_COLOR_COMPATIBILIDAD = t.CL_COLOR_COMPATIBILIDAD,
        //            CL_EVALUADO = t.CL_EVALUADO,
        //            CL_PUESTO = t.CL_PUESTO,
        //            CL_TIPO_COMPETENCIA = t.CL_TIPO_COMPETENCIA,
        //            DS_COMPETENCIA = t.DS_COMPETENCIA,
        //            ID_COMPETENCIA = t.ID_COMPETENCIA,
        //            ID_EMPLEADO = t.ID_EMPLEADO,
        //            ID_EVALUADO = t.ID_EVALUADO,
        //            ID_PERIODO = t.ID_PERIODO,
        //            ID_PUESTO = t.ID_PUESTO,
        //            NB_COMPETENCIA = t.NB_COMPETENCIA,
        //            NB_EVALUADO = t.NB_EVALUADO,
        //            NB_PUESTO = t.NB_PUESTO,
        //            NO_TOTAL_TIPO_EVALUACION = t.NO_TOTAL_TIPO_EVALUACION,
        //            NO_ORDEN = t.NO_LINEA
        //        }).ToList();

        //        return oListaRegreso;
        //    }
        //}

        public List<SPE_OBTIENE_FYD_REPORTE_GENERAL_INDIVIDUAL_Result> ObtenerDatosReporteGeneralIndividual(int ID_PERIODO, int? ID_EVALUADO, int? ID_EMPLEADO )
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_REPORTE_GENERAL_INDIVIDUAL(ID_PERIODO, ID_EVALUADO, ID_EMPLEADO).ToList();
            }
        }

        public List<SPE_OBTIENE_FYD_CUMPLIMIENTO_PUESTO_PERIODO_Result> ObtenerCumplimientoPuestoPeriodo(int pID_PERIODO, int? pID_EVALUADO, int? pID_PUESTO, int? pID_EMPLEADO)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_CUMPLIMIENTO_PUESTO_PERIODO(pID_PERIODO, pID_EVALUADO, pID_PUESTO, pID_EMPLEADO).ToList();
            }
        }


        public List<SPE_OBTIENE_FYD_REPORTE_INDIVIAL_EVALUADO_Result> ObtenerCumplimientoGeneralIndividual(int? pID_PERIODO, int? pID_EMPLEADO, int? pID_COMPETENCIA)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_REPORTE_INDIVIAL_EVALUADO(pID_PERIODO, pID_EMPLEADO, pID_COMPETENCIA).ToList();
            }
        }


        public List<E_REPORTE_360> ObtenerDatosReporte360(int ID_PERIODO, int ID_EVALUADO)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                var oListaSource = contexto.SPE_OBTIENE_FYD_REPORTE_360(ID_PERIODO, ID_EVALUADO).ToList();

                List<E_REPORTE_360> oListaRegreso = oListaSource.Select(t => new E_REPORTE_360
                {
                    CL_COLOR = t.CL_COLOR,
                    CL_PUESTO = t.CL_PUESTO,
                    NO_ORDEN_CONSECUTIVO = t.NO_ORDEN_CONSECUTIVO,
                    NO_COMPETENCIA = t.NO_COMPETENCIA.Value,
                    ID_COMPETENCIA = t.ID_COMPETENCIA.Value,
                    ID_PERIODO = t.ID_PERIODO,
                    ID_PUESTO = t.ID_PUESTO,
                    NB_COMPETENCIA = t.NB_COMPETENCIA,
                    NO_VALOR_COMPETENCIA = t.NO_VALOR_COMPETENCIA,
                    NO_ORDEN = t.NO_ORDEN.Value
                }).ToList();


                return oListaRegreso;
            }
        }

        public List<SPE_OBTIENE_FYD_REPORTE_COMPARATIVO_INDIVIDUAL_Result> ObtenerDatosReporteComparativoIndividual(int ID_EMPLEADO, string XML_PERIODOS)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_REPORTE_COMPARATIVO_INDIVIDUAL(ID_EMPLEADO, XML_PERIODOS).ToList();
            }
        }

        public List<SPE_OBTIENE_FYD_PUESTOS_PERIODO_Result> ObtenerPuestosPeriodo(int ID_EMPLEADO, string XML_PERIODOS)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_PUESTOS_PERIODO(ID_EMPLEADO, XML_PERIODOS).ToList();
            }
        }
    }
}
