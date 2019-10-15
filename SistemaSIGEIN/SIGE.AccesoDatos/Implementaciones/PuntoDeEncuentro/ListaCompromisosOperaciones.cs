using SIGE.Entidades;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Entidades.PuntoDeEncuentro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.PuntoDeEncuentro
{
    
    public class ListaCompromisosOperaciones
    {
        private SistemaSigeinEntities context;
        #region OBTIENE CATALOGO CALIFICACIONES
        public List<E_CALIFICACION> ObtieneCatalogoCalificaciones()
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.Database.SqlQuery<E_CALIFICACION>("EXEC " +
                    "PDE.SPE_OBTIENE_CATALOGO_CALIFICACIONES "
                ).ToList();
            }
        }

        #endregion

        #region OBTIENE CATALOGO PRIORIDADES
        public List<E_PRIORIDAD> ObtieneCatalogoPrioridad()
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.Database.SqlQuery<E_PRIORIDAD>("EXEC " +
                    "PDE.SPE_OBTIENE_CATALOGO_PRIORIDADES "
                    ).ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA  COMPROMISO

        public XElement InsertaActualizaCompromiso(Guid ID_COMPROMISO, string CL_COMPROMISO, string NB_COMPROMISO, int ID_TIPO_COMPROMISO, Guid ID_PRIORIDAD, Guid ID_ESTATUS_COMPROMISO, Guid ID_CALIFICACION, DateTime FE_ENTREGA, DateTime FE_NEGOCIABLE,  bool FG_ACTIVO, string CL_USUARIO_ASIGNADO, string pCLusuario, string pNBprograma, string TIPO_TRANSACCION)
        {
            using (context = new SistemaSigeinEntities())
            {
                var pXmlResultado = new SqlParameter("@XML_RESULTADO", SqlDbType.Xml)
                {
                    Direction = ParameterDirection.Output
                };

                context.Database.ExecuteSqlCommand("EXEC " +
                    "PDE.SPE_INSERTA_ACTUALIZA_COMPROMISO " +
                    "@XML_RESULTADO OUTPUT, " +
                    "@PIN_ID_COMPROMISO, " +        
                    "@PIN_CL_COMPROMISO, " +
                    "@PIN_NB_COMPROMISO, " +
                    "@PIN_ID_TIPO_COMPROMISO, " +
                    "@PIN_ID_PRIORIDAD, " +
                    "@PIN_ID_ESTATUS_COMPROMISO, " +
                    "@PIN_ID_CALIFICACION, " +
                    "@PIN_FE_ENTREGA, " +
                    "@PIN_FE_NEGOCIABLE, " +
                    "@PIN_FG_ACTIVO, " +
                    "@PIN_CL_USUARIO_ASIGNADO, " +
                    "@PIN_CL_USUARIO, " +
                    "@PIN_NB_PROGRAMA, " +
                    "@PIN_TIPO_TRANSACCION "
                    , pXmlResultado
                    , new SqlParameter("@PIN_ID_COMPROMISO", (object)ID_COMPROMISO ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_COMPROMISO", (object)CL_COMPROMISO ?? DBNull.Value)
                    , new SqlParameter("@PIN_NB_COMPROMISO", (object)NB_COMPROMISO ?? DBNull.Value)
                    , new SqlParameter("@PIN_ID_TIPO_COMPROMISO", (object)ID_TIPO_COMPROMISO ?? DBNull.Value)
                    , new SqlParameter("@PIN_ID_PRIORIDAD", (object)ID_PRIORIDAD ?? DBNull.Value)
                    , new SqlParameter("@PIN_ID_ESTATUS_COMPROMISO", (object)ID_ESTATUS_COMPROMISO ?? DBNull.Value)
                    , new SqlParameter("@PIN_ID_CALIFICACION", (object)ID_CALIFICACION ?? DBNull.Value)
                    , new SqlParameter("@PIN_FE_ENTREGA", (object)FE_ENTREGA ?? DBNull.Value)
                    , new SqlParameter("@PIN_FE_NEGOCIABLE", (object)FE_NEGOCIABLE ?? DBNull.Value)
                    , new SqlParameter("@PIN_FG_ACTIVO", (object)FG_ACTIVO ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_USUARIO_ASIGNADO", (object)CL_USUARIO_ASIGNADO ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_USUARIO", (object)pCLusuario ?? DBNull.Value)
                    , new SqlParameter("@PIN_NB_PROGRAMA", (object)pNBprograma ?? DBNull.Value)
                    , new SqlParameter("@PIN_TIPO_TRANSACCION", (object)TIPO_TRANSACCION ?? DBNull.Value)
       
                );

                return XElement.Parse(pXmlResultado.Value.ToString());


                
            }


    

        }

        #endregion

        #region OBTIENE MIS COMPROMISOS

        public List<E_OBTIENE_MIS_COMPROMISOS> ObtenerMisCompromisos(Guid? ID_COMPROMISO, string CL_COMPROMISO, string CL_USUARIO)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.Database.SqlQuery<E_OBTIENE_MIS_COMPROMISOS>("EXEC " +
                    "PDE.SPE_OBTIENE_MIS_COMPROMISOS " +
                    "@PIN_ID_COMPROMISO, " +
                    "@PIN_CL_COMPROMISO, " +
                    "@PIN_CL_USUARIO "
                    , new SqlParameter("@PIN_ID_COMPROMISO", (object)ID_COMPROMISO ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_COMPROMISO", (object)CL_COMPROMISO ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_USUARIO", (object)CL_USUARIO ?? DBNull.Value)
                ).ToList();
            }
        }

        #endregion

        #region OBTIENE MIS COMPROMISOS SOLITADOS

        public List<E_OBTIENE_MIS_COMPROMISOS_SOLICITADOS> ObtenerMisCompromisosSolicitados(Guid? ID_COMPROMISO, string CL_COMPROMISO, string CL_USUARIO)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.Database.SqlQuery<E_OBTIENE_MIS_COMPROMISOS_SOLICITADOS>("EXEC " +
                    "PDE.SPE_OBTIENE_MIS_COMPROMISOS_SOLITADOS " +
                    "@PIN_ID_COMPROMISO, " +
                    "@PIN_CL_COMPROMISO, " +
                    "@PIN_CL_USUARIO "
                    , new SqlParameter("@PIN_ID_COMPROMISO", (object)ID_COMPROMISO ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_COMPROMISO", (object)CL_COMPROMISO ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_USUARIO", (object)CL_USUARIO ?? DBNull.Value)
                ).ToList();
            }
        }
        #endregion

        #region OBTIENE MIS TAREAS
        public List<E_OBTIENE_MIS_TAREAS> ObtenerMisTareas(Guid? ID_COMPROMISO, string CL_COMPROMISO, string CL_USUARIO)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.Database.SqlQuery<E_OBTIENE_MIS_TAREAS>("EXEC " +
                    "PDE.SPE_OBTIENE_MIS_TAREAS " +
                    "@PIN_ID_COMPROMISO, " +
                    "@PIN_CL_COMPROMISO, " +
                    "@PIN_CL_USUARIO "
                    , new SqlParameter("@PIN_ID_COMPROMISO", (object)ID_COMPROMISO ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_COMPROMISO", (object)CL_COMPROMISO ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_USUARIO", (object)CL_USUARIO ?? DBNull.Value)
                ).ToList();
            }
        }

        #endregion

        #region OBTIENE MIS REPORTES
        public List<E_OBTIENE_MIS_REPORTES> ObtenerMisReportes(Guid? ID_COMPROMISO, string CL_COMPROMISO, string CL_USUARIO)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.Database.SqlQuery<E_OBTIENE_MIS_REPORTES>("EXEC " +
                    "PDE.SPE_OBTIENE_MIS_REPORTES " +
                    "@PIN_ID_COMPROMISO, " +
                    "@PIN_CL_COMPROMISO, " +
                    "@PIN_CL_USUARIO "
                    , new SqlParameter("@PIN_ID_COMPROMISO", (object)ID_COMPROMISO ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_COMPROMISO", (object)CL_COMPROMISO ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_USUARIO", (object)CL_USUARIO ?? DBNull.Value)
                ).ToList();
            }
        }
        #endregion

        #region OBTIENE CATALOGO TIPOS DE COMPROMIOS
        public List<E_TIPOCOMPROMISO> ObtieneCatalogoTipoDeCompromisos()
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.Database.SqlQuery<E_TIPOCOMPROMISO>("EXEC " +
                    "PDE.SPE_OBTIENE_CATALOGO_TIPODECOMPROMISO "
                    ).ToList();
            }
        }
        #endregion
    }
}
