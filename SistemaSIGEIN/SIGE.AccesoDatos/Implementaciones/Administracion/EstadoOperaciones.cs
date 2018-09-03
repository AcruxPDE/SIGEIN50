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

    public class EstadoOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  C_ESTADO
        public List<SPE_OBTIENE_C_ESTADO_Result> ObtenerEstados(int? ID_ESTADO = null, String CL_PAIS = null, String CL_ESTADO = null, String NB_ESTADO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_C_ESTADO(ID_ESTADO, CL_PAIS, CL_ESTADO, NB_ESTADO).ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_ESTADO
        public int InsertaActualiza_C_ESTADO(string tipo_transaccion, SPE_OBTIENE_C_ESTADO_Result V_C_ESTADO, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_C_ESTADO(pout_clave_retorno, V_C_ESTADO.ID_ESTADO, V_C_ESTADO.CL_PAIS, V_C_ESTADO.CL_ESTADO, V_C_ESTADO.NB_ESTADO, usuario, usuario, programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
                return Convert.ToInt32(pout_clave_retorno.Value); ;
            }
        }
        #endregion

        #region ELIMINA DATOS  C_ESTADO
        public int Elimina_C_ESTADO(int? ID_ESTADO = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_C_ESTADO(pout_clave_retorno, ID_ESTADO, usuario, programa);
                //regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
        #endregion

    }
}
