using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades.Administracion;
using SIGE.Entidades;
using System.Data.Objects;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{

    public class CompetenciaOperaciones
    {
        private SistemaSigeinEntities context;
        
		public List<SPE_OBTIENE_C_COMPETENCIA_Result> ObtenerCompetencias(int? pIdCompetencia = null, string pClCompetencia = null, string pNbCompetencia = null, string pDsCompetencia = null, string pClTipoCompetencia = null, string pClClasificacion = null, bool? pFgActivo = null, string pXmlCamposAdicionales = null, XElement pXmlSeleccion = null)
		{  
			using (context = new SistemaSigeinEntities())
			{
                string vXmlFiltro = null;
                if (pXmlSeleccion != null)
                    vXmlFiltro = pXmlSeleccion.ToString();

                return context.SPE_OBTIENE_C_COMPETENCIA(pIdCompetencia, pClCompetencia, pNbCompetencia, pDsCompetencia, pClTipoCompetencia, pClClasificacion, pFgActivo, pXmlCamposAdicionales, vXmlFiltro).ToList();
			}  
		}

        public List<SPE_OBTIENE_FACTORES_EVALUACION_TABULADOR_Result> ObtenerFactoresValuacion(int? pIdFactor = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_FACTORES_EVALUACION_TABULADOR(pIdFactor).ToList();
            }
        }  
		        
        public XElement InsertarActualizarCompetencia(string pTipoTransaccion, E_COMPETENCIA_NIVEL vCompetencia, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_C_COMPETENCIA(pout_clave_retorno, vCompetencia.ID_COMPETENCIA, vCompetencia.CL_COMPETENCIA,
                        vCompetencia.NB_COMPETENCIA, vCompetencia.DS_COMPETENCIA, vCompetencia.CL_TIPO_COMPETENCIA,
                        vCompetencia.CL_CLASIFICACION, vCompetencia.FG_ACTIVO,  
                        vCompetencia.DS_NIVEL_COMPETENCIA_PUESTO_N0,vCompetencia.DS_NIVEL_COMPETENCIA_PERSONA_N0,
                        vCompetencia.DS_NIVEL_COMPETENCIA_PUESTO_N1,vCompetencia.DS_NIVEL_COMPETENCIA_PERSONA_N1,
                        vCompetencia.DS_NIVEL_COMPETENCIA_PUESTO_N2,vCompetencia.DS_NIVEL_COMPETENCIA_PERSONA_N2,
                        vCompetencia.DS_NIVEL_COMPETENCIA_PUESTO_N3,vCompetencia.DS_NIVEL_COMPETENCIA_PERSONA_N3,
                        vCompetencia.DS_NIVEL_COMPETENCIA_PUESTO_N4,vCompetencia.DS_NIVEL_COMPETENCIA_PERSONA_N4,
                        vCompetencia.DS_NIVEL_COMPETENCIA_PUESTO_N5,vCompetencia.DS_NIVEL_COMPETENCIA_PERSONA_N5,
                        vCompetencia.XML_CAMPOS_ADICIONALES,
                        pClUsuario, pClUsuario, pNbPrograma, pNbPrograma, pTipoTransaccion);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
                
        public XElement EliminarCompetencia(int? pIdCompetencia = null, string pClUsuario = null, string pNbPrograma = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_C_COMPETENCIA(pout_clave_retorno, pIdCompetencia, pClUsuario, pNbPrograma);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        
        public List<SPE_OBTIENE_COMPETENCIAS_LABORALES_Result> ObtenerCompetenciasLaborales()
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_COMPETENCIAS_LABORALES().ToList();
            }
        }        
    }
}
