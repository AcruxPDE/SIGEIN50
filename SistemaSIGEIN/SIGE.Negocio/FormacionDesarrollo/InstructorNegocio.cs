using System.Xml.Linq;
using SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.FormacionDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Negocio.Utilerias;
using SIGE.Entidades.Externas;

namespace SIGE.Negocio.FormacionDesarrollo
{
    public class InstructorNegocio
    {
        public List<SPE_OBTIENE_INSTRUCTORES_Result> ObtieneInstructores(int? pIdInstructor = null, int? pIdCompetencia = null, int? pIdCurso = null, string pXmlCompetencias = null, int? pIdEmpresa = null)
        {
            InstructorOperaciones oInstructor = new InstructorOperaciones();
            return oInstructor.ObtenerInstructores(pIdInstructor, pIdCompetencia, pIdCurso, pXmlCompetencias,pIdEmpresa);
        }

        public List<SPE_OBTIENE_C_CATALOGO_VALOR_Result> ObtieneTiposTelefono(string pIdTipoTelefono)
        {
            InstructorOperaciones oTipoTelefono = new InstructorOperaciones();
            return oTipoTelefono.ObtenerTipoTelefono(pIdTipoTelefono);
        }

        public List<SPE_OBTIENE_INSTRUCTORES_Result> ObtieneInstructor(int? pIdInstructor)
        {
            InstructorOperaciones nInstructorCurso = new InstructorOperaciones();
            List<SPE_OBTIENE_INSTRUCTORES_Result> vInstructorCompetencia = nInstructorCurso.ObtenerInstructores(pIdInstructor, null, null);
            return vInstructorCompetencia;
        }

        public List<E_INSTRUCTOR_COMPETENCIA> ObtieneInstructorCompetencia(int? pIdInstructor)
        {
            InstructorOperaciones nInstructorCompetencia = new InstructorOperaciones();
            List<SPE_OBTIENE_INSTRUCTORES_Result> vInstructorCompetencia = nInstructorCompetencia.ObtenerInstructores(pIdInstructor, null, null);

            List<E_INSTRUCTOR_COMPETENCIA> InstructorCompetencia = new List<E_INSTRUCTOR_COMPETENCIA>();

            foreach (SPE_OBTIENE_INSTRUCTORES_Result element in vInstructorCompetencia)
            {
                InstructorCompetencia = XElement.Parse(element.XML_COMPETENCIAS).Elements("COMPETENCIA").Select(el => new E_INSTRUCTOR_COMPETENCIA
                {
                    ID_INSTRUCTOR_COMPETENCIA = (int)UtilXML.ValorAtributo(el.Attribute("ID_INSTRUCTOR_COMPETENCIA"), E_TIPO_DATO.INT),
                    ID_COMPETENCIA = (int)UtilXML.ValorAtributo(el.Attribute("ID_COMPETENCIA"), E_TIPO_DATO.INT),
                    CL_COMPETENCIA = el.Attribute("CL_COMPETENCIA").Value,
                    NB_COMPETENCIA = el.Attribute("NB_COMPETENCIA").Value,
                }).ToList();
            }

            return InstructorCompetencia;
        }

        public List<SPE_OBTIENE_M_EMPLEADO_Result> ObtieneEmpleado(int? pIdEmpleado)
        {
            InstructorOperaciones nInstructorCurso = new InstructorOperaciones();
            List<SPE_OBTIENE_M_EMPLEADO_Result> vEmpleado = nInstructorCurso.ObtenerEmpleado(pIdEmpleado);
            return vEmpleado;
        }

        public List<SPE_OBTIENE_C_CURSO_Result> ObtieneCursos(int? pIdCurso)
        {
            InstructorOperaciones oCursos = new InstructorOperaciones();
            return oCursos.ObtenerCursos(pIdCurso);
        }

        public List<SPE_OBTIENE_C_COMPETENCIA_Result> ObtieneCompetencias(int? pIdComptencia)
        {
            InstructorOperaciones oComptencias = new InstructorOperaciones();
            return oComptencias.ObtenerComptencias(pIdComptencia);
        }

        public E_RESULTADO InsertaActualizaInstructor(string pTipoTransaccion, E_INSTRUCTOR pInstructor, XElement pCompetencias, XElement pCursos, List<UDTT_ARCHIVO> pLstArchivosTemporales, List<E_DOCUMENTO> pLstDocumentos, XElement pCamposAdicionales, string pClUsuario, string pNbPrograma)
        {
            InstructorOperaciones oInstructor = new InstructorOperaciones();
            return UtilRespuesta.EnvioRespuesta(oInstructor.InsertarActualizarInstructor(pTipoTransaccion, pInstructor, pCompetencias, pCursos, pLstArchivosTemporales, pLstDocumentos, pCamposAdicionales, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO EliminaInstructor(int pIdInstructor, string pclInstructor, string pClUsuario, string pNbPrograma)
        {
            InstructorOperaciones oInstructor = new InstructorOperaciones();
            return UtilRespuesta.EnvioRespuesta(oInstructor.EliminarInstructor(pIdInstructor, pclInstructor, pClUsuario, pNbPrograma));
        }

        public string ObtieneCampoAdicionalXml(String CL_TABLA_REFERENCIA = null)
        {
            InstructorOperaciones oInstructor = new InstructorOperaciones();
            return oInstructor.ObtieneCampoAdicionalXml(CL_TABLA_REFERENCIA);
        }
    }
}
