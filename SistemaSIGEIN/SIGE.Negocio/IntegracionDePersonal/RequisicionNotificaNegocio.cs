using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Negocio.IntegracionDePersonal
{
   public class RequisicionNotificaNegocio
    {
       public E_RESULTADO InsertaActualizaNotificacion(int ID_NOTIFICACION, string NB_PUESTO, int ID_DEPARTAMENTO, Decimal MN_SUELDO, DateTime FE_SOLICITUD, string DS_JUSTIFICACION, string DS_ACTIVIDADES, string CL_GENERO, int ID_NIVEL_ESCOLARIDAD, Byte NO_EDAD_MINIMA, Byte NO_EDAD_MAXIMA, string DS_EXPERIENCIA_LABORAL, string DS_CUALIDADES, string CL_TOKEN, Guid FL_NOTIFICACION, string CL_ESTATUS, string DS_COMENTARIOS, string pCLusuario, string pNBprograma, string TIPO_TRANSACCION)
       {
           RequisicionNotificaOperacion operaciones = new RequisicionNotificaOperacion();
           return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualizaNotificacion(ID_NOTIFICACION, NB_PUESTO, ID_DEPARTAMENTO, MN_SUELDO, FE_SOLICITUD, DS_JUSTIFICACION, DS_ACTIVIDADES, CL_GENERO, ID_NIVEL_ESCOLARIDAD, NO_EDAD_MINIMA, NO_EDAD_MAXIMA, DS_EXPERIENCIA_LABORAL, DS_CUALIDADES, CL_TOKEN, FL_NOTIFICACION, CL_ESTATUS, DS_COMENTARIOS, pCLusuario, pNBprograma, TIPO_TRANSACCION));

       }
       public List<SPE_OBTIENE_NOTIFICACION_REQUISICION_Result> ObtieneNotificacion(int? pIdNotificacion = null, Guid? pflNotificacion = null, int? pIdRequisicion = null)
       {
           RequisicionNotificaOperacion oNotificacion = new RequisicionNotificaOperacion();
           return oNotificacion.ObtenerNotificacion(pIdNotificacion, pflNotificacion, pIdRequisicion);
       }
       public string ObtienerExistePuesto(string cl_puesto)
       {
           RequisicionNotificaOperacion operaciones = new RequisicionNotificaOperacion();
           return operaciones.ObtieneExistePuesto(cl_puesto);

       }
       //public E_RESULTADO CreaRechazaPuesto(int? ID_NOTIFICACION, Guid? FL_NOTIFICACION, int? ID_REQUISICION, string TIPO_TRANSACCION, string USUARIO, string CL_PUESTO, string DS_COMENTARIO)
       //{
       //    RequisicionNotificaOperacion operaciones = new RequisicionNotificaOperacion();
       //    return UtilRespuesta.EnvioRespuesta(operaciones.CreaRechazaPuestoRequisicion(ID_NOTIFICACION, FL_NOTIFICACION, ID_REQUISICION, TIPO_TRANSACCION, USUARIO, CL_PUESTO, DS_COMENTARIO));

       //}
       
       public E_RESULTADO EliminaNotificacion(int? pIdNotificacion = null)
       {
           RequisicionNotificaOperacion oRequisicion = new RequisicionNotificaOperacion();
           return UtilRespuesta.EnvioRespuesta(oRequisicion.EliminaNotificacion(pIdNotificacion));
       }
       

    }
}
