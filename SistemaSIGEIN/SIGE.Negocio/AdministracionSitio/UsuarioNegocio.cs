using System;
using System.Collections.Generic;
using System.Linq;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades.Administracion;
using System.Xml.Linq;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;

namespace SIGE.Negocio.Administracion
{
    public class UsuarioNegocio
    {

       
        public List<SPE_OBTIENE_C_USUARIO_Result> ObtieneUsuario(String CL_USUARIO = null, String NB_USUARIO = null, String NB_CORREO_ELECTRONICO = null, String NB_PASSWORD = null, bool? FG_CAMBIAR_PASSWORD = null, String XML_PERSONALIZACION = null, int? ID_ROL = null, int? ID_EMPLEADO = null, bool? FG_ACTIVO = null, DateTime? FE_INACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            UsuarioOperaciones operaciones = new UsuarioOperaciones();
            return operaciones.Obtener_C_USUARIO(CL_USUARIO, NB_USUARIO, NB_CORREO_ELECTRONICO, NB_PASSWORD, FG_CAMBIAR_PASSWORD, XML_PERSONALIZACION, ID_ROL, ID_EMPLEADO, FG_ACTIVO, FE_INACTIVO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
        }

        public List<SPE_OBTIENE_USUARIOS_Result> ObtieneUsuarios(string pClUsuario)
        {
            UsuarioOperaciones oUsuario = new UsuarioOperaciones();
            return oUsuario.ObtenerUsuarios(pClUsuario);
        }
        public List<SPE_OBTIENE_USUARIOS_NOLIGADOS_Result> ObtieneUsuariosNoLigados(string pClUsuario)
        {
            UsuarioOperaciones oUsuario = new UsuarioOperaciones();
            return oUsuario.ObtenerUsuariosNoLigado(pClUsuario);
        }
        public List<SPE_OBTIENE_USUARIOS_EMPLEADOS_Result> ObtieneUsuariosActivos(string pClUsuario, bool vLigado)
        {
            UsuarioOperaciones oUsuario = new UsuarioOperaciones();
            return oUsuario.ObtenerUsuariosActivos(pClUsuario, vLigado );
        }

        public E_USUARIO ObtieneUsuario(string pClUsuario)
        {
            UsuarioOperaciones oUsuario = new UsuarioOperaciones();
            SPE_OBTIENE_USUARIO_Result vUsuario = oUsuario.ObtenerUsuario(pClUsuario);

            return new E_USUARIO
            {
                CL_USUARIO = vUsuario.CL_USUARIO,
                FG_ACTIVO = vUsuario.FG_ACTIVO,
                NB_CORREO_ELECTRONICO = vUsuario.NB_CORREO_ELECTRONICO,
                NB_USUARIO = vUsuario.NB_USUARIO,
                ID_EMPLEADO_PDE = vUsuario.ID_EMPLEADO_PDE,
                XML_CATALOGOS = XElement.Parse(vUsuario.XML_CATALOGOS),
                
            };
        }

        public E_USUARIO ObtieneUsuarioCambioPassword(string pClUsuario)
        {
            UsuarioOperaciones oUsuario = new UsuarioOperaciones();
            E_USUARIO vUsuario =  oUsuario.ObtieneUsuarioCambioPassword(pClUsuario);

            return new E_USUARIO
            {
                CL_USUARIO = vUsuario.CL_USUARIO,
                FG_ACTIVO = vUsuario.FG_ACTIVO,
                NB_CORREO_ELECTRONICO = vUsuario.NB_CORREO_ELECTRONICO,
                NB_USUARIO = vUsuario.NB_USUARIO,
                ID_EMPLEADO_PDE = vUsuario.ID_EMPLEADO_PDE,
                XML_CATALOGOS = vUsuario.XML_CATALOGOS,
                FG_CAMBIAR_PASSWORD = vUsuario.FG_CAMBIAR_PASSWORD,

            };
        }

        public E_USUARIO ObtieneUsuarioPde(string pClUsuario)
        {
            UsuarioOperaciones oUsuario = new UsuarioOperaciones();
            SPE_OBTIENE_USUARIOPDE_Result vUsuario = oUsuario.ObtenerUsuarioPde(pClUsuario);

            return new E_USUARIO
            {
                CL_USUARIO = vUsuario.CL_USUARIO,
                FG_ACTIVO = vUsuario.FG_ACTIVO,
                NB_CORREO_ELECTRONICO = vUsuario.NB_CORREO_ELECTRONICO,
                NB_USUARIO = vUsuario.NB_USUARIO,
                ID_EMPLEADO_PDE = vUsuario.ID_EMPLEADO_PDE,
                XML_CATALOGOS = XElement.Parse(vUsuario.XML_CATALOGOS),
                CONTRASENA = vUsuario.CONTRASENA

            };
        }
        public E_USUARIO ObtieneUsuarioNuevo(string pClUsuario)
        {
            UsuarioOperaciones oUsuario = new UsuarioOperaciones();
            SPE_OBTIENE_USUARIO_NUEVO_Result  vUsuario = oUsuario.ObtenerUsuarioNuevo(pClUsuario);

            return new E_USUARIO
            {
                CL_USUARIO = vUsuario.CL_USUARIO,
                NB_CORREO_ELECTRONICO = vUsuario.NB_CORREO_ELECTRONICO,
                NB_USUARIO = vUsuario.NB_USUARIO,
                ID_EMPLEADO_PDE =vUsuario.ID_EMPLEADO_PDE,
                CONTRASENA   = vUsuario.CONTRASENA

            };
        }


        public List<SPE_OBTIENE_PDE_CONFIGURACION_URL_Result> ObtenerConfiguracionPDE()
        {
            UsuarioOperaciones operaciones = new UsuarioOperaciones();
            return operaciones.ObtieneConfiguracionPDE();
        }
             

        public E_USUARIO AutenticaUsuario(string pClUsuario, string pClPassword)
        {
            E_USUARIO vUsuario = new E_USUARIO()
            {
                CL_USUARIO = pClUsuario,
                NB_PASSWORD = pClPassword
            };

            UsuarioOperaciones oUsuario = new UsuarioOperaciones();
            SPE_OBTIENE_USUARIO_AUTENTICACION_Result vAutenticacion = oUsuario.AutenticaUsuario(vUsuario);

            if (vAutenticacion != null && vUsuario.ValidarToken(vAutenticacion.CL_AUTENTICACION) && PasswordHash.PasswordHash.ValidatePassword(pClPassword, vAutenticacion.NB_PASSWORD))
            {
                vUsuario.NB_CORREO_ELECTRONICO = vAutenticacion.NB_CORREO_ELECTRONICO;
                vUsuario.NB_USUARIO = vAutenticacion.NB_USUARIO;
                vUsuario.FG_ACTIVO = vAutenticacion.FG_ACTIVO;
                vUsuario.ID_EMPLEADO = vAutenticacion.ID_EMPLEADO;
                vUsuario.ID_EMPLEADO_PDE = vAutenticacion.ID_EMPLEADO.ToString();
                vUsuario.ID_PUESTO = vAutenticacion.ID_PUESTO;
                vUsuario.ID_PUESTO_PDE = vAutenticacion.ID_PUESTO.ToString();
                vUsuario.oRol = new E_ROL() { ID_ROL = vAutenticacion.ID_ROL, NB_ROL = vAutenticacion.NB_ROL };
                vUsuario.ID_PLANTILLA = vAutenticacion.ID_PLANTILLA; // SE AGREGA EL ID DE LA PLANTILLA LIGADA AL ROL
                vUsuario.ID_EMPRESA = vAutenticacion.ID_EMPRESA;
               // vUsuario.FG_CAMBIAR_PASSWORD = 
                if (vAutenticacion.XML_DATA!=null)
                    vUsuario.oFunciones = XElement.Parse(vAutenticacion.XML_DATA).Elements("FUNCION").Select(f => new E_FUNCION()
                    {
                        CL_FUNCION = f.Attribute("CL_FUNCION").Value,
                        CL_TIPO_FUNCION = f.Attribute("CL_TIPO_FUNCION").Value,
                        ID_FUNCION = (int)UtilXML.ValorAtributo(f.Attribute("ID_FUNCION"), E_TIPO_DATO.INT),
                        ID_FUNCION_PADRE = (int?)UtilXML.ValorAtributo(f.Attribute("ID_FUNCION_PADRE"), E_TIPO_DATO.INT),
                        NB_FUNCION = f.Attribute("NB_FUNCION").Value,
                        DS_FUNCION = f.Attribute("DS_FUNCION").Value,
                        NB_URL = f.Attribute("NB_URL").Value,
                        XML_CONFIGURACION = f.Element("XML_CONFIGURACION").ToString()
                    }).OrderBy(o => o.NO_ORDEN).ToList();
            }
            return vUsuario;
        }

        public E_RESULTADO CambiaPassword(E_USUARIO pUsuario, string pClUsuario, string pNbPrograma)
        {
            UsuarioOperaciones oUsuario = new UsuarioOperaciones();

            XElement vRespuestaXML = oUsuario.CambiarPassword(pUsuario, pClUsuario, pNbPrograma);

            E_RESULTADO vResultado = new E_RESULTADO(vRespuestaXML);

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
            {
                XElement vDatosRespuesta = vResultado.ObtieneDatosRespuesta();

                string vNbCorreoElectronico = vDatosRespuesta.Element("USUARIO").Attribute("NB_CORREO_ELECTRONICO").Value;
                string vNbUsuario = vDatosRespuesta.Element("USUARIO").Attribute("NB_USUARIO").Value;
                string vClEstadoRecuperacion = vDatosRespuesta.Element("USUARIO").Attribute("CL_ESTADO_RECUPERACION").Value;

                Mail mail = new Mail(ContextoApp.mailConfiguration);
                mail.addToAddress(vNbCorreoElectronico, vNbUsuario);

                switch (vClEstadoRecuperacion)
                {
                    case "CHANGING":
                        mail.Send("Cambio de contraseña", String.Format("Estimado {1},<br/><br/>Para realizar el cambio de la contraseña ingrese el siguiente código durante el proceso de ingreso al sistema: {0}<br/><br/>Este código caduca en 1 hora.<br/><br/>Saludos cordiales.", pUsuario.CL_CAMBIAR_PASSWORD, vNbUsuario));
                        break;
                    case "CHANGED":
                        mail.Send("Contraseña cambiada", String.Format("Estimado {0},<br/><br/>Se ha realizado el cambio de la contraseña.<br/><br/>Saludos cordiales.", vNbUsuario));
                        break;
                }
            }

            return vResultado;
        }
            
        public int InsertaActualiza_C_USUARIO(string tipo_transaccion, SPE_OBTIENE_C_USUARIO_Result V_C_USUARIO, string usuario, string programa)
        {
            UsuarioOperaciones operaciones = new UsuarioOperaciones();
            return operaciones.InsertaActualiza_C_USUARIO(tipo_transaccion, V_C_USUARIO, usuario, programa);
        }

        public E_RESULTADO InsertaActualizaUsuario(E_TIPO_OPERACION_DB pClTipoOperacion, E_USUARIO pUsuario, string pClUsuario, string pNbPrograma)
        {
            UsuarioOperaciones oUsuario = new UsuarioOperaciones();
            return new E_RESULTADO(oUsuario.InsertarActualizarUsuario(pClTipoOperacion, pUsuario, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO ActualizaUsuarioPDE(E_TIPO_OPERACION_DB pClTipoOperacion, E_USUARIO pUsuario, string pClUsuario, string pNbPrograma)
        {
            UsuarioOperaciones oUsuario = new UsuarioOperaciones();
            return new E_RESULTADO(oUsuario.ActualizarUsuarioPDE(pClTipoOperacion, pUsuario, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO InsertaActualizaUsuario_pde(E_TIPO_OPERACION_DB pClTipoOperacion, E_USUARIO pUsuario, string pClUsuario, string pNbPrograma)
        {
            UsuarioOperaciones oUsuario = new UsuarioOperaciones();
            return new E_RESULTADO(oUsuario.InsertarActualizarUsuario_pde(pClTipoOperacion, pUsuario, pClUsuario, pNbPrograma));
        }

        public int Elimina_C_USUARIO(String CL_USUARIO = null, string usuario = null, string programa = null)
        {
            UsuarioOperaciones operaciones = new UsuarioOperaciones();
            return operaciones.Elimina_C_USUARIO(CL_USUARIO, usuario, programa);
        }

        public E_RESULTADO EliminaUsuario(string pClUser, string pClUsuario, string pNbPrograma)
        {
            UsuarioOperaciones oUsuario = new UsuarioOperaciones();
            return UtilRespuesta.EnvioRespuesta(oUsuario.EliminarUsuario(pClUser, pClUsuario, pNbPrograma));
        }
        public E_RESULTADO InsertarUsuariosPdeMasivo(string XMLEMPLEADOS, string pClUsuario, string pNbPrograma)
        {
            UsuarioOperaciones oUsuario = new UsuarioOperaciones();
            return new E_RESULTADO(oUsuario.InsertarUsuariosPdeMasivo(XMLEMPLEADOS, pClUsuario, pNbPrograma));
        }
        public E_RESULTADO InsertarUsuarioCorreo(string XMLUSUARIOS, string pClUsuario, string pNbPrograma, string transaccion)
        {
            UsuarioOperaciones oUsuario = new UsuarioOperaciones();
            return new E_RESULTADO(oUsuario.InsertarUsuarioCorreo(XMLUSUARIOS, pClUsuario, pNbPrograma, transaccion ));
        }
        
    }
}