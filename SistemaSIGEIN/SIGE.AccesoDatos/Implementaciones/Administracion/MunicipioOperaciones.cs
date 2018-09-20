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

    public class MunicipioOperaciones
    {

        private SistemaSigeinEntities context;


        public List<SPE_OBTIENE_C_MUNICIPIO_Result> ObtenerMunicipios(int? pIdMunicipio = null, String pClPais = null, String pClEstado = null, String pNbEstado = null, String pClMunicipio = null, String pNbMunicipio = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_C_MUNICIPIO(pIdMunicipio, pClPais, pClEstado,pNbEstado, pClMunicipio, pNbMunicipio).ToList();
            }
        }
                
        public int InsertaActualiza_C_MUNICIPIO(string tipo_transaccion, SPE_OBTIENE_C_MUNICIPIO_Result V_C_MUNICIPIO, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_C_MUNICIPIO(pout_clave_retorno, V_C_MUNICIPIO.ID_MUNICIPIO, V_C_MUNICIPIO.CL_PAIS, V_C_MUNICIPIO.CL_ESTADO, V_C_MUNICIPIO.CL_MUNICIPIO, V_C_MUNICIPIO.NB_MUNICIPIO, usuario, usuario, programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
                return Convert.ToInt32(pout_clave_retorno.Value); ;
            }
        }
                
        public int Elimina_C_MUNICIPIO(int? ID_MUNICIPIO = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_C_MUNICIPIO(pout_clave_retorno, ID_MUNICIPIO, usuario, programa);
                //regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
        

    }
}
