using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades.Administracion;
using SIGE.Entidades;
using System.Data.Objects;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal
{

    public class ConfiguracionOperaciones
    {

        private SistemaSigeinEntities context;

        public SPE_OBTIENE_S_CONFIGURACION_Result ObtenerConfiguracion(String XML_CONFIGURACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_MODIFICA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_S_CONFIGURACION(XML_CONFIGURACION, CL_USUARIO_MODIFICA).FirstOrDefault();
            }
        }

        public XElement InsertaActualizaConfiguracion(string pClTipoTransaccion, XElement pXmlConfiguracion, byte[] pFiLogotipo, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_S_CONFIGURACION(pout_clave_retorno, pXmlConfiguracion.ToString(), pFiLogotipo, pClUsuario, pNbPrograma, pClTipoTransaccion);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        
        public int Elimina_S_CONFIGURACION(string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_S_CONFIGURACION(pout_clave_retorno, usuario, programa);
                //regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
        
    }
}