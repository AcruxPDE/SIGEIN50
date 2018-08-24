using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades.Administracion;
using SIGE.Entidades;
using System.Data.Objects;
using System.Xml.Linq;
using SIGE.Entidades.Externas;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{

    public class PuestoOperaciones
    {
        private SistemaSigeinEntities context;

        public List<SPE_OBTIENE_M_PUESTO_Result> ObtenerPuestos(int? pIdPuesto = null, bool? pFgActivo = null, DateTime? pFeInactivo = null, string pClPuesto = null, string pNbPuesto = null, int? pIdDepartamento = null, string pXmlCamposAdicionales = null, int? pIdBitacora = null, byte? pNoEdadMinima = null, byte? pNoEdadMaxima = null, string pClGenero = null, string pClEstadoCivil = null, string pXmlRequerimientos = null, string pXmlObservaciones = null, string pXmlResponsabilidades = null, string pXmlAutoridad = null, string pXmlCursosAdicionales = null, string pXmlMentor = null, string pClTipoPuesto = null, Guid? pIdCentroAdministrativo = null, Guid? pIdCentroOperativo = null, int? pIdPaquetePrestaciones = null, string pNbDepartamento = null, string pClDepartamento = null, string pXmlPuestos = null, XElement pXmlPuestosSeleccionados = null, int? pIdEmpresa = null, int? ID_ROL = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                string vXmlFiltro = null;
                if (pXmlPuestosSeleccionados != null)
                    vXmlFiltro = pXmlPuestosSeleccionados.ToString();

                return context.SPE_OBTIENE_M_PUESTO(pIdPuesto, pFgActivo, pFeInactivo, pClPuesto, pNbPuesto, pIdDepartamento, pXmlCamposAdicionales, pIdBitacora, pNoEdadMinima, pNoEdadMaxima, pClGenero, pClEstadoCivil, pXmlRequerimientos, pXmlObservaciones, pXmlResponsabilidades, pXmlAutoridad, pXmlCursosAdicionales, pXmlMentor, pClTipoPuesto, pIdCentroAdministrativo, pIdCentroOperativo, pIdPaquetePrestaciones, pXmlPuestos, pNbDepartamento, pClDepartamento, vXmlFiltro, pIdEmpresa, ID_ROL).ToList();
            }
        }

        public List<SPE_OBTIENE_M_PUESTOS_GENERAL_Result> ObtenerPuestosGeneral(int? pIdPuesto = null, bool? pFgActivo = null, DateTime? pFeInactivo = null, string pClPuesto = null, string pNbPuesto = null, int? pIdDepartamento = null, string pXmlCamposAdicionales = null, int? pIdBitacora = null, byte? pNoEdadMinima = null, byte? pNoEdadMaxima = null, string pClGenero = null, string pClEstadoCivil = null, string pXmlRequerimientos = null, string pXmlObservaciones = null, string pXmlResponsabilidades = null, string pXmlAutoridad = null, string pXmlCursosAdicionales = null, string pXmlMentor = null, string pClTipoPuesto = null, Guid? pIdCentroAdministrativo = null, Guid? pIdCentroOperativo = null, int? pIdPaquetePrestaciones = null, string pNbDepartamento = null, string pClDepartamento = null, string pXmlPuestos = null, XElement pXmlPuestosSeleccionados = null, int? pIdEmpresa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_M_PUESTOS_GENERAL(pIdPuesto, pFgActivo, pFeInactivo, pClPuesto, pNbPuesto, pIdDepartamento, pXmlCamposAdicionales, pIdBitacora, pNoEdadMinima, pNoEdadMaxima, pClGenero, pClEstadoCivil, pXmlRequerimientos, pXmlObservaciones, pXmlResponsabilidades, pXmlAutoridad, pXmlCursosAdicionales, pXmlMentor, pClTipoPuesto, pIdCentroAdministrativo, pIdCentroOperativo, pIdPaquetePrestaciones, pXmlPuestos, pNbDepartamento, pClDepartamento, null, pIdEmpresa).ToList();
            }
        }


        public List<SPE_VERIFICA_PUESTO_FACTOR_Result> ValidarConfiguracionPuestos(string pXmlPuestos = null, int? pIdPuesto = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_VERIFICA_PUESTO_FACTOR(pXmlPuestos, pIdPuesto).ToList();
            }
        }

        public List<SPE_OBTIENE_M_PUESTO_REQUISICION_Result> ObtenerPuestosRequisicion(int? pIdPuesto = null, bool? pFgActivo = null, DateTime? pFeInactivo = null, string pClPuesto = null, string pNbPuesto = null, int? pIdDepartamento = null, string pXmlCamposAdicionales = null, int? pIdBitacora = null, byte? pNoEdadMinima = null, byte? pNoEdadMaxima = null, string pClGenero = null, string pClEstadoCivil = null, string pXmlRequerimientos = null, string pXmlObservaciones = null, string pXmlResponsabilidades = null, string pXmlAutoridad = null, string pXmlCursosAdicionales = null, string pXmlMentor = null, string pClTipoPuesto = null, Guid? pIdCentroAdministrativo = null, Guid? pIdCentroOperativo = null, int? pIdPaquetePrestaciones = null, string pNbDepartamento = null, string pClDepartamento = null, string pXmlPuestos = null, XElement pXmlPuestosSeleccionados = null, int? pIdEmpresa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                string vXmlFiltro = null;
                if (pXmlPuestosSeleccionados != null)
                    vXmlFiltro = pXmlPuestosSeleccionados.ToString();

                return context.SPE_OBTIENE_M_PUESTO_REQUISICION(pIdPuesto, pFgActivo, pFeInactivo, pClPuesto, pNbPuesto, pIdDepartamento, pXmlCamposAdicionales, pIdBitacora, pNoEdadMinima, pNoEdadMaxima, pClGenero, pClEstadoCivil, pXmlRequerimientos, pXmlObservaciones, pXmlResponsabilidades, pXmlAutoridad, pXmlCursosAdicionales, pXmlMentor, pClTipoPuesto, pIdCentroAdministrativo, pIdCentroOperativo, pIdPaquetePrestaciones, pXmlPuestos, pNbDepartamento, pClDepartamento, vXmlFiltro, pIdEmpresa).ToList();
            }
        }

        public XElement InsertarActualizarPuesto(string tipo_transaccion, E_PUESTO V_M_PUESTO, List<UDTT_ARCHIVO> pLstArchivoTemporales, List<E_DOCUMENTO> pLstDocumentos, string usuario, string programa, string pXmlOcupacion)
        {
            using (context = new SistemaSigeinEntities())
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
                    new XAttribute("CL_TIPO_DOCUMENTO", s.CL_TIPO_DOCUMENTO))
                ));

                List<UDTT_ARCHIVO> vLstArchivos = pLstArchivoTemporales;

                DataTable dtArchivos = new DataTable();
                dtArchivos.Columns.Add(new DataColumn("ID_ITEM", typeof(SqlGuid)));
                dtArchivos.Columns.Add(new DataColumn("ID_ARCHIVO", typeof(int)));
                dtArchivos.Columns.Add(new DataColumn("NB_ARCHIVO"));
                dtArchivos.Columns.Add(new DataColumn("FI_ARCHIVO", typeof(SqlBinary)));

                vLstArchivos.ForEach(f => dtArchivos.Rows.Add(f.ID_ITEM, f.ID_ARCHIVO ?? 0, f.NB_ARCHIVO, f.FI_ARCHIVO));

                var pArchivos = new SqlParameter("@PIN_ARCHIVOS", SqlDbType.Structured);
                pArchivos.Value = dtArchivos;
                pArchivos.TypeName = "ADM.UDTT_ARCHIVO";

                context.Database.ExecuteSqlCommand("EXEC " +
                    "[ADM].[SPE_INSERTA_ACTUALIZA_M_PUESTO] " +
                      "@XML_RESULTADO OUTPUT " +
                      ",@PIN_ID_PUESTO " +
                      ",@PIN_CL_USUARIO " +
                      ",@PIN_NB_PROGRAMA " +
                      ",@PIN_TIPO_TRANSACCION " +
                      ",@PIN_XML_PUESTO " +
                      ",@PIN_XML_DOCUMENTOS " +
                      ",@PIN_XML_PUESTO_OCUPACION " +
                      ",@PIN_ARCHIVOS "
                    , pXmlResultado
                    , new SqlParameter("@PIN_ID_PUESTO", (object)V_M_PUESTO.ID_PUESTO ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_USUARIO", usuario)
                    , new SqlParameter("@PIN_NB_PROGRAMA", programa)
                    , new SqlParameter("@PIN_TIPO_TRANSACCION", tipo_transaccion)
                    , new SqlParameter("@PIN_XML_PUESTO", (object)V_M_PUESTO.XML_PUESTO ?? DBNull.Value)
                    , new SqlParameter("@PIN_XML_DOCUMENTOS", SqlDbType.Xml) { Value = new SqlXml(vXmlDocumentos.CreateReader()) }
                    , new SqlParameter("@PIN_XML_PUESTO_OCUPACION", pXmlOcupacion)
                    , pArchivos
                );
                return XElement.Parse(pXmlResultado.Value.ToString());
            }

        }


        public XElement InsertarActualizarPuestoRequisicion(string pTipoTransaccion = null, int? pIdPuesto = null, string pXmlPuesto = null, string pXmlPuestoOcupacion = null, int? pIdRequisicion = null, string pDsComentarios = null, string pClAutorizaRechaza = null, string pClAutorizaPuesto = null, string pClUsuario = null, string pNbPrograma = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));

                context.SPE_INSERTA_ACTUALIZA_M_PUESTO_REQUISICION(pout_clave_retorno, pIdPuesto, pXmlPuesto, pXmlPuestoOcupacion, pIdRequisicion, pDsComentarios, pClAutorizaRechaza, pClAutorizaPuesto, pClUsuario, pNbPrograma, pTipoTransaccion);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pout_clave_retorno.Value.ToString());

            }
        }

        public XElement EliminarPuesto(int? pIdPuesto = null, string pClPuesto = null, string pClUsuario = null, string pNbPrograma = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_M_PUESTO(pout_clave_retorno, pIdPuesto, pClUsuario, pNbPrograma);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
    }
}
