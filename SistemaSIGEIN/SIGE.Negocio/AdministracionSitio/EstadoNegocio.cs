using System;
using System.Collections.Generic;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;

namespace SIGE.Negocio.Administracion
{
    public class EstadoNegocio
    {
        
        public List<SPE_OBTIENE_C_ESTADO_Result> ObtieneEstados(int? ID_ESTADO = null, String CL_PAIS = null, String CL_ESTADO = null, String NB_ESTADO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            EstadoOperaciones operaciones = new EstadoOperaciones();
            return operaciones.ObtenerEstados(ID_ESTADO, CL_PAIS, CL_ESTADO, NB_ESTADO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
        }
                
        public int InsertaActualiza_C_ESTADO(string tipo_transaccion, SPE_OBTIENE_C_ESTADO_Result V_C_ESTADO, string usuario, string programa)
        {
            EstadoOperaciones operaciones = new EstadoOperaciones();
            return operaciones.InsertaActualiza_C_ESTADO(tipo_transaccion, V_C_ESTADO, usuario, programa);
        }
                
        public int Elimina_C_ESTADO(int? ID_ESTADO = null, string usuario = null, string programa = null)
        {
            EstadoOperaciones operaciones = new EstadoOperaciones();
            return operaciones.Elimina_C_ESTADO(ID_ESTADO, usuario, programa);
        }
        
    }
}