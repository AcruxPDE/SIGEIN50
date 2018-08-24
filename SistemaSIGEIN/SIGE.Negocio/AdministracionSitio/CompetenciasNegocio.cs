using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal; // reemplazar por la carpeta correspondiente
using SIGE.Entidades.Administracion;
using System.Xml.Linq;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;

namespace SIGE.Negocio.Administracion  // reemplazar por la carpeta correspondiente
{
    public class CompetenciaNegocio
    {


        public List<SPE_OBTIENE_C_COMPETENCIA_Result> ObtieneCompetencias(int? pIdCompetencia = null, string pClCompetencia = null, string pNbCompetencia = null, string pDsCompetencia = null, string pClTipoCompetencia = null, string pClClasificacion = null, bool? pFgActivo = null, string pXmlCamposAdicionales = null, XElement pXmlSeleccion = null)
		{  
		    CompetenciaOperaciones oCompetencia = new CompetenciaOperaciones();
            return oCompetencia.ObtenerCompetencias(pIdCompetencia, pClCompetencia, pNbCompetencia, pDsCompetencia, pClTipoCompetencia, pClClasificacion, pFgActivo, pXmlCamposAdicionales);
		}

        public List<SPE_OBTIENE_FACTORES_EVALUACION_TABULADOR_Result> ObtenerFactoresValuacion(int? pIdFactor = null)
        {
            CompetenciaOperaciones oCompetencia = new CompetenciaOperaciones();
            return oCompetencia.ObtenerFactoresValuacion(pIdFactor);
        }  
		        
        public E_RESULTADO InsertaActualizaCompetencia(string pTipoTransaccion, E_COMPETENCIA_NIVEL pCompetencia, string pClUsuario, string pNbPrograma)
        {
            CompetenciaOperaciones oCompetencia = new CompetenciaOperaciones();
            return UtilRespuesta.EnvioRespuesta(oCompetencia.InsertarActualizarCompetencia(pTipoTransaccion, pCompetencia, pClUsuario, pNbPrograma));
        }
                
        public E_RESULTADO EliminaCompetenica(int? pIdCompetencia = null, string pClUsuario = null, string pNbPrograma = null)
        {
            CompetenciaOperaciones oCompetencia = new CompetenciaOperaciones();
            return UtilRespuesta.EnvioRespuesta(oCompetencia.EliminarCompetencia(pIdCompetencia, pClUsuario, pNbPrograma));
        }
                
        public List<SPE_OBTIENE_COMPETENCIAS_LABORALES_Result> ObtenerCompetenciasLaborales()
        {
            CompetenciaOperaciones oCompetencia = new CompetenciaOperaciones();
            return oCompetencia.ObtenerCompetenciasLaborales();
        }        
    }
}