using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal; 
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;
using System.Xml.Linq;


namespace SIGE.Negocio.Administracion  // reemplazar por la carpeta correspondiente
{
    public class ClasificacionCompetenciaNegocio
    {

        public List<SPE_OBTIENE_C_CLASIFICACION_COMPETENCIA_Result> ObtieneClasificacionCompetencia(int? pIdClasificacionCompetencia = null, String pClClasificacion = null, String pClTipoCompetecia = null, String pNbClasificacionCompetencia = null, String pDsClasificacionCompetencia = null, String pDsNotasClasificacion = null, bool? pFgActivo = null)
		{
			ClasificacionCompetenciaOperaciones operaciones = new ClasificacionCompetenciaOperaciones();
            return operaciones.ObtenerClasificacionCompetencia(pIdClasificacionCompetencia, pClClasificacion, pClTipoCompetecia, pNbClasificacionCompetencia, pDsClasificacionCompetencia, pDsNotasClasificacion, pFgActivo);
		}

		
        public E_RESULTADO InsertaActualizaClasificacionCompetencia(string pTipoTransaccion, SPE_OBTIENE_C_CLASIFICACION_COMPETENCIA_Result pClasificacionCompetencia, string pClUsuario, string pNbPrograma)
		{
            ClasificacionCompetenciaOperaciones oClasificacionCompetencia = new ClasificacionCompetenciaOperaciones();
            return UtilRespuesta.EnvioRespuesta(oClasificacionCompetencia.InsertarActualizarClasificacionCompetencia(pTipoTransaccion, pClasificacionCompetencia, pClUsuario, pNbPrograma));
		}	
		
        public E_RESULTADO EliminaClasificacionCompetencia(int? pIdClasificacionCompetencia = null, string pClUsuario = null, string pNbPrograma = null)
		{
			ClasificacionCompetenciaOperaciones oClasificacionCompetencia = new ClasificacionCompetenciaOperaciones();
			return UtilRespuesta.EnvioRespuesta(oClasificacionCompetencia.EliminarClasificacionCompetencia(pIdClasificacionCompetencia, pClUsuario, pNbPrograma));
		}
	}
}