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

    public class IdiomaOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  C_IDIOMA
        public List<SPE_OBTIENE_C_IDIOMA_Result> Obtener_C_IDIOMA(int? ID_IDIOMA = null, String CL_IDIOMA = null, String NB_IDIOMA = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_C_IDIOMA in context.SPE_OBTIENE_C_IDIOMA(ID_IDIOMA, CL_IDIOMA, NB_IDIOMA, FG_ACTIVO)
                        select V_C_IDIOMA;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_IDIOMA
        public XElement InsertaActualiza_C_IDIOMA(string tipo_transaccion, SPE_OBTIENE_C_IDIOMA_Result V_C_IDIOMA, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_C_IDIOMA(pout_clave_retorno, V_C_IDIOMA.ID_IDIOMA, V_C_IDIOMA.CL_IDIOMA, V_C_IDIOMA.NB_IDIOMA, V_C_IDIOMA.FG_ACTIVO, usuario, usuario, programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        #endregion

        #region ELIMINA DATOS  C_IDIOMA
        public XElement Elimina_C_IDIOMA(int? ID_IDIOMA = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_C_IDIOMA(pout_clave_retorno, ID_IDIOMA, usuario, programa);
                //regresamos el valor de retorno de sql				
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        #endregion

    }
}
