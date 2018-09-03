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
   
    public class GrupoPreguntaOperaciones
    {

        private SistemaSigeinEntities context;		

        #region OBTIENE DATOS  C_GRUPO_PREGUNTA
        public List<SPE_OBTIENE_C_GRUPO_PREGUNTA_Result> Obtener_C_GRUPO_PREGUNTA(int?  ID_GRUPO_PREGUNTA = null,String CL_GRUPO_PREGUNTA = null,String NB_GRUPO_PREGUNTA = null,int?  ID_PREGUNTA = null,DateTime?  FE_CREACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_APP_CREA = null,String CL_USUARIO_APP_MODIFICA = null,String NB_PROGRAMA_CREA = null,String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities ())
            {
                var q = from V_C_GRUPO_PREGUNTA in context.SPE_OBTIENE_C_GRUPO_PREGUNTA(ID_GRUPO_PREGUNTA,CL_GRUPO_PREGUNTA,NB_GRUPO_PREGUNTA,ID_PREGUNTA)
                select V_C_GRUPO_PREGUNTA;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_GRUPO_PREGUNTA
        public int InsertaActualiza_C_GRUPO_PREGUNTA(string tipo_transaccion, SPE_OBTIENE_C_GRUPO_PREGUNTA_Result V_C_GRUPO_PREGUNTA,string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities ())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_C_GRUPO_PREGUNTA(pout_clave_retorno,  V_C_GRUPO_PREGUNTA.ID_GRUPO_PREGUNTA,V_C_GRUPO_PREGUNTA.CL_GRUPO_PREGUNTA,V_C_GRUPO_PREGUNTA.NB_GRUPO_PREGUNTA,V_C_GRUPO_PREGUNTA.ID_PREGUNTA,usuario,usuario,programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
                return Convert.ToInt32(pout_clave_retorno.Value); ; 
            }
        }
        #endregion

        #region ELIMINA DATOS  C_GRUPO_PREGUNTA
        public int Elimina_C_GRUPO_PREGUNTA(SPE_OBTIENE_C_GRUPO_PREGUNTA_Result V_C_GRUPO_PREGUNTA, string usuario  = null, string programa  = null)
        {
            using (context = new SistemaSigeinEntities ())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_C_GRUPO_PREGUNTA(pout_clave_retorno, V_C_GRUPO_PREGUNTA.ID_GRUPO_PREGUNTA, V_C_GRUPO_PREGUNTA.CL_GRUPO_PREGUNTA, V_C_GRUPO_PREGUNTA.NB_GRUPO_PREGUNTA, V_C_GRUPO_PREGUNTA.ID_PREGUNTA, usuario, programa);
                //regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
        #endregion

    }
}
