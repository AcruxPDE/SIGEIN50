using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using System.Data.Objects;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{

    public class CatalogoTipoOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  S_CATALOGO_TIPO
        public List<SPE_OBTIENE_S_CATALOGO_TIPO_Result> Obtener_S_CATALOGO_TIPO(int? ID_CATALOGO_TIPO = null, String NB_CATALOGO_TIPO = null, String DS_CATALOGO_TIPO = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_S_CATALOGO_TIPO in context.SPE_OBTIENE_S_CATALOGO_TIPO(ID_CATALOGO_TIPO, NB_CATALOGO_TIPO, DS_CATALOGO_TIPO)
                        select V_S_CATALOGO_TIPO;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  S_CATALOGO_TIPO
        public int InsertaActualiza_S_CATALOGO_TIPO(string tipo_transaccion, SPE_OBTIENE_S_CATALOGO_TIPO_Result V_S_CATALOGO_TIPO, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_S_CATALOGO_TIPO(pout_clave_retorno, V_S_CATALOGO_TIPO.ID_CATALOGO_TIPO, V_S_CATALOGO_TIPO.NB_CATALOGO_TIPO, V_S_CATALOGO_TIPO.DS_CATALOGO_TIPO, tipo_transaccion, usuario, programa);
                //regresamos el valor de retorno de sql
                return Convert.ToInt32(pout_clave_retorno.Value); ;
            }
        }
        #endregion

        #region ELIMINA DATOS  S_CATALOGO_TIPO
        public int Elimina_S_CATALOGO_TIPO(int? ID_CATALOGO_TIPO = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_S_CATALOGO_TIPO(pout_clave_retorno, ID_CATALOGO_TIPO, usuario, programa);
                //regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
        #endregion

    }
}
