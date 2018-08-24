using SIGE.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.PuntoDeEncuentro
{
     public class InventarioPersonalOperaciones
    {
        private SistemaSigeinEntities context;

        public List<SPE_OBTIENE_CAMPOS_INVENTARIO_PERSONAL_Result> obtieneCamposInventarioPersonal()
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from a in context.SPE_OBTIENE_CAMPOS_INVENTARIO_PERSONAL()
                        select a;
                return q.ToList();
            }
        }

        public XElement Actualiza_Config_Campos_Inventario(string tipo_transaccion, SPE_OBTIENE_CAMPOS_INVENTARIO_PERSONAL_Result V_C_CAMPOS_INVENTARIO, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_PDE_ACTUALIZA_CAMPOS_INVENTARIO(pout_clave_retorno, V_C_CAMPOS_INVENTARIO.ID_CAMPO, V_C_CAMPOS_INVENTARIO.NB_CAMPO, V_C_CAMPOS_INVENTARIO.DS_TOOLTIP,V_C_CAMPOS_INVENTARIO.DS_CONFIGURACION,V_C_CAMPOS_INVENTARIO.FG_CONFIGURACION,V_C_CAMPOS_INVENTARIO.FG_VISIBLE, usuario,programa, tipo_transaccion);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        public XElement ActualizaModificacionEmpleado(int? ID_CAMBIO = null,  bool FG_AUTORIZADO=false , string DS_CAMBIO ="", string  ID_EMPLEADO = null , string CL_ESTADO="" , string  TIPO_TRANSACCION="" , string CL_USUARIO_APP ="", string  NB_PROGRAMA = "")
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_MODIFICACION_INFORMACION_EMPLEADO(pout_clave_retorno, ID_CAMBIO, FG_AUTORIZADO, DS_CAMBIO,ID_EMPLEADO, CL_ESTADO,
             TIPO_TRANSACCION, CL_USUARIO_APP , NB_PROGRAMA);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement Actualiza_Informacion_Original_de_Empleado( string PIN_ID_EMPLEADO , string CL_USUARIO="" ,string NB_PROGRAMA =""	  , int? ID_CAMBIO = null   
	    , bool FG_AUTORIZADO =true ,string DS_CAMBIO ="" , string PIN_CL_ESTADO ="")
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_EMPLEADO_PDE_A_EMPLEADO(pout_clave_retorno,  PIN_ID_EMPLEADO ,  CL_USUARIO , NB_PROGRAMA, ID_CAMBIO
	        ,  FG_AUTORIZADO ,	DS_CAMBIO  ,  PIN_CL_ESTADO);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
    }
}
