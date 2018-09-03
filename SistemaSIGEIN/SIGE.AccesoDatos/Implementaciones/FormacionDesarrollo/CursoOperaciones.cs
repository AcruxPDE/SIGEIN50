using System.Data.Objects;
using System.Xml.Linq;
using SIGE.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Entidades.Externas;
using System.Data;
using System.Data.SqlTypes;

namespace SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo
{
    public class CursoOperaciones
    {
        private SistemaSigeinEntities contexto;

        public List<SPE_OBTIENE_C_CURSO_Result> ObtenerCursos(int? pIdCurso = null, string pXmlCompetencias = null, int? pIdPrograma = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_C_CURSO(pIdCurso, pXmlCompetencias, pIdPrograma).ToList();
            }
        }
        
        public XElement EliminarCurso(int pIdCurso, string pClCurso, string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ELIMINA_C_CURSO(pOutClRetorno, pIdCurso, pClCurso, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

      
        public List<SPE_OBTIENE_CURSO_PUESTO_COMPETENCIA_Result> ObtenerPuestoCompetencia(int? pIdCurso, string clFuncion)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_CURSO_PUESTO_COMPETENCIA(pIdCurso, clFuncion).ToList();
            }
        }

        public XElement InsertarActualizarCurso(string pTipoTransaccion, string pClUsuario, string pNbPrograma, E_CURSO pCurso, List<UDTT_ARCHIVO> pLstArchivosTemporales, List<E_DOCUMENTO> pLstDocumentos, XElement pXmlCursoInstructores, XElement pXmlCursoCompetencias, XElement pXmlTemas, XElement pXmlTemaMateriales, XElement pXmlTemaCompetencias, XElement pXmlCamposAdicionales, XElement pXmlAreasTematicas)
        {
            using (contexto = new SistemaSigeinEntities())
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

                List<UDTT_ARCHIVO> vLstArchivos = pLstArchivosTemporales;

                DataTable dtArchivos = new DataTable();
                dtArchivos.Columns.Add(new DataColumn("ID_ITEM", typeof(SqlGuid)));
                dtArchivos.Columns.Add(new DataColumn("ID_ARCHIVO", typeof(int)));
                dtArchivos.Columns.Add(new DataColumn("NB_ARCHIVO"));
                dtArchivos.Columns.Add(new DataColumn("FI_ARCHIVO", typeof(SqlBinary)));

                vLstArchivos.ForEach(f => dtArchivos.Rows.Add(f.ID_ITEM, f.ID_ARCHIVO ?? 0, f.NB_ARCHIVO, f.FI_ARCHIVO));

                var pArchivos = new SqlParameter("@PIN_ARCHIVOS", SqlDbType.Structured);
                pArchivos.Value = dtArchivos;
                pArchivos.TypeName = "ADM.UDTT_ARCHIVO";

                contexto.Database.ExecuteSqlCommand("EXEC " +
                    "[FYD].[SPE_INSERTA_ACTUALIZA_CURSO] " +
                      "@XML_RESULTADO OUTPUT " +
                      ",@PIN_ID_CURSO " +
                      ",@PIN_CL_CURSO " +
                      ",@PIN_NB_CURSO " +
                      ",@PIN_CL_TIPO_CURSO " +
                      ",@PIN_ID_PUESTO_OBJETIVO " +
                      ",@PIN_NO_DURACION " +
                      ",@PIN_DS_NOTAS " +
                      ",@PIN_XML_CURSO_INSTRUCTORES " +
                      ",@PIN_XML_CURSO_COMPETENCIA " +
                      ",@PIN_XML_TEMAS " +
                      ",@PIN_XML_TEMAS_MATERIALES " +
                      ",@PIN_XML_TEMAS_COMPETENCIAS " +
                      ",@PIN_XML_CAMPOS_ADICIONALES " +
                      ",@PIN_XML_CURSO_AREAS_TEMATICAS" +
                      ",@PIN_XML_DOCUMENTOS " +
                      ",@PIN_ARCHIVOS " +
                      ",@PIN_CL_USUARIO_APP " +
                      ",@PIN_NB_PROGRAMA " +
                      ",@PIN_TIPO_TRANSACCION "
                    , pXmlResultado
                    , new SqlParameter("@PIN_ID_CURSO", (object)pCurso.ID_CURSO ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_CURSO", (object)pCurso.CL_CURSO ?? DBNull.Value)
                    , new SqlParameter("@PIN_NB_CURSO", (object)pCurso.NB_CURSO ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_TIPO_CURSO", (object)pCurso.CL_TIPO_CURSO ?? DBNull.Value)
                    , new SqlParameter("@PIN_ID_PUESTO_OBJETIVO", (object)pCurso.ID_PUESTO_OBJETIVO ?? DBNull.Value)
                    , new SqlParameter("@PIN_NO_DURACION", (object)pCurso.NO_DURACION_CURSO ?? DBNull.Value)
                    , new SqlParameter("@PIN_DS_NOTAS", (object)pCurso.DS_NOTAS ?? DBNull.Value)
                    , new SqlParameter("@PIN_XML_CURSO_INSTRUCTORES", SqlDbType.Xml) { Value = new SqlXml(pXmlCursoInstructores.CreateReader()) }
                    , new SqlParameter("@PIN_XML_CURSO_COMPETENCIA", SqlDbType.Xml) { Value = new SqlXml(pXmlCursoCompetencias.CreateReader()) }
                    , new SqlParameter("@PIN_XML_TEMAS", SqlDbType.Xml) { Value = new SqlXml(pXmlTemas.CreateReader()) }
                    , new SqlParameter("@PIN_XML_TEMAS_MATERIALES", SqlDbType.Xml) { Value = new SqlXml(pXmlTemaMateriales.CreateReader()) }
                    , new SqlParameter("@PIN_XML_TEMAS_COMPETENCIAS", SqlDbType.Xml) { Value = new SqlXml(pXmlTemaCompetencias.CreateReader()) }
                    , new SqlParameter("@PIN_XML_CAMPOS_ADICIONALES", SqlDbType.Xml) { Value = new SqlXml(pXmlCamposAdicionales.CreateReader()) }
                    , new SqlParameter("@PIN_XML_CURSO_AREAS_TEMATICAS", SqlDbType.Xml) { Value = new SqlXml(pXmlAreasTematicas.CreateReader()) }
                    , new SqlParameter("@PIN_XML_DOCUMENTOS", SqlDbType.Xml) { Value = new SqlXml(vXmlDocumentos.CreateReader()) }
                    , pArchivos
                    , new SqlParameter("@PIN_CL_USUARIO_APP", pClUsuario)
                    , new SqlParameter("@PIN_NB_PROGRAMA", pNbPrograma)
                    , new SqlParameter("@PIN_TIPO_TRANSACCION", pTipoTransaccion)
                );
                return XElement.Parse(pXmlResultado.Value.ToString());
            }
        }

        public string ObtieneCampoAdicionalXml(String CL_TABLA_REFERENCIA = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_CAMPO_ADICIONAL_XML(CL_TABLA_REFERENCIA).FirstOrDefault().ToString();
            }
        }
    }

}
