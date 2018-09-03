using System;
using System.Collections.Generic;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades.Administracion;
using SIGE.Negocio.Utilerias;
using SIGE.Entidades.Externas;

namespace SIGE.Negocio.Administracion
{
    public class EmpresaNegocio
    {        
        public List<SPE_OBTIENE_C_EMPRESA_Result> Obtener_C_EMPRESA(int? ID_EMPRESA = null, String CL_EMPRESA = null, String NB_EMPRESA = null, String NB_RAZON_SOCIAL = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            EmpresaOperaciones operaciones = new EmpresaOperaciones();
            return operaciones.Obtener_C_EMPRESA(ID_EMPRESA, CL_EMPRESA, NB_EMPRESA, NB_RAZON_SOCIAL, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
        }
                
        public E_RESULTADO InsertaActualiza_C_EMPRESA(string tipo_transaccion, E_EMPRESAS V_C_EMPRESA, string usuario, string programa)
        {
            EmpresaOperaciones operaciones = new EmpresaOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualiza_C_EMPRESA(tipo_transaccion, V_C_EMPRESA, usuario, programa));
        }
        
        public E_RESULTADO Elimina_C_EMPRESA(int? ID_EMPRESA = null, string usuario = null, string programa = null)
        {
            EmpresaOperaciones operaciones = new EmpresaOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_C_EMPRESA(ID_EMPRESA, usuario, programa));
        }        
    }
}