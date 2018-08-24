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
   
    public class PuestoRelacionadoOperaciones
    {

        private SistemaSigeinEntities context;		

        #region OBTIENE DATOS  C_PUESTO_RELACIONADO
        public List<SPE_OBTIENE_C_PUESTO_RELACIONADO_Result> Obtener_C_PUESTO_RELACIONADO(int?  ID_PUESTO = null,int?  ID_PUESTO_RELACIONADO = null,String CL_TIPO_RELACION = null,String DS_PUESTO_RELACIONADO = null,DateTime?  FE_CREACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_APP_CREA = null,String CL_USUARIO_APP_MODIFICA = null,String NB_PROGRAMA_CREA = null,String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities ())
            {
                var q = from V_C_PUESTO_RELACIONADO in context.SPE_OBTIENE_C_PUESTO_RELACIONADO(ID_PUESTO,ID_PUESTO_RELACIONADO,CL_TIPO_RELACION,DS_PUESTO_RELACIONADO)
                select V_C_PUESTO_RELACIONADO;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_PUESTO_RELACIONADO
        public int InsertaActualiza_C_PUESTO_RELACIONADO(string tipo_transaccion, SPE_OBTIENE_C_PUESTO_RELACIONADO_Result V_C_PUESTO_RELACIONADO,string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities ())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_C_PUESTO_RELACIONADO(pout_clave_retorno,  V_C_PUESTO_RELACIONADO.ID_PUESTO,V_C_PUESTO_RELACIONADO.ID_PUESTO_RELACIONADO,V_C_PUESTO_RELACIONADO.CL_TIPO_RELACION,V_C_PUESTO_RELACIONADO.DS_PUESTO_RELACIONADO,usuario,usuario,programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
                return Convert.ToInt32(pout_clave_retorno.Value); ; 
            }
        }
        #endregion

        #region ELIMINA DATOS  C_PUESTO_RELACIONADO
        public int Elimina_C_PUESTO_RELACIONADO(SPE_OBTIENE_C_PUESTO_RELACIONADO_Result V_C_PUESTO_RELACIONADO, string usuario  = null, string programa  = null)
        {
            using (context = new SistemaSigeinEntities ())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_C_PUESTO_RELACIONADO(pout_clave_retorno, V_C_PUESTO_RELACIONADO.ID_PUESTO,V_C_PUESTO_RELACIONADO.ID_PUESTO_RELACIONADO, V_C_PUESTO_RELACIONADO.CL_TIPO_RELACION, usuario, programa);
                //regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
        #endregion

    }
}