using SIGE.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal
{
    public class RequisicionNotificaOperacion
    {
        private SistemaSigeinEntities context;

        #region INSERTA ACTUALIZA NOTIFICACIÓN REQUISICIÓN

        public XElement InsertaActualizaNotificacion(int ID_NOTIFICACION, string NB_PUESTO, int ID_DEPARTAMENTO, Decimal MN_SUELDO, DateTime FE_SOLICITUD, string DS_JUSTIFICACION, string DS_ACTIVIDADES, string CL_GENERO, int ID_NIVEL_ESCOLARIDAD, Byte NO_EDAD_MINIMA, Byte NO_EDAD_MAXIMA, string DS_EXPERIENCIA_LABORAL, string DS_CUALIDADES, string CL_TOKEN, Guid FL_NOTIFICACION, string CL_ESTATUS, string DS_COMENTARIOS, string pCLusuario, string pNBprograma, string TIPO_TRANSACCION)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_NOTIFICACION_REQUISICION(pOutClRetorno, ID_NOTIFICACION, NB_PUESTO, ID_DEPARTAMENTO, MN_SUELDO, FE_SOLICITUD, DS_JUSTIFICACION, DS_ACTIVIDADES, CL_GENERO, ID_NIVEL_ESCOLARIDAD, NO_EDAD_MINIMA, NO_EDAD_MAXIMA, DS_EXPERIENCIA_LABORAL, DS_CUALIDADES, CL_TOKEN, FL_NOTIFICACION, CL_ESTATUS, DS_COMENTARIOS,  pCLusuario, pNBprograma, TIPO_TRANSACCION);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_NOTIFICACION_REQUISICION_Result> ObtenerNotificacion(int? pIdNotificacion = null, Guid? pflNotificacion = null, int? pIdRequisicion = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_NOTIFICACION_REQUISICION(pIdNotificacion, pflNotificacion, pIdRequisicion).ToList();
            }
        }
        public string ObtieneExistePuesto( string cl_puesto)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_PUESTO_EXISTE( cl_puesto).FirstOrDefault();
            }
        }
        
		
		
        public XElement CreaRechazaPuestoRequisicion(int? ID_NOTIFICACION, Guid? FL_NOTIFICACION, int? ID_REQUISICION, string   TIPO_TRANSACCION, string USUARIO, string CL_PUESTO, string DS_COMENTARIO)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                //context.SPE_CREA_RECHAZA_PUESTO_REQUISICION(pOutClRetorno,  ID_NOTIFICACION,  FL_NOTIFICACION,  ID_REQUISICION,   TIPO_TRANSACCION,  USUARIO,  CL_PUESTO,  DS_COMENTARIO,null,null);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }
        #endregion


        
        public XElement EliminaNotificacion(int? pIdNotificacion = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_K_NOTIFICACION(pout_clave_retorno, pIdNotificacion);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }        
    }
}
