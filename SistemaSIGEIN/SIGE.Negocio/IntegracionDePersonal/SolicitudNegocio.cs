using System;
using System.Collections.Generic;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;
using System.Xml.Linq;
using System.Linq;
using System.Web.Security;


namespace SIGE.Negocio.Administracion
{
    public class SolicitudNegocio
    {
    
        public List<SPE_OBTIENE_K_SOLICITUD_Result> ObtieneSolicitudes(int? ID_SOLICITUD = null, int? ID_CANDIDATO = null, int? ID_EMPLEADO = null, int? ID_DESCRIPTIVO = null, int? ID_REQUISICION = null, String CL_SOLICITUD = null, String CL_ACCESO_EVALUACION = null, int? ID_PLANTILLA_SOLICITUD = null, String DS_COMPETENCIAS_ADICIONALES = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            SolicitudOperaciones oSolicitud = new SolicitudOperaciones();
            return oSolicitud.ObtenerSolicitudes(ID_SOLICITUD, ID_CANDIDATO, ID_EMPLEADO, ID_DESCRIPTIVO, ID_REQUISICION, CL_SOLICITUD, CL_ACCESO_EVALUACION, ID_PLANTILLA_SOLICITUD, DS_COMPETENCIAS_ADICIONALES, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
        }
       
        public E_RESULTADO InsertaActualizaSolicitud(XElement pXmlSolicitud, int? pIdSolicitud, List<UDTT_ARCHIVO> pLstArchivoTemporales, List<E_DOCUMENTO> pLstDocumentos, string pClUsuario, string pNbPrograma, string vTipoSolicitud)
        {
            SolicitudOperaciones oSolicitud = new SolicitudOperaciones();
            return UtilRespuesta.EnvioRespuesta(oSolicitud.InsertarActualizarSolicitud(pXmlSolicitud, pIdSolicitud, pLstArchivoTemporales, pLstDocumentos, pClUsuario, pNbPrograma, vTipoSolicitud));
        }

        public E_RESULTADO ActualizaDatosSolicitudCorreo(int? pIdSolicitud,Guid? tokenCartera,string passCartera, string pClUsuario, string pNbPrograma)
        {
            SolicitudOperaciones oSolicitud = new SolicitudOperaciones();
            return UtilRespuesta.EnvioRespuesta(oSolicitud.ActualizaDatosSolicitudCorreo(pIdSolicitud,tokenCartera,passCartera, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO Elimina_K_SOLICITUD(int? ID_SOLICITUD = null, string usuario = null, string programa = null)
        {
            SolicitudOperaciones operaciones = new SolicitudOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_K_SOLICITUD(ID_SOLICITUD, usuario, programa));
        }

        public E_RESULTADO Elimina_K_SOLICITUDES(XElement listSolicitudes, string usuario = null, string programa = null)
        {
            SolicitudOperaciones operaciones = new SolicitudOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_K_SOLICITUDES(listSolicitudes, usuario, programa));
        }
        
        public List<SPE_OBTIENE_SOLICITUDES_Result> ObtieneCatalogoSolicitudes(string pXmlSeleccion = null)
        {
            SolicitudOperaciones operaciones = new SolicitudOperaciones();
            return operaciones.ObtenerCatalogoSolicitudes(pXmlSeleccion);
        }

        public List<SPE_OBTIENE_CARTERA_Result> Obtener_SOLICITUDES_CARTERA(int? ID_SOLICITUD = null, int? ID_CANDIDATO = null, int? ID_EMPLEADO = null, int? ID_DESCRIPTIVO = null, int? ID_REQUISICION = null, String CL_SOLICITUD = null, String CL_ACCESO_EVALUACION = null, int? ID_PLANTILLA_SOLICITUD = null, String DS_COMPETENCIAS_ADICIONALES = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            SolicitudOperaciones operaciones = new SolicitudOperaciones();
            return operaciones.Obtener_SOLICITUDES_CARTERA();
        }

        public List<SPE_OBTIENE_CARTERA_A_ELIMINAR_Result> Obtener_SOLICITUDES_CARTERA_A_ELIMINAR(int? ID_SOLICITUD = null, int? ID_CANDIDATO = null, int? ID_EMPLEADO = null, int? ID_DESCRIPTIVO = null, int? ID_REQUISICION = null, String CL_SOLICITUD = null, String CL_ACCESO_EVALUACION = null, int? ID_PLANTILLA_SOLICITUD = null, String DS_COMPETENCIAS_ADICIONALES = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            SolicitudOperaciones operaciones = new SolicitudOperaciones();
            return operaciones.Obtener_SOLICITUDES_CARTERA_A_ELIMINAR();
        }

        public SPE_OBTIENE_SOLICITUD_PLANTILLA_Result ObtenerSolicitudPlantilla(int? pIdPlantilla, int? pIdSolicitud, Guid? pFlPlantillaSolicitud)
        {
            SolicitudOperaciones oSolicitud = new SolicitudOperaciones();
            SPE_OBTIENE_SOLICITUD_PLANTILLA_Result vSolicitudPlantilla = oSolicitud.ObtenerSolicitudPlantilla(pIdPlantilla, pIdSolicitud,pFlPlantillaSolicitud);

            XElement vSolicitud = XElement.Parse(vSolicitudPlantilla.XML_SOLICITUD_PLANTILLA);
            XElement vValores = XElement.Parse(vSolicitudPlantilla.XML_VALORES);

            foreach (XElement vXmlContenedor in vSolicitud.Element("CONTENEDORES").Elements("CONTENEDOR"))
                foreach (XElement vXmlCampo in vXmlContenedor.Elements("CAMPO"))
                    UtilXML.AsignarValorCampo(vXmlCampo, vValores);

            vSolicitudPlantilla.XML_SOLICITUD_PLANTILLA = vSolicitud.ToString();

            return vSolicitudPlantilla;
        }

        protected void AsignarValorAtributo(XElement pXmlNode, string pNbAttribute, string pNbValor)
        {
            XAttribute vXmlAttribute = pXmlNode.Attribute(pNbAttribute);
            if (vXmlAttribute == null)
            {
                vXmlAttribute = new XAttribute(pNbAttribute, pNbValor ?? String.Empty);
                pXmlNode.Add(vXmlAttribute);
            }
            else
            {
                vXmlAttribute.Value = pNbValor ?? String.Empty;
            }
        }

        public E_RESULTADO InsertaCandidatoContratado(string pXmlDatosCandidato, string pClUsuario, string pNbPrograma)
        {
            SolicitudOperaciones oSolicitud = new SolicitudOperaciones();
            return UtilRespuesta.EnvioRespuesta(oSolicitud.InsertarCandidatoContratado(pXmlDatosCandidato, pClUsuario, pNbPrograma));
        }

        public string GenerarContrasenaCartera()
        {
            return Membership.GeneratePassword(12, 1);
        }

        public List<SPE_OBTIENE_SOLICITUDES_EMPLEO_Result> ObtieneSolicitudesEmpleo()
        {
            SolicitudOperaciones operaciones = new SolicitudOperaciones();
            return operaciones.ObtenerSolicitudesEmpleo();
        }
        
        public List<SPE_OBTIENE_DATOS_EMPLEADOS_Result> ObtieneDatosEmpleados()
        {
            SolicitudOperaciones operaciones = new SolicitudOperaciones();
            return operaciones.ObtenerDatosEmpleados();
        }

        public List<SPE_OBTIENE_ENTREVISTAS_SELECCIONADOS_Result> ObtieneEntrevistasSeleccionados()
        {
            SolicitudOperaciones operaciones = new SolicitudOperaciones();
            return operaciones.ObtenerEntrevistasSeleccionados();
        }
        

    }
}
