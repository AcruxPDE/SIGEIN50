using System.Collections.Generic;
using System.Linq;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades.Administracion;
using System.Xml.Linq;
using SIGE.Negocio.Utilerias;
using SIGE.Entidades.Externas;

namespace SIGE.Negocio.Administracion
{
    public class RolNegocio
    {
        //#region OBTIENE DATOS  C_ROL
        //public List<SPE_OBTIENE_C_ROL_Result> Obtener_C_ROL(int? ID_ROL = null, String CL_ROL = null, String NB_ROL = null, String XML_AUTORIZACION = null, bool? FG_ACTIVO = null, DateTime? FE_INACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        //{
        //    RolOperaciones operaciones = new RolOperaciones();
        //    return operaciones.Obtener_C_ROL(ID_ROL, CL_ROL, NB_ROL, XML_AUTORIZACION, FG_ACTIVO, FE_INACTIVO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
        //}

        public List<SPE_OBTIENE_ROLES_Result> ObtieneRoles(int? pIdRol)
        {
            RolOperaciones oRol = new RolOperaciones();
            return oRol.ObtenerRoles(pIdRol);
        }

        public E_ROL ObtieneFuncionesRol(int? pIdRol)
        {
            RolOperaciones nRol = new RolOperaciones();
            E_OBTENE_ROL vRol = nRol.ObtieneRol(pIdRol);

            List<E_FUNCION> vFunciones = XElement.Parse(vRol.XML_AUTORIZACION).Elements("FUNCION").Select(el => new E_FUNCION
            {
                ID_FUNCION = (int?)UtilXML.ValorAtributo(el.Attribute("ID_FUNCION"), E_TIPO_DATO.INT),
                CL_FUNCION = el.Attribute("CL_FUNCION").Value,
                CL_TIPO_FUNCION = el.Attribute("CL_TIPO_FUNCION").Value,
                NB_FUNCION = el.Attribute("NB_FUNCION").Value,
                FG_SELECCIONADO = (bool)UtilXML.ValorAtributo(el.Attribute("FG_SELECCIONADO"), E_TIPO_DATO.BOOLEAN),
                ID_FUNCION_PADRE = (int?)UtilXML.ValorAtributo(el.Attribute("ID_FUNCION_PADRE"), E_TIPO_DATO.INT)
            }).ToList();

            return new E_ROL
            {
                ID_ROL = (vRol.ID_ROL < 0) ? (int?)null : vRol.ID_ROL,
                CL_ROL = vRol.CL_ROL,
                ID_PLANTILLA = vRol.ID_PLANTILLA,
                FG_ACTIVO = vRol.FG_ACTIVO,
                FG_SUELDO_VISIBLE= vRol.FG_SUELDO_VISIBLE,
                NB_ROL = vRol.NB_ROL,
                LST_FUNCIONES = vFunciones,
                XML_GRUPOS = vRol.XML_GRUPOS
            };
        }
        

        //#region INSERTA ACTUALIZA DATOS  C_ROL
        //public int InsertaActualiza_C_ROL(string tipo_transaccion, SPE_OBTIENE_C_ROL_Result V_C_ROL, string usuario, string programa)
        //{
        //    RolOperaciones operaciones = new RolOperaciones();
        //    return operaciones.InsertaActualiza_C_ROL(tipo_transaccion, V_C_ROL, usuario, programa);
        //}

        public E_RESULTADO InsertaActualizaRoles(E_TIPO_OPERACION_DB pClTipoOperacion, SPE_OBTIENE_C_ROL_Result pRol, XElement pXmlFunciones, string pClUsuario, string pNbPrograma)
        {
            RolOperaciones oRol = new RolOperaciones();
            return UtilRespuesta.EnvioRespuesta(oRol.InsertarActualizarRoles(pClTipoOperacion, pRol, pXmlFunciones, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO EliminaRol(int pIdRol, string pClUsuario, string pNbPrograma)
        {
            RolOperaciones oRol = new RolOperaciones();
            return UtilRespuesta.EnvioRespuesta(oRol.EliminarRol(pIdRol, pClUsuario, pNbPrograma));
        }
        
    }
}
