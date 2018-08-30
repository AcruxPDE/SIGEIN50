using SIGE.AccesoDatos.Implementaciones.EvaluacionOrganizacional;
using SIGE.Entidades;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.Negocio.EvaluacionOrganizacional
{
    public class PeriodoDesempenoNegocio
    {

        public List<SPE_OBTIENE_EO_PERIODOS_DESEMPENO_Result> ObtienePeriodosDesempeno(int? pIdPeriodo = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return oPeriodo.ObtenerPeriodosDesempeno(pIdPeriodo);
        }

        public List<SPE_OBTIENE_EO_PERIODOS_DESEMPENO_CUESTIONARIO_Result> ObtienePeriodosDesempenoCuestionario(int? pIdPeriodo = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return oPeriodo.ObtienePeriodosDesempenoCuestionario(pIdPeriodo);
        }

        public List<SPE_OBTIENE_PERIODOS_DESEMPENO_COMPARACION_Result> ObtenerPeriodosComparacion(int? pIdPeriodo = null, int? pIdEvaluado = null, string pClTipoSeleccion = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return oPeriodo.ObtenerPeriodosComparacion(pIdPeriodo, pIdEvaluado, pClTipoSeleccion);
        }

        public List<E_PERIODOS_COMPARAR> ObtenerDesempenoComparacion(string vXmlPeriodos = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            var vPeriodos = oPeriodo.ObtenerDesempenoComparacion(vXmlPeriodos).ToList();
            return (from x in vPeriodos
                    select new E_PERIODOS_COMPARAR
                    {
                        NUM_PERIODO = x.NUM_PERIODO,
                        ID_PERIODO = x.ID_PERIODO,
                        CL_PERIODO = x.CL_PERIODO,
                        NB_PERIODO = x.NB_PERIODO,
                        NB_PERIODO_ENCABEZADO = x.NB_PERIODO + x.NUM_PERIODO.ToString(),
                        DS_PERIODO = x.DS_PERIODO,
                        FE_INICIO = x.FE_INICIO,
                        FE_TERMINO = (DateTime)x.FE_TERMINO,
                        CL_ESTADO_PERIODO = x.CL_ESTADO_PERIODO,
                        DS_NOTAS = x.DS_NOTAS,
                        CL_TIPO_PERIODO = x.CL_TIPO_PERIODO,
                        ID_PERIODO_DESEMPENO = x.ID_PERIODO_DESEMPENO,
                        FG_BONO = x.FG_BONO,
                        PR_BONO = x.PR_BONO,
                        MN_BONO = x.MN_BONO,
                        CL_TIPO_BONO = x.CL_TIPO_BONO,
                        CL_TIPO_CAPTURISTA = x.CL_TIPO_CAPTURISTA,
                        CL_TIPO_METAS = x.CL_TIPO_METAS,
                        CL_ORIGEN_CUESTIONARIO = x.CL_ORIGEN_CUESTIONARIO,
                        ID_PERIODO_REPLICA = x.ID_PERIODO_REPLICA,
                        CL_TIPO_COPIA = x.CL_TIPO_COPIA,
                        NO_REPLICA = x.NO_REPLICA,
                        NB_PUESTO = x.NB_PUESTO
                    }).ToList();
        }

        public List<E_OBTIENE_CUMPLIMIENTO_GLOBAL> ObtieneCumplimientoGlobal(int? pIdPeriodo = null, int? pIdRol = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            var vDesempeno = oPeriodo.ObtenerCumplimientoGlobal(pIdPeriodo, pIdRol).ToList();
            return (from x in vDesempeno
                    select new E_OBTIENE_CUMPLIMIENTO_GLOBAL
                        {
                            ID_PERIODO = x.ID_PERIODO,
                            CL_PUESTO_ACTUAL = x.CL_PUESTO_ACTUAL,
                            NB_PUESTO_ACTUAL = x.NB_PUESTO_ACTUAL,
                            CL_PUESTO_PERIODO = x.CL_PUESTO_PERIODO,
                            NB_PUESTO_PERIODO = x.NB_PUESTO_PERIODO,
                            NB_EVALUADO = x.NB_EVALUADO,
                            PR_EVALUADO = x.PR_EVALUADO,
                            PR_CUMPLIMIENTO_EVALUADO = x.PR_CUMPLIMIENTO_EVALUADO,
                            C_GENERAL = x.C_GENERAL,
                            ID_EVALUADO = x.ID_EVALUADO,
                            ID_BONO_EVALUADO = x.ID_BONO_EVALUADO,
                            ID_EMPLEADO = x.ID_EMPLEADO,
                            CL_EMPLEADO = x.CL_EMPLEADO,
                            CUMPLIDO = x.CUMPLIDO
                        }).ToList();
        }

        public List<SPE_OBTIENE_EO_CUMPLIMIENTO_GLOBAL_GRAFICA_Result> ObtenerCumplimientoGlobalGrafica(string pXmlPeriodo = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return oPeriodo.ObtenerCumplimientoGlobalGrafica(pXmlPeriodo);
        }

        public List<SPE_VERIFICA_CONFIGURACION_METAS_Result> VerificaConfiguracion(int? pIdPeriodo = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return oPeriodo.VerificaConfiguracion(pIdPeriodo);
        }

        public List<SPE_OBTIENE_EO_PERIODOS_CONSECUTIVOS_Result> ObtienePeriodoConsecutivo(int? pIdPeriodo = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return oPeriodo.ObtenerPeriodoConsecutivo(pIdPeriodo);
        }

        public List<SPE_OBTIENE_EO_METAS_EVALUADOS_CONSECUENTES_Result> ObtieneMetasConsecuentes(int? pIdPeriodoOriginal = null, int? pIdPeriodoConsecuente = null, int? pIdEvaOriginal = null, int? pIdEvaConsecuente = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return oPeriodo.ObtenerMetasConsecuentes(pIdPeriodoOriginal, pIdPeriodoConsecuente, pIdEvaOriginal, pIdEvaConsecuente);
        }

        public List<SPE_OBTIENE_METAS_COMPARACION_GRAFICA_Result> ObtenerMetasGrafica(string pXmlPeriodos = null, int? pIdEmpleado = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return oPeriodo.ObtenerMetasGrafica(pXmlPeriodos, pIdEmpleado);
        }

        public List<SPE_OBTIENE_PERIODO_REPLICAS_Result> ObtenerPeriodos(int? pIdPeriodo = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return oPeriodo.ObtenerPeriodos(pIdPeriodo);
        }

        public SPE_OBTIENE_EO_PERIODOS_DESEMPENO_Result ObtienePeriodoDesempeno(int pIdPeriodo)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return oPeriodo.ObtenerPeriodoDesempeno(pIdPeriodo);
        }

        public List<SPE_OBTIENE_EO_EVALUADORES_REPLICAS_Result> ObtenerEvaluadoresReplicas(int pIdPeriodo)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return oPeriodo.ObtenerEvaluadoresReplicas(pIdPeriodo);
        }

        public List<SPE_OBTIENE_SOLICITUDES_ENVIAR_Result> ObtenerSolicitudesEnviar()
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return oPeriodo.ObtenerSolicitudesEnviar();
        }

        public List<SPE_OBTIENE_PERIODOS_SOLICITUDES_ENVIAR_Result> ObtenerPeriodosEnviar()
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return oPeriodo.ObtenerPeriodosEnviar();
        }

        public SPE_OBTIENE_EO_CONTEXTO_METAS_Result ObtienePeriodoDesempenoContexto(int pIdPeriodo, int? idEvaluado)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return oPeriodo.ObtenerPeriodoDesempenoContexto(pIdPeriodo, idEvaluado);
        }

        public E_RESULTADO InsertaActualiza_PERIODO(int? pIdPeriodoDesempeno, string pClPeriodoDesempeno, string pNbPeriodoDesempeno, string pDsPeriodoDesempeno, string pClEstadoPeriodoDesempeno, string pDsNotas, DateTime pFeInicio, DateTime pFeTermino, string pClTipoCapturista, string CL_TIPO_META, string pClUsuario, string pNbPrograma, string pTipoTransaccion, bool? pFgCapturaMasiva)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertaActualiza_PERIODO_DESEMPENO(pIdPeriodoDesempeno, pClPeriodoDesempeno, pNbPeriodoDesempeno, pDsPeriodoDesempeno, pClEstadoPeriodoDesempeno, pDsNotas, pFeInicio, pFeTermino, pClTipoCapturista, CL_TIPO_META, pClUsuario, pNbPrograma, pTipoTransaccion, pFgCapturaMasiva));
        }

        public E_RESULTADO EliminaPeriodoDesempeno(int pIdPeriodo)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.EliminarPeriodosDesempeno(pIdPeriodo));
        }

        public E_RESULTADO InsertaPeriodosReplica(int? pIdPeriodo, string pXmlPeriodos, string pClUsuario, string pNbPrograma, string ClTipoTransaccion)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertaPeriodosReplica(pIdPeriodo, pXmlPeriodos, pClUsuario, pNbPrograma, ClTipoTransaccion));
        }

        //public List<SPE_OBTIENE_EO_EVALUADOS_CONFIGURACION_DESEMPENO_Result> ObtieneEvaluados(int? pIdPeriodo = null, int? pIdEvaluado = null, int? pIdEvaluador = null)
        //{
        //    PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
        //    return oPeriodo.ObtenerEvaluados(pIdPeriodo, pIdEvaluado,pIdEvaluador);
        //}

        public List<E_OBTIENE_EVALUADOS_DESEMPENO> ObtieneEvaluados(int? pIdPeriodo = null, int? pIdEvaluado = null, int? pIdEvaluador = null, string pClUsuario = null, string pNbPrograma = null, int? pIdRol = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            var vEvaluadosDesempeno = oPeriodo.ObtenerEvaluados(pIdPeriodo, pIdEvaluado, pIdEvaluador, pClUsuario, pNbPrograma, pIdRol).ToList();
            return (from x in vEvaluadosDesempeno
                    select new E_OBTIENE_EVALUADOS_DESEMPENO
                    {
                        ID_EMPLEADO = x.ID_EMPLEADO,
                        CL_EMPLEADO = x.CL_EMPLEADO,
                        NB_EMPLEADO_COMPLETO = x.NB_EMPLEADO_COMPLETO,
                        NB_EMPLEADO = x.NB_EMPLEADO,
                        NB_APELLIDO_PATERNO = x.NB_APELLIDO_PATERNO,
                        NB_APELLIDO_MATERNO = x.NB_APELLIDO_MATERNO,
                        CL_ESTADO_EMPLEADO = x.CL_ESTADO_EMPLEADO,
                        CL_GENERO = x.CL_GENERO,
                        CL_ESTADO_CIVIL = x.CL_ESTADO_CIVIL,
                        NB_CONYUGUE = x.NB_CONYUGUE,
                        CL_RFC = x.CL_RFC,
                        CL_CURP = x.CL_CURP,
                        CL_NSS = x.CL_NSS,
                        CL_TIPO_SANGUINEO = x.CL_TIPO_SANGUINEO,
                        CL_NACIONALIDAD = x.CL_NACIONALIDAD,
                        NB_PAIS = x.NB_PAIS,
                        NB_ESTADO = x.NB_ESTADO,
                        NB_MUNICIPIO = x.NB_MUNICIPIO,
                        NB_COLONIA = x.NB_COLONIA,
                        NB_CALLE = x.NB_CALLE,
                        NO_INTERIOR = x.NO_INTERIOR,
                        NO_EXTERIOR = x.NO_EXTERIOR,
                        CL_CODIGO_POSTAL = x.CL_CODIGO_POSTAL,
                        M_EMPLEADO_CL_CORREO_ELECTRONICO = x.M_EMPLEADO_CL_CORREO_ELECTRONICO,
                        M_EMPLEADO_ACTIVO = x.M_EMPLEADO_ACTIVO,
                        FE_NACIMIENTO = x.FE_NACIMIENTO,
                        DS_LUGAR_NACIMIENTO = x.DS_LUGAR_NACIMIENTO,
                        FE_ALTA = x.FE_ALTA,
                        FE_BAJA = x.FE_BAJA,
                        MN_SUELDO = x.MN_SUELDO,
                        MN_SUELDO_VARIABLE = x.MN_SUELDO_VARIABLE,
                        DS_SUELDO_COMPOSICION = x.DS_SUELDO_COMPOSICION,
                        CL_PUESTO = x.CL_PUESTO,
                        NB_PUESTO = x.NB_PUESTO,
                        XML_RESPONSABILIDAD = x.XML_RESPONSABILIDAD,
                        CL_EMPRESA = x.CL_EMPRESA,
                        NB_EMPRESA = x.NB_EMPRESA,
                        NB_RAZON_SOCIAL = x.NB_RAZON_SOCIAL,
                        CL_DEPARTAMENTO = x.CL_DEPARTAMENTO,
                        NB_DEPARTAMENTO = x.NB_DEPARTAMENTO,
                        ID_EVALUADO = x.ID_EVALUADO,
                        NO_TOTAL_METAS = x.NO_TOTAL_METAS,
                        NO_TOTAL_METAS_ACTIVAS = x.NO_TOTAL_METAS_ACTIVAS,
                        PR_EVALUADO = x.PR_EVALUADO,
                        NO_EVALUADOR = x.NO_EVALUADOR,
                        FE_CAPTURA_METAS = x.FE_CAPTURA_METAS,
                        MN_TOPE_BONO = x.MN_TOPE_BONO,
                        NO_MONTO_BONO = x.NO_MONTO_BONO,
                        CL_MONTO_BONO = x.CL_MONTO_BONO,
                        PR_CUMPLIMIENTO_EVALUADO = x.PR_CUMPLIMIENTO_EVALUADO,
                        MN_BONO_TOTAL = x.MN_BONO_TOTAL,
                        ESTATUS = x.ESTATUS
                    }).ToList();
        }


        public List<E_OBTIENE_EVALUADOS_DESEMPENO> ObtenerEvaluadosDesempeno(string pXmlPeriodos = null, int? pIdRol = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            var vEvaluadosDesempeno = oPeriodo.ObtenerEvaluadosDesempeno(pXmlPeriodos, pIdRol).ToList();
            return (from x in vEvaluadosDesempeno
                    select new E_OBTIENE_EVALUADOS_DESEMPENO
                    {
                        ID_EMPLEADO = x.ID_EMPLEADO,
                        CL_EMPLEADO = x.CL_EMPLEADO,
                        NB_EMPLEADO = x.NB_EMPLEADO,
                        MN_SUELDO = x.MN_SUELDO,
                        NB_PUESTO = x.NB_PUESTO,
                        NB_DEPARTAMENTO = x.NB_DEPARTAMENTO,
                        FG_VISIBLE_BONO = x.FG_SUELDO_VISIBLE_BONO
                    }).ToList();
        }


        public List<E_OBTIENE_EVALUADOS_DESEMPENO> ObtenerBonosDesempeno(int? pIdEmpledo = null, string pXmlPeriodos = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            var vEvaluadosDesempeno = oPeriodo.ObtenerBonosDesempeno(pIdEmpledo, pXmlPeriodos).ToList();
            return (from x in vEvaluadosDesempeno
                    select new E_OBTIENE_EVALUADOS_DESEMPENO
                    {
                        ID_EVALUADO = x.ID_EVALUADO,
                        PR_EVALUADO = x.PR_EVALUADO,
                        FE_INICIO = x.FE_INICIO,
                        FE_TERMINO = x.FE_TERMINO == null ? x.FE_INICIO : (System.DateTime)x.FE_TERMINO,
                        MN_TOPE_BONO = x.MN_TOPE_BONO,
                        NO_MONTO_BONO = x.NO_MONTO_BONO,
                        PR_CUMPLIMIENTO_EVALUADO = x.PR_CUMPLIMIENTO_EVALUADO,
                        MN_BONO_TOTAL = x.MN_BONO_TOTAL,
                        CL_PERIODO = x.CL_PERIODO,
                        ID_PERIODO = (int)x.ID_PERIODO,
                        PR_BONO = x.PR_BONO,
                        MN_BONO = x.MN_BONO
                    }).ToList();
        }

        public E_RESULTADO EliminaEvaluados(int pIdPeriodo, XElement pXmlEvaluados, string pClUsuario, string pNbPrograma)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.EliminarEvaluados(pIdPeriodo, pXmlEvaluados, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO InsertaEvaluados(int pIdPeriodo, XElement pXmlEvaluados, string pClUsuario, string pNbPrograma)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertarEvaluados(pIdPeriodo, pXmlEvaluados, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO ActualizaConfiguracionDesempeno(int pIdPeriodoDesempeno, int pFgBono, decimal pPrBono, decimal pMnBono, string pClTipoBono, string pClUsuario, string pNbPrograma)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.ActualizarConfiguracionDesempeno(pIdPeriodoDesempeno, pFgBono, pPrBono, pMnBono, pClTipoBono, pClUsuario, pNbPrograma));
        }

        public List<SPE_OBTIENE_BONO_EVALUADOS_Result> ObtieneBonoEvaluados(int pIdPeriodo, int? pIdRol)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return oPeriodo.ObtenerBonoEvaluados(pIdPeriodo, pIdRol);
        }

        public E_RESULTADO ActualizaEvaluadoTopeBono(int pIdPeriodo, decimal pPrBono, string pClTipoBono, string pXmlEvaluado, string pNbPrograma, string pClUsuario)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.ActualizarEvaluadoTopeBono(pIdPeriodo, pPrBono, pClTipoBono, pXmlEvaluado, pNbPrograma, pClUsuario));
        }

        public List<E_META> ObtieneMetas(int? pIdPeriodo = null, int? pIdEvaluado = null, int? pIdEvaluadoMeta = null, int? pNoMeta = null, bool? pFgEvaluar = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return oPeriodo.ObtenerMetas(pIdPeriodo, pIdEvaluado, pIdEvaluadoMeta, pNoMeta, pFgEvaluar);
        }

        public E_RESULTADO ActualizarMetasEvaluado(string pClTipoMetas, int pIdPeriodo, string xmlEmpleados, string pClUsuario, string pNbPrograma)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.ActualizarMetasEvaluado(pClTipoMetas, pIdPeriodo, xmlEmpleados, pClUsuario, pNbPrograma));
        }

        public List<SPE_OBTIENE_CONFIGURACION_PERIODO_REPLICAS_Result> ObtieneConfiguracionEnvio(int? pIdPeriodo = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return oPeriodo.ObtieneConfiguracionEnvio(pIdPeriodo);
        }

        //public List<SPE_OBTIENE_EO_FUNCIONES_METAS_Result> ObtieneFuncionesMetas(int? pIdEvaluado = null, int? pIdPeriodo = null)
        //{
        //    PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
        //    return oPeriodo.ObtenerFuncionesMetas(pIdEvaluado, pIdPeriodo);
        //}
        public List<E_OBTIENE_FUNCIONES_METAS> ObtieneFuncionesMetas(int? pIdEvaluado = null, int? pIdPeriodo = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            var vFuncionesMetas = oPeriodo.ObtenerFuncionesMetas(pIdEvaluado, pIdPeriodo).ToList();
            return (from x in vFuncionesMetas
                    select new E_OBTIENE_FUNCIONES_METAS
                 {
                     ID_EVALUADO = x.ID_EVALUADO,
                     ID_PERIODO = x.ID_PERIODO,
                     ID_EMPLEADO = x.ID_EMPLEADO,
                     ID_PUESTO = x.ID_PUESTO,
                     DS_PUESTO_FUNCION = x.DS_PUESTO_FUNCION
                 }).ToList();
        }

        //**********************************************************************************
        public List<E_INDICADORES_METAS> ObtieneIndicadoresMetas(int? pIdPeriodo = null, int? pIdEvaluado = null, string pDsFuncion = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            var vFuncionesMetas = oPeriodo.ObtenerIndicadoresMetas(pIdPeriodo, pIdEvaluado, pDsFuncion).ToList();
            return (from x in vFuncionesMetas
                    select new E_INDICADORES_METAS
                    {
                        ID_EVALUADO = x.ID_EVALUADO,
                        ID_PERIODO = x.ID_PERIODO,
                        NB_INDICADOR = x.NB_INDICADOR,
                        DS_FUNCION = x.DS_FUNCION
                    }).ToList();
        }
        //***************************************************************************************

        public E_RESULTADO InsetaActualizaMetasEvaluados(int? pIdMetaEvaluado = null, int? pIdPeriodo = null, int? pIdEvaluado = null, string pDsFuncion = null, int? pNoMeta = null, string pNbIndicador = null, string pDsMeta = null, string pClTipoMeta = null, bool? pFgValidaCumplimiento = null, bool? pFgEvaluar = null, string pNbCumplimientoActual = null, string pNbCumplimientoMinimo = null, string pNbCumplimientoSatisfactorio = null, string pNbCumplimientoSobresaliente = null, decimal? pPrMeta = null, decimal? pPrResultado = null, int? pClNivel = null, decimal? pPrCumplimientoMeta = null, string pClUsuario = null, string pNbPrograma = null, string pTipoTransaccion = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsetarActualizarMetasEvaluados(pIdMetaEvaluado, pIdPeriodo, pIdEvaluado, pDsFuncion, pNoMeta, pNbIndicador, pDsMeta, pClTipoMeta, pFgValidaCumplimiento, pFgEvaluar, pNbCumplimientoActual, pNbCumplimientoMinimo, pNbCumplimientoSatisfactorio, pNbCumplimientoSobresaliente, pPrMeta, pPrResultado, pClNivel, pPrCumplimientoMeta, pClUsuario, pNbPrograma, pTipoTransaccion));
        }

        public E_RESULTADO ActualizaPonderacionPuesto(decimal? PrEvaluado, int? pIdPeriodoDesempeno, int? pIdEvaluado, string pClUsuario, string pNbPrograma)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.ActualizaPonderacionPuesto(PrEvaluado, pIdPeriodoDesempeno, pIdEvaluado, pClUsuario, pNbPrograma));
        }

        public List<SPE_OBTIENE_EO_METAS_EVALUADOS_Result> ObtieneMetasEvaluados(int? idEvaluadoMeta = null, int? pIdPeriodo = null, int? idEvaluado = null, int? no_Meta = null, string cl_nivel = null, bool? FgEvaluar = null, int? idEmpleado = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return oPeriodo.ObtenerMetasEvaluados(idEvaluadoMeta, pIdPeriodo, idEvaluado, no_Meta, cl_nivel, FgEvaluar, idEmpleado);
        }

        public List<SPE_OBTIENE_EO_METAS_CAPTURA_MASIVA_Result> ObtieneMetasCapturaMasiva(int? pIdPeriodo = null, int? idEvaluador = null, System.Guid? pFlEvaluador = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return oPeriodo.ObtieneMetasCapturaMasiva(pIdPeriodo, idEvaluador, pFlEvaluador);
        }

        public List<E_METAS_PERIODO_COMPARACION> ObtieneMetasComparacion(int? idEvaluadoMeta = null, int? pIdPeriodo = null, int? idEvaluado = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            var vMetasPeriodo = oPeriodo.ObtieneMetasComparacion(idEvaluadoMeta, pIdPeriodo, idEvaluado).ToList();
            return (from x in vMetasPeriodo
                    select new E_METAS_PERIODO_COMPARACION
                        {
                            ID_EVALUADO_META = x.ID_EVALUADO_META,
                            ID_PERIODO = x.ID_PERIODO,
                            ID_EVALUADO = x.ID_EVALUADO,
                            NO_META = x.NO_META,
                            DS_FUNCION = x.DS_FUNCION,
                            NB_INDICADOR = x.NB_INDICADOR,
                            DS_META = x.DS_META,
                            CL_TIPO_META = x.CL_TIPO_META,
                            FG_VALIDA_CUMPLIMIENTO = x.FG_VALIDA_CUMPLIMIENTO,
                            FG_EVALUAR = x.FG_EVALUAR,
                            NB_CUMPLIMIENTO_ACTUAL = x.NB_CUMPLIMIENTO_ACTUAL,
                            NB_CUMPLIMIENTO_MINIMO = x.NB_CUMPLIMIENTO_MINIMO,
                            NB_CUMPLIMIENTO_SATISFACTORIO = x.NB_CUMPLIMIENTO_SATISFACTORIO,
                            NB_CUMPLIMIENTO_SOBRESALIENTE = x.NB_CUMPLIMIENTO_SOBRESALIENTE,
                            PR_META = x.PR_META,
                            NB_RESULTADO = x.NB_RESULTADO,
                            PR_RESULTADO = x.PR_RESULTADO,
                            CL_NIVEL = x.CL_NIVEL,
                            PR_CUMPLIMIENTO_META = x.PR_CUMPLIMIENTO_META,
                            FG_EVIDENCIA = x.FG_EVIDENCIA,
                            PR_EVALUADO = x.PR_EVALUADO,
                            NIVEL_ALZANZADO = x.NIVEL_ALZANZADO,
                            COLOR_NIVEL = x.COLOR_NIVEL
                        }).ToList();
        }


        public List<E_METAS_COMPARACION_DESEMPENO> ObtieneMetasPeriodoComparar(string pXmlPeriodos = null, int? idEvaluado = null, int? pIdPeriodo = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            var vMetasPeriodo = oPeriodo.ObtieneMetasPeriodoComparar(pXmlPeriodos, idEvaluado, pIdPeriodo).ToList();
            return (from x in vMetasPeriodo
                    select new E_METAS_COMPARACION_DESEMPENO
                    {
                        ID_EVALUADO_META = x.ID_META_EVALUADO,
                        ID_PERIODO = x.ID_PERIODO,
                        ID_EVALUADO = x.ID_EVALUADO_PERIODO,
                        NO_META = (int)x.NO_META,
                        DS_FUNCION = x.DS_FUNCION,
                        NB_INDICADOR = x.NB_INDICADOR,
                        DS_META = x.DS_META,
                        CL_TIPO_META = x.CL_TIPO_META,
                        FG_VALIDA_CUMPLIMIENTO = x.FG_VALIDA_CUMPLIMIENTO,
                        FG_EVALUAR = x.FG_EVALUAR,
                        NB_CUMPLIMIENTO_ACTUAL = x.NB_CUMPLIMIENTO_ACTUAL,
                        NB_CUMPLIMIENTO_MINIMO = x.NB_CUMPLIMIENTO_MINIMO,
                        NB_CUMPLIMIENTO_SATISFACTORIO = x.NB_CUMPLIMIENTO_SATISFACTORIO,
                        NB_CUMPLIMIENTO_SOBRESALIENTE = x.NB_CUMPLIMIENTO_SOBRESALIENTE,
                        PR_META = x.PR_META,
                        NB_RESULTADO = x.NB_RESULTADO,
                        PR_RESULTADO = x.PR_RESULTADO,
                        CL_NIVEL = x.CL_NIVEL,
                        PR_CUMPLIMIENTO_META = x.PR_CUMPLIMIENTO_META,
                        PR_CUMPLIMIENTO_META_STR = x.ID_META_EVALUADO == 0?"N/A": x.PR_CUMPLIMIENTO_META.ToString() + "%",
                        FG_EVIDENCIA = x.FG_EVIDENCIA,
                        PR_EVALUADO = x.ID_META_EVALUADO ==0? "N/A": x.PR_EVALUADO.ToString()+"%",
                        NIVEL_ALZANZADO = x.NIVEL_ALCANZADO,
                        COLOR_NIVEL = x.COLOR_NIVEL
                    }).ToList();
        }

        public List<SPE_OBTIENE_EO_EVALUADORES_TOKEN_Result> ObtenerEvaluadoresPeriodo(int pID_PERIODO, int? pID_ROL)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return oPeriodo.ObtenerEvaluadores(pID_PERIODO, pID_ROL);
        }

        public SPE_OBTIENE_EO_PERIODO_EVALUADOR_DESEMPENO_Result ObtenerPeriodoEvaluadorDesempeno(int? pID_EVALUADOR = null, Guid? pFL_EVALUADOR = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return oPeriodo.ObtenerPeriodoEvaluadorDesempeno(pID_EVALUADOR, pFL_EVALUADOR);
        }

        public E_RESULTADO ActualizaResultadosMetas(int pIdPeriodo, int pIdEvaluado, XElement xmlResultados, string pClUsuario, string pNbPrograma, decimal pSuma)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.ActualizarResultadosMetas(pIdPeriodo, pIdEvaluado, xmlResultados, pClUsuario, pNbPrograma, pSuma));
        }

        public E_RESULTADO ActualizaResultadosMetasMasiva(int pIdPeriodo, int pIdEvaluador, XElement xmlResultados, string pClUsuario, string pNbPrograma, decimal pSuma)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.ActualizarResultadosMetasMasiva(pIdPeriodo, pIdEvaluador, xmlResultados, pClUsuario, pNbPrograma, pSuma));
        }

        public SPE_OBTIENE_EVIDENCIAS_METAS_Result ObtieneEvidenciasMetasEvaluados(int? idEvaluadoMeta = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            SPE_OBTIENE_EVIDENCIAS_METAS_Result vDocumentoProceso = oPeriodo.ObtenerEvidenciasMetasEvaluados(idEvaluadoMeta);
            if (vDocumentoProceso.XML_DOCUMENTOS != null)
            {
                XElement vDocumento = XElement.Parse(vDocumentoProceso.XML_DOCUMENTOS);
                vDocumentoProceso.XML_DOCUMENTOS = vDocumento.ToString();
            }
            else
            {
                vDocumentoProceso.XML_DOCUMENTOS = "";
            }

            return vDocumentoProceso;

        }

        public E_RESULTADO InsertaActualizaEvidenciasMetas(int? pIsEvaluadoMeta, List<UDTT_ARCHIVO> pLstArchivosTemporales, List<E_DOCUMENTO> pLstDocumentos, string pClUsuario, string pNbPrograma, int? pIdEvaluador)
        {
            PeriodoDesempenoOperaciones oSolicitud = new PeriodoDesempenoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oSolicitud.InsertarActualizarEvidenciasMetas(pIsEvaluadoMeta, pLstArchivosTemporales, pLstDocumentos, pClUsuario, pNbPrograma, pIdEvaluador));
        }

        public E_RESULTADO ActualizaPonderacionEvaluados(int? pIdPeriodoDesempeno, string pXmlEvaluados, string pTipoActualizacion, string pClUsuario, string pNbPrograma)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.ActualizaPonderacionEvaluados(pIdPeriodoDesempeno, pXmlEvaluados, pTipoActualizacion, pClUsuario, pNbPrograma));
        }

        public List<SPE_PONDERACION_METAS_DESEMPENO_Result> ObtienePonderacionMetas(int pIdPeriodo)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return oPeriodo.ObtenerPonderacionMetas(pIdPeriodo);
        }

        public E_RESULTADO InsertaEvaluadoresOtro(int pIdPeriodo, XElement pXmlEvaluados, XElement pXmlEvaluadores, string pClUsuario, string pNbPrograma)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertarEvaluadorOtro(pIdPeriodo, pXmlEvaluados, pXmlEvaluadores, pClUsuario, pNbPrograma));
        }

        public List<SPE_EVALUADOR_POR_EVALUADO_Result> ObtieneEvaluadoresPorEvaluado(int? pIdPeriodo = null, int? pIdEvaluado = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return oPeriodo.ObtenerEvaludoresPorEvaluador(pIdPeriodo, pIdEvaluado);
        }

        public E_RESULTADO EliminaEvaluadorEvaluado(int pIdPeriodo, XElement pXmlEvaluadorEvaluado, string pClUsuario, string pNbPrograma)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.EliminarEvaluadoresPorEvaluadorEvaluado(pIdPeriodo, pXmlEvaluadorEvaluado, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO EliminaMetaEvaluado(int pIdPeriodo, int pIdMetaEvaluado, int pIdEvaluado, string pClUsuario, string pNbPrograma)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.EliminarMetaEvaluado(pIdPeriodo, pIdMetaEvaluado, pIdEvaluado, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO EliminaMetaInactivas(int pIdPeriodo)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.EliminarMetasInactivas(pIdPeriodo));
        }

        public E_RESULTADO InsertaPeriodoDesempenoCopia(E_PERIODO_DESEMPENO pPeriodo, string pCL_USUARIO, string pNB_PROGRAMA)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertarPeriodoDesempenoCopia(pPeriodo, pCL_USUARIO, pNB_PROGRAMA));
        }

        public E_RESULTADO InsertaPeriodoDesempenoReplica(int? pIdPeriodo = null, DateTime? pFeInicio = null, DateTime? pFeFin = null, string pCL_USUARIO = null, string pNB_PROGRAMA = null, string pTipoTransaccion = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertarPeriodoDesempenoReplica(pIdPeriodo, pFeInicio, pFeFin, pCL_USUARIO, pNB_PROGRAMA, pTipoTransaccion));
        }

        public List<SPE_OBTIENE_PERIODOS_DESEMPENO_REPLICA_Result> ObtienePeriodosReplicados(int? pIdPeriodo = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return oPeriodo.ObtenerPeriodosReplicados(pIdPeriodo);
        }

        public List<SPE_VALIDA_PERIODO_DESEMPENO_Result> ValidaPeriodoDesempeno(int? pIdPeriodo = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return oPeriodo.ValidarPeriodoDesempeno(pIdPeriodo);
        }

        public List<SPE_OBTIENE_CONTROL_AVANCE_DESEMPENO_Result> ObtieneControlAvanceDesempeno(int pIdPeriodoDesempeno, int? pIdRol)
        {
            PeriodoDesempenoOperaciones oDesempeno = new PeriodoDesempenoOperaciones();
            return oDesempeno.ObtenerControlAvanceDesempeno(pIdPeriodoDesempeno, pIdRol);
        }

        public E_RESULTADO InsertaActualizaBono(int? pIdPeriodo = null, string pCL_USUARIO = null, string pNB_PROGRAMA = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertarActualizarBono(pIdPeriodo, pCL_USUARIO, pNB_PROGRAMA));
        }


        public E_RESULTADO ActualizarEvaluadoMetas(string METAS_EVALUADO_XML = null, string NB_USUARIO = null, string PROGRAMA_APP = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.ActualizarEvaluadoMetas(METAS_EVALUADO_XML, NB_USUARIO, PROGRAMA_APP));
        }

        public E_RESULTADO InsertaCopiaMetas(string METAS_COPIAS_XML = null, int? ID_PERIODO = null, string NB_USUARIO = null, string PROGRAMA_APP = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertaCopiaMetas(METAS_COPIAS_XML, ID_PERIODO, NB_USUARIO, PROGRAMA_APP));
        }

        public E_RESULTADO InsertaFeEnvioSolicitud(string pXmlFechas, string pClUsuario, string pNbPrograma, string pTipoTransaccion)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertaFeEnvioSolicitud(pXmlFechas, pClUsuario, pNbPrograma, pTipoTransaccion));
        }

        public E_RESULTADO InsertaEstatusEnvioSolicitudes(int? pIdPeriodo = null, bool? pFgEstatus = null, string pClUsuario = null, string pNbPrograma = null)
        {
            PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertaEstatusEnvioSolicitudes(pIdPeriodo, pFgEstatus, pClUsuario, pNbPrograma));
        }

        public List<SPE_OBTIENE_EO_EVALUADORES_Result> ObtieneEvaluadoresEvaluacionOrganizacional(int pIdPeriodo, string pClTipoEvaluador = null, int? pID_EMPRESA = null)
        {
            PeriodoDesempenoOperaciones oCuestionario = new PeriodoDesempenoOperaciones();
            return oCuestionario.ObtieneEvaluadoresEvaluacionOrganizacional(pIdPeriodo, pClTipoEvaluador, pID_EMPRESA);
        }

        public List<SPE_OBTIENE_EO_EVALUADOS_Result> ObtenerEvaluadosEvaluacionOrganizacional(int pIdEvaluador)
        {
            PeriodoDesempenoOperaciones oCuestionario = new PeriodoDesempenoOperaciones();
            return oCuestionario.ObtenerEvaluadosEvaluacionOrganizacional(pIdEvaluador);
        }

        //public List<SPE_OBTIENE_EO_RESULTADO_JERARQUICO_Result> ObtieneResultadoJerarquico(int pIdEvaluador)
        //{
        //    PeriodoDesempenoOperaciones oPeriodo = new PeriodoDesempenoOperaciones();
        //    return oPeriodo.ObtenerResultadoJerarquico(pIdEvaluador);
        //}
    }
}
