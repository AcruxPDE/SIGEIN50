
using System.Collections.Generic;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo;

namespace SIGE.Negocio.FormacionDesarrollo
{
    public class CuestionarioNegocio
    {
        public List<SPE_OBTIENE_FYD_EVALUADOS_Result> ObtieneEvaluados(int pIdEvaluador)
        {
            CuestionarioOperaciones oCuestionario = new CuestionarioOperaciones();
            return oCuestionario.ObtenerEvaluados(pIdEvaluador);
        }

        public List<SPE_OBTIENE_FYD_EVALUADORES_Result> ObtieneEvaluadores(int pIdPeriodo, string pClTipoEvaluador = null, int? pID_EMPRESA = null)
        {
            CuestionarioOperaciones oCuestionario = new CuestionarioOperaciones();
            return oCuestionario.ObtieneEvaluadores(pIdPeriodo, pClTipoEvaluador, pID_EMPRESA);
        }

        public List<SPE_OBTIENE_FYD_EVALUADORES_EXTERNOS_Result> ObtieneEvaluadoresExternos(int pIdPeriodo, string pClTipoEvaluador = null, int? pID_EMPRESA = null)
        {
            CuestionarioOperaciones oCuestionario = new CuestionarioOperaciones();
            return oCuestionario.ObtieneEvaluadoresExternos(pIdPeriodo, pClTipoEvaluador, pID_EMPRESA);
        }

        public List<SPE_OBTIENE_FYD_PREGUNTAS_ADICIONALES_Result> ObtienePreguntasAdicionales(int? pIdPreguntaAdicional = null, int? pIdPeriodo = null, string pNbPregunta = null)
        {
            CuestionarioOperaciones oCuestionario = new CuestionarioOperaciones();
            return oCuestionario.ObtienePreguntasAdicionales(pIdPreguntaAdicional, pIdPeriodo, pNbPregunta);
        }

        public List<SPE_OBTIENE_FYD_RESPUESTAS_ADICIONALES_Result> ObtieneResultadosAdicionales(int? pIdEvaluado = null, int? pIdCuestionario = null, int? pIdEvaluadoEvaluador = null)
        {
            CuestionarioOperaciones oCuestionario = new CuestionarioOperaciones();
            return oCuestionario.ObtieneResultadosAdicionales(pIdEvaluado, pIdCuestionario, pIdEvaluadoEvaluador);
        }

    }
}
