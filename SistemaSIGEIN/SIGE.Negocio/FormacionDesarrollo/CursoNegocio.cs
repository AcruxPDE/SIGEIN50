using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;
using System.Xml.Linq;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Entidades.SecretariaTrabajoPrevisionSocial;

namespace SIGE.Negocio.FormacionDesarrollo
{
    public class CursoNegocio
    {
        public List<SPE_OBTIENE_C_CURSO_Result> ObtieneCursos(int? pIdCurso = null)
        {
            CursoOperaciones oCurso = new CursoOperaciones();
            return oCurso.ObtenerCursos(pIdCurso);
        }

        public E_RESULTADO EliminaCurso(int pIdCurso, string pclCurso, string pClUsuario, string pNbPrograma)
        {
            CursoOperaciones oCurso = new CursoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oCurso.EliminarCurso(pIdCurso, pclCurso, pClUsuario, pNbPrograma));
        }

        public List<SPE_OBTIENE_C_CURSO_Result> ObtieneCursos(int? pIdCurso = null, string pXmlCompetencias = null, int? pId_Programa = null)
        {
            CursoOperaciones oCurso = new CursoOperaciones();
            return oCurso.ObtenerCursos(pIdCurso, pXmlCompetencias, pId_Programa);
        }

        public E_CURSO ObtieneCurso(int pIdCurso)
        {
            CursoOperaciones oCurso = new CursoOperaciones();
            SPE_OBTIENE_C_CURSO_Result vCurso = oCurso.ObtenerCursos(pIdCurso).FirstOrDefault();

            E_CURSO eCurso = new E_CURSO
            {
                CL_CURSO = vCurso.CL_CURSO,
                CL_TIPO_CURSO = vCurso.CL_TIPO_CURSO,
                DS_NOTAS = vCurso.DS_NOTAS,
                ID_CURSO = vCurso.ID_CURSO,
                ID_PUESTO_OBJETIVO = vCurso.ID_PUESTO_OBJETIVO,
                NB_CURSO = vCurso.NB_CURSO,
                NO_DURACION_CURSO = vCurso.NO_DURACION,
                XML_DOCUMENTOS = vCurso.XML_DOCUMENTOS,
                XML_CAMPOS_ADICIONALES = vCurso.XML_CAMPOS_ADICIONALES
            };

            if (vCurso.XML_INSTRUCTOR != null)
                eCurso.LS_INSTRUCTORES = XElement.Parse(vCurso.XML_INSTRUCTOR).Elements("INSTRUCTOR").Select(el => new E_CURSO_INSTRUCTOR
                 {
                     ID_INSTRUCTOR_CURSO = UtilXML.ValorAtributo<int>(el.Attribute("ID_INSTRUCTOR_CURSO")),
                     ID_INSTRUCTOR = UtilXML.ValorAtributo<int>(el.Attribute("ID_CURSO")),
                     CL_INSTRUCTOR = UtilXML.ValorAtributo<string>(el.Attribute("CL_INSTRUCTOR")),
                     NB_INSTRUCTOR = UtilXML.ValorAtributo<string>(el.Attribute("NB_INSTRUCTOR")),
                 }).ToList();

            if (vCurso.XML_COMPETENCIAS != null)
                eCurso.LS_COMPETENCIAS = XElement.Parse(vCurso.XML_COMPETENCIAS).Elements("COMPETENCIA").Select(el => new E_CURSO_COMPETENCIA
                {
                    ID_CURSO_COMPETENCIA = UtilXML.ValorAtributo<int>(el.Attribute("ID_CURSO_COMPETENCIA")),
                    ID_COMPETENCIA = UtilXML.ValorAtributo<int>(el.Attribute("ID_COMPETENCIA")),
                    NB_COMPETENCIA = UtilXML.ValorAtributo<string>(el.Attribute("NB_COMPETENCIA")),
                    CL_TIPO_COMPETENCIA = UtilXML.ValorAtributo<string>(el.Attribute("CL_TIPO_COMPETENCIA")),
                }).ToList();

            if (vCurso.XML_AREA_TEMATICA != null)
                eCurso.LS_AREAS_TEMATICAS = XElement.Parse(vCurso.XML_AREA_TEMATICA).Elements("AREATEMATICA").Select(el => new E_CURSO_AREA_TEMATICA
                {
                    ID_AREA_TEMATICA_CURSO = UtilXML.ValorAtributo<int>(el.Attribute("ID_CURSO_AREA_TEMATICA")),
                    ID_AREA_TEMATICA = UtilXML.ValorAtributo<int>(el.Attribute("ID_AREA_TEMATICA")),
                    NB_AREA_TEMATICA = UtilXML.ValorAtributo<string>(el.Attribute("NB_AREA_TEMATICA")),
                    CL_AREA_TEMATICA = UtilXML.ValorAtributo<string>(el.Attribute("CL_AREA_TEMATICA")),
                }).FirstOrDefault();

            if (vCurso.XML_TEMAS != null)
            {
                foreach (XElement item in XElement.Parse(vCurso.XML_TEMAS).Elements("TEMA"))
                {

                    E_TEMA oTema = new E_TEMA();

                    oTema.ID_TEMA = UtilXML.ValorAtributo<int>(item.Attribute("ID_TEMA"));
                    oTema.CL_TEMA = UtilXML.ValorAtributo<string>(item.Attribute("CL_TEMA"));
                    oTema.NB_TEMA = UtilXML.ValorAtributo<string>(item.Attribute("NB_TEMA"));
                    oTema.NO_DURACION = UtilXML.ValorAtributo<string>(item.Attribute("NO_DURACION"));
                    oTema.DS_DESCRIPCION = UtilXML.ValorAtributo<string>(item.Attribute("DS_DESCRIPCION"));

                    if (item.Elements("COMPETENCIATEMA").Count() > 0)
                    {
                        oTema.LS_COMPETENCIAS = item.Elements("COMPETENCIATEMA").Select(ct => new E_TEMA_COMPETENCIA
                         {
                             ID_TEMA = UtilXML.ValorAtributo<int>(ct.Attribute("ID_TEMA")),
                             ID_COMPETENCIA = UtilXML.ValorAtributo<int>(ct.Attribute("ID_COMPETENCIA")),
                             NB_COMPETENCIA = UtilXML.ValorAtributo<string>(ct.Attribute("NB_COMPETENCIA")),
                             CL_TIPO_COMPETENCIA = UtilXML.ValorAtributo<string>(ct.Attribute("CL_TIPO_COMPETENCIA"))
                         }).ToList();
                    }

                    if (item.Element("XML_MATERIALES") != null)
                    {
                        oTema.LS_MATERIALES = item.Element("XML_MATERIALES").Element("MATERIALES").Elements("MATERIAL").Select(m => new E_MATERIAL
                        {
                            ID_ITEM = Guid.Parse(UtilXML.ValorAtributo<String>(m.Attribute("ID_MATERIAL_ITEM"))),
                            CL_MATERIAL = UtilXML.ValorAtributo<int>(m.Attribute("CL_MATERIAL")),
                            NB_MATERIAL = UtilXML.ValorAtributo<string>(m.Attribute("NB_MATERIAL")),
                            MN_MATERIAL = UtilXML.ValorAtributo<string>(m.Attribute("MN_MATERIAL")),

                        }).ToList();
                    }

                    eCurso.LS_TEMAS.Add(oTema);
                }

            }

            return eCurso;
        }

