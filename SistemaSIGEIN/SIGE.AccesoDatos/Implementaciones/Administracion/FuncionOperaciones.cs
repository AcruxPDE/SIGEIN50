using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades.Administracion;
using SIGE.Entidades;
using System.Data.Objects;
using SIGE.Entidades.Externas;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{

    public class FuncionOperaciones
    {

        private SistemaSigeinEntities context;

        
        //public List<SPE_OBTIENE_S_FUNCION_Result> Obtener_S_FUNCION(int? ID_FUNCION = null, String CL_FUNCION = null, String CL_TIPO_FUNCION = null, String NB_FUNCION = null, int? ID_FUNCION_PADRE = null, String NB_URL = null, String XML_CONFIGURACION = null)
        //{
        //    using (context = new SistemaSigeinEntities())
        //    {
        //        return context.SPE_OBTIENE_S_FUNCION(ID_FUNCION, CL_FUNCION, CL_TIPO_FUNCION, NB_FUNCION, ID_FUNCION_PADRE, NB_URL, XML_CONFIGURACION).ToList();
        //    }
        //}

        public List<SPE_OBTIENE_FUNCIONES_Result> ObtenerFunciones(E_TIPO_FUNCION pClTipoFuncion)
        {
            string vClTipoFuncion = null;

            if (!pClTipoFuncion.Equals(E_TIPO_FUNCION.TODOS))
                vClTipoFuncion = pClTipoFuncion.ToString();

            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_FUNCIONES(vClTipoFuncion).ToList();
            }
        }
         
        //public int InsertaActualiza_S_FUNCION(string tipo_transaccion, SPE_OBTIENE_S_FUNCION_Result V_S_FUNCION, string usuario, string programa)
        //{
        //    using (context = new SistemaSigeinEntities())
        //    {
        //        //Declaramos el objeto de valor de retorno
        //        ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
        //        context.SPE_INSERTA_ACTUALIZA_S_FUNCION(pout_clave_retorno, V_S_FUNCION.ID_FUNCION, V_S_FUNCION.CL_FUNCION, V_S_FUNCION.CL_TIPO_FUNCION, V_S_FUNCION.NB_FUNCION, V_S_FUNCION.ID_FUNCION_PADRE, V_S_FUNCION.NB_URL, V_S_FUNCION.XML_CONFIGURACION, tipo_transaccion, usuario, programa);
        //        //regresamos el valor de retorno de sql
        //        return Convert.ToInt32(pout_clave_retorno.Value); ;
        //    }
        //}
          
        //public int Elimina_S_FUNCION(int? ID_FUNCION = null, string usuario = null, string programa = null)
        //{
        //    using (context = new SistemaSigeinEntities())
        //    {
        //        //Declaramos el objeto de valor de retorno
        //        ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
        //        context.SPE_ELIMINA_S_FUNCION(pout_clave_retorno, ID_FUNCION, usuario, programa);
        //        //regresamos el valor de retorno de sql				
        //        return Convert.ToInt32(pout_clave_retorno.Value);
        //    }
        //}   
    }
}
