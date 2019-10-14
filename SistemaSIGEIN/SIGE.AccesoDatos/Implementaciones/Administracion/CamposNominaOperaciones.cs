using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
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

        public XElement InsertaActualizaEmpleadoCandidato(string pXmlDatosCandidato, string pClUsuario = null, string pNbPrograma = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                var pXmlResultado = new SqlParameter("@XML_RESULTADO", SqlDbType.Xml)
                {
                    Direction = ParameterDirection.Output
                };

                contexto.Database.ExecuteSqlCommand("EXEC " +
                    "ADM.SPE_INSERTA_EMPLEADO_CONTRATADO_SOLICITUD " +
                    "@XML_RESULTADO OUTPUT, " +
                    "@PIN_XML_DATOS_CANDIDATO, " +
                    "@PIN_CL_USUARIO, " +
                    "@PIN_NB_PROGRAMA "
                    , pXmlResultado
                    , new SqlParameter("@PIN_XML_DATOS_CANDIDATO", (object)pXmlDatosCandidato ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_USUARIO", (object)pClUsuario ?? DBNull.Value)
                    , new SqlParameter("@PIN_NB_PROGRAMA", (object)pNbPrograma ?? DBNull.Value)
                );

                return XElement.Parse(pXmlResultado.Value.ToString());
            }
        }
        public XElement InsertaActualizaEmpleadoReingreso(string pXmlDatosEmpleado, string pClUsuario = null, string pNbPrograma = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                var pXmlResultado = new SqlParameter("@XML_RESULTADO", SqlDbType.Xml)
                {
                    Direction = ParameterDirection.Output
                };

                contexto.Database.ExecuteSqlCommand("EXEC " +
                    "ADM.SPE_ACTUALIZA_EMPLEADO_REINGRESO " +
                    "@XML_RESULTADO OUTPUT, " +
                    "@PIN_XML_DATOS_EMPLEADO, " +
                    "@PIN_CL_USUARIO, " +
                    "@PIN_NB_PROGRAMA "
                    , pXmlResultado
                    , new SqlParameter("@PIN_XML_DATOS_EMPLEADO", (object)pXmlDatosEmpleado ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_USUARIO", (object)pClUsuario ?? DBNull.Value)
                    , new SqlParameter("@PIN_NB_PROGRAMA", (object)pNbPrograma ?? DBNull.Value)
                );

                return XElement.Parse(pXmlResultado.Value.ToString());
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

        public List<E_SOLICITUD> ObtieneCandidatoSolicitud(int? pID_SOLICITUD = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<E_SOLICITUD>("EXEC " +
                    "ADM.SPE_OBTIENE_CANDIDATO_SOLICITUD " +
                    "@PIN_ID_SOLICITUD ",
                    new SqlParameter("@PIN_ID_SOLICITUD", (object)pID_SOLICITUD ?? DBNull.Value)
                ).ToList();
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

        public List<E_REGISTRO_PATRONAL> ObtieneRegistroPatronal(Guid? ID_RAZON_SOCIAL = null, Guid? ID_REGISTRO_PATRONAL = null, bool? FG_ACTIVO = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<E_REGISTRO_PATRONAL>("EXEC " +
                    "ADM.SPE_OBTIENE_C_REGISTRO_PATRONAL " +
                    "@PIN_ID_RAZON_SOCIAL, " +
                    "@PIN_ID_REGISTRO_PATRONAL, " +
                    "@PIN_FG_ACTIVO ",
                    new SqlParameter("@PIN_ID_RAZON_SOCIAL", (object)ID_RAZON_SOCIAL ?? DBNull.Value),
                    new SqlParameter("@PIN_ID_REGISTRO_PATRONAL", (object)ID_REGISTRO_PATRONAL ?? DBNull.Value),
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
                    "Nomina.SPE_OBTIENE_VW_JORNADA_SUA " +
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

        public List<E_ANTIGUEDAD> ObtenerTablaAntiguedad(Guid? ID_TABLA_ANTIGUEDAD = null, String CL_CLIENTE = null, Guid? ID_PAQUETE_PRESTACIONES = null, short? NO_ANTIGUEDAD = null, short? NO_DIAS_VACACIONES = null, short? NO_DIAS_PRIMA_VAC = null, Decimal? NO_FACTOR_SBC = null, Decimal? NO_CAMPO01 = null, Decimal? NO_CAMPO02 = null, Decimal? NO_CAMPO03 = null, Decimal? NO_CAMPO04 = null, Decimal? NO_CAMPO05 = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<E_ANTIGUEDAD>("EXEC " +
                    "Nomina.SPE_OBTIENE_K_TABLA_ANTIGUEDAD " +
                    "@PIN_ID_TABLA_ANTIGUEDAD, " +
                    "@PIN_CL_CLIENTE, " +
                    "@PIN_ID_PAQUETE_PRESTACIONES, " +
                    "@PIN_NO_ANTIGUEDAD, " +
                    "@PIN_NO_DIAS_VACACIONES, " +
                    "@PIN_NO_DIAS_PRIMA_VAC, " +
                    "@PIN_NO_FACTOR_SBC, " +
                    "@PIN_NO_CAMPO01, " +
                    "@PIN_NO_CAMPO02, " +
                    "@PIN_NO_CAMPO03, " +
                    "@PIN_NO_CAMPO04, " +
                    "@PIN_NO_CAMPO05 ",
                    new SqlParameter("@PIN_ID_TABLA_ANTIGUEDAD", (object)ID_TABLA_ANTIGUEDAD ?? DBNull.Value),
                    new SqlParameter("@PIN_CL_CLIENTE", (object)CL_CLIENTE ?? DBNull.Value),
                    new SqlParameter("@PIN_ID_PAQUETE_PRESTACIONES", (object)ID_PAQUETE_PRESTACIONES ?? DBNull.Value),
                    new SqlParameter("@PIN_NO_ANTIGUEDAD", (object)NO_ANTIGUEDAD ?? DBNull.Value),
                    new SqlParameter("@PIN_NO_DIAS_VACACIONES", (object)NO_DIAS_VACACIONES ?? DBNull.Value),
                    new SqlParameter("@PIN_NO_DIAS_PRIMA_VAC", (object)NO_DIAS_PRIMA_VAC ?? DBNull.Value),
                    new SqlParameter("@PIN_NO_FACTOR_SBC", (object)NO_FACTOR_SBC ?? DBNull.Value),
                    new SqlParameter("@PIN_NO_CAMPO01", (object)NO_CAMPO01 ?? DBNull.Value),
                    new SqlParameter("@PIN_NO_CAMPO02", (object)NO_CAMPO02 ?? DBNull.Value),
                    new SqlParameter("@PIN_NO_CAMPO03", (object)NO_CAMPO03 ?? DBNull.Value),
                    new SqlParameter("@PIN_NO_CAMPO04", (object)NO_CAMPO04 ?? DBNull.Value),
                    new SqlParameter("@PIN_NO_CAMPO05", (object)NO_CAMPO05 ?? DBNull.Value)
                ).ToList();
            }
        }

        public List<E_CONFIGURACION> ObtenerConfiguracion(String CL_CLIENTE = null, String CL_CONFIGURACION = null, String NB_CONFIGURACION = null, String NO_CONFIGURACION = null, String DS_CONFIGURACION = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<E_CONFIGURACION>("EXEC " +
                    "Nomina.SPE_OBTIENE_S_CONFIGURACION " +
                    "@PIN_CL_CLIENTE, " +
                    "@PIN_CL_CONFIGURACION, " +
                    "@PIN_NB_CONFIGURACION, " +
                    "@PIN_NO_CONFIGURACION, " +
                    "@PIN_DS_CONFIGURACION, " +
                    "@PIN_TIPO_CONTROL ",
                    new SqlParameter("@PIN_CL_CLIENTE", (object)CL_CLIENTE ?? DBNull.Value),
                    new SqlParameter("@PIN_CL_CONFIGURACION", (object)CL_CONFIGURACION ?? DBNull.Value),
                    new SqlParameter("@PIN_NB_CONFIGURACION", (object)NB_CONFIGURACION ?? DBNull.Value),
                    new SqlParameter("@PIN_NO_CONFIGURACION", (object)NO_CONFIGURACION ?? DBNull.Value),
                    new SqlParameter("@PIN_DS_CONFIGURACION", (object)DS_CONFIGURACION ?? DBNull.Value),
                    new SqlParameter("@PIN_TIPO_CONTROL", DBNull.Value)
                ).ToList();
            }
        }

        public List<E_UMA> ObtenerUMA(Guid? ID_UMA = null, DateTime? FE_INICIAL = null, DateTime? FE_FINAL = null, Decimal? MN_UMA = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<E_UMA>("EXEC " +
                    "Nomina.SPE_OBTIENE_UMA " +
                    "@PIN_ID_UMA, " +
                    "@PIN_FE_INICIAL, " +
                    "@PIN_FE_FINAL, " +
                    "@PIN_MN_UMA ",
                    new SqlParameter("@PIN_ID_UMA", (object)ID_UMA ?? DBNull.Value),
                    new SqlParameter("@PIN_FE_INICIAL", (object)FE_INICIAL ?? DBNull.Value),
                    new SqlParameter("@PIN_FE_FINAL", (object)FE_FINAL ?? DBNull.Value),
                    new SqlParameter("@PIN_MN_UMA", (object)MN_UMA ?? DBNull.Value)
                ).ToList();
            }
        }

        public List<E_EMPLEADO_NOMINA> ObtieneDatosPersonaNO(int? pID_EMPLEADO, int? pId_ROL)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<E_EMPLEADO_NOMINA>("EXEC " +
                    "ADM.SPE_OBTIENE_PERSONA_NOMINA " +
                    "@PIN_ID_EMPLEADO, " +
                    "@PIN_ID_ROL",
                    new SqlParameter("@PIN_ID_EMPLEADO", (object)pID_EMPLEADO ?? DBNull.Value),
                    new SqlParameter("@PIN_ID_ROL", (object)pId_ROL ?? DBNull.Value)
                ).ToList();
            }
        }

        /////////////////////////////////////////// OBTIENE DATOS   /////////////////////////////////////////////////////////////////////
        public List<E_OBTIENE_S_CONFIGURACION> Obtener_S_CONFIGURACION(String CL_CLIENTE = null, String CL_CONFIGURACION = null, String NB_CONFIGURACION = null, String NO_CONFIGURACION = null, String DS_CONFIGURACION = null, string TIPO_CONTROL = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                var q = from V_S_CONFIGURACION in contexto.Database.SqlQuery<E_OBTIENE_S_CONFIGURACION>("EXEC "+
                        "Nomina.SPE_OBTIENE_S_CONFIGURACION "+
                        " @PIN_CL_CLIENTE," +
                        "@PIN_CL_CONFIGURACION," +
                        "@PIN_NB_CONFIGURACION," +
                        "@PIN_NO_CONFIGURACION," +
                        "@PIN_DS_CONFIGURACION," +
                        "@PIN_TIPO_CONTROL"
                 , new SqlParameter("@PIN_CL_CLIENTE", (object)CL_CLIENTE ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_CONFIGURACION", (object)CL_CONFIGURACION ?? DBNull.Value)
                    , new SqlParameter("@PIN_NB_CONFIGURACION", (object)NB_CONFIGURACION ?? DBNull.Value)
                    , new SqlParameter("@PIN_NO_CONFIGURACION", (object)NO_CONFIGURACION ?? DBNull.Value)
                    , new SqlParameter("@PIN_DS_CONFIGURACION", (object)DS_CONFIGURACION ?? DBNull.Value)
                    , new SqlParameter("@PIN_TIPO_CONTROL", (object)TIPO_CONTROL ?? DBNull.Value))
                        

                        //SPE_OBTIENE_S_CONFIGURACION(CL_CLIENTE, CL_CONFIGURACION, NB_CONFIGURACION, 
                        //NO_CONFIGURACION, DS_CONFIGURACION, TIPO_CONTROL)
                        select V_S_CONFIGURACION;
                return q.ToList();
            }
        }

        public List<E_MENSAJES> insertaLayoutempleados(bool FG_VALIDAR, int NO_LINEA, E_PLANTILLA_NOMINA emp, string CL_USUARIO, string NB_PROGRAMA)
        {
            List<E_MENSAJES> errores = new List<E_MENSAJES>();
            XElement xmlErrores;

            using (contexto = new SistemaSigeinEntities())
            {
                var pout_clave_retorno = new SqlParameter("@POUT_CLAVE_RETORNO", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Output
                };
                var pout_mensaje_retorno = new SqlParameter("@POUT_MENSAJE_RETORNO", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Output
                };
                var pout_xml_mensajes = new SqlParameter("@POUT_XML_MENSAJES", SqlDbType.Xml)
                {
                    Direction = ParameterDirection.Output
                };
                //ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(string));
                //ObjectParameter pout_mensaje_retorno = new ObjectParameter("POUT_MENSAJE_RETORNO", typeof(string));
                //ObjectParameter pout_xml_mensajes = new ObjectParameter("POUT_XML_MENSAJES", typeof(string));

                contexto.Database.ExecuteSqlCommand("EXEC " +
                    "[Nomina].[SPE_INSERTA_LAYOUT_EMPLEADOS]" +
                    "@POUT_CLAVE_RETORNO OUTPUT," +
                    "@POUT_MENSAJE_RETORNO OUTPUT," +
                    "@POUT_XML_MENSAJES OUTPUT," +
                    "@PIN_FG_VALIDAR," +
                    "@PIN_CL_CLIENTE," +
                    " @PIN_CL_RAZON_SOCIAL ," +
                    "@PIN_CL_TRABAJADOR," +
                    "@PIN_ID_EMPLEADO," +
                    "@PIN_NB_NOMBRES," +
                    "@PIN_NB_PATERNO," +
                    "@PIN_NB_MATERNO," +
                    "@PIN_NB_TRABAJADOR," +
                    "@PIN_CL_SEXO," +
                    "@PIN_CL_ESTADO_CIVIL," +
                    "@PIN_DS_NACIONALIDAD," +
                    "@PIN_CL_RFC," +
                    "@PIN_CL_CURP," +
                    "@PIN_NO_TELEFONO_FIJO," +
                    "@PIN_NO_TELEFONO_CELULAR," +
                    "@PIN_DS_EMAIL," +
                    "@PIN_DS_ACCIDENTE," +
                    "@PIN_FE_NACIMIENTO," +
                    "@PIN_DS_LUGAR_NACIMIENTO," +
                    "@PIN_CL_ESTADO_NACIMIENTO," +
                    "@PIN_NB_PADRE," +
                    "@PIN_NB_MADRE," +
                    "@PIN_CL_ESTADO," +
                    "@PIN_NB_MUNICIPIO," +
                    "@PIN_NB_COLONIA," +
                    "@PIN_NB_CALLE," +
                    "@PIN_CL_CP," +
                    "@PIN_NO_EXTERIOR," +
                    "@PIN_NO_INTERIOR," +
                    "@PIN_CL_REGISTRO_PATRONAL," +
                    "@PIN_FG_COTIZA_IMSS," +
                    "@PIN_NO_IMSS," +
                    "@PIN_NO_UMF," +
                    "@PIN_DS_GRUPO_SANGUINEO," +
                    "@PIN_CL_TIPO_TRAB_SUA," +
                    "@PIN_CL_JORNADA_SUA," +
                    "@PIN_CL_UBICACION_SUA," +
                    "@PIN_CL_TIPO_SALARIO_SUA," +
                    "@PIN_CL_HORARIO," +
                    "@PIN_CL_DEPARTAMENTO," +
                    "@PIN_CL_PUESTO," +
                    "@PIN_CL_CENTRO_ADMVO," +
                    "@PIN_CL_CENTRO_OPERATIVO," +
                    "@PIN_CL_PAQUETE_PRESTACIONES," +
                    "@PIN_CL_FORMATO_DISPERSION," +
                    "@PIN_NO_CUENTA_DESPENSA," +
                    "@PIN_CL_FORMATO_VALES_D," +
                    "@PIN_CL_FORMATO_VALES_G," +
                    "@PIN_FE_REINGRESO," +
                    "@PIN_FE_PLANTA," +
                    "@PIN_CL_TIPO_NOMINA," +
                    "@PIN_CL_FORMA_PAGO," +
                    "@PIN_CL_BANCO_SAT," +
                    "@PIN_CL_HORARIO_SEMANA," +
                    "@PIN_MN_SNOMINAL," +
                    "@PIN_MN_SNOMINAL_MENSUAL," +
                    "@PIN_NO_FACTOR_SBC," +
                    "@PIN_NO_CUENTA_PAGO," +
                    "@PIN_NO_CLABE_PAGO," +
                    "@PIN_FILLER01," +
                    "@PIN_FILLER02," +
                    "@PIN_FILLER03," +
                    "@PIN_FILLER04," +
                    "@PIN_FILLER05," +
                    "@PIN_CL_USUARIO_APP_CREA," +
                    "@PIN_NB_PROGRAMA_CREA," +
                    "@PIN_MN_SBC_FIJO," +
                    "@PIN_MN_SBC_VARIABLE," +
                    "@PIN_MN_SBC_DETERMINADO," +
                    "@PIN_MN_SBC_MAXIMO," +
                    "@PIN_MN_SBC," +
                    "@PIN_CL_TIPO_CONTRATO_SAT," +
                    "@PIN_CL_TIPO_JORNADA_SAT," +
                    "@PIN_CL_RIESGO_TRABAJO," +
                    "@PIN_ID_SOLICITUD INT," +
                    "@PIN_CL_SUBCONTRATADO," +
                    "@PIN_CL_REGIMEN_CONTRATACION," +
                    "@PIN_XML_TELEFONOS," +
                    "@PIN_CL_PENSIONADO"
                    , pout_clave_retorno
                    , pout_mensaje_retorno
                    , pout_xml_mensajes
                    ,new SqlParameter("@PIN_FG_VALIDAR", (object)FG_VALIDAR ?? DBNull.Value)
                    ,new SqlParameter("@PIN_CL_CLIENTE",(object) emp.CL_CLIENTE ?? DBNull.Value)
                    ,new SqlParameter ("@PIN_CL_RAZON_SOCIAL", (object) emp.CL_RAZON_SOCIAL ?? DBNull.Value) 
                    ,new SqlParameter ("@PIN_CL_TRABAJADOR", (object) emp.CL_TRABAJADOR ?? DBNull.Value)
                    , new SqlParameter ("@PIN_ID_EMPLEADO", (object) emp.ID_EMPLEADO ?? DBNull.Value)
                    , new SqlParameter ("@PIN_NB_NOMBRES", (object) emp.NB_NOMBRES ?? DBNull.Value)
                    , new SqlParameter ("@PIN_NB_PATERNIO", (object) emp.NB_PATERNO ?? DBNull.Value)
                    , new SqlParameter ("@PIN_NB_MATERNO", (object) emp.NB_MATERNO ?? DBNull.Value)
                    , new SqlParameter ("@PIN_NB_TRABAJADOR", (object) emp.NB_TRABAJADOR ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_SEXO", (object) emp.CL_SEXO ?? DBNull.Value) 
                    , new SqlParameter ("@PIN_CL_ESTADO_CIVIL", (object) emp.CL_ESTADO_CIVIL ?? DBNull.Value)
                    , new SqlParameter ("@PIN_DS_NACIONALIDAD", (object) emp.DS_NACIONALIDAD ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_RFC", (object)emp.CL_RFC ?? DBNull.Value)
                    , new SqlParameter("@PIN_CL_CURP", (object) emp.CL_CURP ?? DBNull.Value)
                    , new SqlParameter("@PIN_NO_TELEFONO_FIJO", (object) emp.NO_TELEFONO_FIJO ?? DBNull.Value)
                    , new SqlParameter("@PIN_NO_TELEFONO_CELULAR", (object) emp.NO_TELEFONO_CELULAR ?? DBNull.Value) 
                    , new SqlParameter ("@PIN_DS_EMAIL", (object) emp.DS_EMAIL ?? DBNull.Value)
                    , new SqlParameter ("@PIN_DS_ACCIDENTE", (object) emp.DS_ACCIDENTE ?? DBNull.Value)
                    , new SqlParameter ("@PIN_FE_NACIMIENTO", (object) emp.FE_NACIMIENTO ?? DBNull.Value)
                    , new SqlParameter ("@PIN_DS_LUGAR_NACIMIENTO", (object) emp.DS_LUGAR_NACIMIENTO ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_ESTADO_NACIMIENTO", (object) emp.CL_ESTADO_NACIMIENTO ?? DBNull.Value)
                    , new SqlParameter ("@PIN_NB_PADRE", (object) emp.NB_PADRE ?? DBNull.Value)
                    , new SqlParameter ("@PIN_NB_MADRE", (object) emp.NB_MADRE ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_ESTADO",(object) emp.CL_ESTADO ?? DBNull.Value)
                    , new SqlParameter ("@PIN_NB_MUNICIPIO", (object) emp.NB_MUNICIPIO ?? DBNull.Value)
                    , new SqlParameter ("@PIN_NB_COLONIA", (object) emp.NB_COLONIA ?? DBNull.Value)
                    , new SqlParameter ("@PIN_NB_CALLE", (object) emp.NB_CALLE ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_CP", (object) emp.CL_CP ?? DBNull.Value)
                    , new SqlParameter ("@PIN_NO_EXTERIOR", (object) emp.NO_EXTERIOR ?? DBNull.Value)
                    , new SqlParameter ("@PIN_NO_INTERIOR", (object) emp.NO_INTERIOR ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_REGISTRO_PATRONAL", (object) emp.CL_REGISTRO_PATRONAL ?? DBNull.Value)
                    ,new SqlParameter ("@PIN_FG_COTIZA_IMSS", (object) emp.FG_COTIZA_IMSS ?? DBNull.Value)
                    , new SqlParameter ("@PIN_NO_IMSS", (object) emp.NO_IMSS ?? DBNull.Value)
                    , new SqlParameter ("@PIN_NO_UMF", (object) emp.NO_UMF ?? DBNull.Value)
                    , new SqlParameter ("@PIN_DS_GRUPOI_SANGUINEO", (object) emp.DS_GRUPO_SANGUINEO ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_TIPO_TRAB_SUA", (object) emp.CL_TIPO_TRAB_SUA ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_JORNADA_SUA", (object) emp.CL_JORNADA_SUA ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_UBICACION_SUA", (object) emp.CL_UBICACION_SUA ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_TIPO_SALRIO_SUA", (object) emp.CL_TIPO_SALARIO_SUA ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_HORARIO", (object) emp.CL_HORARIO ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_DEPARTAMENTO", (object) emp.CL_DEPARTAMENTO ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_PUESTO", (object) emp.CL_PUESTO ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_CENTRO_ADMVO", (object) emp.CL_CENTRO_ADMVO ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_CENTRO_OPERATIVO", (object) emp.CL_CENTRO_OPERATIVO ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_PAQUETE", (object) emp.CL_PAQUETE ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_FORMATO_DISPERSION", (object) emp.CL_FORMATO_DISPERSION ?? DBNull.Value)
                    , new SqlParameter ("@PIN_NO_CUENTA_DESPENSA", (object) emp.NO_CUENTA_DESPENSA ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_FORMATO_VALES_D", (object) emp.CL_FORMATO_VALES_D ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_FORMATO_VALES_G", (object) emp.CL_FORMATO_VALES_G ?? DBNull.Value)
                    , new SqlParameter ("@PIN_FE_REINGRESO", (object) emp.FE_REINGRESO ?? DBNull.Value)
                    , new SqlParameter ("@PIN_FE_PLANTA", (object) emp.FE_PLANTA ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_TIPO_NOMINA", (object) emp.CL_TIPO_NOMINA ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_FORMA_PAGO", (object) emp.CL_FORMA_PAGO ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_BANCO_SAT", (object)emp.CL_BANCO_SAT ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_HORARIO_SEMANA", (object) emp.CL_HORARIO_SEMANA ?? DBNull.Value)
                    , new SqlParameter ("@PIN_NB_SNOMINAL", (object) emp.MN_SNOMINAL ?? DBNull.Value)
                    , new SqlParameter ("@PIN_MN_SNOMINAL_MENSUAL", (object) emp.MN_SNOMINAL_MENSUAL ?? DBNull.Value)
                    , new SqlParameter ("@PIN_NO_FACTOR_SBC", (object) emp.NO_FACTOR_SBC ?? DBNull.Value)
                    , new SqlParameter ("@PIN_NO_CUENTA_PAGO", (object) emp.NO_CUENTA_PAGO ?? DBNull.Value)
                    , new SqlParameter ("@PIN_NO_CLABE_PAGO", (object)emp.NO_CLABE_PAGO ?? DBNull.Value)
                    , new SqlParameter ("@PIN_FILLER01", (object) emp.FILLER01 ?? DBNull.Value)
                    , new SqlParameter("@PIN_FILLER02", (object)emp.FILLER02 ?? DBNull.Value)
                    , new SqlParameter("@PIN_FILLER03", (object)emp.FILLER03 ?? DBNull.Value)
                    , new SqlParameter("@PIN_FILLER04", (object)emp.FILLER04 ?? DBNull.Value)
                    , new SqlParameter("@PIN_FILLER05", (object)emp.FILLER05 ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_USUARIO_APP_CREA", (object) CL_USUARIO ?? DBNull.Value)
                    , new SqlParameter ("@PIN_NB_PROGRAMA_CREA", (object) NB_PROGRAMA ?? DBNull.Value)
                    , new SqlParameter ("@PIN_MN_SBC_FIJO", (object) emp.MN_SBC_FIJO ?? DBNull.Value)
                    , new SqlParameter ("@PIN_MN_SBC_VARIABLE", (object) emp.MN_SBC_VARIABLE ?? DBNull.Value)
                    , new SqlParameter ("@PIN_MN_SBC_DETERMINADO", (object)emp.MN_SBC_DETERMINADO ?? DBNull.Value)
                    , new SqlParameter ("@PIN_MN_SBC_MAXIMO", (object) emp.MN_SBC_MAXIMO ?? DBNull.Value)
                    , new SqlParameter ("@PIN_MN_SBC", (object) emp.MN_SBC ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_TIPO_CONTRATO", (object) emp.CL_TIPO_CONTRATO ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_TIPO_JORNADA", (object) emp.CL_TIPO_JORNADA ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_TIPO_RIESGO_TRABAJO", (object) emp.CL_TIPO_RIESGO_TRABAJO ?? DBNull.Value)
                    , new SqlParameter ("@PIN_ID_SOLICITUD", (object) emp.ID_SOLICITUD ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_SUBCONTRATADO", (object) emp.CL_SUBCONTRATADO ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_REGIMEN_CONTRATACION", (object) emp.CL_REGIMEN_CONTRATACION ?? DBNull.Value)
                    , new SqlParameter ("@PIN_XML_TELEFONOS", (object) emp.XML_TELEFONOS ?? DBNull.Value)
                    , new SqlParameter ("@PIN_CL_PENSIONADO", (object) emp.CL_PENSIONADO ?? DBNull.Value));



                if (pout_clave_retorno.Value.ToString() == "-1")
                {
                    errores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = NO_LINEA.ToString(), NB_MENSAJE_RETORNO = (string)pout_mensaje_retorno.Value });

                    xmlErrores = XElement.Parse(pout_xml_mensajes.Value.ToString());

                    foreach (XElement item in xmlErrores.Elements("ERROR"))
                    {
                        errores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = NO_LINEA.ToString(), NB_MENSAJE_RETORNO = (string)item.Attribute("MENSAJE") });

                    }
                }
                else
                {
                    errores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = NO_LINEA.ToString(), NB_MENSAJE_RETORNO = (string)pout_mensaje_retorno.Value });
                }


            }

            return errores;
        }

        public List<E_PLANTILLA_LAYOUT> Actualiza_K_PLANTILLA_layout(string xmlDatos, bool? esValidacion, string usuario, string programa)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.Database.SqlQuery<E_PLANTILLA_LAYOUT>("EXEC "+
                   "[Nomina].[SPE_ACTUALIZA_K_PLANTILLA_LAYOUT]" +
                    " @PIN_XML_DATOS,"+
                    "@PIN_ES_VALIDACION,"+
                    "@PIN_USUARIO,"+
                    "@PIN_NB_PROGRAMA"
                    ,new SqlParameter ("@PIN_XML_DATOS", (object) xmlDatos ?? DBNull.Value)
                    ,new SqlParameter ("@PIN_ES_VALIDACION", (object) esValidacion ?? DBNull.Value)
                    ,new SqlParameter("@PIN_USUARIO", (object) usuario ?? DBNull.Value)
                    ,new SqlParameter ("@PIN_NB_PROGRAMA", (object) programa ?? DBNull.Value)).ToList();


                //.SPE_ACTUALIZA_K_PLANTILLA_LAYOUT(xmlDatos, esValidacion, usuario, programa).ToList();
            }
        }

    }
}
