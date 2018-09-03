using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal; // reemplazar por la carpeta correspondiente
using SIGE.Entidades.Administracion;

namespace SIGE.Negocio.Administracion  // reemplazar por la carpeta correspondiente
{
    public class SecuenciaNegocio
    {

        //#region OBTIENE FOLIOS DE LA SECUENCIA

        //public List<SPE_OBTIENE_FOLIO_SECUENCIA_Result> ObtieneFolioSecuencia(String CL_SECUENCIA = null)
        //{
        //    SecuenciaOperaciones operaciones = new SecuenciaOperaciones();
        //    return operaciones.ObtieneFolioSecuencia(CL_SECUENCIA);
            
        //}

        //#endregion

        //#region OBTIENE DATOS  C_SECUENCIA
        //public List<SPE_OBTIENE_C_SECUENCIA_Result> Obtener_C_SECUENCIA(String CL_SECUENCIA = null, String CL_PREFIJO = null, int? NO_ULTIMO_VALOR = null, int? NO_VALOR_MAXIMO = null, String CL_SUFIJO = null, byte? NO_DIGITOS = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        //{
        //    SecuenciaOperaciones operaciones = new SecuenciaOperaciones();
        //    return operaciones.Obtener_C_SECUENCIA(CL_SECUENCIA, CL_PREFIJO, NO_ULTIMO_VALOR, NO_VALOR_MAXIMO, CL_SUFIJO, NO_DIGITOS, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
        //}
        //#endregion

        //#region INSERTA ACTUALIZA DATOS  C_SECUENCIA
        //public int InsertaActualiza_C_SECUENCIA(string tipo_transaccion, SPE_OBTIENE_C_SECUENCIA_Result V_C_SECUENCIA, string usuario, string programa)
        //{
        //    SecuenciaOperaciones operaciones = new SecuenciaOperaciones();
        //    return operaciones.InsertaActualiza_C_SECUENCIA(tipo_transaccion, V_C_SECUENCIA, usuario, programa);
        //}
        //#endregion

        //#region ELIMINA DATOS  C_SECUENCIA
        //public int Elimina_C_SECUENCIA(String CL_SECUENCIA = null, string usuario = null, string programa = null)
        //{
        //    SecuenciaOperaciones operaciones = new SecuenciaOperaciones();
        //    return operaciones.Elimina_C_SECUENCIA(CL_SECUENCIA, usuario, programa);
        //}
        //#endregion
    }
}
