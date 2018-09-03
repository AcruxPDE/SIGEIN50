using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades.PuntoDeEncuentro;
using SIGE.Entidades;
using System.Data.Objects;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;
namespace SIGE.AccesoDatos.Implementaciones.PuntoDeEncuentro
{
    public class VisorComunicadoOperaciones
    {
        private SistemaSigeinEntities context;

        #region OBTIENE COMUNICADOS DEL EMPLEADO

        public List<SPE_OBTIENE_K_COMUNICADO_Result> ObtenerComunicados(int? ID_COMUNICADO = null, string ID_EMPLEADO = null, string NB_COMUNICADO = null, DateTime? FE_COMUNICADO = null, string DS_COMUNICADO = null, DateTime? FE_VISIBLE_DEL = null, DateTime? FE_VISIBLE_AL = null, int? ID_ARCHIVO_PDE = null, DateTime? FE_CREACION = null, bool? FG_LEIDO = null, bool? FG_ESTATUS = null, byte? FG_PRIVADO = null, string NB_ARCHIVO = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_K_COMUNICADO(ID_COMUNICADO, ID_EMPLEADO, NB_COMUNICADO, FE_COMUNICADO, DS_COMUNICADO, FE_VISIBLE_DEL, FE_VISIBLE_AL, ID_ARCHIVO_PDE, FG_LEIDO, FE_CREACION, FG_ESTATUS, FG_PRIVADO, NB_ARCHIVO).ToList();
            }
        }

        #endregion

        #region OBTIENE COMENTARIOS DEL COMUNICADO

        public List<SPE_OBTIENE_K_COMENTARIOS_COMUNICADO_Result> ObtenerComentarios_Comunicado(int? ID_COMENTARIO_COMUNICADO = null, int? ID_COMUNICADO = null, string ID_EMPLEADO = null, string NOMBRE = null, DateTime? FE_COMENTARIO = null, string DS_COMENTARIO = null, byte? FG_PRIVADO = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_K_COMENTARIOS_COMUNICADO(ID_COMENTARIO_COMUNICADO, ID_COMUNICADO, ID_EMPLEADO, FE_COMENTARIO, DS_COMENTARIO, FG_PRIVADO).ToList();
            }
        }

        #endregion

        #region ACTUALIZA COMUNICADO NO LEIDO A LEIDO

        public XElement ActualizarComunicadoNoLeido(int ID_COMUNICADO, string ID_EMPLEADO, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ACTUALIZA_EMPLEADO_COMUNICADO_NO_LEIDO(pOutClRetorno, ID_COMUNICADO, ID_EMPLEADO, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        #endregion

        #region ACTUALIZA COMUNICADO LEIOD A NO LEIDO

        public XElement ActualizarComunicadoLeido(int ID_COMUNICADO, string ID_EMPLEADO, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ACTUALIZA_EMPLEADO_COMUNICADO_LEIDO(pOutClRetorno, ID_COMUNICADO, ID_EMPLEADO, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        #endregion

        #region INSERTA  COMENTARIO A COMUNICADO

        public XElement InsertarComentarioComunicado(int ID_COMUNICADO, string CL_USUARIO, string DS_COMENTARIO, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_K_INSERTA_COMENTARIO_COMUNICADO(pOutClRetorno, ID_COMUNICADO, CL_USUARIO, DS_COMENTARIO, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        #endregion
      
        
        public List<SPE_OBTIENE_EMPLEADOS_COMUNICADO_INFORMACION_Result> ObtenerEmpleadosInformacion(int? ID_COMUNICADO = null, string ID_EMPLEADO = "")
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EMPLEADOS_COMUNICADO_INFORMACION(ID_COMUNICADO, ID_EMPLEADO).ToList();
            }
        }

        public List<SPE_OBTIENE_LISTA_EMPLEADOS_INFORMACION_Result> ObtenerListaEmpleadosInformacion(int? ID_COMUNICADO = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_LISTA_EMPLEADOS_INFORMACION(ID_COMUNICADO).ToList();
            }
        }

    }
}
