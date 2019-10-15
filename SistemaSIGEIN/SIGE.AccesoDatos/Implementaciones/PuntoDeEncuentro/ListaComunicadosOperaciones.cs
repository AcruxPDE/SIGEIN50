using SIGE.Entidades;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Entidades.PuntoDeEncuentro;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.PuntoDeEncuentro
{
    public class ListaComunicadosOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTENER COMUNICADOS
        public List<SPE_OBTIENE_K_ADM_COMUNICADO_Result> ObtenerComunicados(int? ID_COMUNICADO = null, string NB_COMUNICADO = null, DateTime? FE_COMUNICADO = null, string DS_COMUNICADO = null, DateTime? FE_VISIBLE_DEL = null, DateTime? FE_VISIBLE_AL = null, int? ID_ARCHIVO_PDE = null, byte? FG_PRIVADO = null, bool? FG_ESTATUS = null, string NB_ARCHIVO = null, byte[] FI_ARCHIVO = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_K_ADM_COMUNICADO(ID_COMUNICADO, NB_COMUNICADO, FE_COMUNICADO, DS_COMUNICADO, FE_VISIBLE_DEL, FE_VISIBLE_AL, ID_ARCHIVO_PDE, FG_PRIVADO, FG_ESTATUS, NB_ARCHIVO).ToList();
            }
        }
        #endregion

        public List<SPE_OBTIENE_K_COMUNICADO_LEIDO_Result> ObtenerEmpleadosComunicadosLeidos(int? IdComunicado)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_K_COMUNICADO_LEIDO(IdComunicado).ToList();
            }
        }


        #region OBTENER EMPLEADOS COMUNICADO
        public List<SPE_OBTIENE_K_EMPLEADOS_COMUNICADO_Result> ObtenerEmpleadosComunicado(int? ID_COMUNICADO = null, string NB_EMPLEADO = null,  string NB_PUESTO = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_K_EMPLEADOS_COMUNICADO(ID_COMUNICADO).ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA COMUNICADO

        public XElement InsertaActualizaComunicado(int ID_ARCHIVO_PDE, string NB_ARCHIVO, byte[] FI_ARCHIVO, int ID_COMUNICADO, string NB_COMUNICADO, string DS_COMUNICADO, DateTime FECHA_DEL, DateTime FECHA_AL, string XML_EMPLEADOS, byte? FG_PRIVADO, string pCLusuario, string pNBprograma, string TIPO_TRANSACCION)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_K_COMUNICADO(pOutClRetorno, ID_ARCHIVO_PDE, NB_ARCHIVO, FI_ARCHIVO, ID_COMUNICADO, NB_COMUNICADO, DS_COMUNICADO, FECHA_DEL, FECHA_AL, FG_PRIVADO, XML_EMPLEADOS, pCLusuario, pNBprograma, TIPO_TRANSACCION);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }


        public XElement InsertaActualizaComunicadoInformacion(int ID_ARCHIVO_PDE, string NB_ARCHIVO, byte[] FI_ARCHIVO, int ID_COMUNICADO, string NB_COMUNICADO, string DS_COMUNICADO, DateTime FECHA_DEL, DateTime FECHA_AL, string XML_EMPLEADOS, byte? FG_PRIVADO, string pCLusuario, string pNBprograma, string TIPO_TRANSACCION,string TIPO_COMUNICADO,string TIPO_ACCION,string XML_EMPLEADOSINFORMACION)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_K_COMUNICADO_INFORMACION(pOutClRetorno, ID_ARCHIVO_PDE, NB_ARCHIVO, FI_ARCHIVO, ID_COMUNICADO, NB_COMUNICADO, DS_COMUNICADO, FECHA_DEL, FECHA_AL, XML_EMPLEADOS,FG_PRIVADO, pCLusuario, pNBprograma, TIPO_TRANSACCION, TIPO_COMUNICADO, TIPO_ACCION, XML_EMPLEADOSINFORMACION);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public string ValidaExisteComunicado(string ID_EMPLEADO, string TIPO_COMUNICADO, string TIPO_ACCION)
        {

            using (context = new SistemaSigeinEntities())
            {
                return context.proc_valida_comunicado_informacion(ID_EMPLEADO, TIPO_COMUNICADO, TIPO_ACCION).FirstOrDefault();
            }
        }


        public string ValidaPuestoAsignado(string ID_PUESTO)
        {

            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_VALIDA_DESCRIPTIVO_ASIGANDO(ID_PUESTO).FirstOrDefault();
            }
        }

        public string ValidaEmpleadoAsignado(string ID_EMPLEADO)
        {

            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_VALIDA_EMPLEADO_ASIGNADO(ID_EMPLEADO).FirstOrDefault();
            }
        }

        #endregion

        #region ELIMINA COMUNICADO
        public XElement EliminaComunicado(int? ID_COMUNICADO = null, string pCLusuario = null, string pNBprograma = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_K_COMUNICADO(pout_clave_retorno, ID_COMUNICADO, pCLusuario, pNBprograma);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        #endregion

        
    }
}
