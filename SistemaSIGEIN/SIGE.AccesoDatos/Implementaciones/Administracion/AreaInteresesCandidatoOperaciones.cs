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

    public class AreaInteresCandidatoOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  K_AREA_INTERES
        public List<SPE_OBTIENE_K_AREA_INTERES_Result> Obtener_K_AREA_INTERES(int? ID_CANDIDATO_AREA_INTERES = null, int? ID_CANDIDATO = null, int? ID_AREA_INTERES = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_K_AREA_INTERES in context.SPE_OBTIENE_K_AREA_INTERES(ID_CANDIDATO_AREA_INTERES, ID_CANDIDATO, ID_AREA_INTERES)
                        select V_K_AREA_INTERES;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  K_AREA_INTERES
        public int InsertaActualiza_K_AREA_INTERES(string tipo_transaccion, SPE_OBTIENE_K_AREA_INTERES_Result V_K_AREA_INTERES, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));

                context.SPE_INSERTA_ACTUALIZA_K_AREA_INTERES(pout_clave_retorno, V_K_AREA_INTERES.ID_CANDIDATO_AREA_INTERES, V_K_AREA_INTERES.ID_CANDIDATO, V_K_AREA_INTERES.ID_AREA_INTERES, usuario, usuario, programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
        #endregion

        #region ELIMINA DATOS  K_AREA_INTERES
        public int Elimina_K_AREA_INTERES(int? ID_CANDIDATO_AREA_INTERES = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_K_AREA_INTERES(pout_clave_retorno, ID_CANDIDATO_AREA_INTERES, usuario, programa);
                //regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
        #endregion

    }
}
