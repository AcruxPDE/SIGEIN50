using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.PuntoDeEncuentro
{
    public class DescriptivoPuestoOperaciones
    {
        private SistemaSigeinEntities context;


        public XElement ObtieneValidacionCambioDescriptivo(string PIN_ID_PUESTO, string PIN_ID_EMPLEADO, string CL_USUARIO = "", string NB_PROGRAMA = "")
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_OBTIENE_VALIDACION_CAMBIO_DESCRIPTIVO_PUESTO(pout_clave_retorno, PIN_ID_PUESTO, PIN_ID_EMPLEADO, CL_USUARIO, NB_PROGRAMA);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
    
        public XElement EliminaDescriptivoPuesto(string PIN_ID_PUESTO, string CL_USUARIO = "", string NB_PROGRAMA = "", string PIN_ID_EMPLEADO = null, int? ID_CAMBIO = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_M_PUESTO_PDE(pout_clave_retorno,  PIN_ID_PUESTO, CL_USUARIO, NB_PROGRAMA, PIN_ID_EMPLEADO, ID_CAMBIO);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement InsertaActualizaCopiaDescriptivo(string PIN_ID_PUESTO, string CL_USUARIO = "", string NB_PROGRAMA = "", string PIN_ID_EMPLEADO = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_COPIA_M_PUESTO_PDE(pout_clave_retorno, PIN_ID_PUESTO, CL_USUARIO, NB_PROGRAMA, PIN_ID_EMPLEADO);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        public XElement InsertaActualizaDescriptivo( string PIN_ID_PUESTO ="", string CL_USUARIO = "", string NB_PROGRAMA = "",
            int? ID_CAMBIO = null , bool FG_AUTORIZADO =true ,string DS_CAMBIO ="" , string PIN_CL_ESTADO ="")
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_PUESTO_PDE_A_PUESTO(pout_clave_retorno ,PIN_ID_PUESTO, CL_USUARIO, NB_PROGRAMA, 
                ID_CAMBIO ,FG_AUTORIZADO ,DS_CAMBIO,PIN_CL_ESTADO);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public SPE_OBTIENE_DESCRIPTIVO_PDE_Result ObtenerDescriptivo(string pIdDescriptivo, int? pIdCambio, string pIdeEmpleado)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_DESCRIPTIVO_PDE(pIdDescriptivo, pIdCambio, pIdeEmpleado).FirstOrDefault();
            }
        }

        public XElement ActualizaModificacionDescriptivo(int? ID_CAMBIO = null, bool FG_AUTORIZADO = false, string DS_CAMBIO = "", string ID_EMPLEADO = null, string CL_ESTADO = "", string TIPO_TRANSACCION = "", string CL_USUARIO_APP = "", string NB_PROGRAMA = "")
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_MODIFICACION_M_PUESTO_EMPLEADO(pout_clave_retorno, ID_CAMBIO, FG_AUTORIZADO, DS_CAMBIO, ID_EMPLEADO, CL_ESTADO,
             TIPO_TRANSACCION, CL_USUARIO_APP, NB_PROGRAMA);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        public XElement FinalizaModificacionDescriptivo(string PIN_ID_PUESTO, string CL_USUARIO = "", string NB_PROGRAMA = "", string PIN_ID_EMPLEADO = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_FINALIZA_MODIFICACION_PUESTO_PDE(pout_clave_retorno, PIN_ID_PUESTO, CL_USUARIO, NB_PROGRAMA, PIN_ID_EMPLEADO);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }


        public XElement InsertaActualizaDescriptivo(string  PIN_ID_PUESTO, int? ID_CAMBIO = null, string CL_USUARIO = "", string NB_PROGRAMA = "", string tipo_transaccion="", E_PUESTO V_M_PUESTO = null)
        {

            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));

                context.SPE_INSERTA_ACTUALIZA_M_PUESTO_PDE(pout_clave_retorno, PIN_ID_PUESTO, ID_CAMBIO, CL_USUARIO, NB_PROGRAMA, tipo_transaccion, V_M_PUESTO.XML_PUESTO);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pout_clave_retorno.Value.ToString());

            }

        }

    }
}
