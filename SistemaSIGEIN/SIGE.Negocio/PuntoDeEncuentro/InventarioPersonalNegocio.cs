using SIGE.AccesoDatos.Implementaciones.PuntoDeEncuentro;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Negocio.PuntoDeEncuentro
{
    public class InventarioPersonalNegocio
    {
        public List<SPE_OBTIENE_CAMPOS_INVENTARIO_PERSONAL_Result> obtieneCamposInventarioPersonal()
        {
            InventarioPersonalOperaciones op = new InventarioPersonalOperaciones();
            return op.obtieneCamposInventarioPersonal();
        }

        public E_RESULTADO Actualiza_Config_Campos_Inventario(string tipo_transaccion, SPE_OBTIENE_CAMPOS_INVENTARIO_PERSONAL_Result v_c_prueba_nivel, string usuario, string programa)
        {
            InventarioPersonalOperaciones operaciones = new InventarioPersonalOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Actualiza_Config_Campos_Inventario(tipo_transaccion, v_c_prueba_nivel, usuario, programa));
        }
        public E_RESULTADO ActualizaModificacionEmpleado(int? ID_CAMBIO = null, bool FG_AUTORIZADO = false, string DS_CAMBIO = "", string ID_EMPLEADO = null, string CL_ESTADO = "", string TIPO_TRANSACCION = "", string CL_USUARIO_APP = "", string NB_PROGRAMA = "")
        {
         
                InventarioPersonalOperaciones operaciones = new InventarioPersonalOperaciones();
                return UtilRespuesta.EnvioRespuesta(operaciones.ActualizaModificacionEmpleado(ID_CAMBIO, FG_AUTORIZADO,DS_CAMBIO, ID_EMPLEADO, CL_ESTADO, TIPO_TRANSACCION, CL_USUARIO_APP, NB_PROGRAMA));
                  
        }
        public E_RESULTADO Actualiza_Informacion_Original_de_Empleado(string PIN_ID_EMPLEADO, string CL_USUARIO = "", string NB_PROGRAMA = "", int? ID_CAMBIO = null, bool FG_AUTORIZADO = true, string DS_CAMBIO = "", string PIN_CL_ESTADO = "")
        {
            InventarioPersonalOperaciones operaciones = new InventarioPersonalOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Actualiza_Informacion_Original_de_Empleado(PIN_ID_EMPLEADO, CL_USUARIO, NB_PROGRAMA, ID_CAMBIO
            , FG_AUTORIZADO, DS_CAMBIO, PIN_CL_ESTADO));
        }
    }
}
