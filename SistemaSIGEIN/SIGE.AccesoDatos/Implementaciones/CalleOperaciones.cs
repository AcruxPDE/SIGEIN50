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

    public class CalleOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  C_CALLE
        public List<SPE_OBTIENE_C_CALLE_Result> Obtener_C_CALLE(int? ID_CALLE = null, String CL_PAIS = null, String CL_ESTADO = null, String CL_MUNICIPIO = null, String CL_COLONIA = null, String CL_CALLE = null, String NB_CALLE = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_C_CALLE in context.SPE_OBTIENE_C_CALLE(ID_CALLE, CL_PAIS, CL_ESTADO, CL_MUNICIPIO, CL_COLONIA, CL_CALLE, NB_CALLE)
                        select V_C_CALLE;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_CALLE
        public int InsertaActualiza_C_CALLE(string tipo_transaccion, SPE_OBTIENE_C_CALLE_Result V_C_CALLE, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_C_CALLE(pout_clave_retorno, V_C_CALLE.ID_CALLE, V_C_CALLE.CL_PAIS, V_C_CALLE.CL_ESTADO, V_C_CALLE.CL_MUNICIPIO, V_C_CALLE.CL_COLONIA, V_C_CALLE.CL_CALLE, V_C_CALLE.NB_CALLE, usuario, usuario, programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
                return Convert.ToInt32(pout_clave_retorno.Value); ;
            }
        }
        #endregion

        #region ELIMINA DATOS  C_CALLE
        public int Elimina_C_CALLE(int? ID_CALLE = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_C_CALLE(pout_clave_retorno, ID_CALLE, usuario, programa);
                //regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
        #endregion

    }
}