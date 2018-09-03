using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Negocio.Administracion
{
    public class CompetenciaNivelNegocio
    {


        public List<SPE_OBTIENE_C_COMPETENCIA_NIVEL_Result> ObtieneCompetenciaNivel(int? pIdNivelCompetencia = null, String pClNivelCompetencia = null, String pNbNivelCompetencia = null, String pDsNivelCompetenciaPuesto = null, String pDsNivelCompetenciaPersona = null, Decimal? pNoValorNivel = null, int? pIdCompetencia = null)
		{
            CompetenciaNivelOperaciones oCompetencia = new CompetenciaNivelOperaciones();
            return oCompetencia.ObtenerCompetenciaNivel(pIdNivelCompetencia, pClNivelCompetencia, pNbNivelCompetencia, pDsNivelCompetenciaPuesto, pDsNivelCompetenciaPersona, pNoValorNivel, pIdCompetencia);
		}
        

       /* #region INSERTA ACTUALIZA DATOS  C_COMPETENCIA_NIVEL
        public bool InsertaActualiza_C_COMPETENCIA_NIVEL(string tipo_transaccion, SPE_OBTIENE_C_COMPETENCIA_NIVEL_Result V_C_COMPETENCIA_NIVEL, string usuario, string programa)
		{
			TipoCompetenciaNegocio ntipocompetencia = new TipoCompetenciaNegocio();Operaciones operaciones = new TipoCompetenciaNegocio ntipocompetencia = new TipoCompetenciaNegocio();Operaciones();
			return operaciones.InsertaActualiza_C_COMPETENCIA_NIVEL(tipo_transaccion,V_C_COMPETENCIA_NIVEL, usuario,programa);
		}
        #endregion
        */

        public string EliminaCompetenciaNivel(int? pIdNivelCompetencia = null, string pClUsuario = null, string pNbPrograma = null)
		{
            CompetenciaNivelOperaciones oCompetenciaNivel = new CompetenciaNivelOperaciones();
            return oCompetenciaNivel.EliminarCompetenciaNivel(pIdNivelCompetencia, pClUsuario, pNbPrograma);
		}
        
    }
}
