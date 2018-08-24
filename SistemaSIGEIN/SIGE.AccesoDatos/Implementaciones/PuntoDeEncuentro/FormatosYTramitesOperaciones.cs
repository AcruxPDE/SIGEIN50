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
   public  class FormatosYTramitesOperaciones
   {
       private SistemaSigeinEntities context;

       public XElement INSERTA_ACTUALIZA_FORMATOS_Y_TRAMITES(int? id_Documento = null, string nombre = "", string xmlDocumento = "",string descripcion ="", string tipoDocumento = "", bool estatus = true, string usuario = "", string programa = "", string tipo_transaccion = "")
       {
           using (context = new SistemaSigeinEntities())
           {
               ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
               context.SPE_INSERTA_ACTUALIZA_FORMATOS_Y_TRAMITES(pout_clave_retorno, id_Documento, nombre, xmlDocumento, descripcion,tipoDocumento, estatus, usuario, programa, tipo_transaccion);// V_C_DS_NOTIFICACION.ID_CONFIGURACION_NOTIFICACION
               return XElement.Parse(pout_clave_retorno.Value.ToString());
           }
       }

       public List<SPE_OBTIENE_FORMATOS_Y_TRAMITES_Result> OBTENER_FORMATOS_Y_TRAMITES(int? id_documento= null, string TipoDocumento = null, bool documentoActivo = true)
       {
           using (context = new SistemaSigeinEntities())
           {
               return context.SPE_OBTIENE_FORMATOS_Y_TRAMITES(id_documento, TipoDocumento, documentoActivo).ToList();
           }
       }

       public XElement ELIMINA_FORMATOS_Y_TRAMITES(int? id_Documento = null,  string tipoDocumento = "", bool estatus = true, string usuario = "", string programa = "", string tipo_transaccion = "")
       {
           using (context = new SistemaSigeinEntities())
           {
               ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
               context.SPE_ELIMINA_FORMATOS_Y_TRAMITES(pout_clave_retorno, id_Documento, tipoDocumento, estatus, usuario, programa, tipo_transaccion);// V_C_DS_NOTIFICACION.ID_CONFIGURACION_NOTIFICACION
               return XElement.Parse(pout_clave_retorno.Value.ToString());
           }
       }
       public XElement INSERTA_COPIA_FORMATOS_Y_TRAMITES(int? id_Documento = null, string descripcion = "", string tipoDocumento = "", bool estatus = true, string usuario = "", string programa = "", string tipo_transaccion = "")
       {
           using (context = new SistemaSigeinEntities())
           {
               ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
               context.SPE_INSERTA_COPIA_FORMATOS_Y_TRAMITES(pout_clave_retorno, id_Documento, descripcion, tipoDocumento, estatus, usuario, programa, tipo_transaccion);// V_C_DS_NOTIFICACION.ID_CONFIGURACION_NOTIFICACION
               return XElement.Parse(pout_clave_retorno.Value.ToString());
           }
       }
       public SPE_OBTIENE_EMPLEADO_FORMATO_TRAMITE_Result ObtenerPlantilla(int? pIdPlantilla, string  pIdEmpleado, string vesrsion = "")
       {
           using (context = new SistemaSigeinEntities())
           {
               return context.SPE_OBTIENE_EMPLEADO_FORMATO_TRAMITE(pIdPlantilla, pIdEmpleado, vesrsion).FirstOrDefault();
           }
       }

    }
}
