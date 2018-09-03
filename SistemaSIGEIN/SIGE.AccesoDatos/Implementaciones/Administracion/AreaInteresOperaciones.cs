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
   
    public class AreaInteresOperaciones
    {

        private SistemaSigeinEntities context;		

        #region OBTIENE DATOS  C_AREA_INTERES
        public List<SPE_OBTIENE_C_AREA_INTERES_Result> Obtener_C_AREA_INTERES(int?  ID_AREA_INTERES = null,String CL_AREA_INTERES = null,String NB_AREA_INTERES = null,bool?  FG_ACTIVO = null,DateTime?  FE_CREACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_APP_CREA = null,String CL_USUARIO_APP_MODIFICA = null,String NB_PROGRAMA_CREA = null,String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities ())
            {
                var q = from V_C_AREA_INTERES in context.SPE_OBTIENE_C_AREA_INTERES(ID_AREA_INTERES,CL_AREA_INTERES,NB_AREA_INTERES,FG_ACTIVO)
                select V_C_AREA_INTERES;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_AREA_INTERES
        public XElement InsertaActualiza_C_AREA_INTERES(string tipo_transaccion, E_AREA_INTERES V_C_AREA_INTERES,string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities ())
            {
                //Declaramos el objeto de valor de retorno

                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
          
                context.SPE_INSERTA_ACTUALIZA_C_AREA_INTERES(pout_clave_retorno,  V_C_AREA_INTERES.ID_AREA_INTERES,V_C_AREA_INTERES.CL_AREA_INTERES,V_C_AREA_INTERES.NB_AREA_INTERES,V_C_AREA_INTERES.FG_ACTIVO,usuario,usuario,programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
                     //   return Convert.ToInt32(pout_clave_retorno.Value);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        #endregion

        #region ELIMINA DATOS  C_AREA_INTERES
        public XElement Elimina_C_AREA_INTERES(int? ID_AREA_INTERES = null, string CL_AREA_INTERES = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities ())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
              //  pout_clave_retorno.Value = "";
                context.SPE_ELIMINA_C_AREA_INTERES(pout_clave_retorno,ID_AREA_INTERES, CL_AREA_INTERES, usuario, programa);
           
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        #endregion

    }
}