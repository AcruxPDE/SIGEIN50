using SIGE.AccesoDatos.Implementaciones.PuntoDeEncuentro;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Entidades.PuntoDeEncuentro;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Negocio.PuntoDeEncuentro
{
    public class ListaComunicadosNegocio
    {

        #region OBTENER COMUNICADO
        public List<E_OBTIENE_ADM_COMUNICADO> ObtenerComunicados(int? ID_COMUNICADO = null, string NB_COMUNICADO = null, DateTime? FE_COMUNICADO = null, string DS_COMUNICADO = null, DateTime? FE_VISIBLE_DEL = null, DateTime? FE_VISIBLE_AL = null, int? ID_ARCHIVO_PDE = null, byte? FG_PRIVADO = null, bool? FG_ESTATUS = null, string NB_ARCHIVO = null)
        {
            ListaComunicadosOperaciones operaciones = new ListaComunicadosOperaciones();
            var vComunicados = operaciones.ObtenerComunicados(ID_COMUNICADO, NB_COMUNICADO, FE_COMUNICADO, DS_COMUNICADO, FE_VISIBLE_DEL, FE_VISIBLE_AL, ID_ARCHIVO_PDE, FG_PRIVADO, FG_ESTATUS, NB_ARCHIVO).ToList();
            return (from c in vComunicados
                    select new E_OBTIENE_ADM_COMUNICADO
                    {
                        ID_COMUNICADO = c.ID_COMUNICADO,
                        NB_COMUNICADO = c.NB_COMUNICADO,
                        FE_COMUNICADO = c.FE_COMUNICADO,
                        DS_COMUNICADO = c.DS_COMUNICADO,
                        FE_VISIBLE_DEL = c.FE_VISIBLE_DEL,
                        FE_VISIBLE_AL = c.FE_VISIBLE_AL,
                        ID_ARCHIVO_PDE = c.ID_ARCHIVO_PDE,
                        FG_PRIVADO = (c.FG_PRIVADO == 1 ? "Privado" : "Público"),
                        FG_ESTATUS = c.FG_ESTATUS,
                        NB_ARCHIVO = (c.NB_ARCHIVO == null ? "Sin Adjunto" : "Archivo Adjunto"),
                        TOTAL = (int)c.TOTAL ,
                        LEIDOS = (int)c.LEIDOS,
                        COMENTARIOS =(int)c.COMENTARIOS,
                        TIPO_COMUNICADO = c.TIPO_COMUNICADO,
                        TIPO_ACCION = c.TIPO_ACCION
                    }).ToList();
        }
        #endregion


        public List<SPE_OBTIENE_K_COMUNICADO_LEIDO_Result> ObtenerEmpleadosComunicadosLeidos(int idComunicado)
        {
            ListaComunicadosOperaciones operaciones = new ListaComunicadosOperaciones();
            return operaciones.ObtenerEmpleadosComunicadosLeidos(idComunicado).ToList();
        }
        #region OBTENER EMPLEADOS COMUNICADO
        public List<E_OBTIENE_EMPLEADOS_COMUNICADO> ObtenerEmpleadosComunicado(int? ID_COMUNICADO = null, string NB_NOMBRE = null, string NB_PUESTO = null)
        {
            ListaComunicadosOperaciones operaciones = new ListaComunicadosOperaciones();
            var vComunicados = operaciones.ObtenerEmpleadosComunicado(ID_COMUNICADO, NB_NOMBRE, NB_PUESTO).ToList();
            return (from c in vComunicados
                    select new E_OBTIENE_EMPLEADOS_COMUNICADO
                    {
                        ID_EMPLEADO = c.ID_EMPLEADO,
                        NB_EMPLEADO = c.NB_EMPLEADO,
                        NB_PUESTO = c.NB_PUESTO,
                        M_CL_USUARIO = c.M_CL_USUARIO 
                    }).ToList();
        }
        #endregion


        #region INSERTA ACTUALIZA COMUNICADO

        public E_RESULTADO InsertaActualizaComunicado(int ID_ARCHIVO_PDE, string NB_ARCHIVO, byte[] FI_ARCHIVO, int ID_COMUNICADO, string NB_COMUNICADO, string DS_COMUNICADO, DateTime FECHA_DEL, DateTime FECHA_AL, string XML_EMPLEADOS, byte? FG_PRIVADO, string pCLusuario, string pNBprograma, string TIPO_TRANSACCION)
        {
            ListaComunicadosOperaciones operaciones = new ListaComunicadosOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualizaComunicado(ID_ARCHIVO_PDE, NB_ARCHIVO, FI_ARCHIVO, ID_COMUNICADO, NB_COMUNICADO, DS_COMUNICADO, FECHA_DEL, FECHA_AL, XML_EMPLEADOS, FG_PRIVADO, pCLusuario, pNBprograma, TIPO_TRANSACCION));

        }
        public E_RESULTADO InsertaActualizaComunicadoInformacion(int ID_ARCHIVO_PDE, string NB_ARCHIVO, byte[] FI_ARCHIVO, int ID_COMUNICADO, string NB_COMUNICADO, string DS_COMUNICADO, DateTime FECHA_DEL, DateTime FECHA_AL, string XML_EMPLEADOS, byte? FG_PRIVADO, string pCLusuario, string pNBprograma, string TIPO_TRANSACCION, string TIPO_COMUNICADO, string TIPO_ACCION, string XML_EMPLEADOSINFORMACION)
        {
            ListaComunicadosOperaciones operaciones = new ListaComunicadosOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualizaComunicadoInformacion(ID_ARCHIVO_PDE, NB_ARCHIVO, FI_ARCHIVO, ID_COMUNICADO, NB_COMUNICADO, DS_COMUNICADO, FECHA_DEL, FECHA_AL, XML_EMPLEADOS, FG_PRIVADO, pCLusuario, pNBprograma, TIPO_TRANSACCION, TIPO_COMUNICADO, TIPO_ACCION, XML_EMPLEADOSINFORMACION));

        }
        public string ValidaExisteComunicado(string ID_EMPLEADO, string TIPO_COMUNICADO, string TIPO_ACCION)
        {
            ListaComunicadosOperaciones operaciones = new ListaComunicadosOperaciones();
            return operaciones.ValidaExisteComunicado(ID_EMPLEADO, TIPO_COMUNICADO, TIPO_ACCION).ToString();
        }

        public string ValidaDescriptivoAsignado(string ID_PUESTO)
        {
            ListaComunicadosOperaciones operaciones = new ListaComunicadosOperaciones();
            return operaciones.ValidaPuestoAsignado(ID_PUESTO).ToString();
        }

        public string ValidaInventarioAsignado(string ID_EMPLEADO)
        {
            ListaComunicadosOperaciones operaciones = new ListaComunicadosOperaciones();
            return operaciones.ValidaEmpleadoAsignado(ID_EMPLEADO).ToString();
        }

        #endregion


        #region ELIMINA COMUNICADO
        public E_RESULTADO EliminaComunicado(int? ID_COMUNICADO = null, string pCLusuario = null, string pNBprograma = null)
        {
            ListaComunicadosOperaciones operaciones = new ListaComunicadosOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.EliminaComunicado(ID_COMUNICADO, pCLusuario, pNBprograma));
        }
        #endregion

        
        
    }
}