        public List<SPE_OBTIENE_CURSO_PUESTO_COMPETENCIA_Result> ObtienePuestoCompetencia(int? pIdCurso, string clFuncion)
        {
            CursoOperaciones oPuestoCompetencia = new CursoOperaciones();
            return oPuestoCompetencia.ObtenerPuestoCompetencia(pIdCurso, clFuncion);
        }

        public E_RESULTADO InsertaActualizaCurso(string pTipoTransaccion, string pClUsuario, string pNbPrograma, E_CURSO pCurso, List<UDTT_ARCHIVO> pLstArchivosTemporales, List<E_DOCUMENTO> pLstDocumentos, XElement pXmlCursoInstructores, XElement pXmlCursoCompetencias, XElement pXmlTemas, XElement pXmlTemaMateriales, XElement pXmlTemaCompetencias, XElement pXmlCamposAdicionales, XElement pXmlAreasTematicas)
        {
            CursoOperaciones oCurso = new CursoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oCurso.InsertarActualizarCurso(pTipoTransaccion, pClUsuario, pNbPrograma, pCurso, pLstArchivosTemporales, pLstDocumentos, pXmlCursoInstructores, pXmlCursoCompetencias, pXmlTemas, pXmlTemaMateriales, pXmlTemaCompetencias, pXmlCamposAdicionales, pXmlAreasTematicas));
        }

        public string ObtieneCampoAdicionalXml(String CL_TABLA_REFERENCIA = null)
        {
            CursoOperaciones oCurso = new CursoOperaciones();
            return oCurso.ObtieneCampoAdicionalXml(CL_TABLA_REFERENCIA);
        }
    }
}
