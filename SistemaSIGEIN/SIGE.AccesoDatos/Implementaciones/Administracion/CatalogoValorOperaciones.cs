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

    public class CatalogoValorOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  C_CATALOGO_VALOR
        public List<SPE_OBTIENE_C_CATALOGO_VALOR_Result> Obtener_C_CATALOGO_VALOR(int? ID_CATALOGO_VALOR = null, String CL_CATALOGO_VALOR = null, String NB_CATALOGO_VALOR = null, String DS_CATALOGO_VALOR = null, int? ID_CATALOGO_LISTA = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_C_CATALOGO_VALOR in context.SPE_OBTIENE_C_CATALOGO_VALOR(ID_CATALOGO_VALOR, CL_CATALOGO_VALOR, NB_CATALOGO_VALOR, DS_CATALOGO_VALOR, ID_CATALOGO_LISTA)
                        select V_C_CATALOGO_VALOR;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_CATALOGO_VALOR
        public XElement InsertaActualiza_C_CATALOGO_VALOR(string tipo_transaccion, SPE_OBTIENE_C_CATALOGO_VALOR_Result V_C_CATALOGO_VALOR, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_C_CATALOGO_VALOR(pout_clave_retorno, V_C_CATALOGO_VALOR.ID_CATALOGO_VALOR, V_C_CATALOGO_VALOR.CL_CATALOGO_VALOR, V_C_CATALOGO_VALOR.NB_CATALOGO_VALOR, V_C_CATALOGO_VALOR.DS_CATALOGO_VALOR, V_C_CATALOGO_VALOR.ID_CATALOGO_LISTA, usuario, usuario, programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
                //return Convert.ToString(pout_clave_retorno.Value);

                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        #endregion

        #region ELIMINA DATOS  C_CATALOGO_VALOR
        public XElement Elimina_C_CATALOGO_VALOR(int? ID_CATALOGO_VALOR = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_C_CATALOGO_VALOR(pout_clave_retorno, ID_CATALOGO_VALOR, usuario, programa);
                //regresamos el valor de retorno de sql				
               // return Convert.ToString(pout_clave_retorno.Value);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        #endregion

    }
}