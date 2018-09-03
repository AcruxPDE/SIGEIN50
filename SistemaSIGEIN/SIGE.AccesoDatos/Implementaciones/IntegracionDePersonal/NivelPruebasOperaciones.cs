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
    public class NivelPruebasOperaciones
    {


        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  C_PRUEBA_NIVEL
        public List<SPE_OBTIENE_C_PRUEBA_NIVEL_Result> Obtener_C_PRUEBA_NIVEL
            (int? pIdPruebaNivel = null, string pClPruebaNivel = null, string pNbPruebaNivel = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_C_PRUEBA_NIVEL in context.SPE_OBTIENE_C_PRUEBA_NIVEL(pIdPruebaNivel, pClPruebaNivel, pNbPruebaNivel)
                        select V_C_PRUEBA_NIVEL;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_PRUEBA_NIVEL
        public XElement InsertaActualiza_C_PRUEBA_NIVEL(string tipo_transaccion, SPE_OBTIENE_C_PRUEBA_NIVEL_Result V_C_PRUEBA_NIVEL, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_C_PRUEBA_NIVEL(pout_clave_retorno, V_C_PRUEBA_NIVEL.ID_PRUEBA_NIVEL, V_C_PRUEBA_NIVEL.CL_PRUEBA_NIVEL, V_C_PRUEBA_NIVEL.NB_PRUEBA_NIVEL, usuario, usuario, programa, programa, tipo_transaccion);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        #endregion

        #region ELIMINA DATOS  C_PRUEBA_NIVEL
        public XElement Elimina_C_PRUEBA_NIVEL(int? pIdPruebaNivel = null,String pClPruebaNivel=null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_C_PRUEBA_NIVEL(pout_clave_retorno, pIdPruebaNivel,pClPruebaNivel, usuario, programa);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        #endregion


        ////////////////////////////////////////K_PRUEBA_NIVEL////////////////////////////////////////////////////
        #region OBTIENE DATOS  K_PRUEBA_NIVEL
        public List<SPE_OBTIENE_K_PRUEBA_NIVEL_Result> Obtener_K_PRUEBA_NIVEL
            (int? pIdPruebaNivel = null, int? pIdPrueba = null )
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_K_PRUEBA_NIVEL in context.SPE_OBTIENE_K_PRUEBA_NIVEL(pIdPruebaNivel, pIdPrueba)
                        select V_K_PRUEBA_NIVEL;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  K_PRUEBA_NIVEL
        public XElement InsertaActualiza_K_PRUEBA_NIVEL(string tipo_transaccion, SPE_OBTIENE_K_PRUEBA_NIVEL_Result V_K_PRUEBA_NIVEL, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_K_PRUEBA_NIVEL(pout_clave_retorno, V_K_PRUEBA_NIVEL.ID_PRUEBA_NIVEL, V_K_PRUEBA_NIVEL.ID_PRUEBA, usuario, usuario, programa, programa, tipo_transaccion);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        #endregion

        #region ELIMINA DATOS  K_PRUEBA_NIVEL
        public XElement Elimina_K_PRUEBA_NIVEL(int? pIdPruebaNivel = null, int? pIdPrueba = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_K_PRUEBA_NIVEL(pout_clave_retorno, pIdPruebaNivel, pIdPrueba, usuario, programa);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        #endregion
    }
}
