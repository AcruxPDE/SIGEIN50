using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades.Administracion;
using SIGE.Entidades;
using System.Data.Objects;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{

    public class CuestionarioPreguntaOperaciones
    {

        private SistemaSigeinEntities context;

  
        public List<SPE_OBTIENE_K_CUESTIONARIO_PREGUNTA_Result> Obtener_K_CUESTIONARIO_PREGUNTA(int? ID_CUESTIONARIO_PREGUNTA = null, int? ID_CUESTIONARIO = null, int? ID_PREGUNTA = null, String NB_PREGUNTA = null, String NB_RESPUESTA = null, Decimal? NO_VALOR_RESPUESTA = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_K_CUESTIONARIO_PREGUNTA(ID_CUESTIONARIO_PREGUNTA, ID_CUESTIONARIO, ID_PREGUNTA, NB_PREGUNTA, NB_RESPUESTA, NO_VALOR_RESPUESTA).ToList();
            }
        }
                
        public XElement InsertaActualiza_K_CUESTIONARIO_PREGUNTA(string tipo_transaccion,int? pIdevaluado , int? pIdevaluador, int? pIdCuestionarioPregunta, int? pIdCuestionario, string XML_CUESTIONARIO, string pNbPrueba, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                //context.SPE_INSERTA_ACTUALIZA_K_CUESTIONARIO_PREGUNTA(pout_clave_retorno, V_K_CUESTIONARIO_PREGUNTA.ID_CUESTIONARIO_PREGUNTA, V_K_CUESTIONARIO_PREGUNTA.ID_CUESTIONARIO, V_K_CUESTIONARIO_PREGUNTA.ID_PREGUNTA, V_K_CUESTIONARIO_PREGUNTA.NB_PREGUNTA, V_K_CUESTIONARIO_PREGUNTA.NB_RESPUESTA, V_K_CUESTIONARIO_PREGUNTA.NO_VALOR_RESPUESTA, usuario, usuario, programa, programa, tipo_transaccion);
                context.SPE_INSERTA_ACTUALIZA_K_CUESTIONARIO_PREGUNTA(pOutClRetorno,pIdevaluado,pIdevaluador,pIdCuestionarioPregunta,pIdCuestionario,XML_CUESTIONARIO, pNbPrueba, usuario, usuario, programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }
            
        //public int Elimina_K_CUESTIONARIO_PREGUNTA(int? ID_CUESTIONARIO_PREGUNTA = null, string usuario = null, string programa = null)
        //{
        //    using (context = new SistemaSigeinEntities())
        //    {
        //        //Declaramos el objeto de valor de retorno
        //        ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
        //        context.SPE_ELIMINA_K_CUESTIONARIO_PREGUNTA(pout_clave_retorno, ID_CUESTIONARIO_PREGUNTA, usuario, programa);
        //        //regresamos el valor de retorno de sql				
        //        return Convert.ToInt32(pout_clave_retorno.Value);
        //    }
        //}
           
        public List<SPE_OBTIENE_K_CUESTIONARIO_PREGUNTA_PPRUEBA_Result> Obtener_K_CUESTIONARIO_PREGUNTA_PRUEBA(int? ID_PRUEBA = null, Guid? CL_TOKEN = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                 return context.SPE_OBTIENE_K_CUESTIONARIO_PREGUNTA_PPRUEBA(ID_PRUEBA, CL_TOKEN).ToList();
            }
        }
       
    }
}