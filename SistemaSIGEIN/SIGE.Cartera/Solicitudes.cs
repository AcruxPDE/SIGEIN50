using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.Cartera
{
     public class Solicitudes
    {
         public E_RESULTADO enviarEliminarSolicitudes()
        {
            E_RESULTADO respuesta = new E_RESULTADO();
            if (ContextoApp.IDP.MensajeActualizacionPeriodica.fgEstatus)
            {
                var lista = obtenerListaCandidatos();
                var respuestaCorreos = EnvioCorreoSolicitudes(lista);
                respuesta = eliminarSolicitud(lista);
            }
            return respuesta;
        }

        public List<E_CANDIDATO_SOLICITUD> obtenerListaCandidatos()
        {
            List<E_CANDIDATO_SOLICITUD> listaCandidatos = new List<E_CANDIDATO_SOLICITUD>();
            SolicitudNegocio nSolicitud = new SolicitudNegocio();
            var solicitudes = nSolicitud.Obtener_SOLICITUDES_CARTERA_A_ELIMINAR();
            listaCandidatos = (from c in solicitudes
                               select new E_CANDIDATO_SOLICITUD
                               {
                                   ID_SOLICITUD = c.ID_SOLICITUD,
                                   C_CANDIDATO_NB_EMPLEADO_COMPLETO = c.C_CANDIDATO_NB_EMPLEADO_COMPLETO,
                                   C_CANDIDATO_CL_CORREO_ELECTRONICO = c.C_CANDIDATO_CL_CORREO_ELECTRONICO,

                               }).ToList();
            return listaCandidatos;
        }

        public E_RESULTADO eliminarSolicitud(List<E_CANDIDATO_SOLICITUD> listaCandidatos)
        {
            XElement xmlElements = new XElement("CANDIDATOS", listaCandidatos.Select(i => new XElement("CANDIDATO", new XAttribute("ID_SOLICITUD", i.ID_SOLICITUD),
                                                                                          new XAttribute("C_CANDIDATO_NB_EMPLEADO_COMPLETO", i.C_CANDIDATO_NB_EMPLEADO_COMPLETO),
                                                                                          new XAttribute("C_CANDIDATO_CL_CORREO_ELECTRONICO", i.C_CANDIDATO_CL_CORREO_ELECTRONICO))));
            SolicitudNegocio nSolicitud = new SolicitudNegocio();
            return nSolicitud.Elimina_K_SOLICITUDES(xmlElements, "Proceso automatico", "Proceso automatico");
        }

        private string EnvioCorreoSolicitudes(List<E_CANDIDATO_SOLICITUD> listaCandidatos)
        {
            var respuesta = "0";
            foreach (E_CANDIDATO_SOLICITUD item in listaCandidatos)
            {
                Mail mail = new Mail(ContextoApp.mailConfiguration);
                var EnviaMail = false;
                if (!String.IsNullOrEmpty(item.C_CANDIDATO_CL_CORREO_ELECTRONICO) && item.C_CANDIDATO_CL_CORREO_ELECTRONICO != "&nbsp;" && ContextoApp.IDP.MensajeActualizacionPeriodica.fgVisible)
                {                    
                    mail.addToAddress(item.C_CANDIDATO_CL_CORREO_ELECTRONICO, null);
                    EnviaMail = true;
                }
                    //AGREGAR CORREOS DE RR.HH
                if (ContextoApp.IDP.MensajeActualizacionPeriodica.dsNotificacion.fgVisible)
                {
                    var direccionesRRHH = ContextoApp.IDP.MensajeActualizacionPeriodica.lstCorreos;
                    foreach (var dirrrhh in direccionesRRHH)
                    {
                        mail.addToAddress(dirrrhh.Address, dirrrhh.DisplayName);
                        EnviaMail = true;
                    }
                }
                if (EnviaMail)
                {
                    respuesta = mail.Send("Candidatos", String.Format("Estimado(a) {0},<br/><br/>{1}<br/><br/>Saludos cordiales.", item.C_CANDIDATO_NB_EMPLEADO_COMPLETO, ContextoApp.IDP.MensajeActualizacionPeriodica.dsNotificacion.dsMensaje));
                    if (respuesta != "0")
                    {
                        return respuesta;
                    }
                }                
            }
            return respuesta;
        }
    }
}
