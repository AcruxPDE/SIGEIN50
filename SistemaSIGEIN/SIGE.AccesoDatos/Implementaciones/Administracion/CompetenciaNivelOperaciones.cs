using SIGE.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal
{
   public class CompetenciaNivelOperaciones
    {

        private SistemaSigeinEntities context;

        
        public List<SPE_OBTIENE_C_COMPETENCIA_NIVEL_Result> ObtenerCompetenciaNivel(int? pIdNivelCompetencia = null, String pClNivelCompetencia = null, String pNbNivelCompetencia = null, String pDsNivelCompetenciaPuesto = null, String pDsNivelCompetenciaPersona = null, Decimal? pNoValorNivel = null, int? pIdCompetencia = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_C_COMPETENCIA_NIVEL(pIdNivelCompetencia, pClNivelCompetencia, pNbNivelCompetencia, pDsNivelCompetenciaPuesto, pDsNivelCompetenciaPersona, pNoValorNivel, pIdCompetencia).ToList();
            }
        }
        

       /* #region INSERTA ACTUALIZA DATOS  C_COMPETENCIA_NIVEL
        public bool InsertaActualiza_C_COMPETENCIA_NIVEL(string tipo_transaccion, SPE_OBTIENE_C_COMPETENCIA_NIVEL_Result V_C_COMPETENCIA_NIVEL, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(bool));
                context.SPE_INSERTA_ACTUALIZA_C_COMPETENCIA_NIVEL(pout_clave_retorno, V_C_COMPETENCIA_NIVEL.ID_NIVEL_COMPETENCIA, V_C_COMPETENCIA_NIVEL.CL_NIVEL_COMPETENCIA, V_C_COMPETENCIA_NIVEL.NB_NIVEL_COMPETENCIA, V_C_COMPETENCIA_NIVEL.DS_NIVEL_COMPETENCIA_PUESTO, V_C_COMPETENCIA_NIVEL.DS_NIVEL_COMPETENCIA_PERSONA, V_C_COMPETENCIA_NIVEL.NO_VALOR_NIVEL, V_C_COMPETENCIA_NIVEL.ID_COMPETENCIA, usuario, usuario, programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
                return Convert.ToBoolean(pout_clave_retorno.Value); ;
            }
        }
        #endregion
       */
        
        public string EliminarCompetenciaNivel(int? pIdNivelCompetencia = null, string pClUsuario = null, string pNbPrograma = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_C_COMPETENCIA_NIVEL(pout_clave_retorno, pIdNivelCompetencia, pClUsuario, pNbPrograma);
                return Convert.ToString(pout_clave_retorno.Value);
            }
        }
        
    }
}
