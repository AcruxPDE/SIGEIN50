using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;

namespace SIGE.Negocio.FormacionDesarrollo
{
    public class ProgramaNegocio
    {
        public List<SPE_OBTIENE_C_PROGRAMA_Result> ObtieneProgramasCapacitacion(int? pIdPrograma = null, string pClPrograma = null, string pNbPrograma = null, string pClTipoPrograma = null, string pClEstado = null, string pClVersion = null, int? pIdDocumentoAutorizacion = null, XElement pXML_PROGRAMAS_SELECCION = null)
        {
            ProgramaOperaciones oPrograma = new ProgramaOperaciones();
            return oPrograma.ObtenerProgramasCapacitacion(pIdPrograma, pClPrograma, pNbPrograma, pClTipoPrograma, pClEstado, pClVersion, pIdDocumentoAutorizacion,pXML_PROGRAMAS_SELECCION);
        }
     
        public List<SPE_OBTIENE_K_PROGRAMA_Result> ObtieneKernelProgramaCapacitacion(int? pIdProgramaEmpleadoCompetencia = null, int? pIdPrograma = null, int? pIdProgramaCompetencia = null, int? pIdProgramaEmpleado = null, string pClPrioridad = null, decimal? prResultado = null)
        {
            ProgramaOperaciones oPrograma = new ProgramaOperaciones();
            return oPrograma.ObtenerKernelProgramasCapacitacion(pIdProgramaEmpleadoCompetencia, pIdPrograma, pIdProgramaCompetencia, pIdProgramaEmpleado, pClPrioridad, prResultado);
        }

        public E_RESULTADO InsertaActualizaProgramaCapacitacion(int? pIdPrograma, string pXmlDatosCero, string pClUsuario, string pNbPrograma, int? pIdEmpresa, bool? pFgModificaPrograma)
        {
            ProgramaOperaciones oPrograma = new ProgramaOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPrograma.InsertaActualizaProgramaCapacitacion(pIdPrograma, pXmlDatosCero, pClUsuario, pNbPrograma, pIdEmpresa, pFgModificaPrograma));
        }
     
        public E_RESULTADO EliminaProgramaCapacitacion(int? pIdPrograma)
        {
            ProgramaOperaciones oPrograma = new ProgramaOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPrograma.EliminaProgramaCapacitacion(pIdPrograma));
        }
     
        public XElement ObtenerProgramaCapacitacionCompleto(int? pIdPrograma,int? pID_EMPRESA = null)
        {
            ProgramaOperaciones oPrograma = new ProgramaOperaciones();
            return oPrograma.ObtenerProgramasCapacitacionCompleto(pIdPrograma, pID_EMPRESA);
        }     

        public List<SPE_OBTIENE_AVANCE_PROGRAMA_CAPACITACION_Result> ObtenerAvancePrograma(int ID_PROGRAMA, int? ID_EMPRESA, string XML_FILTROS)
        {
            ProgramaOperaciones op = new ProgramaOperaciones();
            return op.ObtenerAvancePrograma(ID_PROGRAMA, ID_EMPRESA, XML_FILTROS);
        }

         public List<SPE_OBTIENE_K_PROGRAMA_COMPETENCIA_Result> ObtenerCompetenciasPrograma(int? ID_PROGRAMA_COMPETENCIA = null, int? ID_PROGRAMA = null, int? ID_COMPETENCIA = null, string NB_COMPETENCIA = null, string NB_CLASIFICACION = null, string NB_CATEGORIA = null)
        {
            ProgramaOperaciones op = new ProgramaOperaciones();
            return op.ObtenerCompetenciasPrograma(ID_PROGRAMA_COMPETENCIA, ID_PROGRAMA, ID_COMPETENCIA, NB_COMPETENCIA, NB_CLASIFICACION, NB_CATEGORIA);
        }

        public List<SPE_OBTIENE_FYD_REPORTE_EVENTO_EVALUADO_Result> ObtenerReporteEvaluado(int? ID_EVENTO, int? ID_EMPLEADO, int? ID_COMPETENCIA)
        {
            ProgramaOperaciones op = new ProgramaOperaciones();
            return op.ObtenerReporteEvaluado(ID_EVENTO, ID_EMPLEADO, ID_COMPETENCIA);
        }

        public E_RESULTADO TerminarProgramaCapacitacion(int pIdPrograma) 
        {
            ProgramaOperaciones oPrograma = new ProgramaOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPrograma.TerminaProgramaCapacitacion(pIdPrograma));
        }

        public List<SPE_OBTIENE_K_PROGRAMA_EMPLEADO_Result> ObtieneEmpleadosParticipantes(int? pID_PROGRAMA_EMPLEADO = null, int? pID_PROGRAMA = null, int? pID_EMPLEADO = null, int? pNB_EMPLEADO = null, string pCL_EMPLEADO =null, string pNB_PUESTO =null, string pCL_PUESTO=null, string pNB_DEPARTAMENTO=null, int? pID_EMPRESA = null)
        {
            ProgramaOperaciones oPrograma = new ProgramaOperaciones();
            return oPrograma.ObtenerEmpleadosParticipantes(pID_PROGRAMA_EMPLEADO, pID_PROGRAMA, pID_EMPLEADO, pNB_EMPLEADO, pCL_EMPLEADO, pNB_PUESTO, pCL_PUESTO, pNB_DEPARTAMENTO,pID_EMPRESA);
        }
    }
}
