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
    }
}
