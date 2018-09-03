using SIGE.AccesoDatos.Implementaciones.EvaluacionOrganizacional;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;
using System.Collections.Generic;
using System.Xml.Linq;
using SIGE.Entidades.EvaluacionOrganizacional;
using System.Web.Security;


namespace SIGE.Negocio.EvaluacionOrganizacional
{
    public class ClimaLaboralNegocio
    {
        public List<SPE_OBTIENE_EO_PERIODOS_CLIMA_Result> ObtienePeriodosClima(int? pIdPerido = null, string pClPeriodo = null, string pNbPeriodo = null, string pDsPeriodo = null, string pClEstadoPeriodo = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return oPeriodo.ObtenerPeriodosClima(pIdPerido, pClPeriodo, pNbPeriodo, pDsPeriodo, pClEstadoPeriodo);
        }

        public E_RESULTADO EliminaPeriodoClimaLaboral(int pIdPeriodo)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.EliminarPeriodoClimaLaboral(pIdPeriodo));
        }

        public E_RESULTADO InsertaActualizaPeriodoClima(E_PERIODO_CLIMA pPeriodo, string pCL_USUARIO, string pNB_PROGRAMA, string pTIPO_TRANSACCION)
        {
            ClimaLaboralOperaciones operaciones = new ClimaLaboralOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertarActualizarPeriodoClima(pPeriodo, pCL_USUARIO, pNB_PROGRAMA, pTIPO_TRANSACCION));
        }

        public E_RESULTADO InsertaPeriodoClimaCopia(E_PERIODO_CLIMA_COPIA pPeriodo, string pCL_USUARIO, string pNB_PROGRAMA)
        {
            ClimaLaboralOperaciones operaciones = new ClimaLaboralOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertarPeriodoClimaCopia(pPeriodo, pCL_USUARIO, pNB_PROGRAMA));
        }

        public E_RESULTADO InsertaActualizaEvaluadorClima(int pID_PERDIODO, string pXML_EVALUADORES, string pCL_USUARIO, string pNB_PROGRAMA, string pTIPO_TRANSACCION)
        {
            ClimaLaboralOperaciones operaciones = new ClimaLaboralOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertarActualizarEvaluadorClima(pID_PERDIODO, pXML_EVALUADORES, pCL_USUARIO, pNB_PROGRAMA, pTIPO_TRANSACCION));
        }

        public List<SPE_OBTIENE_EO_EVALUADORES_CLIMA_LAB_Result> ObtieneEvaluadoresClima(int? pID_PERIODO = null, int? pID_EMPLEADO = null, int? pID_PUESTO = null, int? pID_EVALUADOR = null, int? pID_ROL = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return oPeriodo.ObtenerEvaluadoresClima(pID_PERIODO, pID_EMPLEADO, pID_PUESTO, pID_EVALUADOR, pID_ROL);
        }

        public E_RESULTADO InsertaActualizaPreguntasPeriodo(int? pID_PERIODO, E_PREGUNTAS_PERIODO_CLIMA pPREGUNTAS_PERIODO, string pCL_USUARIO, string pPROGRAMA_APP, string pTIPO_TRANSACCION)
        {
            ClimaLaboralOperaciones operaciones = new ClimaLaboralOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertarActualizarPreguntasPeriodo(pID_PERIODO, pPREGUNTAS_PERIODO, pCL_USUARIO, pPROGRAMA_APP, pTIPO_TRANSACCION));
        }

        public E_RESULTADO InsertaActualizaDimension(string pNbDimension = null, string pClUsuario = null, string pNbPrograma = null, string pTipoTransaccion = null)
        {
            ClimaLaboralOperaciones operaciones = new ClimaLaboralOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualizaDimension(pNbDimension, pClUsuario, pNbPrograma, pTipoTransaccion));
        }

        public E_RESULTADO InsertaActualizaTema(string pClDimension = null, string pNbTema = null, string pClUsuario = null, string pNbPrograma = null, string pTipoTransaccion = null)
        {
            ClimaLaboralOperaciones operaciones = new ClimaLaboralOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualizaTema(pClDimension, pNbTema, pClUsuario, pNbPrograma, pTipoTransaccion));
        }

        public E_RESULTADO InsertaActualizaCuestionariosPeriodo(int? pID_PERIODO, string pCL_USUARIO, string pPROGRAMA_APP)
        {
            ClimaLaboralOperaciones operaciones = new ClimaLaboralOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertarActualizarCuestionariosPeriodo(pID_PERIODO, pCL_USUARIO, pPROGRAMA_APP));
        }

        public List<SPE_OBTIENE_EO_PREGUNTAS_PERIODO_Result> ObtienePreguntasPeriodo(int? pID_PERIODO = null, int? pID_PREGUNTA = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return oPeriodo.ObtenerPreguntasPeriodo(pID_PERIODO, pID_PREGUNTA);
        }

        public E_RESULTADO EliminaPreguntasPeriodo(string pCL_USUARIO, string pNB_PROGRAMA, string pXML_PREGUNTAS_PERIODO)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.EliminarPreguntasPeriodo(pCL_USUARIO, pNB_PROGRAMA, pXML_PREGUNTAS_PERIODO));
        }

        public E_RESULTADO EliminaEvaluadoresPeriodo(string pXML_EVALUADORES_PERIODO, string pCL_USUARIO, string pNB_PROGRAMA)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.EliminarEvaluadoresPeriodo(pXML_EVALUADORES_PERIODO, pCL_USUARIO, pNB_PROGRAMA));
        }

        public E_RESULTADO InsertarActualizarTokenEvaluadoresClima(int pIdPeriodo, int? pIdEvaluador, string pClUsuario, string pNbPrograma)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();

            XElement vXmlEvaluadores = new XElement("EVALUADORES");

            List<SPE_OBTIENE_EO_EVALUADORES_TOKEN_Result> vLstEvaluadores = new List<SPE_OBTIENE_EO_EVALUADORES_TOKEN_Result>();
            if (pIdEvaluador == null)
                vLstEvaluadores = oPeriodo.ObtenerTokenEvaluadoresClima(pIdPeriodo, null);
            else
                vLstEvaluadores.Add(new SPE_OBTIENE_EO_EVALUADORES_TOKEN_Result() { ID_EVALUADOR = pIdEvaluador ?? 0 });

            if (vLstEvaluadores.Count > 0)
                vLstEvaluadores.ForEach(f => vXmlEvaluadores.Add(new XElement("EVALUADOR", new XAttribute("ID_EVALUADOR", f.ID_EVALUADOR), new XAttribute("CL_TOKEN", Membership.GeneratePassword(12, 1)))));

            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertarActualizarTokenEvaluadoresClima(pIdPeriodo, vXmlEvaluadores, pClUsuario, pNbPrograma));
        }

        public List<SPE_OBTIENE_EO_PREGUNTA_PERIODO_Result> ObtienePreguntaPeriodo(int? pID_PERIODO = null, int? pID_PREGUNTA = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return oPeriodo.ObtenerPreguntaPeriodo(pID_PERIODO, pID_PREGUNTA);
        }

        public E_RESULTADO ActualizaCorreosEvaluadores(string pXML_EVALUADORES, string pCL_USUARIO, string pPROGRAMA_APP)
        {
            ClimaLaboralOperaciones operaciones = new ClimaLaboralOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.ActualizarCorreoEvaluadores(pXML_EVALUADORES, pCL_USUARIO, pPROGRAMA_APP));
        }


        public List<SPE_OBTIENE_EO_PREGUNTAS_CUESTIONARIO_Result> ObtieneCuestionario(int? pID_EVALUADOR = null, int? pID_PERIODO = null, System.Guid? pFlEvaluador = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return oPeriodo.ObtenerCuestionario(pID_EVALUADOR, pID_PERIODO, pFlEvaluador);
        }

        public E_RESULTADO ActualizaPreguntasCuestionario(string pXML_RESPUESTAS, string pXmlPreguntasAbiertas, bool pFG_CONTESTADO, int pID_EVALUADOR, string pCL_USUARIO, string pPROGRAMA_APP)
        {
            ClimaLaboralOperaciones operaciones = new ClimaLaboralOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.ActualizarPreguntasCuestionario(pXML_RESPUESTAS, pXmlPreguntasAbiertas, pFG_CONTESTADO, pID_EVALUADOR, pCL_USUARIO, pPROGRAMA_APP));
        }


        public E_RESULTADO InsertarCuestionarioConfiable(int pID_PERIODO, string pPIN_XML_RESPUESTAS, string pPIN_XML_PREGUNTAS_ABIERTAS, string pPIN_XML_DATOS_EVALUADOR, string pPIN_XML_DATOS_CAMPOS_EXTRA, string pClUsuario, string pNbPrograma)
        {
            ClimaLaboralOperaciones operaciones = new ClimaLaboralOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertarCuestionarioConfiable(pID_PERIODO, pPIN_XML_RESPUESTAS, pPIN_XML_PREGUNTAS_ABIERTAS, pPIN_XML_DATOS_EVALUADOR, pPIN_XML_DATOS_CAMPOS_EXTRA, pClUsuario, pNbPrograma));
        }

        public List<SPE_OBTIENE_CONTROL_AVANCE_EO_DATOS_GRAFICA_Result> ObtieneControlAvance(int? pID_PERIODO = null, int? pID_ROL = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return oPeriodo.ObtenerControlAvance(pID_PERIODO, pID_ROL);
        }

        public List<SPE_OBTIENE_EO_EVALUADORES_CUESTIONARIO_Result> ObtieneEvaluadoresCuestionario(int? pID_PERIODO = null, int? pID_EMPLEADO = null, int? pID_PUESTO = null, int? pID_EVALUADOR = null, int? pIdRol = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return oPeriodo.ObtenerEvaluadoresCuestionario(pID_PERIODO, pID_EMPLEADO, pID_PUESTO, pID_EVALUADOR, pIdRol);
        }

        public List<SPE_OBTIENE_EO_GRAFICA_INDICE_DIMENSION_Result> ObtieneGraficaDimension(int? pID_PERIODO = null, XElement pXML_FILTROS = null, int? pIdRol = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return oPeriodo.ObtenerGraficaDimension(pID_PERIODO, pXML_FILTROS, pIdRol);
        }

        public List<SPE_OBTIENE_EO_GRAFICA_INDICE_TEMA_Result> ObtieneGraficaTema(int? pID_PERIODO = null, XElement pXML_FILTROS = null, int? pIdRol = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return oPeriodo.ObtenerGraficaTema(pID_PERIODO, pXML_FILTROS, pIdRol);
        }

        public List<SPE_OBTIENE_EO_GRAFICA_INDICE_PREGUNTA_Result> ObtieneGraficaPregunta(int? pID_PERIODO = null, XElement pXML_FILTROS = null, int? pIdRol = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return oPeriodo.ObtenerGraficaPregunta(pID_PERIODO, pXML_FILTROS, pIdRol);
        }

        public List<SPE_OBTIENE_EO_GRAFICA_DISTRIBUCION_DIMENSION_Result> ObtieneGraficaDistribucionDimension(int? pID_PERIODO = null, string pNB_DIMENSION = null, XElement pXML_FILTROS = null, int? pIdRol = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return oPeriodo.ObtenerGraficaDistribucionDimension(pID_PERIODO, pNB_DIMENSION, pXML_FILTROS, pIdRol);
        }

        public List<SPE_OBTIENE_EO_GRAFICA_DISTRIBUCION_TEMA_Result> ObtieneGraficaDistribucionTema(int? pID_PERIODO = null, string pNB_TEMA = null, XElement pXML_FILTROS = null, int? pIdRol = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return oPeriodo.ObtenerGraficaDistribucionTema(pID_PERIODO, pNB_TEMA, pXML_FILTROS, pIdRol);
        }

        public List<SPE_OBTIENE_EO_GRAFICA_DISTRIBUCION_PREGUNTA_Result> ObtieneGraficaDistribucionPregunta(int? pID_PERIODO = null, string pNB_PREGUNTA = null, string pXML_FILTROS = null, int? pIdRol = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return oPeriodo.ObtenerGraficaDistribucionPregunta(pID_PERIODO, pNB_PREGUNTA, pXML_FILTROS, pIdRol);
        }

        public List<SPE_OBTIENE_DIMENSIONES_Result> ObtieneDimensiones(int? pID_DIMENSION = null, string pCL_DIMENSION = null, string pNB_DIMENSION = null, int? pID_PERIODO = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return oPeriodo.ObtenerDimensiones(pID_DIMENSION, pCL_DIMENSION, pNB_DIMENSION, pID_PERIODO);
        }

        public List<SPE_OBTIENE_TEMAS_Result> ObtieneTemas(string pCL_TEMA = null, string pNB_TEMA = null, int? pID_PERIODO = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return oPeriodo.ObtenerTemas(pCL_TEMA, pNB_TEMA, pID_PERIODO);
        }

        public List<SPE_OBTIENE_EO_DIMENSIONES_Result> ObtieneCatalogoDimensiones(int? pIdDimension = null, string pClDimension = null, string pNbDimension = null)
        {
            ClimaLaboralOperaciones oClima = new ClimaLaboralOperaciones();
            return oClima.ObtenerCatalogoDimensiones(pIdDimension, pClDimension, pNbDimension);
        }

        public List<SPE_OBTIENE_EO_TEMAS_Result> ObtenerCatalogoTemas(string pClTema = null, string pNbTema = null, int? pIdTema = null, int? pIdDimension= null)
        {
            ClimaLaboralOperaciones oClima = new ClimaLaboralOperaciones();
            return oClima.ObtenerCatalogoTemas(pClTema, pNbTema, pIdTema, pIdDimension);
        }

        public List<SPE_OBTIENE_PREGUNTAS_Result> ObtienePreguntas(int? pID_PREGUNTA = null, string pCL_PREGUNTA_MODULO = null, int? pID_PERIODO = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return oPeriodo.ObtenerPreguntas(pID_PREGUNTA, pCL_PREGUNTA_MODULO, pID_PERIODO);
        }

        public List<SPE_OBTIENE_EO_GRAFICA_GLOBAL_Result> ObtieneGraficaGlobal(int? pID_PERIODO = null, int? pID_ROL = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return oPeriodo.ObtenerGraficaGlobal(pID_PERIODO, pID_ROL);
        }

        public SPE_OBTIENE_EO_PERIODO_EVALUADOR_Result ObtenerPeriodoEvaluado(int? pID_EVALUADOR = null, System.Guid? pFL_EVALUADOR = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return oPeriodo.ObtenerPeriodoEvaluador(pID_EVALUADOR, pFL_EVALUADOR);
        }

        public E_RESULTADO InsertaActualizaPreguntasAbiertas(int? pIdPeriodo = null, int? pIdPregunta = null, string pNbPregunta = null, string pDsPregunta = null, string pClUsuario = null, string pNbPrograma = null, string pTipoTransaccion = null)
        {
            ClimaLaboralOperaciones operaciones = new ClimaLaboralOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualizaPreguntasAbiertas(pIdPeriodo, pIdPregunta, pNbPregunta, pDsPregunta, pClUsuario, pNbPrograma, pTipoTransaccion));
        }

        public List<SPE_OBTIENE_EO_PREGUNTAS_ABIERTAS_PERIODO_Result> ObtenerPreguntasAbiertas(int? pIdPeriodo = null, int? pIdPregunta = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return oPeriodo.ObtenerPreguntasAbiertas(pIdPeriodo, pIdPregunta);
        }

        public E_RESULTADO EliminarPreguntasAbiertas(int? pIdPeriodo = null, string pXmlPreguntasPeriodo = null, string pClUsuario = null , string pNbPrograma = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.EliminarPreguntasAbiertas(pIdPeriodo, pXmlPreguntasPeriodo, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO InsertaCuestionarioPreguntasAbiertas(int? pIdPeriodo = null, string pClUsuario = null, string pNbPrograma = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertaCuestionarioPreguntasAbiertas(pIdPeriodo, pClUsuario, pNbPrograma));
        }

        public List<SPE_OBTIENE_EO_PREGUNTAS_ABIERTAS_CUESTIONARIO_Result> ObtenerCuestionarioPreAbiertas(int? pID_EVALUADOR = null, int? pID_PERIODO = null, System.Guid? pFlEvaluador = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return oPeriodo.ObtenerCuestionarioPreAbiertas(pID_EVALUADOR, pID_PERIODO, pFlEvaluador);
        }

        public List<SPE_OBTIENE_EO_RESPUESTAS_PREGUNTAS_ABIERTAS_Result> ObtenerRespuestasAbiertas(int? pIdEvaluador = null, int? pIdPeriodo = null, int? pIdPregunta = null, string pXmlFiltros = null, int? pIdRol = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return oPeriodo.ObtenerRespuestasAbiertas(pIdEvaluador, pIdPeriodo, pIdPregunta, pXmlFiltros, pIdRol);
        }

        public E_RESULTADO InsertaEvaluadoresFiltro(int? pIdPeriodo = null, string pXmlSeleccionados = null, string pClUsuario = null, string pNbPrograma = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertaEvaluadoresFiltro(pIdPeriodo, pXmlSeleccionados, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO InsertaFiltroClima(int? pIdPeriodo = null, string pXmlSeleccionados = null, string pClUsuario = null, string pNbPrograma = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertaFiltroClima(pIdPeriodo, pXmlSeleccionados, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO ActualizaValidezCuestionario(int? pIdPeriodo = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.ActualizaValidezCuestionario(pIdPeriodo));
        }
        

        public List<SPE_OBTIENE_EO_FILTROS_Result> ObtenerFiltrosEvaluadores(int? pIdPeriodo = null, int? pIdFiltro = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return oPeriodo.ObtenerFiltrosEvaluadores(pIdPeriodo, pIdFiltro);
        }

        public List<SPE_OBTIENE_EO_PARAMETROS_FILTROS_Result> ObtenerParametrosFiltros(int? pIdPeriodo = null, int? pIdFiltro = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return oPeriodo.ObtenerParametrosFiltros(pIdPeriodo, pIdFiltro);
        }

        public List<SPE_OBTIENE_DATOS_EVALUADORES_CLIMA_Result> ObtenerValoresDatos(int? pIdPeriodo = null, int? pIdEvaluador = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return oPeriodo.ObtenerValoresDatos(pIdPeriodo, pIdEvaluador);
        }

        public E_RESULTADO InsertaInstruccionesCuestionario(string pDsInstrucciones = null, int? pIdPeriodo = null, string pClUsuario = null, string pNbPrograma = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertaInstruccionesCuestionario(pDsInstrucciones, pIdPeriodo, pClUsuario, pNbPrograma));
        }

         public E_RESULTADO EliminaRespuestasCuestionario(int? pID_PERIODO = null, int? pID_EVALUADOR = null, int? pID_CUESTIONARIO = null, string pClUsuario = null, string pNbPrograma = null)
        {
            ClimaLaboralOperaciones oPeriodo = new ClimaLaboralOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.EliminaRespuestasCuestionario(pID_PERIODO, pID_EVALUADOR, pID_CUESTIONARIO, pClUsuario, pNbPrograma));
        }

    }
}
