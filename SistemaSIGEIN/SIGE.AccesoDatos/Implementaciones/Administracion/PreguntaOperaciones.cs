using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades.Administracion;
using SIGE.Entidades;
using System.Data.Objects;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{

    public class PreguntaOperaciones
    {

        private SistemaSigeinEntities context;
        
        //public List<SPE_OBTIENE_C_PREGUNTA_Result> Obtener_C_PREGUNTA(int? ID_PREGUNTA = null, String CL_PREGUNTA = null, String NB_PREGUNTA = null, String DS_PREGUNTA = null, String CL_TIPO_PREGUNTA = null, Decimal? NO_VALOR = null, bool? FG_REQUERIDO = null, bool? FG_ACTIVO = null, int? ID_COMPETENCIA = null, int? ID_BITACORA = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        //{
        //    using (context = new SistemaSigeinEntities())
        //    {
        //        var q = from V_C_PREGUNTA in context.SPE_OBTIENE_C_PREGUNTA(ID_PREGUNTA, CL_PREGUNTA, NB_PREGUNTA, DS_PREGUNTA, CL_TIPO_PREGUNTA, NO_VALOR, FG_REQUERIDO, FG_ACTIVO, ID_COMPETENCIA, ID_BITACORA)
        //                select V_C_PREGUNTA;
        //        return q.ToList();
        //    }
        //}
     
        public List<SPE_OBTIENE_K_PREGUNTA_Result> Obtener_K_PREGUNTA(int? ID_PREGUNTA = null, String CL_PREGUNTA = null, String NB_PREGUNTA = null, String DS_PREGUNTA = null, String CL_TIPO_PREGUNTA = null, Decimal? NO_VALOR = null, bool? FG_REQUERIDO = null, bool? FG_ACTIVO = null, int? ID_COMPETENCIA = null, int? ID_BITACORA = null, int? ID_PRUEBA = null, int? ID_CUESTIONARIO = null, Guid? CL_TOKEN_EXTERNO = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_K_PREGUNTA(ID_PREGUNTA, CL_PREGUNTA, NB_PREGUNTA, DS_PREGUNTA, CL_TIPO_PREGUNTA, NO_VALOR, FG_REQUERIDO, FG_ACTIVO, ID_COMPETENCIA, ID_BITACORA, ID_PRUEBA, ID_CUESTIONARIO, CL_TOKEN_EXTERNO).ToList();
            }
        }

        
        //public int InsertaActualiza_C_PREGUNTA(string tipo_transaccion, SPE_OBTIENE_C_PREGUNTA_Result V_C_PREGUNTA, string usuario, string programa)
        //{
        //    using (context = new SistemaSigeinEntities())
        //    {
        //        //Declaramos el objeto de valor de retorno
        //        ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
        //        context.SPE_INSERTA_ACTUALIZA_C_PREGUNTA(pout_clave_retorno, V_C_PREGUNTA.ID_PREGUNTA, V_C_PREGUNTA.CL_PREGUNTA, V_C_PREGUNTA.NB_PREGUNTA, V_C_PREGUNTA.DS_PREGUNTA, V_C_PREGUNTA.CL_TIPO_PREGUNTA, V_C_PREGUNTA.NO_VALOR, V_C_PREGUNTA.FG_REQUERIDO, V_C_PREGUNTA.FG_ACTIVO, V_C_PREGUNTA.ID_COMPETENCIA, V_C_PREGUNTA.ID_BITACORA, usuario, usuario, programa, programa, tipo_transaccion);
        //        //regresamos el valor de retorno de sql
        //        return Convert.ToInt32(pout_clave_retorno.Value); ;
        //    }
        //}
                
        //public int Elimina_C_PREGUNTA(int? ID_PREGUNTA = null, string usuario = null, string programa = null)
        //{
        //    using (context = new SistemaSigeinEntities())
        //    {
        //        //Declaramos el objeto de valor de retorno
        //        ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
        //        context.SPE_ELIMINA_C_PREGUNTA(pout_clave_retorno, ID_PREGUNTA, usuario, programa);
        //        //regresamos el valor de retorno de sql				
        //        return Convert.ToInt32(pout_clave_retorno.Value);
        //    }
        //}        

    }
}