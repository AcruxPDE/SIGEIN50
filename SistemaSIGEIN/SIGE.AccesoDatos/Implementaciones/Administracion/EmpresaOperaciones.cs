using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using System.Data.Objects;
using System.Xml.Linq;
using SIGE.Entidades.Administracion;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{

    public class EmpresaOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  C_EMPRESA
        public List<SPE_OBTIENE_C_EMPRESA_Result> Obtener_C_EMPRESA(int? ID_EMPRESA = null, String CL_EMPRESA = null, String NB_EMPRESA = null, String NB_RAZON_SOCIAL = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_C_EMPRESA in context.SPE_OBTIENE_C_EMPRESA(ID_EMPRESA, CL_EMPRESA, NB_EMPRESA, NB_RAZON_SOCIAL)
                        select V_C_EMPRESA;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_EMPRESA
        public XElement InsertaActualiza_C_EMPRESA(string tipo_transaccion, E_EMPRESAS V_C_EMPRESA, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
              //  pout_clave_retorno.Value = "";
                context.SPE_INSERTA_ACTUALIZA_C_EMPRESA(pout_clave_retorno, V_C_EMPRESA.ID_EMPRESA, V_C_EMPRESA.CL_EMPRESA, V_C_EMPRESA.NB_EMPRESA, V_C_EMPRESA.NB_RAZON_SOCIAL, usuario, usuario, programa, programa, tipo_transaccion);
              
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        #endregion

        #region ELIMINA DATOS  C_EMPRESA
        public XElement Elimina_C_EMPRESA(int? ID_EMPRESA = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
              //  pout_clave_retorno.Value = "";
                context.SPE_ELIMINA_C_EMPRESA(pout_clave_retorno, ID_EMPRESA, usuario, programa);
             			
                //regresamos el valor de retorno de sql				
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            
            }
        }
        #endregion

    }
}