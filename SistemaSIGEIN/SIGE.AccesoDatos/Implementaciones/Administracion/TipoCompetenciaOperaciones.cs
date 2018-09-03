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

    public class TipoCompetenciasOperaciones
    {

        private SistemaSigeinEntities context;
        
        public List<SPE_OBTIENE_S_TIPO_COMPETENCIA_Result> ObtenerTipoCompetencia(string pClTipoCompetencia = null, string pNbTipoCompetencia = null, string pDsTipoCompetencia = null, bool? pFgActivo = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_S_TIPO_COMPETENCIA(pClTipoCompetencia, pNbTipoCompetencia, pDsTipoCompetencia, pFgActivo).ToList();
            }
        }
                
        public XElement InsertarActualizarTipoCompetencia(string pTipoTransaccion, SPE_OBTIENE_S_TIPO_COMPETENCIA_Result pTipoCompetencia, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_S_TIPO_COMPETENCIA(pout_clave_retorno, pTipoCompetencia.CL_TIPO_COMPETENCIA, pTipoCompetencia.NB_TIPO_COMPETENCIA, pTipoCompetencia.DS_TIPO_COMPETENCIA,pTipoCompetencia.FG_ACTIVO, pTipoTransaccion, pClUsuario, pNbPrograma);             
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
                
        public XElement EliminarTipoCompetencia(String pClTipoCompetencia = null, string pClUsuario = null, string pNbPrograma = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_S_TIPO_COMPETENCIA(pout_clave_retorno, pClTipoCompetencia, pClUsuario, pNbPrograma);
                return XElement.Parse(pout_clave_retorno.Value.ToString());

            }
        }
    }
}