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

    public class PeriodoOperaciones
    {

        private SistemaSigeinEntities context;

        public List<SPE_OBTIENE_C_PERIODO_Result> Obtener_C_PERIODO(int? ID_PERIODO = null, String CL_PERIODO = null, String NB_PERIODO = null, String DS_PERIODO = null, DateTime? FE_INICIO = null, DateTime? FE_TERMINO = null, String CL_ESTADO_PERIODO = null, String XML_CAMPOS_ADICIONALES = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_C_PERIODO(ID_PERIODO, CL_PERIODO, NB_PERIODO, DS_PERIODO, FE_INICIO, FE_TERMINO, CL_ESTADO_PERIODO, XML_CAMPOS_ADICIONALES).ToList();                                 
            }
        }


        //#region INSERTA ACTUALIZA DATOS  C_PERIODO
        //public int InsertaActualiza_C_PERIODO(string tipo_transaccion, SPE_OBTIENE_C_PERIODO_Result V_C_PERIODO, string usuario, string programa)
        //{
        //    using (context = new SistemaSigeinEntities())
        //    {
        //        //Declaramos el objeto de valor de retorno
        //        ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
        //        context.SPE_INSERTA_ACTUALIZA_C_PERIODO(pout_clave_retorno, V_C_PERIODO.ID_PERIODO, V_C_PERIODO.CL_PERIODO, V_C_PERIODO.NB_PERIODO, V_C_PERIODO.DS_PERIODO, V_C_PERIODO.FE_INICIO, V_C_PERIODO.FE_TERMINO, V_C_PERIODO.CL_ESTADO_PERIODO, V_C_PERIODO.XML_CAMPOS_ADICIONALES, usuario, usuario, programa, programa, tipo_transaccion);
        //        //regresamos el valor de retorno de sql
        //        return Convert.ToInt32(pout_clave_retorno.Value); ;
        //    }
        //}
        //#endregion

        //#region ELIMINA DATOS  C_PERIODO
        //public int Elimina_C_PERIODO(int? ID_PERIODO = null, string usuario = null, string programa = null)
        //{
        //    using (context = new SistemaSigeinEntities())
        //    {
        //        //Declaramos el objeto de valor de retorno
        //        ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
        //        context.SPE_ELIMINA_C_PERIODO(pout_clave_retorno, ID_PERIODO, usuario, programa);
        //        //regresamos el valor de retorno de sql				
        //        return Convert.ToInt32(pout_clave_retorno.Value);
        //    }
        //}
        //#endregion

    }
}
