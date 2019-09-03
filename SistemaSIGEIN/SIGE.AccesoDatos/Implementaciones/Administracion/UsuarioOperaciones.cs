using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades.Administracion;
using SIGE.Entidades;
using System.Data.Objects;
using System.Xml.Linq;
using SIGE.Entidades.Externas;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{

    public class UsuarioOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  C_USUARIO
        public List<SPE_OBTIENE_C_USUARIO_Result> Obtener_C_USUARIO(String CL_USUARIO = null, String NB_USUARIO = null, String NB_CORREO_ELECTRONICO = null, String NB_PASSWORD = null, bool? FG_CAMBIAR_PASSWORD = null, String XML_PERSONALIZACION = null, int? ID_ROL = null, int? ID_EMPLEADO = null, bool? FG_ACTIVO = null, DateTime? FE_INACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_C_USUARIO(CL_USUARIO, NB_USUARIO, NB_CORREO_ELECTRONICO, NB_PASSWORD, FG_CAMBIAR_PASSWORD, XML_PERSONALIZACION, ID_ROL, ID_EMPLEADO, FG_ACTIVO, FE_INACTIVO).ToList();
                       
            }
        }

        public List<SPE_OBTIENE_USUARIOS_Result> ObtenerUsuarios(string pClUsuario)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_USUARIOS(pClUsuario).ToList();
            }
        }
        public List<SPE_OBTIENE_USUARIOS_NOLIGADOS_Result> ObtenerUsuariosNoLigado(string pClUsuario)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_USUARIOS_NOLIGADOS(pClUsuario).ToList();
            }
        }
        public List<SPE_OBTIENE_USUARIOS_EMPLEADOS_Result > ObtenerUsuariosActivos(string pClUsuario, bool pLigado)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_USUARIOS_EMPLEADOS(pClUsuario, pLigado).ToList();
            }
        }

        public SPE_OBTIENE_USUARIO_Result ObtenerUsuario(string pClUsuario)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_USUARIO(pClUsuario).FirstOrDefault();
            }
        }

        public E_USUARIO ObtieneUsuarioCambioPassword(string pClUsuario)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.Database.SqlQuery<E_USUARIO>("EXEC " +
                    "ADM.SPE_OBTIENE_USUARIO_PDE_CAMBIO_PASSWORD " +
                    "@PIN_CL_USUARIO ",
                    new SqlParameter("@PIN_CL_USUARIO", (Object)pClUsuario ?? DBNull.Value)
                ).FirstOrDefault();

                //return contexto.SPE_OBTIENE_INSTRUCTORES(pIdInstructor, pIdCompetencia, pIdCurso, pXmlCompetencias,pID_EMPRESA).ToList();
            }
        }
        public SPE_OBTIENE_USUARIOPDE_Result ObtenerUsuarioPde(string pClUsuario)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_USUARIOPDE(pClUsuario).FirstOrDefault();
            }
        }
        public SPE_OBTIENE_USUARIO_NUEVO_Result ObtenerUsuarioNuevo(string pClUsuario)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_USUARIO_NUEVO(pClUsuario).FirstOrDefault();
            }
        }
        public SPE_OBTIENE_USUARIO_AUTENTICACION_Result AutenticaUsuario(E_USUARIO pUsuario)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_USUARIO_AUTENTICACION(pUsuario.CL_USUARIO, pUsuario.NB_PASSWORD, pUsuario.CL_AUTENTICACION.ToString()).FirstOrDefault();
            }
        }

        public XElement CambiarPassword(E_USUARIO pUsuario, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_USUARIO_CAMBIAR_PASSWORD(pOutRetorno, pUsuario.CL_USUARIO, pUsuario.NB_CORREO_ELECTRONICO, pUsuario.NB_PASSWORD, pUsuario.CL_CAMBIAR_PASSWORD, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_PDE_CONFIGURACION_URL_Result> ObtieneConfiguracionPDE()
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_PDE_CONFIGURACION_URL().ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_USUARIO
        public int InsertaActualiza_C_USUARIO(string tipo_transaccion, SPE_OBTIENE_C_USUARIO_Result V_C_USUARIO, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_C_USUARIO(pout_clave_retorno, V_C_USUARIO.CL_USUARIO, V_C_USUARIO.NB_USUARIO, V_C_USUARIO.NB_CORREO_ELECTRONICO, V_C_USUARIO.NB_PASSWORD, V_C_USUARIO.FG_CAMBIAR_PASSWORD, V_C_USUARIO.XML_PERSONALIZACION, V_C_USUARIO.ID_ROL, V_C_USUARIO.ID_EMPLEADO, V_C_USUARIO.FG_ACTIVO, V_C_USUARIO.FE_INACTIVO,usuario, usuario, programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
                return Convert.ToInt32(pout_clave_retorno.Value); ;
            }
        }

        public XElement InsertarActualizarUsuario(E_TIPO_OPERACION_DB pClTipoOperacion, E_USUARIO pUsuario, string pClUsuario, string pNbUsuario)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_USUARIO(pOutClRetorno, pUsuario.CL_USUARIO, pUsuario.NB_USUARIO, pUsuario.NB_PASSWORD, pUsuario.NB_CORREO_ELECTRONICO, pUsuario.FG_ACTIVO, pUsuario.FG_CAMBIAR_PASSWORD, pUsuario.ID_ROL, pUsuario.ID_EMPLEADO, pClUsuario, pNbUsuario, pUsuario.CL_TIPO_MULTIEMPRESA ,pClTipoOperacion.ToString());
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement ActualizarUsuarioPDE(E_TIPO_OPERACION_DB pClTipoOperacion, E_USUARIO pUsuario, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                var pXmlResultado = new SqlParameter("@XML_RESULTADO", SqlDbType.Xml)
                {
                    Direction = ParameterDirection.Output
                };

                //ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.Database.ExecuteSqlCommand("EXEC " +
                    "ADM.SPE_ACTUALIZA_PASSWORD_USUARIO_PDE " +
                    "@XML_RESULTADO OUTPUT," +
                    "@PIN_CL_USER, " +
                    "@PIN_NB_PASSWORD, " +
                    "@PIN_NB_CORREO_ELECTRONICO, " +
                    "@PIN_FG_ACTIVO, "+
                    "@PIN_FG_CAMBIAR_PASSWORD, " +
                    "@PIN_CL_USUARIO, " +
                    "@PIN_NB_PROGRAMA, " +
                    "@PIN_TIPO_TRANSACCION",
                    pXmlResultado,
                    new SqlParameter("@PIN_CL_USER",pUsuario.CL_USUARIO),
                    new SqlParameter("@PIN_NB_PASSWORD", pUsuario.NB_PASSWORD),
                    new SqlParameter("@PIN_NB_CORREO_ELECTRONICO",pUsuario.NB_CORREO_ELECTRONICO),
                    new SqlParameter("@PIN_FG_ACTIVO", pUsuario.FG_ACTIVO),
                    new SqlParameter("@PIN_FG_CAMBIAR_PASSWORD",pUsuario.FG_CAMBIAR_PASSWORD),
                    new SqlParameter("@PIN_CL_USUARIO", pClUsuario),
                    new SqlParameter("@PIN_NB_PROGRAMA", pNbPrograma),
                    new SqlParameter("@PIN_TIPO_TRANSACCION", pClTipoOperacion.ToString())
                );

                return XElement.Parse(pXmlResultado.Value.ToString());

              
            }
        }

        public XElement InsertarActualizarUsuario_pde(E_TIPO_OPERACION_DB pClTipoOperacion, E_USUARIO pUsuario, string pClUsuario, string pNbUsuario)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_USUARIO_PDE(pOutClRetorno, pUsuario.CL_USUARIO, pUsuario.NB_USUARIO, pUsuario.NB_PASSWORD, pUsuario.NB_CORREO_ELECTRONICO, pUsuario.FG_ACTIVO, pUsuario.FG_CAMBIAR_PASSWORD, pUsuario.ID_ROL, pUsuario.ID_EMPLEADO_PDE, pClUsuario, pNbUsuario, pUsuario.CL_TIPO_MULTIEMPRESA, pClTipoOperacion.ToString());
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }
        public XElement InsertarUsuariosPdeMasivo(string XMLEMPLEADOS , string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_USUARIOS_PDE_MASIVO(pOutClRetorno, XMLEMPLEADOS, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }
        public XElement InsertarUsuarioCorreo(string XMLUSUARIOS, string pClUsuario, string pNbPrograma, string transaccion)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_ESTATUS_ENVIO_CORREO(pOutClRetorno, XMLUSUARIOS, pClUsuario, pNbPrograma, transaccion );
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }
        #endregion

        #region ELIMINA DATOS  C_USUARIO
        public int Elimina_C_USUARIO(String CL_USUARIO = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_C_USUARIO(pout_clave_retorno, CL_USUARIO, usuario, programa);
                //regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }

        public XElement EliminarUsuario(string pClUser, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_USUARIO(pOutRetorno, pClUser, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutRetorno.Value.ToString());
            }
        }
        #endregion

    }
}
