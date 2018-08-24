using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SIGE.Entidades;

namespace SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo
{
    public class ProgramaOperaciones
    {

        SistemaSigeinEntities contexto;

        public List<SPE_OBTIENE_C_PROGRAMA_Result> ObtenerProgramasCapacitacion(int? pIdPrograma = null, string pClPrograma = null, string pNbPrograma = null, string pClTipoPrograma = null, string pClEstado = null, string pClVersion = null, int? pIdDocumentoAutorizacion = null, XElement pXML_PROGRAMAS_SELECCION = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                string vXmlFiltro = null;
                if (pXML_PROGRAMAS_SELECCION != null)
                    vXmlFiltro = pXML_PROGRAMAS_SELECCION.ToString();
                return contexto.SPE_OBTIENE_C_PROGRAMA(pIdPrograma, pClPrograma, pNbPrograma, pClTipoPrograma, pClEstado, pClEstado, pIdDocumentoAutorizacion, vXmlFiltro).ToList();
            }
        }


        public List<SPE_OBTIENE_K_PROGRAMA_Result> ObtenerKernelProgramasCapacitacion(int? pIdProgramaEmpleadoCompetencia = null, int? pIdPrograma = null, int? pIdProgramaCompetencia = null, int? pIdProgramaEmpleado = null, string pClPrioridad = null, decimal? prResultado = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_K_PROGRAMA(pIdProgramaEmpleadoCompetencia, pIdPrograma, pIdProgramaCompetencia, pIdProgramaEmpleado, pClPrioridad, prResultado).ToList();
            }
        }


        public XElement InsertaActualizaProgramaCapacitacion(int? pIdPrograma, string pXmlDatosCero, string pClUsuario, string pNbPrograma, int? pIdEmpresa, bool? pFgModificaPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_ACTUALIZA_PROGRAMA_DESDE_CERO(pout_clave_retorno, pIdPrograma, pXmlDatosCero, pClUsuario, pNbPrograma, pIdEmpresa, pFgModificaPrograma);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }


        public XElement EliminaProgramaCapacitacion(int? pIdPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ELIMINA_PROGRAMA_CAPACITACION(pout_clave_retorno, pIdPrograma);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }


        public XElement ObtenerProgramasCapacitacionCompleto( int? pIdPrograma = null,int? pID_EMPRESA = null )
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_OBTIENE_PROGRAMA_CAPACITACION(pout_clave_retorno, pIdPrograma, pID_EMPRESA);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_AVANCE_PROGRAMA_CAPACITACION_Result> ObtenerAvancePrograma(int ID_PROGRAMA, int? ID_EMPRESA, string XML_FILTROS)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_AVANCE_PROGRAMA_CAPACITACION(ID_PROGRAMA, ID_EMPRESA, XML_FILTROS).ToList();
            }
        }

        public List<SPE_OBTIENE_K_PROGRAMA_COMPETENCIA_Result> ObtenerCompetenciasPrograma(int? ID_PROGRAMA_COMPETENCIA = null, int? ID_PROGRAMA = null, int? ID_COMPETENCIA = null, string NB_COMPETENCIA = null, string NB_CLASIFICACION = null, string NB_CATEGORIA = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_K_PROGRAMA_COMPETENCIA(ID_PROGRAMA_COMPETENCIA, ID_PROGRAMA, ID_COMPETENCIA, NB_COMPETENCIA, NB_CLASIFICACION, NB_CATEGORIA).ToList();
            }
        }

        public List<SPE_OBTIENE_FYD_REPORTE_EVENTO_EVALUADO_Result> ObtenerReporteEvaluado(int? ID_EVENTO, int? ID_EMPLEADO, int? ID_COMPETENCIA)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_REPORTE_EVENTO_EVALUADO(ID_EVENTO, ID_EMPLEADO, ID_COMPETENCIA).ToList();
            }
        }

        public List<SPE_OBTIENE_K_PROGRAMA_EMPLEADO_Result> ObtenerEmpleadosParticipantes(int? pID_PROGRAMA_EMPLEADO = null,int? pID_PROGRAMA = null,int? pID_EMPLEADO =  null, int? pNB_EMPLEADO = null, string pCL_EMPLEADO = null, string pNB_PUESTO = null, string pCL_PUESTO = null, string pNB_DEPARTAMENTO = null, int? pID_EMPRESA = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_K_PROGRAMA_EMPLEADO(pID_PROGRAMA_EMPLEADO,pID_PROGRAMA,pID_EMPLEADO,pNB_EMPLEADO,pCL_EMPLEADO,pNB_PUESTO,pCL_PUESTO,pNB_DEPARTAMENTO,pID_EMPRESA).ToList();
            }
        }

        public XElement TerminaProgramaCapacitacion(int pIdPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutclaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_TERMINA_PROGRAMA_CAPACITACION(pOutclaveRetorno, pIdPrograma);
                return XElement.Parse(pOutclaveRetorno.Value.ToString());
            }
        }

    }
}
