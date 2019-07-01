using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.Administracion
{
    public class CamposNominaOperaciones
    {
        private SistemaSigeinEntities contexto;

        public List<SPE_OBTIENE_CONFIGURACION_CAMPOS_NOMINA_DO_Result> ObtenerConfiguracionCampo(int? pID_CAMPO = null, string pCL_CAMPO = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_CONFIGURACION_CAMPOS_NOMINA_DO(pID_CAMPO, pCL_CAMPO).ToList();
            }
        }

        public XElement InsertaActualizaConfiguracionCampos(string pXML_CONFIGURACION = null, string pClUsuario = null, string pNbPrograma = null, string pClTipoTransaccion = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_ACTUALIZA_CAMPO_NOMINA_DO(pOutClRetorno, pXML_CONFIGURACION, pClUsuario, pNbPrograma, pClTipoTransaccion);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement InsertaActualizaPuesto(int? pID_PUESTO = null, string pCL_PUESTO = null, string pNB_PUESTO = null, bool? pFG_NOMINA = null, bool? pFG_DO = null, string pClUsuario = null, string pNbPrograma = null, string pClTipoTransaccion = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_ACTUALIZA_PUESTO_NOMINA_DO(pOutClRetorno, pID_PUESTO, pCL_PUESTO, pNB_PUESTO, pFG_NOMINA, pFG_DO, pClUsuario, pNbPrograma, pClTipoTransaccion);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_CENTROS_OPERATIVOS_Result> ObtieneCentrosOptvos(System.Guid? pID_CENTRO_OPTVO = null, string pCL_CENTRO_OPTVO = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_CENTROS_OPERATIVOS(pID_CENTRO_OPTVO, pCL_CENTRO_OPTVO).ToList();
            }
        }

        public List<SPE_OBTIENE_CENTROS_ADMINISTRATIVOS_Result> ObtieneCentrosAdmvo(System.Guid? pID_CENTRO_ADMVO = null, string pCL_CENTRO_ADMVO = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_CENTROS_ADMINISTRATIVOS(pID_CENTRO_ADMVO, pCL_CENTRO_ADMVO).ToList();
            }
        }

        public List<SPE_OBTIENE_PUESTOS_NOMINA_DO_Result> ObtienePuestosNominaDo(int? pID_PUESTO_NOMINA_DO = null, string pCL_PUESTO = null, bool? pFG_DO = null, bool? FG_NOMINA = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_PUESTOS_NOMINA_DO(pID_PUESTO_NOMINA_DO, pCL_PUESTO, pFG_DO, FG_NOMINA).ToList();
            }
        }

        public XElement EliminaPuestoNominaDO(int? pID_PUESTO = null, string pCL_PUESTO = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ELIMINA_PUESTO_NOMINA_DO(pOutClRetorno, pID_PUESTO, pCL_PUESTO);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement InsertaActualizaEmpleado(int? pID_EMPLEADO = null, SPE_OBTIENE_EMPLEADOS_NOMINA_DO_Result pEmpleado = null, string pClUsuario = null, string pNbPrograma = null, string pClTipoTransaccion = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_ACTUALIZA_EMPLEADO_NOMINA_DO(pOutClRetorno, pID_EMPLEADO, pEmpleado.M_EMPLEADO_CL_EMPLEADO, pEmpleado.NB_EMPLEADO, pEmpleado.NB_APELLIDO_PATERNO, pEmpleado.NB_APELLIDO_MATERNO, pEmpleado.FG_DO, pEmpleado.FG_NOMINA, pEmpleado.FG_NOMINA_DO, pEmpleado.ID_PLAZA, pEmpleado.ID_PUESTO_NOMINA, pEmpleado.CL_EMPLEADO_NOMINA, pEmpleado.ID_RAZON_SOCIAL, pEmpleado.SUELDO_MENSUAL, pEmpleado.SUELDO_DIARIO, pEmpleado.BASE_COTIZACION, pEmpleado.SUELDO_DO, pEmpleado.FG_SUELDO_VISIBLE_INVENTARIO, pEmpleado.FG_SUELDO_VISIBLE_TABULADOR, pEmpleado.FG_SUELDO_VISIBLE_BONO, pClUsuario, pNbPrograma, pClTipoTransaccion);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_EMPLEADOS_NOMINA_DO_Result> ObtieneEmpleadosNominaDo(int? pID_EMPLEADO_NOMINA_DO = null, string pCL_EMPLEADO = null, System.Guid? pID_EMPLEADO_NOMINA = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_EMPLEADOS_NOMINA_DO(pID_EMPLEADO_NOMINA_DO, pCL_EMPLEADO, pID_EMPLEADO_NOMINA).ToList();
            }
        }

        public List<E_PERSONAL> ObtieneEmpleadosGenerales(string pCL_EMPLEADO = null, bool? pFG_ACTIVO = null, int? pID_EMPRESA = null, int? pID_ROL = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<E_PERSONAL>("EXEC " +
                    "ADM.SPE_OBTIENE_EMPLEADOS_GENERAL " +
                    "@PIN_CL_EMPLEADO, " +
                    "@PIN_FG_ACTIVO, " +
                    "@PIN_ID_EMPRESA, " +
                    "@PIN_ID_ROL ",
                    new SqlParameter("@PIN_CL_EMPLEADO", (object)pCL_EMPLEADO ?? DBNull.Value),
                    new SqlParameter("@PIN_FG_ACTIVO", (object)pFG_ACTIVO ?? DBNull.Value),
                    new SqlParameter("@PIN_ID_EMPRESA", (object)pID_EMPRESA ?? DBNull.Value),
                    new SqlParameter("@PIN_ID_ROL", (object)pID_ROL ?? DBNull.Value)
                ).ToList();

                //return contexto.SPE_OBTIENE_EMPLEADOS_GENERAL(pCL_EMPLEADO, pFG_ACTIVO, pID_EMPRESA, pID_ROL).ToList();
            }
        }

        public XElement EliminaEmpleadoNominaDO(int? pID_EMPLEADO = null, string pCL_EMPLEADO = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ELIMINA_EMPLEADO_NOMINA_DO(pOutClRetorno, pID_EMPLEADO, pCL_EMPLEADO);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_RAZON_SOCIAL_NOMINA_Result> ObtenerRazonSocialNomina(System.Guid? pID_RAZON_SOCIAL = null, string pCL_CLIENTE = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_RAZON_SOCIAL_NOMINA(pID_RAZON_SOCIAL, pCL_CLIENTE).ToList();
            }
        }

    }
}
