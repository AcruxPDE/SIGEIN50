using System.Data.Objects;
using SIGE.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Entidades.Externas;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;

namespace SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo
{
    public class InstructorOperaciones
    {
        private SistemaSigeinEntities contexto;

        public List<E_INSTRUCTORES> ObtenerInstructores(int? pIdInstructor = null, int? pIdCompetencia = null, int? pIdCurso = null, string pXmlCompetencias = null, int? pID_EMPRESA = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<E_INSTRUCTORES>("EXEC " +
                    "FYD.SPE_OBTIENE_INSTRUCTORES " +
                    "@PIN_ID_INSTRUCTOR, " +
                    "@PIN_ID_COMPETENCIA, " +
                    "@PIN_ID_CURSO, " +
                    "@PIN_XML_COMPETENCIAS, " +
                    "@PIN_ID_EMPRESA ",
                    new SqlParameter("@PIN_ID_INSTRUCTOR", (Object)pIdInstructor ?? DBNull.Value),
                    new SqlParameter("@PIN_ID_COMPETENCIA", (Object)pIdCompetencia ?? DBNull.Value),
                    new SqlParameter("@PIN_ID_CURSO", (Object)pIdCurso ?? DBNull.Value),
                    new SqlParameter("@PIN_XML_COMPETENCIAS", (Object)pXmlCompetencias ?? DBNull.Value),
                    new SqlParameter("@PIN_ID_EMPRESA", (Object)pID_EMPRESA ?? DBNull.Value)
                ).ToList();

                //return contexto.SPE_OBTIENE_INSTRUCTORES(pIdInstructor, pIdCompetencia, pIdCurso, pXmlCompetencias,pID_EMPRESA).ToList();
            }
        }

