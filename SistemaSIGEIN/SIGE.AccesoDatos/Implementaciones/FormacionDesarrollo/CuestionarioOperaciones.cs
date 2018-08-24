
using SIGE.Entidades;
using System.Collections.Generic;
using System.Linq;


namespace SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo
{
    public class CuestionarioOperaciones
    {
        private SistemaSigeinEntities contexto;

        public List<SPE_OBTIENE_FYD_EVALUADOS_Result> ObtenerEvaluados(int? pIdCurso)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_EVALUADOS(pIdCurso).ToList();
            }
        }

        public List<SPE_OBTIENE_FYD_EVALUADORES_Result> ObtieneEvaluadores(int? pIdPeriodo, string pClTipoEvaluador = null,int? pID_EMPRESA = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_EVALUADORES(pIdPeriodo, pClTipoEvaluador,pID_EMPRESA).ToList();
            }
        }

        public List<SPE_OBTIENE_FYD_EVALUADORES_EXTERNOS_Result> ObtieneEvaluadoresExternos(int? pIdPeriodo, string pClTipoEvaluador = null, int? pID_EMPRESA = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_EVALUADORES_EXTERNOS(pIdPeriodo, pClTipoEvaluador, pID_EMPRESA).ToList();
            }
        }

        public List<SPE_OBTIENE_FYD_PREGUNTAS_ADICIONALES_Result> ObtienePreguntasAdicionales(int? pIdPreguntaAdicional = null, int? pIdPeriodo = null, string pNbPregunta = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_PREGUNTAS_ADICIONALES(pIdPreguntaAdicional, pIdPeriodo, pNbPregunta).ToList();
            }
        }

        public List<SPE_OBTIENE_FYD_RESPUESTAS_ADICIONALES_Result> ObtieneResultadosAdicionales(int? pIdEvaluado= null, int? pIdCuestionario = null, int? pIdEvaluadoEvauador = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_RESPUESTAS_ADICIONALES(pIdEvaluado, pIdCuestionario, pIdEvaluadoEvauador).ToList();
            }
        }


    }
}
