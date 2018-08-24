using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using System.Data.Objects;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{
   
    public class ClasificacionCompetenciaOperaciones
    {
        private SistemaSigeinEntities context;
		
		public List<SPE_OBTIENE_C_CLASIFICACION_COMPETENCIA_Result> ObtenerClasificacionCompetencia(int? pIdClasificacionCompetencia = null, String pClClasificacion = null, String pClTipoCompetecia = null, String pNbClasificacionCompetencia = null, String pDsClasificacionCompetencia = null, String pDsNotasClasificacion = null, bool? pFgActivo = null)
		{
			using (context = new SistemaSigeinEntities ())
			{
                return context.SPE_OBTIENE_C_CLASIFICACION_COMPETENCIA(pIdClasificacionCompetencia, pClClasificacion, pClTipoCompetecia, pNbClasificacionCompetencia, pDsClasificacionCompetencia, pDsNotasClasificacion, pFgActivo).ToList();
			}
		}
				
		public XElement InsertarActualizarClasificacionCompetencia(string pTipoTransaccion, SPE_OBTIENE_C_CLASIFICACION_COMPETENCIA_Result pClasificacionCompetencia,string pClUsuario, string pNbPrograma)
		{
			using (context = new SistemaSigeinEntities ())
			{			
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
				context.SPE_INSERTA_ACTUALIZA_C_CLASIFICACION_COMPETENCIA(pOutClRetorno,  pClasificacionCompetencia.ID_CLASIFICACION_COMPETENCIA,pClasificacionCompetencia.CL_CLASIFICACION,pClasificacionCompetencia.CL_TIPO_COMPETENCIA,pClasificacionCompetencia.NB_CLASIFICACION_COMPETENCIA,pClasificacionCompetencia.DS_CLASIFICACION_COMPETENCIA,pClasificacionCompetencia.DS_NOTAS_CLASIFICACION,pClasificacionCompetencia.CL_COLOR,pClasificacionCompetencia.FG_ACTIVO,pClUsuario,pClUsuario,pNbPrograma, pNbPrograma, pTipoTransaccion);				
                return XElement.Parse(pOutClRetorno.Value.ToString());
			}
		}
				
		public XElement EliminarClasificacionCompetencia(int? pIdClasificacionCompetencia = null, string pClUsuario  = null, string pNbPrograma  = null)
		{
			using (context = new SistemaSigeinEntities ())
			{				
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_C_CLASIFICACION_COMPETENCIA(pout_clave_retorno, pIdClasificacionCompetencia, pClUsuario, pNbPrograma);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
			}
		}
	}
}