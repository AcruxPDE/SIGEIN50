using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.PuntoDeEncuentro;
using SIGE.Entidades.Administracion;
using System.Xml.Linq;
using SIGE.Negocio.Utilerias;
using SIGE.Entidades.Externas;
using SIGE.Entidades.PuntoDeEncuentro;

namespace SIGE.Negocio.PuntoDeEncuentro
{
    public class VisorComunicadoNegocio
    {
        #region OBTIENE COMUNICADO EMPLEADO

        public List<E_OBTIENE_COMUNICADO> ObtenerComunicados(int? ID_COMUNICADO = null, string ID_EMPLEADO = null, string NB_COMUNICADO = null, DateTime? FE_COMUNICADO = null, string DS_COMUNICADO = null, DateTime? FE_VISIBLE_DEL = null, DateTime? FE_VISIBLE_AL = null, int? ID_ARCHIVO_PDE = null, DateTime? FE_CREACION = null, bool? FG_LEIDO = null, bool? FG_ESTATUS = null, byte? FG_PRIVADO = null, string NB_ARCHIVO = null)
        {
            VisorComunicadoOperaciones operaciones = new VisorComunicadoOperaciones();
            var vComunicados = operaciones.ObtenerComunicados(ID_COMUNICADO, ID_EMPLEADO, NB_COMUNICADO, FE_COMUNICADO, DS_COMUNICADO, FE_VISIBLE_DEL, FE_VISIBLE_AL, ID_ARCHIVO_PDE, FE_CREACION, FG_LEIDO, FG_ESTATUS, FG_PRIVADO, NB_ARCHIVO).ToList();
            return (from c in vComunicados
                    select new E_OBTIENE_COMUNICADO
                    {
                        ID_COMUNICADO = c.ID_COMUNICADO,
                        ID_EMPLEADO = c.ID_EMPLEADO,
                        NB_COMUNICADO = c.NB_COMUNICADO,
                        FE_COMUNICADO = c.FE_COMUNICADO,
                        DS_COMUNICADO = c.DS_COMUNICADO,
                        FE_VISIBLE_DEL = c.FE_VISIBLE_DEL,
                        FE_VISIBLE_AL = c.FE_VISIBLE_AL,
                        ID_ARCHIVO_PDE = c.ID_ARCHIVO_PDE,
                        FE_CREACION = c.FE_CREACION,
                        FG_LEIDO = (c.FG_LEIDO ? "Sí" : "No"),
                        FG_ESTATUS = c.FG_ESTATUS,
                        FG_PRIVADO = c.FG_PRIVADO,
                        NB_ARCHIVO = c.NB_ARCHIVO,
                        TIPO_COMUNICADO = c.TIPO_COMUNICADO ,
                        TIPO_ACCION = c.TIPO_ACCION 
                    }).ToList();
        }

        #endregion

        #region OBTIENE COMENTARIOS DEL COMUNICADO

        public List<E_OBTIENE_COMENTARIOS_COMUNICADO> ObtenerComentarios_Comunicado(int? ID_COMENTARIO_COMUNICADO = null, int? ID_COMUNICADO = null, string  ID_EMPLEADO = null, string NOMBRE = null, DateTime? FE_COMENTARIO = null, string DS_COMENTARIO = null, byte? FG_PRIVADO = null)
        {
            VisorComunicadoOperaciones operaciones = new VisorComunicadoOperaciones();
            var vComentarios = operaciones.ObtenerComentarios_Comunicado(ID_COMENTARIO_COMUNICADO, ID_COMUNICADO, ID_EMPLEADO, NOMBRE, FE_COMENTARIO, DS_COMENTARIO, FG_PRIVADO).ToList();
            return (from c in vComentarios
                    select new E_OBTIENE_COMENTARIOS_COMUNICADO
                    {
                        ID_COMENTARIO_COMUNICADO = c.ID_COMENTARIO_COMUNICADO,
                        ID_COMUNICADO = c.ID_COMUNICADO,
                        ID_EMPLEADO = c.ID_EMPLEADO,
                        NOMBRE = c.NOMBRE,
                        FE_COMENTARIO = c.FE_COMENTARIO,
                        DS_COMENTARIO = c.DS_COMENTARIO,
                        FG_PRIVADO = c.FG_PRIVADO
                    }).ToList();
        }

        #endregion

        #region ACTUALIZAR COMUNICADO NO LEIDO A LEIDO

        public E_RESULTADO ActualizarComunicadoNoLeido(int ID_COMUNICADO, string ID_EMPLEADO, string pClUsuario, string pNbPrograma)
        {
            VisorComunicadoOperaciones operaciones = new VisorComunicadoOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.ActualizarComunicadoNoLeido(ID_COMUNICADO, ID_EMPLEADO, pClUsuario, pNbPrograma));
        }

        #endregion

        #region ACTUALIZAR COMUNICADO LEIDO A NO LEIDO

        public E_RESULTADO ActualizarComunicadoLeido(int ID_COMUNICADO, string ID_EMPLEADO, string pClUsuario, string pNbPrograma)
        {
            VisorComunicadoOperaciones operaciones = new VisorComunicadoOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.ActualizarComunicadoLeido(ID_COMUNICADO, ID_EMPLEADO, pClUsuario, pNbPrograma));
        }

        #endregion

        #region INSERTAR COMENTARIO A COMUNICADO

        public E_RESULTADO InsertarComentarioComunicado(int ID_COMUNICADO, string CL_USUARIO, string DS_COMENTARIO, string pClUsuario, string pNbPrograma)
        {
            VisorComunicadoOperaciones operaciones = new VisorComunicadoOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertarComentarioComunicado(ID_COMUNICADO, CL_USUARIO, DS_COMENTARIO, pClUsuario, pNbPrograma));
        }

        #endregion

        public List<SPE_OBTIENE_EMPLEADOS_COMUNICADO_INFORMACION_Result> ObtenerEmpleadosInformacion(int idComunicado, string id_empleado)
        {
            VisorComunicadoOperaciones operaciones = new VisorComunicadoOperaciones();
            return operaciones.ObtenerEmpleadosInformacion(idComunicado, id_empleado).ToList();
        }
        public List<SPE_OBTIENE_LISTA_EMPLEADOS_INFORMACION_Result> ObtenerListaEmpleadosInformacion(int idComunicado)
        {
            VisorComunicadoOperaciones operaciones = new VisorComunicadoOperaciones();
            return operaciones.ObtenerListaEmpleadosInformacion(idComunicado).ToList();
        }
       

    }
}
