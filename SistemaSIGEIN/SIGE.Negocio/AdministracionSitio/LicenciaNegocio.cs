using System;
using System.Collections.Generic;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;
using System.Xml.Linq;


namespace SIGE.Negocio.Administracion
{
    public class LicenciaNegocio
    {
        public SPE_OBTIENE_S_CONFIGURACION_Result obtieneConfiguracionGeneral(String XML_CONFIGURACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_MODIFICA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            ConfiguracionOperaciones operaciones = new ConfiguracionOperaciones();
            return operaciones.ObtenerConfiguracion(XML_CONFIGURACION, FE_MODIFICACION, CL_USUARIO_MODIFICA, NB_PROGRAMA_MODIFICA);
        }

        public SPE_OBTIENE_CONFIGURACION_LICENCIA_Result obtieneConfiguracion(string CL_CONFIGURACION = null, string CL_USUARIO = null)
        {
            LicenciaOperaciones operaciones = new LicenciaOperaciones();
            return operaciones.obtieneConfiguracion(CL_CONFIGURACION, CL_USUARIO);
        }

        public List<SPE_OBTIENE_EMPLEADOS_Result> ObtenerEmpleados(XElement pXmlSeleccion = null, bool? pFgFoto = null, string pClUsuario = null, bool? pFgActivo = null, int? pID_EMPRESA = null)
        {
            EmpleadoOperaciones oEmpleados = new EmpleadoOperaciones();
            return oEmpleados.ObtenerEmpleados(pXmlSeleccion, pFgFoto, pClUsuario, pFgActivo, pID_EMPRESA);
        }

        public E_RESULTADO InsertaActualiza_S_CONFIGURACION(string CL_CONFIGURACION = null, string NO_CONFIGURACION = null, string CL_USUARIO = null, string NB_PROGRAMA = null, string TIPO_TRANSACCION = null)
        {
            LicenciaOperaciones operaciones = new LicenciaOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualiza_S_CONFIGURACION(CL_CONFIGURACION, NO_CONFIGURACION, CL_USUARIO, NB_PROGRAMA, TIPO_TRANSACCION));
        }
    }
}
