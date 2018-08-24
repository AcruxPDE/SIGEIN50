using SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo;
using SIGE.Entidades;
using SIGE.Entidades.FormacionDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.Negocio.FormacionDesarrollo
{
    public class ConsultasFYDNegocio
    {
        public string ObtenerCatalogos()
        {
            ConsultasFYDOperaciones op = new ConsultasFYDOperaciones();
            return op.obtieneCatalogos();
        }

        public List<SPE_OBTIENE_FYD_REPORTE_CURSOS_REALIZADOS_Result> ReporteCursosRealizados(DateTime FE_INICIAL, DateTime FE_FINAL, string CL_TIPO_CURSO, string XML_CURSOS = null, string XML_INSTRUCTORES = null, string XML_COMPETENCIAS = null, string XML_PARTICIPANTES = null, string XML_EVENTOS = null)
        {
            ConsultasFYDOperaciones op = new ConsultasFYDOperaciones();
            return op.ReporteCursosRealizados(FE_INICIAL, FE_FINAL, CL_TIPO_CURSO, XML_CURSOS, XML_INSTRUCTORES, XML_COMPETENCIAS, XML_PARTICIPANTES, XML_EVENTOS);
        }

        public string ReporteCursosrealizadosDetalle(int ID_EVENTO)
        {
            ConsultasFYDOperaciones op = new ConsultasFYDOperaciones();
            return op.ReporteCursosrealizadosDetalle(ID_EVENTO);
        }

        public List<SPE_OBTIENE_FYD_REPORTE_MATERIALES_EVENTO_Result> ReporteMaterialesPorEvento(int ID_EVENTO)
        {
            ConsultasFYDOperaciones op = new ConsultasFYDOperaciones();
            return op.ReporteMaterialesPorEvento(ID_EVENTO);
        }

        public List<E_REPORTE_MAXIMO_MINIMO> ReporteMaximosMinimos(out int NO_NUMEROS_EMPLEADOS, out int NO_STOCK_REEMPLAZO, int ID_PUESTO_OBJETIVO)
        {
            ConsultasFYDOperaciones op = new ConsultasFYDOperaciones();
            List<SPE_OBTIENE_FYD_REPORTE_MAXIMOS_MINIMOS_Result> oLista = op.ReporteMaximosMinimos(out NO_NUMEROS_EMPLEADOS, out NO_STOCK_REEMPLAZO, ID_PUESTO_OBJETIVO);


            List<E_REPORTE_MAXIMO_MINIMO> oListaReportes = (oLista.Select(t => new E_REPORTE_MAXIMO_MINIMO
            {
                FE_INICIO = t.FE_INICIO,
                FE_TERMINO = t.FE_TERMINO,
                ID_CURSO = t.ID_CURSO,
                ID_EVENTO = t.ID_EVENTO,
                NB_CURSO = t.NB_CURSO,
                NB_EVENTO = t.NB_EVENTO,
                NO_PERSONAL_ENTRENAMIENTO = t.NO_PERSONAL_ENTRENAMIENTO,
                DS_COMPETENCIAS = t.DS_COMPETENCIAS
            })).ToList();

            return oListaReportes;
        }

        public List<SPE_OBTIENE_M_PUESTO_Result> ObtienePuestos(int? ID_PUESTO = null, bool? FG_ACTIVO = null, DateTime? FE_INACTIVO = null, String CL_PUESTO = null, String NB_PUESTO = null, int? ID_PUESTO_JEFE = null, int? ID_DEPARTAMENTO = null, String XML_CAMPOS_ADICIONALES = null, int? ID_BITACORA = null, byte? NO_EDAD_MINIMA = null, byte? NO_EDAD_MAXIMA = null, String CL_GENERO = null, String CL_ESTADO_CIVIL = null, String XML_REQUERIMIENTOS = null, String XML_OBSERVACIONES = null, String XML_RESPONSABILIDAD = null, String XML_AUTORIDAD = null, String XML_CURSOS_ADICIONALES = null, String XML_MENTOR = null, String CL_TIPO_PUESTO = null, Guid? ID_CENTRO_ADMINISTRATIVO = null, Guid? ID_CENTRO_OPERATIVO = null, int? ID_PAQUETE_PRESTACIONES = null, String NB_DEPARTAMENTO = null, String CL_DEPARTAMENTO = null, string xml_puestos = null, XElement XML_PUESTOS_SELECCIONADOS = null)
        {
            ConsultasFYDOperaciones op = new ConsultasFYDOperaciones();
            return op.ObtenerPuestos(ID_PUESTO, FG_ACTIVO, FE_INACTIVO, CL_PUESTO, NB_PUESTO, ID_DEPARTAMENTO, XML_CAMPOS_ADICIONALES, ID_BITACORA, NO_EDAD_MINIMA, NO_EDAD_MAXIMA, CL_GENERO, CL_ESTADO_CIVIL, XML_REQUERIMIENTOS, XML_OBSERVACIONES, XML_RESPONSABILIDAD, XML_AUTORIDAD, XML_CURSOS_ADICIONALES, XML_MENTOR, CL_TIPO_PUESTO, ID_CENTRO_ADMINISTRATIVO, ID_CENTRO_OPERATIVO, ID_PAQUETE_PRESTACIONES, NB_DEPARTAMENTO, CL_DEPARTAMENTO, xml_puestos, XML_PUESTOS_SELECCIONADOS);
        }

        public List<SPE_OBTIENE_FYD_REPORTE_PLANTILLAS_REEMPLAZO_Result> ObtenerReportePlantillasReemplazo(int ID_PUESTO)
        {
            ConsultasFYDOperaciones op = new ConsultasFYDOperaciones();
            return op.ObtenerReportePlantillasReemplazo(ID_PUESTO);
        }
    }
}
