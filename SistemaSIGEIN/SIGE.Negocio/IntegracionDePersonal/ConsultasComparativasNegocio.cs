using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades.IntegracionDePersonal;
using System.Xml.Linq;
using SIGE.Negocio.Utilerias;
using SIGE.Entidades.Externas;


namespace SIGE.Negocio.IntegracionDePersonal
{
    public class ConsultasComparativasNegocio
    {
        #region OBTIENE DATOS PUESTO VS N CANDIDATOS
        public List<SPE_OBTIENE_PUESTO_VS_CANDIDATOS_Result> ObtienePuestoCandidatos(int? pID_PUESTO = null, string pXML_CANDIDATOS = null, bool? pFgConsultaParcial = null, bool? pFgCalificacionCero = null)
        {
            ConsultasComparativasOperaciones operaciones = new ConsultasComparativasOperaciones();
            return operaciones.ObtenerPuestoCandidatos(pID_PUESTO, pXML_CANDIDATOS, pFgConsultaParcial, pFgCalificacionCero);
        }
        #endregion

   
        #region OBTIENE DATOS CANDIDATO VS PUESTOS
        public List<SPE_OBTIENE_CANDIDATO_VS_PUESTOS_Result> ObtieneCandidatoPuestos(int? pID_CANDIDATO = null, string pXML_PUESTOS = null, bool? vFgConsultaParcial = null, bool? vFgCalificacionCero = null)
        {
            ConsultasComparativasOperaciones operaciones = new ConsultasComparativasOperaciones();
            return operaciones.ObtenerCandidatoPuestos(pID_CANDIDATO, pXML_PUESTOS, vFgConsultaParcial, vFgCalificacionCero);
        }
        #endregion










        public E_RESULTADO InsertaConsultaGlobalCalificaciones(int pIdCandidato, int pIdPuesto, string pClUsuario, string pNbPrograma)
        {
            ConsultasComparativasOperaciones oConsultas = new ConsultasComparativasOperaciones();
            return UtilRespuesta.EnvioRespuesta(oConsultas.InsertarConsultaGlobalCalificaciones(pIdCandidato, pIdPuesto, pClUsuario, pNbPrograma));
        }

        public List<E_CONSULTA_GLOBAL> ObtieneConsultaGlobalCalificaciones(int pIdCandidato, int pIdPuesto)
        {
            ConsultasComparativasOperaciones oConsultas = new ConsultasComparativasOperaciones();
            return oConsultas.ObtenerConsultaGlobalCalificaciones(pIdPuesto, pIdCandidato);
        }

        public decimal ObtieneCalificionPruebasPsicometricas(int pIdCandidato, int pIdPuesto)
        {
            ConsultasComparativasOperaciones oConsultas = new ConsultasComparativasOperaciones();
            return oConsultas.ObtenerCalificionPruebasPsicometricas(pIdCandidato, pIdPuesto);
        }

        public decimal ObtenerCalificacionIngles(int pIdCandidato)
        {
            ConsultasComparativasOperaciones oConsultas = new ConsultasComparativasOperaciones();
            return oConsultas.ObtenerCalificacionIngles(pIdCandidato);
        }

        public E_RESULTADO ActualizarCalificacionConsultaGlobal(int pIdCandidato, int pIdPuesto, int pIdConsultaGlobal, string pXmlComentarios, string pXmlCalificaciones, string pClUsuario, string pNbPrograma)
        {
            ConsultasComparativasOperaciones oConsultas = new ConsultasComparativasOperaciones();
            return UtilRespuesta.EnvioRespuesta(oConsultas.ActualizarCalificacionConsultaGlobal(pIdCandidato, pIdPuesto, pIdConsultaGlobal, pXmlComentarios, pXmlCalificaciones, pClUsuario, pNbPrograma));
        }
    }
}
