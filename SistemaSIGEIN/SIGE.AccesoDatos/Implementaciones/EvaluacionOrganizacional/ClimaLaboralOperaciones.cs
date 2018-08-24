using SIGE.Entidades;
using SIGE.Entidades.Externas;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SIGE.Entidades.EvaluacionOrganizacional;

namespace SIGE.AccesoDatos.Implementaciones.EvaluacionOrganizacional
{
    public class ClimaLaboralOperaciones
    {
        SistemaSigeinEntities context;

        public List<SPE_OBTIENE_EO_PERIODOS_CLIMA_Result> ObtenerPeriodosClima(int? pIdPeriodo = null, string pClPeriodo = null, string pNbPeriodo = null, string pDsPeriodo = null, string pClEstadoPeriodo = null, int? pIdPeriodoOrigen = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EO_PERIODOS_CLIMA(pIdPeriodo, pClPeriodo, pNbPeriodo, pDsPeriodo, pClEstadoPeriodo, pIdPeriodoOrigen).ToList();
            }
        }

        public XElement EliminarPeriodoClimaLaboral(int pIdPeriodo)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_PERIODO_CLIMA_LABORAL(pOutClRetorno, pIdPeriodo);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement InsertarActualizarPeriodoClima(E_PERIODO_CLIMA pPeriodo, string pClUsuario, string pNbPrograma, string pTipoTransaccion)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_PERIODO_CLIMA_LAB(poutClaveRetorno, pPeriodo.ID_PERIODO, pPeriodo.CL_PERIODO, pPeriodo.NB_PERIODO, pPeriodo.DS_PERIODO, pPeriodo.CL_ESTADO_PERIODO, pPeriodo.DS_NOTAS,pPeriodo.CL_TIPO_CONFIGURACION, pPeriodo.CL_ORIGEN_CUESTIONARIO, pClUsuario, pNbPrograma, pPeriodo.ID_PERIODO_ORIGEN, pTipoTransaccion);
                return XElement.Parse(poutClaveRetorno.Value.ToString());
            }
        }

        public XElement InsertarPeriodoClimaCopia(E_PERIODO_CLIMA_COPIA pPeriodo, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_PERIODO_CLIMA_LAB_COPIA(poutClaveRetorno, pPeriodo.ID_PERIODO, pPeriodo.CL_PERIODO, pPeriodo.NB_PERIODO, pPeriodo.DS_PERIODO, pPeriodo.CL_ESTADO_PERIODO, pPeriodo.DS_NOTAS,pPeriodo.CL_TIPO_CONFIGURACION, pClUsuario, pNbPrograma);
                return XElement.Parse(poutClaveRetorno.Value.ToString());
            }
        }

        public XElement InsertarActualizarEvaluadorClima(int pIdPeriodo, string pXmlEvaluadores, string pClUsuario, string pNbPrograma, string pTipoTransaccion)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_EVALUADOR_CLIMA_LAB(poutClaveRetorno, pIdPeriodo, pXmlEvaluadores, pClUsuario, pNbPrograma, pTipoTransaccion);
                return XElement.Parse(poutClaveRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_EO_EVALUADORES_CLIMA_LAB_Result> ObtenerEvaluadoresClima(int? pIdPeriodo = null, int? pIdEmpleado = null, int? pIdPuesto = null, int? pIdEvaluador = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EO_EVALUADORES_CLIMA_LAB(pIdPeriodo, pIdEmpleado, pIdPuesto, pIdEvaluador).ToList();
            }
        }

        public XElement InsertarActualizarPreguntasPeriodo(int? pIdPeriodo, E_PREGUNTAS_PERIODO_CLIMA pPreguntasPeriodo, string pClUsuario, string pNbPrograma, string pTipoTransaccion)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_EO_PREGUNTAS_PERIODO(poutClaveRetorno, pIdPeriodo, pPreguntasPeriodo.ID_PREGUNTA, pPreguntasPeriodo.ID_PREGUNTA_VERIFICACION, pPreguntasPeriodo.NB_DIMENSION, pPreguntasPeriodo.NB_TEMA, pPreguntasPeriodo.NO_SECUENCIA, pPreguntasPeriodo.NB_PREGUNTA, pPreguntasPeriodo.ID_RELACION_PREGUNTA, pPreguntasPeriodo.NB_PREGUNTA_VERIFICACION, pPreguntasPeriodo.NO_SECUENCIA_VERIFICACION, pPreguntasPeriodo.FG_HABILITA_VERIFICACION, pClUsuario, pNbPrograma, pTipoTransaccion);
                return XElement.Parse(poutClaveRetorno.Value.ToString());
            }
        }

        public XElement InsertaActualizaDimension(string pNbDimension = null, string pClUsuario = null, string pNbPrograma = null, string pTipoTransaccion = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_EO_DIMENSIONES(poutClaveRetorno, pNbDimension, pClUsuario, pNbPrograma, pTipoTransaccion);
                return XElement.Parse(poutClaveRetorno.Value.ToString());
            }
        }

        public XElement InsertaActualizaTema(string pClDimension = null, string pNbTema = null, string pClUsuario = null, string pNbPrograma = null, string pTipoTransaccion = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_EO_TEMAS(poutClaveRetorno, pClDimension , pNbTema, pClUsuario, pNbPrograma, pTipoTransaccion);
                return XElement.Parse(poutClaveRetorno.Value.ToString());
            }
        }

        public XElement InsertarActualizarCuestionariosPeriodo(int? pIdPeriodo, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_EO_CUESTIONARIOS_PERIODO(poutClaveRetorno, pIdPeriodo, pClUsuario, pNbPrograma);
                return XElement.Parse(poutClaveRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_EO_PREGUNTAS_PERIODO_Result> ObtenerPreguntasPeriodo(int? pIdPeriodo = null, int? pIdPregunta = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EO_PREGUNTAS_PERIODO(pIdPeriodo, pIdPregunta).ToList();
            }
        }

        public XElement EliminarPreguntasPeriodo(string pClUsuario, string pNbPrograma, string pXmlPreguntasPeriodo)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_EO_PREGUNTAS_PERIODO(pOutClRetorno, pClUsuario, pNbPrograma, pXmlPreguntasPeriodo);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement EliminarEvaluadoresPeriodo(string pXmlEvaluadoresPeriodo, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_EO_EVALUADORES_PERIODO(pOutClRetorno, pXmlEvaluadoresPeriodo, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement InsertarActualizarTokenEvaluadoresClima(int pIdPeriodo, XElement pXmlTokenEvaluadores, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_EO_EVALUADORES_TOKEN(pOutClRetorno, pIdPeriodo, pXmlTokenEvaluadores.ToString(), pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_EO_EVALUADORES_TOKEN_Result> ObtenerTokenEvaluadoresClima(int pIdPeriodo)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EO_EVALUADORES_TOKEN(pIdPeriodo).ToList();
            }
        }

        public List<SPE_OBTIENE_EO_PREGUNTA_PERIODO_Result> ObtenerPreguntaPeriodo(int? pIdPeriodo = null, int? pIdPregunta = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EO_PREGUNTA_PERIODO(pIdPeriodo, pIdPregunta).ToList();
            }
        }

        public XElement ActualizarCorreoEvaluadores(string pXmlEvaluadores, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ACTUALIZA_EO_EVALUADORES_CORREO(pOutClRetorno, pXmlEvaluadores, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_EO_PREGUNTAS_CUESTIONARIO_Result> ObtenerCuestionario(int? pIdEvaluador = null, int? pIdPeriodo = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EO_PREGUNTAS_CUESTIONARIO(pIdEvaluador, pIdPeriodo).ToList();
            }
        }

        public XElement ActualizarPreguntasCuestionario(string pXmlRespuestas, string pXmlPreguntasAbiertas, bool pFgContestado, int pIdEvaluador, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ACTUALIZA_EO_PREGUNTAS_CUESTIONARIOS(poutClaveRetorno, pXmlRespuestas, pXmlPreguntasAbiertas, pFgContestado, pIdEvaluador, pClUsuario, pNbPrograma);
                return XElement.Parse(poutClaveRetorno.Value.ToString());
            }
        }

        public XElement InsertarCuestionarioConfiable(int pID_PERIODO, string pPIN_XML_RESPUESTAS, string pPIN_XML_PREGUNTAS_ABIERTAS, string pPIN_XML_DATOS_EVALUADOR, string pPIN_XML_DATOS_CAMPOS_EXTRA, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_EO_CUESTIONARIO_CONFIDENCIAL(poutClaveRetorno, pID_PERIODO, pPIN_XML_RESPUESTAS, pPIN_XML_PREGUNTAS_ABIERTAS, pPIN_XML_DATOS_EVALUADOR, pPIN_XML_DATOS_CAMPOS_EXTRA, pClUsuario, pNbPrograma);
                return XElement.Parse(poutClaveRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_CONTROL_AVANCE_EO_DATOS_GRAFICA_Result> ObtenerControlAvance(int? pIdPeriodo = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_CONTROL_AVANCE_EO_DATOS_GRAFICA(pIdPeriodo).ToList();
            }
        }

        public SPE_OBTIENE_EO_PERIODO_EVALUADOR_Result ObtenerPeriodoEvaluador(int? pIdEvaluador = null, Guid? pFlEvaluador = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EO_PERIODO_EVALUADOR(pIdEvaluador, pFlEvaluador).FirstOrDefault();
            }
        }

        public List<SPE_OBTIENE_EO_EVALUADORES_CUESTIONARIO_Result> ObtenerEvaluadoresCuestionario(int? pIdPeriodo = null, int? pIdEmpleado = null, int? pIdPuesto = null, int? pIdEvaluador = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EO_EVALUADORES_CUESTIONARIO(pIdPeriodo, pIdEmpleado, pIdPuesto, pIdEvaluador).ToList();
            }
        }

        public List<SPE_OBTIENE_EO_GRAFICA_INDICE_DIMENSION_Result> ObtenerGraficaDimension(int? pIdPeriodo = null, XElement pXmlFiltros = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                string vXML_FILTROS = null;
                if (pXmlFiltros != null)
                    vXML_FILTROS = pXmlFiltros.ToString();
                return context.SPE_OBTIENE_EO_GRAFICA_INDICE_DIMENSION(pIdPeriodo, vXML_FILTROS).ToList();
            }
        }

        public List<SPE_OBTIENE_EO_GRAFICA_INDICE_TEMA_Result> ObtenerGraficaTema(int? pIdPeriodo = null, XElement pXmlFiltros = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                string vXML_FILTROS = null;
                if (pXmlFiltros != null)
                    vXML_FILTROS = pXmlFiltros.ToString();
                return context.SPE_OBTIENE_EO_GRAFICA_INDICE_TEMA(pIdPeriodo, vXML_FILTROS).ToList();
            }
        }

        public List<SPE_OBTIENE_EO_GRAFICA_INDICE_PREGUNTA_Result> ObtenerGraficaPregunta(int? pIdPeriodo = null, XElement pXmlFiltros = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                string vXML_FILTROS = null;
                if (pXmlFiltros != null)
                    vXML_FILTROS = pXmlFiltros.ToString();
                return context.SPE_OBTIENE_EO_GRAFICA_INDICE_PREGUNTA(pIdPeriodo, vXML_FILTROS).ToList();
            }
        }

        public List<SPE_OBTIENE_EO_GRAFICA_DISTRIBUCION_DIMENSION_Result> ObtenerGraficaDistribucionDimension(int? pIdPeriodo = null, string pNbDimension = null, XElement pXmlFiltros = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                string vXML_FILTROS = null;
                if (pXmlFiltros != null)
                    vXML_FILTROS = pXmlFiltros.ToString();
                return context.SPE_OBTIENE_EO_GRAFICA_DISTRIBUCION_DIMENSION(pIdPeriodo, pNbDimension, vXML_FILTROS).ToList();
            }
        }

        public List<SPE_OBTIENE_EO_GRAFICA_DISTRIBUCION_TEMA_Result> ObtenerGraficaDistribucionTema(int? pIdPeriodo = null, string pNbTema = null, XElement pXmlFiltros = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                string vXML_FILTROS = null;
                if (pXmlFiltros != null)
                    vXML_FILTROS = pXmlFiltros.ToString();
                return context.SPE_OBTIENE_EO_GRAFICA_DISTRIBUCION_TEMA(pIdPeriodo, pNbTema, vXML_FILTROS).ToList();
            }
        }

        public List<SPE_OBTIENE_EO_GRAFICA_DISTRIBUCION_PREGUNTA_Result> ObtenerGraficaDistribucionPregunta(int? pIdPeriodo = null, string pNbPregunta = null, string pXML_FILTROS = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EO_GRAFICA_DISTRIBUCION_PREGUNTA(pIdPeriodo, pNbPregunta, pXML_FILTROS).ToList();
            }
        }

        public List<SPE_OBTIENE_DIMENSIONES_Result> ObtenerDimensiones(int? pIdDimension = null, string pClDimension = null, string pNbDimension = null, int? pIdPeriodo = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_DIMENSIONES(pIdDimension, pClDimension, pNbDimension, pIdPeriodo).ToList();
            }
        }

        public List<SPE_OBTIENE_EO_DIMENSIONES_Result> ObtenerCatalogoDimensiones(int? pIdDimension = null, string pClDimension = null, string pNbDimension = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EO_DIMENSIONES(pIdDimension, pClDimension, pNbDimension).ToList();
            }
        }

        public List<SPE_OBTIENE_EO_TEMAS_Result> ObtenerCatalogoTemas(string pClTema = null, string pNbTema = null, int? pIdTema = null, int? pIdDimension = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EO_TEMAS(pClTema, pNbTema, pIdTema, pIdDimension).ToList();
            }
        }

        public List<SPE_OBTIENE_TEMAS_Result> ObtenerTemas(string pClTema = null, string pNbTema = null, int? pIdPeriodo = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_TEMAS(pClTema, pNbTema, pIdPeriodo).ToList();
            }
        }

        public List<SPE_OBTIENE_PREGUNTAS_Result> ObtenerPreguntas(int? pIdPregunta = null, string pClPregunta = null, int? pIdPeriodo = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_PREGUNTAS(pIdPregunta, pClPregunta, pIdPeriodo).ToList();
            }
        }

        public List<SPE_OBTIENE_EO_GRAFICA_GLOBAL_Result> ObtenerGraficaGlobal(int? pIdPeriodo = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EO_GRAFICA_GLOBAL(pIdPeriodo).ToList();
            }
        }

        public XElement InsertaActualizaPreguntasAbiertas(int? pIdPeriodo = null, int? pIdPregunta = null, string pNbPregunta = null, string pDsPregunta = null, string pClUsuario = null, string pNbPrograma = null, string pTipoTransaccion = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_EO_PREGUNTAS_ABIERTAS_PERIODO(pOutClRetorno, pIdPeriodo, pIdPregunta, pNbPregunta, pDsPregunta, pClUsuario, pNbPrograma, pTipoTransaccion);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_EO_PREGUNTAS_ABIERTAS_PERIODO_Result> ObtenerPreguntasAbiertas(int? pIdPeriodo = null, int? pIdPregunta = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EO_PREGUNTAS_ABIERTAS_PERIODO(pIdPeriodo, pIdPregunta).ToList();
            }
        }

        public XElement EliminarPreguntasAbiertas(int? pIdPeriodo = null, string pXmlPreguntasPeriodo= null, string pClUsuario=null, string pNbPrograma = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_EO_PREGUNTAS_ABIERTAS_PERIODO(pOutClRetorno, pIdPeriodo, pXmlPreguntasPeriodo, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement InsertaCuestionarioPreguntasAbiertas(int? pIdPeriodo = null, string pClUsuario = null, string pNbPrograma = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_CUESTIONARIOS_PREGUNTAS_ABIERTAS(pOutClRetorno, pIdPeriodo, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_EO_PREGUNTAS_ABIERTAS_CUESTIONARIO_Result> ObtenerCuestionarioPreAbiertas(int? pIdEvaluador = null, int? pIdPeriodo = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EO_PREGUNTAS_ABIERTAS_CUESTIONARIO(pIdEvaluador, pIdPeriodo).ToList();
            }
        }

        public List<SPE_OBTIENE_EO_RESPUESTAS_PREGUNTAS_ABIERTAS_Result> ObtenerRespuestasAbiertas(int? pIdEvaluador = null, int? pIdPeriodo = null, int? pIdPregunta = null, string pXmlFiltros = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EO_RESPUESTAS_PREGUNTAS_ABIERTAS(pIdEvaluador, pIdPeriodo, pIdPregunta, pXmlFiltros).ToList();
            }
        }

        public XElement InsertaEvaluadoresFiltro(int? pIdPeriodo = null, string pXmlSeleccionados = null,string pClUsuario = null, string pNbPrograma = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_EVALUADOR_FILTRO_CLIMA_LAB(pOutClRetorno, pIdPeriodo, pXmlSeleccionados, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement InsertaFiltroClima(int? pIdPeriodo = null, string pXmlSeleccionados = null, string pClUsuario = null, string pNbPrograma = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_FILTRO_CLIMA_LAB(pOutClRetorno, pIdPeriodo, pXmlSeleccionados, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_EO_FILTROS_Result> ObtenerFiltrosEvaluadores(int? pIdPeriodo = null, int? pIdFiltro = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EO_FILTROS(pIdPeriodo, pIdFiltro).ToList();
            }
        }

        public List<SPE_OBTIENE_EO_PARAMETROS_FILTROS_Result> ObtenerParametrosFiltros(int? pIdPeriodo = null, int? pIdFiltro = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EO_PARAMETROS_FILTROS(pIdPeriodo, pIdFiltro).ToList();
            }
        }


        public List<SPE_OBTIENE_DATOS_EVALUADORES_CLIMA_Result> ObtenerValoresDatos(int? pIdPeriodo = null, int? pIdEvaluador= null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_DATOS_EVALUADORES_CLIMA(pIdPeriodo, pIdEvaluador).ToList();
            }
        }

        public XElement InsertaInstruccionesCuestionario(string pDsInstrucciones = null, int? pIdPeriodo = null, string pClUsuario = null, string pNbPrograma = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_INSTRUCCIONES_CUESTIONARIO(pOutClRetorno, pDsInstrucciones, pIdPeriodo, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }
        
    }
}
