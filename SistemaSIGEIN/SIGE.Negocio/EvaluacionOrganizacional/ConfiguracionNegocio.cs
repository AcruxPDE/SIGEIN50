using SIGE.AccesoDatos.Implementaciones.EvaluacionOrganizacional;
using SIGE.Entidades;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.Negocio.EvaluacionOrganizacional
{
    public class ConfiguracionNegocio
    {
        public List<E_EMPLEADOS> ObtenerEmpleados(XElement pXmlSeleccion = null)
        {
            ConfiguracionOperaciones op = new ConfiguracionOperaciones();

            List<SPE_OBTIENE_EMPLEADOS_Result> oLista = op.ObtieneEmpleados(pXmlSeleccion);

            List<E_EMPLEADOS> oEmpleados = (oLista.Select(t => new E_EMPLEADOS
            {
                M_EMPLEADO_ID_EMPLEADO = t.M_EMPLEADO_ID_EMPLEADO,
                M_EMPLEADO_CL_EMPLEADO = t.M_EMPLEADO_CL_EMPLEADO,
                M_EMPLEADO_NB_EMPLEADO_COMPLETO = t.M_EMPLEADO_NB_EMPLEADO_COMPLETO,
                M_EMPLEADO_NB_EMPLEADO = t.M_EMPLEADO_NB_EMPLEADO,
                M_EMPLEADO_NB_APELLIDO_PATERNO = t.M_EMPLEADO_NB_APELLIDO_PATERNO,
                M_EMPLEADO_NB_APELLIDO_MATERNO = t.M_EMPLEADO_NB_APELLIDO_MATERNO,
                M_EMPLEADO_CL_ESTADO_EMPLEADO = t.M_EMPLEADO_CL_ESTADO_EMPLEADO,
                M_EMPLEADO_CL_GENERO = t.M_EMPLEADO_CL_GENERO,
                M_EMPLEADO_CL_ESTADO_CIVIL = t.M_EMPLEADO_CL_ESTADO_CIVIL,
                M_EMPLEADO_NB_CONYUGUE = t.M_EMPLEADO_NB_CONYUGUE,
                M_EMPLEADO_CL_RFC = t.M_EMPLEADO_CL_RFC,
                M_EMPLEADO_CL_CURP = t.M_EMPLEADO_CL_CURP,
                M_EMPLEADO_CL_NSS = t.M_EMPLEADO_CL_NSS,
                M_EMPLEADO_CL_TIPO_SANGUINEO = t.M_EMPLEADO_CL_TIPO_SANGUINEO,
                M_EMPLEADO_CL_NACIONALIDAD = t.M_EMPLEADO_CL_NACIONALIDAD,
                M_EMPLEADO_NB_PAIS = t.M_EMPLEADO_NB_PAIS,
                M_EMPLEADO_NB_ESTADO = t.M_EMPLEADO_NB_ESTADO,
                M_EMPLEADO_NB_MUNICIPIO = t.M_EMPLEADO_NB_MUNICIPIO,
                M_EMPLEADO_NB_COLONIA = t.M_EMPLEADO_NB_COLONIA,
                M_EMPLEADO_NB_CALLE = t.M_EMPLEADO_NB_CALLE,
                M_EMPLEADO_NO_INTERIOR = t.M_EMPLEADO_NO_INTERIOR,
                M_EMPLEADO_NO_EXTERIOR = t.M_EMPLEADO_NO_EXTERIOR,
                M_EMPLEADO_CL_CODIGO_POSTAL = t.M_EMPLEADO_CL_CODIGO_POSTAL,
                M_EMPLEADO_XML_TELEFONOS = t.M_EMPLEADO_XML_TELEFONOS,
                M_EMPLEADO_CL_CORREO_ELECTRONICO = t.M_EMPLEADO_CL_CORREO_ELECTRONICO,
                M_EMPLEADO_ACTIVO = t.M_EMPLEADO_ACTIVO,
                M_EMPLEADO_FE_NACIMIENTO = t.M_EMPLEADO_FE_NACIMIENTO,
                M_EMPLEADO_DS_LUGAR_NACIMIENTO = t.M_EMPLEADO_DS_LUGAR_NACIMIENTO,
                M_EMPLEADO_FE_ALTA = t.M_EMPLEADO_FE_ALTA,
                M_EMPLEADO_FE_BAJA = t.M_EMPLEADO_FE_BAJA,
                M_EMPLEADO_MN_SUELDO = t.M_EMPLEADO_MN_SUELDO,
                //M_EMPLEADO_MN_SUELDO_VARIA = t.M_EMPLEADO_MN_SUELDO_VARIA,
                M_EMPLEADO_DS_SUELDO_COMPOSICION = t.M_EMPLEADO_DS_SUELDO_COMPOSICION,
                M_EMPLEADO_ID_CANDIDATO = t.M_EMPLEADO_ID_CANDIDATO,
                M_EMPLEADO_XML_CAMPOS_ADICIONALES = t.M_EMPLEADO_XML_CAMPOS_ADICIONALES,
                M_PUESTO_CL_PUESTO = t.M_PUESTO_CL_PUESTO,
                M_PUESTO_NB_PUESTO = t.M_PUESTO_NB_PUESTO,
                M_PUESTO_XML_CAMPOS_ADICIONALES = t.M_PUESTO_XML_CAMPOS_ADICIONALES,
                C_EMPRESA_CL_EMPRESA = t.C_EMPRESA_CL_EMPRESA,
                C_EMPRESA_NB_EMPRESA = t.C_EMPRESA_NB_EMPRESA,
                C_EMPRESA_NB_RAZON_SOCIAL = t.C_EMPRESA_NB_RAZON_SOCIAL,
                M_DEPARTAMENTO_CL_DEPARTAMENTO = t.M_DEPARTAMENTO_CL_DEPARTAMENTO,
                M_DEPARTAMENTO_NB_DEPARTAMENTO = t.M_DEPARTAMENTO_NB_DEPARTAMENTO,
                M_DEPARTAMENTO_XML_CAMPOS_ADICIONALES = t.M_DEPARTAMENTO_XML_CAMPOS_ADICIONALES
            })).ToList();

            return oEmpleados;
        }

        public List<SPE_OBTIENE_CONFIGURACION_EVALUACION_ORGANIZACIONAL_Result> ObteneConfiguracionEvaluacionOrganizacional(string pClTipoNotificacion)
        {
            ConfiguracionOperaciones oConfiguracion = new ConfiguracionOperaciones();
            return oConfiguracion.ObtenerConfiguracionEvaluacionOrganizacional(pClTipoNotificacion);
        }

        public E_RESULTADO InsertaConfiguracionNotificado(string pXmlEmpleados, string pClTipoNotificacion, string pClUsuario, string pNbPrograma)
        {
            ConfiguracionOperaciones oPeriodo = new ConfiguracionOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.InsertarConfiguracionNotificado(pXmlEmpleados, pClTipoNotificacion, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO EliminaConfiguracionNotificado(string pXmlEmpleados)
        {
            ConfiguracionOperaciones oPeriodo = new ConfiguracionOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPeriodo.EliminarConfiguracionNotificado(pXmlEmpleados));
        }

      
    }
}
