using SIGE.Entidades;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Entidades.Externas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.EvaluacionOrganizacional
{
    public class PeriodoDesempenoOperaciones
    {
        public List<SPE_OBTIENE_EO_PERIODOS_DESEMPENO_Result> ObtenerPeriodosDesempeno(int? pIdPeriodo = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_EO_PERIODOS_DESEMPENO(pIdPeriodo).ToList();
            }
        }
        public List<SPE_OBTIENE_EO_PERIODOS_DESEMPENO_CUESTIONARIO_Result> ObtienePeriodosDesempenoCuestionario(int? pIdPeriodo = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_EO_PERIODOS_DESEMPENO_CUESTIONARIO(pIdPeriodo).ToList();
            }
        }

        public List<SPE_OBTIENE_PERIODOS_DESEMPENO_COMPARACION_Result> ObtenerPeriodosComparacion(int? pIdPeriodo = null, int? pIdEvaluado = null, string pClTipoSeleccion = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_PERIODOS_DESEMPENO_COMPARACION(pIdPeriodo, pIdEvaluado, pClTipoSeleccion).ToList();
            }
        }

        public List<SPE_OBTIENE_EO_CUMPLIMIENTO_GLOBAL_DESEMPENO_Result> ObtenerCumplimientoGlobal(int? pIdPeriodo = null, int? pIdRol = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_EO_CUMPLIMIENTO_GLOBAL_DESEMPENO(pIdPeriodo, pIdRol).ToList();
            }
        }

        public List<SPE_OBTIENE_EO_CUMPLIMIENTO_GLOBAL_GRAFICA_Result> ObtenerCumplimientoGlobalGrafica(string pXmlPeriodos = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_EO_CUMPLIMIENTO_GLOBAL_GRAFICA(pXmlPeriodos).ToList();
            }
        }

        public List<SPE_VERIFICA_CONFIGURACION_METAS_Result> VerificaConfiguracion(int? pIdPeriodo = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_VERIFICA_CONFIGURACION_METAS(pIdPeriodo).ToList();
            }
        }

        public SPE_OBTIENE_EO_PERIODOS_DESEMPENO_Result ObtenerPeriodoDesempeno(int pIdPeriodo)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_EO_PERIODOS_DESEMPENO(pIdPeriodo).FirstOrDefault();
            }
        }


        public List<SPE_OBTIENE_EO_EVALUADORES_REPLICAS_Result> ObtenerEvaluadoresReplicas(int pIdPeriodo)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_EO_EVALUADORES_REPLICAS(pIdPeriodo).ToList();
            }
        }

        public List<SPE_OBTIENE_SOLICITUDES_ENVIAR_Result> ObtenerSolicitudesEnviar()
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_SOLICITUDES_ENVIAR().ToList();
            }
        }

        public List<SPE_OBTIENE_PERIODOS_SOLICITUDES_ENVIAR_Result> ObtenerPeriodosEnviar()
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_PERIODOS_SOLICITUDES_ENVIAR().ToList();
            }
        }


        public List<SPE_OBTIENE_EO_PERIODOS_CONSECUTIVOS_Result> ObtenerPeriodoConsecutivo(int? pIdPeriodo = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_EO_PERIODOS_CONSECUTIVOS(pIdPeriodo).ToList();
            }
        }

        public List<SPE_OBTIENE_EO_METAS_EVALUADOS_CONSECUENTES_Result> ObtenerMetasConsecuentes(int? pIdPeriodoOriginal = null, int? pIdPeriodoConsecuente = null, int? pIdEvaOriginal = null, int? pIdEvaConsecuente = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_EO_METAS_EVALUADOS_CONSECUENTES(pIdPeriodoOriginal, pIdPeriodoConsecuente, pIdEvaOriginal, pIdEvaConsecuente).ToList();
            }
        }

        public List<SPE_OBTIENE_METAS_COMPARACION_GRAFICA_Result> ObtenerMetasGrafica(string pXmlPeriodos = null, int? pIdEmpleado = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_METAS_COMPARACION_GRAFICA(pXmlPeriodos, pIdEmpleado).ToList();
            }
        }

        public List<SPE_OBTIENE_EVALUADOS_PERIODOS_DESEMPENO_Result> ObtenerEvaluadosDesempeno(string pXmlPeriodos = null, int? pIdRol = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_EVALUADOS_PERIODOS_DESEMPENO(pXmlPeriodos, pIdRol).ToList();
            }
        }

        public List<SPE_OBTIENE_BONOS_EVALUADOS_DESEMPENO_Result> ObtenerBonosDesempeno(int? pIdEmpledo = null, string pXmlPeriodos = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_BONOS_EVALUADOS_DESEMPENO(pXmlPeriodos, pIdEmpledo).ToList();
            }
        }


        public List<SPE_OBTIENE_PERIODO_REPLICAS_Result> ObtenerPeriodos(int? pIdPeriodo = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_PERIODO_REPLICAS(pIdPeriodo).ToList();
            }
        }

        public SPE_OBTIENE_EO_CONTEXTO_METAS_Result ObtenerPeriodoDesempenoContexto(int pIdPeriodo, int? idEvaluado)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_EO_CONTEXTO_METAS(pIdPeriodo, idEvaluado).FirstOrDefault();
            }
        }
        public XElement InsertaActualiza_PERIODO_DESEMPENO(int? pIdPeriodoDesempeno, string pClPeriodoDesempeno, string pNbPeriodoDesempeno, string pDsPeriodoDesempeno, string pClEstadoPeriodoDesempeno, string pDsNotas, DateTime pFeInicio, DateTime pFeTermino, string pClTipoCapturista, string CL_TIPO_META, string pClUsuario, string pNbPrograma, string pTipoTransaccion, bool? pFgCapturaMasiva)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                // Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_ACTUALIZA_PERIODO_DESEMPENO(pout_clave_retorno, pIdPeriodoDesempeno, pClPeriodoDesempeno, pNbPeriodoDesempeno, pDsPeriodoDesempeno, pClEstadoPeriodoDesempeno, pDsNotas, pFeInicio, pFeTermino, pClTipoCapturista, CL_TIPO_META, pClUsuario, pNbPrograma, pTipoTransaccion, pFgCapturaMasiva);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pout_clave_retorno.Value.ToString());

            }
        }

        public XElement InsertaFeEnvioSolicitud(string pXmlFechas, string pClUsuario, string pNbPrograma, string pTipoTransaccion)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_FECHA_ENVIA_SOLICITUD(pOutClRetorno, pXmlFechas, pClUsuario, pNbPrograma, pTipoTransaccion);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }


        public XElement EliminarPeriodosDesempeno(int pIdPeriodo)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ELIMINA_PERIODO_DESEMPENO(pOutClRetorno, pIdPeriodo);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement InsertaPeriodosReplica(int? pIdPeriodo, string pXmlPeriodos, string pClUsuario, string pNbPrograma, string ClTipoTransaccion)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_PERIODOS_DESEMPENO_REPLICA(pOutClRetorno, pIdPeriodo, pXmlPeriodos, pClUsuario, pNbPrograma, ClTipoTransaccion);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_EO_EVALUADOS_CONFIGURACION_DESEMPENO_Result> ObtenerEvaluados(int? pIdPeriodo = null, int? pIdEvaluado = null, int? pIdEvaluador = null, string pClUsuario = null, string pNbPrograma = null, int? pIdRol = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_EO_EVALUADOS_CONFIGURACION_DESEMPENO(pIdPeriodo, pIdEvaluado, pIdEvaluador, pClUsuario, pNbPrograma, pIdRol).ToList();
            }
        }

        public List<SPE_OBTIENE_PERIODOS_DESEMPENO_COMPARAR_Result> ObtenerDesempenoComparacion(string vXmlPeriodos = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_PERIODOS_DESEMPENO_COMPARAR(vXmlPeriodos).ToList();
            }
        }

        public XElement EliminarEvaluados(int pIdPeriodo, XElement pXmlEvaluados, string pClUsuario, string pNbPrograma)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ELIMINA_EO_EVALUADOS_DESEMPENO(pOutClRetorno, pIdPeriodo, pXmlEvaluados.ToString(), pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement InsertarEvaluados(int pIdPeriodo, XElement pXmlEvaluados, string pClUsuario, string pNbPrograma)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_ACTUALIZA_EVALUADOS_DESEMPENO(pOutClRetorno, pIdPeriodo, pXmlEvaluados.ToString(), pClUsuario, pNbPrograma, "I");
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement ActualizarConfiguracionDesempeno(int pIdPeriodoDesempeno, int pFgBono, decimal pPrBono, decimal pMnBono, string pClTipoBono, string pClUsuario, string pNbPrograma)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClretorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ACTUALIZA_CONFIGURACION_PERIODO_DESEMPENO(pOutClretorno, pIdPeriodoDesempeno, pFgBono, pPrBono, pMnBono, pClTipoBono, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClretorno.Value.ToString());
            }
        }


        public List<SPE_OBTIENE_BONO_EVALUADOS_Result> ObtenerBonoEvaluados(int pIdPeriodo, int? pIdRol)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_BONO_EVALUADOS(pIdPeriodo, pIdRol).ToList();
            }
        }

        public XElement ActualizarEvaluadoTopeBono(int pIdPeriodo, decimal pPrBono, string pClTipoBono, string pXmlEvaluado, string pNbPrograma, string pClUsuario)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ACTUALIZA_EVALUADO_TOPE_BONO(pOutClRetorno, pIdPeriodo, pPrBono, pClTipoBono, pXmlEvaluado, pNbPrograma, pClUsuario);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public List<E_META> ObtenerMetas(int? pIdPeriodo = null, int? pIdEvaluado = null, int? pIdEvaluadoMeta = null, int? pNoMeta = null, bool? pFgEvaluar = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                var oLista = contexto.SPE_OBTIENE_METAS(pIdPeriodo, pIdEvaluado, pIdEvaluadoMeta, pNoMeta, pFgEvaluar).ToList();

                List<E_META> oListaMetas = oLista.Select(t => new E_META
                {
                    // ID_EVALUADO_FUNCION = t.ID_EVALUADO_FUNCION,
                    // NB_FUNCION = t.NB_FUNCION,
                    ID_EVALUADO = t.ID_EVALUADO,
                    ID_EVALUADO_META = t.ID_EVALUADO_META,
                    NB_INDICADOR = t.NB_INDICADOR,
                    NO_META = t.NO_META.ToString(),
                    DS_META = t.DS_META,
                    DS_FUNCION = t.DS_FUNCION,
                    CL_TIPO_META = t.CL_TIPO_META,
                    FG_EVALUAR = t.FG_EVALUAR,
                    FG_VALIDA_CUMPLIMIENTO = t.FG_VALIDA_CUMPLIMIENTO,
                    NB_CUMPLIMIENTO_ACTUAL = t.NB_CUMPLIMIENTO_ACTUAL,
                    NB_CUMPLIMIENTO_MINIMO = t.NB_CUMPLIMIENTO_MINIMO,
                    NB_CUMPLIMIENTO_SATISFACTORIO = t.NB_CUMPLIMIENTO_SATISFACTORIO,
                    NB_CUMPLIMIENTO_SOBRESALIENTE = t.NB_CUMPLIMIENTO_SOBRESALIENTE,
                    PR_META = t.PR_META,
                    PR_RESULTADO = t.PR_RESULTADO
                }).ToList();

                return oListaMetas;
            }
        }

        public XElement ActualizarMetasEvaluado(string pClTipoMetas, int pIdPeriodo, string xmlEmpleados, string pClUsuario, string pNbPrograma)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutclretorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ACTUALIZA_EO_METAS_EVALUADO(pOutclretorno, pClTipoMetas, xmlEmpleados, pIdPeriodo, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutclretorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_EO_FUNCIONES_METAS_Result> ObtenerFuncionesMetas(int? pIdEvaluado = null, int? pIdPeriodo = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_EO_FUNCIONES_METAS(pIdEvaluado, pIdPeriodo).ToList();
            }
        }
        //**************************************************************************************
        public List<SPE_OBTIENE_IDICADORES_METAS_Result> ObtenerIndicadoresMetas(int? pIdPeriodo = null, int? pIdEvaluado = null, string pDsFuncion = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_IDICADORES_METAS(pIdPeriodo, pIdEvaluado, pDsFuncion).ToList();
            }
        }
        //*******************************************************************************************
        public XElement InsetarActualizarMetasEvaluados(int? pIdMetaEvaluado = null, int? pIdPeriodo = null, int? pIdEvaluado = null, string pDsFuncion = null, int? pNoMeta = null, string pNbIndicador = null, string pDsMeta = null, string pClTipoMeta = null, bool? pFgValidaCumplimiento = null, bool? pFgEvaluar = null, string pNbCumplimientoActual = null, string pNbCumplimientoMinimo = null, string pNbCumplimientoSatisfactorio = null, string pNbCumplimientoSobresaliente = null, decimal? pPrMeta = null, decimal? pPrResultado = null, int? pClNivel = null, decimal? pPrCumplimientoMeta = null, string pClUsuario = null, string pNbPrograma = null, string pTipoTransaccion = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_ACTUALIZA_META_EVALUADO(pOutClRetorno, pIdMetaEvaluado, pIdPeriodo, pIdEvaluado, pDsFuncion, pNoMeta, pNbIndicador, pDsMeta, pClTipoMeta, pFgValidaCumplimiento, pFgEvaluar, pNbCumplimientoActual, pNbCumplimientoMinimo, pNbCumplimientoSatisfactorio, pNbCumplimientoSobresaliente, pPrMeta, pPrResultado, pClNivel, pPrCumplimientoMeta, pClUsuario, pNbPrograma, pTipoTransaccion);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement ActualizaPonderacionPuesto(decimal? PrEvaluado, int? pIdPeriodoDesempeno, int? pIdEvaluado, string pClUsuario, string pNbPrograma)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                // Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ACTUALIZA_EO_PONDERACION_PUESTO(pout_clave_retorno, PrEvaluado, pIdPeriodoDesempeno, pIdEvaluado, pClUsuario, pNbPrograma);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pout_clave_retorno.Value.ToString());

            }
        }

        public List<SPE_OBTIENE_EO_EVALUADORES_TOKEN> ObtenerEvaluadores(int pIdPeriodo, int? pIdRol)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<SPE_OBTIENE_EO_EVALUADORES_TOKEN>("EXEC " +
                "EO.SPE_OBTIENE_EO_EVALUADORES_TOKEN " +
                "@PIN_ID_PERIODO, " +
                "@PIN_ID_ROL",
                new SqlParameter("@PIN_ID_PERIODO", (object)pIdPeriodo ?? DBNull.Value),
                new SqlParameter("@PIN_ID_ROL", (object)pIdRol ?? DBNull.Value)
            ).ToList();
            }
            //using (contexto = new SistemaSigeinEntities())
            //{
            //    return contexto.SPE_OBTIENE_EO_EVALUADORES_TOKEN(pIdPeriodo, pIdRol).ToList();
            //}
        }

        public List<SPE_OBTIENE_EO_METAS_EVALUADOS_Result> ObtenerMetasEvaluados(int? idEvaluadoMeta = null, int? pIdPeriodo = null, int? idEvaluado = null, int? no_Meta = null, string cl_nivel = null, bool? FgEvaluar = null, int? pIdEmpleado = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                //return contexto.SPE_OBTIENE_EO_METAS_EVALUADOS(idEvaluadoMeta, pIdPeriodo, idEvaluado, no_Meta, cl_nivel).ToList();
                return contexto.SPE_OBTIENE_EO_METAS_EVALUADOS(idEvaluadoMeta, pIdPeriodo, idEvaluado, no_Meta, cl_nivel, FgEvaluar, pIdEmpleado).ToList();
            }
        }


        public List<SPE_OBTIENE_EO_METAS_CAPTURA_MASIVA_Result> ObtieneMetasCapturaMasiva(int? pIdPeriodo = null, int? idEvaluador = null, System.Guid? pFlEvaluador = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_EO_METAS_CAPTURA_MASIVA(pIdPeriodo, idEvaluador, pFlEvaluador).ToList();
            }
        }

        public List<SPE_OBTIENE_METAS_PERIODOS_COMPARACION_Result> ObtieneMetasComparacion(int? idEvaluadoMeta = null, int? pIdPeriodo = null, int? idEvaluado = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_METAS_PERIODOS_COMPARACION(idEvaluadoMeta, pIdPeriodo, idEvaluado).ToList();
            }
        }

        public List<SPE_OBTIENE_METAS_COMPARAR_DESEMPENO_Result> ObtieneMetasPeriodoComparar(string pXmlPeriodos = null, int? idEvaluado = null, int? pIdPeriodo = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_METAS_COMPARAR_DESEMPENO(pXmlPeriodos, idEvaluado, pIdPeriodo).ToList();
            }
        }

        public List<SPE_OBTIENE_CONFIGURACION_PERIODO_REPLICAS_Result> ObtieneConfiguracionEnvio(int? pIdPeriodo = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_CONFIGURACION_PERIODO_REPLICAS(pIdPeriodo).ToList();
            }
        }


        public SPE_OBTIENE_EO_PERIODO_EVALUADOR_DESEMPENO_Result ObtenerPeriodoEvaluadorDesempeno(int? pID_EVALUADOR = null, Guid? pFL_EVALUADOR = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_EO_PERIODO_EVALUADOR_DESEMPENO(pID_EVALUADOR, pFL_EVALUADOR).FirstOrDefault();
            }
        }

        public XElement ActualizarResultadosMetas(int pIdPeriodo, int pIdEvaluado, XElement xmlResultados, string pClUsuario, string pNbPrograma, decimal pSuma)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ACTUALIZA_RESULTADO_METAS(pOutClRetorno, pIdPeriodo, pIdEvaluado, xmlResultados.ToString(), pClUsuario, pNbPrograma, pSuma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement ActualizarResultadosMetasMasiva(int pIdPeriodo, int pIdEvaluador, XElement xmlResultados, string pClUsuario, string pNbPrograma, decimal pSuma)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ACTUALIZA_RESULTADO_METAS_MASIVA(pOutClRetorno, pIdPeriodo, pIdEvaluador, xmlResultados.ToString(), pClUsuario, pNbPrograma, pSuma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public SPE_OBTIENE_EVIDENCIAS_METAS_Result ObtenerEvidenciasMetasEvaluados(int? idEvaluadoMeta = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_EVIDENCIAS_METAS(idEvaluadoMeta).FirstOrDefault();
            }
        }

        public XElement InsertarActualizarEvidenciasMetas(int? pIsEvaluadoMeta, List<UDTT_ARCHIVO> pLstArchivosTemporales, List<E_DOCUMENTO> pLstDocumentos, string pClUsuario, string pNbPrograma, int? pIdEvaluador)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {

                var pXmlResultado = new SqlParameter("@XML_RESULTADO", SqlDbType.Xml)
                {
                    Direction = ParameterDirection.Output
                };

                XElement vXmlDocumentos = new XElement("DOCUMENTOS");
                pLstDocumentos.ForEach(s => vXmlDocumentos.Add(new XElement("DOCUMENTO",
                    new XAttribute("ID_ITEM", s.ID_ITEM),
                    new XAttribute("ID_DOCUMENTO", s.ID_DOCUMENTO ?? 0),
                    new XAttribute("ID_ARCHIVO", s.ID_ARCHIVO ?? 0),
                    new XAttribute("NB_DOCUMENTO", s.NB_DOCUMENTO),
                    new XAttribute("CL_TIPO_DOCUMENTO", s.CL_TIPO_DOCUMENTO),
                    new XAttribute("ID_PROCEDENCIA", pIsEvaluadoMeta),
                    new XAttribute("CL_PROCEDENCIA", "META"))

                ));

                List<UDTT_ARCHIVO> vLstArchivos = pLstArchivosTemporales;

                DataTable dtArchivos = new DataTable();
                dtArchivos.Columns.Add(new DataColumn("ID_ITEM", typeof(SqlGuid)));
                dtArchivos.Columns.Add(new DataColumn("ID_ARCHIVO", typeof(int)));
                dtArchivos.Columns.Add(new DataColumn("NB_ARCHIVO"));
                dtArchivos.Columns.Add(new DataColumn("FI_ARCHIVO", typeof(SqlBinary)));

                vLstArchivos.ForEach(f => dtArchivos.Rows.Add(f.ID_ITEM, f.ID_ARCHIVO ?? 0, f.NB_ARCHIVO, f.FI_ARCHIVO));

                var pArchivos = new SqlParameter("@PIN_ARCHIVOS", SqlDbType.Structured);
                pArchivos.Value = dtArchivos;
                pArchivos.TypeName = "ADM.UDTT_ARCHIVO";

                contexto.Database.ExecuteSqlCommand("EXEC " +
                    "EO.SPE_INSERTA_ACTUALIZA_EVIDENCIAS_METAS " +
                    " @XML_RESULTADO OUTPUT, " +
                    " @PIN_XML_DOCUMENTOS, " +
                    " @PIN_ID_EVALUADO_META, " +
                    " @PIN_ARCHIVOS, " +
                    " @PIN_CL_USUARIO, " +
                    " @PIN_NB_PROGRAMA, " +
                    " @PIN_ID_EVALUADOR"
                    , pXmlResultado
                    , new SqlParameter("@PIN_XML_DOCUMENTOS", SqlDbType.Xml) { Value = new SqlXml(vXmlDocumentos.CreateReader()) }
                    , new SqlParameter("@PIN_ID_EVALUADO_META", (int)pIsEvaluadoMeta)
                    , pArchivos
                    , new SqlParameter("@PIN_CL_USUARIO", pClUsuario)
                    , new SqlParameter("@PIN_NB_PROGRAMA", pNbPrograma)
                    , new SqlParameter("@PIN_ID_EVALUADOR", pIdEvaluador == null ? 0 : pIdEvaluador)
                );

                return XElement.Parse(pXmlResultado.Value.ToString());

            }
        }

        public XElement ActualizaPonderacionEvaluados(int? pIdPeriodoDesempeno, string pXmlEvaluados, string tipoActualizacion, string pClUsuario, string pNbPrograma)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                // Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ACTUALIZA_PONDERACION_EVALUADOS(pout_clave_retorno, pIdPeriodoDesempeno, pXmlEvaluados, tipoActualizacion, pClUsuario, pNbPrograma);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pout_clave_retorno.Value.ToString());

            }
        }

        public List<SPE_PONDERACION_METAS_DESEMPENO_Result> ObtenerPonderacionMetas(int pIdPeriodo)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_PONDERACION_METAS_DESEMPENO(pIdPeriodo).ToList();
            }
        }

        public XElement InsertarEvaluadorOtro(int pIdPeriodo, XElement pXmlEvaluados, XElement pXmlEvaluadores, string pClUsuario, string pNbPrograma)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_EVALUADOR_OTRO(pOutClRetorno, pIdPeriodo, pXmlEvaluados.ToString(), pXmlEvaluadores.ToString(), pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public List<SPE_EVALUADOR_POR_EVALUADO_Result> ObtenerEvaludoresPorEvaluador(int? pIdPeriodo = null, int? pId_Evaluado = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_EVALUADOR_POR_EVALUADO(pIdPeriodo, pId_Evaluado).ToList();
            }
        }

        public XElement EliminarEvaluadoresPorEvaluadorEvaluado(int pIdPeriodo, XElement pXmlEvaluadorEvaluador, string pClUsuario, string pNbPrograma)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ELIMINA_EO_EVALUADOR_EVALUADO(pOutClRetorno, pIdPeriodo, pXmlEvaluadorEvaluador.ToString(), pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement EliminarMetaEvaluado(int pIdPeriodo, int pIdMetaEvaluado, int pIdEvaluado, string pClUsuario, string pNbPrograma)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ELIMINA_EO_METAS_EVALUADO(pOutClRetorno, pIdPeriodo, pIdMetaEvaluado, pIdEvaluado, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement EliminarMetasInactivas(int pIdPeriodo, string pClUsuario, string pNbPrograma)
        {
            using (EvaluacionOrganizacionalEntities contexto = new EvaluacionOrganizacionalEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ELIMINA_EO_METAS_INACTIVAS(pOutClRetorno, pIdPeriodo, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement InsertarPeriodoDesempenoCopia(E_PERIODO_DESEMPENO pPeriodo, string pCL_USUARIO, string pNB_PROGRAMA)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_PERIODO_DESEMPENO_COPIA(poutClaveRetorno, pPeriodo.ID_PERIODO, pPeriodo.CL_TIPO_PERIODO, pPeriodo.NB_PERIODO, pPeriodo.DS_PERIODO, pPeriodo.CL_ESTADO, pPeriodo.XML_DS_NOTAS, pPeriodo.FE_INICIO_PERIODO, pPeriodo.FE_TERMINO_PERIODO, pPeriodo.CL_TIPO_CAPTURISTA, pPeriodo.CL_TIPO_COPIA, pCL_USUARIO, pNB_PROGRAMA);
                return XElement.Parse(poutClaveRetorno.Value.ToString());
            }
        }

        public XElement InsertarPeriodoDesempenoReplica(int? pIdPeriodo = null, DateTime? pFeInicio = null, DateTime? pFeFin = null, string pCL_USUARIO = null, string pNB_PROGRAMA = null, string pTipoTransaccion = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_PERIODO_DESEMPENO_REPLICA(poutClaveRetorno, pIdPeriodo, pFeInicio, pFeFin, pCL_USUARIO, pNB_PROGRAMA, pTipoTransaccion);
                return XElement.Parse(poutClaveRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_PERIODOS_DESEMPENO_REPLICA_Result> ObtenerPeriodosReplicados(int? pIdPeriodo = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_PERIODOS_DESEMPENO_REPLICA(pIdPeriodo).ToList();
            }
        }

        public List<SPE_VALIDA_PERIODO_DESEMPENO_Result> ValidarPeriodoDesempeno(int? pIdPeriodo = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_VALIDA_PERIODO_DESEMPENO(pIdPeriodo).ToList();
            }
        }

        public List<SPE_OBTIENE_CONTROL_AVANCE_DESEMPENO_Result> ObtenerControlAvanceDesempeno(int pIdPeriodoDesempeno, int? pIdRol)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_CONTROL_AVANCE_DESEMPENO(pIdPeriodoDesempeno, pIdRol).ToList();
            }
        }

        public XElement InsertarActualizarBono(int? pIdPeriodo = null, string pCL_USUARIO = null, string pNB_PROGRAMA = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_ACTUALIZA_BONO(poutClaveRetorno, pIdPeriodo, pCL_USUARIO, pNB_PROGRAMA);
                return XElement.Parse(poutClaveRetorno.Value.ToString());
            }
        }



        public XElement ActualizarEvaluadoMetas(string pXmlMetasEvaluado = null, string pClUsuario = null, string pNbPrograma = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ACTUALIZA_ESTATUS_EVALUADO_METAS(poutClaveRetorno, pXmlMetasEvaluado, pClUsuario, pNbPrograma);
                return XElement.Parse(poutClaveRetorno.Value.ToString());
            }
        }

        public XElement InsertaCopiaMetas(string METAS_COPIAS_XML = null, int? ID_PERIODO = null, string NB_USUARIO = null, string PROGRAMA_APP = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_METAS_EVALUADO(poutClaveRetorno, METAS_COPIAS_XML, ID_PERIODO, NB_USUARIO, PROGRAMA_APP);
                return XElement.Parse(poutClaveRetorno.Value.ToString());
            }
        }

        public XElement InsertaEstatusEnvioSolicitudes(int? pIdPeriodo = null, bool? pFgEstatus = null, string pClUsuario = null, string pNbPrograma = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_ESTATUS_ENVIO_SOLICITUD(poutClaveRetorno, pIdPeriodo, pFgEstatus, pClUsuario, pNbPrograma);
                return XElement.Parse(poutClaveRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_EO_EVALUADORES_Result> ObtieneEvaluadoresEvaluacionOrganizacional(int? pIdPeriodo, string pClTipoEvaluador = null, int? pID_EMPRESA = null)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_EO_EVALUADORES(pIdPeriodo, pClTipoEvaluador, pID_EMPRESA).ToList();
            }
        }

        public List<SPE_OBTIENE_EO_EVALUADOS_Result> ObtenerEvaluadosEvaluacionOrganizacional(int? pIdCurso)
        {
            using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_EO_EVALUADOS(pIdCurso).ToList();
            }
        }

        //public List<SPE_OBTIENE_EO_RESULTADO_JERARQUICO_Result> ObtenerResultadoJerarquico(int pIdEvaluador)
        //{
        //    using (SistemaSigeinEntities contexto = new SistemaSigeinEntities())
        //    {
        //        return contexto.SPE_OBTIENE_EO_RESULTADO_JERARQUICO(pIdEvaluador).ToList();
        //    }
        //}
        public List<E_BAJAS_PERIODO_EDD> ObtenerBajasEDD(int? pIdEmpleado = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {

                return contexto.Database.SqlQuery<E_BAJAS_PERIODO_EDD>("EXEC " +
                    "EO.SPE_OBTIENE_EO_BAJA_EMPLEADO_DE_EDD " +
                    "@PIN_ID_EMPLEADO ",
                    new SqlParameter("@PIN_ID_EMPLEADO", (object)pIdEmpleado ?? DBNull.Value)
                    ).ToList();
            }

        }
    }
}
