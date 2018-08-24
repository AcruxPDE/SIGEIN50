using OfficeOpenXml;
using SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Xml.Linq;

namespace SIGE.Negocio.FormacionDesarrollo
{
    public class PeriodoNegocio
    {
        public List<SPE_OBTIENE_FYD_PERIODOS_EVALUACION_Result> ObtienePeriodosEvaluacion(int? pIdPeriodo = null, int? pIdEmpleado = null)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return oPeriodo.ObtenerPeriodosEvaluacion(pIdPeriodo, pIdEmpleado);
        }

        public SPE_OBTIENE_FYD_PERIODO_EVALUACION_Result ObtienePeriodoEvaluacion(int pIdPeriodo)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return oPeriodo.ObtenerPeriodoEvaluacion(pIdPeriodo);
        }

         public SPE_OBTIENE_FYD_VALIDACION_AUTORIZACIONES_Result ObtenerValidacionAutorizacion(int pIdPeriodo)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return oPeriodo.ObtenerValidacionAutorizacion(pIdPeriodo);
        }

        public SPE_OBTIENE_CONFIGURACION_PERIODO_COMPETENCIAS_Result VerificaConfiguracion(int? pIdPeriodo = null, string pClPeriodo = null, string pNbPeriodo = null)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return oPeriodo.VerificaConfiguracion(pIdPeriodo, pClPeriodo, pNbPeriodo);
        }

        public List<SPE_OBTIENE_FYD_EVALUADOS_CONFIGURACION_Result> ObtieneEvaluados(int pIdPeriodo, int? pID_EMPRESA = null, int? pID_ROL = null)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return oPeriodo.ObtenerEvaluados(pIdPeriodo, pID_EMPRESA, pID_ROL);
        }

        public List<SPE_OBTIENE_FYD_CUESTIONARIOS_EVALUADOS_Result> ObtieneEvaluadosCuestionarios(int pIdPeriodo, int? pIdEmpresa = null, int? pIdRol = null)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return oPeriodo.ObtenerEvaluadosCuestionarios(pIdPeriodo, pIdEmpresa, pIdRol);
        }

        public List<SPE_OBTIENE_FYD_EVALUADOS_AUTORIZACION_Result> ObtenerEvaluadosEvaluadores(int pIdPeriodo, int? pIdEmpresa = null)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return oPeriodo.ObtenerEvaluadosEvaluadores(pIdPeriodo, pIdEmpresa);
        }

        public E_RESULTADO InsertaActualizaPeriodoEvaluacionCompetencias(int? pIdPeriodo, string pClPeriodo, string pNbPeriodo, string pDsPeriodo, bool pFeInicio, string pFeTermino, string pClEstado, string pClTipo, string pDsNotas, int? pIdBitacora, string pXmlCamposAdicionales, int? pIdPeriodoEvaluacion, bool pFgAutoevaluacion, bool pFgSupervisor, bool pFgSubordinados, bool pFgInterrelacionados, bool pFgOtros, bool pFgPeriodoPVC, string pClUsuario, string pNbPrograma, string pClTipoTransaccion)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertarActualizarPeriodoEvaluacionCompetencias(pIdPeriodo, pClPeriodo, pNbPeriodo, pDsPeriodo, pFeInicio, pFeTermino, pClEstado, pClTipo, pDsNotas, pIdBitacora, pXmlCamposAdicionales, pIdPeriodoEvaluacion, pFgAutoevaluacion, pFgSupervisor, pFgSubordinados, pFgInterrelacionados, pFgOtros, pFgPeriodoPVC, pClUsuario, pNbPrograma, pClTipoTransaccion));
        }

        public E_RESULTADO InsertarCopiaPeriodoEvaluacionCompetencias(int? pIdPeriodo, string pClPeriodo, string pNbPeriodo, string pDsPeriodo, bool pFeInicio, string pFeTermino, string pClEstado, string pClTipo, string pDsNotas, int? pIdBitacora, string pXmlCamposAdicionales, int? pIdPeriodoEvaluacion, bool pFgAutoevaluacion, bool pFgSupervisor, bool pFgSubordinados, bool pFgInterrelacionados, bool pFgOtros, bool pFgPeriodoPVC, string pClUsuario, string pNbPrograma, string pClTipoTransaccion)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertarCopiaPeriodoEvaluacionCompetencias(pIdPeriodo, pClPeriodo, pNbPeriodo, pDsPeriodo, pFeInicio, pFeTermino, pClEstado, pClTipo, pDsNotas, pIdBitacora, pXmlCamposAdicionales, pIdPeriodoEvaluacion, pFgAutoevaluacion, pFgSupervisor, pFgSubordinados, pFgInterrelacionados, pFgOtros, pFgPeriodoPVC, pClUsuario, pNbPrograma, pClTipoTransaccion));
        }

        public E_RESULTADO InsertaEvaluados(int pIdPeriodo, XElement pXmlEvaluados, string pClUsuario, string pNbPrograma)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertarEvaluados(pIdPeriodo, pXmlEvaluados, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO InsertaOtrosEvaluadoresInventario(int pIdPeriodo, XElement pXmlOtrosEvaluadores, bool pFgEvaluaTodos, string pClUsuario, string pNbPrograma)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertarOtrosEvaluadoresInventario(pIdPeriodo, pXmlOtrosEvaluadores, pFgEvaluaTodos, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO InsertaActualizaOtrosEvaluadoresExternos(int pIdPeriodo, int? pIdEmpleado, int? pIdEvaluador, string pNbEvaluador, string pNbPuesto, string pClCorreoElectronico, bool pFgEvaluaTodos, string pClUsuario, string pNbPrograma, E_TIPO_OPERACION_DB pClTipoOperacion)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertarActualizaOtrosEvaluadoresExterno(pIdPeriodo, pIdEmpleado, pIdEvaluador, pNbEvaluador, pNbPuesto, pClCorreoElectronico, pFgEvaluaTodos, pClUsuario, pNbPrograma, pClTipoOperacion));
        }

        public E_RESULTADO EliminaEvaluados(int pIdPeriodo, XElement pXmlEvaluados, string pClUsuario, string pNbPrograma)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.EliminarEvaluados(pIdPeriodo, pXmlEvaluados, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO EliminaEvaluador(int pIdEvaluador, string pClUsuario, string pNbPrograma)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.EliminarEvaluador(pIdEvaluador, pClUsuario, pNbPrograma));
        }

        public SPE_OBTIENE_FYD_PERIODO_EVALUADOR_Result ObtienePeriodoEvaluador(int? pIdPeriodoEvaluador = null, Guid? pFlEvaluador = null)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return oPeriodo.ObtenerPeriodoEvaluador(pIdPeriodoEvaluador, pFlEvaluador);
        }

        public SPE_OBTIENE_FYD_EVALUADO_Result ObtieneEvaluado(int? pIdEvaluadoEvaluador = null, int? pIdEvaluado = null)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return oPeriodo.ObtenerEvaluado(pIdEvaluadoEvaluador, pIdEvaluado);
        }

        public List<SPE_OBTIENE_FYD_PREGUNTAS_EVALUACION_Result> ObtienePreguntas(int pIdEvaluadoEvaluador)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return oPeriodo.ObtenerPreguntas(pIdEvaluadoEvaluador);
        }

        public List<SPE_OBTIENE_EO_PREGUNTAS_EVALUACION_Result> ObtienePreguntasEO(int pIdEvaluadoEvaluador)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return oPeriodo.ObtenerPreguntasEO(pIdEvaluadoEvaluador);
        }

        public List<SPE_OBTIENE_EO_RESULTADO_EVALUADOS_Result> ObtieneResultadosCuestionariosEO(int pIdPeriodo)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return oPeriodo.ObtenerResultadosCuestionariosEO(pIdPeriodo);
        }

        public List<SPE_OBTIENE_FYD_PUESTOS_EVALUADO_Result> ObtienePuestosEvaluado(int pIdEvaluado)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return oPeriodo.ObtenerPuestosEvaluado(pIdEvaluado);
        }

        public E_RESULTADO InsertaActualizaOtrosPuestosEvaluados(int pIdPeriodo, XElement pXmlEvaluados, XElement pXmlPuestos, string pClUsuario, string pNbPrograma)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertarActualizarOtrosPuestosEvaluados(pIdPeriodo, pXmlEvaluados, pXmlPuestos, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO EliminaPuestoEvaluado(int pIdPuestoEvaluado)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.EliminarPuestoEvaluado(pIdPuestoEvaluado));
        }

        public E_RESULTADO ActualizaConfiguracionPeriodo(int pIdPeriodo, XElement pXmlConfiguracion, string pClUsuario, string pNbPrograma)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.ActualizarConfiguracionPeriodo(pIdPeriodo, pXmlConfiguracion, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO InsertaCuestionarios(int pIdPeriodo, bool pFgCreaCuestionarios, string pClUsuario, string pNbPrograma)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertarCuestionarios(pIdPeriodo, pFgCreaCuestionarios, pClUsuario, pNbPrograma));
        }

        public List<SPE_OBTIENE_FYD_EVALUADO_CUESTIONARIOS_Result> ObtieneCuestionariosEvaluado(int pIdEvaluado)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return oPeriodo.ObtenerCuestionariosEvaluado(pIdEvaluado);
        }

        public List<SPE_OBTIENE_FYD_EVALUADORES_AUTORIZACION_Result> ObtenerEvaluadoresAutorizacion(int pIdEvaluado)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return oPeriodo.ObtenerEvaluadoresAutorizacion(pIdEvaluado);
        }

        public E_RESULTADO ActualizaEstatusPeriodo(int pIdPeriodo, string pClEstatus, string pClUsuario, string pNbPrograma)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.ActualizarEstatusPeriodo(pIdPeriodo, pClEstatus, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO InsertaPreguntasAdicionales(int? pIdPeriodo = null,int? pIdPregunta = null, string pNbPregunta = null, XElement pXmlPreguntasAdicionales = null, string pClCuestionarioObjetivo = null, string pClUsuario = null, string pNbPrograma = null,string pClTipoTransaccion = null)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertarPreguntasAdicionales(pIdPeriodo, pIdPregunta, pNbPregunta, pXmlPreguntasAdicionales, pClCuestionarioObjetivo, pClUsuario, pNbPrograma, pClTipoTransaccion));
        }

        public List<SPE_OBTIENE_FYD_PREGUNTAS_ADICIONALES_PERIODO_Result> ObtienePreguntasAdicionales(int? pIdPeriodo = null, int? pIdPregunta = null)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return oPeriodo.ObtenerPreguntasAdicionales(pIdPeriodo, pIdPregunta);
        }

        public E_RESULTADO EliminaPreguntaAdicional(string pXmlPreguntas = null)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.EliminarPreguntaAdicional(pXmlPreguntas));
        }

        public E_RESULTADO EliminaCuestionario(int pIdCuestionario)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.EliminaCuestionario(pIdCuestionario));
        }

        public E_RESULTADO ActualizaRespuestaCuestionario(string pXmlRespuestas, string pClUsuario, string pNbPrograma)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.ActualizarRepuestasCuestionario(pXmlRespuestas, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO InsertaResultadoEvaluacionCompetencia(int pIdPeriodo, int pIdEvaluado, string pClUsuario, string pNbPrograma)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertarResultadoEvaluacionCompetencia(pIdPeriodo, pIdEvaluado, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO TerminaCuestinario(string pXmlRespuestas, int pIdPeriodo, int pIdEvaluado, int pIdEvaluadoEvaluador, int pNoValor, bool pFgTerminado, string pClUsuario, string pNbPrograma, string pXmlRespuestasAdicionales = null)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.TerminarCuestionario(pXmlRespuestas, pIdPeriodo, pIdEvaluado, pIdEvaluadoEvaluador, pNoValor, pFgTerminado, pClUsuario, pNbPrograma, pXmlRespuestasAdicionales));
        }

        public E_CUESTIONARIO ObtieneCuestionario(int? pIdCuestionario = null, int? pIdEvaluado = null, int? pIdEvaluadoEvaluador = null, int? pIdEvaluador = null)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();

            var oCuestionario = oPeriodo.ObtenerCuestionario(pIdCuestionario, pIdEvaluado, pIdEvaluadoEvaluador, pIdEvaluador);
            int vIdCatalogo;
            string vClValor;
            if (oCuestionario != null)
                if (oCuestionario.XML_CATALOGOS != null)
                {
                    XElement vXmlPreguntasAdicionales = XElement.Parse(oCuestionario.XML_PREGUNTAS_ADICIONALES);
                    XElement vXmlCatalogo = XElement.Parse(oCuestionario.XML_CATALOGOS);

                    foreach (XElement itemPregunta in vXmlPreguntasAdicionales.Elements("CAMPO"))
                    {
                        if (itemPregunta.Attribute("ID_CATALOGO") != null)
                        {
                            vIdCatalogo = UtilXML.ValorAtributo<int>(itemPregunta.Attribute("ID_CATALOGO"));
                            vClValor = UtilXML.ValorAtributo<string>(itemPregunta.Attribute("NB_VALOR")) == null ? UtilXML.ValorAtributo<string>(itemPregunta.Attribute("NO_VALOR_DEFECTO")) : UtilXML.ValorAtributo<string>(itemPregunta.Attribute("NB_VALOR"));
                            XElement vXmlCatalogoFiltrado = new XElement("ITEMS");

                            foreach (XElement itemCatalogo in vXmlCatalogo.Elements("ITEM"))
                            {
                                if (UtilXML.ValorAtributo<int>(itemCatalogo.Attribute("ID_CATALOGO_VALOR")) == vIdCatalogo)
                                {

                                    if (UtilXML.ValorAtributo<string>(itemCatalogo.Attribute("NB_VALOR")) == vClValor)
                                    {
                                        UtilXML.AsignarValorAtributo(itemCatalogo, "FG_SELECCIONADO", "1");
                                    }
                                    else
                                    {
                                        UtilXML.AsignarValorAtributo(itemCatalogo, "FG_SELECCIONADO", "0");
                                    }

                                    vXmlCatalogoFiltrado.Add(itemCatalogo);
                                }
                            }

                            itemPregunta.Add(vXmlCatalogoFiltrado);
                        }
                    }

                    oCuestionario.XML_PREGUNTAS_CATALOGOS_ADICIONALES = vXmlPreguntasAdicionales.ToString();

                }

            return oCuestionario;
        }

        public E_CUESTIONARIO ObtenerCuestionarioEvaluacion(int? pIdCuestionario = null, int? pIdEvaluado = null, int? pIdEvaluadoEvaluador = null, int? pIdEvaluador = null)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();

            var oCuestionario = oPeriodo.ObtenerCuestionarioEvaluacion(pIdCuestionario, pIdEvaluado, pIdEvaluadoEvaluador, pIdEvaluador);
            int vIdCatalogo;
            string vClValor;
            if (oCuestionario != null)
                if (oCuestionario.XML_CATALOGOS != null)
                {
                    XElement vXmlPreguntasAdicionales = XElement.Parse(oCuestionario.XML_PREGUNTAS_ADICIONALES);
                    XElement vXmlCatalogo = XElement.Parse(oCuestionario.XML_CATALOGOS);

                    foreach (XElement itemPregunta in vXmlPreguntasAdicionales.Elements("CAMPO"))
                    {
                        if (itemPregunta.Attribute("ID_CATALOGO") != null)
                        {
                            vIdCatalogo = UtilXML.ValorAtributo<int>(itemPregunta.Attribute("ID_CATALOGO"));
                            vClValor = UtilXML.ValorAtributo<string>(itemPregunta.Attribute("NB_VALOR")) == null ? UtilXML.ValorAtributo<string>(itemPregunta.Attribute("NO_VALOR_DEFECTO")) : UtilXML.ValorAtributo<string>(itemPregunta.Attribute("NB_VALOR"));
                            XElement vXmlCatalogoFiltrado = new XElement("ITEMS");

                            foreach (XElement itemCatalogo in vXmlCatalogo.Elements("ITEM"))
                            {
                                if (UtilXML.ValorAtributo<int>(itemCatalogo.Attribute("ID_CATALOGO_VALOR")) == vIdCatalogo)
                                {

                                    if (UtilXML.ValorAtributo<string>(itemCatalogo.Attribute("NB_VALOR")) == vClValor)
                                    {
                                        UtilXML.AsignarValorAtributo(itemCatalogo, "FG_SELECCIONADO", "1");
                                    }
                                    else
                                    {
                                        UtilXML.AsignarValorAtributo(itemCatalogo, "FG_SELECCIONADO", "0");
                                    }

                                    vXmlCatalogoFiltrado.Add(itemCatalogo);
                                }
                            }

                            itemPregunta.Add(vXmlCatalogoFiltrado);
                        }
                    }

                    oCuestionario.XML_PREGUNTAS_CATALOGOS_ADICIONALES = vXmlPreguntasAdicionales.ToString();

                }

            return oCuestionario;
        }

        public List<SPE_OBTIENE_FYD_ROLES_EVALUADOR_PERIODO_Result> ObtieneRolesEvaluador(int pIdPeriodo)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return oPeriodo.ObtenerRolesEvaluador(pIdPeriodo);
        }

        public List<SPE_OBTIENE_C_CATALOGO_VALOR_Result> ObtenerValorCatalogo(int? pIdCatalogoValor = null, string pClCatalogoValor = null, string pNbCatalogoValor = null, string pDsCatalogoValor = null, int? pIdCatalogoLista = null)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return oPeriodo.ObtenerValorCatalogo(pIdCatalogoValor, pClCatalogoValor, pNbCatalogoValor, pDsCatalogoValor, pIdCatalogoLista);
        }
        

        public E_RESULTADO InsertaActualizaCuestionariosAdicionales(int pIdPeriodo, XElement pXmlEvaluados, XElement pXmlEvaluadores, string pClRolEvaluador, bool pFgCreaCuestionarios, string pClUsuario, string pNbPrograma)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertarActualizarCuestionariosAdicionales(pIdPeriodo, pXmlEvaluados, pXmlEvaluadores, pClRolEvaluador, pFgCreaCuestionarios, pClUsuario, pNbPrograma));
        }

        public SPE_OBTIENE_FYD_CUESTIONARIO_EVALUADO_EVALUADOR_Result ObtieneCuestionarioEvaluadoEvaludor(int pIdPeriodo, int? pIdEvaluado, int? pIdEvaluadoEvaluador)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return oPeriodo.ObtenerCuestionarioEvaluadoEvaludor(pIdPeriodo, pIdEvaluado, pIdEvaluadoEvaluador);
        }

        public List<SPE_OBTIENE_FYD_EVALUADORES_TOKEN_Result> ObtieneTokenEvaluadores(int pIdPeriodo, int? pIdEmpresa = null, int? pIdRol = null)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return oPeriodo.ObtenerTokenEvaluadores(pIdPeriodo, pIdEmpresa, pIdRol);
        }

        public E_RESULTADO InsertarActualizarTokenEvaluadores(int pIdPeriodo, int? pIdEvaluador, string pClUsuario, string pNbPrograma, int? pIdRol)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();

            XElement vXmlEvaluadores = new XElement("EVALUADORES");

            List<SPE_OBTIENE_FYD_EVALUADORES_TOKEN_Result> vLstEvaluadores = new List<SPE_OBTIENE_FYD_EVALUADORES_TOKEN_Result>();
            if (pIdEvaluador == null)
                vLstEvaluadores = oPeriodo.ObtenerTokenEvaluadores(pIdPeriodo, pIdRol: pIdRol);
            else
                vLstEvaluadores.Add(new SPE_OBTIENE_FYD_EVALUADORES_TOKEN_Result() { ID_EVALUADOR = pIdEvaluador ?? 0 });

            if (vLstEvaluadores.Count > 0)
                vLstEvaluadores.ForEach(f => vXmlEvaluadores.Add(new XElement("EVALUADOR", new XAttribute("ID_EVALUADOR", f.ID_EVALUADOR), new XAttribute("CL_TOKEN", Membership.GeneratePassword(12, 1)))));

            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertarActualizarTokenEvaluadores(pIdPeriodo, vXmlEvaluadores, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO ActualizarCorreoEvaluador(string pXmlEvaluadores, string pClUsuario, string pNbPrograma)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.ActualizarCorreoEvaluador(pXmlEvaluadores, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO ActualizaEstatusDocumentoAutorizacion(Guid? pFlAutorizacion, string pClEstado, string pDsNotas, DateTime? pFeAutorizacion, string pClUsuarioModifica, string pProgramaModifica)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.ActualizarEstatusDocumentoAutorizacion(pFlAutorizacion, pClEstado, pDsNotas, pFeAutorizacion, pClUsuarioModifica, pProgramaModifica));
        }

        public E_RESULTADO EliminaPeriodoEvaluación(int pIdPeriodo)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.EliminarPeriodoEvaluación(pIdPeriodo));
        }

        public List<E_PLANEACION_CUESTINOARIOS> ObtienePlaneacionMatriz(int pIdPeriodo, string pClUsuario, string pNbPrograma)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            var vLista = oPeriodo.ObtenerPlaneacionMatriz(pIdPeriodo, pClUsuario, pNbPrograma);

            List<E_PLANEACION_CUESTINOARIOS> vLstEvaluadores = vLista.Select(t => new E_PLANEACION_CUESTINOARIOS
            {
                ID_EVALUADO_EVALUADOR = t.ID_EVALUADO_EVALUADOR,
                ID_EMPLEADO_EVALUADOR = t.ID_EMPLEADO_EVALUADOR.Value,
                CL_EMPLEADO = t.CL_EMPLEADO,
                NB_EMPLEADO_COMPLETO = t.NB_EMPLEADO_COMPLETO,
                ID_PUESTO = t.ID_PUESTO.Value,
                CL_PUESTO = t.CL_PUESTO,
                NB_PUESTO = t.NB_PUESTO,
                CL_ROL_EVALUADOR = t.CL_ROL_EVALUADOR,
                ID_EMPLEADO_EVALUADO = t.ID_EMPLEADO_EVALUADO,
                ID_EVALUADO = t.ID_EVALUADO,
                FG_CUESTIONARIO = t.FG_CREAR_CUESTIONARIO.Value
            }).ToList();

            return vLstEvaluadores;
        }

        public E_RESULTADO InsertaActualizaCuestionariosMatriz(int pIdPeriodo, string pXmlMatriz, bool pFgCrearCuestionarios, string pClUsuario, string pNbPrograma)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertarActualizarCuestionariosMatriz(pIdPeriodo, pXmlMatriz, pFgCrearCuestionarios, pClUsuario, pNbPrograma));
        }

        public List<SPE_OBTIENE_EMPLEADOS_Result> ObtenerEmpleados(XElement pXmlSeleccion = null)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return oPeriodo.ObtenerEmpleados(pXmlSeleccion);
        }

        public E_RESULTADO InsertaCuestionarioAutoevaluacion(int pIdPeriodo, int pIdEvaluado, bool pFgCrearCuestinario, string pClUsuario, string pNbPrograma)
        {
            PeriodoOperaciones oPeriodo = new PeriodoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertaCuestionarioAutoevaluacion(pIdPeriodo, pIdEvaluado, pFgCrearCuestinario, pClUsuario, pNbPrograma));
        }
    }
}
