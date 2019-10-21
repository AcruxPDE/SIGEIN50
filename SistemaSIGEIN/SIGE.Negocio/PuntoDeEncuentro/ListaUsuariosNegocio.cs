using SIGE.AccesoDatos.Implementaciones.PuntoDeEncuentro;
using SIGE.Entidades.Externas;
using SIGE.Entidades.PuntoDeEncuentro;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;

namespace SIGE.Negocio.PuntoDeEncuentro
{
    public class ListaUsuariosNegocio
    {
        public E_RESULTADO InsertaProcesos(E_PROCESOS PROCESOS, string pCLusuario, string pNBprograma, string TIPO_TRANSACCION)
        {
            ListaUsuariosOperaciones operaciones = new ListaUsuariosOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaProcesos(PROCESOS.ID_USUARIO_FUNCION, PROCESOS.CL_USUARIO_PROCESO, PROCESOS.FG_COMUNICADOS, PROCESOS.FG_TRAMITES, PROCESOS.FG_COMPROMISOS, PROCESOS.FG_NOMINA, pCLusuario, pNBprograma, TIPO_TRANSACCION));

        }
    }
}
