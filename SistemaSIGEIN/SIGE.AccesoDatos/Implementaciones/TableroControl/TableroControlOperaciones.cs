using SIGE.Entidades;
using SIGE.Entidades.TableroControl;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.TableroControl
{
    public class TableroControlOperaciones
    {
        private SistemaSigeinEntities context;


        public List<SPE_OBTIENE_PERIODO_TABLERO_CONTROL_Result> ObtenerPeriodoTableroControl(int? pIdPeriodo = null, int? pIdPeriodoTablero = null, int? pIdEmpleado = null, int? pIdPuesto = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_PERIODO_TABLERO_CONTROL(pIdPeriodo, pIdPeriodoTablero, pIdEmpleado, pIdPuesto).ToList();
            }
        }

        public XElement InsertarTableroControl(E_TABLERO_CONTROL pTableroControl, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_PERIODO_TABLERO_CONTROL(pout_clave_retorno,pTableroControl.ID_PERIODO, pTableroControl.CL_PERIODO, pTableroControl.NB_PERIODO, pTableroControl.DS_PERIODO, pTableroControl.DS_NOTAS, pTableroControl.FG_EVALUACION_IDP, pTableroControl.FG_EVALUACION_FYD, pTableroControl.FG_EVALUACION_DESEMPENO, pTableroControl.FG_EVALUACION_CLIMA, pTableroControl.FG_SITUACION_SALARIAL, pClUsuario, pNbPrograma);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }


        public XElement InsertarTableroControlSucesion(int? pIdPeriodo, int? pIdEmpleado, int? pIdPuesto, string pXmlPeriodos, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_TABLERO_CONTROL_SUCESION(pout_clave_retorno,pIdPeriodo, pIdEmpleado,pIdPuesto, pXmlPeriodos, pClUsuario, pNbPrograma);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_TABLERO_CONTROL_EVALUADOS_Result> ObtenerTableroControlEvaluados(int? pIdPeriodo = null, int? pIdEmpresa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_TABLERO_CONTROL_EVALUADOS(pIdPeriodo, pIdEmpresa).ToList();
            }
        }

        public XElement InsertarEvaluadosTableroControl(int pIdPeriodo, string pXmlIdSeleccionados, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_EVALUADOS_TABLERO_CONTROL(pout_clave_retorno, pIdPeriodo, pXmlIdSeleccionados, pClUsuario, pNbPrograma);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement EliminarEvaluadoresTableroControl(int pIdPeriodo, string pXmlIdSeleccionados, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_EVALUADOS_TABLERO_CONTROL(pout_clave_retorno, pIdPeriodo, pXmlIdSeleccionados, pClUsuario, pNbPrograma);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_CANDIDATO_EVALUADO_Result> ObtenerRelacionEvaluadosCandidatos(int pIdPeriodo)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_CANDIDATO_EVALUADO(pIdPeriodo).ToList();
            }
        }

        public List<SPE_OBTIENE_PERIODOS_PARA_TABLERO_Result> ObtenerPeriodosParaTablero(int pIdPeriodoTableroControl, string pClTipoPeriodo)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_PERIODOS_PARA_TABLERO(pIdPeriodoTableroControl, pClTipoPeriodo).ToList();
            }
        }

        public XElement InsertarPeriodosReferenciaTableroControl(int pIdPeriodo, string pClTipoPeriodo, string pXmlIdSeleccionados, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_PERIODOS_REFERENCIA_TABLERO_CONTROL(pout_clave_retorno, pIdPeriodo, pClTipoPeriodo, pXmlIdSeleccionados, pClUsuario, pNbPrograma);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement EliminaPeriodoTableroControl(int pIdPeriodoTablero)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_PERIODO_TABLERO_CONTROL(pout_clave_retorno, pIdPeriodoTablero);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement EliminaPeriodosReferenciaTableroControl(int pIdPeriodoTablero, string pXmlIdSeleccionados)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_PERIODOS_REFERENCIA_TABLERO_CONTROL(pout_clave_retorno, pIdPeriodoTablero, pXmlIdSeleccionados);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_PERIODOS_TABLERO_CONTROL_Result> ObtenerPeriodosReferenciaTableroControl(int pIdTableroControl, string pClTipoPeriodo)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_PERIODOS_TABLERO_CONTROL(pIdTableroControl, pClTipoPeriodo).ToList();   
            }
        }

        public List<SPE_OBTIENE_PERIODOS_EVALUADOS_TABLERO_Result> ObtenerPeriodosEvaluadosTableroControl(int? pIdTableroControl, int? pIdEmpleado)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_PERIODOS_EVALUADOS_TABLERO(pIdTableroControl, pIdEmpleado).ToList();
            }
        }

        public List<SPE_OBTIENE_EMPLEADOS_EVALUADOS_TABLERO_Result> ObtenerEvaluadosTableroControl(int? pIdTableroControl, int? pIdEmpleado)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EMPLEADOS_EVALUADOS_TABLERO(pIdTableroControl, pIdEmpleado).ToList();
            }
        }

        public List<SPE_OBTIENE_RESULTADOS_FYD_TABLERO_Result> ObtenerResultadosFyDTableroControl(int? pIdTableroControl, int? pIdEmpleado, int? pIdPuesto)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_RESULTADOS_FYD_TABLERO(pIdTableroControl, pIdEmpleado, pIdPuesto).ToList();
            }
        }

        public List<SPE_OBTIENE_RESULTADOS_ED_TABLERO_Result> ObtenerResultadosEDTableroControl(int? pIdTableroControl, int? pIdEmpleado)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_RESULTADOS_ED_TABLERO(pIdTableroControl, pIdEmpleado).ToList();
            }
        }

        public List<SPE_OBTIENE_RESULTADOS_TABULADORES_TABLERO_Result> ObtenerTabuladoresTableroControl(int? pIdTableroControl, int? pIdEmpleado)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_RESULTADOS_TABULADORES_TABLERO(pIdTableroControl, pIdEmpleado).ToList();
            }
        }

        public List<SPE_OBTIENE_COMPATIBILIDAD_PUESTO_TABLERO_Result> ObtenerCompatibilidadPuestoTablero(int? pIdTableroControl, int? pIdEmpleado, int? pIdPuesto)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_COMPATIBILIDAD_PUESTO_TABLERO(pIdTableroControl, pIdEmpleado, pIdPuesto).ToList();
            }
        }

        public List<SPE_OBTIENE_RESULTADOS_CLIMA_LABORAL_TABLERO_Result> ObtenerClimaLaboralTablero(int? pIdTableroControl, int? pIdEmpleado)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_RESULTADOS_CLIMA_LABORAL_TABLERO(pIdTableroControl, pIdEmpleado).ToList();
            }
        }

        public XElement ActualizaPonderaciones(int? pIdPeriodo, decimal? pPonderacionIdp, decimal? pPonderacionFyd, decimal? pPonedracionDesempeno, decimal? pPonderacionClima, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ACTUALIZA_PONDERACIONES_TABLERO_CONTROL(pout_clave_retorno, pIdPeriodo, pPonderacionIdp, pPonderacionFyd, pPonedracionDesempeno, pPonderacionClima, pClUsuario, pNbPrograma);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement ActualizaEstatusTablero(int? pIdPeriodo, string pXmlComentarios, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ACTUALIZA_ESTATUS_PERIODO_TABLERO(pout_clave_retorno, pIdPeriodo, pXmlComentarios, pClUsuario, pNbPrograma);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement CopiaConsultatablero(int? pIdPeriodo, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_COPIA_CONSULTA_TABLERO_CONTROL(pout_clave_retorno, pIdPeriodo, pClUsuario, pNbPrograma);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
    }
}
