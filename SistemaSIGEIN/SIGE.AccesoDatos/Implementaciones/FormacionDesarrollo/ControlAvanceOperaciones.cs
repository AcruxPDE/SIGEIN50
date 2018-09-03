using SIGE.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo
{
    public class ControlAvanceOperaciones
    {
        SistemaSigeinEntities contexto;

        public SPE_OBTIENE_FYD_PERIODO_EVALUACION_Result ObtenerPeriodoEvaluacion(int pIdPeriodo)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_PERIODO_EVALUACION(pIdPeriodo).FirstOrDefault();
            }
        }

        public SPE_OBTIENE_CONTROL_AVANCE_DATOS_GRAFICA_Result obtenerDatosControlAvance(int idPeriodo, int? pIdRol) 
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_CONTROL_AVANCE_DATOS_GRAFICA(idPeriodo, pIdRol).FirstOrDefault();
            }
        }

        public List<SPE_OBTIENE_CONTROL_AVANCE_EVALUADOS_Result> obtieneEmpleadosEvaluados(int idPeriodo, int? pIdEmpresa, int? pIdRol)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_CONTROL_AVANCE_EVALUADOS(idPeriodo, pIdEmpresa, pIdRol).ToList();
            }
        }

        public List<SPE_OBTIENE_CONTROL_AVANCE_EVALUADOR_Result> obtieneEmpleadosEvaluadores(int idPeriodo, int? pIdEmpresa, int? pIdRol)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_CONTROL_AVANCE_EVALUADOR(idPeriodo, pIdEmpresa, pIdRol).ToList();
            }
        }

    }
}
