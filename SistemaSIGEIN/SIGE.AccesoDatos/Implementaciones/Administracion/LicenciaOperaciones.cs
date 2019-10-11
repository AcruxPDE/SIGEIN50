using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using System.Data.Objects;
using System.Xml.Linq;
using SIGE.Entidades.Administracion;
using System.Data.SqlClient;
using System.Data;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{
   
    public class LicenciaOperaciones
    {
		private SistemaSigeinEntities context;

        public SPE_OBTIENE_CONFIGURACION_LICENCIA_Result obtieneConfiguracion(string CL_CONFIGURACION = null, string CL_USUARIO = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_CONFIGURACION_LICENCIA(CL_CONFIGURACION, CL_USUARIO).FirstOrDefault();
            }
        }

        public List<SPE_OBTIENE_EMPLEADOS_Result> ObtenerEmpleados(XElement pXmlSeleccion = null, bool? pFgFoto = false, string pClUsuario = null, bool? pFgActivo = null, int? pIdEmpresa = null,int? pIdRol = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                string vXmlFiltro = null;
                if (pXmlSeleccion != null)
                    vXmlFiltro = pXmlSeleccion.ToString();
                return context.SPE_OBTIENE_EMPLEADOS(vXmlFiltro, pClUsuario, pFgActivo, pFgFoto, pIdEmpresa, pIdRol).ToList();
            }
        }

        public XElement InsertaActualiza_S_CONFIGURACION(string CL_CONFIGURACION = null, string NO_CONFIGURACION = null, string CL_USUARIO = null, string NB_PROGRAMA = null, string TIPO_TRANSACCION = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_S_CONFIGURACION_LICENCIA(pout_clave_retorno, CL_CONFIGURACION, NO_CONFIGURACION, CL_USUARIO, NB_PROGRAMA, TIPO_TRANSACCION);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        //public E_MENSAJES InsertaActualiza_S_CONFIGURACION2(string CL_CONFIGURACION = null, string NO_CONFIGURACION = null, string XML_CONFIGURACION = null, string USUARIO = null, string PROGRAMA = null, string TIPO_TRANSACCION = null)
        //{
        //    using (context = new SistemaSigeinEntities())
        //    {
        //        ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(string));
        //        ObjectParameter pout_mensaje_retorno = new ObjectParameter("POUT_MENSAJE_RETORNO", typeof(string));
        //        context.SPE_INSERTA_ACTUALIZA_S_CONFIGURACION_NOMINA(pout_clave_retorno, pout_mensaje_retorno, CL_CONFIGURACION, NO_CONFIGURACION, XML_CONFIGURACION, USUARIO, PROGRAMA, TIPO_TRANSACCION);
        //        var mensaje = new E_MENSAJES() { clave_retorno = pout_clave_retorno.Value.ToString(), mensaje_retorno = pout_mensaje_retorno.Value.ToString() };
        //        return mensaje;
        //    }
        //}

        public E_MENSAJES InsertaActualiza_S_CONFIGURACION_NOMINA(string CL_CONFIGURACION = null, string NO_CONFIGURACION = null, string XML_CONFIGURACION = null, string USUARIO = null, string PROGRAMA = null, string TIPO_TRANSACCION = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var pout_clave_retorno = new SqlParameter("@POUT_CLAVE_RETORNO", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Output
                };
                var pout_mensaje_retorno = new SqlParameter("@POUT_MENSAJE_RETORNO", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Output
                };
                //ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(string));
                //ObjectParameter pout_mensaje_retorno = new ObjectParameter("POUT_MENSAJE_RETORNO", typeof(string));
                //context.SPE_INSERTA_ACTUALIZA_S_CONFIGURACION_NOMINA(pout_clave_retorno, pout_mensaje_retorno, CL_CONFIGURACION, NO_CONFIGURACION, XML_CONFIGURACION, USUARIO, PROGRAMA, TIPO_TRANSACCION);
                context.Database.ExecuteSqlCommand("EXEC " +
                    "[Nomina].[SPE_INSERTA_ACTUALIZA_S_CONFIGURACION_NOMINA]" +
                    "@POUT_CLAVE_RETORNO OUTPUT" +
                    ",@POUT_MENSAJE_RETORNO OUTPUT" +
                    ",@PIN_CL_CONFIGURACION" +
                    ", @PIN_NO_CONFIGURACION" +
                    ", @PIN_XML_CONFIGURACION" +
                    ", @PIN_CL_USUARIO_MODIFICA" +
                    ", @PIN_NB_PROGRAMA_MODIFICA" +
                    ", @PIN_TIPO_TRANSACCION"
                    , pout_clave_retorno
                    , pout_mensaje_retorno
                    , new SqlParameter("@PIN_CL_CONFIGURACION", (object)CL_CONFIGURACION ?? DBNull.Value)
                    , new SqlParameter("@PIN_NO_CONFIGURACION", (object)NO_CONFIGURACION ?? DBNull.Value)
                    , new SqlParameter("@PIN_XML_CONFIGURACION", (object)XML_CONFIGURACION ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_USUARIO_MODIFICA", (object)USUARIO ?? DBNull.Value)
                    , new SqlParameter("@PIN_NB_PROGRAMA_MODIFICA", (object)PROGRAMA ?? DBNull.Value)
                    , new SqlParameter("@PIN_TIPO_TRANSACCION", (object)TIPO_TRANSACCION ?? DBNull.Value)
                    );

                var mensaje = new E_MENSAJES() { CL_CLAVE_RETORNO = pout_clave_retorno.Value.ToString(), NB_MENSAJE_RETORNO = pout_mensaje_retorno.Value.ToString() };
                return mensaje;
            }
        }

        public List<E_OBTIENE_S_CONFIGURACION> obtieneConfiguracionContexto(string CL_CLIENTE = null, string CL_CONFIGURACION = null, string NB_CONFIGURACION = null, string NO_CONFIGURACION = null, string DS_CONFIGURACION = null, string TIPO_CONTROL = null)
        {
            using (context = new SistemaSigeinEntities())
            {

                return context.Database.SqlQuery<E_OBTIENE_S_CONFIGURACION>("EXEC " +
                    "[Nomina].[SPE_OBTIENE_S_CONFIGURACION]" +
                    " @PIN_CL_CLIENTE," +
                    "@PIN_CL_CONFIGURACION," +
                    "@PIN_NB_CONFIGURACION," +
                    "@PIN_NO_CONFIGURACION," +
                    "@PIN_DS_CONFIGURACION," +
                    "@PIN_TIPO_CONTROL"
                 , new SqlParameter("@PIN_CL_CLIENTE", (object)CL_CLIENTE ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_CONFIGURACION", (object)CL_CONFIGURACION ?? DBNull.Value)
                    , new SqlParameter("@PIN_NB_CONFIGURACION", (object)NB_CONFIGURACION ?? DBNull.Value)
                    , new SqlParameter("@PIN_NO_CONFIGURACION", (object)NO_CONFIGURACION ?? DBNull.Value)
                    , new SqlParameter("@PIN_DS_CONFIGURACION", (object)DS_CONFIGURACION ?? DBNull.Value)
                    , new SqlParameter("@PIN_TIPO_CONTROL", (object)TIPO_CONTROL ?? DBNull.Value)
                    ).ToList();

                //return context.SPE_OBTIENE_S_CONFIGURACION(CL_CLIENTE, CL_CONFIGURACION, NB_CONFIGURACION, NO_CONFIGURACION, DS_CONFIGURACION, TIPO_CONTROL).ToList();
            }
        }



        public List<SPE_OBTIENE_LICENCIA_VOLUMEN_Result> ObtenerLicenciaVolumen(bool? pFG_ACTIVO = null, int? pID_EMPRESA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_LICENCIA_VOLUMEN(pFG_ACTIVO, pID_EMPRESA).ToList();
            }
        }
	}
}