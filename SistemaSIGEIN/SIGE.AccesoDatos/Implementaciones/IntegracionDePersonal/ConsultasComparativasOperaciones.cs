using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades.Administracion;
using SIGE.Entidades;
using System.Data.Objects;
using System.Xml.Linq;
using SIGE.Entidades.IntegracionDePersonal;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal
{
    public class ConsultasComparativasOperaciones
    {
        #region OBTIENE DATOS PUESTO VS N CANDIDATOS
        private SistemaSigeinEntities context;

        public List<SPE_OBTIENE_PUESTO_VS_CANDIDATOS_Result> ObtenerPuestoCandidatos(int? pID_PUESTO = null, string pXML_CANDIDATOS = null, bool? pFgConsultaParcial = null, bool? pFgCalificacionCero = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from vTabuladores in context.SPE_OBTIENE_PUESTO_VS_CANDIDATOS(pID_PUESTO, pXML_CANDIDATOS, pFgConsultaParcial, pFgCalificacionCero)
                        select vTabuladores;
                return q.ToList();
            }
        }
        #endregion

        #region OBTIENE DATOS CANDIDATO VS N PUESTOS

        public List<SPE_OBTIENE_CANDIDATO_VS_PUESTOS_Result> ObtenerCandidatoPuestos(int? pID_CANDIDATO = null, string pXML_PUESTOS = null, bool? vFgConsultaParcial = null, bool? vFgCalificacionCero = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from vTabuladores in context.SPE_OBTIENE_CANDIDATO_VS_PUESTOS(pID_CANDIDATO, pXML_PUESTOS, vFgConsultaParcial, vFgCalificacionCero)
                        select vTabuladores;
                return q.ToList();
            }
        }
        #endregion


        public XElement InsertarConsultaGlobalCalificaciones(int pIdCandidato, int pIdPuesto, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_DATOS_CONSULTA_GLOBAL(pout_clave_retorno, pIdCandidato, pIdPuesto, pClUsuario, pNbPrograma);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public List<E_CONSULTA_GLOBAL> ObtenerConsultaGlobalCalificaciones(int pIdPuesto, int pIdCandidato)
        {
            using (context = new SistemaSigeinEntities())
            {
                var vLista = context.SPE_OBTIENE_CONSULTA_GLOBAL_CALIFICACION(pIdPuesto, pIdCandidato).ToList();

                List<E_CONSULTA_GLOBAL> vLstCalificaciones = new List<E_CONSULTA_GLOBAL>();

                vLstCalificaciones = vLista.Select(t => new E_CONSULTA_GLOBAL
                {
                    ID_CONSULTA_GLOBAL = t.ID_CONSULTA_GLOBAL,
                    ID_CANDIDATO = t.ID_CANDIDATO,
                    ID_PUESTO = t.ID_PUESTO,
                    PR_COMPATIBILIDAD = t.PR_COMPATIBILIDAD,
                    DS_COMENTARIOS = t.DS_COMENTARIOS,
                    NO_FACTOR = t.NO_FACTOR,
                    NB_FACTOR = t.NB_FACTOR,
                    PR_FACTOR = t.PR_FACTOR,
                    ID_CONSULTA_GLOBAL_CALIFICACION = t.ID_CONSULTA_GLOBAL_CALIFICACION,
                    PR_CALIFICACION = t.PR_CALIFICACION,
                    PR_VALOR = t.PR_VALOR,
                    FG_ASOCIADO_INGLES = t.FG_ASOCIADO_INGLES
                }).ToList();

                return vLstCalificaciones;
            }
        }

        public decimal ObtenerCalificionPruebasPsicometricas(int pIdCandidato, int pIdPuesto)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutPrPsicometria = new ObjectParameter("POUT_PR_CUMPLIMIENTO", typeof(decimal));
                context.SPE_OBTIENE_PORCENTAJE_CUMPLIMIENTO_CANDIDATO_PUESTO(pOutPrPsicometria, pIdPuesto, pIdCandidato);
                return decimal.Parse(pOutPrPsicometria.Value.ToString());
            }
        }

        public decimal ObtenerCalificacionIngles(int pIdCandidato)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutPrIngles = new ObjectParameter("POUT_PR_PRUEBA_INGLES", typeof(decimal));
                context.SPE_OBTIENE_CALIFICACION_EXAMEN_INGLES(pOutPrIngles, pIdCandidato);
                return decimal.Parse(pOutPrIngles.Value.ToString());
            }
        }

        public XElement ActualizarCalificacionConsultaGlobal(int pIdCandidato, int pIdPuesto,int pIdConsultaGlobal,string pXmlComentarios, string pXmlCalificaciones, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ACTUALIZA_DATOS_CONSULTA_GLOBAL(pout_clave_retorno, pIdCandidato, pIdPuesto, pIdConsultaGlobal, pXmlComentarios, pXmlCalificaciones, pClUsuario, pNbPrograma);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

    }
}
