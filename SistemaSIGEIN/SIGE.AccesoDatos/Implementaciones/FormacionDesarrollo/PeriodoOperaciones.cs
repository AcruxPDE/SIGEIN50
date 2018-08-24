using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo
{
    public class PeriodoOperaciones
    {
        SistemaSigeinEntities contexto;

        public List<SPE_OBTIENE_FYD_PERIODOS_EVALUACION_Result> ObtenerPeriodosEvaluacion(int? pIdPeriodo = null, int? pIdEmpleado = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_PERIODOS_EVALUACION(pIdPeriodo, pIdEmpleado).ToList();
            }
        }

        public SPE_OBTIENE_FYD_PERIODO_EVALUACION_Result ObtenerPeriodoEvaluacion(int pIdPeriodo)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_PERIODO_EVALUACION(pIdPeriodo).FirstOrDefault();
            }
        }

        public SPE_OBTIENE_FYD_VALIDACION_AUTORIZACIONES_Result ObtenerValidacionAutorizacion(int pIdPeriodo)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_VALIDACION_AUTORIZACIONES(pIdPeriodo).FirstOrDefault();
            }
        }

        public SPE_OBTIENE_CONFIGURACION_PERIODO_COMPETENCIAS_Result VerificaConfiguracion(int? pIdPeriodo = null, string pClPeriodo = null, string pNbPeriodo = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_CONFIGURACION_PERIODO_COMPETENCIAS(pIdPeriodo, pClPeriodo, pNbPeriodo).FirstOrDefault();
            }
        }


        public List<SPE_OBTIENE_FYD_EVALUADOS_CONFIGURACION_Result> ObtenerEvaluados(int pIdPeriodo, int? pID_EMPRESA = null, int? pID_ROL = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_EVALUADOS_CONFIGURACION(pIdPeriodo, pID_EMPRESA, pID_ROL).ToList();
            }
        }

        public List<SPE_OBTIENE_FYD_CUESTIONARIOS_EVALUADOS_Result> ObtenerEvaluadosCuestionarios(int pIdPeriodo, int? pIdEmpresa = null, int? vIdRol = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_CUESTIONARIOS_EVALUADOS(pIdPeriodo, pIdEmpresa, vIdRol).ToList();
            }
        }

        public List<SPE_OBTIENE_FYD_EVALUADOS_AUTORIZACION_Result> ObtenerEvaluadosEvaluadores(int pIdPeriodo, int? pIdEmpresa = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_EVALUADOS_AUTORIZACION(pIdPeriodo, pIdEmpresa).ToList();
            }
        }

        public XElement InsertarActualizarPeriodoEvaluacionCompetencias(int? pIdPeriodo, string pClPeriodo, string pNbPeriodo, string pDsPeriodo, bool pFeInicio, string pFeTermino, string pClEstado, string pClTipo, string pDsNotas, int? pIdBitacora, string pXmlCamposAdicionales, int? pIdPeriodoEvaluacion, bool pFgAutoevaluacion, bool pFgSupervisor, bool pFgSubordinados, bool pFfgInterrelacionados, bool pFgOtros,bool pFgPeriodoPVC, string pClUsuario, string pNbPrograma, string pClTipoTransaccion)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_ACTUALIZA_PERIODO(pout_clave_retorno, pIdPeriodo, pClPeriodo, pNbPeriodo, pDsPeriodo, pFeInicio, pFeTermino, pClEstado, pDsNotas, pIdBitacora, pXmlCamposAdicionales, pIdPeriodoEvaluacion, pFgAutoevaluacion, pFgSupervisor, pFgSubordinados, pFfgInterrelacionados, pFgOtros,pFgPeriodoPVC, pClUsuario, pNbPrograma, pClTipoTransaccion);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement InsertarCopiaPeriodoEvaluacionCompetencias(int? pIdPeriodo, string pClPeriodo, string pNbPeriodo, string pDsPeriodo, bool pFeInicio, string pFeTermino, string pClEstado, string pClTipo, string pDsNotas, int? pIdBitacora, string pXmlCamposAdicionales, int? pIdPeriodoEvaluacion, bool pFgAutoevaluacion, bool pFgSupervisor, bool pFgSubordinados, bool pFfgInterrelacionados, bool pFgOtros, bool pFgPeriodoPVC, string pClUsuario, string pNbPrograma, string pClTipoTransaccion)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_FYD_COPIA_PERIODO(pout_clave_retorno, pIdPeriodo, pClPeriodo, pNbPeriodo, pDsPeriodo, pFeInicio, pFeTermino, pClEstado, pDsNotas, pIdBitacora, pXmlCamposAdicionales, pIdPeriodoEvaluacion, pFgAutoevaluacion, pFgSupervisor, pFgSubordinados, pFfgInterrelacionados, pFgOtros, pFgPeriodoPVC, pClUsuario, pNbPrograma, pClTipoTransaccion);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement InsertarEvaluados(int pIdPeriodo, XElement pXmlEvaluados, string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_ACTUALIZA_EVALUADOS(pOutClRetorno, pIdPeriodo, pXmlEvaluados.ToString(), pClUsuario, pNbPrograma, "I");
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement InsertarOtrosEvaluadoresInventario(int pIdPeriodo, XElement pXmlOtrosEvaluadores, bool pFgEvaluaTodos, string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_ACTUALIZA_FYD_OTROS_EVALUADORES(pOutClRetorno, pIdPeriodo, pXmlOtrosEvaluadores.ToString(), pFgEvaluaTodos, "OTRO", pClUsuario, pNbPrograma, E_TIPO_OPERACION_DB.I.ToString());
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement InsertarActualizaOtrosEvaluadoresExterno(int pIdPeriodo, int? pIdEmpleado, int? pIdEvaluador, string pNbEvaluador, string pNbPuesto, string pClCorreoElectronico, bool pFgEvaluaTodos, string pClUsuario, string pNbPrograma, E_TIPO_OPERACION_DB pClTipoOperacion)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_ACTUALIZA_FYD_OTROS_EVALUADORES_EXTERNOS(pOutClRetorno, pIdPeriodo, pIdEmpleado, pIdEvaluador, pNbPuesto, pClCorreoElectronico, pNbEvaluador, pFgEvaluaTodos, "OTRO", pClUsuario, pNbPrograma, pClTipoOperacion.ToString());
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement EliminarEvaluados(int pIdPeriodo, XElement pXmlEvaluados, string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ELIMINA_FYD_EVALUADOS(pOutClRetorno, pIdPeriodo, pXmlEvaluados.ToString(), pClUsuario, pNbPrograma, "I");
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement EliminarEvaluador(int pIdEvaluador, string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ELIMINA_FYD_OTRO_EVALUADOR(pOutClRetorno, pIdEvaluador, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public SPE_OBTIENE_FYD_PERIODO_EVALUADOR_Result ObtenerPeriodoEvaluador(int? pIdPeriodoEvaluador = null, Guid? pFlEvaluador = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_PERIODO_EVALUADOR(pIdPeriodoEvaluador, pFlEvaluador).FirstOrDefault();
            }
        }

        public SPE_OBTIENE_FYD_EVALUADO_Result ObtenerEvaluado(int? pIdEvaluadoEvaluador = null, int? pIdEvaluado = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_EVALUADO(pIdEvaluadoEvaluador, pIdEvaluado).FirstOrDefault();
            }
        }

        public List<SPE_OBTIENE_FYD_PREGUNTAS_EVALUACION_Result> ObtenerPreguntas(int pIdEvaluadoEvaluador)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_PREGUNTAS_EVALUACION(pIdEvaluadoEvaluador).ToList();
            }
        }

        public List<SPE_OBTIENE_EO_PREGUNTAS_EVALUACION_Result> ObtenerPreguntasEO(int pIdEvaluadoEvaluador)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_EO_PREGUNTAS_EVALUACION(pIdEvaluadoEvaluador).ToList();
            }
        }

        public List<SPE_OBTIENE_EO_RESULTADO_EVALUADOS_Result> ObtenerResultadosCuestionariosEO(int pIdPeriodo)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_EO_RESULTADO_EVALUADOS(pIdPeriodo).ToList();
            }
        }

        public List<SPE_OBTIENE_FYD_PUESTOS_EVALUADO_Result> ObtenerPuestosEvaluado(int pIdEvaluado)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_PUESTOS_EVALUADO(pIdEvaluado).ToList();
            }
        }

        public XElement InsertarActualizarOtrosPuestosEvaluados(int pIdPeriodo, XElement pXmlEvaluados, XElement pXmlPuestos, string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_ACTUALIZA_OTROS_PUESTOS_EVALUADO(pOutClRetorno, pIdPeriodo, pXmlEvaluados.ToString(), pXmlPuestos.ToString(), pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement EliminarPuestoEvaluado(int pIdPuestoEvaluado)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ELIMINA_FYD_PUESTO_EVALUADO(pOutClRetorno, pIdPuestoEvaluado);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement ActualizarConfiguracionPeriodo(int pIdPeriodo, XElement pXmlConfiguracion, string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ACTUALIZA_FYD_CONFIGURACION_PERIODO_COMPETENCIAS(pOutClRetorno, pIdPeriodo, pXmlConfiguracion.ToString(), pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement InsertarCuestionarios(int pIdPeriodo, bool pFgCreaCuestionarios, string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_ACTUALIZA_FYD_CUESTIONARIO(pOutClRetorno, pIdPeriodo, pFgCreaCuestionarios, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_FYD_EVALUADO_CUESTIONARIOS_Result> ObtenerCuestionariosEvaluado(int pIdEvaluado)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_EVALUADO_CUESTIONARIOS(pIdEvaluado).ToList();
            }
        }

        public List<SPE_OBTIENE_FYD_EVALUADORES_AUTORIZACION_Result> ObtenerEvaluadoresAutorizacion(int pIdEvaluado)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_EVALUADORES_AUTORIZACION(pIdEvaluado).ToList();
            }
        }

        public XElement ActualizarEstatusPeriodo(int pIdPeriodo, string pClEstatus, string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ACTUALIZA_ESTATUS_PERIODO(pOutClRetorno, pIdPeriodo, pClEstatus, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement InsertarPreguntasAdicionales(int? pIdPeriodo = null, int? pIdPregunta = null, string pNbPregunta = null, XElement pXmlPreguntasAdicionales = null, string pClCuestionarioObjetivo = null, string pClUsuario = null, string pNbPrograma = null, string pClTipoTransaccion = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_FYD_PREGUNTAS_ADICIONALES_PERIODO(pOutClRetorno, pIdPeriodo,pIdPregunta, pNbPregunta, pXmlPreguntasAdicionales.ToString(), pClCuestionarioObjetivo, pClUsuario, pNbPrograma,pClTipoTransaccion);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_FYD_PREGUNTAS_ADICIONALES_PERIODO_Result> ObtenerPreguntasAdicionales(int? pIdPeriodo = null, int? pIdPregunta = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_PREGUNTAS_ADICIONALES_PERIODO(pIdPeriodo, pIdPregunta).ToList();
            }
        }

        public XElement EliminarPreguntaAdicional(string  pXmlPreguntas = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ELIMINA_FYD_PREGUNTA_ADICIONAL(pOutClRetorno, pXmlPreguntas);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement EliminaCuestionario(int pIdCuestionario)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ELIMINA_FYD_CUESTIONARIO(pOutClRetorno, pIdCuestionario);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement ActualizarRepuestasCuestionario(string pXmlRespuestas, string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ACTUALIZA_PREGUNTA_RESPUESTA(pOutClRetorno, pXmlRespuestas, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement InsertarResultadoEvaluacionCompetencia(int pIdPeriodo, int pIdEvaluado, string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_RESULTADO_EVALUACION_COMPETENCIA(pOutClRetorno, pIdPeriodo, pIdEvaluado, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement TerminarCuestionario(string pXmlRespuestas, int pIdPeriodo, int pIdEvaluado, int pIdEvaluadoEvaluador, int pNoValor, bool pFgTerminado, string pClUsuario, string pNbPrograma, string pXmlRespuestasAdicionales = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_TERMINA_CUESTIONARIO_EVALUADOR(pOutClRetorno, pXmlRespuestas, pXmlRespuestasAdicionales, pIdPeriodo, pIdEvaluado, pIdEvaluadoEvaluador, pNoValor, pFgTerminado, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public E_CUESTIONARIO ObtenerCuestionario(int? pIdCuestionario = null, int? pIdEvaluado = null, int? pIdEvaluadoEvaluador = null, int? pIdEvaluador = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {

                var oCuestionario = contexto.SPE_OBTIENE_CUESTIONARIO(pIdCuestionario, pIdEvaluado, pIdEvaluadoEvaluador, pIdEvaluador).FirstOrDefault();

                if (oCuestionario != null)
                {
                    E_CUESTIONARIO oCuestionarioExterno = new E_CUESTIONARIO
                    {
                        FG_CONTESTADO = oCuestionario.FG_CONTESTADO,
                        ID_CUESTIONARIO = oCuestionario.ID_CUESTIONARIO,
                        ID_CUESTIONARIO_PLANTILLA = oCuestionario.ID_CUESTIONARIO_PLANTILLA,
                        ID_EVALUADO = oCuestionario.ID_EVALUADO,
                        ID_EVALUADO_EVALUADOR = oCuestionario.ID_EVALUADO_EVALUADOR,
                        ID_EVALUADOR = oCuestionario.ID_EVALUADOR,
                        XML_CATALOGOS = oCuestionario.XML_CATALOGOS,
                        XML_PREGUNTAS_ADICIONALES = oCuestionario.XML_PREGUNTAS_ADICIONALES
                    };

                    return oCuestionarioExterno;
                }
                else 
                {
                    return null;
                }
            }
        }

        public E_CUESTIONARIO ObtenerCuestionarioEvaluacion(int? pIdCuestionario = null, int? pIdEvaluado = null, int? pIdEvaluadoEvaluador = null, int? pIdEvaluador = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {

                var oCuestionario = contexto.SPE_OBTIENE_CUESTIONARIOS_EVALUACION(pIdCuestionario, pIdEvaluado, pIdEvaluadoEvaluador, pIdEvaluador).FirstOrDefault();

                if (oCuestionario != null)
                {
                    E_CUESTIONARIO oCuestionarioExterno = new E_CUESTIONARIO
                    {
                        FG_CONTESTADO = oCuestionario.FG_CONTESTADO,
                        ID_CUESTIONARIO = oCuestionario.ID_CUESTIONARIO,
                        ID_CUESTIONARIO_PLANTILLA = oCuestionario.ID_CUESTIONARIO_PLANTILLA,
                        ID_EVALUADO = oCuestionario.ID_EVALUADO,
                        ID_EVALUADO_EVALUADOR = oCuestionario.ID_EVALUADO_EVALUADOR,
                        ID_EVALUADOR = oCuestionario.ID_EVALUADOR,
                        XML_CATALOGOS = oCuestionario.XML_CATALOGOS,
                        XML_PREGUNTAS_ADICIONALES = oCuestionario.XML_PREGUNTAS_ADICIONALES
                    };

                    return oCuestionarioExterno;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<SPE_OBTIENE_FYD_ROLES_EVALUADOR_PERIODO_Result> ObtenerRolesEvaluador(int pIdPeriodo)
        {
            using (contexto = new SistemaSigeinEntities())
            {

                return contexto.SPE_OBTIENE_FYD_ROLES_EVALUADOR_PERIODO(pIdPeriodo).ToList();
            }
        }

        public List<SPE_OBTIENE_C_CATALOGO_VALOR_Result> ObtenerValorCatalogo(int? pIdCatalogoValor = null, string pClCatalogoValor = null, string pNbCatalogoValor = null, string pDsCatalogoValor = null, int? pIdCatalogoLista = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_C_CATALOGO_VALOR(pIdCatalogoValor, pClCatalogoValor, pNbCatalogoValor, pDsCatalogoValor, pIdCatalogoLista).ToList();
            }
        }

        public XElement InsertarActualizarCuestionariosAdicionales(int pIdPeriodo, XElement pXmlEvaluados, XElement pXmlEvaluadores, string pClRolEvaluador, bool pFgCrearCuestionarios, string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_ACTUALIZA_FYD_CUESTIONARIOS_ADICIONALES(pOutClRetorno, pIdPeriodo, pXmlEvaluados.ToString(), pClRolEvaluador, pXmlEvaluadores.ToString(), pFgCrearCuestionarios, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public SPE_OBTIENE_FYD_CUESTIONARIO_EVALUADO_EVALUADOR_Result ObtenerCuestionarioEvaluadoEvaludor(int pIdPeriodo, int? pIdEvaluado, int? pIdEvaluadoEvaluador)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_CUESTIONARIO_EVALUADO_EVALUADOR(pIdPeriodo, pIdEvaluado, pIdEvaluadoEvaluador).FirstOrDefault();
            }
        }

        public List<SPE_OBTIENE_FYD_EVALUADORES_TOKEN_Result> ObtenerTokenEvaluadores(int pIdPeriodo, int? pIdEmpresa = null, int? pIdRol = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_EVALUADORES_TOKEN(pIdPeriodo, pIdEmpresa, pIdRol).ToList();
            }
        }

        public XElement InsertarActualizarTokenEvaluadores(int pIdPeriodo, XElement pXmlTokenEvaluadores, string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_ACTUALIZA_FYD_EVALUADORES_TOKEN(pOutClRetorno, pIdPeriodo, pXmlTokenEvaluadores.ToString(), pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement ActualizarCorreoEvaluador(string pXmlEvaluadores, string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClretorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ACTUALIZA_FYD_EVALUADORES_CORREO(pOutClretorno, pXmlEvaluadores, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClretorno.Value.ToString());
            }
        }

        public XElement ActualizarEstatusDocumentoAutorizacion(Guid? pFlAutorizacion, string pClEstado, string pDsNotas, DateTime? pFeAutorizacion, string pClUsuarioModifica, string pProgramaModifica)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ACTUALIZA_K_AUTORIZACION_DCTO_EMPLEADO(pout_clave_retorno, pFlAutorizacion, pClEstado, pDsNotas, pFeAutorizacion, pClUsuarioModifica, pProgramaModifica);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement EliminarPeriodoEvaluación(int pIdPeriodo)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ELIMINA_PERIODO_EVALUACION(pOutClaveRetorno, pIdPeriodo);
                return XElement.Parse(pOutClaveRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_PLANEACION_MATRIZ_CUESTIONARIOS_Result> ObtenerPlaneacionMatriz(int pIdPeriodo, string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_PLANEACION_MATRIZ_CUESTIONARIOS(pIdPeriodo, pClUsuario, pNbPrograma).ToList();
            }
        }

        public XElement InsertarActualizarCuestionariosMatriz(int pIdPeriodo, string pXmlMatriz, bool pFgCrearCuestionarios, string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_ACTUALIZA_EVALUADOR_MATRIZ(pOutClaveRetorno, pIdPeriodo, pXmlMatriz, pFgCrearCuestionarios, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClaveRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_EMPLEADOS_Result> ObtenerEmpleados(XElement pXmlSeleccion = null, bool? pFgFoto = null, string pClUsuario = null, bool? pFgActivo = null, int? pIdEmpresa = null, int? pIdRol = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                string sXmlSeleccion = null;

                if (pXmlSeleccion != null)
                {
                    sXmlSeleccion = pXmlSeleccion.ToString();
                }
                else
                {
                    sXmlSeleccion = (new XElement("SELECCION", new XElement("FILTRO", new XAttribute("CL_TIPO", "TODAS")))).ToString();
                }

                return contexto.SPE_OBTIENE_EMPLEADOS(sXmlSeleccion, pClUsuario, pFgActivo, pFgFoto, pIdEmpresa, pIdRol).ToList();
            }
        }

        public XElement InsertaCuestionarioAutoevaluacion(int pIdPeriodo,int pIdEvaluado,bool pFgCrearCuestinario,string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_FYD_CUESTIONARIO_AUTOEVALUACION(pOutClaveRetorno, pIdPeriodo, pIdEvaluado, pFgCrearCuestinario, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClaveRetorno.Value.ToString());
            }
        }

    }
}
