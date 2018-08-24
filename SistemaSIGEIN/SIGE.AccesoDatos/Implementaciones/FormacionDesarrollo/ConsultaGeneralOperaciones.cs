using SIGE.Entidades;
using SIGE.Entidades.FormacionDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo
{
    public class ConsultaGeneralOperaciones
    {
        private SistemaSigeinEntities context;

        public List<SPE_OBTIENE_EMPLEADOS_Result> ObtieneEmpleados(XElement pXmlSeleccion = null, bool? pFgFotografia = null, string pCL_USUARIO = null,bool? pFgActivo = null, int? pIdEmpresa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                string vXmlFiltro = null;
                if (pXmlSeleccion != null)
                    vXmlFiltro = pXmlSeleccion.ToString();
                return context.SPE_OBTIENE_EMPLEADOS(vXmlFiltro, pCL_USUARIO, pFgActivo, pFgFotografia, pIdEmpresa).ToList();
            }
        }
        public List<SPE_OBTIENE_EMPLEADOS_PDE_Result> ObtieneEmpleados_PDE(XElement pXmlSeleccion = null, string pCL_USUARIO = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                string vXmlFiltro = null;
                if (pXmlSeleccion != null)
                    vXmlFiltro = pXmlSeleccion.ToString();
                return context.SPE_OBTIENE_EMPLEADOS_PDE(vXmlFiltro, pCL_USUARIO).ToList();
            }
        }
        public SPE_OBTIENE_FYD_PERIODO_EVALUACION_Result ObtenerPeriodoEvaluacion(int pIdPeriodo)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_FYD_PERIODO_EVALUACION(pIdPeriodo).FirstOrDefault();
            }
        }

        public List<SPE_OBTIENE_FYD_REPORTE_GLOBAL_Result> ObtenerDatosReporteGlobal(int ID_PERIODO, bool FG_FOTO,  string XML_EMPLEADOS = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_FYD_REPORTE_GLOBAL(ID_PERIODO, XML_EMPLEADOS, FG_FOTO).ToList();
            }
        }

        public List<SPE_OBTIENE_FYD_PUESTOS_EVALUADOS_Result> ObtenerPuestosEvaluadosGlobal(int ID_PERIODO)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_FYD_PUESTOS_EVALUADOS(ID_PERIODO).ToList();
            }
        }


        public List<E_PERIODO_EVALUACION> ObtenerPeriodosEvaluacion(int? pIdPeriodo = null, int? pIdEmpleado = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var ListaSource = context.SPE_OBTIENE_FYD_PERIODOS_EVALUACION(pIdPeriodo, pIdEmpleado).ToList();

                var ListaReturn = (from a in ListaSource select new E_PERIODO_EVALUACION {
                    ID_PERIODO = a.ID_PERIODO,
                    NB_PERIODO = a.NB_PERIODO
                }).ToList();

                return ListaReturn;
            }
        }

        public List<SPE_OBTIENE_FYD_REPORTE_COMPARATIVO_Result> ObtenerDatosReporteComparativo(int ID_PERIODO, bool FG_FOTO)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_FYD_REPORTE_COMPARATIVO(ID_PERIODO, FG_FOTO).ToList();
            }
        }

        public List<SPE_OBTIENE_FYD_EVALUADOS_COMPARATIVO_Result> ObtenerEvaluadosComparativo(int ID_PERIODO, string XML_PERIODOS, bool FG_FOTO)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_FYD_EVALUADOS_COMPARATIVO(ID_PERIODO, XML_PERIODOS, FG_FOTO).ToList();
            }
        }

        public List<SPE_OBTIENE_FYD_REPORTE_COMPARATIVO_DETALLE_Result> ObtenerDetalleReporteComparativo(int pIdPeriodo, int pIdPuestoEvaluadoPeriodo, string pXmlPeriodos, int pIdEmpleado, decimal pPrCumplimientoComparacion)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_FYD_REPORTE_COMPARATIVO_DETALLE(pIdPeriodo, pIdPuestoEvaluadoPeriodo, pXmlPeriodos, pIdEmpleado, pPrCumplimientoComparacion).ToList();
            }
        }

        public List<E_EVALUADO> ObtenerEvaluados(int pIdPeriodo, int? pID_EMPRESA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var vListaEvaluados = context.SPE_OBTIENE_FYD_EVALUADOS_CONFIGURACION(pIdPeriodo,pID_EMPRESA).ToList();

                var vLstEvaluados = vListaEvaluados.Select(t => new E_EVALUADO
                {
                    CL_DEPARTAMENTO = t.CL_DEPARTAMENTO,
                    CL_EMPLEADO = t.CL_EMPLEADO,
                    CL_EMPRESA = t.CL_EMPRESA,
                    CL_PUESTO = t.CL_PUESTO,
                    ID_EMPLEADO = t.ID_EMPLEADO,
                    ID_EVALUADO = t.ID_EVALUADO,
                    NB_DEPARTAMENTO = t.NB_DEPARTAMENTO,
                    NB_EMPLEADO_COMPLETO = t.NB_EMPLEADO_COMPLETO,
                    NB_EMPRESA = t.NB_EMPRESA,
                    NB_PUESTO = t.NB_PUESTO,
                    NB_RAZON_SOCIAL = t.NB_RAZON_SOCIAL
                }).ToList();

                return vLstEvaluados;
            }
        }
    }
}
