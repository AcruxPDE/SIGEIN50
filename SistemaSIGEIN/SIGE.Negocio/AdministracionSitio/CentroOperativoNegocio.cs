using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Negocio.Utilerias;
using SIGE.Entidades.Externas;
using SIGE.AccesoDatos.Implementaciones.Administracion;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;

namespace SIGE.Negocio.Administracion
{
   public class CentroOperativoNegocio
    {
       #region OBTIENE DATOS  C_CENTRO_OPTVO
       public List<SPE_OBTIENE_CENTROS_OPTVOS_Result> Obtener_C_CENTRO_OPTVO(Guid? ID_CENTRO_OPTVO = null, String CL_CLIENTE = null, String CL_CENTRO_OPTVO = null, String NB_CENTRO_OPTVO = null, String NB_CALLE = null, String NB_NO_EXTERIOR = null, String NB_NO_INTERIOR = null, String NB_COLONIA = null,String CL_ESTADO=null,String NB_ESTADO=null,String CL_MUNICIPIO = null, String NB_MUNICIPIO = null, String CL_CODIGO_POSTAL = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_CREA_APP = null, String CL_USUARIO_MODIFICA_APP = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
       {
           CentroOperativoOperaciones oCentroOptvo = new CentroOperativoOperaciones();
           return oCentroOptvo.Obtener_C_CENTRO_OPTVO(ID_CENTRO_OPTVO, CL_CLIENTE, CL_CENTRO_OPTVO, NB_CENTRO_OPTVO, NB_CALLE, NB_NO_EXTERIOR, NB_NO_INTERIOR, NB_COLONIA,CL_ESTADO,NB_ESTADO,CL_MUNICIPIO, NB_MUNICIPIO, CL_CODIGO_POSTAL, FE_CREACION, FE_MODIFICACION, CL_USUARIO_CREA_APP, CL_USUARIO_MODIFICA_APP, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
       }
       #endregion

       #region INSERTA ACTUALIZA DATOS  C_CENTRO_OPTVO
       public E_RESULTADO InsertaActualizaCCentroAdmvo(String pClTipoOperacion, E_CENTROS_OPTVOS vCCentroOptvo, string usuario, string programa)
       {
           CentroOperativoOperaciones operaciones = new CentroOperativoOperaciones();
           return UtilRespuesta.EnvioRespuesta(operaciones.InsertarActualizarCCentroOptvo(pClTipoOperacion, vCCentroOptvo, usuario, programa));
       }
       #endregion

       #region ELIMINA DATOS  C_CENTRO_OPTVO
       public E_RESULTADO EliminarCentroOptvo(Guid pIdCentroAdmvo, string usuario, string programa)
       {
           CentroOperativoOperaciones operaciones = new CentroOperativoOperaciones();
           return UtilRespuesta.EnvioRespuesta(operaciones.EliminarCentroOptvo(pIdCentroAdmvo, usuario, programa));
       }
       #endregion
    }
}
