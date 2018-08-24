using SIGE.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo
{
    public class ConsultasFYDOperaciones
    {
        SistemaSigeinEntities Contexto;

        public string obtieneCatalogos()
        {
            ObjectParameter pXmlCatalogos = new ObjectParameter("POUT_XML_CATALOGOS", typeof(XElement));
            using (Contexto = new SistemaSigeinEntities())
            {
                Contexto.SPE_OBTIENE_FYD_REPORTE_CATALOGOS(pXmlCatalogos);
                return pXmlCatalogos.Value.ToString();
            }
        }

        public List<SPE_OBTIENE_FYD_REPORTE_CURSOS_REALIZADOS_Result> ReporteCursosRealizados(DateTime FE_INICIAL, DateTime FE_FINAL, string CL_TIPO_CURSO, string XML_CURSOS = null, string XML_INSTRUCTORES = null, string XML_COMPETENCIAS = null, string XML_PARTICIPANTES = null, string XML_EVENTOS = null)
        {
            using (Contexto = new SistemaSigeinEntities())
            {
                return Contexto.SPE_OBTIENE_FYD_REPORTE_CURSOS_REALIZADOS(FE_INICIAL, FE_FINAL, CL_TIPO_CURSO, XML_CURSOS, XML_INSTRUCTORES, XML_COMPETENCIAS, XML_PARTICIPANTES, XML_EVENTOS).ToList();
            }
        }

        public string ReporteCursosrealizadosDetalle(int ID_EVENTO)
        {
            using (Contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutXmlDatosEvento = new ObjectParameter("POUT_XML_EVENTO", typeof(XElement));

                Contexto.SPE_OBTIENE_FYD_REPORTE_CURSOS_DETALLE(pOutXmlDatosEvento, ID_EVENTO);

                return pOutXmlDatosEvento.Value.ToString();
            }
        }

        public List<SPE_OBTIENE_FYD_REPORTE_MATERIALES_EVENTO_Result> ReporteMaterialesPorEvento(int ID_EVENTO)
        {
            using (Contexto = new SistemaSigeinEntities())
            {
                return Contexto.SPE_OBTIENE_FYD_REPORTE_MATERIALES_EVENTO(ID_EVENTO).ToList();
            }
        }

        public List<SPE_OBTIENE_FYD_REPORTE_MAXIMOS_MINIMOS_Result> ReporteMaximosMinimos(out int NO_EMPLEADOS_PUESTO ,out int NO_STOCK_REEMPLAZO, int ID_PUESTO_OBJETIVO)
        {
            using (Contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pNoEmpleadosPuesto = new ObjectParameter("POUT_NO_EMPLEADOS_PUESTO", typeof(int));
                ObjectParameter pNoStockRemplazo = new ObjectParameter("POUT_NO_STOCK_REEMPLAZO", typeof(int));

                List<SPE_OBTIENE_FYD_REPORTE_MAXIMOS_MINIMOS_Result> Lista = Contexto.SPE_OBTIENE_FYD_REPORTE_MAXIMOS_MINIMOS(pNoEmpleadosPuesto, pNoStockRemplazo, ID_PUESTO_OBJETIVO).ToList();

                NO_EMPLEADOS_PUESTO = int.Parse(pNoEmpleadosPuesto.Value.ToString());
                NO_STOCK_REEMPLAZO = int.Parse(pNoStockRemplazo.Value.ToString());

                return Lista;
            }
        }

        public List<SPE_OBTIENE_M_PUESTO_Result> ObtenerPuestos(int? pIdPuesto = null, bool? pFgActivo = null, DateTime? pFeInactivo = null, string pClPuesto = null, string pNbPuesto = null, int? pIdDepartamento = null, string pXmlCamposAdicionales = null, int? pIdBitacora = null, byte? pNoEdadMinima = null, byte? pNoEdadMaxima = null, string pClGenero = null, string pClEstadoCivil = null, string pXmlRequerimientos = null, string pXmlObservaciones = null, string pXmlResponsabilidades = null, string pXmlAutoridad = null, string pXmlCursosAdicionales = null, string pXmlMentor = null, string pClTipoPuesto = null, Guid? pIdCentroAdministrativo = null, Guid? pIdCentroOperativo = null, int? pIdPaquetePrestaciones = null, string pNbDepartamento = null, string pClDepartamento = null, string xml_puestos = null, XElement XML_PUESTOS_SELECCIONADOS = null, int? pIdEmpresa=null)
        {
            using (Contexto = new SistemaSigeinEntities())
            {
                string vXmlFiltro = null;
                if (XML_PUESTOS_SELECCIONADOS != null)
                    vXmlFiltro = XML_PUESTOS_SELECCIONADOS.ToString();


                return Contexto.SPE_OBTIENE_M_PUESTO(pIdPuesto, pFgActivo, pFeInactivo, pClPuesto, pNbPuesto, pIdDepartamento, pXmlCamposAdicionales, pIdBitacora, pNoEdadMinima, pNoEdadMaxima, pClGenero, pClEstadoCivil, pXmlRequerimientos, pXmlObservaciones, pXmlResponsabilidades, pXmlAutoridad, pXmlCursosAdicionales, pXmlMentor, pClTipoPuesto, pIdCentroAdministrativo, pIdCentroOperativo, pIdPaquetePrestaciones, xml_puestos, pNbDepartamento, pClDepartamento, vXmlFiltro,pIdEmpresa).ToList();
            }
        }

        public List<SPE_OBTIENE_FYD_REPORTE_PLANTILLAS_REEMPLAZO_Result> ObtenerReportePlantillasReemplazo(int ID_PUESTO)
        {
            using (Contexto = new SistemaSigeinEntities())
            {
                return Contexto.SPE_OBTIENE_FYD_REPORTE_PLANTILLAS_REEMPLAZO(ID_PUESTO).ToList();
            }
        }
    }
}
