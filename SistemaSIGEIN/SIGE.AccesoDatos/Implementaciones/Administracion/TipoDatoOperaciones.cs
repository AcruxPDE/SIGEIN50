using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades.Administracion;
//using SIGE.Entidades.EvaluacionOrganizacional;
//using SIGE.Entidades.FormacionDesarrollo;
//using SIGE.Entidades.MetodologiaCompensacion;
using SIGE.Entidades;
using System.Data.Objects;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{
   
    public class TipoDatoOperaciones
    {

		private SistemaSigeinEntities context;		

		#region OBTIENE DATOS  S_TIPO_DATO
        public List<SPE_OBTIENE_S_TIPO_DATO_Result> Obtener_S_TIPO_DATO(String CL_TIPO_DATO = null,String NB_TIPO_DATO = null)
        {
            using (context = new SistemaSigeinEntities ())
            {
                var q = from V_S_TIPO_DATO in context.SPE_OBTIENE_S_TIPO_DATO(CL_TIPO_DATO,NB_TIPO_DATO)
                select V_S_TIPO_DATO;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  S_TIPO_DATO
        public int InsertaActualiza_S_TIPO_DATO(string tipo_transaccion, SPE_OBTIENE_S_TIPO_DATO_Result V_S_TIPO_DATO,string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities ())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_S_TIPO_DATO(pout_clave_retorno,  V_S_TIPO_DATO.CL_TIPO_DATO,V_S_TIPO_DATO.NB_TIPO_DATO,tipo_transaccion,usuario,programa);
                //regresamos el valor de retorno de sql
                return Convert.ToInt32(pout_clave_retorno.Value); ; 
            }
        }
        #endregion

        #region ELIMINA DATOS  S_TIPO_DATO
        public int Elimina_S_TIPO_DATO(SPE_OBTIENE_S_TIPO_DATO_Result V_S_TIPO_DATO, string usuario  = null, string programa  = null)
        {
            using (context = new SistemaSigeinEntities ())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_S_TIPO_DATO(pout_clave_retorno, V_S_TIPO_DATO.CL_TIPO_DATO,V_S_TIPO_DATO.NB_TIPO_DATO, usuario, programa);
                //regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
		#endregion

	}
}