using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using System.Xml.Linq;
using SIGE.Negocio.Utilerias;
using SIGE.Entidades.Externas;
using SIGE.AccesoDatos.Implementaciones.Administracion;

namespace SIGE.Negocio.Administracion
{
   public class CentroAdministrativoNegocio
    {
        #region OBTIENE DATOS  C_CENTRO_ADMVO
        public List<SPE_OBTIENE_CENTROS_ADMVOS_Result> Obtener_C_CENTRO_ADMVO(Guid? ID_CENTRO_ADMVO = null, String CL_CLIENTE = null, Guid? ID_REGISTRO_PATRONAL = null, String CL_CENTRO_ADMVO = null, String NB_CENTRO_ADMVO = null, String NB_CALLE = null, String NB_NO_EXTERIOR = null, String NB_NO_INTERIOR = null, String NB_COLONIA = null, String CL_MUNICIPIO = null, String NB_MUNICIPIO = null, String CL_ESTADO=null,String NB_ESTADO=null, String CL_CODIGO_POSTAL = null, String CL_ZONA_ECONOMICA = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_CREA_APP = null, String CL_USUARIO_MODIFICA_APP = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            CentroAdministrativoOperaciones operaciones = new CentroAdministrativoOperaciones();
            return operaciones.Obtener_C_CENTRO_ADMVO(ID_CENTRO_ADMVO, CL_CLIENTE, ID_REGISTRO_PATRONAL, CL_CENTRO_ADMVO, NB_CENTRO_ADMVO, NB_CALLE, NB_NO_EXTERIOR, NB_NO_INTERIOR, NB_COLONIA,CL_MUNICIPIO, NB_MUNICIPIO, CL_ESTADO, NB_ESTADO, CL_CODIGO_POSTAL, CL_ZONA_ECONOMICA, FE_CREACION, FE_MODIFICACION,CL_USUARIO_CREA_APP, CL_USUARIO_MODIFICA_APP, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_CENTRO_ADMVO
        public E_RESULTADO InsertaActualizaCCentroAdmvo(String pClTipoOperacion, E_CENTROS_ADMVOS vCCentroAdmvo, string usuario, string programa)
        {
            CentroAdministrativoOperaciones operaciones = new CentroAdministrativoOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertarActualizarCCentroAdmvo(pClTipoOperacion, vCCentroAdmvo, usuario, programa));
        }
        #endregion
            
        #region OBTIENE TODOS LOS REGISTROS PATRONALES
        public List<E_REGISTRO_PATRONAL> ObtieneRegistroPatronal()
        {
            CentroAdministrativoOperaciones operaciones = new CentroAdministrativoOperaciones();
            return operaciones.ObtieneRegistroPatronal();
        }
        #endregion




        #region ELIMINA DATOS  C_CENTRO_ADMVO
        public E_RESULTADO EliminarCCentroAdmvo(Guid pIdCentroAdmvo, string usuario, string programa)
        {
            CentroAdministrativoOperaciones operaciones = new CentroAdministrativoOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.EliminarCCentroAdmvo(pIdCentroAdmvo, usuario, programa));
        }
        #endregion
    } 
}
 
