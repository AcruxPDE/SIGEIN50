using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal; // reemplazar por la carpeta correspondiente
using SIGE.Entidades.Administracion;
using System.Xml.Linq;


namespace SIGE.Negocio.Administracion  // reemplazar por la carpeta correspondiente
{
    public class ConfiguracionNegocio
    {

        //#region OBTIENE DATOS  S_CONFIGURACION
        //public XElement Obtener_S_CONFIGURACION(String XML_CONFIGURACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_MODIFICA = null,String NB_PROGRAMA_MODIFICA = null)
        //{
        //    ConfiguracionOperaciones operaciones = new ConfiguracionOperaciones();
        //    return operaciones.ObtenerConfiguracion(XML_CONFIGURACION,FE_MODIFICACION,CL_USUARIO_MODIFICA,NB_PROGRAMA_MODIFICA);
        //}
        //#endregion

        //#region INSERTA ACTUALIZA DATOS  S_CONFIGURACION
        //public XElement InsertaActualiza_S_CONFIGURACION(string tipo_transaccion, XElement V_S_CONFIGURACION, string usuario, string programa)
        //{
        //    ConfiguracionOperaciones operaciones = new ConfiguracionOperaciones();
        //    return operaciones.InsertaActualizaConfiguracion(tipo_transaccion,V_S_CONFIGURACION, usuario,programa);
        //}	
        //#endregion
        
        public int Elimina_S_CONFIGURACION(string usuario = null, string programa = null)
        {
            ConfiguracionOperaciones operaciones = new ConfiguracionOperaciones();
            return operaciones.Elimina_S_CONFIGURACION(usuario, programa);
        }		
	}
}