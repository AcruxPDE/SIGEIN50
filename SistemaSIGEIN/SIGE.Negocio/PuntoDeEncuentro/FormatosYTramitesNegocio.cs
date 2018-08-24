using SIGE.AccesoDatos.Implementaciones.PuntoDeEncuentro;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.Negocio.PuntoDeEncuentro
{
    public class FormatosYTramitesNegocio
    {
        public E_RESULTADO INSERTA_ACTUALIZA_FORMATOS_Y_TRAMITES(int? id_Documento = null, string nombre = "", string xmlDocumento = "",string descripcion="", string tipoDocumento = "", bool estatus = true, string usuario = "", string programa = "", string tipo_transaccion = "")
        {
            FormatosYTramitesOperaciones operaciones = new FormatosYTramitesOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.INSERTA_ACTUALIZA_FORMATOS_Y_TRAMITES(id_Documento, nombre, xmlDocumento, descripcion, tipoDocumento, estatus, usuario, programa, tipo_transaccion));
        }
        public List<SPE_OBTIENE_FORMATOS_Y_TRAMITES_Result> OBTENER_FORMATOS_Y_TRAMITES(int? id_documento =null, string TipoDocumento = null, bool documentoActivo = true)
        {
            FormatosYTramitesOperaciones operaciones = new FormatosYTramitesOperaciones();
            return operaciones.OBTENER_FORMATOS_Y_TRAMITES(id_documento,TipoDocumento, documentoActivo).ToList();
        }


        public E_RESULTADO ELIMINA_FORMATOS_Y_TRAMITES(int? id_Documento = null, string tipoDocumento = "", bool estatus = true, string usuario = "", string programa = "", string tipo_transaccion = "")
        {
            FormatosYTramitesOperaciones operaciones = new FormatosYTramitesOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.ELIMINA_FORMATOS_Y_TRAMITES(id_Documento, tipoDocumento, estatus, usuario, programa, tipo_transaccion));
        }
        public E_RESULTADO INSERTA_COPIA_FORMATOS_Y_TRAMITES(int? id_Documento = null, string descripcion = "", string tipoDocumento = "", bool estatus = true, string usuario = "", string programa = "", string tipo_transaccion = "")
        {
            FormatosYTramitesOperaciones operaciones = new FormatosYTramitesOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.INSERTA_COPIA_FORMATOS_Y_TRAMITES(id_Documento,  descripcion, tipoDocumento, estatus, usuario, programa, tipo_transaccion));
        }
        public SPE_OBTIENE_EMPLEADO_FORMATO_TRAMITE_Result ObtenerPlantilla(int? pIdPlantilla, string  pIdEmpleado, string version = "")
        {
            FormatosYTramitesOperaciones oEmpleado = new FormatosYTramitesOperaciones();
            SPE_OBTIENE_EMPLEADO_FORMATO_TRAMITE_Result vEmpleadoPlantilla = oEmpleado.ObtenerPlantilla(pIdPlantilla, pIdEmpleado, version);

            XElement vEmpleado = XElement.Parse(vEmpleadoPlantilla.XML_SOLICITUD_PLANTILLA);
            XElement vValores = XElement.Parse(vEmpleadoPlantilla.XML_VALORES);

            foreach (XElement vXmlContenedor in vEmpleado.Element("CONTENEDORES").Elements("CONTENEDOR"))
                foreach (XElement vXmlCampo in vXmlContenedor.Elements("CAMPO"))
                    UtilXML.AsignarValorCampo(vXmlCampo, vValores);

            vEmpleadoPlantilla.XML_SOLICITUD_PLANTILLA = vEmpleado.ToString();

            return vEmpleadoPlantilla;
        }

    }
}
