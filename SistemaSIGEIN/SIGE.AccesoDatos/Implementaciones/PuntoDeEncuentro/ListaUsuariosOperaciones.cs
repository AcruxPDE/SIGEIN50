using SIGE.Entidades;
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
    public class ListaUsuariosOperaciones
    {
        private SistemaSigeinEntities context;
        #region INSERTA PROCESOS
        public XElement InsertaProcesos(Guid ID_USUARIO_FUNCION, string CL_USUARIO_PROCESO, bool FG_COMUNICADOS , bool FG_TRAMITES, bool FG_COMPROMISOS, bool FG_NOMINA, string pCLusuario, string pNBprograma, string TIPO_TRANSACCION)
        {
            using (context = new SistemaSigeinEntities())
            {
                var pXmlResultado = new SqlParameter("@XML_RESULTADO", SqlDbType.Xml)
                {
                    Direction = ParameterDirection.Output
                };

                context.Database.ExecuteSqlCommand("EXEC " +
                    "PDE.SPE_INSERTA_PROCESOS " +
                    "@XML_RESULTADO OUTPUT, " +
                    "@PIN_ID_USUARIO_FUNCION, " +
                    "@PIN_CL_USUARIO_PROCESO, " +
                    "@PIN_FG_COMUNICADOS, " +
                    "@PIN_FG_TRAMITES, " +
                    "@PIN_FG_COMPROMISOS, " +
                    "@PIN_FG_NOMINA, " +
                    "@PIN_CL_USUARIO, " +
                    "@PIN_NB_PROGRAMA, " +
                    "@PIN_TIPO_TRANSACCION "
                    , pXmlResultado
                    , new SqlParameter("@PIN_ID_USUARIO_FUNCION", (object)ID_USUARIO_FUNCION ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_USUARIO_PROCESO", (object)CL_USUARIO_PROCESO ?? DBNull.Value)
                    , new SqlParameter("@PIN_FG_COMUNICADOS", (object)FG_COMUNICADOS ?? DBNull.Value)
                    , new SqlParameter("@PIN_FG_TRAMITES", (object)FG_TRAMITES ?? DBNull.Value)
                    , new SqlParameter("@PIN_FG_COMPROMISOS", (object)FG_COMPROMISOS ?? DBNull.Value)
                    , new SqlParameter("@PIN_FG_NOMINA", (object)FG_NOMINA ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_USUARIO", (object)pCLusuario ?? DBNull.Value)
                    , new SqlParameter("@PIN_NB_PROGRAMA", (object)pNBprograma ?? DBNull.Value)
                    , new SqlParameter("@PIN_TIPO_TRANSACCION", (object)TIPO_TRANSACCION ?? DBNull.Value)

                );

                return XElement.Parse(pXmlResultado.Value.ToString());



            }




        }

        #endregion

    }
}
