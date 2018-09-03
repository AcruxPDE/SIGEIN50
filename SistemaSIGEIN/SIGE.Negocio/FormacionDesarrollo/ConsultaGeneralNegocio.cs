using SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo;
using SIGE.Entidades;
using SIGE.Entidades.FormacionDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.Negocio.FormacionDesarrollo
{
    public class ConsultaGeneralNegocio
    {
        public List<SPE_OBTIENE_EMPLEADOS_Result> ObtenerEmpleados(XElement pXmlSeleccion = null)
        {
            ConsultaGeneralOperaciones op = new ConsultaGeneralOperaciones();
            return op.ObtieneEmpleados(pXmlSeleccion);
        }
        public List<SPE_OBTIENE_EMPLEADOS_PDE_Result> ObtenerEmpleados_PDE(XElement pXmlSeleccion = null)
        {
            ConsultaGeneralOperaciones op = new ConsultaGeneralOperaciones();
            return op.ObtieneEmpleados_PDE(pXmlSeleccion);
        }
        public SPE_OBTIENE_FYD_PERIODO_EVALUACION_Result ObtenerPeriodoEvaluacion(int pIdPeriodo)
        {
            ConsultaGeneralOperaciones op = new ConsultaGeneralOperaciones();
            return op.ObtenerPeriodoEvaluacion(pIdPeriodo);
        }

        public List<SPE_OBTIENE_FYD_REPORTE_GLOBAL_Result> ObtenerDatosReporteGlobal(int ID_PERIODO, string XML_EMPLEADOS, bool FG_FOTO)
        {
            ConsultaGeneralOperaciones oConsulta = new ConsultaGeneralOperaciones();
            return oConsulta.ObtenerDatosReporteGlobal(ID_PERIODO, FG_FOTO, XML_EMPLEADOS);
        }

        public List<SPE_OBTIENE_FYD_PUESTOS_EVALUADOS_Result> ObtenerPuestosEvaluadosGlobal(int ID_PERIODO)
        {
            ConsultaGeneralOperaciones oConsulta = new ConsultaGeneralOperaciones();
            return oConsulta.ObtenerPuestosEvaluadosGlobal(ID_PERIODO);
        }

        public List<E_PERIODO_EVALUACION> ObtenerPeriodosEvaluacion(int? pIdPeriodo = null)
        {
            ConsultaGeneralOperaciones oConsulta = new ConsultaGeneralOperaciones();
            return oConsulta.ObtenerPeriodosEvaluacion(pIdPeriodo);
        }

        public List<SPE_OBTIENE_FYD_REPORTE_COMPARATIVO_Result> ObtenerDatosReporteComparativo(int ID_PERIODO, bool FG_FOTO)
        {
            ConsultaGeneralOperaciones oConsulta = new ConsultaGeneralOperaciones();
            return oConsulta.ObtenerDatosReporteComparativo(ID_PERIODO, FG_FOTO);
        }

        public List<SPE_OBTIENE_FYD_EVALUADOS_COMPARATIVO_Result> ObtenerEvaluadosComparativo(int ID_PERIODO, string XML_PERIODOS, bool FG_FOTO, int? vIdRol)
        {
            ConsultaGeneralOperaciones oConsulta = new ConsultaGeneralOperaciones();
            return oConsulta.ObtenerEvaluadosComparativo(ID_PERIODO, XML_PERIODOS, FG_FOTO, vIdRol);
        }

        public List<SPE_OBTIENE_FYD_REPORTE_COMPARATIVO_DETALLE_Result> ObtenerDetalleReporteComparativo(int pIdPeriodo, int pIdPuestoEvaluadoPeriodo, string pXmlPeriodos, int pIdEmpleado, decimal pPrCumplimientoComparacion)
        {
            ConsultaGeneralOperaciones oConsulta = new ConsultaGeneralOperaciones();
            return oConsulta.ObtenerDetalleReporteComparativo(pIdPeriodo, pIdPuestoEvaluadoPeriodo, pXmlPeriodos, pIdEmpleado, pPrCumplimientoComparacion);
        }

        public List<E_EVALUADO> ObtieneEvaluados(int pIdPeriodo, int? pID_EMPRESA = null, int? pID_ROL = null)
        {
            ConsultaGeneralOperaciones oConsulta = new ConsultaGeneralOperaciones();
            return oConsulta.ObtenerEvaluados(pIdPeriodo, pID_EMPRESA, pID_ROL);
        }
    }
}
