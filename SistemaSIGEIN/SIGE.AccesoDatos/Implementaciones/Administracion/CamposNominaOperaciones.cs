using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Data;
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

        public XElement InsertaActualizaEmpleado(int? pID_EMPLEADO = null, E_EMPLEADO_NOMINA_DO pEmpleado = null, string pClUsuario = null, string pNbPrograma = null, string pClTipoTransaccion = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                var pXmlResultado = new SqlParameter("@XML_RESULTADO", SqlDbType.Xml)
                {
                    Direction = ParameterDirection.Output
                };

                contexto.Database.ExecuteSqlCommand("EXEC " +
                    "ADM.SPE_INSERTA_ACTUALIZA_EMPLEADO_NOMINA_DO " +
                    "@XML_RESULTADO OUTPUT, " +
                    "@PIN_ID_EMPLEADO, " +
                    "@PIN_CL_EMPLEADO, " +
                    "@PIN_NB_EMPLEADO, " +
                    "@PIN_NB_APELLIDO_PATERNO, " +
                    "@PIN_NB_APELLIDO_MATERNO, " +
                    "@PIN_FG_NOMINA, " +
                    "@PIN_FG_DO, " +
                    "@PIN_FG_NOMINA_DO, " +
                    "@PIN_ID_PUESTO, " +
                    "@PIN_ID_PLAZA, " +
                    "@PIN_FG_SUELDO_NOMINA_DO, " +
                    "@PIN_CL_USUARIO, " +
                    "@PIN_NB_PROGRAMA, " +
                    "@PIN_TIPO_TRANSACCION "
                    , pXmlResultado
                    , new SqlParameter("@PIN_ID_EMPLEADO", (object)pID_EMPLEADO ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_EMPLEADO", (object)pEmpleado.CL_EMPLEADO ?? DBNull.Value)
                    , new SqlParameter("@PIN_NB_EMPLEADO", (object)pEmpleado.NB_EMPLEADO ?? DBNull.Value)
                    , new SqlParameter("@PIN_NB_APELLIDO_PATERNO", (object)pEmpleado.NB_APELLIDO_PATERNO ?? DBNull.Value)
                    , new SqlParameter("@PIN_NB_APELLIDO_MATERNO", (object)pEmpleado.NB_APELLIDO_MATERNO ?? DBNull.Value)
                    , new SqlParameter("@PIN_FG_NOMINA", (object)pEmpleado.FG_NOMINA ?? DBNull.Value)
                    , new SqlParameter("@PIN_FG_DO", (object)pEmpleado.FG_DO ?? DBNull.Value)
                    , new SqlParameter("@PIN_FG_NOMINA_DO", (object)pEmpleado.FG_NOMINA_DO ?? DBNull.Value)
                    , new SqlParameter("@PIN_ID_PUESTO", (object)pEmpleado.ID_PUESTO ?? DBNull.Value)
                    , new SqlParameter("@PIN_ID_PLAZA", (object)pEmpleado.ID_PLAZA ?? DBNull.Value)
                    , new SqlParameter("@PIN_FG_SUELDO_NOMINA_DO", (object)pEmpleado.FG_SUELDO_NOMINA_DO ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_USUARIO", (object)pClUsuario ?? DBNull.Value)
                    , new SqlParameter("@PIN_NB_PROGRAMA", (object)pNbPrograma ?? DBNull.Value)
                    , new SqlParameter("@PIN_TIPO_TRANSACCION", (object)pClTipoTransaccion ?? DBNull.Value)
                );
                return XElement.Parse(pXmlResultado.Value.ToString());

                //ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                //contexto.SPE_INSERTA_ACTUALIZA_EMPLEADO_NOMINA_DO(pOutClRetorno, pID_EMPLEADO, pEmpleado.M_EMPLEADO_CL_EMPLEADO, pEmpleado.NB_EMPLEADO, pEmpleado.NB_APELLIDO_PATERNO, pEmpleado.NB_APELLIDO_MATERNO, pEmpleado.FG_DO, pEmpleado.FG_NOMINA, pEmpleado.FG_NOMINA_DO, pEmpleado.ID_PLAZA, pEmpleado.ID_PUESTO_NOMINA, pEmpleado.CL_EMPLEADO_NOMINA, pEmpleado.ID_RAZON_SOCIAL, pEmpleado.SUELDO_MENSUAL, pEmpleado.SUELDO_DIARIO, pEmpleado.BASE_COTIZACION, pEmpleado.SUELDO_DO, pEmpleado.FG_SUELDO_VISIBLE_INVENTARIO, pEmpleado.FG_SUELDO_VISIBLE_TABULADOR, pEmpleado.FG_SUELDO_VISIBLE_BONO, pClUsuario, pNbPrograma, pClTipoTransaccion);
                //return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public List<E_EMPLEADO_NOMINA_DO> ObtienePersonalNominaDo(int? pID_EMPLEADO = null, string pCL_EMPLEADO = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<E_EMPLEADO_NOMINA_DO>("EXEC " +
                    "ADM.SPE_OBTIENE_PERSONAL_NOMINA_DO " +
                    "@PIN_ID_EMPLEADO, " +
                    "@PIN_CL_EMPLEADO ",
                    new SqlParameter("@PIN_ID_EMPLEADO", (object)pID_EMPLEADO ?? DBNull.Value),
                    new SqlParameter("@PIN_CL_EMPLEADO", (object)pCL_EMPLEADO ?? DBNull.Value)
                ).ToList();

                //return contexto.SPE_OBTIENE_EMPLEADOS_NOMINA_DO(pID_EMPLEADO_NOMINA_DO, pCL_EMPLEADO, pID_EMPLEADO_NOMINA).ToList();
            }
        }

        public List<SPE_OBTIENE_EMPLEADOS_NOMINA_DO_Result> ObtieneEmpleadosNominaDo(int? pID_EMPLEADO_NOMINA_DO = null, string pCL_EMPLEADO = null, System.Guid? pID_EMPLEADO = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_EMPLEADOS_NOMINA_DO(pID_EMPLEADO_NOMINA_DO, pCL_EMPLEADO, pID_EMPLEADO).ToList();
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

        public List<E_RAZON_SOCIAL> ObtieneRazonSocial(string CL_CLIENTE = null, bool? FG_ACTIVO = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<E_RAZON_SOCIAL>("EXEC " +
                    "ADM.SPE_OBTIENE_C_RAZON_SOCIAL " +
                    "@PIN_CL_CLIENTE, " +
                    "@PIN_FG_ACTIVO ",
                    new SqlParameter("@PIN_CL_CLIENTE", (object)CL_CLIENTE ?? DBNull.Value),
                    new SqlParameter("@PIN_FG_ACTIVO", (object)FG_ACTIVO ?? DBNull.Value)
                ).ToList();
            }
        }

        public List<E_REGISTRO_PATRONAL> ObtieneRegistroPatronal(Guid? ID_RAZON_SOCIAL = null, bool? FG_ACTIVO = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<E_REGISTRO_PATRONAL>("EXEC " +
                    "ADM.SPE_OBTIENE_C_REGISTRO_PATRONAL " +
                    "@PIN_ID_REGISTRO_PATRONAL, " +
                    "@PIN_FG_ACTIVO ",
                    new SqlParameter("@PIN_ID_REGISTRO_PATRONAL", (object)ID_RAZON_SOCIAL ?? DBNull.Value),
                    new SqlParameter("@PIN_FG_ACTIVO", (object)FG_ACTIVO ?? DBNull.Value)
                ).ToList();
            }
        }

        public List<E_TIPO_TRABAJO_SUA> ObtieneTipoTrabajoSUA(double? CL_TIPO_TRAB_SUA = null, string DS_TIPO_TRAB_SUA = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<E_TIPO_TRABAJO_SUA>("EXEC " +
                    "Nomina.SPE_OBTIENE_VW_TIPO_TRAB_SUA " +
                    "@PIN_CL_TIPO_TRAB_SUA, " +
                    "@PIN_DS_TIPO_TRAB_SUA ",
                    new SqlParameter("@PIN_CL_TIPO_TRAB_SUA", (object)CL_TIPO_TRAB_SUA ?? DBNull.Value),
                    new SqlParameter("@PIN_DS_TIPO_TRAB_SUA", (object)DS_TIPO_TRAB_SUA ?? DBNull.Value)
                ).ToList();
            }
        }

        public List<E_TIPO_JORNADA_SUA> ObtieneTipoJornadaSUA(double? CL_JORNADA_SUA = null, string DS_JORNADA_SUA = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<E_TIPO_JORNADA_SUA>("EXEC " +
                    "Nomina.SPE_OBTIENE_VW_TIPO_TRAB_SUA " +
                    "@PIN_CL_JORNADA_SUA, " +
                    "@PIN_DS_JORNADA_SUA ",
                    new SqlParameter("@PIN_CL_JORNADA_SUA", (object)CL_JORNADA_SUA ?? DBNull.Value),
                    new SqlParameter("@PIN_DS_JORNADA_SUA", (object)DS_JORNADA_SUA ?? DBNull.Value)
                ).ToList();
            }
        }

        public List<E_TIPO_CONTRATO_SAT> ObtieneTipoContratoSAT(string CL_TIPO_CONTRATO = null, string DS_TIPO_CONTRATO = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<E_TIPO_CONTRATO_SAT>("EXEC " +
                    "Nomina.SPE_OBTIENE_S_TIPO_CONTRATO_SAT " +
                    "@PIN_CL_TIPO_CONTRATO, " +
                    "@PIN_DS_TIPO_CONTRATO ",
                    new SqlParameter("@PIN_CL_TIPO_CONTRATO", (object)CL_TIPO_CONTRATO ?? DBNull.Value),
                    new SqlParameter("@PIN_DS_TIPO_CONTRATO", (object)DS_TIPO_CONTRATO ?? DBNull.Value)
                ).ToList();
            }
        }

        public List<E_TIPO_JORNADA_SAT> ObtieneTipoJornadaSAT(string CL_TIPO_JORNADA = null, string DS_TIPO_JORNADA = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<E_TIPO_JORNADA_SAT>("EXEC " +
                    "Nomina.SPE_OBTIENE_S_TIPO_JORNADA_SAT " +
                    "@PIN_CL_TIPO_JORNADA, " +
                    "@PIN_DS_TIPO_JORNADA ",
                    new SqlParameter("@PIN_CL_TIPO_JORNADA", (object)CL_TIPO_JORNADA ?? DBNull.Value),
                    new SqlParameter("@PIN_DS_TIPO_JORNADA", (object)DS_TIPO_JORNADA ?? DBNull.Value)
                ).ToList();
            }
        }

        public List<E_REGIMEN_SAT> ObtieneRegimenSAT()
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<E_REGIMEN_SAT>("EXEC " +
                    "Nomina.SPE_OBTIENE_S_REGIMEN_SAT "
                ).ToList();
            }
        }

        public List<E_TIPO_SALARIO_SUA> ObtieneTipoSalarioSUA()
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<E_TIPO_SALARIO_SUA>("EXEC " +
                    "Nomina.SPE_OBTIENE_VW_TIPO_SALARIO_SUA "
                ).ToList();
            }
        }

        public List<E_RIESGO_PUESTO> ObtieneRiesgoPuesto()
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<E_RIESGO_PUESTO>("EXEC " +
                    "Nomina.SPE_OBTIENE_S_RIESGO_PUESTO_SAT "
                ).ToList();
            }
        }

        public List<E_HORARIO_SEMANA> ObtieneHorarioSemana()
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<E_HORARIO_SEMANA>("EXEC " +
                    "Nomina.SPE_OBTIENE_C_HORARIO_SEMANA "
                ).ToList();
            }
        }

        public List<E_PAQUETE_PRESTACIONES> ObtienePaquetePrestaciones()
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<E_PAQUETE_PRESTACIONES>("EXEC " +
                    "Nomina.SPE_OBTIENE_C_PAQUETE_PRESTACIONES "
                ).ToList();
            }
        }

        public List<E_FORMATO_DISPERSION> ObtieneFormatoDispersion(string CL_FORMATO = null, string CL_TIPO_FORMATO = null, string NB_FORMATO = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<E_FORMATO_DISPERSION>("EXEC " +
                    "Nomina.SPE_OBTIENE_S_FORMATO " +
                    "@PIN_CL_FORMATO, " +
                    "@PIN_CL_TIPO_FORMATO, " +
                    "@PIN_NB_FORMATO ",
                    new SqlParameter("@PIN_CL_FORMATO", (object)CL_FORMATO ?? DBNull.Value),
                    new SqlParameter("@PIN_CL_TIPO_FORMATO", (object)CL_TIPO_FORMATO ?? DBNull.Value),
                    new SqlParameter("@PIN_NB_FORMATO", (object)NB_FORMATO ?? DBNull.Value)
                ).ToList();
            }
        }

        public List<E_TIPO_NOMINA> ObtieneTipoNomina(Guid? ID_TIPO_NOMINA = null, string CL_CLIENTE = null, string CL_TIPO_NOMINA = null, string NB_PERIODICIDAD = null, string CL_PERIODICIDAD = null, string DS_TIPO_NOMINA = null, bool? FG_ACTIVO = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<E_TIPO_NOMINA>("EXEC " +
                    "Nomina.SPE_OBTIENE_C_TIPO_NOMINA " +
                    "@PIN_ID_TIPO_NOMINA, " +
                    "@PIN_CL_CLIENTE, " +
                    "@PIN_CL_TIPO_NOMINA, " +
                    "@PIN_CL_PERIODICIDAD, " +
                    "@PIN_DS_TIPO_NOMINA, " +
                    "@PIN_FG_ACTIVO ",
                    new SqlParameter("@PIN_ID_TIPO_NOMINA", (object)ID_TIPO_NOMINA ?? DBNull.Value),
                    new SqlParameter("@PIN_CL_CLIENTE", (object)CL_CLIENTE ?? DBNull.Value),
                    new SqlParameter("@PIN_CL_TIPO_NOMINA", (object)CL_TIPO_NOMINA ?? DBNull.Value),
                    new SqlParameter("@PIN_CL_PERIODICIDAD", (object)CL_PERIODICIDAD ?? DBNull.Value),
                    new SqlParameter("@PIN_DS_TIPO_NOMINA", (object)DS_TIPO_NOMINA ?? DBNull.Value),
                    new SqlParameter("@PIN_FG_ACTIVO", (object)FG_ACTIVO ?? DBNull.Value)
                ).ToList();
            }
        }

        public List<E_FORMA_PAGO> ObtieneFormaPago(string CL_FORMA_PAGO = null, string NB_FORMA_PAGO = null, bool? FG_ACTIVO = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<E_FORMA_PAGO>("EXEC " +
                    "Nomina.SPE_OBTIENE_S_FORMA_PAGO_SAT " +
                    "@PIN_CL_FORMA_PAGO, " +
                    "@PIN_NB_FORMA_PAGO, " +
                    "@PIN_FG_ACTIVO",
                    new SqlParameter("@PIN_CL_FORMA_PAGO", (object)CL_FORMA_PAGO ?? DBNull.Value),
                    new SqlParameter("@PIN_NB_FORMA_PAGO", (object)NB_FORMA_PAGO ?? DBNull.Value),
                    new SqlParameter("@PIN_FG_ACTIVO", (object)FG_ACTIVO ?? DBNull.Value)
                ).ToList();
            }
        }

        public List<E_BANCO> ObtieneBancosNomina(string CL_BANCO = null, string NB_BANCO = null, bool? FG_ACTIVO = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<E_BANCO>("EXEC " +
                    "Nomina.SPE_OBTIENE_S_BANCO_SAT " +
                    "@PIN_CL_BANCO, " +
                    "@PIN_NB_BANCO, " +
                    "@PIN_FG_ACTIVO ",
                    new SqlParameter("@PIN_CL_BANCO", (object)CL_BANCO ?? DBNull.Value),
                    new SqlParameter("@PIN_NB_BANCO", (object)NB_BANCO ?? DBNull.Value),
                    new SqlParameter("@PIN_FG_ACTIVO", (object)FG_ACTIVO ?? DBNull.Value)
                ).ToList();
            }
        }
    }
}
