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
using System.Data;
using System.Data.SqlClient;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{

    public class RolOperaciones
    {

        private SistemaSigeinEntities context;

        
        //public List<SPE_OBTIENE_C_ROL_Result> Obtener_C_ROL(int? ID_ROL = null, String CL_ROL = null, String NB_ROL = null, String XML_AUTORIZACION = null, bool? FG_ACTIVO = null, DateTime? FE_INACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        //{
        //    using (context = new SistemaSigeinEntities())
        //    {
        //        var q = from V_C_ROL in context.SPE_OBTIENE_C_ROL(ID_ROL, CL_ROL, NB_ROL, XML_AUTORIZACION, FG_ACTIVO, FE_INACTIVO)
        //                select V_C_ROL;
        //        return q.ToList();
        //    }
        //}

        public List<SPE_OBTIENE_ROLES_Result> ObtenerRoles(int? pIdRol)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_ROLES(pIdRol).ToList();
            }
        }

        public E_OBTENE_ROL ObtieneRol(int? pIdRol)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.Database.SqlQuery<E_OBTENE_ROL>("EXEC " +
                    "ADM.SPE_OBTIENE_ROL " +
                    "@PIN_ID_ROL"
                    , new SqlParameter("@PIN_ID_ROL", (object)pIdRol ?? DBNull.Value)).FirstOrDefault();
                    //SPE_OBTIENE_ROL(pIdRol).FirstOrDefault();
            }
        }
           
        //public int InsertaActualiza_C_ROL(string tipo_transaccion, SPE_OBTIENE_C_ROL_Result V_C_ROL, string usuario, string programa)
        //{
        //    using (context = new SistemaSigeinEntities())
        //    {
        //        //Declaramos el objeto de valor de retorno
        //        ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
        //        context.SPE_INSERTA_ACTUALIZA_C_ROL(pout_clave_retorno, V_C_ROL.ID_ROL, V_C_ROL.CL_ROL, V_C_ROL.NB_ROL, V_C_ROL.XML_AUTORIZACION, V_C_ROL.FG_ACTIVO, V_C_ROL.FE_INACTIVO, usuario, usuario, programa, programa, tipo_transaccion);
        //        //regresamos el valor de retorno de sql
        //        return Convert.ToInt32(pout_clave_retorno.Value); ;
        //    }
        //}

        public XElement InsertarActualizarRoles(E_TIPO_OPERACION_DB pClTipoOperacion, SPE_OBTIENE_C_ROL_Result pRol, XElement pXmlFunciones, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_ROLES(pOutClRetorno, pRol.ID_ROL, pRol.CL_ROL, pRol.NB_ROL,pRol.ID_PLANTILLA, pRol.FG_ACTIVO,pRol.FG_SUELDO_VISIBLE, pXmlFunciones.ToString(),pRol.XML_GRUPOS, pClUsuario, pNbPrograma, pClTipoOperacion.ToString());
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }
        
        public XElement EliminarRol(int pIdRol, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_ROL(pOutClRetorno, pIdRol, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }
    }
}
