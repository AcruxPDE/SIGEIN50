using SIGE.AccesoDatos.Implementaciones.TableroControl;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.TableroControl;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Negocio.TableroControl
{
    public class TableroControlNegocio
    {
        public List<SPE_OBTIENE_PERIODO_TABLERO_CONTROL_Result> ObtenerPeriodoTableroControl(int? pIdPeriodo = null, int? pIdPeriodoTablero = null, int? pIdEmpleado = null, int? pIdPuesto = null)
        {
            TableroControlOperaciones oTableroControl = new TableroControlOperaciones();
            return oTableroControl.ObtenerPeriodoTableroControl(pIdPeriodo, pIdPeriodoTablero, pIdEmpleado, pIdPuesto);
        }

        public E_RESULTADO InsertarTableroControl(E_TABLERO_CONTROL pTableroControl, string pClUsuario, string pNbPrograma)
        {
            TableroControlOperaciones oTableroControl = new TableroControlOperaciones();
            return UtilRespuesta.EnvioRespuesta(oTableroControl.InsertarTableroControl(pTableroControl, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO InsertarTableroControlSucesion(int? pIdPeriodo, int? pIdEmpleado, int? pIdPuesto, string pXmlPeriodos, string pClUsuario, string pNbPrograma)
        {
           TableroControlOperaciones oTableroControl = new TableroControlOperaciones();
           return UtilRespuesta.EnvioRespuesta(oTableroControl.InsertarTableroControlSucesion(pIdPeriodo, pIdEmpleado, pIdPuesto, pXmlPeriodos, pClUsuario, pNbPrograma));
        }

        public List<SPE_OBTIENE_TABLERO_CONTROL_EVALUADOS_Result> ObtieneTableroControlEvaluados(int? pIdPeriodo = null,int? pIdEmpresa = null)
        {
            TableroControlOperaciones oTableroControl = new TableroControlOperaciones();
            return oTableroControl.ObtenerTableroControlEvaluados(pIdPeriodo, pIdEmpresa);
        }

        public E_RESULTADO InsertarEvaluadosTableroControl(int pIdPeriodo, string pXmlIdSeleccionados, string pClUsuario, string pNbPrograma)
        {
            TableroControlOperaciones oTableroControl = new TableroControlOperaciones();
            return UtilRespuesta.EnvioRespuesta(oTableroControl.InsertarEvaluadosTableroControl(pIdPeriodo, pXmlIdSeleccionados, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO EliminarEvaluadoresTableroControl(int pIdPeriodo, string pXmlIdSeleccionados, string pClUsuario, string pNbPrograma)
        {
            TableroControlOperaciones oTableroControl = new TableroControlOperaciones();
            return UtilRespuesta.EnvioRespuesta(oTableroControl.EliminarEvaluadoresTableroControl(pIdPeriodo, pXmlIdSeleccionados, pClUsuario, pNbPrograma));
        }

        public List<SPE_OBTIENE_CANDIDATO_EVALUADO_Result> ObtenerRelacionEvaluadosCandidatos(int pIdPeriodo)
        {
            TableroControlOperaciones oTableroControl = new TableroControlOperaciones();
            return oTableroControl.ObtenerRelacionEvaluadosCandidatos(pIdPeriodo);
        }

        public List<SPE_OBTIENE_PERIODOS_PARA_TABLERO_Result> ObtenerPeriodosParaTablero(int pIdPeriodoTableroControl, string pClTipoPeriodo)
        {
            TableroControlOperaciones oTableroControl = new TableroControlOperaciones();
            return oTableroControl.ObtenerPeriodosParaTablero(pIdPeriodoTableroControl, pClTipoPeriodo);
        }

        public E_RESULTADO InsertarPeriodosReferenciaTableroControl(int pIdPeriodo, string pClTipoPeriodo, string pXmlIdSeleccionados, string pClUsuario, string pNbPrograma)
        {
            TableroControlOperaciones oTableroControl = new TableroControlOperaciones();
            return UtilRespuesta.EnvioRespuesta(oTableroControl.InsertarPeriodosReferenciaTableroControl(pIdPeriodo, pClTipoPeriodo, pXmlIdSeleccionados, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO EliminaPeriodosReferenciaTableroControl(int pIdPeriodoTablero, string pXmlIdSeleccionados)
        {
            TableroControlOperaciones oTableroControl = new TableroControlOperaciones();
            return UtilRespuesta.EnvioRespuesta(oTableroControl.EliminaPeriodosReferenciaTableroControl(pIdPeriodoTablero, pXmlIdSeleccionados));
        }

        public E_RESULTADO EliminaPeriodoTableroControl(int pIdPeriodoTablero)
        {
            TableroControlOperaciones oTableroControl = new TableroControlOperaciones();
            return UtilRespuesta.EnvioRespuesta(oTableroControl.EliminaPeriodoTableroControl(pIdPeriodoTablero));
        }

        public List<SPE_OBTIENE_PERIODOS_TABLERO_CONTROL_Result> ObtenerPeriodosReferenciaTableroControl(int pIdTableroControl, string pClTipoPeriodo)
        {
            TableroControlOperaciones oTableroControl = new TableroControlOperaciones();
            return oTableroControl.ObtenerPeriodosReferenciaTableroControl(pIdTableroControl, pClTipoPeriodo);
        }

        public List<E_PERIODOS_EVALUADOS> ObtenerPeriodosEvaluadosTableroControl(int? pIdTableroControl, int? pIdEmpleado)
        {
            TableroControlOperaciones oTableroControl = new TableroControlOperaciones();
            var vColumnas = oTableroControl.ObtenerPeriodosEvaluadosTableroControl(pIdTableroControl, pIdEmpleado).ToList();
            return (from x in vColumnas
                    select new E_PERIODOS_EVALUADOS
                    {
                        NUMERO_PERIODO = x.NUMERO_PERIODO,
                        ID_PERIODO  = (int)x.ID_PERIODO,
                        CL_PERIODO = x.CL_PERIODO,
                        ID_PERIODO_REFERENCIA = (int)x.ID_PERIODO_REFERENCIA,
                        CL_TIPO_PERIODO_REFERENCIA = x.CL_TIPO_PERIODO_REFERENCIA,
                        NB_TIPO_PERIODO_REFERENCIA = x.NB_TIPO_PERIODO_REFERENCIA,
                        DS_PERIODO = x.DS_PERIODO,
                        FE_PERIODO = x.FE_PERIODO,
                        CL_TABULADOR = x.CL_TABULADOR,
                        FE_TABULADOR = x.FE_TABULADOR,
                        FG_EVALUACION_IDP = x.FG_EVALUACION_IDP,
                        FG_EVALUACION_FYD = x.FG_EVALUACION_FYD,
                        FG_EVALUCION_ED = x.FG_EVALUCION_ED
                    }
                     ).ToList();
        }

        public List<E_EVALUADOS_TABLERO_CONTROL> ObtenerEvaluadosTableroControl(int? pIdTableroControl, int? pIdEmpleado, int? pIdRol)
        {
            TableroControlOperaciones oTableroControl = new TableroControlOperaciones();
            var vEvaluados = oTableroControl.ObtenerEvaluadosTableroControl(pIdTableroControl, pIdEmpleado, pIdRol).ToList();
            return (from x in vEvaluados
                    select new E_EVALUADOS_TABLERO_CONTROL
                    {
                        ID_EVALUADO = x.ID_EVALUADO,
                        CL_EVALUADO = x.CL_EVALUADO,
                        ID_EMPLEADO = x.ID_EMPLEADO,
                        CL_EMPLEADO = x.CL_EMPLEADO,
                        NB_EMPLEADO = x.NB_EMPLEADO,
                        ID_PUESTO_PERIODO = x.ID_PUESTO_PERIODO,
                        CL_PUESTO = x.CL_PUESTO,
                        NB_PUESTO = x.NB_PUESTO,
                        CL_DEPARTAMENTO = x.CL_DEPARTAMENTO,
                        NB_DEPARTAMENTO = x.NB_DEPARTAMENTO,
                        DS_COMENTARIO = x.DS_COMENTARIO,
                        XML_CAMPOS_ADICIONALES = x.XML_CAMPOS_ADICIONALES,
                        FI_FOTOGRAFIA = x.FI_FOTOGRAFIA
                    }).ToList();
        }

        public List<SPE_OBTIENE_RESULTADOS_FYD_TABLERO_Result> ObtenerResultadosFyDTableroControl(int? pIdTableroControl, int? pIdEmpleado, int? pIdPuesto)
        {
            TableroControlOperaciones oTableroControl = new TableroControlOperaciones();
            return oTableroControl.ObtenerResultadosFyDTableroControl(pIdTableroControl, pIdEmpleado, pIdPuesto);
        }

        public List<SPE_OBTIENE_RESULTADOS_ED_TABLERO_Result> ObtenerResultadosEDTableroControl(int? pIdTableroControl, int? pIdEmpleado)
        {
            TableroControlOperaciones oTableroControl = new TableroControlOperaciones();
            return oTableroControl.ObtenerResultadosEDTableroControl(pIdTableroControl, pIdEmpleado);
        }

        public List<SPE_OBTIENE_RESULTADOS_TABULADORES_TABLERO_Result> ObtenerTabuladoresTableroControl(int? pIdTableroControl, int? pIdEmpleado)
        {
            TableroControlOperaciones oTableroControl = new TableroControlOperaciones();
            return oTableroControl.ObtenerTabuladoresTableroControl(pIdTableroControl, pIdEmpleado);
        }

        public List<SPE_OBTIENE_COMPATIBILIDAD_PUESTO_TABLERO_Result> ObtenerCompatibilidadPuestoTablero(int? pIdTableroControl, int? pIdEmpleado, int? pIdPuesto)
        {
            TableroControlOperaciones oTableroControl = new TableroControlOperaciones();
            return oTableroControl.ObtenerCompatibilidadPuestoTablero(pIdTableroControl, pIdEmpleado, pIdPuesto);
        }

        public List<SPE_OBTIENE_RESULTADOS_CLIMA_LABORAL_TABLERO_Result> ObtenerClimaLaboralTablero(int? pIdTableroControl, int? pIdEmpleado)
        {
            TableroControlOperaciones oTableroControl = new TableroControlOperaciones();
            return oTableroControl.ObtenerClimaLaboralTablero(pIdTableroControl, pIdEmpleado);
        }

        public E_RESULTADO ActualizaPonderaciones(int? pIdPeriodo, decimal? pPonderacionIdp, decimal? pPonderacionFyd, decimal? pPonedracionDesempeno, decimal? pPonderacionClima, string pClUsuario, string pNbPrograma)
        {
            TableroControlOperaciones oTableroControl = new TableroControlOperaciones();
            return UtilRespuesta.EnvioRespuesta(oTableroControl.ActualizaPonderaciones(pIdPeriodo, pPonderacionIdp, pPonderacionFyd, pPonedracionDesempeno, pPonderacionClima, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO ActualizaEstatusTablero(int? pIdPeriodo, string pXmlComentarios, string pClUsuario, string pNbPrograma)
        {
            TableroControlOperaciones oTableroControl = new TableroControlOperaciones();
            return UtilRespuesta.EnvioRespuesta(oTableroControl.ActualizaEstatusTablero(pIdPeriodo,pXmlComentarios, pClUsuario, pNbPrograma));
        }


        public E_RESULTADO CopiaConsultatablero(int? pIdPeriodo, string pClUsuario, string pNbPrograma)
        {
            TableroControlOperaciones oTableroControl = new TableroControlOperaciones();
            return UtilRespuesta.EnvioRespuesta(oTableroControl.CopiaConsultatablero(pIdPeriodo, pClUsuario, pNbPrograma));
        }
        

    }
}
