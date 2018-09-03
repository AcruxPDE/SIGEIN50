using SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo;
using SIGE.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Negocio.FormacionDesarrollo
{
    public class ControlAvanceNegocio
    {
        public SPE_OBTIENE_FYD_PERIODO_EVALUACION_Result ObtenerPeriodoEvaluacion(int pIdPeriodo)
        {
            ControlAvanceOperaciones op = new ControlAvanceOperaciones();
            return op.ObtenerPeriodoEvaluacion(pIdPeriodo);
        }

        public SPE_OBTIENE_CONTROL_AVANCE_DATOS_GRAFICA_Result obtenerDatosControlAvance(int idPeriodo, int? pIdRol)
        {
            ControlAvanceOperaciones op = new ControlAvanceOperaciones();
            return op.obtenerDatosControlAvance(idPeriodo, pIdRol);
        }

        public List<SPE_OBTIENE_CONTROL_AVANCE_EVALUADOS_Result> obtieneEmpleadosEvaluados(int idPeriodo, int? pIdEmpresa, int? pIdRol)
        {
            ControlAvanceOperaciones op = new ControlAvanceOperaciones();
            return op.obtieneEmpleadosEvaluados(idPeriodo, pIdEmpresa, pIdRol);
        }

        public List<SPE_OBTIENE_CONTROL_AVANCE_EVALUADOR_Result> obtieneEmpleadosEvaluadores(int idPeriodo, int? pIdEmpresa, int? pIdRol)
        {
            ControlAvanceOperaciones op = new ControlAvanceOperaciones();
            return op.obtieneEmpleadosEvaluadores(idPeriodo, pIdEmpresa, pIdRol);
        }
    }
}
