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

    public class CatalogoListaOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  C_CATALOGO_LISTA
        public List<SPE_OBTIENE_C_CATALOGO_LISTA_Result> ObtenerCatalogoLista(int? ID_CATALOGO_LISTA = null, String NB_CATALOGO_LISTA = null, String DS_CATALOGO_LISTA = null, int? ID_CATALOGO_TIPO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_C_CATALOGO_LISTA(ID_CATALOGO_LISTA, NB_CATALOGO_LISTA, DS_CATALOGO_LISTA, ID_CATALOGO_TIPO).ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_CATALOGO_LISTA
        public XElement InsertaActualiza_C_CATALOGO_LISTA(string tipo_transaccion, SPE_OBTIENE_C_CATALOGO_LISTA_Result V_C_CATALOGO_LISTA, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
               // pout_clave_retorno.Value = "";
                
                context.SPE_INSERTA_ACTUALIZA_C_CATALOGO_LISTA(pout_clave_retorno, V_C_CATALOGO_LISTA.ID_CATALOGO_LISTA, V_C_CATALOGO_LISTA.NB_CATALOGO_LISTA, V_C_CATALOGO_LISTA.DS_CATALOGO_LISTA, V_C_CATALOGO_LISTA.ID_CATALOGO_TIPO, usuario, usuario, programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
             //   return Convert.ToString(pout_clave_retorno.Value); 
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        #endregion

        #region ELIMINA DATOS  C_CATALOGO_LISTA
        public XElement Elimina_C_CATALOGO_LISTA(int? ID_CATALOGO_LISTA = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_C_CATALOGO_LISTA(pout_clave_retorno, ID_CATALOGO_LISTA, usuario, programa);
                //regresamos el valor de retorno de sql				
               // return Convert.ToString(pout_clave_retorno.Value);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        #endregion

    }
}