        public List<SPE_OBTIENE_C_CATALOGO_VALOR_Result> ObtenerTipoTelefono(string pIdTipoTelefono)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_C_CATALOGO_VALOR(null, null, null, null, 6).ToList();
            }
        }

        public List<SPE_OBTIENE_M_EMPLEADO_Result> ObtenerEmpleado(int? pIdEmpleado)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_M_EMPLEADO(pIdEmpleado, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            }
        }

        public List<SPE_OBTIENE_C_CURSO_Result> ObtenerCursos(int? pIdCurso = null, string pXmlCompetencias = null, int? pIdPrograma = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_C_CURSO(pIdCurso, pXmlCompetencias, pIdPrograma).ToList();
            }
        }

        public List<SPE_OBTIENE_C_COMPETENCIA_Result> ObtenerComptencias(int? pIdCompetencia)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_C_COMPETENCIA(pIdCompetencia, null, null, null, null, null, null, null,null).ToList();
            }
        }

        //public XElement InsertarActualizarInstructor(string tipo_transaccion, E_INSTRUCTOR V_C_INSTRUCTOR, string usuario, string programa, XElement competencias, XElement cursos)
        //{
        //    using (contexto = new SistemaSigeinEntities())
        //    {
        //        ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
        //        contexto.SPE_INSERTA_ACTUALIZA_C_INSTRUCTOR(pout_clave_retorno, V_C_INSTRUCTOR.ID_INSTRUCTOR, V_C_INSTRUCTOR.CL_TIPO_INSTRUCTOR, V_C_INSTRUCTOR.CL_INTRUCTOR, V_C_INSTRUCTOR.NB_INSTRUCTOR, V_C_INSTRUCTOR.NB_VALIDADOR, V_C_INSTRUCTOR.CL_RFC, V_C_INSTRUCTOR.CL_CURP, V_C_INSTRUCTOR.CL_STPS, V_C_INSTRUCTOR.CL_PAIS, V_C_INSTRUCTOR.NB_PAIS, V_C_INSTRUCTOR.CL_ESTADO, V_C_INSTRUCTOR.NB_ESTADO, V_C_INSTRUCTOR.CL_MUNICIPIO, V_C_INSTRUCTOR.NB_MUNICIPIO, V_C_INSTRUCTOR.CL_COLONIA, V_C_INSTRUCTOR.NB_COLONIA, V_C_INSTRUCTOR.NB_CALLE, V_C_INSTRUCTOR.NO_INTERIOR, V_C_INSTRUCTOR.NO_EXTERIOR, V_C_INSTRUCTOR.CL_CODIGO_POSTAL, V_C_INSTRUCTOR.DS_ESCOLARIDAD, V_C_INSTRUCTOR.FE_NACIMIENTO, V_C_INSTRUCTOR.XML_TELEFONOS, V_C_INSTRUCTOR.CL_CORREO_ELECTRONICO, V_C_INSTRUCTOR.MN_COSTO_HORA, V_C_INSTRUCTOR.MN_COSTO_PARTICIPANTE, V_C_INSTRUCTOR.DS_EVIDENCIA_COMPETENCIA, competencias.ToString(), cursos.ToString(), usuario, usuario, programa, programa, tipo_transaccion);
        //        return XElement.Parse(pout_clave_retorno.Value.ToString());

        //    }
        //}

        public XElement InsertarActualizarInstructor(string pTipoTransaccion, E_INSTRUCTOR pInstructor, XElement pCompetencias, XElement pCursos, List<UDTT_ARCHIVO> pLstArchivosTemporales, List<E_DOCUMENTO> pLstDocumentos, XElement pCamposAdicionales, string pClUsuario, string pNbPrograma)
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
                    "[FYD].[SPE_INSERTA_ACTUALIZA_C_INSTRUCTOR] " +
                      "@XML_RESULTADO OUTPUT " +
                      ",@PIN_ID_INSTRUCTOR " +
                      ",@PIN_CL_TIPO_INSTRUCTOR " +
                      ",@PIN_CL_INTRUCTOR " +   
                      ",@PIN_NB_INSTRUCTOR " +
                      ",@PIN_NB_APELLIDO_PATERNO " +
                      ",@PIN_NB_APELLIDO_MATERNO " +
                      ",@PIN_NB_VALIDADOR " +                      
                      ",@PIN_CL_RFC " +
                      ",@PIN_CL_CURP " +
                      ",@PIN_CL_STPS " +
                      ",@PIN_CL_PAIS " +
                      ",@PIN_NB_PAIS " +
                      ",@PIN_CL_ESTADO " +
                      ",@PIN_NB_ESTADO " +
                      ",@PIN_CL_MUNICIPIO " +
                      ",@PIN_NB_MUNICIPIO " +
                      ",@PIN_CL_COLONIA " +
                      ",@PIN_NB_COLONIA " +
                      ",@PIN_NB_CALLE " +
                      ",@PIN_NO_INTERIOR " +
                      ",@PIN_NO_EXTERIOR " +
                      ",@PIN_CL_CODIGO_POSTAL " +
                      ",@PIN_DS_ESCOLARIDAD " +
                      ",@PIN_FE_NACIMIENTO " +
                      ",@PIN_XML_TELEFONOS " +
                      ",@PIN_CL_CORREO_ELECTRONICO " +
                      ",@PIN_MN_COSTO_HORA " +
                      ",@PIN_MN_COSTO_PARTICIPANTE " +
                      ",@PIN_DS_EVIDENCIA_COMPETENCIA " +
                      ",@PIN_XML_COMPETENCIAS " +
                      ",@PIN_XML_CURSOS " +
                      ",@PIN_XML_DOCUMENTOS " +
                      ",@PIN_XML_CAMPOS_ADICIONALES " +
                      ",@PIN_ARCHIVOS " +
                      ",@PIN_CL_USUARIO_APP " +
                      ",@PIN_NB_PROGRAMA " +
                      ",@PIN_TIPO_TRANSACCION"
                    , pXmlResultado
                    , new SqlParameter("@PIN_ID_INSTRUCTOR", (object)pInstructor.ID_INSTRUCTOR ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_TIPO_INSTRUCTOR", (object)pInstructor.CL_TIPO_INSTRUCTOR ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_INTRUCTOR", (object)pInstructor.CL_INTRUCTOR ?? DBNull.Value)
                    , new SqlParameter("@PIN_NB_INSTRUCTOR", (object)pInstructor.NB_INSTRUCTOR ?? DBNull.Value)
                    , new SqlParameter("@PIN_NB_APELLIDO_PATERNO", (object)pInstructor.NB_APELLIDO_PATERNO ?? DBNull.Value)
                    , new SqlParameter("@PIN_NB_APELLIDO_MATERNO", (object)pInstructor.NB_APELLIDO_MATERNO ?? DBNull.Value)
                    , new SqlParameter("@PIN_NB_VALIDADOR", (object)pInstructor.NB_VALIDADOR ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_RFC", (object)pInstructor.CL_RFC ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_CURP", (object)pInstructor.CL_CURP ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_STPS", (object)pInstructor.CL_STPS ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_PAIS", (object)pInstructor.CL_PAIS ?? DBNull.Value)
                    , new SqlParameter("@PIN_NB_PAIS", (object)pInstructor.NB_PAIS ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_ESTADO", (object)pInstructor.CL_ESTADO ?? DBNull.Value)
                    , new SqlParameter("@PIN_NB_ESTADO", (object)pInstructor.NB_ESTADO ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_MUNICIPIO", (object)pInstructor.CL_MUNICIPIO ?? DBNull.Value)
                    , new SqlParameter("@PIN_NB_MUNICIPIO", (object)pInstructor.NB_MUNICIPIO ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_COLONIA", (object)pInstructor.CL_COLONIA ?? DBNull.Value)
                    , new SqlParameter("@PIN_NB_COLONIA", (object)pInstructor.NB_COLONIA ?? DBNull.Value)
                    , new SqlParameter("@PIN_NB_CALLE", (object)pInstructor.NB_CALLE ?? DBNull.Value)
                    , new SqlParameter("@PIN_NO_INTERIOR", (object)pInstructor.NO_INTERIOR ?? DBNull.Value)
                    , new SqlParameter("@PIN_NO_EXTERIOR", (object)pInstructor.NO_EXTERIOR ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_CODIGO_POSTAL", (object)pInstructor.CL_CODIGO_POSTAL ?? DBNull.Value)
                    , new SqlParameter("@PIN_DS_ESCOLARIDAD", (object)pInstructor.DS_ESCOLARIDAD ?? DBNull.Value)
                    , new SqlParameter("@PIN_FE_NACIMIENTO", (object)pInstructor.FE_NACIMIENTO ?? DBNull.Value)
                    , new SqlParameter("@PIN_XML_TELEFONOS", SqlDbType.Xml) { Value = new SqlXml(XElement.Parse(pInstructor.XML_TELEFONOS).CreateReader())}
                    , new SqlParameter("@PIN_CL_CORREO_ELECTRONICO", (object)pInstructor.CL_CORREO_ELECTRONICO ?? DBNull.Value)
                    , new SqlParameter("@PIN_MN_COSTO_HORA", (object)pInstructor.MN_COSTO_HORA ?? DBNull.Value)
                    , new SqlParameter("@PIN_MN_COSTO_PARTICIPANTE", (object)pInstructor.MN_COSTO_PARTICIPANTE ?? DBNull.Value)
                    , new SqlParameter("@PIN_DS_EVIDENCIA_COMPETENCIA", (object)pInstructor.DS_EVIDENCIA_COMPETENCIA ?? DBNull.Value)
                    , new SqlParameter("@PIN_XML_COMPETENCIAS", SqlDbType.Xml) { Value = new SqlXml(pCompetencias.CreateReader())}
                    , new SqlParameter("@PIN_XML_CURSOS", SqlDbType.Xml) { Value = new SqlXml(pCursos.CreateReader()) }
                    , new SqlParameter("@PIN_XML_DOCUMENTOS", SqlDbType.Xml) { Value = new SqlXml(vXmlDocumentos.CreateReader()) }
                    , new SqlParameter("@PIN_XML_CAMPOS_ADICIONALES", SqlDbType.Xml) { Value = new SqlXml(pCamposAdicionales.CreateReader()) }
                    , pArchivos
                    , new SqlParameter("@PIN_CL_USUARIO_APP", pClUsuario)
                    , new SqlParameter("@PIN_NB_PROGRAMA", pNbPrograma)
                    , new SqlParameter("@PIN_TIPO_TRANSACCION", pTipoTransaccion)
                );
                return XElement.Parse(pXmlResultado.Value.ToString());
            }
        }

        public XElement EliminarInstructor(int pIdInstructor, string pClInstructor, string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ELIMINA_C_INSTRUCTOR(pOutClRetorno, pIdInstructor, pClInstructor, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
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
