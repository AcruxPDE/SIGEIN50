using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.Administracion
{
    public class PlazaOperaciones
    {
        SistemaSigeinEntities contexto;

        public List<E_PLAZA> ObtenerPlazas(int? pIdPlaza, XElement pXmlSeleccion, int? pID_EMPRESA, int? pID_ROL = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                if (pXmlSeleccion == null)
                    pXmlSeleccion = new XElement("SELECCION", new XAttribute("CL_TIPO", "ACTIVAS_INACTIVAS"));

                return contexto.Database.SqlQuery<E_PLAZA>("EXEC " +
                    "[ADM].[SPE_OBTIENE_PLAZAS] " +
                    "@PIN_ID_PLAZA, " +
                    "@PIN_XML_SELECCION, " +
                    "@PIN_ID_EMPRESA, " +
                    "@PIN_ID_ROL "
                    , new SqlParameter("@PIN_ID_PLAZA", (object)pIdPlaza ?? DBNull.Value)
                    , new SqlParameter("@PIN_XML_SELECCION", (object)pXmlSeleccion.ToString() ?? DBNull.Value)
                    , new SqlParameter("@PIN_ID_EMPRESA", (object)pID_EMPRESA ?? DBNull.Value)
                    , new SqlParameter("@PIN_ID_ROL", (object)pID_ROL ?? DBNull.Value)
                ).ToList();


                //return contexto.SPE_OBTIENE_PLAZAS(pIdPlaza, pXmlSeleccion.ToString(), pID_EMPRESA, pID_ROL).ToList();
            }
        }

        public XElement InsertarActualizarPlaza(E_TIPO_OPERACION_DB pClTipoOperacion, Entidades.Administracion.E_PLAZA pPlaza, string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                var pXmlResultado = new SqlParameter("@XML_RESULTADO", SqlDbType.Xml)
                {
                    Direction = ParameterDirection.Output
                };

                contexto.Database.ExecuteSqlCommand("EXEC " +
                    "ADM.SPE_INSERTA_ACTUALIZA_PLAZA " +
                    "@XML_RESULTADO OUTPUT, " +
                    "@PIN_ID_PLAZA, " +
                    "@PIN_CL_PLAZA, " +
                    "@PIN_NB_PLAZA, " +
                    "@PIN_ID_EMPLEADO, " +
                    "@PIN_ID_PUESTO, " +
                    "@PIN_ID_DEPARTAMENTO, " +
                    "@PIN_ID_PLAZA_JEFE, " +
                    "@PIN_ID_EMPRESA, " +
                    "@PIN_FG_ACTIVO, " +
                    "@PIN_XML_GRUPOS, " +
                    "@PIN_XML_PLAZAS_INTERRELACIONADAS, " +
                    "@PIN_CL_USUARIO, " +
                    "@PIN_NB_PROGRAMA, " +
                    "@PIN_TIPO_TRANSACCION "
                    , pXmlResultado,
                    new SqlParameter("@PIN_ID_PLAZA", (object)pPlaza.ID_PLAZA ?? DBNull.Value),
                    new SqlParameter("@PIN_CL_PLAZA", (object)pPlaza.CL_PLAZA),
                    new SqlParameter("@PIN_NB_PLAZA", (object)pPlaza.NB_PLAZA),
                    new SqlParameter("@PIN_ID_EMPLEADO", (object)pPlaza.ID_EMPLEADO ?? DBNull.Value),
                    new SqlParameter("@PIN_ID_PUESTO", (object)pPlaza.ID_PUESTO ?? DBNull.Value),
                    new SqlParameter("@PIN_ID_DEPARTAMENTO", (object)pPlaza.ID_DEPARTAMENTO ?? DBNull.Value),
                    new SqlParameter("@PIN_ID_PLAZA_JEFE", (object)pPlaza.ID_PLAZA_SUPERIOR ?? DBNull.Value),
                    new SqlParameter("@PIN_ID_EMPRESA", (object)pPlaza.ID_EMPRESA ?? DBNull.Value),
                    new SqlParameter("@PIN_FG_ACTIVO", (object)pPlaza.FG_ACTIVO),
                    new SqlParameter("@PIN_XML_GRUPOS", (object)pPlaza.XML_GRUPOS),
                    new SqlParameter("@PIN_XML_PLAZAS_INTERRELACIONADAS", (object)pPlaza.XML_PLAZAS_INTERRELACIONADAS),
                    new SqlParameter("@PIN_CL_USUARIO", (object)pClUsuario),
                    new SqlParameter("@PIN_NB_PROGRAMA", (object)pNbPrograma),
                    new SqlParameter("@PIN_TIPO_TRANSACCION", (object)pClTipoOperacion.ToString())
                );

                return XElement.Parse(pXmlResultado.Value.ToString()); 
            }

            //using (contexto = new SistemaSigeinEntities())
            //{
            //    ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
            //    contexto.SPE_INSERTA_ACTUALIZA_PLAZA(pOutClRetorno, pPlaza.ID_PLAZA, pPlaza.CL_PLAZA, pPlaza.NB_PLAZA, pPlaza.ID_EMPLEADO, pPlaza.ID_PUESTO,pPlaza.ID_DEPARTAMENTO, pPlaza.ID_PLAZA_SUPERIOR, pPlaza.ID_EMPRESA, pPlaza.FG_ACTIVO, pPlaza.XML_GRUPOS, pClUsuario, pNbPrograma, pClTipoOperacion.ToString());
            //    return XElement.Parse(pOutClRetorno.Value.ToString());
            //}
        }

        public XElement EliminarPlaza(int pIdPlaza, string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ELIMINA_PLAZA(pOutClRetorno, pIdPlaza, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }
    }
}
