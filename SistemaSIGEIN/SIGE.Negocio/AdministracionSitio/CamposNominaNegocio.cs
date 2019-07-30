using SIGE.AccesoDatos.Implementaciones.Administracion;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Negocio.AdministracionSitio
{
    public class CamposNominaNegocio
    {
        public List<E_CAMPO_NOMINA_DO> ObtenerConfiguracionCampo(int? pID_CAMPO = null, string pCL_CAMPO = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            var vConfiguracionCampos = oCampo.ObtenerConfiguracionCampo(pID_CAMPO, pCL_CAMPO).ToList();

            return (from x in vConfiguracionCampos
                    select new E_CAMPO_NOMINA_DO
                    {
                        CL_CAMPO = x.CL_CAMPO,
                        FG_EDITABLE_NOMINA = (bool)x.FG_EDITABLE_NOMINA,
                        FG_EDITABLE_DO = (bool)x.FG_EDITABLE_DO,
                        FE_MODIFICACION = x.FE_MODIFICACION,
                        CL_ULTIMO_USUARIO_MODIFICA = x.CL_ULTIMO_USUARIO_MODIFICA
                    }).ToList();
        }

        public E_RESULTADO InsertaActualizaConfiguracionCampos(string pXML_CONFIGURACION = null, string pClUsuario = null, string pNbPrograma = null, string pClTipoTransaccion = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return UtilRespuesta.EnvioRespuesta(oCampo.InsertaActualizaConfiguracionCampos(pXML_CONFIGURACION, pClUsuario, pNbPrograma, pClTipoTransaccion));
        }

        public E_RESULTADO InsertaActualizaPuesto(int? pID_PUESTO = null, string pCL_PUESTO = null, string pNB_PUESTO = null, bool? pFG_NOMINA = null, bool? pFG_DO = null, string pClUsuario = null, string pNbPrograma = null, string pClTipoTransaccion = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return UtilRespuesta.EnvioRespuesta(oCampo.InsertaActualizaPuesto(pID_PUESTO, pCL_PUESTO, pNB_PUESTO, pFG_NOMINA, pFG_DO, pClUsuario, pNbPrograma, pClTipoTransaccion));
        }

        public List<SPE_OBTIENE_CENTROS_OPERATIVOS_Result> ObtieneCentrosOptvos(System.Guid? pID_CENTRO_OPTVO = null, string pCL_CENTRO_OPTVO = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return oCampo.ObtieneCentrosOptvos(pID_CENTRO_OPTVO, pCL_CENTRO_OPTVO);
        }

        public List<SPE_OBTIENE_CENTROS_ADMINISTRATIVOS_Result> ObtieneCentrosAdmvo(System.Guid? pID_CENTRO_ADMVO = null, string pCL_CENTRO_ADMVO = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return oCampo.ObtieneCentrosAdmvo(pID_CENTRO_ADMVO, pCL_CENTRO_ADMVO);
        }

        public List<SPE_OBTIENE_PUESTOS_NOMINA_DO_Result> ObtienePuestosNominaDo(int? pID_PUESTO_NOMINA_DO = null, string pCL_PUESTO = null, bool? pFG_DO = null, bool? FG_NOMINA = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return oCampo.ObtienePuestosNominaDo(pID_PUESTO_NOMINA_DO, pCL_PUESTO, pFG_DO, FG_NOMINA);
        }

        public E_RESULTADO EliminaPuestoNominaDO(int? pID_PUESTO = null, string pCL_PUESTO = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return UtilRespuesta.EnvioRespuesta(oCampo.EliminaPuestoNominaDO(pID_PUESTO, pCL_PUESTO));
        }

        public E_RESULTADO InsertaActualizaEmpleado(int? pIdEmpleado = null, E_EMPLEADO_NOMINA_DO pEmpleado = null, string pClUsuario = null, string pNbPrograma = null, string pClTipoTransaccion = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return UtilRespuesta.EnvioRespuesta(oCampo.InsertaActualizaEmpleado(pIdEmpleado, pEmpleado, pClUsuario, pNbPrograma, pClTipoTransaccion));
        }

        public List<E_EMPLEADO_NOMINA_DO> ObtienePersonalNominaDo(int? pID_EMPLEADO = null, string pCL_EMPLEADO = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return oCampo.ObtienePersonalNominaDo(pID_EMPLEADO, pCL_EMPLEADO);
        }

        public List<SPE_OBTIENE_EMPLEADOS_NOMINA_DO_Result> ObtieneEmpleadosNominaDo(int? pID_EMPLEADO_NOMINA_DO = null, string pCL_EMPLEADO = null, System.Guid? pID_EMPLEADO_NOMINA = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return oCampo.ObtieneEmpleadosNominaDo(pID_EMPLEADO_NOMINA_DO, pCL_EMPLEADO, pID_EMPLEADO_NOMINA);
        }

        public E_RESULTADO EliminaEmpleadoNominaDO(int? pID_EMPLEADO = null, string pCL_EMPLEADO = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return UtilRespuesta.EnvioRespuesta(oCampo.EliminaEmpleadoNominaDO(pID_EMPLEADO, pCL_EMPLEADO));
        }

        public List<SPE_OBTIENE_RAZON_SOCIAL_NOMINA_Result> ObtenerRazonSocialNomina(System.Guid? pID_RAZON_SOCIAL = null, string pCL_CLIENTE = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return oCampo.ObtenerRazonSocialNomina(pID_RAZON_SOCIAL, pCL_CLIENTE);
        }

        public List<E_PERSONAL> ObtieneEmpleadosGenerales(string pCL_EMPLEADO = null, bool? pFG_ACTIVO = null, int? pID_EMPRESA = null, int? pID_ROL = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return oCampo.ObtieneEmpleadosGenerales(pCL_EMPLEADO, pFG_ACTIVO, pID_EMPRESA, pID_ROL);
        }

        public List<E_RAZON_SOCIAL> ObtieneRazonSocial(string CL_CLIENTE = null, bool? FG_ACTIVO = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return oCampo.ObtieneRazonSocial(CL_CLIENTE, FG_ACTIVO);
        }

        public List<E_REGISTRO_PATRONAL> ObtieneRegistroPatronal(Guid? ID_RAZON_SOCIAL = null, bool? FG_ACTIVO = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return oCampo.ObtieneRegistroPatronal(ID_RAZON_SOCIAL, FG_ACTIVO);
        }

        public List<E_TIPO_TRABAJO_SUA> ObtieneTipoTrabajoSUA(double? CL_TIPO_TRAB_SUA = null, string DS_TIPO_TRAB_SUA = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return oCampo.ObtieneTipoTrabajoSUA(CL_TIPO_TRAB_SUA, DS_TIPO_TRAB_SUA);
        }
        public List<E_TIPO_JORNADA_SUA> ObtieneTipoJornadaSUA(double? CL_JORNADA_SUA = null, string DS_JORNADA_SUA = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return oCampo.ObtieneTipoJornadaSUA(CL_JORNADA_SUA, DS_JORNADA_SUA);
        }
        public List<E_TIPO_CONTRATO_SAT> ObtieneTipoContratoSAT(string CL_TIPO_CONTRATO = null, string DS_TIPO_CONTRATO = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return oCampo.ObtieneTipoContratoSAT(CL_TIPO_CONTRATO, DS_TIPO_CONTRATO);
        }

        public List<E_TIPO_JORNADA_SAT> ObtieneTipoJornadaSAT(string CL_TIPO_JORNADA = null, string DS_TIPO_JORNADA = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return oCampo.ObtieneTipoJornadaSAT(CL_TIPO_JORNADA, DS_TIPO_JORNADA);
        }

        public List<E_REGIMEN_SAT> ObtieneRegimenSAT()
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return oCampo.ObtieneRegimenSAT();
        }

        public List<E_TIPO_SALARIO_SUA> ObtieneTipoSalarioSUA()
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return oCampo.ObtieneTipoSalarioSUA();
        }

        public List<E_RIESGO_PUESTO> ObtieneRiesgoPuesto()
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return oCampo.ObtieneRiesgoPuesto();
        }

        public List<E_HORARIO_SEMANA> ObtieneHorarioSemana()
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return oCampo.ObtieneHorarioSemana();
        }

        public List<E_PAQUETE_PRESTACIONES> ObtienePaquetePrestaciones()
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return oCampo.ObtienePaquetePrestaciones();
        }

        public List<E_FORMATO_DISPERSION> ObtieneFormatoDispersion(E_FORMATO_DISPERSION formatoDispersion = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return oCampo.ObtieneFormatoDispersion(formatoDispersion.CL_FORMATO, formatoDispersion.CL_TIPO_FORMATO, formatoDispersion.NB_FORMATO);

        }

        public List<E_TIPO_NOMINA> ObtieneTipoNomina(E_TIPO_NOMINA tipoNomina = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return oCampo.ObtieneTipoNomina(tipoNomina.ID_TIPO_NOMINA, tipoNomina.CL_CLIENTE, tipoNomina.CL_TIPO_NOMINA, tipoNomina.NB_PERIODICIDAD, tipoNomina.CL_PERIODICIDAD, tipoNomina.DS_TIPO_NOMINA, tipoNomina.FG_ACTIVO);
        }

        public List <E_FORMA_PAGO> ObtieneFormaPago(E_FORMA_PAGO formaPago = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return oCampo.ObtieneFormaPago(formaPago.CL_FORMA_PAGO, formaPago.NB_FORMA_PAGO, formaPago.FG_ACTIVO);
        }

        public List<E_BANCO> ObtieneBancosNomina(E_BANCO banco = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return oCampo.ObtieneBancosNomina(banco.CL_BANCO, banco.NB_BANCO, banco.FG_ACTIVO);
        }

        public List<E_ANTIGUEDAD> ObtenerTablaAntiguedad(Guid? ID_TABLA_ANTIGUEDAD = null, String CL_CLIENTE = null, Guid? ID_PAQUETE_PRESTACIONES = null, short? NO_ANTIGUEDAD = null, short? NO_DIAS_VACACIONES = null, short? NO_DIAS_PRIMA_VAC = null, Decimal? NO_FACTOR_SBC = null, Decimal? NO_CAMPO01 = null, Decimal? NO_CAMPO02 = null, Decimal? NO_CAMPO03 = null, Decimal? NO_CAMPO04 = null, Decimal? NO_CAMPO05 = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return oCampo.ObtenerTablaAntiguedad(ID_TABLA_ANTIGUEDAD, CL_CLIENTE, ID_PAQUETE_PRESTACIONES, NO_ANTIGUEDAD, NO_DIAS_VACACIONES, NO_DIAS_PRIMA_VAC, NO_FACTOR_SBC, NO_CAMPO01, NO_CAMPO02, NO_CAMPO03, NO_CAMPO04, NO_CAMPO05);
        }

        public List<E_CONFIGURACION> ObtenerConfiguracion(E_CONFIGURACION pConfiguracion = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return oCampo.ObtenerConfiguracion(pConfiguracion.CL_CLIENTE, pConfiguracion.CL_CONFIGURACION, pConfiguracion.NB_CONFIGURACION, pConfiguracion.NO_CONFIGURACION, pConfiguracion.DS_CONFIGURACION);
        }

        public List<E_UMA> ObtenerUMA(Guid? ID_UMA = null, DateTime? FE_INICIAL = null, DateTime? FE_FINAL = null, Decimal? MN_UMA = null)
        {
            CamposNominaOperaciones oCampo = new CamposNominaOperaciones();
            return oCampo.ObtenerUMA(ID_UMA, FE_INICIAL, FE_FINAL, MN_UMA);
        }
    }
}
