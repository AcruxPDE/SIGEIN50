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
using System.Xml.Linq;
using E_PRIORIDAD = SIGE.Entidades.PuntoDeEncuentro.E_PRIORIDAD;


namespace SIGE.Negocio.PuntoDeEncuentro
{
    public class ListaCompromisosNegocios
    {
        #region OBTIENE LAS CALIFICACIONES
        public List<E_CALIFICACION> ObtieneCatalogoCalificaciones()
        {
            ListaCompromisosOperaciones operaciones = new ListaCompromisosOperaciones();
            return operaciones.ObtieneCatalogoCalificaciones();
        }
        #endregion

        #region OBTIENE LAS PRIORIDADES
        public List<E_PRIORIDAD> ObtieneCatalogoPrioridad()
        {
            ListaCompromisosOperaciones operaciones = new ListaCompromisosOperaciones();
            return operaciones.ObtieneCatalogoPrioridad();
        }
        #endregion

        #region INSERTAR ACTUALIZA COMPROMISO

        public E_RESULTADO InsertaActualizaCompromiso(E_COMPROMISO COMPROMISO ,string pCLusuario, string pNBprograma, string TIPO_TRANSACCION)
        {
            ListaCompromisosOperaciones operaciones = new ListaCompromisosOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualizaCompromiso( COMPROMISO.ID_COMPROMISO, COMPROMISO.CL_COMPROMISO, COMPROMISO.NB_COMPROMISO, COMPROMISO.ID_TIPO_COMPROMISO, COMPROMISO.ID_PRIORIDAD, COMPROMISO.ID_ESTATUS_COMPROMISO ,COMPROMISO.ID_CALIFICACION, COMPROMISO.FE_ENTREGA ,COMPROMISO.FE_NEGOCIABLE, COMPROMISO.FG_ACTIVO, COMPROMISO.CL_USUARIO_ASIGNADO, pCLusuario, pNBprograma, TIPO_TRANSACCION));

        }

        #endregion

        #region OBTIENE MIS COMPROMISOS
        public List<E_OBTIENE_MIS_COMPROMISOS> ObtenerMisCompromisos(Guid ? ID_COMPROMISO, string CL_COMPROMISO, string CL_USUARIO)
        {
            ListaCompromisosOperaciones operaciones = new ListaCompromisosOperaciones();
            return operaciones.ObtenerMisCompromisos(ID_COMPROMISO, CL_COMPROMISO, CL_USUARIO);
            
        }

        #endregion

        #region OBTIENE MIS COMPROMISOS SOLICITADOS

        public List<E_OBTIENE_MIS_COMPROMISOS_SOLICITADOS> ObtenerMisCompromisosSolicitados(Guid? ID_COMPROMISO, string CL_COMPROMISO, string CL_USUARIO)
        {
            ListaCompromisosOperaciones operaciones = new ListaCompromisosOperaciones();
            return operaciones.ObtenerMisCompromisosSolicitados(ID_COMPROMISO, CL_COMPROMISO, CL_USUARIO);

        }
        #endregion

        #region OBTIENE MIS TAREAS
        public List<E_OBTIENE_MIS_TAREAS> ObtenerMisTareas(Guid? ID_COMPROMISO, string CL_COMPROMISO, string CL_USUARIO)
        {
            ListaCompromisosOperaciones operaciones = new ListaCompromisosOperaciones();
            return operaciones.ObtenerMisTareas(ID_COMPROMISO, CL_COMPROMISO, CL_USUARIO);

        }
        #endregion

        #region OBTIENE MIS REPORTES
        public List<E_OBTIENE_MIS_REPORTES> ObtenerMisReportes(Guid? ID_COMPROMISO, string CL_COMPROMISO, string CL_USUARIO)
        {
            ListaCompromisosOperaciones operaciones = new ListaCompromisosOperaciones();
            return operaciones.ObtenerMisReportes(ID_COMPROMISO, CL_COMPROMISO, CL_USUARIO);

        }
        #endregion

        #region OBTIENE LOS TIPOS DE COMPROMISOS
        public List<E_TIPOCOMPROMISO> ObtieneCatalogoTipoDeCompromisos()
        {
            ListaCompromisosOperaciones operaciones = new ListaCompromisosOperaciones();
            return operaciones.ObtieneCatalogoTipoDeCompromisos();
        }
        #endregion
    }


}
